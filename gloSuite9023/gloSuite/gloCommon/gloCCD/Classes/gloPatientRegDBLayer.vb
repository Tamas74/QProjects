Imports System.Data.SqlClient
Imports System.Windows.Forms
Partial Public Class gloPatientRegDBLayer

    'Code Start- Added by kanchan on 20101011 for Registration of CCD Patient

    Public Function RegisterNew_Patient(ByVal oPatient As gloCCDLibrary.Patient, ByVal IsPatientAllDetails As Boolean) As Int64
        Dim _PatientID As Int64
        'Dim conn As New SqlConnection
        Dim _CCDID As Int64 = 0
        Dim _Language As String = ""
        Dim objCCDDatabaseLayer As gloCCDDatabaseLayer = Nothing
        Dim ogloPatient As gloPatient.gloPatient = New gloPatient.gloPatient(gloLibCCDGeneral.Connectionstring)
        Try

            If Not IsNothing(oPatient) Then
                _PatientID = ogloPatient.Add(oPatient.PatientDemographics)
            End If

            'Patient Audit
            If _PatientID <> 0 Then
                Dim AuditTrailId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.SetupPatient, gloAuditTrail.ActivityType.Add, "Add Patient", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                Dim oAudit As New gloPatient.clsgloPatientAudit(gloLibCCDGeneral.Connectionstring)
                oAudit.SavePatientAuditDetails(_PatientID, AuditTrailId, "Register Patient - CCD-CCR")
                oAudit.Dispose()
                oAudit = Nothing
            End If

            'Code Start-Added by kanchan on 20101011 for Modular CCD Rendering & save
            'Add Race,Ethnicity & Language in Category_Mst,if not exists
            objCCDDatabaseLayer = New gloCCDDatabaseLayer()
            'Code Start Added by kanchan on 20101113 to save patient code as external code
            If oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode <> "" Then
                objCCDDatabaseLayer.UpdatePatientExternalCode(_PatientID, oPatient.PatientDemographics.DemographicsDetail.PatientExternalCode)
            End If
            If oPatient.Race <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.Race, "Race", _PatientID)
            End If
            If oPatient.ethnicGroupCode <> "" Then
                objCCDDatabaseLayer.UpdateCategoryMaster(oPatient.ethnicGroupCode, "Ethnicity", _PatientID)
            End If
            If Not IsNothing(oPatient.PatientLanguages) AndAlso oPatient.PatientLanguages.Count > 0 Then
                If Not IsNothing(oPatient.PatientLanguages.Item(0).Language) Then
                    _Language = oPatient.PatientLanguages.Item(0).Language
                    If _Language <> "" Then
                        objCCDDatabaseLayer.UpdateCategoryMaster(_Language, "Language", _PatientID)
                    End If
                End If
            End If

            'Code End-Added by kanchan on 20101011 for Modular CCD Rendering & save

            'Code Start-Added by kanchan on 20101102 for registration of all & demo detail option
            If IsPatientAllDetails = True Then
                '' Code Start-Addded by kanchan on 20101011 for Modularwise rendering
                Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)

                If Not IsNothing(oPatient.PatientHistory) Then
                    'Dim ogloPatientHistoryCol As gloPatientHistoryCol = oPatient.PatientHistory
                    ogloPatientRegDBLayer.SavePatientHistory(0, 0, _PatientID, oPatient.PatientHistory, "False", oPatient.UserDetails.UserName)
                End If

                Dim _ProviderName As String = objCCDDatabaseLayer.GetProviderName(objCCDDatabaseLayer.getDefaultProviderId())

                If Not IsNothing(oPatient.PatientProblems) Then
                    'Dim ogloPatientProblemCol As ProblemsCol = oPatient.PatientProblems
                    ogloPatientRegDBLayer.SaveProblemList(_PatientID, oPatient.PatientProblems, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName, _ProviderName)
                End If

                If Not IsNothing(oPatient.PatientVitals) Then
                    'Dim ogloPatientVitalCol As VitalsCol = oPatient.PatientVitals
                    ogloPatientRegDBLayer.AddNewVitals(_PatientID, oPatient.PatientVitals, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientMedications) Then
                    'Dim ogloPatientMedicationCol As MedicationsCol = oPatient.PatientMedications
                    ogloPatientRegDBLayer.SaveMedication(_PatientID, oPatient.PatientMedications, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientImmunizations) Then
                    'Dim ogloPatientImmunizationCol As ImmunizationCol = oPatient.PatientImmunizations
                    ogloPatientRegDBLayer.SaveImmunization(_PatientID, oPatient.PatientImmunizations, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(oPatient.PatientLabResult) Then
                    'Dim ogloPatientLabResultCol As LabResultsCol = oPatient.PatientLabResult
                    ogloPatientRegDBLayer.SaveLabResult(_PatientID, oPatient.PatientLabResult, oPatient.UserDetails.UserID, oPatient.UserDetails.UserName)
                End If

                If Not IsNothing(ogloPatientRegDBLayer) Then
                    ogloPatientRegDBLayer.Dispose()
                    ogloPatientRegDBLayer = Nothing
                End If

                'Code End-Addded by kanchan on 20101011 for Modularwise rendering
            End If


            Return _PatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'conn.Dispose()
            'conn = Nothing
            If Not IsNothing(objCCDDatabaseLayer) Then
                objCCDDatabaseLayer.Dispose()
                objCCDDatabaseLayer = Nothing
            End If
            If Not IsNothing(ogloPatient) Then
                ogloPatient.Dispose()
                ogloPatient = Nothing
            End If
            _Language = Nothing
        End Try

    End Function

    Private Function GetCategoryID(ByVal CategoryName As String, Optional ByVal CategoryType As String = "History") As Int64
        Dim Cmd As SqlCommand = Nothing
        Dim StrQuery As String = Nothing
        Try
            StrQuery = "Select nCategoryID from Category_mst where sDescription='" & CategoryName.Trim() & "' and scategoryType='" & CategoryType & "'"
            Cmd = New SqlCommand(StrQuery, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim intReturn As Int64 = Cmd.ExecuteScalar()

            Return intReturn
        Catch ex As Exception
            Throw
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            StrQuery = Nothing
        End Try
    End Function

    Private Function VerifyHistoryItemAvailability(ByVal ItemName As String, ByVal CategoryID As Int64) As Boolean
        Dim Cmd As SqlCommand = Nothing
        Dim StrQuery As String = Nothing
        Try

            '' Checking record is available or not
            StrQuery = "Select count(*) from History_mst where sDescription='" & ItemName.Trim() & "' and ncategoryId=" & CategoryID
            Cmd = New SqlCommand(StrQuery, conn)
            Dim intReturn As Int64 = 0
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            intReturn = Cmd.ExecuteScalar()
            If intReturn > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            StrQuery = Nothing
        End Try
    End Function
    Private Function AddHistoryItemData(ByVal str1 As String, ByVal str2 As String, ByVal CategoryID As Long) As Long
        Dim Cmd As SqlClient.SqlCommand = Nothing
        Dim objParam As SqlClient.SqlParameter = Nothing
        Try

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpHistory_Mst", conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID

            objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.InputOutput
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

            objParam = Cmd.Parameters.Add("@HistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ""

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Cmd.ExecuteNonQuery()

            Return objParam.Value

        Catch ex As Exception
            Throw
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Function
    Public Function GenerateVisitID(ByVal VisitDate As DateTime, ByVal PatientID As Int64) As Int64

        'Dim addresult As Object = Nothing

        Dim cmdVisits As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Dim objFlagParam As SqlParameter = Nothing
        'Dim otransaction As SqlTransaction()
        Try

            cmdVisits = New SqlCommand("gsp_InsertVisits", conn)
            cmdVisits.CommandType = CommandType.StoredProcedure
            'Dim cmd As New SqlCommand()

            objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            Dim nAppointmentID As Int64
            nAppointmentID = 0

            objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nAppointmentID

            objFlagParam = cmdVisits.Parameters.Add("@flag", SqlDbType.Int)
            objFlagParam.Direction = ParameterDirection.Output

            objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

            objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output
            objParam.Value = 0

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmdVisits.ExecuteNonQuery()


            Return DirectCast(objParam.Value, Int64)
        Catch ex As Exception
            Throw
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(cmdVisits) Then
                cmdVisits.Parameters.Clear()
                cmdVisits.Dispose()
                cmdVisits = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If Not IsNothing(objFlagParam) Then
                objFlagParam = Nothing
            End If

        End Try
    End Function

    Public Function SavePatientHistory(ByVal HistoryID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal ogloPatientHistoryCol As gloPatientHistoryCol, ByVal IsModify As Boolean, ByVal _userName As String, Optional ByVal Col_RemovedAllergies As Collection = Nothing) As Boolean

        Dim _TempHistoryID As Long = 0
        Dim cmd As SqlCommand = Nothing
        Try
            Dim k As Int64 = 1
            ' conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            '''' Col_RemovedAllergies Collection Passed to Maintain Audit Log of Removed Allergies

            ''''' To Delete History 
            '''' If History is in Modify Mode then Use Delete-Insert Methode
            If IsModify = True Then
                Dim cmdDel As New SqlCommand("gsp_DeleteHistory", conn)
                cmdDel.CommandType = CommandType.StoredProcedure

                Dim sqlParam1 As SqlParameter
                sqlParam1 = cmdDel.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam1.Direction = ParameterDirection.Input

                sqlParam1 = cmdDel.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam1.Direction = ParameterDirection.Input


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmdDel.ExecuteNonQuery()

                If cmdDel IsNot Nothing Then
                    cmdDel.Parameters.Clear()
                    cmdDel.Dispose()
                    cmdDel = Nothing
                End If

                If Not IsNothing(sqlParam1) Then
                    sqlParam1 = Nothing
                End If
            End If
            _TempHistoryID = 0


            cmd = New SqlCommand("gsp_AddHistory", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            For Each oPatientHistory As gloPatientHistory In ogloPatientHistoryCol

                '' Reset
                HistoryID = 0

                '''' Check for History_mst for History Item, if not available,insert Data
                Dim Category_id As Int64 = GetCategoryID(oPatientHistory.HistoryCategory, "History")
                If VerifyHistoryItemAvailability(oPatientHistory.HistoryItem, Category_id) = False Then
                    AddHistoryItemData(oPatientHistory.HistoryItem, "", Category_id)
                End If
                ''Added by Mayuri -Discussed with Saket on 20130227-we should save data against current visit
                oPatientHistory.VisitID = GenerateVisitID(DateTime.Now, PatientID)

                '''' If Modify then All Recordes are Deleted 
                '''' there No Need to Check Duplicate Data 
                If IsModify = False Then
                    If CheckDuplicate_New(oPatientHistory.VisitID, oPatientHistory.HistoryCategory, oPatientHistory.HistoryItem) = True Then

                        '' Record found  
                        If CType(oPatientHistory.HistoryCategory, String) <> "Allergies" Then
                            '' if Not Allergies then Modify
                            HistoryID = 1  ''(Update)
                        Else
                            ''  if Allergies then Skip Add-Update
                            'GoTo LINE1
                            HistoryID = 2 'Skip the Record
                        End If
                    Else
                        ''If not found any record then Add (Not Update)
                        HistoryID = 0
                    End If
                End If

                If HistoryID <= 1 Then

                    sqlParam = cmd.Parameters.AddWithValue("@HistoryID", HistoryID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.AddWithValue("@VisitID", oPatientHistory.VisitID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.HistoryCategory  ''HistoryTable.HistoryCategory

                    sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.HistoryItem   ''HistoryItem

                    sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Comments   '' Rows(i)(2) ''Comments

                    If Not IsNothing(oPatientHistory.Reaction) Then
                        If IsNothing(oPatientHistory.Status) Then
                            oPatientHistory.Status = "Active"
                        End If
                        sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                    Else
                        sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = "|Active"
                    End If

                    If Not IsNothing(oPatientHistory.DrugID) Then
                        sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.BigInt)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DrugID   '' DrugID
                    Else
                        sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.InputOutput
                    sqlParam.Value = _TempHistoryID  '' c

                    sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                    sqlParam = cmd.Parameters.Add("@nmedicalconditionid", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Medicalconditionid  '' DrugID

                    'For Deormalization of History table
                    'DrugName
                    If Not IsNothing(oPatientHistory.DrugName) Then
                        sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DrugName
                    Else
                        sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If

                    'DrugDosage
                    If Not IsNothing(oPatientHistory.Dosage) Then
                        sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.Dosage
                    Else
                        sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If


                    'NDCCode
                    If Not IsNothing(oPatientHistory.NDCCode) Then
                        sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.NDCCode
                    Else
                        sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If

                    'nDDID
                    sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.mpid
                    'For Deormalization of History table

                    If Not IsNothing(oPatientHistory.DOEAllergy) Then
                        sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DOEAllergy
                    End If

                    If Not IsNothing(oPatientHistory.ConceptId) Then
                        sqlParam = cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ConceptId
                    End If

                    If Not IsNothing(oPatientHistory.DescId) Then
                        sqlParam = cmd.Parameters.Add("@DescID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DescId
                    End If


                    If Not IsNothing(oPatientHistory.SnoMedId) Then
                        sqlParam = cmd.Parameters.Add("@SnoMedID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.SnoMedId
                    End If

                    If Not IsNothing(oPatientHistory.SnoDescription) Then
                        sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.SnoDescription
                    End If

                    If Not IsNothing(oPatientHistory.ICD9) Then
                        sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ICD9
                    End If

                    sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If _userName <> "" Then
                        sqlParam.Value = _userName
                    Else
                        sqlParam.Value = "admin"
                    End If




                    sqlParam = cmd.Parameters.Add("@nRowOrder", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = k



                    Try
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.ExecuteNonQuery()

                        _TempHistoryID = cmd.Parameters("@TempHistoryID").Value


                        If oPatientHistory.Reaction <> "" Then
                            Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                            objCCDDatabaseLayer.UpdateCategoryMaster(oPatientHistory.Reaction, "Reaction", PatientID)
                            If Not IsNothing(objCCDDatabaseLayer) Then
                                objCCDDatabaseLayer.Dispose()
                                objCCDDatabaseLayer = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try
                    cmd.Parameters.Clear()
                    If IsNothing(sqlParam) = False Then
                        sqlParam = Nothing
                    End If
                End If
                k = k + 1
            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            'If IsNothing(conn) = False Then SLR: Don't free here since it is used in many places
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function
    Public Function SaveQRDAPatientHistory(ByVal patientid As Long, ByVal oglopatienthistorycol As gloPatientHistoryCol, ByVal username As String)

        Dim HistoryID As Long = 0
        Dim cmd As SqlCommand = Nothing
        Try


            Dim k As Int64 = 1
            Dim _TempHistoryID As Long = 0

            cmd = New SqlCommand("gsp_AddHistory", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            For Each oPatientHistory As gloPatientHistory In oglopatienthistorycol

                '' Reset
                HistoryID = 0

                '''' Check for History_mst for History Item, if not available,insert Data
                Dim Category_id As Int64 = GetCategoryID(oPatientHistory.HistoryCategory, "History")
                If VerifyHistoryItemAvailability(oPatientHistory.HistoryItem, Category_id) = False Then
                    AddHistoryItemData(oPatientHistory.HistoryItem, "", Category_id)
                End If
                ''Added by Mayuri -Discussed with Saket on 20130227-we should save data against current visit
                Try
                    If IsNothing(oPatientHistory.OnsetDate) Or oPatientHistory.OnsetDate = "" Then
                        oPatientHistory.OnsetDate = DateTime.Now()
                    End If

                    oPatientHistory.VisitID = GenerateVisitID(Format(Convert.ToDateTime(oPatientHistory.OnsetDate), "MM/dd/yyyy"), patientid)

                Catch ex As Exception

                End Try

                '''' If Modify then All Recordes are Deleted 
                '''' there No Need to Check Duplicate Data 


                If HistoryID <= 1 Then

                    sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.InputOutput
                    sqlParam.Value = _TempHistoryID

                    sqlParam = cmd.Parameters.AddWithValue("@HistoryID", HistoryID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.AddWithValue("@VisitID", oPatientHistory.VisitID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.AddWithValue("@PatientID", patientid)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.HistoryCategory  ''HistoryTable.HistoryCategory

                    sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.HistoryItem   ''HistoryItem

                    sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Comments   '' Rows(i)(2) ''Comments

                    If Not IsNothing(oPatientHistory.Reaction) Then
                        If IsNothing(oPatientHistory.Status) Then
                            oPatientHistory.Status = "Active"
                        End If
                        sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                    Else
                        sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = "|Active"
                    End If

                    If Not IsNothing(oPatientHistory.DrugID) Then
                        sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.BigInt)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DrugID   '' DrugID
                    Else
                        sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If



                    sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                    sqlParam = cmd.Parameters.Add("@nmedicalconditionid", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Medicalconditionid  '' DrugID

                    'For Deormalization of History table
                    'DrugName
                    If Not IsNothing(oPatientHistory.DrugName) Then
                        sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DrugName
                    Else
                        sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If

                    'DrugDosage
                    If Not IsNothing(oPatientHistory.Dosage) Then
                        sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.Dosage
                    Else
                        sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If


                    'NDCCode
                    If Not IsNothing(oPatientHistory.NDCCode) Then
                        sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.NDCCode
                    Else
                        sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If


                    sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.mpid
                    'For Deormalization of History table



                    If Not IsNothing(oPatientHistory.ConceptId) Then
                        sqlParam = cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ConceptId
                    End If

                    If Not IsNothing(oPatientHistory.DescId) Then
                        sqlParam = cmd.Parameters.Add("@DescID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.DescId
                    End If


                    If Not IsNothing(oPatientHistory.SnoMedId) Then
                        sqlParam = cmd.Parameters.Add("@SnoMedID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.SnoMedId
                    End If

                    If Not IsNothing(oPatientHistory.SnoDescription) Then
                        sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.SnoDescription
                    End If

                    If Not IsNothing(oPatientHistory.ICD9) Then
                        sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ICD9
                    End If
                    sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    'sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.OnsetDate), "MM/dd/yyyy")
                    sqlParam.Value = oPatientHistory.OnsetDate
                    'oPatientHistory.DOEAllergy = Format(Convert.ToDateTime(oPatientHistory.OnsetDate), "MM/dd/yyyy")
                    'If Not IsNothing(oPatientHistory.DOEAllergy) Then
                    '    sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.VarChar)
                    '    sqlParam.Direction = ParameterDirection.Input
                    '    sqlParam.Value = oPatientHistory.DOEAllergy
                    'End If
                    sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(oPatientHistory.DOEAllergy) AndAlso oPatientHistory.DOEAllergy <> "" Then
                        sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.DOEAllergy), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = DateTime.Now
                    End If


                    sqlParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.CPT

                    sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If username <> "" Then
                        sqlParam.Value = username
                    Else
                        sqlParam.Value = "admin"
                    End If



                    sqlParam = cmd.Parameters.Add("@nRowOrder", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = k

                    If Not IsNothing(oPatientHistory.ReasonConceptId) Then
                        sqlParam = cmd.Parameters.Add("@ReasonConceptID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ReasonConceptId
                    End If
                    If Not IsNothing(oPatientHistory.ReasonDesc) Then
                        sqlParam = cmd.Parameters.Add("@ReasonDesc", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = oPatientHistory.ReasonDesc
                    End If
                    If Not IsNothing(oPatientHistory.ICDRevision) Then
                        If oPatientHistory.ICDRevision <> 0 Then
                            sqlParam = cmd.Parameters.Add("@ICDRevision", SqlDbType.SmallInt)
                            sqlParam.Direction = ParameterDirection.Input
                            sqlParam.Value = oPatientHistory.ICDRevision
                        End If

                    End If
                    If Not IsNothing(oPatientHistory.ValueSetOID) Then
                        If Not IsNothing(oPatientHistory.ValueSetOID) Then
                            sqlParam = cmd.Parameters.Add("@sValuesetOID", SqlDbType.VarChar)
                            sqlParam.Direction = ParameterDirection.Input
                            sqlParam.Value = oPatientHistory.ValueSetOID
                        End If

                    End If
                    If Not IsNothing(oPatientHistory.ValueSet) Then
                        If Not IsNothing(oPatientHistory.ValueSet) Then
                            sqlParam = cmd.Parameters.Add("@sValueSetName", SqlDbType.VarChar)
                            sqlParam.Direction = ParameterDirection.Input
                            sqlParam.Value = oPatientHistory.ValueSet
                        End If

                    End If

                    sqlParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(oPatientHistory.ConcernEndDate) Then
                        sqlParam.Value = oPatientHistory.ConcernEndDate
                    End If

                    sqlParam = cmd.Parameters.Add("@sLoincCode", SqlDbType.NVarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(oPatientHistory.Loinccode) Then
                        sqlParam.Value = oPatientHistory.Loinccode
                    End If

                    Try
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        cmd.ExecuteNonQuery()

                        _TempHistoryID = cmd.Parameters("@TempHistoryID").Value


                        If oPatientHistory.Reaction <> "" Then
                            Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                            objCCDDatabaseLayer.UpdateCategoryMaster(oPatientHistory.Reaction, "Reaction", patientid)
                            If Not IsNothing(objCCDDatabaseLayer) Then
                                objCCDDatabaseLayer.Dispose()
                                objCCDDatabaseLayer = Nothing
                            End If
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try
                    cmd.Parameters.Clear()
                    If IsNothing(sqlParam) = False Then
                        sqlParam = Nothing
                    End If
                End If
                k = k + 1
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            'If IsNothing(conn) = False Then SLR: Don't free here since it is used in many places
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        Return True
    End Function
    Public Function CheckDuplicate_New(ByVal VisitID As Long, ByVal HistoryCategory As String, ByVal HistoryItem As String) As Boolean
        Dim cmd1 As SqlCommand = Nothing
        Dim sqlParam1 As SqlParameter = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd1 = New SqlCommand("gsp_CheckPatientHistory", conn)
            cmd1.CommandType = CommandType.StoredProcedure



            sqlParam1 = cmd1.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = VisitID

            sqlParam1 = cmd1.Parameters.AddWithValue("@HistoryCategory", SqlDbType.VarChar)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = HistoryCategory

            sqlParam1 = cmd1.Parameters.AddWithValue("@HistoryItem", SqlDbType.VarChar)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = HistoryItem

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            ''''''''DONT CLOSE CONNECTION HERE
            '''''' IT IS HANDLED IN AddNewHistory Function

            Dim rowAffected As Long
            rowAffected = CType(cmd1.ExecuteScalar, Long)

            If rowAffected > 0 Then
                Return True     ' Duplicate Exists
            Else
                Return False    ' Duplicate Not Exists

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(sqlParam1) Then
                sqlParam1 = Nothing
            End If
            If Not IsNothing(cmd1) Then
                cmd1.Parameters.Clear()
                cmd1.Dispose()
                cmd1 = Nothing
            End If
        End Try
    End Function

    'Code Start: Added By Rohit On 20101012
    Public Function SaveProblemList(ByVal PatientID As Long, ByVal ogloProblemCol As ProblemsCol, ByVal _UserID As Long, ByVal _UserName As String, ByVal _ProviderName As String) As Boolean
        ' Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        '  conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Try
            For Each ogloProblem As Problems In ogloProblemCol
                Dim ExamParam As SqlParameter

                ogloProblem.EncounterId = GenerateVisitID(DateTime.Now, PatientID)

                'If ogloProblem.Condition.ToString.Trim() = "" Then
                'if chief Complaints are not inserted then delete that problem list
                'cmd = New SqlCommand("gsp_DeleteProblemList", conn)
                'cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trProbList

                'ExamParam = cmd.Parameters.AddWithValue("@ProblemID", 0) 'lst.ID)
                'ExamParam.Direction = ParameterDirection.Input

                'If conn.State = ConnectionState.Closed Then
                '    conn.Open()
                'End If
                'If cmd.ExecuteNonQuery() > 0 Then

                'End If
                'Else
                'Insert problem List
                cmd = New SqlCommand("gsp_InUpProblemList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trProbList

                ExamParam = cmd.Parameters.AddWithValue("@PatientID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = PatientID

                ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.EncounterId

                ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.DateOfService), "MM/dd/yyyy")

                ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ICD9Code) Then
                    ExamParam.Value = ogloProblem.ICD9Code
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ICD9) Then
                    ExamParam.Value = ogloProblem.ICD9
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.Condition) AndAlso ogloProblem.Condition.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.Condition
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.ProblemStatus

                ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _UserID

                ExamParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                ExamParam.Value = 0

                ExamParam = cmd.Parameters.Add("@RsDt", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = DBNull.Value

                ExamParam = cmd.Parameters.Add("@RsComment", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.Immediacy
                'ogloProblem.ProblemType 
                'If lst.Immediacy <> Nothing Or lst.Immediacy.ToString().Trim() <> "" Then
                '    ExamParam.Value = lst.Immediacy
                'Else
                '    ExamParam.Value = 3
                'End If

                ExamParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sProvider", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _ProviderName

                ExamParam = cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""
                ' ogloProblem.StreetAddress
                'If ogloProblem.StreetAddress <> Nothing Or ogloProblem.StreetAddress.ToString().Trim() <> "" Then
                '    ExamParam.Value = ogloProblem.StreetAddress
                'Else
                '    
                'End If

                ExamParam = cmd.Parameters.Add("@dtModifiedDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = DBNull.Value
                'ExamParam.Value = lst.LastModified
                'If ogloProblem.EncounterDate <> Nothing Or ogloProblem.EncounterDate.ToString().Trim() <> "" Then
                '    If Not lst.LastModified.ToString().Contains("1/1/0001") Then
                '        ExamParam.Value = lst.LastModified.ToString()
                '    End If
                'End If

                ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _UserName

                ExamParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConceptID) AndAlso ogloProblem.ConceptID.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ConceptID
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sTransactionID1", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""


                ExamParam = cmd.Parameters.Add("@sReasonConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ReasonConceptID) AndAlso ogloProblem.ReasonConceptID.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ReasonConceptID
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@sReasonConceptDesc", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ReasonDesc) AndAlso ogloProblem.ReasonDesc.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ReasonDesc
                Else
                    ExamParam.Value = ""
                End If


                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If
                '   End If
            Next
            Return True
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            'If IsNothing(conn) = False Then 'SLR: Don't free it here since it is used else wehere
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function
    Public Function SaveQRDAProblemList(ByVal PatientID As Long, ByVal ogloProblemCol As ProblemsCol, ByVal _UserID As Long, ByVal _UserName As String, ByVal _ProviderName As String) As Boolean
        ' Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        '  conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Try
            For Each ogloProblem As Problems In ogloProblemCol
                Dim ExamParam As SqlParameter
                Try
                    ogloProblem.EncounterId = GenerateVisitID(ogloProblem.DateOfService, PatientID)
                Catch ex As Exception

                End Try


                'Insert problem List
                cmd = New SqlCommand("gsp_InUpProblemList", conn)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trProbList

                ExamParam = cmd.Parameters.AddWithValue("@PatientID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = PatientID

                ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.EncounterId

                ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.DateOfService



                ExamParam = cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.ICDRevision

                If (ogloProblem.ICDRevision = 9) Then
                    ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                    ExamParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloProblem.ICD9Code) Then
                        ExamParam.Value = ogloProblem.ICD9Code
                    Else
                        ExamParam.Value = ""
                    End If
                Else
                    ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                    ExamParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloProblem.ICD10Code) Then
                        ExamParam.Value = ogloProblem.ICD10Code
                    Else
                        ExamParam.Value = ""
                    End If

                End If



                ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ICD9) Then
                    ExamParam.Value = ogloProblem.ICD9
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.Condition) AndAlso ogloProblem.Condition.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.Condition
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.ProblemStatus

                ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _UserID

                ExamParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                ExamParam.Value = 0

                ExamParam = cmd.Parameters.Add("@RsDt", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If ogloProblem.ProblemStatus = Problems.Status.Resolved Then
                    ExamParam.Value = ogloProblem.ResolvedDate
                Else
                    ExamParam.Value = DBNull.Value
                End If

                ExamParam = cmd.Parameters.Add("@dtDischargeDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If ogloProblem.DischargeDate <> "" Then
                    ExamParam.Value = ogloProblem.DischargeDate
                Else
                    ExamParam.Value = DBNull.Value
                End If


                'ExamParam = cmd.Parameters.Add("@RsDt", SqlDbType.DateTime)
                'ExamParam.Direction = ParameterDirection.Input
                'ExamParam.Value = DBNull.Value

                ExamParam = cmd.Parameters.Add("@RsComment", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.Immediacy
                'ogloProblem.ProblemType 
                'If lst.Immediacy <> Nothing Or lst.Immediacy.ToString().Trim() <> "" Then
                '    ExamParam.Value = lst.Immediacy
                'Else
                '    ExamParam.Value = 3
                'End If

                ExamParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sProvider", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _ProviderName

                ExamParam = cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""
                ' ogloProblem.StreetAddress
                'If ogloProblem.StreetAddress <> Nothing Or ogloProblem.StreetAddress.ToString().Trim() <> "" Then
                '    ExamParam.Value = ogloProblem.StreetAddress
                'Else
                '    
                'End If

                ExamParam = cmd.Parameters.Add("@dtModifiedDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = DBNull.Value
                'ExamParam.Value = lst.LastModified
                'If ogloProblem.EncounterDate <> Nothing Or ogloProblem.EncounterDate.ToString().Trim() <> "" Then
                '    If Not lst.LastModified.ToString().Contains("1/1/0001") Then
                '        ExamParam.Value = lst.LastModified.ToString()
                '    End If
                'End If

                ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = _UserName

                ExamParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConceptID) AndAlso ogloProblem.ConceptID.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ConceptID
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sTransactionID1", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.Condition

                ExamParam = cmd.Parameters.Add("@sReasonConceptID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ReasonConceptID) AndAlso ogloProblem.ReasonConceptID.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ReasonConceptID
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@sReasonConceptDesc", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ReasonDesc) AndAlso ogloProblem.ReasonDesc.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.ReasonDesc
                Else
                    ExamParam.Value = ""
                End If
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If
                '   End If
            Next
            Return True
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            'If IsNothing(conn) = False Then 'SLR: Don't free it here since it is used else wehere
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function
    Public Function SaveQRDAPatientEncounters(ByVal patientid As Long, ByVal encounters As EncountersCol, ByVal userid As Long, ByVal providerid As Long)
        Dim dttemplate As DataTable = Nothing
        Dim dticd As DataTable = Nothing
        Dim cmd As SqlCommand = Nothing
        Try

            dttemplate = getTemplate()
            dticd = getICD()

            Dim examid As Long = 0
            Dim visitid As Long = 0
            If IsNothing(conn) = False Then 'SLR:  free it here since it is used else wehere
                conn.Dispose()
                conn = Nothing
            End If
            conn = New SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString)
            'If conn.State = ConnectionState.Closed Then
            '    conn.Open()
            'End If
            For Each encounter As Encounters In encounters
                Dim ExamParam As SqlParameter = Nothing 'New SqlParameter()
                Dim sqlParam As SqlParameter = Nothing 'New SqlParameter()
                Try
                    visitid = GenerateVisitID(encounter.DateOfService, patientid)
                Catch ex As Exception

                End Try



                'Insert Exam
                cmd = New SqlCommand("gsp_InUpExam", conn)
                cmd.CommandType = CommandType.StoredProcedure

                ExamParam = cmd.Parameters.AddWithValue("@ExamID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                examid = 0
                ExamParam.Value = examid
                sqlParam = cmd.Parameters.AddWithValue("@VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = visitid
                sqlParam = cmd.Parameters.AddWithValue("@PatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = patientid

                If dttemplate.Rows.Count > 0 Then
                    sqlParam = cmd.Parameters.AddWithValue("@TemplateName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Convert.ToString(dttemplate.Rows(0)("stemplatename"))
                    sqlParam = cmd.Parameters.AddWithValue("@PatientNotes", SqlDbType.Image)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = dttemplate.Rows(0)("sdescription")
                End If
                If dticd.Rows.Count > 0 Then
                    sqlParam = cmd.Parameters.AddWithValue("@ExamName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Convert.ToString(dticd.Rows(0)("sicd9code") & " " & dticd.Rows(0)("sdescription"))
                End If

                sqlParam = cmd.Parameters.AddWithValue("@Finished", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "0"
                sqlParam = cmd.Parameters.AddWithValue("@dtDOS", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = encounter.DateOfService
                sqlParam = cmd.Parameters.AddWithValue("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)
                sqlParam = cmd.Parameters.AddWithValue("@ReviewerID", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0
                sqlParam = cmd.Parameters.AddWithValue("@IsReviewed", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.AddWithValue("@ReviewDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.AddWithValue("@CopyExamID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0
                sqlParam = cmd.Parameters.AddWithValue("@UserID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1
                sqlParam = cmd.Parameters.AddWithValue("@TemplateSpecility", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.AddWithValue("@PatientSeen", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1
                sqlParam = cmd.Parameters.AddWithValue("@OfficeVisit", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1
                sqlParam = cmd.Parameters.AddWithValue("@Encounter", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1
                sqlParam = cmd.Parameters.AddWithValue("@PatientStatus", SqlDbType.SmallInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1
                sqlParam = cmd.Parameters.AddWithValue("@nProviderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = providerid
                sqlParam = cmd.Parameters.AddWithValue("@nCaseID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0
                sqlParam = cmd.Parameters.AddWithValue("@dtExamCreated", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = encounter.DateOfExamCreated
                sqlParam = cmd.Parameters.AddWithValue("@dtDischargeDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = encounter.DischargeDate
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                conn.Close()
                examid = ExamParam.Value
                If (Not IsNothing(encounter.EncounterCode)) Then
                    SaveExamICD9CPT(examid, visitid, patientid, Convert.ToString(dticd.Rows(0)("sicd9code")), Convert.ToString(dticd.Rows(0)("sdescription")), Convert.ToInt64(dticd.Rows(0)("nicdrevision")), encounter.EncounterCode, encounter.EncounterName, encounter.SnomedCode, encounter.SnomedCodeDeSc)
                Else
                    SaveExamICD9CPT(examid, visitid, patientid, Convert.ToString(dticd.Rows(0)("sicd9code")), Convert.ToString(dticd.Rows(0)("sdescription")), Convert.ToInt64(dticd.Rows(0)("nicdrevision")), encounter.HcpcsCode, encounter.EncounterName, encounter.SnomedCode, encounter.SnomedCodeDeSc)

                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If

            Next
            Return True
        Catch ex As Exception
            Return False
        Finally
            If Not IsNothing(dttemplate) Then
                dttemplate.Dispose()
                dttemplate = Nothing
            End If
            If Not IsNothing(dticd) Then
                dticd.Dispose()
                dticd = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function SaveExamICD9CPT(ByVal examid As Long, ByVal visitid As Long, ByVal patientid As Long, ByVal icd9code As String, ByVal icddesc As String, ByRef icdrevision As Long, ByVal cptcode As String, ByVal cptdesc As String, ByVal snomedcode As String, ByVal snomeddesc As String)
        Try
            Dim cmd As SqlCommand = Nothing
            If IsNothing(conn) = False Then 'SLR:  free it here since it is used else wehere
                conn.Dispose()
                conn = Nothing
            End If

            conn = New SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd = New SqlCommand("gsp_InsertExamICD9CPTModifier", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim ExamParam As SqlParameter
            ExamParam = cmd.Parameters.AddWithValue("@nPatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = patientid
            ExamParam = cmd.Parameters.AddWithValue("@nExamId", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = examid
            ExamParam = cmd.Parameters.AddWithValue("@nVisitId", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = visitid
            ExamParam = cmd.Parameters.AddWithValue("@ICD9Code", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(icd9code) Then
                ExamParam.Value = icd9code
            Else
                ExamParam.Value = ""
            End If


            ExamParam = cmd.Parameters.AddWithValue("@ICD9Desc", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input

            If Not IsNothing(icddesc) Then
                ExamParam.Value = icddesc
            Else
                ExamParam.Value = ""
            End If


            ExamParam = cmd.Parameters.AddWithValue("@CPTcode", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(cptcode) Then
                ExamParam.Value = cptcode
            Else
                ExamParam.Value = ""
            End If


            ExamParam = cmd.Parameters.AddWithValue("@CPTDesc", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(cptdesc) Then
                ExamParam.Value = cptdesc
            Else
                ExamParam.Value = ""
            End If


            ExamParam = cmd.Parameters.AddWithValue("@ModCode", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ""
            ExamParam = cmd.Parameters.AddWithValue("@ModDesc", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ""
            ExamParam = cmd.Parameters.AddWithValue("@Unit", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 1
            ExamParam = cmd.Parameters.AddWithValue("@LineNo", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 1
            ExamParam = cmd.Parameters.AddWithValue("@SnomedCT", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            If Not IsNothing(snomedcode) Then
                ExamParam.Value = snomedcode
            Else
                ExamParam.Value = ""
            End If


            ExamParam = cmd.Parameters.AddWithValue("@SnomedDesc", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input

            If Not IsNothing(snomeddesc) Then
                ExamParam.Value = snomeddesc
            Else
                ExamParam.Value = ""
            End If

            ExamParam = cmd.Parameters.AddWithValue("@nICDRevision", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = icdrevision
            ExamParam = cmd.Parameters.AddWithValue("@bIsSnoMedOneToOneMapping", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = 1
            ExamParam = cmd.Parameters.AddWithValue("@TimedTherapy", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ""
            ExamParam = cmd.Parameters.AddWithValue("@UnTimedTherapy", SqlDbType.NVarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ""
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            If Not IsNothing(conn) Then ''connection state close
                If (conn.State = ConnectionState.Open) Then
                    conn.Close()
                End If
            End If
        End Try

    End Function
    Public Function getTemplate() As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "SELECT top(1) stemplatename,sdescription from TemplateGallery_MST "


            oDB.Retrive_Query(_sqlQuery, dt)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            _sqlQuery = Nothing
        End Try
        Return dt
    End Function
    Public Function GetAttributeList(ByVal ReasonValueSet As String) As String
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        'Dim dtTable As New DataTable
        Dim _ValueSetOID As String = ""
        Dim osqlpara As SqlParameter = Nothing
        Try
            objCon.ConnectionString = gloLibCCDGeneral.Connectionstring
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckAttribute"
            objCmd.Connection = objCon

            objCon.Open()
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@sReasonvaluset"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ReasonValueSet
            objCmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            '   Dim objDA As New SqlDataAdapter(objCmd)
            _ValueSetOID = objCmd.ExecuteScalar()
            ' objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            ' objDA.Dispose() : objDA = Nothing

            Return _ValueSetOID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return _ValueSetOID
        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If

            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If

            'If IsNothing(dtTable) = False Then
            '    dtTable.Dispose() : dtTable = Nothing
            'End If

        End Try
        Return _ValueSetOID
    End Function
    Public Function getICD() As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim _sqlQuery As String = Nothing
        Try
            oDB.Connect(False)
            _sqlQuery = "select top(1) sicd9code,sdescription,nicdrevision from icd9 where nicdrevision=9"


            oDB.Retrive_Query(_sqlQuery, dt)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            _sqlQuery = Nothing
        End Try
        Return dt
    End Function
    Public Function GenerateVisitID(ByVal PatientID As Long) As Long
        'Dim con As SqlConnection = Nothing
        Dim cmdVisits As SqlCommand = Nothing
        Dim objParamFlag As SqlParameter = Nothing
        Dim nVisitID As Long
        Dim objParam As SqlParameter = Nothing
        Try
            'con = New SqlConnection()
            Using con As New SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString)
                cmdVisits = New SqlCommand("gsp_InsertVisits", con)
                cmdVisits.CommandType = CommandType.StoredProcedure

                objParam = cmdVisits.Parameters.AddWithValue("@nPatientID", PatientID)

                objParam.Direction = ParameterDirection.Input

                objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = Now

                'Retrieve Appointment ID
                Dim nAppointmentID As Long
                nAppointmentID = 0

                objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = nAppointmentID

                objParamFlag = cmdVisits.Parameters.Add("@Flag", SqlDbType.Int)
                objParamFlag.Direction = ParameterDirection.Output

                objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Output
                objParam.Value = 0

                con.Open()
                cmdVisits.ExecuteNonQuery()
                con.Close()
                nVisitID = objParam.Value

                If objParamFlag.Value = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Visit, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Visit Added on " & CType(Now, String), PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                objParam = Nothing
                objParamFlag = Nothing


                cmdVisits.Parameters.Clear()
                cmdVisits.Dispose()
                cmdVisits = Nothing

                'con.Dispose()
                'con = Nothing
                Return nVisitID
            End Using


        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(con) Then
            '    If con.State = ConnectionState.Open Then
            '        con.Close()
            '    End If
            '    con.Dispose()
            '    con = Nothing
            'End If
        End Try
    End Function



    'Code End: Added By Rohit On 20101012

    'Code Start: Add By Rohit on 20101013
    Public Function AddNewVitals(ByVal PatientID As Long, ByVal ogloVitalCol As VitalsCol, ByVal _UserID As Long, ByVal _UserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim sqlParam As SqlParameter = Nothing

        Try

            For Each ogloVital As Vitals In ogloVitalCol
                'ogloVital.VitalDate = GenerateVisitID(ogloVital.VitalDate, PatientID)

                cmd = New SqlCommand("gsp_InUpVitals", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@nVitalID", SqlDbType.BigInt) 'nVitalID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                Try
                    sqlParam.Value = GenerateVisitID(ogloVital.VitalDate, PatientID) 'nVisitID
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@dtVitalDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Format(CDate(ogloVital.VitalDate), "MM/dd/yyyy")

                sqlParam = cmd.Parameters.Add("@sHeight", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dWeightinKg", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dWeightChange", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If dWeightChange = 0D Then
                '    sqlParam.Value = System.DBNull.Value
                'Else
                '    sqlParam.Value = dWeightChange
                'End If

                sqlParam = cmd.Parameters.Add("@dBMI", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If dBMI = 0D Then
                '    sqlParam.Value = System.DBNull.Value
                'Else
                '    sqlParam.Value = dBMI
                'End If

                sqlParam = cmd.Parameters.Add("@dWeightinlbs", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Weightinlbs) Then
                    sqlParam.Value = ogloVital.Weightinlbs
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dTemperature", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If Not IsNothing(ogloVital.Temperature) Then
                '    sqlParam.Value = ogloVital.Temperature
                'Else
                'End If

                sqlParam = cmd.Parameters.Add("@dRespiratoryRate", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.RespiratoryRate) Then
                    sqlParam.Value = ogloVital.RespiratoryRate
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dPulsePerMinute", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.PulsePerMinute) Then
                    sqlParam.Value = ogloVital.PulsePerMinute
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dPulseOx", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If dPulseOx = 0D Then
                'Else
                '    sqlParam.Value = dPulseOx
                'End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.BloodPressureSittingMin) Then
                    sqlParam.Value = ogloVital.BloodPressureSittingMin
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.BloodPressureSittingMax) Then
                    sqlParam.Value = ogloVital.BloodPressureSittingMax
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dBloodPressureStandingMin

                sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                '    sqlParam.Value = dBloodPressureStandingMax

                sqlParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Comment) Then
                    sqlParam.Value = ogloVital.Comment
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@dHeadCircumferance", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeadCircumferance

                sqlParam = cmd.Parameters.Add("@dStature", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dStature

                sqlParam = cmd.Parameters.Add("@dTHRperMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHRPerMax

                sqlParam = cmd.Parameters.Add("@dTHRMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHRMax

                sqlParam = cmd.Parameters.Add("@dTHRperMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHRPerMin

                sqlParam = cmd.Parameters.Add("@dTHRMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHRMin

                sqlParam = cmd.Parameters.Add("@dTHR", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHR

                ''Sudhir - 20090124 ''
                sqlParam = cmd.Parameters.Add("@dHeightinInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.HeightinInch) Then
                    sqlParam.Value = ogloVital.HeightinInch
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dHeightinCm", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeightinCm
                'End If

                sqlParam = cmd.Parameters.Add("@sWeightinLbsOz", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = sWeightinLbsOz

                sqlParam = cmd.Parameters.Add("@dTemperatureinCelcius", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Temperature) Then
                    sqlParam.Value = ogloVital.Temperature
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@nPainLevel", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                ' sqlParam.Value = nPainLevel

                sqlParam = cmd.Parameters.Add("@dPEFR1", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR1

                sqlParam = cmd.Parameters.Add("@dPEFR2", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR2

                sqlParam = cmd.Parameters.Add("@dPEFR3", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR3

                sqlParam = cmd.Parameters.Add("@dHeadCircuminInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeadCircuminInch

                sqlParam = cmd.Parameters.Add("@dStatureinInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dStatureinInch

                '' End Sudhir ''
                ''Added by Mayuri:20100608
                sqlParam = cmd.Parameters.Add("@sSiteForBloodPressure", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dtLastMenstrualPeriod", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dNeckCircuminCm", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dNeckCircuminCm

                sqlParam = cmd.Parameters.Add("@dNeckCircuminInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dNeckCircuminInch

                sqlParam = cmd.Parameters.Add("@dLeftEyePressure", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dLeftEyePressure

                sqlParam = cmd.Parameters.Add("@dRightEyePressure", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dRightEyePressure

                sqlParam = cmd.Parameters.Add("@bStatusLMPeriod", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                ''End
                ''

                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value



                sqlParam = cmd.Parameters.Add("@nPainLevelWithMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value



                sqlParam = cmd.Parameters.Add("@nPainLevelWithoutMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@nPainLevelWorst", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@nODIPercent", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                ''DAS 28
                sqlParam = cmd.Parameters.Add("@nDAS28", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
            Next
            Return True
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function
    Public Function AddQRDAVitals(ByVal PatientID As Long, ByVal ogloVitalCol As VitalsCol, ByVal _UserID As Long, ByVal _UserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing

        'conn = New SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim sqlParam As SqlParameter = Nothing

        Try

            For Each ogloVital As Vitals In ogloVitalCol
                'ogloVital.VitalDate = GenerateVisitID(ogloVital.VitalDate, PatientID)

                cmd = New SqlCommand("gsp_InUpVitals", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@nVitalID", SqlDbType.BigInt) 'nVitalID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                Try
                    sqlParam.Value = GenerateVisitID(Format(Convert.ToDateTime(ogloVital.VitalDate), "MM/dd/yyyy"), PatientID) 'nVisitID
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@dtVitalDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloVital.VitalDate

                sqlParam = cmd.Parameters.Add("@sHeight", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dWeightinKg", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dWeightChange", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If dWeightChange = 0D Then
                '    sqlParam.Value = System.DBNull.Value
                'Else
                '    sqlParam.Value = dWeightChange
                'End If

                sqlParam = cmd.Parameters.Add("@dBMI", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.BMI) Then
                    sqlParam.Value = ogloVital.BMI
                Else
                    sqlParam.Value = System.DBNull.Value
                End If


                'If dBMI = 0D Then
                '    sqlParam.Value = System.DBNull.Value
                'Else
                '    sqlParam.Value = dBMI
                'End If

                sqlParam = cmd.Parameters.Add("@dWeightinlbs", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Weightinlbs) Then
                    sqlParam.Value = ogloVital.Weightinlbs
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dTemperature", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If Not IsNothing(ogloVital.Temperature) Then
                '    sqlParam.Value = ogloVital.Temperature
                'Else
                'End If

                sqlParam = cmd.Parameters.Add("@dRespiratoryRate", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.RespiratoryRate) Then
                    sqlParam.Value = ogloVital.RespiratoryRate
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dPulsePerMinute", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.PulsePerMinute) Then
                    sqlParam.Value = ogloVital.PulsePerMinute
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dPulseOx", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'If dPulseOx = 0D Then
                'Else
                '    sqlParam.Value = dPulseOx
                'End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.BloodPressureSittingMin) Then
                    sqlParam.Value = ogloVital.BloodPressureSittingMin
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.BloodPressureSittingMax) Then
                    sqlParam.Value = ogloVital.BloodPressureSittingMax
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dBloodPressureStandingMin

                sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                '    sqlParam.Value = dBloodPressureStandingMax

                sqlParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Comment) Then
                    sqlParam.Value = ogloVital.Comment
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@dHeadCircumferance", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeadCircumferance

                sqlParam = cmd.Parameters.Add("@dStature", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dStature

                sqlParam = cmd.Parameters.Add("@dTHRperMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloVital.THRperMax
                'sqlParam.Value = dTHRPerMax

                sqlParam = cmd.Parameters.Add("@dTHRMax", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloVital.THRMax

                'sqlParam.Value = dTHRMax

                sqlParam = cmd.Parameters.Add("@dTHRperMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloVital.THRperMin
                'sqlParam.Value = dTHRPerMin

                sqlParam = cmd.Parameters.Add("@dTHRMin", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloVital.THRMin
                'sqlParam.Value = dTHRMin

                sqlParam = cmd.Parameters.Add("@dTHR", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dTHR

                ''Sudhir - 20090124 ''
                sqlParam = cmd.Parameters.Add("@dHeightinInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.HeightinInch) Then
                    sqlParam.Value = ogloVital.HeightinInch
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@dHeightinCm", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeightinCm
                'End If

                sqlParam = cmd.Parameters.Add("@sWeightinLbsOz", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = sWeightinLbsOz

                sqlParam = cmd.Parameters.Add("@dTemperatureinCelcius", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloVital.Temperature) Then
                    sqlParam.Value = ogloVital.Temperature
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@nPainLevel", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                ' sqlParam.Value = nPainLevel

                sqlParam = cmd.Parameters.Add("@dPEFR1", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR1

                sqlParam = cmd.Parameters.Add("@dPEFR2", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR2

                sqlParam = cmd.Parameters.Add("@dPEFR3", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dPEFR3

                sqlParam = cmd.Parameters.Add("@dHeadCircuminInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dHeadCircuminInch

                sqlParam = cmd.Parameters.Add("@dStatureinInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dStatureinInch

                '' End Sudhir ''
                ''Added by Mayuri:20100608
                sqlParam = cmd.Parameters.Add("@sSiteForBloodPressure", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dtLastMenstrualPeriod", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dNeckCircuminCm", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dNeckCircuminCm

                sqlParam = cmd.Parameters.Add("@dNeckCircuminInch", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dNeckCircuminInch

                sqlParam = cmd.Parameters.Add("@dLeftEyePressure", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dLeftEyePressure

                sqlParam = cmd.Parameters.Add("@dRightEyePressure", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = dRightEyePressure

                sqlParam = cmd.Parameters.Add("@bStatusLMPeriod", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
                ''End
                ''

                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value



                sqlParam = cmd.Parameters.Add("@nPainLevelWithMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value



                sqlParam = cmd.Parameters.Add("@nPainLevelWithoutMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@nPainLevelWorst", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@nODIPercent", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                ''DAS 28
                sqlParam = cmd.Parameters.Add("@nDAS28", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dPulseOxSupplement", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dPulseRate", SqlDbType.Float)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                'sqlParam = cmd.Parameters.Add("@nTotalPregnancies", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value

                'sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value


                'sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value

                'sqlParam = cmd.Parameters.Add("@nLivingChildren", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value

                'sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value

                'sqlParam = cmd.Parameters.Add("@nMultipleBirths", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input

                'sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.SmallInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = System.DBNull.Value
                'sqlParam.Value = System.DBNull.Value

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
            Next
            Return True
        Catch ex As SqlException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Public Function SaveMedication(ByVal PatientID As Long, ByVal ogloMedicationCol As MedicationsCol, ByVal _UserID As Long, ByVal _UserName As String, Optional ByVal _isAccept As Boolean = False) As Boolean
        Dim cmd As SqlCommand = Nothing
        '  conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            For Each ogloMedication As Medication In ogloMedicationCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer

                cmd = New SqlCommand("gsp_InUpMedication", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@nMedicationID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                ''Added by Mayuri as per discussion with Saket on 20130227-to save records against current visit
                sqlParam.Value = GenerateVisitID(DateTime.Now, PatientID)
                ''sqlParam.Value = GenerateVisitID(ogloMedication.MedicationDate, PatientID)

                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@Rx_nProviderId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objgloCCDDatabaselayer.getDefaultProviderId()

                sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugName) Then
                    sqlParam.Value = ogloMedication.DrugName
                    If sqlParam.Value = "" Then
                        MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If IsNothing(cmd) = False Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If IsNothing(objgloCCDDatabaselayer) = False Then
                            objgloCCDDatabaselayer.Dispose()
                            objgloCCDDatabaselayer = Nothing
                        End If
                        Continue For
                    End If

                Else
                    MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                    If IsNothing(objgloCCDDatabaselayer) = False Then
                        objgloCCDDatabaselayer.Dispose()
                        objgloCCDDatabaselayer = Nothing
                    End If
                    Continue For
                End If

                sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                'If Not IsNothing(ogloMedication.DrugQuantity) Then
                '    sqlParam.Value = ogloMedication.DrugQuantity
                'Else
                '    sqlParam.Value = ""
                'End If
                If Not IsNothing(ogloMedication.RxNormCode) Then
                    sqlParam.Value = gloReconciliation.GetDosageForm(ogloMedication.RxNormCode)
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Route) Then
                    sqlParam.Value = ogloMedication.Route
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Frequency) Then
                    sqlParam.Value = ogloMedication.Frequency
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                'If Not IsNothing(ogloMedication.Frequency) Then
                '    sqlParam.Value = ogloMedication.Frequency
                'Else
                'End If

                sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = DateTime.Now
                'If Not IsNothing(ogloMedication.st) Then
                '    sqlParam.Value = ogloMedication.Frequency
                'Else
                '   
                'End If

                sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugStrength) Then
                    sqlParam.Value = ogloMedication.DrugStrength
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@dtMedicationDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.MedicationDate) Then
                    sqlParam.Value = Format(CDate(ogloMedication.MedicationDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DateTime.Now
                End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                'If Not IsNothing(ogloMedication.Status) Then
                '    sqlParam.Value = ogloMedication.Status
                'Else
                sqlParam.Value = ""
                'End If

                sqlParam = cmd.Parameters.Add("@sReason", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                'If Not IsNothing(ogloMedication.Ref) Then
                '    sqlParam.Value = ogloMedication.Status
                'Else
                'End If

                sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.mpid) Then
                    sqlParam.Value = ogloMedication.mpid
                Else
                    sqlParam.Value = 0
                End If

                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If _UserName = "" Then
                    sqlParam.Value = ogloMedication.User
                Else
                    sqlParam.Value = _UserName
                End If


                sqlParam = cmd.Parameters.Add("@nPrescriptionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@sRenewed", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                'sqlParam = cmd.Parameters.Add("@nMedicationId", SqlDbType.BigInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = 0
                If _isAccept = True Then
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloMedication.ProdCode) Then
                        sqlParam.Value = ogloMedication.ProdCode
                        If sqlParam.Value = "" Then
                            System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If IsNothing(cmd) = False Then
                                cmd.Parameters.Clear()
                                cmd.Dispose()
                                cmd = Nothing
                            End If
                            If IsNothing(objgloCCDDatabaselayer) = False Then
                                objgloCCDDatabaselayer.Dispose()
                                objgloCCDDatabaselayer = Nothing
                            End If
                            Continue For
                        End If
                    Else
                        System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Continue For
                    End If
                Else
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloMedication.RxNormCode) OrElse Not IsNothing(ogloMedication.ProdCode) Then
                        '  Dim oRxBusinesslayer As gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(PatientID)
                        'Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                        'sqlParam.Value = objgloCCDDatabaselayer.GetNDCCode(ogloMedication.RxNormCode)
                        Dim _result As gloGlobal.DIB.DrugInfo = Nothing

                        If Convert.ToString(ogloMedication.ProdCode) <> "" Then
                            Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                _result = oGSHelper.GetNdccodebyRxnorm(ogloMedication.ProdCode, 0)
                            End Using
                        End If
                        If Convert.ToString(ogloMedication.RxNormCode) <> "" Then
                            If _result Is Nothing OrElse IsNothing(_result.ndc) Then
                                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                    _result = oGSHelper.GetNdccodebyRxnorm(ogloMedication.RxNormCode, 1)
                                End Using
                            End If
                        End If
                        If Not IsNothing(_result) Then
                            If Not IsNothing(_result.ndc) Then
                                sqlParam.Value = _result.ndc.ToString()
                            End If
                        End If
                        If sqlParam.Value = "" Then
                            'System.Windows.Forms.MessageBox.Show("Since there is no NDC code found for " & ogloMedication.GenericName & " drug, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If IsNothing(cmd) = False Then
                                cmd.Parameters.Clear()
                                cmd.Dispose()
                                cmd = Nothing
                            End If
                            If IsNothing(objgloCCDDatabaselayer) = False Then
                                objgloCCDDatabaselayer.Dispose()
                                objgloCCDDatabaselayer = Nothing
                            End If

                            Continue For

                        End If
                    Else
                        'System.Windows.Forms.MessageBox.Show("Since there is no NDC code found for " & ogloMedication.GenericName & " drug, therefore this will be not added in medication", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If IsNothing(cmd) = False Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If IsNothing(objgloCCDDatabaselayer) = False Then
                            objgloCCDDatabaselayer.Dispose()
                            objgloCCDDatabaselayer = Nothing
                        End If
                        Continue For
                    End If

                End If


                sqlParam = cmd.Parameters.Add("@nIsNarcotic", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugForm) Then
                    sqlParam.Value = ogloMedication.DrugForm
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sStrengthUnit", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.StrengthUnits) Then
                    sqlParam.Value = ogloMedication.StrengthUnits
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sRefills", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Refills) Then
                    sqlParam.Value = ogloMedication.Refills
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sNotes", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sMethod", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_bMaySubstitute", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@Rx_nDrugID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0


                sqlParam = cmd.Parameters.Add("@Rx_blnflag", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@Rx_sLotNo", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_dtExpirationdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@Rx_sChiefComplaints", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.CheifComplaint) Then
                    sqlParam.Value = ogloMedication.CheifComplaint
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sRxReferenceNumber", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sRefillQualifier", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_nPharmacyId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@Rx_sNCPDPID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_nContactID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@Rx_sName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sAddressline1", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sAddressline2", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sCity", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sState", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sZip", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sEmail", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sFax", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sPhone", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sServiceLevel", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sPrescriberNotes", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatusMessage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sPBMSourceName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@RxMed_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If

            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If
            'If IsNothing(conn) = False Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
        End Try
    End Function
    Public Function SaveQRDAMedication(ByVal PatientID As Long, ByVal ogloMedicationCol As MedicationsCol, ByVal _UserID As Long, ByVal _UserName As String, Optional ByVal _isAccept As Boolean = False) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = GETNDCDatabase()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim mySettingsString As String = Convert.ToString(dt.Rows(i)("settingsname"))
                Dim mySettingsValue As String = Convert.ToString(dt.Rows(i)("settingsvalue"))
                If mySettingsString = "GLORXNSERVERNAME" Then
                    gloLibCCDGeneral.gRxNServerName = mySettingsValue
                ElseIf mySettingsString = "GLORXNDBNAME" Then
                    gloLibCCDGeneral.gRxNDatabaseName = mySettingsValue
                ElseIf mySettingsString = "GLOMMWSERVERNAME" Then
                    gloLibCCDGeneral.sMmwServerName = mySettingsValue
                ElseIf mySettingsString = "GLOMMWDBNAME" Then
                    gloLibCCDGeneral.sMmwDatabaseName = mySettingsValue
                ElseIf mySettingsString = "GLODIBURL" Then
                    gloLibCCDGeneral.sDIBServiceURL = mySettingsValue
                End If
            Next

        End If
        Dim _prescriptionid As Int64 = 0
        '  conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            For Each ogloMedication As Medication In ogloMedicationCol
                If ogloMedication.IsPrescription Then
                    _prescriptionid = 0
                    ' _prescriptionid = SaveQRDAPrescription(PatientID, ogloMedication, _UserID, _UserName, _isAccept)
                    ' Continue For

                    If ogloMedication.IsPrescription Then
                        _prescriptionid = SaveQRDAPrescription(PatientID, ogloMedication, _UserID, _UserName, _isAccept)
                        ogloMedication._PrescriptionId = _prescriptionid
                        'Continue For
                    End If
                End If
                objgloCCDDatabaselayer = New gloCCDDatabaseLayer

                Dim _result As gloGlobal.DIB.DrugInfo = Nothing

                If Convert.ToString(ogloMedication.RxNormCode) <> "" Then
                    Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                        _result = oGSHelper.GETQRDANDC(ogloMedication.RxNormCode, False, ogloMedication.Valueset)
                        If IsNothing(_result.ndc) = True Then

                            Dim dtRxNormCodes As DataTable

                            dtRxNormCodes = GetAlternateRxNormCodes(ogloMedication.RxNormCode)

                            If (IsNothing(dtRxNormCodes) = False) Then
                                For intCount As Integer = 0 To dtRxNormCodes.Rows.Count - 1
                                    ogloMedication.RxNormCode = dtRxNormCodes.Rows(intCount)("Code")
                                    _result = oGSHelper.GETQRDANDC(ogloMedication.RxNormCode, True, "")

                                    If IsNothing(_result.ndc) = False Then
                                        Exit For
                                    End If

                                Next
                                dtRxNormCodes.Dispose()
                                dtRxNormCodes = Nothing

                            End If
                        End If
                    End Using
                    If Not IsNothing(_result) Then
                        ogloMedication.DrugName = _result.dnm.ToString()
                        ogloMedication.GenericName = _result.gnm.ToString()
                        ogloMedication.DrugStrength = _result.dosage.ToString()
                        ogloMedication.Route = _result.rt
                        ogloMedication.mpid = _result.mp
                    Else
                        System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If IsNothing(cmd) = False Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If IsNothing(objgloCCDDatabaselayer) = False Then
                            objgloCCDDatabaselayer.Dispose()
                            objgloCCDDatabaselayer = Nothing
                        End If
                        Continue For
                    End If
                End If

                cmd = New SqlCommand("gsp_InUpMedication", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@nMedicationID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                ''Added by Mayuri as per discussion with Saket on 20130227-to save records against current visit
                Try
                    sqlParam.Value = GenerateVisitID(ogloMedication.StartDate, PatientID)
                Catch ex As Exception

                End Try

                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@Rx_nProviderId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objgloCCDDatabaselayer.getDefaultProviderId()

                sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Frequency) Then
                    sqlParam.Value = ogloMedication.Frequency
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                'If Not IsNothing(ogloMedication.Frequency) Then
                '    sqlParam.Value = ogloMedication.Frequency
                'Else
                'End If

                sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloMedication.StartDate
                'If Not IsNothing(ogloMedication.st) Then
                '    sqlParam.Value = ogloMedication.Frequency
                'Else
                '   
                'End If

                sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloMedication.EndDate

                sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugQuantity) Then
                    sqlParam.Value = ogloMedication.DrugQuantity
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@dtMedicationDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.MedicationDate) Then
                    sqlParam.Value = Format(CDate(ogloMedication.MedicationDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DateTime.Now
                End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                'If Not IsNothing(ogloMedication.Status) Then
                '    sqlParam.Value = ogloMedication.Status
                'Else
                sqlParam.Value = ""
                'End If

                sqlParam = cmd.Parameters.Add("@sReason", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If _UserName = "" Then
                    sqlParam.Value = ogloMedication.User
                Else
                    sqlParam.Value = _UserName
                End If


                sqlParam = cmd.Parameters.Add("@nPrescriptionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                If ogloMedication._PrescriptionId <> 0 Then
                    sqlParam.Value = ogloMedication._PrescriptionId
                Else
                    sqlParam.Value = 0
                End If

                sqlParam = cmd.Parameters.Add("@sRenewed", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _result.ndc

                sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloMedication.DrugName

                sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.mpid) Then
                    sqlParam.Value = ogloMedication.mpid
                Else
                    sqlParam.Value = 0
                End If

                sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugStrength) Then
                    sqlParam.Value = ogloMedication.DrugStrength
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Route) Then
                    sqlParam.Value = ogloMedication.Route
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@nIsNarcotic", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugForm) Then
                    sqlParam.Value = ogloMedication.DrugForm
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sStrengthUnit", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.StrengthUnits) Then
                    sqlParam.Value = ogloMedication.StrengthUnits
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sRefills", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Refills) Then
                    sqlParam.Value = ogloMedication.Refills
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sNotes", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sMethod", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_bMaySubstitute", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@Rx_nDrugID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0


                sqlParam = cmd.Parameters.Add("@Rx_blnflag", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@Rx_sLotNo", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_dtExpirationdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@Rx_sChiefComplaints", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.CheifComplaint) Then
                    sqlParam.Value = ogloMedication.CheifComplaint
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sRxReferenceNumber", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sRefillQualifier", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_nPharmacyId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@Rx_sNCPDPID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_nContactID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@Rx_sName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sAddressline1", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sAddressline2", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sCity", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sState", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sZip", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sEmail", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sFax", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sPhone", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sServiceLevel", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_sPrescriberNotes", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatusMessage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sPBMSourceName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@RxMed_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0


                sqlParam = cmd.Parameters.Add("@ReasonConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.ReasonConceptID) Then
                    sqlParam.Value = ogloMedication.ReasonConceptID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@ReasonConceptDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.ReasonConceptDesc) Then
                    sqlParam.Value = ogloMedication.ReasonConceptDesc
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@ValuesetOID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.ValuesetOID) Then
                    sqlParam.Value = ogloMedication.ValuesetOID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Valueset", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Valueset) Then
                    sqlParam.Value = ogloMedication.Valueset
                Else
                    sqlParam.Value = ""
                End If





                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If
            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()

            'If IsNothing(conn) = False Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function
    Private Function GetAlternateRxNormCodes(RxNormCode As String) As DataTable

        Dim cmdMain As SqlCommand = Nothing
        Dim daMain As SqlDataAdapter = Nothing
        Dim dtMain As DataTable = Nothing

        Try
            daMain = New SqlDataAdapter
            dtMain = New DataTable

            cmdMain = New SqlCommand("gsp_GetAlternateRxNormCodes", conn)
            cmdMain.Parameters.Add(New SqlParameter("@RxNormCode", RxNormCode))
            cmdMain.CommandType = CommandType.StoredProcedure

            daMain.SelectCommand = cmdMain
            daMain.Fill(dtMain)

            Return dtMain

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            Return Nothing

        Finally

            If IsNothing(cmdMain) = False Then
                cmdMain.Dispose()
                cmdMain = Nothing
            End If

            If IsNothing(daMain) = False Then
                daMain.Dispose()
                daMain = Nothing
            End If

        End Try


    End Function


    Public Function SaveQRDAPrescription(ByVal PatientID As Long, ByVal ogloMedication As Medication, ByVal _UserID As Long, ByVal _UserName As String, Optional ByVal _isAccept As Boolean = False) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = GETNDCDatabase()
        Dim _prescid As Int64 = 0
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim mySettingsString As String = Convert.ToString(dt.Rows(i)("settingsname"))
                Dim mySettingsValue As String = Convert.ToString(dt.Rows(i)("settingsvalue"))

                If mySettingsString = "GLORXNSERVERNAME" Then
                    gloLibCCDGeneral.gRxNServerName = mySettingsValue
                ElseIf mySettingsString = "GLORXNDBNAME" Then
                    gloLibCCDGeneral.gRxNDatabaseName = mySettingsValue
                ElseIf mySettingsString = "GLOMMWSERVERNAME" Then
                    gloLibCCDGeneral.sMmwServerName = mySettingsValue
                ElseIf mySettingsString = "GLOMMWDBNAME" Then
                    gloLibCCDGeneral.sMmwDatabaseName = mySettingsValue
                ElseIf mySettingsString = "GLODIBURL" Then
                    gloLibCCDGeneral.sDIBServiceURL = mySettingsValue
                End If
            Next

        End If

        '  conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            ' For Each ogloMedication As Medication In ogloMedicationCol

            objgloCCDDatabaselayer = New gloCCDDatabaseLayer

            Dim _result As gloGlobal.DIB.DrugInfo = Nothing

            If Convert.ToString(ogloMedication.RxNormCode) <> "" Then
                Using oGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                    _result = oGSHelper.GETQRDANDC(ogloMedication.RxNormCode, False, ogloMedication.Valueset)
                    If IsNothing(_result.ndc) = True Then

                        Dim dtRxNormCodes As DataTable

                        dtRxNormCodes = GetAlternateRxNormCodes(ogloMedication.RxNormCode)

                        If (IsNothing(dtRxNormCodes) = False) Then
                            For intCount As Integer = 0 To dtRxNormCodes.Rows.Count - 1
                                ogloMedication.RxNormCode = dtRxNormCodes.Rows(intCount)("Code")
                                _result = oGSHelper.GETQRDANDC(ogloMedication.RxNormCode, True, "")

                                If IsNothing(_result.ndc) = False Then
                                    Exit For
                                End If

                            Next
                            dtRxNormCodes.Dispose()
                            dtRxNormCodes = Nothing

                        End If
                    End If
                End Using
                If Not IsNothing(_result) Then
                    ogloMedication.DrugName = _result.dnm.ToString()
                    ogloMedication.GenericName = _result.gnm.ToString()
                    ogloMedication.DrugStrength = _result.dosage.ToString()
                    ogloMedication.Route = _result.rt
                    ogloMedication.mpid = _result.mp
                Else
                    System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                    If IsNothing(objgloCCDDatabaselayer) = False Then
                        objgloCCDDatabaselayer.Dispose()
                        objgloCCDDatabaselayer = Nothing
                    End If
                End If
            End If

            cmd = New SqlCommand("gsp_InUpPrescription_Mst", conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@sChiefComplaints", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.CheifComplaint) Then
                sqlParam.Value = ogloMedication.CheifComplaint
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            ''Added by Mayuri as per discussion with Saket on 20130227-to save records against current visit
            Try
                sqlParam.Value = GenerateVisitID(ogloMedication.StartDate, PatientID)
            Catch ex As Exception

            End Try

            sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _result.ndc

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            sqlParam = cmd.Parameters.Add("@nProviderId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objgloCCDDatabaselayer.getDefaultProviderId()

            sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.Frequency) Then
                sqlParam.Value = ogloMedication.Frequency
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""


            sqlParam = cmd.Parameters.Add("@bMaysubstitute", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ogloMedication.StartDate

            sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ogloMedication.EndDate

            sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.DrugQuantity) Then
                sqlParam.Value = ogloMedication.DrugQuantity
            Else
                sqlParam.Value = ""
            End If


            sqlParam = cmd.Parameters.Add("@dtPrescriptionDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.MedicationDate) Then
                sqlParam.Value = ogloMedication.MedicationDate
            Else
                sqlParam.Value = DateTime.Now
            End If

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

            sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""


            sqlParam = cmd.Parameters.Add("@sReason", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If _UserName = "" Then
                sqlParam.Value = ogloMedication.User
            Else
                sqlParam.Value = _UserName
            End If


            sqlParam = cmd.Parameters.Add("@nPrescriptionID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _result.dnm

            sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.mpid) Then
                sqlParam.Value = ogloMedication.mpid
            Else
                sqlParam.Value = 0
            End If

            sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.DrugStrength) Then
                sqlParam.Value = ogloMedication.DrugStrength
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.Route) Then
                sqlParam.Value = ogloMedication.Route
            Else
                sqlParam.Value = ""
            End If
            sqlParam = cmd.Parameters.Add("@nIsNarcotic", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.DrugForm) Then
                sqlParam.Value = ogloMedication.DrugForm
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@sStrengthUnit", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.StrengthUnits) Then
                sqlParam.Value = ogloMedication.StrengthUnits
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If Not IsNothing(ogloMedication.Refills) Then
                sqlParam.Value = ogloMedication.Refills
            Else
                sqlParam.Value = ""
            End If

            sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sMethod", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = False

            sqlParam = cmd.Parameters.Add("@nDrugID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@sLotNo", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@dtExpirationDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = System.DBNull.Value

            sqlParam = cmd.Parameters.Add("@sRefillQualifier", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@nPharmacyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0


            sqlParam = cmd.Parameters.Add("@nDDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0


            sqlParam = cmd.Parameters.Add("@sNCPDPID", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@nContactID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@sName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sAddressline1", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sAddressline2", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sCity", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sState", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sZip", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sEmail", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sFax", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sPhone", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sServiceLevel", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = cmd.Parameters.Add("@sPrescriberNotes", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            'sqlParam = cmd.Parameters.Add("@eRxStatus", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ""

            'sqlParam = cmd.Parameters.Add("@Rx_eRxStatusMessage", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ""

            'sqlParam = cmd.Parameters.Add("@sPBMSourceName", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ""

            'sqlParam = cmd.Parameters.Add("@RxMed_DMSID", SqlDbType.BigInt)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = 0

            If conn.State = ConnectionState.Closed Then conn.Open()
            'cmd.ExecuteNonQuery()
            If cmd.ExecuteNonQuery() > 0 Then
                _prescid = cmd.Parameters("@nPrescriptionId").Value
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If
            '  Next

            Return _prescid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return _prescid
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            'If IsNothing(conn) = False Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
        Return _prescid
    End Function
    Public Function GETNDCDatabase() As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Try
            oDB.Connect(False)


            oDB.Retrive("GETNDCDATABASEQRDA", dt)
        Catch ex As Exception
            Throw ex
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try
        Return dt

    End Function


    Public Function SaveImmunization_old(ByVal PatientID As Long, ByVal ogloImmunizationCol As ImmunizationCol, ByVal _UserID As Long, ByVal _UserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer
        Dim sqlParam As SqlParameter = Nothing
        Dim Trans_ID, ItemID As Int64 'Trans_ID,
        Dim cmd1 As SqlCommand = Nothing
        Try
            For Each ogloImmunization As Immunization In ogloImmunizationCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer

                'Call Function to get the Item_ID
                ItemID = getItemID(ogloImmunization)

                'Code Start to Store In master Table
                cmd = New SqlCommand("IM_InsUPIMTransRec", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@im_trn_mst_nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                Trans_ID = DirectCast(sqlParam.Value, Int64)
                'Code End to Store In master Table

                'Code Start to Store In Trans Table
                sqlParam = New SqlParameter()
                cmd = Nothing
                cmd = New SqlCommand("IM_InsUPIMTransDtl_SnoMed", conn)
                cmd.CommandType = CommandType.StoredProcedure


                sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Trans_ID

                sqlParam = cmd.Parameters.Add("@im_trn_Date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationDate) Then
                    sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DateTime.Now
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_nVisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                Try
                    sqlParam.Value = GenerateVisitID(ogloImmunization.ImmunizationDate, PatientID)
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@im_trn_ItemID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ItemID

                sqlParam = cmd.Parameters.Add("@im_trn_CounterID", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@im_trn_Dose", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Dose) Then
                    sqlParam.Value = ogloImmunization.Dose
                Else
                    sqlParam.Value = ""
                End If
                ''''''''
                sqlParam = cmd.Parameters.Add("@im_trn_Dategiven", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationDate) Then
                    sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Timegiven", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.TransactionTimeGiven) Then
                    sqlParam.Value = ogloImmunization.TransactionTimeGiven
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Route", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Route) Then
                    sqlParam.Value = ogloImmunization.Route
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Lotnumber", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.LotNumber) Then
                    sqlParam.Value = ogloImmunization.LotNumber
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Expirationdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@im_trn_Manufacturer", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Manufacture) Then
                    sqlParam.Value = ogloImmunization.Manufacture
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Userid", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@im_trn_Notes", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationNotes) Then
                    sqlParam.Value = ogloImmunization.ImmunizationNotes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@bln1", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bln2", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bln3", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bln4", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False


                sqlParam = cmd.Parameters.Add("@im_trn_duedate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.DueDate) Then
                    sqlParam.Value = Format(CDate(ogloImmunization.DueDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@im_trn_Site", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Site) Then
                    sqlParam.Value = ogloImmunization.Site
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_reminder", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False


                sqlParam = cmd.Parameters.Add("@im_vaccine_eligibilitycode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_item_name", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.VaccineName) Then
                    sqlParam.Value = Replace(ogloImmunization.VaccineName, "'", "''")
                Else
                    sqlParam.Value = ""
                End If

                cmd1 = New SqlCommand("select isnull(im_item_Count,0) from IM_MST where im_item_Id = " & ItemID & " and im_item_Name = '" & Replace(ogloImmunization.VaccineName, "'", "''") & "'", conn)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim ItemCount As Int16 = cmd1.ExecuteScalar()

                sqlParam = cmd.Parameters.Add("@im_item_count", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ItemCount

                sqlParam = cmd.Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.VaccineCode) Then
                    sqlParam.Value = ogloImmunization.VaccineCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.CPTCode) Then
                    sqlParam.Value = ogloImmunization.CPTCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_reasonfor_nonadmin", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_trn_Reaction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_trn_ReactionDT", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sTranID1", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@sTranID2", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sTranID3", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If
                If cmd1 IsNot Nothing Then
                    cmd1.Parameters.Clear()
                    cmd1.Dispose()
                    cmd1 = Nothing
                End If
            Next
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If cmd1 IsNot Nothing Then
                cmd1.Parameters.Clear()
                cmd1.Dispose()
                cmd1 = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function
    Public Function SaveImmunization(ByVal PatientID As Long, ByVal ogloImmunizationCol As ImmunizationCol, ByVal _UserID As Long, ByVal _UserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing


        Try
            For Each ogloImmunization As Immunization In ogloImmunizationCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                cmd = New SqlCommand("IM_AddUpdateTransactionLine", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@Patientid", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@transaction_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@transaction_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input

                If Not IsNothing(ogloImmunization.ImmunizationDate) Then
                    sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = DateTime.Now
                End If

                sqlParam = cmd.Parameters.Add("@admin_repo_refused", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@administered_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objgloCCDDatabaselayer.getDefaultProviderId()

                sqlParam = cmd.Parameters.Add("@sku", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@vaccine", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input

                If Not (IsNothing(ogloImmunization.VaccineName) OrElse IsNothing(ogloImmunization.VaccineCode)) Then
                    sqlParam.Value = ogloImmunization.VaccineCode & " - " & ogloImmunization.VaccineName
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@tradeName", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@manufacturer", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Manufacture) Then
                    sqlParam.Value = ogloImmunization.Manufacture
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@lot_number", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.LotNumber) Then
                    sqlParam.Value = ogloImmunization.LotNumber
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@expiration_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dosage_given", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Dose) Then
                    If ogloImmunization.Dose <> 0 Then
                        sqlParam.Value = ogloImmunization.Dose
                    Else
                        sqlParam.Value = 1
                    End If
                Else
                    sqlParam.Value = 1
                End If

                sqlParam = cmd.Parameters.Add("@amount_given", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@units", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@route", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Route) Then
                    sqlParam.Value = ogloImmunization.Route
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Site", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Site) Then
                    sqlParam.Value = ogloImmunization.Site
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@bvis_given", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@vis", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@publication_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@refusal_reason", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@refusal_comments", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@refused_by", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@reminder", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@due_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@Diagnosis_code", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@cpt_code", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS  
                sqlParam = cmd.Parameters.Add("@ndc_code", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@funding", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@notes", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationNotes) Then
                    sqlParam.Value = ogloImmunization.ImmunizationNotes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@user_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@bPatientHasAReaction", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@dtOnsetDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@sAdverseEvent", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@bPatientDied", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@PatientDieddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@bLifeThreateningIllness", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bRequiredEmergencyRoom", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bRequiredHospitalization", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@HospitalizationDays", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@bResultedInProlongation", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bResultedInPermDisability", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bNoneOfTheAbove", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@sPatientRecovered", SqlDbType.Char)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sReaction", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@nDocumentID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@biMasterID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nMasterDocumentID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@transaction_id_Op", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Output
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If

                sqlParam = Nothing
            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
        'Try
        '    For Each ogloImmunization As Immunization In ogloImmunizationCol

        '        objgloCCDDatabaselayer = New gloCCDDatabaseLayer

        '        'Call Function to get the Item_ID
        '        ItemID = getItemID(ogloImmunization)

        '        'Code Start to Store In master Table
        '        cmd = New SqlCommand("IM_InsUPIMTransRec", conn)
        '        cmd.CommandType = CommandType.StoredProcedure

        '        sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_nPatientID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = PatientID

        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.InputOutput
        '        sqlParam.Value = 0

        '        If conn.State = ConnectionState.Closed Then conn.Open()
        '        cmd.ExecuteNonQuery()
        '        Trans_ID = DirectCast(sqlParam.Value, Int64)
        '        'Code End to Store In master Table

        '        'Code Start to Store In Trans Table
        '        sqlParam = New SqlParameter()
        '        cmd = Nothing
        '        cmd = New SqlCommand("IM_InsUPIMTransDtl_SnoMed", conn)
        '        cmd.CommandType = CommandType.StoredProcedure


        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = Trans_ID

        '        sqlParam = cmd.Parameters.Add("@im_trn_Date", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = DateTime.Now
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_nVisitID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = GenerateVisitID(ogloImmunization.ImmunizationDate, PatientID)

        '        sqlParam = cmd.Parameters.Add("@im_trn_ItemID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ItemID

        '        sqlParam = cmd.Parameters.Add("@im_trn_CounterID", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = 1

        '        sqlParam = cmd.Parameters.Add("@im_trn_Dose", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Dose) Then
        '            sqlParam.Value = ogloImmunization.Dose
        '        Else
        '            sqlParam.Value = ""
        '        End If
        '        ''''''''
        '        sqlParam = cmd.Parameters.Add("@im_trn_Dategiven", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = System.DBNull.Value
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Timegiven", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.TransactionTimeGiven) Then
        '            sqlParam.Value = ogloImmunization.TransactionTimeGiven
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Route", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Route) Then
        '            sqlParam.Value = ogloImmunization.Route
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Lotnumber", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.LotNumber) Then
        '            sqlParam.Value = ogloImmunization.LotNumber
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Expirationdate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = System.DBNull.Value

        '        sqlParam = cmd.Parameters.Add("@im_trn_Manufacturer", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Manufacture) Then
        '            sqlParam.Value = ogloImmunization.Manufacture
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Userid", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = _UserID

        '        sqlParam = cmd.Parameters.Add("@im_trn_Notes", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationNotes) Then
        '            sqlParam.Value = ogloImmunization.ImmunizationNotes
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@bln1", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln2", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln3", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln4", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False


        '        sqlParam = cmd.Parameters.Add("@im_trn_duedate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.DueDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.DueDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = System.DBNull.Value
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Site", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Site) Then
        '            sqlParam.Value = ogloImmunization.Site
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_reminder", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False


        '        sqlParam = cmd.Parameters.Add("@im_vaccine_eligibilitycode", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_item_name", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.VaccineName) Then
        '            sqlParam.Value = Replace(ogloImmunization.VaccineName, "'", "''")
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        Dim cmd1 As SqlCommand = New SqlCommand("select isnull(im_item_Count,0) from IM_MST where im_item_Id = " & ItemID & " and im_item_Name = '" & Replace(ogloImmunization.VaccineName, "'", "''") & "'", conn)
        '        If conn.State = ConnectionState.Closed Then
        '            conn.Open()
        '        End If
        '        Dim ItemCount As Int16 = cmd1.ExecuteScalar()

        '        sqlParam = cmd.Parameters.Add("@im_item_count", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ItemCount

        '        sqlParam = cmd.Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.VaccineCode) Then
        '            sqlParam.Value = ogloImmunization.VaccineCode
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.CPTCode) Then
        '            sqlParam.Value = ogloImmunization.CPTCode
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_reasonfor_nonadmin", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_trn_Reaction", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_trn_ReactionDT", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = System.DBNull.Value

        '        sqlParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sTranID1", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""


        '        sqlParam = cmd.Parameters.Add("@sTranID2", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sTranID3", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        If conn.State = ConnectionState.Closed Then conn.Open()
        '        cmd.ExecuteNonQuery()
        '    Next
        '    Return True
        'Catch ex As Exception
        '    Return False
        'Finally
        '    If conn.State = ConnectionState.Open Then conn.Close()
        'End Try
    End Function
    Public Function getItemID(ByVal oGloImmun As Immunization) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim _ItemID As Int64 = 0
        Dim sqlParam As SqlParameter = Nothing
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd = Nothing
            cmd = New SqlCommand("SELECT im_item_Id FROM IM_MST WHERE im_item_Name='" & oGloImmun.VaccineName & "'", conn)
            _ItemID = cmd.ExecuteScalar()

            If _ItemID > 0 Then
            Else
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If


                cmd = Nothing
                cmd = New SqlCommand("IM_InsUpdItemMast", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@im_item_Name", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGloImmun.VaccineName) Then
                    sqlParam.Value = oGloImmun.VaccineName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_item_Count", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGloImmun.CPTCode) Then
                    sqlParam.Value = oGloImmun.CPTCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGloImmun.VaccineCode) Then
                    sqlParam.Value = oGloImmun.VaccineCode
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@im_sConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_sDescriptionID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_sSnoMedID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_sICD9", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@im_item_Id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)



                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                cmd = Nothing
                cmd = New SqlCommand("SELECT im_item_Id FROM IM_MST WHERE im_item_Name='" & oGloImmun.VaccineName & "'", conn)
                _ItemID = cmd.ExecuteScalar()
            End If

            Return _ItemID
        Catch ex As Exception
            MsgBox(ex.ToString())
            ex = Nothing
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function
    'Code End: Add By Rohit on 20101014

    'Code Start: Added By Rohit On 20101015
    Public Function SaveLabResult(ByVal PatientID As Long, ByVal ogloLabResultCol As LabResultsCol, ByVal _UserID As Long, ByVal _UserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim Order_ID, ItemID As Int64

        Try
            For Each ogloLabResult As LabResults In ogloLabResultCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                'Call Function to get the Test_ID
                ItemID = getLabTestID(ogloLabResult)

                'code to insert in Order master Table
                sqlParam = Nothing
                cmd = Nothing
                cmd = New SqlCommand("Lab_InsertOrderMaster", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@labom_OrderNoPrefix", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "ORD"

                sqlParam = cmd.Parameters.Add("@labom_OrderNoID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = getMAXOrderID()

                sqlParam = cmd.Parameters.Add("@labom_DateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultDate) Then
                    sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = Date.Now
                End If
                'sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeYear", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeMonth", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeDay", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_ProviderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objgloCCDDatabaselayer.getDefaultProviderId()

                sqlParam = cmd.Parameters.Add("@labom_PreferredLabID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_SampledByID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_ReferredByID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_ReferredToID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@SendTo", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labom_VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                Try
                    sqlParam.Value = GenerateVisitID(ogloLabResult.ResultDate, PatientID)
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@labom_ExternalCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_PreferredLabName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_SampledByName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByFName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByMName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByLName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _ClinicID

                sqlParam = cmd.Parameters.Add("@Id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                Order_ID = DirectCast(sqlParam.Value, Int64)


                'end of code
                UpdateLabOrderDate(Convert.ToString(Order_ID))

                'Code Start to Store In master Table
                sqlParam = Nothing
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                cmd = Nothing
                cmd = New SqlCommand("Lab_InsertOrderTestDtl", conn)
                cmd.CommandType = CommandType.StoredProcedure


                sqlParam = cmd.Parameters.Add("@labotd_TestID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ItemID

                sqlParam = cmd.Parameters.Add("@labotd_LineNo", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labotd_Note", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_CollectionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_StorageID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_LOINCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultLOINCID) Then
                    sqlParam.Value = ogloLabResult.ResultLOINCID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_Instruction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_Precaution", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_Comment", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultComment) Then
                    sqlParam.Value = ogloLabResult.ResultComment
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_DateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultDate) Then
                    sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyyy")
                Else
                    sqlParam.Value = Date.Now
                End If

                sqlParam = cmd.Parameters.Add("@labotd_UserID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@labotd_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotd_TestName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.TestName) Then
                    sqlParam.Value = ogloLabResult.TestName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ProviderName) Then
                    sqlParam.Value = ogloLabResult.ProviderName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_CollectionName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_StorageName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_OrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Order_ID
                '4046406039303570
                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                'Code End to Store In master Table


                'Code Start to Store In Trans Table
                'sqlParam = New SqlParameter()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                cmd = Nothing
                cmd = New SqlCommand("Lab_InsertOrderTestResultDetails", conn)
                cmd.CommandType = CommandType.StoredProcedure


                sqlParam = cmd.Parameters.Add("@labotrd_OrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Order_ID

                sqlParam = cmd.Parameters.Add("@labotrd_TestID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ItemID

                sqlParam = cmd.Parameters.Add("@labotrd_TestResultNumber", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labotrd_ResultLineNo", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labotrd_ResultNameID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotrd_ResultName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultName) Then
                    sqlParam.Value = ogloLabResult.ResultName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultValue", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultValue) Then
                    sqlParam.Value = ogloLabResult.ResultValue
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultUnit", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultUnit) Then
                    sqlParam.Value = ogloLabResult.ResultUnit
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultRange", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultRange) Then
                    sqlParam.Value = ogloLabResult.ResultRange
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultType", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultType) Then
                    sqlParam.Value = ogloLabResult.ResultType
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_AbnormalFlag", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.AbnormalFlag) Then
                    sqlParam.Value = ogloLabResult.AbnormalFlag
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultComment", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultComment) Then
                    sqlParam.Value = ogloLabResult.ResultComment
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_ResultWord", SqlDbType.Image)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotrd_ResultDMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotrd_ResultUserID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@labotrd_ResultDateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultDate) Then
                    sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = Date.Now
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_IsFinished", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotrd_LOINCID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultLOINCID) Then
                    sqlParam.Value = ogloLabResult.ResultLOINCID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_TestName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.TestName) Then
                    sqlParam.Value = ogloLabResult.TestName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotrd_AlternateResultName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotrd_AlternateResultCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotrd_ProducerIdentifier", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If
            Next
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function
    Public Function SaveQRDALabResult(ByVal PatientID As Long, ByVal ogloLabResultCol As LabResultsCol, ByVal _UserID As Long, ByVal _UserName As String, ByVal providerid As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim Order_ID, ItemID As Int64

        Try
            For Each ogloLabResult As LabResults In ogloLabResultCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                'Call Function to get the Test_ID
                ItemID = getLabTestID(ogloLabResult)

                'code to insert in Order master Table
                sqlParam = Nothing
                cmd = Nothing
                cmd = New SqlCommand("Lab_InsertOrderMaster", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@blnOrderNotCPOE", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0
                sqlParam = cmd.Parameters.Add("@bOutboundTransistion", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@OrderStatusNumber", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_OrderNoPrefix", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "ORD"

                sqlParam = cmd.Parameters.Add("@labom_OrderNoID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = getMAXOrderID()

                sqlParam = cmd.Parameters.Add("@labom_DateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                ogloLabResult.ResultDate = DateTime.Now
                If Not IsNothing(ogloLabResult.ResultDate) Then
                    sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyy")
                Else
                    sqlParam.Value = Date.Now
                End If
                'sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeYear", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeMonth", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_PatientAgeDay", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labom_ProviderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = providerid

                sqlParam = cmd.Parameters.Add("@labom_PreferredLabID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_SampledByID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_ReferredByID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_ReferredToID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@SendTo", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labom_VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                Try
                    sqlParam.Value = GenerateVisitID(ogloLabResult.SpecimenDate, PatientID)
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@labom_ExternalCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labom_PreferredLabName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_SampledByName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByFName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByMName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labom_ReferredByLName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _ClinicID

                sqlParam = cmd.Parameters.Add("@Id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                Order_ID = DirectCast(sqlParam.Value, Int64)


                'end of code
                UpdateLabOrderDate(Convert.ToString(Order_ID))

                'Code Start to Store In master Table
                sqlParam = Nothing
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                cmd = Nothing
                cmd = New SqlCommand("Lab_InsertOrderTestDtl", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenCollectionStartDateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.SpecimenDate) AndAlso ogloLabResult.SpecimenDate <> "" Then
                    sqlParam.Value = ogloLabResult.SpecimenDate
                Else
                    sqlParam.Value = DateTime.Now
                End If

                'sqlParam.Value = ogloLabResult.SpecimenDate

                'Take this fro not done tests.
                sqlParam = cmd.Parameters.Add("@labotd_TestScheduledEndDateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = DateTime.Now


                sqlParam = cmd.Parameters.Add("@labotd_SpecimenActionCode", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenCondition", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenRejectReason", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""


                sqlParam = cmd.Parameters.Add("@labotd_SpecimenTypeCodingSystem", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenTypeText", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.Add("@labotd_SpecimenTypeIdentifier", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_TestScheduledDateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.SpecimenDate) AndAlso ogloLabResult.SpecimenDate <> "" Then
                    sqlParam.Value = ogloLabResult.SpecimenDate
                Else
                    sqlParam.Value = DateTime.Now
                End If


                sqlParam = cmd.Parameters.Add("@labotd_CPT", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_Template", SqlDbType.Image)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotd_TestType", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenConditionDisp", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""
                sqlParam = cmd.Parameters.Add("@labotd_SpecimenSource", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_TestStatus", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_DICOMID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_TestID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ItemID

                sqlParam = cmd.Parameters.Add("@labotd_LineNo", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labotd_Note", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_CollectionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_StorageID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labotd_LOINCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultLOINCID) Then
                    sqlParam.Value = ogloLabResult.ResultLOINCID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_Instruction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_Precaution", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_Comment", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultComment) Then
                    sqlParam.Value = ogloLabResult.ResultComment
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_DateTime", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultDate) Then
                    sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyyy")
                Else
                    sqlParam.Value = Date.Now
                End If

                sqlParam = cmd.Parameters.Add("@labotd_UserID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@labotd_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labotd_TestName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.TestName) Then
                    sqlParam.Value = ogloLabResult.TestName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_SpecimenName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ProviderName) Then
                    sqlParam.Value = ogloLabResult.ProviderName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_CollectionName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_StorageName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labotd_OrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Order_ID

                sqlParam = cmd.Parameters.Add("@labotd_ConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ResultReasonConceptID) Then
                    sqlParam.Value = ogloLabResult.ResultReasonConceptID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labotd_Valuset", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.ValusetOID) Then
                    sqlParam.Value = ogloLabResult.ValusetOID
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@labotd_valuesetname", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloLabResult.Valueset) Then
                    sqlParam.Value = ogloLabResult.Valueset
                Else
                    sqlParam.Value = ""
                End If
                '4046406039303570
                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                'Code End to Store In master Table


                'Code Start to Store In Trans Table
                'sqlParam = New SqlParameter()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                cmd = Nothing
                If Not IsNothing(ogloLabResult.ResultLOINCID) AndAlso ogloLabResult.ResultLOINCID <> "" Then
                    cmd = New SqlCommand("Lab_InsertOrderTestResult", conn)
                    cmd.CommandType = CommandType.StoredProcedure



                    sqlParam = cmd.Parameters.Add("@labotr_OrderID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Order_ID

                    sqlParam = cmd.Parameters.Add("@labotr_TestID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ItemID

                    sqlParam = cmd.Parameters.Add("@labotr_TestResultNumber", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 1




                    sqlParam = cmd.Parameters.Add("@labotr_TestResultName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultName) Then
                        sqlParam.Value = ogloLabResult.ResultName
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotr_TestResultDateTime", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultDate) Then
                        sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = Date.Now
                    End If

                    sqlParam = cmd.Parameters.Add("@labotr_ResultTransferDateTime", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.SpecimenDate) Then
                        sqlParam.Value = ogloLabResult.SpecimenDate
                    Else
                        sqlParam.Value = Date.Now
                    End If

                    sqlParam = cmd.Parameters.Add("@labotr_IsFinished", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0



                    sqlParam = cmd.Parameters.Add("@labotr_TestName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.TestName) Then
                        sqlParam.Value = ogloLabResult.TestName
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotr_AlternateTestName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""

                    sqlParam = cmd.Parameters.Add("@labotr_AlternateTestCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""

                    sqlParam = cmd.Parameters.Add("@labotr_DMSID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0

                    If conn.State = ConnectionState.Closed Then conn.Open()
                    cmd.ExecuteNonQuery()


                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If


                    'sqlParam = New SqlParameter()

                    cmd = Nothing
                    cmd = New SqlCommand("Lab_InsertOrderTestResultDetails", conn)
                    cmd.CommandType = CommandType.StoredProcedure


                    sqlParam = cmd.Parameters.Add("@labotrd_OrderID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Order_ID

                    sqlParam = cmd.Parameters.Add("@labotrd_TestID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ItemID

                    sqlParam = cmd.Parameters.Add("@labotrd_TestResultNumber", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 1

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultLineNo", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 1

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultNameID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultName) Then
                        sqlParam.Value = ogloLabResult.ResultName
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultValue", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultValue) Then
                        sqlParam.Value = ogloLabResult.ResultValue
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultUnit", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultUnit) Then
                        sqlParam.Value = ogloLabResult.ResultUnit
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultRange", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultRange) Then
                        sqlParam.Value = ogloLabResult.ResultRange
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultType", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultType) Then
                        sqlParam.Value = ogloLabResult.ResultType
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_AbnormalFlag", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.AbnormalFlag) Then
                        sqlParam.Value = ogloLabResult.AbnormalFlag
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultComment", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultComment) Then
                        sqlParam.Value = ogloLabResult.ResultComment
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultWord", SqlDbType.Image)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = System.DBNull.Value

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultDMSID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = System.DBNull.Value

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultUserID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = _UserID

                    sqlParam = cmd.Parameters.Add("@labotrd_ResultDateTime", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultDate) Then
                        sqlParam.Value = Format(CDate(ogloLabResult.ResultDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = Date.Now
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_TestSpecimenCollectionDateTime", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.SpecimenDate) Then
                        sqlParam.Value = Format(CDate(ogloLabResult.SpecimenDate), "MM/dd/yyyy")

                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_IsFinished", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0

                    sqlParam = cmd.Parameters.Add("@labotrd_LOINCID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""


                    sqlParam = cmd.Parameters.Add("@labotrd_TestName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.TestName) Then
                        sqlParam.Value = ogloLabResult.TestName
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_AlternateResultName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""

                    sqlParam = cmd.Parameters.Add("@labotrd_AlternateResultCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultLOINCID) Then
                        sqlParam.Value = ogloLabResult.ResultLOINCID
                    Else
                        sqlParam.Value = ""
                    End If
                    sqlParam = cmd.Parameters.Add("@labotrd_ProducerIdentifier", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""


                    sqlParam = cmd.Parameters.Add("@labotrd_ConceptID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultReasonConceptID) Then
                        sqlParam.Value = ogloLabResult.ResultReasonConceptID
                    Else
                        sqlParam.Value = ""
                    End If


                    sqlParam = cmd.Parameters.Add("@labotrd_ICD9", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultReasonICD9) Then
                        sqlParam.Value = ogloLabResult.ResultReasonICD9
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_ICD10", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultReasonICD10) Then
                        sqlParam.Value = ogloLabResult.ResultReasonICD10
                    Else
                        sqlParam.Value = ""
                    End If

                    sqlParam = cmd.Parameters.Add("@labotrd_LOINC", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    If Not IsNothing(ogloLabResult.ResultReasonLOINC) Then
                        sqlParam.Value = ogloLabResult.ResultReasonLOINC
                    Else
                        sqlParam.Value = ""
                    End If

                    If conn.State = ConnectionState.Closed Then conn.Open()
                    cmd.ExecuteNonQuery()
                    If IsNothing(cmd) = False Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                End If


                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If
            Next
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function
    Public Function UpdateLabOrderDate(ByVal _OrderID As String) As Boolean
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try
            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strquery = "update lab_order_mst set labom_orderDate= labom_transactionDate where labom_orderid=" & _OrderID
            cmd.CommandText = strquery
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            conn.Close()
            cmd.Dispose()
            Return False
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strquery = Nothing
        End Try
    End Function
    Public Function getLabTestID(ByVal oGlolabResult As LabResults) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim _ItemID As Int64 = 0
        Dim sqlParam As SqlParameter = Nothing
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd = New SqlCommand("SELECT labtm_ID FROM Lab_Test_Mst WHERE labtm_Name='" & oGlolabResult.TestName & "'", conn)
            _ItemID = cmd.ExecuteScalar()

            If _ItemID > 0 Then
            Else
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                cmd = New SqlCommand("Lab_InsertLabTest", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@labtm_Code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGlolabResult.TestCode) Then
                    sqlParam.Value = oGlolabResult.TestCode
                Else
                    sqlParam.Value = ""
                End If ' sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labtm_Name", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGlolabResult.TestName) Then
                    sqlParam.Value = oGlolabResult.TestName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labtm_Ordarable", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@labtm_SpecimenID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labtm_CollectionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value


                sqlParam = cmd.Parameters.Add("@labtm_StorageID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@labtm_Instruction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labtm_Precuation", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@labtm_LOINCId", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oGlolabResult.ResultLOINCID) Then
                    sqlParam.Value = oGlolabResult.ResultLOINCID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@labtm_DeprtCatID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@labtm_TestHeadID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0


                sqlParam = cmd.Parameters.Add("@labtm_ResultType", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                'If Not IsNothing(oGlolabResult.ResultType) Then
                '    sqlParam.Value = oGlolabResult.ResultType
                'Else
                sqlParam.Value = 0 'System.DBNull.Value
                '  End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

                'sqlParam = cmd.Parameters.Add("@labtm_SpecimenName", SqlDbType.VarChar)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = ""

                'sqlParam = cmd.Parameters.Add("@labtm_CollectionName", SqlDbType.VarChar)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = ""

                'sqlParam = cmd.Parameters.Add("@labtm_StorageName", SqlDbType.VarChar)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = ""


                'sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = _ClinicID

                'sqlParam = cmd.Parameters.Add("@Status", SqlDbType.VarChar)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@iD", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = 0

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                _ItemID = DirectCast(sqlParam.Value, Int64)
            End If

            Return _ItemID
        Catch ex As Exception
            MsgBox(ex.ToString())
            ex = Nothing
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    Public Function getMAXOrderID() As Int64
        Dim cmd As SqlCommand = Nothing
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            cmd = New SqlCommand("SELECT ISNULL(MAX(labom_OrderNoID),0) FROM LAB_Order_MST", conn)
            Dim _MAXOrderNo As Int64 = cmd.ExecuteScalar() + 1
            Return _MAXOrderNo
        Catch ex As Exception
            MsgBox(ex.ToString)
            ex = Nothing
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Function

    'Code End: Added By Rohit On 20101015

    ''Added by Mayuri:20130213-To Save reconciled information
    Public Function SaveQRDAImmunization(ByVal PatientID As Long, ByVal ogloImmunizationCol As ImmunizationCol, ByVal _UserID As Long, ByVal _UserName As String, ByVal providerid As Long) As Boolean
        Dim cmd As SqlCommand = Nothing
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim objgloCCDDatabaselayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing


        Try
            For Each ogloImmunization As Immunization In ogloImmunizationCol

                objgloCCDDatabaselayer = New gloCCDDatabaseLayer
                cmd = New SqlCommand("IM_AddUpdateTransactionLine", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@Patientid", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@transaction_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@transaction_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input

                If Not IsNothing(ogloImmunization.ImmunizationDate) Then
                    sqlParam.Value = ogloImmunization.ImmunizationDate
                Else
                    sqlParam.Value = DBNull.Value
                End If

                sqlParam = cmd.Parameters.Add("@admin_repo_refused", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not (IsNothing(ogloImmunization.admin_report_refused)) Then
                    sqlParam.Value = ogloImmunization.admin_report_refused
                Else
                    sqlParam.Value = "0"
                End If

                sqlParam = cmd.Parameters.Add("@administered_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = providerid

                sqlParam = cmd.Parameters.Add("@sku", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@vaccine", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input

                If Not (IsNothing(ogloImmunization.VaccineName) OrElse IsNothing(ogloImmunization.VaccineCode)) Then
                    sqlParam.Value = ogloImmunization.VaccineCode & " - " & ogloImmunization.VaccineName
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@tradeName", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationTrade) Then
                    sqlParam.Value = Convert.ToString(ogloImmunization.ImmunizationTrade)
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@manufacturer", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Manufacture) Then
                    sqlParam.Value = Convert.ToString(ogloImmunization.Manufacture)
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@lot_number", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.LotNumber) Then
                    sqlParam.Value = Convert.ToString(ogloImmunization.LotNumber)
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@expiration_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@dosage_given", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Dose) Then
                    If ogloImmunization.Dose <> 0 Then
                        sqlParam.Value = ogloImmunization.Dose
                    Else
                        sqlParam.Value = 1
                    End If
                Else
                    sqlParam.Value = 1
                End If

                sqlParam = cmd.Parameters.Add("@amount_given", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.AmountGiven) Then
                    sqlParam.Value = ogloImmunization.AmountGiven
                Else
                    sqlParam.Value = 0
                End If



                sqlParam = cmd.Parameters.Add("@units", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Units) Then
                    sqlParam.Value = ogloImmunization.Units
                Else
                    sqlParam.Value = ""
                End If



                sqlParam = cmd.Parameters.Add("@route", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Route) Then
                    sqlParam.Value = ogloImmunization.Route
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Site", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Site) Then
                    sqlParam.Value = ogloImmunization.Site
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@bvis_given", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@vis", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@publication_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@refusal_reason", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Reason) Then
                    sqlParam.Value = ogloImmunization.Reason
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@refusal_comments", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@refused_by", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = "admin"

                sqlParam = cmd.Parameters.Add("@reminder", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@due_date", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@Diagnosis_code", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ICDCode) Then
                    sqlParam.Value = ogloImmunization.ICDCode
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@cpt_code", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.CPTCode) Then
                    sqlParam.Value = ogloImmunization.CPTCode
                Else
                    sqlParam.Value = ""
                End If
                ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS  
                sqlParam = cmd.Parameters.Add("@ndc_code", SqlDbType.NVarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@funding", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@notes", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ImmunizationNotes) Then
                    sqlParam.Value = ogloImmunization.ImmunizationNotes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@user_id", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = _UserID

                sqlParam = cmd.Parameters.Add("@bPatientHasAReaction", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Patienthasreaction) Then
                    sqlParam.Value = ogloImmunization.Patienthasreaction
                Else
                    sqlParam.Value = False
                End If


                sqlParam = cmd.Parameters.Add("@dtOnsetDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@sAdverseEvent", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@bPatientDied", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@PatientDieddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value

                sqlParam = cmd.Parameters.Add("@bLifeThreateningIllness", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bRequiredEmergencyRoom", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bRequiredHospitalization", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@HospitalizationDays", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@bResultedInProlongation", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bResultedInPermDisability", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@bNoneOfTheAbove", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = False

                sqlParam = cmd.Parameters.Add("@sPatientRecovered", SqlDbType.Char)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sReaction", SqlDbType.Text)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@nDocumentID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@biMasterID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nMasterDocumentID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@transaction_id_Op", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Output
                sqlParam.Value = 0

                sqlParam = cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0


                sqlParam = cmd.Parameters.Add("@sRufusalReasonCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input

                If Not IsNothing(ogloImmunization.ReasonConceptID) Then
                    sqlParam.Value = ogloImmunization.ReasonConceptID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sSnoMedConceptID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input

                If Not IsNothing(ogloImmunization.ConceptID) Then
                    sqlParam.Value = ogloImmunization.ConceptID
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@sValuesetOID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.ValuesetOID) Then
                    sqlParam.Value = ogloImmunization.ValuesetOID
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sValueset", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloImmunization.Valueset) Then
                    sqlParam.Value = ogloImmunization.Valueset
                Else
                    sqlParam.Value = ""
                End If

                If conn.State = ConnectionState.Closed Then conn.Open()
                cmd.ExecuteNonQuery()
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(objgloCCDDatabaselayer) = False Then
                    objgloCCDDatabaselayer.Dispose()
                    objgloCCDDatabaselayer = Nothing
                End If

                sqlParam = Nothing
            Next

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(objgloCCDDatabaselayer) = False Then
                objgloCCDDatabaselayer.Dispose()
                objgloCCDDatabaselayer = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
        'Try
        '    For Each ogloImmunization As Immunization In ogloImmunizationCol

        '        objgloCCDDatabaselayer = New gloCCDDatabaseLayer

        '        'Call Function to get the Item_ID
        '        ItemID = getItemID(ogloImmunization)

        '        'Code Start to Store In master Table
        '        cmd = New SqlCommand("IM_InsUPIMTransRec", conn)
        '        cmd.CommandType = CommandType.StoredProcedure

        '        sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = gloLibCCDGeneral.GetPrefixTransactionID(0)

        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_nPatientID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = PatientID

        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.InputOutput
        '        sqlParam.Value = 0

        '        If conn.State = ConnectionState.Closed Then conn.Open()
        '        cmd.ExecuteNonQuery()
        '        Trans_ID = DirectCast(sqlParam.Value, Int64)
        '        'Code End to Store In master Table

        '        'Code Start to Store In Trans Table
        '        sqlParam = New SqlParameter()
        '        cmd = Nothing
        '        cmd = New SqlCommand("IM_InsUPIMTransDtl_SnoMed", conn)
        '        cmd.CommandType = CommandType.StoredProcedure


        '        sqlParam = cmd.Parameters.Add("@im_trn_mst_Id", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = Trans_ID

        '        sqlParam = cmd.Parameters.Add("@im_trn_Date", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = DateTime.Now
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_nVisitID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = GenerateVisitID(ogloImmunization.ImmunizationDate, PatientID)

        '        sqlParam = cmd.Parameters.Add("@im_trn_ItemID", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ItemID

        '        sqlParam = cmd.Parameters.Add("@im_trn_CounterID", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = 1

        '        sqlParam = cmd.Parameters.Add("@im_trn_Dose", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Dose) Then
        '            sqlParam.Value = ogloImmunization.Dose
        '        Else
        '            sqlParam.Value = ""
        '        End If
        '        ''''''''
        '        sqlParam = cmd.Parameters.Add("@im_trn_Dategiven", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.ImmunizationDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = System.DBNull.Value
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Timegiven", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.TransactionTimeGiven) Then
        '            sqlParam.Value = ogloImmunization.TransactionTimeGiven
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Route", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Route) Then
        '            sqlParam.Value = ogloImmunization.Route
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Lotnumber", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.LotNumber) Then
        '            sqlParam.Value = ogloImmunization.LotNumber
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Expirationdate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = System.DBNull.Value

        '        sqlParam = cmd.Parameters.Add("@im_trn_Manufacturer", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Manufacture) Then
        '            sqlParam.Value = ogloImmunization.Manufacture
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Userid", SqlDbType.BigInt)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = _UserID

        '        sqlParam = cmd.Parameters.Add("@im_trn_Notes", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.ImmunizationNotes) Then
        '            sqlParam.Value = ogloImmunization.ImmunizationNotes
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@bln1", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln2", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln3", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False

        '        sqlParam = cmd.Parameters.Add("@bln4", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False


        '        sqlParam = cmd.Parameters.Add("@im_trn_duedate", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.DueDate) Then
        '            sqlParam.Value = Format(CDate(ogloImmunization.DueDate), "MM/dd/yyyy")
        '        Else
        '            sqlParam.Value = System.DBNull.Value
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_trn_Site", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.Site) Then
        '            sqlParam.Value = ogloImmunization.Site
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_reminder", SqlDbType.Bit)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = False


        '        sqlParam = cmd.Parameters.Add("@im_vaccine_eligibilitycode", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_item_name", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.VaccineName) Then
        '            sqlParam.Value = Replace(ogloImmunization.VaccineName, "'", "''")
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        Dim cmd1 As SqlCommand = New SqlCommand("select isnull(im_item_Count,0) from IM_MST where im_item_Id = " & ItemID & " and im_item_Name = '" & Replace(ogloImmunization.VaccineName, "'", "''") & "'", conn)
        '        If conn.State = ConnectionState.Closed Then
        '            conn.Open()
        '        End If
        '        Dim ItemCount As Int16 = cmd1.ExecuteScalar()

        '        sqlParam = cmd.Parameters.Add("@im_item_count", SqlDbType.Int)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ItemCount

        '        sqlParam = cmd.Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.VaccineCode) Then
        '            sqlParam.Value = ogloImmunization.VaccineCode
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        If Not IsNothing(ogloImmunization.CPTCode) Then
        '            sqlParam.Value = ogloImmunization.CPTCode
        '        Else
        '            sqlParam.Value = ""
        '        End If

        '        sqlParam = cmd.Parameters.Add("@im_reasonfor_nonadmin", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_trn_Reaction", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@im_trn_ReactionDT", SqlDbType.DateTime)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = System.DBNull.Value

        '        sqlParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sTranID1", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""


        '        sqlParam = cmd.Parameters.Add("@sTranID2", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        sqlParam = cmd.Parameters.Add("@sTranID3", SqlDbType.VarChar)
        '        sqlParam.Direction = ParameterDirection.Input
        '        sqlParam.Value = ""

        '        If conn.State = ConnectionState.Closed Then conn.Open()
        '        cmd.ExecuteNonQuery()
        '    Next
        '    Return True
        'Catch ex As Exception
        '    Return False
        'Finally
        '    If conn.State = ConnectionState.Open Then conn.Close()
        'End Try
    End Function
End Class
