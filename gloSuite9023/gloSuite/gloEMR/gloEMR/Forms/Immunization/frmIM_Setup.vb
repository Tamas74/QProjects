Public Class frmIM_Setup
    Inherits frmBaseForm
    Public _EditID As Long

    Private MVaccine As String
    Private blnMVaccine As Boolean

    Public _SaveFlag As Boolean
    Public _CPTCode As String
    Public _ReturnID As Long

    Public _blnOPenedFromDoses As Boolean = False

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
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim isVISGridFilled As Boolean = False
    'Dim _isLoadGridMvxControl As Boolean = False
    'Dim _isLoadGridTradeNameControl As Boolean = False
    ''End 20110322
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsImmunization As gloGlobal.gloToolStripIgnoreFocus
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
    Private ICDType As Integer = 9 ''added for icd10 implementation
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
    Friend WithEvents chkTracVaccinekInv As System.Windows.Forms.CheckBox
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddCategory As System.Windows.Forms.Button
    Friend WithEvents lblFunding As System.Windows.Forms.Label

    Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer
    Private blnTradeNameSelector As Boolean
    Private blnCVXSelector As Boolean
    Friend WithEvents tabSetup As System.Windows.Forms.TabControl
    Friend WithEvents tbIMsetup As System.Windows.Forms.TabPage
    Friend WithEvents tbVISSetup As System.Windows.Forms.TabPage
    Private WithEvents btn_tls_VIS As System.Windows.Forms.ToolStripButton
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents C1VIS As C1.Win.C1FlexGrid.C1FlexGrid
    Private blnMVXSelector As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        _databaseconnectionstring = GetConnectionString()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            Try

                Dim dtpControls As DateTimePicker() = {dtVISDate, dtDateReceived, dtExpiryDate}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try

            Catch
            End Try






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
        Me.btnView = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.chkTracVaccinekInv = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCvx = New System.Windows.Forms.TextBox()
        Me.lblVaccineType = New System.Windows.Forms.Label()
        Me.btnScanVIS = New System.Windows.Forms.Button()
        Me.btnCvx = New System.Windows.Forms.Button()
        Me.btnAddCategory = New System.Windows.Forms.Button()
        Me.BtnAddVaccineCategory = New System.Windows.Forms.Button()
        Me.BtnAddManufacturerCategory = New System.Windows.Forms.Button()
        Me.BtnAddTradeNameCategory = New System.Windows.Forms.Button()
        Me.btnTradeName = New System.Windows.Forms.Button()
        Me.btnMvx = New System.Windows.Forms.Button()
        Me.txtTradeName = New System.Windows.Forms.TextBox()
        Me.txtMvx = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSKUSearch = New gloUserControlLibrary.gloSearchTextBox()
        Me.lblFunding = New System.Windows.Forms.Label()
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
        Me.tlsImmunization = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_VIS = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlCvxControl = New System.Windows.Forms.Panel()
        Me.pnlMvxControl = New System.Windows.Forms.Panel()
        Me.pnlTradeNameControl = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabSetup = New System.Windows.Forms.TabControl()
        Me.tbIMsetup = New System.Windows.Forms.TabPage()
        Me.tbVISSetup = New System.Windows.Forms.TabPage()
        Me.C1VIS = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsImmunization.SuspendLayout()
        Me.tabSetup.SuspendLayout()
        Me.tbIMsetup.SuspendLayout()
        Me.tbVISSetup.SuspendLayout()
        CType(Me.C1VIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCount
        '
        Me.txtCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCount.ForeColor = System.Drawing.Color.Black
        Me.txtCount.Location = New System.Drawing.Point(182, 226)
        Me.txtCount.MaxLength = 8
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ShortcutsEnabled = False
        Me.txtCount.Size = New System.Drawing.Size(69, 22)
        Me.txtCount.TabIndex = 17
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(12, 230)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(167, 14)
        Me.lblCount.TabIndex = 38
        Me.lblCount.Text = "Doses administered in series :"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.btnView)
        Me.pnlTop.Controls.Add(Me.btnScan)
        Me.pnlTop.Controls.Add(Me.chkTracVaccinekInv)
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.Label9)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.cmbCategory)
        Me.pnlTop.Controls.Add(Me.cmbLocation)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.txtCvx)
        Me.pnlTop.Controls.Add(Me.lblVaccineType)
        Me.pnlTop.Controls.Add(Me.btnScanVIS)
        Me.pnlTop.Controls.Add(Me.btnCvx)
        Me.pnlTop.Controls.Add(Me.btnAddCategory)
        Me.pnlTop.Controls.Add(Me.BtnAddVaccineCategory)
        Me.pnlTop.Controls.Add(Me.BtnAddManufacturerCategory)
        Me.pnlTop.Controls.Add(Me.BtnAddTradeNameCategory)
        Me.pnlTop.Controls.Add(Me.btnTradeName)
        Me.pnlTop.Controls.Add(Me.btnMvx)
        Me.pnlTop.Controls.Add(Me.txtTradeName)
        Me.pnlTop.Controls.Add(Me.txtMvx)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.txtSKUSearch)
        Me.pnlTop.Controls.Add(Me.lblFunding)
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
        Me.pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(854, 398)
        Me.pnlTop.TabIndex = 0
        '
        'btnView
        '
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Location = New System.Drawing.Point(652, 470)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(24, 23)
        Me.btnView.TabIndex = 43
        Me.btnView.UseVisualStyleBackColor = True
        Me.btnView.Visible = False
        '
        'btnScan
        '
        Me.btnScan.BackgroundImage = CType(resources.GetObject("btnScan.BackgroundImage"), System.Drawing.Image)
        Me.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScan.Image = CType(resources.GetObject("btnScan.Image"), System.Drawing.Image)
        Me.btnScan.Location = New System.Drawing.Point(625, 470)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(24, 23)
        Me.btnScan.TabIndex = 42
        Me.btnScan.UseVisualStyleBackColor = True
        Me.btnScan.Visible = False
        '
        'chkTracVaccinekInv
        '
        Me.chkTracVaccinekInv.AutoSize = True
        Me.chkTracVaccinekInv.BackColor = System.Drawing.Color.Transparent
        Me.chkTracVaccinekInv.Checked = True
        Me.chkTracVaccinekInv.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTracVaccinekInv.Location = New System.Drawing.Point(182, 168)
        Me.chkTracVaccinekInv.Name = "chkTracVaccinekInv"
        Me.chkTracVaccinekInv.Size = New System.Drawing.Size(197, 18)
        Me.chkTracVaccinekInv.TabIndex = 13
        Me.chkTracVaccinekInv.Text = "Track inventory for this vaccine"
        Me.chkTracVaccinekInv.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(103, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(14, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "*"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(512, 110)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 14)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Category :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(117, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Location :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(579, 106)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(198, 22)
        Me.cmbCategory.TabIndex = 9
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(182, 44)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(203, 22)
        Me.cmbLocation.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(505, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCvx
        '
        Me.txtCvx.Location = New System.Drawing.Point(579, 75)
        Me.txtCvx.Name = "txtCvx"
        Me.txtCvx.Size = New System.Drawing.Size(200, 22)
        Me.txtCvx.TabIndex = 8
        Me.txtCvx.TabStop = False
        '
        'lblVaccineType
        '
        Me.lblVaccineType.AutoSize = True
        Me.lblVaccineType.BackColor = System.Drawing.Color.Transparent
        Me.lblVaccineType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVaccineType.Location = New System.Drawing.Point(519, 79)
        Me.lblVaccineType.Name = "lblVaccineType"
        Me.lblVaccineType.Size = New System.Drawing.Size(57, 14)
        Me.lblVaccineType.TabIndex = 18
        Me.lblVaccineType.Text = "Vaccine :"
        Me.lblVaccineType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnScanVIS
        '
        Me.btnScanVIS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScanVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScanVIS.Location = New System.Drawing.Point(16, 476)
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
        Me.btnCvx.Location = New System.Drawing.Point(783, 75)
        Me.btnCvx.Name = "btnCvx"
        Me.btnCvx.Size = New System.Drawing.Size(24, 23)
        Me.btnCvx.TabIndex = 9
        Me.btnCvx.TabStop = False
        Me.btnCvx.UseVisualStyleBackColor = False
        '
        'btnAddCategory
        '
        Me.btnAddCategory.BackColor = System.Drawing.Color.Transparent
        Me.btnAddCategory.BackgroundImage = CType(resources.GetObject("btnAddCategory.BackgroundImage"), System.Drawing.Image)
        Me.btnAddCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCategory.Image = CType(resources.GetObject("btnAddCategory.Image"), System.Drawing.Image)
        Me.btnAddCategory.Location = New System.Drawing.Point(783, 106)
        Me.btnAddCategory.Name = "btnAddCategory"
        Me.btnAddCategory.Size = New System.Drawing.Size(24, 23)
        Me.btnAddCategory.TabIndex = 10
        Me.btnAddCategory.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddCategory, "Add Immunization Inventory Category")
        Me.btnAddCategory.UseVisualStyleBackColor = False
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
        Me.BtnAddVaccineCategory.Location = New System.Drawing.Point(811, 75)
        Me.BtnAddVaccineCategory.Name = "BtnAddVaccineCategory"
        Me.BtnAddVaccineCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddVaccineCategory.TabIndex = 10
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
        Me.BtnAddManufacturerCategory.Location = New System.Drawing.Point(420, 106)
        Me.BtnAddManufacturerCategory.Name = "BtnAddManufacturerCategory"
        Me.BtnAddManufacturerCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddManufacturerCategory.TabIndex = 13
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
        Me.BtnAddTradeNameCategory.Location = New System.Drawing.Point(420, 75)
        Me.BtnAddTradeNameCategory.Name = "BtnAddTradeNameCategory"
        Me.BtnAddTradeNameCategory.Size = New System.Drawing.Size(24, 23)
        Me.BtnAddTradeNameCategory.TabIndex = 7
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
        Me.btnTradeName.Location = New System.Drawing.Point(393, 75)
        Me.btnTradeName.Name = "btnTradeName"
        Me.btnTradeName.Size = New System.Drawing.Size(24, 23)
        Me.btnTradeName.TabIndex = 6
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
        Me.btnMvx.Location = New System.Drawing.Point(393, 106)
        Me.btnMvx.Name = "btnMvx"
        Me.btnMvx.Size = New System.Drawing.Size(24, 23)
        Me.btnMvx.TabIndex = 12
        Me.btnMvx.TabStop = False
        Me.btnMvx.UseVisualStyleBackColor = False
        '
        'txtTradeName
        '
        Me.txtTradeName.Location = New System.Drawing.Point(182, 75)
        Me.txtTradeName.Name = "txtTradeName"
        Me.txtTradeName.Size = New System.Drawing.Size(203, 22)
        Me.txtTradeName.TabIndex = 5
        Me.txtTradeName.Text = " "
        '
        'txtMvx
        '
        Me.txtMvx.Location = New System.Drawing.Point(182, 106)
        Me.txtMvx.Name = "txtMvx"
        Me.txtMvx.Size = New System.Drawing.Size(203, 22)
        Me.txtMvx.TabIndex = 11
        Me.txtMvx.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(468, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSKUSearch
        '
        Me.txtSKUSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSKUSearch.Location = New System.Drawing.Point(182, 20)
        Me.txtSKUSearch.MaxLength = 15
        Me.txtSKUSearch.Name = "txtSKUSearch"
        Me.txtSKUSearch.Size = New System.Drawing.Size(203, 15)
        Me.txtSKUSearch.TabIndex = 1
        '
        'lblFunding
        '
        Me.lblFunding.AutoSize = True
        Me.lblFunding.BackColor = System.Drawing.Color.Transparent
        Me.lblFunding.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFunding.ForeColor = System.Drawing.Color.Red
        Me.lblFunding.Location = New System.Drawing.Point(472, 264)
        Me.lblFunding.Name = "lblFunding"
        Me.lblFunding.Size = New System.Drawing.Size(14, 14)
        Me.lblFunding.TabIndex = 26
        Me.lblFunding.Text = "*"
        Me.lblFunding.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(109, 141)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 14)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "*"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(84, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFundingSource
        '
        Me.cmbFundingSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFundingSource.FormattingEnabled = True
        Me.cmbFundingSource.Location = New System.Drawing.Point(579, 259)
        Me.cmbFundingSource.Name = "cmbFundingSource"
        Me.cmbFundingSource.Size = New System.Drawing.Size(200, 22)
        Me.cmbFundingSource.TabIndex = 20
        '
        'txtLotNo
        '
        Me.txtLotNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotNo.ForeColor = System.Drawing.Color.Black
        Me.txtLotNo.Location = New System.Drawing.Point(182, 137)
        Me.txtLotNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtLotNo.MaxLength = 50
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ShortcutsEnabled = False
        Me.txtLotNo.Size = New System.Drawing.Size(203, 22)
        Me.txtLotNo.TabIndex = 11
        '
        'txtDosesOnHand
        '
        Me.txtDosesOnHand.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosesOnHand.ForeColor = System.Drawing.Color.Black
        Me.txtDosesOnHand.Location = New System.Drawing.Point(579, 195)
        Me.txtDosesOnHand.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDosesOnHand.MaxLength = 6
        Me.txtDosesOnHand.Name = "txtDosesOnHand"
        Me.txtDosesOnHand.Size = New System.Drawing.Size(108, 22)
        Me.txtDosesOnHand.TabIndex = 16
        '
        'lblDosesOnHand
        '
        Me.lblDosesOnHand.AutoSize = True
        Me.lblDosesOnHand.BackColor = System.Drawing.Color.Transparent
        Me.lblDosesOnHand.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosesOnHand.Location = New System.Drawing.Point(427, 199)
        Me.lblDosesOnHand.Name = "lblDosesOnHand"
        Me.lblDosesOnHand.Size = New System.Drawing.Size(149, 14)
        Me.lblDosesOnHand.TabIndex = 36
        Me.lblDosesOnHand.Text = "Total Doses in Inventory :"
        Me.lblDosesOnHand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSnoMedID
        '
        Me.lblSnoMedID.AutoSize = True
        Me.lblSnoMedID.Location = New System.Drawing.Point(740, 478)
        Me.lblSnoMedID.Name = "lblSnoMedID"
        Me.lblSnoMedID.Size = New System.Drawing.Size(0, 14)
        Me.lblSnoMedID.TabIndex = 302
        Me.lblSnoMedID.Visible = False
        '
        'lblDescriptionID
        '
        Me.lblDescriptionID.AutoSize = True
        Me.lblDescriptionID.Location = New System.Drawing.Point(673, 478)
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
        Me.btnVaccineType.Location = New System.Drawing.Point(148, 480)
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
        Me.btnDiagnosis.Location = New System.Drawing.Point(393, 256)
        Me.btnDiagnosis.Name = "btnDiagnosis"
        Me.btnDiagnosis.Size = New System.Drawing.Size(24, 23)
        Me.btnDiagnosis.TabIndex = 22
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
        Me.btnClearVaccineType.Location = New System.Drawing.Point(175, 477)
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
        Me.btnDiaCancel.Location = New System.Drawing.Point(420, 256)
        Me.btnDiaCancel.Name = "btnDiaCancel"
        Me.btnDiaCancel.Size = New System.Drawing.Size(24, 23)
        Me.btnDiaCancel.TabIndex = 23
        Me.btnDiaCancel.TabStop = False
        Me.btnDiaCancel.UseVisualStyleBackColor = False
        '
        'cmbDiagnosis
        '
        Me.cmbDiagnosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDiagnosis.FormattingEnabled = True
        Me.cmbDiagnosis.Location = New System.Drawing.Point(182, 256)
        Me.cmbDiagnosis.Name = "cmbDiagnosis"
        Me.cmbDiagnosis.Size = New System.Drawing.Size(203, 22)
        Me.cmbDiagnosis.TabIndex = 19
        Me.cmbDiagnosis.TabStop = False
        '
        'txtNDCCode
        '
        Me.txtNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNDCCode.ForeColor = System.Drawing.Color.Black
        Me.txtNDCCode.Location = New System.Drawing.Point(182, 287)
        Me.txtNDCCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNDCCode.MaxLength = 11
        Me.txtNDCCode.Name = "txtNDCCode"
        Me.txtNDCCode.Size = New System.Drawing.Size(203, 22)
        Me.txtNDCCode.TabIndex = 21
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
        Me.dtVISDate.Location = New System.Drawing.Point(579, 226)
        Me.dtVISDate.Name = "dtVISDate"
        Me.dtVISDate.ShowCheckBox = True
        Me.dtVISDate.Size = New System.Drawing.Size(108, 22)
        Me.dtVISDate.TabIndex = 18
        '
        'txtVIS
        '
        Me.txtVIS.Enabled = False
        Me.txtVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVIS.ForeColor = System.Drawing.Color.Black
        Me.txtVIS.Location = New System.Drawing.Point(414, 470)
        Me.txtVIS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVIS.MaxLength = 1000
        Me.txtVIS.Name = "txtVIS"
        Me.txtVIS.Size = New System.Drawing.Size(203, 22)
        Me.txtVIS.TabIndex = 41
        Me.txtVIS.Visible = False
        '
        'lblNDC
        '
        Me.lblNDC.AutoSize = True
        Me.lblNDC.BackColor = System.Drawing.Color.Transparent
        Me.lblNDC.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDC.Location = New System.Drawing.Point(108, 291)
        Me.lblNDC.Name = "lblNDC"
        Me.lblNDC.Size = New System.Drawing.Size(70, 14)
        Me.lblNDC.TabIndex = 54
        Me.lblNDC.Text = "NDC Code :"
        Me.lblNDC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVIS
        '
        Me.lblVIS.AutoSize = True
        Me.lblVIS.BackColor = System.Drawing.Color.Transparent
        Me.lblVIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVIS.Location = New System.Drawing.Point(376, 474)
        Me.lblVIS.Name = "lblVIS"
        Me.lblVIS.Size = New System.Drawing.Size(34, 14)
        Me.lblVIS.TabIndex = 40
        Me.lblVIS.Text = "VIS :"
        Me.lblVIS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVIS.Visible = False
        '
        'lblVISDate
        '
        Me.lblVISDate.AutoSize = True
        Me.lblVISDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVISDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVISDate.Location = New System.Drawing.Point(473, 230)
        Me.lblVISDate.Name = "lblVISDate"
        Me.lblVISDate.Size = New System.Drawing.Size(103, 14)
        Me.lblVISDate.TabIndex = 44
        Me.lblVISDate.Text = "Publication Date :"
        Me.lblVISDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFundingSource
        '
        Me.lblFundingSource.AutoSize = True
        Me.lblFundingSource.BackColor = System.Drawing.Color.Transparent
        Me.lblFundingSource.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFundingSource.Location = New System.Drawing.Point(486, 263)
        Me.lblFundingSource.Name = "lblFundingSource"
        Me.lblFundingSource.Size = New System.Drawing.Size(90, 14)
        Me.lblFundingSource.TabIndex = 56
        Me.lblFundingSource.Text = "Funding Type :"
        Me.lblFundingSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiagnosis
        '
        Me.lblDiagnosis.AutoSize = True
        Me.lblDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.lblDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiagnosis.Location = New System.Drawing.Point(114, 260)
        Me.lblDiagnosis.Name = "lblDiagnosis"
        Me.lblDiagnosis.Size = New System.Drawing.Size(64, 14)
        Me.lblDiagnosis.TabIndex = 46
        Me.lblDiagnosis.Text = "Diagnosis :"
        Me.lblDiagnosis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDosespervial
        '
        Me.txtDosespervial.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDosespervial.ForeColor = System.Drawing.Color.Black
        Me.txtDosespervial.Location = New System.Drawing.Point(356, 195)
        Me.txtDosespervial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDosespervial.MaxLength = 6
        Me.txtDosespervial.Name = "txtDosespervial"
        Me.txtDosespervial.Size = New System.Drawing.Size(69, 22)
        Me.txtDosespervial.TabIndex = 14
        '
        'lblDosesPerVial
        '
        Me.lblDosesPerVial.AutoSize = True
        Me.lblDosesPerVial.BackColor = System.Drawing.Color.Transparent
        Me.lblDosesPerVial.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosesPerVial.Location = New System.Drawing.Point(261, 199)
        Me.lblDosesPerVial.Name = "lblDosesPerVial"
        Me.lblDosesPerVial.Size = New System.Drawing.Size(89, 14)
        Me.lblDosesPerVial.TabIndex = 34
        Me.lblDosesPerVial.Text = "Doses per vial :"
        Me.lblDosesPerVial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVials
        '
        Me.txtVials.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVials.ForeColor = System.Drawing.Color.Black
        Me.txtVials.Location = New System.Drawing.Point(182, 195)
        Me.txtVials.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtVials.MaxLength = 6
        Me.txtVials.Name = "txtVials"
        Me.txtVials.Size = New System.Drawing.Size(69, 22)
        Me.txtVials.TabIndex = 14
        '
        'lblVials
        '
        Me.lblVials.AutoSize = True
        Me.lblVials.BackColor = System.Drawing.Color.Transparent
        Me.lblVials.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVials.Location = New System.Drawing.Point(108, 199)
        Me.lblVials.Name = "lblVials"
        Me.lblVials.Size = New System.Drawing.Size(70, 14)
        Me.lblVials.TabIndex = 32
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
        Me.dtDateReceived.Location = New System.Drawing.Point(579, 16)
        Me.dtDateReceived.Name = "dtDateReceived"
        Me.dtDateReceived.ShowCheckBox = True
        Me.dtDateReceived.Size = New System.Drawing.Size(108, 22)
        Me.dtDateReceived.TabIndex = 2
        '
        'lblDateReceived
        '
        Me.lblDateReceived.AutoSize = True
        Me.lblDateReceived.BackColor = System.Drawing.Color.Transparent
        Me.lblDateReceived.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateReceived.Location = New System.Drawing.Point(482, 20)
        Me.lblDateReceived.Name = "lblDateReceived"
        Me.lblDateReceived.Size = New System.Drawing.Size(94, 14)
        Me.lblDateReceived.TabIndex = 5
        Me.lblDateReceived.Text = "Date Received :"
        Me.lblDateReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(182, 317)
        Me.txtComments.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtComments.MaxLength = 1000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtComments.Size = New System.Drawing.Size(604, 67)
        Me.txtComments.TabIndex = 27
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.BackColor = System.Drawing.Color.Transparent
        Me.lblComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.Location = New System.Drawing.Point(106, 321)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(73, 14)
        Me.lblComments.TabIndex = 58
        Me.lblComments.Text = "Comments :"
        Me.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cmbStatus.Location = New System.Drawing.Point(579, 44)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(108, 22)
        Me.cmbStatus.TabIndex = 4
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(526, 48)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(50, 14)
        Me.lblStatus.TabIndex = 10
        Me.lblStatus.Text = "Status :"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLotNumber
        '
        Me.lblLotNumber.AutoSize = True
        Me.lblLotNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblLotNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLotNumber.Location = New System.Drawing.Point(122, 141)
        Me.lblLotNumber.Name = "lblLotNumber"
        Me.lblLotNumber.Size = New System.Drawing.Size(56, 14)
        Me.lblLotNumber.TabIndex = 27
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
        Me.dtExpiryDate.Location = New System.Drawing.Point(579, 137)
        Me.dtExpiryDate.Name = "dtExpiryDate"
        Me.dtExpiryDate.ShowCheckBox = True
        Me.dtExpiryDate.Size = New System.Drawing.Size(108, 22)
        Me.dtExpiryDate.TabIndex = 12
        '
        'lblexpirydate
        '
        Me.lblexpirydate.AutoSize = True
        Me.lblexpirydate.BackColor = System.Drawing.Color.Transparent
        Me.lblexpirydate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexpirydate.Location = New System.Drawing.Point(507, 141)
        Me.lblexpirydate.Name = "lblexpirydate"
        Me.lblexpirydate.Size = New System.Drawing.Size(69, 14)
        Me.lblexpirydate.TabIndex = 29
        Me.lblexpirydate.Text = "Exp. Date :"
        Me.lblexpirydate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTradeName
        '
        Me.lblTradeName.AutoSize = True
        Me.lblTradeName.BackColor = System.Drawing.Color.Transparent
        Me.lblTradeName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTradeName.Location = New System.Drawing.Point(96, 79)
        Me.lblTradeName.Name = "lblTradeName"
        Me.lblTradeName.Size = New System.Drawing.Size(82, 14)
        Me.lblTradeName.TabIndex = 13
        Me.lblTradeName.Text = "Trade Name :"
        Me.lblTradeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmanufacturer
        '
        Me.lblmanufacturer.AutoSize = True
        Me.lblmanufacturer.BackColor = System.Drawing.Color.Transparent
        Me.lblmanufacturer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmanufacturer.Location = New System.Drawing.Point(91, 110)
        Me.lblmanufacturer.Name = "lblmanufacturer"
        Me.lblmanufacturer.Size = New System.Drawing.Size(87, 14)
        Me.lblmanufacturer.TabIndex = 22
        Me.lblmanufacturer.Text = "Manufacturer :"
        Me.lblmanufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSKU
        '
        Me.lblSKU.AutoSize = True
        Me.lblSKU.BackColor = System.Drawing.Color.Transparent
        Me.lblSKU.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSKU.Location = New System.Drawing.Point(141, 20)
        Me.lblSKU.Name = "lblSKU"
        Me.lblSKU.Size = New System.Drawing.Size(37, 14)
        Me.lblSKU.TabIndex = 2
        Me.lblSKU.Text = "SKU :"
        Me.lblSKU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblConceptID
        '
        Me.lblConceptID.AutoSize = True
        Me.lblConceptID.Location = New System.Drawing.Point(436, 536)
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
        Me.Label4.Location = New System.Drawing.Point(411, 493)
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
        Me.Label3.Location = New System.Drawing.Point(507, 291)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 50
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
        Me.btnBrowseCPT.Location = New System.Drawing.Point(783, 285)
        Me.btnBrowseCPT.Name = "btnBrowseCPT"
        Me.btnBrowseCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnBrowseCPT.TabIndex = 25
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
        Me.btnClearCPT.Location = New System.Drawing.Point(811, 285)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(24, 23)
        Me.btnClearCPT.TabIndex = 26
        Me.btnClearCPT.TabStop = False
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'cmbCPT
        '
        Me.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCPT.FormattingEnabled = True
        Me.cmbCPT.Location = New System.Drawing.Point(579, 287)
        Me.cmbCPT.Name = "cmbCPT"
        Me.cmbCPT.Size = New System.Drawing.Size(200, 22)
        Me.cmbCPT.TabIndex = 24
        Me.cmbCPT.TabStop = False
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 397)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(852, 1)
        Me.lbl_pnlBottom.TabIndex = 9
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 397)
        Me.lbl_pnlLeft.TabIndex = 1
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(853, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 397)
        Me.lbl_pnlRight.TabIndex = 7
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(854, 1)
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
        Me.lblVaccineCode.Location = New System.Drawing.Point(423, 498)
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
        Me.pnl_tls_.Size = New System.Drawing.Size(868, 53)
        Me.pnl_tls_.TabIndex = 1
        '
        'tlsImmunization
        '
        Me.tlsImmunization.BackColor = System.Drawing.Color.Transparent
        Me.tlsImmunization.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsImmunization.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsImmunization.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsImmunization.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_VIS, Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tlsImmunization.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsImmunization.Location = New System.Drawing.Point(0, 0)
        Me.tlsImmunization.Name = "tlsImmunization"
        Me.tlsImmunization.Size = New System.Drawing.Size(868, 53)
        Me.tlsImmunization.TabIndex = 0
        Me.tlsImmunization.TabStop = True
        Me.tlsImmunization.Text = "toolStrip1"
        '
        'btn_tls_VIS
        '
        Me.btn_tls_VIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_VIS.Image = CType(resources.GetObject("btn_tls_VIS.Image"), System.Drawing.Image)
        Me.btn_tls_VIS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_VIS.Name = "btn_tls_VIS"
        Me.btn_tls_VIS.Size = New System.Drawing.Size(36, 50)
        Me.btn_tls_VIS.Tag = "VIS"
        Me.btn_tls_VIS.Text = "&VIS"
        Me.btn_tls_VIS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_VIS.ToolTipText = "Vaccine Information Statement"
        Me.btn_tls_VIS.Visible = False
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
        Me.pnlCvxControl.Location = New System.Drawing.Point(585, 178)
        Me.pnlCvxControl.Name = "pnlCvxControl"
        Me.pnlCvxControl.Size = New System.Drawing.Size(253, 165)
        Me.pnlCvxControl.TabIndex = 322
        Me.pnlCvxControl.Visible = False
        '
        'pnlMvxControl
        '
        Me.pnlMvxControl.Location = New System.Drawing.Point(187, 210)
        Me.pnlMvxControl.Name = "pnlMvxControl"
        Me.pnlMvxControl.Size = New System.Drawing.Size(259, 165)
        Me.pnlMvxControl.TabIndex = 323
        Me.pnlMvxControl.Visible = False
        '
        'pnlTradeNameControl
        '
        Me.pnlTradeNameControl.Location = New System.Drawing.Point(187, 178)
        Me.pnlTradeNameControl.Name = "pnlTradeNameControl"
        Me.pnlTradeNameControl.Size = New System.Drawing.Size(260, 165)
        Me.pnlTradeNameControl.TabIndex = 325
        Me.pnlTradeNameControl.Visible = False
        '
        'tabSetup
        '
        Me.tabSetup.Controls.Add(Me.tbIMsetup)
        Me.tabSetup.Controls.Add(Me.tbVISSetup)
        Me.tabSetup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabSetup.Location = New System.Drawing.Point(0, 53)
        Me.tabSetup.Name = "tabSetup"
        Me.tabSetup.SelectedIndex = 0
        Me.tabSetup.Size = New System.Drawing.Size(868, 431)
        Me.tabSetup.TabIndex = 305
        '
        'tbIMsetup
        '
        Me.tbIMsetup.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbIMsetup.Controls.Add(Me.pnlTop)
        Me.tbIMsetup.Location = New System.Drawing.Point(4, 23)
        Me.tbIMsetup.Name = "tbIMsetup"
        Me.tbIMsetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tbIMsetup.Size = New System.Drawing.Size(860, 404)
        Me.tbIMsetup.TabIndex = 0
        Me.tbIMsetup.Text = "IM Setup"
        '
        'tbVISSetup
        '
        Me.tbVISSetup.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbVISSetup.Controls.Add(Me.C1VIS)
        Me.tbVISSetup.Controls.Add(Me.Label13)
        Me.tbVISSetup.Controls.Add(Me.Label12)
        Me.tbVISSetup.Controls.Add(Me.Label11)
        Me.tbVISSetup.Controls.Add(Me.Label10)
        Me.tbVISSetup.Location = New System.Drawing.Point(4, 23)
        Me.tbVISSetup.Name = "tbVISSetup"
        Me.tbVISSetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tbVISSetup.Size = New System.Drawing.Size(860, 404)
        Me.tbVISSetup.TabIndex = 1
        Me.tbVISSetup.Text = "VIS Setup"
        '
        'C1VIS
        '
        Me.C1VIS.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1VIS.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1VIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1VIS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1VIS.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1VIS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1VIS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1VIS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1VIS.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1VIS.Location = New System.Drawing.Point(4, 4)
        Me.C1VIS.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1VIS.Name = "C1VIS"
        Me.C1VIS.Rows.Count = 1
        Me.C1VIS.Rows.DefaultSize = 19
        Me.C1VIS.Rows.Fixed = 0
        Me.C1VIS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1VIS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1VIS.ShowCellLabels = True
        Me.C1VIS.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1VIS.Size = New System.Drawing.Size(852, 396)
        Me.C1VIS.StyleInfo = resources.GetString("C1VIS.StyleInfo")
        Me.C1VIS.TabIndex = 20
        Me.C1VIS.Visible = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(4, 400)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(852, 1)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(852, 1)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(856, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 398)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 398)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "label4"
        '
        'frmIM_Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(868, 484)
        Me.Controls.Add(Me.tabSetup)
        Me.Controls.Add(Me.pnlCvxControl)
        Me.Controls.Add(Me.pnlTradeNameControl)
        Me.Controls.Add(Me.pnlMvxControl)
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
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsImmunization.ResumeLayout(False)
        Me.tlsImmunization.PerformLayout()
        Me.tabSetup.ResumeLayout(False)
        Me.tbIMsetup.ResumeLayout(False)
        Me.tbVISSetup.ResumeLayout(False)
        CType(Me.C1VIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Form Event"

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

            If GetRequirefunding() = "1" Then
                lblFunding.Visible = True
            Else
                lblFunding.Visible = False
            End If

            FillControls()

            Dim value As New Object()
            Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
            ogloSettings.GetSetting("TrackVaccineInventory", 0, gnClinicID, value)

            If value = "1" Then
                chkTracVaccinekInv.Checked = True
            Else
                chkTracVaccinekInv.Checked = False
                chkTracVaccinekInv.Enabled = False
            End If

            value = Nothing
            ogloSettings.Dispose()
            ogloSettings = Nothing

            If _EditID <> 0 Then

                _isFormLoadModify = True
                _isLoadGridCvxControl = True

                Fill_EditCriteria(_EditID)

                If _blnOPenedFromDoses = True Then
                    EnableVaccineDetail()
                    txtDosesOnHand.Select()
                    txtDosesOnHand.SelectionStart = Len(txtDosesOnHand.Text)
                Else
                    txtTradeName.Select()
                End If

                If txtSKUSearch.Text.Trim() <> "" Then
                    _isLoad = True
                End If
            End If

            _isLoadGridCvxControl = False
            _isLoaded = True

            fillGrid()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oDM) = False Then

                oDM = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub tlsImmunization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsImmunization.ItemClicked
        tlsImmunization.Select()
        Select Case e.ClickedItem.Tag
            Case "Save"
                Try
                    SaveImmunization()
                Catch ex As Exception
                    MessageBox.Show("Error while saving data." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Case "Close"
                Me.Close()

            Case "VIS"
                Try
                    If C1VIS.Rows.Count > 1 Then
                        If (_DocumentID > 0) Then
                            ViewScanDoucment()
                            If _DocumentID = 0 Then
                                DeleteVISMapping(C1VIS.GetData(C1VIS.RowSel, 0))
                            End If

                        Else
                            gDMSCategory_VIS = ""
                            Dim objSettings As New clsSettings
                            objSettings.GetSettings()
                            If IsNothing(objSettings.DMSCategory_VIS) = False Then
                                gDMSCategory_VIS = objSettings.DMSCategory_VIS
                            End If
                            objSettings = Nothing
                            ScanViewDoucment()
                        End If
                        If _DocumentID > 0 Then
                            C1VIS.SetData(C1VIS.RowSel, 2, _DocumentID)
                            C1VIS.SetData(C1VIS.RowSel, 3, True)
                        Else
                            C1VIS.SetData(C1VIS.RowSel, 3, False)
                        End If
                    End If
                    Me.Focus()
                Catch ex As Exception
                    MessageBox.Show("Error while showing or viewing VIS document." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
        End Select
    End Sub

    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'SLR: 2/18/2015, What is the purpose of filling toList here?
            'code added by dipak 20090910 for add all selected code in dataTable and Bind that datable to cmbCPT
            Dim dtCPTCode As DataTable
            '  Dim ToList As gloGeneralItem.gloItems
            dtCPTCode = New DataTable
            Dim dcID As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Code")
            dtCPTCode.Columns.Add(dcID)
            dtCPTCode.Columns.Add(dcDescription)
            '  ToList = New gloGeneralItem.gloItems()
            ' Dim ToItem As gloGeneralItem.gloItem
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
                        ' ToItem = New gloGeneralItem.gloItem()
                        ' ToItem.ID = oListControl.SelectedItems(i).ID
                        ' ToItem.Description = oListControl.SelectedItems(i).Code
                        '  ToList.Add(ToItem)
                        'ToItem = Nothing
                    Next
                    cmbCPT.Items.Clear()
                    For i As Int16 = 0 To dtCPTCode.Rows.Count - 1
                        If cmbCPT.Items.Contains(dtCPTCode.Rows(i)("Code").ToString().Trim) Then
                        Else
                            cmbCPT.Items.Add(dtCPTCode.Rows(i)("Code").ToString().Trim)
                        End If

                    Next
                    If (cmbCPT.Items.Count > 0) Then
                        cmbCPT.SelectedIndex = 0
                    End If

                    ' cmbCPT.DataSource = dtCPTCode
                    cmbCPT.ValueMember = dtCPTCode.Columns("ID").ColumnName
                    cmbCPT.DisplayMember = dtCPTCode.Columns("Code").ColumnName
                    ofrmList.Close()
                End If
            Else
                ''Added Rahul for Fixed BugID 6726 on 20101129
                ' cmbCPT.Items.Clear()
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

#End Region

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
        End Try
    End Sub

    Private Sub FillLocation()
        Try
            ' Dim i As Int16
            If IsNothing(dsVaccineInformation.Tables("Location")) = False Then
                If dsVaccineInformation.Tables("Location").Rows.Count > 0 Then
                    cmbLocation.DataSource = dsVaccineInformation.Tables("Location")
                    cmbLocation.ValueMember = "nLocationID"
                    cmbLocation.DisplayMember = "sLocation"
                End If
            End If

            Dim _DefaultLocationID As Int64
            _DefaultLocationID = GetDefaultLocation()

            If cmbLocation.Items.Count > 0 Then
                cmbLocation.SelectedValue = _DefaultLocationID
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetDefaultLocation() As Int64
        Dim oValue As Object = Nothing
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString())
            oDB.Connect(False)
            oValue = oDB.ExecuteScalar("GetDefaultLocationID")
            oDB.Disconnect()
            Return Convert.ToInt64(oValue)
        Catch ex As Exception
            GetDefaultLocation = Nothing
            Throw
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            oValue = Nothing
        End Try
    End Function

    Private Sub AllowNumericValue(ByVal Text As String, ByVal e As KeyPressEventArgs)
        'Allow only numeric and decimal point keys
        If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
            'e.KeyChar.IsDigit(e.KeyChar)
            e.Handled = True
        Else
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys
            If (InStr(Trim(Text), ".") <> 0 AndAlso e.KeyChar = ChrW(46)) Then
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8)) OrElse ((e.KeyChar = ChrW(45)) AndAlso InStr(Trim(Text), "-") = 0)) Then
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
        If Not IsNothing(dsVaccineInformation) Then
            If dsVaccineInformation.Tables.Contains("Master") Then
                oCriteria = oDM.ItemDetail(dsVaccineInformation.Tables("Master"))
                ''code added to Set ICDType 9 or 10
                If dsVaccineInformation.Tables("Master").Rows.Count > 0 Then
                    ICDType = Convert.ToInt16(dsVaccineInformation.Tables("Master").Rows(0)("nICDRevision"))
                End If
            End If
        End If

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
                    Dim _DefaultLocationID As Int64
                    _DefaultLocationID = GetDefaultLocation()

                    If cmbLocation.Items.Count > 0 Then
                        cmbLocation.SelectedValue = _DefaultLocationID
                    End If
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
                        btnView.Location = btnScan.Location
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
                    MVaccine = .Vaccine
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

                If .DiagnosisCode <> "" Then
                    arrSplitDiagnosis = .DiagnosisCode.Split(",")

                    For i As Integer = 0 To arrSplitDiagnosis.GetUpperBound(0)
                        cmbDiagnosis.Items.Add(arrSplitDiagnosis(i))
                    Next
                    If (cmbDiagnosis.Items.Count > 0) Then
                        cmbDiagnosis.SelectedIndex = 0
                    End If


                End If


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
                chkTracVaccinekInv.Checked = .bTrackInventory
                cmbCategory.SelectedValue = .CategoryID

            End With


        End If

        If IsNothing(oCriteria) = False Then
            oCriteria = Nothing
        End If

        If IsNothing(oDM) = False Then
            oDM = Nothing
        End If
    End Sub

#End Region

#Region "Diagnosis control Added:Mayuri:20120112"

    Private Sub oDiagnosisListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'SLR: 2/18/2015 What is the purpose of filling toList here
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
                        ToItem.Dispose()
                        ToItem = Nothing
                    Next
                    ICDType = oDiagnosisListControl.IsICD9_10 ''added for icd10 implementation
                    cmbDiagnosis.DataSource = dtICD9Code
                    cmbDiagnosis.ValueMember = dtICD9Code.Columns("ID").ColumnName
                    cmbDiagnosis.DisplayMember = dtICD9Code.Columns("Code").ColumnName
                    If (Not IsNothing(oDiagnosisListControl._dtStoreDiagnosis)) Then ''dispose _dtStoreDiagnosis table
                        oDiagnosisListControl._dtStoreDiagnosis.Dispose()
                        oDiagnosisListControl._dtStoreDiagnosis = Nothing
                    End If
                    ofrmDiagnosisList.Close()
                End If
            Else
                ' cmbDiagnosis.Items.Clear()
                cmbDiagnosis.DataSource = Nothing
                cmbDiagnosis.Items.Clear()
                ofrmDiagnosisList.Close()

            End If
            ToList.Dispose()
            ToList = Nothing
        Catch ex As Exception
            MessageBox.Show("Error on User List Control." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub oDiagnosisListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If (Not IsNothing(oDiagnosisListControl._dtStoreDiagnosis)) Then  ''dispose _dtStoreDiagnosis table
            oDiagnosisListControl._dtStoreDiagnosis.Dispose()
            oDiagnosisListControl._dtStoreDiagnosis = Nothing
        End If
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
                    If cmbCPT.Items.Count > 0 Then
                        cmbCPT.SelectedIndex = 0
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

#End Region

#Region "Validating"

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

#End Region

#Region "MouseMove"

    Private Sub oCvxControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oCVXControl.C1GridList.Select()
    End Sub

    Private Sub oMVXControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oMVXControl.C1GridList.Select()
    End Sub

    Private Sub oTradeNameControl_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        oTradeNameControl.C1GridList.Select()
    End Sub

#End Region

#Region "KeyPress"

    Private Sub txtSKUSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSKUSearch.KeyPress
        'AllowNumericValue(txtSKUSearch.Text, e)
    End Sub

    Private Sub txtDosesOnHand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosesOnHand.KeyPress
        AllowDecimal(txtDosesOnHand.Text, e)
    End Sub

    Private Sub txtDosespervial_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDosespervial.KeyPress
        AllowDecimal(txtDosespervial.Text, e)
    End Sub

    Private Sub txtNDCCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNDCCode.KeyPress
        AllowNumericValue(txtNDCCode.Text, e)
    End Sub

    Private Sub txtVials_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVials.KeyPress
        AllowDecimal(txtVials.Text, e)
    End Sub

#End Region

#Region "TextChanged"

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

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

#End Region

#Region "ItemSelected"

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

            blnMVaccine = True

            ''Added New Logic to get CVX and MVX From TradeName:Mayuri:20110119
            GetCVXMVX()
            GetCPTCodeFromCVX()

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

            blnMVaccine = True

            txtCvx.Focus()
            GetCPTCodeFromCVX()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "InternalGridKeyDown"

    Private Sub oTradeNameControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub oMvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub oCvxControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

#End Region

#Region "InternalGridLostFocus"

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

#End Region

#Region "KeyUp event"

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


#End Region

#Region "LostFocus"

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

#End Region

#Region "MouseLeave"

    Private Sub btnBrowseCPT_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnBrowseCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnBrowseCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnClearCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnClearCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnClearCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCvx_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnCvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnCvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnMvx_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnMvx.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnMvx.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnTradeName_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        btnTradeName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        btnTradeName.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDiagnosis_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDiaCancel_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnDiaCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDiaCancel.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

#End Region

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

                txtVIS.Text = gloEDocumentV3.eDocManager.eDocValidator.GetDocumentName_Immunization(_DocumentID)
                If txtVIS.Text.Trim() <> "" Then
                    btnView.Visible = True
                    btnView.Location = btnScan.Location
                    btnScan.Visible = False
                Else
                    _DocumentID = 0
                    btnScan.Visible = True
                    btnView.Visible = False
                End If

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

#Region "Function"

    Public Function AddCategory(ByVal CategoryName As String, ByVal Caption As String)
        Dim frm As New CategoryMaster(CategoryName)
        Try
            frm.Text = Caption
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            frm = Nothing
        End Try
        Return Nothing
    End Function

    Private Function GetNDCFromSKU()
        Dim oDM As New gloStream.Immunization.ItemSetup

        Try
            _NDC = txtSKUSearch.Text.Trim

            Dim _IsNDCCodeFound As Boolean = False
            If _isLoad = True Then
                _isLoad = False
                GetNDCFromSKU = Nothing
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
                        GetNDCFromSKU = Nothing
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
                    Dim _dtDrugsForm As List(Of String)
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
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                            _dtDrugsForm = oDIBGSHelper.GetNDCCodeFromGSDDDB(Replace(_labellerandproductcode.Trim, "'", "''"))
                        End Using
                        If Not IsNothing(_dtDrugsForm) Then
                            For Each item As String In _dtDrugsForm
                                If item.ToString().Trim.Replace("-", "") = _NDC.Trim Then
                                    txtNDCCode.Text = _NDC.Trim
                                    _IsNDCCodeFound = True
                                    Exit For
                                Else
                                    txtNDCCode.Text = ""
                                End If
                            Next
                        End If

                        If _IsNDCCodeFound = False Then

                            Dim _labelcode As String = ""
                            Dim _productcode As String = ""
                            Dim _packageCode As String = ""
                            _labelcode = _NDC.Substring(0, 5)
                            _productcode = _NDC.Substring(5, 3)
                            _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
                            _labelcode = "0" & _labelcode
                            _labellerandproductcode = _labelcode.Trim & _productcode.Trim
                            ' dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
                            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                                _dtDrugsForm = oDIBGSHelper.GetNDCCodeFromGSDDDB(Replace(_labellerandproductcode.Trim, "'", "''"))
                            End Using
                            If (Not IsNothing(_dtDrugsForm)) Then
                                For Each item As String In _dtDrugsForm
                                    If item.ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
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

                            Dim _labelcode As String = ""
                            Dim _productcode As String = ""
                            Dim _packageCode As String = ""
                            _labelcode = _NDC.Substring(0, 5)
                            _productcode = _NDC.Substring(5, 3)
                            _packageCode = _NDC.Substring(8, Len(_NDC) - (Len(_labelcode) + Len(_productcode)))
                            _productcode = "0" & _productcode
                            _labellerandproductcode = _labelcode.Trim & _productcode.Trim
                            'dtndccodelist = oDM.getNDCCodeFromMMWDB(_labellerandproductcode.Trim)
                            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gstrDIBServiceURL)
                                _dtDrugsForm = oDIBGSHelper.GetNDCCodeFromGSDDDB(Replace(_labellerandproductcode.Trim, "'", "''"))
                            End Using
                            If Not IsNothing(_dtDrugsForm) Then
                                For Each item As String In _dtDrugsForm
                                    If item.ToString().Trim.Replace("-", "") = _labellerandproductcode.Trim & _packageCode.Trim Then
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
        Return Nothing
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
            Return Nothing
        End Try
    End Function

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
        Return Nothing
    End Function

    Private Function GetRequirefunding() As String
        'Added by Amit to check Track Vaccine Inventory setting is ON or OFF
        Dim value As New Object()
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        ogloSettings.GetSetting("REQUIREFUNDING", 0, gnClinicID, value)
        ogloSettings.Dispose()
        ogloSettings = Nothing
        Return value
    End Function

#End Region

#Region "Sub-Procedure"

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
        If _EditID = 0 Then
            cmbCPT.Items.Clear()
        End If

        Dim oClsIM As New gloStream.Immunization.ItemSetup
        Dim dt As DataTable
        If txtCvx.Text.Trim() <> "" Then
            dt = oClsIM.GetCPTFromCVXCode(txtCvx.Text.Trim())
            If IsNothing(dt) = False Then

                If dt.Rows.Count > 0 Then
                    If cmbCPT.Items.Count = 0 Then
                        cmbCPT.Items.Clear()
                        For i As Int16 = 0 To dt.Rows.Count - 1
                            cmbCPT.Items.Add(dt.Rows(i)("cptcode").ToString().Trim)
                        Next
                        blnMVaccine = True
                    Else
                        If blnMVaccine = True Then
                            cmbCPT.Items.Clear()
                            For i As Int16 = 0 To dt.Rows.Count - 1
                                cmbCPT.Items.Add(dt.Rows(i)("cptcode").ToString().Trim)
                            Next

                        End If
                    End If
                Else
                    If blnMVaccine = False Then
                        If MVaccine <> txtCvx.Text.Trim() Then
                            blnMVaccine = True
                            cmbCPT.Items.Clear()
                        End If
                    Else
                        cmbCPT.Items.Clear()
                    End If
                End If

                If cmbCPT.Items.Count > 0 Then
                    cmbCPT.SelectedIndex = 0
                End If

            End If
        End If
    End Sub

    Private Sub ClearFields()
        cmbCPT.Items.Clear()
        _isLoadGridCvxControl = True

        txtCvx.Text = ""

        txtMvx.Text = ""

        txtTradeName.Text = ""
        _isLoadGridCvxControl = False

        txtNDCCode.Text = ""
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
        Finally
        End Try
    End Sub

    Private Sub CloseProcedureControl(ByVal _controlName As String)

        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            If _controlName = "Cvx" And blnCVXSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlCvxControl.Controls.Count - 1 To 0 Step -1
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
                '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            ElseIf _controlName = "Mvx" And blnMVXSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlMvxControl.Controls.Count - 1 To 0 Step -1
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
                '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            ElseIf _controlName = "TradeName" And blnTradeNameSelector = False Then
                'SLR: Changed on 4/2/2014
                For i As Integer = pnlTradeNameControl.Controls.Count - 1 To 0 Step -1
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

    Private Sub txtSKUSearch_SearchFired() Handles txtSKUSearch.SearchFired
        GetNDCFromSKU()
        GetCPTCodeFromCVX()
    End Sub

    Private Sub EnableVaccineDetail()
        If _blnOPenedFromDoses = True Then
            cmbLocation.Enabled = False
            txtTradeName.Enabled = False
            btnTradeName.Enabled = False
            BtnAddTradeNameCategory.Enabled = False
            txtCvx.Enabled = False
            btnCvx.Enabled = False
            BtnAddVaccineCategory.Enabled = False
            txtMvx.Enabled = False
            btnMvx.Enabled = False
            BtnAddManufacturerCategory.Enabled = False
            txtLotNo.Enabled = False
        End If
    End Sub

    Private Sub FillControls()

        FillFundingSource()

        cmbStatus.Items.Clear()
        cmbStatus.Items.Add("Active")
        cmbStatus.Items.Add("Inactive")
        cmbStatus.SelectedIndex = 0
        txtDosesOnHand.Text = "0"

        ''Added by Mayuri:20120507-Location changes added in immunization
        FillLocation()

        cmbCategory.DataSource = dsVaccineInformation.Tables("Category")
        cmbCategory.ValueMember = "nCategoryID"
        cmbCategory.DisplayMember = "sDescription"
        cmbCategory.SelectedIndex = -1

    End Sub


#End Region

#Region "Button_Click"

    Private Sub BtnAddVaccineCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddVaccineCategory.Click
        Try
            AddCategory("Vaccine", "Add Vaccine")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAddTradeNameCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddTradeNameCategory.Click
        Try
            AddCategory("TradeName", "Add Trade Name")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAddManufacturerCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddManufacturerCategory.Click
        Try
            AddCategory("Manufacturer", "Add Manufacturer")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddCategory_Click(sender As System.Object, e As System.EventArgs) Handles btnAddCategory.Click
        Try
            AddCategory("Immunization Inventory Category", "Add Immunization Inventory Category")
            GetRefreshedCategory()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    'Added to avoid Object Reference Error 
    Private Sub btnMvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMvx.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnMVXSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Mvx, "Mvx", "")
            If Not IsNothing(oMVXControl) Then
                oMVXControl.FillControl("")
                _isLoadGridCvxControl = False
                txtMvx.Select()
            End If
            blnMVXSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCvx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCvx.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnCVXSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Cvx, "Cvx", "")
            If Not IsNothing(oCVXControl) Then
                oCVXControl.FillControl("")
                _isLoadGridCvxControl = False
                txtCvx.Select()
            End If
            blnCVXSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTradeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTradeName.Click
        Try
            '31-Dec-15 Aniket: Resolving Bug #92277: Waas > gloEMR > Edit>Orders&Result setup>New> Loinc code and CPT code browse button is not working
            blnTradeNameSelector = True
            _isLoadGridCvxControl = True
            OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.TradeName, "TradeName", "")
            If Not IsNothing(oTradeNameControl) Then
                oTradeNameControl.FillControl("")
                _isLoadGridCvxControl = False
                txtTradeName.Select()
            End If
            blnTradeNameSelector = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub btnDiagnosis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagnosis.Click
        Try
            'Code Added By Dipak 20090910 For Display List Control on click of CPTBrowseCPT button
            ofrmDiagnosisList = New frmViewListControl
            ' Dim arrCPTTextSplit As String()

            If IsNothing(oDiagnosisListControl) = False Then
                RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                oDiagnosisListControl.Dispose()
                oDiagnosisListControl = Nothing
            End If


            oDiagnosisListControl = New gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Diagnosis, True, Me.Width)
            oDiagnosisListControl.ControlHeader = "Diagnosis"
            oDiagnosisListControl.gblnIcd10Transition = gblnIcd10MasterTransition 'gblnIcd10Transition ''added to Select  ICD10 if true 
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

            If (ICDType = 10) Then  ''condition added for icd10 implementation
                oDiagnosisListControl.IsICD9_10 = 10
            End If

            oDiagnosisListControl.OpenControl()
            ofrmDiagnosisList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmDiagnosisList.Text = "Diagnosis"
            ofrmDiagnosisList.ShowDialog(IIf(IsNothing(CType(ofrmDiagnosisList, Control).Parent), Me, CType(ofrmDiagnosisList, Control).Parent))

            If IsNothing(ofrmDiagnosisList) = False Then

                If IsNothing(oDiagnosisListControl) = False Then
                    ofrmDiagnosisList.Controls.Remove(oDiagnosisListControl)
                    RemoveHandler oDiagnosisListControl.ItemSelectedClick, AddressOf oDiagnosisListControl_ItemSelectedClick
                    RemoveHandler oDiagnosisListControl.ItemClosedClick, AddressOf oDiagnosisListControl_ItemClosedClick
                    oDiagnosisListControl.Dispose()
                    oDiagnosisListControl = Nothing
                End If
                ofrmDiagnosisList.Dispose()
                ofrmDiagnosisList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowseCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCPT.Click
        Try
            'Code Added By Dipak 20090910 For Display List Control on click of CPTBrowseCPT button
            ofrmList = New frmViewListControl
            'Dim arrCPTTextSplit As String()
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
            ofrmList.ShowDialog(IIf(IsNothing(CType(ofrmList, Control).Parent), Me, CType(ofrmList, Control).Parent))

            If IsNothing(ofrmList) = False Then

                RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
                RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                ofrmList.Controls.Remove(oListControl)
                oListControl.Dispose()
                oListControl = Nothing
                ofrmList.Dispose()
                ofrmList = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnVaccineType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVaccineType.Click

    End Sub

    Private Sub btnClearVaccineType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearVaccineType.Click

    End Sub

    Public Function DeleteVISMapping(ByVal VIS_MST_Id) As Integer
        Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter = Nothing
        Dim Output As Integer
        Try
            oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@vis_mst_Id"
            oParamater.Value = VIS_MST_Id
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Output = oDB.Add("DEL_VIS_Mapping")

        Catch ex As Exception
            MessageBox.Show("Error while deleting VIS mapping", "Error", MessageBoxButtons.OK)

        Finally
            If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
        End Try
        Return Output

    End Function

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

            If GetRequirefunding() = "1" Then
                If cmbFundingSource.Text.Trim = "" Then
                    MessageBox.Show("Select Funding.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    cmbFundingSource.Select()
                    Exit Sub
                End If
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

            If DiagnosisCode.Trim = "" Then
                ICDType = 9
            End If

            Dim _ReceivedDate As String = ""
            Dim _expiryDate As String = ""
            Dim _PublicationDate As String = ""

            If dtDateReceived.Checked = True Then
                _ReceivedDate = dtDateReceived.Value.ToShortDateString()
            End If

            If dtExpiryDate.Checked = True Then
                _expiryDate = dtExpiryDate.Value.ToShortDateString()
            End If

            If dtVISDate.Checked = True Then
                _PublicationDate = dtVISDate.Value.ToShortDateString()
            End If

            Dim dtVIS As New DataTable()

            dtVIS = C1VIS.DataSource


            _ReturnID = oDM.AddData(txtCount.Text.Trim, CPTCode.Trim, txtVaccineCode.Text.Trim, lblConceptID.Text, lblDescriptionID.Text, lblSnoMedID.Text, IM_strDefination, IM_strRxNormCode, IM_strNDCCode, IM_strSnomedDescription, _SKU, _ReceivedDate, cmbStatus.Text.Trim, txtCvx.Text.Trim, txtMvx.Text.Trim, txtTradeName.Text.Trim, txtLotNo.Text.Trim, _expiryDate, Val(txtVials.Text.Trim), Val(txtDosespervial.Text.Trim), Val(txtDosesOnHand.Text.Trim), txtVIS.Text.Trim, _PublicationDate, DiagnosisCode.Trim, _NDCCode, cmbFundingSource.Text.Trim, txtComments.Text.Trim, _EditID, _DocumentID, cmbLocation.Text.ToString().Trim(), chkTracVaccinekInv.Checked, cmbCategory.SelectedValue, dtVIS, ICDType)
            ''ICDType added for ICd10 implementation
            If _ReturnID <> 0 Then
                txtCount.Text = ""

                cmbCPT.DataSource = Nothing
                cmbCPT.Items.Clear()
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

    Private Sub GetRefreshedCategory()
        Dim oDM As New gloStream.Immunization.ItemSetup
        Dim dsCategory As New DataSet
        dsCategory = oDM.getVaccineTypes(0)
        cmbCategory.DataSource = dsCategory.Tables("Category")
        cmbCategory.ValueMember = "nCategoryID"
        cmbCategory.DisplayMember = "sDescription"
        cmbCategory.SelectedIndex = -1
        oDM = Nothing
    End Sub

#End Region

#Region "MouseHover Event"

    Private Sub btnScan_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnScan, "Scan/Import Document")
    End Sub

    Private Sub btnView_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolTip1.SetToolTip(btnView, "View Document")
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

    Private Sub btnScanVIS_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScanVIS.MouseHover
        'If btnScanVIS.Text = "Scan/Import" Then
        '    ToolTip1.SetToolTip(btnScanVIS, "Scan/Import Document")
        'Else
        '    ToolTip1.SetToolTip(btnScanVIS, "View Document")
        'End If
    End Sub

#End Region

#Region "CheckedChanged"

    Private Sub chkTracVaccinekInv_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTracVaccinekInv.CheckedChanged
        If chkTracVaccinekInv.Checked And chkTracVaccinekInv.Enabled = True Then
            txtVials.Enabled = True
            txtDosespervial.Enabled = True
            txtDosesOnHand.Enabled = True
        Else
            txtVials.Enabled = False
            txtDosespervial.Enabled = False
            txtDosesOnHand.Enabled = False
        End If
    End Sub

#End Region

    Private Sub Designgrid()
        Try
            C1VIS.Visible = True

            C1VIS.Rows.Fixed = 1
            If C1VIS.Rows.Count > 1 Then
                C1VIS.Cols(3).DataType = GetType(System.Boolean)
                C1VIS.Cols(7).DataType = GetType(System.String)
            End If

            C1VIS.Cols(0).Caption = "ID"
            C1VIS.Cols(1).Caption = "Item Id"
            C1VIS.Cols(2).Caption = "Document Id"
            C1VIS.Cols(3).Caption = ""
            C1VIS.Cols(4).Caption = "Document Name"
            C1VIS.Cols(5).Caption = "CVX Code"
            C1VIS.Cols(6).Caption = "Encoded text"
            C1VIS.Cols(7).Caption = "Edition Date"

            C1VIS.Cols(0).Visible = False
            C1VIS.Cols(1).Visible = False
            C1VIS.Cols(2).Visible = False
            C1VIS.Cols(3).Visible = True
            C1VIS.Cols(4).Visible = True
            C1VIS.Cols(5).Visible = True
            C1VIS.Cols(6).Visible = True
            C1VIS.Cols(7).Visible = True

            C1VIS.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(4).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(5).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            C1VIS.Cols(7).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            Dim nWidth As Integer = C1VIS.Width

            C1VIS.Cols(3).Width = CInt((0.1 * (nWidth)))
            C1VIS.Cols(4).Width = CInt((0.3 * (nWidth)))
            C1VIS.Cols(5).Width = CInt((0.1 * (nWidth)))
            C1VIS.Cols(6).Width = CInt((0.3 * (nWidth)))
            C1VIS.Cols(7).Width = CInt((0.2 * (nWidth)))

            C1VIS.ShowCellLabels = True

            C1VIS.Cols(3).AllowEditing = False
            C1VIS.Cols(4).AllowEditing = False
            C1VIS.Cols(5).AllowEditing = False
            C1VIS.Cols(6).AllowEditing = False
            C1VIS.Cols(7).AllowEditing = False

        Catch ex As Exception
            MessageBox.Show("Error while designing VIS grid", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub fillGrid()
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtVIS As New DataTable()
        Dim Vaccine_Data As String()
        Dim CVX_Code As String = 0

        Try
            If txtCvx.Text <> "" Then
                Vaccine_Data = Split(txtCvx.Text, "-")
                CVX_Code = Vaccine_Data(0)

                If CVX_Code <> "0" Then
                    oDB.Connect(False)
                    oParam = New gloDatabaseLayer.DBParameters
                    oParam.Add("@CVX_Code", CVX_Code, ParameterDirection.Input, SqlDbType.Text)
                    oParam.Add("@nItemID", _EditID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.Retrive("IM_ShowVIS", oParam, dtVIS)

                    If dtVIS.Rows.Count > 0 Then
                        C1VIS.DataSource = dtVIS
                        Designgrid()
                        C1VIS.Row = 1
                        If Not IsDBNull(C1VIS.GetData(1, 2)) Then
                            _DocumentID = C1VIS.GetData(1, 2)
                        Else
                            _DocumentID = 0
                        End If
                        isVISGridFilled = True
                    Else
                        C1VIS.DataSource = dtVIS
                        Designgrid()
                        isVISGridFilled = False
                    End If

                    oParam.Dispose()
                    oParam = Nothing

                    oDB.Disconnect()
                    oDB = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error while fill grid", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub tabSetup_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tabSetup.SelectedIndexChanged
        If tabSetup.SelectedIndex = 0 Then
            btn_tls_VIS.Visible = False
        ElseIf tabSetup.SelectedIndex = 1 Then
            If isVISGridFilled = True Then
                If C1VIS.Rows.Count > 1 Then
                    Dim Vaccine_Data As String()
                    Dim CVX_Code As Int32 = 0
                    If txtCvx.Text <> "" Then
                        Vaccine_Data = Split(txtCvx.Text, "-")
                        CVX_Code = Vaccine_Data(0)
                    End If
                    If CVX_Code <> C1VIS.GetData(1, 5) Then
                        fillGrid()
                    End If
                Else
                    fillGrid()
                End If
            Else
                fillGrid()
            End If
            If C1VIS.Rows.Count > 1 Then
                btn_tls_VIS.Visible = True
            Else
                btn_tls_VIS.Visible = False
            End If
        End If
    End Sub

    Private Sub C1VIS_Click(sender As System.Object, e As System.EventArgs) Handles C1VIS.Click
        If C1VIS.Rows.Count > 1 Then
            Dim selectedRow As Integer = C1VIS.RowSel
            If Not IsDBNull(C1VIS.GetData(selectedRow, 2)) Then
                _DocumentID = C1VIS.GetData(selectedRow, 2)
            Else
                _DocumentID = 0
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
