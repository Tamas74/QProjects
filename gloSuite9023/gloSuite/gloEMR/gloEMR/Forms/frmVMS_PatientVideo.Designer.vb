<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVMS_PatientVideo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
            Try
                If (IsNothing(OpenFileDialogMedia) = False) Then
                    OpenFileDialogMedia.Dispose()
                    OpenFileDialogMedia = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVMS_PatientVideo))
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_Main = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblShow = New System.Windows.Forms.ToolStripButton
        Me.tblSave = New System.Windows.Forms.ToolStripButton
        Me.tblClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.WMPPatientVideo = New AxWMPLib.AxWindowsMediaPlayer
        Me.pnlMain_BOTTOM = New System.Windows.Forms.Panel
        Me.pnlNote = New System.Windows.Forms.Panel
        Me.txttitle = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlUsers = New System.Windows.Forms.Panel
        Me.cmbUser = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pic_Play = New System.Windows.Forms.PictureBox
        Me.pic_Pause = New System.Windows.Forms.PictureBox
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnEndTime = New System.Windows.Forms.Button
        Me.btnStartTime = New System.Windows.Forms.Button
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnPause = New System.Windows.Forms.Button
        Me.btnPlay = New System.Windows.Forms.Button
        Me.pnlCommentUsers = New System.Windows.Forms.Panel
        Me.pnlComments = New System.Windows.Forms.Panel
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.c1VideoList = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.OpenFileDialogMedia = New System.Windows.Forms.OpenFileDialog
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_Main.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.WMPPatientVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain_BOTTOM.SuspendLayout()
        Me.pnlNote.SuspendLayout()
        Me.pnlUsers.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pic_Play, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Pause, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlCommentUsers.SuspendLayout()
        Me.pnlComments.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1VideoList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_Main)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 56)
        Me.pnlToolStrip.TabIndex = 18
        '
        'ts_Main
        '
        Me.ts_Main.BackColor = System.Drawing.Color.Transparent
        Me.ts_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_Main.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_Main.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblShow, Me.tblSave, Me.tblClose})
        Me.ts_Main.Location = New System.Drawing.Point(0, 0)
        Me.ts_Main.Name = "ts_Main"
        Me.ts_Main.Size = New System.Drawing.Size(1028, 53)
        Me.ts_Main.TabIndex = 1
        Me.ts_Main.Text = "ToolStrip1"
        '
        'tblShow
        '
        Me.tblShow.BackColor = System.Drawing.Color.Transparent
        Me.tblShow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblShow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblShow.Image = CType(resources.GetObject("tblShow.Image"), System.Drawing.Image)
        Me.tblShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblShow.Name = "tblShow"
        Me.tblShow.Size = New System.Drawing.Size(58, 50)
        Me.tblShow.Tag = "ShowHide"
        Me.tblShow.Text = " &Show  "
        Me.tblShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblShow.ToolTipText = "Show  "
        Me.tblShow.Visible = False
        '
        'tblSave
        '
        Me.tblSave.BackColor = System.Drawing.Color.Transparent
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close "
        '
        'tblClose
        '
        Me.tblClose.BackColor = System.Drawing.Color.Transparent
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.pnlMain_BOTTOM)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1028, 670)
        Me.pnlMain.TabIndex = 19
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.WMPPatientVideo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 1, 0)
        Me.Panel2.Size = New System.Drawing.Size(798, 448)
        Me.Panel2.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 447)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(792, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 444)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(796, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 444)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(794, 1)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "label1"
        '
        'WMPPatientVideo
        '
        Me.WMPPatientVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WMPPatientVideo.Enabled = True
        Me.WMPPatientVideo.Location = New System.Drawing.Point(3, 3)
        Me.WMPPatientVideo.Name = "WMPPatientVideo"
        Me.WMPPatientVideo.OcxState = CType(resources.GetObject("WMPPatientVideo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.WMPPatientVideo.Size = New System.Drawing.Size(794, 445)
        Me.WMPPatientVideo.TabIndex = 0
        '
        'pnlMain_BOTTOM
        '
        Me.pnlMain_BOTTOM.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain_BOTTOM.Controls.Add(Me.pnlNote)
        Me.pnlMain_BOTTOM.Controls.Add(Me.pnlUsers)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Panel3)
        Me.pnlMain_BOTTOM.Controls.Add(Me.pnlCommentUsers)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Label8)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Panel4)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Label11)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Label9)
        Me.pnlMain_BOTTOM.Controls.Add(Me.Label10)
        Me.pnlMain_BOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMain_BOTTOM.Location = New System.Drawing.Point(0, 448)
        Me.pnlMain_BOTTOM.Name = "pnlMain_BOTTOM"
        Me.pnlMain_BOTTOM.Padding = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.pnlMain_BOTTOM.Size = New System.Drawing.Size(798, 222)
        Me.pnlMain_BOTTOM.TabIndex = 16
        '
        'pnlNote
        '
        Me.pnlNote.BackColor = System.Drawing.Color.Transparent
        Me.pnlNote.Controls.Add(Me.txttitle)
        Me.pnlNote.Controls.Add(Me.Label1)
        Me.pnlNote.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlNote.Location = New System.Drawing.Point(8, 99)
        Me.pnlNote.Name = "pnlNote"
        Me.pnlNote.Size = New System.Drawing.Size(556, 24)
        Me.pnlNote.TabIndex = 3
        '
        'txttitle
        '
        Me.txttitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txttitle.Enabled = False
        Me.txttitle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttitle.ForeColor = System.Drawing.Color.Black
        Me.txttitle.Location = New System.Drawing.Point(103, 0)
        Me.txttitle.MaxLength = 255
        Me.txttitle.Name = "txttitle"
        Me.txttitle.Size = New System.Drawing.Size(453, 22)
        Me.txttitle.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 24)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Title :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUsers
        '
        Me.pnlUsers.BackColor = System.Drawing.Color.Transparent
        Me.pnlUsers.Controls.Add(Me.cmbUser)
        Me.pnlUsers.Controls.Add(Me.Label4)
        Me.pnlUsers.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlUsers.Location = New System.Drawing.Point(8, 128)
        Me.pnlUsers.Name = "pnlUsers"
        Me.pnlUsers.Size = New System.Drawing.Size(481, 26)
        Me.pnlUsers.TabIndex = 5
        '
        'cmbUser
        '
        Me.cmbUser.Dock = System.Windows.Forms.DockStyle.Top
        Me.cmbUser.Enabled = False
        Me.cmbUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUser.ForeColor = System.Drawing.Color.Black
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(103, 0)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(378, 22)
        Me.cmbUser.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 26)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "User :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.pic_Play)
        Me.Panel3.Controls.Add(Me.pic_Pause)
        Me.Panel3.Controls.Add(Me.btnStop)
        Me.Panel3.Controls.Add(Me.btnEndTime)
        Me.Panel3.Controls.Add(Me.btnStartTime)
        Me.Panel3.Controls.Add(Me.btnBrowse)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(4, 50)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(792, 45)
        Me.Panel3.TabIndex = 2
        '
        'pic_Play
        '
        Me.pic_Play.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pic_Play.BackgroundImage = CType(resources.GetObject("pic_Play.BackgroundImage"), System.Drawing.Image)
        Me.pic_Play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pic_Play.Image = CType(resources.GetObject("pic_Play.Image"), System.Drawing.Image)
        Me.pic_Play.Location = New System.Drawing.Point(259, 10)
        Me.pic_Play.Name = "pic_Play"
        Me.pic_Play.Size = New System.Drawing.Size(21, 21)
        Me.pic_Play.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pic_Play.TabIndex = 28
        Me.pic_Play.TabStop = False
        Me.pic_Play.Visible = False
        '
        'pic_Pause
        '
        Me.pic_Pause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pic_Pause.BackgroundImage = CType(resources.GetObject("pic_Pause.BackgroundImage"), System.Drawing.Image)
        Me.pic_Pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pic_Pause.Image = CType(resources.GetObject("pic_Pause.Image"), System.Drawing.Image)
        Me.pic_Pause.Location = New System.Drawing.Point(286, 10)
        Me.pic_Pause.Name = "pic_Pause"
        Me.pic_Pause.Size = New System.Drawing.Size(21, 21)
        Me.pic_Pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pic_Pause.TabIndex = 27
        Me.pic_Pause.TabStop = False
        Me.pic_Pause.Visible = False
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStop.BackgroundImage = CType(resources.GetObject("btnStop.BackgroundImage"), System.Drawing.Image)
        Me.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.Image = CType(resources.GetObject("btnStop.Image"), System.Drawing.Image)
        Me.btnStop.Location = New System.Drawing.Point(78, 10)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(21, 21)
        Me.btnStop.TabIndex = 2
        Me.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnEndTime
        '
        Me.btnEndTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEndTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEndTime.Enabled = False
        Me.btnEndTime.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnEndTime.FlatAppearance.BorderSize = 0
        Me.btnEndTime.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnEndTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnEndTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEndTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEndTime.Image = CType(resources.GetObject("btnEndTime.Image"), System.Drawing.Image)
        Me.btnEndTime.Location = New System.Drawing.Point(140, 8)
        Me.btnEndTime.Name = "btnEndTime"
        Me.btnEndTime.Size = New System.Drawing.Size(21, 21)
        Me.btnEndTime.TabIndex = 4
        Me.btnEndTime.UseVisualStyleBackColor = True
        '
        'btnStartTime
        '
        Me.btnStartTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStartTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStartTime.Enabled = False
        Me.btnStartTime.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnStartTime.FlatAppearance.BorderSize = 0
        Me.btnStartTime.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnStartTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnStartTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartTime.Image = CType(resources.GetObject("btnStartTime.Image"), System.Drawing.Image)
        Me.btnStartTime.Location = New System.Drawing.Point(112, 8)
        Me.btnStartTime.Name = "btnStartTime"
        Me.btnStartTime.Size = New System.Drawing.Size(21, 21)
        Me.btnStartTime.TabIndex = 3
        Me.btnStartTime.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrowse.BackgroundImage = CType(resources.GetObject("btnBrowse.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(28, 10)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnPause)
        Me.Panel5.Controls.Add(Me.btnPlay)
        Me.Panel5.Location = New System.Drawing.Point(53, 10)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(46, 21)
        Me.Panel5.TabIndex = 30
        '
        'btnPause
        '
        Me.btnPause.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPause.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPause.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPause.Image = CType(resources.GetObject("btnPause.Image"), System.Drawing.Image)
        Me.btnPause.Location = New System.Drawing.Point(21, 0)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(21, 21)
        Me.btnPause.TabIndex = 29
        Me.btnPause.Tag = "Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        Me.btnPause.Visible = False
        '
        'btnPlay
        '
        Me.btnPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnPlay.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPlay.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlay.Image = CType(resources.GetObject("btnPlay.Image"), System.Drawing.Image)
        Me.btnPlay.Location = New System.Drawing.Point(0, 0)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(21, 21)
        Me.btnPlay.TabIndex = 1
        Me.btnPlay.Tag = "Pause"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'pnlCommentUsers
        '
        Me.pnlCommentUsers.Controls.Add(Me.pnlComments)
        Me.pnlCommentUsers.Location = New System.Drawing.Point(9, 156)
        Me.pnlCommentUsers.Name = "pnlCommentUsers"
        Me.pnlCommentUsers.Size = New System.Drawing.Size(559, 57)
        Me.pnlCommentUsers.TabIndex = 1
        '
        'pnlComments
        '
        Me.pnlComments.BackColor = System.Drawing.Color.Transparent
        Me.pnlComments.Controls.Add(Me.txtComments)
        Me.pnlComments.Controls.Add(Me.Label2)
        Me.pnlComments.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlComments.Location = New System.Drawing.Point(0, 0)
        Me.pnlComments.Name = "pnlComments"
        Me.pnlComments.Size = New System.Drawing.Size(556, 57)
        Me.pnlComments.TabIndex = 4
        '
        'txtComments
        '
        Me.txtComments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtComments.Enabled = False
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(103, 0)
        Me.txtComments.MaxLength = 255
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(453, 57)
        Me.txtComments.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 57)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Comments :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 218)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(792, 1)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label2"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.ToolStrip1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(792, 46)
        Me.Panel4.TabIndex = 6
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ToolStrip1.BackgroundImage = CType(resources.GetObject("ToolStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(792, 45)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(40, 42)
        Me.btnOK.Text = "&Save"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 42)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(792, 1)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 216)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(796, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 216)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label3"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Splitter2.Location = New System.Drawing.Point(798, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 670)
        Me.Splitter2.TabIndex = 14
        Me.Splitter2.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.c1VideoList)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(801, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(227, 670)
        Me.Panel1.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(2, 666)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(221, 1)
        Me.lbl_BottomBrd.TabIndex = 30
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(1, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 663)
        Me.lbl_LeftBrd.TabIndex = 29
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(223, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 663)
        Me.lbl_RightBrd.TabIndex = 28
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(1, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(223, 1)
        Me.lbl_TopBrd.TabIndex = 27
        Me.lbl_TopBrd.Text = "label1"
        '
        'c1VideoList
        '
        Me.c1VideoList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1VideoList.BackColor = System.Drawing.Color.White
        Me.c1VideoList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1VideoList.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1VideoList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1VideoList.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1VideoList.Location = New System.Drawing.Point(1, 3)
        Me.c1VideoList.Name = "c1VideoList"
        Me.c1VideoList.Rows.Count = 0
        Me.c1VideoList.Rows.DefaultSize = 19
        Me.c1VideoList.Rows.Fixed = 0
        Me.c1VideoList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1VideoList.ShowCellLabels = True
        Me.c1VideoList.Size = New System.Drawing.Size(223, 664)
        Me.c1VideoList.StyleInfo = resources.GetString("c1VideoList.StyleInfo")
        Me.c1VideoList.TabIndex = 26
        Me.c1VideoList.Tree.Column = 2
        Me.c1VideoList.Tree.Indent = 72
        Me.c1VideoList.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.c1VideoList.Tree.NodeImageCollapsed = CType(resources.GetObject("c1VideoList.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1VideoList.Tree.NodeImageExpanded = CType(resources.GetObject("c1VideoList.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.c1VideoList.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None
        '
        'OpenFileDialogMedia
        '
        Me.OpenFileDialogMedia.FileName = "OpenFileDialog1"
        Me.OpenFileDialogMedia.SupportMultiDottedExtensions = True
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmVMS_PatientVideo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 726)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVMS_PatientVideo"
        Me.Text = "Patient Video"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_Main.ResumeLayout(False)
        Me.ts_Main.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.WMPPatientVideo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain_BOTTOM.ResumeLayout(False)
        Me.pnlNote.ResumeLayout(False)
        Me.pnlNote.PerformLayout()
        Me.pnlUsers.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.pic_Play, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Pause, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlCommentUsers.ResumeLayout(False)
        Me.pnlComments.ResumeLayout(False)
        Me.pnlComments.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1VideoList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_Main As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents WMPPatientVideo As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents OpenFileDialogMedia As System.Windows.Forms.OpenFileDialog
    Friend WithEvents c1VideoList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlMain_BOTTOM As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents pnlUsers As System.Windows.Forms.Panel
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlCommentUsers As System.Windows.Forms.Panel
    Friend WithEvents pnlComments As System.Windows.Forms.Panel
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlNote As System.Windows.Forms.Panel
    Friend WithEvents txttitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnEndTime As System.Windows.Forms.Button
    Friend WithEvents btnStartTime As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents pic_Play As System.Windows.Forms.PictureBox
    Friend WithEvents pic_Pause As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
End Class
