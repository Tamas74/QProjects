'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Public Class frmArchiveAudit
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
    Friend WithEvents optCustomize As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grpArchive As System.Windows.Forms.GroupBox
    Friend WithEvents panCustomize As System.Windows.Forms.Panel
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbPatientType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents dgData As clsDataGrid ' System.Windows.Forms.DataGrid
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnArchive As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArchiveAudit))
        Me.grpArchive = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.panCustomize = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbPatientType = New System.Windows.Forms.ComboBox
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.txtPatient = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.optCustomize = New System.Windows.Forms.RadioButton
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnPreview = New System.Windows.Forms.ToolStripButton
        Me.btnArchive = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.dgData = New gloEMRAdmin.clsDataGrid
        Me.panCustomize.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpArchive
        '
        Me.grpArchive.BackColor = System.Drawing.Color.Transparent
        Me.grpArchive.Location = New System.Drawing.Point(553, 227)
        Me.grpArchive.Name = "grpArchive"
        Me.grpArchive.Size = New System.Drawing.Size(180, 37)
        Me.grpArchive.TabIndex = 0
        Me.grpArchive.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Archive Type :"
        '
        'panCustomize
        '
        Me.panCustomize.Controls.Add(Me.Label4)
        Me.panCustomize.Controls.Add(Me.cmbUsers)
        Me.panCustomize.Controls.Add(Me.Label2)
        Me.panCustomize.Controls.Add(Me.cmbPatientType)
        Me.panCustomize.Controls.Add(Me.cmbCategory)
        Me.panCustomize.Controls.Add(Me.txtPatient)
        Me.panCustomize.Controls.Add(Me.Label5)
        Me.panCustomize.Controls.Add(Me.dtTo)
        Me.panCustomize.Controls.Add(Me.Label3)
        Me.panCustomize.Controls.Add(Me.dtFrom)
        Me.panCustomize.Controls.Add(Me.Label6)
        Me.panCustomize.Location = New System.Drawing.Point(10, 41)
        Me.panCustomize.Name = "panCustomize"
        Me.panCustomize.Size = New System.Drawing.Size(798, 77)
        Me.panCustomize.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(418, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Fi&lter For :"
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Location = New System.Drawing.Point(483, 46)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(256, 22)
        Me.cmbUsers.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(436, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "&Users :"
        '
        'cmbPatientType
        '
        Me.cmbPatientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientType.Location = New System.Drawing.Point(483, 10)
        Me.cmbPatientType.Name = "cmbPatientType"
        Me.cmbPatientType.Size = New System.Drawing.Size(108, 22)
        Me.cmbPatientType.TabIndex = 4
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Location = New System.Drawing.Point(129, 46)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(256, 22)
        Me.cmbCategory.TabIndex = 7
        '
        'txtPatient
        '
        Me.txtPatient.Location = New System.Drawing.Point(599, 10)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(140, 22)
        Me.txtPatient.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Acti&vity Type :"
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(274, 10)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(112, 22)
        Me.dtTo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(241, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "&To :"
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(129, 10)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(105, 22)
        Me.dtFrom.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(77, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "&From :"
        '
        'optCustomize
        '
        Me.optCustomize.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCustomize.Location = New System.Drawing.Point(191, 11)
        Me.optCustomize.Name = "optCustomize"
        Me.optCustomize.Size = New System.Drawing.Size(97, 24)
        Me.optCustomize.TabIndex = 2
        Me.optCustomize.Text = "C&ustomize"
        '
        'optAll
        '
        Me.optAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAll.Location = New System.Drawing.Point(137, 11)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(48, 24)
        Me.optAll.TabIndex = 1
        Me.optAll.Text = "&All"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(850, 56)
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
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPreview, Me.btnArchive, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(850, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnPreview
        '
        Me.btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(59, 50)
        Me.btnPreview.Text = "&Preview"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnArchive
        '
        Me.btnArchive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnArchive.Image = CType(resources.GetObject("btnArchive.Image"), System.Drawing.Image)
        Me.btnArchive.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnArchive.Name = "btnArchive"
        Me.btnArchive.Size = New System.Drawing.Size(60, 50)
        Me.btnArchive.Text = "&Archive "
        Me.btnArchive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnArchive.ToolTipText = "Archive"
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
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.dgData)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 181)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(850, 517)
        Me.Panel2.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(4, 513)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(842, 1)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 513)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(846, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 513)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(844, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.panCustomize)
        Me.Panel1.Controls.Add(Me.optCustomize)
        Me.Panel1.Controls.Add(Me.optAll)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(850, 125)
        Me.Panel1.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 121)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(842, 1)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 118)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(846, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 118)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(844, 1)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "label1"
        '
        'dgData
        '
        Me.dgData.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgData.BackColor = System.Drawing.Color.GhostWhite
        Me.dgData.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.ForeColor = System.Drawing.Color.Black
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.Black
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.HeaderForeColor = System.Drawing.Color.White
        Me.dgData.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.Location = New System.Drawing.Point(3, 1)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.ReadOnly = True
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(844, 513)
        Me.dgData.TabIndex = 0
        '
        'frmArchiveAudit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(850, 698)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Controls.Add(Me.grpArchive)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(856, 730)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(856, 730)
        Me.Name = "frmArchiveAudit"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Archive Audit Data"
        Me.panCustomize.ResumeLayout(False)
        Me.panCustomize.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub optAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAll.Click
        Try
            panCustomize.Enabled = False
            btnPreview.Enabled = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optCustomize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCustomize.Click
        Try
            panCustomize.Enabled = True
            btnPreview.Enabled = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmArchiveAudit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            optAll.Checked = True
            With dtFrom
                .Format = DateTimePickerFormat.Custom
                .CustomFormat = DTFORMAT
                .Value = Date.Now
            End With
            With dtTo
                .Format = DateTimePickerFormat.Custom
                .CustomFormat = DTFORMAT
                .Value = Date.Now
            End With
            With cmbPatientType
                ' .Items.Add("Patient ID")
                .Items.Add("Patient Code")
                .Items.Add("First Name")
                .Items.Add("Last Name")
                .SelectedIndex = 0
            End With
            Call Fill_ActivityTypesUsers()
            panCustomize.Enabled = False
            btnPreview.Enabled = False
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_ActivityTypesUsers()
        Dim objAudit As New clsAudit
        Dim clAudit As New Collection
        Dim nCount As Integer
        clAudit = objAudit.Fill_AuditCategory
        cmbCategory.Items.Clear()
        cmbCategory.Items.Add("All")
        For nCount = 1 To clAudit.Count
            cmbCategory.Items.Add(clAudit.Item(nCount))
        Next
        cmbCategory.SelectedIndex = 0
        'Added Code for Audit LOG Enhancement
        Dim dtUsers As DataTable
        dtUsers = objAudit.Fill_ArchivedUsers(True)
        If (Not IsNothing(dtUsers)) Then
            Dim dr As DataRow
            dr = dtUsers.NewRow
            dr(0) = "0"
            dr(1) = "All"
            dtUsers.Rows.Add(dr)
            cmbUsers.DataSource = dtUsers
            cmbUsers.ValueMember = dtUsers.Columns(0).ColumnName
            cmbUsers.DisplayMember = dtUsers.Columns(1).ColumnName
            cmbUsers.SelectedIndex = cmbUsers.Items.Count - 1
        End If
        objAudit = Nothing
    End Sub
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Dim objAudit As New clsAudit

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim objAuditReports As New clsAudit
            Dim dtAuditReports As New DataTable
            If Trim(txtPatient.Text) = "" Then
                'Added Code for Audit LOG Enhancement
                dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue)
            Else
                'If Trim(cmbPatientType.Text) = "Patient ID" Then
                If Trim(cmbPatientType.Text) = "Patient Code" Then
                    'Commented by shubhangi Coz patient code is not numeric
                    ' If IsNumeric(txtPatient.Text) = True Then
                    'dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedItem, CInt(txtPatient.Text))

                    dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, txtPatient.Text)
                    'End If
                Else
                If Trim(cmbPatientType.Text) = "First Name" Then
                        dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, , Trim(txtPatient.Text))
                ElseIf Trim(cmbPatientType.Text) = "Last Name" Then
                        dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, , , Trim(txtPatient.Text))
                Else
                        dtAuditReports = objAuditReports.RetrieveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, , txtPatient.Text)
                End If
            End If
            End If
            dgData.DataSource = dtAuditReports
            dgData.CaptionText = "Audit Report"
            dgData.RowHeadersVisible = False
            Dim grdTableStyle As New clsDataGridTableStyle(dtAuditReports.TableName)

            Dim grdColAuditReportID As New DataGridTextBoxColumn
            With grdColAuditReportID
                .HeaderText = "Reports ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(0).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColActivityDate As New DataGridTextBoxColumn
            With grdColActivityDate
                .HeaderText = "Activity Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(1).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColActivityCategory As New DataGridTextBoxColumn
            With grdColActivityCategory
                .HeaderText = "Category"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(2).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColDescription As New DataGridTextBoxColumn
            With grdColDescription
                .HeaderText = "Description"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(3).ColumnName
                .NullText = ""
                .Width = 200
            End With

            Dim grdColPatientCode As New DataGridTextBoxColumn
            With grdColPatientCode
                .HeaderText = "Patient Code"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(4).ColumnName
                .NullText = ""
                .Width = 100
            End With
            'Added Code for Audit LOG Enhancement
            Dim grdColPatient As New DataGridTextBoxColumn
            With grdColPatient
                .HeaderText = "Patient"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(14).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColUser As New DataGridTextBoxColumn
            With grdColUser
                .HeaderText = "User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(5).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColOutcome As New DataGridTextBoxColumn
            With grdColOutcome
                .HeaderText = "Outcome"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(6).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColMachineName As New DataGridTextBoxColumn
            With grdColMachineName
                .HeaderText = "Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(7).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColSoftwareComponent As New DataGridTextBoxColumn
            With grdColSoftwareComponent
                .HeaderText = "Software Component"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(8).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColLocalMachineIP As New DataGridTextBoxColumn
            With grdColLocalMachineIP
                .HeaderText = "Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(9).ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColRemoteMachineName As New DataGridTextBoxColumn
            With grdColRemoteMachineName
                .HeaderText = "Remote Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(10).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteMachineIP As New DataGridTextBoxColumn
            With grdColRemoteMachineIP
                .HeaderText = "Remote Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(11).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteUserName As New DataGridTextBoxColumn
            With grdColRemoteUserName
                .HeaderText = "Remote User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(12).ColumnName
                .NullText = ""
                .Width = 150
            End With


            Dim grdColDomain As New DataGridTextBoxColumn
            With grdColDomain
                .HeaderText = "Domain"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(13).ColumnName
                .NullText = ""
                .Width = 150
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColActivityCategory, grdColDescription, grdColPatientCode, grdColPatient, grdColUser, grdColOutcome, grdColMachineName, grdColSoftwareComponent, grdColLocalMachineIP, grdColRemoteMachineName, grdColRemoteMachineIP, grdColRemoteUserName, grdColDomain})
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            Dim _logText As String = ""

            objAudit.CreateLog(clsAudit.enmActivityType.Query, "The user queried and viewed the data to be archived from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Success)

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objAudit.CreateLog(clsAudit.enmActivityType.Query, "Error while viewing the data to be archived from" & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Success)
        Finally
            objAudit = Nothing
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnArchive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArchive.Click
        Dim objAudit As New clsAudit
        Dim objLogin As New clsLogin
        Try
            If MessageBox.Show("Are you sure you want to archive the Audit Trail data?" & vbCrLf & vbCrLf & "The data for the users which are logged in to the system will not be archived. Please request all the users to logoff if all the data needs to be archived." & vbCrLf & vbCrLf & "Please select Yes to continue and No to abort.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor

                If optAll.Checked = True Then
                    If cmbCategory.SelectedItem <> "ALL" Then
                        cmbCategory.SelectedItem = "ALL"
                    End If
                    If cmbUsers.SelectedValue <> 0 Then
                        cmbUsers.SelectedValue = 0
                    End If
                    If objAudit.ArchiveAuditReports(dtFrom.MinDate.Date, dtTo.MaxDate.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue) = True Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("AuditTrail data has been successfully archived", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully archived AuditTrail data.", gstrLoginName, gstrClientMachineName, 0, True)
                        '   objAudit = Nothing
                        Me.Close()
                        Exit Sub
                    End If
                Else
                    If Trim(txtPatient.Text) = "" Then
                        If objAudit.ArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue) = True Then
                            Me.Cursor = Cursors.Default
                            MessageBox.Show("AuditTrail data has been successfully archived", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'sarika  22nd feb
                            objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                            objLogin.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID)
                            '   objAudit = Nothing
                            Me.Close()
                            Exit Sub
                        End If
                    Else
                        'If Trim(cmbPatientType.Text) = "Patient ID" Then
                        If Trim(cmbPatientType.Text) = "Patient Code" Then
                            ' If IsNumeric(txtPatient.Text) = True Then
                            If objAudit.ArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, txtPatient.Text) = True Then
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("AuditTrail data has been successfully archived", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'sarika  22nd feb
                                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                                objLogin.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID)
                                '   objAudit = Nothing
                                Me.Close()
                                Exit Sub
                            End If
                            'End If
                        ElseIf Trim(cmbPatientType.Text) = "First Name" Then
                            If objAudit.ArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, , txtPatient.Text) = True Then
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("AuditTrail data has been successfully archived", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'sarika  22nd feb
                                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                                objLogin.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID)
                                ' objAudit = Nothing
                                Me.Close()
                                Exit Sub
                            End If
                        ElseIf Trim(cmbPatientType.Text) = "Last Name" Then
                            If objAudit.ArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, , , txtPatient.Text) = True Then
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("AuditTrail data has been successfully archived", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                                'sarika  22nd feb
                                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                                objLogin.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID)
                                '  objAudit = Nothing
                                Me.Close()


                                Exit Sub
                            End If
                        End If
                    End If
                End If

                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to archive auditTrail data ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'sarika 27th apr 2007
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to archive auditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)

            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika 27th apr 2007
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to archive auditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.SelectedItem & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
        Finally
            objLogin.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID)
            objAudit = Nothing
            objLogin = Nothing
        End Try
    End Sub


    Private Sub optAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAll.CheckedChanged
        If optAll.Checked = True Then
            optAll.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optAll.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optCustomize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCustomize.CheckedChanged
        If optCustomize.Checked = True Then
            optCustomize.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optCustomize.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
End Class
