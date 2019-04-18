Imports gloEMR.gloStream.gloVMS
Public Class frmVMS_PatientVideo
    Implements IPatientContext


#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(PatientID)
            .SendToBack()
            '.DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
            .DTPEnabled = False
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        ''''
        pnlToolStrip.SendToBack()
        pnlMain.BringToFront()

        ' ''
    End Sub

#End Region

    ''''Grid Column
    Private COL_VideoFileName As Integer = 0
    Private COL_Path As Integer = 1
    Private COL_StartTime As Integer = 2
    Private COL_EndTime As Integer = 3
    Private COL_Comments As Integer = 4
    Private COL_Title As Integer = 5
    Private COL_Reason As Integer = 6
    Private COL_UserID As Integer = 7

    Private COL_COUNT As Integer = 8

    Dim Path As String = ""
    Dim startTime As String
    Dim EndTime As String
    Dim fSourceFile As System.IO.FileInfo
    Dim mediaFileDuration As String = ""
    Dim oDetail As New gloStream.gloVMS.VMSTranscation.TransactionDetail
    Dim PatientID As Long
    Dim m_visitID As Long

    Private Sub frmVMS_PatientVideo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub frmVMS_PatientVideo_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(gloUC_PatientStrip1) = False) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmVMS_PatientVideo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnPlay.Visible = True
        btnPause.Visible = False

        'btnPlay.BackgroundImageLayout = ImageLayout.Center


        gloC1FlexStyle.Style(c1VideoList)

        ''''''Set Patient Strip 
        Set_PatientDetailStrip()
        SetGridStyle()
        ''''Fill Users Name in combobox START
        cmbUser.Items.Clear()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        oDB.Connect(GetConnectionString)
        oDataReader = oDB.ReadQueryRecords("SELECT nUserID,sLoginName FROM User_MST WHERE sLoginName IS NOT NULL ORDER BY sLoginName")
        If oDataReader.HasRows = True Then
            While oDataReader.Read
                cmbUser.Items.Add(oDataReader.Item("sLoginName"))
            End While
        End If
        oDB.Disconnect()

        If cmbUser.Items.Count > 0 Then
            For i As Integer = 0 To cmbUser.Items.Count - 1
                If UCase(cmbUser.Items.Item(i)) = gstrLoginName.ToUpper Then
                    cmbUser.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        btnPlay.Enabled = False
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        ''''Fill Users Name in combobox END
    End Sub

    Public Sub SetGridStyle()
        ''''Set flexgrid style
        With c1VideoList
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0
            .Cols.Count = COL_COUNT

            ''''style for video filename column
            .Cols(COL_VideoFileName).Width = 200
            .SetData(0, COL_VideoFileName, "Video File Name")
            .Cols(COL_VideoFileName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for video Path column
            .Cols(COL_Path).Width = 0
            .SetData(0, COL_Path, "Path")
            .Cols(COL_VideoFileName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for start Time column
            .Cols(COL_StartTime).Width = 0
            .SetData(0, COL_StartTime, "Start Time")
            .Cols(COL_StartTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for End time Column
            .Cols(COL_EndTime).Width = 0
            .SetData(0, COL_EndTime, "End Time")
            .Cols(COL_EndTime).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for Comments column
            .Cols(COL_Comments).Width = 0
            .SetData(0, COL_Comments, "Comments")
            .Cols(COL_Comments).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for Notes column(Title)
            .Cols(COL_Title).Width = 0
            .SetData(0, COL_Title, "Notes")
            .Cols(COL_Title).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for Resone column (use it for further reference)
            .Cols(COL_Reason).Width = 0
            .SetData(0, COL_Reason, "Resone")
            .Cols(COL_Reason).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''''style for UserID column 
            .Cols(COL_UserID).Width = 0
            .SetData(0, COL_UserID, "UserID")
            .Cols(COL_UserID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

        End With

    End Sub

    Private Sub ts_Main_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_Main.ItemClicked
        ''''Toolstrip button click 
        Select Case e.ClickedItem.Tag
            ''''save
            Case tblSave.Tag
                SaveRecord()
                ''''close
            Case tblClose.Tag
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    Public Sub SaveRecord()
        Try
            ''''If there is no data in flexgrid
            If c1VideoList.Rows.Count <= 1 Then
                Exit Sub
            End If

            Dim osaveTrans As New gloStream.gloVMS.VMSTranscation.Transaction_Master
            ''''If visitID is ZERO then create visitID
            ''If gnVisitID = 0 Then '' condition commented by Sandip Darade for the the flow to not to use incorrect visit id 

            m_visitID = GenerateVisitID(Now, PatientID)
            ''End If

            ''''save data in Transaction Master Table if save completed then move further
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'If osaveTrans.SaveTransaction_MSTRecord(gnClientMachineID, gnVisitID, gnPatientID, Now) Then
            If osaveTrans.SaveTransaction_MSTRecord(gnClientMachineID, m_visitID, PatientID, Now) Then 'gnvisitID id replaced by m_visitID
                ''''get Newly created Transcation ID
                'Dim nTranctionID As Int64 = osaveTrans.GetTransctionID(gnVisitID, gnPatientID)
                Dim nTranctionID As Int64 = osaveTrans.GetTransctionID(m_visitID, PatientID) 'gnvisitID id replaced by m_visitID
                'end modification by dipak
                'end modification
                ''''Save the flexgrid content in Table
                With c1VideoList

                    For i As Integer = 1 To .Rows.Count - 1

                        osaveTrans.InsertVMSDetails(nTranctionID, .GetData(i, COL_StartTime), .GetData(i, COL_EndTime), .GetData(i, COL_VideoFileName), Now, .GetData(i, COL_Title), .GetData(i, COL_Reason), .GetData(i, COL_Comments), .GetData(i, COL_UserID))
                    Next
                End With
                '     Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("Error while Saving Video File(s) -- " & ex.ToString)
            'MessageBox.Show("Error while Saving Video File(s)", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try

            OpenFileDialogMedia.FileName = "gloVideo"
            ''''Open File Dialog Box.
            If OpenFileDialogMedia.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                If OpenFileDialogMedia.FileName.Length > 0 Then
                    fSourceFile = New System.IO.FileInfo(OpenFileDialogMedia.FileName)
                    Path = fSourceFile.FullName
                    Dim mymedia As WMPLib.IWMPMedia
                    mymedia = WMPPatientVideo.newMedia(Trim(Path))
                    If IsNothing(mymedia) = False Then
                        'lblMediaName.Text = strFileName '' mymedia.name
                        mediaFileDuration = mymedia.durationString '& " -- " & myMedia.getMarkerTime(myMedia.markerCount)
                        WMPPatientVideo.currentMedia = mymedia

                        btnStartTime.Enabled = True
                        btnEndTime.Enabled = False
                        btnBrowse.Enabled = False
                        Timer1.Start()
                        oDetail.VideoName = fSourceFile.Name
                        btnPlay.Enabled = True
                        WMPPatientVideo.Ctlcontrols.stop()
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("Error while Playing Video File -- " & ex.ToString)
            MessageBox.Show("Error while Playing Video File", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If oDetail.VideoName = "" Then
                Exit Sub
            End If

            Dim oTrans As New gloStream.gloVMS.VMSTranscation.Transaction_Master
            oDetail.Comments = txtComments.Text
            'oDetail.Note = txttitle.Text
            oDetail.Title = txttitle.Text

            If oDetail.Title = "" Then
                MessageBox.Show("Please Enter Title", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            ''''Get UserID form UserName
            oDetail.UserID = oTrans.GetUserID(cmbUser.SelectedItem.ToString)

            ''''Set Data to flexgird 
            With c1VideoList
                .Rows.Add()
                .SetData(.Rows.Count - 1, COL_VideoFileName, oDetail.VideoName)
                .SetData(.Rows.Count - 1, COL_Path, fSourceFile.FullName)
                .SetData(.Rows.Count - 1, COL_StartTime, oDetail.StartTime)
                .SetData(.Rows.Count - 1, COL_EndTime, oDetail.EndTime)
                .SetData(.Rows.Count - 1, COL_Comments, oDetail.Comments)
                .SetData(.Rows.Count - 1, COL_Title, oDetail.Title)
                .SetData(.Rows.Count - 1, COL_Reason, "")
                .SetData(.Rows.Count - 1, COL_UserID, oDetail.UserID)
            End With
            ''''Enable the browse Button.
            btnBrowse.Enabled = True
            btnStartTime.Enabled = False
            btnEndTime.Enabled = False
            txtComments.Text = ""
            txttitle.Text = ""

            btnOK.Enabled = False
            txtComments.Enabled = False
            txttitle.Enabled = False
            cmbUser.Enabled = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("Error while Adding Video File -- " & ex.ToString)
            MessageBox.Show("Error while Adding Video File", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        WMPPatientVideo.Ctlcontrols.stop()

        WMPPatientVideo.close()

        btnBrowse.Enabled = True
        btnStartTime.Enabled = False
        btnEndTime.Enabled = False

        txtComments.Text = ""
        txttitle.Text = ""
        txtComments.Enabled = False
        txttitle.Enabled = False
        oDetail.Comments = ""
        oDetail.dos = Now
        oDetail.EndTime = ""
        oDetail.StartTime = ""
        oDetail.Title = ""
        oDetail.UserID = 0
        oDetail.VideoName = ""

    End Sub



    Private Sub btnStartTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStartTime.Click
        ''''Get the start Time of the video clip
        If WMPPatientVideo.Ctlcontrols.currentPositionString <> "" Then
            startTime = WMPPatientVideo.Ctlcontrols.currentPositionString
            oDetail.StartTime = startTime
            btnBrowse.Enabled = False
            btnStartTime.Enabled = False
            btnEndTime.Enabled = True
        End If
    End Sub

    Private Sub btnEndTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEndTime.Click
        ''''Get The End time of Video clip
        If WMPPatientVideo.Ctlcontrols.currentPositionString = "" Then
            ''''If EndTime button click event is not fire then End time is the total duration of the file
            EndTime = mediaFileDuration
            oDetail.EndTime = EndTime

            btnEndTime.Enabled = False
            txtComments.Enabled = True
            txttitle.Enabled = True

            cmbUser.Enabled = True
            btnOK.Enabled = True
        Else
            ''''If user click on end time button
            EndTime = WMPPatientVideo.Ctlcontrols.currentPositionString
            WMPPatientVideo.Ctlcontrols.stop()
            oDetail.EndTime = EndTime

            btnOK.Enabled = True
            btnEndTime.Enabled = False
            txtComments.Enabled = True
            txttitle.Enabled = True

            cmbUser.Enabled = True

        End If
    End Sub

    Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlay.Click

        Try
            btnStartTime.Enabled = True
            If WMPPatientVideo.playState = WMPLib.WMPPlayState.wmppsPlaying Then
                WMPPatientVideo.Ctlcontrols.pause()
                btnPlay.Visible = True
                btnPause.Visible = False
                'btnPlay.BackgroundImage = pic_Pause.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
            Else
                WMPPatientVideo.Ctlcontrols.play()
                btnPlay.Visible = False
                btnPause.Visible = True
                'btnPlay.BackgroundImage = pic_Play.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.Click
        btnStartTime.Enabled = False
        WMPPatientVideo.Ctlcontrols.stop()
    End Sub

    Private Sub WMPPatientVideo_PlayStateChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles WMPPatientVideo.PlayStateChange
        Try
            SetUIState(e.newState)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Exception")
        End Try
    End Sub

    Private Sub SetUIState(ByVal newState As WMPLib.WMPPlayState)
        Select Case newState
            Case WMPLib.WMPPlayState.wmppsPlaying
                'btnPlay.BackgroundImage = pic_Pause.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
                btnPlay.Visible = False
                btnPause.Visible = True
                Timer1.Start()
            Case WMPLib.WMPPlayState.wmppsPaused
                'btnPlay.BackgroundImage = pic_Play.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
                btnPlay.Visible = True
                btnPause.Visible = False
                Timer1.Stop()
            Case WMPLib.WMPPlayState.wmppsStopped
                'btnPlay.BackgroundImage = pic_Play.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
                btnPlay.Visible = True
                btnPause.Visible = False
                Timer1.Start()
            Case Else
                'btnPlay.BackgroundImage = pic_Pause.Image
                'btnPlay.BackgroundImageLayout = ImageLayout.Center
                btnPlay.Visible = True
                btnPause.Visible = False
                Timer1.Start()
        End Select
    End Sub


    Private Sub btnBrowse_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseEnter
        ToolTip1.SetToolTip(btnBrowse, "Browse File")
    End Sub

    Private Sub btnStartTime_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStartTime.MouseEnter
        ToolTip1.SetToolTip(btnStartTime, "Start Time")
    End Sub

    Private Sub btnEndTime_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEndTime.MouseEnter
        ToolTip1.SetToolTip(btnEndTime, "End Time")
    End Sub

    Private Sub btnStop_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.MouseEnter
        ToolTip1.SetToolTip(btnStop, "Stop")
    End Sub

    Private Sub btnPlayPause_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlay.MouseEnter
        Select Case WMPPatientVideo.playState
            Case WMPLib.WMPPlayState.wmppsPlaying
                ToolTip1.SetToolTip(btnPlay, "Pause")
            Case WMPLib.WMPPlayState.wmppsPaused
                ToolTip1.SetToolTip(btnPlay, "Play")
            Case WMPLib.WMPPlayState.wmppsStopped
                ToolTip1.SetToolTip(btnPlay, "Play")
            Case Else
                ToolTip1.SetToolTip(btnPlay, "Play")
        End Select
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseHover, btnStop.MouseHover, btnPause.MouseHover, btnPlay.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseLeave, btnStop.MouseLeave, btnPause.MouseLeave, btnPlay.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub c1VideoList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1VideoList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnPause_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPause.Click
        Try
            'WMPPatientVideo.Ctlcontrols.play()
            'btnPlay.Visible = True
            'btnPause.Visible = False
            Try
                btnStartTime.Enabled = True
                If WMPPatientVideo.playState = WMPLib.WMPPlayState.wmppsPlaying Then
                    WMPPatientVideo.Ctlcontrols.pause()
                    btnPlay.Visible = True
                    btnPause.Visible = False
                    'btnPlay.BackgroundImage = pic_Pause.Image
                    'btnPlay.BackgroundImageLayout = ImageLayout.Center
                Else
                    WMPPatientVideo.Ctlcontrols.play()
                    btnPlay.Visible = False
                    btnPause.Visible = True
                    'btnPlay.BackgroundImage = pic_Play.Image
                    'btnPlay.BackgroundImageLayout = ImageLayout.Center
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message)
            End Try
        Catch ex As Exception

        End Try

    End Sub

    Public Sub New(ByVal _PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        PatientID = _PatientID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return PatientID 'Curent patient variable(Local variable) for this module 
        End Get
    End Property


End Class