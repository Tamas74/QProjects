using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace gloBilling
{
    public class MG2_1
    {
        public ClsMG21_CaseDetails MG2_1_CaseDetails { get; set; }
        public ClsMG21_PatientDetails MG2_1_PatientDetails { get; set; }
        public ClsMG21_AttendingDoctorInfo MG2_1_AttendingDoctorInformation { get; set; }
        public ClsMG21_ApprovalRequestVariance MG2_1_ApprovalRequestVariance { get; set; }
        public ClsMG21_HealthProviderCertification MG2_1_HealthProviderCertification { get; set; }
        public ClsMG21_HeaderInfo MG2_1_HeaderInfo { get; set; }
        public ClsMG21_IndependentMedicalExaminer MG2_1_IndependentMedicalExaminer { get; set; }
        public ClsMG21_CarrierResponseToVarianceRequest MG2_1_CarrierResponseToVarianceRequest { get; set; }
        public ClsMG21_DenialInformallyResolvedInProviderAndCarrier MG2_1_DenialInformallyResolvedInProviderAndCarrier { get; set; }
        public ClsMG21_RequestForReviewOfDenial MG2_1_RequestForReviewOfDenial { get; set; }


        public MG2_1()
        {
            MG2_1_CaseDetails = new ClsMG21_CaseDetails();
            MG2_1_PatientDetails = new ClsMG21_PatientDetails();
            MG2_1_AttendingDoctorInformation = new ClsMG21_AttendingDoctorInfo();
            MG2_1_ApprovalRequestVariance = new ClsMG21_ApprovalRequestVariance();
            MG2_1_HeaderInfo = new ClsMG21_HeaderInfo();
            MG2_1_HealthProviderCertification = new ClsMG21_HealthProviderCertification();
            MG2_1_IndependentMedicalExaminer = new ClsMG21_IndependentMedicalExaminer();
            MG2_1_CarrierResponseToVarianceRequest = new ClsMG21_CarrierResponseToVarianceRequest();
            MG2_1_DenialInformallyResolvedInProviderAndCarrier = new ClsMG21_DenialInformallyResolvedInProviderAndCarrier();
            MG2_1_RequestForReviewOfDenial = new ClsMG21_RequestForReviewOfDenial();
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

    public class ClsMG21_CaseDetails
    {
        [DisplayName("topmostSubform[0].Page1[0].TextField3[1]")]
        public string WCBCaseNumber_CaseDetails { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField3[2]")]
        public string CarrierCaseNumber_CaseDetails { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField3[3]")]
        public string DateOfInjury_CaseDetails { get; set; }
    }

    public class ClsMG21_PatientDetails
    {
        [DisplayName("topmostSubform[0].Page1[0].TextField3[0]")]
        public string PatientsFullName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField3[6]")]
        public string PatientSSN { get; set; }
    }

    public class ClsMG21_AttendingDoctorInfo
    {
        [DisplayName("topmostSubform[0].Page1[0].TextField3[4]")]
        public string DoctorsName { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField3[5]")]
        public string DoctorsWCBAuthorizationNumber { get; set; }
    }

    public class ClsMG21_ApprovalRequestVariance
    {
        //Guideline Reference 2
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.0[0]")]
        public string WCBTreatmentBodyPartIndicator_2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.1[0]")]
        public string WCBTreatmentGuidelineSection1_2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_10[0]")]
        public string WCBTreatmentGuidelineSection2_2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.2[0]")]
        public string WCBTreatmentGuidelineSection3_2 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.3[0]")]
        public string WCBTreatmentGuidelineSection4_2 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField2[0]")]
        public string DOSofSupportingWCBCase_2 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField4[0]")]
        public string DOSPreviouslyDenied_2 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[0].Approval_Requested_for__one_request_type_per_form_1[0]")]
        public string ApprovalRequestedFor_2 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[0].TextField2[0]")]
        public string MedicalNecessityLine1_2 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[0].Approval_Requested_for__one_request_type_per_form_1[1]")]
        public string MedicalNecessityLine2_2 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[0].Approval_Requested_for__one_request_type_per_form_1[2]")]
        public string MedicalNecessityLine3_2 { get; set; }


        //Guideline Reference 3
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.0[1]")]
        public string WCBTreatmentBodyPartIndicator_3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.1[1]")]
        public string WCBTreatmentGuidelineSection1_3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_10[1]")]
        public string WCBTreatmentGuidelineSection2_3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.2[1]")]
        public string WCBTreatmentGuidelineSection3_3 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.3[1]")]
        public string WCBTreatmentGuidelineSection4_3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField2[1]")]
        public string DOSofSupportingWCBCase_3 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField4[1]")]
        public string DOSPreviouslyDenied_3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[1].Approval_Requested_for__one_request_type_per_form_1[3]")]
        public string ApprovalRequestedFor_3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[1].TextField2[1]")]
        public string MedicalNecessityLine1_3 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[1].Approval_Requested_for__one_request_type_per_form_1[4]")]
        public string MedicalNecessityLine2_3 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[1].Approval_Requested_for__one_request_type_per_form_1[5]")]
        public string MedicalNecessityLine3_3 { get; set; }


        //Guideline Reference 4
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.0[2]")]
        public string WCBTreatmentBodyPartIndicator_4 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.1[2]")]
        public string WCBTreatmentGuidelineSection1_4 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_10[2]")]
        public string WCBTreatmentGuidelineSection2_4 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.2[2]")]
        public string WCBTreatmentGuidelineSection3_4 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.3[2]")]
        public string WCBTreatmentGuidelineSection4_4 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField2[2]")]
        public string DOSofSupportingWCBCase_4 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField4[2]")]
        public string DOSPreviouslyDenied_4 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[2].Approval_Requested_for__one_request_type_per_form_1[6]")]
        public string ApprovalRequestedFor_4 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[2].TextField2[2]")]
        public string MedicalNecessityLine1_4 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[2].Approval_Requested_for__one_request_type_per_form_1[7]")]
        public string MedicalNecessityLine2_4 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[2].Approval_Requested_for__one_request_type_per_form_1[8]")]
        public string MedicalNecessityLine3_4 { get; set; }



        //Guideline Reference 5
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.0[3]")]
        public string WCBTreatmentBodyPartIndicator_5 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.1[3]")]
        public string WCBTreatmentGuidelineSection1_5 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].undefined_10[3]")]
        public string WCBTreatmentGuidelineSection2_5 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.2[3]")]
        public string WCBTreatmentGuidelineSection3_5 { get; set; }
        [DisplayName(@"topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\.3[3]")]
        public string WCBTreatmentGuidelineSection4_5 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].DateTimeField2[3]")]
        public string DOSofSupportingWCBCase_5 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].TextField4[3]")]
        public string DOSPreviouslyDenied_5 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[3].Approval_Requested_for__one_request_type_per_form_1[9]")]
        public string ApprovalRequestedFor_5 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].#area[3].TextField2[3]")]
        public string MedicalNecessityLine1_5 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[3].Approval_Requested_for__one_request_type_per_form_1[10]")]
        public string MedicalNecessityLine2_5 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].#area[3].Approval_Requested_for__one_request_type_per_form_1[11]")]
        public string MedicalNecessityLine3_5 { get; set; }


    }


    public class ClsMG21_HeaderInfo
    {
        [DisplayName("topmostSubform[0].Page2[0].TextField1[0]")]
        public string PatientsName_MG2_1HeaderInfo { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].TextField1[1]")]
        public string WCBCaseNumber_MG2_1HeaderInfo { get; set; }   
        [DisplayName("topmostSubform[0].Page2[0].TextField1[2]")]
        public string DateOfInjury_MG2_1HeaderInfo { get; set; }    
    }


    public class ClsMG21_HealthProviderCertification
    {
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[0]")]
        public bool blnCarrierContactedOnTelephone { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[1]")]
        public bool blnCarrierNotContactedOnTelephone { get; set; } 

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[0]")]
        public string CarrierContactedonDate { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[0]")]
        public string CarrierContactedonPersonName { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[2]")]
        public bool isCarrierContactedByCopyFaxOrEmail { get; set; }   
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[1]")]
        public string CarrierContactedByFaxOrEmail { get; set; }    


        public Int64 ProviderID { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].ProviderSignature[0]")]
        public byte[] ProviderSignImage { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[1]")]
        public string ProviderSignedDate { get; set; }  
    }


    public class ClsMG21_IndependentMedicalExaminer
    {
        [DisplayName("topmostSubform[0].Page2[0].Check_Box91[0]")]
        public bool blnCarrierHerebyGivesNotice { get; set; }   

        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[3]")]
        public bool blnRequestWithRespectToNo2 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[4]")]
        public bool blnRequestWithRespectToNo3 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[5]")]
        public bool blnRequestWithRespectToNo4 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[6]")]
        public bool blnRequestWithRespectToNo5 { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].TextField2[0]")]
        public string IndependentMedicalExaminerName { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].TextField2[1]")]
        public string IndependentMedicalExaminerTitle { get; set; } 
        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[2]")]
        public string IndependentMedicalExamDate { get; set; }  

    }


    public class ClsMG21_CarrierResponseToVarianceRequest
    {

        public ClsMG21_CarrierResponseToVarianceRequestTypes MG2_1_CarrierResponseToVarianceRequestTypes { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[2]")]
        public string CarrierResponseToVarianceRequestLine1 { get; set; }   
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[3]")]
        public string CarrierResponseToVarianceRequestLine2 { get; set; }   
        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[4]")]
        public string CarrierResponseToVarianceRequestLine3 { get; set; }   

        [DisplayName("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[5]")]
        public string MedicalProfessionalReviewedDenialName { get; set; }   

        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[0]")]
        public bool IsDecisionMadeByArbitrator { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[1]")]
        public bool IsAtWCBHearing { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].TextField2[3]")]
        public string VarianceRequestName { get; set; }     
        [DisplayName("topmostSubform[0].Page2[0].TextField2[4]")]
        public string VarianceRequestTitle { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[3]")]
        public string VarianceRequestSignedDate { get; set; }

        public ClsMG21_CarrierResponseToVarianceRequest()
        {
            MG2_1_CarrierResponseToVarianceRequestTypes = new ClsMG21_CarrierResponseToVarianceRequestTypes();
        }

        public class ClsMG21_CarrierResponseToVarianceRequestTypes
        {
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[7]")]
            public bool blnGranted_2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[8]")]
            public bool blnGrantedInPart_2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[9]")]
            public bool blnWithoutPrejudice_2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[10]")]
            public bool blnDenied_2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[11]")]
            public bool blnBurdenOfProofNotMet_2 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[12]")]
            public bool blnSubstantiallySimilarReqPending_2 { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[13]")]
            public bool blnGranted_3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[14]")]
            public bool blnGrantedInPart_3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[15]")]
            public bool blnWithoutPrejudice_3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[16]")]
            public bool blnDenied_3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[17]")]
            public bool blnBurdenOfProofNotMet_3 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[18]")]
            public bool blnSubstantiallySimilarReqPending_3 { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[19]")]
            public bool blnGranted_4 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[20]")]
            public bool blnGrantedInPart_4 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[21]")]
            public bool blnWithoutPrejudice_4 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[22]")]
            public bool blnDenied_4 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[23]")]
            public bool blnBurdenOfProofNotMet_4 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[24]")]
            public bool blnSubstantiallySimilarReqPending_4 { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[25]")]
            public bool blnGranted_5 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[26]")]
            public bool blnGrantedInPart_5 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[27]")]
            public bool blnWithoutPrejudice_5 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[28]")]
            public bool blnDenied_5 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[29]")]
            public bool blnBurdenOfProofNotMet_5 { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].CheckBox1[30]")]
            public bool blnSubstantiallySimilarReqPending_5 { get; set; }


        }

    }


    public class ClsMG21_DenialInformallyResolvedInProviderAndCarrier
    {
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[31]")]
        public bool DenialInformallyResolvedReqNo2 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[32]")]
        public bool DenialInformallyResolvedReqNo3 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[33]")]
        public bool DenialInformallyResolvedReqNo4 { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[34]")]
        public bool DenialInformallyResolvedReqNo5 { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].TextField2[6]")]
        public string DenialInformallyName { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].TextField2[7]")]
        public string DenialInformallyTitle { get; set; } 

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[4]")]
        public string DenialInformallyResolvedDate { get; set; }   
    }


    public class ClsMG21_RequestForReviewOfDenial
    {
        [DisplayName("topmostSubform[0].Page2[0].Check_Box91[1]")]
        public bool isRequestForReviewOfDenial { get; set; }    

        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[35]")]
        public bool DenialOfDoctorReqNo2 { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[36]")]
        public bool DenialOfDoctorReqNo3 { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[37]")]
        public bool DenialOfDoctorReqNo4 { get; set; }  
        [DisplayName("topmostSubform[0].Page2[0].CheckBox1[38]")]
        public bool DenialOfDoctorReqNo5 { get; set; }  

        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[2]")]
        public bool isMedicalArbitratorOfChairDecisionAccepted { get; set; }    
        [DisplayName("topmostSubform[0].Page2[0].Check_Box65[3]")]
        public bool isWCBHearingDecisionAccepted { get; set; }  

        [DisplayName("topmostSubform[0].Page2[0].DateTimeField3[5]")]
        public string RequestForReviewOfDenialSignedDate { get; set; }  

    }


}
