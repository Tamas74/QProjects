Imports System.IO
Imports gloCommon
Imports gloEMR.gloStreamAdmin
Imports System.Text.RegularExpressions

Public Class frmDoctor
    Inherits System.Windows.Forms.Form
    Dim blnLogoChanged As Boolean
    Public blnModify As Boolean
    Public Shared Imagepath As String
    Private dtActiveStartTime As DateTime = Nothing
    Private dtActiveEndTime As DateTime = Nothing
    Private bValidationFailed = False

    Private SPI As String = String.Empty

    'sarika
    'Dim objPrescriber As AddUpdatePrescriber
    'Dim WithEvents objPrescriber As AddUpdatePrescriber
    Private blnIsSurescriptError As Boolean
    Private blnNewModify As Boolean = False
    Dim objNewProvider As New clsProvider(gstrConnectionString)
    Public strMessage As String = ""
    '--

    Private _nProviderID As Int64 = 0
    Private _bIsCloneProvider As Boolean = False
    Private _nCloneFromProviderID As Int64 = 0
    Private _nClinicID As Int64 = 1
    Private _nAUSPortalID As Int64 = 0

    Private oListControl As gloListControl.gloListControl
    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Taxonomy
    Friend WithEvents pnlPracticeAddresssControl As System.Windows.Forms.Panel

    Public oBussinessAddressContol As gloAddress.gloAddressControl
    Public oPracticeAddressContol As gloAddress.gloAddressControl
    Public oCompanyAddressContol As gloAddress.gloAddressControl
    Public oProviderPhysicalAddressContol As gloAddress.gloAddressControl
    Public oProviderCompanyPhysicalAddressContol As gloAddress.gloAddressControl

    Public oProviderMultipalCompanyAddressContol As gloAddress.gloAddressControl
    Public oProviderMultipalCompanyPhysicalAddressContol As gloAddress.gloAddressControl
    Private _nNoOfProviderCompanies As Int16 = 1
    Private _dtProviderMultipleCompanyData As DataTable
    Private _dtProviderMultipleCompanyRetrivedData As DataTable
    Private _dtProviderMultipleAdditionalIDQual As DataTable
    Private _dtProviderMultipleAdditionalRetrivedIDQual As DataTable
    Private _dsCompanyQualifierDetails As New DataSet
    Private _dtProviderOtherIDsQaulifier As New DataTable
    Dim oClsProviderCompany As ClsProviderCompany
    Private ImgWidth As Int16 = 0
    Private TempLicensekey As String = String.Empty
    Private ISDEMOLicensce As Boolean = False
    Private TempProvider As gloAUSLibrary.Class.TempProviderdata = Nothing



#Region " Windows Controls "
    Friend WithEvents pnlSPI As System.Windows.Forms.Panel
    Friend WithEvents lblSPI As System.Windows.Forms.Label
    Friend WithEvents txtSPI As System.Windows.Forms.TextBox
    Friend WithEvents lblRoot As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents chckDisable As System.Windows.Forms.CheckBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents dtpActiveEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpActiveStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents chckRefill As System.Windows.Forms.CheckBox
    Friend WithEvents chckNew As System.Windows.Forms.CheckBox
    Friend WithEvents rbPrescriber As System.Windows.Forms.RadioButton
    Friend WithEvents rbPrescriberLocation As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdate As System.Windows.Forms.RadioButton
    Friend WithEvents mtxt_SSNno As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblDEA As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cmbDoctorType As System.Windows.Forms.ComboBox
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents lblNPI As System.Windows.Forms.Label
    Friend WithEvents txt_EmployerID As System.Windows.Forms.TextBox
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents label27 As System.Windows.Forms.Label
    Friend WithEvents lblUPIN As System.Windows.Forms.Label
    Friend WithEvents label28 As System.Windows.Forms.Label
    Friend WithEvents lblStateMedicalLicense As System.Windows.Forms.Label
    Friend WithEvents txtStateMedicalLicenseNo As System.Windows.Forms.TextBox
    Private WithEvents btn_BrowseTaxonomy As System.Windows.Forms.Button
    Private WithEvents btn_ClearTaxonomy As System.Windows.Forms.Button
    Friend WithEvents txtTaxonomy As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
    Friend WithEvents lblNickName As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtNickName As System.Windows.Forms.TextBox
    Private WithEvents c1ProviderIdentification As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtImagePath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents optBrowse As System.Windows.Forms.RadioButton
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCapture As System.Windows.Forms.Button
    Friend WithEvents picSignature As System.Windows.Forms.PictureBox
    Friend WithEvents optMale As System.Windows.Forms.RadioButton
    Friend WithEvents optFemale As System.Windows.Forms.RadioButton
    Friend WithEvents txtBMContact As System.Windows.Forms.TextBox
    Friend WithEvents label26 As System.Windows.Forms.Label
    Friend WithEvents txtBMAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtBMAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBMstate As System.Windows.Forms.TextBox
    Friend WithEvents txtBMCity As System.Windows.Forms.TextBox
    Friend WithEvents txtBMZip As System.Windows.Forms.TextBox
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents txtBMURL As System.Windows.Forms.TextBox
    Friend WithEvents txtBMEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblURL As System.Windows.Forms.Label
    Friend WithEvents lblPager As System.Windows.Forms.Label
    Private WithEvents lblName As System.Windows.Forms.Label
    Private WithEvents txtSuffix As System.Windows.Forms.TextBox
    Private WithEvents txtPrefix As System.Windows.Forms.TextBox
    Private WithEvents txtFirstName As System.Windows.Forms.TextBox
    Private WithEvents txtMiddleName As System.Windows.Forms.TextBox
    Private WithEvents label45 As System.Windows.Forms.Label
    Private WithEvents txtLastName As System.Windows.Forms.TextBox
    Private WithEvents lblPrefix As System.Windows.Forms.Label
    Private WithEvents lblFirstName As System.Windows.Forms.Label
    Private WithEvents lblMiddleName As System.Windows.Forms.Label
    Private WithEvents lblLastName As System.Windows.Forms.Label
    Friend WithEvents txtBPracContactName As System.Windows.Forms.TextBox
    Friend WithEvents label25 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents txtBPracUrl As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracEMail As System.Windows.Forms.TextBox
    Friend WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents chkAddressasAbove As System.Windows.Forms.CheckBox
    Friend WithEvents txtBPracAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents txtBPracAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracState As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracCity As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracZIP As System.Windows.Forms.TextBox
    Friend WithEvents mskBMPhoneNo As gloMaskControl.gloMaskBox
    Friend WithEvents mskMobileNo As gloMaskControl.gloMaskBox
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents pnlBussinessAddresssControl As System.Windows.Forms.Panel
    Private WithEvents rbNo As System.Windows.Forms.RadioButton
    Private WithEvents rbUsePlanSetting As System.Windows.Forms.RadioButton
    Private WithEvents rbAlways As System.Windows.Forms.RadioButton
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents mskBPracPager As gloMaskControl.gloMaskBox
    Friend WithEvents mskBMPager As gloMaskControl.gloMaskBox
    Friend WithEvents mskBMFax As gloMaskControl.gloMaskBox
    Friend WithEvents mskBPracFax As gloMaskControl.gloMaskBox
    Friend WithEvents mskTxtUPIN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskTxtDEA As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbpgProvider As System.Windows.Forms.TabPage
    Friend WithEvents tbpgBillingID As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents grpSPI As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents gbProviderIdentification As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents tbpgStatement As System.Windows.Forms.TabPage
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents txtExternalCode As System.Windows.Forms.TextBox
    Friend WithEvents lblExternal_Code As System.Windows.Forms.Label
    Friend WithEvents txtLabId As System.Windows.Forms.TextBox
    Friend WithEvents lblLabID As System.Windows.Forms.Label
    Friend WithEvents tbpgProviderCompany As System.Windows.Forms.TabPage
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents mskCompanyFax As gloMaskControl.gloMaskBox
    Friend WithEvents label46 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents pnlCompanyAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents txtCompanyPhone As gloMaskControl.gloMaskBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents label18 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyState As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyContactName As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyZip As System.Windows.Forms.TextBox
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyCity As System.Windows.Forms.TextBox
    Friend WithEvents label47 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents label15 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Friend WithEvents label16 As System.Windows.Forms.Label
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyEmail As System.Windows.Forms.TextBox
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyTaxID As System.Windows.Forms.TextBox
    Friend WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents chkCompanyAsAbove As System.Windows.Forms.CheckBox
    Friend WithEvents txtCompanyAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents mskPLFax As gloMaskControl.gloMaskBox
    Friend WithEvents mskPLPager As gloMaskControl.gloMaskBox
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents pnlPLAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents maskedPLPhno As gloMaskControl.gloMaskBox
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents txtPLContactName As System.Windows.Forms.TextBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents txtPLUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents txtPLEMail As System.Windows.Forms.TextBox
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents mskBIDPLFax As gloMaskControl.gloMaskBox
    Friend WithEvents mskBIDPLPager As gloMaskControl.gloMaskBox
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents pnlBIDPLAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents maskedBIDPLPhno As gloMaskControl.gloMaskBox
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents txtBIDPLContactName As System.Windows.Forms.TextBox
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents txtBIDPLUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label132 As System.Windows.Forms.Label
    Friend WithEvents txtBIDPLEMail As System.Windows.Forms.TextBox
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Label134 As System.Windows.Forms.Label
    Friend WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents Label137 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents txtDPSNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblDPSNumber As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Private WithEvents c1CompanyProvIdentification As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtCmpTaxonomyCode As System.Windows.Forms.TextBox
    Private WithEvents btn_ClearCmpTaxonomy As System.Windows.Forms.Button
    Private WithEvents btn_BrowseCmpTaxonomy As System.Windows.Forms.Button
    Friend WithEvents chkRequire_Supervising_Provider_for_eRx As System.Windows.Forms.CheckBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents lblPreDetails As System.Windows.Forms.Label
    Friend WithEvents tbpgPrvdrMultCmpny As System.Windows.Forms.TabPage
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents Label170 As System.Windows.Forms.Label
    Friend WithEvents Label171 As System.Windows.Forms.Label
    Friend WithEvents mskMltCompanyPhysFax As gloMaskControl.gloMaskBox
    Friend WithEvents mskMltCompanyPhysPager As gloMaskControl.gloMaskBox
    Friend WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Label174 As System.Windows.Forms.Label
    Friend WithEvents pnlMltCompanyPhysAdd As System.Windows.Forms.Panel
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents mskMltCompanyPhys As gloMaskControl.gloMaskBox
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyPhysContactName As System.Windows.Forms.TextBox
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents TextBox23 As System.Windows.Forms.TextBox
    Friend WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents TextBox24 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox25 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox26 As System.Windows.Forms.TextBox
    Friend WithEvents txtMltCompanyPhysURL As System.Windows.Forms.TextBox
    Friend WithEvents Label179 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyPhysMail As System.Windows.Forms.TextBox
    Friend WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents Label181 As System.Windows.Forms.Label
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents TextBox29 As System.Windows.Forms.TextBox
    Friend WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Private WithEvents c1MltCompantAddtnlID As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyTaxonomy As System.Windows.Forms.TextBox
    Private WithEvents btnTaxonomyClear As System.Windows.Forms.Button
    Private WithEvents btnTaxonomySelect As System.Windows.Forms.Button
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents mskMltCompanyFax As gloMaskControl.gloMaskBox
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyNPI As System.Windows.Forms.TextBox
    Friend WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents pnlmltcompanymaillingAdd As System.Windows.Forms.Panel
    Friend WithEvents mskmltCompanyPhoneNo As gloMaskControl.gloMaskBox
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents TxtMltCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents txtmltCompanyContact As System.Windows.Forms.TextBox
    Friend WithEvents TextBox16 As System.Windows.Forms.TextBox
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents TextBox17 As System.Windows.Forms.TextBox
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents TextBox18 As System.Windows.Forms.TextBox
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents txtMltCompanyTaxId As System.Windows.Forms.TextBox
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Private WithEvents chkmltSameAsPrvdrAdd As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox21 As System.Windows.Forms.TextBox
    Friend WithEvents CmbProviderCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label184 As System.Windows.Forms.Label
    Friend WithEvents lblDirectAddressValue As System.Windows.Forms.Label
    Friend WithEvents lblDirectAddress As System.Windows.Forms.Label
    Friend WithEvents ChkCIEvent As System.Windows.Forms.CheckBox
    Friend WithEvents chkCIMessage As System.Windows.Forms.CheckBox
    Friend WithEvents Label172 As System.Windows.Forms.Label
    Friend WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents mtxtDOB As System.Windows.Forms.MaskedTextBox
    Private WithEvents lbPatientDOB As System.Windows.Forms.Label
    Friend WithEvents lblAusStatus As System.Windows.Forms.Label
    Friend WithEvents btnLicenseRefresh As System.Windows.Forms.Button
    Private WithEvents txtLicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents ImgLicenseKey As System.Windows.Forms.ImageList
    Friend WithEvents pnlLicenseKey As System.Windows.Forms.Panel
    Friend WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents Label189 As System.Windows.Forms.Label
    Private WithEvents Label191 As System.Windows.Forms.Label
    Friend WithEvents lblLicenseMessage As System.Windows.Forms.Label
    Friend WithEvents maskedBpracPhno As gloMaskControl.gloMaskBox
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal nProviderID As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        _nProviderID = nProviderID
        _bIsCloneProvider = False
        _nCloneFromProviderID = 0
        InitializeComponent()
        ''dhruv adding the control

        gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country)

        oBussinessAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oBussinessAddressContol.Dock = DockStyle.Fill
        pnlBussinessAddresssControl.Controls.Add(oBussinessAddressContol)

        oPracticeAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oPracticeAddressContol.Dock = DockStyle.Fill
        pnlPracticeAddresssControl.Controls.Add(oPracticeAddressContol)

        oCompanyAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oCompanyAddressContol.Dock = DockStyle.Fill
        pnlCompanyAddresssControl.Controls.Add(oCompanyAddressContol)

        oProviderPhysicalAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oProviderPhysicalAddressContol.Dock = DockStyle.Fill
        pnlBIDPLAddresssControl.Controls.Add(oProviderPhysicalAddressContol)

        oProviderCompanyPhysicalAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oProviderCompanyPhysicalAddressContol.Dock = DockStyle.Fill
        pnlPLAddresssControl.Controls.Add(oProviderCompanyPhysicalAddressContol)

        'oBussinessAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        'oBussinessAddressContol.Dock = DockStyle.Fill
        'oBussinessAddressContol.txtAreaCode.Visible = True
        'oBussinessAddressContol.txtArea.Visible = True
        'oBussinessAddressContol.txtZip.Size = New Size(43, 22)
        'pnlPLAddresssControl.Controls.Add(oBussinessAddressContol)

        'oBussinessAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        'oBussinessAddressContol.Dock = DockStyle.Fill
        'oBussinessAddressContol.txtAreaCode.Visible = True
        'oBussinessAddressContol.txtArea.Visible = True
        'oBussinessAddressContol.txtZip.Size = New Size(43, 22)
        'pnlBIDPLAddresssControl.Controls.Add(oBussinessAddressContol)

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal nProviderID As Int64, ByVal IsForClone As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        '_nProviderID = nProviderID
        _nProviderID = 0
        _nCloneFromProviderID = nProviderID
        _bIsCloneProvider = True

        InitializeComponent()
        ''dhruv adding the control

        gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country)

        oBussinessAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oBussinessAddressContol.Dock = DockStyle.Fill
        pnlBussinessAddresssControl.Controls.Add(oBussinessAddressContol)

        oPracticeAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oPracticeAddressContol.Dock = DockStyle.Fill
        pnlPracticeAddresssControl.Controls.Add(oPracticeAddressContol)

        oCompanyAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oCompanyAddressContol.Dock = DockStyle.Fill
        pnlCompanyAddresssControl.Controls.Add(oCompanyAddressContol)

        oProviderPhysicalAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oProviderPhysicalAddressContol.Dock = DockStyle.Fill
        pnlBIDPLAddresssControl.Controls.Add(oProviderPhysicalAddressContol)

        oProviderCompanyPhysicalAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
        oProviderCompanyPhysicalAddressContol.Dock = DockStyle.Fill
        pnlPLAddresssControl.Controls.Add(oProviderCompanyPhysicalAddressContol)

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
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDoctor))
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rbNo = New System.Windows.Forms.RadioButton()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.rbUsePlanSetting = New System.Windows.Forms.RadioButton()
        Me.rbAlways = New System.Windows.Forms.RadioButton()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.c1ProviderIdentification = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlSPI = New System.Windows.Forms.Panel()
        Me.lblDirectAddressValue = New System.Windows.Forms.Label()
        Me.lblDirectAddress = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.lblSPI = New System.Windows.Forms.Label()
        Me.txtSPI = New System.Windows.Forms.TextBox()
        Me.lblRoot = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.chckDisable = New System.Windows.Forms.CheckBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.dtpActiveEndTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpActiveStartTime = New System.Windows.Forms.DateTimePicker()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.chckRefill = New System.Windows.Forms.CheckBox()
        Me.chckNew = New System.Windows.Forms.CheckBox()
        Me.rbPrescriber = New System.Windows.Forms.RadioButton()
        Me.rbPrescriberLocation = New System.Windows.Forms.RadioButton()
        Me.rbUpdate = New System.Windows.Forms.RadioButton()
        Me.mskTxtDEA = New System.Windows.Forms.MaskedTextBox()
        Me.mskTxtUPIN = New System.Windows.Forms.MaskedTextBox()
        Me.mtxt_SSNno = New System.Windows.Forms.MaskedTextBox()
        Me.lblDEA = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbDoctorType = New System.Windows.Forms.ComboBox()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.lblNPI = New System.Windows.Forms.Label()
        Me.txt_EmployerID = New System.Windows.Forms.TextBox()
        Me.txtNPI = New System.Windows.Forms.TextBox()
        Me.label27 = New System.Windows.Forms.Label()
        Me.lblUPIN = New System.Windows.Forms.Label()
        Me.label28 = New System.Windows.Forms.Label()
        Me.lblStateMedicalLicense = New System.Windows.Forms.Label()
        Me.txtStateMedicalLicenseNo = New System.Windows.Forms.TextBox()
        Me.btn_BrowseTaxonomy = New System.Windows.Forms.Button()
        Me.btn_ClearTaxonomy = New System.Windows.Forms.Button()
        Me.txtTaxonomy = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirmPassword = New System.Windows.Forms.Label()
        Me.lblNickName = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtNickName = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtImagePath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.optBrowse = New System.Windows.Forms.RadioButton()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.picSignature = New System.Windows.Forms.PictureBox()
        Me.optMale = New System.Windows.Forms.RadioButton()
        Me.optFemale = New System.Windows.Forms.RadioButton()
        Me.lblPhoneNo = New System.Windows.Forms.Label()
        Me.lblPager = New System.Windows.Forms.Label()
        Me.pnlBussinessAddresssControl = New System.Windows.Forms.Panel()
        Me.txtBMContact = New System.Windows.Forms.TextBox()
        Me.label26 = New System.Windows.Forms.Label()
        Me.txtBMAddress1 = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtBMAddress2 = New System.Windows.Forms.TextBox()
        Me.txtBMstate = New System.Windows.Forms.TextBox()
        Me.txtBMCity = New System.Windows.Forms.TextBox()
        Me.txtBMZip = New System.Windows.Forms.TextBox()
        Me.txtBMURL = New System.Windows.Forms.TextBox()
        Me.txtBMEmailAddress = New System.Windows.Forms.TextBox()
        Me.lblURL = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.label45 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.lblFirstName = New System.Windows.Forms.Label()
        Me.lblMiddleName = New System.Windows.Forms.Label()
        Me.lblLastName = New System.Windows.Forms.Label()
        Me.label13 = New System.Windows.Forms.Label()
        Me.pnlPracticeAddresssControl = New System.Windows.Forms.Panel()
        Me.txtBPracContactName = New System.Windows.Forms.TextBox()
        Me.label25 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.txtBPracUrl = New System.Windows.Forms.TextBox()
        Me.txtBPracEMail = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.chkAddressasAbove = New System.Windows.Forms.CheckBox()
        Me.txtBPracAddress1 = New System.Windows.Forms.TextBox()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.txtBPracAddress2 = New System.Windows.Forms.TextBox()
        Me.txtBPracState = New System.Windows.Forms.TextBox()
        Me.txtBPracCity = New System.Windows.Forms.TextBox()
        Me.txtBPracZIP = New System.Windows.Forms.TextBox()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.mskBPracFax = New gloMaskControl.gloMaskBox()
        Me.mskBPracPager = New gloMaskControl.gloMaskBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.maskedBpracPhno = New gloMaskControl.gloMaskBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbpgProvider = New System.Windows.Forms.TabPage()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.chkRequire_Supervising_Provider_for_eRx = New System.Windows.Forms.CheckBox()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.grpSPI = New System.Windows.Forms.Panel()
        Me.ChkCIEvent = New System.Windows.Forms.CheckBox()
        Me.chkCIMessage = New System.Windows.Forms.CheckBox()
        Me.lblPreDetails = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.pnlLicenseKey = New System.Windows.Forms.Panel()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.btnLicenseRefresh = New System.Windows.Forms.Button()
        Me.ImgLicenseKey = New System.Windows.Forms.ImageList(Me.components)
        Me.Label186 = New System.Windows.Forms.Label()
        Me.txtLicenseKey = New System.Windows.Forms.TextBox()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblAusStatus = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.txtDPSNumber = New System.Windows.Forms.TextBox()
        Me.lblDPSNumber = New System.Windows.Forms.Label()
        Me.txtExternalCode = New System.Windows.Forms.TextBox()
        Me.lblExternal_Code = New System.Windows.Forms.Label()
        Me.txtLabId = New System.Windows.Forms.TextBox()
        Me.lblLabID = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.mskMobileNo = New gloMaskControl.gloMaskBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.mskBMPhoneNo = New gloMaskControl.gloMaskBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.mskBMPager = New gloMaskControl.gloMaskBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.mskBMFax = New gloMaskControl.gloMaskBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.mtxtDOB = New System.Windows.Forms.MaskedTextBox()
        Me.lbPatientDOB = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.tbpgBillingID = New System.Windows.Forms.TabPage()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.mskBIDPLFax = New gloMaskControl.gloMaskBox()
        Me.mskBIDPLPager = New gloMaskControl.gloMaskBox()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.pnlBIDPLAddresssControl = New System.Windows.Forms.Panel()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.maskedBIDPLPhno = New gloMaskControl.gloMaskBox()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.txtBIDPLContactName = New System.Windows.Forms.TextBox()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.txtBIDPLUrl = New System.Windows.Forms.TextBox()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.txtBIDPLEMail = New System.Windows.Forms.TextBox()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.gbProviderIdentification = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.tbpgProviderCompany = New System.Windows.Forms.TabPage()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.mskPLFax = New gloMaskControl.gloMaskBox()
        Me.mskPLPager = New gloMaskControl.gloMaskBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.pnlPLAddresssControl = New System.Windows.Forms.Panel()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.maskedPLPhno = New gloMaskControl.gloMaskBox()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.txtPLContactName = New System.Windows.Forms.TextBox()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.txtPLUrl = New System.Windows.Forms.TextBox()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.txtPLEMail = New System.Windows.Forms.TextBox()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.c1CompanyProvIdentification = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtCmpTaxonomyCode = New System.Windows.Forms.TextBox()
        Me.btn_ClearCmpTaxonomy = New System.Windows.Forms.Button()
        Me.btn_BrowseCmpTaxonomy = New System.Windows.Forms.Button()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.mskCompanyFax = New gloMaskControl.gloMaskBox()
        Me.label46 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtCompanyNPI = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.pnlCompanyAddresssControl = New System.Windows.Forms.Panel()
        Me.txtCompanyPhone = New gloMaskControl.gloMaskBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.label18 = New System.Windows.Forms.Label()
        Me.txtCompanyState = New System.Windows.Forms.TextBox()
        Me.txtCompanyContactName = New System.Windows.Forms.TextBox()
        Me.txtCompanyZip = New System.Windows.Forms.TextBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.txtCompanyCity = New System.Windows.Forms.TextBox()
        Me.label47 = New System.Windows.Forms.Label()
        Me.txtCompanyAddress2 = New System.Windows.Forms.TextBox()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label16 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.txtCompanyEmail = New System.Windows.Forms.TextBox()
        Me.label20 = New System.Windows.Forms.Label()
        Me.txtCompanyTaxID = New System.Windows.Forms.TextBox()
        Me.label19 = New System.Windows.Forms.Label()
        Me.chkCompanyAsAbove = New System.Windows.Forms.CheckBox()
        Me.txtCompanyAddress1 = New System.Windows.Forms.TextBox()
        Me.tbpgStatement = New System.Windows.Forms.TabPage()
        Me.tbpgPrvdrMultCmpny = New System.Windows.Forms.TabPage()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.mskMltCompanyPhys = New gloMaskControl.gloMaskBox()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.mskMltCompanyPhysFax = New gloMaskControl.gloMaskBox()
        Me.mskMltCompanyPhysPager = New gloMaskControl.gloMaskBox()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.pnlMltCompanyPhysAdd = New System.Windows.Forms.Panel()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.txtMltCompanyPhysContactName = New System.Windows.Forms.TextBox()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.TextBox23 = New System.Windows.Forms.TextBox()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.TextBox24 = New System.Windows.Forms.TextBox()
        Me.TextBox25 = New System.Windows.Forms.TextBox()
        Me.TextBox26 = New System.Windows.Forms.TextBox()
        Me.txtMltCompanyPhysURL = New System.Windows.Forms.TextBox()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.txtMltCompanyPhysMail = New System.Windows.Forms.TextBox()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.TextBox29 = New System.Windows.Forms.TextBox()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.c1MltCompantAddtnlID = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.CmbProviderCompany = New System.Windows.Forms.ComboBox()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtMltCompanyTaxonomy = New System.Windows.Forms.TextBox()
        Me.btnTaxonomyClear = New System.Windows.Forms.Button()
        Me.btnTaxonomySelect = New System.Windows.Forms.Button()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.mskMltCompanyFax = New gloMaskControl.gloMaskBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.txtMltCompanyNPI = New System.Windows.Forms.TextBox()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.pnlmltcompanymaillingAdd = New System.Windows.Forms.Panel()
        Me.mskmltCompanyPhoneNo = New gloMaskControl.gloMaskBox()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.TxtMltCompanyName = New System.Windows.Forms.TextBox()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.txtmltCompanyContact = New System.Windows.Forms.TextBox()
        Me.TextBox16 = New System.Windows.Forms.TextBox()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.TextBox17 = New System.Windows.Forms.TextBox()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.TextBox18 = New System.Windows.Forms.TextBox()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.txtMltCompanyEmail = New System.Windows.Forms.TextBox()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.txtMltCompanyTaxId = New System.Windows.Forms.TextBox()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.chkmltSameAsPrvdrAdd = New System.Windows.Forms.CheckBox()
        Me.TextBox21 = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.lblLicenseMessage = New System.Windows.Forms.Label()
        Me.Panel7.SuspendLayout()
        CType(Me.c1ProviderIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSPI.SuspendLayout()
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbpgProvider.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.grpSPI.SuspendLayout()
        Me.pnlLicenseKey.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tbpgBillingID.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.gbProviderIdentification.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.tbpgProviderCompany.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.c1CompanyProvIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel24.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.tbpgStatement.SuspendLayout()
        Me.tbpgPrvdrMultCmpny.SuspendLayout()
        Me.Panel28.SuspendLayout()
        Me.Panel29.SuspendLayout()
        Me.Panel26.SuspendLayout()
        CType(Me.c1MltCompantAddtnlID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel27.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rbNo)
        Me.Panel7.Controls.Add(Me.Label80)
        Me.Panel7.Controls.Add(Me.rbUsePlanSetting)
        Me.Panel7.Controls.Add(Me.rbAlways)
        Me.Panel7.Controls.Add(Me.Label76)
        Me.Panel7.Controls.Add(Me.Label77)
        Me.Panel7.Controls.Add(Me.Label78)
        Me.Panel7.Controls.Add(Me.Label79)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 493)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel7.Size = New System.Drawing.Size(791, 32)
        Me.Panel7.TabIndex = 7
        '
        'rbNo
        '
        Me.rbNo.AutoSize = True
        Me.rbNo.Checked = True
        Me.rbNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbNo.Location = New System.Drawing.Point(589, 6)
        Me.rbNo.Name = "rbNo"
        Me.rbNo.Size = New System.Drawing.Size(40, 18)
        Me.rbNo.TabIndex = 2
        Me.rbNo.TabStop = True
        Me.rbNo.Text = "No"
        Me.rbNo.UseVisualStyleBackColor = True
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.BackColor = System.Drawing.Color.Transparent
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Location = New System.Drawing.Point(1, 4)
        Me.Label80.Name = "Label80"
        Me.Label80.Padding = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.Label80.Size = New System.Drawing.Size(247, 18)
        Me.Label80.TabIndex = 95
        Me.Label80.Text = "Default Prior Authorization Required :"
        '
        'rbUsePlanSetting
        '
        Me.rbUsePlanSetting.AutoSize = True
        Me.rbUsePlanSetting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUsePlanSetting.Location = New System.Drawing.Point(367, 6)
        Me.rbUsePlanSetting.Name = "rbUsePlanSetting"
        Me.rbUsePlanSetting.Size = New System.Drawing.Size(204, 18)
        Me.rbUsePlanSetting.TabIndex = 1
        Me.rbUsePlanSetting.TabStop = True
        Me.rbUsePlanSetting.Text = "Yes - Use Insurance Plan Setting"
        Me.rbUsePlanSetting.UseVisualStyleBackColor = True
        '
        'rbAlways
        '
        Me.rbAlways.AutoSize = True
        Me.rbAlways.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAlways.Location = New System.Drawing.Point(254, 6)
        Me.rbAlways.Name = "rbAlways"
        Me.rbAlways.Size = New System.Drawing.Size(94, 18)
        Me.rbAlways.TabIndex = 0
        Me.rbAlways.Text = "Yes - Always"
        Me.rbAlways.UseVisualStyleBackColor = True
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Location = New System.Drawing.Point(790, 4)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 24)
        Me.Label76.TabIndex = 127
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Location = New System.Drawing.Point(0, 4)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(1, 24)
        Me.Label77.TabIndex = 126
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Location = New System.Drawing.Point(0, 28)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(791, 1)
        Me.Label78.TabIndex = 125
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Location = New System.Drawing.Point(0, 3)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(791, 1)
        Me.Label79.TabIndex = 124
        '
        'c1ProviderIdentification
        '
        Me.c1ProviderIdentification.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1ProviderIdentification.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform
        Me.c1ProviderIdentification.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1ProviderIdentification.AutoGenerateColumns = False
        Me.c1ProviderIdentification.BackColor = System.Drawing.Color.GhostWhite
        Me.c1ProviderIdentification.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1ProviderIdentification.ColumnInfo = "1,1,0,0,0,95,Columns:"
        Me.c1ProviderIdentification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1ProviderIdentification.ExtendLastCol = True
        Me.c1ProviderIdentification.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1ProviderIdentification.ForeColor = System.Drawing.SystemColors.ControlText
        Me.c1ProviderIdentification.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.c1ProviderIdentification.Location = New System.Drawing.Point(1, 24)
        Me.c1ProviderIdentification.Name = "c1ProviderIdentification"
        Me.c1ProviderIdentification.Rows.Count = 1
        Me.c1ProviderIdentification.Rows.DefaultSize = 19
        Me.c1ProviderIdentification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.c1ProviderIdentification.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1ProviderIdentification.Size = New System.Drawing.Size(789, 307)
        Me.c1ProviderIdentification.StyleInfo = resources.GetString("c1ProviderIdentification.StyleInfo")
        Me.c1ProviderIdentification.TabIndex = 0
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Red
        Me.Label37.Location = New System.Drawing.Point(413, 28)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(14, 14)
        Me.Label37.TabIndex = 192
        Me.Label37.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(142, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 191
        Me.Label6.Text = "*"
        '
        'pnlSPI
        '
        Me.pnlSPI.Controls.Add(Me.lblDirectAddressValue)
        Me.pnlSPI.Controls.Add(Me.lblDirectAddress)
        Me.pnlSPI.Controls.Add(Me.Label113)
        Me.pnlSPI.Controls.Add(Me.Label97)
        Me.pnlSPI.Controls.Add(Me.Label96)
        Me.pnlSPI.Controls.Add(Me.lblSPI)
        Me.pnlSPI.Controls.Add(Me.txtSPI)
        Me.pnlSPI.Controls.Add(Me.lblRoot)
        Me.pnlSPI.Controls.Add(Me.Label29)
        Me.pnlSPI.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSPI.Location = New System.Drawing.Point(0, 600)
        Me.pnlSPI.Name = "pnlSPI"
        Me.pnlSPI.Size = New System.Drawing.Size(791, 26)
        Me.pnlSPI.TabIndex = 8
        Me.pnlSPI.Visible = False
        '
        'lblDirectAddressValue
        '
        Me.lblDirectAddressValue.AutoSize = True
        Me.lblDirectAddressValue.BackColor = System.Drawing.Color.Transparent
        Me.lblDirectAddressValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectAddressValue.ForeColor = System.Drawing.Color.Red
        Me.lblDirectAddressValue.Location = New System.Drawing.Point(525, 6)
        Me.lblDirectAddressValue.Name = "lblDirectAddressValue"
        Me.lblDirectAddressValue.Size = New System.Drawing.Size(0, 14)
        Me.lblDirectAddressValue.TabIndex = 196
        Me.lblDirectAddressValue.Visible = False
        '
        'lblDirectAddress
        '
        Me.lblDirectAddress.AutoSize = True
        Me.lblDirectAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblDirectAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectAddress.ForeColor = System.Drawing.Color.Red
        Me.lblDirectAddress.Location = New System.Drawing.Point(432, 6)
        Me.lblDirectAddress.Name = "lblDirectAddress"
        Me.lblDirectAddress.Size = New System.Drawing.Size(94, 14)
        Me.lblDirectAddress.TabIndex = 195
        Me.lblDirectAddress.Text = "Direct Address :"
        Me.lblDirectAddress.Visible = False
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Location = New System.Drawing.Point(1, 25)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(789, 1)
        Me.Label113.TabIndex = 130
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Location = New System.Drawing.Point(790, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 26)
        Me.Label97.TabIndex = 128
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Location = New System.Drawing.Point(0, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 26)
        Me.Label96.TabIndex = 127
        '
        'lblSPI
        '
        Me.lblSPI.AutoSize = True
        Me.lblSPI.BackColor = System.Drawing.Color.Transparent
        Me.lblSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSPI.ForeColor = System.Drawing.Color.Red
        Me.lblSPI.Location = New System.Drawing.Point(318, 6)
        Me.lblSPI.Name = "lblSPI"
        Me.lblSPI.Size = New System.Drawing.Size(0, 14)
        Me.lblSPI.TabIndex = 82
        Me.lblSPI.Visible = False
        '
        'txtSPI
        '
        Me.txtSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSPI.ForeColor = System.Drawing.Color.Black
        Me.txtSPI.Location = New System.Drawing.Point(72, 1)
        Me.txtSPI.MaxLength = 10
        Me.txtSPI.Name = "txtSPI"
        Me.txtSPI.Size = New System.Drawing.Size(160, 22)
        Me.txtSPI.TabIndex = 0
        '
        'lblRoot
        '
        Me.lblRoot.AutoSize = True
        Me.lblRoot.BackColor = System.Drawing.Color.Transparent
        Me.lblRoot.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoot.Location = New System.Drawing.Point(6, 5)
        Me.lblRoot.Name = "lblRoot"
        Me.lblRoot.Size = New System.Drawing.Size(63, 14)
        Me.lblRoot.TabIndex = 71
        Me.lblRoot.Text = "Root SPI :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Red
        Me.Label29.Location = New System.Drawing.Point(241, 6)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(78, 14)
        Me.Label29.TabIndex = 73
        Me.Label29.Text = "Current SPI :"
        Me.Label29.Visible = False
        '
        'chckDisable
        '
        Me.chckDisable.AutoSize = True
        Me.chckDisable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckDisable.Location = New System.Drawing.Point(141, 27)
        Me.chckDisable.Name = "chckDisable"
        Me.chckDisable.Size = New System.Drawing.Size(63, 18)
        Me.chckDisable.TabIndex = 3
        Me.chckDisable.Text = "Disable"
        Me.chckDisable.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(339, 52)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(104, 14)
        Me.Label32.TabIndex = 80
        Me.Label32.Text = "Active End Date :"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(28, 52)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(110, 14)
        Me.Label31.TabIndex = 79
        Me.Label31.Text = "Active Start Date :"
        '
        'dtpActiveEndTime
        '
        Me.dtpActiveEndTime.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveEndTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpActiveEndTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpActiveEndTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpActiveEndTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpActiveEndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpActiveEndTime.CustomFormat = "MM/dd/yyyy hh:mm:tt"
        Me.dtpActiveEndTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActiveEndTime.Location = New System.Drawing.Point(446, 48)
        Me.dtpActiveEndTime.Name = "dtpActiveEndTime"
        Me.dtpActiveEndTime.Size = New System.Drawing.Size(157, 22)
        Me.dtpActiveEndTime.TabIndex = 7
        '
        'dtpActiveStartTime
        '
        Me.dtpActiveStartTime.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveStartTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpActiveStartTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpActiveStartTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpActiveStartTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpActiveStartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpActiveStartTime.CustomFormat = " MM/dd/yyyy hh:mm:tt"
        Me.dtpActiveStartTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActiveStartTime.Location = New System.Drawing.Point(141, 48)
        Me.dtpActiveStartTime.Name = "dtpActiveStartTime"
        Me.dtpActiveStartTime.Size = New System.Drawing.Size(162, 22)
        Me.dtpActiveStartTime.TabIndex = 6
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(52, 29)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(86, 14)
        Me.Label30.TabIndex = 76
        Me.Label30.Text = "Service Level :"
        '
        'chckRefill
        '
        Me.chckRefill.AutoSize = True
        Me.chckRefill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckRefill.Location = New System.Drawing.Point(305, 27)
        Me.chckRefill.Name = "chckRefill"
        Me.chckRefill.Size = New System.Drawing.Size(50, 18)
        Me.chckRefill.TabIndex = 5
        Me.chckRefill.Text = "Refill"
        Me.chckRefill.UseVisualStyleBackColor = True
        '
        'chckNew
        '
        Me.chckNew.AutoSize = True
        Me.chckNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckNew.Location = New System.Drawing.Point(229, 27)
        Me.chckNew.Name = "chckNew"
        Me.chckNew.Size = New System.Drawing.Size(51, 18)
        Me.chckNew.TabIndex = 4
        Me.chckNew.Text = "New"
        Me.chckNew.UseVisualStyleBackColor = True
        '
        'rbPrescriber
        '
        Me.rbPrescriber.AutoSize = True
        Me.rbPrescriber.BackColor = System.Drawing.Color.Transparent
        Me.rbPrescriber.Checked = True
        Me.rbPrescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrescriber.Location = New System.Drawing.Point(140, 7)
        Me.rbPrescriber.Name = "rbPrescriber"
        Me.rbPrescriber.Size = New System.Drawing.Size(114, 18)
        Me.rbPrescriber.TabIndex = 0
        Me.rbPrescriber.TabStop = True
        Me.rbPrescriber.Text = "Add Prescriber"
        Me.rbPrescriber.UseVisualStyleBackColor = False
        '
        'rbPrescriberLocation
        '
        Me.rbPrescriberLocation.AutoSize = True
        Me.rbPrescriberLocation.BackColor = System.Drawing.Color.Transparent
        Me.rbPrescriberLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrescriberLocation.Location = New System.Drawing.Point(291, 7)
        Me.rbPrescriberLocation.Name = "rbPrescriberLocation"
        Me.rbPrescriberLocation.Size = New System.Drawing.Size(172, 18)
        Me.rbPrescriberLocation.TabIndex = 1
        Me.rbPrescriberLocation.Text = "Add Prescriber on Location"
        Me.rbPrescriberLocation.UseVisualStyleBackColor = False
        '
        'rbUpdate
        '
        Me.rbUpdate.AutoSize = True
        Me.rbUpdate.BackColor = System.Drawing.Color.Transparent
        Me.rbUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUpdate.Location = New System.Drawing.Point(500, 7)
        Me.rbUpdate.Name = "rbUpdate"
        Me.rbUpdate.Size = New System.Drawing.Size(190, 18)
        Me.rbUpdate.TabIndex = 2
        Me.rbUpdate.Text = "Update Prescriber on Location"
        Me.rbUpdate.UseVisualStyleBackColor = False
        '
        'mskTxtDEA
        '
        Me.mskTxtDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskTxtDEA.Location = New System.Drawing.Point(318, 105)
        Me.mskTxtDEA.Mask = "LL0000000"
        Me.mskTxtDEA.Name = "mskTxtDEA"
        Me.mskTxtDEA.Size = New System.Drawing.Size(82, 22)
        Me.mskTxtDEA.TabIndex = 9
        '
        'mskTxtUPIN
        '
        Me.mskTxtUPIN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskTxtUPIN.Location = New System.Drawing.Point(318, 55)
        Me.mskTxtUPIN.Mask = "L00000"
        Me.mskTxtUPIN.Name = "mskTxtUPIN"
        Me.mskTxtUPIN.Size = New System.Drawing.Size(82, 22)
        Me.mskTxtUPIN.TabIndex = 5
        Me.mskTxtUPIN.Tag = ""
        '
        'mtxt_SSNno
        '
        Me.mtxt_SSNno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxt_SSNno.ForeColor = System.Drawing.Color.Black
        Me.mtxt_SSNno.Location = New System.Drawing.Point(318, 80)
        Me.mtxt_SSNno.Mask = "000-00-0000"
        Me.mtxt_SSNno.Name = "mtxt_SSNno"
        Me.mtxt_SSNno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.mtxt_SSNno.Size = New System.Drawing.Size(82, 22)
        Me.mtxt_SSNno.TabIndex = 7
        Me.mtxt_SSNno.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'lblDEA
        '
        Me.lblDEA.AutoSize = True
        Me.lblDEA.BackColor = System.Drawing.Color.Transparent
        Me.lblDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDEA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDEA.Location = New System.Drawing.Point(280, 109)
        Me.lblDEA.Name = "lblDEA"
        Me.lblDEA.Size = New System.Drawing.Size(38, 14)
        Me.lblDEA.TabIndex = 108
        Me.lblDEA.Text = "DEA :"
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(52, 84)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(43, 14)
        Me.lblType.TabIndex = 115
        Me.lblType.Text = "Type :"
        '
        'cmbDoctorType
        '
        Me.cmbDoctorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoctorType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoctorType.ForeColor = System.Drawing.Color.Black
        Me.cmbDoctorType.Location = New System.Drawing.Point(97, 80)
        Me.cmbDoctorType.Name = "cmbDoctorType"
        Me.cmbDoctorType.Size = New System.Drawing.Size(166, 22)
        Me.cmbDoctorType.TabIndex = 8
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMobileNo.Location = New System.Drawing.Point(27, 8)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(68, 14)
        Me.lblMobileNo.TabIndex = 96
        Me.lblMobileNo.Text = "Mobile No :"
        '
        'lblNPI
        '
        Me.lblNPI.AutoSize = True
        Me.lblNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNPI.Location = New System.Drawing.Point(284, 34)
        Me.lblNPI.Name = "lblNPI"
        Me.lblNPI.Size = New System.Drawing.Size(34, 14)
        Me.lblNPI.TabIndex = 116
        Me.lblNPI.Text = "NPI :"
        '
        'txt_EmployerID
        '
        Me.txt_EmployerID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_EmployerID.ForeColor = System.Drawing.Color.Black
        Me.txt_EmployerID.Location = New System.Drawing.Point(97, 55)
        Me.txt_EmployerID.MaxLength = 9
        Me.txt_EmployerID.Name = "txt_EmployerID"
        Me.txt_EmployerID.Size = New System.Drawing.Size(166, 22)
        Me.txt_EmployerID.TabIndex = 6
        '
        'txtNPI
        '
        Me.txtNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNPI.ForeColor = System.Drawing.Color.Black
        Me.txtNPI.Location = New System.Drawing.Point(318, 30)
        Me.txtNPI.MaxLength = 10
        Me.txtNPI.Name = "txtNPI"
        Me.txtNPI.Size = New System.Drawing.Size(82, 22)
        Me.txtNPI.TabIndex = 1
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label27.Location = New System.Drawing.Point(14, 58)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(81, 14)
        Me.label27.TabIndex = 186
        Me.label27.Text = "Employer ID :"
        '
        'lblUPIN
        '
        Me.lblUPIN.AutoSize = True
        Me.lblUPIN.BackColor = System.Drawing.Color.Transparent
        Me.lblUPIN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUPIN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUPIN.Location = New System.Drawing.Point(276, 59)
        Me.lblUPIN.Name = "lblUPIN"
        Me.lblUPIN.Size = New System.Drawing.Size(42, 14)
        Me.lblUPIN.TabIndex = 117
        Me.lblUPIN.Text = "UPIN :"
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.BackColor = System.Drawing.Color.Transparent
        Me.label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label28.Location = New System.Drawing.Point(277, 84)
        Me.label28.Name = "label28"
        Me.label28.Size = New System.Drawing.Size(41, 14)
        Me.label28.TabIndex = 184
        Me.label28.Text = "SSN  :"
        '
        'lblStateMedicalLicense
        '
        Me.lblStateMedicalLicense.AutoSize = True
        Me.lblStateMedicalLicense.BackColor = System.Drawing.Color.Transparent
        Me.lblStateMedicalLicense.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateMedicalLicense.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStateMedicalLicense.Location = New System.Drawing.Point(186, 8)
        Me.lblStateMedicalLicense.Name = "lblStateMedicalLicense"
        Me.lblStateMedicalLicense.Size = New System.Drawing.Size(132, 14)
        Me.lblStateMedicalLicense.TabIndex = 118
        Me.lblStateMedicalLicense.Text = "State Medical License :"
        '
        'txtStateMedicalLicenseNo
        '
        Me.txtStateMedicalLicenseNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateMedicalLicenseNo.ForeColor = System.Drawing.Color.Black
        Me.txtStateMedicalLicenseNo.Location = New System.Drawing.Point(318, 4)
        Me.txtStateMedicalLicenseNo.MaxLength = 10
        Me.txtStateMedicalLicenseNo.Name = "txtStateMedicalLicenseNo"
        Me.txtStateMedicalLicenseNo.Size = New System.Drawing.Size(82, 22)
        Me.txtStateMedicalLicenseNo.TabIndex = 10
        '
        'btn_BrowseTaxonomy
        '
        Me.btn_BrowseTaxonomy.BackgroundImage = CType(resources.GetObject("btn_BrowseTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_BrowseTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_BrowseTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_BrowseTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_BrowseTaxonomy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_BrowseTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_BrowseTaxonomy.Image = CType(resources.GetObject("btn_BrowseTaxonomy.Image"), System.Drawing.Image)
        Me.btn_BrowseTaxonomy.Location = New System.Drawing.Point(214, 30)
        Me.btn_BrowseTaxonomy.Name = "btn_BrowseTaxonomy"
        Me.btn_BrowseTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_BrowseTaxonomy.TabIndex = 3
        Me.btn_BrowseTaxonomy.UseVisualStyleBackColor = True
        '
        'btn_ClearTaxonomy
        '
        Me.btn_ClearTaxonomy.BackgroundImage = CType(resources.GetObject("btn_ClearTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_ClearTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ClearTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ClearTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearTaxonomy.Image = CType(resources.GetObject("btn_ClearTaxonomy.Image"), System.Drawing.Image)
        Me.btn_ClearTaxonomy.Location = New System.Drawing.Point(239, 30)
        Me.btn_ClearTaxonomy.Name = "btn_ClearTaxonomy"
        Me.btn_ClearTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_ClearTaxonomy.TabIndex = 4
        Me.btn_ClearTaxonomy.UseVisualStyleBackColor = True
        '
        'txtTaxonomy
        '
        Me.txtTaxonomy.BackColor = System.Drawing.Color.White
        Me.txtTaxonomy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxonomy.ForeColor = System.Drawing.Color.Black
        Me.txtTaxonomy.Location = New System.Drawing.Point(97, 30)
        Me.txtTaxonomy.MaxLength = 99
        Me.txtTaxonomy.Name = "txtTaxonomy"
        Me.txtTaxonomy.ReadOnly = True
        Me.txtTaxonomy.Size = New System.Drawing.Size(114, 22)
        Me.txtTaxonomy.TabIndex = 2
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label8.Location = New System.Drawing.Point(23, 34)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 14)
        Me.label8.TabIndex = 122
        Me.label8.Text = "Taxonomy :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Red
        Me.Label38.Location = New System.Drawing.Point(104, 29)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(14, 14)
        Me.Label38.TabIndex = 192
        Me.Label38.Text = "*"
        '
        'txtUserName
        '
        Me.txtUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(195, 26)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(104, 22)
        Me.txtUserName.TabIndex = 0
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPassword.ForeColor = System.Drawing.Color.Black
        Me.txtConfirmPassword.Location = New System.Drawing.Point(195, 78)
        Me.txtConfirmPassword.MaxLength = 100
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPassword.Size = New System.Drawing.Size(104, 22)
        Me.txtConfirmPassword.TabIndex = 2
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.Location = New System.Drawing.Point(195, 52)
        Me.txtPassword.MaxLength = 100
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(104, 22)
        Me.txtPassword.TabIndex = 1
        '
        'lblConfirmPassword
        '
        Me.lblConfirmPassword.AutoSize = True
        Me.lblConfirmPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblConfirmPassword.Location = New System.Drawing.Point(80, 82)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Size = New System.Drawing.Size(111, 14)
        Me.lblConfirmPassword.TabIndex = 3
        Me.lblConfirmPassword.Text = "Confirm Password :"
        '
        'lblNickName
        '
        Me.lblNickName.AutoSize = True
        Me.lblNickName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNickName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNickName.Location = New System.Drawing.Point(119, 108)
        Me.lblNickName.Name = "lblNickName"
        Me.lblNickName.Size = New System.Drawing.Size(72, 14)
        Me.lblNickName.TabIndex = 37
        Me.lblNickName.Text = "Nick Name :"
        Me.lblNickName.Visible = False
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUserName.Location = New System.Drawing.Point(117, 30)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(74, 14)
        Me.lblUserName.TabIndex = 33
        Me.lblUserName.Text = "User Name :"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPassword.Location = New System.Drawing.Point(125, 56)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(66, 14)
        Me.lblPassword.TabIndex = 34
        Me.lblPassword.Text = "Password :"
        '
        'txtNickName
        '
        Me.txtNickName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNickName.ForeColor = System.Drawing.Color.Black
        Me.txtNickName.Location = New System.Drawing.Point(195, 104)
        Me.txtNickName.MaxLength = 50
        Me.txtNickName.Name = "txtNickName"
        Me.txtNickName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNickName.Size = New System.Drawing.Size(104, 22)
        Me.txtNickName.TabIndex = 4
        Me.txtNickName.TabStop = False
        Me.txtNickName.Visible = False
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Red
        Me.Label40.Location = New System.Drawing.Point(70, 83)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(14, 14)
        Me.Label40.TabIndex = 194
        Me.Label40.Text = "*"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Red
        Me.Label39.Location = New System.Drawing.Point(114, 56)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(14, 14)
        Me.Label39.TabIndex = 193
        Me.Label39.Text = "*"
        '
        'txtImagePath
        '
        Me.txtImagePath.BackColor = System.Drawing.Color.White
        Me.txtImagePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImagePath.ForeColor = System.Drawing.Color.Black
        Me.txtImagePath.Location = New System.Drawing.Point(80, 188)
        Me.txtImagePath.Name = "txtImagePath"
        Me.txtImagePath.ReadOnly = True
        Me.txtImagePath.Size = New System.Drawing.Size(268, 22)
        Me.txtImagePath.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "File Name :"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(352, 188)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 0
        '
        'optBrowse
        '
        Me.optBrowse.Checked = True
        Me.optBrowse.Location = New System.Drawing.Point(244, 3)
        Me.optBrowse.Name = "optBrowse"
        Me.optBrowse.Size = New System.Drawing.Size(132, 18)
        Me.optBrowse.TabIndex = 0
        Me.optBrowse.TabStop = True
        Me.optBrowse.Text = "Browse From File"
        Me.optBrowse.Visible = False
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Location = New System.Drawing.Point(195, 157)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "&Clear"
        '
        'btnCapture
        '
        Me.btnCapture.BackgroundImage = CType(resources.GetObject("btnCapture.BackgroundImage"), System.Drawing.Image)
        Me.btnCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCapture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCapture.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCapture.Location = New System.Drawing.Point(109, 157)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(75, 23)
        Me.btnCapture.TabIndex = 15
        Me.btnCapture.Text = "&Capture"
        Me.btnCapture.Visible = False
        '
        'picSignature
        '
        Me.picSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSignature.Location = New System.Drawing.Point(19, 24)
        Me.picSignature.Name = "picSignature"
        Me.picSignature.Size = New System.Drawing.Size(359, 127)
        Me.picSignature.TabIndex = 17
        Me.picSignature.TabStop = False
        '
        'optMale
        '
        Me.optMale.AutoSize = True
        Me.optMale.BackColor = System.Drawing.Color.Transparent
        Me.optMale.Checked = True
        Me.optMale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optMale.Location = New System.Drawing.Point(65, 1)
        Me.optMale.Name = "optMale"
        Me.optMale.Size = New System.Drawing.Size(53, 18)
        Me.optMale.TabIndex = 0
        Me.optMale.TabStop = True
        Me.optMale.Text = "Male"
        Me.optMale.UseVisualStyleBackColor = False
        '
        'optFemale
        '
        Me.optFemale.AutoSize = True
        Me.optFemale.BackColor = System.Drawing.Color.Transparent
        Me.optFemale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFemale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optFemale.Location = New System.Drawing.Point(118, 1)
        Me.optFemale.Name = "optFemale"
        Me.optFemale.Size = New System.Drawing.Size(63, 18)
        Me.optFemale.TabIndex = 1
        Me.optFemale.TabStop = True
        Me.optFemale.Text = "Female"
        Me.optFemale.UseVisualStyleBackColor = False
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPhoneNo.Location = New System.Drawing.Point(12, 178)
        Me.lblPhoneNo.MaximumSize = New System.Drawing.Size(69, 13)
        Me.lblPhoneNo.MinimumSize = New System.Drawing.Size(69, 13)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(69, 13)
        Me.lblPhoneNo.TabIndex = 95
        Me.lblPhoneNo.Text = "Phone No :"
        Me.lblPhoneNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPager
        '
        Me.lblPager.AutoSize = True
        Me.lblPager.BackColor = System.Drawing.Color.Transparent
        Me.lblPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPager.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPager.Location = New System.Drawing.Point(241, 152)
        Me.lblPager.Name = "lblPager"
        Me.lblPager.Size = New System.Drawing.Size(46, 14)
        Me.lblPager.TabIndex = 109
        Me.lblPager.Text = "Pager :"
        '
        'pnlBussinessAddresssControl
        '
        Me.pnlBussinessAddresssControl.Location = New System.Drawing.Point(4, 41)
        Me.pnlBussinessAddresssControl.Name = "pnlBussinessAddresssControl"
        Me.pnlBussinessAddresssControl.Size = New System.Drawing.Size(328, 134)
        Me.pnlBussinessAddresssControl.TabIndex = 1
        Me.pnlBussinessAddresssControl.TabStop = True
        '
        'txtBMContact
        '
        Me.txtBMContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMContact.ForeColor = System.Drawing.Color.Black
        Me.txtBMContact.Location = New System.Drawing.Point(85, 18)
        Me.txtBMContact.MaxLength = 99
        Me.txtBMContact.Name = "txtBMContact"
        Me.txtBMContact.Size = New System.Drawing.Size(305, 22)
        Me.txtBMContact.TabIndex = 0
        '
        'label26
        '
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label26.Location = New System.Drawing.Point(13, 22)
        Me.label26.MaximumSize = New System.Drawing.Size(69, 13)
        Me.label26.MinimumSize = New System.Drawing.Size(69, 13)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(69, 13)
        Me.label26.TabIndex = 123
        Me.label26.Text = "Contact :"
        Me.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMAddress1
        '
        Me.txtBMAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtBMAddress1.Location = New System.Drawing.Point(155, 42)
        Me.txtBMAddress1.MaxLength = 99
        Me.txtBMAddress1.Name = "txtBMAddress1"
        Me.txtBMAddress1.Size = New System.Drawing.Size(10, 22)
        Me.txtBMAddress1.TabIndex = 1
        Me.txtBMAddress1.TabStop = False
        Me.txtBMAddress1.Visible = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(86, 46)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(65, 14)
        Me.label1.TabIndex = 90
        Me.label1.Text = "Address1 :"
        Me.label1.Visible = False
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Location = New System.Drawing.Point(171, 46)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(65, 14)
        Me.label2.TabIndex = 91
        Me.label2.Text = "Address2 :"
        Me.label2.Visible = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(109, 74)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(35, 14)
        Me.label3.TabIndex = 92
        Me.label3.Text = "City :"
        Me.label3.Visible = False
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label5.Location = New System.Drawing.Point(188, 78)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(45, 14)
        Me.label5.TabIndex = 93
        Me.label5.Text = "State :"
        Me.label5.Visible = False
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.BackColor = System.Drawing.Color.Transparent
        Me.lblFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFax.Location = New System.Drawing.Point(253, 178)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(33, 14)
        Me.lblFax.TabIndex = 104
        Me.lblFax.Text = "Fax :"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label7.Location = New System.Drawing.Point(256, 46)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(33, 14)
        Me.label7.TabIndex = 94
        Me.label7.Text = "ZIP :"
        Me.label7.Visible = False
        '
        'lblEmail
        '
        Me.lblEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblEmail.Location = New System.Drawing.Point(12, 204)
        Me.lblEmail.MaximumSize = New System.Drawing.Size(69, 13)
        Me.lblEmail.MinimumSize = New System.Drawing.Size(69, 13)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(69, 13)
        Me.lblEmail.TabIndex = 105
        Me.lblEmail.Text = "Email :"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMAddress2
        '
        Me.txtBMAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtBMAddress2.Location = New System.Drawing.Point(239, 43)
        Me.txtBMAddress2.MaxLength = 99
        Me.txtBMAddress2.Name = "txtBMAddress2"
        Me.txtBMAddress2.Size = New System.Drawing.Size(10, 22)
        Me.txtBMAddress2.TabIndex = 2
        Me.txtBMAddress2.TabStop = False
        Me.txtBMAddress2.Visible = False
        '
        'txtBMstate
        '
        Me.txtBMstate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMstate.ForeColor = System.Drawing.Color.Black
        Me.txtBMstate.Location = New System.Drawing.Point(238, 74)
        Me.txtBMstate.MaxLength = 99
        Me.txtBMstate.Name = "txtBMstate"
        Me.txtBMstate.Size = New System.Drawing.Size(11, 22)
        Me.txtBMstate.TabIndex = 4
        Me.txtBMstate.TabStop = False
        Me.txtBMstate.Visible = False
        '
        'txtBMCity
        '
        Me.txtBMCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCity.ForeColor = System.Drawing.Color.Black
        Me.txtBMCity.Location = New System.Drawing.Point(155, 74)
        Me.txtBMCity.MaxLength = 99
        Me.txtBMCity.Name = "txtBMCity"
        Me.txtBMCity.Size = New System.Drawing.Size(10, 22)
        Me.txtBMCity.TabIndex = 3
        Me.txtBMCity.TabStop = False
        Me.txtBMCity.Visible = False
        '
        'txtBMZip
        '
        Me.txtBMZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMZip.ForeColor = System.Drawing.Color.Black
        Me.txtBMZip.Location = New System.Drawing.Point(291, 42)
        Me.txtBMZip.MaxLength = 10
        Me.txtBMZip.Name = "txtBMZip"
        Me.txtBMZip.Size = New System.Drawing.Size(18, 22)
        Me.txtBMZip.TabIndex = 5
        Me.txtBMZip.TabStop = False
        Me.txtBMZip.Visible = False
        '
        'txtBMURL
        '
        Me.txtBMURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMURL.ForeColor = System.Drawing.Color.Black
        Me.txtBMURL.Location = New System.Drawing.Point(85, 226)
        Me.txtBMURL.MaxLength = 99
        Me.txtBMURL.Name = "txtBMURL"
        Me.txtBMURL.Size = New System.Drawing.Size(305, 22)
        Me.txtBMURL.TabIndex = 6
        '
        'txtBMEmailAddress
        '
        Me.txtBMEmailAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMEmailAddress.ForeColor = System.Drawing.Color.Black
        Me.txtBMEmailAddress.Location = New System.Drawing.Point(85, 201)
        Me.txtBMEmailAddress.MaxLength = 99
        Me.txtBMEmailAddress.Name = "txtBMEmailAddress"
        Me.txtBMEmailAddress.Size = New System.Drawing.Size(305, 22)
        Me.txtBMEmailAddress.TabIndex = 5
        '
        'lblURL
        '
        Me.lblURL.BackColor = System.Drawing.Color.Transparent
        Me.lblURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblURL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblURL.Location = New System.Drawing.Point(45, 229)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(36, 14)
        Me.lblURL.TabIndex = 106
        Me.lblURL.Text = "URL :"
        Me.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(9, 9)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(46, 14)
        Me.lblName.TabIndex = 74
        Me.lblName.Text = "Name :"
        '
        'txtSuffix
        '
        Me.txtSuffix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuffix.ForeColor = System.Drawing.Color.Black
        Me.txtSuffix.Location = New System.Drawing.Point(535, 5)
        Me.txtSuffix.MaxLength = 20
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(46, 22)
        Me.txtSuffix.TabIndex = 4
        '
        'txtPrefix
        '
        Me.txtPrefix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrefix.ForeColor = System.Drawing.Color.Black
        Me.txtPrefix.Location = New System.Drawing.Point(58, 5)
        Me.txtPrefix.MaxLength = 20
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(46, 22)
        Me.txtPrefix.TabIndex = 0
        '
        'txtFirstName
        '
        Me.txtFirstName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.ForeColor = System.Drawing.Color.Black
        Me.txtFirstName.Location = New System.Drawing.Point(110, 5)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(150, 22)
        Me.txtFirstName.TabIndex = 1
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddleName.ForeColor = System.Drawing.Color.Black
        Me.txtMiddleName.Location = New System.Drawing.Point(266, 5)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(108, 22)
        Me.txtMiddleName.TabIndex = 2
        '
        'label45
        '
        Me.label45.AutoSize = True
        Me.label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label45.Location = New System.Drawing.Point(541, 29)
        Me.label45.Name = "label45"
        Me.label45.Size = New System.Drawing.Size(34, 11)
        Me.label45.TabIndex = 79
        Me.label45.Text = "(Suffix)"
        '
        'txtLastName
        '
        Me.txtLastName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.ForeColor = System.Drawing.Color.Black
        Me.txtLastName.Location = New System.Drawing.Point(380, 5)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(149, 22)
        Me.txtLastName.TabIndex = 3
        '
        'lblPrefix
        '
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPrefix.Location = New System.Drawing.Point(64, 29)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(34, 11)
        Me.lblPrefix.TabIndex = 79
        Me.lblPrefix.Text = "(Prefix)"
        '
        'lblFirstName
        '
        Me.lblFirstName.AutoSize = True
        Me.lblFirstName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFirstName.Location = New System.Drawing.Point(157, 29)
        Me.lblFirstName.Name = "lblFirstName"
        Me.lblFirstName.Size = New System.Drawing.Size(57, 11)
        Me.lblFirstName.TabIndex = 80
        Me.lblFirstName.Text = "(First Name)"
        '
        'lblMiddleName
        '
        Me.lblMiddleName.AutoSize = True
        Me.lblMiddleName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiddleName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMiddleName.Location = New System.Drawing.Point(287, 29)
        Me.lblMiddleName.Name = "lblMiddleName"
        Me.lblMiddleName.Size = New System.Drawing.Size(66, 11)
        Me.lblMiddleName.TabIndex = 81
        Me.lblMiddleName.Text = "(Middle Name)"
        '
        'lblLastName
        '
        Me.lblLastName.AutoSize = True
        Me.lblLastName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLastName.Location = New System.Drawing.Point(427, 29)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(55, 11)
        Me.lblLastName.TabIndex = 82
        Me.lblLastName.Text = "(Last Name)"
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.BackColor = System.Drawing.Color.Transparent
        Me.label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label13.Location = New System.Drawing.Point(399, 164)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(46, 14)
        Me.label13.TabIndex = 119
        Me.label13.Text = "Pager :"
        '
        'pnlPracticeAddresssControl
        '
        Me.pnlPracticeAddresssControl.Location = New System.Drawing.Point(149, 53)
        Me.pnlPracticeAddresssControl.Name = "pnlPracticeAddresssControl"
        Me.pnlPracticeAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlPracticeAddresssControl.TabIndex = 2
        Me.pnlPracticeAddresssControl.TabStop = True
        '
        'txtBPracContactName
        '
        Me.txtBPracContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracContactName.ForeColor = System.Drawing.Color.Black
        Me.txtBPracContactName.Location = New System.Drawing.Point(231, 29)
        Me.txtBPracContactName.MaxLength = 99
        Me.txtBPracContactName.Name = "txtBPracContactName"
        Me.txtBPracContactName.Size = New System.Drawing.Size(312, 22)
        Me.txtBPracContactName.TabIndex = 0
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.Transparent
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label25.Location = New System.Drawing.Point(157, 33)
        Me.label25.MaximumSize = New System.Drawing.Size(69, 13)
        Me.label25.MinimumSize = New System.Drawing.Size(69, 13)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(69, 13)
        Me.label25.TabIndex = 121
        Me.label25.Text = "Contact :"
        Me.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.Color.Transparent
        Me.label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Location = New System.Drawing.Point(412, 188)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(33, 14)
        Me.label9.TabIndex = 116
        Me.label9.Text = "Fax :"
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.Transparent
        Me.label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Location = New System.Drawing.Point(156, 214)
        Me.label10.MaximumSize = New System.Drawing.Size(69, 13)
        Me.label10.MinimumSize = New System.Drawing.Size(69, 13)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(69, 13)
        Me.label10.TabIndex = 117
        Me.label10.Text = "Email :"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.Transparent
        Me.label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Location = New System.Drawing.Point(156, 186)
        Me.label11.MaximumSize = New System.Drawing.Size(69, 13)
        Me.label11.MinimumSize = New System.Drawing.Size(69, 13)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(69, 13)
        Me.label11.TabIndex = 111
        Me.label11.Text = "Phone No :"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBPracUrl
        '
        Me.txtBPracUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracUrl.ForeColor = System.Drawing.Color.Black
        Me.txtBPracUrl.Location = New System.Drawing.Point(231, 236)
        Me.txtBPracUrl.MaxLength = 99
        Me.txtBPracUrl.Name = "txtBPracUrl"
        Me.txtBPracUrl.Size = New System.Drawing.Size(312, 22)
        Me.txtBPracUrl.TabIndex = 7
        '
        'txtBPracEMail
        '
        Me.txtBPracEMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracEMail.ForeColor = System.Drawing.Color.Black
        Me.txtBPracEMail.Location = New System.Drawing.Point(231, 211)
        Me.txtBPracEMail.MaxLength = 99
        Me.txtBPracEMail.Name = "txtBPracEMail"
        Me.txtBPracEMail.Size = New System.Drawing.Size(312, 22)
        Me.txtBPracEMail.TabIndex = 6
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.Transparent
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Location = New System.Drawing.Point(157, 238)
        Me.label12.MaximumSize = New System.Drawing.Size(69, 13)
        Me.label12.MinimumSize = New System.Drawing.Size(69, 13)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(69, 13)
        Me.label12.TabIndex = 118
        Me.label12.Text = "URL :"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkAddressasAbove
        '
        Me.chkAddressasAbove.AutoSize = True
        Me.chkAddressasAbove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAddressasAbove.Location = New System.Drawing.Point(555, 31)
        Me.chkAddressasAbove.Name = "chkAddressasAbove"
        Me.chkAddressasAbove.Size = New System.Drawing.Size(166, 18)
        Me.chkAddressasAbove.TabIndex = 1
        Me.chkAddressasAbove.Text = "Same as Provider Address"
        Me.chkAddressasAbove.UseVisualStyleBackColor = True
        '
        'txtBPracAddress1
        '
        Me.txtBPracAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtBPracAddress1.Location = New System.Drawing.Point(218, 67)
        Me.txtBPracAddress1.MaxLength = 99
        Me.txtBPracAddress1.Name = "txtBPracAddress1"
        Me.txtBPracAddress1.Size = New System.Drawing.Size(47, 22)
        Me.txtBPracAddress1.TabIndex = 1
        Me.txtBPracAddress1.TabStop = False
        Me.txtBPracAddress1.Visible = False
        '
        'lblAddress1
        '
        Me.lblAddress1.AutoSize = True
        Me.lblAddress1.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAddress1.Location = New System.Drawing.Point(263, 67)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(65, 14)
        Me.lblAddress1.TabIndex = 90
        Me.lblAddress1.Text = "Address1 :"
        Me.lblAddress1.Visible = False
        '
        'lblAddress2
        '
        Me.lblAddress2.AutoSize = True
        Me.lblAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAddress2.Location = New System.Drawing.Point(321, 83)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(65, 14)
        Me.lblAddress2.TabIndex = 91
        Me.lblAddress2.Text = "Address2 :"
        Me.lblAddress2.Visible = False
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCity.Location = New System.Drawing.Point(295, 97)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 14)
        Me.lblCity.TabIndex = 92
        Me.lblCity.Text = "City :"
        Me.lblCity.Visible = False
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblState.Location = New System.Drawing.Point(330, 122)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 14)
        Me.lblState.TabIndex = 93
        Me.lblState.Text = "State :"
        Me.lblState.Visible = False
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblZip.Location = New System.Drawing.Point(275, 86)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(33, 14)
        Me.lblZip.TabIndex = 94
        Me.lblZip.Text = "ZIP :"
        Me.lblZip.Visible = False
        '
        'txtBPracAddress2
        '
        Me.txtBPracAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtBPracAddress2.Location = New System.Drawing.Point(392, 83)
        Me.txtBPracAddress2.MaxLength = 99
        Me.txtBPracAddress2.Name = "txtBPracAddress2"
        Me.txtBPracAddress2.Size = New System.Drawing.Size(10, 22)
        Me.txtBPracAddress2.TabIndex = 2
        Me.txtBPracAddress2.TabStop = False
        Me.txtBPracAddress2.Visible = False
        '
        'txtBPracState
        '
        Me.txtBPracState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracState.ForeColor = System.Drawing.Color.Black
        Me.txtBPracState.Location = New System.Drawing.Point(396, 94)
        Me.txtBPracState.MaxLength = 99
        Me.txtBPracState.Name = "txtBPracState"
        Me.txtBPracState.Size = New System.Drawing.Size(13, 22)
        Me.txtBPracState.TabIndex = 5
        Me.txtBPracState.Visible = False
        '
        'txtBPracCity
        '
        Me.txtBPracCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracCity.ForeColor = System.Drawing.Color.Black
        Me.txtBPracCity.Location = New System.Drawing.Point(333, 97)
        Me.txtBPracCity.MaxLength = 99
        Me.txtBPracCity.Name = "txtBPracCity"
        Me.txtBPracCity.Size = New System.Drawing.Size(14, 22)
        Me.txtBPracCity.TabIndex = 4
        Me.txtBPracCity.TabStop = False
        Me.txtBPracCity.Visible = False
        '
        'txtBPracZIP
        '
        Me.txtBPracZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracZIP.ForeColor = System.Drawing.Color.Black
        Me.txtBPracZIP.Location = New System.Drawing.Point(380, 100)
        Me.txtBPracZIP.MaxLength = 10
        Me.txtBPracZIP.Name = "txtBPracZIP"
        Me.txtBPracZIP.Size = New System.Drawing.Size(22, 22)
        Me.txtBPracZIP.TabIndex = 3
        Me.txtBPracZIP.TabStop = False
        Me.txtBPracZIP.Visible = False
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.lblLicenseMessage)
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(799, 56)
        Me.pnl_tlsp_Top.TabIndex = 1
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSave, Me.tlsbtnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(799, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Text = "Sa&ve&&Cls"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel16)
        Me.Panel9.Controls.Add(Me.mskBPracFax)
        Me.Panel9.Controls.Add(Me.mskBPracPager)
        Me.Panel9.Controls.Add(Me.Label81)
        Me.Panel9.Controls.Add(Me.label13)
        Me.Panel9.Controls.Add(Me.Label82)
        Me.Panel9.Controls.Add(Me.pnlPracticeAddresssControl)
        Me.Panel9.Controls.Add(Me.Label83)
        Me.Panel9.Controls.Add(Me.maskedBpracPhno)
        Me.Panel9.Controls.Add(Me.Label84)
        Me.Panel9.Controls.Add(Me.txtBPracContactName)
        Me.Panel9.Controls.Add(Me.label25)
        Me.Panel9.Controls.Add(Me.txtBPracZIP)
        Me.Panel9.Controls.Add(Me.label9)
        Me.Panel9.Controls.Add(Me.txtBPracCity)
        Me.Panel9.Controls.Add(Me.label10)
        Me.Panel9.Controls.Add(Me.txtBPracState)
        Me.Panel9.Controls.Add(Me.label11)
        Me.Panel9.Controls.Add(Me.txtBPracAddress2)
        Me.Panel9.Controls.Add(Me.txtBPracUrl)
        Me.Panel9.Controls.Add(Me.lblZip)
        Me.Panel9.Controls.Add(Me.txtBPracEMail)
        Me.Panel9.Controls.Add(Me.lblState)
        Me.Panel9.Controls.Add(Me.label12)
        Me.Panel9.Controls.Add(Me.lblCity)
        Me.Panel9.Controls.Add(Me.chkAddressasAbove)
        Me.Panel9.Controls.Add(Me.lblAddress2)
        Me.Panel9.Controls.Add(Me.txtBPracAddress1)
        Me.Panel9.Controls.Add(Me.lblAddress1)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(791, 616)
        Me.Panel9.TabIndex = 194
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Label85)
        Me.Panel16.Controls.Add(Me.Label100)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(1, 1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(789, 23)
        Me.Panel16.TabIndex = 133
        Me.Panel16.TabStop = True
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.BackColor = System.Drawing.Color.Transparent
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Location = New System.Drawing.Point(0, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label85.Size = New System.Drawing.Size(170, 17)
        Me.Label85.TabIndex = 131
        Me.Label85.Text = "Practice Location Address"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Location = New System.Drawing.Point(0, 22)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(789, 1)
        Me.Label100.TabIndex = 130
        '
        'mskBPracFax
        '
        Me.mskBPracFax.AllowValidate = True
        Me.mskBPracFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBPracFax.IncludeLiteralsAndPrompts = False
        Me.mskBPracFax.Location = New System.Drawing.Point(449, 184)
        Me.mskBPracFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskBPracFax.Name = "mskBPracFax"
        Me.mskBPracFax.ReadOnly = False
        Me.mskBPracFax.Size = New System.Drawing.Size(92, 25)
        Me.mskBPracFax.TabIndex = 5
        '
        'mskBPracPager
        '
        Me.mskBPracPager.AllowValidate = True
        Me.mskBPracPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBPracPager.IncludeLiteralsAndPrompts = False
        Me.mskBPracPager.Location = New System.Drawing.Point(449, 160)
        Me.mskBPracPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.mskBPracPager.Name = "mskBPracPager"
        Me.mskBPracPager.ReadOnly = False
        Me.mskBPracPager.Size = New System.Drawing.Size(92, 25)
        Me.mskBPracPager.TabIndex = 3
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Location = New System.Drawing.Point(790, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 614)
        Me.Label81.TabIndex = 127
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(0, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 614)
        Me.Label82.TabIndex = 126
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(0, 615)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(791, 1)
        Me.Label83.TabIndex = 125
        '
        'maskedBpracPhno
        '
        Me.maskedBpracPhno.AllowValidate = True
        Me.maskedBpracPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedBpracPhno.IncludeLiteralsAndPrompts = False
        Me.maskedBpracPhno.Location = New System.Drawing.Point(231, 185)
        Me.maskedBpracPhno.MaskType = gloMaskControl.gloMaskType.Phone
        Me.maskedBpracPhno.Name = "maskedBpracPhno"
        Me.maskedBpracPhno.ReadOnly = False
        Me.maskedBpracPhno.Size = New System.Drawing.Size(92, 25)
        Me.maskedBpracPhno.TabIndex = 4
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Location = New System.Drawing.Point(0, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(791, 1)
        Me.Label84.TabIndex = 124
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbpgProvider)
        Me.TabControl1.Controls.Add(Me.tbpgBillingID)
        Me.TabControl1.Controls.Add(Me.tbpgProviderCompany)
        Me.TabControl1.Controls.Add(Me.tbpgStatement)
        Me.TabControl1.Controls.Add(Me.tbpgPrvdrMultCmpny)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(799, 643)
        Me.TabControl1.TabIndex = 192
        Me.TabControl1.TabStop = False
        '
        'tbpgProvider
        '
        Me.tbpgProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgProvider.Controls.Add(Me.Panel15)
        Me.tbpgProvider.Controls.Add(Me.pnlSPI)
        Me.tbpgProvider.Controls.Add(Me.grpSPI)
        Me.tbpgProvider.Controls.Add(Me.Panel7)
        Me.tbpgProvider.Controls.Add(Me.pnlLicenseKey)
        Me.tbpgProvider.Controls.Add(Me.Panel13)
        Me.tbpgProvider.Controls.Add(Me.Panel12)
        Me.tbpgProvider.Controls.Add(Me.Panel10)
        Me.tbpgProvider.Location = New System.Drawing.Point(4, 23)
        Me.tbpgProvider.Name = "tbpgProvider"
        Me.tbpgProvider.Size = New System.Drawing.Size(791, 616)
        Me.tbpgProvider.TabIndex = 0
        Me.tbpgProvider.Text = "Provider"
        Me.tbpgProvider.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.chkRequire_Supervising_Provider_for_eRx)
        Me.Panel15.Controls.Add(Me.Label124)
        Me.Panel15.Controls.Add(Me.Label126)
        Me.Panel15.Controls.Add(Me.Label127)
        Me.Panel15.Controls.Add(Me.Label128)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.Location = New System.Drawing.Point(0, 626)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(791, 32)
        Me.Panel15.TabIndex = 131
        Me.Panel15.Visible = False
        '
        'chkRequire_Supervising_Provider_for_eRx
        '
        Me.chkRequire_Supervising_Provider_for_eRx.AutoSize = True
        Me.chkRequire_Supervising_Provider_for_eRx.Enabled = False
        Me.chkRequire_Supervising_Provider_for_eRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRequire_Supervising_Provider_for_eRx.Location = New System.Drawing.Point(141, 7)
        Me.chkRequire_Supervising_Provider_for_eRx.Name = "chkRequire_Supervising_Provider_for_eRx"
        Me.chkRequire_Supervising_Provider_for_eRx.Size = New System.Drawing.Size(216, 18)
        Me.chkRequire_Supervising_Provider_for_eRx.TabIndex = 128
        Me.chkRequire_Supervising_Provider_for_eRx.Text = "Require Supervising Provider for Rx"
        Me.chkRequire_Supervising_Provider_for_eRx.UseVisualStyleBackColor = True
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label124.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Location = New System.Drawing.Point(1, 28)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(789, 1)
        Me.Label124.TabIndex = 130
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label126.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label126.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label126.Location = New System.Drawing.Point(1, 3)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(789, 1)
        Me.Label126.TabIndex = 129
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label127.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label127.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label127.Location = New System.Drawing.Point(790, 3)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1, 26)
        Me.Label127.TabIndex = 128
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Location = New System.Drawing.Point(0, 3)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1, 26)
        Me.Label128.TabIndex = 127
        '
        'grpSPI
        '
        Me.grpSPI.Controls.Add(Me.ChkCIEvent)
        Me.grpSPI.Controls.Add(Me.chkCIMessage)
        Me.grpSPI.Controls.Add(Me.lblPreDetails)
        Me.grpSPI.Controls.Add(Me.Label67)
        Me.grpSPI.Controls.Add(Me.chckDisable)
        Me.grpSPI.Controls.Add(Me.Label32)
        Me.grpSPI.Controls.Add(Me.Label68)
        Me.grpSPI.Controls.Add(Me.Label31)
        Me.grpSPI.Controls.Add(Me.Label69)
        Me.grpSPI.Controls.Add(Me.dtpActiveEndTime)
        Me.grpSPI.Controls.Add(Me.dtpActiveStartTime)
        Me.grpSPI.Controls.Add(Me.Label71)
        Me.grpSPI.Controls.Add(Me.Label30)
        Me.grpSPI.Controls.Add(Me.rbPrescriberLocation)
        Me.grpSPI.Controls.Add(Me.chckRefill)
        Me.grpSPI.Controls.Add(Me.rbUpdate)
        Me.grpSPI.Controls.Add(Me.chckNew)
        Me.grpSPI.Controls.Add(Me.rbPrescriber)
        Me.grpSPI.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSPI.Location = New System.Drawing.Point(0, 525)
        Me.grpSPI.Name = "grpSPI"
        Me.grpSPI.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.grpSPI.Size = New System.Drawing.Size(791, 75)
        Me.grpSPI.TabIndex = 3
        Me.grpSPI.Visible = False
        '
        'ChkCIEvent
        '
        Me.ChkCIEvent.AutoSize = True
        Me.ChkCIEvent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCIEvent.Location = New System.Drawing.Point(488, 27)
        Me.ChkCIEvent.Name = "ChkCIEvent"
        Me.ChkCIEvent.Size = New System.Drawing.Size(69, 18)
        Me.ChkCIEvent.TabIndex = 197
        Me.ChkCIEvent.Text = "CIEvent"
        Me.ChkCIEvent.UseVisualStyleBackColor = True
        '
        'chkCIMessage
        '
        Me.chkCIMessage.AutoSize = True
        Me.chkCIMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCIMessage.Location = New System.Drawing.Point(380, 27)
        Me.chkCIMessage.Name = "chkCIMessage"
        Me.chkCIMessage.Size = New System.Drawing.Size(83, 18)
        Me.chkCIMessage.TabIndex = 196
        Me.chkCIMessage.Text = "CIMessage"
        Me.chkCIMessage.UseVisualStyleBackColor = True
        '
        'lblPreDetails
        '
        Me.lblPreDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPreDetails.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreDetails.Location = New System.Drawing.Point(1, 74)
        Me.lblPreDetails.Name = "lblPreDetails"
        Me.lblPreDetails.Size = New System.Drawing.Size(789, 1)
        Me.lblPreDetails.TabIndex = 195
        Me.lblPreDetails.Text = "label2"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.Color.Transparent
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Location = New System.Drawing.Point(1, 4)
        Me.Label67.Name = "Label67"
        Me.Label67.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label67.Size = New System.Drawing.Size(118, 17)
        Me.Label67.TabIndex = 95
        Me.Label67.Text = "Prescriber Details"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Location = New System.Drawing.Point(790, 4)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 71)
        Me.Label68.TabIndex = 127
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Location = New System.Drawing.Point(0, 4)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 71)
        Me.Label69.TabIndex = 126
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Location = New System.Drawing.Point(0, 3)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(791, 1)
        Me.Label71.TabIndex = 124
        '
        'pnlLicenseKey
        '
        Me.pnlLicenseKey.Controls.Add(Me.Label190)
        Me.pnlLicenseKey.Controls.Add(Me.btnLicenseRefresh)
        Me.pnlLicenseKey.Controls.Add(Me.Label186)
        Me.pnlLicenseKey.Controls.Add(Me.txtLicenseKey)
        Me.pnlLicenseKey.Controls.Add(Me.Label187)
        Me.pnlLicenseKey.Controls.Add(Me.Label188)
        Me.pnlLicenseKey.Controls.Add(Me.Label189)
        Me.pnlLicenseKey.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLicenseKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLicenseKey.Location = New System.Drawing.Point(0, 461)
        Me.pnlLicenseKey.Name = "pnlLicenseKey"
        Me.pnlLicenseKey.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlLicenseKey.Size = New System.Drawing.Size(791, 32)
        Me.pnlLicenseKey.TabIndex = 132
        '
        'Label190
        '
        Me.Label190.AutoSize = True
        Me.Label190.BackColor = System.Drawing.Color.Transparent
        Me.Label190.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label190.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label190.Location = New System.Drawing.Point(15, 10)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(79, 14)
        Me.Label190.TabIndex = 207
        Me.Label190.Text = "License Key :"
        '
        'btnLicenseRefresh
        '
        Me.btnLicenseRefresh.BackgroundImage = CType(resources.GetObject("btnLicenseRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnLicenseRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLicenseRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnLicenseRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnLicenseRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLicenseRefresh.ImageIndex = 2
        Me.btnLicenseRefresh.ImageList = Me.ImgLicenseKey
        Me.btnLicenseRefresh.Location = New System.Drawing.Point(757, 6)
        Me.btnLicenseRefresh.Name = "btnLicenseRefresh"
        Me.btnLicenseRefresh.Size = New System.Drawing.Size(24, 23)
        Me.btnLicenseRefresh.TabIndex = 201
        Me.btnLicenseRefresh.Tag = "refresh"
        '
        'ImgLicenseKey
        '
        Me.ImgLicenseKey.ImageStream = CType(resources.GetObject("ImgLicenseKey.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgLicenseKey.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgLicenseKey.Images.SetKeyName(0, "AUSOk.ico")
        Me.ImgLicenseKey.Images.SetKeyName(1, "AUSInvalid.ico")
        Me.ImgLicenseKey.Images.SetKeyName(2, "AUSRefresh.ico")
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label186.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label186.Location = New System.Drawing.Point(1, 31)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(789, 1)
        Me.Label186.TabIndex = 130
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicenseKey.ForeColor = System.Drawing.Color.Black
        Me.txtLicenseKey.Location = New System.Drawing.Point(97, 6)
        Me.txtLicenseKey.MaxLength = 1000
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(656, 22)
        Me.txtLicenseKey.TabIndex = 200
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label187.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label187.Location = New System.Drawing.Point(1, 3)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(789, 1)
        Me.Label187.TabIndex = 129
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label188.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Location = New System.Drawing.Point(790, 3)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(1, 29)
        Me.Label188.TabIndex = 128
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label189.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label189.Location = New System.Drawing.Point(0, 3)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(1, 29)
        Me.Label189.TabIndex = 127
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel6)
        Me.Panel13.Controls.Add(Me.Panel5)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 302)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel13.Size = New System.Drawing.Size(791, 159)
        Me.Panel13.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.lblAusStatus)
        Me.Panel6.Controls.Add(Me.Label72)
        Me.Panel6.Controls.Add(Me.Label38)
        Me.Panel6.Controls.Add(Me.Label63)
        Me.Panel6.Controls.Add(Me.txtUserName)
        Me.Panel6.Controls.Add(Me.txtConfirmPassword)
        Me.Panel6.Controls.Add(Me.Label64)
        Me.Panel6.Controls.Add(Me.txtPassword)
        Me.Panel6.Controls.Add(Me.Label65)
        Me.Panel6.Controls.Add(Me.lblConfirmPassword)
        Me.Panel6.Controls.Add(Me.Label66)
        Me.Panel6.Controls.Add(Me.Label39)
        Me.Panel6.Controls.Add(Me.lblNickName)
        Me.Panel6.Controls.Add(Me.Label40)
        Me.Panel6.Controls.Add(Me.lblUserName)
        Me.Panel6.Controls.Add(Me.txtNickName)
        Me.Panel6.Controls.Add(Me.lblPassword)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(405, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel6.Size = New System.Drawing.Size(386, 156)
        Me.Panel6.TabIndex = 1
        '
        'lblAusStatus
        '
        Me.lblAusStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAusStatus.Location = New System.Drawing.Point(312, 130)
        Me.lblAusStatus.Name = "lblAusStatus"
        Me.lblAusStatus.Size = New System.Drawing.Size(67, 14)
        Me.lblAusStatus.TabIndex = 197
        Me.lblAusStatus.Text = "aus"
        Me.lblAusStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAusStatus.Visible = False
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Location = New System.Drawing.Point(4, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label72.Size = New System.Drawing.Size(84, 17)
        Me.Label72.TabIndex = 95
        Me.Label72.Text = "User Details"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Location = New System.Drawing.Point(385, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 154)
        Me.Label63.TabIndex = 127
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Location = New System.Drawing.Point(3, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 154)
        Me.Label64.TabIndex = 126
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Location = New System.Drawing.Point(3, 155)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(383, 1)
        Me.Label65.TabIndex = 125
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Location = New System.Drawing.Point(3, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(383, 1)
        Me.Label66.TabIndex = 124
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label191)
        Me.Panel5.Controls.Add(Me.txtDPSNumber)
        Me.Panel5.Controls.Add(Me.lblDPSNumber)
        Me.Panel5.Controls.Add(Me.txtExternalCode)
        Me.Panel5.Controls.Add(Me.lblExternal_Code)
        Me.Panel5.Controls.Add(Me.txtLabId)
        Me.Panel5.Controls.Add(Me.lblLabID)
        Me.Panel5.Controls.Add(Me.mskTxtDEA)
        Me.Panel5.Controls.Add(Me.Label58)
        Me.Panel5.Controls.Add(Me.mskTxtUPIN)
        Me.Panel5.Controls.Add(Me.Label59)
        Me.Panel5.Controls.Add(Me.mskMobileNo)
        Me.Panel5.Controls.Add(Me.mtxt_SSNno)
        Me.Panel5.Controls.Add(Me.Label60)
        Me.Panel5.Controls.Add(Me.Label61)
        Me.Panel5.Controls.Add(Me.lblType)
        Me.Panel5.Controls.Add(Me.label8)
        Me.Panel5.Controls.Add(Me.cmbDoctorType)
        Me.Panel5.Controls.Add(Me.txtTaxonomy)
        Me.Panel5.Controls.Add(Me.lblMobileNo)
        Me.Panel5.Controls.Add(Me.btn_ClearTaxonomy)
        Me.Panel5.Controls.Add(Me.btn_BrowseTaxonomy)
        Me.Panel5.Controls.Add(Me.txt_EmployerID)
        Me.Panel5.Controls.Add(Me.txtStateMedicalLicenseNo)
        Me.Panel5.Controls.Add(Me.txtNPI)
        Me.Panel5.Controls.Add(Me.label27)
        Me.Panel5.Controls.Add(Me.lblDEA)
        Me.Panel5.Controls.Add(Me.lblNPI)
        Me.Panel5.Controls.Add(Me.lblStateMedicalLicense)
        Me.Panel5.Controls.Add(Me.label28)
        Me.Panel5.Controls.Add(Me.lblUPIN)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(0, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(405, 156)
        Me.Panel5.TabIndex = 0
        '
        'Label191
        '
        Me.Label191.AutoSize = True
        Me.Label191.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label191.ForeColor = System.Drawing.Color.Red
        Me.Label191.Location = New System.Drawing.Point(273, 32)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(14, 14)
        Me.Label191.TabIndex = 207
        Me.Label191.Text = "*"
        '
        'txtDPSNumber
        '
        Me.txtDPSNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDPSNumber.ForeColor = System.Drawing.Color.Black
        Me.txtDPSNumber.Location = New System.Drawing.Point(280, 130)
        Me.txtDPSNumber.MaxLength = 16
        Me.txtDPSNumber.Name = "txtDPSNumber"
        Me.txtDPSNumber.Size = New System.Drawing.Size(120, 22)
        Me.txtDPSNumber.TabIndex = 206
        '
        'lblDPSNumber
        '
        Me.lblDPSNumber.AutoSize = True
        Me.lblDPSNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblDPSNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDPSNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDPSNumber.Location = New System.Drawing.Point(209, 133)
        Me.lblDPSNumber.Name = "lblDPSNumber"
        Me.lblDPSNumber.Size = New System.Drawing.Size(71, 14)
        Me.lblDPSNumber.TabIndex = 205
        Me.lblDPSNumber.Text = "State Rx# :"
        '
        'txtExternalCode
        '
        Me.txtExternalCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExternalCode.ForeColor = System.Drawing.Color.Black
        Me.txtExternalCode.Location = New System.Drawing.Point(97, 130)
        Me.txtExternalCode.MaxLength = 500
        Me.txtExternalCode.Name = "txtExternalCode"
        Me.txtExternalCode.Size = New System.Drawing.Size(100, 22)
        Me.txtExternalCode.TabIndex = 202
        '
        'lblExternal_Code
        '
        Me.lblExternal_Code.AutoSize = True
        Me.lblExternal_Code.BackColor = System.Drawing.Color.Transparent
        Me.lblExternal_Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExternal_Code.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblExternal_Code.Location = New System.Drawing.Point(4, 133)
        Me.lblExternal_Code.Name = "lblExternal_Code"
        Me.lblExternal_Code.Size = New System.Drawing.Size(91, 14)
        Me.lblExternal_Code.TabIndex = 204
        Me.lblExternal_Code.Text = "External Code :"
        '
        'txtLabId
        '
        Me.txtLabId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabId.ForeColor = System.Drawing.Color.Black
        Me.txtLabId.Location = New System.Drawing.Point(97, 105)
        Me.txtLabId.MaxLength = 9
        Me.txtLabId.Name = "txtLabId"
        Me.txtLabId.Size = New System.Drawing.Size(100, 22)
        Me.txtLabId.TabIndex = 201
        '
        'lblLabID
        '
        Me.lblLabID.AutoSize = True
        Me.lblLabID.BackColor = System.Drawing.Color.Transparent
        Me.lblLabID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLabID.Location = New System.Drawing.Point(46, 108)
        Me.lblLabID.Name = "lblLabID"
        Me.lblLabID.Size = New System.Drawing.Size(49, 14)
        Me.lblLabID.TabIndex = 203
        Me.lblLabID.Text = "Lab Id :"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Location = New System.Drawing.Point(404, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 154)
        Me.Label58.TabIndex = 127
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Location = New System.Drawing.Point(0, 1)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(1, 154)
        Me.Label59.TabIndex = 126
        '
        'mskMobileNo
        '
        Me.mskMobileNo.AllowValidate = True
        Me.mskMobileNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMobileNo.IncludeLiteralsAndPrompts = False
        Me.mskMobileNo.Location = New System.Drawing.Point(97, 4)
        Me.mskMobileNo.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskMobileNo.Name = "mskMobileNo"
        Me.mskMobileNo.ReadOnly = False
        Me.mskMobileNo.Size = New System.Drawing.Size(87, 23)
        Me.mskMobileNo.TabIndex = 0
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Location = New System.Drawing.Point(0, 155)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(405, 1)
        Me.Label60.TabIndex = 125
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Location = New System.Drawing.Point(0, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(405, 1)
        Me.Label61.TabIndex = 124
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Panel3)
        Me.Panel12.Controls.Add(Me.Panel4)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 44)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel12.Size = New System.Drawing.Size(791, 258)
        Me.Panel12.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label62)
        Me.Panel3.Controls.Add(Me.txtImagePath)
        Me.Panel3.Controls.Add(Me.Label50)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label51)
        Me.Panel3.Controls.Add(Me.btnBrowse)
        Me.Panel3.Controls.Add(Me.Label52)
        Me.Panel3.Controls.Add(Me.optBrowse)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.Label53)
        Me.Panel3.Controls.Add(Me.btnCapture)
        Me.Panel3.Controls.Add(Me.picSignature)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(405, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(386, 255)
        Me.Panel3.TabIndex = 1
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Location = New System.Drawing.Point(4, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label62.Size = New System.Drawing.Size(73, 17)
        Me.Label62.TabIndex = 95
        Me.Label62.Text = "Signature"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Location = New System.Drawing.Point(385, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 253)
        Me.Label50.TabIndex = 127
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Location = New System.Drawing.Point(3, 1)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 253)
        Me.Label51.TabIndex = 126
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Location = New System.Drawing.Point(3, 254)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(383, 1)
        Me.Label52.TabIndex = 125
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Location = New System.Drawing.Point(3, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(383, 1)
        Me.Label53.TabIndex = 124
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.txtBMContact)
        Me.Panel4.Controls.Add(Me.Label57)
        Me.Panel4.Controls.Add(Me.lblPhoneNo)
        Me.Panel4.Controls.Add(Me.Label49)
        Me.Panel4.Controls.Add(Me.mskBMPhoneNo)
        Me.Panel4.Controls.Add(Me.Label54)
        Me.Panel4.Controls.Add(Me.lblPager)
        Me.Panel4.Controls.Add(Me.Label55)
        Me.Panel4.Controls.Add(Me.mskBMPager)
        Me.Panel4.Controls.Add(Me.Label56)
        Me.Panel4.Controls.Add(Me.pnlBussinessAddresssControl)
        Me.Panel4.Controls.Add(Me.mskBMFax)
        Me.Panel4.Controls.Add(Me.lblURL)
        Me.Panel4.Controls.Add(Me.txtBMEmailAddress)
        Me.Panel4.Controls.Add(Me.label26)
        Me.Panel4.Controls.Add(Me.txtBMURL)
        Me.Panel4.Controls.Add(Me.txtBMAddress1)
        Me.Panel4.Controls.Add(Me.txtBMZip)
        Me.Panel4.Controls.Add(Me.label1)
        Me.Panel4.Controls.Add(Me.txtBMCity)
        Me.Panel4.Controls.Add(Me.label2)
        Me.Panel4.Controls.Add(Me.txtBMstate)
        Me.Panel4.Controls.Add(Me.label3)
        Me.Panel4.Controls.Add(Me.txtBMAddress2)
        Me.Panel4.Controls.Add(Me.label5)
        Me.Panel4.Controls.Add(Me.lblEmail)
        Me.Panel4.Controls.Add(Me.lblFax)
        Me.Panel4.Controls.Add(Me.label7)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(405, 255)
        Me.Panel4.TabIndex = 0
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Location = New System.Drawing.Point(1, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label57.Size = New System.Drawing.Size(165, 17)
        Me.Label57.TabIndex = 95
        Me.Label57.Text = "Business Mailing Address"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Location = New System.Drawing.Point(404, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 253)
        Me.Label49.TabIndex = 127
        '
        'mskBMPhoneNo
        '
        Me.mskBMPhoneNo.AllowValidate = True
        Me.mskBMPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBMPhoneNo.IncludeLiteralsAndPrompts = False
        Me.mskBMPhoneNo.Location = New System.Drawing.Point(85, 174)
        Me.mskBMPhoneNo.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskBMPhoneNo.Name = "mskBMPhoneNo"
        Me.mskBMPhoneNo.ReadOnly = False
        Me.mskBMPhoneNo.Size = New System.Drawing.Size(100, 23)
        Me.mskBMPhoneNo.TabIndex = 3
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Location = New System.Drawing.Point(0, 1)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 253)
        Me.Label54.TabIndex = 126
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Location = New System.Drawing.Point(0, 254)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(405, 1)
        Me.Label55.TabIndex = 125
        '
        'mskBMPager
        '
        Me.mskBMPager.AllowValidate = True
        Me.mskBMPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBMPager.IncludeLiteralsAndPrompts = False
        Me.mskBMPager.Location = New System.Drawing.Point(288, 148)
        Me.mskBMPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.mskBMPager.Name = "mskBMPager"
        Me.mskBMPager.ReadOnly = False
        Me.mskBMPager.Size = New System.Drawing.Size(102, 25)
        Me.mskBMPager.TabIndex = 2
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Location = New System.Drawing.Point(0, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(405, 1)
        Me.Label56.TabIndex = 124
        '
        'mskBMFax
        '
        Me.mskBMFax.AllowValidate = True
        Me.mskBMFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBMFax.IncludeLiteralsAndPrompts = False
        Me.mskBMFax.Location = New System.Drawing.Point(288, 173)
        Me.mskBMFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskBMFax.Name = "mskBMFax"
        Me.mskBMFax.ReadOnly = False
        Me.mskBMFax.Size = New System.Drawing.Size(102, 25)
        Me.mskBMFax.TabIndex = 4
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel2)
        Me.Panel10.Controls.Add(Me.Panel1)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(791, 44)
        Me.Panel10.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.mtxtDOB)
        Me.Panel2.Controls.Add(Me.lbPatientDOB)
        Me.Panel2.Controls.Add(Me.Label48)
        Me.Panel2.Controls.Add(Me.optMale)
        Me.Panel2.Controls.Add(Me.Label41)
        Me.Panel2.Controls.Add(Me.optFemale)
        Me.Panel2.Controls.Add(Me.Label42)
        Me.Panel2.Controls.Add(Me.Label43)
        Me.Panel2.Controls.Add(Me.Label44)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(596, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(195, 44)
        Me.Panel2.TabIndex = 1
        '
        'mtxtDOB
        '
        Me.mtxtDOB.Location = New System.Drawing.Point(69, 19)
        Me.mtxtDOB.Mask = "00/00/0000"
        Me.mtxtDOB.Name = "mtxtDOB"
        Me.mtxtDOB.Size = New System.Drawing.Size(114, 22)
        Me.mtxtDOB.TabIndex = 1923
        Me.mtxtDOB.ValidatingType = GetType(Date)
        '
        'lbPatientDOB
        '
        Me.lbPatientDOB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbPatientDOB.AutoEllipsis = True
        Me.lbPatientDOB.AutoSize = True
        Me.lbPatientDOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPatientDOB.Location = New System.Drawing.Point(26, 23)
        Me.lbPatientDOB.Name = "lbPatientDOB"
        Me.lbPatientDOB.Size = New System.Drawing.Size(39, 14)
        Me.lbPatientDOB.TabIndex = 1924
        Me.lbPatientDOB.Text = "DOB :"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Location = New System.Drawing.Point(10, 2)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(55, 14)
        Me.Label48.TabIndex = 128
        Me.Label48.Text = "Gender :"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(194, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 42)
        Me.Label41.TabIndex = 127
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 42)
        Me.Label42.TabIndex = 126
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(0, 43)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(195, 1)
        Me.Label43.TabIndex = 125
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(195, 1)
        Me.Label44.TabIndex = 124
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label36)
        Me.Panel1.Controls.Add(Me.Label35)
        Me.Panel1.Controls.Add(Me.Label34)
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtPrefix)
        Me.Panel1.Controls.Add(Me.lblLastName)
        Me.Panel1.Controls.Add(Me.lblMiddleName)
        Me.Panel1.Controls.Add(Me.lblFirstName)
        Me.Panel1.Controls.Add(Me.lblPrefix)
        Me.Panel1.Controls.Add(Me.txtLastName)
        Me.Panel1.Controls.Add(Me.label45)
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Controls.Add(Me.txtMiddleName)
        Me.Panel1.Controls.Add(Me.txtSuffix)
        Me.Panel1.Controls.Add(Me.txtFirstName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel1.Size = New System.Drawing.Size(596, 44)
        Me.Panel1.TabIndex = 0
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Location = New System.Drawing.Point(592, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 42)
        Me.Label36.TabIndex = 127
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(0, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 42)
        Me.Label35.TabIndex = 126
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(0, 43)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(593, 1)
        Me.Label34.TabIndex = 125
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(593, 1)
        Me.Label33.TabIndex = 124
        '
        'tbpgBillingID
        '
        Me.tbpgBillingID.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgBillingID.Controls.Add(Me.Panel20)
        Me.tbpgBillingID.Controls.Add(Me.gbProviderIdentification)
        Me.tbpgBillingID.Location = New System.Drawing.Point(4, 23)
        Me.tbpgBillingID.Name = "tbpgBillingID"
        Me.tbpgBillingID.Size = New System.Drawing.Size(791, 616)
        Me.tbpgBillingID.TabIndex = 1
        Me.tbpgBillingID.Text = "Billing IDs"
        Me.tbpgBillingID.UseVisualStyleBackColor = True
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.Panel21)
        Me.Panel20.Controls.Add(Me.mskBIDPLFax)
        Me.Panel20.Controls.Add(Me.mskBIDPLPager)
        Me.Panel20.Controls.Add(Me.Label119)
        Me.Panel20.Controls.Add(Me.Label120)
        Me.Panel20.Controls.Add(Me.Label121)
        Me.Panel20.Controls.Add(Me.pnlBIDPLAddresssControl)
        Me.Panel20.Controls.Add(Me.Label122)
        Me.Panel20.Controls.Add(Me.maskedBIDPLPhno)
        Me.Panel20.Controls.Add(Me.Label123)
        Me.Panel20.Controls.Add(Me.txtBIDPLContactName)
        Me.Panel20.Controls.Add(Me.Label125)
        Me.Panel20.Controls.Add(Me.TextBox6)
        Me.Panel20.Controls.Add(Me.Label129)
        Me.Panel20.Controls.Add(Me.TextBox7)
        Me.Panel20.Controls.Add(Me.Label130)
        Me.Panel20.Controls.Add(Me.TextBox9)
        Me.Panel20.Controls.Add(Me.Label131)
        Me.Panel20.Controls.Add(Me.TextBox10)
        Me.Panel20.Controls.Add(Me.txtBIDPLUrl)
        Me.Panel20.Controls.Add(Me.Label132)
        Me.Panel20.Controls.Add(Me.txtBIDPLEMail)
        Me.Panel20.Controls.Add(Me.Label133)
        Me.Panel20.Controls.Add(Me.Label134)
        Me.Panel20.Controls.Add(Me.Label135)
        Me.Panel20.Controls.Add(Me.Label136)
        Me.Panel20.Controls.Add(Me.TextBox13)
        Me.Panel20.Controls.Add(Me.Label137)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel20.Location = New System.Drawing.Point(0, 332)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel20.Size = New System.Drawing.Size(791, 284)
        Me.Panel20.TabIndex = 198
        '
        'Panel21
        '
        Me.Panel21.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.Label117)
        Me.Panel21.Controls.Add(Me.Label118)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel21.Location = New System.Drawing.Point(1, 4)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(789, 23)
        Me.Panel21.TabIndex = 133
        Me.Panel21.TabStop = True
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.BackColor = System.Drawing.Color.Transparent
        Me.Label117.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label117.Location = New System.Drawing.Point(0, 0)
        Me.Label117.Name = "Label117"
        Me.Label117.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label117.Size = New System.Drawing.Size(170, 17)
        Me.Label117.TabIndex = 131
        Me.Label117.Text = "Physical Location Address"
        '
        'Label118
        '
        Me.Label118.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label118.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Location = New System.Drawing.Point(0, 22)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(789, 1)
        Me.Label118.TabIndex = 130
        '
        'mskBIDPLFax
        '
        Me.mskBIDPLFax.AllowValidate = True
        Me.mskBIDPLFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBIDPLFax.IncludeLiteralsAndPrompts = False
        Me.mskBIDPLFax.Location = New System.Drawing.Point(419, 188)
        Me.mskBIDPLFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskBIDPLFax.Name = "mskBIDPLFax"
        Me.mskBIDPLFax.ReadOnly = False
        Me.mskBIDPLFax.Size = New System.Drawing.Size(92, 25)
        Me.mskBIDPLFax.TabIndex = 5
        '
        'mskBIDPLPager
        '
        Me.mskBIDPLPager.AllowValidate = True
        Me.mskBIDPLPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBIDPLPager.IncludeLiteralsAndPrompts = False
        Me.mskBIDPLPager.Location = New System.Drawing.Point(419, 164)
        Me.mskBIDPLPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.mskBIDPLPager.Name = "mskBIDPLPager"
        Me.mskBIDPLPager.ReadOnly = False
        Me.mskBIDPLPager.Size = New System.Drawing.Size(92, 25)
        Me.mskBIDPLPager.TabIndex = 3
        '
        'Label119
        '
        Me.Label119.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Location = New System.Drawing.Point(790, 4)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(1, 279)
        Me.Label119.TabIndex = 127
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.BackColor = System.Drawing.Color.Transparent
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Location = New System.Drawing.Point(368, 168)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(46, 14)
        Me.Label120.TabIndex = 119
        Me.Label120.Text = "Pager :"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Location = New System.Drawing.Point(0, 4)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 279)
        Me.Label121.TabIndex = 126
        '
        'pnlBIDPLAddresssControl
        '
        Me.pnlBIDPLAddresssControl.Location = New System.Drawing.Point(120, 57)
        Me.pnlBIDPLAddresssControl.Name = "pnlBIDPLAddresssControl"
        Me.pnlBIDPLAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlBIDPLAddresssControl.TabIndex = 2
        Me.pnlBIDPLAddresssControl.TabStop = True
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label122.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Location = New System.Drawing.Point(0, 283)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(791, 1)
        Me.Label122.TabIndex = 125
        '
        'maskedBIDPLPhno
        '
        Me.maskedBIDPLPhno.AllowValidate = True
        Me.maskedBIDPLPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedBIDPLPhno.IncludeLiteralsAndPrompts = False
        Me.maskedBIDPLPhno.Location = New System.Drawing.Point(202, 189)
        Me.maskedBIDPLPhno.MaskType = gloMaskControl.gloMaskType.Phone
        Me.maskedBIDPLPhno.Name = "maskedBIDPLPhno"
        Me.maskedBIDPLPhno.ReadOnly = False
        Me.maskedBIDPLPhno.Size = New System.Drawing.Size(92, 25)
        Me.maskedBIDPLPhno.TabIndex = 4
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Location = New System.Drawing.Point(0, 3)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(791, 1)
        Me.Label123.TabIndex = 124
        '
        'txtBIDPLContactName
        '
        Me.txtBIDPLContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBIDPLContactName.ForeColor = System.Drawing.Color.Black
        Me.txtBIDPLContactName.Location = New System.Drawing.Point(202, 33)
        Me.txtBIDPLContactName.MaxLength = 99
        Me.txtBIDPLContactName.Name = "txtBIDPLContactName"
        Me.txtBIDPLContactName.Size = New System.Drawing.Size(312, 22)
        Me.txtBIDPLContactName.TabIndex = 0
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.Transparent
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label125.Location = New System.Drawing.Point(130, 37)
        Me.Label125.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label125.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(69, 13)
        Me.Label125.TabIndex = 121
        Me.Label125.Text = "Contact :"
        Me.Label125.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox6
        '
        Me.TextBox6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.ForeColor = System.Drawing.Color.Black
        Me.TextBox6.Location = New System.Drawing.Point(351, 104)
        Me.TextBox6.MaxLength = 10
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(22, 22)
        Me.TextBox6.TabIndex = 3
        Me.TextBox6.TabStop = False
        Me.TextBox6.Visible = False
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.BackColor = System.Drawing.Color.Transparent
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label129.Location = New System.Drawing.Point(381, 192)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(33, 14)
        Me.Label129.TabIndex = 116
        Me.Label129.Text = "Fax :"
        '
        'TextBox7
        '
        Me.TextBox7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.ForeColor = System.Drawing.Color.Black
        Me.TextBox7.Location = New System.Drawing.Point(304, 101)
        Me.TextBox7.MaxLength = 99
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(14, 22)
        Me.TextBox7.TabIndex = 4
        Me.TextBox7.TabStop = False
        Me.TextBox7.Visible = False
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.Transparent
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label130.Location = New System.Drawing.Point(129, 218)
        Me.Label130.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label130.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(69, 13)
        Me.Label130.TabIndex = 117
        Me.Label130.Text = "Email :"
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox9
        '
        Me.TextBox9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox9.ForeColor = System.Drawing.Color.Black
        Me.TextBox9.Location = New System.Drawing.Point(367, 98)
        Me.TextBox9.MaxLength = 99
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(13, 22)
        Me.TextBox9.TabIndex = 5
        Me.TextBox9.Visible = False
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.Transparent
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label131.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label131.Location = New System.Drawing.Point(129, 193)
        Me.Label131.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label131.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(69, 13)
        Me.Label131.TabIndex = 111
        Me.Label131.Text = "Phone No :"
        Me.Label131.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox10
        '
        Me.TextBox10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox10.ForeColor = System.Drawing.Color.Black
        Me.TextBox10.Location = New System.Drawing.Point(363, 87)
        Me.TextBox10.MaxLength = 99
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(10, 22)
        Me.TextBox10.TabIndex = 2
        Me.TextBox10.TabStop = False
        Me.TextBox10.Visible = False
        '
        'txtBIDPLUrl
        '
        Me.txtBIDPLUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBIDPLUrl.ForeColor = System.Drawing.Color.Black
        Me.txtBIDPLUrl.Location = New System.Drawing.Point(202, 240)
        Me.txtBIDPLUrl.MaxLength = 99
        Me.txtBIDPLUrl.Name = "txtBIDPLUrl"
        Me.txtBIDPLUrl.Size = New System.Drawing.Size(312, 22)
        Me.txtBIDPLUrl.TabIndex = 7
        '
        'Label132
        '
        Me.Label132.AutoSize = True
        Me.Label132.BackColor = System.Drawing.Color.Transparent
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label132.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Location = New System.Drawing.Point(246, 93)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(33, 14)
        Me.Label132.TabIndex = 94
        Me.Label132.Text = "ZIP :"
        Me.Label132.Visible = False
        '
        'txtBIDPLEMail
        '
        Me.txtBIDPLEMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBIDPLEMail.ForeColor = System.Drawing.Color.Black
        Me.txtBIDPLEMail.Location = New System.Drawing.Point(202, 215)
        Me.txtBIDPLEMail.MaxLength = 99
        Me.txtBIDPLEMail.Name = "txtBIDPLEMail"
        Me.txtBIDPLEMail.Size = New System.Drawing.Size(312, 22)
        Me.txtBIDPLEMail.TabIndex = 6
        '
        'Label133
        '
        Me.Label133.AutoSize = True
        Me.Label133.BackColor = System.Drawing.Color.Transparent
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Location = New System.Drawing.Point(301, 129)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(45, 14)
        Me.Label133.TabIndex = 93
        Me.Label133.Text = "State :"
        Me.Label133.Visible = False
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.Transparent
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label134.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Location = New System.Drawing.Point(130, 243)
        Me.Label134.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label134.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(69, 13)
        Me.Label134.TabIndex = 118
        Me.Label134.Text = "URL :"
        Me.Label134.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label135
        '
        Me.Label135.AutoSize = True
        Me.Label135.BackColor = System.Drawing.Color.Transparent
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label135.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label135.Location = New System.Drawing.Point(266, 104)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(35, 14)
        Me.Label135.TabIndex = 92
        Me.Label135.Text = "City :"
        Me.Label135.Visible = False
        '
        'Label136
        '
        Me.Label136.AutoSize = True
        Me.Label136.BackColor = System.Drawing.Color.Transparent
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label136.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Location = New System.Drawing.Point(292, 90)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(65, 14)
        Me.Label136.TabIndex = 91
        Me.Label136.Text = "Address2 :"
        Me.Label136.Visible = False
        '
        'TextBox13
        '
        Me.TextBox13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.ForeColor = System.Drawing.Color.Black
        Me.TextBox13.Location = New System.Drawing.Point(189, 71)
        Me.TextBox13.MaxLength = 99
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(47, 22)
        Me.TextBox13.TabIndex = 1
        Me.TextBox13.TabStop = False
        Me.TextBox13.Visible = False
        '
        'Label137
        '
        Me.Label137.AutoSize = True
        Me.Label137.BackColor = System.Drawing.Color.Transparent
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label137.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Location = New System.Drawing.Point(234, 74)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(65, 14)
        Me.Label137.TabIndex = 90
        Me.Label137.Text = "Address1 :"
        Me.Label137.Visible = False
        '
        'gbProviderIdentification
        '
        Me.gbProviderIdentification.Controls.Add(Me.c1ProviderIdentification)
        Me.gbProviderIdentification.Controls.Add(Me.Panel11)
        Me.gbProviderIdentification.Controls.Add(Me.Label86)
        Me.gbProviderIdentification.Controls.Add(Me.Label87)
        Me.gbProviderIdentification.Controls.Add(Me.Label88)
        Me.gbProviderIdentification.Controls.Add(Me.Label89)
        Me.gbProviderIdentification.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbProviderIdentification.Location = New System.Drawing.Point(0, 0)
        Me.gbProviderIdentification.Name = "gbProviderIdentification"
        Me.gbProviderIdentification.Size = New System.Drawing.Size(791, 332)
        Me.gbProviderIdentification.TabIndex = 1
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label92)
        Me.Panel11.Controls.Add(Me.Label91)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(1, 1)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(789, 23)
        Me.Panel11.TabIndex = 128
        Me.Panel11.TabStop = True
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Location = New System.Drawing.Point(0, 22)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(789, 1)
        Me.Label92.TabIndex = 130
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.Transparent
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Location = New System.Drawing.Point(0, 0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(783, 23)
        Me.Label91.TabIndex = 129
        Me.Label91.Text = "  Additional Billing IDs"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Location = New System.Drawing.Point(790, 1)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1, 330)
        Me.Label86.TabIndex = 127
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label87.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Location = New System.Drawing.Point(0, 1)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 330)
        Me.Label87.TabIndex = 126
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Location = New System.Drawing.Point(0, 331)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(791, 1)
        Me.Label88.TabIndex = 125
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Location = New System.Drawing.Point(0, 0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(791, 1)
        Me.Label89.TabIndex = 124
        '
        'tbpgProviderCompany
        '
        Me.tbpgProviderCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgProviderCompany.Controls.Add(Me.Panel17)
        Me.tbpgProviderCompany.Controls.Add(Me.Panel23)
        Me.tbpgProviderCompany.Controls.Add(Me.Panel8)
        Me.tbpgProviderCompany.Location = New System.Drawing.Point(4, 23)
        Me.tbpgProviderCompany.Name = "tbpgProviderCompany"
        Me.tbpgProviderCompany.Size = New System.Drawing.Size(791, 616)
        Me.tbpgProviderCompany.TabIndex = 3
        Me.tbpgProviderCompany.Text = "Provider Company"
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.Label94)
        Me.Panel17.Controls.Add(Me.Label110)
        Me.Panel17.Controls.Add(Me.Label109)
        Me.Panel17.Controls.Add(Me.Panel18)
        Me.Panel17.Controls.Add(Me.mskPLFax)
        Me.Panel17.Controls.Add(Me.mskPLPager)
        Me.Panel17.Controls.Add(Me.Label102)
        Me.Panel17.Controls.Add(Me.Label103)
        Me.Panel17.Controls.Add(Me.Label104)
        Me.Panel17.Controls.Add(Me.pnlPLAddresssControl)
        Me.Panel17.Controls.Add(Me.Label105)
        Me.Panel17.Controls.Add(Me.maskedPLPhno)
        Me.Panel17.Controls.Add(Me.Label106)
        Me.Panel17.Controls.Add(Me.txtPLContactName)
        Me.Panel17.Controls.Add(Me.Label107)
        Me.Panel17.Controls.Add(Me.TextBox2)
        Me.Panel17.Controls.Add(Me.Label108)
        Me.Panel17.Controls.Add(Me.TextBox3)
        Me.Panel17.Controls.Add(Me.TextBox4)
        Me.Panel17.Controls.Add(Me.TextBox5)
        Me.Panel17.Controls.Add(Me.txtPLUrl)
        Me.Panel17.Controls.Add(Me.Label111)
        Me.Panel17.Controls.Add(Me.txtPLEMail)
        Me.Panel17.Controls.Add(Me.Label112)
        Me.Panel17.Controls.Add(Me.Label114)
        Me.Panel17.Controls.Add(Me.Label115)
        Me.Panel17.Controls.Add(Me.TextBox8)
        Me.Panel17.Controls.Add(Me.Label116)
        Me.Panel17.Controls.Add(Me.Label172)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(0, 346)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel17.Size = New System.Drawing.Size(791, 270)
        Me.Panel17.TabIndex = 196
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.Transparent
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Location = New System.Drawing.Point(140, 215)
        Me.Label94.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label94.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(69, 13)
        Me.Label94.TabIndex = 137
        Me.Label94.Text = "Email :"
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label110
        '
        Me.Label110.Location = New System.Drawing.Point(140, 189)
        Me.Label110.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label110.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(69, 13)
        Me.Label110.TabIndex = 136
        Me.Label110.Text = "Phone No :"
        Me.Label110.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label109
        '
        Me.Label109.Location = New System.Drawing.Point(141, 239)
        Me.Label109.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label109.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(69, 13)
        Me.Label109.TabIndex = 135
        Me.Label109.Text = "URL :"
        Me.Label109.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel18
        '
        Me.Panel18.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label99)
        Me.Panel18.Controls.Add(Me.Label101)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(1, 4)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(788, 23)
        Me.Panel18.TabIndex = 133
        Me.Panel18.TabStop = True
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.BackColor = System.Drawing.Color.Transparent
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label99.Location = New System.Drawing.Point(0, 0)
        Me.Label99.Name = "Label99"
        Me.Label99.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label99.Size = New System.Drawing.Size(174, 17)
        Me.Label99.TabIndex = 131
        Me.Label99.Text = " Physical Location Address"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Location = New System.Drawing.Point(0, 22)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(788, 1)
        Me.Label101.TabIndex = 130
        '
        'mskPLFax
        '
        Me.mskPLFax.AllowValidate = True
        Me.mskPLFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskPLFax.IncludeLiteralsAndPrompts = False
        Me.mskPLFax.Location = New System.Drawing.Point(423, 184)
        Me.mskPLFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskPLFax.Name = "mskPLFax"
        Me.mskPLFax.ReadOnly = False
        Me.mskPLFax.Size = New System.Drawing.Size(92, 25)
        Me.mskPLFax.TabIndex = 5
        '
        'mskPLPager
        '
        Me.mskPLPager.AllowValidate = True
        Me.mskPLPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskPLPager.IncludeLiteralsAndPrompts = False
        Me.mskPLPager.Location = New System.Drawing.Point(423, 160)
        Me.mskPLPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.mskPLPager.Name = "mskPLPager"
        Me.mskPLPager.ReadOnly = False
        Me.mskPLPager.Size = New System.Drawing.Size(92, 25)
        Me.mskPLPager.TabIndex = 3
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Location = New System.Drawing.Point(789, 4)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 265)
        Me.Label102.TabIndex = 127
        '
        'Label103
        '
        Me.Label103.AutoEllipsis = True
        Me.Label103.AutoSize = True
        Me.Label103.BackColor = System.Drawing.Color.Transparent
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Location = New System.Drawing.Point(374, 164)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(46, 14)
        Me.Label103.TabIndex = 119
        Me.Label103.Text = "Pager :"
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Location = New System.Drawing.Point(0, 4)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1, 265)
        Me.Label104.TabIndex = 126
        '
        'pnlPLAddresssControl
        '
        Me.pnlPLAddresssControl.Location = New System.Drawing.Point(131, 53)
        Me.pnlPLAddresssControl.Name = "pnlPLAddresssControl"
        Me.pnlPLAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlPLAddresssControl.TabIndex = 2
        Me.pnlPLAddresssControl.TabStop = True
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label105.Location = New System.Drawing.Point(0, 269)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(790, 1)
        Me.Label105.TabIndex = 125
        '
        'maskedPLPhno
        '
        Me.maskedPLPhno.AllowValidate = True
        Me.maskedPLPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedPLPhno.IncludeLiteralsAndPrompts = False
        Me.maskedPLPhno.Location = New System.Drawing.Point(213, 185)
        Me.maskedPLPhno.MaskType = gloMaskControl.gloMaskType.Phone
        Me.maskedPLPhno.Name = "maskedPLPhno"
        Me.maskedPLPhno.ReadOnly = False
        Me.maskedPLPhno.Size = New System.Drawing.Size(92, 25)
        Me.maskedPLPhno.TabIndex = 4
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Location = New System.Drawing.Point(0, 3)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(790, 1)
        Me.Label106.TabIndex = 124
        '
        'txtPLContactName
        '
        Me.txtPLContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPLContactName.ForeColor = System.Drawing.Color.Black
        Me.txtPLContactName.Location = New System.Drawing.Point(213, 29)
        Me.txtPLContactName.MaxLength = 99
        Me.txtPLContactName.Name = "txtPLContactName"
        Me.txtPLContactName.Size = New System.Drawing.Size(302, 22)
        Me.txtPLContactName.TabIndex = 0
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.Transparent
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Location = New System.Drawing.Point(141, 34)
        Me.Label107.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label107.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(69, 13)
        Me.Label107.TabIndex = 121
        Me.Label107.Text = "Contact :"
        Me.Label107.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(351, 100)
        Me.TextBox2.MaxLength = 10
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(22, 22)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.TabStop = False
        Me.TextBox2.Visible = False
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.BackColor = System.Drawing.Color.Transparent
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Location = New System.Drawing.Point(387, 188)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(33, 14)
        Me.Label108.TabIndex = 116
        Me.Label108.Text = "Fax :"
        Me.Label108.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.Black
        Me.TextBox3.Location = New System.Drawing.Point(304, 97)
        Me.TextBox3.MaxLength = 99
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(14, 22)
        Me.TextBox3.TabIndex = 4
        Me.TextBox3.TabStop = False
        Me.TextBox3.Visible = False
        '
        'TextBox4
        '
        Me.TextBox4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.Color.Black
        Me.TextBox4.Location = New System.Drawing.Point(367, 94)
        Me.TextBox4.MaxLength = 99
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(13, 22)
        Me.TextBox4.TabIndex = 5
        Me.TextBox4.Visible = False
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.Color.Black
        Me.TextBox5.Location = New System.Drawing.Point(363, 83)
        Me.TextBox5.MaxLength = 99
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(10, 22)
        Me.TextBox5.TabIndex = 2
        Me.TextBox5.TabStop = False
        Me.TextBox5.Visible = False
        '
        'txtPLUrl
        '
        Me.txtPLUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPLUrl.ForeColor = System.Drawing.Color.Black
        Me.txtPLUrl.Location = New System.Drawing.Point(213, 235)
        Me.txtPLUrl.MaxLength = 99
        Me.txtPLUrl.Name = "txtPLUrl"
        Me.txtPLUrl.Size = New System.Drawing.Size(304, 22)
        Me.txtPLUrl.TabIndex = 7
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.Color.Transparent
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Location = New System.Drawing.Point(246, 89)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(33, 14)
        Me.Label111.TabIndex = 94
        Me.Label111.Text = "ZIP :"
        Me.Label111.Visible = False
        '
        'txtPLEMail
        '
        Me.txtPLEMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPLEMail.ForeColor = System.Drawing.Color.Black
        Me.txtPLEMail.Location = New System.Drawing.Point(213, 210)
        Me.txtPLEMail.MaxLength = 99
        Me.txtPLEMail.Name = "txtPLEMail"
        Me.txtPLEMail.Size = New System.Drawing.Size(304, 22)
        Me.txtPLEMail.TabIndex = 6
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.BackColor = System.Drawing.Color.Transparent
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Location = New System.Drawing.Point(301, 125)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(45, 14)
        Me.Label112.TabIndex = 93
        Me.Label112.Text = "State :"
        Me.Label112.Visible = False
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.BackColor = System.Drawing.Color.Transparent
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Location = New System.Drawing.Point(266, 100)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(35, 14)
        Me.Label114.TabIndex = 92
        Me.Label114.Text = "City :"
        Me.Label114.Visible = False
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.BackColor = System.Drawing.Color.Transparent
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Location = New System.Drawing.Point(292, 86)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(65, 14)
        Me.Label115.TabIndex = 91
        Me.Label115.Text = "Address2 :"
        Me.Label115.Visible = False
        '
        'TextBox8
        '
        Me.TextBox8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox8.ForeColor = System.Drawing.Color.Black
        Me.TextBox8.Location = New System.Drawing.Point(189, 67)
        Me.TextBox8.MaxLength = 99
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(47, 22)
        Me.TextBox8.TabIndex = 1
        Me.TextBox8.TabStop = False
        Me.TextBox8.Visible = False
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.BackColor = System.Drawing.Color.Transparent
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label116.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Location = New System.Drawing.Point(234, 70)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(65, 14)
        Me.Label116.TabIndex = 90
        Me.Label116.Text = "Address1 :"
        Me.Label116.Visible = False
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label172.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label172.Location = New System.Drawing.Point(790, 3)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(1, 267)
        Me.Label172.TabIndex = 139
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.c1CompanyProvIdentification)
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Controls.Add(Me.Label150)
        Me.Panel23.Controls.Add(Me.Label151)
        Me.Panel23.Controls.Add(Me.Label152)
        Me.Panel23.Controls.Add(Me.Label153)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel23.Location = New System.Drawing.Point(0, 264)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel23.Size = New System.Drawing.Size(791, 82)
        Me.Panel23.TabIndex = 197
        '
        'c1CompanyProvIdentification
        '
        Me.c1CompanyProvIdentification.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1CompanyProvIdentification.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform
        Me.c1CompanyProvIdentification.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1CompanyProvIdentification.AutoGenerateColumns = False
        Me.c1CompanyProvIdentification.BackColor = System.Drawing.Color.GhostWhite
        Me.c1CompanyProvIdentification.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1CompanyProvIdentification.ColumnInfo = "1,1,0,0,0,95,Columns:"
        Me.c1CompanyProvIdentification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1CompanyProvIdentification.ExtendLastCol = True
        Me.c1CompanyProvIdentification.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1CompanyProvIdentification.ForeColor = System.Drawing.SystemColors.ControlText
        Me.c1CompanyProvIdentification.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.c1CompanyProvIdentification.Location = New System.Drawing.Point(1, 27)
        Me.c1CompanyProvIdentification.Name = "c1CompanyProvIdentification"
        Me.c1CompanyProvIdentification.Rows.Count = 1
        Me.c1CompanyProvIdentification.Rows.DefaultSize = 19
        Me.c1CompanyProvIdentification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.c1CompanyProvIdentification.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1CompanyProvIdentification.Size = New System.Drawing.Size(789, 54)
        Me.c1CompanyProvIdentification.StyleInfo = resources.GetString("c1CompanyProvIdentification.StyleInfo")
        Me.c1CompanyProvIdentification.TabIndex = 135
        '
        'Panel24
        '
        Me.Panel24.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Label148)
        Me.Panel24.Controls.Add(Me.Label149)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(1, 4)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(789, 23)
        Me.Panel24.TabIndex = 133
        Me.Panel24.TabStop = True
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.Transparent
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Location = New System.Drawing.Point(0, 0)
        Me.Label148.Name = "Label148"
        Me.Label148.Padding = New System.Windows.Forms.Padding(3)
        Me.Label148.Size = New System.Drawing.Size(789, 22)
        Me.Label148.TabIndex = 131
        Me.Label148.Text = " Company Additional ID"
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Location = New System.Drawing.Point(0, 22)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(789, 1)
        Me.Label149.TabIndex = 130
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label150.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Location = New System.Drawing.Point(1, 3)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(789, 1)
        Me.Label150.TabIndex = 132
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Location = New System.Drawing.Point(1, 81)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(789, 1)
        Me.Label151.TabIndex = 131
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Location = New System.Drawing.Point(790, 3)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 79)
        Me.Label152.TabIndex = 128
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Location = New System.Drawing.Point(0, 3)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(1, 79)
        Me.Label153.TabIndex = 127
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Label74)
        Me.Panel8.Controls.Add(Me.txtCmpTaxonomyCode)
        Me.Panel8.Controls.Add(Me.btn_ClearCmpTaxonomy)
        Me.Panel8.Controls.Add(Me.btn_BrowseCmpTaxonomy)
        Me.Panel8.Controls.Add(Me.Panel14)
        Me.Panel8.Controls.Add(Me.Label93)
        Me.Panel8.Controls.Add(Me.mskCompanyFax)
        Me.Panel8.Controls.Add(Me.label46)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Controls.Add(Me.txtCompanyNPI)
        Me.Panel8.Controls.Add(Me.Label73)
        Me.Panel8.Controls.Add(Me.pnlCompanyAddresssControl)
        Me.Panel8.Controls.Add(Me.txtCompanyPhone)
        Me.Panel8.Controls.Add(Me.Label75)
        Me.Panel8.Controls.Add(Me.txtCompanyName)
        Me.Panel8.Controls.Add(Me.label18)
        Me.Panel8.Controls.Add(Me.txtCompanyState)
        Me.Panel8.Controls.Add(Me.txtCompanyContactName)
        Me.Panel8.Controls.Add(Me.txtCompanyZip)
        Me.Panel8.Controls.Add(Me.label17)
        Me.Panel8.Controls.Add(Me.txtCompanyCity)
        Me.Panel8.Controls.Add(Me.label47)
        Me.Panel8.Controls.Add(Me.txtCompanyAddress2)
        Me.Panel8.Controls.Add(Me.label14)
        Me.Panel8.Controls.Add(Me.label24)
        Me.Panel8.Controls.Add(Me.label15)
        Me.Panel8.Controls.Add(Me.label22)
        Me.Panel8.Controls.Add(Me.label16)
        Me.Panel8.Controls.Add(Me.label21)
        Me.Panel8.Controls.Add(Me.txtCompanyEmail)
        Me.Panel8.Controls.Add(Me.label20)
        Me.Panel8.Controls.Add(Me.txtCompanyTaxID)
        Me.Panel8.Controls.Add(Me.label19)
        Me.Panel8.Controls.Add(Me.chkCompanyAsAbove)
        Me.Panel8.Controls.Add(Me.txtCompanyAddress1)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(791, 264)
        Me.Panel8.TabIndex = 1
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Location = New System.Drawing.Point(428, 239)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(72, 14)
        Me.Label74.TabIndex = 136
        Me.Label74.Text = "Taxonomy :"
        '
        'txtCmpTaxonomyCode
        '
        Me.txtCmpTaxonomyCode.BackColor = System.Drawing.Color.White
        Me.txtCmpTaxonomyCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCmpTaxonomyCode.ForeColor = System.Drawing.Color.Black
        Me.txtCmpTaxonomyCode.Location = New System.Drawing.Point(500, 235)
        Me.txtCmpTaxonomyCode.MaxLength = 99
        Me.txtCmpTaxonomyCode.Name = "txtCmpTaxonomyCode"
        Me.txtCmpTaxonomyCode.ReadOnly = True
        Me.txtCmpTaxonomyCode.Size = New System.Drawing.Size(229, 22)
        Me.txtCmpTaxonomyCode.TabIndex = 10
        '
        'btn_ClearCmpTaxonomy
        '
        Me.btn_ClearCmpTaxonomy.BackgroundImage = CType(resources.GetObject("btn_ClearCmpTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_ClearCmpTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ClearCmpTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearCmpTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ClearCmpTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearCmpTaxonomy.Image = CType(resources.GetObject("btn_ClearCmpTaxonomy.Image"), System.Drawing.Image)
        Me.btn_ClearCmpTaxonomy.Location = New System.Drawing.Point(759, 235)
        Me.btn_ClearCmpTaxonomy.Name = "btn_ClearCmpTaxonomy"
        Me.btn_ClearCmpTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_ClearCmpTaxonomy.TabIndex = 12
        Me.btn_ClearCmpTaxonomy.UseVisualStyleBackColor = True
        '
        'btn_BrowseCmpTaxonomy
        '
        Me.btn_BrowseCmpTaxonomy.BackgroundImage = CType(resources.GetObject("btn_BrowseCmpTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_BrowseCmpTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_BrowseCmpTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_BrowseCmpTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_BrowseCmpTaxonomy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_BrowseCmpTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_BrowseCmpTaxonomy.Image = CType(resources.GetObject("btn_BrowseCmpTaxonomy.Image"), System.Drawing.Image)
        Me.btn_BrowseCmpTaxonomy.Location = New System.Drawing.Point(734, 235)
        Me.btn_BrowseCmpTaxonomy.Name = "btn_BrowseCmpTaxonomy"
        Me.btn_BrowseCmpTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_BrowseCmpTaxonomy.TabIndex = 11
        Me.btn_BrowseCmpTaxonomy.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label90)
        Me.Panel14.Controls.Add(Me.Label95)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(1, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(789, 23)
        Me.Panel14.TabIndex = 132
        Me.Panel14.TabStop = True
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.Transparent
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Padding = New System.Windows.Forms.Padding(3)
        Me.Label90.Size = New System.Drawing.Size(789, 22)
        Me.Label90.TabIndex = 131
        Me.Label90.Text = " Company Mailing Address"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Location = New System.Drawing.Point(0, 22)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(789, 1)
        Me.Label95.TabIndex = 130
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Location = New System.Drawing.Point(1, 263)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(789, 1)
        Me.Label93.TabIndex = 130
        '
        'mskCompanyFax
        '
        Me.mskCompanyFax.AllowValidate = True
        Me.mskCompanyFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskCompanyFax.IncludeLiteralsAndPrompts = False
        Me.mskCompanyFax.Location = New System.Drawing.Point(364, 209)
        Me.mskCompanyFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskCompanyFax.Name = "mskCompanyFax"
        Me.mskCompanyFax.ReadOnly = False
        Me.mskCompanyFax.Size = New System.Drawing.Size(92, 25)
        Me.mskCompanyFax.TabIndex = 6
        '
        'label46
        '
        Me.label46.AutoSize = True
        Me.label46.BackColor = System.Drawing.Color.Transparent
        Me.label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label46.Location = New System.Drawing.Point(466, 214)
        Me.label46.Name = "label46"
        Me.label46.Size = New System.Drawing.Size(34, 14)
        Me.label46.TabIndex = 117
        Me.label46.Text = "NPI :"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(790, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 263)
        Me.Label23.TabIndex = 127
        '
        'txtCompanyNPI
        '
        Me.txtCompanyNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyNPI.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyNPI.Location = New System.Drawing.Point(500, 210)
        Me.txtCompanyNPI.MaxLength = 10
        Me.txtCompanyNPI.Name = "txtCompanyNPI"
        Me.txtCompanyNPI.Size = New System.Drawing.Size(112, 22)
        Me.txtCompanyNPI.TabIndex = 8
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Location = New System.Drawing.Point(0, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 263)
        Me.Label73.TabIndex = 126
        '
        'pnlCompanyAddresssControl
        '
        Me.pnlCompanyAddresssControl.Location = New System.Drawing.Point(131, 76)
        Me.pnlCompanyAddresssControl.Name = "pnlCompanyAddresssControl"
        Me.pnlCompanyAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlCompanyAddresssControl.TabIndex = 3
        Me.pnlCompanyAddresssControl.TabStop = True
        '
        'txtCompanyPhone
        '
        Me.txtCompanyPhone.AllowValidate = True
        Me.txtCompanyPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyPhone.IncludeLiteralsAndPrompts = False
        Me.txtCompanyPhone.Location = New System.Drawing.Point(211, 209)
        Me.txtCompanyPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtCompanyPhone.Name = "txtCompanyPhone"
        Me.txtCompanyPhone.ReadOnly = False
        Me.txtCompanyPhone.Size = New System.Drawing.Size(92, 25)
        Me.txtCompanyPhone.TabIndex = 5
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Location = New System.Drawing.Point(0, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(791, 1)
        Me.Label75.TabIndex = 124
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyName.Location = New System.Drawing.Point(213, 28)
        Me.txtCompanyName.MaxLength = 99
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(314, 22)
        Me.txtCompanyName.TabIndex = 0
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.BackColor = System.Drawing.Color.Transparent
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Location = New System.Drawing.Point(146, 32)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(65, 14)
        Me.label18.TabIndex = 121
        Me.label18.Text = "Company :"
        '
        'txtCompanyState
        '
        Me.txtCompanyState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyState.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyState.Location = New System.Drawing.Point(431, 129)
        Me.txtCompanyState.MaxLength = 99
        Me.txtCompanyState.Name = "txtCompanyState"
        Me.txtCompanyState.Size = New System.Drawing.Size(16, 22)
        Me.txtCompanyState.TabIndex = 6
        Me.txtCompanyState.TabStop = False
        Me.txtCompanyState.Visible = False
        '
        'txtCompanyContactName
        '
        Me.txtCompanyContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyContactName.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyContactName.Location = New System.Drawing.Point(213, 53)
        Me.txtCompanyContactName.MaxLength = 99
        Me.txtCompanyContactName.Name = "txtCompanyContactName"
        Me.txtCompanyContactName.Size = New System.Drawing.Size(314, 22)
        Me.txtCompanyContactName.TabIndex = 2
        '
        'txtCompanyZip
        '
        Me.txtCompanyZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyZip.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyZip.Location = New System.Drawing.Point(361, 103)
        Me.txtCompanyZip.MaxLength = 10
        Me.txtCompanyZip.Name = "txtCompanyZip"
        Me.txtCompanyZip.Size = New System.Drawing.Size(11, 22)
        Me.txtCompanyZip.TabIndex = 7
        Me.txtCompanyZip.TabStop = False
        Me.txtCompanyZip.Visible = False
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.BackColor = System.Drawing.Color.Transparent
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label17.Location = New System.Drawing.Point(154, 56)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(58, 14)
        Me.label17.TabIndex = 119
        Me.label17.Text = "Contact :"
        '
        'txtCompanyCity
        '
        Me.txtCompanyCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyCity.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyCity.Location = New System.Drawing.Point(306, 98)
        Me.txtCompanyCity.MaxLength = 99
        Me.txtCompanyCity.Name = "txtCompanyCity"
        Me.txtCompanyCity.Size = New System.Drawing.Size(40, 22)
        Me.txtCompanyCity.TabIndex = 5
        Me.txtCompanyCity.TabStop = False
        Me.txtCompanyCity.Visible = False
        '
        'label47
        '
        Me.label47.AutoSize = True
        Me.label47.BackColor = System.Drawing.Color.Transparent
        Me.label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label47.Location = New System.Drawing.Point(618, 214)
        Me.label47.Name = "label47"
        Me.label47.Size = New System.Drawing.Size(51, 14)
        Me.label47.TabIndex = 116
        Me.label47.Text = "Tax ID :"
        '
        'txtCompanyAddress2
        '
        Me.txtCompanyAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyAddress2.Location = New System.Drawing.Point(401, 101)
        Me.txtCompanyAddress2.MaxLength = 99
        Me.txtCompanyAddress2.Name = "txtCompanyAddress2"
        Me.txtCompanyAddress2.Size = New System.Drawing.Size(43, 22)
        Me.txtCompanyAddress2.TabIndex = 4
        Me.txtCompanyAddress2.TabStop = False
        Me.txtCompanyAddress2.Visible = False
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.BackColor = System.Drawing.Color.Transparent
        Me.label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label14.Location = New System.Drawing.Point(330, 214)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(33, 14)
        Me.label14.TabIndex = 116
        Me.label14.Text = "Fax :"
        '
        'label24
        '
        Me.label24.AutoSize = True
        Me.label24.BackColor = System.Drawing.Color.Transparent
        Me.label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Location = New System.Drawing.Point(268, 109)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(33, 14)
        Me.label24.TabIndex = 94
        Me.label24.Text = "ZIP :"
        Me.label24.Visible = False
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.Color.Transparent
        Me.label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label15.Location = New System.Drawing.Point(169, 239)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(42, 14)
        Me.label15.TabIndex = 117
        Me.label15.Text = "Email :"
        '
        'label22
        '
        Me.label22.AutoSize = True
        Me.label22.BackColor = System.Drawing.Color.Transparent
        Me.label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label22.Location = New System.Drawing.Point(312, 103)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(45, 14)
        Me.label22.TabIndex = 93
        Me.label22.Text = "State :"
        Me.label22.Visible = False
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.BackColor = System.Drawing.Color.Transparent
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label16.Location = New System.Drawing.Point(141, 214)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(69, 14)
        Me.label16.TabIndex = 111
        Me.label16.Text = "Phone No :"
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label21.Location = New System.Drawing.Point(278, 87)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(35, 14)
        Me.label21.TabIndex = 92
        Me.label21.Text = "City :"
        Me.label21.Visible = False
        '
        'txtCompanyEmail
        '
        Me.txtCompanyEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyEmail.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyEmail.Location = New System.Drawing.Point(213, 235)
        Me.txtCompanyEmail.MaxLength = 99
        Me.txtCompanyEmail.Name = "txtCompanyEmail"
        Me.txtCompanyEmail.Size = New System.Drawing.Size(209, 22)
        Me.txtCompanyEmail.TabIndex = 7
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.BackColor = System.Drawing.Color.Transparent
        Me.label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label20.Location = New System.Drawing.Point(364, 106)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(65, 14)
        Me.label20.TabIndex = 91
        Me.label20.Text = "Address2 :"
        Me.label20.Visible = False
        '
        'txtCompanyTaxID
        '
        Me.txtCompanyTaxID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyTaxID.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyTaxID.Location = New System.Drawing.Point(669, 210)
        Me.txtCompanyTaxID.MaxLength = 9
        Me.txtCompanyTaxID.Name = "txtCompanyTaxID"
        Me.txtCompanyTaxID.Size = New System.Drawing.Size(112, 22)
        Me.txtCompanyTaxID.TabIndex = 9
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.BackColor = System.Drawing.Color.Transparent
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Location = New System.Drawing.Point(312, 84)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(65, 14)
        Me.label19.TabIndex = 90
        Me.label19.Text = "Address1 :"
        Me.label19.Visible = False
        '
        'chkCompanyAsAbove
        '
        Me.chkCompanyAsAbove.AutoSize = True
        Me.chkCompanyAsAbove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompanyAsAbove.Location = New System.Drawing.Point(535, 30)
        Me.chkCompanyAsAbove.Name = "chkCompanyAsAbove"
        Me.chkCompanyAsAbove.Size = New System.Drawing.Size(166, 18)
        Me.chkCompanyAsAbove.TabIndex = 1
        Me.chkCompanyAsAbove.Text = "Same as Provider Address"
        Me.chkCompanyAsAbove.UseVisualStyleBackColor = True
        '
        'txtCompanyAddress1
        '
        Me.txtCompanyAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyAddress1.Location = New System.Drawing.Point(373, 78)
        Me.txtCompanyAddress1.MaxLength = 99
        Me.txtCompanyAddress1.Name = "txtCompanyAddress1"
        Me.txtCompanyAddress1.Size = New System.Drawing.Size(78, 22)
        Me.txtCompanyAddress1.TabIndex = 2
        Me.txtCompanyAddress1.TabStop = False
        Me.txtCompanyAddress1.Visible = False
        '
        'tbpgStatement
        '
        Me.tbpgStatement.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgStatement.Controls.Add(Me.Panel9)
        Me.tbpgStatement.Location = New System.Drawing.Point(4, 23)
        Me.tbpgStatement.Name = "tbpgStatement"
        Me.tbpgStatement.Size = New System.Drawing.Size(791, 616)
        Me.tbpgStatement.TabIndex = 2
        Me.tbpgStatement.Text = "Statements"
        Me.tbpgStatement.UseVisualStyleBackColor = True
        '
        'tbpgPrvdrMultCmpny
        '
        Me.tbpgPrvdrMultCmpny.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgPrvdrMultCmpny.Controls.Add(Me.Panel28)
        Me.tbpgPrvdrMultCmpny.Controls.Add(Me.Panel26)
        Me.tbpgPrvdrMultCmpny.Controls.Add(Me.Panel19)
        Me.tbpgPrvdrMultCmpny.Location = New System.Drawing.Point(4, 23)
        Me.tbpgPrvdrMultCmpny.Name = "tbpgPrvdrMultCmpny"
        Me.tbpgPrvdrMultCmpny.Size = New System.Drawing.Size(791, 616)
        Me.tbpgPrvdrMultCmpny.TabIndex = 4
        Me.tbpgPrvdrMultCmpny.Text = "Provider Other Company "
        '
        'Panel28
        '
        Me.Panel28.AutoSize = True
        Me.Panel28.Controls.Add(Me.mskMltCompanyPhys)
        Me.Panel28.Controls.Add(Me.Label167)
        Me.Panel28.Controls.Add(Me.Label168)
        Me.Panel28.Controls.Add(Me.Label169)
        Me.Panel28.Controls.Add(Me.Panel29)
        Me.Panel28.Controls.Add(Me.mskMltCompanyPhysFax)
        Me.Panel28.Controls.Add(Me.mskMltCompanyPhysPager)
        Me.Panel28.Controls.Add(Me.Label173)
        Me.Panel28.Controls.Add(Me.Label174)
        Me.Panel28.Controls.Add(Me.pnlMltCompanyPhysAdd)
        Me.Panel28.Controls.Add(Me.Label175)
        Me.Panel28.Controls.Add(Me.Label176)
        Me.Panel28.Controls.Add(Me.txtMltCompanyPhysContactName)
        Me.Panel28.Controls.Add(Me.Label177)
        Me.Panel28.Controls.Add(Me.TextBox23)
        Me.Panel28.Controls.Add(Me.Label178)
        Me.Panel28.Controls.Add(Me.TextBox24)
        Me.Panel28.Controls.Add(Me.TextBox25)
        Me.Panel28.Controls.Add(Me.TextBox26)
        Me.Panel28.Controls.Add(Me.txtMltCompanyPhysURL)
        Me.Panel28.Controls.Add(Me.Label179)
        Me.Panel28.Controls.Add(Me.txtMltCompanyPhysMail)
        Me.Panel28.Controls.Add(Me.Label180)
        Me.Panel28.Controls.Add(Me.Label181)
        Me.Panel28.Controls.Add(Me.Label182)
        Me.Panel28.Controls.Add(Me.TextBox29)
        Me.Panel28.Controls.Add(Me.Label183)
        Me.Panel28.Controls.Add(Me.Label185)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(0, 369)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel28.Size = New System.Drawing.Size(791, 247)
        Me.Panel28.TabIndex = 199
        '
        'mskMltCompanyPhys
        '
        Me.mskMltCompanyPhys.AllowValidate = True
        Me.mskMltCompanyPhys.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMltCompanyPhys.IncludeLiteralsAndPrompts = False
        Me.mskMltCompanyPhys.Location = New System.Drawing.Point(213, 181)
        Me.mskMltCompanyPhys.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskMltCompanyPhys.Name = "mskMltCompanyPhys"
        Me.mskMltCompanyPhys.ReadOnly = False
        Me.mskMltCompanyPhys.Size = New System.Drawing.Size(92, 25)
        Me.mskMltCompanyPhys.TabIndex = 4
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.Transparent
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label167.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label167.Location = New System.Drawing.Point(140, 212)
        Me.Label167.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label167.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(69, 13)
        Me.Label167.TabIndex = 137
        Me.Label167.Text = "Email :"
        Me.Label167.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label168
        '
        Me.Label168.Location = New System.Drawing.Point(140, 187)
        Me.Label168.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label168.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(69, 13)
        Me.Label168.TabIndex = 136
        Me.Label168.Text = "Phone No :"
        Me.Label168.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label169
        '
        Me.Label169.Location = New System.Drawing.Point(141, 234)
        Me.Label169.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label169.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(69, 13)
        Me.Label169.TabIndex = 135
        Me.Label169.Text = "URL :"
        Me.Label169.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel29
        '
        Me.Panel29.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel29.Controls.Add(Me.Label170)
        Me.Panel29.Controls.Add(Me.Label171)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(1, 4)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(789, 23)
        Me.Panel29.TabIndex = 133
        Me.Panel29.TabStop = True
        '
        'Label170
        '
        Me.Label170.AutoSize = True
        Me.Label170.BackColor = System.Drawing.Color.Transparent
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label170.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label170.Location = New System.Drawing.Point(0, 0)
        Me.Label170.Name = "Label170"
        Me.Label170.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label170.Size = New System.Drawing.Size(174, 17)
        Me.Label170.TabIndex = 131
        Me.Label170.Text = " Physical Location Address"
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label171.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label171.Location = New System.Drawing.Point(0, 22)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(789, 1)
        Me.Label171.TabIndex = 130
        '
        'mskMltCompanyPhysFax
        '
        Me.mskMltCompanyPhysFax.AllowValidate = True
        Me.mskMltCompanyPhysFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMltCompanyPhysFax.IncludeLiteralsAndPrompts = False
        Me.mskMltCompanyPhysFax.Location = New System.Drawing.Point(423, 181)
        Me.mskMltCompanyPhysFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskMltCompanyPhysFax.Name = "mskMltCompanyPhysFax"
        Me.mskMltCompanyPhysFax.ReadOnly = False
        Me.mskMltCompanyPhysFax.Size = New System.Drawing.Size(92, 25)
        Me.mskMltCompanyPhysFax.TabIndex = 5
        '
        'mskMltCompanyPhysPager
        '
        Me.mskMltCompanyPhysPager.AllowValidate = True
        Me.mskMltCompanyPhysPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMltCompanyPhysPager.IncludeLiteralsAndPrompts = False
        Me.mskMltCompanyPhysPager.Location = New System.Drawing.Point(423, 157)
        Me.mskMltCompanyPhysPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.mskMltCompanyPhysPager.Name = "mskMltCompanyPhysPager"
        Me.mskMltCompanyPhysPager.ReadOnly = False
        Me.mskMltCompanyPhysPager.Size = New System.Drawing.Size(92, 25)
        Me.mskMltCompanyPhysPager.TabIndex = 3
        '
        'Label173
        '
        Me.Label173.AutoEllipsis = True
        Me.Label173.AutoSize = True
        Me.Label173.BackColor = System.Drawing.Color.Transparent
        Me.Label173.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label173.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label173.Location = New System.Drawing.Point(374, 161)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(46, 14)
        Me.Label173.TabIndex = 119
        Me.Label173.Text = "Pager :"
        Me.Label173.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label174.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label174.Location = New System.Drawing.Point(0, 4)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(1, 242)
        Me.Label174.TabIndex = 126
        '
        'pnlMltCompanyPhysAdd
        '
        Me.pnlMltCompanyPhysAdd.Location = New System.Drawing.Point(131, 53)
        Me.pnlMltCompanyPhysAdd.Name = "pnlMltCompanyPhysAdd"
        Me.pnlMltCompanyPhysAdd.Size = New System.Drawing.Size(325, 132)
        Me.pnlMltCompanyPhysAdd.TabIndex = 2
        Me.pnlMltCompanyPhysAdd.TabStop = True
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label175.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Location = New System.Drawing.Point(0, 246)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(790, 1)
        Me.Label175.TabIndex = 125
        '
        'Label176
        '
        Me.Label176.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label176.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label176.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label176.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label176.Location = New System.Drawing.Point(0, 3)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(790, 1)
        Me.Label176.TabIndex = 124
        '
        'txtMltCompanyPhysContactName
        '
        Me.txtMltCompanyPhysContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyPhysContactName.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyPhysContactName.Location = New System.Drawing.Point(213, 29)
        Me.txtMltCompanyPhysContactName.MaxLength = 99
        Me.txtMltCompanyPhysContactName.Name = "txtMltCompanyPhysContactName"
        Me.txtMltCompanyPhysContactName.Size = New System.Drawing.Size(302, 22)
        Me.txtMltCompanyPhysContactName.TabIndex = 0
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.Transparent
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label177.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label177.Location = New System.Drawing.Point(141, 34)
        Me.Label177.MaximumSize = New System.Drawing.Size(69, 13)
        Me.Label177.MinimumSize = New System.Drawing.Size(69, 13)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(69, 13)
        Me.Label177.TabIndex = 121
        Me.Label177.Text = "Contact :"
        Me.Label177.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox23
        '
        Me.TextBox23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox23.ForeColor = System.Drawing.Color.Black
        Me.TextBox23.Location = New System.Drawing.Point(351, 100)
        Me.TextBox23.MaxLength = 10
        Me.TextBox23.Name = "TextBox23"
        Me.TextBox23.Size = New System.Drawing.Size(22, 22)
        Me.TextBox23.TabIndex = 3
        Me.TextBox23.TabStop = False
        Me.TextBox23.Visible = False
        '
        'Label178
        '
        Me.Label178.AutoSize = True
        Me.Label178.BackColor = System.Drawing.Color.Transparent
        Me.Label178.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label178.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label178.Location = New System.Drawing.Point(387, 186)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(33, 14)
        Me.Label178.TabIndex = 116
        Me.Label178.Text = "Fax :"
        Me.Label178.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox24
        '
        Me.TextBox24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox24.ForeColor = System.Drawing.Color.Black
        Me.TextBox24.Location = New System.Drawing.Point(304, 97)
        Me.TextBox24.MaxLength = 99
        Me.TextBox24.Name = "TextBox24"
        Me.TextBox24.Size = New System.Drawing.Size(14, 22)
        Me.TextBox24.TabIndex = 4
        Me.TextBox24.TabStop = False
        Me.TextBox24.Visible = False
        '
        'TextBox25
        '
        Me.TextBox25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox25.ForeColor = System.Drawing.Color.Black
        Me.TextBox25.Location = New System.Drawing.Point(367, 94)
        Me.TextBox25.MaxLength = 99
        Me.TextBox25.Name = "TextBox25"
        Me.TextBox25.Size = New System.Drawing.Size(13, 22)
        Me.TextBox25.TabIndex = 5
        Me.TextBox25.Visible = False
        '
        'TextBox26
        '
        Me.TextBox26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox26.ForeColor = System.Drawing.Color.Black
        Me.TextBox26.Location = New System.Drawing.Point(363, 83)
        Me.TextBox26.MaxLength = 99
        Me.TextBox26.Name = "TextBox26"
        Me.TextBox26.Size = New System.Drawing.Size(10, 22)
        Me.TextBox26.TabIndex = 2
        Me.TextBox26.TabStop = False
        Me.TextBox26.Visible = False
        '
        'txtMltCompanyPhysURL
        '
        Me.txtMltCompanyPhysURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyPhysURL.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyPhysURL.Location = New System.Drawing.Point(213, 231)
        Me.txtMltCompanyPhysURL.MaxLength = 99
        Me.txtMltCompanyPhysURL.Name = "txtMltCompanyPhysURL"
        Me.txtMltCompanyPhysURL.Size = New System.Drawing.Size(304, 22)
        Me.txtMltCompanyPhysURL.TabIndex = 7
        '
        'Label179
        '
        Me.Label179.AutoSize = True
        Me.Label179.BackColor = System.Drawing.Color.Transparent
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label179.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label179.Location = New System.Drawing.Point(246, 89)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(33, 14)
        Me.Label179.TabIndex = 94
        Me.Label179.Text = "ZIP :"
        Me.Label179.Visible = False
        '
        'txtMltCompanyPhysMail
        '
        Me.txtMltCompanyPhysMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyPhysMail.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyPhysMail.Location = New System.Drawing.Point(213, 207)
        Me.txtMltCompanyPhysMail.MaxLength = 99
        Me.txtMltCompanyPhysMail.Name = "txtMltCompanyPhysMail"
        Me.txtMltCompanyPhysMail.Size = New System.Drawing.Size(304, 22)
        Me.txtMltCompanyPhysMail.TabIndex = 6
        '
        'Label180
        '
        Me.Label180.AutoSize = True
        Me.Label180.BackColor = System.Drawing.Color.Transparent
        Me.Label180.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label180.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label180.Location = New System.Drawing.Point(301, 125)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(45, 14)
        Me.Label180.TabIndex = 93
        Me.Label180.Text = "State :"
        Me.Label180.Visible = False
        '
        'Label181
        '
        Me.Label181.AutoSize = True
        Me.Label181.BackColor = System.Drawing.Color.Transparent
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label181.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label181.Location = New System.Drawing.Point(266, 100)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(35, 14)
        Me.Label181.TabIndex = 92
        Me.Label181.Text = "City :"
        Me.Label181.Visible = False
        '
        'Label182
        '
        Me.Label182.AutoSize = True
        Me.Label182.BackColor = System.Drawing.Color.Transparent
        Me.Label182.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label182.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label182.Location = New System.Drawing.Point(292, 86)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(65, 14)
        Me.Label182.TabIndex = 91
        Me.Label182.Text = "Address2 :"
        Me.Label182.Visible = False
        '
        'TextBox29
        '
        Me.TextBox29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox29.ForeColor = System.Drawing.Color.Black
        Me.TextBox29.Location = New System.Drawing.Point(189, 67)
        Me.TextBox29.MaxLength = 99
        Me.TextBox29.Name = "TextBox29"
        Me.TextBox29.Size = New System.Drawing.Size(47, 22)
        Me.TextBox29.TabIndex = 1
        Me.TextBox29.TabStop = False
        Me.TextBox29.Visible = False
        '
        'Label183
        '
        Me.Label183.AutoSize = True
        Me.Label183.BackColor = System.Drawing.Color.Transparent
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label183.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label183.Location = New System.Drawing.Point(234, 70)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(65, 14)
        Me.Label183.TabIndex = 90
        Me.Label183.Text = "Address1 :"
        Me.Label183.Visible = False
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label185.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Location = New System.Drawing.Point(790, 3)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1, 244)
        Me.Label185.TabIndex = 138
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.c1MltCompantAddtnlID)
        Me.Panel26.Controls.Add(Me.Panel27)
        Me.Panel26.Controls.Add(Me.Label163)
        Me.Panel26.Controls.Add(Me.Label164)
        Me.Panel26.Controls.Add(Me.Label165)
        Me.Panel26.Controls.Add(Me.Label166)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel26.Location = New System.Drawing.Point(0, 287)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel26.Size = New System.Drawing.Size(791, 82)
        Me.Panel26.TabIndex = 198
        '
        'c1MltCompantAddtnlID
        '
        Me.c1MltCompantAddtnlID.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1MltCompantAddtnlID.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform
        Me.c1MltCompantAddtnlID.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1MltCompantAddtnlID.AutoGenerateColumns = False
        Me.c1MltCompantAddtnlID.BackColor = System.Drawing.Color.GhostWhite
        Me.c1MltCompantAddtnlID.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1MltCompantAddtnlID.ColumnInfo = "1,1,0,0,0,95,Columns:"
        Me.c1MltCompantAddtnlID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1MltCompantAddtnlID.ExtendLastCol = True
        Me.c1MltCompantAddtnlID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1MltCompantAddtnlID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.c1MltCompantAddtnlID.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.c1MltCompantAddtnlID.Location = New System.Drawing.Point(1, 27)
        Me.c1MltCompantAddtnlID.Name = "c1MltCompantAddtnlID"
        Me.c1MltCompantAddtnlID.Rows.Count = 1
        Me.c1MltCompantAddtnlID.Rows.DefaultSize = 19
        Me.c1MltCompantAddtnlID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.c1MltCompantAddtnlID.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1MltCompantAddtnlID.Size = New System.Drawing.Size(789, 54)
        Me.c1MltCompantAddtnlID.StyleInfo = resources.GetString("c1MltCompantAddtnlID.StyleInfo")
        Me.c1MltCompantAddtnlID.TabIndex = 135
        '
        'Panel27
        '
        Me.Panel27.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel27.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel27.Controls.Add(Me.Label161)
        Me.Panel27.Controls.Add(Me.Label162)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel27.Location = New System.Drawing.Point(1, 4)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(789, 23)
        Me.Panel27.TabIndex = 133
        Me.Panel27.TabStop = True
        '
        'Label161
        '
        Me.Label161.BackColor = System.Drawing.Color.Transparent
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Location = New System.Drawing.Point(0, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Padding = New System.Windows.Forms.Padding(3)
        Me.Label161.Size = New System.Drawing.Size(789, 22)
        Me.Label161.TabIndex = 131
        Me.Label161.Text = " Company Additional ID"
        '
        'Label162
        '
        Me.Label162.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label162.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label162.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label162.Location = New System.Drawing.Point(0, 22)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(789, 1)
        Me.Label162.TabIndex = 130
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label163.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label163.Location = New System.Drawing.Point(1, 3)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(789, 1)
        Me.Label163.TabIndex = 132
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Location = New System.Drawing.Point(1, 81)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(789, 1)
        Me.Label164.TabIndex = 131
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label165.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label165.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label165.Location = New System.Drawing.Point(790, 3)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(1, 79)
        Me.Label165.TabIndex = 128
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label166.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label166.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Location = New System.Drawing.Point(0, 3)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(1, 79)
        Me.Label166.TabIndex = 127
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.CmbProviderCompany)
        Me.Panel19.Controls.Add(Me.Label184)
        Me.Panel19.Controls.Add(Me.Label70)
        Me.Panel19.Controls.Add(Me.txtMltCompanyTaxonomy)
        Me.Panel19.Controls.Add(Me.btnTaxonomyClear)
        Me.Panel19.Controls.Add(Me.btnTaxonomySelect)
        Me.Panel19.Controls.Add(Me.Panel22)
        Me.Panel19.Controls.Add(Me.Label139)
        Me.Panel19.Controls.Add(Me.mskMltCompanyFax)
        Me.Panel19.Controls.Add(Me.Label140)
        Me.Panel19.Controls.Add(Me.Label141)
        Me.Panel19.Controls.Add(Me.txtMltCompanyNPI)
        Me.Panel19.Controls.Add(Me.Label142)
        Me.Panel19.Controls.Add(Me.pnlmltcompanymaillingAdd)
        Me.Panel19.Controls.Add(Me.mskmltCompanyPhoneNo)
        Me.Panel19.Controls.Add(Me.Label143)
        Me.Panel19.Controls.Add(Me.TxtMltCompanyName)
        Me.Panel19.Controls.Add(Me.Label144)
        Me.Panel19.Controls.Add(Me.TextBox14)
        Me.Panel19.Controls.Add(Me.txtmltCompanyContact)
        Me.Panel19.Controls.Add(Me.TextBox16)
        Me.Panel19.Controls.Add(Me.Label145)
        Me.Panel19.Controls.Add(Me.TextBox17)
        Me.Panel19.Controls.Add(Me.Label146)
        Me.Panel19.Controls.Add(Me.TextBox18)
        Me.Panel19.Controls.Add(Me.Label147)
        Me.Panel19.Controls.Add(Me.Label154)
        Me.Panel19.Controls.Add(Me.Label155)
        Me.Panel19.Controls.Add(Me.Label156)
        Me.Panel19.Controls.Add(Me.Label157)
        Me.Panel19.Controls.Add(Me.Label158)
        Me.Panel19.Controls.Add(Me.txtMltCompanyEmail)
        Me.Panel19.Controls.Add(Me.Label159)
        Me.Panel19.Controls.Add(Me.txtMltCompanyTaxId)
        Me.Panel19.Controls.Add(Me.Label160)
        Me.Panel19.Controls.Add(Me.chkmltSameAsPrvdrAdd)
        Me.Panel19.Controls.Add(Me.TextBox21)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(0, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(791, 287)
        Me.Panel19.TabIndex = 2
        '
        'CmbProviderCompany
        '
        Me.CmbProviderCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbProviderCompany.Location = New System.Drawing.Point(213, 27)
        Me.CmbProviderCompany.Name = "CmbProviderCompany"
        Me.CmbProviderCompany.Size = New System.Drawing.Size(312, 22)
        Me.CmbProviderCompany.TabIndex = 0
        '
        'Label184
        '
        Me.Label184.AutoSize = True
        Me.Label184.BackColor = System.Drawing.Color.Transparent
        Me.Label184.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label184.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label184.Location = New System.Drawing.Point(98, 30)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(113, 14)
        Me.Label184.TabIndex = 138
        Me.Label184.Text = "Provider Company :"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.BackColor = System.Drawing.Color.Transparent
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Location = New System.Drawing.Point(427, 266)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(72, 14)
        Me.Label70.TabIndex = 136
        Me.Label70.Text = "Taxonomy :"
        '
        'txtMltCompanyTaxonomy
        '
        Me.txtMltCompanyTaxonomy.BackColor = System.Drawing.Color.White
        Me.txtMltCompanyTaxonomy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyTaxonomy.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyTaxonomy.Location = New System.Drawing.Point(499, 262)
        Me.txtMltCompanyTaxonomy.MaxLength = 99
        Me.txtMltCompanyTaxonomy.Name = "txtMltCompanyTaxonomy"
        Me.txtMltCompanyTaxonomy.ReadOnly = True
        Me.txtMltCompanyTaxonomy.Size = New System.Drawing.Size(230, 22)
        Me.txtMltCompanyTaxonomy.TabIndex = 11
        '
        'btnTaxonomyClear
        '
        Me.btnTaxonomyClear.BackgroundImage = CType(resources.GetObject("btnTaxonomyClear.BackgroundImage"), System.Drawing.Image)
        Me.btnTaxonomyClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTaxonomyClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTaxonomyClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTaxonomyClear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTaxonomyClear.Image = CType(resources.GetObject("btnTaxonomyClear.Image"), System.Drawing.Image)
        Me.btnTaxonomyClear.Location = New System.Drawing.Point(760, 262)
        Me.btnTaxonomyClear.Name = "btnTaxonomyClear"
        Me.btnTaxonomyClear.Size = New System.Drawing.Size(22, 22)
        Me.btnTaxonomyClear.TabIndex = 12
        Me.btnTaxonomyClear.UseVisualStyleBackColor = True
        '
        'btnTaxonomySelect
        '
        Me.btnTaxonomySelect.BackgroundImage = CType(resources.GetObject("btnTaxonomySelect.BackgroundImage"), System.Drawing.Image)
        Me.btnTaxonomySelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTaxonomySelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTaxonomySelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTaxonomySelect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTaxonomySelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTaxonomySelect.Image = CType(resources.GetObject("btnTaxonomySelect.Image"), System.Drawing.Image)
        Me.btnTaxonomySelect.Location = New System.Drawing.Point(735, 262)
        Me.btnTaxonomySelect.Name = "btnTaxonomySelect"
        Me.btnTaxonomySelect.Size = New System.Drawing.Size(22, 22)
        Me.btnTaxonomySelect.TabIndex = 11
        Me.btnTaxonomySelect.UseVisualStyleBackColor = True
        '
        'Panel22
        '
        Me.Panel22.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel22.Controls.Add(Me.Label98)
        Me.Panel22.Controls.Add(Me.Label138)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel22.Location = New System.Drawing.Point(1, 1)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(789, 23)
        Me.Panel22.TabIndex = 132
        Me.Panel22.TabStop = True
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.Transparent
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Location = New System.Drawing.Point(0, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Padding = New System.Windows.Forms.Padding(3)
        Me.Label98.Size = New System.Drawing.Size(789, 22)
        Me.Label98.TabIndex = 131
        Me.Label98.Text = " Company Mailing Address"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label138.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Location = New System.Drawing.Point(0, 22)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(789, 1)
        Me.Label138.TabIndex = 130
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Location = New System.Drawing.Point(1, 286)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(789, 1)
        Me.Label139.TabIndex = 130
        '
        'mskMltCompanyFax
        '
        Me.mskMltCompanyFax.AllowValidate = True
        Me.mskMltCompanyFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMltCompanyFax.IncludeLiteralsAndPrompts = False
        Me.mskMltCompanyFax.Location = New System.Drawing.Point(366, 235)
        Me.mskMltCompanyFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.mskMltCompanyFax.Name = "mskMltCompanyFax"
        Me.mskMltCompanyFax.ReadOnly = False
        Me.mskMltCompanyFax.Size = New System.Drawing.Size(92, 25)
        Me.mskMltCompanyFax.TabIndex = 7
        '
        'Label140
        '
        Me.Label140.AutoSize = True
        Me.Label140.BackColor = System.Drawing.Color.Transparent
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Location = New System.Drawing.Point(465, 240)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(34, 14)
        Me.Label140.TabIndex = 117
        Me.Label140.Text = "NPI :"
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Location = New System.Drawing.Point(790, 1)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(1, 286)
        Me.Label141.TabIndex = 127
        '
        'txtMltCompanyNPI
        '
        Me.txtMltCompanyNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyNPI.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyNPI.Location = New System.Drawing.Point(499, 236)
        Me.txtMltCompanyNPI.MaxLength = 10
        Me.txtMltCompanyNPI.Name = "txtMltCompanyNPI"
        Me.txtMltCompanyNPI.Size = New System.Drawing.Size(112, 22)
        Me.txtMltCompanyNPI.TabIndex = 9
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label142.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Location = New System.Drawing.Point(0, 1)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1, 286)
        Me.Label142.TabIndex = 126
        '
        'pnlmltcompanymaillingAdd
        '
        Me.pnlmltcompanymaillingAdd.Location = New System.Drawing.Point(131, 102)
        Me.pnlmltcompanymaillingAdd.Name = "pnlmltcompanymaillingAdd"
        Me.pnlmltcompanymaillingAdd.Size = New System.Drawing.Size(325, 132)
        Me.pnlmltcompanymaillingAdd.TabIndex = 4
        Me.pnlmltcompanymaillingAdd.TabStop = True
        '
        'mskmltCompanyPhoneNo
        '
        Me.mskmltCompanyPhoneNo.AllowValidate = True
        Me.mskmltCompanyPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskmltCompanyPhoneNo.IncludeLiteralsAndPrompts = False
        Me.mskmltCompanyPhoneNo.Location = New System.Drawing.Point(211, 235)
        Me.mskmltCompanyPhoneNo.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskmltCompanyPhoneNo.Name = "mskmltCompanyPhoneNo"
        Me.mskmltCompanyPhoneNo.ReadOnly = False
        Me.mskmltCompanyPhoneNo.Size = New System.Drawing.Size(92, 25)
        Me.mskmltCompanyPhoneNo.TabIndex = 6
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Location = New System.Drawing.Point(0, 0)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(791, 1)
        Me.Label143.TabIndex = 124
        '
        'TxtMltCompanyName
        '
        Me.TxtMltCompanyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMltCompanyName.ForeColor = System.Drawing.Color.Black
        Me.TxtMltCompanyName.Location = New System.Drawing.Point(213, 52)
        Me.TxtMltCompanyName.MaxLength = 99
        Me.TxtMltCompanyName.Name = "TxtMltCompanyName"
        Me.TxtMltCompanyName.Size = New System.Drawing.Size(312, 22)
        Me.TxtMltCompanyName.TabIndex = 1
        '
        'Label144
        '
        Me.Label144.AutoSize = True
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Location = New System.Drawing.Point(146, 56)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(65, 14)
        Me.Label144.TabIndex = 121
        Me.Label144.Text = "Company :"
        '
        'TextBox14
        '
        Me.TextBox14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox14.ForeColor = System.Drawing.Color.Black
        Me.TextBox14.Location = New System.Drawing.Point(431, 155)
        Me.TextBox14.MaxLength = 99
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(16, 22)
        Me.TextBox14.TabIndex = 6
        Me.TextBox14.TabStop = False
        Me.TextBox14.Visible = False
        '
        'txtmltCompanyContact
        '
        Me.txtmltCompanyContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmltCompanyContact.ForeColor = System.Drawing.Color.Black
        Me.txtmltCompanyContact.Location = New System.Drawing.Point(213, 78)
        Me.txtmltCompanyContact.MaxLength = 99
        Me.txtmltCompanyContact.Name = "txtmltCompanyContact"
        Me.txtmltCompanyContact.Size = New System.Drawing.Size(312, 22)
        Me.txtmltCompanyContact.TabIndex = 3
        '
        'TextBox16
        '
        Me.TextBox16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox16.ForeColor = System.Drawing.Color.Black
        Me.TextBox16.Location = New System.Drawing.Point(361, 129)
        Me.TextBox16.MaxLength = 10
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New System.Drawing.Size(11, 22)
        Me.TextBox16.TabIndex = 7
        Me.TextBox16.TabStop = False
        Me.TextBox16.Visible = False
        '
        'Label145
        '
        Me.Label145.AutoSize = True
        Me.Label145.BackColor = System.Drawing.Color.Transparent
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Location = New System.Drawing.Point(154, 82)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(58, 14)
        Me.Label145.TabIndex = 119
        Me.Label145.Text = "Contact :"
        '
        'TextBox17
        '
        Me.TextBox17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox17.ForeColor = System.Drawing.Color.Black
        Me.TextBox17.Location = New System.Drawing.Point(306, 124)
        Me.TextBox17.MaxLength = 99
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New System.Drawing.Size(40, 22)
        Me.TextBox17.TabIndex = 5
        Me.TextBox17.TabStop = False
        Me.TextBox17.Visible = False
        '
        'Label146
        '
        Me.Label146.AutoSize = True
        Me.Label146.BackColor = System.Drawing.Color.Transparent
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label146.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Location = New System.Drawing.Point(619, 240)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(51, 14)
        Me.Label146.TabIndex = 116
        Me.Label146.Text = "Tax ID :"
        '
        'TextBox18
        '
        Me.TextBox18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox18.ForeColor = System.Drawing.Color.Black
        Me.TextBox18.Location = New System.Drawing.Point(401, 127)
        Me.TextBox18.MaxLength = 99
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Size = New System.Drawing.Size(43, 22)
        Me.TextBox18.TabIndex = 4
        Me.TextBox18.TabStop = False
        Me.TextBox18.Visible = False
        '
        'Label147
        '
        Me.Label147.AutoSize = True
        Me.Label147.BackColor = System.Drawing.Color.Transparent
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Location = New System.Drawing.Point(332, 240)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(33, 14)
        Me.Label147.TabIndex = 116
        Me.Label147.Text = "Fax :"
        '
        'Label154
        '
        Me.Label154.AutoSize = True
        Me.Label154.BackColor = System.Drawing.Color.Transparent
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Location = New System.Drawing.Point(268, 135)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(33, 14)
        Me.Label154.TabIndex = 94
        Me.Label154.Text = "ZIP :"
        Me.Label154.Visible = False
        '
        'Label155
        '
        Me.Label155.AutoSize = True
        Me.Label155.BackColor = System.Drawing.Color.Transparent
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label155.Location = New System.Drawing.Point(169, 266)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(42, 14)
        Me.Label155.TabIndex = 117
        Me.Label155.Text = "Email :"
        '
        'Label156
        '
        Me.Label156.AutoSize = True
        Me.Label156.BackColor = System.Drawing.Color.Transparent
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Location = New System.Drawing.Point(312, 129)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(45, 14)
        Me.Label156.TabIndex = 93
        Me.Label156.Text = "State :"
        Me.Label156.Visible = False
        '
        'Label157
        '
        Me.Label157.AutoSize = True
        Me.Label157.BackColor = System.Drawing.Color.Transparent
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label157.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label157.Location = New System.Drawing.Point(141, 239)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(69, 14)
        Me.Label157.TabIndex = 111
        Me.Label157.Text = "Phone No :"
        '
        'Label158
        '
        Me.Label158.AutoSize = True
        Me.Label158.BackColor = System.Drawing.Color.Transparent
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label158.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label158.Location = New System.Drawing.Point(278, 113)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(35, 14)
        Me.Label158.TabIndex = 92
        Me.Label158.Text = "City :"
        Me.Label158.Visible = False
        '
        'txtMltCompanyEmail
        '
        Me.txtMltCompanyEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyEmail.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyEmail.Location = New System.Drawing.Point(211, 262)
        Me.txtMltCompanyEmail.MaxLength = 99
        Me.txtMltCompanyEmail.Name = "txtMltCompanyEmail"
        Me.txtMltCompanyEmail.Size = New System.Drawing.Size(183, 22)
        Me.txtMltCompanyEmail.TabIndex = 8
        '
        'Label159
        '
        Me.Label159.AutoSize = True
        Me.Label159.BackColor = System.Drawing.Color.Transparent
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label159.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label159.Location = New System.Drawing.Point(364, 132)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(65, 14)
        Me.Label159.TabIndex = 91
        Me.Label159.Text = "Address2 :"
        Me.Label159.Visible = False
        '
        'txtMltCompanyTaxId
        '
        Me.txtMltCompanyTaxId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMltCompanyTaxId.ForeColor = System.Drawing.Color.Black
        Me.txtMltCompanyTaxId.Location = New System.Drawing.Point(670, 236)
        Me.txtMltCompanyTaxId.MaxLength = 9
        Me.txtMltCompanyTaxId.Name = "txtMltCompanyTaxId"
        Me.txtMltCompanyTaxId.Size = New System.Drawing.Size(112, 22)
        Me.txtMltCompanyTaxId.TabIndex = 10
        '
        'Label160
        '
        Me.Label160.AutoSize = True
        Me.Label160.BackColor = System.Drawing.Color.Transparent
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label160.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label160.Location = New System.Drawing.Point(312, 110)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(65, 14)
        Me.Label160.TabIndex = 90
        Me.Label160.Text = "Address1 :"
        Me.Label160.Visible = False
        '
        'chkmltSameAsPrvdrAdd
        '
        Me.chkmltSameAsPrvdrAdd.AutoSize = True
        Me.chkmltSameAsPrvdrAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkmltSameAsPrvdrAdd.Location = New System.Drawing.Point(535, 54)
        Me.chkmltSameAsPrvdrAdd.Name = "chkmltSameAsPrvdrAdd"
        Me.chkmltSameAsPrvdrAdd.Size = New System.Drawing.Size(166, 18)
        Me.chkmltSameAsPrvdrAdd.TabIndex = 2
        Me.chkmltSameAsPrvdrAdd.Text = "Same as Provider Address"
        Me.chkmltSameAsPrvdrAdd.UseVisualStyleBackColor = True
        '
        'TextBox21
        '
        Me.TextBox21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox21.ForeColor = System.Drawing.Color.Black
        Me.TextBox21.Location = New System.Drawing.Point(373, 104)
        Me.TextBox21.MaxLength = 99
        Me.TextBox21.Name = "TextBox21"
        Me.TextBox21.Size = New System.Drawing.Size(78, 22)
        Me.TextBox21.TabIndex = 2
        Me.TextBox21.TabStop = False
        Me.TextBox21.Visible = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'lblLicenseMessage
        '
        Me.lblLicenseMessage.AutoSize = True
        Me.lblLicenseMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseMessage.ForeColor = System.Drawing.Color.Green
        Me.lblLicenseMessage.Location = New System.Drawing.Point(149, 21)
        Me.lblLicenseMessage.Name = "lblLicenseMessage"
        Me.lblLicenseMessage.Size = New System.Drawing.Size(501, 14)
        Me.lblLicenseMessage.TabIndex = 198
        Me.lblLicenseMessage.Text = "License key is generated by TRIARQ Health, please save to apply the license key."
        Me.lblLicenseMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLicenseMessage.Visible = False
        '
        'frmDoctor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(799, 699)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoctor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Provider Information"
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.c1ProviderIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSPI.ResumeLayout(False)
        Me.pnlSPI.PerformLayout()
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tbpgProvider.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.grpSPI.ResumeLayout(False)
        Me.grpSPI.PerformLayout()
        Me.pnlLicenseKey.ResumeLayout(False)
        Me.pnlLicenseKey.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tbpgBillingID.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.gbProviderIdentification.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.tbpgProviderCompany.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        CType(Me.c1CompanyProvIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel24.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.tbpgStatement.ResumeLayout(False)
        Me.tbpgPrvdrMultCmpny.ResumeLayout(False)
        Me.tbpgPrvdrMultCmpny.PerformLayout()
        Me.Panel28.ResumeLayout(False)
        Me.Panel28.PerformLayout()
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.Panel26.ResumeLayout(False)
        CType(Me.c1MltCompantAddtnlID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel27.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property SPICode() As String
        Get
            Return SPI
        End Get
        Set(ByVal Value As String)
            SPI = Value
        End Set
    End Property
    Public Property ActiveStartTime() As DateTime
        Get
            Return dtActiveStartTime
        End Get
        Set(ByVal Value As DateTime)
            dtActiveStartTime = Value
        End Set
    End Property
    Public Property ActiveEndTime() As DateTime
        Get
            Return dtActiveEndTime
        End Get
        Set(ByVal Value As DateTime)
            dtActiveEndTime = Value
        End Set
    End Property

    Public Property ValidationFailed() As Boolean
        Get
            Return bValidationFailed
        End Get
        Set(ByVal Value As Boolean)
            bValidationFailed = Value
        End Set
    End Property

    '------------------------------------------------------------------

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Try
            TempProvider = Nothing
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDoctor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim scheme As Cls_TabIndexSettings.TabScheme = Cls_TabIndexSettings.TabScheme.AcrossFirst
        'Dim TabMe As Cls_TabIndexSettings = New Cls_TabIndexSettings(Me)
        'TabMe.SetTabOrder(scheme)

        gloC1FlexStyle.Style(c1ProviderIdentification)
        gloC1FlexStyle.Style(c1CompanyProvIdentification)
        gloC1FlexStyle.Style(c1MltCompantAddtnlID)
        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        Try


            FillProviderTypes()

            DesignGrid()
            DesignCompanyAdditionalGrid()

            blnLogoChanged = False
            txtPrefix.Select()
            oPracticeAddressContol.txtZip.Size = New Size(43, 22)
            oPracticeAddressContol.txtAreaCode.Visible = True
            oPracticeAddressContol.txtArea.Visible = True
            oBussinessAddressContol.txtAreaCode.Visible = True
            oBussinessAddressContol.txtZip.Size = New Size(43, 22)
            oBussinessAddressContol.txtArea.Visible = True

            oCompanyAddressContol.txtAreaCode.Visible = True
            oCompanyAddressContol.txtZip.Size = New Size(43, 22)
            oCompanyAddressContol.txtArea.Visible = True

            oProviderPhysicalAddressContol.txtAreaCode.Visible = True
            oProviderPhysicalAddressContol.txtZip.Size = New Size(43, 22)
            oProviderPhysicalAddressContol.txtArea.Visible = True

            oProviderCompanyPhysicalAddressContol.txtAreaCode.Visible = True
            oProviderCompanyPhysicalAddressContol.txtZip.Size = New Size(43, 22)
            oProviderCompanyPhysicalAddressContol.txtArea.Visible = True

            'Hide Provider Multipal Company Tab'
            Me.TabControl1.TabPages.Remove(tbpgPrvdrMultCmpny)
            ''Check if no. of provider companies more than one'

            _nNoOfProviderCompanies = oProvider.GetNoProviderCompnies()
            If _nNoOfProviderCompanies > 1 Then
                DesignProviderCompanyTab(_nNoOfProviderCompanies)
            End If


            '' Removed Signature Pad Control 20090603
            'If AxSigPlus1.TabletConnectQuery() Then
            '    AxSigPlus1.DisplayPenWidth = 11
            '    'AxSigPlus1.ImageXSize = 1000 'sets image width in pixels
            '    'AxSigPlus1.ImageYSize = 350
            '    'AxSigPlus1.JustifyMode = 5
            '    AxSigPlus1.ClearTablet()
            '    AxSigPlus1.Refresh()
            '    AxSigPlus1.TabletState = 1
            'End If
            ''

            '******By Sandip Deshmukh 18 Oct 07 2.13PM Bug #328
            '******for making nickname field invisible on the doctor information Form
            'label22.Visible = False
            'txtNickName.Visible = False
            '******18 Oct 07 2.13PM Bug #328
            If _nProviderID > 0 Then
                dtpActiveStartTime.Value = DateTime.Now
                dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
                dtpActiveStartTime.Enabled = True
                dtpActiveEndTime.Enabled = True
            Else
                dtpActiveStartTime.Enabled = False
            End If
            If gblnIsSurescriptEnabled = False Then
                grpSPI.Enabled = False
            End If

            If _nProviderID > 0 Then
                LoadProvider(_nProviderID)
            ElseIf _nCloneFromProviderID > 0 AndAlso _bIsCloneProvider = True Then
                LoadProviderForClone(_nCloneFromProviderID)
                lblAusStatus.Text = 0
            Else
                lblAusStatus.Text = 0
            End If


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Unable to Load the Doctor Form due to " & objErr.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Retrieve, True)
        Finally
            If oProvider IsNot Nothing Then
                oProvider = Nothing
            End If
        End Try
    End Sub

    Private Function AddUpdatePrescriber(ByVal objProvider As clsProvider) As String
        Dim strSurescriptMsg As New System.Text.StringBuilder

        Try

            Return "" 'objPrescriber.strMessage & strSurescriptMsg.ToString

        Catch ex As Exception
        Finally
            'objPrescriber = Nothing
        End Try
    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            txtImagePath.Text = ""
            If optBrowse.Checked = True Then
                picSignature.Image = Nothing
            Else
                '' Removed Signature Pad Control 20090603
                'btnCapture.Enabled = True
                'If AxSigPlus1.TabletConnectQuery() Then
                '    AxSigPlus1.ClearTablet()
                '    AxSigPlus1.Refresh()
                '    AxSigPlus1.TabletState = 1
                '    AxSigPlus1.Visible = True
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtImagePath_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = True
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            'picSignature.SizeMode = PictureBoxSizeMode.StretchImage
            With dlgOpenFile
                .Title = "Select Signature"
                .Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With
            If dlgOpenFile.ShowDialog = DialogResult.OK Then

                picSignature.Visible = True
                '' Removed Signature Pad Control 20090603
                'AxSigPlus1.Visible = False
                picSignature.Image = Image.FromFile(dlgOpenFile.FileName)
                ImgWidth = Image.FromFile(dlgOpenFile.FileName).Width
                blnLogoChanged = True
                frmDoctor.Imagepath = dlgOpenFile.FileName

                txtImagePath.Text = dlgOpenFile.FileName
                btnCapture.Enabled = False

                '****************************
                Dim img As Image
                Dim nWidth As Int16
                Dim nHeight As Int16
                img = picSignature.Image
                nHeight = img.Height
                nWidth = img.Width
                'If nWidth > 150 Then
                '    nWidth = 150
                'End If
                'If nHeight > 75 Then
                '    nHeight = 75
                'End If
                img = New Bitmap(img, New Size(nWidth, nHeight))
                picSignature.Image = img
                picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                '****************************
            End If
        Catch objErr As Exception
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnCapture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            blnLogoChanged = True
            'If File.Exists(Application.StartupPath & "\DoctorSign.tif") Then
            '    File.Delete(Application.StartupPath & "\DoctorSign.tif")
            'End If

            '' Removed Signature Pad Control 20090603
            'If AxSigPlus1.TabletConnectQuery() Then
            '    If AxSigPlus1.GetNumberOfStrokes() > 0 Then
            '        AxSigPlus1.TabletState = 0 'allows JustifyMode to be set
            '        'AxSigPlus1.ImageXSize = 500 'sets image width in pixels
            '        AxSigPlus1.ImageXSize = 150 'sets image width in pixels
            '        'AxSigPlus1.ImageYSize = 200 'sets image height in pixels
            '        AxSigPlus1.ImageYSize = 75 'sets image height in pixels
            '        AxSigPlus1.ImagePenWidth = 11 'sets width of pen stroke in pixels
            '        AxSigPlus1.JustifyMode = 0 '+expands signature to fit all of sig window
            '        AxSigPlus1.WriteImageFile(Application.StartupPath & "\DoctorSign.tif")
            '        'picSignature.SizeMode = PictureBoxSizeMode.StretchImage

            '        AxSigPlus1.Refresh()
            '        AxSigPlus1.TabletState = 0
            '        btnCapture.Enabled = False

            '        'Me.Close()
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Uanble to capture the Doctor Signature due to " & ex.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
        End Try
    End Sub

    Private Sub optBrowse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If optBrowse.Checked = True Then
                'grpBrowse.Enabled = True
                '' Removed Signature Pad Control 20090603
                'AxSigPlus1.Visible = False
                picSignature.Visible = True
                btnCapture.Enabled = False
            Else
                'grpBrowse.Enabled = False
                '' Removed Signature Pad Control 20090603
                ' AxSigPlus1.Visible = True
                picSignature.Visible = False
                btnCapture.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmDoctor_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        '' Removed Signature Pad Control 20090603
        'AxSigPlus1.Dispose()
    End Sub

    Public Sub Fill_DcotorTypes()
        Try
            With cmbDoctorType
                .BeginUpdate()
                .Items.Clear()
                Dim clProviderTypes As New Collection
                Dim objProviderType As New clsProviderType(gstrConnectionString)
                clProviderTypes = objProviderType.Fill_ProviderTypes()
                objProviderType = Nothing
                Dim nCount As Int16
                For nCount = 1 To clProviderTypes.Count
                    .Items.Add(clProviderTypes.Item(nCount))
                Next
                If .Items.Count >= 1 Then .SelectedIndex = 0
                .EndUpdate()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Fill Doctor Types", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtZIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '******By Sandip Deshmukh 20 Oct 2007 10.58 a.m. 
        '******For the control for Zip No. is 
        '******modified from Textbox to 10 digit numeric and following code is added

        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
        '******20 Oct 2007 10.58 a.m. 

    End Sub

    Private Sub rbPrescriber_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPrescriber.CheckedChanged
        If rbPrescriber.Checked Then
            pnlSPI.Visible = False
            dtpActiveStartTime.Value = DateTime.Now
            dtpActiveStartTime.Enabled = True
            dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
            dtpActiveEndTime.Enabled = True
        End If
    End Sub

    Private Sub rbPrescriberLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPrescriberLocation.CheckedChanged
        If rbPrescriberLocation.Checked Then
            pnlSPI.Visible = False
            txtSPI.Clear()
            txtSPI.Visible = False
            txtSPI.Enabled = True
            lblRoot.Visible = True
            lblSPI.Text = SPI
        End If
    End Sub

    Private Sub rbUpdate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbUpdate.CheckedChanged
        If rbUpdate.Checked Then
            pnlSPI.Visible = False
            txtSPI.Visible = False
            lblRoot.Visible = False
            lblSPI.Text = SPI
        End If
    End Sub

    Private Sub chckDisable_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckDisable.CheckedChanged
        If chckDisable.Checked Then
            chckNew.Checked = False
            chckRefill.Checked = False
            chckNew.Enabled = False
            chckRefill.Enabled = False
            dtpActiveEndTime.Value = DateTime.Now
            dtpActiveEndTime.Enabled = False

        Else
            chckNew.Enabled = True
            chckRefill.Enabled = True
            dtpActiveEndTime.Enabled = True
        End If
    End Sub

    Private Sub txtSPI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub
    Private Function IsValidDateCheck(dateTextBox As MaskedTextBox) As Boolean
        Dim DateContents As String = dateTextBox.Text.Trim()
        If Not String.IsNullOrEmpty(DateContents) AndAlso DateContents <> "" Then
            Dim dateSoFar As String() = dateTextBox.Text.Split("/"c)
            Dim month As String = dateSoFar(0).Trim()
            Dim day As String = dateSoFar(1).Trim()
            Dim year As String = dateSoFar(2).Trim()
            If month.Length = 2 AndAlso day.Length = 2 AndAlso year.Length = 4 AndAlso (year.StartsWith("19") OrElse year.StartsWith("20")) Then
                Dim d As DateTime
                If Not DateTime.TryParse(dateTextBox.Text, d) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click

        Try

            'Mandatory fields are prefix, first name and last name
            'If Trim(txtPrefix.Text) = "" Then
            '    MessageBox.Show("Please enter Prefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtPrefix.Focus()
            '    Exit Sub
            'End If
            Dim IsLicenseused As Boolean = False

            If Trim(txtFirstName.Text) = "" Then
                MessageBox.Show("Please enter First Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFirstName.Focus()
                Exit Sub
            End If


            If Trim(txtLastName.Text) = "" Then
                MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtLastName.Focus()
                Exit Sub
            End If

            '******By Sandip Deshmukh 20 Oct 2007 11.15 a.m. 
            '******For bug reported the control for Phone No. is 
            '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
            Me.Cursor = Cursors.Default
            'If Len(Trim(txtPhoneNo.Text)) > 0 And Len(Trim(txtPhoneNo.Text)) < 10 Then
            '    MessageBox.Show("Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtPhoneNo.Focus()
            '    Exit Sub
            'End If
            If mskBMPager.Text <> "" Then
                If (mskBMPager.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskBMPhoneNo.Text <> "" Then
                If (mskBMPhoneNo.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskBMFax.Text <> "" Then
                If (mskBMFax.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default
            'If Len(Trim(txtMobileNo.Text)) > 0 And Len(Trim(txtMobileNo.Text)) < 10 Then
            '    MessageBox.Show("Mobile No. Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtMobileNo.Focus()
            '    Exit Sub
            'End If

            If mskMobileNo.Text <> "" Then
                If (mskMobileNo.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            '******20 Oct 2007 11.15 a.m. 


            '******By Ravikiran 21 feb 2008 11.15 a.m. 
            '******To Satisfy Surescript requiremnts

            'If Trim(txtLastName.Text) = "" Then
            '    MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtLastName.Focus()
            '    Exit Sub
            'End If

            'If Trim(txtbmAddress.Text) = "" Then
            '    'MessageBox.Show("Please enter Address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtAddress.Focus()
            '    'Exit Sub
            'End If

            'If Trim(txtBMCity.Text) = "" Then
            '    'MessageBox.Show("Please enter City", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtCity.Focus()
            '    'Exit Sub
            'End If

            'If Trim(txtState.Text) = "" Then
            '    'MessageBox.Show("Please enter State", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtState.Focus()
            '    'Exit Sub
            'End If

            'If Trim(txtZIP.Text) = "" Then
            '    'MessageBox.Show("Please enter ZIP code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtZIP.Focus()
            '    'Exit Sub
            'End If


            'If Trim(txtPhoneNo.Text) = "" Then
            '    'MessageBox.Show("Please enter Phone No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtPhoneNo.Focus()
            '    'Exit Sub
            'End If

            'If Trim(txtFAX.Text) = "" Then
            '    'MessageBox.Show("Please enter Fax", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'txtFAX.Focus()
            '    'Exit Sub
            'End If

            'If gblnIsSurescriptEnabled = True Then
            '    If Trim(txtDEA.Text) = "" Then
            '        MessageBox.Show("Please enter DEA Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        txtDEA.Focus()
            '        Exit Sub
            '    End If
            'End If

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            Me.Cursor = Cursors.Default
            If (txtBMEmailAddress.Text.Trim() <> "") Then
                If CheckEmailAddress(txtBMEmailAddress.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBMEmailAddress.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 08-09-2010
            ' For URL validation 
            Me.Cursor = Cursors.Default
            If (txtBMURL.Text.Trim() <> "") Then
                If CheckURLAddress(txtBMURL.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBMURL.Focus()
                    Exit Sub
                End If
            End If
            'End of URL Validation
            'Sanjog - Added on 2011 May 23 for DPS No.
            Me.Cursor = Cursors.Default
            If Len(Trim(txtDPSNumber.Text)) > 0 And Len(Trim(txtDPSNumber.Text)) < 9 Then
                MessageBox.Show("DPS Number Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDPSNumber.Focus()
                Exit Sub
            End If
            'Sanjog - Added on 2011 May 23 for DPS No.
            If mskBPracPager.Text <> "" Then
                If (mskBPracPager.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If maskedBpracPhno.Text <> "" Then
                If (maskedBpracPhno.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskBPracFax.Text <> "" Then
                If (mskBPracFax.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default


            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            Me.Cursor = Cursors.Default
            If (txtBPracEMail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtBPracEMail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBPracEMail.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 08-09-2010
            ' For URL validation 
            Me.Cursor = Cursors.Default
            If (txtBPracUrl.Text.Trim() <> "") Then
                If CheckURLAddress(txtBPracUrl.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBPracUrl.Focus()
                    Exit Sub
                End If
            End If
            'End of URL Validation


            ' For UPIN proper format i. one alphabhet and 4 or 5 character
            Me.Cursor = Cursors.Default
            If (mskTxtUPIN.Text.Trim() <> "") Then
                If Regex.IsMatch(mskTxtUPIN.Text.Trim(), "^[a-zA-Z]{1}[0-9]{4,5}$") = False Then
                    MessageBox.Show("Please enter a valid UPIN.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskTxtUPIN.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default
            If Len(Trim(mskTxtDEA.Text)) > 0 And Len(Trim(mskTxtDEA.Text)) < 9 Then
                MessageBox.Show("DEA Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                mskTxtDEA.Focus()
                Exit Sub
            End If

            Me.Cursor = Cursors.Default
            If (mskTxtDEA.Text.Trim() <> "") Then
                If Regex.IsMatch(mskTxtDEA.Text.Trim(), "^[a-zA-Z]{2}[0-9]{7}$") = False Then
                    MessageBox.Show("Please enter a valid DEA details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mskTxtDEA.Focus()
                    Exit Sub
                End If
            End If

            If (oBussinessAddressContol.txtAreaCode.TextLength > 0 And oBussinessAddressContol.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    oBussinessAddressContol.txtAreaCode.Select()
                    oBussinessAddressContol.txtAreaCode.Focus()
                    Exit Sub
                End If

            ElseIf (oPracticeAddressContol.txtAreaCode.TextLength > 0 And oPracticeAddressContol.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    oPracticeAddressContol.txtAreaCode.Select()
                    oPracticeAddressContol.txtAreaCode.Focus()
                    Exit Sub
                End If


            ElseIf (oCompanyAddressContol.txtAreaCode.TextLength > 0 And oCompanyAddressContol.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    oCompanyAddressContol.txtAreaCode.Select()
                    oCompanyAddressContol.txtAreaCode.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default


            'START Provider Physical Address Related Validation
            If (oProviderPhysicalAddressContol.txtAreaCode.TextLength > 0 And oProviderPhysicalAddressContol.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    oProviderPhysicalAddressContol.txtAreaCode.Select()
                    oProviderPhysicalAddressContol.txtAreaCode.Focus()
                    Exit Sub
                End If
            End If

            If mskBIDPLPager.Text <> "" Then
                If (mskBIDPLPager.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If maskedBIDPLPhno.Text <> "" Then
                If (maskedBIDPLPhno.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskBIDPLFax.Text <> "" Then
                If (mskBIDPLFax.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default



            If (txtBIDPLEMail.Text.Trim() <> "") Then

                If CheckEmailAddress(txtBIDPLEMail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBIDPLEMail.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 08-09-2010
            ' For URL validation 
            Me.Cursor = Cursors.Default
            If (txtBIDPLUrl.Text.Trim() <> "") Then
                If CheckURLAddress(txtBIDPLUrl.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBIDPLUrl.Focus()
                    Exit Sub
                End If
            End If

            If mtxtDOB.MaskCompleted = True Then
                Try
                    If Not IsValidDateCheck(mtxtDOB) Then
                        MessageBox.Show("Enter a valid date of birth.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TabControl1.SelectedTab = tbpgProvider
                        mtxtDOB.Focus()
                        Exit Sub
                    End If
                Catch ex As Exception
                    MessageBox.Show("Enter a valid date of birth.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TabControl1.SelectedTab = tbpgProvider
                    mtxtDOB.Focus()
                    Exit Sub
                End Try
            End If

            'END Provider Physical Address Related Validation


            'START Company Related Validation
            If txtCompanyPhone.Text <> "" Then
                If (txtCompanyPhone.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskCompanyFax.Text <> "" Then
                If (mskCompanyFax.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            Me.Cursor = Cursors.Default
            If (txtCompanyEmail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtCompanyEmail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCompanyEmail.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 09-09-2010
            'END Company Related Validation

            'START Provider Company Physical Address Related Validations 
            If (oProviderCompanyPhysicalAddressContol.txtAreaCode.TextLength > 0 And oProviderCompanyPhysicalAddressContol.txtAreaCode.TextLength < 4) Then
                If (MessageBox.Show("Area code information is incomplete. Do you want to continue with this information?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No) Then
                    oProviderCompanyPhysicalAddressContol.txtAreaCode.Select()
                    oProviderCompanyPhysicalAddressContol.txtAreaCode.Focus()
                    Exit Sub
                End If
            End If

            If mskPLPager.Text <> "" Then
                If (mskPLPager.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If maskedPLPhno.Text <> "" Then
                If (maskedPLPhno.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            If mskPLFax.Text <> "" Then
                If (mskPLFax.IsValidated = False) Then
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default

            Me.Cursor = Cursors.Default
            If (txtPLEMail.Text.Trim() <> "") Then

                If CheckEmailAddress(txtPLEMail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPLEMail.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 08-09-2010
            ' For URL validation 
            Me.Cursor = Cursors.Default
            If (txtPLUrl.Text.Trim() <> "") Then
                If CheckURLAddress(txtPLUrl.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPLUrl.Focus()
                    Exit Sub
                End If
            End If
            'END Provider Company Physical Address Related Validations 


            '''''Provider Other Company Validations'''''
            '''Validation for Provider Multiple Companies 
            ''' 

            If (txtMltCompanyEmail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtMltCompanyEmail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMltCompanyEmail.Focus()
                    Exit Sub
                End If
            End If



            If (txtMltCompanyPhysMail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtMltCompanyPhysMail.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMltCompanyPhysMail.Focus()
                    Exit Sub
                End If
            End If




            If (txtMltCompanyPhysURL.Text.Trim() <> "") Then
                If CheckURLAddress(txtMltCompanyPhysURL.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMltCompanyPhysURL.Focus()
                    Exit Sub
                End If
            End If


            '''''End Provider Other Company Validation


            'sarika For removing Provider validations
            'commented bcoz this validations should be done in the class while validating data for surescript
            'If gblnIsSurescriptEnabled Then
            '    If chckDisable.Checked = False And chckNew.Checked = False And chckRefill.Checked = False Then
            '        MessageBox.Show("Service Level Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        chckDisable.Focus()
            '        Exit Sub
            '    End If
            '    If rbPrescriberLocation.Checked Then
            '        If txtSPI.Text.Trim = "" Then
            '            MessageBox.Show("Please enter SPI Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            txtSPI.Focus()
            '            Exit Sub
            '        End If
            '    End If
            '    If DateTime.Compare(dtpActiveEndTime.Value, dtpActiveStartTime.Value) <= 0 Then
            '        MessageBox.Show("Active End Date should be always greater than Active Start Time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        dtpActiveEndTime.Focus()
            '        Exit Sub
            '    End If
            '    If chckDisable.Checked = False Then
            '        If DateTime.Compare(dtpActiveEndTime.Value, DateTime.Now) <= 0 Then
            '            MessageBox.Show("Active End Date should be always greater than Today", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            dtpActiveEndTime.Focus()
            '            Exit Sub
            '        End If
            '    End If
            'End If
            '------------


            If _nProviderID = 0 Then
                'Check User Name must be entered
                If Trim(txtUserName.Text) = "" Then
                    MessageBox.Show("Please enter User Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtUserName.Focus()
                    Exit Sub
                End If
                'Check Password must be entered
                If Trim(txtPassword.Text) = "" Then
                    MessageBox.Show("Please enter Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPassword.Focus()
                    Exit Sub
                End If
                'Check Confirm Password must be entered
                If Trim(txtConfirmPassword.Text) = "" Then
                    MessageBox.Show("Please enter Confirm Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtConfirmPassword.Focus()
                    Exit Sub
                End If
                'Check Password and Confirm Password must be same
                If Trim(txtConfirmPassword.Text) <> Trim(txtPassword.Text) Then
                    MessageBox.Show("Password and Confirm Password must be same", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPassword.Focus()
                    Exit Sub
                End If
                '******By Sandip Deshmukh 13 Oct 2007  12.30 PM  Bug# 328
                '******The Code is commented for the following 
                '******Dont keep Nick Name as Mandetory , Remove validation and make it invisible
                'Check Nick Name must be entered
                'If Trim(txtNickName.Text) = "" Then
                '    MessageBox.Show("Please enter Nick Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    txtNickName.Focus()
                '    Exit Sub
                'End If
                '******13 Oct 2007  12.30 PM Bug# 328

                'Check User Name already exists or not
                Dim objUser As New clsUsers(gstrConnectionString)
                If blnNewModify = False Then
                    If Trim(txtUserName.Text) <> "" Then


                        If objUser.CheckUserExists(txtUserName.Text) = True Then
                            Me.Cursor = Cursors.Default
                            MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            objUser = Nothing
                            txtUserName.Focus()
                            Exit Sub
                        End If
                        objUser = Nothing

                    End If
                End If

            End If

            If txtNPI.Text.Trim = "" Then
                MessageBox.Show("Please enter NPI number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNPI.Focus()
                Exit Sub
            Else
                If txtNPI.Text.Length < 10 Or txtNPI.Text.Length > 10 Then
                    MessageBox.Show("Please enter NPI number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNPI.Focus()
                    Exit Sub
                End If
                If Val(txtNPI.Text.Trim) = 0 Then
                    MessageBox.Show("Please enter valid numeric NPI number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtNPI.Focus()
                    Exit Sub
                End If
            End If

            SetDemoLicenseMode(txtNPI.Text.Trim)

            Dim oProvider As New clsProvider(gstrConnectionString)

            'Check Provider already exists or not
            If _nProviderID = 0 Then
                If oProvider.CheckProviderExists(txtFirstName.Text + Space(2) + txtMiddleName.Text + Space(2) + txtLastName.Text, txtFirstName.Tag) = True Then
                    MessageBox.Show("Provider already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    oProvider = Nothing
                    txtFirstName.Focus()
                    Exit Sub
                End If
                ' check license is already in use ?
                If ISDEMOLicensce = False Then
                    If txtLicenseKey.Text.Trim <> "" Then
                        Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                            IsLicenseused = oLicense.CheckLicenseIsInUse(txtLicenseKey.Text.Trim, 0)
                        End Using

                        ' If oProvider.CheckLicenseIsInUse(txtLicenseKey.Text.Trim) = True Then
                        If IsLicenseused = True Then
                            MessageBox.Show("License Key already in use.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            oProvider = Nothing
                            txtLicenseKey.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            Else
                If blnNewModify = False Then


                    If oProvider.CheckProviderExists(txtFirstName.Text + Space(2) + txtMiddleName.Text + Space(2) + txtLastName.Text, _nProviderID) = True Then
                        MessageBox.Show("Provider already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        oProvider = Nothing
                        txtFirstName.Focus()
                        Exit Sub
                    End If

                    If ISDEMOLicensce = False And lblAusStatus.Text.Trim <> "4" Then
                        If txtLicenseKey.Text.Trim <> "" Then
                            Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                IsLicenseused = oLicense.CheckLicenseIsInUse(txtLicenseKey.Text.Trim, _nProviderID)
                            End Using

                            'If oProvider.CheckLicenseIsInUse(txtLicenseKey.Text.Trim, _nProviderID) = True Then
                            If IsLicenseused = True Then
                                MessageBox.Show("License Key already in use.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                oProvider = Nothing
                                txtLicenseKey.Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            '''''''''''''''''''''''''''
            '''''''''''''''''''''''''''
            If btnLicenseRefresh.Tag <> "valid" Then
                If ISDEMOLicensce = False And lblAusStatus.Text.Trim <> "4" Then
                    If Trim(txtLicenseKey.Text) <> "" Then
                        If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                            Me.Cursor = Cursors.WaitCursor
                            Dim ausmessage As String

                            Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                ausmessage = oLicense.ValidateLicenseKey(txtLicenseKey.Text.Trim, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, gstrClinicExternalCode, _nProviderID, _nAUSPortalID)
                            End Using

                            Me.Cursor = Cursors.Default
                            If ausmessage <> "" Then
                                If ausmessage <> "ok" Then
                                    MessageBox.Show(ausmessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    TabControl1.SelectedTab = tbpgProvider
                                    txtLicenseKey.Focus()
                                    Exit Sub
                                Else

                                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                        _nAUSPortalID = oLicense.GetAUSPortalID(txtLicenseKey.Text.Trim)
                                    End Using
                                End If
                            Else
                                MessageBox.Show("Error while validating license key.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtLicenseKey.Text = ""
                            End If
                        End If
                    Else
                        If _nProviderID = 0 Then
                            If MessageBox.Show("Provider info will be sent to TRIARQ Health and license key will be generated to activate the provider, are you sure ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                TabControl1.SelectedTab = tbpgProvider
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If



            oProvider.ProviderID = _nProviderID
            oProvider.Prefix = txtPrefix.Text.Trim()
            oProvider.FirstName = txtFirstName.Text.Trim()
            oProvider.MiddleName = txtMiddleName.Text.Trim()
            oProvider.LastName = txtLastName.Text.Trim()
            oProvider.Suffix = txtSuffix.Text.Trim()
            oProvider.BMContactName = txtBMContact.Text.Trim()
            'oProvider.BMAddress1 = txtBMAddress1.Text.Trim()
            'oProvider.BMAddress2 = txtBMAddress2.Text.Trim()
            'oProvider.BMState = txtBMstate.Text.Trim()
            'oProvider.BMCity = txtBMCity.Text.Trim()
            'oProvider.BMZIP = txtBMZip.Text.Trim()

            oProvider.BMAddress1 = oBussinessAddressContol.txtAddress1.Text     ''add1
            oProvider.BMAddress2 = oBussinessAddressContol.txtAddress2.Text     ''add2
            oProvider.BMState = oBussinessAddressContol.cmbState.Text           ''state
            oProvider.BMCity = oBussinessAddressContol.txtCity.Text             ''city
            oProvider.BMZIP = oBussinessAddressContol.txtZip.Text               ''zip
            oProvider.BMAreaCode = oBussinessAddressContol.txtAreaCode.Text     ''area code
            oProvider.BMCountry = oBussinessAddressContol.cmbCountry.Text       ''Country
            oProvider.BMCounty = oBussinessAddressContol.txtCounty.Text        ''County
            ''End------Dhruv


            oProvider.BPracContactName = txtBPracContactName.Text.Trim()
            'oProvider.BPracAddress1 = txtBPracAddress1.Text.Trim()
            'oProvider.BPracAddress2 = txtBPracAddress2.Text.Trim()
            'oProvider.BPracState = txtBPracState.Text.Trim()
            'oProvider.BPracCity = txtBPracCity.Text.Trim()
            'oProvider.BPracZIP = txtBPracZIP.Text.Trim()
            oProvider.BPracAddress1 = oPracticeAddressContol.txtAddress1.Text     ''add1
            oProvider.BPracAddress2 = oPracticeAddressContol.txtAddress2.Text     ''add2
            oProvider.BPracState = oPracticeAddressContol.cmbState.Text           ''state
            oProvider.BPracCity = oPracticeAddressContol.txtCity.Text             ''city
            oProvider.BPracZIP = oPracticeAddressContol.txtZip.Text               ''zip
            oProvider.BPracAreaCode = oPracticeAddressContol.txtAreaCode.Text     ''area code
            oProvider.BPracCountry = oPracticeAddressContol.cmbCountry.Text       ''Country
            oProvider.BPracCounty = oPracticeAddressContol.txtCounty.Text        ''County


            oProvider.BMPhone = mskBMPhoneNo.Text.Trim()
            oProvider.BMPager = mskBMPager.Text.Trim()
            oProvider.BMFAX = mskBMFax.Text.Trim()
            oProvider.BMEmail = txtBMEmailAddress.Text.Trim()
            If mtxtDOB.MaskCompleted Then
                oProvider.dtDOB = Convert.ToDateTime(mtxtDOB.Text)
            End If
            oProvider.DirectAddress = lblDirectAddressValue.Text.Trim()

            oProvider.BMURL = txtBMURL.Text.Trim()
            oProvider.BPracPhone = maskedBpracPhno.Text.Trim()
            oProvider.BPracPager = mskBPracPager.Text.Trim()
            oProvider.BPracFAX = mskBPracFax.Text.Trim()
            oProvider.BPracEmail = txtBPracEMail.Text.Trim()
            oProvider.BPracURL = txtBPracUrl.Text.Trim()

            oProvider.ComapanyName = txtCompanyName.Text
            oProvider.ComapanyContactName = txtCompanyContactName.Text
            'oProvider.ComapanyAddress1 = txtCompanyAddress1.Text
            'oProvider.ComapanyAddress2 = txtCompanyAddress2.Text
            'oProvider.ComapanyCity = txtCompanyCity.Text
            'oProvider.ComapanyState = txtCompanyState.Text
            'oProvider.ComapanyZip = txtCompanyZip.Text

            oProvider.ComapanyAddress1 = oCompanyAddressContol.txtAddress1.Text     ''add1
            oProvider.ComapanyAddress2 = oCompanyAddressContol.txtAddress2.Text     ''add2
            oProvider.ComapanyCity = oCompanyAddressContol.txtCity.Text           ''state
            oProvider.ComapanyState = oCompanyAddressContol.cmbState.Text             ''city
            oProvider.ComapanyZip = oCompanyAddressContol.txtZip.Text               ''zip
            oProvider.ComapanyAreaCode = oCompanyAddressContol.txtAreaCode.Text     ''area code
            oProvider.ComapanyCountry = oCompanyAddressContol.cmbCountry.Text       ''Country
            oProvider.ComapanyCounty = oCompanyAddressContol.txtCounty.Text        ''County

            oProvider.ComapanyPhone = txtCompanyPhone.Text
            oProvider.ComapanyFax = mskCompanyFax.Text
            oProvider.ComapanyEmail = txtCompanyEmail.Text
            oProvider.ComapanyNPI = txtCompanyNPI.Text
            oProvider.ComapanyTaxID = txtCompanyTaxID.Text

            If txtCmpTaxonomyCode.Text.Trim() <> "" Then
                Dim _splitter As [String]()
                _splitter = txtCmpTaxonomyCode.Text.Split("-"c)
                oProvider.CompanyTaxonomyCode = _splitter(0)
            Else
                oProvider.CompanyTaxonomyCode = ""

            End If
            oProvider.SignWidth = ImgWidth


            'Sanjog - Added on 2011 May 23 for DPS No.
            oProvider.DPSNumber = txtDPSNumber.Text
            'Sanjog - Added on 2011 May 23 for DPS No.
            '************ sandip dhakane 20100726 added validation for SSN masktextbox
            'oProvider.SSNno = mtxt_SSNno.Text

            If Len(Trim(mtxt_SSNno.Text)) > 0 And Len(Trim(mtxt_SSNno.Text)) < 9 Then
                MessageBox.Show("SSN Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                mtxt_SSNno.Focus()
                Exit Sub
            Else
                oProvider.SSNno = mtxt_SSNno.Text
            End If

            '*****************  end code

            oProvider.EmployerID = txt_EmployerID.Text
            oProvider.Mobile = mskMobileNo.Text.Trim()

            If txtTaxonomy.Text.Trim() <> "" Then
                Dim _splitter As [String]()
                _splitter = txtTaxonomy.Text.Split("-"c)
                oProvider.Taxonomy = _splitter(0)
                oProvider.TaxonomyDesc = _splitter(1)
            End If

            If optMale.Checked = True Then
                oProvider.Gender = "Male"
            Else
                oProvider.Gender = "Female"
            End If

            oProvider.DEA = mskTxtDEA.Text.Trim()
            oProvider.ProviderTypeID = Convert.ToInt64(cmbDoctorType.SelectedValue)
            oProvider.Description = Convert.ToString(cmbDoctorType.Text)

            oProvider.NPI = txtNPI.Text.Trim()
            oProvider.ExternalCode = txtExternalCode.Text
            oProvider.UPIN = mskTxtUPIN.Text.Trim()
            oProvider.StateMedicalNo = txtStateMedicalLicenseNo.Text.Trim()

            If txtUserName.Tag IsNot Nothing Then
                oProvider.UserID = Convert.ToInt64(txtUserName.Tag)
            Else
                oProvider.UserID = 0
            End If

            oProvider.UserName = txtUserName.Text.Trim

            Dim oEncryption As New clsEncryption
            oProvider.NickName = oEncryption.EncryptToBase64String(txtNickName.Text.Trim, constEncryptDecryptKey)
            oProvider.Password = oEncryption.EncryptToBase64String(txtPassword.Text.Trim, constEncryptDecryptKey)
            oEncryption = Nothing

            If picSignature.Image IsNot Nothing Then
                oProvider.Signature = picSignature.Image
            Else
                oProvider.Signature = Nothing
            End If

            oProvider.ClinicID = _nClinicID

            'Provider Physical Address
            oProvider.PhysicalAddContactName = txtBIDPLContactName.Text.Trim()
            oProvider.PhysicalAddressline1 = oProviderPhysicalAddressContol.txtAddress1.Text.Trim()
            oProvider.PhysicalAddressline2 = oProviderPhysicalAddressContol.txtAddress2.Text.Trim()
            oProvider.PhysicalCity = oProviderPhysicalAddressContol.txtCity.Text.Trim()
            oProvider.PhysicalState = oProviderPhysicalAddressContol.cmbState.Text.Trim()
            oProvider.PhysicalZIP = oProviderPhysicalAddressContol.txtZip.Text.Trim()
            oProvider.PhysicalAreaCode = oProviderPhysicalAddressContol.txtAreaCode.Text.Trim()
            oProvider.PhysicalCountry = oProviderPhysicalAddressContol.cmbCountry.Text.Trim()
            If (oProviderPhysicalAddressContol.cmbCountry.Text.Trim().ToUpper() = "US") Then
                oProvider.PhysicalCounty = oProviderPhysicalAddressContol.txtCounty.Text.Trim()
            Else
                oProvider.PhysicalCounty = ""
            End If
            oProvider.PhysicalPagerNo = mskBIDPLPager.Text.Trim()
            oProvider.PhysicalPhoneNo = maskedBIDPLPhno.Text.Trim()
            oProvider.PhysicalFAX = mskBIDPLFax.Text.Trim()
            oProvider.PhysicalEmail = txtBIDPLEMail.Text.Trim()
            oProvider.PhysicalURL = txtBIDPLUrl.Text.Trim()

            'Provider Company Physical Address
            oProvider.CompanyPhysicalAddContactName = txtPLContactName.Text.Trim()
            oProvider.CompanyPhysicalAddressline1 = oProviderCompanyPhysicalAddressContol.txtAddress1.Text.Trim()
            oProvider.CompanyPhysicalAddressline2 = oProviderCompanyPhysicalAddressContol.txtAddress2.Text.Trim()
            oProvider.CompanyPhysicalCity = oProviderCompanyPhysicalAddressContol.txtCity.Text.Trim()
            oProvider.CompanyPhysicalState = oProviderCompanyPhysicalAddressContol.cmbState.Text.Trim()
            oProvider.CompanyPhysicalZIP = oProviderCompanyPhysicalAddressContol.txtZip.Text.Trim()
            oProvider.CompanyPhysicalAreaCode = oProviderCompanyPhysicalAddressContol.txtAreaCode.Text.Trim()
            oProvider.CompanyPhysicalCountry = oProviderCompanyPhysicalAddressContol.cmbCountry.Text.Trim()
            If (oProviderCompanyPhysicalAddressContol.cmbCountry.Text.Trim().ToUpper() = "US") Then
                oProvider.CompanyPhysicalCounty = oProviderCompanyPhysicalAddressContol.txtCounty.Text.Trim()
            Else
                oProvider.CompanyPhysicalCounty = ""
            End If
            oProvider.CompanyPhysicalPagerNo = mskPLPager.Text.Trim()
            oProvider.CompanyPhysicalPhoneNo = maskedPLPhno.Text.Trim()
            oProvider.CompanyPhysicalFAX = mskPLFax.Text.Trim()
            oProvider.CompanyPhysicalEmail = txtPLEMail.Text.Trim()
            oProvider.CompanyPhysicalURL = txtPLUrl.Text.Trim()

            'Provider Details           
            'If c1ProviderIdentification.Rows.Count >= 3 Then
            '    oProvider.ProviderDetails.MedicareID = Convert.ToString(c1ProviderIdentification.GetData(1, 1))
            '    oProvider.ProviderDetails.MedicaidID = Convert.ToString(c1ProviderIdentification.GetData(2, 1))


            '    Dim _Name As String = ""
            '    Dim _Value As String = ""
            '    For i As Integer = 3 To c1ProviderIdentification.Rows.Count - 1
            '        _Name = Convert.ToString(c1ProviderIdentification.GetData(i, 0))
            '        _Value = Convert.ToString(c1ProviderIdentification.GetData(i, 1))


            '        If _Name.Trim() <> "" Then
            '            Dim oOtherDetails As New ProviderDetails.structOtherIdentifications
            '            oOtherDetails.Code = _Value.Trim()
            '            oOtherDetails.Description = _Name.Trim()
            '            oProvider.ProviderDetails.OtherIdentifications.Add(oOtherDetails)
            '        End If
            '    Next
            'End If

            ''''''''''Provider Qualifier Details -Start''''''''''''''''
            Dim _nQualifierID As Int64
            Dim _sValue As String
            Dim _bIsSystem As Boolean
            Dim oQualifierDetails As New ProviderDetails.structOtherIdentifications
            Dim oCompanyQualifierDetails As New ProviderDetails.structCompanyIdentifications
            If c1ProviderIdentification.Rows.Count > 0 Then
                c1ProviderIdentification.FinishEditing()

                For i As Integer = 1 To c1ProviderIdentification.Rows.Count - 1
                    _nQualifierID = Convert.ToInt64(c1ProviderIdentification.GetData(i, 0))
                    _sValue = Convert.ToString(c1ProviderIdentification.GetData(i, 3))
                    _bIsSystem = Convert.ToBoolean(c1ProviderIdentification.GetData(i, 4))
                    If _sValue <> Nothing And _sValue <> String.Empty Then
                        oQualifierDetails = New ProviderDetails.structOtherIdentifications
                        oQualifierDetails.nQualifierID = _nQualifierID
                        oQualifierDetails.sValue = _sValue.Trim()
                        oQualifierDetails.bIsSystem = _bIsSystem
                        oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
                    End If
                Next
            End If

            ''NPI''
            _nQualifierID = 1
            _sValue = txtNPI.Text.ToString()
            _bIsSystem = True
            ''  If _sValue <> Nothing And _sValue <> String.Empty Then
            oQualifierDetails = New ProviderDetails.structOtherIdentifications
            oQualifierDetails.nQualifierID = _nQualifierID
            oQualifierDetails.sValue = _sValue.Trim()
            oQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
            '' End If

            ''UPIN''
            _nQualifierID = 5
            _sValue = mskTxtUPIN.Text.ToString()
            _bIsSystem = True
            ''If _sValue <> Nothing And _sValue <> String.Empty Then
            oQualifierDetails = New ProviderDetails.structOtherIdentifications
            oQualifierDetails.nQualifierID = _nQualifierID
            oQualifierDetails.sValue = _sValue.Trim()
            oQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
            ''   End If

            ''SSN''
            _nQualifierID = 6
            _sValue = mtxt_SSNno.Text.ToString()
            _bIsSystem = True
            '''If _sValue <> Nothing And _sValue <> String.Empty Then
            oQualifierDetails = New ProviderDetails.structOtherIdentifications
            oQualifierDetails.nQualifierID = _nQualifierID
            oQualifierDetails.sValue = _sValue.Trim()
            oQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
            '' End If

            ' ''DEA''
            '_nQualifierID = 0
            '_sValue = mskTxtDEA.Text.ToString()
            '_bIsSystem = True
            'oQualifierSysDetails = New ProviderDetails.structOtherIdentifications
            'oQualifierSysDetails.nQualifierID = _nQualifierID
            'oQualifierSysDetails.sValue = _sValue.Trim()
            'oQualifierSysDetails.bIsSystem = _bIsSystem
            'oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierSysDetails)

            ''State medical License''
            _nQualifierID = 8
            _sValue = txtStateMedicalLicenseNo.Text.ToString()
            _bIsSystem = True
            ''If _sValue <> Nothing And _sValue <> String.Empty Then
            oQualifierDetails = New ProviderDetails.structOtherIdentifications
            oQualifierDetails.nQualifierID = _nQualifierID
            oQualifierDetails.sValue = _sValue.Trim()
            oQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
            '' End If

            ''Employer ID''
            _nQualifierID = 7
            _sValue = txt_EmployerID.Text.ToString()
            _bIsSystem = True
            ''If _sValue <> Nothing And _sValue <> String.Empty Then
            oQualifierDetails = New ProviderDetails.structOtherIdentifications
            oQualifierDetails.nQualifierID = _nQualifierID
            oQualifierDetails.sValue = _sValue.Trim()
            oQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.OtherIdentifications.Add(oQualifierDetails)
            ''End If
            ''''''''''''''''''Provider Qualifier Details-End'''''''''''''''''''

            ''''''''''Provider Company Qualifier Details -Start''''''''''''''''
            If c1CompanyProvIdentification.Rows.Count > 0 Then
                c1CompanyProvIdentification.FinishEditing()
                For i As Integer = 1 To c1CompanyProvIdentification.Rows.Count - 1
                    _nQualifierID = Convert.ToInt64(c1CompanyProvIdentification.GetData(i, 0))
                    _sValue = Convert.ToString(c1CompanyProvIdentification.GetData(i, 3))
                    _bIsSystem = Convert.ToBoolean(c1CompanyProvIdentification.GetData(i, 4))
                    If _sValue <> Nothing And _sValue <> String.Empty Then
                        oCompanyQualifierDetails = New ProviderDetails.structCompanyIdentifications
                        oCompanyQualifierDetails.nQualifierID = _nQualifierID
                        oCompanyQualifierDetails.sValue = _sValue.Trim()
                        oCompanyQualifierDetails.bIsSystem = _bIsSystem
                        oProvider.ProviderDetails.CompanyIdentifications.Add(oCompanyQualifierDetails)
                    End If
                Next
            End If

            ''COMPANY NPI''
            _nQualifierID = 2
            _sValue = txtCompanyNPI.Text.ToString()
            _bIsSystem = True
            '' If _sValue <> Nothing And _sValue <> String.Empty Then
            oCompanyQualifierDetails = New ProviderDetails.structCompanyIdentifications
            oCompanyQualifierDetails.nQualifierID = _nQualifierID
            oCompanyQualifierDetails.sValue = _sValue.Trim()
            oCompanyQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.CompanyIdentifications.Add(oCompanyQualifierDetails)
            '' End If

            ''COMPANY TAX ID''
            _nQualifierID = 4
            _sValue = txtCompanyTaxID.Text.ToString()
            _bIsSystem = True
            ''If _sValue <> Nothing And _sValue <> String.Empty Then
            oCompanyQualifierDetails = New ProviderDetails.structCompanyIdentifications
            oCompanyQualifierDetails.nQualifierID = _nQualifierID
            oCompanyQualifierDetails.sValue = _sValue.Trim()
            oCompanyQualifierDetails.bIsSystem = _bIsSystem
            oProvider.ProviderDetails.CompanyIdentifications.Add(oCompanyQualifierDetails)
            ''End If
            ''''''''''Provider Company Qualifier Details -End''''''''''''''''


            If rbUpdate.Checked Then
                oProvider.SPI = lblSPI.Text
                oProvider.PrescriberLocation = False
            ElseIf rbPrescriberLocation.Checked Then
                oProvider.RootSPI = txtSPI.Text
                oProvider.PrescriberLocation = True
            End If

            oProvider.ActiveStartTime = dtpActiveStartTime.Value
            oProvider.ActiveEndTime = dtpActiveEndTime.Value

            oProvider.LicenceKey = txtLicenseKey.Text
            oProvider.AUSStatus = lblAusStatus.Text
            oProvider.AUSPortalID = _nAUSPortalID

           

            If rbNo.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.No
            ElseIf rbAlways.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.Always
            ElseIf rbUsePlanSetting.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.UsePlanSetting
            End If

            gstrCategory = Trim(txtFirstName.Text) & Space(2) & Trim(txtMiddleName.Text) & Space(2) & Trim(txtLastName.Text)

            oProvider.RequireSupervisingProviderforeRx = chkRequire_Supervising_Provider_for_eRx.CheckState

            Dim bsendprovider As Boolean = True
            If ISDEMOLicensce = False Then
                If lblAusStatus.Text.Trim = "1" Or txtLicenseKey.Text.Trim = "" Then
                    If Not IsNothing(TempProvider) Then
                        bsendprovider = ISProviderDataChange(TempProvider, oProvider)
                    End If
                    If bsendprovider = True Then
                        If _nProviderID > 0 Then
                            If lblAusStatus.Text.Trim <> "4" And txtLicenseKey.Text.Trim = "" Then
                                If MessageBox.Show("Provider info will be sent to TRIARQ Health and license key will be generated to activate the provider, are you sure ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                    TabControl1.SelectedTab = tbpgProvider
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            End If
         

            _nProviderID = oProvider.SaveProvider(ISDEMOLicensce)
            If ISDEMOLicensce = False Then
                If bsendprovider = True Then
                    If lblAusStatus.Text.Trim <> "4" Then
                        Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                            oLicense.SendProviderForApproval(_nProviderID)
                        End Using
                    End If
                End If
            End If

            'Start'LabId
            If _nProviderID > 0 Then
                'If txtLabId.Text <> "" Then
                SaveEmdeonLabId(_nProviderID, txtLabId.Text.Trim())
                'End If
            End If
            'End'LabId

            If _nProviderID > 0 Then
                'oProvider.ProviderID = CType(txtFirstName.Tag, Int32)
                UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update)
                If gblnIsSurescriptEnabled Then
                    'sarika for removing provider validations
                    strMessage = AddUpdatePrescriber(oProvider)
                    If strMessage.Trim <> "" Then
                        If strMessage.Trim.EndsWith(",") Then
                            strMessage = strMessage.Substring(0, Len(strMessage.Trim) - 1)
                        End If
                        If MessageBox.Show(strMessage & vbCrLf & "Do you want to insert the missing data?" & vbCrLf & "Click No to insert the Provider." & vbCrLf & "Click Yes to enter the missing data for Surescript.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                            Exit Sub
                        End If
                    End If
                End If

                oProvider = Nothing
                ' Me.DialogResult = DialogResult.OK
                If blnIsSurescriptError = True Then
                    blnIsSurescriptError = False
                    Exit Sub
                End If
                Me.Close()
            Else
                MessageBox.Show("Unable to update Provider Details ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                UpdateErrorLog("Unable to update Provider Details ", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update, True)
                oProvider = Nothing
            End If
            'Else
            '    If blnNewModify = False Then
            '        If objProvider.InsertProviderDetails = True Then
            '            If gblnIsSurescriptEnabled Then
            '                'sarika for removing provider validations
            '                strMessage = AddUpdatePrescriber(oProvider)

            '                If strMessage.Trim.EndsWith(",") Then
            '                    strMessage = strMessage.Substring(0, Len(strMessage.Trim) - 1)
            '                End If
            '                If strMessage.Trim <> "" Then
            '                    If MessageBox.Show(strMessage & vbCrLf & "Do you want to insert the missing data?" & vbCrLf & "Click No to update the Provider." & vbCrLf & "Click Yes to enter the missing data for Surescript.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '                        objNewProvider = oProvider
            '                        blnNewModify = True
            '                        Exit Sub
            '                    End If
            '                End If
            '                '--
            '            Else

            '            End If


            '            UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Add)
            '            '  Me.DialogResult = DialogResult.OK
            '            If blnIsSurescriptError = True Then
            '                blnIsSurescriptError = False
            '                Exit Sub
            '            End If

            '            Me.Close()
            '        Else
            '            MessageBox.Show("Unable to insert Doctor Details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            UpdateErrorLog("Unable to Add Provider Details due to " & objProvider.ErrorMessage, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Add, True)
            '            oProvider = Nothing
            '        End If  'objProvider.InsertProviderDetails = True

            '    Else

            '        If gblnIsSurescriptEnabled Then
            '            oProvider = objNewProvider
            '            'sarika for removing provider validations
            '            strMessage = AddUpdatePrescriber(oProvider)

            '            If strMessage.Trim.EndsWith(",") Then
            '                strMessage = strMessage.Substring(0, Len(strMessage.Trim) - 1)
            '            End If
            '            If strMessage.Trim <> "" Then
            '                If MessageBox.Show(strMessage & vbCrLf & "Do you want to insert the missing data?" & vbCrLf & "Click No to update the Provider." & vbCrLf & "Click Yes to enter the missing data for Surescript.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '                    blnNewModify = True
            '                    Exit Sub
            '                End If
            '            End If
            '            '--
            '        Else

            '        End If

            '        oProvider = Nothing
            '        UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Add)
            '        '  Me.DialogResult = DialogResult.OK
            '        If blnIsSurescriptError = True Then
            '            blnIsSurescriptError = False
            '            Exit Sub
            '        End If

            'Me.Close()
            'End If             'blnNewModify = False Then





            If (_nNoOfProviderCompanies > 1) Then

                Dim e1 As System.Windows.Forms.MouseEventArgs
                CmbProviderCompany_MouseClick(sender, e1)

                '''Coding for Saving provider Other Company
                ''' 

                For nRowindex As Int16 = 0 To _dtProviderMultipleCompanyData.Rows.Count - 1

                    If Convert.IsDBNull(_dtProviderMultipleCompanyData.Rows(nRowindex)("nProviderID")) Or _dtProviderMultipleCompanyData.Rows(nRowindex)("nProviderID").ToString() = "0" Then
                        _dtProviderMultipleCompanyData.Rows(nRowindex)("nProviderID") = _nProviderID
                    End If

                    If _dtProviderMultipleCompanyData.Rows(nRowindex)("sCompanyTaxonomyCode").ToString().Trim() <> "" Then
                        Dim _splitter As [String]()
                        _splitter = _dtProviderMultipleCompanyData.Rows(nRowindex)("sCompanyTaxonomyCode").ToString().Trim().Split("-"c)
                        _dtProviderMultipleCompanyData.Rows(nRowindex)("sCompanyTaxonomyCode") = _splitter(0)
                    Else
                        _dtProviderMultipleCompanyData.Rows(nRowindex)("sCompanyTaxonomyCode") = ""

                    End If
                Next

                oClsProviderCompany.saveProviderCompany(_dtProviderMultipleCompanyData, _dsCompanyQualifierDetails)
                e1 = Nothing
            End If


            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Unable to Add/update Provider Details due to " & ex.ToString, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
        Finally

        End Try
    End Sub

    'Private Sub objPrescriber_SurescriptError() Handles objPrescriber.SurescriptError
    '    blnIsSurescriptError = True
    'End Sub

    '' SUDHIR 2009025 ''
    Private Sub FillProviderTypes()
        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        Dim dtProviderType As New DataTable()
        Try
            dtProviderType = oProvider.GetProviderTypes()
            If dtProviderType IsNot Nothing Then
                cmbDoctorType.DataSource = dtProviderType
                cmbDoctorType.DisplayMember = "sProviderType"
                cmbDoctorType.ValueMember = "nProviderTypeID"
            End If
        Catch ex As Exception
        Finally
            oProvider = Nothing
        End Try
    End Sub


    Private Sub LoadProvider(ByVal ProviderID As Int64)
        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        oProvider.GetProvider(ProviderID)
        TempProvider = New gloAUSLibrary.Class.TempProviderdata
        Try

            Panel6.Enabled = False

            'Fill Provider
            txtPrefix.Text = oProvider.Prefix
            txtFirstName.Text = oProvider.FirstName
            txtMiddleName.Text = oProvider.MiddleName
            txtLastName.Text = oProvider.LastName
            txtSuffix.Text = oProvider.Suffix

            txtBMContact.Text = oProvider.BMContactName
            If Not IsNothing(oProvider.dtDOB) AndAlso oProvider.dtDOB <> "#12:00:00 AM#" Then
                mtxtDOB.Text = oProvider.dtDOB.ToString("MM/dd/yyyy")
            End If
            'txtBMAddress1.Text = oProvider.BMAddress1
            'txtBMAddress2.Text = oProvider.BMAddress2
            'txtBMstate.Text = oProvider.BMState
            'txtBMCity.Text = oProvider.BMCity
            'txtBMZip.Text = oProvider.BMZIP
            'txtBMCity.Text = oProvider.BMCity

            oBussinessAddressContol.isFormLoading = True
            oBussinessAddressContol.txtAddress1.Text = oProvider.BMAddress1         ''Add1
            oBussinessAddressContol.txtAddress2.Text = oProvider.BMAddress2         ''Add2
            oBussinessAddressContol.txtCity.Text = oProvider.BMCity                 ''city
            oBussinessAddressContol.txtZip.Text = oProvider.BMZIP                   ''Zip

            oBussinessAddressContol.cmbCountry.Text = oProvider.BMCountry         ''Country
            oBussinessAddressContol.txtCounty.Text = oProvider.BMCounty           ''county
            oBussinessAddressContol.txtAreaCode.Text = oProvider.BMAreaCode       ''AreaCode
            oBussinessAddressContol.cmbState.Text = oProvider.BMState               ''state
            oBussinessAddressContol.isFormLoading = False

            txtBPracContactName.Text = oProvider.BPracContactName
            'txtBPracAddress1.Text = oProvider.BPracAddress1
            'txtBPracAddress2.Text = oProvider.BPracAddress2
            'txtBPracState.Text = oProvider.BPracState
            'txtBPracCity.Text = oProvider.BPracCity
            'txtBPracZIP.Text = oProvider.BPracZIP
            'txtBPracCity.Text = oProvider.BPracCity

            oPracticeAddressContol.isFormLoading = True
            oPracticeAddressContol.txtAddress1.Text = oProvider.BPracAddress1          ''Add1
            oPracticeAddressContol.txtAddress2.Text = oProvider.BPracAddress2         ''Add2
            oPracticeAddressContol.txtCity.Text = oProvider.BPracCity                  ''city
            oPracticeAddressContol.txtZip.Text = oProvider.BPracZIP                    ''Zip

            oPracticeAddressContol.cmbCountry.Text = oProvider.BPracCountry         ''Country
            oPracticeAddressContol.txtCounty.Text = oProvider.BPracCounty           ''County
            oPracticeAddressContol.txtAreaCode.Text = oProvider.BPracAreaCode       ''AreaCode
            oPracticeAddressContol.cmbState.Text = oProvider.BPracState                ''state
            oPracticeAddressContol.isFormLoading = False

            mskMobileNo.Text = oProvider.Mobile
            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID

            'Start' Lab Id
            If _nProviderID > 0 Then

                Dim _eLabId As String = String.Empty
                _eLabId = GetEmdeonLabId(_nProviderID)

                If _eLabId <> "" Then
                    'Set value read for a lab id against a provider.
                    txtLabId.Text = _eLabId
                End If

            End If
            'End' Lab Id

            cmbDoctorType.Text = oProvider.Description
            If oProvider.Taxonomy <> "" Then
                txtTaxonomy.Text = (oProvider.Taxonomy & "-") + oProvider.TaxonomyDesc
            End If
            mtxt_SSNno.Text = oProvider.SSNno
            txt_EmployerID.Text = oProvider.EmployerID

            mskTxtDEA.Text = oProvider.DEA

            If oProvider.Gender = "Male" Then
                optMale.Checked = True
                'if
            ElseIf oProvider.Gender = "Female" Then
                optFemale.Checked = True
            End If
            'else
            mskBMPhoneNo.Text = oProvider.BMPhone
            txtBMEmailAddress.Text = oProvider.BMEmail
            mskBMFax.Text = oProvider.BMFAX
            mskBMPager.Text = oProvider.BMPager
            txtBMURL.Text = oProvider.BMURL

            lblDirectAddressValue.Text = oProvider.DirectAddress  ''Direct Address

            maskedBpracPhno.Text = oProvider.BPracPhone
            txtBPracEMail.Text = oProvider.BPracEmail
            mskBPracFax.Text = oProvider.BPracFAX
            mskBPracPager.Text = oProvider.BPracPager
            txtBPracUrl.Text = oProvider.BPracURL

            txtCompanyName.Text = oProvider.ComapanyName
            txtCompanyContactName.Text = oProvider.ComapanyContactName
            'txtCompanyAddress1.Text = oProvider.ComapanyAddress1
            'txtCompanyAddress2.Text = oProvider.ComapanyAddress2
            'txtCompanyCity.Text = oProvider.ComapanyCity
            'txtCompanyState.Text = oProvider.ComapanyState
            'txtCompanyZip.Text = oProvider.ComapanyZip

            oCompanyAddressContol.isFormLoading = True
            oCompanyAddressContol.txtAddress1.Text = oProvider.ComapanyAddress1          ''Add1
            oCompanyAddressContol.txtAddress2.Text = oProvider.ComapanyAddress2          ''Add2
            oCompanyAddressContol.txtCity.Text = oProvider.ComapanyCity                   ''city
            oCompanyAddressContol.txtZip.Text = oProvider.ComapanyZip                     ''Zip

            oCompanyAddressContol.cmbCountry.Text = oProvider.ComapanyCountry            ''Country
            oCompanyAddressContol.txtCounty.Text = oProvider.ComapanyCounty             ''county
            oCompanyAddressContol.txtAreaCode.Text = oProvider.ComapanyAreaCode         ''Area Code
            oCompanyAddressContol.cmbState.Text = oProvider.ComapanyState                 ''state
            'Added By Rahul Patel on 10-09-2010
            'For not working zip control problem.
            oCompanyAddressContol.isFormLoading = False

            txtCompanyPhone.Text = oProvider.ComapanyPhone
            mskCompanyFax.Text = oProvider.ComapanyFax
            txtCompanyEmail.Text = oProvider.ComapanyEmail

            'Added by Anil on 20090310
            txtCompanyNPI.Text = oProvider.ComapanyNPI
            txtCompanyTaxID.Text = oProvider.ComapanyTaxID
            txtCmpTaxonomyCode.Text = oProvider.CompanyTaxonomyCode

            mskTxtUPIN.Text = oProvider.UPIN
            txtNPI.Text = oProvider.NPI
            txtExternalCode.Text = oProvider.ExternalCode '' External Code
            txtStateMedicalLicenseNo.Text = oProvider.StateMedicalNo

            'Sanjog - Added on 2011 May 23 for DPS No.
            txtDPSNumber.Text = oProvider.DPSNumber
            'Sanjog - Added on 2011 May 23 for DPS No.
            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID


            If oProvider.Signature IsNot Nothing Then
                picSignature.Image = oProvider.Signature
                ImgWidth = oProvider.Signature.Width
                picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                picSignature.Visible = True
            End If

            txtBIDPLContactName.Text = oProvider.PhysicalAddContactName
            oProviderPhysicalAddressContol.isFormLoading = True
            oProviderPhysicalAddressContol.txtAddress1.Text = oProvider.PhysicalAddressline1
            oProviderPhysicalAddressContol.txtAddress2.Text = oProvider.PhysicalAddressline2
            oProviderPhysicalAddressContol.txtCity.Text = oProvider.PhysicalCity

            oProviderPhysicalAddressContol.txtZip.Text = oProvider.PhysicalZIP
            oProviderPhysicalAddressContol.txtAreaCode.Text = oProvider.PhysicalAreaCode
            oProviderPhysicalAddressContol.cmbCountry.Text = oProvider.PhysicalCountry
            oProviderPhysicalAddressContol.txtCounty.Text = oProvider.PhysicalCounty
            oProviderPhysicalAddressContol.cmbState.Text = oProvider.PhysicalState
            oProviderPhysicalAddressContol.isFormLoading = False
            mskBIDPLPager.Text = oProvider.PhysicalPagerNo
            maskedBIDPLPhno.Text = oProvider.PhysicalPhoneNo
            mskBIDPLFax.Text = oProvider.PhysicalFAX
            txtBIDPLEMail.Text = oProvider.PhysicalEmail
            txtBIDPLUrl.Text = oProvider.PhysicalURL


            txtPLContactName.Text = oProvider.CompanyPhysicalAddContactName
            oProviderCompanyPhysicalAddressContol.isFormLoading = True
            oProviderCompanyPhysicalAddressContol.txtAddress1.Text = oProvider.CompanyPhysicalAddressline1
            oProviderCompanyPhysicalAddressContol.txtAddress2.Text = oProvider.CompanyPhysicalAddressline2
            oProviderCompanyPhysicalAddressContol.txtCity.Text = oProvider.CompanyPhysicalCity

            oProviderCompanyPhysicalAddressContol.txtZip.Text = oProvider.CompanyPhysicalZIP
            oProviderCompanyPhysicalAddressContol.txtAreaCode.Text = oProvider.CompanyPhysicalAreaCode
            oProviderCompanyPhysicalAddressContol.cmbCountry.Text = oProvider.CompanyPhysicalCountry
            oProviderCompanyPhysicalAddressContol.txtCounty.Text = oProvider.CompanyPhysicalCounty
            oProviderCompanyPhysicalAddressContol.cmbState.Text = oProvider.CompanyPhysicalState
            oProviderCompanyPhysicalAddressContol.isFormLoading = False
            mskPLPager.Text = oProvider.CompanyPhysicalPagerNo
            maskedPLPhno.Text = oProvider.CompanyPhysicalPhoneNo
            mskPLFax.Text = oProvider.CompanyPhysicalFAX
            txtPLEMail.Text = oProvider.CompanyPhysicalEmail
            txtPLUrl.Text = oProvider.CompanyPhysicalURL



            'c1ProviderIdentification.SetData(1, 1, oProvider.ProviderDetails.MedicareID)
            'c1ProviderIdentification.SetData(2, 1, oProvider.ProviderDetails.MedicaidID)

            'Dim RowIndex As Int32 = 0
            'For i As Integer = 1 To oProvider.ProviderDetails.OtherIdentifications.Count
            '    c1ProviderIdentification.Rows.Add()
            '    RowIndex = c1ProviderIdentification.Rows.Count - 1

            '    c1ProviderIdentification.SetData(RowIndex, 0, CType(oProvider.ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Description)
            '    c1ProviderIdentification.SetData(RowIndex, 1, CType(oProvider.ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Code)
            'Next


            If IsDBNull(oProvider.ActiveStartTime) Or oProvider.ActiveStartTime = Nothing Then
                dtpActiveStartTime.Value = DateTime.Now
            Else
                dtpActiveStartTime.Value = oProvider.ActiveStartTime
            End If
            If IsDBNull(oProvider.ActiveEndTime) Or oProvider.ActiveEndTime = Nothing Then
                dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
            Else
                dtpActiveEndTime.Value = oProvider.ActiveEndTime
            End If

            If oProvider.PARequired = clsProvider.PriorAuthorizationRequired.No.GetHashCode() Then
                rbNo.Checked = True
            ElseIf oProvider.PARequired = clsProvider.PriorAuthorizationRequired.Always.GetHashCode() Then
                rbAlways.Checked = True
            ElseIf oProvider.PARequired = clsProvider.PriorAuthorizationRequired.UsePlanSetting.GetHashCode() Then
                rbUsePlanSetting.Checked = True
            End If

            TempLicensekey = oProvider.LicenceKey
            txtLicenseKey.Text = oProvider.LicenceKey
            lblAusStatus.Text = oProvider.AUSStatus
            _nAUSPortalID = oProvider.AUSPortalID

            TempProvider.FirstName = oProvider.FirstName
            TempProvider.MiddleName = oProvider.MiddleName
            TempProvider.LastName = oProvider.LastName
            TempProvider.BMAddress1 = oProvider.BMAddress1
            TempProvider.BMAddress2 = oProvider.BMAddress2
            TempProvider.BMCity = oProvider.BMCity
            TempProvider.BMZIP = oProvider.BMZIP
            TempProvider.BMState = oProvider.BMState
            TempProvider.NPI = oProvider.NPI
            TempProvider.licenseKey = oProvider.LicenceKey

            If lblAusStatus.Text = "1" Then
                If txtLicenseKey.Text = "" Then
                    ReffreshLicense(False)
                    If btnLicenseRefresh.Tag = "valid" Then
                        lblLicenseMessage.Visible = True
                    End If
                End If
            End If
            If IsDBNull(oProvider.SPI) Or oProvider.SPI Is Nothing Or oProvider.SPI = "" Then
                rbPrescriber.Checked = True
                pnlSPI.Visible = False
                rbUpdate.Enabled = False
            Else
                rbPrescriber.Enabled = False
                rbUpdate.Checked = True
                lblPreDetails.Visible = False
                pnlSPI.Visible = False
                txtSPI.Visible = False
                lblRoot.Visible = False
                lblSPI.Text = oProvider.SPI
                SPICode = oProvider.SPI
                txtSPI.Enabled = False
            End If
            chkRequire_Supervising_Provider_for_eRx.Checked = oProvider.RequireSupervisingProviderforeRx

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
   
    Private Sub DesignGrid()

        ''Design Additional Billing IDs Grid 
        c1ProviderIdentification.Rows.Fixed = 1
        c1ProviderIdentification.Cols.Fixed = 0
        c1ProviderIdentification.Rows.Count = 1
        c1ProviderIdentification.Cols.Count = 5

        c1ProviderIdentification.SetData(0, 0, "MainID") ''ID
        c1ProviderIdentification.SetData(0, 1, "Code")
        c1ProviderIdentification.SetData(0, 2, "ID") ''Description
        c1ProviderIdentification.SetData(0, 3, "Value")
        c1ProviderIdentification.SetData(0, 4, "bIsSystem")

        'c1ProviderIdentification.SetData(1, 0, "Medicare ID")
        'c1ProviderIdentification.SetData(1, 1, "")
        c1ProviderIdentification.Cols(0).DataType = GetType(System.Int64)
        c1ProviderIdentification.Cols(1).DataType = GetType(System.String)
        c1ProviderIdentification.Cols(2).DataType = GetType(System.String)
        c1ProviderIdentification.Cols(3).DataType = GetType(System.String)
        c1ProviderIdentification.Cols(4).DataType = GetType(System.Boolean)
        'c1ProviderIdentification.SetData(2, 0, "Medicaid ID")
        'c1ProviderIdentification.SetData(2, 1, "")

        Dim _width As Int32 = gbProviderIdentification.Width
        c1ProviderIdentification.Cols(0).Width = 0
        c1ProviderIdentification.Cols(1).Width = Convert.ToInt32(_width * 0.2)
        c1ProviderIdentification.Cols(2).Width = Convert.ToInt32(_width * 0.5)
        c1ProviderIdentification.Cols(3).Width = Convert.ToInt32(_width * 0.42)
        c1ProviderIdentification.Cols(4).Width = 0

        c1ProviderIdentification.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1ProviderIdentification.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1ProviderIdentification.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1ProviderIdentification.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        c1ProviderIdentification.Cols(0).AllowEditing = False
        c1ProviderIdentification.Cols(1).AllowEditing = False
        c1ProviderIdentification.Cols(2).AllowEditing = False
        c1ProviderIdentification.Cols(3).AllowEditing = True
        c1ProviderIdentification.Cols(4).AllowEditing = False

        c1ProviderIdentification.Cols(0).Visible = False
        c1ProviderIdentification.Cols(1).Visible = False
        c1ProviderIdentification.Cols(2).Visible = True
        c1ProviderIdentification.Cols(3).Visible = True
        c1ProviderIdentification.Cols(4).Visible = False

        Dim dtQualifierID As DataTable = New DataTable
        Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString)

        If _bIsCloneProvider = True AndAlso _nCloneFromProviderID > 0 Then
            'dtQualifierID = ogloSettings.getIDQualifiers(3, _nCloneFromProviderID, True)
            'Don't need to load the existing values for clone, so calling the existing function
            dtQualifierID = ogloSettings.getIDQualifiers(3, _nProviderID, True)
        Else
            dtQualifierID = ogloSettings.getIDQualifiers(3, _nProviderID, True)
        End If

        If dtQualifierID IsNot Nothing AndAlso dtQualifierID.Rows.Count > 0 Then
            For i As Integer = 0 To dtQualifierID.Rows.Count - 1
                c1ProviderIdentification.Rows.Add()
                Dim RowIndex As Int32 = c1ProviderIdentification.Rows.Count - 1
                c1ProviderIdentification.SetData(RowIndex, 0, Convert.ToString(dtQualifierID.Rows(i)("nQualifierID")))
                c1ProviderIdentification.SetData(RowIndex, 1, Convert.ToString(dtQualifierID.Rows(i)("sCode")))
                c1ProviderIdentification.SetData(RowIndex, 2, Convert.ToString(dtQualifierID.Rows(i)("sAdditionalDescription")))
                c1ProviderIdentification.SetData(RowIndex, 3, Convert.ToString(dtQualifierID.Rows(i)("sValue")))
                c1ProviderIdentification.SetData(RowIndex, 4, Convert.ToString(dtQualifierID.Rows(i)("bIsSystem")))
            Next
        End If




    End Sub

    Private Sub DesignCompanyAdditionalGrid()

        ''Design Company Additional Billing IDs Grid 
        c1CompanyProvIdentification.Rows.Fixed = 1
        c1CompanyProvIdentification.Cols.Fixed = 0
        c1CompanyProvIdentification.Rows.Count = 1
        c1CompanyProvIdentification.Cols.Count = 5

        c1CompanyProvIdentification.SetData(0, 0, "MainID") ''ID
        c1CompanyProvIdentification.SetData(0, 1, "Code")
        c1CompanyProvIdentification.SetData(0, 2, "ID") ''Description
        c1CompanyProvIdentification.SetData(0, 3, "Value")
        c1CompanyProvIdentification.SetData(0, 4, "bIsSystem")

        'c1CompanyProvIdentification.SetData(1, 0, "Medicare ID")
        'c1CompanyProvIdentification.SetData(1, 1, "")
        c1CompanyProvIdentification.Cols(0).DataType = GetType(System.Int64)
        c1CompanyProvIdentification.Cols(1).DataType = GetType(System.String)
        c1CompanyProvIdentification.Cols(2).DataType = GetType(System.String)
        c1CompanyProvIdentification.Cols(3).DataType = GetType(System.String)
        c1CompanyProvIdentification.Cols(4).DataType = GetType(System.Boolean)
        'c1CompanyProvIdentification.SetData(2, 0, "Medicaid ID")
        'c1CompanyProvIdentification.SetData(2, 1, "")

        Dim _width As Int32 = Panel23.Width
        c1CompanyProvIdentification.Cols(0).Width = 0
        c1CompanyProvIdentification.Cols(1).Width = Convert.ToInt32(_width * 0.2)
        c1CompanyProvIdentification.Cols(2).Width = Convert.ToInt32(_width * 0.5)
        c1CompanyProvIdentification.Cols(3).Width = Convert.ToInt32(_width * 0.42)
        c1CompanyProvIdentification.Cols(4).Width = 0

        c1CompanyProvIdentification.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1CompanyProvIdentification.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1CompanyProvIdentification.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1CompanyProvIdentification.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        c1CompanyProvIdentification.Cols(0).AllowEditing = False
        c1CompanyProvIdentification.Cols(1).AllowEditing = False
        c1CompanyProvIdentification.Cols(2).AllowEditing = False
        c1CompanyProvIdentification.Cols(3).AllowEditing = True
        c1CompanyProvIdentification.Cols(4).AllowEditing = False

        c1CompanyProvIdentification.Cols(0).Visible = False
        c1CompanyProvIdentification.Cols(1).Visible = False
        c1CompanyProvIdentification.Cols(2).Visible = True
        c1CompanyProvIdentification.Cols(3).Visible = True
        c1CompanyProvIdentification.Cols(4).Visible = False

        Dim dtQualifierID As DataTable = New DataTable
        Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString)

        If _bIsCloneProvider = True AndAlso _nCloneFromProviderID > 0 Then
            dtQualifierID = ogloSettings.getIDQualifiers(4, _nCloneFromProviderID, True)
        Else
            dtQualifierID = ogloSettings.getIDQualifiers(4, _nProviderID, True)
        End If

        If dtQualifierID IsNot Nothing AndAlso dtQualifierID.Rows.Count > 0 Then
            For i As Integer = 0 To dtQualifierID.Rows.Count - 1
                c1CompanyProvIdentification.Rows.Add()
                Dim RowIndex As Int32 = c1CompanyProvIdentification.Rows.Count - 1
                c1CompanyProvIdentification.SetData(RowIndex, 0, Convert.ToString(dtQualifierID.Rows(i)("nQualifierID")))
                c1CompanyProvIdentification.SetData(RowIndex, 1, Convert.ToString(dtQualifierID.Rows(i)("sCode")))
                c1CompanyProvIdentification.SetData(RowIndex, 2, Convert.ToString(dtQualifierID.Rows(i)("sAdditionalDescription")))
                c1CompanyProvIdentification.SetData(RowIndex, 3, Convert.ToString(dtQualifierID.Rows(i)("sValue")))
                c1CompanyProvIdentification.SetData(RowIndex, 4, Convert.ToString(dtQualifierID.Rows(i)("bIsSystem")))
            Next
        End If
    End Sub

    'Private Sub c1ProviderIdentification_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProviderIdentification.BeforeEdit
    '    If (e.Col = 0 AndAlso e.Row = 1) OrElse (e.Col = 0 AndAlso e.Row = 2) Then
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Sub chkAddressasAbove_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAddressasAbove.CheckedChanged
        If chkAddressasAbove.Checked = True Then


            'txtBPracAddress1.Text = txtBMAddress1.Text.Trim()
            'txtBPracAddress2.Text = txtBMAddress2.Text.Trim()
            'txtBPracState.Text = txtBMstate.Text.Trim()
            'txtBPracCity.Text = txtBMCity.Text.Trim()
            'txtBPracZIP.Text = txtBMZip.Text.Trim()
            'Commented by Rahul A Patel on 09-09-2010
            'As User control is used it shoud be fetch from the user control so modify code as per requirement

            txtBPracContactName.Text = txtBMContact.Text

            oPracticeAddressContol.isFormLoading = True
            oPracticeAddressContol.txtAddress1.Text = oBussinessAddressContol.txtAddress1.Text.Trim()
            oPracticeAddressContol.txtAddress2.Text = oBussinessAddressContol.txtAddress2.Text.Trim()
            oPracticeAddressContol.cmbState.Text = oBussinessAddressContol.cmbState.Text
            oPracticeAddressContol.cmbCountry.SelectedIndex = oBussinessAddressContol.cmbCountry.SelectedIndex
            oPracticeAddressContol.txtCity.Text = oBussinessAddressContol.txtCity.Text.Trim()
            oPracticeAddressContol.txtZip.Text = oBussinessAddressContol.txtZip.Text.Trim()
            oPracticeAddressContol.txtArea.Text = oBussinessAddressContol.txtArea.Text.Trim()
            oPracticeAddressContol.txtAreaCode.Text = oBussinessAddressContol.txtAreaCode.Text.Trim()
            oPracticeAddressContol.txtCounty.Text = oBussinessAddressContol.txtCounty.Text
            oPracticeAddressContol.isFormLoading = False
            'End of code modified by Rahul Patel

            maskedBpracPhno.Text = mskBMPhoneNo.Text
            mskBPracFax.Text = mskBMFax.Text.Trim()
            mskBPracPager.Text = mskBMPager.Text.Trim()
            txtBPracEMail.Text = txtBMEmailAddress.Text.Trim()

            txtBPracUrl.Text = txtBMURL.Text.Trim()
        Else


            txtBPracContactName.Text = ""

            oPracticeAddressContol.isFormLoading = True
            oPracticeAddressContol.txtAddress1.Text = ""
            oPracticeAddressContol.txtAddress2.Text = ""
            oPracticeAddressContol.cmbState.Text = ""
            oPracticeAddressContol.txtCity.Text = ""
            oPracticeAddressContol.txtZip.Text = ""
            oPracticeAddressContol.txtArea.Text = ""
            oPracticeAddressContol.txtAreaCode.Text = ""
            oPracticeAddressContol.txtCounty.Text = ""
            oPracticeAddressContol.isFormLoading = False
            'End of code modified by Rahul Patel

            maskedBpracPhno.Text = ""
            mskBPracFax.Text = ""
            mskBPracPager.Text = ""
            txtBPracEMail.Text = ""

            txtBPracUrl.Text = ""
        End If
    End Sub

    Private Sub chkCompanyAsAbove_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCompanyAsAbove.CheckedChanged
        If chkCompanyAsAbove.Checked = True Then


            'txtCompanyAddress1.Text = txtBMAddress1.Text.Trim()
            'txtCompanyAddress2.Text = txtBMAddress2.Text.Trim()
            'txtCompanyState.Text = txtBMstate.Text.Trim()
            'txtCompanyCity.Text = txtBMCity.Text.Trim()
            'txtCompanyZip.Text = txtBMZip.Text.Trim()

            'Commented by Rahul A Patel on 09-09-2010
            'As User control is used it shoud be fetch from the user control so modify code as per requirement

            txtCompanyContactName.Text = txtBMContact.Text

            oCompanyAddressContol.isFormLoading = True
            oCompanyAddressContol.txtAddress1.Text = oBussinessAddressContol.txtAddress1.Text.Trim()
            oCompanyAddressContol.txtAddress2.Text = oBussinessAddressContol.txtAddress2.Text.Trim()
            oCompanyAddressContol.cmbState.Text = oBussinessAddressContol.cmbState.Text
            'oCompanyAddressContol.cmbState.SelectedItem = txtBMstate.Text.Trim()
            oCompanyAddressContol.cmbCountry.SelectedIndex = oBussinessAddressContol.cmbCountry.SelectedIndex
            oCompanyAddressContol.txtCity.Text = oBussinessAddressContol.txtCity.Text.Trim()
            oCompanyAddressContol.txtZip.Text = oBussinessAddressContol.txtZip.Text.Trim()
            oCompanyAddressContol.txtArea.Text = oBussinessAddressContol.txtArea.Text.Trim()
            oCompanyAddressContol.txtAreaCode.Text = oBussinessAddressContol.txtAreaCode.Text.Trim()
            oCompanyAddressContol.txtCounty.Text = oBussinessAddressContol.txtCounty.Text
            oCompanyAddressContol.isFormLoading = False

            ''End of code modified by Rahul Patel
            txtCompanyPhone.Text = mskBMPhoneNo.Text
            mskCompanyFax.Text = mskBMFax.Text.Trim()
            txtCompanyEmail.Text = txtBMEmailAddress.Text.Trim()
        Else

            txtCompanyContactName.Text = ""

            oCompanyAddressContol.isFormLoading = True
            oCompanyAddressContol.txtAddress1.Text = ""
            oCompanyAddressContol.txtAddress2.Text = ""
            oCompanyAddressContol.cmbState.Text = ""
            'oCompanyAddressContol.cmbState.SelectedItem = txtBMstate.Text.Trim()
            oCompanyAddressContol.txtCity.Text = ""
            oCompanyAddressContol.txtZip.Text = ""
            oCompanyAddressContol.txtArea.Text = ""
            oCompanyAddressContol.txtAreaCode.Text = ""
            oCompanyAddressContol.txtCounty.Text = ""
            oCompanyAddressContol.isFormLoading = False

            ''End of code modified by Rahul Patel
            txtCompanyPhone.Text = ""
            mskCompanyFax.Text = ""
            txtCompanyEmail.Text = ""
        End If
    End Sub

    Private Sub btn_BrowseTaxonomy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_BrowseTaxonomy.Click
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Taxonomy, False, Me.Width)
            oListControl.ClinicID = _nClinicID
            oListControl.ControlHeader = "Taxonomy"
            'tsb_SearchAppointment.Enabled = false;

            _CurrentControlType = gloListControl.gloListControlType.Providers
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
        Finally
            pnl_tlsp_Top.Visible = False
            TabControl1.Visible = False

            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub oListControl_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        If _CurrentControlType = gloListControl.gloListControlType.Taxonomy Then

            txtCmpTaxonomyCode.Clear()

            If oListControl.SelectedItems.Count > 0 Then

                txtCmpTaxonomyCode.Text = (oListControl.SelectedItems(0).Code.ToString() & "-") + oListControl.SelectedItems(0).Description.ToString()

            End If
        Else
            txtTaxonomy.Clear()

            If oListControl.SelectedItems.Count > 0 Then

                txtTaxonomy.Text = (oListControl.SelectedItems(0).Code.ToString() & "-") + oListControl.SelectedItems(0).Description.ToString()

            End If
        End If

        pnl_tlsp_Top.Visible = True
        TabControl1.Visible = True

    End Sub
    Private Sub oListControl_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        pnl_tlsp_Top.Visible = True
        TabControl1.Visible = True
        'pnl_tlsp_Top.Show()
    End Sub


    Private Sub btn_ClearTaxonomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearTaxonomy.Click
        txtTaxonomy.Clear()
    End Sub

    Private Sub Numeric_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtBMZip.KeyPress, txtBPracZIP.KeyPress
        'code to allow nos only 
        If Not ((e.KeyChar >= Convert.ToChar(48) AndAlso e.KeyChar <= Convert.ToChar(57)) OrElse e.KeyChar = Convert.ToChar(8)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub optMale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optMale.CheckedChanged
        If optMale.Checked Then
            optMale.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optMale.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optFemale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optFemale.CheckedChanged
        If optFemale.Checked Then
            optFemale.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optFemale.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
    '' END SUDHIR ''
    Private Sub mtxt_SSNno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mtxt_SSNno.Click

        ' added by sandip dhakane 20100726 when user click at any position on mask, cursor must go at start position
        If mtxt_SSNno.Text.Trim() = "" Then
            mtxt_SSNno.SelectionStart = 0
            mtxt_SSNno.SelectionLength = 0
        End If
    End Sub

    Private Sub mskTxtDEA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskTxtDEA.Click
        If mskTxtDEA.Text.Trim() = "" Then
            mskTxtDEA.SelectionStart = 0
            mskTxtDEA.SelectionLength = 0
        End If
    End Sub
    ' Added by Rahul Patel on 09-09-2010 
    ' For allowing only numeric value of Company NPI field
    Private Sub txtCompanyNPI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub

    ' Added by Rahul Patel on 09-09-2010 
    ' For allowing only numeric value of NPI field
    Private Sub txtNPI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNPI.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub
    ' Added by Rahul Patel on 09-09-2010 
    ' For allowing only numeric value of company tax id field
    Private Sub txtCompanyTaxID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub
    ' Added by Rahul Patel on 10-09-2010 
    ' For allowing only numeric value of Employee id field
    Private Sub txt_EmployerID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_EmployerID.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If


    End Sub

    Private Sub rbAlways_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAlways.CheckedChanged

        If rbAlways.Checked = True Then
            rbAlways.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbAlways.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub rbUsePlanSetting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUsePlanSetting.CheckedChanged

        If rbUsePlanSetting.Checked = True Then
            rbUsePlanSetting.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbUsePlanSetting.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub rbNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbNo.CheckedChanged

        If rbNo.Checked = True Then
            rbNo.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbNo.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub
#Region "Labs Id"
    Private Sub SaveEmdeonLabId(ByVal _ProviderId As Long, ByVal _LabId As String)

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim _sqlQuery As String = String.Empty
        Try
            oDB.Connect(False)

            _sqlQuery = "UPDATE Provider_MST SET sgloLabId= '" & _LabId.Replace("'", "''") & "' WHERE nProviderID = " & _ProviderId

            oDB.Execute_Query(_sqlQuery)
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If
            _sqlQuery = String.Empty
        End Try

        'Removed by madan on 20100419-- for using gloDB Layer...
        'Dim conn As New SqlConnection()
        'Dim objCmd As SqlCommand
        'Dim _strSQL As String = ""

        'Try
        '    conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        '    conn.Open()
        '    _strSQL = "UPDATE Provider_MST SET sgloLabId= '" & _LabId & "' WHERE nProviderID = " & _ProviderId
        '    objCmd = New SqlCommand(_strSQL, conn)
        '    objCmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    conn.Dispose()
        'End Try
    End Sub

    Private Function GetEmdeonLabId(ByVal _ProviderId As Long) As String

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oLabId As Object = Nothing

        Dim sLabId As String = String.Empty
        Dim _sqlQuery As String = String.Empty

        Try
            oDB.Connect(False)

            _sqlQuery = "select sgloLabId from Provider_MST where nProviderID = " & _ProviderId

            oLabId = oDB.ExecuteScalar_Query(_sqlQuery)

            If Not IsNothing(oLabId) Then
                If oLabId.ToString().Trim() = "" Then
                    sLabId = String.Empty
                Else
                    sLabId = CType(oLabId, String)
                End If
            End If
            oDB.Disconnect()

        Catch ex As Exception
            ' Log exceptions in AUDIT Trails implementations ....
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            sLabId = String.Empty
            _sqlQuery = String.Empty
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose()
            End If

            _sqlQuery = String.Empty
            oLabId = Nothing

        End Try
        Return sLabId
        'Removed by madan on 20100419-- for using gloDB Layer...
        'Dim conn As New SqlConnection()
        'Dim objCmd As SqlCommand
        'Dim sLabId As String = ""
        'Dim _strSQL As String = ""

        'Try
        '    conn.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        '    conn.Open()
        '    _strSQL = "select sgloLabId from Provider_MST where nProviderID = " & _ProviderId
        '    objCmd = New SqlCommand(_strSQL, conn)
        '    sLabId = Convert.ToString(objCmd.ExecuteScalar())
        '    Return sLabId
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return ""
        'Finally
        '    conn.Dispose()
        'End Try
        'End Removel
    End Function
#End Region

    Private Sub c1ProviderIdentification_StartEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProviderIdentification.StartEdit
        c1ProviderIdentification.Editor = CType(c1ProviderIdentification.Editor, TextBox)
    End Sub

    Private Sub c1ProviderIdentification_SetupEditor(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ProviderIdentification.SetupEditor
        If e.Col = 3 Then
            CType(c1ProviderIdentification.Editor, TextBox).MaxLength = 250
        End If
    End Sub

    Private Sub c1CompanyProvIdentification_SetupEditor(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        'c1CompanyProvIdentification.Editor = CType(c1CompanyProvIdentification.Editor, TextBox)
        If e.Col = 3 Then
            CType(c1CompanyProvIdentification.Editor, TextBox).MaxLength = 250
        End If
    End Sub

    Private Sub c1CompanyProvIdentification_StartEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        'If e.Col = 3 Then
        '    CType(c1CompanyProvIdentification.Editor, TextBox).MaxLength = 250
        'End If
        c1CompanyProvIdentification.Editor = CType(c1CompanyProvIdentification.Editor, TextBox)
    End Sub


    Private Sub c1CompanyProvIdentification_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ''gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Sub c1ProviderIdentification_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ProviderIdentification.MouseMove
        ''gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub


    Private Sub mskTrextBoxErrorMessageInvoked() Handles mskPLPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub TabControl1_Deselecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Deselecting
        e.Cancel = ValidationFailed
        ValidationFailed = False
    End Sub


    Private Sub mskPLFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub maskedPLPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedPLPhno.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBMPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBMPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBMPhoneNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBMPhoneNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBMFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBMFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskMobileNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMobileNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBIDPLPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBIDPLPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBIDPLFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBIDPLFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub maskedBIDPLPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedBIDPLPhno.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtCompanyPhone_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyPhone.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskCompanyFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskCompanyFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBPracPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBPracPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBPracFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBPracFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub maskedBpracPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedBpracPhno.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub btn_BrowseCmpTaxonomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_BrowseCmpTaxonomy.Click
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Taxonomy, False, Me.Width)
            oListControl.ClinicID = _nClinicID
            oListControl.ControlHeader = "Taxonomy"
            'tsb_SearchAppointment.Enabled = false;

            _CurrentControlType = gloListControl.gloListControlType.Taxonomy
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            pnl_tlsp_Top.Visible = False
            TabControl1.Visible = False
        End Try
    End Sub

    Private Sub btn_ClearCmpTaxonomy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearCmpTaxonomy.Click
        txtCmpTaxonomyCode.Clear()
    End Sub

    Private Sub cmbDoctorType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDoctorType.SelectedIndexChanged
        If Not IsNothing(cmbDoctorType.SelectedValue) Then
            If cmbDoctorType.SelectedValue.ToString = "1" Then
                chkRequire_Supervising_Provider_for_eRx.Enabled = True
            Else
                chkRequire_Supervising_Provider_for_eRx.CheckState = CheckState.Unchecked
                chkRequire_Supervising_Provider_for_eRx.Enabled = False
            End If
        End If
    End Sub

    Private Sub mskTrextBoxErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLPager.ErrorMessageInvoked

    End Sub
    Private Sub DesignProviderCompanyTab(ByVal _nNoofProviderCompanies As Int16)

        Try

            oClsProviderCompany = New ClsProviderCompany(gloPMAdmin.mdlGeneral.GetConnectionString())
            Me.TabControl1.TabPages.Add(tbpgPrvdrMultCmpny)

            ''Provider Multipal Company Address Control

            oProviderMultipalCompanyAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
            oProviderMultipalCompanyAddressContol.Dock = DockStyle.Fill
            pnlmltcompanymaillingAdd.Controls.Add(oProviderMultipalCompanyAddressContol)

            oProviderMultipalCompanyAddressContol.txtAreaCode.Visible = True
            oProviderMultipalCompanyAddressContol.txtZip.Size = New Size(43, 22)
            oProviderMultipalCompanyAddressContol.txtArea.Visible = True

            ''Provider Multipal Companies Physical Address Control 

            oProviderMultipalCompanyPhysicalAddressContol = New gloAddress.gloAddressControl(gloPMAdmin.mdlGeneral.GetConnectionString())
            oProviderMultipalCompanyPhysicalAddressContol.Dock = DockStyle.Fill
            pnlMltCompanyPhysAdd.Controls.Add(oProviderMultipalCompanyPhysicalAddressContol)

            oProviderMultipalCompanyPhysicalAddressContol.txtAreaCode.Visible = True
            oProviderMultipalCompanyPhysicalAddressContol.txtZip.Size = New Size(43, 22)
            oProviderMultipalCompanyPhysicalAddressContol.txtArea.Visible = True

            ''''Retrive Provider Multipla Company Data 
            If _nCloneFromProviderID > 0 AndAlso _bIsCloneProvider = True Then
                _dtProviderMultipleCompanyData = oClsProviderCompany.GetProviderCompany(_nCloneFromProviderID, _nNoofProviderCompanies)
                'For rowindex As Int16 = 0 To _dtProviderMultipleCompanyData.Rows.Count - 1
                '    _dtProviderMultipleCompanyData.Rows(rowindex)("nProviderID") = 0
                '    _dtProviderMultipleCompanyData.Rows(rowindex)("CompanyID") = 0
                '    _dtProviderMultipleCompanyData.AcceptChanges()
                'Next

            Else
                _dtProviderMultipleCompanyData = oClsProviderCompany.GetProviderCompany(_nProviderID, _nNoofProviderCompanies)
            End If

            _dtProviderMultipleCompanyRetrivedData = _dtProviderMultipleCompanyData

            _dsCompanyQualifierDetails.Clear()
            For rowIndex As Int16 = 0 To _dtProviderMultipleCompanyData.Rows.Count - 1
                Dim _dt As New DataTable
                Dim _dt1 As New DataTable
                _dt = Nothing

                If Convert.IsDBNull(_dtProviderMultipleCompanyData.Rows(rowIndex)("CompanyID")) Then
                    _dt = oClsProviderCompany.getProviderCompanyAdditionalQualifier(0, 0, _dtProviderMultipleCompanyData.Rows(rowIndex)(0))
                Else
                    If _dtProviderMultipleCompanyData.Rows(rowIndex)("CompanyID") > 0 Then
                        _dt = oClsProviderCompany.getProviderCompanyAdditionalQualifier(Convert.ToInt64(_dtProviderMultipleCompanyData.Rows(rowIndex)("nProviderID")), Convert.ToInt64(_dtProviderMultipleCompanyData.Rows(rowIndex)("CompanyID")), _dtProviderMultipleCompanyData.Rows(rowIndex)(0))
                    Else
                        _dt = oClsProviderCompany.getProviderCompanyAdditionalQualifier(0, 0, _dtProviderMultipleCompanyData.Rows(rowIndex)(0))
                    End If

                End If

                _dt.AcceptChanges()
                _dt1 = _dt.Copy()
                _dt1.TableName = _dtProviderMultipleCompanyData.Rows(rowIndex)(1)
                _dt.Clear()
                '' If _dt1 IsNot Nothing And _dt1.Rows.Count > 0 Then

                _dsCompanyQualifierDetails.Tables.Add(_dt1)
                _dt1 = Nothing
                '' End If

            Next

            'Clear the Provider and Company ID's 
            If _nCloneFromProviderID > 0 AndAlso _bIsCloneProvider = True Then

                '_dtProviderMultipleCompanyData
                If _dtProviderMultipleCompanyData IsNot Nothing Then

                    For rowindex As Int16 = 0 To _dtProviderMultipleCompanyData.Rows.Count - 1
                        _dtProviderMultipleCompanyData.Rows(rowindex)("nProviderID") = 0
                        _dtProviderMultipleCompanyData.Rows(rowindex)("CompanyID") = 0
                        _dtProviderMultipleCompanyData.AcceptChanges()
                    Next

                End If

                '_dtProviderMultipleCompanyRetrivedData
                If _dtProviderMultipleCompanyRetrivedData IsNot Nothing Then

                    For rowindex As Int16 = 0 To _dtProviderMultipleCompanyRetrivedData.Rows.Count - 1
                        _dtProviderMultipleCompanyRetrivedData.Rows(rowindex)("nProviderID") = 0
                        _dtProviderMultipleCompanyRetrivedData.Rows(rowindex)("CompanyID") = 0
                        _dtProviderMultipleCompanyRetrivedData.AcceptChanges()
                    Next

                End If

                '_dsCompanyQualifierDetails
                If _dsCompanyQualifierDetails IsNot Nothing AndAlso _dsCompanyQualifierDetails.Tables IsNot Nothing Then

                    For tableindex As Int16 = 0 To _dsCompanyQualifierDetails.Tables.Count - 1

                        If _dsCompanyQualifierDetails.Tables(tableindex) IsNot Nothing Then

                            For rowindex As Int16 = 0 To _dsCompanyQualifierDetails.Tables(tableindex).Rows.Count - 1
                                _dsCompanyQualifierDetails.Tables(tableindex).Rows(rowindex)("nProviderID") = 0
                                _dsCompanyQualifierDetails.Tables(tableindex).Rows(rowindex)("nProviderCompanyID") = 0
                                _dsCompanyQualifierDetails.Tables(tableindex).AcceptChanges()
                            Next

                        End If

                    Next

                End If

            End If

            fillProviderCompanyCombo(_dtProviderMultipleCompanyData)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub fillProviderCompanyCombo(ByVal dtProviderMultipleCompanyData As DataTable)
        If dtProviderMultipleCompanyData IsNot Nothing And dtProviderMultipleCompanyData.Rows.Count > 0 Then

            CmbProviderCompany.DataSource = dtProviderMultipleCompanyData.Copy()
            CmbProviderCompany.DisplayMember = "CompanyIndex"
            CmbProviderCompany.ValueMember = "CompanyID"
            CmbProviderCompany.Update()
            CmbProviderCompany.Refresh()
            CmbProviderCompany.SelectedIndex = 0
        Else
            Dim _dtQual As New DataTable
            Dim dtTempProviderCompany As DataTable
            Dim dr As DataRow
            oClsProviderCompany = New ClsProviderCompany(mdlGeneral.GetConnectionString)
            dtTempProviderCompany = dtProviderMultipleCompanyData.Clone()

            For value As Integer = 2 To _nNoOfProviderCompanies

                '''Create Temp DataTable For Companies'
                dr = dtTempProviderCompany.NewRow()
                dr("CompanyIndex") = "ProviderCompany " + value.ToString()
                dtTempProviderCompany.Rows.Add(dr)
                dtTempProviderCompany.AcceptChanges()
                dtTempProviderCompany.Rows(value - 1)("nCompanyIndex") = value
                dtTempProviderCompany.Rows(value - 1)("nProviderID") = value
                dtTempProviderCompany.AcceptChanges()

                '''End Create Temp DataTable For Companies'


                ''''Create Temp DataTable for AdditionalQaul
                _dtProviderOtherIDsQaulifier = oClsProviderCompany.getProviderCompanyAdditionalQualifier(0, 0, value)
                _dtProviderOtherIDsQaulifier.AcceptChanges()
                _dtQual = _dtProviderOtherIDsQaulifier.Copy()
                _dtQual.TableName = "ProviderCompany " + value.ToString()
                _dtProviderOtherIDsQaulifier.Clear()
                If _dtQual IsNot Nothing And _dtQual.Rows.Count > 0 Then
                    _dsCompanyQualifierDetails.Tables.Add(_dtQual)
                    _dtQual = Nothing
                End If
                _dsCompanyQualifierDetails.AcceptChanges()

                ''''END Create Temp DataTable for AdditionalQaul


            Next


            If dtTempProviderCompany IsNot Nothing And dtTempProviderCompany.Rows.Count > 0 Then
                _dtProviderMultipleCompanyData = dtTempProviderCompany
                CmbProviderCompany.DataSource = dtTempProviderCompany.Copy()
                CmbProviderCompany.DisplayMember = "CompanyIndex"
                CmbProviderCompany.ValueMember = "CompanyID"
                CmbProviderCompany.Update()
                CmbProviderCompany.Refresh()
                CmbProviderCompany.SelectedIndex = 0
            End If


        End If
    End Sub

    Private Sub DesignCompanyAdditionalGrid(ByVal dtQualifierID As DataTable)

        ''Design Company Additional Billing IDs Grid 
        c1MltCompantAddtnlID.Rows.Fixed = 1
        c1MltCompantAddtnlID.Cols.Fixed = 0
        c1MltCompantAddtnlID.Rows.Count = 1
        c1MltCompantAddtnlID.Cols.Count = 5

        c1MltCompantAddtnlID.SetData(0, 0, "MainID") ''ID
        c1MltCompantAddtnlID.SetData(0, 1, "Code")
        c1MltCompantAddtnlID.SetData(0, 2, "ID") ''Description
        c1MltCompantAddtnlID.SetData(0, 3, "Value")
        c1MltCompantAddtnlID.SetData(0, 4, "bIsSystem")

        'c1MltCompantAddtnlID.SetData(1, 0, "Medicare ID")
        'c1MltCompantAddtnlID.SetData(1, 1, "")
        c1MltCompantAddtnlID.Cols(0).DataType = GetType(System.Int64)
        c1MltCompantAddtnlID.Cols(1).DataType = GetType(System.String)
        c1MltCompantAddtnlID.Cols(2).DataType = GetType(System.String)
        c1MltCompantAddtnlID.Cols(3).DataType = GetType(System.String)
        c1MltCompantAddtnlID.Cols(4).DataType = GetType(System.Boolean)
        'c1MltCompantAddtnlID.SetData(2, 0, "Medicaid ID")
        'c1MltCompantAddtnlID.SetData(2, 1, "")

        Dim _width As Int32 = Panel23.Width
        c1MltCompantAddtnlID.Cols(0).Width = 0
        c1MltCompantAddtnlID.Cols(1).Width = Convert.ToInt32(_width * 0.2)
        c1MltCompantAddtnlID.Cols(2).Width = Convert.ToInt32(_width * 0.5)
        c1MltCompantAddtnlID.Cols(3).Width = Convert.ToInt32(_width * 0.42)
        c1MltCompantAddtnlID.Cols(4).Width = 0

        c1MltCompantAddtnlID.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1MltCompantAddtnlID.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1MltCompantAddtnlID.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        c1MltCompantAddtnlID.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

        c1MltCompantAddtnlID.Cols(0).AllowEditing = False
        c1MltCompantAddtnlID.Cols(1).AllowEditing = False
        c1MltCompantAddtnlID.Cols(2).AllowEditing = False
        c1MltCompantAddtnlID.Cols(3).AllowEditing = True
        c1MltCompantAddtnlID.Cols(4).AllowEditing = False

        c1MltCompantAddtnlID.Cols(0).Visible = False
        c1MltCompantAddtnlID.Cols(1).Visible = False
        c1MltCompantAddtnlID.Cols(2).Visible = True
        c1MltCompantAddtnlID.Cols(3).Visible = True
        c1MltCompantAddtnlID.Cols(4).Visible = False

        If dtQualifierID IsNot Nothing AndAlso dtQualifierID.Rows.Count > 0 Then
            For i As Integer = 0 To dtQualifierID.Rows.Count - 1
                c1MltCompantAddtnlID.Rows.Add()
                Dim RowIndex As Int32 = c1MltCompantAddtnlID.Rows.Count - 1
                c1MltCompantAddtnlID.SetData(RowIndex, 0, Convert.ToString(dtQualifierID.Rows(i)("nQualifierID")))
                c1MltCompantAddtnlID.SetData(RowIndex, 1, Convert.ToString(dtQualifierID.Rows(i)("sCode")))
                c1MltCompantAddtnlID.SetData(RowIndex, 2, Convert.ToString(dtQualifierID.Rows(i)("sAdditionalDescription")))
                c1MltCompantAddtnlID.SetData(RowIndex, 3, Convert.ToString(dtQualifierID.Rows(i)("sValue")))
                c1MltCompantAddtnlID.SetData(RowIndex, 4, Convert.ToString(dtQualifierID.Rows(i)("bIsSystem")))
            Next
        End If
    End Sub

    Private Sub CmbProviderCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbProviderCompany.SelectedIndexChanged
        Dim nSelectedIndex As Int16
        nSelectedIndex = CmbProviderCompany.SelectedIndex
        chkmltSameAsPrvdrAdd.Checked = False


        If _dtProviderMultipleCompanyData IsNot Nothing And _dtProviderMultipleCompanyData.Rows.Count > nSelectedIndex And nSelectedIndex >= 0 Then
            TxtMltCompanyName.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sProviderOtherCompanyName"))

            '''Provider Multiple Company Multiple Bussiness Mailling Address
            txtmltCompanyContact.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sContactName"))

            oProviderMultipalCompanyAddressContol.isFormLoading = True
            oProviderMultipalCompanyAddressContol.txtAddress1.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAddressline1"))
            oProviderMultipalCompanyAddressContol.txtAddress2.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAddressline2"))
            oProviderMultipalCompanyAddressContol.txtCity.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCity"))
            oProviderMultipalCompanyAddressContol.cmbState.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sState"))
            oProviderMultipalCompanyAddressContol.txtZip.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sZIP"))
            oProviderMultipalCompanyAddressContol.txtAreaCode.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAreaCode"))
            oProviderMultipalCompanyAddressContol.cmbCountry.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCountry"))
            oProviderMultipalCompanyAddressContol.txtCounty.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCounty"))
            oProviderMultipalCompanyAddressContol.isFormLoading = False

            mskmltCompanyPhoneNo.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sPhone"))
            txtMltCompanyNPI.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sNPI"))
            mskMltCompanyFax.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sFax"))
            txtMltCompanyTaxId.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sTaxID"))
            txtMltCompanyEmail.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sEmail"))
            txtMltCompanyTaxonomy.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyTaxonomyCode"))


            '''Provider Multiple Company Multiple Physical  Address
            txtMltCompanyPhysContactName.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sProviderPhyCompanyContactName"))

            oProviderMultipalCompanyPhysicalAddressContol.isFormLoading = True
            oProviderMultipalCompanyPhysicalAddressContol.txtAddress1.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAddressline1"))
            oProviderMultipalCompanyPhysicalAddressContol.txtAddress2.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAddressline2"))
            oProviderMultipalCompanyPhysicalAddressContol.txtCity.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCity"))
            oProviderMultipalCompanyPhysicalAddressContol.cmbState.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalState"))
            oProviderMultipalCompanyPhysicalAddressContol.txtZip.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalZIP"))
            oProviderMultipalCompanyPhysicalAddressContol.txtAreaCode.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAreaCode"))
            oProviderMultipalCompanyPhysicalAddressContol.cmbCountry.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCountry"))
            oProviderMultipalCompanyPhysicalAddressContol.txtCounty.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCounty"))
            oProviderMultipalCompanyPhysicalAddressContol.isFormLoading = False

            mskMltCompanyPhys.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalPhoneNo"))
            mskMltCompanyPhysPager.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalPagerNo"))
            mskMltCompanyPhysFax.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalFAX"))
            txtMltCompanyPhysMail.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalEmail"))
            txtMltCompanyPhysURL.Text = Convert.ToString(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalURL"))

        End If

        If nSelectedIndex >= 0 And _dsCompanyQualifierDetails.Tables.Count > nSelectedIndex Then
            DesignCompanyAdditionalGrid(_dsCompanyQualifierDetails.Tables(nSelectedIndex))
        End If
    End Sub

    Private Sub CmbProviderCompany_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles CmbProviderCompany.MouseClick
        Dim nSelectedIndex As Int16
        nSelectedIndex = CmbProviderCompany.SelectedIndex
        If _dtProviderMultipleCompanyData IsNot Nothing And _dtProviderMultipleCompanyData.Rows.Count > nSelectedIndex Then

            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("nCompanyIndex") = nSelectedIndex + 2
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("nProviderID") = _nProviderID
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sProviderOtherCompanyName") = TxtMltCompanyName.Text

            '''Provider Multiple Company Multiple Bussiness Mailling Address
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sContactName") = txtmltCompanyContact.Text

            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAddressline1") = oProviderMultipalCompanyAddressContol.txtAddress1.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAddressline2") = oProviderMultipalCompanyAddressContol.txtAddress2.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCity") = oProviderMultipalCompanyAddressContol.txtCity.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sState") = oProviderMultipalCompanyAddressContol.cmbState.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sZIP") = oProviderMultipalCompanyAddressContol.txtZip.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sAreaCode") = oProviderMultipalCompanyAddressContol.txtAreaCode.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCountry") = oProviderMultipalCompanyAddressContol.cmbCountry.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCounty") = oProviderMultipalCompanyAddressContol.txtCounty.Text


            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sPhone") = mskmltCompanyPhoneNo.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sNPI") = txtMltCompanyNPI.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sFax") = mskMltCompanyFax.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sTaxID") = txtMltCompanyTaxId.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sEmail") = txtMltCompanyEmail.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyTaxonomyCode") = txtMltCompanyTaxonomy.Text


            '''Provider Multiple Company Multiple Physical  Address
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sProviderPhyCompanyContactName") = txtMltCompanyPhysContactName.Text


            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAddressline1") = oProviderMultipalCompanyPhysicalAddressContol.txtAddress1.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAddressline2") = oProviderMultipalCompanyPhysicalAddressContol.txtAddress2.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCity") = oProviderMultipalCompanyPhysicalAddressContol.txtCity.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalState") = oProviderMultipalCompanyPhysicalAddressContol.cmbState.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalZIP") = oProviderMultipalCompanyPhysicalAddressContol.txtZip.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalAreaCode") = oProviderMultipalCompanyPhysicalAddressContol.txtAreaCode.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCountry") = oProviderMultipalCompanyPhysicalAddressContol.cmbCountry.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalCounty") = oProviderMultipalCompanyPhysicalAddressContol.txtCounty.Text


            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalPhoneNo") = mskMltCompanyPhys.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalPagerNo") = mskMltCompanyPhysPager.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalFAX") = mskMltCompanyPhysFax.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalEmail") = txtMltCompanyPhysMail.Text
            _dtProviderMultipleCompanyData.Rows(nSelectedIndex)("sCompanyPhysicalURL") = txtMltCompanyPhysURL.Text
        End If

        If _dtProviderMultipleCompanyData IsNot Nothing And _dtProviderMultipleCompanyData.Rows.Count > nSelectedIndex Then

            If c1MltCompantAddtnlID.Rows.Count > 0 Then

                c1MltCompantAddtnlID.FinishEditing()

                For Rowindex As Integer = 1 To c1MltCompantAddtnlID.Rows.Count - 1
                    If Convert.ToString(c1MltCompantAddtnlID.GetData(Rowindex, 3)) <> "" Then

                        If Not Convert.IsDBNull(c1MltCompantAddtnlID.GetData(Rowindex, 3)) Then
                            _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("sValue") = Convert.ToString(c1MltCompantAddtnlID.GetData(Rowindex, 3))
                        Else
                            _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("sValue") = ""
                        End If

                        _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("nProviderID") = _nProviderID

                        If Not Convert.IsDBNull(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("CompanyID")) Then
                            _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("nProviderCompanyID") = Convert.ToInt64(_dtProviderMultipleCompanyData.Rows(nSelectedIndex)("CompanyID"))
                        Else
                            _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("nProviderCompanyID") = 0
                        End If

                    Else
                        _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("sValue") = ""
                        _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("nProviderID") = 0
                        _dsCompanyQualifierDetails.Tables(nSelectedIndex).Rows(Rowindex - 1)("nProviderCompanyID") = 0

                    End If
                Next
            End If
            _dsCompanyQualifierDetails.AcceptChanges()
        End If

        'If (ogloProviderCompanyControl.oProviderCompanyPhysicalAddressContol.cmbCountry.Text.Trim().ToUpper() = "US") Then
        '    ogloProviderCompanyControl.ProviderCompany.CompanyPhysicalCounty = ogloProviderCompanyControl.oProviderCompanyPhysicalAddressContol.txtCounty.Text.Trim()
        'Else
        '    oProvider.CompanyPhysicalCounty = ""
        ''End If
    End Sub
    Private Sub oListControl_ProviderOtherCompanyItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        If _CurrentControlType = gloListControl.gloListControlType.Taxonomy Then

            txtMltCompanyTaxonomy.Clear()
            txtCmpTaxonomyCode.Text = ""
            If oListControl.SelectedItems.Count > 0 Then

                txtMltCompanyTaxonomy.Text = (oListControl.SelectedItems(0).Code.ToString() & "-") + oListControl.SelectedItems(0).Description.ToString()

            End If
        Else
            txtMltCompanyTaxonomy.Clear()

            If oListControl.SelectedItems.Count > 0 Then

                txtMltCompanyTaxonomy.Text = (oListControl.SelectedItems(0).Code.ToString() & "-") + oListControl.SelectedItems(0).Description.ToString()

            End If
        End If

        pnl_tlsp_Top.Visible = True
        TabControl1.Visible = True

    End Sub
    Private Sub oListControl_ProviderOtherCompanyItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        pnl_tlsp_Top.Visible = True
        TabControl1.Visible = True
        'pnl_tlsp_Top.Show()
    End Sub


    Private Sub btnTaxonomySelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaxonomySelect.Click
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Taxonomy, False, Me.Width)
            oListControl.ClinicID = _nClinicID
            oListControl.ControlHeader = "Taxonomy"
            'tsb_SearchAppointment.Enabled = false;

            _CurrentControlType = gloListControl.gloListControlType.Taxonomy
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ProviderOtherCompanyItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ProviderOtherCompanyItemClosedClick

            Me.Controls.Add(oListControl)


            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            pnl_tlsp_Top.Visible = False
            TabControl1.Visible = False
        End Try
    End Sub

    Private Sub btnTaxonomyClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTaxonomyClear.Click
        txtMltCompanyTaxonomy.Clear()
    End Sub

    Private Sub chkmltSameAsPrvdrAdd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmltSameAsPrvdrAdd.CheckedChanged
        If chkmltSameAsPrvdrAdd.Checked = True Then

            txtmltCompanyContact.Text = txtBMContact.Text



            oProviderMultipalCompanyAddressContol.isFormLoading = True
            oProviderMultipalCompanyAddressContol.txtAddress1.Text = oBussinessAddressContol.txtAddress1.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtAddress2.Text = oBussinessAddressContol.txtAddress2.Text.Trim()
            oProviderMultipalCompanyAddressContol.cmbState.Text = oBussinessAddressContol.cmbState.Text
            'oCompanyAddressContol.cmbState.SelectedItem = txtBMstate.Text.Trim()
            oProviderMultipalCompanyAddressContol.cmbCountry.SelectedIndex = oBussinessAddressContol.cmbCountry.SelectedIndex
            oProviderMultipalCompanyAddressContol.txtCity.Text = oBussinessAddressContol.txtCity.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtZip.Text = oBussinessAddressContol.txtZip.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtArea.Text = oBussinessAddressContol.txtArea.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtAreaCode.Text = oBussinessAddressContol.txtAreaCode.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtCounty.Text = oBussinessAddressContol.txtCounty.Text
            oProviderMultipalCompanyAddressContol.isFormLoading = False

            mskmltCompanyPhoneNo.Text = mskBMPhoneNo.Text
            mskMltCompanyFax.Text = mskBMFax.Text.Trim()
            txtMltCompanyEmail.Text = txtBMEmailAddress.Text.Trim()

        Else

            txtmltCompanyContact.Text = ""

            oProviderMultipalCompanyAddressContol.isFormLoading = True
            oProviderMultipalCompanyAddressContol.txtAddress1.Text = ""
            oProviderMultipalCompanyAddressContol.txtAddress2.Text = ""
            oProviderMultipalCompanyAddressContol.cmbState.Text = ""
            'oCompanyAddressContol.cmbState.SelectedItem = txtBMstate.Text.Trim()
            oProviderMultipalCompanyAddressContol.txtCity.Text = ""
            oProviderMultipalCompanyAddressContol.txtZip.Text = ""
            oProviderMultipalCompanyAddressContol.txtArea.Text = ""
            oProviderMultipalCompanyAddressContol.txtAreaCode.Text = ""
            oProviderMultipalCompanyAddressContol.txtCounty.Text = ""
            oProviderMultipalCompanyAddressContol.isFormLoading = False

            mskmltCompanyPhoneNo.Text = ""
            mskMltCompanyFax.Text = ""
            txtMltCompanyEmail.Text = ""
        End If
    End Sub

    Private Sub txtMltCompanyEmail_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMltCompanyEmail.Validating
        If (txtMltCompanyEmail.Text.Trim() <> "") Then
            If CheckEmailAddress(txtMltCompanyEmail.Text.Trim()) = False Then
                MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ValidationFailed = True
                txtMltCompanyEmail.Focus()
            End If
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub txtMltCompanyPhysMail_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMltCompanyPhysMail.Validating
        If (txtMltCompanyPhysMail.Text.Trim() <> "") Then
            If CheckEmailAddress(txtMltCompanyPhysMail.Text.Trim()) = False Then
                MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ValidationFailed = True
                txtMltCompanyPhysMail.Focus()
            End If
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub txtMltCompanyPhysURL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMltCompanyPhysURL.Validating

        If (txtMltCompanyPhysURL.Text.Trim() <> "") Then
            If CheckURLAddress(txtMltCompanyPhysURL.Text.Trim()) = False Then
                MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ValidationFailed = True
                txtMltCompanyPhysURL.Focus()
            End If
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub mskmltCompanyPhoneNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskmltCompanyPhoneNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskMltCompanyFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMltCompanyFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskMltCompanyPhysPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMltCompanyPhysPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskMltCompanyPhys_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMltCompanyPhys.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskMltCompanyPhysFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskMltCompanyPhysFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

#Region "License Update"
    Private Sub btnLicenseRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnLicenseRefresh.Click
        Try
            If btnLicenseRefresh.Tag <> "valid" Then
                If Trim(txtFirstName.Text) = "" Then
                    MessageBox.Show("Please enter First Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtFirstName.Focus()
                    Exit Sub
                End If
                If Trim(txtLastName.Text) = "" Then
                    MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtLastName.Focus()
                    Exit Sub
                End If
                If Trim(gstrClinicExternalCode) = "" Then
                    MessageBox.Show("Please enter AUS User Name in clinic master.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                ReffreshLicense(True)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Status, "License Refresh. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub ReffreshLicense(ByVal bViewMessage As Boolean)
        Try

            Dim oProvider As New clsProvider(gstrConnectionString)
            Me.Cursor = Cursors.WaitCursor

            Dim ausmessage As String = String.Empty

            Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                ausmessage = oLicense.ValidateLicenseKey(txtLicenseKey.Text.Trim, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, gstrClinicExternalCode, _nProviderID, _nAUSPortalID)
            End Using

            Me.Cursor = Cursors.Default

            If ausmessage <> "" Then
                If ausmessage <> "ok" And Trim(txtLicenseKey.Text) <> "" Then
                    btnLicenseRefresh.Tag = "invalid"
                    btnLicenseRefresh.ImageIndex = 1
                    btnLicenseRefresh.Enabled = True
                    txtLicenseKey.ReadOnly = False
                    If bViewMessage Then
                        MessageBox.Show(ausmessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    txtLicenseKey.Focus()
                    Exit Sub
                Else
                    If Trim(txtLicenseKey.Text) = "" Then
                        If ausmessage.Trim <> "not found" Then
                            txtLicenseKey.Text = ausmessage
                        Else
                            If bViewMessage Then
                                MessageBox.Show("Provider not registered on TRIARQ Health network.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                            Exit Sub
                        End If
                    End If
                    btnLicenseRefresh.Tag = "valid"
                    btnLicenseRefresh.ImageIndex = 0
                    ' btnLicenseRefresh.Enabled = False
                    txtLicenseKey.ReadOnly = True

                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                        _nAUSPortalID = oLicense.GetAUSPortalID(txtLicenseKey.Text)
                    End Using

                End If
            Else
                MessageBox.Show("Error while validating license key.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtLicenseKey.Text = ""
            End If
            oProvider = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Status, "License Refresh. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub SetDemoLicenseMode(ByVal sNPI As String)
        Dim oProvider As New clsProvider(gstrConnectionString)
        Try
            If sNPI.Trim <> "" Then
                '' ISDEMOLicensce = oProvider.IsDemoNPI(sNPI)
                Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                    ISDEMOLicensce = oLicense.IsDemoNPI(sNPI, gstrDemoNPIs)
                End Using

                If ISDEMOLicensce = True Then
                    txtLicenseKey.Text = "DEMO LICENSE"
                Else
                    If txtLicenseKey.Text.Trim.ToUpper = "DEMO LICENSE" Then
                        txtLicenseKey.Text = ""
                    End If
                End If
            Else
                ISDEMOLicensce = False
            End If
        Catch ex As Exception
            MessageBox.Show("SetDemoLicenseMode :" & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oProvider = Nothing
        End Try
    End Sub
    Public Function ISProviderDataChange(ByVal TempProviders As gloAUSLibrary.Class.TempProviderdata, ByVal Providers As clsProvider) As Boolean
        Try
            If TempProviders.FirstName.Trim <> Providers.FirstName.Trim Then
                Return True
            End If
            If TempProviders.MiddleName.Trim <> Providers.MiddleName.Trim Then
                Return True
            End If
            If TempProviders.LastName.Trim <> Providers.LastName.Trim Then
                Return True
            End If
            If TempProviders.NPI.Trim <> Providers.NPI.Trim Then
                Return True
            End If
            If TempProviders.BMAddress1.Trim <> Providers.BMAddress1.Trim Then
                Return True
            End If
            If TempProviders.BMAddress2.Trim <> Providers.BMAddress2.Trim Then
                Return True
            End If
            If TempProviders.BMCity.Trim <> Providers.BMCity.Trim Then
                Return True
            End If
            If TempProviders.BMState.Trim <> Providers.BMState.Trim Then
                Return True
            End If
            If TempProviders.BMZIP.Trim <> Providers.BMZIP.Trim Then
                Return True
            End If
            
            'If TempProviders.licenseKey.Trim <> Providers.LicenceKey.Trim Then
            '    Return True
            'End If
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Status, "ISProviderDataChange. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Function

#End Region

    Private Sub LoadProviderForClone(ByVal ProviderID As Int64)

        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        oProvider.GetProvider(ProviderID)
        TempProvider = New gloAUSLibrary.Class.TempProviderdata
        Try

            'Panel6.Enabled = False

            'Fill Provider
            txtPrefix.Text = "" 'oProvider.Prefix
            txtFirstName.Text = "" ' oProvider.FirstName
            txtMiddleName.Text = "" ' oProvider.MiddleName
            txtLastName.Text = "" ' oProvider.LastName
            txtSuffix.Text = "" ' oProvider.Suffix

            txtBMContact.Text = oProvider.BMContactName
            'If Not IsNothing(oProvider.dtDOB) AndAlso oProvider.dtDOB <> "#12:00:00 AM#" Then
            '    mtxtDOB.Text = oProvider.dtDOB.ToString("MM/dd/yyyy")
            'End If

            oBussinessAddressContol.isFormLoading = True
            oBussinessAddressContol.txtAddress1.Text = oProvider.BMAddress1         ''Add1
            oBussinessAddressContol.txtAddress2.Text = oProvider.BMAddress2         ''Add2
            oBussinessAddressContol.txtCity.Text = oProvider.BMCity                 ''city
            oBussinessAddressContol.txtZip.Text = oProvider.BMZIP                   ''Zip

            oBussinessAddressContol.cmbCountry.Text = oProvider.BMCountry         ''Country
            oBussinessAddressContol.txtCounty.Text = oProvider.BMCounty           ''county
            oBussinessAddressContol.txtAreaCode.Text = oProvider.BMAreaCode       ''AreaCode
            oBussinessAddressContol.cmbState.Text = oProvider.BMState               ''state
            oBussinessAddressContol.isFormLoading = False

            txtBPracContactName.Text = oProvider.BPracContactName

            oPracticeAddressContol.isFormLoading = True
            oPracticeAddressContol.txtAddress1.Text = oProvider.BPracAddress1          ''Add1
            oPracticeAddressContol.txtAddress2.Text = oProvider.BPracAddress2         ''Add2
            oPracticeAddressContol.txtCity.Text = oProvider.BPracCity                  ''city
            oPracticeAddressContol.txtZip.Text = oProvider.BPracZIP                    ''Zip

            oPracticeAddressContol.cmbCountry.Text = oProvider.BPracCountry         ''Country
            oPracticeAddressContol.txtCounty.Text = oProvider.BPracCounty           ''County
            oPracticeAddressContol.txtAreaCode.Text = oProvider.BPracAreaCode       ''AreaCode
            oPracticeAddressContol.cmbState.Text = oProvider.BPracState                ''state
            oPracticeAddressContol.isFormLoading = False

            mskMobileNo.Text = oProvider.Mobile
            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID

            ''Start' Lab Id
            'If _nCloneFromProviderID > 0 Then

            '    Dim _eLabId As String = String.Empty
            '    _eLabId = GetEmdeonLabId(_nCloneFromProviderID)

            '    If _eLabId <> "" Then
            '        'Set value read for a lab id against a provider.
            '        txtLabId.Text = _eLabId
            '    End If

            'End If
            ''End' Lab Id

            cmbDoctorType.Text = oProvider.Description
            If oProvider.Taxonomy <> "" Then
                txtTaxonomy.Text = (oProvider.Taxonomy & "-") + oProvider.TaxonomyDesc
            End If
            mtxt_SSNno.Text = "" 'oProvider.SSNno
            txt_EmployerID.Text = oProvider.EmployerID

            mskTxtDEA.Text = "" 'oProvider.DEA

            If oProvider.Gender = "Male" Then
                optMale.Checked = True
                'if
            ElseIf oProvider.Gender = "Female" Then
                optFemale.Checked = True
            End If
            'else
            mskBMPhoneNo.Text = oProvider.BMPhone
            txtBMEmailAddress.Text = oProvider.BMEmail
            mskBMFax.Text = oProvider.BMFAX
            mskBMPager.Text = oProvider.BMPager
            txtBMURL.Text = oProvider.BMURL

            lblDirectAddressValue.Text = oProvider.DirectAddress  ''Direct Address

            maskedBpracPhno.Text = oProvider.BPracPhone
            txtBPracEMail.Text = oProvider.BPracEmail
            mskBPracFax.Text = oProvider.BPracFAX
            mskBPracPager.Text = oProvider.BPracPager
            txtBPracUrl.Text = oProvider.BPracURL

            txtCompanyName.Text = oProvider.ComapanyName
            txtCompanyContactName.Text = oProvider.ComapanyContactName
            'txtCompanyAddress1.Text = oProvider.ComapanyAddress1
            'txtCompanyAddress2.Text = oProvider.ComapanyAddress2
            'txtCompanyCity.Text = oProvider.ComapanyCity
            'txtCompanyState.Text = oProvider.ComapanyState
            'txtCompanyZip.Text = oProvider.ComapanyZip

            oCompanyAddressContol.isFormLoading = True
            oCompanyAddressContol.txtAddress1.Text = oProvider.ComapanyAddress1          ''Add1
            oCompanyAddressContol.txtAddress2.Text = oProvider.ComapanyAddress2          ''Add2
            oCompanyAddressContol.txtCity.Text = oProvider.ComapanyCity                   ''city
            oCompanyAddressContol.txtZip.Text = oProvider.ComapanyZip                     ''Zip

            oCompanyAddressContol.cmbCountry.Text = oProvider.ComapanyCountry            ''Country
            oCompanyAddressContol.txtCounty.Text = oProvider.ComapanyCounty             ''county
            oCompanyAddressContol.txtAreaCode.Text = oProvider.ComapanyAreaCode         ''Area Code
            oCompanyAddressContol.cmbState.Text = oProvider.ComapanyState                 ''state
            'Added By Rahul Patel on 10-09-2010
            'For not working zip control problem.
            oCompanyAddressContol.isFormLoading = False

            txtCompanyPhone.Text = oProvider.ComapanyPhone
            mskCompanyFax.Text = oProvider.ComapanyFax
            txtCompanyEmail.Text = oProvider.ComapanyEmail

            'Added by Anil on 20090310
            txtCompanyNPI.Text = oProvider.ComapanyNPI
            txtCompanyTaxID.Text = oProvider.ComapanyTaxID
            txtCmpTaxonomyCode.Text = oProvider.CompanyTaxonomyCode

            mskTxtUPIN.Text = "" 'oProvider.UPIN
            txtNPI.Text = "" 'oProvider.NPI
            txtExternalCode.Text = "" 'oProvider.ExternalCode '' External Code
            txtStateMedicalLicenseNo.Text = "" 'oProvider.StateMedicalNo

            'Sanjog - Added on 2011 May 23 for DPS No.
            txtDPSNumber.Text = "" 'oProvider.DPSNumber
            'Sanjog - Added on 2011 May 23 for DPS No.
            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID


            'If oProvider.Signature IsNot Nothing Then
            '    picSignature.Image = oProvider.Signature
            '    ImgWidth = oProvider.Signature.Width
            '    picSignature.SizeMode = PictureBoxSizeMode.CenterImage
            '    picSignature.Visible = True
            'End If

            txtBIDPLContactName.Text = oProvider.PhysicalAddContactName
            oProviderPhysicalAddressContol.isFormLoading = True
            oProviderPhysicalAddressContol.txtAddress1.Text = oProvider.PhysicalAddressline1
            oProviderPhysicalAddressContol.txtAddress2.Text = oProvider.PhysicalAddressline2
            oProviderPhysicalAddressContol.txtCity.Text = oProvider.PhysicalCity

            oProviderPhysicalAddressContol.txtZip.Text = oProvider.PhysicalZIP
            oProviderPhysicalAddressContol.txtAreaCode.Text = oProvider.PhysicalAreaCode
            oProviderPhysicalAddressContol.cmbCountry.Text = oProvider.PhysicalCountry
            oProviderPhysicalAddressContol.txtCounty.Text = oProvider.PhysicalCounty
            oProviderPhysicalAddressContol.cmbState.Text = oProvider.PhysicalState
            oProviderPhysicalAddressContol.isFormLoading = False
            mskBIDPLPager.Text = oProvider.PhysicalPagerNo
            maskedBIDPLPhno.Text = oProvider.PhysicalPhoneNo
            mskBIDPLFax.Text = oProvider.PhysicalFAX
            txtBIDPLEMail.Text = oProvider.PhysicalEmail
            txtBIDPLUrl.Text = oProvider.PhysicalURL


            txtPLContactName.Text = oProvider.CompanyPhysicalAddContactName
            oProviderCompanyPhysicalAddressContol.isFormLoading = True
            oProviderCompanyPhysicalAddressContol.txtAddress1.Text = oProvider.CompanyPhysicalAddressline1
            oProviderCompanyPhysicalAddressContol.txtAddress2.Text = oProvider.CompanyPhysicalAddressline2
            oProviderCompanyPhysicalAddressContol.txtCity.Text = oProvider.CompanyPhysicalCity

            oProviderCompanyPhysicalAddressContol.txtZip.Text = oProvider.CompanyPhysicalZIP
            oProviderCompanyPhysicalAddressContol.txtAreaCode.Text = oProvider.CompanyPhysicalAreaCode
            oProviderCompanyPhysicalAddressContol.cmbCountry.Text = oProvider.CompanyPhysicalCountry
            oProviderCompanyPhysicalAddressContol.txtCounty.Text = oProvider.CompanyPhysicalCounty
            oProviderCompanyPhysicalAddressContol.cmbState.Text = oProvider.CompanyPhysicalState
            oProviderCompanyPhysicalAddressContol.isFormLoading = False
            mskPLPager.Text = oProvider.CompanyPhysicalPagerNo
            maskedPLPhno.Text = oProvider.CompanyPhysicalPhoneNo
            mskPLFax.Text = oProvider.CompanyPhysicalFAX
            txtPLEMail.Text = oProvider.CompanyPhysicalEmail
            txtPLUrl.Text = oProvider.CompanyPhysicalURL

            'If IsDBNull(oProvider.ActiveStartTime) Or oProvider.ActiveStartTime = Nothing Then
            dtpActiveStartTime.Value = DateTime.Now
            'Else
            'dtpActiveStartTime.Value = oProvider.ActiveStartTime
            'End If

            'If IsDBNull(oProvider.ActiveEndTime) Or oProvider.ActiveEndTime = Nothing Then
            dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
            'Else
            'dtpActiveEndTime.Value = oProvider.ActiveEndTime
            'End If

            If oProvider.PARequired = clsProvider.PriorAuthorizationRequired.No.GetHashCode() Then
                rbNo.Checked = True
            ElseIf oProvider.PARequired = clsProvider.PriorAuthorizationRequired.Always.GetHashCode() Then
                rbAlways.Checked = True
            ElseIf oProvider.PARequired = clsProvider.PriorAuthorizationRequired.UsePlanSetting.GetHashCode() Then
                rbUsePlanSetting.Checked = True
            End If

            'TempLicensekey = oProvider.LicenceKey
            'txtLicenseKey.Text = oProvider.LicenceKey
            'lblAusStatus.Text = oProvider.AUSStatus
            '_nAUSPortalID = oProvider.AUSPortalID

            TempProvider.FirstName = oProvider.FirstName
            TempProvider.MiddleName = oProvider.MiddleName
            TempProvider.LastName = oProvider.LastName
            TempProvider.BMAddress1 = oProvider.BMAddress1
            TempProvider.BMAddress2 = oProvider.BMAddress2
            TempProvider.BMCity = oProvider.BMCity
            TempProvider.BMZIP = oProvider.BMZIP
            TempProvider.BMState = oProvider.BMState
            TempProvider.NPI = "" 'oProvider.NPI
            TempProvider.licenseKey = "" 'oProvider.LicenceKey

            If lblAusStatus.Text = "1" Then
                If txtLicenseKey.Text = "" Then
                    ReffreshLicense(False)
                    If btnLicenseRefresh.Tag = "valid" Then
                        lblLicenseMessage.Visible = True
                    End If
                End If
            End If

            If IsDBNull(oProvider.SPI) Or oProvider.SPI Is Nothing Or oProvider.SPI = "" Then
                rbPrescriber.Checked = True
                pnlSPI.Visible = False
                rbUpdate.Enabled = False
            Else
                rbPrescriber.Enabled = False
                rbUpdate.Checked = True
                lblPreDetails.Visible = False
                pnlSPI.Visible = False
                txtSPI.Visible = False
                lblRoot.Visible = False
                lblSPI.Text = oProvider.SPI
                SPICode = oProvider.SPI
                txtSPI.Enabled = False
            End If
            chkRequire_Supervising_Provider_for_eRx.Checked = oProvider.RequireSupervisingProviderforeRx

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

End Class
