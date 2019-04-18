Imports System.Data.SqlClient


Public Enum CCDFileStatus
    Imported = 1
    ListExtracted = 2
    NoList = 3
End Enum

Public Enum ListStatus
    Ready = 1
    Finished = 2

End Enum

Public Class gloCCDReconcilation
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
    Public Function IsSamePatientAsDashboard(ByVal objPatient As gloCCDLibrary.Patient, ByVal DashBoardPatientID As Long) As String

        Dim conn As SqlConnection = Nothing
        Dim cmd As New SqlCommand
        Dim strQuery As String = ""
        Dim _Result As String = ""
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_SamePatientAsDashboard"
            'strQuery = "IF Exists(SELECT 'True' FROM Patient where sFirstName='" & objPatient.PatientDemographics.DemographicsDetail.PatientFirstName & "' and sLastName='" & objPatient.PatientDemographics.DemographicsDetail.PatientLastName & "' " _
            '& " and dtDOB='" & objPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToShortDateString() & "' and sGender='" & objPatient.PatientDemographics.DemographicsDetail.PatientGender & "' and nPatientID=" & DashBoardPatientID _
            '& " ) Select 'True' Else Select upper(sLastName) + ', '+ sFirstName +' '+ Convert(Varchar(10),dtDOB,101) + ' '+  isnull(sGender,'') FROM Patient  where nPatientID = " & DashBoardPatientID

            'cmd.CommandText = ""



            sqlParam = cmd.Parameters.Add("@nDashBoardPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DashBoardPatientID

            sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientFirstName

            sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientLastName


            sqlParam = cmd.Parameters.Add("@dtDOB", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy")

            sqlParam = cmd.Parameters.Add("@sGender", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientGender


            sqlParam = cmd.Parameters.Add("@sPatient", SqlDbType.VarChar, 8000)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = ""



            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()


            _Result = cmd.Parameters("@sPatient").Value.ToString()

            'If Not IsNothing(temp) Then
            '    _Result = CType(temp, String)
            'End If

            Return _Result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return " "
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

    Public Function GetPatientIDFromFile(ByVal objPatient As gloCCDLibrary.Patient) As Long

        Dim conn As SqlConnection = Nothing
        Dim cmd As New SqlCommand
        'Dim strQuery As String = ""
        Dim _Result As Long = 0
        Dim sqlParam As SqlParameter = Nothing

        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SureScriptGetPatientIDFromFile"
         
            sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientFirstName

            sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientLastName


            sqlParam = cmd.Parameters.Add("@dtDOB", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientDOB.ToString("MM/dd/yyyy")

            sqlParam = cmd.Parameters.Add("@sGender", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = objPatient.PatientDemographics.DemographicsDetail.PatientGender

            'Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
            'If Not IsNothing(objPatient.PatientProviders) AndAlso objPatient.PatientProviders.Count > 0 Then
            '    oProvider = objPatient.PatientProviders.Item(0)

            '    sqlParam = cmd.Parameters.Add("@sProviderFirstName", SqlDbType.VarChar)
            '    sqlParam.Direction = ParameterDirection.Input
            '    sqlParam.Value = oProvider.FirstName

            '    sqlParam = cmd.Parameters.Add("@sProviderLastName", SqlDbType.VarChar)
            '    sqlParam.Direction = ParameterDirection.Input
            '    sqlParam.Value = oProvider.LastName
            'Else
            '    Return 0
            'End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            _Result = cmd.ExecuteScalar()

            Return _Result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return " "
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

    Public Function GetDashBoardPatient(ByVal DashBoardPatientID As Long) As gloPatient.Patient
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim oPatient As New gloPatient.Patient
        Dim DtPatient As New DataTable
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "Select sLastName as LastName, sFirstName As FirstName ,Convert(Varchar(10),dtDOB,101) as DOB,  isnull(sGender,'') as Gender , sPatientCode as PatientCode FROM Patient  where nPatientID = " & DashBoardPatientID

            cmd.CommandText = strQuery

            Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter(cmd)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            DataAdapter.Fill(DtPatient)
            DataAdapter.Dispose()
            DataAdapter = Nothing

            If Not IsNothing(DtPatient) Then
                If DtPatient.Rows.Count > 0 Then
                    '_Patient.PatientName.ID = DashBoardPatientID
                    '_Patient.PatientName.Code = DtPatient.Rows(0)("PatientCode").ToString()
                    '_Patient.PatientName.FirstName = DtPatient.Rows(0)("FirstName").ToString()
                    '_Patient.PatientName.LastName = DtPatient.Rows(0)("LastName").ToString()
                    '_Patient.DateofBirth = DtPatient.Rows(0)("DOB").ToString()
                    '_Patient.Gender = DtPatient.Rows(0)("Gender").ToString()
                    oPatient.DemographicsDetail.PatientFirstName = DtPatient.Rows(0)("FirstName").ToString()
                    oPatient.DemographicsDetail.PatientLastName = DtPatient.Rows(0)("LastName").ToString()
                    oPatient.DemographicsDetail.PatientDOB = DtPatient.Rows(0)("DOB").ToString()
                    oPatient.DemographicsDetail.PatientCode = DtPatient.Rows(0)("PatientCode").ToString()
                    oPatient.DemographicsDetail.PatientGender = DtPatient.Rows(0)("Gender").ToString()

                End If
            End If

            Return oPatient

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            If Not IsNothing(DtPatient) Then
                DtPatient.Dispose()
                DtPatient = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()

            End If
            conn.Dispose()
            strQuery = Nothing
        End Try
    End Function

    Public Function GetReconcilationList(ByVal PatientID As Long, Optional ByVal SourceName As String = "") As DataTable
        Dim cmd As New SqlCommand
        Dim conn As New SqlConnection
        'Dim strQuery As String = ""
        'Dim _Patient As New gloCCDLibrary.Patient
        Dim DtReconcilationList As New DataTable
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
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


    Public Function GetExtractedList(ByVal ListID As Long, ByVal ListType As String, ByVal PatientID As Long) As DataTable
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        'Dim strQuery As String = ""
        'Dim _Patient As New gloCCDLibrary.Patient
        Dim DtReconcilationList As New DataTable
        Dim sqlParam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_GetExtractedList_forReconcilation"



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

    Public Function IsReadyListsPresent(ByVal _PatientID As Int64, ByVal _ModuleName As String) As Boolean
        '      Dim objgloCCDReconcilation As New gloCCDLibrary.gloCCDReconcilation
        Dim _dtList As DataTable = Nothing
        Dim Dv As DataView = Nothing

        Try
            _dtList = GetReconcilationList(_PatientID)
            If Not IsNothing(_dtList) Then
                Dv = _dtList.DefaultView()
                Dv.RowFilter = "sListType = '" & _ModuleName & "' And  sListStatus='Ready'"
                Dim dt As DataTable = Nothing
                dt = Dv.ToTable()
                If Not IsNothing(dt) Then
                    If (dt.Rows.Count) > 0 Then
                        dt.Dispose()
                        Return True
                    Else
                        dt.Dispose()
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return (Nothing)
        Finally
            'If IsNothing(objgloCCDReconcilation) = False Then
            '    objgloCCDReconcilation = Nothing
            'End If
            If IsNothing(_dtList) = False Then
                _dtList.Dispose()
                _dtList = Nothing
            End If
            If IsNothing(Dv) = False Then
                Dv.Dispose()
                Dv = Nothing
            End If
        End Try
    End Function
    Public Function IsImportedFilePresent(ByVal PatientID As Long) As Boolean
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        'Dim strQuery As String = ""
        Dim sqlParam As SqlParameter = Nothing

        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "CCD_Reconcile_IsUnfinishedFiles"


            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID



            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim rowAffected As Long
            rowAffected = CType(cmd.ExecuteScalar, Long)

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
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
            conn = Nothing

        End Try
    End Function
    Public Function GetUnFinishedReconcileList(ByVal PatientID As Long) As DataTable
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        'Dim strQuery As String = ""
        Dim sqlParam As SqlParameter = Nothing
        Dim DtReconcilationList As New DataTable
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
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
