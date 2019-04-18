using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmSetupRemarkCodes : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _RemarkID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64   _ClinicID = 0;
        #endregion " Declarations "

        #region " Property Procedures "

       
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

        #endregion " Property Procedures "

        public frmSetupRemarkCodes(string databaseConnectionString, Int64 RemarkID)
        {
            _databaseconnectionstring = databaseConnectionString;
            _RemarkID = RemarkID;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            InitializeComponent();

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

        private void frmSetupRemarkCodes_Load(object sender, EventArgs e)
        {
            FillRemarkCode();
           
        }

        private bool ValidateData()
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return false;
                }

                RemarkCodes ObjRemarkCodes = new RemarkCodes(_databaseconnectionstring);

                if (ObjRemarkCodes.IsExists(_RemarkID, txtCode.Text, txtDescription.Text))
                {
                    MessageBox.Show("Remark Code : " + txtCode.Text + " is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;

                }


                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            finally
            {

            }
        }

        private void FillRemarkCode()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " select nRemarkID, sRemarkCode, sRemarkDescription,isnull(bIsSystem,0) as bIsSystem, isnull(bIsBlock,0) as bIsBlock, nClinicID from BL_RemarkCodes_MST where nRemarkID='" + _RemarkID + "' AND nClinicID=" + _ClinicID + " ";
                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sRemarkCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sRemarkDescription"]);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }

        }


        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                RemarkCodes ObjRemarkCodes = new RemarkCodes(_databaseconnectionstring);
                Int64 _tempResult = ObjRemarkCodes.AddModify(_RemarkID, txtCode.Text.Trim(), txtDescription.Text.Trim(),chkIsBlock.Checked, chkIsSystem.Checked);
                if (_tempResult > 0)
                {
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                }
                this.Close();
            }

           
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                RemarkCodes ObjRemarkCodes = new RemarkCodes(_databaseconnectionstring);
                Int64 _tempResult = ObjRemarkCodes.AddModify(_RemarkID, txtCode.Text.Trim(), txtDescription.Text.Trim(), chkIsBlock.Checked, chkIsSystem.Checked);
                if (_tempResult > 0)
                {
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                }


                txtCode.Text = "";
                txtDescription.Text = "";
                chkIsSystem.Checked = false;
                chkIsBlock.Checked = false;
                _RemarkID = 0;
            }
        }

    }
}