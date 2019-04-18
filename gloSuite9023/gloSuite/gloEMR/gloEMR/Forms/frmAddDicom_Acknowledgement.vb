Public Class frmAddDicom_Acknowledgement
    Private _blnIsAcknowledgement As Boolean = False
    Private _DICOMID As Long = 0
    Private _DICOMDetID As Long = 0
    Dim _PatientID As Long


    Public Sub New(ByVal DICOMID As Long, ByVal DICOMDetID As Long, ByVal IsAck As Boolean, ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _blnIsAcknowledgement = IsAck
        _DICOMID = DICOMID
        _DICOMDetID = DICOMDetID
        _PatientID = PatientID
    End Sub

    Private Sub frmAddDicom_Acknowledgement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            'If _blnIsAcknowledgement = True Then
            '    'acknowledgement
            '    Me.Text = "Add Acknowledgement"
            '    txtComment.Text = FillAcknowledgements(0)
            'Else
            '    'review
            '    Me.Text = "Add Review"
            '    txtComment.Text = FillAcknowledgements(1)
            'End If


            '  lblUser.Visible = True
            ' cmbUser.Visible = True
            lblComment.Visible = True
            txtComment.Visible = True
            tlb_Delete.Visible = False
            ' tlb_Review.Visible = False
            '  //tlb_History.Visible = false;

            txtComment.Text = ""
            cmbUser.SelectedIndex = -1


            If _blnIsAcknowledgement = True Then
                'acknowledgement
                Me.Text = "Add Acknowledgement"

            Else
                'review
                Me.Text = "Add Review"

            End If
            Dim objDICOM As New clsDICOM

            '  txtComment.Text = FillAcknowledgements(_blnIsAcknowledgement)
            txtComment.Text = ""
            txtUserName.Text = objDICOM.GetLoginName(gnLoginID)

            objDICOM = Nothing
            ' End If

            '  cmbUser.Enabled = False

            '  FillUserCombo()


            '    Dim _UsrName As String = eDocManager.eDocValidator.GetUserName(gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID)
            'Dim _UsrName As String = gstrLoginName
            'For i As Integer = 0 To cmbUser.Items.Count - 1
            '    If cmbUser.Items(i).ToString().ToUpper() = _UsrName.ToUpper() Then
            '        cmbUser.SelectedIndex = i
            '        Exit For
            '    End If
            'Next

            txtComment.Select()

        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.PatientSynopsis, ActivityCategory.ElectroPhysiology, ActivityType.Add,ex.ToString(), ActivityOutCome.Failure)
            If _blnIsAcknowledgement = True Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End If

        End Try
    End Sub

    Private Sub FillUserCombo()
        Dim _UserList As New ArrayList()
        Dim objDICOM As New clsDICOM

        Try
            _UserList = objDICOM.GetUsers()
            If _UserList IsNot Nothing AndAlso _UserList.Count > 0 Then
                For i As Integer = 0 To _UserList.Count - 1
                    cmbUser.Items.Add(_UserList(i).ToString())
                Next
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If _UserList IsNot Nothing Then
                _UserList = Nothing
            End If
            objDICOM = Nothing
        End Try
    End Sub

    Private Function FillAcknowledgements(ByVal flag As Boolean) As String
        Dim objDICOM As New clsDICOM
        Dim _result As String = ""

        Try
            If flag = True Then
                'ack
                '_result = objDICOM.FillAcknowledgements("select isnull(AckRvwDescription,'') as AckRvwDescription from DicomDetails where IsAckReview = 1 and DICOMID=" & _DICOMID & "")
                _result = objDICOM.FillAcknowledgements("select isnull(Description,'') as AckRvwDescription from DICOM_AckRvwNotes where ntype = 1 and DICOMID=" & _DICOMID & "")
            Else
                'review
                _result = objDICOM.FillAcknowledgements("select isnull(Description,'') as AckRvwDescription from DICOM_AckRvwNotes where ntype =2 and DICOMID=" & _DICOMID & "")
            End If

        Catch ex As Exception
            If _blnIsAcknowledgement = True Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End If
        End Try
        objDICOM = Nothing
        Return _result
    End Function

    Private Sub tlb_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Ok.Click
        Try

            Dim objDICOM As New clsDICOM

            '  objDICOM.UpdateNotes(_DICOMID, txtComment.Text.Trim, _blnIsAcknowledgement)

            Dim machineid As Long = 0
            machineid = GetPrefixTransactionID(_PatientID)

            Dim RetDICOMDetailID As Long = 0
            If _blnIsAcknowledgement = True Then
                objDICOM.InsertUpdateDICOMAckRvwNotes(machineid, 0, _DICOMID, clsDICOM.DICOMDescType.Acknowledge, txtComment.Text.Trim, txtUserName.Text.Trim, RetDICOMDetailID)

            Else
                objDICOM.InsertUpdateDICOMAckRvwNotes(machineid, 0, _DICOMID, clsDICOM.DICOMDescType.Review, txtComment.Text.Trim, txtUserName.Text.Trim, RetDICOMDetailID)

            End If
            Me.Close()
            objDICOM = Nothing
        Catch ex As Exception
            If _blnIsAcknowledgement = True Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End If
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tlb_Review_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlb_Review.Click

    End Sub

    Private Sub tlb_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Cancel.Click
        Me.Close()
    End Sub
End Class