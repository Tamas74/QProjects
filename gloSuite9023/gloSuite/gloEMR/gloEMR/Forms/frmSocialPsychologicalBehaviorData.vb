Imports gloGlobal

Public Class frmSocialPsychologicalBehaviorData

   

    'Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, Optional ByVal CarePlanID As Long = 0)
    '    ' This call is required by the Windows Form Designer.                
    '    InitializeComponent()
    '    _PatientID = PatientID
    '    _VisitID = VisitID
    '    _CarePlanID = CarePlanID
    '    ' Add any initialization after the InitializeComponent() call.  
    'End Sub
    Dim nPatientID As Long
    Dim nVisitID As Long
    Dim sConnectionString As String = String.Empty

    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip

    Dim oDynamicSBP As gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP = Nothing

    Public Sub New(ByVal PatientID As Long, ByVal VisitID As Long, ByVal ApplicationConnectionString As String)
        ' This call is required by the Windows Form Designer.                
        InitializeComponent()

        nPatientID = PatientID
        nVisitID = VisitID
        sConnectionString = ApplicationConnectionString

        ' Add any initialization after the InitializeComponent() call.  
    End Sub

    Private Sub frmSocialPsychologicalBehaviorData_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            gloGlobal.SBPQuestionnaire.DynamicFormData.AppConnectionString = sConnectionString
            gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientId = nPatientID
            gloGlobal.SBPQuestionnaire.DynamicFormData.nVisitId = nVisitID
            gloGlobal.SBPQuestionnaire.DynamicFormData.nPatientProviderId = gnPatientProviderID
            gloGlobal.SBPQuestionnaire.DynamicFormData.nLoginsessionId = gintLoginSessionID
            gloGlobal.SBPQuestionnaire.DynamicFormData.PatientProviderName = gstrPatientProviderName

            Me.Text = "Patient Social Psychological Behavioral observations"


            ''Dim oDynamicSBP As New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP(sConnectionString)
            oDynamicSBP = New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP()
           
            Set_PatientDetailStrip()
            oDynamicSBP.Dock = DockStyle.Fill
            pnlWebbrowserCtrl.Controls.Add(oDynamicSBP)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        Try
            gloUC_PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip

            With gloUC_PatientStrip
                .Dock = DockStyle.Top
                .Padding = New Padding(3, 0, 3, 0)

                .ShowDetail(nPatientID)

                .SendToBack()
                '.DTPValue = Format(Now, "MM/dd/yyyy")
                .DTPEnabled = False
            End With
            Me.Controls.Add(gloUC_PatientStrip)
            pnlSBPToolBar.SendToBack()
            pnlWebbrowserCtrl.BringToFront()
            'C1_CarePlan.BringToFront()
            'tbCarePlan.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlstrpBtnSaveClose_Click(sender As System.Object, e As System.EventArgs) Handles tlstrpBtnSaveClose.Click
        Try
            ''oDynamicSBP = New gloGlobal.SBPQuestionnaire.usrCtrlDynamicSBP()
            If oDynamicSBP.SaveSBPData() Then ''return true then close the form
                Me.Close()
            Else ''do not close the form
                'Me.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlstrpBtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlstrpBtnClose.Click
        tlstrpBtnSaveClose_Click(sender, e)
    End Sub

    Private Sub tlstrpbtnSBPHistory_Click(sender As System.Object, e As System.EventArgs) Handles tlstrpbtnSBPHistory.Click
        Try
            oDynamicSBP.SBPHistory()

        Catch ex As Exception

        End Try
    End Sub
End Class