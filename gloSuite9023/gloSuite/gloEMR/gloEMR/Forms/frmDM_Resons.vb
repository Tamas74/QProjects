Public Class frmDM_Resons
    Inherits System.Windows.Forms.Form

    Dim _CriteriaID As Long
    Dim _GuideLineID As Long
    Dim _COL_Patient As Collection

    Dim COL_PatientID As Integer = 0
    Dim COL_PatientName As Integer = 1
    Dim COL_Reson As Integer = 2
    Private WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Save As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip

    Dim COL_COLCOUNT As Integer = 3

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal CriteriaID As Long, ByVal GuideLineID As Long, ByVal COL_Patient As Collection)
        MyBase.New()

        _CriteriaID = CriteriaID
        _GuideLineID = GuideLineID
        _COL_Patient = COL_Patient
        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents pnlReson As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents btnCommon As System.Windows.Forms.Button
    Friend WithEvents txtCommonReason As System.Windows.Forms.TextBox
    Friend WithEvents C1Reasons As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_Resons))
        Me.pnlReson = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnCommon = New System.Windows.Forms.Button
        Me.txtCommonReason = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlGrid = New System.Windows.Forms.Panel
        Me.C1Reasons = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlReson.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.C1Reasons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlReson
        '
        Me.pnlReson.BackColor = System.Drawing.Color.Transparent
        Me.pnlReson.Controls.Add(Me.Label6)
        Me.pnlReson.Controls.Add(Me.Label7)
        Me.pnlReson.Controls.Add(Me.Label8)
        Me.pnlReson.Controls.Add(Me.Label9)
        Me.pnlReson.Controls.Add(Me.btnCommon)
        Me.pnlReson.Controls.Add(Me.txtCommonReason)
        Me.pnlReson.Controls.Add(Me.Label1)
        Me.pnlReson.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlReson.Location = New System.Drawing.Point(0, 54)
        Me.pnlReson.Name = "pnlReson"
        Me.pnlReson.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlReson.Size = New System.Drawing.Size(602, 66)
        Me.pnlReson.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(594, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 59)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(598, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 59)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(596, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'btnCommon
        '
        Me.btnCommon.BackColor = System.Drawing.Color.Transparent
        Me.btnCommon.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnCommon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCommon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCommon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCommon.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCommon.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCommon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCommon.Location = New System.Drawing.Point(440, 14)
        Me.btnCommon.Name = "btnCommon"
        Me.btnCommon.Size = New System.Drawing.Size(150, 40)
        Me.btnCommon.TabIndex = 2
        Me.btnCommon.Text = "Make as Common Reason for All Patients"
        Me.btnCommon.UseVisualStyleBackColor = False
        '
        'txtCommonReason
        '
        Me.txtCommonReason.ForeColor = System.Drawing.Color.Black
        Me.txtCommonReason.Location = New System.Drawing.Point(116, 11)
        Me.txtCommonReason.Multiline = True
        Me.txtCommonReason.Name = "txtCommonReason"
        Me.txtCommonReason.Size = New System.Drawing.Size(318, 45)
        Me.txtCommonReason.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Common Reason :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.Transparent
        Me.pnlGrid.Controls.Add(Me.C1Reasons)
        Me.pnlGrid.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlGrid.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlGrid.Controls.Add(Me.lbl_RightBrd)
        Me.pnlGrid.Controls.Add(Me.lbl_TopBrd)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 146)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(602, 342)
        Me.pnlGrid.TabIndex = 2
        '
        'C1Reasons
        '
        Me.C1Reasons.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Reasons.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Reasons.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Reasons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Reasons.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1Reasons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Reasons.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Reasons.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Reasons.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Reasons.Location = New System.Drawing.Point(4, 2)
        Me.C1Reasons.Name = "C1Reasons"
        Me.C1Reasons.Rows.Count = 1
        Me.C1Reasons.Rows.DefaultSize = 19
        Me.C1Reasons.Rows.Fixed = 0
        Me.C1Reasons.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Reasons.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Reasons.ShowSort = False
        Me.C1Reasons.Size = New System.Drawing.Size(594, 336)
        Me.C1Reasons.StyleInfo = resources.GetString("C1Reasons.StyleInfo")
        Me.C1Reasons.TabIndex = 3
        Me.C1Reasons.Tree.NodeImageCollapsed = CType(resources.GetObject("C1Reasons.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1Reasons.Tree.NodeImageExpanded = CType(resources.GetObject("C1Reasons.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 338)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(594, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 337)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(598, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 337)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(596, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.Label3)
        Me.pnlSearch.Controls.Add(Me.Label4)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 120)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(602, 26)
        Me.pnlSearch.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(594, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 21)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(598, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(596, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.Controls.Add(Me.tls)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(602, 54)
        Me.pnl_ToolStrip.TabIndex = 4
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(602, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'btn_tls_Save
        '
        Me.btn_tls_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Save.Image = CType(resources.GetObject("btn_tls_Save.Image"), System.Drawing.Image)
        Me.btn_tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Save.Name = "btn_tls_Save"
        Me.btn_tls_Save.Size = New System.Drawing.Size(70, 50)
        Me.btn_tls_Save.Tag = "Save"
        Me.btn_tls_Save.Text = " &Save&&Cls"
        Me.btn_tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Save.ToolTipText = "Save and Close"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDM_Resons
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(602, 488)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlReson)
        Me.Controls.Add(Me.pnl_ToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDM_Resons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Guideline Reasons"
        Me.pnlReson.ResumeLayout(False)
        Me.pnlReson.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.C1Reasons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_Resons_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1Reasons)
        Try
            Call SetGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetGridStyle()
        With C1Reasons
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COLCOUNT
            .Cols.Fixed = 0

            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_Reson).AllowEditing = True

            .SetData(0, COL_PatientID, "PatientID")
            .SetData(0, COL_PatientName, "Patient Name")
            .SetData(0, COL_Reson, "Reason")

            .Cols(COL_PatientID).Width = 0
            .Cols(COL_PatientName).Width = .Width * 0.3
            .Cols(COL_Reson).Width = .Width * 0.68

            For i As Integer = 1 To _COL_Patient.Count
                Dim str() As String
                .Rows.Add()
                str = Split(_COL_Patient(i), "|", 2)
                .SetData(i, COL_PatientID, str(0))
                If (str.Length > 1) Then
                    .SetData(i, COL_PatientName, str(1))
                End If

            Next

        End With
    End Sub

    Private Sub SaveDMReasons()
        Try

            With C1Reasons
                Dim i As Integer

                For i = 1 To .Rows.Count - 1
                    If .GetData(i, COL_Reson) = "" Then
                        MessageBox.Show("Reasons to some Patients are not inserted. Please insert the reason for all patients", gstrMessageBoxCaption, MessageBoxButtons.OK)
                        .Select(i, COL_Reson)
                        Exit Sub
                    End If
                Next

                Dim oclsDM_Tempalte As New clsDM_Template
                For i = 1 To .Rows.Count - 1
                    oclsDM_Tempalte.Save_Template(0, .GetData(i, COL_PatientID), _CriteriaID, _GuideLineID, "", frmDM_Template.Type.Save)
                Next
            End With

            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCommon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommon.Click
        Try
            Dim i, j As Integer
            Dim Result As DialogResult
            With C1Reasons
                If txtCommonReason.Text.Trim <> "" Then
                    For i = 1 To .Rows.Count - 1
                        If .GetData(i, COL_Reson) <> "" Then
                            Result = MessageBox.Show("Reasons to some Patients are inserted. Do you want to set these common reasons to all Patients", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                            If Result = DialogResult.Yes Then
                                '' Set Common Reason to all Patients
                                For j = 1 To .Rows.Count - 1
                                    .SetData(j, COL_Reson, txtCommonReason.Text.Trim)
                                Next
                            ElseIf Result = DialogResult.No Then
                                '' Set Common Reason to Only those Patients for whom the Reason is Not Entered
                                For j = 1 To .Rows.Count - 1
                                    If .GetData(j, COL_Reson) = "" Then
                                        .SetData(j, COL_Reson, txtCommonReason.Text.Trim)
                                    End If
                                Next
                            Else
                                '' Dont Do Any Thing
                            End If

                            Exit Sub
                        End If
                    Next

                    '' Set Common Reason to all Patients
                    For i = 1 To .Rows.Count - 1
                        .SetData(i, COL_Reson, txtCommonReason.Text.Trim)
                    Next
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CloseDMReasons()
        Try
            With C1Reasons
                Dim i As Integer

                For i = 1 To .Rows.Count - 1
                    If .GetData(i, COL_Reson) = "" Then
                        MessageBox.Show("Reasons to some Patients are not inserted. Please insert the reason for all Patients", gstrMessageBoxCaption, MessageBoxButtons.OK)
                        .Select(i, COL_Reson)
                        Exit Sub
                    End If
                Next
            End With
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tls_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveDMReasons()
            Case "Close"
                CloseDMReasons()

        End Select
    End Sub

    Private Sub C1Reasons_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Reasons.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
