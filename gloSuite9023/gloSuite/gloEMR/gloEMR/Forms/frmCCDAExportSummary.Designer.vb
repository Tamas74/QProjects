<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDAExportSummary
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
            Try
                If (IsNothing(FolderBrowserDialog1) = False) Then
                    FolderBrowserDialog1.Dispose()
                    FolderBrowserDialog1 = Nothing
                End If
            Catch ex As Exception

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDAExportSummary))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlProgessbar = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pgrbarStatus = New System.Windows.Forms.ProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlPatients = New System.Windows.Forms.Panel()
        Me.lstviewPatient = New System.Windows.Forms.ListView()
        Me.colPatName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblPatients = New System.Windows.Forms.Label()
        Me.btnClearAllPatient = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClearPatient = New System.Windows.Forms.Button()
        Me.btnBrowsePatient = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblTop = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblExport = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlStatusBar = New System.Windows.Forms.Panel()
        Me.pnlFormDate = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.chkintime = New System.Windows.Forms.CheckBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtExportLocation = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnBrowseLocation = New System.Windows.Forms.Button()
        Me.pnlProgessbar.SuspendLayout()
        Me.pnlPatients.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblTop.SuspendLayout()
        Me.pnlStatusBar.SuspendLayout()
        Me.pnlFormDate.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlProgessbar
        '
        Me.pnlProgessbar.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.pnlProgessbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProgessbar.Controls.Add(Me.Label7)
        Me.pnlProgessbar.Controls.Add(Me.pgrbarStatus)
        Me.pnlProgessbar.Controls.Add(Me.Label3)
        Me.pnlProgessbar.Controls.Add(Me.Label1)
        Me.pnlProgessbar.Controls.Add(Me.lblStatus)
        Me.pnlProgessbar.Controls.Add(Me.Label9)
        Me.pnlProgessbar.Controls.Add(Me.Label8)
        Me.pnlProgessbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProgessbar.Location = New System.Drawing.Point(3, 3)
        Me.pnlProgessbar.Name = "pnlProgessbar"
        Me.pnlProgessbar.Size = New System.Drawing.Size(709, 42)
        Me.pnlProgessbar.TabIndex = 100
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(707, 1)
        Me.Label7.TabIndex = 217
        '
        'pgrbarStatus
        '
        Me.pgrbarStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgrbarStatus.BackColor = System.Drawing.Color.White
        Me.pgrbarStatus.ForeColor = System.Drawing.Color.LimeGreen
        Me.pgrbarStatus.Location = New System.Drawing.Point(12, 19)
        Me.pgrbarStatus.Name = "pgrbarStatus"
        Me.pgrbarStatus.Size = New System.Drawing.Size(688, 18)
        Me.pgrbarStatus.Step = 1
        Me.pgrbarStatus.TabIndex = 4
        Me.pgrbarStatus.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(707, 1)
        Me.Label3.TabIndex = 216
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 214
        Me.Label1.Text = "Status :"
        '
        'lblStatus
        '
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(56, 3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(68, 13)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "//glosvr01"
        Me.lblStatus.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 42)
        Me.Label9.TabIndex = 219
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(708, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 42)
        Me.Label8.TabIndex = 218
        '
        'pnlPatients
        '
        Me.pnlPatients.Controls.Add(Me.btnBrowseLocation)
        Me.pnlPatients.Controls.Add(Me.Label14)
        Me.pnlPatients.Controls.Add(Me.txtExportLocation)
        Me.pnlPatients.Controls.Add(Me.lstviewPatient)
        Me.pnlPatients.Controls.Add(Me.lblPatients)
        Me.pnlPatients.Controls.Add(Me.btnClearAllPatient)
        Me.pnlPatients.Controls.Add(Me.Label2)
        Me.pnlPatients.Controls.Add(Me.btnClearPatient)
        Me.pnlPatients.Controls.Add(Me.btnBrowsePatient)
        Me.pnlPatients.Controls.Add(Me.Label6)
        Me.pnlPatients.Controls.Add(Me.Label4)
        Me.pnlPatients.Controls.Add(Me.Label5)
        Me.pnlPatients.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatients.Location = New System.Drawing.Point(0, 90)
        Me.pnlPatients.Name = "pnlPatients"
        Me.pnlPatients.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlPatients.Size = New System.Drawing.Size(715, 311)
        Me.pnlPatients.TabIndex = 0
        '
        'lstviewPatient
        '
        Me.lstviewPatient.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstviewPatient.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPatName})
        Me.lstviewPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstviewPatient.ForeColor = System.Drawing.Color.Black
        Me.lstviewPatient.FullRowSelect = True
        Me.lstviewPatient.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstviewPatient.Location = New System.Drawing.Point(140, 6)
        Me.lstviewPatient.MultiSelect = False
        Me.lstviewPatient.Name = "lstviewPatient"
        Me.lstviewPatient.Size = New System.Drawing.Size(537, 262)
        Me.lstviewPatient.TabIndex = 219
        Me.lstviewPatient.UseCompatibleStateImageBehavior = False
        Me.lstviewPatient.View = System.Windows.Forms.View.Details
        '
        'colPatName
        '
        Me.colPatName.Text = "Patient Name"
        Me.colPatName.Width = 360
        '
        'lblPatients
        '
        Me.lblPatients.AutoSize = True
        Me.lblPatients.BackColor = System.Drawing.Color.Transparent
        Me.lblPatients.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatients.Location = New System.Drawing.Point(12, 13)
        Me.lblPatients.Name = "lblPatients"
        Me.lblPatients.Size = New System.Drawing.Size(122, 14)
        Me.lblPatients.TabIndex = 213
        Me.lblPatients.Text = "Selected Patients :"
        '
        'btnClearAllPatient
        '
        Me.btnClearAllPatient.BackColor = System.Drawing.Color.Transparent
        Me.btnClearAllPatient.BackgroundImage = CType(resources.GetObject("btnClearAllPatient.BackgroundImage"), System.Drawing.Image)
        Me.btnClearAllPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearAllPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearAllPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearAllPatient.Image = CType(resources.GetObject("btnClearAllPatient.Image"), System.Drawing.Image)
        Me.btnClearAllPatient.Location = New System.Drawing.Point(680, 67)
        Me.btnClearAllPatient.Name = "btnClearAllPatient"
        Me.btnClearAllPatient.Size = New System.Drawing.Size(26, 24)
        Me.btnClearAllPatient.TabIndex = 3
        Me.btnClearAllPatient.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(707, 1)
        Me.Label2.TabIndex = 214
        '
        'btnClearPatient
        '
        Me.btnClearPatient.BackColor = System.Drawing.Color.Transparent
        Me.btnClearPatient.BackgroundImage = CType(resources.GetObject("btnClearPatient.BackgroundImage"), System.Drawing.Image)
        Me.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPatient.Image = CType(resources.GetObject("btnClearPatient.Image"), System.Drawing.Image)
        Me.btnClearPatient.Location = New System.Drawing.Point(680, 38)
        Me.btnClearPatient.Name = "btnClearPatient"
        Me.btnClearPatient.Size = New System.Drawing.Size(26, 24)
        Me.btnClearPatient.TabIndex = 2
        Me.btnClearPatient.UseVisualStyleBackColor = False
        '
        'btnBrowsePatient
        '
        Me.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePatient.BackgroundImage = CType(resources.GetObject("btnBrowsePatient.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePatient.Image = CType(resources.GetObject("btnBrowsePatient.Image"), System.Drawing.Image)
        Me.btnBrowsePatient.Location = New System.Drawing.Point(680, 9)
        Me.btnBrowsePatient.Name = "btnBrowsePatient"
        Me.btnBrowsePatient.Size = New System.Drawing.Size(26, 24)
        Me.btnBrowsePatient.TabIndex = 1
        Me.btnBrowsePatient.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 310)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(707, 1)
        Me.Label6.TabIndex = 218
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 311)
        Me.Label4.TabIndex = 216
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(711, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 311)
        Me.Label5.TabIndex = 217
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblTop)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(715, 55)
        Me.pnlToolStrip.TabIndex = 226
        '
        'tblTop
        '
        Me.tblTop.BackColor = System.Drawing.Color.Transparent
        Me.tblTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblTop.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblExport, Me.tblClose})
        Me.tblTop.Location = New System.Drawing.Point(0, 0)
        Me.tblTop.Name = "tblTop"
        Me.tblTop.Size = New System.Drawing.Size(715, 53)
        Me.tblTop.TabIndex = 0
        Me.tblTop.Text = "ToolStrip1"
        '
        'tblExport
        '
        Me.tblExport.Image = CType(resources.GetObject("tblExport.Image"), System.Drawing.Image)
        Me.tblExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblExport.Name = "tblExport"
        Me.tblExport.Size = New System.Drawing.Size(113, 50)
        Me.tblExport.Tag = "ExportSummary"
        Me.tblExport.Text = "&Export Summary"
        Me.tblExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblExport.ToolTipText = "Export Summary"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'pnlStatusBar
        '
        Me.pnlStatusBar.Controls.Add(Me.pnlProgessbar)
        Me.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatusBar.Location = New System.Drawing.Point(0, 401)
        Me.pnlStatusBar.Name = "pnlStatusBar"
        Me.pnlStatusBar.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlStatusBar.Size = New System.Drawing.Size(715, 48)
        Me.pnlStatusBar.TabIndex = 227
        '
        'pnlFormDate
        '
        Me.pnlFormDate.Controls.Add(Me.Label13)
        Me.pnlFormDate.Controls.Add(Me.Panel6)
        Me.pnlFormDate.Controls.Add(Me.chkDate)
        Me.pnlFormDate.Controls.Add(Me.lblFromDate)
        Me.pnlFormDate.Controls.Add(Me.Label10)
        Me.pnlFormDate.Controls.Add(Me.Label11)
        Me.pnlFormDate.Controls.Add(Me.Label12)
        Me.pnlFormDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFormDate.Location = New System.Drawing.Point(0, 55)
        Me.pnlFormDate.Name = "pnlFormDate"
        Me.pnlFormDate.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlFormDate.Size = New System.Drawing.Size(715, 35)
        Me.pnlFormDate.TabIndex = 228
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(711, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 27)
        Me.Label13.TabIndex = 220
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.lblToDate)
        Me.Panel6.Controls.Add(Me.chkintime)
        Me.Panel6.Controls.Add(Me.dtpFrom)
        Me.Panel6.Controls.Add(Me.dtpToDate)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(118, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(585, 27)
        Me.Panel6.TabIndex = 72
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(317, 6)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(67, 14)
        Me.lblToDate.TabIndex = 68
        Me.lblToDate.Text = "To Date : "
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkintime
        '
        Me.chkintime.AutoSize = True
        Me.chkintime.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkintime.Location = New System.Drawing.Point(0, 0)
        Me.chkintime.Name = "chkintime"
        Me.chkintime.Padding = New System.Windows.Forms.Padding(15, 3, 0, 0)
        Me.chkintime.Size = New System.Drawing.Size(112, 27)
        Me.chkintime.TabIndex = 71
        Me.chkintime.Text = "Include Time"
        Me.chkintime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkintime.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(114, 2)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(192, 22)
        Me.dtpFrom.TabIndex = 67
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(385, 2)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(195, 22)
        Me.dtpToDate.TabIndex = 69
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDate.Location = New System.Drawing.Point(88, 4)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.chkDate.Size = New System.Drawing.Size(30, 27)
        Me.chkDate.TabIndex = 70
        Me.chkDate.Text = ":"
        Me.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDate.UseVisualStyleBackColor = True
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(4, 4)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(84, 27)
        Me.lblFromDate.TabIndex = 66
        Me.lblFromDate.Text = "From Date "
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(4, 31)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(708, 1)
        Me.Label10.TabIndex = 71
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 28)
        Me.Label11.TabIndex = 221
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(709, 1)
        Me.Label12.TabIndex = 222
        '
        'txtExportLocation
        '
        Me.txtExportLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtExportLocation.Location = New System.Drawing.Point(140, 281)
        Me.txtExportLocation.Name = "txtExportLocation"
        Me.txtExportLocation.Size = New System.Drawing.Size(505, 22)
        Me.txtExportLocation.TabIndex = 220
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 289)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(113, 14)
        Me.Label14.TabIndex = 221
        Me.Label14.Text = "Export Location :"
        '
        'btnBrowseLocation
        '
        Me.btnBrowseLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseLocation.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseLocation.BackgroundImage = CType(resources.GetObject("btnBrowseLocation.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseLocation.Image = CType(resources.GetObject("btnBrowseLocation.Image"), System.Drawing.Image)
        Me.btnBrowseLocation.Location = New System.Drawing.Point(651, 279)
        Me.btnBrowseLocation.Name = "btnBrowseLocation"
        Me.btnBrowseLocation.Size = New System.Drawing.Size(26, 24)
        Me.btnBrowseLocation.TabIndex = 222
        Me.btnBrowseLocation.UseVisualStyleBackColor = False
        '
        'frmCCDAExportSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(715, 449)
        Me.Controls.Add(Me.pnlPatients)
        Me.Controls.Add(Me.pnlFormDate)
        Me.Controls.Add(Me.pnlStatusBar)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCCDAExportSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Export Summaries"
        Me.pnlProgessbar.ResumeLayout(False)
        Me.pnlProgessbar.PerformLayout()
        Me.pnlPatients.ResumeLayout(False)
        Me.pnlPatients.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        Me.pnlStatusBar.ResumeLayout(False)
        Me.pnlFormDate.ResumeLayout(False)
        Me.pnlFormDate.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents pnlProgessbar As System.Windows.Forms.Panel
    Friend WithEvents pgrbarStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Private WithEvents pnlPatients As System.Windows.Forms.Panel
    Friend WithEvents lblPatients As System.Windows.Forms.Label
    Friend WithEvents btnClearAllPatient As System.Windows.Forms.Button
    Friend WithEvents btnClearPatient As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePatient As System.Windows.Forms.Button
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblTop As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents tblExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lstviewPatient As System.Windows.Forms.ListView
    Friend WithEvents colPatName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlStatusBar As System.Windows.Forms.Panel
    Friend WithEvents pnlFormDate As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents chkintime As System.Windows.Forms.CheckBox
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseLocation As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtExportLocation As System.Windows.Forms.TextBox
End Class
