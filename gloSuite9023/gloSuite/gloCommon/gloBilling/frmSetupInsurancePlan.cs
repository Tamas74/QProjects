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
    public partial class frmSetupInsurancePlan : Form
    {
        #region " Constructor "

        public frmSetupInsurancePlan(string DatabaseConnectionString,Int64 PlanID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _InsurancePlanID = PlanID;
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

        public frmSetupInsurancePlan(string DatabaseConnectionString)
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

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _InsurancePlanID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 InsurancePlanID
        {
            get { return _InsurancePlanID; }
            set { _InsurancePlanID = value; }
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
                if (txtPlanCode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a plan code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPlanCode.Focus();
                    return false;
                }

                if (txtPlanDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a plan description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPlanDescription.Focus();
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

        public void DeleteInsurancePlan(Int64 InsurancePlanID)
        {
            InsurancePlan oInsurancePlan = new InsurancePlan(_databaseconnectionstring);
            try
            {
                oInsurancePlan.Delete(InsurancePlanID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }            
           
        }

        private void FillInsurancePlan(Int64 InsurancePlanID)
        {
            InsurancePlan oInsurancePlan = new InsurancePlan(_databaseconnectionstring);
            DataTable dtPlan = new DataTable();

            try
            {
                if (InsurancePlanID > 0)
                {
                    dtPlan = oInsurancePlan.GetInsurancePlan(InsurancePlanID);
                    if (dtPlan != null && dtPlan.Rows.Count>0)
                    {
                        txtPlanCode.Text = dtPlan.Rows[0]["sPlanCode"].ToString();
                        txtPlanDescription.Text = dtPlan.Rows[0]["sPlanDescription"].ToString();
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

        private void tls_InsurancePlan_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempResult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {

                            if (Validate())
                            {
                                InsurancePlan oInsurancePlan = new InsurancePlan(_databaseconnectionstring);
                                oInsurancePlan.ClinicID = _ClinicID;
                                oInsurancePlan.Decription = txtPlanDescription.Text.Trim();
                                oInsurancePlan.InsurancePlanCode = txtPlanCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancePlan.CheckDuplicate(_InsurancePlanID,txtPlanCode.Text.Trim(),txtPlanDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancePlanID > 0)
                                {
                                   _tempResult= oInsurancePlan.Modify(InsurancePlanID);
                                   if (_tempResult > 0)
                                   {
                                       //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                       gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Success);

                                   }
                                   else 
                                   {
                                       gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Failure);

                                   }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancePlan.Add();
                                    _InsurancePlanID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Success);

                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Failure);

                                    }

                                }

                                this.Close();
                            }
                        }    //Case "OK"

                        break;
                    case "Cancel":
                        {
                            this.Close();
                            break;
                        }

                    case "Save":
                        {
                            if (Validate())
                            {
                                InsurancePlan oInsurancePlan = new InsurancePlan(_databaseconnectionstring);
                                oInsurancePlan.ClinicID = _ClinicID;
                                oInsurancePlan.Decription = txtPlanDescription.Text.Trim();
                                oInsurancePlan.InsurancePlanCode = txtPlanCode.Text.Trim();

                                //Check if the Facility already exists if yes dont add.
                                if ((oInsurancePlan.CheckDuplicate(_InsurancePlanID, txtPlanCode.Text.Trim(), txtPlanDescription.Text.Trim())))
                                {
                                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (InsurancePlanID > 0)
                                {
                                    _tempResult = oInsurancePlan.Modify(InsurancePlanID);
                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _InsurancePlanID = 0;
                                        _tempResult = 0;
                                        txtPlanCode.Text = "";
                                        txtPlanDescription.Text = "";
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Success);

                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Failure);

                                    }
                                }
                                else
                                {
                                    //Add New
                                    _tempResult = oInsurancePlan.Add();
                                    _InsurancePlanID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _InsurancePlanID = 0;
                                        _tempResult = 0;
                                        txtPlanCode.Text = "";
                                        txtPlanDescription.Text = "";
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Success);

                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Failure);

                                    }

                                }

                                
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.InsurancePlancode, ActivityType.Add, "Add Insurance Plan", 0, _tempResult, 0, ActivityOutCome.Failure);
            }

        }

        private void frmSetupInsurancePlan_Load(object sender, EventArgs e)
        {

            txtPlanCode.Select();
            try
            {
                FillInsurancePlan(InsurancePlanID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }
    }
}