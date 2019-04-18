Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloGeneral
Imports System.Linq


Public Class CustomPrescription
    Inherits System.Windows.Forms.UserControl

#Region "Properties and Variables"
    Private WithEvents dgCustomGrid As CustomTask
    Friend WithEvents pnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnclrsing As System.Windows.Forms.Button
    Public WithEvents lstChfcomp As System.Windows.Forms.ListBox
    Friend WithEvents txtDosageFrequencyValue As System.Windows.Forms.TextBox
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private WithEvents cmbReasonToOverride As System.Windows.Forms.ComboBox   '' Chetan Added on 14 Oct 2010

    'for dosage calc - passed from the AddControl function from the Rx form because we get the PatientId = 0. so passed the global patient id value from the Rx form
    Private _nRxPatientid As Long
    Friend WithEvents cmbPharmacy As System.Windows.Forms.ComboBox
    Dim _RxGridNDCCode As String = ""
    Friend WithEvents chkCPOEOrder As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDoseUnit As System.Windows.Forms.ComboBox
    Friend WithEvents ChkMedicationAdministered As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtReasonToOverride As System.Windows.Forms.TextBox
    Friend WithEvents txtPharmacy As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtPriorAuthorization As System.Windows.Forms.TextBox
    Friend WithEvents cmbRoute As System.Windows.Forms.ComboBox
    Friend WithEvents lblRouteAstr As System.Windows.Forms.Label
    ''''''class level variable to set the drug data against the drug selected from medication grid and not from collection
    Public gblnIncludeFrequencyAbbrevationInRxMeds As Boolean = False
#End Region


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal objRxBusinessLayer As RxBusinesslayer, ByVal _RxRowItemnumber As Int32, ByVal DisableERXFields As Boolean, Optional ByVal _npatientid As Long = 0, Optional ByVal RxGridNDCCode As String = "")
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()


        cmbReasonToOverride.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbReasonToOverride.DrawItem, AddressOf ShowTooltipOnComboBox
        'cmbReasonToOverride.DropDownStyle = ComboBoxStyle.DropDownList
        tlTip = Me.Width

        _RxGridNDCCode = RxGridNDCCode

        'Add any initialization after the InitializeComponent() call
        _RXBusinessLayer = objRxBusinessLayer
        _RowIndex = _RxRowItemnumber '''''_RowIndex will contain the value from the RowNumber col of Rx grid, which will keep track with the item Number from the Rx collection

        'FillPotencyCode()

        cmbDuration.Items.Add("Days")
        cmbDuration.Items.Add("Weeks")
        cmbDuration.Items.Add("Months")
        cmbDuration.Text = cmbDuration.Items(0)

        CmbMethod.Items.Add("Fax")
        CmbMethod.Items.Add("Print")
        CmbMethod.Items.Add("Phone")
        CmbMethod.Items.Add("Sample")
        CmbMethod.Items.Add("HandWritten")
        CmbMethod.Items.Add("eRx")
        CmbMethod.Items.Add("OTC")
        'for dosage calc - assign the gnpatietid var passed from the Rxform to the local var in the custom Rx control
        _nRxPatientid = _npatientid

        If DisableERXFields Then
            txtAmount.ReadOnly = True
            cmbDoseUnit.Enabled = False
            cmbDrugsForm.Enabled = False
            dtpStartDate.Enabled = False
            dtpEndDate.Enabled = False
            dtpExpiryDate.Enabled = False
            txtDuration.ReadOnly = True
            cmbDuration.Enabled = False
            txtFrequency.ReadOnly = True
            txtroute.ReadOnly = True
            btnSig.Enabled = False
            txtDosage.ReadOnly = True
            CmbMethod.Enabled = False
            txtRefills.ReadOnly = True
            chkMaysubstitute.Enabled = False
            chkPRN.Enabled = False
            cmbPharmacy.Enabled = False
            btnSelectPharmacy.Enabled = False
            txtNotes.ReadOnly = True
            txtPrescriberNotes.ReadOnly = True
            ts_btnDoseCalculator.Enabled = False
        End If

    End Sub

    Dim tlTip As Integer
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        cmbReasonToOverride.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbReasonToOverride.DrawItem, AddressOf ShowTooltipOnComboBox
        'cmbReasonToOverride.DropDownStyle = ComboBoxStyle.DropDownList
        tlTip = Me.Width

        cmbDuration.Items.Add("Days")
        cmbDuration.Items.Add("Weeks")
        cmbDuration.Items.Add("Months")
        cmbDuration.Text = cmbDuration.Items(0)

        'Add any initialization after the InitializeComponent() call
        CmbMethod.Items.Add("Fax")
        CmbMethod.Items.Add("Print")
        CmbMethod.Items.Add("Phone")
        CmbMethod.Items.Add("Sample")
        CmbMethod.Items.Add("HandWritten")
        CmbMethod.Items.Add("eRx")
        CmbMethod.Items.Add("OTC")
        FillPotencyCode()
    End Sub

    Private Sub ShowTooltipOnComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        Try

            'cmbDrugsForm = DirectCast(sender, ComboBox)
            If cmbReasonToOverride.Items.Count > 0 AndAlso e.Index >= 0 Then

                e.DrawBackground()
                Using br As New SolidBrush(e.ForeColor)
                    e.Graphics.DrawString(cmbReasonToOverride.GetItemText(cmbReasonToOverride.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
                End Using

                If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                    If cmbReasonToOverride.DroppedDown Then
                        'Me.Width = 388
                        If getWidthofListItems(cmbReasonToOverride.GetItemText(cmbReasonToOverride.Items(e.Index)).ToString(), cmbReasonToOverride) >= cmbReasonToOverride.DropDownWidth - 2 Then
                            ToolTip1.Show(cmbReasonToOverride.GetItemText(cmbReasonToOverride.Items(e.Index)), cmbReasonToOverride, e.Bounds.Right - 100, e.Bounds.Bottom)
                        End If
                    Else
                        ToolTip1.Hide(cmbReasonToOverride)
                    End If
                Else
                    ToolTip1.Hide(cmbReasonToOverride)
                End If
                e.DrawFocusRectangle()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer
        Try

            'Dim g As Graphics = CreateGraphics()
            'Dim s As SizeF = g.MeasureString(_text, comboBox1.Font)
            'Width = Convert.ToInt32(s.Width)
            Width = Me.Width
            Return Width

        Catch ex As Exception
            Return 0
        End Try
    End Function

#Region "Windows Form Designer Code"



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDosage As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtroute As System.Windows.Forms.TextBox
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbMethod As System.Windows.Forms.ComboBox
    Friend WithEvents chkMaysubstitute As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSig As System.Windows.Forms.Button
    Friend WithEvents txtRefills As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblProblems As System.Windows.Forms.Label
    Friend WithEvents btnclosecomplaints As System.Windows.Forms.Button
    Friend WithEvents txtChiefComplaint As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkPRN As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDuration As System.Windows.Forms.ComboBox
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Caption1 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents ts_btnDoseCalculator As System.Windows.Forms.ToolStripButton
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnl_Caption As System.Windows.Forms.Panel
    Friend WithEvents lblPharmacy As System.Windows.Forms.Label
    Friend WithEvents btnSelectPharmacy As System.Windows.Forms.Button
    Friend WithEvents lblMedicationNDCCode As System.Windows.Forms.Label
    Friend WithEvents cmbDrugsForm As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPrescriberNotes As System.Windows.Forms.TextBox
    Private WithEvents ts_btnAddtoFavourites As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomPrescription))
        Me.cmbDuration = New System.Windows.Forms.ComboBox()
        Me.chkPRN = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbReasonToOverride = New System.Windows.Forms.ComboBox()
        Me.txtChiefComplaint = New System.Windows.Forms.TextBox()
        Me.lblProblems = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRefills = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.chkMaysubstitute = New System.Windows.Forms.CheckBox()
        Me.CmbMethod = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtroute = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDosage = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lblRouteAstr = New System.Windows.Forms.Label()
        Me.cmbRoute = New System.Windows.Forms.ComboBox()
        Me.pnlCustomTask = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtPriorAuthorization = New System.Windows.Forms.TextBox()
        Me.txtReasonToOverride = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lstChfcomp = New System.Windows.Forms.ListBox()
        Me.btnclrsing = New System.Windows.Forms.Button()
        Me.btnclear = New System.Windows.Forms.Button()
        Me.btnclosecomplaints = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtPrescriberNotes = New System.Windows.Forms.TextBox()
        Me.chkCPOEOrder = New System.Windows.Forms.CheckBox()
        Me.ChkMedicationAdministered = New System.Windows.Forms.CheckBox()
        Me.cmbDoseUnit = New System.Windows.Forms.ComboBox()
        Me.cmbPharmacy = New System.Windows.Forms.ComboBox()
        Me.txtDosageFrequencyValue = New System.Windows.Forms.TextBox()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.cmbDrugsForm = New System.Windows.Forms.ComboBox()
        Me.lblPharmacy = New System.Windows.Forms.Label()
        Me.txtPharmacy = New System.Windows.Forms.TextBox()
        Me.btnSelectPharmacy = New System.Windows.Forms.Button()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.btnSig = New System.Windows.Forms.Button()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnDoseCalculator = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnAddtoFavourites = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnl_Caption = New System.Windows.Forms.Panel()
        Me.pnl_Caption1 = New System.Windows.Forms.Panel()
        Me.lblMedicationNDCCode = New System.Windows.Forms.Label()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlCustomTask.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.pnl_Caption.SuspendLayout()
        Me.pnl_Caption1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbDuration
        '
        Me.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuration.ForeColor = System.Drawing.Color.Black
        Me.cmbDuration.Location = New System.Drawing.Point(265, 89)
        Me.cmbDuration.Name = "cmbDuration"
        Me.cmbDuration.Size = New System.Drawing.Size(85, 22)
        Me.cmbDuration.TabIndex = 11
        '
        'chkPRN
        '
        Me.chkPRN.AutoSize = True
        Me.chkPRN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPRN.Location = New System.Drawing.Point(751, 35)
        Me.chkPRN.Name = "chkPRN"
        Me.chkPRN.Size = New System.Drawing.Size(48, 18)
        Me.chkPRN.TabIndex = 9
        Me.chkPRN.Text = "PRN"
        Me.chkPRN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.chkPRN, "Refill As Needed")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbReasonToOverride)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(429, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 52)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reason to Override Drug Interaction"
        '
        'cmbReasonToOverride
        '
        Me.cmbReasonToOverride.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbReasonToOverride.ForeColor = System.Drawing.Color.Black
        Me.cmbReasonToOverride.FormattingEnabled = True
        Me.cmbReasonToOverride.Location = New System.Drawing.Point(3, 21)
        Me.cmbReasonToOverride.MaxLength = 255
        Me.cmbReasonToOverride.Name = "cmbReasonToOverride"
        Me.cmbReasonToOverride.Size = New System.Drawing.Size(296, 22)
        Me.cmbReasonToOverride.TabIndex = 0
        '
        'txtChiefComplaint
        '
        Me.txtChiefComplaint.BackColor = System.Drawing.Color.White
        Me.txtChiefComplaint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChiefComplaint.ForeColor = System.Drawing.Color.Black
        Me.txtChiefComplaint.Location = New System.Drawing.Point(305, 209)
        Me.txtChiefComplaint.MaxLength = 255
        Me.txtChiefComplaint.Multiline = True
        Me.txtChiefComplaint.Name = "txtChiefComplaint"
        Me.txtChiefComplaint.ReadOnly = True
        Me.txtChiefComplaint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtChiefComplaint.Size = New System.Drawing.Size(0, 0)
        Me.txtChiefComplaint.TabIndex = 10
        '
        'lblProblems
        '
        Me.lblProblems.AutoSize = True
        Me.lblProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProblems.Location = New System.Drawing.Point(17, 5)
        Me.lblProblems.Name = "lblProblems"
        Me.lblProblems.Size = New System.Drawing.Size(64, 14)
        Me.lblProblems.TabIndex = 27
        Me.lblProblems.Text = "Problems :"
        Me.lblProblems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(235, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 14)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Exp Date :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpExpiryDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpExpiryDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpExpiryDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpExpiryDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpExpiryDate.Checked = False
        Me.dtpExpiryDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpExpiryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(304, 30)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.ShowCheckBox = True
        Me.dtpExpiryDate.Size = New System.Drawing.Size(116, 22)
        Me.dtpExpiryDate.TabIndex = 3
        '
        'txtLotNo
        '
        Me.txtLotNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotNo.ForeColor = System.Drawing.Color.Black
        Me.txtLotNo.Location = New System.Drawing.Point(304, 3)
        Me.txtLotNo.MaxLength = 255
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.Size = New System.Drawing.Size(116, 22)
        Me.txtLotNo.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(248, 7)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 14)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Lot No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRefills
        '
        Me.txtRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefills.ForeColor = System.Drawing.Color.Black
        Me.txtRefills.Location = New System.Drawing.Point(541, 33)
        Me.txtRefills.MaxLength = 2
        Me.txtRefills.Name = "txtRefills"
        Me.txtRefills.Size = New System.Drawing.Size(164, 22)
        Me.txtRefills.TabIndex = 5
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtNotes)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(429, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(302, 54)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Pharmacy Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(3, 18)
        Me.txtNotes.MaxLength = 210
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(296, 33)
        Me.txtNotes.TabIndex = 1
        '
        'chkMaysubstitute
        '
        Me.chkMaysubstitute.AutoSize = True
        Me.chkMaysubstitute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMaysubstitute.Location = New System.Drawing.Point(751, 9)
        Me.chkMaysubstitute.Name = "chkMaysubstitute"
        Me.chkMaysubstitute.Size = New System.Drawing.Size(125, 18)
        Me.chkMaysubstitute.TabIndex = 8
        Me.chkMaysubstitute.Text = "Allow Substitution"
        Me.chkMaysubstitute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbMethod
        '
        Me.CmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbMethod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbMethod.ForeColor = System.Drawing.Color.Black
        Me.CmbMethod.Location = New System.Drawing.Point(541, 5)
        Me.CmbMethod.Name = "CmbMethod"
        Me.CmbMethod.Size = New System.Drawing.Size(164, 22)
        Me.CmbMethod.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(494, 37)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 14)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Refills :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(449, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 14)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Issue Method :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpEndDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(112, 30)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(111, 22)
        Me.dtpEndDate.TabIndex = 2
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpStartDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(112, 3)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(111, 22)
        Me.dtpStartDate.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(42, 33)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 14)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "End Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(36, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Start Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.Black
        Me.txtAmount.Location = New System.Drawing.Point(126, 117)
        Me.txtAmount.MaxLength = 11
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(70, 22)
        Me.txtAmount.TabIndex = 14
        '
        'txtDuration
        '
        Me.txtDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDuration.ForeColor = System.Drawing.Color.Black
        Me.txtDuration.Location = New System.Drawing.Point(126, 89)
        Me.txtDuration.MaxLength = 3
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.ShortcutsEnabled = False
        Me.txtDuration.Size = New System.Drawing.Size(134, 22)
        Me.txtDuration.TabIndex = 10
        '
        'txtFrequency
        '
        Me.txtFrequency.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtFrequency.Location = New System.Drawing.Point(126, 61)
        Me.txtFrequency.MaxLength = 140
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(224, 22)
        Me.txtFrequency.TabIndex = 6
        Me.txtFrequency.Tag = ""
        '
        'txtroute
        '
        Me.txtroute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtroute.ForeColor = System.Drawing.Color.Black
        Me.txtroute.Location = New System.Drawing.Point(126, 33)
        Me.txtroute.MaxLength = 255
        Me.txtroute.Name = "txtroute"
        Me.txtroute.Size = New System.Drawing.Size(224, 22)
        Me.txtroute.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(60, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Quantity :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(61, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Duration :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Patient Directions :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(74, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Route :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDosage
        '
        Me.txtDosage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosage.ForeColor = System.Drawing.Color.Black
        Me.txtDosage.Location = New System.Drawing.Point(126, 5)
        Me.txtDosage.MaxLength = 70
        Me.txtDosage.Name = "txtDosage"
        Me.txtDosage.Size = New System.Drawing.Size(224, 22)
        Me.txtDosage.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(67, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dosage :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_Base
        '
        Me.pnl_Base.AutoScroll = True
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lblRouteAstr)
        Me.pnl_Base.Controls.Add(Me.cmbRoute)
        Me.pnl_Base.Controls.Add(Me.pnlCustomTask)
        Me.pnl_Base.Controls.Add(Me.Label22)
        Me.pnl_Base.Controls.Add(Me.txtPriorAuthorization)
        Me.pnl_Base.Controls.Add(Me.txtReasonToOverride)
        Me.pnl_Base.Controls.Add(Me.Panel2)
        Me.pnl_Base.Controls.Add(Me.cmbDoseUnit)
        Me.pnl_Base.Controls.Add(Me.cmbPharmacy)
        Me.pnl_Base.Controls.Add(Me.txtDosageFrequencyValue)
        Me.pnl_Base.Controls.Add(Me.txtDoseUnit)
        Me.pnl_Base.Controls.Add(Me.cmbDrugsForm)
        Me.pnl_Base.Controls.Add(Me.lblPharmacy)
        Me.pnl_Base.Controls.Add(Me.txtPharmacy)
        Me.pnl_Base.Controls.Add(Me.btnSelectPharmacy)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.cmbDuration)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.chkPRN)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Controls.Add(Me.Label1)
        Me.pnl_Base.Controls.Add(Me.txtroute)
        Me.pnl_Base.Controls.Add(Me.Label9)
        Me.pnl_Base.Controls.Add(Me.txtDosage)
        Me.pnl_Base.Controls.Add(Me.CmbMethod)
        Me.pnl_Base.Controls.Add(Me.Label8)
        Me.pnl_Base.Controls.Add(Me.Label2)
        Me.pnl_Base.Controls.Add(Me.chkMaysubstitute)
        Me.pnl_Base.Controls.Add(Me.txtChiefComplaint)
        Me.pnl_Base.Controls.Add(Me.Label3)
        Me.pnl_Base.Controls.Add(Me.Label4)
        Me.pnl_Base.Controls.Add(Me.btnSig)
        Me.pnl_Base.Controls.Add(Me.Label5)
        Me.pnl_Base.Controls.Add(Me.txtRefills)
        Me.pnl_Base.Controls.Add(Me.txtAmount)
        Me.pnl_Base.Controls.Add(Me.txtFrequency)
        Me.pnl_Base.Controls.Add(Me.txtDuration)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 84)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Size = New System.Drawing.Size(970, 315)
        Me.pnl_Base.TabIndex = 0
        '
        'lblRouteAstr
        '
        Me.lblRouteAstr.AutoSize = True
        Me.lblRouteAstr.BackColor = System.Drawing.Color.Transparent
        Me.lblRouteAstr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRouteAstr.ForeColor = System.Drawing.Color.Red
        Me.lblRouteAstr.Location = New System.Drawing.Point(62, 38)
        Me.lblRouteAstr.Name = "lblRouteAstr"
        Me.lblRouteAstr.Size = New System.Drawing.Size(14, 14)
        Me.lblRouteAstr.TabIndex = 314
        Me.lblRouteAstr.Text = "*"
        Me.lblRouteAstr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbRoute
        '
        Me.cmbRoute.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(6, Byte), Integer), CType(CType(6, Byte), Integer))
        Me.cmbRoute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRoute.ForeColor = System.Drawing.Color.White
        Me.cmbRoute.Location = New System.Drawing.Point(126, 33)
        Me.cmbRoute.Name = "cmbRoute"
        Me.cmbRoute.Size = New System.Drawing.Size(224, 22)
        Me.cmbRoute.TabIndex = 304
        '
        'pnlCustomTask
        '
        Me.pnlCustomTask.Controls.Add(Me.Label18)
        Me.pnlCustomTask.Controls.Add(Me.Label19)
        Me.pnlCustomTask.Controls.Add(Me.Label20)
        Me.pnlCustomTask.Controls.Add(Me.Label21)
        Me.pnlCustomTask.Location = New System.Drawing.Point(354, 61)
        Me.pnlCustomTask.Name = "pnlCustomTask"
        Me.pnlCustomTask.Size = New System.Drawing.Size(196, 92)
        Me.pnlCustomTask.TabIndex = 301
        Me.pnlCustomTask.TabStop = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1, 91)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(194, 1)
        Me.Label18.TabIndex = 301
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(194, 1)
        Me.Label19.TabIndex = 300
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(195, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 92)
        Me.Label20.TabIndex = 299
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 92)
        Me.Label21.TabIndex = 298
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(410, 93)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(128, 14)
        Me.Label22.TabIndex = 303
        Me.Label22.Text = "Prior Authorization # :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPriorAuthorization
        '
        Me.txtPriorAuthorization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriorAuthorization.ForeColor = System.Drawing.Color.Black
        Me.txtPriorAuthorization.Location = New System.Drawing.Point(541, 89)
        Me.txtPriorAuthorization.MaxLength = 140
        Me.txtPriorAuthorization.Name = "txtPriorAuthorization"
        Me.txtPriorAuthorization.Size = New System.Drawing.Size(164, 22)
        Me.txtPriorAuthorization.TabIndex = 302
        '
        'txtReasonToOverride
        '
        Me.txtReasonToOverride.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReasonToOverride.ForeColor = System.Drawing.Color.Black
        Me.txtReasonToOverride.Location = New System.Drawing.Point(918, 260)
        Me.txtReasonToOverride.MaxLength = 1500
        Me.txtReasonToOverride.Multiline = True
        Me.txtReasonToOverride.Name = "txtReasonToOverride"
        Me.txtReasonToOverride.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReasonToOverride.Size = New System.Drawing.Size(10, 11)
        Me.txtReasonToOverride.TabIndex = 0
        Me.txtReasonToOverride.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.dtpStartDate)
        Me.Panel2.Controls.Add(Me.txtLotNo)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.chkCPOEOrder)
        Me.Panel2.Controls.Add(Me.dtpEndDate)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.dtpExpiryDate)
        Me.Panel2.Controls.Add(Me.ChkMedicationAdministered)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Location = New System.Drawing.Point(14, 143)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(862, 167)
        Me.Panel2.TabIndex = 18
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lstChfcomp)
        Me.Panel1.Controls.Add(Me.btnclrsing)
        Me.Panel1.Controls.Add(Me.btnclear)
        Me.Panel1.Controls.Add(Me.btnclosecomplaints)
        Me.Panel1.Controls.Add(Me.lblProblems)
        Me.Panel1.Location = New System.Drawing.Point(25, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(363, 82)
        Me.Panel1.TabIndex = 302
        '
        'lstChfcomp
        '
        Me.lstChfcomp.FormattingEnabled = True
        Me.lstChfcomp.HorizontalExtent = 5
        Me.lstChfcomp.HorizontalScrollbar = True
        Me.lstChfcomp.ItemHeight = 14
        Me.lstChfcomp.Location = New System.Drawing.Point(86, 3)
        Me.lstChfcomp.Name = "lstChfcomp"
        Me.lstChfcomp.Size = New System.Drawing.Size(212, 74)
        Me.lstChfcomp.TabIndex = 0
        '
        'btnclrsing
        '
        Me.btnclrsing.BackColor = System.Drawing.Color.Transparent
        Me.btnclrsing.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnclrsing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrsing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrsing.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclrsing.Image = CType(resources.GetObject("btnclrsing.Image"), System.Drawing.Image)
        Me.btnclrsing.Location = New System.Drawing.Point(303, 29)
        Me.btnclrsing.Name = "btnclrsing"
        Me.btnclrsing.Size = New System.Drawing.Size(22, 22)
        Me.btnclrsing.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnclrsing, "Clear Selected")
        Me.btnclrsing.UseVisualStyleBackColor = False
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.Transparent
        Me.btnclear.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnclear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.Image = CType(resources.GetObject("btnclear.Image"), System.Drawing.Image)
        Me.btnclear.Location = New System.Drawing.Point(303, 55)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(22, 22)
        Me.btnclear.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnclear, "Clear All")
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnclosecomplaints
        '
        Me.btnclosecomplaints.BackColor = System.Drawing.Color.Transparent
        Me.btnclosecomplaints.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnclosecomplaints.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclosecomplaints.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclosecomplaints.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclosecomplaints.Image = CType(resources.GetObject("btnclosecomplaints.Image"), System.Drawing.Image)
        Me.btnclosecomplaints.Location = New System.Drawing.Point(303, 4)
        Me.btnclosecomplaints.Name = "btnclosecomplaints"
        Me.btnclosecomplaints.Size = New System.Drawing.Size(22, 22)
        Me.btnclosecomplaints.TabIndex = 3
        Me.btnclosecomplaints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnclosecomplaints, "Select Chief Complaints")
        Me.btnclosecomplaints.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPrescriberNotes)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(429, 54)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(302, 54)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Prescriber Notes"
        '
        'txtPrescriberNotes
        '
        Me.txtPrescriberNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPrescriberNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrescriberNotes.ForeColor = System.Drawing.Color.Black
        Me.txtPrescriberNotes.Location = New System.Drawing.Point(3, 18)
        Me.txtPrescriberNotes.MaxLength = 210
        Me.txtPrescriberNotes.Multiline = True
        Me.txtPrescriberNotes.Name = "txtPrescriberNotes"
        Me.txtPrescriberNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrescriberNotes.Size = New System.Drawing.Size(296, 33)
        Me.txtPrescriberNotes.TabIndex = 0
        '
        'chkCPOEOrder
        '
        Me.chkCPOEOrder.AutoSize = True
        Me.chkCPOEOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCPOEOrder.Location = New System.Drawing.Point(737, 30)
        Me.chkCPOEOrder.Name = "chkCPOEOrder"
        Me.chkCPOEOrder.Size = New System.Drawing.Size(115, 18)
        Me.chkCPOEOrder.TabIndex = 3
        Me.chkCPOEOrder.Text = "Order Not CPOE"
        Me.chkCPOEOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.chkCPOEOrder, "MU Reporting: Recording of a previous order from this practice. ")
        '
        'ChkMedicationAdministered
        '
        Me.ChkMedicationAdministered.AutoSize = True
        Me.ChkMedicationAdministered.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMedicationAdministered.Location = New System.Drawing.Point(737, 3)
        Me.ChkMedicationAdministered.Name = "ChkMedicationAdministered"
        Me.ChkMedicationAdministered.Size = New System.Drawing.Size(97, 18)
        Me.ChkMedicationAdministered.TabIndex = 2
        Me.ChkMedicationAdministered.Text = "Administered"
        Me.ChkMedicationAdministered.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDoseUnit
        '
        Me.cmbDoseUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.cmbDoseUnit.Location = New System.Drawing.Point(199, 117)
        Me.cmbDoseUnit.Name = "cmbDoseUnit"
        Me.cmbDoseUnit.Size = New System.Drawing.Size(132, 22)
        Me.cmbDoseUnit.TabIndex = 15
        '
        'cmbPharmacy
        '
        Me.cmbPharmacy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPharmacy.FormattingEnabled = True
        Me.cmbPharmacy.Location = New System.Drawing.Point(541, 61)
        Me.cmbPharmacy.Name = "cmbPharmacy"
        Me.cmbPharmacy.Size = New System.Drawing.Size(163, 22)
        Me.cmbPharmacy.TabIndex = 12
        '
        'txtDosageFrequencyValue
        '
        Me.txtDosageFrequencyValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosageFrequencyValue.ForeColor = System.Drawing.Color.Black
        Me.txtDosageFrequencyValue.Location = New System.Drawing.Point(354, 61)
        Me.txtDosageFrequencyValue.MaxLength = 15
        Me.txtDosageFrequencyValue.Name = "txtDosageFrequencyValue"
        Me.txtDosageFrequencyValue.Size = New System.Drawing.Size(25, 22)
        Me.txtDosageFrequencyValue.TabIndex = 7
        Me.txtDosageFrequencyValue.Visible = False
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.txtDoseUnit.Location = New System.Drawing.Point(201, 117)
        Me.txtDoseUnit.MaxLength = 30
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.ShortcutsEnabled = False
        Me.txtDoseUnit.Size = New System.Drawing.Size(58, 22)
        Me.txtDoseUnit.TabIndex = 15
        '
        'cmbDrugsForm
        '
        Me.cmbDrugsForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDrugsForm.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbDrugsForm.FormattingEnabled = True
        Me.cmbDrugsForm.Location = New System.Drawing.Point(335, 117)
        Me.cmbDrugsForm.Name = "cmbDrugsForm"
        Me.cmbDrugsForm.Size = New System.Drawing.Size(100, 22)
        Me.cmbDrugsForm.TabIndex = 16
        '
        'lblPharmacy
        '
        Me.lblPharmacy.AutoSize = True
        Me.lblPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPharmacy.Location = New System.Drawing.Point(471, 65)
        Me.lblPharmacy.Name = "lblPharmacy"
        Me.lblPharmacy.Size = New System.Drawing.Size(67, 14)
        Me.lblPharmacy.TabIndex = 37
        Me.lblPharmacy.Text = "Pharmacy :"
        Me.lblPharmacy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPharmacy
        '
        Me.txtPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPharmacy.ForeColor = System.Drawing.Color.Black
        Me.txtPharmacy.Location = New System.Drawing.Point(934, 257)
        Me.txtPharmacy.MaxLength = 70
        Me.txtPharmacy.Name = "txtPharmacy"
        Me.txtPharmacy.Size = New System.Drawing.Size(10, 22)
        Me.txtPharmacy.TabIndex = 49
        Me.txtPharmacy.Visible = False
        '
        'btnSelectPharmacy
        '
        Me.btnSelectPharmacy.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectPharmacy.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnSelectPharmacy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectPharmacy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPharmacy.Image = CType(resources.GetObject("btnSelectPharmacy.Image"), System.Drawing.Image)
        Me.btnSelectPharmacy.Location = New System.Drawing.Point(708, 61)
        Me.btnSelectPharmacy.Name = "btnSelectPharmacy"
        Me.btnSelectPharmacy.Size = New System.Drawing.Size(22, 22)
        Me.btnSelectPharmacy.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.btnSelectPharmacy, "Select Pharmacy")
        Me.btnSelectPharmacy.UseVisualStyleBackColor = False
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 314)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(968, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 314)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(969, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 314)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(970, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'btnSig
        '
        Me.btnSig.BackColor = System.Drawing.Color.Transparent
        Me.btnSig.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnSig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSig.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSig.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSig.Image = CType(resources.GetObject("btnSig.Image"), System.Drawing.Image)
        Me.btnSig.Location = New System.Drawing.Point(354, 33)
        Me.btnSig.Name = "btnSig"
        Me.btnSig.Size = New System.Drawing.Size(22, 22)
        Me.btnSig.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnSig, "Select SIG")
        Me.btnSig.UseVisualStyleBackColor = False
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls)
        Me.pnl_tlspTOP.Controls.Add(Me.Label16)
        Me.pnl_tlspTOP.Controls.Add(Me.Label17)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(970, 55)
        Me.pnl_tlspTOP.TabIndex = 1
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnDoseCalculator, Me.ts_btnOk, Me.ts_btnAddtoFavourites, Me.ts_btnCancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(1, 1)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(969, 53)
        Me.tls.TabIndex = 0
        Me.tls.TabStop = True
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnDoseCalculator
        '
        Me.ts_btnDoseCalculator.Image = CType(resources.GetObject("ts_btnDoseCalculator.Image"), System.Drawing.Image)
        Me.ts_btnDoseCalculator.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDoseCalculator.Name = "ts_btnDoseCalculator"
        Me.ts_btnDoseCalculator.Size = New System.Drawing.Size(106, 50)
        Me.ts_btnDoseCalculator.Tag = "Dose Calculator"
        Me.ts_btnDoseCalculator.Text = "&Dose Calculator"
        Me.ts_btnDoseCalculator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnAddtoFavourites
        '
        Me.ts_btnAddtoFavourites.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAddtoFavourites.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAddtoFavourites.Image = CType(resources.GetObject("ts_btnAddtoFavourites.Image"), System.Drawing.Image)
        Me.ts_btnAddtoFavourites.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAddtoFavourites.Name = "ts_btnAddtoFavourites"
        Me.ts_btnAddtoFavourites.Size = New System.Drawing.Size(114, 50)
        Me.ts_btnAddtoFavourites.Tag = "Add To Favorites"
        Me.ts_btnAddtoFavourites.Text = "&Add To Favorites"
        Me.ts_btnAddtoFavourites.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(969, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 55)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label4"
        '
        'pnl_Caption
        '
        Me.pnl_Caption.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Caption.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        Me.pnl_Caption.Controls.Add(Me.pnl_Caption1)
        Me.pnl_Caption.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Caption.Location = New System.Drawing.Point(0, 55)
        Me.pnl_Caption.Name = "pnl_Caption"
        Me.pnl_Caption.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnl_Caption.Size = New System.Drawing.Size(970, 29)
        Me.pnl_Caption.TabIndex = 38
        Me.pnl_Caption.TabStop = True
        '
        'pnl_Caption1
        '
        Me.pnl_Caption1.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Caption1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnl_Caption1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_Caption1.Controls.Add(Me.lblMedicationNDCCode)
        Me.pnl_Caption1.Controls.Add(Me.lblCaption)
        Me.pnl_Caption1.Controls.Add(Me.Label12)
        Me.pnl_Caption1.Controls.Add(Me.Label13)
        Me.pnl_Caption1.Controls.Add(Me.Label14)
        Me.pnl_Caption1.Controls.Add(Me.Label15)
        Me.pnl_Caption1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Caption1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Caption1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Caption1.Location = New System.Drawing.Point(0, 3)
        Me.pnl_Caption1.Name = "pnl_Caption1"
        Me.pnl_Caption1.Size = New System.Drawing.Size(970, 23)
        Me.pnl_Caption1.TabIndex = 37
        '
        'lblMedicationNDCCode
        '
        Me.lblMedicationNDCCode.AutoSize = True
        Me.lblMedicationNDCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblMedicationNDCCode.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMedicationNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedicationNDCCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMedicationNDCCode.Location = New System.Drawing.Point(20, 1)
        Me.lblMedicationNDCCode.Name = "lblMedicationNDCCode"
        Me.lblMedicationNDCCode.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblMedicationNDCCode.Size = New System.Drawing.Size(19, 20)
        Me.lblMedicationNDCCode.TabIndex = 49
        Me.lblMedicationNDCCode.Text = "  "
        Me.lblMedicationNDCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.BackColor = System.Drawing.Color.Transparent
        Me.lblCaption.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCaption.Location = New System.Drawing.Point(1, 1)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblCaption.Size = New System.Drawing.Size(19, 20)
        Me.lblCaption.TabIndex = 47
        Me.lblCaption.Text = "  "
        Me.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(1, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(968, 1)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(969, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 22)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(970, 1)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'CustomPrescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_Caption)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "CustomPrescription"
        Me.Size = New System.Drawing.Size(970, 399)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.pnlCustomTask.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.pnl_Caption.ResumeLayout(False)
        Me.pnl_Caption1.ResumeLayout(False)
        Me.pnl_Caption1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
#End Region

    Public Event OKClick(ByVal ndc As String, ByVal amt As Decimal, ByVal frequency As Int16, ByVal duration As Integer, ByVal durationunit As String, ByVal SelectedRxColItem As Integer)

    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CheckedClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SigClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SigClick_DrugProvAsso(ByVal sender As Object, ByVal e As System.EventArgs, ByVal sDrugName As String)
    'RxHub
    Public Event PharmacyClick(ByVal sender As Object, ByVal e As System.EventArgs)
    'RxHub
    Public Event CloseComplaintClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private m_DrugId As Int64
    Private m_IsNarcotics As Int64
    Private m_DosageForm As String
    Private m_ProviderID As Int64
    Private m_PharmacyID As Int64
    Private m_renewed As String
    Private sMessagetype As String = ""
    Private _RXBusinessLayer As RxBusinesslayer
    Private _RowIndex As Int32
    Private _Prescription As Prescription
    Private _ChiefComplaint As String
    Dim blnCheckDb As Boolean = True
    Dim blnGetFreqvalonTab As Boolean = False
    'for Dosage Calculator

    Dim SelectedItemInCol As Integer

    'Dim WithEvents calculator As frmDosageCalculator
    Private _gblnIncludeFrequencyAbbrevationInRxMeds As Boolean
    Public Problems As New List(Of String)

    Public Property ChiefComplaint() As String
        Get
            Return txtChiefComplaint.Text
        End Get
        Set(ByVal value As String)
            txtChiefComplaint.Text = value
        End Set
    End Property


    Public ReadOnly Property PrescriptionObject() As Prescription
        Get
            Return _Prescription
        End Get
    End Property
    Public Property ShowFrequencyAbbrevationInRxMeds() As Boolean
        Get
            Return _gblnIncludeFrequencyAbbrevationInRxMeds

        End Get
        Set(ByVal Value As Boolean)
            _gblnIncludeFrequencyAbbrevationInRxMeds = Value
        End Set
    End Property

    Enum enmcontrolname
        Dosage
        Route
        Frequency
        Duration
        Amount
        StartDate
        EndDate
        Maysubstitute
        Refills
        Method
        Notes
        OK
        Close
        Sig
        LotNO
        ExpiryDate
        ReasonToOverride
        PrescriberNotes '20090716
    End Enum
    Private Sub CustomPrescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pnlCustomTask.Visible = False

        SetData(_RowIndex)

        txtDosage.Focus()

        Dim scheme As Cls_TabIndexSettings.TabScheme = Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing
        txtLotNo.Enabled = False
        dtpExpiryDate.Enabled = False

        txtDoseUnit.Visible = False
        txtDoseUnit.SendToBack()
        cmbDoseUnit.Visible = True
        cmbDoseUnit.BringToFront()


        ''code related to surescripts - before loading the control check if the status of the presectiption item in the prescription collection is "Approved".
        'if the status is "Approved" then make the Notes, Dispense(Refill Quantity), MaySubstitute ansd startdate as readonly.
        If _RXBusinessLayer.PrescriptionCol.Count > _RowIndex Then
            If _RXBusinessLayer.PrescriptionCol.Item(_RowIndex).Status = "Approved" Then
                txtDosage.ReadOnly = True
                txtDosage.BackColor = Color.White
                btnSig.Enabled = False

                'don't keep these readonly
                'txtroute.ReadOnly = True
                'txtFrequency.ReadOnly = True
                'txtDuration.ReadOnly = True
                'txtAmount.ReadOnly = True
                'dtpEndDate.Enabled = False
                'txtLotNo.ReadOnly = True
                'txtChiefComplaint.ReadOnly = True
                'btnclear.Enabled = False
                'btnclosecomplaints.Enabled = False
                'CmbMethod.Enabled = False
                'txtReasonToOverride.ReadOnly = True
                'dtpExpiryDate.Enabled = False
                'don't keep these readonly

                'these 4 controls are to be kept editable
                'txtNotes.ReadOnly = False
                'txtAmount.ReadOnly = False
                'chkMaysubstitute.Enabled = True
                'dtpStartDate.Enabled = True
            End If
        End If

        'For CCHIT2008
        If txtDuration.Text <> "" Then
            CalculateEndDate()
            dtpEndDate.Checked = True
        End If
        'For CCHIT2008


        ''Load the Drugs Form Combo box
        'Rxhub
        ''in the setdata() i.e called in constructor we if we set the text of drugform then we disable the cmbDrugsForm combo box, 
        ''therefore if the combo box is disabled do not fetch the drugs form values for cmbdrugsform.
        If cmbDrugsForm.Enabled = True Then
            Dim _listDrugsForm As List(Of String)

            Try
                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    _listDrugsForm = oDIBHelper.GetDrugFormList()
                End Using
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error in DIB service: " + ex.Message, False)
                _listDrugsForm = New List(Of String)
            End Try

            'clear the combo box
            cmbDrugsForm.Items.Clear()

            If Not IsNothing(_listDrugsForm) And _listDrugsForm.Count > 0 Then

                For _dtRowCount As Integer = 0 To _listDrugsForm.Count - 1
                    'add the items to the combo box
                    cmbDrugsForm.Items.Add(_listDrugsForm.Item(_dtRowCount))
                    'cmbDrugsForm.
                Next
            End If

        End If
        'Rxhub

        ''Load the DI override reason Combo box
        FillDIOverrideReason()
        If cmbReasonToOverride IsNot Nothing Then
            cmbReasonToOverride.Text = txtReasonToOverride.Text
        End If
        If pnlCustomTask.Visible = True Then
            pnlCustomTask.Visible = False
        End If
    End Sub

    Private Sub FillDIOverrideReason()
        Dim dtDIReason As DataTable = Nothing
        Try
            dtDIReason = _RXBusinessLayer.GetDIOverrideReason()
            If dtDIReason IsNot Nothing Then
                If dtDIReason.Rows.Count > 0 Then
                    Dim dr As DataRow = dtDIReason.NewRow()
                    dr("sCode") = "0"
                    dr("sDIReason") = ""
                    dtDIReason.Rows.InsertAt(dr, 0)
                    dtDIReason.AcceptChanges()

                    cmbReasonToOverride.DataSource = dtDIReason
                    cmbReasonToOverride.ValueMember = dtDIReason.Columns("sCode").ColumnName
                    '_SubscriberRelationShip = cmbRelationShip.Text;
                    'if (dtRelation.Rows.Count > 0)
                    '{
                    '    cmbGauInfoRelation.SelectedIndex = 0;
                    '}
                    cmbReasonToOverride.DisplayMember = dtDIReason.Columns("sDIReason").ColumnName
                    'dtReleationShip
                End If
            End If


        Catch ex As Exception
            If Not IsNothing(dtDIReason) Then
                dtDIReason.Dispose()
            End If
        Finally
            '''''dont dispose the dtDIReason datatable because we have give it as a datasource to the cmbDIOverrideReason combo box
        End Try
    End Sub

    Private Sub FillPotencyCode()
        Dim dtPotencyCode As DataTable = Nothing
        Try
            If cmbDoseUnit.Items.Count = 0 Then
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dtPotencyCode = helper.GetPotencyCode()
                End Using

                If dtPotencyCode IsNot Nothing Then
                    If dtPotencyCode.Rows.Count > 0 Then

                        Dim dr As DataRow = dtPotencyCode.NewRow()
                        dr("sPotencycode") = "0"
                        dr("sDescription") = ""
                        dtPotencyCode.Rows.InsertAt(dr, 0)
                        dtPotencyCode.AcceptChanges()

                        cmbDoseUnit.DataSource = dtPotencyCode
                        cmbDoseUnit.ValueMember = dtPotencyCode.Columns("sPotencycode").ColumnName
                        cmbDoseUnit.DisplayMember = dtPotencyCode.Columns("sDescription").ColumnName

                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(dtPotencyCode) Then
                dtPotencyCode.Dispose()
                dtPotencyCode = Nothing
            End If
        End Try
    End Sub

    Public Property Dosage() As String
        Get
            Try
                Return FormatZeros(txtDosage.Text)
            Catch ex As Exception
                Return txtDosage.Text
            End Try
        End Get

        Set(ByVal Value As String)
            txtDosage.Text = Value

        End Set
    End Property
    Public Property DrugID() As String
        Get
            Return m_DrugId

        End Get
        Set(ByVal Value As String)
            m_DrugId = Value
        End Set
    End Property
    Public Property Amount() As String
        Get
            Return txtAmount.Text
        End Get
        Set(ByVal Value As String)
            txtAmount.Text = Value
        End Set
    End Property
    ''GLO2011-0014767 : Quantity not being written out on prescriptions
    Public Property DoseUnit() As String
        Get
            Return cmbDoseUnit.Text ''txtDoseUnit.Text
        End Get
        Set(ByVal Value As String)
            cmbDoseUnit.Text = Value
        End Set
    End Property
    ''end

    'Public Property SAmount() As String
    '    Get
    '        Return txtSAmount.Text
    '    End Get
    '    Set(ByVal Value As String)
    '        txtSAmount.Text = Value
    '    End Set
    'End Property
    Public Property route() As String
        Get
            Return txtroute.Text
        End Get
        Set(ByVal Value As String)
            txtroute.Text = Value
        End Set
    End Property

    'Dim _routes As List(Of String)
    'Public Property routes() As List(Of String)
    '    Get
    '        Return _routes
    '    End Get
    '    Set(ByVal Value As List(Of String))
    '        _routes = Value
    '    End Set
    'End Property

    Public Property Frequency() As String
        Get
            Try
                Return FormatZeros(txtFrequency.Text)
            Catch ex As Exception
                Return txtFrequency.Text
            End Try

        End Get
        Set(ByVal Value As String)
            txtFrequency.Text = Value
        End Set
    End Property

    ''' <summary>
    ''' for CCHIT11
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DosageFrequencyValue() As String
        Get
            Return txtDosageFrequencyValue.Text
        End Get
        Set(ByVal Value As String)
            txtDosageFrequencyValue.Text = Value
        End Set
    End Property
    Public Property Duration() As String
        Get
            Try
                Return FormatZeros(txtDuration.Text)
            Catch ex As Exception
                Return txtDuration.Text
            End Try

        End Get
        Set(ByVal Value As String)
            txtDuration.Text = Value
        End Set
    End Property
    'CCHIT 08 to get the values from SIG Control
    Public Property cmbDurationDyWkMnth() As String
        Get

            Return cmbDuration.Text
        End Get
        Set(ByVal Value As String)
            cmbDuration.Text = Value
        End Set
    End Property
    'CCHIT 08
    Public Property ProviderID() As Int64
        Get
            Return m_ProviderID
        End Get
        Set(ByVal Value As Int64)
            m_ProviderID = Value
        End Set
    End Property
    Public Property PharmacyID() As Int64
        Get
            Return m_PharmacyID
        End Get
        Set(ByVal Value As Int64)
            m_PharmacyID = Value
        End Set
    End Property
    Public Property IsNarcotics() As Int64
        Get
            Return m_IsNarcotics
        End Get
        Set(ByVal Value As Int64)
            m_IsNarcotics = Value
        End Set
    End Property

    Public Property DosageForm() As String
        Get
            Return m_DosageForm
        End Get
        Set(ByVal Value As String)
            m_DosageForm = Value
        End Set
    End Property
    'Public Property SDuration() As String
    '    Get
    '        Return txtSDuration.Text
    '    End Get
    '    Set(ByVal Value As String)
    '        txtSDuration.Text = Value
    '    End Set
    'End Property
    Public Property ReasonToOverride() As String
        Get
            Return txtReasonToOverride.Text
        End Get
        Set(ByVal Value As String)
            txtReasonToOverride.Text = Value
        End Set
    End Property
    Public Property Notes() As String
        Get
            Return txtNotes.Text
        End Get
        Set(ByVal Value As String)
            txtNotes.Text = Value
        End Set
    End Property
    Public Property PrescriberNotes() As String
        Get
           Return txtPrescriberNotes.Text
        End Get
        Set(ByVal Value As String)
            txtPrescriberNotes.Text = Value
        End Set
    End Property
    Public Property Method() As String
        Get
            Return CmbMethod.Text
        End Get
        Set(ByVal Value As String)
            CmbMethod.Text = Value
        End Set
    End Property
    Public Property Refills() As String
        Get
            Try
                Return FormatZeros(txtRefills.Text)
            Catch ex As Exception
                Return txtRefills.Text
            End Try

        End Get
        Set(ByVal Value As String)
            txtRefills.Text = Value
        End Set
    End Property
    Public Property Maysubstitute() As Boolean
        Get
            Return chkMaysubstitute.Checked
        End Get
        Set(ByVal Value As Boolean)
            chkMaysubstitute.Checked = Value
        End Set
    End Property

    Public Property PRN() As Boolean
        Get
            Return chkPRN.Checked
        End Get
        Set(ByVal Value As Boolean)
            chkPRN.Checked = Value
        End Set
    End Property


    Public Property StartDate() As DateTime
        Get
            Return dtpStartDate.Value

        End Get
        Set(ByVal Value As DateTime)
            dtpStartDate.Value = Value
        End Set
    End Property
    Public Property CheckEndDate() As Boolean
        Get
            Return dtpEndDate.Checked

        End Get
        Set(ByVal Value As Boolean)
            dtpEndDate.Checked = Value
        End Set
    End Property
    Public Property EndDate() As DateTime
        Get
            Return dtpEndDate.Value
        End Get
        Set(ByVal Value As DateTime)
            dtpEndDate.Value = Value
        End Set
    End Property
    Public Property Caption() As String
        Get
            Return lblCaption.Text
        End Get
        Set(ByVal Value As String)
            lblCaption.Text = Value
        End Set
    End Property

    'RxHub
    Public Property MedicationLableNDCCode() As String
        Get
            Return lblMedicationNDCCode.Text
        End Get
        Set(ByVal Value As String)
            lblMedicationNDCCode.Text = Value
        End Set
    End Property

    Public Property PharmacyName() As String
        Get
            'Return txtPharmacy.Text
            Return cmbPharmacy.Text
        End Get
        Set(ByVal Value As String)
            'txtPharmacy.Text = Value
            cmbPharmacy.Text = Value
        End Set
    End Property
    'RxHub

    Public WriteOnly Property DisableEndDate() As Boolean
        Set(ByVal Value As Boolean)
            dtpEndDate.Enabled = Value
        End Set
    End Property
    '''' Code added by Ravikiran on 23/01/2007 as per CCHIT requirements

    Public Property LotNO() As String
        Get
            Return txtLotNo.Text
        End Get
        Set(ByVal Value As String)
            txtLotNo.Text = Value
        End Set
    End Property
    Public Property ExpiryDate() As DateTime
        Get
            Return dtpExpiryDate.Value
        End Get
        Set(ByVal Value As DateTime)
            dtpExpiryDate.Value = Value
        End Set
    End Property
    Public Property CheckExpiryDate() As Boolean
        Get
            Return dtpExpiryDate.Checked

        End Get
        Set(ByVal Value As Boolean)
            dtpExpiryDate.Checked = Value
        End Set
    End Property
    Public Property Renewed() As String
        Get
            Return m_renewed
        End Get
        Set(ByVal Value As String)
            m_renewed = Value
        End Set
    End Property
    Public Property MessageType() As String
        Get
            Return sMessagetype
        End Get
        Set(ByVal Value As String)
            sMessagetype = Value
        End Set
    End Property
    Private Sub CmbMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMethod.SelectedIndexChanged
        Try
            If CmbMethod.SelectedItem = "Sample" Then
                txtLotNo.Enabled = True
                dtpExpiryDate.Enabled = True
            Else
                txtLotNo.Text = ""
                ''Resolved Bug :75380
                'dtpEndDate.Checked = False
                txtLotNo.Enabled = False
                dtpExpiryDate.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    '''' Updation ends
    Public Sub HidePrescriptionControls(ByVal value As Boolean)
        GroupBox3.Enabled = False
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnOk.Click
        'Dim _prescription As Prescription
        Dim nSelectedRxColItem As Integer ''''this will help to set data against correct Rx item on flex grid, and not make tht item as duplicate
        Try

            If cmbRoute.Visible And cmbRoute.SelectedItem = "" Then
                MessageBox.Show("Route cannot be blank.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbRoute.Focus()
                Exit Sub
            End If

            If Me.StartDate.Date > Me.EndDate.Date And dtpEndDate.Checked Then ''bug 75491
                ''''''''''fixed bug 5296
                MessageBox.Show("Start date cannot be greater than end date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If DateDiff(DateInterval.Day, Me.StartDate.Date, Me.EndDate.Date) > 999 Then
                MessageBox.Show("Duration cannot exceed 999 days", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Val(txtAmount.Text.Trim) <= 0 Or txtAmount.Text.Trim = "" Then
                MessageBox.Show("Quantity cannot be zero or blank.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtAmount.Focus()
                Exit Sub
            End If

            If chkPRN.Checked = False Then
                If txtRefills.Text.Trim = "" Then
                    MessageBox.Show("Refill Quantity cannot be blank.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtRefills.Focus()
                    Exit Sub
                End If

            End If
            If cmbDoseUnit.Text = String.Empty Then
                MessageBox.Show("Select Unit Of Measure. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbDoseUnit.Focus()
                Exit Sub
            End If

            If cmbDoseUnit.Text.Trim.Contains("Unspecified") Then
                Dim _IsDrugForm As DialogResult = System.Windows.Forms.DialogResult.Yes
                _IsDrugForm = MessageBox.Show("Unit Of Measure is Unspecified. Do you want to continue? ", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If _IsDrugForm = DialogResult.No Then
                    cmbDoseUnit.Focus()
                    Exit Sub
                End If
            End If
            ' txtDoseUnit.Text = cmbDoseUnit.Text

            ' _prescription.VisitID = _RXBusinessLayer.CurrentVisitID
            If _RXBusinessLayer.PrescriptionCol.Count > 0 Then
                For i As Integer = 0 To _RXBusinessLayer.PrescriptionCol.Count - 1
                    ''''If _RXBusinessLayer.PrescriptionCol.Item(i).NDCCode = _RxGridNDCCode And _RXBusinessLayer.PrescriptionCol.Item(i).Status <> "D" Then '''''since the status of the item is D that means that row is deleted and therefore we will not modify this item in the collection
                    If _RXBusinessLayer.PrescriptionCol.Item(i).ItemNumber = _RowIndex Then
                        nSelectedRxColItem = i
                        '_prescription = _RXBusinessLayer.PrescriptionCol.Item(i)

                        If cmbRoute.Visible And cmbRoute.SelectedItem <> "" Then
                            Me.route = cmbRoute.SelectedItem
                        End If
                        ''''Assign the cmbDrugsForm combo box value to the prescription object
                        If cmbDrugsForm.Text <> "" Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).DosageForm = Me.DosageForm 'cmbDoseUnit.Text ''Me.DosageForm
                        End If

                        _RXBusinessLayer.PrescriptionCol.Item(i).Frequency = Me.Frequency.Trim ''''''''fixed bug 4671

                        If Me.Duration <> "" Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).Duration = Me.Duration.Trim & " " & cmbDuration.SelectedItem.ToString()

                        Else
                            _RXBusinessLayer.PrescriptionCol.Item(i).Duration = Me.Duration.Trim ''''''''fixed bug 4671
                        End If

                        If String.IsNullOrEmpty(_RXBusinessLayer.PrescriptionCol.Item(i).Dosage) Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).Medication = Me.Caption & " " & Me.Dosage.Trim
                        Else
                            _RXBusinessLayer.PrescriptionCol.Item(i).Medication = Me.Caption
                        End If

                        _RXBusinessLayer.PrescriptionCol.Item(i).Dosage = Me.Dosage.Trim ''''''''fixed bug 4671
                        _RXBusinessLayer.PrescriptionCol.Item(i).Refills = Me.Refills
                        _RXBusinessLayer.PrescriptionCol.Item(i).RefillQuantity = Me.Refills

                        _RXBusinessLayer.PrescriptionCol.Item(i).PriorAuthorizationNumber = Me.txtPriorAuthorization.Text

                        _RXBusinessLayer.PrescriptionCol.Item(i).Route = Me.route

                        'If chkMaysubstitute.Checked = True Then
                        '    _RXBusinessLayer.PrescriptionCol.Item(i).Maysubstitute = 0
                        'Else
                        '    _RXBusinessLayer.PrescriptionCol.Item(i).Maysubstitute = 1
                        'End If
                        _RXBusinessLayer.PrescriptionCol.Item(i).Maysubstitute = Me.Maysubstitute

                        _RXBusinessLayer.PrescriptionCol.Item(i).Method = Me.Method
                        If _RXBusinessLayer.PrescriptionCol.Item(i).Method = "eRx" Then
                            If _RXBusinessLayer.PrescriptionCol.Item(i).MessageType = "" Then
                                _RXBusinessLayer.PrescriptionCol.Item(i).MessageType = "NewRx"
                            End If
                            'Else
                            '    _RXBusinessLayer.PrescriptionCol.Item(i).MessageType = ""
                        End If
                        _RXBusinessLayer.PrescriptionCol.Item(i).Startdate = Me.StartDate
                        _RXBusinessLayer.PrescriptionCol.Item(i).Notes = Me.Notes 'this is for Pharmacy notes
                        _RXBusinessLayer.PrescriptionCol.Item(i).PrescriberNotes = Me.PrescriberNotes  'this is for Prescriber Notes
                        _RXBusinessLayer.PrescriptionCol.Item(i).ReasontoOverride = Me.cmbReasonToOverride.Text 'Me.ReasonToOverride

                        Dim Amt As Decimal
                        Amt = Val(txtAmount.Text)

                        _RXBusinessLayer.PrescriptionCol.Item(i).Amount = Amt & " " & cmbDoseUnit.Text ''''fixed bug 5453''''Me.Amount 
                        Dim dtpotencyCode As DataTable = Nothing
                        dtpotencyCode = RxBusinesslayer.GetDrugPotencyCode(cmbDoseUnit.Text) 'SLR: Shared function
                        If Not IsNothing(dtpotencyCode) Then
                            If dtpotencyCode.Rows.Count > 0 Then
                                _RXBusinessLayer.PrescriptionCol.Item(i).PotencyCode = dtpotencyCode.Rows(0)("sPotencycode")
                            End If
                            dtpotencyCode.Dispose()
                            dtpotencyCode = Nothing
                        End If


                        _RXBusinessLayer.PrescriptionCol.Item(i).DrugID = Me.DrugID
                        _RXBusinessLayer.PrescriptionCol.Item(i).PrescriptionID = Me.Tag
                        '_prescription.ChiefComplaint = Me.ChiefComplaint
                        Dim StrChiefComp As String = ""
                        For Len As Integer = 0 To lstChfcomp.Items.Count - 1
                            If StrChiefComp.Trim() <> "" Then
                                StrChiefComp = StrChiefComp & "|" & lstChfcomp.Items(Len)
                            Else
                                StrChiefComp = lstChfcomp.Items(Len).ToString().Trim()
                            End If

                        Next
                        Dim sProblems As String = ""
                        For Len As Integer = 0 To Problems.Count - 1
                            If sProblems.Trim() <> "" Then
                                sProblems = sProblems & "|" & Problems(Len)
                            Else
                                sProblems = Problems(Len).ToString().Trim()
                            End If

                        Next

                        _RXBusinessLayer.PrescriptionCol.Item(i).ChiefComplaint = StrChiefComp
                        _RXBusinessLayer.PrescriptionCol.Item(i).Problems = sProblems 'Me.ProblemIDs 'problemid set to update in problemlist table
                        _RXBusinessLayer.PrescriptionCol.Item(i).Renewed = Me.Renewed

                        'code added by supriya 19/7/2008
                        _RXBusinessLayer.PrescriptionCol.Item(i).IsNarcotics = Me.m_IsNarcotics

                        'Update the pharmacy ID and PhContactID
                        'if user has not assigned any pharmacy in the prescription control then assign the pharmacy name that is assigned at the time of registration
                        'COMMENTED BY SHUBHANGI 20110104
                        'If Me.txtPharmacy.Text.Trim = "" Then
                        '    _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyName = _RXBusinessLayer.PrescriptionCol.Item(_RowIndex - 1).PharmacyName.ToString
                        'Else
                        '    _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyName = Me.txtPharmacy.Text
                        'End If
                        'SHUBHANGI 20110104
                        If Me.cmbPharmacy.Text.Trim = "" Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyName = _RXBusinessLayer.PrescriptionCol.Item(nSelectedRxColItem).PharmacyName.ToString ''''fixed bug 7462
                        Else
                            'Update the pharmacy ID and PhContactID

                            _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyID = Me.cmbPharmacy.SelectedValue
                            _RXBusinessLayer.PrescriptionCol.Item(i).PhContactID = Me.cmbPharmacy.SelectedValue
                            _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyName = Me.cmbPharmacy.Text
                        End If



                        ''''''get the appropriate NCPDPID against teh contact id and save in prescription collection
                        If _RXBusinessLayer.PrescriptionCol.Item(i).PharmacyID <> 0 Then
                            Dim dtPharmacyDetails As DataTable = GetPharmacyNCPDPID(_RXBusinessLayer.PrescriptionCol.Item(i).PharmacyID)
                            If Not IsNothing(dtPharmacyDetails) Then
                                If dtPharmacyDetails.Rows.Count > 0 Then
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhNCPDPID = dtPharmacyDetails.Rows(0)("NCPDPID")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhAddressline1 = dtPharmacyDetails.Rows(0)("PhAddressline1")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhAddressline2 = dtPharmacyDetails.Rows(0)("PhAddressline2")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhCity = dtPharmacyDetails.Rows(0)("PhCity")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhState = dtPharmacyDetails.Rows(0)("PhState")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhZip = dtPharmacyDetails.Rows(0)("PhZip")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhPhone = dtPharmacyDetails.Rows(0)("PhPhone")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhFax = dtPharmacyDetails.Rows(0)("PhFax")
                                    _RXBusinessLayer.PrescriptionCol.Item(i).PhServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                                End If
                                dtPharmacyDetails.Dispose()
                                dtPharmacyDetails = Nothing
                            End If
                        Else
                            _RXBusinessLayer.PrescriptionCol.Item(i).PhNCPDPID = ""
                            _RXBusinessLayer.PrescriptionCol.Item(i).PhServiceLevel = "0"
                        End If




                        _RXBusinessLayer.PrescriptionCol.Item(i).ProviderID = Me.m_ProviderID

                        If chkPRN.Checked = True Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "PRN"
                            _RXBusinessLayer.PrescriptionCol.Item(i).RefillQuantity = ""
                        Else
                            If _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "P" Then
                                _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "P"
                            ElseIf _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "R" Then
                                _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "R"
                            Else
                                _RXBusinessLayer.PrescriptionCol.Item(i).RefillQualifier = "R"
                            End If
                        End If

                        _RXBusinessLayer.PrescriptionCol.Item(i).UserName = _RXBusinessLayer.GetCurrentUserName
                        If _RXBusinessLayer.PrescriptionCol.Item(i).Method = "Sample" Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).LotNo = Me.LotNO
                            If Me.CheckExpiryDate Then
                                _RXBusinessLayer.PrescriptionCol.Item(i).CheckExpiryDate = True
                                _RXBusinessLayer.PrescriptionCol.Item(i).ExpirationDate = Me.ExpiryDate
                            Else
                                _RXBusinessLayer.PrescriptionCol.Item(i).CheckExpiryDate = False
                            End If
                        Else
                            _RXBusinessLayer.PrescriptionCol.Item(i).LotNo = ""
                            _RXBusinessLayer.PrescriptionCol.Item(i).CheckExpiryDate = False
                        End If

                        '------------------
                        'Prescription.EndDate = objCustomPrescription.EndDate

                        'If Len(objCustomPrescription.GetCliptext(False)) = 8 Then 'check if enddate entered
                        'if checked then store enddate or store check status


                        If Me.CheckEndDate Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).CheckEndDate = True
                            _RXBusinessLayer.PrescriptionCol.Item(i).Enddate = Me.EndDate.Date
                        Else
                            _RXBusinessLayer.PrescriptionCol.Item(i).CheckEndDate = False
                            'Added By Shweta 20091127 
                            'To Set the default end date 
                            ' 'Against salesForce Case NO: GLO2009 0002946
                            _RXBusinessLayer.PrescriptionCol.Item(i).Enddate = "12:00:00 AM"
                            'End 20091127
                        End If
                        '-----------------
                        Dim unit As Integer = 0
                        Try
                            unit = txtDosage.Text.Substring(txtDosage.Text.IndexOf("/") + 1, txtDosage.Text.IndexOf("ML") - (txtDosage.Text.IndexOf("/") + 1))
                        Catch ex As Exception
                            unit = 1
                        End Try
                        If txtFrequency.Tag = "" Then
                            txtFrequency.Tag = 1
                        End If

                        Dim ndc As String = _RXBusinessLayer.PrescriptionCol.Item(i).NDCCode
                        Dim amount As Decimal = 0
                        Dim freq As String = Convert.ToString(txtFrequency.Tag)
                        Dim nFreq As Int16 = 1
                        Dim duration As Integer = 0
                        If freq <> 0 Then
                            nFreq = freq
                        End If

                        If txtAmount.Text.Trim <> "" Then
                            amount = Val(txtAmount.Text)
                        End If
                        If txtDuration.Text <> "" Then
                            duration = Convert.ToInt32(txtDuration.Text)
                        End If

                        _RXBusinessLayer.PrescriptionCol.Item(i).DosageFrequencyValue = Convert.ToInt32(txtFrequency.Tag.ToString().Trim()) ''''this val of freq will be passed to dosage calc constructor to calculate the dosage val
                        _RXBusinessLayer.PrescriptionCol.Item(i).DosageFrequencyText = txtFrequency.Text '''''assing the frequency text value to show on dosage calc

                        'added new property to save rowstate used in TVP(6040)
                        If _RXBusinessLayer.PrescriptionCol.Item(i).State <> "A" Then
                            _RXBusinessLayer.PrescriptionCol.Item(i).State = "M"
                        End If

                        '_RXBusinessLayer.SetInsuranceCol(_prescription, nSelectedRxColItem) ''_RowIndex - 1

                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnIsRxMedsFormEdited = True ''''''''means Rx-Meds form is edited and we should prompt the user if he directly Clicks the Close button
                        _RXBusinessLayer.PrescriptionCol.Item(i).CPOEOrder = chkCPOEOrder.Checked
                        _RXBusinessLayer.PrescriptionCol.Item(i).MedicationAdministered = ChkMedicationAdministered.Checked
                        If CType(gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrPatientAgeInYears, Integer) >= 14 Then
                            ndc = ""
                        End If
                        RaiseEvent OKClick(ndc, amount, nFreq, duration, cmbDuration.Text.Trim(), nSelectedRxColItem)

                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = True ''''for CCHIT11 audit log

                        Exit For '''''for optimization once the values from the control are assigned then exit from the loop
                    End If

                Next
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Try
            RaiseEvent CloseClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub btnSig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSig.Click
        Try
            If clsgeneral.blnIsProviderSpecificDrugsBtnSelected = True Then ''''''if provider specific drug btn selected then show the drug provider specific sig values when we click on the custom Rx sig btn
                RaiseEvent SigClick_DrugProvAsso(sender, e, lblCaption.Text)
            Else
                RaiseEvent SigClick(sender, e)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub chkMaysubstitute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMaysubstitute.CheckedChanged
        Try
            RaiseEvent CheckedClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Public Sub SetControlFocus(ByVal enm As enmcontrolname)
        Dim objSender As New Object
        Dim obje As New EventArgs
        Select Case enm
            Case enmcontrolname.Dosage
                txtDosage.Focus()
            Case enmcontrolname.Route
                txtroute.Focus()
            Case enmcontrolname.Frequency
                txtFrequency.Focus()
            Case enmcontrolname.Duration
                txtDuration.Focus()
            Case enmcontrolname.Amount
                txtAmount.Focus()
            Case enmcontrolname.Maysubstitute
                chkMaysubstitute.Checked = Not chkMaysubstitute.Checked
            Case enmcontrolname.Notes
                txtNotes.Focus()
            Case enmcontrolname.Refills
                txtRefills.Focus()
            Case enmcontrolname.Method
                CmbMethod.Focus()
            Case enmcontrolname.StartDate
                dtpStartDate.Focus()
            Case enmcontrolname.EndDate
                dtpEndDate.Focus()
            Case enmcontrolname.OK
                btnOK_Click(objSender, obje)
            Case enmcontrolname.Close
                btnClose_Click(objSender, obje)
            Case enmcontrolname.Sig
                btnSig_Click(objSender, obje)
            Case enmcontrolname.LotNO
                txtLotNo.Focus()
            Case enmcontrolname.ExpiryDate
                dtpExpiryDate.Focus()
            Case enmcontrolname.ReasonToOverride
                txtReasonToOverride.Focus()
            Case enmcontrolname.PrescriberNotes
                txtPrescriberNotes.Focus()
        End Select
        obje = Nothing
        objSender = Nothing
    End Sub


    Private Function SplitDrug(ByVal _strDuration As String) As Array
        Try
            Dim _result As String()
            _result = _strDuration.Split(" ")
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try

    End Function

    Private Sub SetData(ByVal _RxRowItemnumber As Integer) ''_RxRowItemnumber will help to determine which item number to modify in the collection, becaz we save the item number in the Rxgrid col
        Dim strCmbDuration As String = ""
        ''for CCHIT11
        Dim dtDosageFreq As DataTable = Nothing

        Try
            FillPotencyCode()

            Dim _prescription As Prescription = Nothing

            If _RXBusinessLayer.PrescriptionCol.Count > 0 Then
                For i As Integer = 0 To _RXBusinessLayer.PrescriptionCol.Count - 1
                    If _RXBusinessLayer.PrescriptionCol.Item(i).ItemNumber = _RxRowItemnumber Then
                        _prescription = _RXBusinessLayer.PrescriptionCol.Item(i) '''''fixed bug 7882
                        SelectedItemInCol = i
                        Exit For
                    End If
                Next
            End If
            If (IsNothing(_prescription)) Then
                Exit Sub
            End If


            'If _RXBusinessLayer.GetInsuranceCol(_prescription, _RowIndex - 1) Then
            Me.Caption = _prescription.Medication
            'RxHub
            Me.MedicationLableNDCCode = " NDC Code : " & _prescription.NDCCode
            'shubhangi
            'Me.txtPharmacy.Text = _prescription.PharmacyName
            Dim cnt As Int64 = 0
            Dim dtPharma As DataTable = GetPharmacyList(_nRxPatientid)
            Me.cmbPharmacy.DataSource = dtPharma
            If (IsNothing(dtPharma) = False) Then
                'Me.cmbPharmacy.Text = _prescription.PharmacyName
                For i As Int64 = 0 To dtPharma.Rows.Count - 1
                    If (_prescription.PharmacyName <> "") Then
                        If (dtPharma.Rows(i)("nContactID").ToString() = _prescription.PharmacyID) Then

                            cnt = cnt + 1
                        End If
                    Else
                        cnt = -1
                    End If
                Next
                If (cnt = 0) Then
                    Dim dtRow As DataRow
                    dtRow = dtPharma.NewRow()
                    dtRow.Item(0) = _prescription.PharmacyID
                    dtRow.Item(1) = _prescription.PharmacyName
                    dtPharma.Rows.Add(dtRow)
                End If
                cmbPharmacy.ValueMember = dtPharma.Columns("nContactID").ColumnName
                cmbPharmacy.DisplayMember = dtPharma.Columns("sName").ColumnName
            End If

            cmbPharmacy.SelectedValue = _prescription.PharmacyID

            Me.m_PharmacyID = _prescription.PharmacyID
            If Not String.IsNullOrEmpty(_prescription.PAReferenceID) Then
                Using ePAInsertUpdate As New EPABusinesslayer()
                    If Not ePAInsertUpdate.epa_IsManualPriorAuthorization(_nRxPatientid, _prescription.PAReferenceID) Then
                        Me.txtPriorAuthorization.ReadOnly = True
                    Else
                        Me.txtPriorAuthorization.ReadOnly = False
                    End If
                End Using
            Else
                Me.txtPriorAuthorization.ReadOnly = False
            End If

            Me.txtPriorAuthorization.Text = _prescription.PriorAuthorizationNumber

            '''''if  there is value in Dosageform then make the combo box simple and assign the Dosage form value to combo box and make the combo box disabled
            If Not IsNothing(_prescription.DosageForm) AndAlso _prescription.DosageForm <> "" Then
                cmbDrugsForm.DropDownStyle = ComboBoxStyle.Simple
                cmbDrugsForm.Text = _prescription.DosageForm
                cmbDrugsForm.Enabled = False
            Else
                cmbDrugsForm.DropDownStyle = ComboBoxStyle.DropDownList
            End If


            'cmbDrugsForm.Enabled = False
            'RxHub
            Me.StartDate = _prescription.Startdate
            Me.Dosage = _prescription.Dosage
            Me.Frequency = _prescription.Frequency

            dtDosageFreq = _RXBusinessLayer.getDosageFreqValue(_prescription.Frequency)

            If Not IsNothing(dtDosageFreq) Then
                If dtDosageFreq.Rows.Count > 0 Then
                    _prescription.DosageFrequencyValue = dtDosageFreq.Rows(0)("AbbrValue")
                End If
            End If
            txtFrequency.Tag = _prescription.DosageFrequencyValue.ToString
            Me.DosageFrequencyValue = _prescription.DosageFrequencyValue.ToString


            'split the duration part from the combo box
            Dim strDuration As String = ""
            If Not IsNothing(_prescription.Duration) Then
                Dim retval As String() = SplitDrug(_prescription.Duration)

                If Not IsNothing(retval) Then
                    If retval.Length > 1 Then
                        'Against salesForce Case No:GLO2009 0002946 For calculating end date while refill Rx
                        'Commented By Shweta 20091127
                        'strDuration = retval(0)
                        'End Commenting
                        'Added By Shweta 20091127
                        'If the duration is alpha numeric or has more than one word then to get whole string 
                        For i As Integer = 0 To retval.Length - 2
                            strDuration = strDuration + " " + retval(i)
                        Next
                        'strDuration = retval(retval.Length)
                        strDuration = strDuration.Remove(0, 1)
                        'End Shweta
                        strCmbDuration = retval(retval.Length - 1)
                    Else
                        strDuration = _prescription.Duration.Trim
                    End If

                Else
                    strDuration = _prescription.Duration
                End If
            Else
                strDuration = ""
            End If

            If strCmbDuration <> "" Then

                If strCmbDuration.ToUpper() = "DAYS" Then
                    cmbDuration.SelectedIndex = 0 '0th item is Days
                ElseIf strCmbDuration.ToUpper() = "WEEKS" Then
                    cmbDuration.SelectedIndex = 1 '1st item is Weeks
                Else
                    cmbDuration.SelectedIndex = 2 '2nd item is Months
                End If
            End If



            Me.Duration = strDuration ' _prescription.Duration
            '    Me.route = _prescription.Route

            'Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
            '    If Me.routes Is Nothing Then
            '        Me.routes = helper.GetDrugRoutes(_prescription.mpid)
            '    End If
            'End Using

            If _prescription.routes IsNot Nothing Then
                If (_prescription.routes.Count > 2) Then
                    cmbRoute.DataSource = _prescription.routes
                    If _prescription.Route <> "" Then
                        cmbRoute.SelectedIndex = cmbRoute.FindString(_prescription.Route)
                    End If
                    cmbRoute.Visible = True
                    txtroute.Visible = False
                    lblRouteAstr.Visible = True
                Else
                    Me.route = _prescription.Route
                    cmbRoute.Visible = False
                    txtroute.Visible = True
                    lblRouteAstr.Visible = False
                End If
            Else
                Me.route = _prescription.Route
                cmbRoute.Visible = False
                txtroute.Visible = True
                lblRouteAstr.Visible = False
            End If


            'this is for Pharmacy notes
            Me.Notes = _prescription.Notes
            'this is for Prescriber notes
            Me.PrescriberNotes = _prescription.PrescriberNotes


            Me.ReasonToOverride = _prescription.ReasontoOverride
            cmbReasonToOverride.Text = Me.ReasonToOverride

            If clsgeneral.gblnDisableAllowSubstitution.HasValue Then
                chkMaysubstitute.Enabled = False
            Else
                chkMaysubstitute.Enabled = True
            End If


            '10.6 changes substitution flag feedback changes
            If _prescription.Maysubstitute = True Then
                chkMaysubstitute.Checked = True
            Else
                chkMaysubstitute.Checked = False
            End If
            If _prescription.RefillQualifier = "PRN" Then
                Me.PRN = True
            Else
                Me.PRN = False
            End If

            Me.CheckEndDate = _prescription.CheckEndDate

            ' chetan added 31 Aug 2010
            Dim strChiefComp As String()
            strChiefComp = _prescription.ChiefComplaint.Split("|")
            lstChfcomp.Items.Clear()
            For Len As Integer = 0 To strChiefComp.Length - 1
                If strChiefComp(Len).Trim() <> "" Then
                    lstChfcomp.Items.Add(strChiefComp(Len))
                End If

            Next

            Dim strProblems As String()
            strProblems = _prescription.Problems.Split("|")
            Problems.Clear()
            For Len As Integer = 0 To strProblems.Length - 1
                If strProblems(Len).Trim() <> "" Then
                    Problems.Add(strProblems(Len))
                End If
            Next

            Me.Renewed = _prescription.Renewed

            ''GLO2011-0014767 : Quantity not being written out on prescriptions
            '' text value set to control property instead of textbox
            If _prescription.Amount <> "" Then 'fixed bug 5453
                Dim strDispense As String() = Split(_prescription.Amount, " ")
                If strDispense.Length > 1 Then
                    Me.Amount = strDispense(0)

                    Dim strbld As New System.Text.StringBuilder

                    For i As Integer = 1 To strDispense.Length - 1
                        strbld.Append(" ")
                        strbld.Append(strDispense(i).ToString)
                    Next

                    Me.DoseUnit = strbld.ToString.Trim
                    strbld = Nothing
                Else
                    Me.Amount = _prescription.Amount
                End If
            Else
                Me.Amount = _prescription.Amount
            End If



            If Not String.IsNullOrEmpty(_prescription.PotencyCode) Then
                cmbDoseUnit.SelectedValue = _prescription.PotencyCode
            End If

            If _prescription.AlternativeFormId = 23 Then '23 : ORAL LIQUID/ ML
                cmbDoseUnit.Enabled = False
            End If

            Me.DrugID = _prescription.DrugID
            Me.Tag = _prescription.PrescriptionID
            Me.LotNO = _prescription.LotNo
            Me.m_IsNarcotics = _prescription.IsNarcotics
            Me.m_DosageForm = _prescription.DosageForm
            Me.m_ProviderID = _prescription.ProviderID

            If _prescription.CheckExpiryDate = True Then
                If Not IsNothing(_prescription.ExpirationDate) Then
                    Me.ExpiryDate = _prescription.ExpirationDate
                    Me.CheckExpiryDate = _prescription.CheckExpiryDate
                End If
            End If
            If IsNothing(_RXBusinessLayer.PrescriptionCol.Item(SelectedItemInCol).PrescriptionID) Then
                If _prescription.Method = "" Then
                    Me.Method = CmbMethod.Items(1)
                Else
                    Me.Method = _prescription.Method
                End If

            ElseIf _RXBusinessLayer.PrescriptionCol.Item(SelectedItemInCol).PrescriptionID = 0 Then
                If _prescription.Method = "" Then
                    Me.Method = CmbMethod.Items(1)
                Else
                    Me.Method = _prescription.Method
                End If
            Else
                Me.Method = _prescription.Method
            End If



            If _prescription.CheckEndDate = True Then
                If Not IsNothing(Me.EndDate) Then
                    Me.EndDate = _prescription.Enddate
                    Me.CheckEndDate = _prescription.CheckEndDate
                    If _RXBusinessLayer.TransactionMode = RxBusinesslayer._TransactionMode.Edit Then
                        Me.DisableEndDate = False
                    End If
                End If
            End If
            'as discussed with phil in 8022
            'If _prescription.Status = "Approved" Then
            '    chkCPOEOrder.Enabled = False
            'Else
            '    chkCPOEOrder.Checked = _prescription.CPOEOrder
            'End If
            chkCPOEOrder.Checked = _prescription.CPOEOrder

            ChkMedicationAdministered.Checked = _prescription.MedicationAdministered

            MessageType = _prescription.Status

            Dim refillsvalue As String = Nothing
            refillsvalue = _prescription.RefillQuantity

            If refillsvalue = "" Then
                refillsvalue = _prescription.Refills
            End If

            If refillsvalue = "0" Then
                'If _prescription.Status <> "" Then
                '    '    Me.Refills = 1
                '    'Else
                '    Me.Refills = 0
                'End If
                Me.Refills = 0
            ElseIf refillsvalue = "" Then
                'If _prescription.Status <> "" Then
                '    '    Me.Refills = 1
                '    'Else
                '    Me.Refills = 0
                'End If
                Me.Refills = 0
            Else
                Me.Refills = refillsvalue
            End If
            If Me.PRN = True Then
                Me.Refills = ""
            End If
            '''''EpcsChange

            If _prescription.IsNarcotics = 2 Then
                txtRefills.Enabled = False
            End If
            If _prescription.IsNarcotics >= 2 Then
                chkPRN.Enabled = False
            End If

            If _prescription.MessageType = "RefillRequest" OrElse _prescription.MessageType = "RxChangeRequest" Then
                Me.btnSelectPharmacy.Enabled = False
                Me.cmbPharmacy.Enabled = False
            End If
            'EpcsChange....
            If Trim(txtDosage.Text) = "" Then
                txtDosage.Enabled = True
            Else
                txtDosage.Enabled = False
            End If
            If Trim(txtroute.Text) = "" And txtroute.Visible Then
                txtroute.Enabled = True
            Else
                txtroute.Enabled = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting prescription details"
            Throw objex
        Finally
            If Not IsNothing(dtDosageFreq) Then
                dtDosageFreq.Dispose()
            End If
        End Try
    End Sub
    'SHUBHANFGI 20110104
    Private Function GetPharmacyList(ByVal PatientID As Int64) As DataTable
        Dim _dtPharmacy As DataTable
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim _strSQL As String = ""
        Try
            _strSQL = "SELECT nContactID,ISNULL(sName,'') as sName FROM Patient_DTL where nPatientID = " & PatientID & " AND sName <> '' AND nContactFlag = 1 "
            _dtPharmacy = _gloEMRDatabase.GetDataTable_Query(_strSQL)
            Return _dtPharmacy
        Catch ex As Exception
            Return Nothing
        Finally
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try
    End Function

    ''METHOD TO ADD NEWLY ADDED PHARMACY INTO COMBO BOX 20110118
    Public Sub SetPharmacyData(ByVal dt As DataTable, ByVal PatientId As Int64)
        Dim cnt As Int64 = 0
        Dim dtcmbPharma As DataTable = Nothing
        Try
            Dim isPharmacyUpdated As Boolean = False
            dtcmbPharma = GetPharmacyList(PatientId)
            Dim UpdatedPharmacyRowIndex As Integer = -1
            If (IsNothing(dtcmbPharma) = False) Then


                For i As Int64 = 0 To dtcmbPharma.Rows.Count - 1
                    If (dt.Rows(0)("nContactID") <> "") Then
                        If (dtcmbPharma.Rows(i)("nContactID").ToString() = dt.Rows(0)("nContactID").ToString()) Then
                            If dtcmbPharma.Rows(i)("sName").ToString() <> dt.Rows(0)("sName").ToString() Then
                                isPharmacyUpdated = True
                                UpdatedPharmacyRowIndex = i
                            Else
                                cnt = cnt + 1
                            End If
                        End If
                    Else
                        cnt = -1
                    End If
                Next
                If (cnt = 0) Then
                    If isPharmacyUpdated = True Then
                        dtcmbPharma.Rows.RemoveAt(UpdatedPharmacyRowIndex)
                    End If
                    Dim dtRow As DataRow
                    dtRow = dtcmbPharma.NewRow()
                    dtRow.Item(0) = dt.Rows(0)("nContactID").ToString()
                    dtRow.Item(1) = dt.Rows(0)("sName").ToString()
                    dtcmbPharma.Rows.Add(dtRow)
                End If
            End If

            Me.cmbPharmacy.DataSource = Nothing
            Me.cmbPharmacy.Items.Clear()
            Me.cmbPharmacy.DataSource = dtcmbPharma
            If (IsNothing(dtcmbPharma) = False) Then '
                Me.cmbPharmacy.ValueMember = dtcmbPharma.Columns("nContactID").ColumnName
                Me.cmbPharmacy.DisplayMember = dtcmbPharma.Columns("sName").ColumnName
            End If

            Me.cmbPharmacy.Text = dt.Rows(0)("sName").ToString()
        Catch ex As Exception
            If Not IsNothing(dtcmbPharma) Then
                dtcmbPharma.Dispose()
                dtcmbPharma = Nothing
            End If
        Finally
            If Not IsNothing(dtcmbPharma) Then
                'dtcmbPharma.Dispose()
                'dtcmbPharma = Nothing
            End If
        End Try
    End Sub

    Private Function GetPharmacyNCPDPID(ByVal PhContactId As Long) As DataTable
        Dim _dtPharmacyDetails As DataTable = Nothing
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim _strSQL As String = ""

        Dim _sNCPDPID As String = ""
        Try
            _strSQL = "SELECT   ISNULL(sName,'') as PhName , ISNULL(sCity,'') as PhCity, ISNULL(sState,'') as PhState, ISNULL(sZIP,'') as PhZip,ISNULL(sPhone,'') as PhPhone, ISNULL(sAddressLine2,'') as PhAddressline2, ISNULL(sNCPDPID,'') as NCPDPID,ISNULL(sAddressLine1,'') as PhAddressLine1,ISNULL(sfax,'') as PhFax ,ISNULL(sServiceLevel,'') as sServiceLevel FROM         dbo.Contacts_MST where nContactId = " & PhContactId & " AND sContactType = 'Pharmacy' "
            _dtPharmacyDetails = _gloEMRDatabase.GetDataTable_Query(_strSQL)
            If (IsNothing(_dtPharmacyDetails) = False) Then


                If _dtPharmacyDetails.Rows.Count = 0 Then
                    _strSQL = " SELECT  ISNULL(p.sName,'') as PhName , ISNULL(p.sCity,'') as PhCity, ISNULL(p.sState,'') as PhState, " _
                               & "ISNULL(p.sZIP,'') as PhZip,ISNULL(p.sPhone,'') as PhPhone, ISNULL(p.sAddressLine2,'') as PhAddressline2, " _
                               & "ISNULL(p.sNCPDPID,'') as NCPDPID,ISNULL(p.sAddressLine1,'') as PhAddressLine1,ISNULL(p.sfax,'') as PhFax, ISNULL(p.sServiceLevel,'') as sServiceLevel FROM " _
                               & "dbo.Contacts_MST c  INNER JOIN  Patient_DTL as p ON p.sNCPDPID = c.sNCPDPID " _
                               & "WHERE     p.nPatientID = " & _nRxPatientid _
                               & "AND p.nContactId = " & PhContactId
                    _dtPharmacyDetails.Dispose()
                    _dtPharmacyDetails = _gloEMRDatabase.GetDataTable_Query(_strSQL)
                End If
                Return _dtPharmacyDetails
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return _dtPharmacyDetails
        Finally
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try
    End Function

    'END

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then







            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)

        Dim dtpControls As System.Windows.Forms.DateTimePicker() = {dtpStartDate, dtpEndDate, dtpExpiryDate}
        Dim cntControls As System.Windows.Forms.DateTimePicker() = {dtpStartDate, dtpEndDate, dtpExpiryDate}

        Try
            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            gloGlobal.cEventHelper.DisposeAllControls(cntControls)
        Catch

        End Try

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnclosecomplaints_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclosecomplaints.Click
        RaiseEvent CloseComplaintClick(sender, e)
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        'txtChiefComplaint.Text = ""
        lstChfcomp.Items.Clear()
        Problems.Clear()
        'If _RXBusinessLayer.PrescriptionCol.Count > _RowIndex - 1 Then
        '    _RXBusinessLayer.PrescriptionCol.Item(_RowIndex - 1).ChiefComplaint = ""
        'End If
    End Sub

    Private Sub txtRefills_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRefills.KeyPress
        Try

            If txtRefills.ReadOnly Then
                e.Handled = True
                Exit Sub
            End If

            Dim chkNumeric As String = txtRefills.Text.Trim()
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then
                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting prescription details"
            Throw objex
        End Try
    End Sub



    'Private Sub txtDuration_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress
    '    Try

    '        Dim chkNumeric As String = txtDuration.Text.Trim()
    '        If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
    '            e.Handled = False
    '        Else

    '            If Char.IsDigit(e.KeyChar) Then

    '            Else
    '                If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

    '                Else
    '                    MessageBox.Show("Enter valid Numeric or Decimal value", "Prescription", MessageBoxButtons.OK)
    '                    e.Handled = True
    '                    Exit Sub
    '                End If
    '            End If


    '        End If


    '    Catch ex As Exception
    '        Dim objex As New gloUserControlLibrary.gloUserControlExceptions
    '        objex.ErrorMessage = "Error setting prescription details"
    '        Throw objex
    '    End Try
    'End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            If txtAmount.ReadOnly Then
                e.Handled = True
                Exit Sub
            End If

            Dim chkNumeric As String = txtAmount.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub

            End If



            'If chkNumeric = "" Then

            '    Exit Sub
            'End If
            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                    Else
                        MessageBox.Show("Enter valid numeric or decimal value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If


            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            objex.ErrorMessage = "Error setting prescription details"
            Throw objex
        End Try
    End Sub
    Private Sub txtDuration_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuration.TextChanged

        CalculateEndDate()
    End Sub
    'Commented By Shweta 20091127
    'Private Sub CalculateEndDate()
    '    Dim strLastVal As String = "" ''''''fixed bug 4494
    '    Try
    '        If txtDuration.Text.Trim.Length > 0 Then
    '            If IsNumeric(txtDuration.Text.Trim) Then
    '                Select Case cmbDuration.Text
    '                    Case "Months"
    '                        strLastVal = txtDuration.Text.Trim
    '                        dtpEndDate.Value = DateAdd(DateInterval.Month, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
    '                    Case "Days"
    '                        strLastVal = txtDuration.Text.Trim
    '                        dtpEndDate.Value = DateAdd(DateInterval.Day, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
    '                    Case "Weeks"
    '                        strLastVal = txtDuration.Text.Trim
    '                        dtpEndDate.Value = DateAdd(DateInterval.WeekOfYear, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
    '                End Select
    '            End If
    '        End If


    '    Catch ex As Exception
    '        'txtDuration.Text = strLastVal.Remove(strLastVal.Length - 1) ''''''fixed bug 4494
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(txtDuration.Text & " is invalid duration value to calculate End date. Please enter valid duration.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        txtDuration.Text = "" ''''''''fixed bug 4742
    '    End Try
    'End Sub
    'End Commenting 20091127

    'Added BY Shweta 20091127
    'Against salesForce Case NO: GLO2009 0002946
    Private Sub CalculateEndDate()
        'Added By Shweta 20091127
        'To calculate the end date  
        Dim sDuration As String
        sDuration = txtDuration.Text
        Dim aDuration As Array
        aDuration = sDuration.Split(" ")
        'If the duration mention is alphanumeric then pass the only numeric part to calculate end date
        Try
            If txtDuration.Text.Trim.Length > 0 Then
                'Commented By Shweta 20091127
                'If IsNumeric(txtDuration.Text.Trim) Then
                'End Commenting
                If IsNumeric(aDuration(0)) Then
                    Select Case cmbDuration.Text
                        Case "Months"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.Month, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.Month, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                        Case "Days"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.Day, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.Day, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                        Case "Weeks"
                            'Changed By Shweta 20091127
                            dtpEndDate.Value = DateAdd(DateInterval.WeekOfYear, CType(aDuration(0), Double), dtpStartDate.Value.Date).AddDays(If(Convert.ToInt64(txtDuration.Text) = 0, 0, -1))
                            'End Shweta 
                            'dtpEndDate.Value = DateAdd(DateInterval.WeekOfYear, CType(txtDuration.Text.Trim, Double), dtpStartDate.Value.Date)
                    End Select
                Else
                    'Added By Shweta 20091127
                    dtpEndDate.ResetText()
                End If
            Else
                'Added By Shweta 20091127
                dtpEndDate.ResetText()
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(txtDuration.Text & " is invalid duration value to calculate end date. Please enter valid duration.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDuration.Text = "" ''''''''fixed bug 4742
        End Try
    End Sub
    'End Code Add 20091127
    Private Sub dtpStartDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpStartDate.ValueChanged
        CalculateEndDate()
    End Sub


    Private Sub cmbDuration_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDuration.SelectedIndexChanged
        CalculateEndDate()
    End Sub


    'C

    Private Sub ts_btnDoseCalculator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnDoseCalculator.Click
        Dim _dtPatVitals As DataTable = Nothing
        Try


            '1. pass the dosage value from the dosage text box.
            '2. pass the weight value from Vitals table against the last visit in the VITALS table.
            _dtPatVitals = _RXBusinessLayer.GetPatientVitals(_nRxPatientid)

            Dim _Patweight As String = ""
            Dim _PatweightUnit As String = ""



            If Not IsNothing(_dtPatVitals) Then
                If _dtPatVitals.Rows.Count > 0 Then
                    'we will take/pass the zeroth rows values because the zeroth row is having the latest vitals against that patient in the vitals table 
                    '1. first check whether the patients weight in lbs is present or not. 
                    'if present the assign the lbs weight value to the _Patweight var else check if patients weight in Kgs is present or not. 
                    'if present the assign the Kgs weight value to the _Patweight var  
                    'else if both the weights (lbs / kgs ) is not present then pass zero to the _Patweight var so that the doctor can enter this value on his own.
                    If _dtPatVitals.Rows(0)("WeightinKg") <> 0 Then 'patients weight in lbs is present
                        _Patweight = _dtPatVitals.Rows(0)("WeightinKg")
                        _PatweightUnit = "kg"
                    ElseIf _dtPatVitals.Rows(0)("Weightinlbs") <> 0 Then 'patients weight in Kgs is present
                        _Patweight = _dtPatVitals.Rows(0)("Weightinlbs")
                        _PatweightUnit = "lb"
                    Else 'patient weight is not present in the Vitals table
                        _Patweight = 0
                        _PatweightUnit = ""
                    End If
                Else
                    _Patweight = 0
                    _PatweightUnit = ""
                End If
            Else
                _Patweight = 0
                _PatweightUnit = ""
            End If

            Dim _strDosage As String = txtDosage.Text

            Dim _strDosageForm As String = Me.m_DosageForm

            Dim DosageFrequencyText As String = txtFrequency.Text
            Dim DosageFrequencyValue As Int32 = CType(txtDosageFrequencyValue.Text, Int32)
            Dim ofrmDosageCalculator As frmDosageCalculator
            ofrmDosageCalculator = New frmDosageCalculator(_Patweight, _PatweightUnit, _strDosage, _strDosageForm, DosageFrequencyText, DosageFrequencyValue, _nRxPatientid)

            ofrmDosageCalculator.ShowDialog()
            If ofrmDosageCalculator.Frequency <> "" Then
                txtFrequency.Text = ofrmDosageCalculator.Frequency
            End If
            ofrmDosageCalculator.Dispose()
            ofrmDosageCalculator = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(_dtPatVitals) = False) Then
                _dtPatVitals.Dispose()
                _dtPatVitals = Nothing
            End If
            '_dtPatVitals.Dispose()
        End Try
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSig.MouseHover, btnclosecomplaints.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSig.MouseLeave, btnclosecomplaints.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub


    Private Sub btnSelectPharmacy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectPharmacy.Click
        Try
            RaiseEvent PharmacyClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub cmbDrugsForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDrugsForm.SelectedIndexChanged
        Try
            If cmbDrugsForm.Items.Count > 0 Then
                Me.DosageForm = cmbDrugsForm.Text
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ts_btnAddtoFavourites_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnAddtoFavourites.Click
        Dim _dtProvSpecificDrugs As DataTable = Nothing
        Dim _gloEMRDatabase As New DataBaseLayer
        Dim strSQL As String = ""
        Dim _ConfigureDrug As Boolean = False
        Dim nSelectedItem As Integer = -1
        Try


            ''''code modified to resolve bug 10745
            If _RXBusinessLayer.PrescriptionCol.Count > 0 Then
                Dim _prescription As Prescription

                For i As Integer = 0 To _RXBusinessLayer.PrescriptionCol.Count - 1
                    If _RXBusinessLayer.PrescriptionCol.Item(i).ItemNumber = _RowIndex Then
                        nSelectedItem = _RXBusinessLayer.PrescriptionCol.Item(i).ItemNumber
                    End If
                Next
                If nSelectedItem = -1 Then
                    Exit Sub '''''is the nselecteditem is -1 then exit from the function
                End If
                _prescription = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem)

                ''''''' update the selected drug sig into the drug provider association table
                '''''1. before inserting/updating first check  for duplicacy
                Dim strDrugName As String = lblCaption.Text
                Dim strDosage As String = txtDosage.Text
                Dim strRoute As String = txtroute.Text
                Dim strFrequency As String = txtFrequency.Text
                Dim strDuration As String = txtDuration.Text
                Dim strNoOfRefills As String = txtRefills.Text
                Dim _sDispAmt As String = txtAmount.Text

                Dim providerId As Long = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).ProviderID
                Dim DrugId As Long = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).DrugID
                Dim _drugform As String = ""
                If Not IsNothing(_RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).DosageForm) Then
                    _drugform = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).DosageForm
                Else
                    _drugform = ""
                End If

                Dim _ndccode As String = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).NDCCode
                Dim _isnarcotics As Int64 = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).IsNarcotics
                Dim mpid As Int32 = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).mpid

                Dim _sDrugQtyQualifier As String = ""
                If Not IsNothing(_RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).StrengthUnit) Then
                    _sDrugQtyQualifier = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).StrengthUnit
                Else
                    _sDrugQtyQualifier = ""
                End If

                'Added BY Rahul Patel on 19-11-2010
                'Resolving Case no :GLO2010-0007221  i.e Provider Favorite not saving Dosing Info
                If Not IsNothing(_RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Refills) AndAlso _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Refills <> "" AndAlso _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Refills <> 0 Then
                    strNoOfRefills = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Refills
                Else
                    'strNoOfRefills = "0"
                    'Added BY Rahul Patel on 19-11-2010
                    'Resolving Case no :GLO2010-0007221  i.e Provider Favorite not saving Dosing Info
                    strNoOfRefills = txtRefills.Text.Trim()
                End If
                'Added BY Rahul Patel on 19-11-2010
                'Resolving Case no :GLO2010-0007221  i.e Provider Favorite not saving Dosing Info
                If Not IsNothing(_RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Amount) AndAlso _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Amount <> "" Then
                    _sDispAmt = _RXBusinessLayer.PrescriptionCol.Item(nSelectedItem).Amount
                    'Added BY Rahul Patel on 19-11-2010
                    'Resolving Case no :GLO2010-0007221  i.e Provider Favorite not saving Dosing Info
                Else
                    If Not String.IsNullOrWhiteSpace(txtAmount.Text) Then
                        _sDispAmt = txtAmount.Text.Trim() & " " & cmbDoseUnit.Text.Trim()
                    End If
                    'End of code Added BY Rahul Patel on 19-11-2010       
                End If




                '''''get all ProviderSpecificDrug and comapre with all 
                strSQL = "SELECT DrugProviderAssociation.nDrugID, UPPER(SUBSTRING(DrugProviderAssociation.sDrugName, 1, 1)) + LOWER(SUBSTRING(DrugProviderAssociation.sDrugName, 2, " _
                & " LEN(DrugProviderAssociation.sDrugName))) as  DrugName, ISNULL(DrugProviderAssociation.sDosage, ' ') as Dosage,   " _
                & " ISNULL(DrugProviderAssociation.sRoute, ' ') as sRoute, ISNULL(DrugProviderAssociation.sFrequency, ' ') as sFrequency, ISNULL(DrugProviderAssociation.sDuration, ' ') as sDuration ,  " _
                & "  ISNULL(DrugProviderAssociation.sRefills, '0') as sRefills,  ISNULL(DrugProviderAssociation.nIsNarcotics, 0) as nIsNarcotics,ISNULL(DrugProviderAssociation.sDrugForm, '') as sDrugForm ,isnull(nSIGID,0)  as SIGID  " _
                & " FROM DrugProviderAssociation WHERE    (DrugProviderAssociation.nProviderID = (select nproviderid from patient where npatientid = " & _nRxPatientid & " )) "

                _dtProvSpecificDrugs = _gloEMRDatabase.GetDataTable_Query(strSQL)

                '''''''''if provider is new provider then drug can be added to his favorites list
                If (IsNothing(_dtProvSpecificDrugs) = False) Then


                    If _dtProvSpecificDrugs.Rows.Count > 0 Then
                        For i As Integer = 0 To _dtProvSpecificDrugs.Rows.Count - 1
                            If strDrugName.ToUpper = _dtProvSpecificDrugs.Rows(i)("DrugName").ToString.ToUpper And strDosage.Trim.ToString = _dtProvSpecificDrugs.Rows(i)("Dosage").trim.ToString And strRoute.Trim.ToString.ToUpper = _dtProvSpecificDrugs.Rows(i)("sRoute").ToString.ToUpper And strFrequency.Trim.ToString = _dtProvSpecificDrugs.Rows(i)("sFrequency").trim.ToString And strDuration.Trim.ToString = _dtProvSpecificDrugs.Rows(i)("sDuration").trim.ToString And strNoOfRefills.Trim.ToString = _dtProvSpecificDrugs.Rows(i)("sRefills").trim.ToString Then
                                MessageBox.Show("Drug " & """" & strDrugName & """" & " is already configured against this provider.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                _ConfigureDrug = False
                                Exit Sub
                            Else
                                _ConfigureDrug = True
                            End If
                        Next
                    Else
                        _ConfigureDrug = True
                    End If
                Else
                    _ConfigureDrug = True
                End If

                If _ConfigureDrug = True Then '''''''insert the drug in drugProviderAssociation table against this provider
                    _ConfigureDrug = False

                    '''''''generate SIG id and then pass it to the function
                    '' Problem 00000199 
                    '' Description : Duplicate SigId Causes incorrect drug selection.
                    '' Reason for change : as a preventative action change the unique id generation logic only for SigID.
                    Dim _nSIGid As Long = clsgeneral.GetUniqueID() 'clsgeneral.GetPrefixTransactionID(_nRxPatientid) ''Generate unique SigID
                    If providerId <> 0 Then
                        Dim retval As Boolean
                        retval = _RXBusinessLayer.AddProvidersDrugs(providerId, DrugId, strDrugName, strDosage, strRoute, strFrequency, strDuration, _drugform, _ndccode, _isnarcotics, _sDrugQtyQualifier, strNoOfRefills, _nSIGid, _sDispAmt, mpid)
                        If retval = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, strDrugName + " added to Provider Favorites", _nRxPatientid, DrugId, providerId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            MessageBox.Show("Drug " & """" & strDrugName & """" & " added successfully against the provider.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else ''''if there is any error returned from the stored proc then give the foll msg
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, strDrugName + " not added to Provider Favorites", _nRxPatientid, DrugId, providerId, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                            MessageBox.Show("Problem in adding Drug " & """" & strDrugName & """" & " against the provider.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If


                End If
            End If ''Prescriptioncol.Count > 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(_dtProvSpecificDrugs) Then
                _dtProvSpecificDrugs.Dispose()
                _dtProvSpecificDrugs = Nothing
            End If
        Finally
            If Not IsNothing(_dtProvSpecificDrugs) Then
                _dtProvSpecificDrugs.Dispose()
                _dtProvSpecificDrugs = Nothing
            End If
            _gloEMRDatabase.Dispose()
            _gloEMRDatabase = Nothing
        End Try
    End Sub

    ''issue: 5493
    ''20091205 Added as per pravin sir discussion: Duration should be numeric value
    Private Sub txtDuration_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDuration.KeyPress
        Try

            If txtDuration.ReadOnly Then
                e.Handled = True
                Exit Sub
            End If

            Dim chkNumeric As String = txtDuration.Text.Trim()
            If e.KeyChar.ToString() = " " Then
                e.Handled = True
                Exit Sub
            End If

            If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
                e.Handled = False
            Else

                If Char.IsDigit(e.KeyChar) Then

                Else
                    If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    Else
                        MessageBox.Show("Enter valid numeric value", "gloEMR", MessageBoxButtons.OK)
                        e.Handled = True
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFrequency_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFrequency.GotFocus
        blnGetFreqvalonTab = True
    End Sub
    Private Sub txtFrequency_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFrequency.KeyDown
        'MessageBox.Show("Key Down " & e.KeyValue.ToString())
    End Sub
    Private Sub txtFrequency_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFrequency.LostFocus
        ''00000284 : Rx Meds Bug #77746 
        If Not IsNothing(dgCustomGrid) Then
            If Not (IsNothing(dgCustomGrid.C1Task) OrElse IsNothing(txtFrequency)) Then
                If Not (txtFrequency.Focused OrElse dgCustomGrid.C1Task.Focused) Then
                    If Not IsNothing(pnlCustomTask) Then
                        pnlCustomTask.Visible = False
                    End If
                End If
            End If
        End If
        If blnGetFreqvalonTab = True Then
            If pnlCustomTask.Visible = True Then
                'Try
                '    If dgCustomGrid.C1Task.Rows.Count > 1 Then ''''issue resolved for case 10469
                '        '''''we want the frequency as abbrevation seperation as per Drew comment...
                '        Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
                '        Dim Freqvalue As String = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString() ''''issue resolved for case 10469
                '        If abbrv <> "" Then
                '            txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                '        Else
                '            txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                '        End If
                '        txtFrequency.Tag = Freqvalue ''''issue resolved for case 10469
                '        pnlCustomTask.Visible = False
                '    Else
                '        pnlCustomTask.Visible = False
                '    End If


                'Catch ex As Exception
                '    pnlCustomTask.Visible = False ''''issue resolved for case 10469
                'End Try

                pnlCustomTask.Visible = False
            End If

            blnGetFreqvalonTab = False
        End If

    End Sub

    Private Sub txtFrequency_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFrequency.TextChanged
        '' Chetan Added for freq Abbravation on 14 Oct 2010
        pnlCustomTask.Visible = True
        If blnCheckDb = True Then
            LoadUserGrid()
        End If
    End Sub
    '' Chetan Added for freq Abbravation on 14 Oct 2010
    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Panel1.Visible = False
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                pnlCustomTask.Width = 350
                dgCustomGrid.Width = pnlCustomTask.Width
                'reduced the height for case GLO2011-0013418
                pnlCustomTask.Height = 130
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlCustomTask.Height
                dgCustomGrid.txtsearch.Visible = False
                dgCustomGrid.Label1.Visible = False

                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False

                BindUserGrid()
                dgCustomGrid.Panel2.Visible = False
                dgCustomGrid.tsbtn_SelectAll.Visible = False
                'dgCustomGrid.tsbtn_OK.Visible = False
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Cardiac Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '' chetan added for FreqAbbreviation data binding on 14 Oct 2010
    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable
            dt = FillFreqAbbreviation()
            CustomDrugsGridStyle()
            'Dim col As New DataColumn
            'col.ColumnName = "Select"
            'col.DataType = System.Type.GetType("System.Boolean")

            'col.DefaultValue = CBool("False")
            'dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            '  dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = False
            dgCustomGrid.C1Task.Cols(0).AllowEditing = False
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.35
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.35
            dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0

            dgCustomGrid.C1Task.ScrollBars = ScrollBars.Both

        Catch ex As SqlClient.SqlException
            ' MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    '' chetan added for FreqAbbreviation on 14 Oct 2010
    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = 3

            Dim Col_Abbre As Integer = 0
            Dim Col_Mean As Integer = 1
            Dim Col_Val As Integer = 2

            .AllowEditing = True

            .SetData(0, Col_Abbre, "Abb")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Abbre).Width = _TotalWidth * 0.35
            .Cols(Col_Abbre).AllowEditing = False


            .SetData(0, Col_Mean, "Meaning")
            .Cols(Col_Mean).Width = _TotalWidth * 0.35
            .Cols(Col_Mean).AllowEditing = False


            .SetData(0, Col_Val, "Value")
            .Cols(Col_Val).Width = _TotalWidth * 0
            .Cols(Col_Val).AllowEditing = False



            ' .Cols(Col_DrugName).AllowEditing = False

        End With

    End Sub
    '' chetan added for FreqAbbreviation on 14 Oct 2010
    Public Function FillFreqAbbreviation() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""
        Dim dtabbr As DataTable = Nothing
        Try
            'Dim strsearch As String = txtFrequency.Text.Trim()
            'Dim strtosearch As String = ""
            'If strsearch.Length > 0 Then
            '    For Len As Integer = 0 To strsearch.Length - 1
            '        If Len = strsearch.Length - 1 Then
            '            strtosearch &= "'%" & strsearch(Len) & "%' "
            '        Else
            '            strtosearch &= "'%" & strsearch(Len) & "%' OR "

            '        End If
            '      Next


            'End If
            Dim strfreq As String = txtFrequency.Text.Trim.Replace("'", "") ''''bug fix 6891

            ''''' solving sales force case for 6031 -  sales force case - GLO2011-0012504   adding order by condition

            _strSQL = "Select ISNULL(Abbrevation,'')as Abb,ISNULL(Meaning,'') as Meaning,ISNULL(Value,'1') as Value From Frequency_Abbreviation Where  Abbrevation Like '%" + strfreq + "%'  OR Meaning Like '%" + strfreq + "%' order by Abbrevation asc"
            ' _strSQL = "Select ISNULL(Abbrevation,'')as Abb,ISNULL(Meaning,'') as Meaning from Frequency_Abbreviation Where  Abbrevation Like " + strtosearch + "  OR Meaning Like " + strtosearch + ""

            dtabbr = oDB.GetDataTable_Query(_strSQL)
            If Not dtabbr Is Nothing Then
                Return dtabbr
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function



    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        pnlCustomTask.Controls.Add(dgCustomGrid)
        pnlCustomTask.BringToFront()

        ''''''''''''''''''''''
        ''dgCustomGrid.lblCaption.Visible = True
        ''''''''''''''''''''''

        Dim y As Int64
        Dim x As Int64
        y = 220
        x = 200
        'If strLst = "cpt" Then
        '    y = 220
        '    x = 200
        '    'dgCustomGrid.lblCaption.Text = " CPT List "
        'ElseIf strLst = "testtype" Then
        '    y = 220
        '    x = 520
        '    'dgCustomGrid.lblCaption.Text = " Test Type List "
        'ElseIf strLst = "intervention" Then
        '    y = 220
        '    x = 550
        '    'dgCustomGrid.lblCaption.Text = " Intervention Type List "
        'ElseIf strLst = "physician" Then
        '    y = 220
        '    x = 560
        '    'dgCustomGrid.lblCaption.Text = " Physician List "
        'End If

        'pnlCustomTask.Location = New Point(txtFrequency.Margin.Right + txtFrequency.Width, txtFrequency.Margin.Top)
        pnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()

    End Sub
    'Remove customGrid control to form 
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlCustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        pnlCustomTask.Visible = False
    End Sub

    ''on grid enter add the frequency selected item to the txt frequency text box
    Private Sub dgCustomGrid_Gridkeypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgCustomGrid.Gridkeypress
        Dim key As Integer = Asc(e.KeyChar)
        blnGetFreqvalonTab = True
        If key = 13 And pnlCustomTask.Visible = True Then
            Try
                If dgCustomGrid.C1Task.RowSel > 0 Then
                    txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

                    '''''we want the frequency as abbrevation seperation as per Drew comment...
                    Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
                    If abbrv <> "" Then
                        If ShowFrequencyAbbrevationInRxMeds = True Then
                            txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                        Else
                            txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                        End If
                    Else
                        txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    End If

                    pnlCustomTask.Visible = False
                End If

            Catch ex As Exception
            End Try
        End If
        pnlCustomTask.Visible = False
    End Sub


    Private Sub dgCustomGrid_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.Leave
        blnGetFreqvalonTab = True
        pnlCustomTask.Visible = False
    End Sub

    Private Sub dgCustomGrid_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.LostFocus
        blnGetFreqvalonTab = True
        pnlCustomTask.Visible = False
    End Sub



    Private Sub dgCustomGrid_MouseDubClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgCustomGrid.MouseDubClick
        Try
            blnCheckDb = False
            '''''we want the frequency as abbrevation seperation as per Drew comment...
            Dim abbrv As String = ""
            If dgCustomGrid.C1Task.Rows.Count > 1 Then
                abbrv = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
            End If

            If abbrv <> "" Then
                If ShowFrequencyAbbrevationInRxMeds = True Then
                    txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
            Else
                txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
            End If
            txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

            blnCheckDb = True
            pnlCustomTask.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgCustomGrid_MouseMoveClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgCustomGrid.MouseMoveClick
        blnGetFreqvalonTab = False
    End Sub



    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        If dgCustomGrid.C1Task.Rows.Count > 1 Then
            txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()
            blnCheckDb = False

            '''''we want the frequency as abbrevation seperation as per Drew comment...
            Dim abbrv As String = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
            If abbrv <> "" Then
                If ShowFrequencyAbbrevationInRxMeds = True Then
                    txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
            Else
                txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
            End If

            blnCheckDb = True
            pnlCustomTask.Visible = False
        End If

    End Sub

    Private Sub txtFrequency_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFrequency.KeyPress
        Dim key As Integer = Asc(e.KeyChar)
        blnGetFreqvalonTab = True
        If key = 13 And pnlCustomTask.Visible = True Then
            Try
                'blnCheckDb = False

                Dim abbrv As String = ""
                If dgCustomGrid.C1Task.Rows.Count > 1 Then
                    abbrv = dgCustomGrid.C1Task.Cols(0)(dgCustomGrid.C1Task.RowSel).ToString() ''''the 0th col contains abbrevation value
                End If

                If abbrv <> "" Then
                    If ShowFrequencyAbbrevationInRxMeds = True Then
                        txtFrequency.Text = abbrv & " - " & dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    Else
                        txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                    End If
                Else
                    txtFrequency.Text = dgCustomGrid.C1Task.Cols(1)(dgCustomGrid.C1Task.RowSel).ToString()
                End If
                txtFrequency.Tag = dgCustomGrid.C1Task.Cols(2)(dgCustomGrid.C1Task.RowSel).ToString()

                'blnCheckDb = True
                'pnlCustomTask.Visible = False
            Catch ex As Exception

            End Try
        End If
        pnlCustomTask.Visible = False
    End Sub


    Private Sub btnclrsing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclrsing.Click
        Try
            Dim selectedIndex As Int16 = lstChfcomp.SelectedIndex
            lstChfcomp.Items.RemoveAt(selectedIndex)
            Problems.RemoveAt(selectedIndex)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstChfcomp_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstChfcomp.MouseMove
        SetListBoxToolTip(lstChfcomp, C1SuperTooltip1, Control.MousePosition)
    End Sub
    Private Sub chkPRN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPRN.CheckedChanged
        If chkPRN.Checked Then
            txtRefills.Enabled = False
            txtRefills.Text = "" ''Reverted back for  as per requirement mentioned in \\glosvr05\gloDocuments\gloSuite 7031\WEEK 0 - REQ - DRAFT_PRD\gloEMR\10 6 testing bugs\22/08/2013 sheet point no 6
        Else
            txtRefills.Enabled = True
            If MessageType <> "" Then
                txtRefills.Text = _RXBusinessLayer.PrescriptionCol.Item(SelectedItemInCol).Refills
            Else
                txtRefills.Text = "0"
            End If

        End If
    End Sub

    Private Sub cmbPharmacy_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPharmacy.SelectionChangeCommitted
        Try
            Me.m_PharmacyID = cmbPharmacy.SelectedValue
        Catch ex As Exception
        End Try
    End Sub

    Public Sub RemoveAbbrevationCntrl()
        If dgCustomGrid.C1Task.Rows.Count <= 1 Then
            pnlCustomTask.Visible = False
        End If
    End Sub

    Dim strnum As String() = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "."}
    Dim strsplchar As String() = {"@", "#", "&", "~", "^", "$", "*"}

    Public Function chkno(ByVal strno As String) As String

        While strno.LastIndexOf("0") > 0
            If strno.Length - 1 = strno.LastIndexOf("0") AndAlso strno.Contains(".") Then
                strno = strno.Substring(0, strno.Length - 1)
            Else
                Exit While
            End If

        End While
        Return strno
    End Function

    Private Function FormatZeros(ByVal strdatachk As String) As String
        Dim strdata As String = ""

        If String.IsNullOrEmpty(strdatachk) Then
            Return ""
        End If

        Dim splstrno As String() = replacemultiplecomma(strdatachk).Split(" ")


        For Len As Integer = 0 To splstrno.Length - 1
            Try
                Dim strnoo = ""
                For lennum As Integer = 0 To splstrno(Len).Length - 1
                    If (strnum.Contains(splstrno(Len)(lennum))) Then
                        strnoo = strnoo & splstrno(Len)(lennum)
                    Else
                        Dim num As String = ""
                        If (strnoo.Length > 0) Then
                            Try
                                ' If (lennum + 1 < splstrno(Len).Length - 1) Then
                                Dim no As Integer = lennum + 1
                                If (splstrno(Len)(lennum) = "/") OrElse (splstrno(Len)(lennum) = ":") Then
                                    num = chkno(strnoo)
                                    strnoo = ""
                                    strdata = strdata & num & "" & splstrno(Len)(lennum)
                                Else
                                    num = Convert.ToDecimal(chkno(strnoo))
                                    strnoo = ""
                                    strdata = strdata & num & "" & splstrno(Len)(lennum)
                                End If
                                ' Else
                                'num = Convert.ToDecimal(chkno(strnoo))
                                ' strnoo = ""
                                'strdata = strdata & num & "" & splstrno(Len)(lennum)
                                ' End If

                            Catch ex As Exception
                                num = chkno(strnoo)
                                strnoo = ""
                                strdata = strdata & num & "" & splstrno(Len)(lennum)
                            End Try

                        Else
                            strdata = strdata & splstrno(Len)(lennum) & ""
                        End If


                        'num = ""
                    End If
                Next
                If strnoo.Trim() <> "" Then
                    Try
                        strdata = strdata & Convert.ToDecimal(chkno(strnoo)) & " "
                    Catch ex As Exception
                        strdata = strdata & chkno(strnoo) & " "
                    End Try

                Else
                    strdata = strdata & " "
                End If

            Catch ex As Exception

            End Try
        Next
        Return strdata.Trim()
    End Function

    Private Function replacemultiplecomma(ByVal strdot As String) As String

        While (strdot.Contains(".."))
            strdot = strdot.Replace("..", ".")
        End While

        Dim repspl As String = ""
        Dim blno As Boolean = False
        For Len As Integer = 0 To strdot.Length - 1
            If (strnum.Contains(strdot(Len))) Then
                repspl = repspl & strdot(Len)
                blno = True
            Else
                If (blno = True) Then
                    If (strsplchar.Contains(strdot(Len))) Then

                    Else
                        repspl = repspl & strdot(Len)
                        blno = False
                    End If
                Else

                    repspl = repspl & strdot(Len)
                    blno = False

                End If
            End If


        Next
        Return repspl
    End Function

End Class
