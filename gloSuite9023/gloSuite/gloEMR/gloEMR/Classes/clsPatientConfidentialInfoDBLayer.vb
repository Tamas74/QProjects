Imports System.Data
Imports System.Data.SqlClient

Public Class clsPatientConfidentialInfoDBLayer
    Implements IDisposable

    Public Sub New(ByVal PatientID As Long)
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        _PatientID = PatientID
    End Sub

    Private Conn As SqlConnection
    '  Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    '  Private Ds As System.Data.DataSet
    Private Dv As DataView
    '  Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand
    Private _userid As Int64 = 0
    Private _username As String = ""
    Dim strsql As String = ""
    Dim _PatientID As Long

    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get

    End Property

    Public Function FillControls(ByVal btnstatus As Int16) As DataTable
        Dim objParam As SqlParameter
        Try
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If btnstatus = 1 Then
                Dim Cmd As SqlCommand = New SqlCommand("gsp_FillUsers", Conn)
                Cmd.CommandType = CommandType.StoredProcedure

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 1
                'User_MST.nUserId, User_MST.sLoginName ,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') , Provider_MST.nProviderID , ISNULL(Provider_MST.sFirstName,'') +' '+ ISNULL(Provider_MST.sLastName,'')  
                ''ElseIf btnstatus = 2 Then
                ''    'nPatientID, sPatientCode ,ISNULL(sFirstName,'') , ISNULL( sLastName,'')
                ''    Cmd = New SqlCommand("gsp_ViewPatient", Conn)
                ''    Cmd.CommandType = CommandType.StoredProcedure

                Dim adpt As New SqlDataAdapter
                Dim dt As New DataTable


                adpt.SelectCommand = Cmd
                adpt.Fill(dt)
              
                Dv = New DataView(dt.Copy())
                adpt.Dispose()
                adpt = Nothing
                If Cmd IsNot Nothing Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                Conn.Close()
                Return dt
            Else
                Return Nothing
            End If
            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()

            'Do While dreader.Read
            '    Dim i As Integer
            '    i = dreader("nSpecialtyID")

            'Loop
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
            objParam = Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            objParam = Nothing
        End Try
    End Function

    '' if Confedential information is present for selected VisitID 
    Public Function GetConfidentialInfo(ByVal VisitID As Int64, Optional ByVal ConfidentialID As Int64 = 0) As DataTable
        Try
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable

            Dim Cmd As SqlCommand = New SqlCommand
            Cmd.Connection = Conn
            Cmd.CommandType = CommandType.Text
            'strsql = "SELECT     ISNULL(PatientConfidentialInfo.sDescription,'') AS sDescription  , ISNULL(PatientConfidentialInfoDetails.nUserId,0) AS nUserId , ISNULL(PatientConfidentialInfoDetails.sUserName,'') AS sUserName " _
            '        & " FROM  PatientConfidentialInfo INNER JOIN " _
            '        & " PatientConfidentialInfoDetails ON PatientConfidentialInfo.nConfidentialId = PatientConfidentialInfoDetails.nConfidentialId " _
            '        & " WHERE PatientConfidentialInfo.nVisitId = " & VisitID

            strsql = "SELECT  ISNULL(PatientConfidentialInfo.sDescription,'') AS sDescription , " _
    & " ISNULL(PatientConfidentialInfoDetails.sUserName,'') AS sUserName ,  " _
    & " ISNULL(PatientConfidentialInfo.nPatientId,0) AS nPatientId , " _
    & " ISNULL(PatientConfidentialInfo.nVisitId,0) AS nVisitId ,  " _
    & " ISNULL(PatientConfidentialInfo.nExamId,0) as  nExamId , " _
            & " PatientConfidentialInfo.dtVisitDate, " _
    & " ISNULL(PatientConfidentialInfo.bIsActive,0) AS bIsActive , " _
    & " ISNULL(PatientConfidentialInfoDetails.nUserId,0) AS nUserId  " _
                     & " FROM  PatientConfidentialInfo INNER JOIN " _
                    & " PatientConfidentialInfoDetails ON PatientConfidentialInfo.nConfidentialId = PatientConfidentialInfoDetails.nConfidentialId " _
                        & " WHERE PatientConfidentialInfo.nVisitId = " & VisitID

            Cmd.CommandText = strsql
            Conn.Open()
            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            adpt.Dispose()
            adpt = Nothing

            Conn.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try

    End Function

    Public Function AddNewPatientConfidentialID(ByVal Pid As Int64, ByVal confidentialid As Int64, ByVal discription As String, ByVal nVisitId As Int64, ByVal nExamId As Int64, ByVal dtDateTime As Date, ByVal _isActive As Boolean) As Int64
        Dim _confidentialID As Int64 = 0
        Try

            Dim cmd As New SqlCommand("gsp_InUpPatientConfidentialInfo", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@PConfidentialID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = confidentialid

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Pid

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = discription

            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitId

            sqlParam = cmd.Parameters.Add("@nExamID ", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nExamId


            sqlParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtDateTime

            sqlParam = cmd.Parameters.Add("@bIsActive", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _isActive

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            Conn.Open()
            cmd.ExecuteNonQuery()

            If Not IsNothing(cmd.Parameters("@PConfidentialID").Value) Then
                _confidentialID = CType(cmd.Parameters("@PConfidentialID").Value, Int64)
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            Return _confidentialID

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try

    End Function

    ''Comment By Sudhir 20090212
    ''Changes According to New ListControl.. 
    'Public Function AddPatientConfidentialDetails(ByVal _ConfidentialID As Int64, ByVal arylist As ArrayList)

    '    Try
    '        Dim j As Int32 = 0

    '        '1 Delete previous record against _ConfidentialID
    '        Cmd = New SqlCommand
    '        Cmd.Connection = Conn
    '        Cmd.CommandType = CommandType.Text
    '        strsql = "Delete from PatientConfidentialInfoDetails where nConfidentialId = " & _ConfidentialID & ""
    '        Cmd.CommandText = strsql
    '        Conn.Open()
    '        Cmd.ExecuteNonQuery()

    '        '2 insert new user & uasername against _ConfidentialID
    '        If arylist.Count > 0 Then
    '            For j = 0 To arylist.Count - 1
    '                _userid = CType(arylist.Item(j), myList).Index
    '                _username = CType(arylist.Item(j), myList).Description

    '                strsql = "insert into PatientConfidentialInfoDetails(nConfidentialId, nUserId, sUserName) values(" & _ConfidentialID & "," & _userid & ",'" & _username & "' )"
    '                Cmd.CommandText = strsql
    '                Cmd.ExecuteNonQuery()
    '                Cmd.Dispose()
    '            Next

    '        End If
    '        Conn.Close()


    '    Catch ex As Exception
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '    End Try
    'End Function

    ''sudhir 20090212 - New ListControl Passes ToList Object having User Info.
    Public Sub AddPatientConfidentialDetails(ByVal _ConfidentialID As Int64, ByVal ToList As gloGeneralItem.gloItems)

        Try
            Dim j As Int32 = 0

            '1 Delete previous record against _ConfidentialID
            Dim Cmd As SqlCommand = New SqlCommand
            Cmd.Connection = Conn
            Cmd.CommandType = CommandType.Text
            strsql = "Delete from PatientConfidentialInfoDetails where nConfidentialId = " & _ConfidentialID & ""
            Cmd.CommandText = strsql
            Conn.Open()
            Cmd.ExecuteNonQuery()

            '2 insert new user & uasername against _ConfidentialID
            If ToList.Count > 0 Then
                For j = 0 To ToList.Count - 1
                    _userid = ToList(j).ID
                    _username = ToList(j).Description

                    strsql = "insert into PatientConfidentialInfoDetails(nConfidentialId, nUserId, sUserName) values(" & _ConfidentialID & "," & _userid & ",'" & _username & "' )"
                    Cmd.CommandText = strsql
                    Cmd.ExecuteNonQuery()
                    Cmd.Parameters.Clear()
                Next

            End If
            Conn.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

        End Try

    End Sub

    Public Function GetPatientConfidentailInfoStatus(ByVal VisitID As Int64) As String
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Dim sqlConfidentailIDParam As SqlParameter
        Dim sqlStatusParam As SqlParameter
        Dim Conn = New SqlConnection(GetConnectionString)
        Try

            cmd = New SqlCommand("gsp_CheckPatientConfidentialInfo", Conn)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            'Lines commented by dipak 20100906 for cASE UC5070.003 as gnPatientID replaced by local variable
            'sqlParam.Value = gnPatientID
            sqlParam.Value = _PatientID
            'end modification 
            sqlParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnLoginID

            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            'If VisitID = 0 Then
            '    VisitID = gnVisitID
            'End If
            sqlParam.Value = VisitID

            sqlConfidentailIDParam = cmd.Parameters.Add("@ConfidentialID", SqlDbType.BigInt)
            sqlConfidentailIDParam.Direction = ParameterDirection.Output
            'sqlParam.Value = _confidentialid

            sqlStatusParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
            sqlStatusParam.Direction = ParameterDirection.Output
            'sqlParam.Value = _status
            Conn.Open()
            cmd.ExecuteNonQuery()

            If IsNothing(sqlConfidentailIDParam.Value) = False AndAlso IsNothing(sqlStatusParam.Value) = False Then
                Return (sqlConfidentailIDParam.Value & "," & sqlStatusParam.Value)
            Else
                Return (",")
            End If

            ''Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Conn.Close()
            Conn.Dispose()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            sqlConfidentailIDParam = Nothing
            sqlStatusParam = Nothing
        End Try

    End Function





    'Public Function CheckConfidentialStatus(ByVal Pid As Int64, ByVal Uid As Int64) As Int64

    '    Dim _confidentialid As Int64 = 0
    '    Dim _status As Int32 = 0
    '    Try
    '        Dim sqlconn As String
    '        sqlconn = GetConnectionString()
    '        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

    '        Dim cmd As New SqlCommand("gsp_CheckPatientConfidentialInfo", Conn)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter

    '        sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = gnPatientID

    '        sqlParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = Uid

    '        sqlParam = cmd.Parameters.Add("@PConfidentialID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Output
    '        'sqlParam.Value = _confidentialid

    '        sqlParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
    '        sqlParam.Direction = ParameterDirection.Output

    '        Conn.Open()
    '        cmd.ExecuteNonQuery()

    '        If Not IsDBNull(cmd.Parameters("@PConfidentialID").Value) Then
    '            _confidentialid = CType(cmd.Parameters("@PConfidentialID").Value, Int64)
    '        End If

    '        If Not IsDBNull(cmd.Parameters("@Status").Value) Then
    '            _status = CType(cmd.Parameters("@Status").Value, Int32)

    '            If _status = 3 And _confidentialid = 0 Then
    '                Exit Function
    '            Else
    '                If _confidentialid <> 0 Then
    '                    'fill

    '                Else
    '                    MessageBox.Show("Confidential Information not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Return 0
    '                End If

    '            End If
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '    End Try
    'End Function



#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Dv IsNot Nothing Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                If Conn IsNot Nothing Then
                    Conn.Dispose()
                    Conn = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
