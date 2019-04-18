Imports System.Data.SqlClient
Imports System.Data
Public Class gloSureScriptDBLayer
    Implements IDisposable
    ''' <summary>
    ''' Insert data into SurescriptMessageTransaction
    ''' </summary>
    ''' <param name="oMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertintoMessageTransaction(ByVal oMessage As SureScriptMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        conn.Open()
        Dim sql As SqlCommand
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            Dim intstatus As Int16 = 0
            _strsql = "Insert into SurescriptMessageTransaction (nMessageID,sMessageName,sRelatesToMessageID,sMessageFrom,sMessageTo,sDateTimeStamp,dtDateReceived,sReferenceNumber,IsAlertCheck) values ('" & oMessage.MessageID & "','" & oMessage.MessageName & "','" & oMessage.RelatesToMessageId & "','" & oMessage.MessageFrom & "','" & oMessage.MessageTo & "','" & oMessage.DateTimeStamp & "','" & oMessage.DateReceived & "','" & oMessage.TransactionID & "'," & intstatus & ")"
            sql.CommandText = _strsql
            sql.Connection = conn

            sql.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    ''' <summary>
    ''' Insert data into Error Details
    ''' </summary>
    ''' <param name="objError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertErrorDetails(ByVal objError As SureScriptErrorMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        Try
            conn.Open()
            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "delete from ErrorTransaction"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "delete from SureScriptMessageTransaction where smessagename='Error'"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "select isnull(max(isnull(nTransactionId,0)),0)+1 from ErrorTransaction"
            sql.CommandText = _strsql

            Dim Id As Int64 = sql.ExecuteScalar

            objError.TransactionID = Id.ToString

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text

            _strsql = "Insert into ErrorTransaction values (nTransactionID,sErrorCode,sDescriptionCode,sDescription) (" & objError.TransactionID & " ,'" & objError.ErrorCode & "','" & objError.DescriptionCode & "','" & objError.Description & "')"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            _strsql = ""
            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
   
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
   
  
    Public Function GetErrorList() As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
      
        conn.Open()
        Dim sql As SqlCommand
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sexternalcode,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            _strsql = "select distinct e.nTransactionID ,s.sRelatesToMessageID from ErrorTransaction e inner join SureScriptMessageTransaction s on Convert(varchar(50),e.nTransactionID)=s.sReferenceNumber where s.sMessageName='Error'"

            sql.CommandText = _strsql
            sql.Connection = conn

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    Public Function GetErrorMessageDetails(ByVal objError As SureScriptErrorMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)

        conn.Open()
        Dim sql As SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sexternalcode,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            _strsql = "select isnull(e.sErrorCode,''),isnull(e.sDescriptionCode,''),isnull(e.sDescription,''),isnull(s1.sMessageName,''),isnull(s1.sReferenceNumber,''),s1.nMessageID, s1.sRelatesToMessageID,s1.dtDateReceived from ErrorTransaction  e  inner join SureScriptMessageTransaction s on convert(varchar(50),e.nTransactionID) = s.sReferenceNumber inner join SureScriptMessageTransaction s1 on s.sRelatesToMessageID= s1.nMessageID where e.nTransactionID= " & objError.TransactionID & " and s.sMessageName='Error'"

            sql.CommandText = _strsql
            sql.Connection = conn
            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            Dim sReferenceNumber As String
            If dt.Rows.Count > 0 Then
                objError.ErrorCode = dt.Rows(0)(0)
                objError.DescriptionCode = dt.Rows(0)(1)
                objError.Description = dt.Rows(0)(2)
                objError.RelatesToMessageName = dt.Rows(0)(3)
                sReferenceNumber = dt.Rows(0)(4)
            End If

            sql.Dispose()
            sql = Nothing

            sqladpt.Dispose()
            sqladpt = Nothing

            Select Case objError.RelatesToMessageName

                Case "RefillRequest"
                    _strsql = "select distinct nRxTransactionId from PrescriptionRefillTransactiondetail where sRxReferenceNumber= '" & sReferenceNumber & "'"
                    sql.CommandText = _strsql
                    sql.Connection = conn
                    'objError.PrescriptionObject.RxTransactionID = sql.ExecuteScalar()
                    ' GetNewPrescription(objError.PrescriptionObject)

                Case "NewRx", "RefillResponse"
                    'objError.PrescriptionObject.RxTransactionID = sReferenceNumber
                    'GetNewPrescription(objError.PrescriptionObject)
                    'objError.PrescriptionObject.DrugsCol.Item(0).MessageID = dt.Rows(0)(5)
                    'objError.PrescriptionObject.DrugsCol.Item(0).RelatesToMessageId = dt.Rows(0)(6)
                    'objError.PrescriptionObject.DrugsCol.Item(0).DateReceived = dt.Rows(0)(7)
                Case "Verify"

            End Select
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function




    '*************************function written by Sagaar k. on 16 feb 2008
    Public Function GetErrorTransaction_Details() As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "select distinct e.nTransactionID ,isnull(s.sRelatesToMessageID,''),sErrorCode,sDescriptionCode,sDescription from ErrorTransaction e inner join SureScriptMessageTransaction s on Convert(varchar(50),e.nTransactionID)=s.sReferenceNumber where s.sMessageName='Error'"
            sql.CommandText = _strsql
            conn.Open()

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    '*****************function written by Sagaar k. on 16 feb 2008

    
    ''' <summary>
    ''' Insert Acknowledgements
    ''' </summary>
    ''' <param name="objStatus"></param>
    ''' <param name="blnType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertAcknowledgements(ByVal objStatus As StatusMessage, ByVal blnType As Boolean) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        conn.Open()
        Try
            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "select isnull(max(isnull(nTransactionId,0)),0)+1 from AcknowledgementTransaction"
            sql.CommandText = _strsql

            Dim Id As Int64 = sql.ExecuteScalar

            objStatus.TransactionID = Id.ToString

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            If blnType Then
                _strsql = "Insert into AcknowledgementTransaction (nTransactionID,sCode,sType) values ( '" & objStatus.TransactionID & "','" & objStatus.StatusCode & "',1)"
            Else
                _strsql = "Insert into AcknowledgementTransaction (nTransactionID,sCode,sType) values ( '" & objStatus.TransactionID & "','" & objStatus.StatusCode & "',0)"
            End If

            sql.CommandText = _strsql
            sql.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' Used to insert Response data (ex Refill Response details)
    ''' </summary>
    ''' <param name="oResponseMessage"></param>
    ''' <param name="blnType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertResponseTransaction(ByVal oResponseMessage As SureScriptResponseMessage, ByVal blnType As Boolean) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        conn.Open()
        Try

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            Dim strApproved As String
            Select Case oResponseMessage.ApprovalStatus

                Case True
                    strApproved = "AP"
                Case False
                    strApproved = "DN"
                Case Else
                    strApproved = "DR"

            End Select
            If blnType Then
                _strsql = "Insert into ResponseTransaction (nMessageID,sType,sNotes,sDenialReasoncode,sDenialReason,bType) values ( '" & oResponseMessage.MessageID & "','" & strApproved & "','" & oResponseMessage.Notes & "','" & oResponseMessage.Denialcode & "','" & oResponseMessage.DenialReason & "',1)"
            Else
                _strsql = "Insert into ResponseTransaction (nMessageID,sType,sNotes,sDenialReasoncode,sDenialReason,bType) values ( '" & oResponseMessage.MessageID & "','" & strApproved & "','" & oResponseMessage.Notes & "','" & oResponseMessage.Denialcode & "','" & oResponseMessage.DenialReason & "',0)"

            End If
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    Public Function GetClinicName() As String
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        conn.Open()
        Dim sql As SqlCommand
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "select sClinicName from Clinic_Mst"

            sql.CommandText = _strsql
            sql.Connection = conn

            Dim strclinicname As String = sql.ExecuteScalar
            If Not IsDBNull(strclinicname) Then
                Return strclinicname
            Else
                Return ""
            End If
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function GetClinicDetails(ByVal nClinicId As Int64, ByVal gstrConnectionString As String) As DataTable
        Dim conn As New SqlConnection(gstrConnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            conn.Open()

            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "SELECT ISNULL(sClinicName,'') AS sClinicName,ISNULL(sExternalcode,'') AS sExternalcode FROM dbo.Clinic_MST WHERE nClinicID=" & nClinicId & " "
            sql.CommandText = _strsql
            sql.Connection = conn

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function ValidateNPIwithLuhn(ByVal NPI As String) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "select [dbo].[IsValidNPI]('" & NPI & "')"

            sql.CommandText = _strsql
            sql.Connection = conn

            Dim retVal As Boolean = sql.ExecuteScalar
            conn.Close()
            Return retVal
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return True ''if there is exception bydefault send true because if user is using SS 10.6 he should be able to do eRx
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    
End Class


