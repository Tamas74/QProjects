using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using gloCommon;
using gloSettings;
using C1.Win.C1FlexGrid;

namespace gloContacts
{
    public partial class frmSetupPhysician : Form
    {
        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private bool _IsInternetFax = false;

        private Int64 _ContactID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 _ContactDetailID = 0;

        const int COL_QUALIFIERID = 0;
        const int COL_QUALIFIERCODE = 1;
        const int COL_QUALIFIERDESCRIPTION = 2;
        const int COL_VALUE = 3;
        const int COL_ISSYSTEM = 4;
        const int COL_COUNT = 5;

        public Int64 ContactID
        {
            get { return _ContactID;}
            set { _ContactID = value; }
        }
        private gloListControl.gloListControl oListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Taxonomy;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        gloAddress.gloAddressControl oCompAddresscontrol = null;
        gloAddress.gloAddressControl oBMAddresscontrol = null;
        gloAddress.gloAddressControl oBPracAddresscontrol = null;

        public String CallFrom = "";

        #endregion "Private Variables"

        public frmSetupPhysician(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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
 
            //abhisekh pandey  20110429
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


        }

        public frmSetupPhysician(Int64 ContactId, string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;

            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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
            //abhisekh pandey  20110429
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
        }


        private void frmSetupPhysician_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = null;
            gloPMContacts.gloContacts oglocontact = null;
            gloPMContacts.ContactDetails Ocdet = null;
            gloPMContacts.Physician oPhysician = null;
            try
            {
                if (Screen.PrimaryScreen.Bounds.Height < 900)
                {
                    this.Size = new System.Drawing.Size(810, 720);
                    panel1.AutoScroll = true;
                }
                else
                {
                    panel1.AutoScroll = false;
                }

                grpBoxSecureMsg.Visible = false;
                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);
                gloC1FlexStyle.Style(c1PhysicianAdditionalIDs, false);
                //Sandip Darade 20091006
                //If fax is not internet fax do no masking  for fax information
                if (_IsInternetFax == false)
                {
                    txt_Prac_Fax.MaskType = gloMaskControl.gloMaskType.Other;
                    txt_Comp_Fax.MaskType = gloMaskControl.gloMaskType.Other;
                    txt_BM_Fax.MaskType = gloMaskControl.gloMaskType.Other;
                }
                oCompAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
                oCompAddresscontrol.Dock = DockStyle.Fill;
                pnlComAddresssControl.Controls.Add(oCompAddresscontrol);

                oBMAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
                oBMAddresscontrol.Dock = DockStyle.Fill;
                pnlBMAddresssControl.Controls.Add(oBMAddresscontrol);

                oBPracAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
                oBPracAddresscontrol.Dock = DockStyle.Fill;
                pnlBPracAddresssControl.Controls.Add(oBPracAddresscontrol);

                txtfName.Select();
                DesignGrid();
                DesignAlternateGrid();
                SetListViews();
                Fill_Speciality();
                if (_ContactID != 0)
                {

                    oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                    //ContactDetail oCDetail = new ContactDetail();
                    Ocdet = new gloPMContacts.ContactDetails();
                    oPhysician = new gloPMContacts.Physician();
                    Ocdet = oglocontact.SelectPhysicianDetails(_ContactID);
                    Fill_ContactDetails(Ocdet);
                    oPhysician = oglocontact.SelectPhysician(_ContactID);
                    Fill_Physiciancontacts(oPhysician);
                }
                if (CallFrom == "Direct Physician")
                {
                    //Company Address Information
                    groupBox1.Enabled = false;
                    //Business Mailing Address Information
                    GBox_BMadrs.Enabled = true;
                    //Business Practice Location Address Information
                    GBox_Pracadrs.Enabled = true;
                    //Secure Message
                    grpBoxSecureMsg.Visible = true;
                    grpBoxSecureMsg.Enabled = true;
                    txtDirectAddress.Enabled = true;
                    txtClinicName.Enabled = false;
                    txtSPI.Enabled = false;
                    txtSpecialtyType.Enabled = false;
                    //Taxonomy                   
                    GroupBoxTaxonomy.Enabled = true;
                    txtNPI.Enabled = false;
                    //Notes
                    groupBox2.Enabled = true;
                    //Additional Provider Information
                    gboxContact_detail.Enabled = true;

                    panel1.Enabled = true;
                    ts_btnSave.Enabled = true;
                }

                if (CallFrom == "Physician")
                {
                    grpBoxSecureMsg.Visible = true;
                    txtDirectAddress.Enabled = true;
                    txtClinicName.Enabled = false;
                    txtSpecialtyType.Enabled = false;
                    txtSPI.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                tabSettings = null;
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                if (Ocdet != null) { Ocdet.Dispose(); Ocdet = null; }
                if (oPhysician != null) { oPhysician.Dispose(); oPhysician = null; }
            }
        }

        private void Fill_Speciality()
        {
            DataTable dtSpecialty = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT ISNULL(sCode,'') AS sCode, ISNULL(sDescription,'') AS sDescription,nSpecialtyID,ISNULL(bIsBlocked,'false') AS bIsBlocked "
                                + " FROM Specialty_MST WHERE nClinicID = " + _ClinicID;
                oDB.Retrive_Query(strQuery, out dtSpecialty);

                if (dtSpecialty != null)
                {
                    if (dtSpecialty.Rows.Count != 0)
                    {

                        DataRow dr = dtSpecialty.NewRow();
                        dr["nSpecialtyID"] = 0;
                        dr["sDescription"] = "";
                        dtSpecialty.Rows.InsertAt(dr, 0);
                        dtSpecialty.AcceptChanges();

                        cmbSpecialty.DataSource = dtSpecialty;
                        cmbSpecialty.DisplayMember = dtSpecialty.Columns["sDescription"].ColumnName;
                        cmbSpecialty.ValueMember = dtSpecialty.Columns["nSpecialtyID"].ColumnName;
                        cmbSpecialty.Refresh();
                    }
                }
                strQuery = null;
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
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        #region "Design Grid"

        private void DesignGrid()
        {
            try
            {
                // c1Template.DataSource = dt;

                c1Template.Cols.Count = 3;
                //c1Template.Rows.Count = 1;
                c1Template.Rows.Count = 2;
                c1Template.AllowAddNew = true;
                c1Template.SetData(0, 1, "Description");
                c1Template.SetData(0, 2, "Value");
                c1Template.SetData(1, 1, "Medicare ID");
                c1Template.SetData(2, 1, "Medicaid ID");
             
                c1Template.Cols[0].Visible = false;
                c1Template.Cols[1].Visible = true;
                c1Template.Cols[2].Visible = true;

                int nWidth = gboxContact_detail.Width - 10;
                c1Template.Cols[0].Width = 0;
                c1Template.Cols[1].Width = (int)(0.47 * (nWidth));
                c1Template.Cols[2].Width = (int)(0.45 * (nWidth));
                c1Template.ExtendLastCol = true;

                //c1Template.GetCellRange(0,0).Style =
                //c1Template.Cols[0].AllowEditing = false;
                //c1Template.Cols[1].AllowEditing = true;
                //c1Template.Cols[2].AllowEditing = true;

                c1Template.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Template.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            }

            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }
        

        private void SetListViews()
        {
            int _Width = 0;
            lvwInsurances.Items.Clear();
            lvwHospitals.Items.Clear();


            lvwInsurances.Items.Clear();
            lvwInsurances.Columns.Add("Code"); //0 Text
            lvwInsurances.Columns.Add("Insurances"); //Sub Item 1
            lvwInsurances.Columns.Add("ID"); //Sub Item 7
            lvwInsurances.Columns.Add("Insurance ID"); //Sub Item 8

            _Width = (lvwInsurances.Width - 15);

            lvwInsurances.Columns[0].Width = 0; // Code
            lvwInsurances.Columns[1].Width = _Width; // procedure
            lvwInsurances.Columns[2].Width = 0;
            lvwInsurances.Columns[3].Width = 0;

            lvwHospitals.Items.Clear();
            lvwHospitals.Columns.Add("Code"); //0 Text
            lvwHospitals.Columns.Add("Hospitals"); //Sub Item 1
            lvwHospitals.Columns.Add("ID"); //Sub Item 7
            lvwHospitals.Columns.Add("Hospital ID"); //Sub Item 8

            _Width = (lvwHospitals.Width - 15);

            lvwHospitals.Columns[0].Width = 0; // Code
            lvwHospitals.Columns[1].Width = _Width; // procedure
            lvwHospitals.Columns[2].Width = 0;
            lvwHospitals.Columns[3].Width = 0;

            ///**Code For the initial setup of list Control on tab page2
        }

        #endregion


        #region "Tool Strip Buttons"
        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            gloPMContacts.gloContacts oglocontact = null;
            string _PhysicianFirstName = null;
            string _PhysicianMiddleName = null;
            string _PhysicianLastName = null;
            string _sSPI = null;
            try
            {
                c1Template.Row = -1;
                //Added By MaheshB
                oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                bool _result;
                _PhysicianFirstName = txtfName.Text.Trim();
                _PhysicianMiddleName = txtmName.Text.Trim();
                _PhysicianLastName = txtlName.Text.Trim();
                _sSPI = txtSPI.Text.Trim();
                _result = oglocontact.IsExistsPhysician(_PhysicianFirstName, _PhysicianMiddleName, _PhysicianLastName, _ContactID, _sSPI);
                if (_result == true)
                {
                    if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                    {
                        return;
                    }

                }

                if (ValidateData() == true)
                {
                    if (SaveData() == true)
                    {
                        this.Close();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                _PhysicianFirstName = null;
                _PhysicianMiddleName = null;
                _PhysicianLastName = null;
                _sSPI = null;
            }
        }
        #endregion

        private bool SaveData()
        {
            gloPMContacts.Physician oPhysician = new gloPMContacts.Physician();
            gloPMContacts.ContactDetails oCDetail = new gloPMContacts.ContactDetails();
            gloPMContacts.ReferringProviderAdditionalQualifierIDs oAddRefferalIDs = new gloPMContacts.ReferringProviderAdditionalQualifierIDs();
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _Result = false;
            try
            {
                oPhysician = AddPhysicianContacts();

                //New Contact
                if (_ContactID == 0)
                {
                    _ContactID = ogloContacts.Add(oPhysician);
                    oCDetail = AddDetails();
                    _ContactDetailID = ogloContacts.AddDetails(oCDetail);
                    oAddRefferalIDs = AddAdditionalQuailifierIDDetails();
                    ogloContacts.AddQualifierDetails(oAddRefferalIDs);
                    if (_ContactID == 0)
                    {
                        _Result = false;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Add, "Add physician", 0, _ContactID, 0, ActivityOutCome.Failure);
                    }
                    else
                    {
                        _Result = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Add, "Physician added", 0, _ContactID, 0, ActivityOutCome.Success);
                    }


                }
                //contact to modify 
                else
                {
                    #region"Commented Code."
                    //oPhysician.ContactID = _ContactID;
                    //oCDetail = AddDetails();
                    //_ContactDetailID = ogloContacts.AddDetails(oCDetail);
                    //oAddRefferalIDs = AddAdditionalQuailifierIDDetails();
                    //ogloContacts.AddQualifierDetails(oAddRefferalIDs);

                    //if (ogloContacts.Add(oPhysician) == 0)
                    //{
                    //    _Result = false;
                    //}
                    //else
                    //{
                    //    if (ModifyPatientPhysician(oPhysician) == true)
                    //    {
                    //        _Result = true;

                    //        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Modify, "Physician modified", 0, _ContactID, 0, ActivityOutCome.Success);
                    //    }
                    //    else
                    //    {
                    //        _Result = false;
                    //        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Modify, "Modified physician", 0, _ContactID, 0, ActivityOutCome.Failure);
                    //    }
                    //}
                    #endregion""

                    oPhysician.ContactID = _ContactID;

                    if (ModifyPatientPhysician(oPhysician) == true)
                    {

                        oCDetail = AddDetails();
                        _ContactDetailID = ogloContacts.AddDetails(oCDetail);
                        oAddRefferalIDs = AddAdditionalQuailifierIDDetails();
                        ogloContacts.AddQualifierDetails(oAddRefferalIDs);

                        if (ogloContacts.Add(oPhysician) == 0)
                        {
                            _Result = false;
                        }
                        else
                        {
                            _Result = true;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Modify, "Physician modified", 0, _ContactID, 0, ActivityOutCome.Success);
                        }

                    }
                    else
                    {
                        _Result = false;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Modify, "Modified physician", 0, _ContactID, 0, ActivityOutCome.Failure);
                    }






                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPhysician != null) { oPhysician.Dispose(); oPhysician = null; }
                if (oCDetail != null) { oCDetail.Dispose(); oCDetail = null; }
                if (oAddRefferalIDs != null) { oAddRefferalIDs.Dispose(); oAddRefferalIDs = null; }
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
            return _Result;
        }

        private bool ModifyPatientPhysician(gloPMContacts.Physician oPhysician)
        {
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _result = false;
            try
            {
                bool IsPatientPhysician = false;

                IsPatientPhysician = ogloContacts.IsPatientPhysician(oPhysician.ContactID);

                if (IsPatientPhysician == true)
                {
                    DialogResult _DialogResult = DialogResult.None;
                    _DialogResult = MessageBox.Show("This physician is associated with patient.  Do you want to modify patient physician?  ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (_DialogResult == DialogResult.Yes)
                    {
                        ogloContacts.ModifyPatientPhysician(oPhysician);
                        _result = true;
                    }
                    else if (_DialogResult == DialogResult.No)
                    {
                        _result = true;
                    }
                    else if (_DialogResult == DialogResult.Cancel)
                    {
                        _result = false;
                    }
                }
                else
                {
                    _result = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
            return _result; 
        }

        //Adding info to Class Object 
        private gloPMContacts.Physician AddPhysicianContacts()
        {
            try
            {
                gloPMContacts.Physician oPhysician = new gloPMContacts.Physician();

                oPhysician.LastName = txtlName.Text.Trim();
                oPhysician.MiddleName = txtmName.Text.Trim();
                oPhysician.FirstName = txtfName.Text.Trim();
                if (rbGender1.Checked)
                {
                    oPhysician.Gender = rbGender1.Text.Trim();
                }
                else if (rbGender2.Checked)
                {
                    oPhysician.Gender = rbGender2.Text.Trim();
                }
                oPhysician.TaxID = txtTaxID.Text.Trim();

                if (txtTaxonomy.Text != "")
                {
                    String[] _arrSpliter;
                    _arrSpliter = txtTaxonomy.Text.Split('-');

                    oPhysician.Taxonomy = _arrSpliter[0];
                    oPhysician.TaxonomyDesc = _arrSpliter[1];
                    _arrSpliter = null;
                }
                oPhysician.TaxID = txtTaxID.Text.Trim();
                oPhysician.NPI = txtNPI.Text.Trim();
                oPhysician.UPIN = txtUPIN.Text.Trim();
               // oPhysician.Mobile = mtxt_Comp_mobile.Text.Trim(); Sandip Darade 20100216
                oPhysician.Pager = txt_Comp_Pager.Text.Trim();
                if (cmbSpecialty.SelectedIndex != -1)
                {
                    oPhysician.SpecialtyID = Convert.ToInt64(cmbSpecialty.SelectedValue);
                }



                //oPhysician.CompanyAddress.AddrressLine1 = txt_Comp_AddressLine1.Text.Trim();
                //oPhysician.CompanyAddress.AddrressLine2 = txt_Comp_AddressLine2.Text.Trim();
                //oPhysician.CompanyAddress.City = txt_Comp_City.Text.Trim();
                //oPhysician.CompanyAddress.State = txt_Comp_State.Text.Trim();
                //oPhysician.CompanyAddress.ZIP = txt_comp_ZIP.Text.Trim();


                //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                oPhysician.CompanyAddress.AddrressLine1 = oCompAddresscontrol.txtAddress1.Text.Trim();
                oPhysician.CompanyAddress.AddrressLine2 = oCompAddresscontrol.txtAddress2.Text.Trim();
                oPhysician.CompanyAddress.City = oCompAddresscontrol.txtCity.Text.Trim();
                oPhysician.CompanyAddress.State = oCompAddresscontrol.cmbState.Text.Trim();
                oPhysician.CompanyAddress.ZIP = oCompAddresscontrol.txtZip.Text.Trim();

                oPhysician.CompanyAddress.Phone = mtxt_Comp_Phone.Text.Trim();
                oPhysician.CompanyAddress.Fax = txt_Comp_Fax.Text.Trim();
                oPhysician.CompanyAddress.Email = txt_Comp_Email.Text.Trim();
                oPhysician.CompanyAddress.URL = txt_Comp_URL.Text.Trim();

                //oPhysician.BusinessMailingAddress.AddrressLine1 = txt_BM_AddressLine1.Text.Trim();
                //oPhysician.BusinessMailingAddress.AddrressLine2 = txt_BM_AddressLine2.Text.Trim();
                //oPhysician.BusinessMailingAddress.City = txt_BM_City.Text.Trim();
                //oPhysician.BusinessMailingAddress.State = txt_BM_State.Text.Trim();
                //oPhysician.BusinessMailingAddress.ZIP = txt_BM_Zip.Text.Trim();

                //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                oPhysician.BusinessMailingAddress.AddrressLine1 = oBMAddresscontrol.txtAddress1.Text.Trim();
                oPhysician.BusinessMailingAddress.AddrressLine2 = oBMAddresscontrol.txtAddress2.Text.Trim();
                oPhysician.BusinessMailingAddress.City = oBMAddresscontrol.txtCity.Text.Trim();
                oPhysician.BusinessMailingAddress.State = oBMAddresscontrol.cmbState.Text.Trim();
                oPhysician.BusinessMailingAddress.ZIP = oBMAddresscontrol.txtZip.Text.Trim();

              //  oPhysician.BusinessMailingAddress.Phone = mtxt_BM_Phone.Text.Trim();Sandip Darade 20100216
                // oPhysician.BusinessMailingAddress.Fax = txt_BM_Fax.Text.Trim();  Sandip Darade 20100216
                oPhysician.BusinessMailingAddress.Email = txt_BM_Email.Text.Trim();
                oPhysician.BusinessMailingAddress.URL = txt_BM_URL.Text.Trim();

                //oPhysician.PracticeLocationAddress.AddrressLine1 = txt_Prac_AddressLine1.Text.Trim();
                //oPhysician.PracticeLocationAddress.AddrressLine2 = txt_Prac_AddressLine2.Text.Trim();
                //oPhysician.PracticeLocationAddress.City = txt_Prac_City.Text.Trim();
                //oPhysician.PracticeLocationAddress.State = txt_Prac_State.Text.Trim();
                //oPhysician.PracticeLocationAddress.City = txt_Prac_City.Text.Trim();
                //oPhysician.PracticeLocationAddress.ZIP = txt_Prac_Zip.Text.Trim();

                //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                oPhysician.PracticeLocationAddress.AddrressLine1 = oBPracAddresscontrol.txtAddress1.Text.Trim();
                oPhysician.PracticeLocationAddress.AddrressLine2 = oBPracAddresscontrol.txtAddress2.Text.Trim();
                oPhysician.PracticeLocationAddress.City = oBPracAddresscontrol.txtCity.Text.Trim();
                oPhysician.PracticeLocationAddress.State = oBPracAddresscontrol.cmbState.Text.Trim();
                oPhysician.PracticeLocationAddress.ZIP = oBPracAddresscontrol.txtZip.Text.Trim();

                oPhysician.PracticeLocationAddress.Phone = mtxt_Prac_Phone.Text.Trim();
               // oPhysician.PracticeLocationAddress.Fax = txt_Prac_Fax.Text.Trim();//Sandip Darade 20100216
                oPhysician.PracticeLocationAddress.Email = txt_Prac_Email.Text.Trim();
                oPhysician.PracticeLocationAddress.URL = txt_Prac_URL.Text.Trim();

                oPhysician.ContactType = Convert.ToString(gloPMContacts.ContactType.Physician);
                oPhysician.SpecialtyID = Convert.ToInt64(cmbSpecialty.SelectedValue);
                oPhysician.Notes = txtNotes.Text;



                // Start 
                //Sandip Darade 20100216
                //case  GLO2008-0002029
                //phone no,mobile no ,fax no will be saved with  mask e.g .(111)222-3333

                if (mtxt_Comp_mobile.Text.Length != 0 && mtxt_Comp_mobile.MaskFull == false)
                {
                    oPhysician.Mobile = "";
                }
                else
                {
                    oPhysician.Mobile = mtxt_Comp_mobile.Text;

                }
                if (mtxt_Comp_Phone.Text.Length != 0 && mtxt_Comp_Phone.MaskFull == false)
                {
                    oPhysician.CompanyAddress.Phone = "";
                }
                else
                {
                    oPhysician.CompanyAddress.Phone = mtxt_Comp_Phone.Text;

                }

                if (mtxt_BM_Phone.Text.Length != 0 && mtxt_BM_Phone.MaskFull == false)
                {
                    oPhysician.BusinessMailingAddress.Phone = "";
                }
                else
                {
                    oPhysician.BusinessMailingAddress.Phone = mtxt_BM_Phone.Text;

                }
                if (mtxt_Prac_Phone.Text.Length != 0 && mtxt_Prac_Phone.MaskFull == false)
                {
                    oPhysician.PracticeLocationAddress.Phone = "";
                }
                else
                {
                    oPhysician.PracticeLocationAddress.Phone = mtxt_Prac_Phone.Text;

                }
                if (_IsInternetFax == true)
                {
                    if (txt_Comp_Fax.Text.Length != 0 && txt_Comp_Fax.MaskFull == false)
                    {
                        oPhysician.CompanyAddress.Fax = "";
                    }
                    else
                    {
                        oPhysician.CompanyAddress.Fax = txt_Comp_Fax.Text.Trim();

                    }

                    if (txt_BM_Fax.Text.Length != 0 && mtxt_BM_Phone.MaskFull == false)
                    {
                        oPhysician.BusinessMailingAddress.Fax = "";
                    }
                    else
                    {
                        oPhysician.BusinessMailingAddress.Fax = txt_BM_Fax.Text.Trim();

                    }
                    if (txt_Prac_Fax.Text.Length != 0 && txt_Prac_Fax.MaskFull == false)
                    {
                        oPhysician.PracticeLocationAddress.Fax = "";
                    }
                    else
                    {
                        oPhysician.PracticeLocationAddress.Fax = txt_Prac_Fax.Text.Trim();

                    }
                }
                else
                {
                    oPhysician.CompanyAddress.Fax = txt_Comp_Fax.Text.Trim();
                    oPhysician.BusinessMailingAddress.Fax = txt_BM_Fax.Text.Trim();
                    oPhysician.PracticeLocationAddress.Fax = txt_Prac_Fax.Text.Trim();
                }

                 
                //Sandip Darade 20100215
                // end 


                // Sandip Darade case GLO2010-0004426 
                oPhysician.Degree = txtSuffix.Text.Trim();
                oPhysician.Suffix = txtSuffix.Text.Trim();  //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen
                oPhysician.Prefix = txtPrefix.Text.Trim();
                //Mahesh Nawal 20102607
                    //if (rbNo.Checked.Equals(true))
                    //{ oPhysician.PARequired = gloPMContacts.PriorAuthorizationRequired.No; }
                    //else if (rbAlways.Checked.Equals(true))
                    //{ oPhysician.PARequired = gloPMContacts.PriorAuthorizationRequired.Always; }
                    //else if (rbUsePlanSetting.Checked.Equals(true))
                    //{ oPhysician.PARequired = gloPMContacts.PriorAuthorizationRequired.UsePlanSetting; }

                oPhysician.SPI = txtSPI.Text.Trim();
                oPhysician.DirectAddress = txtDirectAddress.Text.Trim();

                oPhysician.SpecialtyType = txtSpecialtyType.Text.Trim();
                oPhysician.ClinicName = txtClinicName.Text.Trim();
                
                return oPhysician;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
         
        }

        private gloPMContacts.ContactDetails AddDetails()
        {
            gloPMContacts.ContactDetail odetails = new gloPMContacts.ContactDetail();
            gloPMContacts.ContactDetails oCDet = new gloPMContacts.ContactDetails();
            try
            {

                for (int i = 0; i <= lvwInsurances.Items.Count - 1; i++)
                {
                    odetails = new gloPMContacts.ContactDetail();
                    odetails.ContactID = _ContactID;
                    odetails.ContactDetailID = _ContactDetailID;
                    odetails.InsuranceCode = "";
                    odetails.InsuranceDesc = Convert.ToString(lvwInsurances.Items[i].Text);
                    odetails.Type = gloPMContacts.ContactType.Insurance.GetHashCode();
                    oCDet.Add(odetails);
                }
                for (int i = 0; i <= lvwHospitals.Items.Count - 1; i++)
                {
                    odetails = new gloPMContacts.ContactDetail();
                    odetails.ContactID = _ContactID;
                    odetails.ContactDetailID = _ContactDetailID;
                    odetails.InsuranceCode = "";
                    odetails.InsuranceDesc = Convert.ToString(lvwHospitals.Items[i].Text);
                    odetails.Type = gloPMContacts.ContactType.Hospital.GetHashCode();
                    oCDet.Add(odetails);
                }

                if (c1Template.Rows.Count > 0)
                    for (int i = 1; i <= c1Template.Rows.Count - 1; i++)
                    {
                        if (c1Template.GetData(i, 1) != null && c1Template.GetData(i, 2) != null)
                            if (c1Template.GetData(i, 1).ToString().Trim() != "")
                            {
                                if (c1Template.GetData(i, 2).ToString().Trim() != "")
                                {
                                    odetails = new gloPMContacts.ContactDetail();
                                    odetails.ContactID = _ContactID;
                                    odetails.ContactDetailID = _ContactDetailID;
                                    odetails.InsuranceCode = "";
                                    odetails.Description = Convert.ToString(c1Template.GetData(i, 1));
                                    odetails.Value = Convert.ToString(c1Template.GetData(i, 2));
                                    if (i == 1)
                                    {
                                        odetails.Type = gloPMContacts.ContactType.MediCare.GetHashCode();
                                    }
                                    else if (i == 2)
                                    {
                                        odetails.Type = gloPMContacts.ContactType.MediCaid.GetHashCode();
                                    }
                                    else
                                    {
                                        odetails.Type = gloPMContacts.ContactType.Miscellaneous.GetHashCode();
                                    }

                                    oCDet.Add(odetails);
                                }
                                //else condition below added by Sandip Darade 20100112
                                else
                                {
                                    odetails = new gloPMContacts.ContactDetail();
                                    odetails.ContactID = _ContactID;
                                    odetails.ContactDetailID = _ContactDetailID;
                                    odetails.InsuranceCode = "";
                                    odetails.Description = Convert.ToString(c1Template.GetData(i, 1));
                                    odetails.Value = "";
                                    if (i == 1)
                                    {
                                        odetails.Type = gloPMContacts.ContactType.MediCare.GetHashCode();
                                    }
                                    else if (i == 2)
                                    {
                                        odetails.Type = gloPMContacts.ContactType.MediCaid.GetHashCode();
                                    }
                                    else
                                    {
                                        odetails.Type = gloPMContacts.ContactType.Miscellaneous.GetHashCode();
                                    }

                                    oCDet.Add(odetails);

                                }
                            }
                    }
                return oCDet;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (odetails != null) { odetails.Dispose(); odetails = null; }
            }
            

        }

        private gloPMContacts.ReferringProviderAdditionalQualifierIDs AddAdditionalQuailifierIDDetails()
        {
            gloPMContacts.ReferringProviderAdditionalQualifierID odetails = new gloPMContacts.ReferringProviderAdditionalQualifierID();
            gloPMContacts.ReferringProviderAdditionalQualifierIDs oCDet = new gloPMContacts.ReferringProviderAdditionalQualifierIDs();
            try
            {
                c1PhysicianAdditionalIDs.FinishEditing();
                if (c1PhysicianAdditionalIDs.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1PhysicianAdditionalIDs.Rows.Count - 1; i++)
                    {
                        if (c1PhysicianAdditionalIDs.GetData(i, COL_VALUE) != null && Convert.ToString(c1PhysicianAdditionalIDs.GetData(i, COL_VALUE)).Trim() != "")
                        {
                            odetails = new gloPMContacts.ReferringProviderAdditionalQualifierID();
                            odetails.nRefProviderID = _ContactID;
                            odetails.nQualifierID = Convert.ToInt64(c1PhysicianAdditionalIDs.GetData(i, COL_QUALIFIERID));
                            odetails.sValue = Convert.ToString(c1PhysicianAdditionalIDs.GetData(i, COL_VALUE));
                            odetails.bIsSystem = Convert.ToBoolean(c1PhysicianAdditionalIDs.GetData(i, COL_ISSYSTEM));
                            odetails.nUserID = _UserID;
                            odetails.nClinicID = _ClinicID;
                            oCDet.Add(odetails);
                            odetails.Dispose();
                        }
                    }
                }

                //NPI
                odetails = new gloPMContacts.ReferringProviderAdditionalQualifierID();
                odetails.nRefProviderID = _ContactID;
                odetails.nQualifierID = 1;
                odetails.sValue = Convert.ToString(txtNPI.Text);
                odetails.bIsSystem = true;
                odetails.nUserID = _UserID;
                odetails.nClinicID = _ClinicID;
                oCDet.Add(odetails);
                odetails.Dispose();

                //TAXID
                odetails = new gloPMContacts.ReferringProviderAdditionalQualifierID();
                odetails.nRefProviderID = _ContactID;
                odetails.nQualifierID = 3;
                odetails.sValue = Convert.ToString(txtTaxID.Text);
                odetails.bIsSystem = true;
                odetails.nUserID = _UserID;
                odetails.nClinicID = _ClinicID;
                oCDet.Add(odetails);
                odetails.Dispose();

                //UPIN
                odetails = new gloPMContacts.ReferringProviderAdditionalQualifierID();
                odetails.nRefProviderID = _ContactID;
                odetails.nQualifierID = 5;
                odetails.sValue = Convert.ToString(txtUPIN.Text);
                odetails.bIsSystem = true;
                odetails.nUserID = _UserID;
                odetails.nClinicID = _ClinicID;
                oCDet.Add(odetails);
                odetails.Dispose();

                return oCDet;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (odetails != null) { odetails.Dispose(); odetails = null; }
            }


        }

        private bool ValidateData()
        {

            if (txtfName.Text.Trim() == "")
            {

                MessageBox.Show(this, "Enter first name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtfName.Focus();

                return false;
            }

            if (CheckEmailAddress(txt_Comp_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (CheckEmailAddress(txt_BM_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (CheckEmailAddress(txt_Prac_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!string.IsNullOrEmpty(txt_Comp_URL.Text))
            {
                if (CheckURL(txt_Comp_URL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txt_BM_URL.Text))
            {
                if (CheckURL(txt_BM_URL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(txt_Prac_URL.Text))
            {
                if (CheckURL(txt_Prac_URL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }


            //if (txtmName.Text.Trim() == "")
            //{

            //    MessageBox.Show(this, "Enter Middle Name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    txtmName.Focus();

            //    return false;
            //}


            //if (txtlName.Text.Trim() == "")
            //{

            //    MessageBox.Show(this, "Enter Last Name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    txtlName.Focus();

            //    return false;
            //}



            //Validate the phone and mobile numbers
            //Incomplete Phone Numbers
            //Sandip Darade 20090424
            ///Validations  removed as gloMask control being used
            ////if (mtxt_Comp_Phone.Text.Length > 0 && mtxt_Comp_Phone.MaskCompleted == false)
            ////{
            ////    MessageBox.Show(this, "Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ////    TabControl1.SelectedTab = TabPage1;
            ////    mtxt_Comp_Phone.Focus();
            ////    return false;
            ////}
            if (mtxt_Comp_Phone.IsValidated == false)
            {
                return false;
            }
            ////////Incomplete Mobile Numbers
            ////if (mtxt_Comp_mobile.Text.Length > 0 && mtxt_Comp_mobile.MaskCompleted == false)
            ////{
            ////    MessageBox.Show(this, "Please enter a 10 digit number for mobile.", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ////    TabControl1.SelectedTab = TabPage1;
            ////    mtxt_Comp_mobile.Focus();
            ////    return false;
            ////}
            if (mtxt_Comp_mobile.IsValidated == false)
            {
                return false;
            }
            ////if (mtxt_BM_Phone.Text.Length > 0 && mtxt_BM_Phone.MaskCompleted == false)
            ////{
            ////    MessageBox.Show(this, "Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ////    TabControl1.SelectedTab = TabPage1;
            ////    mtxt_BM_Phone.Focus();
            ////    return false;
            ////}
            if (mtxt_BM_Phone.IsValidated == false)
            {
                return false;
            }
            ////if (mtxt_Prac_Phone.Text.Length > 0 && mtxt_Prac_Phone.MaskCompleted == false)
            ////{
            ////    MessageBox.Show(this, "Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            ////    TabControl1.SelectedTab = TabPage1;
            ////    mtxt_Prac_Phone.Focus();
            ////    return false;
            ////}
            if (mtxt_Prac_Phone.IsValidated == false)
            {
                return false;
            }

            return true;
        }

        #region "Designer Events"
        private void rbGender1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGender1.Checked == true)
            {
                rbGender1.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbGender1.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbGender2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGender2.Checked == true)
            {
                rbGender2.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbGender2.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloContacts.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion


        #region "Adding Taxonomy "
        private void btnBrowseTaxonomy_Click(object sender, EventArgs e)
        {
            TabControl1.Visible = false;

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
                    //SLR30:
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
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControlInsurance_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControlInsurance_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Taxonomy, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Taxonomy";
                //tsb_SearchAppointment.Enabled = false;

                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                pnlTopToolStrip.Visible = false;
                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                TabControl1.Visible = true;
            }
        }
        private void btnClearTaxonomy_Click(object sender, EventArgs e)
        {
            txtTaxonomy.Clear();
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            pnlTopToolStrip.Visible = true;
            txtTaxonomy.Clear();

            if (oListControl.SelectedItems.Count > 0)
            {
                txtTaxonomy.Text = oListControl.SelectedItems[0].Code.ToString() + "-" + oListControl.SelectedItems[0].Description.ToString();

            }

        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            pnlTopToolStrip.Visible = true;

        }
        #endregion


        #region " Zip Text Changed "
        private void txt_BM_Zip_TextChanged(object sender, EventArgs e)
        {
            if (txt_BM_Zip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txt_BM_Zip.Text.Trim() + "'";

                    txt_BM_State.Text = "";
                    txt_BM_City.Text = "";
                    // txtPACountry.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt_BM_State.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        txt_BM_City.Text = Convert.ToString(dt.Rows[0]["City"]);
                    }
                    qry = null;
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                txt_BM_State.Text = "";
                txt_BM_City.Text = "";
                // txtPACountry.Text = "";
            }
        }

        private void txt_Prac_Zip_TextChanged(object sender, EventArgs e)
        {
            if (txt_Prac_Zip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txt_Prac_Zip.Text.Trim() + "'";

                    txt_Prac_State.Text = "";
                    txt_Prac_City.Text = "";
                    // txtPACountry.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txt_Prac_State.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        txt_Prac_City.Text = Convert.ToString(dt.Rows[0]["City"]);
                    }
                    qry = null;
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                txt_Prac_State.Text = "";
                txt_Prac_City.Text = "";
                // txtPACountry.Text = "";
            }
        }

        private void txt_comp_ZIP_TextChanged(object sender, EventArgs e)
        {
            if (txt_comp_ZIP.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txt_comp_ZIP.Text.Trim() + "'";

                    txt_Comp_State.Text = "";
                    txt_Comp_City.Text = "";
                    // txtPACountry.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                         txt_Comp_State.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        txt_Comp_City.Text = Convert.ToString(dt.Rows[0]["City"]);
                    }
                    qry = null;
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                txt_Comp_State.Text = "";
                txt_Comp_City.Text = "";
                // txtPACountry.Text = "";
            }
        }


        #endregion


        #region "Other Information Events"
        private void btnAddInsurance_Click(object sender, EventArgs e)
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
                    //SLR30:
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
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControlInsurance_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControlInsurance_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Insurances";



                _CurrentControlType = gloListControl.gloListControlType.Insurance;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControlInsurance_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControlInsurance_ItemClosedClick);

                pnlTopToolStrip.Visible = false;
                this.Controls.Add(oListControl);

                if (lvwInsurances.Items.Count > 0)
                {
                    for (int i = 0; i <= lvwInsurances.Items.Count - 1; i++)
                    {
                        oListControl.SelectedItems.Add(0, lvwInsurances.Items[i].Text, lvwInsurances.Items[i].SubItems[1].Text);

                    }
                }

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnRemoveInsurance_Click(object sender, EventArgs e)
        {
            if (lvwInsurances.SelectedItems.Count > 0)
            {
                lvwInsurances.Items.RemoveAt(lvwInsurances.SelectedItems[0].Index);
            }
            else
            {
                //lvwInsurances.Items.Clear();
            }
        }

        private void btnRemAllInsurance_Click(object sender, EventArgs e)
        {
            if (lvwInsurances.Items.Count > 0)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to remove all?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res.Equals(DialogResult.Yes))
                {
                    lvwInsurances.Items.Clear();
                }
            }
        }

        private void btnAddHospital_Click(object sender, EventArgs e)
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
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControlInsurance_ItemSelectedClick);
                        }
                        catch { }

                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControlInsurance_ItemClosedClick);
                        }
                        catch { }

                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Hospital, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Hospitals";

                _CurrentControlType = gloListControl.gloListControlType.Hospital;
                //oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                //oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControlInsurance_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControlInsurance_ItemClosedClick);

                pnlTopToolStrip.Visible = false;
                this.Controls.Add(oListControl);

                if (lvwHospitals.Items.Count > 0)
                {
                    for (int i = 0; i <= lvwHospitals.Items.Count - 1; i++)
                    {
                        oListControl.SelectedItems.Add(0, lvwHospitals.Items[i].Text, lvwHospitals.Items[i].SubItems[1].Text);
                    }
                }

                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnRemoveHospital_Click(object sender, EventArgs e)
        {
            if (lvwHospitals.SelectedItems.Count > 0)
            {
                lvwHospitals.Items.RemoveAt(lvwHospitals.SelectedItems[0].Index);

            }
        }

        private void btnRemAllHospital_Click(object sender, EventArgs e)
        {
            if (lvwHospitals.Items.Count > 0)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to remove all?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res.Equals(DialogResult.Yes))
                {
                    lvwHospitals.Items.Clear();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TabControl1.SelectedTab = TabPage2;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            TabControl1.SelectedTab = TabPage1;
        }

        //List Control
        void oListControlInsurance_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
                
                int _Counter = 0;
                switch (_CurrentControlType)
                {

                    case gloListControl.gloListControlType.Insurance:
                        {
                            //lvwInsurances.Items.Clear();
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    ListViewItem oSubListItem = new ListViewItem();
                                    oSubListItem.Text = oListControl.SelectedItems[_Counter].Description; //Code 0
                                    oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].Description); //Insurance 1
                                    oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].Code); //Start Date 2
                                    //oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].ID.ToString()); //ID 7 
                                    //oSubListItem.SubItems.Add("0"); //Procedure ID 8 
                                    if (lvwInsurances.Items.Count != 0)
                                    {
                                        int j = 0;
                                        for (int i = 0; i < lvwInsurances.Items.Count; i++)
                                        {
                                            if (lvwInsurances.Items[i].Text == oListControl.SelectedItems[_Counter].Description)
                                            {
                                                j++;
                                                break;
                                            }
                                        }
                                        if (j == 0)
                                        {
                                            lvwInsurances.Items.Add(oSubListItem);
                                        }
                                    }
                                    else
                                    {
                                        lvwInsurances.Items.Add(oSubListItem);
                                    }

                                    oSubListItem = null;
                                }
                            }
                        }
                        break;
                    case gloListControl.gloListControlType.Hospital:
                        {
                            // lvwHospitals.Items.Clear();
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    ListViewItem oSubListItem = new ListViewItem();
                                    oSubListItem.Text = oListControl.SelectedItems[_Counter].Description; //Code 0
                                    oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].Description); //Hospitals 1
                                    oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].Code);
                                    //oSubListItem.SubItems.Add(oListControl.SelectedItems[_Counter].ID.ToString()); //ID 7 
                                    //oSubListItem.SubItems.Add("0"); //Procedure ID 8 
                                    if (lvwHospitals.Items.Count != 0)
                                    {
                                        int j = 0;
                                        for (int i = 0; i < lvwHospitals.Items.Count; i++)
                                        {
                                            if (lvwHospitals.Items[i].Text == oListControl.SelectedItems[_Counter].Description)
                                            {
                                                j++;
                                                break;
                                            }
                                        }
                                        if (j == 0)
                                        {
                                            lvwHospitals.Items.Add(oSubListItem);
                                        }
                                    }
                                    else
                                    {
                                        lvwHospitals.Items.Add(oSubListItem);
                                    }
                                    oSubListItem = null;
                                }
                            }
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
                pnlTopToolStrip.Visible = true;
            }
            catch(Exception ee)
            {
                ee.ToString();
                ee = null;
            }

        }
        void oListControlInsurance_ItemClosedClick(object sender, EventArgs e)
        {
            pnlTopToolStrip.Visible = true;

        }
        #endregion "Other Information Events"

        #region "Methods Retrieving Contact Information  "

        private void Fill_ContactDetails(gloPMContacts.ContactDetails Ocdet)
        {

            lvwInsurances.Items.Clear();
            lvwHospitals.Items.Clear();
            c1Template.Rows.Count = 1;
            c1Template.Rows.Add();
            c1Template.SetData(1, 1, "Medicare ID");
            c1Template.Rows.Add();
            c1Template.SetData(2, 1, "Medicaid ID");

            for (int i = 0; i < Ocdet.Count; i++)
                
            {
                int rowindex = i;
                if ((gloPMContacts.ContactType)(Ocdet[i].Type) == gloPMContacts.ContactType.Insurance)
                {
                    ListViewItem oSubListItem = new ListViewItem();
                    oSubListItem.Text = Ocdet[i].InsuranceDesc;
                    oSubListItem.SubItems.Add(Ocdet[i].InsuranceDesc);
                    oSubListItem.SubItems.Add(Ocdet[i].InsuranceCode);
                    lvwInsurances.Items.Add(oSubListItem);
                    oSubListItem = null;
                }
                if ((gloPMContacts.ContactType)(Ocdet[i].Type) == gloPMContacts.ContactType.Hospital)
                {
                    ListViewItem oSubListItem = new ListViewItem();
                    oSubListItem.Text = Ocdet[i].InsuranceDesc;
                    oSubListItem.SubItems.Add(Ocdet[i].InsuranceDesc);
                    oSubListItem.SubItems.Add(Ocdet[i].InsuranceCode);
                    lvwHospitals.Items.Add(oSubListItem);
                    oSubListItem = null;
                }
                if ((gloPMContacts.ContactType)(Ocdet[i].Type) == gloPMContacts.ContactType.MediCare)
                {
                    //c1Template.Rows.Add();
                   // c1Template.SetData(1,1,Ocdet[i].Description);
                    c1Template.SetData(1,2, Ocdet[i].Value);

                }
                if ((gloPMContacts.ContactType)(Ocdet[i].Type) == gloPMContacts.ContactType.MediCaid)
                {
                    //c1Template.Rows.Add();
                    //c1Template.SetData(2, 1, Ocdet[i].Description);
                    c1Template.SetData(2, 2, Ocdet[i].Value);

                }

                if ((gloPMContacts.ContactType)(Ocdet[i].Type) == gloPMContacts.ContactType.Miscellaneous)
                {
                    
                    c1Template.Rows.Add();
                    c1Template.SetData(c1Template.Rows.Count-2 , 1, Ocdet[i].Description);
                    c1Template.SetData(c1Template.Rows.Count-2 , 2, Ocdet[i].Value);
                    
                }
                c1Template.AllowAddNew = true;
                
            }
            
        }

        private void Fill_Physiciancontacts(gloPMContacts.Physician oPhysician)
        {
            //Business mailing address fields 
            //txt_BM_AddressLine1.Text = oPhysician.BusinessMailingAddress.AddrressLine1.Trim();
            //txt_BM_AddressLine2.Text = oPhysician.BusinessMailingAddress.AddrressLine2.Trim();
            //txt_BM_City.Text = oPhysician.BusinessMailingAddress.City.Trim();
            //txt_BM_State.Text = oPhysician.BusinessMailingAddress.State.Trim();
            //txt_BM_Zip.Text = oPhysician.BusinessMailingAddress.ZIP.Trim();
           
            //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

            oBMAddresscontrol.txtAddress1.Text = oPhysician.BusinessMailingAddress.AddrressLine1.Trim();
            oBMAddresscontrol.txtAddress2.Text = oPhysician.BusinessMailingAddress.AddrressLine2.Trim();
            oBMAddresscontrol.txtCity.Text = oPhysician.BusinessMailingAddress.City.Trim();
            oBMAddresscontrol.cmbState.Text = oPhysician.BusinessMailingAddress.State.Trim();
            oBMAddresscontrol._isTextBoxLoading = true;
            oBMAddresscontrol.txtZip.Text = oPhysician.BusinessMailingAddress.ZIP.Trim();
            oBMAddresscontrol._isTextBoxLoading = false;


            mtxt_BM_Phone.Text = oPhysician.BusinessMailingAddress.Phone.Trim();
            txt_BM_Fax.Text = oPhysician.BusinessMailingAddress.Fax.Trim();
            //txtMobile.Text = oPhysician.BusinessMailingAddress.Mobile.Trim();
            //txtPager.Text = oPhysician.BusinessMailingAddress.Pager.Trim();
            txt_BM_Email.Text = oPhysician.BusinessMailingAddress.Email.Trim();
            txt_BM_URL.Text = oPhysician.BusinessMailingAddress.URL.Trim();
            //Company address fields 
            //txt_Comp_AddressLine1.Text = oPhysician.CompanyAddress.AddrressLine1.Trim();
            //txt_Comp_AddressLine2.Text = oPhysician.CompanyAddress.AddrressLine2.Trim();
            //txt_Comp_City.Text = oPhysician.CompanyAddress.City.Trim();
            //txt_Comp_State.Text =  oPhysician.CompanyAddress.State;
            //txt_comp_ZIP.Text = oPhysician.CompanyAddress.ZIP.Trim();

            //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

            oCompAddresscontrol.txtAddress1.Text = oPhysician.CompanyAddress.AddrressLine1.Trim();
            oCompAddresscontrol.txtAddress2.Text = oPhysician.CompanyAddress.AddrressLine2.Trim();
            oCompAddresscontrol.txtCity.Text = oPhysician.CompanyAddress.City.Trim();
            oCompAddresscontrol.cmbState.Text = oPhysician.CompanyAddress.State.Trim();
            oCompAddresscontrol._isTextBoxLoading = true;
            oCompAddresscontrol.txtZip.Text = oPhysician.CompanyAddress.ZIP.Trim();
            oCompAddresscontrol._isTextBoxLoading = false;

            mtxt_Comp_Phone.Text = oPhysician.CompanyAddress.Phone.Trim();
            txt_Comp_Fax.Text = oPhysician.CompanyAddress.Fax.Trim();
            mtxt_Comp_mobile.Text = oPhysician.Mobile.Trim();
            txt_Comp_Pager.Text = oPhysician.Pager.Trim();
            txt_Comp_Email.Text = oPhysician.CompanyAddress.Email.Trim();
            txt_Comp_URL.Text = oPhysician.CompanyAddress.URL.Trim();

            //txt_Prac_AddressLine1.Text = oPhysician.PracticeLocationAddress.AddrressLine1.Trim();
            //txt_Prac_AddressLine2.Text = oPhysician.PracticeLocationAddress.AddrressLine2.Trim();
            //txt_Prac_City.Text = oPhysician.PracticeLocationAddress.City.Trim();
            //txt_Prac_State.Text = oPhysician.PracticeLocationAddress.State.Trim();            
            //txt_Prac_Zip.Text = oPhysician.PracticeLocationAddress.ZIP.Trim();
           
            //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

            oBPracAddresscontrol.txtAddress1.Text = oPhysician.PracticeLocationAddress.AddrressLine1.Trim();
            oBPracAddresscontrol.txtAddress2.Text = oPhysician.PracticeLocationAddress.AddrressLine2.Trim();
            oBPracAddresscontrol.txtCity.Text = oPhysician.PracticeLocationAddress.City.Trim();
            oBPracAddresscontrol.cmbState.Text = oPhysician.PracticeLocationAddress.State.Trim();
            oBPracAddresscontrol._isTextBoxLoading = true;
            oBPracAddresscontrol.txtZip.Text = oPhysician.PracticeLocationAddress.ZIP.Trim();
            oBPracAddresscontrol._isTextBoxLoading = false;

            mtxt_Prac_Phone.Text = oPhysician.PracticeLocationAddress.Phone.Trim();
            txt_Prac_Fax.Text = oPhysician.PracticeLocationAddress.Fax.Trim();
            //mtxt_Prac_Mobile.Text = oPhysician.PracticeLocationAddress.Mobile.Trim();
            //txt_Prac_Pager.Text = oPhysician.PracticeLocationAddress.Pager.Trim();
            txt_Prac_Email.Text = oPhysician.PracticeLocationAddress.Email.Trim();
            txt_Prac_URL.Text = oPhysician.PracticeLocationAddress.URL.Trim();


            txtfName.Text = oPhysician.FirstName.Trim();
            txtmName.Text = oPhysician.MiddleName.Trim();
            txtlName.Text = oPhysician.LastName.Trim();
            //If Gender is blank or null then set it to default as a Male.
            //if (oPhysician.Gender.Trim() == rbGender1.Text || string.IsNullOrEmpty(oPhysician.Gender))
            if (oPhysician.Gender.Trim() == rbGender1.Text)
            {
                rbGender1.Checked = true;
            }
            else if (oPhysician.Gender.Trim() == rbGender2.Text)
            {
                rbGender2.Checked = true;
            }

            //txtPager.Text = oPhysician.Pager.Trim();
            cmbSpecialty.SelectedValue = oPhysician.SpecialtyID;

            if (oPhysician.Taxonomy != "")
            {
                txtTaxonomy.Text = oPhysician.Taxonomy + "-" + oPhysician.TaxonomyDesc;
            }

            txtTaxID.Text = oPhysician.TaxID;
            txtUPIN.Text = oPhysician.UPIN;
            txtNPI.Text = oPhysician.NPI;
            txtNotes.Text = oPhysician.Notes;


            // Sandip Darade case GLO2010-0004426 
            txtPrefix.Text = oPhysician.Prefix;
            txtSuffix.Text = oPhysician.Degree;

            txtSPI.Text = oPhysician.SPI;
            txtDirectAddress.Text = oPhysician.DirectAddress;

            txtSpecialtyType.Text = oPhysician.SpecialtyType;
            txtClinicName.Text = oPhysician.ClinicName;

            //Mahesh Nawal 20102307
            //if (oPhysician.PARequired.Equals(gloPMContacts.PriorAuthorizationRequired.No))
            //{ rbNo.Checked = true; }
            //else if (oPhysician.PARequired.Equals(gloPMContacts.PriorAuthorizationRequired.Always))
            //{ rbAlways.Checked = true; }
            //else if (oPhysician.PARequired.Equals(gloPMContacts.PriorAuthorizationRequired.UsePlanSetting))
            //{ rbUsePlanSetting.Checked = true; }
        }

        #endregion

        private void chBoxBMAds_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxBMAds.Checked == true)
                {
                    //txt_BM_AddressLine1.Text = txt_Comp_AddressLine1.Text;
                    //txt_BM_AddressLine2.Text = txt_Comp_AddressLine2.Text;
                    //txt_BM_City.Text = txt_Comp_City.Text;
                    //txt_BM_Zip.Text = txt_comp_ZIP.Text;
                   
                    //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                    oBMAddresscontrol.txtAddress1.Text = oCompAddresscontrol.txtAddress1.Text;
                    oBMAddresscontrol.txtAddress2.Text = oCompAddresscontrol.txtAddress2.Text;
                    oBMAddresscontrol.txtCity.Text = oCompAddresscontrol.txtCity.Text;
                    oBMAddresscontrol._isTextBoxLoading = true;
                    oBMAddresscontrol.txtZip.Text = oCompAddresscontrol.txtZip.Text;
                    oBMAddresscontrol._isTextBoxLoading = false;
                    oBMAddresscontrol.cmbState.Text = oCompAddresscontrol.cmbState.Text;

                    mtxt_BM_Phone.Text = mtxt_Comp_Phone.Text;
                    txt_BM_Fax.Text = txt_Comp_Fax.Text;
                    txt_BM_Email.Text = txt_Comp_Email.Text;
                    txt_BM_URL.Text = txt_Comp_URL.Text;

                    
                }
                else
                {
                    //txt_BM_AddressLine1.Clear();
                    //txt_BM_AddressLine2.Clear();
                    //txt_BM_City.Clear();
                    //txt_BM_Zip.Clear();

                    //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                    oBMAddresscontrol.txtAddress1.Clear();
                    oBMAddresscontrol.txtAddress2.Clear();
                    oBMAddresscontrol.txtCity.Clear();
                    oBMAddresscontrol._isTextBoxLoading = true;
                    oBMAddresscontrol.txtZip.Clear();
                    oBMAddresscontrol._isTextBoxLoading = false;
                    oBMAddresscontrol.cmbState.Text = "";

                    mtxt_BM_Phone.Clear();
                    txt_BM_Fax.Clear();
                    txt_BM_Email.Clear();
                    txt_BM_URL.Clear();
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void chBoxPracAdds_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chBoxPracAdds.Checked == true)
                {
                    //txt_Prac_AddressLine1.Text = txt_Comp_AddressLine1.Text;
                    //txt_Prac_AddressLine2.Text = txt_Comp_AddressLine2.Text;
                    //txt_Prac_City.Text = txt_Comp_City.Text;
                    //txt_Prac_Zip.Text = txt_comp_ZIP.Text;

                    //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                    oBPracAddresscontrol.txtAddress1.Text = oCompAddresscontrol.txtAddress1.Text;
                    oBPracAddresscontrol.txtAddress2.Text = oCompAddresscontrol.txtAddress2.Text;
                    oBPracAddresscontrol.txtCity.Text = oCompAddresscontrol.txtCity.Text;
                    oBPracAddresscontrol._isTextBoxLoading = true;
                    oBPracAddresscontrol.txtZip.Text = oCompAddresscontrol.txtZip.Text;
                    oBPracAddresscontrol._isTextBoxLoading = false;
                    oBPracAddresscontrol.cmbState.Text = oCompAddresscontrol.cmbState.Text;

                    mtxt_Prac_Phone.Text = mtxt_Comp_Phone.Text;
                    txt_Prac_Fax.Text = txt_Comp_Fax.Text;
                    txt_Prac_Email.Text = txt_Comp_Email.Text;
                    txt_Prac_URL.Text = txt_Comp_URL.Text;
                }
                else
                {
                    //txt_Prac_AddressLine1.Clear();
                    //txt_Prac_AddressLine2.Clear();
                    //txt_Prac_City.Clear();
                    //txt_Prac_Zip.Clear();

                    //Sandip Darade 20091010 gloAddress control implemented  replacing code for address info above with code below 

                    oBPracAddresscontrol.txtAddress1.Clear();
                    oBPracAddresscontrol.txtAddress2.Clear();
                    oBPracAddresscontrol.txtCity.Clear();
                    oBPracAddresscontrol._isTextBoxLoading = true;
                    oBPracAddresscontrol.txtZip.Clear();
                    oBPracAddresscontrol._isTextBoxLoading = false;
                    oBPracAddresscontrol.cmbState.Text = "";

                    mtxt_Prac_Phone.Clear();
                    txt_Prac_Fax.Clear();
                    txt_Prac_Email.Clear();
                    txt_Prac_URL.Clear();
                }
            }
            catch (Exception ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void txt_comp_ZIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // //code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{

            //    e.Handled = true;
            //}
            //Sandip Darade 20090925
            //allow alpha numeric characters for zip 
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
            }

        }

        private void txt_BM_Zip_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{

            //    e.Handled = true;
            //}
            //Sandip Darade 20090925
            //allow alpha numeric characters for zip 
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
            }

        }

        private void txt_Prac_Zip_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{

            //    e.Handled = true;
            //}
            //Sandip Darade 20090925
            //allow alpha numeric characters for zip 
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
            }

        }

        private void c1Template_BeforeEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
             if ((e.Col == 1 && e.Row == 1) || (e.Col == 1 && e.Row == 2))
             {
                 e.Cancel = true;
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
   
        private void txt_Comp_Email_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txt_Comp_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
  
        private void txt_BM_Email_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txt_BM_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void txt_Prac_Email_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txt_Prac_Email.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtTaxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete;
        }

        private void txt_URL_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtURL = (TextBox)sender;
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
            if (txtURL != null) { txtURL.Dispose(); txtURL = null; }
        }

        public bool CheckURL(string input)
        {
            bool response = false;

            System.Globalization.CompareInfo cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;

            //if (cmpUrl.IsPrefix(input, "http://") == false)
            //{ input = "http://" + input; }

            Regex RgxUrl = new Regex("^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\\+.~#?&//=]+$");

            if (RgxUrl.IsMatch(input))
            { response = true; }
            else
            { response = false; }
            RgxUrl = null;

            return response;
        }

        private void DesignAlternateGrid()
        {
            //'Design Additional Billing IDs Grid 
            c1PhysicianAdditionalIDs.Rows.Fixed = 1;
            c1PhysicianAdditionalIDs.Cols.Fixed = 0;
            c1PhysicianAdditionalIDs.Rows.Count = 1;
            c1PhysicianAdditionalIDs.Cols.Count = 5;

            c1PhysicianAdditionalIDs.SetData(0, COL_QUALIFIERID, "QualifierID");
            c1PhysicianAdditionalIDs.SetData(0, COL_QUALIFIERCODE, "Code");
            c1PhysicianAdditionalIDs.SetData(0, COL_QUALIFIERDESCRIPTION, "Description");
            c1PhysicianAdditionalIDs.SetData(0, COL_VALUE, "Value");
            c1PhysicianAdditionalIDs.SetData(0, COL_ISSYSTEM, "bIsSystem");

            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERID].DataType = typeof(System.Int64);
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERCODE].DataType = typeof(System.String);
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERDESCRIPTION].DataType = typeof(System.String);
            c1PhysicianAdditionalIDs.Cols[COL_VALUE].DataType = typeof(System.String);
            c1PhysicianAdditionalIDs.Cols[COL_ISSYSTEM].DataType = typeof(System.Boolean);

          //  Int32 _width = 700;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERID].Width = 0;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERCODE].Width = 0;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERDESCRIPTION].Width = 300;
            c1PhysicianAdditionalIDs.Cols[COL_VALUE].Width = 425;
            c1PhysicianAdditionalIDs.Cols[COL_ISSYSTEM].Width = 0;

            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERCODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERDESCRIPTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PhysicianAdditionalIDs.Cols[COL_VALUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERID].AllowEditing = false;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERCODE].AllowEditing = false;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERDESCRIPTION].AllowEditing = false;
            c1PhysicianAdditionalIDs.Cols[COL_VALUE].AllowEditing = true;
            c1PhysicianAdditionalIDs.Cols[COL_ISSYSTEM].AllowEditing = false;

            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERID].Visible = false;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERCODE].Visible = false;
            c1PhysicianAdditionalIDs.Cols[COL_QUALIFIERDESCRIPTION].Visible = true;
            c1PhysicianAdditionalIDs.Cols[COL_VALUE].Visible = true;
            c1PhysicianAdditionalIDs.Cols[COL_ISSYSTEM].Visible = false;

            DataTable dtQualifierID = null;
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            dtQualifierID = ogloSettings.getIDQualifiers(5, _ContactID, true);
            ogloSettings.Dispose();
            ogloSettings = null;
            if (dtQualifierID != null && dtQualifierID.Rows.Count > 0)
            {
                for (int i = 0; i <= dtQualifierID.Rows.Count - 1; i++)
                {
                    c1PhysicianAdditionalIDs.Rows.Add();
                    Int32 RowIndex = c1PhysicianAdditionalIDs.Rows.Count - 1;
                    c1PhysicianAdditionalIDs.SetData(RowIndex, COL_QUALIFIERID, Convert.ToString(dtQualifierID.Rows[i]["nQualifierID"]));
                    c1PhysicianAdditionalIDs.SetData(RowIndex, COL_QUALIFIERCODE, Convert.ToString(dtQualifierID.Rows[i]["sCode"]));
                    c1PhysicianAdditionalIDs.SetData(RowIndex, COL_QUALIFIERDESCRIPTION, Convert.ToString(dtQualifierID.Rows[i]["sAdditionalDescription"]));
                    c1PhysicianAdditionalIDs.SetData(RowIndex, COL_VALUE, Convert.ToString(dtQualifierID.Rows[i]["sValue"]));
                    c1PhysicianAdditionalIDs.SetData(RowIndex, COL_ISSYSTEM, Convert.ToString(dtQualifierID.Rows[i]["bIsSystem"]));
                }
            }

            if (dtQualifierID != null) { dtQualifierID.Dispose(); dtQualifierID = null; }


        }

        private void c1ProviderIdentification_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            c1PhysicianAdditionalIDs.Editor = (TextBox)c1PhysicianAdditionalIDs.Editor;
        }

        private void c1ProviderIdentification_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_VALUE)
            {
                ((TextBox)c1PhysicianAdditionalIDs.Editor).MaxLength = 250;
            }
        }

        private void c1ProviderIdentification_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1FlexGrid)sender), e.Location);
        }


      
    }
}