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
    public partial class frmStandardFollowupAction : Form
    {
        private Int64 _nStdFollowupActionID = 0;
        private string _sStdFollowupActionCode = "";
        private string _sStdFollowupActionDesc = "";
        private string _MessageBoxCaption = "";
        private StandardFollowupAction oStdFollowupaction;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #region" Public Properties"
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 nStdFollowupActionID
        {
            get { return _nStdFollowupActionID; }
            set { _nStdFollowupActionID = value; }
        }
        public string sStdFollowupActionCode
        {
            get { return _sStdFollowupActionCode; }
            set { _sStdFollowupActionCode = value; }
        }

        public string sStdFollowupActionDesc
        {
            get { return _sStdFollowupActionDesc; }
            set { _sStdFollowupActionDesc = value; }
        }
        #endregion

        private string _databaseconnectionstring = "";
        public frmStandardFollowupAction(string Databasaconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = Databasaconnectionstring;
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
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            #endregion
        }
        public frmStandardFollowupAction(Int64 StdFollowupActionID, string Databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = Databaseconnectionstring;
            _nStdFollowupActionID = StdFollowupActionID;
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

        

        private void frmStandardFollowupAction_Load(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                if (_nStdFollowupActionID != 0)
                {
                    oStdFollowupaction = new StandardFollowupAction(_databaseconnectionstring);
                    
                    dt = oStdFollowupaction.GetStdFollowupAction(_nStdFollowupActionID);

                    if (dt != null)
                    {
                        txtStdFollowupActionCode.Text = dt.Rows[0]["sStdFollowupActionCode"].ToString();
                        txtStdFollowupActionDesc.Text = dt.Rows[0]["sStdFollowupActionDesc"].ToString();
                    }
                    dt.Dispose();
                }
                else
                {
                    txtStdFollowupActionCode.Select();
                    txtStdFollowupActionDesc.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
        }

        private StandardFollowupAction SetStdFollowupAction()
        {
            StandardFollowupAction oStd = new StandardFollowupAction(_databaseconnectionstring);
            oStd.nStdFollowupID = nStdFollowupActionID;
            oStd.sStdFollowupCode = txtStdFollowupActionCode.Text.Trim();
            oStd.sStdFollowupDesc = txtStdFollowupActionDesc.Text.Trim();
            return oStd;
        }

        private void tls_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "SaveCls":
                        {
                            if (SaveStdFollowupCode())
                            {
                                this.Close();
                            }

                        }
                        break;

                    case "Save":
                        {
                            if (SaveStdFollowupCode())
                            {
                                txtStdFollowupActionCode.Text = "";
                                txtStdFollowupActionDesc.Text = "";
                                txtStdFollowupActionCode.Select();
                                _nStdFollowupActionID = 0;
                            } 
                        }
                        break;
                    case "Close":
                        {
                            try
                            {
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                ex = null;
                            }//catch

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
        }

        private Boolean SaveStdFollowupCode()
        {
            bool _result = false;

            try
            {
                if (txtStdFollowupActionCode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a Standard Follow-up code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStdFollowupActionCode.Focus();
                    return false;
                }
                if (txtStdFollowupActionDesc.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a Standard Follow-up Description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStdFollowupActionDesc.Select();
                    return false;
                }
                oStdFollowupaction = SetStdFollowupAction();

                if (oStdFollowupaction.CheckDublicate(oStdFollowupaction.nStdFollowupID, txtStdFollowupActionCode.Text, txtStdFollowupActionDesc.Text))
                {
                    MessageBox.Show("Code or description is alredy in use by another entry.  Please select a unique code or description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStdFollowupActionCode.Text = "";
                    txtStdFollowupActionDesc.Text = "";
                    txtStdFollowupActionCode.Select();
                    return false;
                }

                _nStdFollowupActionID = oStdFollowupaction.Add();

                if (_nStdFollowupActionID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, ActivityCategory.FollowUp, ActivityType.Add, "Add Standard Follow-up Action", 0, _nStdFollowupActionID, 0, ActivityOutCome.Success);
                    _result = true;

                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.FollowUp, ActivityType.Add, "Add Standard Follow-up Action", 0, _nStdFollowupActionID, 0, ActivityOutCome.Failure);

                    MessageBox.Show(" Error ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
            return _result;
        }
        
    }
}
