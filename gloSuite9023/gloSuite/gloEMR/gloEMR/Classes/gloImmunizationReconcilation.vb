Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports gloSettings

Public Enum CCDFileStatus
    Imported = 1
    ListExtracted = 2
    NoList = 3
End Enum

Public Enum ListStatus
    Ready = 1
    Finished = 2

End Enum

Public Class gloImmunizationReconcilation
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
    Public Shared blnRemovePatientDataSetting As Boolean = False
    Public Function GetReconcilationList(ByVal PatientID As Long, Optional ByVal SourceName As String = "") As DataTable
        Dim cmd As New SqlCommand
        Dim conn As New SqlConnection
        'Dim strQuery As String = ""
        'Dim _Patient As New gloCCDLibrary.Patient
        Dim DtReconcilationList As New DataTable
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(GetConnectionString())
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_GetReconciliationList"


            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            sqlParam = cmd.Parameters.Add("@SourceType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SourceName

            Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter(cmd)


            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            DataAdapter.Fill(DtReconcilationList)

            Return DtReconcilationList

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
            End If
        End Try
    End Function

    Public Function AddUpDelReconciledImmunization(ByVal dtReconcile As DataTable, ByVal _PatientId As Int64, ByVal _immunizationType As String) As String
        Dim conn As New SqlConnection(GetConnectionString())
        Try

            Dim cmd As SqlCommand = Nothing
            Dim sqlParam As SqlParameter = Nothing

            cmd = New SqlCommand("gsp_InUpDel_ReconcileImmunization", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@TVP_ReconcileImmunization", SqlDbType.Structured)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtReconcile

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _PatientId

            sqlParam = cmd.Parameters.Add("@ImmunizationType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _immunizationType

            If cmd.Connection.State = ConnectionState.Closed Then
                cmd.Connection.Open()
            End If


            cmd.ExecuteNonQuery()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            ''connection state closed
            If Not IsNothing(conn) Then
                If (conn.State = ConnectionState.Open) Then
                    conn.Close()
                End If
            End If
            conn.Dispose()
        End Try
        Return Nothing
    End Function

    Public Function GetExtractedList(ByVal ListID As Long, ByVal ListType As String, ByVal PatientID As Long) As DataTable
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        
        Dim DtReconcilationList As New DataTable
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(GetConnectionString())
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_GetExtractedList_forHxForecastReconcilation"

            sqlParam = cmd.Parameters.Add("@ListID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ListID

            sqlParam = cmd.Parameters.Add("@sListType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ListType

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter(cmd)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            DataAdapter.Fill(DtReconcilationList)
            DataAdapter.Dispose()
            DataAdapter = Nothing

            Return DtReconcilationList

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()

            End If
            conn.Dispose()
        End Try
    End Function

    
    Public Function GetUnFinishedReconcileList(ByVal PatientID As Long) As DataTable
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        'Dim strQuery As String = ""
        Dim sqlParam As SqlParameter = Nothing
        Dim DtReconcilationList As New DataTable
        Try
            conn = New SqlConnection(GetConnectionString())
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "CCD_Reconcile_GetUnfinishedLists"



            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID


            Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter(cmd)


            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            DataAdapter.Fill(DtReconcilationList)

            DataAdapter.Dispose()
            DataAdapter = Nothing

            Return DtReconcilationList

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()

            End If
            conn.Dispose()
        End Try
    End Function

End Class

Public Class gloImmHxReconciliation
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

    Public Sub New()

    End Sub

    Public Enum CodeSystem
        HL7
        FDA
    End Enum

#Region "Public Functions"
    Public Sub SaveColumnWidth(ByVal _IsControlFilling As Boolean, ByVal dg As C1FlexGrid, ByVal UserID As Long)
        If _IsControlFilling = False AndAlso dg.DataSource IsNot Nothing Then
            Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(GetConnectionString())
            Try
                ogloSettings.SaveGridColumnWidth(dg, ModuleOfGridColumn.Reconciliation, UserID)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
                ex = Nothing
            Finally
                If Not IsNothing(ogloSettings) Then
                    ogloSettings.Dispose()
                    ogloSettings = Nothing
                End If
            End Try
        End If
    End Sub

    Public Shared Function GetRouteCodeSystem(_CodeSystem As String, RouteCode As String) As String
        Dim routeDescription As String = String.Empty
        Try
            If _CodeSystem <> String.Empty And RouteCode <> String.Empty Then
                'TempMayuri
                'If String.Compare(_CodeSystem, _CodeSystem.FDARouteCode, True) = 0 Then
                '    _CodeSystem = CodeSystem.FDA.ToString()
                'ElseIf String.Compare(_CodeSystem, _CodeSystem.HL7RouteCode, True) = 0 Then
                '    _CodeSystem = CodeSystem.HL7.ToString()
                'End If
                Using con As New SqlConnection(GetConnectionString())
                    Dim _strSQl As String = (Convert.ToString("Select ConceptName from CodingSystemDetails Where ConceptCode= '") & RouteCode) + "' and CodeSystem=  '" + _CodeSystem.ToString() + "'"
                    Using cmd As New SqlCommand(_strSQl, con)
                        cmd.CommandType = CommandType.Text
                        Using da As New SqlDataAdapter(cmd)
                            con.Open()
                            Dim result As Object = cmd.ExecuteScalar()
                            con.Close()
                            If result IsNot Nothing Then
                                Return Convert.ToString(result)
                            End If
                        End Using
                    End Using
                End Using
            End If
        Catch generatedExceptionName As SqlException
            'throw ex
            Return routeDescription
        End Try
        Return routeDescription
    End Function

   
    Public Shared Function GetFileType(ByVal _nCCDID As Int64) As String
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _FileType As String = ""
        Try
            cmd = New SqlCommand
            conn = New SqlConnection(GetConnectionString())

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT sFileType FROM CCD_Files WHERE nCCDID=" & _nCCDID
            cmd.CommandText = strQuery

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _FileType = temp.ToString.Trim()
            End If
            Return _FileType
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            strQuery = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            End If
        End Try
    End Function
    Public Shared Function GetPatientProviderName(ByVal _nProviderID As Int64) As String
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _FileType As String = ""
        Try
            cmd = New SqlCommand
            conn = New SqlConnection(GetConnectionString())
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT sFirstName + ' ' + sMiddleName + ' ' + sLastName AS ProviderName FROM Provider_MST WHERE nProviderID=" & _nProviderID
            cmd.CommandText = strQuery

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _FileType = temp.ToString.Trim()
            End If
            Return _FileType
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return ""
        Finally
            strQuery = Nothing
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If IsNothing(cmd) = False Then
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Shared Function RetrieveDocumentFile(ByVal nCCDId As Int64) As String

        Dim oResult As New Object
        Dim strFileName As String = ""
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())

        Try
            cmd = New SqlCommand()
            cmd.Connection = conn

            cmd.CommandText = "CCD_RetrieveCCDFile"

            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCCDId
            If conn.State = ConnectionState.Closed Then conn.Open()

            oResult = cmd.ExecuteScalar()

            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                strFileName = GenerateTempFileName(gloSettings.FolderSettings.AppTempFolderPath, ".xml")
                '' generate Physical file
                strFileName = GenerateFile(oResult, strFileName)
                Return strFileName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
            If IsNothing(oResult) = False Then
                oResult = Nothing
            End If
        End Try
    End Function

    Public Shared Function SavePatientHistory(ByVal ogloPatientHistoryCol As gloCCDLibrary.gloPatientHistoryCol, ByVal RowOrder As Int64) As Boolean

        Dim _TempHistoryID As Long = 0
        Dim conn As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            Dim k As Int64 = 1
            k = RowOrder + 1
            _TempHistoryID = 0


            cmd = New SqlCommand("gsp_AddHistory", conn)
            cmd.CommandType = CommandType.StoredProcedure


            For Each oPatientHistory As gloCCDLibrary.gloPatientHistory In ogloPatientHistoryCol
                If oPatientHistory.Source <> "Current" Then
                    Dim Category_id As Int64 = GetCategoryID(oPatientHistory.HistoryCategory, "History")
                    If VerifyHistoryItemAvailability(oPatientHistory.HistoryItem, Category_id) = False Then
                        AddHistoryItemData(oPatientHistory.HistoryItem, "", Category_id)
                    End If
                End If
                If oPatientHistory.IsDeleted Then
                    If oPatientHistory.HistoryID <> 0 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Delete, "Deleted " & "Allergy" & " List", oPatientHistory.PatientID, oPatientHistory.HistoryID, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                    DeleteHistoryItemData(oPatientHistory.PatientID, oPatientHistory.HistoryID)
                    Continue For
                End If
                ''Added by Mayuri -Discussed with Saket on 20130227-we should save data against current visit
                Try
                    oPatientHistory.VisitID = GenerateVisitID(DateTime.Now, oPatientHistory.PatientID)
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.AddWithValue("@HistoryID", oPatientHistory.HistoryID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.AddWithValue("@VisitID", oPatientHistory.VisitID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", oPatientHistory.PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oPatientHistory.HistoryCategory  ''HistoryTable.HistoryCategory

                sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = oPatientHistory.HistoryItem   ''HistoryItem
                If Not IsNothing(oPatientHistory.Comments) Then
                    sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.Comments   '' Rows(i)(2) ''Comments
                Else
                    sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""   '' Rows(i)(2) ''Comments
                End If


                'If Not IsNothing(oPatientHistory.Reaction) Then

                '    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                '    sqlParam.Direction = ParameterDirection.Input
                '    sqlParam.Value = oPatientHistory.Reaction
                'Else
                '    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                '    sqlParam.Direction = ParameterDirection.Input
                '    sqlParam.Value = "|Active"
                'End If

                If IsNothing(oPatientHistory.Status) Then
                    oPatientHistory.Status = "Active"
                End If

                'If Not IsNothing(oPatientHistory.Reaction) Then
                '    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                '    sqlParam.Direction = ParameterDirection.Input
                '    sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                'Else
                '    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                '    sqlParam.Direction = ParameterDirection.Input
                '    sqlParam.Value = "|" + oPatientHistory.Status
                'End If
                If Not IsNothing(oPatientHistory.Reaction) Then
                    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input

                    If oPatientHistory.HistoryType <> "" Then
                        If oPatientHistory.HistoryType = "Fam" Then
                            If oPatientHistory.Reaction <> "" Then
                                sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                            Else
                                sqlParam.Value = oPatientHistory.Reaction & ":" & "|" & oPatientHistory.Status
                            End If

                        Else
                            sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                        End If
                    Else
                        Dim _HistoryTYpe As String = ""
                        _HistoryTYpe = gloImmHxReconciliation.GetHistoryType(oPatientHistory.HistoryCategory)
                        If _HistoryTYpe = "Fam" Then
                            If oPatientHistory.Reaction <> "" Then
                                sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                            Else
                                sqlParam.Value = oPatientHistory.Reaction & ":" & "|" & oPatientHistory.Status
                            End If

                        Else
                            sqlParam.Value = oPatientHistory.Reaction & "|" & oPatientHistory.Status
                        End If
                    End If
                Else
                    sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam.Value = "|" & oPatientHistory.Status

                End If
                If Not IsNothing(oPatientHistory.DrugID) Then
                    sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.DrugID   '' DrugID
                Else
                    sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = _TempHistoryID  '' c

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = GetPrefixTransactionID(0)

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
                Else
                    sqlParam = cmd.Parameters.Add("@DescID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If


                If Not IsNothing(oPatientHistory.SnoMedId) Then
                    sqlParam = cmd.Parameters.Add("@SnoMedID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.SnoMedId
                Else
                    sqlParam = cmd.Parameters.Add("@SnoMedID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                If Not IsNothing(oPatientHistory.SnoDescription) Then
                    sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.SnoDescription
                Else
                    sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                If Not IsNothing(oPatientHistory.ICD9) Then
                    sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.ICD9
                End If
                If Not IsNothing(oPatientHistory.RxNormCode) Then
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.RxNormCode
                Else
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                If Not IsNothing(oPatientHistory.CPT) Then
                    sqlParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.CPT
                Else
                    sqlParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                If Not IsNothing(oPatientHistory.OnsetDate) Then
                    sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.OnsetDate
                Else
                    sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = DBNull.Value
                End If

                If Not IsNothing(oPatientHistory.HistoryType) Then
                    sqlParam = cmd.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = oPatientHistory.HistoryType
                Else
                    sqlParam = cmd.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If oPatientHistory.UserName <> "" Then
                    sqlParam.Value = oPatientHistory.UserName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@sHistorySource", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If oPatientHistory.HistorySource <> "" Then
                    sqlParam.Value = oPatientHistory.HistorySource
                Else
                    sqlParam.Value = ""
                End If
                sqlParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.ConcernEndDate.Trim) Then
                    If oPatientHistory.ConcernEndDate.Trim <> "" Then
                        sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.ConcernEndDate.Trim), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = DBNull.Value
                    End If
                Else
                    sqlParam.Value = DBNull.Value
                End If
                sqlParam = cmd.Parameters.Add("@dtObservationEndDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(oPatientHistory.ObservationEndDate.Trim) Then
                    If oPatientHistory.ObservationEndDate.Trim <> "" Then
                        sqlParam.Value = Format(Convert.ToDateTime(oPatientHistory.ObservationEndDate.Trim), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = DBNull.Value
                    End If
                Else
                    sqlParam.Value = DBNull.Value
                End If
                sqlParam = cmd.Parameters.Add("@sProcstatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If oPatientHistory.HistorySource <> "" Then
                    sqlParam.Value = oPatientHistory.ConcernStatus
                Else
                    sqlParam.Value = ""
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

                    If oPatientHistory.Source <> "Current" Then
                        If oPatientHistory.Reaction <> "" Then
                            'Dim objCCDDatabaseLayer As New gloCCDDatabaseLayer
                            Dim _Reaction() As String
                            Dim _sReaction As String = ""
                            _Reaction = oPatientHistory.Reaction.Split("|")
                            If _Reaction.Length > 0 Then
                                _sReaction = _Reaction(0)
                                'objCCDDatabaseLayer.UpdateCategoryMaster(_sReaction, "Reaction", oPatientHistory.PatientID)
                            End If
                            'If Not IsNothing(objCCDDatabaseLayer) Then
                            '    objCCDDatabaseLayer.Dispose()
                            'End If

                        End If
                    End If


                Catch ex As Exception
                    Throw ex
                End Try


                cmd.Parameters.Clear()
                If IsNothing(sqlParam) = False Then
                    sqlParam = Nothing
                End If

                k = k + 1

                If oPatientHistory.HistoryID <> 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Modify, "Updated " & "Allergy" & " List", oPatientHistory.PatientID, oPatientHistory.HistoryID, 0, gloAuditTrail.ActivityOutCome.Success)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Add, "Accepted " & "Allergy" & " List", oPatientHistory.PatientID, _TempHistoryID, 0, gloAuditTrail.ActivityOutCome.Success)
                End If
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If


        End Try
    End Function
    Private Shared Function GetHistoryType(ByVal Category As String) As String

        Dim strQuery As String = ""
        Dim _result As New DataTable
        Dim _HistoryType As String = ""
        Dim sqladp As SqlDataAdapter = Nothing
        Try

            strQuery = "select isnull(sHistoryType,'') from category_mst WHERE sDescription= '" + Category.Replace("'", "''") + "'"

            sqladp = New SqlDataAdapter(strQuery, GetConnectionString())
            sqladp.Fill(_result)
            If Not IsNothing(_result) Then
                If _result.Rows.Count > 0 Then
                    _HistoryType = _result.Rows(0)(0)
                End If

            End If
            Return _HistoryType
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(sqladp) Then
                sqladp.Dispose()
                sqladp = Nothing
            End If
            If Not IsNothing(_result) Then
                _result.Dispose()
                _result = Nothing
            End If
            strQuery = Nothing
        End Try

    End Function
    Public Shared Function Fill_History(ByVal PatientID As Long, ByVal VisitID As Long, ByVal Flag As Integer) As DataTable
        Dim con As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("CCD_Reconcile_GetHistory", con)
            cmd.CommandType = CommandType.StoredProcedure


            objParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now.Date

            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            objParam = cmd.Parameters.Add("@VisitId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
    Public Shared Function SaveMedication(ByVal ogloMedicationCol As gloCCDLibrary.MedicationsCol) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        If conn.State = ConnectionState.Closed Then conn.Open()

        Dim objgloCCDDatabaselayer As gloCCDLibrary.gloCCDDatabaseLayer = Nothing

        Dim sqlParam As SqlParameter = Nothing

        Try
            For Each ogloMedication As gloCCDLibrary.Medication In ogloMedicationCol

                objgloCCDDatabaselayer = New gloCCDLibrary.gloCCDDatabaseLayer

                cmd = New SqlCommand("gsp_InUpMedication", conn)
                cmd.CommandType = CommandType.StoredProcedure

                sqlParam = cmd.Parameters.Add("@nMedicationID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = ogloMedication.MedicationID

                sqlParam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                ''Added by Mayuri as per discussion with Saket on 20130227-to save records against current visit
                ''sqlParam.Value = GenerateVisitID(DateTime.Now, ogloMedication.PatientID)
                Try
                    sqlParam.Value = GenerateVisitID(DateTime.Now, ogloMedication.PatientID)
                Catch ex As Exception

                End Try


                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ogloMedication.PatientID

                If Not IsNothing(ogloMedication.ProviderID) Then
                    sqlParam = cmd.Parameters.Add("@Rx_nProviderId", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ogloMedication.ProviderID
                Else
                    sqlParam = cmd.Parameters.Add("@Rx_nProviderId", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0
                End If


                sqlParam = cmd.Parameters.Add("@sMedication", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.DrugName) Then
                    sqlParam.Value = ogloMedication.DrugName
                    If sqlParam.Value = "" Then
                        MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    MessageBox.Show("Since there is no drug details available, therefore this will be not added in medication", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                If Not IsNothing(ogloMedication.DrugStrength) Then
                    sqlParam.Value = GetDosageForm(ogloMedication.DrugStrength)
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
                If Not IsNothing(ogloMedication.Duration) Then
                    sqlParam.Value = ogloMedication.Duration
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@dtStartdate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.StartDate) Then
                    If (ogloMedication.StartDate) <> "" Then
                        sqlParam.Value = Format(CDate(ogloMedication.StartDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = DateTime.Now
                    End If
                Else
                    sqlParam.Value = DateTime.Now
                End If



                sqlParam = cmd.Parameters.Add("@dtEnddate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.EndDate) Then
                    If (ogloMedication.EndDate) <> "" Then
                        sqlParam.Value = Format(CDate(ogloMedication.EndDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = System.DBNull.Value
                    End If
                Else
                    sqlParam.Value = System.DBNull.Value
                End If

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
                    If (ogloMedication.MedicationDate) <> "" Then
                        sqlParam.Value = Format(CDate(ogloMedication.MedicationDate), "MM/dd/yyyy")
                    Else
                        sqlParam.Value = DateTime.Now
                    End If
                Else
                    sqlParam.Value = DateTime.Now
                End If

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = GetPrefixTransactionID(0)

                sqlParam = cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Status) Then
                    sqlParam.Value = ogloMedication.Status
                Else
                    sqlParam.Value = ""
                End If
                ''sqlParam.Value = ogloMedication.Status


                sqlParam = cmd.Parameters.Add("@sReason", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Reason) Then
                    sqlParam.Value = ogloMedication.Reason
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.mpid) Then
                    sqlParam.Value = ogloMedication.mpid
                Else
                    sqlParam.Value = 0
                End If

                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If ogloMedication.User <> "" Then
                    sqlParam.Value = ogloMedication.User
                Else
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@nPrescriptionID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication._PrescriptionId) Then
                    sqlParam.Value = ogloMedication._PrescriptionId
                Else
                    sqlParam.Value = 0
                End If

                sqlParam = cmd.Parameters.Add("@sRenewed", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Renewed) Then
                    sqlParam.Value = ogloMedication.Renewed
                Else
                    sqlParam.Value = 0
                End If



                sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.ProdCode) Then
                    sqlParam.Value = ogloMedication.ProdCode
                    If sqlParam.Value = "" Then
                        System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.GenericName & ". It will not be added to medication history.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                    System.Windows.Forms.MessageBox.Show("No NDC code was found for " & ogloMedication.DrugName & ". It will not be added to medication history.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
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




                sqlParam = cmd.Parameters.Add("@nIsNarcotic", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.IsNarcotics) Then
                    sqlParam.Value = ogloMedication.IsNarcotics
                Else
                    sqlParam.Value = 0
                End If

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
                If Not IsNothing(ogloMedication.Rx_Notes) Then
                    sqlParam.Value = ogloMedication.Rx_Notes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_sMethod", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Rx_Method) Then
                    sqlParam.Value = ogloMedication.Rx_Method
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_bMaySubstitute", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.Rx_MaySubstitute) Then
                    sqlParam.Value = ogloMedication.Rx_MaySubstitute
                Else
                    sqlParam.Value = False
                End If

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
                If Not IsNothing(ogloMedication.Rx_PhName) Then
                    sqlParam.Value = ogloMedication.Rx_PhName
                Else
                    sqlParam.Value = ""
                End If

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
                If Not IsNothing(ogloMedication.State) Then
                    sqlParam.Value = ogloMedication.State
                Else
                    sqlParam.Value = ""
                End If
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
                If Not IsNothing(ogloMedication.Rx_PrescriberNotes) Then
                    sqlParam.Value = ogloMedication.Rx_PrescriberNotes
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Rx_eRxStatusMessage", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@sPBMSourceName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.PBMSourceName) Then
                    sqlParam.Value = ogloMedication.PBMSourceName
                Else
                    sqlParam.Value = ""
                End If

                sqlParam = cmd.Parameters.Add("@RxMed_DMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloMedication.RxMedDMSID) Then
                    sqlParam.Value = ogloMedication.RxMedDMSID
                Else
                    sqlParam.Value = 0
                End If

                If conn.State = ConnectionState.Closed Then conn.Open()
                Dim _medicationid As Int64
                cmd.ExecuteNonQuery()
                _medicationid = cmd.Parameters(0).Value
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
                If ogloMedication.MedicationID <> 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Modify, "Updated " & "Medication" & " List", ogloMedication.PatientID, ogloMedication.MedicationID, 0, gloAuditTrail.ActivityOutCome.Success)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Add, "Accepted " & "Medication" & " List", ogloMedication.PatientID, _medicationid, 0, gloAuditTrail.ActivityOutCome.Success)
                End If
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
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
        End Try
    End Function
    Public Shared Function SaveProblemList(ByVal ogloProblemCol As gloCCDLibrary.ProblemsCol) As Boolean
        Dim conn As New SqlConnection(GetConnectionString())
        Dim cmd As SqlCommand
        Dim ExamParam As SqlParameter = Nothing
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Try
            For Each ogloProblem As gloCCDLibrary.Problems In ogloProblemCol

                Try
                    If ogloProblem.ConcernStartDate <> "" Then
                        ogloProblem.EncounterId = GenerateVisitID(Convert.ToDateTime(ogloProblem.ConcernStartDate), ogloProblem.PatientID)
                    Else
                        ogloProblem.EncounterId = GenerateVisitID(DateTime.Now, ogloProblem.PatientID)
                    End If
                Catch ex As Exception

                End Try


                cmd = New SqlCommand("gsp_InUpProblemList", conn)
                cmd.CommandType = CommandType.StoredProcedure


                ExamParam = cmd.Parameters.AddWithValue("@PatientID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.PatientID

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
                ExamParam.Value = ogloProblem.UserID

                ExamParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = GetPrefixTransactionID(0)

                ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
                ExamParam.Direction = ParameterDirection.InputOutput
                ExamParam.Value = ogloProblem.ProblemID

                ExamParam = cmd.Parameters.Add("@RsDt", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ResolvedDate.Trim) Then
                    If ogloProblem.ResolvedDate.Trim <> "" Then
                        ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.ResolvedDate.Trim), "MM/dd/yyyy")
                    Else
                        ExamParam.Value = DBNull.Value
                    End If

                Else
                    ExamParam.Value = DBNull.Value
                End If


                ExamParam = cmd.Parameters.Add("@RsComment", SqlDbType.VarChar, 255)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.Immediacy


                ExamParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sProvider", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ProviderName) Then
                    ExamParam.Value = ogloProblem.ProviderName
                Else
                    ExamParam.Value = ""
                End If


                ExamParam = cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""


                ExamParam = cmd.Parameters.Add("@dtModifiedDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.ModifiedDate


                ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ""

                ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = ogloProblem.User

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
                If Not IsNothing(ogloProblem.Condition) AndAlso ogloProblem.Condition.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.Condition
                Else
                    ExamParam.Value = ""
                End If

                ExamParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.ConcernEndDate.Trim) Then
                    If ogloProblem.ConcernEndDate.Trim <> "" Then
                        ExamParam.Value = Format(Convert.ToDateTime(ogloProblem.ConcernEndDate.Trim), "MM/dd/yyyy")
                    Else
                        ExamParam.Value = DBNull.Value
                    End If

                Else
                    ExamParam.Value = DBNull.Value
                End If

                ExamParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar)
                ExamParam.Direction = ParameterDirection.Input
                If Not IsNothing(ogloProblem.sConcernStatus) AndAlso ogloProblem.sConcernStatus.ToString() <> "" Then
                    ExamParam.Value = ogloProblem.sConcernStatus
                Else
                    ExamParam.Value = ""
                End If

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.ExecuteNonQuery()
                Dim _ProblemID As Int64
                _ProblemID = cmd.Parameters("@ProblemID").Value
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If IsNothing(ExamParam) = False Then
                    ExamParam = Nothing
                End If
                '   End If
                If ogloProblem.ProblemID <> 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Modify, "Updated " & "Problem" & " List", ogloProblem.PatientID, ogloProblem.ProblemID, 0, gloAuditTrail.ActivityOutCome.Success)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Add, "Accepted " & "Problem" & " List", ogloProblem.PatientID, _ProblemID, 0, gloAuditTrail.ActivityOutCome.Success)
                End If
            Next
            Return True
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False

        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(ExamParam) = False Then
                ExamParam = Nothing
            End If
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Public Shared Function GetDosageForm(ByVal sNDCCode As String) As String

        Dim strQuery As String = ""
        Dim _dt As New DataTable
        Dim sqladp As SqlDataAdapter = Nothing
        Dim _result As String = String.Empty
        Try

            strQuery = "Select isnull(sDosage,'') as sDosage From Drugs_Mst Where sNDCCode = '" & sNDCCode & "'"

            sqladp = New SqlDataAdapter(strQuery, GetConnectionString())
            sqladp.Fill(_dt)
            If _dt.Rows.Count > 0 Then
                _result = (CType(_dt.Rows(0)("sDosage"), String))
            End If
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            strQuery = Nothing
            If Not IsNothing(sqladp) Then
                sqladp.Dispose()
                sqladp = Nothing
            End If
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If
        End Try

    End Function
    Public Shared Function DateFromHL7(ByVal strDate As String) As Date
        Dim _Date As Date = Now
        Dim _DateAsString As String = Nothing
        Try

            If Len(strDate) < 8 Then
                DateFromHL7 = Nothing
                Exit Function
            End If

            _DateAsString = Mid(strDate, 1, 8)


            _Date = DateSerial(Mid(_DateAsString, 1, 4), Val(Mid(_DateAsString, 5, 2)), Val(Mid(_DateAsString, 7, 2)))
            If IsDate(_Date) = False Then
                _Date = "12:00:00 AM"
            End If

            Return _Date
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _DateAsString = Nothing
        End Try
    End Function

#End Region

#Region "Private Functions"

    Private Shared Function GenerateTempFileName(ByVal _path As String, ByVal _extension As String) As String

        'Dim strListName As String = ""

        Try

            'Dim _NewDocumentName As String = ""
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _extension
            'While File.Exists(_path & _NewDocumentName) = True And i < Int16.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _extension
            'End While

            'strListName = _path & _NewDocumentName
            '_dtCurrentDateTime = Nothing
            Return gloGlobal.clsFileExtensions.NewDocumentName(_path, _extension, "MMddyyyyHHmmssffff")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
            Return Nothing
        End Try

        'Return strListName

    End Function

    Private Shared Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String

        'Dim stream As MemoryStream = Nothing
        Dim content() As Byte = Nothing

        Try
            If Not cntFromDB Is Nothing Then

                content = CType(cntFromDB, Byte())
                ' stream = New MemoryStream(content)

                Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
                If IsNothing(oFile) = False Then
                    '    stream.WriteTo(oFile)
                    oFile.Write(content, 0, content.Length)
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                End If

                Return strFileName
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            'If Not IsNothing(stream) Then
            '    stream.Close()
            '    stream.Dispose()
            '    stream = Nothing
            'End If
            If Not IsNothing(content) Then
                content = Nothing
            End If
        End Try

    End Function

    Private Shared Function SaveReconciledInformation(ByVal oReconcileList As gloCCDLibrary.ReconcileList) As String

        Dim _GeneratedLists As String = ""
        '   Dim conn As New SqlConnection

        Dim objCCDDatabaseLayer As gloCCDLibrary.gloCCDDatabaseLayer = New gloCCDLibrary.gloCCDDatabaseLayer()
        Dim ogloPatientRegDBLayer As gloCCDLibrary.gloPatientRegDBLayer = New gloCCDLibrary.gloPatientRegDBLayer(GetConnectionString())
        Dim ogloPatient As gloPatient.gloPatient = New gloPatient.gloPatient(GetConnectionString())
        Try

            If Not IsNothing(oReconcileList) Then


                If Not IsNothing(oReconcileList.mPatient) Then

                    '' Patient Allergy History
                    If Not IsNothing(oReconcileList.mPatient.PatientHistory) Then
                        If oReconcileList.mPatient.PatientHistory.Count > 0 Then

                            Dim _bIsHistoryList As Boolean
                            _bIsHistoryList = ogloPatientRegDBLayer.SaveReconcilationHistoryLists(oReconcileList)

                            If _bIsHistoryList = True Then
                                If _GeneratedLists <> "" Then
                                    _GeneratedLists = _GeneratedLists & "," & " Med Allergy"
                                Else
                                    _GeneratedLists = "Med Allergy"
                                End If
                            End If

                        End If

                    End If

                    '' Problems
                    If Not IsNothing(oReconcileList.mPatient.PatientProblems) Then
                        If oReconcileList.mPatient.PatientProblems.Count > 0 Then


                            Dim _bIsProblemList As Boolean

                            _bIsProblemList = ogloPatientRegDBLayer.SaveReconcilationProblemLists(oReconcileList)

                            If _bIsProblemList = True Then
                                If _GeneratedLists <> "" Then
                                    _GeneratedLists = _GeneratedLists & "," & " Problem"
                                Else
                                    _GeneratedLists = "Problem"
                                End If
                            End If

                        End If
                    End If


                    ''Medication
                    If Not IsNothing(oReconcileList.mPatient.PatientMedications) Then
                        If oReconcileList.mPatient.PatientMedications.Count > 0 Then

                            Dim _bIsMedicationList As Boolean
                            _bIsMedicationList = ogloPatientRegDBLayer.SaveReconcilationMedicationLists(oReconcileList)

                            If _bIsMedicationList = True Then
                                If _GeneratedLists <> "" Then
                                    _GeneratedLists = _GeneratedLists & "," & " Medication"
                                Else
                                    _GeneratedLists = "Medication"
                                End If
                            End If

                        End If
                    End If

                End If



                If _GeneratedLists <> "" Then
                    _GeneratedLists = _GeneratedLists & " List Generated. "
                    ogloPatientRegDBLayer.UpdateStatus(oReconcileList.CCDID, oReconcileList.PatientID, 0, True, False, CCDFileStatus.ListExtracted)
                Else
                    ogloPatientRegDBLayer.UpdateStatus(oReconcileList.CCDID, oReconcileList.PatientID, 0, True, False, CCDFileStatus.NoList)

                End If

            End If 'oReconcileList <> Null



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            If Not IsNothing(objCCDDatabaseLayer) Then
                objCCDDatabaseLayer.Dispose()
                objCCDDatabaseLayer = Nothing
            End If
            If Not IsNothing(ogloPatientRegDBLayer) Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
            If Not IsNothing(ogloPatient) Then
                ogloPatient.Dispose()
                ogloPatient = Nothing
            End If
        End Try

        Return _GeneratedLists

    End Function

    Private Shared Function GetCategoryID(ByVal CategoryName As String, Optional ByVal CategoryType As String = "History") As Int64
        Dim conn As New SqlConnection(GetConnectionString())
        Dim StrQuery As String = Nothing
        Try
            StrQuery = "Select nCategoryID from Category_mst where sDescription='" & Replace(CategoryName.Trim(), "'", "''") & "' and scategoryType='" & CategoryType & "'"
            Dim Cmd As New SqlClient.SqlCommand(StrQuery, conn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim intReturn As Int64 = Cmd.ExecuteScalar()
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Return intReturn
        Catch ex As Exception
            Throw
        Finally
            StrQuery = Nothing
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Private Shared Function VerifyHistoryItemAvailability(ByVal ItemName As String, ByVal CategoryID As Int64) As Boolean
        Dim conn As New SqlConnection(GetConnectionString())
        Dim StrQuery As String = Nothing
        Try


            StrQuery = "Select count(nHistoryID) from History_mst where sDescription='" & Replace(ItemName.Trim(), "'", "''") & "' and ncategoryId=" & CategoryID
            Dim Cmd As New SqlClient.SqlCommand(StrQuery, conn)
            Dim intReturn As Int64 = 0
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            intReturn = Cmd.ExecuteScalar()
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If intReturn > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw
        Finally
            StrQuery = Nothing
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Shared Function AddHistoryItemData(ByVal str1 As String, ByVal str2 As String, ByVal CategoryID As Long) As Long
        Dim conn As New SqlConnection(GetConnectionString())
        Dim objParam As SqlClient.SqlParameter = Nothing
        Dim Cmd As SqlClient.SqlCommand = Nothing
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
            objParam.Value = GetPrefixTransactionID(0)

            objParam = Cmd.Parameters.Add("@HistoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ""

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Cmd.ExecuteNonQuery()

            '  Return objParam.Value

        Catch ex As Exception
            Throw
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(objParam) = False Then
                objParam = Nothing
            End If
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Private Shared Function DeleteHistoryItemData(ByVal PatientID As Long, ByVal HistoryID As Long) As Long
        Dim conn As New SqlConnection(GetConnectionString())
        Dim Cmd As SqlClient.SqlCommand = Nothing
        Dim objParam As SqlClient.SqlParameter = Nothing

        Try

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeleteHistoryData", conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = Cmd.Parameters.Add("@HistoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryID

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If IsNothing(conn) = False Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Public Shared Function GenerateVisitID(ByVal VisitDate As DateTime, ByVal PatientID As Int64) As Int64

        'Dim addresult As Object = Nothing
        Dim cmdVisits As SqlCommand = Nothing
        Dim objParam As New SqlParameter()
        Dim objFlagParam As New SqlParameter()
        Dim conn As New SqlConnection(GetConnectionString())
        Dim _VisitID As Int64 = 0
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
            objParam.Value = GetPrefixTransactionID(PatientID)

            objParam = cmdVisits.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output
            objParam.Value = 0

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmdVisits.ExecuteNonQuery()

            _VisitID = DirectCast(objParam.Value, Int64)

        Catch ex As Exception
            Throw
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
            If Not IsNothing(cmdVisits) Then
                cmdVisits.Parameters.Clear()
                cmdVisits.Dispose()
                cmdVisits = Nothing

            End If
            If Not IsNothing(objFlagParam) Then
                objFlagParam = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
        End Try
        Return _VisitID
    End Function

#End Region


End Class