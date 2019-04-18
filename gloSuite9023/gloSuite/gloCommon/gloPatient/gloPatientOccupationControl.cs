using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using gloContacts;
using gloAddress;

namespace gloPatient
{
    public partial class gloPatientOccupationControl : UserControl
    {
        #region Declarations

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private PatientOccupation oPatientOccu = null;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = " gloPM ";

        gloAddressControl oAddresscontrol = null;
        private bool _IsInternetFax = false;

       #endregion

        #region Properties
        
        public PatientOccupation OcupationDetails
        {
            get { return oPatientOccu; }
            set { oPatientOccu = value; }
        }
        
#endregion

        #region Constructor

        public gloPatientOccupationControl()
        {
            InitializeComponent();
            oPatientOccu = new PatientOccupation();
        }

        public gloPatientOccupationControl(string databaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseConnectionString;
            oPatientOccu = new PatientOccupation();

            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion 


            //Sandip Darade  20091021
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

            #endregion  
        }
        
#endregion

        #region Control Events and Delegates 

        public delegate void onOccupationSaveClicked(object sender, EventArgs e);
        public event onOccupationSaveClicked onOccupationSave_Clicked;

        public delegate void onOccupationCloseClicked(object sender, EventArgs e);
        public event onOccupationCloseClicked onOccupationClose_Clicked;
      
        private void btnsaveOccuInfo_Click(object sender, EventArgs e)
        {
            this.GetData();
            onOccupationSave_Clicked(sender, e);

        }

        private void btnClsOccuInfo_Click(object sender, EventArgs e)
        {
            onOccupationClose_Clicked(sender,e);
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            //used for the validating the masked text box
                            if (mskOIMobile.IsValidated == true && mskOIPhone.IsValidated == true)
                            {
                                if(this.GetData())
                                onOccupationSave_Clicked(sender, e);
                            }
                            break;
                        }
                    case "Cancel":
                        {
                            if (IsModified() == false)
                                onOccupationClose_Clicked(sender, e);
                            else
                            {
                                 DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                 if (res == DialogResult.Yes)
                                 {
                                     if (mskOIMobile.IsValidated == true && mskOIPhone.IsValidated == true)
                                     {
                                            if (this.GetData())
                                             onOccupationSave_Clicked(sender, e);
                                     }
                                 }
                                 else if (res == DialogResult.No)
                                 {
                                     //used for mask text box to not to validate the text box.
                                     mskOIPhone.AllowValidate = false;
                                     mskOIMobile.AllowValidate = false;
                                     onOccupationClose_Clicked(sender, e);
                                    
                                 }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods GetData, SetData

        public bool GetData()
        {
            try
            {
                if (!ValidateData())
                {
                    return false; 
                }

                oPatientOccu.Occupation = txtwOccupation.Text.Trim();

                //private string _sEmploymentStatus = "";
                //if (cmbwEmpStatus.SelectedIndex != -1)
                oPatientOccu.PatientEmploymentStatus = cmbStatus.Text.ToString(); //cmbwEmpStatus.Text.ToString();

                //private string _sPlaceofEmployment = "";
                oPatientOccu.PatientPlaceofEmployment = txtWLocation.Text.Trim();

                //private string _sWorkAddress1 = "";
                //oPatientOccu.PatientWorkAddress1 = txtwAddress1.Text.Trim();

                ////private string _sWorkAddress2 = "";
                //oPatientOccu.PatientWorkAddress2 = txtwAddress2.Text.Trim();

                ////private string _sWorkCity = "";
                //oPatientOccu.PatientWorkCity = txtwCity.Text.Trim();

                ////private string _sWorkState = "";
                //oPatientOccu.PatientWorkState = cmbWState.Text.Trim();

                ////private string _sWorkZip = "";
                //oPatientOccu.PatientWorkZip = txtwZip.Text.Trim();

                //oPatientOccu.PatientWorkCounty = txtCounty.Text.Trim();

                //oPatientOccu.PatientWorkCountry = cmbCountry.Text.Trim();    


                //Sandip Darade 20091021
                //Above code for address replaced by code below as we now use addrescontrol 

                oPatientOccu.PatientWorkAddress1 = oAddresscontrol.txtAddress1.Text.Trim();
                oPatientOccu.PatientWorkAddress2 = oAddresscontrol.txtAddress2.Text.Trim();
                oPatientOccu.PatientWorkZip = oAddresscontrol.txtZip.Text.Trim();
                oPatientOccu.PatientWorkCity = oAddresscontrol.txtCity.Text.Trim();
                oPatientOccu.PatientWorkCountry = oAddresscontrol.cmbCountry.Text.Trim();
                oPatientOccu.PatientWorkState = oAddresscontrol.cmbState.Text.Trim();
                oPatientOccu.PatientWorkCounty = oAddresscontrol.txtCounty.Text.Trim();


                //private string _sWorkPhone = "";
                oPatientOccu.PatientWorkPhone = mskOIPhone.Text.Trim();

                ////private string _sWorkFax = "";
               oPatientOccu.PatientWorkFax = txtOIFax.Text.Trim();

                oPatientOccu.PatientWorkMobile = mskOIMobile.Text.Trim();

                //Sandip Darade 20100216
                //case  GLO2008-0002029
                //phone no,mobile no ,fax no will be saved with  mask e.g .(111)222-3333
                //if (mskOIMobile.Text.Length != 0 && mskOIMobile.MaskFull == false)
                //{
                //    oPatientOccu.PatientWorkMobile = "";
                //}
                //else
                //{
                //    oPatientOccu.PatientWorkMobile = mskOIMobile.Text;

                //}
                //if (mskOIPhone.Text.Length != 0 && mskOIPhone.MaskFull == false)
                //{
                //    oPatientOccu.PatientWorkPhone = "";
                //}
                //else
                //{
                //    oPatientOccu.PatientWorkPhone = mskOIPhone.Text;

                //}

                //if (_IsInternetFax == true)
                //{
                //    if (txtOIFax.Text.Length != 0 && txtOIFax.MaskFull == false)
                //    {
                //        oPatientOccu.PatientWorkFax = "";
                //    }
                //    else
                //    {
                //        oPatientOccu.PatientWorkFax = txtOIFax.Text.Trim();

                //    }
                //}
                //else
                //{
                //    oPatientOccu.PatientWorkFax = txtOIFax.Text.Trim();
                //}

                //Sandip Darade 20100216
                // end 

                oPatientOccu.PatientWorkEmail = txtOIEmail.Text.Trim();

                //Code added on 14 May 2008 by Sagar Ghodke

                oPatientOccu.EmployerName = txtEmployerName.Text.Trim();
                //commented 20090406 Sandip Darade 
                //oPatientOccu.RetirementDate = dtpRetirementDate.Value.Date;
                //Sandip Darade 20090406
                //retirement date added through masked text box.
                //when employment status is  as retired
                if (mtxtPARetirementDate.Enabled==true && mtxtPARetirementDate.MaskCompleted == true)
                {
                    mtxtPARetirementDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    oPatientOccu.RetirementDate = Convert.ToDateTime(mtxtPARetirementDate.Text);
                }
                else 
                {
                    oPatientOccu.RetirementDate = null;
                }
                

                if (cmbwEmpStatus.Enabled == true)
                {
                    oPatientOccu.EmploymentType = cmbwEmpStatus.Text.ToString();
                }
                else
                {
                    oPatientOccu.EmploymentType = "";
                }
                //
               

                return true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        private bool ValidateData()
        {
            ////Incomplete Phone Numbers
            //mskOIPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskOIPhone.Text.Length > 0 && mskOIPhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for the occupation phone.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskOIPhone.Focus();
            //    return false;
            //}
            ////Incomplete mobile no
            //mskOIMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskOIMobile.Text.Length > 0 && mskOIMobile.MaskCompleted == false)
            //{

            //    MessageBox.Show("Please enter a 10 digit number for the occupation mobile.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskOIMobile.Focus();
            //    return false;
            //}
            //invalid email address
            if (CheckEmailAddress(txtOIEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOIEmail.Focus();
                return false;
            }

            #region "Validate Retirement Date"
            if (mtxtPARetirementDate.Enabled == true)
            {
                mtxtPARetirementDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mtxtPARetirementDate.Text.Length > 0 && mtxtPARetirementDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    if (mtxtPARetirementDate.MaskCompleted == true)
                    {
                        try
                        {
                            mtxtPARetirementDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                            if (Convert.ToDateTime(mtxtPARetirementDate.Text).Date >= DateTime.Now.Date)
                            {
                                MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                        catch (Exception) // ex)
                        {   
                            //ex.ToString();
                            //ex = null;
                            MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }
            }

#endregion

            return true; 
        }

        public bool SetData()
        {
            try
            {
                
                //cmbwEmpStatus.Text = oPatientOccu.PatientEmploymentStatus;

                txtWLocation.Text = oPatientOccu.PatientPlaceofEmployment;

                //private string _sWorkAddress1 = "";
                txtwAddress1.Text = oPatientOccu.PatientWorkAddress1;

                ////private string _sWorkAddress2 = "";
                //txtwAddress2.Text = oPatientOccu.PatientWorkAddress2;

                ////private string _sWorkCity = "";
                //txtwCity.Text = oPatientOccu.PatientWorkCity;

                ////private string _sWorkState = "";
                //cmbWState.Text = oPatientOccu.PatientWorkState.ToString();

                ////private string _sWorkZip = "";
                //txtwZip.Text = oPatientOccu.PatientWorkZip;

                //txtCounty.Text = oPatientOccu.PatientWorkCounty;

                //cmbCountry.Text = oPatientOccu.PatientWorkCountry;    

                //Sandip Darade   20091021
                //above code replaced by code below to add address information  as we now use gloAddress control 
                oAddresscontrol.txtAddress1.Text = oPatientOccu.PatientWorkAddress1.ToString();
                oAddresscontrol.txtAddress2.Text = oPatientOccu.PatientWorkAddress2.ToString();
                oAddresscontrol.isFormLoading = true;
                oAddresscontrol.txtZip.Text = oPatientOccu.PatientWorkZip.Trim();
                oAddresscontrol.isFormLoading = false;
                oAddresscontrol.txtCity.Text = oPatientOccu.PatientWorkCity.Trim();
                oAddresscontrol.cmbCountry.Text = oPatientOccu.PatientWorkCountry.Trim();
                oAddresscontrol.cmbState.Text = oPatientOccu.PatientWorkState.ToString();
                oAddresscontrol.txtCounty.Text = oPatientOccu.PatientWorkCounty.Trim();
                oPatientOccu.PatientWorkCountry = oAddresscontrol.cmbCountry.Text;

                //private string _sWorkPhone = "";
                mskOIPhone.Text = oPatientOccu.PatientWorkPhone;

                //private string _sWorkFax = "";
                txtOIFax.Text = oPatientOccu.PatientWorkFax;

                mskOIMobile.Text = oPatientOccu.PatientWorkMobile;

                txtOIEmail.Text = oPatientOccu.PatientWorkEmail;

                //Code added on 14 May 2008 by Sagar Ghodke
                txtEmployerName.Text = oPatientOccu.EmployerName;

                //Added By MaheshB

                txtwOccupation.Text = oPatientOccu.Occupation;
                //if (oPatientOccu.RetirementDate != null && oPatientOccu.RetirementDate != DateTime.MinValue)
                //{
                //    dtpRetirementDate.Value = oPatientOccu.RetirementDate;
                //}
               

                cmbStatus.Text = oPatientOccu.PatientEmploymentStatus;
                cmbwEmpStatus.Text = oPatientOccu.EmploymentType;
                //txtwOccupation.Text = oPatientOccu.Occupation;


                if (oPatientOccu.Occupation != "")
                {
                    //Getting OccupationID
                    string query = "SELECT nOccupationID FROM AB_Occupation_MST WHERE sOccupation='" + Convert.ToString(oPatientOccu.Occupation).Replace("'", "''") + "'";
                    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
                    DataTable dt = new DataTable();
                    ODB.Retrive_Query(query, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtwOccupation.Tag = Convert.ToInt64(dt.Rows[0]["nOccupationID"]);
                        //txtwOccupation.Text = oPatientOccu.Occupation; Commented on 20091029 By MaheshB
                        //txtEmployerName.Text = oPatientOccu.EmployerName;
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    ODB.Disconnect();
                    ODB.Dispose();
                    ODB = null;
                    //txtwOccupation.Tag = Convert.ToInt64(dt.Rows[0]["nOccupationID"]);
                }


                //Sandip Darade 20090406
                if (oPatientOccu.RetirementDate != null)
                {
                    mtxtPARetirementDate.Text =Convert.ToDateTime(oPatientOccu.RetirementDate).ToString("MM/dd/yyyy");
                  
                }
                return true;
            }
             catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            return false;
            }
        }

        private void fillEmployeeStatus()
        {
            

            //Employment Status
            cmbStatus.Items.Add("Employed");
            cmbStatus.Items.Add("UnEmployed");
            cmbStatus.Items.Add("Retired");
            cmbStatus.Items.Add("Self-Employed");
            cmbStatus.Items.Add("Student");
            //cmbStatus.Items.Add("Other");
            //
            
            //Employement type
            cmbwEmpStatus.Items.Add("Full Time");
            cmbwEmpStatus.Items.Add("Part Time");
            //


            
        }

        private void fillStates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbWState.DataSource = dtStates ;
                    cmbWState.DisplayMember = "ST";
                     
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        private void fillAutoComplete()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT distinct sEmployerName FROM AB_Occupation_MST order by sEmployerName";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                AutoCompleteStringCollection oEmployeer = new AutoCompleteStringCollection();

                foreach (DataRow dr in dtStates.Rows)
                {
                    oEmployeer.Add(Convert.ToString(dr[0]));
                }
                txtEmployerName.AutoCompleteCustomSource = oEmployeer;
                if (dtStates != null)
                {
                    dtStates.Dispose();
                    dtStates = null;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private bool IsModified()
        {
            bool _result = true; 
            try
            {
                //Commented by Dhruv 20091229
                //if (txtwOccupation.Text.Trim() == oPatientOccu.Occupation
                //   && cmbStatus.Text.ToString() == oPatientOccu.PatientEmploymentStatus
                //   && txtWLocation.Text.Trim() == oPatientOccu.PatientPlaceofEmployment
                //   && txtwAddress1.Text.Trim() == oPatientOccu.PatientWorkAddress1
                //   && txtwAddress2.Text.Trim() == oPatientOccu.PatientWorkAddress2
                //   && txtwCity.Text.Trim() == oPatientOccu.PatientWorkCity
                //   && cmbWState.Text.Trim() == oPatientOccu.PatientWorkState
                //   && txtwZip.Text.Trim() == oPatientOccu.PatientWorkZip
                //   && txtCounty.Text.Trim() == oPatientOccu.PatientWorkCountry
                //   && mskOIPhone.Text.Trim() == oPatientOccu.PatientWorkPhone
                //   && txtOIFax.Text.Trim() == oPatientOccu.PatientWorkFax
                //   && mskOIMobile.Text.Trim() == oPatientOccu.PatientWorkMobile
                //   && txtOIEmail.Text.Trim() == oPatientOccu.PatientWorkEmail
                //   && txtEmployerName.Text.Trim() == oPatientOccu.EmployerName
                //   && cmbwEmpStatus.Text.ToString() == oPatientOccu.EmploymentType)
                    //Dhruv 20091229 
                    //To Check the value 
                    if ( oAddresscontrol.txtAddress1.Text == oPatientOccu.PatientWorkAddress1.ToString()
                         && oAddresscontrol.txtAddress2.Text == oPatientOccu.PatientWorkAddress2.ToString()
                         && oAddresscontrol.txtZip.Text == oPatientOccu.PatientWorkZip.Trim()
                         && oAddresscontrol.txtCity.Text == oPatientOccu.PatientWorkCity.Trim()
                         && oAddresscontrol.cmbCountry.Text == oPatientOccu.PatientWorkCountry.Trim()
                         && oAddresscontrol.cmbState.Text == oPatientOccu.PatientWorkState.ToString()
                         && oAddresscontrol.txtCounty.Text == oPatientOccu.PatientWorkCounty.Trim()
                         && txtwOccupation.Text.Trim() == oPatientOccu.Occupation
                         && cmbStatus.Text.ToString() == oPatientOccu.PatientEmploymentStatus
                         && txtWLocation.Text.Trim() == oPatientOccu.PatientPlaceofEmployment
                         && mskOIPhone.Text.Trim() == oPatientOccu.PatientWorkPhone
                         && txtOIFax.Text.Trim() == oPatientOccu.PatientWorkFax
                         && mskOIMobile.Text.Trim() == oPatientOccu.PatientWorkMobile
                         && txtOIEmail.Text.Trim() == oPatientOccu.PatientWorkEmail
                         && txtEmployerName.Text.Trim() == oPatientOccu.EmployerName
                         && cmbwEmpStatus.Text.ToString() == oPatientOccu.EmploymentType)
                       //&& mtxtPARetirementDate.Text == Convert.ToDateTime(oPatientOccu.RetirementDate).ToString("MM/dd/yyyy"))

                      
                        
                {
                    _result = false; 
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            return _result; 
        }

#endregion


        private void gloPatientOccupation_Load(object sender, EventArgs e)
        {
            //Sandip Darade 20091021
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtOIFax.MaskType = gloMaskControl.gloMaskType.Other;


            } 
            oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
            oAddresscontrol.Dock = DockStyle.Fill;
            pnlAddresssControl.Controls.Add(oAddresscontrol);
            cmbStatus.Select();
            fillEmployeeStatus();
            fillStates();
            SetData();

            fillAutoComplete();
          
            // Added by Sandip dhakane if Employment status is blank then Retirement date should be Disabled.
            if (cmbStatus.Text == "")
            {
                mtxtPARetirementDate.Enabled = false;
            }

           
        }

        private void txtwZip_Leave(object sender, EventArgs e)
        {
            if (txtwZip.Text.Trim() != "" && Regex.IsMatch(txtwZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtwZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbWState.Text =  Convert.ToString(dt.Rows[0]["ST"]);
                        if (txtwCity.Text.Trim() == "")
                            txtwCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                        txtCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        cmbCountry.Text = "US"; 
                    }
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
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
        }

        private void txtwZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEmployerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtEmployerName.Text.Trim() != "")
            {

                DataTable dt = new DataTable();
                //string _result = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT isnull(sOccupation,'') as sOccupation,isnull(sPlaceofEmployment,'') as sPlaceofEmployment,isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as sWorkCity,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(sWorkCountry,'') as sWorkCountry,isnull(sWorkPhone,'') as sWorkPhone,isnull(sWorkMobile,'') as sWorkMobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as  sWorkEmail FROM AB_Occupation_MST WHERE sEmployerName = '" + txtEmployerName.Text.Trim().Replace("'", "''") + "'";
                    oDB.Retrive_Query(_sqlQuery, out  dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtwOccupation.Text = Convert.ToString(dt.Rows[0]["sOccupation"]);
                            txtWLocation.Text = Convert.ToString(dt.Rows[0]["sPlaceofEmployment"]);
                            txtwAddress1.Text = Convert.ToString(dt.Rows[0]["sWorkAddress1"]);
                            txtwAddress2.Text = Convert.ToString(dt.Rows[0]["sWorkAddress2"]);
                            txtwZip.Text = Convert.ToString(dt.Rows[0]["sWorkZip"]);
                            txtwCity.Text = Convert.ToString(dt.Rows[0]["sWorkCity"]);
                            cmbWState.SelectedIndex = cmbWState.FindStringExact(Convert.ToString(dt.Rows[0]["sWorkState"]));
                            txtCounty.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);
                            mskOIPhone.Text = Convert.ToString(dt.Rows[0]["sWorkPhone"]);
                            mskOIMobile.Text = Convert.ToString(dt.Rows[0]["sWorkMobile"]);
                            txtOIFax.Text = Convert.ToString(dt.Rows[0]["sWorkFax"]);
                            txtOIEmail.Text = Convert.ToString(dt.Rows[0]["sWorkEmail"]);
                        }
                        dt.Dispose();
                        dt = null;
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                e.Handled = true;

            }
        }

        private void txtEmployerName_Validated(object sender, EventArgs e)
        {
            if (txtEmployerName.Text.Trim() != "")
            {

                DataTable dt = new DataTable();
                //string _result = "";
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT isnull(sOccupation,'') as sOccupation,isnull(sPlaceofEmployment,'') as sPlaceofEmployment,isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as sWorkCity,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(scountry,'') as sWorkCountry,isnull(sWorkPhone,'') as sWorkPhone,isnull(sWorkMobile,'') as sWorkMobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as  sWorkEmail FROM AB_Occupation_MST WHERE sEmployerName = '" + txtEmployerName.Text.Trim().Replace("'", "''") + "'";
                    oDB.Retrive_Query(_sqlQuery, out  dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (txtwOccupation.Text == "")
                            {
                                txtwOccupation.Text = Convert.ToString(dt.Rows[0]["sOccupation"]);
                                //Getting OccupationID
                                string query = "SELECT nOccupationID FROM AB_Occupation_MST WHERE sOccupation='" + Convert.ToString(txtwOccupation.Text) + "'";
                                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                ODB.Connect(false);
                                DataTable dt1 = new DataTable();
                                ODB.Retrive_Query(query, out dt1);
                                if (dt1 != null && dt1.Rows.Count > 0)
                                {
                                    txtwOccupation.Tag = Convert.ToInt64(dt1.Rows[0]["nOccupationID"]);
                                }
                                else
                                {
                                    txtwOccupation.Tag = null;
                                }
                                if (dt1 != null)
                                {
                                    dt1.Dispose();
                                    dt1 = null;
                                }
                               
                                oDB.Disconnect();
                                oDB.Dispose();
                            }
                            else
                            {
                                txtwOccupation.Tag = null;
                            }
                            
                            //txtwAddress1.Text = Convert.ToString(dt.Rows[0]["sWorkAddress1"]);
                            //txtwAddress2.Text = Convert.ToString(dt.Rows[0]["sWorkAddress2"]);
                            //txtwZip.Text = Convert.ToString(dt.Rows[0]["sWorkZip"]);
                            //txtwCity.Text = Convert.ToString(dt.Rows[0]["sWorkCity"]);
                            //cmbWState.SelectedIndex = cmbWState.FindStringExact(Convert.ToString(dt.Rows[0]["sWorkState"]));
                            //txtCounty.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);
                            try
                            {
                                mskOIPhone.Text = Convert.ToString(dt.Rows[0]["sWorkPhone"]);
                                mskOIMobile.Text = Convert.ToString(dt.Rows[0]["sWorkMobile"]);
                            }
                            catch (Exception)
                            {
                            }
                            txtOIFax.Text = Convert.ToString(dt.Rows[0]["sWorkFax"]);
                            txtOIEmail.Text = Convert.ToString(dt.Rows[0]["sWorkEmail"]);
                            txtWLocation.Text = Convert.ToString(dt.Rows[0]["sPlaceofEmployment"]);


                            oAddresscontrol.txtAddress1.Text = Convert.ToString(dt.Rows[0]["sWorkAddress1"]);
                            oAddresscontrol.txtAddress2.Text = Convert.ToString(dt.Rows[0]["sWorkAddress2"]);
                            oAddresscontrol.isFormLoading = true;
                            oAddresscontrol.txtZip.Text = Convert.ToString(dt.Rows[0]["sWorkZip"]);
                            oAddresscontrol.isFormLoading = false;
                            oAddresscontrol.txtCity.Text = Convert.ToString(dt.Rows[0]["sWorkCity"]);
                            oAddresscontrol.cmbCountry.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);
                            oAddresscontrol.cmbState.Text = Convert.ToString(dt.Rows[0]["sWorkState"]);
                            oAddresscontrol.txtCounty.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);
                            

                        }
                        else
                        {
                            txtwOccupation.Tag = null;
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    DBErr.ERROR_Log(DBErr.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private void tsb_MouseHover(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            ((ToolStripButton)sender).BackgroundImageLayout = ImageLayout.Tile;
        }

        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = null;
        }

        private void btnsaveOccuInfo_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_ButtonHover;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnsaveOccuInfo_MouseLeave(object sender, EventArgs e)
        {           
                ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;            
        }

        //private void cmbEmployerStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cmbEmployerStatus.Text.Trim() == "Retired")
        //        {
        //            dtpRetirementDate.Enabled = true;
        //        }
        //        else
        //        {
        //            dtpRetirementDate.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void cmbwEmpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbStatus.Text.Trim() == "Retired")
                {
                    dtpRetirementDate.Enabled = true;
                    mtxtPARetirementDate.Enabled = true;
                    txtwOccupation.Text = cmbStatus.Text.Trim();
                }
                else
                {
                    dtpRetirementDate.Enabled = false;
                    mtxtPARetirementDate.Enabled = false;
                    if (txtwOccupation.Text.Trim() == "Retired")
                    { txtwOccupation.Text = ""; }
                    
                }

                if (cmbStatus.Text.Trim() == "Employed" || cmbStatus.Text.Trim() == "Student")
                {
                    cmbwEmpStatus.Enabled = true;
                    mtxtPARetirementDate.Text = "";
                }
                else
                {
                    cmbwEmpStatus.Enabled = false;
                }

                //Disable the fields if Employment status is other than Employed or Retired 
                if (cmbStatus.Text.Trim() == "UnEmployed" || cmbStatus.Text.Trim() == "Self-Employed" || cmbStatus.Text.Trim() == "Student" )
                {
                    txtEmployerName.Text = "";
                    txtEmployerName.Enabled=false;
                    txtWLocation.Text = "";
                    txtWLocation.Enabled = false;
                    mtxtPARetirementDate.Text = "";

                }
                else
                {                                       
                    txtEmployerName.Enabled = true;                    
                    txtWLocation.Enabled = true;
                 }

               


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #region "Email Address Validation"

        public bool CheckEmailAddress(string input)
        {
            bool response = false;
            if (Regex.IsMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") || input.Trim() == "")
            {
                response = true;
            }
            else
            {
                response = false;
            }
            return response;
        }

        private void txtOIEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtOIEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mtxtPARetirementDate_Validating(object sender, CancelEventArgs e)
        {
            //mtxtPARetirementDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mtxtPARetirementDate.Text.Length > 0 && mtxtPARetirementDate.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    e.Cancel = true;
            //}
            //else
            //{
            //    if (mtxtPARetirementDate.MaskCompleted == true)
            //    {
            //        try
            //        {
            //            mtxtPARetirementDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            //            if (Convert.ToDateTime(mtxtPARetirementDate.Text).Date >= DateTime.Now.Date)
            //            {
            //                MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                e.Cancel = true;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Please enter a valid date of retirement.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            e.Cancel = true;
            //        }
            //    }
            //}
           
        }

        private void lblEmployerName_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmployerName.Enabled)
            {
                gloAppointmentBook.frmSetupOccupation ofrmSetupOccupation = null;
                try
                {
                    //Int64 OccupationId = 0;
                    //OccupationId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                    ofrmSetupOccupation = new gloAppointmentBook.frmSetupOccupation(0, _databaseconnectionstring);
                    ofrmSetupOccupation.showemployertextbox = true;
                    ofrmSetupOccupation.ShowDialog(this);
                    //Fill_Occupation(0);
                    if (ofrmSetupOccupation != null)
                    {
                        setAfterModify(Convert.ToInt64(ofrmSetupOccupation.ReturnOccupationID),Convert.ToString(ofrmSetupOccupation.OccupationCountry));
                    }
                    fillAutoComplete();

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (ofrmSetupOccupation != null)
                    {
                        ofrmSetupOccupation.Dispose();
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (txtEmployerName.Enabled)
            {
                gloAppointmentBook.frmSetupOccupation ofrmSetupOccupation = null;
                gloDatabaseLayer.DBLayer ODB = null;
                DataTable dt = null;
                try
                {
                    //Int64 OccupationId = 0;


                    if (txtwOccupation.Tag != null && Convert.ToInt64(txtwOccupation.Tag) != 0)
                    {

                        ofrmSetupOccupation = new gloAppointmentBook.frmSetupOccupation(Convert.ToInt64(txtwOccupation.Tag), _databaseconnectionstring);
                        ofrmSetupOccupation.showemployertextbox = cmbwEmpStatus.Enabled;
                        ofrmSetupOccupation.OccupationCountry = cmbCountry.SelectedText;
                        ofrmSetupOccupation.ShowDialog(this);
                        setAfterModify(ofrmSetupOccupation.ReturnOccupationID, Convert.ToString(ofrmSetupOccupation.OccupationCountry));
                    }
                    //OccupationId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                    else
                    {//Check For Both Conditions
                        string _strquery = "SELECT nOccupationID FROM AB_Occupation_MST WHERE sOccupation='" + Convert.ToString(txtwOccupation.Text).Replace("'", "''") + "' and sEmployerName='" + Convert.ToString(txtEmployerName.Text).Replace("'", "''") + "'";
                        ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        ODB.Connect(false);
                        dt = new DataTable();
                        ODB.Retrive_Query(_strquery, out dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt64(dt.Rows[0]["nOccupationID"]) != 0)
                            {

                                ofrmSetupOccupation = new gloAppointmentBook.frmSetupOccupation(Convert.ToInt64(dt.Rows[0]["nOccupationID"]), _databaseconnectionstring);
                                ofrmSetupOccupation.showemployertextbox = cmbwEmpStatus.Enabled;
                                ofrmSetupOccupation.ShowDialog(this);
                                setAfterModify(ofrmSetupOccupation.ReturnOccupationID, Convert.ToString(ofrmSetupOccupation.OccupationCountry));
                            }
                        }
                        else
                        {//Open For New
                            if (txtwOccupation.Text != null && Convert.ToString(txtwOccupation.Text) != "")
                            {

                                ofrmSetupOccupation = new gloAppointmentBook.frmSetupOccupation(0, _databaseconnectionstring, txtwOccupation.Text, txtEmployerName.Text);
                                ofrmSetupOccupation.showemployertextbox = true;
                                ofrmSetupOccupation.ShowDialog(this);
                                setAfterModify(ofrmSetupOccupation.ReturnOccupationID, Convert.ToString(ofrmSetupOccupation.OccupationCountry));
                            }
                        }

                    }
                    //ofrmSetupOccupation.ReturnOccupationID


                    //Fill_Occupation(0);
                    fillAutoComplete();

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (ofrmSetupOccupation != null)
                    {
                        ofrmSetupOccupation.Dispose();
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                        dt = null;
                    }
                    if (ODB != null)
                    {
                        ODB.Disconnect();
                        ODB.Dispose();
                        ODB = null;
                    }
                }
            }
        }

        private void setAfterModify(Int64 _ReturnOccupationID,string sCountry)
        {
            try 
            {
                string query = "SELECT nOccupationID, sOccupation, sEmployerName, sPlaceofEmployment, sWorkAddress1, sWorkAddress2, sWorkCity, sWorkState, sWorkZip, sWorkCountry, sWorkPhone, sWorkMobile, sWorkFax, sWorkEmail, nClinicID FROM AB_Occupation_MST WHERE nOccupationID='" + _ReturnOccupationID + "'";
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                DataTable dt = new DataTable();
                ODB.Retrive_Query(query, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtwOccupation.Tag = Convert.ToInt64(dt.Rows[0]["nOccupationID"]);
                    txtwOccupation.Text = Convert.ToString(dt.Rows[0]["sOccupation"]);
                    txtEmployerName.Text = Convert.ToString(dt.Rows[0]["sEmployerName"]);


                    txtWLocation.Text = Convert.ToString(dt.Rows[0]["sPlaceofEmployment"]);
                    txtwAddress1.Text = Convert.ToString(dt.Rows[0]["sWorkAddress1"]);
                    txtwAddress2.Text = Convert.ToString(dt.Rows[0]["sWorkAddress2"]);
                    txtwZip.Text = Convert.ToString(dt.Rows[0]["sWorkZip"]);
                    txtwCity.Text = Convert.ToString(dt.Rows[0]["sWorkCity"]);
                    cmbWState.SelectedIndex = cmbWState.FindStringExact(Convert.ToString(dt.Rows[0]["sWorkState"]));
                    txtCounty.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);
                    mskOIPhone.Text = Convert.ToString(dt.Rows[0]["sWorkPhone"]);
                    mskOIMobile.Text = Convert.ToString(dt.Rows[0]["sWorkMobile"]);
                    txtOIFax.Text = Convert.ToString(dt.Rows[0]["sWorkFax"]);
                    txtOIEmail.Text = Convert.ToString(dt.Rows[0]["sWorkEmail"]);



                    oAddresscontrol.txtAddress1.Text = Convert.ToString(dt.Rows[0]["sWorkAddress1"]);
                    oAddresscontrol.txtAddress2.Text = Convert.ToString(dt.Rows[0]["sWorkAddress2"]);
                    oAddresscontrol.isFormLoading = true;
                    oAddresscontrol.txtZip.Text = Convert.ToString(dt.Rows[0]["sWorkZip"]);
                    oAddresscontrol.isFormLoading = false;
                    oAddresscontrol.txtCity.Text = Convert.ToString(dt.Rows[0]["sWorkCity"]);
                    oAddresscontrol.cmbCountry.Text = sCountry.Trim();
                    oAddresscontrol.cmbState.Text = Convert.ToString(dt.Rows[0]["sWorkState"]);
                    oAddresscontrol.txtCounty.Text = Convert.ToString(dt.Rows[0]["sWorkCountry"]);


                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                ODB.Disconnect();
               ODB.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void DisposeAllControls()
        {
            try
            {
                if (oPatientOccu != null) { oPatientOccu.Dispose(); }
                if (oAddresscontrol != null) { oAddresscontrol.Dispose(); }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       

    }


}
