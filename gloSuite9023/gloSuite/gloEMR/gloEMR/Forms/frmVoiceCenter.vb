Imports AxDNSTools.AxDgnEngineControl
Imports DNSTools
Imports System.IO
Public Class frmVoiceCenter
    Inherits System.Windows.Forms.Form
    Private WithEvents AxDgnEngineControl2 As AxDNSTools.AxDgnEngineControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_VoceCenter As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Public clSpeakers As New Collection
#Region " Windows Form Designer generated code "

    Public Sub New(ByRef objengine As AxDNSTools.AxDgnEngineControl)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        AxDgnEngineControl2 = objengine
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents imglstTree As System.Windows.Forms.ImageList
    Friend WithEvents trvCommands As System.Windows.Forms.TreeView
    Friend WithEvents pnlFillSpeakerInfo As System.Windows.Forms.Panel
    Friend WithEvents grpSpeakerConfiguration As System.Windows.Forms.GroupBox
    Friend WithEvents txtSpeakerDirectory As System.Windows.Forms.TextBox
    Friend WithEvents txtCurrentTopic As System.Windows.Forms.TextBox
    Friend WithEvents txtSourceTrained As System.Windows.Forms.TextBox
    Friend WithEvents txtDictationSource As System.Windows.Forms.TextBox
    Friend WithEvents txtSpeechModel As System.Windows.Forms.TextBox
    Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpUserSetup As System.Windows.Forms.GroupBox
    Friend WithEvents txtTrainingTime As System.Windows.Forms.TextBox
    Friend WithEvents txtCalibrated As System.Windows.Forms.TextBox
    Friend WithEvents txtAudioSetup As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVoiceCenter))
        Me.imglstTree = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.pnlLeftMain = New System.Windows.Forms.Panel
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.trvCommands = New System.Windows.Forms.TreeView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlFillSpeakerInfo = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.grpSpeakerConfiguration = New System.Windows.Forms.GroupBox
        Me.txtSpeakerDirectory = New System.Windows.Forms.TextBox
        Me.txtCurrentTopic = New System.Windows.Forms.TextBox
        Me.txtSourceTrained = New System.Windows.Forms.TextBox
        Me.txtDictationSource = New System.Windows.Forms.TextBox
        Me.txtSpeechModel = New System.Windows.Forms.TextBox
        Me.txtLanguage = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpUserSetup = New System.Windows.Forms.GroupBox
        Me.txtTrainingTime = New System.Windows.Forms.TextBox
        Me.txtCalibrated = New System.Windows.Forms.TextBox
        Me.txtAudioSetup = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_VoceCenter = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlFillSpeakerInfo.SuspendLayout()
        Me.grpSpeakerConfiguration.SuspendLayout()
        Me.grpUserSetup.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_VoceCenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'imglstTree
        '
        Me.imglstTree.ImageStream = CType(resources.GetObject("imglstTree.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglstTree.TransparentColor = System.Drawing.Color.Transparent
        Me.imglstTree.Images.SetKeyName(0, "Mange User.ico")
        Me.imglstTree.Images.SetKeyName(1, "New User.ico")
        Me.imglstTree.Images.SetKeyName(2, "Export User.ico")
        Me.imglstTree.Images.SetKeyName(3, "sett.ico")
        Me.imglstTree.Images.SetKeyName(4, "Accuracy Center _16_04.ico")
        Me.imglstTree.Images.SetKeyName(5, "Audio settings.ico")
        Me.imglstTree.Images.SetKeyName(6, "Suppliment Settings.ico")
        Me.imglstTree.Images.SetKeyName(7, "Mobile Record Tranning.ico")
        Me.imglstTree.Images.SetKeyName(8, "Dictionary.ico")
        Me.imglstTree.Images.SetKeyName(9, "Vocabulary_16x16.ico")
        Me.imglstTree.Images.SetKeyName(10, "New vocabulary_01.ico")
        Me.imglstTree.Images.SetKeyName(11, "open vocabulary.ico")
        Me.imglstTree.Images.SetKeyName(12, "Build.ico")
        Me.imglstTree.Images.SetKeyName(13, "Edit vocabulary.ico")
        Me.imglstTree.Images.SetKeyName(14, "Voice Voice.ico")
        Me.imglstTree.Images.SetKeyName(15, "Add Words.ico")
        Me.imglstTree.Images.SetKeyName(16, "Add Word From Doc.ico")
        Me.imglstTree.Images.SetKeyName(17, "Add Words From List_02.ico")
        Me.imglstTree.Images.SetKeyName(18, "Command.ico")
        Me.imglstTree.Images.SetKeyName(19, "Trial Wordss.ico")
        Me.imglstTree.Images.SetKeyName(20, "Transcriber.ico")
        Me.imglstTree.Images.SetKeyName(21, "Settings.ico")
        Me.imglstTree.Images.SetKeyName(22, "options.ico")
        Me.imglstTree.Images.SetKeyName(23, "Option.ico")
        Me.imglstTree.Images.SetKeyName(24, "Performance Assistant.ico")
        Me.imglstTree.Images.SetKeyName(25, "Voice Admin Setting .ico")
        Me.imglstTree.Images.SetKeyName(26, "Tanscribe.ico")
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.pnlLeftMain)
        Me.pnlLeft.Controls.Add(Me.Panel2)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 53)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(225, 472)
        Me.pnlLeft.TabIndex = 14
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.Label17)
        Me.pnlLeftMain.Controls.Add(Me.Label18)
        Me.pnlLeftMain.Controls.Add(Me.Label19)
        Me.pnlLeftMain.Controls.Add(Me.Label20)
        Me.pnlLeftMain.Controls.Add(Me.trvCommands)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftMain.ForeColor = System.Drawing.Color.Black
        Me.pnlLeftMain.Location = New System.Drawing.Point(0, 27)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnlLeftMain.Size = New System.Drawing.Size(225, 445)
        Me.pnlLeftMain.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 441)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(219, 1)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 2)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 440)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(223, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 440)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(221, 1)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label1"
        '
        'trvCommands
        '
        Me.trvCommands.BackColor = System.Drawing.Color.White
        Me.trvCommands.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCommands.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCommands.ForeColor = System.Drawing.Color.Black
        Me.trvCommands.HideSelection = False
        Me.trvCommands.ImageIndex = 0
        Me.trvCommands.ImageList = Me.imglstTree
        Me.trvCommands.ItemHeight = 21
        Me.trvCommands.Location = New System.Drawing.Point(3, 1)
        Me.trvCommands.Name = "trvCommands"
        Me.trvCommands.SelectedImageIndex = 0
        Me.trvCommands.ShowLines = False
        Me.trvCommands.Size = New System.Drawing.Size(221, 441)
        Me.trvCommands.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Panel2.Size = New System.Drawing.Size(225, 27)
        Me.Panel2.TabIndex = 6
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(4, 23)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(219, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 4)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 20)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(223, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 20)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 3)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(221, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(221, 21)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "  Voice Settings"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(225, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 472)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlFillSpeakerInfo)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(228, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(800, 472)
        Me.pnlMain.TabIndex = 16
        '
        'pnlFillSpeakerInfo
        '
        Me.pnlFillSpeakerInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnlFillSpeakerInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFillSpeakerInfo.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlFillSpeakerInfo.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlFillSpeakerInfo.Controls.Add(Me.lbl_RightBrd)
        Me.pnlFillSpeakerInfo.Controls.Add(Me.lbl_TopBrd)
        Me.pnlFillSpeakerInfo.Controls.Add(Me.grpSpeakerConfiguration)
        Me.pnlFillSpeakerInfo.Controls.Add(Me.grpUserSetup)
        Me.pnlFillSpeakerInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFillSpeakerInfo.Location = New System.Drawing.Point(0, 27)
        Me.pnlFillSpeakerInfo.Name = "pnlFillSpeakerInfo"
        Me.pnlFillSpeakerInfo.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlFillSpeakerInfo.Size = New System.Drawing.Size(800, 445)
        Me.pnlFillSpeakerInfo.TabIndex = 4
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(2, 441)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(794, 1)
        Me.lbl_BottomBrd.TabIndex = 14
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(1, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 440)
        Me.lbl_LeftBrd.TabIndex = 13
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(796, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 440)
        Me.lbl_RightBrd.TabIndex = 12
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(1, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(796, 1)
        Me.lbl_TopBrd.TabIndex = 11
        Me.lbl_TopBrd.Text = "label1"
        '
        'grpSpeakerConfiguration
        '
        Me.grpSpeakerConfiguration.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSpeakerConfiguration.BackColor = System.Drawing.Color.Transparent
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtSpeakerDirectory)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtCurrentTopic)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtSourceTrained)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtDictationSource)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtSpeechModel)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtLanguage)
        Me.grpSpeakerConfiguration.Controls.Add(Me.txtUserName)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label14)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label12)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label5)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label4)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label3)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label2)
        Me.grpSpeakerConfiguration.Controls.Add(Me.Label1)
        Me.grpSpeakerConfiguration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSpeakerConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpSpeakerConfiguration.Location = New System.Drawing.Point(16, 15)
        Me.grpSpeakerConfiguration.Name = "grpSpeakerConfiguration"
        Me.grpSpeakerConfiguration.Size = New System.Drawing.Size(770, 277)
        Me.grpSpeakerConfiguration.TabIndex = 8
        Me.grpSpeakerConfiguration.TabStop = False
        Me.grpSpeakerConfiguration.Text = "Basic Information"
        '
        'txtSpeakerDirectory
        '
        Me.txtSpeakerDirectory.BackColor = System.Drawing.Color.White
        Me.txtSpeakerDirectory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpeakerDirectory.ForeColor = System.Drawing.Color.Black
        Me.txtSpeakerDirectory.Location = New System.Drawing.Point(176, 240)
        Me.txtSpeakerDirectory.Name = "txtSpeakerDirectory"
        Me.txtSpeakerDirectory.ReadOnly = True
        Me.txtSpeakerDirectory.Size = New System.Drawing.Size(496, 22)
        Me.txtSpeakerDirectory.TabIndex = 22
        '
        'txtCurrentTopic
        '
        Me.txtCurrentTopic.BackColor = System.Drawing.Color.White
        Me.txtCurrentTopic.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentTopic.ForeColor = System.Drawing.Color.Black
        Me.txtCurrentTopic.Location = New System.Drawing.Point(176, 204)
        Me.txtCurrentTopic.Name = "txtCurrentTopic"
        Me.txtCurrentTopic.ReadOnly = True
        Me.txtCurrentTopic.Size = New System.Drawing.Size(496, 22)
        Me.txtCurrentTopic.TabIndex = 21
        '
        'txtSourceTrained
        '
        Me.txtSourceTrained.BackColor = System.Drawing.Color.White
        Me.txtSourceTrained.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSourceTrained.ForeColor = System.Drawing.Color.Black
        Me.txtSourceTrained.Location = New System.Drawing.Point(176, 168)
        Me.txtSourceTrained.Name = "txtSourceTrained"
        Me.txtSourceTrained.ReadOnly = True
        Me.txtSourceTrained.Size = New System.Drawing.Size(496, 22)
        Me.txtSourceTrained.TabIndex = 20
        '
        'txtDictationSource
        '
        Me.txtDictationSource.BackColor = System.Drawing.Color.White
        Me.txtDictationSource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDictationSource.ForeColor = System.Drawing.Color.Black
        Me.txtDictationSource.Location = New System.Drawing.Point(176, 132)
        Me.txtDictationSource.Name = "txtDictationSource"
        Me.txtDictationSource.ReadOnly = True
        Me.txtDictationSource.Size = New System.Drawing.Size(496, 22)
        Me.txtDictationSource.TabIndex = 19
        '
        'txtSpeechModel
        '
        Me.txtSpeechModel.BackColor = System.Drawing.Color.White
        Me.txtSpeechModel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpeechModel.ForeColor = System.Drawing.Color.Black
        Me.txtSpeechModel.Location = New System.Drawing.Point(176, 96)
        Me.txtSpeechModel.Name = "txtSpeechModel"
        Me.txtSpeechModel.ReadOnly = True
        Me.txtSpeechModel.Size = New System.Drawing.Size(496, 22)
        Me.txtSpeechModel.TabIndex = 18
        '
        'txtLanguage
        '
        Me.txtLanguage.BackColor = System.Drawing.Color.White
        Me.txtLanguage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLanguage.ForeColor = System.Drawing.Color.Black
        Me.txtLanguage.Location = New System.Drawing.Point(176, 60)
        Me.txtLanguage.Name = "txtLanguage"
        Me.txtLanguage.ReadOnly = True
        Me.txtLanguage.Size = New System.Drawing.Size(496, 22)
        Me.txtLanguage.TabIndex = 17
        '
        'txtUserName
        '
        Me.txtUserName.BackColor = System.Drawing.Color.White
        Me.txtUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(176, 24)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(496, 22)
        Me.txtUserName.TabIndex = 16
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(4, 172)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(169, 14)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Dictation Source Trained :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(73, 208)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 14)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Current Topic :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(45, 244)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Speaker Directory :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(70, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Speech Model :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(55, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Dictation Source :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(94, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Language :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(88, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Name :"
        '
        'grpUserSetup
        '
        Me.grpUserSetup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpUserSetup.BackColor = System.Drawing.Color.Transparent
        Me.grpUserSetup.Controls.Add(Me.txtTrainingTime)
        Me.grpUserSetup.Controls.Add(Me.txtCalibrated)
        Me.grpUserSetup.Controls.Add(Me.txtAudioSetup)
        Me.grpUserSetup.Controls.Add(Me.Label13)
        Me.grpUserSetup.Controls.Add(Me.Label7)
        Me.grpUserSetup.Controls.Add(Me.Label6)
        Me.grpUserSetup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpUserSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpUserSetup.Location = New System.Drawing.Point(16, 307)
        Me.grpUserSetup.Name = "grpUserSetup"
        Me.grpUserSetup.Size = New System.Drawing.Size(770, 124)
        Me.grpUserSetup.TabIndex = 10
        Me.grpUserSetup.TabStop = False
        Me.grpUserSetup.Text = "User Setup"
        '
        'txtTrainingTime
        '
        Me.txtTrainingTime.BackColor = System.Drawing.Color.White
        Me.txtTrainingTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTrainingTime.ForeColor = System.Drawing.Color.Black
        Me.txtTrainingTime.Location = New System.Drawing.Point(176, 88)
        Me.txtTrainingTime.Name = "txtTrainingTime"
        Me.txtTrainingTime.ReadOnly = True
        Me.txtTrainingTime.Size = New System.Drawing.Size(196, 22)
        Me.txtTrainingTime.TabIndex = 24
        '
        'txtCalibrated
        '
        Me.txtCalibrated.BackColor = System.Drawing.Color.White
        Me.txtCalibrated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibrated.ForeColor = System.Drawing.Color.Black
        Me.txtCalibrated.Location = New System.Drawing.Point(176, 56)
        Me.txtCalibrated.Name = "txtCalibrated"
        Me.txtCalibrated.ReadOnly = True
        Me.txtCalibrated.Size = New System.Drawing.Size(196, 22)
        Me.txtCalibrated.TabIndex = 23
        '
        'txtAudioSetup
        '
        Me.txtAudioSetup.BackColor = System.Drawing.Color.White
        Me.txtAudioSetup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAudioSetup.ForeColor = System.Drawing.Color.Black
        Me.txtAudioSetup.Location = New System.Drawing.Point(176, 24)
        Me.txtAudioSetup.Name = "txtAudioSetup"
        Me.txtAudioSetup.ReadOnly = True
        Me.txtAudioSetup.Size = New System.Drawing.Size(196, 22)
        Me.txtAudioSetup.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(76, 92)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 14)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Training Time :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(78, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 14)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Is Calibrated :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(83, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Audio Setup :"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(800, 27)
        Me.Panel3.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(2, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(794, 1)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 20)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(796, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 20)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(796, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(796, 21)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "  Speaker Configuration"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_VoceCenter)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(1028, 53)
        Me.pnl_tlsp.TabIndex = 18
        '
        'tlsp_VoceCenter
        '
        Me.tlsp_VoceCenter.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_VoceCenter.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_VoceCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_VoceCenter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_VoceCenter.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_VoceCenter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.tlsp_VoceCenter.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_VoceCenter.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_VoceCenter.Name = "tlsp_VoceCenter"
        Me.tlsp_VoceCenter.Size = New System.Drawing.Size(1028, 53)
        Me.tlsp_VoceCenter.TabIndex = 0
        Me.tlsp_VoceCenter.Text = "toolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmVoiceCenter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 525)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVoiceCenter"
        Me.Text = "gloEMR Voice Center"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlFillSpeakerInfo.ResumeLayout(False)
        Me.grpSpeakerConfiguration.ResumeLayout(False)
        Me.grpSpeakerConfiguration.PerformLayout()
        Me.grpUserSetup.ResumeLayout(False)
        Me.grpUserSetup.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_VoceCenter.ResumeLayout(False)
        Me.tlsp_VoceCenter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Fill_Commands()
        With trvCommands
            .Nodes.Clear()
            Dim trvNde As TreeNode

            trvNde = New TreeNode
            trvNde.Text = "User Management"
            With trvNde
                .ImageIndex = 0
                .SelectedImageIndex = 0
            End With
            .Nodes.Add(trvNde)
            trvNde = Nothing

            'trvNde = New TreeNode
            'trvNde.Text = "Manage User"
            'With trvNde
            '    .ImageIndex = 1
            '    .SelectedImageIndex = 1
            'End With
            '.Nodes(0).Nodes.Add(trvNde)
            'trvNde = Nothing

            If gblnIsAdmin Then
                '' 20080923 If gstrProviderName = "" Then
                If gstrLoginProviderName = "" Then
                    trvNde = New TreeNode
                    trvNde.Text = "New User"
                    With trvNde
                        .ImageIndex = 1
                        .SelectedImageIndex = 1
                    End With
                    .Nodes(0).Nodes.Add(trvNde)
                    trvNde = Nothing
                    'tlbMain.Buttons(1).Visible = True

                    'trvNde = New TreeNode
                    'trvNde.Text = "Administrative Settings Custom"
                    'With trvNde
                    '    .ImageIndex = 22
                    '    .SelectedImageIndex = 22
                    'End With
                    '.Nodes(0).Nodes.Add(trvNde)
                    'trvNde = Nothing


                    trvNde = New TreeNode
                    trvNde.Text = "Administrative Settings"
                    With trvNde
                        .ImageIndex = 25
                        .SelectedImageIndex = 25
                    End With
                    .Nodes(0).Nodes.Add(trvNde)
                    trvNde = Nothing

                  
                End If
            End If


            '***************code commented by supriya 20/02/2006

            'trvNde = New TreeNode
            'trvNde.Text = "Open User"
            'With trvNde
            '    .ImageIndex = 3
            '    .SelectedImageIndex = 3
            'End With
            '.Nodes(0).Nodes.Add(trvNde)
            'trvNde = Nothing

            '***************code commented by supriya 20/02/2006

            If gblnSpeakerExists = True Then
                '***************code commented by supriya 20/02/2006

                'trvNde = New TreeNode
                'trvNde.Text = "Delete User"
                'With trvNde
                '    .ImageIndex = 4
                '    .SelectedImageIndex = 4
                'End With
                '.Nodes(0).Nodes.Add(trvNde)
                'trvNde = Nothing

                '***************code commented by supriya 20/02/2006


                trvNde = New TreeNode
                trvNde.Text = "Export User Profile"
                With trvNde
                    .ImageIndex = 2
                    .SelectedImageIndex = 2
                End With
                .Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing

                'trvNde = New TreeNode
                'trvNde.Text = "Import User Profile"
                'With trvNde
                '    .ImageIndex = 4
                '    .SelectedImageIndex = 4
                'End With
                '.Nodes(0).Nodes.Add(trvNde)
                'trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Settings"
                With trvNde
                    .ImageIndex = 3
                    .SelectedImageIndex = 3
                End With
                .Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Accuracy Center"
                With trvNde
                    .ImageIndex = 4
                    .SelectedImageIndex = 4
                End With
                .Nodes(1).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Audio Settings"
                With trvNde
                    .ImageIndex = 5
                    .SelectedImageIndex = 5
                End With
                .Nodes(1).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Supplementary Training"
                With trvNde
                    .ImageIndex = 6
                    .SelectedImageIndex = 6
                End With
                .Nodes(1).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Mobile Record Training"
                With trvNde
                    .ImageIndex = 7
                    .SelectedImageIndex = 7
                End With
                .Nodes(1).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Dictionary"
                With trvNde
                    .ImageIndex = 8
                    .SelectedImageIndex = 8
                End With
                .Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Vocabulary"
                With trvNde
                    .ImageIndex = 9
                    .SelectedImageIndex = 9
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "New Vocabulary"
                With trvNde
                    .ImageIndex = 10
                    .SelectedImageIndex = 10
                End With
                .Nodes(2).Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Open Vocabulary"
                With trvNde
                    .ImageIndex = 11
                    .SelectedImageIndex = 11
                End With
                .Nodes(2).Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Builder Vocabulary"
                With trvNde
                    .ImageIndex = 12
                    .SelectedImageIndex = 12
                End With
                .Nodes(2).Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Editor Vocabulary"
                With trvNde
                    .ImageIndex = 13
                    .SelectedImageIndex = 13
                End With
                .Nodes(2).Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "VOC"
                With trvNde
                    .ImageIndex = 14
                    .SelectedImageIndex = 14
                End With
                .Nodes(2).Nodes(0).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Add Word"
                With trvNde
                    .ImageIndex = 15
                    .SelectedImageIndex = 15
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Add word from Document"
                With trvNde
                    .ImageIndex = 16
                    .SelectedImageIndex = 16
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Add word from list"
                With trvNde
                    .ImageIndex = 17
                    .SelectedImageIndex = 17
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Command"
                With trvNde
                    .ImageIndex = 18
                    .SelectedImageIndex = 18
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Train words"
                With trvNde
                    .ImageIndex = 19
                    .SelectedImageIndex = 19
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Transcriber"
                With trvNde
                    .ImageIndex = 20
                    .SelectedImageIndex = 20
                End With
                .Nodes(2).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Tools"
                With trvNde
                    .ImageIndex = 21
                    .SelectedImageIndex = 21
                End With
                .Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Option"
                With trvNde
                    .ImageIndex = 22
                    .SelectedImageIndex = 22
                End With
                .Nodes(3).Nodes.Add(trvNde)
                trvNde = Nothing


                trvNde = New TreeNode
                trvNde.Text = "Help"
                With trvNde
                    .ImageIndex = 23
                    .SelectedImageIndex = 23
                End With
                .Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Performance Assistant"
                With trvNde
                    .ImageIndex = 24
                    .SelectedImageIndex = 24
                End With
                .Nodes(4).Nodes.Add(trvNde)
                trvNde = Nothing

                trvNde = New TreeNode
                trvNde.Text = "Transcribe Voice"
                With trvNde
                    .ImageIndex = 26
                    .SelectedImageIndex = 26
                End With
                .Nodes(3).Nodes.Add(trvNde)
                trvNde = Nothing
          
            Else

                'tlbMain.Buttons(4).Visible = False
                'tlbMain.Buttons(9).Visible = False
                'tlbbtnDeleteUser.Visible = False
                'tlbbtnAccuracyCenter.Visible = False
                'tlbbtnAudioSettings.Visible = False
                'tlbbtnSupplimentrySettings.Visible = False
                'tlbbtnNewVoc.Visible = False
                'tlbbtnOpenVoc.Visible = False
                'tlbbtnBuilderVoc.Visible = False
                'tlbbtnEditorVoc.Visible = False
                'tlbbtnVOCVoc.Visible = False
                'tlbbtnAddWord.Visible = False
                'tlbbtnAddWordFromDoc.Visible = False
                'tlbbtnAddWordFromList.Visible = False
                'tlbbtnCommand.Visible = False
                'tlbbtnTrainWords.Visible = False
                'tlbbtnTranscriber.Visible = False
                'tlbbtnOption.Visible = False
                'tlbbtnPerformanceAsst.Visible = False
                'tlbbtnSep1.Visible = False
                'tlbbtnSep2.Visible = False
                'tlbbtnSep3.Visible = False
                'tlbbtnSep4.Visible = False
            End If
            .ExpandAll()
        End With
    End Sub

    Private Sub frmVoiceCenter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Call Fill_Commands()
            If gblnSpeakerExists = True Then
                'AxDgnEngineControl1.set_CompatibilityModule(DNSTools.DgnCompatibilityModuleConstants.dgncompmoduleEditControlSupport, Me.Handle.ToInt32, False)
                'AxDgnEngineControl1.set_CompatibilityModule(DNSTools.DgnCompatibilityModuleConstants.dgncompmoduleNatText, Me.Handle.ToInt32, False)
                'Call Fill_Speakers()
                Call FillSpeakerDetails()
            End If
            pnlFillSpeakerInfo.BringToFront()
            Me.Cursor = Cursors.Default

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Initialize, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExecuteCommands(ByVal strcommand As String)
        With AxDgnEngineControl2
            Select Case Trim(strcommand)
                Case "Manage User"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgManageUsers) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgManageUsers, Me.Handle.ToInt64)
                    End If
                    'Dim intoldspeakers As Int16
                    'intoldspeakers = AxDgnEngineControl2.Speakers.Count
                    'If AxDgnEngineControl2.Speakers.Count > intoldspeakers Then
                    '    Dim strpath As String
                    '    strpath = .SpeakerDirectory & "\" & .Speakers.Item(.Speakers.Count) & "\current\options.ini"

                    '    Dim sw As StreamWriter = File.AppendText(strpath)
                    '    ' Add some text to the file.
                    '    sw.WriteLine("Save Speech With Document=0")
                    '    ' Arbitrary objects can also be written to the file.
                    '    sw.Close()
                    '    'AxDgnEngineControl2.SpeakerSave()
                    '    'AxDgnEngineControl2.SpeakerClose()
                    '    'AxDgnEngineControl2.Speaker = gstrCurrentSpeaker
                    'End If
                Case "New User"
                    blnMicrophoneOn = True
                    'Dim intoldspeakers As Int16
                    'intoldspeakers = AxDgnEngineControl2.Speakers.Count
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgSpeakerNew) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgSpeakerNew, Me.Handle.ToInt64)
                    End If
                    'If AxDgnEngineControl2.Speakers.Count > intoldspeakers Then
                    '    Dim strpath As String
                    '    strpath = .SpeakerDirectory & "\" & .Speakers.Item(.Speakers.Count) & "\current\options.ini"

                    '    Dim sw As StreamWriter = File.AppendText(strpath)
                    '    ' Add some text to the file.
                    '    sw.WriteLine("Save Speech With Document=0")
                    '    ' Arbitrary objects can also be written to the file.
                    '    sw.Close()
                    '    AxDgnEngineControl2.SpeakerSave()
                    '    AxDgnEngineControl2.SpeakerClose()
                    '    AxDgnEngineControl2.Speaker = gstrCurrentSpeaker
                    'End If
                    'Case "Open User"
                    '    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgSpeakerOpen) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                    '        .DlgShow(DNSTools.DgnDialogConstants.dgndlgSpeakerOpen, Me.Handle.ToInt64)
                    '    End If
                    'Case "Delete User"
                    '    If Trim(AxDgnEngineControl2.Speaker) <> "" Then
                    '        If Trim(AxDgnEngineControl1.Speaker) = Trim(AxDgnEngineControl2.Speaker) Then
                    '            MessageBox.Show(AxDgnEngineControl2.Speaker & " is currently in Use. You can not delete this user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '        If MessageBox.Show("Are you sure, you want to delete " & cmbSpeakers.SelectedItem & " user?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    '            AxDgnEngineControl1.SpeakerDelete(cmbSpeakers.SelectedItem)
                    '            Call Fill_Speakers(True)
                    '        End If
                    '    Else
                    '        MessageBox.Show("Please select the Speaker which you want to delete.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    End If
                Case "Export User Profile"
                    '*****************************code commented by supriya on 20/02/2006
                    'If MessageBox.Show("Are you sure, you want to export " & AxDgnEngineControl2.Speaker & " voice files?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'Dim strLocation As String
                    'Dim objFolder As New clsBrowseForFolder
                    'strLocation = objFolder.BrowseDialog("Select Export Files Location")
                    'objFolder = Nothing
                    'If Trim(strLocation) = "" Then
                    '    Exit Sub
                    'End If
                    'If Dir(strLocation, FileAttribute.Directory) = "" Then
                    '    MessageBox.Show("Please select the Export Files Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    'If Dir(strLocation & "\" & cmbSpeakers.SelectedItem, FileAttribute.Directory) <> "" Then
                    '    If MessageBox.Show(cmbSpeakers.SelectedItem & " user's voice files already exported at " & strLocation & "\" & vbCrLf & "Are you sure, you want to overwrite the existing files ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    '        Dim objDir As New DirectoryInfo(strLocation & "\" & cmbSpeakers.SelectedItem)
                    '        objDir.Delete(True)
                    '        objDir = Nothing
                    '    Else
                    '        Exit Sub
                    '    End If
                    'End If
                    'AxDgnEngineControl2.SpeakerExport(cmbSpeakers.SelectedItem, strLocation)
                    'MessageBox.Show(cmbSpeakers.SelectedItem & " user's voice files successfully exported", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If
                    '*****************************code commented by supriya on 20/02/2006

                    If MessageBox.Show("Are you sure, you want to export " & AxDgnEngineControl2.Speaker & " voice files?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim strLocation As String
                        Dim objFolder As New clsBrowseForFolder
                        strLocation = objFolder.BrowseDialog("Select Export Files Location")
                        objFolder = Nothing
                        If Trim(strLocation) = "" Then
                            Exit Sub
                        End If
                        If Dir(strLocation, FileAttribute.Directory) = "" Then
                            MessageBox.Show("Please select the Export Files Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                        If Dir(strLocation & "\" & AxDgnEngineControl2.Speaker, FileAttribute.Directory) <> "" Then
                            If MessageBox.Show(AxDgnEngineControl2.Speaker & " user's voice files already exported at " & strLocation & "\" & vbCrLf & "Are you sure, you want to overwrite the existing files ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Dim objDir As New DirectoryInfo(strLocation & "\" & AxDgnEngineControl2.Speaker)
                                objDir.Delete(True)
                                objDir = Nothing
                            Else
                                Exit Sub
                            End If
                        End If
                        AxDgnEngineControl2.SpeakerExport(AxDgnEngineControl2.Speaker, strLocation)
                        MessageBox.Show(AxDgnEngineControl2.Speaker & " user's voice files successfully exported", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "Import User Profile"
                    'Dim strLocation As String
                    'Dim objFolder As New clsBrowseForFolder
                    'strLocation = objFolder.BrowseDialog("Select Import Files Location")
                    'objFolder = Nothing
                    'If Dir(strLocation, FileAttribute.Directory) = "" Then
                    '    MessageBox.Show("Please select the Import Files Directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    ''Directory is selected
                    ''Check Directory is valid or not
                    'If AxDgnEngineControl1.get_SpeakersInDirectory(strLocation).Count <= 0 Then
                    '    MessageBox.Show("Invalid Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If

                    ''Check All Topics are installed on computer or not
                    ''Get Speaker Name
                    'Dim objDirectory As New DirectoryInfo(strLocation)
                    'Dim strUserName As String
                    'strUserName = objDirectory.Name
                    'If AxDgnEngineControl1.get_Topics(strUserName).Count <= 0 Then
                    '    MessageBox.Show("Invalid Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    ''Check This user is already exists or not
                    'Dim strDefaultSpeakerDirectory As String
                    'strDefaultSpeakerDirectory = AxDgnEngineControl1.SpeakerDirectory




                    'If Directory.Exists(strDefaultSpeakerDirectory & "\" & strUserName) = True Then
                    '    If MessageBox.Show(strUserName & " user already exists. Do you want to overwrite " & strUserName & " user's profile?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    '        Exit Sub
                    '    End If
                    'End If



                    'If Dir(strLocation & "\" & cmbUsers.SelectedItem, FileAttribute.Directory) <> "" Then
                    '    If MessageBox.Show(cmbUsers.SelectedItem & " user's voice files already exported at " & strLocation & "\" & vbCrLf & "Are you sure, you want to overwrite the existing files ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    '        Dim objDir As New DirectoryInfo(strLocation & "\" & cmbUsers.SelectedItem)
                    '        objDir.Delete(True)
                    '        objDir = Nothing
                    '    Else
                    '        Exit Sub
                    '    End If
                    'End If
                    'AxDgnEngineControl1.SpeakerExport(cmbUsers.SelectedItem, strLocation)
                    'MessageBox.Show(cmbUsers.SelectedItem & " user's voice files successfully exported", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case "Accuracy Center"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgAccuracyCenter) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgAccuracyCenter, Me.Handle.ToInt64)
                    End If
                Case "Audio Settings"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgAudioSetupWizard) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgAudioSetupWizard, Me.Handle.ToInt64)
                    End If
                Case "Supplementary Training"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgGeneralTraining) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgGeneralTraining, Me.Handle.ToInt64)
                    End If
                Case "Mobile Record Training"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgMobileTraining) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgMobileTraining, Me.Handle.ToInt64)
                    End If
                Case "New Vocabulary"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgVocabularyNew) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgVocabularyNew, Me.Handle.ToInt64)
                    End If
                Case "Open Vocabulary"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgVocabularyOpen) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgVocabularyOpen, Me.Handle.ToInt64)
                    End If
                Case "Builder Vocabulary"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgVocabularyBuilder) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgVocabularyBuilder, Me.Handle.ToInt64)
                    End If
                Case "Editor Vocabulary"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgVocabularyEditor) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgVocabularyEditor, Me.Handle.ToInt64)
                    End If
                Case "VOC"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgVocBuilder) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgVocBuilder, Me.Handle.ToInt64)
                    End If
                Case "Add Word"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgAddWord) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgAddWord, Me.Handle.ToInt64)
                    End If
                Case "Add word from Document"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgAddWordsFromDocs) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgAddWordsFromDocs, Me.Handle.ToInt64)
                    End If
                Case "Add word from list"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgAddWordsFromList) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgAddWordsFromList, Me.Handle.ToInt64)
                    End If
                Case "Command"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgNewCommandWizard) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgNewCommandWizard, Me.Handle.ToInt64)
                    End If
                Case "Train words"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgTrainWords) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgTrainWords, Me.Handle.ToInt64)
                    End If
                Case "Transcriber"

                Case "Option"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgOptions) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgOptions, Me.Handle.ToInt64)
                    End If
                Case "Performance Assistant"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgPerformanceAssistant) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgPerformanceAssistant, Me.Handle.ToInt64)
                    End If
                Case "Administrative Settings Custom"
                    On Error GoTo Cannot_Enter_Administration_Mode
                    AxDgnEngineControl2.EnterAdministrationMode(Me.Handle.ToInt32)

                    Dim settingsForm As New frmVo_Roamingsettings
                    settingsForm.Initialize(AxDgnEngineControl2)

                    If settingsForm.ShowDialog(IIf(IsNothing(settingsForm.Parent), Me, settingsForm.Parent)) = DialogResult.OK Then
                        ' Update the locations list in case anything changed
                        updateLocationsList(False)
                    End If

                    AxDgnEngineControl2.LeaveAdministrationMode()
                    settingsForm.Dispose()
                    settingsForm = Nothing
                    Return

Cannot_Enter_Administration_Mode:
                    MsgBox("Cannot enter administration mode" & vbCrLf & _
                         "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
                Case "Transcribe Voice"
                    'If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgTranscribe) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                    '    .DlgShow(DNSTools.DgnDialogConstants.dgndlgTranscribe, Me.Handle.ToInt64)
                    'End If
                    Dim ofrmTranscribe As New frmVo_Transcribe
                    ofrmTranscribe.Initialize(AxDgnEngineControl2)
                    ofrmTranscribe.ShowDialog(IIf(IsNothing(ofrmTranscribe.Parent), Me, ofrmTranscribe.Parent))
                    ofrmTranscribe.Dispose()
                    ofrmTranscribe = Nothing
                Case "Administrative Settings"
                    If .DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgRoamingUsersOptions) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        .DlgShow(DNSTools.DgnDialogConstants.dgndlgRoamingUsersOptions, Me.Handle.ToInt64)
                    End If
            End Select
            'trvCommands.SelectedNode = trvCommands.Nodes.Item(0)
        End With

    End Sub
    Private Sub updateLocationsList(ByVal bSelectBrowseDirectory As Boolean)
        'code commented by supriya
        'Dim prevLocation As String
        'prevLocation = cbLocations.Text

        'cbLocations.Items.Clear()
        'code commented by supriya


        ' Add roaming locations if RU is on
        If AxDgnEngineControl2.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn) Then
            Dim networkDirectories As DNSTools.IDgnNetworkDirectories
            networkDirectories = AxDgnEngineControl2.RoamingUserNetworkDirectories

            'Dim networkDirectory As DNSTools.IDgnNetworkDirectory

            'code commented by supriya
            'For Each networkDirectory In networkDirectories
            '    cbLocations.Items.Add("<" + networkDirectory.DisplayName + ">")
            'Next networkDirectory
            'code commented by supriya
        End If

        ' Add <Default> location if browsing is enabled
        'code commented by supriya
        'If allowBrowse() Then
        '    cbLocations.Items.Add(txtDefDirectory)

        '    If Not txtBrowseLocation = "" Then
        '        cbLocations.Items.Add(txtBrowseLocation)
        '    End If
        'End If

        'If bSelectBrowseDirectory Then
        '    cbLocations.Text = txtBrowseLocation
        'Else
        '    restorePrevSelection(cbLocations, prevLocation)
        'End If
        'code commented by supriya
    End Sub
    Private Sub tlbMain_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        'Try
        '    'Me.Cursor = Cursors.WaitCursor
        '    'Dim obje As System.Windows.Forms.TreeViewEventArgs


        '    Select Case e.Button.Tag
        '        Case "Close"
        '            Me.Close()
        '        Case Else
        '            Call ExecuteCommands(e.Button.Tag)

        '            '    'Case "ManageUser"
        '            '    '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(0)
        '            'Case "NewUser"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(0))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '        'Case "OpenUser"
        '            '        '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(2)
        '            '        'Case "DeleteUser"
        '            '        '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(3)
        '            '    Case "ExportUser"
        '            '        If gblnIsAdmin Then
        '            '            obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(1))
        '            '        Else
        '            '            obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(0))
        '            '        End If
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "AccuracyCenter"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(0))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "AudioSettings"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(1))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "SupplimentrySettings"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(2))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "MobileRecordTraining"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(3))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "NewVOC"
        '            '        'trvCommands.SelectedNode = trvCommands.Nodes(2).Nodes(0).Nodes(0)
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(0))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "OpenVOC"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(1))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "BuilderVOC"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(2))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "EditorVOC"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(3))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "VOCVOC"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(4))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "AddWord"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(1))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "AddWordDoc"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(2))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "AddWordList"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(3))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "Command"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(4))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "TrainWords"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(5))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "Transcriber"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(6))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "Option"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(3).Nodes(0))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "PerformanceAssistant"
        '            '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(4).Nodes(0))
        '            '        Call trvCommands_AfterSelect(trvCommands, obje)
        '            '    Case "Close"
        '            '        Me.Close()
        '    End Select
        '    'Call ExecuteCommands()
        '    'Me.Cursor = Cursors.Default
        'Catch objErr As Exception
        '    Me.Cursor = Cursors.Default
        '    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub
    Private Sub trvCommands_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCommands.MouseDown
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim trvNode As TreeNode
            trvNode = trvCommands.GetNodeAt(e.X, e.Y)

            If IsNothing(trvNode) = False Then
                trvCommands.SelectedNode = trvNode
                Call ExecuteCommands(trvCommands.SelectedNode.Text)
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVoiceCenter_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            'If AxDgnEngineControl1.Speakers.Count >= 1 Then
            '    gblnSpeakerExists = True
            'End If
            'e.Cancel = Not checkChanges()
            If Not IsNothing(AxDgnEngineControl2) Then
                AxDgnEngineControl2 = Nothing
            End If
            CType(Me.MdiParent, MainMenu).tlbStripMain.Visible = True

            '' Added By Mahesh 20080319
            '' To Enable/ Register HotKey
            '' Ref: Hot key Gets Disabled after opening the Voice center form
            CType(Me.MdiParent, MainMenu).RegisterMyHotKey()
            CType(Me.MdiParent, MainMenu).timerLockScreen.Enabled = True
            ''
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Close, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearSpeakerDetails()
        txtUserName.Text = ""
        txtAudioSetup.Text = ""
        txtLanguage.Text = ""
        txtCalibrated.Text = ""
        txtSpeakerDirectory.Text = ""
        txtTrainingTime.Text = ""
        txtCurrentTopic.Text = ""
        txtDictationSource.Text = ""
        txtSourceTrained.Text = ""
        txtSpeechModel.Text = ""
    End Sub

    Private Sub AxDgnEngineControl2_SpeakerChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxDgnEngineControl2.SpeakerChanged
        Try
            blnMicrophoneOn = False
            'Dim frm As MainMenu
            'frm = CType(Me.MdiParent, MainMenu)
            'If frm.blnSpeakerChanged Then
            '    frm.UnRegisterVoiceComponents()
            '    frm.blnSpeakerChanged = False
            'End If
            Call FillSpeakerDetails()
            'Dim frm As MainMenu
            'frm = Me.MdiParent
            'If Not IsNothing(frm) Then
            '    frm.StatusBar1.Panels(3).Text = "Current Speaker :- " & Trim(AxDgnEngineControl2.Speakers)
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillSpeakerDetails()
        Dim strtext As String
        Dim ncount As Int16
        If AxDgnEngineControl2.Speaker <> "" Then

            'Dim objEngineControl As New DNSTools.DgnEngineControl
            'With objEngineControl
            With AxDgnEngineControl2
                txtUserName.Text = AxDgnEngineControl2.Speaker
                txtAudioSetup.Text = .AudioSetupComplete
                strtext = ""

                Select Case .get_SpeakerLanguage(AxDgnEngineControl2.Speaker)
                    Case DgnLanguageConstants.dgnlangAustralianEnglish
                        strtext = strtext & "Australian English,"
                    Case DgnLanguageConstants.dgnlangCastilianSpanish
                        strtext = strtext & "Castilian Spanish,"
                    Case DgnLanguageConstants.dgnlangDutch
                        strtext = strtext & "Dutch,"
                    Case DgnLanguageConstants.dgnlangEnglish
                        strtext = strtext & "English,"
                    Case DgnLanguageConstants.dgnlangFrench
                        strtext = strtext & "French,"
                    Case DgnLanguageConstants.dgnlangGerman
                        strtext = strtext & "German,"
                    Case DgnLanguageConstants.dgnlangIndianEnglish
                        strtext = strtext & "Indian English,"
                    Case DgnLanguageConstants.dgnlangItalian
                        strtext = strtext & "Italian,"
                    Case DgnLanguageConstants.dgnlangJapanese
                        strtext = strtext & "Japanese,"
                    Case DgnLanguageConstants.dgnlangLatinAmericanSpanish
                        strtext = strtext & "Latin American Spanish,"
                    Case DgnLanguageConstants.dgnlangPortuguese
                        strtext = strtext & "Portuguese,"
                    Case DgnLanguageConstants.dgnlangSingaporeanEnglish
                        strtext = strtext & "Singaporean English,"
                    Case DgnLanguageConstants.dgnlangSpanish
                        strtext = strtext & "Spanish,"
                    Case DgnLanguageConstants.dgnlangUKEnglish
                        strtext = strtext & "UK English,"
                    Case DgnLanguageConstants.dgnlangUSEnglish
                        strtext = strtext & "US English,"
                End Select

                'Fix for Case GLO2010-0004494
                If strtext.Length > 0 Then
                    txtLanguage.Text = strtext.Substring(0, strtext.Length - 1)
                End If

                txtCalibrated.Text = .SpeakerCalibrated

                txtSpeakerDirectory.Text = .SpeakerDirectory
                txtTrainingTime.Text = .SpeakerTrainingTime

                strtext = ""
                For ncount = 1 To .get_Topics(AxDgnEngineControl2.Speaker).Count
                    strtext = strtext & .get_Topics(AxDgnEngineControl2.Speaker).Item(ncount) & ","
                Next

                'Fix for Case GLO2010-0004494
                If strtext.Length > 0 Then
                    txtCurrentTopic.Text = strtext.Substring(0, strtext.Length - 1)
                End If

                strtext = ""
                Dim enmsource As DNSTools.DgnDictationSourceConstants
                For ncount = 1 To .get_DictationSources(AxDgnEngineControl2.Speaker).Count
                    enmsource = AxDgnEngineControl2.get_DictationSources(AxDgnEngineControl2.Speaker).Item(ncount)
                    Select Case .get_DictationSources(AxDgnEngineControl2.Speaker).Item(ncount)

                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneFarField
                            strtext = strtext & "Microphone Far Field,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneFarFieldAndrea
                            strtext = strtext & "Microphone Far Field Andrea,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneFarFieldLineIn
                            strtext = strtext & "Microphone Far Field Line In,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneFarFieldUsb
                            strtext = strtext & "Microphone Far Field USB,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneFarFieldUsb
                            strtext = strtext & "Microphone Far Field USB,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneLineIn
                            strtext = strtext & "Microphone Line In,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneMicIn
                            strtext = strtext & "Microphone Mic In,"
                        Case DgnDictationSourceConstants.dgndictsrcMicrophoneUsb
                            strtext = strtext & "Microphone USB,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderGeneric
                            strtext = strtext & "Recorder Generic,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderIpaq
                            strtext = strtext & "Recorder Ipaq,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderOlympus
                            strtext = strtext & "Recorder Olympus,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderPanasonicRr
                            strtext = strtext & "Recorder Panasonic,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderSony
                            strtext = strtext & "Recorder Sony,"
                        Case DgnDictationSourceConstants.dgndictsrcRecorderSonyCX
                            strtext = strtext & "Recorder Sony CX,"
                            'Case DgnDictationSourceConstants.dgndictsrcRecorderVoiceit
                            '    strtext = strtext & "Recorder Voice,"
                        Case DgnDictationSourceConstants.dgndictsrcTelephony
                            strtext = strtext & "Telephony,"
                        Case DgnDictationSourceConstants.dgndictsrcWaveFiles
                            strtext = strtext & "Wave Files,"
                    End Select
                Next

                'Fix for Case GLO2010-0004494
                If strtext.Length > 0 Then
                    txtDictationSource.Text = strtext.Substring(0, strtext.Length - 1)
                End If

                txtSourceTrained.Text = .get_SpeakerDictationSourceTrained(AxDgnEngineControl2.Speaker, enmsource)

                'txtSourceTrained.Text = .get_SpeakerDictationSourceTrained(AxDgnEngineControl2.Speaker)

                txtSpeechModel.Text = .get_BaseSpeakerModel(AxDgnEngineControl2.Speaker, .get_DictationSources(AxDgnEngineControl2.Speaker).Item(1))

            End With
            'objEngineControl = Nothing
        End If
    End Sub

    Private Sub frmVoiceCenter_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        'Lock screen disabled

        'code commented as mic on/off logic has changed
        'blnMicrophoneOn = True
        'Dim frm As MainMenu
        'frm = CType(Me.MdiParent, MainMenu)
        'frm.picLockScreen.Visible = False
        'frm.mnuLockApplication.Visible = False
        'frm = Nothing
        'code commented as mic on/off logic has changed
        Dim frm As MainMenu
        frm = CType(Me.MdiParent, MainMenu)
        frm.picLockScreen.Visible = False
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicOn Or frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicSleeping Then
                frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicOff
            End If
            If Not IsNothing(Me.MdiParent) Then
                frm.timerLockScreen.Enabled = False
            End If
        End If
        frm = Nothing
        'Lock screen disabled
        blnMicrophoneOn = True
        'Panel1.Visible = False
    End Sub

    Private Sub frmVoiceCenter_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        'Lock screen enabled

        'code commented as mic on/off logic has changed
        'blnMicrophoneOn = False
        'Dim frm As MainMenu
        'frm = CType(Me.MdiParent, MainMenu)
        'frm.picLockScreen.Visible = True
        'frm.mnuLockApplication.Visible = True
        'frm = Nothing
        'code commented as mic on/off logic has changed

        Dim frm As MainMenu
        frm = CType(Me.MdiParent, MainMenu)
        frm.picLockScreen.Visible = True
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicOn Or frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicSleeping Then
                frm.DgnMicBtn1.MicState = DgnMicStateConstants.dgnmicOff
            End If

            '' Code commeneted By Mahesh 20080321
            '' Ref: Hot key Gets Disabled after opening the Voice center form
            ' frm.timerLockScreen.Enabled = False
            ''
        End If
        frm = Nothing
        blnMicrophoneOn = False
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Dim obje As System.Windows.Forms.TreeViewEventArgs


            Select Case e.ClickedItem.Tag
                Case "Close"
                    Me.Close()
                Case Else
                    Call ExecuteCommands(e.ClickedItem.Tag)

                    '    'Case "ManageUser"
                    '    '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(0)
                    'Case "NewUser"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(0))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '        'Case "OpenUser"
                    '        '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(2)
                    '        'Case "DeleteUser"
                    '        '    trvCommands.SelectedNode = trvCommands.Nodes(0).Nodes(3)
                    '    Case "ExportUser"
                    '        If gblnIsAdmin Then
                    '            obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(1))
                    '        Else
                    '            obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(0).Nodes(0))
                    '        End If
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "AccuracyCenter"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(0))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "AudioSettings"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(1))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "SupplimentrySettings"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(2))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "MobileRecordTraining"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(1).Nodes(3))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "NewVOC"
                    '        'trvCommands.SelectedNode = trvCommands.Nodes(2).Nodes(0).Nodes(0)
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(0))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "OpenVOC"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(1))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "BuilderVOC"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(2))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "EditorVOC"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(3))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "VOCVOC"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(0).Nodes(4))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "AddWord"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(1))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "AddWordDoc"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(2))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "AddWordList"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(3))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "Command"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(4))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "TrainWords"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(5))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "Transcriber"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(2).Nodes(6))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "Option"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(3).Nodes(0))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "PerformanceAssistant"
                    '        obje = New System.Windows.Forms.TreeViewEventArgs(trvCommands.Nodes(4).Nodes(0))
                    '        Call trvCommands_AfterSelect(trvCommands, obje)
                    '    Case "Close"
                    '        Me.Close()
            End Select
            'Call ExecuteCommands()
            'Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()

    End Sub
End Class
