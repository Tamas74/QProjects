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
    public partial class frmSetupSpecialty : Form
    {

        #region "Declarations"
        private Int64 _SpecialtyID = 0;
        private string _databaseconnectionstring;
        public string _MessageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private Int64 _ClinicID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 SpecialtyID
        {
            get { return _SpecialtyID; }
            set { _SpecialtyID = value; }
        }
        #endregion  

        #region "Constructors"
        public frmSetupSpecialty(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
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

        public frmSetupSpecialty(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _SpecialtyID = ID;
            _databaseconnectionstring = databaseconnectionstring;
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

        #region " Form Load Event"
        private void frmSetupSpecialty_Load(object sender, EventArgs e)
        {
            if (_SpecialtyID != 0)
            {
                Specialty oSpec = new Specialty(_databaseconnectionstring);
                DataTable dtSpecialty;
                dtSpecialty = oSpec.GetSpecialty(_SpecialtyID);
                //nSpecialtyID, sDescription, nClinicID, sCode, sTaxonomy, bIsBlocked
                if (dtSpecialty != null)
                {
                    if (dtSpecialty.Rows.Count != 0)
                    {
                        txtSpecialtyCode.Text = dtSpecialty.Rows[0]["sCode"].ToString();
                        txtSpecialtyDesc.Text = dtSpecialty.Rows[0]["sDescription"].ToString();
                        txtTaxonomyCode.Text = dtSpecialty.Rows[0]["sTaxonomyCode"].ToString();
                        txtTaxonomyDesc.Text = dtSpecialty.Rows[0]["sTaxonomyDesc"].ToString();
                        txtClassification.Text = dtSpecialty.Rows[0]["sTaxonomyClassification"].ToString();
                    }
                }
                oSpec.Dispose();

            }
        } 
        #endregion

        #region " Tool Strip Event "
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
                    
                 case "Save":
                     if (Save())
                     {
                         _SpecialtyID = 0;
                         txtSpecialtyCode.Text = "";
                         txtSpecialtyDesc.Text = "";
                         txtTaxonomyCode.Text = "";
                         txtTaxonomyDesc.Text = "";
                         txtClassification.Text = "";
                         txtSpecialtyCode.Focus();
                     }
                break;
            
                case "Cancel":
                    this.Close();
                    break;
            }


        } 
        #endregion

        #region "Save Method"

        private bool Save()
        {
            if (txtSpecialtyCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpecialtyCode.Focus();
                return false;

            }
            if (txtSpecialtyDesc.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpecialtyDesc.Focus();
                return false;
            }
            // if (txtTaxonomy.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter Taxonomy!", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtTaxonomy.Focus();
            //    return false;
            //}
            try
            {
                Specialty oSpec = new Specialty(_databaseconnectionstring);
                oSpec.ClinicID = this.ClinicID;
                oSpec.SpecialtyID = _SpecialtyID;
                oSpec.SpecialtyCode = Convert.ToString(txtSpecialtyCode.Text.Trim());
                oSpec.Decription = Convert.ToString(txtSpecialtyDesc.Text.Trim());
                oSpec.TaxonomyCode = Convert.ToString(txtTaxonomyCode.Text.Trim());
                oSpec.TaxonomyDesc = Convert.ToString(txtTaxonomyDesc.Text.Trim());
                oSpec.Classification = Convert.ToString(txtClassification.Text.Trim());

                if (oSpec.CheckDuplicate(oSpec.SpecialtyID, oSpec.SpecialtyCode, oSpec.Decription))
                {
                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;

                }
                if (_SpecialtyID != 0)
                {
                    if (oSpec.Modify(_SpecialtyID) > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Speciality, ActivityType.Add, "Add  Speciality ", 0, _SpecialtyID, 0, ActivityOutCome.Success);

                        return true;
                    }
                    else 
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Speciality, ActivityType.Add, "Add  Speciality ", 0, _SpecialtyID, 0, ActivityOutCome.Failure);

                        return false ;
                    }
                }
                else
                {
                    _SpecialtyID =oSpec.Add();
                    if (_SpecialtyID > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Speciality, ActivityType.Add, "Add  Speciality ", 0, _SpecialtyID, 0, ActivityOutCome.Success);

                        return true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Speciality, ActivityType.Add, "Add  Speciality ", 0, _SpecialtyID, 0, ActivityOutCome.Failure);

                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Speciality, ActivityType.Add, "Add  Speciality ", 0, _SpecialtyID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return false;
        }

        #endregion
      
            
    }
}