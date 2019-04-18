Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloGeneral.clsgeneral
Imports System.IO

Public Class FrmAllowAccess

   
    Dim nCount As Integer = 0
    Dim sPassword As String = ""
    Dim PatientId As Long = 0
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private Sub FrmAllowAccess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtPassword.Text = ""
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtPassword.Text = "" Then
            MessageBox.Show("Please Enter Emergency Access Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        End If

        gbAllowEmergencyAccess = False
        Dim cmd As SqlCommand = Nothing
        Dim Conn As SqlConnection = Nothing
        Try
            Conn = New SqlConnection(GetConnectionString())
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Dim oEncryption As New clsencryption
            sPassword = oEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)            
            oEncryption = Nothing
            cmd = New SqlCommand("select count(*) from user_mst where nuserid=" & gnLoginID & " and sAccessPassword ='" & sPassword & "'", Conn)
            cmd.CommandType = CommandType.Text
            cmd.Connection = Conn

            nCount = cmd.ExecuteScalar()

            If nCount > 0 Then
                appSettings("BreakTheGlass") = "true"
                gbAllowEmergencyAccess = True
                ''Bug #67150 ( Modified): 00000555 : Patient Lock Chart Issue
                'Code commented here and written in dashboard when this form exits
                'Dim oShowDetails As New MainMenu
                ''oShowDetails.SelectedPatientDetail = oShowDetails.PatientDetails.History
                'oShowDetails.ShowPatientAllDetails(PatientId)
                'oShowDetails = Nothing
                ''Added Rahul P on 20100916
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Emergency Access Login Successful.", PatientId, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                ''
                Me.Close()
            Else
                appSettings("BreakTheGlass") = "false"
                gbAllowEmergencyAccess = False
                MessageBox.Show("Incorrect Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''Added Rahul P on 20100916
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.BreakTheGlass, gloAuditTrail.ActivityType.EmergencyAccess, "Incorrect Password for Emergency Access.", PatientId, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
                txtPassword.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Conn IsNot Nothing Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
            End If

        End Try

    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click        
        Me.Close()
    End Sub


    Private Sub txtPassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOK_Click(sender, e)
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal _Patientid As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PatientId = _Patientid
    End Sub
End Class
