Imports DrawingControl
Imports gloAuditTrail
Imports gloSettings
Public Class frmDrawingPad
    Private WithEvents Dpad As DrawingControl.gloDrawingPad

    Public mycaller As frmPatientExam
    Private sDrawingImagePath As String = ""
    Public blnInsertStatus As Boolean
    Public Property DrawingImagePath() As String
        Get
            Return sDrawingImagePath
        End Get
        Set(ByVal value As String)
            sDrawingImagePath = value
        End Set
    End Property

    Private Sub frmDrawingPad_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        For Each myForm As Form In Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
    End Sub

    Private Sub frmDrawingPad_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.TopMost = False
    End Sub

    Private Sub frmDrawingPad_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If Not IsNothing(Dpad) Then 
                If Me.pnlfill.Controls.Count > 0 Then
                    Me.pnlfill.Controls.Remove(Dpad)

                End If
                Dpad.mydispose()
                Dpad.Dispose()
                Dpad = Nothing
            End If


        Catch ex As DrawingControl.DrawingControlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Close, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Close, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub frmDrawingPad_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dpad = New gloDrawingPad(System.Windows.Forms.Application.StartupPath, sDrawingImagePath)

            If gintPenWidth = 0 Then
                Dpad.PenWidth = 1
            Else
                Dpad.PenWidth = gintPenWidth
            End If
            Dpad.DrawingImagePath = sDrawingImagePath
            Dpad.gloApplicationPath = System.Windows.Forms.Application.StartupPath
            Me.pnlfill.Controls.Add(Dpad)
            Dpad.Dock = DockStyle.Fill

        Catch ex As DrawingControl.DrawingControlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Initialize, ex.ToString(), ActivityOutCome.Failure)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Initialize, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        sDrawingImagePath = ""
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    ''solving salesforce case-GLO2010-0005441
    Private Sub Dpad_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dpad.btnCloseForm
        Me.Close()
    End Sub

    Private Sub Dpad_btnClick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal blninsert As Boolean) Handles Dpad.btnClick
        'Dim regKey As Microsoft.Win32.RegistryKey
        Try
            'If Not IsNothing(Dpad) Then
            '    If blninsert = True Then
            '        If Dpad.PenWidth <> gintPenWidth Then
            '            If IsNothing(Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
            '                regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
            '                regKey.CreateSubKey("gloEMR")
            '                regKey.Close()
            '            End If
            '            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
            '            regKey.SetValue("PenWidth", Dpad.PenWidth)
            '            gintPenWidth = Dpad.PenWidth
            '        End If
            '    End If
            'End If
            If Not IsNothing(Dpad) Then
                If blninsert = True Then
                    If Dpad.PenWidth <> gintPenWidth Then
                        If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                            gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                        End If
                        gloRegistrySetting.CloseRegistryKey()
                        'GLO2010-0008217'Registry error when drawing in a chart
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                        gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPenwdth, Dpad.PenWidth)
                        gloRegistrySetting.CloseRegistryKey()
                        gintPenWidth = Dpad.PenWidth
                    End If
                End If
            End If

            blnInsertStatus = blninsert
            Me.Close()

        Catch ex As DrawingControl.DrawingControlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Select, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.DrawingPad, ActivityType.Select, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try

    End Sub

 
End Class