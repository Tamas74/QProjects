using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using gloAddress;
using gloCommon;

namespace gloAppointmentBook
{
    public partial class frmSetupOccupation : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _OccupationID = 0;
        private Int64 _ReturnOccupationID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        Int64 _ClinicID = 0;
        string _OccupationName = String.Empty;
        string _EmployerName = String.Empty;
        //Int64 _OccupationId = 0;

        gloAddress.gloAddressControl ogloAddressControl;
        


        //Added By Pramod Nair For Filling The States 20090716
        private String sState = "";


        //MaheshB
        bool _showemployertextbox=false;
        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64  ReturnOccupationID
        {
            get { return _ReturnOccupationID; }
            set { _ReturnOccupationID = value; }
        }

        public string OccupationCountry
        {
            get;
            set;
        }

        public bool showemployertextbox
        {
            get { return _showemployertextbox; }
            set { _showemployertextbox = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupOccupation(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
         
        }

        public frmSetupOccupation(Int64 OccupationID, string databaseconnectionstring)
        {
            InitializeComponent();
            _OccupationID = OccupationID;
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
         
        }


        public frmSetupOccupation(Int64 OccupationID, string databaseconnectionstring, string OccupationName, string EmployerName)
        {
            InitializeComponent();
            _OccupationID = OccupationID;
            _OccupationName = OccupationName;
            _EmployerName = EmployerName;
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        }
        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupOccupation_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
            tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

            try
            {
                //Added the gloaddresscontrol.
                ogloAddressControl = new gloAddress.gloAddressControl(_databaseconnectionstring);
                pnlAddresssControl.Controls.Add(ogloAddressControl);


                if (_EmployerName != "" || _OccupationID != 0)//For New But Values from Occupation Control
                {
                    if (_OccupationID != 0)//|| _OccupationName != ""
                    {
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        oDB.Connect(false);
                        DataTable dt = null;
                        String strQuery = "";
                        if (_OccupationID != 0)
                        {
                            strQuery = "SELECT isnull(sOccupation,'') as sOccupation,isnull(sEmployerName,'') as sEmployerName,isnull(sPlaceofEmployment,'') as sPlaceofEmployment,isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as sWorkCity,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(sWorkCountry,'') as sWorkCountry,isnull(sWorkPhone,'') as sWorkPhone,isnull(sWorkMobile,'') as sWorkMobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as sWorkEmail,ISNULL(sCountry,'') AS sCountry FROM AB_Occupation_MST WHERE nOccupationID = " + _OccupationID.ToString();
                        }
                        else if (_OccupationName != "")
                        {
                            strQuery = "SELECT isnull(sOccupation,'') as sOccupation,isnull(sEmployerName,'') as sEmployerName,isnull(sPlaceofEmployment,'') as sPlaceofEmployment,isnull(sWorkAddress1,'') as sWorkAddress1,isnull(sWorkAddress2,'') as sWorkAddress2,isnull(sWorkCity,'') as sWorkCity,isnull(sWorkState,'') as sWorkState,isnull(sWorkZip,'') as sWorkZip,isnull(sWorkCountry,'') as sWorkCountry,isnull(sWorkPhone,'') as sWorkPhone,isnull(sWorkMobile,'') as sWorkMobile,isnull(sWorkFax,'') as sWorkFax,isnull(sWorkEmail,'') as sWorkEmail,ISNULL(sCountry,'') AS sCountry FROM AB_Occupation_MST WHERE sOccupation = '" + _OccupationName.ToString().Replace("'", "''").Trim() + "'";
                        }
                        if (strQuery != "")
                        {
                            oDB.Retrive_Query(strQuery.ToString(), out  dt);
                        }
                        oDB.Disconnect();
                        oDB.Dispose();
                        strQuery = null;

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                txtOccupation.Text = dt.Rows[0]["sOccupation"].ToString(); // Resourse Description
                                txtEmployer.Text = dt.Rows[0]["sEmployerName"].ToString();
                                txtPlaceOfEmployment.Text = dt.Rows[0]["sPlaceofEmployment"].ToString();
                                //Dhruv 20100109 
                                //to not to open the zip control when loading
                                ogloAddressControl.isFormLoading = true;
                                //added the gloAddress control
                                ogloAddressControl.txtAddress1.Text = dt.Rows[0]["sWorkAddress1"].ToString();
                                ogloAddressControl.txtAddress2.Text = dt.Rows[0]["sWorkAddress2"].ToString();
                                ogloAddressControl.txtCity.Text = dt.Rows[0]["sWorkCity"].ToString();
                                ogloAddressControl.txtCounty.Text = dt.Rows[0]["sWorkCountry"].ToString();
                                ogloAddressControl.txtZip.Text = dt.Rows[0]["sWorkZip"].ToString();

                                //ogloAddressControl.cmbCountry.Text = dt.Rows[0]["sCountry"].ToString();


                                for (int i = 0; i <= ogloAddressControl.cmbCountry.Items.Count - 1; i++)
                                {
                                    if (dt.Rows[0]["sCountry"].ToString() == Convert.ToString(((DataRowView)ogloAddressControl.cmbCountry.Items[i])["sName"]))
                                    {
                                        ogloAddressControl.cmbCountry.SelectedIndex = i;
                                        break;
                                    }
                                }
                                ogloAddressControl.cmbState.Text = dt.Rows[0]["sWorkState"].ToString();
                                ogloAddressControl.isFormLoading = false;

                                //
                                //commented as new gloaddress control has been launched.
                                //txtAddress1.Text = dt.Rows[0]["sWorkAddress1"].ToString();
                                //txtAddress2.Text = dt.Rows[0]["sWorkAddress2"].ToString();
                                //txtCity.Text = dt.Rows[0]["sWorkCity"].ToString();
                                //txtZip.Text = dt.Rows[0]["sWorkZip"].ToString();
                                //txtCounty.Text = dt.Rows[0]["sWorkCountry"].ToString();

                                txtPhone.Text = dt.Rows[0]["sWorkPhone"].ToString();
                                txtMobile.Text = dt.Rows[0]["sWorkMobile"].ToString();
                                mtxtFax.Text = dt.Rows[0]["sWorkFax"].ToString();
                                txtEmail.Text = dt.Rows[0]["sWorkEmail"].ToString();


                                //commented By Pramod Nair For Filling The States 20090716
                                //cmbState.SelectedText = dt.Rows[0]["State"].ToString();

                                //Added By Pramod Nair For Filling The States 20090716

                                //sState = dt.Rows[0]["sWorkState"].ToString();

                                //cmbState.Items.Add(dt.Rows[0]["State"].ToString());
                                //cmbState.SelectedText = dt.Rows[0]["State"].ToString();
                                //chkDefault.Checked = Convert.ToBoolean(dt.Rows[0]["bISDefault"]);

                            }
                        }
                        dt.Dispose();

                    }
                    else
                    {
                        txtOccupation.Text = _OccupationName; // Resourse Description
                        txtEmployer.Text = _EmployerName;
                    }
                }
                else
                {
                    txtOccupation.Text = _OccupationName; // Resourse Description
                    txtEmployer.Text = _EmployerName;
                }
                if (showemployertextbox == true)
                {
                    txtEmployer.Enabled = true;
                }
                else
                {
                    //txtEmployer.Text = "";
                    txtEmployer.Enabled = false;
                }

                fillStates(sState);
                txtEmployer.Select();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (tabSettings != null) { tabSettings = null; }
            }
        }//function

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":

                        if (txtEmployer.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Employer name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtEmployer.Select();
                            break;
                        }
                        
                        if (txtOccupation.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Occupation name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOccupation.Select();
                            break;
                        }

                        if (txtPhone.Text.Length != 0 && txtPhone.MaskFull == false)
                        {
                            MessageBox.Show("Phone details are incomplete.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtPhone.Focus();
                            break;
                        }
                        if (txtMobile.Text.Length != 0 && txtMobile.MaskFull == false)
                        {
                            MessageBox.Show("Mobile details are incomplete.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtMobile.Focus();
                            break;
                        }

                        if (CheckEmailAddress(txtEmail.Text) == false)
                        {
                            MessageBox.Show("Enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtEmail.Focus();
                            break;
                        }

                        Books.Occupation oOccupation = new Books.Occupation();

                        if (oOccupation.IsExists(_OccupationID, txtOccupation.Text.Trim(),txtEmployer.Text.Replace("'","''")) == true)
                        {
                            MessageBox.Show("Occupation with same name already exists, Enter unique Occupation name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOccupation.Select();
                            break;
                        }

                        oOccupation.OccupationID = _OccupationID;
                        oOccupation.OccupationName = txtOccupation.Text.Trim();
                        oOccupation.EmployerName = txtEmployer.Text.Trim();
                        oOccupation.PlaceofEmployment = txtPlaceOfEmployment.Text.Trim();
                        oOccupation.AddressLine1 = ogloAddressControl.txtAddress1.Text.Trim();
                        oOccupation.AddressLine2 = ogloAddressControl.txtAddress2.Text.Trim();
                        oOccupation.City = ogloAddressControl.txtCity.Text.Trim();
                        //oOccupation.AddressLine1 = txtAddress1.Text.Trim();
                        //oOccupation.AddressLine2 = txtAddress2.Text.Trim();
                        //oOccupation.City = txtCity.Text.Trim();
                        if (cmbState.SelectedIndex != -1)
                        {
                            //oOccupation.State = cmbState.Text;
                            oOccupation.State = ogloAddressControl.cmbState.Text;
                        }
                        //oOccupation.ZIP = txtZip.Text.Trim();
                        //oOccupation.County = txtCounty.Text.Trim();
                        oOccupation.ZIP = ogloAddressControl.txtZip.Text.Trim();
                        oOccupation.County = ogloAddressControl.txtCounty.Text.Trim();
                        oOccupation.Phone = txtPhone.Text.Trim();
                        oOccupation.Mobile = txtMobile.Text.Trim();
                        oOccupation.Fax = mtxtFax.Text.Trim();
                        oOccupation.Email = txtEmail.Text.Trim();
                      
                            oOccupation.Country = ogloAddressControl.cmbCountry.Text ;
                      
                        oOccupation.ClinicID = ClinicID;

                        if (_OccupationID == 0)
                        {
                            _ReturnOccupationID = oOccupation.Add();
                            OccupationCountry = oOccupation.Country;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Occupation", 0, _ReturnOccupationID, 0, ActivityOutCome.Success);                

                            if (_ReturnOccupationID < 0)
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Occupation not added, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtOccupation.Select();

                                break;
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Add, "Add Occupation", 0, _OccupationID, 0, ActivityOutCome.Success);

                            if (oOccupation.Modify() == false)
                            {
                                MessageBox.Show("Occupation not modified, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                txtOccupation.Select();
                                break;
                            }
                            else
                            {
                                OccupationCountry = oOccupation.Country;
                                _ReturnOccupationID = oOccupation.OccupationID;
                            }
                        }
                        
                        //txtOccupation.Text = "";
                        //txtOccupation.Select();
                        ClearControls();
                        this.Close();
                        break;
                    case "Cancel":
                        this.Close();
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

        #endregion " Tool Strip Event "

        private void ClearControls()
        {
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtZip.Clear();
            txtCounty.Clear();
            txtEmail.Clear();
            mtxtFax.Clear();
            txtMobile.Clear();
            txtPhone.Clear();
            txtEmployer.Clear();
            txtPlaceOfEmployment.Clear();
            cmbState.SelectedIndex = -1;
            cmbCountry.SelectedIndex = -1;
        }


        #region " Form Control Events "

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Commented To accepts the Alphanumeric Value also.
            //code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtZip.Text.Trim() + "'";
                    
                    //txtCity.Text = "";
                    cmbState.Text = "";
                    // txtPACountry.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    qry = null;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //Commented By Pramod Nair For Filling The States 20090716
                        //cmbState.Items.Add(Convert.ToString(dt.Rows[0]["ST"]));
                        //cmbState.SelectedItem = Convert.ToString(dt.Rows[0]["ST"]);

                        //Added By Pramod Nair For Filling The States 20090716
                        cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtCity.Text.Trim() == "")
                        {
                            txtCity.Text = Convert.ToString(dt.Rows[0]["City"]);
                        }
                        txtCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        cmbCountry.Text = "US";
                    }
                    else
                    {
                        //cmbState.Items.Clear();
                        txtCity.Text = "";
                        txtCounty.Text = "";
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
                //cmbState.Items.Clear();
                cmbState.Text = "";
                txtCity.Text = "";
                txtCounty.Text = "";
            }

        }

        #endregion

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

        private void txttxtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion


        #region " Button Click Events "
        #endregion " Button Click Events "

        #region "Fill Methods"

        private void fillStates(String _States)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = null;
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();
                _sqlQuery = null;

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "ST";
                }

                if (_States != "")
                {
                    cmbState.Text = _States;
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

        #endregion

    }
}