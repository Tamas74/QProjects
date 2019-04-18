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
    public partial class frmSetupRevCode : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
      //  private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 RevCodeId { get; set; }

        public Int64 ClinicID { get; set; }
       
        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupRevCode(string DatabaseConnectionString, Int64 _RevCodeId)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            RevCodeId = _RevCodeId;
            InitializeComponent();
           
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { ClinicID = 0; }
            }
            else
            { ClinicID = 0; }

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

        #region " Form Load "

        private void frmSetupTOS_Load(object sender, EventArgs e)
        {
            try
            {
                FillRevCode(RevCodeId);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempresult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                            CLsBL_RevenueCode oRevenueCode = new CLsBL_RevenueCode(_databaseconnectionstring);
                            if (ValidateSave())
                            {

                                //Check if RevenueCode already exists if Yes do not add
                                if ((oRevenueCode.IsExistsRevenueCode(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim())))
                                {
                                    MessageBox.Show("Revenue code is already in use by another entry.\n Enter unique revenue code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtCode.Focus();
                                    return;
                                }

                                //Add
                                if (RevCodeId > 0)
                                {

                                    _tempresult = oRevenueCode.AddModifyRevenueCode(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim(),(rbActive.Checked ? true:false));

                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                                                                                
                                        oRevenueCode.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                                    }
                                }
                                else
                                {
                                    _tempresult = oRevenueCode.AddModifyRevenueCode(0, txtCode.Text.Trim(), txtName.Text.Trim(), (rbActive.Checked ? true : false));
                                    RevCodeId = _tempresult;
                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        oRevenueCode.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                                    }
                                }

                                this.Close(); 
                            }
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        {
                            CLsBL_RevenueCode oRevenueCode = new CLsBL_RevenueCode(_databaseconnectionstring);
                            if (ValidateSave())
                            {

                                

                                //Add
                                if (RevCodeId > 0)
                                {
                                    _tempresult = oRevenueCode.AddModifyRevenueCode(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim(), (rbActive.Checked ? true : false));

                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                                        RevCodeId = 0;
                                        txtName.Text = "";
                                        txtCode.Text = "";
                                        oRevenueCode.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                                    }
                                }
                                else
                                {
                                    //Modify
                                    _tempresult = oRevenueCode.AddModifyRevenueCode(0, txtCode.Text.Trim(), txtName.Text.Trim(), (rbActive.Checked ? true : false));

                                    RevCodeId = _tempresult;
                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                                        RevCodeId = 0;
                                        txtName.Text = "";
                                        txtCode.Text = "";
                                        txtName.Focus();
                                        oRevenueCode.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.TOS, ActivityType.Add, "Add Revenue Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
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

            }
            finally
            {

            }
        }

        #endregion " Tool Strip Event "

        #region " Private & Public Methods "

        private void FillRevCode(Int64 Id)
        {

            CLsBL_RevenueCode oRevenueCode = new CLsBL_RevenueCode(_databaseconnectionstring);
            DataTable dtRevCode = new DataTable();

            try
            {
                if (Id > 0)
                {
                    dtRevCode = oRevenueCode.GetRevenueCode(Id, 0, true);
                    if (dtRevCode != null && dtRevCode.Rows.Count > 0)
                    {
                        txtName.Text = dtRevCode.Rows[0]["sDescription"].ToString();
                        txtCode.Tag = dtRevCode.Rows[0]["nID"].ToString();
                        txtCode.Text = dtRevCode.Rows[0]["nCode"].ToString();

                        if(Convert.ToString(dtRevCode.Rows[0]["bIsActive"])== "Active")
                        {
                            rbActive.Checked = true;
                            rbInactive.Checked = false;
                        }
                        else
                        {
                            rbActive.Checked = false;
                            rbInactive.Checked = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oRevenueCode.Dispose();
                dtRevCode.Dispose();
            }
        }

        private bool ValidateSave()
        {
            bool result = false;
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a revenue code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    //result=false;
                    return false; 
                }
                else if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter a description. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    //result=false;
                    return false;
                }
                result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                result = false;
            }
            finally
            { 
                
            }
            return result;
        }

        #endregion " Private & Public Methods "
       
        # region " Events "
        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbInactive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActive.Checked == true)
            {
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbActive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
        #endregion

    }
}