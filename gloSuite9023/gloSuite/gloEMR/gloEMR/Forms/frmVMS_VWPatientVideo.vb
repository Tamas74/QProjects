Public Class frmVMS_VWPatientVideo
    Implements IPatientContext

    Private COL_ID As Integer = 0
    Private COL_TransID As Integer = 1
    Private COL_VisitID As Integer = 2
    Private COL_UserID As Integer = 3
    Private COL_VisitDate As Integer = 4
    Private COL_VideoName As Integer = 5
    Private COL_Title As Integer = 6
    Private COL_StartTime As Integer = 7
    Private COL_EndTime As Integer = 8
    Private COL_Comments As Integer = 9

    Private COL_COUNT As Integer = 10
    Dim _PatientID As Long

    Private Sub frmVMS_VWPatientVideo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmVMS_VWPatientVideo_HelpButtonClicked(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.HelpButtonClicked

    End Sub

    Private Sub pnlMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        gloC1FlexStyle.Style(c1VideoList)
        Call SetGridStyle()
        Call FillGrid()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Public Sub SetGridStyle()

        With c1VideoList
            Dim _TotalWidth As Single = .Width - 5
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0
            .Cols.Count = COL_COUNT



            .Cols(COL_ID).Width = _TotalWidth * 0
            .SetData(0, COL_ID, "ID")
            .Cols(COL_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_ID).AllowEditing = False

            .Cols(COL_TransID).Width = _TotalWidth * 0
            .SetData(0, COL_TransID, "Transaction ID")
            .Cols(COL_TransID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_TransID).AllowEditing = False

            .Cols(COL_VisitID).Width = _TotalWidth * 0
            .SetData(0, COL_VisitID, "Visit ID")
            .Cols(COL_VisitID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_VisitID).AllowEditing = False

            .Cols(COL_UserID).Width = _TotalWidth * 0
            .SetData(0, COL_UserID, "User ID")
            .Cols(COL_UserID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_UserID).AllowEditing = False

            .Cols(COL_VisitDate).Width = _TotalWidth * 0.18
            .SetData(0, COL_VisitDate, "Visit Date")
            .Cols(COL_VisitDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_VisitDate).AllowEditing = False

            .Cols(COL_VideoName).Width = _TotalWidth * 0
            .SetData(0, COL_VideoName, "Video Name")
            .Cols(COL_VideoName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_VideoName).AllowEditing = False

            .Cols(COL_Title).Width = _TotalWidth * 0.37
            .SetData(0, COL_Title, "Title")
            .Cols(COL_Title).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_Title).AllowEditing = False

            .Cols(COL_StartTime).Width = _TotalWidth * 0.17
            .SetData(0, COL_StartTime, "Start Time")
            .Cols(COL_StartTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_StartTime).AllowEditing = False

            .Cols(COL_EndTime).Width = _TotalWidth * 0.17
            .SetData(0, COL_EndTime, "End Time")
            .Cols(COL_EndTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_EndTime).AllowEditing = False

            .Cols(COL_Comments).Width = _TotalWidth * 0
            .SetData(0, COL_Comments, "Comments")
            .Cols(COL_Comments).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_Comments).AllowEditing = False
        End With
    End Sub

    Public Sub FillGrid()
        Try
            Dim oTranscation As New gloStream.gloVMS.VMSTranscation.Transaction_Master
            Dim dt As New DataTable
            dt = oTranscation.GetVideodetailsList(_PatientID)
            ''''set the details of video

            With c1VideoList
                For i As Integer = 0 To dt.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_ID, dt.Rows(i)("vtd_ID"))
                    .SetData(.Rows.Count - 1, COL_TransID, dt.Rows(i)("vtd_trnID"))
                    .SetData(.Rows.Count - 1, COL_VisitID, dt.Rows(i)("vtm_VisitID"))
                    .SetData(.Rows.Count - 1, COL_UserID, dt.Rows(i)("vtd_UserID"))
                    .SetData(.Rows.Count - 1, COL_VisitDate, dt.Rows(i)("vtm_DOS"))
                    .SetData(.Rows.Count - 1, COL_VideoName, dt.Rows(i)("vtd_VideoName"))
                    .SetData(.Rows.Count - 1, COL_Title, dt.Rows(i)("vtd_Note"))
                    .SetData(.Rows.Count - 1, COL_StartTime, dt.Rows(i)("vtd_StartTime"))
                    .SetData(.Rows.Count - 1, COL_EndTime, dt.Rows(i)("vtd_EndTime"))
                    .SetData(.Rows.Count - 1, COL_Comments, dt.Rows(i)("vtd_Comments"))
                Next
            End With
            oTranscation = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub DeleteCategory()
        If c1VideoList.Rows.Count = 1 Then
            Exit Sub
        End If
        Dim oTranscation As New gloStream.gloVMS.VMSTranscation.Transaction_Master
        oTranscation.DeletePatientVideo(c1VideoList.GetData(c1VideoList.Row, COL_ID), c1VideoList.GetData(c1VideoList.Row, COL_TransID))
        c1VideoList.RemoveItem(c1VideoList.Row)
    End Sub


    Private Sub RefreshCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            c1VideoList.Rows.Count = 1
            Call FillGrid()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub c1VideoList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1VideoList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class