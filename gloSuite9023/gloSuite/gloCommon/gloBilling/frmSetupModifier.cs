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
    public partial class frmSetupModifier : Form
    {
        private string _databaseconnectionstring = "";
        public string _MessageBoxCaption = String.Empty;
        private Int64 _modifierID = 0;
        private Modifier oModifier;


        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _Code = "";
        private string _Description = "";

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 ModifierID
        {
            get { return _modifierID ; }
            set { _modifierID = value; }
        }
        public string ModifierCode
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string ModifierDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }
        //

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #region Constructor
               
        public frmSetupModifier(string DatabaseConnectionString)
        {
            InitializeComponent();           
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        public frmSetupModifier(Int64 ModifierId, string DatabaseConnectionString)
        {
            InitializeComponent();
            _modifierID = ModifierId;
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        #endregion

        private void frmSetupModifier_Load(object sender, EventArgs e)
        {
            //get Modifier record from database for given _modifierID
            //and Display it for update
            if (_modifierID != 0)
            {
                oModifier = new Modifier(_databaseconnectionstring);
                DataTable dtModifier;
                dtModifier = oModifier.GetModifier(_modifierID);

                if (dtModifier != null)
                {
                    if (dtModifier.Rows.Count != 0)
                    {
                        txtModifierCode.Text = dtModifier.Rows[0]["sModifierCode"].ToString();
                        txtDescription.Text = dtModifier.Rows[0]["sDescription"].ToString();
                    }
                }
                dtModifier.Dispose();
            }
            else
            {
                if (_Code.Trim() != "")
                {
                    txtModifierCode.Text = _Code;
                    txtDescription.Select();
                    txtDescription.Focus();
                }
            }
        }

        /// <summary>
        /// this method validate the input data and 
        /// then add or update Modifier 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {

                    case "SaveClose": // Add or Update Modifier
                        if (txtModifierCode.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter a modifier code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtModifierCode.Focus();
                            return;
                        }
                        if (txtDescription.Text.Trim() == "")
                        {
                            MessageBox.Show(" Please enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDescription.Focus();
                            return;
                        }

                        oModifier = setModifier();


                        // Chech for duplicate Modifier code
                        if (oModifier.CheckDuplicate(oModifier.ModifierID, txtModifierCode.Text.Trim(), txtDescription.Text.Trim()))
                        {
                            MessageBox.Show("Code or description is alredy in use by another entry.  Please select a unique code or description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtModifierCode.Focus();
                            return;
                        }

                        //Add modifier
                        _modifierID = oModifier.Add();
                        _Code = txtModifierCode.Text.Trim(); ;
                        _Description = txtDescription.Text.Trim(); ;

                        if (_modifierID > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _modifierID, 0, ActivityOutCome.Success);

                            this.Close();
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _modifierID, 0, ActivityOutCome.Failure);

                            MessageBox.Show(" Error ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "Save": // Add or Update Modifier
                        if (txtModifierCode.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter a modifier code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtModifierCode.Focus();
                            return;
                        }
                        if (txtDescription.Text.Trim() == "")
                        {
                            MessageBox.Show(" Please enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDescription.Focus();
                            return;
                        }

                        oModifier = setModifier();


                        // Chech for duplicate Modifier code
                        if (oModifier.CheckDuplicate(oModifier.ModifierID, txtModifierCode.Text.Trim(), txtDescription.Text.Trim()))
                        {
                            MessageBox.Show("Code or description is alredy in use by another entry.  Please select a unique code or description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtModifierCode.Focus();
                            return;
                        }

                        //Add modifier
                        _modifierID = oModifier.Add();
                        _Code = txtModifierCode.Text.Trim(); ;
                        _Description = txtDescription.Text.Trim(); ;

                        if (_modifierID > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _modifierID, 0, ActivityOutCome.Success);
                            txtDescription.Text = "";
                            txtModifierCode.Text = "";
                            _modifierID = 0;
                            
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _modifierID, 0, ActivityOutCome.Failure);

                            MessageBox.Show(" Error ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;


                    case "Cancel":
                        try
                        {
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null; 
                        }//catch
                        break;
                 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Modifier, ActivityType.Add, "Add Modifier", 0, _modifierID, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }   
        }

        /// <summary>
        /// create and set modifier object for Adding or updation
        /// </summary>
        /// <returns></returns>
        private Modifier setModifier()
        {
            Modifier oInternelModifier = new Modifier(DatabaseConnectionString);
            //set ModifierID = 0 for Adding new Modifier
            oInternelModifier.ModifierID = _modifierID;
            oInternelModifier.ModifierCode = txtModifierCode.Text.Trim();
            oInternelModifier.Decription = txtDescription.Text.Trim();
            return oInternelModifier;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtModifierCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]+$") == false)
                {
                    e.Handled = true;
                }
            }
        }
    }
}