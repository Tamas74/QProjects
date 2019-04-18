using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace gloContacts
{
    public partial class frmSetupCollectionAgency : Form
    {
        private string _databaseconnectionstring = "";
        gloAddress.gloAddressControl oAddresscontrol = null;
        private string _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        private long ContactId = 0;

        public frmSetupCollectionAgency()
        {
            InitializeComponent();
        }

        public frmSetupCollectionAgency(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
        }

        public frmSetupCollectionAgency(string DatabaseConnectionString, long contact)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            ContactId = contact;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }


        private void frmSetupCollectionAgency_Load(object sender, EventArgs e)
        {
            try
            {
                oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
                oAddresscontrol.Dock = DockStyle.Fill;
                pnlAddresssControl.Controls.Add(oAddresscontrol);

                if (ContactId != 0)
                {
                    clsCollectionAgency oCollectionAgency = new clsCollectionAgency(_databaseconnectionstring);
                    DataTable dtCollectionagency = oCollectionAgency.GetCollectionAgency(ContactId);
                    if (dtCollectionagency != null)
                    {
                        if (dtCollectionagency.Rows.Count > 0)
                        {
                            oAddresscontrol.isFormLoading = true;
                            txtname.Text = Convert.ToString(dtCollectionagency.Rows[0]["sName"]);
                            txtcontact.Text = Convert.ToString(dtCollectionagency.Rows[0]["sContact"]);
                            oAddresscontrol.txtAddress1.Text = Convert.ToString(dtCollectionagency.Rows[0]["sAddressLine1"]);
                            oAddresscontrol.txtAddress2.Text = Convert.ToString(dtCollectionagency.Rows[0]["sAddressLine2"]);
                            oAddresscontrol.txtCity.Text = Convert.ToString(dtCollectionagency.Rows[0]["sCity"]);
                            oAddresscontrol.cmbState.Text = Convert.ToString(dtCollectionagency.Rows[0]["sState"]);
                            oAddresscontrol.txtZip.Text = Convert.ToString(dtCollectionagency.Rows[0]["sZip"]);
                            mtxtPhone.Text = Convert.ToString(dtCollectionagency.Rows[0]["sPhone"]);
                            txtFax.Text = Convert.ToString(dtCollectionagency.Rows[0]["sFax"]);
                            txtEmail.Text = Convert.ToString(dtCollectionagency.Rows[0]["sEmail"]);
                            txtURL.Text = Convert.ToString(dtCollectionagency.Rows[0]["sURL"]);
                            txtPercentofSelfPay.Text = Convert.ToString(dtCollectionagency.Rows[0]["nPercentofSelfPayBalance"]);
                            txtFlatfee.Text = Convert.ToString(dtCollectionagency.Rows[0]["nFlatfee"]);

                            if (Convert.ToInt32(dtCollectionagency.Rows[0]["nBadDebtFeeType"]) == Convert.ToInt32(CollectionFeeType.PercentofSelfPayBalance))
                            {
                                rbPercentofselfpay.Checked = true;
                                rbFlatfee.Checked = false;
                            }
                            else if (Convert.ToInt32(dtCollectionagency.Rows[0]["nBadDebtFeeType"]) == Convert.ToInt32(CollectionFeeType.FlatFee))
                            {
                                rbPercentofselfpay.Checked = false;
                                rbFlatfee.Checked = true;
                            }
                            oAddresscontrol.isFormLoading = false;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.Open, "Open Collection Agency", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                    }
                    if (oCollectionAgency != null)
                    {
                        oCollectionAgency.Dispose();
                        oCollectionAgency = null;
                    }

                    if (dtCollectionagency != null)
                    {
                        dtCollectionagency.Dispose();
                        dtCollectionagency = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }


        private void rbPercentofselfpay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPercentofselfpay.Checked)
            {
                txtPercentofSelfPay.Enabled = true;
                txtFlatfee.Enabled = false;
            }
        }

        private void rbFlatfee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFlatfee.Checked)
            {
                txtPercentofSelfPay.Enabled = false;
                txtFlatfee.Enabled = true;
            }
        }


        private void txtPercentofSelfPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (txtPercentofSelfPay.Text.Contains('.') && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                    return;
                }
                string _strRegex = "";
                _strRegex = "^([0-9]*)$";
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), _strRegex) == false)
                    {
                        e.Handled = true;
                    }
                    if ((e.KeyChar == Convert.ToChar(46)))
                    {
                        //Allow Dot 
                        e.Handled = false;
                    }
                }
            }
        }

        private void txtFlatfee_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (txtFlatfee.Text.Contains('.') && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                    return;
                }
                string _strRegex = "";
                _strRegex = "^([0-9]*)$";
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), _strRegex) == false)
                    {
                        e.Handled = true;
                    }
                    if ((e.KeyChar == Convert.ToChar(46)))
                    {
                        //Allow Dot
                        e.Handled = false;
                    }
                }
            }
        }


        private void ts_btnSaveandClose_Click(object sender, EventArgs e)
        {
            if (SaveCollectionAgency())
            {
                this.Close();
            }

        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            if (SaveCollectionAgency())
            {
                if (ContactId != 0)
                {
                    MessageBox.Show("Collection agency updated successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.Save, "Updated Collection Agency", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
                else
                {
                    MessageBox.Show("Collection agency saved successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.CollectionAgency, gloAuditTrail.ActivityType.Save, "Added Collection Agency", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                }
                ClearData();
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ClearData()
        {
            ContactId = 0;
            txtname.Text = "";
            txtcontact.Text = "";
            oAddresscontrol.isFormLoading = true;
            oAddresscontrol.txtAddress1.Text = "";
            oAddresscontrol.txtAddress2.Text = "";
            oAddresscontrol.txtCity.Text = "";
            oAddresscontrol.cmbState.Text = "";
            oAddresscontrol.txtZip.Text = "";
            oAddresscontrol.isFormLoading = false;
            mtxtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtURL.Text = "";
            rbPercentofselfpay.Checked = false;
            rbFlatfee.Checked = false;
            txtPercentofSelfPay.Text = "";
            txtFlatfee.Text = "";
            txtname.Focus();
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

            return response;
        }

        private bool ValidateData()
        {
            clsCollectionAgency oCollectionAgency = new clsCollectionAgency(_databaseconnectionstring);
            try
            {
                if (txtname.Text.Trim() == "")
                {
                    MessageBox.Show("Enter collection agency name.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtname.Focus();
                    return false;
                }

                if (oCollectionAgency.CheckCollectionAgency(ContactId, txtname.Text.Trim()))
                {
                    MessageBox.Show("collection agency name already Exist.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtname.Focus();
                    return false;
                }

                if (CheckEmailAddress(txtEmail.Text) == false)
                {
                    MessageBox.Show("Please enter a valid email id.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmail.Focus();
                    return false;
                }

                if (!string.IsNullOrEmpty(txtURL.Text))
                {
                    if (CheckURL(txtURL.Text) == false)
                    {
                        MessageBox.Show("Please enter a valid URL ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtURL.Focus();
                        return false;
                    }
                }

                if (mtxtPhone.Text.Length > 0)
                {
                    if (mtxtPhone.MaskFull == false)
                    {
                        MessageBox.Show("Phone details are incomplete.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mtxtPhone.Focus();
                        return false;
                    }
                }

                if (txtFax.Text.Length > 0)
                {

                    if (txtFax.MaskFull == false)
                    {
                        MessageBox.Show("Fax details are incomplete.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFax.Focus();
                        return false;
                    }
                }

                if (!rbFlatfee.Checked && !rbPercentofselfpay.Checked)
                {
                    MessageBox.Show("Please select collection fee type", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbPercentofselfpay.Focus();
                    txtPercentofSelfPay.Enabled = true;
                    return false;
                }

                if (rbPercentofselfpay.Checked)
                {
                    if (txtPercentofSelfPay.Text == "")
                    {
                        MessageBox.Show("Enter percent of self pay.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPercentofSelfPay.Focus();
                        return false;
                    }
                }

                if (rbFlatfee.Checked)
                {
                    if (txtFlatfee.Text == "")
                    {
                        MessageBox.Show("Enter flat fee amount.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFlatfee.Focus();
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show("Error while saving collection agency", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCollectionAgency != null)
                {
                    oCollectionAgency.Dispose();
                    oCollectionAgency = null;
                }
            }

            return true;
        }

        private bool SaveCollectionAgency()
        {
            bool isSaved = true;
            gloContacts.Contact oContact = new gloContacts.Contact();
            clsCollectionAgency oCollectionAgency = new clsCollectionAgency(_databaseconnectionstring);
            try
            {
                if (ValidateData())
                {
                    oContact.ContactID = ContactId;
                    oContact.Name = txtname.Text;
                    oContact.ContactName = txtcontact.Text;
                    oCollectionAgency.Address1 = oAddresscontrol.txtAddress1.Text;
                    oCollectionAgency.Address2 = oAddresscontrol.txtAddress2.Text;
                    oContact.City = oAddresscontrol.txtCity.Text;
                    oContact.State = oAddresscontrol.cmbState.Text;
                    oContact.ZIP = oAddresscontrol.txtZip.Text;
                    oContact.Phone = mtxtPhone.Text;
                    oContact.Fax = txtFax.Text;
                    oContact.Email = txtEmail.Text;
                    oContact.URL = txtURL.Text;
                    oCollectionAgency.ContactType = Convert.ToString(gloPMContacts.ContactType.ColectionAgency);

                    if (rbPercentofselfpay.Checked && !rbFlatfee.Checked)
                    {
                        oCollectionAgency.nCollectionFeeType = Convert.ToInt32(CollectionFeeType.PercentofSelfPayBalance);
                        oCollectionAgency.SelfPayBalancePercent = Convert.ToDouble(txtPercentofSelfPay.Text);
                        oCollectionAgency.FlatFee = 0;
                    }
                    else if (!rbPercentofselfpay.Checked && rbFlatfee.Checked)
                    {
                        oCollectionAgency.nCollectionFeeType = Convert.ToInt32(CollectionFeeType.FlatFee);
                        oCollectionAgency.SelfPayBalancePercent = 0;
                        oCollectionAgency.FlatFee = Convert.ToDouble(txtFlatfee.Text);
                    }
                    oCollectionAgency.Add(oContact);
                    return isSaved;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                if (ContactId != 0)
                {
                    MessageBox.Show("Error while updating collection agency", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error while saving collection agency", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }
            finally
            {
                if (oContact != null)
                {
                    oContact.Dispose();
                    oContact = null;
                }
                if (oCollectionAgency != null)
                {
                    oCollectionAgency.Dispose();
                    oCollectionAgency = null;
                }
            }
        }

        private void frmSetupCollectionAgency_FormClosing(object sender, FormClosingEventArgs e)
        {
            _databaseconnectionstring = null;
            if (oAddresscontrol == null)
            {
                oAddresscontrol.Dispose();
                oAddresscontrol = null;
            }
            _MessageBoxCaption = null;
        }
       
    }
}
