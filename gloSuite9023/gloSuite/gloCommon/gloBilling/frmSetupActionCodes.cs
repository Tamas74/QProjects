using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupActionCodes : Form
    {
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ActionCodeID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public frmSetupActionCodes(string databaseConnectionString, Int64 ActionCodeID)
        {
            _databaseconnectionstring = databaseConnectionString;
            _ActionCodeID = ActionCodeID;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            InitializeComponent();
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

        }

        private void frmSetupActionCodes_Load(object sender, EventArgs e)
        {
            FillActionCode();
        }
        //
        private bool Validate()
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                //Code Added by Mayuri:20091113
                //To make Description field mandatory
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return false;
                }
                //End Code Added on 20091113
                
                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

            }
        }
        //

        //Code Added by Mayuri:20091106
        //For Action Codes
        private void FillActionCode()
        {
         //   bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                //
                //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";
                strQuery = " select nActionCodeID, sCode, sDescription,isnull(bIsSystem,0) as bIsSystem, isnull(bIsBlock,0) as bIsBlock,  isnull(nActionID,0) as nActionID, nClinicID from BL_ActionCodes_MST where nActionCodeID='" + _ActionCodeID  + "' AND nClinicID=" + _ClinicID + " ";
                //

                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    if (Convert.ToBoolean(dt.Rows[0]["bIsSystem"]) == true)
                    {
                        chkIsSystem.Checked = true;
                    }
                    else
                    {
                        chkIsSystem.Checked = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["bIsBlock"]) == true)
                    {
                        chkIsBlock.Checked = true;
                    }
                    else
                    {
                        chkIsBlock.Checked = false;
                    }
                }


            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            //return dt;

        }

        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                if (ObjReasonCodes.IsExistsActionCode(_ActionCodeID, txtCode.Text, txtDescription.Text))
                {
                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                else
                {
                    //Modify

                    Int64 _tempResult = ObjReasonCodes.AddModifyActionCodes(_ActionCodeID, txtCode.Text.Trim(), txtDescription.Text.Trim(), chkIsBlock.Checked, chkIsSystem.Checked);
                    if (_tempResult > 0)
                    {
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory., ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                       // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                    }

                }



                this.Close();

            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                if (ObjReasonCodes.IsExistsActionCode (_ActionCodeID , txtCode.Text, txtDescription.Text))
                {
                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                else
                {
                    //Modify

                    Int64 _tempResult = ObjReasonCodes.AddModifyActionCodes (_ActionCodeID , txtCode.Text.Trim(), txtDescription.Text.Trim(), chkIsBlock.Checked, chkIsSystem.Checked);
                    if (_tempResult > 0)
                    {
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory., ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                    }
                    txtCode.Text = "";
                    txtDescription.Text = "";
                    chkIsSystem.Checked = false;
                    chkIsBlock.Checked = false;
                    _ActionCodeID  = 0;
                }
            }
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //End Code Added by Mayuri:20091106

    }
}