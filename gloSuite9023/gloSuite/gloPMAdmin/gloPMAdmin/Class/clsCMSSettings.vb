Imports System.IO

Public Class clsCMSSettings

    Public Function GetCMSPrinterSettings() As DataTable
        Try
            Dim _Query As String = ""
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
            Dim dtSettings As New DataTable
            oDB.Connect(False)
            _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
                & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel] FROM BL_CMS1500PrintSettings " _
                & " WHERE nClinicID = 1 AND nFormType = 1 ORDER BY [Field Name], [nPanel]"
            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Function

    Public Function GetDefaultCMSPrinterSettings() As DataTable
        Try
            Dim _Query As String = ""
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
            Dim dtSettings As New DataTable
            oDB.Connect(False)
            _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
               & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel] FROM BL_CMS1500PrintSettings " _
               & " WHERE nClinicID = 1 AND nFormType = 0 ORDER BY [Field Name], [nPanel]"
            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Function
End Class
Public Class gloHCFA1500PaperForm
#Region "Constructor & Destructor"

    Public Sub New()
        _DataBaseConnectionString = mdlGeneral.GetConnectionString
        dtSettings = GetPrinterSetting()
        InitializeBoxes()
    End Sub

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

#Region " Private Variables "
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private _DataBaseConnectionString As String
    Private dtSettings As DataTable
#End Region

#Region " Boxes Declarations "

    '.Insurance Header


    Public CF_Top_InsuranceHeader As FormFieldString
    ' = new FormFieldString(1042, 112);
    '.Insurance Type
    Public CF_1_Insuracne_Type_Medicare As FormFieldBoolean
    ' = new FormFieldBoolean(28, 99);
    Public CF_1_Insuracne_Type_Medicaid As FormFieldBoolean
    ' = new FormFieldBoolean(96, 99);
    Public CF_1_Insuracne_Type_Tricare As FormFieldBoolean
    ' = new FormFieldBoolean(156, 99);
    Public CF_1_Insuracne_Type_Champva As FormFieldBoolean
    ' = new FormFieldBoolean(247, 99);
    Public CF_1_Insuracne_Type_GroupHealthPlan As FormFieldBoolean
    ' = new FormFieldBoolean(314, 99);
    Public CF_1_Insuracne_Type_FECA As FormFieldBoolean
    ' = new FormFieldBoolean(392, 99);
    Public CF_1_Insuracne_Type_Other As FormFieldBoolean
    ' = new FormFieldBoolean(447, 99);
    '.Insured's ID Number
    Public CF_1a_InsuredsIDNumber As FormFieldString
    ' = new FormFieldString(500, 99);
    '.Patient's Name
    'public FormFieldString CF_2_Patient_Name ; // = new FormFieldString(30, 300);
    Public CF_2_Patient_Name As FormFieldString
    ' = new FormFieldString(29, 128);
    '.Patient's Birth Date
    Public CF_3_Patient_DOB_MM As FormFieldString
    ' = new FormFieldString(306, 132);
    Public CF_3_Patient_DOB_DD As FormFieldString
    ' = new FormFieldString(336, 132);
    Public CF_3_Patient_DOB_YY As FormFieldString
    ' = new FormFieldString(366, 132);
    '.Patient's Sex
    Public CF_3_Patient_Sex_Male As FormFieldBoolean
    ' = new FormFieldBoolean(430, 128);
    Public CF_3_Patient_Sex_Female As FormFieldBoolean
    ' = new FormFieldBoolean(480, 128);

    '.Insured's Name
    Public CF_4_Insureds_Name As FormFieldString
    ' = new FormFieldString(500, 128);
    '.Patient's Address
    Public CF_5_Patient_Address As FormFieldString
    ' = new FormFieldString(29, 160);
    Public CF_5_Patient_City As FormFieldString
    ' = new FormFieldString(29, 190);
    Public CF_5_Patient_State As FormFieldString
    ' = new FormFieldString(265, 190);
    Public CF_5_Patient_Zip As FormFieldString
    ' = new FormFieldString(29, 225);
    Public CF_5_Patient_Tel_AreaCode As FormFieldString
    ' = new FormFieldString(140, 228);
    Public CF_5_Patient_Tel_Number As FormFieldString
    ' = new FormFieldString(170, 228);
    '.Paient Relationship to Insured
    Public CF_6_PatientRelationship_Self As FormFieldBoolean
    ' = new FormFieldBoolean(321, 163);
    Public CF_6_PatientRelationship_Spouse As FormFieldBoolean
    ' = new FormFieldBoolean(371, 163);
    Public CF_6_PatientRelationship_Child As FormFieldBoolean
    ' = new FormFieldBoolean(404, 163);
    Public CF_6_PatientRelationship_Other As FormFieldBoolean
    ' = new FormFieldBoolean(440, 163);
    '.Insured's Address
    Public CF_7_Insureds_Address As FormFieldString
    ' = new FormFieldString(500, 163);
    Public CF_7_Insureds_City As FormFieldString
    ' = new FormFieldString(500, 190);
    Public CF_7_Insureds_State As FormFieldString
    ' = new FormFieldString(725, 190);
    Public CF_7_Insureds_Zip As FormFieldString
    ' = new FormFieldString(500, 225);
    Public CF_7_Insureds_Tel_AreaCode As FormFieldString
    ' = new FormFieldString(640, 225);
    Public CF_7_Insureds_Tel_Number As FormFieldString
    ' = new FormFieldString(670, 225);
    '.Patient Status
    Public CF_8_PatientStatus_Single As FormFieldBoolean
    ' = new FormFieldBoolean(246, 200);
    Public CF_8_PatientStatus_Married As FormFieldBoolean
    ' = new FormFieldBoolean(398, 200);
    Public CF_8_PatientStatus_Other As FormFieldBoolean
    ' = new FormFieldBoolean(456, 200);
    Public CF_8_PatientStatus_Employed As FormFieldBoolean
    ' = new FormFieldBoolean(346, 230);
    Public CF_8_PatientStatus_FullTimeStudent As FormFieldBoolean
    ' = new FormFieldBoolean(398, 230);
    Public CF_8_PatientStatus_PartTimeStudent As FormFieldBoolean
    ' = new FormFieldBoolean(458, 230);
    '.Other Insured's Name
    Public CF_9_Other_Insureds_Name As FormFieldString
    ' = new FormFieldString(30, 260);
    Public CF_9_Other_Insureds_PolicyGroupNo As FormFieldString
    ' = new FormFieldString(30, 287);
    Public CF_9_Other_Insureds_DOB_MM As FormFieldString
    ' = new FormFieldString(30, 322);
    Public CF_9_Other_Insureds_DOB_DD As FormFieldString
    ' = new FormFieldString(60, 322);
    Public CF_9_Other_Insureds_DOB_YY As FormFieldString
    ' = new FormFieldString(90, 322);
    Public CF_9_Other_Insureds_Sex_Male As FormFieldBoolean
    ' = new FormFieldBoolean(177, 324);
    Public CF_9_Other_Insureds_Sex_Female As FormFieldBoolean
    ' = new FormFieldBoolean(237, 324);
    Public CF_9_Other_Insureds_EmployerName As FormFieldString
    ' = new FormFieldString(30, 355);
    Public CF_9_Other_Insureds_InsuracnePlan As FormFieldString
    ' = new FormFieldString(30, 390);
    '.Patient Condition 
    Public CF_10_PatientConditionTo_Employement_Yes As FormFieldBoolean
    ' = new FormFieldBoolean(344, 290);
    Public CF_10_PatientConditionTo_Employement_No As FormFieldBoolean
    ' = new FormFieldBoolean(394, 290);
    Public CF_10_PatientConditionTo_AutoAccident_Yes As FormFieldBoolean
    ' = new FormFieldBoolean(344, 326);
    Public CF_10_PatientConditionTo_AutoAccident_No As FormFieldBoolean
    ' = new FormFieldBoolean(394, 326);
    Public CF_10_PatientConditionTo_AutoAccident_State As FormFieldString
    ' = new FormFieldString(439, 326);
    Public CF_10_PatientConditionTo_OtherAccident_Yes As FormFieldBoolean
    ' = new FormFieldBoolean(344, 831);
    Public CF_10_PatientConditionTo_OtherAccident_No As FormFieldBoolean
    ' = new FormFieldBoolean(394, 831);
    Public CF_10_PatientConditionTo_ResForLocaluse As FormFieldString
    ' = new FormFieldString(332, 370);
    '.Insured's Information
    Public CF_11_Insureds_PolicyGroupNo As FormFieldString
    ' = new FormFieldString(1150, 630);
    Public CF_11_Insureds_DOB_MM As FormFieldString
    ' = new FormFieldString(1212, 708);
    Public CF_11_Insureds_DOB_DD As FormFieldString
    ' = new FormFieldString(1270, 708);
    Public CF_11_Insureds_DOB_YY As FormFieldString
    ' = new FormFieldString(1350, 708);
    Public CF_11_Insureds_Sex_Male As FormFieldBoolean
    ' = new FormFieldBoolean(1498, 698);
    Public CF_11_Insureds_Sex_Female As FormFieldBoolean
    ' = new FormFieldBoolean(1638, 698);
    Public CF_11_Insureds_EmployerName As FormFieldString
    ' = new FormFieldString(1150, 764);
    Public CF_11_Insureds_InsuracnePlan As FormFieldString
    ' = new FormFieldString(1150, 830);
    Public CF_11_Insureds_OtherHealthPlan_Yes As FormFieldBoolean
    ' = new FormFieldBoolean(1177, 898);
    Public CF_11_Insureds_OtherHealthPlan_No As FormFieldBoolean
    ' = new FormFieldBoolean(1278, 898);
    Public CF_12_PatientAuthorizedPersons_Signature As FormFieldString
    ' = new FormFieldString(286, 1032);
    Public CF_12_PatientAuthorizedPersons_Signature_Date As FormFieldString
    ' = new FormFieldString(869, 1035);
    Public CF_13_InsuredsAuthorizedPersons_Signature As FormFieldString
    ' = new FormFieldString(1259, 1034);
    Public CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM As FormFieldString
    ' = new FormFieldString(189, 1108);
    Public CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD As FormFieldString
    ' = new FormFieldString(248, 1108);
    Public CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY As FormFieldString
    ' = new FormFieldString(322, 1108);
    Public CF_15_FirstDateOfSimilar_Illness_MM As FormFieldString
    ' = new FormFieldString(884, 1106);
    Public CF_15_FirstDateOfSimilar_Illness_DD As FormFieldString
    ' = new FormFieldString(948, 1106);
    Public CF_15_FirstDateOfSimilar_Illness_YY As FormFieldString
    ' = new FormFieldString(1022, 1106);
    Public CF_16_UnableToWorkFromDate_MM As FormFieldString
    ' = new FormFieldString(1221, 1106);
    Public CF_16_UnableToWorkFromDate_DD As FormFieldString
    ' = new FormFieldString(1284, 1106);
    Public CF_16_UnableToWorkFromDate_YY As FormFieldString
    ' = new FormFieldString(1362, 1106);
    Public CF_16_UnableToWorkTillDate_MM As FormFieldString
    ' = new FormFieldString(1500, 1106);
    Public CF_16_UnableToWorkTillDate_DD As FormFieldString
    ' = new FormFieldString(1563, 1106);
    Public CF_16_UnableToWorkTillDate_YY As FormFieldString
    ' = new FormFieldString(1641, 1106);
    Public CF_17_ReferringProvider_Name As FormFieldString
    ' = new FormFieldString(169, 1162);
    Public CF_17a_ReferringProvider_UnknownField As FormFieldString
    ' = new FormFieldString(796, 1142);
    Public CF_17b_ReferringProvider_NPI As FormFieldString
    ' = new FormFieldString(796, 1173);
    Public CF_18_HospitalizationFromDate_MM As FormFieldString
    ' = new FormFieldString(1223, 1170);
    Public CF_18_HospitalizationFromDate_DD As FormFieldString
    ' = new FormFieldString(1286, 1170);
    Public CF_18_HospitalizationFromDate_YY As FormFieldString
    ' = new FormFieldString(1364, 1170);
    Public CF_18_HospitalizationTillDate_MM As FormFieldString
    ' = new FormFieldString(1501, 1171);
    Public CF_18_HospitalizationTillDate_DD As FormFieldString
    ' = new FormFieldString(1564, 1170);
    Public CF_18_HospitalizationTillDate_YY As FormFieldString
    ' = new FormFieldString(1643, 1170);
    Public CF_19_LocalUse_Field As FormFieldString
    ' = new FormFieldString(169, 1228);
    Public CF_20_OutsideLab_Yes As FormFieldBoolean
    ' = new FormFieldBoolean(1179, 1230);
    Public CF_20_OutsideLab_No As FormFieldBoolean
    ' = new FormFieldBoolean(1280, 1230);
    Public CF_20_OutsideLab_Charges_Principal As FormFieldString
    ' = new FormFieldString(1466, 1235);
    Public CF_20_OutsideLab_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1571, 1235);
    Public CF_21_Diagnosis_1_Principal As FormFieldString
    ' = new FormFieldString(205, 1308);
    Public CF_21_Diagnosis_1_Secondary As FormFieldString
    ' = new FormFieldString(284, 1308);
    Public CF_21_Diagnosis_2_Principal As FormFieldString
    ' = new FormFieldString(204, 1374);
    Public CF_21_Diagnosis_2_Secondary As FormFieldString
    ' = new FormFieldString(283, 1374);
    Public CF_21_Diagnosis_3_Principal As FormFieldString
    ' = new FormFieldString(744, 1308);
    Public CF_21_Diagnosis_3_Secondary As FormFieldString
    ' = new FormFieldString(823, 1308);
    Public CF_21_Diagnosis_4_Principal As FormFieldString
    ' = new FormFieldString(744, 1374);
    Public CF_21_Diagnosis_4_Secondary As FormFieldString
    ' = new FormFieldString(823, 1374);
    Public CF_22_MecaidResubmission_Code As FormFieldString
    ' = new FormFieldString(1150, 1307);
    Public CF_22_Original_Refrence_No As FormFieldString
    ' = new FormFieldString(1393, 1307);
    Public CF_23_PriorAuthorization_No As FormFieldString
    ' = new FormFieldString(1150, 1364);
#Region " Service Line 1 "

    Public CF_IsPresent_Line1 As Boolean = False

    'From Date.
    Public CF_24A_L1_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1505);
    Public CF_24A_L1_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1505);
    Public CF_24A_L1_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1505);
    'To Date
    Public CF_24A_L1_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1505);
    Public CF_24A_L1_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1505);
    Public CF_24A_L1_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1505);
    'Place Of Service
    Public CF_24B_L1_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1505);
    'EMG - Emergency
    Public CF_24C_L1_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1504);
    'CPT/HCPCS 
    Public CF_24D_L1_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1505);
    'Modifiers
    Public CF_24D_L1_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1505);
    Public CF_24D_L1_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1505);
    Public CF_24D_L1_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1505);
    Public CF_24D_L1_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1505);
    'Diagnosis Pointers 
    Public CF_24E_L1_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1505);
    'Charges
    Public CF_24F_L1_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1505);
    Public CF_24F_L1_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1505);
    'Days or Units
    Public CF_24G_L1_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1505);
    'EPSDT Family Plan
    Public CF_24H_L1_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1505);
    'Rendering Provider ID (NPI)
    Public CF_24J_L1_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1505);
    Public CF_24J_L1_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L1_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    Public CF_24A_L1_Note As FormFieldString
    ' = new FormFieldString(169, 1472);
#End Region

#Region " Service Line 2 "

    Public CF_IsPresent_Line2 As Boolean = False

    'From Date.
    Public CF_24A_L2_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1573);
    Public CF_24A_L2_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1573);
    Public CF_24A_L2_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1573);
    'To Date
    Public CF_24A_L2_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1573);
    Public CF_24A_L2_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1573);
    Public CF_24A_L2_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1573);
    'Place Of Service
    Public CF_24B_L2_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1573);
    'EMG - Emergency
    Public CF_24C_L2_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1572);
    'CPT/HCPCS 
    Public CF_24D_L2_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1573);
    'Modifiers
    Public CF_24D_L2_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1573);
    Public CF_24D_L2_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1573);
    Public CF_24D_L2_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1573);
    Public CF_24D_L2_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1573);
    'Diagnosis Pointers 
    Public CF_24E_L2_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1573);
    'Charges
    Public CF_24F_L2_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1573);
    Public CF_24F_L2_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1573);
    'Days or Units
    Public CF_24G_L2_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1573);
    'EPSDT Family Plan
    Public CF_24H_L2_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1573);
    'Rendering Provider ID (NPI)
    Public CF_24J_L2_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1573);
    Public CF_24J_L2_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L2_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    Public CF_24A_L2_Note As FormFieldString
    ' = new FormFieldString(167, 1537);
#End Region

#Region " Service Line 3 "

    Public CF_IsPresent_Line3 As Boolean = False

    'From Date.
    Public CF_24A_L3_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1638);
    Public CF_24A_L3_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1638);
    Public CF_24A_L3_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1638);
    'To Date
    Public CF_24A_L3_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1638);
    Public CF_24A_L3_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1638);
    Public CF_24A_L3_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1638);
    'Place Of Service
    Public CF_24B_L3_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1638);
    'EMG - Emergency
    Public CF_24C_L3_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1637);
    'CPT/HCPCS 
    Public CF_24D_L3_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1638);
    'Modifiers
    Public CF_24D_L3_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1638);
    Public CF_24D_L3_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1638);
    Public CF_24D_L3_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1638);
    Public CF_24D_L3_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1638);
    'Diagnosis Pointers 
    Public CF_24E_L3_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1638);
    'Charges
    Public CF_24F_L3_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1638);
    Public CF_24F_L3_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1638);
    'Days or Units
    Public CF_24G_L3_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1638);
    'EPSDT Family Plan
    Public CF_24H_L3_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1638);
    'Rendering Provider ID (NPI)
    Public CF_24J_L3_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1638);
    Public CF_24J_L3_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L3_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    'Line Note
    Public CF_24A_L3_Note As FormFieldString
    ' = new FormFieldString(167, 1604);
#End Region

#Region " Service Line 4 "

    Public CF_IsPresent_Line4 As Boolean = False

    'From Date.
    Public CF_24A_L4_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1704);
    Public CF_24A_L4_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1704);
    Public CF_24A_L4_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1704);
    'To Date
    Public CF_24A_L4_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1704);
    Public CF_24A_L4_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1704);
    Public CF_24A_L4_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1704);
    'Place Of Service
    Public CF_24B_L4_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1704);
    'EMG - Emergency
    Public CF_24C_L4_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1703);
    'CPT/HCPCS 
    Public CF_24D_L4_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1704);
    'Modifiers
    Public CF_24D_L4_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1704);
    Public CF_24D_L4_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1704);
    Public CF_24D_L4_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1704);
    Public CF_24D_L4_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1704);
    'Diagnosis Pointers 
    Public CF_24E_L4_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1704);
    'Charges
    Public CF_24F_L4_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1704);
    Public CF_24F_L4_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1704);
    'Days or Units
    Public CF_24G_L4_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1704);
    'EPSDT Family Plan
    Public CF_24H_L4_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1704);
    'Rendering Provider ID (NPI)
    Public CF_24J_L4_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1704);
    Public CF_24J_L4_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L4_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    'Line Note
    Public CF_24A_L4_Note As FormFieldString
    ' = new FormFieldString(167, 1674);
#End Region

#Region " Service Line 5 "

    Public CF_IsPresent_Line5 As Boolean = False

    'From Date.
    Public CF_24A_L5_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1773);
    Public CF_24A_L5_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1773);
    Public CF_24A_L5_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1773);
    'To Date
    Public CF_24A_L5_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1773);
    Public CF_24A_L5_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1773);
    Public CF_24A_L5_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1773);
    'Place Of Service
    Public CF_24B_L5_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1773);
    'EMG - Emergency
    Public CF_24C_L5_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1772);
    'CPT/HCPCS 
    Public CF_24D_L5_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1773);
    'Modifiers
    Public CF_24D_L5_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1773);
    Public CF_24D_L5_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1773);
    Public CF_24D_L5_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1773);
    Public CF_24D_L5_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1773);
    'Diagnosis Pointers 
    Public CF_24E_L5_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1773);
    'Charges
    Public CF_24F_L5_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1773);
    Public CF_24F_L5_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1773);
    'Days or Units
    Public CF_24G_L5_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1773);
    'EPSDT Family Plan
    Public CF_24H_L5_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1773);
    'Rendering Provider ID (NPI)
    Public CF_24J_L5_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1773);
    Public CF_24J_L5_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L5_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    'Line Note
    Public CF_24A_L5_Note As FormFieldString
    ' = new FormFieldString(167, 1739);
#End Region

#Region " Service Line 6 "

    Public CF_IsPresent_Line6 As Boolean = False

    'From Date.
    Public CF_24A_L6_DOS_From_MM As FormFieldString
    ' = new FormFieldString(167, 1838);
    Public CF_24A_L6_DOS_From_DD As FormFieldString
    ' = new FormFieldString(225, 1838);
    Public CF_24A_L6_DOS_From_YY As FormFieldString
    ' = new FormFieldString(283, 1838);
    'To Date
    Public CF_24A_L6_DOS_To_MM As FormFieldString
    ' = new FormFieldString(343, 1838);
    Public CF_24A_L6_DOS_To_DD As FormFieldString
    ' = new FormFieldString(401, 1838);
    Public CF_24A_L6_DOS_To_YY As FormFieldString
    ' = new FormFieldString(464, 1838);
    'Place Of Service
    Public CF_24B_L6_POS_Code As FormFieldString
    ' = new FormFieldString(525, 1838);
    'EMG - Emergency
    Public CF_24C_L6_EMG_Code As FormFieldString
    ' = new FormFieldString(584, 1837);
    'CPT/HCPCS 
    Public CF_24D_L6_CPT_HCPCS_Code As FormFieldString
    ' = new FormFieldString(662, 1838);
    'Modifiers
    Public CF_24D_L6_Modifier_1_Code As FormFieldString
    ' = new FormFieldString(803, 1838);
    Public CF_24D_L6_Modifier_2_Code As FormFieldString
    ' = new FormFieldString(862, 1838);
    Public CF_24D_L6_Modifier_3_Code As FormFieldString
    ' = new FormFieldString(922, 1838);
    Public CF_24D_L6_Modifier_4_Code As FormFieldString
    ' = new FormFieldString(983, 1838);
    'Diagnosis Pointers 
    Public CF_24E_L6_Diagnosis_Pointers As FormFieldString
    ' = new FormFieldString(1047, 1838);
    'Charges
    Public CF_24F_L6_Charges_Principal As FormFieldString
    ' = new FormFieldString(1161, 1838);
    Public CF_24F_L6_Charges_Secondary As FormFieldString
    ' = new FormFieldString(1269, 1838);
    'Days or Units
    Public CF_24G_L6_Days_Units As FormFieldString
    ' = new FormFieldString(1328, 1838);
    'EPSDT Family Plan
    Public CF_24H_L6_EPSDT_FamilyPlan As FormFieldString
    ' = new FormFieldString(1403, 1838);
    'Rendering Provider ID (NPI)
    Public CF_24J_L6_RenderingProvider_NPI As FormFieldString
    ' = new FormFieldString(1507, 1838);
    Public CF_24J_L6_RenderingProvider_OthrQualifier As FormFieldString

    Public CF_24J_L6_RenderingProvider_OthrQualifierValue As FormFieldString
    'Line Note
    'Line Note
    Public CF_24A_L6_Note As FormFieldString
    ' = new FormFieldString(167, 1802);
#End Region

    Public CF_25_FederalTax_ID_No As FormFieldString
    ' = new FormFieldString(167, 1895);
    Public CF_25_FederalTaxID_Qualifier_SSN As FormFieldBoolean
    ' = new FormFieldBoolean(482, 1895);
    Public CF_25_FederalTaxID_Qualifier_EIN As FormFieldBoolean
    ' = new FormFieldBoolean(522, 1895);
    Public CF_26_PatientAccount_No As FormFieldString
    ' = new FormFieldString(615, 1895);
    Public CF_27_AcceptAssignment_YES As FormFieldBoolean
    ' = new FormFieldBoolean(901, 1895);
    Public CF_27_AcceptAssignment_NO As FormFieldBoolean
    ' = new FormFieldBoolean(1001, 1895);
    Public CF_28_TotalCharge_Principal As FormFieldString
    ' = new FormFieldString(1171, 1901);
    Public CF_28_TotalCharge_Secondary As FormFieldString
    ' = new FormFieldString(1314, 1901);
    Public CF_29_AmountPaid_Principal As FormFieldString
    ' = new FormFieldString(1392, 1901);
    Public CF_29_AmountPaid_Secondary As FormFieldString
    ' = new FormFieldString(1511, 1901);
    Public CF_30_BalanceDue_Principal As FormFieldString
    ' = new FormFieldString(1573, 1901);
    Public CF_30_BalanceDue_Secondary As FormFieldString
    ' = new FormFieldString(1692, 1901);
    Public CF_31_Physician_Supplier_Signature As FormFieldString
    ' = new FormFieldString(167, 2054);
    Public CF_31_Physician_Supplier_Signature_Date As FormFieldString
    ' = new FormFieldString(450, 2050); //(519, 2078);
    Public CF_31_Physician_Supplier_QualifierValue As FormFieldString
    Public CF_32_Service_Facility_Name As FormFieldString
    ' = new FormFieldString(620, 1964);
    Public CF_32_Service_Facility_Address_Line1 As FormFieldString
    ' = new FormFieldString(620, 1991);
    Public CF_32_Service_Facility_Address_Line2 As FormFieldString
    ' = new FormFieldString(620, 2019);
    Public CF_32_Service_Facility_City As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_32_Service_Facility_State As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_32_Service_Facility_Zip As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_32a_Service_Facility_NPI As FormFieldString
    ' = new FormFieldString(620, 2068);
    Public CF_32b_Service_Facility_UPIN_OtherID As FormFieldString
    ' = new FormFieldString(844, 2068);
    Public CF_33_BillingProvider_Name As FormFieldString
    ' = new FormFieldString(1148, 1964);
    Public CF_33_BillingProvider_Address_Line1 As FormFieldString
    ' = new FormFieldString(1148, 1991);
    Public CF_33_BillingProvider_Address_Line2 As FormFieldString
    ' = new FormFieldString(1148, 2019);
    Public CF_33_BillingProvider_City As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_33_BillingProvider_State As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_33_BillingProvider_Zip As FormFieldString
    ' = new FormFieldString(3, 2);
    Public CF_33a_BillingProvider_NPI As FormFieldString
    ' = new FormFieldString(1161, 2068);
    Public CF_33b_BillingProvider_UPIN_OtherID As FormFieldString
    ' = new FormFieldString(1385, 2068);
    Public CF_33_BillingProvider_Tel_AreaCode As FormFieldString
    ' = new FormFieldString(1472, 1938);
    Public CF_33_BillingProvider_Tel_Number As FormFieldString
    ' = new FormFieldString(1543, 1938); 
#End Region
#Region " Private Method "
    Private Sub InitializeBoxes()
        Dim X As Int32 = 0
        Dim Y As Int32 = 0


        '.Insurance Header
        GetCoordinates("CF_Top_InsuranceHeader", X, Y)
        CF_Top_InsuranceHeader = New FormFieldString(X, Y)

        '.Insurance Type
        GetCoordinates("CF_1_Insuracne_Type_Medicare", X, Y)
        CF_1_Insuracne_Type_Medicare = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_Medicaid", X, Y)
        CF_1_Insuracne_Type_Medicaid = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_Tricare", X, Y)
        CF_1_Insuracne_Type_Tricare = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_Champva", X, Y)
        CF_1_Insuracne_Type_Champva = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_GroupHealthPlan", X, Y)
        CF_1_Insuracne_Type_GroupHealthPlan = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_FECA", X, Y)
        CF_1_Insuracne_Type_FECA = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_1_Insuracne_Type_Other", X, Y)
        CF_1_Insuracne_Type_Other = New FormFieldBoolean(X, Y)

        '.Insured's ID Number
        GetCoordinates("CF_1a_InsuredsIDNumber", X, Y)
        CF_1a_InsuredsIDNumber = New FormFieldString(X, Y)

        '.Patient's Name
        'public FormFieldString CF_2_Patient_Name = new FormFieldString(30, 300);
        GetCoordinates("CF_2_Patient_Name", X, Y)
        CF_2_Patient_Name = New FormFieldString(X, Y)

        '.Patient's Birth Date
        GetCoordinates("CF_3_Patient_DOB_MM", X, Y)
        CF_3_Patient_DOB_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_3_Patient_DOB_DD", X, Y)
        CF_3_Patient_DOB_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_3_Patient_DOB_YY", X, Y)
        CF_3_Patient_DOB_YY = New FormFieldString(X, Y)

        '.Patient's Sex
        GetCoordinates("CF_3_Patient_Sex_Male", X, Y)
        CF_3_Patient_Sex_Male = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_3_Patient_Sex_Female", X, Y)
        CF_3_Patient_Sex_Female = New FormFieldBoolean(X, Y)


        '.Insured's Name
        GetCoordinates("CF_4_Insureds_Name", X, Y)
        CF_4_Insureds_Name = New FormFieldString(X, Y)

        '.Patient's Address
        GetCoordinates("CF_5_Patient_Address", X, Y)
        CF_5_Patient_Address = New FormFieldString(X, Y)
        GetCoordinates("CF_5_Patient_City", X, Y)
        CF_5_Patient_City = New FormFieldString(X, Y)
        GetCoordinates("CF_5_Patient_State", X, Y)
        CF_5_Patient_State = New FormFieldString(X, Y)
        GetCoordinates("CF_5_Patient_Zip", X, Y)
        CF_5_Patient_Zip = New FormFieldString(X, Y)
        GetCoordinates("CF_5_Patient_Tel_AreaCode", X, Y)
        CF_5_Patient_Tel_AreaCode = New FormFieldString(X, Y)
        GetCoordinates("CF_5_Patient_Tel_Number", X, Y)
        CF_5_Patient_Tel_Number = New FormFieldString(X, Y)

        '.Paient Relationship to Insured
        GetCoordinates("CF_6_PatientRelationship_Self", X, Y)
        CF_6_PatientRelationship_Self = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_6_PatientRelationship_Spouse", X, Y)
        CF_6_PatientRelationship_Spouse = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_6_PatientRelationship_Child", X, Y)
        CF_6_PatientRelationship_Child = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_6_PatientRelationship_Other", X, Y)
        CF_6_PatientRelationship_Other = New FormFieldBoolean(X, Y)

        '.Insured's Address
        GetCoordinates("CF_7_Insureds_Address", X, Y)
        CF_7_Insureds_Address = New FormFieldString(X, Y)
        GetCoordinates("CF_7_Insureds_City", X, Y)
        CF_7_Insureds_City = New FormFieldString(X, Y)
        GetCoordinates("CF_7_Insureds_State", X, Y)
        CF_7_Insureds_State = New FormFieldString(X, Y)
        GetCoordinates("CF_7_Insureds_Zip", X, Y)
        CF_7_Insureds_Zip = New FormFieldString(X, Y)
        GetCoordinates("CF_7_Insureds_Tel_AreaCode", X, Y)
        CF_7_Insureds_Tel_AreaCode = New FormFieldString(X, Y)
        GetCoordinates("CF_7_Insureds_Tel_Number", X, Y)
        CF_7_Insureds_Tel_Number = New FormFieldString(X, Y)

        '.Patient Status
        GetCoordinates("CF_8_PatientStatus_Single", X, Y)
        CF_8_PatientStatus_Single = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_8_PatientStatus_Married", X, Y)
        CF_8_PatientStatus_Married = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_8_PatientStatus_Other", X, Y)
        CF_8_PatientStatus_Other = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_8_PatientStatus_Employed", X, Y)
        CF_8_PatientStatus_Employed = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_8_PatientStatus_FullTimeStudent", X, Y)
        CF_8_PatientStatus_FullTimeStudent = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_8_PatientStatus_PartTimeStudent", X, Y)
        CF_8_PatientStatus_PartTimeStudent = New FormFieldBoolean(X, Y)

        '.Other Insured's Name
        GetCoordinates("CF_9_Other_Insureds_Name", X, Y)
        CF_9_Other_Insureds_Name = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_PolicyGroupNo", X, Y)
        CF_9_Other_Insureds_PolicyGroupNo = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_DOB_MM", X, Y)
        CF_9_Other_Insureds_DOB_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_DOB_DD", X, Y)
        CF_9_Other_Insureds_DOB_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_DOB_YY", X, Y)
        CF_9_Other_Insureds_DOB_YY = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_Sex_Male", X, Y)
        CF_9_Other_Insureds_Sex_Male = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_9_Other_Insureds_Sex_Female", X, Y)
        CF_9_Other_Insureds_Sex_Female = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_9_Other_Insureds_EmployerName", X, Y)
        CF_9_Other_Insureds_EmployerName = New FormFieldString(X, Y)
        GetCoordinates("CF_9_Other_Insureds_InsuracnePlan", X, Y)
        CF_9_Other_Insureds_InsuracnePlan = New FormFieldString(X, Y)

        '.Patient Condition 
        GetCoordinates("CF_10_PatientConditionTo_Employement_Yes", X, Y)
        CF_10_PatientConditionTo_Employement_Yes = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_Employement_No", X, Y)
        CF_10_PatientConditionTo_Employement_No = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_AutoAccident_Yes", X, Y)
        CF_10_PatientConditionTo_AutoAccident_Yes = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_AutoAccident_No", X, Y)
        CF_10_PatientConditionTo_AutoAccident_No = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_AutoAccident_State", X, Y)
        CF_10_PatientConditionTo_AutoAccident_State = New FormFieldString(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_OtherAccident_Yes", X, Y)
        CF_10_PatientConditionTo_OtherAccident_Yes = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_OtherAccident_No", X, Y)
        CF_10_PatientConditionTo_OtherAccident_No = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_10_PatientConditionTo_ResForLocaluse", X, Y)
        CF_10_PatientConditionTo_ResForLocaluse = New FormFieldString(X, Y)

        '.Insured's Information
        GetCoordinates("CF_11_Insureds_PolicyGroupNo", X, Y)
        CF_11_Insureds_PolicyGroupNo = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_DOB_MM", X, Y)
        CF_11_Insureds_DOB_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_DOB_DD", X, Y)
        CF_11_Insureds_DOB_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_DOB_YY", X, Y)
        CF_11_Insureds_DOB_YY = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_Sex_Male", X, Y)
        CF_11_Insureds_Sex_Male = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_11_Insureds_Sex_Female", X, Y)
        CF_11_Insureds_Sex_Female = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_11_Insureds_EmployerName", X, Y)
        CF_11_Insureds_EmployerName = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_InsuracnePlan", X, Y)
        CF_11_Insureds_InsuracnePlan = New FormFieldString(X, Y)
        GetCoordinates("CF_11_Insureds_OtherHealthPlan_Yes", X, Y)
        CF_11_Insureds_OtherHealthPlan_Yes = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_11_Insureds_OtherHealthPlan_No", X, Y)
        CF_11_Insureds_OtherHealthPlan_No = New FormFieldBoolean(X, Y)

        GetCoordinates("CF_12_PatientAuthorizedPersons_Signature", X, Y)
        CF_12_PatientAuthorizedPersons_Signature = New FormFieldString(X, Y)
        GetCoordinates("CF_12_PatientAuthorizedPersons_Signature_Date", X, Y)
        CF_12_PatientAuthorizedPersons_Signature_Date = New FormFieldString(X, Y)

        GetCoordinates("CF_13_InsuredsAuthorizedPersons_Signature", X, Y)
        CF_13_InsuredsAuthorizedPersons_Signature = New FormFieldString(X, Y)

        GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM", X, Y)
        CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD", X, Y)
        CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY", X, Y)
        CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_15_FirstDateOfSimilar_Illness_MM", X, Y)
        CF_15_FirstDateOfSimilar_Illness_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_15_FirstDateOfSimilar_Illness_DD", X, Y)
        CF_15_FirstDateOfSimilar_Illness_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_15_FirstDateOfSimilar_Illness_YY", X, Y)
        CF_15_FirstDateOfSimilar_Illness_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_16_UnableToWorkFromDate_MM", X, Y)
        CF_16_UnableToWorkFromDate_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_16_UnableToWorkFromDate_DD", X, Y)
        CF_16_UnableToWorkFromDate_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_16_UnableToWorkFromDate_YY", X, Y)
        CF_16_UnableToWorkFromDate_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_16_UnableToWorkTillDate_MM", X, Y)
        CF_16_UnableToWorkTillDate_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_16_UnableToWorkTillDate_DD", X, Y)
        CF_16_UnableToWorkTillDate_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_16_UnableToWorkTillDate_YY", X, Y)
        CF_16_UnableToWorkTillDate_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_17_ReferringProvider_Name", X, Y)
        CF_17_ReferringProvider_Name = New FormFieldString(X, Y)
        GetCoordinates("CF_17a_ReferringProvider_UnknownField", X, Y)
        CF_17a_ReferringProvider_UnknownField = New FormFieldString(X, Y)
        GetCoordinates("CF_17b_ReferringProvider_NPI", X, Y)
        CF_17b_ReferringProvider_NPI = New FormFieldString(X, Y)

        GetCoordinates("CF_18_HospitalizationFromDate_MM", X, Y)
        CF_18_HospitalizationFromDate_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_18_HospitalizationFromDate_DD", X, Y)
        CF_18_HospitalizationFromDate_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_18_HospitalizationFromDate_YY", X, Y)
        CF_18_HospitalizationFromDate_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_18_HospitalizationTillDate_MM", X, Y)
        CF_18_HospitalizationTillDate_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_18_HospitalizationTillDate_DD", X, Y)
        CF_18_HospitalizationTillDate_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_18_HospitalizationTillDate_YY", X, Y)
        CF_18_HospitalizationTillDate_YY = New FormFieldString(X, Y)

        GetCoordinates("CF_19_LocalUse_Field", X, Y)
        CF_19_LocalUse_Field = New FormFieldString(X, Y)

        GetCoordinates("CF_20_OutsideLab_Yes", X, Y)
        CF_20_OutsideLab_Yes = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_20_OutsideLab_No", X, Y)
        CF_20_OutsideLab_No = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_20_OutsideLab_Charges_Principal", X, Y)
        CF_20_OutsideLab_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_20_OutsideLab_Charges_Secondary", X, Y)
        CF_20_OutsideLab_Charges_Secondary = New FormFieldString(X, Y)

        GetCoordinates("CF_21_Diagnosis_1_Principal", X, Y)
        CF_21_Diagnosis_1_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_1_Secondary", X, Y)
        CF_21_Diagnosis_1_Secondary = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_2_Principal", X, Y)
        CF_21_Diagnosis_2_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_2_Secondary", X, Y)
        CF_21_Diagnosis_2_Secondary = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_3_Principal", X, Y)
        CF_21_Diagnosis_3_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_3_Secondary", X, Y)
        CF_21_Diagnosis_3_Secondary = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_4_Principal", X, Y)
        CF_21_Diagnosis_4_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_21_Diagnosis_4_Secondary", X, Y)
        CF_21_Diagnosis_4_Secondary = New FormFieldString(X, Y)

        GetCoordinates("CF_22_MecaidResubmission_Code", X, Y)
        CF_22_MecaidResubmission_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_22_Original_Refrence_No", X, Y)
        CF_22_Original_Refrence_No = New FormFieldString(X, Y)

        GetCoordinates("CF_23_PriorAuthorization_No", X, Y)
        CF_23_PriorAuthorization_No = New FormFieldString(X, Y)

        '#Region " Service Line 1 "

        Dim CF_IsPresent_Line1 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L1_DOS_From_MM", X, Y)
        CF_24A_L1_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L1_DOS_From_DD", X, Y)
        CF_24A_L1_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L1_DOS_From_YY", X, Y)
        CF_24A_L1_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L1_DOS_To_MM", X, Y)
        CF_24A_L1_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L1_DOS_To_DD", X, Y)
        CF_24A_L1_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L1_DOS_To_YY", X, Y)
        CF_24A_L1_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L1_POS_Code", X, Y)
        CF_24B_L1_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L1_EMG_Code", X, Y)
        CF_24C_L1_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L1_CPT_HCPCS_Code", X, Y)
        CF_24D_L1_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L1_Modifier_1_Code", X, Y)
        CF_24D_L1_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L1_Modifier_2_Code", X, Y)
        CF_24D_L1_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L1_Modifier_3_Code", X, Y)
        CF_24D_L1_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L1_Modifier_4_Code", X, Y)
        CF_24D_L1_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L1_Diagnosis_Pointers", X, Y)
        CF_24E_L1_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L1_Charges_Principal", X, Y)
        CF_24F_L1_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L1_Charges_Secondary", X, Y)
        CF_24F_L1_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L1_Days_Units", X, Y)
        CF_24G_L1_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L1_EPSDT_FamilyPlan", X, Y)
        CF_24H_L1_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L1_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L1_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L1_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L1_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L1_RenderingProvider_NPI", X, Y)
        CF_24J_L1_RenderingProvider_NPI = New FormFieldString(X, Y)

        'Line Note
        GetCoordinates("CF_24A_L1_Note", X, Y)
        CF_24A_L1_Note = New FormFieldString(X, Y)

        '#End Region

        '#Region " Service Line 2 "

        Dim CF_IsPresent_Line2 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L2_DOS_From_MM", X, Y)
        CF_24A_L2_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L2_DOS_From_DD", X, Y)
        CF_24A_L2_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L2_DOS_From_YY", X, Y)
        CF_24A_L2_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L2_DOS_To_MM", X, Y)
        CF_24A_L2_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L2_DOS_To_DD", X, Y)
        CF_24A_L2_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L2_DOS_To_YY", X, Y)
        CF_24A_L2_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L2_POS_Code", X, Y)
        CF_24B_L2_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L2_EMG_Code", X, Y)
        CF_24C_L2_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L2_CPT_HCPCS_Code", X, Y)
        CF_24D_L2_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L2_Modifier_1_Code", X, Y)
        CF_24D_L2_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L2_Modifier_2_Code", X, Y)
        CF_24D_L2_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L2_Modifier_3_Code", X, Y)
        CF_24D_L2_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L2_Modifier_4_Code", X, Y)
        CF_24D_L2_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L2_Diagnosis_Pointers", X, Y)
        CF_24E_L2_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L2_Charges_Principal", X, Y)
        CF_24F_L2_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L2_Charges_Secondary", X, Y)
        CF_24F_L2_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L2_Days_Units", X, Y)
        CF_24G_L2_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L2_EPSDT_FamilyPlan", X, Y)
        CF_24H_L2_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L2_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L2_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L2_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L2_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L2_RenderingProvider_NPI", X, Y)
        CF_24J_L2_RenderingProvider_NPI = New FormFieldString(X, Y)

        'Line Note
        GetCoordinates("CF_24A_L2_Note", X, Y)
        CF_24A_L2_Note = New FormFieldString(X, Y)

        '#End Region

        '#Region " Service Line 3 "

        Dim CF_IsPresent_Line3 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L3_DOS_From_MM", X, Y)
        CF_24A_L3_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L3_DOS_From_DD", X, Y)
        CF_24A_L3_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L3_DOS_From_YY", X, Y)
        CF_24A_L3_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L3_DOS_To_MM", X, Y)
        CF_24A_L3_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L3_DOS_To_DD", X, Y)
        CF_24A_L3_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L3_DOS_To_YY", X, Y)
        CF_24A_L3_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L3_POS_Code", X, Y)
        CF_24B_L3_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L3_EMG_Code", X, Y)
        CF_24C_L3_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L3_CPT_HCPCS_Code", X, Y)
        CF_24D_L3_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L3_Modifier_1_Code", X, Y)
        CF_24D_L3_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L3_Modifier_2_Code", X, Y)
        CF_24D_L3_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L3_Modifier_3_Code", X, Y)
        CF_24D_L3_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L3_Modifier_4_Code", X, Y)
        CF_24D_L3_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L3_Diagnosis_Pointers", X, Y)
        CF_24E_L3_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L3_Charges_Principal", X, Y)
        CF_24F_L3_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L3_Charges_Secondary", X, Y)
        CF_24F_L3_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L3_Days_Units", X, Y)
        CF_24G_L3_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L3_EPSDT_FamilyPlan", X, Y)
        CF_24H_L3_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L3_RenderingProvider_NPI", X, Y)
        CF_24J_L3_RenderingProvider_NPI = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L3_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L3_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L3_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L3_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Line Note
        GetCoordinates("CF_24A_L3_Note", X, Y)
        CF_24A_L3_Note = New FormFieldString(X, Y)

        '#End Region

        '#Region " Service Line 4 "

        Dim CF_IsPresent_Line4 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L4_DOS_From_MM", X, Y)
        CF_24A_L4_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L4_DOS_From_DD", X, Y)
        CF_24A_L4_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L4_DOS_From_YY", X, Y)
        CF_24A_L4_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L4_DOS_To_MM", X, Y)
        CF_24A_L4_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L4_DOS_To_DD", X, Y)
        CF_24A_L4_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L4_DOS_To_YY", X, Y)
        CF_24A_L4_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L4_POS_Code", X, Y)
        CF_24B_L4_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L4_EMG_Code", X, Y)
        CF_24C_L4_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L4_CPT_HCPCS_Code", X, Y)
        CF_24D_L4_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L4_Modifier_1_Code", X, Y)
        CF_24D_L4_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L4_Modifier_2_Code", X, Y)
        CF_24D_L4_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L4_Modifier_3_Code", X, Y)
        CF_24D_L4_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L4_Modifier_4_Code", X, Y)
        CF_24D_L4_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L4_Diagnosis_Pointers", X, Y)
        CF_24E_L4_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L4_Charges_Principal", X, Y)
        CF_24F_L4_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L4_Charges_Secondary", X, Y)
        CF_24F_L4_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L4_Days_Units", X, Y)
        CF_24G_L4_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L4_EPSDT_FamilyPlan", X, Y)
        CF_24H_L4_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L4_RenderingProvider_NPI", X, Y)
        CF_24J_L4_RenderingProvider_NPI = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L4_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L4_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L4_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L4_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Line Note
        GetCoordinates("CF_24A_L4_Note", X, Y)
        CF_24A_L4_Note = New FormFieldString(X, Y)

        '#End Region

        '#Region " Service Line 5 "

        Dim CF_IsPresent_Line5 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L5_DOS_From_MM", X, Y)
        CF_24A_L5_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L5_DOS_From_DD", X, Y)
        CF_24A_L5_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L5_DOS_From_YY", X, Y)
        CF_24A_L5_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L5_DOS_To_MM", X, Y)
        CF_24A_L5_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L5_DOS_To_DD", X, Y)
        CF_24A_L5_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L5_DOS_To_YY", X, Y)
        CF_24A_L5_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L5_POS_Code", X, Y)
        CF_24B_L5_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L5_EMG_Code", X, Y)
        CF_24C_L5_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L5_CPT_HCPCS_Code", X, Y)
        CF_24D_L5_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L5_Modifier_1_Code", X, Y)
        CF_24D_L5_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L5_Modifier_2_Code", X, Y)
        CF_24D_L5_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L5_Modifier_3_Code", X, Y)
        CF_24D_L5_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L5_Modifier_4_Code", X, Y)
        CF_24D_L5_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L5_Diagnosis_Pointers", X, Y)
        CF_24E_L5_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L5_Charges_Principal", X, Y)
        CF_24F_L5_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L5_Charges_Secondary", X, Y)
        CF_24F_L5_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L5_Days_Units", X, Y)
        CF_24G_L5_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L5_EPSDT_FamilyPlan", X, Y)
        CF_24H_L5_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L5_RenderingProvider_NPI", X, Y)
        CF_24J_L5_RenderingProvider_NPI = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L5_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L5_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L5_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L5_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Line Note
        GetCoordinates("CF_24A_L5_Note", X, Y)
        CF_24A_L5_Note = New FormFieldString(X, Y)

        '#End Region

        '#Region " Service Line 6 "

        Dim CF_IsPresent_Line6 As Boolean = False

        'From Date.
        GetCoordinates("CF_24A_L6_DOS_From_MM", X, Y)
        CF_24A_L6_DOS_From_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L6_DOS_From_DD", X, Y)
        CF_24A_L6_DOS_From_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L6_DOS_From_YY", X, Y)
        CF_24A_L6_DOS_From_YY = New FormFieldString(X, Y)

        'To Date
        GetCoordinates("CF_24A_L6_DOS_To_MM", X, Y)
        CF_24A_L6_DOS_To_MM = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L6_DOS_To_DD", X, Y)
        CF_24A_L6_DOS_To_DD = New FormFieldString(X, Y)
        GetCoordinates("CF_24A_L6_DOS_To_YY", X, Y)
        CF_24A_L6_DOS_To_YY = New FormFieldString(X, Y)

        'Place Of Service
        GetCoordinates("CF_24B_L6_POS_Code", X, Y)
        CF_24B_L6_POS_Code = New FormFieldString(X, Y)

        'EMG - Emergency
        GetCoordinates("CF_24C_L6_EMG_Code", X, Y)
        CF_24C_L6_EMG_Code = New FormFieldString(X, Y)

        'CPT/HCPCS 
        GetCoordinates("CF_24D_L6_CPT_HCPCS_Code", X, Y)
        CF_24D_L6_CPT_HCPCS_Code = New FormFieldString(X, Y)

        'Modifiers
        GetCoordinates("CF_24D_L6_Modifier_1_Code", X, Y)
        CF_24D_L6_Modifier_1_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L6_Modifier_2_Code", X, Y)
        CF_24D_L6_Modifier_2_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L6_Modifier_3_Code", X, Y)
        CF_24D_L6_Modifier_3_Code = New FormFieldString(X, Y)
        GetCoordinates("CF_24D_L6_Modifier_4_Code", X, Y)
        CF_24D_L6_Modifier_4_Code = New FormFieldString(X, Y)

        'Diagnosis Pointers 
        GetCoordinates("CF_24E_L6_Diagnosis_Pointers", X, Y)
        CF_24E_L6_Diagnosis_Pointers = New FormFieldString(X, Y)

        'Charges
        GetCoordinates("CF_24F_L6_Charges_Principal", X, Y)
        CF_24F_L6_Charges_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_24F_L6_Charges_Secondary", X, Y)
        CF_24F_L6_Charges_Secondary = New FormFieldString(X, Y)

        'Days or Units
        GetCoordinates("CF_24G_L6_Days_Units", X, Y)
        CF_24G_L6_Days_Units = New FormFieldString(X, Y)

        'EPSDT Family Plan
        GetCoordinates("CF_24H_L6_EPSDT_FamilyPlan", X, Y)
        CF_24H_L6_EPSDT_FamilyPlan = New FormFieldString(X, Y)

        'Rendering Provider ID (NPI)
        GetCoordinates("CF_24J_L6_RenderingProvider_NPI", X, Y)
        CF_24J_L6_RenderingProvider_NPI = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L6_RenderingProvider_OtherQualifier", X, Y)
        CF_24J_L6_RenderingProvider_OthrQualifier = New FormFieldString(X, Y)

        GetCoordinates("CF_24J_L6_RenderingProvider_OtherQualifiervalue", X, Y)
        CF_24J_L6_RenderingProvider_OthrQualifierValue = New FormFieldString(X, Y)
        'Line Note
        GetCoordinates("CF_24A_L6_Note", X, Y)
        CF_24A_L6_Note = New FormFieldString(X, Y)

        '#End Region

        GetCoordinates("CF_25_FederalTax_ID_No", X, Y)
        CF_25_FederalTax_ID_No = New FormFieldString(X, Y)
        GetCoordinates("CF_25_FederalTaxID_Qualifier_SSN", X, Y)
        CF_25_FederalTaxID_Qualifier_SSN = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_25_FederalTaxID_Qualifier_EIN", X, Y)
        CF_25_FederalTaxID_Qualifier_EIN = New FormFieldBoolean(X, Y)

        GetCoordinates("CF_26_PatientAccount_No", X, Y)
        CF_26_PatientAccount_No = New FormFieldString(X, Y)

        GetCoordinates("CF_27_AcceptAssignment_YES", X, Y)
        CF_27_AcceptAssignment_YES = New FormFieldBoolean(X, Y)
        GetCoordinates("CF_27_AcceptAssignment_NO", X, Y)
        CF_27_AcceptAssignment_NO = New FormFieldBoolean(X, Y)

        GetCoordinates("CF_28_TotalCharge_Principal", X, Y)
        CF_28_TotalCharge_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_28_TotalCharge_Secondary", X, Y)
        CF_28_TotalCharge_Secondary = New FormFieldString(X, Y)

        GetCoordinates("CF_29_AmountPaid_Principal", X, Y)
        CF_29_AmountPaid_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_29_AmountPaid_Secondary", X, Y)
        CF_29_AmountPaid_Secondary = New FormFieldString(X, Y)

        GetCoordinates("CF_30_BalanceDue_Principal", X, Y)
        CF_30_BalanceDue_Principal = New FormFieldString(X, Y)
        GetCoordinates("CF_30_BalanceDue_Secondary", X, Y)
        CF_30_BalanceDue_Secondary = New FormFieldString(X, Y)

        GetCoordinates("CF_31_Physician_Supplier_Signature", X, Y)
        CF_31_Physician_Supplier_Signature = New FormFieldString(X, Y)
        GetCoordinates("CF_31_Physician_Supplier_Signature_Date", X, Y)
        CF_31_Physician_Supplier_Signature_Date = New FormFieldString(X, Y)
        GetCoordinates("CF_31_Physician_Supplier_QualifierValue", X, Y)
        CF_31_Physician_Supplier_QualifierValue = New FormFieldString(X, Y)

        '(519, 2078);
        GetCoordinates("CF_32_Service_Facility_Name", X, Y)
        CF_32_Service_Facility_Name = New FormFieldString(X, Y)
        GetCoordinates("CF_32_Service_Facility_Address_Line1", X, Y)
        CF_32_Service_Facility_Address_Line1 = New FormFieldString(X, Y)
        GetCoordinates("CF_32_Service_Facility_Address_Line2", X, Y)
        CF_32_Service_Facility_Address_Line2 = New FormFieldString(X, Y)
        GetCoordinates("CF_32_Service_Facility_City", X, Y)
        CF_32_Service_Facility_City = New FormFieldString(X, Y)
        GetCoordinates("CF_32_Service_Facility_State", X, Y)
        CF_32_Service_Facility_State = New FormFieldString(X, Y)
        GetCoordinates("CF_32_Service_Facility_Zip", X, Y)
        CF_32_Service_Facility_Zip = New FormFieldString(X, Y)
        GetCoordinates("CF_32a_Service_Facility_NPI", X, Y)
        CF_32a_Service_Facility_NPI = New FormFieldString(X, Y)
        GetCoordinates("CF_32b_Service_Facility_UPIN_OtherID", X, Y)
        CF_32b_Service_Facility_UPIN_OtherID = New FormFieldString(X, Y)

        GetCoordinates("CF_33_BillingProvider_Name", X, Y)
        CF_33_BillingProvider_Name = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_Address_Line1", X, Y)
        CF_33_BillingProvider_Address_Line1 = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_Address_Line2", X, Y)
        CF_33_BillingProvider_Address_Line2 = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_City", X, Y)
        CF_33_BillingProvider_City = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_State", X, Y)
        CF_33_BillingProvider_State = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_Zip", X, Y)
        CF_33_BillingProvider_Zip = New FormFieldString(X, Y)
        GetCoordinates("CF_33a_BillingProvider_NPI", X, Y)
        CF_33a_BillingProvider_NPI = New FormFieldString(X, Y)
        GetCoordinates("CF_33b_BillingProvider_UPIN_OtherID", X, Y)
        CF_33b_BillingProvider_UPIN_OtherID = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_Tel_AreaCode", X, Y)
        CF_33_BillingProvider_Tel_AreaCode = New FormFieldString(X, Y)
        GetCoordinates("CF_33_BillingProvider_Tel_Number", X, Y)
        CF_33_BillingProvider_Tel_Number = New FormFieldString(X, Y)
    End Sub

    Private Function GetPrinterSetting() As DataTable
        Try
            Dim _Query As String = ""
            Dim dtSettings As DataTable
            Dim oDB As New gloDatabaseLayer.DBLayer(_DataBaseConnectionString)
            _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName " & " FROM BL_CMS1500PrintSettings WHERE nClinicID = 1 AND nFormType = 1"
            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Throw ex
        End Try
    End Function

    Private Sub GetCoordinates(ByVal sBoxName As [String], ByRef nX As Int32, ByRef nY As Int32)
        Try
            nX = 0
            nY = 0
            Dim oView As DataView
            Dim oSetting As New DataTable()
            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                oView = dtSettings.DefaultView
                oView.RowFilter = "sBoxName = '" & sBoxName & "'"
                oSetting = oView.ToTable()
                If oSetting.Rows.Count > 0 Then
                    nX = Convert.ToInt32(oSetting.Rows(0)("nXCoordinate"))
                    nY = Convert.ToInt32(oSetting.Rows(0)("nYCoordinate"))

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Throw ex
        End Try
    End Sub
#End Region

End Class

Public Class FormFieldString
#Region "Constructor & Destructor"

    Public Sub New(ByVal PointX As Single, ByVal PointY As Single, Optional ByVal nCharSize As Single = 30)
        _LocationX = PointX
        _LocationY = PointY
        _CharSize = nCharSize
    End Sub

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

    Private _FieldValue As String = ""
    Private _LocationX As Single = 0
    Private _LocationY As Single = 0
    Private _CharSize As Single = 0

    Public Property Value() As String
        Get
            Return _FieldValue
        End Get
        Set(ByVal value As String)
            _FieldValue = value
        End Set
    End Property

    Public ReadOnly Property Location() As System.Drawing.PointF
        Get
            Return New System.Drawing.PointF(_LocationX, _LocationY)
        End Get
    End Property
    Public Property CharSize() As Single
        Get
            Return _CharSize
        End Get
        Set(ByVal value As Single)
            _CharSize = value
        End Set
    End Property
End Class

Public Class FormFieldBoolean
#Region "Constructor & Destructor"

    Public Sub New(ByVal PointX As Single, ByVal PointY As Single, Optional ByVal nCharSize As Single = 30)
        _LocationX = PointX
        _LocationY = PointY
        _CharSize = nCharSize
    End Sub

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

    Private _FieldValue As Boolean = False
    Private _LocationX As Single = 0
    Private _LocationY As Single = 0
    Private _CharSize As Single = 0

    Public Property Value() As Boolean
        Get
            Return _FieldValue
        End Get
        Set(ByVal value As Boolean)
            _FieldValue = value
        End Set
    End Property

    Public ReadOnly Property Location() As System.Drawing.PointF
        Get
            Return New System.Drawing.PointF(_LocationX, _LocationY)
        End Get
    End Property

    Public Property CharSize() As Single
        Get
            Return _CharSize
        End Get
        Set(ByVal value As Single)
            _CharSize = value
        End Set
    End Property
End Class


Public Class gloPrintPaperForm
#Region "Constructor & Destructor"

    Public Sub New()
    End Sub

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

    Private oSourceHCFA1500 As Bitmap = Nothing
    Private oGraphics As Graphics = Nothing
    Dim arialRegular As Font = Nothing
    Dim arialBold As Font = Nothing
    Dim toCreateEMF As Boolean = (gloGlobal.gloTSPrint.isCopyPrint And gloGlobal.gloTSPrint.UseEMFForClaims)
    Dim arialFontSmallHeight As Single = 8.5F
    Dim arialFontBigHeight As Single = 24.0F
    Private _oHCFA1500Form As gloHCFA1500PaperForm = Nothing

    Private Sub GetFontHeight()
        Using oGraphics = Graphics.FromImage(oSourceHCFA1500)
            arialFontSmallHeight = arialRegular.GetHeight(oGraphics)
            arialFontBigHeight = arialBold.GetHeight(oGraphics)
        End Using
    End Sub
    Private Sub AdjustFontHeight()
        Dim _arialFontSmall As Single = arialRegular.GetHeight(oGraphics)
        Dim _arialFontBig As Single = arialBold.GetHeight(oGraphics)
        arialRegular = New Font(arialRegular.FontFamily, arialRegular.Size * arialFontSmallHeight / _arialFontSmall, arialRegular.Style)
        arialBold = New Font(arialBold.FontFamily, arialBold.Size * arialFontBigHeight / _arialFontBig, arialBold.Style)
    End Sub
    Private Function CreateEMFHCFA1500Form(thisGraphics As Graphics, bmpWidth As Single, bmpHeight As Single) As Integer
        Try
            oGraphics = thisGraphics
            AdjustFontHeight()
            '#Region "Write Respective Data on Image"
            ' Write Data Image region shifted to following Method, 
            oGraphics.Clear(Color.White)
            WriteRespectiveData(_oHCFA1500Form)
            '#End Region

            Return 0
        Catch
            Return 1
        End Try

    End Function



    Public Function PrintHCFA1500Form(ByVal oHCFA1500Form As gloHCFA1500PaperForm) As String
        Dim _result As String = ""
        Dim _printFilePath As String = ""
        Dim _SourceFilePath As String = ""
        _oHCFA1500Form = oHCFA1500Form
        Try

            'If File.Exists(SourceFilePath) = True Then
            'Dim oFileInfo As New FileInfo(SourceFilePath)
            _printFilePath = (Application.StartupPath & "\") + "CMS1500TestPrint" & (If(toCreateEMF, ".emf", ".tif"))
            If File.Exists(_printFilePath) = True Then
                File.Delete(_printFilePath)
            End If

            'oFileInfo = Nothing

            'If File.Exists(SourceFilePath) = True Then

            'oSourceHCFA1500 = new Bitmap(SourceFilePath);
            'oSourceHCFA1500 = New Bitmap(817, 1057)
            ' oSourceHCFA1500 = New Bitmap(817 * 2, 1057 * 2)
            '' Dim oSourceHCFA1500 As New Bitmap(Application.StartupPath.ToString() & "\\CMS1500_BLANK.tif")
            _SourceFilePath = (Application.StartupPath & "\") & "CMS1500_BLANK.tif"
            oSourceHCFA1500 = New Bitmap(_SourceFilePath)
            oSourceHCFA1500 = New Bitmap(Application.StartupPath.ToString() & "\CMS1500_BLANK.tif")
            arialRegular = New Font("Arial", 8.25F, FontStyle.Regular)
            arialBold = New Font("Arial", 8.25F, FontStyle.Bold)
            If toCreateEMF Then
                GetFontHeight()
                Dim emfBytes As Byte() = gloGlobal.CreateEMF.GetEMFBytes(CSng(oSourceHCFA1500.Width) / oSourceHCFA1500.HorizontalResolution, CSng(oSourceHCFA1500.Height) / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height, AddressOf CreateEMFHCFA1500Form)
                File.WriteAllBytes(_printFilePath, emfBytes)
            Else

                oGraphics = Graphics.FromImage(oSourceHCFA1500)
                WriteRespectiveData(oHCFA1500Form)

                oSourceHCFA1500.Save(_printFilePath)

                If oGraphics IsNot Nothing Then
                    oGraphics.Dispose()
                End If
            End If

            If oSourceHCFA1500 IsNot Nothing Then
                oSourceHCFA1500.Dispose()
            End If
            If arialRegular IsNot Nothing Then
                arialRegular.Dispose()
                arialRegular = Nothing
            End If
            If arialBold IsNot Nothing Then
                arialBold.Dispose()
                arialBold = Nothing
            End If

            _result = _printFilePath
            'End If
            'End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
        End Try
        Return _result
    End Function

    Private Sub WriteRespectiveData(oHCFA1500Form As gloHCFA1500PaperForm)
        '.Insuracne Header on Claim Form
        WriteData(oHCFA1500Form.CF_Top_InsuranceHeader.Value, oHCFA1500Form.CF_Top_InsuranceHeader.Location, False)

        '.Insurance Type"
        '#Region "Print Insurance Type"
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, True)
        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, True)
        '#End Region

        '.Insured's ID Number
        WriteData(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, False)

        '.Patient's Name
        WriteData(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, False)

        '.Patient's Birth Date
        WriteData(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, False)
        WriteData(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, False)
        WriteData(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, False)

        '.Patient's Sex
        WriteData(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, True)
        WriteData(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, True)

        '.Insured's Name
        WriteData(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, False)

        '.Patient's Address
        WriteData(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, False)
        WriteData(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, False)
        WriteData(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, False)
        WriteData(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, False)
        WriteData(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, False)
        WriteData(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, False)

        '.Paient Relationship to Insured
        WriteData(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, True)
        WriteData(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, True)
        WriteData(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, True)
        WriteData(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, True)

        '.Insured's Address
        WriteData(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, False)
        WriteData(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, False)
        WriteData(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, False)
        WriteData(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, False)
        WriteData(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, False)
        WriteData(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, False)

        '.Patient Status
        WriteData(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, True)
        WriteData(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, True)
        WriteData(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, True)
        WriteData(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, True)
        WriteData(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, True)
        WriteData(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, True)

        '.Other Insured's Name
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, True)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, True)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, False)
        WriteData(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, False)

        '.Patient Condition 
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, False)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, True)
        WriteData(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, False)

        '.Insured's Information
        WriteData(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, True)
        WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, True)
        WriteData(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, False)
        WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, True)
        WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, True)

        WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, False)
        WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, False)

        WriteData(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, False)

        WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, False)
        WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, False)
        WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, False)

        WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, False)
        WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, False)
        WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, False)

        WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, False)
        WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, False)
        WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, False)

        WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, False)
        WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, False)
        WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, False)

        WriteData(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, False)
        WriteData(oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Value, oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Location, False)
        WriteData(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, False)

        WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, False)
        WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, False)
        WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, False)

        WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, False)
        WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, False)
        WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, False)

        WriteData(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, False)

        WriteData(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, True)
        WriteData(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, True)
        WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, False)

        WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, False)

        WriteData(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, False)
        WriteData(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, False)

        WriteData(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, False)

        '#Region " Service Line 1 "

        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)
        WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L1_Note.Value, oHCFA1500Form.CF_24A_L1_Note.Location, False)
        '#End Region

        '#Region " Service Line 2 "

        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)
        WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L2_Note.Value, oHCFA1500Form.CF_24A_L2_Note.Location, False)

        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)
        WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L3_Note.Value, oHCFA1500Form.CF_24A_L3_Note.Location, False)

        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)

        WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, False)
        WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L4_Note.Value, oHCFA1500Form.CF_24A_L4_Note.Location, False)


        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)
        WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, False)
        WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L5_Note.Value, oHCFA1500Form.CF_24A_L5_Note.Location, False)
        '#End Region

        '#Region " Service Line 6 "


        'From Date.
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, False)

        'To Date
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, False)
        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, False)

        'Place Of Service
        WriteData(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, False)

        'EMG - Emergency
        WriteData(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, False)

        'CPT/HCPCS 
        WriteData(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, False)

        'Modifiers
        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, False)
        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, False)

        'Diagnosis Pointers 
        WriteData(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, False)

        'Charges
        WriteData(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, False)

        'Days or Units
        WriteData(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, False)

        'EPSDT Family Plan
        WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, False)

        'Rendering Provider ID (NPI)
        WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, False)
        WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_OthrQualifier.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OthrQualifier.Location, False)

        WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_OthrQualifierValue.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OthrQualifierValue.Location, False)

        'Line Note
        WriteData(oHCFA1500Form.CF_24A_L6_Note.Value, oHCFA1500Form.CF_24A_L6_Note.Location, False)
        '#End Region

        WriteData(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, False)
        WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, True)
        WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, True)

        WriteData(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, False)

        WriteData(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, True)
        WriteData(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, True)

        WriteData(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, False)

        WriteData(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, False)

        WriteData(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, False)
        WriteData(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, False)

        WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, False)
        WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, False)
        WriteData(oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Value, oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Location, False)

        WriteData(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, False)
        WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, False)
        WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, False)
        WriteData(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, False)
        WriteData(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, False)
        WriteData(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, False)
        WriteData(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, False)
        WriteData(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, False)

        WriteData(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, False)
        WriteData(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, False)
        WriteData(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, False)
        WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, False)

    End Sub

    Public Function PrintHCFA1500Form(ByVal oHCFA1500Form As gloHCFA1500PaperForm, ByVal SourceFilePath As String, ByVal DestinationFilePath As String) As String
        Dim _result As String = ""
        Dim _printFilePath As String = ""
        Try

            If File.Exists(SourceFilePath) = True Then
                'FileInfo oFileInfo = new FileInfo(SourceFilePath);
                '_printFilePath = oFileInfo.DirectoryName + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".tif";
                'if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }
                'oFileInfo = null;

                _printFilePath = DestinationFilePath
                If File.Exists(_printFilePath) = True Then
                    File.Delete(_printFilePath)
                End If

                If File.Exists(SourceFilePath) = True Then

                    oSourceHCFA1500 = New Bitmap(SourceFilePath)
                    oGraphics = Graphics.FromImage(oSourceHCFA1500)

                    '#Region "Write Data on Image"
                    '.Insurance Type"
                    '#Region "Print Insurance Type"
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, True)
                    WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, True)
                    '#End Region

                    '.Insured's ID Number
                    WriteData(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, False)

                    '.Patient's Name
                    WriteData(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, False)

                    '.Patient's Birth Date
                    WriteData(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, False)

                    '.Patient's Sex
                    WriteData(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, True)
                    WriteData(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, True)

                    '.Insured's Name
                    WriteData(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, False)

                    '.Patient's Address
                    WriteData(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, False)
                    WriteData(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, False)
                    WriteData(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, False)
                    WriteData(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, False)
                    WriteData(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, False)
                    WriteData(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, False)

                    '.Paient Relationship to Insured
                    WriteData(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, True)
                    WriteData(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, True)
                    WriteData(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, True)
                    WriteData(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, True)

                    '.Insured's Address
                    WriteData(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, False)
                    WriteData(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, False)
                    WriteData(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, False)
                    WriteData(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, False)
                    WriteData(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, False)
                    WriteData(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, False)

                    '.Patient Status
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, True)
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, True)
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, True)
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, True)
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, True)
                    WriteData(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, True)

                    '.Other Insured's Name
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, True)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, True)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, False)
                    WriteData(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, False)

                    '.Patient Condition 
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, False)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, True)
                    WriteData(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, False)

                    '.Insured's Information
                    WriteData(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, True)
                    WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, True)
                    WriteData(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, False)
                    WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, True)
                    WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, True)

                    WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, False)
                    WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, False)

                    WriteData(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, False)

                    WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, False)
                    WriteData(oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Value, oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Location, False)
                    WriteData(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, False)

                    WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, False)
                    WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, False)
                    WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, False)

                    WriteData(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, False)

                    WriteData(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, True)
                    WriteData(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, True)
                    WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, False)

                    WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, False)

                    WriteData(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, False)
                    WriteData(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, False)

                    WriteData(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, False)

                    '#Region " Service Line 1 "

                    If oHCFA1500Form.CF_IsPresent_Line1 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, False)

                        'Line Note
                        WriteData(oHCFA1500Form.CF_24A_L1_Note.Value, oHCFA1500Form.CF_24A_L1_Note.Location, False)
                    End If
                    '#End Region

                    '#Region " Service Line 2 "

                    If oHCFA1500Form.CF_IsPresent_Line2 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, False)

                        'Line Note
                        WriteData(oHCFA1500Form.CF_24A_L2_Note.Value, oHCFA1500Form.CF_24A_L2_Note.Location, False)
                    End If
                    '#End Region

                    '#Region " Service Line 3 "

                    If oHCFA1500Form.CF_IsPresent_Line3 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, False)

                        'Line Note
                        WriteData(oHCFA1500Form.CF_24A_L3_Note.Value, oHCFA1500Form.CF_24A_L3_Note.Location, False)
                    End If
                    '#End Region

                    '#Region " Service Line 4 "

                    If oHCFA1500Form.CF_IsPresent_Line4 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, False)

                        'Line Note

                        WriteData(oHCFA1500Form.CF_24A_L4_Note.Value, oHCFA1500Form.CF_24A_L4_Note.Location, False)
                    End If
                    '#End Region

                    '#Region " Service Line 5 "

                    If oHCFA1500Form.CF_IsPresent_Line5 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, False)

                        'Line Note

                        WriteData(oHCFA1500Form.CF_24A_L5_Note.Value, oHCFA1500Form.CF_24A_L5_Note.Location, False)
                    End If
                    '#End Region

                    '#Region " Service Line 6 "

                    If oHCFA1500Form.CF_IsPresent_Line6 = True Then
                        'From Date.
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, False)

                        'To Date
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, False)
                        WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, False)

                        'Place Of Service
                        WriteData(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, False)

                        'EMG - Emergency
                        WriteData(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, False)

                        'CPT/HCPCS 
                        WriteData(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, False)

                        'Modifiers
                        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, False)
                        WriteData(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, False)

                        'Diagnosis Pointers 
                        WriteData(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, False)

                        'Charges
                        WriteData(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, False)
                        WriteData(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, False)

                        'Days or Units
                        WriteData(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, False)

                        'EPSDT Family Plan
                        WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, False)

                        'Rendering Provider ID (NPI)
                        WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, False)

                        'Line Note
                        WriteData(oHCFA1500Form.CF_24A_L6_Note.Value, oHCFA1500Form.CF_24A_L6_Note.Location, False)
                    End If
                    '#End Region

                    WriteData(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, False)
                    WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, True)
                    WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, True)

                    WriteData(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, False)

                    WriteData(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, True)
                    WriteData(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, True)

                    WriteData(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, False)

                    WriteData(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, False)

                    WriteData(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, False)
                    WriteData(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, False)

                    WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, False)
                    WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, False)

                    WriteData(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, False)
                    WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, False)
                    WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, False)
                    WriteData(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, False)
                    WriteData(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, False)
                    WriteData(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, False)
                    WriteData(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, False)
                    WriteData(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, False)

                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, False)
                    WriteData(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, False)
                    WriteData(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, True)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, False)
                    WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, False)

                    '#End Region

                    oSourceHCFA1500.Save(_printFilePath)

                    If oGraphics IsNot Nothing Then
                        oGraphics.Dispose()
                    End If
                    If oSourceHCFA1500 IsNot Nothing Then
                        oSourceHCFA1500.Dispose()
                    End If

                    _result = _printFilePath
                End If
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
        End Try
        Return _result
    End Function

    Private Sub WriteData(ByVal data As String, ByVal location As PointF, ByVal isboolean As Boolean)
        Dim oPoint As New PointF(location.X * 2, location.Y * 2)
        If isboolean = False Then

            oGraphics.DrawString(data, arialRegular, Brushes.Black, oPoint)
        Else
            If data.ToUpper() = "TRUE" Then
                oGraphics.DrawString("X", arialBold, Brushes.Black, oPoint)
            End If
        End If
    End Sub
End Class