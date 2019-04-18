using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloCMSEDI
{
    public class ClsBL_HCFA1500PaperForm
    {
       
    } 
        public class gloHCFA1500PaperForm
        {
            #region "Constructor & Destructor"

            public gloHCFA1500PaperForm()
            {
                _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                dtSettings = GetPrinterSetting();
                if (dtSettings == null || dtSettings.Rows.Count == 0)
                {
                    dtSettings = GetDefaultPrinterSetting();
                }

                InitializeBoxes();
            }

            #region "Code added by Pankaj 23122009 for PrintForm Setup"
            // New constructor created for PrintForm 
            // Passing IsForPrint=True will call InitializeBoxesForPrintForm() method
            public gloHCFA1500PaperForm(bool IsForPrint)
            {
                _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                dtSettings = GetPrinterSetting();
                if (dtSettings == null || dtSettings.Rows.Count == 0)
                {
                    dtSettings = GetDefaultPrinterSetting();
                }
                if (IsForPrint)
                { InitializeBoxesForPrintForm(); }
                else
                { InitializeBoxes(); }
            }
            #endregion

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

            #region " Private Variables "
            private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string _DataBaseConnectionString;
            DataTable dtSettings;
            #endregion

            #region " Boxes Declarations "

            //.Insurance Header


            public FormFieldString CF_Top_InsuranceHeader; // = new FormFieldString(1042, 112);

            //.Insurance Type
            public FormFieldBoolean CF_1_Insuracne_Type_Medicare; // = new FormFieldBoolean(28, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_Medicaid; // = new FormFieldBoolean(96, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_Tricare; // = new FormFieldBoolean(156, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_Champva; // = new FormFieldBoolean(247, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_GroupHealthPlan; // = new FormFieldBoolean(314, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_FECA; // = new FormFieldBoolean(392, 99);
            public FormFieldBoolean CF_1_Insuracne_Type_Other; // = new FormFieldBoolean(447, 99);

            //.Insured's ID Number
            public FormFieldString CF_1a_InsuredsIDNumber; // = new FormFieldString(500, 99);

            //.Patient's Name
            //public FormFieldString CF_2_Patient_Name ; // = new FormFieldString(30, 300);
            public FormFieldString CF_2_Patient_Name; // = new FormFieldString(29, 128);

            //.Patient's Birth Date
            public FormFieldString CF_3_Patient_DOB_MM; // = new FormFieldString(306, 132);
            public FormFieldString CF_3_Patient_DOB_DD; // = new FormFieldString(336, 132);
            public FormFieldString CF_3_Patient_DOB_YY; // = new FormFieldString(366, 132);

            //.Patient's Sex
            public FormFieldBoolean CF_3_Patient_Sex_Male; // = new FormFieldBoolean(430, 128);
            public FormFieldBoolean CF_3_Patient_Sex_Female; // = new FormFieldBoolean(480, 128);


            //.Insured's Name
            public FormFieldString CF_4_Insureds_Name; // = new FormFieldString(500, 128);

            //.Patient's Address
            public FormFieldString CF_5_Patient_Address; // = new FormFieldString(29, 160);
            public FormFieldString CF_5_Patient_City; // = new FormFieldString(29, 190);
            public FormFieldString CF_5_Patient_State; // = new FormFieldString(265, 190);
            public FormFieldString CF_5_Patient_Zip; // = new FormFieldString(29, 225);
            public FormFieldString CF_5_Patient_Tel_AreaCode; // = new FormFieldString(140, 228);
            public FormFieldString CF_5_Patient_Tel_Number; // = new FormFieldString(170, 228);

            //.Paient Relationship to Insured
            public FormFieldBoolean CF_6_PatientRelationship_Self; // = new FormFieldBoolean(321, 163);
            public FormFieldBoolean CF_6_PatientRelationship_Spouse; // = new FormFieldBoolean(371, 163);
            public FormFieldBoolean CF_6_PatientRelationship_Child; // = new FormFieldBoolean(404, 163);
            public FormFieldBoolean CF_6_PatientRelationship_Other; // = new FormFieldBoolean(440, 163);

            //.Insured's Address
            public FormFieldString CF_7_Insureds_Address; // = new FormFieldString(500, 163);
            public FormFieldString CF_7_Insureds_City; // = new FormFieldString(500, 190);
            public FormFieldString CF_7_Insureds_State; // = new FormFieldString(725, 190);
            public FormFieldString CF_7_Insureds_Zip; // = new FormFieldString(500, 225);
            public FormFieldString CF_7_Insureds_Tel_AreaCode; // = new FormFieldString(640, 225);
            public FormFieldString CF_7_Insureds_Tel_Number; // = new FormFieldString(670, 225);

            //.Patient Status
            public FormFieldBoolean CF_8_PatientStatus_Single; // = new FormFieldBoolean(246, 200);
            public FormFieldBoolean CF_8_PatientStatus_Married; // = new FormFieldBoolean(398, 200);
            public FormFieldBoolean CF_8_PatientStatus_Other; // = new FormFieldBoolean(456, 200);
            public FormFieldBoolean CF_8_PatientStatus_Employed; // = new FormFieldBoolean(346, 230);
            public FormFieldBoolean CF_8_PatientStatus_FullTimeStudent; // = new FormFieldBoolean(398, 230);
            public FormFieldBoolean CF_8_PatientStatus_PartTimeStudent; // = new FormFieldBoolean(458, 230);

            //.Other Insured's Name
            public FormFieldString CF_9_Other_Insureds_Name; // = new FormFieldString(30, 260);
            public FormFieldString CF_9_Other_Insureds_PolicyGroupNo; // = new FormFieldString(30, 287);
            public FormFieldString CF_9_Other_Insureds_DOB_MM; // = new FormFieldString(30, 322);
            public FormFieldString CF_9_Other_Insureds_DOB_DD; // = new FormFieldString(60, 322);
            public FormFieldString CF_9_Other_Insureds_DOB_YY; // = new FormFieldString(90, 322);
            public FormFieldBoolean CF_9_Other_Insureds_Sex_Male; // = new FormFieldBoolean(177, 324);
            public FormFieldBoolean CF_9_Other_Insureds_Sex_Female; // = new FormFieldBoolean(237, 324);
            public FormFieldString CF_9_Other_Insureds_EmployerName; // = new FormFieldString(30, 355);
            public FormFieldString CF_9_Other_Insureds_InsuracnePlan; // = new FormFieldString(30, 390);

            //.Patient Condition 
            public FormFieldBoolean CF_10_PatientConditionTo_Employement_Yes; // = new FormFieldBoolean(344, 290);
            public FormFieldBoolean CF_10_PatientConditionTo_Employement_No; // = new FormFieldBoolean(394, 290);
            public FormFieldBoolean CF_10_PatientConditionTo_AutoAccident_Yes; // = new FormFieldBoolean(344, 326);
            public FormFieldBoolean CF_10_PatientConditionTo_AutoAccident_No; // = new FormFieldBoolean(394, 326);
            public FormFieldString CF_10_PatientConditionTo_AutoAccident_State; // = new FormFieldString(439, 326);
            public FormFieldBoolean CF_10_PatientConditionTo_OtherAccident_Yes; // = new FormFieldBoolean(344, 831);
            public FormFieldBoolean CF_10_PatientConditionTo_OtherAccident_No; // = new FormFieldBoolean(394, 831);
            public FormFieldString CF_10_PatientConditionTo_ResForLocaluse; // = new FormFieldString(332, 370);

            //.Insured's Information
            public FormFieldString CF_11_Insureds_PolicyGroupNo; // = new FormFieldString(1150, 630);
            public FormFieldString CF_11_Insureds_DOB_MM; // = new FormFieldString(1212, 708);
            public FormFieldString CF_11_Insureds_DOB_DD; // = new FormFieldString(1270, 708);
            public FormFieldString CF_11_Insureds_DOB_YY; // = new FormFieldString(1350, 708);
            public FormFieldBoolean CF_11_Insureds_Sex_Male; // = new FormFieldBoolean(1498, 698);
            public FormFieldBoolean CF_11_Insureds_Sex_Female; // = new FormFieldBoolean(1638, 698);
            public FormFieldString CF_11_Insureds_EmployerName; // = new FormFieldString(1150, 764);
            public FormFieldString CF_11_Insureds_InsuracnePlan; // = new FormFieldString(1150, 830);
            public FormFieldBoolean CF_11_Insureds_OtherHealthPlan_Yes; // = new FormFieldBoolean(1177, 898);
            public FormFieldBoolean CF_11_Insureds_OtherHealthPlan_No; // = new FormFieldBoolean(1278, 898);

            public FormFieldString CF_12_PatientAuthorizedPersons_Signature; // = new FormFieldString(286, 1032);
            public FormFieldString CF_12_PatientAuthorizedPersons_Signature_Date; // = new FormFieldString(869, 1035);

            public FormFieldString CF_13_InsuredsAuthorizedPersons_Signature; // = new FormFieldString(1259, 1034);

            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM; // = new FormFieldString(189, 1108);
            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD; // = new FormFieldString(248, 1108);
            public FormFieldString CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY; // = new FormFieldString(322, 1108);

            public FormFieldString CF_15_FirstDateOfSimilar_Illness_MM; // = new FormFieldString(884, 1106);
            public FormFieldString CF_15_FirstDateOfSimilar_Illness_DD; // = new FormFieldString(948, 1106);
            public FormFieldString CF_15_FirstDateOfSimilar_Illness_YY; // = new FormFieldString(1022, 1106);

            public FormFieldString CF_16_UnableToWorkFromDate_MM; // = new FormFieldString(1221, 1106);
            public FormFieldString CF_16_UnableToWorkFromDate_DD; // = new FormFieldString(1284, 1106);
            public FormFieldString CF_16_UnableToWorkFromDate_YY; // = new FormFieldString(1362, 1106);

            public FormFieldString CF_16_UnableToWorkTillDate_MM; // = new FormFieldString(1500, 1106);
            public FormFieldString CF_16_UnableToWorkTillDate_DD; // = new FormFieldString(1563, 1106);
            public FormFieldString CF_16_UnableToWorkTillDate_YY; // = new FormFieldString(1641, 1106);

            public FormFieldString CF_17_ReferringProvider_Name; // = new FormFieldString(169, 1162);
            public FormFieldString CF_17a_ReferringProvider_OtherQualifier; // = new FormFieldString(796, 1142);
            public FormFieldString CF_17a_ReferringProvider_OtherID; // = new FormFieldString(796, 1142);
            public FormFieldString CF_17b_ReferringProvider_NPI; // = new FormFieldString(796, 1173);

            public FormFieldString CF_18_HospitalizationFromDate_MM; // = new FormFieldString(1223, 1170);
            public FormFieldString CF_18_HospitalizationFromDate_DD; // = new FormFieldString(1286, 1170);
            public FormFieldString CF_18_HospitalizationFromDate_YY; // = new FormFieldString(1364, 1170);

            public FormFieldString CF_18_HospitalizationTillDate_MM; // = new FormFieldString(1501, 1171);
            public FormFieldString CF_18_HospitalizationTillDate_DD; // = new FormFieldString(1564, 1170);
            public FormFieldString CF_18_HospitalizationTillDate_YY; // = new FormFieldString(1643, 1170);

            public FormFieldString CF_19_LocalUse_Field; // = new FormFieldString(169, 1228);

            public FormFieldBoolean CF_20_OutsideLab_Yes; // = new FormFieldBoolean(1179, 1230);
            public FormFieldBoolean CF_20_OutsideLab_No; // = new FormFieldBoolean(1280, 1230);
            public FormFieldString CF_20_OutsideLab_Charges_Principal; // = new FormFieldString(1466, 1235);
            public FormFieldString CF_20_OutsideLab_Charges_Secondary; // = new FormFieldString(1571, 1235);

            public FormFieldString CF_21_Diagnosis_1_Principal; // = new FormFieldString(205, 1308);
            public FormFieldString CF_21_Diagnosis_1_Secondary; // = new FormFieldString(284, 1308);
            public FormFieldString CF_21_Diagnosis_2_Principal; // = new FormFieldString(204, 1374);
            public FormFieldString CF_21_Diagnosis_2_Secondary; // = new FormFieldString(283, 1374);
            public FormFieldString CF_21_Diagnosis_3_Principal; // = new FormFieldString(744, 1308);
            public FormFieldString CF_21_Diagnosis_3_Secondary; // = new FormFieldString(823, 1308);
            public FormFieldString CF_21_Diagnosis_4_Principal; // = new FormFieldString(744, 1374);
            public FormFieldString CF_21_Diagnosis_4_Secondary; // = new FormFieldString(823, 1374);

            public FormFieldString CF_22_MecaidResubmission_Code; // = new FormFieldString(1150, 1307);
            public FormFieldString CF_22_Original_Refrence_No; // = new FormFieldString(1393, 1307);

            public FormFieldString CF_23_PriorAuthorization_No; // = new FormFieldString(1150, 1364);

            #region " Service Line 1 "

            public bool CF_IsPresent_Line1 = false;

            //From Date.
            public FormFieldString CF_24A_L1_DOS_From_MM; // = new FormFieldString(167, 1505);
            public FormFieldString CF_24A_L1_DOS_From_DD; // = new FormFieldString(225, 1505);
            public FormFieldString CF_24A_L1_DOS_From_YY; // = new FormFieldString(283, 1505);

            //To Date
            public FormFieldString CF_24A_L1_DOS_To_MM; // = new FormFieldString(343, 1505);
            public FormFieldString CF_24A_L1_DOS_To_DD; // = new FormFieldString(401, 1505);
            public FormFieldString CF_24A_L1_DOS_To_YY; // = new FormFieldString(464, 1505);

            //Place Of Service
            public FormFieldString CF_24B_L1_POS_Code; // = new FormFieldString(525, 1505);

            //EMG - Emergency
            public FormFieldString CF_24C_L1_EMG_Code; // = new FormFieldString(584, 1504);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L1_CPT_HCPCS_Code; // = new FormFieldString(662, 1505);

            //Modifiers
            public FormFieldString CF_24D_L1_Modifier_1_Code; // = new FormFieldString(803, 1505);
            public FormFieldString CF_24D_L1_Modifier_2_Code; // = new FormFieldString(862, 1505);
            public FormFieldString CF_24D_L1_Modifier_3_Code; // = new FormFieldString(922, 1505);
            public FormFieldString CF_24D_L1_Modifier_4_Code; // = new FormFieldString(983, 1505);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L1_Diagnosis_Pointers; // = new FormFieldString(1047, 1505);

            //Charges
            public FormFieldString CF_24F_L1_Charges_Principal; // = new FormFieldString(1161, 1505);
            public FormFieldString CF_24F_L1_Charges_Secondary; // = new FormFieldString(1269, 1505);

            //Days or Units
            public FormFieldString CF_24G_L1_Days_Units; // = new FormFieldString(1328, 1505);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L1_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L1_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1505);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L1_RenderingProvider_NPI; // = new FormFieldString(1507, 1505);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L1_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L1_RenderingProvider_OtherQualifiervalue;

            //Line Note
            public FormFieldString CF_24A_L1_Note; // = new FormFieldString(169, 1472);

            #endregion

            #region " Service Line 2 "

            public bool CF_IsPresent_Line2 = false;

            //From Date.
            public FormFieldString CF_24A_L2_DOS_From_MM; // = new FormFieldString(167, 1573);
            public FormFieldString CF_24A_L2_DOS_From_DD; // = new FormFieldString(225, 1573);
            public FormFieldString CF_24A_L2_DOS_From_YY; // = new FormFieldString(283, 1573);

            //To Date
            public FormFieldString CF_24A_L2_DOS_To_MM; // = new FormFieldString(343, 1573);
            public FormFieldString CF_24A_L2_DOS_To_DD; // = new FormFieldString(401, 1573);
            public FormFieldString CF_24A_L2_DOS_To_YY; // = new FormFieldString(464, 1573);

            //Place Of Service
            public FormFieldString CF_24B_L2_POS_Code; // = new FormFieldString(525, 1573);

            //EMG - Emergency
            public FormFieldString CF_24C_L2_EMG_Code; // = new FormFieldString(584, 1572);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L2_CPT_HCPCS_Code; // = new FormFieldString(662, 1573);

            //Modifiers
            public FormFieldString CF_24D_L2_Modifier_1_Code; // = new FormFieldString(803, 1573);
            public FormFieldString CF_24D_L2_Modifier_2_Code; // = new FormFieldString(862, 1573);
            public FormFieldString CF_24D_L2_Modifier_3_Code; // = new FormFieldString(922, 1573);
            public FormFieldString CF_24D_L2_Modifier_4_Code; // = new FormFieldString(983, 1573);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L2_Diagnosis_Pointers; // = new FormFieldString(1047, 1573);

            //Charges
            public FormFieldString CF_24F_L2_Charges_Principal; // = new FormFieldString(1161, 1573);
            public FormFieldString CF_24F_L2_Charges_Secondary; // = new FormFieldString(1269, 1573);

            //Days or Units
            public FormFieldString CF_24G_L2_Days_Units; // = new FormFieldString(1328, 1573);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L2_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L2_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1573);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L2_RenderingProvider_NPI; // = new FormFieldString(1507, 1573);

            //Line Note
            public FormFieldString CF_24A_L2_Note; // = new FormFieldString(167, 1537);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L2_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L2_RenderingProvider_OtherQualifiervalue;

            #endregion

            #region " Service Line 3 "

            public bool CF_IsPresent_Line3 = false;

            //From Date.
            public FormFieldString CF_24A_L3_DOS_From_MM; // = new FormFieldString(167, 1638);
            public FormFieldString CF_24A_L3_DOS_From_DD; // = new FormFieldString(225, 1638);
            public FormFieldString CF_24A_L3_DOS_From_YY; // = new FormFieldString(283, 1638);

            //To Date
            public FormFieldString CF_24A_L3_DOS_To_MM; // = new FormFieldString(343, 1638);
            public FormFieldString CF_24A_L3_DOS_To_DD; // = new FormFieldString(401, 1638);
            public FormFieldString CF_24A_L3_DOS_To_YY; // = new FormFieldString(464, 1638);

            //Place Of Service
            public FormFieldString CF_24B_L3_POS_Code; // = new FormFieldString(525, 1638);

            //EMG - Emergency
            public FormFieldString CF_24C_L3_EMG_Code; // = new FormFieldString(584, 1637);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L3_CPT_HCPCS_Code; // = new FormFieldString(662, 1638);

            //Modifiers
            public FormFieldString CF_24D_L3_Modifier_1_Code; // = new FormFieldString(803, 1638);
            public FormFieldString CF_24D_L3_Modifier_2_Code; // = new FormFieldString(862, 1638);
            public FormFieldString CF_24D_L3_Modifier_3_Code; // = new FormFieldString(922, 1638);
            public FormFieldString CF_24D_L3_Modifier_4_Code; // = new FormFieldString(983, 1638);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L3_Diagnosis_Pointers; // = new FormFieldString(1047, 1638);

            //Charges
            public FormFieldString CF_24F_L3_Charges_Principal; // = new FormFieldString(1161, 1638);
            public FormFieldString CF_24F_L3_Charges_Secondary; // = new FormFieldString(1269, 1638);

            //Days or Units
            public FormFieldString CF_24G_L3_Days_Units; // = new FormFieldString(1328, 1638);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L3_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L3_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1638);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L3_RenderingProvider_NPI; // = new FormFieldString(1507, 1638);

            //Line Note
            public FormFieldString CF_24A_L3_Note; // = new FormFieldString(167, 1604);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L3_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L3_RenderingProvider_OtherQualifiervalue;

            #endregion

            #region " Service Line 4 "

            public bool CF_IsPresent_Line4 = false;

            //From Date.
            public FormFieldString CF_24A_L4_DOS_From_MM; // = new FormFieldString(167, 1704);
            public FormFieldString CF_24A_L4_DOS_From_DD; // = new FormFieldString(225, 1704);
            public FormFieldString CF_24A_L4_DOS_From_YY; // = new FormFieldString(283, 1704);

            //To Date
            public FormFieldString CF_24A_L4_DOS_To_MM; // = new FormFieldString(343, 1704);
            public FormFieldString CF_24A_L4_DOS_To_DD; // = new FormFieldString(401, 1704);
            public FormFieldString CF_24A_L4_DOS_To_YY; // = new FormFieldString(464, 1704);

            //Place Of Service
            public FormFieldString CF_24B_L4_POS_Code; // = new FormFieldString(525, 1704);

            //EMG - Emergency
            public FormFieldString CF_24C_L4_EMG_Code; // = new FormFieldString(584, 1703);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L4_CPT_HCPCS_Code; // = new FormFieldString(662, 1704);

            //Modifiers
            public FormFieldString CF_24D_L4_Modifier_1_Code; // = new FormFieldString(803, 1704);
            public FormFieldString CF_24D_L4_Modifier_2_Code; // = new FormFieldString(862, 1704);
            public FormFieldString CF_24D_L4_Modifier_3_Code; // = new FormFieldString(922, 1704);
            public FormFieldString CF_24D_L4_Modifier_4_Code; // = new FormFieldString(983, 1704);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L4_Diagnosis_Pointers; // = new FormFieldString(1047, 1704);

            //Charges
            public FormFieldString CF_24F_L4_Charges_Principal; // = new FormFieldString(1161, 1704);
            public FormFieldString CF_24F_L4_Charges_Secondary; // = new FormFieldString(1269, 1704);

            //Days or Units
            public FormFieldString CF_24G_L4_Days_Units; // = new FormFieldString(1328, 1704);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L4_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L4_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1704);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L4_RenderingProvider_NPI; // = new FormFieldString(1507, 1704);

            //Line Note
            public FormFieldString CF_24A_L4_Note; // = new FormFieldString(167, 1674);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L4_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L4_RenderingProvider_OtherQualifiervalue;

            #endregion

            #region " Service Line 5 "

            public bool CF_IsPresent_Line5 = false;

            //From Date.
            public FormFieldString CF_24A_L5_DOS_From_MM; // = new FormFieldString(167, 1773);
            public FormFieldString CF_24A_L5_DOS_From_DD; // = new FormFieldString(225, 1773);
            public FormFieldString CF_24A_L5_DOS_From_YY; // = new FormFieldString(283, 1773);

            //To Date
            public FormFieldString CF_24A_L5_DOS_To_MM; // = new FormFieldString(343, 1773);
            public FormFieldString CF_24A_L5_DOS_To_DD; // = new FormFieldString(401, 1773);
            public FormFieldString CF_24A_L5_DOS_To_YY; // = new FormFieldString(464, 1773);

            //Place Of Service
            public FormFieldString CF_24B_L5_POS_Code; // = new FormFieldString(525, 1773);

            //EMG - Emergency
            public FormFieldString CF_24C_L5_EMG_Code; // = new FormFieldString(584, 1772);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L5_CPT_HCPCS_Code; // = new FormFieldString(662, 1773);

            //Modifiers
            public FormFieldString CF_24D_L5_Modifier_1_Code; // = new FormFieldString(803, 1773);
            public FormFieldString CF_24D_L5_Modifier_2_Code; // = new FormFieldString(862, 1773);
            public FormFieldString CF_24D_L5_Modifier_3_Code; // = new FormFieldString(922, 1773);
            public FormFieldString CF_24D_L5_Modifier_4_Code; // = new FormFieldString(983, 1773);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L5_Diagnosis_Pointers; // = new FormFieldString(1047, 1773);

            //Charges
            public FormFieldString CF_24F_L5_Charges_Principal; // = new FormFieldString(1161, 1773);
            public FormFieldString CF_24F_L5_Charges_Secondary; // = new FormFieldString(1269, 1773);

            //Days or Units
            public FormFieldString CF_24G_L5_Days_Units; // = new FormFieldString(1328, 1773);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L5_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L5_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1773);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L5_RenderingProvider_NPI; // = new FormFieldString(1507, 1773);

            //Line Note
            public FormFieldString CF_24A_L5_Note; // = new FormFieldString(167, 1739);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L5_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L5_RenderingProvider_OtherQualifiervalue;

            #endregion

            #region " Service Line 6 "

            public bool CF_IsPresent_Line6 = false;

            //From Date.
            public FormFieldString CF_24A_L6_DOS_From_MM; // = new FormFieldString(167, 1838);
            public FormFieldString CF_24A_L6_DOS_From_DD; // = new FormFieldString(225, 1838);
            public FormFieldString CF_24A_L6_DOS_From_YY; // = new FormFieldString(283, 1838);

            //To Date
            public FormFieldString CF_24A_L6_DOS_To_MM; // = new FormFieldString(343, 1838);
            public FormFieldString CF_24A_L6_DOS_To_DD; // = new FormFieldString(401, 1838);
            public FormFieldString CF_24A_L6_DOS_To_YY; // = new FormFieldString(464, 1838);

            //Place Of Service
            public FormFieldString CF_24B_L6_POS_Code; // = new FormFieldString(525, 1838);

            //EMG - Emergency
            public FormFieldString CF_24C_L6_EMG_Code; // = new FormFieldString(584, 1837);

            //CPT/HCPCS 
            public FormFieldString CF_24D_L6_CPT_HCPCS_Code; // = new FormFieldString(662, 1838);

            //Modifiers
            public FormFieldString CF_24D_L6_Modifier_1_Code; // = new FormFieldString(803, 1838);
            public FormFieldString CF_24D_L6_Modifier_2_Code; // = new FormFieldString(862, 1838);
            public FormFieldString CF_24D_L6_Modifier_3_Code; // = new FormFieldString(922, 1838);
            public FormFieldString CF_24D_L6_Modifier_4_Code; // = new FormFieldString(983, 1838);

            //Diagnosis Pointers 
            public FormFieldString CF_24E_L6_Diagnosis_Pointers; // = new FormFieldString(1047, 1838);

            //Charges
            public FormFieldString CF_24F_L6_Charges_Principal; // = new FormFieldString(1161, 1838);
            public FormFieldString CF_24F_L6_Charges_Secondary; // = new FormFieldString(1269, 1838);

            //Days or Units
            public FormFieldString CF_24G_L6_Days_Units; // = new FormFieldString(1328, 1838);

            //EPSDT Family Plan
            public FormFieldString CF_24H_L6_EPSDT_FamilyPlan_Shaded;
            public FormFieldString CF_24H_L6_EPSDT_FamilyPlan; // = new FormFieldString(1403, 1838);

            //Rendering Provider ID (NPI)
            public FormFieldString CF_24J_L6_RenderingProvider_NPI; // = new FormFieldString(1507, 1838);

            //Line Note
            public FormFieldString CF_24A_L6_Note; // = new FormFieldString(167, 1802);

            //Rendering Provider Other ID (Qualifier)
            public FormFieldString CF_24J_L6_RenderingProvider_OtherQualifier;

            //Rendering Provider ID (QualifierValue)
            public FormFieldString CF_24J_L6_RenderingProvider_OtherQualifiervalue;

            #endregion

            public FormFieldString CF_25_FederalTax_ID_No; // = new FormFieldString(167, 1895);
            public FormFieldBoolean CF_25_FederalTaxID_Qualifier_SSN; // = new FormFieldBoolean(482, 1895);
            public FormFieldBoolean CF_25_FederalTaxID_Qualifier_EIN; // = new FormFieldBoolean(522, 1895);

            public FormFieldString CF_26_PatientAccount_No; // = new FormFieldString(615, 1895);

            public FormFieldBoolean CF_27_AcceptAssignment_YES; // = new FormFieldBoolean(901, 1895);
            public FormFieldBoolean CF_27_AcceptAssignment_NO; // = new FormFieldBoolean(1001, 1895);

            public FormFieldString CF_28_TotalCharge_Principal; // = new FormFieldString(1171, 1901);
            public FormFieldString CF_28_TotalCharge_Secondary; // = new FormFieldString(1314, 1901);

            public FormFieldString CF_29_AmountPaid_Principal; // = new FormFieldString(1392, 1901);
            public FormFieldString CF_29_AmountPaid_Secondary; // = new FormFieldString(1511, 1901);

            public FormFieldString CF_30_BalanceDue_Principal; // = new FormFieldString(1573, 1901);
            public FormFieldString CF_30_BalanceDue_Secondary; // = new FormFieldString(1692, 1901);

            public FormFieldString CF_31_Physician_Supplier_Signature; // = new FormFieldString(167, 2054);
            public FormFieldString CF_31_Physician_Supplier_Signature_Date; // = new FormFieldString(450, 2050); //(519, 2078);
            public FormFieldString CF_31_Physician_Supplier_QualifierValue;

            public FormFieldString CF_32_Service_Facility_Name; // = new FormFieldString(620, 1964);
            public FormFieldString CF_32_Service_Facility_Address_Line1; // = new FormFieldString(620, 1991);
            public FormFieldString CF_32_Service_Facility_Address_Line2; // = new FormFieldString(620, 2019);
            public FormFieldString CF_32_Service_Facility_City; // = new FormFieldString(3, 2);
            public FormFieldString CF_32_Service_Facility_State; // = new FormFieldString(3, 2);
            public FormFieldString CF_32_Service_Facility_Zip; // = new FormFieldString(3, 2);
            public FormFieldString CF_32a_Service_Facility_NPI; // = new FormFieldString(620, 2068);
            public FormFieldString CF_32b_Service_Facility_UPIN_OtherID; // = new FormFieldString(844, 2068);

            public FormFieldString CF_33_BillingProvider_Name; // = new FormFieldString(1148, 1964);
            public FormFieldString CF_33_BillingProvider_Address_Line1; // = new FormFieldString(1148, 1991);
            public FormFieldString CF_33_BillingProvider_Address_Line2; // = new FormFieldString(1148, 2019);
            public FormFieldString CF_33_BillingProvider_City; // = new FormFieldString(3, 2);
            public FormFieldString CF_33_BillingProvider_State; // = new FormFieldString(3, 2);
            public FormFieldString CF_33_BillingProvider_Zip; // = new FormFieldString(3, 2);
            public FormFieldString CF_33a_BillingProvider_NPI; // = new FormFieldString(1161, 2068);
            public FormFieldString CF_33b_BillingProvider_UPIN_OtherID; // = new FormFieldString(1385, 2068);
            public FormFieldString CF_33_BillingProvider_Tel_AreaCode; // = new FormFieldString(1472, 1938);
            public FormFieldString CF_33_BillingProvider_Tel_Number; // = new FormFieldString(1543, 1938); 
            #endregion

            #region " Private Method "
            private void InitializeBoxes()
            {
                Int32 X = 0;
                Int32 Y = 0;


                //.Insurance Header
                GetCoordinates("CF_Top_InsuranceHeader", out X, out Y);
                CF_Top_InsuranceHeader = new FormFieldString(X, Y);

                //.Insurance Type
                GetCoordinates("CF_1_Insuracne_Type_Medicare", out X, out Y);
                CF_1_Insuracne_Type_Medicare = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_Medicaid", out X, out Y);
                CF_1_Insuracne_Type_Medicaid = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_Tricare", out X, out Y);
                CF_1_Insuracne_Type_Tricare = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_Champva", out X, out Y);
                CF_1_Insuracne_Type_Champva = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_GroupHealthPlan", out X, out Y);
                CF_1_Insuracne_Type_GroupHealthPlan = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_FECA", out X, out Y);
                CF_1_Insuracne_Type_FECA = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_1_Insuracne_Type_Other", out X, out Y);
                CF_1_Insuracne_Type_Other = new FormFieldBoolean(X, Y);

                //.Insured's ID Number
                GetCoordinates("CF_1a_InsuredsIDNumber", out X, out Y);
                CF_1a_InsuredsIDNumber = new FormFieldString(X, Y);

                //.Patient's Name
                //public FormFieldString CF_2_Patient_Name = new FormFieldString(30, 300);
                GetCoordinates("CF_2_Patient_Name", out X, out Y);
                CF_2_Patient_Name = new FormFieldString(X, Y);

                //.Patient's Birth Date
                GetCoordinates("CF_3_Patient_DOB_MM", out X, out Y);
                CF_3_Patient_DOB_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_3_Patient_DOB_DD", out X, out Y);
                CF_3_Patient_DOB_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_3_Patient_DOB_YY", out X, out Y);
                CF_3_Patient_DOB_YY = new FormFieldString(X, Y);

                //.Patient's Sex
                GetCoordinates("CF_3_Patient_Sex_Male", out X, out Y);
                CF_3_Patient_Sex_Male = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_3_Patient_Sex_Female", out X, out Y);
                CF_3_Patient_Sex_Female = new FormFieldBoolean(X, Y);


                //.Insured's Name
                GetCoordinates("CF_4_Insureds_Name", out X, out Y);
                CF_4_Insureds_Name = new FormFieldString(X, Y);

                //.Patient's Address
                GetCoordinates("CF_5_Patient_Address", out X, out Y);
                CF_5_Patient_Address = new FormFieldString(X, Y);
                GetCoordinates("CF_5_Patient_City", out X, out Y);
                CF_5_Patient_City = new FormFieldString(X, Y);
                GetCoordinates("CF_5_Patient_State", out X, out Y);
                CF_5_Patient_State = new FormFieldString(X, Y);
                GetCoordinates("CF_5_Patient_Zip", out X, out Y);
                CF_5_Patient_Zip = new FormFieldString(X, Y);
                GetCoordinates("CF_5_Patient_Tel_AreaCode", out X, out Y);
                CF_5_Patient_Tel_AreaCode = new FormFieldString(X, Y);
                GetCoordinates("CF_5_Patient_Tel_Number", out X, out Y);
                CF_5_Patient_Tel_Number = new FormFieldString(X, Y);

                //.Paient Relationship to Insured
                GetCoordinates("CF_6_PatientRelationship_Self", out X, out Y);
                CF_6_PatientRelationship_Self = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_6_PatientRelationship_Spouse", out X, out Y);
                CF_6_PatientRelationship_Spouse = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_6_PatientRelationship_Child", out X, out Y);
                CF_6_PatientRelationship_Child = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_6_PatientRelationship_Other", out X, out Y);
                CF_6_PatientRelationship_Other = new FormFieldBoolean(X, Y);

                //.Insured's Address
                GetCoordinates("CF_7_Insureds_Address", out X, out Y);
                CF_7_Insureds_Address = new FormFieldString(X, Y);
                GetCoordinates("CF_7_Insureds_City", out X, out Y);
                CF_7_Insureds_City = new FormFieldString(X, Y);
                GetCoordinates("CF_7_Insureds_State", out X, out Y);
                CF_7_Insureds_State = new FormFieldString(X, Y);
                GetCoordinates("CF_7_Insureds_Zip", out X, out Y);
                CF_7_Insureds_Zip = new FormFieldString(X, Y);
                GetCoordinates("CF_7_Insureds_Tel_AreaCode", out X, out Y);
                CF_7_Insureds_Tel_AreaCode = new FormFieldString(X, Y);
                GetCoordinates("CF_7_Insureds_Tel_Number", out X, out Y);
                CF_7_Insureds_Tel_Number = new FormFieldString(X, Y);

                //.Patient Status
                GetCoordinates("CF_8_PatientStatus_Single", out X, out Y);
                CF_8_PatientStatus_Single = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_8_PatientStatus_Married", out X, out Y);
                CF_8_PatientStatus_Married = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_8_PatientStatus_Other", out X, out Y);
                CF_8_PatientStatus_Other = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_8_PatientStatus_Employed", out X, out Y);
                CF_8_PatientStatus_Employed = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_8_PatientStatus_FullTimeStudent", out X, out Y);
                CF_8_PatientStatus_FullTimeStudent = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_8_PatientStatus_PartTimeStudent", out X, out Y);
                CF_8_PatientStatus_PartTimeStudent = new FormFieldBoolean(X, Y);

                //.Other Insured's Name
                GetCoordinates("CF_9_Other_Insureds_Name", out X, out Y);
                CF_9_Other_Insureds_Name = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_PolicyGroupNo", out X, out Y);
                CF_9_Other_Insureds_PolicyGroupNo = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_DOB_MM", out X, out Y);
                CF_9_Other_Insureds_DOB_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_DOB_DD", out X, out Y);
                CF_9_Other_Insureds_DOB_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_DOB_YY", out X, out Y);
                CF_9_Other_Insureds_DOB_YY = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_Sex_Male", out X, out Y);
                CF_9_Other_Insureds_Sex_Male = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_9_Other_Insureds_Sex_Female", out X, out Y);
                CF_9_Other_Insureds_Sex_Female = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_9_Other_Insureds_EmployerName", out X, out Y);
                CF_9_Other_Insureds_EmployerName = new FormFieldString(X, Y);
                GetCoordinates("CF_9_Other_Insureds_InsuracnePlan", out X, out Y);
                CF_9_Other_Insureds_InsuracnePlan = new FormFieldString(X, Y);

                //.Patient Condition 
                GetCoordinates("CF_10_PatientConditionTo_Employement_Yes", out X, out Y);
                CF_10_PatientConditionTo_Employement_Yes = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_Employement_No", out X, out Y);
                CF_10_PatientConditionTo_Employement_No = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_AutoAccident_Yes", out X, out Y);
                CF_10_PatientConditionTo_AutoAccident_Yes = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_AutoAccident_No", out X, out Y);
                CF_10_PatientConditionTo_AutoAccident_No = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_AutoAccident_State", out X, out Y);
                CF_10_PatientConditionTo_AutoAccident_State = new FormFieldString(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_OtherAccident_Yes", out X, out Y);
                CF_10_PatientConditionTo_OtherAccident_Yes = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_OtherAccident_No", out X, out Y);
                CF_10_PatientConditionTo_OtherAccident_No = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_10_PatientConditionTo_ResForLocaluse", out X, out Y);
                CF_10_PatientConditionTo_ResForLocaluse = new FormFieldString(X, Y);

                //.Insured's Information
                GetCoordinates("CF_11_Insureds_PolicyGroupNo", out X, out Y);
                CF_11_Insureds_PolicyGroupNo = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_DOB_MM", out X, out Y);
                CF_11_Insureds_DOB_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_DOB_DD", out X, out Y);
                CF_11_Insureds_DOB_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_DOB_YY", out X, out Y);
                CF_11_Insureds_DOB_YY = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_Sex_Male", out X, out Y);
                CF_11_Insureds_Sex_Male = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_11_Insureds_Sex_Female", out X, out Y);
                CF_11_Insureds_Sex_Female = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_11_Insureds_EmployerName", out X, out Y);
                CF_11_Insureds_EmployerName = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_InsuracnePlan", out X, out Y);
                CF_11_Insureds_InsuracnePlan = new FormFieldString(X, Y);
                GetCoordinates("CF_11_Insureds_OtherHealthPlan_Yes", out X, out Y);
                CF_11_Insureds_OtherHealthPlan_Yes = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_11_Insureds_OtherHealthPlan_No", out X, out Y);
                CF_11_Insureds_OtherHealthPlan_No = new FormFieldBoolean(X, Y);

                GetCoordinates("CF_12_PatientAuthorizedPersons_Signature", out X, out Y);
                CF_12_PatientAuthorizedPersons_Signature = new FormFieldString(X, Y);
                GetCoordinates("CF_12_PatientAuthorizedPersons_Signature_Date", out X, out Y);
                CF_12_PatientAuthorizedPersons_Signature_Date = new FormFieldString(X, Y);

                GetCoordinates("CF_13_InsuredsAuthorizedPersons_Signature", out X, out Y);
                CF_13_InsuredsAuthorizedPersons_Signature = new FormFieldString(X, Y);

                GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM", out X, out Y);
                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD", out X, out Y);
                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY", out X, out Y);
                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_15_FirstDateOfSimilar_Illness_MM", out X, out Y);
                CF_15_FirstDateOfSimilar_Illness_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_15_FirstDateOfSimilar_Illness_DD", out X, out Y);
                CF_15_FirstDateOfSimilar_Illness_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_15_FirstDateOfSimilar_Illness_YY", out X, out Y);
                CF_15_FirstDateOfSimilar_Illness_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_16_UnableToWorkFromDate_MM", out X, out Y);
                CF_16_UnableToWorkFromDate_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_16_UnableToWorkFromDate_DD", out X, out Y);
                CF_16_UnableToWorkFromDate_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_16_UnableToWorkFromDate_YY", out X, out Y);
                CF_16_UnableToWorkFromDate_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_16_UnableToWorkTillDate_MM", out X, out Y);
                CF_16_UnableToWorkTillDate_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_16_UnableToWorkTillDate_DD", out X, out Y);
                CF_16_UnableToWorkTillDate_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_16_UnableToWorkTillDate_YY", out X, out Y);
                CF_16_UnableToWorkTillDate_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_17_ReferringProvider_Name", out X, out Y);
                CF_17_ReferringProvider_Name = new FormFieldString(X, Y);

                GetCoordinates("CF_17a_ReferringProvider_OtherQualifier", out X, out Y);
                CF_17a_ReferringProvider_OtherQualifier = new FormFieldString(X, Y);

                GetCoordinates("CF_17a_ReferringProvider_OtherID", out X, out Y);
                CF_17a_ReferringProvider_OtherID = new FormFieldString(X, Y);

                GetCoordinates("CF_17b_ReferringProvider_NPI", out X, out Y);
                CF_17b_ReferringProvider_NPI = new FormFieldString(X, Y);

                GetCoordinates("CF_18_HospitalizationFromDate_MM", out X, out Y);
                CF_18_HospitalizationFromDate_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_18_HospitalizationFromDate_DD", out X, out Y);
                CF_18_HospitalizationFromDate_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_18_HospitalizationFromDate_YY", out X, out Y);
                CF_18_HospitalizationFromDate_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_18_HospitalizationTillDate_MM", out X, out Y);
                CF_18_HospitalizationTillDate_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_18_HospitalizationTillDate_DD", out X, out Y);
                CF_18_HospitalizationTillDate_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_18_HospitalizationTillDate_YY", out X, out Y);
                CF_18_HospitalizationTillDate_YY = new FormFieldString(X, Y);

                GetCoordinates("CF_19_LocalUse_Field", out X, out Y);
                CF_19_LocalUse_Field = new FormFieldString(X, Y);

                GetCoordinates("CF_20_OutsideLab_Yes", out X, out Y);
                CF_20_OutsideLab_Yes = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_20_OutsideLab_No", out X, out Y);
                CF_20_OutsideLab_No = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_20_OutsideLab_Charges_Principal", out X, out Y);
                CF_20_OutsideLab_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_20_OutsideLab_Charges_Secondary", out X, out Y);
                CF_20_OutsideLab_Charges_Secondary = new FormFieldString(X, Y);

                GetCoordinates("CF_21_Diagnosis_1_Principal", out X, out Y);
                CF_21_Diagnosis_1_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_1_Secondary", out X, out Y);
                CF_21_Diagnosis_1_Secondary = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_2_Principal", out X, out Y);
                CF_21_Diagnosis_2_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_2_Secondary", out X, out Y);
                CF_21_Diagnosis_2_Secondary = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_3_Principal", out X, out Y);
                CF_21_Diagnosis_3_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_3_Secondary", out X, out Y);
                CF_21_Diagnosis_3_Secondary = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_4_Principal", out X, out Y);
                CF_21_Diagnosis_4_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_21_Diagnosis_4_Secondary", out X, out Y);
                CF_21_Diagnosis_4_Secondary = new FormFieldString(X, Y);

                GetCoordinates("CF_22_MecaidResubmission_Code", out X, out Y);
                CF_22_MecaidResubmission_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_22_Original_Refrence_No", out X, out Y);
                CF_22_Original_Refrence_No = new FormFieldString(X, Y);

                GetCoordinates("CF_23_PriorAuthorization_No", out X, out Y);
                CF_23_PriorAuthorization_No = new FormFieldString(X, Y);

                #region " Service Line 1 "

             //   bool CF_IsPresent_Line1 = false;

                //From Date.
                GetCoordinates("CF_24A_L1_DOS_From_MM", out X, out Y);
                CF_24A_L1_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L1_DOS_From_DD", out X, out Y);
                CF_24A_L1_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L1_DOS_From_YY", out X, out Y);
                CF_24A_L1_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L1_DOS_To_MM", out X, out Y);
                CF_24A_L1_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L1_DOS_To_DD", out X, out Y);
                CF_24A_L1_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L1_DOS_To_YY", out X, out Y);
                CF_24A_L1_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L1_POS_Code", out X, out Y);
                CF_24B_L1_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L1_EMG_Code", out X, out Y);
                CF_24C_L1_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L1_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L1_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L1_Modifier_1_Code", out X, out Y);
                CF_24D_L1_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L1_Modifier_2_Code", out X, out Y);
                CF_24D_L1_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L1_Modifier_3_Code", out X, out Y);
                CF_24D_L1_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L1_Modifier_4_Code", out X, out Y);
                CF_24D_L1_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L1_Diagnosis_Pointers", out X, out Y);
                CF_24E_L1_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L1_Charges_Principal", out X, out Y);
                CF_24F_L1_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L1_Charges_Secondary", out X, out Y);
                CF_24F_L1_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L1_Days_Units", out X, out Y);
                CF_24G_L1_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan
                GetCoordinates("CF_24H_L1_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L1_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);

                GetCoordinates("CF_24H_L1_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L1_EPSDT_FamilyPlan = new FormFieldString(X, Y);

                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L1_RenderingProvider_NPI", out X, out Y);
                CF_24J_L1_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L1_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L1_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L1_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L1_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);

                //Line Note
                GetCoordinates("CF_24A_L1_Note", out X, out Y);
                CF_24A_L1_Note = new FormFieldString(X, Y);

                #endregion

                #region " Service Line 2 "

            //    bool CF_IsPresent_Line2 = false;

                //From Date.
                GetCoordinates("CF_24A_L2_DOS_From_MM", out X, out Y);
                CF_24A_L2_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L2_DOS_From_DD", out X, out Y);
                CF_24A_L2_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L2_DOS_From_YY", out X, out Y);
                CF_24A_L2_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L2_DOS_To_MM", out X, out Y);
                CF_24A_L2_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L2_DOS_To_DD", out X, out Y);
                CF_24A_L2_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L2_DOS_To_YY", out X, out Y);
                CF_24A_L2_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L2_POS_Code", out X, out Y);
                CF_24B_L2_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L2_EMG_Code", out X, out Y);
                CF_24C_L2_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L2_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L2_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L2_Modifier_1_Code", out X, out Y);
                CF_24D_L2_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L2_Modifier_2_Code", out X, out Y);
                CF_24D_L2_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L2_Modifier_3_Code", out X, out Y);
                CF_24D_L2_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L2_Modifier_4_Code", out X, out Y);
                CF_24D_L2_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L2_Diagnosis_Pointers", out X, out Y);
                CF_24E_L2_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L2_Charges_Principal", out X, out Y);
                CF_24F_L2_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L2_Charges_Secondary", out X, out Y);
                CF_24F_L2_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L2_Days_Units", out X, out Y);
                CF_24G_L2_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan

                GetCoordinates("CF_24H_L2_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L2_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);

                GetCoordinates("CF_24H_L2_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L2_EPSDT_FamilyPlan = new FormFieldString(X, Y);
                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L2_RenderingProvider_NPI", out X, out Y);
                CF_24J_L2_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Line Note
                GetCoordinates("CF_24A_L2_Note", out X, out Y);
                CF_24A_L2_Note = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L2_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L2_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L2_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L2_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);

                #endregion

                #region " Service Line 3 "

            //    bool CF_IsPresent_Line3 = false;

                //From Date.
                GetCoordinates("CF_24A_L3_DOS_From_MM", out X, out Y);
                CF_24A_L3_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L3_DOS_From_DD", out X, out Y);
                CF_24A_L3_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L3_DOS_From_YY", out X, out Y);
                CF_24A_L3_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L3_DOS_To_MM", out X, out Y);
                CF_24A_L3_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L3_DOS_To_DD", out X, out Y);
                CF_24A_L3_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L3_DOS_To_YY", out X, out Y);
                CF_24A_L3_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L3_POS_Code", out X, out Y);
                CF_24B_L3_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L3_EMG_Code", out X, out Y);
                CF_24C_L3_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L3_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L3_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L3_Modifier_1_Code", out X, out Y);
                CF_24D_L3_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L3_Modifier_2_Code", out X, out Y);
                CF_24D_L3_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L3_Modifier_3_Code", out X, out Y);
                CF_24D_L3_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L3_Modifier_4_Code", out X, out Y);
                CF_24D_L3_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L3_Diagnosis_Pointers", out X, out Y);
                CF_24E_L3_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L3_Charges_Principal", out X, out Y);
                CF_24F_L3_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L3_Charges_Secondary", out X, out Y);
                CF_24F_L3_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L3_Days_Units", out X, out Y);
                CF_24G_L3_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan
                GetCoordinates("CF_24H_L3_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L3_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);

                GetCoordinates("CF_24H_L3_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L3_EPSDT_FamilyPlan = new FormFieldString(X, Y);

                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L3_RenderingProvider_NPI", out X, out Y);
                CF_24J_L3_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Line Note
                GetCoordinates("CF_24A_L3_Note", out X, out Y);
                CF_24A_L3_Note = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L3_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L3_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L3_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L3_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);

                #endregion

                #region " Service Line 4 "

         //       bool CF_IsPresent_Line4 = false;

                //From Date.
                GetCoordinates("CF_24A_L4_DOS_From_MM", out X, out Y);
                CF_24A_L4_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L4_DOS_From_DD", out X, out Y);
                CF_24A_L4_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L4_DOS_From_YY", out X, out Y);
                CF_24A_L4_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L4_DOS_To_MM", out X, out Y);
                CF_24A_L4_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L4_DOS_To_DD", out X, out Y);
                CF_24A_L4_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L4_DOS_To_YY", out X, out Y);
                CF_24A_L4_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L4_POS_Code", out X, out Y);
                CF_24B_L4_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L4_EMG_Code", out X, out Y);
                CF_24C_L4_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L4_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L4_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L4_Modifier_1_Code", out X, out Y);
                CF_24D_L4_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L4_Modifier_2_Code", out X, out Y);
                CF_24D_L4_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L4_Modifier_3_Code", out X, out Y);
                CF_24D_L4_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L4_Modifier_4_Code", out X, out Y);
                CF_24D_L4_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L4_Diagnosis_Pointers", out X, out Y);
                CF_24E_L4_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L4_Charges_Principal", out X, out Y);
                CF_24F_L4_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L4_Charges_Secondary", out X, out Y);
                CF_24F_L4_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L4_Days_Units", out X, out Y);
                CF_24G_L4_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan
                GetCoordinates("CF_24H_L4_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L4_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);
                GetCoordinates("CF_24H_L4_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L4_EPSDT_FamilyPlan = new FormFieldString(X, Y);

                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L4_RenderingProvider_NPI", out X, out Y);
                CF_24J_L4_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Line Note
                GetCoordinates("CF_24A_L4_Note", out X, out Y);
                CF_24A_L4_Note = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L4_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L4_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L4_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L4_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);

                #endregion

                #region " Service Line 5 "

              //  bool CF_IsPresent_Line5 = false;

                //From Date.
                GetCoordinates("CF_24A_L5_DOS_From_MM", out X, out Y);
                CF_24A_L5_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L5_DOS_From_DD", out X, out Y);
                CF_24A_L5_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L5_DOS_From_YY", out X, out Y);
                CF_24A_L5_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L5_DOS_To_MM", out X, out Y);
                CF_24A_L5_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L5_DOS_To_DD", out X, out Y);
                CF_24A_L5_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L5_DOS_To_YY", out X, out Y);
                CF_24A_L5_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L5_POS_Code", out X, out Y);
                CF_24B_L5_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L5_EMG_Code", out X, out Y);
                CF_24C_L5_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L5_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L5_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L5_Modifier_1_Code", out X, out Y);
                CF_24D_L5_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L5_Modifier_2_Code", out X, out Y);
                CF_24D_L5_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L5_Modifier_3_Code", out X, out Y);
                CF_24D_L5_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L5_Modifier_4_Code", out X, out Y);
                CF_24D_L5_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L5_Diagnosis_Pointers", out X, out Y);
                CF_24E_L5_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L5_Charges_Principal", out X, out Y);
                CF_24F_L5_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L5_Charges_Secondary", out X, out Y);
                CF_24F_L5_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L5_Days_Units", out X, out Y);
                CF_24G_L5_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan
                GetCoordinates("CF_24H_L5_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L5_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);
                GetCoordinates("CF_24H_L5_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L5_EPSDT_FamilyPlan = new FormFieldString(X, Y);

                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L5_RenderingProvider_NPI", out X, out Y);
                CF_24J_L5_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L5_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L5_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L5_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L5_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);


                //Line Note
                GetCoordinates("CF_24A_L5_Note", out X, out Y);
                CF_24A_L5_Note = new FormFieldString(X, Y);

                #endregion

                #region " Service Line 6 "

             //   bool CF_IsPresent_Line6 = false;

                //From Date.
                GetCoordinates("CF_24A_L6_DOS_From_MM", out X, out Y);
                CF_24A_L6_DOS_From_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L6_DOS_From_DD", out X, out Y);
                CF_24A_L6_DOS_From_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L6_DOS_From_YY", out X, out Y);
                CF_24A_L6_DOS_From_YY = new FormFieldString(X, Y);

                //To Date
                GetCoordinates("CF_24A_L6_DOS_To_MM", out X, out Y);
                CF_24A_L6_DOS_To_MM = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L6_DOS_To_DD", out X, out Y);
                CF_24A_L6_DOS_To_DD = new FormFieldString(X, Y);
                GetCoordinates("CF_24A_L6_DOS_To_YY", out X, out Y);
                CF_24A_L6_DOS_To_YY = new FormFieldString(X, Y);

                //Place Of Service
                GetCoordinates("CF_24B_L6_POS_Code", out X, out Y);
                CF_24B_L6_POS_Code = new FormFieldString(X, Y);

                //EMG - Emergency
                GetCoordinates("CF_24C_L6_EMG_Code", out X, out Y);
                CF_24C_L6_EMG_Code = new FormFieldString(X, Y);

                //CPT/HCPCS 
                GetCoordinates("CF_24D_L6_CPT_HCPCS_Code", out X, out Y);
                CF_24D_L6_CPT_HCPCS_Code = new FormFieldString(X, Y);

                //Modifiers
                GetCoordinates("CF_24D_L6_Modifier_1_Code", out X, out Y);
                CF_24D_L6_Modifier_1_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L6_Modifier_2_Code", out X, out Y);
                CF_24D_L6_Modifier_2_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L6_Modifier_3_Code", out X, out Y);
                CF_24D_L6_Modifier_3_Code = new FormFieldString(X, Y);
                GetCoordinates("CF_24D_L6_Modifier_4_Code", out X, out Y);
                CF_24D_L6_Modifier_4_Code = new FormFieldString(X, Y);

                //Diagnosis Pointers 
                GetCoordinates("CF_24E_L6_Diagnosis_Pointers", out X, out Y);
                CF_24E_L6_Diagnosis_Pointers = new FormFieldString(X, Y);

                //Charges
                GetCoordinates("CF_24F_L6_Charges_Principal", out X, out Y);
                CF_24F_L6_Charges_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_24F_L6_Charges_Secondary", out X, out Y);
                CF_24F_L6_Charges_Secondary = new FormFieldString(X, Y);

                //Days or Units
                GetCoordinates("CF_24G_L6_Days_Units", out X, out Y);
                CF_24G_L6_Days_Units = new FormFieldString(X, Y);

                //EPSDT Family Plan
                GetCoordinates("CF_24H_L6_EPSDT_FamilyPlan_Shaded", out X, out Y);
                CF_24H_L6_EPSDT_FamilyPlan_Shaded = new FormFieldString(X, Y);
                GetCoordinates("CF_24H_L6_EPSDT_FamilyPlan", out X, out Y);
                CF_24H_L6_EPSDT_FamilyPlan = new FormFieldString(X, Y);

                //Rendering Provider ID (NPI)
                GetCoordinates("CF_24J_L6_RenderingProvider_NPI", out X, out Y);
                CF_24J_L6_RenderingProvider_NPI = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier)
                GetCoordinates("CF_24J_L6_RenderingProvider_OtherQualifier", out X, out Y);
                CF_24J_L6_RenderingProvider_OtherQualifier = new FormFieldString(X, Y);

                //Rendering Provider Other ID (Qualifier value)
                GetCoordinates("CF_24J_L6_RenderingProvider_OtherQualifiervalue", out X, out Y);
                CF_24J_L6_RenderingProvider_OtherQualifiervalue = new FormFieldString(X, Y);



                //Line Note
                GetCoordinates("CF_24A_L6_Note", out X, out Y);
                CF_24A_L6_Note = new FormFieldString(X, Y);

                #endregion

                GetCoordinates("CF_25_FederalTax_ID_No", out X, out Y);
                CF_25_FederalTax_ID_No = new FormFieldString(X, Y);
                GetCoordinates("CF_25_FederalTaxID_Qualifier_SSN", out X, out Y);
                CF_25_FederalTaxID_Qualifier_SSN = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_25_FederalTaxID_Qualifier_EIN", out X, out Y);
                CF_25_FederalTaxID_Qualifier_EIN = new FormFieldBoolean(X, Y);

                GetCoordinates("CF_26_PatientAccount_No", out X, out Y);
                CF_26_PatientAccount_No = new FormFieldString(X, Y);

                GetCoordinates("CF_27_AcceptAssignment_YES", out X, out Y);
                CF_27_AcceptAssignment_YES = new FormFieldBoolean(X, Y);
                GetCoordinates("CF_27_AcceptAssignment_NO", out X, out Y);
                CF_27_AcceptAssignment_NO = new FormFieldBoolean(X, Y);

                GetCoordinates("CF_28_TotalCharge_Principal", out X, out Y);
                CF_28_TotalCharge_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_28_TotalCharge_Secondary", out X, out Y);
                CF_28_TotalCharge_Secondary = new FormFieldString(X, Y);

                GetCoordinates("CF_29_AmountPaid_Principal", out X, out Y);
                CF_29_AmountPaid_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_29_AmountPaid_Secondary", out X, out Y);
                CF_29_AmountPaid_Secondary = new FormFieldString(X, Y);

                GetCoordinates("CF_30_BalanceDue_Principal", out X, out Y);
                CF_30_BalanceDue_Principal = new FormFieldString(X, Y);
                GetCoordinates("CF_30_BalanceDue_Secondary", out X, out Y);
                CF_30_BalanceDue_Secondary = new FormFieldString(X, Y);

                GetCoordinates("CF_31_Physician_Supplier_Signature", out X, out Y);
                CF_31_Physician_Supplier_Signature = new FormFieldString(X, Y);
                GetCoordinates("CF_31_Physician_Supplier_Signature_Date", out X, out Y);
                CF_31_Physician_Supplier_Signature_Date = new FormFieldString(X, Y); //(519, 2078);
                GetCoordinates("CF_31_Physician_Supplier_QualifierValue", out X, out Y);
                CF_31_Physician_Supplier_QualifierValue = new FormFieldString(X, Y); 

                GetCoordinates("CF_32_Service_Facility_Name", out X, out Y);
                CF_32_Service_Facility_Name = new FormFieldString(X, Y);
                GetCoordinates("CF_32_Service_Facility_Address_Line1", out X, out Y);
                CF_32_Service_Facility_Address_Line1 = new FormFieldString(X, Y);
                GetCoordinates("CF_32_Service_Facility_Address_Line2", out X, out Y);
                CF_32_Service_Facility_Address_Line2 = new FormFieldString(X, Y);
                GetCoordinates("CF_32_Service_Facility_City", out X, out Y);
                CF_32_Service_Facility_City = new FormFieldString(X, Y);
                GetCoordinates("CF_32_Service_Facility_State", out X, out Y);
                CF_32_Service_Facility_State = new FormFieldString(X, Y);
                GetCoordinates("CF_32_Service_Facility_Zip", out X, out Y);
                CF_32_Service_Facility_Zip = new FormFieldString(X, Y);
                GetCoordinates("CF_32a_Service_Facility_NPI", out X, out Y);
                CF_32a_Service_Facility_NPI = new FormFieldString(X, Y);
                GetCoordinates("CF_32b_Service_Facility_UPIN_OtherID", out X, out Y);
                CF_32b_Service_Facility_UPIN_OtherID = new FormFieldString(X, Y);

                GetCoordinates("CF_33_BillingProvider_Name", out X, out Y);
                CF_33_BillingProvider_Name = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_Address_Line1", out X, out Y);
                CF_33_BillingProvider_Address_Line1 = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_Address_Line2", out X, out Y);
                CF_33_BillingProvider_Address_Line2 = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_City", out X, out Y);
                CF_33_BillingProvider_City = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_State", out X, out Y);
                CF_33_BillingProvider_State = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_Zip", out X, out Y);
                CF_33_BillingProvider_Zip = new FormFieldString(X, Y);
                GetCoordinates("CF_33a_BillingProvider_NPI", out X, out Y);
                CF_33a_BillingProvider_NPI = new FormFieldString(X, Y);
                GetCoordinates("CF_33b_BillingProvider_UPIN_OtherID", out X, out Y);
                CF_33b_BillingProvider_UPIN_OtherID = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_Tel_AreaCode", out X, out Y);
                CF_33_BillingProvider_Tel_AreaCode = new FormFieldString(X, Y);
                GetCoordinates("CF_33_BillingProvider_Tel_Number", out X, out Y);
                CF_33_BillingProvider_Tel_Number = new FormFieldString(X, Y);
            }

            #region "Code added by Pankaj 23122009 for PrintForm Setup"
            // New method is created for InitializingBoxes for Print on Form
            public void InitializeBoxesForPrintForm()
            {
                Int32 X = 75;
                Int32 Y = 25;


                //.Insurance Header
                CF_Top_InsuranceHeader = new FormFieldString(X + 322, Y + 30);
                //.Insurance Type
                CF_1_Insuracne_Type_Medicare = new FormFieldBoolean(X + 7, Y + 125);
                CF_1_Insuracne_Type_Medicaid = new FormFieldBoolean(X + 76, Y + 125);
                CF_1_Insuracne_Type_Tricare = new FormFieldBoolean(X + 144, Y + 125);
                CF_1_Insuracne_Type_Champva = new FormFieldBoolean(X + 235, Y + 125);
                CF_1_Insuracne_Type_GroupHealthPlan = new FormFieldBoolean(X + 305, Y + 125);
                CF_1_Insuracne_Type_FECA = new FormFieldBoolean(X + 385, Y + 125);
                CF_1_Insuracne_Type_Other = new FormFieldBoolean(X + 445, Y + 125);

                //.Insured's ID Number
                CF_1a_InsuredsIDNumber = new FormFieldString(X + 500, Y + 125);

                //.Patient's Name
                //public FormFieldString CF_2_Patient_Name = new FormFieldString(30, 300);

                CF_2_Patient_Name = new FormFieldString(X + 13, Y + 158);

                //.Patient's Birth Date
                CF_3_Patient_DOB_MM = new FormFieldString(X + 308, Y + 158);
                CF_3_Patient_DOB_DD = new FormFieldString(X + 338, Y + 158);
                CF_3_Patient_DOB_YY = new FormFieldString(X + 363, Y + 158);

                //.Patient's Sex
                CF_3_Patient_Sex_Male = new FormFieldBoolean(X + 416, Y + 158);
                CF_3_Patient_Sex_Female = new FormFieldBoolean(X + 465, Y + 158);

                //.Insured's Name
                CF_4_Insureds_Name = new FormFieldString(X + 500, Y + 158);

                //.Patient's Address
                CF_5_Patient_Address = new FormFieldString(X + 13, Y + 192);
                CF_5_Patient_City = new FormFieldString(X + 13, Y + 220);
                CF_5_Patient_State = new FormFieldString(X + 255, Y + 220);
                CF_5_Patient_Zip = new FormFieldString(X + 13, Y + 255);
                CF_5_Patient_Tel_AreaCode = new FormFieldString(X + 147, Y + 258);
                CF_5_Patient_Tel_Number = new FormFieldString(X + 183, Y + 258);

                //.Paient Relationship to Insured
                CF_6_PatientRelationship_Self = new FormFieldBoolean(X + 326, Y + 192);
                CF_6_PatientRelationship_Spouse = new FormFieldBoolean(X + 375, Y + 192);
                CF_6_PatientRelationship_Child = new FormFieldBoolean(X + 416, Y + 192);
                CF_6_PatientRelationship_Other = new FormFieldBoolean(X + 465, Y + 192);

                //.Insured's Address
                CF_7_Insureds_Address = new FormFieldString(X + 500, Y + 192);
                CF_7_Insureds_City = new FormFieldString(X + 500, Y + 223);
                CF_7_Insureds_State = new FormFieldString(X + 732, Y + 223);
                CF_7_Insureds_Zip = new FormFieldString(X + 500, Y + 258);
                CF_7_Insureds_Tel_AreaCode = new FormFieldString(X + 650, Y + 258);
                CF_7_Insureds_Tel_Number = new FormFieldString(X + 684, Y + 258);

                //.Patient Status
                CF_8_PatientStatus_Single = new FormFieldBoolean(X + 346, Y + 225);
                CF_8_PatientStatus_Married = new FormFieldBoolean(X + 405, Y + 225);
                CF_8_PatientStatus_Other = new FormFieldBoolean(X + 465, Y + 225);
                CF_8_PatientStatus_Employed = new FormFieldBoolean(X + 346, Y + 257);
                CF_8_PatientStatus_FullTimeStudent = new FormFieldBoolean(X + 405, Y + 257);
                CF_8_PatientStatus_PartTimeStudent = new FormFieldBoolean(X + 465, Y + 257);

                //.Other Insured's Name
                CF_9_Other_Insureds_Name = new FormFieldString(X + 14, Y + 290);
                CF_9_Other_Insureds_PolicyGroupNo = new FormFieldString(X + 14, Y + 321);
                CF_9_Other_Insureds_DOB_MM = new FormFieldString(X + 14, Y + 358);
                CF_9_Other_Insureds_DOB_DD = new FormFieldString(X + 44, Y + 358);
                CF_9_Other_Insureds_DOB_YY = new FormFieldString(X + 74, Y + 358);
                CF_9_Other_Insureds_Sex_Male = new FormFieldBoolean(X + 176, Y + 358);
                CF_9_Other_Insureds_Sex_Female = new FormFieldBoolean(X + 237, Y + 358);
                CF_9_Other_Insureds_EmployerName = new FormFieldString(X + 14, Y + 390);
                CF_9_Other_Insureds_InsuracnePlan = new FormFieldString(X + 14, Y + 424);

                //.Patient Condition 
                CF_10_PatientConditionTo_Employement_Yes = new FormFieldBoolean(X + 346, Y + 324);
                CF_10_PatientConditionTo_Employement_No = new FormFieldBoolean(X + 406, Y + 324);
                CF_10_PatientConditionTo_AutoAccident_Yes = new FormFieldBoolean(X + 346, Y + 358);
                CF_10_PatientConditionTo_AutoAccident_No = new FormFieldBoolean(X + 406, Y + 358);
                CF_10_PatientConditionTo_AutoAccident_State = new FormFieldString(X + 450, Y + 358);
                CF_10_PatientConditionTo_OtherAccident_Yes = new FormFieldBoolean(X + 346, Y + 391);
                CF_10_PatientConditionTo_OtherAccident_No = new FormFieldBoolean(X + 406, Y + 391);
                CF_10_PatientConditionTo_ResForLocaluse = new FormFieldString(X + 308, Y + 424);

                //.Insured's Information
                CF_11_Insureds_PolicyGroupNo = new FormFieldString(X + 502, Y + 290);
                CF_11_Insureds_DOB_MM = new FormFieldString(X + 531, Y + 325);
                CF_11_Insureds_DOB_DD = new FormFieldString(X + 554, Y + 325);
                CF_11_Insureds_DOB_YY = new FormFieldString(X + 583, Y + 325);
                CF_11_Insureds_Sex_Male = new FormFieldBoolean(X + 675, Y + 324);
                CF_11_Insureds_Sex_Female = new FormFieldBoolean(X + 746, Y + 324);
                CF_11_Insureds_EmployerName = new FormFieldString(X + 502, Y + 356);
                CF_11_Insureds_InsuracnePlan = new FormFieldString(X + 502, Y + 390);
                CF_11_Insureds_OtherHealthPlan_Yes = new FormFieldBoolean(X + 514, Y + 425);
                CF_11_Insureds_OtherHealthPlan_No = new FormFieldBoolean(X + 566, Y + 425);

                CF_12_PatientAuthorizedPersons_Signature = new FormFieldString(X + 100, Y + 483);
                CF_12_PatientAuthorizedPersons_Signature_Date = new FormFieldString(X + 371, Y + 483);

                CF_13_InsuredsAuthorizedPersons_Signature = new FormFieldString(X + 620, Y + 483);

                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM = new FormFieldString(X + 18, Y + 525);//gloPM5040
                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD = new FormFieldString(X + 48, Y + 525);
                CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY = new FormFieldString(X + 82, Y + 525);

                CF_15_FirstDateOfSimilar_Illness_MM = new FormFieldString(X + 370, Y + 525);
                CF_15_FirstDateOfSimilar_Illness_DD = new FormFieldString(X + 397, Y + 525);
                CF_15_FirstDateOfSimilar_Illness_YY = new FormFieldString(X + 428, Y + 525);

                CF_16_UnableToWorkFromDate_MM = new FormFieldString(X + 536, Y + 525);
                CF_16_UnableToWorkFromDate_DD = new FormFieldString(X + 567, Y + 525);
                CF_16_UnableToWorkFromDate_YY = new FormFieldString(X + 599, Y + 525);

                CF_16_UnableToWorkTillDate_MM = new FormFieldString(X + 673, Y + 525);
                CF_16_UnableToWorkTillDate_DD = new FormFieldString(X + 706, Y + 525);
                CF_16_UnableToWorkTillDate_YY = new FormFieldString(X + 738, Y + 525);

                CF_17_ReferringProvider_Name = new FormFieldString(X + 14, Y + 557);
                CF_17a_ReferringProvider_OtherQualifier = new FormFieldString(X + 296, Y + 541);
                CF_17a_ReferringProvider_OtherID = new FormFieldString(X + 320, Y + 541);
                CF_17b_ReferringProvider_NPI = new FormFieldString(X + 320, Y + 559);

                CF_18_HospitalizationFromDate_MM = new FormFieldString(X + 536, Y + 559);
                CF_18_HospitalizationFromDate_DD = new FormFieldString(X + 567, Y + 559);
                CF_18_HospitalizationFromDate_YY = new FormFieldString(X + 599, Y + 559);

                CF_18_HospitalizationTillDate_MM = new FormFieldString(X + 673, Y + 559);
                CF_18_HospitalizationTillDate_DD = new FormFieldString(X + 706, Y + 559);
                CF_18_HospitalizationTillDate_YY = new FormFieldString(X + 738, Y + 559);

                CF_19_LocalUse_Field = new FormFieldString(X + 18, Y + 589);

                CF_20_OutsideLab_Yes = new FormFieldBoolean(X + 516, Y + 590);
                CF_20_OutsideLab_No = new FormFieldBoolean(X + 566, Y + 590);
                CF_20_OutsideLab_Charges_Principal = new FormFieldString(X + 630, Y + 589);
                CF_20_OutsideLab_Charges_Secondary = new FormFieldString(X + 715, Y + 589);

                CF_21_Diagnosis_1_Principal = new FormFieldString(X + 29, Y + 625);
                CF_21_Diagnosis_1_Secondary = new FormFieldString(X + 69, Y + 625);

                CF_21_Diagnosis_2_Principal = new FormFieldString(X + 299, Y + 625);
                CF_21_Diagnosis_2_Secondary = new FormFieldString(X + 340, Y + 625);

                CF_21_Diagnosis_3_Principal = new FormFieldString(X + 29, Y + 657);
                CF_21_Diagnosis_3_Secondary = new FormFieldString(X + 69, Y + 657);

                CF_21_Diagnosis_4_Principal = new FormFieldString(X + 299, Y + 657);
                CF_21_Diagnosis_4_Secondary = new FormFieldString(X + 340, Y + 657);

                CF_22_MecaidResubmission_Code = new FormFieldString(X + 511, Y + 625);
                CF_22_Original_Refrence_No = new FormFieldString(X + 620, Y + 625);

                CF_23_PriorAuthorization_No = new FormFieldString(X + 506, Y + 657);

                #region " Service Line 1 "

               // bool CF_IsPresent_Line1 = false;

                //From Date.
                CF_24A_L1_DOS_From_MM = new FormFieldString(X + 08, Y + 725);
                CF_24A_L1_DOS_From_DD = new FormFieldString(X + 35, Y + 725);
                CF_24A_L1_DOS_From_YY = new FormFieldString(X + 62, Y + 725);

                //To Date
                CF_24A_L1_DOS_To_MM = new FormFieldString(X + 94, Y + 725);
                CF_24A_L1_DOS_To_DD = new FormFieldString(X + 124, Y + 725);
                CF_24A_L1_DOS_To_YY = new FormFieldString(X + 152, Y + 725);

                //Place Of Service
                CF_24B_L1_POS_Code = new FormFieldString(X + 183, Y + 725);

                //EMG - Emergency
                CF_24C_L1_EMG_Code = new FormFieldString(X + 214, Y + 725);

                //CPT/HCPCS 
                CF_24D_L1_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 725);

                //Modifiers
                CF_24D_L1_Modifier_1_Code = new FormFieldString(X + 322, Y + 725);
                CF_24D_L1_Modifier_2_Code = new FormFieldString(X + 353, Y + 725);
                CF_24D_L1_Modifier_3_Code = new FormFieldString(X + 383, Y + 725);
                CF_24D_L1_Modifier_4_Code = new FormFieldString(X + 414, Y + 725);

                //Diagnosis Pointers 
                CF_24E_L1_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 725);

                //Charges
                CF_24F_L1_Charges_Principal = new FormFieldString(X + 503, Y + 725);
                CF_24F_L1_Charges_Secondary = new FormFieldString(X + 558, Y + 725);

                //Days or Units
                CF_24G_L1_Days_Units = new FormFieldString(X + 590, Y + 725);

                //EPSDT Family Plan
                CF_24H_L1_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 708);
                CF_24H_L1_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 725);

                //Rendering Provider ID (NPI)
                CF_24J_L1_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 725);

                //Rendering Provider QF  
                CF_24J_L1_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 708);
                //Rendering Provider QF Value 
                CF_24J_L1_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 708);
                
                //Line Note
                CF_24A_L1_Note = new FormFieldString(X + 10, Y + 708);

                #endregion

                #region " Service Line 2 "

              //  bool CF_IsPresent_Line2 = false;

                //From Date.
                CF_24A_L2_DOS_From_MM = new FormFieldString(X + 08, Y + 758);
                CF_24A_L2_DOS_From_DD = new FormFieldString(X + 35, Y + 758);
                CF_24A_L2_DOS_From_YY = new FormFieldString(X + 62, Y + 758);

                //To Date
                CF_24A_L2_DOS_To_MM = new FormFieldString(X + 94, Y + 758);
                CF_24A_L2_DOS_To_DD = new FormFieldString(X + 124, Y + 758);
                CF_24A_L2_DOS_To_YY = new FormFieldString(X + 152, Y + 758);

                //Place Of Service
                CF_24B_L2_POS_Code = new FormFieldString(X + 183, Y + 758);

                //EMG - Emergency
                CF_24C_L2_EMG_Code = new FormFieldString(X + 214, Y + 758);

                //CPT/HCPCS 
                CF_24D_L2_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 758);

                //Modifiers
                CF_24D_L2_Modifier_1_Code = new FormFieldString(X + 322, Y + 758);
                CF_24D_L2_Modifier_2_Code = new FormFieldString(X + 353, Y + 758);
                CF_24D_L2_Modifier_3_Code = new FormFieldString(X + 383, Y + 758);
                CF_24D_L2_Modifier_4_Code = new FormFieldString(X + 414, Y + 758);

                //Diagnosis Pointers 
                CF_24E_L2_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 758);

                //Charges
                CF_24F_L2_Charges_Principal = new FormFieldString(X + 503, Y + 758);
                CF_24F_L2_Charges_Secondary = new FormFieldString(X + 558, Y + 758);

                //Days or Units
                CF_24G_L2_Days_Units = new FormFieldString(X + 590, Y + 758);

                //EPSDT Family Plan
                CF_24H_L2_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 741);
                CF_24H_L2_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 758);

                //Rendering Provider ID (NPI)
                CF_24J_L2_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 758);

                //Rendering Provider QF  
                CF_24J_L2_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 740);
                //Rendering Provider QF Value 
                CF_24J_L2_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 740);

                //Line Note
                CF_24A_L2_Note = new FormFieldString(X + 10, Y + 741);

                #endregion

                #region " Service Line 3 "

              //  bool CF_IsPresent_Line3 = false;

                //From Date.
                CF_24A_L3_DOS_From_MM = new FormFieldString(X + 08, Y + 791);
                CF_24A_L3_DOS_From_DD = new FormFieldString(X + 35, Y + 791);
                CF_24A_L3_DOS_From_YY = new FormFieldString(X + 62, Y + 791);

                //To Date
                CF_24A_L3_DOS_To_MM = new FormFieldString(X + 94, Y + 791);
                CF_24A_L3_DOS_To_DD = new FormFieldString(X + 124, Y + 791);
                CF_24A_L3_DOS_To_YY = new FormFieldString(X + 152, Y + 791);


                //Place Of Service
                CF_24B_L3_POS_Code = new FormFieldString(X + 183, Y + 791);

                //EMG - Emergency
                CF_24C_L3_EMG_Code = new FormFieldString(X + 214, Y + 791);

                //CPT/HCPCS 
                CF_24D_L3_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 791);

                //Modifiers
                CF_24D_L3_Modifier_1_Code = new FormFieldString(X + 322, Y + 791);
                CF_24D_L3_Modifier_2_Code = new FormFieldString(X + 353, Y + 791);
                CF_24D_L3_Modifier_3_Code = new FormFieldString(X + 383, Y + 791);
                CF_24D_L3_Modifier_4_Code = new FormFieldString(X + 414, Y + 791);

                //Diagnosis Pointers 
                CF_24E_L3_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 791);

                //Charges
                CF_24F_L3_Charges_Principal = new FormFieldString(X + 502, Y + 791);
                CF_24F_L3_Charges_Secondary = new FormFieldString(X + 558, Y + 791);

                //Days or Units
                CF_24G_L3_Days_Units = new FormFieldString(X + 590, Y + 791);

                //EPSDT Family Plan
                CF_24H_L3_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 774);
                CF_24H_L3_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 791);

                //Rendering Provider ID (NPI)
                CF_24J_L3_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 791);


                //Rendering Provider QF  
                CF_24J_L3_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 774);
                //Rendering Provider QF Value 
                CF_24J_L3_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 774);

                //Line Note
                CF_24A_L3_Note = new FormFieldString(X + 10, Y + 774);

                #endregion

                #region " Service Line 4 "

            //    bool CF_IsPresent_Line4 = false;

                //From Date.
                CF_24A_L4_DOS_From_MM = new FormFieldString(X + 08, Y + 824);
                CF_24A_L4_DOS_From_DD = new FormFieldString(X + 35, Y + 824);
                CF_24A_L4_DOS_From_YY = new FormFieldString(X + 62, Y + 824);

                //To Date
                CF_24A_L4_DOS_To_MM = new FormFieldString(X + 94, Y + 824);
                CF_24A_L4_DOS_To_DD = new FormFieldString(X + 124, Y + 824);
                CF_24A_L4_DOS_To_YY = new FormFieldString(X + 152, Y + 824);

                //Place Of Service
                CF_24B_L4_POS_Code = new FormFieldString(X + 183, Y + 824);

                //EMG - Emergency
                CF_24C_L4_EMG_Code = new FormFieldString(X + 214, Y + 824);

                //CPT/HCPCS 
                CF_24D_L4_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 824);

                //Modifiers
                CF_24D_L4_Modifier_1_Code = new FormFieldString(X + 322, Y + 824);
                CF_24D_L4_Modifier_2_Code = new FormFieldString(X + 353, Y + 824);
                CF_24D_L4_Modifier_3_Code = new FormFieldString(X + 383, Y + 824);
                CF_24D_L4_Modifier_4_Code = new FormFieldString(X + 414, Y + 824);

                //Diagnosis Pointers 
                CF_24E_L4_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 824);

                //Charges
                CF_24F_L4_Charges_Principal = new FormFieldString(X + 503, Y + 824);
                CF_24F_L4_Charges_Secondary = new FormFieldString(X + 558, Y + 824);

                //Days or Units
                CF_24G_L4_Days_Units = new FormFieldString(X + 590, Y + 824);

                //EPSDT Family Plan
                CF_24H_L4_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 807);
                CF_24H_L4_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 824);

                //Rendering Provider ID (NPI)
                CF_24J_L4_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 824);


                //Rendering Provider QF  
                CF_24J_L4_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 808);
                //Rendering Provider QF Value 
                CF_24J_L4_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 808);


                //Line Note
                CF_24A_L4_Note = new FormFieldString(X + 10, Y + 807);

                #endregion

                #region " Service Line 5 "

         //       bool CF_IsPresent_Line5 = false;

                //From Date.
                CF_24A_L5_DOS_From_MM = new FormFieldString(X + 08, Y + 857);
                CF_24A_L5_DOS_From_DD = new FormFieldString(X + 35, Y + 857);
                CF_24A_L5_DOS_From_YY = new FormFieldString(X + 62, Y + 857);

                //To Date
                CF_24A_L5_DOS_To_MM = new FormFieldString(X + 94, Y + 857);
                CF_24A_L5_DOS_To_DD = new FormFieldString(X + 124, Y + 857);
                CF_24A_L5_DOS_To_YY = new FormFieldString(X + 152, Y + 857);

                //Place Of Service
                CF_24B_L5_POS_Code = new FormFieldString(X + 183, Y + 857);

                //EMG - Emergency
                CF_24C_L5_EMG_Code = new FormFieldString(X + 214, Y + 857);

                //CPT/HCPCS 
                CF_24D_L5_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 857);

                //Modifiers
                CF_24D_L5_Modifier_1_Code = new FormFieldString(X + 322, Y + 857);
                CF_24D_L5_Modifier_2_Code = new FormFieldString(X + 353, Y + 857);
                CF_24D_L5_Modifier_3_Code = new FormFieldString(X + 383, Y + 857);
                CF_24D_L5_Modifier_4_Code = new FormFieldString(X + 414, Y + 857);

                //Diagnosis Pointers 
                CF_24E_L5_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 857);

                //Charges
                CF_24F_L5_Charges_Principal = new FormFieldString(X + 503, Y + 857);
                CF_24F_L5_Charges_Secondary = new FormFieldString(X + 558, Y + 857);

                //Days or Units
                CF_24G_L5_Days_Units = new FormFieldString(X + 590, Y + 857);

                //EPSDT Family Plan
                CF_24H_L5_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 840);
                CF_24H_L5_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 857);

                //Rendering Provider ID (NPI)
                CF_24J_L5_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 857);


                //Rendering Provider QF  
                CF_24J_L5_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 842);
                //Rendering Provider QF Value 
                CF_24J_L5_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 842);

                //Line Note
                CF_24A_L5_Note = new FormFieldString(X + 10, Y + 840);

                #endregion

                #region " Service Line 6 "

               // bool CF_IsPresent_Line6 = false;

                //From Date.
                CF_24A_L6_DOS_From_MM = new FormFieldString(X + 08, Y + 890);
                CF_24A_L6_DOS_From_DD = new FormFieldString(X + 35, Y + 890);
                CF_24A_L6_DOS_From_YY = new FormFieldString(X + 62, Y + 890);

                //To Date
                CF_24A_L6_DOS_To_MM = new FormFieldString(X + 94, Y + 890);
                CF_24A_L6_DOS_To_DD = new FormFieldString(X + 124, Y + 890);
                CF_24A_L6_DOS_To_YY = new FormFieldString(X + 152, Y + 890);

                //Place Of Service
                CF_24B_L6_POS_Code = new FormFieldString(X + 183, Y + 890);

                //EMG - Emergency
                CF_24C_L6_EMG_Code = new FormFieldString(X + 214, Y + 890);

                //CPT/HCPCS 
                CF_24D_L6_CPT_HCPCS_Code = new FormFieldString(X + 255, Y + 890);

                //Modifiers
                CF_24D_L6_Modifier_1_Code = new FormFieldString(X + 322, Y + 890);
                CF_24D_L6_Modifier_2_Code = new FormFieldString(X + 353, Y + 890);
                CF_24D_L6_Modifier_3_Code = new FormFieldString(X + 383, Y + 890);
                CF_24D_L6_Modifier_4_Code = new FormFieldString(X + 414, Y + 890);

                //Diagnosis Pointers 
                CF_24E_L6_Diagnosis_Pointers = new FormFieldString(X + 451, Y + 890);

                //Charges
                CF_24F_L6_Charges_Principal = new FormFieldString(X + 503, Y + 890);
                CF_24F_L6_Charges_Secondary = new FormFieldString(X + 558, Y + 890);

                //Days or Units
                CF_24G_L6_Days_Units = new FormFieldString(X + 590, Y + 890);

                //EPSDT Family Plan
                CF_24H_L6_EPSDT_FamilyPlan_Shaded = new FormFieldString(X + 620, Y + 873);
                CF_24H_L6_EPSDT_FamilyPlan = new FormFieldString(X + 622, Y + 890);

                //Rendering Provider ID (NPI)
                CF_24J_L6_RenderingProvider_NPI = new FormFieldString(X + 675, Y + 890);


                //Rendering Provider QF  
                CF_24J_L6_RenderingProvider_OtherQualifier = new FormFieldString(X + 642, Y + 876);
                //Rendering Provider QF Value 
                CF_24J_L6_RenderingProvider_OtherQualifiervalue = new FormFieldString(X + 675, Y + 876);

                //Line Note
                CF_24A_L6_Note = new FormFieldString(X + 10, Y + 873);

                #endregion

                CF_25_FederalTax_ID_No = new FormFieldString(X + 15, Y + 923);
                CF_25_FederalTaxID_Qualifier_SSN = new FormFieldBoolean(X + 166, Y + 923);
                CF_25_FederalTaxID_Qualifier_EIN = new FormFieldBoolean(X + 187, Y + 923);

                CF_26_PatientAccount_No = new FormFieldString(X + 231, Y + 923);

                CF_27_AcceptAssignment_YES = new FormFieldBoolean(X + 376, Y + 923);
                CF_27_AcceptAssignment_NO = new FormFieldBoolean(X + 426, Y + 923);

                CF_28_TotalCharge_Principal = new FormFieldString(X + 504, Y + 923);
                CF_28_TotalCharge_Secondary = new FormFieldString(X + 578, Y + 923);

                CF_29_AmountPaid_Principal = new FormFieldString(X + 605, Y + 923);
                CF_29_AmountPaid_Secondary = new FormFieldString(X + 678, Y + 923);

                CF_30_BalanceDue_Principal = new FormFieldString(X + 695, Y + 923);
                CF_30_BalanceDue_Secondary = new FormFieldString(X + 768, Y + 923);

                // ----------------------------
                // Code modified by Pankaj Bedse 22012010
                // X,Y Co-ordinates changed to display large physician sign in a row
                CF_31_Physician_Supplier_Signature = new FormFieldString(X + 10, Y + 985);
                CF_31_Physician_Supplier_Signature_Date = new FormFieldString(X + 140, Y + 998); //(519, 2078);
                CF_31_Physician_Supplier_QualifierValue = new FormFieldString(X + 50, Y + 1007); //(519, 2078);
                // ----------------------------

                CF_32_Service_Facility_Name = new FormFieldString(X + 231, Y + 953);
                CF_32_Service_Facility_Address_Line1 = new FormFieldString(X + 231, Y + 965);
                CF_32_Service_Facility_Address_Line2 = new FormFieldString(X + 231, Y + 977);
                CF_32_Service_Facility_City = new FormFieldString(X + 356, Y + 955);
                CF_32_Service_Facility_State = new FormFieldString(X + 396, Y + 955);
                CF_32_Service_Facility_Zip = new FormFieldString(X + 431, Y + 955);

                CF_32a_Service_Facility_NPI = new FormFieldString(X + 241, Y + 1007);
                CF_32b_Service_Facility_UPIN_OtherID = new FormFieldString(X + 351, Y + 1007);

                CF_33_BillingProvider_Name = new FormFieldString(X + 505, Y + 953);
                CF_33_BillingProvider_Address_Line1 = new FormFieldString(X + 505, Y + 965);
                CF_33_BillingProvider_Address_Line2 = new FormFieldString(X + 505, Y + 977);
                CF_33_BillingProvider_City = new FormFieldString(X + 625, Y + 958);
                CF_33_BillingProvider_State = new FormFieldString(X + 675, Y + 958);
                CF_33_BillingProvider_Zip = new FormFieldString(X + 710, Y + 958);

                CF_33a_BillingProvider_NPI = new FormFieldString(X + 510, Y + 1007);
                CF_33b_BillingProvider_UPIN_OtherID = new FormFieldString(X + 620, Y + 1007);

                CF_33_BillingProvider_Tel_AreaCode = new FormFieldString(X + 655, Y + 941);
                CF_33_BillingProvider_Tel_Number = new FormFieldString(X + 692, Y + 941);
            }
            #endregion
            


            private DataTable GetPrinterSetting()
            {
                try
                {
                    string _Query = "";
                    DataTable dtSettings;
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName " +
                            " FROM BL_CMS1500PrintSettings WHERE nClinicID = 1 AND nFormType = 1";
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

            private DataTable GetDefaultPrinterSetting()
            {
                try
                {
                    string _Query = "";
                    DataTable dtSettings;
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName " +
                            " FROM BL_CMS1500PrintSettings WHERE nClinicID = 1 AND nFormType = 0";
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

            private void GetCoordinates(String sBoxName, out Int32 nX, out Int32 nY)
            {
                try
                {
                    nX = 0;
                    nY = 0;
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
                        }

                    }
                }
                catch (Exception ex)
                {
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    throw ex;
                }
            }
            #endregion

        }

        public class FormFieldString
        {
            #region "Constructor & Destructor"
            public FormFieldString()
            {
                _LocationX = 0;
                _LocationY = 0;
                _nCharSize = 30;
            }
            public FormFieldString(float PointX, float PointY,Int32 nCharSize=30)
            {
                _LocationX = PointX;
                _LocationY = PointY;
                _nCharSize = nCharSize;
               
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
            private Int32 _nCharSize = 0;
            public string Value
            {
                get { return _FieldValue; }
                set { _FieldValue = value; }
            }

            public System.Drawing.PointF Location
            {
                get { return new System.Drawing.PointF(_LocationX, _LocationY); }
            }
            public Int32 CharSize
            {
                get { return _nCharSize; }
                set { _nCharSize = value; }
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
