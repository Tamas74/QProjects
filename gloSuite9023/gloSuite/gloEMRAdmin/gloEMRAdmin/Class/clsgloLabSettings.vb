'***************************************************************************
' Module Name :- gloEMR Admin gloLab Settings
' Company Name :- gloStream Inc.
' Written By :- Madan.T
'Date:-20100213
' Description :-
'This classis to get the details from EMDEON
'Processes
'   1) 
'***************************************************************************

Imports System.Data.SqlClient
Imports System.io
Imports System.Collections.Generic
Imports System.Xml
Imports System.Text
Imports System.Net
Imports System.Text.RegularExpressions

Public Class clsgloLabSettings

    Private _strOutboxFilePath As String

    Public Property strOutboxFilePath() As String
        Get
            Return (_strOutboxFilePath)
        End Get
        Set(ByVal value As [String])
            _strOutboxFilePath = value
        End Set
    End Property

    Private _gloLabUserName As String
    Private _gloLabuserPwd As String
    Private _gloLabfacilltyCode As String
    Private _gloLabURL As String
    Public Property gloLabUserName() As String
        Get
            Return (_gloLabUserName)
        End Get
        Set(ByVal value As [String])
            _gloLabUserName = value
        End Set
    End Property

    Public Property gloLabuserPwd() As String
        Get
            Return (_gloLabuserPwd)
        End Get
        Set(ByVal value As [String])
            _gloLabuserPwd = value
        End Set
    End Property
    Public Property gloLabfacilltyCode() As String
        Get
            Return (_gloLabfacilltyCode)
        End Get
        Set(ByVal value As [String])
            _gloLabfacilltyCode = value
        End Set
    End Property
    Public Property gloLabURL() As String
        Get
            Return (_gloLabURL)
        End Get
        Set(ByVal value As [String])
            _gloLabURL = value
        End Set
    End Property
    'Metod for Posting Request
    Public Function PostDirServiceXML(ByVal odoc As XmlDocument) As Byte()

        Dim restURL As New StringBuilder()
        Dim restRequest As WebRequest = Nothing
        Dim restResponse As WebResponse = Nothing
        ' XmlDocument xDoc = new XmlDocument(); 
        Try
            restURL.AppendFormat(gloLabURL & "/servlet/XMLServlet?request=")

            restURL.Append(odoc.InnerXml.ToString())
            restRequest = DirectCast(HttpWebRequest.Create(restURL.ToString()), HttpWebRequest)
            ' the key line. This adds the base64-encoded authentication information to the request header 
            'restRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic" & Convert.ToBase64String(Encoding.UTF8.GetBytes("glo$treamU$er:m@3r4501g"))) 

            restRequest.Method = "POST"
            restRequest.ContentType = "text/xml"
            restRequest.Timeout = 60000
            restResponse = restRequest.GetResponse()
            Dim oReader As New System.IO.BinaryReader(restResponse.GetResponseStream())
            Dim bytesRead As Byte() = oReader.ReadBytes(Convert.ToInt32(restResponse.ContentLength))
            Return (bytesRead)
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-1.0:" + ex.ToString.ToString(), False)
            ' UpdateLog("GL-A-1.0:" + ex.ToString.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-1.1:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-1.1:" + ex.ToString.ToString())
            Return (Nothing)

        Finally
            If restResponse IsNot Nothing Then
                restResponse.Close()
            End If
        End Try
    End Function
    'Method for Extract XML ..
    Public Function ExtractXML(ByVal cntFromDB As Object, ByVal strFilePath As String) As String
        Dim stream As MemoryStream = Nothing
        Try
            If (cntFromDB IsNot Nothing) Then
                Dim strfilename As String = (strFilePath & "\") + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + "1" & ".xml")
                Dim content As Byte() = DirectCast(cntFromDB, Byte())
                stream = New MemoryStream(content)
                Dim oFile As New System.IO.FileStream(strfilename, System.IO.FileMode.Create)
                stream.WriteTo(oFile)
                oFile.Close()
                Dim odoc As New XmlDocument()
                odoc.Load(strfilename)
                Return (strfilename)
            Else
                Return ("")
            End If
        Catch ex As XmlException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-2.0:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-2.0:" + ex.ToString.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-2.1:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-2.1:" + ex.ToString.ToString())
            Return ("")
        Finally
            If (stream IsNot Nothing) Then
                stream.Dispose()
                stream = Nothing
            End If
        End Try
    End Function
    'Method for Generating XML file for request in gloLab
    Public Function GenerateXMLFIlE(ByVal strRequestType As String, ByVal xmlContent As Object) As String
        Dim _outboxFIlename As String = (strOutboxFilePath & "\") + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() & ".xml")
        Try
            ' Dim writerString As New StringWriter() 
            Dim writer As New XmlTextWriter(_outboxFIlename, Nothing)
            If strRequestType = "hsilabel_search" Then
                writer.WriteStartDocument()
                writer.WriteStartElement("REQUEST")
                writer.WriteAttributeString("userid", gloLabUserName)
                writer.WriteAttributeString("password", gloLabuserPwd)
                ' oPatient = (gloPatient.Patient)xmlContent;
                writer.WriteStartElement("OBJECT")
                writer.WriteAttributeString("name", "hsilabel")
                writer.WriteAttributeString("op", "search")
                writer.WriteElementString("organization", gloLabfacilltyCode)
                writer.WriteElementString("is_hsi_for", "Patient")
                writer.WriteElementString("registration", "Y")
                writer.WriteEndElement()
                writer.WriteEndElement()
                'Request End 
                writer.WriteEndDocument()
                writer.Close()
            End If
        Catch ex As XmlException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-3.0:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-3.0:" + ex.ToString.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-3.1:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-3.1:" + ex.ToString.ToString())
        End Try
        Return (_outboxFIlename)
    End Function
    Public Function ProcessRequest(ByVal strRequestType As String, ByVal xmlContent As Object) As String
        Dim strFileName As String = Nothing
        Dim _requestFile As String = GenerateXMLFIlE(strRequestType, xmlContent)
        'I've request file redy with me Outbox - Now sending 
        Try
            If Not String.IsNullOrEmpty(_requestFile) Then
                If File.Exists(_requestFile) Then
                    Dim odoc As New XmlDocument()
                    odoc.Load(_requestFile)
                    ''Send Request file and recieveing response and convert to XML file to Inbox 
                    Dim bytesRead As Byte() = Nothing
                    bytesRead = PostDirServiceXML(odoc)
                    If bytesRead IsNot Nothing AndAlso bytesRead.Length > 0 Then
                        strFileName = ExtractXML(bytesRead, strOutboxFilePath)
                        Return (strFileName)
                    Else
                        Return ("")
                    End If
                Else
                    Return ("")
                End If
            Else
                Return ("")
            End If
        Catch ex As XmlException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-4.0:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-4.0:" + ex.ToString.ToString())
        Catch ex As IOException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-4.1:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-4.1:" + ex.ToString.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-4.2:" + ex.ToString.ToString(), False)
            '  UpdateLog("GL-A-4.2:" + ex.ToString.ToString())
            Return ""
        Finally
            Try
                If System.IO.File.Exists(_requestFile) Then
                    System.IO.File.Delete(_requestFile)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-4.3:" + ex.ToString.ToString(), False)
                'UpdateLog("GL-A-4.3:" + ex.ToString.ToString())
            End Try
        End Try
    End Function
    ''' <summary>
    ''' To retrieve a facility code for gloLab with users, pswd, facility, URLs
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <param name="userPwd"></param>
    ''' <param name="facilltyCode"></param>
    ''' <param name="URL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getHsiLabel(ByVal userName As String, ByVal userPwd As String, ByVal facilltyCode As String, ByVal URL As String) As String

        gloLabUserName = userName.ToString()
        gloLabuserPwd = userPwd.ToString()
        While URL.EndsWith("\") Or URL.EndsWith("/")
            URL = URL.Substring(0, URL.Length - 1)
        End While
        gloLabURL = URL.ToString()
        gloLabfacilltyCode = facilltyCode.ToString()
        Dim dshsi_label As New DataSet()
        strOutboxFilePath = gloSettings.FolderSettings.AppTempFolderPath
        
        Dim strFileName As String = Nothing
        Dim Hsi_value As String = Nothing
        'Internet Connection test
        If IsInternetConnectionAvailable() = True Then
            strFileName = ProcessRequest("hsilabel_search", "")
            Try
                If IsAthenticated(strFileName) Then
                    dshsi_label.ReadXml(strFileName)
                    If dshsi_label IsNot Nothing Then
                        If dshsi_label.Tables(1).Rows.Count > 0 Then

                            Hsi_value = dshsi_label.Tables(1).Rows(0)("hsilabel").ToString()
                        End If
                    End If
                Else
                    Hsi_value = "false"
                End If
            Catch ex As XmlException
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-5.2:" + ex.ToString.ToString(), False)
                'UpdateLog("GL-A-5.2:" + ex.ToString.ToString())
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-5.3:" + ex.ToString.ToString(), False)
                ' UpdateLog("GL-A-5.3:" + ex.ToString.ToString())
                Hsi_value = "false"
            Finally
                Try
                    'Deleting Temp directory & temp files in gloLab
                    If System.IO.File.Exists(strFileName) Then
                        System.IO.File.Delete(strFileName)
                    End If
                    'Removed on 20100310--Madan
                    'If System.IO.Directory.Exists(strOutboxFilePath) Then
                    '    System.IO.Directory.Delete(strOutboxFilePath)
                    'End If
                Catch ex As IOException
                    'UpdateLog("GL-A-5.4:" + ex.ToString.ToString())
                    gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-5.4:" + ex.ToString.ToString(), False)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-5.5:" + ex.ToString.ToString(), False)
                    ' UpdateLog("GL-A-5.5:" + ex.ToString.ToString())
                End Try
            End Try
        Else
            Hsi_value = "internet"
        End If
        Return Hsi_value
    End Function
    'Method for checking Internet connection
    Public Function IsInternetConnectionAvailable() As Boolean
        Dim isAvailable As [Boolean] = False
        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://cli-cert.emdeon.com"), HttpWebRequest)
            request.Timeout = 5000
            request.Credentials = CredentialCache.DefaultNetworkCredentials
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode = HttpStatusCode.OK Then
                Console.WriteLine("IsSIPServerAvailable: " & response.StatusCode)
                isAvailable = True
            End If
            response.Close()
            request.Abort()
            Return (isAvailable)
        Catch ex As WebException
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-6.0:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-6.0:" + ex.ToString.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-6.1:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-6.1:" + ex.ToString.ToString())
        End Try
        Return (False)

    End Function
    'Method Validating Username,Password,URL
    Public Function IsAthenticated(ByVal FileName As String) As Boolean
        Dim _Status As Boolean = False
        Try
            If FileName <> "" Then
                Dim testTxt As New StreamReader(FileName)
                Dim allRead As String = testTxt.ReadToEnd()
                testTxt.Close()
                Dim regMatch As String = "sessionid"
                If Regex.IsMatch(allRead, regMatch) Then
                    _Status = True
                Else
                    _Status = False
                End If
            Else
                _Status = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-A-7.0:" + ex.ToString.ToString(), False)
            'UpdateLog("GL-A-7.0:" + ex.ToString.ToString())
            _Status = False
        End Try
        Return _Status
    End Function
    'Emdeon Log file...
    'Public Sub UpdateLog(ByVal strLogMessage As String)
    '    Try
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(strLogMessage, False)
    '        'Dim strApplPath As String = System.IO.Directory.GetCurrentDirectory()
    '        '' string strApplPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
    '        'Dim objFile As New System.IO.StreamWriter(strApplPath & "\Log\gloLabLog.Log", True)
    '        'objFile.WriteLine(Now.ToString() + ":" + Now.Now.Millisecond.ToString() + " " + strLogMessage)
    '        'objFile.Close()
    '        'objFile = Nothing
    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class

