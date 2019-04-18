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

Public Class frmRegisterPrescriber
    Inherits Form
    Private _encryptionKey As String = "12345678"
    Public ServiceCall As EpcsSeviceCall = Nothing
    Public Property StrfileName() As String
        Get
            Return _strfileName
        End Get
        Set(ByVal value As String)
            _strfileName = value
        End Set
    End Property
    Private _strfileName As String

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region
    Private Sub frmRegisterPrescriber_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            pnlWebbrowser.BringToFront()
            Me.Cursor = Cursors.WaitCursor
            If ServiceCall = EpcsSeviceCall.UILaunchLogicalAccess Then
                wbEPCS.Navigate(Application.StartupPath & "\HtmlPages\UILaunchLogicalAccess.html", False)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Register, "Register Prescriber Fails.", gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub wbMedHx_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbEPCS.DocumentCompleted
        Dim url As String = Application.StartupPath & "\HtmlPages\UILaunchLogicalAccess.html"
        Dim strShareSec As HtmlElement
        Dim strIssuteTimeStamp As HtmlElement
        Dim strdataIn As HtmlElement
        Dim buttonclick As HtmlElement
        Dim xdoc As XmlDocument = Nothing
        Dim decryptionObject As New clsEncryption
        Dim strUrlMatch As String = ""
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
                    If gloSurescriptGeneral.blnStagingServer Then
                        strShareSec.InnerText = decryptionObject.DecryptFromBase64String(clsEPCSHelper.sSharedSecret, _encryptionKey)
                    Else
                        strShareSec.InnerText = decryptionObject.DecryptFromBase64String(clsEPCSHelper.sSharedSecret, _encryptionKey)
                    End If
                End If
                strIssuteTimeStamp = wbEPCS.Document.GetElementById("issueTimeStamp")
                If strIssuteTimeStamp <> Nothing Then
                    strIssuteTimeStamp.InnerText = strUTCFormat
                End If
                strdataIn = wbEPCS.Document.GetElementById("dataIn")
                If strdataIn <> Nothing Then
                    strdataIn.InnerText = xdoc.InnerXml
                End If
                strUrlMatch = clsEPCSHelper.sEpcsUrl & "UILaunchLogicalAccess"
                wbEPCS.Document.GetElementById("testform").SetAttribute("action", strUrlMatch)

                buttonclick = wbEPCS.Document.GetElementById("SubmitXML")
                If buttonclick IsNot Nothing Then
                    buttonclick.InvokeMember("Click")
                End If
            End If
            If gloSurescriptGeneral.blnStagingServer Then
                If wbEPCS.Url.ToString() = strUrlMatch Then
                    Me.Cursor = Cursors.Default
                End If
            Else
                If wbEPCS.Url.ToString() = strUrlMatch Then

                    Me.Cursor = Cursors.Default
                End If
            End If
        Catch ex As Exception
            wbEPCS.Visible = True
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Register, "Register Prescriber Fails." & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Register, "Register Prescriber.", gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class
