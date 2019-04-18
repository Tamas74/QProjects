Public Class frmMSTAppointmentScheduler
    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                If (IsNothing(ColorDialog1) = False) Then
                    ColorDialog1.Dispose()
                    ColorDialog1 = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optDaily As System.Windows.Forms.RadioButton
    Friend WithEvents optWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents optMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents pnlDaily As System.Windows.Forms.Panel
    Friend WithEvents pnlWeekly As System.Windows.Forms.Panel
    Friend WithEvents pnlMonthly As System.Windows.Forms.Panel
    Friend WithEvents lblJobOccursValue As System.Windows.Forms.Label
    Friend WithEvents lblJobOccursHead As System.Windows.Forms.Label
    Friend WithEvents chkSun As System.Windows.Forms.CheckBox
    Friend WithEvents chkSat As System.Windows.Forms.CheckBox
    Friend WithEvents chkFri As System.Windows.Forms.CheckBox
    Friend WithEvents chkThu As System.Windows.Forms.CheckBox
    Friend WithEvents chkWed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTue As System.Windows.Forms.CheckBox
    Friend WithEvents chkMon As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents numDailyFrequency As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents numMonthlyFrequency As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAppointmentType As System.Windows.Forms.TextBox
    Friend WithEvents cmbAppointmentUpToInterval As System.Windows.Forms.ComboBox
    Friend WithEvents picColor As System.Windows.Forms.PictureBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents numAppointmentUpTo As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents numAppointmentDuration As System.Windows.Forms.NumericUpDown
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls_MSTAppointmentScheduler As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTAppointmentScheduler))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.numAppointmentDuration = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.numAppointmentUpTo = New System.Windows.Forms.NumericUpDown
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.picColor = New System.Windows.Forms.PictureBox
        Me.cmbAppointmentUpToInterval = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.pnlWeekly = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkSun = New System.Windows.Forms.CheckBox
        Me.chkSat = New System.Windows.Forms.CheckBox
        Me.chkFri = New System.Windows.Forms.CheckBox
        Me.chkThu = New System.Windows.Forms.CheckBox
        Me.chkWed = New System.Windows.Forms.CheckBox
        Me.chkTue = New System.Windows.Forms.CheckBox
        Me.chkMon = New System.Windows.Forms.CheckBox
        Me.pnlMonthly = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.numMonthlyFrequency = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlDaily = New System.Windows.Forms.Panel
        Me.numDailyFrequency = New System.Windows.Forms.NumericUpDown
        Me.lblJobOccursValue = New System.Windows.Forms.Label
        Me.lblJobOccursHead = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.optMonthly = New System.Windows.Forms.RadioButton
        Me.optWeekly = New System.Windows.Forms.RadioButton
        Me.optDaily = New System.Windows.Forms.RadioButton
        Me.txtAppointmentType = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tls_MSTAppointmentScheduler = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.pnlMain.SuspendLayout()
        CType(Me.numAppointmentDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numAppointmentUpTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.pnlWeekly.SuspendLayout()
        Me.pnlMonthly.SuspendLayout()
        CType(Me.numMonthlyFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDaily.SuspendLayout()
        CType(Me.numDailyFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls_MSTAppointmentScheduler.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlMain.Controls.Add(Me.lbl_pnlTop)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.numAppointmentDuration)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.numAppointmentUpTo)
        Me.pnlMain.Controls.Add(Me.btnBrowse)
        Me.pnlMain.Controls.Add(Me.picColor)
        Me.pnlMain.Controls.Add(Me.cmbAppointmentUpToInterval)
        Me.pnlMain.Controls.Add(Me.GroupBox1)
        Me.pnlMain.Controls.Add(Me.txtAppointmentType)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(450, 291)
        Me.pnlMain.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(241, 229)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "(in minutes)"
        '
        'numAppointmentDuration
        '
        Me.numAppointmentDuration.ForeColor = System.Drawing.Color.Black
        Me.numAppointmentDuration.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numAppointmentDuration.Location = New System.Drawing.Point(174, 225)
        Me.numAppointmentDuration.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.numAppointmentDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAppointmentDuration.Name = "numAppointmentDuration"
        Me.numAppointmentDuration.Size = New System.Drawing.Size(58, 21)
        Me.numAppointmentDuration.TabIndex = 9
        Me.numAppointmentDuration.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(48, 229)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Appointment Duration :"
        '
        'numAppointmentUpTo
        '
        Me.numAppointmentUpTo.Location = New System.Drawing.Point(174, 43)
        Me.numAppointmentUpTo.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numAppointmentUpTo.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAppointmentUpTo.Name = "numAppointmentUpTo"
        Me.numAppointmentUpTo.Size = New System.Drawing.Size(58, 21)
        Me.numAppointmentUpTo.TabIndex = 1
        Me.numAppointmentUpTo.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(208, 255)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(29, 21)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "..."
        '
        'picColor
        '
        Me.picColor.BackColor = System.Drawing.Color.Red
        Me.picColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picColor.Location = New System.Drawing.Point(174, 255)
        Me.picColor.Name = "picColor"
        Me.picColor.Size = New System.Drawing.Size(23, 20)
        Me.picColor.TabIndex = 7
        Me.picColor.TabStop = False
        '
        'cmbAppointmentUpToInterval
        '
        Me.cmbAppointmentUpToInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAppointmentUpToInterval.Location = New System.Drawing.Point(240, 43)
        Me.cmbAppointmentUpToInterval.Name = "cmbAppointmentUpToInterval"
        Me.cmbAppointmentUpToInterval.Size = New System.Drawing.Size(136, 21)
        Me.cmbAppointmentUpToInterval.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.pnlWeekly)
        Me.GroupBox1.Controls.Add(Me.pnlMonthly)
        Me.GroupBox1.Controls.Add(Me.pnlDaily)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(24, 74)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(403, 144)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Appointment Interval"
        '
        'pnlWeekly
        '
        Me.pnlWeekly.Controls.Add(Me.Label11)
        Me.pnlWeekly.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlWeekly.Controls.Add(Me.lbl_pnlRight)
        Me.pnlWeekly.Controls.Add(Me.Label12)
        Me.pnlWeekly.Controls.Add(Me.Label4)
        Me.pnlWeekly.Controls.Add(Me.chkSun)
        Me.pnlWeekly.Controls.Add(Me.chkSat)
        Me.pnlWeekly.Controls.Add(Me.chkFri)
        Me.pnlWeekly.Controls.Add(Me.chkThu)
        Me.pnlWeekly.Controls.Add(Me.chkWed)
        Me.pnlWeekly.Controls.Add(Me.chkTue)
        Me.pnlWeekly.Controls.Add(Me.chkMon)
        Me.pnlWeekly.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWeekly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlWeekly.Location = New System.Drawing.Point(3, 50)
        Me.pnlWeekly.Name = "pnlWeekly"
        Me.pnlWeekly.Size = New System.Drawing.Size(412, 91)
        Me.pnlWeekly.TabIndex = 2
        Me.pnlWeekly.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "After Every :"
        '
        'chkSun
        '
        Me.chkSun.AutoSize = True
        Me.chkSun.Location = New System.Drawing.Point(139, 62)
        Me.chkSun.Name = "chkSun"
        Me.chkSun.Size = New System.Drawing.Size(44, 17)
        Me.chkSun.TabIndex = 16
        Me.chkSun.Text = "Sun"
        '
        'chkSat
        '
        Me.chkSat.AutoSize = True
        Me.chkSat.Location = New System.Drawing.Point(77, 62)
        Me.chkSat.Name = "chkSat"
        Me.chkSat.Size = New System.Drawing.Size(42, 17)
        Me.chkSat.TabIndex = 15
        Me.chkSat.Text = "Sat"
        '
        'chkFri
        '
        Me.chkFri.AutoSize = True
        Me.chkFri.Location = New System.Drawing.Point(13, 62)
        Me.chkFri.Name = "chkFri"
        Me.chkFri.Size = New System.Drawing.Size(38, 17)
        Me.chkFri.TabIndex = 14
        Me.chkFri.Text = "Fri"
        '
        'chkThu
        '
        Me.chkThu.AutoSize = True
        Me.chkThu.Location = New System.Drawing.Point(205, 34)
        Me.chkThu.Name = "chkThu"
        Me.chkThu.Size = New System.Drawing.Size(44, 17)
        Me.chkThu.TabIndex = 13
        Me.chkThu.Text = "Thu"
        '
        'chkWed
        '
        Me.chkWed.AutoSize = True
        Me.chkWed.Location = New System.Drawing.Point(139, 34)
        Me.chkWed.Name = "chkWed"
        Me.chkWed.Size = New System.Drawing.Size(48, 17)
        Me.chkWed.TabIndex = 12
        Me.chkWed.Text = "Wed"
        '
        'chkTue
        '
        Me.chkTue.AutoSize = True
        Me.chkTue.Location = New System.Drawing.Point(77, 34)
        Me.chkTue.Name = "chkTue"
        Me.chkTue.Size = New System.Drawing.Size(44, 17)
        Me.chkTue.TabIndex = 11
        Me.chkTue.Text = "Tue"
        '
        'chkMon
        '
        Me.chkMon.AutoSize = True
        Me.chkMon.Location = New System.Drawing.Point(13, 34)
        Me.chkMon.Name = "chkMon"
        Me.chkMon.Size = New System.Drawing.Size(46, 17)
        Me.chkMon.TabIndex = 10
        Me.chkMon.Text = "Mon"
        '
        'pnlMonthly
        '
        Me.pnlMonthly.Controls.Add(Me.Label13)
        Me.pnlMonthly.Controls.Add(Me.Label14)
        Me.pnlMonthly.Controls.Add(Me.Label15)
        Me.pnlMonthly.Controls.Add(Me.Label16)
        Me.pnlMonthly.Controls.Add(Me.Label5)
        Me.pnlMonthly.Controls.Add(Me.numMonthlyFrequency)
        Me.pnlMonthly.Controls.Add(Me.Label6)
        Me.pnlMonthly.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMonthly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMonthly.Location = New System.Drawing.Point(3, 50)
        Me.pnlMonthly.Name = "pnlMonthly"
        Me.pnlMonthly.Size = New System.Drawing.Size(412, 91)
        Me.pnlMonthly.TabIndex = 3
        Me.pnlMonthly.Visible = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(157, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "month(s)"
        '
        'numMonthlyFrequency
        '
        Me.numMonthlyFrequency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numMonthlyFrequency.Location = New System.Drawing.Point(97, 7)
        Me.numMonthlyFrequency.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numMonthlyFrequency.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMonthlyFrequency.Name = "numMonthlyFrequency"
        Me.numMonthlyFrequency.Size = New System.Drawing.Size(58, 21)
        Me.numMonthlyFrequency.TabIndex = 7
        Me.numMonthlyFrequency.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "After Every :"
        '
        'pnlDaily
        '
        Me.pnlDaily.Controls.Add(Me.Label17)
        Me.pnlDaily.Controls.Add(Me.Label18)
        Me.pnlDaily.Controls.Add(Me.Label19)
        Me.pnlDaily.Controls.Add(Me.Label20)
        Me.pnlDaily.Controls.Add(Me.numDailyFrequency)
        Me.pnlDaily.Controls.Add(Me.lblJobOccursValue)
        Me.pnlDaily.Controls.Add(Me.lblJobOccursHead)
        Me.pnlDaily.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDaily.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDaily.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlDaily.Location = New System.Drawing.Point(3, 49)
        Me.pnlDaily.Name = "pnlDaily"
        Me.pnlDaily.Size = New System.Drawing.Size(397, 92)
        Me.pnlDaily.TabIndex = 1
        '
        'numDailyFrequency
        '
        Me.numDailyFrequency.Location = New System.Drawing.Point(89, 4)
        Me.numDailyFrequency.Maximum = New Decimal(New Integer() {366, 0, 0, 0})
        Me.numDailyFrequency.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numDailyFrequency.Name = "numDailyFrequency"
        Me.numDailyFrequency.Size = New System.Drawing.Size(52, 21)
        Me.numDailyFrequency.TabIndex = 4
        Me.numDailyFrequency.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblJobOccursValue
        '
        Me.lblJobOccursValue.Location = New System.Drawing.Point(143, 7)
        Me.lblJobOccursValue.Name = "lblJobOccursValue"
        Me.lblJobOccursValue.Size = New System.Drawing.Size(74, 14)
        Me.lblJobOccursValue.TabIndex = 5
        Me.lblJobOccursValue.Text = "day(s)"
        '
        'lblJobOccursHead
        '
        Me.lblJobOccursHead.AutoSize = True
        Me.lblJobOccursHead.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobOccursHead.Location = New System.Drawing.Point(6, 8)
        Me.lblJobOccursHead.Name = "lblJobOccursHead"
        Me.lblJobOccursHead.Size = New System.Drawing.Size(77, 13)
        Me.lblJobOccursHead.TabIndex = 3
        Me.lblJobOccursHead.Text = "After Every :"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.optMonthly)
        Me.Panel1.Controls.Add(Me.optWeekly)
        Me.Panel1.Controls.Add(Me.optDaily)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(3, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(397, 32)
        Me.Panel1.TabIndex = 0
        '
        'optMonthly
        '
        Me.optMonthly.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.optMonthly.Location = New System.Drawing.Point(174, 5)
        Me.optMonthly.Name = "optMonthly"
        Me.optMonthly.Size = New System.Drawing.Size(111, 20)
        Me.optMonthly.TabIndex = 2
        Me.optMonthly.Text = "Monthly"
        '
        'optWeekly
        '
        Me.optWeekly.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.optWeekly.Location = New System.Drawing.Point(87, 6)
        Me.optWeekly.Name = "optWeekly"
        Me.optWeekly.Size = New System.Drawing.Size(105, 19)
        Me.optWeekly.TabIndex = 1
        Me.optWeekly.Text = "Weekly"
        '
        'optDaily
        '
        Me.optDaily.Checked = True
        Me.optDaily.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDaily.Location = New System.Drawing.Point(13, 5)
        Me.optDaily.Name = "optDaily"
        Me.optDaily.Size = New System.Drawing.Size(80, 20)
        Me.optDaily.TabIndex = 0
        Me.optDaily.TabStop = True
        Me.optDaily.Text = "Daily"
        '
        'txtAppointmentType
        '
        Me.txtAppointmentType.Location = New System.Drawing.Point(174, 14)
        Me.txtAppointmentType.Name = "txtAppointmentType"
        Me.txtAppointmentType.Size = New System.Drawing.Size(202, 21)
        Me.txtAppointmentType.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(42, 259)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Scheduler Color Coding :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(61, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Appointment Up To :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(65, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Appointment Type :"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_MSTAppointmentScheduler)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(450, 53)
        Me.pnl_tlspTOP.TabIndex = 3
        '
        'tls_MSTAppointmentScheduler
        '
        Me.tls_MSTAppointmentScheduler.BackColor = System.Drawing.Color.Transparent
        Me.tls_MSTAppointmentScheduler.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_MSTAppointmentScheduler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_MSTAppointmentScheduler.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_MSTAppointmentScheduler.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_MSTAppointmentScheduler.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls_MSTAppointmentScheduler.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_MSTAppointmentScheduler.Location = New System.Drawing.Point(0, 0)
        Me.tls_MSTAppointmentScheduler.Name = "tls_MSTAppointmentScheduler"
        Me.tls_MSTAppointmentScheduler.Size = New System.Drawing.Size(450, 52)
        Me.tls_MSTAppointmentScheduler.TabIndex = 0
        Me.tls_MSTAppointmentScheduler.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(36, 49)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&OK"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(48, 49)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Cancel"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 285)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Label9"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Location = New System.Drawing.Point(446, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 285)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Label10"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 287)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(442, 1)
        Me.lbl_pnlBottom.TabIndex = 14
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(4, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(442, 1)
        Me.lbl_pnlTop.TabIndex = 13
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(1, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(410, 1)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 90)
        Me.lbl_pnlLeft.TabIndex = 20
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(411, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 90)
        Me.lbl_pnlRight.TabIndex = 19
        Me.lbl_pnlRight.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(412, 1)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(1, 90)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(410, 1)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 90)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(411, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 90)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(412, 1)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Location = New System.Drawing.Point(1, 91)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(395, 1)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 91)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Location = New System.Drawing.Point(396, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 91)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(397, 1)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Location = New System.Drawing.Point(0, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 31)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Location = New System.Drawing.Point(396, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 31)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(397, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'frmMSTAppointmentScheduler
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(450, 344)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTAppointmentScheduler"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Appointment Scheduler"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.numAppointmentDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numAppointmentUpTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.pnlWeekly.ResumeLayout(False)
        Me.pnlWeekly.PerformLayout()
        Me.pnlMonthly.ResumeLayout(False)
        Me.pnlMonthly.PerformLayout()
        CType(Me.numMonthlyFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDaily.ResumeLayout(False)
        Me.pnlDaily.PerformLayout()
        CType(Me.numDailyFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls_MSTAppointmentScheduler.ResumeLayout(False)
        Me.tls_MSTAppointmentScheduler.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With ColorDialog1
            .AllowFullOpen = True
            .FullOpen = False
            .ShowHelp = False
            .Color = picColor.BackColor
            Try
                .CustomColors = gloGlobal.gloCustomColor.customColor
            Catch ex As Exception

            End Try
            If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                picColor.BackColor = .Color
                Try
                    gloGlobal.gloCustomColor.customColor = .CustomColors
                Catch ex As Exception

                End Try
            End If
        End With
    End Sub

    Private Sub optDaily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDaily.CheckedChanged

    End Sub

    Private Sub optDaily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDaily.Click
        pnlDaily.Visible = True
        pnlWeekly.Visible = False
        pnlMonthly.Visible = False
    End Sub

    Private Sub optWeekly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWeekly.CheckedChanged

    End Sub

    Private Sub optWeekly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optWeekly.Click
        pnlDaily.Visible = False
        pnlWeekly.Visible = True
        pnlMonthly.Visible = False
    End Sub

    Private Sub optMonthly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMonthly.CheckedChanged
        pnlDaily.Visible = False
        pnlWeekly.Visible = False
        pnlMonthly.Visible = True
    End Sub

    Private Sub frmMSTAppointmentScheduler_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub FillAppointmentUpToIntervals()
        With cmbAppointmentUpToInterval
            .Items.Clear()
            .Items.Add("Days")
            .Items.Add("Weeks")
            .Items.Add("Months")
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub OKMSTAppointmentScheduler()

        If Trim(txtAppointmentType.Text) = "" Then
            MessageBox.Show("Please enter Appointment Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAppointmentType.Focus()
            Exit Sub
        End If
        If numAppointmentUpTo.Value > 31 Then
            MessageBox.Show("Appointment Up To must be less than or equal to 31", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            numAppointmentUpTo.Focus()
            Exit Sub
        End If
        Dim objAppointmentScheduler As New clsAppointmentScheduler
        objAppointmentScheduler.AppointmentType = txtAppointmentType.Text
        objAppointmentScheduler.AppointmentUpToDuration = numAppointmentUpTo.Value
        Select Case Trim(cmbAppointmentUpToInterval.Text)
            Case "Days"
                objAppointmentScheduler.AppointmentUpToDurationType = clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
            Case "Weeks"
                objAppointmentScheduler.AppointmentUpToDurationType = clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
            Case "Months"
                objAppointmentScheduler.AppointmentUpToDurationType = clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
        End Select
        If optDaily.Checked = True Then
            objAppointmentScheduler.AppointmentIntervalType = clsAppointmentScheduler.enmAppointmentIntervalType.Daily
            objAppointmentScheduler.AppointmentInterval = numDailyFrequency.Value
        ElseIf optWeekly.Checked = True Then
            objAppointmentScheduler.AppointmentIntervalType = clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
            Dim nAppointmentDuration As Integer
            nAppointmentDuration = 1
            If chkMon.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 2
            End If
            If chkTue.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 3
            End If
            If chkWed.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 5
            End If
            If chkThu.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 7
            End If
            If chkFri.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 11
            End If
            If chkSat.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 13
            End If
            If chkSun.Checked = True Then
                nAppointmentDuration = nAppointmentDuration * 17
            End If
            objAppointmentScheduler.AppointmentInterval = nAppointmentDuration
        ElseIf optMonthly.Checked = True Then
            objAppointmentScheduler.AppointmentIntervalType = clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
            objAppointmentScheduler.AppointmentInterval = numMonthlyFrequency.Value
        End If
        objAppointmentScheduler.AppointmentDuration = numAppointmentDuration.Value
        objAppointmentScheduler.ColorCode = picColor.BackColor.ToArgb
        If blnModify = False Then
            If objAppointmentScheduler.AddAppointmentSchedulerType() = False Then
                MessageBox.Show("Unable to add Appointment scheduler type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objAppointmentScheduler = Nothing
                Exit Sub
            End If
        Else
            If objAppointmentScheduler.ModifyAppointmentSchedulerType(txtAppointmentType.Tag) = False Then
                MessageBox.Show("Unable to modify Appointment scheduler type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objAppointmentScheduler = Nothing
                Exit Sub
            End If
        End If
        objAppointmentScheduler = Nothing
        Me.Close()
    End Sub

    Private Sub tls_MSTAppointmentScheduler_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_MSTAppointmentScheduler.ItemClicked
        Try
            Select Case UCase(e.ClickedItem.Tag)
                Case UCase("OK")
                    OKMSTAppointmentScheduler()
                Case UCase("Cancel")
                    Me.Close()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub
End Class
