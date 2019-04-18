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
    public partial class frmSetup_InsuranceType : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _InsuranceTypeID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region " Constructor "

        public frmSetup_InsuranceType(string DatabaseConnectionString, Int64 TypeID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _InsuranceTypeID = TypeID;
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

        public frmSetup_InsuranceType(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
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

        #endregion " Constructor "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 InsurancetypeID
        {
            get { return _InsuranceTypeID; }
            set { _InsuranceTypeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion

        #region " Methods "

        private bool Validate()
        {
            try
            {
                if (txtTypeCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter plan type code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeCode.Focus();
                    return false;
                }

                if (txtTypeDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Enter plan type.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeDescription.Focus();
                    return false;
                }

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

        public void DeleteInsuranceType(Int64 InsuranceTypeID)
        {
            InsuranceType oInsuranceType = new InsuranceType(_databaseconnectionstring);
            try
            {
                oInsuranceType.Delete(InsuranceTypeID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

        private void FillInsuranceType(Int64 InsuranceTypeID)
        {
            InsuranceType oInsuranceType = new InsuranceType(_databaseconnectionstring);
            DataTable dttype = new DataTable();

            try
            {
                if (InsuranceTypeID > 0)
                {
                    dttype = oInsuranceType.GetInsuranceType(InsuranceTypeID);
                    if (dttype != null && dttype.Rows.Count > 0)
                    {
                        txtTypeCode.Text = dttype.Rows[0]["sTypeCode"].ToString();
                        txtTypeDescription.Text = dttype.Rows[0]["sTypeDesc"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion " Methods "

        private void frmSetup_InsuranceType_Load(object sender, EventArgs e)
        {
            try
            {
                FillInsuranceType(InsurancetypeID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }


        private void tls_InsuranceType_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempResult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "SaveClose":
                        {

                            if (Validate())
                            {
                                InsuranceType oInsurancetype = new InsuranceType(_databaseconnectionstring);
                                oInsurancetype.ClinicID = _ClinicID;
                                oInsurancetype.InduranceTypeDesc = txtTypeDescription.Text.Trim();
                                oInsurancetype.InsuranceTypeCode = txtTypeCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancetype.CheckDuplicate(_InsuranceTypeID, txtTypeCode.Text.Trim(), txtTypeDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancetypeID > 0)
                                {
                                    _tempResult = oInsurancetype.Modify(InsurancetypeID);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Insurance Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancetype.Add();
                                    _InsuranceTypeID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Insurance Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }

                                this.Close();
                            }
                        }    //Case "OK"

                        break;

                    case "Save":
                        {

                            if (Validate())
                            {
                                InsuranceType oInsurancetype = new InsuranceType(_databaseconnectionstring);
                                oInsurancetype.ClinicID = _ClinicID;
                                oInsurancetype.InduranceTypeDesc = txtTypeDescription.Text.Trim();
                                oInsurancetype.InsuranceTypeCode = txtTypeCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancetype.CheckDuplicate(_InsuranceTypeID, txtTypeCode.Text.Trim(), txtTypeDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancetypeID > 0)
                                {
                                    _tempResult = oInsurancetype.Modify(InsurancetypeID);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Insurance Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancetype.Add();
                                    _InsuranceTypeID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Insurance Type", 0, _tempResult, 0, ActivityOutCome.Success);
                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }

                                txtTypeCode.Text = "";
                                txtTypeDescription.Text = "";
                                _InsuranceTypeID = 0;
                                txtTypeCode.Focus();
                            }
                        }
                        break;
                   

                    case "Cancel":
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsuranceType, ActivityType.Add, "Add Insurance Type", 0, _tempResult, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);


            }

        }

        private void lblPlanCode_Click(object sender, EventArgs e)
        {

        }
    }
}