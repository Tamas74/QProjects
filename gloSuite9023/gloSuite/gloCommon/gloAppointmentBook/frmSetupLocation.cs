using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloAddress;
using System.Text.RegularExpressions;

namespace gloAppointmentBook
{
    internal partial class frmSetupLocation : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _LocationID = 0;
        private Int64 _ReturnLocationID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        //Added By Pramod Nair For Filling The States 20090716
        private String sState = "";
        gloAddress.gloAddressControl oAddresscontrol;
      

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64  ReturnLocationID
        {
            get { return _ReturnLocationID; }
            set { _ReturnLocationID = value; }
        }


        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupLocation(string databaseconnectionstring)
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

        public frmSetupLocation(Int64 LocationID, string databaseconnectionstring)
        {
            InitializeComponent();
            _LocationID = LocationID;
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

        private void frmSetupLocation_Load(object sender, EventArgs e)
        {
            try
            {
                oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
                oAddresscontrol.Dock = DockStyle.Fill;
                pnlAddresControl.Controls.Add(oAddresscontrol);


                if (_LocationID != 0)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    DataTable dt;
                    String strQuery = "";
                    strQuery = "SELECT ISNULL(sLocation,'') as sLocation,ISNULL(sAddressLine1,'')AS AddressLine1 ,ISNULL(sAddressLine2,'')AS AddressLine2,ISNULL(sCity,'')AS City ,ISNULL(sState,'')AS State,ISNULL(sZIP,'')AS ZIP,ISNULL(sCounty,'')AS County, bISDefault,ISNULL(sCountry,'')AS sCountry, bIsTurnOffReminders FROM AB_Location WHERE nLocationID = " + _LocationID.ToString();
                    oDB.Retrive_Query(strQuery.ToString(), out  dt);
                    oDB.Disconnect();
                    oDB.Dispose();

                    strQuery = null;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            //txtLocation.Text = dt.Rows[0]["sLocation"].ToString(); // Resourse Description
                            //txtAddress1.Text = dt.Rows[0]["AddressLine1"].ToString();
                            //txtAddress2.Text = dt.Rows[0]["AddressLine2"].ToString();
                            //txtCity.Text = dt.Rows[0]["City"].ToString();
                            //txtZip.Text = dt.Rows[0]["ZIP"].ToString();
                            //txtCounty.Text = dt.Rows[0]["County"].ToString();

                            txtLocation.Text = dt.Rows[0]["sLocation"].ToString(); // Resourse Description
                            oAddresscontrol.txtAddress1.Text = dt.Rows[0]["AddressLine1"].ToString();
                             oAddresscontrol.txtAddress2.Text = dt.Rows[0]["AddressLine2"].ToString();
                             oAddresscontrol.txtCity.Text = dt.Rows[0]["City"].ToString();
                             oAddresscontrol.isFormLoading = true; 
                            oAddresscontrol.txtZip.Text = dt.Rows[0]["ZIP"].ToString();
                            oAddresscontrol.isFormLoading = false ;
                             oAddresscontrol.txtCounty.Text = dt.Rows[0]["County"].ToString();

                             oAddresscontrol.cmbCountry.Text = dt.Rows[0]["sCountry"].ToString();
                            //commented By Pramod Nair For Filling The States 20090716
                            //cmbState.SelectedText = dt.Rows[0]["State"].ToString();

                            //Added By Pramod Nair For Filling The States 20090716
                             //oAddresscontrol.cmbState.Text    =dt.Rows[0]["State"].ToString();
                            sState = dt.Rows[0]["State"].ToString();


                            //cmbState.Items.Add(dt.Rows[0]["State"].ToString());
                            //cmbState.SelectedText = dt.Rows[0]["State"].ToString();
                            chkDefault.Checked = Convert.ToBoolean(dt.Rows[0]["bISDefault"]);
                            chkTurnOffReminders.Checked = Convert.ToBoolean(dt.Rows[0]["bIsTurnOffReminders"]);
                        }
                    }
                    dt.Dispose();
                    
                }
                fillStates(sState);
                txtLocation.Select();
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
        }//function

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Books.Location oLocation = null;
            Books.Location oLocation1 = null;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (txtLocation.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Location.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtLocation.Select();
                            break;
                        }

                        oLocation = new Books.Location();

                        if (oLocation.IsExists(_LocationID, txtLocation.Text.Trim()) == true)
                        {
                            MessageBox.Show("Location with same name already exists, Enter unique Location name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtLocation.Select();
                            break;
                        }

                        oLocation.LocationID = _LocationID;
                        oLocation.LocationName = txtLocation.Text.Trim();
                        //oLocation.AddressLine1 = txtAddress1.Text.Trim();
                        //oLocation.AddressLine2 = txtAddress2.Text.Trim();
                        //oLocation.City = txtCity.Text.Trim();
                        //if (cmbState.SelectedIndex != -1)
                        //{
                        //    oLocation.State = cmbState.Text;
                        //}
                        //oLocation.ZIP = txtZip.Text.Trim();
                        //oLocation.County = txtCounty.Text.Trim();

                        oLocation.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                        oLocation.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                        oLocation.City = oAddresscontrol.txtCity.Text.Trim();
                        //if (oAddresscontrol.cmbState.SelectedIndex != -1)
                        //{
                        oLocation.State = oAddresscontrol.cmbState.Text;
                        //}
                        oLocation.ZIP = oAddresscontrol.txtZip.Text.Trim();
                        oLocation.County = oAddresscontrol.txtCounty.Text.Trim();
                        oLocation.Country = oAddresscontrol.cmbCountry.Text;

                        oLocation.IsBlocked = false;
                        oLocation.IsDefault = chkDefault.Checked;

                        if (chkDefault.Checked)
                        {
                            string previousDefault = string.Empty;

                            DataTable dtDefault = oLocation.GetDefaultLocation();
                            if (dtDefault != null && dtDefault.Rows.Count > 0)
                            { previousDefault = Convert.ToString(dtDefault.Rows[0]["sLocation"]); }

                            if ((oLocation.LocationName != previousDefault) && (!string.IsNullOrEmpty(previousDefault)))
                            {
                                if (MessageBox.Show("Are you sure you want to change the default Location, " + previousDefault + " ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    break;
                                }
                            }
                            dtDefault.Dispose();
                            previousDefault = null;
                        }

                        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                        //oLocation.ClinicID = 1;
                        oLocation.ClinicID = ClinicID;
                        //

                        oLocation.IsTurnOffReminders = chkTurnOffReminders.Checked;

                        if (_LocationID == 0)
                        {
                            _ReturnLocationID = oLocation.Add();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.Add, "Add Location", 0, _ReturnLocationID, 0, ActivityOutCome.Success);

                            if (_ReturnLocationID < 0)
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Location not added, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLocation.Select();

                                break;
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.Add, "Add Location", 0, _LocationID, 0, ActivityOutCome.Success);

                            if (oLocation.Modify() == false)
                            {
                                MessageBox.Show("Location not modified, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                txtLocation.Select();
                                break;
                            }
                        }

                        // Record is Added Successfully
                        //frmViewResourceType oRefResTypes;
                        //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                        //oRefResTypes.FillResourceTypes();

                        //'oRefResTypes = null;
                        txtLocation.Text = "";
                        txtLocation.Select();
                        // ClearControls();
                        //if (_LocationID != 0)
                        //{
                        //    this.Close();
                        //}
                        this.Close();
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        {
                            if (txtLocation.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter the Location.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLocation.Select();
                                break;
                            }

                            oLocation1 = new Books.Location();

                            if (oLocation1.IsExists(_LocationID, txtLocation.Text.Trim()) == true)
                            {
                                MessageBox.Show("Location with same name already exists, Enter unique Location name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLocation.Select();
                                break;
                            }

                            oLocation1.LocationID = _LocationID;
                            oLocation1.LocationName = txtLocation.Text.Trim();
                            //oLocation1.AddressLine1 = txtAddress1.Text.Trim();
                            //oLocation1.AddressLine2 = txtAddress2.Text.Trim();
                            //oLocation1.City = txtCity.Text.Trim();
                            //if (cmbState.SelectedIndex != -1)
                            //{
                            //    oLocation1.State = cmbState.Text;
                            //}
                            //oLocation1.ZIP = txtZip.Text.Trim();
                            //oLocation1.County = txtCounty.Text.Trim();
                            oLocation1.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                            oLocation1.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                            oLocation1.City = oAddresscontrol.txtCity.Text.Trim();
                            //if (oAddresscontrol.cmbState.SelectedIndex != -1)
                            //{
                            oLocation1.State = oAddresscontrol.cmbState.Text;
                            //}
                            oLocation1.ZIP = oAddresscontrol.txtZip.Text.Trim();
                            oLocation1.County = oAddresscontrol.txtCounty.Text.Trim();
                            oLocation1.Country = oAddresscontrol.cmbCountry.Text.Trim();


                            oLocation1.IsBlocked = false;
                            oLocation1.IsDefault = chkDefault.Checked;

                            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                            //oLocation.ClinicID = 1;
                            oLocation1.ClinicID = ClinicID;
                            //

                            oLocation1.IsTurnOffReminders = chkTurnOffReminders.Checked;

                            if (_LocationID == 0)
                            {
                                _ReturnLocationID = oLocation1.Add();
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.Add, "Add Location", 0, _ReturnLocationID, 0, ActivityOutCome.Success);

                                if (_ReturnLocationID < 0)
                                {
                                    // Record is Not Added Successfully
                                    MessageBox.Show("Location not added, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtLocation.Select();

                                    break;
                                }
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.Add, "Add Location", 0, _LocationID, 0, ActivityOutCome.Success);

                                if (oLocation1.Modify() == false)
                                {
                                    MessageBox.Show("Location not modified, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                    txtLocation.Select();
                                    break;
                                }
                            }

                            // Record is Added Successfully
                            //frmViewResourceType oRefResTypes;
                            //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                            //oRefResTypes.FillResourceTypes();

                            //'oRefResTypes = null;
                            _LocationID = 0;
                            txtLocation.Text = "";
                            txtLocation.Select();
                            ClearControls();
                            //if (_LocationID != 0)
                            //{
                            //    this.Close();
                            //}

                            break;
                        }

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
                if (oLocation != null) { oLocation.Dispose(); oLocation = null; }
                if (oLocation1 != null) { oLocation1.Dispose(); oLocation1 = null; }
            }
        }

        #endregion " Tool Strip Event "

        private void ClearControls()
        {
            //txtAddress1.Clear();
            //txtAddress2.Clear();
            //txtCity.Clear();
            //txtZip.Clear();
            //txtCounty.Clear();
            //cmbState.SelectedIndex = -1;
           oAddresscontrol.txtAddress1.Clear();
            oAddresscontrol.txtAddress2.Clear();
            oAddresscontrol.txtCity.Clear();
            oAddresscontrol.txtZip.Clear();
            oAddresscontrol.txtCounty.Clear();
            oAddresscontrol.cmbState.SelectedIndex = -1;
            oAddresscontrol.cmbCountry.SelectedIndex = -1; 

        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code to allow nos only 
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }

        //private void txtZip_Leave(object sender, EventArgs e)
        //{
        //    if (txtZip.Text.Trim() != "")
        //    {
        //        DataTable dt = new System.Data.DataTable();
        //        gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        try
        //        {
        //            oDb.Connect(false);
        //            string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtZip.Text.Trim() + "'";
                    
        //            //txtCity.Text = "";
        //            cmbState.Text = "";
        //            // txtPACountry.Text = "";

        //            oDb.Retrive_Query(qry, out dt);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                //Commented By Pramod Nair For Filling The States 20090716
        //                //cmbState.Items.Add(Convert.ToString(dt.Rows[0]["ST"]));
        //                //cmbState.SelectedItem = Convert.ToString(dt.Rows[0]["ST"]);

        //                //Added By Pramod Nair For Filling The States 20090716
        //                //cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);
        //                oAddresscontrol.cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);

        //                if (oAddresscontrol.txtCity.Text.Trim() == "")
        //                {
        //                    oAddresscontrol.txtCity.Text = Convert.ToString(dt.Rows[0]["City"]);
        //                }
        //                oAddresscontrol.txtCounty.Text = Convert.ToString(dt.Rows[0]["County"]);
        //            }
        //            else
        //            {
        //                //cmbState.Items.Clear();
        //                txtCity.Text = "";
        //                txtCounty.Text = "";
        //            }
        //        }
        //        catch (gloDatabaseLayer.DBException ex)
        //        {
        //            ex.ERROR_Log(ex.ToString());
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        }
        //        catch (Exception ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        }
        //        finally
        //        {
        //            dt.Dispose();
        //            oDb.Disconnect();
        //            oDb.Dispose();
        //        }
        //    }
        //    else
        //    {
        //        //cmbState.Items.Clear();
        //        cmbState.Text = "";
        //        txtCity.Text = "";
        //        txtCounty.Text = "";
        //    }

        //}
     

        #region " Button Click Events "
        #endregion " Button Click Events "


        //Added By Pramod For Filling The States 20090716
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

                    oAddresscontrol.cmbState.DataSource = dtStates;
                    oAddresscontrol.cmbState.DisplayMember = "ST";
                }

                if (_States != "")
                {
                    oAddresscontrol.cmbState.Text = _States;
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

        private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }

    }
}