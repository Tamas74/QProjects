using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace gloPatient
{
    public partial class frmSetupInsurancePlan: Form
    {
        #region "Global Declarations for Variables"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private Int64 _ClinicID = 1;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = String.Empty;
        private Int64 _PatientID = 0;
        gloPatientInsuranceControl ogloPatientInsuranceControl = null;
       // PatientInsuranceOther _oPatientInsurances = new PatientInsuranceOther(); 
        #endregion "Global Declarations for Variables"


        #region "Delegates"

        public delegate void SaveandCloseHandler(Int64 PatientID);
        //public event SaveandCloseHandler EvntSaveandClose;

        #endregion

       // Patient oPatient = new Patient();
        gloPatient oPatientTrans;

        #region "Constructor"
       
        public frmSetupInsurancePlan(Int64 PatientID, String databaseconnectionstring)
        {
            InitializeComponent();

            _databaseconnectionstring = databaseconnectionstring;
            _PatientID = PatientID;
            
            //oPatientDemographicsControl = new gloPatientDemographicsControl(_PatientID, _databaseconnectionstring);
            //oPatientDemographicsControl.onDemographicControl_Enter += new gloPatientDemographicsControl.onDemographicControlEnter(oPatientDemographicsControl_onDemographicControl_Enter);
            //oPatientDemographicsControl.onDemographicControl_Leave += new gloPatientDemographicsControl.onDemographicControlLeave(oPatientDemographicsControl_onDemographicControl_Leave);
            //oPatientDemographicsControl.Dock = DockStyle.Fill;
            if (ogloPatientInsuranceControl != null)
            {
                try
                {
                    ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                    ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);
 
                }
                catch
                {
                }
                ogloPatientInsuranceControl.Dispose();
                ogloPatientInsuranceControl = null;
            }

            ogloPatientInsuranceControl = new gloPatientInsuranceControl(_databaseconnectionstring, 0, _PatientID);
            ogloPatientInsuranceControl.onInsuranceSave_Clicked += new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
            ogloPatientInsuranceControl.onInsuranceClose_Clicked += new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);
            ogloPatientInsuranceControl.IsAddInsurancePlanMode = true; 

            oPatientTrans = new gloPatient(_databaseconnectionstring);

           
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
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion 
           
        }

        #endregion "Constructor"

        #region "Form Load Event"

        private void frmSetupInsurancePlan_Load(object sender, EventArgs e)
        {
            try
            {
                if (_PatientID == 0)
                {
                    //pnlContainer.Controls.Add(ogloPatientInsuranceControl);
                }
                else
                {
                    Patient oPatient  = oPatientTrans.GetPatient(_PatientID);
                    ogloPatientInsuranceControl.InsuranceOtherDetails = oPatient.InsuranceDetails;
                    PatientInsuranceOther _oPatientInsurances;
                    _oPatientInsurances = oPatient.InsuranceDetails;

                    ogloPatientInsuranceControl.PatientFName = oPatient.DemographicsDetail.PatientFirstName;
                    ogloPatientInsuranceControl.PatientMName = oPatient.DemographicsDetail.PatientMiddleName;
                    ogloPatientInsuranceControl.PatientLName = oPatient.DemographicsDetail.PatientLastName; 

               

                    //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 
                    ogloPatientInsuranceControl.PatientAddressLine1 = oPatient.DemographicsDetail.PatientAddress1;
                    ogloPatientInsuranceControl.PatientAddressLine2 = oPatient.DemographicsDetail.PatientAddress2;
                    ogloPatientInsuranceControl.PatientCity = oPatient.DemographicsDetail.PatientCity;
                    ogloPatientInsuranceControl.PatientCounty = oPatient.DemographicsDetail.PatientCounty;
                    ogloPatientInsuranceControl.PatientZip = oPatient.DemographicsDetail.PatientZip;
                    ogloPatientInsuranceControl.PatientState = oPatient.DemographicsDetail.PatientState;
                    ogloPatientInsuranceControl.PatientCountry = oPatient.DemographicsDetail.PatientCountry;
                    ogloPatientInsuranceControl.PatientSSN = oPatient.DemographicsDetail.PatientSSN;
                    ogloPatientInsuranceControl.PatientDOB = oPatient.DemographicsDetail.PatientDOB.ToShortDateString();
                    ogloPatientInsuranceControl.PatientPhone = oPatient.DemographicsDetail.PatientPhone;
                    ogloPatientInsuranceControl.PatientGender = oPatient.DemographicsDetail.PatientGender; 

                    pnlContainer.Controls.Add(ogloPatientInsuranceControl);
                }

                ogloPatientInsuranceControl.Dock = DockStyle.Fill;
                ogloPatientInsuranceControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion "Form Load Event"

        #region "Main Tool Strip Events "


        //Save Insurence
        private void ogloPatientInsuranceControl_onInsuranceSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                PatientInsuranceOther _oPatientInsurances;
                   _oPatientInsurances = ogloPatientInsuranceControl.InsuranceOtherDetails;
                   oPatientTrans.Add_InsurancePlan(_oPatientInsurances, _PatientID);
                   this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                   this.Controls.Remove(ogloPatientInsuranceControl);
                   try
                   {
                       ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                       ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

                   }
                   catch
                   {
                   }
            }
            
        }

        //Close Insurence control
        private void ogloPatientInsuranceControl_onInsuranceClose_Clicked(object sender, EventArgs e)
        {
            this.Controls.Remove(ogloPatientInsuranceControl);
            try
            {
                ogloPatientInsuranceControl.onInsuranceSave_Clicked -= new gloPatientInsuranceControl.onInsuranceSaveClicked(ogloPatientInsuranceControl_onInsuranceSave_Clicked);
                ogloPatientInsuranceControl.onInsuranceClose_Clicked -= new gloPatientInsuranceControl.onInsuranceCloseClicked(ogloPatientInsuranceControl_onInsuranceClose_Clicked);

            }
            catch
            {
            }
            this.Close(); 
        }


        #endregion "Main Tool Strip Events "

      

    }
}