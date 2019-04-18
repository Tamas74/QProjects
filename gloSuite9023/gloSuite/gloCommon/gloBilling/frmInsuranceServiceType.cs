using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmInsuranceServiceType : Form
    {
        #region   "Declararions"
        private Int64 _ServiceTypeID = 0;
        private string _databaseconnectionstring;
        public string _MessageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 ServiceTypeID
        {
            get { return _ServiceTypeID; }
            set { _ServiceTypeID = value; }
        } 
        #endregion

        #region "Constructors"
        public frmInsuranceServiceType(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
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
        public frmInsuranceServiceType(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _ServiceTypeID = ID;
            _databaseconnectionstring = databaseconnectionstring;
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
        #endregion  

        private void frmInsuranceServiceType_Load(object sender, EventArgs e)
        {
            if (_ServiceTypeID!= 0)
            {
                ServiceType oStype = new ServiceType(_databaseconnectionstring);
                DataTable dtInsuranceype;
                dtInsuranceype = oStype.GetInsuranceServiceType(_ServiceTypeID);
                //nServiceTypeID, sServiceTypeCode, sServiceTypeDesc, sInsuranceType, nClinicID  --BL_InsuranceServiceType
                if (dtInsuranceype != null)
                {
                    if (dtInsuranceype.Rows.Count != 0)
                    {
                        txtServiceTypeCode.Text = dtInsuranceype.Rows[0]["ServiceTypeCode"].ToString();
                        txtServiceTypeDesc.Text = dtInsuranceype.Rows[0]["ServiceTypeDesc"].ToString();
                        txtInsuranceType.Text = dtInsuranceype.Rows[0]["InsuranceType"].ToString();

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceServiceType, ActivityType.Modify, "Open Modify", 0, _ServiceTypeID, 0, ActivityOutCome.Success);
                    }
                }
                oStype.Dispose();

            }

        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (Save())
                    {
                        this.Close();
                    }

                    break;
                case "Cancel":
                    this.Close();
                    break;
                case "Save":
                    if (Save())
                    {
                        _ServiceTypeID = 0;
                        txtServiceTypeCode.Text="";
                        txtServiceTypeDesc.Text="";
                        txtInsuranceType.Text = "";
                    }

                    break;
            }
        }

        private bool Save()
        {
            if (txtServiceTypeCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServiceTypeCode.Focus();
                return false;

            }
            if (txtServiceTypeDesc .Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServiceTypeDesc.Focus();
                return false;
            }
            if (txtInsuranceType.Text.Trim() == "")
            {
                MessageBox.Show("Enter a plan type.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInsuranceType.Focus();
                return false;
            }
            try
            {
                ServiceType oStype = new ServiceType(_databaseconnectionstring);
                oStype.ClinicID = this.ClinicID;
                oStype.ServiceTypeID  = _ServiceTypeID;
                oStype.ServiceTypeCode  = Convert.ToString(txtServiceTypeCode.Text.Trim());
                oStype.ServiceTypeDesc  = Convert.ToString(txtServiceTypeDesc.Text.Trim());
                oStype.InsuranceType = Convert.ToString(txtInsuranceType.Text.Trim());

                if (oStype.IsExistsServicetype(oStype.ServiceTypeID, oStype.ServiceTypeCode, oStype.ServiceTypeDesc))
                {
                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;

                }
                _ServiceTypeID=oStype.Add();
                if (_ServiceTypeID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceServiceType, ActivityType.Add, "Add Insurance Service Type", 0, _ServiceTypeID, 0, ActivityOutCome.Success);
                    return true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceServiceType, ActivityType.Add, "Add Insurance Service Type", 0, _ServiceTypeID, 0, ActivityOutCome.Failure);
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceServiceType, ActivityType.Add, "Add Insurance Service Type", 0, _ServiceTypeID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return false;
        }
    }
}