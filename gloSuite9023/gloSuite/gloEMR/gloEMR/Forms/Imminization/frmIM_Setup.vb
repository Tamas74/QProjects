Public Class frmIM_Setup
    Inherits frmBaseForm
    Public _EditID As Long

    Public _SaveFlag As Boolean
    Public _CPTCode As String
    Public _ReturnID As Long

    Public IM_StrSnoMedID As String = ""
    Public IM_strConceptID As String = ""
    Public IM_strDescriptionID As String = ""
    ''Added by Mayuri:20110322
    Public IM_strDefination As String = ""
    Public IM_strNDCCode As String = ""
    Public IM_strRxNormCode As String = ""
    Public IM_strSnomedDescription As String = ""
    Dim _labellercode1 As String = ""
    Dim _productcode1 As String = ""
    Dim _packageCode1 As String = ""
    Dim _NDC As String = ""
    ''If barcode is invalid then it should not go into searchfired event on formload
    Dim _isFormLoadModify As Boolean = False
    Dim _isLoad As Boolean = False
    Dim _isLoadGridCvxControl As Boolean = False
    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    'Dim _isLoadGridMvxControl As Boolean = False
    'Dim _isLoadGridTradeNameControl As Boolean = False
    ''End 20110322
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsImmunization As System.Windows.Forms.ToolStrip
    Friend WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents cmbCPT As System.Windows.Forms.ComboBox
    Friend WithEvents btnBrowseCPT As System.Windows.Forms.Button
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button

    Public MyOwner As Form

    Private oListControl As gloListControl.gloListControl
    Private oDiagnosisListControl As gloListControl.gloListControl

    Private _databaseconnectionstring As String
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblConceptID As System.Windows.Forms.Label
    Friend WithEvents btnDiagnosis As System.Windows.Forms.Button
    Friend WithEvents btnDiaCancel As System.Windows.Forms.Button
    Friend WithEvents cmbDiagnosis As System.Windows.Forms.ComboBox
    Friend WithEvents txtNDCCode As System.Windows.Forms.TextBox
    Friend WithEvents dtVISDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtVIS As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblNDC As System.Windows.Forms.Label
    Protected Friend WithEvents lblVIS As System.Windows.Forms.Label
    Protected Friend WithEvents lblVISDate As System.Windows.Forms.Label
    Protected Friend WithEvents lblFundingSource As System.Windows.Forms.Label
    Protected Friend WithEvents lblDiagnosis As System.Windows.Forms.Label
    Friend WithEvents txtDosespervial As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblDosesPerVial As System.Windows.Forms.Label
    Friend WithEvents txtVials As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblVials As System.Windows.Forms.Label
    Friend WithEvents dtDateReceived As System.Windows.Forms.DateTimePicker
    Protected Friend WithEvents lblDateReceived As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Protected Friend WithEvents lblStatus As System.Windows.Forms.Label
    Protected Friend WithEvents lblLotNumber As System.Windows.Forms.Label
    Friend WithEvents dtExpiryDate As System.Windows.Forms.DateTimePicker
    Protected Friend WithEvents lblexpirydate As System.Windows.Forms.Label
    Friend WithEvents lblTradeName As System.Windows.Forms.Label
    Friend WithEvents lblmanufacturer As System.Windows.Forms.Label
    Friend WithEvents lblVaccineType As System.Windows.Forms.Label
    Friend WithEvents txtSKU As System.Windows.Forms.TextBox
    Friend WithEvents lblSKU As System.Windows.Forms.Label
    Friend WithEvents lblSnoMedID As System.Windows.Forms.Label
    Friend WithEvents lblDescriptionID As System.Windows.Forms.Label
    Friend WithEvents btnVaccineType As System.Windows.Forms.Button
    Friend WithEvents btnClearVaccineType As System.Windows.Forms.Button
    Friend WithEvents txtVaccineCode As System.Windows.Forms.TextBox
    Friend WithEvents lblVaccineCode As System.Windows.Forms.Label
    Protected Friend WithEvents lblDosesOnHand As System.Windows.Forms.Label
    Friend WithEvents txtDosesOnHand As System.Windows.Forms.TextBox
    Dim ofrmList = New frmViewListControl
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbFundingSource As System.Windows.Forms.ComboBox
    Dim ofrmDiagnosisList = New frmViewListControl
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSKUSearch As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTradeName As System.Windows.Forms.TextBox
    Friend WithEvents txtMvx As System.Windows.Forms.TextBox
    Friend WithEvents txtCvx As System.Windows.Forms.TextBox
    Friend WithEvents pnlCvxControl As System.Windows.Forms.Panel
    Friend WithEvents pnlMvxControl As System.Windows.Forms.Panel
    Friend WithEvents pnlTradeNameControl As System.Windows.Forms.Panel
    'end dipak variable
    Dim dsVaccineInformation As DataSet
    ''Added by Mayuri:20120117-To implement shortcuts for Cvx ,MVX,TradeName
    Private oCVXControl As gloUserControlLibrary.gloUC_GridList
    Public Event GridListLoaded()
    Public Event GridListClosed()

    Private oMVXControl As gloUserControlLibrary.gloUC_GridList
    Friend WithEvents btnMvx As System.Windows.Forms.Button
    Friend WithEvents btnCvx As System.Windows.Forms.Button
    Friend WithEvents btnTradeName As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnScanVIS As System.Windows.Forms.Button

    Public Event GridListLoaded1()
    Public Event GridListClosed1()


    Private oTradeNameControl As gloUserControlLibrary.gloUC_GridList
    Private WithEvents btn_tls_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnAddTradeNameCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddManufacturerCategory As System.Windows.Forms.Button
    Friend WithEvents BtnAddVaccineCategory As System.Windows.Forms.Button
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox

    Public Event GridListLoaded2()
    Public Event GridListClosed2()
    Dim _DocumentID As Long
    Dim _DefaultLocationID As Long
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Dim _DefaultLocation As String
    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer



#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _databaseconnectionstring = GetConnectionString()
        If appSettings("DefaultLocationID") IsNot Nothing Then
            If appSettings("DefaultLocationID").ToString() <> "" Then
                _DefaultLocationID = Convert.ToInt64(appSettings("DefaultLocationID"))
                _DefaultLocation = Convert.ToString(appSettings("DefaultLocation"))

            End If
        End If
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
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIM_Setup))
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.txtCvx = New System.Windows.Forms.TextBox()
        Me.lblVaccineType = New System.Windows.Forms.Label()
        Me.btnScanVIS = New System.Windows.Forms.Button()
        Me.btnCvx = New System.Windows.Forms.Button()
        Me.BtnAddVaccineCategory = New System.Windows.Forms.Button()
        Me.BtnAddManufacturerCategory = New System.Windows.Forms.Button()
        Me.BtnAddTradeNameCategory = New System.Windows.Forms.Button()
        Me.btnTradeName = New System.Windows.Forms.Button()
        Me.btnMvx = New System.Windows.Forms.Button()
        Me.txtTradeName = New System.Windows.Forms.TextBox()
        Me.txtMvx = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSKUSearch = New gloUserControlLibrary.gloSearchTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbFundingSource = New System.Windows.Forms.ComboBox()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.txtDosesOnHand = New System.Windows.Forms.TextBox()
        Me.lblDosesOnHand = New System.Windows.Forms.Label()
        Me.lblSnoMedID = New System.Windows.Forms.Label()
        Me.lblDescriptionID = New System.Windows.Forms.Label()
        Me.btnVaccineType = New System.Windows.Forms.Button()
        Me.btnDiagnosis = New System.Windows.Forms.Button()
        Me.btnClearVaccineType = New System.Windows.Forms.Button()
        Me.btnDiaCancel = New System.Windows.Forms.Button()
        Me.cmbDiagnosis = New System.Windows.Forms.ComboBox()
        Me.txtNDCCode = New System.Windows.Forms.TextBox()
        Me.dtVISDate = New System.Windows.Forms.DateTimePicker()
        Me.txtVIS = New System.Windows.Forms.TextBox()
        Me.lblNDC = New System.Windows.Forms.Label()
        Me.lblVIS = New System.Windows.Forms.Label()
        Me.lblVISDate = New System.Windows.Forms.Label()
        Me.lblFundingSource = New System.Windows.Forms.Label()
        Me.lblDiagnosis = New System.Windows.Forms.Label()
        Me.txtDosespervial = New System.Windows.Forms.TextBox()
        Me.lblDosesPerVial = New System.Windows.Forms.Label()
        Me.txtVials = New System.Windows.Forms.TextBox()
        Me.lblVials = New System.Windows.Forms.Label()
        Me.dtDateReceived = New System.Windows.Forms.DateTimePicker()
        Me.lblDateReceived = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.lblComments = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblLotNumber = New System.Windows.Forms.Label()
        Me.dtExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.lblexpirydate = New System.Windows.Forms.Label()
        Me.lblTradeName = New System.Windows.Forms.Label()
        Me.lblmanufacturer = New System.Windows.Forms.Label()
        Me.lblSKU = New System.Windows.Forms.Label()
        Me.lblConceptID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBrowseCPT = New System.Windows.Forms.Button()
        Me.btnClearCPT = New System.Windows.Forms.Button()
        Me.cmbCPT = New System.Windows.Forms.ComboBox()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.txtVaccineCode = New System.Windows.Forms.TextBox()
        Me.lblVaccineCode = New System.Windows.Forms.Label()
        Me.txtSKU = New System.Windows.Forms.TextBox()
        Me.pnl_tls_ = New System.Windows.Forms.Panel()
        Me.tlsImmunization = New System.Windows.Forms.ToolStrip()
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlCvxControl = New System.Windows.Forms.Panel()
        Me.pnlMvxControl = New System.Windows.Forms.Panel()
        Me.pnlTradeNameControl = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsImmunization.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCount
        '
        Me.txtCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCount.ForeColor = System.Drawing.Color.Black
        Me.txtCount.Location = New System.Drawing.Point(187, 214)
        Me.txtCount.MaxLength = 8
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ShortcutsEnabled = False
        Me.txtCount.Size = New System.Drawing.Size(69, 22)
        Me.txtCount.TabIndex = 10
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(17, 218)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(167, 14)
        Me.lblCount.TabIndex = 5
        Me.lblCount.Text = "Doses administered in series :"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.cmbLocation)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Controls.Add(Me.txtCvx)
        Me.pnlTop.Controls.Add(Me.lblVaccineType)
        Me.pnlTop.Controls.Add(Me.btnScanVIS)
        Me.pnlTop.Controls.Add(Me.btnCvx)
        Me.pnlTop.Controls.Add(Me.BtnAddVaccineCategory)
        Me.pnlTop.Controls.Add(Me.BtnAddManufacturerCategory)
        Me.pnlTop.Controls.Add(Me.BtnAddTradeNameCategory)
        Me.pnlTop.Controls.Add(Me.btnTradeName)
        Me.pnlTop.Controls.Add(Me.btnMvx)
        Me.pnlTop.Controls.Add(Me.txtTradeName)
        Me.pnlTop.Controls.Add(Me.txtMvx)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.txtSKUSearch)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.cmbFundingSource)
        Me.pnlTop.Controls.Add(Me.txtLotNo)
        Me.pnlTop.Controls.Add(Me.txtDosesOnHand)
        Me.pnlTop.Controls.Add(Me.lblDosesOnHand)
        Me.pnlTop.Controls.Add(Me.lblSnoMedID)
        Me.pnlTop.Controls.Add(Me.lblDescriptionID)
        Me.pnlTop.Controls.Add(Me.btnVaccineType)
        Me.pnlTop.Controls.Add(Me.btnDiagnosis)
        Me.pnlTop.Controls.Add(Me.btnClearVaccineType)
        Me.pnlTop.Controls.Add(Me.btnDiaCancel)
        Me.pnlTop.Controls.Add(Me.cmbDiagnosis)
        Me.pnlTop.Controls.Add(Me.txtNDCCode)
        Me.pnlTop.Controls.Add(Me.dtVISDate)
        Me.pnlTop.Controls.Add(Me.txtVIS)
        Me.pnlTop.Controls.Add(Me.lblNDC)
        Me.pnlTop.Controls.Add(Me.lblVIS)
        Me.pnlTop.Controls.Add(Me.lblVISDate)
        Me.pnlTop.Controls.Add(Me.lblFundingSource)
        Me.pnlTop.Controls.Add(Me.lblDiagnosis)
        Me.pnlTop.Controls.Add(Me.txtDosespervial)
        Me.pnlTop.Controls.Add(Me.lblDosesPerVial)
        Me.pnlTop.Controls.Add(Me.txtVials)
        Me.pnlTop.Controls.Add(Me.lblVials)
        Me.pnlTop.Controls.Add(Me.dtDateReceived)
        Me.pnlTop.Controls.Add(Me.lblDateReceived)
        Me.pnlTop.Controls.Add(Me.txtComments)
        Me.pnlTop.Controls.Add(Me.lblComments)
        Me.pnlTop.Controls.Add(Me.cmbStatus)
        Me.pnlTop.Controls.Add(Me.lblStatus)
        Me.pnlTop.Controls.Add(Me.lblLotNumber)
        Me.pnlTop.Controls.Add(Me.dtExpiryDate)
        Me.pnlTop.Controls.Add(Me.lblexpirydate)
        Me.pnlTop.Controls.Add(Me.lblTradeName)
        Me.pnlTop.Controls.Add(Me.lblmanufacturer)
        Me.pnlTop.Controls.Add(Me.lblSKU)
        Me.pnlTop.Controls.Add(Me.lblConceptID)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.btnBrowseCPT)
        Me.pnlTop.Controls.Add(Me.btnClearCPT)
        Me.pnlTop.Controls.Add(Me.cmbCPT)
        Me.pnlTop.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlTop.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTop.Controls.Add(Me.lbl_pnlRight)
        Me.pnlTop.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTop.Controls.Add(Me.txtVaccineCode)
        Me.pnlTop.Controls.Add(Me.txtCount)
        Me.pnlTop.Controls.Add(Me.lblVaccineCode)
        Me.pnlTop.Controls.Add(Me.lblCount)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(867, 434)
        Me.pnlTop.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(122, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 343
        Me.Label6.Text = "Location :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(186, 44)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(203, 22)
        Me.cmbLocation.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(516, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 14)
        Me.Label1.TabIndex = 341
        Me.Label1.Text = "*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnView)
        Me.Panel1.Controls.Add(Me.btnScan)
        Me.Panel1.Location = New System.Drawing.Point(395, 249)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(61, 23)
        Me.Panel1.TabIndex = 340
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Location = New System.Drawing.Point(24, 0)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(24, 23)
        Me.btnView.TabIndex = 11
        Me.btnView.UseVisualStyleBackColor = True
        '
        'btnScan
        '
        Me.btnScan.BackgroundImage = CType(resources.GetObject("btnScan.BackgroundImage"), System.Drawing.Image)
        Me.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScan.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.Location = New System.Drawing.Point(0, 0)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(24, 23)
        Me.btnScan.TabIndex = 11
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'txtCvx
        '
        Me.txtCvx.Location = New System.Drawing.Point(590, 74)
        Me.txtCvx.Name = "txtCvx"
        Me.txtCvx.Size = New System.Drawing.Size(200, 22)
        Me.txtCvx.TabIndex = 336
        Me.txtCvx.TabStop = False
        '
        'lblVaccineType
        '
        Me.lblVaccineType.AutoSize = True
        Me.lblVaccineType.BackColor = System.Drawing.Color.Transparent
        Me.lblVaccineType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVaccineType.Location = New System.Drawing.Point(530, 78)
        Me.lblVaccineType.Name = "lblVaccineType"
        Me.lblVaccineType.Size = New System.Drawing.Size(57, 14)
        Me.lblVaccineType.TabIndex = 261
        Me.lblVaccineType.Text = "Vaccine :"
        Me.lblVaccineType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnScanVIS
        '
        Me.btnScanVIS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScanVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScanVIS.Location = New System.Drawing.Point(16, 451)
        Me.btnScanVIS.Name = "btnScanVIS"
        Me.btnScanVIS.Size = New System.Drawing.Size(28, 28)
        Me.btnScanVIS.TabIndex = 9
        Me.btnScanVIS.Text = "Scan/Import"
        Me.btnScanVIS.UseVisualStyleBackColor = True
        Me.btnScanVIS.Visible = False
        '
        'btnCvx
        '
        Me.btnCvx.BackColor = System.Drawing.Color.Transparent
        Me.btnCvx.BackgroundImage = CType(resources.GetObject("btnCvx.BackgroundImage"), System.Drawing.Image)
        Me.btnCvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCvx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCvx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCvx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCvx.Image = CType(resources.GetObject("btnCvx.Image"), System.Drawing.Image)
        Me.btnCvx.Location = New System.Drawing.Point(794, 73)
        Me.btnCvx.Name = "btnCvx"
        Me.btnCvx.Size = New System.Drawing.Size(24, 23)
        Me.btnCvx.TabIndex = 333
        Me.btnCvx.TabStop = False
        Me.btnCvx.UseVisualStyleBackColor = False
        '
        'BtnAddVaccineCategory
        '
        Me.BtnAddVaccineCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddVaccineCategory.BackgroundImage = CType(resources.GetObject("BtnAddVaccineCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddVaccineCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddVaccineCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddVaccineCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddVaccineCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddVaccineCategory.Image = CType(resources.GetObject("BtnAddVaccineCategory.Image"), System.Drawing.Image)
        Me.BtnAddVaccineCategory.Location = New System.Drawing.Point(821, 73)
        Me.BtnAddVaccineCategory.Name = "BtnAddVaccineCategory"
        Me.BtnAddVaccineCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddVaccineCategory.TabIndex = 322
        Me.BtnAddVaccineCategory.TabStop = False
        Me.BtnAddVaccineCategory.Text = "          "
        Me.ToolTip1.SetToolTip(Me.BtnAddVaccineCategory, "Add Vaccine")
        Me.BtnAddVaccineCategory.UseVisualStyleBackColor = False
        '
        'BtnAddManufacturerCategory
        '
        Me.BtnAddManufacturerCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddManufacturerCategory.BackgroundImage = CType(resources.GetObject("BtnAddManufacturerCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddManufacturerCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddManufacturerCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddManufacturerCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddManufacturerCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddManufacturerCategory.Image = CType(resources.GetObject("BtnAddManufacturerCategory.Image"), System.Drawing.Image)
        Me.BtnAddManufacturerCategory.Location = New System.Drawing.Point(422, 109)
        Me.BtnAddManufacturerCategory.Name = "BtnAddManufacturerCategory"
        Me.BtnAddManufacturerCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddManufacturerCategory.TabIndex = 322
        Me.BtnAddManufacturerCategory.TabStop = False
        Me.BtnAddManufacturerCategory.Text = "          "
        Me.ToolTip1.SetToolTip(Me.BtnAddManufacturerCategory, "Add Manufacturer")
        Me.BtnAddManufacturerCategory.UseVisualStyleBackColor = False
        '
        'BtnAddTradeNameCategory
        '
        Me.BtnAddTradeNameCategory.BackColor = System.Drawing.Color.Transparent
        Me.BtnAddTradeNameCategory.BackgroundImage = CType(resources.GetObject("BtnAddTradeNameCategory.BackgroundImage"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnAddTradeNameCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnAddTradeNameCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnAddTradeNameCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddTradeNameCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddTradeNameCategory.Image = CType(resources.GetObject("BtnAddTradeNameCategory.Image"), System.Drawing.Image)
        Me.BtnAddTradeNameCategory.Location = New System.Drawing.Point(422, 74)
        Me.BtnAddTradeNameCategory.Name = "BtnAddTradeNameCategory"
        Me.BtnAddTradeNameCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddTradeNameCategory.TabIndex = 322
        Me.BtnAddTradeNameCategory.TabStop = False
        Me.BtnAddTradeNameCategory.Text = "          "
        Me.ToolTip1.SetToolTip(Me.BtnAddTradeNameCategory, "Add Trade Name")
        Me.BtnAddTradeNameCategory.UseVisualStyleBackColor = False
        '
        'btnTradeName
        '
        Me.btnTradeName.BackColor = System.Drawing.Color.Transparent
        Me.btnTradeName.BackgroundImage = CType(resources.GetObject("btnTradeName.BackgroundImage"), System.Drawing.Image)
        Me.btnTradeName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTradeName.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTradeName.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTradeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTradeName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTradeName.Image = CType(resources.GetObject("btnTradeName.Image"), System.Drawing.Image)
        Me.btnTradeName.Location = New System.Drawing.Point(395, 74)
        Me.btnTradeName.Name = "btnTradeName"
        Me.btnTradeName.Size = New System.Drawing.Size(24, 23)
        Me.btnTradeName.TabIndex = 322
        Me.btnTradeName.TabStop = False
        Me.btnTradeName.Text = "          "
        Me.btnTradeName.UseVisualStyleBackColor = False
        '
        'btnMvx
        '
        Me.btnMvx.BackColor = System.Drawing.Color.Transparent
        Me.btnMvx.BackgroundImage = CType(resources.GetObject("btnMvx.BackgroundImage"), System.Drawing.Image)
        Me.btnMvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMvx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMvx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMvx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMvx.Image = CType(resources.GetObject("btnMvx.Image"), System.Drawing.Image)
        Me.btnMvx.Location = New System.Drawing.Point(395, 109)
        Me.btnMvx.Name = "btnMvx"
        Me.btnMvx.Size = New System.Drawing.Size(24, 23)
        Me.btnMvx.TabIndex = 335
        Me.btnMvx.TabStop = False
        Me.btnMvx.UseVisualStyleBackColor = False
        '
        'txtTradeName
        '
        Me.txtTradeName.Location = New System.Drawing.Point(186, 74)
        Me.txtTradeName.Name = "txtTradeName"
        Me.txtTradeName.Size = New System.Drawing.Size(203, 22)
        Me.txtTradeName.TabIndex = 4
        Me.txtTradeName.Text = " "
        '
        'txtMvx
        '
        Me.txtMvx.Location = New System.Drawing.Point(186, 109)
        Me.txtMvx.Name = "txtMvx"
        Me.txtMvx.Size = New System.Drawing.Size(203, 22)
        Me.txtMvx.TabIndex = 337
        Me.txtMvx.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(479, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 14)
        Me.Label2.TabIndex = 320
        Me.Label2.Text = "*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSKUSearch
        '
        Me.txtSKUSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSKUSearch.Location = New System.Drawing.Point(186, 21)
        Me.txtSKUSearch.MaxLength = 15
        Me.txtSKUSearch.Name = "txtSKUSearch"
        Me.txtSKUSearch.Size = New System.Drawing.Size(203, 15)
        Me.txtSKUSearch.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(115, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 14)
        Me.Label7.TabIndex = 315
        Me.Label7.Text = "*"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(91, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 313
        Me.Label5.Text = "*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFundingSource
        '
        Me.cmbFundingSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFundingSource.FormattingEnabled = True
        Me.cmbFundingSource.Location = New System.Drawing.Point(590, 319)
        Me.cmbFundingSource.Name = "cmbFundingSource"
        Me.cmbFundingSource.Size = New System.Drawing.Size(200, 22)
        Me.cmbFundingSource.TabIndex = 16
        '
        'txtLotNo
        '
        Me.txtLotNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotNo.ForeColor = System.Drawing.Color.Black
        Me.txtLotNo.Location = New System.Drawing.Point(186, 144)
        Me.txtLotNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLotNo.MaxLength = 50
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ShortcutsEnabled = False
        Me.txtLotNo.Size = New System.Drawing.Size(203, 22)
        Me.txtLotNo.TabIndex = 5
        '
        'txtDosesOnHand
        '
        Me.txtDosesOnHand.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosesOnHand.ForeColor = System.Drawing.Color.Black
        Me.txtDosesOnHand.Location = New System.Drawing.Point(589, 179)
        Me.txtDosesOnHand.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDosesOnHand.MaxLength = 5
        Me.txtDosesOnHand.Name = "txtDosesOnHand"
        Me.txtDosesOnHand.Size = New System.Drawing.Size(108, 22)
        Me.txtDosesOnHand.TabIndex = 9
        '
        'lblDosesOnHand
        '
        Me.lblDosesOnHand.AutoSize = True
        Me.lblDosesOnHand.BackColor = System.Drawing.Color.Transparent
        Me.lblDosesOnHand.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosesOnHand.Location = New System.Drawing.Point(438, 183)
        Me.lblDosesOnHand.Name = "lblDosesOnHand"
        Me.lblDosesOnHand.Size = New System.Drawing.Size(149, 14)
        Me.lblDosesOnHand.TabIndex = 309
        Me.lblDosesOnHand.Text = "Total Doses in Inventory :"
        Me.lblDosesOnHand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSnoMedID
        '
        Me.lblSnoMedID.AutoSize = True
        Me.lblSnoMedID.Location = New System.Drawing.Point(614, 418)
        Me.lblSnoMedID.Name = "lblSnoMedID"
        Me.lblSnoMedID.Size = New System.Drawing.Size(0, 14)
        Me.lblSnoMedID.TabIndex = 302
        Me.lblSnoMedID.Visible = False
        '
        'lblDescriptionID
        '
        Me.lblDescriptionID.AutoSize = True
        Me.lblDescriptionID.Location = New System.Drawing.Point(547, 418)
        Me.lblDescriptionID.Name = "lblDescriptionID"
        Me.lblDescriptionID.Size = New System.Drawing.Size(0, 14)
        Me.lblDescriptionID.TabIndex = 301
        Me.lblDescriptionID.Visible = False
        '
        'btnVaccineType
        '
        Me.btnVaccineType.BackColor = System.Drawing.Color.Transparent
        Me.btnVaccineType.BackgroundImage = CType(resources.GetObject("btnVaccineType.BackgroundImage"), System.Drawing.Image)
        Me.btnVaccineType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVaccineType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnVaccineType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnVaccineType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVaccineType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVaccineType.Image = CType(resources.GetObject("btnVaccineType.Image"), System.Drawing.Image)
        Me.btnVaccineType.Location = New System.Drawing.Point(148, 461)
        Me.btnVaccineType.Name = "btnVaccineType"
        Me.btnVaccineType.Size = New System.Drawing.Size(21, 21)
        Me.btnVaccineType.TabIndex = 303
        Me.btnVaccineType.UseVisualStyleBackColor = False
        Me.btnVaccineType.Visible = False
        '
        'btnDiagnosis
        '
        Me.btnDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.btnDiagnosis.BackgroundImage = CType(resources.GetObject("btnDiagnosis.BackgroundImage"), System.Drawing.Image)
        Me.btnDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDiagnosis.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDiagnosis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDiagnosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiagnosis.Image = CType(resources.GetObject("btnDiagnosis.Image"), System.Drawing.Image)
        Me.btnDiagnosis.Location = New System.Drawing.Point(395, 284)
        Me.btnDiagnosis.Name = "btnDiagnosis"
        Me.btnDiagnosis.Size = New System.Drawing.Size(24, 23)
        Me.btnDiagnosis.TabIndex = 13
        Me.btnDiagnosis.UseVisualStyleBackColor = False
        '
        'btnClearVaccineType
        '
        Me.btnClearVaccineType.BackColor = System.Drawing.Color.Transparent
        Me.btnClearVaccineType.BackgroundImage = CType(resources.GetObject("btnClearVaccineType.BackgroundImage"), System.Drawing.Image)
        Me.btnClearVaccineType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearVaccineType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearVaccineType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearVaccineType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearVaccineType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearVaccineType.Image = CType(resources.GetObject("btnClearVaccineType.Image"), System.Drawing.Image)
        Me.btnClearVaccineType.Location = New System.Drawing.Point(175, 461)
        Me.btnClearVaccineType.Name = "btnClearVaccineType"
        Me.btnClearVaccineType.Size = New System.Drawing.Size(21, 21)
        Me.btnClearVaccineType.TabIndex = 304
        Me.btnClearVaccineType.UseVisualStyleBackColor = False
        Me.btnClearVaccineType.Visible = False
        '
        'btnDiaCancel
        '
        Me.btnDiaCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnDiaCancel.BackgroundImage = CType(resources.GetObject("btnDiaCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnDiaCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDiaCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDiaCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDiaCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDiaCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiaCancel.Image = CType(resources.GetObject("btnDiaCancel.Image"), System.Drawing.Image)
        Me.btnDiaCancel.Location = New System.Drawing.Point(422, 284)
        Me.btnDiaCancel.Name = "btnDiaCancel"
        Me.btnDiaCancel.Size = New System.Drawing.Size(24, 23)
        Me.btnDiaCancel.TabIndex = 15
        Me.btnDiaCancel.TabStop = False
        Me.btnDiaCancel.UseVisualStyleBackColor = False
        '
        'cmbDiagnosis
        '
        Me.cmbDiagnosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDiagnosis.FormattingEnabled = True
        Me.cmbDiagnosis.Location = New System.Drawing.Point(186, 284)
        Me.cmbDiagnosis.Name = "cmbDiagnosis"
        Me.cmbDiagnosis.Size = New System.Drawing.Size(203, 22)
        Me.cmbDiagnosis.TabIndex = 566
        Me.cmbDiagnosis.TabStop = False
        '
        'txtNDCCode
        '
        Me.txtNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNDCCode.ForeColor = System.Drawing.Color.Black
        Me.txtNDCCode.Location = New System.Drawing.Point(186, 319)
        Me.txtNDCCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNDCCode.MaxLength = 11
        Me.txtNDCCode.Name = "txtNDCCode"
        Me.txtNDCCode.Size = New System.Drawing.Size(203, 22)
        Me.txtNDCCode.TabIndex = 15
        '
        'dtVISDate
        '
        Me.dtVISDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtVISDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtVISDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtVISDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtVISDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtVISDate.Checked = False
        Me.dtVISDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVISDate.Location = New System.Drawing.Point(590, 249)
        Me.dtVISDate.Name = "dtVISDate"
        Me.dtVISDate.ShowCheckBox = True
        Me.dtVISDate.Size = New System.Drawing.Size(108, 22)
        Me.dtVISDate.TabIndex = 12
        '
        'txtVIS
        '
        Me.txtVIS.Enabled = False
        Me.txtVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVIS.ForeColor = System.Drawing.Color.Black
        Me.txtVIS.Location = New System.Drawing.Point(186, 249)
        Me.txtVIS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVIS.MaxLength = 1000
        Me.txtVIS.Name = "txtVIS"
        Me.txtVIS.Size = New System.Drawing.Size(203, 22)
        Me.txtVIS.TabIndex = 11
        '
        'lblNDC
        '
        Me.lblNDC.AutoSize = True
        Me.lblNDC.BackColor = System.Drawing.Color.Transparent
        Me.lblNDC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDC.Location = New System.Drawing.Point(113, 323)
        Me.lblNDC.Name = "lblNDC"
        Me.lblNDC.Size = New System.Drawing.Size(70, 14)
        Me.lblNDC.TabIndex = 289
        Me.lblNDC.Text = "NDC Code :"
        Me.lblNDC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVIS
        '
        Me.lblVIS.AutoSize = True
        Me.lblVIS.BackColor = System.Drawing.Color.Transparent
        Me.lblVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVIS.Location = New System.Drawing.Point(149, 253)
        Me.lblVIS.Name = "lblVIS"
        Me.lblVIS.Size = New System.Drawing.Size(34, 14)
        Me.lblVIS.TabIndex = 286
        Me.lblVIS.Text = "VIS :"
        Me.lblVIS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVISDate
        '
        Me.lblVISDate.AutoSize = True
        Me.lblVISDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVISDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVISDate.Location = New System.Drawing.Point(484, 253)
        Me.lblVISDate.Name = "lblVISDate"
        Me.lblVISDate.Size = New System.Drawing.Size(103, 14)
        Me.lblVISDate.TabIndex = 285
        Me.lblVISDate.Text = "Publication Date :"
        Me.lblVISDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFundingSource
        '
        Me.lblFundingSource.AutoSize = True
        Me.lblFundingSource.BackColor = System.Drawing.Color.Transparent
        Me.lblFundingSource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFundingSource.Location = New System.Drawing.Point(487, 323)
        Me.lblFundingSource.Name = "lblFundingSource"
        Me.lblFundingSource.Size = New System.Drawing.Size(100, 14)
        Me.lblFundingSource.TabIndex = 284
        Me.lblFundingSource.Text = "Funding Source :"
        Me.lblFundingSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiagnosis
        '
        Me.lblDiagnosis.AutoSize = True
        Me.lblDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.lblDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiagnosis.Location = New System.Drawing.Point(119, 288)
        Me.lblDiagnosis.Name = "lblDiagnosis"
        Me.lblDiagnosis.Size = New System.Drawing.Size(64, 14)
        Me.lblDiagnosis.TabIndex = 283
        Me.lblDiagnosis.Text = "Diagnosis :"
        Me.lblDiagnosis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDosespervial
        '
        Me.txtDosespervial.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosespervial.ForeColor = System.Drawing.Color.Black
        Me.txtDosespervial.Location = New System.Drawing.Point(358, 179)
        Me.txtDosespervial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDosespervial.MaxLength = 5
        Me.txtDosespervial.Name = "txtDosespervial"
        Me.txtDosespervial.Size = New System.Drawing.Size(69, 22)
        Me.txtDosespervial.TabIndex = 8
        '
        'lblDosesPerVial
        '
        Me.lblDosesPerVial.AutoSize = True
        Me.lblDosesPerVial.BackColor = System.Drawing.Color.Transparent
        Me.lblDosesPerVial.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosesPerVial.Location = New System.Drawing.Point(266, 183)
        Me.lblDosesPerVial.Name = "lblDosesPerVial"
        Me.lblDosesPerVial.Size = New System.Drawing.Size(89, 14)
        Me.lblDosesPerVial.TabIndex = 281
        Me.lblDosesPerVial.Text = "Doses per vial :"
        Me.lblDosesPerVial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVials
        '
        Me.txtVials.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVials.ForeColor = System.Drawing.Color.Black
        Me.txtVials.Location = New System.Drawing.Point(186, 179)
        Me.txtVials.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVials.MaxLength = 5
        Me.txtVials.Name = "txtVials"
        Me.txtVials.Size = New System.Drawing.Size(69, 22)
        Me.txtVials.TabIndex = 7
        '
        'lblVials
        '
        Me.lblVials.AutoSize = True
        Me.lblVials.BackColor = System.Drawing.Color.Transparent
        Me.lblVials.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVials.Location = New System.Drawing.Point(113, 183)
        Me.lblVials.Name = "lblVials"
        Me.lblVials.Size = New System.Drawing.Size(70, 14)
        Me.lblVials.TabIndex = 279
        Me.lblVials.Text = "Vial Count :"
        Me.lblVials.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtDateReceived
        '
        Me.dtDateReceived.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtDateReceived.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtDateReceived.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtDateReceived.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtDateReceived.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtDateReceived.Checked = False
        Me.dtDateReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDateReceived.Location = New System.Drawing.Point(590, 17)
        Me.dtDateReceived.Name = "dtDateReceived"
        Me.dtDateReceived.ShowCheckBox = True
        Me.dtDateReceived.Size = New System.Drawing.Size(108, 22)
        Me.dtDateReceived.TabIndex = 1
        '
        'lblDateReceived
        '
        Me.lblDateReceived.AutoSize = True
        Me.lblDateReceived.BackColor = System.Drawing.Color.Transparent
        Me.lblDateReceived.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateReceived.Location = New System.Drawing.Point(493, 21)
        Me.lblDateReceived.Name = "lblDateReceived"
        Me.lblDateReceived.Size = New System.Drawing.Size(94, 14)
        Me.lblDateReceived.TabIndex = 272
        Me.lblDateReceived.Text = "Date Received :"
        Me.lblDateReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(186, 354)
        Me.txtComments.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtComments.MaxLength = 1000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComments.Size = New System.Drawing.Size(604, 67)
        Me.txtComments.TabIndex = 17
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.BackColor = System.Drawing.Color.Transparent
        Me.lblComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(110, 354)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(73, 14)
        Me.lblComments.TabIndex = 270
        Me.lblComments.Text = "Comments :"
        Me.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cmbStatus.Location = New System.Drawing.Point(590, 45)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(108, 22)
        Me.cmbStatus.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(537, 49)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(50, 14)
        Me.lblStatus.TabIndex = 268
        Me.lblStatus.Text = "Status :"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLotNumber
        '
        Me.lblLotNumber.AutoSize = True
        Me.lblLotNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblLotNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLotNumber.Location = New System.Drawing.Point(127, 148)
        Me.lblLotNumber.Name = "lblLotNumber"
        Me.lblLotNumber.Size = New System.Drawing.Size(56, 14)
        Me.lblLotNumber.TabIndex = 266
        Me.lblLotNumber.Text = "Lot No. :"
        Me.lblLotNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtExpiryDate
        '
        Me.dtExpiryDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtExpiryDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtExpiryDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtExpiryDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtExpiryDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtExpiryDate.Checked = False
        Me.dtExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtExpiryDate.Location = New System.Drawing.Point(590, 144)
        Me.dtExpiryDate.Name = "dtExpiryDate"
        Me.dtExpiryDate.ShowCheckBox = True
        Me.dtExpiryDate.Size = New System.Drawing.Size(108, 22)
        Me.dtExpiryDate.TabIndex = 6
        '
        'lblexpirydate
        '
        Me.lblexpirydate.AutoSize = True
        Me.lblexpirydate.BackColor = System.Drawing.Color.Transparent
        Me.lblexpirydate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexpirydate.Location = New System.Drawing.Point(518, 148)
        Me.lblexpirydate.Name = "lblexpirydate"
        Me.lblexpirydate.Size = New System.Drawing.Size(69, 14)
        Me.lblexpirydate.TabIndex = 264
        Me.lblexpirydate.Text = "Exp. Date :"
        Me.lblexpirydate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTradeName
        '
        Me.lblTradeName.AutoSize = True
        Me.lblTradeName.BackColor = System.Drawing.Color.Transparent
        Me.lblTradeName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeName.Location = New System.Drawing.Point(101, 78)
        Me.lblTradeName.Name = "lblTradeName"
        Me.lblTradeName.Size = New System.Drawing.Size(82, 14)
        Me.lblTradeName.TabIndex = 263
        Me.lblTradeName.Text = "Trade Name :"
        Me.lblTradeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmanufacturer
        '
        Me.lblmanufacturer.AutoSize = True
        Me.lblmanufacturer.BackColor = System.Drawing.Color.Transparent
        Me.lblmanufacturer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmanufacturer.Location = New System.Drawing.Point(96, 113)
        Me.lblmanufacturer.Name = "lblmanufacturer"
        Me.lblmanufacturer.Size = New System.Drawing.Size(87, 14)
        Me.lblmanufacturer.TabIndex = 262
        Me.lblmanufacturer.Text = "Manufacturer :"
        Me.lblmanufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSKU
        '
        Me.lblSKU.AutoSize = True
        Me.lblSKU.BackColor = System.Drawing.Color.Transparent
        Me.lblSKU.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSKU.Location = New System.Drawing.Point(146, 21)
        Me.lblSKU.Name = "lblSKU"
        Me.lblSKU.Size = New System.Drawing.Size(37, 14)
        Me.lblSKU.TabIndex = 259
        Me.lblSKU.Text = "SKU :"
        Me.lblSKU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblConceptID
        '
        Me.lblConceptID.AutoSize = True
        Me.lblConceptID.Location = New System.Drawing.Point(439, 539)
        Me.lblConceptID.Name = "lblConceptID"
        Me.lblConceptID.Size = New System.Drawing.Size(0, 14)
        Me.lblConceptID.TabIndex = 204
        Me.lblConceptID.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(414, 496)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 14)
        Me.Label4.TabIndex = 201
        Me.Label4.Text = "*"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(518, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "CPT Code :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowseCPT
        '
        Me.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCPT.BackgroundImage = CType(resources.GetObject("btnBrowseCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseCPT.Image = CType(resources.GetObject("btnBrowseCPT.Image"), System.Drawing.Image)
        Me.btnBrowseCPT.Location = New System.Drawing.Point(794, 283)
        Me.btnBrowseCPT.Name = "btnBrowseCPT"
        Me.btnBrowseCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnBrowseCPT.TabIndex = 14
        Me.btnBrowseCPT.UseVisualStyleBackColor = False
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(821, 283)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnClearCPT.TabIndex = 18
        Me.btnClearCPT.TabStop = False
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'cmbCPT
        '
        Me.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCPT.FormattingEnabled = True
        Me.cmbCPT.Location = New System.Drawing.Point(590, 284)
        Me.cmbCPT.Name = "cmbCPT"
        Me.cmbCPT.Size = New System.Drawing.Size(200, 22)
        Me.cmbCPT.TabIndex = 677
        Me.cmbCPT.TabStop = False
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 430)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(859, 1)
        Me.lbl_pnlBottom.TabIndex = 9
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 427)
        Me.lbl_pnlLeft.TabIndex = 8
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(863, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 427)
        Me.lbl_pnlRight.TabIndex = 7
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(861, 1)
        Me.lbl_pnlTop.TabIndex = 6
        Me.lbl_pnlTop.Text = "label1"
        '
        'txtVaccineCode
        '
        Me.txtVaccineCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVaccineCode.ForeColor = System.Drawing.Color.Black
        Me.txtVaccineCode.Location = New System.Drawing.Point(518, 497)
        Me.txtVaccineCode.MaxLength = 10
        Me.txtVaccineCode.Name = "txtVaccineCode"
        Me.txtVaccineCode.ShortcutsEnabled = False
        Me.txtVaccineCode.Size = New System.Drawing.Size(77, 22)
        Me.txtVaccineCode.TabIndex = 3
        Me.txtVaccineCode.Visible = False
        '
        'lblVaccineCode
        '
        Me.lblVaccineCode.AutoSize = True
        Me.lblVaccineCode.BackColor = System.Drawing.Color.Transparent
        Me.lblVaccineCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVaccineCode.Location = New System.Drawing.Point(426, 501)
        Me.lblVaccineCode.Name = "lblVaccineCode"
        Me.lblVaccineCode.Size = New System.Drawing.Size(89, 14)
        Me.lblVaccineCode.TabIndex = 5
        Me.lblVaccineCode.Text = "Vaccine Code :"
        Me.lblVaccineCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVaccineCode.Visible = False
        '
        'txtSKU
        '
        Me.txtSKU.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSKU.ForeColor = System.Drawing.Color.Black
        Me.txtSKU.Location = New System.Drawing.Point(436, 13)
        Me.txtSKU.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSKU.MaxLength = 15
        Me.txtSKU.Name = "txtSKU"
        Me.txtSKU.Size = New System.Drawing.Size(172, 22)
        Me.txtSKU.TabIndex = 0
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsImmunization)
        Me.pnl_tls_.Controls.Add(Me.txtSKU)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(867, 53)
        Me.pnl_tls_.TabIndex = 1
        '
        'tlsImmunization
        '
        Me.tlsImmunization.BackColor = System.Drawing.Color.Transparent
        Me.tlsImmunization.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsImmunization.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsImmunization.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsImmunization.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tlsImmunization.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsImmunization.Location = New System.Drawing.Point(0, 0)
        Me.tlsImmunization.Name = "tlsImmunization"
        Me.tlsImmunization.Size = New System.Drawing.Size(867, 53)
        Me.tlsImmunization.TabIndex = 0
        Me.tlsImmunization.TabStop = True
        Me.tlsImmunization.Text = "toolStrip1"
        '
        'btn_tls_Save
        '
        Me.btn_tls_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Save.Image = CType(resources.GetObject("btn_tls_Save.Image"), System.Drawing.Image)
        Me.btn_tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Save.Name = "btn_tls_Save"
        Me.btn_tls_Save.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Save.Tag = "Save"
        Me.btn_tls_Save.Text = "&Save&&Cls"
        Me.btn_tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Save.ToolTipText = "Save and Close"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlCvxControl
        '
        Me.pnlCvxControl.Location = New System.Drawing.Point(592, 153)
        Me.pnlCvxControl.Name = "pnlCvxControl"
        Me.pnlCvxControl.Size = New System.Drawing.Size(253, 165)
        Me.pnlCvxControl.TabIndex = 322
        Me.pnlCvxControl.Visible = False
        '
        'pnlMvxControl
        '
        Me.pnlMvxControl.Location = New System.Drawing.Point(187, 188)
        Me.pnlMvxControl.Name = "pnlMvxControl"
        Me.pnlMvxControl.Size = New System.Drawing.Size(259, 165)
        Me.pnlMvxControl.TabIndex = 323
        Me.pnlMvxControl.Visible = False
        '
        'pnlTradeNameControl
        '
        Me.pnlTradeNameControl.Location = New System.Drawing.Point(186, 151)
        Me.pnlTradeNameControl.Name = "pnlTradeNameControl"
        Me.pnlTradeNameControl.Size = New System.Drawing.Size(260, 165)
        Me.pnlTradeNameControl.TabIndex = 325
        Me.pnlTradeNameControl.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(108, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 14)
        Me.Label8.TabIndex = 678
        Me.Label8.Text = "*"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmIM_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(867, 487)
        Me.Controls.Add(Me.pnlCvxControl)
        Me.Controls.Add(Me.pnlTradeNameControl)
        Me.Controls.Add(Me.pnlMvxControl)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIM_Setup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Immunization Setup"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsImmunization.ResumeLayout(False)
        Me.tlsImmunization.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmIM_Setup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DisposedGlobal()   'obj Disposed by mitesh
    End Sub



    Private Sub frmIMSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oDM As New gloStream.Immunization.ItemSetup
        Try
            ''Added by Mayuri:20110112-Immunization functionality changed;now we are using standard data

            btnView.Visible = False

            ''To fill data for modify,duplicate
            dsVaccineInformation = oDM.getVaccineTypes(_EditID)
            txtCount.Text = "0"
            ''To fill combo boxes
            dtDateReceived.Checked = True

            FillFundingSource()
            cmbStatus.Items.Clear()

            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Inactive")
            cmbStatus.SelectedIndex = 0
            txtDosesOnHand.Text = "0"
            ''Added by Mayuri:20120507-Location changes added in immunization
            FillLocation()
            If _EditID <> 0 Then
                txtTradeName.Select()
                _isFormLoadModify = True
                _isLoadGridCvxControl = True
               

                Fill_EditCriteria(_EditID)
                If txtSKUSearch.Text.Trim() <> "" Then
                    _isLoad = True
                End If
            End If
            'If txtSKUSearch.Text <> "" Then

            'End If
            _isLoadGridCvxControl = False
            _isLoaded = True
            If btnScan.Visible = True Then
                btnScan.TabStop = True
                btnView.TabStop = False
                btnScan.TabIndex = 9

            Else
                btnView.TabStop = True
                btnScan.TabStop = False
                btnView.TabIndex = 9

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oDM) = False Then
                oDM = Nothing
            End If
        End Try
    End Sub
#Region "To fill drop down from standard data :Mayuri 20120112"

    Private Sub FillFundingSource()

        Try


            cmbFundingSource.Items.Add("")

            Dim i As Int16
            If IsNothing(dsVaccineInformation.Tables("FundingSource")) = False Then
                If dsVaccineInformation.Tables("FundingSource").Rows.Count > 0 Then
                    For i = 0 To dsVaccineInformation.Tables("FundingSource").Rows.Count - 1
                        cmbFundingSource.Items.Add(dsVaccineInformation.Tables("FundingSource").Rows(i)(1).ToString())
                    Next
                End If
            End If
            cmbFundingSource.Text = ""



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

        End Try
    End Sub
    Private Sub FillLocation()

        Try


            ''  cmbLocation.Items.Add("")

            Dim i As Int16
            If IsNothing(dsVaccineInformation.Tables("Location")) = False Then
                If dsVaccineInformation.Tables("Location").Rows.Count > 0 Then
                    For i = 0 To dsVaccineInformation.Tables("Location").Rows.Count - 1
                        cmbLocation.Items.Add(dsVaccineInformation.Tables("Location").Rows(i)(1).ToString())
                       
                    Next
                End If
            End If
            ''cmbLocation.
            'If IsNothing(_DefaultLocation) Then
            If Convert.ToString(_DefaultLocation) = "" Then
                If cmbLocation.Items.Count > 0 Then
                    cmbLocation.SelectedIndex = 0
                End If

            Else
                cmbLocation.SelectedIndex = cmbLocation.FindStringExact(_DefaultLocation)
            End If

            'Else
            '' cmbLocation.SelectedValue = _DefaultLocationID
            'cmbLocation.SelectedIndex = cmbLocation.FindStringExact(_DefaultLocation)
            'End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

        End Try
    End Sub
    'Private Sub FillVaccineTypes()

    '    Try
    '        cmbVaccineType.Items.Add("")
    '        Dim i As Int16
    '        If IsNothing(dsVaccineInformation.Tables("VaccineTypes")) = False Then
    '            If dsVaccineInformation.Tables("VaccineTypes").Rows.Count > 0 Then
    '                For i = 0 To dsVaccineInformation.Tables("VaccineTypes").Rows.Count - 1
    '                    cmbVaccineType.Items.Add(dsVaccineInformation.Tables("VaccineTypes").Rows(i)("Code").ToString() & " - " & dsVaccineInformation.Tables("VaccineTypes").Rows(i)("ShortDescription").ToString())
    '                Next
    '            End If
    '        End If

    '        cmbVaccineType.SelectedIndex = 0

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally


    '    End Try
    'End Sub
    'Private Sub FillManufacturerTypes()

    '    Try
    '        cmbManufacturer.Items.Add("")
    '        Dim i As Int16
    '        If IsNothing(dsVaccineInformation.Tables("Manufacturer")) = False Then
    '            If dsVaccineInformation.Tables("Manufacturer").Rows.Count > 0 Then
    '                For i = 0 To dsVaccineInformation.Tables("Manufacturer").Rows.Count - 1
    '                    cmbManufacturer.Items.Add(dsVaccineInformation.Tables("Manufacturer").Rows(i)("Code").ToString() & " - " & dsVaccineInformation.Tables("Manufacturer").Rows(i)("ManufacturerName").ToString())
    '                Next
    '            End If
    '        End If

    '        cmbManufacturer.SelectedIndex = 0

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally


    '    End Try
    'End Sub

    'Private Sub FillTradeNames()

    '    Try
    '        cmbTradeName.Items.Add("")
    '        Dim i As Int16
    '        If IsNothing(dsVaccineInformation.Tables("TradeName")) = False Then
    '            If dsVaccineInformation.Tables("TradeName").Rows.Count > 0 Then
    '                For i = 0 To dsVaccineInformation.Tables("TradeName").Rows.Count - 1
    '                    cmbTradeName.Items.Add(dsVaccineInformation.Tables("TradeName").Rows(i)("CdcProductName").ToString())
    '                Next
    '            End If
    '        End If

    '        cmbTradeName.SelectedIndex = 0

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally


    '    End Try
    'End Sub
    Private Sub AllowNumericValue(ByVal Text As String, ByVal e As KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
            'e.KeyChar.IsDigit(e.KeyChar)
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Then
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(46)) Or (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCount.KeyPress
        Try
            AllowNumericValue(txtCount.Text, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub Fill_EditCriteria(ByVal lItemID As Long)
        Dim oCriteria As New gloStream.Immunization.Supporting.ImmunizationItem
        Dim oDM As New gloStream.Immunization.ItemSetup

        Dim arrSplitCode As String()
        Dim arrSplitDiagnosis As String()

        oCriteria = oDM.ItemDetail(dsVaccineInformation.Tables(2))

        If Not oCriteria Is Nothing Then
            With oCriteria

                txtCount.Text = .HowMany
                txtVaccineCode.Text = .VaccineCode


                arrSplitCode = .CPTCode.Split(",")
                For i As Integer = 0 To arrSplitCode.GetUpperBound(0)
                    If arrSplitCode(i).Trim <> "" Then
                        cmbCPT.Items.Add(arrSplitCode(i))
                    End If
                Next
                If cmbCPT.Items.Count > 0 Then
                    cmbCPT.SelectedIndex = 0
                End If
                '  cmbCPT.SelectedIndex = 0

                lblConceptID.Text = .ConceptID
                lblDescriptionID.Text = .DescriptionID
                lblSnoMedID.Text = .SnoMedID

                IM_strDefination = .SnomedDefinition
                IM_strSnomedDescription = .SnomedDescription
                IM_strRxNormCode = .RxnormID
                IM_strNDCCode = .NDCCode

                If .Location <> "" Then
                    If cmbLocation.Items.Count > 0 Then
                        cmbLocation.SelectedIndex = cmbLocation.FindStringExact(.Location)
                    End If

                Else
                    'If IsNothing(_DefaultLocation) Then
                    If Convert.ToString(_DefaultLocation) = "" Then
                        If cmbLocation.Items.Count > 0 Then
                            cmbLocation.SelectedIndex = 0
                        End If

                    Else
                        cmbLocation.SelectedIndex = cmbLocation.FindStringExact(_DefaultLocation)
                    End If

                    '    Else
                    '    ' cmbLocation.SelectedValue = _DefaultLocationID
                    '    cmbLocation.SelectedIndex = cmbLocation.FindStringExact(_DefaultLocation)
                    'End If
                End If


        If .SKU <> "" Then
            txtSKUSearch.Text = .SKU
        Else
            txtSKUSearch.Text = ""
        End If

        If .ReceivedDate = "12:00:00 AM" Or .LotNo = "" Then
        Else
            dtDateReceived.Value = .ReceivedDate
        End If

        If .DocumentID <> 0 Then

            _DocumentID = .DocumentID
            txtVIS.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
            If txtVIS.Text.Trim() <> "" Then
                btnView.Visible = True
                btnScan.Visible = False
            Else
                btnScan.Visible = True
                btnView.Visible = False
            End If
        Else
            btnScan.Visible = True
            btnView.Visible = False
        End If

        If .Vaccine <> "" Then
            txtCvx.Text = .Vaccine


        End If
        If .Manufacturer <> "" Then
            txtMvx.Text = .Manufacturer


        End If
        If .TradeName <> "" Then
            txtTradeName.Text = .TradeName


        End If
        txtLotNo.Text = .LotNo
        If .ExpiryDate = "12:00:00 AM" Or .LotNo = "" Then

        Else
            dtExpiryDate.Value = .ExpiryDate
        End If
        If Val(.VialCount) = 0 Then
            txtVials.Text = ""
        Else
            txtVials.Text = .VialCount
        End If
        If Val(.DosesPerVial) = 0 Then
            txtDosespervial.Text = ""
        Else
            txtDosespervial.Text = .DosesPerVial
        End If

        If Val(.AvailableDoses) = 0 Then
            txtDosesOnHand.Text = "0"
        Else
            txtDosesOnHand.Text = .AvailableDoses

        End If

        If .PublicationDate = "12:00:00 AM" Or .LotNo = "" Then
        Else
            dtVISDate.Value = .PublicationDate

        End If


        arrSplitDiagnosis = .DiagnosisCode.Split(",")
        For i As Integer = 0 To arrSplitDiagnosis.GetUpperBound(0)
            cmbDiagnosis.Items.Add(arrSplitDiagnosis(i))
        Next
        If Val(.NDCCode1) = 0 Then
            txtNDCCode.Text = ""
        Else
            txtNDCCode.Text = .NDCCode1
        End If
        If cmbFundingSource.Items.Contains(.FundingSource) Then

            cmbFundingSource.Text = .FundingSource
        Else
            cmbFundingSource.Items.Add(.FundingSource)
            cmbFundingSource.Text = .FundingSource
        End If

        txtComments.Text = .Comments
        cmbStatus.Text = .Active

            End With

            cmbDiagnosis.SelectedIndex = 0


        End If
        If IsNothing(oCriteria) = False Then
            oCriteria = Nothing
        End If
        If IsNothing(oDM) = False Then
            oDM = Nothing
        End If

    End Sub


#End Region
    Private Sub SaveImmunization() Handles Me.SaveFunction
        Dim CPTCode As String = ""
        Dim DiagnosisCode As String = ""
        Dim oDM As New gloStream.Immunization.ItemSetup
        Dim dtVaccine As DataSet
        Try
           

            oCvxControl_InternalGridLostFocus(Nothing, Nothing)
            oMvxControl_InternalGridLostFocus(Nothing, Nothing)
            oTradeNameControl_InternalGridLostFocus(Nothing, Nothing)
            
            If dtDateReceived.Checked = False Then
                MessageBox.Show("Select Date Received. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                dtDateReceived.Select()
                _IsValidationFailed = True
                Exit Sub
            End If
            If cmbLocation.Text.Trim = "" Then
                MessageBox.Show("Select Location. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbLocation.Select()
                _IsValidationFailed = True
                Exit Sub
            End If
            If txtTradeName.Text.Trim = "" Then
                MessageBox.Show("Enter Trade Name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtTradeName.Select()
                _IsValidationFailed = True
                Exit Sub
            End If
            If txtCvx.Text.Trim = "" Then
                MessageBox.Show("Enter Vaccine Name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCvx.Select()
                _IsValidationFailed = True
                Exit Sub
            End If
          
            If txtLotNo.Text.Trim = "" Then
                MessageBox.Show("Enter Lot Number. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLotNo.Select()
                _IsValidationFailed = True
                Exit Sub
            End If
            If txtCount.Text.Trim = "" Then

                txtCount.Text = "0"
                _IsValidationFailed = True
                Exit Sub
            End If
            

            If CheckTradeNameisValidagainstSKU() = False Then
                _IsValidationFailed = True
                txtSKUSearch.Select()
                Exit Sub
            End If
            Dim _CVX As String = ""
            If oDM.IsCustomTradeNameOrCVX(txtTradeName.Text.Trim, txtCvx.Text.Trim, 0) = False Then
                If oDM.IsCustomTradeNameOrCVX(txtTradeName.Text.Trim, txtCvx.Text.Trim, 1) = False Then
                    dtVaccine = oDM.CheckVaccineisValidaginstTradeName(txtTradeName.Text.Trim, txtCvx.Text.Trim)
                    If IsNothing(dtVaccine) = False Then
                        If dtVaccine.Tables("CVX").Rows.Count > 0 Then
                            For i As Integer = 0 To dtVaccine.Tables("CVX").Rows.Count - 1
                                If _CVX = "" Then
                                    _CVX = dtVaccine.Tables("CVX").Rows(i)("Vaccine")
                                Else
                                    _CVX = _CVX & Chr(13) & dtVaccine.Tables("CVX").Rows(i)("Vaccine")
                                End If


                            Next


                        End If
                        If dtVaccine.Tables("Exists").Rows.Count > 0 Then

                            If dtVaccine.Tables("Exists").Rows(0)("IsExists") = "0" Then
                                MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txtTradeName.Text.Trim & "'" & " does not match the Vaccine Given " & "'" & txtCvx.Text.Trim & "'" & "." & Chr(13) & Chr(13) & "Please select one of the following CVX codes or use a custom CVX code: " & Space(70) & Chr(13) & _CVX, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                _IsValidationFailed = True
                                txtCvx.Select()
                                Exit Sub
                            End If
                        End If


                    End If
                End If
            End If

          
            If _SaveFlag = True Then
                _EditID = 0
            End If
            Dim status As String = ""
            If IsNothing(cmbStatus.SelectedItem) = False Then
                status = cmbStatus.SelectedItem.ToString().ToUpper()
            End If
            If oDM.IsExists(_EditID, txtCvx.Text.Trim, txtLotNo.Text.Trim, cmbLocation.Text.Trim) = True Then
                MessageBox.Show("Vaccine or Lot Number already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtLotNo.Select()
                ' txtSKU.Select()
                _IsValidationFailed = True
                Exit Sub
            End If




            Dim CPTText As String
            For i As Integer = 0 To cmbCPT.Items.Count - 1
                cmbCPT.SelectedIndex = i
                CPTText = cmbCPT.Text
                If (i) Then
                    CPTCode = CPTCode & ","
                End If
                CPTCode &= CPTText
            Next


            Dim _SKU As String
            Dim _NDCCode As String

            _SKU = txtSKUSearch.Text.Trim


            _NDCCode = txtNDCCode.Text.Trim



            Dim DiagnosisText As String
            For i As Integer = 0 To cmbDiagnosis.Items.Count - 1
                cmbDiagnosis.SelectedIndex = i
                DiagnosisText = cmbDiagnosis.Text
                If (i) Then
                    DiagnosisCode = DiagnosisCode & ","
                End If
                DiagnosisCode &= DiagnosisText
            Next
            Dim _ReceivedDate As String
            Dim _expiryDate As String
            Dim _PublicationDate As String
            If dtDateReceived.Checked = True Then
                _ReceivedDate = dtDateReceived.Value.ToShortDateString()
            End If
            If dtExpiryDate.Checked = True Then
                _expiryDate = dtExpiryDate.Value.ToShortDateString()
            End If
            If dtVISDate.Checked = True Then
                _PublicationDate = dtVISDate.Value.ToShortDateString()
            End If


            _ReturnID = oDM.AddData(txtCount.Text.Trim, CPTCode.Trim, txtVaccineCode.Text.Trim, lblConceptID.Text, lblDescriptionID.Text, lblSnoMedID.Text, IM_strDefination, IM_strRxNormCode, IM_strNDCCode, IM_strSnomedDescription, _SKU, _ReceivedDate, cmbStatus.Text.Trim, txtCvx.Text.Trim, txtMvx.Text.Trim, txtTradeName.Text.Trim, txtLotNo.Text.Trim, _expiryDate, Val(txtVials.Text.Trim), Val(txtDosespervial.Text.Trim), Val(txtDosesOnHand.Text.Trim), txtVIS.Text.Trim, _PublicationDate, DiagnosisCode.Trim, _NDCCode, cmbFundingSource.Text.Trim, txtComments.Text.Trim, _EditID, _DocumentID, cmbLocation.Text.ToString().Trim())

            If _ReturnID <> 0 Then

                txtCount.Text = ""
                cmbCPT.DataSource = Nothing

                If IsNothing(MyOwner) = False Then
                    Dim frm As frmIM_View

                    frm = CType(MyOwner, frmIM_View)
                    If IsNothing(frm) = False Then

                        frm.RefreshCategory()

                    End If
                End If
                _isSaveClicked = True
                _isClose = True
                Me.Close()

            Else
                _isSaveClicked = True
                _isClose = True
                Me.Close()
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oDM) = False Then
                oDM = Nothing
            End If
        End Try
    End Sub


    Private Sub tlsImmunization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsImmunization.ItemClicked
        tlsImmunization.Select()
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveImmunization()

            Case "Close"
                Me.Close()

        End Select
    End Sub

    Private Sub btnBrowseCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCPT.Click
        Try
            'Code Added By Dipak 20090910 For Display List Control on click of CPTBrowseCPT button
            ofrmList = New frmViewListControl
            Dim arrCPTTextSplit As String()
            oListControl = New gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, True, Me.Width)
            oListControl.ControlHeader = "CPT"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            ofrmList.Controls.Add(oListControl)
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            For i As Integer = 0 To cmbCPT.Items.Count - 1
                cmbCPT.SelectedIndex = i
                oListControl.SelectedItems.Add(0, cmbCPT.Text, "")
            Next
            oListControl.ShowHeaderPanel(False)
            oListControl.OpenControl()
            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmList.Text = "CPT"
            ofrmList.ShowDialog()

            If IsNothing(ofrmList) = False Then
                ofrmList.Dispose()
                ofrmList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'code added by dipak 20090910 for add all selected code in dataTable and Bind that datable to cmbCPT
            Dim dtCPTCode As DataTable
            Dim ToList As gloGeneralItem.gloItems
            dtCPTCode = New DataTable
            Dim dcID As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Code")
            dtCPTCode.Columns.Add(dcID)
            dtCPTCode.Columns.Add(dcDescription)
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem
            If oListControl.SelectedItems.Count > 0 Then
                If oListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 CPT", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oListControl.CloseOnDoubleClick = False
                Else
                    For i As Int16 = 0 To oListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtCPTCode.NewRow()
                        drTemp("ID") = oListControl.SelectedItems(i).ID
                        drTemp("Code") = oListControl.SelectedItems(i).Code
                        dtCPTCode.Rows.Add(drTemp)
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oListControl.SelectedItems(i).ID
                        ToItem.Description = oListControl.SelectedItems(i).Code
                        ToList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbCPT.Items.Clear()
                    For i As Int16 = 0 To dtCPTCode.Rows.Count - 1
                        If cmbCPT.Items.Contains(dtCPTCode.Rows(i)("Code").ToString().Trim) Then
                        Else
                            cmbCPT.Items.Add(dtCPTCode.Rows(i)("Code").ToString().Trim)
                        End If

                    Next
                    cmbCPT.SelectedIndex = 0
                    ' cmbCPT.DataSource = dtCPTCode
                    cmbCPT.ValueMember = dtCPTCode.Columns("ID").ColumnName
                    cmbCPT.DisplayMember = dtCPTCode.Columns("Code").ColumnName
                    ofrmList.Close()
                End If
            Else
                ''Added Rahul for Fixed BugID 6726 on 20101129
                cmbCPT.DataSource = Nothing
                cmbCPT.Items.Clear()
                ofrmList.Close()
                ''End
            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'end dipak
    End Sub
    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmList.Close()
        If IsNothing(ofrmList) = False Then
            ofrmList = Nothing
        End If
    End Sub
#Region "Diagnosis control Added:Mayuri:20120112"


    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim dtICD9Code As DataTable
            Dim ToList As gloGeneralItem.gloItems
            dtICD9Code = New DataTable
            Dim dcID As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Code")
            dtICD9Code.Columns.Add(dcID)
            dtICD9Code.Columns.Add(dcDescription)
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem
            If oDiagnosisListControl.SelectedItems.Count > 0 Then
                If oDiagnosisListControl.SelectedItems.Count > 5 Then
                    MessageBox.Show("Select only 5 Diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oDiagnosisListControl.CloseOnDoubleClick = False
                Else
                    For i As Int16 = 0 To oDiagnosisListControl.SelectedItems.Count - 1
                        Dim drTemp As DataRow = dtICD9Code.NewRow()
                        drTemp("ID") = oDiagnosisListControl.SelectedItems(i).ID
                        drTemp("Code") = oDiagnosisListControl.SelectedItems(i).Code
                        dtICD9Code.Rows.Add(drTemp)
                        ToItem = New gloGeneralItem.gloItem()
                        ToItem.ID = oDiagnosisListControl.SelectedItems(i).ID
                        ToItem.Description = oDiagnosisListControl.SelectedItems(i).Code
                        ToList.Add(ToItem)
                        ToItem = Nothing
                    Next
                    cmbDiagnosis.DataSource = dtICD9Code
                    cmbDiagnosis.ValueMember = dtICD9Code.Columns("ID").ColumnName
                    cmbDiagnosis.DisplayMember = dtICD9Code.Columns("Code").ColumnName
                    ofrmDiagnosisList.Close()
                End If
            Else

                cmbDiagnosis.DataSource = Nothing
                cmbDiagnosis.Items.Clear()
                ofrmDiagnosisList.Close()

            End If

        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        ofrmDiagnosisList.Close()
        If IsNothing(ofrmDiagnosisList) = False Then
            ofrmDiagnosisList = Nothing
        End If
    End Sub
    Private Sub btnClearCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.Click
        Try


            If cmbCPT.SelectedIndex >= 0 Then
                If cmbCPT.Items.Count = 1 Then
                    cmbCPT.Items.RemoveAt(cmbCPT.SelectedIndex)
                Else
                    cmbCPT.Items.RemoveAt(cmbCPT.SelectedIndex)
                    cmbCPT.SelectedIndex = 0
                    'Dim dtCPTCode As DataTable
                    'dtCPTCode = cmbCPT.DataSource
                    'dtCPTCode.Rows(cmbCPT.SelectedIndex).Delete()
                    'cmbCPT.Refresh()
                    'cmbCPT.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
#End Region
    'Private Sub btnBrowseCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnBrowseCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnBrowseCPT.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnBrowseCPT, "Select CPT")
    'End Sub

    Private Sub btnBrowseCPT_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnBrowseCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnBrowseCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    'Private Sub btnClearCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnClearCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnClearCPT.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnClearCPT, "Clear CPT")
    'End Sub

    Private Sub btnClearCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnClearCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnClearCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVaccineCode.Click

    End Sub


    Private Sub DisposedGlobal()     'obj Disposed by mitesh
        If Not IsNothing(oListControl) Then
            RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            oListControl.Dispose()
            oListControl = Nothing
        End If
        If IsNothing(dsVaccineInformation) = False Then
            dsVaccineInformation.Dispose()
            dsVaccineInformation = Nothing
        End If
        If Not IsNothing(ofrmList) Then
            ofrmList.Dispose()
            ofrmList = Nothing
        End If
    End Sub


    Private Sub btnVaccineType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVaccineType.Click

    End Sub

    Private Sub btnClearVaccineType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearVaccineType.Click

    End Sub



    'Private Sub btnDiagnosis_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnDiagnosis, "Select Diagnosis")

    'End Sub

    Private Sub btnDiagnosis_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDiaCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiaCancel.Click
        If cmbDiagnosis.SelectedIndex >= 0 Then
            If IsNothing(cmbDiagnosis.DataSource) Then
                cmbDiagnosis.Items.RemoveAt(cmbDiagnosis.SelectedIndex)
            Else
                Dim dtCPTCode As DataTable
                dtCPTCode = cmbDiagnosis.DataSource
                dtCPTCode.Rows(cmbDiagnosis.SelectedIndex).Delete()
                cmbDiagnosis.Refresh()
            End If
        End If
    End Sub

    'Private Sub btnDiaCancel_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    btnDiaCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnDiaCancel.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnDiaCancel, "Clear Diagnosis")
    'End Sub

    Private Sub btnDiaCancel_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnDiaCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDiaCancel.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnosis.Click
        Try
            'Code Added By Dipak 20090910 For Display List Control on click of CPTBrowseCPT button
            ofrmDiagnosisList = New frmViewListControl
            Dim arrCPTTextSplit As String()
            oDiagnosisListControl = New gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Diagnosis, True, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            AddHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
            AddHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
            ofrmDiagnosisList.Controls.Add(oDiagnosisListControl)
            oDiagnosisListControl.Dock = DockStyle.Fill
            oDiagnosisListControl.BringToFront()
            'code make all CPT Code Selected in ListControl  Which are present in cmbCPT
            For i As Integer = 0 To cmbDiagnosis.Items.Count - 1
                cmbDiagnosis.SelectedIndex = i
                oDiagnosisListControl.SelectedItems.Add(0, cmbDiagnosis.Text, "")
            Next
            oDiagnosisListControl.ShowHeaderPanel(False)
            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog()

            If IsNothing(ofrmDiagnosisList) = False Then
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtVials_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVials.KeyPress
        AllowDecimal(txtVials.Text, e)
    End Sub


    ''Added by MAYURI:20120104
    ''Vial Calculation
    Private Sub txtVials_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVials.TextChanged
        If Len(txtVials.Text) > 0 And Len(txtDosespervial.Text) > 0 Then
            txtDosesOnHand.Text = Format(Val(txtVials.Text * txtDosespervial.Text), "#0.00")
        Else
            txtDosesOnHand.Text = ""

        End If
    End Sub

    ''End Vial Calculation


    'End Sub

    Private Sub txtDosespervial_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDosespervial.TextChanged
        Try
            If Len(txtVials.Text) > 0 And Len(txtDosespervial.Text) > 0 Then
                txtDosesOnHand.Text = Format(Val(txtVials.Text * txtDosespervial.Text), "#0.00")
            Else
                txtDosesOnHand.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub




    Private Sub txtSKU_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSKU.Leave

    End Sub
    Private Function AssignCodes(ByVal _Type As String) As Boolean
        Dim oDM As New gloStream.Immunization.ItemSetup
        If _Type = "First" Then
            _labellercode1 = _NDC.Substring(0, 5)
            _productcode1 = _NDC.Substring(5, 3)
            If _NDC.Length < 11 Then
                _packageCode1 = _NDC.Substring(8, Len(_NDC) - (Len(_labellercode1) + Len(_productcode1)))
            Else
                _packageCode1 = _NDC.Substring(8, 2)
            End If

            _productcode1 = "0" & _productcode1
            If oDM.IsExistsLabellerCode(_labellercode1, _productcode1) Then
                Return True
            Else
                Return False
            End If
        ElseIf _Type = "Second" Then
            _labellercode1 = _NDC.Substring(0, 5)
            _productcode1 = _NDC.Substring(5, 3)
            If _NDC.Length < 11 Then
                _packageCode1 = _NDC.Substring(8, Len(_NDC) - (Len(_labellercode1) + Len(_productcode1)))
            Else
                _packageCode1 = _NDC.Substring(8, 2)
            End If

            _labellercode1 = "0" & _labellercode1


            If oDM.IsExistsLabellerCode(_labellercode1, _productcode1) Then
                Return True
            Else
                Return False
            End If
        End If


    End Function


    Private Sub txtDosesOnHand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosesOnHand.KeyPress
        AllowDecimal(txtDosesOnHand.Text, e)
    End Sub

    Private Sub txtDosespervial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosespervial.KeyPress
        AllowDecimal(txtDosespervial.Text, e)
    End Sub

    Private Sub txtNDCCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNDCCode.KeyPress
        AllowNumericValue(txtNDCCode.Text, e)
    End Sub

    Private Sub txtNDCCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNDCCode.Validating
        Try

            Dim txt As New TextBox
            txt = sender
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub



    Private Sub txtDosesOnHand_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDosesOnHand.Validating
        Try
            Dim txt As New TextBox
            txt = sender
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtDosespervial_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDosespervial.Validating
        Try
            Dim txt As New TextBox
            txt = sender
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtVials_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtVials.Validating
        Try
            Dim txt As New TextBox
            txt = sender
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub txtSKUSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSKUSearch.KeyPress
        'AllowNumericValue(txtSKUSearch.Text, e)
    End Sub


    Private Sub txtSKUSearch_SearchFired() Handles txtSKUSearch.SearchFired
        GetNDCFromSKU()
        GetCPTCodeFromCVX()
        'Dim oDM As New gloStream.Immunization.ItemSetup
        'Try
        '    If _isFormLoadModify = True Then
        '        _isFormLoadModify = False
        '        Exit Sub
        '    End If

        '    _NDC = txtSKUSearch.Text.Trim

        '    Dim _IsNDCCodeFound As Boolean = False
        '    Dim _CVX As String = ""
        '    Dim _ShortName As String = ""
        '    Dim _MVX As String = ""
        '    Dim _MVXName As String = ""
        '    Dim _tradeName As String = ""
        '    Dim _iscontinue As Boolean = False

        '    If _NDC <> "" Then

        '        If _NDC.Contains("-") Then
        '            _NDC = _NDC.Replace("-", "")
        '        End If
        '        If IsNumeric(_NDC) = False Then
        '            ClearFields()
        '            Exit Sub
        '        End If
        '        If _NDC.Length >= 10 Then
        '            If _NDC.Length = 12 Then
        '                If _NDC.StartsWith("3") Then
        '                    _NDC = _NDC.Substring(1, Len(_NDC.Trim) - 1)
        '                End If
        '            ElseIf _NDC.Length = 14 Then
        '                If _NDC.StartsWith("003") Then
        '                    _NDC = _NDC.Substring(3, Len(_NDC.Trim) - 3)
        '                End If
        '            End If

        '            _labellercode1 = _NDC.Substring(0, 5)
        '            _productcode1 = _NDC.Substring(5, 4)
        '            If oDM.IsExistsLabellerCode(_labellercode1.Trim, _productcode1.Trim) Then
        '                If _NDC.Length < 11 Then
        '                    _packageCode1 = _NDC.Substring(9, Len(_NDC) - (Len(_labellercode1) + Len(_productcode1)))
        '                Else
        '                    _packageCode1 = _NDC.Substring(9, 2)
        '                End If

        '                If Len(_packageCode1) = 1 Then
        '                    _packageCode1 = "0" & _packageCode1.Trim
        '                Else
        '                    _packageCode1 = _packageCode1.Trim
        '                End If
        '                _iscontinue = True
        '            ElseIf AssignCodes("First") Then

        '                _iscontinue = True
        '            ElseIf AssignCodes("Second") Then

        '                _iscontinue = True
        '            End If


        '            If _iscontinue = True Then


        '                Dim dtNDCCode As New DataTable
        '                dtNDCCode = oDM.getNDCCode(_labellercode1, _productcode1)
        '                Dim _NDCCodecollection() As String
        '                Dim _packagecodecollection() As String
        '                If IsNothing(dtNDCCode) = False Then
        '                    If dtNDCCode.Rows.Count > 0 Then
        '                        ClearFields()
        '                        _NDCCodecollection = dtNDCCode.Rows(0)(0).ToString().Split(",")
        '                        If _NDCCodecollection.Length > 0 Then
        '                            For i As Int16 = 0 To _NDCCodecollection.Length - 1
        '                                If _NDCCodecollection(i).ToString().Trim.Replace("-", "") = (_labellercode1.Trim & _productcode1.Trim) Then
        '                                    _IsNDCCodeFound = True
        '                                    _NDC = _NDCCodecollection(i).ToString().Trim() & _packageCode1.Trim()
        '                                    _CVX = dtNDCCode.Rows(0)("CvxCode").ToString.Trim
        '                                    _ShortName = dtNDCCode.Rows(0)("ShortDescription").ToString.Trim
        '                                    _MVX = dtNDCCode.Rows(0)("MvxCode").ToString.Trim
        '                                    _MVXName = dtNDCCode.Rows(0)("ManufacturerName").ToString.Trim
        '                                    _tradeName = dtNDCCode.Rows(0)("CdcProductName").ToString.Trim

        '                                    txtNDCCode.Text = _NDC

        '                                End If

        '                            Next
        '                        Else
        '                            ClearFields()

        '                        End If
        '                    Else
        '                        ClearFields()

        '                    End If
        '                End If


        '                If _CVX <> "" Then

        '                    _isLoadGridCvxControl = True

        '                    txtCvx.Text = _CVX & " - " & _ShortName
        '                    _isLoadGridCvxControl = False

        '                End If
        '                If _MVX <> "" Then

        '                    _isLoadGridCvxControl = True

        '                    txtMvx.Text = _MVX & " - " & _MVXName
        '                    _isLoadGridCvxControl = False

        '                End If
        '                If _tradeName <> "" Then

        '                    _isLoadGridCvxControl = True

        '                    txtTradeName.Text = _tradeName
        '                    _isLoadGridCvxControl = False

        '                End If

        '                Dim dtinform As New DataTable
        '                dtinform = oDM.getInformation(_CVX)
        '                '  cmbCPT.Items.Clear()
        '                If IsNothing(dtinform) = False Then
        '                    If dtinform.Rows.Count > 0 Then
        '                        For i As Int16 = 0 To dtinform.Rows.Count - 1
        '                            If cmbCPT.Items.Contains(dtinform.Rows(i)(0).ToString().Trim) Then
        '                            Else
        '                                If oDM.IsExistsCPTCode(dtinform.Rows(i)(0).ToString().Trim) Then
        '                                    cmbCPT.Items.Add(dtinform.Rows(i)(0).ToString().Trim)
        '                                    cmbCPT.Text = dtinform.Rows(i)(0).ToString().Trim
        '                                Else
        '                                    ''Add record of CPT in CPT master
        '                                End If
        '                            End If
        '                        Next
        '                    End If
        '                End If
        '            End If
        '            Dim dtndccodelist As New DataTable
        '            Dim _labellerandproductcode As String = ""
        '            _labellerandproductcode = _NDC.Substring(0, 9)
        '            ''Get NDCCode From MMW database
        '            If _IsNDCCodeFound = False Then
        '                cmbCPT.Items.Clear()
        '                _isLoadGridCvxControl = True

        '                txtCvx.Text = ""

        '                txtMvx.Text = ""

        '                txtTradeName.Text = ""
        '                _isLoadGridCvxControl = False

        '                txtNDCCode.Text = ""
        '                dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
        '                If IsNothing(dtndccodelist) = False Then
        '                    For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
        '                        If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _NDC.Trim Then
        '                            txtNDCCode.Text = _NDC.Trim
        '                            _IsNDCCodeFound = True
        '                            Exit For
        '                        Else
        '                            txtNDCCode.Text = ""
        '                        End If
        '                    Next
        '                End If

        '                If _IsNDCCodeFound = False Then
        '                    dtndccodelist = Nothing
        '                    Dim _labelcode As String = ""
        '                    Dim _productcode As String = ""
        '                    Dim _packageCode As String = ""
        '                    _labelcode = _NDC.Substring(0, 5)
        '                    _productcode = _NDC.Substring(5, 3)
        '                    _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
        '                    _labelcode = "0" & _labelcode
        '                    _labellerandproductcode = _labelcode.Trim & _productcode.Trim
        '                    dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)

        '                    If IsNothing(dtndccodelist) = False Then
        '                        For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
        '                            If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
        '                                txtNDCCode.Text = _labellerandproductcode.Trim & _packageCode.Trim
        '                                _IsNDCCodeFound = True
        '                                Exit For
        '                            Else
        '                                txtNDCCode.Text = ""
        '                            End If
        '                        Next
        '                    End If
        '                End If

        '                If _IsNDCCodeFound = False Then
        '                    dtndccodelist = Nothing
        '                    Dim _labelcode As String = ""
        '                    Dim _productcode As String = ""
        '                    Dim _packageCode As String = ""
        '                    _labelcode = _NDC.Substring(0, 5)
        '                    _productcode = _NDC.Substring(5, 3)
        '                    _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
        '                    _productcode = "0" & _productcode
        '                    _labellerandproductcode = _labelcode.Trim & _productcode.Trim
        '                    dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
        '                    If IsNothing(dtndccodelist) = False Then
        '                        For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
        '                            If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
        '                                txtNDCCode.Text = _labellerandproductcode.Trim & _packageCode.Trim
        '                                _IsNDCCodeFound = True
        '                                Exit For
        '                            Else
        '                                txtNDCCode.Text = ""
        '                            End If
        '                        Next
        '                    End If
        '                End If
        '            End If
        '        Else
        '            'If _EditID = 0 Then
        '            ClearFields()
        '            ' End If
        '        End If
        '    Else
        '        ClearFields()
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        'End Try
    End Sub
    Private Function GetNDCFromSKU()
        Dim oDM As New gloStream.Immunization.ItemSetup
        Try
           

            _NDC = txtSKUSearch.Text.Trim

            Dim _IsNDCCodeFound As Boolean = False
            If _isLoad = True Then
                _isLoad = False
                Exit Function
            End If

            If _NDC <> "" Then
               
                If _NDC.Contains("-") Then
                    _NDC = _NDC.Replace("-", "")
                End If
                If _isFormLoadModify = False Then
                    ClearFields()
                    If IsNumeric(_NDC) = False Then
                        ClearFields()
                        Exit Function
                    End If
                End If

                If _NDC.Length >= 10 Then
                    If _NDC.Length = 12 Or _NDC.Length = 11 Then
                        If _NDC.StartsWith("3") Then
                            _NDC = _NDC.Substring(1, Len(_NDC.Trim) - 1)
                        End If
                    ElseIf _NDC.Length = 14 Or _NDC.Length = 13 Then
                        If _NDC.StartsWith("003") Then
                            _NDC = _NDC.Substring(3, Len(_NDC.Trim) - 3)
                        End If
                    End If
                    Dim ds As DataSet
                    ds = oDM.getNDCCode1(_NDC)
                    If IsNothing(ds) = False Then
                        'If IsNothing(ds.Tables("CPT")) = False Then
                        '    If ds.Tables("CPT").Rows.Count > 0 Then
                        '        cmbCPT.Items.Clear()
                        '        For i As Int16 = 0 To ds.Tables("CPT").Rows.Count - 1

                        '            If cmbCPT.Items.Contains(ds.Tables("CPT").Rows(i)(0).ToString().Trim) Then
                        '            Else
                        '                If oDM.IsExistsCPTCode(ds.Tables("CPT").Rows(i)(0).ToString().Trim) Then
                        '                    cmbCPT.Items.Add(ds.Tables("CPT").Rows(i)(0).ToString().Trim)
                        '                    cmbCPT.Text = ds.Tables("CPT").Rows(i)(0).ToString().Trim
                        '                Else
                        '                    ''Add record of CPT in CPT master
                        '                End If
                        '            End If
                        '        Next
                        '    End If
                        'End If
                        If IsNothing(ds.Tables("NDCInfo")) = False Then


                            If ds.Tables("NDCInfo").Rows.Count > 0 Then
                                _isLoadGridCvxControl = True

                                txtCvx.Text = ds.Tables("NDCInfo").Rows(0)("CVX").ToString().Trim
                                txtMvx.Text = ds.Tables("NDCInfo").Rows(0)("MVX").ToString().Trim
                                txtTradeName.Text = ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim
                                txtNDCCode.Text = ds.Tables("NDCInfo").Rows(0)("NDCCode").ToString().Trim
                                _IsNDCCodeFound = True
                                _isLoadGridCvxControl = False


                            End If
                        End If
                    End If
                    If _IsNDCCodeFound = False Then
                        _NDC = txtSKUSearch.Text.Trim
                        If _NDC <> "" Then
                            If _NDC.Contains("-") Then
                                _NDC = _NDC.Replace("-", "")
                            End If
                            If _NDC.Length = 11 Then
                                If _NDC.StartsWith("3") Then


                                    ds = oDM.getNDCCode1(_NDC)
                                    If IsNothing(ds) = False Then
                                        If IsNothing(ds.Tables("CPT")) = False Then
                                            'If ds.Tables("CPT").Rows.Count > 0 Then
                                            '    cmbCPT.Items.Clear()
                                            '    For i As Int16 = 0 To ds.Tables("CPT").Rows.Count - 1

                                            '        If cmbCPT.Items.Contains(ds.Tables("CPT").Rows(i)(0).ToString().Trim) Then
                                            '        Else
                                            '            If oDM.IsExistsCPTCode(ds.Tables("CPT").Rows(i)(0).ToString().Trim) Then
                                            '                cmbCPT.Items.Add(ds.Tables("CPT").Rows(i)(0).ToString().Trim)
                                            '                cmbCPT.Text = ds.Tables("CPT").Rows(i)(0).ToString().Trim
                                            '            Else
                                            '                ''Add record of CPT in CPT master
                                            '            End If
                                            '        End If
                                            '    Next
                                            'End If
                                            If ds.Tables("NDCInfo").Rows.Count > 0 Then
                                                _isLoadGridCvxControl = True

                                                txtCvx.Text = ds.Tables("NDCInfo").Rows(0)("CVX").ToString().Trim
                                                txtMvx.Text = ds.Tables("NDCInfo").Rows(0)("MVX").ToString().Trim
                                                txtTradeName.Text = ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim
                                                txtNDCCode.Text = ds.Tables("NDCInfo").Rows(0)("NDCCode").ToString().Trim
                                                _IsNDCCodeFound = True
                                                _isLoadGridCvxControl = False


                                            End If
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    End If
                    ''MMW 
                    Dim dtndccodelist As New DataTable
                    Dim _labellerandproductcode As String = ""
                    _labellerandproductcode = _NDC.Substring(0, 9)
                    ''Get NDCCode From MMW database
                    If _IsNDCCodeFound = False Then
                        If _isFormLoadModify = False Then


                            cmbCPT.Items.Clear()
                            _isLoadGridCvxControl = True

                            txtCvx.Text = ""

                            txtMvx.Text = ""

                            txtTradeName.Text = ""
                            _isLoadGridCvxControl = False

                            txtNDCCode.Text = ""
                        End If
                        dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
                        If IsNothing(dtndccodelist) = False Then
                            For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
                                If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _NDC.Trim Then
                                    txtNDCCode.Text = _NDC.Trim
                                    _IsNDCCodeFound = True
                                    Exit For
                                Else
                                    txtNDCCode.Text = ""
                                End If
                            Next
                        End If

                        If _IsNDCCodeFound = False Then
                            dtndccodelist = Nothing
                            Dim _labelcode As String = ""
                            Dim _productcode As String = ""
                            Dim _packageCode As String = ""
                            _labelcode = _NDC.Substring(0, 5)
                            _productcode = _NDC.Substring(5, 3)
                            _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
                            _labelcode = "0" & _labelcode
                            _labellerandproductcode = _labelcode.Trim & _productcode.Trim
                            dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)

                            If IsNothing(dtndccodelist) = False Then
                                For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
                                    If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
                                        txtNDCCode.Text = _labellerandproductcode.Trim & _packageCode.Trim
                                        _IsNDCCodeFound = True
                                        Exit For
                                    Else
                                        txtNDCCode.Text = ""
                                    End If
                                Next
                            End If
                        End If

                        If _IsNDCCodeFound = False Then
                            dtndccodelist = Nothing
                            Dim _labelcode As String = ""
                            Dim _productcode As String = ""
                            Dim _packageCode As String = ""
                            _labelcode = _NDC.Substring(0, 5)
                            _productcode = _NDC.Substring(5, 3)
                            _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
                            _productcode = "0" & _productcode
                            _labellerandproductcode = _labelcode.Trim & _productcode.Trim
                            dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
                            If IsNothing(dtndccodelist) = False Then
                                For i As Int16 = 0 To dtndccodelist.Rows.Count - 1
                                    If dtndccodelist.Rows(i)(0).ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
                                        txtNDCCode.Text = _labellerandproductcode.Trim & _packageCode.Trim
                                        _IsNDCCodeFound = True
                                        Exit For
                                    Else
                                        txtNDCCode.Text = ""
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else
                    If _isFormLoadModify = False Then
                        ClearFields()
                    End If
                End If
            Else
                If _isFormLoadModify = False Then
                    ClearFields()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Private Function CheckTradeNameisValidagainstSKU() As Boolean
        Dim oDM As New gloStream.Immunization.ItemSetup
        Dim _result As Boolean = True
        Try
            _NDC = txtSKUSearch.Text.Trim
            If _NDC <> "" Then
                If _NDC.Contains("-") Then
                    _NDC = _NDC.Replace("-", "")
                End If

                If _NDC.Length >= 10 Then
                    If _NDC.Length = 12 Or _NDC.Length = 11 Then
                        If _NDC.StartsWith("3") Then
                            _NDC = _NDC.Substring(1, Len(_NDC.Trim) - 1)
                        End If
                    ElseIf _NDC.Length = 14 Or _NDC.Length = 13 Then
                        If _NDC.StartsWith("003") Then
                            _NDC = _NDC.Substring(3, Len(_NDC.Trim) - 3)
                        End If
                    End If
                    Dim ds As DataSet
                    ds = oDM.getNDCCode1(_NDC)
                    If IsNothing(ds) = False Then
                        If IsNothing(ds.Tables("NDCInfo")) = False Then

                            If ds.Tables("NDCInfo").Rows.Count > 0 Then

                                If txtTradeName.Text <> ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim Then

                                    If MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txtTradeName.Text.Trim & "'" & " does not match the SKU Given " & "'" & txtSKUSearch.Text.Trim & "'" & Chr(13) & Chr(13) & "Please verify that the SKU from the packaging has been entered correctly and choose one of the following:" & Chr(13) & Chr(13) & "YES – Save this vaccine entry matching " & "'" & txtTradeName.Text.Trim & "'" & " to the SKU " & "'" & txtSKUSearch.Text.Trim & "'" & Chr(13) & Chr(13) & "NO - Continue to edit this vaccine", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                                        _result = True
                                        Return _result
                                    Else
                                        _result = False
                                        Return _result
                                    End If
                                End If
                            End If
                        End If
                    End If
                    ''
                    '  If _result = False Then


                    _NDC = txtSKUSearch.Text.Trim
                    If _NDC <> "" Then
                        If _NDC.Contains("-") Then
                            _NDC = _NDC.Replace("-", "")
                        End If
                        If _NDC.Length = 11 Then
                            If _NDC.StartsWith("3") Then
                                ds = oDM.getNDCCode1(_NDC)
                                If IsNothing(ds) = False Then
                                    If IsNothing(ds.Tables("NDCInfo")) = False Then

                                        If ds.Tables("NDCInfo").Rows.Count > 0 Then

                                            If txtTradeName.Text <> ds.Tables("NDCInfo").Rows(0)("CdcProductName").ToString().Trim Then

                                                If MessageBox.Show("Based on the CDC Vaccine Database,  the Trade Name " & "'" & txtTradeName.Text.Trim & "'" & " does not match the SKU Given " & "'" & txtSKUSearch.Text.Trim & "'" & Chr(13) & Chr(13) & "Please verify that the SKU from the packaging has been entered correctly and choose one of the following:" & Chr(13) & Chr(13) & "YES – Save this vaccine entry matching " & "'" & txtTradeName.Text.Trim & "'" & " to the SKU " & "'" & txtSKUSearch.Text.Trim & "'" & Chr(13) & Chr(13) & "NO - Continue to edit this vaccine", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                                    _result = True

                                                Else
                                                    _result = False
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    'End If
                    ''
                End If
            End If
            Return _result
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Function
    Private Sub ClearFields()
        cmbCPT.Items.Clear()
        _isLoadGridCvxControl = True

        txtCvx.Text = ""

        txtMvx.Text = ""

        txtTradeName.Text = ""
        _isLoadGridCvxControl = False

        txtNDCCode.Text = ""
    End Sub

    Private Sub txtCvx_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCvx.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oCVXControl.GetCurrentSelectedItem()
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        oCVXControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlCvxControl.Visible Then
                    If oCVXControl IsNot Nothing Then
                        oCvxControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtCvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCvx.LostFocus
        If pnlCvxControl.Visible Then
            If oCVXControl IsNot Nothing Then
                If oCVXControl.Focus() = False Then
                    If oCVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Cvx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCvx_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCvx.TextChanged
        Try
            txtNDCCode.Text = ""
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtCvx.Text
                If (_strSearchString.Trim() <> "") Then
                    If IsNothing(oCVXControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
                    Else
                        If oCVXControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
                        End If
                    End If

                    If Not IsNothing(oCVXControl) Then
                        oCVXControl.FillControl(_strSearchString)
                    End If
                Else


                    cmbCPT.Items.Clear()
                  
                End If
            End If

            If IsNothing(oCVXControl) = False Then
                If oCVXControl.C1GridList.Focused Then
                    SetCursorPos(txtCvx.Left + Me.Left + 270, txtCvx.Top + Me.Top + 70)
                End If
                RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
                txtCvx.Focus()
                txtCvx.SelectionStart = Len(txtCvx.Text)
                AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
            End If


            ' _isLoadGridCvxControl = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenProcedureControl(ByVal ControlType As gloUserControlLibrary.gloGridListControlType, ByVal ControlHeader As String, ByVal SearchText As String)

        Try
            If ControlType = gloUserControlLibrary.gloGridListControlType.Cvx Then
                If oCVXControl IsNot Nothing Then
                    CloseProcedureControl("Cvx")
                End If
                oCVXControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.Cvx, True, 100)
                oCVXControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oCVXControl.ItemSelected, AddressOf oCvxControl_ItemSelected
                AddHandler oCVXControl.InternalGridKeyDown, AddressOf oCvxControl_InternalGridKeyDown
                AddHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
                AddHandler oCVXControl.C1GridList.MouseMove, AddressOf oCvxControl_MouseMove
                oCVXControl.ImmunizationName = txtCvx.Text.Trim()
                oCVXControl.ShowHeader = False
                oCVXControl.ControlHeader = ControlHeader
                pnlCvxControl.Controls.Add(oCVXControl)
                oCVXControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oCVXControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oCVXControl.Show()
                pnlCvxControl.Visible = True
                RaiseEvent GridListLoaded()
                pnlCvxControl.BringToFront()
            ElseIf ControlType = gloUserControlLibrary.gloGridListControlType.Mvx Then
                If oMVXControl IsNot Nothing Then
                    CloseProcedureControl("Mvx")
                End If
                oMVXControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.Mvx, True, 100)
                oMVXControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oMVXControl.ItemSelected, AddressOf oMvxControl_ItemSelected
                AddHandler oMVXControl.InternalGridKeyDown, AddressOf oMvxControl_InternalGridKeyDown
                AddHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                AddHandler oMVXControl.C1GridList.MouseMove, AddressOf oMVXControl_MouseMove
                oMVXControl.ImmunizationName = txtMvx.Text.Trim()
                oMVXControl.ControlHeader = ControlHeader
                oMVXControl.ShowHeader = False
                pnlMvxControl.Controls.Add(oMVXControl)
                oMVXControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oMVXControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oMVXControl.Show()
                pnlMvxControl.Visible = True
                RaiseEvent GridListLoaded1()
                pnlMvxControl.BringToFront()
            ElseIf ControlType = gloUserControlLibrary.gloGridListControlType.TradeName Then
                If oTradeNameControl IsNot Nothing Then
                    CloseProcedureControl("TradeName")
                End If
                oTradeNameControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.TradeName, True, 100)
                oTradeNameControl.DatabaseConnectionString = GetConnectionString()
                AddHandler oTradeNameControl.ItemSelected, AddressOf oTradeNameControl_ItemSelected
                AddHandler oTradeNameControl.InternalGridKeyDown, AddressOf oTradeNameControl_InternalGridKeyDown
                AddHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                AddHandler oTradeNameControl.C1GridList.MouseMove, AddressOf oTradeNameControl_MouseMove

                oTradeNameControl.ImmunizationName = txtTradeName.Text.Trim()
                oTradeNameControl.ControlHeader = ControlHeader
                oTradeNameControl.ShowHeader = False
                pnlTradeNameControl.Controls.Add(oTradeNameControl)
                oTradeNameControl.Dock = DockStyle.Fill
                If SearchText <> "" Then
                    oTradeNameControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
                End If
                oTradeNameControl.Show()
                pnlTradeNameControl.Visible = True
                RaiseEvent GridListLoaded2()
                pnlTradeNameControl.BringToFront()
            End If


        Catch ex As Exception
            '  gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            _
        Finally

        End Try

    End Sub
    Private Sub CloseProcedureControl(ByVal _controlName As String)

        Try
            If _controlName = "Cvx" Then
                For i As Integer = 0 To pnlCvxControl.Controls.Count - 1
                    pnlCvxControl.Controls.RemoveAt(i)
                Next
                If oCVXControl IsNot Nothing Then
                    RemoveHandler oCVXControl.ItemSelected, AddressOf oCvxControl_ItemSelected
                    RemoveHandler oCVXControl.InternalGridKeyDown, AddressOf oCvxControl_InternalGridKeyDown
                    RemoveHandler oCVXControl.InternalGridLostFocus, AddressOf oCvxControl_InternalGridLostFocus
                    RemoveHandler oCVXControl.C1GridList.MouseMove, AddressOf oCvxControl_MouseMove

                    oCVXControl.Dispose()
                    oCVXControl = Nothing
                End If
                pnlCvxControl.Visible = False
                RaiseEvent GridListClosed()
                pnlCvxControl.SendToBack()
            ElseIf _controlName = "Mvx" Then
                For i As Integer = 0 To pnlMvxControl.Controls.Count - 1
                    pnlMvxControl.Controls.RemoveAt(i)
                Next
                If oMVXControl IsNot Nothing Then
                    RemoveHandler oMVXControl.ItemSelected, AddressOf oMvxControl_ItemSelected
                    RemoveHandler oMVXControl.InternalGridKeyDown, AddressOf oMvxControl_InternalGridKeyDown
                    RemoveHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                    RemoveHandler oMVXControl.C1GridList.MouseMove, AddressOf oMVXControl_MouseMove

                    oMVXControl.Dispose()
                    oMVXControl = Nothing
                End If
                pnlMvxControl.Visible = False
                RaiseEvent GridListClosed1()
                pnlMvxControl.SendToBack()
            ElseIf _controlName = "TradeName" Then
                For i As Integer = 0 To pnlTradeNameControl.Controls.Count - 1
                    pnlTradeNameControl.Controls.RemoveAt(i)
                Next
                If oTradeNameControl IsNot Nothing Then
                    RemoveHandler oTradeNameControl.ItemSelected, AddressOf oTradeNameControl_ItemSelected
                    RemoveHandler oTradeNameControl.InternalGridKeyDown, AddressOf oTradeNameControl_InternalGridKeyDown
                    RemoveHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                    RemoveHandler oTradeNameControl.C1GridList.MouseMove, AddressOf oTradeNameControl_MouseMove

                    oTradeNameControl.Dispose()
                    oTradeNameControl = Nothing
                End If
                pnlTradeNameControl.Visible = False
                RaiseEvent GridListClosed2()
                pnlTradeNameControl.SendToBack()
            End If


        Catch ex As Exception
            ''   gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)

        Finally
        End Try

    End Sub
    Private Sub oCvxControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oCVXControl.C1GridList.Select()
    End Sub

    Private Sub oMVXControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oMVXControl.C1GridList.Select()
    End Sub

    Private Sub oTradeNameControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oTradeNameControl.C1GridList.Select()
    End Sub
    Private Sub oCvxControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oCVXControl.SelectedItems IsNot Nothing Then
                If oCVXControl.SelectedItems.Count > 0 Then
                    oProcedure = oCVXControl.SelectedItems(0)
                    Select Case oCVXControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.Cvx
                            txtCvx.Text = oProcedure.Description


                    End Select

                    CloseProcedureControl("Cvx")
                Else

                End If

            End If
            txtCvx.Focus()
            GetCPTCodeFromCVX()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oCvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub oCvxControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlCvxControl.Visible Then
            If oCVXControl IsNot Nothing Then
                If oCVXControl.IsRecordExist(txtCvx.Text.Trim) = False Then
                    txtCvx.Text = ""
                    CloseProcedureControl("Cvx")
                Else
                    CloseProcedureControl("Cvx")
                End If
            End If
        End If

        ' _isLoadGridCvxControl = False
    End Sub


    Private Sub txtMvx_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMvx.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oMVXControl.GetCurrentSelectedItem()
                        'If _IsItemSelected Then

                        'End If
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        oMVXControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlMvxControl.Visible Then
                    If oMVXControl IsNot Nothing Then
                        oMvxControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtMvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMvx.LostFocus
        If pnlMvxControl.Visible Then
            If oMVXControl IsNot Nothing Then
                If oMVXControl.Focus() = False Then
                    If oMVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Mvx")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtMvx_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMvx.TextChanged
        Try
            txtNDCCode.Text = ""
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtMvx.Text
                If (_strSearchString.Trim() <> "") Then
                    If IsNothing(oMVXControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
                    Else
                        If oMVXControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
                        End If
                    End If

                    If Not IsNothing(oMVXControl) Then
                        oMVXControl.FillControl(_strSearchString)
                    End If
                End If
            End If


            If IsNothing(oMVXControl) = False Then
                If oMVXControl.C1GridList.Focused Then
                    SetCursorPos(txtMvx.Left + Me.Left + 270, txtMvx.Top + Me.Top + 70)
                End If
                RemoveHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
                txtMvx.Focus()
                txtMvx.SelectionStart = Len(txtMvx.Text)
                AddHandler oMVXControl.InternalGridLostFocus, AddressOf oMvxControl_InternalGridLostFocus
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oMvxControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oMVXControl.SelectedItems IsNot Nothing Then
                If oMVXControl.SelectedItems.Count > 0 Then
                    oProcedure = oMVXControl.SelectedItems(0)
                    Select Case oMVXControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.Mvx
                            txtMvx.Text = oProcedure.Description


                    End Select

                    CloseProcedureControl("Mvx")
                Else

                End If
            Else

            End If
            txtMvx.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oMvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub oMvxControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlMvxControl.Visible Then
            If oMVXControl IsNot Nothing Then

                If oMVXControl.IsRecordExist(txtMvx.Text.Trim) = False Then
                    txtMvx.Text = ""
                    CloseProcedureControl("Mvx")
                Else
                    CloseProcedureControl("Mvx")
                End If
            End If
        End If
    End Sub

    Private Sub txtTradeName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTradeName.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oTradeNameControl.GetCurrentSelectedItem()
                        'If _IsItemSelected Then

                        'End If
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        oTradeNameControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlTradeNameControl.Visible Then
                    If oTradeNameControl IsNot Nothing Then
                        oTradeNameControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtTradeName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeName.LostFocus
        If pnlTradeNameControl.Visible Then
            If oTradeNameControl IsNot Nothing Then
                If oTradeNameControl.Focus() = False Then
                    If oTradeNameControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("TradeName")
                    End If
                End If
            End If
        End If


    End Sub

    Private Sub txtTradeName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTradeName.TextChanged
        Try
            txtNDCCode.Text = ""
            If _isLoadGridCvxControl = True Then
            Else
                Dim _strSearchString As String = ""
                _strSearchString = txtTradeName.Text.Trim
                If (_strSearchString.Trim() <> "") Then
                    If IsNothing(oTradeNameControl) Then
                        OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
                    Else
                        If oTradeNameControl.Visible = False Then
                            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
                        End If
                    End If

                    If Not IsNothing(oTradeNameControl) Then
                        oTradeNameControl.FillControl(_strSearchString)
                    End If
                Else
                    _isLoadGridCvxControl = True
                    txtCvx.Text = ""
                    cmbCPT.Items.Clear()
                    txtMvx.Text = ""
                    _isLoadGridCvxControl = False
                End If
            End If

            If IsNothing(oTradeNameControl) = False Then
                If oTradeNameControl.C1GridList.Focused Then
                    SetCursorPos(txtTradeName.Left + Me.Left + 270, txtTradeName.Top + Me.Top + 70)
                End If
                RemoveHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
                txtTradeName.Focus()
                txtTradeName.SelectionStart = Len(txtTradeName.Text)
                AddHandler oTradeNameControl.InternalGridLostFocus, AddressOf oTradeNameControl_InternalGridLostFocus
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oTradeNameControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem

            If oTradeNameControl.SelectedItems IsNot Nothing Then
                If oTradeNameControl.SelectedItems.Count > 0 Then
                    oProcedure = oTradeNameControl.SelectedItems(0)
                    Select Case oTradeNameControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.TradeName
                            txtTradeName.Text = oProcedure.Description


                    End Select

                    CloseProcedureControl("TradeName")
                Else

                End If
            Else

            End If
            txtTradeName.Select()
            ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
            GetCVXMVX()
            GetCPTCodeFromCVX()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GetCVXMVX()
        ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim ds As DataSet
        If txtTradeName.Text.Trim() <> "" Then
            ds = oClsIM.GetCVXMvxFromTradeName(txtTradeName.Text.Trim())
            If IsNothing(ds) = False Then
                If IsNothing(ds) = False Then
                    If ds.Tables("Vaccine").Rows.Count > 0 Then
                        _isLoadGridCvxControl = True
                        txtCvx.Text = Convert.ToString(ds.Tables("Vaccine").Rows(0)("Vaccine"))

                        _isLoadGridCvxControl = False
                    End If
                    If ds.Tables("Manufacturer").Rows.Count > 0 Then
                        _isLoadGridCvxControl = True

                        txtMvx.Text = Convert.ToString(ds.Tables("Manufacturer").Rows(0)("Manufacturer"))
                        _isLoadGridCvxControl = False
                    End If
                End If

            End If
        End If
    End Sub
    Private Sub GetCPTCodeFromCVX()
        ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
        cmbCPT.Items.Clear()
        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim dt As DataTable
        If txtCvx.Text.Trim() <> "" Then
            dt = oClsIM.GetCPTFromCVXCode(txtCvx.Text.Trim())
            If IsNothing(dt) = False Then

                If dt.Rows.Count > 0 Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        cmbCPT.Items.Add(dt.Rows(i)("cptcode").ToString().Trim)
                    Next


                End If
                If cmbCPT.Items.Count > 0 Then
                    cmbCPT.SelectedIndex = 0
                End If

            End If
        End If
    End Sub
    Private Sub oTradeNameControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub oTradeNameControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlTradeNameControl.Visible Then
            If oTradeNameControl IsNot Nothing Then

                If oTradeNameControl.IsRecordExist(txtTradeName.Text.Trim) = False Then
                    txtTradeName.Text = ""
                    CloseProcedureControl("TradeName")
                Else
                    CloseProcedureControl("TradeName")
                End If
            End If
        End If
    End Sub

    Private Sub btnMvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvx.Click
        Try


            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
            oMVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtMvx.Select()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCvx.Click
        Try


            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
            oCVXControl.FillControl("")
            _isLoadGridCvxControl = False
            txtCvx.Select()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTradeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeName.Click
        Try


            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
            oTradeNameControl.FillControl("")
            _isLoadGridCvxControl = False

            txtTradeName.Select()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMvx.LostFocus
        If pnlMvxControl.Visible Then
            _isLoadGridCvxControl = True
            If oMVXControl IsNot Nothing Then
                If oMVXControl.Focus() = False Then
                    If oMVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Mvx")
                    End If
                End If
            End If
            _isLoadGridCvxControl = False
        End If
    End Sub

    Private Sub btnCvx_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCvx.KeyUp
        'If e.KeyCode = Keys.Tab Then
        '    e.SuppressKeyPress = True
        '    '#Region "Escape Key"
        '    If pnlCvxControl.Visible Then
        '        pnlCvxControl.Visible = False
        '        '#End Region
        '    End If
        'End If
    End Sub

    Private Sub btnCvx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCvx.LostFocus
        If pnlCvxControl.Visible Then
            _isLoadGridCvxControl = True
            If oCVXControl IsNot Nothing Then
                If oCVXControl.Focus() = False Then
                    If oCVXControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("Cvx")
                    End If
                End If
            End If
            _isLoadGridCvxControl = False
        End If
    End Sub

    Private Sub btnTradeName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTradeName.LostFocus
        If pnlTradeNameControl.Visible Then
            _isLoadGridCvxControl = True
            If oTradeNameControl IsNot Nothing Then
                If oTradeNameControl.Focus() = False Then
                    If oTradeNameControl.C1GridList.Focus() = False Then
                        CloseProcedureControl("TradeName")
                    End If
                End If
            End If
            _isLoadGridCvxControl = False
        End If
    End Sub

    'Private Sub btnCvx_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnCvx.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnCvx, "Select Vaccine")
    'End Sub

    Private Sub btnCvx_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    'Private Sub btnMvx_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnMvx.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnMvx, "Select Manufacturer")
    'End Sub

    Private Sub btnMvx_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    'Private Sub btnTradeName_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
    '    btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
    '    btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
    '    ToolTip1.SetToolTip(btnTradeName, "Select Trade Name")
    'End Sub

    Private Sub btnTradeName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
#Region "VIS Immnunization-Added by Mayuri"


    Private Sub btnScanVIS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanVIS.Click
        Try


            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_VIS) = False Then
                gDMSCategory_VIS = objSettings.DMSCategory_VIS
            End If
            objSettings = Nothing
            ScanViewDoucment()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Public Sub ViewScanDoucment()
        Try

            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _SelectedDocumentId As Int64 = 0
            Dim _result As Boolean = False
            If Not IsNothing(oViewDocument) Then
                oViewDocument = Nothing
            End If

            If (_DocumentID > 0) Then

                If IsNothing(oViewDocument) Then
                    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                End If


                _result = Get_ViewDocumentEvent(_ScanContainerID, _ScanDocumentID, _SelectedDocumentId)

                _DocumentID = _ScanDocumentID
                ''_DocumentID = _SelectedDocumentId
                ' If _DocumentID <> 0 Then
                txtVIS.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If txtVIS.Text.Trim() <> "" Then
                    btnView.Visible = True
                    btnScan.Visible = False
                Else
                    btnScan.Visible = True
                    btnView.Visible = False
                End If
                'Else

                ' End If



                If Not IsNothing(oViewDocument) Then
                    oViewDocument = Nothing
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If
        Finally
            If Not IsNothing(oViewDocument) Then
                oViewDocument.Dispose()
            End If


        End Try
    End Sub



    Public Sub ScanViewDoucment()
        Try


            Dim _ScanContainerID As Int64 = 0
            Dim _ScanDocumentID As Int64 = 0
            Dim _SelectedDocumentID As Int64 = 0
            Dim _result As Boolean = False

            Dim sDMSScanCategory As String


            Dim _ScanDocFlag As Boolean = True
            sDMSScanCategory = gDMSCategory_VIS


            If _ScanDocFlag = True Then
                If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, sDMSScanCategory, gClinicID) = False Then
                    MessageBox.Show("DMS Category for VIS has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _ScanDocFlag = False
                End If
            End If

            If _ScanDocFlag = True Then
                Dim arrDocumentInfo As New ArrayList
                Dim strDocumentInfo As String = ""
                _result = Set_ScanDocumentEvent(sDMSScanCategory, _ScanContainerID, _ScanDocumentID, _SelectedDocumentID)
                '' _DocumentID = _SelectedDocumentID
                _DocumentID = _ScanDocumentID

                txtVIS.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If txtVIS.Text.Trim() <> "" Then
                    btnView.Visible = True
                    btnScan.Visible = False
                Else

                    btnView.Visible = False
                    btnScan.Visible = True
                End If
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try

    End Sub
    Private Function Set_ScanDocumentEvent(ByVal VISCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef SelectedDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try

            _result = oScanDocument.ShowEDocument_Immunization(-1, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, SelectedDocumentID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oScanDocument) = False Then
                oScanDocument.Dispose()
                oScanDocument = Nothing
            End If

        End Try
        Return _result
    End Function

    Private Function Get_ViewDocumentEvent(ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64, ByRef selectedDocumentid As Int64) As Boolean
        If IsNothing(oViewDocument) Then
            oViewDocument = New gloEDocumentV3.gloEDocV3Management()
        End If
        Dim _result As Boolean = False
        Try

            _result = oViewDocument.ShowEDocument_Immunization(-1, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocument, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization, _DocumentID, ScanContainerID, ScanDocumentID, selectedDocumentid)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oViewDocument) = False Then
                oViewDocument.Dispose()

            End If

        End Try
        Return _result
    End Function

    Private Sub txtVIS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVIS.TextChanged
        If txtVIS.Text = "" Then
            btnScan.Visible = True
            btnView.Visible = False
        End If
    End Sub
#End Region
    Private Sub btnScanVIS_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScanVIS.MouseHover
        'If btnScanVIS.Text = "Scan/Import" Then
        '    ToolTip1.SetToolTip(btnScanVIS, "Scan/Import Document")
        'Else
        '    ToolTip1.SetToolTip(btnScanVIS, "View Document")
        'End If

    End Sub

    Private Sub btnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScan.Click
        Try


            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_VIS) = False Then
                gDMSCategory_VIS = objSettings.DMSCategory_VIS
            End If
            objSettings = Nothing
            ScanViewDoucment()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Scan, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        If (_DocumentID > 0) Then
            ViewScanDoucment()
        End If
    End Sub

    Private Sub btnScan_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScan.MouseHover

        ToolTip1.SetToolTip(btnScan, "Scan/Import Document")

    End Sub

    Private Sub btnView_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.MouseHover
        ToolTip1.SetToolTip(btnView, "View Document")
    End Sub

    Private Sub BtnAddManufacturerCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddManufacturerCategory.Click
        AddCategory("Manufacturer", "Add Manufacturer")
    End Sub
    Public Function AddCategory(ByVal CategoryName As String, ByVal Caption As String)
        Dim frm As New CategoryMaster(CategoryName)
        Try
            frm.Text = Caption
            frm.ShowDialog()
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

            frm = Nothing
        End Try
    End Function

    Private Sub BtnAddVaccineCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddVaccineCategory.Click
        AddCategory("Vaccine", "Add Vaccine")
    End Sub

    Private Sub BtnAddTradeNameCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddTradeNameCategory.Click
        AddCategory("TradeName", "Add Trade Name")
    End Sub

    Private Sub btnBrowseCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCPT.MouseHover
        ToolTip1.SetToolTip(btnBrowseCPT, "Select CPT")
    End Sub

    Private Sub btnClearCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.MouseHover
        ToolTip1.SetToolTip(btnClearCPT, "Clear CPT")
    End Sub

    Private Sub btnDiagnosis_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnosis.MouseHover
        ToolTip1.SetToolTip(btnDiagnosis, "Select Diagnosis")
    End Sub

    Private Sub btnDiaCancel_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiaCancel.MouseHover
        ToolTip1.SetToolTip(btnDiaCancel, "Clear Diagnosis")
    End Sub

    Private Sub btnMvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvx.MouseHover
        ToolTip1.SetToolTip(btnMvx, "Select Manufacturer")
    End Sub

  

    Private Sub btnTradeName_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeName.MouseHover
        ToolTip1.SetToolTip(btnTradeName, "Select Trade Name")
    End Sub

    Private Sub btnCvx_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCvx.MouseHover
        ToolTip1.SetToolTip(btnCvx, "Select Vaccine")
    End Sub

   
End Class
