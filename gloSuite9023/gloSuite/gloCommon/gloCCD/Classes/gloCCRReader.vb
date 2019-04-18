Imports gloPatient
Imports gloCCRSchema
Imports gloCCDSchema




Public Class gloCCRReader
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

    Public Function ExtractCCR_DemographicsOnly(ByVal strCCDFilePath As String) As ReconcileList

        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCRSchema As ContinuityOfCareRecord = Nothing

        Try
            oCCRSchema = gloSerialization.GetContinuityOfCareRecord(strCCDFilePath)

            If Not IsNothing(oCCRSchema) Then

                oReconcileList.mPatient = New Patient()

                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCRSchema)


             

                ''File Source
                If Not IsNothing(oCCRSchema.Actors) Then

                    ''File Datetime
                    If Not IsNothing(oCCRSchema.DateTime) Then
                        If Not IsNothing(oCCRSchema.DateTime.ExactDateTime) Then
                            Dim _FileHeaderDateTime As DateTime
                            If Date.TryParse(Convert.ToDateTime(oCCRSchema.DateTime.ExactDateTime), _FileHeaderDateTime) = True Then
                                oReconcileList.FileHeaderDateTime = _FileHeaderDateTime
                                oReconcileList.LastModifiedDateTime = _FileHeaderDateTime
                            End If
                        End If
                    End If
                    Dim oOrganization As ActorTypeOrganization
                    Dim i As Int32
                    For i = 0 To oCCRSchema.Actors.Length - 1
                        oOrganization = TryCast(oCCRSchema.Actors(i).Item, ActorTypeOrganization)
                        If Not IsNothing(oOrganization) Then
                            If Not IsNothing(oOrganization.Name) Then
                                oReconcileList.FileHeaderSource = Convert.ToString(oOrganization.Name)
                            End If
                            Exit For
                        End If
                    Next
                End If


            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            If Not IsNothing(oCCRSchema) Then
                oCCRSchema = Nothing
            End If

        End Try

        Return oReconcileList

    End Function

    Public Function ExtractCCR(ByVal strCCDFilePath As String) As ReconcileList

        Dim oReconcileList As ReconcileList = New ReconcileList
        Dim oCCRSchema As ContinuityOfCareRecord = Nothing

        Try
            oCCRSchema = gloSerialization.GetContinuityOfCareRecord(strCCDFilePath)

            If Not IsNothing(oCCRSchema) Then

                oReconcileList.mPatient = New Patient()

                oReconcileList.mPatient.PatientDemographics = getPatientDemographics(oCCRSchema)
                oReconcileList.mPatient.PatientHistory = getPatientHistory(oCCRSchema)
                oReconcileList.mPatient.PatientProblems = getPatientProblems(oCCRSchema)
                oReconcileList.mPatient.PatientMedications = getPatientMedications(oCCRSchema)


               

                ''File Source
                If Not IsNothing(oCCRSchema.Actors) Then
                    ''File Datetime
                    If Not IsNothing(oCCRSchema.DateTime) Then
                        If Not IsNothing(oCCRSchema.DateTime.ExactDateTime) Then
                            Dim _FileHeaderDateTime As DateTime
                            If Date.TryParse(Convert.ToDateTime(oCCRSchema.DateTime.ExactDateTime), _FileHeaderDateTime) = True Then
                                oReconcileList.FileHeaderDateTime = _FileHeaderDateTime
                                oReconcileList.LastModifiedDateTime = _FileHeaderDateTime
                            End If
                        End If
                    End If
                    Dim oOrganization As ActorTypeOrganization
                    Dim i As Int32
                    For i = 0 To oCCRSchema.Actors.Length - 1
                        oOrganization = TryCast(oCCRSchema.Actors(i).Item, ActorTypeOrganization)
                        If Not IsNothing(oOrganization) Then
                            If Not IsNothing(oOrganization.Name) Then
                                oReconcileList.FileHeaderSource = Convert.ToString(oOrganization.Name)
                            End If
                            Exit For
                        End If
                    Next
                    oOrganization = Nothing
                End If


            End If


        Catch ex As Exception
            oReconcileList = Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        Finally
            If Not IsNothing(oCCRSchema) Then
                oCCRSchema = Nothing
            End If

        End Try

        Return oReconcileList

    End Function


    Private Function getPatientDemographics(ByRef oCCR As ContinuityOfCareRecord) As gloPatient.Patient

        Dim PatDemographics As gloPatient.Patient = New gloPatient.Patient
        Dim sPatientActorID As String = ""
        Try

            If Not IsNothing(oCCR) Then

                If Not IsNothing(oCCR.Patient(0)) Then

                    'Get Patient ActorID 
                    sPatientActorID = oCCR.Patient(0).ActorID

                    Dim i As Int32
                    If Not IsNothing(oCCR.Actors) Then


                        For i = 0 To oCCR.Actors.Length - 1

                            'Find Patient in Actors List by ActorID 
                            If oCCR.Actors(i).ActorObjectID = sPatientActorID Then
                                PatDemographics.DemographicsDetail.PatientCode = ""
                                PatDemographics.DemographicsDetail.PatientExternalCode = sPatientActorID



                                'Patient Name, DOB, Gender
                                If Not IsNothing(oCCR.Actors(i).Item) Then
                                    If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name) Then
                                        If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName) Then
                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Given) Then
                                                PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Given(0))
                                            End If
                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Middle) Then
                                                PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Middle(0))
                                            End If
                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Family) Then
                                                PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.CurrentName.Family(0))
                                            End If
                                        ElseIf Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName) Then

                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Given) Then
                                                PatDemographics.DemographicsDetail.PatientFirstName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Given(0))
                                            End If
                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Middle) Then
                                                PatDemographics.DemographicsDetail.PatientMiddleName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Middle(0))
                                            End If
                                            If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Family) Then
                                                PatDemographics.DemographicsDetail.PatientLastName = Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).Name.BirthName.Family(0))
                                            End If
                                        End If
                                    End If

                                    If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).DateOfBirth) Then
                                        Dim _DOB As DateTime
                                        If CType(oCCR.Actors(i).Item, ActorTypePerson).DateOfBirth.ExactDateTime <> "" Then
                                            If Date.TryParse(Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).DateOfBirth.ExactDateTime), _DOB) = True Then
                                                PatDemographics.DemographicsDetail.PatientDOB = _DOB.ToString("MM/dd/yyyy")
                                            End If

                                        Else
                                            If Date.TryParse(Convert.ToString(CType(oCCR.Actors(i).Item, ActorTypePerson).DateOfBirth.ApproximateDateTime.Text), _DOB) = True Then
                                                PatDemographics.DemographicsDetail.PatientDOB = _DOB.ToString("MM/dd/yyyy")
                                            End If
                                        End If

                                    End If

                                    If Not IsNothing(CType(oCCR.Actors(i).Item, ActorTypePerson).Gender) Then
                                        If CType(oCCR.Actors(i).Item, ActorTypePerson).Gender.Text = "F" Or CType(oCCR.Actors(i).Item, ActorTypePerson).Gender.Text = "Female" Then
                                            PatDemographics.DemographicsDetail.PatientGender = "Female"
                                        ElseIf CType(oCCR.Actors(i).Item, ActorTypePerson).Gender.Text = "M" Or CType(oCCR.Actors(i).Item, ActorTypePerson).Gender.Text = "Male" Then
                                            PatDemographics.DemographicsDetail.PatientGender = "Male"
                                        End If
                                    End If

                                End If

                                'Patient Address
                                If Not IsNothing(oCCR.Actors(i).Address) AndAlso (oCCR.Actors(i).Address.Length > 0) Then
                                    If Not IsNothing(oCCR.Actors(i).Address.Length) Then
                                        If (oCCR.Actors(i).Address.Length > 0) Then
                                            PatDemographics.DemographicsDetail.PatientAddress1 = Convert.ToString(oCCR.Actors(i).Address(0).Line1)
                                            PatDemographics.DemographicsDetail.PatientAddress2 = Convert.ToString(oCCR.Actors(i).Address(0).Line2)
                                            PatDemographics.DemographicsDetail.PatientCity = Convert.ToString(oCCR.Actors(i).Address(0).City)
                                            PatDemographics.DemographicsDetail.PatientState = Convert.ToString(oCCR.Actors(i).Address(0).State)
                                            PatDemographics.DemographicsDetail.PatientZip = Convert.ToString(oCCR.Actors(i).Address(0).PostalCode)
                                            PatDemographics.DemographicsDetail.PatientCounty = Convert.ToString(oCCR.Actors(i).Address(0).County)
                                            PatDemographics.DemographicsDetail.PatientCountry = Convert.ToString(oCCR.Actors(i).Address(0).Country)
                                        End If
                                    End If
                                End If

                                'Patient Phone
                                If Not IsNothing(oCCR.Actors(i).Telephone) AndAlso (oCCR.Actors(i).Telephone.Length > 0) Then
                                    If Not IsNothing(oCCR.Actors(i).Telephone.Length) Then
                                        If (oCCR.Actors(i).Telephone.Length > 0) Then
                                            PatDemographics.DemographicsDetail.PatientPhone = Convert.ToString(oCCR.Actors(i).Telephone(0).Value)
                                        End If
                                    End If
                                End If

                                'Patient Actor Found
                                Exit For

                            End If
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            PatDemographics = Nothing
            Throw ex
        Finally
            sPatientActorID = Nothing
        End Try

        Return PatDemographics

    End Function

    Private Function getPatientHistory(ByRef oCCR As ContinuityOfCareRecord) As gloPatientHistoryCol

        Dim oPatientHistoryList As gloPatientHistoryCol = New gloPatientHistoryCol()
        Try

            If Not IsNothing(oCCR) Then
                Dim oPatientHistory As gloPatientHistory = Nothing

                If Not IsNothing(oCCR.Body) Then
                    If Not IsNothing(oCCR.Body.Alerts) Then

                        Dim i As Int32
                        For i = 0 To oCCR.Body.Alerts.Length - 1

                            oPatientHistory = New gloPatientHistory()

                            'Drug Name
                            If Not IsNothing(oCCR.Body.Alerts(i).Description) Then
                                oPatientHistory.HistoryItem = Convert.ToString(oCCR.Body.Alerts(i).Description.Text)
                                oPatientHistory.DrugName = Convert.ToString(oCCR.Body.Alerts(i).Description.Text)

                                'SNOMED Code (ConceptId)
                                If Not IsNothing(oCCR.Body.Alerts(i).Description.Code) Then
                                    If oCCR.Body.Alerts(i).Description.Code.Length > 0 Then
                                        If Not IsNothing(Convert.ToString(oCCR.Body.Alerts(i).Description.Code(0).CodingSystem)) Then
                                            If oCCR.Body.Alerts(i).Description.Code(0).CodingSystem.Length > 0 Then
                                                If Convert.ToString(oCCR.Body.Alerts(i).Description.Code(0).CodingSystem).Contains("SNOMED") Then

                                                    oPatientHistory.ConceptId = Convert.ToString(oCCR.Body.Alerts(i).Description.Code(0).Value)

                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                ''NDCCode and ''Rxnorm
                                If Not IsNothing(oCCR.Body.Alerts(i).Agent) Then
                                    If oCCR.Body.Alerts(i).Agent.Length > 0 Then
                                        If Not IsNothing(oCCR.Body.Alerts(i).Agent(0).Products) Then
                                            If oCCR.Body.Alerts(i).Agent(0).Products.Length > 0 Then
                                                If Not IsNothing(oCCR.Body.Alerts(i).Agent(0).Products(0).Product) Then
                                                    If Not IsNothing(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName) Then
                                                        If oCCR.Body.Alerts(i).Agent(0).Products(0).Product.Length > 0 Then

                                                            If Not IsNothing(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code) Then
                                                                If oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code.Length > 0 Then


                                                                    If Not IsNothing(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code(0).CodingSystem) Then

                                                                        If Convert.ToString(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code(0).CodingSystem.ToUpper().Contains("NDC")) Then
                                                                            oPatientHistory.NDCCode = Convert.ToString(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code(0).Value)
                                                                        ElseIf Convert.ToString(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code(0).CodingSystem.ToUpper().Contains("RXNORM")) Then
                                                                            oPatientHistory.RxNormCode = Convert.ToString(oCCR.Body.Alerts(i).Agent(0).Products(0).Product(0).ProductName.Code(0).Value)
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


                            If Not IsNothing(oCCR.Body.Alerts(i).Reaction) Then
                                If Not IsNothing(oCCR.Body.Alerts(i).Reaction.Length) Then
                                    If oCCR.Body.Alerts(i).Reaction.Length > 0 Then
                                        If Not IsNothing(oCCR.Body.Alerts(i).Reaction(0).Description) Then
                                            oPatientHistory.Reaction = Convert.ToString(oCCR.Body.Alerts(i).Reaction(0).Description.Text)
                                        End If
                                    End If
                                End If
                            End If

                            If Not IsNothing(oCCR.Body.Alerts(i).Status) Then
                                oPatientHistory.Status = Convert.ToString(oCCR.Body.Alerts(i).Status.Text)
                            End If

                            oPatientHistory.DOEAllergy = DateTime.Now.ToString("MM/dd/yyyy")
                            If Not IsNothing(oCCR.Body.Alerts(i).DateTime) Then
                                If Not IsNothing(oCCR.Body.Alerts(i).DateTime.Length) Then
                                    If oCCR.Body.Alerts(i).DateTime.Length > 0 Then
                                        If Not IsNothing(oCCR.Body.Alerts(i).DateTime(0).ApproximateDateTime) Then

                                            Dim _DOEAllergy As DateTime

                                            If Date.TryParse(Convert.ToString(oCCR.Body.Alerts(i).DateTime(0).ApproximateDateTime.Text), _DOEAllergy) = False Then
                                                _DOEAllergy = DateTime.Now
                                            End If

                                            oPatientHistory.DOEAllergy = _DOEAllergy.ToString("MM/dd/yyyy")

                                        End If
                                    End If
                                End If
                            End If

                            'If Not IsNothing(oCCR.Body.Alerts(i).Status) Then
                            '    oPatientHistory.Status = Convert.ToString(oCCR.Body.Alerts(i).Status.Text)
                            'End If

                            oPatientHistory.HistoryCategory = "Allergies"
                            ' oPatientHistory.RxNormCode = ""
                            oPatientHistory.Comments = ""


                            ''Add History Item to collection
                            oPatientHistoryList.Add(oPatientHistory)
                            If Not IsNothing(oPatientHistory) Then
                                oPatientHistory.Dispose()
                                oPatientHistory = Nothing
                            End If

                        Next

                    End If
                End If
            End If



        Catch ex As Exception
            oPatientHistoryList = Nothing
            Throw ex
        End Try

        Return oPatientHistoryList

    End Function

    Private Function getPatientProblems(ByRef oCCR As ContinuityOfCareRecord) As ProblemsCol

        Dim oProblemList As ProblemsCol = New ProblemsCol()
        Try

            If Not IsNothing(oCCR) Then
                Dim oProblem As Problems = Nothing

                If Not IsNothing(oCCR.Body) Then
                    If Not IsNothing(oCCR.Body.Problems) Then

                        Dim i As Int32
                        For i = 0 To oCCR.Body.Problems.Length - 1
                            oProblem = New Problems()

                            'Problem DOS
                            oProblem.DateOfService = DateTime.Now.ToString("MM/dd/yyyy")
                            If Not IsNothing(oCCR.Body.Problems(i).DateTime) Then
                                If Not IsNothing(oCCR.Body.Problems(i).DateTime.Length) Then
                                    If oCCR.Body.Problems(i).DateTime.Length > 0 Then
                                        If Not IsNothing(oCCR.Body.Problems(i).DateTime(0).ApproximateDateTime) Then

                                            Dim _DateOfService As DateTime

                                            If Date.TryParse(Convert.ToString(oCCR.Body.Problems(i).DateTime(0).ApproximateDateTime.Text), _DateOfService) = False Then
                                                _DateOfService = DateTime.Now
                                            End If
                                            If (IsNothing(_DateOfService)) Then
                                                _DateOfService = DateTime.Now
                                            End If
                                            oProblem.DateOfService = _DateOfService.ToString("MM/dd/yyyy")

                                        End If
                                    End If
                                End If
                            End If


                            'ICD9 Code and sCheifComplaint
                            If Not IsNothing(oCCR.Body.Problems(i).Description) Then

                                oProblem.Condition = Convert.ToString(oCCR.Body.Problems(i).Description.Text)     'sCheifComplaint

                                If Not IsNothing(oCCR.Body.Problems(i).Description.Code) Then

                                    If oCCR.Body.Problems(i).Description.Code.Length = 1 Then

                                        If Not IsNothing(Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).CodingSystem)) Then
                                            If Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).CodingSystem).ToUpper().Contains("ICD") = True Then
                                                oProblem.ICD9Code = Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).Value)
                                                oProblem.ICD9 = Convert.ToString(oCCR.Body.Problems(i).Description.Text)
                                            End If
                                        End If
                                    ElseIf oCCR.Body.Problems(i).Description.Code.Length > 1 Then
                                        If Not IsNothing(Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).CodingSystem)) Then
                                            If Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).CodingSystem).ToUpper().Contains("ICD") = True Then
                                                oProblem.ICD9Code = Convert.ToString(oCCR.Body.Problems(i).Description.Code(0).Value)
                                                oProblem.ICD9 = Convert.ToString(oCCR.Body.Problems(i).Description.Text)
                                            End If
                                        End If
                                        If Not IsNothing(Convert.ToString(oCCR.Body.Problems(i).Description.Code(1).CodingSystem)) Then
                                            If Convert.ToString(oCCR.Body.Problems(i).Description.Code(1).CodingSystem).ToUpper().Contains("SNOMED-CT") = True Then
                                                oProblem.ConceptID = Convert.ToString(oCCR.Body.Problems(i).Description.Code(1).Value)

                                            End If
                                        End If
                                    End If


                                End If
                            End If

                            oProblem.Immediacy = 3      'hardcode  

                            'Status
                            If Not IsNothing(oCCR.Body.Problems(i).Status) Then
                                If Not IsNothing(oCCR.Body.Problems(i).Status.Text) Then

                                    Select Case Convert.ToString(oCCR.Body.Problems(i).Status.Text.ToUpper())
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

                            'Add Problem object to problem list
                            oProblemList.Add(oProblem)
                            If Not IsNothing(oProblem) Then
                                oProblem.Dispose()
                                oProblem = Nothing
                            End If
                        Next

                    End If
                End If
            End If

            Return oProblemList

        Catch ex As Exception
            oProblemList = Nothing
            Throw ex
        End Try

        Return oProblemList

    End Function

    Private Function getPatientMedications(ByRef oCCR As ContinuityOfCareRecord) As MedicationsCol

        Dim oMedicationList As MedicationsCol = New MedicationsCol()
        Try

            If Not IsNothing(oCCR) Then
                Dim oMedication As Medication = Nothing

                If Not IsNothing(oCCR.Body) Then
                    If Not IsNothing(oCCR.Body.Medications) Then


                        Dim i As Int32
                        For i = 0 To oCCR.Body.Medications.Length - 1
                            oMedication = New Medication()

                            'Medication Date
                            oMedication.MedicationDate = DateTime.Now.ToString("MM/dd/yyyy")
                                       
                            'Medication Start Date
                            If Not IsNothing(oCCR.Body.Medications(i).DateTime) Then
                                If oCCR.Body.Medications(i).DateTime.Length > 0 Then
                                    If Not IsNothing(oCCR.Body.Medications(i).DateTime(0).ApproximateDateTime) Then

                                        Dim _MedicationDate As DateTime

                                        If Date.TryParse(Convert.ToString(oCCR.Body.Medications(i).DateTime(0).ApproximateDateTime.Text), _MedicationDate) = True Then
                                            oMedication.StartDate = _MedicationDate.ToString("MM/dd/yyyy")
                                        End If

                                    End If
                                End If
                            End If


                            'RxNormCode, GenericName
                            If Not IsNothing(oCCR.Body.Medications(i).Product) Then
                                If oCCR.Body.Medications(i).Product.Length > 0 Then


                                    If Not IsNothing(oCCR.Body.Medications(i).Product(0).ProductName) Then

                                        If Not IsNothing(oCCR.Body.Medications(i).Product(0).ProductName.Code) Then
                                            If oCCR.Body.Medications(i).Product(0).ProductName.Code.Length > 0 Then
                                                If Not IsNothing(Convert.ToString(oCCR.Body.Medications(i).Product(0).ProductName.Code(0).CodingSystem)) Then

                                                    If Convert.ToString(oCCR.Body.Medications(i).Product(0).ProductName.Code(0).CodingSystem).ToUpper().Contains("RXNORM") = True Then
                                                        oMedication.RxNormCode = Convert.ToString(oCCR.Body.Medications(i).Product(0).ProductName.Code(0).Value)
                                                    Else
                                                        oMedication.RxNormCode = ""
                                                    End If

                                                End If

                                            End If
                                        End If

                                        oMedication.GenericName = Convert.ToString(oCCR.Body.Medications(i).Product(0).ProductName.Text)
                                        oMedication.DrugName = Convert.ToString(oCCR.Body.Medications(i).Product(0).ProductName.Text)
                                    End If



                                    'Strength
                                    If Not IsNothing(oCCR.Body.Medications(i).Product(0).Strength) Then
                                        If oCCR.Body.Medications(i).Product(0).Strength.Length > 0 Then

                                            oMedication.DrugStrength = Convert.ToString(oCCR.Body.Medications(i).Product(0).Strength(0).Value)

                                            If Not IsNothing(oCCR.Body.Medications(i).Product(0).Strength(0).Units) Then
                                                oMedication.StrengthUnits = Convert.ToString(oCCR.Body.Medications(i).Product(0).Strength(0).Units.Unit)
                                            End If

                                        End If

                                    End If

                                    'Route
                                    If Not IsNothing(oCCR.Body.Medications(i).Product(0).Form) Then
                                        If oCCR.Body.Medications(i).Product(0).Form.Length > 0 Then
                                            oMedication.Route = Convert.ToString(oCCR.Body.Medications(i).Product(0).Form(0).Text)
                                        End If
                                    End If

                                End If
                            End If

                            'Frequency
                            If Not IsNothing(oCCR.Body.Medications(i).Directions) Then
                                If oCCR.Body.Medications(i).Directions.Length > 0 Then
                                    If Not IsNothing(oCCR.Body.Medications(i).Directions(0).Frequency) Then
                                        If oCCR.Body.Medications(i).Directions(0).Frequency.Length > 0 Then
                                            oMedication.Frequency = Convert.ToString(CType(oCCR.Body.Medications(i).Directions(0).Frequency(0), FrequencyType).Value)
                                        End If
                                    End If
                                End If
                            End If

                            'Quantity
                            If Not IsNothing(oCCR.Body.Medications(i).Quantity) Then
                                If oCCR.Body.Medications(i).Quantity.Length > 0 Then
                                    oMedication.DrugQuantity = Convert.ToString(oCCR.Body.Medications(i).Quantity(0).Value)
                                End If

                            End If

                            'Status
                            If Not IsNothing(oCCR.Body.Medications(i).Status) Then
                                oMedication.Status = Convert.ToString(oCCR.Body.Medications(i).Status.Text)
                            End If

                            'Refills
                            If Not IsNothing(oCCR.Body.Medications(i).Refills) Then
                                If oCCR.Body.Medications(i).Refills.Length > 0 Then
                                    oMedication.Refills = Convert.ToString(oCCR.Body.Medications(i).Refills(0).Number)
                                End If

                            End If



                            oMedicationList.Add(oMedication)
                            If Not IsNothing(oMedication) Then
                                oMedication.Dispose()
                                oMedication = Nothing
                            End If
                        Next

                    End If
                End If
            End If



        Catch ex As Exception
            oMedicationList = Nothing
            Throw ex
        End Try

        Return oMedicationList

    End Function


End Class
