using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloPatient;
using gloPatientStripControl;
using System.Linq;

using System.Collections;
using System.Globalization;
using System.Resources;

using System.Collections; 
namespace gloStripControl
{

    
             
    public enum PatientInfo
    {
        Gender = 0,
        HandDominance = 1,
        Occupation = 2,
        HomePhone = 3,
        CellPhone = 4,
        ReferralPhysican = 5,
        PharmacyName = 6,
        PharmacyPhone = 7,
        PharmacyFax = 8,
        PrimaryInsurance = 9,
        SecondaryInsurance = 10,
        SSN = 11,
        ProviderName = 12,
        DateofBirth = 13,
        PatientCode = 14,
        Gaurantor = 15

    }
    
    //If Chaged also make chage at gloPatient,gloBilling
    public enum InsuranceTypeFlag
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        Tertiary = 3
    }

    //If Chaged also make chage at gloPatient,gloBilling - Enumerating the Forms where the Patient Strip is to be shown.
    public enum FormName
    {
        None = 0,
        Schedule = 1,
        Billing = 2,
        Temp = 3,
        Appointment = 4,
        NewCharges = 5,
        CMS1500 = 6,
        UB04 = 7,
        PatientAccountView = 8,
        PatientPayment = 9,
        ModifyCharges = 10
    }
       
    public enum InsuracePaymentType
    {
        None = 0, Patient = 1, Insurace = 2, Charges = 3
    }

    public partial class gloPatientStrip_FA : UserControl
    {
  
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle &= ~0x02000000;
                cp.ExStyle &= ~0x00000020;
                return cp;
            }
        }


        #region " Constants "

        // Patient Grid
        private const byte COL_PAT_ID = 0;
        private const byte COL_PAT_Code = 1;
        private const byte COL_PAT_FirstName = 2;
        private const byte COL_PAT_MI = 3;
        private const byte COL_PAT_LastName = 4;
        private const byte COL_PAT_SSN = 5;
        private const byte COL_PAT_Provider = 6;
        private const byte COL_PAT_DOB = 7;
        private const byte COL_PAT_Phone = 8;
        private const byte COL_PAT_Mobile = 9;

        //Account Grid
        private const byte COL_Act_IsAddressAvailble = 0;
        private const byte COL_Act_PAT_ID = 1;
        private const byte COL_Act_ID = 2;
        private const byte COL_Act_No = 3;
        private const byte COL_Act_GuarantorName = 4;
        private const byte COL_Act_BusinessCenter = 5;
        private const byte COL_Act_Name = 6;
        private const byte COL_Act_Address1 = 7;
        private const byte COL_Act_Address2 = 8;
        private const byte COL_Act_City = 9;
        private const byte COL_Act_State = 10;
        private const byte COL_Act_Zip = 11;

        #endregion

        #region " Variables "
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
     
        private ToolTip oToolTip = null;
        private string _DataBaseConnectionString = "";
        private string _MessageBoxCaption = "";
        private Int64 _ClinicID = 1;
        private FormName _ParentForm = FormName.None;
        private bool _DisableComboEvents = false;
        private bool _IsPatientAccFearureEnabled = false;
        private bool _IsFollowUpFeatureEnable = false;

        //Patient Fields
        private Int64 _nPatientID = 0;
        private string _PatientCode = "";
        private string _PatientName = "";
        private string _PatientMedicalCategory = "";
        private string _NextAppointment = "";
        private string _EMRAlert = "";
        private Int64 _nProviderID = 0;
        private string _ProviderName = "";
        private string _Gender = "";
        private string _Guarantor = "";
        private string _PatientsMaritalStatus = "";
        private DateTime _DateOfBirth;
        private string _BirthTime = "";
        private Image _patientPhoto;

        //PAF fields : Added By Mahesh S(Apollo)
        private Int64 _nPAccountID = 0;
        private Int64 _nGuarantorID = 0;
        private Int64 _nAccountPatientID = 0;

        private Int64 _FormSelectedPatientID = 0;
        private bool _IsCalledFromChargesOrModifyCharges = false;
       
        //Search : 
        //DataView dvPatient = new DataView();
        //DataView dvAccount = new DataView();
        //DataTable dtTemp = new DataTable();
        //DataView dvNext = new DataView();
        
        // End by Mahesh.S(Apollo)
        
        private Boolean _isRequireBusinessCenteronAccounts=false;
        public bool IsPatientExists = true;
        #endregion

        #region " Properties "

        public bool IsPatientAccFearureEnabled { get { return _IsPatientAccFearureEnabled; } }
        public bool  DisableInputControl
        {           
            set 
            {
                if (value)
                {
                    txtAccountSearch.ReadOnly = true;
                    txtPatientSearch.ReadOnly = true;
                    cmbAccountno.Enabled = false;
                    cmbAccountPatients.Enabled = false;                   
                }
                else
                {
                    txtAccountSearch.ReadOnly = false;
                    txtPatientSearch.ReadOnly = false;
                    cmbAccountno.Enabled = true;
                    cmbAccountPatients.Enabled = true;                  
                }
            }
        }

        public bool DisablePatientChange
        {
            set
            {
                if (value)
                {
                    txtAccountSearch.ReadOnly = true;
                    txtPatientSearch.ReadOnly = true;
                    cmbAccountno.Enabled = true;
                    cmbAccountPatients.Enabled = false;
                }
                else
                {
                    txtAccountSearch.ReadOnly = false;
                    txtPatientSearch.ReadOnly = false;
                    cmbAccountno.Enabled = true;
                    cmbAccountPatients.Enabled = true;
                }
            }
        }

        public DateTime PatientDateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 CmbSelectedPatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 CmbSelectedAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }
      
        public string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }

        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        public string PatientGender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string PatientsMaritalStatus
        {
            get { return _PatientsMaritalStatus; }
            set { _PatientsMaritalStatus = value; }
        }

        public string Guarantor
        {
            get { return _Guarantor; }
            set { _Guarantor = value; }
        }

        public Int64 PatientProviderID
        {
            get { return _nProviderID; }
        }

        public string PatientProviderName
        {
            get { return _ProviderName; }
        }

        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        public Int64 GuarantorID
        {
            get { return _nGuarantorID; }
            set { _nGuarantorID = value; }
        }

        public Int64 AccountPatientID
        {
            get { return _nAccountPatientID; }
            set { _nAccountPatientID = value; }
        }

        public Image PatientPhoto
        {
            get { return _patientPhoto; }
            set {
                if (_patientPhoto != null)
                {
                    _patientPhoto.Dispose();
                    _patientPhoto = null;
                }
                _patientPhoto = value; 
            }
        }

        public bool ShowStatementNotes
        {
            get { return pnlStatementNotes.Visible; }
            set { pnlStatementNotes.Visible = value; }
        }

        public bool IsAllAccPatSelected
        {
            get { return chkAllAcctPat.Checked; }
            set 
            {
                this.chkAllAcctPat.CheckedChanged -= new System.EventHandler(this.chkAllAcctPat_CheckedChanged);
                chkAllAcctPat.Checked = value; 
                this.chkAllAcctPat.CheckedChanged += new System.EventHandler(this.chkAllAcctPat_CheckedChanged); 
            }
        }

        public bool DisableChkAllAcctPat
        {
            set
            {
                if (value)
                {
                    chkAllAcctPat.Enabled = false;
                }
                else
                {
                    chkAllAcctPat.Enabled = true;
                }
            }
        }

        public bool ShowAccountPatientSearch
        {
            set
            {
                if (value)
                {
                    cmbAccountno.Enabled = false;
                    cmbAccountPatients.Enabled = false;
                    pnlSearch.Visible = false;
                    btnEditAccount.Enabled = false;
                    DisableChkAllAcctPat = true;
                    //btn_ModifyPatient.Enabled = false; 
                }
                else
                {
                    cmbAccountno.Enabled = true;
                    cmbAccountPatients.Enabled = true;
                    pnlSearch.Visible = true;
                    btnEditAccount.Enabled = true;
                    DisableChkAllAcctPat = false;
                    //btn_ModifyPatient.Enabled = true; 
                }
            }
        }


        public bool SetFocusOnPatientSearch
        {
            set
            {
                if (value)
                {
                    txtPatientSearch.Focus();
                    txtPatientSearch.Select();
                }
            }
        }

        public Boolean isBlankPatientSearch = false;

        public Boolean IsCalledFromChargesOrModifyCharges
        {
            get { return _IsCalledFromChargesOrModifyCharges; }
            set { _IsCalledFromChargesOrModifyCharges = value; }
        }
        #endregion

        #region " Delegates "

        //Added By Mahesh Satlapalli (Apollo)

        //for get Selected Patient Details in Calling form Bind this Event
        public delegate void PatientChanged(object sender, EventArgs e);
        public event PatientChanged OnPatientChanged;

       
        //for get Selected Account Details in Calling form Bind this Event
        public delegate void AccountChangedHandler(object sender, EventArgs e);
        public event AccountChangedHandler OnAccountChanged;

        public delegate void CmbAccountChangedHandler(object sender, EventArgs e);
        public event CmbAccountChangedHandler OnCmbAccountChanged;

        ////for get Selected Patient Details in Calling form Bind this Event
        //public delegate void AccountPatientChangedHandler(object sender, EventArgs e);
        //public event AccountPatientChangedHandler OnAccountPatientChanged;

        public delegate void PatientModified(object sender, EventArgs e);
        public event PatientModified OnPatientModified;

        // Bug# 48050 - when we hold a insurance plan from PatAcct >> modify patient from banner then it will not refresh the grid.
       // public delegate void InsurancePlanHoldChanged();

       // public event InsurancePlanHoldChanged OnInsurancePlanHoldChanged;
        // End

        #endregion

        #region " Constructors & Distructors "
        public gloPatientStrip_FA()
        {
            #region " Get DatabaseConnectionString from AppSettings "
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            #endregion

            #region " Get MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBoxCaption"] != "")
                { _MessageBoxCaption = Convert.ToString(appSettings["MessageBoxCaption"]); }
                else
                    _MessageBoxCaption = "gloPM";
            }
            else
                _MessageBoxCaption = "gloPM";
            #endregion

            #region " Get ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion


            //SetStyle(ControlStyles.AllPaintingInWmPaint, ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        //for form level settings need to pass FormName
        public gloPatientStrip_FA(FormName oFormName)
        {
            #region " Get DatabaseConnectionString from AppSettings "
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            #endregion

            #region " Get MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBoxCaption"] != "")
                { _MessageBoxCaption = Convert.ToString(appSettings["MessageBoxCaption"]); }
                else
                    _MessageBoxCaption = "gloPM";
            }
            else
                _MessageBoxCaption = "gloPM";
            #endregion

            #region " Get ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion

            _ParentForm = oFormName;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();


        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // DISPOSE ALL UNMANAGED OBJECT HERE //
            if (disposing)
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    c1PatientDetails.DataSource = null;
                    c1FlexAccountGrid.DataSource = null;

                    c1PatientDetails.Styles.Clear();
                    c1FlexAccountGrid.Styles.Clear();
                }
                catch
                {
                }

                if (oToolTip != null) { oToolTip.RemoveAll(); oToolTip.Dispose(); oToolTip = null; }
            }

            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //To Avoid Flickering
        //  SetStyle(ControlStyles.AllPaintingInWmPaint, true);     
        //  InitializeComponent();
        //Then override OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        #endregion

        #region " Events "

        private void gloPatientStrip_FA_Load(object sender, EventArgs e)
        {
            // need to disable AutoScroll, otherwise disabling the horizontal scrollbar doesn't work
            //pnlNotes.AutoScroll = true;
            //pnlNotes.HorizontalScroll.Enabled = false;
         
            pnlNotes.HorizontalScroll.Visible = false;

          

            btn_ModifyPatient.BackgroundImage = global::gloStrips.Properties.Resources.Patient;
            btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;

          
            c1PatientDetails.Visible = false;
            c1FlexAccountGrid.Visible = false;
            SetView();

 

           
        }

        

        private void AddrecentPatient(Int64 PatID, Int64 UserID, ref Hashtable hashtbl)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            string _ProcName;
            Int64 deletedpatid = -1;
            try
            {

                // 'Retrieve the Image for Patient
                // _strSQL = "select iphoto,sGender from patient where npatientid=" & PatientID
                _ProcName = "gsp_InsertRecentPatientUserwise";
                oDB.Connect(false);
              
                     oPara = new gloDatabaseLayer.DBParameters();
                     oPara.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                     oPara.Add("@PatientID", PatID, ParameterDirection.Input, SqlDbType.BigInt); 
                 


                    deletedpatid = Convert.ToInt64(oDB.ExecuteScalar(_ProcName, oPara));

                if ((deletedpatid != -1))
                {
                    if ((hashtbl.Contains(deletedpatid)))
                        hashtbl.Remove(deletedpatid);
                }
            }

            catch
            {
            }
            finally
            {
                if (oDB!=null)
                {
                   
                    oDB.Dispose();
                    oDB = null/* TODO Change to default(_) if this is not a reference type */;
                }
                if (oPara != null)
                {
                    oPara.Clear();  
                }
            }
        }


        #endregion

        #region "Combo & Button Events "

        private void cmbAccountno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_DisableComboEvents == false)
                {
                    if (cmbAccountno.SelectedValue != null && Convert.ToInt64(cmbAccountno.SelectedValue) > 0)
                    {

                        Int64 nPatientToSelect = 0;
                        if (cmbAccountPatients.SelectedValue != null && Convert.ToInt64(cmbAccountPatients.SelectedValue) > 0)
                        {
                            nPatientToSelect = Convert.ToInt64(cmbAccountPatients.SelectedValue);
                        }
                        else
                        {
                            nPatientToSelect = 0;
                        }

                        
                        Int64 nSelectedPatientID = FillPatientsCombo(Convert.ToInt64(cmbAccountno.SelectedValue), nPatientToSelect);
                        _FormSelectedPatientID = nSelectedPatientID;
                        Int32 nSelectedIndex = cmbAccountno.SelectedIndex;
                        DataView dvAccount = ((DataView)cmbAccountno.DataSource);
                        if (dvAccount != null)
                        {
                            _nPAccountID = Convert.ToInt64(cmbAccountno.SelectedValue);
                            _nAccountPatientID = Convert.ToInt64(dvAccount.Table.Rows[nSelectedIndex]["nAccountPatientId"]);
                            _nGuarantorID = Convert.ToInt64(dvAccount.Table.Rows[nSelectedIndex]["nGuarantorID"]);
                            _Guarantor = dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"].ToString();


                            lblAccountNo.Text = dvAccount.Table.Rows[nSelectedIndex]["sAccount"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sAccount"].ToString();
                            lblAccountDesc.Text = dvAccount.Table.Rows[nSelectedIndex]["sAccountDesc"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sAccountDesc"].ToString();
                            lblDemoGuarantor.Text = dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"].ToString();
                            this.toolTip1.SetToolTip(lblAccountNo, dvAccount.Table.Rows[nSelectedIndex]["sAccount"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sAccount"].ToString());
                            this.toolTip1.SetToolTip(lblAccountDesc, dvAccount.Table.Rows[nSelectedIndex]["sAccountDesc"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sAccountDesc"].ToString());
                            this.toolTip1.SetToolTip(lblDemoGuarantor, dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"] == DBNull.Value ? string.Empty : dvAccount.Table.Rows[nSelectedIndex]["sGuarantorName"].ToString());
                        }
                        //dvAccount.Dispose(); 
                        
                        DisplayPatient(nSelectedPatientID);
                        
                    }
                    else
                    {
                        ClearControl("All");
                    }

                    //Call delegate
                    if (!IsCalledFromChargesOrModifyCharges)
                    {
                        if (OnAccountChanged != null)
                            OnAccountChanged(null, null);
                    }
                    else
                    {
                        if (OnCmbAccountChanged != null)
                            OnCmbAccountChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);    
            }    
        }
       
        private void cmbAccountPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (_DisableComboEvents == false)
                {
                    if (cmbAccountPatients.SelectedValue != null && Convert.ToInt64(cmbAccountPatients.SelectedValue) > 0)
                    {
                        Int64 nAccounttToSelect = 0;
                        if (cmbAccountno.SelectedValue != null && Convert.ToInt64(cmbAccountno.SelectedValue) > 0)
                        {
                            nAccounttToSelect = Convert.ToInt64(cmbAccountno.SelectedValue);
                        }
                        else
                        {
                            nAccounttToSelect = 0;
                        }

                        Int64 nSelectedAccountID = FillAccountsCombo(Convert.ToInt64(cmbAccountPatients.SelectedValue), nAccounttToSelect);

                        _nPAccountID = nSelectedAccountID;

                        _FormSelectedPatientID = Convert.ToInt64(cmbAccountPatients.SelectedValue);
                        DisplayPatient(Convert.ToInt64(cmbAccountPatients.SelectedValue));
                       
                    }
                    else
                    {

                        ClearControl("Patient");

                        DisplayPatient(0);

                        //Display Account balance when [All Acct Patient]  selected
                        DisplayBalances();

                        #region "Claim On Hold"
                        if (_ParentForm == FormName.PatientAccountView)
                        {
                            FillClaimOnHold();
                        }
                        #endregion

                    }

                    if (OnPatientChanged != null)
                        OnPatientChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }    
        }
      
        private void btnUp_Click(object sender, EventArgs e)
        {
            btnDown.Visible = true;
            btnUp.Visible = false;
            if (_IsFollowUpFeatureEnable)
            { pnlRevenueCycle.Visible = false; }
            this.Height = pnlHeader.Height;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            btnDown.Visible = false;
            btnUp.Visible = true;
            if (_IsFollowUpFeatureEnable)
            { pnlRevenueCycle.Visible = true; this.Height = pnlHeader.Height + pnlRevenueCycle.Height + 148; }
            else
            { pnlRevenueCycle.Visible = false; this.Height = pnlHeader.Height + 148; }
            
        }

        public DateOfBirthIntegers GetAge(DateTime BirthDate)
        {
            DateTime _BDate = BirthDate;
            DateOfBirthIntegers dateOfBirth = new DateOfBirthIntegers();

            // Compute the difference between BirthDate 'CODE FROM gloPM : year and end year. 
            bool IsBirthDateLeap = false;
            Int32 years = DateTime.Now.Year - BirthDate.Year;
            Int32 months = 0;
            Int32 days = 0;

            //Test if BirthDay for LeapYear.
            if (BirthDate.Day == 29 & BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }

            //Check if the last year was a full year. 
            if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);
            // Now we know BirthDate <= end and the diff between them  : is < 1 year.
            if (BirthDate.Year == DateTime.Now.Year)
            {
                months = DateTime.Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + DateTime.Now.Month;
            }
            // Check if the last month was a full month. 
            if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);
            // Now we know that BirthDate < end and is within 1 month 
            // of each other. 
            days = (DateTime.Now - BirthDate).Days;

            //To Adjust Age if BirthDate is 29th Feb in leap year
            if (IsBirthDateLeap == true)
            {
                //'Sequence of following IF code is too important.. DON'T MODIFY
                days -= 1;
                if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                {
                    days += 1;
                }
                else if (DateTime.Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 & DateTime.Now.Year % 4 != 0)
                {
                    days = 30;
                    months = months - 1;
                    if (months < 0)
                    {
                        months = 11;
                        years = years - 1;
                    }
                }
                if (months == 12)
                {
                    days = 30;
                    months = 11;
                }
            }

            //Return years & " years " & months & " months " & days & " days"
            //Following code to display age in Numeric and Text
            //Dim age As New AgeDetail
            //age.Age = years & " Years " & months & " Months " & days & " Days"
            //' Cases

            //'20081119   ''Following Code to Store ExactAge in String
            string _AgeStr = "";
            //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
            //{
            if (years == 0)
            {
                if (months == 0)
                {


                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Months " + days + " Days";
                    }
                }
            }
            else if (years == 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Days";
                    }
                }
            }
            else if (years > 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Days";
                    }
                }
            }

            dateOfBirth.DOBYears = years;
            dateOfBirth.DOBMonths = months;
            dateOfBirth.DOBDays = days;

            return dateOfBirth;
        }






        private void btn_ModityPatient_Click(object sender, EventArgs e)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DataBaseConnectionString);

            try
            {
                if (oSecurity.isPatientLock(_nPatientID, true) == false && _nPatientID > 0)
                {
                    gloPatient.frmSetupPatient ofrmSetupPatient = new gloPatient.frmSetupPatient(_nPatientID, _DataBaseConnectionString);
                    ofrmSetupPatient.ShowDialog(this);

                    //6031 HL7
                    //Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926


                    if (ofrmSetupPatient.ReturnIsClose == false)
                    {
                        if (gloGlobal.gloPMGlobal.MessageBoxCaption == "gloEMR")
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
                                                    PatientStripControl.gblnAddModPatient = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (gloGlobal.gloPMGlobal.MessageBoxCaption == "gloPM")
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
                                                    PatientStripControl.gblnAddModPatient = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (PatientStripControl.gblnAddModPatient == true)
                        {
                            PatientStripControl.InsertInMessageQueue("A08", _nPatientID, _nPatientID, _DataBaseConnectionString);
                        }
                    }
                    //if (appSettings["GenerateHL7Message"] != null && appSettings["SendPatientDetails"] != null)
                    //{
                    //    if (appSettings["GenerateHL7Message"] != "" && appSettings["SendPatientDetails"] != "")
                    //    {
                    //        if (ofrmSetupPatient.ReturnIsClose == false)
                    //        {
                    //            if ((Convert.ToBoolean(appSettings["GenerateHL7Message"]) == true) && (Convert.ToBoolean(appSettings["SendPatientDetails"]) == true))
                    //            {
                    //                PatientStripControl.InsertInMessageQueue("A08", _nPatientID, _nPatientID, _DataBaseConnectionString);
                    //            }
                    //        }
                    //    }
                    //}
                    //End of code to Modified setting name 'GenerateHL7Message' to 'SendPatientDetails' by Abhijeet on 20110926

                    ofrmSetupPatient.Dispose();

                    if (_ParentForm != FormName.ModifyCharges)
                    {
                        FillDetails(_nPatientID, _ParentForm, 1, false);
                    }
                    else
                    {
                        //ClearControl("Patient"); 
                        _nPAccountID = FillAccountsCombo(PatientID, _nPAccountID);
                        _nPatientID = FillPatientsCombo(_nPAccountID, _nPatientID);
                        _FormSelectedPatientID = _nPatientID;
                        DisplayPatient(_nPatientID);

                    }

                    if (OnPatientModified != null)
                    {
                        OnPatientModified(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }
        }

        private void btn_Alerts_Click(object sender, EventArgs e)
        {
            if (_nPatientID > 0)
            {
                try
                {
                    frmPatientAlerts ofrmPatientAlerts = new frmPatientAlerts(_DataBaseConnectionString, _nPatientID);
                    ofrmPatientAlerts.ShowDialog(this);
                    ofrmPatientAlerts.Dispose();
                    DisplayPatient(_nPatientID);  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No Patient is selected.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            //moved
            gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_DataBaseConnectionString);
            Patient oPatient = null;
            _nPAccountID = Convert.ToInt64(cmbAccountno.SelectedValue);
            try
            {
                if (_nPAccountID > 0)
                {
                    frmEditPatientAccount oFrmEditPatientAccount = null;
                    oFrmEditPatientAccount = new frmEditPatientAccount(_DataBaseConnectionString, _nPatientID, _nGuarantorID, _nPAccountID);

                    //validation added by mahesh s on 25/may/2011.
                    if (objPatient != null && _nPatientID > 0)
                        oPatient = objPatient.GetPatient(_nPatientID);

                    if (oPatient != null)
                    {
                        oFrmEditPatientAccount.PatientGuarantors = oPatient.PatientGuarantors;
                        oFrmEditPatientAccount.PatientGuardianDetails = oPatient.GuardianDetail;
                        oFrmEditPatientAccount.PatientDemographicDetails = oPatient.DemographicsDetail;
                        oFrmEditPatientAccount._ownAccount = true;
                        //oFrmEditPatientAccount.SaveButton_Click += new frmEditPatientAccount.SaveButtonClick(oFrmEditPatientAccount_SaveButton_Click);
                        oFrmEditPatientAccount.ShowDialog(this);
                        oFrmEditPatientAccount.Dispose();
                        oFrmEditPatientAccount = null;
                        _nPatientID = FillPatientsCombo(_nPAccountID, _nPatientID);
                        _FormSelectedPatientID = _nPatientID;
                        FillAccountsCombo(_nPatientID, _nPAccountID);
                        DisplayPatient(_nPatientID);
                    }
                    else
                    {
                        oFrmEditPatientAccount.Dispose();
                        oFrmEditPatientAccount = null;
                    }
                }
                else
                {
                    MessageBox.Show("No Account is selected.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (objPatient != null)
                    objPatient.Dispose();
                if (oPatient != null)
                    oPatient.Dispose();
            }
        }

        #endregion

        #region " Private Methods "
       
        private void SetView()
        {
           
            try
            {
                Boolean IsPatientAccountFeature = false;

                //To avoid DB call at design time
                if (_DataBaseConnectionString != "")
                {
                    gloAccount objAccount = new gloAccount(_DataBaseConnectionString);
                    IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
                    _IsPatientAccFearureEnabled = IsPatientAccountFeature;
                    objAccount.Dispose();
                    _isRequireBusinessCenteronAccounts = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
                }

               if (IsPatientAccountFeature)
                {
                    lblAccSearch.Visible = true;
                    txtAccountSearch.Visible = true;
                    pnlPatient.Visible = true;
                    pnlAccountDetails.Visible = true;
                }
                else
                {
                    lblAccSearch.Visible = false;
                    txtAccountSearch.Visible = false;
                    pnlPatient.Visible = false;
                    pnlAccountDetails.Visible = false;
                }

               
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
               
            }

        }

        /// <summary>
        /// Refresh All data on Patient Strip
        /// </summary>
        public void RefreshData()
        {
            _DisableComboEvents = true; 
            try
            {
                DisplayPatient(_nPatientID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);   
            }
            _DisableComboEvents = false; 
        }

        /// <summary>
        /// Refresh only Patient Charges & balances
        /// </summary>
        public void RefreshBalances()
        {
            _DisableComboEvents = true;
            try
            {
                DisplayBalances();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            _DisableComboEvents = false;
        }

        //fill form controls.
        public void FillDetails(Int64 PatientID, Int64 AccountID, FormName CallingFormName)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DataBaseConnectionString);
            bool _ShowPatient = true;

            try
            {
                ClearControl("All");

                //Set global variable. needed in case of search
                _ParentForm = CallingFormName;

                #region "Validations"

                //Addess vaidations are removed

                ////If Account Does not have Address then dont show patient 
                //if (GetAccountAddress(PatientID, 0) == false)
                //{
                //    _ShowPatient = false;
                //}

                if (_ParentForm != FormName.NewCharges
                   && _ParentForm != FormName.Appointment
                   && _ParentForm != FormName.CMS1500
                   && _ParentForm != FormName.UB04)
                {
                    //---This Method will Show Message box if Patient is Locked
                    if (oSecurity.isPatientLock(PatientID, true) == true)
                    {
                        _ShowPatient = false;
                    }
                }
                

                #endregion

                if (_ShowPatient == true)
                {
                    //Fill Accounts Combo
                    _nPAccountID = FillAccountsCombo(PatientID, AccountID);
                    if (_nPAccountID > 0)
                    {
                        //Fill Patients Combo
                        _nPatientID = FillPatientsCombo(_nPAccountID, PatientID);
                        _FormSelectedPatientID = _nPatientID;
                        //Display Selected Patients Details                        
                        DisplayPatient(_nPatientID);

                    }
                } //_ShowPatient

                //If calling form is modify charges hide the search controls
                if (_ParentForm == FormName.ModifyCharges)
                {
                    //Hide the search controls if modify charges
                    pnlSearch.Visible = false;
                }
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }

        }

        public void FillDetails(Int64 PatientID, FormName CallingFormName, Int64 nProviderid, bool blnflag)
        {
            FillDetails(PatientID, CallingFormName);
        }

        public void FillDetails(Int64 PatientID, FormName CallingFormName)
        {
            FillDetails(PatientID, 0, CallingFormName);  
        }

        public void FillDetails(Int64 PatientID, Int64 AccountID, FormName CallingFormName, Boolean RefreshBanner)
        {
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(_DataBaseConnectionString);
            bool _ShowPatient = true;

            try
            {
                //Set global variable. needed in case of search
                _ParentForm = CallingFormName;

                #region "Validations"

                //Addess vaidations are removed

                ////If Account Does not have Address then dont show patient 
                //if (GetAccountAddress(PatientID, 0) == false)
                //{
                //    _ShowPatient = false;
                //}

                if (_ParentForm != FormName.NewCharges
                   && _ParentForm != FormName.Appointment
                   && _ParentForm != FormName.CMS1500
                   && _ParentForm != FormName.UB04)
                {
                    //---This Method will Show Message box if Patient is Locked
                    if (oSecurity.isPatientLock(PatientID, true) == true)
                    {
                        _ShowPatient = false;
                    }
                }


                #endregion

                if (_ShowPatient == true)
                {
                    _nPatientID = FillPatientsCombo(AccountID, PatientID);
                    DisplayPatient(_nPatientID);
                } //_ShowPatient

                //If calling form is modify charges hide the search controls
                if (_ParentForm == FormName.ModifyCharges)
                {
                    //Hide the search controls if modify charges
                    pnlSearch.Visible = false;
                }
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSecurity != null)
                    oSecurity.Dispose();
            }

        }

        /// <summary>
        /// Fill All Patient in combo for given AccountID
        /// </summary>
        /// <param name="PatientID">AccountID</param>
        /// <param name="SelectAccountID">Select this PatientID from combo</param>
        /// <returns></returns>
        private long FillPatientsCombo(Int64 AccountID,Int64 HighlightPatientID)
        {
            _DisableComboEvents = true; 
            this.cmbAccountPatients.BeginUpdate(); 

            DataTable dtPatients = null;
            Int64 _SelectedPatientID = 0;
            try
            {

                dtPatients = PatientStripControl.GetAccountPatients(AccountID);
                if (dtPatients != null && dtPatients.Rows.Count > 0)
                {
                    ////Adding empty row.
                    //if (_ParentForm != FormName.NewCharges)
                    //{
                    //    //DataRow oNewRow = dtPatients.NewRow();
                    //    //oNewRow["PatientDisplayName"] = "All Acct. Pat. ";
                    //    //oNewRow["nPatientId"] = 0;
                    //    //dtPatients.Rows.InsertAt(oNewRow, 0);
                    //    //dtPatients.AcceptChanges();
                    //    //oNewRow = null;

                    //    chkAllAcctPat.Visible = true;
                    //}
                    //else
                    //{
                    //    chkAllAcctPat.Visible = false;
                    //}
                    chkAllAcctPat.Visible = true;

                    cmbAccountPatients.Visible = true;
                    cmbAccountPatients.DisplayMember = dtPatients.Columns["PatientDisplayName"].ColumnName;
                    cmbAccountPatients.ValueMember = dtPatients.Columns["nPatientId"].ColumnName;
                    cmbAccountPatients.DataSource = dtPatients.Copy();


                    DataRow[] drPatientToSelect = null;
                    if (HighlightPatientID > 0) //Select requisted account
                    {
                        drPatientToSelect = dtPatients.Select("nPatientID = " + HighlightPatientID + "");
                    }
                    else //select any account having address
                    {
                        drPatientToSelect = dtPatients.Select("nPatientID <> 0 ");
                    }

                    // Display Patient

                    if (drPatientToSelect != null && drPatientToSelect.Length > 0)
                    {
                        //cmbAccountPatients.SelectedIndex = 1;
                        _SelectedPatientID = drPatientToSelect[0]["nPatientID"] == DBNull.Value ? 0 : Convert.ToInt64(drPatientToSelect[0]["nPatientID"]);
                        lblDemoPatient.Text = drPatientToSelect[0]["PatientDisplayName"] == DBNull.Value ? string.Empty : drPatientToSelect[0]["PatientDisplayName"].ToString();
                        this.toolTip1.SetToolTip(lblDemoPatient, drPatientToSelect[0]["PatientDisplayName"] == DBNull.Value ? string.Empty : drPatientToSelect[0]["PatientDisplayName"].ToString());
                        cmbAccountPatients.SelectedValue = _SelectedPatientID;
                    }
                    else //Account dont have Patinets
                    {
                        _SelectedPatientID = 0;
                        lblDemoPatient.Text = "";
                        this.toolTip1.SetToolTip(lblDemoPatient, "");
                    }

                    //Account have one Patient.
                    drPatientToSelect = null;
                    drPatientToSelect = dtPatients.Select("nPatientID <> 0 ");
                    if (drPatientToSelect.Length == 1) //Show Lable
                    {
                        cmbAccountPatients.Visible = false;
                        lblDemoPatient.Visible = true;
                        chkAllAcctPat.Visible = false;
                    }
                    else //Show Account Combo
                    {
                        lblDemoPatient.Visible = false;
                        cmbAccountPatients.Visible = true;
                    }
                }
                else //Account dont have Patinets
                {
                    _SelectedPatientID = 0;
                    lblDemoPatient.Text = "";
                    this.toolTip1.SetToolTip(lblDemoPatient, "");
                }

                //Do not Allow to change the Patient
                if (_ParentForm == FormName.ModifyCharges)
                {
                    cmbAccountPatients.Visible = false;
                    lblDemoPatient.Visible = true;
                    //chkAllAcctPat.Visible = false;
                }



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (dtPatients != null) { dtPatients.Dispose(); }

                _DisableComboEvents = false; 
            }
            this.cmbAccountPatients.EndUpdate(); 
            return _SelectedPatientID;  
        }

        /// <summary>
        /// Fill All Accounts in combo for given PatientID
        /// </summary>
        /// <param name="PatientID">PatientID</param>
        /// <param name="SelectAccountID">Select this AccountID from combo</param>
        /// <returns></returns>
        private Int64 FillAccountsCombo(Int64 PatientID, Int64 HighlightAccountID)
        {
            _DisableComboEvents = true; 
            this.cmbAccountno.BeginUpdate();  

            Int64 _nAccountID = 0;
            DataView dvAccounts = null;
            DataTable dtAcct = null;
            try
            {
                if (PatientID > 0)
                {
                    //Get All Acounts for Patient 
                    dtAcct = PatientStripControl.GetPatientAccounts(PatientID);

                    dvAccounts = dtAcct.DefaultView;

                    if (dvAccounts != null && dvAccounts.Table.Rows.Count > 0)
                    {
                        //if (_ParentForm != FormName.ModifyCharges)
                        //{
                        //    dvAccounts.RowFilter = "Status = 'Active'";  
                        //}

                        cmbAccountno.Visible = true;
                        cmbAccountno.DisplayMember = "sAccount";
                        cmbAccountno.ValueMember = "nPAccountID";
                        cmbAccountno.DataSource = dvAccounts;


                        DataRow[] drAccountToSelect = null;
                        if (HighlightAccountID > 0) //Select requisted account
                        {
                            drAccountToSelect = dtAcct.Select("nPAccountID = " + HighlightAccountID + "");
                        }
                        else //select any account having address
                        {
                            drAccountToSelect = dtAcct.Select("IsAccountAddressAvailable = 1 or IsAccountAddressAvailable = 0");
                        }

                        if (drAccountToSelect != null && drAccountToSelect.Length > 0)//This code for Initial formLoad. combo should show address availbe Account only.
                        {
                            _nAccountID = Convert.ToInt64(drAccountToSelect[0]["nPAccountId"]);
                            _nAccountPatientID = Convert.ToInt64(drAccountToSelect[0]["nAccountPatientId"]);
                            _nGuarantorID = Convert.ToInt64(drAccountToSelect[0]["nGuarantorID"]);
                            _Guarantor = Convert.ToString(drAccountToSelect[0]["sGuarantorName"]); 


                            cmbAccountno.SelectedValue = _nAccountID;
                            lblAccountNo.Text = drAccountToSelect[0]["sAccount"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sAccount"].ToString();
                            lblAccountDesc.Text = drAccountToSelect[0]["sAccountDesc"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sAccountDesc"].ToString();
                            lblDemoGuarantor.Text = drAccountToSelect[0]["sGuarantorName"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sGuarantorName"].ToString();
                            this.toolTip1.SetToolTip(lblAccountNo, drAccountToSelect[0]["sAccount"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sAccount"].ToString());
                            this.toolTip1.SetToolTip(lblAccountDesc, drAccountToSelect[0]["sAccountDesc"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sAccountDesc"].ToString());
                            this.toolTip1.SetToolTip(lblDemoGuarantor, drAccountToSelect[0]["sGuarantorName"] == DBNull.Value ? string.Empty : drAccountToSelect[0]["sGuarantorName"].ToString());
                        }
                        else
                        {
                            _nAccountID = 0;
                            _nAccountPatientID = 0;
                            _nGuarantorID = 0;
                            _Guarantor = ""; 

                            cmbAccountno.SelectedIndex  = -1;
                            lblAccountNo.Text = "";
                            lblAccountDesc.Text = "";
                            lblDemoGuarantor.Text = "";
                            this.toolTip1.SetToolTip(lblAccountNo, "");
                            this.toolTip1.SetToolTip(lblAccountDesc, "");
                            this.toolTip1.SetToolTip(lblDemoGuarantor, "");
                        }

                        drAccountToSelect = null; 

                        //Patient have one Account show lable
                        if (dtAcct.Rows.Count == 1)
                        {
                            cmbAccountno.Visible = false;
                            lblAccountNo.Visible = true;
                            
                        }
                        else
                        {
                            lblAccountNo.Visible = false;
                            cmbAccountno.Visible = true;
                        }
                       
                    }
                    else
                    {
                        _nAccountID = 0;
                        _nAccountPatientID = 0;
                        _nGuarantorID = 0;
                        _Guarantor = ""; 
                        lblAccountNo.Text = "";
                        lblAccountDesc.Text = "";
                        lblDemoGuarantor.Text = "";
                        this.toolTip1.SetToolTip(lblAccountNo, "");
                        this.toolTip1.SetToolTip(lblAccountDesc, "");
                        this.toolTip1.SetToolTip(lblDemoGuarantor, "");
                        MessageBox.Show("Patient is not associated with any Account.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }

                    //Do not Allow to change the account
                    //if (_ParentForm == FormName.ModifyCharges)
                    //{
                        //cmbAccountno.Visible = false;
                        //lblAccountNo.Visible = true;
                    //}

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
               // if (dvAccounts != null) { dvAccounts.Dispose(); }
                _DisableComboEvents = false; 
            }
            this.cmbAccountno.EndUpdate(); 
            return _nAccountID;
        }

        Dictionary<string, string> sortedDict = null;
        public Dictionary<string, string> DishtblMedcatClr;
        private global::System.Globalization.CultureInfo resourceCulture;
        [System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        private void FillMedicalCategoryHashTable()
        {
            if ((DishtblMedcatClr == null))
            {
                DishtblMedcatClr = new Dictionary<string, string>();
                DishtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown");
                DishtblMedcatClr.Add("MedicalCategoryImages_2_Blue_TopSkyBlue", "MedicalCategoryImages_2_Blue_BottomSkyBlue");
                DishtblMedcatClr.Add("MedicalCategoryImages_3_Gray_TopGray", "MedicalCategoryImages_3_Gray_BottomGray");
                DishtblMedcatClr.Add("MedicalCategoryImages_4_GreenTopLightGreen", "MedicalCategoryImages_4_Green_BottomLightGreen");
                DishtblMedcatClr.Add("MedicalCategoryImages_5_TopOrange", "MedicalCategoryImages_5_BottomOrange");

                DishtblMedcatClr.Add("MedicalCategoryImages_6_Pink_TopPink", "MedicalCategoryImages_6_Pink_BottomPink");
                DishtblMedcatClr.Add("MedicalCategoryImages_7_Red_TopRed", "MedicalCategoryImages_7_Red_BottomRed");
                DishtblMedcatClr.Add("MedicalCategoryImages_8_Violet_TopViolet", "MedicalCategoryImages_8_Violet_BottomViolet");
                DishtblMedcatClr.Add("MedicalCategoryImages_9_Yellow_TopYellow", "MedicalCategoryImages_9_Yellow_BottomYellow");
                DishtblMedcatClr.Add("MedicalCategoryImages_91_TopDark_Blue", "MedicalCategoryImages_91_BottomDark_Blue");
                //   DishtblMedcatClr.  
                sortedDict = (from entry in DishtblMedcatClr orderby entry.Key ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

        //private void SetLabelColorForDarkBlue(bool blndrkblue)
        //{
        //    if (blndrkblue == true)
        //    {
        //        lblPatientName.ForeColor = Color.White;
        //        lblGender.ForeColor = Color.White;
        //        lblGenderCaption.ForeColor = Color.White;
        //        lblDOB.ForeColor = Color.White;
        //        lblDOBCaption.ForeColor = Color.White;
        //        lblPatientCode.ForeColor = Color.White;
        //        lblPatientCodeCaption.ForeColor = Color.White;
        //        //lblBrdHdrTop.ForeColor = Color.White;
        //        //lblBrdHdrBottom.ForeColor = Color.White;
        //        label40.ForeColor = Color.White;
        //        lblBalOtherReserve.ForeColor = Color.White;
        //        lblBalAdvancedReserve.ForeColor = Color.White;
        //        lblBalPatientDue.ForeColor = Color.White;
        //        lblBalOtherReserveCaption.ForeColor = Color.White;
        //        lblBalInsurancePending.ForeColor = Color.White;
        //        lblBalCopayReserve.ForeColor = Color.White;
        //        lblBalPatientDueCaption.ForeColor = Color.White;
        //        lblBalAdvancedReserveCaption.ForeColor = Color.White;
        //        lblBalTotalBalance.ForeColor = Color.White;
        //        lblBalCopayReserveCaption.ForeColor = Color.White;
        //        lblBalInsurancePendingCaption.ForeColor = Color.White;
        //        lblBalTotalBalanceCaption.ForeColor = Color.White;
        //        txtPatientSearchCaption.ForeColor = Color.White;
        //        lblAccSearch.ForeColor = Color.White;
        //        lblDemoPatientCaption.ForeColor = Color.White;
        //        lblAccountDesc.ForeColor = Color.White;
        //        label5.ForeColor = Color.White;
        //        lblDemoAccountDesc.ForeColor = Color.White;
        //        lblAccountNoCaption.ForeColor = Color.White;
        //        lblNotesCaption.ForeColor = Color.White;
        //        lblDemoCopay.ForeColor = Color.White;
        //        lblDemoProvider.ForeColor = Color.White;
        //        lblAlerts.ForeColor = Color.White;
        //        lblDemoCopayCaption.ForeColor = Color.White;
        //        lblDemoProviderCaption.ForeColor = Color.White;
        //        lblDemoGuarantor.ForeColor = Color.White;
        //        lblDemoPatientPayment.ForeColor = Color.White;
        //        lblDemoGuarantorCaption.ForeColor = Color.White;
        //        lblAlertsCap.ForeColor = Color.White;
        //        lblDemoLastPatPayment.ForeColor = Color.White;
        //        lblNotes.ForeColor = Color.White;
        //        lblAccountNo.ForeColor = Color.White;
        //        lblDemoPatient.ForeColor = Color.White;
        //        lblStatementNote.ForeColor = Color.White;
        //        lblClaimOnHold.ForeColor = Color.White;
        //        FollowingClaimCaption.ForeColor = Color.White;
        //        lblClaimOnHoldBottom.ForeColor = Color.White;
        //        lblClaimOnHoldTop.ForeColor = Color.White;
        //        lblClaimOnHoldLeft.ForeColor = Color.White;
        //        lblClaimOnHoldRight.ForeColor = Color.White;
        //        lblStmtCntCaption.ForeColor = Color.White;
        //        lblStmtCnt.ForeColor = Color.White;
        //        lblPmtPlanAmtCaption.ForeColor = Color.White;
        //        lblPmtPlanAmt.ForeColor = Color.White;
        //        lblAcctFollowUpCaption.ForeColor = Color.White;
        //        lblAcctFollowUp.ForeColor = Color.White;
        //        lblAcctNote.ForeColor = Color.White;
        //        lblAcctNoteCaption.ForeColor = Color.White;
        //        //lblBrdMainRight.ForeColor = Color.White;
        //       // lblBrdMainLeft.ForeColor = Color.White;
        //        lblMedCat.ForeColor = Color.White;
        //        lblMedCatCaption.ForeColor = Color.White;
        //       // lblBrdMainBottom.ForeColor = Color.White;
        //    }
        //    else
        //    {
        //        lblPatientName.ForeColor = Color.Black;
        //        lblGender.ForeColor = Color.Black;
        //        lblGenderCaption.ForeColor = Color.Black;
        //        lblDOB.ForeColor = Color.Black;
        //        lblDOBCaption.ForeColor = Color.Black;
        //        lblPatientCode.ForeColor = Color.Black;
        //        lblPatientCodeCaption.ForeColor = Color.Black;
        //        //lblBrdHdrTop.ForeColor = Color.Black;
        //        //lblBrdHdrBottom.ForeColor = Color.Black;
        //        label40.ForeColor = Color.Black;
        //        lblBalOtherReserve.ForeColor = Color.Black;
        //        lblBalAdvancedReserve.ForeColor = Color.Black;
        //        lblBalPatientDue.ForeColor = Color.Black;
        //        lblBalOtherReserveCaption.ForeColor = Color.Black;
        //        lblBalInsurancePending.ForeColor = Color.Black;
        //        lblBalCopayReserve.ForeColor = Color.Black;
        //        lblBalPatientDueCaption.ForeColor = Color.Black;
        //        lblBalAdvancedReserveCaption.ForeColor = Color.Black;
        //        lblBalTotalBalance.ForeColor = Color.Black;
        //        lblBalCopayReserveCaption.ForeColor = Color.Black;
        //        lblBalInsurancePendingCaption.ForeColor = Color.Black;
        //        lblBalTotalBalanceCaption.ForeColor = Color.Black;
        //        txtPatientSearchCaption.ForeColor = Color.Black;
        //        lblAccSearch.ForeColor = Color.Black;
        //        lblDemoPatientCaption.ForeColor = Color.Black;
        //        lblAccountDesc.ForeColor = Color.Black;
        //        label5.ForeColor = Color.Black;
        //        lblDemoAccountDesc.ForeColor = Color.Black;
        //        lblAccountNoCaption.ForeColor = Color.Black;
        //        lblNotesCaption.ForeColor = Color.Black;
        //        lblDemoCopay.ForeColor = Color.Black;
        //        lblDemoProvider.ForeColor = Color.Black;
        //        lblAlerts.ForeColor = Color.Black;
        //        lblDemoCopayCaption.ForeColor = Color.Black;
        //        lblDemoProviderCaption.ForeColor = Color.Black;
        //        lblDemoGuarantor.ForeColor = Color.Black;
        //        lblDemoPatientPayment.ForeColor = Color.Black;
        //        lblDemoGuarantorCaption.ForeColor = Color.Black;
        //        lblAlertsCap.ForeColor = Color.Black;
        //        lblDemoLastPatPayment.ForeColor = Color.Black;
        //        lblNotes.ForeColor = Color.Black;
        //        lblAccountNo.ForeColor = Color.Black;
        //        lblDemoPatient.ForeColor = Color.Black;
        //        lblStatementNote.ForeColor = Color.Black;
        //        lblClaimOnHold.ForeColor = Color.Black;
        //        FollowingClaimCaption.ForeColor = Color.Black;
        //        lblClaimOnHoldBottom.ForeColor = Color.Black;
        //        lblClaimOnHoldTop.ForeColor = Color.Black;
        //        lblClaimOnHoldLeft.ForeColor = Color.Black;
        //        lblClaimOnHoldRight.ForeColor = Color.Black;
        //        lblStmtCntCaption.ForeColor = Color.Black;
        //        lblStmtCnt.ForeColor = Color.Black;
        //        lblPmtPlanAmtCaption.ForeColor = Color.Black;
        //        lblPmtPlanAmt.ForeColor = Color.Black;
        //        lblAcctFollowUpCaption.ForeColor = Color.Black;
        //        lblAcctFollowUp.ForeColor = Color.Black;
        //        lblAcctNote.ForeColor = Color.Black;
        //        lblAcctNoteCaption.ForeColor = Color.Black;
        //        //lblBrdMainRight.ForeColor = Color.Black;
        //        //lblBrdMainLeft.ForeColor = Color.Black;
        //        lblMedCat.ForeColor = Color.Black;
        //        lblMedCatCaption.ForeColor = Color.Black;
        //       // lblBrdMainBottom.ForeColor = Color.Black;
        //    }
        //}
        private static ResourceSet resourceSet = gloStrips.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
        private void GetMedicalCategoryImage(DataTable dtMedCat = null)
        {

            DataSet DS = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtMedColor = null;
            string strcolor = "";
            string strborderColor = string.Empty;
            string strbottompanelcolr = string.Empty;
            try
            {
                var _with1 = oDB;
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@PatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oPara, out dtMedColor);
                oDB.Disconnect();
                if ((dtMedColor != null))
                {
                    if ((dtMedColor.Rows.Count > 0))
                    {
                        strcolor = Convert.ToString(dtMedColor.Rows[0]["ImageColor"]);
                        strborderColor = Convert.ToString(dtMedColor.Rows[0]["BorderColor"]);
                        strbottompanelcolr = Convert.ToString(dtMedColor.Rows[0]["BottomPanelColor"]);
                    }
                }


                if ((!string.IsNullOrEmpty(strcolor.Trim())))
                {
                    foreach (KeyValuePair<string, string> di in sortedDict)
                    {
                        if ((di.Key.ToString().Contains(strcolor.Trim().Replace(" ", "_"))))
                        {
                           
                            pnlHeader.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Key));
                            pnlMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                         
                            //pnlRight.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            //pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            //pnlAccount.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString(di.Value));
                            lblBrdMainBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblBrdMainLeft.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblBrdMainRight.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblBrdHdrTop.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblBrdHdrBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblClaimOnHoldBottom.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblClaimOnHoldLeft.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblClaimOnHoldTop.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                            lblClaimOnHoldRight.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor);
                             
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                else
                {
                    //ResourceSet resourceSet = gloStrips.Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    pnlHeader.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"));
                    pnlMain.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                  //pnlRight.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                  //  pnlLeft.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                  //  pnlAccount.BackgroundImage = (Image)resourceSet.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"));
                    lblBrdMainBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblBrdMainLeft.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblBrdMainRight.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblBrdHdrBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblBrdHdrTop.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblClaimOnHoldBottom.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblClaimOnHoldLeft.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblClaimOnHoldTop.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                    lblClaimOnHoldRight.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF8B8E7B");
                }

                if ((strcolor.Contains("Pink") || strcolor.Contains("Red") || strcolor.Contains("Violet") || strcolor.Contains("Dark")))
                {
                    lblAccSearch.ForeColor = Color.White;
                    lblPatientCode.ForeColor = Color.White;
                    lblPatientCodeCaption.ForeColor = Color.White;
                    lblGender.ForeColor = Color.White;
                    lblGenderCaption.ForeColor = Color.White;
                    lblDOB.ForeColor = Color.White;
                    lblDOBCaption.ForeColor = Color.White;
                    lblPatientName.ForeColor = Color.White;
                    txtPatientSearchCaption.ForeColor = Color.White;
                }
                else
                {
                    lblAccSearch.ForeColor = Color.Black;
                    lblPatientCode.ForeColor = Color.Black;
                    lblPatientCodeCaption.ForeColor = Color.Black;
                    lblGender.ForeColor = Color.Black;
                    lblGenderCaption.ForeColor = Color.Black;
                    lblDOB.ForeColor = Color.Black;
                    lblDOBCaption.ForeColor = Color.Black;
                    lblPatientName.ForeColor = Color.Black; 
                    txtPatientSearchCaption.ForeColor = Color.Black;
                }

              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((dtMedColor != null))
                {
                    dtMedColor.Dispose();
                    dtMedColor = null;
                }
                if ((dtMedCat != null))
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                if ((DS != null))
                {
                    DS.Tables.Clear();
                    DS.Dispose();
                    DS = null;
                }
            }


        }

        private void DisplayPatient(Int64 PatientID)
        {
            this.SuspendLayout();
            
            DataSet dtSet = null;
            DataTable dtInitialdemographic = null;
            DataTable dtAlerts = null;
            DataTable dtPatientNotes = null;
            DataTable dtStatementNotes = null;
            DataTable dtCopay = null;
            DataTable dtPmtPlan = null;
            DataTable dtAccountNotes = null;
            DataTable dtAccountFollowUp = null;
            bool IsFollowUpEnable = false;
            DataSet dsAccountFollowUp = null;
            Int64 nStatementCount = 0;
            
            //your code here

           
            try
            {

                //Display Selected Patients Details
                if (PatientID > 0)
                {
                    if (gloPatient.frmSetupPatient.hshPatData == null)
                    {
                        gloPatient.frmSetupPatient.hshPatData = new Hashtable(); 
                    }
                    Object obj = gloPatient.frmSetupPatient.hshPatData[_nPatientID];
                    if (obj == null)
                    {
                        gloPatient.frmSetupPatient.hshPatData.Add(_nPatientID, gloGlobal.gloPMGlobal.UserID);
                        AddrecentPatient(_nPatientID, gloGlobal.gloPMGlobal.UserID, ref gloPatient.frmSetupPatient.hshPatData);
                    }
                    dtSet = PatientStripControl.GetPatientDemographicInformation(PatientID);  //Collect all the information in Data Set

                    if (dtSet.Tables.Count > 0)
                    {
                        dtInitialdemographic = dtSet.Tables[0];   //Table 0 for patient name ,DOB,patientCode,Provider,Gender
                        dtAlerts = dtSet.Tables[1];   // Table 1 nAlertID, sAlertName
                        dtPatientNotes = dtSet.Tables[2];  // Table 2 Note
                        dtStatementNotes = dtSet.Tables[3];   // Table 3 nFromDate, nToDate,sStatementNote 
                        dtCopay = dtSet.Tables[4]; //Table 4 To Fetch Copay
                    }

                    //Assign patient name ,DOB,patientCode,Provider,Gender to Variables
                    #region  "Patient Demographics"



                    //decimal nExpectedCopay = 0;

                    if (dtInitialdemographic != null && dtInitialdemographic.Rows.Count > 0)
                    {
                        _nPatientID = PatientID;
                        _PatientName = dtInitialdemographic.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientName"].ToString();
                        _PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();
                        _DateOfBirth = dtInitialdemographic.Rows[0]["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtInitialdemographic.Rows[0]["DOB"]);
                        _Gender = dtInitialdemographic.Rows[0]["Gender"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Gender"].ToString();
                        //_PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();

                        _nProviderID = dtInitialdemographic.Rows[0]["nProviderID"] == DBNull.Value ? 0 : Convert.ToInt64(dtInitialdemographic.Rows[0]["nProviderID"]);
                        _ProviderName = dtInitialdemographic.Rows[0]["Provider"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Provider"].ToString();
                        //nExpectedCopay = dtInitialdemographic.Rows[0]["ExpectedCopay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInitialdemographic.Rows[0]["ExpectedCopay"].ToString());
                        _PatientsMaritalStatus = dtInitialdemographic.Rows[0]["Provider"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["MaritalStatus"].ToString();
                        _PatientMedicalCategory = dtInitialdemographic.Rows[0]["MedicalCategory"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["MedicalCategory"].ToString();
                        _NextAppointment = dtInitialdemographic.Rows[0]["NextAppointment"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["NextAppointment"].ToString();
                        _EMRAlert = dtInitialdemographic.Rows[0]["EMRAlerts"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["EMRAlerts"].ToString();
                        _BirthTime = dtInitialdemographic.Rows[0]["sBirthTime"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["sBirthTime"].ToString();
                        
                        if (_patientPhoto != null)
                        {
                            _patientPhoto.Dispose();
                            _patientPhoto = null;
                        }
                        if (dtInitialdemographic.Rows[0]["PatientPhoto"] != DBNull.Value)
                        {
                            Byte[] patPicture = (Byte[])dtInitialdemographic.Rows[0]["PatientPhoto"];
                            try
                            {
                                _patientPhoto = byteArrayToImage(patPicture);
                            }
                            catch //(Exception ex)
                            {
                                _patientPhoto = null;
                            }
                            patPicture = null;
                        }
                        else
                        {
                            _patientPhoto = null;
                        }
                    }
                    else
                    {
                        _nPatientID = 0;
                        _PatientName = "";
                        _PatientCode = "";
                        _DateOfBirth = DateTime.MinValue;
                        _Gender = "";
                        _PatientCode = "";
                        _nProviderID = 0;
                        _ProviderName = "";
                        //nExpectedCopay = 0;
                        _patientPhoto = null;
                        _PatientMedicalCategory = "";
                        _BirthTime = "";
                    }

                    lblPatientName.Text = _PatientName;
                   // lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";

                    AgeDetail oAgeDetails = new AgeDetail();
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + Convert.ToString(oAgeDetails.CalculateAge_New(_DateOfBirth,_BirthTime)) + ")";


                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;
                    lblDemoProvider.Text = _ProviderName;
                    if (_PatientMedicalCategory == "")
                    {
                        lblMedCat.Visible = false;
                        lblMedCatCaption.Visible = false;
                        lblMedCat.Text = "";
                        pnlMedCategory.Visible = false;
                    }
                    else
                    {
                        lblMedCat.Visible = true;
                        lblMedCatCaption.Visible = true;
                        lblMedCat.Text = _PatientMedicalCategory;
                        pnlMedCategory.Visible = true;
                    }

                    if (PatientStripControl.ShowEMRAlertsOnPatientBanner())
                    {
                        if (_EMRAlert != "")
                        {
                            lblDemoEMRAlerts.Visible = true;
                            lblDemogloEMRAlertsCaption.Visible = true;
                            lblDemoEMRAlerts.ForeColor = Color.FromArgb(231, 15, 71);
                            //lblDemogloEMRAlertsCaption.ForeColor = Color.FromArgb(231, 15, 71);
                        }
                        else
                        {
                            lblDemoEMRAlerts.Visible = false;
                        }
                    }
                    else
                    {
                        lblDemoEMRAlerts.Visible = false;
                        lblDemogloEMRAlertsCaption.Visible = false;
                    }

                    lblDemoNextApptCaption.Text = _NextAppointment;
                    lblDemoEMRAlerts.Text = _EMRAlert;
                    this.toolTip1.SetToolTip(lblDemoNextApptCaption, _NextAppointment);
                    this.toolTip1.SetToolTip(lblDemoEMRAlerts, _EMRAlert);

                    this.toolTip1.SetToolTip(lblPatientName, _PatientName);
                    this.toolTip1.SetToolTip(lblDemoProvider, _ProviderName);
                    this.toolTip1.SetToolTip(lblMedCat, _PatientMedicalCategory);


                    if (dtCopay != null && dtCopay.Rows.Count > 0)
                    {
                        StringBuilder _sCopayAmount = new StringBuilder();
                        StringBuilder _sCopayDesc = new StringBuilder();

                        if (dtCopay.Rows.Count > 1)
                        {
                            for (int i = 0; i <= dtCopay.Rows.Count - 1; i++)
                            {
                                if ((dtCopay.Rows[i]["nCopay"] != DBNull.Value))
                                {
                                    if (i == dtCopay.Rows.Count - 1)
                                    {
                                        _sCopayAmount.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceFlag"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "]");
                                        _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "]");
                                    }
                                    else
                                    {
                                        _sCopayAmount.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceFlag"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "], ");
                                        _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[i]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[i]["nCopay"]).ToString("#0.00") + "], ");
                                    }
                                }
                            }
                        }
                        else
                        {
                            _sCopayAmount.Append(" $ " + Convert.ToDecimal(dtCopay.Rows[0]["nCopay"]).ToString("#0.00"));
                            _sCopayDesc.Append(Convert.ToString(dtCopay.Rows[0]["sInsuranceName"]) + " [$ " + Convert.ToDecimal(dtCopay.Rows[0]["nCopay"]).ToString("#0.00")+ "]");
                        }
                        if (_sCopayAmount.Length > 35)
                        {
                            lblDemoCopay.Text = _sCopayAmount.ToString().Substring(0, 30) + " . . . ";
                        }
                        else
                        {
                            lblDemoCopay.Text = _sCopayAmount.ToString();
                        }
                        lblDemoCopay.Tag = _sCopayDesc.ToString();
                        toolTip1.SetToolTip(lblDemoCopay, lblDemoCopay.Tag.ToString());
                    }
                    else
                    {
                        lblDemoCopay.Text = "$ 0.00";
                        lblDemoCopay.Tag = "";
                    }




                    if (_patientPhoto != null)
                    {
                        picPAPhoto.Image = _patientPhoto;
                        picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else //Default photo will show.
                    {
                        if (_Gender.ToLower() == "male")
                        {
                            this.picPAPhoto.Image = global::gloStrips.Properties.Resources.MalePatient;
                        }
                        else if (_Gender.ToLower() == "female")
                        {
                            this.picPAPhoto.Image = global::gloStrips.Properties.Resources.FemalePatient;
                        }
                        else
                        {
                            this.picPAPhoto.Image = null;
                        }
                        picPAPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                     FillMedicalCategoryHashTable();
                     GetMedicalCategoryImage();
                    #endregion "Patient Demographics"

                    #region "Alerts"

                    if (dtAlerts != null && dtAlerts.Rows.Count > 0)
                    {
                        //if ((dtAlerts.Rows[0]["sAlertName"] != DBNull.Value)
                        //    && (dtAlerts.Rows[0]["sAlertName"].ToString().Length > 45))
                        //{
                        //    lblAlerts.Text = dtAlerts.Rows[0]["sAlertName"].ToString().Substring(0, 35) + " . . .  ";
                        //}
                        //else
                        //{
                            lblAlerts.Text = dtAlerts.Rows[0]["sAlertName"] == DBNull.Value ? string.Empty : dtAlerts.Rows[0]["sAlertName"].ToString();
                        //}
                        this.toolTip1.SetToolTip(lblAlerts, dtAlerts.Rows[0]["sAlertName"].ToString());


                        if (dtAlerts.Rows.Count > 1)
                            lblAlertsCap.Text = "PM Alerts (" + dtAlerts.Rows.Count + ") :";
                        else
                            lblAlertsCap.Text = "PM Alerts :";
                    }
                    else
                    {
                        lblAlerts.Text = "";
                        this.toolTip1.SetToolTip(lblAlerts, "");
                        lblAlertsCap.Text = "PM Alerts :";
                    }


                    #endregion

                    #region "Patient Notes"

                    if (dtPatientNotes != null && dtPatientNotes.Rows.Count > 0)
                    {
                        //if ((dtPatientNotes.Rows[0]["Note"] != DBNull.Value)
                        //    && (dtPatientNotes.Rows[0]["Note"].ToString().Length > 35))
                        //{
                        //    lblNotes.Text = dtPatientNotes.Rows[0]["Note"].ToString().Substring(0, 28) + " . . . ";
                        //}
                        //else
                        //{
                        //    lblNotes.Text = dtPatientNotes.Rows[0]["Note"] == DBNull.Value ? string.Empty : dtPatientNotes.Rows[0]["Note"].ToString();
                        //}

                        if (dtPatientNotes.Rows[0]["Note"] != DBNull.Value)
                        {
                            lblNotes.Text = dtPatientNotes.Rows[0]["Note"] == DBNull.Value ? string.Empty : dtPatientNotes.Rows[0]["Note"].ToString();
                            this.toolTip1.SetToolTip(lblNotes, dtPatientNotes.Rows[0]["Note"].ToString());
                        }

                    }


                    #endregion

                    #region "Statement Note"

                    if (dtStatementNotes != null && dtStatementNotes.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtStatementNotes.Rows[0]["sStatementNote"]) != "")
                        {
                            String sFromDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtStatementNotes.Rows[0]["nFromDate"])).ToString("MM/dd/yy");
                            String sToDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtStatementNotes.Rows[0]["nToDate"])).ToString("MM/dd/yy");
                            lblStatementNote.Text = "Statement Note" + " [" + sFromDate + " - " + sToDate + "] " + Environment.NewLine + dtStatementNotes.Rows[0]["sStatementNote"].ToString();
                            toolTip1.SetToolTip(lblStatementNote, SplitToolTip(dtStatementNotes.Rows[0]["sStatementNote"].ToString()));
                        }
                        else
                        {
                            lblStatementNote.Text = "";
                            toolTip1.SetToolTip(lblStatementNote, "");
                        }
                    }
                    else
                    {
                        lblStatementNote.Text = "";
                        toolTip1.SetToolTip(lblStatementNote, "");
                    }

                    #endregion  "Statement Note"

                    #region "Statement Count"
                    nStatementCount = PatientStripControl.GetAccountStatementCount(_nPAccountID);

                    if (nStatementCount > 0)
                    {
                            pnlStmntCt.Visible = true;
                            lblStmtCnt.Text = Convert.ToString(nStatementCount.ToString());
                            toolTip1.SetToolTip(lblStmtCnt, SplitToolTip(Convert.ToString(nStatementCount.ToString())));
                    }
                    else
                    {
                        pnlStmntCt.Visible = false;
                        lblStmtCnt.Text = "";
                        toolTip1.SetToolTip(lblStmtCnt, "");
                    }

                    #endregion  "Statement Count"

                    IsFollowUpEnable = PatientStripControl.IsFollowUpEnable();

                    if (IsFollowUpEnable)
                    {
                        
                        dsAccountFollowUp = PatientStripControl.GetAccountFollowUp(_nPAccountID);

                        if (dsAccountFollowUp.Tables.Count > 0)
                        {
                            dtAccountNotes = dsAccountFollowUp.Tables[0];
                            dtPmtPlan = dsAccountFollowUp.Tables[1];
                            if (dtPmtPlan.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtPmtPlan.Rows[0]["dPlanAmount"]).Trim() == string.Empty)
                                {
                                    dtPmtPlan.Rows.Clear();
                                }
                            }
                            dtAccountFollowUp = dsAccountFollowUp.Tables[2];
                            if (dtAccountNotes.Rows.Count > 0 || dtPmtPlan.Rows.Count > 0 || dtAccountFollowUp.Rows.Count > 0)
                            { _IsFollowUpFeatureEnable = true; pnlRevenueCycle.Visible = true; this.Height = 194; pnlRevenueCycle.Width = this.Width - 2; }
                            else { 
                                this.Height = pnlHeader.Height + pnlMain.Height; pnlRevenueCycle.Visible = false;
                                this.Height = 174; pnlRevenueCycle.Width = 1220; _IsFollowUpFeatureEnable = false;
                            }
                        }

                        #region "Account Notes"

                        if (dtAccountNotes != null && dtAccountNotes.Rows.Count > 0)
                        {
                            if ((dtAccountNotes.Rows[0]["sNoteDescription"] != DBNull.Value)
                                && (dtAccountNotes.Rows[0]["sNoteDescription"].ToString() != ""))
                            {
                                pnlAccNote.Visible = true;
                                lblAcctNote.Visible = true;
                                lblAcctNote.Text = dtAccountNotes.Rows[0]["sNoteDescription"].ToString();
                            }
                            else
                            {
                                pnlAccNote.Visible = false;
                                lblAcctNote.Text = dtAccountNotes.Rows[0]["sNoteDescription"] == DBNull.Value ? string.Empty : dtAccountNotes.Rows[0]["sNoteDescription"].ToString();
                            }
                            this.toolTip1.SetToolTip(lblAcctNote, dtAccountNotes.Rows[0]["sNoteDescription"].ToString());
                        }
                        else
                        {
                            pnlAccNote.Visible = false;
                            lblAcctNote.Text = "";
                            toolTip1.SetToolTip(lblAcctNote, "");
                        }

                        #endregion "Account Notes"

                        #region "Payment Plan"

                        if (dtPmtPlan != null && dtPmtPlan.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtPmtPlan.Rows[0]["dPlanAmount"]) != "")
                            {
                                //Panel p = (Panel)pnlRevenueCycle.FindForm("Panel2");
                                //p.Visible = false;
                                pnlPmtPlan.Visible = true;
                                lblPmtPlanAmt.Text = "$ " + dtPmtPlan.Rows[0]["dPlanAmount"].ToString();
                                toolTip1.SetToolTip(lblPmtPlanAmt, SplitToolTip(dtPmtPlan.Rows[0]["dPlanAmount"].ToString()));
                            }
                            else
                            {
                                pnlPmtPlan.Visible = false;
                                lblPmtPlanAmt.Text = "";
                                toolTip1.SetToolTip(lblPmtPlanAmt, "");
                            }
                        }
                        else
                        {
                            pnlPmtPlan.Visible = false;
                            lblPmtPlanAmt.Text = "";
                            toolTip1.SetToolTip(lblPmtPlanAmt, "");
                        }
                        #endregion  "Payment Plan"

                        #region "Follow-up Description"

                        if (dtAccountFollowUp != null && dtAccountFollowUp.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtAccountFollowUp.Rows[0]["sFollowupDescription"]) != "")
                            {
                                pnlAcctFollowUp.Visible = true;
                                lblAcctFollowUp.Text = dtAccountFollowUp.Rows[0]["sFollowupDescription"].ToString();
                                toolTip1.SetToolTip(lblAcctFollowUp, SplitToolTip(dtAccountFollowUp.Rows[0]["sFollowupDescription"].ToString()));
                                if (Convert.ToDateTime(dtAccountFollowUp.Rows[0]["dtAcctFollowUpDate"].ToString()) <= DateTime.Now)
                                {
                                    lblAcctFollowUp.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblAcctFollowUp.Font, FontStyle.Bold);
                                    lblAcctFollowUp.ForeColor = System.Drawing.Color.Maroon;
                                }
                                else
                                {
                                    lblAcctFollowUp.ForeColor = System.Drawing.Color.Black;
                                }
                            }
                            else
                            {
                                pnlAcctFollowUp.Visible = false;
                                lblAcctFollowUp.Text = "";
                                toolTip1.SetToolTip(lblAcctFollowUp, "");
                            }
                        }
                        else
                        {
                            pnlAcctFollowUp.Visible = false;
                            lblAcctFollowUp.Text = "";
                            toolTip1.SetToolTip(lblAcctFollowUp, "");
                        }
                        #endregion  "Follow-up Description"
                    }
                    else
                    {
                        this.Height = 174; pnlRevenueCycle.Width = 1220;
                        pnlRevenueCycle.Visible = false;
                        _IsFollowUpFeatureEnable = false;
                    }
                    //if (pnlAcctFollowUp.Visible == false && pnlAccNote.Visible == false && pnlPmtPlan.Visible == false)
                    //{
                    //    this.Height = 174; pnlRevenueCycle.Width = 1220;
                    //    pnlRevenueCycle.Visible = false;
                    //    _IsFollowUpFeatureEnable = false;
                    //}
                    lblBalTotalBalance.Text = "$ 0.00";
                    lblBalInsurancePending.Text = "$ 0.00";
                    lblBalPatientDue.Text = "$ 0.00";
                    lblBalBadDebt.Text = "$ 0.00";
                    lblBalCopayReserve.Text = "$ 0.00";
                    lblBalAdvancedReserve.Text = "$ 0.00";
                    lblBalOtherReserve.Text = "$ 0.00";
                                       
                    DisplayBalances();
                                       
                    btnUp.Visible = true;
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        pnlBadDebt.Visible = true;
                    }
                    else
                    {
                        pnlBadDebt.Visible = false;
                    }

                }
                else
                {
                    dtSet = PatientStripControl.GetPatientDemographicInformation(_FormSelectedPatientID);  //Collect all the information in Data Set

                    if (dtSet.Tables.Count > 0)
                    {
                        dtInitialdemographic = dtSet.Tables[0];   //Table 0 for patient name ,DOB,patientCode,Provider,Gender
                   }

                    //Assign patient name ,DOB,patientCode,Provider,Gender to Variables
                    #region  "Patient Demographics"

                    if (dtInitialdemographic != null && dtInitialdemographic.Rows.Count > 0)
                    {
                        _PatientName = dtInitialdemographic.Rows[0]["PatientName"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientName"].ToString();
                        _PatientCode = dtInitialdemographic.Rows[0]["PatientCode"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["PatientCode"].ToString();
                        _DateOfBirth = dtInitialdemographic.Rows[0]["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtInitialdemographic.Rows[0]["DOB"]);
                        _Gender = dtInitialdemographic.Rows[0]["Gender"] == DBNull.Value ? string.Empty : dtInitialdemographic.Rows[0]["Gender"].ToString();

                    }
                    else
                    {
                        _nPatientID = 0;
                        _PatientName = "";
                        _PatientCode = "";
                        _DateOfBirth = DateTime.MinValue;
                        _Gender = "";
                        _PatientCode = "";
              
                    }

                    lblPatientName.Text = _PatientName;
                    lblDOB.Text = Convert.ToString(_DateOfBirth.ToString("MM/dd/yyyy")) + "(" + CalculateAge(_DateOfBirth) + "y)";
                    lblGender.Text = _Gender;
                    lblPatientCode.Text = _PatientCode;

                    #endregion "Patient Demographics"
 
                }

                #region "Claim On Hold"
                if (_ParentForm == FormName.PatientAccountView)
                {
                    FillClaimOnHold();
                }
                #endregion

            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
               
                if (dtInitialdemographic != null)
                    dtInitialdemographic.Dispose();
                if (dtPatientNotes != null)
                    dtPatientNotes.Dispose();
                if (dtStatementNotes != null)
                    dtStatementNotes.Dispose();
                if (dtAlerts != null)
                    dtAlerts.Dispose();
                if (dtSet != null)
                {
                    dtSet.Dispose();
                    dtSet = null;
                }
                if (dsAccountFollowUp != null)
                {
                    dsAccountFollowUp.Dispose();
                    dsAccountFollowUp = null;
                }
                this.ResumeLayout(true);
            }
            
        }
     
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

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            //SLR: Added a static function in gloPictureBox
            return gloPictureBox.gloImage.GetImage(byteArrayIn, true);
            //gloPictureBox.gloPictureBox myPicBx = new gloPictureBox.gloPictureBox();
            //System.Drawing.Image _ResultImage = null;
            //try
            //{
            //    myPicBx.byteImage = byteArrayIn;
            //    _ResultImage = (Bitmap)myPicBx.copyFrame(true);
            //}
            //catch (Exception)
            //{

            //}
            //finally
            //{
            //    myPicBx.Dispose();
            //    myPicBx = null;

            //}
            //return _ResultImage;
        }

        //added By Mahesh Satlapalli (Apollo) for Design Patients Grid. 
        private void DesignPatientGrid()
        {
            try
            {
                c1PatientDetails.Rows.Count = 1;
                c1PatientDetails.Rows.Fixed = 1;
                c1PatientDetails.Cols.Count = 10;
                c1PatientDetails.Cols.Fixed = 0;

                c1PatientDetails.SetData(0, COL_PAT_ID, "Id");
                c1PatientDetails.SetData(0, COL_PAT_Code, "Code");
                c1PatientDetails.SetData(0, COL_PAT_FirstName, "First Name");
                c1PatientDetails.SetData(0, COL_PAT_MI, "MI");
                c1PatientDetails.SetData(0, COL_PAT_LastName, "Last Name");
                c1PatientDetails.SetData(0, COL_PAT_SSN, "SSN");
                c1PatientDetails.SetData(0, COL_PAT_Provider, "Provider");
                c1PatientDetails.SetData(0, COL_PAT_DOB, "DOB");
                c1PatientDetails.SetData(0, COL_PAT_Phone, "Phone");
                c1PatientDetails.SetData(0, COL_PAT_Mobile, "Mobile");

                c1PatientDetails.Cols[COL_PAT_ID].Visible = false;

                Int32 _width = (this.Width - 20) / 10;

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
                gloC1FlexStyle.Style(c1PatientDetails, false);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        //added by Mahesh Satlapalli (Apollo) : Design Account Grid.
        private void DesignAccountGrid()
        {
            try
            {
                c1FlexAccountGrid.Rows.Count = 1;
                c1FlexAccountGrid.Rows.Fixed = 1;
                c1FlexAccountGrid.Cols.Count = 12;
                c1FlexAccountGrid.Cols.Fixed = 0;

                c1FlexAccountGrid.SetData(0, COL_Act_IsAddressAvailble, "AddressAvailable");
                c1FlexAccountGrid.SetData(0, COL_Act_PAT_ID, "PatientID");
                c1FlexAccountGrid.SetData(0, COL_Act_ID, "Acct.ID");
                c1FlexAccountGrid.SetData(0, COL_Act_No, "Acct.#");
                c1FlexAccountGrid.SetData(0, COL_Act_GuarantorName, "Guarantor");
                c1FlexAccountGrid.SetData(0, COL_Act_Name, "Acct. Desc.");
                c1FlexAccountGrid.SetData(0, COL_Act_Address1, "Address1");
                c1FlexAccountGrid.SetData(0, COL_Act_Address2, "Address2");
                c1FlexAccountGrid.SetData(0, COL_Act_City, "City");
                c1FlexAccountGrid.SetData(0, COL_Act_State, "State");
                c1FlexAccountGrid.SetData(0, COL_Act_Zip, "Zip");
                c1FlexAccountGrid.SetData(0, COL_Act_BusinessCenter, "BUS");

                c1FlexAccountGrid.Cols[COL_Act_IsAddressAvailble].Visible = false;
                c1FlexAccountGrid.Cols[COL_Act_PAT_ID].Visible = false;
                c1FlexAccountGrid.Cols[COL_Act_ID].Visible = false;

                c1FlexAccountGrid.Cols[COL_Act_No].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_GuarantorName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_BusinessCenter].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_Address1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_Address2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_City].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_State].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FlexAccountGrid.Cols[COL_Act_Zip].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                Int32 _width=0;
                if (_isRequireBusinessCenteronAccounts)
                {

                    _width = (this.Width - 20) / 11;
                    c1FlexAccountGrid.Cols[COL_Act_BusinessCenter].Visible = true;
                    c1FlexAccountGrid.Cols[COL_Act_BusinessCenter].Width = Convert.ToInt32(_width * 1);
                }
                else
                {
                    _width = (this.Width - 20) / 10;
                    c1FlexAccountGrid.Cols[COL_Act_BusinessCenter].Visible = false;
                    c1FlexAccountGrid.Cols[COL_Act_BusinessCenter].Width = 0;
                }

                c1FlexAccountGrid.Cols[COL_Act_No].Width = _width * 1;
                c1FlexAccountGrid.Cols[COL_Act_Name].Width = Convert.ToInt32(_width * 1.5);
                c1FlexAccountGrid.Cols[COL_Act_GuarantorName].Width = Convert.ToInt32(_width * 1.5);
                c1FlexAccountGrid.Cols[COL_Act_Address1].Width = Convert.ToInt32(_width * 1.5);
                c1FlexAccountGrid.Cols[COL_Act_Address2].Width = Convert.ToInt32(_width * 1.5);
                c1FlexAccountGrid.Cols[COL_Act_City].Width = Convert.ToInt32(_width * 1);
                c1FlexAccountGrid.Cols[COL_Act_State].Width = Convert.ToInt32(_width * 0.5);
                c1FlexAccountGrid.Cols[COL_Act_Zip].Width = Convert.ToInt32(_width * 0.5);

                gloC1FlexStyle.Style(c1FlexAccountGrid, false);
                c1FlexAccountGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

     
        private void DisplayBalances()
        {
            
            decimal dLastPatPayment = 0;
            decimal dTotalBalAmt = 0;
            decimal dTotalInsPending = 0;
            decimal dTotalPatientDue = 0;
            decimal dTotalcopayReserve = 0;
            decimal dTotalAdvancedReserve = 0;
            decimal dTotalOtherReserve = 0;
            decimal dTotalBadDebtDue = 0;
            string sLastPatPaymentDate = "";

            DataSet dtSet = null;
            DataTable dtInsuranceDetails = null;
            DataTable dtReserveDetails = null;
          //  pnlBadDebt.Visible = true;
            try
            {
                if (chkAllAcctPat.Checked)
                {
                    dtSet = PatientStripControl.GetAccountBalances(PAccountID);
                }
                else
                {
                    dtSet = PatientStripControl.GetPatientBalances(_nPatientID, PAccountID);
                }

                //if (_nPatientID > 0)
                //    dtSet = PatientStripControl.GetPatientBalances(_nPatientID, PAccountID);
                //else
                //    dtSet = PatientStripControl.GetAccountBalances(PAccountID);


                dtInsuranceDetails = dtSet.Tables[0];
                dtReserveDetails = dtSet.Tables[1];

                if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
                {
                    //dTotalBalAmt = dtInsuranceDetails.Rows[0]["TotalBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["TotalBalance"]);
                    dTotalInsPending = dtInsuranceDetails.Rows[0]["InsuranceDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["InsuranceDue"]);
                    dTotalPatientDue = dtInsuranceDetails.Rows[0]["PatientDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientDue"]);
                    dTotalBadDebtDue = dtInsuranceDetails.Rows[0]["BadDebtDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["BadDebtDue"]);
                    dLastPatPayment = dtInsuranceDetails.Rows[0]["PatientLastPay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientLastPay"]);
                    sLastPatPaymentDate = dtInsuranceDetails.Rows[0]["LastPayDate"] == DBNull.Value ? "" : Convert.ToDateTime(dtInsuranceDetails.Rows[0]["LastPayDate"]).ToString("MM/dd/yyyy");
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        dTotalBalAmt = dTotalInsPending + dTotalPatientDue + dTotalBadDebtDue;
                        pnlBadDebt.Visible = true;

                        gloSecurity.gloSecurity gloSecurity = new gloSecurity.gloSecurity(_DataBaseConnectionString);
                        if (gloSecurity.isBadDebtPatient(this.PatientID))
                        { lblBadDebtStatusII.Visible = true; }
                        else
                        { lblBadDebtStatusII.Visible = false; }

                        if (gloSecurity != null)
                        {
                            gloSecurity.Dispose();
                            gloSecurity = null;
                        }

                        if (dTotalBadDebtDue > 0)
                        {
                            lblBadDebtSatus.Visible = true;                            
                        }
                        else
                        {
                            lblBadDebtSatus.Visible = false ;                            
                        }
                    }
                    else
                    {
                        pnlBadDebt.Visible = false;
                        dTotalBalAmt = dTotalInsPending + dTotalPatientDue;
                    }
                }
                //Assign Copay Reserve,AdvancedResere,OtherReserve to Varialbles
                if (dtReserveDetails != null && dtReserveDetails.Rows.Count > 0)
                {
                    foreach (DataRow drReserveDetails in dtReserveDetails.Rows)
                    {
                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 2)   //For Copay Reserve
                        {
                            dTotalcopayReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }

                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 3)  //ForAdvanced Reserve
                        {
                            dTotalAdvancedReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }
                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 4) //For OtherReserve
                        {
                            dTotalOtherReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }
                    }
                }
                dTotalBalAmt = dTotalBalAmt - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);
                dTotalPatientDue = dTotalPatientDue - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);

                lblBalTotalBalance.Text = "$ " + dTotalBalAmt.ToString("#0.00");
                lblBalInsurancePending.Text = "$ " + dTotalInsPending.ToString("#0.00");
                lblBalPatientDue.Text = "$ " + dTotalPatientDue.ToString("#0.00");
                lblBalBadDebt.Text = "$ " + dTotalBadDebtDue.ToString("#0.00");
                lblBalCopayReserve.Text = "$ " + dTotalcopayReserve.ToString("#0.00");
                lblBalAdvancedReserve.Text = "$ " + dTotalAdvancedReserve.ToString("#0.00");
                lblBalOtherReserve.Text = "$ " + dTotalOtherReserve.ToString("#0.00");
                if (sLastPatPaymentDate != "")
                { lblDemoLastPatPayment.Text = "$ " + dLastPatPayment.ToString("#0.00") + " [" + sLastPatPaymentDate + "]"; }
                else { lblDemoLastPatPayment.Text = "$ " + dLastPatPayment.ToString("#0.00"); }  
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (dtInsuranceDetails != null)
                    dtInsuranceDetails.Dispose();
                if(dtReserveDetails!=null)
                    dtReserveDetails.Dispose();
                if (dtSet != null)
                {
                    dtSet.Dispose();
                    dtSet = null;
                }
            }
        }

        private Int32 CalculateAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            Int32 years = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                --years;
            return years;
        }

        private string FormatAge(DateTime BirthDate)
        {
            DateTime _BDate = BirthDate;
            
            // Compute the difference between BirthDate 'CODE FROM gloPM : year and end year. 
            bool IsBirthDateLeap = false;
            Int32 years = DateTime.Now.Year - BirthDate.Year;
            Int32 months = 0;
            Int32 days = 0;
           
            //Test if BirthDay for LeapYear.
            if (BirthDate.Day == 29 & BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }
           
            //Check if the last year was a full year. 
            if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);
            // Now we know BirthDate <= end and the diff between them  : is < 1 year.
            if (BirthDate.Year == DateTime.Now.Year)
            {
                months = DateTime.Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + DateTime.Now.Month;
            }
            // Check if the last month was a full month. 
            if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);
            // Now we know that BirthDate < end and is within 1 month 
            // of each other. 
            days = (DateTime.Now - BirthDate).Days;

            //To Adjust Age if BirthDate is 29th Feb in leap year
            if (IsBirthDateLeap == true)
            {
                //'Sequence of following IF code is too important.. DON'T MODIFY
                days -= 1;
                if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                {
                    days += 1;
                }
                else if (DateTime.Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 & DateTime.Now.Year % 4 != 0)
                {
                    days = 30;
                    months = months - 1;
                    if (months < 0)
                    {
                        months = 11;
                        years = years - 1;
                    }
                }
                if (months == 12)
                {
                    days = 30;
                    months = 11;
                }
            }

            //Return years & " years " & months & " months " & days & " days"
            //Following code to display age in Numeric and Text
            //Dim age As New AgeDetail
            //age.Age = years & " Years " & months & " Months " & days & " Days"
            //' Cases

            //'20081119   ''Following Code to Store ExactAge in String
            string _AgeStr = "";
            //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
            //{
            if (years == 0)
            {
                if (months == 0)
                {


                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = months + " Months " + days + " Days";
                    }
                }
            }
            else if (years == 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Year " + months + " Months " + days + " Days";
                    }
                }
            }
            else if (years > 1)
            {
                if (months == 0)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + days + " Days";
                    }
                }
                else if (months == 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Month";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Month " + days + " Days";
                    }
                }
                else if (months > 1)
                {
                    if (days == 0)
                    {
                        _AgeStr = years + " Years " + months + " Months";
                    }
                    else if (days == 1)
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Day";
                    }
                    else
                    {
                        _AgeStr = years + " Years " + months + " Months " + days + " Days";
                    }
                }
            }

            return _AgeStr;
        }

        //account address validation
        private bool GetAccountAddress(Int64 nPatientId_AccAddr, Int64 PAccountID_AccAddr)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            bool IsAddressAvialble=false;
            string sqlStr = "";

            try
            {
                if (PAccountID_AccAddr > 0)
                    sqlStr = "Select (Case When (len(ltrim(rtrim(sAddressLine1))) <> 0 and LEN(ltrim(rtrim(sCity))) <> 0 and LEN(ltrim(rtrim( sState))) <> 0 and  LEN(ltrim(rtrim(sZip))) <> 0) then 1 else 0 end) as IsAccountAddressAvailable "
                                   + " From PA_Accounts where nPAccountID = " + PAccountID_AccAddr;
                else
                    sqlStr = "Select count(IsAccountAddressAvailable) from (Select (Case When (len(ltrim(rtrim(sAddressLine1))) <> 0 and LEN(ltrim(rtrim(sCity))) <> 0 and LEN(ltrim(rtrim( sState))) <> 0 and  LEN(ltrim(rtrim(sZip))) <> 0) then 1 else 0 end) as IsAccountAddressAvailable "
                                + " From PA_Accounts where nPAccountID in ("
                                + " Select nPAccountID from PA_Accounts_Patients where nPatientID = " + nPatientId_AccAddr + "))a where IsAccountAddressAvailable=1";

                object result = oDB.ExecuteScalar_Query(sqlStr);

                if (Convert.ToInt64(result) > 0)
                    IsAddressAvialble = true;
                else
                    IsAddressAvialble = false;

                if (!IsAddressAvialble && nPatientId_AccAddr != 0)
                    _nPatientID = 0;

                if (!IsAddressAvialble && nPatientId_AccAddr == 0)
                    PAccountID = 0;

                if (!IsAddressAvialble)
                {
                    MessageBox.Show("Patient Account doesn't have the address.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return IsAddressAvialble;
        }

        //clear all controls
        private void ClearControl(String Type)
        {
            _DisableComboEvents = true; 

            try
            {

                switch (Type)
                {
                    case "All" :

                        #region Clear All
                        //c1PatientDetails.Visible = false;
                        _nPatientID = 0;
                        _PatientCode = "";
                        _PatientName = "";
                         _nProviderID = 0;
                        _ProviderName = "";
                        _Gender = "";
                        _Guarantor = "";
                        if (_patientPhoto != null)
                        {
                            _patientPhoto.Dispose();
                            _patientPhoto = null;
                        }


                       
                        //Clear Patient Details on control
                        cmbAccountPatients.DataSource = null;
                        cmbAccountPatients.Items.Clear();
                        lblPatientName.Text = "";
                        lblPatientCode.Text = "";
                        lblDemoPatient.Text = "";
                        lblDOB.Text = "";
                        lblGender.Text = "";
                        lblDOB.Text = "";
                        lblGender.Text = "";
                        lblPatientCode.Text = "";
                        lblPatientName.Text = "";
                        lblAlerts.Text = "";
                        lblNotes.Text = "";
                        lblDemoCopay.Text = "$ 0.00";
                        picPAPhoto.Image = null;
                        lblDemoEMRAlerts.Text = "";
                        lblDemoNextApptCaption.Text = "";
                       
                        //Clear Balance Details on control
                        lblBalTotalBalance.Text = "$ 0.00";
                        lblBalInsurancePending.Text = "$ 0.00";
                        lblBalPatientDue.Text = "$ 0.00";
                        lblBalBadDebt.Text = "$ 0.00";
                        lblBalCopayReserve.Text = "$ 0.00";
                        lblBalAdvancedReserve.Text = "$ 0.00";
                        lblBalOtherReserve.Text = "$ 0.00";

                        //Clear Account Details on control
                        _nPAccountID = 0;
                        _nGuarantorID = 0;
                        _nAccountPatientID = 0;
                      
                        cmbAccountno.DataSource = null;
                        cmbAccountno.Items.Clear();
                        lblAccountDesc.Text = "";
                        lblDemoGuarantor.Text = "";
                        lblAccountNo.Text = "";
                        lblDemoProvider.Text = "";
                        lblDemoLastPatPayment.Text = "$ 0.00";
                        if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                        {
                            pnlBadDebt.Visible = true;
                        }
                        else
                        {
                            pnlBadDebt.Visible = false;
                        }

                        
                        #endregion

                        break;
                    case "Patient":

                        #region Patient
                        //c1PatientDetails.Visible = false;
                        _nPatientID = 0;
                        _PatientCode = "";
                        _PatientName = "";
                         _nProviderID = 0;
                        _ProviderName = "";
                        _Gender = "";
                        if (_patientPhoto != null)
                        {
                            _patientPhoto.Dispose();
                            _patientPhoto = null;
                        }


                        if (_FormSelectedPatientID > 0)
                        {
                            lblGender.Text = "";
                            lblDemoProvider.Text = "";
                            lblAlerts.Text = "";
                            lblNotes.Text = "";
                            lblDemoCopay.Text = "$ 0.00";
                            picPAPhoto.Image = null;
                        }
                        else
                        {
                            //DO not Clear the Combo datasource. Other Patients list needs to stay in combo 
                            //cmbAccountPatients.Text = ""; 
                            lblPatientName.Text = "";
                            lblPatientCode.Text = "";
                            lblDOB.Text = "";
                            lblGender.Text = "";
                            lblDOB.Text = "";
                            lblGender.Text = "";
                            lblDemoProvider.Text = "";
                            lblAlerts.Text = "";
                            lblNotes.Text = "";
                            lblDemoCopay.Text = "$ 0.00";
                            picPAPhoto.Image = null;
                            lblDemoEMRAlerts.Text = "";
                            lblDemoNextApptCaption.Text = "";
                          
                        }
                        #endregion

                        break;
                    case "Balance":

                        #region Balance

                        //Clear Balance Details on control
                        lblBalTotalBalance.Text = "$ 0.00";
                        lblBalInsurancePending.Text = "$ 0.00";
                        lblBalPatientDue.Text = "$ 0.00";
                        lblBalBadDebt.Text = "$ 0.00";
                        lblBalCopayReserve.Text = "$ 0.00";
                        lblBalAdvancedReserve.Text = "$ 0.00";
                        lblBalOtherReserve.Text = "$ 0.00";

                        //Clear Account Details on control
                        lblAccountDesc.Text = "";
                        lblDemoGuarantor.Text = "";
                        lblDemoProvider.Text = "";
                        lblDemoLastPatPayment.Text = "$ 0.00";
                        if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                        {
                            pnlBadDebt.Visible = true;
                        }
                        else
                        {
                            pnlBadDebt.Visible = false;
                        }
                        #endregion

                        break;
                    case "Account":

                        #region Account
                        _nPAccountID = 0;
                        _nGuarantorID = 0;
                        _nAccountPatientID = 0;
                        //Clear Account Details on control
                        lblAccountDesc.Text = "";
                        lblAccountNo.Text = "";
                        lblDemoGuarantor.Text = "";
                        lblDemoProvider.Text = "";
                        lblDemoLastPatPayment.Text = "$ 0.00";
                       
                        cmbAccountno.DataSource = null;
                        cmbAccountno.Items.Clear();
                        #endregion

                        break;
                    default:
                        break;
                } 

              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _DisableComboEvents = false; 
            }
        }



        #endregion

        #region "Search"
		
        private void txtAccountSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            isBlankPatientSearch = false;
            if (e.KeyChar == 46)
            {
                //e.Handled =false;
            }
            if (e.KeyChar == 13)
            {
                if (txtAccountSearch.Text.Trim() != "")
                {
                    InstringAccountSearch(txtAccountSearch.Text.Trim());
                    txtPatientSearch.Clear();

                    //If Single Account found from search then only call AccountChangeEvent Delegete
                    if (c1FlexAccountGrid.Visible == false)
                    {
                        if (_nPAccountID != 0)
                        {
                            if (OnAccountChanged != null)
                                OnAccountChanged(null, null);
                        }
                    }
                   
                }
                else
                {
                    pnlMiddle.Visible = true;
                    pnlLeft.Visible = true;
                    pnlRight.Visible = true;
                    pnlAccount.Visible = false;
                    c1PatientDetails.Visible = false;
                    isBlankPatientSearch = true;
                }
            }
        }

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            isBlankPatientSearch = false;
            if (e.KeyChar == 46)
            {
                //e.Handled =false;
            }
            if (e.KeyChar == 13)
            {
                if (txtPatientSearch.Text.Trim() != "")
                {
                    InstringSearch(txtPatientSearch.Text.Trim());
                    txtAccountSearch.Clear();

                    //If Single Patient found from search then only call PatientChangeEvent Delegete
                    if (c1PatientDetails.Visible == false)
                    {
                        if (OnPatientChanged != null)
                            OnPatientChanged(null, null);
                    }
                }
                else
                {
  
                    pnlMiddle.Visible = true;
                    pnlLeft.Visible = true;
                    pnlRight.Visible = true;
                    pnlAccount.Visible = false;
                    c1PatientDetails.Visible = false;
                    isBlankPatientSearch = true;
                    //To alow the focus on CPT when we Hit Enter
                    if (_ParentForm == FormName.NewCharges)
                    {
                        if (OnPatientChanged != null)
                            OnPatientChanged(null, null);
                    }
                }
            }
        }
               
        private void InstringSearch(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dtPatients = null;
            String strSQL = "";
            

            try
            {
                byte nPatientCodeColumnNo = 1;
                byte nPatientFirstNameColumnNo = 2;
                byte nPatientMiddleNameColumnNo = 3;
                byte nPatientLastNameColumnNo = 4;
                byte nPatientDOBColumnNo = 8;
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

                        strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                        + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                        + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                        + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                        + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                        " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' OR " +
                        " CONVERT(VARCHAR,Patient.dtdob,101) LIKE '%" + strSearch + "%'  ";

                        oDB.Retrive_Query(strSQL, out dtPatients);
                        if (dtPatients != null)
                        {
                            DataView dvPatient = dtPatients.DefaultView;

                            // dtPatients.Dispose();

                            if (dvPatient.Count > 0)
                            {
                                IsPatientExists = true;
                                if (dvPatient.Count == 1)
                                {
                                    c1PatientDetails.Visible = false;

                                    _nPatientID = Convert.ToInt64(dvPatient[0]["nPatientID"]);

                                    _FormSelectedPatientID = _nPatientID;
                                    //fill selected patient details
                                    FillDetails(_nPatientID, _ParentForm);

                                    pnlMiddle.Visible = true;
                                    pnlLeft.Visible = true;
                                    pnlRight.Visible = true;

                                    pnlAccount.Visible = false;

                                    txtPatientSearch.Clear();
                                    txtAccountSearch.Clear();
                                }
                                else
                                {
                                    #region "Show Multiple Patients"

                                    DesignPatientGrid();
                                    for (Int32 _PatientCount = 0; _PatientCount <= dvPatient.Count - 1; _PatientCount++)
                                    {
                                        c1PatientDetails.Rows.Add();
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvPatient[_PatientCount]["nPatientID"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvPatient[_PatientCount]["PatientCode"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvPatient[_PatientCount]["FirstName"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvPatient[_PatientCount]["MiddleName"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvPatient[_PatientCount]["LastName"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvPatient[_PatientCount]["SSN"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvPatient[_PatientCount]["sProviderName"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvPatient[_PatientCount]["DOB"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvPatient[_PatientCount]["Phone"].ToString());
                                        c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvPatient[_PatientCount]["Mobile"].ToString());

                                    }
                                    pnlMiddle.Visible = false;
                                    pnlLeft.Visible = false;
                                    pnlRight.Visible = false;
                                    c1FlexAccountGrid.Visible = false;
                                    c1PatientDetails.Visible = true;
                                    pnlMain.BringToFront();
                                    c1PatientDetails.BringToFront();
                                    c1PatientDetails.Select(1, 0);
                                    c1PatientDetails.Focus();

                                    if (btnDown.Visible == true)
                                    {
                                        btnDown_Click(null, null);
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                //_nPatientID = 0;
                                //_nPAccountID = 0;
                                //ClearControl("All"); 

                                MessageBox.Show("Patient does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                IsPatientExists = false;
                                txtPatientSearch.Focus();
                                txtPatientSearch.SelectionStart = 0;
                                txtPatientSearch.SelectionLength = txtPatientSearch.Text.Length;
                            }
                            dvPatient.Dispose();
                            dvPatient = null;
                            dtPatients.Dispose();
                            dtPatients = null;
                        }
                    }
                    else
                    {
                        DataTable dtTemp = null;
                        DataView dvNext = null;
                        for (Int32 _ArrayLength = 0; _ArrayLength < strSearchArray.Length; _ArrayLength++)
                        {
                            strSearch = strSearchArray[_ArrayLength];
                            
                            if (strSearch.Trim() != "")
                            {
                                if (_ArrayLength == 0)
                                {
                                    strSQL = "SELECT Patient.nPatientID, ISNULL(Patient.sPatientCode, '') AS PatientCode, ISNULL(Patient.sFirstName, '') AS FirstName, ISNULL(Patient.sMiddleName, '') AS MiddleName, ISNULL(Patient.sLastName, '') AS LastName, "
                                  + " ISNULL(Patient.sPhone,'') AS Phone,ISNULL(Patient.sMobile,'') AS Mobile, ISNULL(Patient.nSSN,'') AS SSN,  "
                                  + " Convert(varchar,Patient.dtDOB,101) AS DOB, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) +  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName "
                                  + " FROM Patient INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID "
                                  + " WHERE ISNULL(Patient.nClinicID,0)  = " + _ClinicID + " AND ISNULL(Patient.sPatientCode, '') LIKE '" + strSearch + "%' OR " +
                                  " ISNULL(Patient.sFirstName, '') LIKE '" + strSearch + "%' OR " +
                                  " ISNULL(Patient.sMiddleName, '') LIKE '" + strSearch + "%' OR " +
                                  " ISNULL(Patient.sLastName, '') LIKE '" + strSearch + "%' OR " +
                                  " CONVERT(VARCHAR,Patient.dtdob,101) LIKE '%" + strSearch + "%'  ";

                                    oDB.Retrive_Query(strSQL, out dtPatients);
                                    if (dtTemp != null)
                                    {
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }
                                    if (dtPatients != null)
                                    {
                                        IsPatientExists = true;
                                        DataView dvPatient = dtPatients.DefaultView;

                                        if (dvPatient != null && dvPatient.Table != null)
                                        {
                                            dtTemp = dvPatient.ToTable();
                                        }
                                        else
                                        {
                                            dtTemp = new DataTable();
                                        }
                                        dtPatients.Dispose();
                                        dtPatients = null;
                                        if (dvPatient != null)
                                        {
                                            dvPatient.Dispose();
                                            dvPatient = null;
                                        }
                                    }
                                    else
                                    {
                                        dtTemp = new DataTable();
                                    }
                                    if (dvNext != null)
                                    {
                                        dvNext.Dispose();
                                        dvNext = null;
                                    }
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    if (dtTemp != null)
                                    {
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }
                                    if (dvNext != null)
                                    {
                                        dtTemp = dvNext.ToTable();
                                        dvNext.Dispose();
                                        dvNext = null;
                                    }
                                    else
                                    {
                                        dtTemp = new DataTable();
                                    }
                                    dvNext = dtTemp.DefaultView;
                                }
                                if (dvNext != null && dvNext.Table != null && dvNext.Table.Columns.Count > 0)
                                    dvNext.RowFilter = dvNext.Table.Columns[nPatientCodeColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nPatientFirstNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nPatientMiddleNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nPatientLastNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nPatientDOBColumnNo].ColumnName + " Like '%" + strSearch + "%' ";
                            }
                        }
                        if (dvNext.Count > 0)
                        {
                            if (dvNext.Count == 1)
                            {
                                pnlMiddle.Visible = true;
                                c1PatientDetails.Visible = false;
                                pnlLeft.Visible = true;
                                pnlRight.Visible = true;
                                pnlMain.BringToFront();

                                _nPatientID = Convert.ToInt64(dvNext[0]["nPatientID"]);
                                _FormSelectedPatientID = _nPatientID;
                                //fill selected patient details
                                FillDetails(_nPatientID, _ParentForm);

                                txtPatientSearch.Clear();
                                txtAccountSearch.Clear();
                            }
                            else
                            {
                                #region "Show Multiple Patients"

                                DesignPatientGrid();

                                for (Int32 _Count = 0; _Count <= dvNext.Count - 1; _Count++)
                                {
                                    c1PatientDetails.Rows.Add();
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_ID, dvNext[_Count]["nPatientID"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Code, dvNext[_Count]["PatientCode"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_FirstName, dvNext[_Count]["FirstName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_MI, dvNext[_Count]["MiddleName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_LastName, dvNext[_Count]["LastName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_SSN, dvNext[_Count]["SSN"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Provider, dvNext[_Count]["sProviderName"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_DOB, dvNext[_Count]["DOB"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Phone, dvNext[_Count]["Phone"].ToString());
                                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_PAT_Mobile, dvNext[_Count]["Mobile"].ToString());
                                }

                                pnlMiddle.Visible = false;
                                pnlLeft.Visible = false;
                                pnlRight.Visible = false;
                                c1FlexAccountGrid.Visible = false;
                                c1PatientDetails.Visible = true;
                                pnlMain.BringToFront();
                                c1PatientDetails.BringToFront();
                                c1PatientDetails.Select(1, 0);
                                c1PatientDetails.Focus();
                                if (btnDown.Visible == true)
                                {
                                    btnDown_Click(null, null);
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            //added by mahesh s on 23/may/2011 for if patient search word have ',' it is throwing error.
                            //_nPatientID = 0;
                            //_nPAccountID = 0;
                            //ClearControl("All"); 

                            MessageBox.Show("Patient does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            IsPatientExists = false;
                            txtPatientSearch.Focus();
                            txtPatientSearch.SelectionStart = 0;
                            txtPatientSearch.SelectionLength = txtPatientSearch.Text.Length;
                        }
                        if (dvNext != null)
                        {
                            dvNext.Dispose();
                            dvNext = null;
                        }
                        if (dtTemp != null)
                        {
                            dtTemp.Dispose();
                            dtTemp = null;
                        }
                    }
                  
                }
                else
                {
                    //added by mahesh s on 23/may/2011 for if patient search word have ',' it is throwing error.
                    _nPatientID = 0;
                    _FormSelectedPatientID = 0;
                    _nPAccountID = 0;
                    ClearControl("All");
                    txtPatientSearch.Focus();
                    txtPatientSearch.SelectionStart = 0;
                    txtPatientSearch.SelectionLength = txtPatientSearch.Text.Length;
                }
            }
            catch (System.OverflowException e)
            {
                MessageBox.Show("Claim number is invalid please enter a valid claim number.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                //if (dvPatient != null) { dvPatient.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SearchPatient(string SearchText)
        {
            if (SearchText.Trim() != "")
            {
                InstringSearch(SearchText.Trim());
                txtAccountSearch.Clear();

                //If Single Patient found from search then only call PatientChangeEvent Delegete
                if (c1PatientDetails.Visible == false)
                {
                    if (OnPatientChanged != null)
                        OnPatientChanged(null, null);
                }
            }
        }

        //added By Mahesh S(Apollo) : For Search and View Accounts on Grid based on given text in Account Serach TextBox. The Search will work on AccountNo and AccountDesc.
        private void InstringAccountSearch(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dtAccounts = null;
            String strSQL = "";

            try
            {
                byte nAccountNoColumnNo = 3;
                byte nAccountDescColumnNo = 4;
                byte nGuarantorFirstNameColumnNo = 12;
                byte nGuarantorLastNameColumnNo = 13;

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
                        strSearch = strSearchArray[0].Trim();

                        strSQL = "SELECT (Case When (len(ltrim(rtrim(PA.sAddressLine1))) <> 0 and LEN(ltrim(rtrim(PA.sCity))) <> 0 and LEN(ltrim(rtrim( PA.sState))) <> 0 " +
                                       " and  LEN(ltrim(rtrim(PA.sZip))) <> 0) then '1' else '0' end) IsAccountAddressAvailable,  " +
                                       " nPatientID, PA.nPAccountID , PA.sAccountNo , PA.sAccountDesc ,   " +
                                       " ISNULL(POC.sFirstName,'') + SPACE(1) + ISNULL(POC.sLastName,'') as  GuarantorName, " +
                                       " (Select top 1 isnull(PAAP.nPatientID, 0) from PA_Accounts_Patients PAAP  " +
                                                   " where PA.nPAccountID = PAAP.nPAccountID and bIsOwnAccount = 1) as nPatientID,  " +
                                           " PA.sAddressLine1,PA.sAddressLine2,PA.sCity,PA.sState,PA.sZip,ISNULL(BS.sBusinessCenterCode,'') AS sBusinessCenter 		 " +
                                       " FROM  PA_Accounts PA Inner join Patient_OtherContacts POC " +
                                           " On POC.nPatientContactID = PA.nGuarantorID " +
                                           "LEFT OUTER JOIN dbo.BL_BusinessCenterCodes BS ON PA.nBusinessCenterID = BS.nBusinessCenterID "+
                                       " WHERE sAccountNo Like '" + strSearch + "%' Or POC.sFirstName Like '" + strSearch + "%' or POC.sLastName Like '" + strSearch + "%' or  PA.sAccountDesc Like '" + strSearch + "%'  ORDER BY PA.sAccountNo ";

                        oDB.Retrive_Query(strSQL, out dtAccounts);

                        DataView dvAccount = dtAccounts.DefaultView;

                      //  dtAccounts.Dispose();

                        if (dvAccount.Count > 0)
                        {
                            if (dvAccount.Count == 1)
                            {
                                #region single Account

                                c1PatientDetails.Visible = false;
                                c1FlexAccountGrid.Visible = false;

                                _nPAccountID = Convert.ToInt64(dvAccount[0]["nPAccountID"]);
                                _nPatientID = FillPatientsCombo(_nPAccountID, 0);
                                _FormSelectedPatientID = _nPatientID;
                                FillAccountsCombo(_nPatientID, _nPAccountID); 
                                DisplayPatient(_nPatientID); 
                                
                                pnlAccount.Visible = false;
                                pnlMiddle.Visible = true;
                                pnlLeft.Visible = true;
                                pnlRight.Visible = true;
                                txtPatientSearch.Clear();
                                txtAccountSearch.Clear();

                                #endregion
                            }
                            else
                            {
                                #region show multiple Accounts
                                DesignAccountGrid();
                                for (Int32 _AcctCount = 0; _AcctCount <= dvAccount.Count - 1; _AcctCount++)
                                {
                                    c1FlexAccountGrid.Rows.Add();
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_IsAddressAvailble, dvAccount[_AcctCount]["IsAccountAddressAvailable"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_PAT_ID, dvAccount[_AcctCount]["nPatientID"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_ID, dvAccount[_AcctCount]["nPAccountID"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_No, dvAccount[_AcctCount]["sAccountNo"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_GuarantorName, dvAccount[_AcctCount]["GuarantorName"].ToString());

                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_BusinessCenter, dvAccount[_AcctCount]["sBusinessCenter"].ToString());

                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Name, dvAccount[_AcctCount]["sAccountDesc"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Address1, dvAccount[_AcctCount]["sAddressLine1"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Address2, dvAccount[_AcctCount]["sAddressLine2"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_City, dvAccount[_AcctCount]["sCity"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_State, dvAccount[_AcctCount]["sState"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Zip, dvAccount[_AcctCount]["sZip"].ToString());
                                }

                                pnlMiddle.Visible = false;
                                pnlLeft.Visible = false;
                                pnlRight.Visible = false;
                                pnlAccount.Visible = true;
                                c1PatientDetails.Visible = false;
                                c1FlexAccountGrid.Visible = true;
                                c1FlexAccountGrid.BringToFront();
                                c1FlexAccountGrid.Select(1, 0);
                                c1FlexAccountGrid.Focus();
                                //this.Controls.Add(pnlAccount);
                                pnlAccount.BringToFront();

                                #endregion
                            }
                        }
                        else
                        {

                            ////..Code commented by Sagar Ghodke on 20110810
                            ////.. Will not clear the patient banner if the searched account is not present 

                            //pnlAccount.Visible = false;
                            //_nPatientID = 0;
                            //_nPAccountID = 0;
                            //_nAccountPatientID = 0;
                            //ClearControl("All"); 

                            ////..End code comment by Sagar Ghodke on 20110810

                            MessageBox.Show("Account does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountSearch.Focus();
                            txtAccountSearch.SelectAll();
                            //txtAccountSearch.SelectionStart = 0;
                            //txtAccountSearch.SelectionLength = txtAccountSearch.Text.Length;
                        }
                        dvAccount.Dispose();
                        dvAccount = null;
                        dtAccounts.Dispose();
                        dtAccounts = null;
                    }
                    else
                    {
                        DataTable dtTemp = null;
                        DataView dvNext = null;
                        for (Int32 _Length = 0; _Length < strSearchArray.Length; _Length++)
                        {
                            strSearch = strSearchArray[_Length].Trim();
                            if (strSearch.Trim() != "")
                            {

                                if (_Length == 0)
                                {
                                    strSQL = "SELECT (Case When (len(ltrim(rtrim(PA.sAddressLine1))) <> 0 and LEN(ltrim(rtrim(PA.sCity))) <> 0 and LEN(ltrim(rtrim( PA.sState))) <> 0 " +
                                      " and  LEN(ltrim(rtrim(PA.sZip))) <> 0) then '1' else '0' end) IsAccountAddressAvailable,  " +
                                      " nPatientID, PA.nPAccountID , PA.sAccountNo , PA.sAccountDesc ,   " +
                                      " ISNULL(POC.sFirstName,'') + SPACE(1) + ISNULL(POC.sLastName,'') as  GuarantorName, " +
                                      " (Select top 1 isnull(PAAP.nPatientID, 0) from PA_Accounts_Patients PAAP  " +
                                                  " where PA.nPAccountID = PAAP.nPAccountID and bIsOwnAccount = 1) as nPatientID,  " +
                                          " PA.sAddressLine1,PA.sAddressLine2,PA.sCity,PA.sState,PA.sZip, 		 " +
                                      " POC.sFirstName, POC.sLastName " +
                                      " FROM  PA_Accounts PA Inner join Patient_OtherContacts POC " +
                                          " On POC.nPatientContactID = PA.nGuarantorID " +
                                      " WHERE sAccountNo Like '" + strSearch + "%' Or POC.sFirstName Like '" + strSearch + "%' or POC.sLastName Like '" + strSearch + "%' or PA.sAccountDesc Like '" + strSearch + "%' ";

                                    oDB.Retrive_Query(strSQL, out dtAccounts);
                                    if (dtTemp != null)
                                    {
                                        dtTemp.Dispose();
                                        dtTemp=null;
                                    }
                                    if (dtAccounts != null)
                                    {
                                        DataView dvAccount = dtAccounts.DefaultView;
                                        //  dtAccounts.Dispose();

                                        if (dvAccount != null && dvAccount.Table != null)
                                        {
                                            dtTemp = dvAccount.ToTable();
                                        }
                                        else
                                        {
                                            dtTemp = new DataTable();
                                        }
                                        if (dvAccount != null)
                                        {
                                            dvAccount.Dispose();
                                            dvAccount = null;

                                        }
                                        dtAccounts.Dispose();
                                        dtAccounts = null;
                                    }
                                    else
                                    {
                                        dtTemp = new DataTable();
                                    }
                                    if (dvNext != null)
                                    {
                                        dvNext.Dispose();
                                        dvNext = null;
                                    }
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    if (dtTemp != null)
                                    {
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }
                                    dtTemp = dvNext.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                if (dvNext != null && dvNext.Table != null && dvNext.Table.Columns.Count > 0)
                                    dvNext.RowFilter = dvNext.Table.Columns[nAccountNoColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nAccountDescColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nGuarantorLastNameColumnNo].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dvNext.Table.Columns[nGuarantorFirstNameColumnNo].ColumnName + " Like '%" + strSearch + "%' ";
                            }
                        }

                        if (dvNext.Count > 0)
                        {
                            if (dvNext.Count == 1)
                            {
                                //pnlMiddle.Visible = true;
                                c1PatientDetails.Visible = false;
                                
                                _nPAccountID = Convert.ToInt64(dvNext[0]["nPAccountID"]);
                                _nPatientID = FillPatientsCombo(_nPAccountID, 0);
                                _FormSelectedPatientID = _nPatientID;
                                FillAccountsCombo(_nPatientID, _nPAccountID);
                                DisplayPatient(_nPatientID);

                                pnlAccount.Visible = false;
                                pnlMiddle.Visible = true;
                                pnlLeft.Visible = true;
                                pnlRight.Visible = true;


                                txtPatientSearch.Clear();
                                txtAccountSearch.Clear();
                            }
                            else
                            {
                                #region "Show Multiple Patients"
                                DesignAccountGrid();
                                for (Int32 _AcctCount = 0; _AcctCount <= dvNext.Count - 1; _AcctCount++)
                                {
                                    c1FlexAccountGrid.Rows.Add();
                                    //c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_IsAddressAvailble, dvAccount[_AcctCount]["IsAccountAddressAvailable"].ToString());
                                    //c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_PAT_ID, dvAccount[_AcctCount]["nPatientID"].ToString());
                                    //c1FlexAccountGrid.SetData(c1PatientDetails.Rows.Count - 1, COL_Act_ID, dvAccount[_AcctCount]["nPAccountID"].ToString());
                                    //c1FlexAccountGrid.SetData(c1PatientDetails.Rows.Count - 1, COL_Act_No, dvPatient[_AcctCount]["sAccountNo"].ToString());
                                    //c1FlexAccountGrid.SetData(c1PatientDetails.Rows.Count - 1, COL_Act_Name, dvPatient[_AcctCount]["sAccountDesc"].ToString());
                                    //c1FlexAccountGrid.SetData(c1PatientDetails.Rows.Count - 1, COL_Act_GuarantorName, dvPatient[_AcctCount]["GuarantorName"].ToString());

                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_IsAddressAvailble, dvNext[_AcctCount]["IsAccountAddressAvailable"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_PAT_ID, dvNext[_AcctCount]["nPatientID"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_ID, dvNext[_AcctCount]["nPAccountID"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_No, dvNext[_AcctCount]["sAccountNo"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_GuarantorName, dvNext[_AcctCount]["GuarantorName"].ToString());

                                    //c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_BusinessCenter, dvAccount[_AcctCount]["sBusinessCenter"].ToString());

                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_BusinessCenter, dvNext[_AcctCount]["sBusinessCenter"].ToString());
                                    
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Name, dvNext[_AcctCount]["sAccountDesc"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Address1, dvNext[_AcctCount]["sAddressLine1"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Address2, dvNext[_AcctCount]["sAddressLine2"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_City, dvNext[_AcctCount]["sCity"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_State, dvNext[_AcctCount]["sState"].ToString());
                                    c1FlexAccountGrid.SetData(c1FlexAccountGrid.Rows.Count - 1, COL_Act_Zip, dvNext[_AcctCount]["sZip"].ToString());

                                }

                                pnlMiddle.Visible = false;
                                pnlLeft.Visible = false;
                                pnlRight.Visible = false;
                                pnlAccount.Visible = true;
                                c1PatientDetails.Visible = false;
                                c1FlexAccountGrid.Visible = true;
                                c1FlexAccountGrid.BringToFront();
                                c1FlexAccountGrid.Select(1, 0);
                                c1FlexAccountGrid.Focus();
                                //this.Controls.Add(pnlAccount);
                                pnlAccount.BringToFront();

                                #endregion
                            }
                        }
                        else
                        {
                            //added by mahesh s on 23/may/2011 for if account search have ',' throwing error.

                            ////..Code commented by Sagar Ghodke on 20110810
                            ////..Will not clear the patient banner if searched account is not found 
                            //_nPatientID = 0;
                            //_nPAccountID = 0;
                            //_nAccountPatientID = 0;
                            //ClearControl("All"); 
                            //pnlAccount.Visible = false;
                            ////..End code commenting by Sagar Ghodke on 20110810


                            //pnlMiddle.Visible = true;
     


                            MessageBox.Show("Account does not exist.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountSearch.Focus();
                            txtAccountSearch.SelectAll();
                        }
                        if (dtTemp != null)
                        {
                            dtTemp.Dispose();
                            dtTemp = null;

                        }
                        if (dvNext != null)
                        {
                            dvNext.Dispose();
                            dvNext = null;
                        }
                    }

                }
                else
                {
                    _FormSelectedPatientID = 0;
                    _nPatientID = 0;
                    _nPAccountID = 0;
                    _nAccountPatientID = 0;
                    ClearControl("All"); 
                    pnlAccount.Visible = false;
                    txtAccountSearch.Focus();
                    txtAccountSearch.SelectionStart = 0;
                    txtAccountSearch.SelectionLength = txtAccountSearch.Text.Length;
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
                if (dtAccounts != null) { dtAccounts.Dispose(); }
            //    if (dvAccount != null) { dvAccount.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        //code added by mahesh S(Apollo)
        private void c1FlexAccountGrid_DoubleClick(object sender, EventArgs e)
        {
            if (c1FlexAccountGrid.Rows.Count > 0)
            {
                if (c1FlexAccountGrid.RowSel > 0)
                {
                    if (c1FlexAccountGrid.GetData(c1FlexAccountGrid.RowSel, COL_Act_ID) != null && c1FlexAccountGrid.GetData(c1FlexAccountGrid.RowSel, COL_Act_ID).ToString() != "")
                    {
                        _nPAccountID = Convert.ToInt64(c1FlexAccountGrid.GetData(c1FlexAccountGrid.RowSel, COL_Act_ID).ToString());

                        c1PatientDetails.Visible = false;
                        c1FlexAccountGrid.Visible = false;
                        
                        _nPatientID = FillPatientsCombo(_nPAccountID, 0);
                        _FormSelectedPatientID = _nPatientID; ;
                        FillAccountsCombo(_nPatientID, _nPAccountID);
                        DisplayPatient(_nPatientID);

                        //pnlAccount.Visible = false;
                        //pnlMiddle.Visible = true;
                        //pnlLeft.Visible = true;
                        //pnlRight.Visible = true;
                        
                        pnlMiddle.Visible = true;
                        pnlLeft.Visible = true;
                        pnlRight.Visible = true;
                        pnlAccount.Visible = false;
                        c1PatientDetails.Visible = false;

                        txtPatientSearch.Clear();
                        txtAccountSearch.Clear();


                        if (OnAccountChanged != null)
                            OnAccountChanged(null, null);
                        //------------------------------
                    }
                }
            }
        }

        private void c1FlexAccountGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                c1FlexAccountGrid_DoubleClick(null, null);
            }
        }

        private void c1FlexAccountGrid_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        //code added by mahesh S(Apollo)
        private void c1PatientDetails_DoubleClick(object sender, EventArgs e)
        {
            if (c1PatientDetails.Rows.Count > 0)
            {
                if (c1PatientDetails.RowSel > 0)
                {
                    if (c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID) != null && c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID).ToString() != "")
                    {
                        if (PatientStripControl.GetPatientAccountCount(Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID).ToString())))
                        {
                            MessageBox.Show("Patient is not associated with any Account.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        _nPatientID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_PAT_ID).ToString());
                        _FormSelectedPatientID = _nPatientID;
                        c1PatientDetails.Visible = false;
                      
                        //fill selected patient details
                        FillDetails(_nPatientID, _ParentForm);

                        //pnlMiddle.Visible = true;
                        //pnlLeft.Visible = true;
                        //pnlRight.Visible = true;

                        pnlMiddle.Visible = true;
                        pnlLeft.Visible = true;
                        pnlRight.Visible = true;
                        pnlAccount.Visible = false;
                        c1PatientDetails.Visible = false;


                        txtPatientSearch.Clear();
                        txtAccountSearch.Clear();

                        if (OnPatientChanged != null)
                            OnPatientChanged(null, null);
                    }
                }
            }
        }

        private void c1PatientDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                c1PatientDetails_DoubleClick(null, null);
            }
        }

        private void c1PatientDetails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

	    #endregion    

        #region "On Hold Messages"

        private void FillClaimOnHold()
        {

            lblClaimOnHold.Text = "";
            //txtNotes.Text = "";
            DataTable dtClaimOnHold = null;
            dtClaimOnHold = PatientStripControl.GetClaimsOnHold(_nPatientID, _nPAccountID);

            if (dtClaimOnHold != null && dtClaimOnHold.Rows.Count > 0)
            {
                if (pnlClaimOnHold.Visible == true)
                {
                    pnlHoldAlert.Visible = false;
                }
                else
                {
                    pnlHoldAlert.Visible = true;
                }

                pnlNotes.Height = 28;
                lblClaimOnHold.Text = "Some claims are on hold !";
                txtClaimOnHold.Text = dtClaimOnHold.Rows[0]["ClaimNo"].ToString();

            }
            else
            {
                pnlClaimOnHold.Hide();
                lblClaimOnHold.Text = "";
                pnlHoldAlert.Visible = false;
                pnlNotes.Height = 41;
                txtClaimOnHold.Text = "";
            }
            if( dtClaimOnHold != null )
            {
                dtClaimOnHold.Dispose();
                dtClaimOnHold=null;
            }
            // Bug# 48050 - when we hold a insurance plan from PatAcct >> modify patient from banner then it will not refresh the grid.
            //if (OnInsurancePlanHoldChanged != null)
            //{
            //    OnInsurancePlanHoldChanged();
            //}
        }

        private void btnClaimOnHold_Click(object sender, EventArgs e)
        {
            pnlClaimOnHold.Show();
            pnlHoldAlert.Hide();
        }

        private void btnClaimOnHoldClose_Click(object sender, EventArgs e)
        {
            pnlClaimOnHold.Hide();
            pnlHoldAlert.Show();
        } 

        #endregion

        private void chkAllAcctPat_CheckedChanged(object sender, EventArgs e)
        {
            RefreshBalances();
            if (_ParentForm == FormName.NewCharges || _ParentForm == FormName.ModifyCharges)
            {

            }
            else
            {
                if (OnPatientChanged != null)
                    OnPatientChanged(null, null);
            }
        }

        
        private void btnMouseHover(object sender, EventArgs e)
        {
           try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloStrips.Properties.Resources.Img_Yellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloStrips.Properties.Resources.Img_Orange;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btn_ModifyPatient_MouseHover(object sender, EventArgs e)
        {

            btn_ModifyPatient.BackgroundImage = global::gloStrips.Properties.Resources.PatientHover;
            btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_ModifyPatient_MouseLeave(object sender, EventArgs e)
        {
            btn_ModifyPatient.BackgroundImage = global::gloStrips.Properties.Resources.Patient;
            btn_ModifyPatient.BackgroundImageLayout = ImageLayout.Center;
        }

        //private void pnlNotes_Scroll(object sender, ScrollEventArgs e)
        //{
        //    HorizontalScroll.Visible = false;
           
        //}
         
       
    }
}
