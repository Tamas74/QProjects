<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRpt_ReminderPatientLetter
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
                'Try
                '    If (IsNothing(PrintDialog1) = False) Then
                '        PrintDialog1.Dispose()
                '        PrintDialog1 = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                'Try
                '    If (IsNothing(printDocument1) = False) Then
                '        gloGlobal.cEventHelper.RemoveAllEventHandlers(printDocument1)
                '        printDocument1.Dispose()
                '        printDocument1 = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRpt_ReminderPatientLetter))
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tls_OverDue = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbtnSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnSendtoportal = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.rdoNonActivated = New System.Windows.Forms.RadioButton()
        Me.rdoActivated = New System.Windows.Forms.RadioButton()
        Me.rdoAll = New System.Windows.Forms.RadioButton()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.CmbCommunicationPref = New System.Windows.Forms.ComboBox()
        Me.btnSelectAllClearAll = New System.Windows.Forms.Button()
        Me.pnlWdPAtientLetters = New System.Windows.Forms.Panel()
        Me.wdPatientLetter = New AxDSOFramer.AxFramerControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_datefilter = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtp_FromDate = New System.Windows.Forms.DateTimePicker()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.trvTemplates = New System.Windows.Forms.TreeView()
        Me.label25 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.label17 = New System.Windows.Forms.Label()
        Me.label18 = New System.Windows.Forms.Label()
        Me.label19 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.ChkRemiderForUnSchedle = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.c1Patients = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblprgstatus = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        'Me.printDocument1 = New System.Drawing.Printing.PrintDocument()
        'Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls_OverDue.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        Me.pnlWdPAtientLetters.SuspendLayout()
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel2.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.panel1.SuspendLayout()
        CType(Me.c1Patients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_OverDue)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(655, 57)
        Me.pnl_tlspTOP.TabIndex = 211
        '
        'tls_OverDue
        '
        Me.tls_OverDue.BackColor = System.Drawing.Color.Transparent
        Me.tls_OverDue.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_OverDue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_OverDue.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls_OverDue.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_OverDue.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbtnSelectAll, Me.ts_btnRefresh, Me.ts_btnPrint, Me.btnSendtoportal, Me.tlsbtnSave, Me.ts_btnClose})
        Me.tls_OverDue.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_OverDue.Location = New System.Drawing.Point(0, 0)
        Me.tls_OverDue.Name = "tls_OverDue"
        Me.tls_OverDue.Size = New System.Drawing.Size(655, 53)
        Me.tls_OverDue.TabIndex = 0
        Me.tls_OverDue.Text = "toolStrip1"
        '
        'tlbtnSelectAll
        '
        Me.tlbtnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbtnSelectAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Resources.Select_All1
        Me.tlbtnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbtnSelectAll.Name = "tlbtnSelectAll"
        Me.tlbtnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbtnSelectAll.Tag = "Select"
        Me.tlbtnSelectAll.Text = "S&elect All"
        Me.tlbtnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
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
        'ts_btnPrint
        '
        Me.ts_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnPrint.ToolTipText = "Print"
        '
        'btnSendtoportal
        '
        Me.btnSendtoportal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSendtoportal.Image = CType(resources.GetObject("btnSendtoportal.Image"), System.Drawing.Image)
        Me.btnSendtoportal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSendtoportal.Name = "btnSendtoportal"
        Me.btnSendtoportal.Size = New System.Drawing.Size(101, 50)
        Me.btnSendtoportal.Text = "Send to portal"
        Me.btnSendtoportal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Tag = "Save"
        Me.tlsbtnSave.Text = "&Save&&Cls"
        Me.tlsbtnSave.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
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
        'rdoNonActivated
        '
        Me.rdoNonActivated.AutoSize = True
        Me.rdoNonActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoNonActivated.Location = New System.Drawing.Point(491, 4)
        Me.rdoNonActivated.Name = "rdoNonActivated"
        Me.rdoNonActivated.Size = New System.Drawing.Size(136, 18)
        Me.rdoNonActivated.TabIndex = 2
        Me.rdoNonActivated.Text = "Portal Not-Activated"
        Me.rdoNonActivated.UseVisualStyleBackColor = True
        '
        'rdoActivated
        '
        Me.rdoActivated.AutoSize = True
        Me.rdoActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoActivated.Location = New System.Drawing.Point(364, 4)
        Me.rdoActivated.Name = "rdoActivated"
        Me.rdoActivated.Size = New System.Drawing.Size(112, 18)
        Me.rdoActivated.TabIndex = 1
        Me.rdoActivated.Text = "Portal Activated"
        Me.rdoActivated.UseVisualStyleBackColor = True
        '
        'rdoAll
        '
        Me.rdoAll.AutoSize = True
        Me.rdoAll.Checked = True
        Me.rdoAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAll.Location = New System.Drawing.Point(309, 4)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(40, 18)
        Me.rdoAll.TabIndex = 0
        Me.rdoAll.TabStop = True
        Me.rdoAll.Text = "All"
        Me.rdoAll.UseVisualStyleBackColor = True
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlControl.Controls.Add(Me.Label15)
        Me.pnlControl.Controls.Add(Me.CmbCommunicationPref)
        Me.pnlControl.Controls.Add(Me.btnSelectAllClearAll)
        Me.pnlControl.Controls.Add(Me.pnlWdPAtientLetters)
        Me.pnlControl.Controls.Add(Me.Label1)
        Me.pnlControl.Controls.Add(Me.lbl_datefilter)
        Me.pnlControl.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlControl.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlControl.Controls.Add(Me.lbl_RightBrd)
        Me.pnlControl.Controls.Add(Me.lbl_TopBrd)
        Me.pnlControl.Controls.Add(Me.dtp_ToDate)
        Me.pnlControl.Controls.Add(Me.dtp_FromDate)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlControl.Location = New System.Drawing.Point(0, 57)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlControl.Size = New System.Drawing.Size(655, 78)
        Me.pnlControl.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(281, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(117, 14)
        Me.Label15.TabIndex = 237
        Me.Label15.Text = "Comm. Preference :"
        '
        'CmbCommunicationPref
        '
        Me.CmbCommunicationPref.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCommunicationPref.Items.AddRange(New Object() {"", "No Pref", "Email", "Fax", "gloStream Direct", "Mail", "Mobile", "Phone", "Secure Message"})
        Me.CmbCommunicationPref.Location = New System.Drawing.Point(403, 11)
        Me.CmbCommunicationPref.Name = "CmbCommunicationPref"
        Me.CmbCommunicationPref.Size = New System.Drawing.Size(120, 22)
        Me.CmbCommunicationPref.TabIndex = 1
        '
        'btnSelectAllClearAll
        '
        Me.btnSelectAllClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAllClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAllClearAll.Location = New System.Drawing.Point(537, 9)
        Me.btnSelectAllClearAll.Name = "btnSelectAllClearAll"
        Me.btnSelectAllClearAll.Size = New System.Drawing.Size(87, 25)
        Me.btnSelectAllClearAll.TabIndex = 5
        Me.btnSelectAllClearAll.Tag = "Select"
        Me.btnSelectAllClearAll.Text = "&Select All"
        Me.btnSelectAllClearAll.UseVisualStyleBackColor = True
        Me.btnSelectAllClearAll.Visible = False
        '
        'pnlWdPAtientLetters
        '
        Me.pnlWdPAtientLetters.Controls.Add(Me.wdPatientLetter)
        Me.pnlWdPAtientLetters.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlWdPAtientLetters.Location = New System.Drawing.Point(632, 1)
        Me.pnlWdPAtientLetters.Name = "pnlWdPAtientLetters"
        Me.pnlWdPAtientLetters.Size = New System.Drawing.Size(19, 73)
        Me.pnlWdPAtientLetters.TabIndex = 234
        Me.pnlWdPAtientLetters.Visible = False
        '
        'wdPatientLetter
        '
        Me.wdPatientLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientLetter.Enabled = True
        Me.wdPatientLetter.Location = New System.Drawing.Point(0, 0)
        Me.wdPatientLetter.Name = "wdPatientLetter"
        Me.wdPatientLetter.OcxState = CType(resources.GetObject("wdPatientLetter.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientLetter.Size = New System.Drawing.Size(19, 73)
        Me.wdPatientLetter.TabIndex = 0
        Me.wdPatientLetter.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Reminder End Date :"
        '
        'lbl_datefilter
        '
        Me.lbl_datefilter.AutoSize = True
        Me.lbl_datefilter.Location = New System.Drawing.Point(10, 14)
        Me.lbl_datefilter.Name = "lbl_datefilter"
        Me.lbl_datefilter.Size = New System.Drawing.Size(131, 14)
        Me.lbl_datefilter.TabIndex = 1
        Me.lbl_datefilter.Text = " Reminder Start Date :"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 74)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(647, 1)
        Me.lbl_BottomBrd.TabIndex = 218
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 74)
        Me.lbl_LeftBrd.TabIndex = 0
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(651, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 74)
        Me.lbl_RightBrd.TabIndex = 216
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(649, 1)
        Me.lbl_TopBrd.TabIndex = 215
        Me.lbl_TopBrd.Text = "label1"
        '
        'dtp_ToDate
        '
        Me.dtp_ToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtp_ToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtp_ToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtp_ToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtp_ToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtp_ToDate.CustomFormat = "MM/dd/yyyy"
        Me.dtp_ToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_ToDate.Location = New System.Drawing.Point(145, 40)
        Me.dtp_ToDate.Name = "dtp_ToDate"
        Me.dtp_ToDate.Size = New System.Drawing.Size(125, 22)
        Me.dtp_ToDate.TabIndex = 2
        '
        'dtp_FromDate
        '
        Me.dtp_FromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtp_FromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtp_FromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtp_FromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtp_FromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtp_FromDate.CustomFormat = "MM/dd/yyyy"
        Me.dtp_FromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_FromDate.Location = New System.Drawing.Point(145, 10)
        Me.dtp_FromDate.Name = "dtp_FromDate"
        Me.dtp_FromDate.Size = New System.Drawing.Size(125, 22)
        Me.dtp_FromDate.TabIndex = 0
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel2.Controls.Add(Me.panel4)
        Me.panel2.Controls.Add(Me.Panel6)
        Me.panel2.Controls.Add(Me.panel1)
        Me.panel2.Controls.Add(Me.Panel3)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.panel2.Location = New System.Drawing.Point(0, 135)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(655, 415)
        Me.panel2.TabIndex = 213
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel4.Controls.Add(Me.trvTemplates)
        Me.panel4.Controls.Add(Me.label25)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Controls.Add(Me.label17)
        Me.panel4.Controls.Add(Me.label18)
        Me.panel4.Controls.Add(Me.label19)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.panel4.Location = New System.Drawing.Point(0, 227)
        Me.panel4.Name = "panel4"
        Me.panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.panel4.Size = New System.Drawing.Size(655, 188)
        Me.panel4.TabIndex = 212
        '
        'trvTemplates
        '
        Me.trvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplates.CheckBoxes = True
        Me.trvTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplates.ForeColor = System.Drawing.Color.Black
        Me.trvTemplates.HideSelection = False
        Me.trvTemplates.Location = New System.Drawing.Point(7, 4)
        Me.trvTemplates.Name = "trvTemplates"
        Me.trvTemplates.Size = New System.Drawing.Size(644, 180)
        Me.trvTemplates.TabIndex = 0
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.White
        Me.label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label25.Location = New System.Drawing.Point(7, 1)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(644, 3)
        Me.label25.TabIndex = 221
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.White
        Me.label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label24.Location = New System.Drawing.Point(4, 1)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(3, 183)
        Me.label24.TabIndex = 220
        '
        'label17
        '
        Me.label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label17.Location = New System.Drawing.Point(4, 184)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(647, 1)
        Me.label17.TabIndex = 218
        Me.label17.Text = "label2"
        '
        'label18
        '
        Me.label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.Location = New System.Drawing.Point(3, 1)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(1, 184)
        Me.label18.TabIndex = 217
        Me.label18.Text = "label4"
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label19.Location = New System.Drawing.Point(651, 1)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(1, 184)
        Me.label19.TabIndex = 216
        Me.label19.Text = "label3"
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.Location = New System.Drawing.Point(3, 0)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(649, 1)
        Me.label20.TabIndex = 215
        Me.label20.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 199)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(655, 28)
        Me.Panel6.TabIndex = 3
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.ChkRemiderForUnSchedle)
        Me.Panel7.Controls.Add(Me.Label8)
        Me.Panel7.Controls.Add(Me.Label13)
        Me.Panel7.Controls.Add(Me.Label14)
        Me.Panel7.Controls.Add(Me.Label16)
        Me.Panel7.Controls.Add(Me.label3)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(3, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(649, 25)
        Me.Panel7.TabIndex = 19
        '
        'ChkRemiderForUnSchedle
        '
        Me.ChkRemiderForUnSchedle.AutoSize = True
        Me.ChkRemiderForUnSchedle.BackColor = System.Drawing.Color.Transparent
        Me.ChkRemiderForUnSchedle.Checked = True
        Me.ChkRemiderForUnSchedle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkRemiderForUnSchedle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRemiderForUnSchedle.Location = New System.Drawing.Point(444, 5)
        Me.ChkRemiderForUnSchedle.Name = "ChkRemiderForUnSchedle"
        Me.ChkRemiderForUnSchedle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkRemiderForUnSchedle.Size = New System.Drawing.Size(198, 18)
        Me.ChkRemiderForUnSchedle.TabIndex = 1
        Me.ChkRemiderForUnSchedle.Text = "Reminder for Unscheduled Care"
        Me.ChkRemiderForUnSchedle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkRemiderForUnSchedle.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(647, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 24)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(648, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 24)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(649, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'label3
        '
        Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(0, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(649, 25)
        Me.label3.TabIndex = 210
        Me.label3.Text = "  Templates "
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.c1Patients)
        Me.panel1.Controls.Add(Me.label9)
        Me.panel1.Controls.Add(Me.label11)
        Me.panel1.Controls.Add(Me.label12)
        Me.panel1.Controls.Add(Me.label10)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 29)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.panel1.Size = New System.Drawing.Size(655, 170)
        Me.panel1.TabIndex = 2
        '
        'c1Patients
        '
        Me.c1Patients.BackColor = System.Drawing.Color.White
        Me.c1Patients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Patients.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1Patients.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Patients.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1Patients.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Patients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Patients.Location = New System.Drawing.Point(4, 1)
        Me.c1Patients.Name = "c1Patients"
        Me.c1Patients.Rows.Count = 1
        Me.c1Patients.Rows.DefaultSize = 19
        Me.c1Patients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Patients.ShowCellLabels = True
        Me.c1Patients.Size = New System.Drawing.Size(647, 165)
        Me.c1Patients.StyleInfo = resources.GetString("c1Patients.StyleInfo")
        Me.c1Patients.TabIndex = 1
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label9.Location = New System.Drawing.Point(4, 166)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(647, 1)
        Me.label9.TabIndex = 218
        Me.label9.Text = "label2"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label11.Location = New System.Drawing.Point(651, 1)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 166)
        Me.label11.TabIndex = 216
        Me.label11.Text = "label3"
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(4, 0)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(648, 1)
        Me.label12.TabIndex = 0
        Me.label12.Text = "label1"
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.Location = New System.Drawing.Point(3, 0)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(1, 167)
        Me.label10.TabIndex = 217
        Me.label10.Text = "label4"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(655, 29)
        Me.Panel3.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.lblprgstatus)
        Me.Panel5.Controls.Add(Me.rdoNonActivated)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.rdoActivated)
        Me.Panel5.Controls.Add(Me.rdoAll)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.label2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(3, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(649, 26)
        Me.Panel5.TabIndex = 19
        '
        'lblprgstatus
        '
        Me.lblprgstatus.AutoSize = True
        Me.lblprgstatus.Location = New System.Drawing.Point(82, 9)
        Me.lblprgstatus.Name = "lblprgstatus"
        Me.lblprgstatus.Size = New System.Drawing.Size(0, 14)
        Me.lblprgstatus.TabIndex = 239
        Me.lblprgstatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(647, 1)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(647, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 26)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(648, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 26)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label3"
        '
        'label2
        '
        Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(0, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(649, 26)
        Me.label2.TabIndex = 2
        Me.label2.Text = "  Patient "
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'PrintDialog1
        '
        'Me.PrintDialog1.UseEXDialog = True
        '
        'frmRpt_ReminderPatientLetter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(655, 550)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRpt_ReminderPatientLetter"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Patient Reminder Letters"
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls_OverDue.ResumeLayout(False)
        Me.tls_OverDue.PerformLayout()
        Me.pnlControl.ResumeLayout(False)
        Me.pnlControl.PerformLayout()
        Me.pnlWdPAtientLetters.ResumeLayout(False)
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel2.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.panel1.ResumeLayout(False)
        CType(Me.c1Patients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls_OverDue As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlControl As System.Windows.Forms.Panel
    Private WithEvents lbl_datefilter As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents dtp_ToDate As System.Windows.Forms.DateTimePicker
    Private WithEvents dtp_FromDate As System.Windows.Forms.DateTimePicker
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents trvTemplates As System.Windows.Forms.TreeView
    Private WithEvents label25 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents btnSelectAllClearAll As System.Windows.Forms.Button
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents wdPatientLetter As AxDSOFramer.AxFramerControl
    Private WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlWdPAtientLetters As System.Windows.Forms.Panel
    Private WithEvents c1Patients As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents tlbtnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    '' Friend WithEvents CmbCommunicationPref As CheckComboBox
    Friend WithEvents CmbCommunicationPref As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ChkRemiderForUnSchedle As System.Windows.Forms.CheckBox
    Friend WithEvents btnSendtoportal As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents rdoNonActivated As System.Windows.Forms.RadioButton
    Friend WithEvents rdoActivated As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblprgstatus As System.Windows.Forms.Label
    'Private WithEvents printDocument1 As System.Drawing.Printing.PrintDocument
    'Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
End Class
