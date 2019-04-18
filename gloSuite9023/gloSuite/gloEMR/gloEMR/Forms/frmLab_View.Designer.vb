<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLab_View
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then

            Try
                If (IsNothing(dttodate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dttodate)
                    Catch ex As Exception

                    End Try


                    dttodate.Dispose()
                    dttodate = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtfromdate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtfromdate)
                    Catch ex As Exception

                    End Try


                    dtfromdate.Dispose()
                    dtfromdate = Nothing
                End If
            Catch
            End Try

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_View))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtListSearch = New System.Windows.Forms.TextBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkenbdate = New System.Windows.Forms.CheckBox()
        Me.lblfrdate = New System.Windows.Forms.Label()
        Me.dtfromdate = New System.Windows.Forms.DateTimePicker()
        Me.dttodate = New System.Windows.Forms.DateTimePicker()
        Me.lbltodate = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbtransition = New System.Windows.Forms.ComboBox()
        Me.lblstruct = New System.Windows.Forms.Label()
        Me.cmbstruct = New System.Windows.Forms.ComboBox()
        Me.lblordtype = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cmbCriteria = New System.Windows.Forms.ComboBox()
        Me.cmbordtype = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.EventLog1 = New System.Diagnostics.EventLog()
        Me.c1TestLibrary = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnNormal = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1TestLibrary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.cmbtransition)
        Me.Panel2.Controls.Add(Me.lblstruct)
        Me.Panel2.Controls.Add(Me.cmbstruct)
        Me.Panel2.Controls.Add(Me.lblordtype)
        Me.Panel2.Controls.Add(Me.lblSearch)
        Me.Panel2.Controls.Add(Me.cmbCriteria)
        Me.Panel2.Controls.Add(Me.cmbordtype)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1058, 104)
        Me.Panel2.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.txtListSearch)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Location = New System.Drawing.Point(510, 13)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(263, 22)
        Me.Panel3.TabIndex = 61
        '
        'txtListSearch
        '
        Me.txtListSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtListSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtListSearch.ForeColor = System.Drawing.Color.Black
        Me.txtListSearch.Location = New System.Drawing.Point(6, 3)
        Me.txtListSearch.Name = "txtListSearch"
        Me.txtListSearch.Size = New System.Drawing.Size(232, 15)
        Me.txtListSearch.TabIndex = 1
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
        Me.btnClear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(241, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 20)
        Me.btnClear.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Gray
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(261, 1)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Gray
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(1, 21)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(261, 1)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Gray
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(262, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 22)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Gray
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 22)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "label4"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkenbdate)
        Me.GroupBox1.Controls.Add(Me.lblfrdate)
        Me.GroupBox1.Controls.Add(Me.dtfromdate)
        Me.GroupBox1.Controls.Add(Me.dttodate)
        Me.GroupBox1.Controls.Add(Me.lbltodate)
        Me.GroupBox1.Location = New System.Drawing.Point(803, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(235, 78)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'chkenbdate
        '
        Me.chkenbdate.AutoSize = True
        Me.chkenbdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.chkenbdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkenbdate.Location = New System.Drawing.Point(15, -2)
        Me.chkenbdate.Name = "chkenbdate"
        Me.chkenbdate.Size = New System.Drawing.Size(130, 18)
        Me.chkenbdate.TabIndex = 6
        Me.chkenbdate.Text = "Test Creation Date"
        Me.chkenbdate.UseVisualStyleBackColor = False
        '
        'lblfrdate
        '
        Me.lblfrdate.AutoSize = True
        Me.lblfrdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrdate.Location = New System.Drawing.Point(25, 23)
        Me.lblfrdate.Name = "lblfrdate"
        Me.lblfrdate.Size = New System.Drawing.Size(72, 14)
        Me.lblfrdate.TabIndex = 54
        Me.lblfrdate.Text = "From Date :"
        '
        'dtfromdate
        '
        Me.dtfromdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtfromdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtfromdate.Location = New System.Drawing.Point(99, 19)
        Me.dtfromdate.Name = "dtfromdate"
        Me.dtfromdate.Size = New System.Drawing.Size(113, 22)
        Me.dtfromdate.TabIndex = 7
        '
        'dttodate
        '
        Me.dttodate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dttodate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dttodate.Location = New System.Drawing.Point(99, 47)
        Me.dttodate.Name = "dttodate"
        Me.dttodate.Size = New System.Drawing.Size(113, 22)
        Me.dttodate.TabIndex = 8
        '
        'lbltodate
        '
        Me.lbltodate.AutoSize = True
        Me.lbltodate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltodate.Location = New System.Drawing.Point(37, 51)
        Me.lbltodate.Name = "lbltodate"
        Me.lbltodate.Size = New System.Drawing.Size(60, 14)
        Me.lbltodate.TabIndex = 55
        Me.lbltodate.Text = "To Date :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(335, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(173, 14)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "Outbound Transition Of Care :"
        '
        'cmbtransition
        '
        Me.cmbtransition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtransition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtransition.FormattingEnabled = True
        Me.cmbtransition.Items.AddRange(New Object() {"All", "Yes", "No"})
        Me.cmbtransition.Location = New System.Drawing.Point(510, 69)
        Me.cmbtransition.Name = "cmbtransition"
        Me.cmbtransition.Size = New System.Drawing.Size(263, 22)
        Me.cmbtransition.TabIndex = 5
        '
        'lblstruct
        '
        Me.lblstruct.AutoSize = True
        Me.lblstruct.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstruct.Location = New System.Drawing.Point(369, 45)
        Me.lblstruct.Name = "lblstruct"
        Me.lblstruct.Size = New System.Drawing.Size(139, 14)
        Me.lblstruct.TabIndex = 52
        Me.lblstruct.Text = "Structured Lab Results :"
        '
        'cmbstruct
        '
        Me.cmbstruct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstruct.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstruct.FormattingEnabled = True
        Me.cmbstruct.Items.AddRange(New Object() {"All", "Yes", "No"})
        Me.cmbstruct.Location = New System.Drawing.Point(510, 41)
        Me.cmbstruct.Name = "cmbstruct"
        Me.cmbstruct.Size = New System.Drawing.Size(263, 22)
        Me.cmbstruct.TabIndex = 4
        '
        'lblordtype
        '
        Me.lblordtype.AutoSize = True
        Me.lblordtype.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblordtype.Location = New System.Drawing.Point(10, 45)
        Me.lblordtype.Name = "lblordtype"
        Me.lblordtype.Size = New System.Drawing.Size(78, 14)
        Me.lblordtype.TabIndex = 51
        Me.lblordtype.Text = "Order Type :"
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(456, 17)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(52, 14)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCriteria
        '
        Me.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbCriteria.FormattingEnabled = True
        Me.cmbCriteria.Items.AddRange(New Object() {"Test", "Group"})
        Me.cmbCriteria.Location = New System.Drawing.Point(89, 13)
        Me.cmbCriteria.Name = "cmbCriteria"
        Me.cmbCriteria.Size = New System.Drawing.Size(247, 22)
        Me.cmbCriteria.TabIndex = 0
        '
        'cmbordtype
        '
        Me.cmbordtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbordtype.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbordtype.FormattingEnabled = True
        Me.cmbordtype.Location = New System.Drawing.Point(89, 41)
        Me.cmbordtype.Name = "cmbordtype"
        Me.cmbordtype.Size = New System.Drawing.Size(246, 22)
        Me.cmbordtype.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(24, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 14)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "Category :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(4, 20)
        Me.Label1.TabIndex = 46
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1054, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 96)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1051, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 97)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1052, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'EventLog1
        '
        Me.EventLog1.SynchronizingObject = Me
        '
        'c1TestLibrary
        '
        Me.c1TestLibrary.AllowEditing = False
        Me.c1TestLibrary.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1TestLibrary.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1TestLibrary.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me.c1TestLibrary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1TestLibrary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1TestLibrary.Location = New System.Drawing.Point(4, 1)
        Me.c1TestLibrary.Name = "c1TestLibrary"
        Me.c1TestLibrary.Rows.DefaultSize = 21
        Me.c1TestLibrary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1TestLibrary.ShowCellLabels = True
        Me.c1TestLibrary.Size = New System.Drawing.Size(1050, 592)
        Me.c1TestLibrary.StyleInfo = resources.GetString("c1TestLibrary.StyleInfo")
        Me.c1TestLibrary.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1058, 53)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnNormal, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1058, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "New"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnNormal
        '
        Me.ts_btnNormal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnNormal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnNormal.Image = CType(resources.GetObject("ts_btnNormal.Image"), System.Drawing.Image)
        Me.ts_btnNormal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnNormal.Name = "ts_btnNormal"
        Me.ts_btnNormal.Size = New System.Drawing.Size(86, 50)
        Me.ts_btnNormal.Tag = "Normal Note"
        Me.ts_btnNormal.Text = "N&ormal Note"
        Me.ts_btnNormal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnNormal.Visible = False
        '
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.c1TestLibrary)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 157)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1058, 596)
        Me.Panel1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 592)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1050, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 592)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1054, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 592)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1052, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmLab_View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1058, 753)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLab_View"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Orders & Results Setup"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.EventLog1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1TestLibrary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtListSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents EventLog1 As System.Diagnostics.EventLog
    Friend WithEvents c1TestLibrary As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ts_btnNormal As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbstruct As System.Windows.Forms.ComboBox
    Friend WithEvents cmbordtype As System.Windows.Forms.ComboBox
    Friend WithEvents lblstruct As System.Windows.Forms.Label
    Friend WithEvents lblordtype As System.Windows.Forms.Label
    Friend WithEvents chkenbdate As System.Windows.Forms.CheckBox
    Friend WithEvents dttodate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbltodate As System.Windows.Forms.Label
    Friend WithEvents lblfrdate As System.Windows.Forms.Label
    Friend WithEvents dtfromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbtransition As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
End Class
