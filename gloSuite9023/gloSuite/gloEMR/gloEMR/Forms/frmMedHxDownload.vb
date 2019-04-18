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
Imports System.Xml.Linq
Imports gloMeds.Core.MedHx
Imports Microsoft.Win32
Imports gloRxHub

Public Class frmMedHxDownload
    Inherits Form

    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private oRequest As MedHxRequests = Nothing
    Private WithEvents _PatientStrip As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Dim PatientID As Int64 = 0
    Public MedicationHistory As List(Of MedHxItemNew)
    Private oMedHxReconcilation As gloMedHxReconcilation = Nothing
    Dim _MedicationVisitID As Long
    Dim _RequestID As Int64 = 0
    Private MedhxPortalURL As String = String.Empty


    Public Property MedicationVisitID() As Long
        Get
            Return _MedicationVisitID
        End Get
        Set(ByVal value As Long)
            _MedicationVisitID = value
        End Set
    End Property
    Public Property RequestID() As Long
        Get
            Return _RequestID
        End Get
        Set(ByVal value As Long)
            _RequestID = value
        End Set
    End Property
#Region "Constructor"

    Public Sub New(ByVal pid As Long, objRequests As MedHxRequests)
        InitializeComponent()
        oRequest = objRequests
        PatientID = pid
    End Sub

#End Region


    Private Sub frmMedHxDownload_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If CheckforScript() = False Then
                MessageBox.Show("JavaScript setting is turned OFF in Internet Settings, please contact your system administrator to turn it ON.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If gblnRxhubStagingServer = True Then
                MedhxPortalURL = gstrMedHXStagingUrl
            Else
                MedhxPortalURL = gstrMedHXProductionUrl
            End If
            pnlPleasewait.Visible = True
            tblbtn_Reconcile.Enabled = False

            Call loadPatientStrip()

            Dim oSerializeData = SerializationHelper.Serialize(oRequest)
            Dim Requestpostdata As [Byte]() = Encoding.UTF8.GetBytes(oSerializeData)
            wbMedHx.Navigate(MedhxPortalURL & "medhx", "", Requestpostdata, "")
            ' wbMedHx.Navigate("http://dev60:8080/" & "medhx", "", Requestpostdata, "")

        Catch ex As Exception
            pnlPleasewait.Visible = False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Private Sub loadPatientStrip()
        Try
            If IsNothing(_PatientStrip) = False Then
                Me.Controls.Remove(_PatientStrip)
                _PatientStrip.Dispose() : _PatientStrip = Nothing
            End If
            _PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip

            _PatientStrip.ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.MedicationHistory)
            _PatientStrip.Dock = DockStyle.Top
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            _PatientStrip.BringToFront()

            _PatientStrip.DTP.CustomFormat = "MM/dd/yyyy"
            Me.Controls.Add(_PatientStrip)
            pnlToolStrip.SendToBack()
            pnlWebbrowser.BringToFront()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Close_Click(sender As Object, e As EventArgs) Handles tblbtn_Close.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmProcessMedHx_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            CheckMedHistoryDownloadorNot()

            If wbMedHx IsNot Nothing Then
                wbMedHx.Dispose()
                wbMedHx = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Accept_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Accept.Click
        Dim dt As DataTable = oMedHxReconcilation.DataTableFromMedHXItem("Selected")
        Using oFinalizeMedication As New frmFinalizeReconcileList(dt, "Medication", PatientID, "", "", gstrLoginName, gnLoginID)
            oFinalizeMedication.LoadFinalizeRecords()
            oFinalizeMedication.AcceptReconcileLists()
            MedicationVisitID = oFinalizeMedication.MedicationVisitID
            Me.Close()
        End Using

    End Sub
    Private Sub tblbtn_Preview_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Preview.Click
        Dim SelectedMedication As DataTable = oMedHxReconcilation.DataTableFromMedHXItem("Selected")
        Using oFinalizeMedication As New frmFinalizeReconcileList(SelectedMedication, "Medication", PatientID, "", "", gstrLoginName, gnLoginID)
            oFinalizeMedication.ShowDialog()
            If oFinalizeMedication.SelectedAction = frmFinalizeReconcileList.FormAction.Accepted Then
                MedicationVisitID = oFinalizeMedication.MedicationVisitID
                oFinalizeMedication.Close()
                Me.Close()
            Else
                oFinalizeMedication.Close()
            End If
        End Using
    End Sub
    Private Sub tblbtn_Reconcile_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Reconcile.Click
        Try
            If IsNothing(oMedHxReconcilation) Then
                MedicationHistory = DownloadMedHistory()
                If Not IsNothing(MedicationHistory) Then
                    If MedicationHistory.Count = 0 Then
                        MessageBox.Show("No Medication History available for Reconcile.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Return
                    End If
                Else
                    MessageBox.Show("Please send Medication History request to Reconcile. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                oMedHxReconcilation = New gloMedHxReconcilation(MedicationHistory, PatientID)
            End If
            Dim currentMedication As DataTable = Nothing
            currentMedication = oMedHxReconcilation.GetActiveMedication()
            If (currentMedication.Rows.Count = 0) Then
                'if current medication 0 then directly save 
                Dim MedHxMedication As DataTable = oMedHxReconcilation.DataTableFromMedHXItem("All")
                Using oFinalizeMedication As New frmFinalizeReconcileList(MedHxMedication, "Medication", PatientID, "", "", gstrLoginName, gnLoginID)
                    oFinalizeMedication.LoadFinalizeRecords()
                    oFinalizeMedication.AcceptReconcileLists()
                    MedicationVisitID = oFinalizeMedication.MedicationVisitID
                    Me.Close()
                End Using
                If Not IsNothing(MedHxMedication) Then
                    MedHxMedication.Dispose()
                    MedHxMedication = Nothing
                End If
                If Not IsNothing(currentMedication) Then
                    currentMedication.Dispose()
                    currentMedication = Nothing
                End If
            Else
                pnlMedHx.Controls.Add(oMedHxReconcilation)
                oMedHxReconcilation.Dock = DockStyle.Fill
                pnlMedHx.BringToFront()
                tblbtn_Accept.Visible = True
                tblbtn_Preview.Visible = True
                tblbtn_Accept.Enabled = True
                tblbtn_Preview.Enabled = True
                tblbtn_Reconcile.Enabled = False
                Me.Text = "Reconcile new Med Hx Download information with Patient’s Current Medication History"
            End If
            'tblbtn_Reconcile.Text = "&Finalize"
            'tblbtn_Reconcile.Image = Global.gloEMR.My.Resources.Finalized           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
    End Sub
    Public Function DownloadMedHistoryTest() As List(Of MedHxItem)
        Dim oMedHxInterface As New MedHxInterface
        Return oMedHxInterface.GetMedHxResponses(RequestID)
    End Function

    Private Sub wbMedHx_DocumentCompleted(sender As System.Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbMedHx.DocumentCompleted
        pnlPleasewait.Visible = False
        tblbtn_Reconcile.Enabled = True
    End Sub

    Public Function DownloadMedHistory() As List(Of MedHxItemNew)
        Dim MedHxHttpWebRequest As HttpWebRequest = Nothing
        Dim MedHxHttpWebResponse As HttpWebResponse = Nothing
        Dim MedHxXMLDocument As XmlDocument = Nothing
        Dim MedHxXMLReader As XmlTextReader = Nothing
        Dim lstMedications As List(Of MedHxItemNew) = Nothing
        Dim FilteredlstMedications As List(Of MedHxItemNew) = Nothing
        Try
            Dim sURL As String = MedhxPortalURL & "api/MedHxInterfaceNew/" & RequestID.ToString()
            Dim temp As String() = Nothing
            MedHxHttpWebRequest = DirectCast(HttpWebRequest.Create(sURL), HttpWebRequest)
            If (IsNothing(MedHxHttpWebRequest) = False) Then
                MedHxHttpWebRequest.Method = "GET"
                MedHxHttpWebResponse = DirectCast(MedHxHttpWebRequest.GetResponse(), HttpWebResponse)
                If (IsNothing(MedHxHttpWebResponse) = False) Then
                    Using ResponceStreamReader = New StreamReader(MedHxHttpWebResponse.GetResponseStream())
                        Dim JsonString As String = ResponceStreamReader.ReadToEnd()
                        If (IsNothing(JsonString) = False) Then
                            lstMedications = SerializationHelper.DeserializeMedHxItemNew(JsonString)
                        End If
                        Dim strIDS As String = CallBrowserButtonCLick()
                        If (IsNothing(strIDS) = False) Then
                            temp = strIDS.Split(New String() {" ", ",", ">", "name"}, StringSplitOptions.None)
                        End If

                        If Not IsNothing(lstMedications) Then
                            If String.IsNullOrEmpty(strIDS) AndAlso lstMedications.Count <> 0 Then
                                MessageBox.Show("No Medication History available for Reconcile.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Return Nothing
                            End If
                        End If
                        If (IsNothing(lstMedications) = False) AndAlso (IsNothing(temp) = False) Then
                            FilteredlstMedications = (From med In lstMedications
                                                             Where temp.Contains(med.UniqueNo)
                                                             Select med).ToList()
                        End If
                    End Using
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            MedHxHttpWebRequest = Nothing
            MedHxHttpWebResponse = Nothing
            MedHxXMLReader = Nothing
        End Try
        Return FilteredlstMedications
    End Function

    Private Sub UpdateHistoryRequest()
        Dim oEligibilityCheck As New clsEligibilityCheckDBLayer()
        oEligibilityCheck.GetEligibilityCheck(PatientID)

        'Save data into Request detail table
        Dim oclsgloRxHubDBLayer = New clsgloRxHubDBLayer()
        oclsgloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString)
        oclsgloRxHubDBLayer.InsertRxH_HistoryRequestDetails("gsp_InUpRxH_HistoryRequest_DTL", oEligibilityCheck.Patient)
        oclsgloRxHubDBLayer.Disconnect()

        oclsgloRxHubDBLayer = Nothing

        If IsNothing(oEligibilityCheck) = False Then
            oEligibilityCheck.Dispose()
            oEligibilityCheck = Nothing
        End If
    End Sub

    Public Sub CheckMedHistoryDownloadorNot()
        Dim MedHxHttpWebRequest As HttpWebRequest = Nothing
        Dim MedHxHttpWebResponse As HttpWebResponse = Nothing
        Try
            Dim sURL As String = MedhxPortalURL & "api/MedHxInterfaceNew/" & RequestID.ToString()

            MedHxHttpWebRequest = DirectCast(HttpWebRequest.Create(sURL), HttpWebRequest)
            MedHxHttpWebRequest.Method = "GET"

            MedHxHttpWebResponse = DirectCast(MedHxHttpWebRequest.GetResponse(), HttpWebResponse)

            UpdateHistoryRequest()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            MedHxHttpWebRequest = Nothing
            MedHxHttpWebResponse = Nothing
        End Try
    End Sub

    Private Function CallBrowserButtonCLick() As String
        For Each btn As HtmlElement In wbMedHx.Document.GetElementsByTagName("input")
            If btn.GetAttribute("id") = "btnGetSelected" Then
                btn.InvokeMember("Click")
                Exit For
            End If
        Next
        For Each btn As HtmlElement In wbMedHx.Document.GetElementsByTagName("input")
            If btn.GetAttribute("id") = "btnSelectedItems" Then
                Return Convert.ToString(btn.GetAttribute("value"))
                Exit For
            End If
        Next
        Return Nothing
    End Function
End Class
