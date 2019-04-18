using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class FrmSetupHoldBilling : Form
    {
        private Int64 _nHoldBillingID = 0;
        private string _sHoldBillingReason = "";
        private string _sHoldBillingDescription = "";
        private string _messageBoxCaption = "";
        private HoldBilling oHoldBilling;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #region Properties
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 HoldBillingID
        {
            get { return _nHoldBillingID; }
            set { _nHoldBillingID = value; }
        }
        public string HoldBillingReason
        {
            get { return _sHoldBillingReason; }
            set { _sHoldBillingReason = value; }
        }
        public string HoldBillingDescription
        {
            get { return _sHoldBillingDescription; }
            set { _sHoldBillingDescription = value; }
        }
        #endregion

       

        private string _databaseconnectionstring = "";

        public FrmSetupHoldBilling(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["clinicId"]);
                }
                else { _ClinicID = 0; }
            }

            else
            {
                _ClinicID = 0;
            }
            #region "Retrieve MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
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
            #endregion
        }

        public FrmSetupHoldBilling(Int64 HoldBillingID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _nHoldBillingID = HoldBillingID;
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
        }


        private void FrmSetupHoldBilling_Load(object sender, EventArgs e)
        {
            if (_nHoldBillingID != 0)
            {
                
                oHoldBilling = new HoldBilling(_databaseconnectionstring);
                DataTable dtHoldBilling;
                dtHoldBilling = oHoldBilling.GetHoldBilling(_nHoldBillingID);
               
                if(dtHoldBilling !=null)
                {
                    if(dtHoldBilling.Rows.Count!=0)
                    {
                        txtHoldBillingReasonCode.Text=dtHoldBilling.Rows[0]["sHoldBillingReason"].ToString();
                        txtHoldBillingReasonDesc.Text=dtHoldBilling.Rows[0]["sHoldBillingDescription"].ToString();
                    }
                }
                dtHoldBilling.Dispose();
            }
            else
            {
                if(_sHoldBillingReason.Trim()!="")
                {
                    txtHoldBillingReasonCode.Text=_sHoldBillingReason;
                    txtHoldBillingReasonDesc.Select();
                    txtHoldBillingReasonDesc.Focus();
                }
      
            }
        }
        //private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
       
        private HoldBilling setHoldBilling()
        {
            HoldBilling oInternalHoldBilling = new HoldBilling(_databaseconnectionstring);
            oInternalHoldBilling.HoldBillingID = _nHoldBillingID;
            oInternalHoldBilling.HoldBillingReason = txtHoldBillingReasonCode.Text.Trim();
            oInternalHoldBilling.HoldBillingDescription = txtHoldBillingReasonDesc.Text.Trim();
            return oInternalHoldBilling;
        }
        private void txtHoldBillingReasonCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
            //{
            //    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9\w\s]+$") == false)
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void tls_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
             {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {

                    case "SaveCls": // Add or Update Modifier
                        if (txtHoldBillingReasonCode.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter a Reason code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonCode.Focus();
                            return;
                        }
                        if (txtHoldBillingReasonDesc.Text.Trim() == "")
                        {
                            MessageBox.Show(" Please enter a description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonDesc.Focus();
                            return;
                        }

                        oHoldBilling = setHoldBilling();


                        //// Chech for duplicate Modifier code
                        if (oHoldBilling.CheckDuplicate(oHoldBilling.HoldBillingID, txtHoldBillingReasonCode.Text.Trim(), txtHoldBillingReasonDesc.Text.Trim()))
                        {
                            MessageBox.Show("Code or description is alredy in use by another entry.  Please select a unique code or description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonCode.Focus();
                            return;
                        }

                        //Add modifier
                        _nHoldBillingID = oHoldBilling.Add();
                        _sHoldBillingReason = txtHoldBillingReasonCode.Text.Trim(); 
                        _sHoldBillingDescription = txtHoldBillingReasonCode.Text.Trim(); 

                        if (_nHoldBillingID > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, ActivityCategory.HoldBilling, ActivityType.Add, "Add Hold Billing", 0, _nHoldBillingID, 0, ActivityOutCome.Success);

                            this.Close();
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _nHoldBillingID, 0, ActivityOutCome.Failure);

                            MessageBox.Show(" Error ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "Save": // Add or Update Modifier
                        if (txtHoldBillingReasonCode.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter a reason code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonCode.Focus();
                            return;
                        }
                        if (txtHoldBillingReasonDesc.Text.Trim() == "")
                        {
                            MessageBox.Show(" Please enter a description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonDesc.Focus();
                            return;
                        }

                        oHoldBilling = setHoldBilling();


                        //// Chech for duplicate Modifier code
                        if (oHoldBilling.CheckDuplicate(oHoldBilling.HoldBillingID, txtHoldBillingReasonCode.Text.Trim(), txtHoldBillingReasonDesc.Text.Trim()))
                        {
                            MessageBox.Show("Code or description is alredy in use by another entry.  Please select a unique code or description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHoldBillingReasonCode.Focus();
                            return;
                        }

                        //Add modifier
                        _nHoldBillingID = oHoldBilling.Add();
                        _sHoldBillingReason = txtHoldBillingReasonCode.Text.Trim(); ;
                        _sHoldBillingDescription = txtHoldBillingReasonDesc.Text.Trim(); ;
                       
                        

                        if (_nHoldBillingID > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.HoldBilling, ActivityType.Add, "Add Hold Billing", 0, _nHoldBillingID, 0, ActivityOutCome.Success);
                            txtHoldBillingReasonCode.Text = "";
                            txtHoldBillingReasonDesc.Text = "";
                            _nHoldBillingID = 0;

                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.HoldBilling, ActivityType.Add, "Add Hold Billing", 0, _nHoldBillingID, 0, ActivityOutCome.Failure);

                            MessageBox.Show(" Error ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;


                    case "Close":
                        try
                        {
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }//catch
                        break;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.HoldBilling, ActivityType.Add, "Add Hold Billing", 0, _nHoldBillingID, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        }
     
           
    }
}
