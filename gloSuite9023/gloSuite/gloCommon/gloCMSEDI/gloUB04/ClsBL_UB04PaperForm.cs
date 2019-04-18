using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace gloCMSEDI
{
    public class ClsBL_UB04PaperForm
    {


        #region " Private Variables "
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _DataBaseConnectionString;
        DataTable dtSettings;
        #endregion

        #region "Constructor"
     
        //public ClsBL_UB04PaperForm()
        //    {
        //        _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
        //        dtSettings = GetDefaultPrinterSetting();
        //        UB04_ServiceLines = new ServiceLines();
        //        if (dtSettings == null || dtSettings.Rows.Count == 0)
        //        {
        //            dtSettings = GetDefaultPrinterSetting();
        //        }

        //        InitializeBoxes();
        //    }

        public ClsBL_UB04PaperForm(bool IsForPrint)
        {
            _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            UB04_ServiceLines = new ServiceLines();
            dtSettings = GetDefaultPrinterSetting(IsForPrint);
            
            if (IsForPrint)
            {
                InitializeBoxes();
            }
            else
            { InitializeBoxes(); }
        }
        //public ClsBL_UB04PaperForm(string sPrinterName)
        //{
        //    _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
        //    UB04_ServiceLines = new ServiceLines();
        //    dtSettings = null;
        //    dtSettings = GetPrinterSettingNew(sPrinterName);
        //    if (dtSettings == null || dtSettings.Rows.Count == 0)
        //    {
        //        dtSettings = GetDefaultPrinterSettingNew(sPrinterName);
        //        if (dtSettings == null || dtSettings.Rows.Count == 0)
        //        {
        //            dtSettings = GetPrinterSettingNew();
        //            if (dtSettings == null || dtSettings.Rows.Count == 0)
        //            {
        //                dtSettings = GetDefaultPrinterSettingNew();
        //            }
        //        }
        //    }

        //    InitializeBoxes();
        //}
        public ClsBL_UB04PaperForm(string sPrinterName)
        {
            _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            UB04_ServiceLines = new ServiceLines();
            dtSettings = null;
            dtSettings = GetPrinterCoordinates(sPrinterName, 1); //GetPrinterSettingNew(sPrinterName);
            if (dtSettings == null || dtSettings.Rows.Count == 0)
            {
                dtSettings = GetPrinterCoordinates(sPrinterName, 0);//GetDefaultPrinterSettingNew(sPrinterName);
                if (dtSettings == null || dtSettings.Rows.Count == 0)
                {
                    dtSettings = GetPrinterCoordinates("Default", 1); //GetPrinterSettingNew();
                    if (dtSettings == null || dtSettings.Rows.Count == 0)
                    {
                        dtSettings = GetPrinterCoordinates("Default", 0); //GetDefaultPrinterSettingNew();
                    }
                }
            }

            InitializeBoxes();
        }
        #endregion

        #region "Variable Declaration"

            public FormFieldString   UB04_1_ProviderName;
            public FormFieldString   UB04_1_ProviderAddress;
            public FormFieldString   UB04_1a_ProviderCity;
            public FormFieldString   UB04_1b_ProviderState;
            public FormFieldString   UB04_1c_ProviderZipCode;
            public FormFieldString   UB04_1a_ProviderPhone;
            public FormFieldString   UB04_1b_ProviderFaxNumber;
            public FormFieldString   UB04_1c_ProviderCountryCode;
            public FormFieldString   UB04_2_PayToName;
            public FormFieldString   UB04_2_PayToAddress;
            public FormFieldString   UB04_2a_Pay_toCity;
            public FormFieldString   UB04_2b_Pay_toState;
            public FormFieldString   UB04_2a_Pay_toZip;
            public FormFieldString   UB04_2b_ReservedFL02;
            public FormFieldString   UB04_3a_PatientControlNumber;
            public FormFieldString   UB04_3b_MedicalHealthRecordNumber;
            public FormFieldString   UB04_4_TypeofBill;
            public FormFieldString   UB04_4_TypeofBillFrequencyCode;
            public FormFieldString   UB04_5_FederalTaxNumber_Upperline;
            public FormFieldString   UB04_5_FederalTaxNumber_Lowerline;
            public FormFieldString   UB04_6a_StatementCoversPeriod_From;
            public FormFieldString   UB04_6b_StatementCoversPeriod_Through;
            public FormFieldString   UB04_7a_ReservedFL07A;
            public FormFieldString   UB04_7b_ReservedFL07B;
            public FormFieldString   UB04_8_PatientIdentifier;
            public FormFieldString   UB04_8a_PatientSocialSecurityNumber;
            public FormFieldString   UB04_8b_PatientName;
            public FormFieldString   UB04_9a_PatientStreetAddress;
            public FormFieldString   UB04_9b_PatientCity;
            public FormFieldString   UB04_9c_PatientState;
            public FormFieldString   UB04_9d_PatientZip;
            public FormFieldString   UB04_9e_PatientCountryCode;
            public FormFieldString   UB04_10_PatientBirthDate;
            public FormFieldString  UB04_11_PatientGender;
            public FormFieldString   UB04_11_PatientMaritalStatus;
            public FormFieldString   UB04_11_PatientRace;
            public FormFieldString   UB04_12_Admission_Visit_StartofCareDate;
            public FormFieldString   UB04_13_Admission_Visit_Hour;
            public FormFieldString   UB04_14_AdmissionType;
            public FormFieldString   UB04_15_ReferralSource;
            public FormFieldString   UB04_16_DischargeHour;
            public FormFieldString   UB04_17_DischargeStatus;
            public FormFieldString   UB04_18_ConditionCodes;
            public FormFieldString   UB04_19_ConditionCodes;
            public FormFieldString   UB04_20_ConditionCodes;
            public FormFieldString   UB04_21_ConditionCodes;
            public FormFieldString   UB04_22_ConditionCodes;
            public FormFieldString   UB04_23_ConditionCodes;
            public FormFieldString   UB04_24_ConditionCodes;
            public FormFieldString   UB04_25_ConditionCodes;
            public FormFieldString   UB04_26_ConditionCodes;
            public FormFieldString   UB04_27_ConditionCodes;
            public FormFieldString   UB04_28_ConditionCodes;
            public FormFieldString   UB04_29_AccidentState;
            public FormFieldString   UB04_30_ReservedFL30A;
            public FormFieldString   UB04_30_ReservedFL30B;

            public FormFieldString UB04_31a_OccurrenceCode;
            public FormFieldString UB04_31a_OccurrenceDate;
            public FormFieldString   UB04_31b_OccurrenceCode;
            public FormFieldString   UB04_31b_OccurrenceDate;
            public FormFieldString   UB04_32a_OccurrenceCode;
            public FormFieldString   UB04_32a_OccurrenceDate;
            public FormFieldString   UB04_32b_OccurrenceCode;
            public FormFieldString   UB04_32b_OccurrenceDate;
            public FormFieldString   UB04_33a_OccurrenceCode;
            public FormFieldString   UB04_33a_OccurrenceDate;
            public FormFieldString   UB04_33b_OccurrenceCode;
            public FormFieldString   UB04_33b_OccurrenceDate;
            public FormFieldString   UB04_34a_OccurrenceCode;
            public FormFieldString   UB04_34a_OccurrenceDate;
            public FormFieldString   UB04_34b_OccurrenceCode;
            public FormFieldString   UB04_34b_OccurrenceDate;

            public FormFieldString   UB04_35a_OccurrenceSpanCode;
            public FormFieldString   UB04_35a_OccurrenceSpanDateFrom;
            public FormFieldString   UB04_35a_OccurrenceSpanDateThrough;
            public FormFieldString   UB04_35b_OccurrenceSpanCode;
            public FormFieldString   UB04_35b_OccurrenceSpanDateFrom;
            public FormFieldString   UB04_35b_OccurrenceSpanDateThrough;
            public FormFieldString   UB04_36a_OccurrenceSpanCode;
            public FormFieldString   UB04_36a_OccurrenceSpanDateFrom;
            public FormFieldString   UB04_36a_OccurrenceSpanDateThrough;
            public FormFieldString   UB04_36b_OccurrenceSpanCode;
            public FormFieldString   UB04_36b_OccurrenceSpanDateFrom;
            public FormFieldString   UB04_36b_OccurrenceSpanDateThrough;


            public FormFieldString   UB04_37_ReservedFL37;
            public FormFieldString   UB04_38_ResponsiblePartyNameAddress;

            public FormFieldString   UB04_39a_ValueCode;
            public FormFieldString   UB04_39a_ValueCodeAmount;
            public FormFieldString   UB04_39a_ValueCodeAmount_Cents;
            public FormFieldString   UB04_39b_ValueCode;
            public FormFieldString   UB04_39b_ValueCodeAmount;
            public FormFieldString   UB04_39b_ValueCodeAmount_Cents;
            public FormFieldString   UB04_39c_ValueCode;
            public FormFieldString   UB04_39c_ValueCodeAmount;
            public FormFieldString   UB04_39c_ValueCodeAmount_Cents;
            public FormFieldString   UB04_39d_ValueCode;
            public FormFieldString   UB04_39d_ValueCodeAmount;
            public FormFieldString   UB04_39d_ValueCodeAmount_Cents;
            public FormFieldString   UB04_40a_ValueCode;
            public FormFieldString   UB04_40a_ValueCodeAmount;
            public FormFieldString   UB04_40a_ValueCodeAmount_Cents;
            public FormFieldString   UB04_40b_ValueCode;
            public FormFieldString   UB04_40b_ValueCodeAmount;
            public FormFieldString   UB04_40b_ValueCodeAmount_Cents;
            public FormFieldString   UB04_40c_ValueCode;
            public FormFieldString   UB04_40c_ValueCodeAmount;
            public FormFieldString   UB04_40c_ValueCodeAmount_Cents;
            public FormFieldString   UB04_40d_ValueCode;
            public FormFieldString   UB04_40d_ValueCodeAmount;
            public FormFieldString   UB04_40d_ValueCodeAmount_Cents;
            public FormFieldString   UB04_41a_ValueCode;
            public FormFieldString   UB04_41a_ValueCodeAmount;
            public FormFieldString   UB04_41a_ValueCodeAmount_Cents;
            public FormFieldString   UB04_41b_ValueCode;
            public FormFieldString   UB04_41b_ValueCodeAmount;
            public FormFieldString   UB04_41b_ValueCodeAmount_Cents;
            public FormFieldString   UB04_41c_ValueCode;
            public FormFieldString   UB04_41c_ValueCodeAmount;
            public FormFieldString   UB04_41c_ValueCodeAmount_Cents;
            public FormFieldString   UB04_41d_ValueCode;
            public FormFieldString   UB04_41d_ValueCodeAmount;
            public FormFieldString   UB04_41d_ValueCodeAmount_Cents;



            public ServiceLines UB04_ServiceLines;
            public FormFieldString   UB04_42_RevenueCode;
            public FormFieldString   UB04_43_RevenueCodeDescription;
            public FormFieldString   UB04_44_RateCodes;
            public FormFieldString   UB04_45_ServiceDate_visit_;
            public FormFieldString   UB04_46_ServiceUnits;
            public FormFieldString   UB04_47a_TotalCharges_Dollars;
            public FormFieldString   UB04_47b_TotalCharges_Cents;
            public FormFieldString   UB04_48a_Non_coveredCharges_Dollars;
            public FormFieldString   UB04_48b_Non_coveredCharges_Cents;
            public FormFieldString   UB04_49_ReservedFL49;
            public FormFieldString   UB04_42L23_RevenueCode;
            public FormFieldString   UB04_47L23_SummaryTotalCharges_Dollars;
            public FormFieldString   UB04_47L23_SummaryTotalCharges_Cents;
            public FormFieldString   UB04_48L23_SummaryNon_coveredCharges_Dollars;
            public FormFieldString   UB04_48L23_SummaryNon_coveredCharges_Cents;
            public FormFieldString   UB04_49L23_Reserved49L23;
            public FormFieldString   UB04_43L23_CurrentPage;
            public FormFieldString   UB04_44L23_TotalPages;
            public FormFieldString   UB04_44L23CreationDate;
            public FormFieldString   UB04_50_PayerName_Primary;
            public FormFieldString   UB04_50_PayerName_Secondary;
            public FormFieldString   UB04_50_PayerName_Tertiary;
            public FormFieldString   UB04_51_HealthPlanIDA;
            public FormFieldString   UB04_51_HealthPlanIDB;
            public FormFieldString   UB04_51_HealthPlanIDC;
            public FormFieldString   UB04_52_InformationRelease_Primary;
            public FormFieldString   UB04_52_InformationRelease_Secondary;
            public FormFieldString   UB04_52_InformationRelease_Tertiary;
            public FormFieldString   UB04_53_BenefitsAssignment_Primary;
            public FormFieldString   UB04_53_BenefitsAssignment_Secondary;
            public FormFieldString   UB04_53_BenefitsAssignment_Tertiary;
            public FormFieldString   UB04_54_PriorPaymentsDollars_Primary;
            public FormFieldString   UB04_54_PriorPaymentsCents_Primary;
            public FormFieldString   UB04_54_PriorPaymentsDollars_Secondary;
            public FormFieldString   UB04_54_PriorPaymentsCents_Secondary;
            public FormFieldString   UB04_54_PriorPaymentsDollars_Tertiary;
            public FormFieldString   UB04_54_PriorPaymentsCents_Tertiary;
            public FormFieldString   UB04_55a_EstimatedAmountDueDollars_Primary;
            public FormFieldString   UB04_55a_EstimatedAmountDueCents_Primary;
            public FormFieldString   UB04_55b_EstimatedAmountDueDollars_Secondary;
            public FormFieldString   UB04_55b_EstimatedAmountDueCents_Secondary;
            public FormFieldString   UB04_55c_EstimatedAmountDueDollars_Tertiary;
            public FormFieldString   UB04_55c_EstimatedAmountDueCents_Tertiary;
            public FormFieldString   UB04_56_NationalProviderIdentifier_NPI_;
            public FormFieldString   UB04_57_OtherProvider_Primary;
            public FormFieldString   UB04_57_OtherProvider_Secondary;
            public FormFieldString   UB04_57_OtherProvider_Tertiary;
            public FormFieldString   UB04_58_InsuredName_Primary;
            public FormFieldString   UB04_58_InsuredName_Secondary;
            public FormFieldString   UB04_58_InsuredName_Tertiary;
            public FormFieldString   UB04_59_PatientRelationshipToInsured_Primary;
            public FormFieldString   UB04_59_PatientRelationshipToInsured_Secondary;
            public FormFieldString   UB04_59_PatientRelationshipToInsured_Tertiary;
            public FormFieldString   UB04_60_InsuredUniqueID_Primary;
            public FormFieldString   UB04_60_InsuredUniqueID_Secondary;
            public FormFieldString   UB04_60_InsuredUniqueID_Tertiary;
            public FormFieldString   UB04_61_InsuredGroupName_Primary;
            public FormFieldString   UB04_61_InsuredGroupName_Secondary;
            public FormFieldString   UB04_61_InsuredGroupName_Tertiary;
            public FormFieldString   UB04_62_InsuredGroupNumber_Primary;
            public FormFieldString   UB04_62_InsuredGroupNumber_Secondary;
            public FormFieldString   UB04_62_InsuredGroupNumber_Tertiary;
            public FormFieldString   UB04_63_TreatmentAuthorizationCode_Primary;
            public FormFieldString   UB04_63_TreatmentAuthorizationCode_Secondary;
            public FormFieldString   UB04_63_TreatmentAuthorizationCode_Tertiary;
            public FormFieldString   UB04_64_DocumentControlNumber_A;
            public FormFieldString   UB04_64_DocumentControlNumber_B;
            public FormFieldString   UB04_64_DocumentControlNumber_C;
            public FormFieldString   UB04_65_EmployerName_Primary;
            public FormFieldString   UB04_65_EmployerName_Secondary;
            public FormFieldString   UB04_65_EmployerName_Tertiary;
            public FormFieldString   UB04_66_ICDVersionIndicator;
            public FormFieldString   UB04_67_PrincipalDiagnosisCode;
            public FormFieldString   UB04_67a_OtherDiagnosis_A;
            public FormFieldString   UB04_67b_OtherDiagnosis_B;
            public FormFieldString   UB04_67c_OtherDiagnosis_C;
            public FormFieldString   UB04_67d_OtherDiagnosis_D;
            public FormFieldString   UB04_67e_OtherDiagnosis_E;
            public FormFieldString   UB04_67f_OtherDiagnosis_F;
            public FormFieldString   UB04_67g_OtherDiagnosis_G;
            public FormFieldString   UB04_67h_OtherDiagnosis_H;
            public FormFieldString   UB04_67i_OtherDiagnosis_I;
            public FormFieldString   UB04_67j_OtherDiagnosis_J;
            public FormFieldString   UB04_67k_OtherDiagnosis_K;
            public FormFieldString   UB04_67l_OtherDiagnosis_L;
            public FormFieldString   UB04_67m_OtherDiagnosis_M;
            public FormFieldString   UB04_67n_OtherDiagnosis_N;
            public FormFieldString   UB04_67o_OtherDiagnosis_O;
            public FormFieldString   UB04_67p_OtherDiagnosis_P;
            public FormFieldString   UB04_67q_OtherDiagnosis_Q;
            public FormFieldString   UB04_68_Reserved_68A;
            public FormFieldString   UB04_68_Reserved_68B;
            public FormFieldString   UB04_69_AdmittingDiagnosisCode;
            public FormFieldString   UB04_70a_PatientVisitReason_A;
            public FormFieldString   UB04_70b_PatientVisitReason_B;
            public FormFieldString   UB04_70c_PatientVisitReason_C;
            public FormFieldString   UB04_71_PPSCode;
            public FormFieldString   UB04_72a_ExternalCauseofInjuryCode_A;
            public FormFieldString   UB04_72b_ExternalCauseofInjuryCode_B;
            public FormFieldString   UB04_72c_ExternalCauseofInjuryCode_C;
            public FormFieldString   UB04_73_ReservedFL73;
            public FormFieldString   UB04_74_ProcedureCode_Principal;
            public FormFieldString   UB04_74_ProcedureDate_Principal;
            public FormFieldString   UB04_74a_ProcedureCode_OtherA;
            public FormFieldString   UB04_74a_ProcedureDate_OtherA;
            public FormFieldString   UB04_74b_ProcedureCode_OtherB;
            public FormFieldString   UB04_74b_ProcedureDate_OtherB;
            public FormFieldString   UB04_74c_ProcedureCode_OtherC;
            public FormFieldString   UB04_74c_ProcedureDate_OtherC;
            public FormFieldString   UB04_74d_ProcedureCode_OtherD;
            public FormFieldString   UB04_74d_ProcedureDate_OtherD;
            public FormFieldString   UB04_74e_ProcedureCode_OtherE;
            public FormFieldString   UB04_74e_ProcedureDate_OtherE;
            public FormFieldString   UB04_75a_ReservedFL75A;
            public FormFieldString   UB04_75b_ReservedFL75B;
            public FormFieldString   UB04_75c_ReservedFL75C;
            public FormFieldString   UB04_75d_ReservedFL75D;
            public FormFieldString   UB04_76_AttendingNPI;
            public FormFieldString   UB04_76_AttendingQUAL;
            public FormFieldString   UB04_76_AttendingID;
            public FormFieldString   UB04_76a_AttendingLast;
            public FormFieldString   UB04_76b_AttendingFirst;
            public FormFieldString   UB04_77_OperatingNPI;
            public FormFieldString   UB04_77_OperatingQUAL;
            public FormFieldString   UB04_77_OperatingID;
            public FormFieldString   UB04_77a_OperatingLast;
            public FormFieldString   UB04_77b_OperatingFirst;
            public FormFieldString   UB04_78_OtherNPI;
            public FormFieldString   UB04_78_OtherQUAL;
            public FormFieldString   UB04_78_OtherID;
            public FormFieldString   UB04_78_OtherLast;
            public FormFieldString   UB04_78_OtherFirst;
            public FormFieldString   UB04_78_OtherProvider_QUAL;
            public FormFieldString   UB04_79_OtherNPI;
            public FormFieldString   UB04_79_OtherQUAL;
            public FormFieldString   UB04_79_OtherID;
            public FormFieldString   UB04_79_OtherLast;
            public FormFieldString   UB04_79_OtherFirst;
            public FormFieldString   UB04_79_OtherProvider_QUAL;
            public FormFieldString   PayerCodeA_Primary;
            public FormFieldString   PayerCodeB_Secondary;
            public FormFieldString   PayerCodeC_Tertiary;
            public FormFieldString   UB04_80a_Remarks_1;
            public FormFieldString   UB04_80b_Remarks_2;
            public FormFieldString   UB04_80c_Remarks_3;
            public FormFieldString   UB04_80d_Remarks_4;
            public FormFieldString   UB04_81a_Code_Code_QUAL_A;
            public FormFieldString   UB04_81a_Code_Code_CODE_A;
            public FormFieldString   UB04_81a_Code_Code_VALUE_A;
            public FormFieldString   UB04_81b_Code_Code_QUAL_B;
            public FormFieldString   UB04_81b_Code_Code_CODE_B;
            public FormFieldString   UB04_81b_Code_Code_VALUE_B;
            public FormFieldString   UB04_81c_Code_Code_QUAL_C;
            public FormFieldString   UB04_81c_Code_Code_CODE_C;
            public FormFieldString   UB04_81c_Code_Code_VALUE_C;
            public FormFieldString   UB04_81d_Code_Code_QUAL_D;
            public FormFieldString   UB04_81d_Code_Code_CODE_D;
            public FormFieldString   UB04_81d_Code_Code_VALUE_D;

            #endregion "Variable Declaration"

            private void InitializeBoxes()
            {
                Int32 X = 0;
                Int32 Y = 0;
                Int32 CharSize = 0;

                GetCoordinates("UB04_1_ProviderName", out X, out Y, out CharSize);
                UB04_1_ProviderName = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1_ProviderAddress", out X, out Y, out CharSize);
                UB04_1_ProviderAddress = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1a_ProviderCity", out X, out Y, out CharSize);
                UB04_1a_ProviderCity = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1b_ProviderState", out X, out Y, out CharSize);
                UB04_1b_ProviderState = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1c_ProviderZipCode", out X, out Y, out CharSize);
                UB04_1c_ProviderZipCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1a_ProviderPhone", out X, out Y, out CharSize);
                UB04_1a_ProviderPhone = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1b_ProviderFaxNumber", out X, out Y, out CharSize);
                UB04_1b_ProviderFaxNumber = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2_PayToName", out X, out Y, out CharSize);
                UB04_2_PayToName = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_1c_ProviderCountryCode", out X, out Y, out CharSize);
                UB04_1c_ProviderCountryCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2_PayToAddress", out X, out Y, out CharSize);
                UB04_2_PayToAddress = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2a_Pay_toCity", out X, out Y, out CharSize);
                UB04_2a_Pay_toCity = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2b_Pay_toState", out X, out Y, out CharSize);
                UB04_2b_Pay_toState = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2a_Pay_toZip", out X, out Y, out CharSize);
                UB04_2a_Pay_toZip = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_2b_ReservedFL02", out X, out Y, out CharSize);
                UB04_2b_ReservedFL02 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_3a_PatientControlNumber", out X, out Y, out CharSize);
                UB04_3a_PatientControlNumber = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_3b_MedicalHealthRecordNumber", out X, out Y, out CharSize);
                UB04_3b_MedicalHealthRecordNumber = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_4_TypeofBill", out X, out Y, out CharSize);
                UB04_4_TypeofBill = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_4_TypeofBillFrequencyCode", out X, out Y, out CharSize);
                UB04_4_TypeofBillFrequencyCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_5_FederalTaxNumber_Upperline", out X, out Y, out CharSize);
                UB04_5_FederalTaxNumber_Upperline = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_5_FederalTaxNumber_Lowerline", out X, out Y, out CharSize);
                UB04_5_FederalTaxNumber_Lowerline = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_6a_StatementCoversPeriod_From", out X, out Y, out CharSize);
                UB04_6a_StatementCoversPeriod_From = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_6b_StatementCoversPeriod_Through", out X, out Y, out CharSize);
                UB04_6b_StatementCoversPeriod_Through = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_7a_ReservedFL07A", out X, out Y, out CharSize);
                UB04_7a_ReservedFL07A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_7b_ReservedFL07B", out X, out Y, out CharSize);
                UB04_7b_ReservedFL07B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_8_PatientIdentifier", out X, out Y, out CharSize);
                UB04_8_PatientIdentifier = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_8a_PatientSocialSecurityNumber", out X, out Y, out CharSize);
                UB04_8a_PatientSocialSecurityNumber = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_8b_PatientName", out X, out Y, out CharSize);
                UB04_8b_PatientName = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_9a_PatientStreetAddress", out X, out Y, out CharSize);
                UB04_9a_PatientStreetAddress = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_9b_PatientCity", out X, out Y, out CharSize);
                UB04_9b_PatientCity = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_9c_PatientState", out X, out Y, out CharSize);
                UB04_9c_PatientState = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_9d_PatientZip", out X, out Y, out CharSize);
                UB04_9d_PatientZip = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_9e_PatientCountryCode", out X, out Y, out CharSize);
                UB04_9e_PatientCountryCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_10_PatientBirthDate", out X, out Y, out CharSize);
                UB04_10_PatientBirthDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_11_PatientGender", out X, out Y, out CharSize);
                UB04_11_PatientGender = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_11_PatientMaritalStatus", out X, out Y, out CharSize);
                UB04_11_PatientMaritalStatus = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_11_PatientRace", out X, out Y, out CharSize);
                UB04_11_PatientRace = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_12_Admission_Visit_StartofCareDate", out X, out Y, out CharSize);
                UB04_12_Admission_Visit_StartofCareDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_13_Admission_Visit_Hour", out X, out Y, out CharSize);
                UB04_13_Admission_Visit_Hour = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_14_AdmissionType", out X, out Y, out CharSize);
                UB04_14_AdmissionType = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_15_ReferralSource", out X, out Y, out CharSize);
                UB04_15_ReferralSource = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_16_DischargeHour", out X, out Y, out CharSize);
                UB04_16_DischargeHour = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_17_DischargeStatus", out X, out Y, out CharSize);
                UB04_17_DischargeStatus = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_18_ConditionCodes", out X, out Y, out CharSize);
                UB04_18_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_19_ConditionCodes", out X, out Y, out CharSize);
                UB04_19_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_20_ConditionCodes", out X, out Y, out CharSize);
                UB04_20_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_21_ConditionCodes", out X, out Y, out CharSize);
                UB04_21_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_22_ConditionCodes", out X, out Y, out CharSize);
                UB04_22_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_23_ConditionCodes", out X, out Y, out CharSize);
                UB04_23_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_24_ConditionCodes", out X, out Y, out CharSize);
                UB04_24_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_25_ConditionCodes", out X, out Y, out CharSize);
                UB04_25_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_26_ConditionCodes", out X, out Y, out CharSize);
                UB04_26_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_27_ConditionCodes", out X, out Y, out CharSize);
                UB04_27_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_28_ConditionCodes", out X, out Y, out CharSize);
                UB04_28_ConditionCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_29_AccidentState", out X, out Y, out CharSize);
                UB04_29_AccidentState = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_30_ReservedFL30A", out X, out Y, out CharSize);
                UB04_30_ReservedFL30A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_30_ReservedFL30B", out X, out Y, out CharSize);
                UB04_30_ReservedFL30B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_31a_OccurrenceCode", out X, out Y, out CharSize);
                UB04_31a_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_31a_OccurrenceDate", out X, out Y, out CharSize);
                UB04_31a_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_31b_OccurrenceCode", out X, out Y, out CharSize);
                UB04_31b_OccurrenceCode = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_31b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_31b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_32a_OccurrenceCode", out X, out Y, out CharSize);
                UB04_32a_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_32a_OccurrenceDate", out X, out Y, out CharSize);
                UB04_32a_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_32b_OccurrenceCode", out X, out Y, out CharSize);
                UB04_32b_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_32b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_32b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_32b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_32b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_33a_OccurrenceCode", out X, out Y, out CharSize);
                UB04_33a_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_33a_OccurrenceDate", out X, out Y, out CharSize);
                UB04_33a_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_33b_OccurrenceCode", out X, out Y, out CharSize);
                UB04_33b_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_33b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_33b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_33b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_33b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_34a_OccurrenceCode", out X, out Y, out CharSize);
                UB04_34a_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_34a_OccurrenceDate", out X, out Y, out CharSize);
                UB04_34a_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_34b_OccurrenceCode", out X, out Y, out CharSize);
                UB04_34b_OccurrenceCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_34b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_34b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_34b_OccurrenceDate", out X, out Y, out CharSize);
                UB04_34b_OccurrenceDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35a_OccurrenceSpanCode", out X, out Y, out CharSize);
                UB04_35a_OccurrenceSpanCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35a_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_35a_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35a_OccurrenceSpanDateThrough", out X, out Y, out CharSize);
                UB04_35a_OccurrenceSpanDateThrough = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35b_OccurrenceSpanCode", out X, out Y, out CharSize);
                UB04_35b_OccurrenceSpanCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35b_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_35b_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_35b_OccurrenceSpanDateThrough", out X, out Y, out CharSize);
                UB04_35b_OccurrenceSpanDateThrough = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36b_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_36b_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_36a_OccurrenceSpanCode", out X, out Y, out CharSize);
                UB04_36a_OccurrenceSpanCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36a_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_36a_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36a_OccurrenceSpanDateThrough", out X, out Y, out CharSize);
                UB04_36a_OccurrenceSpanDateThrough = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36b_OccurrenceSpanCode", out X, out Y, out CharSize);
                UB04_36b_OccurrenceSpanCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36b_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_36b_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36b_OccurrenceSpanDateThrough", out X, out Y, out CharSize);
                UB04_36b_OccurrenceSpanDateThrough = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_36b_OccurrenceSpanDateFrom", out X, out Y, out CharSize);
                UB04_36b_OccurrenceSpanDateFrom = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_38_ResponsiblePartyNameAddress", out X, out Y, out CharSize);
                UB04_38_ResponsiblePartyNameAddress = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_39a_ValueCode", out X, out Y, out CharSize);
                UB04_39a_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39a_ValueCodeAmount_Dollars", out X, out Y, out CharSize);
                UB04_39a_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39a_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_39a_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39b_ValueCode", out X, out Y, out CharSize);
                UB04_39b_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39b_ValueCodeAmount_Dollars", out X, out Y, out CharSize);
                UB04_39b_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39b_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_39b_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39c_ValueCode", out X, out Y, out CharSize);
                UB04_39c_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39c_ValueCodeAmount_Dollars", out X, out Y, out CharSize);
                UB04_39c_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39c_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_39c_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39d_ValueCode", out X, out Y, out CharSize);
                UB04_39d_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39d_ValueCodeAmount_Dollars", out X, out Y, out CharSize);
                UB04_39d_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_39d_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_39d_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_40a_ValueCode", out X, out Y, out CharSize);
                UB04_40a_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40a_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_40a_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40a_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_40a_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40b_ValueCode", out X, out Y, out CharSize);
                UB04_40b_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40b_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_40b_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40b_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_40b_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_40c_ValueCode", out X, out Y, out CharSize);
                UB04_40c_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40c_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_40c_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40c_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_40c_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40d_ValueCode", out X, out Y, out CharSize);
                UB04_40d_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40d_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_40d_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_40d_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_40d_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_41a_ValueCode", out X, out Y, out CharSize);
                UB04_41a_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41a_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_41a_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41a_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_41a_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41b_ValueCode", out X, out Y, out CharSize);
                UB04_41b_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41b_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_41b_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41b_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_41b_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41c_ValueCode", out X, out Y, out CharSize);
                UB04_41c_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41c_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_41c_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41c_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_41c_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41d_ValueCode", out X, out Y, out CharSize);
                UB04_41d_ValueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41d_ValueCodeAmount", out X, out Y, out CharSize);
                UB04_41d_ValueCodeAmount = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_41d_ValueCodeAmount_Cents", out X, out Y, out CharSize);
                UB04_41d_ValueCodeAmount_Cents = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_42_RevenueCode", out X, out Y, out CharSize);
                UB04_42_RevenueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_43_RevenueCodeDescription", out X, out Y, out CharSize);
                UB04_43_RevenueCodeDescription = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_44_RateCodes", out X, out Y, out CharSize);
                UB04_44_RateCodes = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_45_ServiceDate_visit", out X, out Y, out CharSize);
                UB04_45_ServiceDate_visit_ = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_46_ServiceUnits", out X, out Y, out CharSize);
                UB04_46_ServiceUnits = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_47a_TotalCharges_Dollars", out X, out Y, out CharSize);
                UB04_47a_TotalCharges_Dollars = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_47b_TotalCharges_Cents", out X, out Y, out CharSize);
                UB04_47b_TotalCharges_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_48a_Non_coveredCharges_Dollars", out X, out Y, out CharSize);
                UB04_48a_Non_coveredCharges_Dollars = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_48b_Non_coveredCharges_Cents", out X, out Y, out CharSize);
                UB04_48b_Non_coveredCharges_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_49_ReservedFL49", out X, out Y, out CharSize);
                UB04_49_ReservedFL49 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_42L23_RevenueCode", out X, out Y, out CharSize);
                UB04_42L23_RevenueCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_47L23_SummaryTotalCharges_Dollars", out X, out Y, out CharSize);
                UB04_47L23_SummaryTotalCharges_Dollars = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_47L23_SummaryTotalCharges_Cents", out X, out Y, out CharSize);
                UB04_47L23_SummaryTotalCharges_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_48L23_SummaryNon_coveredCharges_Dollars", out X, out Y, out CharSize);
                UB04_48L23_SummaryNon_coveredCharges_Dollars = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_48L23_SummaryNon_coveredCharges_Cents", out X, out Y, out CharSize);
                UB04_48L23_SummaryNon_coveredCharges_Cents = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_49L23_Reserved49L23", out X, out Y, out CharSize);
                UB04_49L23_Reserved49L23 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_43L23_CurrentPage", out X, out Y, out CharSize);
                UB04_43L23_CurrentPage = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_44L23_TotalPages", out X, out Y, out CharSize);
                UB04_44L23_TotalPages = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_44L23CreationDate", out X, out Y, out CharSize);
                UB04_44L23CreationDate = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_50_PayerName_Primary", out X, out Y, out CharSize);
                UB04_50_PayerName_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_50_PayerName_Secondary", out X, out Y, out CharSize);
                UB04_50_PayerName_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_50_PayerName_Tertiary", out X, out Y, out CharSize);
                UB04_50_PayerName_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_51_HealthPlanIDA", out X, out Y, out CharSize);
                UB04_51_HealthPlanIDA = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_51_HealthPlanIDB", out X, out Y, out CharSize);
                UB04_51_HealthPlanIDB = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_51_HealthPlanIDC", out X, out Y, out CharSize);
                UB04_51_HealthPlanIDC = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_52_InformationRelease_Primary", out X, out Y, out CharSize);
                UB04_52_InformationRelease_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_52_InformationRelease_Secondary", out X, out Y, out CharSize);
                UB04_52_InformationRelease_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_52_InformationRelease_Tertiary", out X, out Y, out CharSize);
                UB04_52_InformationRelease_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_53_BenefitsAssignment_Primary", out X, out Y, out CharSize);
                UB04_53_BenefitsAssignment_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_53_BenefitsAssignment_Secondary", out X, out Y, out CharSize);
                UB04_53_BenefitsAssignment_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_53_BenefitsAssignment_Tertiary", out X, out Y, out CharSize);
                UB04_53_BenefitsAssignment_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsDollars_Primary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsDollars_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsCents_Primary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsCents_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsDollars_Secondary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsDollars_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsCents_Secondary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsCents_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsDollars_Tertiary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsDollars_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_54_PriorPaymentsCents_Tertiary", out X, out Y, out CharSize);
                UB04_54_PriorPaymentsCents_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55a_EstimatedAmountDueDollars_Primary", out X, out Y, out CharSize);
                UB04_55a_EstimatedAmountDueDollars_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55a_EstimatedAmountDueCents_Primary", out X, out Y, out CharSize);
                UB04_55a_EstimatedAmountDueCents_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55b_EstimatedAmountDueDollars_Secondary", out X, out Y, out CharSize);
                UB04_55b_EstimatedAmountDueDollars_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55b_EstimatedAmountDueCents_Secondary", out X, out Y, out CharSize);
                UB04_55b_EstimatedAmountDueCents_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55c_EstimatedAmountDueDollars_Tertiary", out X, out Y, out CharSize);
                UB04_55c_EstimatedAmountDueDollars_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_55c_EstimatedAmountDueCents_Tertiary", out X, out Y, out CharSize);
                UB04_55c_EstimatedAmountDueCents_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_56_NationalProviderIdentifier_NPI_", out X, out Y, out CharSize);
                UB04_56_NationalProviderIdentifier_NPI_ = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_57_OtherProvider_Primary", out X, out Y, out CharSize);
                UB04_57_OtherProvider_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_57_OtherProvider_Secondary", out X, out Y, out CharSize);
                UB04_57_OtherProvider_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_57_OtherProvider_Tertiary", out X, out Y, out CharSize);
                UB04_57_OtherProvider_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_58_InsuredName_Primary", out X, out Y, out CharSize);
                UB04_58_InsuredName_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_58_InsuredName_Secondary", out X, out Y, out CharSize);
                UB04_58_InsuredName_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_58_InsuredName_Tertiary", out X, out Y, out CharSize);
                UB04_58_InsuredName_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_59_PatientRelationshipToInsured_Primary", out X, out Y, out CharSize);
                UB04_59_PatientRelationshipToInsured_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_59_PatientRelationshipToInsured_Secondary", out X, out Y, out CharSize);
                UB04_59_PatientRelationshipToInsured_Secondary = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_59_PatientRelationshipToInsured_Tertiary", out X, out Y, out CharSize);
                UB04_59_PatientRelationshipToInsured_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_60_InsuredUniqueID_Primary", out X, out Y, out CharSize);
                UB04_60_InsuredUniqueID_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_60_InsuredUniqueID_Secondary", out X, out Y, out CharSize);
                UB04_60_InsuredUniqueID_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_60_InsuredUniqueID_Tertiary", out X, out Y, out CharSize);
                UB04_60_InsuredUniqueID_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_61_InsuredGroupName_Primary", out X, out Y, out CharSize);
                UB04_61_InsuredGroupName_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_61_InsuredGroupName_Secondary", out X, out Y, out CharSize);
                UB04_61_InsuredGroupName_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_61_InsuredGroupName_Tertiary", out X, out Y, out CharSize);
                UB04_61_InsuredGroupName_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_62_InsuredGroupNumber_Primary", out X, out Y, out CharSize);
                UB04_62_InsuredGroupNumber_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_62_InsuredGroupNumber_Secondary", out X, out Y, out CharSize);
                UB04_62_InsuredGroupNumber_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_62_InsuredGroupNumber_Tertiary", out X, out Y, out CharSize);
                UB04_62_InsuredGroupNumber_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_63_TreatmentAuthorizationCode_Primary", out X, out Y, out CharSize);
                UB04_63_TreatmentAuthorizationCode_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_63_TreatmentAuthorizationCode_Secondary", out X, out Y, out CharSize);
                UB04_63_TreatmentAuthorizationCode_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_63_TreatmentAuthorizationCode_Tertiary", out X, out Y, out CharSize);
                UB04_63_TreatmentAuthorizationCode_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_64_DocumentControlNumber_A", out X, out Y, out CharSize);
                UB04_64_DocumentControlNumber_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_64_DocumentControlNumber_B", out X, out Y, out CharSize);
                UB04_64_DocumentControlNumber_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_64_DocumentControlNumber_C", out X, out Y, out CharSize);
                UB04_64_DocumentControlNumber_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_65_EmployerName_Primary", out X, out Y, out CharSize);
                UB04_65_EmployerName_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_65_EmployerName_Secondary", out X, out Y, out CharSize);
                UB04_65_EmployerName_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_65_EmployerName_Tertiary", out X, out Y, out CharSize);
                UB04_65_EmployerName_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_66_ICDVersionIndicator", out X, out Y, out CharSize);
                UB04_66_ICDVersionIndicator = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_67_PrincipalDiagnosisCode", out X, out Y, out CharSize);
                UB04_67_PrincipalDiagnosisCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67a_OtherDiagnosis_A", out X, out Y, out CharSize);
                UB04_67a_OtherDiagnosis_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67b_OtherDiagnosis_B", out X, out Y, out CharSize);
                UB04_67b_OtherDiagnosis_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67c_OtherDiagnosis_C", out X, out Y, out CharSize);
                UB04_67c_OtherDiagnosis_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67d_OtherDiagnosis_D", out X, out Y, out CharSize);
                UB04_67d_OtherDiagnosis_D = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67e_OtherDiagnosis_E", out X, out Y, out CharSize);
                UB04_67e_OtherDiagnosis_E = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67f_OtherDiagnosis_F", out X, out Y, out CharSize);
                UB04_67f_OtherDiagnosis_F = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67g_OtherDiagnosis_G", out X, out Y, out CharSize);
                UB04_67g_OtherDiagnosis_G = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67h_OtherDiagnosis_H", out X, out Y, out CharSize);
                UB04_67h_OtherDiagnosis_H = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67i_OtherDiagnosis_I", out X, out Y, out CharSize);
                UB04_67i_OtherDiagnosis_I = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67j_OtherDiagnosis_J", out X, out Y, out CharSize);
                UB04_67j_OtherDiagnosis_J = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67k_OtherDiagnosis_K", out X, out Y, out CharSize);
                UB04_67k_OtherDiagnosis_K = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67l_OtherDiagnosis_L", out X, out Y, out CharSize);
                UB04_67l_OtherDiagnosis_L = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67m_OtherDiagnosis_M", out X, out Y, out CharSize);
                UB04_67m_OtherDiagnosis_M = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67n_OtherDiagnosis_N", out X, out Y, out CharSize);
                UB04_67n_OtherDiagnosis_N = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67o_OtherDiagnosis_O", out X, out Y, out CharSize);
                UB04_67o_OtherDiagnosis_O = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67p_OtherDiagnosis_P", out X, out Y, out CharSize);
                UB04_67p_OtherDiagnosis_P = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_67q_OtherDiagnosis_Q", out X, out Y, out CharSize);
                UB04_67q_OtherDiagnosis_Q = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_68_Reserved_68A", out X, out Y, out CharSize);
                UB04_68_Reserved_68A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_68_Reserved_68B", out X, out Y, out CharSize);
                UB04_68_Reserved_68B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_69_AdmittingDiagnosisCode", out X, out Y, out CharSize);
                UB04_69_AdmittingDiagnosisCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_70a_PatientVisitReason_A", out X, out Y, out CharSize);
                UB04_70a_PatientVisitReason_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_70b_PatientVisitReason_B", out X, out Y, out CharSize);
                UB04_70b_PatientVisitReason_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_70c_PatientVisitReason_C", out X, out Y, out CharSize);
                UB04_70c_PatientVisitReason_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_71_PPSCode", out X, out Y, out CharSize);
                UB04_71_PPSCode = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_72a_ExternalCauseofInjuryCode_A", out X, out Y, out CharSize);
                UB04_72a_ExternalCauseofInjuryCode_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_72b_ExternalCauseofInjuryCode_B", out X, out Y, out CharSize);
                UB04_72b_ExternalCauseofInjuryCode_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_72c_ExternalCauseofInjuryCode_C", out X, out Y, out CharSize);
                UB04_72c_ExternalCauseofInjuryCode_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_73_ReservedFL73", out X, out Y, out CharSize);
                UB04_73_ReservedFL73 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74_ProcedureCode_Principal", out X, out Y, out CharSize);
                UB04_74_ProcedureCode_Principal = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74_ProcedureDate_Principal", out X, out Y, out CharSize);
                UB04_74_ProcedureDate_Principal = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74a_ProcedureCode_OtherA", out X, out Y, out CharSize);
                UB04_74a_ProcedureCode_OtherA = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74a_ProcedureDate_OtherA", out X, out Y, out CharSize);
                UB04_74a_ProcedureDate_OtherA = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74b_ProcedureCode_OtherB", out X, out Y, out CharSize);
                UB04_74b_ProcedureCode_OtherB = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74b_ProcedureDate_OtherB", out X, out Y, out CharSize);
                UB04_74b_ProcedureDate_OtherB = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74c_ProcedureCode_OtherC", out X, out Y, out CharSize);
                UB04_74c_ProcedureCode_OtherC = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74c_ProcedureDate_OtherC", out X, out Y, out CharSize);
                UB04_74c_ProcedureDate_OtherC = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74d_ProcedureCode_OtherD", out X, out Y, out CharSize);
                UB04_74d_ProcedureCode_OtherD = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74d_ProcedureDate_OtherD", out X, out Y, out CharSize);
                UB04_74d_ProcedureDate_OtherD = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74e_ProcedureCode_OtherE", out X, out Y, out CharSize);
                UB04_74e_ProcedureCode_OtherE = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_74e_ProcedureDate_OtherE", out X, out Y, out CharSize);
                UB04_74e_ProcedureDate_OtherE = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_75a_ReservedFL75A", out X, out Y, out CharSize);
                UB04_75a_ReservedFL75A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_75b_ReservedFL75B", out X, out Y, out CharSize);
                UB04_75b_ReservedFL75B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_75c_ReservedFL75C", out X, out Y, out CharSize);
                UB04_75c_ReservedFL75C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_75d_ReservedFL75D", out X, out Y, out CharSize);
                UB04_75d_ReservedFL75D = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_76_AttendingNPI", out X, out Y, out CharSize);
                UB04_76_AttendingNPI = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_76_AttendingQUAL", out X, out Y, out CharSize);
                UB04_76_AttendingQUAL = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_76_AttendingID", out X, out Y, out CharSize);
                UB04_76_AttendingID = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_76a_AttendingLast", out X, out Y, out CharSize);
                UB04_76a_AttendingLast = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_76b_AttendingFirst", out X, out Y, out CharSize);
                UB04_76b_AttendingFirst = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_77_OperatingNPI", out X, out Y, out CharSize);
                UB04_77_OperatingNPI = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_77_OperatingQUAL", out X, out Y, out CharSize);
                UB04_77_OperatingQUAL = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_77_OperatingID", out X, out Y, out CharSize);
                UB04_77_OperatingID = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_77a_OperatingLast", out X, out Y, out CharSize);
                UB04_77a_OperatingLast = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_77b_OperatingFirst", out X, out Y, out CharSize);
                UB04_77b_OperatingFirst = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherNPI", out X, out Y, out CharSize);
                UB04_78_OtherNPI = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherQUAL", out X, out Y, out CharSize);
                UB04_78_OtherQUAL = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherID", out X, out Y, out CharSize);
                UB04_78_OtherID = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherLast", out X, out Y, out CharSize);
                UB04_78_OtherLast = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherFirst", out X, out Y, out CharSize);
                UB04_78_OtherFirst = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_78_OtherProvider_QUAL", out X, out Y, out CharSize);
                UB04_78_OtherProvider_QUAL = new FormFieldString(X, Y, CharSize);



                GetCoordinates("UB04_79_OtherNPI", out X, out Y, out CharSize);
                UB04_79_OtherNPI = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_79_OtherQUAL", out X, out Y, out CharSize);
                UB04_79_OtherQUAL = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_79_OtherID", out X, out Y, out CharSize);
                UB04_79_OtherID = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_79_OtherLast", out X, out Y, out CharSize);
                UB04_79_OtherLast = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_79_OtherFirst", out X, out Y, out CharSize);
                UB04_79_OtherFirst = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_79_OtherProvider_QUAL", out X, out Y, out CharSize);
                UB04_79_OtherProvider_QUAL = new FormFieldString(X, Y, CharSize);


                GetCoordinates("PayerCodeA_Primary", out X, out Y, out CharSize);
                PayerCodeA_Primary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("PayerCodeB_Secondary", out X, out Y, out CharSize);
                PayerCodeB_Secondary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("PayerCodeC_Tertiary", out X, out Y, out CharSize);
                PayerCodeC_Tertiary = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_80a_Remarks_1", out X, out Y, out CharSize);
                UB04_80a_Remarks_1 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_80b_Remarks_2", out X, out Y, out CharSize);
                UB04_80b_Remarks_2 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_80c_Remarks_3", out X, out Y, out CharSize);
                UB04_80c_Remarks_3 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_80d_Remarks_4", out X, out Y, out CharSize);
                UB04_80d_Remarks_4 = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81a_Code_Code_QUAL_A", out X, out Y, out CharSize);
                UB04_81a_Code_Code_QUAL_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81a_Code_Code_CODE_A", out X, out Y, out CharSize);
                UB04_81a_Code_Code_CODE_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81a_Code_Code_VALUE_A", out X, out Y, out CharSize);
                UB04_81a_Code_Code_VALUE_A = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81b_Code_Code_QUAL_B", out X, out Y, out CharSize);
                UB04_81b_Code_Code_QUAL_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81b_Code_Code_CODE_B", out X, out Y, out CharSize);
                UB04_81b_Code_Code_CODE_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81b_Code_Code_VALUE_B", out X, out Y, out CharSize);
                UB04_81b_Code_Code_VALUE_B = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81c_Code_Code_QUAL_C", out X, out Y, out CharSize);
                UB04_81c_Code_Code_QUAL_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81c_Code_Code_CODE_C", out X, out Y, out CharSize);
                UB04_81c_Code_Code_CODE_C = new FormFieldString(X, Y, CharSize);


                GetCoordinates("UB04_81c_Code_Code_VALUE_C", out X, out Y, out CharSize);
                UB04_81c_Code_Code_VALUE_C = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81d_Code_Code_QUAL_D", out X, out Y, out CharSize);
                UB04_81d_Code_Code_QUAL_D = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81d_Code_Code_CODE_D", out X, out Y, out CharSize);
                UB04_81d_Code_Code_CODE_D = new FormFieldString(X, Y, CharSize);

                GetCoordinates("UB04_81d_Code_Code_VALUE_D", out X, out Y, out CharSize);
                UB04_81d_Code_Code_VALUE_D = new FormFieldString(X, Y, CharSize);




            }

         private DataTable GetPrinterSettingNew(string sPrinterName = "Default")
         {
             try
             {
                 string _Query = "";
                 DataTable dtSettings;
                 gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                 _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName,nCharSize AS nCharSize " +
                         " FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType = 1 and UPPER(RTRIM(LTRIM(sPrinterName)))='" + sPrinterName.Trim().ToUpper() + "'";
                 oDB.Connect(false);
                 oDB.Retrive_Query(_Query, out dtSettings);
                 oDB.Disconnect();
                 oDB.Dispose();
                 return dtSettings;
             }
             catch (Exception ex)
             {
                 //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 throw ex;
             }
         }

         private DataTable GetDefaultPrinterSettingNew(string sPrinterName = "Default")
         {
             try
             {
                 string _Query = "";
                 DataTable dtSettings;
                 gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                 _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName, nCharSize AS nCharSize" +
                         " FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType = 0  and UPPER(RTRIM(LTRIM(sPrinterName)))='" + sPrinterName.Trim().ToUpper() + "'";
                 oDB.Connect(false);
                 oDB.Retrive_Query(_Query, out dtSettings);
                 oDB.Disconnect();
                 oDB.Dispose();
                 return dtSettings;
             }
             catch (Exception ex)
             {
                 //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                 throw ex;
             }
         }
        public DataTable GetDefaultPrinterSetting(bool IsPrintForm)
        {
            try
            {
                string _Query = "";
                DataTable dtSettings;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                if (!IsPrintForm)
                {
                    _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName ,nCharSize AS nCharSize" +
                            " FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType = 1";
                }
                else
                {
                    _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName,nCharSize AS nCharSize " +
                              " FROM BL_UB04PrintSettings WHERE nClinicID = 1 AND nFormType = 2";
                }
                oDB.Connect(false);
                oDB.Retrive_Query(_Query, out dtSettings);
                oDB.Disconnect();
                oDB.Dispose();
                return dtSettings;
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }
       
        private DataTable GetPrinterCoordinates(string sPrinterName = "Default", Int16 FormType = 0)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtSettings = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@PaperForm", "UB04", ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@PrinterName", sPrinterName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@FormType", FormType, ParameterDirection.Input, SqlDbType.SmallInt);
                oDB.Retrive("gsp_GetPrinter_Coordinates", oParameters, out dtSettings);
                oDB.Disconnect();
                return dtSettings;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
                return dtSettings;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return dtSettings;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (dtSettings != null) { dtSettings.Dispose(); }
            }
        }
        private void GetCoordinates(String sBoxName, out Int32 nX, out Int32 nY, out Int32 nCharSize)
        {
            try
            {
                nX = 0;
                nY = 0;
                nCharSize = 30;
                DataView oView;
                DataTable oSetting = new DataTable();
                if (dtSettings != null && dtSettings.Rows.Count > 0)
                {
                    oView = dtSettings.DefaultView;
                    oView.RowFilter = "sBoxName = '" + sBoxName + "'";
                    oSetting = oView.ToTable();
                    if (oSetting.Rows.Count > 0)
                    {
                        nX = Convert.ToInt32(oSetting.Rows[0]["nXCoordinate"]);
                        nY = Convert.ToInt32(oSetting.Rows[0]["nYCoordinate"]);
                        nCharSize = Convert.ToInt32(oSetting.Rows[0]["nCharSize"]);
                    }

                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
        }

        public class FormFieldString
        {
            #region "Constructor & Destructor"

            public FormFieldString(float PointX, float PointY, Int32 nCharSize = 30)
            {
                _LocationX = PointX;
                _LocationY = PointY;
                _CharSize = nCharSize;
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                }
                disposed = true;
            }

            ~FormFieldString()
            {
                Dispose(false);
            }

            #endregion

            private string _FieldValue = "";
            private float _LocationX = 0;
            private float _LocationY = 0;
            private Int32 _CharSize = 0;

            public string Value
            {
                get { return _FieldValue; }
                set { _FieldValue = value; }
            }

            public System.Drawing.PointF Location
            {
                get { return new System.Drawing.PointF(_LocationX, _LocationY); }
                set{_LocationX = value.X ;
                _LocationY = value.Y;
            }

            }

            public Int32 CharSize
            {
                get { return _CharSize; }
                set { _CharSize = value; }
            }

        }

        public class FormFieldBoolean
        {
            #region "Constructor & Destructor"

            public FormFieldBoolean(float PointX, float PointY)
            {
                _LocationX = PointX;
                _LocationY = PointY;
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                }
                disposed = true;
            }

            ~FormFieldBoolean()
            {
                Dispose(false);
            }

            #endregion

            private bool _FieldValue = false;
            private float _LocationX = 0;
            private float _LocationY = 0;

            public bool Value
            {
                get { return _FieldValue; }
                set { _FieldValue = value; }
            }

            public System.Drawing.PointF Location
            {
                get { return new System.Drawing.PointF(_LocationX, _LocationY); }
            }

        }
         
    }

    public class ServiceLine
    {

        #region "Private Variable"

        public FormFieldString UB04_42_RevenueCode;
        public FormFieldString UB04_43_RevenueCodeDescription;
        public FormFieldString UB04_44_RateCodes;
        public FormFieldString UB04_45_ServiceDate_visit_;
        public FormFieldString UB04_46_ServiceUnits;
        public FormFieldString UB04_47a_TotalCharges_Dollars;
        public FormFieldString UB04_47b_TotalCharges_Cents;
        public FormFieldString UB04_48a_Non_coveredCharges_Dollars;
        public FormFieldString UB04_48b_Non_coveredCharges_Cents;


        #endregion

#region "Constructor & Destructor "
       public ServiceLine()
        {
            UB04_42_RevenueCode = new FormFieldString();
            UB04_43_RevenueCodeDescription = new FormFieldString();
            UB04_44_RateCodes = new FormFieldString();
            UB04_45_ServiceDate_visit_ = new FormFieldString();
            UB04_47a_TotalCharges_Dollars = new FormFieldString();
            UB04_46_ServiceUnits = new FormFieldString();
            UB04_47b_TotalCharges_Cents = new FormFieldString();
            UB04_48a_Non_coveredCharges_Dollars = new FormFieldString();
            UB04_48b_Non_coveredCharges_Cents = new FormFieldString();
        }   
#endregion


    }

    public class ServiceLines
    {

        public ArrayList innerlist;
        #region "constructor"

        public ServiceLines()
        {
            innerlist = new ArrayList();

        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        ~ServiceLines()
        {
            Dispose(false);
        }
        #endregion

        public ServiceLine this[int index]
        {
            get { return (ServiceLine)innerlist[index]; }
        }

        //' Methods Add, Remove, Count , Item
        public Int32 Count()
        {

            return Convert.ToInt32(innerlist.Count);

        }


        public void Add(ServiceLine item)
        {
            innerlist.Add(item);
        }

        public bool Remove(ServiceLine item)
        {
            //'Remark - Work Remining for comparision
            bool result = false;
            return result;

        }
        public bool RemoveAt(Int16 index)
        {

            bool result = false;
            innerlist.RemoveAt(index);
            result = true;
            return result;

        }
        public void Clear()
        {
            innerlist.Clear();
        }
    }

}
