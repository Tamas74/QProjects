<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientActivationReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtpfrom) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpfrom)
                        Catch ex As Exception

                        End Try


                        dtpfrom.Dispose()
                        dtpfrom = Nothing
                    End If
                Catch
                End Try


                Try
                    If (IsNothing(dtpTo) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpTo)
                        Catch ex As Exception

                        End Try


                        dtpTo.Dispose()
                        dtpTo = Nothing
                    End If
                Catch
                End Try

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientActivationReport))
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmbQuickFilter = New System.Windows.Forms.ComboBox()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker()
        Me.cmbPortalAccountStatus = New System.Windows.Forms.ComboBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.cmbDateFilter = New System.Windows.Forms.ComboBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.panel9 = New System.Windows.Forms.Panel()
        Me.btnViewReport = New System.Windows.Forms.Button()
        Me.btnSendEmail = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.label25 = New System.Windows.Forms.Label()
        Me.label16 = New System.Windows.Forms.Label()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label17 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label18 = New System.Windows.Forms.Label()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblRowCount = New System.Windows.Forms.Label()
        Me.btnClearFilter = New System.Windows.Forms.Button()
        Me.pnlFilterClear = New System.Windows.Forms.Panel()
        Me.BtnLast = New System.Windows.Forms.Button()
        Me.label15 = New System.Windows.Forms.Label()
        Me.cmbPageSize = New System.Windows.Forms.ComboBox()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.Btn_Next = New System.Windows.Forms.Button()
        Me.BtnFirst = New System.Windows.Forms.Button()
        Me.label26 = New System.Windows.Forms.Label()
        Me.label27 = New System.Windows.Forms.Label()
        Me.gvData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.tlpViewReport = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlpClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.panel5.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.panel9.SuspendLayout()
        Me.panel6.SuspendLayout()
        Me.panel4.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpViewReport.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel5
        '
        Me.panel5.Controls.Add(Me.Label28)
        Me.panel5.Controls.Add(Me.cmbQuickFilter)
        Me.panel5.Controls.Add(Me.pnlSearch)
        Me.panel5.Controls.Add(Me.label7)
        Me.panel5.Controls.Add(Me.label4)
        Me.panel5.Controls.Add(Me.dtpfrom)
        Me.panel5.Controls.Add(Me.cmbPortalAccountStatus)
        Me.panel5.Controls.Add(Me.label5)
        Me.panel5.Controls.Add(Me.cmbDateFilter)
        Me.panel5.Controls.Add(Me.dtpTo)
        Me.panel5.Controls.Add(Me.label6)
        Me.panel5.Controls.Add(Me.label1)
        Me.panel5.Controls.Add(Me.cmbProvider)
        Me.panel5.Controls.Add(Me.label3)
        Me.panel5.Controls.Add(Me.panel9)
        Me.panel5.Controls.Add(Me.label16)
        Me.panel5.Controls.Add(Me.panel6)
        Me.panel5.Controls.Add(Me.label12)
        Me.panel5.Controls.Add(Me.label14)
        Me.panel5.Controls.Add(Me.label18)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel5.Location = New System.Drawing.Point(0, 54)
        Me.panel5.Name = "panel5"
        Me.panel5.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.panel5.Size = New System.Drawing.Size(1120, 171)
        Me.panel5.TabIndex = 37
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(46, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(75, 14)
        Me.Label28.TabIndex = 92
        Me.Label28.Text = "Quick Filter :"
        '
        'cmbQuickFilter
        '
        Me.cmbQuickFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQuickFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQuickFilter.FormattingEnabled = True
        Me.cmbQuickFilter.Items.AddRange(New Object() {"", "Patients needing first invitation", "Patients needing re-invitation"})
        Me.cmbQuickFilter.Location = New System.Drawing.Point(129, 44)
        Me.cmbQuickFilter.Name = "cmbQuickFilter"
        Me.cmbQuickFilter.Size = New System.Drawing.Size(225, 23)
        Me.cmbQuickFilter.TabIndex = 91
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.btnClearSearch)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(132, 127)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(221, 23)
        Me.pnlSearch.TabIndex = 90
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(194, 15)
        Me.txtSearch.TabIndex = 1
        '
        'btnClearSearch
        '
        Me.btnClearSearch.BackColor = System.Drawing.Color.White
        Me.btnClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearch.FlatAppearance.BorderSize = 0
        Me.btnClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearch.Image = CType(resources.GetObject("btnClearSearch.Image"), System.Drawing.Image)
        Me.btnClearSearch.Location = New System.Drawing.Point(199, 4)
        Me.btnClearSearch.Name = "btnClearSearch"
        Me.btnClearSearch.Size = New System.Drawing.Size(21, 13)
        Me.btnClearSearch.TabIndex = 89
        Me.btnClearSearch.UseVisualStyleBackColor = False
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(215, 5)
        Me.Label77.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(215, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 21)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.Gray
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.Gray
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(220, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Gray
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 22)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(221, 1)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Gray
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(221, 1)
        Me.Label24.TabIndex = 45
        Me.Label24.Text = "3"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(74, 132)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(52, 14)
        Me.label7.TabIndex = 88
        Me.label7.Text = "Search :"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(514, 146)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(34, 14)
        Me.label4.TabIndex = 83
        Me.label4.Text = "From"
        '
        'dtpfrom
        '
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(562, 143)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.ShowCheckBox = True
        Me.dtpfrom.Size = New System.Drawing.Size(104, 22)
        Me.dtpfrom.TabIndex = 3
        Me.dtpfrom.Value = New Date(2014, 4, 25, 15, 3, 16, 0)
        '
        'cmbPortalAccountStatus
        '
        Me.cmbPortalAccountStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPortalAccountStatus.FormattingEnabled = True
        Me.cmbPortalAccountStatus.Items.AddRange(New Object() {"", "ACTIVATED", "NOT INVITED", "INVITED", "BLOCKED"})
        Me.cmbPortalAccountStatus.Location = New System.Drawing.Point(562, 41)
        Me.cmbPortalAccountStatus.Name = "cmbPortalAccountStatus"
        Me.cmbPortalAccountStatus.Size = New System.Drawing.Size(243, 22)
        Me.cmbPortalAccountStatus.TabIndex = 0
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(672, 147)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(22, 14)
        Me.label5.TabIndex = 84
        Me.label5.Text = "To"
        '
        'cmbDateFilter
        '
        Me.cmbDateFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDateFilter.FormattingEnabled = True
        Me.cmbDateFilter.Items.AddRange(New Object() {"VISIT", "INVITATION", "ACTIVATION"})
        Me.cmbDateFilter.Location = New System.Drawing.Point(562, 111)
        Me.cmbDateFilter.Name = "cmbDateFilter"
        Me.cmbDateFilter.Size = New System.Drawing.Size(243, 22)
        Me.cmbDateFilter.TabIndex = 2
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(701, 143)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(104, 22)
        Me.dtpTo.TabIndex = 4
        Me.dtpTo.Value = New Date(2014, 4, 25, 15, 3, 16, 0)
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(458, 45)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(100, 14)
        Me.label6.TabIndex = 85
        Me.label6.Text = "Account Status :"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(499, 79)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(59, 14)
        Me.label1.TabIndex = 77
        Me.label1.Text = "Provider :"
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(562, 75)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(243, 22)
        Me.cmbProvider.TabIndex = 1
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(517, 113)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(41, 14)
        Me.label3.TabIndex = 78
        Me.label3.Text = "Date :"
        '
        'panel9
        '
        Me.panel9.Controls.Add(Me.btnViewReport)
        Me.panel9.Controls.Add(Me.btnSendEmail)
        Me.panel9.Controls.Add(Me.btnReset)
        Me.panel9.Controls.Add(Me.label25)
        Me.panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel9.Location = New System.Drawing.Point(916, 29)
        Me.panel9.Name = "panel9"
        Me.panel9.Size = New System.Drawing.Size(200, 141)
        Me.panel9.TabIndex = 76
        '
        'btnViewReport
        '
        Me.btnViewReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewReport.Location = New System.Drawing.Point(27, 12)
        Me.btnViewReport.Name = "btnViewReport"
        Me.btnViewReport.Size = New System.Drawing.Size(146, 29)
        Me.btnViewReport.TabIndex = 0
        Me.btnViewReport.Text = "View Report"
        Me.btnViewReport.UseVisualStyleBackColor = True
        '
        'btnSendEmail
        '
        Me.btnSendEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendEmail.Location = New System.Drawing.Point(27, 54)
        Me.btnSendEmail.Name = "btnSendEmail"
        Me.btnSendEmail.Size = New System.Drawing.Size(146, 29)
        Me.btnSendEmail.TabIndex = 1
        Me.btnSendEmail.Text = "Send Invitation Email"
        Me.btnSendEmail.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(27, 96)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(146, 29)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset Report"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.Location = New System.Drawing.Point(0, 0)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(1, 141)
        Me.label25.TabIndex = 39
        Me.label25.Text = "3"
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.Location = New System.Drawing.Point(4, 170)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(1112, 1)
        Me.label16.TabIndex = 39
        Me.label16.Text = "3"
        '
        'panel6
        '
        Me.panel6.BackgroundImage = CType(resources.GetObject("panel6.BackgroundImage"), System.Drawing.Image)
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Controls.Add(Me.label21)
        Me.panel6.Controls.Add(Me.label17)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel6.Location = New System.Drawing.Point(4, 4)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(1112, 25)
        Me.panel6.TabIndex = 36
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.Location = New System.Drawing.Point(0, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(1112, 24)
        Me.label21.TabIndex = 0
        Me.label21.Text = "  Filter Criteria"
        Me.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label17
        '
        Me.label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.Location = New System.Drawing.Point(0, 24)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(1112, 1)
        Me.label17.TabIndex = 1
        Me.label17.Text = "3"
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(3, 4)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(1, 167)
        Me.label12.TabIndex = 37
        Me.label12.Text = "3"
        '
        'label14
        '
        Me.label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.Location = New System.Drawing.Point(1116, 4)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(1, 167)
        Me.label14.TabIndex = 38
        Me.label14.Text = "3"
        '
        'label18
        '
        Me.label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.Location = New System.Drawing.Point(3, 3)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(1114, 1)
        Me.label18.TabIndex = 40
        Me.label18.Text = "3"
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.Transparent
        Me.panel4.Controls.Add(Me.Label22)
        Me.panel4.Controls.Add(Me.Label20)
        Me.panel4.Controls.Add(Me.Label19)
        Me.panel4.Controls.Add(Me.Label13)
        Me.panel4.Controls.Add(Me.label8)
        Me.panel4.Controls.Add(Me.lblRowCount)
        Me.panel4.Controls.Add(Me.btnClearFilter)
        Me.panel4.Controls.Add(Me.pnlFilterClear)
        Me.panel4.Controls.Add(Me.BtnLast)
        Me.panel4.Controls.Add(Me.label15)
        Me.panel4.Controls.Add(Me.cmbPageSize)
        Me.panel4.Controls.Add(Me.lblSelected)
        Me.panel4.Controls.Add(Me.BtnPrev)
        Me.panel4.Controls.Add(Me.Btn_Next)
        Me.panel4.Controls.Add(Me.BtnFirst)
        Me.panel4.Controls.Add(Me.label26)
        Me.panel4.Controls.Add(Me.label27)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel4.Location = New System.Drawing.Point(0, 225)
        Me.panel4.Name = "panel4"
        Me.panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.panel4.Size = New System.Drawing.Size(1120, 38)
        Me.panel4.TabIndex = 75
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1112, 1)
        Me.Label22.TabIndex = 88
        Me.Label22.Text = "3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 33)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1112, 1)
        Me.Label20.TabIndex = 87
        Me.Label20.Text = "3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1116, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 32)
        Me.Label19.TabIndex = 86
        Me.Label19.Text = "3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 32)
        Me.Label13.TabIndex = 85
        Me.Label13.Text = "3"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(428, 11)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(85, 14)
        Me.label8.TabIndex = 84
        Me.label8.Text = "Total Record :"
        '
        'lblRowCount
        '
        Me.lblRowCount.AutoSize = True
        Me.lblRowCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRowCount.Location = New System.Drawing.Point(516, 12)
        Me.lblRowCount.Name = "lblRowCount"
        Me.lblRowCount.Size = New System.Drawing.Size(0, 13)
        Me.lblRowCount.TabIndex = 83
        '
        'btnClearFilter
        '
        Me.btnClearFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearFilter.Location = New System.Drawing.Point(977, 7)
        Me.btnClearFilter.Name = "btnClearFilter"
        Me.btnClearFilter.Size = New System.Drawing.Size(111, 23)
        Me.btnClearFilter.TabIndex = 1
        Me.btnClearFilter.Text = "Clear Grid Filter"
        Me.btnClearFilter.UseVisualStyleBackColor = True
        '
        'pnlFilterClear
        '
        Me.pnlFilterClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFilterClear.Location = New System.Drawing.Point(970, 9)
        Me.pnlFilterClear.Name = "pnlFilterClear"
        Me.pnlFilterClear.Size = New System.Drawing.Size(7, 19)
        Me.pnlFilterClear.TabIndex = 82
        '
        'BtnLast
        '
        Me.BtnLast.BackColor = System.Drawing.Color.Transparent
        Me.BtnLast.BackgroundImage = CType(resources.GetObject("BtnLast.BackgroundImage"), System.Drawing.Image)
        Me.BtnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnLast.FlatAppearance.BorderSize = 0
        Me.BtnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnLast.Location = New System.Drawing.Point(402, 7)
        Me.BtnLast.Name = "BtnLast"
        Me.BtnLast.Size = New System.Drawing.Size(24, 23)
        Me.BtnLast.TabIndex = 80
        Me.BtnLast.UseVisualStyleBackColor = False
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.Color.Transparent
        Me.label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.Location = New System.Drawing.Point(15, 8)
        Me.label15.Name = "label15"
        Me.label15.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label15.Size = New System.Drawing.Size(111, 17)
        Me.label15.TabIndex = 74
        Me.label15.Text = "Records per Page :"
        '
        'cmbPageSize
        '
        Me.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageSize.FormattingEnabled = True
        Me.cmbPageSize.Location = New System.Drawing.Point(129, 7)
        Me.cmbPageSize.Name = "cmbPageSize"
        Me.cmbPageSize.Size = New System.Drawing.Size(82, 22)
        Me.cmbPageSize.TabIndex = 0
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = True
        Me.lblSelected.BackColor = System.Drawing.Color.Transparent
        Me.lblSelected.Font = New System.Drawing.Font("Tahoma", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblSelected.Location = New System.Drawing.Point(273, 7)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblSelected.Size = New System.Drawing.Size(81, 19)
        Me.lblSelected.TabIndex = 79
        Me.lblSelected.Text = "Page 1 of 1"
        Me.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnPrev
        '
        Me.BtnPrev.BackColor = System.Drawing.Color.Transparent
        Me.BtnPrev.BackgroundImage = CType(resources.GetObject("BtnPrev.BackgroundImage"), System.Drawing.Image)
        Me.BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnPrev.FlatAppearance.BorderSize = 0
        Me.BtnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrev.Location = New System.Drawing.Point(246, 7)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(24, 23)
        Me.BtnPrev.TabIndex = 77
        Me.BtnPrev.UseVisualStyleBackColor = False
        '
        'Btn_Next
        '
        Me.Btn_Next.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Next.BackgroundImage = CType(resources.GetObject("Btn_Next.BackgroundImage"), System.Drawing.Image)
        Me.Btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Btn_Next.FlatAppearance.BorderSize = 0
        Me.Btn_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Btn_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Next.Location = New System.Drawing.Point(378, 7)
        Me.Btn_Next.Name = "Btn_Next"
        Me.Btn_Next.Size = New System.Drawing.Size(24, 23)
        Me.Btn_Next.TabIndex = 78
        Me.Btn_Next.UseVisualStyleBackColor = False
        '
        'BtnFirst
        '
        Me.BtnFirst.BackColor = System.Drawing.Color.Transparent
        Me.BtnFirst.BackgroundImage = CType(resources.GetObject("BtnFirst.BackgroundImage"), System.Drawing.Image)
        Me.BtnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnFirst.FlatAppearance.BorderSize = 0
        Me.BtnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFirst.Location = New System.Drawing.Point(222, 7)
        Me.BtnFirst.Name = "BtnFirst"
        Me.BtnFirst.Size = New System.Drawing.Size(24, 23)
        Me.BtnFirst.TabIndex = 76
        Me.BtnFirst.UseVisualStyleBackColor = False
        '
        'label26
        '
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.Location = New System.Drawing.Point(3, 0)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(1114, 2)
        Me.label26.TabIndex = 72
        Me.label26.Text = "3"
        '
        'label27
        '
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.Location = New System.Drawing.Point(3, 34)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(1114, 1)
        Me.label27.TabIndex = 73
        Me.label27.Text = "3"
        '
        'gvData
        '
        Me.gvData.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gvData.AllowFiltering = True
        Me.gvData.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop
        Me.gvData.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvData.ColumnInfo = resources.GetString("gvData.ColumnInfo")
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
        Me.gvData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvData.Location = New System.Drawing.Point(4, 1)
        Me.gvData.Name = "gvData"
        Me.gvData.Rows.DefaultSize = 21
        Me.gvData.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible
        Me.gvData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvData.Size = New System.Drawing.Size(1112, 615)
        Me.gvData.StyleInfo = resources.GetString("gvData.StyleInfo")
        Me.gvData.TabIndex = 76
        '
        'tlpViewReport
        '
        Me.tlpViewReport.BackgroundImage = CType(resources.GetObject("tlpViewReport.BackgroundImage"), System.Drawing.Image)
        Me.tlpViewReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlpViewReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlpViewReport.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlpViewReport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlpClose})
        Me.tlpViewReport.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlpViewReport.Location = New System.Drawing.Point(0, 0)
        Me.tlpViewReport.Name = "tlpViewReport"
        Me.tlpViewReport.Size = New System.Drawing.Size(1120, 53)
        Me.tlpViewReport.TabIndex = 77
        Me.tlpViewReport.Text = "ToolStrip1"
        '
        'tlpClose
        '
        Me.tlpClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlpClose.Image = CType(resources.GetObject("tlpClose.Image"), System.Drawing.Image)
        Me.tlpClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlpClose.Name = "tlpClose"
        Me.tlpClose.Size = New System.Drawing.Size(51, 50)
        Me.tlpClose.Text = "&Close  "
        Me.tlpClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlpViewReport)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1120, 54)
        Me.Panel1.TabIndex = 90
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.chkSelectAll)
        Me.Panel2.Controls.Add(Me.gvData)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 263)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(1120, 620)
        Me.Panel2.TabIndex = 90
        '
        'chkSelectAll
        '
        Me.chkSelectAll.BackColor = System.Drawing.Color.Transparent
        Me.chkSelectAll.Location = New System.Drawing.Point(36, 8)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(12, 11)
        Me.chkSelectAll.TabIndex = 91
        Me.chkSelectAll.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1116, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 615)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 615)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1114, 1)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 616)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1114, 1)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "3"
        '
        'frmPatientActivationReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1120, 883)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.panel4)
        Me.Controls.Add(Me.panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientActivationReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Activation Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panel5.ResumeLayout(False)
        Me.panel5.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.panel9.ResumeLayout(False)
        Me.panel6.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpViewReport.ResumeLayout(False)
        Me.tlpViewReport.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents panel9 As System.Windows.Forms.Panel
    Friend WithEvents label25 As System.Windows.Forms.Label
    Friend WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Private WithEvents txtSearch As System.Windows.Forms.TextBox
    Private WithEvents cmbPortalAccountStatus As System.Windows.Forms.ComboBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents cmbDateFilter As System.Windows.Forms.ComboBox
    Private WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents btnViewReport As System.Windows.Forms.Button
    Private WithEvents btnSendEmail As System.Windows.Forms.Button
    Private WithEvents btnReset As System.Windows.Forms.Button
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Private WithEvents BtnLast As System.Windows.Forms.Button
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents cmbPageSize As System.Windows.Forms.ComboBox
    Private WithEvents lblSelected As System.Windows.Forms.Label
    Private WithEvents BtnPrev As System.Windows.Forms.Button
    Private WithEvents Btn_Next As System.Windows.Forms.Button
    Private WithEvents BtnFirst As System.Windows.Forms.Button
    Friend WithEvents label26 As System.Windows.Forms.Label
    Friend WithEvents label27 As System.Windows.Forms.Label
    Private WithEvents gvData As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents btnClearFilter As System.Windows.Forms.Button
    Private WithEvents pnlFilterClear As System.Windows.Forms.Panel
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents lblRowCount As System.Windows.Forms.Label
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Friend WithEvents tlpViewReport As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlpClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents cmbQuickFilter As System.Windows.Forms.ComboBox
End Class
