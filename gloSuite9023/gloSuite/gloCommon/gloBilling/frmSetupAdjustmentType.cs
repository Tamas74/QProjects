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
    public partial class frmSetupAdjustmentType : Form
    {
        #region "Variables"
        
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        private Int64 _AdjustmentTypeId = 0;
        private string _AdjustmentTypeCode = "";
        private string _AdjustmentTypeDesc = "";
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
        public Int64 AdjustmentTypeID
        {
            get { return _AdjustmentTypeId; }
            set { _AdjustmentTypeId = value; }
        }
        public string AdjustmentTypeCode
        {
            get { return _AdjustmentTypeCode; }
            set { _AdjustmentTypeCode = value; }
        }
        public string AdjustmentTypeDesc
        {
            get { return _AdjustmentTypeDesc; }
            set { _AdjustmentTypeDesc = value; }
        } 

        #endregion

        #region " Constructor "

        public frmSetupAdjustmentType(string DatabaseConnectionstring)
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

        public frmSetupAdjustmentType(Int64 AdjustmentTypeId, string DatabaseConnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionstring;
            _AdjustmentTypeId = AdjustmentTypeId;
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
        
        private void frmSetupAdjustmentType_Load(object sender, EventArgs e)
        {
            try
            {
                if (_AdjustmentTypeId != 0)
                {
                    //create object to hold value from database.
                    AdjustmentType oAdjustmentType = new AdjustmentType(_databaseconnectionstring);
                    DataTable dtAdjustmentType;

                    dtAdjustmentType = oAdjustmentType.GetAdjustmentType(_AdjustmentTypeId);

                    //dispose object after getting values from database to table.
                    oAdjustmentType.Dispose();

                    if (dtAdjustmentType != null)
                    {
                        if (dtAdjustmentType.Rows.Count != 0)
                        {
                            txtAdjustmentType.Text = Convert.ToString(dtAdjustmentType.Rows[0]["sAdjustmentTypeDesc"]);
                            txtCode.Text = Convert.ToString(dtAdjustmentType.Rows[0]["sAdjustmentTypeCode"]);
                            txtAdjustmentType.Tag = Convert.ToString(dtAdjustmentType.Rows[0]["nAdjustmentTypeId"]);
                            chkDeactivate.Checked = Convert.ToBoolean(dtAdjustmentType.Rows[0]["Inactive"]);   
                        }
                    }

                    if (oAdjustmentType.IsInUse(txtCode.Text) == true)
                    { txtCode.Enabled = false; txtCode.ReadOnly = true; }
                    else
                    { txtCode.Enabled = true; txtCode.ReadOnly = false; }
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
            AdjustmentType oAdjustmentType = new AdjustmentType(_databaseconnectionstring);

            try
            {
                #region " Validate Data "

                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter adjustment type code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
                if (txtAdjustmentType.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter a description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAdjustmentType.Focus();
                    return;
                }

                #endregion

                oAdjustmentType.AdjusmentTypeCode = txtCode.Text.Trim();
                oAdjustmentType.AdjustmentTypeDesc = txtAdjustmentType.Text.Trim();
                oAdjustmentType.Inactive = chkDeactivate.Checked;

                if (_AdjustmentTypeId == 0)
                {
                    if (oAdjustmentType.IsExistsAdjustmentType(0, oAdjustmentType.AdjusmentTypeCode, oAdjustmentType.AdjustmentTypeDesc) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _AdjustmentTypeId = oAdjustmentType.Add();
                    if (_AdjustmentTypeId == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Adjustment type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Success);
                    }
                }
                else
                {
                    oAdjustmentType.AdjusmentTypeID = _AdjustmentTypeId;
                    if (oAdjustmentType.IsExistsAdjustmentType(oAdjustmentType.AdjusmentTypeID, oAdjustmentType.AdjusmentTypeCode, oAdjustmentType.AdjustmentTypeDesc) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (oAdjustmentType.Add() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Adjustment type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Success);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAdjustmentType != null) { oAdjustmentType.Dispose(); }
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
            AdjustmentType oAdjustmentType = new AdjustmentType(_databaseconnectionstring);

            try
            {
                #region " Validate Data "

                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter adjustment type code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
                if (txtAdjustmentType.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter a description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAdjustmentType.Focus();
                    return;
                }

                #endregion

                oAdjustmentType.AdjusmentTypeCode = txtCode.Text.Trim();
                oAdjustmentType.AdjustmentTypeDesc = txtAdjustmentType.Text.Trim();
                oAdjustmentType.Inactive = chkDeactivate.Checked;
  
                if (_AdjustmentTypeId == 0)
                {
                    if (oAdjustmentType.IsExistsAdjustmentType(0, oAdjustmentType.AdjusmentTypeCode, oAdjustmentType.AdjustmentTypeDesc) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    _AdjustmentTypeId = oAdjustmentType.Add();
                    if (_AdjustmentTypeId == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Adjustment type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Success);
                    }
                }
                else
                {
                    oAdjustmentType.AdjusmentTypeID = _AdjustmentTypeId;
                    if (oAdjustmentType.IsExistsAdjustmentType(oAdjustmentType.AdjusmentTypeID, oAdjustmentType.AdjusmentTypeCode, oAdjustmentType.AdjustmentTypeDesc) == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (oAdjustmentType.Add() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                        MessageBox.Show("Adjustment type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Success);
                    }
                }

                txtAdjustmentType.Text = "";
                txtCode.Text = "";
                chkDeactivate.Checked = false;  
                _AdjustmentTypeId = 0;
                txtCode.Focus();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Adjustment Type", 0, _AdjustmentTypeId, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAdjustmentType != null) { oAdjustmentType.Dispose(); }
            }

        }
    }
}