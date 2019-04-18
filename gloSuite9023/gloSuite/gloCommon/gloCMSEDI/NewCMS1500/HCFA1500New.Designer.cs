namespace gloCMSEDI
{
    partial class HCFA1500New
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            System.Windows.Forms.DateTimePicker[] cntdtControls = {    
   	                                                                    dtpPatientSignDate, 
                                                                        dtpDOS6To, 
                                                                        dtpDOS6From, 
                                                                        dtpDOS5To, 
                                                                        dtpDOS5From, 
                                                                        dtpDOS4To, 
                                                                        dtpDOS4From, 
                                                                        dtpDOS3To, 
                                                                        dtpDOS3From, 
                                                                        dtpDOS2To, 
                                                                        dtpDOS2From, 
                                                                        dtpDOS1To, 
                                                                        dtpDOS1From, 
                                                                        dtpHospitalisationTo, 
                                                                        dtpHospitalisationFrom, 
                                                                        dtpUnableToWorkTill, 
                                                                        dtpUnableToWorkFrom, 
                                                                        dtpInsuredsDOB, 
                                                                        dtpPhysicianSignDate, 
                                                                        dtpSimilarIllnessFirstDate, 
                                                                        dtpDateOfCurrentIllness, 
                                                                        dtpPatientDOB 
                                                                    };
            System.Windows.Forms.Control[] cntControls = {    
   	                                                                    dtpPatientSignDate, 
                                                                        dtpDOS6To, 
                                                                        dtpDOS6From, 
                                                                        dtpDOS5To, 
                                                                        dtpDOS5From, 
                                                                        dtpDOS4To, 
                                                                        dtpDOS4From, 
                                                                        dtpDOS3To, 
                                                                        dtpDOS3From, 
                                                                        dtpDOS2To, 
                                                                        dtpDOS2From, 
                                                                        dtpDOS1To, 
                                                                        dtpDOS1From, 
                                                                        dtpHospitalisationTo, 
                                                                        dtpHospitalisationFrom, 
                                                                        dtpUnableToWorkTill, 
                                                                        dtpUnableToWorkFrom, 
                                                                        dtpInsuredsDOB, 
                                                                        dtpPhysicianSignDate, 
                                                                        dtpSimilarIllnessFirstDate, 
                                                                        dtpDateOfCurrentIllness, 
                                                                        dtpPatientDOB 
                                                                    };

            if (disposing && (components != null))
            {
                try
                {
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }

                }
                catch
                {
                }
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
                try
                {
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (printdoc_HCFA1500 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(printdoc_HCFA1500);
                        printdoc_HCFA1500.Dispose();
                        printdoc_HCFA1500 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (longFont != null)
                    {
                        longFont.Dispose();
                        longFont = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (shortFont != null)
                    {
                        shortFont.Dispose();
                        shortFont = null;
                    }
                }
                catch
                {
                }

            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HCFA1500New));
            this.txtTransactionID = new System.Windows.Forms.TextBox();
            this.txtFacilityCode = new System.Windows.Forms.TextBox();
            this.txtFacilityDescription = new System.Windows.Forms.TextBox();
            this.printdoc_HCFA1500 = new System.Drawing.Printing.PrintDocument();
            this.pnlTextBox = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlMainForm = new System.Windows.Forms.Panel();
            this.txtSimilarIllnessFirstDateQualifier = new System.Windows.Forms.TextBox();
            this.txtDateOfCurrentIllnessQualifier = new System.Windows.Forms.TextBox();
            this.txtReferringProviderQualifier = new System.Windows.Forms.TextBox();
            this.txtIcdInd2 = new System.Windows.Forms.TextBox();
            this.txtIcdInd1 = new System.Windows.Forms.TextBox();
            this.txtOtherClaimIDQualifier = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeL = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeK = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeH = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeJ = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeI = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeG = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeF = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCodeE = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded6 = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded5 = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded4 = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded3 = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded2 = new System.Windows.Forms.TextBox();
            this.txtEPSDTShaded1 = new System.Windows.Forms.TextBox();
            this.txtPhyscianQualifierValue = new System.Windows.Forms.TextBox();
            this.txtReferringProvider_OtherType = new System.Windows.Forms.TextBox();
            this.txtReferringProvider_OtherValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider1_Qualifier = new System.Windows.Forms.TextBox();
            this.txtFacilityInfo = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider1_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider6_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider6_Qualifier = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider5_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider5_Qualifier = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider4_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider4_Qualifier = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider3_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider3_Qualifier = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider2_QualifierValue = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider2_Qualifier = new System.Windows.Forms.TextBox();
            this.btnBrowseReferral = new System.Windows.Forms.Button();
            this.btnBrowseFacility = new System.Windows.Forms.Button();
            this.txtRenderingProvider1_NPI = new System.Windows.Forms.TextBox();
            this.btn_PatientBrowse = new System.Windows.Forms.Button();
            this.btn_BrowsePatientRegt = new System.Windows.Forms.Button();
            this.txtNotes6 = new System.Windows.Forms.TextBox();
            this.txtNotes5 = new System.Windows.Forms.TextBox();
            this.txtNotes4 = new System.Windows.Forms.TextBox();
            this.txtNotes3 = new System.Windows.Forms.TextBox();
            this.txtNotes2 = new System.Windows.Forms.TextBox();
            this.txtNotes1 = new System.Windows.Forms.TextBox();
            this.txtPayerNameAndAddress = new System.Windows.Forms.TextBox();
            this.dtpPatientSignDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS6To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS6From = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS5To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS5From = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS4To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS4From = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS3To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS3From = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS2To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS2From = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS1To = new System.Windows.Forms.DateTimePicker();
            this.dtpDOS1From = new System.Windows.Forms.DateTimePicker();
            this.dtpHospitalisationTo = new System.Windows.Forms.DateTimePicker();
            this.dtpHospitalisationFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpUnableToWorkTill = new System.Windows.Forms.DateTimePicker();
            this.dtpUnableToWorkFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpInsuredsDOB = new System.Windows.Forms.DateTimePicker();
            this.dtpPhysicianSignDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSimilarIllnessFirstDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDateOfCurrentIllness = new System.Windows.Forms.DateTimePicker();
            this.dtpPatientDOB = new System.Windows.Forms.DateTimePicker();
            this.txtPatientZip = new System.Windows.Forms.TextBox();
            this.txtInsuredsZip = new System.Windows.Forms.TextBox();
            this.txtInsuredTelephone2 = new System.Windows.Forms.TextBox();
            this.txtPatientTelephone2 = new System.Windows.Forms.TextBox();
            this.txtInsuredTelephone1 = new System.Windows.Forms.TextBox();
            this.txtPatientTelephone1 = new System.Windows.Forms.TextBox();
            this.txtPatientState = new System.Windows.Forms.TextBox();
            this.txtInsuredsState = new System.Windows.Forms.TextBox();
            this.txtPatientCity = new System.Windows.Forms.TextBox();
            this.txtInsuredsCity = new System.Windows.Forms.TextBox();
            this.txtPatientCoditionRelatedTo_State = new System.Windows.Forms.TextBox();
            this.txtReserveForLocalUse = new System.Windows.Forms.TextBox();
            this.txtPatientCondition = new System.Windows.Forms.TextBox();
            this.txtInsuredPolicyorFECANo = new System.Windows.Forms.TextBox();
            this.txtInsuredInsurancePlanName = new System.Windows.Forms.TextBox();
            this.txtInsuredEmployerORSchoolName = new System.Windows.Forms.TextBox();
            this.txtOutsideLabCharges2 = new System.Windows.Forms.TextBox();
            this.txtPriorAuthorizationNumber = new System.Windows.Forms.TextBox();
            this.txtOriginalRefNumber = new System.Windows.Forms.TextBox();
            this.txtMedicaidResubmissionCode = new System.Windows.Forms.TextBox();
            this.txtOutsideLabCharges1 = new System.Windows.Forms.TextBox();
            this.txtReferringProvider_NPI = new System.Windows.Forms.TextBox();
            this.txtBillingProv_b_UPIN = new System.Windows.Forms.TextBox();
            this.txtBillingProv_a_NPI = new System.Windows.Forms.TextBox();
            this.txtFacility_b = new System.Windows.Forms.TextBox();
            this.txtFacility_a_NPI = new System.Windows.Forms.TextBox();
            this.txtCPT6 = new System.Windows.Forms.TextBox();
            this.txtCPT5 = new System.Windows.Forms.TextBox();
            this.txtCPT4 = new System.Windows.Forms.TextBox();
            this.txtCPT3 = new System.Windows.Forms.TextBox();
            this.txtEMG6 = new System.Windows.Forms.TextBox();
            this.txtCPT2 = new System.Windows.Forms.TextBox();
            this.txtEMG5 = new System.Windows.Forms.TextBox();
            this.txtCPT1 = new System.Windows.Forms.TextBox();
            this.txtEMG4 = new System.Windows.Forms.TextBox();
            this.txtEMG3 = new System.Windows.Forms.TextBox();
            this.txtEMG2 = new System.Windows.Forms.TextBox();
            this.txtEMG1 = new System.Windows.Forms.TextBox();
            this.txtEPSDT6 = new System.Windows.Forms.TextBox();
            this.txtEPSDT5 = new System.Windows.Forms.TextBox();
            this.txtEPSDT4 = new System.Windows.Forms.TextBox();
            this.txtEPSDT3 = new System.Windows.Forms.TextBox();
            this.txtBillingProviderPhone1 = new System.Windows.Forms.TextBox();
            this.txtAmountPaid2 = new System.Windows.Forms.TextBox();
            this.txtTotalCharges2 = new System.Windows.Forms.TextBox();
            this.txtCharges61 = new System.Windows.Forms.TextBox();
            this.txtEPSDT2 = new System.Windows.Forms.TextBox();
            this.txtCharges51 = new System.Windows.Forms.TextBox();
            this.txtEPSDT1 = new System.Windows.Forms.TextBox();
            this.txtCharges41 = new System.Windows.Forms.TextBox();
            this.txtCharges31 = new System.Windows.Forms.TextBox();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.txtTotalCharges = new System.Windows.Forms.TextBox();
            this.txtCharges6 = new System.Windows.Forms.TextBox();
            this.txtCharges21 = new System.Windows.Forms.TextBox();
            this.txtCharges5 = new System.Windows.Forms.TextBox();
            this.txtCharges11 = new System.Windows.Forms.TextBox();
            this.txtCharges4 = new System.Windows.Forms.TextBox();
            this.txtCharges3 = new System.Windows.Forms.TextBox();
            this.txtBillingProviderPhone2 = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider6_NPI = new System.Windows.Forms.TextBox();
            this.txtCharges2 = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider5_NPI = new System.Windows.Forms.TextBox();
            this.txtCharges1 = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider4_NPI = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider3_NPI = new System.Windows.Forms.TextBox();
            this.txtUnits6 = new System.Windows.Forms.TextBox();
            this.txtRenderingProvider2_NPI = new System.Windows.Forms.TextBox();
            this.txtUnits5 = new System.Windows.Forms.TextBox();
            this.txtUnits4 = new System.Windows.Forms.TextBox();
            this.txtUnits3 = new System.Windows.Forms.TextBox();
            this.txtDxPtr6 = new System.Windows.Forms.TextBox();
            this.txtUnits2 = new System.Windows.Forms.TextBox();
            this.txtDxPtr5 = new System.Windows.Forms.TextBox();
            this.txtUnits1 = new System.Windows.Forms.TextBox();
            this.txtDxPtr4 = new System.Windows.Forms.TextBox();
            this.txtMOD64 = new System.Windows.Forms.TextBox();
            this.txtDxPtr3 = new System.Windows.Forms.TextBox();
            this.txtMOD54 = new System.Windows.Forms.TextBox();
            this.txtDxPtr2 = new System.Windows.Forms.TextBox();
            this.txtMOD44 = new System.Windows.Forms.TextBox();
            this.txtMOD34 = new System.Windows.Forms.TextBox();
            this.txtMOD63 = new System.Windows.Forms.TextBox();
            this.txtMOD24 = new System.Windows.Forms.TextBox();
            this.txtMOD53 = new System.Windows.Forms.TextBox();
            this.txtDxPtr1 = new System.Windows.Forms.TextBox();
            this.txtMOD43 = new System.Windows.Forms.TextBox();
            this.txtMOD33 = new System.Windows.Forms.TextBox();
            this.txtMOD62 = new System.Windows.Forms.TextBox();
            this.txtMOD23 = new System.Windows.Forms.TextBox();
            this.txtMOD52 = new System.Windows.Forms.TextBox();
            this.txtMOD14 = new System.Windows.Forms.TextBox();
            this.txtMOD42 = new System.Windows.Forms.TextBox();
            this.txtMOD61 = new System.Windows.Forms.TextBox();
            this.txtMOD32 = new System.Windows.Forms.TextBox();
            this.txtMOD51 = new System.Windows.Forms.TextBox();
            this.txtMOD22 = new System.Windows.Forms.TextBox();
            this.txtMOD41 = new System.Windows.Forms.TextBox();
            this.txtPOS6 = new System.Windows.Forms.TextBox();
            this.txtMOD31 = new System.Windows.Forms.TextBox();
            this.txtPOS5 = new System.Windows.Forms.TextBox();
            this.txtMOD13 = new System.Windows.Forms.TextBox();
            this.txtPOS4 = new System.Windows.Forms.TextBox();
            this.txtMOD21 = new System.Windows.Forms.TextBox();
            this.txtPOS3 = new System.Windows.Forms.TextBox();
            this.txtMOD12 = new System.Windows.Forms.TextBox();
            this.txtPOS2 = new System.Windows.Forms.TextBox();
            this.txtMOD11 = new System.Windows.Forms.TextBox();
            this.txtPOS1 = new System.Windows.Forms.TextBox();
            this.txtPhyscianSignature = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCode41 = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCode31 = new System.Windows.Forms.TextBox();
            this.txtBillingProviderInfo = new System.Windows.Forms.TextBox();
            this.txtInsuredPersonSign = new System.Windows.Forms.TextBox();
            this.txtPatientSignature = new System.Windows.Forms.TextBox();
            this.txtPatientAccountNo = new System.Windows.Forms.TextBox();
            this.txtFederalTaxID = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCode21 = new System.Windows.Forms.TextBox();
            this.txtDiagnosisCode11 = new System.Windows.Forms.TextBox();
            this.txtReferringProviderName = new System.Windows.Forms.TextBox();
            this.txtOtherInsuredInsuranceName = new System.Windows.Forms.TextBox();
            this.txt19ReservedForLocalUse = new System.Windows.Forms.TextBox();
            this.txtOtherInsuredPolicyNo = new System.Windows.Forms.TextBox();
            this.txtOtherInsuredName = new System.Windows.Forms.TextBox();
            this.txtPatientAddress = new System.Windows.Forms.TextBox();
            this.txtInsuredsAddress = new System.Windows.Forms.TextBox();
            this.txtInsuredName = new System.Windows.Forms.TextBox();
            this.txtInsuredIdNumber = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.chkPatient_Female = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_OtherAccident_No = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_AutoAccident_No = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes = new System.Windows.Forms.CheckBox();
            this.chkIsOtherHealthPlan_Yes = new System.Windows.Forms.CheckBox();
            this.chkInsuredSex_Male = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_Employment_Yes = new System.Windows.Forms.CheckBox();
            this.chkOutsideLab_No = new System.Windows.Forms.CheckBox();
            this.chkAcceptAssignment_No = new System.Windows.Forms.CheckBox();
            this.chkAcceptAssignment_Yes = new System.Windows.Forms.CheckBox();
            this.chkFederalTaxID_EIN = new System.Windows.Forms.CheckBox();
            this.chkFederalTaxID_SSN = new System.Windows.Forms.CheckBox();
            this.chkOutsideLab_Yes = new System.Windows.Forms.CheckBox();
            this.chkIsOtherHealthPlan_No = new System.Windows.Forms.CheckBox();
            this.chkInsuredSex_Female = new System.Windows.Forms.CheckBox();
            this.chkPatientCoditionRelatedTo_Employment_No = new System.Windows.Forms.CheckBox();
            this.chkRelationship_Other = new System.Windows.Forms.CheckBox();
            this.chkRelationship_Child = new System.Windows.Forms.CheckBox();
            this.chkRelationship_Spouse = new System.Windows.Forms.CheckBox();
            this.chkRelationship_Self = new System.Windows.Forms.CheckBox();
            this.chkPatient_Male = new System.Windows.Forms.CheckBox();
            this.chkOtherInsuranceType = new System.Windows.Forms.CheckBox();
            this.chkFECABlackLung = new System.Windows.Forms.CheckBox();
            this.chkGroupHealthPlan = new System.Windows.Forms.CheckBox();
            this.chkCHAMPVA = new System.Windows.Forms.CheckBox();
            this.chkTricareChampus = new System.Windows.Forms.CheckBox();
            this.chkMedicaid = new System.Windows.Forms.CheckBox();
            this.chkMedicare = new System.Windows.Forms.CheckBox();
            this.cmbRenderingProvider1 = new System.Windows.Forms.ComboBox();
            this.cmbRenderingProvider2 = new System.Windows.Forms.ComboBox();
            this.cmbRenderingProvider3 = new System.Windows.Forms.ComboBox();
            this.cmbRenderingProvider4 = new System.Windows.Forms.ComboBox();
            this.cmbRenderingProvider5 = new System.Windows.Forms.ComboBox();
            this.cmbRenderingProvider6 = new System.Windows.Forms.ComboBox();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblBottom = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.pnlMainFormLabel = new System.Windows.Forms.Panel();
            this.lblBillingProviderPhone2 = new System.Windows.Forms.Label();
            this.lblBillingProviderPhone1 = new System.Windows.Forms.Label();
            this.lblOtherClaimIDQualifier = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeE = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeF = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeG = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeH = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeI = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeJ = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeK = new System.Windows.Forms.Label();
            this.lblDiagnosisCodeL = new System.Windows.Forms.Label();
            this.lblReferringProviderQualifier = new System.Windows.Forms.Label();
            this.lblSimilarIllnessFirstDateQualifier = new System.Windows.Forms.Label();
            this.lblDateOfCurrentIllnessQualifier = new System.Windows.Forms.Label();
            this.lblIcdInd1 = new System.Windows.Forms.Label();
            this.lblIcdInd2 = new System.Windows.Forms.Label();
            this.lblShadedEPSDT6 = new System.Windows.Forms.Label();
            this.lblShadedEPSDT5 = new System.Windows.Forms.Label();
            this.lblShadedEPSDT4 = new System.Windows.Forms.Label();
            this.lblEPSDTShaded3 = new System.Windows.Forms.Label();
            this.lblShadedEPSDT2 = new System.Windows.Forms.Label();
            this.lblShadedEPSDT1 = new System.Windows.Forms.Label();
            this.lblPhyscianQualifierValue = new System.Windows.Forms.Label();
            this.lblReferringProvider_OtherType = new System.Windows.Forms.Label();
            this.lblReferringProvider_OtherValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider6_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider5_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider4_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider3_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider2_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider1_QFValue = new System.Windows.Forms.Label();
            this.lblRenderingProvider6_QF = new System.Windows.Forms.Label();
            this.lblRenderingProvider5_QF = new System.Windows.Forms.Label();
            this.lblRenderingProvider4_QF = new System.Windows.Forms.Label();
            this.lblRenderingProvider3_QF = new System.Windows.Forms.Label();
            this.lblRenderingProvider2_QF = new System.Windows.Forms.Label();
            this.lblRenderingProvider1_QF = new System.Windows.Forms.Label();
            this.lblAmountPaid1 = new System.Windows.Forms.Label();
            this.lblNotes6 = new System.Windows.Forms.Label();
            this.lblNotes5 = new System.Windows.Forms.Label();
            this.lblNotes4 = new System.Windows.Forms.Label();
            this.lblNotes3 = new System.Windows.Forms.Label();
            this.lblNotes2 = new System.Windows.Forms.Label();
            this.btn_BrowseFacility = new System.Windows.Forms.Button();
            this.lblBillingProv_b_UPIN = new System.Windows.Forms.Label();
            this.lblBillingProv_a_NPI = new System.Windows.Forms.Label();
            this.lblBillingProviderInfo = new System.Windows.Forms.Label();
            this.lblFacility_b = new System.Windows.Forms.Label();
            this.lblFacility_a_NPI = new System.Windows.Forms.Label();
            this.lblFacilityInfo = new System.Windows.Forms.Label();
            this.lblPhysicianSignDate_YY = new System.Windows.Forms.Label();
            this.lblPhysicianSignDate_DD = new System.Windows.Forms.Label();
            this.lblPhysicianSignDate_MM = new System.Windows.Forms.Label();
            this.lblPhyscianSignature = new System.Windows.Forms.Label();
            this.lblBalanceDue2 = new System.Windows.Forms.Label();
            this.lblBalanceDue = new System.Windows.Forms.Label();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.lblTotalCharges2 = new System.Windows.Forms.Label();
            this.lblTotalCharges = new System.Windows.Forms.Label();
            this.lblAcceptAssignment_No = new System.Windows.Forms.Label();
            this.lblAcceptAssignment_Yes = new System.Windows.Forms.Label();
            this.lblPatientAccountNo = new System.Windows.Forms.Label();
            this.lblFederalTaxID_EIN = new System.Windows.Forms.Label();
            this.lblFederalTaxID_SSN = new System.Windows.Forms.Label();
            this.lblFederalTaxID = new System.Windows.Forms.Label();
            this.lblRenderingProvider6_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT6 = new System.Windows.Forms.Label();
            this.lblUnits6 = new System.Windows.Forms.Label();
            this.lblCharges61 = new System.Windows.Forms.Label();
            this.lblCharges6 = new System.Windows.Forms.Label();
            this.lblDxPtr6 = new System.Windows.Forms.Label();
            this.lblMOD64 = new System.Windows.Forms.Label();
            this.lblMOD63 = new System.Windows.Forms.Label();
            this.lblMOD62 = new System.Windows.Forms.Label();
            this.lblMOD61 = new System.Windows.Forms.Label();
            this.lblCPT6 = new System.Windows.Forms.Label();
            this.lblEMG6 = new System.Windows.Forms.Label();
            this.lblPOS6 = new System.Windows.Forms.Label();
            this.lblDOS6To_YY = new System.Windows.Forms.Label();
            this.lblDOS6To_DD = new System.Windows.Forms.Label();
            this.lblDOS6To_MM = new System.Windows.Forms.Label();
            this.lblDOS6From_YY = new System.Windows.Forms.Label();
            this.lblDOS6From_DD = new System.Windows.Forms.Label();
            this.lblDOS6From_MM = new System.Windows.Forms.Label();
            this.lblRenderingProvider5_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT5 = new System.Windows.Forms.Label();
            this.lblUnits5 = new System.Windows.Forms.Label();
            this.lblCharges51 = new System.Windows.Forms.Label();
            this.lblCharges5 = new System.Windows.Forms.Label();
            this.lblDxPtr5 = new System.Windows.Forms.Label();
            this.lblMOD54 = new System.Windows.Forms.Label();
            this.lblMOD53 = new System.Windows.Forms.Label();
            this.lblMOD52 = new System.Windows.Forms.Label();
            this.lblMOD51 = new System.Windows.Forms.Label();
            this.lblCPT5 = new System.Windows.Forms.Label();
            this.lblEMG5 = new System.Windows.Forms.Label();
            this.lblPOS5 = new System.Windows.Forms.Label();
            this.lblDOS5To_YY = new System.Windows.Forms.Label();
            this.lblDOS5To_DD = new System.Windows.Forms.Label();
            this.lblDOS5To_MM = new System.Windows.Forms.Label();
            this.lblDOS5From_YY = new System.Windows.Forms.Label();
            this.lblDOS5From_DD = new System.Windows.Forms.Label();
            this.lblDOS5From_MM = new System.Windows.Forms.Label();
            this.lblRenderingProvider4_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT4 = new System.Windows.Forms.Label();
            this.lblUnits4 = new System.Windows.Forms.Label();
            this.lblCharges41 = new System.Windows.Forms.Label();
            this.lblCharges4 = new System.Windows.Forms.Label();
            this.lblDxPtr4 = new System.Windows.Forms.Label();
            this.lblMOD44 = new System.Windows.Forms.Label();
            this.lblMOD43 = new System.Windows.Forms.Label();
            this.lblMOD42 = new System.Windows.Forms.Label();
            this.lblMOD41 = new System.Windows.Forms.Label();
            this.lblCPT4 = new System.Windows.Forms.Label();
            this.lblEMG4 = new System.Windows.Forms.Label();
            this.lblPOS4 = new System.Windows.Forms.Label();
            this.lblDOS4To_YY = new System.Windows.Forms.Label();
            this.lblDOS4To_DD = new System.Windows.Forms.Label();
            this.lblDOS4To_MM = new System.Windows.Forms.Label();
            this.lblDOS4From_YY = new System.Windows.Forms.Label();
            this.lblDOS4From_DD = new System.Windows.Forms.Label();
            this.lblDOS4From_MM = new System.Windows.Forms.Label();
            this.lblRenderingProvider3_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT3 = new System.Windows.Forms.Label();
            this.lblUnits3 = new System.Windows.Forms.Label();
            this.lblCharges31 = new System.Windows.Forms.Label();
            this.lblCharges3 = new System.Windows.Forms.Label();
            this.lblDxPtr3 = new System.Windows.Forms.Label();
            this.lblMOD34 = new System.Windows.Forms.Label();
            this.lblMOD33 = new System.Windows.Forms.Label();
            this.lblMOD32 = new System.Windows.Forms.Label();
            this.lblMOD31 = new System.Windows.Forms.Label();
            this.lblCPT3 = new System.Windows.Forms.Label();
            this.lblEMG3 = new System.Windows.Forms.Label();
            this.lblPOS3 = new System.Windows.Forms.Label();
            this.lblDOS3To_YY = new System.Windows.Forms.Label();
            this.lblDOS3To_DD = new System.Windows.Forms.Label();
            this.lblDOS3To_MM = new System.Windows.Forms.Label();
            this.lblDOS3From_YY = new System.Windows.Forms.Label();
            this.lblDOS3From_DD = new System.Windows.Forms.Label();
            this.lblDOS3From_MM = new System.Windows.Forms.Label();
            this.lblRenderingProvider2_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT2 = new System.Windows.Forms.Label();
            this.lblUnits2 = new System.Windows.Forms.Label();
            this.lblCharges21 = new System.Windows.Forms.Label();
            this.lblCharges2 = new System.Windows.Forms.Label();
            this.lblDxPtr2 = new System.Windows.Forms.Label();
            this.lblMOD24 = new System.Windows.Forms.Label();
            this.lblMOD23 = new System.Windows.Forms.Label();
            this.lblMOD22 = new System.Windows.Forms.Label();
            this.lblMOD21 = new System.Windows.Forms.Label();
            this.lblCPT2 = new System.Windows.Forms.Label();
            this.lblEMG2 = new System.Windows.Forms.Label();
            this.lblPOS2 = new System.Windows.Forms.Label();
            this.lblDOS2To_YY = new System.Windows.Forms.Label();
            this.lblDOS2To_DD = new System.Windows.Forms.Label();
            this.lblDOS2To_MM = new System.Windows.Forms.Label();
            this.lblDOS2From_YY = new System.Windows.Forms.Label();
            this.lblDOS2From_DD = new System.Windows.Forms.Label();
            this.lblDOS2From_MM = new System.Windows.Forms.Label();
            this.lblRenderingProvider1_NPI = new System.Windows.Forms.Label();
            this.lblEPSDT1 = new System.Windows.Forms.Label();
            this.lblUnits1 = new System.Windows.Forms.Label();
            this.lblCharges11 = new System.Windows.Forms.Label();
            this.lblCharges1 = new System.Windows.Forms.Label();
            this.lblDxPtr1 = new System.Windows.Forms.Label();
            this.lblMOD14 = new System.Windows.Forms.Label();
            this.lblMOD13 = new System.Windows.Forms.Label();
            this.lblMOD12 = new System.Windows.Forms.Label();
            this.lblMOD11 = new System.Windows.Forms.Label();
            this.lblCPT1 = new System.Windows.Forms.Label();
            this.lblEMG1 = new System.Windows.Forms.Label();
            this.lblPOS1 = new System.Windows.Forms.Label();
            this.lblDOS1To_YY = new System.Windows.Forms.Label();
            this.lblDOS1To_DD = new System.Windows.Forms.Label();
            this.lblDOS1To_MM = new System.Windows.Forms.Label();
            this.lblDOS1From_YY = new System.Windows.Forms.Label();
            this.lblDOS1From_DD = new System.Windows.Forms.Label();
            this.lblDOS1From_MM = new System.Windows.Forms.Label();
            this.lblNotes1 = new System.Windows.Forms.Label();
            this.lblPriorAuthorizationNumber = new System.Windows.Forms.Label();
            this.lblDiagnosisCode41 = new System.Windows.Forms.Label();
            this.lblDiagnosisCode21 = new System.Windows.Forms.Label();
            this.lblOriginalRefNumber = new System.Windows.Forms.Label();
            this.lblMedicaidResubmissionCode = new System.Windows.Forms.Label();
            this.lblDiagnosisCode31 = new System.Windows.Forms.Label();
            this.lblDiagnosisCode11 = new System.Windows.Forms.Label();
            this.lblOutsideLabCharges2 = new System.Windows.Forms.Label();
            this.lblOutsideLabCharges1 = new System.Windows.Forms.Label();
            this.lblOutsideLab_No = new System.Windows.Forms.Label();
            this.lblOutsideLab_Yes = new System.Windows.Forms.Label();
            this.lblReservedForLocalUse = new System.Windows.Forms.Label();
            this.lblHospitalisationTo_YY = new System.Windows.Forms.Label();
            this.lblHospitalisationTo_DD = new System.Windows.Forms.Label();
            this.lblHospitalisationTo_MM = new System.Windows.Forms.Label();
            this.lblHospitalisationFrom_YY = new System.Windows.Forms.Label();
            this.lblHospitalisationFrom_DD = new System.Windows.Forms.Label();
            this.lblHospitalisationFrom_MM = new System.Windows.Forms.Label();
            this.lblReferringProvider_NPI = new System.Windows.Forms.Label();
            this.btn_BrowseReferral = new System.Windows.Forms.Button();
            this.lblReferringProviderName = new System.Windows.Forms.Label();
            this.lblUnableToWorkTill_YY = new System.Windows.Forms.Label();
            this.lblUnableToWorkTill_DD = new System.Windows.Forms.Label();
            this.lblUnableToWorkTill_MM = new System.Windows.Forms.Label();
            this.lblUnableToWorkFrom_YY = new System.Windows.Forms.Label();
            this.lblUnableToWorkFrom_DD = new System.Windows.Forms.Label();
            this.lblUnableToWorkFrom_MM = new System.Windows.Forms.Label();
            this.lblSimilarIllnessFirstDate_YY = new System.Windows.Forms.Label();
            this.lblSimilarIllnessFirstDate_DD = new System.Windows.Forms.Label();
            this.lblSimilarIllnessFirstDate_MM = new System.Windows.Forms.Label();
            this.lblDateOfCurrentIllness_YY = new System.Windows.Forms.Label();
            this.lblDateOfCurrentIllness_DD = new System.Windows.Forms.Label();
            this.lblDateOfCurrentIllness_MM = new System.Windows.Forms.Label();
            this.lblInsuredPersonSign = new System.Windows.Forms.Label();
            this.lblPatientSignDate_YY = new System.Windows.Forms.Label();
            this.lblPatientSignDate_DD = new System.Windows.Forms.Label();
            this.lblPatientSignDate_MM = new System.Windows.Forms.Label();
            this.lblPatientSignature = new System.Windows.Forms.Label();
            this.btnBrowsePatientRegt = new System.Windows.Forms.Button();
            this.lblIsOtherHealthPlan_No = new System.Windows.Forms.Label();
            this.lblIsOtherHealthPlan_Yes = new System.Windows.Forms.Label();
            this.lblReserveForLocalUse = new System.Windows.Forms.Label();
            this.lblOtherInsuredInsuranceName = new System.Windows.Forms.Label();
            this.lblInsuredInsurancePlanName = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_OtherAccident_No = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes = new System.Windows.Forms.Label();
            this.lblOtherInsuredEmployerORSchoolName = new System.Windows.Forms.Label();
            this.lblInsuredEmployerORSchoolName = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_State = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_AutoAccident_No = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes = new System.Windows.Forms.Label();
            this.lblInsuredSex_Female = new System.Windows.Forms.Label();
            this.lblInsuredSex_Male = new System.Windows.Forms.Label();
            this.lblInsuredsDOB_YY = new System.Windows.Forms.Label();
            this.lblInsuredsDOB_DD = new System.Windows.Forms.Label();
            this.lblInsuredsDOB_MM = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_Employment_No = new System.Windows.Forms.Label();
            this.lblPatientCoditionRelatedTo_Employment_Yes = new System.Windows.Forms.Label();
            this.lblOtherInsuredPolicyNo = new System.Windows.Forms.Label();
            this.lblInsuredPolicyorFECANo = new System.Windows.Forms.Label();
            this.lblPatientCondition = new System.Windows.Forms.Label();
            this.lblOtherInsuredName = new System.Windows.Forms.Label();
            this.lblInsuredTelephone2 = new System.Windows.Forms.Label();
            this.lblInsuredTelephone1 = new System.Windows.Forms.Label();
            this.lblInsuredsZip = new System.Windows.Forms.Label();
            this.lblPatientTelephone2 = new System.Windows.Forms.Label();
            this.lblPatientTelephone1 = new System.Windows.Forms.Label();
            this.lblPatientZip = new System.Windows.Forms.Label();
            this.lblInsuredsState = new System.Windows.Forms.Label();
            this.lblInsuredsCity = new System.Windows.Forms.Label();
            this.lblPatientState = new System.Windows.Forms.Label();
            this.lblPatientCity = new System.Windows.Forms.Label();
            this.lblInsuredsAddress = new System.Windows.Forms.Label();
            this.lblRelationship_Other = new System.Windows.Forms.Label();
            this.lblRelationship_Child = new System.Windows.Forms.Label();
            this.lblRelationship_Spouse = new System.Windows.Forms.Label();
            this.lblRelationship_Self = new System.Windows.Forms.Label();
            this.lblPatientAddress = new System.Windows.Forms.Label();
            this.lblInsuredName = new System.Windows.Forms.Label();
            this.lblPatient_Female = new System.Windows.Forms.Label();
            this.lblPatient_Male = new System.Windows.Forms.Label();
            this.lblPatientDOB_YY = new System.Windows.Forms.Label();
            this.lblPatientDOB_DD = new System.Windows.Forms.Label();
            this.lblPatientDOB_MM = new System.Windows.Forms.Label();
            this.lblInsuredIdNumber = new System.Windows.Forms.Label();
            this.btnPatientBrowse = new System.Windows.Forms.Button();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblOtherInsuranceType = new System.Windows.Forms.Label();
            this.lblFECABlackLung = new System.Windows.Forms.Label();
            this.lblGroupHealthPlan = new System.Windows.Forms.Label();
            this.lblCHAMPVA = new System.Windows.Forms.Label();
            this.lblTricareChampus = new System.Windows.Forms.Label();
            this.lblMedicaid = new System.Windows.Forms.Label();
            this.lblMedicare = new System.Windows.Forms.Label();
            this.lblPayerNameAndAddress = new System.Windows.Forms.Label();
            this.pnlTextBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMainForm.SuspendLayout();
            this.pnlLabel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlMainFormLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTransactionID
            // 
            this.txtTransactionID.BackColor = System.Drawing.Color.White;
            this.txtTransactionID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionID.Enabled = false;
            this.txtTransactionID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionID.Location = new System.Drawing.Point(0, 0);
            this.txtTransactionID.MaxLength = 7;
            this.txtTransactionID.Name = "txtTransactionID";
            this.txtTransactionID.Size = new System.Drawing.Size(97, 22);
            this.txtTransactionID.TabIndex = 214;
            // 
            // txtFacilityCode
            // 
            this.txtFacilityCode.BackColor = System.Drawing.Color.White;
            this.txtFacilityCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacilityCode.Enabled = false;
            this.txtFacilityCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityCode.Location = new System.Drawing.Point(0, 0);
            this.txtFacilityCode.MaxLength = 7;
            this.txtFacilityCode.Name = "txtFacilityCode";
            this.txtFacilityCode.Size = new System.Drawing.Size(97, 22);
            this.txtFacilityCode.TabIndex = 211;
            this.txtFacilityCode.Visible = false;
            // 
            // txtFacilityDescription
            // 
            this.txtFacilityDescription.BackColor = System.Drawing.Color.White;
            this.txtFacilityDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacilityDescription.Enabled = false;
            this.txtFacilityDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityDescription.Location = new System.Drawing.Point(0, 0);
            this.txtFacilityDescription.MaxLength = 7;
            this.txtFacilityDescription.Name = "txtFacilityDescription";
            this.txtFacilityDescription.Size = new System.Drawing.Size(97, 22);
            this.txtFacilityDescription.TabIndex = 215;
            // 
            // printdoc_HCFA1500
            // 
            this.printdoc_HCFA1500.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printdoc_HCFA1500_BeginPrint);
            this.printdoc_HCFA1500.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printdoc_HCFA1500_EndPrint);
            this.printdoc_HCFA1500.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printdoc_HCFA1500_PrintPage);
            this.printdoc_HCFA1500.QueryPageSettings += new System.Drawing.Printing.QueryPageSettingsEventHandler(this.printdoc_HCFA1500_QueryPageSettings);
            // 
            // pnlTextBox
            // 
            this.pnlTextBox.Controls.Add(this.panel1);
            this.pnlTextBox.Location = new System.Drawing.Point(0, 0);
            this.pnlTextBox.Name = "pnlTextBox";
            this.pnlTextBox.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTextBox.Size = new System.Drawing.Size(927, 1153);
            this.pnlTextBox.TabIndex = 216;
            this.pnlTextBox.Visible = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.pnlMainForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 1147);
            this.panel1.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(0, 1162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(900, 1);
            this.label9.TabIndex = 12;
            this.label9.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(900, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 1163);
            this.label11.TabIndex = 10;
            this.label11.Text = "label3";
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMainForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMainForm.BackgroundImage")));
            this.pnlMainForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlMainForm.Controls.Add(this.txtSimilarIllnessFirstDateQualifier);
            this.pnlMainForm.Controls.Add(this.txtDateOfCurrentIllnessQualifier);
            this.pnlMainForm.Controls.Add(this.txtReferringProviderQualifier);
            this.pnlMainForm.Controls.Add(this.txtIcdInd2);
            this.pnlMainForm.Controls.Add(this.txtIcdInd1);
            this.pnlMainForm.Controls.Add(this.txtOtherClaimIDQualifier);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeL);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeK);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeH);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeJ);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeI);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeG);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeF);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCodeE);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded6);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded5);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded4);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded3);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded2);
            this.pnlMainForm.Controls.Add(this.txtEPSDTShaded1);
            this.pnlMainForm.Controls.Add(this.txtPhyscianQualifierValue);
            this.pnlMainForm.Controls.Add(this.txtReferringProvider_OtherType);
            this.pnlMainForm.Controls.Add(this.txtReferringProvider_OtherValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider1_Qualifier);
            this.pnlMainForm.Controls.Add(this.txtFacilityInfo);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider1_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider6_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider6_Qualifier);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider5_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider5_Qualifier);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider4_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider4_Qualifier);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider3_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider3_Qualifier);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider2_QualifierValue);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider2_Qualifier);
            this.pnlMainForm.Controls.Add(this.btnBrowseReferral);
            this.pnlMainForm.Controls.Add(this.btnBrowseFacility);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider1_NPI);
            this.pnlMainForm.Controls.Add(this.btn_PatientBrowse);
            this.pnlMainForm.Controls.Add(this.btn_BrowsePatientRegt);
            this.pnlMainForm.Controls.Add(this.txtNotes6);
            this.pnlMainForm.Controls.Add(this.txtNotes5);
            this.pnlMainForm.Controls.Add(this.txtNotes4);
            this.pnlMainForm.Controls.Add(this.txtNotes3);
            this.pnlMainForm.Controls.Add(this.txtNotes2);
            this.pnlMainForm.Controls.Add(this.txtNotes1);
            this.pnlMainForm.Controls.Add(this.txtPayerNameAndAddress);
            this.pnlMainForm.Controls.Add(this.dtpPatientSignDate);
            this.pnlMainForm.Controls.Add(this.dtpDOS6To);
            this.pnlMainForm.Controls.Add(this.dtpDOS6From);
            this.pnlMainForm.Controls.Add(this.dtpDOS5To);
            this.pnlMainForm.Controls.Add(this.dtpDOS5From);
            this.pnlMainForm.Controls.Add(this.dtpDOS4To);
            this.pnlMainForm.Controls.Add(this.dtpDOS4From);
            this.pnlMainForm.Controls.Add(this.dtpDOS3To);
            this.pnlMainForm.Controls.Add(this.dtpDOS3From);
            this.pnlMainForm.Controls.Add(this.dtpDOS2To);
            this.pnlMainForm.Controls.Add(this.dtpDOS2From);
            this.pnlMainForm.Controls.Add(this.dtpDOS1To);
            this.pnlMainForm.Controls.Add(this.dtpDOS1From);
            this.pnlMainForm.Controls.Add(this.dtpHospitalisationTo);
            this.pnlMainForm.Controls.Add(this.dtpHospitalisationFrom);
            this.pnlMainForm.Controls.Add(this.dtpUnableToWorkTill);
            this.pnlMainForm.Controls.Add(this.dtpUnableToWorkFrom);
            this.pnlMainForm.Controls.Add(this.dtpInsuredsDOB);
            this.pnlMainForm.Controls.Add(this.dtpPhysicianSignDate);
            this.pnlMainForm.Controls.Add(this.dtpSimilarIllnessFirstDate);
            this.pnlMainForm.Controls.Add(this.dtpDateOfCurrentIllness);
            this.pnlMainForm.Controls.Add(this.dtpPatientDOB);
            this.pnlMainForm.Controls.Add(this.txtPatientZip);
            this.pnlMainForm.Controls.Add(this.txtInsuredsZip);
            this.pnlMainForm.Controls.Add(this.txtInsuredTelephone2);
            this.pnlMainForm.Controls.Add(this.txtPatientTelephone2);
            this.pnlMainForm.Controls.Add(this.txtInsuredTelephone1);
            this.pnlMainForm.Controls.Add(this.txtPatientTelephone1);
            this.pnlMainForm.Controls.Add(this.txtPatientState);
            this.pnlMainForm.Controls.Add(this.txtInsuredsState);
            this.pnlMainForm.Controls.Add(this.txtPatientCity);
            this.pnlMainForm.Controls.Add(this.txtInsuredsCity);
            this.pnlMainForm.Controls.Add(this.txtPatientCoditionRelatedTo_State);
            this.pnlMainForm.Controls.Add(this.txtReserveForLocalUse);
            this.pnlMainForm.Controls.Add(this.txtPatientCondition);
            this.pnlMainForm.Controls.Add(this.txtInsuredPolicyorFECANo);
            this.pnlMainForm.Controls.Add(this.txtInsuredInsurancePlanName);
            this.pnlMainForm.Controls.Add(this.txtInsuredEmployerORSchoolName);
            this.pnlMainForm.Controls.Add(this.txtOutsideLabCharges2);
            this.pnlMainForm.Controls.Add(this.txtPriorAuthorizationNumber);
            this.pnlMainForm.Controls.Add(this.txtOriginalRefNumber);
            this.pnlMainForm.Controls.Add(this.txtMedicaidResubmissionCode);
            this.pnlMainForm.Controls.Add(this.txtOutsideLabCharges1);
            this.pnlMainForm.Controls.Add(this.txtReferringProvider_NPI);
            this.pnlMainForm.Controls.Add(this.txtBillingProv_b_UPIN);
            this.pnlMainForm.Controls.Add(this.txtBillingProv_a_NPI);
            this.pnlMainForm.Controls.Add(this.txtFacility_b);
            this.pnlMainForm.Controls.Add(this.txtFacility_a_NPI);
            this.pnlMainForm.Controls.Add(this.txtCPT6);
            this.pnlMainForm.Controls.Add(this.txtCPT5);
            this.pnlMainForm.Controls.Add(this.txtCPT4);
            this.pnlMainForm.Controls.Add(this.txtCPT3);
            this.pnlMainForm.Controls.Add(this.txtEMG6);
            this.pnlMainForm.Controls.Add(this.txtCPT2);
            this.pnlMainForm.Controls.Add(this.txtEMG5);
            this.pnlMainForm.Controls.Add(this.txtCPT1);
            this.pnlMainForm.Controls.Add(this.txtEMG4);
            this.pnlMainForm.Controls.Add(this.txtEMG3);
            this.pnlMainForm.Controls.Add(this.txtEMG2);
            this.pnlMainForm.Controls.Add(this.txtEMG1);
            this.pnlMainForm.Controls.Add(this.txtEPSDT6);
            this.pnlMainForm.Controls.Add(this.txtEPSDT5);
            this.pnlMainForm.Controls.Add(this.txtEPSDT4);
            this.pnlMainForm.Controls.Add(this.txtEPSDT3);
            this.pnlMainForm.Controls.Add(this.txtBillingProviderPhone1);
            this.pnlMainForm.Controls.Add(this.txtAmountPaid2);
            this.pnlMainForm.Controls.Add(this.txtTotalCharges2);
            this.pnlMainForm.Controls.Add(this.txtCharges61);
            this.pnlMainForm.Controls.Add(this.txtEPSDT2);
            this.pnlMainForm.Controls.Add(this.txtCharges51);
            this.pnlMainForm.Controls.Add(this.txtEPSDT1);
            this.pnlMainForm.Controls.Add(this.txtCharges41);
            this.pnlMainForm.Controls.Add(this.txtCharges31);
            this.pnlMainForm.Controls.Add(this.txtAmountPaid);
            this.pnlMainForm.Controls.Add(this.txtTotalCharges);
            this.pnlMainForm.Controls.Add(this.txtCharges6);
            this.pnlMainForm.Controls.Add(this.txtCharges21);
            this.pnlMainForm.Controls.Add(this.txtCharges5);
            this.pnlMainForm.Controls.Add(this.txtCharges11);
            this.pnlMainForm.Controls.Add(this.txtCharges4);
            this.pnlMainForm.Controls.Add(this.txtCharges3);
            this.pnlMainForm.Controls.Add(this.txtBillingProviderPhone2);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider6_NPI);
            this.pnlMainForm.Controls.Add(this.txtCharges2);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider5_NPI);
            this.pnlMainForm.Controls.Add(this.txtCharges1);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider4_NPI);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider3_NPI);
            this.pnlMainForm.Controls.Add(this.txtUnits6);
            this.pnlMainForm.Controls.Add(this.txtRenderingProvider2_NPI);
            this.pnlMainForm.Controls.Add(this.txtUnits5);
            this.pnlMainForm.Controls.Add(this.txtUnits4);
            this.pnlMainForm.Controls.Add(this.txtUnits3);
            this.pnlMainForm.Controls.Add(this.txtDxPtr6);
            this.pnlMainForm.Controls.Add(this.txtUnits2);
            this.pnlMainForm.Controls.Add(this.txtDxPtr5);
            this.pnlMainForm.Controls.Add(this.txtUnits1);
            this.pnlMainForm.Controls.Add(this.txtDxPtr4);
            this.pnlMainForm.Controls.Add(this.txtMOD64);
            this.pnlMainForm.Controls.Add(this.txtDxPtr3);
            this.pnlMainForm.Controls.Add(this.txtMOD54);
            this.pnlMainForm.Controls.Add(this.txtDxPtr2);
            this.pnlMainForm.Controls.Add(this.txtMOD44);
            this.pnlMainForm.Controls.Add(this.txtMOD34);
            this.pnlMainForm.Controls.Add(this.txtMOD63);
            this.pnlMainForm.Controls.Add(this.txtMOD24);
            this.pnlMainForm.Controls.Add(this.txtMOD53);
            this.pnlMainForm.Controls.Add(this.txtDxPtr1);
            this.pnlMainForm.Controls.Add(this.txtMOD43);
            this.pnlMainForm.Controls.Add(this.txtMOD33);
            this.pnlMainForm.Controls.Add(this.txtMOD62);
            this.pnlMainForm.Controls.Add(this.txtMOD23);
            this.pnlMainForm.Controls.Add(this.txtMOD52);
            this.pnlMainForm.Controls.Add(this.txtMOD14);
            this.pnlMainForm.Controls.Add(this.txtMOD42);
            this.pnlMainForm.Controls.Add(this.txtMOD61);
            this.pnlMainForm.Controls.Add(this.txtMOD32);
            this.pnlMainForm.Controls.Add(this.txtMOD51);
            this.pnlMainForm.Controls.Add(this.txtMOD22);
            this.pnlMainForm.Controls.Add(this.txtMOD41);
            this.pnlMainForm.Controls.Add(this.txtPOS6);
            this.pnlMainForm.Controls.Add(this.txtMOD31);
            this.pnlMainForm.Controls.Add(this.txtPOS5);
            this.pnlMainForm.Controls.Add(this.txtMOD13);
            this.pnlMainForm.Controls.Add(this.txtPOS4);
            this.pnlMainForm.Controls.Add(this.txtMOD21);
            this.pnlMainForm.Controls.Add(this.txtPOS3);
            this.pnlMainForm.Controls.Add(this.txtMOD12);
            this.pnlMainForm.Controls.Add(this.txtPOS2);
            this.pnlMainForm.Controls.Add(this.txtMOD11);
            this.pnlMainForm.Controls.Add(this.txtPOS1);
            this.pnlMainForm.Controls.Add(this.txtPhyscianSignature);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCode41);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCode31);
            this.pnlMainForm.Controls.Add(this.txtBillingProviderInfo);
            this.pnlMainForm.Controls.Add(this.txtInsuredPersonSign);
            this.pnlMainForm.Controls.Add(this.txtPatientSignature);
            this.pnlMainForm.Controls.Add(this.txtPatientAccountNo);
            this.pnlMainForm.Controls.Add(this.txtFederalTaxID);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCode21);
            this.pnlMainForm.Controls.Add(this.txtDiagnosisCode11);
            this.pnlMainForm.Controls.Add(this.txtReferringProviderName);
            this.pnlMainForm.Controls.Add(this.txtOtherInsuredInsuranceName);
            this.pnlMainForm.Controls.Add(this.txt19ReservedForLocalUse);
            this.pnlMainForm.Controls.Add(this.txtOtherInsuredPolicyNo);
            this.pnlMainForm.Controls.Add(this.txtOtherInsuredName);
            this.pnlMainForm.Controls.Add(this.txtPatientAddress);
            this.pnlMainForm.Controls.Add(this.txtInsuredsAddress);
            this.pnlMainForm.Controls.Add(this.txtInsuredName);
            this.pnlMainForm.Controls.Add(this.txtInsuredIdNumber);
            this.pnlMainForm.Controls.Add(this.txtPatientName);
            this.pnlMainForm.Controls.Add(this.chkPatient_Female);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_OtherAccident_No);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_OtherAccident_Yes);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_AutoAccident_No);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_AutoAccident_Yes);
            this.pnlMainForm.Controls.Add(this.chkIsOtherHealthPlan_Yes);
            this.pnlMainForm.Controls.Add(this.chkInsuredSex_Male);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_Employment_Yes);
            this.pnlMainForm.Controls.Add(this.chkOutsideLab_No);
            this.pnlMainForm.Controls.Add(this.chkAcceptAssignment_No);
            this.pnlMainForm.Controls.Add(this.chkAcceptAssignment_Yes);
            this.pnlMainForm.Controls.Add(this.chkFederalTaxID_EIN);
            this.pnlMainForm.Controls.Add(this.chkFederalTaxID_SSN);
            this.pnlMainForm.Controls.Add(this.chkOutsideLab_Yes);
            this.pnlMainForm.Controls.Add(this.chkIsOtherHealthPlan_No);
            this.pnlMainForm.Controls.Add(this.chkInsuredSex_Female);
            this.pnlMainForm.Controls.Add(this.chkPatientCoditionRelatedTo_Employment_No);
            this.pnlMainForm.Controls.Add(this.chkRelationship_Other);
            this.pnlMainForm.Controls.Add(this.chkRelationship_Child);
            this.pnlMainForm.Controls.Add(this.chkRelationship_Spouse);
            this.pnlMainForm.Controls.Add(this.chkRelationship_Self);
            this.pnlMainForm.Controls.Add(this.chkPatient_Male);
            this.pnlMainForm.Controls.Add(this.chkOtherInsuranceType);
            this.pnlMainForm.Controls.Add(this.chkFECABlackLung);
            this.pnlMainForm.Controls.Add(this.chkGroupHealthPlan);
            this.pnlMainForm.Controls.Add(this.chkCHAMPVA);
            this.pnlMainForm.Controls.Add(this.chkTricareChampus);
            this.pnlMainForm.Controls.Add(this.chkMedicaid);
            this.pnlMainForm.Controls.Add(this.chkMedicare);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider1);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider2);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider3);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider4);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider5);
            this.pnlMainForm.Controls.Add(this.cmbRenderingProvider6);
            this.pnlMainForm.Location = new System.Drawing.Point(0, 17);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(900, 1145);
            this.pnlMainForm.TabIndex = 0;
            // 
            // txtSimilarIllnessFirstDateQualifier
            // 
            this.txtSimilarIllnessFirstDateQualifier.BackColor = System.Drawing.Color.Gold;
            this.txtSimilarIllnessFirstDateQualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSimilarIllnessFirstDateQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSimilarIllnessFirstDateQualifier.Location = new System.Drawing.Point(355, 570);
            this.txtSimilarIllnessFirstDateQualifier.Name = "txtSimilarIllnessFirstDateQualifier";
            this.txtSimilarIllnessFirstDateQualifier.ReadOnly = true;
            this.txtSimilarIllnessFirstDateQualifier.Size = new System.Drawing.Size(24, 15);
            this.txtSimilarIllnessFirstDateQualifier.TabIndex = 235;
            this.txtSimilarIllnessFirstDateQualifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDateOfCurrentIllnessQualifier
            // 
            this.txtDateOfCurrentIllnessQualifier.BackColor = System.Drawing.Color.DarkGray;
            this.txtDateOfCurrentIllnessQualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDateOfCurrentIllnessQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateOfCurrentIllnessQualifier.Location = new System.Drawing.Point(190, 573);
            this.txtDateOfCurrentIllnessQualifier.MaxLength = 4;
            this.txtDateOfCurrentIllnessQualifier.Name = "txtDateOfCurrentIllnessQualifier";
            this.txtDateOfCurrentIllnessQualifier.ReadOnly = true;
            this.txtDateOfCurrentIllnessQualifier.Size = new System.Drawing.Size(43, 15);
            this.txtDateOfCurrentIllnessQualifier.TabIndex = 234;
            this.txtDateOfCurrentIllnessQualifier.Text = "14 Qual";
            // 
            // txtReferringProviderQualifier
            // 
            this.txtReferringProviderQualifier.BackColor = System.Drawing.Color.Gold;
            this.txtReferringProviderQualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReferringProviderQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferringProviderQualifier.Location = new System.Drawing.Point(24, 609);
            this.txtReferringProviderQualifier.Name = "txtReferringProviderQualifier";
            this.txtReferringProviderQualifier.ReadOnly = true;
            this.txtReferringProviderQualifier.Size = new System.Drawing.Size(24, 15);
            this.txtReferringProviderQualifier.TabIndex = 233;
            this.txtReferringProviderQualifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIcdInd2
            // 
            this.txtIcdInd2.BackColor = System.Drawing.Color.DarkGray;
            this.txtIcdInd2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIcdInd2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcdInd2.Location = new System.Drawing.Point(493, 666);
            this.txtIcdInd2.MaxLength = 4;
            this.txtIcdInd2.Name = "txtIcdInd2";
            this.txtIcdInd2.ReadOnly = true;
            this.txtIcdInd2.Size = new System.Drawing.Size(43, 15);
            this.txtIcdInd2.TabIndex = 232;
            this.txtIcdInd2.Text = "ICD2";
            // 
            // txtIcdInd1
            // 
            this.txtIcdInd1.BackColor = System.Drawing.Color.Gold;
            this.txtIcdInd1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIcdInd1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIcdInd1.Location = new System.Drawing.Point(470, 666);
            this.txtIcdInd1.Name = "txtIcdInd1";
            this.txtIcdInd1.ReadOnly = true;
            this.txtIcdInd1.Size = new System.Drawing.Size(21, 15);
            this.txtIcdInd1.TabIndex = 231;
            this.txtIcdInd1.Text = "IC1";
            this.txtIcdInd1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOtherClaimIDQualifier
            // 
            this.txtOtherClaimIDQualifier.BackColor = System.Drawing.Color.Gold;
            this.txtOtherClaimIDQualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherClaimIDQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherClaimIDQualifier.Location = new System.Drawing.Point(556, 393);
            this.txtOtherClaimIDQualifier.Name = "txtOtherClaimIDQualifier";
            this.txtOtherClaimIDQualifier.ReadOnly = true;
            this.txtOtherClaimIDQualifier.Size = new System.Drawing.Size(24, 15);
            this.txtOtherClaimIDQualifier.TabIndex = 230;
            this.txtOtherClaimIDQualifier.Text = "11b";
            this.txtOtherClaimIDQualifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDiagnosisCodeL
            // 
            this.txtDiagnosisCodeL.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeL.Location = new System.Drawing.Point(472, 719);
            this.txtDiagnosisCodeL.MaxLength = 4;
            this.txtDiagnosisCodeL.Name = "txtDiagnosisCodeL";
            this.txtDiagnosisCodeL.ReadOnly = true;
            this.txtDiagnosisCodeL.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeL.TabIndex = 229;
            // 
            // txtDiagnosisCodeK
            // 
            this.txtDiagnosisCodeK.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeK.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeK.Location = new System.Drawing.Point(333, 719);
            this.txtDiagnosisCodeK.MaxLength = 4;
            this.txtDiagnosisCodeK.Name = "txtDiagnosisCodeK";
            this.txtDiagnosisCodeK.ReadOnly = true;
            this.txtDiagnosisCodeK.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeK.TabIndex = 228;
            // 
            // txtDiagnosisCodeH
            // 
            this.txtDiagnosisCodeH.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeH.Location = new System.Drawing.Point(472, 700);
            this.txtDiagnosisCodeH.MaxLength = 4;
            this.txtDiagnosisCodeH.Name = "txtDiagnosisCodeH";
            this.txtDiagnosisCodeH.ReadOnly = true;
            this.txtDiagnosisCodeH.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeH.TabIndex = 225;
            // 
            // txtDiagnosisCodeJ
            // 
            this.txtDiagnosisCodeJ.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeJ.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeJ.Location = new System.Drawing.Point(192, 719);
            this.txtDiagnosisCodeJ.MaxLength = 4;
            this.txtDiagnosisCodeJ.Name = "txtDiagnosisCodeJ";
            this.txtDiagnosisCodeJ.ReadOnly = true;
            this.txtDiagnosisCodeJ.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeJ.TabIndex = 227;
            // 
            // txtDiagnosisCodeI
            // 
            this.txtDiagnosisCodeI.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeI.Location = new System.Drawing.Point(52, 719);
            this.txtDiagnosisCodeI.MaxLength = 4;
            this.txtDiagnosisCodeI.Name = "txtDiagnosisCodeI";
            this.txtDiagnosisCodeI.ReadOnly = true;
            this.txtDiagnosisCodeI.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeI.TabIndex = 226;
            // 
            // txtDiagnosisCodeG
            // 
            this.txtDiagnosisCodeG.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeG.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeG.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeG.Location = new System.Drawing.Point(333, 700);
            this.txtDiagnosisCodeG.MaxLength = 4;
            this.txtDiagnosisCodeG.Name = "txtDiagnosisCodeG";
            this.txtDiagnosisCodeG.ReadOnly = true;
            this.txtDiagnosisCodeG.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeG.TabIndex = 224;
            // 
            // txtDiagnosisCodeF
            // 
            this.txtDiagnosisCodeF.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeF.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeF.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeF.Location = new System.Drawing.Point(192, 700);
            this.txtDiagnosisCodeF.MaxLength = 4;
            this.txtDiagnosisCodeF.Name = "txtDiagnosisCodeF";
            this.txtDiagnosisCodeF.ReadOnly = true;
            this.txtDiagnosisCodeF.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeF.TabIndex = 223;
            // 
            // txtDiagnosisCodeE
            // 
            this.txtDiagnosisCodeE.BackColor = System.Drawing.Color.DarkGray;
            this.txtDiagnosisCodeE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCodeE.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCodeE.Location = new System.Drawing.Point(52, 700);
            this.txtDiagnosisCodeE.MaxLength = 4;
            this.txtDiagnosisCodeE.Name = "txtDiagnosisCodeE";
            this.txtDiagnosisCodeE.ReadOnly = true;
            this.txtDiagnosisCodeE.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCodeE.TabIndex = 222;
            // 
            // txtEPSDTShaded6
            // 
            this.txtEPSDTShaded6.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded6.Location = new System.Drawing.Point(694, 953);
            this.txtEPSDTShaded6.MaxLength = 2;
            this.txtEPSDTShaded6.Name = "txtEPSDTShaded6";
            this.txtEPSDTShaded6.ReadOnly = true;
            this.txtEPSDTShaded6.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDTShaded6.TabIndex = 221;
            // 
            // txtEPSDTShaded5
            // 
            this.txtEPSDTShaded5.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded5.Location = new System.Drawing.Point(694, 918);
            this.txtEPSDTShaded5.MaxLength = 2;
            this.txtEPSDTShaded5.Name = "txtEPSDTShaded5";
            this.txtEPSDTShaded5.ReadOnly = true;
            this.txtEPSDTShaded5.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDTShaded5.TabIndex = 220;
            // 
            // txtEPSDTShaded4
            // 
            this.txtEPSDTShaded4.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded4.Location = new System.Drawing.Point(694, 881);
            this.txtEPSDTShaded4.MaxLength = 2;
            this.txtEPSDTShaded4.Name = "txtEPSDTShaded4";
            this.txtEPSDTShaded4.ReadOnly = true;
            this.txtEPSDTShaded4.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDTShaded4.TabIndex = 219;
            // 
            // txtEPSDTShaded3
            // 
            this.txtEPSDTShaded3.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded3.Location = new System.Drawing.Point(694, 844);
            this.txtEPSDTShaded3.MaxLength = 2;
            this.txtEPSDTShaded3.Name = "txtEPSDTShaded3";
            this.txtEPSDTShaded3.ReadOnly = true;
            this.txtEPSDTShaded3.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDTShaded3.TabIndex = 218;
            // 
            // txtEPSDTShaded2
            // 
            this.txtEPSDTShaded2.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded2.Location = new System.Drawing.Point(694, 808);
            this.txtEPSDTShaded2.MaxLength = 2;
            this.txtEPSDTShaded2.Name = "txtEPSDTShaded2";
            this.txtEPSDTShaded2.ReadOnly = true;
            this.txtEPSDTShaded2.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDTShaded2.TabIndex = 217;
            // 
            // txtEPSDTShaded1
            // 
            this.txtEPSDTShaded1.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDTShaded1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDTShaded1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDTShaded1.Location = new System.Drawing.Point(694, 772);
            this.txtEPSDTShaded1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtEPSDTShaded1.MaxLength = 2;
            this.txtEPSDTShaded1.Name = "txtEPSDTShaded1";
            this.txtEPSDTShaded1.ReadOnly = true;
            this.txtEPSDTShaded1.Size = new System.Drawing.Size(18, 13);
            this.txtEPSDTShaded1.TabIndex = 216;
            // 
            // txtPhyscianQualifierValue
            // 
            this.txtPhyscianQualifierValue.BackColor = System.Drawing.Color.Ivory;
            this.txtPhyscianQualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhyscianQualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhyscianQualifierValue.Location = new System.Drawing.Point(65, 1095);
            this.txtPhyscianQualifierValue.Multiline = true;
            this.txtPhyscianQualifierValue.Name = "txtPhyscianQualifierValue";
            this.txtPhyscianQualifierValue.ReadOnly = true;
            this.txtPhyscianQualifierValue.Size = new System.Drawing.Size(88, 20);
            this.txtPhyscianQualifierValue.TabIndex = 215;
            // 
            // txtReferringProvider_OtherType
            // 
            this.txtReferringProvider_OtherType.BackColor = System.Drawing.Color.Ivory;
            this.txtReferringProvider_OtherType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReferringProvider_OtherType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferringProvider_OtherType.Location = new System.Drawing.Point(338, 591);
            this.txtReferringProvider_OtherType.MaxLength = 10;
            this.txtReferringProvider_OtherType.Name = "txtReferringProvider_OtherType";
            this.txtReferringProvider_OtherType.ReadOnly = true;
            this.txtReferringProvider_OtherType.Size = new System.Drawing.Size(22, 15);
            this.txtReferringProvider_OtherType.TabIndex = 214;
            // 
            // txtReferringProvider_OtherValue
            // 
            this.txtReferringProvider_OtherValue.BackColor = System.Drawing.Color.Ivory;
            this.txtReferringProvider_OtherValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReferringProvider_OtherValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferringProvider_OtherValue.Location = new System.Drawing.Point(365, 591);
            this.txtReferringProvider_OtherValue.MaxLength = 10;
            this.txtReferringProvider_OtherValue.Name = "txtReferringProvider_OtherValue";
            this.txtReferringProvider_OtherValue.ReadOnly = true;
            this.txtReferringProvider_OtherValue.Size = new System.Drawing.Size(184, 15);
            this.txtReferringProvider_OtherValue.TabIndex = 213;
            // 
            // txtRenderingProvider1_Qualifier
            // 
            this.txtRenderingProvider1_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider1_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider1_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider1_Qualifier.ForeColor = System.Drawing.Color.Black;
            this.txtRenderingProvider1_Qualifier.Location = new System.Drawing.Point(716, 771);
            this.txtRenderingProvider1_Qualifier.MaxLength = 2;
            this.txtRenderingProvider1_Qualifier.Name = "txtRenderingProvider1_Qualifier";
            this.txtRenderingProvider1_Qualifier.ReadOnly = true;
            this.txtRenderingProvider1_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider1_Qualifier.TabIndex = 208;
            // 
            // txtFacilityInfo
            // 
            this.txtFacilityInfo.BackColor = System.Drawing.Color.Ivory;
            this.txtFacilityInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFacilityInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityInfo.Location = new System.Drawing.Point(261, 1045);
            this.txtFacilityInfo.Multiline = true;
            this.txtFacilityInfo.Name = "txtFacilityInfo";
            this.txtFacilityInfo.ReadOnly = true;
            this.txtFacilityInfo.Size = new System.Drawing.Size(314, 32);
            this.txtFacilityInfo.TabIndex = 212;
            // 
            // txtRenderingProvider1_QualifierValue
            // 
            this.txtRenderingProvider1_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider1_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider1_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider1_QualifierValue.ForeColor = System.Drawing.Color.Black;
            this.txtRenderingProvider1_QualifierValue.Location = new System.Drawing.Point(749, 771);
            this.txtRenderingProvider1_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider1_QualifierValue.Multiline = true;
            this.txtRenderingProvider1_QualifierValue.Name = "txtRenderingProvider1_QualifierValue";
            this.txtRenderingProvider1_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider1_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider1_QualifierValue.TabIndex = 209;
            // 
            // txtRenderingProvider6_QualifierValue
            // 
            this.txtRenderingProvider6_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider6_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider6_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider6_QualifierValue.Location = new System.Drawing.Point(749, 953);
            this.txtRenderingProvider6_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider6_QualifierValue.Multiline = true;
            this.txtRenderingProvider6_QualifierValue.Name = "txtRenderingProvider6_QualifierValue";
            this.txtRenderingProvider6_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider6_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider6_QualifierValue.TabIndex = 211;
            // 
            // txtRenderingProvider6_Qualifier
            // 
            this.txtRenderingProvider6_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider6_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider6_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider6_Qualifier.Location = new System.Drawing.Point(716, 953);
            this.txtRenderingProvider6_Qualifier.MaxLength = 2;
            this.txtRenderingProvider6_Qualifier.Name = "txtRenderingProvider6_Qualifier";
            this.txtRenderingProvider6_Qualifier.ReadOnly = true;
            this.txtRenderingProvider6_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider6_Qualifier.TabIndex = 210;
            // 
            // txtRenderingProvider5_QualifierValue
            // 
            this.txtRenderingProvider5_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider5_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider5_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider5_QualifierValue.Location = new System.Drawing.Point(749, 919);
            this.txtRenderingProvider5_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider5_QualifierValue.Multiline = true;
            this.txtRenderingProvider5_QualifierValue.Name = "txtRenderingProvider5_QualifierValue";
            this.txtRenderingProvider5_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider5_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider5_QualifierValue.TabIndex = 211;
            // 
            // txtRenderingProvider5_Qualifier
            // 
            this.txtRenderingProvider5_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider5_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider5_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider5_Qualifier.Location = new System.Drawing.Point(716, 919);
            this.txtRenderingProvider5_Qualifier.MaxLength = 2;
            this.txtRenderingProvider5_Qualifier.Name = "txtRenderingProvider5_Qualifier";
            this.txtRenderingProvider5_Qualifier.ReadOnly = true;
            this.txtRenderingProvider5_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider5_Qualifier.TabIndex = 210;
            // 
            // txtRenderingProvider4_QualifierValue
            // 
            this.txtRenderingProvider4_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider4_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider4_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider4_QualifierValue.Location = new System.Drawing.Point(749, 881);
            this.txtRenderingProvider4_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider4_QualifierValue.Multiline = true;
            this.txtRenderingProvider4_QualifierValue.Name = "txtRenderingProvider4_QualifierValue";
            this.txtRenderingProvider4_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider4_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider4_QualifierValue.TabIndex = 211;
            // 
            // txtRenderingProvider4_Qualifier
            // 
            this.txtRenderingProvider4_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider4_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider4_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider4_Qualifier.Location = new System.Drawing.Point(716, 881);
            this.txtRenderingProvider4_Qualifier.MaxLength = 2;
            this.txtRenderingProvider4_Qualifier.Name = "txtRenderingProvider4_Qualifier";
            this.txtRenderingProvider4_Qualifier.ReadOnly = true;
            this.txtRenderingProvider4_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider4_Qualifier.TabIndex = 210;
            // 
            // txtRenderingProvider3_QualifierValue
            // 
            this.txtRenderingProvider3_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider3_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider3_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider3_QualifierValue.Location = new System.Drawing.Point(749, 844);
            this.txtRenderingProvider3_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider3_QualifierValue.Multiline = true;
            this.txtRenderingProvider3_QualifierValue.Name = "txtRenderingProvider3_QualifierValue";
            this.txtRenderingProvider3_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider3_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider3_QualifierValue.TabIndex = 211;
            // 
            // txtRenderingProvider3_Qualifier
            // 
            this.txtRenderingProvider3_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider3_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider3_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider3_Qualifier.Location = new System.Drawing.Point(716, 844);
            this.txtRenderingProvider3_Qualifier.MaxLength = 2;
            this.txtRenderingProvider3_Qualifier.Name = "txtRenderingProvider3_Qualifier";
            this.txtRenderingProvider3_Qualifier.ReadOnly = true;
            this.txtRenderingProvider3_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider3_Qualifier.TabIndex = 210;
            // 
            // txtRenderingProvider2_QualifierValue
            // 
            this.txtRenderingProvider2_QualifierValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider2_QualifierValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider2_QualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider2_QualifierValue.Location = new System.Drawing.Point(749, 808);
            this.txtRenderingProvider2_QualifierValue.MaxLength = 10;
            this.txtRenderingProvider2_QualifierValue.Multiline = true;
            this.txtRenderingProvider2_QualifierValue.Name = "txtRenderingProvider2_QualifierValue";
            this.txtRenderingProvider2_QualifierValue.ReadOnly = true;
            this.txtRenderingProvider2_QualifierValue.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider2_QualifierValue.TabIndex = 211;
            // 
            // txtRenderingProvider2_Qualifier
            // 
            this.txtRenderingProvider2_Qualifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.txtRenderingProvider2_Qualifier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider2_Qualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider2_Qualifier.Location = new System.Drawing.Point(716, 808);
            this.txtRenderingProvider2_Qualifier.MaxLength = 2;
            this.txtRenderingProvider2_Qualifier.Name = "txtRenderingProvider2_Qualifier";
            this.txtRenderingProvider2_Qualifier.ReadOnly = true;
            this.txtRenderingProvider2_Qualifier.Size = new System.Drawing.Size(28, 15);
            this.txtRenderingProvider2_Qualifier.TabIndex = 210;
            // 
            // btnBrowseReferral
            // 
            this.btnBrowseReferral.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseReferral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseReferral.BackgroundImage")));
            this.btnBrowseReferral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseReferral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseReferral.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseReferral.Image")));
            this.btnBrowseReferral.Location = new System.Drawing.Point(288, 604);
            this.btnBrowseReferral.Name = "btnBrowseReferral";
            this.btnBrowseReferral.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseReferral.TabIndex = 207;
            this.btnBrowseReferral.UseVisualStyleBackColor = false;
            // 
            // btnBrowseFacility
            // 
            this.btnBrowseFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.BackgroundImage")));
            this.btnBrowseFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.Image")));
            this.btnBrowseFacility.Location = new System.Drawing.Point(530, 1097);
            this.btnBrowseFacility.Name = "btnBrowseFacility";
            this.btnBrowseFacility.Size = new System.Drawing.Size(22, 18);
            this.btnBrowseFacility.TabIndex = 206;
            this.btnBrowseFacility.UseVisualStyleBackColor = false;
            // 
            // txtRenderingProvider1_NPI
            // 
            this.txtRenderingProvider1_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider1_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider1_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider1_NPI.Location = new System.Drawing.Point(749, 789);
            this.txtRenderingProvider1_NPI.MaxLength = 10;
            this.txtRenderingProvider1_NPI.Multiline = true;
            this.txtRenderingProvider1_NPI.Name = "txtRenderingProvider1_NPI";
            this.txtRenderingProvider1_NPI.ReadOnly = true;
            this.txtRenderingProvider1_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider1_NPI.TabIndex = 102;
            // 
            // btn_PatientBrowse
            // 
            this.btn_PatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btn_PatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btn_PatientBrowse.Image")));
            this.btn_PatientBrowse.Location = new System.Drawing.Point(304, 168);
            this.btn_PatientBrowse.Name = "btn_PatientBrowse";
            this.btn_PatientBrowse.Size = new System.Drawing.Size(22, 22);
            this.btn_PatientBrowse.TabIndex = 9;
            this.btn_PatientBrowse.UseVisualStyleBackColor = false;
            // 
            // btn_BrowsePatientRegt
            // 
            this.btn_BrowsePatientRegt.BackColor = System.Drawing.Color.Transparent;
            this.btn_BrowsePatientRegt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowsePatientRegt.BackgroundImage")));
            this.btn_BrowsePatientRegt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowsePatientRegt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowsePatientRegt.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowsePatientRegt.Image")));
            this.btn_BrowsePatientRegt.Location = new System.Drawing.Point(855, 420);
            this.btn_BrowsePatientRegt.Name = "btn_BrowsePatientRegt";
            this.btn_BrowsePatientRegt.Size = new System.Drawing.Size(22, 22);
            this.btn_BrowsePatientRegt.TabIndex = 56;
            this.btn_BrowsePatientRegt.UseVisualStyleBackColor = false;
            // 
            // txtNotes6
            // 
            this.txtNotes6.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes6.Location = new System.Drawing.Point(31, 954);
            this.txtNotes6.MaxLength = 150;
            this.txtNotes6.Multiline = true;
            this.txtNotes6.Name = "txtNotes6";
            this.txtNotes6.Size = new System.Drawing.Size(658, 13);
            this.txtNotes6.TabIndex = 205;
            // 
            // txtNotes5
            // 
            this.txtNotes5.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes5.Location = new System.Drawing.Point(31, 917);
            this.txtNotes5.MaxLength = 150;
            this.txtNotes5.Multiline = true;
            this.txtNotes5.Name = "txtNotes5";
            this.txtNotes5.Size = new System.Drawing.Size(658, 13);
            this.txtNotes5.TabIndex = 204;
            // 
            // txtNotes4
            // 
            this.txtNotes4.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes4.Location = new System.Drawing.Point(31, 881);
            this.txtNotes4.MaxLength = 150;
            this.txtNotes4.Multiline = true;
            this.txtNotes4.Name = "txtNotes4";
            this.txtNotes4.Size = new System.Drawing.Size(658, 13);
            this.txtNotes4.TabIndex = 203;
            // 
            // txtNotes3
            // 
            this.txtNotes3.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes3.Location = new System.Drawing.Point(31, 844);
            this.txtNotes3.MaxLength = 150;
            this.txtNotes3.Multiline = true;
            this.txtNotes3.Name = "txtNotes3";
            this.txtNotes3.Size = new System.Drawing.Size(658, 13);
            this.txtNotes3.TabIndex = 202;
            // 
            // txtNotes2
            // 
            this.txtNotes2.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes2.Location = new System.Drawing.Point(31, 807);
            this.txtNotes2.MaxLength = 150;
            this.txtNotes2.Multiline = true;
            this.txtNotes2.Name = "txtNotes2";
            this.txtNotes2.Size = new System.Drawing.Size(660, 13);
            this.txtNotes2.TabIndex = 201;
            // 
            // txtNotes1
            // 
            this.txtNotes1.BackColor = System.Drawing.Color.Ivory;
            this.txtNotes1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes1.Location = new System.Drawing.Point(31, 770);
            this.txtNotes1.MaxLength = 150;
            this.txtNotes1.Multiline = true;
            this.txtNotes1.Name = "txtNotes1";
            this.txtNotes1.Size = new System.Drawing.Size(660, 13);
            this.txtNotes1.TabIndex = 200;
            // 
            // txtPayerNameAndAddress
            // 
            this.txtPayerNameAndAddress.BackColor = System.Drawing.Color.Ivory;
            this.txtPayerNameAndAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPayerNameAndAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerNameAndAddress.Location = new System.Drawing.Point(453, 16);
            this.txtPayerNameAndAddress.Multiline = true;
            this.txtPayerNameAndAddress.Name = "txtPayerNameAndAddress";
            this.txtPayerNameAndAddress.ReadOnly = true;
            this.txtPayerNameAndAddress.Size = new System.Drawing.Size(301, 92);
            this.txtPayerNameAndAddress.TabIndex = 0;
            // 
            // dtpPatientSignDate
            // 
            this.dtpPatientSignDate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPatientSignDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpPatientSignDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpPatientSignDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpPatientSignDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpPatientSignDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpPatientSignDate.Checked = false;
            this.dtpPatientSignDate.CustomFormat = "MM/dd/yy";
            this.dtpPatientSignDate.Enabled = false;
            this.dtpPatientSignDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPatientSignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPatientSignDate.Location = new System.Drawing.Point(407, 525);
            this.dtpPatientSignDate.Name = "dtpPatientSignDate";
            this.dtpPatientSignDate.ShowCheckBox = true;
            this.dtpPatientSignDate.Size = new System.Drawing.Size(98, 22);
            this.dtpPatientSignDate.TabIndex = 62;
            // 
            // dtpDOS6To
            // 
            this.dtpDOS6To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS6To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS6To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS6To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS6To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS6To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS6To.CustomFormat = "MM/dd/yy";
            this.dtpDOS6To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS6To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS6To.Location = new System.Drawing.Point(125, 966);
            this.dtpDOS6To.Name = "dtpDOS6To";
            this.dtpDOS6To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS6To.TabIndex = 164;
            // 
            // dtpDOS6From
            // 
            this.dtpDOS6From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS6From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS6From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS6From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS6From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS6From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS6From.CustomFormat = "MM/dd/yy";
            this.dtpDOS6From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS6From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS6From.Location = new System.Drawing.Point(31, 966);
            this.dtpDOS6From.Name = "dtpDOS6From";
            this.dtpDOS6From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS6From.TabIndex = 163;
            // 
            // dtpDOS5To
            // 
            this.dtpDOS5To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS5To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS5To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS5To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS5To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS5To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS5To.CustomFormat = "MM/dd/yy";
            this.dtpDOS5To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS5To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS5To.Location = new System.Drawing.Point(125, 930);
            this.dtpDOS5To.Name = "dtpDOS5To";
            this.dtpDOS5To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS5To.TabIndex = 149;
            // 
            // dtpDOS5From
            // 
            this.dtpDOS5From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS5From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS5From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS5From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS5From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS5From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS5From.CustomFormat = "MM/dd/yy";
            this.dtpDOS5From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS5From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS5From.Location = new System.Drawing.Point(31, 930);
            this.dtpDOS5From.Name = "dtpDOS5From";
            this.dtpDOS5From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS5From.TabIndex = 148;
            // 
            // dtpDOS4To
            // 
            this.dtpDOS4To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS4To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS4To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS4To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS4To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS4To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS4To.CustomFormat = "MM/dd/yy";
            this.dtpDOS4To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS4To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS4To.Location = new System.Drawing.Point(125, 894);
            this.dtpDOS4To.Name = "dtpDOS4To";
            this.dtpDOS4To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS4To.TabIndex = 134;
            // 
            // dtpDOS4From
            // 
            this.dtpDOS4From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS4From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS4From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS4From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS4From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS4From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS4From.CustomFormat = "MM/dd/yy";
            this.dtpDOS4From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS4From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS4From.Location = new System.Drawing.Point(31, 895);
            this.dtpDOS4From.Name = "dtpDOS4From";
            this.dtpDOS4From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS4From.TabIndex = 133;
            // 
            // dtpDOS3To
            // 
            this.dtpDOS3To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS3To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS3To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS3To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS3To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS3To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS3To.CustomFormat = "MM/dd/yy";
            this.dtpDOS3To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS3To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS3To.Location = new System.Drawing.Point(125, 857);
            this.dtpDOS3To.Name = "dtpDOS3To";
            this.dtpDOS3To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS3To.TabIndex = 119;
            // 
            // dtpDOS3From
            // 
            this.dtpDOS3From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS3From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS3From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS3From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS3From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS3From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS3From.CustomFormat = "MM/dd/yy";
            this.dtpDOS3From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS3From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS3From.Location = new System.Drawing.Point(31, 857);
            this.dtpDOS3From.Name = "dtpDOS3From";
            this.dtpDOS3From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS3From.TabIndex = 118;
            // 
            // dtpDOS2To
            // 
            this.dtpDOS2To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS2To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS2To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS2To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS2To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS2To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS2To.CustomFormat = "MM/dd/yy";
            this.dtpDOS2To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS2To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS2To.Location = new System.Drawing.Point(125, 821);
            this.dtpDOS2To.Name = "dtpDOS2To";
            this.dtpDOS2To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS2To.TabIndex = 104;
            // 
            // dtpDOS2From
            // 
            this.dtpDOS2From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS2From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS2From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS2From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS2From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS2From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS2From.CustomFormat = "MM/dd/yy";
            this.dtpDOS2From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS2From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS2From.Location = new System.Drawing.Point(31, 821);
            this.dtpDOS2From.Name = "dtpDOS2From";
            this.dtpDOS2From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS2From.TabIndex = 103;
            // 
            // dtpDOS1To
            // 
            this.dtpDOS1To.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS1To.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS1To.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS1To.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS1To.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS1To.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS1To.CustomFormat = "MM/dd/yy";
            this.dtpDOS1To.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS1To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS1To.Location = new System.Drawing.Point(125, 784);
            this.dtpDOS1To.Name = "dtpDOS1To";
            this.dtpDOS1To.Size = new System.Drawing.Size(92, 22);
            this.dtpDOS1To.TabIndex = 89;
            // 
            // dtpDOS1From
            // 
            this.dtpDOS1From.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS1From.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOS1From.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOS1From.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOS1From.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOS1From.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOS1From.CustomFormat = "MM/dd/yy";
            this.dtpDOS1From.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOS1From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOS1From.Location = new System.Drawing.Point(31, 784);
            this.dtpDOS1From.Name = "dtpDOS1From";
            this.dtpDOS1From.Size = new System.Drawing.Size(90, 22);
            this.dtpDOS1From.TabIndex = 88;
            // 
            // dtpHospitalisationTo
            // 
            this.dtpHospitalisationTo.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalisationTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpHospitalisationTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpHospitalisationTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpHospitalisationTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpHospitalisationTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpHospitalisationTo.Checked = false;
            this.dtpHospitalisationTo.CustomFormat = "MM/dd/yyyy";
            this.dtpHospitalisationTo.Enabled = false;
            this.dtpHospitalisationTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalisationTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHospitalisationTo.Location = new System.Drawing.Point(753, 604);
            this.dtpHospitalisationTo.Name = "dtpHospitalisationTo";
            this.dtpHospitalisationTo.ShowCheckBox = true;
            this.dtpHospitalisationTo.Size = new System.Drawing.Size(99, 22);
            this.dtpHospitalisationTo.TabIndex = 71;
            // 
            // dtpHospitalisationFrom
            // 
            this.dtpHospitalisationFrom.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalisationFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpHospitalisationFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpHospitalisationFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpHospitalisationFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpHospitalisationFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpHospitalisationFrom.Checked = false;
            this.dtpHospitalisationFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpHospitalisationFrom.Enabled = false;
            this.dtpHospitalisationFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalisationFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHospitalisationFrom.Location = new System.Drawing.Point(603, 604);
            this.dtpHospitalisationFrom.Name = "dtpHospitalisationFrom";
            this.dtpHospitalisationFrom.ShowCheckBox = true;
            this.dtpHospitalisationFrom.Size = new System.Drawing.Size(96, 22);
            this.dtpHospitalisationFrom.TabIndex = 70;
            // 
            // dtpUnableToWorkTill
            // 
            this.dtpUnableToWorkTill.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpUnableToWorkTill.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpUnableToWorkTill.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpUnableToWorkTill.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpUnableToWorkTill.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpUnableToWorkTill.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpUnableToWorkTill.Checked = false;
            this.dtpUnableToWorkTill.CustomFormat = "MM/dd/yyyy";
            this.dtpUnableToWorkTill.Enabled = false;
            this.dtpUnableToWorkTill.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpUnableToWorkTill.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpUnableToWorkTill.Location = new System.Drawing.Point(753, 568);
            this.dtpUnableToWorkTill.Name = "dtpUnableToWorkTill";
            this.dtpUnableToWorkTill.ShowCheckBox = true;
            this.dtpUnableToWorkTill.Size = new System.Drawing.Size(99, 22);
            this.dtpUnableToWorkTill.TabIndex = 67;
            // 
            // dtpUnableToWorkFrom
            // 
            this.dtpUnableToWorkFrom.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpUnableToWorkFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpUnableToWorkFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpUnableToWorkFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpUnableToWorkFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpUnableToWorkFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpUnableToWorkFrom.Checked = false;
            this.dtpUnableToWorkFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpUnableToWorkFrom.Enabled = false;
            this.dtpUnableToWorkFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpUnableToWorkFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpUnableToWorkFrom.Location = new System.Drawing.Point(603, 568);
            this.dtpUnableToWorkFrom.Name = "dtpUnableToWorkFrom";
            this.dtpUnableToWorkFrom.ShowCheckBox = true;
            this.dtpUnableToWorkFrom.Size = new System.Drawing.Size(96, 22);
            this.dtpUnableToWorkFrom.TabIndex = 66;
            // 
            // dtpInsuredsDOB
            // 
            this.dtpInsuredsDOB.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInsuredsDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpInsuredsDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpInsuredsDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpInsuredsDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpInsuredsDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpInsuredsDOB.Checked = false;
            this.dtpInsuredsDOB.CustomFormat = "MM/dd/yyyy";
            this.dtpInsuredsDOB.Enabled = false;
            this.dtpInsuredsDOB.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInsuredsDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInsuredsDOB.Location = new System.Drawing.Point(575, 351);
            this.dtpInsuredsDOB.Name = "dtpInsuredsDOB";
            this.dtpInsuredsDOB.ShowCheckBox = true;
            this.dtpInsuredsDOB.Size = new System.Drawing.Size(117, 22);
            this.dtpInsuredsDOB.TabIndex = 42;
            // 
            // dtpPhysicianSignDate
            // 
            this.dtpPhysicianSignDate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPhysicianSignDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpPhysicianSignDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpPhysicianSignDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpPhysicianSignDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpPhysicianSignDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpPhysicianSignDate.CustomFormat = "MM/dd/yy";
            this.dtpPhysicianSignDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPhysicianSignDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPhysicianSignDate.Location = new System.Drawing.Point(178, 1082);
            this.dtpPhysicianSignDate.Name = "dtpPhysicianSignDate";
            this.dtpPhysicianSignDate.Size = new System.Drawing.Size(78, 22);
            this.dtpPhysicianSignDate.TabIndex = 191;
            // 
            // dtpSimilarIllnessFirstDate
            // 
            this.dtpSimilarIllnessFirstDate.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSimilarIllnessFirstDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpSimilarIllnessFirstDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpSimilarIllnessFirstDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpSimilarIllnessFirstDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpSimilarIllnessFirstDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpSimilarIllnessFirstDate.Checked = false;
            this.dtpSimilarIllnessFirstDate.CustomFormat = "MM/dd/yy";
            this.dtpSimilarIllnessFirstDate.Enabled = false;
            this.dtpSimilarIllnessFirstDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSimilarIllnessFirstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSimilarIllnessFirstDate.Location = new System.Drawing.Point(413, 567);
            this.dtpSimilarIllnessFirstDate.Name = "dtpSimilarIllnessFirstDate";
            this.dtpSimilarIllnessFirstDate.ShowCheckBox = true;
            this.dtpSimilarIllnessFirstDate.Size = new System.Drawing.Size(98, 22);
            this.dtpSimilarIllnessFirstDate.TabIndex = 65;
            // 
            // dtpDateOfCurrentIllness
            // 
            this.dtpDateOfCurrentIllness.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfCurrentIllness.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDateOfCurrentIllness.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDateOfCurrentIllness.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDateOfCurrentIllness.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDateOfCurrentIllness.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDateOfCurrentIllness.Checked = false;
            this.dtpDateOfCurrentIllness.CustomFormat = "MM/dd/yy";
            this.dtpDateOfCurrentIllness.Enabled = false;
            this.dtpDateOfCurrentIllness.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfCurrentIllness.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateOfCurrentIllness.Location = new System.Drawing.Point(34, 568);
            this.dtpDateOfCurrentIllness.Name = "dtpDateOfCurrentIllness";
            this.dtpDateOfCurrentIllness.ShowCheckBox = true;
            this.dtpDateOfCurrentIllness.Size = new System.Drawing.Size(98, 22);
            this.dtpDateOfCurrentIllness.TabIndex = 64;
            // 
            // dtpPatientDOB
            // 
            this.dtpPatientDOB.CalendarFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPatientDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpPatientDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpPatientDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpPatientDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpPatientDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpPatientDOB.CustomFormat = "MM/dd/yyyy";
            this.dtpPatientDOB.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPatientDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPatientDOB.Location = new System.Drawing.Point(353, 168);
            this.dtpPatientDOB.Name = "dtpPatientDOB";
            this.dtpPatientDOB.Size = new System.Drawing.Size(98, 22);
            this.dtpPatientDOB.TabIndex = 10;
            // 
            // txtPatientZip
            // 
            this.txtPatientZip.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientZip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientZip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientZip.Location = new System.Drawing.Point(35, 282);
            this.txtPatientZip.MaxLength = 5;
            this.txtPatientZip.Name = "txtPatientZip";
            this.txtPatientZip.ReadOnly = true;
            this.txtPatientZip.Size = new System.Drawing.Size(117, 15);
            this.txtPatientZip.TabIndex = 27;
            // 
            // txtInsuredsZip
            // 
            this.txtInsuredsZip.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredsZip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredsZip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredsZip.Location = new System.Drawing.Point(575, 282);
            this.txtInsuredsZip.MaxLength = 5;
            this.txtInsuredsZip.Name = "txtInsuredsZip";
            this.txtInsuredsZip.ReadOnly = true;
            this.txtInsuredsZip.Size = new System.Drawing.Size(105, 15);
            this.txtInsuredsZip.TabIndex = 33;
            // 
            // txtInsuredTelephone2
            // 
            this.txtInsuredTelephone2.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredTelephone2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredTelephone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredTelephone2.Location = new System.Drawing.Point(766, 282);
            this.txtInsuredTelephone2.MaxLength = 7;
            this.txtInsuredTelephone2.Name = "txtInsuredTelephone2";
            this.txtInsuredTelephone2.ReadOnly = true;
            this.txtInsuredTelephone2.Size = new System.Drawing.Size(97, 15);
            this.txtInsuredTelephone2.TabIndex = 35;
            // 
            // txtPatientTelephone2
            // 
            this.txtPatientTelephone2.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientTelephone2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientTelephone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientTelephone2.Location = new System.Drawing.Point(230, 282);
            this.txtPatientTelephone2.MaxLength = 7;
            this.txtPatientTelephone2.Name = "txtPatientTelephone2";
            this.txtPatientTelephone2.ReadOnly = true;
            this.txtPatientTelephone2.Size = new System.Drawing.Size(97, 15);
            this.txtPatientTelephone2.TabIndex = 29;
            // 
            // txtInsuredTelephone1
            // 
            this.txtInsuredTelephone1.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredTelephone1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredTelephone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredTelephone1.Location = new System.Drawing.Point(721, 282);
            this.txtInsuredTelephone1.MaxLength = 3;
            this.txtInsuredTelephone1.Name = "txtInsuredTelephone1";
            this.txtInsuredTelephone1.ReadOnly = true;
            this.txtInsuredTelephone1.Size = new System.Drawing.Size(32, 15);
            this.txtInsuredTelephone1.TabIndex = 34;
            this.txtInsuredTelephone1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPatientTelephone1
            // 
            this.txtPatientTelephone1.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientTelephone1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientTelephone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientTelephone1.Location = new System.Drawing.Point(177, 282);
            this.txtPatientTelephone1.MaxLength = 3;
            this.txtPatientTelephone1.Name = "txtPatientTelephone1";
            this.txtPatientTelephone1.ReadOnly = true;
            this.txtPatientTelephone1.Size = new System.Drawing.Size(32, 15);
            this.txtPatientTelephone1.TabIndex = 28;
            this.txtPatientTelephone1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPatientState
            // 
            this.txtPatientState.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientState.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientState.Location = new System.Drawing.Point(304, 244);
            this.txtPatientState.MaxLength = 2;
            this.txtPatientState.Name = "txtPatientState";
            this.txtPatientState.ReadOnly = true;
            this.txtPatientState.Size = new System.Drawing.Size(28, 15);
            this.txtPatientState.TabIndex = 21;
            this.txtPatientState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInsuredsState
            // 
            this.txtInsuredsState.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredsState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredsState.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredsState.Location = new System.Drawing.Point(821, 244);
            this.txtInsuredsState.MaxLength = 2;
            this.txtInsuredsState.Name = "txtInsuredsState";
            this.txtInsuredsState.ReadOnly = true;
            this.txtInsuredsState.Size = new System.Drawing.Size(45, 15);
            this.txtInsuredsState.TabIndex = 26;
            this.txtInsuredsState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPatientCity
            // 
            this.txtPatientCity.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientCity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCity.Location = new System.Drawing.Point(35, 244);
            this.txtPatientCity.Name = "txtPatientCity";
            this.txtPatientCity.ReadOnly = true;
            this.txtPatientCity.Size = new System.Drawing.Size(251, 15);
            this.txtPatientCity.TabIndex = 20;
            // 
            // txtInsuredsCity
            // 
            this.txtInsuredsCity.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredsCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredsCity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredsCity.Location = new System.Drawing.Point(575, 243);
            this.txtInsuredsCity.Name = "txtInsuredsCity";
            this.txtInsuredsCity.ReadOnly = true;
            this.txtInsuredsCity.Size = new System.Drawing.Size(226, 15);
            this.txtInsuredsCity.TabIndex = 25;
            // 
            // txtPatientCoditionRelatedTo_State
            // 
            this.txtPatientCoditionRelatedTo_State.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientCoditionRelatedTo_State.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientCoditionRelatedTo_State.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCoditionRelatedTo_State.Location = new System.Drawing.Point(503, 390);
            this.txtPatientCoditionRelatedTo_State.Name = "txtPatientCoditionRelatedTo_State";
            this.txtPatientCoditionRelatedTo_State.ReadOnly = true;
            this.txtPatientCoditionRelatedTo_State.Size = new System.Drawing.Size(24, 15);
            this.txtPatientCoditionRelatedTo_State.TabIndex = 50;
            this.txtPatientCoditionRelatedTo_State.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtReserveForLocalUse
            // 
            this.txtReserveForLocalUse.BackColor = System.Drawing.Color.Ivory;
            this.txtReserveForLocalUse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReserveForLocalUse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReserveForLocalUse.Location = new System.Drawing.Point(352, 463);
            this.txtReserveForLocalUse.Name = "txtReserveForLocalUse";
            this.txtReserveForLocalUse.ReadOnly = true;
            this.txtReserveForLocalUse.Size = new System.Drawing.Size(183, 15);
            this.txtReserveForLocalUse.TabIndex = 58;
            // 
            // txtPatientCondition
            // 
            this.txtPatientCondition.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientCondition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientCondition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCondition.Location = new System.Drawing.Point(352, 318);
            this.txtPatientCondition.Name = "txtPatientCondition";
            this.txtPatientCondition.ReadOnly = true;
            this.txtPatientCondition.Size = new System.Drawing.Size(183, 15);
            this.txtPatientCondition.TabIndex = 37;
            // 
            // txtInsuredPolicyorFECANo
            // 
            this.txtInsuredPolicyorFECANo.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredPolicyorFECANo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredPolicyorFECANo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredPolicyorFECANo.Location = new System.Drawing.Point(575, 319);
            this.txtInsuredPolicyorFECANo.MaxLength = 20;
            this.txtInsuredPolicyorFECANo.Name = "txtInsuredPolicyorFECANo";
            this.txtInsuredPolicyorFECANo.ReadOnly = true;
            this.txtInsuredPolicyorFECANo.Size = new System.Drawing.Size(291, 15);
            this.txtInsuredPolicyorFECANo.TabIndex = 38;
            // 
            // txtInsuredInsurancePlanName
            // 
            this.txtInsuredInsurancePlanName.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredInsurancePlanName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredInsurancePlanName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredInsurancePlanName.Location = new System.Drawing.Point(575, 426);
            this.txtInsuredInsurancePlanName.Name = "txtInsuredInsurancePlanName";
            this.txtInsuredInsurancePlanName.ReadOnly = true;
            this.txtInsuredInsurancePlanName.Size = new System.Drawing.Size(276, 15);
            this.txtInsuredInsurancePlanName.TabIndex = 55;
            // 
            // txtInsuredEmployerORSchoolName
            // 
            this.txtInsuredEmployerORSchoolName.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredEmployerORSchoolName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredEmployerORSchoolName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredEmployerORSchoolName.Location = new System.Drawing.Point(575, 392);
            this.txtInsuredEmployerORSchoolName.Name = "txtInsuredEmployerORSchoolName";
            this.txtInsuredEmployerORSchoolName.ReadOnly = true;
            this.txtInsuredEmployerORSchoolName.Size = new System.Drawing.Size(291, 15);
            this.txtInsuredEmployerORSchoolName.TabIndex = 51;
            // 
            // txtOutsideLabCharges2
            // 
            this.txtOutsideLabCharges2.BackColor = System.Drawing.Color.Ivory;
            this.txtOutsideLabCharges2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutsideLabCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutsideLabCharges2.Location = new System.Drawing.Point(792, 644);
            this.txtOutsideLabCharges2.MaxLength = 2;
            this.txtOutsideLabCharges2.Name = "txtOutsideLabCharges2";
            this.txtOutsideLabCharges2.ReadOnly = true;
            this.txtOutsideLabCharges2.Size = new System.Drawing.Size(83, 15);
            this.txtOutsideLabCharges2.TabIndex = 76;
            // 
            // txtPriorAuthorizationNumber
            // 
            this.txtPriorAuthorizationNumber.BackColor = System.Drawing.Color.Ivory;
            this.txtPriorAuthorizationNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPriorAuthorizationNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPriorAuthorizationNumber.Location = new System.Drawing.Point(575, 718);
            this.txtPriorAuthorizationNumber.Name = "txtPriorAuthorizationNumber";
            this.txtPriorAuthorizationNumber.ReadOnly = true;
            this.txtPriorAuthorizationNumber.Size = new System.Drawing.Size(300, 15);
            this.txtPriorAuthorizationNumber.TabIndex = 87;
            // 
            // txtOriginalRefNumber
            // 
            this.txtOriginalRefNumber.BackColor = System.Drawing.Color.Ivory;
            this.txtOriginalRefNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOriginalRefNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOriginalRefNumber.Location = new System.Drawing.Point(694, 682);
            this.txtOriginalRefNumber.MaximumSize = new System.Drawing.Size(181, 15);
            this.txtOriginalRefNumber.MaxLength = 15;
            this.txtOriginalRefNumber.MinimumSize = new System.Drawing.Size(181, 15);
            this.txtOriginalRefNumber.Name = "txtOriginalRefNumber";
            this.txtOriginalRefNumber.ReadOnly = true;
            this.txtOriginalRefNumber.Size = new System.Drawing.Size(181, 15);
            this.txtOriginalRefNumber.TabIndex = 86;
            // 
            // txtMedicaidResubmissionCode
            // 
            this.txtMedicaidResubmissionCode.BackColor = System.Drawing.Color.Ivory;
            this.txtMedicaidResubmissionCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMedicaidResubmissionCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMedicaidResubmissionCode.Location = new System.Drawing.Point(575, 681);
            this.txtMedicaidResubmissionCode.MaxLength = 10;
            this.txtMedicaidResubmissionCode.Name = "txtMedicaidResubmissionCode";
            this.txtMedicaidResubmissionCode.ReadOnly = true;
            this.txtMedicaidResubmissionCode.Size = new System.Drawing.Size(90, 15);
            this.txtMedicaidResubmissionCode.TabIndex = 85;
            // 
            // txtOutsideLabCharges1
            // 
            this.txtOutsideLabCharges1.BackColor = System.Drawing.Color.Ivory;
            this.txtOutsideLabCharges1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutsideLabCharges1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutsideLabCharges1.Location = new System.Drawing.Point(686, 644);
            this.txtOutsideLabCharges1.MaxLength = 7;
            this.txtOutsideLabCharges1.Name = "txtOutsideLabCharges1";
            this.txtOutsideLabCharges1.ReadOnly = true;
            this.txtOutsideLabCharges1.Size = new System.Drawing.Size(90, 15);
            this.txtOutsideLabCharges1.TabIndex = 75;
            this.txtOutsideLabCharges1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReferringProvider_NPI
            // 
            this.txtReferringProvider_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtReferringProvider_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReferringProvider_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferringProvider_NPI.Location = new System.Drawing.Point(365, 610);
            this.txtReferringProvider_NPI.MaxLength = 10;
            this.txtReferringProvider_NPI.Name = "txtReferringProvider_NPI";
            this.txtReferringProvider_NPI.ReadOnly = true;
            this.txtReferringProvider_NPI.Size = new System.Drawing.Size(178, 15);
            this.txtReferringProvider_NPI.TabIndex = 69;
            // 
            // txtBillingProv_b_UPIN
            // 
            this.txtBillingProv_b_UPIN.BackColor = System.Drawing.Color.Ivory;
            this.txtBillingProv_b_UPIN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillingProv_b_UPIN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingProv_b_UPIN.Location = new System.Drawing.Point(690, 1098);
            this.txtBillingProv_b_UPIN.MaxLength = 10;
            this.txtBillingProv_b_UPIN.Name = "txtBillingProv_b_UPIN";
            this.txtBillingProv_b_UPIN.ReadOnly = true;
            this.txtBillingProv_b_UPIN.Size = new System.Drawing.Size(176, 15);
            this.txtBillingProv_b_UPIN.TabIndex = 194;
            // 
            // txtBillingProv_a_NPI
            // 
            this.txtBillingProv_a_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtBillingProv_a_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillingProv_a_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingProv_a_NPI.Location = new System.Drawing.Point(569, 1098);
            this.txtBillingProv_a_NPI.MaxLength = 10;
            this.txtBillingProv_a_NPI.Name = "txtBillingProv_a_NPI";
            this.txtBillingProv_a_NPI.ReadOnly = true;
            this.txtBillingProv_a_NPI.Size = new System.Drawing.Size(96, 15);
            this.txtBillingProv_a_NPI.TabIndex = 193;
            // 
            // txtFacility_b
            // 
            this.txtFacility_b.BackColor = System.Drawing.Color.Ivory;
            this.txtFacility_b.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFacility_b.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacility_b.Location = new System.Drawing.Point(395, 1098);
            this.txtFacility_b.MaxLength = 10;
            this.txtFacility_b.Name = "txtFacility_b";
            this.txtFacility_b.ReadOnly = true;
            this.txtFacility_b.Size = new System.Drawing.Size(133, 15);
            this.txtFacility_b.TabIndex = 197;
            // 
            // txtFacility_a_NPI
            // 
            this.txtFacility_a_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtFacility_a_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFacility_a_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacility_a_NPI.Location = new System.Drawing.Point(274, 1098);
            this.txtFacility_a_NPI.MaxLength = 10;
            this.txtFacility_a_NPI.Name = "txtFacility_a_NPI";
            this.txtFacility_a_NPI.ReadOnly = true;
            this.txtFacility_a_NPI.Size = new System.Drawing.Size(112, 15);
            this.txtFacility_a_NPI.TabIndex = 196;
            // 
            // txtCPT6
            // 
            this.txtCPT6.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT6.Location = new System.Drawing.Point(291, 970);
            this.txtCPT6.MaxLength = 5;
            this.txtCPT6.Name = "txtCPT6";
            this.txtCPT6.ReadOnly = true;
            this.txtCPT6.Size = new System.Drawing.Size(66, 15);
            this.txtCPT6.TabIndex = 167;
            this.txtCPT6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCPT5
            // 
            this.txtCPT5.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT5.Location = new System.Drawing.Point(291, 934);
            this.txtCPT5.MaxLength = 5;
            this.txtCPT5.Name = "txtCPT5";
            this.txtCPT5.ReadOnly = true;
            this.txtCPT5.Size = new System.Drawing.Size(66, 15);
            this.txtCPT5.TabIndex = 152;
            this.txtCPT5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCPT4
            // 
            this.txtCPT4.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT4.Location = new System.Drawing.Point(291, 898);
            this.txtCPT4.MaxLength = 5;
            this.txtCPT4.Name = "txtCPT4";
            this.txtCPT4.ReadOnly = true;
            this.txtCPT4.Size = new System.Drawing.Size(66, 15);
            this.txtCPT4.TabIndex = 137;
            this.txtCPT4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCPT3
            // 
            this.txtCPT3.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT3.Location = new System.Drawing.Point(291, 861);
            this.txtCPT3.MaxLength = 5;
            this.txtCPT3.Name = "txtCPT3";
            this.txtCPT3.ReadOnly = true;
            this.txtCPT3.Size = new System.Drawing.Size(66, 15);
            this.txtCPT3.TabIndex = 122;
            this.txtCPT3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG6
            // 
            this.txtEMG6.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG6.Location = new System.Drawing.Point(254, 970);
            this.txtEMG6.MaxLength = 2;
            this.txtEMG6.Name = "txtEMG6";
            this.txtEMG6.ReadOnly = true;
            this.txtEMG6.Size = new System.Drawing.Size(26, 15);
            this.txtEMG6.TabIndex = 166;
            this.txtEMG6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCPT2
            // 
            this.txtCPT2.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT2.Location = new System.Drawing.Point(291, 825);
            this.txtCPT2.MaxLength = 5;
            this.txtCPT2.Name = "txtCPT2";
            this.txtCPT2.ReadOnly = true;
            this.txtCPT2.Size = new System.Drawing.Size(66, 15);
            this.txtCPT2.TabIndex = 107;
            this.txtCPT2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG5
            // 
            this.txtEMG5.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG5.Location = new System.Drawing.Point(254, 934);
            this.txtEMG5.MaxLength = 2;
            this.txtEMG5.Name = "txtEMG5";
            this.txtEMG5.ReadOnly = true;
            this.txtEMG5.Size = new System.Drawing.Size(26, 15);
            this.txtEMG5.TabIndex = 151;
            this.txtEMG5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCPT1
            // 
            this.txtCPT1.BackColor = System.Drawing.Color.Ivory;
            this.txtCPT1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPT1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCPT1.Location = new System.Drawing.Point(291, 788);
            this.txtCPT1.MaxLength = 5;
            this.txtCPT1.Name = "txtCPT1";
            this.txtCPT1.ReadOnly = true;
            this.txtCPT1.Size = new System.Drawing.Size(66, 15);
            this.txtCPT1.TabIndex = 92;
            this.txtCPT1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG4
            // 
            this.txtEMG4.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG4.Location = new System.Drawing.Point(254, 898);
            this.txtEMG4.MaxLength = 2;
            this.txtEMG4.Name = "txtEMG4";
            this.txtEMG4.ReadOnly = true;
            this.txtEMG4.Size = new System.Drawing.Size(26, 15);
            this.txtEMG4.TabIndex = 136;
            this.txtEMG4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG3
            // 
            this.txtEMG3.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG3.Location = new System.Drawing.Point(254, 861);
            this.txtEMG3.MaxLength = 2;
            this.txtEMG3.Name = "txtEMG3";
            this.txtEMG3.ReadOnly = true;
            this.txtEMG3.Size = new System.Drawing.Size(26, 15);
            this.txtEMG3.TabIndex = 121;
            this.txtEMG3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG2
            // 
            this.txtEMG2.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG2.Location = new System.Drawing.Point(254, 825);
            this.txtEMG2.MaxLength = 2;
            this.txtEMG2.Name = "txtEMG2";
            this.txtEMG2.ReadOnly = true;
            this.txtEMG2.Size = new System.Drawing.Size(26, 15);
            this.txtEMG2.TabIndex = 106;
            this.txtEMG2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEMG1
            // 
            this.txtEMG1.BackColor = System.Drawing.Color.Ivory;
            this.txtEMG1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEMG1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEMG1.Location = new System.Drawing.Point(254, 788);
            this.txtEMG1.MaxLength = 2;
            this.txtEMG1.Name = "txtEMG1";
            this.txtEMG1.ReadOnly = true;
            this.txtEMG1.Size = new System.Drawing.Size(26, 15);
            this.txtEMG1.TabIndex = 91;
            this.txtEMG1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEPSDT6
            // 
            this.txtEPSDT6.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT6.Location = new System.Drawing.Point(694, 970);
            this.txtEPSDT6.MaxLength = 2;
            this.txtEPSDT6.Name = "txtEPSDT6";
            this.txtEPSDT6.ReadOnly = true;
            this.txtEPSDT6.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDT6.TabIndex = 176;
            // 
            // txtEPSDT5
            // 
            this.txtEPSDT5.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT5.Location = new System.Drawing.Point(694, 934);
            this.txtEPSDT5.MaxLength = 2;
            this.txtEPSDT5.Name = "txtEPSDT5";
            this.txtEPSDT5.ReadOnly = true;
            this.txtEPSDT5.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDT5.TabIndex = 161;
            // 
            // txtEPSDT4
            // 
            this.txtEPSDT4.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT4.Location = new System.Drawing.Point(694, 898);
            this.txtEPSDT4.MaxLength = 2;
            this.txtEPSDT4.Name = "txtEPSDT4";
            this.txtEPSDT4.ReadOnly = true;
            this.txtEPSDT4.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDT4.TabIndex = 146;
            // 
            // txtEPSDT3
            // 
            this.txtEPSDT3.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT3.Location = new System.Drawing.Point(694, 861);
            this.txtEPSDT3.MaxLength = 2;
            this.txtEPSDT3.Name = "txtEPSDT3";
            this.txtEPSDT3.ReadOnly = true;
            this.txtEPSDT3.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDT3.TabIndex = 131;
            // 
            // txtBillingProviderPhone1
            // 
            this.txtBillingProviderPhone1.BackColor = System.Drawing.Color.Ivory;
            this.txtBillingProviderPhone1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillingProviderPhone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingProviderPhone1.Location = new System.Drawing.Point(732, 1027);
            this.txtBillingProviderPhone1.MaxLength = 3;
            this.txtBillingProviderPhone1.Name = "txtBillingProviderPhone1";
            this.txtBillingProviderPhone1.Size = new System.Drawing.Size(30, 15);
            this.txtBillingProviderPhone1.TabIndex = 194;
            this.txtBillingProviderPhone1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAmountPaid2
            // 
            this.txtAmountPaid2.BackColor = System.Drawing.Color.Ivory;
            this.txtAmountPaid2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmountPaid2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaid2.Location = new System.Drawing.Point(755, 1006);
            this.txtAmountPaid2.MaxLength = 2;
            this.txtAmountPaid2.Name = "txtAmountPaid2";
            this.txtAmountPaid2.ReadOnly = true;
            this.txtAmountPaid2.Size = new System.Drawing.Size(22, 15);
            this.txtAmountPaid2.TabIndex = 187;
            // 
            // txtTotalCharges2
            // 
            this.txtTotalCharges2.BackColor = System.Drawing.Color.Ivory;
            this.txtTotalCharges2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCharges2.Location = new System.Drawing.Point(648, 1006);
            this.txtTotalCharges2.MaxLength = 2;
            this.txtTotalCharges2.Name = "txtTotalCharges2";
            this.txtTotalCharges2.ReadOnly = true;
            this.txtTotalCharges2.Size = new System.Drawing.Size(25, 15);
            this.txtTotalCharges2.TabIndex = 185;
            // 
            // txtCharges61
            // 
            this.txtCharges61.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges61.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges61.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges61.Location = new System.Drawing.Point(622, 970);
            this.txtCharges61.MaxLength = 2;
            this.txtCharges61.Name = "txtCharges61";
            this.txtCharges61.ReadOnly = true;
            this.txtCharges61.Size = new System.Drawing.Size(25, 15);
            this.txtCharges61.TabIndex = 174;
            // 
            // txtEPSDT2
            // 
            this.txtEPSDT2.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT2.Location = new System.Drawing.Point(694, 825);
            this.txtEPSDT2.MaxLength = 2;
            this.txtEPSDT2.Name = "txtEPSDT2";
            this.txtEPSDT2.ReadOnly = true;
            this.txtEPSDT2.Size = new System.Drawing.Size(18, 15);
            this.txtEPSDT2.TabIndex = 116;
            // 
            // txtCharges51
            // 
            this.txtCharges51.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges51.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges51.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges51.Location = new System.Drawing.Point(622, 934);
            this.txtCharges51.MaxLength = 2;
            this.txtCharges51.Name = "txtCharges51";
            this.txtCharges51.ReadOnly = true;
            this.txtCharges51.Size = new System.Drawing.Size(25, 15);
            this.txtCharges51.TabIndex = 159;
            // 
            // txtEPSDT1
            // 
            this.txtEPSDT1.BackColor = System.Drawing.Color.Ivory;
            this.txtEPSDT1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEPSDT1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPSDT1.Location = new System.Drawing.Point(694, 789);
            this.txtEPSDT1.MaxLength = 2;
            this.txtEPSDT1.Name = "txtEPSDT1";
            this.txtEPSDT1.ReadOnly = true;
            this.txtEPSDT1.Size = new System.Drawing.Size(18, 13);
            this.txtEPSDT1.TabIndex = 101;
            // 
            // txtCharges41
            // 
            this.txtCharges41.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges41.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges41.Location = new System.Drawing.Point(622, 898);
            this.txtCharges41.MaxLength = 2;
            this.txtCharges41.Name = "txtCharges41";
            this.txtCharges41.ReadOnly = true;
            this.txtCharges41.Size = new System.Drawing.Size(25, 15);
            this.txtCharges41.TabIndex = 144;
            // 
            // txtCharges31
            // 
            this.txtCharges31.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges31.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges31.Location = new System.Drawing.Point(622, 861);
            this.txtCharges31.MaxLength = 2;
            this.txtCharges31.Name = "txtCharges31";
            this.txtCharges31.ReadOnly = true;
            this.txtCharges31.Size = new System.Drawing.Size(25, 15);
            this.txtCharges31.TabIndex = 129;
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.BackColor = System.Drawing.Color.Ivory;
            this.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmountPaid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaid.Location = new System.Drawing.Point(694, 1006);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.ReadOnly = true;
            this.txtAmountPaid.Size = new System.Drawing.Size(60, 15);
            this.txtAmountPaid.TabIndex = 186;
            this.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalCharges
            // 
            this.txtTotalCharges.BackColor = System.Drawing.Color.Ivory;
            this.txtTotalCharges.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalCharges.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCharges.Location = new System.Drawing.Point(570, 1006);
            this.txtTotalCharges.MaxLength = 8;
            this.txtTotalCharges.Name = "txtTotalCharges";
            this.txtTotalCharges.ReadOnly = true;
            this.txtTotalCharges.Size = new System.Drawing.Size(71, 15);
            this.txtTotalCharges.TabIndex = 184;
            this.txtTotalCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCharges6
            // 
            this.txtCharges6.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges6.Location = new System.Drawing.Point(561, 970);
            this.txtCharges6.MaxLength = 7;
            this.txtCharges6.Name = "txtCharges6";
            this.txtCharges6.ReadOnly = true;
            this.txtCharges6.Size = new System.Drawing.Size(59, 15);
            this.txtCharges6.TabIndex = 173;
            this.txtCharges6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCharges21
            // 
            this.txtCharges21.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges21.Location = new System.Drawing.Point(622, 825);
            this.txtCharges21.MaxLength = 2;
            this.txtCharges21.Name = "txtCharges21";
            this.txtCharges21.Size = new System.Drawing.Size(25, 15);
            this.txtCharges21.TabIndex = 114;
            // 
            // txtCharges5
            // 
            this.txtCharges5.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges5.Location = new System.Drawing.Point(561, 934);
            this.txtCharges5.MaxLength = 7;
            this.txtCharges5.Name = "txtCharges5";
            this.txtCharges5.ReadOnly = true;
            this.txtCharges5.Size = new System.Drawing.Size(59, 15);
            this.txtCharges5.TabIndex = 158;
            this.txtCharges5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCharges11
            // 
            this.txtCharges11.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges11.Location = new System.Drawing.Point(622, 788);
            this.txtCharges11.MaxLength = 2;
            this.txtCharges11.Name = "txtCharges11";
            this.txtCharges11.Size = new System.Drawing.Size(25, 15);
            this.txtCharges11.TabIndex = 99;
            // 
            // txtCharges4
            // 
            this.txtCharges4.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges4.Location = new System.Drawing.Point(561, 895);
            this.txtCharges4.MaxLength = 7;
            this.txtCharges4.Multiline = true;
            this.txtCharges4.Name = "txtCharges4";
            this.txtCharges4.ReadOnly = true;
            this.txtCharges4.Size = new System.Drawing.Size(59, 20);
            this.txtCharges4.TabIndex = 143;
            this.txtCharges4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCharges3
            // 
            this.txtCharges3.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges3.Location = new System.Drawing.Point(561, 858);
            this.txtCharges3.MaxLength = 7;
            this.txtCharges3.Multiline = true;
            this.txtCharges3.Name = "txtCharges3";
            this.txtCharges3.ReadOnly = true;
            this.txtCharges3.Size = new System.Drawing.Size(59, 20);
            this.txtCharges3.TabIndex = 128;
            this.txtCharges3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBillingProviderPhone2
            // 
            this.txtBillingProviderPhone2.BackColor = System.Drawing.Color.Ivory;
            this.txtBillingProviderPhone2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillingProviderPhone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingProviderPhone2.Location = new System.Drawing.Point(775, 1027);
            this.txtBillingProviderPhone2.MaxLength = 7;
            this.txtBillingProviderPhone2.Name = "txtBillingProviderPhone2";
            this.txtBillingProviderPhone2.Size = new System.Drawing.Size(95, 15);
            this.txtBillingProviderPhone2.TabIndex = 195;
            // 
            // txtRenderingProvider6_NPI
            // 
            this.txtRenderingProvider6_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider6_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider6_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider6_NPI.Location = new System.Drawing.Point(749, 970);
            this.txtRenderingProvider6_NPI.MaxLength = 10;
            this.txtRenderingProvider6_NPI.Multiline = true;
            this.txtRenderingProvider6_NPI.Name = "txtRenderingProvider6_NPI";
            this.txtRenderingProvider6_NPI.ReadOnly = true;
            this.txtRenderingProvider6_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider6_NPI.TabIndex = 177;
            // 
            // txtCharges2
            // 
            this.txtCharges2.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges2.Location = new System.Drawing.Point(561, 825);
            this.txtCharges2.MaxLength = 7;
            this.txtCharges2.Name = "txtCharges2";
            this.txtCharges2.ReadOnly = true;
            this.txtCharges2.Size = new System.Drawing.Size(59, 15);
            this.txtCharges2.TabIndex = 113;
            this.txtCharges2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRenderingProvider5_NPI
            // 
            this.txtRenderingProvider5_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider5_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider5_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider5_NPI.Location = new System.Drawing.Point(749, 935);
            this.txtRenderingProvider5_NPI.MaxLength = 10;
            this.txtRenderingProvider5_NPI.Multiline = true;
            this.txtRenderingProvider5_NPI.Name = "txtRenderingProvider5_NPI";
            this.txtRenderingProvider5_NPI.ReadOnly = true;
            this.txtRenderingProvider5_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider5_NPI.TabIndex = 162;
            // 
            // txtCharges1
            // 
            this.txtCharges1.BackColor = System.Drawing.Color.Ivory;
            this.txtCharges1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCharges1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharges1.Location = new System.Drawing.Point(561, 788);
            this.txtCharges1.MaxLength = 7;
            this.txtCharges1.Name = "txtCharges1";
            this.txtCharges1.ReadOnly = true;
            this.txtCharges1.Size = new System.Drawing.Size(59, 15);
            this.txtCharges1.TabIndex = 98;
            this.txtCharges1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRenderingProvider4_NPI
            // 
            this.txtRenderingProvider4_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider4_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider4_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider4_NPI.Location = new System.Drawing.Point(749, 899);
            this.txtRenderingProvider4_NPI.MaxLength = 10;
            this.txtRenderingProvider4_NPI.Multiline = true;
            this.txtRenderingProvider4_NPI.Name = "txtRenderingProvider4_NPI";
            this.txtRenderingProvider4_NPI.ReadOnly = true;
            this.txtRenderingProvider4_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider4_NPI.TabIndex = 147;
            // 
            // txtRenderingProvider3_NPI
            // 
            this.txtRenderingProvider3_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider3_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider3_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider3_NPI.Location = new System.Drawing.Point(749, 862);
            this.txtRenderingProvider3_NPI.MaxLength = 10;
            this.txtRenderingProvider3_NPI.Multiline = true;
            this.txtRenderingProvider3_NPI.Name = "txtRenderingProvider3_NPI";
            this.txtRenderingProvider3_NPI.ReadOnly = true;
            this.txtRenderingProvider3_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider3_NPI.TabIndex = 132;
            // 
            // txtUnits6
            // 
            this.txtUnits6.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits6.Location = new System.Drawing.Point(650, 970);
            this.txtUnits6.MaxLength = 2;
            this.txtUnits6.Name = "txtUnits6";
            this.txtUnits6.ReadOnly = true;
            this.txtUnits6.Size = new System.Drawing.Size(40, 15);
            this.txtUnits6.TabIndex = 175;
            // 
            // txtRenderingProvider2_NPI
            // 
            this.txtRenderingProvider2_NPI.BackColor = System.Drawing.Color.Ivory;
            this.txtRenderingProvider2_NPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRenderingProvider2_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenderingProvider2_NPI.Location = new System.Drawing.Point(749, 826);
            this.txtRenderingProvider2_NPI.MaxLength = 10;
            this.txtRenderingProvider2_NPI.Multiline = true;
            this.txtRenderingProvider2_NPI.Name = "txtRenderingProvider2_NPI";
            this.txtRenderingProvider2_NPI.ReadOnly = true;
            this.txtRenderingProvider2_NPI.Size = new System.Drawing.Size(110, 15);
            this.txtRenderingProvider2_NPI.TabIndex = 117;
            // 
            // txtUnits5
            // 
            this.txtUnits5.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits5.Location = new System.Drawing.Point(650, 934);
            this.txtUnits5.MaxLength = 2;
            this.txtUnits5.Name = "txtUnits5";
            this.txtUnits5.ReadOnly = true;
            this.txtUnits5.Size = new System.Drawing.Size(40, 15);
            this.txtUnits5.TabIndex = 160;
            // 
            // txtUnits4
            // 
            this.txtUnits4.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits4.Location = new System.Drawing.Point(650, 898);
            this.txtUnits4.MaxLength = 2;
            this.txtUnits4.Name = "txtUnits4";
            this.txtUnits4.ReadOnly = true;
            this.txtUnits4.Size = new System.Drawing.Size(40, 15);
            this.txtUnits4.TabIndex = 145;
            // 
            // txtUnits3
            // 
            this.txtUnits3.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits3.Location = new System.Drawing.Point(650, 861);
            this.txtUnits3.MaxLength = 2;
            this.txtUnits3.Name = "txtUnits3";
            this.txtUnits3.ReadOnly = true;
            this.txtUnits3.Size = new System.Drawing.Size(40, 15);
            this.txtUnits3.TabIndex = 130;
            // 
            // txtDxPtr6
            // 
            this.txtDxPtr6.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr6.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr6.Location = new System.Drawing.Point(501, 971);
            this.txtDxPtr6.MaxLength = 8;
            this.txtDxPtr6.Name = "txtDxPtr6";
            this.txtDxPtr6.ReadOnly = true;
            this.txtDxPtr6.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr6.TabIndex = 172;
            this.txtDxPtr6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnits2
            // 
            this.txtUnits2.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits2.Location = new System.Drawing.Point(650, 825);
            this.txtUnits2.MaxLength = 2;
            this.txtUnits2.Name = "txtUnits2";
            this.txtUnits2.ReadOnly = true;
            this.txtUnits2.Size = new System.Drawing.Size(40, 15);
            this.txtUnits2.TabIndex = 115;
            // 
            // txtDxPtr5
            // 
            this.txtDxPtr5.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr5.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr5.Location = new System.Drawing.Point(501, 935);
            this.txtDxPtr5.MaxLength = 8;
            this.txtDxPtr5.Name = "txtDxPtr5";
            this.txtDxPtr5.ReadOnly = true;
            this.txtDxPtr5.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr5.TabIndex = 157;
            this.txtDxPtr5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnits1
            // 
            this.txtUnits1.BackColor = System.Drawing.Color.Ivory;
            this.txtUnits1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUnits1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnits1.Location = new System.Drawing.Point(650, 788);
            this.txtUnits1.MaxLength = 2;
            this.txtUnits1.Name = "txtUnits1";
            this.txtUnits1.ReadOnly = true;
            this.txtUnits1.Size = new System.Drawing.Size(40, 15);
            this.txtUnits1.TabIndex = 100;
            // 
            // txtDxPtr4
            // 
            this.txtDxPtr4.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr4.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr4.Location = new System.Drawing.Point(501, 899);
            this.txtDxPtr4.MaxLength = 8;
            this.txtDxPtr4.Name = "txtDxPtr4";
            this.txtDxPtr4.ReadOnly = true;
            this.txtDxPtr4.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr4.TabIndex = 142;
            this.txtDxPtr4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD64
            // 
            this.txtMOD64.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD64.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD64.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD64.Location = new System.Drawing.Point(466, 970);
            this.txtMOD64.MaxLength = 2;
            this.txtMOD64.Name = "txtMOD64";
            this.txtMOD64.ReadOnly = true;
            this.txtMOD64.Size = new System.Drawing.Size(30, 15);
            this.txtMOD64.TabIndex = 171;
            this.txtMOD64.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDxPtr3
            // 
            this.txtDxPtr3.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr3.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr3.Location = new System.Drawing.Point(501, 862);
            this.txtDxPtr3.MaxLength = 8;
            this.txtDxPtr3.Name = "txtDxPtr3";
            this.txtDxPtr3.ReadOnly = true;
            this.txtDxPtr3.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr3.TabIndex = 127;
            this.txtDxPtr3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD54
            // 
            this.txtMOD54.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD54.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD54.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD54.Location = new System.Drawing.Point(466, 934);
            this.txtMOD54.MaxLength = 2;
            this.txtMOD54.Name = "txtMOD54";
            this.txtMOD54.ReadOnly = true;
            this.txtMOD54.Size = new System.Drawing.Size(30, 15);
            this.txtMOD54.TabIndex = 156;
            this.txtMOD54.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDxPtr2
            // 
            this.txtDxPtr2.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr2.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr2.Location = new System.Drawing.Point(501, 826);
            this.txtDxPtr2.MaxLength = 8;
            this.txtDxPtr2.Name = "txtDxPtr2";
            this.txtDxPtr2.ReadOnly = true;
            this.txtDxPtr2.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr2.TabIndex = 112;
            this.txtDxPtr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD44
            // 
            this.txtMOD44.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD44.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD44.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD44.Location = new System.Drawing.Point(466, 898);
            this.txtMOD44.MaxLength = 2;
            this.txtMOD44.Name = "txtMOD44";
            this.txtMOD44.ReadOnly = true;
            this.txtMOD44.Size = new System.Drawing.Size(30, 15);
            this.txtMOD44.TabIndex = 141;
            this.txtMOD44.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD34
            // 
            this.txtMOD34.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD34.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD34.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD34.Location = new System.Drawing.Point(466, 861);
            this.txtMOD34.MaxLength = 2;
            this.txtMOD34.Name = "txtMOD34";
            this.txtMOD34.ReadOnly = true;
            this.txtMOD34.Size = new System.Drawing.Size(30, 15);
            this.txtMOD34.TabIndex = 126;
            this.txtMOD34.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD63
            // 
            this.txtMOD63.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD63.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD63.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD63.Location = new System.Drawing.Point(432, 970);
            this.txtMOD63.MaxLength = 2;
            this.txtMOD63.Name = "txtMOD63";
            this.txtMOD63.ReadOnly = true;
            this.txtMOD63.Size = new System.Drawing.Size(30, 15);
            this.txtMOD63.TabIndex = 170;
            this.txtMOD63.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD24
            // 
            this.txtMOD24.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD24.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD24.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD24.Location = new System.Drawing.Point(466, 825);
            this.txtMOD24.MaxLength = 2;
            this.txtMOD24.Name = "txtMOD24";
            this.txtMOD24.ReadOnly = true;
            this.txtMOD24.Size = new System.Drawing.Size(30, 15);
            this.txtMOD24.TabIndex = 111;
            this.txtMOD24.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD53
            // 
            this.txtMOD53.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD53.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD53.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD53.Location = new System.Drawing.Point(432, 934);
            this.txtMOD53.MaxLength = 2;
            this.txtMOD53.Name = "txtMOD53";
            this.txtMOD53.ReadOnly = true;
            this.txtMOD53.Size = new System.Drawing.Size(30, 15);
            this.txtMOD53.TabIndex = 155;
            this.txtMOD53.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDxPtr1
            // 
            this.txtDxPtr1.BackColor = System.Drawing.Color.Ivory;
            this.txtDxPtr1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDxPtr1.Font = new System.Drawing.Font("Arial", 7.25F, System.Drawing.FontStyle.Bold);
            this.txtDxPtr1.Location = new System.Drawing.Point(501, 789);
            this.txtDxPtr1.MaxLength = 8;
            this.txtDxPtr1.Name = "txtDxPtr1";
            this.txtDxPtr1.ReadOnly = true;
            this.txtDxPtr1.Size = new System.Drawing.Size(49, 12);
            this.txtDxPtr1.TabIndex = 97;
            this.txtDxPtr1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD43
            // 
            this.txtMOD43.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD43.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD43.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD43.Location = new System.Drawing.Point(432, 898);
            this.txtMOD43.MaxLength = 2;
            this.txtMOD43.Name = "txtMOD43";
            this.txtMOD43.ReadOnly = true;
            this.txtMOD43.Size = new System.Drawing.Size(30, 15);
            this.txtMOD43.TabIndex = 140;
            this.txtMOD43.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD33
            // 
            this.txtMOD33.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD33.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD33.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD33.Location = new System.Drawing.Point(432, 861);
            this.txtMOD33.MaxLength = 2;
            this.txtMOD33.Name = "txtMOD33";
            this.txtMOD33.ReadOnly = true;
            this.txtMOD33.Size = new System.Drawing.Size(30, 15);
            this.txtMOD33.TabIndex = 125;
            this.txtMOD33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD62
            // 
            this.txtMOD62.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD62.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD62.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD62.Location = new System.Drawing.Point(400, 970);
            this.txtMOD62.MaxLength = 2;
            this.txtMOD62.Name = "txtMOD62";
            this.txtMOD62.ReadOnly = true;
            this.txtMOD62.Size = new System.Drawing.Size(30, 15);
            this.txtMOD62.TabIndex = 169;
            this.txtMOD62.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD23
            // 
            this.txtMOD23.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD23.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD23.Location = new System.Drawing.Point(432, 825);
            this.txtMOD23.MaxLength = 2;
            this.txtMOD23.Name = "txtMOD23";
            this.txtMOD23.ReadOnly = true;
            this.txtMOD23.Size = new System.Drawing.Size(30, 15);
            this.txtMOD23.TabIndex = 110;
            this.txtMOD23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD52
            // 
            this.txtMOD52.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD52.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD52.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD52.Location = new System.Drawing.Point(400, 934);
            this.txtMOD52.MaxLength = 2;
            this.txtMOD52.Name = "txtMOD52";
            this.txtMOD52.ReadOnly = true;
            this.txtMOD52.Size = new System.Drawing.Size(30, 15);
            this.txtMOD52.TabIndex = 154;
            this.txtMOD52.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD14
            // 
            this.txtMOD14.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD14.Location = new System.Drawing.Point(466, 788);
            this.txtMOD14.MaxLength = 2;
            this.txtMOD14.Name = "txtMOD14";
            this.txtMOD14.ReadOnly = true;
            this.txtMOD14.Size = new System.Drawing.Size(30, 15);
            this.txtMOD14.TabIndex = 96;
            this.txtMOD14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD42
            // 
            this.txtMOD42.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD42.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD42.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD42.Location = new System.Drawing.Point(400, 898);
            this.txtMOD42.MaxLength = 2;
            this.txtMOD42.Name = "txtMOD42";
            this.txtMOD42.ReadOnly = true;
            this.txtMOD42.Size = new System.Drawing.Size(30, 15);
            this.txtMOD42.TabIndex = 139;
            this.txtMOD42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD61
            // 
            this.txtMOD61.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD61.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD61.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD61.Location = new System.Drawing.Point(365, 970);
            this.txtMOD61.MaxLength = 2;
            this.txtMOD61.Name = "txtMOD61";
            this.txtMOD61.ReadOnly = true;
            this.txtMOD61.Size = new System.Drawing.Size(33, 15);
            this.txtMOD61.TabIndex = 168;
            this.txtMOD61.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD32
            // 
            this.txtMOD32.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD32.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD32.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD32.Location = new System.Drawing.Point(400, 861);
            this.txtMOD32.MaxLength = 2;
            this.txtMOD32.Name = "txtMOD32";
            this.txtMOD32.ReadOnly = true;
            this.txtMOD32.Size = new System.Drawing.Size(30, 15);
            this.txtMOD32.TabIndex = 124;
            this.txtMOD32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD51
            // 
            this.txtMOD51.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD51.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD51.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD51.Location = new System.Drawing.Point(365, 934);
            this.txtMOD51.MaxLength = 2;
            this.txtMOD51.Name = "txtMOD51";
            this.txtMOD51.ReadOnly = true;
            this.txtMOD51.Size = new System.Drawing.Size(33, 15);
            this.txtMOD51.TabIndex = 153;
            this.txtMOD51.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD22
            // 
            this.txtMOD22.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD22.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD22.Location = new System.Drawing.Point(400, 825);
            this.txtMOD22.MaxLength = 2;
            this.txtMOD22.Name = "txtMOD22";
            this.txtMOD22.ReadOnly = true;
            this.txtMOD22.Size = new System.Drawing.Size(30, 15);
            this.txtMOD22.TabIndex = 109;
            this.txtMOD22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD41
            // 
            this.txtMOD41.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD41.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD41.Location = new System.Drawing.Point(365, 898);
            this.txtMOD41.MaxLength = 2;
            this.txtMOD41.Name = "txtMOD41";
            this.txtMOD41.ReadOnly = true;
            this.txtMOD41.Size = new System.Drawing.Size(33, 15);
            this.txtMOD41.TabIndex = 138;
            this.txtMOD41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS6
            // 
            this.txtPOS6.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS6.Location = new System.Drawing.Point(217, 970);
            this.txtPOS6.MaxLength = 2;
            this.txtPOS6.Name = "txtPOS6";
            this.txtPOS6.ReadOnly = true;
            this.txtPOS6.Size = new System.Drawing.Size(31, 15);
            this.txtPOS6.TabIndex = 165;
            this.txtPOS6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD31
            // 
            this.txtMOD31.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD31.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD31.Location = new System.Drawing.Point(365, 861);
            this.txtMOD31.MaxLength = 2;
            this.txtMOD31.Name = "txtMOD31";
            this.txtMOD31.ReadOnly = true;
            this.txtMOD31.Size = new System.Drawing.Size(33, 15);
            this.txtMOD31.TabIndex = 123;
            this.txtMOD31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS5
            // 
            this.txtPOS5.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS5.Location = new System.Drawing.Point(217, 934);
            this.txtPOS5.MaxLength = 2;
            this.txtPOS5.Name = "txtPOS5";
            this.txtPOS5.ReadOnly = true;
            this.txtPOS5.Size = new System.Drawing.Size(31, 15);
            this.txtPOS5.TabIndex = 150;
            this.txtPOS5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD13
            // 
            this.txtMOD13.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD13.Location = new System.Drawing.Point(432, 788);
            this.txtMOD13.MaxLength = 2;
            this.txtMOD13.Name = "txtMOD13";
            this.txtMOD13.ReadOnly = true;
            this.txtMOD13.Size = new System.Drawing.Size(30, 15);
            this.txtMOD13.TabIndex = 95;
            this.txtMOD13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS4
            // 
            this.txtPOS4.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS4.Location = new System.Drawing.Point(217, 898);
            this.txtPOS4.MaxLength = 2;
            this.txtPOS4.Name = "txtPOS4";
            this.txtPOS4.ReadOnly = true;
            this.txtPOS4.Size = new System.Drawing.Size(31, 15);
            this.txtPOS4.TabIndex = 135;
            this.txtPOS4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD21
            // 
            this.txtMOD21.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD21.Location = new System.Drawing.Point(365, 825);
            this.txtMOD21.MaxLength = 2;
            this.txtMOD21.Name = "txtMOD21";
            this.txtMOD21.ReadOnly = true;
            this.txtMOD21.Size = new System.Drawing.Size(33, 15);
            this.txtMOD21.TabIndex = 108;
            this.txtMOD21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS3
            // 
            this.txtPOS3.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS3.Location = new System.Drawing.Point(217, 861);
            this.txtPOS3.MaxLength = 2;
            this.txtPOS3.Name = "txtPOS3";
            this.txtPOS3.ReadOnly = true;
            this.txtPOS3.Size = new System.Drawing.Size(31, 15);
            this.txtPOS3.TabIndex = 120;
            this.txtPOS3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD12
            // 
            this.txtMOD12.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD12.Location = new System.Drawing.Point(400, 788);
            this.txtMOD12.MaxLength = 2;
            this.txtMOD12.Name = "txtMOD12";
            this.txtMOD12.ReadOnly = true;
            this.txtMOD12.Size = new System.Drawing.Size(30, 15);
            this.txtMOD12.TabIndex = 94;
            this.txtMOD12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS2
            // 
            this.txtPOS2.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS2.Location = new System.Drawing.Point(217, 825);
            this.txtPOS2.MaxLength = 2;
            this.txtPOS2.Name = "txtPOS2";
            this.txtPOS2.ReadOnly = true;
            this.txtPOS2.Size = new System.Drawing.Size(31, 15);
            this.txtPOS2.TabIndex = 105;
            this.txtPOS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMOD11
            // 
            this.txtMOD11.BackColor = System.Drawing.Color.Ivory;
            this.txtMOD11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMOD11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMOD11.Location = new System.Drawing.Point(365, 788);
            this.txtMOD11.MaxLength = 2;
            this.txtMOD11.Name = "txtMOD11";
            this.txtMOD11.ReadOnly = true;
            this.txtMOD11.Size = new System.Drawing.Size(33, 15);
            this.txtMOD11.TabIndex = 93;
            this.txtMOD11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPOS1
            // 
            this.txtPOS1.BackColor = System.Drawing.Color.Ivory;
            this.txtPOS1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPOS1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOS1.Location = new System.Drawing.Point(217, 788);
            this.txtPOS1.MaxLength = 2;
            this.txtPOS1.Name = "txtPOS1";
            this.txtPOS1.ReadOnly = true;
            this.txtPOS1.Size = new System.Drawing.Size(31, 15);
            this.txtPOS1.TabIndex = 90;
            this.txtPOS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPhyscianSignature
            // 
            this.txtPhyscianSignature.BackColor = System.Drawing.Color.Ivory;
            this.txtPhyscianSignature.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhyscianSignature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhyscianSignature.Location = new System.Drawing.Point(31, 1071);
            this.txtPhyscianSignature.Multiline = true;
            this.txtPhyscianSignature.Name = "txtPhyscianSignature";
            this.txtPhyscianSignature.ReadOnly = true;
            this.txtPhyscianSignature.Size = new System.Drawing.Size(144, 20);
            this.txtPhyscianSignature.TabIndex = 190;
            // 
            // txtDiagnosisCode41
            // 
            this.txtDiagnosisCode41.BackColor = System.Drawing.Color.Ivory;
            this.txtDiagnosisCode41.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCode41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCode41.Location = new System.Drawing.Point(472, 680);
            this.txtDiagnosisCode41.MaxLength = 4;
            this.txtDiagnosisCode41.Name = "txtDiagnosisCode41";
            this.txtDiagnosisCode41.ReadOnly = true;
            this.txtDiagnosisCode41.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCode41.TabIndex = 83;
            // 
            // txtDiagnosisCode31
            // 
            this.txtDiagnosisCode31.BackColor = System.Drawing.Color.Ivory;
            this.txtDiagnosisCode31.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCode31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCode31.Location = new System.Drawing.Point(333, 680);
            this.txtDiagnosisCode31.MaxLength = 4;
            this.txtDiagnosisCode31.Name = "txtDiagnosisCode31";
            this.txtDiagnosisCode31.ReadOnly = true;
            this.txtDiagnosisCode31.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCode31.TabIndex = 81;
            // 
            // txtBillingProviderInfo
            // 
            this.txtBillingProviderInfo.BackColor = System.Drawing.Color.Ivory;
            this.txtBillingProviderInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillingProviderInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillingProviderInfo.Location = new System.Drawing.Point(561, 1047);
            this.txtBillingProviderInfo.Multiline = true;
            this.txtBillingProviderInfo.Name = "txtBillingProviderInfo";
            this.txtBillingProviderInfo.ReadOnly = true;
            this.txtBillingProviderInfo.Size = new System.Drawing.Size(314, 43);
            this.txtBillingProviderInfo.TabIndex = 193;
            // 
            // txtInsuredPersonSign
            // 
            this.txtInsuredPersonSign.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredPersonSign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredPersonSign.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredPersonSign.Location = new System.Drawing.Point(617, 526);
            this.txtInsuredPersonSign.Multiline = true;
            this.txtInsuredPersonSign.Name = "txtInsuredPersonSign";
            this.txtInsuredPersonSign.ReadOnly = true;
            this.txtInsuredPersonSign.Size = new System.Drawing.Size(261, 20);
            this.txtInsuredPersonSign.TabIndex = 63;
            // 
            // txtPatientSignature
            // 
            this.txtPatientSignature.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientSignature.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientSignature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientSignature.Location = new System.Drawing.Point(80, 526);
            this.txtPatientSignature.Multiline = true;
            this.txtPatientSignature.Name = "txtPatientSignature";
            this.txtPatientSignature.ReadOnly = true;
            this.txtPatientSignature.Size = new System.Drawing.Size(270, 20);
            this.txtPatientSignature.TabIndex = 61;
            // 
            // txtPatientAccountNo
            // 
            this.txtPatientAccountNo.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientAccountNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientAccountNo.Location = new System.Drawing.Point(269, 1006);
            this.txtPatientAccountNo.MaxLength = 15;
            this.txtPatientAccountNo.Name = "txtPatientAccountNo";
            this.txtPatientAccountNo.ReadOnly = true;
            this.txtPatientAccountNo.Size = new System.Drawing.Size(143, 15);
            this.txtPatientAccountNo.TabIndex = 181;
            // 
            // txtFederalTaxID
            // 
            this.txtFederalTaxID.BackColor = System.Drawing.Color.Ivory;
            this.txtFederalTaxID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFederalTaxID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFederalTaxID.Location = new System.Drawing.Point(42, 1006);
            this.txtFederalTaxID.MaxLength = 10;
            this.txtFederalTaxID.Name = "txtFederalTaxID";
            this.txtFederalTaxID.ReadOnly = true;
            this.txtFederalTaxID.Size = new System.Drawing.Size(143, 15);
            this.txtFederalTaxID.TabIndex = 178;
            // 
            // txtDiagnosisCode21
            // 
            this.txtDiagnosisCode21.BackColor = System.Drawing.Color.Ivory;
            this.txtDiagnosisCode21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCode21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCode21.Location = new System.Drawing.Point(192, 680);
            this.txtDiagnosisCode21.MaxLength = 4;
            this.txtDiagnosisCode21.Name = "txtDiagnosisCode21";
            this.txtDiagnosisCode21.ReadOnly = true;
            this.txtDiagnosisCode21.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCode21.TabIndex = 79;
            // 
            // txtDiagnosisCode11
            // 
            this.txtDiagnosisCode11.BackColor = System.Drawing.Color.Ivory;
            this.txtDiagnosisCode11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiagnosisCode11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosisCode11.Location = new System.Drawing.Point(52, 680);
            this.txtDiagnosisCode11.MaxLength = 4;
            this.txtDiagnosisCode11.Name = "txtDiagnosisCode11";
            this.txtDiagnosisCode11.ReadOnly = true;
            this.txtDiagnosisCode11.Size = new System.Drawing.Size(68, 15);
            this.txtDiagnosisCode11.TabIndex = 77;
            // 
            // txtReferringProviderName
            // 
            this.txtReferringProviderName.BackColor = System.Drawing.Color.Ivory;
            this.txtReferringProviderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReferringProviderName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferringProviderName.Location = new System.Drawing.Point(57, 609);
            this.txtReferringProviderName.Name = "txtReferringProviderName";
            this.txtReferringProviderName.ReadOnly = true;
            this.txtReferringProviderName.Size = new System.Drawing.Size(250, 15);
            this.txtReferringProviderName.TabIndex = 68;
            // 
            // txtOtherInsuredInsuranceName
            // 
            this.txtOtherInsuredInsuranceName.BackColor = System.Drawing.Color.Ivory;
            this.txtOtherInsuredInsuranceName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherInsuredInsuranceName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherInsuredInsuranceName.Location = new System.Drawing.Point(35, 463);
            this.txtOtherInsuredInsuranceName.Name = "txtOtherInsuredInsuranceName";
            this.txtOtherInsuredInsuranceName.ReadOnly = true;
            this.txtOtherInsuredInsuranceName.Size = new System.Drawing.Size(291, 15);
            this.txtOtherInsuredInsuranceName.TabIndex = 57;
            // 
            // txt19ReservedForLocalUse
            // 
            this.txt19ReservedForLocalUse.BackColor = System.Drawing.Color.Ivory;
            this.txt19ReservedForLocalUse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt19ReservedForLocalUse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt19ReservedForLocalUse.Location = new System.Drawing.Point(41, 643);
            this.txt19ReservedForLocalUse.MaxLength = 150;
            this.txt19ReservedForLocalUse.Name = "txt19ReservedForLocalUse";
            this.txt19ReservedForLocalUse.ReadOnly = true;
            this.txt19ReservedForLocalUse.Size = new System.Drawing.Size(509, 15);
            this.txt19ReservedForLocalUse.TabIndex = 72;
            // 
            // txtOtherInsuredPolicyNo
            // 
            this.txtOtherInsuredPolicyNo.BackColor = System.Drawing.Color.Ivory;
            this.txtOtherInsuredPolicyNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherInsuredPolicyNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherInsuredPolicyNo.Location = new System.Drawing.Point(35, 353);
            this.txtOtherInsuredPolicyNo.MaxLength = 20;
            this.txtOtherInsuredPolicyNo.Name = "txtOtherInsuredPolicyNo";
            this.txtOtherInsuredPolicyNo.ReadOnly = true;
            this.txtOtherInsuredPolicyNo.Size = new System.Drawing.Size(291, 15);
            this.txtOtherInsuredPolicyNo.TabIndex = 39;
            // 
            // txtOtherInsuredName
            // 
            this.txtOtherInsuredName.BackColor = System.Drawing.Color.Ivory;
            this.txtOtherInsuredName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherInsuredName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherInsuredName.Location = new System.Drawing.Point(35, 317);
            this.txtOtherInsuredName.Name = "txtOtherInsuredName";
            this.txtOtherInsuredName.ReadOnly = true;
            this.txtOtherInsuredName.Size = new System.Drawing.Size(291, 15);
            this.txtOtherInsuredName.TabIndex = 36;
            // 
            // txtPatientAddress
            // 
            this.txtPatientAddress.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientAddress.Location = new System.Drawing.Point(35, 209);
            this.txtPatientAddress.Name = "txtPatientAddress";
            this.txtPatientAddress.ReadOnly = true;
            this.txtPatientAddress.Size = new System.Drawing.Size(291, 15);
            this.txtPatientAddress.TabIndex = 14;
            // 
            // txtInsuredsAddress
            // 
            this.txtInsuredsAddress.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredsAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredsAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredsAddress.Location = new System.Drawing.Point(575, 210);
            this.txtInsuredsAddress.Name = "txtInsuredsAddress";
            this.txtInsuredsAddress.ReadOnly = true;
            this.txtInsuredsAddress.Size = new System.Drawing.Size(291, 15);
            this.txtInsuredsAddress.TabIndex = 19;
            // 
            // txtInsuredName
            // 
            this.txtInsuredName.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredName.Location = new System.Drawing.Point(575, 172);
            this.txtInsuredName.Name = "txtInsuredName";
            this.txtInsuredName.ReadOnly = true;
            this.txtInsuredName.Size = new System.Drawing.Size(291, 15);
            this.txtInsuredName.TabIndex = 13;
            // 
            // txtInsuredIdNumber
            // 
            this.txtInsuredIdNumber.BackColor = System.Drawing.Color.Ivory;
            this.txtInsuredIdNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInsuredIdNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsuredIdNumber.Location = new System.Drawing.Point(575, 137);
            this.txtInsuredIdNumber.MaxLength = 15;
            this.txtInsuredIdNumber.Name = "txtInsuredIdNumber";
            this.txtInsuredIdNumber.ReadOnly = true;
            this.txtInsuredIdNumber.Size = new System.Drawing.Size(291, 15);
            this.txtInsuredIdNumber.TabIndex = 7;
            // 
            // txtPatientName
            // 
            this.txtPatientName.BackColor = System.Drawing.Color.Ivory;
            this.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientName.Location = new System.Drawing.Point(35, 172);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(265, 15);
            this.txtPatientName.TabIndex = 8;
            // 
            // chkPatient_Female
            // 
            this.chkPatient_Female.AutoSize = true;
            this.chkPatient_Female.BackColor = System.Drawing.Color.Ivory;
            this.chkPatient_Female.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatient_Female.ForeColor = System.Drawing.Color.Black;
            this.chkPatient_Female.Location = new System.Drawing.Point(523, 175);
            this.chkPatient_Female.Name = "chkPatient_Female";
            this.chkPatient_Female.Size = new System.Drawing.Size(15, 14);
            this.chkPatient_Female.TabIndex = 12;
            this.chkPatient_Female.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_OtherAccident_No
            // 
            this.chkPatientCoditionRelatedTo_OtherAccident_No.AutoSize = true;
            this.chkPatientCoditionRelatedTo_OtherAccident_No.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_OtherAccident_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_OtherAccident_No.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_OtherAccident_No.Location = new System.Drawing.Point(459, 429);
            this.chkPatientCoditionRelatedTo_OtherAccident_No.Name = "chkPatientCoditionRelatedTo_OtherAccident_No";
            this.chkPatientCoditionRelatedTo_OtherAccident_No.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_OtherAccident_No.TabIndex = 54;
            this.chkPatientCoditionRelatedTo_OtherAccident_No.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_OtherAccident_Yes
            // 
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.AutoSize = true;
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.Location = new System.Drawing.Point(393, 429);
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.Name = "chkPatientCoditionRelatedTo_OtherAccident_Yes";
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.TabIndex = 53;
            this.chkPatientCoditionRelatedTo_OtherAccident_Yes.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_AutoAccident_No
            // 
            this.chkPatientCoditionRelatedTo_AutoAccident_No.AutoSize = true;
            this.chkPatientCoditionRelatedTo_AutoAccident_No.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_AutoAccident_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_AutoAccident_No.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_AutoAccident_No.Location = new System.Drawing.Point(459, 394);
            this.chkPatientCoditionRelatedTo_AutoAccident_No.Name = "chkPatientCoditionRelatedTo_AutoAccident_No";
            this.chkPatientCoditionRelatedTo_AutoAccident_No.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_AutoAccident_No.TabIndex = 49;
            this.chkPatientCoditionRelatedTo_AutoAccident_No.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_AutoAccident_Yes
            // 
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.AutoSize = true;
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.Location = new System.Drawing.Point(393, 394);
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.Name = "chkPatientCoditionRelatedTo_AutoAccident_Yes";
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.TabIndex = 48;
            this.chkPatientCoditionRelatedTo_AutoAccident_Yes.UseVisualStyleBackColor = false;
            // 
            // chkIsOtherHealthPlan_Yes
            // 
            this.chkIsOtherHealthPlan_Yes.AutoSize = true;
            this.chkIsOtherHealthPlan_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkIsOtherHealthPlan_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsOtherHealthPlan_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkIsOtherHealthPlan_Yes.Location = new System.Drawing.Point(576, 465);
            this.chkIsOtherHealthPlan_Yes.Name = "chkIsOtherHealthPlan_Yes";
            this.chkIsOtherHealthPlan_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkIsOtherHealthPlan_Yes.TabIndex = 59;
            this.chkIsOtherHealthPlan_Yes.UseVisualStyleBackColor = false;
            // 
            // chkInsuredSex_Male
            // 
            this.chkInsuredSex_Male.AutoSize = true;
            this.chkInsuredSex_Male.BackColor = System.Drawing.Color.Ivory;
            this.chkInsuredSex_Male.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsuredSex_Male.ForeColor = System.Drawing.Color.Black;
            this.chkInsuredSex_Male.Location = new System.Drawing.Point(751, 357);
            this.chkInsuredSex_Male.Name = "chkInsuredSex_Male";
            this.chkInsuredSex_Male.Size = new System.Drawing.Size(15, 14);
            this.chkInsuredSex_Male.TabIndex = 43;
            this.chkInsuredSex_Male.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_Employment_Yes
            // 
            this.chkPatientCoditionRelatedTo_Employment_Yes.AutoSize = true;
            this.chkPatientCoditionRelatedTo_Employment_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_Employment_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_Employment_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_Employment_Yes.Location = new System.Drawing.Point(393, 358);
            this.chkPatientCoditionRelatedTo_Employment_Yes.Name = "chkPatientCoditionRelatedTo_Employment_Yes";
            this.chkPatientCoditionRelatedTo_Employment_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_Employment_Yes.TabIndex = 40;
            this.chkPatientCoditionRelatedTo_Employment_Yes.UseVisualStyleBackColor = false;
            // 
            // chkOutsideLab_No
            // 
            this.chkOutsideLab_No.AutoSize = true;
            this.chkOutsideLab_No.BackColor = System.Drawing.Color.Ivory;
            this.chkOutsideLab_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOutsideLab_No.ForeColor = System.Drawing.Color.Black;
            this.chkOutsideLab_No.Location = new System.Drawing.Point(633, 646);
            this.chkOutsideLab_No.Name = "chkOutsideLab_No";
            this.chkOutsideLab_No.Size = new System.Drawing.Size(15, 14);
            this.chkOutsideLab_No.TabIndex = 74;
            this.chkOutsideLab_No.UseVisualStyleBackColor = false;
            // 
            // chkAcceptAssignment_No
            // 
            this.chkAcceptAssignment_No.AutoSize = true;
            this.chkAcceptAssignment_No.BackColor = System.Drawing.Color.Ivory;
            this.chkAcceptAssignment_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcceptAssignment_No.ForeColor = System.Drawing.Color.Black;
            this.chkAcceptAssignment_No.Location = new System.Drawing.Point(480, 1008);
            this.chkAcceptAssignment_No.Name = "chkAcceptAssignment_No";
            this.chkAcceptAssignment_No.Size = new System.Drawing.Size(15, 14);
            this.chkAcceptAssignment_No.TabIndex = 183;
            this.chkAcceptAssignment_No.UseVisualStyleBackColor = false;
            // 
            // chkAcceptAssignment_Yes
            // 
            this.chkAcceptAssignment_Yes.AutoSize = true;
            this.chkAcceptAssignment_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkAcceptAssignment_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcceptAssignment_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkAcceptAssignment_Yes.Location = new System.Drawing.Point(426, 1008);
            this.chkAcceptAssignment_Yes.Name = "chkAcceptAssignment_Yes";
            this.chkAcceptAssignment_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkAcceptAssignment_Yes.TabIndex = 182;
            this.chkAcceptAssignment_Yes.UseVisualStyleBackColor = false;
            // 
            // chkFederalTaxID_EIN
            // 
            this.chkFederalTaxID_EIN.AutoSize = true;
            this.chkFederalTaxID_EIN.BackColor = System.Drawing.Color.Ivory;
            this.chkFederalTaxID_EIN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFederalTaxID_EIN.ForeColor = System.Drawing.Color.Black;
            this.chkFederalTaxID_EIN.Location = new System.Drawing.Point(220, 1008);
            this.chkFederalTaxID_EIN.Name = "chkFederalTaxID_EIN";
            this.chkFederalTaxID_EIN.Size = new System.Drawing.Size(15, 14);
            this.chkFederalTaxID_EIN.TabIndex = 180;
            this.chkFederalTaxID_EIN.UseVisualStyleBackColor = false;
            // 
            // chkFederalTaxID_SSN
            // 
            this.chkFederalTaxID_SSN.AutoSize = true;
            this.chkFederalTaxID_SSN.BackColor = System.Drawing.Color.Ivory;
            this.chkFederalTaxID_SSN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFederalTaxID_SSN.ForeColor = System.Drawing.Color.Black;
            this.chkFederalTaxID_SSN.Location = new System.Drawing.Point(198, 1008);
            this.chkFederalTaxID_SSN.Name = "chkFederalTaxID_SSN";
            this.chkFederalTaxID_SSN.Size = new System.Drawing.Size(15, 14);
            this.chkFederalTaxID_SSN.TabIndex = 179;
            this.chkFederalTaxID_SSN.UseVisualStyleBackColor = false;
            // 
            // chkOutsideLab_Yes
            // 
            this.chkOutsideLab_Yes.AutoSize = true;
            this.chkOutsideLab_Yes.BackColor = System.Drawing.Color.Ivory;
            this.chkOutsideLab_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOutsideLab_Yes.ForeColor = System.Drawing.Color.Black;
            this.chkOutsideLab_Yes.Location = new System.Drawing.Point(578, 647);
            this.chkOutsideLab_Yes.Name = "chkOutsideLab_Yes";
            this.chkOutsideLab_Yes.Size = new System.Drawing.Size(15, 14);
            this.chkOutsideLab_Yes.TabIndex = 73;
            this.chkOutsideLab_Yes.UseVisualStyleBackColor = false;
            // 
            // chkIsOtherHealthPlan_No
            // 
            this.chkIsOtherHealthPlan_No.AutoSize = true;
            this.chkIsOtherHealthPlan_No.BackColor = System.Drawing.Color.Ivory;
            this.chkIsOtherHealthPlan_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsOtherHealthPlan_No.ForeColor = System.Drawing.Color.Black;
            this.chkIsOtherHealthPlan_No.Location = new System.Drawing.Point(632, 465);
            this.chkIsOtherHealthPlan_No.Name = "chkIsOtherHealthPlan_No";
            this.chkIsOtherHealthPlan_No.Size = new System.Drawing.Size(15, 14);
            this.chkIsOtherHealthPlan_No.TabIndex = 60;
            this.chkIsOtherHealthPlan_No.UseVisualStyleBackColor = false;
            // 
            // chkInsuredSex_Female
            // 
            this.chkInsuredSex_Female.AutoSize = true;
            this.chkInsuredSex_Female.BackColor = System.Drawing.Color.Ivory;
            this.chkInsuredSex_Female.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsuredSex_Female.ForeColor = System.Drawing.Color.Black;
            this.chkInsuredSex_Female.Location = new System.Drawing.Point(828, 357);
            this.chkInsuredSex_Female.Name = "chkInsuredSex_Female";
            this.chkInsuredSex_Female.Size = new System.Drawing.Size(15, 14);
            this.chkInsuredSex_Female.TabIndex = 44;
            this.chkInsuredSex_Female.UseVisualStyleBackColor = false;
            // 
            // chkPatientCoditionRelatedTo_Employment_No
            // 
            this.chkPatientCoditionRelatedTo_Employment_No.AutoSize = true;
            this.chkPatientCoditionRelatedTo_Employment_No.BackColor = System.Drawing.Color.Ivory;
            this.chkPatientCoditionRelatedTo_Employment_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatientCoditionRelatedTo_Employment_No.ForeColor = System.Drawing.Color.Black;
            this.chkPatientCoditionRelatedTo_Employment_No.Location = new System.Drawing.Point(459, 358);
            this.chkPatientCoditionRelatedTo_Employment_No.Name = "chkPatientCoditionRelatedTo_Employment_No";
            this.chkPatientCoditionRelatedTo_Employment_No.Size = new System.Drawing.Size(15, 14);
            this.chkPatientCoditionRelatedTo_Employment_No.TabIndex = 41;
            this.chkPatientCoditionRelatedTo_Employment_No.UseVisualStyleBackColor = false;
            // 
            // chkRelationship_Other
            // 
            this.chkRelationship_Other.AutoSize = true;
            this.chkRelationship_Other.BackColor = System.Drawing.Color.Ivory;
            this.chkRelationship_Other.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRelationship_Other.ForeColor = System.Drawing.Color.Black;
            this.chkRelationship_Other.Location = new System.Drawing.Point(523, 211);
            this.chkRelationship_Other.Name = "chkRelationship_Other";
            this.chkRelationship_Other.Size = new System.Drawing.Size(15, 14);
            this.chkRelationship_Other.TabIndex = 18;
            this.chkRelationship_Other.UseVisualStyleBackColor = false;
            // 
            // chkRelationship_Child
            // 
            this.chkRelationship_Child.AutoSize = true;
            this.chkRelationship_Child.BackColor = System.Drawing.Color.Ivory;
            this.chkRelationship_Child.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRelationship_Child.ForeColor = System.Drawing.Color.Black;
            this.chkRelationship_Child.Location = new System.Drawing.Point(469, 212);
            this.chkRelationship_Child.Name = "chkRelationship_Child";
            this.chkRelationship_Child.Size = new System.Drawing.Size(15, 14);
            this.chkRelationship_Child.TabIndex = 17;
            this.chkRelationship_Child.UseVisualStyleBackColor = false;
            // 
            // chkRelationship_Spouse
            // 
            this.chkRelationship_Spouse.AutoSize = true;
            this.chkRelationship_Spouse.BackColor = System.Drawing.Color.Ivory;
            this.chkRelationship_Spouse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRelationship_Spouse.ForeColor = System.Drawing.Color.Black;
            this.chkRelationship_Spouse.Location = new System.Drawing.Point(426, 212);
            this.chkRelationship_Spouse.Name = "chkRelationship_Spouse";
            this.chkRelationship_Spouse.Size = new System.Drawing.Size(15, 14);
            this.chkRelationship_Spouse.TabIndex = 16;
            this.chkRelationship_Spouse.UseVisualStyleBackColor = false;
            // 
            // chkRelationship_Self
            // 
            this.chkRelationship_Self.AutoSize = true;
            this.chkRelationship_Self.BackColor = System.Drawing.Color.Ivory;
            this.chkRelationship_Self.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRelationship_Self.ForeColor = System.Drawing.Color.Black;
            this.chkRelationship_Self.Location = new System.Drawing.Point(370, 212);
            this.chkRelationship_Self.Name = "chkRelationship_Self";
            this.chkRelationship_Self.Size = new System.Drawing.Size(15, 14);
            this.chkRelationship_Self.TabIndex = 15;
            this.chkRelationship_Self.UseVisualStyleBackColor = false;
            // 
            // chkPatient_Male
            // 
            this.chkPatient_Male.AutoSize = true;
            this.chkPatient_Male.BackColor = System.Drawing.Color.Ivory;
            this.chkPatient_Male.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatient_Male.ForeColor = System.Drawing.Color.Black;
            this.chkPatient_Male.Location = new System.Drawing.Point(469, 175);
            this.chkPatient_Male.Name = "chkPatient_Male";
            this.chkPatient_Male.Size = new System.Drawing.Size(15, 14);
            this.chkPatient_Male.TabIndex = 11;
            this.chkPatient_Male.UseVisualStyleBackColor = false;
            // 
            // chkOtherInsuranceType
            // 
            this.chkOtherInsuranceType.AutoSize = true;
            this.chkOtherInsuranceType.BackColor = System.Drawing.Color.Ivory;
            this.chkOtherInsuranceType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOtherInsuranceType.ForeColor = System.Drawing.Color.Black;
            this.chkOtherInsuranceType.Location = new System.Drawing.Point(501, 139);
            this.chkOtherInsuranceType.Name = "chkOtherInsuranceType";
            this.chkOtherInsuranceType.Size = new System.Drawing.Size(15, 14);
            this.chkOtherInsuranceType.TabIndex = 6;
            this.chkOtherInsuranceType.UseVisualStyleBackColor = false;
            // 
            // chkFECABlackLung
            // 
            this.chkFECABlackLung.AutoSize = true;
            this.chkFECABlackLung.BackColor = System.Drawing.Color.Ivory;
            this.chkFECABlackLung.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFECABlackLung.ForeColor = System.Drawing.Color.Black;
            this.chkFECABlackLung.Location = new System.Drawing.Point(435, 139);
            this.chkFECABlackLung.Name = "chkFECABlackLung";
            this.chkFECABlackLung.Size = new System.Drawing.Size(15, 14);
            this.chkFECABlackLung.TabIndex = 5;
            this.chkFECABlackLung.UseVisualStyleBackColor = false;
            // 
            // chkGroupHealthPlan
            // 
            this.chkGroupHealthPlan.AutoSize = true;
            this.chkGroupHealthPlan.BackColor = System.Drawing.Color.Ivory;
            this.chkGroupHealthPlan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupHealthPlan.ForeColor = System.Drawing.Color.Black;
            this.chkGroupHealthPlan.Location = new System.Drawing.Point(349, 139);
            this.chkGroupHealthPlan.Name = "chkGroupHealthPlan";
            this.chkGroupHealthPlan.Size = new System.Drawing.Size(15, 14);
            this.chkGroupHealthPlan.TabIndex = 4;
            this.chkGroupHealthPlan.UseVisualStyleBackColor = false;
            // 
            // chkCHAMPVA
            // 
            this.chkCHAMPVA.AutoSize = true;
            this.chkCHAMPVA.BackColor = System.Drawing.Color.Ivory;
            this.chkCHAMPVA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCHAMPVA.ForeColor = System.Drawing.Color.Black;
            this.chkCHAMPVA.Location = new System.Drawing.Point(272, 139);
            this.chkCHAMPVA.Name = "chkCHAMPVA";
            this.chkCHAMPVA.Size = new System.Drawing.Size(15, 14);
            this.chkCHAMPVA.TabIndex = 3;
            this.chkCHAMPVA.UseVisualStyleBackColor = false;
            // 
            // chkTricareChampus
            // 
            this.chkTricareChampus.AutoSize = true;
            this.chkTricareChampus.BackColor = System.Drawing.Color.Ivory;
            this.chkTricareChampus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTricareChampus.ForeColor = System.Drawing.Color.Black;
            this.chkTricareChampus.Location = new System.Drawing.Point(174, 139);
            this.chkTricareChampus.Name = "chkTricareChampus";
            this.chkTricareChampus.Size = new System.Drawing.Size(15, 14);
            this.chkTricareChampus.TabIndex = 2;
            this.chkTricareChampus.UseVisualStyleBackColor = false;
            // 
            // chkMedicaid
            // 
            this.chkMedicaid.AutoSize = true;
            this.chkMedicaid.BackColor = System.Drawing.Color.Ivory;
            this.chkMedicaid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMedicaid.ForeColor = System.Drawing.Color.Black;
            this.chkMedicaid.Location = new System.Drawing.Point(98, 139);
            this.chkMedicaid.Name = "chkMedicaid";
            this.chkMedicaid.Size = new System.Drawing.Size(15, 14);
            this.chkMedicaid.TabIndex = 1;
            this.chkMedicaid.UseVisualStyleBackColor = false;
            // 
            // chkMedicare
            // 
            this.chkMedicare.AutoSize = true;
            this.chkMedicare.BackColor = System.Drawing.Color.Ivory;
            this.chkMedicare.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMedicare.ForeColor = System.Drawing.Color.Black;
            this.chkMedicare.Location = new System.Drawing.Point(24, 139);
            this.chkMedicare.Name = "chkMedicare";
            this.chkMedicare.Size = new System.Drawing.Size(15, 14);
            this.chkMedicare.TabIndex = 0;
            this.chkMedicare.UseVisualStyleBackColor = false;
            // 
            // cmbRenderingProvider1
            // 
            this.cmbRenderingProvider1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider1.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider1.FormattingEnabled = true;
            this.cmbRenderingProvider1.Location = new System.Drawing.Point(749, 783);
            this.cmbRenderingProvider1.Name = "cmbRenderingProvider1";
            this.cmbRenderingProvider1.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider1.TabIndex = 102;
            // 
            // cmbRenderingProvider2
            // 
            this.cmbRenderingProvider2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider2.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider2.FormattingEnabled = true;
            this.cmbRenderingProvider2.Location = new System.Drawing.Point(749, 816);
            this.cmbRenderingProvider2.Name = "cmbRenderingProvider2";
            this.cmbRenderingProvider2.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider2.TabIndex = 117;
            // 
            // cmbRenderingProvider3
            // 
            this.cmbRenderingProvider3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider3.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider3.FormattingEnabled = true;
            this.cmbRenderingProvider3.Location = new System.Drawing.Point(749, 854);
            this.cmbRenderingProvider3.Name = "cmbRenderingProvider3";
            this.cmbRenderingProvider3.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider3.TabIndex = 132;
            // 
            // cmbRenderingProvider4
            // 
            this.cmbRenderingProvider4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider4.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider4.FormattingEnabled = true;
            this.cmbRenderingProvider4.Location = new System.Drawing.Point(749, 892);
            this.cmbRenderingProvider4.Name = "cmbRenderingProvider4";
            this.cmbRenderingProvider4.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider4.TabIndex = 147;
            // 
            // cmbRenderingProvider5
            // 
            this.cmbRenderingProvider5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider5.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider5.FormattingEnabled = true;
            this.cmbRenderingProvider5.Location = new System.Drawing.Point(749, 926);
            this.cmbRenderingProvider5.Name = "cmbRenderingProvider5";
            this.cmbRenderingProvider5.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider5.TabIndex = 162;
            // 
            // cmbRenderingProvider6
            // 
            this.cmbRenderingProvider6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRenderingProvider6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRenderingProvider6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRenderingProvider6.ForeColor = System.Drawing.Color.Black;
            this.cmbRenderingProvider6.FormattingEnabled = true;
            this.cmbRenderingProvider6.Location = new System.Drawing.Point(749, 962);
            this.cmbRenderingProvider6.Name = "cmbRenderingProvider6";
            this.cmbRenderingProvider6.Size = new System.Drawing.Size(130, 22);
            this.cmbRenderingProvider6.TabIndex = 177;
            // 
            // pnlLabel
            // 
            this.pnlLabel.Controls.Add(this.panel4);
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLabel.Size = new System.Drawing.Size(927, 1153);
            this.pnlLabel.TabIndex = 217;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.lblBottom);
            this.panel4.Controls.Add(this.lblRight);
            this.panel4.Controls.Add(this.pnlMainFormLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(918, 1147);
            this.panel4.TabIndex = 8;
            // 
            // lblBottom
            // 
            this.lblBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBottom.Location = new System.Drawing.Point(0, 1163);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(900, 1);
            this.lblBottom.TabIndex = 12;
            this.lblBottom.Text = "label2";
            // 
            // lblRight
            // 
            this.lblRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRight.Location = new System.Drawing.Point(900, 0);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(1, 1164);
            this.lblRight.TabIndex = 10;
            this.lblRight.Text = "label3";
            // 
            // pnlMainFormLabel
            // 
            this.pnlMainFormLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMainFormLabel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMainFormLabel.BackgroundImage")));
            this.pnlMainFormLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlMainFormLabel.Controls.Add(this.lblBillingProviderPhone2);
            this.pnlMainFormLabel.Controls.Add(this.lblBillingProviderPhone1);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherClaimIDQualifier);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeE);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeF);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeG);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeH);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeI);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeJ);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeK);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCodeL);
            this.pnlMainFormLabel.Controls.Add(this.lblReferringProviderQualifier);
            this.pnlMainFormLabel.Controls.Add(this.lblSimilarIllnessFirstDateQualifier);
            this.pnlMainFormLabel.Controls.Add(this.lblDateOfCurrentIllnessQualifier);
            this.pnlMainFormLabel.Controls.Add(this.lblIcdInd1);
            this.pnlMainFormLabel.Controls.Add(this.lblIcdInd2);
            this.pnlMainFormLabel.Controls.Add(this.lblShadedEPSDT6);
            this.pnlMainFormLabel.Controls.Add(this.lblShadedEPSDT5);
            this.pnlMainFormLabel.Controls.Add(this.lblShadedEPSDT4);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDTShaded3);
            this.pnlMainFormLabel.Controls.Add(this.lblShadedEPSDT2);
            this.pnlMainFormLabel.Controls.Add(this.lblShadedEPSDT1);
            this.pnlMainFormLabel.Controls.Add(this.lblPhyscianQualifierValue);
            this.pnlMainFormLabel.Controls.Add(this.lblReferringProvider_OtherType);
            this.pnlMainFormLabel.Controls.Add(this.lblReferringProvider_OtherValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider6_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider5_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider4_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider3_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider2_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider1_QFValue);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider6_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider5_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider4_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider3_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider2_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider1_QF);
            this.pnlMainFormLabel.Controls.Add(this.lblAmountPaid1);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes6);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes5);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes4);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes3);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes2);
            this.pnlMainFormLabel.Controls.Add(this.btn_BrowseFacility);
            this.pnlMainFormLabel.Controls.Add(this.lblBillingProv_b_UPIN);
            this.pnlMainFormLabel.Controls.Add(this.lblBillingProv_a_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblBillingProviderInfo);
            this.pnlMainFormLabel.Controls.Add(this.lblFacility_b);
            this.pnlMainFormLabel.Controls.Add(this.lblFacility_a_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblFacilityInfo);
            this.pnlMainFormLabel.Controls.Add(this.lblPhysicianSignDate_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblPhysicianSignDate_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblPhysicianSignDate_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblPhyscianSignature);
            this.pnlMainFormLabel.Controls.Add(this.lblBalanceDue2);
            this.pnlMainFormLabel.Controls.Add(this.lblBalanceDue);
            this.pnlMainFormLabel.Controls.Add(this.lblAmountPaid);
            this.pnlMainFormLabel.Controls.Add(this.lblTotalCharges2);
            this.pnlMainFormLabel.Controls.Add(this.lblTotalCharges);
            this.pnlMainFormLabel.Controls.Add(this.lblAcceptAssignment_No);
            this.pnlMainFormLabel.Controls.Add(this.lblAcceptAssignment_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientAccountNo);
            this.pnlMainFormLabel.Controls.Add(this.lblFederalTaxID_EIN);
            this.pnlMainFormLabel.Controls.Add(this.lblFederalTaxID_SSN);
            this.pnlMainFormLabel.Controls.Add(this.lblFederalTaxID);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider6_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT6);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits6);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges61);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges6);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr6);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD64);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD63);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD62);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD61);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT6);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG6);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS6);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS6From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider5_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT5);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits5);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges51);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges5);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr5);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD54);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD53);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD52);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD51);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT5);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG5);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS5);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS5From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider4_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT4);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits4);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges41);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges4);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr4);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD44);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD43);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD42);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD41);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT4);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG4);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS4);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS4From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider3_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT3);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits3);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges31);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges3);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr3);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD34);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD33);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD32);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD31);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT3);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG3);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS3);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS3From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider2_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT2);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits2);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges21);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges2);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr2);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD24);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD23);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD22);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD21);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT2);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG2);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS2);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS2From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblRenderingProvider1_NPI);
            this.pnlMainFormLabel.Controls.Add(this.lblEPSDT1);
            this.pnlMainFormLabel.Controls.Add(this.lblUnits1);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges11);
            this.pnlMainFormLabel.Controls.Add(this.lblCharges1);
            this.pnlMainFormLabel.Controls.Add(this.lblDxPtr1);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD14);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD13);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD12);
            this.pnlMainFormLabel.Controls.Add(this.lblMOD11);
            this.pnlMainFormLabel.Controls.Add(this.lblCPT1);
            this.pnlMainFormLabel.Controls.Add(this.lblEMG1);
            this.pnlMainFormLabel.Controls.Add(this.lblPOS1);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1To_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1To_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1To_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1From_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1From_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDOS1From_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblNotes1);
            this.pnlMainFormLabel.Controls.Add(this.lblPriorAuthorizationNumber);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCode41);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCode21);
            this.pnlMainFormLabel.Controls.Add(this.lblOriginalRefNumber);
            this.pnlMainFormLabel.Controls.Add(this.lblMedicaidResubmissionCode);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCode31);
            this.pnlMainFormLabel.Controls.Add(this.lblDiagnosisCode11);
            this.pnlMainFormLabel.Controls.Add(this.lblOutsideLabCharges2);
            this.pnlMainFormLabel.Controls.Add(this.lblOutsideLabCharges1);
            this.pnlMainFormLabel.Controls.Add(this.lblOutsideLab_No);
            this.pnlMainFormLabel.Controls.Add(this.lblOutsideLab_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblReservedForLocalUse);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationTo_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationTo_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationTo_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationFrom_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationFrom_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblHospitalisationFrom_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblReferringProvider_NPI);
            this.pnlMainFormLabel.Controls.Add(this.btn_BrowseReferral);
            this.pnlMainFormLabel.Controls.Add(this.lblReferringProviderName);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkTill_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkTill_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkTill_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkFrom_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkFrom_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblUnableToWorkFrom_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblSimilarIllnessFirstDate_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblSimilarIllnessFirstDate_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblSimilarIllnessFirstDate_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblDateOfCurrentIllness_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblDateOfCurrentIllness_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblDateOfCurrentIllness_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredPersonSign);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientSignDate_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientSignDate_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientSignDate_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientSignature);
            this.pnlMainFormLabel.Controls.Add(this.btnBrowsePatientRegt);
            this.pnlMainFormLabel.Controls.Add(this.lblIsOtherHealthPlan_No);
            this.pnlMainFormLabel.Controls.Add(this.lblIsOtherHealthPlan_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblReserveForLocalUse);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherInsuredInsuranceName);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredInsurancePlanName);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_OtherAccident_No);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_OtherAccident_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherInsuredEmployerORSchoolName);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredEmployerORSchoolName);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_State);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_AutoAccident_No);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_AutoAccident_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredSex_Female);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredSex_Male);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsDOB_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsDOB_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsDOB_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_Employment_No);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCoditionRelatedTo_Employment_Yes);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherInsuredPolicyNo);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredPolicyorFECANo);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCondition);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherInsuredName);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredTelephone2);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredTelephone1);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsZip);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientTelephone2);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientTelephone1);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientZip);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsState);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsCity);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientState);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientCity);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredsAddress);
            this.pnlMainFormLabel.Controls.Add(this.lblRelationship_Other);
            this.pnlMainFormLabel.Controls.Add(this.lblRelationship_Child);
            this.pnlMainFormLabel.Controls.Add(this.lblRelationship_Spouse);
            this.pnlMainFormLabel.Controls.Add(this.lblRelationship_Self);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientAddress);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredName);
            this.pnlMainFormLabel.Controls.Add(this.lblPatient_Female);
            this.pnlMainFormLabel.Controls.Add(this.lblPatient_Male);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientDOB_YY);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientDOB_DD);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientDOB_MM);
            this.pnlMainFormLabel.Controls.Add(this.lblInsuredIdNumber);
            this.pnlMainFormLabel.Controls.Add(this.btnPatientBrowse);
            this.pnlMainFormLabel.Controls.Add(this.lblPatientName);
            this.pnlMainFormLabel.Controls.Add(this.lblOtherInsuranceType);
            this.pnlMainFormLabel.Controls.Add(this.lblFECABlackLung);
            this.pnlMainFormLabel.Controls.Add(this.lblGroupHealthPlan);
            this.pnlMainFormLabel.Controls.Add(this.lblCHAMPVA);
            this.pnlMainFormLabel.Controls.Add(this.lblTricareChampus);
            this.pnlMainFormLabel.Controls.Add(this.lblMedicaid);
            this.pnlMainFormLabel.Controls.Add(this.lblMedicare);
            this.pnlMainFormLabel.Controls.Add(this.lblPayerNameAndAddress);
            this.pnlMainFormLabel.Location = new System.Drawing.Point(-1, 16);
            this.pnlMainFormLabel.Name = "pnlMainFormLabel";
            this.pnlMainFormLabel.Size = new System.Drawing.Size(900, 1147);
            this.pnlMainFormLabel.TabIndex = 0;
            // 
            // lblBillingProviderPhone2
            // 
            this.lblBillingProviderPhone2.BackColor = System.Drawing.Color.Ivory;
            this.lblBillingProviderPhone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingProviderPhone2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBillingProviderPhone2.Location = new System.Drawing.Point(773, 1027);
            this.lblBillingProviderPhone2.Name = "lblBillingProviderPhone2";
            this.lblBillingProviderPhone2.Size = new System.Drawing.Size(95, 15);
            this.lblBillingProviderPhone2.TabIndex = 352;
            this.lblBillingProviderPhone2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillingProviderPhone1
            // 
            this.lblBillingProviderPhone1.BackColor = System.Drawing.Color.Ivory;
            this.lblBillingProviderPhone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingProviderPhone1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBillingProviderPhone1.Location = new System.Drawing.Point(732, 1027);
            this.lblBillingProviderPhone1.Name = "lblBillingProviderPhone1";
            this.lblBillingProviderPhone1.Size = new System.Drawing.Size(30, 15);
            this.lblBillingProviderPhone1.TabIndex = 351;
            this.lblBillingProviderPhone1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOtherClaimIDQualifier
            // 
            this.lblOtherClaimIDQualifier.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherClaimIDQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherClaimIDQualifier.ForeColor = System.Drawing.Color.Black;
            this.lblOtherClaimIDQualifier.Location = new System.Drawing.Point(555, 392);
            this.lblOtherClaimIDQualifier.Name = "lblOtherClaimIDQualifier";
            this.lblOtherClaimIDQualifier.Size = new System.Drawing.Size(23, 15);
            this.lblOtherClaimIDQualifier.TabIndex = 388;
            this.lblOtherClaimIDQualifier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDiagnosisCodeE
            // 
            this.lblDiagnosisCodeE.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeE.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeE.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeE.Location = new System.Drawing.Point(51, 699);
            this.lblDiagnosisCodeE.Name = "lblDiagnosisCodeE";
            this.lblDiagnosisCodeE.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeE.TabIndex = 394;
            // 
            // lblDiagnosisCodeF
            // 
            this.lblDiagnosisCodeF.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeF.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeF.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeF.Location = new System.Drawing.Point(192, 699);
            this.lblDiagnosisCodeF.Name = "lblDiagnosisCodeF";
            this.lblDiagnosisCodeF.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeF.TabIndex = 395;
            // 
            // lblDiagnosisCodeG
            // 
            this.lblDiagnosisCodeG.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeG.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeG.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeG.Location = new System.Drawing.Point(333, 699);
            this.lblDiagnosisCodeG.Name = "lblDiagnosisCodeG";
            this.lblDiagnosisCodeG.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeG.TabIndex = 396;
            // 
            // lblDiagnosisCodeH
            // 
            this.lblDiagnosisCodeH.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeH.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeH.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeH.Location = new System.Drawing.Point(472, 700);
            this.lblDiagnosisCodeH.Name = "lblDiagnosisCodeH";
            this.lblDiagnosisCodeH.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeH.TabIndex = 397;
            // 
            // lblDiagnosisCodeI
            // 
            this.lblDiagnosisCodeI.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeI.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeI.Location = new System.Drawing.Point(51, 717);
            this.lblDiagnosisCodeI.Name = "lblDiagnosisCodeI";
            this.lblDiagnosisCodeI.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeI.TabIndex = 398;
            // 
            // lblDiagnosisCodeJ
            // 
            this.lblDiagnosisCodeJ.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeJ.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeJ.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeJ.Location = new System.Drawing.Point(192, 717);
            this.lblDiagnosisCodeJ.Name = "lblDiagnosisCodeJ";
            this.lblDiagnosisCodeJ.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeJ.TabIndex = 399;
            // 
            // lblDiagnosisCodeK
            // 
            this.lblDiagnosisCodeK.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeK.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeK.Location = new System.Drawing.Point(333, 717);
            this.lblDiagnosisCodeK.Name = "lblDiagnosisCodeK";
            this.lblDiagnosisCodeK.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCodeK.TabIndex = 400;
            // 
            // lblDiagnosisCodeL
            // 
            this.lblDiagnosisCodeL.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCodeL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCodeL.ForeColor = System.Drawing.Color.Black;
            this.lblDiagnosisCodeL.Location = new System.Drawing.Point(472, 718);
            this.lblDiagnosisCodeL.Name = "lblDiagnosisCodeL";
            this.lblDiagnosisCodeL.Size = new System.Drawing.Size(68, 15);
            this.lblDiagnosisCodeL.TabIndex = 401;
            // 
            // lblReferringProviderQualifier
            // 
            this.lblReferringProviderQualifier.BackColor = System.Drawing.Color.Ivory;
            this.lblReferringProviderQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferringProviderQualifier.ForeColor = System.Drawing.Color.Black;
            this.lblReferringProviderQualifier.Location = new System.Drawing.Point(23, 609);
            this.lblReferringProviderQualifier.Name = "lblReferringProviderQualifier";
            this.lblReferringProviderQualifier.Size = new System.Drawing.Size(29, 15);
            this.lblReferringProviderQualifier.TabIndex = 393;
            this.lblReferringProviderQualifier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSimilarIllnessFirstDateQualifier
            // 
            this.lblSimilarIllnessFirstDateQualifier.BackColor = System.Drawing.Color.Ivory;
            this.lblSimilarIllnessFirstDateQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimilarIllnessFirstDateQualifier.ForeColor = System.Drawing.Color.Black;
            this.lblSimilarIllnessFirstDateQualifier.Location = new System.Drawing.Point(345, 572);
            this.lblSimilarIllnessFirstDateQualifier.Name = "lblSimilarIllnessFirstDateQualifier";
            this.lblSimilarIllnessFirstDateQualifier.Size = new System.Drawing.Size(38, 15);
            this.lblSimilarIllnessFirstDateQualifier.TabIndex = 391;
            this.lblSimilarIllnessFirstDateQualifier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateOfCurrentIllnessQualifier
            // 
            this.lblDateOfCurrentIllnessQualifier.BackColor = System.Drawing.Color.Ivory;
            this.lblDateOfCurrentIllnessQualifier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfCurrentIllnessQualifier.ForeColor = System.Drawing.Color.Black;
            this.lblDateOfCurrentIllnessQualifier.Location = new System.Drawing.Point(190, 573);
            this.lblDateOfCurrentIllnessQualifier.Name = "lblDateOfCurrentIllnessQualifier";
            this.lblDateOfCurrentIllnessQualifier.Size = new System.Drawing.Size(43, 15);
            this.lblDateOfCurrentIllnessQualifier.TabIndex = 392;
            this.lblDateOfCurrentIllnessQualifier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIcdInd1
            // 
            this.lblIcdInd1.BackColor = System.Drawing.Color.Ivory;
            this.lblIcdInd1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcdInd1.ForeColor = System.Drawing.Color.Black;
            this.lblIcdInd1.Location = new System.Drawing.Point(467, 666);
            this.lblIcdInd1.Name = "lblIcdInd1";
            this.lblIcdInd1.Size = new System.Drawing.Size(19, 15);
            this.lblIcdInd1.TabIndex = 389;
            this.lblIcdInd1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIcdInd2
            // 
            this.lblIcdInd2.BackColor = System.Drawing.Color.Ivory;
            this.lblIcdInd2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcdInd2.ForeColor = System.Drawing.Color.Black;
            this.lblIcdInd2.Location = new System.Drawing.Point(491, 666);
            this.lblIcdInd2.Name = "lblIcdInd2";
            this.lblIcdInd2.Size = new System.Drawing.Size(43, 15);
            this.lblIcdInd2.TabIndex = 390;
            this.lblIcdInd2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShadedEPSDT6
            // 
            this.lblShadedEPSDT6.BackColor = System.Drawing.Color.Ivory;
            this.lblShadedEPSDT6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadedEPSDT6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblShadedEPSDT6.Location = new System.Drawing.Point(692, 953);
            this.lblShadedEPSDT6.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT6.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblShadedEPSDT6.Name = "lblShadedEPSDT6";
            this.lblShadedEPSDT6.Size = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT6.TabIndex = 383;
            this.lblShadedEPSDT6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShadedEPSDT5
            // 
            this.lblShadedEPSDT5.BackColor = System.Drawing.Color.Ivory;
            this.lblShadedEPSDT5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadedEPSDT5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblShadedEPSDT5.Location = new System.Drawing.Point(692, 918);
            this.lblShadedEPSDT5.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT5.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblShadedEPSDT5.Name = "lblShadedEPSDT5";
            this.lblShadedEPSDT5.Size = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT5.TabIndex = 384;
            this.lblShadedEPSDT5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShadedEPSDT4
            // 
            this.lblShadedEPSDT4.BackColor = System.Drawing.Color.Ivory;
            this.lblShadedEPSDT4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadedEPSDT4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblShadedEPSDT4.Location = new System.Drawing.Point(692, 880);
            this.lblShadedEPSDT4.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT4.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblShadedEPSDT4.Name = "lblShadedEPSDT4";
            this.lblShadedEPSDT4.Size = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT4.TabIndex = 385;
            this.lblShadedEPSDT4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDTShaded3
            // 
            this.lblEPSDTShaded3.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDTShaded3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDTShaded3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDTShaded3.Location = new System.Drawing.Point(692, 846);
            this.lblEPSDTShaded3.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDTShaded3.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDTShaded3.Name = "lblEPSDTShaded3";
            this.lblEPSDTShaded3.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDTShaded3.TabIndex = 386;
            this.lblEPSDTShaded3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShadedEPSDT2
            // 
            this.lblShadedEPSDT2.BackColor = System.Drawing.Color.Ivory;
            this.lblShadedEPSDT2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadedEPSDT2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblShadedEPSDT2.Location = new System.Drawing.Point(692, 809);
            this.lblShadedEPSDT2.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT2.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblShadedEPSDT2.Name = "lblShadedEPSDT2";
            this.lblShadedEPSDT2.Size = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT2.TabIndex = 387;
            this.lblShadedEPSDT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShadedEPSDT1
            // 
            this.lblShadedEPSDT1.BackColor = System.Drawing.Color.Ivory;
            this.lblShadedEPSDT1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShadedEPSDT1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblShadedEPSDT1.Location = new System.Drawing.Point(692, 772);
            this.lblShadedEPSDT1.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT1.MinimumSize = new System.Drawing.Size(18, 15);
            this.lblShadedEPSDT1.Name = "lblShadedEPSDT1";
            this.lblShadedEPSDT1.Size = new System.Drawing.Size(22, 15);
            this.lblShadedEPSDT1.TabIndex = 382;
            this.lblShadedEPSDT1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhyscianQualifierValue
            // 
            this.lblPhyscianQualifierValue.BackColor = System.Drawing.Color.Ivory;
            this.lblPhyscianQualifierValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhyscianQualifierValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPhyscianQualifierValue.Location = new System.Drawing.Point(66, 1099);
            this.lblPhyscianQualifierValue.Name = "lblPhyscianQualifierValue";
            this.lblPhyscianQualifierValue.Size = new System.Drawing.Size(98, 15);
            this.lblPhyscianQualifierValue.TabIndex = 381;
            this.lblPhyscianQualifierValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReferringProvider_OtherType
            // 
            this.lblReferringProvider_OtherType.BackColor = System.Drawing.Color.Transparent;
            this.lblReferringProvider_OtherType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferringProvider_OtherType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReferringProvider_OtherType.Location = new System.Drawing.Point(342, 591);
            this.lblReferringProvider_OtherType.Name = "lblReferringProvider_OtherType";
            this.lblReferringProvider_OtherType.Size = new System.Drawing.Size(23, 15);
            this.lblReferringProvider_OtherType.TabIndex = 378;
            // 
            // lblReferringProvider_OtherValue
            // 
            this.lblReferringProvider_OtherValue.BackColor = System.Drawing.Color.Transparent;
            this.lblReferringProvider_OtherValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferringProvider_OtherValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReferringProvider_OtherValue.Location = new System.Drawing.Point(368, 592);
            this.lblReferringProvider_OtherValue.Name = "lblReferringProvider_OtherValue";
            this.lblReferringProvider_OtherValue.Size = new System.Drawing.Size(178, 14);
            this.lblReferringProvider_OtherValue.TabIndex = 377;
            this.lblReferringProvider_OtherValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider6_QFValue
            // 
            this.lblRenderingProvider6_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider6_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider6_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider6_QFValue.Location = new System.Drawing.Point(748, 953);
            this.lblRenderingProvider6_QFValue.Name = "lblRenderingProvider6_QFValue";
            this.lblRenderingProvider6_QFValue.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider6_QFValue.TabIndex = 376;
            this.lblRenderingProvider6_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider5_QFValue
            // 
            this.lblRenderingProvider5_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider5_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider5_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider5_QFValue.Location = new System.Drawing.Point(748, 918);
            this.lblRenderingProvider5_QFValue.Name = "lblRenderingProvider5_QFValue";
            this.lblRenderingProvider5_QFValue.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider5_QFValue.TabIndex = 375;
            this.lblRenderingProvider5_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider4_QFValue
            // 
            this.lblRenderingProvider4_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider4_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider4_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider4_QFValue.Location = new System.Drawing.Point(748, 880);
            this.lblRenderingProvider4_QFValue.Name = "lblRenderingProvider4_QFValue";
            this.lblRenderingProvider4_QFValue.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider4_QFValue.TabIndex = 374;
            this.lblRenderingProvider4_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider3_QFValue
            // 
            this.lblRenderingProvider3_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider3_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider3_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider3_QFValue.Location = new System.Drawing.Point(748, 845);
            this.lblRenderingProvider3_QFValue.Name = "lblRenderingProvider3_QFValue";
            this.lblRenderingProvider3_QFValue.Size = new System.Drawing.Size(128, 15);
            this.lblRenderingProvider3_QFValue.TabIndex = 373;
            this.lblRenderingProvider3_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider2_QFValue
            // 
            this.lblRenderingProvider2_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider2_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider2_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider2_QFValue.Location = new System.Drawing.Point(748, 809);
            this.lblRenderingProvider2_QFValue.Name = "lblRenderingProvider2_QFValue";
            this.lblRenderingProvider2_QFValue.Size = new System.Drawing.Size(128, 15);
            this.lblRenderingProvider2_QFValue.TabIndex = 372;
            this.lblRenderingProvider2_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider1_QFValue
            // 
            this.lblRenderingProvider1_QFValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider1_QFValue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider1_QFValue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider1_QFValue.Location = new System.Drawing.Point(748, 772);
            this.lblRenderingProvider1_QFValue.Name = "lblRenderingProvider1_QFValue";
            this.lblRenderingProvider1_QFValue.Size = new System.Drawing.Size(128, 15);
            this.lblRenderingProvider1_QFValue.TabIndex = 371;
            this.lblRenderingProvider1_QFValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider6_QF
            // 
            this.lblRenderingProvider6_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider6_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider6_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider6_QF.Location = new System.Drawing.Point(716, 953);
            this.lblRenderingProvider6_QF.Name = "lblRenderingProvider6_QF";
            this.lblRenderingProvider6_QF.Size = new System.Drawing.Size(30, 16);
            this.lblRenderingProvider6_QF.TabIndex = 370;
            this.lblRenderingProvider6_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider5_QF
            // 
            this.lblRenderingProvider5_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider5_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider5_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider5_QF.Location = new System.Drawing.Point(716, 916);
            this.lblRenderingProvider5_QF.Name = "lblRenderingProvider5_QF";
            this.lblRenderingProvider5_QF.Size = new System.Drawing.Size(30, 15);
            this.lblRenderingProvider5_QF.TabIndex = 369;
            this.lblRenderingProvider5_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider4_QF
            // 
            this.lblRenderingProvider4_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider4_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider4_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider4_QF.Location = new System.Drawing.Point(716, 880);
            this.lblRenderingProvider4_QF.Name = "lblRenderingProvider4_QF";
            this.lblRenderingProvider4_QF.Size = new System.Drawing.Size(30, 15);
            this.lblRenderingProvider4_QF.TabIndex = 368;
            this.lblRenderingProvider4_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider3_QF
            // 
            this.lblRenderingProvider3_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider3_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider3_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider3_QF.Location = new System.Drawing.Point(716, 844);
            this.lblRenderingProvider3_QF.Name = "lblRenderingProvider3_QF";
            this.lblRenderingProvider3_QF.Size = new System.Drawing.Size(30, 15);
            this.lblRenderingProvider3_QF.TabIndex = 367;
            this.lblRenderingProvider3_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider2_QF
            // 
            this.lblRenderingProvider2_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider2_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider2_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider2_QF.Location = new System.Drawing.Point(716, 807);
            this.lblRenderingProvider2_QF.Name = "lblRenderingProvider2_QF";
            this.lblRenderingProvider2_QF.Size = new System.Drawing.Size(30, 15);
            this.lblRenderingProvider2_QF.TabIndex = 366;
            this.lblRenderingProvider2_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider1_QF
            // 
            this.lblRenderingProvider1_QF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblRenderingProvider1_QF.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRenderingProvider1_QF.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider1_QF.Location = new System.Drawing.Point(716, 772);
            this.lblRenderingProvider1_QF.Margin = new System.Windows.Forms.Padding(0);
            this.lblRenderingProvider1_QF.Name = "lblRenderingProvider1_QF";
            this.lblRenderingProvider1_QF.Size = new System.Drawing.Size(30, 15);
            this.lblRenderingProvider1_QF.TabIndex = 365;
            this.lblRenderingProvider1_QF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountPaid1
            // 
            this.lblAmountPaid1.BackColor = System.Drawing.Color.Ivory;
            this.lblAmountPaid1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountPaid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblAmountPaid1.Location = new System.Drawing.Point(753, 1006);
            this.lblAmountPaid1.Name = "lblAmountPaid1";
            this.lblAmountPaid1.Size = new System.Drawing.Size(23, 15);
            this.lblAmountPaid1.TabIndex = 364;
            this.lblAmountPaid1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNotes6
            // 
            this.lblNotes6.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes6.Location = new System.Drawing.Point(22, 954);
            this.lblNotes6.Name = "lblNotes6";
            this.lblNotes6.Size = new System.Drawing.Size(667, 15);
            this.lblNotes6.TabIndex = 363;
            // 
            // lblNotes5
            // 
            this.lblNotes5.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes5.Location = new System.Drawing.Point(22, 918);
            this.lblNotes5.Name = "lblNotes5";
            this.lblNotes5.Size = new System.Drawing.Size(667, 15);
            this.lblNotes5.TabIndex = 362;
            // 
            // lblNotes4
            // 
            this.lblNotes4.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes4.Location = new System.Drawing.Point(22, 881);
            this.lblNotes4.Name = "lblNotes4";
            this.lblNotes4.Size = new System.Drawing.Size(667, 15);
            this.lblNotes4.TabIndex = 361;
            // 
            // lblNotes3
            // 
            this.lblNotes3.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes3.Location = new System.Drawing.Point(22, 846);
            this.lblNotes3.Name = "lblNotes3";
            this.lblNotes3.Size = new System.Drawing.Size(667, 15);
            this.lblNotes3.TabIndex = 360;
            // 
            // lblNotes2
            // 
            this.lblNotes2.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes2.Location = new System.Drawing.Point(22, 809);
            this.lblNotes2.Name = "lblNotes2";
            this.lblNotes2.Size = new System.Drawing.Size(667, 15);
            this.lblNotes2.TabIndex = 359;
            // 
            // btn_BrowseFacility
            // 
            this.btn_BrowseFacility.BackColor = System.Drawing.Color.Transparent;
            this.btn_BrowseFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowseFacility.BackgroundImage")));
            this.btn_BrowseFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowseFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseFacility.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowseFacility.Image")));
            this.btn_BrowseFacility.Location = new System.Drawing.Point(530, 1098);
            this.btn_BrowseFacility.Name = "btn_BrowseFacility";
            this.btn_BrowseFacility.Size = new System.Drawing.Size(22, 17);
            this.btn_BrowseFacility.TabIndex = 358;
            this.btn_BrowseFacility.UseVisualStyleBackColor = false;
            // 
            // lblBillingProv_b_UPIN
            // 
            this.lblBillingProv_b_UPIN.BackColor = System.Drawing.Color.Ivory;
            this.lblBillingProv_b_UPIN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingProv_b_UPIN.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBillingProv_b_UPIN.Location = new System.Drawing.Point(690, 1098);
            this.lblBillingProv_b_UPIN.Name = "lblBillingProv_b_UPIN";
            this.lblBillingProv_b_UPIN.Size = new System.Drawing.Size(188, 17);
            this.lblBillingProv_b_UPIN.TabIndex = 357;
            this.lblBillingProv_b_UPIN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillingProv_a_NPI
            // 
            this.lblBillingProv_a_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblBillingProv_a_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingProv_a_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBillingProv_a_NPI.Location = new System.Drawing.Point(569, 1098);
            this.lblBillingProv_a_NPI.Name = "lblBillingProv_a_NPI";
            this.lblBillingProv_a_NPI.Size = new System.Drawing.Size(102, 17);
            this.lblBillingProv_a_NPI.TabIndex = 356;
            this.lblBillingProv_a_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBillingProviderInfo
            // 
            this.lblBillingProviderInfo.BackColor = System.Drawing.Color.Ivory;
            this.lblBillingProviderInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingProviderInfo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBillingProviderInfo.Location = new System.Drawing.Point(559, 1047);
            this.lblBillingProviderInfo.Name = "lblBillingProviderInfo";
            this.lblBillingProviderInfo.Size = new System.Drawing.Size(317, 45);
            this.lblBillingProviderInfo.TabIndex = 355;
            this.lblBillingProviderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFacility_b
            // 
            this.lblFacility_b.BackColor = System.Drawing.Color.Ivory;
            this.lblFacility_b.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacility_b.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblFacility_b.Location = new System.Drawing.Point(394, 1098);
            this.lblFacility_b.Name = "lblFacility_b";
            this.lblFacility_b.Size = new System.Drawing.Size(133, 17);
            this.lblFacility_b.TabIndex = 354;
            this.lblFacility_b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFacility_a_NPI
            // 
            this.lblFacility_a_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblFacility_a_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacility_a_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblFacility_a_NPI.Location = new System.Drawing.Point(273, 1098);
            this.lblFacility_a_NPI.Name = "lblFacility_a_NPI";
            this.lblFacility_a_NPI.Size = new System.Drawing.Size(107, 17);
            this.lblFacility_a_NPI.TabIndex = 353;
            this.lblFacility_a_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFacilityInfo
            // 
            this.lblFacilityInfo.BackColor = System.Drawing.Color.Ivory;
            this.lblFacilityInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacilityInfo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblFacilityInfo.Location = new System.Drawing.Point(262, 1041);
            this.lblFacilityInfo.Name = "lblFacilityInfo";
            this.lblFacilityInfo.Size = new System.Drawing.Size(285, 50);
            this.lblFacilityInfo.TabIndex = 350;
            this.lblFacilityInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhysicianSignDate_YY
            // 
            this.lblPhysicianSignDate_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblPhysicianSignDate_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysicianSignDate_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPhysicianSignDate_YY.Location = new System.Drawing.Point(228, 1136);
            this.lblPhysicianSignDate_YY.Name = "lblPhysicianSignDate_YY";
            this.lblPhysicianSignDate_YY.Size = new System.Drawing.Size(25, 14);
            this.lblPhysicianSignDate_YY.TabIndex = 349;
            this.lblPhysicianSignDate_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhysicianSignDate_DD
            // 
            this.lblPhysicianSignDate_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblPhysicianSignDate_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysicianSignDate_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPhysicianSignDate_DD.Location = new System.Drawing.Point(202, 1136);
            this.lblPhysicianSignDate_DD.Name = "lblPhysicianSignDate_DD";
            this.lblPhysicianSignDate_DD.Size = new System.Drawing.Size(25, 14);
            this.lblPhysicianSignDate_DD.TabIndex = 348;
            this.lblPhysicianSignDate_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhysicianSignDate_MM
            // 
            this.lblPhysicianSignDate_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblPhysicianSignDate_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhysicianSignDate_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPhysicianSignDate_MM.Location = new System.Drawing.Point(177, 1087);
            this.lblPhysicianSignDate_MM.Name = "lblPhysicianSignDate_MM";
            this.lblPhysicianSignDate_MM.Size = new System.Drawing.Size(73, 14);
            this.lblPhysicianSignDate_MM.TabIndex = 347;
            this.lblPhysicianSignDate_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhyscianSignature
            // 
            this.lblPhyscianSignature.BackColor = System.Drawing.Color.Ivory;
            this.lblPhyscianSignature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhyscianSignature.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPhyscianSignature.Location = new System.Drawing.Point(26, 1072);
            this.lblPhyscianSignature.Name = "lblPhyscianSignature";
            this.lblPhyscianSignature.Size = new System.Drawing.Size(228, 20);
            this.lblPhyscianSignature.TabIndex = 346;
            this.lblPhyscianSignature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBalanceDue2
            // 
            this.lblBalanceDue2.BackColor = System.Drawing.Color.Ivory;
            this.lblBalanceDue2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceDue2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBalanceDue2.Location = new System.Drawing.Point(853, 1006);
            this.lblBalanceDue2.Name = "lblBalanceDue2";
            this.lblBalanceDue2.Size = new System.Drawing.Size(25, 15);
            this.lblBalanceDue2.TabIndex = 345;
            this.lblBalanceDue2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBalanceDue
            // 
            this.lblBalanceDue.BackColor = System.Drawing.Color.Ivory;
            this.lblBalanceDue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceDue.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblBalanceDue.Location = new System.Drawing.Point(784, 1006);
            this.lblBalanceDue.Name = "lblBalanceDue";
            this.lblBalanceDue.Size = new System.Drawing.Size(60, 15);
            this.lblBalanceDue.TabIndex = 344;
            this.lblBalanceDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.BackColor = System.Drawing.Color.Ivory;
            this.lblAmountPaid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblAmountPaid.Location = new System.Drawing.Point(693, 1006);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(57, 15);
            this.lblAmountPaid.TabIndex = 343;
            this.lblAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCharges2
            // 
            this.lblTotalCharges2.BackColor = System.Drawing.Color.Ivory;
            this.lblTotalCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCharges2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTotalCharges2.Location = new System.Drawing.Point(648, 1006);
            this.lblTotalCharges2.Name = "lblTotalCharges2";
            this.lblTotalCharges2.Size = new System.Drawing.Size(25, 15);
            this.lblTotalCharges2.TabIndex = 342;
            this.lblTotalCharges2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalCharges
            // 
            this.lblTotalCharges.BackColor = System.Drawing.Color.Ivory;
            this.lblTotalCharges.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCharges.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTotalCharges.Location = new System.Drawing.Point(571, 1006);
            this.lblTotalCharges.Name = "lblTotalCharges";
            this.lblTotalCharges.Size = new System.Drawing.Size(71, 15);
            this.lblTotalCharges.TabIndex = 341;
            this.lblTotalCharges.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAcceptAssignment_No
            // 
            this.lblAcceptAssignment_No.BackColor = System.Drawing.Color.Ivory;
            this.lblAcceptAssignment_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAcceptAssignment_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcceptAssignment_No.ForeColor = System.Drawing.Color.Black;
            this.lblAcceptAssignment_No.Location = new System.Drawing.Point(478, 1006);
            this.lblAcceptAssignment_No.Name = "lblAcceptAssignment_No";
            this.lblAcceptAssignment_No.Size = new System.Drawing.Size(18, 18);
            this.lblAcceptAssignment_No.TabIndex = 340;
            this.lblAcceptAssignment_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAcceptAssignment_Yes
            // 
            this.lblAcceptAssignment_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblAcceptAssignment_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAcceptAssignment_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcceptAssignment_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblAcceptAssignment_Yes.Location = new System.Drawing.Point(424, 1006);
            this.lblAcceptAssignment_Yes.Name = "lblAcceptAssignment_Yes";
            this.lblAcceptAssignment_Yes.Size = new System.Drawing.Size(18, 18);
            this.lblAcceptAssignment_Yes.TabIndex = 339;
            this.lblAcceptAssignment_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientAccountNo
            // 
            this.lblPatientAccountNo.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientAccountNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientAccountNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientAccountNo.Location = new System.Drawing.Point(269, 1006);
            this.lblPatientAccountNo.Name = "lblPatientAccountNo";
            this.lblPatientAccountNo.Size = new System.Drawing.Size(143, 15);
            this.lblPatientAccountNo.TabIndex = 338;
            this.lblPatientAccountNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFederalTaxID_EIN
            // 
            this.lblFederalTaxID_EIN.BackColor = System.Drawing.Color.Ivory;
            this.lblFederalTaxID_EIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFederalTaxID_EIN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFederalTaxID_EIN.ForeColor = System.Drawing.Color.Black;
            this.lblFederalTaxID_EIN.Location = new System.Drawing.Point(218, 1005);
            this.lblFederalTaxID_EIN.Name = "lblFederalTaxID_EIN";
            this.lblFederalTaxID_EIN.Size = new System.Drawing.Size(18, 19);
            this.lblFederalTaxID_EIN.TabIndex = 337;
            this.lblFederalTaxID_EIN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFederalTaxID_SSN
            // 
            this.lblFederalTaxID_SSN.BackColor = System.Drawing.Color.Ivory;
            this.lblFederalTaxID_SSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFederalTaxID_SSN.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFederalTaxID_SSN.ForeColor = System.Drawing.Color.Black;
            this.lblFederalTaxID_SSN.Location = new System.Drawing.Point(196, 1005);
            this.lblFederalTaxID_SSN.Name = "lblFederalTaxID_SSN";
            this.lblFederalTaxID_SSN.Size = new System.Drawing.Size(18, 19);
            this.lblFederalTaxID_SSN.TabIndex = 336;
            this.lblFederalTaxID_SSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFederalTaxID
            // 
            this.lblFederalTaxID.BackColor = System.Drawing.Color.Ivory;
            this.lblFederalTaxID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFederalTaxID.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblFederalTaxID.Location = new System.Drawing.Point(42, 1006);
            this.lblFederalTaxID.Name = "lblFederalTaxID";
            this.lblFederalTaxID.Size = new System.Drawing.Size(143, 15);
            this.lblFederalTaxID.TabIndex = 335;
            this.lblFederalTaxID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider6_NPI
            // 
            this.lblRenderingProvider6_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider6_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider6_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider6_NPI.Location = new System.Drawing.Point(748, 971);
            this.lblRenderingProvider6_NPI.Name = "lblRenderingProvider6_NPI";
            this.lblRenderingProvider6_NPI.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider6_NPI.TabIndex = 324;
            this.lblRenderingProvider6_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT6
            // 
            this.lblEPSDT6.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT6.Location = new System.Drawing.Point(692, 971);
            this.lblEPSDT6.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT6.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT6.Name = "lblEPSDT6";
            this.lblEPSDT6.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT6.TabIndex = 328;
            this.lblEPSDT6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits6
            // 
            this.lblUnits6.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits6.Location = new System.Drawing.Point(650, 971);
            this.lblUnits6.Name = "lblUnits6";
            this.lblUnits6.Size = new System.Drawing.Size(40, 16);
            this.lblUnits6.TabIndex = 329;
            this.lblUnits6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges61
            // 
            this.lblCharges61.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges61.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges61.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges61.Location = new System.Drawing.Point(622, 971);
            this.lblCharges61.Name = "lblCharges61";
            this.lblCharges61.Size = new System.Drawing.Size(25, 16);
            this.lblCharges61.TabIndex = 326;
            this.lblCharges61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges6
            // 
            this.lblCharges6.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges6.Location = new System.Drawing.Point(560, 971);
            this.lblCharges6.Name = "lblCharges6";
            this.lblCharges6.Size = new System.Drawing.Size(60, 16);
            this.lblCharges6.TabIndex = 327;
            this.lblCharges6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr6
            // 
            this.lblDxPtr6.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr6.Location = new System.Drawing.Point(499, 973);
            this.lblDxPtr6.Name = "lblDxPtr6";
            this.lblDxPtr6.Size = new System.Drawing.Size(48, 13);
            this.lblDxPtr6.TabIndex = 330;
            this.lblDxPtr6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD64
            // 
            this.lblMOD64.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD64.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD64.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD64.Location = new System.Drawing.Point(464, 971);
            this.lblMOD64.Name = "lblMOD64";
            this.lblMOD64.Size = new System.Drawing.Size(30, 14);
            this.lblMOD64.TabIndex = 333;
            this.lblMOD64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD63
            // 
            this.lblMOD63.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD63.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD63.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD63.Location = new System.Drawing.Point(433, 971);
            this.lblMOD63.Name = "lblMOD63";
            this.lblMOD63.Size = new System.Drawing.Size(30, 14);
            this.lblMOD63.TabIndex = 334;
            this.lblMOD63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD62
            // 
            this.lblMOD62.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD62.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD62.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD62.Location = new System.Drawing.Point(400, 971);
            this.lblMOD62.Name = "lblMOD62";
            this.lblMOD62.Size = new System.Drawing.Size(30, 14);
            this.lblMOD62.TabIndex = 331;
            this.lblMOD62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD61
            // 
            this.lblMOD61.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD61.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD61.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD61.Location = new System.Drawing.Point(363, 970);
            this.lblMOD61.Name = "lblMOD61";
            this.lblMOD61.Size = new System.Drawing.Size(33, 16);
            this.lblMOD61.TabIndex = 332;
            this.lblMOD61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT6
            // 
            this.lblCPT6.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT6.Location = new System.Drawing.Point(287, 970);
            this.lblCPT6.Name = "lblCPT6";
            this.lblCPT6.Size = new System.Drawing.Size(71, 17);
            this.lblCPT6.TabIndex = 323;
            this.lblCPT6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG6
            // 
            this.lblEMG6.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG6.Location = new System.Drawing.Point(252, 971);
            this.lblEMG6.Name = "lblEMG6";
            this.lblEMG6.Size = new System.Drawing.Size(26, 14);
            this.lblEMG6.TabIndex = 322;
            this.lblEMG6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS6
            // 
            this.lblPOS6.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS6.Location = new System.Drawing.Point(214, 971);
            this.lblPOS6.Name = "lblPOS6";
            this.lblPOS6.Size = new System.Drawing.Size(31, 14);
            this.lblPOS6.TabIndex = 325;
            this.lblPOS6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6To_YY
            // 
            this.lblDOS6To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6To_YY.Location = new System.Drawing.Point(185, 971);
            this.lblDOS6To_YY.Name = "lblDOS6To_YY";
            this.lblDOS6To_YY.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6To_YY.TabIndex = 321;
            this.lblDOS6To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6To_DD
            // 
            this.lblDOS6To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6To_DD.Location = new System.Drawing.Point(153, 971);
            this.lblDOS6To_DD.Name = "lblDOS6To_DD";
            this.lblDOS6To_DD.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6To_DD.TabIndex = 320;
            this.lblDOS6To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6To_MM
            // 
            this.lblDOS6To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6To_MM.Location = new System.Drawing.Point(119, 971);
            this.lblDOS6To_MM.Name = "lblDOS6To_MM";
            this.lblDOS6To_MM.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6To_MM.TabIndex = 319;
            this.lblDOS6To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6From_YY
            // 
            this.lblDOS6From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6From_YY.Location = new System.Drawing.Point(87, 971);
            this.lblDOS6From_YY.Name = "lblDOS6From_YY";
            this.lblDOS6From_YY.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6From_YY.TabIndex = 318;
            this.lblDOS6From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6From_DD
            // 
            this.lblDOS6From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6From_DD.Location = new System.Drawing.Point(55, 971);
            this.lblDOS6From_DD.Name = "lblDOS6From_DD";
            this.lblDOS6From_DD.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6From_DD.TabIndex = 317;
            this.lblDOS6From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS6From_MM
            // 
            this.lblDOS6From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS6From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS6From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS6From_MM.Location = new System.Drawing.Point(23, 971);
            this.lblDOS6From_MM.Name = "lblDOS6From_MM";
            this.lblDOS6From_MM.Size = new System.Drawing.Size(25, 13);
            this.lblDOS6From_MM.TabIndex = 316;
            this.lblDOS6From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider5_NPI
            // 
            this.lblRenderingProvider5_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider5_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider5_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider5_NPI.Location = new System.Drawing.Point(748, 936);
            this.lblRenderingProvider5_NPI.Name = "lblRenderingProvider5_NPI";
            this.lblRenderingProvider5_NPI.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider5_NPI.TabIndex = 305;
            this.lblRenderingProvider5_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT5
            // 
            this.lblEPSDT5.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT5.Location = new System.Drawing.Point(692, 936);
            this.lblEPSDT5.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT5.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT5.Name = "lblEPSDT5";
            this.lblEPSDT5.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT5.TabIndex = 309;
            this.lblEPSDT5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits5
            // 
            this.lblUnits5.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits5.Location = new System.Drawing.Point(650, 936);
            this.lblUnits5.Name = "lblUnits5";
            this.lblUnits5.Size = new System.Drawing.Size(40, 16);
            this.lblUnits5.TabIndex = 310;
            this.lblUnits5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges51
            // 
            this.lblCharges51.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges51.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges51.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges51.Location = new System.Drawing.Point(622, 936);
            this.lblCharges51.Name = "lblCharges51";
            this.lblCharges51.Size = new System.Drawing.Size(25, 16);
            this.lblCharges51.TabIndex = 307;
            this.lblCharges51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges5
            // 
            this.lblCharges5.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges5.Location = new System.Drawing.Point(560, 936);
            this.lblCharges5.Name = "lblCharges5";
            this.lblCharges5.Size = new System.Drawing.Size(60, 16);
            this.lblCharges5.TabIndex = 308;
            this.lblCharges5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr5
            // 
            this.lblDxPtr5.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr5.Location = new System.Drawing.Point(498, 937);
            this.lblDxPtr5.Name = "lblDxPtr5";
            this.lblDxPtr5.Size = new System.Drawing.Size(50, 13);
            this.lblDxPtr5.TabIndex = 311;
            this.lblDxPtr5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD54
            // 
            this.lblMOD54.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD54.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD54.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD54.Location = new System.Drawing.Point(464, 936);
            this.lblMOD54.Name = "lblMOD54";
            this.lblMOD54.Size = new System.Drawing.Size(30, 16);
            this.lblMOD54.TabIndex = 314;
            this.lblMOD54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD53
            // 
            this.lblMOD53.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD53.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD53.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD53.Location = new System.Drawing.Point(433, 936);
            this.lblMOD53.Name = "lblMOD53";
            this.lblMOD53.Size = new System.Drawing.Size(30, 16);
            this.lblMOD53.TabIndex = 315;
            this.lblMOD53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD52
            // 
            this.lblMOD52.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD52.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD52.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD52.Location = new System.Drawing.Point(400, 936);
            this.lblMOD52.Name = "lblMOD52";
            this.lblMOD52.Size = new System.Drawing.Size(30, 16);
            this.lblMOD52.TabIndex = 312;
            this.lblMOD52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD51
            // 
            this.lblMOD51.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD51.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD51.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD51.Location = new System.Drawing.Point(363, 936);
            this.lblMOD51.Name = "lblMOD51";
            this.lblMOD51.Size = new System.Drawing.Size(33, 16);
            this.lblMOD51.TabIndex = 313;
            this.lblMOD51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT5
            // 
            this.lblCPT5.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT5.Location = new System.Drawing.Point(287, 935);
            this.lblCPT5.Name = "lblCPT5";
            this.lblCPT5.Size = new System.Drawing.Size(71, 17);
            this.lblCPT5.TabIndex = 304;
            this.lblCPT5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG5
            // 
            this.lblEMG5.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG5.Location = new System.Drawing.Point(252, 936);
            this.lblEMG5.Name = "lblEMG5";
            this.lblEMG5.Size = new System.Drawing.Size(26, 16);
            this.lblEMG5.TabIndex = 303;
            this.lblEMG5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS5
            // 
            this.lblPOS5.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS5.Location = new System.Drawing.Point(214, 936);
            this.lblPOS5.Name = "lblPOS5";
            this.lblPOS5.Size = new System.Drawing.Size(31, 16);
            this.lblPOS5.TabIndex = 306;
            this.lblPOS5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5To_YY
            // 
            this.lblDOS5To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5To_YY.Location = new System.Drawing.Point(185, 936);
            this.lblDOS5To_YY.Name = "lblDOS5To_YY";
            this.lblDOS5To_YY.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5To_YY.TabIndex = 302;
            this.lblDOS5To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5To_DD
            // 
            this.lblDOS5To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5To_DD.Location = new System.Drawing.Point(153, 936);
            this.lblDOS5To_DD.Name = "lblDOS5To_DD";
            this.lblDOS5To_DD.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5To_DD.TabIndex = 301;
            this.lblDOS5To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5To_MM
            // 
            this.lblDOS5To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5To_MM.Location = new System.Drawing.Point(119, 936);
            this.lblDOS5To_MM.Name = "lblDOS5To_MM";
            this.lblDOS5To_MM.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5To_MM.TabIndex = 300;
            this.lblDOS5To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5From_YY
            // 
            this.lblDOS5From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5From_YY.Location = new System.Drawing.Point(87, 936);
            this.lblDOS5From_YY.Name = "lblDOS5From_YY";
            this.lblDOS5From_YY.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5From_YY.TabIndex = 299;
            this.lblDOS5From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5From_DD
            // 
            this.lblDOS5From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5From_DD.Location = new System.Drawing.Point(55, 936);
            this.lblDOS5From_DD.Name = "lblDOS5From_DD";
            this.lblDOS5From_DD.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5From_DD.TabIndex = 298;
            this.lblDOS5From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS5From_MM
            // 
            this.lblDOS5From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS5From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS5From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS5From_MM.Location = new System.Drawing.Point(23, 936);
            this.lblDOS5From_MM.Name = "lblDOS5From_MM";
            this.lblDOS5From_MM.Size = new System.Drawing.Size(25, 15);
            this.lblDOS5From_MM.TabIndex = 297;
            this.lblDOS5From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider4_NPI
            // 
            this.lblRenderingProvider4_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider4_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider4_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider4_NPI.Location = new System.Drawing.Point(748, 899);
            this.lblRenderingProvider4_NPI.Name = "lblRenderingProvider4_NPI";
            this.lblRenderingProvider4_NPI.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider4_NPI.TabIndex = 286;
            this.lblRenderingProvider4_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT4
            // 
            this.lblEPSDT4.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT4.Location = new System.Drawing.Point(692, 899);
            this.lblEPSDT4.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT4.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT4.Name = "lblEPSDT4";
            this.lblEPSDT4.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT4.TabIndex = 290;
            this.lblEPSDT4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits4
            // 
            this.lblUnits4.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits4.Location = new System.Drawing.Point(650, 899);
            this.lblUnits4.Name = "lblUnits4";
            this.lblUnits4.Size = new System.Drawing.Size(40, 16);
            this.lblUnits4.TabIndex = 291;
            this.lblUnits4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges41
            // 
            this.lblCharges41.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges41.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges41.Location = new System.Drawing.Point(622, 899);
            this.lblCharges41.Name = "lblCharges41";
            this.lblCharges41.Size = new System.Drawing.Size(25, 16);
            this.lblCharges41.TabIndex = 288;
            this.lblCharges41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges4
            // 
            this.lblCharges4.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges4.Location = new System.Drawing.Point(560, 899);
            this.lblCharges4.Name = "lblCharges4";
            this.lblCharges4.Size = new System.Drawing.Size(60, 16);
            this.lblCharges4.TabIndex = 289;
            this.lblCharges4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr4
            // 
            this.lblDxPtr4.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr4.Location = new System.Drawing.Point(498, 900);
            this.lblDxPtr4.Name = "lblDxPtr4";
            this.lblDxPtr4.Size = new System.Drawing.Size(50, 13);
            this.lblDxPtr4.TabIndex = 292;
            this.lblDxPtr4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD44
            // 
            this.lblMOD44.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD44.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD44.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD44.Location = new System.Drawing.Point(464, 899);
            this.lblMOD44.Name = "lblMOD44";
            this.lblMOD44.Size = new System.Drawing.Size(30, 16);
            this.lblMOD44.TabIndex = 295;
            this.lblMOD44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD43
            // 
            this.lblMOD43.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD43.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD43.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD43.Location = new System.Drawing.Point(433, 899);
            this.lblMOD43.Name = "lblMOD43";
            this.lblMOD43.Size = new System.Drawing.Size(30, 16);
            this.lblMOD43.TabIndex = 296;
            this.lblMOD43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD42
            // 
            this.lblMOD42.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD42.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD42.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD42.Location = new System.Drawing.Point(400, 899);
            this.lblMOD42.Name = "lblMOD42";
            this.lblMOD42.Size = new System.Drawing.Size(30, 16);
            this.lblMOD42.TabIndex = 293;
            this.lblMOD42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD41
            // 
            this.lblMOD41.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD41.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD41.Location = new System.Drawing.Point(363, 899);
            this.lblMOD41.Name = "lblMOD41";
            this.lblMOD41.Size = new System.Drawing.Size(33, 16);
            this.lblMOD41.TabIndex = 294;
            this.lblMOD41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT4
            // 
            this.lblCPT4.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT4.Location = new System.Drawing.Point(287, 899);
            this.lblCPT4.Name = "lblCPT4";
            this.lblCPT4.Size = new System.Drawing.Size(71, 17);
            this.lblCPT4.TabIndex = 285;
            this.lblCPT4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG4
            // 
            this.lblEMG4.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG4.Location = new System.Drawing.Point(252, 899);
            this.lblEMG4.Name = "lblEMG4";
            this.lblEMG4.Size = new System.Drawing.Size(26, 16);
            this.lblEMG4.TabIndex = 284;
            this.lblEMG4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS4
            // 
            this.lblPOS4.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS4.Location = new System.Drawing.Point(214, 899);
            this.lblPOS4.Name = "lblPOS4";
            this.lblPOS4.Size = new System.Drawing.Size(31, 16);
            this.lblPOS4.TabIndex = 287;
            this.lblPOS4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4To_YY
            // 
            this.lblDOS4To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4To_YY.Location = new System.Drawing.Point(185, 899);
            this.lblDOS4To_YY.Name = "lblDOS4To_YY";
            this.lblDOS4To_YY.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4To_YY.TabIndex = 283;
            this.lblDOS4To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4To_DD
            // 
            this.lblDOS4To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4To_DD.Location = new System.Drawing.Point(153, 899);
            this.lblDOS4To_DD.Name = "lblDOS4To_DD";
            this.lblDOS4To_DD.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4To_DD.TabIndex = 282;
            this.lblDOS4To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4To_MM
            // 
            this.lblDOS4To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4To_MM.Location = new System.Drawing.Point(119, 899);
            this.lblDOS4To_MM.Name = "lblDOS4To_MM";
            this.lblDOS4To_MM.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4To_MM.TabIndex = 281;
            this.lblDOS4To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4From_YY
            // 
            this.lblDOS4From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4From_YY.Location = new System.Drawing.Point(87, 899);
            this.lblDOS4From_YY.Name = "lblDOS4From_YY";
            this.lblDOS4From_YY.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4From_YY.TabIndex = 280;
            this.lblDOS4From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4From_DD
            // 
            this.lblDOS4From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4From_DD.Location = new System.Drawing.Point(55, 899);
            this.lblDOS4From_DD.Name = "lblDOS4From_DD";
            this.lblDOS4From_DD.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4From_DD.TabIndex = 279;
            this.lblDOS4From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS4From_MM
            // 
            this.lblDOS4From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS4From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS4From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS4From_MM.Location = new System.Drawing.Point(23, 899);
            this.lblDOS4From_MM.Name = "lblDOS4From_MM";
            this.lblDOS4From_MM.Size = new System.Drawing.Size(25, 15);
            this.lblDOS4From_MM.TabIndex = 278;
            this.lblDOS4From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider3_NPI
            // 
            this.lblRenderingProvider3_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider3_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider3_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider3_NPI.Location = new System.Drawing.Point(748, 863);
            this.lblRenderingProvider3_NPI.Name = "lblRenderingProvider3_NPI";
            this.lblRenderingProvider3_NPI.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider3_NPI.TabIndex = 267;
            this.lblRenderingProvider3_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT3
            // 
            this.lblEPSDT3.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT3.Location = new System.Drawing.Point(692, 863);
            this.lblEPSDT3.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT3.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT3.Name = "lblEPSDT3";
            this.lblEPSDT3.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT3.TabIndex = 271;
            this.lblEPSDT3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits3
            // 
            this.lblUnits3.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits3.Location = new System.Drawing.Point(650, 863);
            this.lblUnits3.Name = "lblUnits3";
            this.lblUnits3.Size = new System.Drawing.Size(40, 16);
            this.lblUnits3.TabIndex = 272;
            this.lblUnits3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges31
            // 
            this.lblCharges31.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges31.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges31.Location = new System.Drawing.Point(622, 863);
            this.lblCharges31.Name = "lblCharges31";
            this.lblCharges31.Size = new System.Drawing.Size(25, 16);
            this.lblCharges31.TabIndex = 269;
            this.lblCharges31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges3
            // 
            this.lblCharges3.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges3.Location = new System.Drawing.Point(560, 863);
            this.lblCharges3.Name = "lblCharges3";
            this.lblCharges3.Size = new System.Drawing.Size(60, 16);
            this.lblCharges3.TabIndex = 270;
            this.lblCharges3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr3
            // 
            this.lblDxPtr3.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr3.Location = new System.Drawing.Point(498, 864);
            this.lblDxPtr3.Name = "lblDxPtr3";
            this.lblDxPtr3.Size = new System.Drawing.Size(50, 13);
            this.lblDxPtr3.TabIndex = 273;
            this.lblDxPtr3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD34
            // 
            this.lblMOD34.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD34.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD34.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD34.Location = new System.Drawing.Point(464, 863);
            this.lblMOD34.Name = "lblMOD34";
            this.lblMOD34.Size = new System.Drawing.Size(30, 15);
            this.lblMOD34.TabIndex = 276;
            this.lblMOD34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD33
            // 
            this.lblMOD33.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD33.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD33.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD33.Location = new System.Drawing.Point(433, 863);
            this.lblMOD33.Name = "lblMOD33";
            this.lblMOD33.Size = new System.Drawing.Size(30, 15);
            this.lblMOD33.TabIndex = 277;
            this.lblMOD33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD32
            // 
            this.lblMOD32.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD32.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD32.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD32.Location = new System.Drawing.Point(400, 863);
            this.lblMOD32.Name = "lblMOD32";
            this.lblMOD32.Size = new System.Drawing.Size(30, 15);
            this.lblMOD32.TabIndex = 274;
            this.lblMOD32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD31
            // 
            this.lblMOD31.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD31.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD31.Location = new System.Drawing.Point(363, 863);
            this.lblMOD31.Name = "lblMOD31";
            this.lblMOD31.Size = new System.Drawing.Size(33, 16);
            this.lblMOD31.TabIndex = 275;
            this.lblMOD31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT3
            // 
            this.lblCPT3.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT3.Location = new System.Drawing.Point(287, 862);
            this.lblCPT3.Name = "lblCPT3";
            this.lblCPT3.Size = new System.Drawing.Size(71, 17);
            this.lblCPT3.TabIndex = 266;
            this.lblCPT3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG3
            // 
            this.lblEMG3.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG3.Location = new System.Drawing.Point(252, 863);
            this.lblEMG3.Name = "lblEMG3";
            this.lblEMG3.Size = new System.Drawing.Size(26, 15);
            this.lblEMG3.TabIndex = 265;
            this.lblEMG3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS3
            // 
            this.lblPOS3.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS3.Location = new System.Drawing.Point(214, 863);
            this.lblPOS3.Name = "lblPOS3";
            this.lblPOS3.Size = new System.Drawing.Size(31, 15);
            this.lblPOS3.TabIndex = 268;
            this.lblPOS3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3To_YY
            // 
            this.lblDOS3To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3To_YY.Location = new System.Drawing.Point(185, 863);
            this.lblDOS3To_YY.Name = "lblDOS3To_YY";
            this.lblDOS3To_YY.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3To_YY.TabIndex = 264;
            this.lblDOS3To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3To_DD
            // 
            this.lblDOS3To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3To_DD.Location = new System.Drawing.Point(153, 863);
            this.lblDOS3To_DD.Name = "lblDOS3To_DD";
            this.lblDOS3To_DD.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3To_DD.TabIndex = 263;
            this.lblDOS3To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3To_MM
            // 
            this.lblDOS3To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3To_MM.Location = new System.Drawing.Point(119, 863);
            this.lblDOS3To_MM.Name = "lblDOS3To_MM";
            this.lblDOS3To_MM.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3To_MM.TabIndex = 262;
            this.lblDOS3To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3From_YY
            // 
            this.lblDOS3From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3From_YY.Location = new System.Drawing.Point(87, 863);
            this.lblDOS3From_YY.Name = "lblDOS3From_YY";
            this.lblDOS3From_YY.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3From_YY.TabIndex = 261;
            this.lblDOS3From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3From_DD
            // 
            this.lblDOS3From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3From_DD.Location = new System.Drawing.Point(55, 863);
            this.lblDOS3From_DD.Name = "lblDOS3From_DD";
            this.lblDOS3From_DD.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3From_DD.TabIndex = 260;
            this.lblDOS3From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS3From_MM
            // 
            this.lblDOS3From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS3From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS3From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS3From_MM.Location = new System.Drawing.Point(23, 863);
            this.lblDOS3From_MM.Name = "lblDOS3From_MM";
            this.lblDOS3From_MM.Size = new System.Drawing.Size(25, 14);
            this.lblDOS3From_MM.TabIndex = 259;
            this.lblDOS3From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider2_NPI
            // 
            this.lblRenderingProvider2_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider2_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider2_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider2_NPI.Location = new System.Drawing.Point(748, 826);
            this.lblRenderingProvider2_NPI.Name = "lblRenderingProvider2_NPI";
            this.lblRenderingProvider2_NPI.Size = new System.Drawing.Size(128, 16);
            this.lblRenderingProvider2_NPI.TabIndex = 248;
            this.lblRenderingProvider2_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT2
            // 
            this.lblEPSDT2.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT2.Location = new System.Drawing.Point(692, 826);
            this.lblEPSDT2.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT2.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT2.Name = "lblEPSDT2";
            this.lblEPSDT2.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT2.TabIndex = 252;
            this.lblEPSDT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits2
            // 
            this.lblUnits2.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits2.Location = new System.Drawing.Point(650, 826);
            this.lblUnits2.Name = "lblUnits2";
            this.lblUnits2.Size = new System.Drawing.Size(40, 16);
            this.lblUnits2.TabIndex = 253;
            this.lblUnits2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges21
            // 
            this.lblCharges21.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges21.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges21.Location = new System.Drawing.Point(622, 826);
            this.lblCharges21.Name = "lblCharges21";
            this.lblCharges21.Size = new System.Drawing.Size(25, 16);
            this.lblCharges21.TabIndex = 250;
            this.lblCharges21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges2
            // 
            this.lblCharges2.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges2.Location = new System.Drawing.Point(560, 826);
            this.lblCharges2.Name = "lblCharges2";
            this.lblCharges2.Size = new System.Drawing.Size(60, 16);
            this.lblCharges2.TabIndex = 251;
            this.lblCharges2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr2
            // 
            this.lblDxPtr2.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr2.Location = new System.Drawing.Point(498, 827);
            this.lblDxPtr2.Name = "lblDxPtr2";
            this.lblDxPtr2.Size = new System.Drawing.Size(50, 13);
            this.lblDxPtr2.TabIndex = 254;
            this.lblDxPtr2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD24
            // 
            this.lblMOD24.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD24.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD24.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD24.Location = new System.Drawing.Point(464, 826);
            this.lblMOD24.Name = "lblMOD24";
            this.lblMOD24.Size = new System.Drawing.Size(30, 17);
            this.lblMOD24.TabIndex = 257;
            this.lblMOD24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD23
            // 
            this.lblMOD23.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD23.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD23.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD23.Location = new System.Drawing.Point(433, 826);
            this.lblMOD23.Name = "lblMOD23";
            this.lblMOD23.Size = new System.Drawing.Size(30, 17);
            this.lblMOD23.TabIndex = 258;
            this.lblMOD23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD22
            // 
            this.lblMOD22.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD22.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD22.Location = new System.Drawing.Point(400, 826);
            this.lblMOD22.Name = "lblMOD22";
            this.lblMOD22.Size = new System.Drawing.Size(30, 17);
            this.lblMOD22.TabIndex = 255;
            this.lblMOD22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD21
            // 
            this.lblMOD21.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD21.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD21.Location = new System.Drawing.Point(363, 826);
            this.lblMOD21.Name = "lblMOD21";
            this.lblMOD21.Size = new System.Drawing.Size(33, 16);
            this.lblMOD21.TabIndex = 256;
            this.lblMOD21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT2
            // 
            this.lblCPT2.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT2.Location = new System.Drawing.Point(287, 826);
            this.lblCPT2.Name = "lblCPT2";
            this.lblCPT2.Size = new System.Drawing.Size(71, 17);
            this.lblCPT2.TabIndex = 247;
            this.lblCPT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG2
            // 
            this.lblEMG2.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG2.Location = new System.Drawing.Point(252, 826);
            this.lblEMG2.Name = "lblEMG2";
            this.lblEMG2.Size = new System.Drawing.Size(26, 17);
            this.lblEMG2.TabIndex = 246;
            this.lblEMG2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS2
            // 
            this.lblPOS2.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS2.Location = new System.Drawing.Point(214, 826);
            this.lblPOS2.Name = "lblPOS2";
            this.lblPOS2.Size = new System.Drawing.Size(31, 17);
            this.lblPOS2.TabIndex = 249;
            this.lblPOS2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2To_YY
            // 
            this.lblDOS2To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2To_YY.Location = new System.Drawing.Point(185, 826);
            this.lblDOS2To_YY.Name = "lblDOS2To_YY";
            this.lblDOS2To_YY.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2To_YY.TabIndex = 245;
            this.lblDOS2To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2To_DD
            // 
            this.lblDOS2To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2To_DD.Location = new System.Drawing.Point(153, 826);
            this.lblDOS2To_DD.Name = "lblDOS2To_DD";
            this.lblDOS2To_DD.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2To_DD.TabIndex = 244;
            this.lblDOS2To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2To_MM
            // 
            this.lblDOS2To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2To_MM.Location = new System.Drawing.Point(119, 826);
            this.lblDOS2To_MM.Name = "lblDOS2To_MM";
            this.lblDOS2To_MM.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2To_MM.TabIndex = 243;
            this.lblDOS2To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2From_YY
            // 
            this.lblDOS2From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2From_YY.Location = new System.Drawing.Point(87, 826);
            this.lblDOS2From_YY.Name = "lblDOS2From_YY";
            this.lblDOS2From_YY.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2From_YY.TabIndex = 242;
            this.lblDOS2From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2From_DD
            // 
            this.lblDOS2From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2From_DD.Location = new System.Drawing.Point(55, 826);
            this.lblDOS2From_DD.Name = "lblDOS2From_DD";
            this.lblDOS2From_DD.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2From_DD.TabIndex = 241;
            this.lblDOS2From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS2From_MM
            // 
            this.lblDOS2From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS2From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS2From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS2From_MM.Location = new System.Drawing.Point(23, 826);
            this.lblDOS2From_MM.Name = "lblDOS2From_MM";
            this.lblDOS2From_MM.Size = new System.Drawing.Size(25, 16);
            this.lblDOS2From_MM.TabIndex = 240;
            this.lblDOS2From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRenderingProvider1_NPI
            // 
            this.lblRenderingProvider1_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblRenderingProvider1_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderingProvider1_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblRenderingProvider1_NPI.Location = new System.Drawing.Point(748, 789);
            this.lblRenderingProvider1_NPI.Name = "lblRenderingProvider1_NPI";
            this.lblRenderingProvider1_NPI.Size = new System.Drawing.Size(128, 17);
            this.lblRenderingProvider1_NPI.TabIndex = 239;
            this.lblRenderingProvider1_NPI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEPSDT1
            // 
            this.lblEPSDT1.BackColor = System.Drawing.Color.Ivory;
            this.lblEPSDT1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPSDT1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEPSDT1.Location = new System.Drawing.Point(692, 789);
            this.lblEPSDT1.MaximumSize = new System.Drawing.Size(22, 15);
            this.lblEPSDT1.MinimumSize = new System.Drawing.Size(20, 15);
            this.lblEPSDT1.Name = "lblEPSDT1";
            this.lblEPSDT1.Size = new System.Drawing.Size(22, 15);
            this.lblEPSDT1.TabIndex = 239;
            this.lblEPSDT1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits1
            // 
            this.lblUnits1.BackColor = System.Drawing.Color.Ivory;
            this.lblUnits1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnits1.Location = new System.Drawing.Point(650, 789);
            this.lblUnits1.Name = "lblUnits1";
            this.lblUnits1.Size = new System.Drawing.Size(40, 16);
            this.lblUnits1.TabIndex = 239;
            this.lblUnits1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges11
            // 
            this.lblCharges11.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges11.Location = new System.Drawing.Point(622, 789);
            this.lblCharges11.Name = "lblCharges11";
            this.lblCharges11.Size = new System.Drawing.Size(25, 16);
            this.lblCharges11.TabIndex = 239;
            this.lblCharges11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharges1
            // 
            this.lblCharges1.BackColor = System.Drawing.Color.Ivory;
            this.lblCharges1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCharges1.Location = new System.Drawing.Point(560, 789);
            this.lblCharges1.Name = "lblCharges1";
            this.lblCharges1.Size = new System.Drawing.Size(60, 16);
            this.lblCharges1.TabIndex = 239;
            this.lblCharges1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDxPtr1
            // 
            this.lblDxPtr1.BackColor = System.Drawing.Color.Ivory;
            this.lblDxPtr1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDxPtr1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDxPtr1.Location = new System.Drawing.Point(498, 790);
            this.lblDxPtr1.Name = "lblDxPtr1";
            this.lblDxPtr1.Size = new System.Drawing.Size(50, 13);
            this.lblDxPtr1.TabIndex = 239;
            this.lblDxPtr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD14
            // 
            this.lblMOD14.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD14.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD14.Location = new System.Drawing.Point(464, 789);
            this.lblMOD14.Name = "lblMOD14";
            this.lblMOD14.Size = new System.Drawing.Size(30, 17);
            this.lblMOD14.TabIndex = 239;
            this.lblMOD14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD13
            // 
            this.lblMOD13.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD13.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD13.Location = new System.Drawing.Point(433, 789);
            this.lblMOD13.Name = "lblMOD13";
            this.lblMOD13.Size = new System.Drawing.Size(30, 17);
            this.lblMOD13.TabIndex = 239;
            this.lblMOD13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD12
            // 
            this.lblMOD12.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD12.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD12.Location = new System.Drawing.Point(400, 789);
            this.lblMOD12.Name = "lblMOD12";
            this.lblMOD12.Size = new System.Drawing.Size(30, 17);
            this.lblMOD12.TabIndex = 239;
            this.lblMOD12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMOD11
            // 
            this.lblMOD11.BackColor = System.Drawing.Color.Ivory;
            this.lblMOD11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMOD11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMOD11.Location = new System.Drawing.Point(363, 789);
            this.lblMOD11.Name = "lblMOD11";
            this.lblMOD11.Size = new System.Drawing.Size(33, 16);
            this.lblMOD11.TabIndex = 239;
            this.lblMOD11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPT1
            // 
            this.lblCPT1.BackColor = System.Drawing.Color.Ivory;
            this.lblCPT1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPT1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblCPT1.Location = new System.Drawing.Point(285, 789);
            this.lblCPT1.Name = "lblCPT1";
            this.lblCPT1.Size = new System.Drawing.Size(71, 17);
            this.lblCPT1.TabIndex = 239;
            this.lblCPT1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMG1
            // 
            this.lblEMG1.BackColor = System.Drawing.Color.Ivory;
            this.lblEMG1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMG1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblEMG1.Location = new System.Drawing.Point(252, 789);
            this.lblEMG1.Name = "lblEMG1";
            this.lblEMG1.Size = new System.Drawing.Size(26, 17);
            this.lblEMG1.TabIndex = 239;
            this.lblEMG1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPOS1
            // 
            this.lblPOS1.BackColor = System.Drawing.Color.Ivory;
            this.lblPOS1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPOS1.Location = new System.Drawing.Point(213, 789);
            this.lblPOS1.Name = "lblPOS1";
            this.lblPOS1.Size = new System.Drawing.Size(31, 17);
            this.lblPOS1.TabIndex = 239;
            this.lblPOS1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1To_YY
            // 
            this.lblDOS1To_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1To_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1To_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1To_YY.Location = new System.Drawing.Point(185, 789);
            this.lblDOS1To_YY.Name = "lblDOS1To_YY";
            this.lblDOS1To_YY.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1To_YY.TabIndex = 238;
            this.lblDOS1To_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1To_DD
            // 
            this.lblDOS1To_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1To_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1To_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1To_DD.Location = new System.Drawing.Point(153, 789);
            this.lblDOS1To_DD.Name = "lblDOS1To_DD";
            this.lblDOS1To_DD.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1To_DD.TabIndex = 237;
            this.lblDOS1To_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1To_MM
            // 
            this.lblDOS1To_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1To_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1To_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1To_MM.Location = new System.Drawing.Point(119, 789);
            this.lblDOS1To_MM.Name = "lblDOS1To_MM";
            this.lblDOS1To_MM.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1To_MM.TabIndex = 236;
            this.lblDOS1To_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1From_YY
            // 
            this.lblDOS1From_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1From_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1From_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1From_YY.Location = new System.Drawing.Point(87, 789);
            this.lblDOS1From_YY.Name = "lblDOS1From_YY";
            this.lblDOS1From_YY.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1From_YY.TabIndex = 235;
            this.lblDOS1From_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1From_DD
            // 
            this.lblDOS1From_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1From_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1From_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1From_DD.Location = new System.Drawing.Point(55, 789);
            this.lblDOS1From_DD.Name = "lblDOS1From_DD";
            this.lblDOS1From_DD.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1From_DD.TabIndex = 234;
            this.lblDOS1From_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOS1From_MM
            // 
            this.lblDOS1From_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDOS1From_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOS1From_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDOS1From_MM.Location = new System.Drawing.Point(23, 789);
            this.lblDOS1From_MM.Name = "lblDOS1From_MM";
            this.lblDOS1From_MM.Size = new System.Drawing.Size(25, 16);
            this.lblDOS1From_MM.TabIndex = 233;
            this.lblDOS1From_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNotes1
            // 
            this.lblNotes1.BackColor = System.Drawing.Color.Ivory;
            this.lblNotes1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblNotes1.Location = new System.Drawing.Point(22, 772);
            this.lblNotes1.Name = "lblNotes1";
            this.lblNotes1.Size = new System.Drawing.Size(667, 15);
            this.lblNotes1.TabIndex = 232;
            // 
            // lblPriorAuthorizationNumber
            // 
            this.lblPriorAuthorizationNumber.BackColor = System.Drawing.Color.Ivory;
            this.lblPriorAuthorizationNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriorAuthorizationNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPriorAuthorizationNumber.Location = new System.Drawing.Point(575, 717);
            this.lblPriorAuthorizationNumber.Name = "lblPriorAuthorizationNumber";
            this.lblPriorAuthorizationNumber.Size = new System.Drawing.Size(300, 15);
            this.lblPriorAuthorizationNumber.TabIndex = 231;
            // 
            // lblDiagnosisCode41
            // 
            this.lblDiagnosisCode41.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCode41.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCode41.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDiagnosisCode41.Location = new System.Drawing.Point(472, 682);
            this.lblDiagnosisCode41.Name = "lblDiagnosisCode41";
            this.lblDiagnosisCode41.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCode41.TabIndex = 229;
            // 
            // lblDiagnosisCode21
            // 
            this.lblDiagnosisCode21.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCode21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCode21.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDiagnosisCode21.Location = new System.Drawing.Point(192, 680);
            this.lblDiagnosisCode21.Name = "lblDiagnosisCode21";
            this.lblDiagnosisCode21.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCode21.TabIndex = 227;
            // 
            // lblOriginalRefNumber
            // 
            this.lblOriginalRefNumber.BackColor = System.Drawing.Color.Ivory;
            this.lblOriginalRefNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriginalRefNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOriginalRefNumber.Location = new System.Drawing.Point(683, 685);
            this.lblOriginalRefNumber.Name = "lblOriginalRefNumber";
            this.lblOriginalRefNumber.Size = new System.Drawing.Size(181, 14);
            this.lblOriginalRefNumber.TabIndex = 226;
            // 
            // lblMedicaidResubmissionCode
            // 
            this.lblMedicaidResubmissionCode.BackColor = System.Drawing.Color.Ivory;
            this.lblMedicaidResubmissionCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicaidResubmissionCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMedicaidResubmissionCode.Location = new System.Drawing.Point(575, 682);
            this.lblMedicaidResubmissionCode.Name = "lblMedicaidResubmissionCode";
            this.lblMedicaidResubmissionCode.Size = new System.Drawing.Size(90, 15);
            this.lblMedicaidResubmissionCode.TabIndex = 225;
            // 
            // lblDiagnosisCode31
            // 
            this.lblDiagnosisCode31.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCode31.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCode31.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDiagnosisCode31.Location = new System.Drawing.Point(333, 680);
            this.lblDiagnosisCode31.Name = "lblDiagnosisCode31";
            this.lblDiagnosisCode31.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCode31.TabIndex = 223;
            // 
            // lblDiagnosisCode11
            // 
            this.lblDiagnosisCode11.BackColor = System.Drawing.Color.Ivory;
            this.lblDiagnosisCode11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisCode11.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDiagnosisCode11.Location = new System.Drawing.Point(51, 680);
            this.lblDiagnosisCode11.Name = "lblDiagnosisCode11";
            this.lblDiagnosisCode11.Size = new System.Drawing.Size(68, 16);
            this.lblDiagnosisCode11.TabIndex = 221;
            // 
            // lblOutsideLabCharges2
            // 
            this.lblOutsideLabCharges2.BackColor = System.Drawing.Color.Ivory;
            this.lblOutsideLabCharges2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutsideLabCharges2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutsideLabCharges2.Location = new System.Drawing.Point(788, 645);
            this.lblOutsideLabCharges2.Name = "lblOutsideLabCharges2";
            this.lblOutsideLabCharges2.Size = new System.Drawing.Size(83, 15);
            this.lblOutsideLabCharges2.TabIndex = 220;
            this.lblOutsideLabCharges2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOutsideLabCharges1
            // 
            this.lblOutsideLabCharges1.BackColor = System.Drawing.Color.Ivory;
            this.lblOutsideLabCharges1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutsideLabCharges1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOutsideLabCharges1.Location = new System.Drawing.Point(684, 645);
            this.lblOutsideLabCharges1.Name = "lblOutsideLabCharges1";
            this.lblOutsideLabCharges1.Size = new System.Drawing.Size(90, 15);
            this.lblOutsideLabCharges1.TabIndex = 219;
            this.lblOutsideLabCharges1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOutsideLab_No
            // 
            this.lblOutsideLab_No.BackColor = System.Drawing.Color.Ivory;
            this.lblOutsideLab_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutsideLab_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutsideLab_No.ForeColor = System.Drawing.Color.Black;
            this.lblOutsideLab_No.Location = new System.Drawing.Point(630, 644);
            this.lblOutsideLab_No.Name = "lblOutsideLab_No";
            this.lblOutsideLab_No.Size = new System.Drawing.Size(19, 20);
            this.lblOutsideLab_No.TabIndex = 218;
            this.lblOutsideLab_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutsideLab_Yes
            // 
            this.lblOutsideLab_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblOutsideLab_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutsideLab_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutsideLab_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblOutsideLab_Yes.Location = new System.Drawing.Point(575, 644);
            this.lblOutsideLab_Yes.Name = "lblOutsideLab_Yes";
            this.lblOutsideLab_Yes.Size = new System.Drawing.Size(19, 20);
            this.lblOutsideLab_Yes.TabIndex = 217;
            this.lblOutsideLab_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReservedForLocalUse
            // 
            this.lblReservedForLocalUse.BackColor = System.Drawing.Color.Ivory;
            this.lblReservedForLocalUse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservedForLocalUse.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReservedForLocalUse.Location = new System.Drawing.Point(41, 643);
            this.lblReservedForLocalUse.Name = "lblReservedForLocalUse";
            this.lblReservedForLocalUse.Size = new System.Drawing.Size(488, 15);
            this.lblReservedForLocalUse.TabIndex = 216;
            // 
            // lblHospitalisationTo_YY
            // 
            this.lblHospitalisationTo_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationTo_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationTo_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationTo_YY.Location = new System.Drawing.Point(823, 611);
            this.lblHospitalisationTo_YY.Name = "lblHospitalisationTo_YY";
            this.lblHospitalisationTo_YY.Size = new System.Drawing.Size(40, 14);
            this.lblHospitalisationTo_YY.TabIndex = 215;
            this.lblHospitalisationTo_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHospitalisationTo_DD
            // 
            this.lblHospitalisationTo_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationTo_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationTo_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationTo_DD.Location = new System.Drawing.Point(785, 611);
            this.lblHospitalisationTo_DD.Name = "lblHospitalisationTo_DD";
            this.lblHospitalisationTo_DD.Size = new System.Drawing.Size(25, 14);
            this.lblHospitalisationTo_DD.TabIndex = 214;
            this.lblHospitalisationTo_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHospitalisationTo_MM
            // 
            this.lblHospitalisationTo_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationTo_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationTo_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationTo_MM.Location = new System.Drawing.Point(751, 611);
            this.lblHospitalisationTo_MM.Name = "lblHospitalisationTo_MM";
            this.lblHospitalisationTo_MM.Size = new System.Drawing.Size(25, 14);
            this.lblHospitalisationTo_MM.TabIndex = 213;
            this.lblHospitalisationTo_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHospitalisationFrom_YY
            // 
            this.lblHospitalisationFrom_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationFrom_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationFrom_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationFrom_YY.Location = new System.Drawing.Point(666, 611);
            this.lblHospitalisationFrom_YY.Name = "lblHospitalisationFrom_YY";
            this.lblHospitalisationFrom_YY.Size = new System.Drawing.Size(40, 14);
            this.lblHospitalisationFrom_YY.TabIndex = 212;
            this.lblHospitalisationFrom_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHospitalisationFrom_DD
            // 
            this.lblHospitalisationFrom_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationFrom_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationFrom_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationFrom_DD.Location = new System.Drawing.Point(632, 611);
            this.lblHospitalisationFrom_DD.Name = "lblHospitalisationFrom_DD";
            this.lblHospitalisationFrom_DD.Size = new System.Drawing.Size(25, 14);
            this.lblHospitalisationFrom_DD.TabIndex = 211;
            this.lblHospitalisationFrom_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHospitalisationFrom_MM
            // 
            this.lblHospitalisationFrom_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblHospitalisationFrom_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitalisationFrom_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblHospitalisationFrom_MM.Location = new System.Drawing.Point(599, 611);
            this.lblHospitalisationFrom_MM.Name = "lblHospitalisationFrom_MM";
            this.lblHospitalisationFrom_MM.Size = new System.Drawing.Size(25, 14);
            this.lblHospitalisationFrom_MM.TabIndex = 210;
            this.lblHospitalisationFrom_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReferringProvider_NPI
            // 
            this.lblReferringProvider_NPI.BackColor = System.Drawing.Color.Ivory;
            this.lblReferringProvider_NPI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferringProvider_NPI.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReferringProvider_NPI.Location = new System.Drawing.Point(364, 610);
            this.lblReferringProvider_NPI.Name = "lblReferringProvider_NPI";
            this.lblReferringProvider_NPI.Size = new System.Drawing.Size(186, 15);
            this.lblReferringProvider_NPI.TabIndex = 209;
            // 
            // btn_BrowseReferral
            // 
            this.btn_BrowseReferral.BackColor = System.Drawing.Color.Transparent;
            this.btn_BrowseReferral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowseReferral.BackgroundImage")));
            this.btn_BrowseReferral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowseReferral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseReferral.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowseReferral.Image")));
            this.btn_BrowseReferral.Location = new System.Drawing.Point(289, 603);
            this.btn_BrowseReferral.Name = "btn_BrowseReferral";
            this.btn_BrowseReferral.Size = new System.Drawing.Size(22, 22);
            this.btn_BrowseReferral.TabIndex = 208;
            this.btn_BrowseReferral.UseVisualStyleBackColor = false;
            // 
            // lblReferringProviderName
            // 
            this.lblReferringProviderName.BackColor = System.Drawing.Color.Ivory;
            this.lblReferringProviderName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferringProviderName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReferringProviderName.Location = new System.Drawing.Point(55, 609);
            this.lblReferringProviderName.Name = "lblReferringProviderName";
            this.lblReferringProviderName.Size = new System.Drawing.Size(231, 15);
            this.lblReferringProviderName.TabIndex = 84;
            // 
            // lblUnableToWorkTill_YY
            // 
            this.lblUnableToWorkTill_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkTill_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkTill_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkTill_YY.Location = new System.Drawing.Point(823, 576);
            this.lblUnableToWorkTill_YY.Name = "lblUnableToWorkTill_YY";
            this.lblUnableToWorkTill_YY.Size = new System.Drawing.Size(40, 14);
            this.lblUnableToWorkTill_YY.TabIndex = 83;
            this.lblUnableToWorkTill_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnableToWorkTill_DD
            // 
            this.lblUnableToWorkTill_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkTill_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkTill_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkTill_DD.Location = new System.Drawing.Point(785, 576);
            this.lblUnableToWorkTill_DD.Name = "lblUnableToWorkTill_DD";
            this.lblUnableToWorkTill_DD.Size = new System.Drawing.Size(25, 14);
            this.lblUnableToWorkTill_DD.TabIndex = 82;
            this.lblUnableToWorkTill_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnableToWorkTill_MM
            // 
            this.lblUnableToWorkTill_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkTill_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkTill_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkTill_MM.Location = new System.Drawing.Point(751, 576);
            this.lblUnableToWorkTill_MM.Name = "lblUnableToWorkTill_MM";
            this.lblUnableToWorkTill_MM.Size = new System.Drawing.Size(25, 14);
            this.lblUnableToWorkTill_MM.TabIndex = 81;
            this.lblUnableToWorkTill_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnableToWorkFrom_YY
            // 
            this.lblUnableToWorkFrom_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkFrom_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkFrom_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkFrom_YY.Location = new System.Drawing.Point(666, 576);
            this.lblUnableToWorkFrom_YY.Name = "lblUnableToWorkFrom_YY";
            this.lblUnableToWorkFrom_YY.Size = new System.Drawing.Size(40, 14);
            this.lblUnableToWorkFrom_YY.TabIndex = 80;
            this.lblUnableToWorkFrom_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnableToWorkFrom_DD
            // 
            this.lblUnableToWorkFrom_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkFrom_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkFrom_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkFrom_DD.Location = new System.Drawing.Point(632, 576);
            this.lblUnableToWorkFrom_DD.Name = "lblUnableToWorkFrom_DD";
            this.lblUnableToWorkFrom_DD.Size = new System.Drawing.Size(25, 14);
            this.lblUnableToWorkFrom_DD.TabIndex = 79;
            this.lblUnableToWorkFrom_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnableToWorkFrom_MM
            // 
            this.lblUnableToWorkFrom_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblUnableToWorkFrom_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnableToWorkFrom_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblUnableToWorkFrom_MM.Location = new System.Drawing.Point(599, 576);
            this.lblUnableToWorkFrom_MM.Name = "lblUnableToWorkFrom_MM";
            this.lblUnableToWorkFrom_MM.Size = new System.Drawing.Size(25, 14);
            this.lblUnableToWorkFrom_MM.TabIndex = 78;
            this.lblUnableToWorkFrom_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSimilarIllnessFirstDate_YY
            // 
            this.lblSimilarIllnessFirstDate_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblSimilarIllnessFirstDate_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimilarIllnessFirstDate_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblSimilarIllnessFirstDate_YY.Location = new System.Drawing.Point(483, 575);
            this.lblSimilarIllnessFirstDate_YY.Name = "lblSimilarIllnessFirstDate_YY";
            this.lblSimilarIllnessFirstDate_YY.Size = new System.Drawing.Size(44, 14);
            this.lblSimilarIllnessFirstDate_YY.TabIndex = 77;
            this.lblSimilarIllnessFirstDate_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSimilarIllnessFirstDate_DD
            // 
            this.lblSimilarIllnessFirstDate_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblSimilarIllnessFirstDate_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimilarIllnessFirstDate_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblSimilarIllnessFirstDate_DD.Location = new System.Drawing.Point(449, 575);
            this.lblSimilarIllnessFirstDate_DD.Name = "lblSimilarIllnessFirstDate_DD";
            this.lblSimilarIllnessFirstDate_DD.Size = new System.Drawing.Size(25, 14);
            this.lblSimilarIllnessFirstDate_DD.TabIndex = 76;
            this.lblSimilarIllnessFirstDate_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSimilarIllnessFirstDate_MM
            // 
            this.lblSimilarIllnessFirstDate_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblSimilarIllnessFirstDate_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimilarIllnessFirstDate_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblSimilarIllnessFirstDate_MM.Location = new System.Drawing.Point(416, 575);
            this.lblSimilarIllnessFirstDate_MM.Name = "lblSimilarIllnessFirstDate_MM";
            this.lblSimilarIllnessFirstDate_MM.Size = new System.Drawing.Size(25, 14);
            this.lblSimilarIllnessFirstDate_MM.TabIndex = 75;
            this.lblSimilarIllnessFirstDate_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateOfCurrentIllness_YY
            // 
            this.lblDateOfCurrentIllness_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblDateOfCurrentIllness_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfCurrentIllness_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDateOfCurrentIllness_YY.Location = new System.Drawing.Point(100, 576);
            this.lblDateOfCurrentIllness_YY.Name = "lblDateOfCurrentIllness_YY";
            this.lblDateOfCurrentIllness_YY.Size = new System.Drawing.Size(37, 14);
            this.lblDateOfCurrentIllness_YY.TabIndex = 74;
            this.lblDateOfCurrentIllness_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateOfCurrentIllness_DD
            // 
            this.lblDateOfCurrentIllness_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblDateOfCurrentIllness_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfCurrentIllness_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDateOfCurrentIllness_DD.Location = new System.Drawing.Point(64, 576);
            this.lblDateOfCurrentIllness_DD.Name = "lblDateOfCurrentIllness_DD";
            this.lblDateOfCurrentIllness_DD.Size = new System.Drawing.Size(25, 14);
            this.lblDateOfCurrentIllness_DD.TabIndex = 73;
            this.lblDateOfCurrentIllness_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateOfCurrentIllness_MM
            // 
            this.lblDateOfCurrentIllness_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblDateOfCurrentIllness_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfCurrentIllness_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblDateOfCurrentIllness_MM.Location = new System.Drawing.Point(35, 576);
            this.lblDateOfCurrentIllness_MM.Name = "lblDateOfCurrentIllness_MM";
            this.lblDateOfCurrentIllness_MM.Size = new System.Drawing.Size(25, 14);
            this.lblDateOfCurrentIllness_MM.TabIndex = 72;
            this.lblDateOfCurrentIllness_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInsuredPersonSign
            // 
            this.lblInsuredPersonSign.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredPersonSign.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredPersonSign.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredPersonSign.Location = new System.Drawing.Point(615, 521);
            this.lblInsuredPersonSign.Name = "lblInsuredPersonSign";
            this.lblInsuredPersonSign.Size = new System.Drawing.Size(261, 28);
            this.lblInsuredPersonSign.TabIndex = 71;
            // 
            // lblPatientSignDate_YY
            // 
            this.lblPatientSignDate_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientSignDate_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSignDate_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientSignDate_YY.Location = new System.Drawing.Point(357, 516);
            this.lblPatientSignDate_YY.Name = "lblPatientSignDate_YY";
            this.lblPatientSignDate_YY.Size = new System.Drawing.Size(25, 14);
            this.lblPatientSignDate_YY.TabIndex = 70;
            this.lblPatientSignDate_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientSignDate_DD
            // 
            this.lblPatientSignDate_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientSignDate_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSignDate_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientSignDate_DD.Location = new System.Drawing.Point(362, 516);
            this.lblPatientSignDate_DD.Name = "lblPatientSignDate_DD";
            this.lblPatientSignDate_DD.Size = new System.Drawing.Size(25, 14);
            this.lblPatientSignDate_DD.TabIndex = 69;
            this.lblPatientSignDate_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientSignDate_MM
            // 
            this.lblPatientSignDate_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientSignDate_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSignDate_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientSignDate_MM.Location = new System.Drawing.Point(410, 532);
            this.lblPatientSignDate_MM.Name = "lblPatientSignDate_MM";
            this.lblPatientSignDate_MM.Size = new System.Drawing.Size(94, 14);
            this.lblPatientSignDate_MM.TabIndex = 68;
            this.lblPatientSignDate_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientSignature
            // 
            this.lblPatientSignature.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientSignature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSignature.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientSignature.Location = new System.Drawing.Point(85, 518);
            this.lblPatientSignature.Name = "lblPatientSignature";
            this.lblPatientSignature.Size = new System.Drawing.Size(270, 28);
            this.lblPatientSignature.TabIndex = 67;
            // 
            // btnBrowsePatientRegt
            // 
            this.btnBrowsePatientRegt.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatientRegt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientRegt.BackgroundImage")));
            this.btnBrowsePatientRegt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatientRegt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatientRegt.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientRegt.Image")));
            this.btnBrowsePatientRegt.Location = new System.Drawing.Point(855, 421);
            this.btnBrowsePatientRegt.Name = "btnBrowsePatientRegt";
            this.btnBrowsePatientRegt.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatientRegt.TabIndex = 66;
            this.btnBrowsePatientRegt.UseVisualStyleBackColor = false;
            // 
            // lblIsOtherHealthPlan_No
            // 
            this.lblIsOtherHealthPlan_No.BackColor = System.Drawing.Color.Ivory;
            this.lblIsOtherHealthPlan_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIsOtherHealthPlan_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsOtherHealthPlan_No.ForeColor = System.Drawing.Color.Black;
            this.lblIsOtherHealthPlan_No.Location = new System.Drawing.Point(629, 462);
            this.lblIsOtherHealthPlan_No.Name = "lblIsOtherHealthPlan_No";
            this.lblIsOtherHealthPlan_No.Size = new System.Drawing.Size(19, 20);
            this.lblIsOtherHealthPlan_No.TabIndex = 65;
            this.lblIsOtherHealthPlan_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIsOtherHealthPlan_Yes
            // 
            this.lblIsOtherHealthPlan_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblIsOtherHealthPlan_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIsOtherHealthPlan_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsOtherHealthPlan_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblIsOtherHealthPlan_Yes.Location = new System.Drawing.Point(574, 462);
            this.lblIsOtherHealthPlan_Yes.Name = "lblIsOtherHealthPlan_Yes";
            this.lblIsOtherHealthPlan_Yes.Size = new System.Drawing.Size(19, 20);
            this.lblIsOtherHealthPlan_Yes.TabIndex = 64;
            this.lblIsOtherHealthPlan_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReserveForLocalUse
            // 
            this.lblReserveForLocalUse.BackColor = System.Drawing.Color.Ivory;
            this.lblReserveForLocalUse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveForLocalUse.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblReserveForLocalUse.Location = new System.Drawing.Point(353, 464);
            this.lblReserveForLocalUse.Name = "lblReserveForLocalUse";
            this.lblReserveForLocalUse.Size = new System.Drawing.Size(183, 15);
            this.lblReserveForLocalUse.TabIndex = 63;
            // 
            // lblOtherInsuredInsuranceName
            // 
            this.lblOtherInsuredInsuranceName.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherInsuredInsuranceName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherInsuredInsuranceName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOtherInsuredInsuranceName.Location = new System.Drawing.Point(41, 464);
            this.lblOtherInsuredInsuranceName.Name = "lblOtherInsuredInsuranceName";
            this.lblOtherInsuredInsuranceName.Size = new System.Drawing.Size(291, 15);
            this.lblOtherInsuredInsuranceName.TabIndex = 62;
            // 
            // lblInsuredInsurancePlanName
            // 
            this.lblInsuredInsurancePlanName.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredInsurancePlanName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredInsurancePlanName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredInsurancePlanName.Location = new System.Drawing.Point(575, 428);
            this.lblInsuredInsurancePlanName.Name = "lblInsuredInsurancePlanName";
            this.lblInsuredInsurancePlanName.Size = new System.Drawing.Size(276, 15);
            this.lblInsuredInsurancePlanName.TabIndex = 61;
            // 
            // lblPatientCoditionRelatedTo_OtherAccident_No
            // 
            this.lblPatientCoditionRelatedTo_OtherAccident_No.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_OtherAccident_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_OtherAccident_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_OtherAccident_No.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_OtherAccident_No.Location = new System.Drawing.Point(455, 426);
            this.lblPatientCoditionRelatedTo_OtherAccident_No.Name = "lblPatientCoditionRelatedTo_OtherAccident_No";
            this.lblPatientCoditionRelatedTo_OtherAccident_No.Size = new System.Drawing.Size(20, 20);
            this.lblPatientCoditionRelatedTo_OtherAccident_No.TabIndex = 60;
            this.lblPatientCoditionRelatedTo_OtherAccident_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientCoditionRelatedTo_OtherAccident_Yes
            // 
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.Location = new System.Drawing.Point(390, 426);
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.Name = "lblPatientCoditionRelatedTo_OtherAccident_Yes";
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.Size = new System.Drawing.Size(19, 20);
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.TabIndex = 59;
            this.lblPatientCoditionRelatedTo_OtherAccident_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOtherInsuredEmployerORSchoolName
            // 
            this.lblOtherInsuredEmployerORSchoolName.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherInsuredEmployerORSchoolName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherInsuredEmployerORSchoolName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOtherInsuredEmployerORSchoolName.Location = new System.Drawing.Point(41, 427);
            this.lblOtherInsuredEmployerORSchoolName.Name = "lblOtherInsuredEmployerORSchoolName";
            this.lblOtherInsuredEmployerORSchoolName.Size = new System.Drawing.Size(291, 15);
            this.lblOtherInsuredEmployerORSchoolName.TabIndex = 58;
            // 
            // lblInsuredEmployerORSchoolName
            // 
            this.lblInsuredEmployerORSchoolName.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredEmployerORSchoolName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredEmployerORSchoolName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredEmployerORSchoolName.Location = new System.Drawing.Point(583, 392);
            this.lblInsuredEmployerORSchoolName.Name = "lblInsuredEmployerORSchoolName";
            this.lblInsuredEmployerORSchoolName.Size = new System.Drawing.Size(291, 15);
            this.lblInsuredEmployerORSchoolName.TabIndex = 57;
            // 
            // lblPatientCoditionRelatedTo_State
            // 
            this.lblPatientCoditionRelatedTo_State.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_State.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_State.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientCoditionRelatedTo_State.Location = new System.Drawing.Point(500, 390);
            this.lblPatientCoditionRelatedTo_State.Name = "lblPatientCoditionRelatedTo_State";
            this.lblPatientCoditionRelatedTo_State.Size = new System.Drawing.Size(27, 15);
            this.lblPatientCoditionRelatedTo_State.TabIndex = 56;
            this.lblPatientCoditionRelatedTo_State.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientCoditionRelatedTo_AutoAccident_No
            // 
            this.lblPatientCoditionRelatedTo_AutoAccident_No.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_AutoAccident_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_AutoAccident_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_AutoAccident_No.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_AutoAccident_No.Location = new System.Drawing.Point(455, 391);
            this.lblPatientCoditionRelatedTo_AutoAccident_No.Name = "lblPatientCoditionRelatedTo_AutoAccident_No";
            this.lblPatientCoditionRelatedTo_AutoAccident_No.Size = new System.Drawing.Size(20, 20);
            this.lblPatientCoditionRelatedTo_AutoAccident_No.TabIndex = 55;
            this.lblPatientCoditionRelatedTo_AutoAccident_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientCoditionRelatedTo_AutoAccident_Yes
            // 
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.Location = new System.Drawing.Point(390, 391);
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.Name = "lblPatientCoditionRelatedTo_AutoAccident_Yes";
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.Size = new System.Drawing.Size(19, 20);
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.TabIndex = 54;
            this.lblPatientCoditionRelatedTo_AutoAccident_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsuredSex_Female
            // 
            this.lblInsuredSex_Female.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredSex_Female.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInsuredSex_Female.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredSex_Female.ForeColor = System.Drawing.Color.Black;
            this.lblInsuredSex_Female.Location = new System.Drawing.Point(825, 353);
            this.lblInsuredSex_Female.Name = "lblInsuredSex_Female";
            this.lblInsuredSex_Female.Size = new System.Drawing.Size(19, 20);
            this.lblInsuredSex_Female.TabIndex = 48;
            this.lblInsuredSex_Female.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsuredSex_Male
            // 
            this.lblInsuredSex_Male.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredSex_Male.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInsuredSex_Male.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredSex_Male.ForeColor = System.Drawing.Color.Black;
            this.lblInsuredSex_Male.Location = new System.Drawing.Point(748, 353);
            this.lblInsuredSex_Male.Name = "lblInsuredSex_Male";
            this.lblInsuredSex_Male.Size = new System.Drawing.Size(19, 20);
            this.lblInsuredSex_Male.TabIndex = 47;
            this.lblInsuredSex_Male.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsuredsDOB_YY
            // 
            this.lblInsuredsDOB_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsDOB_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsDOB_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsDOB_YY.Location = new System.Drawing.Point(656, 358);
            this.lblInsuredsDOB_YY.Name = "lblInsuredsDOB_YY";
            this.lblInsuredsDOB_YY.Size = new System.Drawing.Size(40, 14);
            this.lblInsuredsDOB_YY.TabIndex = 46;
            this.lblInsuredsDOB_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInsuredsDOB_DD
            // 
            this.lblInsuredsDOB_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsDOB_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsDOB_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsDOB_DD.Location = new System.Drawing.Point(622, 358);
            this.lblInsuredsDOB_DD.Name = "lblInsuredsDOB_DD";
            this.lblInsuredsDOB_DD.Size = new System.Drawing.Size(25, 14);
            this.lblInsuredsDOB_DD.TabIndex = 45;
            this.lblInsuredsDOB_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInsuredsDOB_MM
            // 
            this.lblInsuredsDOB_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsDOB_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsDOB_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsDOB_MM.Location = new System.Drawing.Point(589, 358);
            this.lblInsuredsDOB_MM.Name = "lblInsuredsDOB_MM";
            this.lblInsuredsDOB_MM.Size = new System.Drawing.Size(25, 14);
            this.lblInsuredsDOB_MM.TabIndex = 44;
            this.lblInsuredsDOB_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientCoditionRelatedTo_Employment_No
            // 
            this.lblPatientCoditionRelatedTo_Employment_No.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_Employment_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_Employment_No.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_Employment_No.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_Employment_No.Location = new System.Drawing.Point(455, 355);
            this.lblPatientCoditionRelatedTo_Employment_No.Name = "lblPatientCoditionRelatedTo_Employment_No";
            this.lblPatientCoditionRelatedTo_Employment_No.Size = new System.Drawing.Size(20, 20);
            this.lblPatientCoditionRelatedTo_Employment_No.TabIndex = 43;
            this.lblPatientCoditionRelatedTo_Employment_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientCoditionRelatedTo_Employment_Yes
            // 
            this.lblPatientCoditionRelatedTo_Employment_Yes.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCoditionRelatedTo_Employment_Yes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientCoditionRelatedTo_Employment_Yes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCoditionRelatedTo_Employment_Yes.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCoditionRelatedTo_Employment_Yes.Location = new System.Drawing.Point(390, 355);
            this.lblPatientCoditionRelatedTo_Employment_Yes.Name = "lblPatientCoditionRelatedTo_Employment_Yes";
            this.lblPatientCoditionRelatedTo_Employment_Yes.Size = new System.Drawing.Size(19, 20);
            this.lblPatientCoditionRelatedTo_Employment_Yes.TabIndex = 42;
            this.lblPatientCoditionRelatedTo_Employment_Yes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOtherInsuredPolicyNo
            // 
            this.lblOtherInsuredPolicyNo.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherInsuredPolicyNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherInsuredPolicyNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOtherInsuredPolicyNo.Location = new System.Drawing.Point(41, 354);
            this.lblOtherInsuredPolicyNo.Name = "lblOtherInsuredPolicyNo";
            this.lblOtherInsuredPolicyNo.Size = new System.Drawing.Size(291, 15);
            this.lblOtherInsuredPolicyNo.TabIndex = 41;
            // 
            // lblInsuredPolicyorFECANo
            // 
            this.lblInsuredPolicyorFECANo.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredPolicyorFECANo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredPolicyorFECANo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredPolicyorFECANo.Location = new System.Drawing.Point(575, 320);
            this.lblInsuredPolicyorFECANo.Name = "lblInsuredPolicyorFECANo";
            this.lblInsuredPolicyorFECANo.Size = new System.Drawing.Size(291, 15);
            this.lblInsuredPolicyorFECANo.TabIndex = 40;
            // 
            // lblPatientCondition
            // 
            this.lblPatientCondition.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCondition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCondition.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientCondition.Location = new System.Drawing.Point(357, 317);
            this.lblPatientCondition.Name = "lblPatientCondition";
            this.lblPatientCondition.Size = new System.Drawing.Size(183, 15);
            this.lblPatientCondition.TabIndex = 39;
            // 
            // lblOtherInsuredName
            // 
            this.lblOtherInsuredName.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherInsuredName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherInsuredName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblOtherInsuredName.Location = new System.Drawing.Point(41, 319);
            this.lblOtherInsuredName.Name = "lblOtherInsuredName";
            this.lblOtherInsuredName.Size = new System.Drawing.Size(291, 15);
            this.lblOtherInsuredName.TabIndex = 38;
            // 
            // lblInsuredTelephone2
            // 
            this.lblInsuredTelephone2.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredTelephone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredTelephone2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredTelephone2.Location = new System.Drawing.Point(769, 283);
            this.lblInsuredTelephone2.Name = "lblInsuredTelephone2";
            this.lblInsuredTelephone2.Size = new System.Drawing.Size(97, 15);
            this.lblInsuredTelephone2.TabIndex = 37;
            // 
            // lblInsuredTelephone1
            // 
            this.lblInsuredTelephone1.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredTelephone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredTelephone1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredTelephone1.Location = new System.Drawing.Point(722, 283);
            this.lblInsuredTelephone1.Name = "lblInsuredTelephone1";
            this.lblInsuredTelephone1.Size = new System.Drawing.Size(32, 15);
            this.lblInsuredTelephone1.TabIndex = 36;
            // 
            // lblInsuredsZip
            // 
            this.lblInsuredsZip.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsZip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsZip.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsZip.Location = new System.Drawing.Point(575, 283);
            this.lblInsuredsZip.Name = "lblInsuredsZip";
            this.lblInsuredsZip.Size = new System.Drawing.Size(105, 15);
            this.lblInsuredsZip.TabIndex = 35;
            // 
            // lblPatientTelephone2
            // 
            this.lblPatientTelephone2.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientTelephone2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientTelephone2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientTelephone2.Location = new System.Drawing.Point(218, 284);
            this.lblPatientTelephone2.Name = "lblPatientTelephone2";
            this.lblPatientTelephone2.Size = new System.Drawing.Size(97, 15);
            this.lblPatientTelephone2.TabIndex = 31;
            // 
            // lblPatientTelephone1
            // 
            this.lblPatientTelephone1.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientTelephone1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientTelephone1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientTelephone1.Location = new System.Drawing.Point(178, 284);
            this.lblPatientTelephone1.Name = "lblPatientTelephone1";
            this.lblPatientTelephone1.Size = new System.Drawing.Size(32, 15);
            this.lblPatientTelephone1.TabIndex = 30;
            // 
            // lblPatientZip
            // 
            this.lblPatientZip.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientZip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientZip.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientZip.Location = new System.Drawing.Point(41, 283);
            this.lblPatientZip.Name = "lblPatientZip";
            this.lblPatientZip.Size = new System.Drawing.Size(117, 15);
            this.lblPatientZip.TabIndex = 29;
            // 
            // lblInsuredsState
            // 
            this.lblInsuredsState.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsState.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsState.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsState.Location = new System.Drawing.Point(816, 246);
            this.lblInsuredsState.Name = "lblInsuredsState";
            this.lblInsuredsState.Size = new System.Drawing.Size(45, 15);
            this.lblInsuredsState.TabIndex = 28;
            // 
            // lblInsuredsCity
            // 
            this.lblInsuredsCity.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsCity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsCity.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsCity.Location = new System.Drawing.Point(575, 244);
            this.lblInsuredsCity.Name = "lblInsuredsCity";
            this.lblInsuredsCity.Size = new System.Drawing.Size(226, 19);
            this.lblInsuredsCity.TabIndex = 27;
            // 
            // lblPatientState
            // 
            this.lblPatientState.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientState.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientState.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientState.Location = new System.Drawing.Point(301, 246);
            this.lblPatientState.Name = "lblPatientState";
            this.lblPatientState.Size = new System.Drawing.Size(28, 15);
            this.lblPatientState.TabIndex = 23;
            // 
            // lblPatientCity
            // 
            this.lblPatientCity.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientCity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCity.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientCity.Location = new System.Drawing.Point(41, 244);
            this.lblPatientCity.Name = "lblPatientCity";
            this.lblPatientCity.Size = new System.Drawing.Size(251, 17);
            this.lblPatientCity.TabIndex = 22;
            // 
            // lblInsuredsAddress
            // 
            this.lblInsuredsAddress.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredsAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredsAddress.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredsAddress.Location = new System.Drawing.Point(575, 211);
            this.lblInsuredsAddress.Name = "lblInsuredsAddress";
            this.lblInsuredsAddress.Size = new System.Drawing.Size(291, 15);
            this.lblInsuredsAddress.TabIndex = 21;
            // 
            // lblRelationship_Other
            // 
            this.lblRelationship_Other.BackColor = System.Drawing.Color.Ivory;
            this.lblRelationship_Other.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRelationship_Other.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelationship_Other.ForeColor = System.Drawing.Color.Black;
            this.lblRelationship_Other.Location = new System.Drawing.Point(520, 209);
            this.lblRelationship_Other.Name = "lblRelationship_Other";
            this.lblRelationship_Other.Size = new System.Drawing.Size(19, 20);
            this.lblRelationship_Other.TabIndex = 20;
            this.lblRelationship_Other.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRelationship_Child
            // 
            this.lblRelationship_Child.BackColor = System.Drawing.Color.Ivory;
            this.lblRelationship_Child.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRelationship_Child.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelationship_Child.ForeColor = System.Drawing.Color.Black;
            this.lblRelationship_Child.Location = new System.Drawing.Point(467, 209);
            this.lblRelationship_Child.Name = "lblRelationship_Child";
            this.lblRelationship_Child.Size = new System.Drawing.Size(19, 20);
            this.lblRelationship_Child.TabIndex = 19;
            this.lblRelationship_Child.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRelationship_Spouse
            // 
            this.lblRelationship_Spouse.BackColor = System.Drawing.Color.Ivory;
            this.lblRelationship_Spouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRelationship_Spouse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelationship_Spouse.ForeColor = System.Drawing.Color.Black;
            this.lblRelationship_Spouse.Location = new System.Drawing.Point(424, 209);
            this.lblRelationship_Spouse.Name = "lblRelationship_Spouse";
            this.lblRelationship_Spouse.Size = new System.Drawing.Size(19, 20);
            this.lblRelationship_Spouse.TabIndex = 18;
            this.lblRelationship_Spouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRelationship_Self
            // 
            this.lblRelationship_Self.BackColor = System.Drawing.Color.Ivory;
            this.lblRelationship_Self.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRelationship_Self.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelationship_Self.ForeColor = System.Drawing.Color.Black;
            this.lblRelationship_Self.Location = new System.Drawing.Point(368, 209);
            this.lblRelationship_Self.Name = "lblRelationship_Self";
            this.lblRelationship_Self.Size = new System.Drawing.Size(19, 20);
            this.lblRelationship_Self.TabIndex = 17;
            this.lblRelationship_Self.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientAddress
            // 
            this.lblPatientAddress.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientAddress.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientAddress.Location = new System.Drawing.Point(41, 210);
            this.lblPatientAddress.Name = "lblPatientAddress";
            this.lblPatientAddress.Size = new System.Drawing.Size(291, 15);
            this.lblPatientAddress.TabIndex = 16;
            // 
            // lblInsuredName
            // 
            this.lblInsuredName.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredName.Location = new System.Drawing.Point(575, 173);
            this.lblInsuredName.Name = "lblInsuredName";
            this.lblInsuredName.Size = new System.Drawing.Size(291, 15);
            this.lblInsuredName.TabIndex = 15;
            // 
            // lblPatient_Female
            // 
            this.lblPatient_Female.BackColor = System.Drawing.Color.Ivory;
            this.lblPatient_Female.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatient_Female.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient_Female.ForeColor = System.Drawing.Color.Black;
            this.lblPatient_Female.Location = new System.Drawing.Point(520, 172);
            this.lblPatient_Female.Name = "lblPatient_Female";
            this.lblPatient_Female.Size = new System.Drawing.Size(19, 20);
            this.lblPatient_Female.TabIndex = 14;
            this.lblPatient_Female.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatient_Male
            // 
            this.lblPatient_Male.BackColor = System.Drawing.Color.Ivory;
            this.lblPatient_Male.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatient_Male.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient_Male.ForeColor = System.Drawing.Color.Black;
            this.lblPatient_Male.Location = new System.Drawing.Point(466, 172);
            this.lblPatient_Male.Name = "lblPatient_Male";
            this.lblPatient_Male.Size = new System.Drawing.Size(19, 20);
            this.lblPatient_Male.TabIndex = 13;
            this.lblPatient_Male.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatientDOB_YY
            // 
            this.lblPatientDOB_YY.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientDOB_YY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientDOB_YY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientDOB_YY.Location = new System.Drawing.Point(414, 176);
            this.lblPatientDOB_YY.Name = "lblPatientDOB_YY";
            this.lblPatientDOB_YY.Size = new System.Drawing.Size(40, 14);
            this.lblPatientDOB_YY.TabIndex = 12;
            this.lblPatientDOB_YY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientDOB_DD
            // 
            this.lblPatientDOB_DD.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientDOB_DD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientDOB_DD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientDOB_DD.Location = new System.Drawing.Point(382, 176);
            this.lblPatientDOB_DD.Name = "lblPatientDOB_DD";
            this.lblPatientDOB_DD.Size = new System.Drawing.Size(25, 14);
            this.lblPatientDOB_DD.TabIndex = 11;
            this.lblPatientDOB_DD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientDOB_MM
            // 
            this.lblPatientDOB_MM.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientDOB_MM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientDOB_MM.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientDOB_MM.Location = new System.Drawing.Point(351, 176);
            this.lblPatientDOB_MM.Name = "lblPatientDOB_MM";
            this.lblPatientDOB_MM.Size = new System.Drawing.Size(25, 14);
            this.lblPatientDOB_MM.TabIndex = 10;
            this.lblPatientDOB_MM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInsuredIdNumber
            // 
            this.lblInsuredIdNumber.BackColor = System.Drawing.Color.Ivory;
            this.lblInsuredIdNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuredIdNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblInsuredIdNumber.Location = new System.Drawing.Point(575, 139);
            this.lblInsuredIdNumber.Name = "lblInsuredIdNumber";
            this.lblInsuredIdNumber.Size = new System.Drawing.Size(291, 15);
            this.lblInsuredIdNumber.TabIndex = 7;
            // 
            // btnPatientBrowse
            // 
            this.btnPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientBrowse.Image")));
            this.btnPatientBrowse.Location = new System.Drawing.Point(312, 166);
            this.btnPatientBrowse.Name = "btnPatientBrowse";
            this.btnPatientBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnPatientBrowse.TabIndex = 9;
            this.btnPatientBrowse.UseVisualStyleBackColor = false;
            // 
            // lblPatientName
            // 
            this.lblPatientName.BackColor = System.Drawing.Color.Ivory;
            this.lblPatientName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPatientName.Location = new System.Drawing.Point(41, 173);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(265, 15);
            this.lblPatientName.TabIndex = 8;
            // 
            // lblOtherInsuranceType
            // 
            this.lblOtherInsuranceType.BackColor = System.Drawing.Color.Ivory;
            this.lblOtherInsuranceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOtherInsuranceType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOtherInsuranceType.ForeColor = System.Drawing.Color.Black;
            this.lblOtherInsuranceType.Location = new System.Drawing.Point(499, 136);
            this.lblOtherInsuranceType.Name = "lblOtherInsuranceType";
            this.lblOtherInsuranceType.Size = new System.Drawing.Size(19, 20);
            this.lblOtherInsuranceType.TabIndex = 6;
            this.lblOtherInsuranceType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFECABlackLung
            // 
            this.lblFECABlackLung.BackColor = System.Drawing.Color.Ivory;
            this.lblFECABlackLung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFECABlackLung.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFECABlackLung.ForeColor = System.Drawing.Color.Black;
            this.lblFECABlackLung.Location = new System.Drawing.Point(433, 136);
            this.lblFECABlackLung.Name = "lblFECABlackLung";
            this.lblFECABlackLung.Size = new System.Drawing.Size(19, 20);
            this.lblFECABlackLung.TabIndex = 5;
            this.lblFECABlackLung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroupHealthPlan
            // 
            this.lblGroupHealthPlan.BackColor = System.Drawing.Color.Ivory;
            this.lblGroupHealthPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGroupHealthPlan.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupHealthPlan.ForeColor = System.Drawing.Color.Black;
            this.lblGroupHealthPlan.Location = new System.Drawing.Point(346, 136);
            this.lblGroupHealthPlan.Name = "lblGroupHealthPlan";
            this.lblGroupHealthPlan.Size = new System.Drawing.Size(19, 20);
            this.lblGroupHealthPlan.TabIndex = 4;
            this.lblGroupHealthPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCHAMPVA
            // 
            this.lblCHAMPVA.BackColor = System.Drawing.Color.Ivory;
            this.lblCHAMPVA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCHAMPVA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCHAMPVA.ForeColor = System.Drawing.Color.Black;
            this.lblCHAMPVA.Location = new System.Drawing.Point(270, 136);
            this.lblCHAMPVA.Name = "lblCHAMPVA";
            this.lblCHAMPVA.Size = new System.Drawing.Size(19, 20);
            this.lblCHAMPVA.TabIndex = 3;
            this.lblCHAMPVA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTricareChampus
            // 
            this.lblTricareChampus.BackColor = System.Drawing.Color.Ivory;
            this.lblTricareChampus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTricareChampus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTricareChampus.ForeColor = System.Drawing.Color.Black;
            this.lblTricareChampus.Location = new System.Drawing.Point(172, 136);
            this.lblTricareChampus.Name = "lblTricareChampus";
            this.lblTricareChampus.Size = new System.Drawing.Size(19, 20);
            this.lblTricareChampus.TabIndex = 2;
            this.lblTricareChampus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMedicaid
            // 
            this.lblMedicaid.BackColor = System.Drawing.Color.Ivory;
            this.lblMedicaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMedicaid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicaid.ForeColor = System.Drawing.Color.Black;
            this.lblMedicaid.Location = new System.Drawing.Point(96, 136);
            this.lblMedicaid.Name = "lblMedicaid";
            this.lblMedicaid.Size = new System.Drawing.Size(19, 20);
            this.lblMedicaid.TabIndex = 1;
            this.lblMedicaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMedicare
            // 
            this.lblMedicare.BackColor = System.Drawing.Color.Ivory;
            this.lblMedicare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMedicare.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicare.ForeColor = System.Drawing.Color.Black;
            this.lblMedicare.Location = new System.Drawing.Point(21, 136);
            this.lblMedicare.Name = "lblMedicare";
            this.lblMedicare.Size = new System.Drawing.Size(19, 20);
            this.lblMedicare.TabIndex = 0;
            this.lblMedicare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPayerNameAndAddress
            // 
            this.lblPayerNameAndAddress.BackColor = System.Drawing.Color.Ivory;
            this.lblPayerNameAndAddress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayerNameAndAddress.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPayerNameAndAddress.Location = new System.Drawing.Point(453, 16);
            this.lblPayerNameAndAddress.Name = "lblPayerNameAndAddress";
            this.lblPayerNameAndAddress.Size = new System.Drawing.Size(301, 92);
            this.lblPayerNameAndAddress.TabIndex = 0;
            // 
            // HCFA1500New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLabel);
            this.Controls.Add(this.txtFacilityCode);
            this.Controls.Add(this.txtTransactionID);
            this.Controls.Add(this.txtFacilityDescription);
            this.Controls.Add(this.pnlTextBox);
            this.Name = "HCFA1500New";
            this.Size = new System.Drawing.Size(900, 1153);
            this.pnlTextBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.pnlLabel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlMainFormLabel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTransactionID;
        private System.Windows.Forms.TextBox txtFacilityCode;
        private System.Windows.Forms.TextBox txtFacilityDescription;
        private System.Drawing.Printing.PrintDocument printdoc_HCFA1500;
        private System.Windows.Forms.Panel pnlTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlMainForm;
        private System.Windows.Forms.Button btnBrowseReferral;
        private System.Windows.Forms.Button btnBrowseFacility;
        private System.Windows.Forms.TextBox txtRenderingProvider1_NPI;
        private System.Windows.Forms.Button btn_PatientBrowse;
        private System.Windows.Forms.Button btn_BrowsePatientRegt;
        private System.Windows.Forms.TextBox txtNotes6;
        private System.Windows.Forms.TextBox txtNotes5;
        private System.Windows.Forms.TextBox txtNotes4;
        private System.Windows.Forms.TextBox txtNotes3;
        private System.Windows.Forms.TextBox txtNotes2;
        private System.Windows.Forms.TextBox txtNotes1;
        private System.Windows.Forms.TextBox txtPayerNameAndAddress;
        private System.Windows.Forms.DateTimePicker dtpPatientSignDate;
        private System.Windows.Forms.DateTimePicker dtpDOS6To;
        private System.Windows.Forms.DateTimePicker dtpDOS6From;
        private System.Windows.Forms.DateTimePicker dtpDOS5To;
        private System.Windows.Forms.DateTimePicker dtpDOS5From;
        private System.Windows.Forms.DateTimePicker dtpDOS4To;
        private System.Windows.Forms.DateTimePicker dtpDOS4From;
        private System.Windows.Forms.DateTimePicker dtpDOS3To;
        private System.Windows.Forms.DateTimePicker dtpDOS3From;
        private System.Windows.Forms.DateTimePicker dtpDOS2To;
        private System.Windows.Forms.DateTimePicker dtpDOS2From;
        private System.Windows.Forms.DateTimePicker dtpDOS1To;
        private System.Windows.Forms.DateTimePicker dtpDOS1From;
        private System.Windows.Forms.DateTimePicker dtpHospitalisationTo;
        private System.Windows.Forms.DateTimePicker dtpHospitalisationFrom;
        private System.Windows.Forms.DateTimePicker dtpUnableToWorkTill;
        private System.Windows.Forms.DateTimePicker dtpUnableToWorkFrom;
        private System.Windows.Forms.DateTimePicker dtpInsuredsDOB;
        private System.Windows.Forms.DateTimePicker dtpPhysicianSignDate;
        private System.Windows.Forms.DateTimePicker dtpSimilarIllnessFirstDate;
        private System.Windows.Forms.DateTimePicker dtpDateOfCurrentIllness;
        private System.Windows.Forms.DateTimePicker dtpPatientDOB;
        private System.Windows.Forms.TextBox txtPatientZip;
        private System.Windows.Forms.TextBox txtInsuredsZip;
        private System.Windows.Forms.TextBox txtInsuredTelephone2;
        private System.Windows.Forms.TextBox txtPatientTelephone2;
        private System.Windows.Forms.TextBox txtInsuredTelephone1;
        private System.Windows.Forms.TextBox txtPatientTelephone1;
        private System.Windows.Forms.TextBox txtPatientState;
        private System.Windows.Forms.TextBox txtInsuredsState;
        private System.Windows.Forms.TextBox txtPatientCity;
        private System.Windows.Forms.TextBox txtInsuredsCity;
        private System.Windows.Forms.TextBox txtReserveForLocalUse;
        private System.Windows.Forms.TextBox txtPatientCondition;
        private System.Windows.Forms.TextBox txtInsuredPolicyorFECANo;
        private System.Windows.Forms.TextBox txtInsuredInsurancePlanName;
        private System.Windows.Forms.TextBox txtInsuredEmployerORSchoolName;
        private System.Windows.Forms.TextBox txtOutsideLabCharges2;
        private System.Windows.Forms.TextBox txtPriorAuthorizationNumber;
        private System.Windows.Forms.TextBox txtOriginalRefNumber;
        private System.Windows.Forms.TextBox txtMedicaidResubmissionCode;
        private System.Windows.Forms.TextBox txtOutsideLabCharges1;
        private System.Windows.Forms.TextBox txtReferringProvider_NPI;
        private System.Windows.Forms.TextBox txtBillingProv_b_UPIN;
        private System.Windows.Forms.TextBox txtBillingProv_a_NPI;
        private System.Windows.Forms.TextBox txtFacility_b;
        private System.Windows.Forms.TextBox txtFacility_a_NPI;
        private System.Windows.Forms.TextBox txtCPT6;
        private System.Windows.Forms.TextBox txtCPT5;
        private System.Windows.Forms.TextBox txtCPT4;
        private System.Windows.Forms.TextBox txtCPT3;
        private System.Windows.Forms.TextBox txtEMG6;
        private System.Windows.Forms.TextBox txtCPT2;
        private System.Windows.Forms.TextBox txtEMG5;
        private System.Windows.Forms.TextBox txtCPT1;
        private System.Windows.Forms.TextBox txtEMG4;
        private System.Windows.Forms.TextBox txtEMG3;
        private System.Windows.Forms.TextBox txtEMG2;
        private System.Windows.Forms.TextBox txtEMG1;
        private System.Windows.Forms.TextBox txtEPSDT6;
        private System.Windows.Forms.TextBox txtEPSDT5;
        private System.Windows.Forms.TextBox txtEPSDT4;
        private System.Windows.Forms.TextBox txtEPSDT3;
        private System.Windows.Forms.TextBox txtBillingProviderPhone1;
        private System.Windows.Forms.TextBox txtAmountPaid2;
        private System.Windows.Forms.TextBox txtTotalCharges2;
        private System.Windows.Forms.TextBox txtCharges61;
        private System.Windows.Forms.TextBox txtEPSDT2;
        private System.Windows.Forms.TextBox txtCharges51;
        private System.Windows.Forms.TextBox txtEPSDT1;
        private System.Windows.Forms.TextBox txtCharges41;
        private System.Windows.Forms.TextBox txtCharges31;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.TextBox txtTotalCharges;
        private System.Windows.Forms.TextBox txtCharges6;
        private System.Windows.Forms.TextBox txtCharges21;
        private System.Windows.Forms.TextBox txtCharges5;
        private System.Windows.Forms.TextBox txtCharges11;
        private System.Windows.Forms.TextBox txtCharges4;
        private System.Windows.Forms.TextBox txtCharges3;
        private System.Windows.Forms.TextBox txtBillingProviderPhone2;
        private System.Windows.Forms.TextBox txtRenderingProvider6_NPI;
        private System.Windows.Forms.TextBox txtCharges2;
        private System.Windows.Forms.TextBox txtRenderingProvider5_NPI;
        private System.Windows.Forms.TextBox txtCharges1;
        private System.Windows.Forms.TextBox txtRenderingProvider4_NPI;
        private System.Windows.Forms.TextBox txtRenderingProvider3_NPI;
        private System.Windows.Forms.TextBox txtUnits6;
        private System.Windows.Forms.TextBox txtRenderingProvider2_NPI;
        private System.Windows.Forms.TextBox txtUnits5;
        private System.Windows.Forms.TextBox txtUnits4;
        private System.Windows.Forms.TextBox txtUnits3;
        private System.Windows.Forms.TextBox txtDxPtr6;
        private System.Windows.Forms.TextBox txtUnits2;
        private System.Windows.Forms.TextBox txtDxPtr5;
        private System.Windows.Forms.TextBox txtUnits1;
        private System.Windows.Forms.TextBox txtDxPtr4;
        private System.Windows.Forms.TextBox txtMOD64;
        private System.Windows.Forms.TextBox txtDxPtr3;
        private System.Windows.Forms.TextBox txtMOD54;
        private System.Windows.Forms.TextBox txtDxPtr2;
        private System.Windows.Forms.TextBox txtMOD44;
        private System.Windows.Forms.TextBox txtMOD34;
        private System.Windows.Forms.TextBox txtMOD63;
        private System.Windows.Forms.TextBox txtMOD24;
        private System.Windows.Forms.TextBox txtMOD53;
        private System.Windows.Forms.TextBox txtDxPtr1;
        private System.Windows.Forms.TextBox txtMOD43;
        private System.Windows.Forms.TextBox txtMOD33;
        private System.Windows.Forms.TextBox txtMOD62;
        private System.Windows.Forms.TextBox txtMOD23;
        private System.Windows.Forms.TextBox txtMOD52;
        private System.Windows.Forms.TextBox txtMOD14;
        private System.Windows.Forms.TextBox txtMOD42;
        private System.Windows.Forms.TextBox txtMOD61;
        private System.Windows.Forms.TextBox txtMOD32;
        private System.Windows.Forms.TextBox txtMOD51;
        private System.Windows.Forms.TextBox txtMOD22;
        private System.Windows.Forms.TextBox txtMOD41;
        private System.Windows.Forms.TextBox txtPOS6;
        private System.Windows.Forms.TextBox txtMOD31;
        private System.Windows.Forms.TextBox txtPOS5;
        private System.Windows.Forms.TextBox txtMOD13;
        private System.Windows.Forms.TextBox txtPOS4;
        private System.Windows.Forms.TextBox txtMOD21;
        private System.Windows.Forms.TextBox txtPOS3;
        private System.Windows.Forms.TextBox txtMOD12;
        private System.Windows.Forms.TextBox txtPOS2;
        private System.Windows.Forms.TextBox txtMOD11;
        private System.Windows.Forms.TextBox txtPOS1;
        private System.Windows.Forms.TextBox txtPhyscianSignature;
        private System.Windows.Forms.TextBox txtDiagnosisCode41;
        private System.Windows.Forms.TextBox txtDiagnosisCode31;
        private System.Windows.Forms.TextBox txtBillingProviderInfo;
        private System.Windows.Forms.TextBox txtInsuredPersonSign;
        private System.Windows.Forms.TextBox txtPatientSignature;
        private System.Windows.Forms.TextBox txtPatientAccountNo;
        private System.Windows.Forms.TextBox txtFederalTaxID;
        private System.Windows.Forms.TextBox txtDiagnosisCode21;
        private System.Windows.Forms.TextBox txtDiagnosisCode11;
        private System.Windows.Forms.TextBox txtReferringProviderName;
        private System.Windows.Forms.TextBox txtOtherInsuredInsuranceName;
        private System.Windows.Forms.TextBox txt19ReservedForLocalUse;
        private System.Windows.Forms.TextBox txtOtherInsuredPolicyNo;
        private System.Windows.Forms.TextBox txtOtherInsuredName;
        private System.Windows.Forms.TextBox txtPatientAddress;
        private System.Windows.Forms.TextBox txtInsuredsAddress;
        private System.Windows.Forms.TextBox txtInsuredName;
        private System.Windows.Forms.TextBox txtInsuredIdNumber;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.CheckBox chkPatient_Female;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_OtherAccident_No;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_OtherAccident_Yes;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_AutoAccident_No;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_AutoAccident_Yes;
        private System.Windows.Forms.CheckBox chkIsOtherHealthPlan_Yes;
        private System.Windows.Forms.CheckBox chkInsuredSex_Male;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_Employment_Yes;
        private System.Windows.Forms.CheckBox chkOutsideLab_No;
        private System.Windows.Forms.CheckBox chkAcceptAssignment_No;
        private System.Windows.Forms.CheckBox chkAcceptAssignment_Yes;
        private System.Windows.Forms.CheckBox chkFederalTaxID_EIN;
        private System.Windows.Forms.CheckBox chkFederalTaxID_SSN;
        private System.Windows.Forms.CheckBox chkOutsideLab_Yes;
        private System.Windows.Forms.CheckBox chkIsOtherHealthPlan_No;
        private System.Windows.Forms.CheckBox chkInsuredSex_Female;
        private System.Windows.Forms.CheckBox chkPatientCoditionRelatedTo_Employment_No;
        private System.Windows.Forms.CheckBox chkRelationship_Other;
        private System.Windows.Forms.CheckBox chkRelationship_Child;
        private System.Windows.Forms.CheckBox chkRelationship_Spouse;
        private System.Windows.Forms.CheckBox chkRelationship_Self;
        private System.Windows.Forms.CheckBox chkPatient_Male;
        private System.Windows.Forms.CheckBox chkOtherInsuranceType;
        private System.Windows.Forms.CheckBox chkFECABlackLung;
        private System.Windows.Forms.CheckBox chkGroupHealthPlan;
        private System.Windows.Forms.CheckBox chkCHAMPVA;
        private System.Windows.Forms.CheckBox chkTricareChampus;
        private System.Windows.Forms.CheckBox chkMedicaid;
        private System.Windows.Forms.CheckBox chkMedicare;
        private System.Windows.Forms.ComboBox cmbRenderingProvider1;
        private System.Windows.Forms.ComboBox cmbRenderingProvider2;
        private System.Windows.Forms.ComboBox cmbRenderingProvider3;
        private System.Windows.Forms.ComboBox cmbRenderingProvider4;
        private System.Windows.Forms.ComboBox cmbRenderingProvider5;
        private System.Windows.Forms.ComboBox cmbRenderingProvider6;
        private System.Windows.Forms.Panel pnlLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblBottom;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Panel pnlMainFormLabel;
        private System.Windows.Forms.Label lblMedicare;
        private System.Windows.Forms.Label lblPayerNameAndAddress;
        private System.Windows.Forms.Label lblFECABlackLung;
        private System.Windows.Forms.Label lblGroupHealthPlan;
        private System.Windows.Forms.Label lblCHAMPVA;
        private System.Windows.Forms.Label lblTricareChampus;
        private System.Windows.Forms.Label lblMedicaid;
        private System.Windows.Forms.Label lblOtherInsuranceType;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblInsuredIdNumber;
        private System.Windows.Forms.Button btnPatientBrowse;
        private System.Windows.Forms.Label lblPatientDOB_MM;
        private System.Windows.Forms.Label lblPatient_Male;
        private System.Windows.Forms.Label lblPatientDOB_YY;
        private System.Windows.Forms.Label lblPatientDOB_DD;
        private System.Windows.Forms.Label lblPatient_Female;
        private System.Windows.Forms.Label lblRelationship_Other;
        private System.Windows.Forms.Label lblRelationship_Child;
        private System.Windows.Forms.Label lblRelationship_Spouse;
        private System.Windows.Forms.Label lblRelationship_Self;
        private System.Windows.Forms.Label lblPatientAddress;
        private System.Windows.Forms.Label lblInsuredName;
        private System.Windows.Forms.Label lblPatientState;
        private System.Windows.Forms.Label lblPatientCity;
        private System.Windows.Forms.Label lblInsuredsAddress;
        private System.Windows.Forms.Label lblInsuredsState;
        private System.Windows.Forms.Label lblInsuredsCity;
        private System.Windows.Forms.Label lblPatientZip;
        private System.Windows.Forms.Label lblPatientTelephone2;
        private System.Windows.Forms.Label lblPatientTelephone1;
        private System.Windows.Forms.Label lblInsuredTelephone1;
        private System.Windows.Forms.Label lblInsuredsZip;
        private System.Windows.Forms.Label lblInsuredTelephone2;
        private System.Windows.Forms.Label lblOtherInsuredName;
        private System.Windows.Forms.Label lblInsuredPolicyorFECANo;
        private System.Windows.Forms.Label lblPatientCondition;
        private System.Windows.Forms.Label lblOtherInsuredPolicyNo;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_Employment_No;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_Employment_Yes;
        private System.Windows.Forms.Label lblInsuredsDOB_YY;
        private System.Windows.Forms.Label lblInsuredsDOB_DD;
        private System.Windows.Forms.Label lblInsuredsDOB_MM;
        private System.Windows.Forms.Label lblInsuredSex_Female;
        private System.Windows.Forms.Label lblInsuredSex_Male;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_AutoAccident_No;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_AutoAccident_Yes;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_State;
        private System.Windows.Forms.Label lblInsuredEmployerORSchoolName;
        private System.Windows.Forms.Label lblOtherInsuredEmployerORSchoolName;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_OtherAccident_No;
        private System.Windows.Forms.Label lblPatientCoditionRelatedTo_OtherAccident_Yes;
        private System.Windows.Forms.Label lblInsuredInsurancePlanName;
        private System.Windows.Forms.Label lblOtherInsuredInsuranceName;
        private System.Windows.Forms.Label lblReserveForLocalUse;
        private System.Windows.Forms.Button btnBrowsePatientRegt;
        private System.Windows.Forms.Label lblIsOtherHealthPlan_No;
        private System.Windows.Forms.Label lblIsOtherHealthPlan_Yes;
        private System.Windows.Forms.Label lblPatientSignature;
        private System.Windows.Forms.Label lblPatientSignDate_MM;
        private System.Windows.Forms.Label lblInsuredPersonSign;
        private System.Windows.Forms.Label lblDateOfCurrentIllness_YY;
        private System.Windows.Forms.Label lblDateOfCurrentIllness_DD;
        private System.Windows.Forms.Label lblDateOfCurrentIllness_MM;
        private System.Windows.Forms.Label lblSimilarIllnessFirstDate_YY;
        private System.Windows.Forms.Label lblSimilarIllnessFirstDate_DD;
        private System.Windows.Forms.Label lblSimilarIllnessFirstDate_MM;
        private System.Windows.Forms.Label lblUnableToWorkFrom_YY;
        private System.Windows.Forms.Label lblUnableToWorkFrom_DD;
        private System.Windows.Forms.Label lblUnableToWorkFrom_MM;
        private System.Windows.Forms.Label lblUnableToWorkTill_YY;
        private System.Windows.Forms.Label lblUnableToWorkTill_DD;
        private System.Windows.Forms.Label lblUnableToWorkTill_MM;
        private System.Windows.Forms.Label lblReferringProvider_NPI;
        private System.Windows.Forms.Button btn_BrowseReferral;
        private System.Windows.Forms.Label lblReferringProviderName;
        private System.Windows.Forms.Label lblHospitalisationFrom_YY;
        private System.Windows.Forms.Label lblHospitalisationFrom_DD;
        private System.Windows.Forms.Label lblHospitalisationFrom_MM;
        private System.Windows.Forms.Label lblHospitalisationTo_YY;
        private System.Windows.Forms.Label lblHospitalisationTo_DD;
        private System.Windows.Forms.Label lblHospitalisationTo_MM;
        private System.Windows.Forms.Label lblOutsideLabCharges2;
        private System.Windows.Forms.Label lblOutsideLabCharges1;
        private System.Windows.Forms.Label lblOutsideLab_No;
        private System.Windows.Forms.Label lblOutsideLab_Yes;
        private System.Windows.Forms.Label lblReservedForLocalUse;
        private System.Windows.Forms.Label lblDiagnosisCode31;
        private System.Windows.Forms.Label lblDiagnosisCode11;
        private System.Windows.Forms.Label lblMedicaidResubmissionCode;
        private System.Windows.Forms.Label lblOriginalRefNumber;
        private System.Windows.Forms.Label lblDiagnosisCode21;
        private System.Windows.Forms.Label lblDiagnosisCode41;
        private System.Windows.Forms.Label lblPriorAuthorizationNumber;
        private System.Windows.Forms.Label lblNotes1;
        private System.Windows.Forms.Label lblDOS1To_YY;
        private System.Windows.Forms.Label lblDOS1To_DD;
        private System.Windows.Forms.Label lblDOS1To_MM;
        private System.Windows.Forms.Label lblDOS1From_YY;
        private System.Windows.Forms.Label lblDOS1From_DD;
        private System.Windows.Forms.Label lblDOS1From_MM;
        private System.Windows.Forms.Label lblMOD14;
        private System.Windows.Forms.Label lblMOD13;
        private System.Windows.Forms.Label lblMOD12;
        private System.Windows.Forms.Label lblMOD11;
        private System.Windows.Forms.Label lblCPT1;
        private System.Windows.Forms.Label lblEMG1;
        private System.Windows.Forms.Label lblPOS1;
        private System.Windows.Forms.Label lblRenderingProvider1_NPI;
        private System.Windows.Forms.Label lblEPSDT1;
        private System.Windows.Forms.Label lblUnits1;
        private System.Windows.Forms.Label lblCharges11;
        private System.Windows.Forms.Label lblCharges1;
        private System.Windows.Forms.Label lblDxPtr1;
        private System.Windows.Forms.Label lblRenderingProvider2_NPI;
        private System.Windows.Forms.Label lblEPSDT2;
        private System.Windows.Forms.Label lblUnits2;
        private System.Windows.Forms.Label lblCharges21;
        private System.Windows.Forms.Label lblCharges2;
        private System.Windows.Forms.Label lblDxPtr2;
        private System.Windows.Forms.Label lblMOD24;
        private System.Windows.Forms.Label lblMOD23;
        private System.Windows.Forms.Label lblMOD22;
        private System.Windows.Forms.Label lblMOD21;
        private System.Windows.Forms.Label lblCPT2;
        private System.Windows.Forms.Label lblEMG2;
        private System.Windows.Forms.Label lblPOS2;
        private System.Windows.Forms.Label lblDOS2To_YY;
        private System.Windows.Forms.Label lblDOS2To_DD;
        private System.Windows.Forms.Label lblDOS2To_MM;
        private System.Windows.Forms.Label lblDOS2From_YY;
        private System.Windows.Forms.Label lblDOS2From_DD;
        private System.Windows.Forms.Label lblDOS2From_MM;
        private System.Windows.Forms.Label lblRenderingProvider3_NPI;
        private System.Windows.Forms.Label lblEPSDT3;
        private System.Windows.Forms.Label lblUnits3;
        private System.Windows.Forms.Label lblCharges31;
        private System.Windows.Forms.Label lblCharges3;
        private System.Windows.Forms.Label lblDxPtr3;
        private System.Windows.Forms.Label lblMOD34;
        private System.Windows.Forms.Label lblMOD33;
        private System.Windows.Forms.Label lblMOD32;
        private System.Windows.Forms.Label lblMOD31;
        private System.Windows.Forms.Label lblCPT3;
        private System.Windows.Forms.Label lblEMG3;
        private System.Windows.Forms.Label lblPOS3;
        private System.Windows.Forms.Label lblDOS3To_YY;
        private System.Windows.Forms.Label lblDOS3To_DD;
        private System.Windows.Forms.Label lblDOS3To_MM;
        private System.Windows.Forms.Label lblDOS3From_YY;
        private System.Windows.Forms.Label lblDOS3From_DD;
        private System.Windows.Forms.Label lblDOS3From_MM;
        private System.Windows.Forms.Label lblRenderingProvider4_NPI;
        private System.Windows.Forms.Label lblEPSDT4;
        private System.Windows.Forms.Label lblUnits4;
        private System.Windows.Forms.Label lblCharges41;
        private System.Windows.Forms.Label lblCharges4;
        private System.Windows.Forms.Label lblDxPtr4;
        private System.Windows.Forms.Label lblMOD44;
        private System.Windows.Forms.Label lblMOD43;
        private System.Windows.Forms.Label lblMOD42;
        private System.Windows.Forms.Label lblMOD41;
        private System.Windows.Forms.Label lblCPT4;
        private System.Windows.Forms.Label lblEMG4;
        private System.Windows.Forms.Label lblPOS4;
        private System.Windows.Forms.Label lblDOS4To_YY;
        private System.Windows.Forms.Label lblDOS4To_DD;
        private System.Windows.Forms.Label lblDOS4To_MM;
        private System.Windows.Forms.Label lblDOS4From_YY;
        private System.Windows.Forms.Label lblDOS4From_DD;
        private System.Windows.Forms.Label lblDOS4From_MM;
        private System.Windows.Forms.Label lblRenderingProvider5_NPI;
        private System.Windows.Forms.Label lblEPSDT5;
        private System.Windows.Forms.Label lblUnits5;
        private System.Windows.Forms.Label lblCharges51;
        private System.Windows.Forms.Label lblCharges5;
        private System.Windows.Forms.Label lblDxPtr5;
        private System.Windows.Forms.Label lblMOD54;
        private System.Windows.Forms.Label lblMOD53;
        private System.Windows.Forms.Label lblMOD52;
        private System.Windows.Forms.Label lblMOD51;
        private System.Windows.Forms.Label lblCPT5;
        private System.Windows.Forms.Label lblEMG5;
        private System.Windows.Forms.Label lblPOS5;
        private System.Windows.Forms.Label lblDOS5To_YY;
        private System.Windows.Forms.Label lblDOS5To_DD;
        private System.Windows.Forms.Label lblDOS5To_MM;
        private System.Windows.Forms.Label lblDOS5From_YY;
        private System.Windows.Forms.Label lblDOS5From_DD;
        private System.Windows.Forms.Label lblDOS5From_MM;
        private System.Windows.Forms.Label lblRenderingProvider6_NPI;
        private System.Windows.Forms.Label lblEPSDT6;
        private System.Windows.Forms.Label lblUnits6;
        private System.Windows.Forms.Label lblCharges61;
        private System.Windows.Forms.Label lblCharges6;
        private System.Windows.Forms.Label lblDxPtr6;
        private System.Windows.Forms.Label lblMOD64;
        private System.Windows.Forms.Label lblMOD63;
        private System.Windows.Forms.Label lblMOD62;
        private System.Windows.Forms.Label lblMOD61;
        private System.Windows.Forms.Label lblCPT6;
        private System.Windows.Forms.Label lblEMG6;
        private System.Windows.Forms.Label lblPOS6;
        private System.Windows.Forms.Label lblDOS6To_YY;
        private System.Windows.Forms.Label lblDOS6To_DD;
        private System.Windows.Forms.Label lblDOS6To_MM;
        private System.Windows.Forms.Label lblDOS6From_YY;
        private System.Windows.Forms.Label lblDOS6From_DD;
        private System.Windows.Forms.Label lblDOS6From_MM;
        private System.Windows.Forms.Label lblFederalTaxID;
        private System.Windows.Forms.Label lblFederalTaxID_EIN;
        private System.Windows.Forms.Label lblFederalTaxID_SSN;
        private System.Windows.Forms.Label lblPatientAccountNo;
        private System.Windows.Forms.Label lblAcceptAssignment_No;
        private System.Windows.Forms.Label lblAcceptAssignment_Yes;
        private System.Windows.Forms.Label lblTotalCharges;
        private System.Windows.Forms.Label lblBalanceDue;
        private System.Windows.Forms.Label lblAmountPaid;
        private System.Windows.Forms.Label lblTotalCharges2;
        private System.Windows.Forms.Label lblBalanceDue2;
        private System.Windows.Forms.Label lblPhyscianSignature;
        private System.Windows.Forms.Label lblPhysicianSignDate_YY;
        private System.Windows.Forms.Label lblPhysicianSignDate_DD;
        private System.Windows.Forms.Label lblPhysicianSignDate_MM;
        private System.Windows.Forms.Label lblFacilityInfo;
        private System.Windows.Forms.Label lblBillingProviderPhone1;
        private System.Windows.Forms.Label lblBillingProviderPhone2;
        private System.Windows.Forms.Label lblFacility_b;
        private System.Windows.Forms.Label lblFacility_a_NPI;
        private System.Windows.Forms.Label lblBillingProviderInfo;
        private System.Windows.Forms.Label lblBillingProv_a_NPI;
        private System.Windows.Forms.Label lblBillingProv_b_UPIN;
        private System.Windows.Forms.Button btn_BrowseFacility;
        private System.Windows.Forms.Label lblNotes2;
        private System.Windows.Forms.Label lblNotes3;
        private System.Windows.Forms.Label lblNotes4;
        private System.Windows.Forms.Label lblNotes5;
        private System.Windows.Forms.Label lblNotes6;
        private System.Windows.Forms.Label lblAmountPaid1;
        private System.Windows.Forms.Label lblPatientSignDate_YY;
        private System.Windows.Forms.Label lblPatientSignDate_DD;
        private System.Windows.Forms.TextBox txtRenderingProvider1_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider6_QualifierValue;
        private System.Windows.Forms.TextBox txtRenderingProvider6_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider5_QualifierValue;
        private System.Windows.Forms.TextBox txtRenderingProvider5_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider4_QualifierValue;
        private System.Windows.Forms.TextBox txtRenderingProvider4_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider3_QualifierValue;
        private System.Windows.Forms.TextBox txtRenderingProvider3_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider2_QualifierValue;
        private System.Windows.Forms.TextBox txtRenderingProvider2_Qualifier;
        private System.Windows.Forms.TextBox txtRenderingProvider1_QualifierValue;
        private System.Windows.Forms.Label lblRenderingProvider2_QF;
        private System.Windows.Forms.Label lblRenderingProvider1_QF;
        private System.Windows.Forms.Label lblRenderingProvider6_QF;
        private System.Windows.Forms.Label lblRenderingProvider5_QF;
        private System.Windows.Forms.Label lblRenderingProvider4_QF;
        private System.Windows.Forms.Label lblRenderingProvider3_QF;
        private System.Windows.Forms.Label lblRenderingProvider6_QFValue;
        private System.Windows.Forms.Label lblRenderingProvider5_QFValue;
        private System.Windows.Forms.Label lblRenderingProvider4_QFValue;
        private System.Windows.Forms.Label lblRenderingProvider3_QFValue;
        private System.Windows.Forms.Label lblRenderingProvider2_QFValue;
        private System.Windows.Forms.Label lblRenderingProvider1_QFValue;
        private System.Windows.Forms.TextBox txtFacilityInfo;
        private System.Windows.Forms.TextBox txtReferringProvider_OtherType;
        private System.Windows.Forms.TextBox txtReferringProvider_OtherValue;
        private System.Windows.Forms.Label lblReferringProvider_OtherValue;
        private System.Windows.Forms.Label lblReferringProvider_OtherType;
        private System.Windows.Forms.Label lblPhyscianQualifierValue;
        private System.Windows.Forms.TextBox txtPhyscianQualifierValue;
        private System.Windows.Forms.TextBox txtEPSDTShaded1;
        private System.Windows.Forms.Label lblShadedEPSDT1;
        private System.Windows.Forms.TextBox txtEPSDTShaded6;
        private System.Windows.Forms.TextBox txtEPSDTShaded5;
        private System.Windows.Forms.TextBox txtEPSDTShaded4;
        private System.Windows.Forms.TextBox txtEPSDTShaded3;
        private System.Windows.Forms.TextBox txtEPSDTShaded2;
        private System.Windows.Forms.Label lblShadedEPSDT6;
        private System.Windows.Forms.Label lblShadedEPSDT5;
        private System.Windows.Forms.Label lblShadedEPSDT4;
        private System.Windows.Forms.Label lblEPSDTShaded3;
        private System.Windows.Forms.Label lblShadedEPSDT2;
        private System.Windows.Forms.TextBox txtReferringProviderQualifier;
        private System.Windows.Forms.TextBox txtIcdInd2;
        private System.Windows.Forms.TextBox txtIcdInd1;
        private System.Windows.Forms.TextBox txtOtherClaimIDQualifier;
        private System.Windows.Forms.TextBox txtDiagnosisCodeL;
        private System.Windows.Forms.TextBox txtDiagnosisCodeK;
        private System.Windows.Forms.TextBox txtDiagnosisCodeJ;
        private System.Windows.Forms.TextBox txtDiagnosisCodeI;
        private System.Windows.Forms.TextBox txtDiagnosisCodeH;
        private System.Windows.Forms.TextBox txtDiagnosisCodeG;
        private System.Windows.Forms.TextBox txtDiagnosisCodeF;
        private System.Windows.Forms.TextBox txtDiagnosisCodeE;
        private System.Windows.Forms.TextBox txtSimilarIllnessFirstDateQualifier;
        private System.Windows.Forms.TextBox txtDateOfCurrentIllnessQualifier;
        private System.Windows.Forms.TextBox txtPatientCoditionRelatedTo_State;
        private System.Windows.Forms.Label lblOtherClaimIDQualifier;
        private System.Windows.Forms.Label lblIcdInd1;
        private System.Windows.Forms.Label lblIcdInd2;
        private System.Windows.Forms.Label lblDiagnosisCodeE;
        private System.Windows.Forms.Label lblDiagnosisCodeF;
        private System.Windows.Forms.Label lblDiagnosisCodeG;
        private System.Windows.Forms.Label lblDiagnosisCodeH;
        private System.Windows.Forms.Label lblDiagnosisCodeI;
        private System.Windows.Forms.Label lblDiagnosisCodeJ;
        private System.Windows.Forms.Label lblDiagnosisCodeK;
        private System.Windows.Forms.Label lblDiagnosisCodeL;
        private System.Windows.Forms.Label lblReferringProviderQualifier;
        private System.Windows.Forms.Label lblSimilarIllnessFirstDateQualifier;
        private System.Windows.Forms.Label lblDateOfCurrentIllnessQualifier;
    }
}
