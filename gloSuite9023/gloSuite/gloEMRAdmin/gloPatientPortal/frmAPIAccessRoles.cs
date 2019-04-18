using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommon;

namespace gloPatientPortal
{
    public partial class frmAPIAccessRoles : Form
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _RoleID = 0;
        private Int64 _loginUserId = 0;
        private Int64 _ClinicID = 0;
        public bool _showGrid = false;
        public bool ShowGrid
        {
            get { return _showGrid; }
            set
            {
                _showGrid = value;
                ShowGridPanel(value);
            }
        }
        private bool _IsModified = false;
       

        public void ShowGridPanel(bool showListmode)
        {
            if (showListmode)
            {

                pnl_Grid.BringToFront();
                pnlDetails.SendToBack();
            }
            else
            {
                pnlDetails.BringToFront();
                pnl_Grid.SendToBack();
            }
        }
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        public string SelectedCDASection { get; set; }
        public frmAPIAccessRoles()
        {
            InitializeComponent();
        }
        public frmAPIAccessRoles(string DatabaseConnectionString, long RoleID = 0, long loginuserid = 0)
        {
            InitializeComponent();
            _RoleID = RoleID;
            _loginUserId = loginuserid;
            _databaseconnectionstring = DatabaseConnectionString;
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

            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloEMR";
                }
            }
            else
            { _messageBoxCaption = "gloEMR"; }

            #endregion

            //Sandip Darade  20091006


        }
        private void frmAPIAccessRoles_Load(object sender, EventArgs e)
        {
            try
            {
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                if (_RoleID > 0)
                {
                    _IsModified = true;
                    clsAPIRole objRole = new clsAPIRole(_databaseconnectionstring);
                    DataTable dtRoleData = objRole.GetAPIRoles(_RoleID);
                    if (dtRoleData != null && dtRoleData.Rows.Count > 0)
                    {
                        txtRoleName.Text = dtRoleData.Rows[0]["sRoleName"].ToString();
                        if (dtRoleData.Rows[0]["bIsSystemDefined"].ToString().ToLower()=="true")
                        {
                            txtRoleName.Enabled = false;
                        }
                        else
                        {
                            txtRoleName.Enabled = true;
                        
                        }

                        SelectedCDASection = dtRoleData.Rows[0]["CDASections"].ToString();
                        SetCDASections(SelectedCDASection);
                    }
                }
                else
                {
                    _IsModified = false;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               
            }

        }
      
        

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoleName.Text))
                {
                    MessageBox.Show("Enter Role name.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_IsModified)
                {
                    clsAPIRole objRole = new clsAPIRole(_databaseconnectionstring);
                    objRole.RoleName = txtRoleName.Text.Trim();
                    objRole.RoleID = _RoleID;
                    objRole.IsSystemDefined = false;
                    objRole.CDASections = GetSelectedCDASections();
                    long _retRolId = objRole.Modify(objRole);
                    if (_retRolId > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIRole, gloAuditTrail.ActivityType.Modify, "API Role "+txtRoleName.Text.Trim()+" modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        this.Close();

                    }
                    else if (_retRolId == -1)
                    {
                        MessageBox.Show("Role name already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    clsAPIRole objRole = new clsAPIRole(_databaseconnectionstring);
                    objRole.RoleName = txtRoleName.Text.Trim();
                    objRole.IsSystemDefined = false;
                    objRole.UserID = _loginUserId;
                    objRole.CDASections = GetSelectedCDASections();
                    long _retRolId = objRole.Add(objRole);
                    if (_retRolId > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIRole, gloAuditTrail.ActivityType.Add, "API Role "+txtRoleName.Text.Trim()+" added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        this.Close();

                    }
                    else if (_retRolId == -1)
                    {
                        MessageBox.Show("Role name already exist", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private string GetSelectedCDASections()
        {
            string _retSection = "";
            try
            {
                if (chkCODemographic.Checked)
                {
                    _retSection += chkCODemographic.Tag + ",";
                }

                if (chkCOProblems.Checked)
                {
                    _retSection += chkCOProblems.Tag + ",";
                }
                if (chkCOAllergy.Checked)
                {
                    _retSection += chkCOAllergy.Tag + ",";
                }
                if (chkCOlabResult.Checked)
                {
                    _retSection += chkCOlabResult.Tag + ",";
                }
                if (chkCOProcedures.Checked)
                {
                    _retSection += chkCOProcedures.Tag + ",";
                }
                //if (chkCOCarePlan.Checked)
                //{
                //    _retSection += chkCOCarePlan.Tag + ",";
                //}
                if (chkCOMedication.Checked)
                {
                    _retSection += chkCOMedication.Tag + ",";
                }
                if (chkCOLabTest.Checked)
                {
                    _retSection += chkCOLabTest.Tag + ",";
                }
                if (chkCOVitalSigns.Checked)
                {
                    _retSection += chkCOVitalSigns.Tag + ",";
                }
                if (chkCOCareTeamMem.Checked)
                {
                    _retSection += chkCOCareTeamMem.Tag + ",";
                }
                if (chkCOSocialHistory.Checked)
                {
                    _retSection += chkCOSocialHistory.Tag + ",";
                }
                if (chkCOFamilyHistory.Checked)
                {
                    _retSection += chkCOFamilyHistory.Tag + ",";
                }
                if (chkImplant.Checked)
                {
                    _retSection += chkImplant.Tag + ",";
                }
                if (chkCSClinicalInstru.Checked)
                {
                    _retSection += chkCSClinicalInstru.Tag + ",";
                }
                if (chkPrivarySection.Checked)
                {
                    _retSection += chkPrivarySection.Tag + ",";
                }

                if (chkCSProviderName.Checked)
                {
                    _retSection += chkCSProviderName.Tag + ",";
                }
                if (chkAmbProviderContact.Checked)
                {
                    _retSection += chkAmbProviderContact.Tag + ",";
                }
                //------

                if (chkCareplanImmunizations.Checked)
                {
                    _retSection += chkCareplanImmunizations.Tag + ",";
                }
                if (ChkCOAssessments.Checked)
                {
                    _retSection += ChkCOAssessments.Tag + ",";
                }
                if (ChkCOTreatmentPlan.Checked)
                {
                    _retSection += ChkCOTreatmentPlan.Tag + ",";
                }
                if (ChkCOGoals.Checked)
                {
                    _retSection += ChkCOGoals.Tag + ",";
                }
                if (ChkCOHealthConcerns.Checked)
                {
                    _retSection += ChkCOHealthConcerns.Tag + ",";
                }
                if (chktransDateLocationvisit.Checked)
                {
                    _retSection += chktransDateLocationvisit.Tag + ",";
                }
                if (chkTransCareEncounter.Checked)
                {
                    _retSection += chkTransCareEncounter.Tag + ",";
                }
                if (chkTransCareCognitiveStat.Checked)
                {
                    _retSection += chkTransCareCognitiveStat.Tag + ",";
                }

                if (chkTransCareResReferral.Checked)
                {
                    _retSection += chkTransCareResReferral.Tag + ",";
                }
                if (chkTransCareRefProvider.Checked)
                {
                    _retSection += chkTransCareRefProvider.Tag + ",";
                }
                if (chkTransCareFunctionalStat.Checked)
                {
                    _retSection += chkTransCareFunctionalStat.Tag + ",";
                }
                //---------

                if (_retSection.Length > 0)
                {
                    _retSection = _retSection.Substring(0, _retSection.Length - 1);
                }
            }
            catch (Exception ex)
            {
                
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _retSection;
        }
        private void SetCDASections(string sDefaultCCDASections)
        {
            try
            {
                string[] sarrDefaultCCDASections = sDefaultCCDASections.ToString().Split(',');

                if (sarrDefaultCCDASections.Length == 27)
                {
                    chkSelectAll.Checked = true;
                    //chkSelectAll_CheckedChanged(null, null);
                }
                else
                {

                    if (sarrDefaultCCDASections.Contains("CCDAPatientDemographic"))
                        chkCODemographic.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAProblems"))
                        chkCOProblems.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAMedicationAllergies"))
                        chkCOAllergy.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDALaboratoryValue"))
                        chkCOlabResult.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAProcedures"))
                        chkCOProcedures.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAFamilyHistory"))
                        chkCOFamilyHistory.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDACSProviderName"))
                        chkCSProviderName.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAASImmunizations"))
                        chkCareplanImmunizations.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAEncounterDiagnoses"))
                        chkTransCareEncounter.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAMentalStatus"))
                        chkTransCareCognitiveStat.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAReasonforReferral"))
                        chkTransCareResReferral.Checked = true;

                    //if (sarrDefaultCCDASections.Contains("CCDACarePlan"))
                    //    chkCOCarePlan.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAMedications"))
                        chkCOMedication.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDALaboratoryTest"))
                        chkCOLabTest.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAVitalSigns"))
                        chkCOVitalSigns.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDACareTeamMember"))
                        chkCOCareTeamMem.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAClinicalInstructions"))
                        chkCSClinicalInstru.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDASocialHistory"))
                        chkCOSocialHistory.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAPrivarySection"))
                        chkPrivarySection.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAASProviderOfficeContactInformation"))
                        chkAmbProviderContact.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAFunctionalStatus"))
                        chkTransCareFunctionalStat.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAReferringProviders"))
                        chkTransCareRefProvider.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDACSDateLocationofVisit"))
                        chktransDateLocationvisit.Checked = true;


                    if (sarrDefaultCCDASections.Contains("CCDAImplants"))
                        chkImplant.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAAssessments"))
                        ChkCOAssessments.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDATreatmentPlan"))
                        ChkCOTreatmentPlan.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAGoals"))
                        ChkCOGoals.Checked = true;

                    if (sarrDefaultCCDASections.Contains("CCDAHealthConcerns"))
                        ChkCOHealthConcerns.Checked = true;
               
                }
                
                //---------
            }
            catch (Exception ex)
            {
                
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            

        
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.chkImplant.CheckedChanged -= new System.EventHandler(this.chkTrackAll_CheckedChanged);
           
            //    this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            try
            {
                bool checkALlStatus = chkSelectAll.Checked;
                //  chkCODemographic.Checked = checkALlStatus;
                chkCOProblems.Checked = checkALlStatus;
                chkCOAllergy.Checked = checkALlStatus;
                chkCOlabResult.Checked = checkALlStatus;
                chkCOProcedures.Checked = checkALlStatus;
                chkCOFamilyHistory.Checked = checkALlStatus;
                chkCSProviderName.Checked = checkALlStatus;
                chkCareplanImmunizations.Checked = checkALlStatus;
                chkTransCareEncounter.Checked = checkALlStatus;
                chkTransCareCognitiveStat.Checked = checkALlStatus;
                chkTransCareResReferral.Checked = checkALlStatus;
                
                //chkCOCarePlan.Checked = checkALlStatus;
                chkCOMedication.Checked = checkALlStatus;
                chkCOLabTest.Checked = checkALlStatus;
                chkCOVitalSigns.Checked = checkALlStatus;
                chkCOCareTeamMem.Checked = checkALlStatus;
                chkCSClinicalInstru.Checked = checkALlStatus;
                chkCOSocialHistory.Checked = checkALlStatus;
                chkPrivarySection.Checked = checkALlStatus;
                chkAmbProviderContact.Checked = checkALlStatus;
                chkTransCareFunctionalStat.Checked = checkALlStatus;
                chkTransCareRefProvider.Checked = checkALlStatus;
                chktransDateLocationvisit.Checked = checkALlStatus;

                chkImplant.Checked = checkALlStatus;
                ChkCOAssessments.Checked = checkALlStatus;
                ChkCOTreatmentPlan.Checked = checkALlStatus;
                ChkCOGoals.Checked = checkALlStatus;
                ChkCOHealthConcerns.Checked = checkALlStatus;

            }
            catch (Exception ex)
            {
                
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


            this.chkImplant.CheckedChanged += new System.EventHandler(this.chkTrackAll_CheckedChanged);

        }
       
        private void chkTrackAll_CheckedChanged(object sender, EventArgs e)
        {
            
            bool isAllchecked = true;
            foreach (Control c in this.pnlCommonMUData.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    if (((CheckBox)c).Checked == false && c.Name!="chkSelectAll")
                    {
                        isAllchecked = false;
                        break;
                    }
                }
            }

            if (isAllchecked)
            {
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                chkSelectAll.Checked = true;
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);

            }
            else
            {
                this.chkSelectAll.CheckedChanged -= new System.EventHandler(this.chkSelectAll_CheckedChanged);
                chkSelectAll.Checked = false;
                this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);

            }


        }

        

       
    }
}
