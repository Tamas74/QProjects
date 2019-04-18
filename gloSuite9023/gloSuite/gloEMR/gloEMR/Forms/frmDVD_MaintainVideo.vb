Imports gloEMR
Imports gloEMRGeneralLibrary
Imports gloUserControlLibrary
Imports System.IO
Imports System.Windows.Forms.Cursor
Imports AxWMPLib
Imports WMPLib
Imports System.Runtime.InteropServices

Public Class frmDVD_MaintainVideo
    Implements IPatientContext
    Dim _DestinationFolder As String = ""
    Private COL_HiddenYear As Integer = 0
    Private COL_HiddenMonth As Integer = 1
    Private COL_VideoName As Integer = 2
    Private COL_Path As Integer = 3
    Private COL_DocumentFileName As Integer = 4
    Private COL_IsAcknowledge As Integer = 5
    Private COL_Flag As Integer = 6
    Private COL_COUNT As Integer = 7
    Dim oDMS As gloStream.gloDMS.Supporting.Supporting
    Dim _PatientID As Long
#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            '' Pass Paarameters Type of Form
            .ShowDetail(_PatientID) '', gloUserControlLibrary.gloUC_PatientStrip.enumFormName.History)
          
            .DTPValue = Now
            .DTPEnabled = False
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        gloUC_PatientStrip1.BringToFront()
        sc_Main.BringToFront()
    End Sub

#End Region

    Private Sub frmDVD_MaintainVideo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmDVD_MaintainVideo_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(gloUC_PatientStrip1) = False) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmDVD_MaintainVideo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        
        gloC1FlexStyle.Style(c1CategorisedDocuments) 'OJESWINI

        Try
            Call Set_PatientDetailStrip()
            Call setGridStyle()
            tb_SeekBar.Enabled = False
            Call SetPlayList()

            If (c1CategorisedDocuments.Row > 0) Then
                If IsNothing(c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge)) Then
                    ts_btnAcknowlegment.Visible = False
                    ts_btnViewAcknowlegment.Visible = False
                ElseIf c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge) = 0 Then
                    ts_btnAcknowlegment.Visible = True
                    ts_btnViewAcknowlegment.Visible = False
                Else
                    ts_btnViewAcknowlegment.Visible = True
                    ts_btnAcknowlegment.Visible = False
                End If
            End If
            'tooltpPause.SetToolTip(btnPlayPause, "Play")
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'tooltpPause.ToolTipTitle = "Pause"
    End Sub

    Private Sub btnUploadVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadVideo.Click, ts_btnAdd.Click
        'Dim oFile As String
        Try
            oDMS = New gloStream.gloDMS.Supporting.Supporting
            Dim objImportVideo As New frmVMS_ImportDocumentEvent(_PatientID)
            With objImportVideo
                .ProcessParameter.PatientID = _PatientID
                .ProcessParameter.Month = oDMS.MonthAsString(Now.Month)
                .ProcessParameter.Year = Now.Year
                .ProcessParameter.Category = VMSCategory
                .ProcessParameter.Container = VMSContainer
                .ProcessParameter.SystemFolder = VMSSystem ''  "VMS System"
                .ProcessParameter.Documents = Nothing
                .ProcessParameter.DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.None

                .StartPosition = FormStartPosition.CenterParent
                .ShowInTaskbar = False
                .ShowDialog(IIf(IsNothing(objImportVideo.Parent), Me, objImportVideo.Parent))
                .Dispose()
            End With

            Call SetPlayList()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  
    
   
    Public Sub setGridStyle()
        With c1CategorisedDocuments
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0
            .Cols.Count = COL_COUNT

            ''''Column setting for flexgrid  
            .Cols(COL_HiddenYear).Width = 0
            .SetData(0, COL_HiddenYear, "Hidden Year")
            .Cols(COL_HiddenYear).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_HiddenYear).AllowEditing = False

            .Cols(COL_HiddenMonth).Width = 0
            .SetData(0, COL_HiddenMonth, "Hidden Month")
            .Cols(COL_HiddenMonth).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_HiddenMonth).AllowEditing = False

            .Cols(COL_VideoName).Width = 250
            .SetData(0, COL_VideoName, "Video Name")
            .Cols(COL_VideoName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_VideoName).AllowEditing = False
            .Cols(COL_VideoName).AllowDragging = False


            .Cols(COL_DocumentFileName).Width = 0
            .SetData(0, COL_DocumentFileName, "Document File Name")
            .Cols(COL_DocumentFileName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_DocumentFileName).AllowEditing = False

            .Cols(COL_Path).Width = 0
            .SetData(0, COL_Path, "Path")
            .Cols(COL_Path).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_Path).AllowEditing = False

            .Cols(COL_IsAcknowledge).Width = 0
            .SetData(0, COL_IsAcknowledge, "Acknowledge")
            .Cols(COL_IsAcknowledge).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_IsAcknowledge).AllowEditing = False

            .Cols(COL_Flag).Width = 19
            .SetData(0, COL_Flag, "")
            .Cols(COL_Flag).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            .Cols(COL_Flag).AllowEditing = False
            .Cols(COL_Flag).AllowDragging = False



        End With
    End Sub
    Public Sub SetPlayList()
        'If File.Exists(VMSRootPath) = False Then
        '    Exit Sub
        'End If
        With c1CategorisedDocuments
            .Rows.Count = 1

            '''''Set the Tree Properties
            .Tree.Column = COL_VideoName
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15

            Dim cStyle As C1.Win.C1FlexGrid.CellStyle


            ''''Access the media file from database
            Dim dt As New DataTable
            Dim objField As New gloStream.gloVMS.gloVMS(_PatientID)
            Dim Month As New Collection
            dt = objField.GetVideoList(_PatientID)


            ''''Bind the media file to Tree Column 
            For i As Integer = 0 To dt.Rows.Count - 1
                ''''check the file is present in directory
                Dim strFilePath As String = VMSRootPath & "\" & dt.Rows(i)("SystemFolder") & "\" & dt.Rows(i)("Container") & "\" & dt.Rows(i)("Category") & "\" & dt.Rows(i)("PatientID") & "\" & dt.Rows(i)("Year") & "\" & dt.Rows(i)("Month") & "\" & dt.Rows(i)("DocumentFileName") & dt.Rows(i)("Extension")
                ''''If present then show in the playlist 
                If File.Exists(strFilePath) = True Then
                    ''''If the first file is to bind the flexgrid
                    If .Rows.Count = 1 Then

                        'cStyle = .Styles.Add("Year")
                        Try
                            If (.Styles.Contains("Year")) Then
                                cStyle = .Styles("Year")
                            Else
                                cStyle = .Styles.Add("Year")

                            End If
                        Catch ex As Exception
                            cStyle = .Styles.Add("Year")

                        End Try
                        cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                        ''''Bind Year as Parent node
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 0
                            .Node.Data = dt.Rows(i)("Year")
                        End With

                        Dim rgYear As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_VideoName, .Rows.Count - 1, COL_VideoName)
                        rgYear.Style = cStyle

                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) '         dt.Rows(i)("Month"))

                        ''''Bind Month as child Node 
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = dt.Rows(i)("Month") ' dt.Rows(i)("Month")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) 'dt.Rows(i)("Month"))

                        ''''Bind File Name as sub Child Node
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2
                            .Node.Data = dt.Rows(i)("DocumentDisplayName")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) '' Month.Item(1)) ' dt.Rows(i)("Month")
                        .SetData(.Rows.Count - 1, COL_DocumentFileName, dt.Rows(i)("DocumentFileName"))
                        Dim FilePath As String = VMSRootPath & "\" & VMSSystem & "\" & VMSContainer & "\" & VMSCategory & "\" & _PatientID & "\" & dt.Rows(i)("Year") & "\" & dt.Rows(i)("Month") & "\" & dt.Rows(i)("DocumentFileName") & dt.Rows(i)("Extension")
                        .SetData(.Rows.Count - 1, COL_Path, FilePath)
                        .SetData(.Rows.Count - 1, COL_IsAcknowledge, dt.Rows(i)("IsReviewed"))

                        If dt.Rows(i)("IsReviewed") = True Then
                            .SetCellImage(.Rows.Count - 1, COL_Flag, Img_Reviwed.Image)
                            ts_btnViewAcknowlegment.Visible = True
                        End If

                        ''''If the file is from same month and year then only bind file name as sub child Node
                    ElseIf .GetData((.Rows.Count - 1) - 1, COL_HiddenYear) = dt.Rows(i)("Year") And .GetData((.Rows.Count - 1) - 1, COL_HiddenMonth) = dt.Rows(i)("Month") Then  ''  If .Rows.Count = 1 Then

                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2
                            .Node.Data = dt.Rows(i)("DocumentDisplayName")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) 'dt.Rows(i)("Month"))
                        .SetData(.Rows.Count - 1, COL_DocumentFileName, dt.Rows(i)("DocumentFileName"))
                        Dim FilePath As String = VMSRootPath & "\" & VMSSystem & "\" & VMSContainer & "\" & VMSCategory & "\" & _PatientID & "\" & dt.Rows(i)("Year") & "\" & dt.Rows(i)("Month") & "\" & dt.Rows(i)("DocumentFileName") & dt.Rows(i)("Extension")
                        .SetData(.Rows.Count - 1, COL_Path, FilePath)
                        .SetData(.Rows.Count - 1, COL_IsAcknowledge, dt.Rows(i)("IsReviewed"))
                        If dt.Rows(i)("IsReviewed") = True Then
                            .SetCellImage(.Rows.Count - 1, COL_Flag, Img_Reviwed.Image)

                            ts_btnViewAcknowlegment.Visible = True
                        End If
                        '''' If the file is from same year but not from same month then add new month as a child node
                    ElseIf .GetData((.Rows.Count - 1) - 1, COL_HiddenYear) = dt.Rows(i)("Year") And .GetData((.Rows.Count - 1) - 1, COL_HiddenMonth) <> dt.Rows(i)("Month") Then

                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = dt.Rows(i)("Month") 'dt.Rows(i)("Month")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month"))  'dt.Rows(i)("Month"))

                        .Rows.Add()
                        ''''Add the sub child node as a file name for  the month
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2
                            .Node.Data = dt.Rows(i)("DocumentDisplayName")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) ' dt.Rows(i)("Month"))
                        .SetData(.Rows.Count - 1, COL_DocumentFileName, dt.Rows(i)("DocumentFileName"))
                        Dim FilePath As String = VMSRootPath & "\" & VMSSystem & "\" & VMSContainer & "\" & VMSCategory & "\" & _PatientID & "\" & dt.Rows(i)("Year") & "\" & dt.Rows(i)("Month") & "\" & dt.Rows(i)("DocumentFileName") & dt.Rows(i)("Extension")
                        .SetData(.Rows.Count - 1, COL_Path, FilePath)
                        .SetData(.Rows.Count - 1, COL_IsAcknowledge, dt.Rows(i)("IsReviewed"))
                        If dt.Rows(i)("IsReviewed") = True Then
                            .SetCellImage(.Rows.Count - 1, COL_Flag, Img_Reviwed.Image)

                            ts_btnViewAcknowlegment.Visible = True
                        End If
                        ''''If the year is not present then add new node as a parent node and bind year to that node
                    ElseIf .GetData((.Rows.Count - 1) - 1, COL_HiddenYear) <> dt.Rows(i)("Year") Then

                        ' cStyle = .Styles.Add("Year")
                        Try
                            If (.Styles.Contains("Year")) Then
                                cStyle = .Styles("Year")
                            Else
                                cStyle = .Styles.Add("Year")

                            End If
                        Catch ex As Exception
                            cStyle = .Styles.Add("Year")

                        End Try
                        cStyle.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 0
                            .Node.Data = dt.Rows(i)("Year")
                        End With

                        Dim rgYear As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_VideoName, .Rows.Count - 1, COL_VideoName)
                        rgYear.Style = cStyle

                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) ' dt.Rows(i)("Month"))

                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = dt.Rows(i)("Month") 'dt.Rows(i)("Month")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) '    dt.Rows(i)("Month"))

                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2
                            .Node.Data = dt.Rows(i)("DocumentDisplayName")
                        End With
                        .SetData(.Rows.Count - 1, COL_HiddenYear, dt.Rows(i)("Year"))
                        .SetData(.Rows.Count - 1, COL_HiddenMonth, dt.Rows(i)("Month")) ' dt.Rows(i)("Month"))
                        .SetData(.Rows.Count - 1, COL_DocumentFileName, dt.Rows(i)("DocumentFileName"))
                        Dim FilePath As String = VMSRootPath & "\" & VMSSystem & "\" & VMSContainer & "\" & VMSCategory & "\" & _PatientID & "\" & dt.Rows(i)("Year") & "\" & dt.Rows(i)("Month") & "\" & dt.Rows(i)("DocumentFileName") & dt.Rows(i)("Extension")
                        .SetData(.Rows.Count - 1, COL_Path, FilePath)
                        .SetData(.Rows.Count - 1, COL_IsAcknowledge, dt.Rows(i)("IsReviewed"))
                        If dt.Rows(i)("IsReviewed") = True Then
                            .SetCellImage(.Rows.Count - 1, COL_Flag, Img_Reviwed.Image)

                            ts_btnViewAcknowlegment.Visible = True
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    

    Private Sub btnPlayPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayPause.Click
        Try
            If c1CategorisedDocuments.Rows.Count <= 1 Then
                Exit Sub
            End If

            If c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_DocumentFileName) = "" Then
                Exit Sub
            End If
            If WMPlayer.playState = WMPLib.WMPPlayState.wmppsPlaying Then
                WMPlayer.Ctlcontrols.pause()
            ElseIf WMPlayer.playState = WMPLib.WMPPlayState.wmppsPaused Then
                WMPlayer.Ctlcontrols.play()
            Else
                Playfile()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
      
    End Sub

    Private Sub WMPlayer_PlayStateChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles WMPlayer.PlayStateChange
        Try
            SetUIState(e.newState)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Exception")
        End Try
    End Sub

    Private Sub SetUIState(ByVal newState As WMPLib.WMPPlayState)
        Select Case newState
            Case WMPLib.WMPPlayState.wmppsPlaying
                btnPlayPause.BackgroundImage = pic_Pause.Image
                Timer1.Start()
            Case WMPLib.WMPPlayState.wmppsPaused
                btnPlayPause.BackgroundImage = pic_Play.Image
                Timer1.Stop()
            Case WMPLib.WMPPlayState.wmppsStopped
                btnPlayPause.BackgroundImage = pic_Play.Image
                Timer1.Start()
            Case Else
                btnPlayPause.BackgroundImage = pic_Pause.Image
                Timer1.Start()
        End Select
    End Sub

    Private Sub tb_SeekBar_ChangeUICues(ByVal sender As Object, ByVal e As System.Windows.Forms.UICuesEventArgs) Handles tb_SeekBar.ChangeUICues

    End Sub

    Private Sub tb_SeekBar_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles tb_SeekBar.Scroll
        Try
            If (WMPlayer.currentMedia.duration <> 0) Then
                Dim newPerc As Double = Convert.ToDouble(tb_SeekBar.Value) / 100
                Dim duration As Integer = Convert.ToInt32(WMPlayer.currentMedia.duration * 1000) ' milliseconds
                Dim newPos As Integer = (duration * newPerc) / 1000 ' seconds

                ' Seek the Player
                WMPlayer.Ctlcontrols.currentPosition = newPos
            Else
                tb_SeekBar.Value = 0 'No duration available. Just ground the slider.
                btnPlayPause.BackgroundImage = pic_Play.Image
            End If
        Catch comExc As COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, comExc.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim hr As Integer = comExc.ErrorCode
            Dim Message As String = String.Format("There was an error.\nHRESULT = {1}\n{2}", hr.ToString(), comExc.Message)
            MessageBox.Show(Message, "COM Exception")
        End Try
    End Sub


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

            ' Update the trackbar.
            Dim curPos As Integer = Convert.ToInt32(WMPlayer.Ctlcontrols.currentPosition * 1000) ' milliseconds
            Dim duration As Integer = Convert.ToInt32(WMPlayer.currentMedia.duration * 1000) ' milliseconds
            If duration > 0 Then
                tb_SeekBar.Value = Convert.ToInt32((curPos * 100) / duration) ' % complete
            End If

            ' Update the time label
            lblCurrentDuration.Text = WMPlayer.Ctlcontrols.currentPositionString

        Catch comExc As COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, comExc.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Dim hr As Integer = comExc.ErrorCode
            'Dim Message As String = String.Format("There was an error.\nHRESULT = {1}\n{2}", hr.ToString(), comExc.Message)
            'MessageBox.Show(Message, "COM Exception")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, "Exception")
        End Try
    End Sub

    Private Sub btnStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.Click
        WMPlayer.Ctlcontrols.stop()
    End Sub

    Private Sub c1CategorisedDocuments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1CategorisedDocuments.Click

        'If c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_VideoName) = "Video Name" Or c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_Flag) = "" Then
        '    Exit Sub
        'End If
        Try
            If (c1CategorisedDocuments.Row > 0) Then
                If IsNothing(c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge)) Then
                    ts_btnAcknowlegment.Visible = False
                    ts_btnViewAcknowlegment.Visible = False
                ElseIf c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge) = 0 Then
                    ts_btnAcknowlegment.Visible = True
                    ts_btnViewAcknowlegment.Visible = False
                Else
                    ts_btnViewAcknowlegment.Visible = True
                    ts_btnAcknowlegment.Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    
    Private Sub c1CategorisedDocuments_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1CategorisedDocuments.DoubleClick
        

        Try
            If c1CategorisedDocuments.Rows.Count = 1 Then
                Exit Sub
            End If

            If c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_Path) = "" Then
                Exit Sub
            End If

            Call Playfile()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Public Sub Playfile()

    '    If IsNothing(c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge)) Then
    '        ts_btnAcknowlegment.Visible = False
    '        ts_btnViewAcknowlegment.Visible = False
    '    ElseIf c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge) = 0 Then
    '        ts_btnAcknowlegment.Visible = True
    '        ts_btnViewAcknowlegment.Visible = False
    '    Else
    '        ts_btnViewAcknowlegment.Visible = True
    '        ts_btnAcknowlegment.Visible = False
    '    End If

    '    Dim Path As String = c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_Path)
    '    Dim strFileName As String = c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_VideoName)
    '    Dim mymedia As WMPLib.IWMPMedia
    '    mymedia = WMPlayer.newMedia(Trim(Path))
    '    'WMPlayer.currentMedia = mymedia
    '    'WMPlayer.Ctlcontrols.play()
    '    If IsNothing(mymedia) = False Then
    '        tb_SeekBar.Enabled = True
    '        lblMediaName.Text = strFileName '' mymedia.name
    '        lblDuration.Text = mymedia.durationString '& " -- " & myMedia.getMarkerTime(myMedia.markerCount)
    '        WMPlayer.currentMedia = mymedia
    '        Timer1.Start()
    '    End If
    'End Sub

    Public Sub Playfile()

        If IsNothing(c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge)) Then
            ts_btnAcknowlegment.Visible = False
            ts_btnViewAcknowlegment.Visible = False
        ElseIf c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_IsAcknowledge) = 0 Then
            ts_btnAcknowlegment.Visible = True
            ts_btnViewAcknowlegment.Visible = False
        Else
            ts_btnViewAcknowlegment.Visible = True
            ts_btnAcknowlegment.Visible = False
        End If

        Dim Path As String = c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_Path)
        Dim strFileName As String = c1CategorisedDocuments.GetData(c1CategorisedDocuments.Row, COL_VideoName)
        'Dim mymedia As WMPLib.IWMPMedia
        'mymedia = WMPlayer.newMedia(Trim(Path))
        'WMPlayer.currentMedia = mymedia
        'WMPlayer.Ctlcontrols.play()
        'If IsNothing(mymedia) = False Then
        '    tb_SeekBar.Enabled = True
        '    lblMediaName.Text = strFileName '' mymedia.name
        '    lblDuration.Text = mymedia.durationString '& " -- " & myMedia.getMarkerTime(myMedia.markerCount)
        '    WMPlayer.currentMedia = mymedia
        '    Timer1.Start()
        'End If

        'AxWMPLib.AxWindowsMediaPlayer axWmp =winFormsHost.Child as AxWMPLib.AxWindowsMediaPlayer;
        'activeXMediaPlayer.URL = Path


        'Dim axWmp As AxWMPLib.AxWindowsMediaPlayer
        WMPlayer.URL = Path

    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click

    End Sub

    Private Sub btnAcknowlegment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnAcknowlegment.Click
        If File.Exists(Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_Path))) = True Then
            Dim oAcknoledgement As New frmVMS_Acknoledgement
            oAcknoledgement._ViewedDocumentDispName = Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_VideoName))
            oAcknoledgement._ViewedDocumentPath = Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_Path))

            If oAcknoledgement.ShowDialog(IIf(IsNothing(oAcknoledgement.Parent), Me, oAcknoledgement.Parent)) = Windows.Forms.DialogResult.OK Then

                ts_btnAcknowlegment.Visible = False

                ts_btnViewAcknowlegment.Visible = True
                SetPlayList()
            End If
            oAcknoledgement.Dispose()
            oAcknoledgement = Nothing

        End If
    End Sub

    Private Sub btnViewAcknowlegment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnViewAcknowlegment.Click
        Try
            If c1CategorisedDocuments.Rows.Count >= 0 Then
                If c1CategorisedDocuments.RowSel >= 0 Then
                    If File.Exists(Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_Path))) = True Then
                        Dim oAcknoledgement As New frmVMS_ViewAcknoledgement
                        oAcknoledgement._ViewedDocumentDispName = Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_VideoName))
                        oAcknoledgement._ViewedDocumentPath = Trim(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_Path))
                        oAcknoledgement.ShowDialog(IIf(IsNothing(oAcknoledgement.Parent), Me, oAcknoledgement.Parent))
                        oAcknoledgement.Dispose()
                        oAcknoledgement = Nothing
                    End If
                End If
            End If
        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.UploadVideo, gloAuditTrail.ActivityType.View, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    'Private Sub btnPatientVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'Dim oPatientVideo As New frmVMS_PatientVideo
    '    'oPatientVideo.Show()
    '    Dim oVideo As New frmVMS_PatientVideo
    '    With oVideo
    '        'Me.pnlLeft.Visible = False
    '        'Me.Splitter1.Visible = False
    '        .MdiParent = CType(Me.ParentForm, MainMenu)
    '        .ShowInTaskbar = False
    '        .WindowState = FormWindowState.Maximized
    '        .Show()
    '    End With
    'End Sub

    Private Sub btnPlayPause_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlayPause.MouseHover
        If WMPlayer.playState = WMPLib.WMPPlayState.wmppsPlaying Then
            tooltpPause.SetToolTip(btnPlayPause, "Pause")
        ElseIf WMPlayer.playState = WMPLib.WMPPlayState.wmppsPaused Then
            tooltpPause.SetToolTip(btnPlayPause, "Play")
        Else
            tooltpPause.SetToolTip(btnPlayPause, "Play")
        End If

    End Sub

    Private Sub btnPlayPause_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlayPause.MouseLeave
        tooltpPause.SetToolTip(btnPlayPause, "")
    End Sub

    Private Sub btnStop_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.MouseHover
        tooltpPause.SetToolTip(btnStop, "Stop")
    End Sub

    Private Sub btnStop_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.MouseLeave
        tooltpPause.SetToolTip(btnStop, "")
    End Sub

   
    Private Sub c1CategorisedDocuments_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1CategorisedDocuments.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID   'Curent patient variable(Local variable) for this module 
        End Get
    End Property

End Class