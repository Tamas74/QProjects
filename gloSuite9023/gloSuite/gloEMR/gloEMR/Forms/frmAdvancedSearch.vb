Public Class frmAdvancedSearch
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

                Try
                    Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpDOB}
                    Dim cntControls() As System.Windows.Forms.Control = {dtpDOB}
                    components.Dispose()

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

                Catch
                End Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try


            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDOB As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlTOP As System.Windows.Forms.Panel
    Friend WithEvents chkAdvSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chkGardianInfo As System.Windows.Forms.CheckBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents txtSSN As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlGardian As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFFName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFLName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMFName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMLName As System.Windows.Forms.TextBox
    Friend WithEvents pnlPhone As System.Windows.Forms.Panel
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Search As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPhone As gloMaskControl.gloMaskBox
    Friend WithEvents txtMobile As gloMaskControl.gloMaskBox
    Friend WithEvents txtFCellNo As gloMaskControl.gloMaskBox
    Friend WithEvents txtMCellNo As gloMaskControl.gloMaskBox
    Friend WithEvents txtMPhone As gloMaskControl.gloMaskBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtEmpPhone As gloMaskControl.gloMaskBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents txtFPhone As gloMaskControl.gloMaskBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdvancedSearch))
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.txtSSN = New System.Windows.Forms.TextBox
        Me.chkAdvSearch = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpDOB = New System.Windows.Forms.DateTimePicker
        Me.chkGardianInfo = New System.Windows.Forms.CheckBox
        Me.pnlTOP = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblHeader = New System.Windows.Forms.Label
        Me.pnlGardian = New System.Windows.Forms.Panel
        Me.txtFPhone = New gloMaskControl.gloMaskBox
        Me.txtFCellNo = New gloMaskControl.gloMaskBox
        Me.txtMCellNo = New gloMaskControl.gloMaskBox
        Me.txtMPhone = New gloMaskControl.gloMaskBox
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtFFName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFLName = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtMFName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtMLName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMobile = New gloMaskControl.gloMaskBox
        Me.pnlPhone = New System.Windows.Forms.Panel
        Me.txtEmpPhone = New gloMaskControl.gloMaskBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtPhone = New gloMaskControl.gloMaskBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnl_tls_ = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Search = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton
        Me.Label17 = New System.Windows.Forms.Label
        Me.pnlTOP.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlGardian.SuspendLayout()
        Me.pnlPhone.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.Color.GhostWhite
        Me.txtCode.Location = New System.Drawing.Point(395, 9)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(8, 21)
        Me.txtCode.TabIndex = 14
        Me.txtCode.Visible = False
        '
        'txtLName
        '
        Me.txtLName.BackColor = System.Drawing.Color.GhostWhite
        Me.txtLName.Location = New System.Drawing.Point(395, 38)
        Me.txtLName.Name = "txtLName"
        Me.txtLName.ReadOnly = True
        Me.txtLName.Size = New System.Drawing.Size(8, 21)
        Me.txtLName.TabIndex = 13
        Me.txtLName.Visible = False
        '
        'txtFName
        '
        Me.txtFName.BackColor = System.Drawing.Color.GhostWhite
        Me.txtFName.Location = New System.Drawing.Point(395, 70)
        Me.txtFName.Name = "txtFName"
        Me.txtFName.ReadOnly = True
        Me.txtFName.Size = New System.Drawing.Size(8, 21)
        Me.txtFName.TabIndex = 12
        Me.txtFName.Visible = False
        '
        'txtSSN
        '
        Me.txtSSN.BackColor = System.Drawing.Color.GhostWhite
        Me.txtSSN.Location = New System.Drawing.Point(395, 98)
        Me.txtSSN.Name = "txtSSN"
        Me.txtSSN.ReadOnly = True
        Me.txtSSN.Size = New System.Drawing.Size(8, 21)
        Me.txtSSN.TabIndex = 11
        Me.txtSSN.Visible = False
        '
        'chkAdvSearch
        '
        Me.chkAdvSearch.AutoSize = True
        Me.chkAdvSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkAdvSearch.Checked = True
        Me.chkAdvSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAdvSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAdvSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkAdvSearch.Location = New System.Drawing.Point(192, 10)
        Me.chkAdvSearch.Name = "chkAdvSearch"
        Me.chkAdvSearch.Size = New System.Drawing.Size(15, 14)
        Me.chkAdvSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(104, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Date of Birth :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(139, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Phone :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDOB
        '
        Me.dtpDOB.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpDOB.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpDOB.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpDOB.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpDOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDOB.Location = New System.Drawing.Point(192, 108)
        Me.dtpDOB.Name = "dtpDOB"
        Me.dtpDOB.ShowCheckBox = True
        Me.dtpDOB.Size = New System.Drawing.Size(139, 22)
        Me.dtpDOB.TabIndex = 2
        Me.dtpDOB.Value = New Date(2007, 1, 27, 0, 0, 0, 0)
        '
        'chkGardianInfo
        '
        Me.chkGardianInfo.AutoSize = True
        Me.chkGardianInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkGardianInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGardianInfo.Location = New System.Drawing.Point(8, 136)
        Me.chkGardianInfo.Name = "chkGardianInfo"
        Me.chkGardianInfo.Size = New System.Drawing.Size(200, 18)
        Me.chkGardianInfo.TabIndex = 3
        Me.chkGardianInfo.Text = "Search in Guardian information :"
        '
        'pnlTOP
        '
        Me.pnlTOP.BackColor = System.Drawing.Color.Transparent
        Me.pnlTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTOP.Controls.Add(Me.Panel1)
        Me.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTOP.Location = New System.Drawing.Point(0, 54)
        Me.pnlTOP.Name = "pnlTOP"
        Me.pnlTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTOP.Size = New System.Drawing.Size(362, 32)
        Me.pnlTOP.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lblHeader)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(356, 26)
        Me.Panel1.TabIndex = 19
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(356, 26)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = " Advanced Search on Patient"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlGardian
        '
        Me.pnlGardian.BackColor = System.Drawing.Color.Transparent
        Me.pnlGardian.Controls.Add(Me.txtFPhone)
        Me.pnlGardian.Controls.Add(Me.txtFCellNo)
        Me.pnlGardian.Controls.Add(Me.txtMCellNo)
        Me.pnlGardian.Controls.Add(Me.txtMPhone)
        Me.pnlGardian.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlGardian.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlGardian.Controls.Add(Me.lbl_pnlRight)
        Me.pnlGardian.Controls.Add(Me.lbl_pnlTop)
        Me.pnlGardian.Controls.Add(Me.Label11)
        Me.pnlGardian.Controls.Add(Me.Label10)
        Me.pnlGardian.Controls.Add(Me.Label7)
        Me.pnlGardian.Controls.Add(Me.txtFFName)
        Me.pnlGardian.Controls.Add(Me.Label8)
        Me.pnlGardian.Controls.Add(Me.txtFLName)
        Me.pnlGardian.Controls.Add(Me.Label9)
        Me.pnlGardian.Controls.Add(Me.Label6)
        Me.pnlGardian.Controls.Add(Me.txtMFName)
        Me.pnlGardian.Controls.Add(Me.Label5)
        Me.pnlGardian.Controls.Add(Me.txtMLName)
        Me.pnlGardian.Controls.Add(Me.Label4)
        Me.pnlGardian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGardian.Location = New System.Drawing.Point(0, 251)
        Me.pnlGardian.Name = "pnlGardian"
        Me.pnlGardian.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGardian.Size = New System.Drawing.Size(362, 222)
        Me.pnlGardian.TabIndex = 28
        '
        'txtFPhone
        '
        Me.txtFPhone.AllowValidate = True
        Me.txtFPhone.IncludeLiteralsAndPrompts = False
        Me.txtFPhone.Location = New System.Drawing.Point(194, 186)
        Me.txtFPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtFPhone.Name = "txtFPhone"
        Me.txtFPhone.ReadOnly = False
        Me.txtFPhone.Size = New System.Drawing.Size(141, 22)
        Me.txtFPhone.TabIndex = 31
        '
        'txtFCellNo
        '
        Me.txtFCellNo.AllowValidate = True
        Me.txtFCellNo.IncludeLiteralsAndPrompts = False
        Me.txtFCellNo.Location = New System.Drawing.Point(194, 162)
        Me.txtFCellNo.MaskType = gloMaskControl.gloMaskType.Mobile
        Me.txtFCellNo.Name = "txtFCellNo"
        Me.txtFCellNo.ReadOnly = False
        Me.txtFCellNo.Size = New System.Drawing.Size(139, 21)
        Me.txtFCellNo.TabIndex = 29
        '
        'txtMCellNo
        '
        Me.txtMCellNo.AllowValidate = True
        Me.txtMCellNo.IncludeLiteralsAndPrompts = False
        Me.txtMCellNo.Location = New System.Drawing.Point(194, 62)
        Me.txtMCellNo.MaskType = gloMaskControl.gloMaskType.Mobile
        Me.txtMCellNo.Name = "txtMCellNo"
        Me.txtMCellNo.ReadOnly = False
        Me.txtMCellNo.Size = New System.Drawing.Size(139, 21)
        Me.txtMCellNo.TabIndex = 28
        '
        'txtMPhone
        '
        Me.txtMPhone.AllowValidate = True
        Me.txtMPhone.IncludeLiteralsAndPrompts = False
        Me.txtMPhone.Location = New System.Drawing.Point(194, 87)
        Me.txtMPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtMPhone.Name = "txtMPhone"
        Me.txtMPhone.ReadOnly = False
        Me.txtMPhone.Size = New System.Drawing.Size(139, 21)
        Me.txtMPhone.TabIndex = 27
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 218)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(354, 1)
        Me.lbl_pnlBottom.TabIndex = 26
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 218)
        Me.lbl_pnlLeft.TabIndex = 25
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(358, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 218)
        Me.lbl_pnlRight.TabIndex = 24
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(356, 1)
        Me.lbl_pnlTop.TabIndex = 23
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(95, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 14)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Father's Phone :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(91, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 14)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Mother's Phone :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(93, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Father's Cell No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFFName
        '
        Me.txtFFName.ForeColor = System.Drawing.Color.Black
        Me.txtFFName.Location = New System.Drawing.Point(194, 137)
        Me.txtFFName.MaxLength = 255
        Me.txtFFName.Name = "txtFFName"
        Me.txtFFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFFName.Size = New System.Drawing.Size(139, 21)
        Me.txtFFName.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(73, 139)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 14)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Father's First Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFLName
        '
        Me.txtFLName.ForeColor = System.Drawing.Color.Black
        Me.txtFLName.Location = New System.Drawing.Point(194, 112)
        Me.txtFLName.MaxLength = 255
        Me.txtFLName.Name = "txtFLName"
        Me.txtFLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFLName.Size = New System.Drawing.Size(139, 21)
        Me.txtFLName.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(73, 114)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(119, 14)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Father's Last Name :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(89, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 14)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Mother's Cell No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMFName
        '
        Me.txtMFName.ForeColor = System.Drawing.Color.Black
        Me.txtMFName.Location = New System.Drawing.Point(194, 37)
        Me.txtMFName.MaxLength = 255
        Me.txtMFName.Name = "txtMFName"
        Me.txtMFName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMFName.Size = New System.Drawing.Size(139, 21)
        Me.txtMFName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(69, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 14)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Mother's First Name :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMLName
        '
        Me.txtMLName.ForeColor = System.Drawing.Color.Black
        Me.txtMLName.Location = New System.Drawing.Point(194, 11)
        Me.txtMLName.MaxLength = 255
        Me.txtMLName.Name = "txtMLName"
        Me.txtMLName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMLName.Size = New System.Drawing.Size(139, 21)
        Me.txtMLName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(69, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 14)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Mother's Last Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMobile
        '
        Me.txtMobile.AllowValidate = True
        Me.txtMobile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.IncludeLiteralsAndPrompts = False
        Me.txtMobile.Location = New System.Drawing.Point(192, 56)
        Me.txtMobile.MaskType = gloMaskControl.gloMaskType.Mobile
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = False
        Me.txtMobile.Size = New System.Drawing.Size(139, 22)
        Me.txtMobile.TabIndex = 30
        '
        'pnlPhone
        '
        Me.pnlPhone.BackColor = System.Drawing.Color.Transparent
        Me.pnlPhone.Controls.Add(Me.txtEmpPhone)
        Me.pnlPhone.Controls.Add(Me.Label16)
        Me.pnlPhone.Controls.Add(Me.txtMobile)
        Me.pnlPhone.Controls.Add(Me.Label15)
        Me.pnlPhone.Controls.Add(Me.txtPhone)
        Me.pnlPhone.Controls.Add(Me.Label3)
        Me.pnlPhone.Controls.Add(Me.Label12)
        Me.pnlPhone.Controls.Add(Me.Label13)
        Me.pnlPhone.Controls.Add(Me.Label14)
        Me.pnlPhone.Controls.Add(Me.txtCode)
        Me.pnlPhone.Controls.Add(Me.chkAdvSearch)
        Me.pnlPhone.Controls.Add(Me.Label1)
        Me.pnlPhone.Controls.Add(Me.txtLName)
        Me.pnlPhone.Controls.Add(Me.Label17)
        Me.pnlPhone.Controls.Add(Me.Label2)
        Me.pnlPhone.Controls.Add(Me.txtFName)
        Me.pnlPhone.Controls.Add(Me.dtpDOB)
        Me.pnlPhone.Controls.Add(Me.txtSSN)
        Me.pnlPhone.Controls.Add(Me.chkGardianInfo)
        Me.pnlPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPhone.Location = New System.Drawing.Point(0, 86)
        Me.pnlPhone.Name = "pnlPhone"
        Me.pnlPhone.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPhone.Size = New System.Drawing.Size(362, 165)
        Me.pnlPhone.TabIndex = 24
        '
        'txtEmpPhone
        '
        Me.txtEmpPhone.AllowValidate = True
        Me.txtEmpPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpPhone.IncludeLiteralsAndPrompts = False
        Me.txtEmpPhone.Location = New System.Drawing.Point(192, 82)
        Me.txtEmpPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtEmpPhone.Name = "txtEmpPhone"
        Me.txtEmpPhone.ReadOnly = False
        Me.txtEmpPhone.Size = New System.Drawing.Size(139, 22)
        Me.txtEmpPhone.TabIndex = 32
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(77, 86)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(112, 14)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Employer's Phone :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(140, 60)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 14)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Mobile :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPhone
        '
        Me.txtPhone.AllowValidate = True
        Me.txtPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.IncludeLiteralsAndPrompts = False
        Me.txtPhone.Location = New System.Drawing.Point(192, 30)
        Me.txtPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.ReadOnly = False
        Me.txtPhone.Size = New System.Drawing.Size(139, 22)
        Me.txtPhone.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(4, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(354, 1)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 161)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(358, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 161)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(356, 1)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "label1"
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tls)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(362, 54)
        Me.pnl_tls_.TabIndex = 30
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Search, Me.btn_tls_Cancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(362, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'btn_tls_Search
        '
        Me.btn_tls_Search.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Search.Image = CType(resources.GetObject("btn_tls_Search.Image"), System.Drawing.Image)
        Me.btn_tls_Search.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Search.Name = "btn_tls_Search"
        Me.btn_tls_Search.Size = New System.Drawing.Size(52, 50)
        Me.btn_tls_Search.Tag = "Search"
        Me.btn_tls_Search.Text = "&Search"
        Me.btn_tls_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(38, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(151, 14)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "Search in Previous Result :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmAdvancedSearch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(362, 473)
        Me.Controls.Add(Me.pnlGardian)
        Me.Controls.Add(Me.pnlPhone)
        Me.Controls.Add(Me.pnlTOP)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdvancedSearch"
        Me.Text = "Advanced Search"
        Me.pnlTOP.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlGardian.ResumeLayout(False)
        Me.pnlGardian.PerformLayout()
        Me.pnlPhone.ResumeLayout(False)
        Me.pnlPhone.PerformLayout()
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _FName As String
    Dim _LName As String
    Dim _Code As String
    Dim _SSN As String
    Dim _Phone As String
    Dim _DOB As Date
    Dim _ISDOB As Boolean = False
    '20090822 commented by Mayuri
    Dim _Mobile As String
    Dim _EmpPhone As String

    Dim _MFName As String ' Mother's first name
    Dim _MLName As String 'Mother's last name
    Dim _MCellNo As String 'Mother's cell no
    Dim _MPhone As String 'Mother's phone

    Dim _FFName As String ' Father's first name
    Dim _FLName As String 'Father's last name
    Dim _FCellNo As String 'Father's cell no
    Dim _FPhone As String 'Father's phone

    Dim _IsGardianinfo As Boolean = False 'flag to check if guardian search is ON 
    


    Public Property FName() As String
        Get
            Return _FName
        End Get
        Set(ByVal Value As String)
            _FName = Value
        End Set
    End Property

    Public Property LName() As String
        Get
            Return _LName
        End Get
        Set(ByVal Value As String)
            _LName = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal Value As String)
            _Code = Value
        End Set
    End Property

    Public Property SSN() As String
        Get
            Return _SSN
        End Get
        Set(ByVal Value As String)
            _SSN = Value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal Value As String)
            _Phone = Value
        End Set
    End Property
    '20090822 commented by Mayuri
    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal Value As String)
            _Mobile = Value
        End Set
    End Property
    Public Property EmpPhone() As String
        Get
            Return _EmpPhone
        End Get
        Set(ByVal Value As String)
            _EmpPhone = Value
        End Set
    End Property


    Public Property DOB() As Date
        Get
            Return _DOB
        End Get
        Set(ByVal Value As Date)
            _DOB = Value
        End Set
    End Property

    Public Property ISDOB() As Boolean
        Get
            Return _ISDOB
        End Get
        Set(ByVal Value As Boolean)
            _ISDOB = Value
        End Set
    End Property

    Public Property MotherLastName() As String
        Get
            Return _MLName
        End Get
        Set(ByVal Value As String)
            _MLName = Value
        End Set
    End Property

    Public Property MotherFirstName() As String
        Get
            Return _MFName
        End Get
        Set(ByVal Value As String)
            _MFName = Value
        End Set
    End Property

    Public Property MotherCellNo() As String
        Get
            Return _MCellNo
        End Get
        Set(ByVal Value As String)
            _MCellNo = Value
        End Set
    End Property

    Public Property MotherPhoneNo() As String
        Get
            Return _MPhone
        End Get
        Set(ByVal Value As String)
            _MPhone = Value
        End Set
    End Property

    Public Property FatherLastName() As String
        Get
            Return _FLName
        End Get
        Set(ByVal Value As String)
            _FLName = Value
        End Set
    End Property

    Public Property FatherFirstName() As String
        Get
            Return _FFName
        End Get
        Set(ByVal Value As String)
            _FFName = Value
        End Set
    End Property

    Public Property FatherCellNo() As String
        Get
            Return _FCellNo
        End Get
        Set(ByVal Value As String)
            _FCellNo = Value
        End Set
    End Property

    Public Property FatherPhoneNo() As String
        Get
            Return _FPhone
        End Get
        Set(ByVal Value As String)
            _FPhone = Value
        End Set
    End Property

    Public Property IsGuardianinfo() As Boolean
        Get
            Return _IsGardianinfo
        End Get
        Set(ByVal Value As Boolean)
            _IsGardianinfo = Value
        End Set
    End Property

    Private Sub frmAdvancedSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtpDOB.Format = DateTimePickerFormat.Custom
            '
            If _ISDOB = False Then
                dtpDOB.Value = Format(Now, "MM/dd/yyyy")
            Else
                dtpDOB.Value = Format(_DOB, "MM/dd/yyyy")
            End If

            dtpDOB.MaxDate = Now

            dtpDOB.Checked = _ISDOB
            txtPhone.Text = _Phone
            txtCode.Text = _Code
            txtLName.Text = _LName
            txtFName.Text = _FName
            txtSSN.Text = _SSN
            ''20090822: Mayuri Added Mobile and Employer's Phone in Advanced Search Of Patient summary
            txtMobile.Text = _Mobile
            txtEmpPhone.Text = _EmpPhone


            If _Code = "" AndAlso _FName = "" AndAlso _LName = "" AndAlso _SSN = "" Then
                chkAdvSearch.Checked = False
                chkAdvSearch.Enabled = False
            Else
                chkAdvSearch.Checked = True
                chkAdvSearch.Enabled = True
            End If
            Call chkGardianInfo_Click(sender, e)
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        End Try
    End Sub


    Private Sub chkGardianInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGardianInfo.Click
        Try
            If chkGardianInfo.CheckState = CheckState.Checked Then
                pnlGardian.Visible = True
                'pnlGardian.Dock = DockStyle.Fill
                'pnlGardian.BringToFront()
                Me.Height = 509             'lblheader.Height + pnlGardian.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height - 10
                _IsGardianinfo = True
            ElseIf chkGardianInfo.CheckState = CheckState.Unchecked Then

                pnlGardian.Visible = False
                Me.Height = 283 'lblHeader.Height + pnlPhone.Height + pnlBottom.Height + pnlBottom.Height
                'Me.Height.Height = pnlMain.Height - pnlGardian.Height
                _IsGardianinfo = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        End Try
    End Sub

    Private Sub tls_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Search"
                PatientSearch()
            Case "Cancel"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub
    Private Sub PatientSearch()
        Try
            If chkAdvSearch.Checked = True Then
                _Code = txtCode.Text.Trim
                _LName = txtLName.Text.Trim
                _FName = txtFName.Text.Trim
                _SSN = txtSSN.Text.Trim
            Else
                _Code = "" ' txtCode.Text.Trim
                _LName = "" 'txtLName.Text.Trim
                _FName = "" ' txtFName.Text.Trim
                _SSN = "" 'txtSSN.Text.Trim
            End If

            _Phone = txtPhone.Text.Trim
            '20090822:Mayuri
            _Mobile = txtMobile.Text.Trim
            _EmpPhone = txtEmpPhone.Text.Trim


            If dtpDOB.Checked = True Then
                _ISDOB = True
                _DOB = dtpDOB.Value
            Else
                _ISDOB = False
            End If

            '' Added On 20070128
            If _IsGardianinfo = True Then
                _MFName = txtMFName.Text.Trim
                _MLName = txtMLName.Text.Trim
                _MCellNo = txtMCellNo.Text.Trim
                _MPhone = txtMPhone.Text.Trim
                _FFName = txtFFName.Text.Trim
                _FLName = txtFLName.Text.Trim
                _FCellNo = txtFCellNo.Text.Trim
                '_FPhone = txtMobile.Text.Trim
                '20090907
                _FPhone = txtFPhone.Text.Trim
                '
            Else
                _MFName = ""
                _MLName = ""
                _MCellNo = ""
                _MPhone = ""
                _FFName = ""
                _FLName = ""
                _FCellNo = ""
                _FPhone = ""
            End If

            '            Me.Close()
            gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption)
        End Try
    End Sub

    Private Sub btn_tls_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Search.Click

    End Sub

    Private Sub chkGardianInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGardianInfo.CheckedChanged

    End Sub
End Class
