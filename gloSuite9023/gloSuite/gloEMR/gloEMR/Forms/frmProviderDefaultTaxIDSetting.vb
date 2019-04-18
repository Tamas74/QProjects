Imports C1.Win.C1FlexGrid
Imports C1.Util


Public Class frmProviderDefaultTaxIDSetting
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents flxData As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProviderDefaultTaxIDSetting))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.flxData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.flxData)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 83)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(548, 379)
        Me.pnlMain.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 375)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(540, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 375)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(544, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 375)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(542, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'flxData
        '
        Me.flxData.AllowFiltering = True
        Me.flxData.BackColor = System.Drawing.Color.GhostWhite
        Me.flxData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxData.ColumnInfo = "10,0,0,0,0,105,Columns:"
        Me.flxData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxData.ForeColor = System.Drawing.Color.Black
        Me.flxData.Location = New System.Drawing.Point(3, 0)
        Me.flxData.Name = "flxData"
        Me.flxData.Rows.DefaultSize = 21
        Me.flxData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxData.Size = New System.Drawing.Size(542, 376)
        Me.flxData.StyleInfo = resources.GetString("flxData.StyleInfo")
        Me.flxData.TabIndex = 1
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.AutoSize = True
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(548, 53)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(548, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(40, 50)
        Me.btnSave.Text = "&Save"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.ToolTipText = "Save "
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 53)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(548, 30)
        Me.pnlSearch.TabIndex = 19
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(542, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 49
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(5, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(214, 3)
        Me.label21.TabIndex = 37
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 17)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(214, 5)
        Me.label20.TabIndex = 43
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 46
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.White
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Location = New System.Drawing.Point(1, 0)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(4, 22)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 22)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(240, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 22)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(540, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(541, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(542, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmProviderDefaultTaxIDSetting
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(548, 462)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProviderDefaultTaxIDSetting"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Provider Default Tax ID "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Const COL_PROVIDER As Byte = 0
    Private Const COL_TaxID As Byte = 1
    Private Const COL_TaxIDExcludedGroup As Byte = 2
    Private Const COL_AssociationID As Byte = 3
    Private Const COL_ProviderID As Byte = 4
    Private _messageBoxCaption As String = "gloEMR"
    Private Sub RetrieveProviders()
        Dim dtProvider As New DataTable
        dtProvider = getProviderWithDefaultTaxID()
        Dim nCount As Int16
        If Not IsNothing(dtProvider) AndAlso dtProvider.Rows.Count > 1 Then
            For nCount = 0 To dtProvider.Rows.Count - 1
                With flxData
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_PROVIDER, Convert.ToString(dtProvider.Rows(nCount)("ProviderName")))
                    SetCellStyleValueColumn(Convert.ToInt64(dtProvider.Rows(nCount)("nProviderID")), .Rows.Count - 1, Convert.ToString(dtProvider.Rows(nCount)("AssociationID")))
                    .SetData(.Rows.Count - 1, COL_TaxID, Convert.ToString(dtProvider.Rows(nCount)("sDiaplayDefaultTaxID")))
                    .SetData(.Rows.Count - 1, COL_ProviderID, Convert.ToString(dtProvider.Rows(nCount)("nProviderID")))
                    .SetData(.Rows.Count - 1, COL_TaxIDExcludedGroup, Convert.ToString(dtProvider.Rows(nCount)("TIN")))
                    .SetData(.Rows.Count - 1, COL_AssociationID, Convert.ToString(dtProvider.Rows(nCount)("AssociationID")))
                End With
            Next
        Else
            MessageBox.Show("No provider information available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    Private Sub frmProviderReferralSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(flxData)

        Try
            Call FormatGrid()
            Call RetrieveProviders()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Public Function getProviderWithDefaultTaxID() As DataTable
        Dim dt As DataTable = New DataTable
        Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters
        Try
            oDB.Connect(False)
            oDB.Retrive("gsp_getProviderWithDefaultTaxID", dt)
            Return dt.Copy
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDBParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
            If (Not (dt) Is Nothing) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try

    End Function

    Public Function getProviderAssociatedTaxID(nProviderID As Int64) As DataTable
        Dim dt As DataTable = New DataTable
        Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters
        Try
            oDB.Connect(False)
            oDBParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_getProviderAssociatedTaxID", oDBParameters, dt)
            Return dt.Copy
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDBParameters = Nothing
            oDB.Dispose()
            oDB = Nothing
            If (Not (dt) Is Nothing) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try

    End Function

    Private Sub SetCellStyleValueColumn(ByVal nProviderID As Int64, ByVal Index As Integer, ByVal nDefaultAssociationID As Int64)
        Try
            Dim dtValue As New DataTable
            dtValue = getProviderAssociatedTaxID(nProviderID)

            If (Not (dtValue) Is Nothing) Then
                Dim cmbValue As ComboBox = New ComboBox
                cmbValue.DropDownStyle = ComboBoxStyle.DropDownList
                flxData.BeginUpdate()
                Dim cslist As C1.Win.C1FlexGrid.CellStyle
                Dim stringRandom As Random = New Random(1)
                cslist = Me.flxData.Styles.Add(stringRandom.GetHashCode.ToString)
                cslist.Editor = cmbValue
                AddHandler cmbValue.SelectedIndexChanged, AddressOf Me.cmbValue_SelectedIndexChanged
                cmbValue.BeginUpdate()
                cmbValue.DataSource = Nothing
                cmbValue.Items.Clear()
                cmbValue.ValueMember = "nAssociationID"
                cmbValue.DisplayMember = "sTIN"
                cmbValue.DataSource = dtValue
                'cmbValue.SelectedValue = 396123792658910121
                'cmbValue.SelectedText = "TESTTTT" ''SelectedValue = nDefaultAssociationID
                'If (cmbValue.Items.Count > 0) Then
                '    Dim i As Integer = 0
                '    Do While (i < cmbValue.Items.Count)
                '        Dim item As Object = cmbValue.Items(i)
                '        Dim thisValue As Object = item.GetType.GetProperty(cmbValue.ValueMember).GetValue(item, New Object() {i})     ''item.GetType.GetProperty(cmbValue.ValueMember).GetValue(item)
                '        If ((Not (thisValue) Is Nothing) AndAlso thisValue.Equals(nDefaultAssociationID)) Then
                '            cmbValue.SelectedIndex = i
                '            Return
                '        End If

                '        i = (i + 1)
                '    Loop
                '    cmbValue.SelectedIndex = 0
                'End If

                '' cmbValue.SelectedIndex = CType(3, Integer) ''SelectedIndex = 3
                flxData.SetCellStyle(Index, COL_TaxID, cslist)
                flxData.EndUpdate()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub cmbValue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim selectedRow As DataRowView = CType(CType(sender, ComboBox).SelectedItem, DataRowView)

            With flxData
                .BeginUpdate()
                .SetData(.RowSel, COL_TaxIDExcludedGroup, Convert.ToString(selectedRow.Row(2))) ''Convert.ToString(selectedRow.Row(2)))
                .SetData(.RowSel, COL_AssociationID, Convert.ToString(selectedRow.Row(0)))
                .EndUpdate()
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub FormatGrid()
        With flxData
            .Cols.Count = 5
            .Rows.Count = 1
            .Rows.Fixed = 1
            .AllowDragging = AllowDraggingEnum.None
            .Cols(COL_PROVIDER).Width = 0.4 * .Width
            .Cols(COL_TaxID).Width = 0.6 * .Width - 5

            .Cols(COL_AssociationID).Width = 0
            .Cols(COL_ProviderID).Width = 0
            .Cols(COL_TaxIDExcludedGroup).Width = 0

            flxData.SetData(0, COL_PROVIDER, "Provider")
            flxData.SetData(0, COL_TaxID, "Default Tax ID")
            flxData.SetData(0, COL_TaxIDExcludedGroup, "Tax ID")
            flxData.SetData(0, COL_AssociationID, "AssociationID")
            flxData.SetData(0, COL_ProviderID, "ProviderID")

            .Cols(COL_PROVIDER).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_TaxID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            .Cols(COL_PROVIDER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_TaxID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PROVIDER).AllowEditing = False
        End With
    End Sub

    Private Function Save() As Boolean
        Try
            If Not IsNothing(flxData) AndAlso flxData.Rows.Count > 1 Then
                Dim dtProviderDefaultTaxID As New DataTable
                dtProviderDefaultTaxID.Columns.Add("nAssociationID")
                dtProviderDefaultTaxID.Columns.Add("nProviderID")
                dtProviderDefaultTaxID.Columns.Add("sTIN")
                dtProviderDefaultTaxID.Columns.Add("sModule")
                Dim nCount As Integer = 0
                For nCount = 1 To flxData.Rows.Count - 1
                    If Convert.ToString(flxData.Rows(nCount)(COL_TaxIDExcludedGroup)) <> "" Then
                        dtProviderDefaultTaxID.Rows.Add()
                        dtProviderDefaultTaxID.Rows(dtProviderDefaultTaxID.Rows.Count - 1)("nAssociationID") = Convert.ToInt64(flxData.Rows(nCount)(COL_AssociationID))
                        dtProviderDefaultTaxID.Rows(dtProviderDefaultTaxID.Rows.Count - 1)("nProviderID") = Convert.ToInt64(flxData.Rows(nCount)(COL_ProviderID))
                        dtProviderDefaultTaxID.Rows(dtProviderDefaultTaxID.Rows.Count - 1)("sTIN") = Convert.ToString(flxData.Rows(nCount)(COL_TaxIDExcludedGroup))
                        dtProviderDefaultTaxID.Rows(dtProviderDefaultTaxID.Rows.Count - 1)("sModule") = ""
                    End If

                Next
                dtProviderDefaultTaxID.AcceptChanges()
                '' If Not IsNothing(dtProviderDefaultTaxID) AndAlso dtProviderDefaultTaxID.Rows.Count > 0 Then
                If SaveProvidersDefaulTaxID(dtProviderDefaultTaxID) Then
                    Return True
                Else
                    Return False
                End If

                ''End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Save() Then
            Me.Close()
        End If
    End Sub
    Public Function SaveProvidersDefaulTaxID(ByVal dtProviderDefaultTaxID As DataTable) As Boolean
        Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters
        Try
            oDB.Connect(True)
            oParameters.Add("@TVPProvidersDefaulTaxID", dtProviderDefaultTaxID, ParameterDirection.Input, SqlDbType.Structured)
            oDB.ExecuteWithTransaction("gsp_SaveProviderDefaulTaxID", oParameters)
            oDB.Disconnect()
            Return True
        Catch dbEx As gloDatabaseLayer.DBException
            oDB.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, False)
            MessageBox.Show(dbEx.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            oDB.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If (Not (oDB) Is Nothing) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If (Not (oParameters) Is Nothing) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

        End Try

    End Function

    
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Cursor = Cursors.WaitCursor
        If Save() Then
            '' MessageBox.Show("")  
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If txtSearch.Text.Trim() <> "" Then
                Dim Filter As C1.Win.C1FlexGrid.ConditionFilter = New C1.Win.C1FlexGrid.ConditionFilter()
                Filter.AndConditions = False
                flxData.Cols(COL_PROVIDER).AllowFiltering = AllowFiltering.ByCondition
                Filter.Condition1.Operator = ConditionOperator.Contains
                Filter.Condition1.Parameter = ""
                Filter.Condition1.Parameter = Convert.ToString(txtSearch.Text.Trim())
                flxData.Cols(COL_PROVIDER).Filter = Filter
            Else
                flxData.Cols(COL_PROVIDER).Filter = Nothing
            End If
            flxData.ApplyFilters()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub flxData_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles flxData.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
