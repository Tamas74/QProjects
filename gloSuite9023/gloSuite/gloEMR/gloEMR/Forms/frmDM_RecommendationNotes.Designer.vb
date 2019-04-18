<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_RecommendationNotes
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpSatisFiedDate}
                Dim cntControls() As System.Windows.Forms.Control = {dtpSatisFiedDate}
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_RecommendationNotes))
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.pnl_tls_ = New System.Windows.Forms.Panel()
        Me.tlsDetails = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblSatisFiedDate = New System.Windows.Forms.Label()
        Me.dtpSatisFiedDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblOrder = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlReason = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlEdit = New System.Windows.Forms.Panel()
        Me.pnlSnoozeDetails = New System.Windows.Forms.Panel()
        Me.cmbSnoozeUnit = New System.Windows.Forms.ComboBox()
        Me.cmbSnoozePeriod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSnoozePeriod = New System.Windows.Forms.Label()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsDetails.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlReason.SuspendLayout()
        Me.pnlEdit.SuspendLayout()
        Me.pnlSnoozeDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackgroundImage = CType(resources.GetObject("miniToolStrip.BackgroundImage"), System.Drawing.Image)
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.miniToolStrip.Location = New System.Drawing.Point(124, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(425, 53)
        Me.miniToolStrip.TabIndex = 15
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsDetails)
        Me.pnl_tls_.Controls.Add(Me.Label43)
        Me.pnl_tls_.Controls.Add(Me.Label44)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(488, 54)
        Me.pnl_tls_.TabIndex = 21
        '
        'tlsDetails
        '
        Me.tlsDetails.BackColor = System.Drawing.Color.Transparent
        Me.tlsDetails.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDetails.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok, Me.btn_tls_Cancel})
        Me.tlsDetails.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDetails.Location = New System.Drawing.Point(1, 1)
        Me.tlsDetails.Name = "tlsDetails"
        Me.tlsDetails.Size = New System.Drawing.Size(487, 53)
        Me.tlsDetails.TabIndex = 0
        Me.tlsDetails.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Tag = "OK"
        Me.btn_tls_Ok.Text = "&Save&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Cancel.Tag = "Cancel"
        Me.btn_tls_Cancel.Text = "&Close"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 53)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label4"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(488, 1)
        Me.Label44.TabIndex = 9
        Me.Label44.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(488, 28)
        Me.Panel3.TabIndex = 7
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.lblSatisFiedDate)
        Me.Panel7.Controls.Add(Me.dtpSatisFiedDate)
        Me.Panel7.Controls.Add(Me.Label12)
        Me.Panel7.Controls.Add(Me.Label13)
        Me.Panel7.Controls.Add(Me.Label14)
        Me.Panel7.Controls.Add(Me.lblOrder)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(485, 22)
        Me.Panel7.TabIndex = 9
        '
        'lblSatisFiedDate
        '
        Me.lblSatisFiedDate.AutoSize = True
        Me.lblSatisFiedDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSatisFiedDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSatisFiedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSatisFiedDate.Location = New System.Drawing.Point(12, 4)
        Me.lblSatisFiedDate.Name = "lblSatisFiedDate"
        Me.lblSatisFiedDate.Size = New System.Drawing.Size(101, 14)
        Me.lblSatisFiedDate.TabIndex = 10
        Me.lblSatisFiedDate.Text = "Satisfied Date :"
        Me.lblSatisFiedDate.Visible = False
        '
        'dtpSatisFiedDate
        '
        Me.dtpSatisFiedDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.dtpSatisFiedDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpSatisFiedDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpSatisFiedDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpSatisFiedDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpSatisFiedDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpSatisFiedDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpSatisFiedDate.Enabled = False
        Me.dtpSatisFiedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSatisFiedDate.Location = New System.Drawing.Point(116, 1)
        Me.dtpSatisFiedDate.Name = "dtpSatisFiedDate"
        Me.dtpSatisFiedDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpSatisFiedDate.TabIndex = 9
        Me.dtpSatisFiedDate.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 20)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(484, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 20)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(485, 1)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "label1"
        '
        'lblOrder
        '
        Me.lblOrder.BackColor = System.Drawing.Color.Transparent
        Me.lblOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblOrder.Location = New System.Drawing.Point(0, 0)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(485, 21)
        Me.lblOrder.TabIndex = 2
        Me.lblOrder.Text = "  "
        Me.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(0, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(485, 1)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "label2"
        '
        'pnlReason
        '
        Me.pnlReason.Controls.Add(Me.Label23)
        Me.pnlReason.Controls.Add(Me.Label24)
        Me.pnlReason.Controls.Add(Me.Label25)
        Me.pnlReason.Controls.Add(Me.Label26)
        Me.pnlReason.Controls.Add(Me.txtNotes)
        Me.pnlReason.Controls.Add(Me.Label4)
        Me.pnlReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReason.Location = New System.Drawing.Point(0, 28)
        Me.pnlReason.Name = "pnlReason"
        Me.pnlReason.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlReason.Size = New System.Drawing.Size(488, 114)
        Me.pnlReason.TabIndex = 16
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 110)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(483, 1)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 110)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(484, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 110)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(485, 1)
        Me.Label26.TabIndex = 5
        Me.Label26.Text = "label1"
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.Color.White
        Me.txtNotes.Location = New System.Drawing.Point(68, 15)
        Me.txtNotes.MaxLength = 255
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(400, 81)
        Me.txtNotes.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(12, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Notes :"
        '
        'pnlEdit
        '
        Me.pnlEdit.BackColor = System.Drawing.Color.Transparent
        Me.pnlEdit.Controls.Add(Me.pnlReason)
        Me.pnlEdit.Controls.Add(Me.Panel3)
        Me.pnlEdit.Controls.Add(Me.pnlSnoozeDetails)
        Me.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEdit.Location = New System.Drawing.Point(0, 54)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(488, 142)
        Me.pnlEdit.TabIndex = 18
        '
        'pnlSnoozeDetails
        '
        Me.pnlSnoozeDetails.Controls.Add(Me.cmbSnoozeUnit)
        Me.pnlSnoozeDetails.Controls.Add(Me.cmbSnoozePeriod)
        Me.pnlSnoozeDetails.Controls.Add(Me.Label2)
        Me.pnlSnoozeDetails.Controls.Add(Me.Label3)
        Me.pnlSnoozeDetails.Controls.Add(Me.Label5)
        Me.pnlSnoozeDetails.Controls.Add(Me.lblSnoozePeriod)
        Me.pnlSnoozeDetails.Location = New System.Drawing.Point(0, 28)
        Me.pnlSnoozeDetails.Name = "pnlSnoozeDetails"
        Me.pnlSnoozeDetails.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSnoozeDetails.Size = New System.Drawing.Size(488, 36)
        Me.pnlSnoozeDetails.TabIndex = 22
        Me.pnlSnoozeDetails.Visible = False
        '
        'cmbSnoozeUnit
        '
        Me.cmbSnoozeUnit.FormattingEnabled = True
        Me.cmbSnoozeUnit.Items.AddRange(New Object() {"Year(s)", "Month(s)", "Day(s)"})
        Me.cmbSnoozeUnit.Location = New System.Drawing.Point(198, 8)
        Me.cmbSnoozeUnit.Name = "cmbSnoozeUnit"
        Me.cmbSnoozeUnit.Size = New System.Drawing.Size(121, 21)
        Me.cmbSnoozeUnit.TabIndex = 9
        '
        'cmbSnoozePeriod
        '
        Me.cmbSnoozePeriod.FormattingEnabled = True
        Me.cmbSnoozePeriod.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", ""})
        Me.cmbSnoozePeriod.Location = New System.Drawing.Point(68, 8)
        Me.cmbSnoozePeriod.Name = "cmbSnoozePeriod"
        Me.cmbSnoozePeriod.Size = New System.Drawing.Size(121, 21)
        Me.cmbSnoozePeriod.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 32)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(484, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 32)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(485, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'lblSnoozePeriod
        '
        Me.lblSnoozePeriod.AutoSize = True
        Me.lblSnoozePeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSnoozePeriod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSnoozePeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSnoozePeriod.Location = New System.Drawing.Point(2, 11)
        Me.lblSnoozePeriod.Name = "lblSnoozePeriod"
        Me.lblSnoozePeriod.Size = New System.Drawing.Size(60, 14)
        Me.lblSnoozePeriod.TabIndex = 2
        Me.lblSnoozePeriod.Text = "Snooze :"
        '
        'frmDM_RecommendationNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(488, 196)
        Me.Controls.Add(Me.pnlEdit)
        Me.Controls.Add(Me.pnl_tls_)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDM_RecommendationNotes"
        Me.Text = "Notes"
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsDetails.ResumeLayout(False)
        Me.tlsDetails.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.pnlReason.ResumeLayout(False)
        Me.pnlReason.PerformLayout()
        Me.pnlEdit.ResumeLayout(False)
        Me.pnlSnoozeDetails.ResumeLayout(False)
        Me.pnlSnoozeDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsDetails As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblOrder As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlReason As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlEdit As System.Windows.Forms.Panel
    Friend WithEvents lblSatisFiedDate As System.Windows.Forms.Label
    Friend WithEvents dtpSatisFiedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlSnoozeDetails As System.Windows.Forms.Panel
    Friend WithEvents cmbSnoozeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSnoozePeriod As System.Windows.Forms.ComboBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSnoozePeriod As System.Windows.Forms.Label
End Class
