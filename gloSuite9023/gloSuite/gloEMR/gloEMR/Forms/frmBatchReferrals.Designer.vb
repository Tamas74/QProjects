<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatchReferrals
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DTPTo, DTPFrom, DTPFromLog, DTPTOLog}
                    Dim cntControls() As System.Windows.Forms.Control = {DTPTo, DTPFrom, DTPFromLog, DTPTOLog}

                    components.Dispose()
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                    Catch
                    End Try


                    If (IsNothing(dtpControls) = False) Then
                        If dtpControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                        End If
                    End If


                    If (IsNothing(cntControls) = False) Then
                        If cntControls.Length > 0 Then
                            gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                        End If
                    End If

                Catch
                End Try

                


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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatchReferrals))
        Me.pnlmain = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.C1ExamDetails = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.pnlProgress = New System.Windows.Forms.Panel
        Me.pnlProgress1 = New System.Windows.Forms.Panel
        Me.PrgBarPrintFax = New System.Windows.Forms.ProgressBar
        Me.pnlgrid = New System.Windows.Forms.Panel
        Me.Panel21 = New System.Windows.Forms.Panel
        Me.C1ShowLog = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Panel20 = New System.Windows.Forms.Panel
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.btnLogSelect = New System.Windows.Forms.Button
        Me.btnLogCLear = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.DTPTOLog = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.DTPFromLog = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ToolStrip2 = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton
        Me.tlb_Close = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel19 = New System.Windows.Forms.Panel
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.pnlstrip = New System.Windows.Forms.Panel
        Me.pnlstrip1 = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.btnViewlog = New System.Windows.Forms.Button
        Me.pnlShowLog1 = New System.Windows.Forms.Panel
        Me.rbNone = New System.Windows.Forms.RadioButton
        Me.rbSelect = New System.Windows.Forms.RadioButton
        Me.rbNotes = New System.Windows.Forms.RadioButton
        Me.Label14 = New System.Windows.Forms.Label
        Me.cmbTemplate = New System.Windows.Forms.ComboBox
        Me.lblTemplate = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.pnlShowLog = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.cmbProvider = New System.Windows.Forms.ComboBox
        Me.lblProvider = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.cmbExamtype = New System.Windows.Forms.ComboBox
        Me.lblcmbType = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.DTPFrom = New System.Windows.Forms.DateTimePicker
        Me.lblto = New System.Windows.Forms.Label
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.DTPTo = New System.Windows.Forms.DateTimePicker
        Me.lblFrom = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel17 = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.tsb_SelectContactFax = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.tsb_Viewlogs = New System.Windows.Forms.ToolStripButton
        Me.tsb_Hidelogs = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlmain.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.C1ExamDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProgress.SuspendLayout()
        Me.pnlProgress1.SuspendLayout()
        Me.pnlgrid.SuspendLayout()
        Me.Panel21.SuspendLayout()
        CType(Me.C1ShowLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel20.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.pnlstrip.SuspendLayout()
        Me.pnlstrip1.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlShowLog.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlmain
        '
        Me.pnlmain.Controls.Add(Me.Panel7)
        Me.pnlmain.Controls.Add(Me.pnlProgress)
        Me.pnlmain.Controls.Add(Me.pnlgrid)
        Me.pnlmain.Controls.Add(Me.pnlstrip)
        Me.pnlmain.Controls.Add(Me.pnlShowLog)
        Me.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlmain.Location = New System.Drawing.Point(0, 54)
        Me.pnlmain.Name = "pnlmain"
        Me.pnlmain.Size = New System.Drawing.Size(889, 647)
        Me.pnlmain.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label15)
        Me.Panel7.Controls.Add(Me.C1ExamDetails)
        Me.Panel7.Controls.Add(Me.Label16)
        Me.Panel7.Controls.Add(Me.Label17)
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 59)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel7.Size = New System.Drawing.Size(889, 252)
        Me.Panel7.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(4, 251)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(881, 1)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "label2"
        '
        'C1ExamDetails
        '
        Me.C1ExamDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1ExamDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1ExamDetails.BackColor = System.Drawing.Color.GhostWhite
        Me.C1ExamDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ExamDetails.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1ExamDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ExamDetails.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ExamDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1ExamDetails.Location = New System.Drawing.Point(4, 1)
        Me.C1ExamDetails.Name = "C1ExamDetails"
        Me.C1ExamDetails.Rows.Count = 1
        Me.C1ExamDetails.Rows.DefaultSize = 19
        Me.C1ExamDetails.Rows.Fixed = 0
        Me.C1ExamDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ExamDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1ExamDetails.ShowSort = False
        Me.C1ExamDetails.Size = New System.Drawing.Size(881, 251)
        Me.C1ExamDetails.StyleInfo = resources.GetString("C1ExamDetails.StyleInfo")
        Me.C1ExamDetails.TabIndex = 0
        Me.C1ExamDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1ExamDetails.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1ExamDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1ExamDetails.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 251)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(885, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 251)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(883, 1)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "label1"
        '
        'pnlProgress
        '
        Me.pnlProgress.Controls.Add(Me.pnlProgress1)
        Me.pnlProgress.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlProgress.Location = New System.Drawing.Point(0, 311)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlProgress.Size = New System.Drawing.Size(889, 24)
        Me.pnlProgress.TabIndex = 3
        Me.pnlProgress.Visible = False
        '
        'pnlProgress1
        '
        Me.pnlProgress1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlProgress1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProgress1.Controls.Add(Me.PrgBarPrintFax)
        Me.pnlProgress1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProgress1.Location = New System.Drawing.Point(3, 3)
        Me.pnlProgress1.Name = "pnlProgress1"
        Me.pnlProgress1.Size = New System.Drawing.Size(883, 18)
        Me.pnlProgress1.TabIndex = 11
        '
        'PrgBarPrintFax
        '
        Me.PrgBarPrintFax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrgBarPrintFax.ForeColor = System.Drawing.Color.LimeGreen
        Me.PrgBarPrintFax.Location = New System.Drawing.Point(0, 0)
        Me.PrgBarPrintFax.Name = "PrgBarPrintFax"
        Me.PrgBarPrintFax.Size = New System.Drawing.Size(883, 18)
        Me.PrgBarPrintFax.Step = 1
        Me.PrgBarPrintFax.TabIndex = 0
        '
        'pnlgrid
        '
        Me.pnlgrid.Controls.Add(Me.Panel21)
        Me.pnlgrid.Controls.Add(Me.Panel20)
        Me.pnlgrid.Controls.Add(Me.Panel1)
        Me.pnlgrid.Controls.Add(Me.Panel2)
        Me.pnlgrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlgrid.Location = New System.Drawing.Point(0, 335)
        Me.pnlgrid.Name = "pnlgrid"
        Me.pnlgrid.Size = New System.Drawing.Size(889, 312)
        Me.pnlgrid.TabIndex = 4
        Me.pnlgrid.Visible = False
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.C1ShowLog)
        Me.Panel21.Controls.Add(Me.Label32)
        Me.Panel21.Controls.Add(Me.Label33)
        Me.Panel21.Controls.Add(Me.Label34)
        Me.Panel21.Controls.Add(Me.Label35)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 115)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel21.Size = New System.Drawing.Size(889, 197)
        Me.Panel21.TabIndex = 1
        '
        'C1ShowLog
        '
        Me.C1ShowLog.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1ShowLog.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1ShowLog.BackColor = System.Drawing.Color.GhostWhite
        Me.C1ShowLog.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ShowLog.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1ShowLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ShowLog.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ShowLog.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1ShowLog.Location = New System.Drawing.Point(4, 1)
        Me.C1ShowLog.Name = "C1ShowLog"
        Me.C1ShowLog.Rows.Count = 1
        Me.C1ShowLog.Rows.DefaultSize = 19
        Me.C1ShowLog.Rows.Fixed = 0
        Me.C1ShowLog.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ShowLog.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1ShowLog.ShowSort = False
        Me.C1ShowLog.Size = New System.Drawing.Size(881, 192)
        Me.C1ShowLog.StyleInfo = resources.GetString("C1ShowLog.StyleInfo")
        Me.C1ShowLog.TabIndex = 0
        Me.C1ShowLog.Tree.NodeImageCollapsed = CType(resources.GetObject("C1ShowLog.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1ShowLog.Tree.NodeImageExpanded = CType(resources.GetObject("C1ShowLog.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(4, 193)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(881, 1)
        Me.Label32.TabIndex = 16
        Me.Label32.Text = "label2"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(3, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 193)
        Me.Label33.TabIndex = 15
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(885, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 193)
        Me.Label34.TabIndex = 14
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(3, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(883, 1)
        Me.Label35.TabIndex = 13
        Me.Label35.Text = "label1"
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Panel16)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 85)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel20.Size = New System.Drawing.Size(889, 30)
        Me.Panel20.TabIndex = 0
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Panel15)
        Me.Panel16.Controls.Add(Me.Panel13)
        Me.Panel16.Controls.Add(Me.Label2)
        Me.Panel16.Controls.Add(Me.Panel14)
        Me.Panel16.Controls.Add(Me.Label1)
        Me.Panel16.Controls.Add(Me.Label23)
        Me.Panel16.Controls.Add(Me.Label24)
        Me.Panel16.Controls.Add(Me.Label25)
        Me.Panel16.Controls.Add(Me.Label26)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(3, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(883, 27)
        Me.Panel16.TabIndex = 0
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.Controls.Add(Me.btnLogSelect)
        Me.Panel15.Controls.Add(Me.btnLogCLear)
        Me.Panel15.Controls.Add(Me.btnDelete)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel15.Location = New System.Drawing.Point(638, 1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(244, 25)
        Me.Panel15.TabIndex = 2
        '
        'btnLogSelect
        '
        Me.btnLogSelect.BackColor = System.Drawing.Color.Transparent
        Me.btnLogSelect.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLogSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLogSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLogSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogSelect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogSelect.Location = New System.Drawing.Point(4, 0)
        Me.btnLogSelect.Name = "btnLogSelect"
        Me.btnLogSelect.Size = New System.Drawing.Size(80, 25)
        Me.btnLogSelect.TabIndex = 0
        Me.btnLogSelect.Text = "Select All"
        Me.btnLogSelect.UseVisualStyleBackColor = False
        Me.btnLogSelect.Visible = False
        '
        'btnLogCLear
        '
        Me.btnLogCLear.BackColor = System.Drawing.Color.Transparent
        Me.btnLogCLear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLogCLear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLogCLear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLogCLear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogCLear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogCLear.Location = New System.Drawing.Point(84, 0)
        Me.btnLogCLear.Name = "btnLogCLear"
        Me.btnLogCLear.Size = New System.Drawing.Size(80, 25)
        Me.btnLogCLear.TabIndex = 1
        Me.btnLogCLear.Text = "Clear All"
        Me.btnLogCLear.UseVisualStyleBackColor = False
        Me.btnLogCLear.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnDelete.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(164, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 25)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnDelete.Visible = False
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Transparent
        Me.Panel13.Controls.Add(Me.DTPTOLog)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel13.Location = New System.Drawing.Point(195, 1)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(102, 25)
        Me.Panel13.TabIndex = 1
        '
        'DTPTOLog
        '
        Me.DTPTOLog.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DTPTOLog.CalendarMonthBackground = System.Drawing.Color.White
        Me.DTPTOLog.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DTPTOLog.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DTPTOLog.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DTPTOLog.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPTOLog.Location = New System.Drawing.Point(3, 2)
        Me.DTPTOLog.Name = "DTPTOLog"
        Me.DTPTOLog.Size = New System.Drawing.Size(95, 22)
        Me.DTPTOLog.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(165, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 25)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.Controls.Add(Me.DTPFromLog)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(59, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(106, 25)
        Me.Panel14.TabIndex = 0
        '
        'DTPFromLog
        '
        Me.DTPFromLog.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DTPFromLog.CalendarMonthBackground = System.Drawing.Color.White
        Me.DTPFromLog.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DTPFromLog.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DTPFromLog.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DTPFromLog.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFromLog.Location = New System.Drawing.Point(3, 2)
        Me.DTPFromLog.Name = "DTPFromLog"
        Me.DTPFromLog.Size = New System.Drawing.Size(100, 22)
        Me.DTPFromLog.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 25)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "From "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 26)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(881, 1)
        Me.Label23.TabIndex = 25
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 26)
        Me.Label24.TabIndex = 24
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(882, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 26)
        Me.Label25.TabIndex = 23
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(883, 1)
        Me.Label26.TabIndex = 22
        Me.Label26.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ToolStrip2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(889, 56)
        Me.Panel1.TabIndex = 24
        '
        'ToolStrip2
        '
        Me.ToolStrip2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripButton8, Me.tlb_Close})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(889, 53)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(67, 50)
        Me.ToolStripButton6.Tag = "Selectall"
        Me.ToolStripButton6.Text = "&Select All"
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(60, 50)
        Me.ToolStripButton7.Tag = "Clear"
        Me.ToolStripButton7.Text = "&Clear All"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton7.Visible = False
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton8.Tag = "Close"
        Me.ToolStripButton8.Text = "&Delete"
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Close
        '
        Me.tlb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Close.Image = CType(resources.GetObject("tlb_Close.Image"), System.Drawing.Image)
        Me.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Close.Name = "tlb_Close"
        Me.tlb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Close.Tag = "Close"
        Me.tlb_Close.Text = "&Close"
        Me.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(889, 29)
        Me.Panel2.TabIndex = 21
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.Transparent
        Me.Panel19.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Label28)
        Me.Panel19.Controls.Add(Me.Label4)
        Me.Panel19.Controls.Add(Me.Label29)
        Me.Panel19.Controls.Add(Me.Label30)
        Me.Panel19.Controls.Add(Me.Label31)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.Location = New System.Drawing.Point(3, 3)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(883, 23)
        Me.Panel19.TabIndex = 19
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(1, 22)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(881, 1)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(881, 22)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "    View Logs"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 22)
        Me.Label29.TabIndex = 7
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(882, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 22)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(883, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'pnlstrip
        '
        Me.pnlstrip.Controls.Add(Me.pnlstrip1)
        Me.pnlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlstrip.Location = New System.Drawing.Point(0, 32)
        Me.pnlstrip.Name = "pnlstrip"
        Me.pnlstrip.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlstrip.Size = New System.Drawing.Size(889, 27)
        Me.pnlstrip.TabIndex = 1
        '
        'pnlstrip1
        '
        Me.pnlstrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlstrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlstrip1.Controls.Add(Me.Panel8)
        Me.pnlstrip1.Controls.Add(Me.lblTemplate)
        Me.pnlstrip1.Controls.Add(Me.Label10)
        Me.pnlstrip1.Controls.Add(Me.Label11)
        Me.pnlstrip1.Controls.Add(Me.Label12)
        Me.pnlstrip1.Controls.Add(Me.Label13)
        Me.pnlstrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlstrip1.Location = New System.Drawing.Point(3, 0)
        Me.pnlstrip1.Name = "pnlstrip1"
        Me.pnlstrip1.Size = New System.Drawing.Size(883, 24)
        Me.pnlstrip1.TabIndex = 7
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Controls.Add(Me.btnViewlog)
        Me.Panel8.Controls.Add(Me.pnlShowLog1)
        Me.Panel8.Controls.Add(Me.rbNone)
        Me.Panel8.Controls.Add(Me.rbSelect)
        Me.Panel8.Controls.Add(Me.rbNotes)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.cmbTemplate)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(130, 1)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(752, 22)
        Me.Panel8.TabIndex = 14
        '
        'btnViewlog
        '
        Me.btnViewlog.BackColor = System.Drawing.Color.Transparent
        Me.btnViewlog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnViewlog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnViewlog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnViewlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewlog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewlog.Location = New System.Drawing.Point(630, -2)
        Me.btnViewlog.Name = "btnViewlog"
        Me.btnViewlog.Size = New System.Drawing.Size(91, 27)
        Me.btnViewlog.TabIndex = 4
        Me.btnViewlog.Text = "&View Log"
        Me.btnViewlog.UseVisualStyleBackColor = False
        '
        'pnlShowLog1
        '
        Me.pnlShowLog1.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlShowLog1.Location = New System.Drawing.Point(721, 0)
        Me.pnlShowLog1.Name = "pnlShowLog1"
        Me.pnlShowLog1.Size = New System.Drawing.Size(31, 22)
        Me.pnlShowLog1.TabIndex = 30
        Me.pnlShowLog1.Visible = False
        '
        'rbNone
        '
        Me.rbNone.AutoSize = True
        Me.rbNone.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbNone.Location = New System.Drawing.Point(487, 0)
        Me.rbNone.Name = "rbNone"
        Me.rbNone.Size = New System.Drawing.Size(54, 22)
        Me.rbNone.TabIndex = 3
        Me.rbNone.Text = "None"
        Me.rbNone.UseVisualStyleBackColor = True
        '
        'rbSelect
        '
        Me.rbSelect.AutoSize = True
        Me.rbSelect.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbSelect.Location = New System.Drawing.Point(352, 0)
        Me.rbSelect.Name = "rbSelect"
        Me.rbSelect.Size = New System.Drawing.Size(135, 22)
        Me.rbSelect.TabIndex = 2
        Me.rbSelect.Text = "Add Selected Notes"
        Me.rbSelect.UseVisualStyleBackColor = True
        '
        'rbNotes
        '
        Me.rbNotes.AutoSize = True
        Me.rbNotes.Checked = True
        Me.rbNotes.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbNotes.Location = New System.Drawing.Point(201, 0)
        Me.rbNotes.Name = "rbNotes"
        Me.rbNotes.Size = New System.Drawing.Size(151, 22)
        Me.rbNotes.TabIndex = 1
        Me.rbNotes.TabStop = True
        Me.rbNotes.Text = "Add Complete Notes"
        Me.rbNotes.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(181, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 22)
        Me.Label14.TabIndex = 29
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTemplate
        '
        Me.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbTemplate.FormattingEnabled = True
        Me.cmbTemplate.Location = New System.Drawing.Point(0, 0)
        Me.cmbTemplate.Name = "cmbTemplate"
        Me.cmbTemplate.Size = New System.Drawing.Size(181, 22)
        Me.cmbTemplate.TabIndex = 0
        '
        'lblTemplate
        '
        Me.lblTemplate.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplate.Location = New System.Drawing.Point(1, 1)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(129, 22)
        Me.lblTemplate.TabIndex = 10
        Me.lblTemplate.Text = "  Select Template :"
        Me.lblTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(881, 1)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(882, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(883, 1)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "label1"
        '
        'pnlShowLog
        '
        Me.pnlShowLog.Controls.Add(Me.Panel4)
        Me.pnlShowLog.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlShowLog.Location = New System.Drawing.Point(0, 0)
        Me.pnlShowLog.Name = "pnlShowLog"
        Me.pnlShowLog.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlShowLog.Size = New System.Drawing.Size(889, 32)
        Me.pnlShowLog.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel12)
        Me.Panel4.Controls.Add(Me.lblProvider)
        Me.Panel4.Controls.Add(Me.Panel11)
        Me.Panel4.Controls.Add(Me.lblcmbType)
        Me.Panel4.Controls.Add(Me.Panel9)
        Me.Panel4.Controls.Add(Me.lblto)
        Me.Panel4.Controls.Add(Me.Panel10)
        Me.Panel4.Controls.Add(Me.lblFrom)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(883, 26)
        Me.Panel4.TabIndex = 12
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.cmbProvider)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel12.Location = New System.Drawing.Point(693, 1)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(223, 24)
        Me.Panel12.TabIndex = 13
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(3, 1)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(220, 22)
        Me.cmbProvider.TabIndex = 3
        '
        'lblProvider
        '
        Me.lblProvider.BackColor = System.Drawing.Color.Transparent
        Me.lblProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvider.Location = New System.Drawing.Point(581, 1)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(112, 24)
        Me.lblProvider.TabIndex = 7
        Me.lblProvider.Text = "Select Provider :"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.cmbExamtype)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel11.Location = New System.Drawing.Point(444, 1)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(137, 24)
        Me.Panel11.TabIndex = 12
        '
        'cmbExamtype
        '
        Me.cmbExamtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExamtype.ForeColor = System.Drawing.Color.Black
        Me.cmbExamtype.FormattingEnabled = True
        Me.cmbExamtype.Location = New System.Drawing.Point(3, 1)
        Me.cmbExamtype.Name = "cmbExamtype"
        Me.cmbExamtype.Size = New System.Drawing.Size(132, 22)
        Me.cmbExamtype.TabIndex = 2
        '
        'lblcmbType
        '
        Me.lblcmbType.BackColor = System.Drawing.Color.Transparent
        Me.lblcmbType.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblcmbType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcmbType.Location = New System.Drawing.Point(341, 1)
        Me.lblcmbType.Name = "lblcmbType"
        Me.lblcmbType.Size = New System.Drawing.Size(103, 24)
        Me.lblcmbType.TabIndex = 6
        Me.lblcmbType.Text = "Select Exam :"
        Me.lblcmbType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.DTPFrom)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(241, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(100, 24)
        Me.Panel9.TabIndex = 11
        '
        'DTPFrom
        '
        Me.DTPFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DTPFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.DTPFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DTPFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DTPFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPFrom.Location = New System.Drawing.Point(2, 1)
        Me.DTPFrom.Name = "DTPFrom"
        Me.DTPFrom.Size = New System.Drawing.Size(98, 22)
        Me.DTPFrom.TabIndex = 0
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblto.Location = New System.Drawing.Point(206, 1)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(35, 24)
        Me.lblto.TabIndex = 2
        Me.lblto.Text = "To"
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.DTPTo)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel10.Location = New System.Drawing.Point(109, 1)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(97, 24)
        Me.Panel10.TabIndex = 10
        '
        'DTPTo
        '
        Me.DTPTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DTPTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.DTPTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DTPTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DTPTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DTPTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPTo.Location = New System.Drawing.Point(1, 1)
        Me.DTPTo.Name = "DTPTo"
        Me.DTPTo.Size = New System.Drawing.Size(94, 22)
        Me.DTPTo.TabIndex = 0
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(59, 1)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(50, 24)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "From"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(1, 1)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(58, 24)
        Me.Panel6.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 24)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Search :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(881, 1)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(882, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 25)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(883, 1)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "label1"
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.ToolStrip1)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(889, 54)
        Me.Panel17.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator1, Me.ToolStripButton2, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripSeparator3, Me.ToolStripButton4, Me.tsb_SelectContactFax, Me.ToolStripSeparator4, Me.tsb_Viewlogs, Me.tsb_Hidelogs, Me.ToolStripSeparator5, Me.ToolStripButton5})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(889, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(67, 50)
        Me.ToolStripButton1.Tag = "Selectall"
        Me.ToolStripButton1.Text = "&Select All"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 53)
        Me.ToolStripSeparator1.Visible = False
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(60, 50)
        Me.ToolStripButton2.Tag = "Clear"
        Me.ToolStripButton2.Text = "Clear &All"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton2.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(41, 50)
        Me.ToolStripButton3.Tag = "Print"
        Me.ToolStripButton3.Text = "&Print"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(36, 50)
        Me.ToolStripButton4.Tag = "Fax"
        Me.ToolStripButton4.Text = "&Fax"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_SelectContactFax
        '
        Me.tsb_SelectContactFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_SelectContactFax.Image = CType(resources.GetObject("tsb_SelectContactFax.Image"), System.Drawing.Image)
        Me.tsb_SelectContactFax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_SelectContactFax.Name = "tsb_SelectContactFax"
        Me.tsb_SelectContactFax.Size = New System.Drawing.Size(50, 50)
        Me.tsb_SelectContactFax.Tag = "SelectContactFax"
        Me.tsb_SelectContactFax.Text = "Fax &To"
        Me.tsb_SelectContactFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_SelectContactFax.ToolTipText = "Send Fax To"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 53)
        '
        'tsb_Viewlogs
        '
        Me.tsb_Viewlogs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Viewlogs.Image = CType(resources.GetObject("tsb_Viewlogs.Image"), System.Drawing.Image)
        Me.tsb_Viewlogs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Viewlogs.Name = "tsb_Viewlogs"
        Me.tsb_Viewlogs.Size = New System.Drawing.Size(73, 50)
        Me.tsb_Viewlogs.Tag = "ViewLogs"
        Me.tsb_Viewlogs.Text = "&View Logs"
        Me.tsb_Viewlogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_Hidelogs
        '
        Me.tsb_Hidelogs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Hidelogs.Image = CType(resources.GetObject("tsb_Hidelogs.Image"), System.Drawing.Image)
        Me.tsb_Hidelogs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Hidelogs.Name = "tsb_Hidelogs"
        Me.tsb_Hidelogs.Size = New System.Drawing.Size(71, 50)
        Me.tsb_Hidelogs.Tag = "HideLogs"
        Me.tsb_Hidelogs.Text = "&Hide Logs"
        Me.tsb_Hidelogs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Hidelogs.Visible = False
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton5.Tag = "Close"
        Me.ToolStripButton5.Text = "&Close"
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmBatchReferrals
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(889, 701)
        Me.Controls.Add(Me.pnlmain)
        Me.Controls.Add(Me.Panel17)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmBatchReferrals"
        Me.Text = "Batch Referrals"
        Me.pnlmain.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1ExamDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProgress.ResumeLayout(False)
        Me.pnlProgress1.ResumeLayout(False)
        Me.pnlgrid.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        CType(Me.C1ShowLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel20.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.pnlstrip.ResumeLayout(False)
        Me.pnlstrip1.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlShowLog.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlmain As System.Windows.Forms.Panel
    Friend WithEvents pnlgrid As System.Windows.Forms.Panel
    Friend WithEvents C1ExamDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlProgress1 As System.Windows.Forms.Panel
    Friend WithEvents C1ShowLog As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents PrgBarPrintFax As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlstrip1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents DTPTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents DTPFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblcmbType As System.Windows.Forms.Label
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents cmbExamtype As System.Windows.Forms.ComboBox
    Friend WithEvents btnViewlog As System.Windows.Forms.Button
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents cmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents DTPFromLog As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents DTPTOLog As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnLogCLear As System.Windows.Forms.Button
    Friend WithEvents btnLogSelect As System.Windows.Forms.Button
    Friend WithEvents rbSelect As System.Windows.Forms.RadioButton
    Friend WithEvents rbNotes As System.Windows.Forms.RadioButton
    Friend WithEvents rbNone As System.Windows.Forms.RadioButton
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlstrip As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlShowLog As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlShowLog1 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip2 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_Viewlogs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_Hidelogs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsb_SelectContactFax As System.Windows.Forms.ToolStripButton
End Class
