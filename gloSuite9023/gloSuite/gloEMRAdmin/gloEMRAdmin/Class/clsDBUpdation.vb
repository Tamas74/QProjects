Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.Mail
Public Class clsDBUpdation
    Dim _nUpdationID As Int16
    Dim _sVersion As String
    Dim _sUpdatedBy As String
    Dim _dtUpdationDate As DateTime
    Dim _sLogFile As String
    Dim _blnSuccessfullyExecuted As Boolean
    Dim _sConnectionString As String

    Dim _sMailTo As String = "support@glostream.com"
    Dim _sMailCC As String = "pankaj@glostream.com"


    Public Property UpdationID() As Int16
        Get
            Return _nUpdationID
        End Get
        Set(ByVal Value As Int16)
            _nUpdationID = Value
        End Set
    End Property
    Public Property Version() As String
        Get
            Return _sVersion
        End Get
        Set(ByVal Value As String)
            _sVersion = Value
        End Set
    End Property
    Public Property UpdatedBy() As DateTime
        Get
            Return _sUpdatedBy
        End Get
        Set(ByVal Value As DateTime)
            _sUpdatedBy = Value
        End Set
    End Property
    Public Property UpdationDate() As DateTime
        Get
            Return _dtUpdationDate
        End Get
        Set(ByVal Value As DateTime)
            _dtUpdationDate = Value
        End Set
    End Property
    Public Property LogFile() As String
        Get
            Return _sLogFile
        End Get
        Set(ByVal Value As String)
            _sLogFile = Value
        End Set
    End Property
    Public Property SuccessfullyExecuted() As Boolean
        Get
            Return _blnSuccessfullyExecuted
        End Get
        Set(ByVal Value As Boolean)
            _blnSuccessfullyExecuted = Value
        End Set
    End Property
    Public ReadOnly Property ConnectionString() As String
        Get
            Return _sConnectionString
        End Get
    End Property
    Public ReadOnly Property MailTo() As String
        Get
            Return _sMailTo
        End Get
    End Property
    Public ReadOnly Property MailCC() As String
        Get
            Return _sMailCC
        End Get
    End Property

    Public Event ExecutingSQLQuery(ByVal strQuery As String)
    Public Event ExecutionProgress(ByVal nCurrent As Int16, ByVal nTotal As Int16)
    Public Event ExecutionCompleted()

    Sub New(ByVal sConnectionString As String)
        _sConnectionString = sConnectionString
    End Sub
    Private Function AddDBUpdation() As Boolean
        Return AddDBUpdation(_sVersion, _sUpdatedBy, _sLogFile, _blnSuccessfullyExecuted)
    End Function
    Private Function AddDBUpdation(ByVal sVerion As String, ByVal sUpdatedBy As String, ByVal sLogFile As String, ByVal blnSucess As Boolean) As Boolean

        _sVersion = sVerion
        _sUpdatedBy = sUpdatedBy
        _sLogFile = sLogFile
        _blnSuccessfullyExecuted = blnSucess

        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InUpDBUpdations"

        Dim objParaVersion As New SqlParameter
        With objParaVersion
            .ParameterName = "@Version"
            .Value = sVerion
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaVersion)


        Dim objParaUpdatedBy As New SqlParameter
        With objParaUpdatedBy
            .ParameterName = "@UpdatedBy"
            .Value = sUpdatedBy
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUpdatedBy)

        Dim objParaUpdateDate As New SqlParameter
        With objParaUpdateDate
            .ParameterName = "@UpdateDate"
            .Value = System.DateTime.Now
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaUpdateDate)

        Dim objParaLogFile As New SqlParameter
        With objParaLogFile
            .ParameterName = "@LogFile"
            .Value = sLogFile
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLogFile)


        Dim objParaExecutionStatus As New SqlParameter
        With objParaExecutionStatus
            .ParameterName = "@Success"
            If blnSucess = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaExecutionStatus)


        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True

    End Function
    Public Function RetrieveDBVersions() As Collection
        Dim clDBVersions As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveDBVersions"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clDBVersions.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clDBVersions

    End Function
    Public Function Fill_DBUpdations(Optional ByVal sVersion As String = "") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillDBVersionDetails"
        objCmd.Connection = objCon
        Dim objParaDBVersion As New SqlParameter
        With objParaDBVersion
            .ParameterName = "@DBVersion"
            .Value = sVersion
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaDBVersion)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)

    End Function
    Public Function Retrieve_LatestDBVersion() As String
        Dim strDBLatestVersion As String = "0.0.0"
        Dim clSelfNotesStatus As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveLatestDBVersion"
        objCmd.Connection = objCon
        objCon.Open()
        If IsNothing(objCmd.ExecuteScalar) = False Then
            If IsDBNull(objCmd.ExecuteScalar) = False Then
                strDBLatestVersion = objCmd.ExecuteScalar
            End If
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        Return strDBLatestVersion

    End Function


    Public Function ExecuteSQLQuery(ByVal sSQLScriptFile As String, ByVal strVersion As String, ByVal sUpdatedBy As String) As Boolean
        Dim blnSuccessfullyExecuted As Boolean = True
        Dim nTotalNoOfQueries As Integer = 100

        'Dim strTotalQuery As String
        'Dim objFile As New StreamReader(sSQLScriptFile)
        'strTotalQuery = objFile.ReadToEnd()
        'objFile = Nothing
        'Dim strQueries() As String
        'Dim strSplitString As String
        'strSplitString = vbCr & "GO" & vbLf
        'strQueries = strTotalQuery.Split(strSplitString)
        'nTotalNoOfQueries = UBound(strQueries)




        If Directory.Exists(Application.StartupPath & "\DBUpdationLog") = False Then
            Directory.CreateDirectory(Application.StartupPath & "\DBUpdationLog")
        End If
        Dim strLogFile As String
        Dim sb As New System.Text.StringBuilder(15)
        sb.Append(CStr(System.DateTime.Now.Year))
        sb.Append(CStr(System.DateTime.Now.Month))
        sb.Append(CStr(System.DateTime.Now.Day))
        sb.Append(CStr(System.DateTime.Now.Hour))
        sb.Append(CStr(System.DateTime.Now.Minute))
        sb.Append(CStr(System.DateTime.Now.Second))
        strLogFile = Application.StartupPath & "\DBUpdationLog\" & sb.ToString & ".log"
        sb = Nothing

        _sLogFile = strLogFile
        UpdateLog(strLogFile, "******************************************** Executing SQL Script ***********************************")
        UpdateLog(strLogFile, "SQL Script File " & sSQLScriptFile)
        UpdateLog(strLogFile, "Date & Time - " & System.DateTime.Now)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = _sConnectionString
        Dim objCmd As New SqlCommand
        objCmd.Connection = objCon
        objCon.Open()
        Dim objSR As StreamReader
        objSR = File.OpenText(sSQLScriptFile)
        Dim strLine As String
        strLine = objSR.ReadLine
        strLine = objSR.ReadLine
        Dim nQueryNo As Integer
        Dim strQuery As String = ""
        While Not strLine Is Nothing
            If UCase(Trim(strLine)) = "GO" Then
                objCmd.CommandType = CommandType.Text
                objCmd.CommandText = strQuery
                Try
                    RaiseEvent ExecutingSQLQuery(strQuery)
                    nQueryNo = nQueryNo + 1
                    RaiseEvent ExecutionProgress(nQueryNo, nTotalNoOfQueries)
                    objCmd.ExecuteNonQuery()
                Catch ex As Exception
                    blnSuccessfullyExecuted = False
                    UpdateLog(strLogFile, "___________________________________________________________________________________________")
                    UpdateLog(strLogFile, "Unable to execute SQL Query ")
                    UpdateLog(strLogFile, strQuery)
                    UpdateLog(strLogFile, "Error is ")
                    UpdateLog(strLogFile, ex.Message)
                    UpdateLog(strLogFile, "___________________________________________________________________________________________")
                End Try
                strQuery = ""
            Else
                strQuery = strQuery & " " & strLine
            End If
            strLine = objSR.ReadLine
        End While
        objSR.Close()

        If Trim(strQuery) <> "" Then
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQuery
            Try
                RaiseEvent ExecutingSQLQuery(strQuery)
                nQueryNo = nQueryNo + 1
                RaiseEvent ExecutionProgress(nQueryNo, nTotalNoOfQueries)
                objCmd.ExecuteNonQuery()
            Catch ex As Exception
                blnSuccessfullyExecuted = False
                UpdateLog(strLogFile, "___________________________________________________________________________________________")
                UpdateLog(strLogFile, "Unable to execute SQL Query ")
                UpdateLog(strLogFile, strQuery)
                UpdateLog(strLogFile, "Error is ")
                UpdateLog(strLogFile, ex.Message)
                UpdateLog(strLogFile, "___________________________________________________________________________________________")
            End Try
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        UpdateLog(strLogFile, "******************************************** Completing SQL Script ***********************************")
        AddDBUpdation(strVersion, sUpdatedBy, strLogFile, blnSuccessfullyExecuted)
        RaiseEvent ExecutionCompleted()
        Return True
    End Function
    Private Sub UpdateLog(ByVal strFileName As String, ByVal strText As String)
        Try
            Dim objFile As New StreamWriter(strFileName, True)
            objFile.WriteLine(strText)
            objFile.Close()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub


    'Public Sub SendStatusTogloStream()
    '    SendStatusTogloStream(_sVersion, _sUpdatedBy, _dtUpdationDate, _sLogFile, _blnSuccessfullyExecuted)
    'End Sub

    'Public Sub SendStatusTogloStream(ByVal sVersion As String, ByVal sUpdatedBy As String, ByVal dtUpdatedDate As DateTime, ByVal strLogFile As String, ByVal blnsucess As Boolean)
    '    Dim strStatus As String
    '    If blnsucess = True Then
    '        strStatus = "Successfully Done"
    '    Else
    '        strStatus = "Unsuccessfully Done"
    '    End If

    '    Dim objMail As System.Web.Mail.SmtpMail
    '    objMail.SmtpServer = "glostream.com"
    '    Dim objMailmsg As New System.Web.Mail.MailMessage
    '    objMailmsg.To = _sMailTo
    '    'objMailmsg.Cc = _sMailCC
    '    objMailmsg.From = _sMailTo
    '    objMailmsg.Subject = "gloEMR Database Updation Status - Version - " & sVersion & " Status-" & strStatus
    '    objMailmsg.BodyFormat = MailFormat.Html
    '    'Dim strBody As String
    '    'strBody = "<html><body><table width=80% align=center border=1><caption>gloEMR Database Updation Status</caption>"
    '    'strBody = strBody & "<tr><td colspan=2 align=center><b>Version - " & sVersion & "</b></td></tr><tr><td align=right width=40%>Updation Date</td><td>" & dtUpdatedDate & "</td>"
    '    'strBody = strBody & "</tr><tr><td align=right>Updation Status</td><td>"
    '    'If blnsucess = True Then
    '    '    strBody = strBody & "<font color='#0000ff'>Successfully Done"
    '    'Else
    '    '    strBody = strBody & "<font color='#ff0000'>Unsuccessfully Done"
    '    'End If
    '    'strBody = strBody & "</font></td></tr></table></body></html>"
    '    'objMailmsg.Body = strBody


    '    Dim sDetails As String = ""
    '    If File.Exists(strLogFile) = True Then
    '        Dim objFile As New StreamReader(strLogFile)
    '        sDetails = objFile.ReadToEnd()
    '        objFile = Nothing
    '    End If

    '    objMailmsg.Body = GetHTMLBody(sVersion, dtUpdatedDate, strStatus, gstrSQLServerName, gstrDatabaseName, gstrClientMachineName, sDetails)
    '    If File.Exists(strLogFile) = True Then
    '        objMailmsg.Attachments.Add(New MailAttachment(strLogFile))
    '    End If

    '    objMail.Send(objMailmsg)

    '    'sarika  21 feb
    '    Dim objAudit As New clsAudit
    '    objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has sent Database status to gloStream.", gstrLoginName, gstrClientMachineName)
    '    objAudit = Nothing
    '    '-------------
    '    objMailmsg = Nothing
    '    objMail = Nothing
    'End Sub

    Public Function GetHTMLBody_Old(ByVal strVersionNo As String, ByVal dtUpdateDate As DateTime, ByVal sStatus As String, ByVal sSQLServer As String, ByVal sDatabase As String, ByVal sMachineName As String, ByVal sDetails As String) As String
        Dim strBody As String
        strBody = "<html><body MS_POSITIONING=GridLayout><table width=100% align=left><tr><td bgcolor=#8c0043><font color='#FFFFFF' style='FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: white; FONT-FAMILY:	Verdana' ><b>gloEMR Database Update Details</b></font></td></tr><tr><td><table width=100%>"
        strBody = strBody & "<tr><td width=150 align=right><font style='FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' > Version No : </font></b></td><td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & strVersionNo & "</font></td></tr><tr><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Update Date</font></td>"
        strBody = strBody & "<td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & dtUpdateDate & "</font></td></tr><tr><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial'  >Update Status</font></td><td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sStatus & "</font></td></tr><tr><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >SQL Server</font></td>"
        strBody = strBody & "<td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sSQLServer & "</font></td></tr><tr><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Database</font></td><td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sDatabase & "</font></td></tr><tr><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Machine Name</font></td>"
        strBody = strBody & "<td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sMachineName & "</font></td></tr></table></td></tr><tr><td><table width=100%><tr><td>&nbsp;</td></tr><tr><td><hr></td></tr><tr>	<td><b>Updation Summary</b></td></tr><tr><td>&nbsp;</td></tr><tr><td>" & sDetails & "</td></tr></table></td></tr></table></body></html>"
        Return strBody
    End Function
    Public Function GetHTMLBody(ByVal strVersionNo As String, ByVal dtUpdateDate As DateTime, ByVal sStatus As String, ByVal sSQLServer As String, ByVal sDatabase As String, ByVal sMachineName As String, ByVal sDetails As String) As String

        Dim sb As New System.Text.StringBuilder(500)
        sb.Append("<html><body MS_POSITIONING='GridLayout'><table width=800 align=left><tr><td bgcolor=#8c0043><font color='#FFFFFF' style='FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: white; FONT-FAMILY:	Verdana' ><b>gloEMR Database Update Details</b></font></td></tr><tr><td><table ><tr><td width=100 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial'  > Version No : </font></b></td><td width=200><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & strVersionNo & "</font></td><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >SQL Server : </font></td>")
        sb.Append("<td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sSQLServer & "</font></td></tr><tr><td width=100 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Update Date : </font></td><td width=200><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & dtUpdateDate & "</font></td><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Database : </font></td><td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sDatabase & "</font></td></tr><tr><td width=100 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial'  >Update Status : </font></td>")
        sb.Append("<td width=200><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sStatus & "</font></td><td width=150 align=right><font style= 'FONT-WEIGHT: Bold; FONT-SIZE: 12px; FONT-FAMILY: Arial' >Machine Name : </font></td><td><font style=  'FONT-SIZE: 12px; FONT-FAMILY: Verdana'>" & sMachineName & "</font></td></tr></table></td></tr><tr><td><table width=800 ><tr><td>&nbsp;</td></tr><tr><td><hr></td></tr><tr><td><b>Updation Summary</b></td></tr>	<tr><td>&nbsp;</td></tr><tr><td>" & sDetails & "</td></tr></table></td></tr></table></body></html>")
        Return sb.ToString
    End Function
End Class
