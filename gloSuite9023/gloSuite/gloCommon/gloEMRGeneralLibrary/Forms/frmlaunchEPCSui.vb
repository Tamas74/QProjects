Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Linq
Imports System.Xml
Imports Microsoft.Win32
Imports System.Configuration
Imports gloSureScript

Public Class frmlaunchEPCSui
    Inherits Form
    Public Flags As Boolean = False
    Private _blnStagingServer As Boolean
    Private _strfileName As String
    Public Methodname As String = ""
    Private _DecryptedSharedSecretKey As String
    Public urlForEpcs As String = ""

#Region "Property"

    Public Property StrfileName() As String
        Get
            Return _strfileName
        End Get
        Set(ByVal value As String)
            _strfileName = value
        End Set
    End Property

    Public Property DecryptedSharedSecretKey() As String
        Get
            Return _DecryptedSharedSecretKey
        End Get
        Set(ByVal value As String)
            _DecryptedSharedSecretKey = value
        End Set
    End Property


    Public Property blnStagingServer() As Boolean
        Get
            Return _blnStagingServer
        End Get
        Set(ByVal value As Boolean)
            _blnStagingServer = value
        End Set
    End Property


#End Region

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Method to Call from EPCS
    ''' </summary>
    ''' <param name="_Methodname">Method Name</param>
    ''' <remarks></remarks>
    Public Sub New(_Methodname As String)
        InitializeComponent()
        Methodname = _Methodname
    End Sub


#End Region


    Private Sub frmRegisterPrescriber_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If CheckforScript() = False Then
                MessageBox.Show("JavaScript setting is turned OFF in Internet Settings, please contact your system administrator to turn it ON.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            pnlWebbrowser.BringToFront()
            Flags = True
            Me.Cursor = Cursors.WaitCursor
            wbEPCS.ScriptErrorsSuppressed = True
            wbEPCS.Navigate(Application.StartupPath & "\HtmlPages\UILaunchLogicalAccess.html", False)
        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckforScript() As Boolean
        Dim key As Microsoft.Win32.RegistryKey = Nothing
        Const DWORD_FOR_ACTIVE_SCRIPTING As String = "1400"
        Const VALUE_FOR_DISABLED As String = "3"
        Const VALUE_FOR_ENABLED As String = "0"
        Dim retVal As Boolean = False
        Try


            ''retVal As Boolean = True
            'get the registry key for Zone 3(Internet Zone)
            key = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3", True)

            If key IsNot Nothing Then
                Dim value As [Object] = key.GetValue(DWORD_FOR_ACTIVE_SCRIPTING, VALUE_FOR_ENABLED)
                If value.ToString().Equals(VALUE_FOR_DISABLED) Then
                    '' retVal = False
                    key.SetValue(DWORD_FOR_ACTIVE_SCRIPTING, VALUE_FOR_ENABLED)
                End If
            End If
            retVal = True
        Catch ex As Exception
            retVal = False
        Finally
            If (IsNothing(key) = False) Then
                key.Close()
                key.Dispose()
                key = Nothing
            End If

        End Try
        Return retVal
    End Function

    Private Sub wbMedHx_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbEPCS.DocumentCompleted
        Dim url As String = Application.StartupPath & "\HtmlPages\UILaunchLogicalAccess.html"
        Dim strShareSec As HtmlElement
        Dim strIssuteTimeStamp As HtmlElement
        Dim strdataIn As HtmlElement
        Dim buttonclick As HtmlElement
        Dim xdoc As XmlDocument = Nothing

        Try
            If wbEPCS.Url.LocalPath = url Then
                Dim dtdate As DateTime = System.DateTime.UtcNow
                Dim strdate As String = dtdate.ToString("dd:MM:yyyy")
                Dim strtime As String = dtdate.ToString("HH:mm:ss")
                Dim strUTCFormat As String = (Convert.ToString(strdate & Convert.ToString("-")) & strtime) + ""
                xdoc = New XmlDocument()
                xdoc.Load(StrfileName)
                strShareSec = wbEPCS.Document.GetElementById("sharedSecret")
                If strShareSec <> Nothing Then
                    'strShareSec.InnerText = ConfigurationManager.AppSettings("sharedSecret")
                    strShareSec.InnerText = DecryptedSharedSecretKey
                End If
                strIssuteTimeStamp = wbEPCS.Document.GetElementById("issueTimeStamp")
                If strIssuteTimeStamp <> Nothing Then
                    strIssuteTimeStamp.InnerText = strUTCFormat
                End If
                strdataIn = wbEPCS.Document.GetElementById("dataIn")
                If strdataIn <> Nothing Then
                    strdataIn.InnerText = xdoc.InnerXml
                End If

                If (Convert.ToString(Methodname).Trim <> String.Empty) Then
                    Dim strActionURL As String = wbEPCS.Document.GetElementById("testform").GetAttribute("action")
                    Dim strURL As String = urlForEpcs 'ConfigurationManager.AppSettings("Stagingactionbaseurl")
                    Dim strURLtoset As String = UriCombine(strURL, Convert.ToString(Methodname))
                    wbEPCS.Document.GetElementById("testform").SetAttribute("action", strURLtoset)
                End If
                buttonclick = wbEPCS.Document.GetElementById("SubmitXML")
                If buttonclick IsNot Nothing Then
                    buttonclick.InvokeMember("Click")
                End If
            End If

            Dim strUrlMatch As String = urlForEpcs & "UILaunchSigning"
            If wbEPCS.Url.ToString() = strUrlMatch Then
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception

        Finally
            If Not IsNothing(xdoc) Then
                xdoc = Nothing
            End If
        End Try

    End Sub

    Private Sub tblbtn_Close_Click(sender As Object, e As System.EventArgs) Handles tblbtn_Close.Click
        Try
            If wbEPCS IsNot Nothing Then
                wbEPCS.Dispose()
                wbEPCS = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function UriCombine(strURL As String, param As String) As String
        Dim str As String = strURL
        If Not str.EndsWith("/") Then
            str = str & "/"
        End If
        Dim uri As New Uri(str)
        Return New Uri(uri, param).AbsoluteUri
    End Function

End Class
