Imports gloPatient
Imports gloCCDSchema





Public Class gloCCDReader
    Implements IDisposable

#Region " IDisposable  "

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#End Region

    Public Function ExtractCCD_DemographicsOnly(ByVal strCCDFilePath As String) As ReconcileList
        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Try
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema)

                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author)) Then
                        If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor) Then
                            If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization) Then
                                If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name) Then
                                    If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name(0).Text(0))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If Not IsNothing(oCCDSchema.effectiveTime) Then
                    If Not IsNothing(oCCDSchema.effectiveTime.value) Then
                        oReconcileList.FileHeaderDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.effectiveTime.value))
                    End If
                End If

                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(oCCDSchema.author.Length > 0) Then
                        If Not IsNothing(oCCDSchema.author(0).time) Then
                            If Not IsNothing(oCCDSchema.author(0).time.value) Then
                                oReconcileList.LastModifiedDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.author(0).time.value))
                            End If
                        End If
                    End If
                End If


            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            oCCDSchema = Nothing
        End Try

        Return oReconcileList

    End Function

    Public Function ExtractCCD(ByVal strCCDFilePath As String) As ReconcileList
        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        Try
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema)
                ''Added on 20151202-To read non xml content from CCDA file
                Dim strNonXMLPath As String = ""
                Dim mediatype As String = ""
                Dim oCDAReader As gloCDAReader = New gloCDAReader()

                strNonXMLPath = oCDAReader.IsExistsCCDANonXMLBody(strCCDFilePath, mediatype)
                oCDAReader.Dispose()
                oCDAReader = Nothing
                If strNonXMLPath <> "" Then

                    Return oReconcileList
                End If
                oReconcileList.mPatient.PatientHistory = getPatientHistory(oCCDSchema)
                oReconcileList.mPatient.PatientProblems = getPatientProblems(oCCDSchema)
                oReconcileList.mPatient.PatientMedications = getPatientMedications(oCCDSchema)


                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author)) Then
                        If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor) Then
                            If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization) Then
                                If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name) Then
                                    If Not IsNothing(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(CType(oCCDSchema.author(0), POCD_MT000040UV02Author).assignedAuthor.representedOrganization.name(0).Text(0))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If Not IsNothing(oCCDSchema.effectiveTime) Then
                    If Not IsNothing(oCCDSchema.effectiveTime.value) Then
                        oReconcileList.FileHeaderDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.effectiveTime.value))
                    End If
                End If

                If Not IsNothing(oCCDSchema.author) Then
                    If Not IsNothing(oCCDSchema.author.Length > 0) Then
                        If Not IsNothing(oCCDSchema.author(0).time) Then
                            If Not IsNothing(oCCDSchema.author(0).time.value) Then
                                oReconcileList.LastModifiedDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.author(0).time.value))
                            End If
                        End If
                    End If
                End If


            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            oCCDSchema = Nothing
        End Try

        Return oReconcileList

    End Function


    Private Function getPatientDemographics(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As gloPatient.Patient
        Dim PatDemographics As gloPatient.Patient = New gloPatient.Patient
        Try
            If Not IsNothing(oCCDSchema.recordTarget) Then
                If oCCDSchema.recordTarget.Length > 0 Then
                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole) Then

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.id) Then
                            ' PatDemographics.DemographicsDetail.PatientCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id)
                            If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id.Length) > 0 Then
                                PatDemographics.DemographicsDetail.PatientExternalCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id(0).extension)
                            End If


                        End If

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient) Then
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name) Then
                                If oCCDSchema.recordTarget(0).patientRole.patient.name.Length > 0 Then
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items) Then
                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items.Length > 0) Then
                                            Dim k As Integer = 0
                                            For k = 0 To oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items.Length - 1
                                                If (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).ToString().Contains("given") = True)) Then
                                                    If PatDemographics.DemographicsDetail.PatientFirstName = "" Then
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                                            If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                                                PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                                            End If
                                                        End If
                                                    Else
                                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                                            If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                                                PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                                            End If
                                                        End If
                                                    End If
                                                ElseIf (Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).ToString().Contains("family") = True)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text) Then
                                                        If oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text.Length > 0 Then
                                                            PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.name(0).Items(k).Text(0))
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            End If
                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.birthTime) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value).Length >= 8 Then
                                        PatDemographics.DemographicsDetail.PatientDOB = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value).Substring(0, 8))
                                    End If
                                End If
                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "f" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "female" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Female"
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "m" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "male" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Male"
                                    Else
                                        ''PatDemographics.DemographicsDetail.PatientGender = "Other"
                                    End If
                                End If
                            End If


                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then
                                If oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0 Then
                                    PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).displayName)
                                End If

                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then
                                If oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0 Then
                                    PatDemographics.DemographicsDetail.PatientEthnicities = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).displayName)
                                End If

                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr.Length > 0) Then
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items) Then
                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length > 0) Then
                                            Dim i As Integer = 0
                                            '   If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text) Then
                                            For i = 0 To oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length - 1
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i)) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text) Then
                                                        If oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text.Length > 0 Then

                                                            If (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("city") = True) Then
                                                                PatDemographics.DemographicsDetail.PatientCity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                            ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("state") = True) Then
                                                                PatDemographics.DemographicsDetail.PatientState = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                            ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("country") = True) Then
                                                                PatDemographics.DemographicsDetail.PatientCountry = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                            ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("postal") = True) Then
                                                                PatDemographics.DemographicsDetail.PatientZip = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                            ElseIf (oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).ToString().ToLower().Contains("streetaddressline") = True) Then
                                                                If PatDemographics.DemographicsDetail.PatientAddress1 = "" Then
                                                                    PatDemographics.DemographicsDetail.PatientAddress1 = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                                Else
                                                                    PatDemographics.DemographicsDetail.PatientAddress2 = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.addr(0).Items(i).Text(0))
                                                                End If

                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next
                                            'End If
                                        End If
                                    End If
                                End If
                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom.Length > 0) Then
                                    PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(0).value)
                                End If
                            End If


                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode) Then
                                'UnMarried/Married/Single/Widowed/Divorced
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode.code) Then
                                    Select Case Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.maritalStatusCode.code)
                                        Case "M"
                                            PatDemographics.DemographicsDetail.PatientMaritalStatus = "Married"
                                        Case "S"
                                            PatDemographics.DemographicsDetail.PatientMaritalStatus = "Single"
                                        Case "W"
                                            PatDemographics.DemographicsDetail.PatientMaritalStatus = "Widowed"
                                        Case "D"
                                            PatDemographics.DemographicsDetail.PatientMaritalStatus = "Divorced"
                                    End Select
                                End If
                            End If

                            PatDemographics.DemographicsDetail.PatientProviderID = 0
                            PatDemographics.DemographicsDetail.PatientLanguage = ""
                            PatDemographics.DemographicsDetail.PatientSSN = ""
                            PatDemographics.DemographicsDetail.PatientAddress2 = ""
                            PatDemographics.DemographicsDetail.PatientCounty = ""
                            PatDemographics.DemographicsDetail.PatientMobile = ""
                            PatDemographics.DemographicsDetail.PatientOccupation = ""
                            PatDemographics.DemographicsDetail.PatientFax = ""
                            PatDemographics.DemographicsDetail.EmergencyContact = ""
                            PatDemographics.DemographicsDetail.EmergencyPhone = ""
                            PatDemographics.GuardianDetail.PatientMotherFirstName = ""
                            PatDemographics.GuardianDetail.PatientMotherMiddleName = ""
                            PatDemographics.GuardianDetail.PatientMotherLastName = ""
                            PatDemographics.OccupationDetail.PatientEmploymentStatus = ""
                            PatDemographics.OccupationDetail.PatientPlaceofEmployment = ""
                            PatDemographics.OccupationDetail.PatientWorkAddress1 = ""
                            PatDemographics.OccupationDetail.PatientWorkAddress2 = ""
                            PatDemographics.OccupationDetail.PatientWorkCity = ""
                            PatDemographics.OccupationDetail.PatientWorkState = ""
                            PatDemographics.OccupationDetail.PatientWorkZip = ""
                            PatDemographics.OccupationDetail.PatientWorkPhone = ""
                            PatDemographics.OccupationDetail.PatientWorkFax = ""
                            PatDemographics.PatientDemographicOtherInfo.RegistrationDate = Nothing
                            PatDemographics.PatientDemographicOtherInfo.Status = ""

                        End If


                    End If

                End If

            End If


            Return PatDemographics
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            PatDemographics = Nothing
            Return Nothing
        End Try
    End Function


    Private Function getPatientHistory(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As gloPatientHistoryCol

        Dim oPatientHistoryList As gloPatientHistoryCol = New gloPatientHistoryCol
        Dim oCDADataBaseLayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
        Dim i As Integer = 0
        Dim TemplateID As String = ""
        Dim _sStatusTemplateID As String = ""
        Dim _sReactionTemplateID As String = ""
        Try

            Dim oPatientHistory As gloPatientHistory

            If Not IsNothing(oCCDSchema.component.Item) Then

                If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then


                    TemplateID = oCDADataBaseLayer.getCCDTemplateID("Allergies")
                    Dim SectionFound As Boolean = False
                    For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
                        If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
                            If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then
                                Dim TemlateCount As Integer = 0
                                SectionFound = False
                                For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
                                    If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
                                        SectionFound = True
                                        Exit For
                                    End If
                                Next
                                If SectionFound = False Then
                                    Continue For
                                Else
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If SectionFound = True Then


                        If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
                            If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then

                                Dim oHistorySchema As POCD_MT000040UV02Entry = Nothing
                                Dim oHistoryObservation As POCD_MT000040UV02Observation = Nothing
                                Dim oHistoryPlayingEntity As POCD_MT000040UV02PlayingEntity = Nothing

                                Dim EntryCount As Integer = 0
                                For EntryCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length - 1


                                    oHistorySchema = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry(EntryCount)
                                    If Not IsNothing(oHistorySchema) Then

                                        If Not IsNothing(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship) Then

                                            If CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship.Length > 0 Then

                                                If Not IsNothing(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship(0).Item) Then

                                                    oHistoryObservation = TryCast(CType(oHistorySchema.Item, POCD_MT000040UV02Act).entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                                    If Not IsNothing(oHistoryObservation) Then
                                                        If Not IsNothing(oHistoryObservation.participant) Then
                                                            If oHistoryObservation.participant.Length > 0 Then
                                                                If Not IsNothing(oHistoryObservation.participant(0).participantRole) Then
                                                                    If Not IsNothing(oHistoryObservation.participant(0).participantRole.Item) Then
                                                                        oHistoryPlayingEntity = TryCast(oHistoryObservation.participant(0).participantRole.Item, POCD_MT000040UV02PlayingEntity)
                                                                    End If

                                                                End If

                                                            End If

                                                        End If

                                                    End If
                                                End If
                                            End If
                                        End If

                                    End If

                                    oPatientHistory = New gloPatientHistory()

                                    oPatientHistory.HistoryCategory = "Allergies"
                                    oPatientHistory.Comments = ""
                                    If Not IsNothing(oHistoryPlayingEntity) Then
                                        If Not IsNothing(oHistoryPlayingEntity.code) Then

                                            ''History Item/Drug Name
                                            oPatientHistory.HistoryItem = Convert.ToString(oHistoryPlayingEntity.code.displayName)
                                            oPatientHistory.DrugName = Convert.ToString(oHistoryPlayingEntity.code.displayName)

                                            ''RxNormCode

                                            If Not IsNothing(oHistoryPlayingEntity.code.codeSystem) Then
                                                If oHistoryPlayingEntity.code.codeSystem = CodeSystem.RxNorm Then
                                                    oPatientHistory.RxNormCode = Convert.ToString(oHistoryPlayingEntity.code.code)
                                                End If
                                            End If



                                        End If
                                    End If

                                    ''ConceptDI/ SNOMED CT Code
                                    'If Not IsNothing(oHistoryObservation) Then
                                    '    If Not IsNothing(oHistoryObservation.value) Then
                                    '        If Not IsNothing(CType(oHistoryObservation.value(0),CD)) Then
                                    '            If Not IsNothing(CType(oHistoryObservation.value(0),CD).codeSystem) Then
                                    '                If Convert.ToString(CType(oHistoryObservation.value(0),CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                    '                    oPatientHistory.ConceptId = Convert.ToString(CType(oHistoryObservation.value(0),CD).code)
                                    '                End If
                                    '            End If
                                    '        End If
                                    '    End If
                                    'End If


                                    ''DOEAllergy
                                    Try
                                        oPatientHistory.DOEAllergy = gloDateMaster.gloDate.DateAsStringDate(Convert.ToString(CType(CType(oHistorySchema.Item, POCD_MT000040UV02Act).effectiveTime.Items(0), IVXB_TS).value))
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                        oPatientHistory.DOEAllergy = DateTime.Now
                                    End Try


                                    _sStatusTemplateID = oCDADataBaseLayer.getCCDTemplateID("Allergies Status")
                                    _sReactionTemplateID = oCDADataBaseLayer.getCCDTemplateID("Allergies Reaction")

                                    If Not IsNothing(oHistoryObservation) Then
                                        If Not IsNothing(oHistoryObservation.entryRelationship) Then
                                            If oHistoryObservation.entryRelationship.Length > 0 Then

                                                Dim oObservation_ReactionStatus As POCD_MT000040UV02Observation = Nothing

                                                Dim indexAllergyEntry As Int32
                                                For indexAllergyEntry = 0 To oHistoryObservation.entryRelationship.Length - 1

                                                    If Not IsNothing(oHistoryObservation.entryRelationship(indexAllergyEntry).Item) Then

                                                        oObservation_ReactionStatus = TryCast(oHistoryObservation.entryRelationship(indexAllergyEntry).Item, POCD_MT000040UV02Observation)
                                                        If Not IsNothing(oObservation_ReactionStatus) Then
                                                            If Not IsNothing(oObservation_ReactionStatus.templateId) Then

                                                                If oObservation_ReactionStatus.templateId.Length > 0 Then
                                                                    If Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sStatusTemplateID Then
                                                                        ''Allergy Status Section
                                                                        oPatientHistory.Status = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CE).displayName)
                                                                    ElseIf Convert.ToString(oObservation_ReactionStatus.templateId(0).root) = _sReactionTemplateID Then
                                                                        ''Allergy Reaction Section
                                                                        oPatientHistory.Reaction = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).displayName)
                                                                        oPatientHistory.ReactionCode = Convert.ToString(CType(oObservation_ReactionStatus.value(0), CD).code)
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                    oObservation_ReactionStatus = Nothing
                                                Next

                                            End If
                                        End If
                                    End If


                                    ''Add History Item to collection
                                    If (Convert.ToString(oPatientHistory.HistoryItem) <> "") Then
                                        oPatientHistoryList.Add(oPatientHistory)
                                    End If
                                    If Not IsNothing(oPatientHistory) Then
                                        oPatientHistory.Dispose()
                                        oPatientHistory = Nothing
                                    End If

                                    oHistorySchema = Nothing
                                    oHistoryObservation = Nothing
                                    oHistoryPlayingEntity = Nothing
                                Next

                            End If
                        End If
                    End If
                End If
            End If



        Catch ex As Exception
            oPatientHistoryList = Nothing
            Throw ex
        Finally
            If Not IsNothing(oCDADataBaseLayer) Then
                oCDADataBaseLayer.Dispose()
            End If
            TemplateID = Nothing
            _sStatusTemplateID = Nothing
            _sReactionTemplateID = Nothing
        End Try

        Return oPatientHistoryList

    End Function


    Private Function getPatientProblems(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As ProblemsCol



        Dim oProblemList As ProblemsCol = New ProblemsCol()
        Dim oCCDDbLayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()

        Try
            If Not IsNothing(oCCDSchema) Then
                If Not IsNothing(oCCDSchema.component) Then
                    If Not IsNothing(oCCDSchema.component.Item) Then
                        If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then

                            Dim TemplateID As String = ""
                            TemplateID = oCCDDbLayer.getCCDTemplateID("ProblemList")
                            Dim SectionFound As Boolean = False
                            Dim i As Integer = 0
                            For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
                                If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
                                    If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then

                                        Dim TemlateCount As Integer = 0
                                        SectionFound = False
                                        For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
                                            If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
                                                SectionFound = True
                                                Exit For
                                            End If
                                        Next
                                        If SectionFound = False Then
                                            Continue For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If SectionFound = True Then


                                ''---Read Propblem Entry---------------------------------
                                If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
                                    If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then

                                        Dim oProblem As Problems = Nothing
                                        Dim oCDAProblemSchema As POCD_MT000040UV02Entry = Nothing
                                        Dim oCDAProblemEntry As POCD_MT000040UV02Act = Nothing
                                        Dim oCDAProblemObservation As POCD_MT000040UV02Observation = Nothing
                                        Dim EntryCount As Integer = 0

                                        For EntryCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length - 1

                                            oCDAProblemSchema = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry(EntryCount)

                                            If Not IsNothing(oCDAProblemSchema) Then
                                                oCDAProblemEntry = TryCast(oCDAProblemSchema.Item, POCD_MT000040UV02Act)
                                                If Not IsNothing(oCDAProblemEntry) Then
                                                    If Not IsNothing(oCDAProblemEntry.entryRelationship) Then
                                                        If oCDAProblemEntry.entryRelationship.Length > 0 Then
                                                            If Not IsNothing(oCDAProblemEntry.entryRelationship(0).Item) Then
                                                                oCDAProblemObservation = TryCast(oCDAProblemEntry.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                                            End If

                                                        End If
                                                    End If
                                                End If
                                            End If


                                            oProblem = New Problems()

                                            If Not IsNothing(oCDAProblemObservation) Then
                                                If Not IsNothing(oCDAProblemObservation.value) Then
                                                    If oCDAProblemObservation.value.Length > 0 Then


                                                        If Not IsNothing(oCDAProblemObservation.value(0)) Then

                                                            ''oProblem.Condition
                                                            oProblem.Condition = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)

                                                            ''oProblem.DateOfService
                                                            Try
                                                                oProblem.DateOfService = gloDateMaster.gloDate.DateAsStringDate((Convert.ToString(CType(oCDAProblemObservation.effectiveTime.Items(0), IVXB_TS).value)))
                                                            Catch ex As Exception
                                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                                oProblem.DateOfService = DateTime.Now
                                                            End Try

                                                            ''Value element SNOMED or ICD9
                                                            If Not IsNothing(CType(oCDAProblemObservation.value(0), CD).codeSystem) Then

                                                                If CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                                    oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                ElseIf CType(oCDAProblemObservation.value(0), CD).codeSystem = CodeSystem.ICD9 Then
                                                                    oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).code)
                                                                    oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                                End If

                                                            End If

                                                            ''Transaction element SNOMED or ICD9
                                                            If Not IsNothing(CType(oCDAProblemObservation.value(0), CD).translation) Then
                                                                If CType(oCDAProblemObservation.value(0), CD).translation.Length > 0 Then


                                                                    If (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).codeSystem) = CodeSystem.ICD9) Then

                                                                        oProblem.ICD9Code = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).code)
                                                                        oProblem.ICD9 = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).displayName)


                                                                    ElseIf (Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                                        oProblem.ConceptID = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).translation(0).code)
                                                                    End If
                                                                End If
                                                            End If

                                                            oProblem.Immediacy = 3

                                                            ''Status
                                                            Dim strStatus As String = ""
                                                            If Not IsNothing(oCDAProblemEntry.statusCode) Then
                                                                strStatus = Convert.ToString(oCDAProblemEntry.statusCode.code)
                                                            End If

                                                            Select Case strStatus.ToUpper()
                                                                Case "RESOLVED"
                                                                    oProblem.ProblemStatus = Problems.Status.Resolved
                                                                Case "ACTIVE"
                                                                    oProblem.ProblemStatus = Problems.Status.Active
                                                                Case "INACTIVE"
                                                                    oProblem.ProblemStatus = Problems.Status.Inactive
                                                                Case "CHRONIC"
                                                                    oProblem.Immediacy = 2 'hardcode
                                                                    oProblem.ProblemStatus = Problems.Status.Active
                                                                Case "ALL"
                                                                    oProblem.ProblemStatus = Problems.Status.All
                                                                Case Else
                                                                    oProblem.ProblemStatus = Problems.Status.Active
                                                            End Select


                                                        End If
                                                    End If

                                                End If
                                            End If

                                            If (Convert.ToString(oProblem.Condition) <> "") Then
                                                oProblemList.Add(oProblem)
                                            End If

                                            If Not IsNothing(oProblem) Then
                                                oProblem.Dispose()
                                                oProblem = Nothing
                                            End If
                                            oCDAProblemSchema = Nothing
                                            oCDAProblemEntry = Nothing
                                            oCDAProblemObservation = Nothing

                                        Next ''For each Problem 
                                    End If
                                End If
                                ''---Read Propblem Entry-----------------------------------------
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            oProblemList = Nothing
            Throw ex
        Finally

            If Not IsNothing(oCCDDbLayer) Then
                oCCDDbLayer.Dispose()
            End If

        End Try

        Return oProblemList


    End Function


    Private Function getPatientMedications(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As MedicationsCol
        Dim oMedicationList As MedicationsCol = New MedicationsCol()
        Dim oCCDDataBaseLayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
        Dim TemplateID As String = ""
        Try

            If Not IsNothing(oCCDSchema) Then
                Dim oMedication As Medication = Nothing


                If Not IsNothing(oCCDSchema.component.Item) Then

                    If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then

                        Dim i As Integer = 0

                        TemplateID = oCCDDataBaseLayer.getCCDTemplateID("Medication")
                        Dim SectionFound As Boolean = False
                        For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
                            If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
                                If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then

                                    Dim TemlateCount As Integer = 0
                                    SectionFound = False
                                    For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
                                        If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
                                            SectionFound = True
                                            Exit For
                                        End If
                                    Next
                                    If SectionFound = False Then
                                        Continue For
                                    Else
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If SectionFound = True Then


                            If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
                                If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then

                                    Dim oCDAMedicationSchema As POCD_MT000040UV02Entry = Nothing
                                    Dim oCDAMedicationAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing
                                    Dim oCDAMedicationMaterial As POCD_MT000040UV02Material = Nothing
                                    Dim oCDAMedicationRelationShip As POCD_MT000040UV02EntryRelationship = Nothing

                                    Dim EntryCount As Integer = 0
                                    For EntryCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length - 1

                                        oCDAMedicationSchema = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry(EntryCount)
                                        If Not IsNothing(oCDAMedicationSchema) Then
                                            oCDAMedicationAdmin = TryCast(oCDAMedicationSchema.Item, POCD_MT000040UV02SubstanceAdministration)
                                            If Not IsNothing(oCDAMedicationAdmin) Then
                                                If Not IsNothing(oCDAMedicationAdmin.consumable) Then
                                                    If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct) Then
                                                        If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct.Item) Then
                                                            oCDAMedicationMaterial = TryCast(oCDAMedicationAdmin.consumable.manufacturedProduct.Item, POCD_MT000040UV02Material)
                                                        End If

                                                    End If
                                                End If

                                            End If
                                        End If

                                        oMedication = New Medication

                                        ''Set as Todays date. This is Update date in gloEMR
                                        oMedication.MedicationDate = DateTime.Now.ToString("MM/dd/yyyy")

                                        If Not IsNothing(oCDAMedicationAdmin) Then
                                            If Not IsNothing(oCDAMedicationAdmin.effectiveTime) Then


                                                ''Get StartTime and Frequncy CDA objects
                                                Dim oCDAStartTime As IVL_TS = Nothing
                                                Dim oCDAFrequency As PIVL_TS = Nothing
                                                Dim indexEffective As Int32
                                                For indexEffective = 0 To oCDAMedicationAdmin.effectiveTime.Length - 1

                                                    ''IVL_TS = EffectiveDate code for Start and End DAte
                                                    If IsNothing(oCDAStartTime) Then
                                                        oCDAStartTime = TryCast(oCDAMedicationAdmin.effectiveTime(indexEffective), IVL_TS)
                                                    End If

                                                    ''PIVL_TS = Frequency and unit information
                                                    If IsNothing(oCDAFrequency) Then
                                                        oCDAFrequency = TryCast(oCDAMedicationAdmin.effectiveTime(indexEffective), PIVL_TS)
                                                    End If

                                                Next

                                                ''Read Low and Hight elements for Start and End time
                                                If Not IsNothing(oCDAStartTime) Then
                                                    Dim indexStartTime As Int32
                                                    For indexStartTime = 0 To oCDAStartTime.Items.Length - 1
                                                        Try
                                                            If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.low Then
                                                                If CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value <> "" Then
                                                                    oMedication.StartDate = gloDateMaster.gloDate.DateAsStringDate(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
                                                                End If

                                                            End If

                                                            If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.high Then
                                                                If CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value <> "" Then
                                                                    oMedication.EndDate = gloDateMaster.gloDate.DateAsStringDate(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
                                                                End If

                                                            End If
                                                        Catch ex As Exception
                                                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                            ex = Nothing
                                                        End Try
                                                    Next
                                                End If
                                                oCDAStartTime = Nothing

                                                ''Frequency
                                                If Not IsNothing(oCDAFrequency) Then
                                                    If Not IsNothing(oCDAFrequency.period) Then
                                                        Dim _sUnit As String = ""
                                                        If Not IsNothing(oCDAFrequency.period.unit) AndAlso Convert.ToString(oCDAFrequency.period.unit) <> "1" Then
                                                            Select Case Convert.ToString(oCDAFrequency.period.unit)
                                                                Case "h"
                                                                    _sUnit = " Hours"
                                                                Case "d"
                                                                    _sUnit = " Day"
                                                            End Select
                                                        End If
                                                        oMedication.Frequency = Convert.ToString(oCDAFrequency.period.value) + _sUnit
                                                    End If
                                                End If

                                                oCDAFrequency = Nothing

                                            End If ''oCDAMedicationAdmin.effectiveTime 

                                            ''Dose Quantity

                                            If Not IsNothing(oCDAMedicationAdmin.doseQuantity) Then
                                                If Not IsNothing(oCDAMedicationAdmin.doseQuantity.unit) AndAlso Convert.ToString(oCDAMedicationAdmin.doseQuantity.unit) <> "1" Then
                                                    oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.doseQuantity.value) + " " + Convert.ToString(oCDAMedicationAdmin.doseQuantity.unit)
                                                Else
                                                    oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.doseQuantity.value)
                                                End If
                                            ElseIf Not IsNothing(oCDAMedicationAdmin.rateQuantity) Then
                                                If Not IsNothing(oCDAMedicationAdmin.rateQuantity.unit) AndAlso Convert.ToString(oCDAMedicationAdmin.rateQuantity.unit) <> "1" Then
                                                    oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.rateQuantity.value) + " " + Convert.ToString(oCDAMedicationAdmin.rateQuantity.unit)
                                                Else
                                                    oMedication.DrugQuantity = Convert.ToString(oCDAMedicationAdmin.rateQuantity.value)
                                                End If
                                            End If

                                            ''Route
                                            If Not IsNothing(oCDAMedicationAdmin.routeCode) Then
                                                Dim sDescription As String = gloReconciliation.GetRouteCodeSystem(oCDAMedicationAdmin.routeCode.codeSystem, oCDAMedicationAdmin.routeCode.code)
                                                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                                    If ogloGSHelper.IsRouteExist(sDescription) Then
                                                        oMedication.Route = sDescription
                                                    End If
                                                End Using
                                            End If

                                            ''DrugForm //i.e. Tablet etc
                                            If Not IsNothing(oCDAMedicationAdmin.administrationUnitCode) Then
                                                oMedication.DrugForm = Convert.ToString(oCDAMedicationAdmin.administrationUnitCode.displayName)
                                            End If

                                            ''Status
                                            If Not IsNothing(oCDAMedicationAdmin.statusCode) Then
                                                oMedication.Status = Convert.ToString(oCDAMedicationAdmin.statusCode.code)
                                            End If

                                            ''Prescriber Note
                                            If Not IsNothing(oCDAMedicationAdmin.entryRelationship) Then
                                                Dim oCDAMedicationAct As POCD_MT000040UV02Act = Nothing
                                                If oCDAMedicationAdmin.entryRelationship.Length > 0 Then
                                                    oCDAMedicationAct = TryCast(oCDAMedicationAdmin.entryRelationship(0).Item, POCD_MT000040UV02Act)
                                                    If Not IsNothing(oCDAMedicationAct) Then
                                                        If Not IsNothing(oCDAMedicationAct.text) Then
                                                            If Not IsNothing(oCDAMedicationAct.text.Text) Then
                                                                oMedication.Rx_PrescriberNotes = oCDAMedicationAct.text.Text(0).Trim()
                                                            End If

                                                        End If

                                                    End If
                                                    oCDAMedicationAct = Nothing
                                                End If

                                            End If


                                        End If  ''IF oCDAMedicationAdmin

                                        If Not IsNothing(oCDAMedicationMaterial) Then
                                            If Not IsNothing(oCDAMedicationMaterial.code) Then

                                                ''RxNormCode
                                                If Convert.ToString(oCDAMedicationMaterial.code.codeSystem) = CodeSystem.RxNorm Then
                                                    oMedication.RxNormCode = Convert.ToString(oCDAMedicationMaterial.code.code)
                                                End If

                                                If Convert.ToString(oCDAMedicationMaterial.code.codeSystem) = CodeSystem.NDC Then
                                                    oMedication.ProdCode = Convert.ToString(oCDAMedicationMaterial.code.code)
                                                End If
                                                ''Drug Name
                                                oMedication.DrugName = Convert.ToString(oCDAMedicationMaterial.code.displayName)
                                                oMedication.GenericName = Convert.ToString(oCDAMedicationMaterial.code.displayName)


                                            End If
                                        End If


                                        oMedicationList.Add(oMedication)
                                        If Not IsNothing(oMedication) Then
                                            oMedication.Dispose()
                                            oMedication = Nothing
                                        End If

                                    Next

                                    oCDAMedicationMaterial = Nothing
                                    oCDAMedicationAdmin = Nothing
                                    oCDAMedicationSchema = Nothing
                                    oCDAMedicationRelationShip = Nothing

                                End If
                            End If
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            oMedicationList = Nothing
            Throw ex
        Finally
            If Not IsNothing(oCCDDataBaseLayer) Then
                oCCDDataBaseLayer.Dispose()
            End If
            TemplateID = Nothing
        End Try
        Return oMedicationList


    End Function





End Class
