Public Class frmReviewOfHistory
    Inherits System.Windows.Forms.Form
    'Dim objclsPatientHistory As New clsPatientHistory
    ' Dim objclsMessage As New clsMessage
    Dim UserID As Long


    Private r_VisitID As Long
    Private r_patientID As Long
    Private r_VisitDate As Date
    Private r_LoginName As String
    Private r_intCheck As Integer

    Public CommentsEntered As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub


    Public Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal rLoginName As String, ByVal rPatientId As Long, ByVal rintCheck As Integer)
        MyBase.New()
        r_VisitID = VisitID
        r_VisitDate = VisitDate.Date
        r_LoginName = rLoginName
        r_patientID = rPatientId
        r_intCheck = rintCheck
        Dim objclsMessage As New clsMessage
        UserID = objclsMessage.GetUserID(r_LoginName)
        objclsMessage.Dispose()
        objclsMessage = Nothing
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
#End Region

    Private Sub frmReviewOfHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtReviewDate.Format = DateTimePickerFormat.Custom
        dtReviewDate.CustomFormat = "MM/dd/yyyy hh:mm:ss"

        txtUserName.Text = r_LoginName
        txtComment.Select()
        txtComment.Focus()
        'Dim oDB As New gloStream.gloDataBase.gloDataBase
        'Dim strSelect = "Select dtReviewDate,sComments from ReviewHistory where nPatientID= " & r_patientID & " and nVisitID= " & r_VisitID & " "
        'Dim dt As New DataTable
        'oDB.Connect(GetConnectionString)
        'dt = oDB.ReadQueryDataTable(strSelect)
        'oDB.Disconnect()
        'If IsNothing(dt) = False Then
        '    If dt.Rows.Count > 0 Then
        '        dtReviewDate.Text = Format(dt.Rows(0)("dtReviewDate"), "MM/dd/yyyy hh:mm:ss")
        '        txtComment.Text = dt.Rows(0)("sComments")
        '    End If
        'End If

    End Sub

    Private Sub OKReviewHistory()
        Try
            If txtComment.Text.Trim = "" Then
                MessageBox.Show("Review Comments are not entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtComment.Focus()
                Exit Sub
            End If

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            With oDB
                .DBParameters.Add("@nVisitID", r_VisitID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@nPatientID", r_patientID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@dtReviewDate", dtReviewDate.Value, ParameterDirection.Input, SqlDbType.DateTime)
                .DBParameters.Add("@sComment", txtComment.Text.Trim, ParameterDirection.Input, SqlDbType.VarChar, 255)
                .Connect(GetConnectionString)
                'If .ExecuteNonQuery("gsp_INSERT_PatientTracking") = True Then
                '    lblBtnCheckIn.Enabled = False
                '    lblBtnStatus.Enabled = False
                '    lblBtnCheckOut.Enabled = False
                'End If
                .ExecuteNonQuery("gsp_INSERT_ReviewOfHistory")
                CommentsEntered = True
                .Disconnect()
            End With
            Me.DialogResult = Windows.Forms.DialogResult.OK
            oDB = Nothing
            ' gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Other, "Patient History Reviewed", gstrLoginName, gstrClientMachineName, gnPatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient History Reviewed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient History Reviewed", r_patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelReviewHistory()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub tlsp_ReviewofHistory_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ReviewofHistory.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OKReviewHistory()

                Case "Cancel"
                    CancelReviewHistory()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class