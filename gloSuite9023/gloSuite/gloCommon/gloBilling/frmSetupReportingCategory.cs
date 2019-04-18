using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupReportingCategory : Form
    {
        #region "Variables Declaration"
        private String _databaseconnectionstring;
        private Int64 _ReportingCategoryID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        #endregion "Variables Declaration"

        #region "Constructor "
        public frmSetupReportingCategory(String _databaseconnectionstring, Int64 _ReportingCategoryID)
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
            this._ReportingCategoryID=_ReportingCategoryID;
        }
       #endregion "Constructor "

        #region "Form Load Event"
        private void frmSetupeportingCategory_Load(object sender, EventArgs e)
        {
            try
            {
                if (_ReportingCategoryID > 0)
                {
                    tsb_Save.Visible = false;
                    FillReportingCategoryCode();
                }
                else
                    tsb_Save.Visible = true;
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
                if (ValidateData()) //if condition added for  bugid 73165 to not allow to enter only special characters
               {

                if (txtIDReportingCategoryCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Reporting Category Code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIDReportingCategoryCode.Focus();
                    return;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Reporting Category Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescription.Focus();
                    return;
                }
           //     if (this._ReportingCategoryID == 0)
                {
                    String _strQuery = "Select  count(*)  from Cases_ReportingCategory where sCode = '" + this.txtIDReportingCategoryCode.Text.Replace("'", "''") + "'  and bIsblocked=0 and nID!=" + _ReportingCategoryID;
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    DataTable dtReportingCategoryCode = new DataTable();
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
                                    MessageBox.Show("Reporting Category already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        dtReportingCategoryCode.Dispose();

                    }
                }
                #endregion
                ReportingCategory objReportingCategory = new ReportingCategory(_databaseconnectionstring);
                objReportingCategory.sCode = this.txtIDReportingCategoryCode.Text.Trim() ;
                objReportingCategory.sDescription = this.txtDescription.Text.Trim();
                objReportingCategory.ReportingCategoryID = _ReportingCategoryID;

                objReportingCategory.AddReportingCategory();
                this.Close();
               }
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

                if (ValidateData())
                {
                    if (txtIDReportingCategoryCode.Text.Trim() == "")
                    {
                        MessageBox.Show(" Please enter Reporting Category Code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIDReportingCategoryCode.Focus();
                        return;
                    }
                    if (txtDescription.Text.Trim() == "")
                    {
                        MessageBox.Show(" Please enter Reporting Category Description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDescription.Focus();
                        return;
                    }
                    //  if (this._ReportingCategoryID == 0)
                    {
                        String _strQuery = "Select  count(*)  from Cases_ReportingCategory where sCode = '" + this.txtIDReportingCategoryCode.Text.Replace("'", "''") + "'  and bIsblocked=0 and nID!=" + _ReportingCategoryID;
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        DataTable dtReportingCategoryCode = new DataTable();
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
                                        MessageBox.Show("Reporting Category already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            dtReportingCategoryCode.Dispose();

                        }
                    }
                #endregion
                    ReportingCategory objReportingCategory = new ReportingCategory(_databaseconnectionstring);
                    objReportingCategory.sCode = this.txtIDReportingCategoryCode.Text;
                    objReportingCategory.sDescription = this.txtDescription.Text;
                    objReportingCategory.ReportingCategoryID = _ReportingCategoryID;

                    objReportingCategory.AddReportingCategory();

                    this.txtDescription.Text = "";
                    this.txtIDReportingCategoryCode.Text = "";
                }
                }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        #endregion "ToolStrip Events"

        #region "Private Methods"
        private void FillReportingCategoryCode()
        {

            ReportingCategory ReportingCategory = new ReportingCategory(_databaseconnectionstring);
            DataTable dtReportingCategoryCode = new DataTable();

            try
            {
                if (_ReportingCategoryID > 0)
                {
                    dtReportingCategoryCode = ReportingCategory.GetReportingCategoryCode(_ReportingCategoryID);
                    if (dtReportingCategoryCode != null && dtReportingCategoryCode.Rows.Count > 0)
                    {
                        txtDescription.Text = dtReportingCategoryCode.Rows[0]["sDescription"].ToString();
                        txtIDReportingCategoryCode.Text = dtReportingCategoryCode.Rows[0]["sCode"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {   
                dtReportingCategoryCode.Dispose();
            }
        }
        #endregion "Private Methods"

        private void txtDescription_MouseHover(object sender, EventArgs e)
        {
            if (Convert.ToString(txtDescription.Text).Length > 40)
                this.C1SuperTooltip1.SetToolTip(this.txtDescription, Convert.ToString(txtDescription.Text));
        }

        private bool ValidateData() //added for bugid 73165
        {
            if ((!string.IsNullOrEmpty(txtIDReportingCategoryCode.Text.ToString().Trim())))
            {
                bool replaced = System.Text.RegularExpressions.Regex.IsMatch(txtIDReportingCategoryCode.Text.Trim(), "[a-zA-Z0-9]");
                if ((replaced == false))
                {
                    MessageBox.Show("Code cannot contain only special characters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        
    }
}
