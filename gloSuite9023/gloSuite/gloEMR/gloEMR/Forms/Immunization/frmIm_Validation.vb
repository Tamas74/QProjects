Public Class frmIm_Validation

    Dim ImmValidationMsg As String = ""

    Public Sub New(ByVal strImValidationMessage As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ImmValidationMsg = strImValidationMessage

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _btnClick As Int16

    Public Property btnClick() As Int16
        Get
            Return _btnClick
        End Get
        Set(ByVal value As Int16)
            _btnClick = value
        End Set
    End Property

    Private Sub frmIm_Validation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            'ImmValidationMsg = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub frmIm_Validation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblImMessage.Text = "The data to generate MCIR report is incomplete and following data are missing. " & vbCrLf & "Click Save&&Cls to save current immunization without generating MCIR file." & vbCrLf & "Click Close to correct the data."
            rchtxtImValidation.Text = ImmValidationMsg

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Ok.Click
        Try
            _btnClick = 1
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub tblbtn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close.Click
        Try
            _btnClick = 2
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class