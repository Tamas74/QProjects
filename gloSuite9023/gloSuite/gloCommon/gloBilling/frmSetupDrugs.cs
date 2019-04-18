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
    internal partial class frmSetupDrugs : Form
    {
        #region "Global Variable"
        //private variable for Messagebox Captions.
        private string _messageBoxCaption = String.Empty;

        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //


        #endregion "Global Variable"

        #region "Property Procedures"
        //private variable for connection string.
        private string _databaseconnectionstring = "";

        //variable that hold current drug id.
        private Int64 _drugID = 0;

       //property procedure for connection string.
        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        //property procedure for Drug ID.
        public Int64 DrugID
        {
            get { return _drugID; }
            set { _drugID = value; }
        }
        #endregion "Property Procedures"

        #region "Constructor"
        //Constructor with conn string.
        public frmSetupDrugs(string DatabaseConnectionString)
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

        //Constructor with conn string and id of current drug.
        public frmSetupDrugs(Int64 DrugID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _drugID = DrugID;

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
        #endregion "Constructor"

        #region "Form Load"
        private void frmSetupDrugs_Load(object sender, EventArgs e)
        {
            try
            {
                //Fill procedure to fill different controls on the page load
                _FillControls();

                // _drugID = 2;    //temp Pass Id for testing modify Operation.

                //for modify show information of the drug.
                if (_drugID != 0)
                {
                    //Create object to hold the Drug information.
                    Drugs oDrg = new Drugs(_databaseconnectionstring);
                    DataTable dt;

                    //Get the drug info in datatable 
                    dt = oDrg.GetDrug(_drugID);

                    //After reading data dispose the object.
                    oDrg.Dispose();

                    if (dt != null)
                    {
                        if (dt.Rows.Count != 0)
                        {
                            //Show Drug information in controls.

                            //nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, sDuration, bIsClinicalDrug, 
                            txtDrugsName.Text = dt.Rows[0]["sDrugName"].ToString();
                            txtGenericName.Text = dt.Rows[0]["sGenericName"].ToString();
                            txtDosage.Text = dt.Rows[0]["sDosage"].ToString();
                            txtRoute.Text = dt.Rows[0]["sRoute"].ToString();
                            txtFrequency.Text = dt.Rows[0]["sFrequency"].ToString();
                            txtDuration.Text = dt.Rows[0]["sDuration"].ToString();

                            if (Convert.ToBoolean(dt.Rows[0]["bIsClinicalDrug"]))
                            { chkClinicDrug.Checked = true; }
                            else { chkClinicDrug.Checked = false; }

                            //sAmount, nIsNarcotics, nddid, bIsAllergicDrug
                            txtAmount.Text = dt.Rows[0]["sAmount"].ToString();
                            //txtDrugsName.Text = dtCat.Rows[0][""].ToString();
                            //txtDrugsName.Text = dtCat.Rows[0][""].ToString();
                            if (Convert.ToBoolean(dt.Rows[0]["bIsAllergicDrug"]))
                            {
                                chkAllergicDrug.Checked = true;
                            }
                            else
                            {
                                chkAllergicDrug.Checked = false;
                            }
                            
                            cmbNarcotics.SelectedItem = dt.Rows[0]["nIsNarcotics"].ToString();
                        }
                    }

                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion "Form Load"

        #region "Fill Controls"
        private void _FillControls()
        {

            //Fill Narcotics Combo and show first one selected.
            cmbNarcotics.Items.Add("C1");
            cmbNarcotics.Items.Add("C2");
            cmbNarcotics.SelectedIndex = 0;

        }
        #endregion "Fill Controls"

        #region "ToolStrip Button Events"

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
              
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            //Validations for Drug Master
                            
                            //drug name not entered
                            if (txtDrugsName.Text.Trim() == "")
                            {

                                MessageBox.Show(this, "Please enter a drug name.  ", _messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                txtDrugsName.Focus();
                                return;
                            }
                            
                            //drug Generic name not entered
                            if (txtGenericName.Text.Trim() == "")
                            {

                                MessageBox.Show(this, "Please enter a drug generic name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtGenericName.Focus();
                                return;
                            }

                            ////drug Dosage not entered
                            //if (txtDosage.Text.Trim() == "")
                            //{

                            //    MessageBox.Show(this, "Please enter Dosage.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtDosage.Focus();
                            //    return;
                            //}

                            ////drug Route not entered
                            //if (txtRoute.Text.Trim() == "")
                            //{

                            //    MessageBox.Show(this, "Please enter Drug Route.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtRoute.Focus();
                            //    return;
                            //}

                            ////drug Frequency not entered
                            //if (txtFrequency.Text.Trim() == "")
                            //{

                            //    MessageBox.Show(this, "Please enter Drug Frequency.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtFrequency.Focus();
                            //    return;
                            //}

                            ////drug Duration not entered
                            //if (txtDuration.Text.Trim() == "")
                            //{

                            //    MessageBox.Show(this, "Please enter Drug Duration.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtDuration.Focus();
                            //    return;
                            //}

                            ////drug Amount not entered
                            //if (txtAmount.Text.Trim() == "")
                            //{
                            //    MessageBox.Show(this, "Please enter Drug Dispence.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtAmount.Focus();
                            //    return;
                            //}

                            ////drug Amount not entered correctly
                            //float Amt;
                            //if (!float.TryParse(txtAmount.Text.Trim(),out Amt))
                            //{
                            //    MessageBox.Show(this, "Please enter valid Drug Dispence.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                        }

                        //Create instance of Drug object
                        Drugs oDrugs = new Drugs(_databaseconnectionstring);

                        //Set values to Drug object
                        //nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, sDuration, bIsClinicalDrug, 
                        oDrugs.DrugsName = txtDrugsName.Text.Trim();
                        oDrugs.DrugsGenericName = txtGenericName.Text.Trim();
                        oDrugs.Dosage = txtDosage.Text.Trim();
                        oDrugs.Route = txtRoute.Text.Trim();
                        oDrugs.Frequency = txtFrequency.Text.Trim();
                        oDrugs.Duration = txtDuration.Text.Trim();
                        if (chkClinicDrug.Checked == true)
                            oDrugs.IsClinicalDrug = true;
                        else
                            oDrugs.IsClinicalDrug = false;

                        //sAmount, nIsNarcotics, nddid, bIsAllergicDrug
                        oDrugs.Amount = txtAmount.Text.Trim();
                        
                        ///    --- Temporary False -------  //
                        ///   

                        if (cmbNarcotics.Text == "C2")
                        {
                            oDrugs.IsNarcotics = 1;
                        }
                        else 
                        {
                            oDrugs.IsNarcotics = 0;
                        }

                        ///
                        ///    --- Temporary False -------  //

                        oDrugs.ddid = 0;

                        if (chkAllergicDrug.Checked == true)
                            oDrugs.IsAllergicDrug = true;
                        else
                            oDrugs.IsAllergicDrug = false;



                       // _drugID = 2;
                        if (_drugID == 0)
                        {
                            if (oDrugs.IsExists(0, oDrugs.DrugsName.ToString()) == true)
                            {
                                //Hit message on add operation Drug name is already in use.
                                MessageBox.Show(this, "Drug name  is alredy in use by another entry.  Please select a unique drug name.  ", _messageBoxCaption);
                                return;
                            }
                            _drugID = oDrugs.Add(_drugID);
                            if (_drugID == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Drugs, ActivityType.Add, "Add Drug", 0, _drugID, 0, ActivityOutCome.Failure);

                                //Hit message on add operation is not successfull
                                MessageBox.Show("Drug not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else 
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Drugs, ActivityType.Add, "Add Drug", 0, _drugID, 0, ActivityOutCome.Success);

                            }
                        }
                        else
                        {
                            oDrugs.DrugsID = _drugID;
                            if (oDrugs.IsExists(_drugID, oDrugs.DrugsName.Trim()) == true)
                            {
                                //Hit message on add operation Drug name is already in use.
                                MessageBox.Show(this, "Drug name  is alredy in use by another entry.  Please select a unique drug name.  ", _messageBoxCaption);
                                return;
                            }


                            if (oDrugs.Add(_drugID) == 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Drugs, ActivityType.Add, "Add Drug", 0, _drugID, 0, ActivityOutCome.Failure);

                                //Hit message on add operation is not successfull
                                MessageBox.Show("Drug not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Drugs, ActivityType.Add, "Add Drug", 0, _drugID, 0, ActivityOutCome.Success);

                            }
                        }
                        //Close the form 
                        this.Close();

                        //Dispose the object.
                        oDrugs.Dispose();
                        break;

                    case "Cancel":
                        //if cancel clicked then close the form.
                        this.Close();
                        break;

                    default:
                        //Default Case.
                        break;
                }
            }
             catch (gloDatabaseLayer.DBException  ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Drugs, ActivityType.Add, "Add Drug", 0, _drugID, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion "ToolStrip Button Events"
    }
}