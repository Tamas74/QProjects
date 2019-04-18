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
    public partial class frmSetupRefundType : Form
    {
        #region "Variables"
        
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        private Int64 _RefundTypeId = 0;
        private string _RefundTypeCode = "";
        private string _RefundTypeDesc = "";
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Variables"

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public string DatabaseConnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 RefundTypeID
        {
            get { return _RefundTypeId; }
            set { _RefundTypeId = value; }
        }
        public string RefundTypeCode
        {
            get { return _RefundTypeCode; }
            set { _RefundTypeCode = value; }
        }
        public string RefundTypeDesc
        {
            get { return _RefundTypeDesc; }
            set { _RefundTypeDesc = value; }
        } 

        #endregion

        #region " Constructor "

        public frmSetupRefundType(string DatabaseConnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionstring;
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

        public frmSetupRefundType(Int64 RefundTypeId, string DatabaseConnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionstring;
            _RefundTypeId = RefundTypeId;
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

        #endregion

        #region " Form Load Event "
        
        private void frmSetupRefundType_Load(object sender, EventArgs e)
        {
            try
            {
                if (_RefundTypeId != 0)
                {
                    //create object to hold value from database.
                    RefundType oRefundType = new RefundType(_databaseconnectionstring);
                    DataTable dtRefundType;

                    dtRefundType = oRefundType.GetRefundType(_RefundTypeId);

                    //dispose object after getting values from database to table.
                    oRefundType.Dispose();

                    if (dtRefundType != null)
                    {
                        if (dtRefundType.Rows.Count != 0)
                        {
                            txtRefundType.Text = Convert.ToString(dtRefundType.Rows[0]["sRefundTypeDesc"]);
                            txtCode.Text = Convert.ToString(dtRefundType.Rows[0]["sRefundTypeCode"]);
                            txtRefundType.Tag = Convert.ToString(dtRefundType.Rows[0]["nRefundTypeId"]);
                        }
                    }
                }
                else
                {
                    txtCode.Focus();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
 
        #endregion

        #region " Tool Strip Button Click Event "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            RefundType oRefundType = new RefundType(_databaseconnectionstring);

            try
            {
                #region " Validate Data "

                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter Refund type code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
                if (txtRefundType.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter a description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundType.Focus();
                    return;
                }

                #endregion

                oRefundType.RefundTypeCode = txtCode.Text.Trim();
                oRefundType.RefundTypeDesc = txtRefundType.Text.Trim();

                if (_RefundTypeId == 0)
                {
                    // Changed made by Rahul Patel on 16-09-2010 
                    // For separating the validation for Refund Type Code and Refund Type Description 
                    // Here last parameter of function 'IsExistsRefundType' is '1' which identify the validation is for Refund Type Code
                    if (oRefundType.IsExistsRefundType(0, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc,1) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // Here last parameter of function 'IsExistsRefundType' is '2' which identify the validation is for Refund Type Description
                    if (oRefundType.IsExistsRefundType(0, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc,2) == true)
                    {
                        MessageBox.Show("Description is already in use by another entry.  Select a unique description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _RefundTypeId = oRefundType.Add();
                    if (_RefundTypeId == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Refund type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Success);
                    }
                }
                else
                {
                    oRefundType.RefundTypeID = _RefundTypeId;
                    // Changed made by Rahul Patel on 16-09-2010 
                    // For separating the validation for Refund Type Code and Refund Type Description 
                    // Here last parameter of function 'IsExistsRefundType' is '1' which identify the validation is for Refund Type Code
                    if (oRefundType.IsExistsRefundType(oRefundType.RefundTypeID, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc, 1) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // Here last parameter of function 'IsExistsRefundType' is '2' which identify the validation is for Refund Type Description
                    if (oRefundType.IsExistsRefundType(oRefundType.RefundTypeID, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc,2) == true)
                    {
                        MessageBox.Show("Description is already in use by another entry.  Select a unique descript.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (oRefundType.Add() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Refund type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Success);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oRefundType != null) { oRefundType.Dispose(); }
            }

            
        }

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
        #endregion

        private void tsb_Save_Click(object sender, EventArgs e)
        {

            RefundType oRefundType = new RefundType(_databaseconnectionstring);

            try
            {
                #region " Validate Data "

                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter Refund type code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
                if (txtRefundType.Text.Trim() == "")
                {
                    MessageBox.Show(" Please enter description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRefundType.Focus();
                    return;
                }

                #endregion

                oRefundType.RefundTypeCode = txtCode.Text.Trim();
                oRefundType.RefundTypeDesc = txtRefundType.Text.Trim();

                if (_RefundTypeId == 0)
                {
                    //Changed made by Rahul Patel on 16-09-2010 
                    //For separating the validation for Refund Type Code and Refund Type Description 
                    // Here last parameter of function 'IsExistsRefundType' is '1' which identify the validation is for Refund Type Code
                    if (oRefundType.IsExistsRefundType(0, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc,1) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCode.Focus();
                        return;
                    }
                    // Here last parameter of function 'IsExistsRefundType' is '2' which identify the validation is for Refund Type Description
                    if (oRefundType.IsExistsRefundType(0, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc, 2) == true)
                    {
                        MessageBox.Show("Description is already in use by another entry.  Select a unique description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefundType.Focus();
                        return;
                    }
                    _RefundTypeId = oRefundType.Add();
                    if (_RefundTypeId == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Refund type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        txtCode.Text = "";
                        txtRefundType.Text = "";
                        _RefundTypeId = 0;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Success);
                    }
                }
                else
                {
                    oRefundType.RefundTypeID = _RefundTypeId;
                    //Changed made by Rahul Patel on 16-09-2010 
                    //For separating the validation for Refund Type Code and Refund Type Description 
                    // Here last parameter of function 'IsExistsRefundType' is '1' which identify the validation is for Refund Type Code
                    if (oRefundType.IsExistsRefundType(oRefundType.RefundTypeID, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc,1) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCode.Focus();
                        return;
                    }
                    // Here last parameter of function 'IsExistsRefundType' is '2' which identify the validation is for Refund Type Description
                    if (oRefundType.IsExistsRefundType(oRefundType.RefundTypeID, oRefundType.RefundTypeCode, oRefundType.RefundTypeDesc, 2) == true)
                    {
                        MessageBox.Show("Description is already in use by another entry.  Select a unique description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRefundType.Focus();
                        return;
                    }
                    if (oRefundType.Add() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Refund type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        txtCode.Text = "";
                        txtRefundType.Text = "";
                        _RefundTypeId = 0;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Success);
                    }
                }

                //this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.None, ActivityType.Add, "Add Refund Type", 0, _RefundTypeId, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oRefundType != null) { oRefundType.Dispose(); }
            }
        }
    }
}