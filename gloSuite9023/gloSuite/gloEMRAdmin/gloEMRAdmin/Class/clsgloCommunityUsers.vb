Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Net
Imports System.Web.Services.Protocols

'Code Start-Added by kanchan on 20120731 for gloCommunity
Public Class clsgloCommunityUsers
    Private Conn As SqlConnection
    Private Cmd As SqlCommand

    Public Function InsertGCUSer(ByVal gc_nUserId As Long, ByVal gc_sUserName As String, ByVal gc_sPassword As String, ByVal IsStaging As Boolean) As Long
        Dim Id As Long = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Dim gc_sEnvironment As String = "staging"
        If IsStaging = False Then
            gc_sEnvironment = "production"
        End If

        Try
            oDB.Connect(False)
            oDBParameters.Add("@gc_nUserId", gc_nUserId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@gc_sUserName", gc_sUserName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@gc_sPassword", gc_sPassword, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@gc_sEnvironment", gc_sEnvironment, ParameterDirection.Input, SqlDbType.Char)

            Id = Convert.ToInt64(oDB.ExecuteScalar("GC_InsertgloCommunityUser", oDBParameters))
            Return Id
        Catch
            Return Id
        Finally
            Id = Nothing
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function ChangeGCUSerPassword(ByVal gc_nUserId As Long, ByVal gc_sUserName As String, ByVal gc_sPassword As String) As Long
        Dim Id As Long = 0

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@gc_nUserId", gc_nUserId, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@gc_sUserName", gc_sUserName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@gc_sPassword", gc_sPassword, ParameterDirection.Input, SqlDbType.VarChar)

            Id = Convert.ToInt64(oDB.ExecuteScalar("GC_ChangegloCommunityUserPwd", oDBParameters))

            Return Id
        Catch
            Return Id
        Finally
            Id = Nothing
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function

    Public Function getGCUser(ByVal gc_nUserId As Long, ByVal IsStaging As Boolean) As DataTable
        Dim dt As DataTable = Nothing
        Dim sqladpt As SqlDataAdapter = Nothing

        Dim gc_sEnvironment As String = "staging"
        If IsStaging = False Then
            gc_sEnvironment = "production"
        End If

        Try
            dt = New DataTable()
            sqladpt = New SqlDataAdapter()
            Conn = New SqlConnection(gstrConnectionString)

            Cmd = New System.Data.SqlClient.SqlCommand("GC_IsExistGCUser", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter = Nothing

            objParam = Cmd.Parameters.Add("@gc_nUserId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gc_nUserId

            objParam = Cmd.Parameters.Add("@gc_sEnvironment", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gc_sEnvironment

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Conn.Close()
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            If sqladpt IsNot Nothing Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function getClinicData() As DataTable
        Dim dt As DataTable = Nothing
        Dim sqladpt As SqlDataAdapter = Nothing
        Try
            dt = New DataTable()
            sqladpt = New SqlDataAdapter()
            Conn = New SqlConnection(gstrConnectionString)

            Cmd = New System.Data.SqlClient.SqlCommand("GC_getClinic", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As SqlException
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            Conn.Close()
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            If sqladpt IsNot Nothing Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Function getCommunityAuthenticationSetting() As String
        Dim ogloSettings As clsSettings
        Dim _result As String = String.Empty
        Try
            ogloSettings = New clsSettings()
            ogloSettings.GetSetting("gloCommunityAuthentication", 0, gnClinicID, _result)
            Return _result
        Catch ex As Exception
            Return Nothing
        Finally
            ogloSettings = Nothing
        End Try
    End Function
    Public Function IsSiteExist() As Boolean
        Dim _result As Boolean = False
        Try
            Dim objWeb As New gloWeb.Webs()

            objWeb.CookieContainer = New CookieContainer()

            objWeb.CookieContainer.Add(QueryToSharePoint(mdlGeneral.gstrGCUserName, mdlGeneral.gstrGCPassword))

            objWeb.Url = mdlGeneral.gstrSharepointSrvNm & "/_vti_bin/Webs.asmx"
            Dim temp As XmlNode = objWeb.GetWebCollection()
            For Each chldNode As XmlNode In temp.ChildNodes
                Dim _SiteName As String = chldNode.Attributes("Url").Value

                Dim _cnt As Integer = chldNode.Attributes("Url").Value.LastIndexOf("/")
                _SiteName = _SiteName.Substring(_cnt + 1, _SiteName.Length - _cnt - 1)
                If mdlGeneral.gstrSharepointSiteNm.ToUpper().Trim() = _SiteName.ToUpper().Trim() Then
                    _result = True
                    Return _result
                End If
            Next
            Return _result
        Catch ex As System.Web.Services.Protocols.SoapException
            _result = False

        Catch ex As Exception
            _result = False
        End Try


    End Function

    'Added for access gloCommunity using Form authentication on 20120730
    Public Shared Function QueryToSharePoint(ByVal userName As String, ByVal password As String) As Cookie
        Dim cookie As Cookie = Nothing
        Dim spAuthentication As SPAuthentication.Authentication = Nothing
        Try
            Dim authenticationWSAddress As String = gstrSharepointSrvNm + "/" + "_vti_bin" + "/Authentication.asmx"
            spAuthentication = New SPAuthentication.Authentication()
            spAuthentication.Url = authenticationWSAddress
            spAuthentication.CookieContainer = New CookieContainer()

            'Try to login to SharePoint site with Form based authentication
            Dim loginResult As SPAuthentication.LoginResult = spAuthentication.Login(userName, password)
            cookie = New Cookie()
            'If login is successful
            If loginResult.ErrorCode = gloEMRAdmin.SPAuthentication.LoginErrorCode.NoError Then
                'Get the cookie collection from the authentication web service
                Dim cookies As CookieCollection = spAuthentication.CookieContainer.GetCookies(New Uri(spAuthentication.Url))
                'Get the specific cookie which contains the security token
                cookie = cookies(loginResult.CookieName)
                oFormCookie = cookie
            End If
            Return cookie
            'return "SOAP ERROR: " + Environment.NewLine + exp.Message;
        Catch exp As SoapException
            Return Nothing
            'return "ERROR: " + Environment.NewLine + exp.Message;
        Catch exp As Exception
            Return Nothing
        Finally
            cookie = Nothing
            If Not IsNothing(spAuthentication) Then
                spAuthentication.Dispose()
            End If

        End Try

    End Function
    'End

End Class
'Code End-Added by kanchan on 20120731 for gloCommunity


