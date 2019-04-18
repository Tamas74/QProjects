<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmvwPatientConsentTracking
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
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmvwPatientConsentTracking))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Add = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Modify = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Delete = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_ViewHistory = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gvData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.pnl_txtSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.pnl_txtSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.tblStrip)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1213, 53)
        Me.pnlTop.TabIndex = 28
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Add, Me.tblbtn_Modify, Me.tblbtn_Delete, Me.tblbtn_ViewHistory, Me.tblbtn_Close})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1213, 53)
        Me.tblStrip.TabIndex = 0
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Add
        '
        Me.tblbtn_Add.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Add.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Add.Image = CType(resources.GetObject("tblbtn_Add.Image"), System.Drawing.Image)
        Me.tblbtn_Add.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Add.Name = "tblbtn_Add"
        Me.tblbtn_Add.Size = New System.Drawing.Size(37, 50)
        Me.tblbtn_Add.Tag = "Add"
        Me.tblbtn_Add.Text = "&New"
        Me.tblbtn_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Modify
        '
        Me.tblbtn_Modify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Modify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Modify.Image = CType(resources.GetObject("tblbtn_Modify.Image"), System.Drawing.Image)
        Me.tblbtn_Modify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Modify.Name = "tblbtn_Modify"
        Me.tblbtn_Modify.Size = New System.Drawing.Size(53, 50)
        Me.tblbtn_Modify.Tag = "Modify"
        Me.tblbtn_Modify.Text = "&Modify"
        Me.tblbtn_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Delete
        '
        Me.tblbtn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Delete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Delete.Image = CType(resources.GetObject("tblbtn_Delete.Image"), System.Drawing.Image)
        Me.tblbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Delete.Name = "tblbtn_Delete"
        Me.tblbtn_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tblbtn_Delete.Tag = "Delete"
        Me.tblbtn_Delete.Text = "&Delete"
        Me.tblbtn_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_ViewHistory
        '
        Me.tblbtn_ViewHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_ViewHistory.Image = CType(resources.GetObject("tblbtn_ViewHistory.Image"), System.Drawing.Image)
        Me.tblbtn_ViewHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ViewHistory.Name = "tblbtn_ViewHistory"
        Me.tblbtn_ViewHistory.Size = New System.Drawing.Size(88, 50)
        Me.tblbtn_ViewHistory.Tag = "View History"
        Me.tblbtn_ViewHistory.Text = "&View History"
        Me.tblbtn_ViewHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.gvData)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 80)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1213, 513)
        Me.pnlMain.TabIndex = 1
        '
        'gvData
        '
        Me.gvData.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gvData.AutoResize = True
        Me.gvData.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvData.ColumnInfo = resources.GetString("gvData.ColumnInfo")
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.ExtendLastCol = True
        Me.gvData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvData.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.gvData.Location = New System.Drawing.Point(4, 4)
        Me.gvData.Name = "gvData"
        Me.gvData.Rows.Count = 13
        Me.gvData.Rows.DefaultSize = 19
        Me.gvData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvData.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gvData.ShowCellLabels = True
        Me.gvData.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.gvData.Size = New System.Drawing.Size(1205, 505)
        Me.gvData.StyleInfo = resources.GetString("gvData.StyleInfo")
        Me.gvData.TabIndex = 0
        Me.gvData.Tree.NodeImageCollapsed = CType(resources.GetObject("gvData.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.gvData.Tree.NodeImageExpanded = CType(resources.GetObject("gvData.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 509)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1205, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 506)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1209, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 506)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1207, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 53)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlSearch.Size = New System.Drawing.Size(1213, 27)
        Me.pnlSearch.TabIndex = 0
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.pnl_txtSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(1207, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'pnl_txtSearch
        '
        Me.pnl_txtSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnl_txtSearch.Controls.Add(Me.txtSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label77)
        Me.pnl_txtSearch.Controls.Add(Me.Label61)
        Me.pnl_txtSearch.Controls.Add(Me.Label62)
        Me.pnl_txtSearch.Controls.Add(Me.btnClearSearch)
        Me.pnl_txtSearch.Controls.Add(Me.Label63)
        Me.pnl_txtSearch.Controls.Add(Me.Label64)
        Me.pnl_txtSearch.Controls.Add(Me.label11)
        Me.pnl_txtSearch.Controls.Add(Me.label12)
        Me.pnl_txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_txtSearch.ForeColor = System.Drawing.Color.Black
        Me.pnl_txtSearch.Location = New System.Drawing.Point(78, 1)
        Me.pnl_txtSearch.Name = "pnl_txtSearch"
        Me.pnl_txtSearch.Size = New System.Drawing.Size(241, 22)
        Me.pnl_txtSearch.TabIndex = 231
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(212, 15)
        Me.txtSearch.TabIndex = 1
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 16)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(212, 5)
        Me.Label77.TabIndex = 43
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.White
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Location = New System.Drawing.Point(5, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(212, 3)
        Me.Label61.TabIndex = 37
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.White
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.Location = New System.Drawing.Point(1, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(4, 20)
        Me.Label62.TabIndex = 38
        '
        'btnClearSearch
        '
        Me.btnClearSearch.BackgroundImage = CType(resources.GetObject("btnClearSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearch.FlatAppearance.BorderSize = 0
        Me.btnClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearch.Image = CType(resources.GetObject("btnClearSearch.Image"), System.Drawing.Image)
        Me.btnClearSearch.Location = New System.Drawing.Point(217, 1)
        Me.btnClearSearch.Name = "btnClearSearch"
        Me.btnClearSearch.Size = New System.Drawing.Size(23, 20)
        Me.btnClearSearch.TabIndex = 2
        Me.btnClearSearch.UseVisualStyleBackColor = True
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label63.Location = New System.Drawing.Point(0, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 20)
        Me.Label63.TabIndex = 39
        Me.Label63.Text = "label4"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label64.Location = New System.Drawing.Point(240, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 20)
        Me.Label64.TabIndex = 40
        Me.Label64.Text = "label4"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label11.Location = New System.Drawing.Point(0, 21)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(241, 1)
        Me.label11.TabIndex = 44
        Me.label11.Text = "label1"
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.label12.Location = New System.Drawing.Point(0, 0)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(241, 1)
        Me.label12.TabIndex = 45
        Me.label12.Text = "label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(74, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(4, 20)
        Me.Label1.TabIndex = 49
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(73, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1205, 1)
        Me.Label2.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1206, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(0, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1207, 1)
        Me.Label9.TabIndex = 8
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmvwPatientConsentTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1213, 593)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmvwPatientConsentTracking"
        Me.Text = "Patient Consent Tracking"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnl_txtSearch.ResumeLayout(False)
        Me.pnl_txtSearch.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tblStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Modify As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents gvData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tblbtn_ViewHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl_txtSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
