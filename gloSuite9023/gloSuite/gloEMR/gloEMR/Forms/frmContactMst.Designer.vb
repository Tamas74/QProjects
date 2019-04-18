<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContactMst
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            components.Dispose()
        End If
        If (disposing) Then
            If (IsNothing(ContactDBLayer) = False) Then
                ContactDBLayer.Dispose()
                ContactDBLayer = Nothing
            End If
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmContactMst))
        Me.TabControl1 = New DevComponents.DotNetBar.TabControl
        Me.TabControlPanel1 = New DevComponents.DotNetBar.TabControlPanel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnNext1 = New DevComponents.DotNetBar.ButtonX
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.txtState = New System.Windows.Forms.TextBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.mskPhone = New System.Windows.Forms.MaskedTextBox
        Me.mskMobile = New System.Windows.Forms.MaskedTextBox
        Me.txtURL = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtPager = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtMobile = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtAddressLine2 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbState = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.txtAddressLine1 = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtcontact = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtname = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtDegree = New System.Windows.Forms.TextBox
        Me.lblDegree = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.rbGender2 = New System.Windows.Forms.RadioButton
        Me.rbGender1 = New System.Windows.Forms.RadioButton
        Me.txtlName = New System.Windows.Forms.TextBox
        Me.txtmName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtfName = New System.Windows.Forms.TextBox
        Me.TabItem1 = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel2 = New DevComponents.DotNetBar.TabControlPanel
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.btnPrev1 = New DevComponents.DotNetBar.ButtonX
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.chkLstHospital = New System.Windows.Forms.CheckedListBox
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.chkLstInsurance = New System.Windows.Forms.CheckedListBox
        Me.cmbSpeciality = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlBottom2 = New DevComponents.DotNetBar.PanelEx
        Me.TabItem2 = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tls_ContactMaster = New System.Windows.Forms.ToolStrip
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        CType(Me.TabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabControlPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControlPanel2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls_ContactMaster.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.BackColor = System.Drawing.Color.Transparent
        Me.TabControl1.CanReorderTabs = True
        Me.TabControl1.Controls.Add(Me.TabControlPanel1)
        Me.TabControl1.Controls.Add(Me.TabControlPanel2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedTabFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TabControl1.SelectedTabIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(941, 575)
        Me.TabControl1.TabIndex = 2
        Me.TabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox
        Me.TabControl1.Tabs.Add(Me.TabItem1)
        Me.TabControl1.Tabs.Add(Me.TabItem2)
        Me.TabControl1.Text = "TabControl1"
        '
        'TabControlPanel1
        '
        Me.TabControlPanel1.Controls.Add(Me.GroupBox1)
        Me.TabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel1.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel1.Name = "TabControlPanel1"
        Me.TabControlPanel1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel1.Size = New System.Drawing.Size(941, 549)
        Me.TabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.TabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.TabControlPanel1.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel1.Style.GradientAngle = 90
        Me.TabControlPanel1.TabIndex = 1
        Me.TabControlPanel1.TabItem = Me.TabItem1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnNext1)
        Me.GroupBox1.Controls.Add(Me.GroupBox7)
        Me.GroupBox1.Controls.Add(Me.txtState)
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.txtMobile)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.txtPhone)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(40, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(868, 509)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        '
        'btnNext1
        '
        Me.btnNext1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNext1.Location = New System.Drawing.Point(777, 473)
        Me.btnNext1.Name = "btnNext1"
        Me.btnNext1.Size = New System.Drawing.Size(75, 25)
        Me.btnNext1.TabIndex = 12
        Me.btnNext1.Text = "&Next"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtNotes)
        Me.GroupBox7.Location = New System.Drawing.Point(9, 372)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(476, 129)
        Me.GroupBox7.TabIndex = 4
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.Color.White
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(9, 17)
        Me.txtNotes.MaxLength = 255
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(457, 103)
        Me.txtNotes.TabIndex = 0
        '
        'txtState
        '
        Me.txtState.ForeColor = System.Drawing.Color.Black
        Me.txtState.Location = New System.Drawing.Point(534, 389)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(49, 22)
        Me.txtState.TabIndex = 2
        Me.txtState.Visible = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.mskPhone)
        Me.GroupBox6.Controls.Add(Me.mskMobile)
        Me.GroupBox6.Controls.Add(Me.txtURL)
        Me.GroupBox6.Controls.Add(Me.Label17)
        Me.GroupBox6.Controls.Add(Me.txtEmail)
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Controls.Add(Me.txtPager)
        Me.GroupBox6.Controls.Add(Me.txtFax)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 129)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(852, 107)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Contact Information"
        '
        'mskPhone
        '
        Me.mskPhone.BackColor = System.Drawing.Color.White
        Me.mskPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskPhone.ForeColor = System.Drawing.Color.Black
        Me.mskPhone.Location = New System.Drawing.Point(52, 31)
        Me.mskPhone.Mask = "(999) 000-0000"
        Me.mskPhone.Name = "mskPhone"
        Me.mskPhone.Size = New System.Drawing.Size(165, 22)
        Me.mskPhone.TabIndex = 15
        Me.mskPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'mskMobile
        '
        Me.mskMobile.BackColor = System.Drawing.Color.White
        Me.mskMobile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMobile.ForeColor = System.Drawing.Color.Black
        Me.mskMobile.Location = New System.Drawing.Point(682, 31)
        Me.mskMobile.Mask = "(999) 000-0000"
        Me.mskMobile.Name = "mskMobile"
        Me.mskMobile.Size = New System.Drawing.Size(158, 22)
        Me.mskMobile.TabIndex = 13
        Me.mskMobile.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtURL
        '
        Me.txtURL.BackColor = System.Drawing.Color.White
        Me.txtURL.ForeColor = System.Drawing.Color.Black
        Me.txtURL.Location = New System.Drawing.Point(464, 67)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(374, 22)
        Me.txtURL.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(425, 71)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(36, 14)
        Me.Label17.TabIndex = 14
        Me.Label17.Text = "URL :"
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.ForeColor = System.Drawing.Color.Black
        Me.txtEmail.Location = New System.Drawing.Point(52, 67)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(360, 22)
        Me.txtEmail.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 71)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 14)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "Email :"
        '
        'txtPager
        '
        Me.txtPager.BackColor = System.Drawing.Color.White
        Me.txtPager.ForeColor = System.Drawing.Color.Black
        Me.txtPager.Location = New System.Drawing.Point(463, 32)
        Me.txtPager.MaxLength = 10
        Me.txtPager.Name = "txtPager"
        Me.txtPager.Size = New System.Drawing.Size(165, 22)
        Me.txtPager.TabIndex = 1
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.Color.White
        Me.txtFax.ForeColor = System.Drawing.Color.Black
        Me.txtFax.Location = New System.Drawing.Point(260, 32)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(152, 22)
        Me.txtFax.TabIndex = 4
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(416, 37)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 14)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "Pager :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(631, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(49, 14)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Mobile :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(223, 37)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 14)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Fax :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(5, 37)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 14)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Phone :"
        '
        'txtMobile
        '
        Me.txtMobile.ForeColor = System.Drawing.Color.Black
        Me.txtMobile.Location = New System.Drawing.Point(708, 389)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(49, 22)
        Me.txtMobile.TabIndex = 5
        Me.txtMobile.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtAddressLine2)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.cmbState)
        Me.GroupBox5.Controls.Add(Me.txtZip)
        Me.GroupBox5.Controls.Add(Me.txtCity)
        Me.GroupBox5.Controls.Add(Me.txtAddressLine1)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 242)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(843, 123)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Address Information"
        '
        'txtAddressLine2
        '
        Me.txtAddressLine2.BackColor = System.Drawing.Color.White
        Me.txtAddressLine2.ForeColor = System.Drawing.Color.Black
        Me.txtAddressLine2.Location = New System.Drawing.Point(115, 57)
        Me.txtAddressLine2.Name = "txtAddressLine2"
        Me.txtAddressLine2.Size = New System.Drawing.Size(289, 22)
        Me.txtAddressLine2.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(19, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 14)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Address Line 2 :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbState
        '
        Me.cmbState.DisplayMember = "Text"
        Me.cmbState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbState.ForeColor = System.Drawing.Color.Black
        Me.cmbState.FormattingEnabled = True
        Me.cmbState.ItemHeight = 15
        Me.cmbState.Location = New System.Drawing.Point(532, 57)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(236, 21)
        Me.cmbState.TabIndex = 19
        '
        'txtZip
        '
        Me.txtZip.BackColor = System.Drawing.Color.White
        Me.txtZip.ForeColor = System.Drawing.Color.Black
        Me.txtZip.Location = New System.Drawing.Point(532, 87)
        Me.txtZip.MaxLength = 10
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(236, 22)
        Me.txtZip.TabIndex = 3
        '
        'txtCity
        '
        Me.txtCity.BackColor = System.Drawing.Color.White
        Me.txtCity.ForeColor = System.Drawing.Color.Black
        Me.txtCity.Location = New System.Drawing.Point(532, 27)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(236, 22)
        Me.txtCity.TabIndex = 1
        '
        'txtAddressLine1
        '
        Me.txtAddressLine1.BackColor = System.Drawing.Color.White
        Me.txtAddressLine1.ForeColor = System.Drawing.Color.Black
        Me.txtAddressLine1.Location = New System.Drawing.Point(115, 27)
        Me.txtAddressLine1.Name = "txtAddressLine1"
        Me.txtAddressLine1.Size = New System.Drawing.Size(289, 22)
        Me.txtAddressLine1.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(491, 92)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 14)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Zip :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(477, 61)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "State :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(487, 31)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 14)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "City :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(19, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 14)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Address Line 1 :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPhone
        '
        Me.txtPhone.ForeColor = System.Drawing.Color.Black
        Me.txtPhone.Location = New System.Drawing.Point(625, 389)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(45, 22)
        Me.txtPhone.TabIndex = 0
        Me.txtPhone.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtcontact)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtname)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(450, 103)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General Information"
        '
        'txtcontact
        '
        Me.txtcontact.BackColor = System.Drawing.Color.White
        Me.txtcontact.ForeColor = System.Drawing.Color.Black
        Me.txtcontact.Location = New System.Drawing.Point(77, 69)
        Me.txtcontact.Name = "txtcontact"
        Me.txtcontact.Size = New System.Drawing.Size(335, 22)
        Me.txtcontact.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(19, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Contact :"
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.White
        Me.txtname.ForeColor = System.Drawing.Color.Black
        Me.txtname.Location = New System.Drawing.Point(77, 26)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(335, 22)
        Me.txtname.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(31, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Name :"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDegree)
        Me.GroupBox3.Controls.Add(Me.lblDegree)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.txtlName)
        Me.GroupBox3.Controls.Add(Me.txtmName)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.txtfName)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(849, 103)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "General Information"
        '
        'txtDegree
        '
        Me.txtDegree.ForeColor = System.Drawing.Color.Black
        Me.txtDegree.Location = New System.Drawing.Point(744, 26)
        Me.txtDegree.Name = "txtDegree"
        Me.txtDegree.Size = New System.Drawing.Size(93, 22)
        Me.txtDegree.TabIndex = 23
        '
        'lblDegree
        '
        Me.lblDegree.AutoSize = True
        Me.lblDegree.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDegree.Location = New System.Drawing.Point(687, 30)
        Me.lblDegree.Name = "lblDegree"
        Me.lblDegree.Size = New System.Drawing.Size(55, 14)
        Me.lblDegree.TabIndex = 24
        Me.lblDegree.Text = "Degree :"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbGender2)
        Me.GroupBox4.Controls.Add(Me.rbGender1)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 54)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(177, 43)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Gender"
        '
        'rbGender2
        '
        Me.rbGender2.Location = New System.Drawing.Point(84, 17)
        Me.rbGender2.Name = "rbGender2"
        Me.rbGender2.Size = New System.Drawing.Size(75, 17)
        Me.rbGender2.TabIndex = 0
        Me.rbGender2.Text = "Female"
        '
        'rbGender1
        '
        Me.rbGender1.Checked = True
        Me.rbGender1.Location = New System.Drawing.Point(9, 17)
        Me.rbGender1.Name = "rbGender1"
        Me.rbGender1.Size = New System.Drawing.Size(65, 17)
        Me.rbGender1.TabIndex = 0
        Me.rbGender1.TabStop = True
        Me.rbGender1.Text = "Male"
        '
        'txtlName
        '
        Me.txtlName.ForeColor = System.Drawing.Color.Black
        Me.txtlName.Location = New System.Drawing.Point(535, 26)
        Me.txtlName.Name = "txtlName"
        Me.txtlName.Size = New System.Drawing.Size(140, 22)
        Me.txtlName.TabIndex = 2
        '
        'txtmName
        '
        Me.txtmName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmName.Location = New System.Drawing.Point(310, 26)
        Me.txtmName.Name = "txtmName"
        Me.txtmName.Size = New System.Drawing.Size(140, 22)
        Me.txtmName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(461, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 14)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Last Name :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(226, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 14)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Middle Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "First Name"
        '
        'txtfName
        '
        Me.txtfName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfName.Location = New System.Drawing.Point(76, 26)
        Me.txtfName.Name = "txtfName"
        Me.txtfName.Size = New System.Drawing.Size(140, 22)
        Me.txtfName.TabIndex = 0
        '
        'TabItem1
        '
        Me.TabItem1.AttachedControl = Me.TabControlPanel1
        Me.TabItem1.Name = "TabItem1"
        Me.TabItem1.Text = "Contact Information"
        '
        'TabControlPanel2
        '
        Me.TabControlPanel2.Controls.Add(Me.GroupBox8)
        Me.TabControlPanel2.Controls.Add(Me.pnlBottom2)
        Me.TabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel2.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel2.Name = "TabControlPanel2"
        Me.TabControlPanel2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel2.Size = New System.Drawing.Size(947, 555)
        Me.TabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.TabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.TabControlPanel2.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel2.Style.GradientAngle = 90
        Me.TabControlPanel2.TabIndex = 2
        Me.TabControlPanel2.TabItem = Me.TabItem2
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox8.Controls.Add(Me.btnPrev1)
        Me.GroupBox8.Controls.Add(Me.GroupBox10)
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        Me.GroupBox8.Controls.Add(Me.cmbSpeciality)
        Me.GroupBox8.Controls.Add(Me.Label2)
        Me.GroupBox8.Location = New System.Drawing.Point(27, 23)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(893, 508)
        Me.GroupBox8.TabIndex = 30
        Me.GroupBox8.TabStop = False
        '
        'btnPrev1
        '
        Me.btnPrev1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrev1.Location = New System.Drawing.Point(777, 473)
        Me.btnPrev1.Name = "btnPrev1"
        Me.btnPrev1.Size = New System.Drawing.Size(75, 25)
        Me.btnPrev1.TabIndex = 34
        Me.btnPrev1.Text = "&Previous"
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.chkLstHospital)
        Me.GroupBox10.Location = New System.Drawing.Point(336, 52)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(299, 388)
        Me.GroupBox10.TabIndex = 32
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Hospital Affiliation"
        '
        'chkLstHospital
        '
        Me.chkLstHospital.BackColor = System.Drawing.Color.White
        Me.chkLstHospital.CheckOnClick = True
        Me.chkLstHospital.ForeColor = System.Drawing.Color.Black
        Me.chkLstHospital.Location = New System.Drawing.Point(9, 26)
        Me.chkLstHospital.Name = "chkLstHospital"
        Me.chkLstHospital.Size = New System.Drawing.Size(279, 327)
        Me.chkLstHospital.TabIndex = 0
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.chkLstInsurance)
        Me.GroupBox9.Location = New System.Drawing.Point(19, 52)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(289, 388)
        Me.GroupBox9.TabIndex = 31
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Insurance Information"
        '
        'chkLstInsurance
        '
        Me.chkLstInsurance.BackColor = System.Drawing.Color.White
        Me.chkLstInsurance.CheckOnClick = True
        Me.chkLstInsurance.ForeColor = System.Drawing.Color.Black
        Me.chkLstInsurance.Location = New System.Drawing.Point(9, 26)
        Me.chkLstInsurance.Name = "chkLstInsurance"
        Me.chkLstInsurance.Size = New System.Drawing.Size(270, 327)
        Me.chkLstInsurance.TabIndex = 0
        '
        'cmbSpeciality
        '
        Me.cmbSpeciality.BackColor = System.Drawing.Color.White
        Me.cmbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpeciality.Location = New System.Drawing.Point(121, 17)
        Me.cmbSpeciality.Name = "cmbSpeciality"
        Me.cmbSpeciality.Size = New System.Drawing.Size(186, 22)
        Me.cmbSpeciality.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(58, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 14)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Speciality :"
        '
        'pnlBottom2
        '
        Me.pnlBottom2.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlBottom2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom2.Location = New System.Drawing.Point(1, 550)
        Me.pnlBottom2.Name = "pnlBottom2"
        Me.pnlBottom2.Size = New System.Drawing.Size(945, 4)
        Me.pnlBottom2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlBottom2.Style.BackColor1.Color = System.Drawing.Color.White
        Me.pnlBottom2.Style.BackColor2.Color = System.Drawing.Color.White
        Me.pnlBottom2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlBottom2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlBottom2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlBottom2.Style.GradientAngle = 90
        Me.pnlBottom2.TabIndex = 0
        '
        'TabItem2
        '
        Me.TabItem2.AttachedControl = Me.TabControlPanel2
        Me.TabItem2.Name = "TabItem2"
        Me.TabItem2.Text = "Other Information"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_ContactMaster)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(947, 53)
        Me.pnl_tlspTOP.TabIndex = 4
        '
        'tls_ContactMaster
        '
        Me.tls_ContactMaster.BackColor = System.Drawing.Color.Transparent
        Me.tls_ContactMaster.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_ContactMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ContactMaster.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ContactMaster.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ContactMaster.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls_ContactMaster.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_ContactMaster.Location = New System.Drawing.Point(0, 0)
        Me.tls_ContactMaster.Name = "tls_ContactMaster"
        Me.tls_ContactMaster.Size = New System.Drawing.Size(947, 53)
        Me.tls_ContactMaster.TabIndex = 0
        Me.tls_ContactMaster.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(36, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&OK"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Cancel"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(947, 581)
        Me.Panel1.TabIndex = 31
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 577)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(939, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 574)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(943, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 574)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(941, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'frmContactMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(947, 634)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmContactMst"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contact Master"
        CType(Me.TabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabControlPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.TabControlPanel2.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls_ContactMaster.ResumeLayout(False)
        Me.tls_ContactMaster.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As DevComponents.DotNetBar.TabControl
    Friend WithEvents TabControlPanel2 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents TabItem2 As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel1 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents TabItem1 As DevComponents.DotNetBar.TabItem
    Friend WithEvents pnlBottom2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNext1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents txtPager As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddressLine1 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbGender2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbGender1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtlName As System.Windows.Forms.TextBox
    Friend WithEvents txtmName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtfName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtcontact As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrev1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLstHospital As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLstInsurance As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmbSpeciality As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskMobile As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskPhone As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbState As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents txtDegree As System.Windows.Forms.TextBox
    Friend WithEvents lblDegree As System.Windows.Forms.Label
    Friend WithEvents txtAddressLine2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls_ContactMaster As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
End Class
