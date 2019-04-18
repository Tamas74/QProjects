'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Public Class frmUnArchive
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
    Friend WithEvents dgData As clsDataGrid ' System.Windows.Forms.DataGrid
    Friend WithEvents optCustomize As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panCustomize As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnArchive As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlCustomized As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnArchive))
        Me.optCustomize = New System.Windows.Forms.RadioButton
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.panCustomize = New System.Windows.Forms.Panel
        Me.pnlCustomized = New System.Windows.Forms.Panel
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPatient = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnPreview = New System.Windows.Forms.ToolStripButton
        Me.btnArchive = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.dgData = New gloEMRAdmin.clsDataGrid
        Me.panCustomize.SuspendLayout()
        Me.pnlCustomized.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'optCustomize
        '
        Me.optCustomize.AutoSize = True
        Me.optCustomize.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCustomize.Location = New System.Drawing.Point(195, 17)
        Me.optCustomize.Name = "optCustomize"
        Me.optCustomize.Size = New System.Drawing.Size(80, 18)
        Me.optCustomize.TabIndex = 2
        Me.optCustomize.Text = "C&ustomize"
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAll.Location = New System.Drawing.Point(134, 17)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(40, 18)
        Me.optAll.TabIndex = 1
        Me.optAll.TabStop = True
        Me.optAll.Text = "&All"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Archive Type :"
        '
        'panCustomize
        '
        Me.panCustomize.Controls.Add(Me.pnlCustomized)
        Me.panCustomize.Controls.Add(Me.optCustomize)
        Me.panCustomize.Controls.Add(Me.optAll)
        Me.panCustomize.Controls.Add(Me.Label1)
        Me.panCustomize.Controls.Add(Me.Label7)
        Me.panCustomize.Controls.Add(Me.Label8)
        Me.panCustomize.Controls.Add(Me.Label9)
        Me.panCustomize.Controls.Add(Me.Label10)
        Me.panCustomize.Dock = System.Windows.Forms.DockStyle.Top
        Me.panCustomize.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panCustomize.Location = New System.Drawing.Point(0, 56)
        Me.panCustomize.Name = "panCustomize"
        Me.panCustomize.Padding = New System.Windows.Forms.Padding(3)
        Me.panCustomize.Size = New System.Drawing.Size(850, 107)
        Me.panCustomize.TabIndex = 3
        '
        'pnlCustomized
        '
        Me.pnlCustomized.Controls.Add(Me.cmbCategory)
        Me.pnlCustomized.Controls.Add(Me.Label6)
        Me.pnlCustomized.Controls.Add(Me.Label4)
        Me.pnlCustomized.Controls.Add(Me.dtFrom)
        Me.pnlCustomized.Controls.Add(Me.cmbUsers)
        Me.pnlCustomized.Controls.Add(Me.Label3)
        Me.pnlCustomized.Controls.Add(Me.dtTo)
        Me.pnlCustomized.Controls.Add(Me.Label5)
        Me.pnlCustomized.Controls.Add(Me.Label2)
        Me.pnlCustomized.Controls.Add(Me.txtPatient)
        Me.pnlCustomized.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCustomized.Location = New System.Drawing.Point(4, 39)
        Me.pnlCustomized.Name = "pnlCustomized"
        Me.pnlCustomized.Size = New System.Drawing.Size(842, 64)
        Me.pnlCustomized.TabIndex = 15
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCategory.Location = New System.Drawing.Point(549, 4)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(227, 22)
        Me.cmbCategory.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(55, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "From :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Patient Code :"
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(103, 4)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(105, 22)
        Me.dtFrom.TabIndex = 1
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsers.Location = New System.Drawing.Point(549, 32)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(227, 22)
        Me.cmbUsers.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(245, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "&To :"
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(277, 4)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(109, 22)
        Me.dtTo.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(459, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Activity Type :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(501, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Users :"
        '
        'txtPatient
        '
        Me.txtPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatient.Location = New System.Drawing.Point(103, 32)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(283, 22)
        Me.txtPatient.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(4, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(842, 1)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 100)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(846, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 100)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(844, 1)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.dgData)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 163)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(850, 535)
        Me.Panel2.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 531)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(842, 1)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 531)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(846, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 531)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(844, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label1"
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
        Me.pnl_tlsp_Top.TabIndex = 19
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
        Me.btnArchive.Size = New System.Drawing.Size(59, 50)
        Me.btnArchive.Text = "&Restore"
        Me.btnArchive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnArchive.ToolTipText = "Restore"
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
        'dgData
        '
        Me.dgData.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgData.BackColor = System.Drawing.Color.GhostWhite
        Me.dgData.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgData.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.CaptionForeColor = System.Drawing.Color.White
        Me.dgData.DataMember = ""
        Me.dgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgData.ForeColor = System.Drawing.Color.Black
        Me.dgData.FullRowSelect = True
        Me.dgData.GridLineColor = System.Drawing.Color.Black
        Me.dgData.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgData.HeaderForeColor = System.Drawing.Color.Black
        Me.dgData.Location = New System.Drawing.Point(3, 0)
        Me.dgData.Name = "dgData"
        Me.dgData.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgData.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgData.ReadOnly = True
        Me.dgData.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgData.SelectionForeColor = System.Drawing.Color.Black
        Me.dgData.Size = New System.Drawing.Size(844, 532)
        Me.dgData.TabIndex = 0
        '
        'frmUnArchive
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(850, 698)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.panCustomize)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(856, 730)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(856, 730)
        Me.Name = "frmUnArchive"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Restore Archive"
        Me.panCustomize.ResumeLayout(False)
        Me.panCustomize.PerformLayout()
        Me.pnlCustomized.ResumeLayout(False)
        Me.pnlCustomized.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        CType(Me.dgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub optAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAll.Click
        Try
            pnlCustomized.Enabled = False
            btnPreview.Enabled = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optCustomize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCustomize.Click
        Try
            pnlCustomized.Enabled = True
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
            Call Fill_ActivityTypesUsers()
            pnlCustomized.Enabled = False
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
        clAudit = objAudit.Fill_ArchivedAuditCategory
        cmbCategory.Items.Clear()
        cmbCategory.Items.Add("All")
        For nCount = 1 To clAudit.Count
            cmbCategory.Items.Add(clAudit.Item(nCount))
        Next
        cmbCategory.SelectedIndex = 0
        Dim dtUsers As DataTable
        dtUsers = objAudit.Fill_ArchivedUsers(False)
        If (Not IsNothing(dtUsers)) Then


            Dim dr As DataRow
            dr = dtUsers.NewRow
            dr(0) = "0"
            dr(1) = "All"
            dtUsers.Rows.Add(dr)
            cmbUsers.DataSource = dtUsers
            cmbUsers.ValueMember = dtUsers.Columns(0).ColumnName
            cmbUsers.DisplayMember = dtUsers.Columns(1).ColumnName
            'cmbUsers.Items.Clear()
            'cmbUsers.Items.Add("All")
            'For nCount = 0 To dtUsers.Rows.Count - 1
            '    cmbUsers.Items.Add(dtUsers.Rows(nCount).Item(1))
            'Next
            cmbUsers.SelectedIndex = cmbUsers.Items.Count - 1
        End If
        objAudit = Nothing
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            Dim dt As New DataTable()
            Me.Cursor = Cursors.WaitCursor
            Dim objAuditReports As New clsAudit
            Dim dtAuditReports As New DataTable
            If Trim(txtPatient.Text) = "" Then
                dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, gstrDatabaseName)
            Else
                ' If IsNumeric(txtPatient.Text) = True Then
                'dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, gstrDatabaseName, CInt(txtPatient.Text))
                'Else
                dtAuditReports = objAuditReports.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, gstrDatabaseName, txtPatient.Text)
                'End If
            End If
            If txtPatient.Text <> "" And dtAuditReports.Rows.Count >= 0 Then
                Dim dv As DataView = dtAuditReports.DefaultView
                dv.RowFilter = "PatientCode Like '" & txtPatient.Text.Replace("'", "''") & "%'"

                dgData.DataSource = dv

                'dt = dv.Table()

                'dt.Columns.Remove("Patientcode")
                'dt.Columns.Remove("FirstName")
                'dt.Columns.Remove("LastName")
            Else
                dgData.DataSource = dtAuditReports

            End If
            dgData.CaptionText = "Archived Audit Report"
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

            Dim grdColPatient As New DataGridTextBoxColumn
            With grdColPatient
                .HeaderText = "Patient ID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(4).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColUser As New DataGridTextBoxColumn
            With grdColUser
                .HeaderText = "User"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(5).ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColMachine As New DataGridTextBoxColumn
            With grdColMachine
                .HeaderText = "Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(6).ColumnName
                .NullText = ""
                .Width = 120
            End With

            Dim grdColSoftwareComponent As New DataGridTextBoxColumn
            With grdColSoftwareComponent
                .HeaderText = "Software Component"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(7).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColOutcome As New DataGridTextBoxColumn
            With grdColOutcome
                .HeaderText = "Outcome"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(8).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdPatientCode As New DataGridTextBoxColumn
            With grdPatientCode
                .HeaderText = "Patient Code"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(9).ColumnName
                .NullText = ""
                .Width = 100
            End With

            
            'Added Code for Audit LOG Enhancement
            Dim grdColLocalMachineIP As New DataGridTextBoxColumn
            With grdColLocalMachineIP
                .HeaderText = "Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(12).ColumnName
                .NullText = ""
                .Width = 100
            End With


            Dim grdColRemoteMachineName As New DataGridTextBoxColumn
            With grdColRemoteMachineName
                .HeaderText = "Remote Machine Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(13).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteMachineIP As New DataGridTextBoxColumn
            With grdColRemoteMachineIP
                .HeaderText = "Remote Machine IP"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(14).ColumnName
                .NullText = ""
                .Width = 150
            End With

            Dim grdColRemoteUserName As New DataGridTextBoxColumn
            With grdColRemoteUserName
                .HeaderText = "Remote User Name"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(15).ColumnName
                .NullText = ""
                .Width = 150
            End With


            Dim grdColDomain As New DataGridTextBoxColumn
            With grdColDomain
                .HeaderText = "Domain"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAuditReports.Columns(16).ColumnName
                .NullText = ""
                .Width = 150
            End With


            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColAuditReportID, grdColActivityDate, grdColDescription, grdColUser, grdColMachine, grdColActivityCategory, grdColPatient, grdPatientCode, grdColSoftwareComponent, grdColOutcome, grdColLocalMachineIP, grdColRemoteMachineName, grdColRemoteMachineIP, grdColRemoteUserName, grdColDomain}) ', grdFirstName, grdLastName
            dgData.TableStyles.Clear()
            dgData.TableStyles.Add(grdTableStyle)

            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "User queried archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0)
            objAudit = Nothing

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "Failed to preview archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
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
        Try
            Dim dt As New DataTable()
            Dim dtAuditReports As New DataTable
            If MessageBox.Show("Are you sure you want to restore the Audit Trail data?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Dim objAudit As New clsAudit
                If optAll.Checked = True Then
                    If cmbCategory.SelectedItem <> "ALL" Then
                        cmbCategory.SelectedItem = "ALL"
                    End If
                    If cmbUsers.SelectedValue <> 0 Then
                        cmbUsers.SelectedValue = 0
                    End If
                    If objAudit.RestoreArchiveAuditReports(dtFrom.MinDate.Date, dtTo.MaxDate.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, txtPatient.Text) = True Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("AuditTrail data has been successfully restored", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully restored AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                        Me.Close()
                        Exit Sub
                    End If
                Else
                    If Trim(txtPatient.Text) = "" Then
                        dtAuditReports = objAudit.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, gstrDatabaseName, txtPatient.Text)
                        dtAuditReports.Columns.Remove("Patientcode")
                        dtAuditReports.Columns.Remove("FirstName")
                        dtAuditReports.Columns.Remove("LastName")

                        If dtAuditReports.Rows.Count > 0 Then
                            If objAudit.RestoreArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, txtPatient.Text) = True Then
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("AuditTrail data has been successfully restored", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully restored AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                                Me.Close()
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("No data is present to restore", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub

                        End If

                    Else
                        dtAuditReports = objAudit.RetrieveArchivedAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, gstrDatabaseName, txtPatient.Text)
                        Dim dv As DataView = dtAuditReports.DefaultView
                        dv.RowFilter = "PatientCode Like '" & txtPatient.Text.Replace("'", "''") & "%'"
                        dt = dv.ToTable()
                        dt.Columns.Remove("Patientcode")
                        dt.Columns.Remove("FirstName")
                        dt.Columns.Remove("LastName")

                        If dt.Rows.Count > 0 Then
                            If objAudit.RestoreArchiveAuditReports(dtFrom.Value.Date, dtTo.Value.Date, cmbCategory.SelectedItem, cmbUsers.SelectedValue, txtPatient.Text) = True Then
                                Me.Cursor = Cursors.Default
                                MessageBox.Show("AuditTrail data has been successfully restored", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                objAudit.CreateLog(clsAudit.enmActivityType.Other, gstrLoginName & " user has successfully restored AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, True)
                                Me.Close()
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("No data is present to restore", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If

                    End If
                End If
                Me.Cursor = Cursors.Default
                MessageBox.Show("Unable to restore archived AuditTrail data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to restore archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
                objAudit = Nothing
            End If
           
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unable to restore archived AuditTrail data from " & dtFrom.Value.Date & " to " & dtTo.Value.Date & " for the Category " & cmbCategory.Text & " for the user " & cmbUsers.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
        End Try
    End Sub

End Class
