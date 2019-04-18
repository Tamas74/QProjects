Imports gloPatient
Imports gloCCDSchema
Imports System.Data.SqlClient
Imports gloDateMaster
Imports System.Windows.Forms


Public Class gloQRDAReader
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
    Dim oTemplateIDMaster As TemplateIDMaster
    Dim bCompareQRDA As Boolean = False
    Dim exportprovider As Boolean = False
    Dim oCodingSystemMaster As CodeSystemMaster

    Public Property CompareQRDA As Boolean

        Get
            Return bCompareQRDA
        End Get
        Set(ByVal value As Boolean)
            bCompareQRDA = value
        End Set
    End Property
    Public Property ExportproviderList As Boolean
        Get
            Return exportprovider
        End Get
        Set(ByVal value As Boolean)
            exportprovider = value
        End Set
    End Property




    Public Function ExtractCDA_DemographicsOnly(ByVal strCCDFilePath As String, ByVal nproviderid As Int64) As ReconcileList
        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing

        'Dim oassignAuthor As POCD_MT000040UV02Author = Nothing
        oTemplateIDMaster = New TemplateIDMaster(1)
        Dim _assignPerson As POCD_MT000040UV02Person = Nothing
        Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
        Try

            oProvider = New gloCCDLibrary.PatientProvider
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema, nproviderid)
                'oReconcileList.mPatient.PatientDemographics.Insurances = getPatientInsurances(oCCDSchema)




                If Not IsNothing(oCCDSchema.custodian) Then
                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian) Then
                        If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization) Then
                            If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name) Then
                                If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text) Then
                                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text(0))
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

                        _assignPerson = oCCDSchema.author(0).assignedAuthor.Item
                        ''
                        If Not IsNothing(_assignPerson) Then
                            If Not IsNothing(_assignPerson.name(0).Items) Then
                                If Not IsNothing(_assignPerson.name(0).Items.Length > 0) Then
                                    Dim k As Integer = 0
                                    For k = 0 To _assignPerson.name(0).Items.Length - 1
                                        If (Convert.ToString(_assignPerson.name(0).Items(k).ToString().Contains("given") = True)) Then
                                            If oProvider.FirstName = "" Then
                                                If Not IsNothing(_assignPerson.name(0).Items(k).Text) Then
                                                    oProvider.FirstName = Convert.ToString(_assignPerson.name(0).Items(k).Text(0))
                                                End If

                                                'oReconcileList .mPatient .PatientProviders(0
                                                'Else
                                                ' PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(b.name(0).Items(k).Text(0))
                                            End If
                                        ElseIf (Convert.ToString(_assignPerson.name(0).Items(k).ToString().Contains("family") = True)) Then
                                            If Not IsNothing(_assignPerson.name(0).Items(k).Text) Then
                                                oProvider.LastName = Convert.ToString(_assignPerson.name(0).Items(k).Text(0))
                                            End If

                                        End If

                                    Next
                                    If oProvider.FirstName <> "" Or oProvider.LastName <> "" Then
                                        oReconcileList.mPatient.PatientProviders.Add(oProvider)
                                    End If

                                End If
                            End If
                        End If


                        If Not IsNothing(oCCDSchema.author(0).time) Then
                            If Not IsNothing(oCCDSchema.author(0).time.value) Then
                                If oCCDSchema.author(0).time.value <> "" Then
                                    oReconcileList.LastModifiedDateTime = Convert.ToDateTime(gloReconciliation.DateFromHL7(oCCDSchema.author(0).time.value))
                                End If

                            End If
                        End If
                    End If
                End If

            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            oCCDSchema = Nothing
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
            End If
            If Not IsNothing(oProvider) Then
                oProvider.Dispose()
                oProvider = Nothing
            End If
            _assignPerson = Nothing
        End Try

        Return oReconcileList

    End Function

    Public Function ExtractCDA(ByVal strCCDFilePath As String, ByVal nproviderid As Int64, Optional ByVal blnRegProvider As Boolean = False) As ReconcileList
        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
        oCodingSystemMaster = New CodeSystemMaster()
        gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = True
        oTemplateIDMaster = New TemplateIDMaster(1)
        Try
            oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)

            If Not IsNothing(oCCDSchema) Then

                oReconcileList.mPatient = New Patient()
                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCDSchema, nproviderid)
                Try
                    oReconcileList.RootguidID = oCCDSchema.id.root
                Catch ex As Exception

                End Try

                Dim oTeamMember As PatientSupport = Nothing
                If (blnRegProvider = True) Then
                    getproviderinfo(oCCDSchema, oTeamMember)
                End If
                If exportprovider = True Then
                    If blnRegProvider = False Then
                        getproviderinfo(oCCDSchema, oTeamMember)
                    End If

                    Dim Measurename As String = GetMeasureName(oCCDSchema)
                    If Measurename <> "" Then
                        oReconcileList.CMSID = Measurename
                    End If
                    If Not IsNothing(oTeamMember) Then
                        Dim osupportcol As New PatientSupportCol
                        osupportcol.Add(oTeamMember)
                        oReconcileList.mPatient.PatientCareTeam = osupportcol
                    End If

                    Return oReconcileList
                End If
                Try
                    oReconcileList.PatProvNPI = oTeamMember.PersonName.ProvNPI
                Catch ex As Exception

                End Try
                'oReconcileList.mPatient.PatientCareTeam
                getSection(oCCDSchema, oReconcileList)

                If Not IsNothing(oTeamMember) Then
                    Dim osupportcol As New PatientSupportCol
                    osupportcol.Add(oTeamMember)
                    oReconcileList.mPatient.PatientCareTeam = osupportcol
                End If

                If Not IsNothing(oCCDSchema.custodian) Then
                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian) Then
                        If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization) Then
                            If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name) Then
                                If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text) Then
                                    If Not IsNothing(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text.Length > 1) Then
                                        oReconcileList.FileHeaderSource = Convert.ToString(oCCDSchema.custodian.assignedCustodian.representedCustodianOrganization.name.Text(0))
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
            ex = Nothing
        Finally
            oCCDSchema = Nothing
            If Not IsNothing(oTemplateIDMaster) Then
                oTemplateIDMaster.Dispose()
            End If
        End Try

        Return oReconcileList

    End Function



    Private Sub getproviderinfo(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument, ByRef oTeamMember As PatientSupport)
        If Not IsNothing(oCCDSchema.documentationOf) Then
            oTeamMember = New PatientSupport
            If oCCDSchema.documentationOf.Length > 0 Then
                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent) Then
                    If oCCDSchema.documentationOf(0).serviceEvent.classCode = ActClassRoot.PCPR Then
                        If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer) Then
                            If oCCDSchema.documentationOf(0).serviceEvent.performer.Length > 0 Then
                                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity) Then
                                    If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.id) Then
                                        If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.id.Length > 0 Then
                                            If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.id(0).root) Then
                                                If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.id(0).root = "2.16.840.1.113883.4.6" Then

                                                    ' oTeamMember = oReconcileList.mPatient.PatientCareTeam.Item(0)
                                                    oTeamMember.PersonName.ProvNPI = oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.id(0).extension
                                                    '  oTeamMember.PersonName .PersonContactAddress .
                                                End If
                                                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.code) Then
                                                    If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.code.code) Then
                                                        oTeamMember.PersonName.TaxonomyCode = oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.code.code
                                                    End If
                                                End If

                                                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr) Then
                                                    If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr.Length > 0 Then
                                                        If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items) Then
                                                            If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items.Length > 0 Then
                                                                For Len As Integer = 0 To oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items.Length - 1
                                                                    Dim _item As ADXP = oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len)
                                                                    ' oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).items(len).
                                                                    If (oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).ToString().ToLower().Contains("city") = True) Then
                                                                        oTeamMember.PersonContactAddress.City = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))
                                                                    End If
                                                                    If (oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).ToString().ToLower().Contains("state") = True) Then
                                                                        oTeamMember.PersonContactAddress.State = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))
                                                                    End If
                                                                    If (oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).ToString().ToLower().Contains("country") = True) Then
                                                                        oTeamMember.PersonContactAddress.Country = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))
                                                                    End If
                                                                    If (oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).ToString().ToLower().Contains("postal") = True) Then
                                                                        oTeamMember.PersonContactAddress.Zip = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))
                                                                    End If
                                                                    If (oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).ToString().ToLower().Contains("streetaddressline") = True) Then
                                                                        If (oTeamMember.PersonContactAddress.Street = "") Then
                                                                            oTeamMember.PersonContactAddress.Street = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))
                                                                        Else
                                                                            oTeamMember.PersonContactAddress.AddressLine2 = Convert.ToString(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.addr(0).Items(Len).Text(0))

                                                                        End If
                                                                    End If
                                                                Next
                                                            Else
                                                                SetFakeProviderInfo(oTeamMember)
                                                            End If
                                                        Else
                                                            SetFakeProviderInfo(oTeamMember)
                                                        End If
                                                    Else
                                                        SetFakeProviderInfo(oTeamMember)
                                                    End If
                                                Else
                                                    SetFakeProviderInfo(oTeamMember)
                                                End If

                                                ''  DirectCast(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.
                                                If oCCDSchema.legalAuthenticator.Length > 0 Then
                                                    If Not IsNothing(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson) Then
                                                        If Not IsNothing(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name) Then
                                                            If (oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name.Length > 0) Then
                                                                If Not IsNothing(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items) Then
                                                                    oTeamMember.PersonName.FirstName = ""
                                                                    For Len As Integer = 0 To oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items.Length - 1
                                                                        If (oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).ToString().ToLower().Contains("given") = True) Then
                                                                            If (oTeamMember.PersonName.FirstName = "") Then
                                                                                oTeamMember.PersonName.FirstName = Convert.ToString(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).Text(0))
                                                                            Else
                                                                                If Not IsNothing(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).Text) Then
                                                                                    oTeamMember.PersonName.MiddleName = Convert.ToString(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).Text(0))
                                                                                End If

                                                                            End If
                                                                        End If
                                                                        If (oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).ToString().ToLower().Contains("family") = True) Then
                                                                            oTeamMember.PersonName.LastName = Convert.ToString(oCCDSchema.legalAuthenticator(0).assignedEntity.assignedPerson.name(0).Items(Len).Text(0))
                                                                        End If
                                                                    Next
                                                                End If


                                                            End If
                                                        End If
                                                    End If
                                                Else
                                                    SetFakeProviderInfo(oTeamMember)
                                                End If

                                                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization) Then

                                                    If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id) Then


                                                        If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id) Then
                                                            If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id.Length > 0 Then
                                                                If Not IsNothing(oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id(0).root) Then
                                                                    If oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id(0).root = "2.16.840.1.113883.4.2" Then

                                                                        oTeamMember.PersonName.ProvidersTaxID = oCCDSchema.documentationOf(0).serviceEvent.performer(0).assignedEntity.representedOrganization.id(0).extension

                                                                        'If (ClinicTaxID = String.Empty) Then
                                                                        '    ClinicTaxID = oReconcileList.mPatient.Clinic.ClinicTaxID
                                                                        'ElseIf ClinicTaxID <> Convert.ToString(oReconcileList.mPatient.Clinic.ClinicTaxID) Then
                                                                        '    System.Windows.Forms.MessageBox.Show("ClinicTaxID is Different " & Convert.ToString(oReconcileList.mPatient.Clinic.ClinicTaxID))

                                                                        'End If

                                                                    End If
                                                                End If
                                                            End If
                                                        End If

                                                        ''added
                                                    End If

                                                End If





                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub SetFakeProviderInfo(ByRef oTeamMember As PatientSupport)
        oTeamMember.PersonContactAddress.City = String.Empty
        oTeamMember.PersonContactAddress.State = String.Empty
        oTeamMember.PersonContactAddress.Country = String.Empty
        oTeamMember.PersonContactAddress.Zip = String.Empty
        oTeamMember.PersonContactAddress.AddressLine2 = String.Empty

    End Sub
    Private Function GetMeasureName(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As String
        Try
            Dim measurename As String = ""
            If Not IsNothing(oCCDSchema.component.Item) Then
                If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0 Then
                    Dim i As Integer = 0
                    Dim TemplateID As String = "2.16.840.1.113883.10.20.24.2.3"
                    Dim templateorganizer = "2.16.840.1.113883.10.20.24.3.97"
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
                                Dim entryarray As POCD_MT000040UV02Entry() = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry
                                Dim organizerfound As Boolean = False
                                Dim organizer As POCD_MT000040UV02Organizer = Nothing
                                For EntryCount As Integer = 0 To entryarray.Length - 1
                                    Dim entry As POCD_MT000040UV02Entry = Nothing
                                    entry = entryarray(EntryCount)
                                    If Not IsNothing(entry) Then

                                        If Convert.ToString(entry.Item) = "POCD_MT000040UV02Organizer" Then
                                            organizer = CType(entry.Item, POCD_MT000040UV02Organizer)
                                            Dim TemlateCount As Integer = 0

                                            For TemlateCount = 0 To organizer.templateId.Length - 1
                                                If templateorganizer = organizer.templateId(TemlateCount).root.ToString() Then
                                                    organizerfound = True
                                                    Exit For
                                                End If

                                            Next
                                        End If

                                    End If
                                Next
                                If organizerfound = True Then
                                    If Not IsNothing(organizer) Then
                                        If Not IsNothing(organizer.id) Then
                                            If organizer.id.Length > 0 Then
                                                Dim extension As String = organizer.id(0).extension
                                                'get measurename from database aainst this extension
                                                measurename = getMeasure(extension)
                                            End If
                                        End If

                                    End If

                                End If


                            End If
                        End If
                    End If

                End If
            End If

            Return measurename
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function
    Private Function getMeasure(ByRef versionspecific As String) As String

        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Dim measurename As String
        Try
            oDB.Connect(False)
            _sqlQuery = "select CMSID from MUCQM2014CQMdata where versionspecificid = '" & versionspecific & "'"
            measurename = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return measurename
    End Function
    Private Function getPatientDemographics(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument, ByVal nproviderid As Int64) As gloPatient.Patient
        Dim PatDemographics As gloPatient.Patient = New gloPatient.Patient

        Try
            If Not IsNothing(oCCDSchema.recordTarget) Then
                If oCCDSchema.recordTarget.Length > 0 Then
                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole) Then

                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.id) Then
                            ''PatDemographics.DemographicsDetail.PatientCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id)

                            PatDemographics.DemographicsDetail.PatientExternalCode = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.id(0).extension)
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
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value).Length >= 8 And IsNumeric(Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.birthTime.value)) Then
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
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "un" Or Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.administrativeGenderCode.code).ToLower() = "unknown" Then
                                        PatDemographics.DemographicsDetail.PatientGender = "Other"

                                    End If
                                End If
                            End If

                            Dim _CodeSysItem As CodeSystemItem = Nothing
                            Dim _oCDADataExtraction As New gloCDADataExtraction
                            Dim sRaceCode As String = String.Empty
                            Dim sRace As String = String.Empty
                            Dim sEthnicityCode As String = String.Empty
                            Dim sEthnicity As String = String.Empty

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then

                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "ASKU" Then
                                        sRaceCode = "ASKU"
                                        sRace = "Declined to specify"
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "UNK" Then
                                        sRaceCode = "UNK"
                                        sRace = "Unknown"
                                    Else
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i)) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation.Length > 0) Then
                                                        For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation.Length - 1
                                                            If sRaceCode = "" Then
                                                                sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).code) + "'"
                                                                ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).displayName)
                                                            Else
                                                                sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).code) + "'"
                                                                ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).translation(j).displayName)
                                                            End If

                                                        Next
                                                    End If
                                                Else
                                                    If sRaceCode = "" Then
                                                        sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code) + "'"
                                                        ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                                    Else
                                                        sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code) + "'"
                                                        ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).displayName)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If


                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1) Then

                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1.Length > 0) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(0).nullFlavor) = "ASKU" Then
                                        If sRaceCode = "ASKU" Or sRaceCode = "UNK" Then
                                            sRaceCode = "ASKU"
                                            sRace = "Declined to specify"
                                        End If
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(0).nullFlavor) = "UNK" Then
                                        If sRaceCode = "ASKU" Or sRaceCode = "UNK" Then
                                            sRaceCode = "UNK"
                                            sRace = "Unknown"
                                        End If
                                    Else
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode1.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i)) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation.Length > 0) Then
                                                        For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation.Length - 1
                                                            If sRaceCode = "" Then
                                                                sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).code) + "'"
                                                                ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).displayName)
                                                            Else
                                                                sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).code) + "'"
                                                                ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).translation(j).displayName)
                                                            End If

                                                        Next
                                                    End If
                                                Else
                                                    If sRaceCode = "" Then
                                                        sRaceCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).code) + "'"
                                                        ''sRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).displayName)
                                                    Else
                                                        sRaceCode = sRaceCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).code) + "'"
                                                        ''sRace = sRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode1(i).displayName)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If


                            If sRaceCode <> "ASKU" And sRaceCode <> "UNK" Then
                                Dim databaselayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                sRace = databaselayer.getRaceEthnicityDescription("race", sRaceCode)
                                If Not IsNothing(databaselayer) Then
                                    databaselayer.Dispose()
                                End If
                            End If

                            PatDemographics.DemographicsDetail.PatientRace = sRace
                            ''

                            'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then
                            '    If oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0 Then
                            '        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "ASKU" Then
                            '            PatDemographics.DemographicsDetail.PatientRace = "Declined to specify"
                            '        Else
                            '            'If PatDemographics.DemographicsDetail.PatientRace = "" Then
                            '            '    PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).displayName)
                            '            'Else
                            '            '    PatDemographics.DemographicsDetail.PatientRace = PatDemographics.DemographicsDetail.PatientRace + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).displayName)
                            '            'End If
                            '            '  PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)
                            '        End If
                            '    End If



                            '    ' PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)

                            'End If

                            'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then
                            '    If oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0 Then
                            '        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "ASKU" Then
                            '            PatDemographics.DemographicsDetail.PatientEthnicities = "Declined to specify"
                            '        Else
                            '            PatDemographics.DemographicsDetail.PatientEthnicities = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).displayName)
                            '        End If

                            '    End If

                            'End If



                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then

                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "ASKU" Then
                                        sEthnicityCode = "ASKU"
                                        sEthnicity = "Declined to specify"
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor) = "UNK" Then
                                        sEthnicityCode = "UNK"
                                        sEthnicity = "Unknown"
                                    Else
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i)) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation.Length > 0) Then
                                                        For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation.Length - 1
                                                            If sEthnicityCode = "" Then
                                                                sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).code) + "'"
                                                                ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).displayName)
                                                            Else
                                                                sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).code) + "'"
                                                                ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).translation(j).displayName)
                                                            End If

                                                        Next
                                                    End If
                                                Else
                                                    If sEthnicityCode = "" Then
                                                        sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).code) + "'"
                                                        ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).displayName)
                                                    Else
                                                        sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).code) + "'"
                                                        ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(i).displayName)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If


                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1.Length > 0) Then
                                    If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(0).nullFlavor) = "ASKU" Then
                                        If sEthnicityCode = "ASKU" Or sEthnicityCode = "UNK" Then
                                            sEthnicityCode = "ASKU"
                                            sEthnicity = "Declined to specify"
                                        End If
                                    ElseIf Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(0).nullFlavor) = "UNK" Then
                                        If sEthnicityCode = "ASKU" Or sEthnicityCode = "UNK" Then
                                            sEthnicityCode = "UNK"
                                            sEthnicity = "Unknown"
                                        End If
                                    Else
                                        For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1.Length - 1
                                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i)) Then
                                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation) Then
                                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation.Length > 0) Then
                                                        For j As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation.Length - 1
                                                            If sEthnicityCode = "" Then
                                                                sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).code) + "'"
                                                                ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).displayName)
                                                            Else
                                                                sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).code) + "'"
                                                                ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).translation(j).displayName)
                                                            End If

                                                        Next
                                                    End If
                                                Else
                                                    If sEthnicityCode = "" Then
                                                        sEthnicityCode = "'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).code) + "'"
                                                        ''sEthnicity = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).displayName)
                                                    Else
                                                        sEthnicityCode = sEthnicityCode + ",'" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).code) + "'"
                                                        ''sEthnicity = sEthnicity + "|" + Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode1(i).displayName)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                            '' End If

                            If sEthnicityCode <> "ASKU" And sEthnicityCode <> "UNK" Then
                                Dim databaselayer1 As gloCCDDatabaseLayer = New gloCCDDatabaseLayer()
                                sEthnicity = databaselayer1.getRaceEthnicityDescription("ethnicity", sEthnicityCode)
                                If Not IsNothing(databaselayer1) Then
                                    databaselayer1.Dispose()
                                End If
                            End If

                            PatDemographics.DemographicsDetail.PatientEthnicities = sEthnicity


                            ''  End If

                            'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient) Then
                            '    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then

                            '        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0) Then
                            '            For i As Integer = 0 To oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length - 1
                            '                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i)) Then
                            '                    If PatDemographics.DemographicsDetail.PatientRace = "" Then
                            '                        _CodeSysItem = oCodingSystemMaster.GetbyCode(CodeSystem.RaceAndEthnicity, Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code))
                            '                        PatDemographics.DemographicsDetail.PatientRace = _CodeSysItem.Description
                            '                    Else
                            '                        _CodeSysItem = oCodingSystemMaster.GetbyCode(CodeSystem.RaceAndEthnicity, Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(i).code))
                            '                        PatDemographics.DemographicsDetail.PatientRace = PatDemographics.DemographicsDetail.PatientRace + "|" + _CodeSysItem.Description
                            '                    End If

                            '                End If
                            '            Next

                            '        End If
                            '    End If
                            'End If

                            ' ''

                            'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.raceCode) Then
                            '    If oCCDSchema.recordTarget(0).patientRole.patient.raceCode.Length > 0 Then
                            '        If Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).nullFlavor) = "ASKU" Then
                            '            PatDemographics.DemographicsDetail.PatientRace = "Declined to specify"
                            '        Else
                            '            'Dim code As String = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode(0).code)
                            '            'Dim dt As DataTable = GetPatientRace(code)
                            '            'If dt.Rows.Count > 0 Then
                            '            '    If PatDemographics.DemographicsDetail.PatientRace = "" Then

                            '            '        PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(dt.Rows(0)("sDescription"))
                            '            '    Else
                            '            '        PatDemographics.DemographicsDetail.PatientRace = PatDemographics.DemographicsDetail.PatientRace + "|" + Convert.ToString(dt.Rows(0)("sDescription"))
                            '            '    End If
                            '            'End If
                            '            'If Not IsNothing(dt) Then
                            '            '    dt.Dispose()
                            '            '    dt = Nothing
                            '            'End If


                            '            '  PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)
                            '        End If
                            '    End If

                            '    ' PatDemographics.DemographicsDetail.PatientRace = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.raceCode.displayName)

                            'End If

                            'If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode) Then
                            '    If oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode.Length > 0 Then
                            '        If oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).nullFlavor = "ASKU" Then
                            '            PatDemographics.DemographicsDetail.PatientEthnicities = "Declined to specify"
                            '        Else
                            '            Dim code As String = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.ethnicGroupCode(0).code)
                            '            Dim dt As DataTable = GetPatientEthnicity(code)
                            '            If dt.Rows.Count > 0 Then
                            '                PatDemographics.DemographicsDetail.PatientEthnicities = Convert.ToString(dt.Rows(0)("sDescription"))
                            '            End If
                            '            If Not IsNothing(dt) Then
                            '                dt.Dispose()
                            '                dt = Nothing
                            '            End If
                            '        End If
                            '    End If


                            'End If


                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr.Length > 0) Then
                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items) Then
                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length > 0) Then
                                            Dim i As Integer = 0

                                            For i = 0 To oCCDSchema.recordTarget(0).patientRole.addr(0).Items.Length - 1
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
                                            Next

                                        End If
                                    End If
                                End If
                            End If

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom) Then
                                If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.telecom.Length > 0) Then
                                    ' PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(0).value)
                                    PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCDSchema.recordTarget(0).patientRole.telecom(0).value).Replace(") ", "").Replace("(", "").Replace("-", "").Replace("tel:+1", "")
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

                            If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication) Then
                                If oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication.Length > 0 Then


                                    If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode) Then
                                        If Not IsNothing(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.code) Then


                                            Select Case Convert.ToString(oCCDSchema.recordTarget(0).patientRole.patient.languageCommunication(0).languageCode.code)
                                                Case "eng", "en"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "English"

                                                Case "spa"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Spanish"
                                                Case "zho"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Chinese"

                                                Case "fra"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "French"
                                                Case "deu"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "German"
                                                Case "el"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Greek"
                                                Case "guj"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Gujarati"
                                                Case "jpn"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Japanese"
                                                Case "mar"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Marathi"
                                                Case "ASKU"
                                                    PatDemographics.DemographicsDetail.PatientLanguage = "Declined to specify"



                                            End Select
                                        End If
                                    End If
                                End If

                            End If


                            ' ''''IsInsuranceModified is set to true so that record can be saved'''''''''
                            'PatDemographics.IsInsuranceModified = True
                            PatDemographics.DemographicsDetail.PatientProviderID = nproviderid
                            'PatDemographics.DemographicsDetail.PatientLanguage = ""
                            PatDemographics.DemographicsDetail.PatientSSN = ""
                            ' PatDemographics.DemographicsDetail.PatientAddress2 = ""
                            PatDemographics.DemographicsDetail.PatientCounty = ""
                            PatDemographics.DemographicsDetail.PatientMobile = ""
                            PatDemographics.DemographicsDetail.PatientOccupation = ""
                            PatDemographics.DemographicsDetail.PatientFax = ""
                            PatDemographics.DemographicsDetail.EmergencyContact = ""
                            PatDemographics.DemographicsDetail.EmergencyPhone = ""
                            PatDemographics.GuardianDetail.PatientMotherFirstName = ""
                            PatDemographics.GuardianDetail.PatientMotherMiddleName = ""
                            PatDemographics.GuardianDetail.PatientMotherLastName = ""
                            PatDemographics.GuardianDetail.PatientMotherMaidenFirstName = ""
                            PatDemographics.GuardianDetail.PatientMotherMaidenMiddleName = ""
                            PatDemographics.GuardianDetail.PatientMotherMaidenLastName = ""
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            PatDemographics = Nothing
            Return Nothing
        End Try
    End Function
    Private Function getPatientInsurances(ByVal oinsuranceentry As POCD_MT000040UV02Entry) As gloPatient.Insurances

        Dim oInsurancecol As gloPatient.Insurances = New gloPatient.Insurances

        ''Dim PatInsurancecol As InsuranceCol = New InsuranceCol
        Dim oinsuranceobservation As POCD_MT000040UV02Observation


        Dim Insurancecode As String = ""
        Try


            'Dim TemplateIDEntry As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("Insurance")


            If Not IsNothing(oinsuranceentry) Then

                If Convert.ToString(oinsuranceentry.Item) = "POCD_MT000040UV02Observation" Then
                    oinsuranceobservation = CType(oinsuranceentry.Item, POCD_MT000040UV02Observation)

                    Dim TemlateCount As Integer = 0
                    'For TemlateCount = 0 To oinsuranceobservation.templateId.Length - 1
                    '    If TemplateIDEntry = oinsuranceobservation.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True
                    '    End If
                    'Next
                    'If entryfound = True Then
                    Dim ob As ANY()
                    ob = CType(oinsuranceobservation.value, ANY())
                    ob.GetValue(0)
                    Insurancecode = DirectCast(ob.GetValue(0), CD).code.ToString()
                    Dim oInsurance As gloPatient.Insurance = New gloPatient.Insurance
                    If Insurancecode = "2" Then
                        oInsurance.InsTypeDescriptionDefault = "Medicaid"
                        oInsurance.InsTypeCodeDefault = "MC"
                    ElseIf Insurancecode = "349" Then
                        oInsurance.InsTypeDescriptionDefault = "Other"
                        oInsurance.InsTypeCodeDefault = "OT"
                    ElseIf Insurancecode = "1" Then
                        oInsurance.InsTypeDescriptionDefault = "Medicare"
                        oInsurance.InsTypeCodeDefault = "MA"
                    End If
                    oInsurance.IsSameAsPatient = True
                    Dim dt As DataTable = GetInsuranceName()
                    If dt.Rows.Count > 0 Then

                        oInsurance.InsuranceName = dt.Rows(0)("sName")
                        oInsurance.ContactID = dt.Rows(0)("ContactID")
                        oInsurance.AddrressLine1 = dt.Rows(0)("AddressLine1")
                        oInsurance.AddrressLine2 = dt.Rows(0)("AddressLine2")
                        oInsurance.City = dt.Rows(0)("City")
                        oInsurance.State = dt.Rows(0)("State")
                        oInsurance.ZIP = dt.Rows(0)("ZIP")
                        oInsurance.InsuranceTypeCode = dt.Rows(0)("InsuranceTypeCode")
                        oInsurance.InsuranceTypeDesc = dt.Rows(0)("InsuranceTypeDesc")
                        oInsurance.SubscriberID = "12345"
                        oInsurance.InsuranceFlag = 1
                        oInsurance.RelationshipID = 8
                        oInsurance.RelationshipName = "Self"
                        oInsurance.IsAddressSameAsPatient = 1
                        oInsurance.PayerID = "45632"
                        oInsurance.InsuranceID = 0
                        oInsurance.IsNotEndDate = True
                        oInsurance.IsNotStartDate = True
                        oInsurance.IsNotDOB = False
                        oInsurance.StartDate = DateTime.Now
                        oInsurance.EndDate = DateTime.Now

                    End If

                    oInsurancecol.Add(oInsurance)
                    oInsurance.Dispose()
                    If Not IsNothing(dt) Then
                        dt.Dispose()
                    End If
                End If

            End If


            'If TemplateIDEntry = Convert.ToString(oinsuranceobservation.templateId) Then

            'End If




            Return oInsurancecol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            oInsurancecol = Nothing
            Return Nothing
        Finally
            Insurancecode = Nothing
            oinsuranceobservation = Nothing
        End Try
    End Function
    Private Function GetInsuranceName() As DataTable
        Dim dtinsurance As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "SELECT DISTINCT top(1) Contacts_MST.nContactID AS ContactID, ISNULL(Contacts_MST.sFirstName, '') + SPACE(1) +   ISNULL(Contacts_MST.sMiddleName, '') + SPACE(1) + ISNULL(Contacts_MST.sLastName, '') AS PhysicianName, ISNULL(Contacts_MST.sLastName, '') AS LastName,   ISNULL(Contacts_MST.sName, '') AS sName,ISNULL(Contacts_InsuranceCompany_MST.sDescription, '') AS Company,  ISNULL(Contacts_InsuranceReportingCategory_MST.sDescription, '') AS ReportingCategory,   ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc, '') AS InsuranceTypeDesc,  ISNULL(Contacts_MST.sGender, '') AS Gender, ISNULL(Contacts_MST.sAddressLine1,'') AS AddressLine1,   ISNULL(Contacts_MST.sAddressLine2, '') AS AddressLine2, ISNULL(Contacts_MST.sCity, '') AS City, ISNULL(Contacts_MST.sState, '') AS State,   ISNULL(Contacts_MST.sZIP, '') AS ZIP, ISNULL(Contacts_MST.sContact, '') AS ContactName,  dbo.FormatPhone(ISNULL(Contacts_MST.sPhone, ''),0) AS Phone,   dbo.FormatFax(ISNULL(Contacts_MST.sFax, '')) AS FAX, dbo.FormatPhone(ISNULL(Contacts_MST.sMobile, ''),0) AS Mobile, ISNULL(Contacts_MST.sEmail, '') AS Email,   ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode FROM Contact_InsurancePlanReportingCat_Association   INNER JOIN Contacts_InsuranceReportingCategory_MST ON Contact_InsurancePlanReportingCat_Association.nReportingCategoryId = Contacts_InsuranceReportingCategory_MST.nID   RIGHT OUTER JOIN Contacts_MST ON Contact_InsurancePlanReportingCat_Association.nContactId = Contacts_MST.nContactID   LEFT OUTER JOIN Contacts_InsuranceCompany_MST INNER JOIN Contact_InsurancePlan_Association ON   Contacts_InsuranceCompany_MST.nID = Contact_InsurancePlan_Association.nCompanyId   ON Contacts_MST.nContactID = Contact_InsurancePlan_Association.nContactId LEFT OUTER JOIN   Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID   WHERE (ISNULL(Contacts_MST.bIsBlocked, 0) = 0) AND (Contacts_MST.sContactType = 'Insurance')   AND (ISNULL(Contacts_MST.nClinicID, 1) = 1) ORDER BY sName "


            oDB.Retrive_Query(_sqlQuery, dtinsurance)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtinsurance
    End Function
    Private Function getCPTDescription(ByVal cptcode As String) As DataTable
        Dim dt As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select sdescription  from CPT_MST where scptcode='" & cptcode & "'"


            oDB.Retrive_Query(_sqlQuery, dt)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dt
    End Function
    Private Function GetPatientRace(ByVal code As String) As DataTable
        Dim dtrace As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select  sDescription, sCode from category_mst where sCategoryType IN ('Race','Race Specification') and sCode= '" & code & "' order by sDescription"


            oDB.Retrive_Query(_sqlQuery, dtrace)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtrace
    End Function
    Private Function GetLabTestName() As DataTable
        Dim dtrace As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select  top (1) labtm_Code,labtm_Name from Lab_Test_Mst"


            oDB.Retrive_Query(_sqlQuery, dtrace)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtrace
    End Function
    Private Function GetPatientEthnicity(ByVal code As String) As DataTable
        Dim dtethnicity As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select  sDescription, sCode from category_mst where sCategoryType = 'Ethnicity' and sCode= '" & code & "' order by sDescription"


            oDB.Retrive_Query(_sqlQuery, dtethnicity)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtethnicity
    End Function
    Private Function GetICDdescription(ByVal code As String, ByVal ICDRevision As Int16)
        Dim dtICD As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select isnull(sDescription,'') as sDescription from icd9 where nicdrevision=" & ICDRevision & "and sicd9code='" & code & "' union select isnull(sDescriptionShort,'') as sDescription from ICD9Gallery where nicdrevision=" & ICDRevision & "and sicd9code='" & code & "'"


            oDB.Retrive_Query(_sqlQuery, dtICD)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtICD
    End Function
    Private Function GetReasonDescription(ByVal code As String) As String
        '  Dim dtICD As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Dim desc As String = ""
        Try
            oDB.Connect(False)
            _sqlQuery = "SELECT DISTINCT  [Description]  FROM   MUCQMcodes  WHERE Code ='" & code & "'"

            desc = oDB.ExecuteScalar_Query(_sqlQuery)
            ' oDB.Retrive_Query(_sqlQuery, dtICD)
        Catch ex As Exception
            Return ""
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return desc
    End Function
    Private Function GetReasonDescriptionfromValueset(ByVal code As String) As String
        '  Dim dtICD As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Dim desc As String = ""
        Try
            oDB.Connect(False)
            _sqlQuery = "SELECT DISTINCT  [valuesetname]  FROM   MUCQMcodes  WHERE valuesetOID ='" & code & "'"

            desc = oDB.ExecuteScalar_Query(_sqlQuery)
            ' oDB.Retrive_Query(_sqlQuery, dtICD)
        Catch ex As Exception
            Return ""
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return desc
    End Function
    Private Function GetSnomed(ByVal code As String)
        Dim dtsnomed As New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Try
            oDB.Connect(False)
            Dim dbpara As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters
            dbpara.Add("@tempstring", code, ParameterDirection.Input, SqlDbType.NVarChar)
            oDB.Retrive("History_SearchsnomedString", dbpara, dtsnomed)
            dbpara.Dispose()
            dbpara = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
        Return dtsnomed
    End Function

    Private Function getPatientHistory(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument) As gloPatientHistoryCol

        Dim oPatientHistoryList As gloPatientHistoryCol = New gloPatientHistoryCol
        Dim TemplateID As String = ""
        Dim _sStatusTemplateID As String = ""
        Dim _sReactionTemplateID As String = ""
        Try

            Dim oPatientHistory As gloPatientHistory

            If Not IsNothing(oCCDSchema.component.Item) Then

                If (CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0) Then

                    Dim i As Integer = 0

                    ''TemplateID = oCDADataBaseLayer.getCDATemplateID("Allergies")
                    TemplateID = oTemplateIDMaster.GetSectionID("Allergies")
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
                                        ex = Nothing
                                        oPatientHistory.DOEAllergy = DateTime.Now
                                    End Try


                                    _sStatusTemplateID = oTemplateIDMaster.GetEntryID("Allergy Status Observation")
                                    _sReactionTemplateID = oTemplateIDMaster.GetEntryID("Reaction Observation")



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
            TemplateID = Nothing
            _sStatusTemplateID = Nothing
            _sReactionTemplateID = Nothing
        End Try

        Return oPatientHistoryList

    End Function

    Private Function getPatientProblems(ByVal Diagnosisentry As POCD_MT000040UV02Entry, ByRef oProblemList As ProblemsCol) As ProblemsCol

        'Dim oProblemList As ProblemsCol = New ProblemsCol()

        'Dim entryfound As Boolean = False
        'Dim conceptid As String = ""
        Try

            'Dim TemplateIDEntry As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("Diagnosis")
            'If Not IsNothing(entryarray) Then

            '    Dim EntryCount As Integer = 0
            '    For EntryCount = 0 To entryarray.Length - 1
            '        Dim Diagnosisentry As POCD_MT000040UV02Entry = Nothing
            '        Diagnosisentry = entryarray(EntryCount)
            If Not IsNothing(Diagnosisentry) Then
                Dim Diagnosisobservation As POCD_MT000040UV02Observation = Nothing
                If Convert.ToString(Diagnosisentry.Item) = "POCD_MT000040UV02Observation" Then
                    Diagnosisobservation = CType(Diagnosisentry.Item, POCD_MT000040UV02Observation)
                    Dim TemlateCount As Integer = 0

                    'For TemlateCount = 0 To Diagnosisobservation.templateId.Length - 1
                    '    If TemplateIDEntry = Diagnosisobservation.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True

                    '        'Dim valuecount As Integer = 0
                    '        'For valuecount = 0 To oinsuranceobservation.value.Length-1


                    '        'Next



                    '    End If
                    'Next
                    'If entryfound = True Then
                    Dim oProblem As Problems = Nothing
                    oProblem = New Problems()
                    Dim ob As ANY()
                    ob = CType(Diagnosisobservation.value, ANY())

                    If Not IsNothing(ob) Then
                        If ob.Length > 0 Then
                            'ob.GetValue(0)

                            'conceptid = DirectCast(ob.GetValue(0),CD).code.ToString()
                            'oProblem.ConceptID = conceptid
                            If Not IsNothing(Diagnosisobservation.effectiveTime) Then
                                If Diagnosisobservation.effectiveTime.Items.Length > 0 Then
                                    If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                        oProblem.DateOfService = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).value)))
                                    End If


                                End If

                                If Diagnosisobservation.effectiveTime.Items.Length > 1 Then
                                    If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                        oProblem.ResolvedDate = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).value)))
                                    End If

                                End If

                            End If
                            ''Value element SNOMED or ICD9
                            'Dim dtsnomed As DataTable = New DataTable()
                            Dim dticd As DataTable = New DataTable()
                            If Not IsNothing(CType(ob.GetValue(0), CD).codeSystem) Then

                                'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then
                                If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                    oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                    'dtsnomed = GetSnomed(oProblem.ConceptID)
                                ElseIf CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD10 Then
                                    oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                    oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).displayName)
                                    oProblem.ICDRevision = 10
                                    dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                End If
                                'Else
                                'If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                '    oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD9 Then
                                    oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                    oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).displayName)
                                    oProblem.ICDRevision = 9
                                    dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                End If
                                'End If


                            End If

                            ' ''Transaction element SNOMED or ICD9
                            If Not IsNothing(CType(ob.GetValue(0), CD).translation) Then
                                If CType(ob.GetValue(0), CD).translation.Length > 0 Then
                                    For index As Integer = 0 To CType(ob.GetValue(0), CD).translation.Length - 1
                                        'If oProblem.Condition = "" Then
                                        '    oProblem.Condition = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                        'End If
                                        'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then

                                        If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                            oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                            'dtsnomed = GetSnomed(oProblem.ConceptID)
                                        ElseIf (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                            oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                            oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                            oProblem.ICDRevision = 10
                                            dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                        End If
                                        'Else
                                        If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                            oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                            oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                            oProblem.ICDRevision = 9
                                            dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                            'ElseIf (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                            '    oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)

                                        End If
                                        'End If

                                    Next
                                End If
                            End If
                            Dim ed As ED
                            ed = CType(ob.GetValue(0), CD).originalText
                            Dim text As String = Convert.ToString(ed.Text(0))
                            Dim arr As String()
                            If text.Contains(":") Then
                                arr = text.Split(":")
                                If arr.Length > 0 Then
                                    text = arr(1)
                                End If


                            End If
                            arr = Nothing

                            If oProblem.ConceptID <> "" Then
                                oProblem.Condition = text
                            End If
                            'If oProblem.ICD9 = "" Then
                            '    oProblem.ICD9 = Text
                            'End If
                            'If dtsnomed.Rows.Count > 0 Then
                            '    oProblem.Condition = dtsnomed.Rows(0)("TermDescription")
                            'End If
                            If dticd.Rows.Count > 0 Then
                                oProblem.ICD9 = dticd.Rows(0)("sDescription")
                            End If
                            If oProblem.Condition = "" Then
                                oProblem.Condition = dticd.Rows(0)("sDescription")
                            End If
                            If Not IsNothing(dticd) Then
                                dticd.Dispose()
                            End If

                        End If


                    End If
                    Dim strStatus As String = ""

                    Dim oCDAProblemObservation As POCD_MT000040UV02Observation = Nothing
                    'Dim oCDAProblemEntryRelationship As POCD_MT000040UV02EntryRelationship = Nothing
                    If Not IsNothing(Diagnosisobservation.entryRelationship) Then

                        If Diagnosisobservation.entryRelationship.Length > 0 Then
                            If Not IsNothing(Diagnosisobservation.entryRelationship(0).Item) Then
                                oCDAProblemObservation = TryCast(Diagnosisobservation.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                                Dim _sStatusTemplateID As String = ""
                                Dim statusinactive As String = ""
                                Dim statusResolved As String = ""
                                ''_sStatusTemplateID = oCDADataBaseLayer.getCDATemplateID("Problems Status")
                                _sStatusTemplateID = oTemplateIDMaster.GetQRDAEntryID("DiagnosisActive")
                                statusinactive = oTemplateIDMaster.GetQRDAEntryID("DiagnosisInActive")
                                statusResolved = oTemplateIDMaster.GetQRDAEntryID("DiagnosisResolved")
                                If Not IsNothing(oCDAProblemObservation) Then
                                    If Not IsNothing(oCDAProblemObservation.templateId) Then
                                        If oCDAProblemObservation.templateId.Length > 0 Then
                                            For templatecount As Integer = 0 To oCDAProblemObservation.templateId.Length - 1
                                                If Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = _sStatusTemplateID Then
                                                    ''Problems Status Section
                                                    strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                ElseIf Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = statusinactive Then
                                                    strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                ElseIf Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = statusResolved Then
                                                    strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0), CD).displayName)
                                                End If
                                            Next


                                        End If
                                    End If
                                End If
                            End If

                        End If

                    End If
                    If strStatus <> "" Then
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
                    oProblemList.Add(oProblem)
                    oProblem.Dispose()

                End If

            End If


            'End If

            '    Next
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            oProblemList = Nothing
            Return Nothing
        Finally

        End Try

        Return oProblemList

    End Function

    Private Function getPatientMedications(ByVal oCDAMedicationSchema As POCD_MT000040UV02Entry, ByRef oMedicationList As MedicationsCol, ByVal isPrescription As Boolean) As MedicationsCol

        'Dim oMedicationList As MedicationsCol = New MedicationsCol()
        Dim oMedication As Medication = Nothing
        Try
            'If Not IsNothing(entryarray) Then
            '    If entryarray.Length > 0 Then

            'Dim oCDAMedicationSchema As POCD_MT000040UV02Entry = Nothing
            Dim oCDAMedicationAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing
            Dim oCDAMedicationMaterial As POCD_MT000040UV02Material = Nothing
            'Dim oCDAMedicationRelationShip As POCD_MT000040UV02EntryRelationship = Nothing

            Dim EntryCount As Integer = 0

            'For EntryCount = 0 To entryarray.Length - 1
            '    Dim entryfound As Boolean = False
            'oCDAMedicationSchema = entryarray(EntryCount)
            If Not IsNothing(oCDAMedicationSchema) Then
                oCDAMedicationAdmin = TryCast(oCDAMedicationSchema.Item, POCD_MT000040UV02SubstanceAdministration)
                If Not IsNothing(oCDAMedicationAdmin) Then
                    'For templatecount As Integer = 0 To oCDAMedicationAdmin.templateId.Length - 1
                    '    If activeentryfound = True AndAlso entryorderfound = True Then
                    '        If medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                    '            entryfound = True

                    '        End If
                    '    Else
                    '        If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                    '            entryfound = True

                    '        ElseIf medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                    '            entryfound = True

                    '        End If
                    '    End If
                    'Next
                    'If entryfound = True Then

                    oMedication = New Medication()
                    ''Set as Todays date. This is Update date in gloEMR
                    oMedication.MedicationDate = DateTime.Now.ToString()
                    oMedication.IsPrescription = isPrescription
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
                                            oMedication.StartDate = gloDateMaster.gloDate.QRDADateAsDateTime(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
                                        End If

                                    End If

                                    If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.high Then
                                        If CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value <> "" Then
                                            oMedication.EndDate = gloDateMaster.gloDate.QRDADateAsDateTime(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
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
                                If Not IsNothing(oCDAFrequency.period.unit) Then
                                    Dim _sUnit As String = ""
                                    Dim objDataExt As New gloCDADataExtraction()
                                    Dim _frequency As String = objDataExt.GetFrequencyDescription(oCDAFrequency.period.value, oCDAFrequency.period.unit)
                                    If _frequency = "" Then
                                        If Convert.ToString(oCDAFrequency.period.unit) <> "1" Then
                                            Select Case Convert.ToString(oCDAFrequency.period.unit)
                                                Case "h"
                                                    _sUnit = " Hours"
                                                Case "d"
                                                    _sUnit = " Day"
                                            End Select
                                        End If
                                        oMedication.Frequency = Convert.ToString(oCDAFrequency.period.value) + _sUnit
                                    Else
                                        oMedication.Frequency = _frequency
                                    End If
                                    If Not IsNothing(objDataExt) Then
                                        objDataExt.Dispose()
                                        objDataExt = Nothing
                                    End If
                                    _frequency = Nothing
                                    _sUnit = Nothing
                                End If
                            End If
                        End If
                        oCDAFrequency = Nothing

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
                    End If
                    If Not IsNothing(oCDAMedicationAdmin.consumable) Then
                        If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct) Then
                            If Not IsNothing(oCDAMedicationAdmin.consumable.manufacturedProduct.Item) Then
                                'TestMayuri
                                oCDAMedicationMaterial = TryCast(oCDAMedicationAdmin.consumable.manufacturedProduct.Item, POCD_MT000040UV02Material)
                            End If

                        End If
                    End If

                    If Not IsNothing(oCDAMedicationMaterial) Then
                        If Not IsNothing(oCDAMedicationMaterial.code) Then

                            If Convert.ToString(oCDAMedicationMaterial.code.nullFlavor) <> "NA" AndAlso Convert.ToString(oCDAMedicationMaterial.code.code) <> "" Then
                                If Convert.ToString(oCDAMedicationMaterial.code.codeSystem) = CodeSystem.RxNorm Then
                                    oMedication.RxNormCode = Convert.ToString(oCDAMedicationMaterial.code.code)
                                    oMedication.ValuesetOID = Convert.ToString(oCDAMedicationMaterial.code.valueSet)
                                    oMedication.Valueset = GetReasonDescriptionfromValueset(Convert.ToString(oCDAMedicationMaterial.code.valueSet))
                                End If
                                oMedication.DrugName = Convert.ToString(oCDAMedicationMaterial.code.displayName)
                                oMedication.GenericName = Convert.ToString(oCDAMedicationMaterial.code.displayName)
                            Else
                                oMedication.RxNormCode = ""
                                oMedication.ValuesetOID = Convert.ToString(oCDAMedicationMaterial.code.valueSet)
                                oMedication.Valueset = GetReasonDescriptionfromValueset(Convert.ToString(oCDAMedicationMaterial.code.valueSet))
                            End If
                        End If

                        If Not IsNothing(oCDAMedicationMaterial.code.translation) Then
                            If oCDAMedicationMaterial.code.translation.Length > 0 Then
                                For index As Integer = 0 To oCDAMedicationMaterial.code.translation.Length - 1
                                    If Not IsNothing(oCDAMedicationMaterial.code.translation(index)) Then
                                        If oCDAMedicationMaterial.code.translation(index).codeSystem = CodeSystem.NDC Then
                                            oMedication.ProdCode = oCDAMedicationMaterial.code.translation(index).code
                                        ElseIf oCDAMedicationMaterial.code.translation(index).codeSystem = CodeSystem.RxNorm Then
                                            oMedication.RxNormCode = oCDAMedicationMaterial.code.translation(index).code
                                            If oMedication.GenericName = "" Then
                                                oMedication.GenericName = oCDAMedicationMaterial.code.translation(index).displayName
                                            End If
                                        End If
                                    End If
                                Next

                            End If

                        End If
                    End If
                    If Not IsNothing(oCDAMedicationAdmin) Then

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
                        'If Not IsNothing(oCDAMedicationAdmin.entryRelationship) Then

                        '    Dim oCDAMedicationAct As POCD_MT000040UV02Act = Nothing
                        '    oCDAMedicationAct = TryCast(oCDAMedicationAdmin.entryRelationship(0).Item, POCD_MT000040UV02Act)
                        '    If Not IsNothing(oCDAMedicationAct) Then
                        '        ''Dim _TemplateID As String = oCDADataBaseLayer.getCDATemplateID("Medication Instructions")
                        '        Dim _TemplateID As String = oTemplateIDMaster.GetEntryID("Instructions")

                        '        If oCDAMedicationAct.templateId(0).root = _TemplateID Then
                        '            If Not IsNothing(oCDAMedicationAct.text) Then
                        '                If Not IsNothing(oCDAMedicationAct.text.Text) Then
                        '                    oMedication.Rx_PrescriberNotes = oCDAMedicationAct.text.Text(0).Trim()
                        '                End If

                        '            End If
                        '        End If
                        '        _TemplateID = ""
                        '    End If
                        '    oCDAMedicationAct = Nothing
                        'End If



                    End If  ''IF oCDAMedicationAdmin
                    If Not IsNothing(oCDAMedicationAdmin) Then
                        If Not IsNothing(oCDAMedicationAdmin.entryRelationship) Then
                            If oCDAMedicationAdmin.entryRelationship.Length > 0 Then
                                For i As Integer = 0 To oCDAMedicationAdmin.entryRelationship.Length - 1


                                    If Not IsNothing(oCDAMedicationAdmin.entryRelationship(i).Item) Then
                                        If Convert.ToString(oCDAMedicationAdmin.entryRelationship(i).Item) = "POCD_MT000040UV02Observation" Then
                                            If CType(oCDAMedicationAdmin.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                                If Not IsNothing(CType(oCDAMedicationAdmin.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                    If CType(CType(oCDAMedicationAdmin.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then


                                                        oMedication.ReasonConceptID = CType(CType(oCDAMedicationAdmin.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                        oMedication.ReasonConceptDesc = GetReasonDescription(oMedication.ReasonConceptID)

                                                    End If

                                                End If


                                            End If

                                        End If


                                    End If
                                Next

                            End If


                        End If
                    End If

                    Dim arr As String()
                    If Not IsNothing(oCDAMedicationMaterial.code.originalText) Then
                        Dim ed As ED
                        ed = CType(oCDAMedicationMaterial.code, CD).originalText
                        Dim text As String = Convert.ToString(ed.Text(0))
                        If text.Contains(":") Then
                            arr = text.Split(":")
                            text = arr(1)
                        End If
                        If oMedication.DrugName = "" Then
                            oMedication.DrugName = text
                        End If
                        If oMedication.GenericName = "" Then
                            oMedication.GenericName = text
                        End If

                    End If



                    oMedicationList.Add(oMedication)
                End If


            End If
            'End If


            If Not IsNothing(oMedication) Then
                oMedication.Dispose()
                oMedication = Nothing
            End If

            'Next

            oCDAMedicationMaterial = Nothing
            oCDAMedicationAdmin = Nothing
            oCDAMedicationSchema = Nothing
            'frmPatientRelationship = Nothing

            '    End If
            'End If




        Catch ex As Exception
            oMedicationList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            Return Nothing
        Finally

        End Try
        Return oMedicationList


    End Function
    Private Sub getPatientEncounters(ByVal encounterentry As POCD_MT000040UV02Entry, ByRef oencounters As EncountersCol, Optional ByRef patienthistorycol As gloPatientHistoryCol = Nothing)
        'Dim oencounters As EncountersCol = New EncountersCol()
        Dim encountercode As String = ""
        Dim hcpcscode As String = ""
        Dim snomedcode As String = ""
        Dim snomeddesc As String = ""
        Dim startdate As String = ""
        Dim ExamCreated As String = ""
        Dim DischargeDate As String = ""
        Dim encounter As POCD_MT000040UV02Encounter = Nothing
        Dim encounterfound As Boolean = False

        Try








            Dim act As POCD_MT000040UV02Act = Nothing
            If Not IsNothing(encounterentry) Then
                If Convert.ToString(encounterentry.Item) = "POCD_MT000040UV02Act" Then
                    act = CType(encounterentry.Item, POCD_MT000040UV02Act)
                    If Not IsNothing(act.entryRelationship) Then
                        If act.entryRelationship.Length > 0 Then
                            For i As Integer = 0 To act.entryRelationship.Length - 1
                                If Not IsNothing(act.entryRelationship.GetValue(i)) Then
                                    If Not IsNothing(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item) Then
                                        If Convert.ToString(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item) = "POCD_MT000040UV02Encounter" Then
                                            encounter = CType(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02Encounter)
                                            Dim TemlateCount As Integer = 0
                                            Dim TemplateencounterEntry As String = oTemplateIDMaster.GetQRDAEntryID("Encounter")
                                            For TemlateCount = 0 To encounter.templateId.Length - 1
                                                If TemplateencounterEntry = encounter.templateId(TemlateCount).root.ToString() Then
                                                    encounterfound = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                    'getEncounterSection(act, encounterfound)
                    'If entryfound = True Then
                    If encounterfound = True Then
                        Dim icdcode As String = ""
                        Dim dthistory As DataTable = getPatientprocedurehistory("proc")
                        Dim encounterhistory As gloPatientHistory = New gloPatientHistory()
                        Dim encounetrdticd As DataTable = New DataTable()
                        If Convert.ToString(CType(encounter.code, CD).codeSystem) = CodeSystem.CPT Then

                            encountercode = Convert.ToString(CType(encounter.code, CD).code)
                        ElseIf Convert.ToString(CType(encounter.code, CD).codeSystem) = CodeSystem.HCPCS Then
                            '    If bCompareQRDA = True Then
                            hcpcscode = Convert.ToString(CType(encounter.code, CD).code)
                            'Else
                            ' encountercode = Convert.ToString(CType(encounter.code, CD).code)
                            ' End If

                        ElseIf Convert.ToString(CType(encounter.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
                            If Not IsNothing(encounter.code) And Not IsNothing(CType(encounter.code, CD).code) Then
                                snomedcode = Convert.ToString(CType(encounter.code, CD).code)
                                encounterhistory.ConceptId = snomedcode
                            End If

                            Dim ed As ED
                            ed = CType(encounter.code, CD).originalText
                            If Not IsNothing(ed) Then
                                Dim text As String = Convert.ToString(ed.Text(0))
                                Dim arr As String()
                                If text.Contains(":") Then
                                    arr = text.Split(":")
                                    If arr.Length > 0 Then
                                        text = arr(1)
                                    End If
                                End If
                                snomeddesc = text
                                snomedcode = Convert.ToString(CType(encounter.code, CD).code)
                                encounterhistory.ConceptId = snomedcode
                                encounterhistory.SnoDescription = snomeddesc
                                text = Nothing
                                arr = Nothing
                            End If


                        ElseIf Convert.ToString(CType(encounter.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD9 Then
                            icdcode = Convert.ToString(CType(encounter.code, CD).code)
                            encounetrdticd = GetICDdescription(icdcode, "9")
                            encounterhistory.ICDRevision = 9
                        ElseIf Convert.ToString(CType(encounter.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD10 Then
                            icdcode = Convert.ToString(CType(encounter.code, CD).code)
                            encounetrdticd = GetICDdescription(icdcode, "10")
                            encounterhistory.ICDRevision = 10
                        End If
                        'To check for translation
                        'If _isonlySnomed Then
                        '    Return Nothing
                        'End If
                        If Not IsNothing(CType(encounter.code, CD).translation) Then
                            If CType(encounter.code, CD).translation.Length > 0 Then
                                For index As Integer = 0 To CType(encounter.code, CD).translation.Length - 1
                                    'If oProblem.Condition = "" Then
                                    '    oProblem.Condition = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                    'End If
                                    'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then

                                    If (Convert.ToString(CType(encounter.code, CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                        snomedcode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        encounterhistory.ConceptId = snomedcode
                                        Dim ed As ED



                                        If Not IsNothing(CType(encounter.code, CD).originalText) Then
                                            ed = CType(encounter.code, CD).originalText
                                            Dim text As String = Convert.ToString(ed.Text(0))
                                            Dim arr As String()
                                            If text.Contains(":") Then
                                                arr = text.Split(":")
                                                If arr.Length > 0 Then
                                                    text = arr(1)
                                                End If



                                            End If
                                            snomeddesc = text
                                            encounterhistory.SnoDescription = text
                                            text = Nothing
                                            arr = Nothing
                                            ed = Nothing
                                        End If


                                        'dtsnomed = GetSnomed(oProblem.ConceptID)
                                    ElseIf (Convert.ToString(CType(encounter.code, CD).translation(index).codeSystem) = CodeSystem.CPT) Then
                                        encountercode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                    ElseIf (Convert.ToString(CType(encounter.code, CD).translation(index).codeSystem) = CodeSystem.HCPCS) Then
                                        '' encountercode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        '   If bCompareQRDA = True Then
                                        hcpcscode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        'Else
                                        ' encountercode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        'End If
                                    ElseIf (Convert.ToString(CType(encounter.code, CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                        icdcode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        encounetrdticd = GetICDdescription(icdcode, "10")
                                        encounterhistory.ICDRevision = 10
                                    ElseIf (Convert.ToString(CType(encounter.code, CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                        icdcode = Convert.ToString(CType(encounter.code, CD).translation(index).code)
                                        encounetrdticd = GetICDdescription(icdcode, "9")
                                        encounterhistory.ICDRevision = 9
                                    End If
                                Next
                            End If
                        End If

                        'If encountercode <> Convert.ToString(CType(encounter.code, CD).code) Or startdate <> Convert.ToString(CType(encounter.code, CD).code) Then

                        If Not IsNothing(encounter.effectiveTime) Then
                            If encounter.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(encounter.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    Dim datetm As String = Convert.ToString(CType(encounter.effectiveTime.Items(0), IVXB_TS).value).ToString().Replace("+0000", "")
                                    ' startdate = FormatDate(datetm)
                                    startdate = gloDateMaster.gloDate.QRDADateAsDateTime(datetm)
                                    ExamCreated = gloDateMaster.gloDate.QRDADateAsDateTime(datetm)
                                    encounterhistory.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime(datetm)
                                End If
                                If Convert.ToString(CType(encounter.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                    Dim strdate As String = Convert.ToString(CType(encounter.effectiveTime.Items(1), IVXB_TS).value).Replace("+0000", "")
                                    DischargeDate = gloDateMaster.gloDate.QRDADateAsDateTime(strdate)
                                    encounterhistory.ConcernEndDate = DischargeDate
                                End If
                            End If
                        End If
                        If Not IsNothing(encounter.dischargeDispositionCode) Then
                            If Not IsNothing(encounter.dischargeDispositionCode.code) Then
                                encounterhistory.ReasonConceptId = encounter.dischargeDispositionCode.code
                                encounterhistory.ReasonDesc = GetReasonDescription(encounterhistory.ReasonConceptId)
                            End If
                        End If
                        If Not IsNothing(encounter.entryRelationship) Then
                            If encounter.entryRelationship.Length > 0 Then
                                For i As Integer = 0 To encounter.entryRelationship.Length - 1
                                    If Not IsNothing(encounter.entryRelationship(i).Item) Then
                                        If TypeOf encounter.entryRelationship(i).Item Is POCD_MT000040UV02Observation Then
                                            If CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                                If Not IsNothing(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                    If CType(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then

                                                        encounterhistory.ReasonConceptId = CType(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                        encounterhistory.ReasonDesc = GetReasonDescription(encounterhistory.ReasonConceptId)
                                                    End If
                                                End If



                                            End If
                                        End If

                                        If TypeOf encounter.entryRelationship(i).Item Is POCD_MT000040UV02Act Then
                                            If CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Act).code.code = "77301-0" Then
                                                If Not IsNothing(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Act)) Then
                                                    If CType(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Act).code, CD).codeSystem = CodeSystem.SNOMED_CT Then

                                                        If Not IsNothing(act.entryRelationship.GetValue(i)) Then
                                                            encounterhistory.ReasonConceptId = CType(CType(encounter.entryRelationship(i).Item, POCD_MT000040UV02Act).code, CD).code
                                                            ' hcpcscode = Convert.ToString(CType(encounter.code, CD).code)
                                                            encounterhistory.ReasonDesc = GetReasonDescription(encounterhistory.ReasonConceptId)
                                                        End If
                                                    End If



                                                End If
                                            End If

                                        End If

                                    End If
                                Next
                            End If


                        End If
                        If dthistory.Rows.Count > 0 Then
                            encounterhistory.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                            encounterhistory.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                            encounterhistory.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                            'comments not taken from database
                            encounterhistory.Comments = ""
                        End If
                        If Not IsNothing(encounetrdticd) Then
                            encounetrdticd.Dispose()
                        End If
                        If encountercode <> "" Then
                            Dim dt As DataTable = getCPTDescription(encountercode)
                            Dim cptdesciption As String = ""
                            If dt.Rows.Count > 0 Then
                                cptdesciption = Convert.ToString(dt.Rows(0)("sdescription"))
                            End If
                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If
                            encounterhistory.CPT = encountercode & " : " & cptdesciption
                        End If
                        If encounetrdticd.Rows.Count > 0 Then
                            Dim icddescription As String = ""
                            icddescription = encounetrdticd.Rows(0)("sDescription")
                            encounterhistory.ICD9 = icdcode & " : " & icddescription
                        End If
                        If Not IsNothing(patienthistorycol) Then
                            patienthistorycol.Add(encounterhistory)
                        End If
                        encounterhistory.Dispose()
                        Dim exam As Encounters = New Encounters
                        exam.DateOfService = startdate
                        exam.DateOfExamCreated = ExamCreated
                        exam.DischargeDate = DischargeDate
                        exam.HcpcsCode = hcpcscode

                        If encountercode <> "" AndAlso startdate <> "" Then
                            exam.EncounterCode = encountercode
                            Dim dtcpdes As DataTable = getCPTDescription(encountercode)
                            If dtcpdes.Rows.Count > 0 Then
                                exam.EncounterName = dtcpdes.Rows(0)("sdescription")
                            End If
                            If Not IsNothing(dtcpdes) Then
                                dtcpdes.Dispose()
                            End If

                        End If
                        If snomedcode <> "" AndAlso startdate <> "" Then
                            exam.SnomedCode = snomedcode
                            exam.SnomedCodeDeSc = snomeddesc
                        End If
                        oencounters.Add(exam)
                        exam.Dispose()
                        'End If
                    End If
                End If
            End If
















































            'End If
            'Next



            'Return oencounters
        Catch ex As Exception
            oencounters = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            'Return Nothing
        Finally
            encountercode = Nothing
            snomedcode = Nothing
            snomeddesc = Nothing
            startdate = Nothing
        End Try

    End Sub

    Private Function FormatDate(ByVal dateval As String)
        Dim dt As Date
        If (dateval.Trim().Length = 12) Then
            dt = DateTime.Parse(dateval, System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None)
        ElseIf (dateval.Trim().Length = 14) Then
            DateTime.TryParseExact(dateval, "yyyyMMddhhmmss", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dt)
        ElseIf (dateval.Trim().Length = 10) Then
            DateTime.TryParseExact(dateval, "yyyymmddhh", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dt)
        ElseIf (dateval.Trim().Length = 8) Then
            DateTime.TryParseExact(dateval, "yyyymmdd", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dt)
        End If
        Return dt
    End Function
    Private Function getPatientProcedurePerformed(ByVal procedureentry As POCD_MT000040UV02Entry, ByRef patienthistorycol As gloPatientHistoryCol, Optional ByVal isIntervention As Boolean = False, Optional ByVal _onlyonesnomed As Boolean = False) As gloPatientHistoryCol
        'Dim patienthistorycol As gloPatientHistoryCol = New gloPatientHistoryCol
        Dim procedure As POCD_MT000040UV02Procedure = Nothing
        Dim procedureact As POCD_MT000040UV02Act = Nothing
        Dim observation As POCD_MT000040UV02Observation = Nothing
        Dim cptcode As String = ""
        Dim cptdesciption As String = ""
        Dim icdcode As String = ""
        Dim icddescription As String = ""
        Dim icd10code As String = ""
        Dim hcpcscode As String = ""
        Try







            Dim dthistory As DataTable = getPatientprocedurehistory("proc")






            If Not IsNothing(procedureentry) Then

                If Convert.ToString(procedureentry.Item) = "POCD_MT000040UV02Act" Then
                    procedureact = CType(procedureentry.Item, POCD_MT000040UV02Act)
                    Dim history As gloPatientHistory = New gloPatientHistory()
                    Dim history1 As gloPatientHistory = Nothing
                    Dim dticd As DataTable = New DataTable()
                    'Dim HCPCScode As String = ""
                    If Not IsNothing(procedureact) Then
                        If Convert.ToString(CType(procedureact.code, CD).codeSystem) = gloCCDSchema.CodeSystem.SNOMED_CT Then
                            history.ConceptId = Convert.ToString(CType(procedureact.code, CD).code)
                            Dim ed As ED
                            ed = CType(procedureact.code, CD).originalText
                            If Not IsNothing(ed) Then
                                Dim text As String = Convert.ToString(ed.Text(0))
                                Dim arr As String()
                                If text.Contains(":") Then
                                    arr = text.Split(":")
                                    If arr.Length > 0 Then
                                        text = arr(1)
                                    End If
                                End If
                                history.SnoDescription = text
                                ed = Nothing
                                text = Nothing
                                arr = Nothing
                            End If



                        ElseIf Convert.ToString(CType(procedureact.code, CD).codeSystem) = gloCCDSchema.CodeSystem.CPT Then
                            cptcode = Convert.ToString(CType(procedureact.code, CD).code)
                        ElseIf Convert.ToString(CType(procedureact.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD9 Then
                            icdcode = Convert.ToString(CType(procedureact.code, CD).code)
                            dticd = GetICDdescription(icdcode, "9")
                            history.ICDRevision = 9
                        ElseIf Convert.ToString(CType(procedureact.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD10 Then
                            '  If bCompareQRDA = True Then
                            icd10code = Convert.ToString(CType(procedureact.code, CD).code)
                            'Else
                            ' icdcode = Convert.ToString(CType(procedureact.code, CD).code)
                            ' End If


                            dticd = GetICDdescription(icdcode, "10")
                            history.ICDRevision = 10
                        ElseIf Convert.ToString(CType(procedureact.code, CD).codeSystem) = gloCCDSchema.CodeSystem.HCPCS Then
                            ' If bCompareQRDA = True Then
                            hcpcscode = Convert.ToString(CType(procedureact.code, CD).code)
                            'Else
                            '  cptcode = Convert.ToString(CType(procedureact.code, CD).code)
                            'End If
                        ElseIf Convert.ToString(CType(procedureact.code, CD).codeSystem) = "" AndAlso Convert.ToString(CType(procedureact.code, CD).nullFlavor) = "NA" AndAlso Convert.ToString(CType(procedureact.code, CD).code) = "" Then
                            history.ValueSetOID = Convert.ToString(CType(procedureact.code, CD).valueSet)
                            history.ValueSet = GetReasonDescriptionfromValueset(Convert.ToString(CType(procedureact.code, CD).valueSet))
                        End If


                        ''''TRanslation code'''''
                        If Not IsNothing(CType(procedureact.code, CD).translation) Then
                            If CType(procedureact.code, CD).translation.Length > 0 Then
                                For index As Integer = 0 To CType(procedureact.code, CD).translation.Length - 1

                                    If (Convert.ToString(CType(procedureact.code, CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                        history.ConceptId = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                        Dim ed As ED
                                        ed = CType(procedureact.code, CD).originalText
                                        If Not IsNothing(ed) Then
                                            Dim text As String = Convert.ToString(ed.Text(0))
                                            Dim arr As String()
                                            If text.Contains(":") Then
                                                arr = text.Split(":")
                                                If arr.Length > 0 Then
                                                    text = arr(1)
                                                End If
                                            End If
                                            history.SnoDescription = text
                                            ed = Nothing
                                            text = Nothing
                                            arr = Nothing
                                        End If

                                    ElseIf (Convert.ToString(CType(procedureact.code, CD).translation(index).codeSystem) = CodeSystem.CPT) Then
                                        cptcode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)

                                    ElseIf (Convert.ToString(CType(procedureact.code, CD).translation(index).codeSystem) = CodeSystem.HCPCS) Then

                                        ' If bCompareQRDA = True Then
                                        hcpcscode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                        'Else
                                        ' cptcode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                        'End If

                                    ElseIf (Convert.ToString(CType(procedureact.code, CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                        ' If bCompareQRDA = True Then
                                        icdcode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                        'Else
                                        ' icdcode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                        'End If
                                        dticd = GetICDdescription(icdcode, "10")
                                        history.ICDRevision = 10
                                    ElseIf (Convert.ToString(CType(procedureact.code, CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                        If (icdcode.Trim() = "") Then
                                            icdcode = Convert.ToString(CType(procedureact.code, CD).translation(index).code)
                                            dticd = GetICDdescription(icdcode, "9")
                                            history.ICDRevision = 9
                                        End If
                                    End If

                                Next
                            End If
                        End If


                        If cptcode <> "" Then
                            Dim dt As DataTable = getCPTDescription(cptcode)
                            If dt.Rows.Count > 0 Then
                                cptdesciption = Convert.ToString(dt.Rows(0)("sdescription"))
                            End If
                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If

                            history.CPT = cptcode & " : " & cptdesciption
                        End If
                        If dticd.Rows.Count > 0 Then
                            icddescription = dticd.Rows(0)("sDescription")
                            ' If (history.ICDRevision = 10) AndAlso icd10code.Trim() <> "" Then
                            'history.ICD9 = icd10code & " : " & icddescription
                            '     icd10code = ""
                            '  Else
                            history.ICD9 = icdcode & " : " & icddescription
                            'End If
                            icdcode = ""
                        End If

                        If Not IsNothing(procedureact.effectiveTime) Then
                            If procedureact.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(procedureact.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    history.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(procedureact.effectiveTime.Items(0), IVXB_TS).value)))
                                End If
                            End If
                        End If
                        If IsNothing(history.OnsetDate) Then
                            Dim author As POCD_MT000040UV02Author = Nothing
                            If Not IsNothing(procedureact.author) AndAlso procedureact.author.Length > 0 Then
                                'procedureact.author(0) = New POCD_MT000040UV02Author
                                author = CType(procedureact.author(0), POCD_MT000040UV02Author)
                                If Not IsNothing(author.time) Then
                                    history.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(author.time.value)))
                                End If

                            End If

                        End If

                        If dthistory.Rows.Count > 0 Then
                            history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                            history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                            history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                            'comments not taken from database
                            history.Comments = ""
                        End If
                        If Not IsNothing(dticd) Then
                            dticd.Dispose()
                        End If
                        ''''''If any reason is given for procedure not performed''''''''''''

                        If Not IsNothing(procedureact.entryRelationship) Then
                            If procedureact.entryRelationship.Length > 0 Then
                                For i As Integer = 0 To procedureact.entryRelationship.Length - 1
                                    If Not IsNothing(procedureact.entryRelationship(i).Item) Then
                                        If CType(procedureact.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                            If Not IsNothing(CType(procedureact.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                If CType(CType(procedureact.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                    history.ReasonConceptId = CType(CType(procedureact.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                    history.ReasonDesc = GetReasonDescription(history.ReasonConceptId)
                                                End If
                                            End If
                                        End If
                                    End If
                                Next

                            End If


                        End If

                    End If
                    patienthistorycol.Add(history)





































































































































































                ElseIf Convert.ToString(procedureentry.Item) = "POCD_MT000040UV02Procedure" Then
                    procedure = CType(procedureentry.Item, POCD_MT000040UV02Procedure)
                    'Dim TemlateCount As Integer = 0
                    'Dim entryfound As Boolean = False
                    'For TemlateCount = 0 To procedure.templateId.Length - 1
                    '    If TemplateIDEntry = procedure.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True

                    '    End If
                    'Next
                    'If entryfound = True Then
                    Dim history As gloPatientHistory = New gloPatientHistory()
                    Dim history1 As gloPatientHistory = Nothing
                    Dim dticd As DataTable = New DataTable()
                    'Dim HCPCScode As String = ""
                    If Not IsNothing(procedure) Then
                        If Convert.ToString(CType(procedure.code, CD).codeSystem) = gloCCDSchema.CodeSystem.SNOMED_CT Then
                            history.ConceptId = Convert.ToString(CType(procedure.code, CD).code)
                            Dim ed As ED
                            ed = CType(procedure.code, CD).originalText
                            If Not IsNothing(ed) Then
                                Dim text As String = Convert.ToString(ed.Text(0))
                                Dim arr As String()
                                If text.Contains(":") Then
                                    arr = text.Split(":")
                                    If arr.Length > 0 Then
                                        text = arr(1)
                                    End If
                                End If
                                history.SnoDescription = text
                                ed = Nothing
                                text = Nothing
                                arr = Nothing
                            End If


                        ElseIf Convert.ToString(CType(procedure.code, CD).codeSystem) = gloCCDSchema.CodeSystem.CPT Then
                            cptcode = Convert.ToString(CType(procedure.code, CD).code)
                        ElseIf Convert.ToString(CType(procedure.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD9 Then
                            icdcode = Convert.ToString(CType(procedure.code, CD).code)
                            dticd = GetICDdescription(icdcode, "9")
                        ElseIf Convert.ToString(CType(procedure.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD10 Then
                            ' If bCompareQRDA = True Then
                            history.ICD10 = Convert.ToString(CType(procedure.code, CD).code)
                            'Else
                            'icdcode = Convert.ToString(CType(procedure.code, CD).code)
                            'End If


                            dticd = GetICDdescription(icdcode, "10")
                        ElseIf Convert.ToString(CType(procedure.code, CD).codeSystem) = gloCCDSchema.CodeSystem.HCPCS Then
                            ' If bCompareQRDA Then
                            history.HCPCS = Convert.ToString(CType(procedure.code, CD).code)
                            'Else
                            ' cptcode = Convert.ToString(CType(procedure.code, CD).code)
                            'End If

                            cptcode = Convert.ToString(CType(procedure.code, CD).code)
                        ElseIf Convert.ToString(CType(procedure.code, CD).codeSystem) = "" AndAlso Convert.ToString(CType(procedure.code, CD).nullFlavor) = "NA" AndAlso Convert.ToString(CType(procedure.code, CD).code) = "" Then
                            history.ValueSetOID = Convert.ToString(CType(procedure.code, CD).valueSet)
                            history.ValueSet = GetReasonDescriptionfromValueset(Convert.ToString(CType(procedure.code, CD).valueSet))
                        End If


                        ''''TRanslation code'''''
                        If Not IsNothing(CType(procedure.code, CD).translation) Then
                            If CType(procedure.code, CD).translation.Length > 0 Then
                                For index As Integer = 0 To CType(procedure.code, CD).translation.Length - 1

                                    If (Convert.ToString(CType(procedure.code, CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                        history.ConceptId = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        Dim ed As ED
                                        ed = CType(procedure.code, CD).originalText
                                        If Not IsNothing(ed) Then
                                            Dim text As String = Convert.ToString(ed.Text(0))
                                            Dim arr As String()
                                            If text.Contains(":") Then
                                                arr = text.Split(":")
                                                If arr.Length > 0 Then
                                                    text = arr(1)
                                                End If
                                            End If
                                            history.SnoDescription = text
                                            ed = Nothing
                                            text = Nothing
                                            arr = Nothing
                                        End If
                                    ElseIf (Convert.ToString(CType(procedure.code, CD).translation(index).codeSystem) = CodeSystem.CPT) Then
                                        cptcode = Convert.ToString(CType(procedure.code, CD).translation(index).code)

                                    ElseIf (Convert.ToString(CType(procedure.code, CD).translation(index).codeSystem) = CodeSystem.HCPCS) Then
                                        '  If bCompareQRDA Then
                                        history.HCPCS = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        'Else
                                        '  cptcode = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        '  End If


                                    ElseIf (Convert.ToString(CType(procedure.code, CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                        'If bCompareQRDA = True Then
                                        history.ICD10 = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        'Else
                                        ' icdcode = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        'End If


                                        dticd = GetICDdescription(icdcode, "10")
                                        history.ICDRevision = 10
                                    ElseIf (Convert.ToString(CType(procedure.code, CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                        icdcode = Convert.ToString(CType(procedure.code, CD).translation(index).code)
                                        dticd = GetICDdescription(icdcode, "9")
                                        history.ICDRevision = 9
                                    End If

                                Next
                            End If
                        End If


                        If cptcode <> "" Then
                            Dim dt As DataTable = getCPTDescription(cptcode)
                            If dt.Rows.Count > 0 Then
                                cptdesciption = Convert.ToString(dt.Rows(0)("sdescription"))
                            End If
                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If

                            history.CPT = cptcode & " : " & cptdesciption
                        End If
                        If dticd.Rows.Count > 0 Then
                            icddescription = dticd.Rows(0)("sDescription")
                            history.ICD9 = icdcode & " : " & icddescription
                        End If

                        If Not IsNothing(procedure.effectiveTime) Then
                            If procedure.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(procedure.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    history.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(procedure.effectiveTime.Items(0), IVXB_TS).value)))
                                End If
                            End If
                        ElseIf Not IsNothing(procedure.author) Then
                            If procedure.author.Length > 0 Then
                                If Not IsNothing(procedure.author(0).templateId) Then
                                    If Not IsNothing(procedure.author(0).templateId(0).root) Then
                                        If procedure.author(0).templateId(0).root = "2.16.840.1.113883.10.20.22.4.119" Then
                                            If Not IsNothing(procedure.author(0).time) Then
                                                If Not IsNothing(procedure.author(0).time.value) Then
                                                    history.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(procedure.author(0).time.value)))
                                                End If

                                            End If

                                        End If
                                    End If


                                End If
                            End If
                        End If
                        If dthistory.Rows.Count > 0 Then
                            history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                            history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                            history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                            'comments not taken from database
                            history.Comments = ""
                        End If
                        If Not IsNothing(dticd) Then
                            dticd.Dispose()
                        End If
                        ''''''If any reason is given for procedure not performed''''''''''''
                        If Not IsNothing(procedure.entryRelationship) Then
                            If procedure.entryRelationship.Length > 0 Then
                                For i As Integer = 0 To procedure.entryRelationship.Length - 1
                                    Dim observation1 As POCD_MT000040UV02Observation = Nothing

                                    If Not IsNothing(procedure.entryRelationship(i).Item) Then
                                        observation1 = TryCast(procedure.entryRelationship(i).Item, POCD_MT000040UV02Observation)
                                        If Not IsNothing(observation1) Then
                                            If CType(procedure.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                                If Not IsNothing(CType(procedure.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                    If CType(CType(procedure.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then


                                                        history.ReasonConceptId = CType(CType(procedure.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                        history.ReasonDesc = GetReasonDescription(history.ReasonConceptId)

                                                    End If
                                                End If

                                            End If


                                        End If

                                    End If
                                Next

                            End If


                        End If

                    End If
                    patienthistorycol.Add(history)
                    If Not IsNothing(history1) Then
                        patienthistorycol.Add(history1)
                        history1.Dispose()
                    End If

                    history.Dispose()



                    'End If
                Else
                    If Convert.ToString(procedureentry.Item) = "POCD_MT000040UV02Observation" Then
                        observation = CType(procedureentry.Item, POCD_MT000040UV02Observation)
                        'Dim TemlateCount As Integer = 0
                        'Dim entryfound As Boolean = False
                        'For TemlateCount = 0 To observation.templateId.Length - 1

                        '    If templatephysicalperformed = observation.templateId(TemlateCount).root.ToString() Then
                        '        entryfound = True
                        '    End If
                        'Next
                        'If entryfound = True Then
                        Dim history As gloPatientHistory = New gloPatientHistory()
                        cptcode = ""
                        cptdesciption = ""
                        icdcode = ""
                        icddescription = ""
                        Dim dticd As DataTable = New DataTable()
                        'Dim HCPCScode As String = ""
                        If Not IsNothing(observation) Then
                            If Convert.ToString(CType(observation.code, CD).codeSystem) = gloCCDSchema.CodeSystem.SNOMED_CT Then
                                history.ConceptId = Convert.ToString(CType(observation.code, CD).code)
                                Dim ed As ED
                                ed = CType(observation.code, CD).originalText
                                If Not IsNothing(ed) Then
                                    Dim text As String = Convert.ToString(ed.Text(0))
                                    Dim arr As String()
                                    If text.Contains(":") Then
                                        arr = text.Split(":")
                                        If arr.Length > 0 Then
                                            text = arr(1)
                                        End If
                                    End If
                                    history.SnoDescription = text
                                    text = Nothing
                                    arr = Nothing
                                End If



                                ed = Nothing

                            ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = gloCCDSchema.CodeSystem.CPT Then
                                cptcode = Convert.ToString(CType(observation.code, CD).code)
                            ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD9 Then
                                icdcode = Convert.ToString(CType(observation.code, CD).code)
                                dticd = GetICDdescription(icdcode, "9")
                                history.ICDRevision = 9
                            ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = gloCCDSchema.CodeSystem.ICD10 Then
                                icdcode = Convert.ToString(CType(observation.code, CD).code)
                                dticd = GetICDdescription(icdcode, "10")
                                history.ICDRevision = 10
                            ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = gloCCDSchema.CodeSystem.HCPCS Then
                                cptcode = Convert.ToString(CType(observation.code, CD).code)
                            End If


                            ''''TRanslation code'''''
                            If Not IsNothing(CType(observation.code, CD).translation) Then
                                If CType(observation.code, CD).translation.Length > 0 Then
                                    For index As Integer = 0 To CType(observation.code, CD).translation.Length - 1

                                        If (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                            history.ConceptId = Convert.ToString(CType(observation.code, CD).translation(index).code)
                                            Dim ed As ED
                                            ed = CType(observation.code, CD).originalText
                                            Dim text As String = Convert.ToString(ed.Text(0))
                                            Dim arr As String()
                                            If text.Contains(":") Then
                                                arr = text.Split(":")
                                                If arr.Length > 0 Then
                                                    text = arr(1)
                                                End If
                                            End If
                                            history.SnoDescription = text
                                            ed = Nothing
                                            text = Nothing
                                            arr = Nothing
                                        ElseIf (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.CPT) Then
                                            cptcode = Convert.ToString(CType(observation.code, CD).translation(index).code)

                                        ElseIf (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.HCPCS) Then
                                            cptcode = Convert.ToString(CType(observation.code, CD).translation(index).code)
                                        ElseIf (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                            icdcode = Convert.ToString(CType(observation.code, CD).translation(index).code)
                                            dticd = GetICDdescription(icdcode, "10")
                                        ElseIf (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                            icdcode = Convert.ToString(CType(observation.code, CD).translation(index).code)
                                            dticd = GetICDdescription(icdcode, "9")
                                        End If

                                    Next
                                End If
                            End If

                            If cptcode <> "" Then
                                Dim dt As DataTable = getCPTDescription(cptcode)
                                If dt.Rows.Count > 0 Then
                                    cptdesciption = Convert.ToString(dt.Rows(0)("sdescription"))
                                End If

                                history.CPT = cptcode & " : " & cptdesciption
                                If Not IsNothing(dt) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                            End If
                            If dticd.Rows.Count > 0 Then
                                icddescription = dticd.Rows(0)("sDescription")
                                history.ICD9 = icdcode & " : " & icddescription
                            End If

                            If Not IsNothing(observation.effectiveTime) Then

                                If observation.effectiveTime.Items.Length > 0 Then
                                    If Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                        history.OnsetDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).value)))
                                    End If
                                End If


                            End If
                            ''Reason ConceptID
                            Dim ob As ANY()
                            ob = CType(observation.value, ANY())

                            If ob.GetValue(0).GetType().FullName = "CD" Then
                                If Not IsNothing(CType(ob.GetValue(0), CD).codeSystem) Then


                                    If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.SNOMED_CT Then

                                        history.ReasonConceptId = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                        history.ReasonDesc = GetReasonDescription(history.ReasonConceptId)

                                    End If
                                End If
                            End If

                            If dthistory.Rows.Count > 0 Then
                                history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                                history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                                history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                                history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
                            End If

                            If Not IsNothing(dticd) Then
                                dticd.Dispose()
                            End If

                        End If
                        patienthistorycol.Add(history)
                        history.Dispose()
                        'End If
                    End If

                End If

            End If
            'Next
            If Not IsNothing(dthistory) Then
                dthistory.Dispose()
            End If
            '    End If

            'End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            procedure = Nothing
            observation = Nothing
            cptcode = Nothing
            cptdesciption = Nothing
            icdcode = Nothing
            icddescription = Nothing
        End Try
        'patienthistorycol = getPatientRiskcategory(entryarray, patienthistorycol)
        Return patienthistorycol
    End Function
    Private Function getPatientProcedureIntelorence(ByVal procedureentry As POCD_MT000040UV02Entry, ByRef immcol As ImmunizationCol, Optional ByVal isIntervention As Boolean = False, Optional ByVal _onlyonesnomed As Boolean = False) As ImmunizationCol
        'Dim patienthistorycol As gloPatientHistoryCol = New gloPatientHistoryCol
        Dim procedure As POCD_MT000040UV02Procedure = Nothing
        Dim procedureact As POCD_MT000040UV02Act = Nothing
        Dim observation As POCD_MT000040UV02Observation = Nothing
        Dim cptcode As String = ""
        Dim cptdesciption As String = ""
        Dim icdcode As String = ""
        Dim icddescription As String = ""
        Try

            'Dim TemplateIDEntry As String = ""
            'Dim templatephysicalperformed As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("ProcedurePerformed")
            'templatephysicalperformed = oTemplateIDMaster.GetQRDAEntryID("PhysicalExamPerformed")
            'If Not IsNothing(entryarray) Then
            '    If entryarray.Length > 0 Then
            '  Dim dthistory As DataTable = getPatientprocedurehistory("proc")
            'Dim EntryCount As Integer = 0
            'For EntryCount = 0 To entryarray.Length - 1
            '    Dim procedureentry As POCD_MT000040UV02Entry = Nothing
            '    procedureentry = entryarray(EntryCount)
            If Not IsNothing(procedureentry) Then


                Dim proc As POCD_MT000040UV02Procedure
                ' Dim history As gloPatientHistory = New gloPatientHistory()
                Dim immunization As Immunization = New Immunization()
                cptcode = ""
                cptdesciption = ""
                icdcode = ""
                icddescription = ""
                Dim dticd As DataTable = New DataTable()
                If Convert.ToString(procedureentry.Item) = "POCD_MT000040UV02Observation" Then
                    observation = CType(procedureentry.Item, POCD_MT000040UV02Observation)
                    If Not IsNothing(observation.entryRelationship) Then
                        If observation.entryRelationship.Length > 0 Then
                            If Not IsNothing(observation.entryRelationship(0)) Then

                                If observation.entryRelationship(0).typeCode = ActRelationshipType.CAUS Then
                                    proc = observation.entryRelationship(0).Item
                                    If proc.classCode = ActClassProcedure.PROC Then ''Proc change to ActClassProcedure
                                        If Not IsNothing(proc.templateId) Then
                                            If proc.templateId.Length > 0 Then
                                                For i As Integer = 0 To proc.templateId.Length - 1
                                                    If proc.templateId(i).root = "2.16.840.1.113883.10.20.24.3.64" Then
                                                        If Convert.ToString(CType(proc.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                                            immunization.ConceptID = Convert.ToString(CType(proc.code, CD).code)

                                                        ElseIf Convert.ToString(CType(proc.code, CD).codeSystem) = CodeSystem.CPT Then
                                                            cptcode = Convert.ToString(CType(proc.code, CD).code)
                                                        ElseIf Convert.ToString(CType(proc.code, CD).codeSystem) = CodeSystem.ICD9 Then
                                                            icdcode = Convert.ToString(CType(proc.code, CD).code)
                                                            dticd = GetICDdescription(icdcode, "9")
                                                        ElseIf Convert.ToString(CType(proc.code, CD).codeSystem) = CodeSystem.ICD10 Then
                                                            icdcode = Convert.ToString(CType(proc.code, CD).code)
                                                            dticd = GetICDdescription(icdcode, "10")
                                                        ElseIf Convert.ToString(CType(proc.code, CD).codeSystem) = CodeSystem.HCPCS Then
                                                            cptcode = Convert.ToString(CType(proc.code, CD).code)
                                                        End If


                                                        ''''TRanslation code'''''
                                                        If Not IsNothing(CType(proc.code, CD).translation) Then
                                                            If CType(proc.code, CD).translation.Length > 0 Then
                                                                For index As Integer = 0 To CType(proc.code, CD).translation.Length - 1

                                                                    If (Convert.ToString(CType(proc.code, CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                                        immunization.ConceptID = Convert.ToString(CType(proc.code, CD).translation(index).code)

                                                                    ElseIf (Convert.ToString(CType(proc.code, CD).translation(index).codeSystem) = CodeSystem.CPT) Then
                                                                        cptcode = Convert.ToString(CType(proc.code, CD).translation(index).code)

                                                                    ElseIf (Convert.ToString(CType(proc.code, CD).translation(index).codeSystem) = CodeSystem.HCPCS) Then
                                                                        cptcode = Convert.ToString(CType(proc.code, CD).translation(index).code)
                                                                    ElseIf (Convert.ToString(CType(proc.code, CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                                                        icdcode = Convert.ToString(CType(proc.code, CD).translation(index).code)
                                                                        dticd = GetICDdescription(icdcode, "10")
                                                                    ElseIf (Convert.ToString(CType(proc.code, CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                                                        icdcode = Convert.ToString(CType(proc.code, CD).translation(index).code)
                                                                        dticd = GetICDdescription(icdcode, "9")
                                                                    End If

                                                                Next
                                                            End If
                                                        End If

                                                        If cptcode <> "" Then
                                                            Dim dt1 As DataTable = getCPTDescription(cptcode)
                                                            If dt1.Rows.Count > 0 Then
                                                                cptdesciption = Convert.ToString(dt1.Rows(0)("sdescription"))
                                                            End If

                                                            immunization.CPTCode = cptcode ' & " : " & cptdesciption
                                                            If Not IsNothing(dt1) Then
                                                                dt1.Dispose()
                                                                dt1 = Nothing
                                                            End If
                                                        End If
                                                        If dticd.Rows.Count > 0 Then
                                                            icddescription = dticd.Rows(0)("sDescription")
                                                            immunization.ICDCode = icdcode '& " : " & icddescription
                                                        End If

                                                        If Not IsNothing(proc.effectiveTime) Then

                                                            If proc.effectiveTime.Items.Length > 0 Then
                                                                If Convert.ToString(CType(proc.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                                                    immunization.ImmunizationDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(proc.effectiveTime.Items(0), IVXB_TS).value)))
                                                                End If
                                                            End If

                                                        End If
                                                        immunization.admin_report_refused = 2
                                                        immunization.Reason = "unKnown"
                                                        immunization.ReasonConceptID = "UNK"



                                                        immunization.LotNumber = "12345"
                                                        Dim dt As DataTable = getimmcvx()
                                                        If dt.Rows.Count > 0 Then
                                                            immunization.VaccineCode = Convert.ToString(dt.Rows(0)("cvxcode"))
                                                            immunization.ImmunizationTrade = Convert.ToString(dt.Rows(0)("CdcProductName"))
                                                            immunization.VaccineName = Convert.ToString(dt.Rows(0)("ShortDescription"))
                                                        End If
                                                        If Not IsNothing(dt) Then
                                                            dt.Dispose()
                                                            dt = Nothing
                                                        End If

                                                        'If dthistory.Rows.Count > 0 Then
                                                        '    history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                                                        '    history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                                                        '    history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                                                        '    history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
                                                        'End If

                                                        If Not IsNothing(dticd) Then
                                                            dticd.Dispose()
                                                        End If
                                                        immcol.Add(immunization)
                                                        immunization.Dispose()

                                                    End If

                                                Next
                                            End If

                                        End If
                                    End If

                                End If

                            End If
                        End If

                    End If



                End If

            End If
            'Next
            'If Not IsNothing(dthistory) Then
            '    dthistory.Dispose()
            'End If
            '    End If

            'End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            procedure = Nothing
            observation = Nothing
            cptcode = Nothing
            cptdesciption = Nothing
            icdcode = Nothing
            icddescription = Nothing
        End Try
        'patienthistorycol = getPatientRiskcategory(entryarray, patienthistorycol)
        Return immcol
    End Function

    'Private Sub getPatientRiskcategory(ByVal riskentry As POCD_MT000040UV02Entry, ByRef historycol As gloPatientHistoryCol, ByVal riskobservation As POCD_MT000040UV02Observation, ByRef labresultcol As LabResultsCol)
    '    'Dim labresultcol As New LabResultsCol
    '    Dim dthistory As DataTable = Nothing
    '    Dim dt As DataTable = Nothing
    '    Dim snomedcode As String = ""
    '    Dim snodesc As String = ""
    '    Dim loinccode As String = ""
    '    Dim startdate As String = ""
    '    Try

    '        'Dim TemplateIDEntry As String = ""
    '        'Dim templatecharentry As String = ""
    '        'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("RiskCategoryAssesment")
    '        'templatecharentry = oTemplateIDMaster.GetQRDAEntryID("PatientChar")
    '        'If Not IsNothing(entryarray) Then
    '        '    If entryarray.Length > 0 Then
    '        dthistory = getPatientprocedurehistory("risk")
    '        dt = GetLabTestName()
    '        'Dim EntryCount As Integer = 0
    '        'For EntryCount = 0 To entryarray.Length - 1
    '        '    Dim riskentry As POCD_MT000040UV02Entry = Nothing
    '        'riskentry = entryarray(EntryCount)
    '        If Not IsNothing(riskentry) Then

    '            If Convert.ToString(riskentry.Item) = "POCD_MT000040UV02Observation" Then
    '                riskobservation = CType(riskentry.Item, POCD_MT000040UV02Observation)
    '                'Dim TemlateCount As Integer = 0
    '                'Dim entryfound As Boolean = False
    '                'For TemlateCount = 0 To riskobservation.templateId.Length - 1
    '                '    If TemplateIDEntry = riskobservation.templateId(TemlateCount).root.ToString() Then
    '                '        entryfound = True
    '                '    ElseIf templatecharentry = riskobservation.templateId(TemlateCount).root.ToString() Then
    '                '        entryfound = True
    '                '    End If
    '                'Next
    '                'If entryfound = True Then
    '                If Not IsNothing(riskobservation) Then
    '                    If riskobservation.negationInd = True Then
    '                        ' Dim entryrelation As POCD_MT000040UV02EntryRelationship()
    '                        'entryrelation=CType (riskobservation .entryRelationship (0)){}
    '                        Dim observationentry As POCD_MT000040UV02Observation

    '                        observationentry = CType(CType(riskobservation.entryRelationship.GetValue(0), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02Observation)

    '                        Dim ob As ANY()
    '                        ob = CType(observationentry.value, ANY())
    '                        If Not IsNothing(ob) Then
    '                            If ob.GetValue(0).GetType.Name = "CD" Then
    '                                If Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.SNOMED_CT Then
    '                                    snomedcode = Convert.ToString(CType(ob.GetValue(0), CD).code)
    '                                    Dim ed As ED
    '                                    'ed = CType(riskobservation.code, CD).originalText
    '                                    ed = CType(ob.GetValue(0),CD).originalText
    '                                    If Not IsNothing(ed) Then
    '                                        Dim text As String = Convert.ToString(ed.Text(0))
    '                                        Dim arr As String()
    '                                        If text.Contains(":") Then
    '                                            arr = text.Split(":")
    '                                            If arr.Length > 0 Then
    '                                                text = arr(1)
    '                                            End If
    '                                        End If
    '                                        snodesc = text
    '                                        text = Nothing
    '                                        arr = Nothing
    '                                    End If
    '                                    ed = Nothing
    '                                End If
    '                                If Not IsNothing(riskobservation.code) Then
    '                                    If Convert.ToString(riskobservation.code.codeSystem) = CodeSystem.LOINC Then
    '                                        loinccode = Convert.ToString(riskobservation.code.code)
    '                                    End If
    '                                End If

    '                            End If


    '                        End If
    '                    Else
    '                        If Not IsNothing(riskobservation.code) Then
    '                            If Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
    '                                snomedcode = Convert.ToString(CType(riskobservation.code, CD).code)
    '                                Dim ed As ED
    '                                ed = CType(riskobservation.code, CD).originalText
    '                                Dim text As String = Convert.ToString(ed.Text(0))
    '                                Dim arr As String()
    '                                If text.Contains(":") Then
    '                                    arr = text.Split(":")
    '                                    If arr.Length > 0 Then
    '                                        text = arr(1)
    '                                    End If
    '                                End If
    '                                snodesc = text
    '                                ed = Nothing
    '                                text = Nothing
    '                                arr = Nothing
    '                            ElseIf Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.LOINC Then
    '                                loinccode = Convert.ToString(CType(riskobservation.code, CD).code)
    '                            End If

    '                        End If
    '                        Dim ob As ANY()
    '                        ob = CType(riskobservation.value, ANY())
    '                        If Not IsNothing(ob) Then
    '                            If ob.GetValue(0).GetType.Name = "CD" Then
    '                                If Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.SNOMED_CT Then
    '                                    snomedcode = Convert.ToString(CType(ob.GetValue(0), CD).code)
    '                                    Dim ed As ED
    '                                    'ed = CType(riskobservation.code, CD).originalText
    '                                    ed = CType(ob.GetValue(0),CD).originalText
    '                                    If Not IsNothing(ed) Then
    '                                        Dim text As String = Convert.ToString(ed.Text(0))
    '                                        Dim arr As String()
    '                                        If text.Contains(":") Then
    '                                            arr = text.Split(":")
    '                                            If arr.Length > 0 Then
    '                                                text = arr(1)
    '                                            End If
    '                                        End If
    '                                        snodesc = text
    '                                        text = Nothing
    '                                        arr = Nothing
    '                                    End If
    '                                    ed = Nothing

    '                                ElseIf Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.LOINC Then
    '                                    loinccode = Convert.ToString(CType(ob.GetValue(0), CD).code)
    '                                End If
    '                            End If


    '                        End If
    '                    End If


    '                    If Not IsNothing(riskobservation.effectiveTime) Then
    '                        If riskobservation.effectiveTime.Items.Length > 0 Then
    '                            If Convert.ToString(CType(riskobservation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
    '                                startdate = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(riskobservation.effectiveTime.Items(0), IVXB_TS).value)))
    '                            End If
    '                        End If

    '                    End If
    '                    ''''if snomed is there then insert in history else insert in LOINC
    '                    If snomedcode <> "" Then
    '                        Dim history As New gloPatientHistory
    '                        history.ConceptId = snomedcode
    '                        history.SnoDescription = snodesc
    '                        If riskobservation.negationInd = True Then
    '                            history.DOEAllergy = startdate

    '                            history.OnsetDate = startdate
    '                        Else


    '                            history.OnsetDate = startdate
    '                        End If


    '                        If dthistory.Rows.Count > 0 Then
    '                            history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
    '                            history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
    '                            history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
    '                            history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
    '                        End If
    '                        historycol.Add(history)
    '                        history.Dispose()
    '                    End If
    '                    If snomedcode <> "" AndAlso loinccode <> "" Then

    '                        'Dim labtest As New LabTest

    '                        Dim labresult As New LabResults
    '                        labresult.ResultLOINCID = loinccode
    '                        labresult.TestLOINCID = loinccode
    '                        labresult.ResultComment = ""
    '                        labresult.ResultName = "Result1"
    '                        labresult.ResultRange = "2-50"
    '                        labresult.ResultValue = "40"
    '                        labresult.ResultUnit = "mg"
    '                        labresult.SpecimenDate = startdate
    '                        'labresult.ResultDate = startdate
    '                        'labresult.SpecimenDate = startdate
    '                        If dt.Rows.Count > 0 Then
    '                            labresult.TestCode = Convert.ToString(dt.Rows(0)("labtm_Code"))
    '                            labresult.TestName = Convert.ToString(dt.Rows(0)("labtm_Name"))
    '                            labresult.ResultName = Convert.ToString(dt.Rows(0)("labtm_Name"))
    '                        End If

    '                        labresultcol.Add(labresult)
    '                        labresult.Dispose()
    '                    Else
    '                        If loinccode <> "" And snomedcode = "" Then
    '                            Dim labresult As New LabResults
    '                            labresult.ResultLOINCID = loinccode
    '                            labresult.TestLOINCID = loinccode
    '                            labresult.ResultComment = ""
    '                            labresult.ResultName = "Result1"
    '                            labresult.ResultRange = "2-50"
    '                            labresult.ResultValue = "40"
    '                            labresult.ResultUnit = "mg"
    '                            labresult.SpecimenDate = startdate
    '                            'labresult.ResultDate = startdate
    '                            'labresult.SpecimenDate = startdate
    '                            If dt.Rows.Count > 0 Then
    '                                labresult.TestCode = Convert.ToString(dt.Rows(0)("labtm_Code"))
    '                                labresult.TestName = Convert.ToString(dt.Rows(0)("labtm_Name"))
    '                                labresult.ResultName = Convert.ToString(dt.Rows(0)("labtm_Name"))
    '                            End If

    '                            labresultcol.Add(labresult)
    '                            labresult.Dispose()
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '        'End If
    '        'Next
    '        '    End If
    '        'End If
    '        'oReconcileList.mPatient.PatientLabResult = labresultcol
    '        'oReconcileList.mPatient.PatientHistory = historycol
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
    '        ex = Nothing
    '    Finally
    '        snomedcode = Nothing
    '        snodesc = Nothing
    '        loinccode = Nothing
    '        startdate = Nothing
    '        If Not IsNothing(dt) Then
    '            dt.Dispose()
    '        End If
    '        If Not IsNothing(dthistory) Then
    '            dthistory.Dispose()
    '        End If
    '    End Try

    '    'Return historycol
    'End Sub

    Private Sub getPatientRiskcategory_New(ByVal riskentry As POCD_MT000040UV02Entry, ByRef historycol As gloPatientHistoryCol, ByVal riskobservation As POCD_MT000040UV02Observation, ByRef labresultcol As LabResultsCol)
        'Dim labresultcol As New LabResultsCol
        Dim dthistory As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Dim snomedcode As String = ""
        Dim snodesc As String = ""
        Dim loinccode As String = ""
        Dim startdate As String = ""
        Dim ResonConceptID As String = ""
        Dim ResonConceptDesc As String = ""
        Dim ResonLOINCID As String = ""
        Dim ResonLOINCDesc As String = ""
        Dim valueset As String = ""
        'Dim valuesetDescription As String = ""
        Try


            dthistory = getPatientprocedurehistory("proc")
            dt = GetLabTestName()
            Dim value As String = Nothing
            Dim unit As String = Nothing
            If Not IsNothing(riskentry) Then

                If Convert.ToString(riskentry.Item) = "POCD_MT000040UV02Observation" Then
                    riskobservation = CType(riskentry.Item, POCD_MT000040UV02Observation)

                    If Not IsNothing(riskobservation) Then
                        If riskobservation.negationInd = True Then
                            If Not IsNothing(riskobservation.entryRelationship) Then


                                Dim observationentry As POCD_MT000040UV02Observation

                                observationentry = CType(CType(riskobservation.entryRelationship.GetValue(0), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02Observation)
                                If Not IsNothing(riskobservation.code) Then
                                    If Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                        snomedcode = Convert.ToString(CType(riskobservation.code, CD).code)
                                        Dim ed As ED
                                        ed = CType(riskobservation.code, CD).originalText
                                        Dim text As String = Convert.ToString(ed.Text(0))
                                        Dim arr As String()
                                        If text.Contains(":") Then
                                            arr = text.Split(":")
                                            If arr.Length > 0 Then
                                                text = arr(1)
                                            End If
                                        End If
                                        snodesc = text
                                        ed = Nothing
                                        text = Nothing
                                        arr = Nothing
                                    ElseIf Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.LOINC Then
                                        loinccode = Convert.ToString(CType(riskobservation.code, CD).code)
                                    End If
                                    If Not IsNothing(CType(riskobservation.code, CD).valueSet) Then
                                        valueset = Convert.ToString(CType(riskobservation.code, CD).valueSet)
                                        '   valuesetDescription = GetReasonDescriptionfromValueset(valueset)
                                    End If






                                    If Not IsNothing(CType(riskobservation.code, CD).translation) Then
                                        If Not IsNothing(CType(riskobservation.code, CD).translation.Length > 0) Then
                                            If Convert.ToString(CType(riskobservation.code, CD).translation(0).codeSystem) = CodeSystem.SNOMED_CT Then
                                                snomedcode = Convert.ToString(CType(riskobservation.code, CD).translation(0).code)
                                                Dim ed As ED
                                                ed = CType(riskobservation.code, CD).translation(0).originalText
                                                Dim text As String = Convert.ToString(ed.Text(0))
                                                Dim arr As String()
                                                If text.Contains(":") Then
                                                    arr = text.Split(":")
                                                    If arr.Length > 0 Then
                                                        text = arr(1)
                                                    End If
                                                End If
                                                snodesc = text
                                                ed = Nothing
                                                text = Nothing
                                                arr = Nothing
                                            ElseIf Convert.ToString(CType(riskobservation.code, CD).translation(0).codeSystem) = CodeSystem.LOINC Then
                                                loinccode = Convert.ToString(CType(riskobservation.code, CD).translation(0).code)
                                            End If
                                            'If Not IsNothing(CType(riskobservation.code, CD).valueSet) Then
                                            '    valueset = Convert.ToString(CType(riskobservation.code, CD).valueSet)
                                            '    '   valuesetDescription = GetReasonDescriptionfromValueset(valueset)
                                            'End If
                                        End If
                                    End If









                                End If
                                Dim ob As ANY()
                                ob = CType(observationentry.value, ANY())
                                If Not IsNothing(ob) Then
                                    If ob.GetValue(0).GetType.Name = "CD" Then

                                        If Not IsNothing(riskobservation.code) Then
                                            If Convert.ToString(riskobservation.code.codeSystem) = CodeSystem.LOINC Then
                                                loinccode = Convert.ToString(riskobservation.code.code)
                                            End If
                                        End If

                                    End If
                                    If Not IsNothing(riskobservation.entryRelationship) Then
                                        If riskobservation.entryRelationship.Length > 0 Then
                                            For i As Integer = 0 To riskobservation.entryRelationship.Length - 1
                                                If Not IsNothing(riskobservation.entryRelationship(i).Item) Then
                                                    If CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                                        If Not IsNothing(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                            If CType(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then

                                                                ResonConceptID = CType(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                                ResonConceptDesc = GetReasonDescription(ResonConceptID)

                                                            End If

                                                        End If


                                                    End If

                                                End If
                                            Next

                                        End If


                                    End If

                                End If
                            End If
                        Else
                            If Not IsNothing(riskobservation.code) Then
                                If Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                    snomedcode = Convert.ToString(CType(riskobservation.code, CD).code)
                                    Dim ed As ED
                                    If Not IsNothing(CType(riskobservation.code, CD).originalText) Then
                                        ed = CType(riskobservation.code, CD).originalText
                                        Dim text As String = Convert.ToString(ed.Text(0))
                                        Dim arr As String()
                                        If text.Contains(":") Then
                                            arr = text.Split(":")
                                            If arr.Length > 0 Then
                                                text = arr(1)
                                            End If
                                        End If
                                        snodesc = text
                                        ed = Nothing
                                        text = Nothing
                                        arr = Nothing
                                    End If

                                ElseIf Convert.ToString(CType(riskobservation.code, CD).codeSystem) = CodeSystem.LOINC Then
                                    loinccode = Convert.ToString(CType(riskobservation.code, CD).code)
                                End If


                                If Not IsNothing(CType(riskobservation.code, CD).translation) Then
                                    If Not IsNothing(CType(riskobservation.code, CD).translation.Length > 0) Then
                                        If Convert.ToString(CType(riskobservation.code, CD).translation(0).codeSystem) = CodeSystem.SNOMED_CT Then
                                            snomedcode = Convert.ToString(CType(riskobservation.code, CD).translation(0).code)
                                            Dim ed As ED
                                            If (Not IsNothing(CType(riskobservation.code, CD).translation(0).originalText)) Then
                                                ed = CType(riskobservation.code, CD).translation(0).originalText
                                                Dim text As String = Convert.ToString(ed.Text(0))
                                                Dim arr As String()
                                                If text.Contains(":") Then
                                                    arr = text.Split(":")
                                                    If arr.Length > 0 Then
                                                        text = arr(1)
                                                    End If
                                                End If
                                                snodesc = text
                                                ed = Nothing
                                                text = Nothing
                                                arr = Nothing
                                            End If


                                        ElseIf Convert.ToString(CType(riskobservation.code, CD).translation(0).codeSystem) = CodeSystem.LOINC Then
                                            loinccode = Convert.ToString(CType(riskobservation.code, CD).translation(0).code)
                                        End If
                                        'If Not IsNothing(CType(riskobservation.code, CD).valueSet) Then
                                        '    valueset = Convert.ToString(CType(riskobservation.code, CD).valueSet)
                                        '    '   valuesetDescription = GetReasonDescriptionfromValueset(valueset)
                                        'End If
                                    End If
                                End If




                            End If

                            Dim ob As ANY()
                            ob = CType(riskobservation.value, ANY())

                            If riskobservation.entryRelationship IsNot Nothing AndAlso TypeOf (riskobservation.entryRelationship) Is POCD_MT000040UV02EntryRelationship() Then
                                Dim entryRelationship As POCD_MT000040UV02EntryRelationship() = CType(riskobservation.entryRelationship, POCD_MT000040UV02EntryRelationship())

                                If entryRelationship(0).Item IsNot Nothing AndAlso TypeOf (entryRelationship(0).Item) Is POCD_MT000040UV02Observation Then
                                    Dim observation As POCD_MT000040UV02Observation = CType(entryRelationship(0).Item, POCD_MT000040UV02Observation)

                                    If observation.value IsNot Nothing AndAlso TypeOf (observation.value) Is ANY() Then
                                        Dim observationValue As ANY() = CType(observation.value, ANY())

                                        If observationValue(0) IsNot Nothing AndAlso TypeOf (observationValue(0)) Is CD Then
                                            Dim cd As CD = CType(observationValue(0), CD)

                                            If cd.codeSystem = CodeSystem.SNOMED_CT Then
                                                snomedcode = Convert.ToString(cd.code)
                                            End If
                                            cd = Nothing
                                        End If
                                        observationValue = Nothing
                                        observation = Nothing
                                    End If
                                    entryRelationship = Nothing
                                End If
                            End If

                            If Not IsNothing(ob) Then
                                If ob.GetValue(0).GetType.Name = "CD" Then
                                    If Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                        snomedcode = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                        Dim ed As ED
                                        'ed = CType(riskobservation.code, CD).originalText
                                        ed = CType(ob.GetValue(0), CD).originalText
                                        If Not IsNothing(ed) Then
                                            Dim text As String = Convert.ToString(ed.Text(0))
                                            Dim arr As String()
                                            If text.Contains(":") Then
                                                arr = text.Split(":")
                                                If arr.Length > 0 Then
                                                    text = arr(1)
                                                End If
                                            End If
                                            snodesc = text
                                            text = Nothing
                                            arr = Nothing
                                        End If
                                        ed = Nothing

                                    ElseIf Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.LOINC Then
                                        loinccode = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                    End If
                                ElseIf ob.GetValue(0).GetType.Name = "PQ" Then
                                    value = DirectCast(ob.GetValue(0), PQ).value
                                    unit = DirectCast(ob.GetValue(0), PQ).unit
                                End If


                            End If

                            If Not IsNothing(riskobservation.entryRelationship) Then
                                If riskobservation.entryRelationship.Length > 0 Then
                                    For i As Integer = 0 To riskobservation.entryRelationship.Length - 1
                                        If Not IsNothing(riskobservation.entryRelationship(i).Item) Then
                                            If CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "73831-0" Or CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "73832-8" Then
                                                If Not IsNothing(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                    If CType(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then

                                                        ResonConceptID = CType(CType(riskobservation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                        ResonConceptDesc = GetReasonDescription(ResonConceptID)

                                                    End If

                                                End If


                                            End If

                                        End If
                                    Next

                                End If


                            End If
                        End If


                        If Not IsNothing(riskobservation.effectiveTime) Then
                            If riskobservation.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(riskobservation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    startdate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(riskobservation.effectiveTime.Items(0), IVXB_TS).value)))
                                End If
                            End If

                        End If

                        If (Not IsNothing(riskobservation.entryRelationship) AndAlso (riskobservation.entryRelationship.Length > 0)) Then
                            'If Not IsNothing(riskobservation.entryRelationship Then

                            'End If
                            Dim entryrelationship As POCD_MT000040UV02EntryRelationship() = Nothing
                            entryrelationship = DirectCast(riskobservation.entryRelationship, POCD_MT000040UV02EntryRelationship())
                            If entryrelationship.Length > 0 Then
                                If Not IsNothing(entryrelationship) AndAlso Not IsNothing(entryrelationship(0)) Then
                                    Dim obs As POCD_MT000040UV02Observation = Nothing
                                    obs = DirectCast(entryrelationship(0).Item, POCD_MT000040UV02Observation)
                                    If Not IsNothing(obs) Then
                                        If Not IsNothing(obs.effectiveTime) Then
                                            startdate = gloDateMaster.gloDate.QRDADateAsDateTime(Convert.ToString(obs.effectiveTime.value))
                                        End If

                                    End If

                                End If
                            End If



                        End If


                        If startdate = "" Then
                            If Not IsNothing(riskobservation.author) AndAlso riskobservation.author.Length > 0 Then
                                Dim author As POCD_MT000040UV02Author = Nothing
                                author = CType(riskobservation.author(0), POCD_MT000040UV02Author)
                                If Not IsNothing(author.time) Then
                                    startdate = gloDateMaster.gloDate.QRDADateAsDateTime(Convert.ToString(author.time.value))
                                End If

                            End If
                        End If


                        ''''if snomed is there then insert in history else insert in LOINC

                        If snomedcode <> "" AndAlso loinccode <> "" Then

                            'Dim labtest As New LabTest

                            Dim labresult As New LabResults
                            labresult.ResultLOINCID = loinccode
                            labresult.TestLOINCID = loinccode
                            labresult.ResultComment = ""
                            labresult.ResultName = "Result1"
                            labresult.ResultRange = "2-50"
                            If Not IsNothing(value) Then
                                labresult.ResultValue = value
                            Else
                                labresult.ResultValue = "40"
                            End If
                            If Not IsNothing(unit) Then
                                labresult.ResultUnit = unit
                            Else
                                labresult.ResultUnit = "mg"
                            End If

                            labresult.SpecimenDate = startdate
                            'If snomedcode <> "" AndAlso ResonConceptID = "" Then
                            '    labresult.ResultReasonConceptID = snomedcode
                            'ElseIf snomedcode <> "" AndAlso ResonConceptID <> "" Then
                            labresult.ResultReasonConceptID = snomedcode

                            'End If
                            labresult.ValusetOID = valueset
                            labresult.Valueset = GetReasonDescriptionfromValueset(valueset)
                            'labresult.ResultDate = startdate
                            'labresult.SpecimenDate = startdate
                            If dt.Rows.Count > 0 Then
                                labresult.TestCode = Convert.ToString(dt.Rows(0)("labtm_Code"))
                                labresult.TestName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                                labresult.ResultName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                            End If

                            labresultcol.Add(labresult)
                            labresult.Dispose()
                        ElseIf loinccode <> "" And snomedcode = "" Or (snomedcode = "" AndAlso loinccode = "") Then
                            Dim labresult As New LabResults
                            labresult.ResultLOINCID = loinccode
                            labresult.TestLOINCID = loinccode
                            labresult.ResultComment = ""
                            labresult.ResultName = "Result1"
                            labresult.ResultRange = "2-50"
                            If Not IsNothing(value) Then
                                labresult.ResultValue = value
                            Else
                                labresult.ResultValue = "40"
                            End If
                            If Not IsNothing(unit) Then
                                labresult.ResultUnit = unit
                            Else
                                labresult.ResultUnit = "mg"
                            End If
                            labresult.SpecimenDate = startdate
                            labresult.ResultReasonConceptID = ResonConceptID
                            labresult.ValusetOID = valueset
                            labresult.Valueset = GetReasonDescriptionfromValueset(valueset)
                            'labresult.ResultDate = startdate
                            'labresult.SpecimenDate = startdate
                            If dt.Rows.Count > 0 Then
                                labresult.TestCode = Convert.ToString(dt.Rows(0)("labtm_Code"))
                                labresult.TestName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                                labresult.ResultName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                            End If

                            labresultcol.Add(labresult)
                            labresult.Dispose()

                            'insert loincode in history also'
                            Dim history As New gloPatientHistory
                            history.ConceptId = snomedcode
                            history.SnoDescription = snodesc
                            history.ReasonConceptId = ResonConceptID
                            history.ReasonDesc = ResonConceptDesc
                            history.Loinccode = loinccode
                            If riskobservation.negationInd = True Then
                                history.DOEAllergy = startdate

                                history.OnsetDate = startdate
                            Else


                                history.OnsetDate = startdate

                            End If

                            If dthistory.Rows.Count > 0 Then
                                history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                                history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                                history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                                history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
                            End If
                            historycol.Add(history)
                            history.Dispose()


                        End If

                    End If
                    If snomedcode <> "" Or (loinccode <> "" AndAlso ResonConceptID <> "") Then
                        Dim history As New gloPatientHistory
                        history.ConceptId = snomedcode
                        history.SnoDescription = snodesc
                        history.ReasonConceptId = ResonConceptID
                        history.ReasonDesc = ResonConceptDesc
                        history.Loinccode = loinccode
                        If riskobservation.negationInd = True Then
                            history.DOEAllergy = startdate

                            history.OnsetDate = startdate
                        Else


                            history.OnsetDate = startdate
                        End If

                        history.ValueSetOID = valueset
                        history.ValueSet = GetReasonDescriptionfromValueset(valueset)
                        If dthistory.Rows.Count > 0 Then
                            history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                            history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                            history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                            history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
                        End If
                        historycol.Add(history)
                        history.Dispose()

                    End If

                End If
            End If

            'End If
            'Next
            '    End If
            'End If
            'oReconcileList.mPatient.PatientLabResult = labresultcol
            'oReconcileList.mPatient.PatientHistory = historycol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            snomedcode = Nothing
            snodesc = Nothing
            loinccode = Nothing
            startdate = Nothing
            If Not IsNothing(dt) Then
                dt.Dispose()
            End If
            If Not IsNothing(dthistory) Then
                dthistory.Dispose()
            End If
        End Try

        'Return historycol
    End Sub
    Private Function getPatientprocedurehistory(ByVal type As String)
        Dim dt As DataTable = New DataTable()
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim dbpara As gloDatabaseLayer.DBParameters = Nothing
        Try
            oDB.Connect(False)
            dbpara = New gloDatabaseLayer.DBParameters
            dbpara.Add("@action", type, ParameterDirection.Input, SqlDbType.NVarChar)
            oDB.Retrive("gsp_getProcedureHistory", dbpara, dt)
            If type = "proc" Then
                If dt.Rows.Count <= 0 Then
                    Try
                        Dim cmd As New SqlCommand
                        Dim conn As New SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString)
                        cmd = New SqlCommand("gsp_QRDAInsertCategory", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Dispose()
                        oDB.Retrive("gsp_getProcedureHistory", dbpara, dt)
                    Catch ex As Exception

                    End Try

                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            If Not IsNothing(dbpara) Then
                dbpara.Dispose()
                dbpara = Nothing
            End If
        End Try
        Return dt
    End Function
    Private Function getPatientLabresults(ByVal entry As POCD_MT000040UV02Entry, ByVal entryresultfound As Boolean, ByVal entryorderfound As Boolean, ByVal LabTestperformedEntry As Boolean, ByVal entryfunctionalperformedfound As Boolean, ByRef labresultcol As LabResultsCol, ByRef oproblemcol As ProblemsCol, ByRef oHistorycol As gloPatientHistoryCol) As LabResultsCol
        Dim dt As DataTable = Nothing
        Dim observation As POCD_MT000040UV02Observation = Nothing
        Try


            'If Not IsNothing(entryarray) Then

            'Dim functionalEntry As String = ""
            'Dim LabTestEntry As String = ""
            'Dim DiagnosticResultEntry As String = ""
            'Dim DiagnosticOrderEntry As String = ""
            'Dim LabtestResult As String = ""
            'Dim diagnosticperformed As String = ""
            'functionalEntry = oTemplateIDMaster.GetQRDAEntryID("FunctionalStatus")
            'LabTestEntry = oTemplateIDMaster.GetQRDAEntryID("LaboratoryTestOrder")
            'LabtestResult = oTemplateIDMaster.GetQRDAEntryID("LabTestResults")
            'DiagnosticOrderEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosisOrdered")
            'DiagnosticResultEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosticResults")
            'diagnosticperformed = oTemplateIDMaster.GetQRDAEntryID("DiagnosticstudyPerformed")
            'If entryarray.Length > 0 Then
            dt = GetLabTestName()
            'Dim EntryCount As Integer = 0
            'For EntryCount = 0 To entryarray.Length - 1
            'Dim entry As POCD_MT000040UV02Entry = Nothing
            'entry = entryarray(EntryCount)
            If Not IsNothing(entry) Then
                If Convert.ToString(entry.Item) = "POCD_MT000040UV02Observation" Then
                    observation = CType(entry.Item, POCD_MT000040UV02Observation)
                    'Dim TemlateCount As Integer = 0
                    'Dim entryresultfound As Boolean = False
                    'Dim entryorderfound As Boolean = False
                    'For TemlateCount = 0 To observation.templateId.Length - 1
                    '    If functionalEntry = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryresultfound = True
                    '    ElseIf LabTestEntry = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryorderfound = True
                    '    ElseIf DiagnosticResultEntry = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryresultfound = True
                    '    ElseIf DiagnosticOrderEntry = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryorderfound = True
                    '    ElseIf LabtestResult = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryresultfound = True
                    '    ElseIf diagnosticperformed = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryresultfound = True
                    '    End If
                    'Next
                    If entryresultfound = True Or entryorderfound = True Or LabTestperformedEntry = True Or entryfunctionalperformedfound = True Then
                        Dim labresult As New LabResults
                        Dim loinccode As String = ""
                        Dim HCPCSCode As String = ""
                        Dim startdate As String = ""
                        If Not IsNothing(observation) Then
                            If Not IsNothing(observation.code) Then
                                If Convert.ToString(CType(observation.code, CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                    'snomedcode = Convert.ToString(CType(observation.code, CD).code)
                                    'Dim ed As ED
                                    'ed = CType(observation.code, CD).originalText
                                    'Dim text As String = Convert.ToString(ed.Text(0))
                                    'Dim arr As String()
                                    'If text.Contains(":") Then
                                    '    arr = text.Split(":")
                                    '    If arr.Length > 0 Then
                                    '        text = arr(1)
                                    '    End If
                                    'End If
                                    'snodesc = text
                                ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = CodeSystem.LOINC Then
                                    loinccode = Convert.ToString(CType(observation.code, CD).code)
                                    If Not IsNothing(CType(observation.code, CD).valueSet) Then
                                        'If CType(observation.code, CD).valueSet = "2.16.840.1.113883.3.464.1003.198.12.1013" Then
                                        '    loinccode = ""
                                        'End If

                                    End If
                                ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = "" AndAlso Convert.ToString(CType(observation.code, CD).code) = "" AndAlso Convert.ToString(CType(observation.code, CD).nullFlavor) = "NA" Then
                                    If Not IsNothing(CType(observation.code, CD).valueSet) Then
                                        labresult.ValusetOID = Convert.ToString(CType(observation.code, CD).valueSet)
                                        labresult.Valueset = GetReasonDescriptionfromValueset(Convert.ToString(CType(observation.code, CD).valueSet))
                                    End If
                                ElseIf Convert.ToString(CType(observation.code, CD).codeSystem) = CodeSystem.HCPCS Then
                                    HCPCSCode = Convert.ToString(CType(observation.code, CD).code)
                                End If
                                If Not IsNothing(CType(observation.code, CD).translation) Then
                                    If (CType(observation.code, CD).translation).Length > 0 Then
                                        For index As Integer = 0 To (CType(observation.code, CD).translation).Length - 1

                                            If (Convert.ToString(CType(observation.code, CD).translation(index).codeSystem) = CodeSystem.LOINC) Then
                                                loinccode = (Convert.ToString(CType(observation.code, CD).translation(index).code))
                                            End If
                                            'End If

                                        Next
                                    End If

                                End If

                            End If
                            Dim ob As ANY()
                            ob = CType(observation.value, ANY())
                            If Not IsNothing(ob) Then
                                'If Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.SNOMED_CT Then
                                '    'snomedcode = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                '    'Dim ed As ED
                                '    'ed = CType(observation.code, CD).originalText
                                '    'Dim text As String = Convert.ToString(ed.Text(0))
                                '    'Dim arr As String()
                                '    'If text.Contains(":") Then
                                '    '    arr = text.Split(":")
                                '    '    If arr.Length > 0 Then
                                '    '        text = arr(1)
                                '    '    End If
                                '    'End If
                                '    'snodesc = text
                                'ElseIf Convert.ToString(CType(ob.GetValue(0), CD).codeSystem) = CodeSystem.LOINC Then
                                '    loinccode = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                'End If

                                If ob.GetValue(0).GetType().FullName = "PQ" Then
                                    labresult.ResultValue = Convert.ToString(CType(ob.GetValue(0), PQ).value)


                                ElseIf ob.GetValue(0).GetType().FullName = "CD" Then
                                    If Not IsNothing(CType(ob.GetValue(0), CD).codeSystem) Then

                                        'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then
                                        If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.SNOMED_CT Then


                                            labresult.ResultReasonConceptID = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                        ElseIf CType(ob.GetValue(0), CD).codeSystem = CodeSystem.LOINC Then
                                            labresult.ResultReasonLOINC = Convert.ToString(CType(ob.GetValue(0), CD).code)

                                        ElseIf CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD10 Then
                                            labresult.ResultReasonICD9 = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                            ' labresult.ResultReasonICDRev = 10
                                        End If

                                        If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD9 Then
                                            labresult.ResultReasonICD10 = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                            ' labresult.ResultReasonICDRev = 9
                                        End If
                                    End If
                                    If Not IsNothing(CType(ob.GetValue(0), CD).translation) Then
                                        If CType(ob.GetValue(0), CD).translation.Length > 0 Then
                                            For index As Integer = 0 To CType(ob.GetValue(0), CD).translation.Length - 1


                                                If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                    labresult.ResultReasonConceptID = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)

                                                ElseIf (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                                    labresult.ResultReasonICD10 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                                ElseIf (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.LOINC) Then
                                                    loinccode = (Convert.ToString(CType(ob.GetValue(0), CD).code))
                                                End If
                                                'Else
                                                If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                                    labresult.ResultReasonICD9 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                                End If
                                                'End If

                                            Next
                                        End If
                                    End If

                                End If


                                ''commneeted -Mayuri Reasoncode
                                ''  getpatientproblems_LabtestPerformed(entry, oproblemcol, oHistorycol)
                            End If

                        End If

                        If Not IsNothing(observation.effectiveTime) Then
                            If observation.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    startdate = gloDateMaster.gloDate.QRDADateAsDateTime(Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).value))
                                End If
                            End If

                        End If
                        If entryorderfound = True Then
                            If Not IsNothing(observation.author) Then

                                startdate = gloDateMaster.gloDate.QRDADateAsDateTime(Convert.ToString(CType(CType(observation.author.GetValue(0), POCD_MT000040UV02Author).time, TS).value))
                            End If

                        End If
                        ''''''If any reason is given for procedure not performed''''''''''''
                        If Not IsNothing(observation.entryRelationship) Then
                            If observation.entryRelationship.Length > 0 Then
                                For i As Integer = 0 To observation.entryRelationship.Length - 1
                                    If Not IsNothing(observation.entryRelationship(i).Item) Then
                                        If CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                            If Not IsNothing(CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                If CType(CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then


                                                    labresult.ResultReasonConceptID = CType(CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).code
                                                    ' labresult.ReasonDesc = CType(CType(observation.entryRelationship(0).Item, POCD_MT000040UV02Observation).value(0),CD).displayName

                                                End If

                                            End If
                                        ElseIf CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "17856-6" Then
                                            Dim resultobservation As POCD_MT000040UV02Observation = Nothing
                                            resultobservation = CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation)
                                            Dim Value As ANY()
                                            If Not IsNothing(resultobservation.value) Then
                                                Value = CType(resultobservation.value, ANY())
                                                If Value.Length > 0 Then
                                                    If Value.GetValue(0).GetType.Name = "PQ" Then
                                                        labresult.ResultValue = Convert.ToString(CType(Value.GetValue(0), PQ).value)
                                                        If Not IsNothing(CType(Value.GetValue(0), PQ).unit) Then
                                                            labresult.ResultUnit = Convert.ToString(CType(Value.GetValue(0), PQ).unit)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                Next

                            End If


                        End If
                        ''''if snomed is there then insert in history else insert in LOINC
                        'If snomedcode <> "" Then
                        '    Dim history As New gloPatientHistory
                        '    history.ConceptId = snomedcode
                        '    history.SnoDescription = snodesc
                        '    history.OnsetDate = startdate
                        '    Dim dthistory As DataTable = getPatientprocedurehistory("risk")
                        '    If dthistory.Rows.Count > 0 Then
                        '        history.HistoryCategory = Convert.ToString(dthistory.Rows(0)("sDescription"))
                        '        history.HistoryItem = Convert.ToString(dthistory.Rows(0)("Description"))
                        '        history.HistoryType = Convert.ToString(dthistory.Rows(0)("sHistoryType"))
                        '        history.Comments = Convert.ToString(dthistory.Rows(0)("scomments"))
                        '    End If
                        '    historycol.Add(history)
                        'End If
                        If loinccode <> "" Or labresult.ResultReasonConceptID <> "" Then
                            'Dim labtest As New LabTest
                            labresult.ResultLOINCID = loinccode
                            labresult.TestLOINCID = loinccode

                            labresult.ResultComment = ""
                            labresult.ResultName = "Result1"
                            labresult.ResultRange = "2-50"
                            If labresult.ResultValue = "" Then
                                labresult.ResultValue = "40"
                            End If
                            If labresult.ResultUnit = "" Then
                                labresult.ResultUnit = "mg"
                            End If
                            labresult.SpecimenDate = startdate

                            'labresult.ResultDate = startdate
                            'labresult.SpecimenDate = startdate
                            If dt.Rows.Count > 0 Then
                                labresult.TestCode = Convert.ToString(dt.Rows(0)("labtm_Code"))
                                labresult.TestName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                                labresult.ResultName = Convert.ToString(dt.Rows(0)("labtm_Name"))
                            End If
                            'oReconcileList.mPatient.PatientLabResult.Add(labresult)
                            labresultcol.Add(labresult)
                            labresult.Dispose()
                        End If
                    End If
                End If
            End If


            'Next
            'End If
            'End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
            End If
            observation = Nothing
        End Try
        Return labresultcol
    End Function

    Private Function getPatientVitals(ByVal entry As POCD_MT000040UV02Entry, ByRef vitalscol As VitalsCol, ByRef HistroyCol As gloPatientHistoryCol, ByRef ResultCol As LabResultsCol) As VitalsCol
        Dim observation As POCD_MT000040UV02Observation = Nothing
        'Dim vitalscol As VitalsCol = New VitalsCol
        Try

            'Dim TemplateIDEntry As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("Vitals")
            'If Not IsNothing(entryarray) Then
            '    If entryarray.Length > 0 Then
            '        Dim EntryCount As Integer = 0
            '        For EntryCount = 0 To entryarray.Length - 1
            'Dim entry As POCD_MT000040UV02Entry = Nothing
            'entry = entryarray(EntryCount)
            If Not IsNothing(entry) Then
                If Convert.ToString(entry.Item) = "POCD_MT000040UV02Observation" Then
                    observation = CType(entry.Item, POCD_MT000040UV02Observation)
                    'Dim TemlateCount As Integer = 0
                    'Dim entryfound As Boolean = False
                    'For TemlateCount = 0 To observation.templateId.Length - 1
                    '    If TemplateIDEntry = observation.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True
                    '    End If
                    'Next
                    'If entryfound = True Then
                    Dim startdate As String = ""
                    Dim vital As Vitals = New Vitals
                    If Not IsNothing(observation) Then
                        Dim ob As ANY()
                        ob = CType(observation.value, ANY())
                        If ob.GetValue(0).GetType().FullName = "CD" Then
                            If DirectCast(ob(0), CD).nullFlavor = "UNK" Then

                                If observation.code.codeSystem = CodeSystem.SNOMED_CT Then
                                    ''CMS123v4	NQF0056 Physical Exam, Performed: Pulse Exam of Foot
                                    getPatientProcedurePerformed(entry, HistroyCol)
                                    Return vitalscol
                                    Exit Function
                                ElseIf observation.code.codeSystem = CodeSystem.LOINC Or (observation.code.codeSystem = "" AndAlso observation.code.nullFlavor = "NA") Then
                                    ''CMS69v4	NQF0421	BMI LOINC Value Not done
                                    ''********
                                    If Not IsNothing(observation.entryRelationship) Then
                                        If observation.entryRelationship.Length > 0 Then
                                            For i As Integer = 0 To observation.entryRelationship.Length - 1
                                                If Not IsNothing(observation.entryRelationship(i).Item) Then
                                                    If CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).code.code = "77301-0" Then
                                                        If Not IsNothing(CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value) Then
                                                            If CType(CType(observation.entryRelationship(i).Item, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                                ''********
                                                                getPatientRiskcategory_New(entry, HistroyCol, observation, ResultCol)
                                                                Return vitalscol 'ResultCol
                                                                Exit Function
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                ElseIf observation.code.codeSystem = "" AndAlso observation.code.nullFlavor = "NA" Then

                                End If


                            Else

                                If Convert.ToString(CType(observation.code, CD).code) = "8480-6" Then
                                    If Not IsNothing(ob) Then
                                        vital.BloodPressureSittingMax = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                    End If
                                ElseIf Convert.ToString(CType(observation.code, CD).code) = "8462-4" Then
                                    If Not IsNothing(ob) Then
                                        vital.BloodPressureSittingMin = Convert.ToString(CType(ob.GetValue(0), PQ).value)

                                    End If
                                ElseIf Convert.ToString(CType(observation.code, CD).code) = "39156-5" Then
                                    If Not IsNothing(ob) Then
                                        vital.BMI = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                    End If
                                ElseIf observation.code.codeSystem = CodeSystem.SNOMED_CT Then
                                    getPatientProcedurePerformed(entry, HistroyCol)
                                    Return vitalscol
                                    Exit Function
                                Else


                                End If
                            End If
                        Else
                            If Convert.ToString(CType(observation.code, CD).code) = "8480-6" Then
                                If Not IsNothing(ob) Then
                                    vital.BloodPressureSittingMax = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                End If
                            ElseIf Convert.ToString(CType(observation.code, CD).code) = "8462-4" Then
                                If Not IsNothing(ob) Then
                                    vital.BloodPressureSittingMin = Convert.ToString(CType(ob.GetValue(0), PQ).value)

                                End If
                            ElseIf Convert.ToString(CType(observation.code, CD).code) = "39156-5" Then
                                If Not IsNothing(ob) Then
                                    vital.BMI = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                End If
                            ElseIf observation.code.codeSystem = CodeSystem.SNOMED_CT Then
                                getPatientProcedurePerformed(entry, HistroyCol)
                                Return vitalscol
                                Exit Function
                            Else


                            End If
                        End If
                        If Not IsNothing(CType(observation.code, CD).translation) Then
                            If (CType(observation.code, CD).translation.Length > 0) Then
                                For index As Integer = 0 To (CType(observation.code, CD).translation.Length) - 1
                                    If Not IsNothing((CType(observation.code, CD).translation(index))) Then
                                        If (CType(observation.code, CD).translation(index).code) = "8480-6" Then
                                            vital.BloodPressureSittingMax = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                        ElseIf (CType(observation.code, CD).translation(index).code) = "8462-4" Then
                                            vital.BloodPressureSittingMin = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                        ElseIf (CType(observation.code, CD).translation(index).code) = "39156-5" Then
                                            vital.BMI = Convert.ToString(CType(ob.GetValue(0), PQ).value)
                                        End If
                                    End If
                                Next

                            End If

                        End If

                        If Not IsNothing(observation.effectiveTime) Then
                            If observation.effectiveTime.Items.Length > 0 Then
                                If Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                    startdate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(observation.effectiveTime.Items(0), IVXB_TS).value)))
                                End If
                            End If
                        End If
                    End If
                    vital.VitalDate = startdate
                    vital.HeightinInch = "60.00"
                    vital.Weightinlbs = "110.231"
                    vital.Temperature = "37"
                    vital.THRperMin = 75
                    vital.THRperMax = 85
                    vital.THRMin = 111.0
                    vital.THRMax = 125.8
                    Dim flag As Boolean = False
                    If vitalscol.Count > 0 Then
                        For Each vital1 As Vitals In vitalscol
                            If vital1.VitalDate = startdate Then
                                flag = True
                                If vital.BloodPressureSittingMax <> "" Then
                                    vital1.BloodPressureSittingMax = vital.BloodPressureSittingMax
                                End If
                                If vital.BloodPressureSittingMin <> "" Then
                                    vital1.BloodPressureSittingMin = vital.BloodPressureSittingMin
                                End If
                                If vital.BMI <> "" Then
                                    vital1.BMI = vital.BMI
                                End If


                            End If
                        Next
                    End If
                    If flag = False Then
                        vitalscol.Add(vital)
                        vital.Dispose()
                    End If


                    'If vital.BloodPressureSittingMin = "" Then
                    '    vital.BloodPressureSittingMin = "50"
                    'End If
                    'If vital.BloodPressureSittingMax = "" Then
                    '    vital.BloodPressureSittingMax = "85"
                    'End If
                    'If vital.BMI = "" Then
                    '    vital.BMI = "21.5"
                    'End If

                End If
            End If
            'End If
            '        Next
            '    End If
            'End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            observation = Nothing
        End Try
        Return vitalscol
    End Function
    Private Function getPatientImmunizations(ByVal entry As POCD_MT000040UV02Entry, ByRef immunizationcol As ImmunizationCol) As ImmunizationCol
        'Dim immunizationcol As New ImmunizationCol
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim subadmin As POCD_MT000040UV02SubstanceAdministration = Nothing
        Dim manufactureproduct As POCD_MT000040UV02ManufacturedProduct = Nothing
        Dim manufacturedmaterial As POCD_MT000040UV02Material = Nothing
        Try

            Dim immunization As New Immunization
            If Not IsNothing(entry) Then
                If Convert.ToString(entry.Item) = "POCD_MT000040UV02Act" Then
                    act = CType(entry.Item, POCD_MT000040UV02Act)
                    Dim TemlateCount As Integer = 0

                    'Dim immunization As New Immunization
                    If Not IsNothing(act.entryRelationship) Then
                        If act.entryRelationship.Length > 0 Then
                            For i As Integer = 0 To act.entryRelationship.Length - 1
                                If Not IsNothing(act.entryRelationship.GetValue(i)) Then
                                    If Not IsNothing(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item) Then
                                        If Convert.ToString(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item) = "POCD_MT000040UV02SubstanceAdministration" Then
                                            subadmin = CType(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02SubstanceAdministration)
                                            If Not IsNothing(subadmin) Then
                                                Dim templateactid As String = ""
                                                Dim templateactfound As Boolean = False
                                                templateactid = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsActivityNew")
                                                For TemlateCount = 0 To subadmin.templateId.Length - 1
                                                    If templateactid = subadmin.templateId(TemlateCount).root.ToString() Then
                                                        templateactfound = True

                                                    End If
                                                Next
                                                If templateactfound = True Then
                                                    If Not IsNothing(subadmin.effectiveTime) Then
                                                        If subadmin.effectiveTime.Length > 0 Then
                                                            Dim oCDAStartTime As IVL_TS = Nothing
                                                            Dim indexEffective As Int32
                                                            For indexEffective = 0 To subadmin.effectiveTime.Length - 1

                                                                ''IVL_TS = EffectiveDate code for Start and End DAte
                                                                If IsNothing(oCDAStartTime) Then
                                                                    oCDAStartTime = TryCast(subadmin.effectiveTime(indexEffective), IVL_TS)
                                                                End If

                                                            Next
                                                            If Not IsNothing(oCDAStartTime) Then
                                                                Dim indexStartTime As Int32
                                                                For indexStartTime = 0 To oCDAStartTime.Items.Length - 1
                                                                    Try
                                                                        If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.low Then
                                                                            If CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value <> "" Then
                                                                                immunization.ImmunizationDate = gloDateMaster.gloDate.QRDADateAsDateTime(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
                                                                            End If

                                                                        End If


                                                                    Catch ex As Exception
                                                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                                                        ex = Nothing
                                                                    End Try
                                                                Next
                                                            End If
                                                        End If
                                                    End If
                                                    If Not IsNothing(subadmin) Then
                                                        If Not IsNothing(subadmin.doseQuantity) Then
                                                            If subadmin.doseQuantity.value <> "" Then
                                                                immunization.AmountGiven = Convert.ToDecimal(subadmin.doseQuantity.value)
                                                            End If
                                                            If subadmin.doseQuantity.unit <> "" Then
                                                                immunization.Units = Convert.ToString(subadmin.doseQuantity.unit)
                                                            End If
                                                        End If
                                                    End If



                                                    If Not IsNothing(subadmin.consumable) Then
                                                        manufactureproduct = CType(subadmin.consumable.manufacturedProduct, POCD_MT000040UV02ManufacturedProduct)
                                                        If Not IsNothing(manufactureproduct) Then
                                                            Dim templateinfofound As Boolean = False
                                                            Dim templateinfo As String = ""
                                                            templateinfo = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsInfoNew")
                                                            For TemlateCount = 0 To manufactureproduct.templateId.Length - 1
                                                                If templateinfo = manufactureproduct.templateId(TemlateCount).root.ToString() Then
                                                                    templateinfofound = True

                                                                End If
                                                            Next
                                                            If templateinfofound = True Then
                                                                manufacturedmaterial = CType(manufactureproduct.Item, POCD_MT000040UV02Material)
                                                                If Not IsNothing(manufacturedmaterial) Then
                                                                    If (manufacturedmaterial.code).codeSystem = CodeSystem.RxNorm Then
                                                                        Return immunizationcol
                                                                        Exit Function
                                                                    End If
                                                                    If Convert.ToString(CType(manufacturedmaterial.code, CD).code) = "" AndAlso Convert.ToString(CType(manufacturedmaterial.code, CD).codeSystem) = "" AndAlso Convert.ToString(CType(manufacturedmaterial.code, CD).nullFlavor) = "NA" Then
                                                                        If Not IsNothing(CType(manufacturedmaterial.code, CD).valueSet) Then
                                                                            immunization.ValuesetOID = Convert.ToString(CType(manufacturedmaterial.code, CD).valueSet)
                                                                            immunization.Valueset = GetReasonDescriptionfromValueset(Convert.ToString(CType(manufacturedmaterial.code, CD).valueSet))
                                                                        End If
                                                                    Else
                                                                        immunization.VaccineCode = Convert.ToString(CType(manufacturedmaterial.code, CD).code)
                                                                        immunization.LotNumber = "12345"
                                                                        Dim dt As DataTable = getimmunization(immunization.VaccineCode)
                                                                        If dt.Rows.Count > 0 Then
                                                                            immunization.ImmunizationTrade = Convert.ToString(dt.Rows(0)("CdcProductName"))
                                                                            immunization.VaccineName = Convert.ToString(dt.Rows(0)("ShortDescription"))
                                                                        End If
                                                                        If Not IsNothing(dt) Then
                                                                            dt.Dispose()
                                                                            dt = Nothing
                                                                        End If
                                                                    End If

                                                                End If
                                                            End If

                                                        End If


                                                    End If

                                                End If

                                            End If

                                        ElseIf Convert.ToString(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item) = "POCD_MT000040UV02Observation" Then
                                            Dim reason As POCD_MT000040UV02Observation
                                            reason = CType(CType(act.entryRelationship.GetValue(i), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02Observation)
                                            If Not IsNothing(reason) Then

                                                If reason.code.code = "77301-0" Then
                                                    If Not IsNothing(CType(reason, POCD_MT000040UV02Observation).value) Then
                                                        If CType(CType(reason, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                            immunization.ReasonConceptID = CType(CType(reason, POCD_MT000040UV02Observation).value(0), CD).code
                                                            immunization.Reason = GetReasonDescription(immunization.ReasonConceptID)

                                                            immunization.admin_report_refused = 2

                                                        End If


                                                    End If


                                                End If
                                                ''

                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If

                    End If
                    immunizationcol.Add(immunization)
                    immunization.Dispose()
                ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02SubstanceAdministration" Then
                    Dim templateCount As Integer = 0
                    subadmin = CType(entry.Item, POCD_MT000040UV02SubstanceAdministration)
                    If Not IsNothing(subadmin) Then
                        Dim templateactid As String = ""
                        Dim templateactfound As Boolean = False
                        templateactid = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsAdministeredNew")
                        For templateCount = 0 To subadmin.templateId.Length - 1
                            If templateactid = subadmin.templateId(templateCount).root.ToString() Then
                                templateactfound = True

                            End If
                        Next
                        If templateactfound = True Then
                            If Not IsNothing(subadmin.effectiveTime) Then
                                If subadmin.effectiveTime.Length > 0 Then
                                    Dim oCDAStartTime As SXCM_TS = Nothing
                                    Dim indexEffective As Int32
                                    For indexEffective = 0 To subadmin.effectiveTime.Length - 1

                                        ''IVL_TS = EffectiveDate code for Start and End DAte
                                        If IsNothing(oCDAStartTime) Then
                                            oCDAStartTime = TryCast(subadmin.effectiveTime(indexEffective), SXCM_TS)
                                        End If

                                    Next
                                    If Not IsNothing(oCDAStartTime) Then

                                        If oCDAStartTime.value <> "" Then
                                            immunization.ImmunizationDate = gloDateMaster.gloDate.QRDADateAsDateTime(oCDAStartTime.value)
                                        End If

                                        'Dim indexStartTime As Int32

                                        'For indexStartTime = 0 To oCDAStartTime.Items.Length - 1
                                        '    Try
                                        '        If oCDAStartTime.ItemsElementName(indexStartTime) = ItemsChoiceType2.low Then
                                        '            If CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value <> "" Then
                                        '                Immunization.ImmunizationDate = gloDateMaster.gloDate.QRDADateAsDateTime(CType(oCDAStartTime.Items(indexStartTime), IVXB_TS).value)
                                        '            End If

                                        '        End If


                                        '    Catch ex As Exception
                                        '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                        '        ex = Nothing
                                        '    End Try
                                        'Next
                                    End If
                                End If
                            End If
                            If Not IsNothing(subadmin) Then
                                If Not IsNothing(subadmin.doseQuantity) Then
                                    If subadmin.doseQuantity.value <> "" Then
                                        Immunization.AmountGiven = Convert.ToDecimal(subadmin.doseQuantity.value)
                                    End If
                                    If subadmin.doseQuantity.unit <> "" Then
                                        Immunization.Units = Convert.ToString(subadmin.doseQuantity.unit)
                                    End If
                                End If
                            End If



                            If Not IsNothing(subadmin.consumable) Then
                                templateCount = 0
                                manufactureproduct = CType(subadmin.consumable.manufacturedProduct, POCD_MT000040UV02ManufacturedProduct)
                                If Not IsNothing(manufactureproduct) Then
                                    Dim templateinfofound As Boolean = False
                                    Dim templateinfo As String = ""
                                    templateinfo = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsInfoNew")
                                    For templateCount = 0 To manufactureproduct.templateId.Length - 1
                                        If templateinfo = manufactureproduct.templateId(templateCount).root.ToString() Then
                                            templateinfofound = True

                                        End If
                                    Next
                                    If templateinfofound = True Then
                                        manufacturedmaterial = CType(manufactureproduct.Item, POCD_MT000040UV02Material)
                                        If Not IsNothing(manufacturedmaterial) Then
                                            If (manufacturedmaterial.code).codeSystem = CodeSystem.RxNorm Then
                                                Return immunizationcol
                                                Exit Function
                                            End If
                                            If Convert.ToString(CType(manufacturedmaterial.code, CD).code) = "" AndAlso Convert.ToString(CType(manufacturedmaterial.code, CD).codeSystem) = "" AndAlso Convert.ToString(CType(manufacturedmaterial.code, CD).nullFlavor) = "NA" Then
                                                If Not IsNothing(CType(manufacturedmaterial.code, CD).valueSet) Then
                                                    immunization.ValuesetOID = Convert.ToString(CType(manufacturedmaterial.code, CD).valueSet)
                                                    immunization.Valueset = GetReasonDescriptionfromValueset(Convert.ToString(CType(manufacturedmaterial.code, CD).valueSet))
                                                End If
                                            Else
                                                immunization.VaccineCode = Convert.ToString(CType(manufacturedmaterial.code, CD).code)
                                                immunization.LotNumber = "12345"
                                                Dim dt As DataTable = getimmunization(immunization.VaccineCode)
                                                If dt.Rows.Count > 0 Then
                                                    immunization.ImmunizationTrade = Convert.ToString(dt.Rows(0)("CdcProductName"))
                                                    immunization.VaccineName = Convert.ToString(dt.Rows(0)("ShortDescription"))
                                                End If
                                                If Not IsNothing(dt) Then
                                                    dt.Dispose()
                                                    dt = Nothing
                                                End If
                                            End If

                                        End If
                                    End If

                                End If


                            End If

                        End If
                        If Not IsNothing(subadmin.entryRelationship) AndAlso (subadmin.entryRelationship.Length > 0) Then
                            For index As Integer = 0 To subadmin.entryRelationship.Length - 1
                                If Convert.ToString(CType(subadmin.entryRelationship.GetValue(index), POCD_MT000040UV02EntryRelationship).Item) = "POCD_MT000040UV02Observation" Then
                                    Dim reason As POCD_MT000040UV02Observation
                                    reason = CType(CType(subadmin.entryRelationship.GetValue(index), POCD_MT000040UV02EntryRelationship).Item, POCD_MT000040UV02Observation)
                                    If Not IsNothing(reason) Then
                                        If reason.code.code = "77301-0" Then
                                            If Not IsNothing(CType(reason, POCD_MT000040UV02Observation).value) Then
                                                If CType(CType(reason, POCD_MT000040UV02Observation).value(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                                    immunization.ReasonConceptID = CType(CType(reason, POCD_MT000040UV02Observation).value(0), CD).code
                                                    immunization.Reason = GetReasonDescription(immunization.ReasonConceptID)
                                                    immunization.admin_report_refused = 2
                                                End If
                                            End If
                                        End If
                                        ''
                                    End If
                                End If
                            Next
                        End If
                    End If
                    immunizationcol.Add(immunization)
                    immunization.Dispose()
                End If

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            immunizationcol = Nothing
            Return Nothing
        Finally
            act = Nothing
            subadmin = Nothing
            manufactureproduct = Nothing
            manufacturedmaterial = Nothing
        End Try
        Return immunizationcol
    End Function


    Private Function getmedicationallergyorintelorence(ByVal entry As POCD_MT000040UV02Entry, ByRef immunizationcol As ImmunizationCol, ByVal isallergy As Boolean) As ImmunizationCol
        'Dim immunizationcol As New ImmunizationCol
        Dim obs As POCD_MT000040UV02Observation = Nothing
        Dim subadmin As POCD_MT000040UV02SubstanceAdministration = Nothing
        Dim manufactureproduct As POCD_MT000040UV02ManufacturedProduct = Nothing
        Dim manufacturedmaterial As POCD_MT000040UV02Material = Nothing
        Try

            'Dim TemplateIDEntry As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsAdministered")
            'If Not IsNothing(entryarray) Then
            '    If entryarray.Length > 0 Then
            '        Dim EntryCount As Integer = 0
            '        For EntryCount = 0 To entryarray.Length - 1
            'Dim entry As POCD_MT000040UV02Entry = Nothing
            'entry = entryarray(EntryCount)
            If Not IsNothing(entry) Then
                If Convert.ToString(entry.Item) = "POCD_MT000040UV02Observation" Then
                    obs = CType(entry.Item, POCD_MT000040UV02Observation)

                    Dim immunization As New Immunization
                    If Not IsNothing(obs.effectiveTime) Then
                        If obs.effectiveTime.Items.Length > 0 Then
                            If Convert.ToString(CType(obs.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                immunization.ImmunizationDate = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(obs.effectiveTime.Items(0), IVXB_TS).value)))
                            End If
                        End If
                    End If
                    If IsNothing(immunizationcol) Then
                        immunizationcol = New ImmunizationCol
                    End If

                    ' immunizationcol = New ImmunizationCol
                    Dim obsentity As POCD_MT000040UV02PlayingEntity
                    If Not IsNothing(obs.participant) Then
                        If obs.participant.Length > 0 Then
                            If Not IsNothing(obs.participant(0)) Then
                                If Convert.ToString(obs.participant(0).typeCode) = ParticipationType.CSM Then
                                    If Not IsNothing(obs.participant(0).participantRole) Then
                                        If Not IsNothing(obs.participant(0).participantRole.Item) Then
                                            obsentity = obs.participant(0).participantRole.Item
                                            immunization.VaccineCode = Convert.ToString(CType(obsentity.code, CD).code)
                                            immunization.LotNumber = "12345"
                                            Dim dt As DataTable = getimmunization(immunization.VaccineCode)
                                            If dt.Rows.Count > 0 Then
                                                immunization.ImmunizationTrade = Convert.ToString(dt.Rows(0)("CdcProductName"))
                                                immunization.VaccineName = Convert.ToString(dt.Rows(0)("ShortDescription"))
                                            End If
                                            If Not IsNothing(dt) Then
                                                dt.Dispose()
                                                dt = Nothing
                                            End If
                                            ' If Not IsNothing(obs.value(0)) Then
                                            ' If DirectCast(obs.value(0),CD).code.ToString() Then
                                            If isallergy Then
                                                'immunization.admin_report_refused = "2"

                                                'immunization.Reason = "unKnown"
                                                'immunization.ReasonConceptID = "UNK"
                                                immunization.Patienthasreaction = True
                                            Else
                                                immunization.admin_report_refused = "2"

                                                immunization.Reason = "unKnown"
                                                immunization.ReasonConceptID = "UNK"

                                            End If




                                            'End If
                                            'End If

                                            immunizationcol.Add(immunization)
                                            immunization.Dispose()
                                        End If
                                    End If
                                End If
                            End If

                        End If


                    End If

                End If

            End If


            'End If
            '        Next
            '    End If
            'End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            immunizationcol = Nothing
            Return Nothing
        Finally

            subadmin = Nothing
            manufactureproduct = Nothing
            manufacturedmaterial = Nothing
        End Try
        Return immunizationcol
    End Function
    ''Added by Mayuri
    Private Function getpatientproblems_new(ByVal entry As POCD_MT000040UV02Entry, ByRef oProblemList As ProblemsCol) As ProblemsCol
        'Dim immunizationcol As New ImmunizationCol
        Dim act As POCD_MT000040UV02Act = Nothing
        Dim subadmin As POCD_MT000040UV02SubstanceAdministration = Nothing
        Dim manufactureproduct As POCD_MT000040UV02ManufacturedProduct = Nothing
        Dim manufacturedmaterial As POCD_MT000040UV02Material = Nothing
        Try

            'Dim TemplateIDEntry As String = ""
            'TemplateIDEntry = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsAdministered")
            'If Not IsNothing(entryarray) Then
            '    If entryarray.Length > 0 Then
            '        Dim EntryCount As Integer = 0
            '        For EntryCount = 0 To entryarray.Length - 1
            'Dim entry As POCD_MT000040UV02Entry = Nothing
            'entry = entryarray(EntryCount)
            If Not IsNothing(entry) Then
                If Convert.ToString(entry.Item) = "POCD_MT000040UV02Act" Then
                    act = CType(entry.Item, POCD_MT000040UV02Act)
                    Dim TemlateCount As Integer = 0
                    'Dim entryfound As Boolean = False
                    'For TemlateCount = 0 To act.templateId.Length - 1
                    '    If TemplateIDEntry = act.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True

                    '    End If
                    'Next

                    If Not IsNothing(act) Then
                        Dim actentry As POCD_MT000040UV02EntryRelationship = Nothing
                        actentry = act.entryRelationship(0)
                        ''  actentry = CType(actentry, POCD_MT000040UV02EntryRelationship)
                        Dim Diagnosisobservation As POCD_MT000040UV02Observation = Nothing
                        If Convert.ToString(actentry.Item) = "POCD_MT000040UV02Observation" Then
                            Diagnosisobservation = CType(actentry.Item, POCD_MT000040UV02Observation)
                            ' Dim TemlateCount As Integer = 0

                            'For TemlateCount = 0 To Diagnosisobservation.templateId.Length - 1
                            '    If TemplateIDEntry = Diagnosisobservation.templateId(TemlateCount).root.ToString() Then
                            '        entryfound = True

                            '        'Dim valuecount As Integer = 0
                            '        'For valuecount = 0 To oinsuranceobservation.value.Length-1


                            '        'Next



                            '    End If
                            'Next
                            'If entryfound = True Then
                            Dim oProblem As Problems = Nothing
                            oProblem = New Problems()
                            Dim ob As ANY()
                            ob = CType(Diagnosisobservation.value, ANY())

                            If Not IsNothing(ob) Then
                                If ob.Length > 0 Then

                                    Dim dticd As DataTable = New DataTable()
                                    If Not IsNothing(CType(ob.GetValue(0), CD).codeSystem) Then

                                        'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then
                                        If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.SNOMED_CT Then
                                            If Convert.ToString(CType(ob.GetValue(0), CD).code) <> "" Then
                                                oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                            End If

                                            'dtsnomed = GetSnomed(oProblem.ConceptID)
                                        ElseIf CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD10 Then
                                            If Convert.ToString(CType(ob.GetValue(0), CD).code) <> "" Then
                                                'If bCompareQRDA = True Then
                                                oProblem.ICD10Code = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                                'Else
                                                '  oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                                '  End If

                                                oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).displayName)
                                                oProblem.ICDRevision = 10
                                                dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                            End If
                                        End If

                                        If CType(ob.GetValue(0), CD).codeSystem = CodeSystem.ICD9 Then
                                            If Convert.ToString(CType(ob.GetValue(0), CD).code) <> "" Then
                                                oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).code)
                                                oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).displayName)
                                                oProblem.ICDRevision = 9
                                                dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                            End If
                                        End If

                                        'End If
                                        'Dim _qualifier As CR()
                                        'If Not IsNothing(CType(ob.GetValue(0), CD).qualifier) Then


                                        '    _qualifier = CType(ob.GetValue(0), CD).qualifier

                                        '    Dim patienthistorycol As New gloPatientHistoryCol
                                        '    Dim history As gloPatientHistory = Nothing
                                        '    If _qualifier.Length > 0 Then
                                        '        If Not IsNothing(_qualifier(0)) Then


                                        '            If Not IsNothing(_qualifier(0).name) Then


                                        '                If Convert.ToString(_qualifier(0).name.code) = "182353008" Then


                                        '                    '  Dim History As New gloPatientHistory()
                                        '                    Dim _qValue As CD
                                        '                    _qValue = CType(_qualifier(0).value, CD)
                                        '                    '  _qualifier(0).value = New CD()
                                        '                    oProblem.ReasonConceptID = Convert.ToString(_qValue.code)
                                        '                    oProblem.ReasonDesc = GetReasonDescription(_qValue.displayName)




                                        '                End If
                                        '            End If
                                        '        End If
                                        '    End If

                                        'End If
                                    End If

                                    ' ''Transaction element SNOMED or ICD9
                                    If Not IsNothing(CType(ob.GetValue(0), CD).translation) Then
                                        If CType(ob.GetValue(0), CD).translation.Length > 0 Then
                                            For index As Integer = 0 To CType(ob.GetValue(0), CD).translation.Length - 1
                                                'If oProblem.Condition = "" Then
                                                '    oProblem.Condition = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                                'End If
                                                'If gloLibCCDGeneral.gblnCCDAICD10Transition = True Then

                                                If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.SNOMED_CT) Then
                                                    If Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code) <> "" Then
                                                        oProblem.ConceptID = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                                    End If
                                                    'dtsnomed = GetSnomed(oProblem.ConceptID)
                                                ElseIf (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD10) Then
                                                    If Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code) <> "" Then
                                                        'If bCompareQRDA = True Then
                                                        oProblem.ICD10Code = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)

                                                        'Else
                                                        ' oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                                        'End If

                                                        oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                                        oProblem.ICDRevision = 10
                                                        dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                                    End If
                                                End If
                                                'Else
                                                If (Convert.ToString(CType(ob.GetValue(0), CD).translation(index).codeSystem) = CodeSystem.ICD9) Then
                                                    If Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code) <> "" Then
                                                        oProblem.ICD9Code = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).code)
                                                        oProblem.ICD9 = Convert.ToString(CType(ob.GetValue(0), CD).translation(index).displayName)
                                                        oProblem.ICDRevision = 9
                                                        dticd = GetICDdescription(oProblem.ICD9Code, oProblem.ICDRevision)
                                                    End If

                                                End If
                                                'End If

                                            Next
                                        End If
                                    End If
                                    Dim ed As ED
                                    ed = CType(ob.GetValue(0), CD).originalText
                                    If Not IsNothing(ed) Then
                                        Dim text As String = Convert.ToString(ed.Text(0))
                                        Dim arr As String()
                                        If text.Contains(":") Then
                                            arr = text.Split(":")
                                            If arr.Length > 0 Then
                                                text = arr(1)
                                            End If


                                        End If
                                        arr = Nothing

                                        If oProblem.ConceptID <> "" Then
                                            oProblem.Condition = text
                                        End If
                                    End If


                                    'If oProblem.ICD9 = "" Then
                                    '    oProblem.ICD9 = Text
                                    'End If
                                    'If dtsnomed.Rows.Count > 0 Then
                                    '    oProblem.Condition = dtsnomed.Rows(0)("TermDescription")
                                    'End If
                                    If dticd.Rows.Count > 0 Then
                                        oProblem.ICD9 = dticd.Rows(0)("sDescription")
                                        If oProblem.Condition = "" Then
                                            oProblem.Condition = dticd.Rows(0)("sDescription")
                                        End If
                                    End If

                                    If Not IsNothing(dticd) Then
                                        dticd.Dispose()
                                    End If

                                End If


                            End If
                            If Not IsNothing(Diagnosisobservation) Then
                                If Not IsNothing(Diagnosisobservation.targetSiteCode) Then

                                    If Not IsNothing(CType(Diagnosisobservation.targetSiteCode.GetValue(0), CD).code) Then
                                        oProblem.ReasonConceptID = CType(Diagnosisobservation.targetSiteCode.GetValue(0), CD).code
                                        oProblem.ReasonDesc = CType(Diagnosisobservation.targetSiteCode.GetValue(0), CD).displayName
                                    End If




                                End If
                            End If
                            Dim strStatus As String = ""

                            If Convert.ToString(act.statusCode.code).ToUpper() = "ACTIVE" Then
                                strStatus = "ACTIVE"
                            ElseIf Convert.ToString(act.statusCode.code).ToUpper() = "SUSPENDED" Then
                                strStatus = "INACTIVE"
                            ElseIf Convert.ToString(act.statusCode.code).ToUpper() = "COMPLETED" Then
                                strStatus = "RESOLVED"

                            End If

                            'Dim oCDAProblemObservation As POCD_MT000040UV02Observation = Nothing
                            ''Dim oCDAProblemEntryRelationship As POCD_MT000040UV02EntryRelationship = Nothing
                            'If Not IsNothing(Diagnosisobservation.entryRelationship) Then

                            '    If Diagnosisobservation.entryRelationship.Length > 0 Then
                            '        If Not IsNothing(Diagnosisobservation.entryRelationship(0).Item) Then
                            '            oCDAProblemObservation = TryCast(Diagnosisobservation.entryRelationship(0).Item, POCD_MT000040UV02Observation)
                            '            Dim _sStatusTemplateID As String = ""
                            '            Dim statusinactive As String = ""
                            '            Dim statusResolved As String = ""
                            '            ''_sStatusTemplateID = oCDADataBaseLayer.getCDATemplateID("Problems Status")
                            '            _sStatusTemplateID = oTemplateIDMaster.GetQRDAEntryID("DiagnosisActive")
                            '            statusinactive = oTemplateIDMaster.GetQRDAEntryID("DiagnosisInActive")
                            '            statusResolved = oTemplateIDMaster.GetQRDAEntryID("DiagnosisResolved")
                            '            If Not IsNothing(oCDAProblemObservation) Then
                            '                If Not IsNothing(oCDAProblemObservation.templateId) Then
                            '                    If oCDAProblemObservation.templateId.Length > 0 Then
                            '                        For templatecount As Integer = 0 To oCDAProblemObservation.templateId.Length - 1
                            '                            If Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = _sStatusTemplateID Then
                            '                                ''Problems Status Section
                            '                                strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0),CD).displayName)
                            '                            ElseIf Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = statusinactive Then
                            '                                strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0),CD).displayName)
                            '                            ElseIf Convert.ToString(oCDAProblemObservation.templateId(templatecount).root) = statusResolved Then
                            '                                strStatus = Convert.ToString(CType(oCDAProblemObservation.value(0),CD).displayName)
                            '                            End If
                            '                        Next


                            '                    End If
                            '                End If
                            '            End If
                            '        End If

                            '    End If

                            'End If

                            If Not IsNothing(ob) Then
                                If ob.Length > 0 Then
                                    If Not IsNothing(Diagnosisobservation.effectiveTime) Then

                                        If Diagnosisobservation.effectiveTime.Items.Length > 0 Then
                                            If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                                oProblem.DateOfService = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).value)))
                                            End If
                                        End If

                                        If strStatus.ToString.ToUpper() = "RESOLVED" Then
                                            If Diagnosisobservation.effectiveTime.Items.Length > 0 Then
                                                If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                                    strStatus = "RESOLVED"
                                                    oProblem.ResolvedDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).value)))
                                                Else
                                                    strStatus = "ACTIVE"
                                                End If
                                            End If
                                        End If

                                        If Diagnosisobservation.effectiveTime.Items.Length > 1 Then
                                            If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                                Try
                                                    oProblem.DischargeDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).value)))
                                                Catch ex As Exception
                                                End Try
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            If strStatus <> "" Then
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

                            oProblemList.Add(oProblem)
                            oProblem.Dispose()
                        End If
                    End If
                End If
            End If
            'End If

            '    Next
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            oProblemList = Nothing
            Return Nothing
        Finally
        End Try
        Return oProblemList
    End Function


    Private Function getimmunization(ByVal code As String) As DataTable
        Dim dtimmunization As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select CdcProductName,ShortDescription  from IMTradeName where CvxCode='" & code & "'"


            oDB.Retrive_Query(_sqlQuery, dtimmunization)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtimmunization
    End Function
    Private Function getimmcvx() As DataTable
        Dim dtimmunization As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Dim _vaccinecode As String = ""
        Try
            oDB.Connect(False)
            _sqlQuery = "select cvxcode,CdcProductName,ShortDescription  from IMTradeName"


            oDB.Retrive_Query(_sqlQuery, dtimmunization)
            'If Not IsNothing(dtimmunization) Then
            '    If dtimmunization.Rows.Count > 0 Then
            '        _vaccinecode = Convert.ToString(dtimmunization.Rows(0)(0))
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtimmunization
    End Function
    Private Sub getSection(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument, ByRef oReconcileList As ReconcileList)

        Dim TemplatevitalEntry As String = ""
        Dim TemplateriskEntry As String = ""
        Dim templatecharentry As String = ""
        Dim functionalEntry As String = ""
        Dim LabTestEntry As String = ""

        Dim LabTestperformedEntry As String = ""
        Dim FunctionalStatusPerformed As String = ""
        Dim DiagnosticResultEntry As String = ""
        Dim DiagnosticOrderEntry As String = ""
        Dim LabtestResult As String = ""
        Dim diagnosticperformed As String = ""
        Dim TemplateencounterEntry As String = ""
        Dim TemplateproEntry As String = ""
        Dim Templateprocperformednew As String = ""  ''2018 reporting valueset change for procedure
        Dim TemplateprointelorenceEntry As String = ""
        Dim templatephysicalperformed As String = ""
        Dim TemplateImmEntry As String = ""
        Dim TemplatemedallergyorintEntry As String = ""
        Dim TemplatemedintEntry As String = ""
        Dim templateinsurance As String = ""
        Dim TemplateprobEntry As String = ""
        'Dim TemplateInactiveprobEntry As String = ""
        ' Dim TemplateResolvedprobEntry As String = ""
        Dim TemplateInterventionEntry As String = ""
        Dim TemplateInterventionOrderEntry As String = ""
        Dim Templatecommpttoprovider As String = ""
        Dim entryid As String = ""
        Dim medicationactiveentry As String = "'"
        Dim templateProcorderEntry As String = ""
        Dim templateEncounterAct As String = ""
        Dim templateAssessment As String = ""
        'Dim medicationorderentry As String = "'"
        Dim entry1 As POCD_MT000040UV02Entry = Nothing
        Dim templateallergyIntolerance As String = ""

        Dim oCDAMedicationAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing

        Try
            oReconcileList.mPatient.PatientHistory = New gloPatientHistoryCol()
            oReconcileList.mPatient.PatientEncounters = New EncountersCol()
            oReconcileList.mPatient.PatientLabResult = New LabResultsCol()
            oReconcileList.mPatient.PatientVitals = New VitalsCol()
            oReconcileList.mPatient.PatientMedications = New MedicationsCol()
            oReconcileList.mPatient.PatientProblems = New ProblemsCol()
            oReconcileList.mPatient.PatientImmunizations = New ImmunizationCol()


            If Not IsNothing(oCCDSchema.component.Item) Then
                If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0 Then
                    Dim i As Integer = 0
                    Dim TemplateID As String = ""
                    TemplateID = oTemplateIDMaster.GetQRDASectionID("PatientData")
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
                                Dim entryarray As POCD_MT000040UV02Entry() = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry
                                Dim EntryCount As Integer = 0
                                TemplateprobEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosisAct")
                                ' TemplateInactiveprobEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosisInActive")
                                ' TemplateResolvedprobEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosisResolved")
                                templateallergyIntolerance = oTemplateIDMaster.GetQRDAEntryID("AllergyIntolerance")
                                TemplateInterventionEntry = oTemplateIDMaster.GetQRDAEntryID("Intervention")
                                TemplateInterventionOrderEntry = oTemplateIDMaster.GetQRDAEntryID("InterventionOrder")
                                templateinsurance = oTemplateIDMaster.GetQRDAEntryID("Insurance")
                                TemplateImmEntry = oTemplateIDMaster.GetQRDAEntryID("ImmunizationsAdministeredNew")
                                TemplatemedallergyorintEntry = oTemplateIDMaster.GetQRDAEntryID("Medication Allergy")
                                TemplatemedintEntry = oTemplateIDMaster.GetQRDAEntryID("Medication Intolerance")
                                TemplateproEntry = oTemplateIDMaster.GetQRDAEntryID("ProcedurePerformed")
                                Templateprocperformednew = oTemplateIDMaster.GetQRDAEntryID("ProcedurePerformedNew")
                                TemplateprointelorenceEntry = oTemplateIDMaster.GetQRDAEntryID("ProcedureIntolerence")
                                templatephysicalperformed = oTemplateIDMaster.GetQRDAEntryID("PhysicalExamPerformed")
                                TemplateencounterEntry = oTemplateIDMaster.GetQRDAEntryID("Encounter")
                                TemplatevitalEntry = oTemplateIDMaster.GetQRDAEntryID("Vitals")
                                functionalEntry = oTemplateIDMaster.GetQRDAEntryID("FunctionalStatus")
                                LabTestEntry = oTemplateIDMaster.GetQRDAEntryID("LaboratoryTestOrder")
                                LabTestperformedEntry = oTemplateIDMaster.GetQRDAEntryID("LaboratoryTestPerformed")
                                FunctionalStatusPerformed = oTemplateIDMaster.GetQRDAEntryID("FunctionalStatusPerformed")
                                LabtestResult = oTemplateIDMaster.GetQRDAEntryID("LabTestResults")
                                DiagnosticOrderEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosisOrdered")
                                DiagnosticResultEntry = oTemplateIDMaster.GetQRDAEntryID("DiagnosticResults")
                                diagnosticperformed = oTemplateIDMaster.GetQRDAEntryID("DiagnosticstudyPerformed")
                                TemplateriskEntry = oTemplateIDMaster.GetQRDAEntryID("RiskCategoryAssesment")
                                templateAssessment = oTemplateIDMaster.GetQRDAEntryID("Assessment")
                                templatecharentry = oTemplateIDMaster.GetQRDAEntryID("PatientChar")
                                Templatecommpttoprovider = oTemplateIDMaster.GetQRDAEntryID("commPttoProvider")
                                templateProcorderEntry = oTemplateIDMaster.GetQRDAEntryID("ProcedureOrder")
                                '   Templateprocperformednew = oTemplateIDMaster.GetQRDAEntryID("ProcedurePerformedNew")
                                templateEncounterAct = oTemplateIDMaster.GetQRDAEntryID("EncounterAct")
                                Dim EntryCount1 As Integer = 0
                                Dim entrymediorderfound As Boolean = False
                                Dim activeentryfound As Boolean = False
                                Dim orderentryfound As Boolean = False
                                entryid = oTemplateIDMaster.GetQRDAEntryID("Medication")
                                medicationactiveentry = oTemplateIDMaster.GetQRDAEntryID("MedicationActive")
                                '  medicationorderentry = oTemplateIDMaster.GetQRDAEntryID("MedicationOrder")
                                'For EntryCount1 = 0 To entryarray.Length - 1
                                '    entry1 = entryarray(EntryCount1)
                                '    If Not IsNothing(entry1) Then
                                '        If Convert.ToString(entry1.Item) = "POCD_MT000040UV02SubstanceAdministration" Then
                                '            oCDAMedicationAdmin = CType(entry1.Item, POCD_MT000040UV02SubstanceAdministration)
                                '            If Not IsNothing(oCDAMedicationAdmin) Then
                                '                For templatecount As Integer = 0 To oCDAMedicationAdmin.templateId.Length - 1
                                '                    If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                '                        entrymediorderfound = True
                                '                    ElseIf medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                '                        activeentryfound = True
                                '                        'ElseIf medicationorderentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                '                        '    orderentryfound = True
                                '                    End If
                                '                Next
                                '            End If
                                '        End If
                                '    End If
                                'Next
                                For EntryCount = 0 To entryarray.Length - 1
                                    Dim vitalfound As Boolean = False
                                    Dim entryresultfound As Boolean = False
                                    Dim entryorderfound As Boolean = False
                                    Dim riskfound As Boolean = False
                                    Dim encounterfound As Boolean = False
                                    Dim procedurefound As Boolean = False
                                    Dim procedureintelofound As Boolean = False
                                    Dim physicalexamfound As Boolean = False
                                    Dim immunifound As Boolean = False
                                    Dim insurancefound As Boolean = False
                                    Dim problemfound As Boolean = False
                                    Dim interventionfound As Boolean = False
                                    Dim interventionOrder As Boolean = False
                                    Dim commpttoprovider As Boolean = False
                                    Dim medicationfound As Boolean = False
                                    Dim entry As POCD_MT000040UV02Entry = Nothing
                                    Dim entrytestperformedfound As Boolean = False
                                    Dim entryfunctionalperformedfound As Boolean = False
                                    Dim medallergyfound As Boolean = False
                                    Dim medintelorencefound As Boolean = False
                                    Dim Procedureorderfound As Boolean = False
                                    Dim Procedureperformednew As Boolean = False
                                    Dim AllergyIntolerancefound As Boolean = False
                                    entry = entryarray(EntryCount)
                                    If Not IsNothing(entry) Then
                                        Dim observation As POCD_MT000040UV02Observation = Nothing
                                        If Convert.ToString(entry.Item) = "POCD_MT000040UV02Observation" Then
                                            observation = CType(entry.Item, POCD_MT000040UV02Observation)
                                            Dim TemlateCount As Integer = 0
                                            For TemlateCount = 0 To observation.templateId.Length - 1
                                                If TemplatevitalEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    vitalfound = True
                                                ElseIf TemplatemedallergyorintEntry = observation.templateId(TemlateCount).root.ToString() Then

                                                    medallergyfound = True
                                                ElseIf TemplatemedintEntry = observation.templateId(TemlateCount).root.ToString() Then

                                                    medintelorencefound = True
                                                ElseIf functionalEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryresultfound = True
                                                ElseIf LabTestEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryorderfound = True
                                                ElseIf LabTestperformedEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    entrytestperformedfound = True
                                                ElseIf FunctionalStatusPerformed = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryfunctionalperformedfound = True
                                                ElseIf DiagnosticResultEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryresultfound = True
                                                ElseIf DiagnosticOrderEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryorderfound = True
                                                ElseIf LabtestResult = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryresultfound = True
                                                ElseIf diagnosticperformed = observation.templateId(TemlateCount).root.ToString() Then
                                                    entryresultfound = True
                                                ElseIf TemplateriskEntry = observation.templateId(TemlateCount).root.ToString() OrElse templateAssessment = observation.templateId(TemlateCount).root.ToString() Then
                                                    riskfound = True
                                                ElseIf templatecharentry = observation.templateId(TemlateCount).root.ToString() Then
                                                    riskfound = True
                                                ElseIf templatephysicalperformed = observation.templateId(TemlateCount).root.ToString() Then
                                                    physicalexamfound = True
                                                ElseIf templateinsurance = observation.templateId(TemlateCount).root.ToString() Then
                                                    insurancefound = True
                                                    'ElseIf TemplateprobEntry = observation.templateId(TemlateCount).root.ToString() Or TemplateInactiveprobEntry = observation.templateId(TemlateCount).root.ToString() Or TemplateResolvedprobEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    '    problemfound = True
                                                ElseIf TemplateprointelorenceEntry = observation.templateId(TemlateCount).root.ToString() Then
                                                    procedureintelofound = True
                                                ElseIf templateallergyIntolerance = observation.templateId(TemlateCount).root.ToString() Then
                                                    AllergyIntolerancefound = True
                                                End If
                                            Next
                                            If procedureintelofound = True Then
                                                ' oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations)
                                                oReconcileList.mPatient.PatientImmunizations = getPatientProcedureIntelorence(entry, oReconcileList.mPatient.PatientImmunizations)
                                            End If
                                            If vitalfound = True Then
                                                'pass only observation and entry not entryaaray
                                                oReconcileList.mPatient.PatientVitals = getPatientVitals(entry, oReconcileList.mPatient.PatientVitals, oReconcileList.mPatient.PatientHistory, oReconcileList.mPatient.PatientLabResult)
                                            End If
                                            If medallergyfound = True Then
                                                'pass only observation and entry not entryaaray
                                                oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations, True)
                                            End If
                                            If medintelorencefound = True Then
                                                'pass only observation and entry not entryaaray
                                                oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations, False)
                                            End If
                                            If entryresultfound = True Or entryorderfound = True Or entrytestperformedfound = True Or entryfunctionalperformedfound Then
                                                oReconcileList.mPatient.PatientLabResult = getPatientLabresults(entry, entryresultfound, entryorderfound, entrytestperformedfound, entryfunctionalperformedfound, oReconcileList.mPatient.PatientLabResult, oReconcileList.mPatient.PatientProblems, oReconcileList.mPatient.PatientHistory)
                                            End If
                                            If physicalexamfound = True Then
                                                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
                                            End If
                                            If insurancefound = True Then
                                                oReconcileList.mPatient.PatientDemographics.Insurances = getPatientInsurances(entry)
                                            End If
                                            If problemfound = True Then
                                                oReconcileList.mPatient.PatientProblems = getPatientProblems(entry, oReconcileList.mPatient.PatientProblems)
                                            End If
                                            If riskfound = True Then
                                                getPatientRiskcategory_New(entry, oReconcileList.mPatient.PatientHistory, observation, oReconcileList.mPatient.PatientLabResult)
                                            End If
                                            If AllergyIntolerancefound = True Then
                                                getPatientAllergies(entry, oReconcileList.mPatient.PatientProblems)
                                            End If


                                            'ElseIf Convert.ToString(entry.Item) = "gloCCDSchema.POCD_MT000040Encounter" Then
                                            '    Dim encounter As POCD_MT000040Encounter
                                            '    encounter = CType(entry.Item, POCD_MT000040Encounter)
                                            '    Dim TemlateCount As Integer = 0
                                            '    For TemlateCount = 0 To encounter.templateId.Length - 1
                                            '        If TemplateencounterEntry = encounter.templateId(TemlateCount).root.ToString() Then
                                            '            encounterfound = True
                                            '        End If
                                            '    Next
                                            '    If encounterfound = True Then
                                            '        'If CheckOnlyoneSnomed(entry, encounter) Then
                                            '        '  oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, , True)
                                            '        'Else
                                            '        'oReconcileList.mPatient.PatientEncounters = getPatientEncounters(entry, encounter, oReconcileList.mPatient.PatientEncounters)
                                            '        ' End If
                                            '        ' Dim _isonlysnomed As Boolean = False










                                            '        'If _isonlysnomed = True Then
                                            '        If bCompareQRDA = False Then
                                            '            oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
                                            '        End If

                                            '        'End If
                                            '    End If
                                            '    encounter = Nothing
                                        ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02Procedure" Then
                                            Dim procedure As POCD_MT000040UV02Procedure
                                            procedure = CType(entry.Item, POCD_MT000040UV02Procedure)
                                            Dim TemlateCount As Integer = 0
                                            For TemlateCount = 0 To procedure.templateId.Length - 1
                                                If TemplateproEntry = procedure.templateId(TemlateCount).root.ToString() Then
                                                    procedurefound = True
                                                ElseIf templateProcorderEntry = procedure.templateId(TemlateCount).root.ToString() Then
                                                    Procedureorderfound = True
                                                ElseIf Templateprocperformednew = procedure.templateId(TemlateCount).root.ToString() Then
                                                    Procedureperformednew = True
                                                End If
                                                'If TemplateprointelorenceEntry = procedure.templateId(TemlateCount).root.ToString() Then
                                                '    procedureintelofound = True
                                                'End If
                                            Next
                                            procedure = Nothing
                                            If procedurefound = True OrElse Procedureorderfound = True OrElse Procedureperformednew = True Then
                                                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
                                            End If

                                            'Dim act As POCD_MT000040UV02Act = Nothing
                                        ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02Act" Then
                                            Dim act As POCD_MT000040UV02Act
                                            act = CType(entry.Item, POCD_MT000040UV02Act)
                                            Dim TemlateCount As Integer = 0

                                            For TemlateCount = 0 To act.templateId.Length - 1
                                                If templateEncounterAct = act.templateId(TemlateCount).root.ToString() Then
                                                    encounterfound = True
                                                End If

                                                If TemplateImmEntry = act.templateId(TemlateCount).root.ToString() Then
                                                    immunifound = True
                                                End If
                                                If TemplateprobEntry = act.templateId(TemlateCount).root.ToString() Then
                                                    problemfound = True
                                                End If
                                                If TemplateInterventionEntry = act.templateId(TemlateCount).root.ToString() Then
                                                    interventionfound = True
                                                End If
                                                If TemplateInterventionOrderEntry = act.templateId(TemlateCount).root.ToString() Then
                                                    interventionOrder = True
                                                End If
                                                If Templatecommpttoprovider = act.templateId(TemlateCount).root.ToString() Then
                                                    commpttoprovider = True
                                                End If
                                            Next
                                            'If problemfound = True Then
                                            '    oReconcileList.mPatient.PatientProblems = getPatientProblems(entry, oReconcileList.mPatient.PatientProblems)
                                            'End If
                                            If encounterfound = True Then
                                                ' If bCompareQRDA = False Then
                                                getPatientEncounters(entry, oReconcileList.mPatient.PatientEncounters, oReconcileList.mPatient.PatientHistory)
                                                'Else
                                                '    getPatientEncounters(entry, oReconcileList.mPatient.PatientEncounters)
                                                'End If
                                            End If
                                            If immunifound = True Then
                                                oReconcileList.mPatient.PatientImmunizations = getPatientImmunizations(entry, oReconcileList.mPatient.PatientImmunizations)
                                                'oReconcileList.mPatient.PatientHistory = getMedicationadministerednotperformed(entry, oReconcileList.mPatient.PatientHistory)
                                            End If
                                            If problemfound = True Then
                                                oReconcileList.mPatient.PatientProblems = getpatientproblems_new(entry, oReconcileList.mPatient.PatientProblems)
                                            End If
                                            If interventionfound = True Or interventionOrder = True Or commpttoprovider = True Then
                                                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
                                            End If
                                            'If interventionOrder = True Then
                                            '    oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
                                            'End If
                                            'If commpttoprovider = True Then
                                            '    oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
                                            'End If
                                            act = Nothing
                                        ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02SubstanceAdministration" Then
                                            'Dim TemlateCount As Integer = 0
                                            oCDAMedicationAdmin = CType(entry.Item, POCD_MT000040UV02SubstanceAdministration)
                                            For templatecount As Integer = 0 To oCDAMedicationAdmin.templateId.Length - 1
                                                ' If activeentryfound = True Then
                                                entrymediorderfound = False
                                                activeentryfound = False
                                                If medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                    activeentryfound = True
                                                    medicationfound = True
                                                End If

                                                'ElseIf entrymediorderfound = True Then
                                                If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                    entrymediorderfound = True
                                                    medicationfound = True
                                                End If

                                                If TemplateImmEntry = oCDAMedicationAdmin.templateId(templatecount).root.ToString() Then
                                                    immunifound = True
                                                End If

                                                'For TemlateCount = 0 To oCDAMedicationAdmin.templateId.Length - 1
                                                '    If TemplateImmEntry = oCDAMedicationAdmin.templateId(TemlateCount).root.ToString() Then
                                                '        immunifound = True
                                                '    End If
                                                'Next


                                                '  End If
                                                'If activeentryfound = True AndAlso entrymediorderfound = True Then
                                                '    If medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                '        medicationfound = True
                                                '    ElseIf entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                '        medicationfound = True
                                                '    End If
                                                'ElseIf entrymediorderfound = True Then
                                                '    If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                '        medicationfound = True
                                                '    End If
                                                'Else
                                                '    If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                '        medicationfound = True
                                                '    ElseIf medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
                                                '        medicationfound = True
                                                '    End If
                                                'End If
                                            Next
                                            If medicationfound = True Then
                                                oReconcileList.mPatient.PatientMedications = getPatientMedications(entry, oReconcileList.mPatient.PatientMedications, entrymediorderfound)

                                            End If
                                            If immunifound = True Then
                                                oReconcileList.mPatient.PatientImmunizations = getPatientImmunizations(entry, oReconcileList.mPatient.PatientImmunizations)
                                                'oReconcileList.mPatient.PatientHistory = getMedicationadministerednotperformed(entry, oReconcileList.mPatient.PatientHistory)
                                            End If

                                        End If
                                    End If

                                Next

                            End If

                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            TemplatevitalEntry = Nothing
            TemplateriskEntry = Nothing
            templatecharentry = Nothing
            functionalEntry = Nothing
            LabTestEntry = Nothing
            DiagnosticResultEntry = Nothing
            DiagnosticOrderEntry = Nothing
            LabtestResult = Nothing
            diagnosticperformed = Nothing
            TemplateencounterEntry = Nothing
            TemplateproEntry = Nothing
            templatephysicalperformed = Nothing
            TemplateImmEntry = Nothing
            templateinsurance = Nothing
            TemplateprobEntry = Nothing

            entryid = Nothing
            medicationactiveentry = Nothing

            entry1 = Nothing
            oCDAMedicationAdmin = Nothing
        End Try
    End Sub


    'Public Function ExtractQRDAToCompare(ByVal strCCDFilePath As String, ByVal strDestFilePath As String)
    '    Dim oReconcileList As ReconcileList = New ReconcileList
    '    Dim oCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing
    '    Dim DestoCCDSchema As POCD_MT000040UV02ClinicalDocument = Nothing

    '    gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = True
    '    oTemplateIDMaster = New TemplateIDMaster(1)
    '    Try
    '        oCCDSchema = gloSerialization.GetClinicalDocument(strCCDFilePath)
    '        DestoCCDSchema = gloSerialization.GetClinicalDocument(strDestFilePath)
    '        If Not IsNothing(oCCDSchema) Then
    '            CompareQRDASection(oCCDSchema, DestoCCDSchema)
    '        End If


    '    Catch ex As Exception
    '        oReconcileList = Nothing
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
    '        ex = Nothing
    '    Finally
    '        oCCDSchema = Nothing
    '        If Not IsNothing(oTemplateIDMaster) Then
    '            oTemplateIDMaster.Dispose()
    '        End If
    '    End Try

    '    Return oReconcileList

    'End Function
    'Private Sub CompareQRDASection(ByRef oCCDSchema As POCD_MT000040UV02ClinicalDocument, ByRef DestOCCDSchema As POCD_MT000040UV02ClinicalDocument)

    '    Dim entry1 As POCD_MT000040UV02Entry = Nothing
    '    Dim oCDAMedicationAdmin As POCD_MT000040UV02SubstanceAdministration = Nothing

    '    Try

    '        If Not IsNothing(oCCDSchema.component.Item) AndAlso Not IsNothing(DestOCCDSchema.component.Item) Then
    '            If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0 And CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length > 0 Then
    '                Dim i As Integer = 0
    '                Dim TemplateID As String = ""
    '                TemplateID = oTemplateIDMaster.GetQRDASectionID("PatientData")
    '                Dim SectionFound As Boolean = False
    '                For i = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
    '                    If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
    '                        If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then
    '                            Dim TemlateCount As Integer = 0
    '                            SectionFound = False
    '                            For TemlateCount = 0 To CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
    '                                If TemplateID = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
    '                                    SectionFound = True
    '                                    Exit For
    '                                End If
    '                            Next
    '                            If SectionFound = False Then
    '                                Continue For
    '                            Else
    '                                Exit For
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '                Dim DestSectionFound As Boolean = False
    '                For i = 0 To CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component.Length - 1
    '                    If Not IsNothing(CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId) Then
    '                        If CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length > 0 Then
    '                            Dim TemlateCount As Integer = 0
    '                            DestSectionFound = False
    '                            For TemlateCount = 0 To CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId.Length - 1
    '                                If TemplateID = CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.templateId(TemlateCount).root.ToString() Then
    '                                    DestSectionFound = True
    '                                    Exit For
    '                                End If
    '                            Next
    '                            If DestSectionFound = False Then
    '                                Continue For
    '                            Else
    '                                Exit For
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '                If SectionFound = True AndAlso DestSectionFound = True Then
    '                    If Not IsNothing(CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) AndAlso Not IsNothing(CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry) Then
    '                        If CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 AndAlso CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry.Length > 0 Then
    '                            Dim entryarray As POCD_MT000040UV02Entry() = CType(oCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry
    '                            Dim Destentryarray As POCD_MT000040UV02Entry() = CType(DestOCCDSchema.component.Item, POCD_MT000040UV02StructuredBody).component(i).section.entry
    '                            Dim EntryCount As Integer = 0

    '                            Dim EntryCount1 As Integer = 0
    '                            Dim strmsg As String = ""
    '                            Dim transalationcode As String = ""

    '                            Dim EncounterCode As String = ""
    '                            Dim StartDate As String = ""
    '                            Dim Enddate As String = ""
    '                            For Each entry As POCD_MT000040UV02Entry In entryarray
    '                                Dim encounterfound As Boolean = False
    '                                If Convert.ToString(entry.Item) = "gloCCDSchema.POCD_MT000040UV02Encounter" Then
    '                                    For Each destentry As POCD_MT000040UV02Entry In Destentryarray
    '                                        If Convert.ToString(destentry.Item) = "gloCCDSchema.POCD_MT000040UV02Encounter" Then
    '                                            EncounterCode = CType(entry.Item, POCD_MT000040UV02Encounter).code.code
    '                                            If CType(entry.Item, POCD_MT000040UV02Encounter).code.code = CType(destentry.Item, POCD_MT000040UV02Encounter).code.code Then
    '                                                'match transalation of source in destination
    '                                                If Not IsNothing(CType(entry.Item, POCD_MT000040UV02Encounter).code.translation) AndAlso Not IsNothing(CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation) Then
    '                                                    If CType(entry.Item, POCD_MT000040UV02Encounter).code.translation.Length > 0 AndAlso CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation.Length > 0 Then
    '                                                        For index As Integer = 0 To CType(entry.Item, POCD_MT000040UV02Encounter).code.translation.Length - 1
    '                                                            For index1 As Integer = 0 To CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation.Length - 1
    '                                                                transalationcode = transalationcode + CType(entry.Item, POCD_MT000040UV02Encounter).code.translation(index).code
    '                                                                If CType(entry.Item, POCD_MT000040UV02Encounter).code.translation(index).code = CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation(index1).code Then
    '                                                                    Exit For
    '                                                                End If
    '                                                            Next

    '                                                        Next
    '                                                    End If
    '                                                End If

    '                                                'effectivetime

    '                                                If CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items.Length > 0 AndAlso CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items.Length > 0 Then

    '                                                    If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
    '                                                        StartDate = Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).value)
    '                                                        If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).value) = Convert.ToString(CType(CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).value) Then

    '                                                            If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
    '                                                                Enddate = Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).value)
    '                                                                If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).value) = Convert.ToString(CType(CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).value) Then
    '                                                                    encounterfound = True

    '                                                                    Exit For

    '                                                                End If
    '                                                            End If

    '                                                        End If

    '                                                    End If

    '                                                End If

    '                                            Else
    '                                                ''translation
    '                                                Dim translationfound As Boolean = False
    '                                                If Not IsNothing(CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation) Then
    '                                                    If CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation.Length > 0 Then
    '                                                        For index As Integer = 0 To CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation.Length - 1
    '                                                            If CType(entry.Item, POCD_MT000040UV02Encounter).code.code = CType(destentry.Item, POCD_MT000040UV02Encounter).code.translation(index).code Then
    '                                                                'Check vice versa fro code and translation 
    '                                                                translationfound = True

    '                                                            End If
    '                                                        Next
    '                                                        If translationfound = True Then
    '                                                            For index As Integer = 0 To CType(entry.Item, POCD_MT000040UV02Encounter).code.translation.Length - 1
    '                                                                If CType(destentry.Item, POCD_MT000040UV02Encounter).code.code = CType(entry.Item, POCD_MT000040UV02Encounter).code.translation(index).code Then
    '                                                                    'Check vice versa fro code and translation 
    '                                                                    Exit For
    '                                                                End If
    '                                                            Next

    '                                                            'effectivetime
    '                                                            If CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items.Length > 0 AndAlso CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items.Length > 0 Then

    '                                                                If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
    '                                                                    If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).value) = Convert.ToString(CType(CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(0), IVXB_TS).value) Then

    '                                                                        If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
    '                                                                            If Convert.ToString(CType(CType(entry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).value) = Convert.ToString(CType(CType(destentry.Item, POCD_MT000040UV02Encounter).effectiveTime.Items(1), IVXB_TS).value) Then
    '                                                                                encounterfound = True
    '                                                                                Exit For
    '                                                                            End If
    '                                                                        End If

    '                                                                    End If

    '                                                                End If

    '                                                            End If
    '                                                        End If


    '                                                    End If
    '                                                End If


    '                                            End If
    '                                            '
    '                                        End If
    '                                    Next
    '                                    If encounterfound = False Then
    '                                        'gloAuditTrail.gloAuditTrail.ExceptionLog("Encounter Missing", "", True)
    '                                        strmsg = strmsg & vbNewLine & "Encounter Code  '" & EncounterCode & "' Encounter Translation Code  '" & transalationcode & "' Date Of Service  '" & StartDate + "' Discharge Date  '" & Enddate + "' "

    '                                        'MessageBox.Show("Encounter Missing")
    '                                    End If

    '                                End If
    '                            Next
    '                            If strmsg <> "" Then
    '                                MessageBox.Show(strmsg, "Encounter is missing")
    '                            Else
    '                                MessageBox.Show("All Encounter are Present")
    '                            End If



    '                            'For EntryCount = 0 To entryarray.Length - 1
    '                            '    Dim vitalfound As Boolean = False
    '                            '    Dim entryresultfound As Boolean = False
    '                            '    Dim entryorderfound As Boolean = False
    '                            '    Dim riskfound As Boolean = False
    '                            '    Dim encounterfound As Boolean = False
    '                            '    Dim procedurefound As Boolean = False
    '                            '    Dim procedureintelofound As Boolean = False
    '                            '    Dim physicalexamfound As Boolean = False
    '                            '    Dim immunifound As Boolean = False
    '                            '    Dim insurancefound As Boolean = False
    '                            '    Dim problemfound As Boolean = False
    '                            '    Dim interventionfound As Boolean = False
    '                            '    Dim interventionOrder As Boolean = False
    '                            '    Dim commpttoprovider As Boolean = False
    '                            '    Dim medicationfound As Boolean = False
    '                            '    Dim entry As POCD_MT000040UV02Entry = Nothing
    '                            '    Dim entrytestperformedfound As Boolean = False
    '                            '    Dim entryfunctionalperformedfound As Boolean = False
    '                            '    Dim medallergyfound As Boolean = False
    '                            '    Dim medintelorencefound As Boolean = False
    '                            '    Dim Procedureorderfound As Boolean = False
    '                            '    entry = entryarray(EntryCount)

    '                            '    If Not IsNothing(entry) Then
    '                            '        Dim observation As POCD_MT000040UV02Observation = Nothing
    '                            '        If Convert.ToString(entry.Item) = "POCD_MT000040UV02Observation" Then
    '                            '            observation = CType(entry.Item, POCD_MT000040UV02Observation)
    '                            '            Dim TemlateCount As Integer = 0
    '                            '            For TemlateCount = 0 To observation.templateId.Length - 1
    '                            '                If TemplatevitalEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    vitalfound = True
    '                            '                ElseIf TemplatemedallergyorintEntry = observation.templateId(TemlateCount).root.ToString() Then

    '                            '                    medallergyfound = True
    '                            '                ElseIf TemplatemedintEntry = observation.templateId(TemlateCount).root.ToString() Then

    '                            '                    medintelorencefound = True
    '                            '                ElseIf functionalEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryresultfound = True
    '                            '                ElseIf LabTestEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryorderfound = True
    '                            '                ElseIf LabTestperformedEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entrytestperformedfound = True
    '                            '                ElseIf FunctionalStatusPerformed = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryfunctionalperformedfound = True
    '                            '                ElseIf DiagnosticResultEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryresultfound = True
    '                            '                ElseIf DiagnosticOrderEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryorderfound = True
    '                            '                ElseIf LabtestResult = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryresultfound = True
    '                            '                ElseIf diagnosticperformed = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    entryresultfound = True
    '                            '                ElseIf TemplateriskEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    riskfound = True
    '                            '                ElseIf templatecharentry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    riskfound = True
    '                            '                ElseIf templatephysicalperformed = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    physicalexamfound = True
    '                            '                ElseIf templateinsurance = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    insurancefound = True
    '                            '                    'ElseIf TemplateprobEntry = observation.templateId(TemlateCount).root.ToString() Or TemplateInactiveprobEntry = observation.templateId(TemlateCount).root.ToString() Or TemplateResolvedprobEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    '    problemfound = True
    '                            '                ElseIf TemplateprointelorenceEntry = observation.templateId(TemlateCount).root.ToString() Then
    '                            '                    procedureintelofound = True
    '                            '                End If

    '                            '            Next
    '                            '            If procedureintelofound = True Then
    '                            '                ' oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations)
    '                            '                oReconcileList.mPatient.PatientImmunizations = getPatientProcedureIntelorence(entry, oReconcileList.mPatient.PatientImmunizations)
    '                            '            End If
    '                            '            If vitalfound = True Then
    '                            '                'pass only observation and entry not entryaaray
    '                            '                oReconcileList.mPatient.PatientVitals = getPatientVitals(entry, oReconcileList.mPatient.PatientVitals, oReconcileList.mPatient.PatientHistory, oReconcileList.mPatient.PatientLabResult)
    '                            '            End If
    '                            '            If medallergyfound = True Then
    '                            '                'pass only observation and entry not entryaaray
    '                            '                oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations, True)
    '                            '            End If
    '                            '            If medintelorencefound = True Then
    '                            '                'pass only observation and entry not entryaaray
    '                            '                oReconcileList.mPatient.PatientImmunizations = getmedicationallergyorintelorence(entry, oReconcileList.mPatient.PatientImmunizations, False)
    '                            '            End If
    '                            '            If entryresultfound = True Or entryorderfound = True Or entrytestperformedfound = True Or entryfunctionalperformedfound Then
    '                            '                oReconcileList.mPatient.PatientLabResult = getPatientLabresults(entry, entryresultfound, entryorderfound, entrytestperformedfound, entryfunctionalperformedfound, oReconcileList.mPatient.PatientLabResult, oReconcileList.mPatient.PatientProblems, oReconcileList.mPatient.PatientHistory)
    '                            '            End If
    '                            '            If physicalexamfound = True Then
    '                            '                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
    '                            '            End If
    '                            '            If insurancefound = True Then
    '                            '                oReconcileList.mPatient.PatientDemographics.Insurances = getPatientInsurances(entry)
    '                            '            End If
    '                            '            If problemfound = True Then
    '                            '                oReconcileList.mPatient.PatientProblems = getPatientProblems(entry, oReconcileList.mPatient.PatientProblems)
    '                            '            End If
    '                            '            If riskfound = True Then
    '                            '                getPatientRiskcategory_New(entry, oReconcileList.mPatient.PatientHistory, observation, oReconcileList.mPatient.PatientLabResult)
    '                            '            End If



    '                            '        ElseIf Convert.ToString(entry.Item) = "gloCCDSchema.POCD_MT000040UV02Encounter" Then
    '                            '            Dim encounter As POCD_MT000040UV02Encounter
    '                            '            encounter = CType(entry.Item, POCD_MT000040UV02Encounter)
    '                            '            Dim TemlateCount As Integer = 0
    '                            '            For TemlateCount = 0 To encounter.templateId.Length - 1
    '                            '                If TemplateencounterEntry = encounter.templateId(TemlateCount).root.ToString() Then
    '                            '                    encounterfound = True
    '                            '                End If
    '                            '            Next
    '                            '            If encounterfound = True Then

    '                            '                'If CheckOnlyoneSnomed(entry, encounter) Then
    '                            '                '  oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, , True)
    '                            '                'Else
    '                            '                oReconcileList.mPatient.PatientEncounters = getPatientEncounters(entry, encounter, oReconcileList.mPatient.PatientEncounters)
    '                            '                ' End If
    '                            '                ' Dim _isonlysnomed As Boolean = False

    '                            '                'If _isonlysnomed = True Then
    '                            '                If blnCompareQRDA = False Then
    '                            '                    oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
    '                            '                End If


    '                            '                'End If
    '                            '            End If
    '                            '            encounter = Nothing
    '                            '        ElseIf Convert.ToString(entry.Item) = "gloCCDSchema.POCD_MT000040UV02Procedure" Then
    '                            '            Dim procedure As POCD_MT000040UV02Procedure
    '                            '            procedure = CType(entry.Item, POCD_MT000040UV02Procedure)
    '                            '            Dim TemlateCount As Integer = 0
    '                            '            For TemlateCount = 0 To procedure.templateId.Length - 1
    '                            '                If TemplateproEntry = procedure.templateId(TemlateCount).root.ToString() Then
    '                            '                    procedurefound = True
    '                            '                ElseIf templateProcorderEntry = procedure.templateId(TemlateCount).root.ToString() Then
    '                            '                    Procedureorderfound = True
    '                            '                End If
    '                            '                'If TemplateprointelorenceEntry = procedure.templateId(TemlateCount).root.ToString() Then
    '                            '                '    procedureintelofound = True
    '                            '                'End If
    '                            '            Next
    '                            '            procedure = Nothing
    '                            '            If procedurefound = True OrElse Procedureorderfound = True Then
    '                            '                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory)
    '                            '            End If

    '                            '            'Dim act As POCD_MT000040UV02Act = Nothing
    '                            '        ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02Act" Then
    '                            '            Dim act As POCD_MT000040UV02Act
    '                            '            act = CType(entry.Item, POCD_MT000040UV02Act)
    '                            '            Dim TemlateCount As Integer = 0

    '                            '            For TemlateCount = 0 To act.templateId.Length - 1
    '                            '                If TemplateImmEntry = act.templateId(TemlateCount).root.ToString() Then
    '                            '                    immunifound = True
    '                            '                End If
    '                            '                If TemplateprobEntry = act.templateId(TemlateCount).root.ToString() Or TemplateResolvedprobEntry = act.templateId(TemlateCount).root.ToString() Or TemplateInactiveprobEntry = act.templateId(TemlateCount).root.ToString() Then
    '                            '                    problemfound = True
    '                            '                End If
    '                            '                If TemplateInterventionEntry = act.templateId(TemlateCount).root.ToString() Then
    '                            '                    interventionfound = True
    '                            '                End If
    '                            '                If TemplateInterventionOrderEntry = act.templateId(TemlateCount).root.ToString() Then
    '                            '                    interventionOrder = True
    '                            '                End If
    '                            '                If Templatecommpttoprovider = act.templateId(TemlateCount).root.ToString() Then
    '                            '                    commpttoprovider = True
    '                            '                End If
    '                            '            Next
    '                            '            'If problemfound = True Then
    '                            '            '    oReconcileList.mPatient.PatientProblems = getPatientProblems(entry, oReconcileList.mPatient.PatientProblems)
    '                            '            'End If
    '                            '            If immunifound = True Then
    '                            '                oReconcileList.mPatient.PatientImmunizations = getPatientImmunizations(entry, oReconcileList.mPatient.PatientImmunizations)
    '                            '                'oReconcileList.mPatient.PatientHistory = getMedicationadministerednotperformed(entry, oReconcileList.mPatient.PatientHistory)
    '                            '            End If
    '                            '            If problemfound = True Then
    '                            '                oReconcileList.mPatient.PatientProblems = getpatientproblems_new(entry, oReconcileList.mPatient.PatientProblems)
    '                            '            End If
    '                            '            If interventionfound = True Or interventionOrder = True Or commpttoprovider = True Then
    '                            '                oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
    '                            '            End If
    '                            '            'If interventionOrder = True Then
    '                            '            '    oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
    '                            '            'End If
    '                            '            'If commpttoprovider = True Then
    '                            '            '    oReconcileList.mPatient.PatientHistory = getPatientProcedurePerformed(entry, oReconcileList.mPatient.PatientHistory, True)
    '                            '            'End If
    '                            '            act = Nothing
    '                            '        ElseIf Convert.ToString(entry.Item) = "POCD_MT000040UV02SubstanceAdministration" Then

    '                            '            oCDAMedicationAdmin = CType(entry.Item, POCD_MT000040UV02SubstanceAdministration)
    '                            '            For templatecount As Integer = 0 To oCDAMedicationAdmin.templateId.Length - 1
    '                            '                ' If activeentryfound = True Then
    '                            '                entrymediorderfound = False
    '                            '                activeentryfound = False
    '                            '                If medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                    activeentryfound = True
    '                            '                    medicationfound = True
    '                            '                End If

    '                            '                'ElseIf entrymediorderfound = True Then
    '                            '                If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                    entrymediorderfound = True
    '                            '                    medicationfound = True
    '                            '                End If


    '                            '                '  End If
    '                            '                'If activeentryfound = True AndAlso entrymediorderfound = True Then
    '                            '                '    If medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                '        medicationfound = True
    '                            '                '    ElseIf entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                '        medicationfound = True
    '                            '                '    End If
    '                            '                'ElseIf entrymediorderfound = True Then
    '                            '                '    If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                '        medicationfound = True
    '                            '                '    End If
    '                            '                'Else
    '                            '                '    If entryid = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                '        medicationfound = True
    '                            '                '    ElseIf medicationactiveentry = Convert.ToString(oCDAMedicationAdmin.templateId(templatecount).root) Then
    '                            '                '        medicationfound = True
    '                            '                '    End If
    '                            '                'End If
    '                            '            Next
    '                            '            If medicationfound = True Then
    '                            '                oReconcileList.mPatient.PatientMedications = getPatientMedications(entry, oReconcileList.mPatient.PatientMedications, entrymediorderfound)

    '                            '            End If

    '                            '        End If
    '                            '    End If

    '                            'Next

    '                        End If

    '                    End If
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
    '        ex = Nothing
    '    Finally
    '        'TemplatevitalEntry = Nothing
    '        'TemplateriskEntry = Nothing
    '        'templatecharentry = Nothing
    '        'functionalEntry = Nothing
    '        'LabTestEntry = Nothing
    '        'DiagnosticResultEntry = Nothing
    '        'DiagnosticOrderEntry = Nothing
    '        'LabtestResult = Nothing
    '        'diagnosticperformed = Nothing
    '        'TemplateencounterEntry = Nothing
    '        'TemplateproEntry = Nothing
    '        'templatephysicalperformed = Nothing
    '        'TemplateImmEntry = Nothing
    '        'templateinsurance = Nothing
    '        'TemplateprobEntry = Nothing

    '        'entryid = Nothing
    '        'medicationactiveentry = Nothing

    '        entry1 = Nothing
    '        oCDAMedicationAdmin = Nothing
    '    End Try
    'End Sub
    Private Function getPatientAllergies(ByVal Diagnosisentry As POCD_MT000040UV02Entry, ByRef oProblemList As ProblemsCol) As ProblemsCol
        'Allergy Intolerance is saved in Problems as previously itss QDMCategoy was condition/Problem/Disnosis

        Try


            If Not IsNothing(Diagnosisentry) Then
                Dim Diagnosisobservation As POCD_MT000040UV02Observation = Nothing
                If Convert.ToString(Diagnosisentry.Item) = "POCD_MT000040UV02Observation" Then
                    Diagnosisobservation = CType(Diagnosisentry.Item, POCD_MT000040UV02Observation)
                    Dim TemlateCount As Integer = 0

                    'For TemlateCount = 0 To Diagnosisobservation.templateId.Length - 1
                    '    If TemplateIDEntry = Diagnosisobservation.templateId(TemlateCount).root.ToString() Then
                    '        entryfound = True

                    '        'Dim valuecount As Integer = 0
                    '        'For valuecount = 0 To oinsuranceobservation.value.Length-1


                    '        'Next



                    '    End If
                    'Next
                    'If entryfound = True Then
                    Dim oProblem As Problems = Nothing
                    oProblem = New Problems()
                    Dim ob As ANY()
                    ob = CType(Diagnosisobservation.value, ANY())
                    Dim strStatus As String = ""
                    If Convert.ToString(Diagnosisobservation.statusCode.code).ToUpper() = "ACTIVE" Then
                        strStatus = "ACTIVE"
                    ElseIf Convert.ToString(Diagnosisobservation.statusCode.code).ToUpper() = "SUSPENDED" Then
                        strStatus = "INACTIVE"
                    ElseIf Convert.ToString(Diagnosisobservation.statusCode.code).ToUpper() = "COMPLETED" Then
                        strStatus = "RESOLVED"
                    End If

                    If Not IsNothing(ob) Then
                        If ob.Length > 0 Then
                            'ob.GetValue(0)

                            'conceptid = DirectCast(ob.GetValue(0),CD).code.ToString()
                            'oProblem.ConceptID = conceptid
                            If Not IsNothing(Diagnosisobservation.effectiveTime) Then
                                If Diagnosisobservation.effectiveTime.Items.Length > 0 Then
                                    If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).nullFlavor) <> "UNK" Then
                                        oProblem.DateOfService = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(0), IVXB_TS).value)))
                                    End If


                                End If
                                If strStatus.ToString.ToUpper() = "RESOLVED" Then
                                    If Diagnosisobservation.effectiveTime.Items.Length > 0 Then
                                        If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                            strStatus = "RESOLVED"
                                            oProblem.ResolvedDate = gloDateMaster.gloDate.QRDADateAsDateTime((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).value)))
                                        Else
                                            strStatus = "ACTIVE"
                                        End If
                                    End If
                                End If
                                'If Diagnosisobservation.effectiveTime.Items.Length > 1 Then
                                '    If Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).nullFlavor) <> "UNK" Then
                                '        oProblem.ResolvedDate = gloDateMaster.gloDate.QRDADateAsDateString((Convert.ToString(CType(Diagnosisobservation.effectiveTime.Items(1), IVXB_TS).value)))
                                '    End If

                                'End If

                            End If
                            If Not IsNothing(Diagnosisobservation.participant) AndAlso Diagnosisobservation.participant.Length > 0 Then
                                Dim Particpant As POCD_MT000040UV02Participant3() = Nothing
                                Dim ParticipantRole As POCD_MT000040UV02ParticipantRole = Nothing
                                Particpant = CType(Diagnosisobservation.participant, POCD_MT000040UV02Participant3())
                                ParticipantRole = CType(Particpant(0).participantRole, POCD_MT000040UV02ParticipantRole)
                                If Not IsNothing(ParticipantRole.Item) Then
                                    Dim PlayingEntity As POCD_MT000040UV02PlayingEntity = Nothing
                                    PlayingEntity = CType(ParticipantRole.Item, POCD_MT000040UV02PlayingEntity)
                                    If Not IsNothing(PlayingEntity.code) Then
                                        If PlayingEntity.code.codeSystem = CodeSystem.SNOMED_CT Then
                                            oProblem.ConceptID = PlayingEntity.code.code
                                        End If

                                    End If

                                End If






                            End If

                            Dim dticd As DataTable = New DataTable()




                            Dim ed As ED
                            ed = CType(ob.GetValue(0), CD).originalText
                            If Not IsNothing(ed) Then
                                Dim text As String = Convert.ToString(ed.Text(0))
                                Dim arr As String()
                                If text.Contains(":") Then
                                    arr = text.Split(":")
                                    If arr.Length > 0 Then
                                        text = arr(1)
                                    End If
                                    arr = Nothing



                                End If
                                If oProblem.ConceptID <> "" Then
                                    oProblem.Condition = text
                                End If
                            End If



                            'If oProblem.ICD9 = "" Then
                            '    oProblem.ICD9 = Text
                            'End If
                            'If dtsnomed.Rows.Count > 0 Then
                            '    oProblem.Condition = dtsnomed.Rows(0)("TermDescription")
                            'End If
                            If dticd.Rows.Count > 0 Then
                                oProblem.ICD9 = dticd.Rows(0)("sDescription")
                                If oProblem.Condition = "" Then
                                    oProblem.Condition = dticd.Rows(0)("sDescription")
                                End If
                            End If

                            If Not IsNothing(dticd) Then
                                dticd.Dispose()
                            End If

                        End If


                    End If





                    If strStatus <> "" Then
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
                    oProblemList.Add(oProblem)
                    oProblem.Dispose()

                End If

            End If


            'End If

            '    Next
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            oProblemList = Nothing
            Return Nothing
        Finally

        End Try

        Return oProblemList

    End Function
    Public Function GetCQMMeasureCodes() As DataTable
        Dim dtMeasure As DataTable = New DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "SELECT DISTINCT Code,ValueSetOID  FROM MUCQMCodes"


            oDB.Retrive_Query(_sqlQuery, dtMeasure)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            _sqlQuery = Nothing
        End Try
        Return dtMeasure
    End Function
End Class
