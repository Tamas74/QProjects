<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewEligiblityInformation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewEligiblityInformation))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtnSaveCls = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.rtfEligiliblityinfo = New System.Windows.Forms.RichTextBox()
        Me.pnlTextBox = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.C1271demoInfo = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPatientdemoinfo = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnl72HrsMessage = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lbl72HrsMessage = New System.Windows.Forms.Label()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlTextBox.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1271demoInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnl72HrsMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlToolStrip.Size = New System.Drawing.Size(794, 54)
        Me.pnlToolStrip.TabIndex = 3
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSaveCls, Me.tlsbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 3)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(794, 51)
        Me.ts_ViewButtons.TabIndex = 2
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlsbtnSaveCls
        '
        Me.tlsbtnSaveCls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSaveCls.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnSaveCls.Image = CType(resources.GetObject("tlsbtnSaveCls.Image"), System.Drawing.Image)
        Me.tlsbtnSaveCls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSaveCls.Name = "tlsbtnSaveCls"
        Me.tlsbtnSaveCls.Size = New System.Drawing.Size(66, 48)
        Me.tlsbtnSaveCls.Tag = "SaveandClose"
        Me.tlsbtnSaveCls.Text = "&Save&&Cls"
        Me.tlsbtnSaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSaveCls.ToolTipText = "Save and Close"
        Me.tlsbtnSaveCls.Visible = False
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 48)
        Me.tlsbtnClose.Tag = "Close"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'rtfEligiliblityinfo
        '
        Me.rtfEligiliblityinfo.BackColor = System.Drawing.Color.White
        Me.rtfEligiliblityinfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtfEligiliblityinfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtfEligiliblityinfo.ForeColor = System.Drawing.Color.Black
        Me.rtfEligiliblityinfo.Location = New System.Drawing.Point(8, 9)
        Me.rtfEligiliblityinfo.Name = "rtfEligiliblityinfo"
        Me.rtfEligiliblityinfo.ReadOnly = True
        Me.rtfEligiliblityinfo.Size = New System.Drawing.Size(782, 646)
        Me.rtfEligiliblityinfo.TabIndex = 4
        Me.rtfEligiliblityinfo.Text = ""
        Me.rtfEligiliblityinfo.Visible = False
        '
        'pnlTextBox
        '
        Me.pnlTextBox.AutoScroll = True
        Me.pnlTextBox.Controls.Add(Me.WebBrowser1)
        Me.pnlTextBox.Controls.Add(Me.rtfEligiliblityinfo)
        Me.pnlTextBox.Controls.Add(Me.Label15)
        Me.pnlTextBox.Controls.Add(Me.Label10)
        Me.pnlTextBox.Controls.Add(Me.Label4)
        Me.pnlTextBox.Controls.Add(Me.Label3)
        Me.pnlTextBox.Controls.Add(Me.Label2)
        Me.pnlTextBox.Controls.Add(Me.Label1)
        Me.pnlTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTextBox.Location = New System.Drawing.Point(0, 84)
        Me.pnlTextBox.Name = "pnlTextBox"
        Me.pnlTextBox.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlTextBox.Size = New System.Drawing.Size(794, 656)
        Me.pnlTextBox.TabIndex = 5
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(8, 9)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(782, 646)
        Me.WebBrowser1.TabIndex = 11
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.White
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(8, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(782, 5)
        Me.Label15.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(4, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(4, 651)
        Me.Label10.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(790, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 651)
        Me.Label4.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 651)
        Me.Label3.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 655)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(788, 1)
        Me.Label2.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(788, 1)
        Me.Label1.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.C1271demoInfo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(794, 656)
        Me.Panel1.TabIndex = 24
        Me.Panel1.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(790, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 651)
        Me.Label11.TabIndex = 29
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 651)
        Me.Label12.TabIndex = 28
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(3, 652)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(788, 1)
        Me.Label13.TabIndex = 27
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(788, 1)
        Me.Label14.TabIndex = 26
        '
        'C1271demoInfo
        '
        Me.C1271demoInfo.AllowDelete = True
        Me.C1271demoInfo.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1271demoInfo.AllowEditing = False
        Me.C1271demoInfo.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1271demoInfo.BackColor = System.Drawing.Color.White
        Me.C1271demoInfo.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1271demoInfo.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1271demoInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1271demoInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1271demoInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1271demoInfo.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1271demoInfo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1271demoInfo.Location = New System.Drawing.Point(3, 0)
        Me.C1271demoInfo.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1271demoInfo.Name = "C1271demoInfo"
        Me.C1271demoInfo.Rows.Count = 1
        Me.C1271demoInfo.Rows.DefaultSize = 19
        Me.C1271demoInfo.Rows.Fixed = 0
        Me.C1271demoInfo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1271demoInfo.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1271demoInfo.ShowCellLabels = True
        Me.C1271demoInfo.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1271demoInfo.Size = New System.Drawing.Size(788, 653)
        Me.C1271demoInfo.StyleInfo = resources.GetString("C1271demoInfo.StyleInfo")
        Me.C1271demoInfo.TabIndex = 24
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.lblPatientdemoinfo)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(788, 27)
        Me.Panel2.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(786, 25)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "  Changed Demographic Information :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(787, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 25)
        Me.Label6.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(0, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(788, 1)
        Me.Label8.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(788, 1)
        Me.Label9.TabIndex = 10
        '
        'lblPatientdemoinfo
        '
        Me.lblPatientdemoinfo.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientdemoinfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPatientdemoinfo.Location = New System.Drawing.Point(0, 0)
        Me.lblPatientdemoinfo.Name = "lblPatientdemoinfo"
        Me.lblPatientdemoinfo.Size = New System.Drawing.Size(788, 27)
        Me.lblPatientdemoinfo.TabIndex = 0
        Me.lblPatientdemoinfo.Text = "Changed Demographic Information"
        Me.lblPatientdemoinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 57)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel3.Size = New System.Drawing.Size(794, 27)
        Me.Panel3.TabIndex = 26
        Me.Panel3.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(794, 3)
        Me.Splitter1.TabIndex = 27
        Me.Splitter1.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnl72HrsMessage
        '
        Me.pnl72HrsMessage.Controls.Add(Me.Label19)
        Me.pnl72HrsMessage.Controls.Add(Me.Label18)
        Me.pnl72HrsMessage.Controls.Add(Me.Label17)
        Me.pnl72HrsMessage.Controls.Add(Me.Label16)
        Me.pnl72HrsMessage.Controls.Add(Me.lbl72HrsMessage)
        Me.pnl72HrsMessage.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl72HrsMessage.Location = New System.Drawing.Point(0, 740)
        Me.pnl72HrsMessage.Name = "pnl72HrsMessage"
        Me.pnl72HrsMessage.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl72HrsMessage.Size = New System.Drawing.Size(794, 32)
        Me.pnl72HrsMessage.TabIndex = 30
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(4, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(786, 1)
        Me.Label19.TabIndex = 13
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Location = New System.Drawing.Point(4, 28)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(786, 1)
        Me.Label18.TabIndex = 12
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Location = New System.Drawing.Point(790, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 26)
        Me.Label17.TabIndex = 9
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 26)
        Me.Label16.TabIndex = 8
        '
        'lbl72HrsMessage
        '
        Me.lbl72HrsMessage.AutoSize = True
        Me.lbl72HrsMessage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl72HrsMessage.Location = New System.Drawing.Point(21, 10)
        Me.lbl72HrsMessage.Name = "lbl72HrsMessage"
        Me.lbl72HrsMessage.Size = New System.Drawing.Size(90, 13)
        Me.lbl72HrsMessage.TabIndex = 0
        Me.lbl72HrsMessage.Text = "72HrsMessage"
        '
        'frmViewEligiblityInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(794, 772)
        Me.Controls.Add(Me.pnlTextBox)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl72HrsMessage)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmViewEligiblityInformation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Eligibility Information"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlTextBox.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1271demoInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnl72HrsMessage.ResumeLayout(False)
        Me.pnl72HrsMessage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnSaveCls As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents rtfEligiliblityinfo As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlTextBox As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1271demoInfo As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblPatientdemoinfo As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents pnl72HrsMessage As System.Windows.Forms.Panel
    Friend WithEvents lbl72HrsMessage As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
