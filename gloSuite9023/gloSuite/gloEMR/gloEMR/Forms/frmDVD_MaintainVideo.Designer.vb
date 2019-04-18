<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDVD_MaintainVideo
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
                If (IsNothing(openfiledilogVideo) = False) Then
                    openfiledilogVideo.Dispose()
                    openfiledilogVideo = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDVD_MaintainVideo))
        Me.sc_Main = New System.Windows.Forms.SplitContainer
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.c1CategorisedDocuments = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlUploadBtn = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnUploadVideo = New System.Windows.Forms.Button
        Me.pnlRightMain = New System.Windows.Forms.Panel
        Me.WMPlayer = New AxWMPLib.AxWindowsMediaPlayer
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pnlRight_TOP = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblMediaName = New System.Windows.Forms.Label
        Me.pnlRignt_Controls = New System.Windows.Forms.Panel
        Me.pic_Play = New System.Windows.Forms.PictureBox
        Me.pic_Pause = New System.Windows.Forms.PictureBox
        Me.lblCurrentDuration = New System.Windows.Forms.Label
        Me.btnOpenFile = New System.Windows.Forms.Button
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnPlayPause = New System.Windows.Forms.Button
        Me.tb_SeekBar = New System.Windows.Forms.TrackBar
        Me.lblDuration = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.openfiledilogVideo = New System.Windows.Forms.OpenFileDialog
        Me.tooltpPause = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.Img_Reviwed = New System.Windows.Forms.PictureBox
        Me.tls_DVDMaintain = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnViewAcknowlegment = New System.Windows.Forms.ToolStripButton
        Me.ts_btnAcknowlegment = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.sc_Main.Panel1.SuspendLayout()
        Me.sc_Main.Panel2.SuspendLayout()
        Me.sc_Main.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.c1CategorisedDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlUploadBtn.SuspendLayout()
        Me.pnlRightMain.SuspendLayout()
        CType(Me.WMPlayer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlRight_TOP.SuspendLayout()
        Me.pnlRignt_Controls.SuspendLayout()
        CType(Me.pic_Play, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Pause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tb_SeekBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tls_DVDMaintain.SuspendLayout()
        Me.SuspendLayout()
        '
        'sc_Main
        '
        Me.sc_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sc_Main.Location = New System.Drawing.Point(0, 55)
        Me.sc_Main.Margin = New System.Windows.Forms.Padding(4, 0, 4, 3)
        Me.sc_Main.Name = "sc_Main"
        '
        'sc_Main.Panel1
        '
        Me.sc_Main.Panel1.Controls.Add(Me.Panel2)
        Me.sc_Main.Panel1.Controls.Add(Me.Panel1)
        '
        'sc_Main.Panel2
        '
        Me.sc_Main.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.sc_Main.Panel2.Controls.Add(Me.pnlRightMain)
        Me.sc_Main.Panel2.Controls.Add(Me.Panel3)
        Me.sc_Main.Panel2.Controls.Add(Me.pnlRignt_Controls)
        Me.sc_Main.Size = New System.Drawing.Size(743, 521)
        Me.sc_Main.SplitterDistance = 176
        Me.sc_Main.SplitterWidth = 3
        Me.sc_Main.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.c1CategorisedDocuments)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Panel2.Size = New System.Drawing.Size(176, 493)
        Me.Panel2.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(4, 489)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(170, 1)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 486)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'c1CategorisedDocuments
        '
        Me.c1CategorisedDocuments.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1CategorisedDocuments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1CategorisedDocuments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1CategorisedDocuments.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1CategorisedDocuments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1CategorisedDocuments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1CategorisedDocuments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1CategorisedDocuments.Location = New System.Drawing.Point(3, 4)
        Me.c1CategorisedDocuments.Name = "c1CategorisedDocuments"
        Me.c1CategorisedDocuments.Rows.Count = 0
        Me.c1CategorisedDocuments.Rows.DefaultSize = 19
        Me.c1CategorisedDocuments.Rows.Fixed = 0
        Me.c1CategorisedDocuments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1CategorisedDocuments.Size = New System.Drawing.Size(171, 486)
        Me.c1CategorisedDocuments.StyleInfo = resources.GetString("c1CategorisedDocuments.StyleInfo")
        Me.c1CategorisedDocuments.TabIndex = 25
        Me.c1CategorisedDocuments.Tree.Column = 2
        Me.c1CategorisedDocuments.Tree.Indent = 72
        Me.c1CategorisedDocuments.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.c1CategorisedDocuments.Tree.NodeImageCollapsed = CType(resources.GetObject("c1CategorisedDocuments.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1CategorisedDocuments.Tree.NodeImageExpanded = CType(resources.GetObject("c1CategorisedDocuments.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.c1CategorisedDocuments.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(174, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 486)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(172, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlUploadBtn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(176, 28)
        Me.Panel1.TabIndex = 27
        Me.Panel1.Visible = False
        '
        'pnlUploadBtn
        '
        Me.pnlUploadBtn.Controls.Add(Me.Label1)
        Me.pnlUploadBtn.Controls.Add(Me.Label6)
        Me.pnlUploadBtn.Controls.Add(Me.Label7)
        Me.pnlUploadBtn.Controls.Add(Me.Label8)
        Me.pnlUploadBtn.Controls.Add(Me.btnUploadVideo)
        Me.pnlUploadBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUploadBtn.Location = New System.Drawing.Point(3, 0)
        Me.pnlUploadBtn.Name = "pnlUploadBtn"
        Me.pnlUploadBtn.Size = New System.Drawing.Size(173, 25)
        Me.pnlUploadBtn.TabIndex = 26
        Me.pnlUploadBtn.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(171, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(172, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 24)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(173, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'btnUploadVideo
        '
        Me.btnUploadVideo.BackColor = System.Drawing.Color.Transparent
        Me.btnUploadVideo.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnUploadVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUploadVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUploadVideo.FlatAppearance.BorderSize = 0
        Me.btnUploadVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUploadVideo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUploadVideo.Location = New System.Drawing.Point(0, 0)
        Me.btnUploadVideo.Name = "btnUploadVideo"
        Me.btnUploadVideo.Size = New System.Drawing.Size(173, 25)
        Me.btnUploadVideo.TabIndex = 0
        Me.btnUploadVideo.Text = "&Upload Video"
        Me.btnUploadVideo.UseVisualStyleBackColor = False
        Me.btnUploadVideo.Visible = False
        '
        'pnlRightMain
        '
        Me.pnlRightMain.Controls.Add(Me.WMPlayer)
        Me.pnlRightMain.Controls.Add(Me.Label13)
        Me.pnlRightMain.Controls.Add(Me.Label14)
        Me.pnlRightMain.Controls.Add(Me.Label15)
        Me.pnlRightMain.Controls.Add(Me.Label16)
        Me.pnlRightMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRightMain.Location = New System.Drawing.Point(0, 30)
        Me.pnlRightMain.Name = "pnlRightMain"
        Me.pnlRightMain.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlRightMain.Size = New System.Drawing.Size(564, 412)
        Me.pnlRightMain.TabIndex = 2
        '
        'WMPlayer
        '
        Me.WMPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WMPlayer.Enabled = True
        Me.WMPlayer.Location = New System.Drawing.Point(1, 1)
        Me.WMPlayer.Name = "WMPlayer"
        Me.WMPlayer.OcxState = CType(resources.GetObject("WMPlayer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.WMPlayer.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.WMPlayer.Size = New System.Drawing.Size(559, 407)
        Me.WMPlayer.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 408)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(559, 1)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 408)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(560, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 408)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(561, 1)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlRight_TOP)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(564, 30)
        Me.Panel3.TabIndex = 3
        '
        'pnlRight_TOP
        '
        Me.pnlRight_TOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlRight_TOP.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlRight_TOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRight_TOP.Controls.Add(Me.Label5)
        Me.pnlRight_TOP.Controls.Add(Me.Label2)
        Me.pnlRight_TOP.Controls.Add(Me.Label3)
        Me.pnlRight_TOP.Controls.Add(Me.Label4)
        Me.pnlRight_TOP.Controls.Add(Me.lblMediaName)
        Me.pnlRight_TOP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight_TOP.Location = New System.Drawing.Point(0, 3)
        Me.pnlRight_TOP.Name = "pnlRight_TOP"
        Me.pnlRight_TOP.Size = New System.Drawing.Size(561, 24)
        Me.pnlRight_TOP.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(559, 1)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(560, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(561, 1)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "label1"
        '
        'lblMediaName
        '
        Me.lblMediaName.BackColor = System.Drawing.Color.Transparent
        Me.lblMediaName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMediaName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaName.Location = New System.Drawing.Point(0, 0)
        Me.lblMediaName.Name = "lblMediaName"
        Me.lblMediaName.Size = New System.Drawing.Size(561, 24)
        Me.lblMediaName.TabIndex = 25
        Me.lblMediaName.Text = "   Media Name"
        Me.lblMediaName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlRignt_Controls
        '
        Me.pnlRignt_Controls.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlRignt_Controls.Controls.Add(Me.pic_Play)
        Me.pnlRignt_Controls.Controls.Add(Me.pic_Pause)
        Me.pnlRignt_Controls.Controls.Add(Me.lblCurrentDuration)
        Me.pnlRignt_Controls.Controls.Add(Me.btnOpenFile)
        Me.pnlRignt_Controls.Controls.Add(Me.btnStop)
        Me.pnlRignt_Controls.Controls.Add(Me.btnPlayPause)
        Me.pnlRignt_Controls.Controls.Add(Me.tb_SeekBar)
        Me.pnlRignt_Controls.Controls.Add(Me.lblDuration)
        Me.pnlRignt_Controls.Controls.Add(Me.Label17)
        Me.pnlRignt_Controls.Controls.Add(Me.Label20)
        Me.pnlRignt_Controls.Controls.Add(Me.Label19)
        Me.pnlRignt_Controls.Controls.Add(Me.Label18)
        Me.pnlRignt_Controls.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlRignt_Controls.Location = New System.Drawing.Point(0, 442)
        Me.pnlRignt_Controls.Name = "pnlRignt_Controls"
        Me.pnlRignt_Controls.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlRignt_Controls.Size = New System.Drawing.Size(564, 79)
        Me.pnlRignt_Controls.TabIndex = 0
        '
        'pic_Play
        '
        Me.pic_Play.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pic_Play.Image = CType(resources.GetObject("pic_Play.Image"), System.Drawing.Image)
        Me.pic_Play.Location = New System.Drawing.Point(33, 55)
        Me.pic_Play.Name = "pic_Play"
        Me.pic_Play.Size = New System.Drawing.Size(16, 16)
        Me.pic_Play.TabIndex = 26
        Me.pic_Play.TabStop = False
        Me.tooltpPause.SetToolTip(Me.pic_Play, "Play")
        Me.pic_Play.Visible = False
        '
        'pic_Pause
        '
        Me.pic_Pause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pic_Pause.Image = CType(resources.GetObject("pic_Pause.Image"), System.Drawing.Image)
        Me.pic_Pause.Location = New System.Drawing.Point(9, 55)
        Me.pic_Pause.Name = "pic_Pause"
        Me.pic_Pause.Size = New System.Drawing.Size(16, 16)
        Me.pic_Pause.TabIndex = 25
        Me.pic_Pause.TabStop = False
        Me.tooltpPause.SetToolTip(Me.pic_Pause, "Pause")
        Me.pic_Pause.Visible = False
        '
        'lblCurrentDuration
        '
        Me.lblCurrentDuration.AutoSize = True
        Me.lblCurrentDuration.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblCurrentDuration.Location = New System.Drawing.Point(433, 1)
        Me.lblCurrentDuration.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCurrentDuration.Name = "lblCurrentDuration"
        Me.lblCurrentDuration.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lblCurrentDuration.Size = New System.Drawing.Size(57, 19)
        Me.lblCurrentDuration.TabIndex = 23
        Me.lblCurrentDuration.Text = "00:00:00"
        Me.lblCurrentDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpenFile.BackgroundImage = CType(resources.GetObject("btnOpenFile.BackgroundImage"), System.Drawing.Image)
        Me.btnOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnOpenFile.Location = New System.Drawing.Point(146, 8)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(23, 24)
        Me.btnOpenFile.TabIndex = 22
        Me.btnOpenFile.UseVisualStyleBackColor = True
        Me.btnOpenFile.Visible = False
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnStop.BackgroundImage = CType(resources.GetObject("btnStop.BackgroundImage"), System.Drawing.Image)
        Me.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnStop.FlatAppearance.BorderSize = 0
        Me.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Location = New System.Drawing.Point(7, 4)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(21, 22)
        Me.btnStop.TabIndex = 18
        Me.tooltpPause.SetToolTip(Me.btnStop, "Stop")
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnPlayPause
        '
        Me.btnPlayPause.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPlayPause.BackgroundImage = CType(resources.GetObject("btnPlayPause.BackgroundImage"), System.Drawing.Image)
        Me.btnPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnPlayPause.FlatAppearance.BorderSize = 0
        Me.btnPlayPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPlayPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlayPause.Location = New System.Drawing.Point(31, 4)
        Me.btnPlayPause.Name = "btnPlayPause"
        Me.btnPlayPause.Size = New System.Drawing.Size(21, 22)
        Me.btnPlayPause.TabIndex = 17
        Me.btnPlayPause.Tag = "Pause"
        Me.tooltpPause.SetToolTip(Me.btnPlayPause, "Paly")
        Me.btnPlayPause.UseVisualStyleBackColor = True
        '
        'tb_SeekBar
        '
        Me.tb_SeekBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tb_SeekBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_SeekBar.Location = New System.Drawing.Point(11, 28)
        Me.tb_SeekBar.Margin = New System.Windows.Forms.Padding(1)
        Me.tb_SeekBar.Maximum = 100
        Me.tb_SeekBar.Name = "tb_SeekBar"
        Me.tb_SeekBar.Size = New System.Drawing.Size(528, 45)
        Me.tb_SeekBar.TabIndex = 21
        Me.tb_SeekBar.TickFrequency = 0
        Me.tb_SeekBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = True
        Me.lblDuration.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblDuration.Location = New System.Drawing.Point(490, 1)
        Me.lblDuration.Margin = New System.Windows.Forms.Padding(3)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lblDuration.Size = New System.Drawing.Size(70, 19)
        Me.lblDuration.TabIndex = 27
        Me.lblDuration.Text = "/ 00:00:00 "
        Me.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 75)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(559, 1)
        Me.Label17.TabIndex = 31
        Me.Label17.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(559, 1)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "label1"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(560, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 76)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 76)
        Me.Label18.TabIndex = 30
        Me.Label18.Text = "label4"
        '
        'openfiledilogVideo
        '
        Me.openfiledilogVideo.FileName = "OpenFileDialog1"
        '
        'tooltpPause
        '
        Me.tooltpPause.Tag = ""
        '
        'Timer1
        '
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.Img_Reviwed)
        Me.pnl_tlsp_Top.Controls.Add(Me.tls_DVDMaintain)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(743, 55)
        Me.pnl_tlsp_Top.TabIndex = 13
        '
        'Img_Reviwed
        '
        Me.Img_Reviwed.Image = CType(resources.GetObject("Img_Reviwed.Image"), System.Drawing.Image)
        Me.Img_Reviwed.Location = New System.Drawing.Point(704, 12)
        Me.Img_Reviwed.Name = "Img_Reviwed"
        Me.Img_Reviwed.Size = New System.Drawing.Size(14, 27)
        Me.Img_Reviwed.TabIndex = 22
        Me.Img_Reviwed.TabStop = False
        Me.Img_Reviwed.Visible = False
        '
        'tls_DVDMaintain
        '
        Me.tls_DVDMaintain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_DVDMaintain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_DVDMaintain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_DVDMaintain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_DVDMaintain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_DVDMaintain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_DVDMaintain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnViewAcknowlegment, Me.ts_btnAcknowlegment, Me.ts_btnClose})
        Me.tls_DVDMaintain.Location = New System.Drawing.Point(0, 0)
        Me.tls_DVDMaintain.Name = "tls_DVDMaintain"
        Me.tls_DVDMaintain.Size = New System.Drawing.Size(743, 53)
        Me.tls_DVDMaintain.TabIndex = 0
        Me.tls_DVDMaintain.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(91, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&Upload Video"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnViewAcknowlegment
        '
        Me.ts_btnViewAcknowlegment.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnViewAcknowlegment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnViewAcknowlegment.Image = CType(resources.GetObject("ts_btnViewAcknowlegment.Image"), System.Drawing.Image)
        Me.ts_btnViewAcknowlegment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnViewAcknowlegment.Name = "ts_btnViewAcknowlegment"
        Me.ts_btnViewAcknowlegment.Size = New System.Drawing.Size(151, 50)
        Me.ts_btnViewAcknowlegment.Tag = "Modify"
        Me.ts_btnViewAcknowlegment.Text = "&View Acknowledgment"
        Me.ts_btnViewAcknowlegment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnViewAcknowlegment.Visible = False
        '
        'ts_btnAcknowlegment
        '
        Me.ts_btnAcknowlegment.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAcknowlegment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAcknowlegment.Image = CType(resources.GetObject("ts_btnAcknowlegment.Image"), System.Drawing.Image)
        Me.ts_btnAcknowlegment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAcknowlegment.Name = "ts_btnAcknowlegment"
        Me.ts_btnAcknowlegment.Size = New System.Drawing.Size(118, 50)
        Me.ts_btnAcknowlegment.Tag = "Delete"
        Me.ts_btnAcknowlegment.Text = "&Acknowledgment"
        Me.ts_btnAcknowlegment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnAcknowlegment.Visible = False
        '
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDVD_MaintainVideo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(743, 576)
        Me.Controls.Add(Me.sc_Main)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmDVD_MaintainVideo"
        Me.ShowInTaskbar = False
        Me.Text = "Upload Video"
        Me.sc_Main.Panel1.ResumeLayout(False)
        Me.sc_Main.Panel2.ResumeLayout(False)
        Me.sc_Main.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.c1CategorisedDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnlUploadBtn.ResumeLayout(False)
        Me.pnlRightMain.ResumeLayout(False)
        CType(Me.WMPlayer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlRight_TOP.ResumeLayout(False)
        Me.pnlRignt_Controls.ResumeLayout(False)
        Me.pnlRignt_Controls.PerformLayout()
        CType(Me.pic_Play, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Pause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tb_SeekBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tls_DVDMaintain.ResumeLayout(False)
        Me.tls_DVDMaintain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sc_Main As System.Windows.Forms.SplitContainer
    Friend WithEvents c1CategorisedDocuments As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlRightMain As System.Windows.Forms.Panel
    Friend WithEvents pnlRignt_Controls As System.Windows.Forms.Panel
    Friend WithEvents pnlRight_TOP As System.Windows.Forms.Panel
    Friend WithEvents pic_Play As System.Windows.Forms.PictureBox
    Friend WithEvents pic_Pause As System.Windows.Forms.PictureBox
    Friend WithEvents lblCurrentDuration As System.Windows.Forms.Label
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnPlayPause As System.Windows.Forms.Button
    Private WithEvents tb_SeekBar As System.Windows.Forms.TrackBar
    Friend WithEvents lblDuration As System.Windows.Forms.Label
    Friend WithEvents WMPlayer As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents pnlUploadBtn As System.Windows.Forms.Panel
    Friend WithEvents btnUploadVideo As System.Windows.Forms.Button
    Friend WithEvents openfiledilogVideo As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tooltpPause As System.Windows.Forms.ToolTip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblMediaName As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tls_DVDMaintain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnViewAcknowlegment As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnAcknowlegment As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Public WithEvents Img_Reviwed As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
