Imports System.Data.SqlClient
Public Class frmAddDicomNotes
    

#Region "Page Manuipulation Variables"
    Public oDialogResultIsOK As Boolean = False
    Public oClinicID As Int64 = 0
    Public oDicomID As Int64 = 0
    Private oDICOMDetID As Int64 = 0
    Public oPatientID As Int64 = 0

    Public _ErrorMessage As String = ""
    Public _messageboxCaption As String = "gloEMR"


    Private blnIsDescExists As Boolean = False
 
    Public Sub New()
         InitializeComponent()
    End Sub

    Public Sub New(ByVal dicomID As Int64, ByVal DICOMDetID As Long, ByVal PatientID As Int64)

        InitializeComponent()

        oDicomID = dicomID
        oDICOMDetID = DICOMDetID
        oPatientID = PatientID

    End Sub

#End Region

    Private Sub frmAddDicomNotes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objDICOM As New clsDICOM
        Dim strSelect As String = ""

        Try
            txtUserName.Text = objDICOM.GetLoginName(gnLoginID)

            'strSelect = "select isnull(Description,'') as Description from  DICOM_AckRvwNotes where ntype = 3 and DICOMID= " & oDicomID & " and DICOMDetID= " & oDICOMDetID & ""

            'txtNotes.Text = objDICOM.GetDICOMDescription(strSelect)

            'If txtNotes.Text <> "" Then
            '    blnIsDescExists = True
            'End If

            'If (txtNotes.Text.Trim() <> "") Then
            '    tlb_Delete.Visible = True
            'Else
            '    tlb_Delete.Visible = False
            'End If

            txtNotes.Select()
        Catch ex As Exception
            If oDicomID = 0 Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        objDICOM = Nothing
    End Sub

    Private Sub tlb_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Cancel.Click
        oDialogResultIsOK = False
        Me.Close()
    End Sub

    Private Sub tlb_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Ok.Click
        Dim objDICOM As New clsDICOM

        Try
            If txtNotes.Text.Trim() = "" Then
                MessageBox.Show("Enter notes.", _messageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            ''Save notes  
            ' oDialogResultIsOK = UpdateNotes(oDicomID, txtNotes.Text.Trim())


            Dim MachineID As Long = 0
            MachineID = GetPrefixTransactionID(oPatientID)
            Dim RetDICOMID As Long = 0
            oDialogResultIsOK = objDICOM.InsertUpdateDICOMAckRvwNotes(MachineID, oDICOMDetID, oDicomID, clsDICOM.DICOMDescType.Notes, txtNotes.Text.Trim, txtUserName.Text.Trim(), RetDICOMID)
            ', ByVal nType As DICOMDescType, ByVal Description As String, ByVal nUserID As Long, ByRef RetDICOMDetID As Long)

            If oDialogResultIsOK = True Then
                Me.Close()
            Else
                MessageBox.Show("There is some problem while adding notes.  ", _messageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            End If

        Catch ex As Exception
            If oDicomID = 0 Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DICOM, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If

            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            'tlb_Ok.Enabled = True
            'tlb_Cancel.Enabled = True
            'tlb_Notes.Enabled = True
            'tlb_Delete.Enabled = True
        End Try
        objDICOM = Nothing
    End Sub

    Private Sub tlb_Notes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Notes.Click

    End Sub

    Private Sub tlb_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Delete.Click

        'Dim _result As Boolean = False
        'Try
        '    ''Update notes set notes as blank
        '    _result = UpdateNotes(oDicomID, "")
        '    If _result = False Then
        '        MessageBox.Show("Problem while deleting notes.  ", _messageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Else
        '        '  frmAddDicomNotes_Load(Nothing, Nothing)
        '        Me.Close()
        '    End If
        'Catch ex As Exception

        'End Try

    End Sub
  
End Class