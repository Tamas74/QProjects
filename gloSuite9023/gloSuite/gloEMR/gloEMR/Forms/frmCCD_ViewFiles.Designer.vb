<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCD_ViewFiles
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cfgCCD.ContextMenuStrip, cmsCCDCCR}
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Try

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                        End If
                    End If

                    If (IsNothing(CmpControls) = False) Then
                        If CmpControls.Length > 0 Then
                            gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                        End If
                    End If
                Catch

                End Try

            End If
            Try
                If Not IsNothing(gloUC_PatientStrip1) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch

            End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCD_ViewFiles))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tsEjectionFraction = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlb_ViewCCD = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Extract = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Reconcile = New System.Windows.Forms.ToolStripButton()
        Me.tsb_ViewCDAErrors = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.tls_SaveAs = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlEjectionFraction = New System.Windows.Forms.Panel()
        Me.cfgCCD = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.pnlPatientSearch = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PnlDisplayFileType = New System.Windows.Forms.Panel()
        Me.cmbSelectFileType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rbImport = New System.Windows.Forms.RadioButton()
        Me.rbExport = New System.Windows.Forms.RadioButton()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cmsCCDCCR = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.tsEjectionFraction.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlEjectionFraction.SuspendLayout()
        CType(Me.cfgCCD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientSearch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.PnlDisplayFileType.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tsEjectionFraction)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(808, 56)
        Me.Panel1.TabIndex = 0
        '
        'tsEjectionFraction
        '
        Me.tsEjectionFraction.BackgroundImage = CType(resources.GetObject("tsEjectionFraction.BackgroundImage"), System.Drawing.Image)
        Me.tsEjectionFraction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsEjectionFraction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsEjectionFraction.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsEjectionFraction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_ViewCCD, Me.tlbbtn_Extract, Me.tlbbtn_Reconcile, Me.tsb_ViewCDAErrors, Me.tlbbtn_Refresh, Me.tls_SaveAs, Me.tlbbtn_Close})
        Me.tsEjectionFraction.Location = New System.Drawing.Point(0, 0)
        Me.tsEjectionFraction.Name = "tsEjectionFraction"
        Me.tsEjectionFraction.Size = New System.Drawing.Size(808, 53)
        Me.tsEjectionFraction.TabIndex = 0
        Me.tsEjectionFraction.Text = "ToolStrip1"
        '
        'tlb_ViewCCD
        '
        Me.tlb_ViewCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlb_ViewCCD.Image = CType(resources.GetObject("tlb_ViewCCD.Image"), System.Drawing.Image)
        Me.tlb_ViewCCD.Name = "tlb_ViewCCD"
        Me.tlb_ViewCCD.Size = New System.Drawing.Size(130, 50)
        Me.tlb_ViewCCD.Tag = "ViewCCDCCR"
        Me.tlb_ViewCCD.Text = "&View CCD-CCR-CDA"
        Me.tlb_ViewCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Extract
        '
        Me.tlbbtn_Extract.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Extract.Image = CType(resources.GetObject("tlbbtn_Extract.Image"), System.Drawing.Image)
        Me.tlbbtn_Extract.Name = "tlbbtn_Extract"
        Me.tlbbtn_Extract.Size = New System.Drawing.Size(55, 50)
        Me.tlbbtn_Extract.Tag = "Extract"
        Me.tlbbtn_Extract.Text = "&Extract"
        Me.tlbbtn_Extract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Reconcile
        '
        Me.tlbbtn_Reconcile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_Reconcile.Image = CType(resources.GetObject("tlbbtn_Reconcile.Image"), System.Drawing.Image)
        Me.tlbbtn_Reconcile.Name = "tlbbtn_Reconcile"
        Me.tlbbtn_Reconcile.Size = New System.Drawing.Size(68, 50)
        Me.tlbbtn_Reconcile.Tag = "Reconcile"
        Me.tlbbtn_Reconcile.Text = "&Reconcile"
        Me.tlbbtn_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_ViewCDAErrors
        '
        Me.tsb_ViewCDAErrors.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_ViewCDAErrors.Image = CType(resources.GetObject("tsb_ViewCDAErrors.Image"), System.Drawing.Image)
        Me.tsb_ViewCDAErrors.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_ViewCDAErrors.Name = "tsb_ViewCDAErrors"
        Me.tsb_ViewCDAErrors.Size = New System.Drawing.Size(77, 50)
        Me.tsb_ViewCDAErrors.Tag = "CDAErrors"
        Me.tsb_ViewCDAErrors.Text = "CDA &Errors"
        Me.tsb_ViewCDAErrors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_ViewCDAErrors.ToolTipText = "CDA Errors"
        Me.tsb_ViewCDAErrors.Visible = False
        '
        'tlbbtn_Refresh
        '
        Me.tlbbtn_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Refresh.Image = CType(resources.GetObject("tlbbtn_Refresh.Image"), System.Drawing.Image)
        Me.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Refresh.Name = "tlbbtn_Refresh"
        Me.tlbbtn_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tlbbtn_Refresh.Tag = "Refresh"
        Me.tlbbtn_Refresh.Text = "&Refresh"
        Me.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_SaveAs
        '
        Me.tls_SaveAs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_SaveAs.Image = CType(resources.GetObject("tls_SaveAs.Image"), System.Drawing.Image)
        Me.tls_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_SaveAs.Name = "tls_SaveAs"
        Me.tls_SaveAs.Size = New System.Drawing.Size(59, 50)
        Me.tls_SaveAs.Tag = "Save As"
        Me.tls_SaveAs.Text = "&Save As"
        Me.tls_SaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlEjectionFraction)
        Me.pnlMain.Controls.Add(Me.pnlPatientSearch)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(808, 576)
        Me.pnlMain.TabIndex = 0
        '
        'pnlEjectionFraction
        '
        Me.pnlEjectionFraction.Controls.Add(Me.cfgCCD)
        Me.pnlEjectionFraction.Controls.Add(Me.Label5)
        Me.pnlEjectionFraction.Controls.Add(Me.Label4)
        Me.pnlEjectionFraction.Controls.Add(Me.Label3)
        Me.pnlEjectionFraction.Controls.Add(Me.Label1)
        Me.pnlEjectionFraction.Controls.Add(Me.Label2)
        Me.pnlEjectionFraction.Controls.Add(Me.Panel4)
        Me.pnlEjectionFraction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEjectionFraction.Location = New System.Drawing.Point(0, 27)
        Me.pnlEjectionFraction.Name = "pnlEjectionFraction"
        Me.pnlEjectionFraction.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlEjectionFraction.Size = New System.Drawing.Size(808, 549)
        Me.pnlEjectionFraction.TabIndex = 0
        '
        'cfgCCD
        '
        Me.cfgCCD.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.cfgCCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cfgCCD.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.cfgCCD.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.cfgCCD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cfgCCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.cfgCCD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.cfgCCD.Location = New System.Drawing.Point(4, 1)
        Me.cfgCCD.Name = "cfgCCD"
        Me.cfgCCD.Rows.DefaultSize = 21
        Me.cfgCCD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.cfgCCD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.cfgCCD.ShowCellLabels = True
        Me.cfgCCD.Size = New System.Drawing.Size(732, 544)
        Me.cfgCCD.StyleInfo = resources.GetString("cfgCCD.StyleInfo")
        Me.cfgCCD.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(736, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 544)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 544)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(734, 1)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 545)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(734, 1)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(737, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label2.Size = New System.Drawing.Size(68, 20)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "  Search :"
        Me.Label2.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.txtSearch)
        Me.Panel4.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.Panel4.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.Panel4.Controls.Add(Me.PicBx_Search)
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.Black
        Me.Panel4.Location = New System.Drawing.Point(58, 52)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(250, 21)
        Me.Panel4.TabIndex = 17
        Me.Panel4.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(28, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(222, 15)
        Me.txtSearch.TabIndex = 4
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(28, 0)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(222, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(28, 19)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(222, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(0, 0)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        Me.PicBx_Search.WaitOnLoad = True
        '
        'pnlPatientSearch
        '
        Me.pnlPatientSearch.Controls.Add(Me.Panel3)
        Me.pnlPatientSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlPatientSearch.Name = "pnlPatientSearch"
        Me.pnlPatientSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientSearch.Size = New System.Drawing.Size(808, 27)
        Me.pnlPatientSearch.TabIndex = 19
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.PnlDisplayFileType)
        Me.Panel3.Controls.Add(Me.rbImport)
        Me.Panel3.Controls.Add(Me.rbExport)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.Panel3.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(802, 24)
        Me.Panel3.TabIndex = 0
        '
        'PnlDisplayFileType
        '
        Me.PnlDisplayFileType.BackColor = System.Drawing.Color.Transparent
        Me.PnlDisplayFileType.Controls.Add(Me.cmbSelectFileType)
        Me.PnlDisplayFileType.Controls.Add(Me.Label6)
        Me.PnlDisplayFileType.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlDisplayFileType.Location = New System.Drawing.Point(325, 1)
        Me.PnlDisplayFileType.Name = "PnlDisplayFileType"
        Me.PnlDisplayFileType.Size = New System.Drawing.Size(261, 22)
        Me.PnlDisplayFileType.TabIndex = 47
        Me.PnlDisplayFileType.Visible = False
        '
        'cmbSelectFileType
        '
        Me.cmbSelectFileType.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbSelectFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSelectFileType.FormattingEnabled = True
        Me.cmbSelectFileType.Items.AddRange(New Object() {"Valid Files", "Invalid Files"})
        Me.cmbSelectFileType.Location = New System.Drawing.Point(103, 0)
        Me.cmbSelectFileType.Name = "cmbSelectFileType"
        Me.cmbSelectFileType.Size = New System.Drawing.Size(137, 22)
        Me.cmbSelectFileType.TabIndex = 47
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 22)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Select file type :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rbImport
        '
        Me.rbImport.BackColor = System.Drawing.Color.Transparent
        Me.rbImport.Checked = True
        Me.rbImport.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbImport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbImport.Location = New System.Drawing.Point(586, 1)
        Me.rbImport.Name = "rbImport"
        Me.rbImport.Size = New System.Drawing.Size(99, 22)
        Me.rbImport.TabIndex = 44
        Me.rbImport.TabStop = True
        Me.rbImport.Text = "Inbound    "
        Me.rbImport.UseVisualStyleBackColor = False
        '
        'rbExport
        '
        Me.rbExport.BackColor = System.Drawing.Color.Transparent
        Me.rbExport.Dock = System.Windows.Forms.DockStyle.Right
        Me.rbExport.Location = New System.Drawing.Point(685, 1)
        Me.rbExport.Name = "rbExport"
        Me.rbExport.Size = New System.Drawing.Size(116, 22)
        Me.rbExport.TabIndex = 45
        Me.rbExport.TabStop = True
        Me.rbExport.Text = "Outbound"
        Me.rbExport.UseVisualStyleBackColor = False
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(801, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(802, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(0, 23)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(802, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cmsCCDCCR
        '
        Me.cmsCCDCCR.Name = "cmsCCDCCR"
        Me.cmsCCDCCR.Size = New System.Drawing.Size(61, 4)
        '
        'frmCCD_ViewFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(808, 632)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCCD_ViewFiles"
        Me.Text = "View CCD-CCR-CDA Files"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tsEjectionFraction.ResumeLayout(False)
        Me.tsEjectionFraction.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlEjectionFraction.ResumeLayout(False)
        Me.pnlEjectionFraction.PerformLayout()
        CType(Me.cfgCCD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientSearch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.PnlDisplayFileType.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlEjectionFraction As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents tsEjectionFraction As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents cfgCCD As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Friend WithEvents tlbbtn_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_SaveAs As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbImport As System.Windows.Forms.RadioButton
    Friend WithEvents rbExport As System.Windows.Forms.RadioButton
    Friend WithEvents tlbbtn_Extract As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Reconcile As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_ViewCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmsCCDCCR As System.Windows.Forms.ContextMenuStrip
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents PnlDisplayFileType As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSelectFileType As System.Windows.Forms.ComboBox
    Friend WithEvents tsb_ViewCDAErrors As System.Windows.Forms.ToolStripButton

End Class
