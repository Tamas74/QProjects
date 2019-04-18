'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.Web.Mail
Imports System.Data.SqlClient
Public Class clsSuggestions
    Enum enmSuggestionCriteria
        Today
        Yesterday
        LastWeek
        LastMonth
        LastQuarter
        LastYear
        All
    End Enum


    Dim _sLicenseCode As String
    Dim _dtSuggestionDate As DateTime
    Dim _sClientName As String
    Dim _sEmailAddress As String
    Dim _sSubject As String
    Dim _sComments As String

    Public Property SuggestionDate() As DateTime
        Get
            Return _dtSuggestionDate
        End Get
        Set(ByVal Value As DateTime)
            _dtSuggestionDate = Value
        End Set
    End Property
    Public Property LicenseCode() As String
        Get
            Return _sLicenseCode
        End Get
        Set(ByVal Value As String)
            _sLicenseCode = Value
        End Set
    End Property
    Public Property ClientName() As String
        Get
            Return _sClientName
        End Get
        Set(ByVal Value As String)
            _sClientName = Value
        End Set
    End Property
    Public Property ClientEmailAddress() As String
        Get
            Return _sEmailAddress
        End Get
        Set(ByVal Value As String)
            _sEmailAddress = Value

        End Set
    End Property
    Public WriteOnly Property Subject() As String
        Set(ByVal Value As String)
            _sSubject = Value
        End Set
    End Property
    Public WriteOnly Property Comments() As String
        Set(ByVal Value As String)
            _sComments = Value
        End Set
    End Property
    'Public Function SendSuggestion() As Boolean
    '    Return SendSuggestion(_sLicenseCode, _sClientName, _sEmailAddress, _sSubject, _sComments)
    'End Function
    'Public Function SendSuggestion(ByVal sLicenseCode As String, ByVal sClientName As String, ByVal sEmailAddress As String, ByVal sSubject As String, ByVal sComments As String) As Boolean
    '    If IsInternetConnectionAvailable() = False Then
    '        Return False
    '    End If
    '    Dim objMail As System.Web.Mail.SmtpMail
    '    objMail.SmtpServer = "glostream.com"
    '    Dim objMailmsg As New System.Web.Mail.MailMessage
    '    objMailmsg.To = "support@glostream.com"
    '    objMailmsg.From = sClientName & " <" & sEmailAddress & ">"
    '    objMailmsg.Subject = sSubject
    '    objMailmsg.BodyFormat = MailFormat.Html
    '    Dim strBody As String
    '    strBody = "<body><table width=80% align=center border=1 cellspacing=0><tr><td align=center colspan=4><b>gloStream Support</b></td></tr>"
    '    strBody = strBody & "<tr><td width=25% align=right><b>Client Name</b></td><td width=25% align=left>" & sClientName & "</td><td width=25% align=right><b>License No</b></td>"
    '    strBody = strBody & "<td width=25% align=left>L2005/234</td></tr><tr><td width=25% align=right><b>Purchase Date</b></td><td width=25% align=left>09/19/2005</td><td width=25% align=right><b>Version No</b></td>"
    '    strBody = strBody & "<td width=25% align=left>1.0.0</td></tr><tr><td width=25% align=right><b>Contact Person</b></td><td width=75% align=left colspan=3>Dr.Andrew Fuller</td>"
    '    strBody = strBody & "</tr><tr>	<td width=25% align=right><b>Contact Nos</b></td><td width=75% align=left colspan=3>(206) 555-9482, (206) 555-3412, (206) 555-9857</td>"
    '    strBody = strBody & "</tr><tr><td width=25% align=right><b>Email Address</b></td><td width=75% align=left colspan=3>" & sEmailAddress & "</td></tr><tr><td align=left colspan=4><b>Message</b></td></tr>"
    '    strBody = strBody & "<tr><td align=left colspan=4>" & sComments.Replace(vbCrLf, "<br>") & "</td></tr></table></body>"
    '    objMailmsg.Body = strBody
    '    objMail.Send(objMailmsg)
    '    objMailmsg = Nothing
    '    objMail = Nothing

    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    'Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "sp_InsertSuggestion"

    '    Dim objParaSuggestionDate As New SqlParameter
    '    With objParaSuggestionDate
    '        .ParameterName = "@SuggestionDate"
    '        .Value = Date.Now.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaSuggestionDate)


    '    Dim objParaClientName As New SqlParameter
    '    With objParaClientName
    '        .ParameterName = "@ClientName"
    '        .Value = sClientName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClientName)

    '    Dim objParaEmail As New SqlParameter
    '    With objParaEmail
    '        .ParameterName = "@Email"
    '        .Value = sEmailAddress
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaEmail)

    '    Dim objParaSubject As New SqlParameter
    '    With objParaSubject
    '        .ParameterName = "@Subject"
    '        .Value = sSubject
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSubject)



    '    Dim objParaComments As New SqlParameter
    '    With objParaComments
    '        .ParameterName = "@Comments"
    '        .Value = sComments
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaComments)

    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    objCmd.ExecuteNonQuery()
    '    objCon.Close()
    '    objCmd = Nothing
    '    objCon = Nothing
    '    Return True
    'End Function

    Private Function IsInternetConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.gloStream.com/")
        ' Setup WebRequest
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            ' Attempt to get response and return True
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            'objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function

    Public Sub ScanClientDetails()
        _sLicenseCode = "XYZ"
        _sClientName = "Pankaj Naval"
        _sEmailAddress = "pankaj@glostream.com"
    End Sub

    Public Function RetrieveSuggestions(ByVal enmCriteria As enmSuggestionCriteria) As DataTable
        Dim dtFromDate As DateTime
        Dim dtEndDate As DateTime
        dtEndDate = Date.Now.Date
        Select Case enmCriteria
            Case enmSuggestionCriteria.Today
                dtFromDate = Date.Now.Date
            Case enmSuggestionCriteria.Yesterday
                dtFromDate = Date.Now.Date.AddDays(-1)
            Case enmSuggestionCriteria.LastWeek
                dtFromDate = Date.Now.Date.AddDays(-7)
            Case enmSuggestionCriteria.LastMonth
                dtFromDate = Date.Now.Date.AddMonths(-1)
            Case enmSuggestionCriteria.LastQuarter
                dtFromDate = Date.Now.Date.AddMonths(-3)
            Case enmSuggestionCriteria.LastYear
                dtFromDate = Date.Now.Date.AddYears(-1)
        End Select
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillSuggestion"
        objCmd.Connection = objCon
        If enmCriteria <> enmSuggestionCriteria.All Then
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFromDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtEndDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)
        End If
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        ' objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return dsData.Tables(0)
    End Function

    Public Function DeleteSuggestions(ByVal nSuggestionID As Integer) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '---
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteSuggestions"

        Dim objParaSuggestionID As New SqlParameter
        With objParaSuggestionID
            .ParameterName = "@SuggestionID"
            .Value = nSuggestionID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSuggestionID)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return True
    End Function
End Class



