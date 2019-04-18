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
    
    public partial class gloPatientGuardianControl : UserControl
    {
        private enum enumZipTextType
        {
            None,
            PatientZip,
            WorkZip,
            MotherZip,
            FatherZip,
            GuardianZip,
            InsuranceZip
        }
        //Start :: ReleationShip Tag
        String searchstr =String.Empty;
        Int64 _nClinicID = 0;
        //End :: ReleationShip Tag
        #region "Constructor And Destructor"


        public gloPatientGuardianControl()
        {
            InitializeComponent();
            _PatientGuardian = new PatientGuardian(_databaseconnectionstring);
            oPatientDemo = new PatientDemographics();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "";
                }
            }
            else
            { _messageboxcaption = ""; }

            #endregion
            //Sandip Darade  20091006
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


        public gloPatientGuardianControl(string ConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = ConnectionString;
            _PatientGuardian = new PatientGuardian(_databaseconnectionstring);
            oPatientDemo = new PatientDemographics();
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
                    _messageboxcaption = "";
                }
            }
            else
            { _messageboxcaption = ""; }

            #endregion

            //Sandip Darade  20091006
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

        #region "Private Variables"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = "gloPM";
        private PatientGuardian _PatientGuardian = null;
        private PatientDemographics oPatientDemo = null;
        private bool _IsInternetFax = false;
        gloAddressControl oMotherAddresscontrol;
        gloAddressControl oFatherAddresscontrol;
        gloAddressControl oGauAddresscontrol;
        #endregion

        #region "Properties & Procedures"

        public PatientGuardian GuardianDetail
        {
            get { return _PatientGuardian; }
            set { _PatientGuardian = value; }
        }

        public PatientDemographics Address
        {
            get { return oPatientDemo; }
            set { oPatientDemo = value; }
        }
        #endregion

        #region Events And Delegates

        public delegate void CloseButtonClick(object sender, EventArgs e);
        public event CloseButtonClick CloseButton_Click;

        public delegate void SaveButtonClick(object sender, EventArgs e);
        public event SaveButtonClick SaveButton_Click;

        #endregion

        #region "Set/Get data, validations "

        //Start :: Guardian ReleationShip filling from PatientReleationship table
        private void fillRelationships()
        {
                       
            RelationShip oRelationShip = new RelationShip(_databaseconnectionstring);
            DataTable dtRelation;
            if (oRelationShip != null)
            {
                dtRelation = oRelationShip.GetList();
                if (dtRelation != null)
                {
                    if (dtRelation.Rows.Count > 0)
                    {
                        DataRow dr = dtRelation.NewRow();
                        dr["sRelationshipCode"] = "";
                        dr["sRelationshipDesc"] = "";
                        dtRelation.Rows.InsertAt(dr, 0);
                        dtRelation.AcceptChanges();
                        //cmbRelationship -- cmbGauInfoRelation -- On PatientDemogrphicControl
                        cmbGauInfoRelation.DataSource = dtRelation;
                        cmbGauInfoRelation.ValueMember = dtRelation.Columns["sRelationshipCode"].ColumnName;
                        cmbGauInfoRelation.DisplayMember = dtRelation.Columns["sRelationshipDesc"].ColumnName;

                        if (dtRelation.Rows.Count > 0)
                        {
                            cmbGauInfoRelation.SelectedIndex = 0;
                        }

                    }
                }
            }
            //


        }
        //End :: Guardian ReleationShip

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

                    cmbGauIMState.DataSource = dtStates.Copy();
                    cmbGauIMState.DisplayMember = "ST";

                    cmbFState.DataSource = dtStates.Copy();
                    cmbFState.DisplayMember = "ST";

                    cmbGauInfoState.DataSource = dtStates.Copy();
                    cmbGauInfoState.DisplayMember = "ST";
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

        //Set data to the controls on the user control
        public bool SetData()
        {
            oMotherAddresscontrol.isFormLoading = true;
            oFatherAddresscontrol.isFormLoading = true;
            oGauAddresscontrol.isFormLoading = true;

            try
            {
                txtGauIMotherfName.Text = _PatientGuardian.PatientMotherFirstName.Trim();
                txtGauIMotherMName.Text = _PatientGuardian.PatientMotherMiddleName.Trim();
                txtGauIMotherLName.Text = _PatientGuardian.PatientMotherLastName.Trim();

                txtGauIMotherMaidenFName.Text = _PatientGuardian.PatientMotherMaidenFirstName.Trim();
                txtGauIMotherMaidenMName.Text = _PatientGuardian.PatientMotherMaidenMiddleName.Trim();
                txtGauIMotherMaidenLName.Text = _PatientGuardian.PatientMotherMaidenLastName.Trim();

                //Sandip Darade 20091009
                if (_PatientGuardian.PatientMotherAddress1.ToString().Trim() != "")
                { oMotherAddresscontrol.txtAddress1.Text = _PatientGuardian.PatientMotherAddress1.Trim(); }
               
                if (_PatientGuardian.PatientMotherAddress2.ToString().Trim() != "")
                { oMotherAddresscontrol.txtAddress2.Text = _PatientGuardian.PatientMotherAddress2.Trim(); }
                
                if (_PatientGuardian.PatientMotherCity.ToString().Trim() != "")
                { oMotherAddresscontrol.txtCity.Text = _PatientGuardian.PatientMotherCity.Trim(); }
                
                if (_PatientGuardian.PatientMotherState.ToString().Trim() != "")
                { oMotherAddresscontrol.cmbState.Text = _PatientGuardian.PatientMotherState.ToString(); }
                
                if (_PatientGuardian.PatientMotherZip.ToString().Trim() != "")
                { oMotherAddresscontrol.txtZip.Text = _PatientGuardian.PatientMotherZip.Trim(); }

                if (_PatientGuardian.PatientMotherCounty.ToString().Trim() != "")
                { oMotherAddresscontrol.txtCounty.Text = _PatientGuardian.PatientMotherCounty.Trim(); }

                if (_PatientGuardian.PatientMotherCountry.ToString().Trim() != "")
                { oMotherAddresscontrol.cmbCountry.Text = _PatientGuardian.PatientMotherCountry.Trim(); }
                
                _PatientGuardian.PatientMotherCountry = oMotherAddresscontrol.cmbCountry.Text;

                mskGauIMPhone.Text = _PatientGuardian.PatientMotherPhone.Trim();
                mskGauIMMobile.Text = _PatientGuardian.PatientMotherMobile.Trim();
                txtGauIMFax.Text = _PatientGuardian.PatientMotherFAX.Trim();
                txtGauiMEmail.Text = _PatientGuardian.PatientMotherEmail.Trim();

                txtGauIFatherfName.Text = _PatientGuardian.PatientFatherFirstName.Trim();
                txtGauIFatherMName.Text = _PatientGuardian.PatientFatherMiddleName.Trim();
                txtGauIFatherLName.Text = _PatientGuardian.PatientFatherLastName.Trim();

                if (_PatientGuardian.PatientFatherAddress1.Trim() != "")
                { oFatherAddresscontrol.txtAddress1.Text = _PatientGuardian.PatientFatherAddress1.Trim(); }

                if (_PatientGuardian.PatientFatherAddress2.Trim() != "")
                { oFatherAddresscontrol.txtAddress2.Text = _PatientGuardian.PatientFatherAddress2.Trim(); }

                if (_PatientGuardian.PatientFatherCity.Trim() != "")
                { oFatherAddresscontrol.txtCity.Text = _PatientGuardian.PatientFatherCity.Trim(); }

                if (_PatientGuardian.PatientFatherState.Trim() != "")
                { oFatherAddresscontrol.cmbState.Text = _PatientGuardian.PatientFatherState.ToString(); }

                if (_PatientGuardian.PatientFatherZip.Trim() != "")
                { oFatherAddresscontrol.txtZip.Text = _PatientGuardian.PatientFatherZip.Trim(); }

                if (_PatientGuardian.PatientFatherCounty.Trim() != "")
                { oFatherAddresscontrol.txtCounty.Text = _PatientGuardian.PatientFatherCounty.Trim(); }

                if (_PatientGuardian.PatientFatherCountry.Trim() != "")
                { oFatherAddresscontrol.cmbCountry.Text = _PatientGuardian.PatientFatherCountry.Trim(); }

                _PatientGuardian.PatientFatherCountry = oFatherAddresscontrol.cmbCountry.Text;

                mskGauIFatherPhone.Text = _PatientGuardian.PatientFatherPhone.Trim();
                mtxtFatherMobile.Text = _PatientGuardian.PatientFatherMobile.Trim();
                txtGauIFatherFax.Text = _PatientGuardian.PatientFatherFAX.Trim();
                txtGauIFatherEmail.Text = _PatientGuardian.PatientFatherEmail.Trim();
                
                txtGauInfoFName.Text = _PatientGuardian.PatientGuardianFirstName.Trim();
                txtGauInfoMName.Text = _PatientGuardian.PatientGuardianMiddleName.Trim();
                txtGauInfoLName.Text = _PatientGuardian.PatientGuardianLastName.Trim();

                if (_PatientGuardian.PatientGuardianAddress1.Trim() != "")
                { oGauAddresscontrol.txtAddress1.Text = _PatientGuardian.PatientGuardianAddress1.Trim(); }

                if (_PatientGuardian.PatientGuardianAddress2.Trim() != "")
                { oGauAddresscontrol.txtAddress2.Text = _PatientGuardian.PatientGuardianAddress2.Trim(); }

                if (_PatientGuardian.PatientGuardianCity.Trim() != "")
                { oGauAddresscontrol.txtCity.Text = _PatientGuardian.PatientGuardianCity.Trim(); }

                if (_PatientGuardian.PatientGuardianState.Trim() != "")
                { oGauAddresscontrol.cmbState.Text = _PatientGuardian.PatientGuardianState.ToString(); }

                if (_PatientGuardian.PatientGuardianZip.Trim() != "")
                { oGauAddresscontrol.txtZip.Text = _PatientGuardian.PatientGuardianZip.Trim(); }

                if (_PatientGuardian.PatientGuardianCounty.Trim() != "")
                { oGauAddresscontrol.txtCounty.Text = _PatientGuardian.PatientGuardianCounty.Trim(); }

                if (_PatientGuardian.PatientGuardianCountry.Trim() != "")
                { oGauAddresscontrol.cmbCountry.Text = _PatientGuardian.PatientGuardianCountry.Trim(); }

                _PatientGuardian.PatientGuardianCountry = oGauAddresscontrol.cmbCountry.Text;

                mskGauInfoPhone.Text = _PatientGuardian.PatientGuardianPhone.Trim();
                mskGauInfoMobile.Text = _PatientGuardian.PatientGuardianMobile.Trim();
                txtGauInfoFax.Text = _PatientGuardian.PatientGuardianFAX.Trim();
                txtGauInfoEmail.Text = _PatientGuardian.PatientGuardianEmail.Trim();

                //Start :: Patient Releationship
                if (cmbGauInfoRelation != null)
                {
                   //cmbGauInfoRelation.SelectedValue = _PatientGuardian.PatientGuardianRelationCD.Trim();
                   cmbGauInfoRelation.Text = _PatientGuardian.PatientGuardianRelationDS.Trim();
                   //  cmbGauInfoRelation.SelectedIndex = cmbGauInfoRelation.FindString (cmbGauInfoRelation.Text,0);
                }
                //End :: Patient Releationship

                oMotherAddresscontrol.isFormLoading = false;
                oFatherAddresscontrol.isFormLoading = false;
                oGauAddresscontrol.isFormLoading = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return true;
        }

        //Get data for the controls on the User control from database
        public bool GetData()
        {

            try
            {
                if (ValidateData() == false)
                {
                    return false;
                }

                _PatientGuardian.PatientMotherFirstName = txtGauIMotherfName.Text;

                _PatientGuardian.PatientMotherMiddleName = txtGauIMotherMName.Text;

                _PatientGuardian.PatientMotherLastName = txtGauIMotherLName.Text;


                _PatientGuardian.PatientMotherMaidenFirstName = txtGauIMotherMaidenFName.Text;

                _PatientGuardian.PatientMotherMaidenMiddleName = txtGauIMotherMaidenMName.Text;

                _PatientGuardian.PatientMotherMaidenLastName = txtGauIMotherMaidenLName.Text;

                //Sandip Darade 200901009 
                //_PatientGuardian.PatientMotherAddress1 = txtGauIMotherAddress1.Text;

                //_PatientGuardian.PatientMotherAddress2 = txtGauIMotherAddress2.Text;

                //_PatientGuardian.PatientMotherCity = txtGauIMotherCity.Text;

                //_PatientGuardian.PatientMotherState = cmbGauIMState.Text;

                //_PatientGuardian.PatientMotherZip = txtGauIMotherZip.Text;

                //_PatientGuardian.PatientMotherCounty = txtGauIMCounty.Text;
                //_PatientGuardian.PatientMotherCountry = cmbGauIMCountry.Text.Trim();   

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                _PatientGuardian.PatientMotherAddress1 = oMotherAddresscontrol.txtAddress1.Text.Trim();
                _PatientGuardian.PatientMotherAddress2 = oMotherAddresscontrol.txtAddress2.Text.Trim();
                _PatientGuardian.PatientMotherCity = oMotherAddresscontrol.txtCity.Text.Trim();
                _PatientGuardian.PatientMotherState = oMotherAddresscontrol.cmbState.Text.Trim();
                _PatientGuardian.PatientMotherZip = oMotherAddresscontrol.txtZip.Text.Trim();
                _PatientGuardian.PatientMotherCounty = oMotherAddresscontrol.txtCounty.Text.Trim();
                _PatientGuardian.PatientMotherCountry = oMotherAddresscontrol.cmbCountry.Text.Trim();


                _PatientGuardian.PatientMotherPhone = mskGauIMPhone.Text;

                _PatientGuardian.PatientMotherMobile = mskGauIMMobile.Text;

                _PatientGuardian.PatientMotherFAX = txtGauIMFax.Text;

                _PatientGuardian.PatientMotherEmail = txtGauiMEmail.Text;

                _PatientGuardian.PatientFatherFirstName = txtGauIFatherfName.Text;

                _PatientGuardian.PatientFatherMiddleName = txtGauIFatherMName.Text;

                _PatientGuardian.PatientFatherLastName = txtGauIFatherLName.Text;

                //_PatientGuardian.PatientFatherAddress1 = txtGauIFatherAddress1.Text;

                //_PatientGuardian.PatientFatherAddress2 = txtGauIFatherAddress2.Text;

                //_PatientGuardian.PatientFatherCity = txtGauIFatherCity.Text;

                //_PatientGuardian.PatientFatherState = cmbFState.Text;

                //_PatientGuardian.PatientFatherZip = txtGauIFatherZip.Text;

                //_PatientGuardian.PatientFatherCounty = txtGauIFatherCounty.Text;
                //_PatientGuardian.PatientFatherCountry = cmbFCountry.Text.Trim();    
                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                _PatientGuardian.PatientFatherAddress1 = oFatherAddresscontrol.txtAddress1.Text.Trim();
                _PatientGuardian.PatientFatherAddress2 = oFatherAddresscontrol.txtAddress2.Text.Trim();
                _PatientGuardian.PatientFatherCity = oFatherAddresscontrol.txtCity.Text.Trim();
                _PatientGuardian.PatientFatherState = oFatherAddresscontrol.cmbState.Text.Trim();
                _PatientGuardian.PatientFatherZip = oFatherAddresscontrol.txtZip.Text.Trim();
                _PatientGuardian.PatientFatherCounty = oFatherAddresscontrol.txtCounty.Text.Trim();
                _PatientGuardian.PatientFatherCountry = oFatherAddresscontrol.cmbCountry.Text.Trim();


                _PatientGuardian.PatientFatherPhone = mskGauIFatherPhone.Text;

                _PatientGuardian.PatientFatherMobile = mtxtFatherMobile.Text;

                _PatientGuardian.PatientFatherFAX = txtGauIFatherFax.Text;

                _PatientGuardian.PatientFatherEmail = txtGauIFatherEmail.Text;

                _PatientGuardian.PatientGuardianFirstName = txtGauInfoFName.Text;

                _PatientGuardian.PatientGuardianMiddleName = txtGauInfoMName.Text;

                _PatientGuardian.PatientGuardianLastName = txtGauInfoLName.Text;

                //_PatientGuardian.PatientGuardianAddress1 = txtGauInfoAddress1.Text;

                //_PatientGuardian.PatientGuardianAddress2 = txtGauInfoAddress2.Text;

                //_PatientGuardian.PatientGuardianCity = txtGauInfoCity.Text;

                //_PatientGuardian.PatientGuardianState = cmbGauInfoState.Text;

                //_PatientGuardian.PatientGuardianZip = txtGauInfoZip.Text;

                //_PatientGuardian.PatientGuardianCounty = txtGauInfoCounty.Text;
                //_PatientGuardian.PatientGuardianCountry = cmbGauInfoCountry.Text.Trim();    
                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                _PatientGuardian.PatientGuardianAddress1 = oGauAddresscontrol.txtAddress1.Text.Trim();
                _PatientGuardian.PatientGuardianAddress2 = oGauAddresscontrol.txtAddress2.Text.Trim();
                _PatientGuardian.PatientGuardianCity = oGauAddresscontrol.txtCity.Text.Trim();
                _PatientGuardian.PatientGuardianState = oGauAddresscontrol.cmbState.Text.Trim();
                _PatientGuardian.PatientGuardianZip = oGauAddresscontrol.txtZip.Text.Trim();
                _PatientGuardian.PatientGuardianCounty = oGauAddresscontrol.txtCounty.Text.Trim();
                _PatientGuardian.PatientGuardianCountry = oGauAddresscontrol.cmbCountry.Text.Trim();

                _PatientGuardian.PatientGuardianPhone = mskGauInfoPhone.Text;

                _PatientGuardian.PatientGuardianMobile = mskGauInfoMobile.Text;

                _PatientGuardian.PatientGuardianFAX = txtGauInfoFax.Text;

                _PatientGuardian.PatientGuardianEmail = txtGauInfoEmail.Text;


                //Start :: Patient Guardian ReleationShip
                if (cmbGauInfoRelation != null)
                {
                    if (cmbGauInfoRelation.SelectedIndex != -1)
                    {
                        _PatientGuardian.PatientGuardianRelationCD = cmbGauInfoRelation.SelectedValue.ToString();
                        _PatientGuardian.PatientGuardianRelationDS = cmbGauInfoRelation.Text.ToString();
                    }
                    else
                    {
                        _PatientGuardian.PatientGuardianRelationDS = cmbGauInfoRelation.Text.ToString();
                    }
                }
                //End :: Patient Guardian ReleationShip

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return true;
        }

        private bool ValidateData()
        {
            //Incomplete Mother Phone Numbers
            //mskGauIMPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskGauIMPhone.Text.Length > 0 && mskGauIMPhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for mother's phone.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskGauIMPhone.Focus();
            //    return false;
            //}
            if (mskGauIMPhone.IsValidated == false)
            {
                mskGauIMPhone.Focus();
                return false;
            }
            if (mskGauIMMobile.IsValidated == false)
            {
                mskGauIMPhone.Focus();
                return false;
            }

            ////Incomplete Father Phone Numbers
            //mskGauIFatherPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskGauIFatherPhone.Text.Length > 0 && mskGauIFatherPhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for father's phone.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskGauIFatherPhone.Focus();
            //    return false;
            //}
            if (mskGauIFatherPhone.IsValidated == false)
            {
                mskGauIFatherPhone.Focus();
                return false;
            }
            ////Incomplete Gaurdian Phone Numbers
            //mskGauInfoPhone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskGauInfoPhone.Text.Length > 0 && mskGauInfoPhone.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for guardian's phone.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskGauInfoPhone.Focus();
            //    return false;
            //}
            if (mskGauInfoPhone.IsValidated == false)
            {
                mskGauInfoPhone.Focus();
                return false;
            }
            ////Incomplete Mother Mobile Number 
            //mskGauIMMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskGauIMMobile.Text.Length > 0 && mskGauIMMobile.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for mother's mobile.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskGauIMMobile.Focus();
            //    return false;
            //}
            if (mskGauIMMobile.IsValidated == false)
            {
                mskGauIMMobile.Focus();
                return false;
            }
            ////Incomplete Father Mobile Number 
            //mtxtFatherMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mtxtFatherMobile.Text.Length > 0 && mtxtFatherMobile.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for father's mobile.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtFatherMobile.Focus();
            //    return false;
            //}
            if (mtxtFatherMobile.IsValidated == false)
            {
                mtxtFatherMobile.Focus();
                return false;
            }
            ////Incomplete Mobile Number 
            //mskGauInfoMobile.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //if (mskGauInfoMobile.Text.Length > 0 && mskGauInfoMobile.MaskCompleted == false)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for guardian's mobile.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mskGauInfoMobile.Focus();
            //    return false;
            //}
            if (mskGauInfoMobile.IsValidated == false)
            {
                mskGauInfoMobile.Focus();
                return false;
            }
            //Validations for email address

            if (CheckEmailAddress(txtGauiMEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGauiMEmail.Focus();
                return false;
            }
            if (CheckEmailAddress(txtGauIFatherEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGauIFatherEmail.Focus();
                return false;
            }
            if (CheckEmailAddress(txtGauInfoEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGauInfoEmail.Focus();
                return false;
            }
            if (cmbGauInfoRelation.Text.Trim() != "")
            {
                if (txtGauInfoFName.Text.Trim() == "" || txtGauInfoFName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please enter atleast guardian name.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGauInfoEmail.Focus();
                    return false;
                }
            }
            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            if (txtGauIMotherfName.Text.Trim().ToString() != "")
            {
                if (txtGauIMotherLName.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("Enter Mother last name. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGauIMotherLName.Focus();
                    return false;
                }

                //if (oMotherAddresscontrol.txtAddress1.Text == "")
                //{
                //    if (MessageBox.Show("Mothers address is missing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oMotherAddresscontrol.txtAddress1.Focus();
                //        return false;
                //    }
                //    else
                //        return true;

                //}

                //if (oMotherAddresscontrol.txtCity.Text == "")
                //{
                //    if (MessageBox.Show("Mother city is missing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oMotherAddresscontrol.txtCity.Focus();
                //        return false;
                //    }
                //    else
                //        return true;
                //}

            }
            //father
            if (txtGauIFatherfName.Text.Trim().ToString() != "")
            {
                if (txtGauIFatherLName.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("Enter Father last name. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGauIFatherLName.Focus();
                    return false;
                }
                //if (oFatherAddresscontrol.txtAddress1.Text == "")
                //{
                //    if (MessageBox.Show("Father address is missing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oFatherAddresscontrol.txtAddress1.Focus();
                //        return false;
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
                //if (oFatherAddresscontrol.txtCity.Text == "")
                //{
                //    if (MessageBox.Show("Father city is missing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oFatherAddresscontrol.txtCity.Focus();
                //        return false;
                //    }
                //    else
                //        return true;
                //}
            }
            //other gaurdian
            if (txtGauInfoFName.Text.Trim().ToString() != "")
            {
                if (txtGauInfoLName.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("Enter Gaurdian last name.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGauInfoLName.Focus();
                    return false;
                }
                //if (oGauAddresscontrol.txtAddress1.Text == "")
                //{
                //    if (MessageBox.Show("Gaurdian address is missing. Do you want to continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oGauAddresscontrol.txtAddress1.Focus();
                //        return false;
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
                //if (oGauAddresscontrol.txtCity.Text == "")
                //{
                //    if (MessageBox.Show("Gaurdian zip is missing. Do you want to Continue?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                //    {
                //        oGauAddresscontrol.txtCity.Focus();
                //        return false;
                //    }
                //    else
                //        return true;
                //}
            }

            return true;
        }

        private bool IsModified()
        {
            bool _Result = true;  
            try
            {
                //if (_PatientGuardian.PatientMotherFirstName == txtGauIMotherfName.Text
                //    && _PatientGuardian.PatientMotherMiddleName == txtGauIMotherMName.Text
                //    && _PatientGuardian.PatientMotherLastName == txtGauIMotherLName.Text
                //    && _PatientGuardian.PatientMotherAddress1 == txtGauIMotherAddress1.Text
                //    && _PatientGuardian.PatientMotherAddress2 == txtGauIMotherAddress2.Text
                //    && _PatientGuardian.PatientMotherCity == txtGauIMotherCity.Text
                //    && _PatientGuardian.PatientMotherState == cmbGauIMState.Text
                //    && _PatientGuardian.PatientMotherZip == txtGauIMotherZip.Text
                //    && _PatientGuardian.PatientMotherCountry == txtGauIMCounty.Text
                //    && _PatientGuardian.PatientMotherPhone == mskGauIMPhone.Text
                //    && _PatientGuardian.PatientMotherMobile == mskGauIMMobile.Text
                //    && _PatientGuardian.PatientMotherFAX == txtGauIMFax.Text
                //    && _PatientGuardian.PatientMotherEmail == txtGauiMEmail.Text
                //    && _PatientGuardian.PatientFatherFirstName == txtGauIFatherfName.Text
                //    && _PatientGuardian.PatientFatherMiddleName == txtGauIFatherMName.Text
                //    && _PatientGuardian.PatientFatherLastName == txtGauIFatherLName.Text
                //    && _PatientGuardian.PatientFatherAddress1 == txtGauIFatherAddress1.Text
                //    && _PatientGuardian.PatientFatherAddress2 == txtGauIFatherAddress2.Text
                //    && _PatientGuardian.PatientFatherCity == txtGauIFatherCity.Text
                //    && _PatientGuardian.PatientFatherState == cmbFState.Text
                //    && _PatientGuardian.PatientFatherZip == txtGauIFatherZip.Text
                //    && _PatientGuardian.PatientFatherCountry == txtGauIFatherCounty.Text
                //    && _PatientGuardian.PatientFatherPhone == mskGauIFatherPhone.Text
                //    && _PatientGuardian.PatientFatherMobile == mtxtFatherMobile.Text
                //    && _PatientGuardian.PatientFatherFAX == txtGauIFatherFax.Text
                //    && _PatientGuardian.PatientFatherEmail == txtGauIFatherEmail.Text
                //    && _PatientGuardian.PatientGuardianFirstName == txtGauInfoFName.Text
                //    && _PatientGuardian.PatientGuardianMiddleName == txtGauInfoMName.Text
                //    && _PatientGuardian.PatientGuardianLastName == txtGauInfoLName.Text
                //    && _PatientGuardian.PatientGuardianAddress1 == txtGauInfoAddress1.Text
                //    && _PatientGuardian.PatientGuardianAddress2 == txtGauInfoAddress2.Text
                //    && _PatientGuardian.PatientGuardianCity == txtGauInfoCity.Text
                //    && _PatientGuardian.PatientGuardianState == cmbGauInfoState.Text
                //    && _PatientGuardian.PatientGuardianZip == txtGauInfoZip.Text
                //    && _PatientGuardian.PatientGuardianCountry == txtGauInfoCounty.Text
                //    && _PatientGuardian.PatientGuardianPhone == mskGauInfoPhone.Text
                //    && _PatientGuardian.PatientGuardianMobile == mskGauInfoMobile.Text
                //    && _PatientGuardian.PatientGuardianFAX == txtGauInfoFax.Text
                //    && _PatientGuardian.PatientGuardianEmail == txtGauInfoEmail.Text
                //   )
                //Mother
                if ( oMotherAddresscontrol.txtAddress1.Text  == _PatientGuardian.PatientMotherAddress1.Trim()
                    &&  oMotherAddresscontrol.txtAddress2.Text == _PatientGuardian.PatientMotherAddress2.Trim()
                    && oMotherAddresscontrol.txtCity.Text == _PatientGuardian.PatientMotherCity.Trim()
                    && oMotherAddresscontrol.cmbState.Text == _PatientGuardian.PatientMotherState.ToString()
                    && oMotherAddresscontrol.txtZip.Text == _PatientGuardian.PatientMotherZip.Trim()
                    && oMotherAddresscontrol.txtCounty.Text == _PatientGuardian.PatientMotherCounty.Trim()
                    && oMotherAddresscontrol.cmbCountry.Text == _PatientGuardian.PatientMotherCountry.Trim()
                    && _PatientGuardian.PatientMotherFirstName == txtGauIMotherfName.Text
                    && _PatientGuardian.PatientMotherMiddleName == txtGauIMotherMName.Text
                    && _PatientGuardian.PatientMotherLastName == txtGauIMotherLName.Text
                    && _PatientGuardian.PatientMotherMaidenFirstName == txtGauIMotherMaidenFName.Text
                    && _PatientGuardian.PatientMotherMaidenMiddleName == txtGauIMotherMaidenMName.Text
                    && _PatientGuardian.PatientMotherMaidenLastName == txtGauIMotherMaidenLName.Text
                    && _PatientGuardian.PatientMotherPhone == mskGauIMPhone.Text
                    && _PatientGuardian.PatientMotherMobile == mskGauIMMobile.Text
                    && _PatientGuardian.PatientMotherFAX == txtGauIMFax.Text
                    && _PatientGuardian.PatientMotherEmail == txtGauiMEmail.Text

                    //Father
                    && oFatherAddresscontrol.txtAddress1.Text == _PatientGuardian.PatientFatherAddress1.Trim()
                    && oFatherAddresscontrol.txtAddress2.Text == _PatientGuardian.PatientFatherAddress2.Trim()
                    && oFatherAddresscontrol.txtCity.Text == _PatientGuardian.PatientFatherCity.Trim()
                    && oFatherAddresscontrol.cmbState.Text == _PatientGuardian.PatientFatherState.ToString()
                    && oFatherAddresscontrol.txtZip.Text == _PatientGuardian.PatientFatherZip.Trim()
                    && oFatherAddresscontrol.txtCounty.Text == _PatientGuardian.PatientFatherCounty.Trim()
                    && oFatherAddresscontrol.cmbCountry.Text == _PatientGuardian.PatientFatherCountry.Trim()
                    && _PatientGuardian.PatientFatherPhone == mskGauIFatherPhone.Text
                    && _PatientGuardian.PatientFatherMobile == mtxtFatherMobile.Text
                    && _PatientGuardian.PatientFatherFAX == txtGauIFatherFax.Text
                    && _PatientGuardian.PatientFatherEmail == txtGauIFatherEmail.Text
                    && _PatientGuardian.PatientFatherFirstName == txtGauIFatherfName.Text
                    && _PatientGuardian.PatientFatherMiddleName == txtGauIFatherMName.Text
                    && _PatientGuardian.PatientFatherLastName == txtGauIFatherLName.Text

                    //gaurdian information.
                    && oGauAddresscontrol.txtAddress1.Text == _PatientGuardian.PatientGuardianAddress1 .Trim()
                    && oGauAddresscontrol.txtAddress2.Text == _PatientGuardian.PatientGuardianAddress2.Trim()
                    && oGauAddresscontrol.txtCity.Text == _PatientGuardian.PatientGuardianCity.Trim()
                    && oGauAddresscontrol.cmbState.Text == _PatientGuardian.PatientGuardianState.ToString()
                    && oGauAddresscontrol.txtZip.Text == _PatientGuardian.PatientGuardianZip.Trim()
                    && oGauAddresscontrol.txtCounty.Text == _PatientGuardian.PatientGuardianCounty.Trim()
                    && oGauAddresscontrol.cmbCountry.Text == _PatientGuardian.PatientGuardianCountry.Trim()
                    && _PatientGuardian.PatientGuardianFirstName == txtGauInfoFName.Text
                    && _PatientGuardian.PatientGuardianMiddleName == txtGauInfoMName.Text
                    && _PatientGuardian.PatientGuardianLastName == txtGauInfoLName.Text
                    && _PatientGuardian.PatientGuardianPhone == mskGauInfoPhone.Text
                    && _PatientGuardian.PatientGuardianMobile == mskGauInfoMobile.Text
                    && _PatientGuardian.PatientGuardianFAX == txtGauInfoFax.Text
                    && _PatientGuardian.PatientGuardianEmail == txtGauInfoEmail.Text
                    )

                {
                    _Result = false; 
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            return _Result ; 
        }

        #endregion


        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            if (GetData() == true)
                                SaveButton_Click(sender, e);
                            break;
                        }
                    case "Cancel":
                        {
                            if (IsModified() == false)
                                CloseButton_Click(sender, e);
                            else
                            {
                                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (res == DialogResult.Yes)
                                {
                                    btnSaveGuardianInfo_Click(null, null);
                                }
                                else if (res == DialogResult.No)
                                {
                                    CloseButton_Click(sender, e);
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //On save button click
        private void btnSaveGuardianInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if(GetData() == true) 
                    SaveButton_Click(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //On close button click
        private void btnGIClose_Click(object sender, EventArgs e)
        {
            
        }

        //On control load set data to different controls
        private void gloPatientGuardian_Load(object sender, EventArgs e)
        {
            //Sandip Darade 20091006
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtGauIMFax.MaskType = gloMaskControl.gloMaskType.Other;
                txtGauIFatherFax.MaskType = gloMaskControl.gloMaskType.Other;
                txtGauInfoFax.MaskType = gloMaskControl.gloMaskType.Other;

            }
            oMotherAddresscontrol = new gloAddressControl(_databaseconnectionstring, false);
            oMotherAddresscontrol.Dock = DockStyle.Fill;
            pnlMthersAdds.Controls.Add(oMotherAddresscontrol);

            oFatherAddresscontrol = new gloAddressControl(_databaseconnectionstring, false);
            oFatherAddresscontrol.Dock = DockStyle.Fill;
            pnlFthersAdds.Controls.Add(oFatherAddresscontrol);

            oGauAddresscontrol = new gloAddressControl(_databaseconnectionstring, false);
            oGauAddresscontrol.Dock = DockStyle.Fill;
            pnlGuaiAdds.Controls.Add(oGauAddresscontrol);


            try
            {
                isFormLoading = true;
                //pnlGI.Visible = true;
                //pnlGI.BringToFront();
                fillStates();



                //Start :: Timer For the Search String
                timer1.Interval = 1000;
                timer1.Start();
                //End :: Timer For the Search String

                #region " Retrieve clinicID from AppSettings "
              //Start :: Clinic ID from the ReleationShip
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _nClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _nClinicID = 1; }
                }
                else
                {
                    _nClinicID = 1;
                }
                //End :: Clinic ID from the ReleationShip  
                #endregion
                //Start :: Releationship
                fillRelationships();
                //End:: Releationship

                //pnlGI.Dock = DockStyle.Top;
                SetData();
                
                txtGauIMotherfName.Focus();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                isFormLoading = false;
 
            }
        }

        private void pnlGI_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtGauIMotherZip_Leave(object sender, EventArgs e)
        {
            if (txtGauIMotherZip.Text.Trim() != "" && Regex.IsMatch(txtGauIMotherZip.Text.Trim(), @"^[0-9]+$") == true)
            {
               
                DataTable dt = getAddress(txtGauIMotherZip.Text.Trim());
                if (dt != null && dt.Rows.Count > 0 )
                {
                    cmbGauIMState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                    if (txtGauIMotherCity.Text.Trim() == "")
                        txtGauIMotherCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                    txtGauIMCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                    cmbGauIMCountry.Text = "US"; 
                }
            }
        }

        private void txtGauIFatherZip_Leave(object sender, EventArgs e)
        {
            if (txtGauIFatherZip.Text.Trim() != "" && Regex.IsMatch(txtGauIFatherZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = getAddress(txtGauIFatherZip.Text.Trim());
                if (dt != null && dt.Rows.Count > 0 )
                {
                    cmbFState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                    if (txtGauIFatherCity.Text.Trim() == "")
                        txtGauIFatherCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                    txtGauIFatherCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                    cmbFCountry.Text = "US"; 
                }
            }
        }

        private void txtGauInfoZip_Leave(object sender, EventArgs e)
        {
            if (txtGauInfoZip.Text.Trim() != "" && Regex.IsMatch(txtGauInfoZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = getAddress(txtGauInfoZip.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbGauInfoState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                    if (txtGauInfoCity.Text.Trim() == "")
                        txtGauInfoCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                    txtGauInfoCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                    cmbGauInfoCountry.Text = "US"; 
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        private DataTable getAddress(string ZipCode)
        {
            DataTable dt = new System.Data.DataTable();
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDb.Connect(false);
                string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + ZipCode + "";
                oDb.Retrive_Query(qry, out dt);
                //if (dt != null)
                //{
                //    return dt;
                //}
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                //return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //return null;
            }
            finally
            {
                //dt.Dispose();
                oDb.Disconnect();
                oDb.Dispose();
            }
            return dt;
        }

        private void chkGauIAddrforMother_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkGauIAddrforMother.Checked == true)
            //{
            //    txtGauIMotherAddress1.Text = Address.PatientAddress1;
            //    txtGauIMotherAddress2.Text = Address.PatientAddress2;
            //    txtGauIMotherCity.Text = Address.PatientCity;
            //    _isTextBoxLoading = true;
            //    txtGauIMotherZip.Text = Address.PatientZip;
            //    _isTextBoxLoading = false;
            //    txtGauIMCounty.Text = Address.PatientCounty;
            //    cmbGauIMState.Text = Address.PatientState;
            //    cmbGauIMCountry.Text = Address.PatientCountry; 
            //}
            //else           
            //{
            //    txtGauIMotherAddress1.Text = "";
            //    txtGauIMotherAddress2.Text = "";
            //    txtGauIMotherCity.Text = "";
            //    _isTextBoxLoading = true;
            //    txtGauIMotherZip.Text = "";
            //    _isTextBoxLoading = false;
            //    txtGauIMCounty.Text = "";
            //    cmbGauIMState.Text = "";
            //    cmbGauIMCountry.Text = "";
            //    cmbGauIMCountry.Text = "";
            //}
            //Sandip Darade 20091009
            if (chkGauIAddrforMother.Checked == true)
            {
                oMotherAddresscontrol.txtAddress1.Text = Address.PatientAddress1;
                oMotherAddresscontrol.txtAddress2.Text = Address.PatientAddress2;
                oMotherAddresscontrol.txtCity.Text = Address.PatientCity;
                oMotherAddresscontrol._isTextBoxLoading = true;
                oMotherAddresscontrol.txtZip.Text = Address.PatientZip;
                oMotherAddresscontrol. _isTextBoxLoading = false;
                oMotherAddresscontrol.txtCounty.Text = Address.PatientCounty;
                oMotherAddresscontrol.cmbState.Text = Address.PatientState;
                oMotherAddresscontrol.cmbCountry.Text = Address.PatientCountry;
            }
            else
            {
                oMotherAddresscontrol.txtAddress1.Text = "";
                oMotherAddresscontrol.txtAddress2.Text = "";
                oMotherAddresscontrol.txtCity.Text = "";
                oMotherAddresscontrol._isTextBoxLoading = true;
                oMotherAddresscontrol.txtZip.Text = "";
                oMotherAddresscontrol._isTextBoxLoading = false;
                oMotherAddresscontrol.txtCounty.Text = "";
                oMotherAddresscontrol.cmbState.Text = "";
                oMotherAddresscontrol.cmbCountry.Text = "";
            }
        }

        private void chkGauIFatherAddress_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkGauIFatherAddress.Checked == true)
            //{
            //    txtGauIFatherAddress1.Text = Address.PatientAddress1;
            //    txtGauIFatherAddress2.Text = Address.PatientAddress2;
            //    txtGauIFatherCity.Text = Address.PatientCity;
            //    _isTextBoxLoading = true;
            //    txtGauIFatherZip.Text = Address.PatientZip;
            //    _isTextBoxLoading = false;
            //    txtGauIFatherCounty.Text = Address.PatientCounty;
            //    cmbFState.Text = Address.PatientState.ToString();
            //    cmbFCountry.Text = Address.PatientCountry; 
            //}
            //else
            //{
            //    txtGauIFatherAddress1.Text = "";
            //    txtGauIFatherAddress2.Text = "";
            //    txtGauIFatherCity.Text = "";
            //    _isTextBoxLoading = true;
            //    txtGauIFatherZip.Text = "";
            //    _isTextBoxLoading = false;
            //    txtGauIFatherCounty.Text = "";
            //    cmbFState.Text = "";
            //    cmbFCountry.Text = "";
            //}
            //Sandip Darade 20091009
            if (chkGauIFatherAddress.Checked == true)
            {
                oFatherAddresscontrol.txtAddress1.Text = Address.PatientAddress1;
                oFatherAddresscontrol.txtAddress2.Text = Address.PatientAddress2;
                oFatherAddresscontrol.txtCity.Text = Address.PatientCity;
                oFatherAddresscontrol._isTextBoxLoading = true;
                oFatherAddresscontrol.txtZip.Text = Address.PatientZip;
                oFatherAddresscontrol._isTextBoxLoading = false;
                oFatherAddresscontrol.txtCounty.Text = Address.PatientCounty;
                oFatherAddresscontrol.cmbState.Text = Address.PatientState;
                oFatherAddresscontrol.cmbCountry.Text = Address.PatientCountry;
            }
            else
            {
                oFatherAddresscontrol.txtAddress1.Text = "";
                oFatherAddresscontrol.txtAddress2.Text = "";
                oFatherAddresscontrol.txtCity.Text = "";
                oFatherAddresscontrol._isTextBoxLoading = true;
                oFatherAddresscontrol.txtZip.Text = "";
                oFatherAddresscontrol._isTextBoxLoading = false;
                oFatherAddresscontrol.txtCounty.Text = "";
                oFatherAddresscontrol.cmbState.Text = "";
                oFatherAddresscontrol.cmbCountry.Text = "";
            }
        }

        private void cb_AddrforGuardian_CheckedChanged(object sender, EventArgs e)
        {
           
            //if (cb_AddrforGuardian.Checked == true)
                
            //{
               
            //    txtGauInfoAddress1.Text = Address.PatientAddress1;
            //    txtGauInfoAddress2.Text = Address.PatientAddress2;
            //    txtGauInfoCity.Text = Address.PatientCity;
            //    _isTextBoxLoading = true;
            //    txtGauInfoZip.Text = Address.PatientZip;
            //    _isTextBoxLoading = false;
            //    txtGauInfoCounty.Text = Address.PatientCounty;
            //    cmbGauInfoState.Text = Address.PatientState.ToString();
            //    cmbGauInfoCountry.Text = Address.PatientCountry;  
            //}
            //else
            //{
               
            //    txtGauInfoAddress1.Text = "";
            //    txtGauInfoAddress2.Text = "";
            //    txtGauInfoCity.Text = "";
            //    _isTextBoxLoading = true;
            //    txtGauInfoZip.Text = "";
            //    _isTextBoxLoading = false;
            //    txtGauInfoCounty.Text = "";
            //    cmbGauInfoState.Text = "";
            //    cmbGauInfoCountry.Text = "";
            //}
            //Sandip Darade 20091009
            if (cb_AddrforGuardian.Checked == true)

            {
                oGauAddresscontrol.txtAddress1.Text = Address.PatientAddress1;
                oGauAddresscontrol.txtAddress2.Text = Address.PatientAddress2;
                oGauAddresscontrol.txtCity.Text = Address.PatientCity;
                oGauAddresscontrol._isTextBoxLoading = true;
                oGauAddresscontrol.txtZip.Text = Address.PatientZip;
                oGauAddresscontrol._isTextBoxLoading = false;
                oGauAddresscontrol.txtCounty.Text = Address.PatientCounty;
                oGauAddresscontrol.cmbState.Text = Address.PatientState;
                oGauAddresscontrol.cmbCountry.Text = Address.PatientCountry;
            }
            else
            {
                oGauAddresscontrol.txtAddress1.Text = "";
                oGauAddresscontrol.txtAddress2.Text = "";
                oGauAddresscontrol.txtCity.Text = "";
                oGauAddresscontrol._isTextBoxLoading = true;
                oGauAddresscontrol.txtZip.Text = "";
                oGauAddresscontrol._isTextBoxLoading = false;
                oGauAddresscontrol.txtCounty.Text = "";
                oGauAddresscontrol.cmbState.Text = "";
                oGauAddresscontrol.cmbCountry.Text = "";
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


        private void pnlTOP_Paint(object sender, PaintEventArgs e)
        {

        }

        #region "Email Address Validation"

        private void txtGauInfoEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtGauInfoEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void txtGauIFatherEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtGauIFatherEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void txtGauiMEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtGauiMEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }   
        }

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


        //Sandip Darade 20090827
        //Zip control implemented 

        #region "Zip control implemented  "
        bool isFormLoading = false;

        private gloZipcontrol oZipcontrol;
      // private bool isSearchControlOpen = false;
        enumZipTextType _ZipTextType;
        private string _TempZipText;
        private bool _isZipItemSelected = false;
        private bool _isTextBoxLoading = false;
        private ToolTip oToolTip = new ToolTip();
        #region " ZIP Text Events "

        private void FatherZip_GotFocus(object sender, System.EventArgs e)
        {
            _ZipTextType = enumZipTextType.FatherZip;
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipText = txtGauIFatherZip.Text.Trim();

                //}
            }
            catch
            {
            }
        }

        private void FatherZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _ZipTextType = enumZipTextType.FatherZip;
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlFInternalControl .Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void FatherZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _ZipTextType = enumZipTextType.FatherZip;
                if (e.KeyChar == Convert.ToChar(13))
                {
                    //' HITS ENTER BUTTON ''
                    if (pnlFInternalControl .Visible)
                    {

                        oZipcontrol_ItemSelected(null, null);
                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''
                    if (txtGauIFatherCity.Text == "" && txtGauIFatherCounty.Text == "" && txtGauIFatherZip.Text == "")
                    {
                        _TempZipText = txtGauIFatherZip.Text;

                    }
                    txtGauIFatherCity.Focus();
                }

                //Sandip Darade 200090912
                //we are allowing only alphanumeric charactors for according referring the information from the link below  
                // http://www.postalcodedownload.com/
                //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
                //an alphabetic character and "N" represents a numeric character. 

                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void FatherZip_LostFocus(object sender, System.EventArgs e)
        {
            _ZipTextType = enumZipTextType.FatherZip;
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    txtGauIFatherZip.Text = _TempZipText;
                    if (txtGauIFatherCity.Text == "" && txtGauIFatherCounty.Text == "" && txtGauIFatherZip.Text == "")
                    {
                        _TempZipText = txtGauIFatherZip.Text;

                    }
                    pnlFInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
        }

        private void FatherZip_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                //_ZipTextType = enumZipTextType.PatientZip;
                pnlFInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlFInternalControl.Visible == false)
                    {
                        pnlFInternalControl.Visible = true;
                        OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                        oZipcontrol.FillControl(Convert.ToString(txtGauIFatherZip.Text.Trim()));
                    }
                    else
                    {
                        oZipcontrol.FillControl(Convert.ToString(txtGauIFatherZip.Text.Trim()));
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //_TempZipText = txtGauIFatherZip.Text;
            }
        }
      
        # region"Mother zip events"
        private void MotherZip_GotFocus(object sender, System.EventArgs e)
        {
            _ZipTextType = enumZipTextType.MotherZip;
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipText = txtGauIMotherZip.Text.Trim();

                //}
            }
            catch
            {
            }
        }

        private void MotherZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _ZipTextType = enumZipTextType.MotherZip;
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlMInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void MotherZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _ZipTextType = enumZipTextType.MotherZip;
                if (e.KeyChar == Convert.ToChar(13))
                {
                    //' HITS ENTER BUTTON ''
                    if (pnlMInternalControl.Visible)
                    {

                        oZipcontrol_ItemSelected(null, null);
                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''
                    if (txtGauIMotherCity.Text == "" && txtGauIMCounty.Text == "" && txtGauIMotherZip.Text == "")
                    {
                        _TempZipText = txtGauIMotherZip.Text;

                    }
                    txtGauIMotherCity.Focus();
                }

                //Sandip Darade 200090912
                //we are allowing only alphanumeric charactors for according referring the information from the link below  
                // http://www.postalcodedownload.com/
                //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
                //an alphabetic character and "N" represents a numeric character. 

                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void MotherZip_LostFocus(object sender, System.EventArgs e)
        {
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    txtGauIMotherZip.Text = _TempZipText;
                    if (txtGauIMotherCity.Text == "" && txtGauIMCounty.Text == "" && txtGauIMotherZip.Text == "")
                    {
                        _TempZipText = txtGauIMotherZip.Text;

                    }
                    pnlMInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
        }

        private void MotherZip_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                _ZipTextType = enumZipTextType.MotherZip ;
                pnlFInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlMInternalControl.Visible == false)
                    {
                        pnlMInternalControl.Visible = true;
                        OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                        oZipcontrol.FillControl(Convert.ToString(txtGauIMotherZip.Text.Trim()));
                    }
                    else
                    {
                        oZipcontrol.FillControl(Convert.ToString(txtGauIMotherZip.Text.Trim()));
                    }
                }

            }
            catch
            {
            }
            finally
            {
               
            }
        }

        # endregion
        # region"Guardian zip events"
        private void GuardianZip_GotFocus(object sender, System.EventArgs e)
        {
            _ZipTextType = enumZipTextType.GuardianZip;
            try
            {
                //if (_ZipTextType != enumZipTextType.PatientZip) {
                _TempZipText = txtGauInfoZip.Text.Trim();

                //}
            }
            catch
            {
            }
        }

        private void GuardianZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _ZipTextType = enumZipTextType.GuardianZip;
            try
            {
                if (e.KeyCode == Keys.Down | e.KeyCode == Keys.Up)
                {
                    //' HITS UP / DOWN ''
                    if (pnlGInternalControl.Visible)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        oZipcontrol.C1GridList.Focus();
                        oZipcontrol.C1GridList.Select(oZipcontrol.C1GridList.RowSel, 0);
                    }
                }
            }
            catch
            {
            }
        }

        private void GuardianZip_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                _ZipTextType = enumZipTextType.GuardianZip;
                if (e.KeyChar == Convert.ToChar(13))
                {
                    //' HITS ENTER BUTTON ''
                    if (pnlGInternalControl.Visible)
                    {

                        oZipcontrol_ItemSelected(null, null);
                    }
                }
                else if (e.KeyChar == Convert.ToChar(27))
                {
                    //' HITS ESCAPE ''
                    if (txtGauInfoCity.Text == "" && txtGauInfoCounty.Text == "" && txtGauInfoZip.Text == "")
                    {
                        _TempZipText = txtGauInfoZip.Text;

                    }
                    txtGauInfoCity.Focus();
                }

                //Sandip Darade 200090912
                //we are allowing only alphanumeric charactors for according referring the information from the link below  
                // http://www.postalcodedownload.com/
                //The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
                //an alphabetic character and "N" represents a numeric character. 

                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void GuardianZip_LostFocus(object sender, System.EventArgs e)
        {
            if (oZipcontrol != null)
            {
                if (_isZipItemSelected == false & oZipcontrol.C1GridList.Focused == false & oZipcontrol.Focused == false)
                {
                    _isTextBoxLoading = true;
                    txtGauInfoZip.Text = _TempZipText;
                    if (txtGauInfoCity.Text == "" && txtGauInfoCounty.Text == "" && txtGauInfoZip.Text == "")
                    {
                        _TempZipText = txtGauInfoZip.Text;

                    }
                    pnlGInternalControl.Visible = false;
                    _isTextBoxLoading = false;
                }
            }
        }

        private void GuardianZip_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                _ZipTextType = enumZipTextType.GuardianZip;
                pnlGInternalControl.BringToFront();

                if (isFormLoading == false & _isTextBoxLoading == false)
                {
                    if (pnlGInternalControl.Visible == false)
                    {
                        pnlGInternalControl.Visible = true;
                        OpenInternalControl(gloGridListControlType.ZIP, "Zip", false, 0, 0, "");
                        oZipcontrol.FillControl(Convert.ToString(txtGauInfoZip.Text.Trim()));
                    }
                    else
                    {
                        oZipcontrol.FillControl(Convert.ToString(txtGauInfoZip.Text.Trim()));
                    }
                }
            }
            catch
            {
            }
            finally
            {
              
            }
        }

          # endregion




        private void oZipcontrol_ItemSelected(object sender, EventArgs e)
        {


            try
            {
                if (oZipcontrol.C1GridList.Row < 0)
                {
                    return;
                }
                string _Zip = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString();
                string _City = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString();
                string _ID = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString();
                string _County = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString();
                string _State = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString();
                string _AreaCode = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString();

                _isTextBoxLoading = true;
                switch (_ZipTextType)
                {
                    //    case enumZipTextType.PatientZip:
                    ////txtPAZip.Text = _Zip;
                    ////txtPAZip.Tag = _ID;
                    ////txtPACity.Text = _City;
                    ////txtPACity.Tag = _AreaCode;
                    ////txtPACounty.Text = _County;
                    ////cmbPAState.Text = _State;

                    //break;
                    //case enumZipTextType.WorkZip:
                    //    txtwZip.Text = _Zip;
                    //    txtwZip.Tag = _ID;
                    //    txtwCity.Text = _City;
                    //    txtwCity.Tag = _AreaCode;
                    //    cmbwState.Text = _State;
                    //    cmbwState.Tag = _County;

                    //    break;

                    case enumZipTextType.MotherZip:

                        txtGauIMotherZip.Text = _Zip;
                        txtGauIMotherZip.Tag = _ID;
                        txtGauIMotherCity.Text = _City;
                        txtGauIMotherCity.Tag = _AreaCode;
                        txtGauIMCounty.Text = _County;
                        cmbGauIMState.Text = _State;
                        break;
                    case enumZipTextType.GuardianZip:

                        txtGauInfoZip.Text = _Zip;
                        txtGauInfoZip.Tag = _ID;
                        txtGauInfoCity.Text = _City;
                        txtGauInfoCity.Tag = _AreaCode;
                        txtGauInfoCounty.Text = _County;
                        cmbGauInfoState.Text = _State;

                        break;
                    case enumZipTextType.FatherZip:

                        txtGauIFatherZip.Text = _Zip;
                        txtGauIFatherZip.Tag = _ID;
                        txtGauIFatherCity.Text = _City;
                        txtGauIFatherCity.Tag = _AreaCode;
                        txtGauIFatherCounty.Text = _County;
                        cmbFState.Text = _State;

                        break;
                        //case enumZipTextType.InsuranceZip:
                        //    txtInsZip.Text = _Zip;
                        //    txtInsZip.Tag = _ID;
                        //    txtInsCity.Text = _City;
                        //    txtInsCity.Tag = _AreaCode;
                        //    txtInsCounty.Text = _County;
                        //    cmbInsState.Text = _State;
                        //break;
                        //}
                }
                        _isTextBoxLoading = false;
                        _isZipItemSelected = true;
                        if (pnlFInternalControl.Visible == true)
                        {
                            pnlFInternalControl.Visible = false;
                            txtGauIFatherCity.Focus();
                        }
                        //else if (pnlWInternalControl.Visible == true) {
                        //    pnlWInternalControl.Visible = false;
                        //    txtwCity.Focus();
                        //}
                        //else if (pnlIIntrernalControl.Visible == true) {
                        //    pnlIIntrernalControl.Visible = false;
                        //    txtInsCity.Focus();
                        //}
                        else if (pnlMInternalControl.Visible == true)
                        {
                            pnlMInternalControl.Visible = false;
                            txtGauIMotherCity .Focus();
                        }
                        else if (pnlFInternalControl.Visible == true)
                        {
                            pnlFInternalControl.Visible = false;
                            txtGauIFatherCity.Focus();
                        }
                        else if (pnlGInternalControl.Visible == true)
                        {
                            pnlGInternalControl.Visible = false;
                            txtGauInfoCity.Focus();
                        }
                        _ZipTextType = enumZipTextType.None;
                        //isSearchControlOpen = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oZipcontrol_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

     


        public bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            _isZipItemSelected = false;
            try
            {

                if (oZipcontrol != null)
                {
                    CloseInternalControl();
                }
                oZipcontrol = new gloZipcontrol(ControlType, false, 0, 0, 0, _databaseconnectionstring);
                oZipcontrol.ItemSelectedclick += oZipcontrol_ItemSelected;
                oZipcontrol.InternalGridKeyDownclick += oZipcontrol_InternalGridKeyDown;
                //AddHandler oZipcontrol.CloseBtnClick, AddressOf oZipcontrol_CloseBtnClick
                oZipcontrol.ControlHeader = ControlHeader;
                oZipcontrol.ShowHeader = false;

                switch (_ZipTextType)
                {
                //    case enumZipTextType.PatientZip:
                //oZipcontrol.Dock = DockStyle.Fill;
                //pnlInternalControl.BringToFront();
                //pnlInternalControl.Visible = true;
                //pnlInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.WorkZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlWInternalControl.BringToFront();
                //    pnlWInternalControl.Visible = true;
                //    pnlWInternalControl.Controls.Add(oZipcontrol);

                //    break;
                //case enumZipTextType.InsuranceZip:
                //    oZipcontrol.Dock = DockStyle.Fill;
                //    pnlIIntrernalControl.BringToFront();
                //    pnlIIntrernalControl.Visible = true;
                //    pnlIIntrernalControl.Controls.Add(oZipcontrol);

                //    break;
                case enumZipTextType.MotherZip:
                    oZipcontrol.Dock = DockStyle.Fill;
                    pnlMInternalControl.BringToFront();
                    pnlMInternalControl.Visible = true;
                    pnlMInternalControl.Controls.Add(oZipcontrol);

                    break;
                case enumZipTextType.FatherZip:
                    oZipcontrol.Dock = DockStyle.Fill;
                    pnlFInternalControl.BringToFront();
                    pnlFInternalControl.Visible = true;
                    pnlFInternalControl.Controls.Add(oZipcontrol);

                    break;
                case enumZipTextType.GuardianZip:
                    oZipcontrol.Dock = DockStyle.Fill;
                    pnlGInternalControl.BringToFront();
                    pnlGInternalControl.Visible = true;
                    pnlGInternalControl.Controls.Add(oZipcontrol);
                    break;
                }
       
                //pnlInternalControl.Controls.Add(oZipcontrol)



                //pnlInternalControl.Controls.Add(oZipcontrol)

                //oZipcontrol.Dock = DockStyle.Fill
                //pnlInternalControl.BringToFront()
                //pnlInternalControl.Visible = True

                if (!string.IsNullOrEmpty(SearchText))
                {
                    oZipcontrol.Search(SearchText, SearchColumn.Code);
                }
                oZipcontrol.Show();
                _result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = false;
            }
            finally
            {

            }

            //isSearchControlOpen = true;
            return _result;
        }

        private bool CloseInternalControl()
        {
            if (oZipcontrol != null)
            {

                _isTextBoxLoading = true;
                switch (_ZipTextType)
                {
                //    case enumZipTextType.PatientZip:
                //for (int i = 0; i <= pnlInternalControl.Controls.Count - 1; i++)
                //{
                //    pnlInternalControl.Controls.RemoveAt(i);
                //}


                //    break;

                //case enumZipTextType.WorkZip:
                //    for (int i = 0; i <= pnlWInternalControl.Controls.Count - 1; i++)
                //    {
                //        pnlWInternalControl.Controls.RemoveAt(i);
                //    }


                //    break;
                case enumZipTextType.MotherZip:
                        //SLR: Changed on 2/4/2014
                        for (int i = pnlMInternalControl.Controls.Count - 1; i >= 0;  i--)
                        {
                            pnlMInternalControl.Controls.RemoveAt(i);
                        }


                    break;
                case enumZipTextType.FatherZip:
                        //SLR: Changd on 2/4/2014
                    for (int i = pnlFInternalControl.Controls.Count - 1; i >= 0;  i--)
                    {
                        pnlFInternalControl.Controls.RemoveAt(i);
                    }


                    break;
                case enumZipTextType.GuardianZip:
                    //for (int i = 0; i <= pnlGInternalControl.Controls.Count - 1; i++)
                    //{
                    //    pnlGInternalControl.Controls.RemoveAt(i);
                    //}


                    break;
                //case enumZipTextType.InsuranceZip:
                //    for (int i = 0; i <= pnlIIntrernalControl.Controls.Count - 1; i++)
                //    {
                //        pnlIIntrernalControl.Controls.RemoveAt(i);
                //    }


                //    break;
            }

                if (oZipcontrol != null)
                {
                    try
                    {
                        oZipcontrol.ItemSelectedclick -= oZipcontrol_ItemSelected;
                        oZipcontrol.InternalGridKeyDownclick -= oZipcontrol_InternalGridKeyDown;
                    }
                    catch
                    {
                    }
                    oZipcontrol.Dispose();
                    oZipcontrol = null;
                }


                _isTextBoxLoading = false;

            }
            return _isTextBoxLoading;
        }

       
        #endregion

      
        #endregion

        //private void addCategory(String CategDs, String CategTp)
        //{
        //    System.Data.SqlClient.SqlConnection conn  = new System.Data.SqlClient.SqlConnection(_databaseconnectionstring);
        //    System.Data.SqlClient.SqlCommand oCmd   = new System.Data.SqlClient.SqlCommand("InsertCategory", conn);
        //    System.Data.SqlClient.SqlParameter objParam;
        //    try
        //    {
        //        if (_databaseconnectionstring != String.Empty)
        //        {
                   
        //            if (conn != null)
        //            {
        //                if (oCmd == null)
        //                {
        //                    return; 
        //                }
        //                oCmd.CommandType = CommandType.StoredProcedure;
        //                objParam = oCmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 50);
        //                objParam.Direction = ParameterDirection.Input;
        //                objParam.Value = CategDs;

        //                objParam = oCmd.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 50);
        //                objParam.Direction = ParameterDirection.Input;
        //                objParam.Value = CategTp;

        //                objParam = oCmd.Parameters.Add("@nClinicID", SqlDbType.BigInt);
        //                objParam.Direction = ParameterDirection.Input;
        //                objParam.Value = _nClinicID;

        //                objParam = oCmd.Parameters.Add("@bIsBlocked", SqlDbType.Bit);
        //                objParam.Direction = ParameterDirection.Input;
        //                objParam.Value = false;
        //                if (conn.State == ConnectionState.Closed)
        //                {
        //                    conn.Open();
        //                }
        //                oCmd.ExecuteNonQuery();
        //                //////fill combo again after adding new category
        //                switch (CategTp.ToLower())
        //                {
        //                    case "relationship":
        //                        {
        //                            searchstr = cmbGauInfoRelation.Text;
        //                            fillRelationShip();
        //                            cmbGauInfoRelation.Text = searchstr;
        //                            break;
        //                        }
        //                }
        //            }//_databaseconnectionstring
        //        }//Connection String 
        //        // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", gloAuditTrail.ActivityOutCome.Success);
        //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
        //    }
        //    catch (System.Data.SqlClient.SqlException  Ex)
        //    {
        //        MessageBox.Show(Ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", gloAuditTrail.ActivityOutCome.Failure);
        //        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Category Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        if (conn != null)
        //        {
        //            conn.Dispose();
        //            conn = null;
        //        }
        //        if (oCmd != null)
        //        {
        //            oCmd.Dispose();
        //            oCmd = null;
        //        }
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        if (conn != null)
        //        {
        //            conn.Dispose();
        //            conn = null;
        //        }
        //        if (oCmd != null)
        //        {
        //            oCmd.Dispose();
        //            oCmd = null;
        //        }
        //    }

        //}

        private void cmbGauInfoRelation_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbGauInfoRelation.SelectedIndex == -1 && cmbGauInfoRelation.Text.Trim() != "")
            //{
            //    if (DialogResult.Yes == MessageBox.Show(" Do you want to Add new relationship Category?  \r\n  Yes - Add Category \r\n  No  - Select From the List", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        addCategory(cmbGauInfoRelation.Text, "Relationship");
            //        MessageBox.Show("Releationship Category Added Successfully ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please select Relationship from the list ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cmbGauInfoRelation.Focus();
            //    }
            //}
        }
        private void cmbGauInfoRelation_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && cmbGauInfoRelation.SelectedIndex == -1 && cmbGauInfoRelation.Text.Trim() != "")
            //{
            //    e.IsInputKey = true;
            //}
            //else
            //{
            //    e.IsInputKey = false;
            //}

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //searchstr = "";
        }

        //private void fillRelationShip()
        //{
        //    // To Fill The Relationships
        //    RelationShip oRelationShip = new RelationShip(_databaseconnectionstring);
        //    DataTable dtRelation = new DataTable();
        //    // Logic changed as per discussion with Pramod on 21/01/2010 - to fetch Relationships from Category only - by Ujwala Atre                
        //    ////////////dtRelation = oRelationShip.GetList(); 
        //    // Logic changed as per discussion with Pramod on 21/01/2010 - to fetch Relationships from Category only - by Ujwala Atre                
        //    //
        //    try
        //    {
        //        if (oRelationShip != null)
        //        {
        //            dtRelation = oRelationShip.GetList("SELECT  nCategoryid nPatientRelID,sDescription sRelationshipDesc,nClinicID,sCode FROM category_mst WHERE UPPER(sCategoryType) ='Relationship' AND bIsBlocked = '" + false + "' AND nClinicID = " + _nClinicID + " order by sDescription ");
        //            if (dtRelation != null)
        //            {
        //                if (dtRelation.Rows.Count > 0)
        //                {
        //                    DataRow dr = dtRelation.NewRow();
        //                    dr["sCode"] = "0";
        //                    dr["sRelationshipDesc"] = "";
        //                    dtRelation.Rows.InsertAt(dr, 0);
        //                    dtRelation.AcceptChanges();

        //                    cmbGauInfoRelation.DataSource = dtRelation;
        //                    cmbGauInfoRelation.ValueMember = dtRelation.Columns["sCode"].ColumnName;
        //                    cmbGauInfoRelation.DisplayMember = dtRelation.Columns["sRelationshipDesc"].ColumnName;
        //                    //_SubscriberRelationShip = cmbRelationShip.Text;
        //                    //if (dtRelation.Rows.Count > 0)
        //                    //{
        //                    //    cmbGauInfoRelation.SelectedIndex = 0;
        //                    //}
        //                }//dtReleationShip

        //            }
        //        }//oReleationShip
        //    }
        //    catch (Exception)
        //    {
        //        if (oRelationShip != null)
        //        {
        //            oRelationShip.Dispose();
        //            oRelationShip = null;
        //        }
        //    }
        //    finally
        //    {
        //        if (oRelationShip != null)
        //        {
        //            oRelationShip.Dispose();
        //            oRelationShip = null;
        //        }
        //    }
        //}

        public void DisposeAllControls()
        {
            try
            {
                if (_PatientGuardian != null) { _PatientGuardian.Dispose(); }
                if (oPatientDemo != null) { oPatientDemo.Dispose(); }
                if (oMotherAddresscontrol != null) { oMotherAddresscontrol.Dispose(); }
                if (oFatherAddresscontrol != null) { oFatherAddresscontrol.Dispose(); }
                if (oGauAddresscontrol != null) { oGauAddresscontrol.Dispose(); }       
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       

    }
}
