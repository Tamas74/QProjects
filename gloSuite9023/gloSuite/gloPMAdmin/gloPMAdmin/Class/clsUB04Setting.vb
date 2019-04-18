Imports System.IO
Public Class clsUB04Setting

    Public Function GetUB04PrinterSettings(Optional ByVal SelectedPrinter As String = "") As DataTable
        Try
            Dim _Query As String = ""
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
            Dim dtSettings As New DataTable
            oDB.Connect(False)
            If SelectedPrinter <> "" Then
                _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
                & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel], nCharSize AS [Char Size] FROM BL_UB04PrintSettings " _
                & " WHERE nClinicID = 1 AND nFormType = 1 and sPrinterName ='" + SelectedPrinter + "' ORDER BY [Field Name], [nPanel]"
            Else
                _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
                & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel] , nCharSize AS [Char Size] FROM BL_UB04PrintSettings " _
                & " WHERE nClinicID = 1 AND nFormType = 1 ORDER BY [Field Name], [nPanel]"
            End If

            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Function

    Public Function GetDefaultUB04PrinterSettings() As DataTable
        Try
            Dim _Query As String = ""
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
            Dim dtSettings As New DataTable
            oDB.Connect(False)
            _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
               & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel], nCharSize AS [Char Size] FROM BL_UB04PrintSettings " _
               & " WHERE nClinicID = 1 AND nFormType = 0 and sPrinterName ='Default' ORDER BY [Field Name], [nPanel]"
            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Function

    Public Function GetDefaultUB04PrinterSpecificSettings(Optional ByVal SelectedPrinter As String = "") As DataTable
        Try
            Dim _Query As String = ""
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString)
            Dim dtSettings As New DataTable
            oDB.Connect(False)
            _Query = " SELECT ISNULL(sBoxCaption,'') AS [Field Name], ISNULL(sBoxName,'') AS [Box Name], ISNULL(nXCoordinate,0) AS [X], " _
               & " ISNULL(nYCoordinate,0) AS [Y], ISNULL(nPanel,0) AS [nPanel], nCharSize AS [Char Size] FROM BL_UB04PrintSettings " _
               & " WHERE nClinicID = 1 AND nFormType = 0  and sPrinterName = '" + SelectedPrinter + "' ORDER BY [Field Name], [nPanel]"
            oDB.Retrive_Query(_Query, dtSettings)
            oDB.Disconnect()
            oDB.Dispose()
            Return dtSettings

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Function
End Class
Public Class gloUB04PaperForm
#Region "Constructor & Destructor"

    Public Sub New(Optional ByVal _sPrinterName As String = "Default")
        _DataBaseConnectionString = mdlGeneral.GetConnectionString
        dtSettings = GetPrinterSetting(_sPrinterName)
        UB04_ServiceLines = New ServiceLines()



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

    Public UB04_1_ProviderName As New UB04FormFieldString
    Public UB04_1_ProviderAddress As New UB04FormFieldString
    Public UB04_1a_ProviderCity As New UB04FormFieldString
    Public UB04_1b_ProviderState As New UB04FormFieldString
    Public UB04_1c_ProviderZipCode As New UB04FormFieldString
    Public UB04_1a_ProviderPhone As New UB04FormFieldString
    Public UB04_1b_ProviderFaxNumber As New UB04FormFieldString
    Public UB04_1c_ProviderCountryCode As New UB04FormFieldString
    Public UB04_2_PayToName As New UB04FormFieldString
    Public UB04_2_PayToAddress As New UB04FormFieldString
    Public UB04_2a_Pay_toCity As New UB04FormFieldString
    Public UB04_2b_Pay_toState As New UB04FormFieldString
    Public UB04_2a_Pay_toZip As New UB04FormFieldString
    Public UB04_2b_ReservedFL02 As New UB04FormFieldString
    Public UB04_3a_PatientControlNumber As New UB04FormFieldString
    Public UB04_3b_MedicalHealthRecordNumber As New UB04FormFieldString
    Public UB04_4_TypeofBill As New UB04FormFieldString
    Public UB04_4_TypeofBillFrequencyCode As New UB04FormFieldString
    Public UB04_5_FederalTaxNumber_Upperline As New UB04FormFieldString
    Public UB04_5_FederalTaxNumber_Lowerline As New UB04FormFieldString
    Public UB04_6a_StatementCoversPeriod_From As New UB04FormFieldString
    Public UB04_6b_StatementCoversPeriod_Through As New UB04FormFieldString
    Public UB04_7a_ReservedFL07A As New UB04FormFieldString
    Public UB04_7b_ReservedFL07B As New UB04FormFieldString
    Public UB04_8_PatientIdentifier As New UB04FormFieldString
    Public UB04_8a_PatientSocialSecurityNumber As New UB04FormFieldString
    Public UB04_8b_PatientName As New UB04FormFieldString
    Public UB04_9a_PatientStreetAddress As New UB04FormFieldString
    Public UB04_9b_PatientCity As New UB04FormFieldString
    Public UB04_9c_PatientState As New UB04FormFieldString
    Public UB04_9d_PatientZip As New UB04FormFieldString
    Public UB04_9e_PatientCountryCode As New UB04FormFieldString
    Public UB04_10_PatientBirthDate As New UB04FormFieldString
    Public UB04_11_PatientGender As New UB04FormFieldString
    Public UB04_11_PatientMaritalStatus As New UB04FormFieldString
    Public UB04_11_PatientRace As New UB04FormFieldString
    Public UB04_12_Admission_Visit_StartofCareDate As New UB04FormFieldString
    Public UB04_13_Admission_Visit_Hour As New UB04FormFieldString
    Public UB04_14_AdmissionType As New UB04FormFieldString
    Public UB04_15_ReferralSource As New UB04FormFieldString
    Public UB04_16_DischargeHour As New UB04FormFieldString
    Public UB04_17_DischargeStatus As New UB04FormFieldString
    Public UB04_18_ConditionCodes As New UB04FormFieldString
    Public UB04_19_ConditionCodes As New UB04FormFieldString
    Public UB04_20_ConditionCodes As New UB04FormFieldString
    Public UB04_21_ConditionCodes As New UB04FormFieldString
    Public UB04_22_ConditionCodes As New UB04FormFieldString
    Public UB04_23_ConditionCodes As New UB04FormFieldString
    Public UB04_24_ConditionCodes As New UB04FormFieldString
    Public UB04_25_ConditionCodes As New UB04FormFieldString
    Public UB04_26_ConditionCodes As New UB04FormFieldString
    Public UB04_27_ConditionCodes As New UB04FormFieldString
    Public UB04_28_ConditionCodes As New UB04FormFieldString
    Public UB04_29_AccidentState As New UB04FormFieldString
    Public UB04_30_ReservedFL30A As New UB04FormFieldString
    Public UB04_30_ReservedFL30B As New UB04FormFieldString

    Public UB04_31a_OccurrenceCode As New UB04FormFieldString
    Public UB04_31b_OccurrenceDate As New UB04FormFieldString
    Public UB04_32a_OccurrenceCode As New UB04FormFieldString
    Public UB04_32b_OccurrenceDate As New UB04FormFieldString
    Public UB04_33a_OccurrenceCode As New UB04FormFieldString
    Public UB04_33b_OccurrenceDate As New UB04FormFieldString
    Public UB04_34a_OccurrenceCode As New UB04FormFieldString
    ''Public UB04_34_OccurrenceDate As New UB04FormFieldString
    Public UB04_31a_OccurrenceDate As New UB04FormFieldString
    Public UB04_31b_OccurrenceCode As New UB04FormFieldString
    Public UB04_32a_OccurrenceDate As New UB04FormFieldString
    Public UB04_32b_OccurrenceCode As New UB04FormFieldString
    Public UB04_33a_OccurrenceDate As New UB04FormFieldString
    Public UB04_33b_OccurrenceCode As New UB04FormFieldString
    Public UB04_34a_OccurrenceDate As New UB04FormFieldString
    Public UB04_34b_OccurrenceCode As New UB04FormFieldString
    Public UB04_34b_OccurrenceDate As New UB04FormFieldString


    Public UB04_35a_OccurrenceSpanCode As New UB04FormFieldString
    Public UB04_35a_OccurrenceSpanDateFrom As New UB04FormFieldString
    Public UB04_35a_OccurrenceSpanDateThrough As New UB04FormFieldString

    Public UB04_35b_OccurrenceSpanCode As New UB04FormFieldString
    Public UB04_35b_OccurrenceSpanDateFrom As New UB04FormFieldString
    Public UB04_35b_OccurrenceSpanDateThrough As New UB04FormFieldString

    Public UB04_36a_OccurrenceSpanCode As New UB04FormFieldString
    Public UB04_36a_OccurrenceSpanDateFrom As New UB04FormFieldString
    Public UB04_36a_OccurrenceSpanDateThrough As New UB04FormFieldString

    Public UB04_36b_OccurrenceSpanCode As New UB04FormFieldString
    Public UB04_36b_OccurrenceSpanDateFrom As New UB04FormFieldString
    Public UB04_36b_OccurrenceSpanDateThrough As New UB04FormFieldString


    Public UB04_37_ReservedFL37 As New UB04FormFieldString
    Public UB04_38_ResponsiblePartyNameAddress As New UB04FormFieldString
    'Public UB04_31a_OccurrenceDate As New UB04FormFieldString
    'Public UB04_31b_OccurrenceCode As New UB04FormFieldString
    'Public UB04_32a_OccurrenceDate As New UB04FormFieldString
    'Public UB04_32b_OccurrenceCode As New UB04FormFieldString
    'Public UB04_33a_OccurrenceDate As New UB04FormFieldString
    'Public UB04_33b_OccurrenceCode As New UB04FormFieldString
    'Public UB04_34a_OccurrenceDate As New UB04FormFieldString
    'Public UB04_34b_OccurrenceCode As New UB04FormFieldString
    'Public UB04_34b_OccurrenceDate As New UB04FormFieldString
    'Public UB04_35a_OccurrenceSpanDateFrom As New UB04FormFieldString
    'Public UB04_35a_OccurrenceSpanDateThrough As New UB04FormFieldString
    'Public UB04_35b_OccurrenceSpanCode As New UB04FormFieldString
    'Public UB04_35b_OccurrenceSpanDateThrough As New UB04FormFieldString
    'Public UB04_36a_OccurrenceSpanCode As New UB04FormFieldString
    'Public UB04_36a_OccurrenceSpanDateFrom As New UB04FormFieldString
    'Public UB04_36a_OccurrenceSpanDateThrough As New UB04FormFieldString
    'Public UB04_36b_OccurrenceSpanCode As New UB04FormFieldString
    'Public UB04_36b_OccurrenceSpanDateThrough As New UB04FormFieldString


    Public UB04_39a_ValueCode As FormFieldString
    Public UB04_39a_ValueCodeAmount As FormFieldString
    Public UB04_39a_ValueCodeAmount_Cents As FormFieldString
    Public UB04_39b_ValueCode As FormFieldString
    Public UB04_39b_ValueCodeAmount As FormFieldString
    Public UB04_39b_ValueCodeAmount_Cents As FormFieldString
    Public UB04_39c_ValueCode As FormFieldString
    Public UB04_39c_ValueCodeAmount As FormFieldString
    Public UB04_39c_ValueCodeAmount_Cents As FormFieldString
    Public UB04_39d_ValueCode As FormFieldString
    Public UB04_39d_ValueCodeAmount As FormFieldString
    Public UB04_39d_ValueCodeAmount_Cents As FormFieldString
    Public UB04_40a_ValueCode As FormFieldString
    Public UB04_40a_ValueCodeAmount As FormFieldString
    Public UB04_40a_ValueCodeAmount_Cents As FormFieldString
    Public UB04_40b_ValueCode As FormFieldString
    Public UB04_40b_ValueCodeAmount As FormFieldString
    Public UB04_40b_ValueCodeAmount_Cents As FormFieldString
    Public UB04_40c_ValueCode As FormFieldString
    Public UB04_40c_ValueCodeAmount As FormFieldString
    Public UB04_40c_ValueCodeAmount_Cents As FormFieldString
    Public UB04_40d_ValueCode As FormFieldString
    Public UB04_40d_ValueCodeAmount As FormFieldString
    Public UB04_40d_ValueCodeAmount_Cents As FormFieldString
    Public UB04_41a_ValueCode As FormFieldString
    Public UB04_41a_ValueCodeAmount As FormFieldString
    Public UB04_41a_ValueCodeAmount_Cents As FormFieldString
    Public UB04_41b_ValueCode As FormFieldString
    Public UB04_41b_ValueCodeAmount As FormFieldString
    Public UB04_41b_ValueCodeAmount_Cents As FormFieldString
    Public UB04_41c_ValueCode As FormFieldString
    Public UB04_41c_ValueCodeAmount As FormFieldString
    Public UB04_41c_ValueCodeAmount_Cents As FormFieldString
    Public UB04_41d_ValueCode As FormFieldString
    Public UB04_41d_ValueCodeAmount As FormFieldString
    Public UB04_41d_ValueCodeAmount_Cents As FormFieldString


    Public UB04_42_RevenueCode As UB04FormFieldString
    Public UB04_43_RevenueCodeDescription As UB04FormFieldString
    Public UB04_44_RateCodes As UB04FormFieldString
    Public UB04_45_ServiceDate_visit As UB04FormFieldString
    Public UB04_46_ServiceUnits As UB04FormFieldString
    Public UB04_47a_TotalCharges_Dollars As UB04FormFieldString
    Public UB04_47b_TotalCharges_Cents As UB04FormFieldString
    Public UB04_48a_Non_coveredCharges_Dollars As UB04FormFieldString
    Public UB04_48b_Non_coveredCharges_Cents As UB04FormFieldString
    Public UB04_49_ReservedFL49 As UB04FormFieldString
    Public UB04_ServiceLines As ServiceLines
    Public UB04_42L23_RevenueCode As New UB04FormFieldString
    Public UB04_47L23_SummaryTotalCharges_Dollars As New UB04FormFieldString
    Public UB04_47L23_SummaryTotalCharges_Cents As New UB04FormFieldString
    Public UB04_48L23_SummaryNon_coveredCharges_Dollars As New UB04FormFieldString
    Public UB04_48L23_SummaryNon_coveredCharges_Cents As New UB04FormFieldString
    Public UB04_49L23_Reserved49L23 As New UB04FormFieldString
    Public UB04_43L23_CurrentPage As New UB04FormFieldString
    Public UB04_44L23_TotalPages As New UB04FormFieldString
    Public UB04_44L23CreationDate As New UB04FormFieldString
    Public UB04_50_PayerName_Primary As New UB04FormFieldString
    Public UB04_50_PayerName_Secondary As New UB04FormFieldString
    Public UB04_50_PayerName_Tertiary As New UB04FormFieldString
    Public UB04_51_HealthPlanIDA As New UB04FormFieldString
    Public UB04_51_HealthPlanIDB As New UB04FormFieldString
    Public UB04_51_HealthPlanIDC As New UB04FormFieldString
    Public UB04_52_InformationRelease_Primary As New UB04FormFieldString
    Public UB04_52_InformationRelease_Secondary As New UB04FormFieldString
    Public UB04_52_InformationRelease_Tertiary As New UB04FormFieldString
    Public UB04_53_BenefitsAssignment_Primary As New UB04FormFieldString
    Public UB04_53_BenefitsAssignment_Secondary As New UB04FormFieldString
    Public UB04_53_BenefitsAssignment_Tertiary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsDollars_Primary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsCents_Primary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsDollars_Secondary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsCents_Secondary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsDollars_Tertiary As New UB04FormFieldString
    Public UB04_54_PriorPaymentsCents_Tertiary As New UB04FormFieldString
    Public UB04_55a_EstimatedAmountDueDollars_Primary As New UB04FormFieldString
    Public UB04_55a_EstimatedAmountDueCents_Primary As New UB04FormFieldString
    Public UB04_55b_EstimatedAmountDueDollars_Secondary As New UB04FormFieldString
    Public UB04_55b_EstimatedAmountDueCents_Secondary As New UB04FormFieldString
    Public UB04_55c_EstimatedAmountDueDollars_Tertiary As New UB04FormFieldString
    Public UB04_55c_EstimatedAmountDueCents_Tertiary As New UB04FormFieldString
    Public UB04_56_NationalProviderIdentifier_NPI_ As New UB04FormFieldString
    Public UB04_57_OtherProvider_Primary As New UB04FormFieldString
    Public UB04_57_OtherProvider_Secondary As New UB04FormFieldString
    Public UB04_57_OtherProvider_Tertiary As New UB04FormFieldString
    Public UB04_58_InsuredName_Primary As New UB04FormFieldString
    Public UB04_58_InsuredName_Secondary As New UB04FormFieldString
    Public UB04_58_InsuredName_Tertiary As New UB04FormFieldString
    Public UB04_59_PatientRelationshipToInsured_Primary As New UB04FormFieldString
    Public UB04_59_PatientRelationshipToInsured_Secondary As New UB04FormFieldString
    Public UB04_59_PatientRelationshipToInsured_Tertiary As New UB04FormFieldString
    Public UB04_60_InsuredUniqueID_Primary As New UB04FormFieldString
    Public UB04_60_InsuredUniqueID_Secondary As New UB04FormFieldString
    Public UB04_60_InsuredUniqueID_Tertiary As New UB04FormFieldString
    Public UB04_61_InsuredGroupName_Primary As New UB04FormFieldString
    Public UB04_61_InsuredGroupName_Secondary As New UB04FormFieldString
    Public UB04_61_InsuredGroupName_Tertiary As New UB04FormFieldString
    Public UB04_62_InsuredGroupNumber_Primary As New UB04FormFieldString
    Public UB04_62_InsuredGroupNumber_Secondary As New UB04FormFieldString
    Public UB04_62_InsuredGroupNumber_Tertiary As New UB04FormFieldString
    Public UB04_63_TreatmentAuthorizationCode_Primary As New UB04FormFieldString
    Public UB04_63_TreatmentAuthorizationCode_Secondary As New UB04FormFieldString
    Public UB04_63_TreatmentAuthorizationCode_Tertiary As New UB04FormFieldString
    Public UB04_64_DocumentControlNumber_A As New UB04FormFieldString
    Public UB04_64_DocumentControlNumber_B As New UB04FormFieldString
    Public UB04_64_DocumentControlNumber_C As New UB04FormFieldString
    Public UB04_65_EmployerName_Primary As New UB04FormFieldString
    Public UB04_65_EmployerName_Secondary As New UB04FormFieldString
    Public UB04_65_EmployerName_Tertiary As New UB04FormFieldString
    Public UB04_66_ICDVersionIndicator As New UB04FormFieldString
    Public UB04_67_PrincipalDiagnosisCode As New UB04FormFieldString
    Public UB04_67a_OtherDiagnosis_A As New UB04FormFieldString
    Public UB04_67b_OtherDiagnosis_B As New UB04FormFieldString
    Public UB04_67c_OtherDiagnosis_C As New UB04FormFieldString
    Public UB04_67d_OtherDiagnosis_D As New UB04FormFieldString
    Public UB04_67e_OtherDiagnosis_E As New UB04FormFieldString
    Public UB04_67f_OtherDiagnosis_F As New UB04FormFieldString
    Public UB04_67g_OtherDiagnosis_G As New UB04FormFieldString
    Public UB04_67h_OtherDiagnosis_H As New UB04FormFieldString
    Public UB04_67i_OtherDiagnosis_I As New UB04FormFieldString
    Public UB04_67j_OtherDiagnosis_J As New UB04FormFieldString
    Public UB04_67k_OtherDiagnosis_K As New UB04FormFieldString
    Public UB04_67l_OtherDiagnosis_L As New UB04FormFieldString
    Public UB04_67m_OtherDiagnosis_M As New UB04FormFieldString
    Public UB04_67n_OtherDiagnosis_N As New UB04FormFieldString
    Public UB04_67o_OtherDiagnosis_O As New UB04FormFieldString
    Public UB04_67p_OtherDiagnosis_P As New UB04FormFieldString
    Public UB04_67q_OtherDiagnosis_Q As New UB04FormFieldString
    Public UB04_68_Reserved_68A As New UB04FormFieldString
    Public UB04_68_Reserved_68B As New UB04FormFieldString
    Public UB04_69_AdmittingDiagnosisCode As New UB04FormFieldString
    Public UB04_70a_PatientVisitReason_A As New UB04FormFieldString
    Public UB04_70b_PatientVisitReason_B As New UB04FormFieldString
    Public UB04_70c_PatientVisitReason_C As New UB04FormFieldString
    Public UB04_71_PPSCode As New UB04FormFieldString
    Public UB04_72a_ExternalCauseofInjuryCode_A As New UB04FormFieldString
    Public UB04_72b_ExternalCauseofInjuryCode_B As New UB04FormFieldString
    Public UB04_72c_ExternalCauseofInjuryCode_C As New UB04FormFieldString
    Public UB04_73_ReservedFL73 As New UB04FormFieldString
    Public UB04_74_ProcedureCode_Principal As New UB04FormFieldString
    Public UB04_74_ProcedureDate_Principal As New UB04FormFieldString
    Public UB04_74a_ProcedureCode_OtherA As New UB04FormFieldString
    Public UB04_74a_ProcedureDate_OtherA As New UB04FormFieldString
    Public UB04_74b_ProcedureCode_OtherB As New UB04FormFieldString
    Public UB04_74b_ProcedureDate_OtherB As New UB04FormFieldString
    Public UB04_74c_ProcedureCode_OtherC As New UB04FormFieldString
    Public UB04_74c_ProcedureDate_OtherC As New UB04FormFieldString
    Public UB04_74d_ProcedureCode_OtherD As New UB04FormFieldString
    Public UB04_74d_ProcedureDate_OtherD As New UB04FormFieldString
    Public UB04_74e_ProcedureCode_OtherE As New UB04FormFieldString
    Public UB04_74e_ProcedureDate_OtherE As New UB04FormFieldString
    Public UB04_75a_ReservedFL75A As New UB04FormFieldString
    Public UB04_75b_ReservedFL75B As New UB04FormFieldString
    Public UB04_75c_ReservedFL75C As New UB04FormFieldString
    Public UB04_75d_ReservedFL75D As New UB04FormFieldString
    Public UB04_76_AttendingNPI As New UB04FormFieldString
    Public UB04_76_AttendingQUAL As New UB04FormFieldString
    Public UB04_76_AttendingID As New UB04FormFieldString
    Public UB04_76a_AttendingLast As New UB04FormFieldString
    Public UB04_76b_AttendingFirst As New UB04FormFieldString
    Public UB04_77_OperatingNPI As New UB04FormFieldString
    Public UB04_77_OperatingQUAL As New UB04FormFieldString
    Public UB04_77_OperatingID As New UB04FormFieldString
    Public UB04_77a_OperatingLast As New UB04FormFieldString
    Public UB04_77b_OperatingFirst As New UB04FormFieldString

    Public UB04_78_OtherNPI As New UB04FormFieldString
    Public UB04_78_OtherQUAL As New UB04FormFieldString
    Public UB04_78_OtherID As New UB04FormFieldString
    Public UB04_78_OtherLast As New UB04FormFieldString
    Public UB04_78_OtherFirst As New UB04FormFieldString
    Public UB04_78_OtherProvider_QUAL As New UB04FormFieldString
    Public UB04_79_OtherNPI As New UB04FormFieldString
    Public UB04_79_OtherQUAL As New UB04FormFieldString
    Public UB04_79_OtherID As New UB04FormFieldString
    Public UB04_79_OtherLast As New UB04FormFieldString
    Public UB04_79_OtherFirst As New UB04FormFieldString
    Public UB04_79_OtherProvider_QUAL As New UB04FormFieldString

    Public PayerCodeA_Primary As New UB04FormFieldString
    Public PayerCodeB_Secondary As New UB04FormFieldString
    Public PayerCodeC_Tertiary As New UB04FormFieldString
    Public UB04_80a_Remarks_1 As New UB04FormFieldString
    Public UB04_80b_Remarks_2 As New UB04FormFieldString
    Public UB04_80c_Remarks_3 As New UB04FormFieldString
    Public UB04_80d_Remarks_4 As New UB04FormFieldString
    Public UB04_81a_Code_Code_QUAL_A As New UB04FormFieldString
    Public UB04_81a_Code_Code_CODE_A As New UB04FormFieldString
    Public UB04_81a_Code_Code_VALUE_A As New UB04FormFieldString
    Public UB04_81b_Code_Code_QUAL_B As New UB04FormFieldString
    Public UB04_81b_Code_Code_CODE_B As New UB04FormFieldString
    Public UB04_81b_Code_Code_VALUE_B As New UB04FormFieldString
    Public UB04_81c_Code_Code_QUAL_C As New UB04FormFieldString
    Public UB04_81c_Code_Code_CODE_C As New UB04FormFieldString
    Public UB04_81c_Code_Code_VALUE_C As New UB04FormFieldString
    Public UB04_81d_Code_Code_QUAL_D As New UB04FormFieldString
    Public UB04_81d_Code_Code_CODE_D As New UB04FormFieldString
    Public UB04_81d_Code_Code_VALUE_D As New UB04FormFieldString


#End Region
#Region " Private Method "
    Private Sub InitializeBoxes()
        Dim X As Int32 = 0
        Dim Y As Int32 = 0
        Dim CharSize As Int32 = 0
        GetCoordinates("UB04_1_ProviderName", X, Y, CharSize)
        UB04_1_ProviderName = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_1_ProviderAddress", X, Y, CharSize)
        UB04_1_ProviderAddress = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_1a_ProviderCity", X, Y, CharSize)
        UB04_1a_ProviderCity = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_1b_ProviderState", X, Y, CharSize)
        UB04_1b_ProviderState = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_1c_ProviderZipCode", X, Y, CharSize)
        UB04_1c_ProviderZipCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_1a_ProviderPhone", X, Y, CharSize)
        UB04_1a_ProviderPhone = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_1b_ProviderFaxNumber", X, Y, CharSize)
        UB04_1b_ProviderFaxNumber = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_2_PayToName", X, Y, CharSize)
        UB04_2_PayToName = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_1c_ProviderCountryCode", X, Y, CharSize)
        UB04_1c_ProviderCountryCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_2_PayToAddress", X, Y, CharSize)
        UB04_2_PayToAddress = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_2a_Pay_toCity", X, Y, CharSize)
        UB04_2a_Pay_toCity = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_2b_Pay_toState", X, Y, CharSize)
        UB04_2b_Pay_toState = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_2a_Pay_toZip", X, Y, CharSize)
        UB04_2a_Pay_toZip = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_2b_ReservedFL02", X, Y, CharSize)
        UB04_2b_ReservedFL02 = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_3a_PatientControlNumber", X, Y, CharSize)
        UB04_3a_PatientControlNumber = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_10_PatientBirthDate", X, Y, CharSize)
        UB04_10_PatientBirthDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_3b_MedicalHealthRecordNumber", X, Y, CharSize)
        UB04_3b_MedicalHealthRecordNumber = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_4_TypeofBill", X, Y, CharSize)
        UB04_4_TypeofBill = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_4_TypeofBillFrequencyCode", X, Y, CharSize)
        UB04_4_TypeofBillFrequencyCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_5_FederalTaxNumber_Upperline", X, Y, CharSize)
        UB04_5_FederalTaxNumber_Upperline = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_5_FederalTaxNumber_Lowerline", X, Y, CharSize)
        UB04_5_FederalTaxNumber_Lowerline = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_6a_StatementCoversPeriod_From", X, Y, CharSize)
        UB04_6a_StatementCoversPeriod_From = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_6b_StatementCoversPeriod_Through", X, Y, CharSize)
        UB04_6b_StatementCoversPeriod_Through = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_7a_ReservedFL07A", X, Y, CharSize)
        UB04_7a_ReservedFL07A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_7b_ReservedFL07B", X, Y, CharSize)
        UB04_7b_ReservedFL07B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_8_PatientIdentifier", X, Y, CharSize)
        UB04_8_PatientIdentifier = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_8a_PatientSocialSecurityNumber", X, Y, CharSize)
        UB04_8a_PatientSocialSecurityNumber = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_8b_PatientName", X, Y, CharSize)
        UB04_8b_PatientName = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_9a_PatientStreetAddress", X, Y, CharSize)
        UB04_9a_PatientStreetAddress = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_9b_PatientCity", X, Y, CharSize)
        UB04_9b_PatientCity = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_9c_PatientState", X, Y, CharSize)
        UB04_9c_PatientState = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_9d_PatientZip", X, Y, CharSize)
        UB04_9d_PatientZip = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_9e_PatientCountryCode", X, Y, CharSize)
        UB04_9e_PatientCountryCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_11_PatientGender", X, Y, CharSize)
        UB04_11_PatientGender = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_11_PatientMaritalStatus", X, Y, CharSize)
        UB04_11_PatientMaritalStatus = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_11_PatientRace", X, Y, CharSize)
        UB04_11_PatientRace = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_12_Admission_Visit_StartofCareDate", X, Y, CharSize)
        UB04_12_Admission_Visit_StartofCareDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_13_Admission_Visit_Hour", X, Y, CharSize)
        UB04_13_Admission_Visit_Hour = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_14_AdmissionType", X, Y, CharSize)
        UB04_14_AdmissionType = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_15_ReferralSource", X, Y, CharSize)
        UB04_15_ReferralSource = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_16_DischargeHour", X, Y, CharSize)
        UB04_16_DischargeHour = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_17_DischargeStatus", X, Y, CharSize)
        UB04_17_DischargeStatus = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_18_ConditionCodes", X, Y, CharSize)
        UB04_18_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_19_ConditionCodes", X, Y, CharSize)
        UB04_19_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_20_ConditionCodes", X, Y, CharSize)
        UB04_20_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_21_ConditionCodes", X, Y, CharSize)
        UB04_21_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_22_ConditionCodes", X, Y, CharSize)
        UB04_22_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_23_ConditionCodes", X, Y, CharSize)
        UB04_23_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_24_ConditionCodes", X, Y, CharSize)
        UB04_24_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_25_ConditionCodes", X, Y, CharSize)
        UB04_25_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_26_ConditionCodes", X, Y, CharSize)
        UB04_26_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_27_ConditionCodes", X, Y, CharSize)
        UB04_27_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_28_ConditionCodes", X, Y, CharSize)
        UB04_28_ConditionCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_29_AccidentState", X, Y, CharSize)
        UB04_29_AccidentState = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_30_ReservedFL30A", X, Y, CharSize)
        UB04_30_ReservedFL30A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_30_ReservedFL30B", X, Y, CharSize)
        UB04_30_ReservedFL30B = New UB04FormFieldString(X, Y, CharSize)



        GetCoordinates("UB04_31a_OccurrenceCode", X, Y, CharSize)
        UB04_31a_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_31a_OccurrenceDate", X, Y, CharSize)
        UB04_31a_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_31b_OccurrenceCode", X, Y, CharSize)
        UB04_31b_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_31b_OccurrenceDate", X, Y, CharSize)
        UB04_31b_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_32a_OccurrenceCode", X, Y, CharSize)
        UB04_32a_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_32a_OccurrenceDate", X, Y, CharSize)
        UB04_32a_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_32b_OccurrenceCode", X, Y, CharSize)
        UB04_32b_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_32b_OccurrenceDate", X, Y, CharSize)
        UB04_32b_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_33a_OccurrenceCode", X, Y, CharSize)
        UB04_33a_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_33a_OccurrenceDate", X, Y, CharSize)
        UB04_33a_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_33b_OccurrenceCode", X, Y, CharSize)
        UB04_33b_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_33b_OccurrenceDate", X, Y, CharSize)
        UB04_33b_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_34a_OccurrenceCode", X, Y, CharSize)
        UB04_34a_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_34a_OccurrenceDate", X, Y, CharSize)
        UB04_34a_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_34b_OccurrenceCode", X, Y, CharSize)
        UB04_34b_OccurrenceCode = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_34b_OccurrenceDate", X, Y, CharSize)
        UB04_34b_OccurrenceDate = New UB04FormFieldString(X, Y, CharSize)

        'GetCoordinates("UB04_34_OccurrenceDate", X, Y, CharSize)
        'UB04_34_OccurrenceDate = New UB04FormFieldString (X, Y, CharSize)


        GetCoordinates("UB04_35a_OccurrenceSpanCode", X, Y, CharSize)
        UB04_35a_OccurrenceSpanCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_35a_OccurrenceSpanDateFrom", X, Y, CharSize)
        UB04_35a_OccurrenceSpanDateFrom = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_35a_OccurrenceSpanDateThrough", X, Y, CharSize)
        UB04_35a_OccurrenceSpanDateThrough = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_35b_OccurrenceSpanCode", X, Y, CharSize)
        UB04_35b_OccurrenceSpanCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_35b_OccurrenceSpanDateFrom", X, Y, CharSize)
        UB04_35b_OccurrenceSpanDateFrom = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_35b_OccurrenceSpanDateThrough", X, Y, CharSize)
        UB04_35b_OccurrenceSpanDateThrough = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_36a_OccurrenceSpanCode", X, Y, CharSize)
        UB04_36a_OccurrenceSpanCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_36a_OccurrenceSpanDateFrom", X, Y, CharSize)
        UB04_36a_OccurrenceSpanDateFrom = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_36a_OccurrenceSpanDateThrough", X, Y, CharSize)
        UB04_36a_OccurrenceSpanDateThrough = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_36b_OccurrenceSpanCode", X, Y, CharSize)
        UB04_36b_OccurrenceSpanCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_36b_OccurrenceSpanDateFrom", X, Y, CharSize)
        UB04_36b_OccurrenceSpanDateFrom = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_36b_OccurrenceSpanDateThrough", X, Y, CharSize)
        UB04_36b_OccurrenceSpanDateThrough = New UB04FormFieldString(X, Y, CharSize)





        GetCoordinates("UB04_38_ResponsiblePartyNameAddress", X, Y, CharSize)
        UB04_38_ResponsiblePartyNameAddress = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39a_ValueCode", X, Y, CharSize)
        UB04_39a_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39a_ValueCodeAmount_Dollars", X, Y, CharSize)
        UB04_39a_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39a_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_39a_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39b_ValueCode", X, Y, CharSize)
        UB04_39b_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39b_ValueCodeAmount_Dollars", X, Y, CharSize)
        UB04_39b_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39b_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_39b_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39c_ValueCode", X, Y, CharSize)
        UB04_39c_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39c_ValueCodeAmount_Dollars", X, Y, CharSize)
        UB04_39c_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39c_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_39c_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39d_ValueCode", X, Y, CharSize)
        UB04_39d_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39d_ValueCodeAmount_Dollars", X, Y, CharSize)
        UB04_39d_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_39d_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_39d_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)



        GetCoordinates("UB04_40a_ValueCode", X, Y, CharSize)
        UB04_40a_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40a_ValueCodeAmount", X, Y, CharSize)
        UB04_40a_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40a_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_40a_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40b_ValueCode", X, Y, CharSize)
        UB04_40b_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40b_ValueCodeAmount", X, Y, CharSize)
        UB04_40b_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40b_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_40b_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_40c_ValueCode", X, Y, CharSize)
        UB04_40c_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40c_ValueCodeAmount", X, Y, CharSize)
        UB04_40c_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40c_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_40c_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40d_ValueCode", X, Y, CharSize)
        UB04_40d_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40d_ValueCodeAmount", X, Y, CharSize)
        UB04_40d_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_40d_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_40d_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)



        GetCoordinates("UB04_41a_ValueCode", X, Y, CharSize)
        UB04_41a_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41a_ValueCodeAmount", X, Y, CharSize)
        UB04_41a_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41a_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_41a_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41b_ValueCode", X, Y, CharSize)
        UB04_41b_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41b_ValueCodeAmount", X, Y, CharSize)
        UB04_41b_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41b_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_41b_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41c_ValueCode", X, Y, CharSize)
        UB04_41c_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41c_ValueCodeAmount", X, Y, CharSize)
        UB04_41c_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41c_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_41c_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41d_ValueCode", X, Y, CharSize)
        UB04_41d_ValueCode = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41d_ValueCodeAmount", X, Y, CharSize)
        UB04_41d_ValueCodeAmount = New FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_41d_ValueCodeAmount_Cents", X, Y, CharSize)
        UB04_41d_ValueCodeAmount_Cents = New FormFieldString(X, Y, CharSize)

        'For index As Integer = 0 To oServiceLines.innerlist.Count - 1
        '    oServiceLines.innerlist.Item = New UB04FormFieldString(X, Y + (index * 15))
        'Next

        GetCoordinates("UB04_42_RevenueCode", X, Y, CharSize)
        UB04_42_RevenueCode = New UB04FormFieldString(X, Y, CharSize)
        ' Dim u As Int16 = UB04_ServiceLines.Count - 1
        GetCoordinates("UB04_43_RevenueCodeDescription", X, Y, CharSize)
        UB04_43_RevenueCodeDescription = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_44_RateCodes", X, Y, CharSize)
        UB04_44_RateCodes = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_45_ServiceDate_visit", X, Y, CharSize)
        UB04_45_ServiceDate_visit = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_46_ServiceUnits", X, Y, CharSize)
        UB04_46_ServiceUnits = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_47a_TotalCharges_Dollars", X, Y, CharSize)
        UB04_47a_TotalCharges_Dollars = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_47b_TotalCharges_Cents", X, Y, CharSize)
        UB04_47b_TotalCharges_Cents = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_48a_Non_coveredCharges_Dollars", X, Y, CharSize)
        UB04_48a_Non_coveredCharges_Dollars = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_48b_Non_coveredCharges_Cents", X, Y, CharSize)
        UB04_48b_Non_coveredCharges_Cents = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_49_ReservedFL49", X, Y, CharSize)
        UB04_49_ReservedFL49 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_42L23_RevenueCode", X, Y, CharSize)
        UB04_42L23_RevenueCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_47L23_SummaryTotalCharges_Dollars", X, Y, CharSize)

        UB04_47L23_SummaryTotalCharges_Dollars = New UB04FormFieldString(X, Y, CharSize)
        GetCoordinates("UB04_47L23_SummaryTotalCharges_Cents", X, Y, CharSize)
        UB04_47L23_SummaryTotalCharges_Cents = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_48L23_SummaryNon_coveredCharges_Dollars", X, Y, CharSize)
        UB04_48L23_SummaryNon_coveredCharges_Dollars = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_48L23_SummaryNon_coveredCharges_Cents", X, Y, CharSize)
        UB04_48L23_SummaryNon_coveredCharges_Cents = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_49L23_Reserved49L23", X, Y, CharSize)
        UB04_49L23_Reserved49L23 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_49L23_Reserved49L23", X, Y, CharSize)
        UB04_49L23_Reserved49L23 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_37_ReservedFL37", X, Y, CharSize)
        UB04_37_ReservedFL37 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_43L23_CurrentPage", X, Y, CharSize)
        UB04_43L23_CurrentPage = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_44L23_TotalPages", X, Y, CharSize)
        UB04_44L23_TotalPages = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_44L23CreationDate", X, Y, CharSize)
        UB04_44L23CreationDate = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_50_PayerName_Primary", X, Y, CharSize)
        UB04_50_PayerName_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_50_PayerName_Secondary", X, Y, CharSize)
        UB04_50_PayerName_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_50_PayerName_Tertiary", X, Y, CharSize)
        UB04_50_PayerName_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_51_HealthPlanIDA", X, Y, CharSize)
        UB04_51_HealthPlanIDA = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_51_HealthPlanIDB", X, Y, CharSize)
        UB04_51_HealthPlanIDB = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_51_HealthPlanIDC", X, Y, CharSize)
        UB04_51_HealthPlanIDC = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_52_InformationRelease_Primary", X, Y, CharSize)
        UB04_52_InformationRelease_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_52_InformationRelease_Secondary", X, Y, CharSize)
        UB04_52_InformationRelease_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_52_InformationRelease_Tertiary", X, Y, CharSize)
        UB04_52_InformationRelease_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_53_BenefitsAssignment_Primary", X, Y, CharSize)
        UB04_53_BenefitsAssignment_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_53_BenefitsAssignment_Secondary", X, Y, CharSize)
        UB04_53_BenefitsAssignment_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_53_BenefitsAssignment_Tertiary", X, Y, CharSize)
        UB04_53_BenefitsAssignment_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsDollars_Primary", X, Y, CharSize)
        UB04_54_PriorPaymentsDollars_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsCents_Primary", X, Y, CharSize)
        UB04_54_PriorPaymentsCents_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsDollars_Secondary", X, Y, CharSize)
        UB04_54_PriorPaymentsDollars_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsCents_Secondary", X, Y, CharSize)
        UB04_54_PriorPaymentsCents_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsDollars_Tertiary", X, Y, CharSize)
        UB04_54_PriorPaymentsDollars_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_54_PriorPaymentsCents_Tertiary", X, Y, CharSize)
        UB04_54_PriorPaymentsCents_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55a_EstimatedAmountDueDollars_Primary", X, Y, CharSize)
        UB04_55a_EstimatedAmountDueDollars_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55a_EstimatedAmountDueCents_Primary", X, Y, CharSize)
        UB04_55a_EstimatedAmountDueCents_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55b_EstimatedAmountDueDollars_Secondary", X, Y, CharSize)
        UB04_55b_EstimatedAmountDueDollars_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55b_EstimatedAmountDueCents_Secondary", X, Y, CharSize)
        UB04_55b_EstimatedAmountDueCents_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55c_EstimatedAmountDueDollars_Tertiary", X, Y, CharSize)
        UB04_55c_EstimatedAmountDueDollars_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_55c_EstimatedAmountDueCents_Tertiary", X, Y, CharSize)
        UB04_55c_EstimatedAmountDueCents_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_56_NationalProviderIdentifier_NPI_", X, Y, CharSize)
        UB04_56_NationalProviderIdentifier_NPI_ = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_57_OtherProvider_Primary", X, Y, CharSize)
        UB04_57_OtherProvider_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_57_OtherProvider_Secondary", X, Y, CharSize)
        UB04_57_OtherProvider_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_57_OtherProvider_Tertiary", X, Y, CharSize)
        UB04_57_OtherProvider_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_58_InsuredName_Primary", X, Y, CharSize)
        UB04_58_InsuredName_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_58_InsuredName_Secondary", X, Y, CharSize)
        UB04_58_InsuredName_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_58_InsuredName_Tertiary", X, Y, CharSize)
        UB04_58_InsuredName_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_59_PatientRelationshipToInsured_Primary", X, Y, CharSize)
        UB04_59_PatientRelationshipToInsured_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_59_PatientRelationshipToInsured_Secondary", X, Y, CharSize)
        UB04_59_PatientRelationshipToInsured_Secondary = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_59_PatientRelationshipToInsured_Tertiary", X, Y, CharSize)
        UB04_59_PatientRelationshipToInsured_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_60_InsuredUniqueID_Primary", X, Y, CharSize)
        UB04_60_InsuredUniqueID_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_60_InsuredUniqueID_Secondary", X, Y, CharSize)
        UB04_60_InsuredUniqueID_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_60_InsuredUniqueID_Tertiary", X, Y, CharSize)
        UB04_60_InsuredUniqueID_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_61_InsuredGroupName_Primary", X, Y, CharSize)
        UB04_61_InsuredGroupName_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_61_InsuredGroupName_Secondary", X, Y, CharSize)
        UB04_61_InsuredGroupName_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_61_InsuredGroupName_Tertiary", X, Y, CharSize)
        UB04_61_InsuredGroupName_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_62_InsuredGroupNumber_Primary", X, Y, CharSize)
        UB04_62_InsuredGroupNumber_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_62_InsuredGroupNumber_Secondary", X, Y, CharSize)
        UB04_62_InsuredGroupNumber_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_62_InsuredGroupNumber_Tertiary", X, Y, CharSize)
        UB04_62_InsuredGroupNumber_Tertiary = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_63_TreatmentAuthorizationCode_Primary", X, Y, CharSize)
        UB04_63_TreatmentAuthorizationCode_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_63_TreatmentAuthorizationCode_Secondary", X, Y, CharSize)
        UB04_63_TreatmentAuthorizationCode_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_63_TreatmentAuthorizationCode_Tertiary", X, Y, CharSize)
        UB04_63_TreatmentAuthorizationCode_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_64_DocumentControlNumber_A", X, Y, CharSize)
        UB04_64_DocumentControlNumber_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_64_DocumentControlNumber_B", X, Y, CharSize)
        UB04_64_DocumentControlNumber_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_64_DocumentControlNumber_C", X, Y, CharSize)
        UB04_64_DocumentControlNumber_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_65_EmployerName_Primary", X, Y, CharSize)
        UB04_65_EmployerName_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_65_EmployerName_Secondary", X, Y, CharSize)
        UB04_65_EmployerName_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_65_EmployerName_Tertiary", X, Y, CharSize)
        UB04_65_EmployerName_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_66_ICDVersionIndicator", X, Y, CharSize)
        UB04_66_ICDVersionIndicator = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_67_PrincipalDiagnosisCode", X, Y, CharSize)
        UB04_67_PrincipalDiagnosisCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67a_OtherDiagnosis_A", X, Y, CharSize)
        UB04_67a_OtherDiagnosis_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67b_OtherDiagnosis_B", X, Y, CharSize)
        UB04_67b_OtherDiagnosis_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67c_OtherDiagnosis_C", X, Y, CharSize)
        UB04_67c_OtherDiagnosis_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67d_OtherDiagnosis_D", X, Y, CharSize)
        UB04_67d_OtherDiagnosis_D = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67e_OtherDiagnosis_E", X, Y, CharSize)
        UB04_67e_OtherDiagnosis_E = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67f_OtherDiagnosis_F", X, Y, CharSize)
        UB04_67f_OtherDiagnosis_F = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67g_OtherDiagnosis_G", X, Y, CharSize)
        UB04_67g_OtherDiagnosis_G = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67h_OtherDiagnosis_H", X, Y, CharSize)
        UB04_67h_OtherDiagnosis_H = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67i_OtherDiagnosis_I", X, Y, CharSize)
        UB04_67i_OtherDiagnosis_I = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67j_OtherDiagnosis_J", X, Y, CharSize)
        UB04_67j_OtherDiagnosis_J = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67k_OtherDiagnosis_K", X, Y, CharSize)
        UB04_67k_OtherDiagnosis_K = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67l_OtherDiagnosis_L", X, Y, CharSize)
        UB04_67l_OtherDiagnosis_L = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67m_OtherDiagnosis_M", X, Y, CharSize)
        UB04_67m_OtherDiagnosis_M = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67n_OtherDiagnosis_N", X, Y, CharSize)
        UB04_67n_OtherDiagnosis_N = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67o_OtherDiagnosis_O", X, Y, CharSize)
        UB04_67o_OtherDiagnosis_O = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67p_OtherDiagnosis_P", X, Y, CharSize)
        UB04_67p_OtherDiagnosis_P = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_67q_OtherDiagnosis_Q", X, Y, CharSize)
        UB04_67q_OtherDiagnosis_Q = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_68_Reserved_68A", X, Y, CharSize)
        UB04_68_Reserved_68A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_68_Reserved_68B", X, Y, CharSize)
        UB04_68_Reserved_68B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_69_AdmittingDiagnosisCode", X, Y, CharSize)
        UB04_69_AdmittingDiagnosisCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_70a_PatientVisitReason_A", X, Y, CharSize)
        UB04_70a_PatientVisitReason_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_70b_PatientVisitReason_B", X, Y, CharSize)
        UB04_70b_PatientVisitReason_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_70c_PatientVisitReason_C", X, Y, CharSize)
        UB04_70c_PatientVisitReason_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_71_PPSCode", X, Y, CharSize)
        UB04_71_PPSCode = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_72a_ExternalCauseofInjuryCode_A", X, Y, CharSize)
        UB04_72a_ExternalCauseofInjuryCode_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_72b_ExternalCauseofInjuryCode_B", X, Y, CharSize)
        UB04_72b_ExternalCauseofInjuryCode_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_72c_ExternalCauseofInjuryCode_C", X, Y, CharSize)
        UB04_72c_ExternalCauseofInjuryCode_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_73_ReservedFL73", X, Y, CharSize)
        UB04_73_ReservedFL73 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74_ProcedureCode_Principal", X, Y, CharSize)
        UB04_74_ProcedureCode_Principal = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74_ProcedureDate_Principal", X, Y, CharSize)
        UB04_74_ProcedureDate_Principal = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74a_ProcedureCode_OtherA", X, Y, CharSize)
        UB04_74a_ProcedureCode_OtherA = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74a_ProcedureDate_OtherA", X, Y, CharSize)
        UB04_74a_ProcedureDate_OtherA = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74b_ProcedureCode_OtherB", X, Y, CharSize)
        UB04_74b_ProcedureCode_OtherB = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74b_ProcedureDate_OtherB", X, Y, CharSize)
        UB04_74b_ProcedureDate_OtherB = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74c_ProcedureCode_OtherC", X, Y, CharSize)
        UB04_74c_ProcedureCode_OtherC = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74c_ProcedureDate_OtherC", X, Y, CharSize)
        UB04_74c_ProcedureDate_OtherC = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74d_ProcedureCode_OtherD", X, Y, CharSize)
        UB04_74d_ProcedureCode_OtherD = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74d_ProcedureDate_OtherD", X, Y, CharSize)
        UB04_74d_ProcedureDate_OtherD = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74e_ProcedureCode_OtherE", X, Y, CharSize)
        UB04_74e_ProcedureCode_OtherE = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_74e_ProcedureDate_OtherE", X, Y, CharSize)
        UB04_74e_ProcedureDate_OtherE = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_75a_ReservedFL75A", X, Y, CharSize)
        UB04_75a_ReservedFL75A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_75b_ReservedFL75B", X, Y, CharSize)
        UB04_75b_ReservedFL75B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_75c_ReservedFL75C", X, Y, CharSize)
        UB04_75c_ReservedFL75C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_75d_ReservedFL75D", X, Y, CharSize)
        UB04_75d_ReservedFL75D = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_76_AttendingNPI", X, Y, CharSize)
        UB04_76_AttendingNPI = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_76_AttendingQUAL", X, Y, CharSize)
        UB04_76_AttendingQUAL = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_76_AttendingID", X, Y, CharSize)
        UB04_76_AttendingID = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_76a_AttendingLast", X, Y, CharSize)
        UB04_76a_AttendingLast = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_76b_AttendingFirst", X, Y, CharSize)
        UB04_76b_AttendingFirst = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_77_OperatingNPI", X, Y, CharSize)
        UB04_77_OperatingNPI = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_77_OperatingQUAL", X, Y, CharSize)
        UB04_77_OperatingQUAL = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_77_OperatingID", X, Y, CharSize)
        UB04_77_OperatingID = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_77a_OperatingLast", X, Y, CharSize)
        UB04_77a_OperatingLast = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_77b_OperatingFirst", X, Y, CharSize)
        UB04_77b_OperatingFirst = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherNPI", X, Y, CharSize)
        UB04_78_OtherNPI = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherQUAL", X, Y, CharSize)
        UB04_78_OtherQUAL = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherID", X, Y, CharSize)
        UB04_78_OtherID = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherLast", X, Y, CharSize)
        UB04_78_OtherLast = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherFirst", X, Y, CharSize)
        UB04_78_OtherFirst = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_78_OtherProvider_QUAL", X, Y, CharSize)
        UB04_78_OtherProvider_QUAL = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_79_OtherNPI", X, Y, CharSize)
        UB04_79_OtherNPI = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_79_OtherQUAL", X, Y, CharSize)
        UB04_79_OtherQUAL = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_79_OtherID", X, Y, CharSize)
        UB04_79_OtherID = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_79_OtherLast", X, Y, CharSize)
        UB04_79_OtherLast = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_79_OtherFirst", X, Y, CharSize)
        UB04_79_OtherFirst = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_79_OtherProvider_QUAL", X, Y, CharSize)
        UB04_79_OtherProvider_QUAL = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("PayerCodeA_Primary", X, Y, CharSize)
        PayerCodeA_Primary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("PayerCodeB_Secondary", X, Y, CharSize)
        PayerCodeB_Secondary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("PayerCodeC_Tertiary", X, Y, CharSize)
        PayerCodeC_Tertiary = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_80a_Remarks_1", X, Y, CharSize)
        UB04_80a_Remarks_1 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_80b_Remarks_2", X, Y, CharSize)
        UB04_80b_Remarks_2 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_80c_Remarks_3", X, Y, CharSize)
        UB04_80c_Remarks_3 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_80d_Remarks_4", X, Y, CharSize)
        UB04_80d_Remarks_4 = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81a_Code_Code_QUAL_A", X, Y, CharSize)
        UB04_81a_Code_Code_QUAL_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81a_Code_Code_CODE_A", X, Y, CharSize)
        UB04_81a_Code_Code_CODE_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81a_Code_Code_VALUE_A", X, Y, CharSize)
        UB04_81a_Code_Code_VALUE_A = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81b_Code_Code_QUAL_B", X, Y, CharSize)
        UB04_81b_Code_Code_QUAL_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81b_Code_Code_CODE_B", X, Y, CharSize)
        UB04_81b_Code_Code_CODE_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81b_Code_Code_VALUE_B", X, Y, CharSize)
        UB04_81b_Code_Code_VALUE_B = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81c_Code_Code_QUAL_C", X, Y, CharSize)
        UB04_81c_Code_Code_QUAL_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81c_Code_Code_CODE_C", X, Y, CharSize)
        UB04_81c_Code_Code_CODE_C = New UB04FormFieldString(X, Y, CharSize)


        GetCoordinates("UB04_81c_Code_Code_VALUE_C", X, Y, CharSize)
        UB04_81c_Code_Code_VALUE_C = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81d_Code_Code_QUAL_D", X, Y, CharSize)
        UB04_81d_Code_Code_QUAL_D = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81d_Code_Code_CODE_D", X, Y, CharSize)
        UB04_81d_Code_Code_CODE_D = New UB04FormFieldString(X, Y, CharSize)

        GetCoordinates("UB04_81d_Code_Code_VALUE_D", X, Y, CharSize)
        UB04_81d_Code_Code_VALUE_D = New UB04FormFieldString(X, Y, CharSize)


    End Sub

    Private Function GetPrinterSetting(Optional ByVal _sPrinterName As String = "Default") As DataTable
        Try
            Dim _Query As String = ""
            Dim dtSettings As DataTable
            Dim oDB As New gloDatabaseLayer.DBLayer(_DataBaseConnectionString)
            _Query = " SELECT ISNULL(sBoxName,'') AS sBoxName, ISNULL(nXCoordinate,0) AS nXCoordinate, ISNULL(nYCoordinate,0) AS nYCoordinate, ISNULL(sPrinterName,'') AS sPrinterName, ISNULL(nCharSize,0) AS nCharSize " & " FROM BL_UB04PrintSettings WHERE nClinicID = 1 and sPrinterName = '" + _sPrinterName + "' AND nFormType = 1"
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

    Private Sub GetCoordinates(ByVal sBoxName As [String], ByRef nX As Int32, ByRef nY As Int32, ByRef nCharSize As Int32)
        Try
            nX = 0
            nY = 0
            nCharSize = 0
            Dim oView As DataView
            Dim oSetting As New DataTable()
            If dtSettings IsNot Nothing AndAlso dtSettings.Rows.Count > 0 Then
                oView = dtSettings.DefaultView
                oView.RowFilter = "sBoxName = '" & sBoxName & "'"
                oSetting = oView.ToTable()
                If oSetting.Rows.Count > 0 Then
                    nX = Convert.ToInt32(oSetting.Rows(0)("nXCoordinate"))
                    nY = Convert.ToInt32(oSetting.Rows(0)("nYCoordinate"))
                    nCharSize = Convert.ToInt32(oSetting.Rows(0)("nCharSize"))
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Throw ex
        End Try
    End Sub
#End Region

End Class

Public Class UB04FormFieldString
#Region "Constructor & Destructor"

    Public Sub New()
        ' _FieldValue = ""
        _LocationX = 0
        _LocationY = 0
        _CharSize = 0
    End Sub
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

    Public Property Location() As System.Drawing.PointF
        Get
            Return New System.Drawing.PointF(_LocationX, _LocationY)
        End Get
        Set(ByVal value As System.Drawing.PointF)
            _LocationX = value.X
            _LocationY = value.Y

        End Set
    End Property
    Public Property CharSize() As Single
        Get
            Return _CharSize
        End Get
        Set(ByVal value As Single)
            _CharSize = value
        End Set
    End Property


    'Public Property Y() As Single

    '    Get
    '        Return Y
    '    End Get
    '    Set(ByVal value As Single)
    '        _LocationY = Y
    '    End Set
    'End Property


End Class

Public Class UB04FormFieldBoolean
#Region "Constructor & Destructor"

    Public Sub New(ByVal PointX As Single, ByVal PointY As Single)
        _LocationX = PointX
        _LocationY = PointY
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

End Class


Public Class gloPrintUB04PaperForm
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

    Private oSourceUB04 As Bitmap = Nothing
    Private oGraphics As Graphics = Nothing
    Private oCoordinte As New gloUB04PaperForm
    Dim arialRegular As Font = Nothing
    Dim arialBold As Font = Nothing
    Dim toCreateEMF As Boolean = (gloGlobal.gloTSPrint.isCopyPrint And gloGlobal.gloTSPrint.UseEMFForClaims)
    Dim arialFontSmallHeight As Single = 8.5F
    Dim arialFontBigHeight As Single = 24.0F
    Private _oUBForm As gloUB04PaperForm = Nothing

    Private Sub GetFontHeight()
        Using oGraphics = Graphics.FromImage(oSourceUB04)
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

    Private Function CreateEMFUB04Form(thisGraphics As Graphics, bmpWidth As Single, bmpHeight As Single) As Integer
        Try
            oGraphics = thisGraphics
            AdjustFontHeight()
            '#Region "Write Respective Data on Image"
            ' Write Data Image region shifted to following Method, 

            oGraphics.Clear(Color.White)

            ' Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
            WriteRespectiveData(_oUBForm)
            '#End Region

            Return 0
        Catch
            Return 1
        End Try

    End Function

    Public Function PrintUB04Form(ByVal oUBForm As gloUB04PaperForm) As String
        Dim _result As String = ""
        Dim _printFilePath As String = ""
        _oUBForm = oUBForm
        Try

            'If File.Exists(SourceFilePath) = True Then
            'Dim oFileInfo As New FileInfo(SourceFilePath)
            _printFilePath = (Application.StartupPath & "\") + "UB04Test" & (If(toCreateEMF, ".emf", ".tif"))
            If File.Exists(_printFilePath) = True Then
                File.Delete(_printFilePath)
            End If

            'oFileInfo = Nothing

            'If File.Exists(SourceFilePath) = True Then

            'oSourceHCFA1500 = new Bitmap(SourceFilePath);
            ''oSourceHCFA1500 = New Bitmap(817, 1057)
            ''''oSourceUB04 = New Bitmap(817 * 2, 1057 * 2) '''for normal image
            ''oSourceUB04 = New Bitmap("D:\TFS2010\UBNEWTest.TIF")
            ''oSourceUB04 = New Bitmap(2550, 3300) '' for new ub04 image
            ''oSourceUB04.SetResolution(300, 300)
            ''arialRegular = New Font("Arial", 8.0F, FontStyle.Bold)
            ''arialBold = New Font("Arial", 24.0F, FontStyle.Regular)

            Dim sFont As String = GetFontSetupSettingformCmsAndUB("UB04 Font")
            Dim sFontSize As String = GetFontSetupSettingformCmsAndUB("UB04 Font Size")
            Dim bIsfontSizeSelectionEnable As Object = Nothing
            bIsfontSizeSelectionEnable = GetFontSetupSettingformCmsAndUB("EnableUB04FontSizeSelection")

            If bIsfontSizeSelectionEnable IsNot Nothing AndAlso Convert.ToString(bIsfontSizeSelectionEnable) <> "" AndAlso Convert.ToBoolean(bIsfontSizeSelectionEnable) Then
                ' oSourceUB04 = new Bitmap(2550, 3300);
                Dim nWidth As Int64 = Convert.ToInt32(850.0F * (300.0F / 96.0F))
                Dim nHeight As Int64 = Convert.ToInt32(1100.0F * (300.0F / 96.0F))

                oSourceUB04 = New Bitmap(Convert.ToInt32(850.0F * (300.0F / 96.0F)), Convert.ToInt32(1100.0F * (300.0F / 96.0F)))
            Else
                oSourceUB04 = New Bitmap(2550, 3300)
            End If
            arialRegular = New Font("Arial", 8.0F, FontStyle.Bold)
            ''arialBold = New Font("Arial", 24.0F, FontStyle.Regular)

            If bIsfontSizeSelectionEnable IsNot Nothing AndAlso bIsfontSizeSelectionEnable <> "" AndAlso Convert.ToBoolean(bIsfontSizeSelectionEnable) Then

                If CheckFontAvailable(sFont, sFontSize) Then
                    arialBold = New Font(If(sFont = "", "Arial", sFont), If(sFontSize = "", 24.0F, ((Single.Parse(sFontSize) * 300.0F) / 96.0F)), FontStyle.Regular)
                Else
                    Dim sNavigation As String = "Install required font on machine OR  Change font setting to default ""Arial"" in gloPM Admin.[Navigation: gloPM Admin> Settings> UB04 Settings] to prevent this dialog in future."
                    Dim sMsg As String = String.Format("Claim form print setting font [Name: ""{0}"", with Style: ""Regular"" or ""Bold""] is not installed on this machine." & vbLf & vbLf & "{1}" & vbLf & vbLf & "Do you want to print data with default font[Name: Arial]?", sFont, sNavigation)
                    If MessageBox.Show(sMsg, "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        arialBold = New Font("Arial", 24.0F, FontStyle.Regular)
                    Else
                        Return String.Empty
                    End If
                End If
            Else
                arialBold = New Font("Arial", 24.0F, FontStyle.Regular)
            End If

            If toCreateEMF Then
                GetFontHeight()
                Dim emfBytes As Byte() = gloGlobal.CreateEMF.GetEMFBytes(CSng(oSourceUB04.Width) / oSourceUB04.HorizontalResolution, CSng(oSourceUB04.Height) / oSourceUB04.VerticalResolution, oSourceUB04.Width, oSourceUB04.Height, AddressOf CreateEMFUB04Form)
                File.WriteAllBytes(_printFilePath, emfBytes)
            Else

                oGraphics = Graphics.FromImage(oSourceUB04)
                WriteRespectiveData(oUBForm)

                oSourceUB04.Save(_printFilePath)

                If oGraphics IsNot Nothing Then
                    oGraphics.Dispose()
                End If
            End If

            If oSourceUB04 IsNot Nothing Then
                oSourceUB04.Dispose()
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

    Private Sub WriteRespectiveData(oUBForm As gloUB04PaperForm)
        ''Provider's Details
        WriteData(oUBForm.UB04_1_ProviderName.Value.ToString(), oUBForm.UB04_1_ProviderName.Location, False)
        WriteData(oUBForm.UB04_1_ProviderAddress.Value.ToString(), oUBForm.UB04_1_ProviderAddress.Location, False)
        WriteData(oUBForm.UB04_1a_ProviderCity.Value.ToString(), oUBForm.UB04_1a_ProviderCity.Location, False)
        WriteData(oUBForm.UB04_1b_ProviderState.Value.ToString(), oUBForm.UB04_1b_ProviderState.Location, False)
        WriteData(oUBForm.UB04_1c_ProviderZipCode.Value.ToString(), oUBForm.UB04_1c_ProviderZipCode.Location, False)
        WriteData(oUBForm.UB04_1b_ProviderFaxNumber.Value.ToString(), oUBForm.UB04_1b_ProviderFaxNumber.Location, False)
        WriteData(oUBForm.UB04_1a_ProviderPhone.Value.ToString(), oUBForm.UB04_1a_ProviderPhone.Location, False)


        ' Payer's Details
        WriteData(oUBForm.UB04_2_PayToName.Value.ToString(), oUBForm.UB04_2_PayToName.Location, False)
        WriteData(oUBForm.UB04_2_PayToAddress.Value.ToString(), oUBForm.UB04_2_PayToAddress.Location, False)
        WriteData(oUBForm.UB04_2a_Pay_toCity.Value.ToString(), oUBForm.UB04_2a_Pay_toCity.Location, False)
        WriteData(oUBForm.UB04_2b_Pay_toState.Value.ToString(), oUBForm.UB04_2b_Pay_toState.Location, False)
        WriteData(oUBForm.UB04_2a_Pay_toZip.Value.ToString(), oUBForm.UB04_2a_Pay_toZip.Location, False)
        WriteData(oUBForm.UB04_2b_ReservedFL02.Value.ToString(), oUBForm.UB04_2b_ReservedFL02.Location, False)

        'Patient Control Number
        WriteData(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, False)
        'WriteData(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, False)
        'WriteData(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, False)
        WriteData(oUBForm.UB04_3b_MedicalHealthRecordNumber.Value.ToString(), oUBForm.UB04_3b_MedicalHealthRecordNumber.Location, False)
        'Type Of Bill
        WriteData(oUBForm.UB04_4_TypeofBill.Value.ToString(), oUBForm.UB04_4_TypeofBill.Location, False)
        WriteData(oUBForm.UB04_4_TypeofBillFrequencyCode.Value.ToString(), oUBForm.UB04_4_TypeofBillFrequencyCode.Location, False)
        'Fedaral Tax Number
        WriteData(oUBForm.UB04_5_FederalTaxNumber_Upperline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Upperline.Location, False)
        WriteData(oUBForm.UB04_5_FederalTaxNumber_Lowerline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Lowerline.Location, False)
        'Statement Covers Period(From - Through)

        WriteData(oUBForm.UB04_6a_StatementCoversPeriod_From.Value.ToString(), oUBForm.UB04_6a_StatementCoversPeriod_From.Location, False)
        WriteData(oUBForm.UB04_6b_StatementCoversPeriod_Through.Value.ToString(), oUBForm.UB04_6b_StatementCoversPeriod_Through.Location, False)
        'Reserved

        WriteData(oUBForm.UB04_7a_ReservedFL07A.Value.ToString(), oUBForm.UB04_7a_ReservedFL07A.Location, False)
        WriteData(oUBForm.UB04_7b_ReservedFL07B.Value.ToString(), oUBForm.UB04_7b_ReservedFL07B.Location, False)
        'Patient Identifier ,Patient Social Security Number

        WriteData(oUBForm.UB04_8_PatientIdentifier.Value.ToString(), oUBForm.UB04_8_PatientIdentifier.Location, False)
        WriteData(oUBForm.UB04_8a_PatientSocialSecurityNumber.Value.ToString(), oUBForm.UB04_8a_PatientSocialSecurityNumber.Location, False)
        WriteData(oUBForm.UB04_8b_PatientName.Value.ToString(), oUBForm.UB04_8b_PatientName.Location, False)
        'Patient Address

        WriteData(oUBForm.UB04_9a_PatientStreetAddress.Value.ToString(), oUBForm.UB04_9a_PatientStreetAddress.Location, False)
        WriteData(oUBForm.UB04_9b_PatientCity.Value.ToString(), oUBForm.UB04_9b_PatientCity.Location, False)
        WriteData(oUBForm.UB04_9c_PatientState.Value.ToString(), oUBForm.UB04_9c_PatientState.Location, False)
        WriteData(oUBForm.UB04_9d_PatientZip.Value.ToString(), oUBForm.UB04_9d_PatientZip.Location, False)
        WriteData(oUBForm.UB04_9e_PatientCountryCode.Value.ToString(), oUBForm.UB04_9e_PatientCountryCode.Location, False)
        'Birth Date

        WriteData(oUBForm.UB04_10_PatientBirthDate.Value.ToString(), oUBForm.UB04_10_PatientBirthDate.Location, False)
        'Gender Admission Date,Admission(Hour),Admission(Type),Admission(Source),Discharge(Hour),Discharge(Status)

        WriteData(oUBForm.UB04_11_PatientGender.Value.ToString(), oUBForm.UB04_11_PatientGender.Location, False)
        WriteData(oUBForm.UB04_11_PatientMaritalStatus.Value.ToString(), oUBForm.UB04_11_PatientMaritalStatus.Location, False)
        WriteData(oUBForm.UB04_11_PatientRace.Value.ToString(), oUBForm.UB04_11_PatientRace.Location, False)
        WriteData(oUBForm.UB04_12_Admission_Visit_StartofCareDate.Value.ToString(), oUBForm.UB04_12_Admission_Visit_StartofCareDate.Location, False)
        WriteData(oUBForm.UB04_13_Admission_Visit_Hour.Value.ToString(), oUBForm.UB04_13_Admission_Visit_Hour.Location, False)
        WriteData(oUBForm.UB04_14_AdmissionType.Value.ToString(), oUBForm.UB04_14_AdmissionType.Location, False)
        WriteData(oUBForm.UB04_15_ReferralSource.Value.ToString(), oUBForm.UB04_15_ReferralSource.Location, False)
        WriteData(oUBForm.UB04_16_DischargeHour.Value.ToString(), oUBForm.UB04_16_DischargeHour.Location, False)
        WriteData(oUBForm.UB04_17_DischargeStatus.Value.ToString(), oUBForm.UB04_17_DischargeStatus.Location, False)
        'Condition Code

        WriteData(oUBForm.UB04_18_ConditionCodes.Value.ToString(), oUBForm.UB04_18_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_19_ConditionCodes.Value.ToString(), oUBForm.UB04_19_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_20_ConditionCodes.Value.ToString(), oUBForm.UB04_20_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_21_ConditionCodes.Value.ToString(), oUBForm.UB04_21_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_22_ConditionCodes.Value.ToString(), oUBForm.UB04_22_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_23_ConditionCodes.Value.ToString(), oUBForm.UB04_23_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_24_ConditionCodes.Value.ToString(), oUBForm.UB04_24_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_25_ConditionCodes.Value.ToString(), oUBForm.UB04_25_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_26_ConditionCodes.Value.ToString(), oUBForm.UB04_26_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_27_ConditionCodes.Value.ToString(), oUBForm.UB04_27_ConditionCodes.Location, False)
        WriteData(oUBForm.UB04_28_ConditionCodes.Value.ToString(), oUBForm.UB04_28_ConditionCodes.Location, False)
        'Accident State
        WriteData(oUBForm.UB04_29_AccidentState.Value.ToString(), oUBForm.UB04_29_AccidentState.Location, False)
        'Reserved
        WriteData(oUBForm.UB04_30_ReservedFL30A.Value.ToString(), oUBForm.UB04_30_ReservedFL30A.Location, False)
        WriteData(oUBForm.UB04_30_ReservedFL30B.Value.ToString(), oUBForm.UB04_30_ReservedFL30B.Location, False)
        'Occurrence Code Details
        WriteData(oUBForm.UB04_31a_OccurrenceCode.Value.ToString(), oUBForm.UB04_31a_OccurrenceCode.Location, False)
        WriteData(oUBForm.UB04_31b_OccurrenceDate.Value.ToString(), oUBForm.UB04_31b_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_31a_OccurrenceDate.Value.ToString(), oUBForm.UB04_31a_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_31b_OccurrenceCode.Value.ToString(), oUBForm.UB04_31b_OccurrenceCode.Location, False)

        WriteData(oUBForm.UB04_32a_OccurrenceCode.Value.ToString(), oUBForm.UB04_32a_OccurrenceCode.Location, False)
        WriteData(oUBForm.UB04_32b_OccurrenceDate.Value.ToString(), oUBForm.UB04_32b_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_32a_OccurrenceDate.Value.ToString(), oUBForm.UB04_32a_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_32b_OccurrenceCode.Value.ToString(), oUBForm.UB04_32b_OccurrenceCode.Location, False)

        WriteData(oUBForm.UB04_33a_OccurrenceCode.Value.ToString(), oUBForm.UB04_33a_OccurrenceCode.Location, False)
        WriteData(oUBForm.UB04_33b_OccurrenceDate.Value.ToString(), oUBForm.UB04_33b_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_33a_OccurrenceDate.Value.ToString(), oUBForm.UB04_33a_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_33b_OccurrenceCode.Value.ToString(), oUBForm.UB04_33b_OccurrenceCode.Location, False)

        WriteData(oUBForm.UB04_34a_OccurrenceCode.Value.ToString(), oUBForm.UB04_34a_OccurrenceCode.Location, False)
        WriteData(oUBForm.UB04_34a_OccurrenceDate.Value.ToString(), oUBForm.UB04_34a_OccurrenceDate.Location, False)
        WriteData(oUBForm.UB04_34b_OccurrenceCode.Value.ToString(), oUBForm.UB04_34b_OccurrenceCode.Location, False)
        WriteData(oUBForm.UB04_34b_OccurrenceDate.Value.ToString(), oUBForm.UB04_34b_OccurrenceDate.Location, False)
        ''WriteData(oUBForm.UB04_34_OccurrenceDate.Value.ToString(), oUBForm.UB04_34_OccurrenceDate.Location, False)


        WriteData(oUBForm.UB04_35a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanCode.Location, False)
        WriteData(oUBForm.UB04_35a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateFrom.Location, False)
        WriteData(oUBForm.UB04_35a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateThrough.Location, False)
        WriteData(oUBForm.UB04_35b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanCode.Location, False)
        WriteData(oUBForm.UB04_35b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateFrom.Location, False)
        WriteData(oUBForm.UB04_35b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateThrough.Location, False)

        WriteData(oUBForm.UB04_36a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanCode.Location, False)
        WriteData(oUBForm.UB04_36a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateFrom.Location, False)
        WriteData(oUBForm.UB04_36a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateThrough.Location, False)
        WriteData(oUBForm.UB04_36b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanCode.Location, False)
        WriteData(oUBForm.UB04_36b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateFrom.Location, False)
        WriteData(oUBForm.UB04_36b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateThrough.Location, False)

        'Future Use
        WriteData(oUBForm.UB04_37_ReservedFL37.Value.ToString(), oUBForm.UB04_37_ReservedFL37.Location, False)
        'Responsible Party Name/Address
        WriteData(oUBForm.UB04_38_ResponsiblePartyNameAddress.Value.ToString(), oUBForm.UB04_38_ResponsiblePartyNameAddress.Location, False)
        'Value Code,Value Code Amount
        WriteData(oUBForm.UB04_39a_ValueCode.Value.ToString(), oUBForm.UB04_39a_ValueCode.Location, False)
        WriteData(oUBForm.UB04_39a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_39a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_39b_ValueCode.Value.ToString(), oUBForm.UB04_39b_ValueCode.Location, False)
        WriteData(oUBForm.UB04_39b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_39b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_39c_ValueCode.Value.ToString(), oUBForm.UB04_39c_ValueCode.Location, False)
        WriteData(oUBForm.UB04_39c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_39c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_39d_ValueCode.Value.ToString(), oUBForm.UB04_39d_ValueCode.Location, False)
        WriteData(oUBForm.UB04_39d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_39d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount_Cents.Location, False)

        WriteData(oUBForm.UB04_40a_ValueCode.Value.ToString(), oUBForm.UB04_40a_ValueCode.Location, False)
        WriteData(oUBForm.UB04_40a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_40a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_40b_ValueCode.Value.ToString(), oUBForm.UB04_40b_ValueCode.Location, False)
        WriteData(oUBForm.UB04_40b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_40b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_40c_ValueCode.Value.ToString(), oUBForm.UB04_40c_ValueCode.Location, False)
        WriteData(oUBForm.UB04_40c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_40c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_40d_ValueCode.Value.ToString(), oUBForm.UB04_40d_ValueCode.Location, False)
        WriteData(oUBForm.UB04_40d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_40d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount_Cents.Location, False)


        WriteData(oUBForm.UB04_41a_ValueCode.Value.ToString(), oUBForm.UB04_41a_ValueCode.Location, False)
        WriteData(oUBForm.UB04_41a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_41a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_41b_ValueCode.Value.ToString(), oUBForm.UB04_41b_ValueCode.Location, False)
        WriteData(oUBForm.UB04_41b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_41b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_41c_ValueCode.Value.ToString(), oUBForm.UB04_41c_ValueCode.Location, False)
        WriteData(oUBForm.UB04_41c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_41c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount_Cents.Location, False)
        WriteData(oUBForm.UB04_41d_ValueCode.Value.ToString(), oUBForm.UB04_41d_ValueCode.Location, False)
        WriteData(oUBForm.UB04_41d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount.Location, False)
        WriteData(oUBForm.UB04_41d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount_Cents.Location, False)
        'Revenue Code

        Dim _Height As Single
        _Height = 100

        If Not oUBForm.UB04_ServiceLines Is Nothing Then
            For index As Integer = 0 To (oUBForm.UB04_ServiceLines.Count - 1)
                If (index > 0) Then
                    _Height = oUBForm.UB04_ServiceLines(index).UB04_42_RevenueCode.Location.Y()
                End If
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_42_RevenueCode.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_42_RevenueCode.Location, False)
                ''oUBForm.UB04_ServiceLines(index).UB04_42_RevenueCode.Location.Y = _Height
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_43_RevenueCodeDescription.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_43_RevenueCodeDescription.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_44_RateCodes.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_44_RateCodes.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_45_ServiceDate_visit.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_45_ServiceDate_visit.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_46_ServiceUnits.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_46_ServiceUnits.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_47a_TotalCharges_Dollars.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_47a_TotalCharges_Dollars.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_47b_TotalCharges_Cents.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_47b_TotalCharges_Cents.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_48a_Non_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_48a_Non_coveredCharges_Dollars.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_48b_Non_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_48b_Non_coveredCharges_Cents.Location, False)
                WriteData(oUBForm.UB04_ServiceLines(index).UB04_49_ReservedFL49.Value.ToString(), oUBForm.UB04_ServiceLines(index).UB04_49_ReservedFL49.Location, False)
            Next
        End If

        WriteData(oUBForm.UB04_42L23_RevenueCode.Value.ToString(), oUBForm.UB04_42L23_RevenueCode.Location, False)
        WriteData(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location, False)
        WriteData(oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Location, False)
        WriteData(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Location, False)
        WriteData(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Location, False)
        WriteData(oUBForm.UB04_49L23_Reserved49L23.Value.ToString(), oUBForm.UB04_49L23_Reserved49L23.Location, False)
        WriteData(oUBForm.UB04_43L23_CurrentPage.Value.ToString(), oUBForm.UB04_43L23_CurrentPage.Location, False)
        WriteData(oUBForm.UB04_44L23_TotalPages.Value.ToString(), oUBForm.UB04_44L23_TotalPages.Location, False)
        WriteData(oUBForm.UB04_44L23CreationDate.Value.ToString(), oUBForm.UB04_44L23CreationDate.Location, False)
        'Primary,Secondary,Tertiary Payer Name
        WriteData(oUBForm.UB04_50_PayerName_Primary.Value.ToString(), oUBForm.UB04_50_PayerName_Primary.Location, False)
        WriteData(oUBForm.UB04_50_PayerName_Secondary.Value.ToString(), oUBForm.UB04_50_PayerName_Secondary.Location, False)
        WriteData(oUBForm.UB04_50_PayerName_Tertiary.Value.ToString(), oUBForm.UB04_50_PayerName_Tertiary.Location, False)
        'Helth Plan ID A,B,C,Release of information.,Assignment of benefits.

        WriteData(oUBForm.UB04_51_HealthPlanIDA.Value.ToString(), oUBForm.UB04_51_HealthPlanIDA.Location, False)
        WriteData(oUBForm.UB04_51_HealthPlanIDB.Value.ToString(), oUBForm.UB04_51_HealthPlanIDB.Location, False)
        WriteData(oUBForm.UB04_51_HealthPlanIDC.Value.ToString(), oUBForm.UB04_51_HealthPlanIDC.Location, False)
        WriteData(oUBForm.UB04_52_InformationRelease_Primary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Primary.Location, False)
        WriteData(oUBForm.UB04_52_InformationRelease_Secondary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Secondary.Location, False)
        WriteData(oUBForm.UB04_52_InformationRelease_Tertiary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Tertiary.Location, False)
        WriteData(oUBForm.UB04_53_BenefitsAssignment_Primary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Primary.Location, False)
        WriteData(oUBForm.UB04_53_BenefitsAssignment_Secondary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Secondary.Location, False)
        WriteData(oUBForm.UB04_53_BenefitsAssignment_Tertiary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Tertiary.Location, False)
        'Prior Payment

        WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location, False)
        WriteData(oUBForm.UB04_54_PriorPaymentsCents_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Primary.Location, False)
        WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location, False)
        WriteData(oUBForm.UB04_54_PriorPaymentsCents_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Secondary.Location, False)
        WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location, False)
        WriteData(oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Location, False)
        'Estimated amount due.
        WriteData(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location, False)
        WriteData(oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Location, False)
        WriteData(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location, False)
        WriteData(oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Location, False)
        WriteData(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location, False)
        WriteData(oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Location, False)
        'NPI Number
        WriteData(oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Value.ToString(), oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Location, False)
        WriteData(oUBForm.UB04_57_OtherProvider_Primary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Primary.Location, False)
        WriteData(oUBForm.UB04_57_OtherProvider_Secondary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Secondary.Location, False)
        WriteData(oUBForm.UB04_57_OtherProvider_Tertiary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Tertiary.Location, False)
        'Othere Provider ID,Insured Name Primary,Secondary ,Tertiary,Patient Relationship,Insured Unique ID ,Group Name,Group Number

        WriteData(oUBForm.UB04_58_InsuredName_Primary.Value.ToString(), oUBForm.UB04_58_InsuredName_Primary.Location, False)
        WriteData(oUBForm.UB04_58_InsuredName_Secondary.Value.ToString(), oUBForm.UB04_58_InsuredName_Secondary.Location, False)
        WriteData(oUBForm.UB04_58_InsuredName_Tertiary.Value.ToString(), oUBForm.UB04_58_InsuredName_Tertiary.Location, False)
        WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Location, False)
        WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Location, False)
        WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Location, False)
        WriteData(oUBForm.UB04_60_InsuredUniqueID_Primary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Primary.Location, False)
        WriteData(oUBForm.UB04_60_InsuredUniqueID_Secondary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Secondary.Location, False)
        WriteData(oUBForm.UB04_60_InsuredUniqueID_Tertiary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Tertiary.Location, False)
        WriteData(oUBForm.UB04_61_InsuredGroupName_Primary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Primary.Location, False)
        WriteData(oUBForm.UB04_61_InsuredGroupName_Secondary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Secondary.Location, False)
        WriteData(oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Tertiary.Location, False)
        WriteData(oUBForm.UB04_62_InsuredGroupNumber_Primary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Primary.Location, False)
        WriteData(oUBForm.UB04_62_InsuredGroupNumber_Secondary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Secondary.Location, False)
        WriteData(oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Location, False)
        'Treatment Authorization Code Primary,Secondary,Tertiary,Document Control Number,Employer Name

        WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Location, False)
        WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Location, False)
        WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Location, False)
        WriteData(oUBForm.UB04_64_DocumentControlNumber_A.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_A.Location, False)
        WriteData(oUBForm.UB04_64_DocumentControlNumber_B.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_B.Location, False)
        WriteData(oUBForm.UB04_64_DocumentControlNumber_C.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_C.Location, False)
        WriteData(oUBForm.UB04_65_EmployerName_Primary.Value.ToString(), oUBForm.UB04_65_EmployerName_Primary.Location, False)
        WriteData(oUBForm.UB04_65_EmployerName_Secondary.Value.ToString(), oUBForm.UB04_65_EmployerName_Secondary.Location, False)
        WriteData(oUBForm.UB04_65_EmployerName_Tertiary.Value.ToString(), oUBForm.UB04_65_EmployerName_Tertiary.Location, False)
        'ICD Version Indicator,Principal Diagnosis Code,Reserved,Admitting diagnosis.,Patient Visit Reason,PPS(Code),External Cause of Injury Code,Procedure(Code)

        WriteData(oUBForm.UB04_66_ICDVersionIndicator.Value.ToString(), oUBForm.UB04_66_ICDVersionIndicator.Location, False)
        WriteData(oUBForm.UB04_67_PrincipalDiagnosisCode.Value.ToString(), oUBForm.UB04_67_PrincipalDiagnosisCode.Location, False)
        WriteData(oUBForm.UB04_67a_OtherDiagnosis_A.Value.ToString(), oUBForm.UB04_67a_OtherDiagnosis_A.Location, False)
        WriteData(oUBForm.UB04_67b_OtherDiagnosis_B.Value.ToString(), oUBForm.UB04_67b_OtherDiagnosis_B.Location, False)
        WriteData(oUBForm.UB04_67c_OtherDiagnosis_C.Value.ToString(), oUBForm.UB04_67c_OtherDiagnosis_C.Location, False)
        WriteData(oUBForm.UB04_67d_OtherDiagnosis_D.Value.ToString(), oUBForm.UB04_67d_OtherDiagnosis_D.Location, False)
        WriteData(oUBForm.UB04_67e_OtherDiagnosis_E.Value.ToString(), oUBForm.UB04_67e_OtherDiagnosis_E.Location, False)
        WriteData(oUBForm.UB04_67f_OtherDiagnosis_F.Value.ToString(), oUBForm.UB04_67f_OtherDiagnosis_F.Location, False)
        WriteData(oUBForm.UB04_67g_OtherDiagnosis_G.Value.ToString(), oUBForm.UB04_67g_OtherDiagnosis_G.Location, False)
        WriteData(oUBForm.UB04_67h_OtherDiagnosis_H.Value.ToString(), oUBForm.UB04_67h_OtherDiagnosis_H.Location, False)
        WriteData(oUBForm.UB04_67i_OtherDiagnosis_I.Value.ToString(), oUBForm.UB04_67i_OtherDiagnosis_I.Location, False)
        WriteData(oUBForm.UB04_67j_OtherDiagnosis_J.Value.ToString(), oUBForm.UB04_67j_OtherDiagnosis_J.Location, False)
        WriteData(oUBForm.UB04_67k_OtherDiagnosis_K.Value.ToString(), oUBForm.UB04_67k_OtherDiagnosis_K.Location, False)
        WriteData(oUBForm.UB04_67l_OtherDiagnosis_L.Value.ToString(), oUBForm.UB04_67l_OtherDiagnosis_L.Location, False)
        WriteData(oUBForm.UB04_67m_OtherDiagnosis_M.Value.ToString(), oUBForm.UB04_67m_OtherDiagnosis_M.Location, False)
        WriteData(oUBForm.UB04_67n_OtherDiagnosis_N.Value.ToString(), oUBForm.UB04_67n_OtherDiagnosis_N.Location, False)
        WriteData(oUBForm.UB04_67o_OtherDiagnosis_O.Value.ToString(), oUBForm.UB04_67o_OtherDiagnosis_O.Location, False)
        WriteData(oUBForm.UB04_67p_OtherDiagnosis_P.Value.ToString(), oUBForm.UB04_67p_OtherDiagnosis_P.Location, False)
        WriteData(oUBForm.UB04_67q_OtherDiagnosis_Q.Value.ToString(), oUBForm.UB04_67q_OtherDiagnosis_Q.Location, False)
        WriteData(oUBForm.UB04_68_Reserved_68A.Value.ToString(), oUBForm.UB04_68_Reserved_68A.Location, False)
        WriteData(oUBForm.UB04_68_Reserved_68B.Value.ToString(), oUBForm.UB04_68_Reserved_68B.Location, False)
        WriteData(oUBForm.UB04_69_AdmittingDiagnosisCode.Value.ToString(), oUBForm.UB04_69_AdmittingDiagnosisCode.Location, False)
        WriteData(oUBForm.UB04_70a_PatientVisitReason_A.Value.ToString(), oUBForm.UB04_70a_PatientVisitReason_A.Location, False)
        WriteData(oUBForm.UB04_70b_PatientVisitReason_B.Value.ToString(), oUBForm.UB04_70b_PatientVisitReason_B.Location, False)
        WriteData(oUBForm.UB04_70c_PatientVisitReason_C.Value.ToString(), oUBForm.UB04_70c_PatientVisitReason_C.Location, False)
        WriteData(oUBForm.UB04_71_PPSCode.Value.ToString(), oUBForm.UB04_71_PPSCode.Location, False)
        WriteData(oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Value.ToString(), oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Location, False)
        WriteData(oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Value.ToString(), oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Location, False)
        WriteData(oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Value.ToString(), oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Location, False)
        WriteData(oUBForm.UB04_73_ReservedFL73.Value.ToString(), oUBForm.UB04_73_ReservedFL73.Location, False)
        WriteData(oUBForm.UB04_74_ProcedureCode_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureCode_Principal.Location, False)
        WriteData(oUBForm.UB04_74_ProcedureDate_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureDate_Principal.Location, False)
        WriteData(oUBForm.UB04_74a_ProcedureCode_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureCode_OtherA.Location, False)
        WriteData(oUBForm.UB04_74a_ProcedureDate_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureDate_OtherA.Location, False)
        WriteData(oUBForm.UB04_74b_ProcedureCode_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureCode_OtherB.Location, False)
        WriteData(oUBForm.UB04_74b_ProcedureDate_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureDate_OtherB.Location, False)
        WriteData(oUBForm.UB04_74c_ProcedureCode_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureCode_OtherC.Location, False)
        WriteData(oUBForm.UB04_74c_ProcedureDate_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureDate_OtherC.Location, False)
        WriteData(oUBForm.UB04_74d_ProcedureCode_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureCode_OtherD.Location, False)
        WriteData(oUBForm.UB04_74d_ProcedureDate_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureDate_OtherD.Location, False)
        WriteData(oUBForm.UB04_74e_ProcedureCode_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureCode_OtherE.Location, False)
        WriteData(oUBForm.UB04_74e_ProcedureDate_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureDate_OtherE.Location, False)
        WriteData(oUBForm.UB04_75a_ReservedFL75A.Value.ToString(), oUBForm.UB04_75a_ReservedFL75A.Location, False)
        WriteData(oUBForm.UB04_75b_ReservedFL75B.Value.ToString(), oUBForm.UB04_75b_ReservedFL75B.Location, False)
        WriteData(oUBForm.UB04_75c_ReservedFL75C.Value.ToString(), oUBForm.UB04_75c_ReservedFL75C.Location, False)
        WriteData(oUBForm.UB04_75d_ReservedFL75D.Value.ToString(), oUBForm.UB04_75d_ReservedFL75D.Location, False)
        ''Attending provider and identifiers,Operating provider and identifiers,Other provider name and identifiers.,other provider name and identifiers.


        WriteData(oUBForm.UB04_76_AttendingNPI.Value.ToString(), oUBForm.UB04_76_AttendingNPI.Location, False)
        WriteData(oUBForm.UB04_76_AttendingQUAL.Value.ToString(), oUBForm.UB04_76_AttendingQUAL.Location, False)
        WriteData(oUBForm.UB04_76_AttendingID.Value.ToString(), oUBForm.UB04_76_AttendingID.Location, False)
        WriteData(oUBForm.UB04_76a_AttendingLast.Value.ToString(), oUBForm.UB04_76a_AttendingLast.Location, False)
        WriteData(oUBForm.UB04_76b_AttendingFirst.Value.ToString(), oUBForm.UB04_76b_AttendingFirst.Location, False)
        WriteData(oUBForm.UB04_77_OperatingNPI.Value.ToString(), oUBForm.UB04_77_OperatingNPI.Location, False)
        WriteData(oUBForm.UB04_77_OperatingQUAL.Value.ToString(), oUBForm.UB04_77_OperatingQUAL.Location, False)
        WriteData(oUBForm.UB04_77_OperatingID.Value.ToString(), oUBForm.UB04_77_OperatingID.Location, False)
        WriteData(oUBForm.UB04_77a_OperatingLast.Value.ToString(), oUBForm.UB04_77a_OperatingLast.Location, False)
        WriteData(oUBForm.UB04_77b_OperatingFirst.Value.ToString(), oUBForm.UB04_77b_OperatingFirst.Location, False)

        WriteData(oUBForm.UB04_78_OtherNPI.Value.ToString(), oUBForm.UB04_78_OtherNPI.Location, False)
        WriteData(oUBForm.UB04_78_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_78_OtherProvider_QUAL.Location, False)
        WriteData(oUBForm.UB04_78_OtherQUAL.Value.ToString(), oUBForm.UB04_78_OtherQUAL.Location, False)
        WriteData(oUBForm.UB04_78_OtherID.Value.ToString(), oUBForm.UB04_78_OtherID.Location, False)
        WriteData(oUBForm.UB04_78_OtherLast.Value.ToString(), oUBForm.UB04_78_OtherLast.Location, False)
        WriteData(oUBForm.UB04_78_OtherFirst.Value.ToString(), oUBForm.UB04_78_OtherFirst.Location, False)

        WriteData(oUBForm.UB04_79_OtherNPI.Value.ToString(), oUBForm.UB04_79_OtherNPI.Location, False)
        WriteData(oUBForm.UB04_79_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_79_OtherProvider_QUAL.Location, False)
        WriteData(oUBForm.UB04_79_OtherQUAL.Value.ToString(), oUBForm.UB04_79_OtherQUAL.Location, False)
        WriteData(oUBForm.UB04_79_OtherID.Value.ToString(), oUBForm.UB04_79_OtherID.Location, False)
        WriteData(oUBForm.UB04_79_OtherLast.Value.ToString(), oUBForm.UB04_79_OtherLast.Location, False)
        WriteData(oUBForm.UB04_79_OtherFirst.Value.ToString(), oUBForm.UB04_79_OtherFirst.Location, False)

        WriteData(oUBForm.PayerCodeA_Primary.Value.ToString(), oUBForm.PayerCodeA_Primary.Location, False)
        WriteData(oUBForm.PayerCodeB_Secondary.Value.ToString(), oUBForm.PayerCodeB_Secondary.Location, False)
        WriteData(oUBForm.PayerCodeC_Tertiary.Value.ToString(), oUBForm.PayerCodeC_Tertiary.Location, False)
        ''Remark
        WriteData(oUBForm.UB04_80a_Remarks_1.Value.ToString(), oUBForm.UB04_80a_Remarks_1.Location, False)
        WriteData(oUBForm.UB04_80b_Remarks_2.Value.ToString(), oUBForm.UB04_80b_Remarks_2.Location, False)
        WriteData(oUBForm.UB04_80c_Remarks_3.Value.ToString(), oUBForm.UB04_80c_Remarks_3.Location, False)
        WriteData(oUBForm.UB04_80d_Remarks_4.Value.ToString(), oUBForm.UB04_80d_Remarks_4.Location, False)
        WriteData(oUBForm.UB04_81a_Code_Code_QUAL_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_QUAL_A.Location, False)
        WriteData(oUBForm.UB04_81a_Code_Code_CODE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_CODE_A.Location, False)
        WriteData(oUBForm.UB04_81a_Code_Code_VALUE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_VALUE_A.Location, False)
        WriteData(oUBForm.UB04_81b_Code_Code_QUAL_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_QUAL_B.Location, False)
        WriteData(oUBForm.UB04_81b_Code_Code_CODE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_CODE_B.Location, False)
        WriteData(oUBForm.UB04_81b_Code_Code_VALUE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_VALUE_B.Location, False)
        WriteData(oUBForm.UB04_81c_Code_Code_QUAL_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_QUAL_C.Location, False)
        WriteData(oUBForm.UB04_81c_Code_Code_CODE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_CODE_C.Location, False)
        WriteData(oUBForm.UB04_81c_Code_Code_VALUE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_VALUE_C.Location, False)
        WriteData(oUBForm.UB04_81d_Code_Code_QUAL_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_QUAL_D.Location, False)
        WriteData(oUBForm.UB04_81d_Code_Code_CODE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_CODE_D.Location, False)
        WriteData(oUBForm.UB04_81d_Code_Code_VALUE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_VALUE_D.Location, False)

    End Sub


    Public Function PrintUB04Form(ByVal oUB04Form As gloUB04PaperForm, ByVal SourceFilePath As String, ByVal DestinationFilePath As String) As String
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

                    oSourceUB04 = New Bitmap(SourceFilePath)
                    oGraphics = Graphics.FromImage(oSourceUB04)

                    '#Region "Write Data on Image"
                    '.Insurance Type"

                    '#End Region

                    oSourceUB04.Save(_printFilePath)

                    If oGraphics IsNot Nothing Then
                        oGraphics.Dispose()
                    End If
                    If oSourceUB04 IsNot Nothing Then
                        oSourceUB04.Dispose()
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
        Dim oPoint As New PointF(location.X, location.Y)
        If isboolean = False Then
            If data <> "" Then
                oGraphics.DrawString(data, arialBold, Brushes.Black, oPoint)
            End If

        Else
            If data.ToUpper() = "TRUE" Then
                oGraphics.DrawString("X", arialBold, Brushes.Black, oPoint)
            End If
        End If
    End Sub

    Public Function GetFontSetupSettingformCmsAndUB(sSettingName As String) As String
        Dim ogloSettings As New gloSettings.GeneralSettings(mdlGeneral.GetConnectionString)
        Dim value As New Object()
        Try
            ogloSettings.GetSetting(sSettingName, value)
            If value IsNot Nothing AndAlso Convert.ToString(value).Trim() <> "" Then
                ' value = null;
                Return Convert.ToString(value)
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            Return ""
        Finally
            ogloSettings.Dispose()
            ogloSettings = Nothing
            value = Nothing
        End Try
    End Function

    Private Function CheckFontAvailable(sFontName As String, sFontSize As String) As Boolean
        Dim _result As [Boolean] = False
        Using fontTester As New Font(sFontName, Single.Parse(sFontSize), FontStyle.Regular, GraphicsUnit.Pixel)
            If fontTester.Name = sFontName Then
                _result = True
            Else
                _result = False
            End If
        End Using
        Return _result
    End Function
End Class


Public Class ServiceLine

#Region "Constructor & Destructor "
    Public Sub New()
        UB04_42_RevenueCode = New UB04FormFieldString()
        UB04_43_RevenueCodeDescription = New UB04FormFieldString()
        UB04_44_RateCodes = New UB04FormFieldString()
        UB04_45_ServiceDate_visit = New UB04FormFieldString()
        UB04_46_ServiceUnits = New UB04FormFieldString()
        UB04_47a_TotalCharges_Dollars = New UB04FormFieldString()
        UB04_47b_TotalCharges_Cents = New UB04FormFieldString()
        UB04_48a_Non_coveredCharges_Dollars = New UB04FormFieldString()
        UB04_48b_Non_coveredCharges_Cents = New UB04FormFieldString()
        UB04_49_ReservedFL49 = New UB04FormFieldString()
    End Sub
#End Region
#Region "Private Variable"
    Public UB04_42_RevenueCode As UB04FormFieldString
    Public UB04_43_RevenueCodeDescription As UB04FormFieldString
    Public UB04_44_RateCodes As UB04FormFieldString
    Public UB04_45_ServiceDate_visit As UB04FormFieldString
    Public UB04_46_ServiceUnits As UB04FormFieldString
    Public UB04_47a_TotalCharges_Dollars As UB04FormFieldString
    Public UB04_47b_TotalCharges_Cents As UB04FormFieldString
    Public UB04_48a_Non_coveredCharges_Dollars As UB04FormFieldString
    Public UB04_48b_Non_coveredCharges_Cents As UB04FormFieldString
    Public UB04_49_ReservedFL49 As UB04FormFieldString
#End Region

End Class
Public Class ServiceLines
    Public innerlist As ArrayList

#Region "constructor"

    Public Sub New()

        innerlist = New ArrayList()

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


    Default Public ReadOnly Property Item(ByVal index As Integer) As ServiceLine
        Get
            Return DirectCast(innerlist(index), ServiceLine)
        End Get
    End Property





    '' Methods Add, Remove, Count , Item
    Public Function Count() As Int16

        Return innerlist.Count

    End Function

    Public Sub Add(ByVal item As ServiceLine)

        innerlist.Add(item)
    End Sub

    Public Function Remove(ByVal item As ServiceLine) As Boolean
        ''Remark - Work Remining for comparision
        Dim result As Boolean = False
        Return result

    End Function
    Public Function RemoveAt(ByVal index As Int16) As Boolean

        Dim result As Boolean = False
        innerlist.RemoveAt(index)
        result = True
        Return result

    End Function
    Public Sub Clear()
        innerlist.Clear()
    End Sub
End Class
