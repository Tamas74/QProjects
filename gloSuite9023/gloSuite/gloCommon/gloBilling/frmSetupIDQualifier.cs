using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupIDQualifier : Form
    {
        #region "Variables Declaration"
        private String _databaseconnectionstring;
        private Int64 _QualifierID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        #endregion "Variables Declaration"

        #region "Constructor "
        public frmSetupIDQualifier(String _databaseconnectionstring, Int64 _QualifierID)
        {
            InitializeComponent();
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
            #endregion " Retrieve MessageBoxCaption from AppSettings "
            this._databaseconnectionstring= _databaseconnectionstring;
            this._QualifierID=_QualifierID;
        }
       #endregion "Constructor "

        #region "Form Load Event"
        private void frmSetupIDQualifier_Load(object sender, EventArgs e)
        {
            try
            {
                if (_QualifierID > 0)
                    FillQualifierCode();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }
        #endregion "Form Load Event"

        #region "ToolStrip Events"
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                #region " Validate Data "

                if (txtIDQualifierCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIDQualifierCode.Focus();
                    return;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return;
                }
                if (this._QualifierID == 0)
                {
                    String _strQuery = "Select  count(*)  from BL_IDQualifier where sCode = '" + this.txtIDQualifierCode.Text.Replace("'","''") + "' and sDescription = '" + this.txtDescription.Text.Replace("'","''") + "'";
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    DataTable dtQualifierCode = new DataTable();
                    try
                    {
                        oDB.Connect(false);
                        object _intResult = null;
                        _intResult = oDB.ExecuteScalar_Query(_strQuery);
                        if (_intResult != null)
                        {
                            if (_intResult.ToString().Trim() != "")
                            {
                                if (Convert.ToInt64(_intResult) > 0)
                                {
                                    MessageBox.Show("Billing Id Qualifier already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        oDB.Disconnect();
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        dtQualifierCode.Dispose();

                    }
                }
                #endregion
                Qualifier objQualifier = new Qualifier(_databaseconnectionstring);
                objQualifier.QualifierCode = this.txtIDQualifierCode.Text;
                objQualifier.QualifierDescription = this.txtDescription.Text;
                objQualifier.QualifierTypeID = _QualifierID;
                objQualifier.ClinicID = this._ClinicID;
                objQualifier.Add();
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {


            try
            {
                #region " Validate Data "

                if (txtIDQualifierCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIDQualifierCode.Focus();
                    return;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Qualifier Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return;
                }
                if (this._QualifierID == 0)
                {
                    String _strQuery = "Select  count(*)  from BL_IDQualifier where sCode = '" + this.txtIDQualifierCode.Text.Replace("'", "''") + "' and sDescription = '" + this.txtDescription.Text.Replace("'", "''") + "'";
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    DataTable dtQualifierCode = new DataTable();
                    try
                    {
                        oDB.Connect(false);
                        object _intResult = null;
                        _intResult = oDB.ExecuteScalar_Query(_strQuery);
                        if (_intResult != null)
                        {
                            if (_intResult.ToString().Trim() != "")
                            {
                                if (Convert.ToInt64(_intResult) > 0)
                                {
                                    MessageBox.Show("Billing Id Qualifier already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                        oDB.Disconnect();
                    }
                    catch (gloDatabaseLayer.DBException dbex)
                    {
                        dbex.ERROR_Log(dbex.ToString());
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        dtQualifierCode.Dispose();

                    }
                }
                #endregion
                Qualifier objQualifier = new Qualifier(_databaseconnectionstring);
                objQualifier.QualifierCode = this.txtIDQualifierCode.Text;
                objQualifier.QualifierDescription = this.txtDescription.Text;
                objQualifier.QualifierTypeID = _QualifierID;
                objQualifier.ClinicID = this._ClinicID;
                objQualifier.Add();
                this.txtDescription.Text = "";
                this.txtIDQualifierCode.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        #endregion "ToolStrip Events"

        #region "Private Methods"
        private void FillQualifierCode()
        {

            Qualifier qualifier = new Qualifier(_databaseconnectionstring);
            DataTable dtQualifierCode = new DataTable();

            try
            {
                if (_QualifierID > 0)
                {
                    dtQualifierCode = qualifier.GetQualifierCode(_QualifierID);
                    if (dtQualifierCode != null && dtQualifierCode.Rows.Count > 0)
                    {
                        txtDescription.Text = dtQualifierCode.Rows[0]["sDescription"].ToString();
                        txtIDQualifierCode.Text = dtQualifierCode.Rows[0]["sCode"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {   
                dtQualifierCode.Dispose();
            }
        }
        #endregion "Private Methods"

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            if (Convert.ToString(txtDescription.Text).Length > 40)
                this.C1SuperTooltip1.SetToolTip(this.txtDescription, Convert.ToString(txtDescription.Text));
        }

        

        
    }
}
