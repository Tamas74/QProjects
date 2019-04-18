using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace gloBilling
{
    public class C4Authorization
    {

        private C4Auth_AuthorizationRequest _C4_Auth_AuthorizationRequest = null;
        public C4Auth_AuthorizationRequest C4_Auth_AuthorizationRequest
        {
            get { return _C4_Auth_AuthorizationRequest; }
            set { _C4_Auth_AuthorizationRequest = value; }
        }

        private CarrierResponseToAuthorizationRequest _CarrierResponseToAuthorizationRequest = null;
        public CarrierResponseToAuthorizationRequest C4_Auth_CarrierResponseToAuthorizationRequest
        {
            get { return _CarrierResponseToAuthorizationRequest; }
            set { _CarrierResponseToAuthorizationRequest = value; }
        }

        private C4AuthFooter _C4AuthFooter = null;
        public C4AuthFooter C4_Auth_Footer
        {
            get { return _C4AuthFooter; }
            set { _C4AuthFooter = value; }
        }

        public C4Authorization()
        {
            _C4_Auth_AuthorizationRequest = new C4Auth_AuthorizationRequest();
            _CarrierResponseToAuthorizationRequest = new CarrierResponseToAuthorizationRequest();
            _C4AuthFooter = new C4AuthFooter();
        }

        public string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(
                typeof(DisplayNameAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }

        [DisplayName("topmostSubform[0].Page1[0].TextField6[0]")]
        public string WCBCaseNo { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField6[1]")]
        public string CarrierCaseNo { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField6[2]")]
        public string DateOfInjury { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Text4[0]")]
        public string PatientName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text5[0]")]
        public string SSN { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text6[0]")]
        public string PatientFullAddress { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text7[0]")]
        public string EmployerName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text8[0]")]
        public string EmployerFullAddress { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text9[0]")]
        public string InsuranceCarrierName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text10[0]")]
        public string InsuranceCarrierFullAddress { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text11[0]")]
        public string AttendingDoctorsName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text12[0]")]
        public string AttendingDoctorsFullAddress { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text13[0]")]
        public string ProviderAuthorizationNo { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text14[0]")]
        public string ProviderPhoneNo { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text15[0]")]
        public string ProviderFaxNo { get; set; }

    }

    public class C4Auth_AuthorizationRequest
    {
        public C4Auth_AuthorizationRequested_DiagnosticTests C4_Auth_AuthorizationRequested_DiagnosticTests { get; set; }
        public C4Auth_AuthorizationRequested_Therapy C4_Auth_AuthorizationRequested_Therapy { get; set; }
        public C4Auth_AuthorizationRequested_Surgery C4_Auth_AuthorizationRequested_Surgery { get; set; }
        public C4Auth_AuthorizationRequested_Treatment C4_Auth_AuthorizationRequested_Treatment { get; set; }
        public C4Auth_MedicalTreatmentGuidelines C4_Auth_MedicalTreatmentGuidelines { get; set; }
        public C4Auth_StatementOfMedicalNecessity C4_Auth_StatementOfMedicalNecessity { get; set; }

        public C4Auth_AuthorizationRequest()
        {
            C4_Auth_AuthorizationRequested_DiagnosticTests = new C4Auth_AuthorizationRequested_DiagnosticTests();
            C4_Auth_AuthorizationRequested_Therapy = new C4Auth_AuthorizationRequested_Therapy();
            C4_Auth_AuthorizationRequested_Surgery = new C4Auth_AuthorizationRequested_Surgery();
            C4_Auth_AuthorizationRequested_Treatment = new C4Auth_AuthorizationRequested_Treatment();
            C4_Auth_MedicalTreatmentGuidelines = new C4Auth_MedicalTreatmentGuidelines();
            C4_Auth_StatementOfMedicalNecessity = new C4Auth_StatementOfMedicalNecessity();
        }

        public class C4Auth_AuthorizationRequested_DiagnosticTests
        {
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[0]")]
            public bool blnRadiologyServices { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text18[0]")]
            public string RadiologyServicesDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[1]")]
            public bool blnRadiologyServicesGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[2]")]
            public bool blnRadiologyServicesGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[3]")]
            public bool blnRadiologyServicesDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[4]")]
            public bool blnOther_DiagnosticTests { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text19[0]")]
            public string Other_DiagnosticTestsDetails { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[5]")]
            public bool blnOther_DiagnosticTestsGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[6]")]
            public bool blnOther_DiagnosticTestsGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[7]")]
            public bool blnOther_DiagnosticTestsDenied { get; set; }

        }

        public class C4Auth_AuthorizationRequested_Therapy
        {
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[8]")]
            public bool blnPhysicalTherapy { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text18[1]")]
            public string PhysicalTherapyDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].TextField1[0]")]
            public string PhysicalTherapyTimesPerWeek { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].TextField1[1]")]
            public string PhysicalTherapyForWeeks { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[9]")]
            public bool blnPhysicalTherapyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[10]")]
            public bool blnPhysicalTherapyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[11]")]
            public bool blnPhysicalTherapyDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[12]")]
            public bool blnOccupationalTherapy { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text18[2]")]
            public string OccupationalTherapyDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].TextField1[2]")]
            public string OccupationalTherapyTimesPerWeek { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].TextField1[3]")]
            public string OccupationalTherapyForWeeks { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[13]")]
            public bool blnOccupationalTherapyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[14]")]
            public bool blnOccupationalTherapyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[15]")]
            public bool blnOccupationalTherapyDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[16]")]
            public bool blnOther_Therapy { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text19[1]")]
            public string Other_TherapyDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[17]")]
            public bool blnOther_TherapyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[18]")]
            public bool blnOther_TherapyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[19]")]
            public bool blnOther_TherapyDenied { get; set; }

        }

        public class C4Auth_AuthorizationRequested_Surgery
        {
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[20]")]
            public bool blnTypeofSurgery { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text18[3]")]
            public string TypeofSurgeryDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[21]")]
            public bool blnTypeofSurgeryGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[22]")]
            public bool blnTypeofSurgeryGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[23]")]
            public bool blnTypeofSurgeryDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].Text19[2]")]
            public string SurgeryDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[24]")]
            public bool blnSurgeryGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[25]")]
            public bool blnSurgeryGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[26]")]
            public bool blnSurgeryDenied { get; set; }

        }

        public class C4Auth_AuthorizationRequested_Treatment
        {
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[27]")]
            public bool blnOther_Treatment { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text19[3]")]
            public string Other_TreatmentDetails { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[28]")]
            public bool blnOther_TreatmentGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[29]")]
            public bool blnOther_TreatmentGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[30]")]
            public bool blnOther_TreatmentDenied { get; set; }
        }

        public class C4Auth_MedicalTreatmentGuidelines
        {
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[31]")]
            public bool blnLumbarFusions { get; set; }
            [DisplayName(@"topmostSubform[0].Page1[0].Text36\.0[0]")]
            public string LumbarFusionsCode { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[32]")]
            public bool blnLumbarFusionsGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[33]")]
            public bool blnLumbarFusionsGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[34]")]
            public bool blnLumbarFusionsDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[35]")]
            public bool blnArtificialDiskReplacement { get; set; }
            [DisplayName(@"topmostSubform[0].Page1[0].Text36\.1\.0[0]")]
            public string ArtificialDiskReplacementCode0 { get; set; }
            [DisplayName(@"topmostSubform[0].Page1[0].Text36\.1\.1[0]")]
            public string ArtificialDiskReplacementCode1 { get; set; }
            [DisplayName(@"topmostSubform[0].Page1[0].Text36\.1\.2[0]")]
            public string ArtificialDiskReplacementCode2 { get; set; }
            [DisplayName(@"topmostSubform[0].Page1[0].Text36\.1\.3[0]")]
            public string ArtificialDiskReplacementCode3 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[36]")]
            public bool blnArtificialDiskReplacementGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[37]")]
            public bool blnArtificialDiskReplacementGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[38]")]
            public bool blnArtificialDiskReplacementDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[39]")]
            public bool blnVertebroplasty { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[40]")]
            public bool blnVertebroplastyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[41]")]
            public bool blnVertebroplastyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[42]")]
            public bool blnVertebroplastyDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[43]")]
            public bool blnKyphoplasty { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[44]")]
            public bool blnKyphoplastyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[45]")]
            public bool blnKyphoplastyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[46]")]
            public bool blnKyphoplastyDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[47]")]
            public bool blnElectricalBoneGrowthStimulators { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text37[0]")]
            public string ElectricalBoneGrowthStimulatorsCode0 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text38[0]")]
            public string ElectricalBoneGrowthStimulatorsCode1 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text39[0]")]
            public string ElectricalBoneGrowthStimulatorsCode2 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[48]")]
            public bool blnElectricalBoneGrowthStimulatorsGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[49]")]
            public bool blnElectricalBoneGrowthStimulatorsGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[50]")]
            public bool blnElectricalBoneGrowthStimulatorsDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[51]")]
            public bool blnSpinalCordStimulators { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[52]")]
            public bool blnSpinalCordStimulatorsGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[53]")]
            public bool blnSpinalCordStimulatorsGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[54]")]
            public bool blnSpinalCordStimulatorsDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[55]")]
            public bool blnOsteochondralAutograft { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text43[0]")]
            public string OsteochondralAutograftCode { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[56]")]
            public bool blnOsteochondralAutograftGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[57]")]
            public bool blnOsteochondralAutograftGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[58]")]
            public bool blnOsteochondralAutograftDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[59]")]
            public bool blnAutologusChondrocyteImplantation { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text44[0]")]
            public string AutologusChondrocyteImplantationCode0 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text45[0]")]
            public string AutologusChondrocyteImplantationCode1 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[60]")]
            public bool blnAutologusChondrocyteImplantationGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[61]")]
            public bool blnAutologusChondrocyteImplantationGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[62]")]
            public bool blnAutologusChondrocyteImplantationDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[63]")]
            public bool blnMeniscalAllograftTransplantation { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text46[0]")]
            public string MeniscalAllograftTransplantationCode0 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text47[0]")]
            public string MeniscalAllograftTransplantationCode1 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text48[0]")]
            public string MeniscalAllograftTransplantationCode2 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[64]")]
            public bool blnMeniscalAllograftTransplantationGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[65]")]
            public bool blnMeniscalAllograftTransplantationGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[66]")]
            public bool blnMeniscalAllograftTransplantationDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[67]")]
            public bool blnKneeArthroplasty { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text49[0]")]
            public string KneeArthroplastyCode0 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text50[0]")]
            public string KneeArthroplastyCode1 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[68]")]
            public bool blnKneeArthroplastyGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[69]")]
            public bool blnKneeArthroplastyGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[70]")]
            public bool blnKneeArthroplastyDenied { get; set; }

            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[71]")]
            public bool blnSubsequentProcedure { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text51[0]")]
            public string SubsequentProcedureCode0 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text52[0]")]
            public string SubsequentProcedureCode1 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text53[0]")]
            public string SubsequentProcedureCode2 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text54[0]")]
            public string SubsequentProcedureCode3 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].Text55[0]")]
            public string SubsequentProcedureCode4 { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[72]")]
            public bool blnSubsequentProcedureGranted { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[73]")]
            public bool blnSubsequentProcedureGrantedWOPredudice { get; set; }
            [DisplayName("topmostSubform[0].Page1[0].CheckBox1[74]")]
            public bool blnSubsequentProcedureDenied { get; set; }
        }

        public class C4Auth_StatementOfMedicalNecessity
        {
            [DisplayName("topmostSubform[0].Page2[0].TextField3[0]")]
            public string MedicalNecessityStatement1 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField3[1]")]
            public string MedicalNecessityStatement2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField3[2]")]
            public string MedicalNecessityStatement3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField3[3]")]
            public string MedicalNecessityStatement4 { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Text2[0]")]
            public string DateOfServiceInFile { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField4[0]")]
            public string ByFaxOnDate { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField4[1]")]
            public string PersonContactedFax { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].TextField4[2]")]
            public string ByTelephoneOnDate { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField4[3]")]
            public string PersonContactedTelephone { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField4[4]")]
            public string EmailedDate { get; set; }

            public Int64 ProviderID { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].ProviderSignature[0]")]
            public byte[] ProviderSignImage { get; set; }
            
            [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[0]")]
            public string ProviderSignatureDate { get; set; }

        }
    }

    public class CarrierResponseToAuthorizationRequest
    {
        [DisplayName("topmostSubform[0].Page2[0].TextField3[4]")]
        public string ReasonForDenial1 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField3[5]")]
        public string ReasonForDenial2 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField3[6]")]
        public string ReasonForDenial3 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField3[7]")]
        public string ReasonForDenial4 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField3[8]")]
        public string ReasonForDenial5 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField4[5]")]
        public string DateOfServiceInCaseFile { get; set; }

    }

    public class C4AuthFooter
    {
        [DisplayName("topmostSubform[0].Page2[0].TextField2[0]")]
        public string PrintName { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField2[1]")]
        public string Title { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[1]")]
        public string FormDate { get; set; }
    }

}
