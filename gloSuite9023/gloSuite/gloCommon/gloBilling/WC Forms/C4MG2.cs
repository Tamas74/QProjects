using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace gloBilling
{

    public class MG2
    {

        private CaseDetails _CaseDetails = null;
        public CaseDetails MG2_CaseDetails
        {
            get { return _CaseDetails; }
            set { _CaseDetails = value; }
        }

        private PatientDetails _MG2_PatientDetails = null;
        public PatientDetails MG2_PatientDetails
        {
            get { return _MG2_PatientDetails; }
            set { _MG2_PatientDetails = value; }
        }

        private AttendingDoctorInfo _MG2_AttendingDoctorInformation = null;
        public AttendingDoctorInfo MG2_AttendingDoctorInformation
        {
            get { return _MG2_AttendingDoctorInformation; }
            set { _MG2_AttendingDoctorInformation = value; }
        }

        private ApprovalRequestVariance _MG2_ApprovalRequestVarianceFor = null;
        public ApprovalRequestVariance MG2_ApprovalRequestVarianceFor
        {
            get { return _MG2_ApprovalRequestVarianceFor; }
            set { _MG2_ApprovalRequestVarianceFor = value; }
        }

        private MG2HeaderInfo _MG2_HeaderInfo = null;
        public MG2HeaderInfo MG2_HeaderInfo
        {
            get { return _MG2_HeaderInfo; }
            set { _MG2_HeaderInfo = value; }
        }

        private IndependentMedicalExaminer _MG2_IndependentMedicalExaminer = null;
        public IndependentMedicalExaminer MG2_IndependentMedicalExaminer
        {
            get { return _MG2_IndependentMedicalExaminer; }
            set { _MG2_IndependentMedicalExaminer = value; }
        }

        private CarrierResponseToVarianceRequest _MG2_CarrierResponseToVarianceRequest = null;
        public CarrierResponseToVarianceRequest MG2_CarrierResponseToVarianceRequest
        {
            get { return _MG2_CarrierResponseToVarianceRequest; }
            set { _MG2_CarrierResponseToVarianceRequest = value; }
        }

        private DenialInfromallyResolvedInProviderAndCarrier _MG2_DenialInfromallyResolvedInProviderAndCarrier = null;
        public DenialInfromallyResolvedInProviderAndCarrier MG2_DenialInfromallyResolvedInProviderAndCarrier
        {
            get { return _MG2_DenialInfromallyResolvedInProviderAndCarrier; }
            set { _MG2_DenialInfromallyResolvedInProviderAndCarrier = value; }
        }

        private RequestForReviewOfDenial _MG2_RequestForReviewOfDenial = null;
        public RequestForReviewOfDenial MG2_RequestForReviewOfDenial
        {
            get { return _MG2_RequestForReviewOfDenial; }
            set { _MG2_RequestForReviewOfDenial = value; }
        }

        public bool isIndependentMedicalExaminerDeclaration { get; set; }


        public MG2()
        {
            _CaseDetails = new CaseDetails();
            _MG2_PatientDetails = new PatientDetails();
            _MG2_AttendingDoctorInformation = new AttendingDoctorInfo();
            _MG2_ApprovalRequestVarianceFor = new ApprovalRequestVariance();
            _MG2_HeaderInfo = new MG2HeaderInfo();
            _MG2_IndependentMedicalExaminer = new IndependentMedicalExaminer();
            _MG2_CarrierResponseToVarianceRequest = new CarrierResponseToVarianceRequest();
            _MG2_DenialInfromallyResolvedInProviderAndCarrier = new DenialInfromallyResolvedInProviderAndCarrier();
            _MG2_RequestForReviewOfDenial = new RequestForReviewOfDenial();
        }

        public string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(
                typeof(DisplayNameAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }
    }

    public class CaseDetails
    {
        [DisplayName("topmostSubform[0].Page1[0].WCBCaseNo[0]")]
        public string WCBCaseNumber_CaseDetails { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField1[0]")]
        public string CarrierCaseNumber_CaseDetails { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].DateofInjury[0]")]
        public string DateOfInjury_CaseDetails { get; set; }
    }

    public class PatientDetails
    {
        [DisplayName("topmostSubform[0].Page1[0].PatientName[0]")]
        public string PatientsFullName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField2[0]")]
        public string PatientSSN { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField2[1]")]
        public string PatientsFullAddress { get; set; }
    
        [DisplayName("topmostSubform[0].Page1[0].TextField2[2]")]
        public string EmployerNameAndAddress { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].TextField2[3]")]
        public string InsuranceCarrierNameAndAddress { get; set; }

        public void SetPatient() { }

    }

    public class AttendingDoctorInfo
    {
        [DisplayName("topmostSubform[0].Page1[0].TextField2[4]")]
        public string AttendingDoctorsNameAndAddress { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_2\.0[0]")]
        public string IndividualWCBAuthorizationNumber1 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_2\.1[0]")]
        public string IndividualWCBAuthorizationNumber2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_2\.2[0]")]
        public string IndividualWCBAuthorizationNumber3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_3[0]")]
        public string IndividualWCBAuthorizationNumber4 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_4\.0[0]")]
        public string IndividualWCBAuthorizationNumber5 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_4\.1[0]")]
        public string IndividualWCBAuthorizationNumber6 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_4\.2[0]")]
        public string IndividualWCBAuthorizationNumber7 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_4\.3[0]")]
        public string IndividualWCBAuthorizationNumber8 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField2[5]")]
        public string TelephoneNumber { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField2[6]")]
        public string FaxNumber { get; set; }
    }

    public class ApprovalRequestVariance
    {
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.0[0]")]
        public string WCBTreatmentBodyPartIndicator { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.1[0]")]
        public string WCBTreatmentGuidelineSection1 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_10[0]")]
        public string WCBTreatmentGuidelineSection2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.2[0]")]
        public string WCBTreatmentGuidelineSection3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.3[0]")]
        public string WCBTreatmentGuidelineSection4 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[0]")]
        public string ApprovalRequestedForLine0 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[1]")]
        public string ApprovalRequestedForLine1 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[2]")]
        public string ApprovalRequestedForLine2 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[3]")]
        public string ApprovalRequestedForLine3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField2[0]")]
        public string DOSofSupportingWCBCase { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField3[0]")]
        public string DOSPreviouslyDenied { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[4]")]
        public string ApprovalRequestedForLine4 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[5]")]
        public string ApprovalRequestedForLine5 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[6]")]
        public string ApprovalRequestedForLine6 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[7]")]
        public string ApprovalRequestedForLine7 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[8]")]
        public string ApprovalRequestedForLine8 { get; set; }


        [DisplayName("topmostSubform[0].Page1[0].CheckBox1[0]")]
        public bool blnCarrierContactedOnTelephone { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].CheckBox1[1]")]
        public bool blnCarrierNotContactedOnTelephone { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].DateTimeField3[0]")]
        public string CarrierContactedonDate { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[9]")]
        public string CarrierContactedonPersonName { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].CheckBox1[2]")]
        public bool isCarrierContactedByCopyFaxOrEmail { get; set; }
        //[DisplayName("topmostSubform[0].Page1[0].CheckBox1[3]")]
        //public bool isCarrierContactedByCopyEmail { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Approval_Requested_for__one_request_type_per_form_1[10]")]
        public string CarrierContactedByFaxOrEmail { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].CheckBox1[3]")]
        public bool isSenderNotEquipedByFaxOrEmail { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].DateTimeField3[1]")]
        public string CarrierContactedByMailOn { get; set; }

        public Int64 ProviderID { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].ProviderSignature[0]")]
        public byte[] ProviderSignImage { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField3[2]")]
        public string ProviderSignedDate { get; set; }
    }

    public class MG2HeaderInfo
    {
        [DisplayName("topmostSubform[0].Page2[0].PatientName[0]")]
        public string PatientsName_MG2HeaderInfo { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].WCBCaseNo[0]")]
        public string WCBCaseNumber_MG2HeaderInfo { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].DateofInjury[0]")]
        public string DateOfInjury_MG2HeaderInfo { get; set; }
    }

    public class IndependentMedicalExaminer
    {
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[0]")]
        public bool blnCarrierHerebyGivesNotice { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField2[0]")]
        public string IndependentMedicalExaminerName { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField2[1]")]
        public string IndependentMedicalExaminerTitle { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[0]")]
        public string IndependentMedicalExamDate { get; set; }

    }

    public class CarrierResponseToVarianceRequest
    {

        public CarrierResponseToVarianceRequest()
        {
            CarrierResponseToVarianceRequestValue = new CarrierResponseToVarianceRequestType();
            MG2_CarrierUndertaken = new CarrierUndertaken();
        }

        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[0]")]
        public string CarrierResponseToVarianceRequestLine0 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[1]")]
        public string CarrierResponseToVarianceRequestLine1 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[2]")]
        public string CarrierResponseToVarianceRequestLine2 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[3]")]
        public string CarrierResponseToVarianceRequestLine3 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[4]")]
        public string CarrierResponseToVarianceRequestLine4 { get; set; }

        public CarrierResponseToVarianceRequestType CarrierResponseToVarianceRequestValue { get; set; }

        public class CarrierResponseToVarianceRequestType
        {
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[1]")]
            public bool blnGranted { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[2]")]
            public bool blnGrantedInPart { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[3]")]
            public bool blnWithoutPrejudice { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[4]")]
            public bool blnDenied { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[5]")]
            public bool blnBurdenOfProofNotMet { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[6]")]
            public bool blnSubstantiallySimilar { get; set; }
        }

        //[DisplayName("")]
        //public string MedicalProfessionalReviewedDenialName { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[0]")]
        public bool IsDecisionMadeByArbitrator { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[1]")]
        public bool IsAtWCBHearing { get; set; }

        public CarrierUndertaken MG2_CarrierUndertaken { get; set; }

        public class CarrierUndertaken
        {
            [DisplayName("topmostSubform[0].Page2[0].TextField2[4]")]
            public string CarrierUndertakenName_VarianceRequest { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].TextField2[5]")]
            public string CarrierUndertakenTitle_VarianceRequest { get; set; }
            
            [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[1]")]
            public string CarrierUndertakenSignedDate_VarianceRequest { get; set; }

        }
    }

    public class DenialInfromallyResolvedInProviderAndCarrier
    {
        [DisplayName("topmostSubform[0].Page2[0].TextField2[7]")]
        public string CarrierUndertakenName_DenialInfromally { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].TextField2[8]")]
        public string CarrierUndertakenTitle_DenialInfromally { get; set; }
        
        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[2]")]
        public string DenialInfromallyResolvedDate_DenialInfromally { get; set; }
    }

    public class RequestForReviewOfDenial
    {
        [DisplayName("topmostSubform[0].Page2[0].Check_Box68[0]")]
        public bool isRequestForReviewOfDenial { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[2]")]
        public bool isMedicalArbitratorOfChairDecisionAccepted { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[3]")]
        public bool isWCBHearingDecisionAccepted { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[3]")]
        public string RequestForReviewOfDenialSignedDate { get; set; }

    }
    
}

