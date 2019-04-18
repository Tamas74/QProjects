using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloPatient
{
    public partial class frmPatientRelationship : Form
    {
        #region 'Constructors'

        public frmPatientRelationship(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Added By Pramod Nair For Messagebox Caption 
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
        public frmPatientRelationship(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _nPatientRelationshipID = ID;

            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Added By Pramod Nair For Messagebox Caption 
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

        #region 'Declaration & Properties'

        private Int64 _nPatientRelationshipID = 0;
        private string _databaseconnectionstring;
        //public string _MessageBoxCaption = "gloPM";
        private string _MessageBoxCaption = String.Empty;


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 nPatientRelationshipID
        {
            get { return _nPatientRelationshipID; }
            set { _nPatientRelationshipID = value; }
        }

        #endregion 'Declaration'

        #region 'Form Load Event'
        private void frmPatientRelationship_Load(object sender, EventArgs e)
        {
            try
            {
                if (_nPatientRelationshipID != 0)
                {
                    RelationShip oRelationship = new RelationShip(_databaseconnectionstring);
                    DataTable dtRel;
                    dtRel = oRelationship.GetRelationShip(_nPatientRelationshipID);

                    if (dtRel != null)
                    {
                        if (dtRel.Rows.Count != 0)
                        {
                            txtRelationshipDesc.Text = dtRel.Rows[0]["sRelationshipDesc"].ToString();
                            txtRelationshipCode.Text = dtRel.Rows[0]["sRelationshipCode"].ToString();
                            

                        }
                    }
                    oRelationship.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region 'Tool Strip Event'
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (Save())
                    {
                        this.Close();
                    }

                    break;
                case "Cancel":
                    this.Close();
                    break;
                case "Save":
                    if (Save())
                    {
                        _nPatientRelationshipID = 0;
                        txtRelationshipCode.Text = "";
                        txtRelationshipDesc.Text = "";
                    }
                    break;
            }

        }
        #endregion

        #region 'Save Method'
        private bool Save()
        {
            if (txtRelationshipCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRelationshipCode.Focus();
                return false;

            }
            if (txtRelationshipDesc.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRelationshipDesc.Focus();
                return false;
            }
            try
            {
                RelationShip oRelationship = new RelationShip(_databaseconnectionstring);
                oRelationship.ClinicID = this.ClinicID;
                oRelationship.PatientRelID = _nPatientRelationshipID;
                oRelationship.RelationshipCode = Convert.ToString(txtRelationshipCode.Text.Trim());
                oRelationship.RelationshipDesc = Convert.ToString(txtRelationshipDesc.Text.Trim());

                //Shubhangi 20100317 CHECK WHETHER THE CODE OR DESCRIPTION IS ALREADY EXIST OR NOT.
                 Int32 _result = oRelationship.IsExists(_nPatientRelationshipID, oRelationship.RelationshipCode, oRelationship.RelationshipDesc);
                if (_result == 1)
                {
                    MessageBox.Show("Code is already in use by another entry. Select a unique code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRelationshipCode.Focus();
                    return false;

                }
                else if (_result == 2)
                {
                    MessageBox.Show("Description is already in use by another entry. Select a unique description.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRelationshipDesc.Focus();
                    return false;
                }


                _nPatientRelationshipID = oRelationship.AddPR();
                if (_nPatientRelationshipID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Relationship, ActivityType.Add, "Add Patient Relationship", 0, _nPatientRelationshipID, 0, ActivityOutCome.Success);

                    return true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Relationship, ActivityType.Add, "Add Patient Relationship", 0, _nPatientRelationshipID, 0, ActivityOutCome.Failure);

                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Relationship, ActivityType.Add, "Add Patient Relationship", 0, _nPatientRelationshipID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return false;

        }
        #endregion

    }
}