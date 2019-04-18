using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloGlobal;
using System.Text.RegularExpressions;
using gloSnoMed; 
namespace gloBilling
{  
 public  enum  EnmImmediacy
   {
        Acute = 1,
        Chronic = 2,
        unknown = 3,
   }
    public partial class frmSetupICD9 : Form
    {
        private string _databaseconnectionstring = "";
        public string _MessageBoxCaption = String.Empty;
        
        //private ICD9 oICD9;

        private Int64 _nICD9ID = 0;
       // private ToolTip tooltipSnomed;
        private string _Code = "";
        public string Code { get { return this._Code; } set { this._Code = value; } }

        private string _Description = "";
        public string Description { get { return this._Description; } set { this._Description = value; } }
       
        public bool IsSaving { get; set; }

        private string _Speciality = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 nICD9ID
        {
            get { return _nICD9ID; }
            set { _nICD9ID = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public string ICD9Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string ICD9Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

    //   gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord
        private string _gstrSMDBServerName = "";
        public string gstrSMDBServerName
        {
            get { return _gstrSMDBServerName; }
            set { _gstrSMDBServerName = value; }
        }


        private string _gstrSMDBDatabaseName = "";
        public string gstrSMDBDatabaseName
        {
            get { return _gstrSMDBDatabaseName; }
            set { _gstrSMDBDatabaseName = value; }
        }

        private bool _gblnSMDBAuthen = false;
        public bool gblnSMDBAuthen
        {
            get { return _gblnSMDBAuthen; }
            set { _gblnSMDBAuthen = value; }
        }

        private string _gstrSMDBUserID = "";
        public string gstrSMDBUserID
        {
            get { return _gstrSMDBUserID; }
            set { _gstrSMDBUserID = value; }
        }

        private string _gstrSMDBPassWord = "";
        public string gstrSMDBPassWord
        {
            get { return _gstrSMDBPassWord; }
            set { _gstrSMDBPassWord = value; }
        }

        
        #region Constructor

        public frmSetupICD9(gloICD.CodeRevision ICDRevision, string DatabaseConnectionString)
        {
            InitializeComponent();           
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

            this.ICDRevision = ICDRevision;
        }

        public gloICD.CodeRevision ICDRevision { get; set; }

        public frmSetupICD9(gloICD.CodeRevision ICDRevision, Int64 ID, string DatabaseConnectionString)
        {
            InitializeComponent();            
            _nICD9ID = ID;
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

            this.ICDRevision = ICDRevision;
        }

        #endregion
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);
        private void frmSetupICD9_Load(object sender, EventArgs e)
        {
            try
            {
                FillSpeciality();
               
                if (ICDRevision == gloICD.CodeRevision.ICD9)
                {
                    IntPtr myIcon = ((Bitmap)ImgICDSetup.Images[0]).GetHicon();
                    this.Icon = Icon.FromHandle(myIcon);
                    DestroyIcon(myIcon);
                    this.Text = "ICD-9 Setup";
                    lblICD9Code.Text = "ICD9 Code :";
                    SetFormDimension(320, 665);    //height change as design change 8020 order PRD  
                }
                else
                {
                    IntPtr myIcon = ((Bitmap)ImgICDSetup.Images[1]).GetHicon();
                    this.Icon = Icon.FromHandle(myIcon);
                    DestroyIcon(myIcon);
                 
                    this.Text = "ICD-10 Setup";
                    lblICD9Code.Text = "ICD10 Code :";                   
                }
                

                bool IsInUse = false;
                if (_nICD9ID != 0)
                {
                    tsb_save.Visible = false;
                    using (ICD9 icd = new ICD9(_databaseconnectionstring))
                    {
                        using (DataTable dtICD9 = icd.GetICD9(_nICD9ID))
                        {
                            if (dtICD9 != null && dtICD9.Rows.Count != 0)
                            {
                                IsInUse = clsICD.IsInUseICD(dtICD9.Rows[0]["sICD9Code"].ToString(), ICDRevision.GetHashCode());
                                if (IsInUse)
                                {
                                    txtICD9Code.ReadOnly = true;
                                }
                                
                                txtICD9Code.Text = dtICD9.Rows[0]["sICD9Code"].ToString();
                                txtDescription.Text = dtICD9.Rows[0]["sDescription"].ToString();
                                cmbSpecialty.SelectedValue = dtICD9.Rows[0]["nSpecialtyID"];
                                rbInactive.Checked = Convert.ToBoolean(dtICD9.Rows[0]["bInactive"]);
                               
                                strSnomedDescription = dtICD9.Rows[0]["sSnomedDescription"].ToString();
                                strSnomedDefination = dtICD9.Rows[0]["sSnomedDefination"].ToString();
                                strConceptID = dtICD9.Rows[0]["sConceptID"].ToString();
                                strDescriptionID=dtICD9.Rows[0]["sDescriptionID"].ToString();
                                strSnoMedID = dtICD9.Rows[0]["sSnomedID"].ToString();
                                txtsnodesc.Text = strSnomedDescription;
                                string strConid = strConceptID.Trim() + "-" + strSnomedDescription.Trim();
                                  // 8020 prd changes conceptid and snomed description concatnated ,chetan 
                                if (strConid.Trim().Length > 1)
                                {
                                    txt_ConceptID.Text = strConceptID + "-" + strSnomedDescription;
                                }
                                else
                                {
                                    txt_ConceptID.Text = ""; 
                                }
                                      //code commented as treeview removed 8020 order PRD
                                //if (!string.IsNullOrEmpty(strSnomedDescription) & !string.IsNullOrEmpty(strSnomedDefination))
                                //{
                                //   BindSnomedToTree(strSnomedDescription + "|" + strSnomedDefination, trv_SNOMEDDesc);
                                //    //trv_SNOMEDDesc.ExpandAll()
                                //}
                                //else
                                //{
                                //    trv_SNOMEDDesc.Nodes.Clear();
                                //}

                                switch (Convert.ToInt64(dtICD9.Rows[0]["Immediacy"]))
                                {
                                    case 1:
                                        rbt_Acute.Checked = true;
                                        break;
                                    case 2:
                                        rbtn_Chronic.Checked = true;
                                        break;
                                    case 3:
                                        rbtn_Unknown.Checked = true;
                                        break;
                                }
                                if (ICDRevision == gloICD.CodeRevision.ICD10)
                                {
                                    if (ucICDNotes.LoadControl(Convert.ToString(dtICD9.Rows[0]["sICD9Code"])) == true)
                                    {
                                        //pnlICDNotes.Show();
                                        SetFormDimension(670, 665);
                                        this.CenterToScreen();
                                    }
                                    else { SetFormDimension(320, 665); } //pnlICDNotes.Hide(); }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_Code.Trim() != "")
                    {
                        txtICD9Code.Text = _Code;
                        txtDescription.Select();
                        txtDescription.Focus();
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while Form Load " + ex.ToString(), true);
            }
           
        }

        private void SetFormDimension(Int32 height,Int32 width)
        {
            this.Height =height;
            this.Width = width;
        }

        private void FillSpeciality()
        {
            DataTable dtSpeciality = null;

            try
            {
                dtSpeciality = clsICD.GetAllSpeciality(false, _databaseconnectionstring);

                if (dtSpeciality != null)
                {
                    cmbSpecialty.DataSource = dtSpeciality;
                    cmbSpecialty.ValueMember = dtSpeciality.Columns[0].ColumnName;
                    cmbSpecialty.DisplayMember = dtSpeciality.Columns[1].ColumnName;
                    cmbSpecialty.SelectedIndex = 0;
                }
            }
            catch //(Exception ex)
            {
               //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ICD, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }


        private bool ValidateICD(clsICD icd)
        {
            try
            {
                if (txtICD9Code.Text.Trim() == "")
                {
                    if (ICDRevision == gloICD.CodeRevision.ICD9)
                    {
                        MessageBox.Show("Enter an ICD-9 code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Enter an ICD-10 code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    txtICD9Code.Focus();
                    return false;
                }



                if (IsSpecialCharInICDCode(txtICD9Code.Text.Trim()))
                {

                    if (ICDRevision == gloICD.CodeRevision.ICD9)
                    {
                        MessageBox.Show("Special characters not allowed in ICD-9 code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Special characters not allowed in ICD-10 code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtICD9Code.Focus();
                    return false;
                }


                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a Description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return false;
                }


                if (clsICD.IsExistsICD(icd.ID, icd.Code, icd.ICDRevision.GetHashCode(), _ClinicID))
                {
                    MessageBox.Show("Code is already in use by another entry. Select a unique code. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
             
                if (icd.ID == 0 && icd.ICDRevision.GetHashCode() == gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode())
                {
                    if (icd.Code.Substring(icd.Code.Length-1,1)==".")
                    {
                        //var resultString = System.Text.RegularExpressions.Regex.Match(icd.Code, "[.]{3},").Value;
                        MessageBox.Show("Cannot add ICD-10 code." + Environment.NewLine + "Invalid code format.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                    else if (icd.Code.Replace(".", "").Length > 3 && icd.Code.Contains(".") == false)
                    {
                        MessageBox.Show("Cannot add ICD-10 code." + Environment.NewLine + "Invalid code format.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (clsICD.IsICD10Category(icd.Code))
                    {
                        MessageBox.Show("Cannot add ICD-10 code." + Environment.NewLine + "Code is a part of standard ICD-10 category and is non-billable.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    
                }
                return true;
            }
            catch (Exception ex)
            {               
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while validate ICD " + ex.ToString(), true);
                return false;
            }
        }

        private static bool IsSpecialCharInICDCode(string sICDCode)
        {            
            var regex = new Regex(@"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]");
            if (regex.IsMatch(sICDCode.Trim()))
            {  return true; }  
            else 
            {  return false; }
            
        }

        /// <summary>
        /// this method validates input data then
        /// Add or Modify ICD9
        /// </summary>       
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        SaveICD(true);
                        break;

                    case "Cancel": 
                        try
                        {
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        break;
                    case "save":
                        SaveICD(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while Button click" + ex.ToString(), true);                
            }
        }

        private void SaveICD(bool IsClose)
        {
            string strisNew = "";
            try
            {
                if (_nICD9ID == 0)
                {
                    strisNew = "Add";
                }
                else
                {
                    strisNew = "Modify";
                }
                using (clsICD icd = GetICD())
                {
                    if (ValidateICD(icd))
                    {
                        icd.sConceptID = strConceptID;
                        icd.sDescriptionID = strDescriptionID;
                        icd.sSnomedDefination = strSnomedDefination;
                        icd.sSnomedDescription = strSnomedDescription;
                        icd.sSnomedID = strSnoMedID;
                        _nICD9ID = clsICD.SaveICD(icd);

                        this.Code = txtICD9Code.Text.Trim();
                        this.Description = txtDescription.Text.Trim();
                        _Speciality = cmbSpecialty.Text.Trim();

                        this.IsSaving = true;

                        if (_nICD9ID <= 0)
                        {
                            if (ICDRevision == gloICD.CodeRevision.ICD9)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ICD9, ActivityType.Add, strisNew + " ICD - 9 Code: " + this.Code.ToString(), 0, _nICD9ID, 0, ActivityOutCome.Failure);
                            else if (ICDRevision == gloICD.CodeRevision.ICD10)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ICD10, ActivityType.Add, strisNew + " ICD - 10 Code: " + this.Code.ToString(), 0, _nICD9ID, 0, ActivityOutCome.Failure);

                            MessageBox.Show(" Error ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (!IsClose)
                            {
                                _nICD9ID = 0;
                                _Code = "";
                                _Description = "";
                                txtICD9Code.Text = "";
                                txtDescription.Text = "";
                                rbActive.Checked = true;
                            }
                            if (ICDRevision == gloICD.CodeRevision.ICD9)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ICD9, ActivityType.Add, strisNew + " ICD - 9 Code: " + this.Code.ToString(), 0, _nICD9ID, 0, ActivityOutCome.Success);
                            else if (ICDRevision == gloICD.CodeRevision.ICD10)
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ICD10, ActivityType.Add, strisNew + " ICD - 10 Code: " + this.Code.ToString(), 0, _nICD9ID, 0, ActivityOutCome.Success);

                        }
                    }
                    else
                    {
                        return;
                    }
                }

                if (IsClose) { this.Close(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while adding a new ICD " + ex.ToString(), true);
            }
            finally
            {
                strisNew = null;
            }
        }

        /// <summary>
        /// Set ICD9 object for Adding new / Update
        /// </summary>
        /// <returns></returns>
        private clsICD GetICD()
        {
            try
            {
                    clsICD objICD = new clsICD();                
                    objICD.ID = _nICD9ID;
                    objICD.Code = txtICD9Code.Text.Trim();
                    objICD.Description = txtDescription.Text.Trim();
                    objICD.SpecialityID = Convert.ToInt64(cmbSpecialty.SelectedValue);
                    //objICD.Speciality = cmbSpecialty.Text.Trim();  
                    objICD.IsActive = rbInactive.Checked;
                    objICD.ICDRevision = ICDRevision;
                    objICD.ClinicID = ClinicID;

                    if (rbt_Acute.Checked)
                    {
                        objICD.ImmediacyID = EnmImmediacy.Acute.GetHashCode();
                    }
                    else if (rbtn_Chronic.Checked)
                    {
                        objICD.ImmediacyID = EnmImmediacy.Chronic.GetHashCode();
                    }
                    else
                    {
                        objICD.ImmediacyID = EnmImmediacy.unknown.GetHashCode();
                    }
                    return objICD;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while GetICD " + ex.ToString(), true);
                return null;
            }
        }

       

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbActive.Checked == true)
                {
                    rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                {

                    rbActive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while rbActive_CheckedChanged " + ex.ToString(), true);               

            }
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbInactive.Checked == true)
                {
                    rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                {
                    rbInactive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while rbInactive_CheckedChanged " + ex.ToString(), true);

            }
        }

        private void rbt_Acute_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_Acute.Checked == true)
            {
                rbt_Acute.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbt_Acute.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbtn_Chronic_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtn_Chronic.Checked == true)
                {
                    rbtn_Chronic.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                {

                    rbtn_Chronic.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while rbtn_Chronic_CheckedChanged " + ex.ToString(), true);

            }
        }

        private void rbtn_Unknown_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Unknown.Checked == true)
            {
                rbtn_Unknown.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbtn_Unknown.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void txtICD9Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsSpecialCharInICDCode(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

           private string  GetHybridConnectionString(string  strSQLServerName , string strDatabase , string  sUserName, string  sPassword,bool blnSMDbauth) 
           {
               string  strConnectionString;
              
              if(blnSMDbauth==true )   
               strConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDatabase + ";User ID=" + sUserName + ";Password=" + sPassword + "";
              else
              strConnectionString = "SERVER= " + strSQLServerName + " ; DATABASE=" + strDatabase + " ; " +" Integrated Security=SSPI";
           

        return  strConnectionString;
           }

           
         string strConceptID = "", strDescriptionID = "", strSnoMedID = "", strSnomedDescription = "", strSnomedDefination = "", strRxNormCode = "", strNDCCode = "";
    
        private void btn_SnomedCode_Click(object sender, EventArgs e)
        {
         
           //  bool gblnSMDBAuthen=false ;
  
  
         string    gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gstrSMDBUserID, gstrSMDBPassWord,gblnSMDBAuthen );
         FrmSelectProblem frm = new FrmSelectProblem("ICD Setup", gstrSMDBConnstr, _databaseconnectionstring);
           // string Hs_StrSnoMedID = null;
           // string str = "";
         try
         {
            // frm.StartPosition = FormStartPosition.Manual;
            // frm.ShowInTaskbar = false;
             frm.blnIsProblem = true; //added for 8020 prd changes 
            
             
               // 'Code changed by MAYURI:20130125 To show Conceptid in search window in modify mode else shoe conceptDescription
             if (!string.IsNullOrEmpty(strConceptID.Trim()))
             {
                 frm.txtSMSearch.Text = strConceptID;  //txt_ConceptID.Text.Trim();
             }
             else
             {
                 if (txtICD9Code.Text.Trim() != "")  //if code is present then show it on snomed screen 8020 PRd changes
                 {
                     frm.txtSMSearch.Text = txtICD9Code.Text;
                 }

                 if (ICDRevision == gloICD.CodeRevision.ICD9)
                 {
                     frm.strCodeSystem = "ICD9";
                 }
                 else
                 {
                     frm.strCodeSystem = "ICD10";
                 }
             }
             frm.strConceptDesc = txtDescription.Text;
             frm.strDescriptionID = strDescriptionID;
             frm.strConceptID = strConceptID;  //txt_ConceptID.Text.Trim();
             frm.ShowDialog(this);
             txtDescription.Focus();

             if (frm._DialogResult)
             {
                 strConceptID = frm.strConceptID;
                 strDescriptionID = frm.strDescriptionID;
                 strSnoMedID = frm.StrSnoMedID;
                 strSnomedDescription = frm.strSelectedDescription;
                 strSnomedDefination = frm.strSelectedDefination;
                 // txtICD9Code.Text = frm.strICD9
                 strRxNormCode = frm.strRxNormCode;
                 strNDCCode = frm.strNDCCode;
                 if (string.IsNullOrEmpty(txtDescription.Text))
                 {

                 }
                 //gblnIcd10Transition


                 if (string.IsNullOrEmpty(txtICD9Code.Text) && !string.IsNullOrEmpty(frm.strICD10))
                 {
                    String[]  stricd=frm.strICD10.Split(':');
                    if (stricd.Length > 0)
                    {
                        txtICD9Code.Text = stricd[0];
                    }


                 }
                 else
                 {
                     if (string.IsNullOrEmpty(txtICD9Code.Text) && !string.IsNullOrEmpty(frm.strICD9))
                     {
                         String[] stricd = frm.strICD9.Split(':');
                         if (stricd.Length > 0)
                         {
                             txtICD9Code.Text = stricd[0];
                         }

                     }
                 }



                 string strConid = strConceptID.Trim() + "-" + strSnomedDescription.Trim();
                  // 8020 prd changes conceptid and snomed description concatnated ,chetan 
                 if (strConid.Trim().Length > 1)
                 {
                     txt_ConceptID.Text = strConceptID + "-" + strSnomedDescription;
                 }
                 else
                 {
                     txt_ConceptID.Text = "";
                 }
                 txtsnodesc.Text = strSnomedDescription;
                 //code commented as treeview removed 8020 order PRD
                 //if (!string.IsNullOrEmpty(strSnomedDescription) & !string.IsNullOrEmpty(strSnomedDefination))
                 //{
                 //  BindSnomedToTree(strSnomedDescription + "|" + strSnomedDefination, trv_SNOMEDDesc);

                 //}
                 //else
                 //{
                 //    trv_SNOMEDDesc.Nodes.Clear();
                 //}

             }

         }
         catch //(Exception ex)
         {

         }
         finally
         {
             if (frm != null)
             {
                 frm.Dispose();
                 frm = null;
             }
         }
        }
        //code commented as treeview removed 8020 order PRD
        //private void BindSnomedToTree(string strDescription, TreeView trvDefination)
        //{
        //    try
        //    {
        //        string[] strHeader = null;
        //        string[] strDefination = null;
        //        string strHead = "";
        //        myTreeNode oIsNode = default(myTreeNode);
        //        myTreeNode oDescr = default(myTreeNode);
        //        if (!string.IsNullOrEmpty(strDescription))
        //        {
        //            trvDefination.Nodes.Clear();
        //            trvDefination.ImageList = null;
        //            trvDefination.ImageList = imgTreeVIew;
        //            strHeader = strDescription.Split('|');
        //            if (strHeader.Length > 0)
        //            {
        //                strHead = strHeader[0];
        //                myTreeNode oParenetNode = new myTreeNode();
        //                oParenetNode.Text = strHead;
        //                oParenetNode.ImageIndex = 2;
        //                oParenetNode.SelectedImageIndex = 2;

        //                strHead = strHeader[1];
        //                myTreeNode oParenetNode1 = new myTreeNode();
        //                oParenetNode1.Text = strHead;
        //                oParenetNode1.ImageIndex = 2;
        //                oParenetNode1.SelectedImageIndex = 2;

        //                oParenetNode.Nodes.Add(oParenetNode1);
        //                trvDefination.Nodes.Add(oParenetNode);
        //                for (int i = 2; i <= strHeader.Length - 1; i++)
        //                {
        //                    strDefination = strHeader.GetValue(i).ToString().Split (':');
        //                    oIsNode = new myTreeNode();
        //                    oIsNode.Text = strDefination[0];
        //                    oIsNode.ImageIndex = 1;
        //                    oIsNode.SelectedImageIndex = 1;
        //                    oParenetNode1.Nodes.Add(oIsNode);
        //                    oDescr = new myTreeNode();
        //                    oDescr.Text = strDefination[1];
        //                    oDescr.ImageIndex = 0;
        //                    oDescr.SelectedImageIndex = 0;
        //                    oIsNode.Nodes.Add(oDescr);
        //                }
        //            }
        //            trv_SNOMEDDesc.ExpandAll();
        //            trv_SNOMEDDesc.SelectedNode = trv_SNOMEDDesc.Nodes[0];
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            txt_ConceptID.Text = "";
         //   trv_SNOMEDDesc.Nodes.Clear();  
            txtsnodesc.Text = "";  
            strSnomedDescription="";
            strSnomedDefination = "";
            strConceptID="";
        }

       

        private void frmSetupICD9_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        
    }
}
