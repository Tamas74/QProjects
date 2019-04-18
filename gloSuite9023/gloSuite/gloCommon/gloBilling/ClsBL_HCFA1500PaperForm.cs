using System;
using System.Collections.Generic;
using System.Text;

namespace gloBilling
{
    namespace Common
    {
        public class gloHCFA1500PaperForm
        {
            #region "Constructor & Destructor"

            public gloHCFA1500PaperForm()
            {
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

            ~gloHCFA1500PaperForm()
            {
                Dispose(false);
            }

            #endregion

            //.Insurance Header
            public FormFieldString CF_Top_InsuranceHeader = new FormFieldString(1042, 112);

            //.Insurance Type
            public FormFieldBoolean CF_1_Insuracne_Type_Medicare = new FormFieldBoolean(162, 297);
            public FormFieldBoolean CF_1_Insuracne_Type_Medicaid = new FormFieldBoolean(298, 296);
            public FormFieldBoolean CF_1_Insuracne_Type_Tricare = new FormFieldBoolean(438, 297);
            public FormFieldBoolean CF_1_Insuracne_Type_Champva = new FormFieldBoolean(618, 296);
            public FormFieldBoolean CF_1_Insuracne_Type_GroupHealthPlan = new FormFieldBoolean(759, 296);
            public FormFieldBoolean CF_1_Insuracne_Type_FECA = new FormFieldBoolean(918, 296);
            public FormFieldBoolean CF_1_Insuracne_Type_Other = new FormFieldBoolean(1039, 296);

            //.Insured's ID Number
            public FormFieldString CF_1a_InsuredsIDNumber = new FormFieldString(1150, 301);

            //.Patient's Name
            public FormFieldString CF_2_Patient_Name = new FormFieldString(169, 365);

            //.Patient's Birth Date
            public FormFieldString CF_3_Patient_DOB_MM = new FormFieldString(762, 372);
            public FormFieldString CF_3_Patient_DOB_DD = new FormFieldString(822, 372);
            public FormFieldString CF_3_Patient_DOB_YY = new FormFieldString(897, 372);

            //.Patient's Sex
            public FormFieldBoolean CF_3_Patient_Sex_Male = new FormFieldBoolean(978, 361);
            public FormFieldBoolean CF_3_Patient_Sex_Female = new FormFieldBoolean(1078, 361);

            //.Insured's Name
            public FormFieldString CF_4_Insureds_Name = new FormFieldString(1150, 366);

            //.Patient's Address
            public FormFieldString CF_5_Patient_Address = new FormFieldString(169, 429);
            public FormFieldString CF_5_Patient_City = new FormFieldString(169, 495);
            public FormFieldString CF_5_Patient_State = new FormFieldString(674, 495);
            public FormFieldString CF_5_Patient_Zip = new FormFieldString(169, 568);
            public FormFieldString CF_5_Patient_Tel_AreaCode = new FormFieldString(445, 570);
            public FormFieldString CF_5_Patient_Tel_Number = new FormFieldString(525, 570);

            //.Paient Relationship to Insured
            public FormFieldBoolean CF_6_PatientRelationship_Self = new FormFieldBoolean(799, 431);
            public FormFieldBoolean CF_6_PatientRelationship_Spouse = new FormFieldBoolean(899, 431);
            public FormFieldBoolean CF_6_PatientRelationship_Child = new FormFieldBoolean(979, 431);
            public FormFieldBoolean CF_6_PatientRelationship_Other = new FormFieldBoolean(1079, 429);

            //.Insured's Address
            public FormFieldString CF_7_Insureds_Address = new FormFieldString(1150, 428);
            public FormFieldString CF_7_Insureds_City = new FormFieldString(1150, 496);
            public FormFieldString CF_7_Insureds_State = new FormFieldString(1620, 496);
            public FormFieldString CF_7_Insureds_Zip = new FormFieldString(1150, 566);
            public FormFieldString CF_7_Insureds_Tel_AreaCode = new FormFieldString(1452, 566);
            public FormFieldString CF_7_Insureds_Tel_Number = new FormFieldString(1523, 566);

            //.Patient Status
            public FormFieldBoolean CF_8_PatientStatus_Single = new FormFieldBoolean(840, 497);
            public FormFieldBoolean CF_8_PatientStatus_Married = new FormFieldBoolean(959, 496);
            public FormFieldBoolean CF_8_PatientStatus_Other = new FormFieldBoolean(1079, 497);
            public FormFieldBoolean CF_8_PatientStatus_Employed = new FormFieldBoolean(840, 565);
            public FormFieldBoolean CF_8_PatientStatus_FullTimeStudent = new FormFieldBoolean(959, 564);
            public FormFieldBoolean CF_8_PatientStatus_PartTimeStudent = new FormFieldBoolean(1079, 564);

            //.Other Insured's Name
            public FormFieldString CF_9_Other_Insureds_Name = new FormFieldString(169, 632);
            public FormFieldString CF_9_Other_Insureds_PolicyGroupNo = new FormFieldString(169, 695);
            public FormFieldString CF_9_Other_Insureds_DOB_MM = new FormFieldString(189, 772);
            public FormFieldString CF_9_Other_Insureds_DOB_DD = new FormFieldString(248, 772);
            public FormFieldString CF_9_Other_Insureds_DOB_YY = new FormFieldString(322, 772);
            public FormFieldBoolean CF_9_Other_Insureds_Sex_Male = new FormFieldBoolean(501, 766);
            public FormFieldBoolean CF_9_Other_Insureds_Sex_Female = new FormFieldBoolean(619, 765);
            public FormFieldString CF_9_Other_Insureds_EmployerName = new FormFieldString(169, 832);
            public FormFieldString CF_9_Other_Insureds_InsuracnePlan = new FormFieldString(169, 898);

            //.Patient Condition 
            public FormFieldBoolean CF_10_PatientConditionTo_Employement_Yes = new FormFieldBoolean(840, 699);
            public FormFieldBoolean CF_10_PatientConditionTo_Employement_No = new FormFieldBoolean(959, 699);
            public FormFieldBoolean CF_10_PatientConditionTo_AutoAccident_Yes = new FormFieldBoolean(839, 765);
            public FormFieldBoolean CF_10_PatientConditionTo_AutoAccident_No = new FormFieldBoolean(959, 765);
            public FormFieldString CF_10_PatientConditionTo_AutoAccident_State = new FormFieldString(1041, 775);
            public FormFieldBoolean CF_10_PatientConditionTo_OtherAccident_Yes = new FormFieldBoolean(840, 831);
            public FormFieldBoolean CF_10_PatientConditionTo_OtherAccident_No = new FormFieldBoolean(960, 831);
            public FormFieldString CF_10_PatientConditionTo_ResForLocaluse = new FormFieldString(748, 898);

            //.Insured's Information
            public FormFieldString CF_11_Insureds_PolicyGroupNo = new FormFieldString(1150, 630);
            public FormFieldString CF_11_Insureds_DOB_MM = new FormFieldString(1212, 708);
            public FormFieldString CF_11_Insureds_DOB_DD = new FormFieldString(1270, 708);
            public FormFieldString CF_11_Insureds_DOB_YY = new FormFieldString(1350, 708);
            public FormFieldBoolean CF_11_Insureds_Sex_Male = new FormFieldBoolean(1498, 698);
            public FormFieldBoolean CF_11_Insureds_Sex_Female = new FormFieldBoolean(1638, 698);
            public FormFieldString CF_11_Insureds_EmployerName = new FormFieldString(1150, 764);
            public FormFieldString CF_11_Insureds_InsuracnePlan = new FormFieldString(1150, 830);
            public FormFieldBoolean CF_11_Insureds_OtherHealthPlan_Yes = new FormFieldBoolean(1177, 898);
            public FormFieldBoolean CF_11_Insureds_OtherHealthPlan_No = new FormFieldBoolean(1278, 898);

            public FormFieldString CF_12_PatientAuthorizedPersons_Signature = new FormFieldString(286, 1032);
            public FormFieldString CF_12_PatientAuthorizedPersons_Signature_Date = new FormFieldString(869, 1035);

            public FormFieldString CF_13_InsuredsAuthorizedPersons_Signature = new FormFieldString(1259, 1034);

            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM = new FormFieldString(189, 1108);
            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD = new FormFieldString(248, 1108);
            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY = new FormFieldString(322, 1108);

            public FormFieldString CF_15_FirstDateOfSimilar_Illness_MM = new FormFieldString(884, 1106);
            public FormFieldString CF_15_FirstDateOfSimilar_Illness_DD = new FormFieldString(948, 1106);
            public FormFieldString CF_15_FirstDateOfSimilar_Illness_YY = new FormFieldString(1022, 1106);

            public FormFieldString CF_16_UnableToWorkFromDate_MM = new FormFieldString(1221, 1106);
            public FormFieldString CF_16_UnableToWorkFromDate_DD = new FormFieldString(1284, 1106);
            public FormFieldString CF_16_UnableToWorkFromDate_YY = new FormFieldString(1362, 1106);

            public FormFieldString CF_16_UnableToWorkTillDate_MM = new FormFieldString(1500, 1106);
            public FormFieldString CF_16_UnableToWorkTillDate_DD = new FormFieldString(1563, 1106);
            public FormFieldString CF_16_UnableToWorkTillDate_YY = new FormFieldString(1641, 1106);

            public FormFieldString CF_17_ReferringProvider_Name = new FormFieldString(169, 1162);
            public FormFieldString CF_17a_ReferringProvider_UnknownField = new FormFieldString(796, 1142);
            public FormFieldString CF_17b_ReferringProvider_NPI = new FormFieldString(796, 1173);

            public FormFieldString CF_18_HospitalizationFromDate_MM = new FormFieldString(1223, 1170);
            public FormFieldString CF_18_HospitalizationFromDate_DD = new FormFieldString(1286, 1170);
            public FormFieldString CF_18_HospitalizationFromDate_YY = new FormFieldString(1364, 1170);

            public FormFieldString CF_18_HospitalizationTillDate_MM = new FormFieldString(1501, 1171);
            public FormFieldString CF_18_HospitalizationTillDate_DD = new FormFieldString(1564, 1170);
            public FormFieldString CF_18_HospitalizationTillDate_YY = new FormFieldString(1643, 1170);

            public FormFieldString CF_19_LocalUse_Field = new FormFieldString(169, 1228);

            public FormFieldBoolean CF_20_OutsideLab_Yes = new FormFieldBoolean(1179, 1230);
            public FormFieldBoolean CF_20_OutsideLab_No = new FormFieldBoolean(1280, 1230);
            public FormFieldString CF_20_OutsideLab_Charges_Principal = new FormFieldString(1466, 1235);
            public FormFieldString CF_20_OutsideLab_Charges_Secondary = new FormFieldString(1571, 1235);

            public FormFieldString CF_21_Diagnosis_1_Principal = new FormFieldString(205, 1308);
            public FormFieldString CF_21_Diagnosis_1_Secondary = new FormFieldString(284, 1308);
            public FormFieldString CF_21_Diagnosis_2_Principal = new FormFieldString(204, 1374);
            public FormFieldString CF_21_Diagnosis_2_Secondary = new FormFieldString(283, 1374);
            public FormFieldString CF_21_Diagnosis_3_Principal = new FormFieldString(744, 1308);
            public FormFieldString CF_21_Diagnosis_3_Secondary = new FormFieldString(823, 1308);
            public FormFieldString CF_21_Diagnosis_4_Principal = new FormFieldString(744, 1374);
            public FormFieldString CF_21_Diagnosis_4_Secondary = new FormFieldString(823, 1374);

            public FormFieldString CF_22_MecaidResubmission_Code = new FormFieldString(1150, 1307);
            public FormFieldString CF_22_Original_Refrence_No = new FormFieldString(1393, 1307);

            public FormFieldString CF_23_PriorAuthorization_No = new FormFieldString(1150, 1364);

            #region " Service Line 1 "

            public bool CF_IsPresent_Line1 = false;

            //From Date.
            public FormFieldString CF_24A_L1_DOS_From_MM = new FormFieldString(167, 1505);
            public FormFieldString CF_24A_L1_DOS_From_DD = new FormFieldString(225, 1505);
            public FormFieldString CF_24A_L1_DOS_From_YY = new FormFieldString(283, 1505);

            //To Date
            public FormFieldString CF_24A_L1_DOS_To_MM = new FormFieldString(343, 1505);
            public FormFieldString CF_24A_L1_DOS_To_DD = new FormFieldString(401, 1505);
            public FormFieldString CF_24A_L1_DOS_To_YY = new FormFieldString(464, 1505);

            //Place Of Service
            public FormFieldString CF_24B_L1_POS_Code = new FormFieldString(525, 1505);

            //EMG - Emergency
            public FormFieldString CF_24C_L1_EMG_Code = new FormFieldString(584, 1504);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L1_CPT_HCPCS_Code = new FormFieldString(662, 1505);

            //Modifiers
            public FormFieldString CF_24D_L1_Modifier_1_Code = new FormFieldString(803, 1505);
            public FormFieldString CF_24D_L1_Modifier_2_Code = new FormFieldString(862, 1505);
            public FormFieldString CF_24D_L1_Modifier_3_Code = new FormFieldString(922, 1505);
            public FormFieldString CF_24D_L1_Modifier_4_Code = new FormFieldString(983, 1505);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L1_Diagnosis_Pointers = new FormFieldString(1047, 1505);

            //Charges
            public FormFieldString CF_24F_L1_Charges_Principal = new FormFieldString(1161, 1505);
            public FormFieldString CF_24F_L1_Charges_Secondary = new FormFieldString(1269, 1505);

            //Days or Units
            public FormFieldString CF_24G_L1_Days_Units = new FormFieldString(1328, 1505);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L1_EPSDT_FamilyPlan = new FormFieldString(1403, 1505);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L1_RenderingProvider_NPI = new FormFieldString(1507, 1505);

            //Line Note
            public FormFieldString CF_24A_L1_Note = new FormFieldString(169, 1472);

            #endregion

            #region " Service Line 2 "

            public bool CF_IsPresent_Line2 = false;

            //From Date.
            public FormFieldString CF_24A_L2_DOS_From_MM = new FormFieldString(167, 1573);
            public FormFieldString CF_24A_L2_DOS_From_DD = new FormFieldString(225, 1573);
            public FormFieldString CF_24A_L2_DOS_From_YY = new FormFieldString(283, 1573);

            //To Date
            public FormFieldString CF_24A_L2_DOS_To_MM = new FormFieldString(343, 1573);
            public FormFieldString CF_24A_L2_DOS_To_DD = new FormFieldString(401, 1573);
            public FormFieldString CF_24A_L2_DOS_To_YY = new FormFieldString(464, 1573);

            //Place Of Service
            public FormFieldString CF_24B_L2_POS_Code = new FormFieldString(525, 1573);

            //EMG - Emergency
            public FormFieldString CF_24C_L2_EMG_Code = new FormFieldString(584, 1572);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L2_CPT_HCPCS_Code = new FormFieldString(662, 1573);

            //Modifiers
            public FormFieldString CF_24D_L2_Modifier_1_Code = new FormFieldString(803, 1573);
            public FormFieldString CF_24D_L2_Modifier_2_Code = new FormFieldString(862, 1573);
            public FormFieldString CF_24D_L2_Modifier_3_Code = new FormFieldString(922, 1573);
            public FormFieldString CF_24D_L2_Modifier_4_Code = new FormFieldString(983, 1573);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L2_Diagnosis_Pointers = new FormFieldString(1047, 1573);

            //Charges
            public FormFieldString CF_24F_L2_Charges_Principal = new FormFieldString(1161, 1573);
            public FormFieldString CF_24F_L2_Charges_Secondary = new FormFieldString(1269, 1573);

            //Days or Units
            public FormFieldString CF_24G_L2_Days_Units = new FormFieldString(1328, 1573);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L2_EPSDT_FamilyPlan = new FormFieldString(1403, 1573);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L2_RenderingProvider_NPI = new FormFieldString(1507, 1573);

            //Line Note
            public FormFieldString CF_24A_L2_Note = new FormFieldString(167, 1537);

            #endregion

            #region " Service Line 3 "

            public bool CF_IsPresent_Line3 = false;

            //From Date.
            public FormFieldString CF_24A_L3_DOS_From_MM = new FormFieldString(167, 1638);
            public FormFieldString CF_24A_L3_DOS_From_DD = new FormFieldString(225, 1638);
            public FormFieldString CF_24A_L3_DOS_From_YY = new FormFieldString(283, 1638);

            //To Date
            public FormFieldString CF_24A_L3_DOS_To_MM = new FormFieldString(343, 1638);
            public FormFieldString CF_24A_L3_DOS_To_DD = new FormFieldString(401, 1638);
            public FormFieldString CF_24A_L3_DOS_To_YY = new FormFieldString(464, 1638);

            //Place Of Service
            public FormFieldString CF_24B_L3_POS_Code = new FormFieldString(525, 1638);

            //EMG - Emergency
            public FormFieldString CF_24C_L3_EMG_Code = new FormFieldString(584, 1637);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L3_CPT_HCPCS_Code = new FormFieldString(662, 1638);

            //Modifiers
            public FormFieldString CF_24D_L3_Modifier_1_Code = new FormFieldString(803, 1638);
            public FormFieldString CF_24D_L3_Modifier_2_Code = new FormFieldString(862, 1638);
            public FormFieldString CF_24D_L3_Modifier_3_Code = new FormFieldString(922, 1638);
            public FormFieldString CF_24D_L3_Modifier_4_Code = new FormFieldString(983, 1638);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L3_Diagnosis_Pointers = new FormFieldString(1047, 1638);

            //Charges
            public FormFieldString CF_24F_L3_Charges_Principal = new FormFieldString(1161, 1638);
            public FormFieldString CF_24F_L3_Charges_Secondary = new FormFieldString(1269, 1638);

            //Days or Units
            public FormFieldString CF_24G_L3_Days_Units = new FormFieldString(1328, 1638);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L3_EPSDT_FamilyPlan = new FormFieldString(1403, 1638);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L3_RenderingProvider_NPI = new FormFieldString(1507, 1638);

            //Line Note
            public FormFieldString CF_24A_L3_Note = new FormFieldString(167, 1604);

            #endregion

            #region " Service Line 4 "

            public bool CF_IsPresent_Line4 = false;

            //From Date.
            public FormFieldString CF_24A_L4_DOS_From_MM = new FormFieldString(167, 1704);
            public FormFieldString CF_24A_L4_DOS_From_DD = new FormFieldString(225, 1704);
            public FormFieldString CF_24A_L4_DOS_From_YY = new FormFieldString(283, 1704);

            //To Date
            public FormFieldString CF_24A_L4_DOS_To_MM = new FormFieldString(343, 1704);
            public FormFieldString CF_24A_L4_DOS_To_DD = new FormFieldString(401, 1704);
            public FormFieldString CF_24A_L4_DOS_To_YY = new FormFieldString(464, 1704);

            //Place Of Service
            public FormFieldString CF_24B_L4_POS_Code = new FormFieldString(525, 1704);

            //EMG - Emergency
            public FormFieldString CF_24C_L4_EMG_Code = new FormFieldString(584, 1703);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L4_CPT_HCPCS_Code = new FormFieldString(662, 1704);

            //Modifiers
            public FormFieldString CF_24D_L4_Modifier_1_Code = new FormFieldString(803, 1704);
            public FormFieldString CF_24D_L4_Modifier_2_Code = new FormFieldString(862, 1704);
            public FormFieldString CF_24D_L4_Modifier_3_Code = new FormFieldString(922, 1704);
            public FormFieldString CF_24D_L4_Modifier_4_Code = new FormFieldString(983, 1704);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L4_Diagnosis_Pointers = new FormFieldString(1047, 1704);

            //Charges
            public FormFieldString CF_24F_L4_Charges_Principal = new FormFieldString(1161, 1704);
            public FormFieldString CF_24F_L4_Charges_Secondary = new FormFieldString(1269, 1704);

            //Days or Units
            public FormFieldString CF_24G_L4_Days_Units = new FormFieldString(1328, 1704);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L4_EPSDT_FamilyPlan = new FormFieldString(1403, 1704);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L4_RenderingProvider_NPI = new FormFieldString(1507, 1704);

            //Line Note
            public FormFieldString CF_24A_L4_Note = new FormFieldString(167, 1674);

            #endregion

            #region " Service Line 5 "

            public bool CF_IsPresent_Line5 = false;

            //From Date.
            public FormFieldString CF_24A_L5_DOS_From_MM = new FormFieldString(167, 1773);
            public FormFieldString CF_24A_L5_DOS_From_DD = new FormFieldString(225, 1773);
            public FormFieldString CF_24A_L5_DOS_From_YY = new FormFieldString(283, 1773);

            //To Date
            public FormFieldString CF_24A_L5_DOS_To_MM = new FormFieldString(343, 1773);
            public FormFieldString CF_24A_L5_DOS_To_DD = new FormFieldString(401, 1773);
            public FormFieldString CF_24A_L5_DOS_To_YY = new FormFieldString(464, 1773);

            //Place Of Service
            public FormFieldString CF_24B_L5_POS_Code = new FormFieldString(525, 1773);

            //EMG - Emergency
            public FormFieldString CF_24C_L5_EMG_Code = new FormFieldString(584, 1772);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L5_CPT_HCPCS_Code = new FormFieldString(662, 1773);

            //Modifiers
            public FormFieldString CF_24D_L5_Modifier_1_Code = new FormFieldString(803, 1773);
            public FormFieldString CF_24D_L5_Modifier_2_Code = new FormFieldString(862, 1773);
            public FormFieldString CF_24D_L5_Modifier_3_Code = new FormFieldString(922, 1773);
            public FormFieldString CF_24D_L5_Modifier_4_Code = new FormFieldString(983, 1773);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L5_Diagnosis_Pointers = new FormFieldString(1047, 1773);

            //Charges
            public FormFieldString CF_24F_L5_Charges_Principal = new FormFieldString(1161, 1773);
            public FormFieldString CF_24F_L5_Charges_Secondary = new FormFieldString(1269, 1773);

            //Days or Units
            public FormFieldString CF_24G_L5_Days_Units = new FormFieldString(1328, 1773);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L5_EPSDT_FamilyPlan = new FormFieldString(1403, 1773);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L5_RenderingProvider_NPI = new FormFieldString(1507, 1773);

            //Line Note
            public FormFieldString CF_24A_L5_Note = new FormFieldString(167, 1739);

            #endregion

            #region " Service Line 6 "

            public bool CF_IsPresent_Line6 = false;

            //From Date.
            public FormFieldString CF_24A_L6_DOS_From_MM = new FormFieldString(167, 1838);
            public FormFieldString CF_24A_L6_DOS_From_DD = new FormFieldString(225, 1838);
            public FormFieldString CF_24A_L6_DOS_From_YY = new FormFieldString(283, 1838);

            //To Date
            public FormFieldString CF_24A_L6_DOS_To_MM = new FormFieldString(343, 1838);
            public FormFieldString CF_24A_L6_DOS_To_DD = new FormFieldString(401, 1838);
            public FormFieldString CF_24A_L6_DOS_To_YY = new FormFieldString(464, 1838);

            //Place Of Service
            public FormFieldString CF_24B_L6_POS_Code = new FormFieldString(525, 1838);

            //EMG - Emergency
            public FormFieldString CF_24C_L6_EMG_Code = new FormFieldString(584, 1837);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L6_CPT_HCPCS_Code = new FormFieldString(662, 1838);

            //Modifiers
            public FormFieldString CF_24D_L6_Modifier_1_Code = new FormFieldString(803, 1838);
            public FormFieldString CF_24D_L6_Modifier_2_Code = new FormFieldString(862, 1838);
            public FormFieldString CF_24D_L6_Modifier_3_Code = new FormFieldString(922, 1838);
            public FormFieldString CF_24D_L6_Modifier_4_Code = new FormFieldString(983, 1838);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L6_Diagnosis_Pointers = new FormFieldString(1047, 1838);

            //Charges
            public FormFieldString CF_24F_L6_Charges_Principal = new FormFieldString(1161, 1838);
            public FormFieldString CF_24F_L6_Charges_Secondary = new FormFieldString(1269, 1838);

            //Days or Units
            public FormFieldString CF_24G_L6_Days_Units = new FormFieldString(1328, 1838);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L6_EPSDT_FamilyPlan = new FormFieldString(1403, 1838);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L6_RenderingProvider_NPI = new FormFieldString(1507, 1838);

            //Line Note
            public FormFieldString CF_24A_L6_Note = new FormFieldString(167, 1802);

            #endregion

            public FormFieldString CF_25_FederalTax_ID_No = new FormFieldString(167, 1895);
            public FormFieldBoolean CF_25_FederalTaxID_Qualifier_SSN = new FormFieldBoolean(482, 1895);
            public FormFieldBoolean CF_25_FederalTaxID_Qualifier_EIN = new FormFieldBoolean(522, 1895);

            public FormFieldString CF_26_PatientAccount_No = new FormFieldString(615, 1895);

            public FormFieldBoolean CF_27_AcceptAssignment_YES = new FormFieldBoolean(901, 1895);
            public FormFieldBoolean CF_27_AcceptAssignment_NO = new FormFieldBoolean(1001, 1895);

            public FormFieldString CF_28_TotalCharge_Principal = new FormFieldString(1171, 1901);
            public FormFieldString CF_28_TotalCharge_Secondary = new FormFieldString(1314, 1901);

            public FormFieldString CF_29_AmountPaid_Principal = new FormFieldString(1392, 1901);
            public FormFieldString CF_29_AmountPaid_Secondary = new FormFieldString(1511, 1901);

            public FormFieldString CF_30_BalanceDue_Principal = new FormFieldString(1573, 1901);
            public FormFieldString CF_30_BalanceDue_Secondary = new FormFieldString(1692, 1901);

            public FormFieldString CF_31_Physician_Supplier_Signature = new FormFieldString(167, 2054);
            public FormFieldString CF_31_Physician_Supplier_Signature_Date = new FormFieldString(450,2050); //(519, 2078);

            public FormFieldString CF_32_Service_Facility_Name = new FormFieldString(620, 1964);
            public FormFieldString CF_32_Service_Facility_Address_Line1 = new FormFieldString(620, 1991);
            public FormFieldString CF_32_Service_Facility_Address_Line2 = new FormFieldString(620, 2019);
            public FormFieldString CF_32_Service_Facility_City = new FormFieldString(3, 2);
            public FormFieldString CF_32_Service_Facility_State = new FormFieldString(3, 2);
            public FormFieldString CF_32_Service_Facility_Zip = new FormFieldString(3, 2);
            public FormFieldString CF_32a_Service_Facility_NPI = new FormFieldString(620, 2068);
            public FormFieldString CF_32b_Service_Facility_UPIN_OtherID = new FormFieldString(844, 2068);

            public FormFieldString CF_33_BillingProvider_Name = new FormFieldString(1148, 1964);
            public FormFieldString CF_33_BillingProvider_Address_Line1 = new FormFieldString(1148, 1991);
            public FormFieldString CF_33_BillingProvider_Address_Line2 = new FormFieldString(1148, 2019);
            public FormFieldString CF_33_BillingProvider_City = new FormFieldString(3, 2);
            public FormFieldString CF_33_BillingProvider_State = new FormFieldString(3, 2);
            public FormFieldString CF_33_BillingProvider_Zip = new FormFieldString(3, 2);
            public FormFieldString CF_33a_BillingProvider_NPI = new FormFieldString(1161, 2068);
            public FormFieldString CF_33b_BillingProvider_UPIN_OtherID = new FormFieldString(1385, 2068);
            public FormFieldString CF_33_BillingProvider_Tel_AreaCode = new FormFieldString(1472, 1938);
            public FormFieldString CF_33_BillingProvider_Tel_Number = new FormFieldString(1543, 1938);

        }

        public class FormFieldString
        {
            #region "Constructor & Destructor"

            public FormFieldString(float PointX, float PointY)
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

            ~FormFieldString()
            {
                Dispose(false);
            }

            #endregion

            private string _FieldValue = "";
            private float _LocationX = 0;
            private float _LocationY = 0;

            public string Value
            {
                get { return _FieldValue; }
                set { _FieldValue = value; }
            }

            public System.Drawing.PointF Location
            {
                get { return new System.Drawing.PointF(_LocationX, _LocationY); }
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
}
