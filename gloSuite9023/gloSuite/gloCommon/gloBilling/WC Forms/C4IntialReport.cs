using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloPatient;
using System.EnterpriseServices;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace gloBilling
{
    public class test
    {
        public void set()
        {
            C4 oC4 = new C4();
            //oC4.C4PatientInfo = new C4PatientInfo();
            //C4PlanOfCare = new C4PlanOfCare();

            oC4.C4DoctorInformation.BillingCity = "X";

            oC4 = null;
        }

    }

    public class C4
    {
        private C4Header _C4Header = null;
        public C4Header C4HeaderInformation 
        {
            get { return _C4Header; }
            set { _C4Header = value; } 
        }

        private C4PatientInfo _C4PatientInformation = null;
        public C4PatientInfo C4PatientInformation
        {
            get { return _C4PatientInformation; }
            set { _C4PatientInformation = value; }
        }

        private C4EmployerInfo _C4EmployerInformation = null;
        public C4EmployerInfo C4EmployerInformation
        {
            get { return _C4EmployerInformation; }
            set { _C4EmployerInformation = value; }
        }

        private C4DoctorInfo _C4DoctorInformation = null;
        public C4DoctorInfo C4DoctorInformation
        {
            get { return _C4DoctorInformation; }
            set { _C4DoctorInformation = value; }
        }

        private C4BillingInfo _C4BillingInformation = null;
        public C4BillingInfo C4BillingInformation
        {
            get { return _C4BillingInformation; }
            set { _C4BillingInformation = value; }
        }
        //public Patient PatientInformation { get; set; }
        // public C4PatientInfo C4PatientInformation { get; set; }
        //public C4EmployerInfo C4EmployerInformation { get; set; }
        //public C4DoctorInfo C4DoctorInformation { get; set; }
        //public C4BillingInfo C4BillingInformation { get; set; }
        public C4History C4HistoryInformation { get; set; }
        public C4ExamInfo C4ExamInformation { get; set; }
        public C4DoctorsOpinion C4DoctorsOpinions { get; set; }
        public C4PlanOfCare C4PlanOfCares { get; set; }
        public C4WorkStatus C4WorkStatuses { get; set; }
        public C4Footer C4Footer { get; set; }

        public C4()
        {
            _C4Header = new C4Header();
            _C4PatientInformation = new C4PatientInfo();
            _C4EmployerInformation = new C4EmployerInfo();
            _C4DoctorInformation = new C4DoctorInfo();
            _C4BillingInformation = new C4BillingInfo();

            C4HistoryInformation = new C4History();
            C4ExamInformation = new C4ExamInfo();
            C4DoctorsOpinions = new C4DoctorsOpinion();
            C4PlanOfCares = new C4PlanOfCare();
            C4WorkStatuses = new C4WorkStatus();
            C4Footer = new C4Footer();
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

    public class C4Header
    {
        [DisplayName("form1[0].P2[0].Claimant-Name2[0]")]
        public string PatientNamePage2 { get; set; }
        [DisplayName("form1[0].P2[0].TextField2[0]")]
        public string DateOfInjuryMMPage2 { get; set; }
        [DisplayName("form1[0].P2[0].TextField2[1]")]
        public string DateOfInjuryDDPage2 { get; set; }
        [DisplayName("form1[0].P2[0].TextField2[2]")]
        public string DateOfInjuryYYYYPage2 { get; set; }

        [DisplayName("form1[0].P3[0].Claimant-Name2[0]")]
        public string PatientNamePage3 { get; set; }
        [DisplayName("form1[0].P3[0].TextField2[0]")]
        public string DateOfInjuryMMPage3 { get; set; }
        [DisplayName("form1[0].P3[0].TextField2[1]")]
        public string DateOfInjuryDDPage3 { get; set; }
        [DisplayName("form1[0].P3[0].TextField2[2]")]
        public string DateOfInjuryYYYYPage3 { get; set; }

        [DisplayName("form1[0].P4[0].Claimant-Name2[0]")]
        public string PatientNamePage4 { get; set; }
        [DisplayName("form1[0].P4[0].TextField2[0]")]
        public string DateOfInjuryMMPage4 { get; set; }
        [DisplayName("form1[0].P4[0].TextField2[1]")]
        public string DateOfInjuryDDPage4 { get; set; }
        [DisplayName("form1[0].P4[0].TextField2[2]")]
        public string DateOfInjuryYYYYPage4 { get; set; }

    }
    public class C4PatientInfo
    {
        //   public PatientDemographics C4PatientDemographics { get; set; }

        [DisplayName("form1[0].P1[0].Claimant-Name[0]")]
        //  [DisplayName("form1[0].P1[0].Claimant-Name[0]1")]
        public string PatientFullName { get; set; }
        [DisplayName("form1[0].P1[0].Employee-SSN[0]")]
        public string PatientSSN { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[0]")]
        public string PatientPhoneCode { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[1]")]
        public string PatientPhone { get; set; }
        [DisplayName("form1[0].P1[0].WCB-Case-Number[0]")]
        public string WCB_CaseNo { get; set; }
        [DisplayName("form1[0].P1[0].Carrier-Case-Number[0]")]
        public string Carrier_CaseNo { get; set; }
        [DisplayName("form1[0].P1[0].Patient-StreetAddress[0]")]
        public string PatientStreetAddress { get; set; }
        [DisplayName("form1[0].P1[0].Patient-City[0]")]
        public string PatientCity { get; set; }
        [DisplayName("form1[0].P1[0].Patient-State[0]")]
        public string PatientState { get; set; }
        [DisplayName("form1[0].P1[0].Patient-Zip[0]")]
        public string PatientZip { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[2]")]
        public string DateOfInjuryMM { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[3]")]
        public string DateOfInjuryDD { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[4]")]
        public string DateOfInjuryYY { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[5]")]
        public string DateOfBirthMM { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[6]")]
        public string DateOfBirthDD { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[7]")]
        public string DateOfBirthYY { get; set; }

        [DisplayName("form1[0].P1[0].Male[0]")]
        public bool IsMale { get; set; }
        //private bool _Male;
        //public bool IsMale
        //{
        //    get 
        //    { return _Male; 
        //    }
        //    set 
        //    {
        //        if ( value == true)
        //        { _Male = true; }
        //        else
        //        { _Male = false; }

        //    }
        //}

        [DisplayName("form1[0].P1[0].Female[0]")]
        public bool IsFemale { get; set; }

        [DisplayName("form1[0].P1[0].Occupation-Job-Title[0]")]
        public string JobOnInjuryDate { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[8]")]
        public string WorkActivitiesOnInjuryDate0 { get; set; }
        [DisplayName("form1[0].P1[0].Where-How-Injury-Occurred-Line2[0]")]
        public string WorkActivitiesOnInjuryDate1 { get; set; }

        [DisplayName("form1[0].P1[0].Carrier-Case-Number[1]")]
        public string PatientAccount { get; set; }
    }

    public class C4EmployerInfo
    {
        public PatientOccupation C4InitialReportEmployerInfo { get; set; }

        [DisplayName("form1[0].P1[0].Employer-Name[0]")]
        public string EmployerWhenInjuryOccurred { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[9]")]
        public string EmployeePhoneCode { get; set; }
        [DisplayName("form1[0].P1[0].TextField2[10]")]
        public string EmployeePhone { get; set; }

        [DisplayName("form1[0].P1[0].Employer-StreetAddress[1]")]
        public string EmployeeAddress { get; set; }

        [DisplayName("form1[0].P1[0].Employer-City[0]")]
        public string EmployeeCity { get; set; }
        [DisplayName("form1[0].P1[0].Employer-State[0]")]
        public string EmployeeState { get; set; }
        [DisplayName("form1[0].P1[0].Employer-Zip[0]")]
        public string EmployeeZip { get; set; }


    }

    public class C4DoctorInfo    //ProviderInfo
    {
        [DisplayName("form1[0].P4[0].Authorized-Physician-Signature[0]")]
        public byte[] ProviderSignImage { get; set; }

        public Int64 ProviderID { get; set; }

        [DisplayName("form1[0].P1[0].Physician-Name[0]")]
        public string ProviderFullName { get; set; }

        [DisplayName("form1[0].P1[0].WCB-Authorization-Number[0]")]
        public string WCBAuthNo { get; set; }

        [DisplayName("form1[0].P1[0].WCB-Rating-Code[0]")]
        public string WCBRatingCode { get; set; }

        [DisplayName("form1[0].P1[0].Federal-ID-Number[0]")]
        public string FederalTaxID { get; set; }

        [DisplayName("form1[0].P1[0].Federal-ID-SSN[0]")]
        public bool blnIsSSN { get; set; }

        [DisplayName("form1[0].P1[0].Federal-ID-EIN[0]")]
        public bool blnIsEIN { get; set; }

        [DisplayName("form1[0].P1[0].Physician-StreetAddress[0]")]
        public string OfficeAddressNumberStreet { get; set; }

        [DisplayName("form1[0].P1[0].Physician-City[0]")]
        public string OfficeCity { get; set; }

        [DisplayName("form1[0].P1[0].Physician-State[0]")]
        public string OfficeState { get; set; }

        [DisplayName("form1[0].P1[0].Physician-Zip[0]")]
        public string OfficeZip { get; set; }

        [DisplayName("form1[0].P1[0].Physician-StreetAddress[1]")]
        public string BillingGrp { get; set; }

        [DisplayName("form1[0].P1[0].PhysicianBilling-StreetAddress[0]")]
        public string BillingAddressNumberStreet { get; set; }

        [DisplayName("form1[0].P1[0].PhysicianBilling-City[0]")]
        public string BillingCity { get; set; }

        [DisplayName("form1[0].P1[0].PhysicianBilling-State[0]")]
        public string BillingState { get; set; }

        [DisplayName("form1[0].P1[0].PhysicianBilling-Zip[0]")]
        public string BillingZip { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[11]")]
        public string OfficePhoneCode { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[12]")]
        public string OfficePhone { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[13]")]
        public string BillingPhoneCode { get; set; }

        [DisplayName("form1[0].P1[0].TextField2[14]")]
        public string BillingPhone { get; set; }

        [DisplayName("form1[0].P1[0].NPI-Number[0]")]
        public string ProviderNPI { get; set; }

        [DisplayName("form1[0].P1[0].Physician[0]")]
        public bool ProviderTypePhysician { get; set; }

        [DisplayName("form1[0].P1[0].Podiatrist[0]")]
        public bool ProviderTypePodiatrist { get; set; }

        [DisplayName("form1[0].P1[0].Chiropractor[0]")]
        public bool ProviderTypeChiropractor { get; set; }

        //[DisplayName("")]   //form1[0].P1[0].Chiropractor[0] ,form1[0].P1[0].Podiatrist[0], form1[0].P1[0].Physician[0]
        //public string ProviderType { get; set; }

        //[DisplayName("")]
        //public byte[] ProviderSignature { get; set; }

        //[DisplayName("")]
        //public string ProviderSpeciality { get; set; }
    }

    public class C4BillingInfo
    {
        [DisplayName("form1[0].P1[0].Carrier-Name[0]")]
        public string InsuranceCarrier { get; set; }

        [DisplayName("form1[0].P1[0].NPI-Number[1]")]
        public string CarrierCode { get; set; }

        [DisplayName("form1[0].P1[0].Carrier-StreetAddress[0]")]
        public string CarrierAddressNumberStreet { get; set; }

        [DisplayName("form1[0].P1[0].Carrier-City[0]")]
        public string CarrierCity { get; set; }

        [DisplayName("form1[0].P1[0].Carrier-State[0]")]
        public string CarrierState { get; set; }

        [DisplayName("form1[0].P1[0].Carrier-Zip[0]")]
        public string CarrierZip { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Code-1[0]")]
        public string ICD9Code1 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Desc-1[0]")]
        public string ICD9Description1 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Code-2[0]")]
        public string ICD9Code2 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Desc-2[0]")]
        public string ICD9Description2 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Code-3[0]")]
        public string ICD9Code3 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Desc-3[0]")]
        public string ICD9Description3 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Code-4[0]")]
        public string ICD9Code4 { get; set; }

        [DisplayName("form1[0].P1[0].Diagnosis-Desc-4[0]")]
        public string ICD9Description4 { get; set; }

        // Date Of Service From - TO -- 1
        [DisplayName("form1[0].P2[0].Service-From-Date-1-MM[0]")]
        public string DateOfServiceFromMM1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-1-YY[0]")]
        public string DateOfServiceFromYY1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-1-DD[0]")]
        public string DateOfServiceFromDD1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-1-MM[0]")]
        public string DateOfServiceToMM1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-1-YY[0]")]
        public string DateOfServiceToYY1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-1-DD[0]")]
        public string DateOfServiceToDD1 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO -- 2
        [DisplayName("form1[0].P2[0].Service-From-Date-2-MM[0]")]
        public string DateOfServiceFromMM2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-2-YY[0]")]
        public string DateOfServiceFromYY2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-2-DD[0]")]
        public string DateOfServiceFromDD2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-2-MM[0]")]
        public string DateOfServiceToMM2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-2-YY[0]")]
        public string DateOfServiceToYY2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-2-DD[0]")]
        public string DateOfServiceToDD2 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO -- 3
        [DisplayName("form1[0].P2[0].Service-From-Date-3-MM[0]")]
        public string DateOfServiceFromMM3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-3-YY[0]")]
        public string DateOfServiceFromYY3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-3-DD[0]")]
        public string DateOfServiceFromDD3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-3-MM[0]")]
        public string DateOfServiceToMM3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-3-YY[0]")]
        public string DateOfServiceToYY3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-3-DD[0]")]
        public string DateOfServiceToDD3 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 4
        [DisplayName("form1[0].P2[0].Service-From-Date-4-MM[0]")]
        public string DateOfServiceFromMM4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-4-YY[0]")]
        public string DateOfServiceFromYY4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-4-DD[0]")]
        public string DateOfServiceFromDD4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-4-MM[0]")]
        public string DateOfServiceToMM4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-4-YY[0]")]
        public string DateOfServiceToYY4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-4-DD[0]")]
        public string DateOfServiceToDD4 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 5
        [DisplayName("form1[0].P2[0].Service-From-Date-5-MM[0]")]
        public string DateOfServiceFromMM5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-5-YY[0]")]
        public string DateOfServiceFromYY5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-5-DD[0]")]
        public string DateOfServiceFromDD5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-5-MM[0]")]
        public string DateOfServiceToMM5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-5-YY[0]")]
        public string DateOfServiceToYY5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-5-DD[0]")]
        public string DateOfServiceToDD5 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 
        [DisplayName("form1[0].P2[0].Service-From-Date-6-MM[0]")]
        public string DateOfServiceFromMM6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-6-YY[0]")]
        public string DateOfServiceFromYY6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-From-Date-6-DD[0]")]
        public string DateOfServiceFromDD6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-6-MM[0]")]
        public string DateOfServiceToMM6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-6-YY[0]")]
        public string DateOfServiceToYY6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Thru-Date-6-DD[0]")]
        public string DateOfServiceToDD6 { get; set; }

        //End Date Of Service From - TO

        [DisplayName("form1[0].P2[0].Place-of-Service-1[0]")]
        public string PlaceOfService1 { get; set; }

        [DisplayName("form1[0].P2[0].Place-of-Service-2[0]")]
        public string PlaceOfService2 { get; set; }

        [DisplayName("form1[0].P2[0].Place-of-Service-3[0]")]
        public string PlaceOfService3 { get; set; }

        [DisplayName("form1[0].P2[0].Place-of-Service-4[0]")]
        public string PlaceOfService4 { get; set; }

        [DisplayName("form1[0].P2[0].Place-of-Service-5[0]")]
        public string PlaceOfService5 { get; set; }

        [DisplayName("form1[0].P2[0].Place-of-Service-6[0]")]
        public string PlaceOfService6 { get; set; }


        [DisplayName("form1[0].P2[0].Procedure-Code-1[0]")]
        public string CPT_HCPCS1 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Code-2[0]")]
        public string CPT_HCPCS2 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Code-3[0]")]
        public string CPT_HCPCS3 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Code-4[0]")]
        public string CPT_HCPCS4 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Code-5[0]")]
        public string CPT_HCPCS5 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Code-6[0]")]
        public string CPT_HCPCS6 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-1[0]")]
        public string ModifierA1 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-1[0]")]
        public string ModifierB1 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-2[0]")]
        public string ModifierA2 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-2[0]")]
        public string ModifierB2 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-3[0]")]
        public string ModifierA3 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-3[0]")]
        public string ModifierB3 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-4[0]")]
        public string ModifierA4 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-4[0]")]
        public string ModifierB4 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-5[0]")]
        public string ModifierA5 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-5[0]")]
        public string ModifierB5 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-A-6[0]")]
        public string ModifierA6 { get; set; }

        [DisplayName("form1[0].P2[0].Procedure-Modifier-B-6[0]")]
        public string ModifierB6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-1[0]")]
        public string DiagnosisCode1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-2[0]")]
        public string DiagnosisCode2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-3[0]")]
        public string DiagnosisCode3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-4[0]")]
        public string DiagnosisCode4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-5[0]")]
        public string DiagnosisCode5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Diagnosis-Code-6[0]")]
        public string DiagnosisCode6 { get; set; }


        [DisplayName("form1[0].P2[0].Service-Charge-Amount-1[0]")]
        public string ChargesAmt1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Charge-Amount-2[0]")]
        public string ChargesAmt2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Charge-Amount-3[0]")]
        public string ChargesAmt3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Charge-Amount-4[0]")]
        public string ChargesAmt4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Charge-Amount-5[0]")]
        public string ChargesAmt5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Charge-Amount-6[0]")]
        public string ChargesAmt6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-1[0]")]
        public string Days_Units1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-2[0]")]
        public string Days_Units2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-3[0]")]
        public string Days_Units3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-4[0]")]
        public string Days_Units4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-5[0]")]
        public string Days_Units5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Days-or-Units-1Service-Days-or-Units-6[0]")]
        public string Days_Units6 { get; set; }


        [DisplayName("form1[0].P2[0].Service-COB-1[0]")]
        public string COB1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-COB-2[0]")]
        public string COB2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-COB-3[0]")]
        public string COB3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-COB-4[0]")]
        public string COB4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-COB-5[0]")]
        public string COB5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-COB-6[0]")]
        public string COB6 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-1[0]")]
        public string ZipCode1 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-2[0]")]
        public string ZipCode2 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-3[0]")]
        public string ZipCode3 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-4[0]")]
        public string ZipCode4 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-5[0]")]
        public string ZipCode5 { get; set; }

        [DisplayName("form1[0].P2[0].Service-Zip-Code-6[0]")]
        public string ZipCode6 { get; set; }

        [DisplayName("form1[0].P2[0].Total-Charge-Amount[0]")]
        public string TotalCharges { get; set; }

        [DisplayName("")]
        public decimal AmountPaid { get; set; }

        [DisplayName("")]
        public decimal BalanceDue { get; set; }

        [DisplayName("form1[0].P2[0].PPO-Provider[0]")]
        public bool blnPPO { get; set; }

    }

    public class C4History
    {
        [DisplayName("form1[0].P2[0].Where-How-Injury-Occurred[0]")]
        public string WhereHowInjuryHappen0 { get; set; }

        [DisplayName("form1[0].P2[0].Where-How-Injury-Occurred-Line2[0]")]
        public string WhereHowInjuryHappen1 { get; set; }

        [DisplayName("form1[0].P2[0].Where-How-Injury-Occurred-Line2[1]")]
        public string WhereHowInjuryHappen2 { get; set; }

        [DisplayName("form1[0].P2[0].Who-explained-injury-Patient[0]")]
        public bool HowYouLearnAbtInjury_IsPatient1 { get; set; }

        [DisplayName("form1[0].P2[0].Who-explained-injury-Patient[1]")]
        public bool HowYouLearnAbtInjury_AreMedicalRecords { get; set; }

        [DisplayName("form1[0].P2[0].Who-explained-injury-Other[0]")]
        public bool HowYouLearnAbtInjury_IsOther { get; set; }

        [DisplayName("form1[0].P2[0].Name-Who-Explained-Injury[0]")]
        public string HowYouLearnAbtInjuryComment { get; set; }

        [DisplayName("form1[0].P2[0].Treated-by-another-physician-Yes[0]")]
        public bool blnAnotherProviderTreatedYes { get; set; }

        [DisplayName("form1[0].P2[0].Treated-by-another-physician-No[0]")]
        public bool blnAnotherProviderTreatedNo { get; set; }

        [DisplayName("form1[0].P2[0].Treated-by-another-physician-Location[0]")]
        public string AnotherProviderTreatedDetails { get; set; }

        //[DisplayName("form1[0].P2[0].Treated-by-another-physician-Yes[0]")]
        //public bool blnIsPreviouslyTreatedYes { get; set; }

        //[DisplayName("form1[0].P2[0].Treated-by-another-physician-No[0]")]
        //public bool blnIsPreviouslyTreatedNo { get; set; }

        [DisplayName("form1[0].P2[0].Prev-Treat-Same-Injury-Yes[0]")]
        public bool blnIsPreviouslyTreatedForSimilarWorkYes { get; set; }

        [DisplayName("form1[0].P2[0].Prev-Treat-Same-Injury-No[0]")]
        public bool blnIsPreviouslyTreatedForSimilarWorkNo { get; set; }

        [DisplayName("form1[0].P2[0].TextField2[3]")]
        public string PreviouslyTreatedWhen { get; set; }
    }

    public class C4ExamInfo
    {
        [DisplayName("form1[0].P2[0].Where-How-Injury-Occurred-Line2[2]")]
        public string DateOfExam { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[4]")]
        public string DiagnosticTestRendered0 { get; set; }

        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[1]")]
        public string DiagnosticTestRendered1 { get; set; }


        [DisplayName("form1[0].P3[0].TextField2[5]")]
        public string TreatmentRendered0 { get; set; }
        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[2]")]
        public string TreatmentRendered1 { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[6]")]
        public string PrognosisForRecovery0 { get; set; }
        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[4]")]
        public string PrognosisForRecovery1 { get; set; }
        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[5]")]
        public string PrognosisForRecovery2 { get; set; }

        [DisplayName("form1[0].P3[0].PreExistingCondition-Yes[0]")]
        public bool blnPreExistingConditionYes { get; set; }

        [DisplayName("form1[0].P3[0].PreExistingCondition-No[0]")]
        public bool blnPreExistingConditionNo { get; set; }

        [DisplayName("form1[0].P3[0].PreExistingCondition-Desc-Line1[0]")]
        public string PreExistingConditionDetails0 { get; set; }
        [DisplayName("form1[0].P3[0].PreExistingCondition-Desc-Line2[0]")]
        public string PreExistingConditionDetails1 { get; set; }

        public C4ExamInfo_Complains C4ExamInfoComplains { get; set; }
        public C4ExamInfo_NatureOfInjury C4ExamInfoNatureOfInjury { get; set; }
        public C4ExamInfo_PhysicalExamination C4ExamInfoPhysicalExamination { get; set; }

        public C4ExamInfo()
        {
            C4ExamInfoComplains = new C4ExamInfo_Complains();
            C4ExamInfoNatureOfInjury = new C4ExamInfo_NatureOfInjury();
            C4ExamInfoPhysicalExamination = new C4ExamInfo_PhysicalExamination();
        }


        public class C4ExamInfo_Complains
        {
            [DisplayName("form1[0].P2[0].Compliant-Paresthesia[0]")]
            public bool blnNumbness { get; set; }

            [DisplayName("form1[0].P2[0].Paresthesia-BodyPart[0]")]
            public string NumbnessDetails { get; set; }

            [DisplayName("form1[0].P2[0].Compliant-Pain[0]")]
            public bool blnPain { get; set; }

            [DisplayName("form1[0].P2[0].Pain-BodyPart[0]")]
            public string PainDetails { get; set; }

            [DisplayName("form1[0].P2[0].Compliant-Stiffness[0]")]
            public bool blnStiffness { get; set; }

            [DisplayName("form1[0].P2[0].Stiffness-BodyPart[0]")]
            public string StiffnessDetails { get; set; }

            [DisplayName("form1[0].P2[0].Compliant-Swelling[0]")]
            public bool blnSwelling { get; set; }

            [DisplayName("form1[0].P2[0].Swelling-BodyPart[0]")]
            public string SwellingDetails { get; set; }

            [DisplayName("form1[0].P2[0].Compliant-Weakness[0]")]
            public bool blnWeakness { get; set; }

            [DisplayName("form1[0].P2[0].Weakness-BodyPart[0]")]
            public string WeaknessDetails { get; set; }

            [DisplayName("form1[0].P2[0].Compliant-Other[0]")]
            public bool blnComplaintsOther { get; set; }

            [DisplayName("form1[0].P2[0].ComplaintOther-Desc[0]")]
            public string OtherComplaintsDetails { get; set; }
        }

        public class C4ExamInfo_NatureOfInjury
        {


            [DisplayName("form1[0].P2[0].Injury-Type-Abrasion[0]")]
            public bool blnAbrasion { get; set; }

            [DisplayName("form1[0].P2[0].Abrasion-BodyPart[0]")]
            public string AbrasionDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Amputation[0]")]
            public bool blnAmputation { get; set; }

            [DisplayName("form1[0].P2[0].Amputation-BodyPart[0]")]
            public string AmputationDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Avulsion[0]")]
            public bool blnAvulsion { get; set; }

            [DisplayName("form1[0].P2[0].Avulsion-BodyPart[0]")]
            public string AvulsionDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Bite[0]")]
            public bool blnBite { get; set; }

            [DisplayName("form1[0].P2[0].Bite-BodyPart[0]")]
            public string BiteDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Burn[0]")]
            public bool blnBurn { get; set; }

            [DisplayName("form1[0].P2[0].Burn-BodyPart[0]")]
            public string BurnDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Contusion[0]")]
            public bool blnContusionHematoma { get; set; }

            [DisplayName("form1[0].P2[0].Contusion-BodyPart[0]")]
            public string ContusionHematomaDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-CrushInjury[0]")]
            public bool blnCrushInjury { get; set; }

            [DisplayName("form1[0].P2[0].CrushInjury-BodyPart[0]")]
            public string CrushInjuryDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Dermatitis[0]")]
            public bool blnDermatitis { get; set; }

            [DisplayName("form1[0].P2[0].Dermatitis-BodyPart[0]")]
            public string DermatitisDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Dislocation[0]")]
            public bool blnDislocation { get; set; }

            [DisplayName("form1[0].P2[0].Dislocation-BodyPart[0]")]
            public string DislocationDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Fracture[0]")]
            public bool blnFracture { get; set; }

            [DisplayName("form1[0].P2[0].Fracture-BodyPart[0]")]
            public string FractureDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-HearingLoss[0]")]
            public bool blnHearingLoss { get; set; }

            [DisplayName("form1[0].P2[0].HearingLoss-BodyPart[0]")]
            public string HearingLossDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Hernia[0]")]
            public bool blnHernia { get; set; }

            [DisplayName("form1[0].P2[0].Hernia-BodyPart[0]")]
            public string HerniaDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-InfectiousDiseas[0]")]
            public bool blnInfectiousDisease { get; set; }

            [DisplayName("form1[0].P2[0].InfectiousDisease-BodyPart[0]")]
            public string InfectiousDiseaseDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-InfectiousDiseas[1]")]
            public bool blnInhalationExposure { get; set; }

            [DisplayName("form1[0].P2[0].Fumes-BodyPart[0]")]
            public string InhalationExposureDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Laceration[0]")]
            public bool blnLaceration { get; set; }

            [DisplayName("form1[0].P2[0].Laceration-BodyPart[0]")]
            public string LacerationDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-NeedleSticK[0]")]
            public bool blnNeedleStick { get; set; }

            [DisplayName("form1[0].P2[0].NeedleSticK-BodyPart[0]")]
            public string NeedleStickDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Poisoning[0]")]
            public bool blnPoisoningToxicEffects { get; set; }

            [DisplayName("form1[0].P2[0].Poisoning-BodyPart[0]")]
            public string PoisoningToxicEffectsDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Psychological[0]")]
            public bool blnPsychological { get; set; }

            [DisplayName("form1[0].P2[0].Psychological-BodyPart[0]")]
            public string PsychologicalDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Puncture[0]")]
            public bool blnPunctureWound { get; set; }

            [DisplayName("form1[0].P2[0].Puncture-BodyPart[0]")]
            public string PunctureWoundDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-RepetitiveStrain[0]")]
            public bool blnRepetitiveStrainInjury { get; set; }

            [DisplayName("form1[0].P2[0].RepetitiveStrain-BodyPart[0]")]
            public string RepetitiveStrainInjuryDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-SpinalCordInjur[0]")]
            public bool blnSpinalCordInjury { get; set; }

            [DisplayName("form1[0].P2[0].SpinalCordInjury-BodyPart[0]")]
            public string SpinalCordInjuryDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Sprain[0]")]
            public bool blnSprainStrain { get; set; }

            [DisplayName("form1[0].P2[0].Sprain-BodyPart[0]")]
            public string SprainStrainDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Sprain[1]")]
            public bool blnTornLigamentTendonMuscle { get; set; }

            [DisplayName("form1[0].P2[0].Sprain-BodyPart[1]")]
            public string TornLigamentTendonMuscleDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Tendonitis[0]")]
            public bool blnVisionLoss { get; set; }

            [DisplayName("form1[0].P2[0].Tendonitis-BodyPart[0]")]
            public string VisionLossDetails { get; set; }

            [DisplayName("form1[0].P2[0].Injury-Type-Other[0]")]
            public bool blnOtherInjuryType { get; set; }

            [DisplayName("form1[0].P2[0].Other-BodyPart[0]")]
            public string OtherDetails0 { get; set; }

            [DisplayName("form1[0].P2[0].Other-BodyPart[1]")]
            public string OtherDetails1 { get; set; }
        }

        public class C4ExamInfo_PhysicalExamination
        {
            [DisplayName("form1[0].P3[0].ExamFindings-None[0]")]
            public bool blnNoneAtPresent { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Bruising[0]")]
            public bool blnBruising { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Bruising-Desc[0]")]
            public string BruisingDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Burns[0]")]
            public bool blnBurns { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Burns-Desc[0]")]
            public string BurnsDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Crepitation[0]")]
            public bool blnCrepitation { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Crepitation-Desc[0]")]
            public string CrepitationDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Deformity[0]")]
            public bool blnDeformity { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Deformity-Desc[0]")]
            public string DeformityDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Deformity[1]")]
            public bool blnEdema { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Deformity-Desc[1]")]
            public string EdemaDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Lump[0]")]
            public bool blnHematomaLumpSwelling { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Lump-Desc[0]")]
            public string HematomaLumpSwelling { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-JointEffusion[0]")]
            public bool blnJointEffusion { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-JointEffusion-Desc[0]")]
            public string JointEffusionDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Laceration[0]")]
            public bool blnLacerationSutures { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Laceration-Desc[0]")]
            public string LacerationSuturesDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Tenderness[0]")]
            public bool blnPainTenderness { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Tenderness-Desc[0]")]
            public string PainTendernessDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Scar[0]")]
            public bool blnScar { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Scar-Desc[0]")]
            public string ScarDetails { get; set; }


            [DisplayName("form1[0].P3[0].ExamFindings-None[1]")]
            public bool blnNeuromuscularFindings { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[0]")]
            public bool blnAbnormalRestrictedROM { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[3]")]
            public bool blnActiveROM { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[2]")]
            public string ActiveROMDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[4]")]
            public bool blnPassiveROM { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[1]")]
            public string PassiveROMDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[5]")]
            public bool blnGait { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[6]")]
            public string GaitDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[2]")]
            public bool blnPalpableMuscleSpasm { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[0]")]
            public string PalpableMuscleSpasmDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[7]")]
            public bool blnReflexes { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[4]")]
            public string ReflexesDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[1]")]
            public bool blnSensation { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[5]")]
            public string SensationDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm[6]")]
            public bool blnStrengthOrWeakness { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleSpasm-Desc[3]")]
            public string StrengthOrWeaknessDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleAtrophy[0]")]
            public bool blnWastingMuscleAtrophy { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-MuscleAtrophy-Desc[0]")]
            public string WastingMuscleAtrophyDetails { get; set; }

            [DisplayName("form1[0].P3[0].ExamFindings-Other[0]")]
            public bool blnOtherfindings { get; set; }

            [DisplayName("form1[0].P3[0].TextField2[3]")]
            public string OtherfindingsDetails { get; set; }
        }

    }

    public class C4DoctorsOpinion
    {
        [DisplayName("form1[0].P3[0].Accident-Cause-Of-Injury-Yes[0]")]
        public bool blnIsMedicalCauseOfInjuryYes { get; set; }

        [DisplayName("form1[0].P3[0].Accident-Cause-Of-Injury-No[0]")]
        public bool blnIsMedicalCauseOfInjuryNo { get; set; }

        [DisplayName("form1[0].P3[0].History_Consistent-Complaint-Yes[0]")]
        public bool blnIsConsistentComplaintYes { get; set; }

        [DisplayName("form1[0].P3[0].History_Consistent-Complaint-No[0]")]
        public bool blnIsConsistentComplaintNo { get; set; }

        [DisplayName("form1[0].P3[0].History_Consistent-Findings-Yes[0]")]
        public bool blnInjuryConsistentWithObjectiveFindingsYes { get; set; }

        [DisplayName("form1[0].P3[0].History_Consistent-Findings-No[0]")]
        public bool blnInjuryConsistentWithObjectiveFindingsNo { get; set; }

        [DisplayName("form1[0].P3[0].No-Objective-Findings[0]")]
        public bool blnInjuryConsistentWithObjectiveFindingsNA { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[7]")]
        public string PercentageOfTemporaryImpairment { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[8]")]
        public string FindingsAndDiagnosticResults0 { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[9]")]
        public string FindingsAndDiagnosticResults1 { get; set; }
    }

    public class C4PlanOfCare
    {
        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred[0]")]
        public string ProposedTreatment0 { get; set; }

        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[0]")]
        public string ProposedTreatment1 { get; set; }

        [DisplayName("form1[0].P3[0].Treatment-Plan-Proposed-Line1[0]")]
        public string ProposedTreatment2 { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[10]")]
        public string MedicationsPrescribed { get; set; }

        [DisplayName("form1[0].P3[0].TextField2[11]")]
        public string MedicationAdvised { get; set; }

        [DisplayName("form1[0].P3[0].Accident-Cause-Of-Injury-No[1]")]
        public bool blnMedicationRestrictionsNone { get; set; }

        [DisplayName("form1[0].P3[0].Accident-Cause-Of-Injury-No[2]")]
        public bool blnMedicationRestrictionsMayAffect { get; set; }

        [DisplayName("form1[0].P3[0].Where-How-Injury-Occurred-Line2[3]")]
        public string MedicationRestrictionsDetails0 { get; set; }

        [DisplayName("form1[0].P3[0].Treatment-Plan-Proposed-Line1[1]")]
        public string MedicationRestrictionsDetails1 { get; set; }

        [DisplayName("form1[0].P4[0].Addnl-Test-Required-Yes[0]")]
        public bool blnIsTestOrReferralNeededYes { get; set; }

        [DisplayName("form1[0].P4[0].Addnl-Test-Required-No[0]")]
        public bool blnIsTestOrReferralNeededNo { get; set; }

        public C4PlanOfCare_Tests C4PlanOfCareTests { get; set; }
        public C4PlanOfCare_Referrals C4PlanOfCareReferrals { get; set; }
        public C4PlanOfCare_AssistiveDevices C4PlanOfCareAssistiveDevices { get; set; }
        public C4PlanOfCare_FollowUpAppt C4PlanOfCareFollowUpAppt { get; set; }

        public C4PlanOfCare()
        {
            C4PlanOfCareTests = new C4PlanOfCare_Tests();
            C4PlanOfCareReferrals = new C4PlanOfCare_Referrals();
            C4PlanOfCareAssistiveDevices = new C4PlanOfCare_AssistiveDevices();
            C4PlanOfCareFollowUpAppt = new C4PlanOfCare_FollowUpAppt();
        }

        public class C4PlanOfCare_Tests
        {
            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[0]")]
            public bool blnCTScan { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-EMG-NCS[0]")]
            public bool blnEMGNCS { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-MRI[0]")]
            public bool blnMRI { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Other-Desc[3]")]
            public string MRIDetails { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Labs[0]")]
            public bool blnLabs { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Other-Desc[2]")]
            public string LabsDetails { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Xrays[0]")]
            public bool blnXRAYs { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Other-Desc[1]")]
            public string XRAYsDetails { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Other[0]")]
            public bool blnAddnlTestOther { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-Other-Desc[0]")]
            public string AddnlTestOtherDetails { get; set; }
        }

        public class C4PlanOfCare_Referrals
        {
            [DisplayName("form1[0].P4[0].Referral-Chiropractor[0]")]
            public bool blnChiropractor { get; set; }

            [DisplayName("form1[0].P4[0].Referral-FamilyPhysician[0]")]
            public bool blnInternistFamilyPhysician { get; set; }

            [DisplayName("form1[0].P4[0].Referral-OccTherapist[0]")]
            public bool blnOccupationalTherapist { get; set; }

            [DisplayName("form1[0].P4[0].Referral-Physiotherapist[0]")]
            public bool blnPhysicalTherapist { get; set; }

            [DisplayName("form1[0].P4[0].Referral-Specialist[0]")]
            public bool blnSpecialistIn { get; set; }

            [DisplayName("form1[0].P4[0].Referral-Specialist-Desc[0]")]
            public string SpecialistInDetails { get; set; }

            [DisplayName("form1[0].P4[0].Referral-Other[0]")]
            public bool blnReferralOther { get; set; }

            [DisplayName("form1[0].P4[0].Referral-Other-Desc[0]")]
            public string ReferralOtherDetails { get; set; }
        }

        public class C4PlanOfCare_AssistiveDevices
        {
            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[1]")]
            public bool blnCane { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[2]")]
            public bool blnCrutches { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[3]")]
            public bool blnOrthotics { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[4]")]
            public bool blnWalker { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[5]")]
            public bool blnWheelchair { get; set; }

            [DisplayName("form1[0].P4[0].Addnl-Test-CT-Scan[6]")]
            public bool blnOtherAssistiveDevices { get; set; }

            [DisplayName("form1[0].P4[0].TextField2[3]")]
            public string OtherAssistiveDevicesDetails { get; set; }
        }

        public class C4PlanOfCare_FollowUpAppt
        {
            [DisplayName("form1[0].P4[0].Follow-Up-Within-Week[0]")]
            public bool blnWithInWeek { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-1-2-Weeks[0]")]
            public bool bln1_2Week { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-3-4-Weeks[0]")]
            public bool bln3_4Week { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-5-6-Weeks[0]")]
            public bool bln5_6Week { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-7-8-Weeks[0]")]
            public bool bln7_8Week { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-Month-ckbox[0]")]
            public bool blnMonths { get; set; }

            [DisplayName("form1[0].P4[0].Follow-Up-Month-Number[0]")]
            public string months { get; set; }

            [DisplayName("form1[0].P4[0].Return-As-Needed[0]")]
            public bool ReturnAsNeeded { get; set; }
        }

    }

    public class C4WorkStatus
    {
        [DisplayName("form1[0].P4[0].Patient-Working-Yes[0]")]
        public bool blnHasMissedWorkYes { get; set; }

        [DisplayName("form1[0].P4[0].Patient-Working-No[0]")]
        public bool blnHasMissedWorkNo { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[4]")]
        public string MissedWorkDateDD { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[5]")]
        public string MissedWorkDateMM { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[6]")]
        public string MissedWorkDateYY { get; set; }

        [DisplayName("form1[0].P4[0].Patient-Working-Yes[1]")]
        public bool blnIsCurrentlyWorkingYes { get; set; }

        [DisplayName("form1[0].P4[0].Patient-Working-No[1]")]
        public bool blnIsCurrentlyWorkingNo { get; set; }

        [DisplayName("form1[0].P4[0].Patient-Working-Yes[2]")]
        public bool blnUsualWorkActivities { get; set; }

        [DisplayName("form1[0].P4[0].Patient-Working-Yes[3]")]
        public bool blnLimitedWorkActivities { get; set; }

        [DisplayName("form1[0].P4[0].No-Return-To-Work[0]")]
        public bool blnCantReturnToWork { get; set; }

        [DisplayName("form1[0].P4[0].No-Return-To-Work-Reason[0]")]
        public string CantReturnToWorkReason { get; set; }

        [DisplayName("form1[0].P4[0].No-Limitations[0]")]
        public bool blnCanReturnToWork { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[7]")]
        public string ReturnDateDD { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[8]")]
        public string ReturnDateMM { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[9]")]
        public string ReturnDateYY { get; set; }

        [DisplayName("form1[0].P4[0].Specific-Limitations-Flag[0]")]
        public bool blnCanReturnToWorkWithLimitations { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[10]")]
        public string CanReturnToWorkWithLimitationsDateDD { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[11]")]
        public string CanReturnToWorkWithLimitationsDateMM { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[12]")]
        public string CanReturnToWorkWithLimitationsDateYY { get; set; }

        public C4WorkStatus_WorkLimitations C4WorkStatusWorkLimitations { get; set; }

        public C4WorkStatus()
        {
            C4WorkStatusWorkLimitations = new C4WorkStatus_WorkLimitations();
        }

        public class C4WorkStatus_WorkLimitations
        {
            [DisplayName("form1[0].P4[0].Limitations-Bending[0]")]
            public bool blnBendingTwisting { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Climbing[0]")]
            public bool blnClimbingStairsLadders { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Environment[0]")]
            public bool blnEnvironmentalConditions { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Kneeling[0]")]
            public bool blnKneeling { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Lifting[0]")]
            public bool blnLifting { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-HeavyEquipOper[0]")]
            public bool blnOperatingHeavyEquipment { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-MotorVehicleOper[0]")]
            public bool blnOperationOfMotorVehicles { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-ProtectEquip[0]")]
            public bool blnPersonalProtectiveEquipment { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Sitting[0]")]
            public bool blnSitting { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Standing[0]")]
            public bool blnStanding { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-PublicTrans[0]")]
            public bool blnUseOfPublicTransportation { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-UpperExtremities[0]")]
            public bool blnUseOfUpperExtremities { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Other[0]")]
            public bool blnOtherLimitations { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Other-Desc[0]")]
            public string OtherLimitationsDetails { get; set; }

            [DisplayName("form1[0].P4[0].Limitations-Desc-Line1[0]")]
            public string DescribeLimitations0 { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Desc-Line1[1]")]
            public string DescribeLimitations1 { get; set; }
            [DisplayName("form1[0].P4[0].Limitations-Desc-Line1[2]")]
            public string DescribeLimitations2 { get; set; }

            [DisplayName("form1[0].P4[0].Restriction-1-2-Days[0]")]
            public bool blnLimitationApplyFor1_2Days { get; set; }
            [DisplayName("form1[0].P4[0].Restriction-3-7-Days[0]")]
            public bool blnLimitationApplyFor3_7Days { get; set; }
            [DisplayName("form1[0].P4[0].Restriction-8-14-Days[0]")]
            public bool blnLimitationApplyFor8_14Days { get; set; }
            [DisplayName("form1[0].P4[0].Restriction-Over14-Days[0]")]
            public bool blnLimitationApplyFor15PlusDays { get; set; }
            [DisplayName("form1[0].P4[0].Restriction-Unknown[0]")]
            public bool blnLimitationApplyForUnknownTime { get; set; }
            [DisplayName("form1[0].P4[0].Restriction-NA[0]")]
            public bool blnLimitationApplyNA { get; set; }
        }

        [DisplayName("form1[0].P4[0].Discussed-Return-to-Work-Employer-Yes[0]")]
        public bool blnDiscussWithPatient { get; set; }

        [DisplayName("form1[0].P4[0].Discussed-Return-to-Work-Employer-Yes[1]")]
        public bool blnDiscussWithPatientsEmployer { get; set; }

        [DisplayName("form1[0].P4[0].Discussed-Return-to-Work-Employer-NA[0]")]
        public bool blnDiscussWithPatientNA { get; set; }

    }

    public class C4Footer
    {

        [DisplayName("form1[0].P4[0].Auth-Provider_Provided-Services-Yes[0]")]
        public bool blnProvidedServices { get; set; }
        [DisplayName("form1[0].P4[0].Auth-Provider_Provided-Services-Yes[1]")]
        public bool blnSupervisedProvider { get; set; }

        [DisplayName("form1[0].P4[0].TextField2[13]")]
        public string ProvidersName { get; set; }
        [DisplayName("form1[0].P4[0].TextField2[14]")]
        public string ProviderSpeciality { get; set; }
        [DisplayName("form1[0].P4[0].Authorized-Physician-Name[0]")]
        public string AuthorizedProviderName { get; set; }
        [DisplayName("form1[0].P4[0].Authorized-Physician-Signature[0]")]
        public string AuthorizedProviderSignature { get; set; }
        [DisplayName("form1[0].P4[0].Authorized-Physician-Specialty[0]")]
        public string AuthorizedProviderSpeciality { get; set; }
        [DisplayName("form1[0].P4[0].Authorized-Physician-Specialty[1]")]
        public string C4AuthorizedDate { get; set; }

    }

}


