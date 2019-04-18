using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace gloBilling
{
    public class C4_2ProgressReport
    {
       // public C4_2PatientInfo C4_2_PatientInfo { get; set; }

        private C4_2Header _C4_2_Header = null;
        public C4_2Header C4_2_Header
        {
            get { return _C4_2_Header; }
            set { _C4_2_Header = value; }
        }

        private C4_2PatientInfo _C4_2_PatientInfo = null;
        public C4_2PatientInfo C4_2_PatientInfo
        {
            get { return _C4_2_PatientInfo; }
            set { _C4_2_PatientInfo = value; }
        }

        private C4_2DoctorsInfo _C4_2_DoctorsInfo = null;
        public C4_2DoctorsInfo C4_2_DoctorsInfo 
        {
            get { return _C4_2_DoctorsInfo; }
            set { _C4_2_DoctorsInfo = value; }
        }

        private C4_2BillingInfo _C4_2_BillingInfo = null;
        public C4_2BillingInfo C4_2_BillingInfo
        {
            get { return _C4_2_BillingInfo; }
            set { _C4_2_BillingInfo = value; }
        }

        private C4_2ExamAndTreatment _C4_2_ExamAndTreatment = null;
        public C4_2ExamAndTreatment C4_2_ExamAndTreatment
        {
            get { return _C4_2_ExamAndTreatment; }
            set { _C4_2_ExamAndTreatment = value; }
        }

        private C4_2DoctorsOpinion _C4_2_DoctorsOpinion = null;
        public C4_2DoctorsOpinion C4_2_DoctorsOpinion
        {
            get { return _C4_2_DoctorsOpinion; }
            set { _C4_2_DoctorsOpinion = value; }
        }

        private C4_2ReturnToWork _C4_2_ReturnToWork = null;
        public C4_2ReturnToWork C4_2_ReturnToWork
        {
            get { return _C4_2_ReturnToWork; }
            set { _C4_2_ReturnToWork = value; }
        }

        private C4_2Footer _C4_2_Footer = null;
        public C4_2Footer C4_2_Footer
        {
            get { return _C4_2_Footer; }
            set { _C4_2_Footer = value; }
        }


        public void set()
        {
            C4DoctorInfo oC4_2 = new C4DoctorInfo();

            C4PatientInfo oTest = new C4PatientInfo();
            C4_2PatientInfo oTest1 = new C4_2PatientInfo();
         //   oTest1 = oTest as C4_2PatientInfo;

            Console.WriteLine(this.GetAttributeDisplayName(typeof(C4PatientInfo).GetProperty("PatientFullName")));

            Console.WriteLine(this.GetAttributeDisplayName(typeof(C4_2PatientInfo).GetProperty("PatientFullName")));

        }

        public string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(
                typeof(DisplayNameAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }

        public C4_2ProgressReport()
        {
            _C4_2_Header = new C4_2Header();
            _C4_2_PatientInfo = new C4_2PatientInfo();
            _C4_2_DoctorsInfo = new C4_2DoctorsInfo();
            _C4_2_BillingInfo = new C4_2BillingInfo();
            _C4_2_ExamAndTreatment = new C4_2ExamAndTreatment();
            _C4_2_DoctorsOpinion = new C4_2DoctorsOpinion();
            _C4_2_ReturnToWork = new C4_2ReturnToWork();
            _C4_2_Footer = new C4_2Footer();
        }

    }

    public class C4_2Header
    {
        [DisplayName("topmostSubform[0].Page2[0].Patients_Name[0]")]
        public string PatientNameHeader { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Date_of_injuryonset_of_illness[0]")]
        public string DateOfInjuryHeaderMM { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].undefined_23[0]")]
        public string DateOfInjuryHeaderDD { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].undefined_24[0]")]
        public string DateOfInjuryHeaderYYYY { get; set; }
    }

    public class C4_2PatientInfo //: C4PatientInfo
    {
        //public C4PatientInfo C4_PatintInfo { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Dates_of_Examination[0]")]
        public string DateOfExam { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].WCB_Case_Number_if_known[0]")]
        public string WCBCaseNumber { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Carrier_Case_Number_if_known[0]")]
        public string CarrierCaseNumber { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._1_Name[0]")]
        public string PatientFullName { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._1[0]")]
        public string DateOfInjuryDD { get; set; }
        [DisplayName("topmostSubform[0].Page1[0]._2_Date_of_injuryillness[0]")]
        public string DateOfInjuryMM { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].undefined[0]")]
        public string DateOfInjuryYY { get; set; }


        [DisplayName("topmostSubform[0].Page1[0].Text1[0]")]
        public string PatientSSN1 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text2[0]")]
        public string PatientSSN2 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text3[0]")]
        public string PatientSSN3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._4_Address_if_changed_from_previous_report[0]")]
        public string PatientStreetAddress { get; set; }
        [DisplayName("topmostSubform[0].Page1[0]._2[0]")]
        public string PatientCity { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].State[0]")]
        public string PatientState { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Zip_Code[0]")]
        public string PatientZip { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._5_Patients_Account[0]")]
        public string PatientAccount { get; set; }

    }

    public class C4_2DoctorsInfo //: C4DoctorInfo
    {
        // public C4DoctorInfo C4_DoctorsInfo { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].ProviderSignature[0]")]
        public byte[] ProviderSignImage { get; set; }
        
        public Int64 ProviderID { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._1_Your_name[0]")]
        public string ProviderFullName { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._2_WCB_Authorization[0]")]
        public string WCBAuthNo { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._3_WCB_Rating_Code[0]")]
        public string WCBRatingCode { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._4_Federal_Tax_ID[0]")]
        public string FederalTaxID { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].SSN[0]")]
        public bool blnIsSSN { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].EIN[0]")]
        public bool blnIsEIN { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._5_Office_address[0]")]
        public string OfficeAddressNumberStreet { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].City[0]")]
        public string OfficeCity { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].State_2[0]")]
        public string OfficeState { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Zip_Code_2[0]")]
        public string OfficeZip { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._6_Billing_Group_or_Practice_Name[0]")]
        public string BillingGrp { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._7_Billing_address[0]")]
        public string BillingAddressNumberStreet { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].City_2[0]")]
        public string BillingCity { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].State_3[0]")]
        public string BillingState { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Zip_Code_3[0]")]
        public string BillingZip { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._8_Office_phone[0]")]
        public string OfficePhoneCode { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].undefined_2[0]")]
        public string OfficePhone { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._9_Billing_phone[0]")]
        public string BillingPhoneCode { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].undefined_3[0]")]
        public string BillingPhone { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._10_Treating_Providers_NPI[0]")]
        public string ProviderNPI { get; set; }



    }

    public class C4_2BillingInfo //: C4BillingInfo
    {
        // public C4BillingInfo C4_BillingInfo { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._1_Employers_insurance_carrier[0]")]
        public string InsuranceCarrier { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._2_Carrier_Code___W[0]")]
        public string CarrierCode { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._3_Insurance_carriers_address[0]")]
        public string CarrierAddressNumberStreet { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].City_3[0]")]
        public string CarrierCity { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].State_4[0]")]
        public string CarrierState { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Zip_Code_4[0]")]
        public string CarrierZip { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._1_2[0]")]
        public string ICD9Code1 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].ICD9_Descriptor_1[0]")]
        public string ICD9Description1 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._2_2[0]")]
        public string ICD9Code2 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].ICD9_Descriptor_2[0]")]
        public string ICD9Description2 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._3[0]")]
        public string ICD9Code3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].ICD9_Descriptor_3[0]")]
        public string ICD9Description3 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0]._4[0]")]
        public string ICD9Code4 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].ICD9_Descriptor_4[0]")]
        public string ICD9Description4 { get; set; }

        // Date Of Service From - TO -- 1
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.0[0]")]
        public string DateOfServiceFromMM1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.0[0]")]
        public string DateOfServiceFromDD1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.0[0]")]
        public string DateOfServiceFromYY1 { get; set; }


        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.0[0]")]
        public string DateOfServiceToMM1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.0[0]")]
        public string DateOfServiceToDD1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.0[0]")]
        public string DateOfServiceToYY1 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO -- 2
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.1[0]")]
        public string DateOfServiceFromMM2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.1[0]")]
        public string DateOfServiceFromYY2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.1[0]")]
        public string DateOfServiceFromDD2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.1[0]")]
        public string DateOfServiceToMM2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.1[0]")]
        public string DateOfServiceToYY2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.1[0]")]
        public string DateOfServiceToDD2 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO -- 3
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.2[0]")]
        public string DateOfServiceFromMM3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.2[0]")]
        public string DateOfServiceFromYY3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.2[0]")]
        public string DateOfServiceFromDD3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.2[0]")]
        public string DateOfServiceToMM3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.2[0]")]
        public string DateOfServiceToYY3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.2[0]")]
        public string DateOfServiceToDD3 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 4
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.3[0]")]
        public string DateOfServiceFromMM4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.3[0]")]
        public string DateOfServiceFromYY4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.3[0]")]
        public string DateOfServiceFromDD4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.3[0]")]
        public string DateOfServiceToMM4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.3[0]")]
        public string DateOfServiceToYY4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.3[0]")]
        public string DateOfServiceToDD4 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 5
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.4[0]")]
        public string DateOfServiceFromMM5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.4[0]")]
        public string DateOfServiceFromYY5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.4[0]")]
        public string DateOfServiceFromDD5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.4[0]")]
        public string DateOfServiceToMM5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.4[0]")]
        public string DateOfServiceToYY5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.4[0]")]
        public string DateOfServiceToDD5 { get; set; }

        //End Date Of Service From - TO

        // Date Of Service From - TO  -- 
        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.0\.5[0]")]
        public string DateOfServiceFromMM6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.0\.5[0]")]
        public string DateOfServiceFromYY6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.1\.5[0]")]
        public string DateOfServiceFromDD6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.0\.1\.5[0]")]
        public string DateOfServiceToMM6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.2\.5[0]")]
        public string DateOfServiceToYY6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.1\.5[0]")]
        public string DateOfServiceToDD6 { get; set; }

        //End Date Of Service From - TO

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.0[0]")]
        public string PlaceOfService1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.1[0]")]
        public string PlaceOfService2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.2[0]")]
        public string PlaceOfService3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.3[0]")]
        public string PlaceOfService4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.4[0]")]
        public string PlaceOfService5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.2\.3\.5[0]")]
        public string PlaceOfService6 { get; set; }


        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.0[0]")]
        public string CPT_HCPCS1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.1[0]")]
        public string CPT_HCPCS2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.2[0]")]
        public string CPT_HCPCS3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.3[0]")]
        public string CPT_HCPCS4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.4[0]")]
        public string CPT_HCPCS5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.3\.5[0]")]
        public string CPT_HCPCS6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.0[0]")]
        public string Modifier1 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-1[0]")]
        //public string ModifierB1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.1[0]")]
        public string Modifier2 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-2[0]")]
        //public string ModifierB2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.2[0]")]
        public string ModifierA3 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-3[0]")]
        //public string ModifierB3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.3[0]")]
        public string ModifierA4 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-4[0]")]
        //public string ModifierB4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.4[0]")]
        public string ModifierA5 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-5[0]")]
        //public string ModifierB5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.4\.5[0]")]
        public string ModifierA6 { get; set; }

        //[DisplayName("form1[0].P2[0].Procedure-Modifier-B-6[0]")]
        //public string ModifierB6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.0[0]")]
        public string DiagnosisCode1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.1[0]")]
        public string DiagnosisCode2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.2[0]")]
        public string DiagnosisCode3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.3[0]")]
        public string DiagnosisCode4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.4[0]")]
        public string DiagnosisCode5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.5\.5[0]")]
        public string DiagnosisCode6 { get; set; }


        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.0[0]")]
        public string ChargesAmt1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.1[0]")]
        public string ChargesAmt2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.2[0]")]
        public string ChargesAmt3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.3[0]")]
        public string ChargesAmt4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.4[0]")]
        public string ChargesAmt5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.6\.5[0]")]
        public string ChargesAmt6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.0[0]")]
        public string Days_Units1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.1[0]")]
        public string Days_Units2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.2[0]")]
        public string Days_Units3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.3[0]")]
        public string Days_Units4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.4[0]")]
        public string Days_Units5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.7\.5[0]")]
        public string Days_Units6 { get; set; }


        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.0[0]")]
        public string COB1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.1[0]")]
        public string COB2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.2[0]")]
        public string COB3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.3[0]")]
        public string COB4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.4[0]")]
        public string COB5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.8\.5[0]")]
        public string COB6 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.0[0]")]
        public string ZipCode1 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.1[0]")]
        public string ZipCode2 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.2[0]")]
        public string ZipCode3 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.3[0]")]
        public string ZipCode4 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.4[0]")]
        public string ZipCode5 { get; set; }

        [DisplayName(@"topmostSubform[0].Page1[0].Text15\.0\.9\.5[0]")]
        public string ZipCode6 { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Text16[0]")]
        public string TotalCharges { get; set; }

        //[DisplayName("")]
        //public decimal AmountPaid { get; set; }

        //[DisplayName("")]
        //public decimal BalanceDue { get; set; }

        [DisplayName("topmostSubform[0].Page1[0].Check_here_if_services_were_provided_by_a_WCB_preferred_provider_organization_PPO[0]")]
        public bool blnPPO { get; set; }
    }

    public class C4_2ExamAndTreatment
    {
        [DisplayName("topmostSubform[0].Page1[0]._1_Describe_any_diagnostic_tests_rendered_at_this_visit[0]")]
        public string DiagnosticTestRendered0 { get; set; }
        [DisplayName("topmostSubform[0].Page1[0].Text4[0]")]
        public string DiagnosticTestRendered1 { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].or_your_objective_findings_1[0]")]
        public string ChangesRevealed0 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].or_your_objective_findings_2[0]")]
        public string ChangesRevealed1 { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._3_List_additional_body_parts_affected_by_this_injury_if_any[0]")]
        public string AdditionalBodyPartsAffected { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._5_Based_on_this_examination_does_the_patient_need_diagnostic_tests_or_referrals[0]")]
        public string ChangesToOriginalTreatment0 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].If_yes_check_all_that_apply[0]")]
        public string ChangesToOriginalTreatment1 { get; set; }


        [DisplayName("topmostSubform[0].Page2[0].Check_Box13[0]")]
        public bool blnIsTestOrReferralNeededYes { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Check_Box14[0]")]
        public bool blnIsTestOrReferralNeededNo { get; set; }

        public C42Examination_Tests C4_2Exam_Tests { get; set; }
        public C42Examination_Referrals C4_2Exam_Referrals { get; set; }
        public C42Examination_FollowUpVisit C4_2FollowUpVisit { get; set; }

        public C4_2ExamAndTreatment()
        {
            C4_2Exam_Tests = new C42Examination_Tests();
            C4_2Exam_Referrals = new C42Examination_Referrals();
            C4_2FollowUpVisit = new C42Examination_FollowUpVisit();
        }

        public class C42Examination_Tests
        {
            [DisplayName("topmostSubform[0].Page2[0].CT_Scan[0]")]
            public bool blnCTScan { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].EMGNCS[0]")]
            public bool blnEMGNCS { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].MRI[0]")]
            public bool blnMRI { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].specify[0]")]
            public string MRIDetails { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Labs[0]")]
            public bool blnLabs { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].specify_2[0]")]
            public string LabsDetails { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Xrays[0]")]
            public bool blnXRAYs { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].specify_3[0]")]
            public string XRAYsDetails { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Other[0]")]
            public bool blnOtherExamination_Tests { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].specify_5[0]")]
            public string OtherDetailsExamination_Tests { get; set; }
        }

        public class C42Examination_Referrals
        {
            [DisplayName("topmostSubform[0].Page2[0].Chiropractor[0]")]
            public bool blnChiropractor { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].InternistFamily_Physician[0]")]
            public bool blnInternistFamilyPhysician { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Occupational_Therapist[0]")]
            public bool blnOccupationalTherapist { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Physical_Therapist[0]")]
            public bool blnPhysicalTherapist { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Specialist_in[0]")]
            public bool blnSpecialistIn { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].specify_4[0]")]
            public string SpecialistInDetails { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Other_2[0]")]
            public bool blnOtherExamination_Referrals { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Text5[0]")]
            public string OtherDetailsExamination_Referrals { get; set; }
        }

        public class C42Examination_FollowUpVisit
        {
            [DisplayName("topmostSubform[0].Page2[0].Within_a_week[0]")]
            public bool blnWithInWeek { get; set; }

            [DisplayName("topmostSubform[0].Page2[0]._12_wks[0]")]
            public bool bln1_2Week { get; set; }

            [DisplayName("topmostSubform[0].Page2[0]._34_wks[0]")]
            public bool bln3_4Week { get; set; }

            [DisplayName("topmostSubform[0].Page2[0]._56_wks[0]")]
            public bool bln5_6Week { get; set; }

            [DisplayName("topmostSubform[0].Page2[0]._78_wks[0]")]
            public bool bln7_8Week { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].undefined_25[0]")]
            public bool blnMonths { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].months[0]")]
            public string months { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].as_needed[0]")]
            public bool ReturnAsNeeded { get; set; }
        }

        [DisplayName("topmostSubform[0].Page2[0]._6_Describe_treatment_rendered_today[0]")]
        public string TreatmentRendered0 { get; set; }
        [DisplayName("topmostSubform[0].Page2[0]._7_When_is_patients_next_followup_visit[0]")]
        public string TreatmentRendered1 { get; set; }


    }

    public class C4_2DoctorsOpinion //: C4DoctorsOpinion
    {
        // public C4DoctorsOpinion C4_2_DoctorsOpinion { get; set; }
        [DisplayName("topmostSubform[0].Page2[0]._1_In_your_opinion_was_the_incident_that_the_patient_described_the_competent_medical_cause_of_this_injuryillness[0]")]
        public string blnIsMedicalCauseOfInjuryYes { get; set; }

        //[DisplayName("topmostSubform[0].Page2[0]._1_In_your_opinion_was_the_incident_that_the_patient_described_the_competent_medical_cause_of_this_injuryillness[0]")]
        //public string blnIsMedicalCauseOfInjuryNo { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._2_Are_the_patients_complaints_consistent_with_hisher_history_of_the_injuryillness[0]")]
        public string blnIsConsistentComplaintYes { get; set; }

        //[DisplayName("topmostSubform[0].Page2[0]._2_Are_the_patients_complaints_consistent_with_hisher_history_of_the_injuryillness[0]")]
        //public string blnIsConsistentComplaintNo { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._3_Is_the_patients_history_of_the_injuryillness_consistent_with_your_objective_findings[0]")]
        public string blnInjuryConsistentWithObjectiveFindingsYes { get; set; }

        //[DisplayName("topmostSubform[0].Page2[0]._3_Is_the_patients_history_of_the_injuryillness_consistent_with_your_objective_findings[0]")]
        //public string blnInjuryConsistentWithObjectiveFindingsNo { get; set; }

        //[DisplayName("topmostSubform[0].Page2[0]._3_Is_the_patients_history_of_the_injuryillness_consistent_with_your_objective_findings[0]")]
        //public string blnInjuryConsistentWithObjectiveFindingsNA { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._4_What_is_the_percentage_0100_of_temporary_impairment[0]")]
        public string PercentageOfTemporaryImpairment { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._5_Describe_findings_and_relevant_diagnostic_test_results[0]")]
        public string FindingsAndDiagnosticResults0 { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].F_Return_to_Work[0]")]
        public string FindingsAndDiagnosticResults1 { get; set; }
    }

    public class C4_2ReturnToWork
    {
        // public C4WorkStatus C4_2_ReturnToWork { get; set; }
        [DisplayName("topmostSubform[0].Page2[0]._1_Is_patient_working_now[0]")]
        public string blnIsPateintWorkingYes { get; set; }
        //[DisplayName("topmostSubform[0].Page2[0]._1_Is_patient_working_now[0]")]
        //public string blnIsPateintWorkingNo { get; set; }

        //[DisplayName("form1[0].P4[0].Patient-Working-Yes[0]")]
        //public bool blnHasMissedWorkYes { get; set; }
        //[DisplayName("form1[0].P4[0].Patient-Working-No[0]")]
        //public bool blnHasMissedWorkNo { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._1B_Is_patient_working_now[0]")]
        public string blnIsWorkRestrictionYes { get; set; }
        //[DisplayName("topmostSubform[0].Page2[0]._1B_Is_patient_working_now[0]")]
        //public string blnIsWorkRestrictionNo { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].How_long_will_the_work_restrictions_apply[0]")]
        public string WorkRestrictionDetails { get; set; }

        public C42_HowLongWorkRestrictionsApply C4_2WorkRestrictionsApply { get; set; }

        public class C42_HowLongWorkRestrictionsApply
        {
            [DisplayName("topmostSubform[0].Page2[0]._12_days[0]")]
            public bool blnRestrictionsApplyFor1_2Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._37_days[0]")]
            public bool blnRestrictionsApplyFor3_7Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._814_days[0]")]
            public bool blnRestrictionsApplyFor8_14Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._15_days[0]")]
            public bool blnRestrictionsApplyFor15PlusDays { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Unknown_at_this_time[0]")]
            public bool blnRestrictionsApplyForUnknownTime { get; set; }
        }


        [DisplayName("topmostSubform[0].Page2[0].The_patient_cannot_return_to_work_because_explain[0]")]
        public bool blnCantReturnToWork { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Text6[0]")]
        public string CantReturnToWorkReason { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].The_patient_can_return_to_work_without_limitations_on[0]")]
        public bool blnCanReturnToWorkWithOutLimitations { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].undefined_26[0]")]
        public string ReturnDateDD { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].undefined_27[0]")]
        public string ReturnDateMM { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].undefined_28[0]")]
        public string ReturnDateYY { get; set; }

        public C42WorkLimitations C4_2WorkLimitations { get; set; }

        public class C42WorkLimitations
        {
            [DisplayName("topmostSubform[0].Page2[0].The_patient_can_return_to_work_with_the_following_limitations_check_all_that_apply_on[0]")]
            public bool blnReturnToWorkWithLimitations { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].undefined_29[0]")]
            public string WithLimitationsMM { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].undefined_30[0]")]
            public string WithLimitationsDD { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].undefined_31[0]")]
            public string WithLimitationsYY { get; set; }

            [DisplayName("topmostSubform[0].Page2[0].Bendingtwisting[0]")]
            public bool blnBendingTwisting { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Climbing_stairsladders[0]")]
            public bool blnClimbingStairsLadders { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Environmental_conditions[0]")]
            public bool blnEnvironmentalConditions { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Kneeling[0]")]
            public bool blnKneeling { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Lifting[0]")]
            public bool blnLifting { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Operating_heavy_equipment[0]")]
            public bool blnOperatingHeavyEquipment { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Operation_of_motor_vehicles[0]")]
            public bool blnOperationOfMotorVehicles { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Personal_protective_equipment[0]")]
            public bool blnPersonalProtectiveEquipment { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Sitting[0]")]
            public bool blnSitting { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Standing[0]")]
            public bool blnStanding { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Use_of_public_transportation[0]")]
            public bool blnUseOfPublicTransportation { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Use_of_upper_extremities[0]")]
            public bool blnUseOfUpperExtremities { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Other_3[0]")]
            public bool blnOtherWorkLimitations { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].explain[0]")]
            public string OtherDetailsWorkLimitations { get; set; }


        }

        [DisplayName("topmostSubform[0].Page2[0].Describequantify_the_limitations[0]")]
        public string C4_2QuantifyLimitations { get; set; }

        public C42_HowLongWorkLimitationsApply C4_2_HowLongWorkLimitationsApply { get; set; }

        public class C42_HowLongWorkLimitationsApply
        {
            [DisplayName("topmostSubform[0].Page2[0]._12_days_2[0]")]
            public bool blnLimitationApplyFor1_2Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._37_days_2[0]")]
            public bool blnLimitationApplyFor3_7Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._814_days_2[0]")]
            public bool blnLimitationApplyFor8_14Days { get; set; }
            [DisplayName("topmostSubform[0].Page2[0]._15_days_2[0]")]
            public bool blnLimitationApplyFor15PlusDays { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].Unknown_at_this_time_2[0]")]
            public bool blnLimitationApplyForUnknownTime { get; set; }
            [DisplayName("topmostSubform[0].Page2[0].NA[0]")]
            public bool blnLimitationApplyNA { get; set; }
        }

        public C4_2ReturnToWork()
        {
            C4_2WorkRestrictionsApply = new C42_HowLongWorkRestrictionsApply();
            C4_2_HowLongWorkLimitationsApply = new C42_HowLongWorkLimitationsApply();
            C4_2WorkLimitations = new C42WorkLimitations();
        }

        [DisplayName("topmostSubform[0].Page2[0].with_patient[0]")]
        public bool blnDiscussWithPatient { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].with_patients_employer[0]")]
        public bool blnDiscussWithPatientsEmployer { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].NA_2[0]")]
        public bool blnDiscussWithPatientNA { get; set; }

        [DisplayName("topmostSubform[0].Page2[0]._4_Would_the_patient_benefit_from_vocational_rehabilitation[0]")]
        public string blnBenefitFromRehabilitation { get; set; }

    }

    public class C4_2Footer
    {

        [DisplayName("topmostSubform[0].Page2[0].I_provided_the_services_listed_above[0]")]
        public bool blnProvidedServices { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].I_actively_supervised_the_healthcare_provider_named_below_who_provided_these_services[0]")]
        public bool blnSupervisedProvider { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Providers_name[0]")]
        public string ProvidersName { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Specialty[0]")]
        public string ProviderSpeciality { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Text7[0]")]
        public string AuthorizedProviderName { get; set; }
        // [DisplayName("form1[0].P4[0].Authorized-Physician-Signature[0]")]
        public string AuthorizedProviderSignature { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Text8[0]")]
        public string AuthorizedProviderSpeciality { get; set; }

        [DisplayName("topmostSubform[0].Page2[0].Text9[0]")]
        public string C42AuthorizedDateDD { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Text10[0]")]
        public string C42AuthorizedDateMM { get; set; }
        [DisplayName("topmostSubform[0].Page2[0].Text11[0]")]
        public string C42AuthorizedDateYY { get; set; }

    }



}


