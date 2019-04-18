Imports System.IO
Imports gloCommon
Imports gloEMR.gloStreamAdmin
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports gloSureScript
Imports System.Collections.Generic
Imports System.Linq
Imports System.Collections.ObjectModel
Imports gloGlobal.EPA

Public Class frmDoctor
    Inherits System.Windows.Forms.Form
    Dim blnLogoChanged As Boolean
    Dim flagControlSubstanceEnable As Boolean
    Public blnModify As Boolean
    Public Shared Imagepath As String
    Private dtActiveStartTime As DateTime = Nothing
    Private dtActiveEndTime As DateTime = Nothing
    Private bValidationFailed = False
    Private ServiceLevelCode As String = String.Empty
    Private SPI As String = String.Empty
    Private blnMultipleSupervisorsforPaperRx As Boolean = False
    'sarika
    'Dim objPrescriber As AddUpdatePrescriber
    Dim WithEvents objPrescriber As AddUpdatePrescriber
    Private blnIsSurescriptError As Boolean
    Private blnNewModify As Boolean = False
    Dim objNewProvider As New clsProvider(gstrConnectionString)
    Public strMessage As String = ""
    Dim pType As Int16
    '--
    Private nProviderUserID As Int64 = 0
    Private _nProviderID As Int64 = 0
    Private _nClinicID As Int64 = 1

    Private _nAUSPortalID As Int64 = 0

    Private oListControl As gloListControl.gloListControl
    Private _CurrentControlType As gloListControl.gloListControlType = gloListControl.gloListControlType.Taxonomy
    Public oBussinessAddressContol As gloAddress.gloAddressControl
    Public oPracticeAddressContol As gloAddress.gloAddressControl
    Public oCompanyAddressContol As gloAddress.gloAddressControl
    Public oProviderPhysicalAddressContol As gloAddress.gloAddressControl
    Public oProviderCompanyPhysicalAddressContol As gloAddress.gloAddressControl

    Private IsLoading As Boolean = False
    Private TempserviceLevel As String = String.Empty
    Dim bIsServiceLevelDisabled As Boolean = False
    Private ImgWidth As Int16 = 0

    Private listUsers As List(Of EPARole) = Nothing
    Private listSubmitter As List(Of EPARole) = Nothing
    Private listReviewer As List(Of EPARole) = Nothing
    Private listPreparer As List(Of EPARole) = Nothing
    Private SelectedRoleType As RoleType = RoleType.PASubmitter
    Private TempLicensekey As String = String.Empty
    Private ISDEMOLicensce As Boolean = False
    Private TempProvider As gloAUSLibrary.Class.TempProviderdata = Nothing
    Private ReadOnly Property IsEPATurnedOn() As Boolean
        Get
            Dim bReturned As Boolean = False
            If BeforeServiceLevel IsNot Nothing AndAlso AfterServiceLevel IsNot Nothing Then

                If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                    If Mid(AfterServiceLevel, 9, 1) = 1 Then
                        bReturned = True
                    End If
                Else
                    If Mid(BeforeServiceLevel, 9, 1) = 0 AndAlso Mid(AfterServiceLevel, 9, 1) = 1 Then
                        bReturned = True
                    End If
                End If
            End If

            Return bReturned
        End Get
    End Property

#Region "Zip control implemented  "
    'region added by dipak 20090912
    'variable are used in Show ZipControl
    'Variable isFormLoading indicate formloading completed or not
    Public isFormLoading As Boolean = False


    ''Object of GloZipControl
    'COMMENTED BY SHUBHANGI20100906
    'Private oZipcontrol As gloPatient.gloZipcontrol
    'We are using gloZipcontrol from gloAddress 
    Private oZipcontrol As gloAddress.gloZipcontrol
    'Variable used as flag of Search control is Open status if it is open for any other zipText Control then it is set to true
    Private isSearchControlOpen As Boolean = False
    'Variable Store ZipText temporary
    Private _TempZipText As String
    'Variable Indicate Any Item Selected in ZipControl or not
    Private _isZipItemSelected As Boolean = False
    'variable use to detect text change event occure due to user type zip or text assign by '='Asignment operator (by default text loading is set to true)
    Private _isTextBoxLoading As Boolean = True
    Private _ZipTextType As enumZipTextType

    Private Enum enumZipTextType
        BusinessZip = 0
        CompanyZip
        BusinessPracticeZip
    End Enum
#End Region



#Region " Windows Controls "
    Friend WithEvents pnlSPI As System.Windows.Forms.Panel
    Friend WithEvents lblSPI As System.Windows.Forms.Label
    Friend WithEvents txtSPI As System.Windows.Forms.TextBox
    Friend WithEvents lblRoot As System.Windows.Forms.Label
    Friend WithEvents lblLabelSPI As System.Windows.Forms.Label
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
    'Line Modified by dipak 20090912 to cahnge SSN no Field to Normal mask control to glo mask control
    Friend WithEvents mtxt_SSNno As gloMaskControl.gloMaskBox
    'end modification
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
    Friend WithEvents txtImagePath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents optBrowse As System.Windows.Forms.RadioButton
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCapture As System.Windows.Forms.Button
    Friend WithEvents picSignature As System.Windows.Forms.PictureBox
    'Line Comented and Modified by Dipak 20090912 to change txtCompanyFax type windows mask control to gloMask Control	
    'Friend WithEvents txtCompanyFax As System.Windows.Forms.TextBox
    'end modification
    'Private WithEvents chkCompanyAsAbove As System.Windows.Forms.CheckBox
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
    Friend WithEvents txtBMCity As System.Windows.Forms.TextBox
    Friend WithEvents txtBMZip As System.Windows.Forms.TextBox
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents txtBMURL As System.Windows.Forms.TextBox
    Friend WithEvents txtBMEmailAddress As System.Windows.Forms.TextBox
    ''Line Comented and Modified by Dipak 20090912 to change txtBMFAX type windows mask control to gloMask Control	
    'Friend WithEvents txtBMFAX As System.Windows.Forms.TextBox
    Friend WithEvents txtBMFAX As gloMaskControl.gloMaskBox
    'end modification
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
    'Code Comente and modified by dipak 20090912 to change txtBPracFax type windows mask control to glomask control
    'Friend WithEvents txtBPracFax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracFax As gloMaskControl.gloMaskBox
    'end modification by dipak
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
    Friend WithEvents txtBPracCity As System.Windows.Forms.TextBox
    Friend WithEvents txtBPracZIP As System.Windows.Forms.TextBox
    Friend WithEvents mskBMPhoneNo As gloMaskControl.gloMaskBox
    Friend WithEvents mskMobileNo As gloMaskControl.gloMaskBox
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents pnlInternalControl As System.Windows.Forms.Panel
    Friend WithEvents pnlBPractInternalControl As System.Windows.Forms.Panel
    Friend WithEvents cmbBPracState As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBMstate As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlBussinessAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents pnlPracticeAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents lblLabID As System.Windows.Forms.Label
    Friend WithEvents txtLabId As System.Windows.Forms.TextBox
    Private WithEvents rbNo As System.Windows.Forms.RadioButton
    Private WithEvents rbUsePlanSetting As System.Windows.Forms.RadioButton
    Private WithEvents rbAlways As System.Windows.Forms.RadioButton
    Friend WithEvents txtBMPager As gloMaskControl.gloMaskBox
    Friend WithEvents txtBPracPager As gloMaskControl.gloMaskBox
    Friend WithEvents txtDEA As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtNADEAN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtUPIN As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbAssignUser As System.Windows.Forms.ComboBox
    Friend WithEvents btnAssignSign As System.Windows.Forms.Button
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents label33 As System.Windows.Forms.Label
    Private WithEvents label36 As System.Windows.Forms.Label
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents label34 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label77 As System.Windows.Forms.Label
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents grpSPI As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents grpUserDetails As System.Windows.Forms.Panel
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents txtExternalCode As System.Windows.Forms.TextBox
    Friend WithEvents lblExternalCode As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tbpgProvider As System.Windows.Forms.TabPage
    Friend WithEvents tbpgStatement As System.Windows.Forms.TabPage
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents Label205 As System.Windows.Forms.Label
    Friend WithEvents Label206 As System.Windows.Forms.Label
    Friend WithEvents Label207 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents txtDPSNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblDPSNumber As System.Windows.Forms.Label
    Friend WithEvents tbpgProviderCompany As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Label109 As System.Windows.Forms.Label
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
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Private WithEvents c1CompanyProvIdentification As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents tbpgBillingID As System.Windows.Forms.TabPage
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
    Friend WithEvents gbProviderIdentification As System.Windows.Forms.Panel
    Private WithEvents c1ProviderIdentification As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyNPI As System.Windows.Forms.TextBox
    Private WithEvents chkCompanyAsAbove As System.Windows.Forms.CheckBox
    Friend WithEvents txtCompanyPhone As gloMaskControl.gloMaskBox
    Friend WithEvents label47 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyAddress1 As System.Windows.Forms.TextBox
    Friend WithEvents label46 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyContactName As System.Windows.Forms.TextBox
    Friend WithEvents label19 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents pnlCompanyInternalControl As System.Windows.Forms.Panel
    Friend WithEvents pnlCompanyAddresssControl As System.Windows.Forms.Panel
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyZip As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtCompanyEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyCity As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyFax As gloMaskControl.gloMaskBox
    Friend WithEvents txtCompanyTaxID As System.Windows.Forms.TextBox
    Friend WithEvents txtCompanyAddress2 As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtCmpTaxonomyCode As System.Windows.Forms.TextBox
    Private WithEvents btn_ClearCmpTaxonomy As System.Windows.Forms.Button
    Private WithEvents btn_BrowseCmpTaxonomy As System.Windows.Forms.Button
    Friend WithEvents tbpg_CCDSettings As System.Windows.Forms.TabPage
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtCDS_PESUrl As System.Windows.Forms.TextBox
    Friend WithEvents txtCDS_PESPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtCDS_PESUserName As System.Windows.Forms.TextBox

    Friend WithEvents Label741 As System.Windows.Forms.Label
    Friend WithEvents Label699 As System.Windows.Forms.Label
    Friend WithEvents Label725 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Private WithEvents btn_ViewCatalog As System.Windows.Forms.Button
    Friend WithEvents cmbCatalogCode As System.Windows.Forms.ComboBox
    Friend WithEvents chkRequire_Supervising_Provider_for_eRx As System.Windows.Forms.CheckBox
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label127 As System.Windows.Forms.Label
    Private WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents pnlchkRequireSupervisingProviderRx As System.Windows.Forms.Panel
    Private WithEvents Label139 As System.Windows.Forms.Label
    Private WithEvents Label140 As System.Windows.Forms.Label
    Private WithEvents Label141 As System.Windows.Forms.Label
    Private WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents lblNPIMandatory As System.Windows.Forms.Label
    Friend WithEvents ChckEPCS As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCIEvent As System.Windows.Forms.CheckBox
    Friend WithEvents chkCIMessage As System.Windows.Forms.CheckBox
    Friend WithEvents lblDirectAddressValue As System.Windows.Forms.Label
    Friend WithEvents lblDirectAddress As System.Windows.Forms.Label
    Friend WithEvents txtDirectAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Private WithEvents lblPreDetails As System.Windows.Forms.Label
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents udMaxScale As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMiaxScale As System.Windows.Forms.Label
    Friend WithEvents udMinScale As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMinScale As System.Windows.Forms.Label
    Friend WithEvents btnFontDialog As System.Windows.Forms.Button
    Friend WithEvents txtFont As System.Windows.Forms.TextBox
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents fdSignature As System.Windows.Forms.FontDialog
    Friend WithEvents tbpg_EpcsSettings As System.Windows.Forms.TabPage
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents LabelUiLaunchLogicalAccess As System.Windows.Forms.Label
    Friend WithEvents LabelUiLaunchPrescriberArea As System.Windows.Forms.Label
    Friend WithEvents LabelWsInvitePrescriber As System.Windows.Forms.Label
    Friend WithEvents LinkLabelUILaunchPrescriberArea As System.Windows.Forms.LinkLabel
    Friend WithEvents btnEnrollPrescriber As System.Windows.Forms.Button
    Friend WithEvents btnInvitePrescriber As System.Windows.Forms.Button
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Private WithEvents mtxtDOB As System.Windows.Forms.MaskedTextBox
    Private WithEvents lbPatientDOB As System.Windows.Forms.Label
    Friend WithEvents ChkePA As System.Windows.Forms.CheckBox
    Friend WithEvents tbpg_ePASettings As System.Windows.Forms.TabPage
    Friend WithEvents pnlPARoles As System.Windows.Forms.Panel
    Friend WithEvents btnRemoveRole As System.Windows.Forms.Button
    Friend WithEvents btnAddRole As System.Windows.Forms.Button
    Friend WithEvents lstReviewer As System.Windows.Forms.ListBox
    Friend WithEvents lstPreparer As System.Windows.Forms.ListBox
    Friend WithEvents lstSubmitter As System.Windows.Forms.ListBox
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents lstUsers As System.Windows.Forms.ListBox
    Private WithEvents Label157 As System.Windows.Forms.Label
    Private WithEvents Label158 As System.Windows.Forms.Label
    Private WithEvents lblBottom As System.Windows.Forms.Label
    Private WithEvents lblLeft As System.Windows.Forms.Label
    Private WithEvents lblRight As System.Windows.Forms.Label
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents pnlPreparer As System.Windows.Forms.Panel
    Private WithEvents Label155 As System.Windows.Forms.Label
    Private WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents pnllblPreparerHeader As System.Windows.Forms.Panel
    Private WithEvents lblPreparerHeaderBottom As System.Windows.Forms.Label
    Friend WithEvents pnlSubmitter As System.Windows.Forms.Panel
    Private WithEvents Label159 As System.Windows.Forms.Label
    Private WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents pnllblSubmitterHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlLightHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlMids As System.Windows.Forms.Panel
    Private WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents Label186 As System.Windows.Forms.Label
    Private WithEvents Label187 As System.Windows.Forms.Label
    Private WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents lblLightHeader As System.Windows.Forms.Label
    Friend WithEvents pnlReviewer As System.Windows.Forms.Panel
    Private WithEvents Label189 As System.Windows.Forms.Label
    Private WithEvents Label191 As System.Windows.Forms.Label
    Friend WithEvents pnllblReviewerHeader As System.Windows.Forms.Panel
    Private WithEvents lblReviewerHeaderBottom As System.Windows.Forms.Label
    Private WithEvents lblSubmitterBottom As System.Windows.Forms.Label
    Friend WithEvents btnSubmitter As System.Windows.Forms.Button
    Private WithEvents lblSubmitterBottomMain As System.Windows.Forms.Label
    Private WithEvents lblSubmitterTopMain As System.Windows.Forms.Label
    Private WithEvents lblSubmitterLeftMain As System.Windows.Forms.Label
    Private WithEvents lblSubmitterRightMain As System.Windows.Forms.Label
    Private WithEvents lblReviewerLeftMain As System.Windows.Forms.Label
    Private WithEvents lblReviewerRightMain As System.Windows.Forms.Label
    Private WithEvents lblReviewerBottomMain As System.Windows.Forms.Label
    Private WithEvents lblReviewerTopMain As System.Windows.Forms.Label
    Private WithEvents lblPreparerBottomMain As System.Windows.Forms.Label
    Private WithEvents lblPreparerTopMain As System.Windows.Forms.Label
    Private WithEvents lblPreparerLeftMain As System.Windows.Forms.Label
    Private WithEvents lblPreparerRightMain As System.Windows.Forms.Label
    Friend WithEvents btnReviewer As System.Windows.Forms.Button
    Friend WithEvents btnPreparer As System.Windows.Forms.Button
    Private WithEvents txtLicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents lblAusStatus As System.Windows.Forms.Label
    Friend WithEvents pnlLicenseKey As System.Windows.Forms.Panel
    Friend WithEvents Label170 As System.Windows.Forms.Label
    Private WithEvents Label156 As System.Windows.Forms.Label
    Private WithEvents Label167 As System.Windows.Forms.Label
    Private WithEvents Label168 As System.Windows.Forms.Label
    Private WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents btnLicenseRefresh As System.Windows.Forms.Button
    Friend WithEvents ImgLicenseKey As System.Windows.Forms.ImageList
    Friend WithEvents lblLicenseMessage As System.Windows.Forms.Label
    Friend WithEvents chkPDR As System.Windows.Forms.CheckBox
    Friend WithEvents ChckRxFill As System.Windows.Forms.CheckBox
    Friend WithEvents ChckChange As System.Windows.Forms.CheckBox
    Friend WithEvents ChckCancel As System.Windows.Forms.CheckBox
    Friend WithEvents Label171 As System.Windows.Forms.Label
    Friend WithEvents chkPDMP As System.Windows.Forms.CheckBox
    Friend WithEvents maskedBpracPhno As gloMaskControl.gloMaskBox
#End Region

#Region " Windows Form Designer generated code "



    Public Sub New(ByVal nProviderID As Int64)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _nProviderID = nProviderID
        ''dhruv adding the control

        gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.Country)

        oBussinessAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oBussinessAddressContol.Dock = DockStyle.Fill
        pnlBussinessAddresssControl.Controls.Add(oBussinessAddressContol)

        oPracticeAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oPracticeAddressContol.Dock = DockStyle.Fill
        pnlPracticeAddresssControl.Controls.Add(oPracticeAddressContol)

        oCompanyAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oCompanyAddressContol.Dock = DockStyle.Fill
        pnlCompanyAddresssControl.Controls.Add(oCompanyAddressContol)

        oProviderPhysicalAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oProviderPhysicalAddressContol.Dock = DockStyle.Fill
        pnlBIDPLAddresssControl.Controls.Add(oProviderPhysicalAddressContol)

        oProviderCompanyPhysicalAddressContol = New gloAddress.gloAddressControl(gloEMRAdmin.mdlGeneral.GetConnectionString())
        oProviderCompanyPhysicalAddressContol.Dock = DockStyle.Fill
        pnlPLAddresssControl.Controls.Add(oProviderCompanyPhysicalAddressContol)

        ''End Dhruv 
        ''Added On 20101005 by Sanjog for signature  
        FillSignature(_nProviderID)
        ''Added On 20101005 by Sanjog for signature 
        'Add any initialization after the InitializeComponent() call

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
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.rbNo = New System.Windows.Forms.RadioButton()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.rbUsePlanSetting = New System.Windows.Forms.RadioButton()
        Me.rbAlways = New System.Windows.Forms.RadioButton()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.grpSPI = New System.Windows.Forms.Panel()
        Me.chkPDMP = New System.Windows.Forms.CheckBox()
        Me.ChckRxFill = New System.Windows.Forms.CheckBox()
        Me.ChckChange = New System.Windows.Forms.CheckBox()
        Me.ChckCancel = New System.Windows.Forms.CheckBox()
        Me.chkPDR = New System.Windows.Forms.CheckBox()
        Me.ChkePA = New System.Windows.Forms.CheckBox()
        Me.lblPreDetails = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.chckDisable = New System.Windows.Forms.CheckBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.ChkCIEvent = New System.Windows.Forms.CheckBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.dtpActiveEndTime = New System.Windows.Forms.DateTimePicker()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.dtpActiveStartTime = New System.Windows.Forms.DateTimePicker()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.rbUpdate = New System.Windows.Forms.RadioButton()
        Me.ChckEPCS = New System.Windows.Forms.CheckBox()
        Me.chkCIMessage = New System.Windows.Forms.CheckBox()
        Me.chckRefill = New System.Windows.Forms.CheckBox()
        Me.rbPrescriberLocation = New System.Windows.Forms.RadioButton()
        Me.chckNew = New System.Windows.Forms.CheckBox()
        Me.rbPrescriber = New System.Windows.Forms.RadioButton()
        Me.pnlSPI = New System.Windows.Forms.Panel()
        Me.txtDirectAddress = New System.Windows.Forms.TextBox()
        Me.lblDirectAddressValue = New System.Windows.Forms.Label()
        Me.lblDirectAddress = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.lblSPI = New System.Windows.Forms.Label()
        Me.txtSPI = New System.Windows.Forms.TextBox()
        Me.lblRoot = New System.Windows.Forms.Label()
        Me.lblLabelSPI = New System.Windows.Forms.Label()
        Me.chkRequire_Supervising_Provider_for_eRx = New System.Windows.Forms.CheckBox()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.lblNPIMandatory = New System.Windows.Forms.Label()
        Me.txtDPSNumber = New System.Windows.Forms.TextBox()
        Me.txtExternalCode = New System.Windows.Forms.TextBox()
        Me.txtDEA = New System.Windows.Forms.MaskedTextBox()
        Me.txtNADEAN = New System.Windows.Forms.MaskedTextBox()
        Me.txtUPIN = New System.Windows.Forms.MaskedTextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtLabId = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.mskMobileNo = New gloMaskControl.gloMaskBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.mtxt_SSNno = New gloMaskControl.gloMaskBox()
        Me.txtTaxonomy = New System.Windows.Forms.TextBox()
        Me.lblDEA = New System.Windows.Forms.Label()
        Me.btn_ClearTaxonomy = New System.Windows.Forms.Button()
        Me.cmbDoctorType = New System.Windows.Forms.ComboBox()
        Me.btn_BrowseTaxonomy = New System.Windows.Forms.Button()
        Me.txtStateMedicalLicenseNo = New System.Windows.Forms.TextBox()
        Me.lblStateMedicalLicense = New System.Windows.Forms.Label()
        Me.txt_EmployerID = New System.Windows.Forms.TextBox()
        Me.label28 = New System.Windows.Forms.Label()
        Me.txtNPI = New System.Windows.Forms.TextBox()
        Me.lblUPIN = New System.Windows.Forms.Label()
        Me.lblNPI = New System.Windows.Forms.Label()
        Me.lblExternalCode = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.label27 = New System.Windows.Forms.Label()
        Me.lblLabID = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblDPSNumber = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtBMContact = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.txtBMPager = New gloMaskControl.gloMaskBox()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtBMFAX = New gloMaskControl.gloMaskBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.lblPager = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.pnlBussinessAddresssControl = New System.Windows.Forms.Panel()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.pnlInternalControl = New System.Windows.Forms.Panel()
        Me.label26 = New System.Windows.Forms.Label()
        Me.cmbBMstate = New System.Windows.Forms.ComboBox()
        Me.lblURL = New System.Windows.Forms.Label()
        Me.mskBMPhoneNo = New gloMaskControl.gloMaskBox()
        Me.txtBMEmailAddress = New System.Windows.Forms.TextBox()
        Me.txtBMURL = New System.Windows.Forms.TextBox()
        Me.lblPhoneNo = New System.Windows.Forms.Label()
        Me.txtBMAddress1 = New System.Windows.Forms.TextBox()
        Me.txtBMZip = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtBMCity = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtBMAddress2 = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.grpUserDetails = New System.Windows.Forms.Panel()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.lblAusStatus = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblConfirmPassword = New System.Windows.Forms.Label()
        Me.lblNickName = New System.Windows.Forms.Label()
        Me.txtNickName = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtLicenseKey = New System.Windows.Forms.TextBox()
        Me.chkAddressasAbove = New System.Windows.Forms.CheckBox()
        Me.label13 = New System.Windows.Forms.Label()
        Me.pnlPracticeAddresssControl = New System.Windows.Forms.Panel()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.pnlBPractInternalControl = New System.Windows.Forms.Panel()
        Me.cmbBPracState = New System.Windows.Forms.ComboBox()
        Me.label11 = New System.Windows.Forms.Label()
        Me.txtBPracContactName = New System.Windows.Forms.TextBox()
        Me.lblState = New System.Windows.Forms.Label()
        Me.label25 = New System.Windows.Forms.Label()
        Me.txtBPracZIP = New System.Windows.Forms.TextBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.txtBPracCity = New System.Windows.Forms.TextBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.txtBPracAddress2 = New System.Windows.Forms.TextBox()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.txtBPracUrl = New System.Windows.Forms.TextBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.txtBPracEMail = New System.Windows.Forms.TextBox()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.txtBPracAddress1 = New System.Windows.Forms.TextBox()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.udMaxScale = New System.Windows.Forms.NumericUpDown()
        Me.lblMiaxScale = New System.Windows.Forms.Label()
        Me.udMinScale = New System.Windows.Forms.NumericUpDown()
        Me.lblMinScale = New System.Windows.Forms.Label()
        Me.btnFontDialog = New System.Windows.Forms.Button()
        Me.txtFont = New System.Windows.Forms.TextBox()
        Me.lblFont = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.cmbAssignUser = New System.Windows.Forms.ComboBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.btnAssignSign = New System.Windows.Forms.Button()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.picSignature = New System.Windows.Forms.PictureBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.txtImagePath = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCapture = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.optBrowse = New System.Windows.Forms.RadioButton()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSuffix = New System.Windows.Forms.TextBox()
        Me.label33 = New System.Windows.Forms.Label()
        Me.label36 = New System.Windows.Forms.Label()
        Me.label35 = New System.Windows.Forms.Label()
        Me.label34 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblLastName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMiddleName = New System.Windows.Forms.Label()
        Me.lblFirstName = New System.Windows.Forms.Label()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.label45 = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.mtxtDOB = New System.Windows.Forms.MaskedTextBox()
        Me.lbPatientDOB = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.optMale = New System.Windows.Forms.RadioButton()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.optFemale = New System.Windows.Forms.RadioButton()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.lblLicenseMessage = New System.Windows.Forms.Label()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tbpgProvider = New System.Windows.Forms.TabPage()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.pnlchkRequireSupervisingProviderRx = New System.Windows.Forms.Panel()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.pnlLicenseKey = New System.Windows.Forms.Panel()
        Me.btnLicenseRefresh = New System.Windows.Forms.Button()
        Me.ImgLicenseKey = New System.Windows.Forms.ImageList(Me.components)
        Me.Label170 = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.Label169 = New System.Windows.Forms.Label()
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
        Me.c1ProviderIdentification = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.tbpgProviderCompany = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
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
        Me.txtCompanyNPI = New System.Windows.Forms.TextBox()
        Me.chkCompanyAsAbove = New System.Windows.Forms.CheckBox()
        Me.txtCompanyPhone = New gloMaskControl.gloMaskBox()
        Me.label47 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.txtCompanyAddress1 = New System.Windows.Forms.TextBox()
        Me.label46 = New System.Windows.Forms.Label()
        Me.txtCompanyContactName = New System.Windows.Forms.TextBox()
        Me.label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlCompanyInternalControl = New System.Windows.Forms.Panel()
        Me.pnlCompanyAddresssControl = New System.Windows.Forms.Panel()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtCompanyZip = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.txtCompanyEmail = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.txtCompanyCity = New System.Windows.Forms.TextBox()
        Me.txtCompanyFax = New gloMaskControl.gloMaskBox()
        Me.txtCompanyTaxID = New System.Windows.Forms.TextBox()
        Me.txtCompanyAddress2 = New System.Windows.Forms.TextBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.tbpgStatement = New System.Windows.Forms.TabPage()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.txtBPracPager = New gloMaskControl.gloMaskBox()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.Label206 = New System.Windows.Forms.Label()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.maskedBpracPhno = New gloMaskControl.gloMaskBox()
        Me.txtBPracFax = New gloMaskControl.gloMaskBox()
        Me.tbpg_EpcsSettings = New System.Windows.Forms.TabPage()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.LabelUiLaunchLogicalAccess = New System.Windows.Forms.Label()
        Me.LabelUiLaunchPrescriberArea = New System.Windows.Forms.Label()
        Me.LabelWsInvitePrescriber = New System.Windows.Forms.Label()
        Me.LinkLabelUILaunchPrescriberArea = New System.Windows.Forms.LinkLabel()
        Me.btnEnrollPrescriber = New System.Windows.Forms.Button()
        Me.btnInvitePrescriber = New System.Windows.Forms.Button()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.tbpg_ePASettings = New System.Windows.Forms.TabPage()
        Me.pnlPARoles = New System.Windows.Forms.Panel()
        Me.pnlReviewer = New System.Windows.Forms.Panel()
        Me.lstReviewer = New System.Windows.Forms.ListBox()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.pnllblReviewerHeader = New System.Windows.Forms.Panel()
        Me.btnReviewer = New System.Windows.Forms.Button()
        Me.lblReviewerHeaderBottom = New System.Windows.Forms.Label()
        Me.lblReviewerLeftMain = New System.Windows.Forms.Label()
        Me.lblReviewerRightMain = New System.Windows.Forms.Label()
        Me.lblReviewerBottomMain = New System.Windows.Forms.Label()
        Me.lblReviewerTopMain = New System.Windows.Forms.Label()
        Me.pnlPreparer = New System.Windows.Forms.Panel()
        Me.lstPreparer = New System.Windows.Forms.ListBox()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.pnllblPreparerHeader = New System.Windows.Forms.Panel()
        Me.btnPreparer = New System.Windows.Forms.Button()
        Me.lblPreparerHeaderBottom = New System.Windows.Forms.Label()
        Me.lblPreparerBottomMain = New System.Windows.Forms.Label()
        Me.lblPreparerTopMain = New System.Windows.Forms.Label()
        Me.lblPreparerLeftMain = New System.Windows.Forms.Label()
        Me.lblPreparerRightMain = New System.Windows.Forms.Label()
        Me.pnlSubmitter = New System.Windows.Forms.Panel()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.lstSubmitter = New System.Windows.Forms.ListBox()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.pnllblSubmitterHeader = New System.Windows.Forms.Panel()
        Me.lblSubmitterBottom = New System.Windows.Forms.Label()
        Me.btnSubmitter = New System.Windows.Forms.Button()
        Me.lblSubmitterBottomMain = New System.Windows.Forms.Label()
        Me.lblSubmitterTopMain = New System.Windows.Forms.Label()
        Me.lblSubmitterLeftMain = New System.Windows.Forms.Label()
        Me.lblSubmitterRightMain = New System.Windows.Forms.Label()
        Me.pnlLightHeader = New System.Windows.Forms.Panel()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.lstUsers = New System.Windows.Forms.ListBox()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.lblBottom = New System.Windows.Forms.Label()
        Me.lblLeft = New System.Windows.Forms.Label()
        Me.lblRight = New System.Windows.Forms.Label()
        Me.pnlMids = New System.Windows.Forms.Panel()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.lblLightHeader = New System.Windows.Forms.Label()
        Me.btnRemoveRole = New System.Windows.Forms.Button()
        Me.btnAddRole = New System.Windows.Forms.Button()
        Me.Panel32 = New System.Windows.Forms.Panel()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.tbpg_CCDSettings = New System.Windows.Forms.TabPage()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.cmbCatalogCode = New System.Windows.Forms.ComboBox()
        Me.btn_ViewCatalog = New System.Windows.Forms.Button()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtCDS_PESUrl = New System.Windows.Forms.TextBox()
        Me.txtCDS_PESPassword = New System.Windows.Forms.TextBox()
        Me.txtCDS_PESUserName = New System.Windows.Forms.TextBox()
        Me.Label741 = New System.Windows.Forms.Label()
        Me.Label699 = New System.Windows.Forms.Label()
        Me.Label725 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.fdSignature = New System.Windows.Forms.FontDialog()
        Me.Panel13.SuspendLayout()
        Me.grpSPI.SuspendLayout()
        Me.pnlSPI.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.grpUserDetails.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.udMaxScale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udMinScale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel14.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tbpgProvider.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.pnlchkRequireSupervisingProviderRx.SuspendLayout()
        Me.Panel27.SuspendLayout()
        Me.pnlLicenseKey.SuspendLayout()
        Me.tbpgBillingID.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.gbProviderIdentification.SuspendLayout()
        CType(Me.c1ProviderIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        Me.tbpgProviderCompany.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.c1CompanyProvIdentification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel24.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.tbpgStatement.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.tbpg_EpcsSettings.SuspendLayout()
        Me.Panel28.SuspendLayout()
        Me.Panel29.SuspendLayout()
        Me.Panel30.SuspendLayout()
        Me.tbpg_ePASettings.SuspendLayout()
        Me.pnlPARoles.SuspendLayout()
        Me.pnlReviewer.SuspendLayout()
        Me.pnllblReviewerHeader.SuspendLayout()
        Me.pnlPreparer.SuspendLayout()
        Me.pnllblPreparerHeader.SuspendLayout()
        Me.pnlSubmitter.SuspendLayout()
        Me.pnllblSubmitterHeader.SuspendLayout()
        Me.pnlLightHeader.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.pnlMids.SuspendLayout()
        Me.Panel32.SuspendLayout()
        Me.tbpg_CCDSettings.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.rbNo)
        Me.Panel13.Controls.Add(Me.Label83)
        Me.Panel13.Controls.Add(Me.rbUsePlanSetting)
        Me.Panel13.Controls.Add(Me.rbAlways)
        Me.Panel13.Controls.Add(Me.Label87)
        Me.Panel13.Controls.Add(Me.Label88)
        Me.Panel13.Controls.Add(Me.Label89)
        Me.Panel13.Controls.Add(Me.Label90)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 29)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel13.Size = New System.Drawing.Size(774, 34)
        Me.Panel13.TabIndex = 4
        '
        'rbNo
        '
        Me.rbNo.AutoSize = True
        Me.rbNo.Checked = True
        Me.rbNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbNo.Location = New System.Drawing.Point(611, 4)
        Me.rbNo.Name = "rbNo"
        Me.rbNo.Size = New System.Drawing.Size(41, 18)
        Me.rbNo.TabIndex = 2
        Me.rbNo.TabStop = True
        Me.rbNo.Text = "No"
        Me.rbNo.UseVisualStyleBackColor = True
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(19, 6)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(241, 14)
        Me.Label83.TabIndex = 193
        Me.Label83.Text = "Default Prior Authorization Required :"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbUsePlanSetting
        '
        Me.rbUsePlanSetting.AutoSize = True
        Me.rbUsePlanSetting.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbUsePlanSetting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUsePlanSetting.Location = New System.Drawing.Point(379, 4)
        Me.rbUsePlanSetting.Name = "rbUsePlanSetting"
        Me.rbUsePlanSetting.Size = New System.Drawing.Size(204, 18)
        Me.rbUsePlanSetting.TabIndex = 1
        Me.rbUsePlanSetting.Text = "Yes - Use Insurance Plan Setting"
        Me.rbUsePlanSetting.UseVisualStyleBackColor = True
        '
        'rbAlways
        '
        Me.rbAlways.AutoSize = True
        Me.rbAlways.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbAlways.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAlways.Location = New System.Drawing.Point(266, 4)
        Me.rbAlways.Name = "rbAlways"
        Me.rbAlways.Size = New System.Drawing.Size(94, 18)
        Me.rbAlways.TabIndex = 0
        Me.rbAlways.Text = "Yes - Always"
        Me.rbAlways.UseVisualStyleBackColor = True
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label87.Location = New System.Drawing.Point(4, 30)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(766, 1)
        Me.Label87.TabIndex = 191
        Me.Label87.Text = "label2"
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(4, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(766, 1)
        Me.Label88.TabIndex = 190
        Me.Label88.Text = "label1"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(770, 0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 31)
        Me.Label89.TabIndex = 189
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(3, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1, 31)
        Me.Label90.TabIndex = 1
        Me.Label90.Text = "label4"
        '
        'grpSPI
        '
        Me.grpSPI.AutoScroll = True
        Me.grpSPI.Controls.Add(Me.chkPDMP)
        Me.grpSPI.Controls.Add(Me.ChckRxFill)
        Me.grpSPI.Controls.Add(Me.ChckChange)
        Me.grpSPI.Controls.Add(Me.ChckCancel)
        Me.grpSPI.Controls.Add(Me.chkPDR)
        Me.grpSPI.Controls.Add(Me.ChkePA)
        Me.grpSPI.Controls.Add(Me.lblPreDetails)
        Me.grpSPI.Controls.Add(Me.Label23)
        Me.grpSPI.Controls.Add(Me.chckDisable)
        Me.grpSPI.Controls.Add(Me.Label32)
        Me.grpSPI.Controls.Add(Me.Label84)
        Me.grpSPI.Controls.Add(Me.ChkCIEvent)
        Me.grpSPI.Controls.Add(Me.Label31)
        Me.grpSPI.Controls.Add(Me.Label85)
        Me.grpSPI.Controls.Add(Me.dtpActiveEndTime)
        Me.grpSPI.Controls.Add(Me.Label86)
        Me.grpSPI.Controls.Add(Me.dtpActiveStartTime)
        Me.grpSPI.Controls.Add(Me.Label30)
        Me.grpSPI.Controls.Add(Me.rbUpdate)
        Me.grpSPI.Controls.Add(Me.ChckEPCS)
        Me.grpSPI.Controls.Add(Me.chkCIMessage)
        Me.grpSPI.Controls.Add(Me.chckRefill)
        Me.grpSPI.Controls.Add(Me.rbPrescriberLocation)
        Me.grpSPI.Controls.Add(Me.chckNew)
        Me.grpSPI.Controls.Add(Me.rbPrescriber)
        Me.grpSPI.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSPI.Location = New System.Drawing.Point(0, 0)
        Me.grpSPI.Name = "grpSPI"
        Me.grpSPI.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.grpSPI.Size = New System.Drawing.Size(774, 96)
        Me.grpSPI.TabIndex = 3
        '
        'chkPDMP
        '
        Me.chkPDMP.AutoSize = True
        Me.chkPDMP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPDMP.Enabled = False
        Me.chkPDMP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPDMP.Location = New System.Drawing.Point(682, 47)
        Me.chkPDMP.Name = "chkPDMP"
        Me.chkPDMP.Size = New System.Drawing.Size(57, 18)
        Me.chkPDMP.TabIndex = 197
        Me.chkPDMP.Text = "PDMP"
        Me.chkPDMP.UseVisualStyleBackColor = True
        '
        'ChckRxFill
        '
        Me.ChckRxFill.AutoSize = True
        Me.ChckRxFill.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChckRxFill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChckRxFill.Location = New System.Drawing.Point(375, 50)
        Me.ChckRxFill.Name = "ChckRxFill"
        Me.ChckRxFill.Size = New System.Drawing.Size(51, 18)
        Me.ChckRxFill.TabIndex = 11
        Me.ChckRxFill.Text = "RxFill"
        Me.ChckRxFill.UseVisualStyleBackColor = True
        '
        'ChckChange
        '
        Me.ChckChange.AutoSize = True
        Me.ChckChange.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChckChange.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChckChange.Location = New System.Drawing.Point(204, 50)
        Me.ChckChange.Name = "ChckChange"
        Me.ChckChange.Size = New System.Drawing.Size(67, 18)
        Me.ChckChange.TabIndex = 9
        Me.ChckChange.Text = "Change"
        Me.ChckChange.UseVisualStyleBackColor = True
        '
        'ChckCancel
        '
        Me.ChckCancel.AutoSize = True
        Me.ChckCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChckCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChckCancel.Location = New System.Drawing.Point(291, 27)
        Me.ChckCancel.Name = "ChckCancel"
        Me.ChckCancel.Size = New System.Drawing.Size(61, 18)
        Me.ChckCancel.TabIndex = 5
        Me.ChckCancel.Text = "Cancel"
        Me.ChckCancel.UseVisualStyleBackColor = True
        '
        'chkPDR
        '
        Me.chkPDR.AutoSize = True
        Me.chkPDR.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPDR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPDR.Location = New System.Drawing.Point(682, 27)
        Me.chkPDR.Name = "chkPDR"
        Me.chkPDR.Size = New System.Drawing.Size(48, 18)
        Me.chkPDR.TabIndex = 8
        Me.chkPDR.Text = "PDR"
        Me.chkPDR.UseVisualStyleBackColor = True
        '
        'ChkePA
        '
        Me.ChkePA.AutoSize = True
        Me.ChkePA.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkePA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkePA.Location = New System.Drawing.Point(542, 50)
        Me.ChkePA.Name = "ChkePA"
        Me.ChkePA.Size = New System.Drawing.Size(48, 18)
        Me.ChkePA.TabIndex = 12
        Me.ChkePA.Text = "ePA"
        Me.ChkePA.UseVisualStyleBackColor = True
        '
        'lblPreDetails
        '
        Me.lblPreDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPreDetails.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreDetails.Location = New System.Drawing.Point(4, 95)
        Me.lblPreDetails.Name = "lblPreDetails"
        Me.lblPreDetails.Size = New System.Drawing.Size(766, 1)
        Me.lblPreDetails.TabIndex = 197
        Me.lblPreDetails.Text = "label2"
        Me.lblPreDetails.Visible = False
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(4, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(124, 22)
        Me.Label23.TabIndex = 193
        Me.Label23.Text = "Prescriber Details :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chckDisable
        '
        Me.chckDisable.AutoSize = True
        Me.chckDisable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chckDisable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckDisable.Location = New System.Drawing.Point(133, 27)
        Me.chckDisable.Name = "chckDisable"
        Me.chckDisable.Size = New System.Drawing.Size(63, 18)
        Me.chckDisable.TabIndex = 3
        Me.chckDisable.Text = "Disable"
        Me.chckDisable.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(334, 75)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(104, 14)
        Me.Label32.TabIndex = 80
        Me.Label32.Text = "Active End Date :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(4, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(766, 1)
        Me.Label84.TabIndex = 190
        Me.Label84.Text = "label1"
        '
        'ChkCIEvent
        '
        Me.ChkCIEvent.AutoSize = True
        Me.ChkCIEvent.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkCIEvent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkCIEvent.Location = New System.Drawing.Point(682, 71)
        Me.ChkCIEvent.Name = "ChkCIEvent"
        Me.ChkCIEvent.Size = New System.Drawing.Size(69, 18)
        Me.ChkCIEvent.TabIndex = 13
        Me.ChkCIEvent.Text = "CIEvent"
        Me.ChkCIEvent.UseVisualStyleBackColor = True
        Me.ChkCIEvent.Visible = False
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(18, 75)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(110, 14)
        Me.Label31.TabIndex = 79
        Me.Label31.Text = "Active Start Date :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label85.Location = New System.Drawing.Point(770, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 96)
        Me.Label85.TabIndex = 189
        Me.Label85.Text = "label3"
        '
        'dtpActiveEndTime
        '
        Me.dtpActiveEndTime.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveEndTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpActiveEndTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpActiveEndTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpActiveEndTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpActiveEndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpActiveEndTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtpActiveEndTime.CustomFormat = "MM/dd/yyyy hh:mm:tt"
        Me.dtpActiveEndTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActiveEndTime.Location = New System.Drawing.Point(443, 71)
        Me.dtpActiveEndTime.Name = "dtpActiveEndTime"
        Me.dtpActiveEndTime.Size = New System.Drawing.Size(172, 22)
        Me.dtpActiveEndTime.TabIndex = 11
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.Location = New System.Drawing.Point(3, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1, 96)
        Me.Label86.TabIndex = 1
        Me.Label86.Text = "label4"
        '
        'dtpActiveStartTime
        '
        Me.dtpActiveStartTime.CalendarFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveStartTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpActiveStartTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpActiveStartTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpActiveStartTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpActiveStartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpActiveStartTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtpActiveStartTime.CustomFormat = " MM/dd/yyyy hh:mm:tt"
        Me.dtpActiveStartTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpActiveStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpActiveStartTime.Location = New System.Drawing.Point(139, 71)
        Me.dtpActiveStartTime.Name = "dtpActiveStartTime"
        Me.dtpActiveStartTime.Size = New System.Drawing.Size(171, 22)
        Me.dtpActiveStartTime.TabIndex = 10
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(42, 28)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(86, 14)
        Me.Label30.TabIndex = 76
        Me.Label30.Text = "Service Level :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rbUpdate
        '
        Me.rbUpdate.BackColor = System.Drawing.Color.Transparent
        Me.rbUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUpdate.Location = New System.Drawing.Point(487, 3)
        Me.rbUpdate.Name = "rbUpdate"
        Me.rbUpdate.Size = New System.Drawing.Size(211, 19)
        Me.rbUpdate.TabIndex = 2
        Me.rbUpdate.Text = "Update Prescriber on Location"
        Me.rbUpdate.UseVisualStyleBackColor = False
        '
        'ChckEPCS
        '
        Me.ChckEPCS.AutoSize = True
        Me.ChckEPCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChckEPCS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChckEPCS.Location = New System.Drawing.Point(375, 27)
        Me.ChckEPCS.Name = "ChckEPCS"
        Me.ChckEPCS.Size = New System.Drawing.Size(142, 18)
        Me.ChckEPCS.TabIndex = 6
        Me.ChckEPCS.Text = "Controlled Substance"
        Me.ChckEPCS.UseVisualStyleBackColor = True
        '
        'chkCIMessage
        '
        Me.chkCIMessage.AutoSize = True
        Me.chkCIMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCIMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCIMessage.Location = New System.Drawing.Point(542, 27)
        Me.chkCIMessage.Name = "chkCIMessage"
        Me.chkCIMessage.Size = New System.Drawing.Size(117, 18)
        Me.chkCIMessage.TabIndex = 7
        Me.chkCIMessage.Text = "DIRECT Message"
        Me.chkCIMessage.UseVisualStyleBackColor = True
        '
        'chckRefill
        '
        Me.chckRefill.AutoSize = True
        Me.chckRefill.Cursor = System.Windows.Forms.Cursors.Default
        Me.chckRefill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckRefill.Location = New System.Drawing.Point(291, 50)
        Me.chckRefill.Name = "chckRefill"
        Me.chckRefill.Size = New System.Drawing.Size(50, 18)
        Me.chckRefill.TabIndex = 10
        Me.chckRefill.Text = "Refill"
        Me.chckRefill.UseVisualStyleBackColor = True
        '
        'rbPrescriberLocation
        '
        Me.rbPrescriberLocation.BackColor = System.Drawing.Color.Transparent
        Me.rbPrescriberLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbPrescriberLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrescriberLocation.Location = New System.Drawing.Point(270, 3)
        Me.rbPrescriberLocation.Name = "rbPrescriberLocation"
        Me.rbPrescriberLocation.Size = New System.Drawing.Size(176, 19)
        Me.rbPrescriberLocation.TabIndex = 1
        Me.rbPrescriberLocation.Text = "Add Prescriber on Location"
        Me.rbPrescriberLocation.UseVisualStyleBackColor = False
        '
        'chckNew
        '
        Me.chckNew.AutoSize = True
        Me.chckNew.Cursor = System.Windows.Forms.Cursors.Default
        Me.chckNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckNew.Location = New System.Drawing.Point(204, 27)
        Me.chckNew.Name = "chckNew"
        Me.chckNew.Size = New System.Drawing.Size(51, 18)
        Me.chckNew.TabIndex = 4
        Me.chckNew.Text = "New"
        Me.chckNew.UseVisualStyleBackColor = True
        '
        'rbPrescriber
        '
        Me.rbPrescriber.BackColor = System.Drawing.Color.Transparent
        Me.rbPrescriber.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbPrescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPrescriber.Location = New System.Drawing.Point(133, 3)
        Me.rbPrescriber.Name = "rbPrescriber"
        Me.rbPrescriber.Size = New System.Drawing.Size(127, 19)
        Me.rbPrescriber.TabIndex = 0
        Me.rbPrescriber.Text = "Add Prescriber"
        Me.rbPrescriber.UseVisualStyleBackColor = False
        '
        'pnlSPI
        '
        Me.pnlSPI.AutoScroll = True
        Me.pnlSPI.Controls.Add(Me.txtDirectAddress)
        Me.pnlSPI.Controls.Add(Me.lblDirectAddressValue)
        Me.pnlSPI.Controls.Add(Me.lblDirectAddress)
        Me.pnlSPI.Controls.Add(Me.Label128)
        Me.pnlSPI.Controls.Add(Me.Label127)
        Me.pnlSPI.Controls.Add(Me.Label126)
        Me.pnlSPI.Controls.Add(Me.lblSPI)
        Me.pnlSPI.Controls.Add(Me.txtSPI)
        Me.pnlSPI.Controls.Add(Me.lblRoot)
        Me.pnlSPI.Controls.Add(Me.lblLabelSPI)
        Me.pnlSPI.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSPI.Location = New System.Drawing.Point(0, 96)
        Me.pnlSPI.Name = "pnlSPI"
        Me.pnlSPI.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSPI.Size = New System.Drawing.Size(774, 55)
        Me.pnlSPI.TabIndex = 8
        '
        'txtDirectAddress
        '
        Me.txtDirectAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirectAddress.ForeColor = System.Drawing.Color.Black
        Me.txtDirectAddress.Location = New System.Drawing.Point(133, 27)
        Me.txtDirectAddress.MaxLength = 100
        Me.txtDirectAddress.Name = "txtDirectAddress"
        Me.txtDirectAddress.Size = New System.Drawing.Size(162, 22)
        Me.txtDirectAddress.TabIndex = 2
        '
        'lblDirectAddressValue
        '
        Me.lblDirectAddressValue.AutoSize = True
        Me.lblDirectAddressValue.BackColor = System.Drawing.Color.Transparent
        Me.lblDirectAddressValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectAddressValue.Location = New System.Drawing.Point(297, 31)
        Me.lblDirectAddressValue.Name = "lblDirectAddressValue"
        Me.lblDirectAddressValue.Size = New System.Drawing.Size(68, 14)
        Me.lblDirectAddressValue.TabIndex = 194
        Me.lblDirectAddressValue.Text = "GLOSREAM"
        '
        'lblDirectAddress
        '
        Me.lblDirectAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblDirectAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectAddress.Location = New System.Drawing.Point(34, 31)
        Me.lblDirectAddress.Name = "lblDirectAddress"
        Me.lblDirectAddress.Size = New System.Drawing.Size(94, 14)
        Me.lblDirectAddress.TabIndex = 193
        Me.lblDirectAddress.Text = "Direct Address :"
        Me.lblDirectAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label128.Location = New System.Drawing.Point(4, 51)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(766, 1)
        Me.Label128.TabIndex = 192
        Me.Label128.Text = "label2"
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label127.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label127.Location = New System.Drawing.Point(770, 0)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1, 52)
        Me.Label127.TabIndex = 84
        Me.Label127.Text = "label4"
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label126.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label126.Location = New System.Drawing.Point(3, 0)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1, 52)
        Me.Label126.TabIndex = 83
        Me.Label126.Text = "label4"
        '
        'lblSPI
        '
        Me.lblSPI.AutoSize = True
        Me.lblSPI.BackColor = System.Drawing.Color.Transparent
        Me.lblSPI.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSPI.Location = New System.Drawing.Point(440, 6)
        Me.lblSPI.Name = "lblSPI"
        Me.lblSPI.Size = New System.Drawing.Size(0, 14)
        Me.lblSPI.TabIndex = 82
        '
        'txtSPI
        '
        Me.txtSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSPI.ForeColor = System.Drawing.Color.Black
        Me.txtSPI.Location = New System.Drawing.Point(133, 3)
        Me.txtSPI.MaxLength = 10
        Me.txtSPI.Name = "txtSPI"
        Me.txtSPI.Size = New System.Drawing.Size(161, 22)
        Me.txtSPI.TabIndex = 0
        '
        'lblRoot
        '
        Me.lblRoot.BackColor = System.Drawing.Color.Transparent
        Me.lblRoot.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoot.Location = New System.Drawing.Point(65, 7)
        Me.lblRoot.Name = "lblRoot"
        Me.lblRoot.Size = New System.Drawing.Size(63, 14)
        Me.lblRoot.TabIndex = 71
        Me.lblRoot.Text = "Root SPI :"
        Me.lblRoot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabelSPI
        '
        Me.lblLabelSPI.BackColor = System.Drawing.Color.Transparent
        Me.lblLabelSPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabelSPI.Location = New System.Drawing.Point(359, 7)
        Me.lblLabelSPI.Name = "lblLabelSPI"
        Me.lblLabelSPI.Size = New System.Drawing.Size(78, 14)
        Me.lblLabelSPI.TabIndex = 73
        Me.lblLabelSPI.Text = "Current SPI :"
        Me.lblLabelSPI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkRequire_Supervising_Provider_for_eRx
        '
        Me.chkRequire_Supervising_Provider_for_eRx.AutoSize = True
        Me.chkRequire_Supervising_Provider_for_eRx.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRequire_Supervising_Provider_for_eRx.Enabled = False
        Me.chkRequire_Supervising_Provider_for_eRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRequire_Supervising_Provider_for_eRx.Location = New System.Drawing.Point(134, 4)
        Me.chkRequire_Supervising_Provider_for_eRx.Name = "chkRequire_Supervising_Provider_for_eRx"
        Me.chkRequire_Supervising_Provider_for_eRx.Size = New System.Drawing.Size(216, 18)
        Me.chkRequire_Supervising_Provider_for_eRx.TabIndex = 0
        Me.chkRequire_Supervising_Provider_for_eRx.Text = "Require Supervising Provider for Rx"
        Me.chkRequire_Supervising_Provider_for_eRx.UseVisualStyleBackColor = True
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.Controls.Add(Me.Panel9)
        Me.Panel17.Controls.Add(Me.Panel4)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel17.Size = New System.Drawing.Size(405, 444)
        Me.Panel17.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel9.Controls.Add(Me.Label171)
        Me.Panel9.Controls.Add(Me.lblNPIMandatory)
        Me.Panel9.Controls.Add(Me.txtDPSNumber)
        Me.Panel9.Controls.Add(Me.txtExternalCode)
        Me.Panel9.Controls.Add(Me.txtDEA)
        Me.Panel9.Controls.Add(Me.txtNADEAN)
        Me.Panel9.Controls.Add(Me.txtUPIN)
        Me.Panel9.Controls.Add(Me.Label77)
        Me.Panel9.Controls.Add(Me.Label78)
        Me.Panel9.Controls.Add(Me.txtLabId)
        Me.Panel9.Controls.Add(Me.Label79)
        Me.Panel9.Controls.Add(Me.mskMobileNo)
        Me.Panel9.Controls.Add(Me.Label80)
        Me.Panel9.Controls.Add(Me.mtxt_SSNno)
        Me.Panel9.Controls.Add(Me.txtTaxonomy)
        Me.Panel9.Controls.Add(Me.lblDEA)
        Me.Panel9.Controls.Add(Me.btn_ClearTaxonomy)
        Me.Panel9.Controls.Add(Me.cmbDoctorType)
        Me.Panel9.Controls.Add(Me.btn_BrowseTaxonomy)
        Me.Panel9.Controls.Add(Me.txtStateMedicalLicenseNo)
        Me.Panel9.Controls.Add(Me.lblStateMedicalLicense)
        Me.Panel9.Controls.Add(Me.txt_EmployerID)
        Me.Panel9.Controls.Add(Me.label28)
        Me.Panel9.Controls.Add(Me.txtNPI)
        Me.Panel9.Controls.Add(Me.lblUPIN)
        Me.Panel9.Controls.Add(Me.lblNPI)
        Me.Panel9.Controls.Add(Me.lblExternalCode)
        Me.Panel9.Controls.Add(Me.label8)
        Me.Panel9.Controls.Add(Me.lblMobileNo)
        Me.Panel9.Controls.Add(Me.label27)
        Me.Panel9.Controls.Add(Me.lblLabID)
        Me.Panel9.Controls.Add(Me.lblType)
        Me.Panel9.Controls.Add(Me.lblDPSNumber)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(3, 246)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel9.Size = New System.Drawing.Size(399, 195)
        Me.Panel9.TabIndex = 1
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.Transparent
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label171.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label171.Location = New System.Drawing.Point(245, 147)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(68, 14)
        Me.Label171.TabIndex = 197
        Me.Label171.Text = "NADEAN :"
        Me.Label171.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNPIMandatory
        '
        Me.lblNPIMandatory.AutoSize = True
        Me.lblNPIMandatory.ForeColor = System.Drawing.Color.Red
        Me.lblNPIMandatory.Location = New System.Drawing.Point(269, 41)
        Me.lblNPIMandatory.Name = "lblNPIMandatory"
        Me.lblNPIMandatory.Size = New System.Drawing.Size(14, 14)
        Me.lblNPIMandatory.TabIndex = 196
        Me.lblNPIMandatory.Text = "*"
        Me.lblNPIMandatory.Visible = False
        '
        'txtDPSNumber
        '
        Me.txtDPSNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDPSNumber.ForeColor = System.Drawing.Color.Black
        Me.txtDPSNumber.Location = New System.Drawing.Point(274, 169)
        Me.txtDPSNumber.MaxLength = 16
        Me.txtDPSNumber.Name = "txtDPSNumber"
        Me.txtDPSNumber.Size = New System.Drawing.Size(122, 22)
        Me.txtDPSNumber.TabIndex = 14
        '
        'txtExternalCode
        '
        Me.txtExternalCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExternalCode.ForeColor = System.Drawing.Color.Black
        Me.txtExternalCode.Location = New System.Drawing.Point(93, 143)
        Me.txtExternalCode.MaxLength = 500
        Me.txtExternalCode.Name = "txtExternalCode"
        Me.txtExternalCode.Size = New System.Drawing.Size(98, 22)
        Me.txtExternalCode.TabIndex = 12
        '
        'txtDEA
        '
        Me.txtDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDEA.Location = New System.Drawing.Point(314, 118)
        Me.txtDEA.Mask = "LL0000000"
        Me.txtDEA.Name = "txtDEA"
        Me.txtDEA.Size = New System.Drawing.Size(82, 22)
        Me.txtDEA.TabIndex = 11
        '
        'txtNADEAN
        '
        Me.txtNADEAN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNADEAN.Location = New System.Drawing.Point(314, 143)
        Me.txtNADEAN.Mask = "LL0000000"
        Me.txtNADEAN.Name = "txtNADEAN"
        Me.txtNADEAN.Size = New System.Drawing.Size(82, 22)
        Me.txtNADEAN.TabIndex = 13
        '
        'txtUPIN
        '
        Me.txtUPIN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUPIN.Location = New System.Drawing.Point(314, 65)
        Me.txtUPIN.Mask = "L00000"
        Me.txtUPIN.Name = "txtUPIN"
        Me.txtUPIN.Size = New System.Drawing.Size(82, 22)
        Me.txtUPIN.TabIndex = 7
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label77.Location = New System.Drawing.Point(1, 194)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(397, 1)
        Me.Label77.TabIndex = 191
        Me.Label77.Text = "label2"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(1, 3)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(397, 1)
        Me.Label78.TabIndex = 190
        Me.Label78.Text = "label1"
        '
        'txtLabId
        '
        Me.txtLabId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabId.ForeColor = System.Drawing.Color.Black
        Me.txtLabId.Location = New System.Drawing.Point(93, 118)
        Me.txtLabId.MaxLength = 48
        Me.txtLabId.Name = "txtLabId"
        Me.txtLabId.Size = New System.Drawing.Size(98, 22)
        Me.txtLabId.TabIndex = 10
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label79.Location = New System.Drawing.Point(398, 3)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 192)
        Me.Label79.TabIndex = 189
        Me.Label79.Text = "label3"
        '
        'mskMobileNo
        '
        Me.mskMobileNo.AllowValidate = True
        Me.mskMobileNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskMobileNo.IncludeLiteralsAndPrompts = False
        Me.mskMobileNo.Location = New System.Drawing.Point(93, 13)
        Me.mskMobileNo.MaskType = gloMaskControl.gloMaskType.Mobile
        Me.mskMobileNo.Name = "mskMobileNo"
        Me.mskMobileNo.ReadOnly = False
        Me.mskMobileNo.Size = New System.Drawing.Size(91, 23)
        Me.mskMobileNo.TabIndex = 0
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(0, 3)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 192)
        Me.Label80.TabIndex = 1
        Me.Label80.Text = "label4"
        '
        'mtxt_SSNno
        '
        Me.mtxt_SSNno.AllowValidate = True
        Me.mtxt_SSNno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtxt_SSNno.ForeColor = System.Drawing.Color.Black
        Me.mtxt_SSNno.IncludeLiteralsAndPrompts = False
        Me.mtxt_SSNno.Location = New System.Drawing.Point(314, 91)
        Me.mtxt_SSNno.MaskType = gloMaskControl.gloMaskType.SSN
        Me.mtxt_SSNno.Name = "mtxt_SSNno"
        Me.mtxt_SSNno.ReadOnly = False
        Me.mtxt_SSNno.Size = New System.Drawing.Size(82, 22)
        Me.mtxt_SSNno.TabIndex = 9
        '
        'txtTaxonomy
        '
        Me.txtTaxonomy.BackColor = System.Drawing.Color.White
        Me.txtTaxonomy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxonomy.ForeColor = System.Drawing.Color.Black
        Me.txtTaxonomy.Location = New System.Drawing.Point(93, 40)
        Me.txtTaxonomy.MaxLength = 99
        Me.txtTaxonomy.Name = "txtTaxonomy"
        Me.txtTaxonomy.ReadOnly = True
        Me.txtTaxonomy.Size = New System.Drawing.Size(122, 22)
        Me.txtTaxonomy.TabIndex = 2
        '
        'lblDEA
        '
        Me.lblDEA.BackColor = System.Drawing.Color.Transparent
        Me.lblDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDEA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDEA.Location = New System.Drawing.Point(275, 122)
        Me.lblDEA.Name = "lblDEA"
        Me.lblDEA.Size = New System.Drawing.Size(38, 14)
        Me.lblDEA.TabIndex = 108
        Me.lblDEA.Text = "DEA :"
        Me.lblDEA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_ClearTaxonomy
        '
        Me.btn_ClearTaxonomy.BackgroundImage = CType(resources.GetObject("btn_ClearTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_ClearTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ClearTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ClearTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearTaxonomy.Image = CType(resources.GetObject("btn_ClearTaxonomy.Image"), System.Drawing.Image)
        Me.btn_ClearTaxonomy.Location = New System.Drawing.Point(245, 40)
        Me.btn_ClearTaxonomy.Name = "btn_ClearTaxonomy"
        Me.btn_ClearTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_ClearTaxonomy.TabIndex = 4
        Me.btn_ClearTaxonomy.UseVisualStyleBackColor = True
        '
        'cmbDoctorType
        '
        Me.cmbDoctorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoctorType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoctorType.ForeColor = System.Drawing.Color.Black
        Me.cmbDoctorType.Location = New System.Drawing.Point(93, 92)
        Me.cmbDoctorType.Name = "cmbDoctorType"
        Me.cmbDoctorType.Size = New System.Drawing.Size(153, 22)
        Me.cmbDoctorType.TabIndex = 8
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
        Me.btn_BrowseTaxonomy.Location = New System.Drawing.Point(220, 40)
        Me.btn_BrowseTaxonomy.Name = "btn_BrowseTaxonomy"
        Me.btn_BrowseTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_BrowseTaxonomy.TabIndex = 3
        Me.btn_BrowseTaxonomy.UseVisualStyleBackColor = True
        '
        'txtStateMedicalLicenseNo
        '
        Me.txtStateMedicalLicenseNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStateMedicalLicenseNo.ForeColor = System.Drawing.Color.Black
        Me.txtStateMedicalLicenseNo.Location = New System.Drawing.Point(314, 13)
        Me.txtStateMedicalLicenseNo.MaxLength = 10
        Me.txtStateMedicalLicenseNo.Name = "txtStateMedicalLicenseNo"
        Me.txtStateMedicalLicenseNo.Size = New System.Drawing.Size(82, 22)
        Me.txtStateMedicalLicenseNo.TabIndex = 1
        '
        'lblStateMedicalLicense
        '
        Me.lblStateMedicalLicense.BackColor = System.Drawing.Color.Transparent
        Me.lblStateMedicalLicense.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStateMedicalLicense.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblStateMedicalLicense.Location = New System.Drawing.Point(181, 17)
        Me.lblStateMedicalLicense.Name = "lblStateMedicalLicense"
        Me.lblStateMedicalLicense.Size = New System.Drawing.Size(132, 14)
        Me.lblStateMedicalLicense.TabIndex = 118
        Me.lblStateMedicalLicense.Text = "State Medical License :"
        Me.lblStateMedicalLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_EmployerID
        '
        Me.txt_EmployerID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_EmployerID.ForeColor = System.Drawing.Color.Black
        Me.txt_EmployerID.Location = New System.Drawing.Point(93, 66)
        Me.txt_EmployerID.MaxLength = 9
        Me.txt_EmployerID.Name = "txt_EmployerID"
        Me.txt_EmployerID.Size = New System.Drawing.Size(153, 22)
        Me.txt_EmployerID.TabIndex = 6
        '
        'label28
        '
        Me.label28.BackColor = System.Drawing.Color.Transparent
        Me.label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label28.Location = New System.Drawing.Point(272, 94)
        Me.label28.Name = "label28"
        Me.label28.Size = New System.Drawing.Size(41, 14)
        Me.label28.TabIndex = 184
        Me.label28.Text = "SSN  :"
        Me.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNPI
        '
        Me.txtNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNPI.ForeColor = System.Drawing.Color.Black
        Me.txtNPI.Location = New System.Drawing.Point(314, 39)
        Me.txtNPI.MaxLength = 10
        Me.txtNPI.Name = "txtNPI"
        Me.txtNPI.Size = New System.Drawing.Size(82, 22)
        Me.txtNPI.TabIndex = 5
        '
        'lblUPIN
        '
        Me.lblUPIN.BackColor = System.Drawing.Color.Transparent
        Me.lblUPIN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUPIN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUPIN.Location = New System.Drawing.Point(271, 68)
        Me.lblUPIN.Name = "lblUPIN"
        Me.lblUPIN.Size = New System.Drawing.Size(42, 14)
        Me.lblUPIN.TabIndex = 117
        Me.lblUPIN.Text = "UPIN :"
        Me.lblUPIN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNPI
        '
        Me.lblNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNPI.Location = New System.Drawing.Point(279, 42)
        Me.lblNPI.Name = "lblNPI"
        Me.lblNPI.Size = New System.Drawing.Size(34, 14)
        Me.lblNPI.TabIndex = 116
        Me.lblNPI.Text = "NPI :"
        Me.lblNPI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExternalCode
        '
        Me.lblExternalCode.BackColor = System.Drawing.Color.Transparent
        Me.lblExternalCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExternalCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblExternalCode.Location = New System.Drawing.Point(1, 147)
        Me.lblExternalCode.Name = "lblExternalCode"
        Me.lblExternalCode.Size = New System.Drawing.Size(91, 14)
        Me.lblExternalCode.TabIndex = 195
        Me.lblExternalCode.Text = "External Code :"
        Me.lblExternalCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label8
        '
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label8.Location = New System.Drawing.Point(20, 43)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 14)
        Me.label8.TabIndex = 122
        Me.label8.Text = "Taxonomy :"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMobileNo
        '
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMobileNo.Location = New System.Drawing.Point(24, 17)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(68, 14)
        Me.lblMobileNo.TabIndex = 96
        Me.lblMobileNo.Text = "Mobile No :"
        Me.lblMobileNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label27
        '
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label27.Location = New System.Drawing.Point(11, 69)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(81, 14)
        Me.label27.TabIndex = 186
        Me.label27.Text = "Employer ID :"
        Me.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabID
        '
        Me.lblLabID.BackColor = System.Drawing.Color.Transparent
        Me.lblLabID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLabID.Location = New System.Drawing.Point(42, 121)
        Me.lblLabID.Name = "lblLabID"
        Me.lblLabID.Size = New System.Drawing.Size(50, 14)
        Me.lblLabID.TabIndex = 190
        Me.lblLabID.Text = "Lab ID :"
        Me.lblLabID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(49, 95)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(43, 14)
        Me.lblType.TabIndex = 115
        Me.lblType.Text = "Type :"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDPSNumber
        '
        Me.lblDPSNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblDPSNumber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDPSNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDPSNumber.Location = New System.Drawing.Point(202, 173)
        Me.lblDPSNumber.Name = "lblDPSNumber"
        Me.lblDPSNumber.Size = New System.Drawing.Size(71, 14)
        Me.lblDPSNumber.TabIndex = 195
        Me.lblDPSNumber.Text = "State Rx# :"
        Me.lblDPSNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.Controls.Add(Me.txtBMContact)
        Me.Panel4.Controls.Add(Me.Label52)
        Me.Panel4.Controls.Add(Me.txtBMPager)
        Me.Panel4.Controls.Add(Me.lblFax)
        Me.Panel4.Controls.Add(Me.Label53)
        Me.Panel4.Controls.Add(Me.txtBMFAX)
        Me.Panel4.Controls.Add(Me.Label54)
        Me.Panel4.Controls.Add(Me.lblPager)
        Me.Panel4.Controls.Add(Me.Label55)
        Me.Panel4.Controls.Add(Me.pnlBussinessAddresssControl)
        Me.Panel4.Controls.Add(Me.Label56)
        Me.Panel4.Controls.Add(Me.pnlInternalControl)
        Me.Panel4.Controls.Add(Me.label26)
        Me.Panel4.Controls.Add(Me.cmbBMstate)
        Me.Panel4.Controls.Add(Me.lblURL)
        Me.Panel4.Controls.Add(Me.mskBMPhoneNo)
        Me.Panel4.Controls.Add(Me.txtBMEmailAddress)
        Me.Panel4.Controls.Add(Me.txtBMURL)
        Me.Panel4.Controls.Add(Me.lblPhoneNo)
        Me.Panel4.Controls.Add(Me.txtBMAddress1)
        Me.Panel4.Controls.Add(Me.txtBMZip)
        Me.Panel4.Controls.Add(Me.label1)
        Me.Panel4.Controls.Add(Me.txtBMCity)
        Me.Panel4.Controls.Add(Me.label2)
        Me.Panel4.Controls.Add(Me.txtBMAddress2)
        Me.Panel4.Controls.Add(Me.label3)
        Me.Panel4.Controls.Add(Me.lblEmail)
        Me.Panel4.Controls.Add(Me.label5)
        Me.Panel4.Controls.Add(Me.label7)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(399, 246)
        Me.Panel4.TabIndex = 0
        '
        'txtBMContact
        '
        Me.txtBMContact.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMContact.ForeColor = System.Drawing.Color.Black
        Me.txtBMContact.Location = New System.Drawing.Point(91, 18)
        Me.txtBMContact.MaxLength = 99
        Me.txtBMContact.Name = "txtBMContact"
        Me.txtBMContact.Size = New System.Drawing.Size(301, 22)
        Me.txtBMContact.TabIndex = 0
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Location = New System.Drawing.Point(1, 1)
        Me.Label52.Name = "Label52"
        Me.Label52.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label52.Size = New System.Drawing.Size(171, 14)
        Me.Label52.TabIndex = 193
        Me.Label52.Text = "Business Mailing Address "
        '
        'txtBMPager
        '
        Me.txtBMPager.AllowValidate = True
        Me.txtBMPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMPager.ForeColor = System.Drawing.Color.Black
        Me.txtBMPager.IncludeLiteralsAndPrompts = False
        Me.txtBMPager.Location = New System.Drawing.Point(302, 148)
        Me.txtBMPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.txtBMPager.Name = "txtBMPager"
        Me.txtBMPager.ReadOnly = False
        Me.txtBMPager.Size = New System.Drawing.Size(91, 22)
        Me.txtBMPager.TabIndex = 2
        '
        'lblFax
        '
        Me.lblFax.BackColor = System.Drawing.Color.Transparent
        Me.lblFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFax.Location = New System.Drawing.Point(268, 177)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(33, 14)
        Me.lblFax.TabIndex = 104
        Me.lblFax.Text = "Fax :"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label53.Location = New System.Drawing.Point(1, 245)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(397, 1)
        Me.Label53.TabIndex = 191
        Me.Label53.Text = "label2"
        '
        'txtBMFAX
        '
        Me.txtBMFAX.AllowValidate = True
        Me.txtBMFAX.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMFAX.ForeColor = System.Drawing.Color.Black
        Me.txtBMFAX.IncludeLiteralsAndPrompts = False
        Me.txtBMFAX.Location = New System.Drawing.Point(302, 173)
        Me.txtBMFAX.MaskType = gloMaskControl.gloMaskType.Fax
        Me.txtBMFAX.Name = "txtBMFAX"
        Me.txtBMFAX.ReadOnly = False
        Me.txtBMFAX.Size = New System.Drawing.Size(91, 22)
        Me.txtBMFAX.TabIndex = 4
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(1, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(397, 1)
        Me.Label54.TabIndex = 190
        Me.Label54.Text = "label1"
        '
        'lblPager
        '
        Me.lblPager.BackColor = System.Drawing.Color.Transparent
        Me.lblPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPager.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPager.Location = New System.Drawing.Point(255, 151)
        Me.lblPager.Name = "lblPager"
        Me.lblPager.Size = New System.Drawing.Size(46, 14)
        Me.lblPager.TabIndex = 109
        Me.lblPager.Text = "Pager :"
        Me.lblPager.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label55.Location = New System.Drawing.Point(398, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 246)
        Me.Label55.TabIndex = 189
        Me.Label55.Text = "label3"
        '
        'pnlBussinessAddresssControl
        '
        Me.pnlBussinessAddresssControl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlBussinessAddresssControl.Location = New System.Drawing.Point(8, 41)
        Me.pnlBussinessAddresssControl.Name = "pnlBussinessAddresssControl"
        Me.pnlBussinessAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlBussinessAddresssControl.TabIndex = 1
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(0, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 246)
        Me.Label56.TabIndex = 1
        Me.Label56.Text = "label4"
        '
        'pnlInternalControl
        '
        Me.pnlInternalControl.Location = New System.Drawing.Point(273, 99)
        Me.pnlInternalControl.Name = "pnlInternalControl"
        Me.pnlInternalControl.Size = New System.Drawing.Size(10, 64)
        Me.pnlInternalControl.TabIndex = 194
        Me.pnlInternalControl.TabStop = True
        Me.pnlInternalControl.Visible = False
        '
        'label26
        '
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label26.Location = New System.Drawing.Point(29, 21)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(58, 14)
        Me.label26.TabIndex = 123
        Me.label26.Text = "Contact :"
        Me.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbBMstate
        '
        Me.cmbBMstate.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbBMstate.FormattingEnabled = True
        Me.cmbBMstate.Location = New System.Drawing.Point(273, 49)
        Me.cmbBMstate.Name = "cmbBMstate"
        Me.cmbBMstate.Size = New System.Drawing.Size(26, 22)
        Me.cmbBMstate.TabIndex = 124
        Me.cmbBMstate.TabStop = False
        Me.cmbBMstate.Visible = False
        '
        'lblURL
        '
        Me.lblURL.BackColor = System.Drawing.Color.Transparent
        Me.lblURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblURL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblURL.Location = New System.Drawing.Point(52, 225)
        Me.lblURL.Name = "lblURL"
        Me.lblURL.Size = New System.Drawing.Size(36, 14)
        Me.lblURL.TabIndex = 106
        Me.lblURL.Text = "URL :"
        Me.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskBMPhoneNo
        '
        Me.mskBMPhoneNo.AllowValidate = True
        Me.mskBMPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskBMPhoneNo.IncludeLiteralsAndPrompts = False
        Me.mskBMPhoneNo.Location = New System.Drawing.Point(91, 173)
        Me.mskBMPhoneNo.MaskType = gloMaskControl.gloMaskType.Phone
        Me.mskBMPhoneNo.Name = "mskBMPhoneNo"
        Me.mskBMPhoneNo.ReadOnly = False
        Me.mskBMPhoneNo.Size = New System.Drawing.Size(100, 22)
        Me.mskBMPhoneNo.TabIndex = 3
        '
        'txtBMEmailAddress
        '
        Me.txtBMEmailAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMEmailAddress.ForeColor = System.Drawing.Color.Black
        Me.txtBMEmailAddress.Location = New System.Drawing.Point(91, 197)
        Me.txtBMEmailAddress.MaxLength = 99
        Me.txtBMEmailAddress.Name = "txtBMEmailAddress"
        Me.txtBMEmailAddress.Size = New System.Drawing.Size(303, 22)
        Me.txtBMEmailAddress.TabIndex = 5
        '
        'txtBMURL
        '
        Me.txtBMURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMURL.ForeColor = System.Drawing.Color.Black
        Me.txtBMURL.Location = New System.Drawing.Point(91, 221)
        Me.txtBMURL.MaxLength = 99
        Me.txtBMURL.Name = "txtBMURL"
        Me.txtBMURL.Size = New System.Drawing.Size(303, 22)
        Me.txtBMURL.TabIndex = 6
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPhoneNo.Location = New System.Drawing.Point(19, 174)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(69, 14)
        Me.lblPhoneNo.TabIndex = 95
        Me.lblPhoneNo.Text = "Phone No :"
        Me.lblPhoneNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMAddress1
        '
        Me.txtBMAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtBMAddress1.Location = New System.Drawing.Point(92, 47)
        Me.txtBMAddress1.MaxLength = 99
        Me.txtBMAddress1.Name = "txtBMAddress1"
        Me.txtBMAddress1.Size = New System.Drawing.Size(10, 22)
        Me.txtBMAddress1.TabIndex = 1
        Me.txtBMAddress1.TabStop = False
        '
        'txtBMZip
        '
        Me.txtBMZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMZip.ForeColor = System.Drawing.Color.Black
        Me.txtBMZip.Location = New System.Drawing.Point(311, 76)
        Me.txtBMZip.MaxLength = 10
        Me.txtBMZip.Name = "txtBMZip"
        Me.txtBMZip.Size = New System.Drawing.Size(10, 22)
        Me.txtBMZip.TabIndex = 5
        Me.txtBMZip.TabStop = False
        Me.txtBMZip.Visible = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(100, 55)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(69, 14)
        Me.label1.TabIndex = 90
        Me.label1.Text = "Address 1 :"
        '
        'txtBMCity
        '
        Me.txtBMCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMCity.ForeColor = System.Drawing.Color.Black
        Me.txtBMCity.Location = New System.Drawing.Point(226, 49)
        Me.txtBMCity.MaxLength = 99
        Me.txtBMCity.Name = "txtBMCity"
        Me.txtBMCity.Size = New System.Drawing.Size(10, 22)
        Me.txtBMCity.TabIndex = 3
        Me.txtBMCity.TabStop = False
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Location = New System.Drawing.Point(167, 52)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(69, 14)
        Me.label2.TabIndex = 91
        Me.label2.Text = "Address 2 :"
        '
        'txtBMAddress2
        '
        Me.txtBMAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtBMAddress2.Location = New System.Drawing.Point(162, 49)
        Me.txtBMAddress2.MaxLength = 99
        Me.txtBMAddress2.Name = "txtBMAddress2"
        Me.txtBMAddress2.Size = New System.Drawing.Size(10, 22)
        Me.txtBMAddress2.TabIndex = 2
        Me.txtBMAddress2.TabStop = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(242, 55)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(35, 14)
        Me.label3.TabIndex = 92
        Me.label3.Text = "City :"
        '
        'lblEmail
        '
        Me.lblEmail.BackColor = System.Drawing.Color.Transparent
        Me.lblEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblEmail.Location = New System.Drawing.Point(46, 201)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(42, 14)
        Me.lblEmail.TabIndex = 105
        Me.lblEmail.Text = "Email :"
        Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label5.Location = New System.Drawing.Point(235, 79)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(45, 14)
        Me.label5.TabIndex = 93
        Me.label5.Text = "State :"
        Me.label5.Visible = False
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label7.Location = New System.Drawing.Point(276, 79)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(31, 14)
        Me.label7.TabIndex = 94
        Me.label7.Text = "Zip :"
        Me.label7.Visible = False
        '
        'grpUserDetails
        '
        Me.grpUserDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grpUserDetails.Controls.Add(Me.Label81)
        Me.grpUserDetails.Controls.Add(Me.Label39)
        Me.grpUserDetails.Controls.Add(Me.Label40)
        Me.grpUserDetails.Controls.Add(Me.Label67)
        Me.grpUserDetails.Controls.Add(Me.Label68)
        Me.grpUserDetails.Controls.Add(Me.lblAusStatus)
        Me.grpUserDetails.Controls.Add(Me.txtUserName)
        Me.grpUserDetails.Controls.Add(Me.txtConfirmPassword)
        Me.grpUserDetails.Controls.Add(Me.Label69)
        Me.grpUserDetails.Controls.Add(Me.Label41)
        Me.grpUserDetails.Controls.Add(Me.Label70)
        Me.grpUserDetails.Controls.Add(Me.txtPassword)
        Me.grpUserDetails.Controls.Add(Me.lblUserName)
        Me.grpUserDetails.Controls.Add(Me.lblConfirmPassword)
        Me.grpUserDetails.Controls.Add(Me.lblNickName)
        Me.grpUserDetails.Controls.Add(Me.txtNickName)
        Me.grpUserDetails.Controls.Add(Me.lblPassword)
        Me.grpUserDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpUserDetails.Location = New System.Drawing.Point(0, 297)
        Me.grpUserDetails.Name = "grpUserDetails"
        Me.grpUserDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.grpUserDetails.Size = New System.Drawing.Size(383, 144)
        Me.grpUserDetails.TabIndex = 1
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.BackColor = System.Drawing.Color.Transparent
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Location = New System.Drawing.Point(6, 7)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(78, 14)
        Me.Label81.TabIndex = 194
        Me.Label81.Text = "User Details"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.ForeColor = System.Drawing.Color.Red
        Me.Label39.Location = New System.Drawing.Point(49, 32)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(14, 14)
        Me.Label39.TabIndex = 194
        Me.Label39.Text = "*"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.ForeColor = System.Drawing.Color.Red
        Me.Label40.Location = New System.Drawing.Point(58, 57)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(14, 14)
        Me.Label40.TabIndex = 195
        Me.Label40.Text = "*"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label67.Location = New System.Drawing.Point(1, 143)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(381, 1)
        Me.Label67.TabIndex = 191
        Me.Label67.Text = "label2"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(1, 3)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(381, 1)
        Me.Label68.TabIndex = 190
        Me.Label68.Text = "label1"
        '
        'lblAusStatus
        '
        Me.lblAusStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAusStatus.Location = New System.Drawing.Point(278, 43)
        Me.lblAusStatus.Name = "lblAusStatus"
        Me.lblAusStatus.Size = New System.Drawing.Size(67, 14)
        Me.lblAusStatus.TabIndex = 196
        Me.lblAusStatus.Text = "aus"
        Me.lblAusStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAusStatus.Visible = False
        '
        'txtUserName
        '
        Me.txtUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.Location = New System.Drawing.Point(138, 27)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(115, 22)
        Me.txtUserName.TabIndex = 0
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirmPassword.ForeColor = System.Drawing.Color.Black
        Me.txtConfirmPassword.Location = New System.Drawing.Point(138, 79)
        Me.txtConfirmPassword.MaxLength = 100
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirmPassword.Size = New System.Drawing.Size(115, 22)
        Me.txtConfirmPassword.TabIndex = 3
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label69.Location = New System.Drawing.Point(382, 3)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 141)
        Me.Label69.TabIndex = 189
        Me.Label69.Text = "label3"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.ForeColor = System.Drawing.Color.Red
        Me.Label41.Location = New System.Drawing.Point(14, 83)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(14, 14)
        Me.Label41.TabIndex = 196
        Me.Label41.Text = "*"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(0, 3)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(1, 141)
        Me.Label70.TabIndex = 1
        Me.Label70.Text = "label4"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.Location = New System.Drawing.Point(138, 53)
        Me.txtPassword.MaxLength = 100
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(115, 22)
        Me.txtPassword.TabIndex = 1
        '
        'lblUserName
        '
        Me.lblUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUserName.Location = New System.Drawing.Point(62, 31)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(74, 14)
        Me.lblUserName.TabIndex = 33
        Me.lblUserName.Text = "User Name :"
        Me.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblConfirmPassword
        '
        Me.lblConfirmPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblConfirmPassword.Location = New System.Drawing.Point(25, 83)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Size = New System.Drawing.Size(111, 14)
        Me.lblConfirmPassword.TabIndex = 3
        Me.lblConfirmPassword.Text = "Confirm Password :"
        Me.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNickName
        '
        Me.lblNickName.AutoSize = True
        Me.lblNickName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNickName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNickName.Location = New System.Drawing.Point(129, 172)
        Me.lblNickName.Name = "lblNickName"
        Me.lblNickName.Size = New System.Drawing.Size(72, 14)
        Me.lblNickName.TabIndex = 37
        Me.lblNickName.Text = "Nick Name :"
        Me.lblNickName.Visible = False
        '
        'txtNickName
        '
        Me.txtNickName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNickName.ForeColor = System.Drawing.Color.Black
        Me.txtNickName.Location = New System.Drawing.Point(206, 181)
        Me.txtNickName.MaxLength = 50
        Me.txtNickName.Name = "txtNickName"
        Me.txtNickName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNickName.Size = New System.Drawing.Size(115, 22)
        Me.txtNickName.TabIndex = 2
        Me.txtNickName.Visible = False
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPassword.Location = New System.Drawing.Point(70, 57)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(66, 14)
        Me.lblPassword.TabIndex = 34
        Me.lblPassword.Text = "Password :"
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicenseKey.ForeColor = System.Drawing.Color.Black
        Me.txtLicenseKey.Location = New System.Drawing.Point(95, 4)
        Me.txtLicenseKey.MaxLength = 1000
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(654, 22)
        Me.txtLicenseKey.TabIndex = 197
        '
        'chkAddressasAbove
        '
        Me.chkAddressasAbove.AutoSize = True
        Me.chkAddressasAbove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAddressasAbove.Location = New System.Drawing.Point(578, 37)
        Me.chkAddressasAbove.Name = "chkAddressasAbove"
        Me.chkAddressasAbove.Size = New System.Drawing.Size(166, 18)
        Me.chkAddressasAbove.TabIndex = 1
        Me.chkAddressasAbove.Text = "Same as Provider Address"
        Me.chkAddressasAbove.UseVisualStyleBackColor = True
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.BackColor = System.Drawing.Color.Transparent
        Me.label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label13.Location = New System.Drawing.Point(420, 168)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(46, 14)
        Me.label13.TabIndex = 119
        Me.label13.Text = "Pager :"
        '
        'pnlPracticeAddresssControl
        '
        Me.pnlPracticeAddresssControl.BackColor = System.Drawing.Color.Transparent
        Me.pnlPracticeAddresssControl.Location = New System.Drawing.Point(161, 58)
        Me.pnlPracticeAddresssControl.Name = "pnlPracticeAddresssControl"
        Me.pnlPracticeAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlPracticeAddresssControl.TabIndex = 2
        '
        'lblAddress1
        '
        Me.lblAddress1.AutoSize = True
        Me.lblAddress1.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAddress1.Location = New System.Drawing.Point(396, 68)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(69, 14)
        Me.lblAddress1.TabIndex = 90
        Me.lblAddress1.Text = "Address 1 :"
        Me.lblAddress1.Visible = False
        '
        'pnlBPractInternalControl
        '
        Me.pnlBPractInternalControl.Location = New System.Drawing.Point(470, 98)
        Me.pnlBPractInternalControl.Name = "pnlBPractInternalControl"
        Me.pnlBPractInternalControl.Size = New System.Drawing.Size(10, 19)
        Me.pnlBPractInternalControl.TabIndex = 198
        Me.pnlBPractInternalControl.Visible = False
        '
        'cmbBPracState
        '
        Me.cmbBPracState.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbBPracState.FormattingEnabled = True
        Me.cmbBPracState.Location = New System.Drawing.Point(361, 119)
        Me.cmbBPracState.Name = "cmbBPracState"
        Me.cmbBPracState.Size = New System.Drawing.Size(36, 22)
        Me.cmbBPracState.TabIndex = 122
        Me.cmbBPracState.Visible = False
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.Color.Transparent
        Me.label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Location = New System.Drawing.Point(171, 194)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(69, 14)
        Me.label11.TabIndex = 111
        Me.label11.Text = "Phone No :"
        '
        'txtBPracContactName
        '
        Me.txtBPracContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracContactName.ForeColor = System.Drawing.Color.Black
        Me.txtBPracContactName.Location = New System.Drawing.Point(244, 35)
        Me.txtBPracContactName.MaxLength = 99
        Me.txtBPracContactName.Name = "txtBPracContactName"
        Me.txtBPracContactName.Size = New System.Drawing.Size(325, 22)
        Me.txtBPracContactName.TabIndex = 0
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblState.Location = New System.Drawing.Point(420, 119)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 14)
        Me.lblState.TabIndex = 93
        Me.lblState.Text = "State :"
        Me.lblState.Visible = False
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.BackColor = System.Drawing.Color.Transparent
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label25.Location = New System.Drawing.Point(183, 39)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(58, 14)
        Me.label25.TabIndex = 121
        Me.label25.Text = "Contact :"
        '
        'txtBPracZIP
        '
        Me.txtBPracZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracZIP.ForeColor = System.Drawing.Color.Black
        Me.txtBPracZIP.Location = New System.Drawing.Point(464, 73)
        Me.txtBPracZIP.MaxLength = 10
        Me.txtBPracZIP.Name = "txtBPracZIP"
        Me.txtBPracZIP.Size = New System.Drawing.Size(10, 22)
        Me.txtBPracZIP.TabIndex = 6
        Me.txtBPracZIP.Visible = False
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.Color.Transparent
        Me.label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Location = New System.Drawing.Point(433, 194)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(33, 14)
        Me.label9.TabIndex = 116
        Me.label9.Text = "Fax :"
        '
        'txtBPracCity
        '
        Me.txtBPracCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracCity.ForeColor = System.Drawing.Color.Black
        Me.txtBPracCity.Location = New System.Drawing.Point(260, 119)
        Me.txtBPracCity.MaxLength = 99
        Me.txtBPracCity.Name = "txtBPracCity"
        Me.txtBPracCity.Size = New System.Drawing.Size(34, 22)
        Me.txtBPracCity.TabIndex = 4
        Me.txtBPracCity.Visible = False
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.BackColor = System.Drawing.Color.Transparent
        Me.label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Location = New System.Drawing.Point(198, 220)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(42, 14)
        Me.label10.TabIndex = 117
        Me.label10.Text = "Email :"
        '
        'txtBPracAddress2
        '
        Me.txtBPracAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtBPracAddress2.Location = New System.Drawing.Point(260, 95)
        Me.txtBPracAddress2.MaxLength = 99
        Me.txtBPracAddress2.Name = "txtBPracAddress2"
        Me.txtBPracAddress2.Size = New System.Drawing.Size(137, 22)
        Me.txtBPracAddress2.TabIndex = 3
        Me.txtBPracAddress2.Visible = False
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblZip.Location = New System.Drawing.Point(449, 77)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(31, 14)
        Me.lblZip.TabIndex = 94
        Me.lblZip.Text = "Zip :"
        Me.lblZip.Visible = False
        '
        'txtBPracUrl
        '
        Me.txtBPracUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracUrl.ForeColor = System.Drawing.Color.Black
        Me.txtBPracUrl.Location = New System.Drawing.Point(243, 241)
        Me.txtBPracUrl.MaxLength = 99
        Me.txtBPracUrl.Name = "txtBPracUrl"
        Me.txtBPracUrl.Size = New System.Drawing.Size(325, 22)
        Me.txtBPracUrl.TabIndex = 8
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCity.Location = New System.Drawing.Point(305, 126)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 14)
        Me.lblCity.TabIndex = 92
        Me.lblCity.Text = "City :"
        Me.lblCity.Visible = False
        '
        'txtBPracEMail
        '
        Me.txtBPracEMail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracEMail.ForeColor = System.Drawing.Color.Black
        Me.txtBPracEMail.Location = New System.Drawing.Point(243, 216)
        Me.txtBPracEMail.MaxLength = 99
        Me.txtBPracEMail.Name = "txtBPracEMail"
        Me.txtBPracEMail.Size = New System.Drawing.Size(325, 22)
        Me.txtBPracEMail.TabIndex = 7
        '
        'lblAddress2
        '
        Me.lblAddress2.AutoSize = True
        Me.lblAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAddress2.Location = New System.Drawing.Point(409, 95)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(69, 14)
        Me.lblAddress2.TabIndex = 91
        Me.lblAddress2.Text = "Address 2 :"
        Me.lblAddress2.Visible = False
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.BackColor = System.Drawing.Color.Transparent
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Location = New System.Drawing.Point(204, 245)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(36, 14)
        Me.label12.TabIndex = 118
        Me.label12.Text = "URL :"
        '
        'txtBPracAddress1
        '
        Me.txtBPracAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtBPracAddress1.Location = New System.Drawing.Point(260, 67)
        Me.txtBPracAddress1.MaxLength = 99
        Me.txtBPracAddress1.Name = "txtBPracAddress1"
        Me.txtBPracAddress1.Size = New System.Drawing.Size(137, 22)
        Me.txtBPracAddress1.TabIndex = 1
        Me.txtBPracAddress1.Visible = False
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.Controls.Add(Me.grpUserDetails)
        Me.Panel15.Controls.Add(Me.Panel5)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel15.Location = New System.Drawing.Point(405, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel15.Size = New System.Drawing.Size(386, 444)
        Me.Panel15.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.udMaxScale)
        Me.Panel5.Controls.Add(Me.lblMiaxScale)
        Me.Panel5.Controls.Add(Me.udMinScale)
        Me.Panel5.Controls.Add(Me.lblMinScale)
        Me.Panel5.Controls.Add(Me.btnFontDialog)
        Me.Panel5.Controls.Add(Me.txtFont)
        Me.Panel5.Controls.Add(Me.lblFont)
        Me.Panel5.Controls.Add(Me.btnBrowse)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.Label51)
        Me.Panel5.Controls.Add(Me.cmbAssignUser)
        Me.Panel5.Controls.Add(Me.Label57)
        Me.Panel5.Controls.Add(Me.btnAssignSign)
        Me.Panel5.Controls.Add(Me.Label58)
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Controls.Add(Me.Label59)
        Me.Panel5.Controls.Add(Me.txtImagePath)
        Me.Panel5.Controls.Add(Me.Label60)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.btnCapture)
        Me.Panel5.Controls.Add(Me.btnClear)
        Me.Panel5.Controls.Add(Me.optBrowse)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(383, 297)
        Me.Panel5.TabIndex = 0
        '
        'udMaxScale
        '
        Me.udMaxScale.DecimalPlaces = 1
        Me.udMaxScale.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udMaxScale.Location = New System.Drawing.Point(293, 238)
        Me.udMaxScale.Maximum = New Decimal(New Integer() {10, 0, 0, 65536})
        Me.udMaxScale.Name = "udMaxScale"
        Me.udMaxScale.Size = New System.Drawing.Size(52, 22)
        Me.udMaxScale.TabIndex = 6
        Me.udMaxScale.Visible = False
        '
        'lblMiaxScale
        '
        Me.lblMiaxScale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiaxScale.Location = New System.Drawing.Point(213, 242)
        Me.lblMiaxScale.Name = "lblMiaxScale"
        Me.lblMiaxScale.Size = New System.Drawing.Size(78, 14)
        Me.lblMiaxScale.TabIndex = 199
        Me.lblMiaxScale.Text = "Max Scale :"
        Me.lblMiaxScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblMiaxScale.Visible = False
        '
        'udMinScale
        '
        Me.udMinScale.DecimalPlaces = 1
        Me.udMinScale.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.udMinScale.Location = New System.Drawing.Point(80, 238)
        Me.udMinScale.Maximum = New Decimal(New Integer() {11, 0, 0, 65536})
        Me.udMinScale.Minimum = New Decimal(New Integer() {2, 0, 0, 65536})
        Me.udMinScale.Name = "udMinScale"
        Me.udMinScale.Size = New System.Drawing.Size(52, 22)
        Me.udMinScale.TabIndex = 5
        Me.udMinScale.Value = New Decimal(New Integer() {3, 0, 0, 65536})
        '
        'lblMinScale
        '
        Me.lblMinScale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinScale.Location = New System.Drawing.Point(1, 242)
        Me.lblMinScale.Name = "lblMinScale"
        Me.lblMinScale.Size = New System.Drawing.Size(78, 14)
        Me.lblMinScale.TabIndex = 197
        Me.lblMinScale.Text = "Min. Scale :"
        Me.lblMinScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnFontDialog
        '
        Me.btnFontDialog.BackgroundImage = CType(resources.GetObject("btnFontDialog.BackgroundImage"), System.Drawing.Image)
        Me.btnFontDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFontDialog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnFontDialog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnFontDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFontDialog.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFontDialog.Image = CType(resources.GetObject("btnFontDialog.Image"), System.Drawing.Image)
        Me.btnFontDialog.Location = New System.Drawing.Point(349, 214)
        Me.btnFontDialog.Name = "btnFontDialog"
        Me.btnFontDialog.Size = New System.Drawing.Size(21, 21)
        Me.btnFontDialog.TabIndex = 3
        '
        'txtFont
        '
        Me.txtFont.BackColor = System.Drawing.Color.White
        Me.txtFont.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFont.ForeColor = System.Drawing.Color.Black
        Me.txtFont.Location = New System.Drawing.Point(80, 213)
        Me.txtFont.Name = "txtFont"
        Me.txtFont.ReadOnly = True
        Me.txtFont.Size = New System.Drawing.Size(265, 22)
        Me.txtFont.TabIndex = 4
        '
        'lblFont
        '
        Me.lblFont.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFont.Location = New System.Drawing.Point(12, 217)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(67, 14)
        Me.lblFont.TabIndex = 196
        Me.lblFont.Text = "Font :"
        Me.lblFont.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = CType(resources.GetObject("btnBrowse.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(349, 189)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(21, 21)
        Me.btnBrowse.TabIndex = 1
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(8, 270)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(125, 14)
        Me.Label42.TabIndex = 125
        Me.Label42.Text = "Signature Delegates :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Location = New System.Drawing.Point(1, 1)
        Me.Label51.Name = "Label51"
        Me.Label51.Padding = New System.Windows.Forms.Padding(4, 2, 4, 0)
        Me.Label51.Size = New System.Drawing.Size(75, 16)
        Me.Label51.TabIndex = 193
        Me.Label51.Text = "Signature"
        '
        'cmbAssignUser
        '
        Me.cmbAssignUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssignUser.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbAssignUser.FormattingEnabled = True
        Me.cmbAssignUser.Location = New System.Drawing.Point(135, 266)
        Me.cmbAssignUser.Name = "cmbAssignUser"
        Me.cmbAssignUser.Size = New System.Drawing.Size(105, 22)
        Me.cmbAssignUser.TabIndex = 7
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(1, 296)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(381, 1)
        Me.Label57.TabIndex = 191
        Me.Label57.Text = "label2"
        '
        'btnAssignSign
        '
        Me.btnAssignSign.BackgroundImage = CType(resources.GetObject("btnAssignSign.BackgroundImage"), System.Drawing.Image)
        Me.btnAssignSign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAssignSign.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAssignSign.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAssignSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAssignSign.Location = New System.Drawing.Point(244, 265)
        Me.btnAssignSign.Name = "btnAssignSign"
        Me.btnAssignSign.Size = New System.Drawing.Size(128, 24)
        Me.btnAssignSign.TabIndex = 8
        Me.btnAssignSign.Text = "Signature Delegates"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(1, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(381, 1)
        Me.Label58.TabIndex = 190
        Me.Label58.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.picSignature)
        Me.Panel1.Location = New System.Drawing.Point(13, 23)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(351, 127)
        Me.Panel1.TabIndex = 23
        '
        'picSignature
        '
        Me.picSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSignature.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picSignature.Location = New System.Drawing.Point(0, 0)
        Me.picSignature.Name = "picSignature"
        Me.picSignature.Size = New System.Drawing.Size(351, 127)
        Me.picSignature.TabIndex = 17
        Me.picSignature.TabStop = False
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label59.Location = New System.Drawing.Point(382, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(1, 297)
        Me.Label59.TabIndex = 189
        Me.Label59.Text = "label3"
        '
        'txtImagePath
        '
        Me.txtImagePath.BackColor = System.Drawing.Color.White
        Me.txtImagePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImagePath.ForeColor = System.Drawing.Color.Black
        Me.txtImagePath.Location = New System.Drawing.Point(80, 188)
        Me.txtImagePath.Name = "txtImagePath"
        Me.txtImagePath.ReadOnly = True
        Me.txtImagePath.Size = New System.Drawing.Size(265, 22)
        Me.txtImagePath.TabIndex = 2
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(0, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1, 297)
        Me.Label60.TabIndex = 1
        Me.Label60.Text = "label4"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "File Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCapture
        '
        Me.btnCapture.BackgroundImage = CType(resources.GetObject("btnCapture.BackgroundImage"), System.Drawing.Image)
        Me.btnCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCapture.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCapture.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCapture.Location = New System.Drawing.Point(57, 154)
        Me.btnCapture.Name = "btnCapture"
        Me.btnCapture.Size = New System.Drawing.Size(22, 24)
        Me.btnCapture.TabIndex = 1
        Me.btnCapture.Text = "Capture"
        Me.btnCapture.Visible = False
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Location = New System.Drawing.Point(141, 157)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(118, 24)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Clear Signature"
        '
        'optBrowse
        '
        Me.optBrowse.Checked = True
        Me.optBrowse.Location = New System.Drawing.Point(17, 23)
        Me.optBrowse.Name = "optBrowse"
        Me.optBrowse.Size = New System.Drawing.Size(132, 18)
        Me.optBrowse.TabIndex = 0
        Me.optBrowse.TabStop = True
        Me.optBrowse.Text = "Browse From File"
        Me.optBrowse.Visible = False
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Panel2)
        Me.Panel14.Controls.Add(Me.Panel3)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 3)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel14.Size = New System.Drawing.Size(791, 48)
        Me.Panel14.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtSuffix)
        Me.Panel2.Controls.Add(Me.label33)
        Me.Panel2.Controls.Add(Me.label36)
        Me.Panel2.Controls.Add(Me.label35)
        Me.Panel2.Controls.Add(Me.label34)
        Me.Panel2.Controls.Add(Me.Label38)
        Me.Panel2.Controls.Add(Me.txtFirstName)
        Me.Panel2.Controls.Add(Me.Label37)
        Me.Panel2.Controls.Add(Me.lblLastName)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lblMiddleName)
        Me.Panel2.Controls.Add(Me.lblFirstName)
        Me.Panel2.Controls.Add(Me.lblPrefix)
        Me.Panel2.Controls.Add(Me.txtLastName)
        Me.Panel2.Controls.Add(Me.label45)
        Me.Panel2.Controls.Add(Me.txtMiddleName)
        Me.Panel2.Controls.Add(Me.txtPrefix)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(592, 45)
        Me.Panel2.TabIndex = 1
        '
        'txtSuffix
        '
        Me.txtSuffix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuffix.ForeColor = System.Drawing.Color.Black
        Me.txtSuffix.Location = New System.Drawing.Point(536, 4)
        Me.txtSuffix.MaxLength = 20
        Me.txtSuffix.Name = "txtSuffix"
        Me.txtSuffix.Size = New System.Drawing.Size(49, 22)
        Me.txtSuffix.TabIndex = 4
        '
        'label33
        '
        Me.label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label33.Location = New System.Drawing.Point(1, 44)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(590, 1)
        Me.label33.TabIndex = 191
        Me.label33.Text = "label2"
        '
        'label36
        '
        Me.label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.Location = New System.Drawing.Point(1, 0)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(590, 1)
        Me.label36.TabIndex = 190
        Me.label36.Text = "label1"
        '
        'label35
        '
        Me.label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label35.Location = New System.Drawing.Point(591, 0)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(1, 45)
        Me.label35.TabIndex = 189
        Me.label35.Text = "label3"
        '
        'label34
        '
        Me.label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label34.Location = New System.Drawing.Point(0, 0)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(1, 45)
        Me.label34.TabIndex = 1
        Me.label34.Text = "label4"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.ForeColor = System.Drawing.Color.Red
        Me.Label38.Location = New System.Drawing.Point(419, 27)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(14, 14)
        Me.Label38.TabIndex = 193
        Me.Label38.Text = "*"
        '
        'txtFirstName
        '
        Me.txtFirstName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.ForeColor = System.Drawing.Color.Black
        Me.txtFirstName.Location = New System.Drawing.Point(117, 4)
        Me.txtFirstName.MaxLength = 100
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(140, 22)
        Me.txtFirstName.TabIndex = 1
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.ForeColor = System.Drawing.Color.Red
        Me.Label37.Location = New System.Drawing.Point(145, 27)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(14, 14)
        Me.Label37.TabIndex = 192
        Me.Label37.Text = "*"
        '
        'lblLastName
        '
        Me.lblLastName.AutoSize = True
        Me.lblLastName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLastName.Location = New System.Drawing.Point(434, 29)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(55, 11)
        Me.lblLastName.TabIndex = 820
        Me.lblLastName.Text = "(Last Name)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(5, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 191
        Me.Label6.Text = "*"
        Me.Label6.Visible = False
        '
        'lblMiddleName
        '
        Me.lblMiddleName.AutoSize = True
        Me.lblMiddleName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMiddleName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblMiddleName.Location = New System.Drawing.Point(291, 29)
        Me.lblMiddleName.Name = "lblMiddleName"
        Me.lblMiddleName.Size = New System.Drawing.Size(66, 11)
        Me.lblMiddleName.TabIndex = 810
        Me.lblMiddleName.Text = "(Middle Name)"
        '
        'lblFirstName
        '
        Me.lblFirstName.AutoSize = True
        Me.lblFirstName.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFirstName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFirstName.Location = New System.Drawing.Point(159, 29)
        Me.lblFirstName.Name = "lblFirstName"
        Me.lblFirstName.Size = New System.Drawing.Size(57, 11)
        Me.lblFirstName.TabIndex = 800
        Me.lblFirstName.Text = "(First Name)"
        '
        'lblPrefix
        '
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPrefix.Location = New System.Drawing.Point(72, 29)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(34, 11)
        Me.lblPrefix.TabIndex = 79
        Me.lblPrefix.Text = "(Prefix)"
        '
        'txtLastName
        '
        Me.txtLastName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.ForeColor = System.Drawing.Color.Black
        Me.txtLastName.Location = New System.Drawing.Point(391, 4)
        Me.txtLastName.MaxLength = 100
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(140, 22)
        Me.txtLastName.TabIndex = 3
        '
        'label45
        '
        Me.label45.AutoSize = True
        Me.label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label45.Location = New System.Drawing.Point(543, 29)
        Me.label45.Name = "label45"
        Me.label45.Size = New System.Drawing.Size(34, 11)
        Me.label45.TabIndex = 79
        Me.label45.Text = "(Suffix)"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddleName.ForeColor = System.Drawing.Color.Black
        Me.txtMiddleName.Location = New System.Drawing.Point(262, 4)
        Me.txtMiddleName.MaxLength = 100
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(124, 22)
        Me.txtMiddleName.TabIndex = 2
        '
        'txtPrefix
        '
        Me.txtPrefix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrefix.ForeColor = System.Drawing.Color.Black
        Me.txtPrefix.Location = New System.Drawing.Point(67, 4)
        Me.txtPrefix.MaxLength = 20
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.Size = New System.Drawing.Size(45, 22)
        Me.txtPrefix.TabIndex = 0
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(18, 7)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(46, 14)
        Me.lblName.TabIndex = 74
        Me.lblName.Text = "Name :"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.mtxtDOB)
        Me.Panel3.Controls.Add(Me.lbPatientDOB)
        Me.Panel3.Controls.Add(Me.Label50)
        Me.Panel3.Controls.Add(Me.optMale)
        Me.Panel3.Controls.Add(Me.Label43)
        Me.Panel3.Controls.Add(Me.optFemale)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Controls.Add(Me.Label48)
        Me.Panel3.Controls.Add(Me.Label49)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(595, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(193, 45)
        Me.Panel3.TabIndex = 2
        '
        'mtxtDOB
        '
        Me.mtxtDOB.Location = New System.Drawing.Point(68, 21)
        Me.mtxtDOB.Mask = "00/00/0000"
        Me.mtxtDOB.Name = "mtxtDOB"
        Me.mtxtDOB.Size = New System.Drawing.Size(114, 22)
        Me.mtxtDOB.TabIndex = 1921
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
        Me.lbPatientDOB.Location = New System.Drawing.Point(25, 25)
        Me.lbPatientDOB.Name = "lbPatientDOB"
        Me.lbPatientDOB.Size = New System.Drawing.Size(39, 14)
        Me.lbPatientDOB.TabIndex = 1922
        Me.lbPatientDOB.Text = "DOB :"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Location = New System.Drawing.Point(10, 6)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(55, 14)
        Me.Label50.TabIndex = 1920
        Me.Label50.Text = "Gender :"
        '
        'optMale
        '
        Me.optMale.AutoSize = True
        Me.optMale.BackColor = System.Drawing.Color.Transparent
        Me.optMale.Checked = True
        Me.optMale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optMale.Location = New System.Drawing.Point(68, 3)
        Me.optMale.Name = "optMale"
        Me.optMale.Size = New System.Drawing.Size(53, 18)
        Me.optMale.TabIndex = 0
        Me.optMale.TabStop = True
        Me.optMale.Text = "Male"
        Me.optMale.UseVisualStyleBackColor = False
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(4, 44)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(188, 1)
        Me.Label43.TabIndex = 191
        Me.Label43.Text = "label2"
        '
        'optFemale
        '
        Me.optFemale.AutoSize = True
        Me.optFemale.BackColor = System.Drawing.Color.Transparent
        Me.optFemale.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optFemale.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.optFemale.Location = New System.Drawing.Point(123, 3)
        Me.optFemale.Name = "optFemale"
        Me.optFemale.Size = New System.Drawing.Size(63, 18)
        Me.optFemale.TabIndex = 1
        Me.optFemale.Text = "Female"
        Me.optFemale.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(4, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(188, 1)
        Me.Label44.TabIndex = 190
        Me.Label44.Text = "label1"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label48.Location = New System.Drawing.Point(192, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 45)
        Me.Label48.TabIndex = 189
        Me.Label48.Text = "label3"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(3, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 45)
        Me.Label49.TabIndex = 1
        Me.Label49.Text = "label4"
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
        Me.pnl_tlsp_Top.TabIndex = 0
        '
        'lblLicenseMessage
        '
        Me.lblLicenseMessage.AutoSize = True
        Me.lblLicenseMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseMessage.ForeColor = System.Drawing.Color.Green
        Me.lblLicenseMessage.Location = New System.Drawing.Point(177, 21)
        Me.lblLicenseMessage.Name = "lblLicenseMessage"
        Me.lblLicenseMessage.Size = New System.Drawing.Size(501, 14)
        Me.lblLicenseMessage.TabIndex = 197
        Me.lblLicenseMessage.Text = "License key is generated by TRIARQ Health, please save to apply the license key."
        Me.lblLicenseMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLicenseMessage.Visible = False
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
        Me.tlsbtnSave.Text = "&Save&&Cls"
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
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tbpgProvider)
        Me.TabControl1.Controls.Add(Me.tbpgBillingID)
        Me.TabControl1.Controls.Add(Me.tbpgProviderCompany)
        Me.TabControl1.Controls.Add(Me.tbpgStatement)
        Me.TabControl1.Controls.Add(Me.tbpg_EpcsSettings)
        Me.TabControl1.Controls.Add(Me.tbpg_ePASettings)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 56)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(799, 700)
        Me.TabControl1.TabIndex = 1
        Me.TabControl1.TabStop = False
        '
        'tbpgProvider
        '
        Me.tbpgProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgProvider.Controls.Add(Me.Panel22)
        Me.tbpgProvider.Controls.Add(Me.Panel27)
        Me.tbpgProvider.Controls.Add(Me.Panel14)
        Me.tbpgProvider.Location = New System.Drawing.Point(4, 23)
        Me.tbpgProvider.Name = "tbpgProvider"
        Me.tbpgProvider.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.tbpgProvider.Size = New System.Drawing.Size(791, 673)
        Me.tbpgProvider.TabIndex = 0
        Me.tbpgProvider.Text = "Provider"
        Me.tbpgProvider.UseVisualStyleBackColor = True
        '
        'Panel22
        '
        Me.Panel22.AutoScroll = True
        Me.Panel22.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel22.Controls.Add(Me.Panel26)
        Me.Panel22.Controls.Add(Me.Panel25)
        Me.Panel22.Controls.Add(Me.pnlSPI)
        Me.Panel22.Controls.Add(Me.grpSPI)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel22.Location = New System.Drawing.Point(0, 527)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(791, 147)
        Me.Panel22.TabIndex = 2
        '
        'Panel26
        '
        Me.Panel26.BackColor = System.Drawing.Color.Transparent
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Location = New System.Drawing.Point(0, 214)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(774, 0)
        Me.Panel26.TabIndex = 9
        Me.Panel26.Visible = False
        '
        'Panel25
        '
        Me.Panel25.Controls.Add(Me.Panel13)
        Me.Panel25.Controls.Add(Me.pnlchkRequireSupervisingProviderRx)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel25.Location = New System.Drawing.Point(0, 151)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(774, 63)
        Me.Panel25.TabIndex = 9
        '
        'pnlchkRequireSupervisingProviderRx
        '
        Me.pnlchkRequireSupervisingProviderRx.AutoScroll = True
        Me.pnlchkRequireSupervisingProviderRx.Controls.Add(Me.Label139)
        Me.pnlchkRequireSupervisingProviderRx.Controls.Add(Me.chkRequire_Supervising_Provider_for_eRx)
        Me.pnlchkRequireSupervisingProviderRx.Controls.Add(Me.Label140)
        Me.pnlchkRequireSupervisingProviderRx.Controls.Add(Me.Label141)
        Me.pnlchkRequireSupervisingProviderRx.Controls.Add(Me.Label142)
        Me.pnlchkRequireSupervisingProviderRx.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlchkRequireSupervisingProviderRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlchkRequireSupervisingProviderRx.Location = New System.Drawing.Point(0, 0)
        Me.pnlchkRequireSupervisingProviderRx.Name = "pnlchkRequireSupervisingProviderRx"
        Me.pnlchkRequireSupervisingProviderRx.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlchkRequireSupervisingProviderRx.Size = New System.Drawing.Size(774, 29)
        Me.pnlchkRequireSupervisingProviderRx.TabIndex = 194
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label139.Location = New System.Drawing.Point(4, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(766, 1)
        Me.Label139.TabIndex = 193
        Me.Label139.Text = "label2"
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label140.Location = New System.Drawing.Point(4, 25)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(766, 1)
        Me.Label140.TabIndex = 192
        Me.Label140.Text = "label2"
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.Location = New System.Drawing.Point(770, 0)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(1, 26)
        Me.Label141.TabIndex = 84
        Me.Label141.Text = "label4"
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label142.Location = New System.Drawing.Point(3, 0)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1, 26)
        Me.Label142.TabIndex = 83
        Me.Label142.Text = "label4"
        '
        'Panel27
        '
        Me.Panel27.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel27.Controls.Add(Me.Panel17)
        Me.Panel27.Controls.Add(Me.Panel15)
        Me.Panel27.Controls.Add(Me.pnlLicenseKey)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel27.Location = New System.Drawing.Point(0, 51)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(791, 476)
        Me.Panel27.TabIndex = 1
        '
        'pnlLicenseKey
        '
        Me.pnlLicenseKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLicenseKey.Controls.Add(Me.btnLicenseRefresh)
        Me.pnlLicenseKey.Controls.Add(Me.Label170)
        Me.pnlLicenseKey.Controls.Add(Me.txtLicenseKey)
        Me.pnlLicenseKey.Controls.Add(Me.Label156)
        Me.pnlLicenseKey.Controls.Add(Me.Label167)
        Me.pnlLicenseKey.Controls.Add(Me.Label168)
        Me.pnlLicenseKey.Controls.Add(Me.Label169)
        Me.pnlLicenseKey.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLicenseKey.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLicenseKey.Location = New System.Drawing.Point(0, 444)
        Me.pnlLicenseKey.Name = "pnlLicenseKey"
        Me.pnlLicenseKey.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlLicenseKey.Size = New System.Drawing.Size(791, 32)
        Me.pnlLicenseKey.TabIndex = 195
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
        Me.btnLicenseRefresh.Location = New System.Drawing.Point(753, 3)
        Me.btnLicenseRefresh.Name = "btnLicenseRefresh"
        Me.btnLicenseRefresh.Size = New System.Drawing.Size(24, 23)
        Me.btnLicenseRefresh.TabIndex = 199
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
        'Label170
        '
        Me.Label170.AutoSize = True
        Me.Label170.BackColor = System.Drawing.Color.Transparent
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label170.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label170.Location = New System.Drawing.Point(15, 7)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(79, 14)
        Me.Label170.TabIndex = 198
        Me.Label170.Text = "License Key :"
        Me.Label170.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label156
        '
        Me.Label156.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label156.Location = New System.Drawing.Point(4, 0)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(783, 1)
        Me.Label156.TabIndex = 193
        Me.Label156.Text = "label2"
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label167.Location = New System.Drawing.Point(4, 28)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(783, 1)
        Me.Label167.TabIndex = 192
        Me.Label167.Text = "label2"
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label168.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label168.Location = New System.Drawing.Point(787, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(1, 29)
        Me.Label168.TabIndex = 84
        Me.Label168.Text = "label4"
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label169.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label169.Location = New System.Drawing.Point(3, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(1, 29)
        Me.Label169.TabIndex = 83
        Me.Label169.Text = "label4"
        '
        'tbpgBillingID
        '
        Me.tbpgBillingID.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgBillingID.Controls.Add(Me.Panel20)
        Me.tbpgBillingID.Controls.Add(Me.gbProviderIdentification)
        Me.tbpgBillingID.Location = New System.Drawing.Point(4, 23)
        Me.tbpgBillingID.Name = "tbpgBillingID"
        Me.tbpgBillingID.Size = New System.Drawing.Size(791, 673)
        Me.tbpgBillingID.TabIndex = 5
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
        Me.Panel20.Size = New System.Drawing.Size(791, 341)
        Me.Panel20.TabIndex = 198
        '
        'Panel21
        '
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
        Me.mskBIDPLFax.Location = New System.Drawing.Point(414, 186)
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
        Me.mskBIDPLPager.Location = New System.Drawing.Point(414, 162)
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
        Me.Label119.Size = New System.Drawing.Size(1, 336)
        Me.Label119.TabIndex = 127
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.BackColor = System.Drawing.Color.Transparent
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Location = New System.Drawing.Point(363, 166)
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
        Me.Label121.Size = New System.Drawing.Size(1, 336)
        Me.Label121.TabIndex = 126
        '
        'pnlBIDPLAddresssControl
        '
        Me.pnlBIDPLAddresssControl.Location = New System.Drawing.Point(120, 55)
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
        Me.Label122.Location = New System.Drawing.Point(0, 340)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(791, 1)
        Me.Label122.TabIndex = 125
        '
        'maskedBIDPLPhno
        '
        Me.maskedBIDPLPhno.AllowValidate = True
        Me.maskedBIDPLPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedBIDPLPhno.IncludeLiteralsAndPrompts = False
        Me.maskedBIDPLPhno.Location = New System.Drawing.Point(202, 187)
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
        Me.txtBIDPLContactName.Location = New System.Drawing.Point(202, 31)
        Me.txtBIDPLContactName.MaxLength = 99
        Me.txtBIDPLContactName.Name = "txtBIDPLContactName"
        Me.txtBIDPLContactName.Size = New System.Drawing.Size(302, 22)
        Me.txtBIDPLContactName.TabIndex = 0
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.Transparent
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label125.Location = New System.Drawing.Point(130, 35)
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
        Me.TextBox6.Location = New System.Drawing.Point(351, 102)
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
        Me.Label129.Location = New System.Drawing.Point(376, 190)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(33, 14)
        Me.Label129.TabIndex = 116
        Me.Label129.Text = "Fax :"
        '
        'TextBox7
        '
        Me.TextBox7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox7.ForeColor = System.Drawing.Color.Black
        Me.TextBox7.Location = New System.Drawing.Point(304, 99)
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
        Me.Label130.Location = New System.Drawing.Point(129, 216)
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
        Me.TextBox9.Location = New System.Drawing.Point(367, 96)
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
        Me.Label131.Location = New System.Drawing.Point(129, 191)
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
        Me.TextBox10.Location = New System.Drawing.Point(363, 85)
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
        Me.txtBIDPLUrl.Location = New System.Drawing.Point(202, 238)
        Me.txtBIDPLUrl.MaxLength = 99
        Me.txtBIDPLUrl.Name = "txtBIDPLUrl"
        Me.txtBIDPLUrl.Size = New System.Drawing.Size(304, 22)
        Me.txtBIDPLUrl.TabIndex = 7
        '
        'Label132
        '
        Me.Label132.AutoSize = True
        Me.Label132.BackColor = System.Drawing.Color.Transparent
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label132.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Location = New System.Drawing.Point(246, 91)
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
        Me.txtBIDPLEMail.Location = New System.Drawing.Point(202, 213)
        Me.txtBIDPLEMail.MaxLength = 99
        Me.txtBIDPLEMail.Name = "txtBIDPLEMail"
        Me.txtBIDPLEMail.Size = New System.Drawing.Size(304, 22)
        Me.txtBIDPLEMail.TabIndex = 6
        '
        'Label133
        '
        Me.Label133.AutoSize = True
        Me.Label133.BackColor = System.Drawing.Color.Transparent
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Location = New System.Drawing.Point(301, 127)
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
        Me.Label134.Location = New System.Drawing.Point(130, 241)
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
        Me.Label135.Location = New System.Drawing.Point(266, 102)
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
        Me.Label136.Location = New System.Drawing.Point(292, 88)
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
        Me.TextBox13.Location = New System.Drawing.Point(189, 69)
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
        Me.Label137.Location = New System.Drawing.Point(234, 72)
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
        Me.gbProviderIdentification.Controls.Add(Me.Label63)
        Me.gbProviderIdentification.Controls.Add(Me.Label64)
        Me.gbProviderIdentification.Controls.Add(Me.Label65)
        Me.gbProviderIdentification.Controls.Add(Me.Label66)
        Me.gbProviderIdentification.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbProviderIdentification.Location = New System.Drawing.Point(0, 0)
        Me.gbProviderIdentification.Name = "gbProviderIdentification"
        Me.gbProviderIdentification.Size = New System.Drawing.Size(791, 332)
        Me.gbProviderIdentification.TabIndex = 1
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
        'Panel11
        '
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
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Location = New System.Drawing.Point(0, 0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(789, 23)
        Me.Label91.TabIndex = 129
        Me.Label91.Text = "  Additional Billing IDs"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Location = New System.Drawing.Point(790, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 330)
        Me.Label63.TabIndex = 127
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Location = New System.Drawing.Point(0, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 330)
        Me.Label64.TabIndex = 126
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Location = New System.Drawing.Point(0, 331)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(791, 1)
        Me.Label65.TabIndex = 125
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(791, 1)
        Me.Label66.TabIndex = 124
        '
        'tbpgProviderCompany
        '
        Me.tbpgProviderCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgProviderCompany.Controls.Add(Me.Panel6)
        Me.tbpgProviderCompany.Controls.Add(Me.Panel23)
        Me.tbpgProviderCompany.Controls.Add(Me.Panel8)
        Me.tbpgProviderCompany.Location = New System.Drawing.Point(4, 23)
        Me.tbpgProviderCompany.Name = "tbpgProviderCompany"
        Me.tbpgProviderCompany.Size = New System.Drawing.Size(791, 673)
        Me.tbpgProviderCompany.TabIndex = 4
        Me.tbpgProviderCompany.Text = "Provider Company"
        Me.tbpgProviderCompany.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label94)
        Me.Panel6.Controls.Add(Me.Label110)
        Me.Panel6.Controls.Add(Me.Label109)
        Me.Panel6.Controls.Add(Me.Panel18)
        Me.Panel6.Controls.Add(Me.mskPLFax)
        Me.Panel6.Controls.Add(Me.mskPLPager)
        Me.Panel6.Controls.Add(Me.Label102)
        Me.Panel6.Controls.Add(Me.Label103)
        Me.Panel6.Controls.Add(Me.Label104)
        Me.Panel6.Controls.Add(Me.pnlPLAddresssControl)
        Me.Panel6.Controls.Add(Me.Label105)
        Me.Panel6.Controls.Add(Me.maskedPLPhno)
        Me.Panel6.Controls.Add(Me.Label106)
        Me.Panel6.Controls.Add(Me.txtPLContactName)
        Me.Panel6.Controls.Add(Me.Label107)
        Me.Panel6.Controls.Add(Me.TextBox2)
        Me.Panel6.Controls.Add(Me.Label108)
        Me.Panel6.Controls.Add(Me.TextBox3)
        Me.Panel6.Controls.Add(Me.TextBox4)
        Me.Panel6.Controls.Add(Me.TextBox5)
        Me.Panel6.Controls.Add(Me.txtPLUrl)
        Me.Panel6.Controls.Add(Me.Label111)
        Me.Panel6.Controls.Add(Me.txtPLEMail)
        Me.Panel6.Controls.Add(Me.Label112)
        Me.Panel6.Controls.Add(Me.Label114)
        Me.Panel6.Controls.Add(Me.Label115)
        Me.Panel6.Controls.Add(Me.TextBox8)
        Me.Panel6.Controls.Add(Me.Label116)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 357)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel6.Size = New System.Drawing.Size(791, 316)
        Me.Panel6.TabIndex = 196
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.Transparent
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Location = New System.Drawing.Point(140, 217)
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
        Me.Label110.Location = New System.Drawing.Point(140, 190)
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
        Me.Label109.Location = New System.Drawing.Point(425, 217)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(42, 13)
        Me.Label109.TabIndex = 135
        Me.Label109.Text = "URL :"
        Me.Label109.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel18
        '
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label99)
        Me.Panel18.Controls.Add(Me.Label101)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(1, 4)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(789, 23)
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
        Me.Label101.Size = New System.Drawing.Size(789, 1)
        Me.Label101.TabIndex = 130
        '
        'mskPLFax
        '
        Me.mskPLFax.AllowValidate = True
        Me.mskPLFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskPLFax.IncludeLiteralsAndPrompts = False
        Me.mskPLFax.Location = New System.Drawing.Point(467, 185)
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
        Me.mskPLPager.Location = New System.Drawing.Point(467, 161)
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
        Me.Label102.Location = New System.Drawing.Point(790, 4)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 311)
        Me.Label102.TabIndex = 127
        '
        'Label103
        '
        Me.Label103.AutoEllipsis = True
        Me.Label103.AutoSize = True
        Me.Label103.BackColor = System.Drawing.Color.Transparent
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Location = New System.Drawing.Point(421, 165)
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
        Me.Label104.Size = New System.Drawing.Size(1, 311)
        Me.Label104.TabIndex = 126
        '
        'pnlPLAddresssControl
        '
        Me.pnlPLAddresssControl.Location = New System.Drawing.Point(131, 54)
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
        Me.Label105.Location = New System.Drawing.Point(0, 315)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(791, 1)
        Me.Label105.TabIndex = 125
        '
        'maskedPLPhno
        '
        Me.maskedPLPhno.AllowValidate = True
        Me.maskedPLPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedPLPhno.IncludeLiteralsAndPrompts = False
        Me.maskedPLPhno.Location = New System.Drawing.Point(213, 186)
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
        Me.Label106.Size = New System.Drawing.Size(791, 1)
        Me.Label106.TabIndex = 124
        '
        'txtPLContactName
        '
        Me.txtPLContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPLContactName.ForeColor = System.Drawing.Color.Black
        Me.txtPLContactName.Location = New System.Drawing.Point(213, 30)
        Me.txtPLContactName.MaxLength = 99
        Me.txtPLContactName.Name = "txtPLContactName"
        Me.txtPLContactName.Size = New System.Drawing.Size(230, 22)
        Me.txtPLContactName.TabIndex = 0
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.Transparent
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Location = New System.Drawing.Point(141, 35)
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
        Me.TextBox2.Location = New System.Drawing.Point(351, 101)
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
        Me.Label108.Location = New System.Drawing.Point(434, 189)
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
        Me.TextBox3.Location = New System.Drawing.Point(304, 98)
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
        Me.TextBox4.Location = New System.Drawing.Point(367, 95)
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
        Me.TextBox5.Location = New System.Drawing.Point(363, 84)
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
        Me.txtPLUrl.Location = New System.Drawing.Point(467, 213)
        Me.txtPLUrl.MaxLength = 99
        Me.txtPLUrl.Name = "txtPLUrl"
        Me.txtPLUrl.Size = New System.Drawing.Size(262, 22)
        Me.txtPLUrl.TabIndex = 7
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.Color.Transparent
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Location = New System.Drawing.Point(246, 90)
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
        Me.txtPLEMail.Location = New System.Drawing.Point(213, 212)
        Me.txtPLEMail.MaxLength = 99
        Me.txtPLEMail.Name = "txtPLEMail"
        Me.txtPLEMail.Size = New System.Drawing.Size(206, 22)
        Me.txtPLEMail.TabIndex = 6
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.BackColor = System.Drawing.Color.Transparent
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Location = New System.Drawing.Point(301, 126)
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
        Me.Label114.Location = New System.Drawing.Point(266, 101)
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
        Me.Label115.Location = New System.Drawing.Point(292, 87)
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
        Me.TextBox8.Location = New System.Drawing.Point(189, 68)
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
        Me.Label116.Location = New System.Drawing.Point(234, 71)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(65, 14)
        Me.Label116.TabIndex = 90
        Me.Label116.Text = "Address1 :"
        Me.Label116.Visible = False
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
        Me.Panel23.Location = New System.Drawing.Point(0, 291)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel23.Size = New System.Drawing.Size(791, 66)
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
        Me.c1CompanyProvIdentification.Location = New System.Drawing.Point(1, 24)
        Me.c1CompanyProvIdentification.Name = "c1CompanyProvIdentification"
        Me.c1CompanyProvIdentification.Rows.Count = 1
        Me.c1CompanyProvIdentification.Rows.DefaultSize = 19
        Me.c1CompanyProvIdentification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.c1CompanyProvIdentification.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1CompanyProvIdentification.Size = New System.Drawing.Size(789, 41)
        Me.c1CompanyProvIdentification.StyleInfo = resources.GetString("c1CompanyProvIdentification.StyleInfo")
        Me.c1CompanyProvIdentification.TabIndex = 135
        '
        'Panel24
        '
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Label148)
        Me.Panel24.Controls.Add(Me.Label149)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(1, 4)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(789, 20)
        Me.Panel24.TabIndex = 133
        Me.Panel24.TabStop = True
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.Transparent
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Location = New System.Drawing.Point(0, 0)
        Me.Label148.Name = "Label148"
        Me.Label148.Padding = New System.Windows.Forms.Padding(3)
        Me.Label148.Size = New System.Drawing.Size(789, 19)
        Me.Label148.TabIndex = 131
        Me.Label148.Text = " Company Additional ID"
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Location = New System.Drawing.Point(0, 19)
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
        Me.Label151.Location = New System.Drawing.Point(1, 65)
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
        Me.Label152.Size = New System.Drawing.Size(1, 63)
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
        Me.Label153.Size = New System.Drawing.Size(1, 63)
        Me.Label153.TabIndex = 127
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Label74)
        Me.Panel8.Controls.Add(Me.txtCmpTaxonomyCode)
        Me.Panel8.Controls.Add(Me.btn_ClearCmpTaxonomy)
        Me.Panel8.Controls.Add(Me.btn_BrowseCmpTaxonomy)
        Me.Panel8.Controls.Add(Me.txtCompanyNPI)
        Me.Panel8.Controls.Add(Me.chkCompanyAsAbove)
        Me.Panel8.Controls.Add(Me.txtCompanyPhone)
        Me.Panel8.Controls.Add(Me.label47)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Controls.Add(Me.txtCompanyName)
        Me.Panel8.Controls.Add(Me.txtCompanyAddress1)
        Me.Panel8.Controls.Add(Me.label46)
        Me.Panel8.Controls.Add(Me.txtCompanyContactName)
        Me.Panel8.Controls.Add(Me.label19)
        Me.Panel8.Controls.Add(Me.Label17)
        Me.Panel8.Controls.Add(Me.label18)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.pnlCompanyInternalControl)
        Me.Panel8.Controls.Add(Me.pnlCompanyAddresssControl)
        Me.Panel8.Controls.Add(Me.Label61)
        Me.Panel8.Controls.Add(Me.txtCompanyZip)
        Me.Panel8.Controls.Add(Me.ComboBox1)
        Me.Panel8.Controls.Add(Me.txtCompanyEmail)
        Me.Panel8.Controls.Add(Me.Label71)
        Me.Panel8.Controls.Add(Me.txtCompanyCity)
        Me.Panel8.Controls.Add(Me.txtCompanyFax)
        Me.Panel8.Controls.Add(Me.txtCompanyTaxID)
        Me.Panel8.Controls.Add(Me.txtCompanyAddress2)
        Me.Panel8.Controls.Add(Me.Panel7)
        Me.Panel8.Controls.Add(Me.Label93)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Controls.Add(Me.Label73)
        Me.Panel8.Controls.Add(Me.Label75)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(791, 291)
        Me.Panel8.TabIndex = 1
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Location = New System.Drawing.Point(395, 266)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(72, 14)
        Me.Label74.TabIndex = 228
        Me.Label74.Text = "Taxonomy :"
        '
        'txtCmpTaxonomyCode
        '
        Me.txtCmpTaxonomyCode.BackColor = System.Drawing.Color.White
        Me.txtCmpTaxonomyCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCmpTaxonomyCode.ForeColor = System.Drawing.Color.Black
        Me.txtCmpTaxonomyCode.Location = New System.Drawing.Point(467, 262)
        Me.txtCmpTaxonomyCode.MaxLength = 99
        Me.txtCmpTaxonomyCode.Name = "txtCmpTaxonomyCode"
        Me.txtCmpTaxonomyCode.ReadOnly = True
        Me.txtCmpTaxonomyCode.Size = New System.Drawing.Size(212, 22)
        Me.txtCmpTaxonomyCode.TabIndex = 211
        '
        'btn_ClearCmpTaxonomy
        '
        Me.btn_ClearCmpTaxonomy.BackgroundImage = CType(resources.GetObject("btn_ClearCmpTaxonomy.BackgroundImage"), System.Drawing.Image)
        Me.btn_ClearCmpTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ClearCmpTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearCmpTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ClearCmpTaxonomy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ClearCmpTaxonomy.Image = CType(resources.GetObject("btn_ClearCmpTaxonomy.Image"), System.Drawing.Image)
        Me.btn_ClearCmpTaxonomy.Location = New System.Drawing.Point(707, 263)
        Me.btn_ClearCmpTaxonomy.Name = "btn_ClearCmpTaxonomy"
        Me.btn_ClearCmpTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_ClearCmpTaxonomy.TabIndex = 213
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
        Me.btn_BrowseCmpTaxonomy.Location = New System.Drawing.Point(682, 263)
        Me.btn_BrowseCmpTaxonomy.Name = "btn_BrowseCmpTaxonomy"
        Me.btn_BrowseCmpTaxonomy.Size = New System.Drawing.Size(22, 22)
        Me.btn_BrowseCmpTaxonomy.TabIndex = 212
        Me.btn_BrowseCmpTaxonomy.UseVisualStyleBackColor = True
        '
        'txtCompanyNPI
        '
        Me.txtCompanyNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyNPI.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyNPI.Location = New System.Drawing.Point(468, 210)
        Me.txtCompanyNPI.MaxLength = 10
        Me.txtCompanyNPI.Name = "txtCompanyNPI"
        Me.txtCompanyNPI.Size = New System.Drawing.Size(106, 22)
        Me.txtCompanyNPI.TabIndex = 209
        '
        'chkCompanyAsAbove
        '
        Me.chkCompanyAsAbove.AutoSize = True
        Me.chkCompanyAsAbove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompanyAsAbove.Location = New System.Drawing.Point(546, 29)
        Me.chkCompanyAsAbove.Name = "chkCompanyAsAbove"
        Me.chkCompanyAsAbove.Size = New System.Drawing.Size(166, 18)
        Me.chkCompanyAsAbove.TabIndex = 199
        Me.chkCompanyAsAbove.Text = "Same as Provider Address"
        Me.chkCompanyAsAbove.UseVisualStyleBackColor = True
        '
        'txtCompanyPhone
        '
        Me.txtCompanyPhone.AllowValidate = True
        Me.txtCompanyPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyPhone.IncludeLiteralsAndPrompts = False
        Me.txtCompanyPhone.Location = New System.Drawing.Point(215, 210)
        Me.txtCompanyPhone.MaskType = gloMaskControl.gloMaskType.Phone
        Me.txtCompanyPhone.Name = "txtCompanyPhone"
        Me.txtCompanyPhone.ReadOnly = False
        Me.txtCompanyPhone.Size = New System.Drawing.Size(100, 22)
        Me.txtCompanyPhone.TabIndex = 206
        '
        'label47
        '
        Me.label47.AutoSize = True
        Me.label47.BackColor = System.Drawing.Color.Transparent
        Me.label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label47.Location = New System.Drawing.Point(415, 240)
        Me.label47.Name = "label47"
        Me.label47.Size = New System.Drawing.Size(51, 14)
        Me.label47.TabIndex = 217
        Me.label47.Text = "Tax ID :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(180, 240)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(33, 14)
        Me.Label16.TabIndex = 218
        Me.Label16.Text = "Fax :"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyName.Location = New System.Drawing.Point(215, 27)
        Me.txtCompanyName.MaxLength = 99
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(325, 22)
        Me.txtCompanyName.TabIndex = 198
        '
        'txtCompanyAddress1
        '
        Me.txtCompanyAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyAddress1.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyAddress1.Location = New System.Drawing.Point(210, 77)
        Me.txtCompanyAddress1.MaxLength = 99
        Me.txtCompanyAddress1.Name = "txtCompanyAddress1"
        Me.txtCompanyAddress1.Size = New System.Drawing.Size(18, 22)
        Me.txtCompanyAddress1.TabIndex = 201
        Me.txtCompanyAddress1.Visible = False
        '
        'label46
        '
        Me.label46.AutoSize = True
        Me.label46.BackColor = System.Drawing.Color.Transparent
        Me.label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label46.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label46.Location = New System.Drawing.Point(431, 214)
        Me.label46.Name = "label46"
        Me.label46.Size = New System.Drawing.Size(34, 14)
        Me.label46.TabIndex = 220
        Me.label46.Text = "NPI :"
        '
        'txtCompanyContactName
        '
        Me.txtCompanyContactName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyContactName.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyContactName.Location = New System.Drawing.Point(215, 52)
        Me.txtCompanyContactName.MaxLength = 99
        Me.txtCompanyContactName.Name = "txtCompanyContactName"
        Me.txtCompanyContactName.Size = New System.Drawing.Size(325, 22)
        Me.txtCompanyContactName.TabIndex = 200
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.BackColor = System.Drawing.Color.Transparent
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label19.Location = New System.Drawing.Point(237, 80)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(69, 14)
        Me.label19.TabIndex = 211
        Me.label19.Text = "Address 1 :"
        Me.label19.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(169, 266)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 14)
        Me.Label17.TabIndex = 219
        Me.Label17.Text = "Email :"
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.BackColor = System.Drawing.Color.Transparent
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Location = New System.Drawing.Point(148, 31)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(65, 14)
        Me.label18.TabIndex = 222
        Me.label18.Text = "Company :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(155, 57)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(58, 14)
        Me.Label20.TabIndex = 221
        Me.Label20.Text = "Contact :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(300, 80)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 14)
        Me.Label21.TabIndex = 212
        Me.Label21.Text = "Address 2 :"
        Me.Label21.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(144, 214)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(69, 14)
        Me.Label22.TabIndex = 216
        Me.Label22.Text = "Phone No :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(381, 80)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(35, 14)
        Me.Label24.TabIndex = 213
        Me.Label24.Text = "City :"
        Me.Label24.Visible = False
        '
        'pnlCompanyInternalControl
        '
        Me.pnlCompanyInternalControl.Location = New System.Drawing.Point(351, 100)
        Me.pnlCompanyInternalControl.Name = "pnlCompanyInternalControl"
        Me.pnlCompanyInternalControl.Size = New System.Drawing.Size(23, 25)
        Me.pnlCompanyInternalControl.TabIndex = 224
        Me.pnlCompanyInternalControl.Visible = False
        '
        'pnlCompanyAddresssControl
        '
        Me.pnlCompanyAddresssControl.BackColor = System.Drawing.Color.Transparent
        Me.pnlCompanyAddresssControl.Location = New System.Drawing.Point(135, 76)
        Me.pnlCompanyAddresssControl.Name = "pnlCompanyAddresssControl"
        Me.pnlCompanyAddresssControl.Size = New System.Drawing.Size(325, 132)
        Me.pnlCompanyAddresssControl.TabIndex = 202
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Location = New System.Drawing.Point(443, 101)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(45, 14)
        Me.Label61.TabIndex = 214
        Me.Label61.Text = "State :"
        Me.Label61.Visible = False
        '
        'txtCompanyZip
        '
        Me.txtCompanyZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyZip.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyZip.Location = New System.Drawing.Point(321, 97)
        Me.txtCompanyZip.MaxLength = 10
        Me.txtCompanyZip.Name = "txtCompanyZip"
        Me.txtCompanyZip.Size = New System.Drawing.Size(18, 22)
        Me.txtCompanyZip.TabIndex = 209
        Me.txtCompanyZip.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(489, 97)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(37, 22)
        Me.ComboBox1.TabIndex = 223
        Me.ComboBox1.Visible = False
        '
        'txtCompanyEmail
        '
        Me.txtCompanyEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyEmail.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyEmail.Location = New System.Drawing.Point(213, 262)
        Me.txtCompanyEmail.MaxLength = 99
        Me.txtCompanyEmail.Name = "txtCompanyEmail"
        Me.txtCompanyEmail.Size = New System.Drawing.Size(167, 22)
        Me.txtCompanyEmail.TabIndex = 208
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.BackColor = System.Drawing.Color.Transparent
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Location = New System.Drawing.Point(294, 95)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(31, 14)
        Me.Label71.TabIndex = 215
        Me.Label71.Text = "Zip :"
        Me.Label71.Visible = False
        '
        'txtCompanyCity
        '
        Me.txtCompanyCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyCity.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyCity.Location = New System.Drawing.Point(404, 77)
        Me.txtCompanyCity.MaxLength = 99
        Me.txtCompanyCity.Name = "txtCompanyCity"
        Me.txtCompanyCity.Size = New System.Drawing.Size(10, 22)
        Me.txtCompanyCity.TabIndex = 205
        Me.txtCompanyCity.Visible = False
        '
        'txtCompanyFax
        '
        Me.txtCompanyFax.AllowValidate = True
        Me.txtCompanyFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyFax.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyFax.IncludeLiteralsAndPrompts = False
        Me.txtCompanyFax.Location = New System.Drawing.Point(215, 236)
        Me.txtCompanyFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.txtCompanyFax.Name = "txtCompanyFax"
        Me.txtCompanyFax.ReadOnly = False
        Me.txtCompanyFax.Size = New System.Drawing.Size(100, 22)
        Me.txtCompanyFax.TabIndex = 207
        '
        'txtCompanyTaxID
        '
        Me.txtCompanyTaxID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyTaxID.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyTaxID.Location = New System.Drawing.Point(468, 236)
        Me.txtCompanyTaxID.MaxLength = 9
        Me.txtCompanyTaxID.Name = "txtCompanyTaxID"
        Me.txtCompanyTaxID.Size = New System.Drawing.Size(106, 22)
        Me.txtCompanyTaxID.TabIndex = 210
        '
        'txtCompanyAddress2
        '
        Me.txtCompanyAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyAddress2.ForeColor = System.Drawing.Color.Black
        Me.txtCompanyAddress2.Location = New System.Drawing.Point(363, 77)
        Me.txtCompanyAddress2.MaxLength = 99
        Me.txtCompanyAddress2.Name = "txtCompanyAddress2"
        Me.txtCompanyAddress2.Size = New System.Drawing.Size(11, 22)
        Me.txtCompanyAddress2.TabIndex = 203
        Me.txtCompanyAddress2.Visible = False
        '
        'Panel7
        '
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label14)
        Me.Panel7.Controls.Add(Me.Label95)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(1, 1)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(789, 23)
        Me.Panel7.TabIndex = 132
        Me.Panel7.TabStop = True
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(3)
        Me.Label14.Size = New System.Drawing.Size(789, 22)
        Me.Label14.TabIndex = 131
        Me.Label14.Text = " Company Mailing Address"
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
        Me.Label93.Location = New System.Drawing.Point(1, 290)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(789, 1)
        Me.Label93.TabIndex = 130
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(790, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 290)
        Me.Label15.TabIndex = 127
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Location = New System.Drawing.Point(0, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 290)
        Me.Label73.TabIndex = 126
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
        'tbpgStatement
        '
        Me.tbpgStatement.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpgStatement.Controls.Add(Me.Panel34)
        Me.tbpgStatement.Location = New System.Drawing.Point(4, 23)
        Me.tbpgStatement.Name = "tbpgStatement"
        Me.tbpgStatement.Size = New System.Drawing.Size(791, 673)
        Me.tbpgStatement.TabIndex = 2
        Me.tbpgStatement.Text = "Statements"
        Me.tbpgStatement.UseVisualStyleBackColor = True
        '
        'Panel34
        '
        Me.Panel34.Controls.Add(Me.Panel16)
        Me.Panel34.Controls.Add(Me.chkAddressasAbove)
        Me.Panel34.Controls.Add(Me.txtBPracPager)
        Me.Panel34.Controls.Add(Me.Label203)
        Me.Panel34.Controls.Add(Me.label13)
        Me.Panel34.Controls.Add(Me.Label205)
        Me.Panel34.Controls.Add(Me.Label206)
        Me.Panel34.Controls.Add(Me.pnlPracticeAddresssControl)
        Me.Panel34.Controls.Add(Me.Label207)
        Me.Panel34.Controls.Add(Me.lblAddress1)
        Me.Panel34.Controls.Add(Me.txtBPracAddress1)
        Me.Panel34.Controls.Add(Me.label12)
        Me.Panel34.Controls.Add(Me.pnlBPractInternalControl)
        Me.Panel34.Controls.Add(Me.lblAddress2)
        Me.Panel34.Controls.Add(Me.txtBPracEMail)
        Me.Panel34.Controls.Add(Me.cmbBPracState)
        Me.Panel34.Controls.Add(Me.lblCity)
        Me.Panel34.Controls.Add(Me.label11)
        Me.Panel34.Controls.Add(Me.txtBPracUrl)
        Me.Panel34.Controls.Add(Me.maskedBpracPhno)
        Me.Panel34.Controls.Add(Me.lblZip)
        Me.Panel34.Controls.Add(Me.txtBPracFax)
        Me.Panel34.Controls.Add(Me.txtBPracAddress2)
        Me.Panel34.Controls.Add(Me.txtBPracContactName)
        Me.Panel34.Controls.Add(Me.label10)
        Me.Panel34.Controls.Add(Me.lblState)
        Me.Panel34.Controls.Add(Me.txtBPracCity)
        Me.Panel34.Controls.Add(Me.label25)
        Me.Panel34.Controls.Add(Me.label9)
        Me.Panel34.Controls.Add(Me.txtBPracZIP)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel34.Location = New System.Drawing.Point(0, 0)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel34.Size = New System.Drawing.Size(791, 673)
        Me.Panel34.TabIndex = 194
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = CType(resources.GetObject("Panel16.BackgroundImage"), System.Drawing.Image)
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Label62)
        Me.Panel16.Controls.Add(Me.Label100)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(4, 4)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(783, 23)
        Me.Panel16.TabIndex = 199
        Me.Panel16.TabStop = True
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Location = New System.Drawing.Point(0, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label62.Size = New System.Drawing.Size(170, 17)
        Me.Label62.TabIndex = 131
        Me.Label62.Text = "Practice Location Address"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Location = New System.Drawing.Point(0, 22)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(783, 1)
        Me.Label100.TabIndex = 130
        '
        'txtBPracPager
        '
        Me.txtBPracPager.AllowValidate = True
        Me.txtBPracPager.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracPager.ForeColor = System.Drawing.Color.Black
        Me.txtBPracPager.IncludeLiteralsAndPrompts = False
        Me.txtBPracPager.Location = New System.Drawing.Point(468, 165)
        Me.txtBPracPager.MaskType = gloMaskControl.gloMaskType.Pager
        Me.txtBPracPager.Name = "txtBPracPager"
        Me.txtBPracPager.ReadOnly = False
        Me.txtBPracPager.Size = New System.Drawing.Size(100, 22)
        Me.txtBPracPager.TabIndex = 4
        '
        'Label203
        '
        Me.Label203.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label203.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label203.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label203.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label203.Location = New System.Drawing.Point(787, 4)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(1, 665)
        Me.Label203.TabIndex = 127
        '
        'Label205
        '
        Me.Label205.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label205.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label205.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label205.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label205.Location = New System.Drawing.Point(3, 4)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(1, 665)
        Me.Label205.TabIndex = 126
        '
        'Label206
        '
        Me.Label206.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label206.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label206.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label206.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label206.Location = New System.Drawing.Point(3, 669)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(785, 1)
        Me.Label206.TabIndex = 125
        '
        'Label207
        '
        Me.Label207.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label207.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label207.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label207.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label207.Location = New System.Drawing.Point(3, 3)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(785, 1)
        Me.Label207.TabIndex = 124
        '
        'maskedBpracPhno
        '
        Me.maskedBpracPhno.AllowValidate = True
        Me.maskedBpracPhno.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maskedBpracPhno.IncludeLiteralsAndPrompts = False
        Me.maskedBpracPhno.Location = New System.Drawing.Point(243, 190)
        Me.maskedBpracPhno.MaskType = gloMaskControl.gloMaskType.Phone
        Me.maskedBpracPhno.Name = "maskedBpracPhno"
        Me.maskedBpracPhno.ReadOnly = False
        Me.maskedBpracPhno.Size = New System.Drawing.Size(100, 23)
        Me.maskedBpracPhno.TabIndex = 5
        '
        'txtBPracFax
        '
        Me.txtBPracFax.AllowValidate = True
        Me.txtBPracFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPracFax.ForeColor = System.Drawing.Color.Black
        Me.txtBPracFax.IncludeLiteralsAndPrompts = False
        Me.txtBPracFax.Location = New System.Drawing.Point(468, 190)
        Me.txtBPracFax.MaskType = gloMaskControl.gloMaskType.Fax
        Me.txtBPracFax.Name = "txtBPracFax"
        Me.txtBPracFax.ReadOnly = False
        Me.txtBPracFax.Size = New System.Drawing.Size(100, 22)
        Me.txtBPracFax.TabIndex = 6
        '
        'tbpg_EpcsSettings
        '
        Me.tbpg_EpcsSettings.Controls.Add(Me.Panel28)
        Me.tbpg_EpcsSettings.Location = New System.Drawing.Point(4, 23)
        Me.tbpg_EpcsSettings.Name = "tbpg_EpcsSettings"
        Me.tbpg_EpcsSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpg_EpcsSettings.Size = New System.Drawing.Size(791, 673)
        Me.tbpg_EpcsSettings.TabIndex = 7
        Me.tbpg_EpcsSettings.Text = "EPCS Settings"
        Me.tbpg_EpcsSettings.UseVisualStyleBackColor = True
        '
        'Panel28
        '
        Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel28.Controls.Add(Me.Panel29)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(3, 3)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(785, 667)
        Me.Panel28.TabIndex = 2
        '
        'Panel29
        '
        Me.Panel29.Controls.Add(Me.Label154)
        Me.Panel29.Controls.Add(Me.Label147)
        Me.Panel29.Controls.Add(Me.Label146)
        Me.Panel29.Controls.Add(Me.LabelUiLaunchLogicalAccess)
        Me.Panel29.Controls.Add(Me.LabelUiLaunchPrescriberArea)
        Me.Panel29.Controls.Add(Me.LabelWsInvitePrescriber)
        Me.Panel29.Controls.Add(Me.LinkLabelUILaunchPrescriberArea)
        Me.Panel29.Controls.Add(Me.btnEnrollPrescriber)
        Me.Panel29.Controls.Add(Me.btnInvitePrescriber)
        Me.Panel29.Controls.Add(Me.Panel30)
        Me.Panel29.Controls.Add(Me.Label138)
        Me.Panel29.Controls.Add(Me.Label143)
        Me.Panel29.Controls.Add(Me.Label144)
        Me.Panel29.Controls.Add(Me.Label145)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel29.Location = New System.Drawing.Point(0, 0)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(785, 667)
        Me.Panel29.TabIndex = 0
        '
        'Label154
        '
        Me.Label154.AutoSize = True
        Me.Label154.Location = New System.Drawing.Point(44, 217)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(48, 14)
        Me.Label154.TabIndex = 209
        Me.Label154.Text = "Step 3."
        '
        'Label147
        '
        Me.Label147.AutoSize = True
        Me.Label147.Location = New System.Drawing.Point(40, 127)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(52, 14)
        Me.Label147.TabIndex = 208
        Me.Label147.Text = "Step 2. "
        '
        'Label146
        '
        Me.Label146.AutoSize = True
        Me.Label146.Location = New System.Drawing.Point(44, 46)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(48, 14)
        Me.Label146.TabIndex = 207
        Me.Label146.Text = "Step 1."
        '
        'LabelUiLaunchLogicalAccess
        '
        Me.LabelUiLaunchLogicalAccess.AutoSize = True
        Me.LabelUiLaunchLogicalAccess.Location = New System.Drawing.Point(99, 243)
        Me.LabelUiLaunchLogicalAccess.Name = "LabelUiLaunchLogicalAccess"
        Me.LabelUiLaunchLogicalAccess.Size = New System.Drawing.Size(372, 14)
        Me.LabelUiLaunchLogicalAccess.TabIndex = 206
        Me.LabelUiLaunchLogicalAccess.Text = "Click on 'Activate Prescriber' to initiate Logical Access Control step."
        '
        'LabelUiLaunchPrescriberArea
        '
        Me.LabelUiLaunchPrescriberArea.AutoSize = True
        Me.LabelUiLaunchPrescriberArea.Location = New System.Drawing.Point(99, 150)
        Me.LabelUiLaunchPrescriberArea.Name = "LabelUiLaunchPrescriberArea"
        Me.LabelUiLaunchPrescriberArea.Size = New System.Drawing.Size(490, 14)
        Me.LabelUiLaunchPrescriberArea.TabIndex = 205
        Me.LabelUiLaunchPrescriberArea.Text = "Click on above link to launch the prescriber dashboard and enter IDP Confirmation" & _
    " Code."
        '
        'LabelWsInvitePrescriber
        '
        Me.LabelWsInvitePrescriber.AutoSize = True
        Me.LabelWsInvitePrescriber.Location = New System.Drawing.Point(99, 75)
        Me.LabelWsInvitePrescriber.Name = "LabelWsInvitePrescriber"
        Me.LabelWsInvitePrescriber.Size = New System.Drawing.Size(327, 14)
        Me.LabelWsInvitePrescriber.TabIndex = 204
        Me.LabelWsInvitePrescriber.Text = "Click on Invite Prescriber to get email for EPCS enrollment."
        '
        'LinkLabelUILaunchPrescriberArea
        '
        Me.LinkLabelUILaunchPrescriberArea.AutoSize = True
        Me.LinkLabelUILaunchPrescriberArea.Location = New System.Drawing.Point(99, 127)
        Me.LinkLabelUILaunchPrescriberArea.Name = "LinkLabelUILaunchPrescriberArea"
        Me.LinkLabelUILaunchPrescriberArea.Size = New System.Drawing.Size(245, 14)
        Me.LinkLabelUILaunchPrescriberArea.TabIndex = 1
        Me.LinkLabelUILaunchPrescriberArea.TabStop = True
        Me.LinkLabelUILaunchPrescriberArea.Text = "https://ui.staging.epcsdrfirst.com/pob/login"
        '
        'btnEnrollPrescriber
        '
        Me.btnEnrollPrescriber.BackgroundImage = CType(resources.GetObject("btnEnrollPrescriber.BackgroundImage"), System.Drawing.Image)
        Me.btnEnrollPrescriber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnrollPrescriber.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnrollPrescriber.Image = CType(resources.GetObject("btnEnrollPrescriber.Image"), System.Drawing.Image)
        Me.btnEnrollPrescriber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEnrollPrescriber.Location = New System.Drawing.Point(99, 209)
        Me.btnEnrollPrescriber.Name = "btnEnrollPrescriber"
        Me.btnEnrollPrescriber.Size = New System.Drawing.Size(144, 28)
        Me.btnEnrollPrescriber.TabIndex = 2
        Me.btnEnrollPrescriber.Text = "Activate Prescriber"
        Me.btnEnrollPrescriber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEnrollPrescriber.UseVisualStyleBackColor = True
        '
        'btnInvitePrescriber
        '
        Me.btnInvitePrescriber.BackgroundImage = CType(resources.GetObject("btnInvitePrescriber.BackgroundImage"), System.Drawing.Image)
        Me.btnInvitePrescriber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInvitePrescriber.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInvitePrescriber.Image = CType(resources.GetObject("btnInvitePrescriber.Image"), System.Drawing.Image)
        Me.btnInvitePrescriber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInvitePrescriber.Location = New System.Drawing.Point(99, 40)
        Me.btnInvitePrescriber.Name = "btnInvitePrescriber"
        Me.btnInvitePrescriber.Size = New System.Drawing.Size(132, 28)
        Me.btnInvitePrescriber.TabIndex = 0
        Me.btnInvitePrescriber.Text = "Invite Prescriber"
        Me.btnInvitePrescriber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnInvitePrescriber.UseVisualStyleBackColor = True
        '
        'Panel30
        '
        Me.Panel30.BackgroundImage = CType(resources.GetObject("Panel30.BackgroundImage"), System.Drawing.Image)
        Me.Panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel30.Controls.Add(Me.Label29)
        Me.Panel30.Controls.Add(Me.Label82)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel30.Location = New System.Drawing.Point(1, 1)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(783, 23)
        Me.Panel30.TabIndex = 200
        Me.Panel30.TabStop = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label29.Size = New System.Drawing.Size(265, 17)
        Me.Label29.TabIndex = 131
        Me.Label29.Text = "E-Prescribing Control Substance Settings"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(0, 22)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(783, 1)
        Me.Label82.TabIndex = 130
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label138.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Location = New System.Drawing.Point(784, 1)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 665)
        Me.Label138.TabIndex = 165
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Location = New System.Drawing.Point(0, 1)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(1, 665)
        Me.Label143.TabIndex = 164
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label144.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Location = New System.Drawing.Point(0, 0)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(785, 1)
        Me.Label144.TabIndex = 163
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Location = New System.Drawing.Point(0, 666)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(785, 1)
        Me.Label145.TabIndex = 162
        '
        'tbpg_ePASettings
        '
        Me.tbpg_ePASettings.Controls.Add(Me.pnlPARoles)
        Me.tbpg_ePASettings.Location = New System.Drawing.Point(4, 23)
        Me.tbpg_ePASettings.Name = "tbpg_ePASettings"
        Me.tbpg_ePASettings.Size = New System.Drawing.Size(791, 673)
        Me.tbpg_ePASettings.TabIndex = 8
        Me.tbpg_ePASettings.Text = "EPA User Roles"
        Me.tbpg_ePASettings.UseVisualStyleBackColor = True
        '
        'pnlPARoles
        '
        Me.pnlPARoles.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPARoles.Controls.Add(Me.pnlReviewer)
        Me.pnlPARoles.Controls.Add(Me.pnlPreparer)
        Me.pnlPARoles.Controls.Add(Me.pnlSubmitter)
        Me.pnlPARoles.Controls.Add(Me.pnlLightHeader)
        Me.pnlPARoles.Controls.Add(Me.btnRemoveRole)
        Me.pnlPARoles.Controls.Add(Me.btnAddRole)
        Me.pnlPARoles.Controls.Add(Me.Panel32)
        Me.pnlPARoles.Controls.Add(Me.Label163)
        Me.pnlPARoles.Controls.Add(Me.Label164)
        Me.pnlPARoles.Controls.Add(Me.Label165)
        Me.pnlPARoles.Controls.Add(Me.Label166)
        Me.pnlPARoles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPARoles.Enabled = False
        Me.pnlPARoles.Location = New System.Drawing.Point(0, 0)
        Me.pnlPARoles.Name = "pnlPARoles"
        Me.pnlPARoles.Size = New System.Drawing.Size(791, 673)
        Me.pnlPARoles.TabIndex = 2
        '
        'pnlReviewer
        '
        Me.pnlReviewer.BackColor = System.Drawing.Color.White
        Me.pnlReviewer.Controls.Add(Me.lstReviewer)
        Me.pnlReviewer.Controls.Add(Me.Label191)
        Me.pnlReviewer.Controls.Add(Me.Label189)
        Me.pnlReviewer.Controls.Add(Me.pnllblReviewerHeader)
        Me.pnlReviewer.Controls.Add(Me.lblReviewerLeftMain)
        Me.pnlReviewer.Controls.Add(Me.lblReviewerRightMain)
        Me.pnlReviewer.Controls.Add(Me.lblReviewerBottomMain)
        Me.pnlReviewer.Controls.Add(Me.lblReviewerTopMain)
        Me.pnlReviewer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlReviewer.Location = New System.Drawing.Point(400, 420)
        Me.pnlReviewer.Name = "pnlReviewer"
        Me.pnlReviewer.Size = New System.Drawing.Size(376, 180)
        Me.pnlReviewer.TabIndex = 233
        Me.pnlReviewer.TabStop = True
        '
        'lstReviewer
        '
        Me.lstReviewer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstReviewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstReviewer.FormattingEnabled = True
        Me.lstReviewer.ItemHeight = 14
        Me.lstReviewer.Location = New System.Drawing.Point(4, 29)
        Me.lstReviewer.Name = "lstReviewer"
        Me.lstReviewer.Size = New System.Drawing.Size(371, 150)
        Me.lstReviewer.TabIndex = 17
        '
        'Label191
        '
        Me.Label191.BackColor = System.Drawing.Color.White
        Me.Label191.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label191.Location = New System.Drawing.Point(4, 26)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(371, 3)
        Me.Label191.TabIndex = 45
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.White
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Location = New System.Drawing.Point(1, 26)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(3, 153)
        Me.Label189.TabIndex = 46
        '
        'pnllblReviewerHeader
        '
        Me.pnllblReviewerHeader.BackgroundImage = CType(resources.GetObject("pnllblReviewerHeader.BackgroundImage"), System.Drawing.Image)
        Me.pnllblReviewerHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblReviewerHeader.Controls.Add(Me.btnReviewer)
        Me.pnllblReviewerHeader.Controls.Add(Me.lblReviewerHeaderBottom)
        Me.pnllblReviewerHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllblReviewerHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblReviewerHeader.Location = New System.Drawing.Point(1, 1)
        Me.pnllblReviewerHeader.Name = "pnllblReviewerHeader"
        Me.pnllblReviewerHeader.Size = New System.Drawing.Size(374, 25)
        Me.pnllblReviewerHeader.TabIndex = 44
        '
        'btnReviewer
        '
        Me.btnReviewer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnReviewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReviewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReviewer.FlatAppearance.BorderSize = 0
        Me.btnReviewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReviewer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReviewer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnReviewer.Location = New System.Drawing.Point(0, 0)
        Me.btnReviewer.Name = "btnReviewer"
        Me.btnReviewer.Size = New System.Drawing.Size(374, 24)
        Me.btnReviewer.TabIndex = 15
        Me.btnReviewer.Text = "  Reviewer"
        Me.btnReviewer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReviewer.UseVisualStyleBackColor = True
        '
        'lblReviewerHeaderBottom
        '
        Me.lblReviewerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReviewerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblReviewerHeaderBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblReviewerHeaderBottom.Location = New System.Drawing.Point(0, 24)
        Me.lblReviewerHeaderBottom.Name = "lblReviewerHeaderBottom"
        Me.lblReviewerHeaderBottom.Size = New System.Drawing.Size(374, 1)
        Me.lblReviewerHeaderBottom.TabIndex = 13
        Me.lblReviewerHeaderBottom.Text = "label2"
        '
        'lblReviewerLeftMain
        '
        Me.lblReviewerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReviewerLeftMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblReviewerLeftMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblReviewerLeftMain.Location = New System.Drawing.Point(0, 1)
        Me.lblReviewerLeftMain.Name = "lblReviewerLeftMain"
        Me.lblReviewerLeftMain.Size = New System.Drawing.Size(1, 178)
        Me.lblReviewerLeftMain.TabIndex = 51
        Me.lblReviewerLeftMain.Text = "label2"
        '
        'lblReviewerRightMain
        '
        Me.lblReviewerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReviewerRightMain.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblReviewerRightMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblReviewerRightMain.Location = New System.Drawing.Point(375, 1)
        Me.lblReviewerRightMain.Name = "lblReviewerRightMain"
        Me.lblReviewerRightMain.Size = New System.Drawing.Size(1, 178)
        Me.lblReviewerRightMain.TabIndex = 52
        Me.lblReviewerRightMain.Text = "label2"
        '
        'lblReviewerBottomMain
        '
        Me.lblReviewerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReviewerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblReviewerBottomMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblReviewerBottomMain.Location = New System.Drawing.Point(0, 179)
        Me.lblReviewerBottomMain.Name = "lblReviewerBottomMain"
        Me.lblReviewerBottomMain.Size = New System.Drawing.Size(376, 1)
        Me.lblReviewerBottomMain.TabIndex = 53
        Me.lblReviewerBottomMain.Text = "label2"
        '
        'lblReviewerTopMain
        '
        Me.lblReviewerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReviewerTopMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblReviewerTopMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblReviewerTopMain.Location = New System.Drawing.Point(0, 0)
        Me.lblReviewerTopMain.Name = "lblReviewerTopMain"
        Me.lblReviewerTopMain.Size = New System.Drawing.Size(376, 1)
        Me.lblReviewerTopMain.TabIndex = 54
        Me.lblReviewerTopMain.Text = "label2"
        '
        'pnlPreparer
        '
        Me.pnlPreparer.BackColor = System.Drawing.Color.White
        Me.pnlPreparer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPreparer.Controls.Add(Me.lstPreparer)
        Me.pnlPreparer.Controls.Add(Me.Label190)
        Me.pnlPreparer.Controls.Add(Me.Label155)
        Me.pnlPreparer.Controls.Add(Me.pnllblPreparerHeader)
        Me.pnlPreparer.Controls.Add(Me.lblPreparerBottomMain)
        Me.pnlPreparer.Controls.Add(Me.lblPreparerTopMain)
        Me.pnlPreparer.Controls.Add(Me.lblPreparerLeftMain)
        Me.pnlPreparer.Controls.Add(Me.lblPreparerRightMain)
        Me.pnlPreparer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlPreparer.Location = New System.Drawing.Point(400, 228)
        Me.pnlPreparer.Name = "pnlPreparer"
        Me.pnlPreparer.Size = New System.Drawing.Size(376, 180)
        Me.pnlPreparer.TabIndex = 233
        Me.pnlPreparer.TabStop = True
        '
        'lstPreparer
        '
        Me.lstPreparer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstPreparer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPreparer.FormattingEnabled = True
        Me.lstPreparer.ItemHeight = 14
        Me.lstPreparer.Location = New System.Drawing.Point(4, 29)
        Me.lstPreparer.Name = "lstPreparer"
        Me.lstPreparer.Size = New System.Drawing.Size(371, 150)
        Me.lstPreparer.TabIndex = 17
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.White
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label190.Location = New System.Drawing.Point(4, 26)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(371, 3)
        Me.Label190.TabIndex = 45
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.White
        Me.Label155.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label155.Location = New System.Drawing.Point(1, 26)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(3, 153)
        Me.Label155.TabIndex = 46
        '
        'pnllblPreparerHeader
        '
        Me.pnllblPreparerHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnllblPreparerHeader.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.pnllblPreparerHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblPreparerHeader.Controls.Add(Me.btnPreparer)
        Me.pnllblPreparerHeader.Controls.Add(Me.lblPreparerHeaderBottom)
        Me.pnllblPreparerHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllblPreparerHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblPreparerHeader.Location = New System.Drawing.Point(1, 1)
        Me.pnllblPreparerHeader.Name = "pnllblPreparerHeader"
        Me.pnllblPreparerHeader.Size = New System.Drawing.Size(374, 25)
        Me.pnllblPreparerHeader.TabIndex = 44
        '
        'btnPreparer
        '
        Me.btnPreparer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnPreparer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPreparer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPreparer.FlatAppearance.BorderSize = 0
        Me.btnPreparer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreparer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreparer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPreparer.Location = New System.Drawing.Point(0, 0)
        Me.btnPreparer.Name = "btnPreparer"
        Me.btnPreparer.Size = New System.Drawing.Size(374, 24)
        Me.btnPreparer.TabIndex = 15
        Me.btnPreparer.Text = "  Preparer"
        Me.btnPreparer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreparer.UseVisualStyleBackColor = True
        '
        'lblPreparerHeaderBottom
        '
        Me.lblPreparerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreparerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPreparerHeaderBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreparerHeaderBottom.Location = New System.Drawing.Point(0, 24)
        Me.lblPreparerHeaderBottom.Name = "lblPreparerHeaderBottom"
        Me.lblPreparerHeaderBottom.Size = New System.Drawing.Size(374, 1)
        Me.lblPreparerHeaderBottom.TabIndex = 13
        Me.lblPreparerHeaderBottom.Text = "label2"
        '
        'lblPreparerBottomMain
        '
        Me.lblPreparerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreparerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblPreparerBottomMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreparerBottomMain.Location = New System.Drawing.Point(1, 179)
        Me.lblPreparerBottomMain.Name = "lblPreparerBottomMain"
        Me.lblPreparerBottomMain.Size = New System.Drawing.Size(374, 1)
        Me.lblPreparerBottomMain.TabIndex = 48
        Me.lblPreparerBottomMain.Text = "label2"
        '
        'lblPreparerTopMain
        '
        Me.lblPreparerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreparerTopMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPreparerTopMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreparerTopMain.Location = New System.Drawing.Point(1, 0)
        Me.lblPreparerTopMain.Name = "lblPreparerTopMain"
        Me.lblPreparerTopMain.Size = New System.Drawing.Size(374, 1)
        Me.lblPreparerTopMain.TabIndex = 49
        Me.lblPreparerTopMain.Text = "label2"
        '
        'lblPreparerLeftMain
        '
        Me.lblPreparerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreparerLeftMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPreparerLeftMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreparerLeftMain.Location = New System.Drawing.Point(0, 0)
        Me.lblPreparerLeftMain.Name = "lblPreparerLeftMain"
        Me.lblPreparerLeftMain.Size = New System.Drawing.Size(1, 180)
        Me.lblPreparerLeftMain.TabIndex = 50
        Me.lblPreparerLeftMain.Text = "label2"
        '
        'lblPreparerRightMain
        '
        Me.lblPreparerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreparerRightMain.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblPreparerRightMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblPreparerRightMain.Location = New System.Drawing.Point(375, 0)
        Me.lblPreparerRightMain.Name = "lblPreparerRightMain"
        Me.lblPreparerRightMain.Size = New System.Drawing.Size(1, 180)
        Me.lblPreparerRightMain.TabIndex = 51
        Me.lblPreparerRightMain.Text = "label2"
        '
        'pnlSubmitter
        '
        Me.pnlSubmitter.BackColor = System.Drawing.Color.White
        Me.pnlSubmitter.Controls.Add(Me.Label160)
        Me.pnlSubmitter.Controls.Add(Me.lstSubmitter)
        Me.pnlSubmitter.Controls.Add(Me.Label159)
        Me.pnlSubmitter.Controls.Add(Me.pnllblSubmitterHeader)
        Me.pnlSubmitter.Controls.Add(Me.lblSubmitterBottomMain)
        Me.pnlSubmitter.Controls.Add(Me.lblSubmitterTopMain)
        Me.pnlSubmitter.Controls.Add(Me.lblSubmitterLeftMain)
        Me.pnlSubmitter.Controls.Add(Me.lblSubmitterRightMain)
        Me.pnlSubmitter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlSubmitter.Location = New System.Drawing.Point(400, 36)
        Me.pnlSubmitter.Name = "pnlSubmitter"
        Me.pnlSubmitter.Size = New System.Drawing.Size(376, 180)
        Me.pnlSubmitter.TabIndex = 232
        Me.pnlSubmitter.TabStop = True
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.White
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label160.Location = New System.Drawing.Point(4, 26)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(371, 3)
        Me.Label160.TabIndex = 45
        '
        'lstSubmitter
        '
        Me.lstSubmitter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstSubmitter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lstSubmitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSubmitter.FormattingEnabled = True
        Me.lstSubmitter.ItemHeight = 14
        Me.lstSubmitter.Location = New System.Drawing.Point(4, 26)
        Me.lstSubmitter.Name = "lstSubmitter"
        Me.lstSubmitter.Size = New System.Drawing.Size(371, 153)
        Me.lstSubmitter.TabIndex = 16
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.White
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label159.Location = New System.Drawing.Point(1, 26)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(3, 153)
        Me.Label159.TabIndex = 46
        '
        'pnllblSubmitterHeader
        '
        Me.pnllblSubmitterHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblSubmitterHeader.Controls.Add(Me.lblSubmitterBottom)
        Me.pnllblSubmitterHeader.Controls.Add(Me.btnSubmitter)
        Me.pnllblSubmitterHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllblSubmitterHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblSubmitterHeader.Location = New System.Drawing.Point(1, 1)
        Me.pnllblSubmitterHeader.Name = "pnllblSubmitterHeader"
        Me.pnllblSubmitterHeader.Size = New System.Drawing.Size(374, 25)
        Me.pnllblSubmitterHeader.TabIndex = 44
        '
        'lblSubmitterBottom
        '
        Me.lblSubmitterBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubmitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblSubmitterBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubmitterBottom.Location = New System.Drawing.Point(0, 24)
        Me.lblSubmitterBottom.Name = "lblSubmitterBottom"
        Me.lblSubmitterBottom.Size = New System.Drawing.Size(374, 1)
        Me.lblSubmitterBottom.TabIndex = 15
        Me.lblSubmitterBottom.Text = "label2"
        '
        'btnSubmitter
        '
        Me.btnSubmitter.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnSubmitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSubmitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSubmitter.FlatAppearance.BorderSize = 0
        Me.btnSubmitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubmitter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmitter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSubmitter.Location = New System.Drawing.Point(0, 0)
        Me.btnSubmitter.Name = "btnSubmitter"
        Me.btnSubmitter.Size = New System.Drawing.Size(374, 25)
        Me.btnSubmitter.TabIndex = 14
        Me.btnSubmitter.Text = "  Submitter"
        Me.btnSubmitter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSubmitter.UseVisualStyleBackColor = True
        '
        'lblSubmitterBottomMain
        '
        Me.lblSubmitterBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubmitterBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblSubmitterBottomMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubmitterBottomMain.Location = New System.Drawing.Point(1, 179)
        Me.lblSubmitterBottomMain.Name = "lblSubmitterBottomMain"
        Me.lblSubmitterBottomMain.Size = New System.Drawing.Size(374, 1)
        Me.lblSubmitterBottomMain.TabIndex = 47
        Me.lblSubmitterBottomMain.Text = "label2"
        '
        'lblSubmitterTopMain
        '
        Me.lblSubmitterTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubmitterTopMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSubmitterTopMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubmitterTopMain.Location = New System.Drawing.Point(1, 0)
        Me.lblSubmitterTopMain.Name = "lblSubmitterTopMain"
        Me.lblSubmitterTopMain.Size = New System.Drawing.Size(374, 1)
        Me.lblSubmitterTopMain.TabIndex = 48
        Me.lblSubmitterTopMain.Text = "label2"
        '
        'lblSubmitterLeftMain
        '
        Me.lblSubmitterLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubmitterLeftMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSubmitterLeftMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubmitterLeftMain.Location = New System.Drawing.Point(0, 0)
        Me.lblSubmitterLeftMain.Name = "lblSubmitterLeftMain"
        Me.lblSubmitterLeftMain.Size = New System.Drawing.Size(1, 180)
        Me.lblSubmitterLeftMain.TabIndex = 49
        Me.lblSubmitterLeftMain.Text = "label2"
        '
        'lblSubmitterRightMain
        '
        Me.lblSubmitterRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSubmitterRightMain.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblSubmitterRightMain.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubmitterRightMain.Location = New System.Drawing.Point(375, 0)
        Me.lblSubmitterRightMain.Name = "lblSubmitterRightMain"
        Me.lblSubmitterRightMain.Size = New System.Drawing.Size(1, 180)
        Me.lblSubmitterRightMain.TabIndex = 50
        Me.lblSubmitterRightMain.Text = "label2"
        '
        'pnlLightHeader
        '
        Me.pnlLightHeader.Controls.Add(Me.pnl)
        Me.pnlLightHeader.Controls.Add(Me.pnlMids)
        Me.pnlLightHeader.Location = New System.Drawing.Point(14, 36)
        Me.pnlLightHeader.Name = "pnlLightHeader"
        Me.pnlLightHeader.Size = New System.Drawing.Size(314, 564)
        Me.pnlLightHeader.TabIndex = 231
        Me.pnlLightHeader.TabStop = True
        '
        'pnl
        '
        Me.pnl.BackColor = System.Drawing.Color.Transparent
        Me.pnl.Controls.Add(Me.lstUsers)
        Me.pnl.Controls.Add(Me.Label157)
        Me.pnl.Controls.Add(Me.Label158)
        Me.pnl.Controls.Add(Me.lblBottom)
        Me.pnl.Controls.Add(Me.lblLeft)
        Me.pnl.Controls.Add(Me.lblRight)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 25)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(314, 539)
        Me.pnl.TabIndex = 207
        '
        'lstUsers
        '
        Me.lstUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstUsers.FormattingEnabled = True
        Me.lstUsers.ItemHeight = 14
        Me.lstUsers.Location = New System.Drawing.Point(4, 3)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.Size = New System.Drawing.Size(309, 535)
        Me.lstUsers.TabIndex = 15
        '
        'Label157
        '
        Me.Label157.BackColor = System.Drawing.Color.White
        Me.Label157.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label157.Location = New System.Drawing.Point(1, 3)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(3, 535)
        Me.Label157.TabIndex = 14
        '
        'Label158
        '
        Me.Label158.BackColor = System.Drawing.Color.White
        Me.Label158.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label158.Location = New System.Drawing.Point(1, 0)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(312, 3)
        Me.Label158.TabIndex = 13
        '
        'lblBottom
        '
        Me.lblBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBottom.Location = New System.Drawing.Point(1, 538)
        Me.lblBottom.Name = "lblBottom"
        Me.lblBottom.Size = New System.Drawing.Size(312, 1)
        Me.lblBottom.TabIndex = 12
        Me.lblBottom.Text = "label1"
        '
        'lblLeft
        '
        Me.lblLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLeft.Location = New System.Drawing.Point(0, 0)
        Me.lblLeft.Name = "lblLeft"
        Me.lblLeft.Size = New System.Drawing.Size(1, 539)
        Me.lblLeft.TabIndex = 10
        Me.lblLeft.Text = "label1"
        '
        'lblRight
        '
        Me.lblRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblRight.Location = New System.Drawing.Point(313, 0)
        Me.lblRight.Name = "lblRight"
        Me.lblRight.Size = New System.Drawing.Size(1, 539)
        Me.lblRight.TabIndex = 9
        Me.lblRight.Text = "label1"
        '
        'pnlMids
        '
        Me.pnlMids.BackgroundImage = CType(resources.GetObject("pnlMids.BackgroundImage"), System.Drawing.Image)
        Me.pnlMids.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMids.Controls.Add(Me.Label185)
        Me.pnlMids.Controls.Add(Me.Label186)
        Me.pnlMids.Controls.Add(Me.Label187)
        Me.pnlMids.Controls.Add(Me.Label188)
        Me.pnlMids.Controls.Add(Me.lblLightHeader)
        Me.pnlMids.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMids.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMids.Location = New System.Drawing.Point(0, 0)
        Me.pnlMids.Name = "pnlMids"
        Me.pnlMids.Size = New System.Drawing.Size(314, 25)
        Me.pnlMids.TabIndex = 44
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label185.Location = New System.Drawing.Point(1, 24)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(312, 1)
        Me.Label185.TabIndex = 13
        Me.Label185.Text = "label2"
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label186.Location = New System.Drawing.Point(0, 1)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(1, 24)
        Me.Label186.TabIndex = 12
        Me.Label186.Text = "label4"
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label187.Location = New System.Drawing.Point(313, 1)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(1, 24)
        Me.Label187.TabIndex = 11
        Me.Label187.Text = "Label187"
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label188.Location = New System.Drawing.Point(0, 0)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(314, 1)
        Me.Label188.TabIndex = 10
        Me.Label188.Text = "label1"
        '
        'lblLightHeader
        '
        Me.lblLightHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblLightHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLightHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblLightHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLightHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLightHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLightHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblLightHeader.Name = "lblLightHeader"
        Me.lblLightHeader.Size = New System.Drawing.Size(314, 25)
        Me.lblLightHeader.TabIndex = 9
        Me.lblLightHeader.Text = "  Users"
        Me.lblLightHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRemoveRole
        '
        Me.btnRemoveRole.BackColor = System.Drawing.Color.Transparent
        Me.btnRemoveRole.BackgroundImage = CType(resources.GetObject("btnRemoveRole.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveRole.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemoveRole.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveRole.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveRole.Image = CType(resources.GetObject("btnRemoveRole.Image"), System.Drawing.Image)
        Me.btnRemoveRole.Location = New System.Drawing.Point(344, 323)
        Me.btnRemoveRole.Name = "btnRemoveRole"
        Me.btnRemoveRole.Size = New System.Drawing.Size(38, 36)
        Me.btnRemoveRole.TabIndex = 226
        Me.btnRemoveRole.UseVisualStyleBackColor = False
        '
        'btnAddRole
        '
        Me.btnAddRole.BackColor = System.Drawing.Color.Transparent
        Me.btnAddRole.BackgroundImage = CType(resources.GetObject("btnAddRole.BackgroundImage"), System.Drawing.Image)
        Me.btnAddRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddRole.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddRole.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRole.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddRole.Image = CType(resources.GetObject("btnAddRole.Image"), System.Drawing.Image)
        Me.btnAddRole.Location = New System.Drawing.Point(344, 276)
        Me.btnAddRole.Name = "btnAddRole"
        Me.btnAddRole.Size = New System.Drawing.Size(38, 36)
        Me.btnAddRole.TabIndex = 226
        Me.btnAddRole.UseVisualStyleBackColor = False
        '
        'Panel32
        '
        Me.Panel32.BackgroundImage = CType(resources.GetObject("Panel32.BackgroundImage"), System.Drawing.Image)
        Me.Panel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel32.Controls.Add(Me.Label161)
        Me.Panel32.Controls.Add(Me.Label162)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel32.Location = New System.Drawing.Point(1, 1)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Size = New System.Drawing.Size(789, 23)
        Me.Panel32.TabIndex = 200
        Me.Panel32.TabStop = True
        '
        'Label161
        '
        Me.Label161.AutoSize = True
        Me.Label161.BackColor = System.Drawing.Color.Transparent
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Location = New System.Drawing.Point(0, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label161.Size = New System.Drawing.Size(249, 17)
        Me.Label161.TabIndex = 131
        Me.Label161.Text = "Electronic Prior Authorization Settings"
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
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label163.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label163.Location = New System.Drawing.Point(790, 1)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(1, 671)
        Me.Label163.TabIndex = 165
        '
        'Label164
        '
        Me.Label164.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label164.Location = New System.Drawing.Point(0, 1)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(1, 671)
        Me.Label164.TabIndex = 164
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label165.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label165.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label165.Location = New System.Drawing.Point(0, 0)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(791, 1)
        Me.Label165.TabIndex = 163
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label166.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label166.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Location = New System.Drawing.Point(0, 672)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(791, 1)
        Me.Label166.TabIndex = 162
        '
        'tbpg_CCDSettings
        '
        Me.tbpg_CCDSettings.Controls.Add(Me.Panel10)
        Me.tbpg_CCDSettings.Location = New System.Drawing.Point(4, 23)
        Me.tbpg_CCDSettings.Name = "tbpg_CCDSettings"
        Me.tbpg_CCDSettings.Size = New System.Drawing.Size(791, 616)
        Me.tbpg_CCDSettings.TabIndex = 6
        Me.tbpg_CCDSettings.Text = "CDS Settings"
        Me.tbpg_CCDSettings.UseVisualStyleBackColor = True
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel10.Controls.Add(Me.Panel12)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(791, 616)
        Me.Panel10.TabIndex = 0
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.cmbCatalogCode)
        Me.Panel12.Controls.Add(Me.btn_ViewCatalog)
        Me.Panel12.Controls.Add(Me.Panel19)
        Me.Panel12.Controls.Add(Me.Label98)
        Me.Panel12.Controls.Add(Me.Label97)
        Me.Panel12.Controls.Add(Me.Label96)
        Me.Panel12.Controls.Add(Me.Label76)
        Me.Panel12.Controls.Add(Me.Label72)
        Me.Panel12.Controls.Add(Me.txtCDS_PESUrl)
        Me.Panel12.Controls.Add(Me.txtCDS_PESPassword)
        Me.Panel12.Controls.Add(Me.txtCDS_PESUserName)
        Me.Panel12.Controls.Add(Me.Label741)
        Me.Panel12.Controls.Add(Me.Label699)
        Me.Panel12.Controls.Add(Me.Label725)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(791, 616)
        Me.Panel12.TabIndex = 0
        '
        'cmbCatalogCode
        '
        Me.cmbCatalogCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCatalogCode.Enabled = False
        Me.cmbCatalogCode.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmbCatalogCode.FormattingEnabled = True
        Me.cmbCatalogCode.Items.AddRange(New Object() {" "})
        Me.cmbCatalogCode.Location = New System.Drawing.Point(283, 55)
        Me.cmbCatalogCode.Name = "cmbCatalogCode"
        Me.cmbCatalogCode.Size = New System.Drawing.Size(328, 22)
        Me.cmbCatalogCode.Sorted = True
        Me.cmbCatalogCode.TabIndex = 213
        '
        'btn_ViewCatalog
        '
        Me.btn_ViewCatalog.BackgroundImage = CType(resources.GetObject("btn_ViewCatalog.BackgroundImage"), System.Drawing.Image)
        Me.btn_ViewCatalog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ViewCatalog.Enabled = False
        Me.btn_ViewCatalog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ViewCatalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ViewCatalog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ViewCatalog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_ViewCatalog.Image = CType(resources.GetObject("btn_ViewCatalog.Image"), System.Drawing.Image)
        Me.btn_ViewCatalog.Location = New System.Drawing.Point(617, 54)
        Me.btn_ViewCatalog.Name = "btn_ViewCatalog"
        Me.btn_ViewCatalog.Size = New System.Drawing.Size(22, 22)
        Me.btn_ViewCatalog.TabIndex = 214
        Me.btn_ViewCatalog.UseVisualStyleBackColor = True
        '
        'Panel19
        '
        Me.Panel19.BackgroundImage = CType(resources.GetObject("Panel19.BackgroundImage"), System.Drawing.Image)
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Label113)
        Me.Panel19.Controls.Add(Me.Label124)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel19.Location = New System.Drawing.Point(1, 1)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(789, 23)
        Me.Panel19.TabIndex = 200
        Me.Panel19.TabStop = True
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.Color.Transparent
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Location = New System.Drawing.Point(0, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.Label113.Size = New System.Drawing.Size(218, 17)
        Me.Label113.TabIndex = 131
        Me.Label113.Text = "Clinical Decision Support Settings"
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label124.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Location = New System.Drawing.Point(0, 22)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(789, 1)
        Me.Label124.TabIndex = 130
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Location = New System.Drawing.Point(790, 1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(1, 614)
        Me.Label98.TabIndex = 165
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Location = New System.Drawing.Point(0, 1)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 614)
        Me.Label97.TabIndex = 164
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Location = New System.Drawing.Point(0, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(791, 1)
        Me.Label96.TabIndex = 163
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Location = New System.Drawing.Point(0, 615)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(791, 1)
        Me.Label76.TabIndex = 162
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Location = New System.Drawing.Point(190, 58)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(87, 14)
        Me.Label72.TabIndex = 160
        Me.Label72.Text = "Catalog Code :"
        '
        'txtCDS_PESUrl
        '
        Me.txtCDS_PESUrl.Enabled = False
        Me.txtCDS_PESUrl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDS_PESUrl.Location = New System.Drawing.Point(283, 163)
        Me.txtCDS_PESUrl.MaxLength = 1000
        Me.txtCDS_PESUrl.Name = "txtCDS_PESUrl"
        Me.txtCDS_PESUrl.Size = New System.Drawing.Size(329, 22)
        Me.txtCDS_PESUrl.TabIndex = 157
        '
        'txtCDS_PESPassword
        '
        Me.txtCDS_PESPassword.Enabled = False
        Me.txtCDS_PESPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDS_PESPassword.Location = New System.Drawing.Point(283, 127)
        Me.txtCDS_PESPassword.MaxLength = 50
        Me.txtCDS_PESPassword.Name = "txtCDS_PESPassword"
        Me.txtCDS_PESPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCDS_PESPassword.Size = New System.Drawing.Size(329, 22)
        Me.txtCDS_PESPassword.TabIndex = 159
        '
        'txtCDS_PESUserName
        '
        Me.txtCDS_PESUserName.Enabled = False
        Me.txtCDS_PESUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDS_PESUserName.Location = New System.Drawing.Point(283, 91)
        Me.txtCDS_PESUserName.MaxLength = 50
        Me.txtCDS_PESUserName.Name = "txtCDS_PESUserName"
        Me.txtCDS_PESUserName.Size = New System.Drawing.Size(329, 22)
        Me.txtCDS_PESUserName.TabIndex = 158
        '
        'Label741
        '
        Me.Label741.AutoSize = True
        Me.Label741.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label741.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label741.Location = New System.Drawing.Point(186, 130)
        Me.Label741.Name = "Label741"
        Me.Label741.Size = New System.Drawing.Size(91, 14)
        Me.Label741.TabIndex = 156
        Me.Label741.Text = "PES Password :"
        '
        'Label699
        '
        Me.Label699.AutoSize = True
        Me.Label699.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label699.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label699.Location = New System.Drawing.Point(178, 94)
        Me.Label699.Name = "Label699"
        Me.Label699.Size = New System.Drawing.Size(99, 14)
        Me.Label699.TabIndex = 155
        Me.Label699.Text = "PES User Name :"
        '
        'Label725
        '
        Me.Label725.AutoSize = True
        Me.Label725.BackColor = System.Drawing.Color.Transparent
        Me.Label725.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label725.Location = New System.Drawing.Point(216, 166)
        Me.Label725.Name = "Label725"
        Me.Label725.Size = New System.Drawing.Size(61, 14)
        Me.Label725.TabIndex = 153
        Me.Label725.Text = "PES URL :"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'fdSignature
        '
        Me.fdSignature.ShowEffects = False
        '
        'frmDoctor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(799, 756)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Cursor = System.Windows.Forms.Cursors.Default
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
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.grpSPI.ResumeLayout(False)
        Me.grpSPI.PerformLayout()
        Me.pnlSPI.ResumeLayout(False)
        Me.pnlSPI.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.grpUserDetails.ResumeLayout(False)
        Me.grpUserDetails.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.udMaxScale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udMinScale, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel14.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tbpgProvider.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel25.ResumeLayout(False)
        Me.pnlchkRequireSupervisingProviderRx.ResumeLayout(False)
        Me.pnlchkRequireSupervisingProviderRx.PerformLayout()
        Me.Panel27.ResumeLayout(False)
        Me.pnlLicenseKey.ResumeLayout(False)
        Me.pnlLicenseKey.PerformLayout()
        Me.tbpgBillingID.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.gbProviderIdentification.ResumeLayout(False)
        CType(Me.c1ProviderIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.tbpgProviderCompany.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        CType(Me.c1CompanyProvIdentification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel24.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.tbpgStatement.ResumeLayout(False)
        Me.Panel34.ResumeLayout(False)
        Me.Panel34.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.tbpg_EpcsSettings.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        Me.tbpg_ePASettings.ResumeLayout(False)
        Me.pnlPARoles.ResumeLayout(False)
        Me.pnlReviewer.ResumeLayout(False)
        Me.pnllblReviewerHeader.ResumeLayout(False)
        Me.pnlPreparer.ResumeLayout(False)
        Me.pnllblPreparerHeader.ResumeLayout(False)
        Me.pnlSubmitter.ResumeLayout(False)
        Me.pnllblSubmitterHeader.ResumeLayout(False)
        Me.pnlLightHeader.ResumeLayout(False)
        Me.pnl.ResumeLayout(False)
        Me.pnlMids.ResumeLayout(False)
        Me.Panel32.ResumeLayout(False)
        Me.Panel32.PerformLayout()
        Me.tbpg_CCDSettings.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property ServiceLevel() As String
        Get
            Return ServiceLevelCode
        End Get
        Set(ByVal Value As String)
            ServiceLevelCode = Value
        End Set
    End Property
    Public Property ProviderSPI() As String
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

    Public PDROnFormLoad As Boolean
    Public PDNMPOnFormLoad As Boolean
    '------------------------------------------------------------------

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Try
            TempProvider = Nothing
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Dim IsDirectMessageEnable As Boolean = False
    Private Sub changeHeightAsPerResolution() 'Added by Ojeswini for 1980x1080 dpi settings Resolution
        Dim myScreenHeight As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99)
        If myScreenHeight < Me.Height Then
            Me.Height = myScreenHeight
            Dim myScreenWidth As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63)
            If myScreenWidth > Me.Width Then
                Me.Width = myScreenWidth
            End If
        End If
    End Sub
    Private Sub frmDoctor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        changeHeightAsPerResolution()
        gloC1FlexStyle.Style(c1ProviderIdentification)
        gloC1FlexStyle.Style(c1CompanyProvIdentification)
        Try
            Dim hgth As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            Dim wdth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim res As Integer = System.Windows.SystemParameters.FullPrimaryScreenHeight
            If res < 800 Then
                Me.MaximumSize = New System.Drawing.Size(wdth, (hgth - 50))
                Me.Height = hgth - 50
                '  Me.AutoScroll = True
            End If

            FillProviderTypes()

            DesignGrid()
            DesignCompanyAdditionalGrid()
            'code added by dipak 20090912 to fill State Combobox 
            ''Commented by dhruv 
            'fillStates(cmbBMstate)
            'fillStates(cmbBPracState)
            'fillStates(ComboBox1)
            ''End---commenting
            'end dipak 20090912
            blnLogoChanged = False
            txtPrefix.Select()

            DetachEvents()
            If gbln10dot6Version = True Then
                lblNPIMandatory.Visible = True
                chkCIMessage.Enabled = True
                ChkCIEvent.Enabled = True
            Else
                chkCIMessage.Enabled = False
                ChkCIEvent.Enabled = False
            End If
            rbPrescriber.Checked = True

            pnlSPI.Visible = False
            lblRoot.Visible = False
            dtpActiveStartTime.Value = DateTime.Now
            dtpActiveStartTime.Enabled = True
            dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
            dtpActiveEndTime.Enabled = True


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
            Else
                rbUpdate.Enabled = False
                lblAusStatus.Text = 0
            End If


            'If _nProviderID > 0 Then
            '    LoadProvider(_nProviderID)
            '    rbPrescriber.Checked = False
            '    rbPrescriber.Enabled = False

            '    rbPrescriberLocation.Checked = False
            '    rbPrescriberLocation.Enabled = False

            '    rbUpdate.Checked = True
            '    rbUpdate.Enabled = True
            'Else
            '    rbUpdate.Checked = False
            '    rbUpdate.Enabled = False

            '    rbPrescriber.Checked = True
            '    rbPrescriber.Enabled = True

            '    rbPrescriberLocation.Checked = False
            '    rbPrescriberLocation.Enabled = True
            'End If

            AttachEvents()

            If cmbDoctorType.SelectedValue = 1 Then
                chkRequire_Supervising_Provider_for_eRx.Enabled = True
            Else
                chkRequire_Supervising_Provider_for_eRx.Enabled = False
            End If

            GetMultipleSupervisorsforPaperRx()
            'Dim scheme As Cls_TabIndexSettings.TabScheme = Cls_TabIndexSettings.TabScheme.AcrossFirst
            'Dim tom As New Cls_TabIndexSettings(Me)
            '' This method actually sets the order all the way down the control hierarchy.
            'tom.SetTabOrder(scheme)

            If gbln10dot6Version = True Then
                Dim objSettings As New clsSettings
                If objSettings.GetSettings() = True Then
                    IsDirectMessageEnable = objSettings.IsSecureMessageEnabled
                    If Not chckDisable.Checked Then
                        chkCIMessage.Enabled = IsDirectMessageEnable
                    End If
                End If
            Else
                chkCIMessage.Enabled = False
                ChkCIEvent.Enabled = False
            End If

            ''''added by kishor for epcs
            Dim UrlForUILaunchPrescriberArea As String = ""
            clsEPCSHelper.GetVendorAndUrlInformationForEpcs(gnClinicID, gblnIsStagingServer)
            If gblnIsStagingServer Then
                UrlForUILaunchPrescriberArea = clsEPCSHelper.sEpcsUrl & "pob/login"
                LinkLabelUILaunchPrescriberArea.Text = UrlForUILaunchPrescriberArea
            Else
                UrlForUILaunchPrescriberArea = clsEPCSHelper.sEpcsUrl & "pob/login"
                LinkLabelUILaunchPrescriberArea.Text = UrlForUILaunchPrescriberArea
            End If
            Dim organizationFlag As Boolean = frmSettings_New.GetClinicInformation("bIsEpcsEnable")
            If organizationFlag Then
                If Not chckDisable.Checked Then
                    ChckEPCS.Enabled = True
                Else
                    ChckEPCS.Enabled = False
                End If
            Else
                ChckEPCS.Enabled = False
                btnInvitePrescriber.Enabled = False
                btnEnrollPrescriber.Enabled = False
            End If

            listUsers = New List(Of EPARole)
            listSubmitter = New List(Of EPARole)
            listReviewer = New List(Of EPARole)
            listPreparer = New List(Of EPARole)

            If Not String.IsNullOrWhiteSpace(Me.ProviderSPI) Then
                Me.FillEPARoles()
                Me.lstSubmitter_MouseClick(Me, Nothing)
            Else
                If Me.TabControl1.TabPages.ContainsKey("tbpg_ePASettings") Then
                    Me.TabControl1.TabPages.RemoveByKey("tbpg_ePASettings")
                End If
            End If
            If _nProviderID > 0 And txtLicenseKey.Text.Trim <> "" Then
                Dim sServicelvl As String = ""
                Dim bisPDR As Boolean = False
                Dim bisPDMP As Boolean = False
                Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                    sServicelvl = oLicense.GetLicensedServiceLevel(txtLicenseKey.Text)
                    bisPDR = oLicense.getLicensedPDRFlag(txtLicenseKey.Text)
                    bisPDMP = oLicense.getLicensedPDMPFlag(txtLicenseKey.Text)
                End Using
                SetLicenceServiceLevels(sServicelvl, bisPDR, bisPDMP)
                If ISDEMOLicensce = False And lblAusStatus.Text.Trim <> "4" Then
                    chckDisable.Enabled = False
                End If
            Else
                chckDisable.Enabled = True
            End If
            If ChckEPCS.Checked = True Then
                chkPDMP.Enabled = True
            Else
                chkPDMP.Enabled = False
            End If
            PDROnFormLoad = chkPDR.Checked
            PDNMPOnFormLoad = chkPDMP.Checked
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Unable to Load the provider Form due to " & objErr.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Retrieve, True)
        End Try
    End Sub
    'Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

    '    Try
    '        If Trim(txtPrefix.Text) = "" Then
    '            MessageBox.Show("Please enter Prefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtPrefix.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtFirstName.Text) = "" Then
    '            MessageBox.Show("Please enter First Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtFirstName.Focus()
    '            Exit Sub
    '        End If

    '        '******By Sandip Deshmukh 20 Oct 2007 11.15 a.m. 
    '        '******For bug reported the control for Phone No. is 
    '        '******modified from Textbox to MaskedTextbox(10 digit numeric )and following code is added
    '        Me.Cursor = Cursors.Default
    '        'If Len(Trim(txtPhoneNo.Text)) > 0 And Len(Trim(txtPhoneNo.Text)) < 10 Then
    '        '    MessageBox.Show("Phone Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    txtPhoneNo.Focus()
    '        '    Exit Sub
    '        'End If
    '        If (txtPhoneNo.IsValidated = False) Then
    '            Exit Sub
    '        End If
    '        Me.Cursor = Cursors.Default
    '        'If Len(Trim(txtMobileNo.Text)) > 0 And Len(Trim(txtMobileNo.Text)) < 10 Then
    '        '    MessageBox.Show("Mobile No. Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    txtMobileNo.Focus()
    '        '    Exit Sub
    '        'End If
    '        If (txtMobileNo.IsValidated = False) Then
    '            Exit Sub
    '        End If
    '        '******20 Oct 2007 11.15 a.m. 


    '        '******By Ravikiran 21 feb 2008 11.15 a.m. 
    '        '******To Satisfy Surescript requiremnts

    '        If Trim(txtLastName.Text) = "" Then
    '            MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtLastName.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtAddress.Text) = "" Then
    '            MessageBox.Show("Please enter Address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtAddress.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtCity.Text) = "" Then
    '            MessageBox.Show("Please enter City", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtCity.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtState.Text) = "" Then
    '            MessageBox.Show("Please enter State", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtState.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtZIP.Text) = "" Then
    '            MessageBox.Show("Please enter ZIP code", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtZIP.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtLastName.Text) = "" Then
    '            MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtLastName.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtPhoneNo.Text) = "" Then
    '            MessageBox.Show("Please enter Phone No", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtPhoneNo.Focus()
    '            Exit Sub
    '        End If

    '        If Trim(txtFAX.Text) = "" Then
    '            MessageBox.Show("Please enter Fax", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtFAX.Focus()
    '            Exit Sub
    '        End If

    '        If gblnIsSurescriptEnabled = True Then
    '            If Trim(txtDEA.Text) = "" Then
    '                MessageBox.Show("Please enter DEA Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtDEA.Focus()
    '                Exit Sub
    '            End If
    '        End If


    '        Me.Cursor = Cursors.Default
    '        If Len(Trim(txtDEA.Text)) > 0 And Len(Trim(txtDEA.Text)) < 9 Then
    '            MessageBox.Show("DEA Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            txtMobileNo.Focus()
    '            Exit Sub
    '        End If

    '        If gblnIsSurescriptEnabled Then
    '            If chckDisable.Checked = False And chckNew.Checked = False And chckRefill.Checked = False Then
    '                MessageBox.Show("Service Level Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                chckDisable.Focus()
    '                Exit Sub
    '            End If
    '            If rbPrescriberLocation.Checked Then
    '                If txtSPI.Text.Trim = "" Then
    '                    MessageBox.Show("Please enter SPI Number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    txtSPI.Focus()
    '                    Exit Sub
    '                End If
    '            End If
    '            If DateTime.Compare(dtpActiveEndTime.Value, dtpActiveStartTime.Value) <= 0 Then
    '                MessageBox.Show("Active End Date should be always greater than Active Start Time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                dtpActiveEndTime.Focus()
    '                Exit Sub
    '            End If
    '            If chckDisable.Checked = False Then
    '                If DateTime.Compare(dtpActiveEndTime.Value, DateTime.Now) <= 0 Then
    '                    MessageBox.Show("Active End Date should be always greater than Today", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    dtpActiveEndTime.Focus()
    '                    Exit Sub
    '                End If
    '            End If
    '        End If

    '        If blnModify = False Then
    '            'Check User Name must be entered
    '            If Trim(txtUserName.Text) = "" Then
    '                MessageBox.Show("Please enter User Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtUserName.Focus()
    '                Exit Sub
    '            End If
    '            'Check Password must be entered
    '            If Trim(txtPassword.Text) = "" Then
    '                MessageBox.Show("Please enter Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtPassword.Focus()
    '                Exit Sub
    '            End If
    '            'Check Confirm Password must be entered
    '            If Trim(txtConfirmPassword.Text) = "" Then
    '                MessageBox.Show("Please enter Confirm Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtConfirmPassword.Focus()
    '                Exit Sub
    '            End If
    '            'Check Password and Confirm Password must be same
    '            If Trim(txtConfirmPassword.Text) <> Trim(txtPassword.Text) Then
    '                MessageBox.Show("Password and Confirm Password must be same", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                txtPassword.Focus()
    '                Exit Sub
    '            End If
    '            '******By Sandip Deshmukh 13 Oct 2007  12.30 PM  Bug# 328
    '            '******The Code is commented for the following 
    '            '******Dont keep Nick Name as Mandetory , Remove validation and make it invisible
    '            'Check Nick Name must be entered
    '            ' ''If Trim(txtNickName.Text) = "" Then
    '            ' ''    MessageBox.Show("Please enter Nick Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            ' ''    txtNickName.Focus()
    '            ' ''    Exit Sub
    '            ' ''End If
    '            '******13 Oct 2007  12.30 PM Bug# 328

    '            'Check User Name already exists or not
    '            Dim objUser As New clsUsers(gstrConnectionString)
    '            If objUser.CheckUserExists(txtUserName.Text) = True Then
    '                Me.Cursor = Cursors.Default
    '                MessageBox.Show("User already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                objUser = Nothing
    '                txtUserName.Focus()
    '                Exit Sub
    '            End If
    '            objUser = Nothing
    '        End If
    '        Dim objProvider As New clsProvider(gstrConnectionString)
    '        'Check Provider already exists or not
    '        If blnModify = True Then
    '            If objProvider.CheckProviderExists(txtFirstName.Text + Space(2) + txtMiddleName.Text + Space(2) + txtLastName.Text, txtFirstName.Tag) = True Then
    '                MessageBox.Show("Provider already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                objProvider = Nothing
    '                txtFirstName.Focus()
    '                Exit Sub
    '            End If
    '        Else
    '            If objProvider.CheckProviderExists(txtFirstName.Text + Space(2) + txtMiddleName.Text + Space(2) + txtLastName.Text) = True Then
    '                MessageBox.Show("Provider already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                objProvider = Nothing
    '                txtFirstName.Focus()
    '                Exit Sub
    '            End If
    '        End If


    '        With objProvider
    '            .FirstName = txtFirstName.Text
    '            .MiddleName = txtMiddleName.Text
    '            .LastName = txtLastName.Text
    '            .Address = txtAddress.Text
    '            .Street = txtStreet.Text
    '            .City = txtCity.Text
    '            .State = txtState.Text
    '            .ZIP = txtZIP.Text
    '            .Phone = txtPhoneNo.Text
    '            .Mobile = txtMobileNo.Text
    '            .FAX = txtFAX.Text
    '            .Email = txtEmailAddress.Text
    '            .URL = txtURL.Text
    '            .DEA = txtDEA.Text
    '            .Pager = txtPager.Text
    '            .NPI = txtNPI.Text
    '            .UPIN = txtUPIN.Text
    '            .StateMedicalNo = txtStateMedicalLicenseNo.Text

    '            'sarika 7th june 07
    '            .Prefix = txtPrefix.Text
    '            '------------------------------------
    '            If rbUpdate.Checked Then
    '                .SPI = lblSPI.Text
    '            ElseIf rbPrescriberLocation.Checked Then
    '                .RootSPI = txtSPI.Text
    '            End If

    '            .ActiveStartTime = dtpActiveStartTime.Value
    '            .ActiveEndTime = dtpActiveEndTime.Value

    '            .ServiceLevel = GetServiceLevelCode()

    '            If optMale.Checked = True Then
    '                .Gender = "Male"
    '            Else
    '                .Gender = "Female"
    '            End If

    '            Dim objProviderType As New clsProviderType(gstrConnectionString)
    '            objProviderType.SearchProviderType(cmbDoctorType.Text)
    '            .ProviderTypeID = objProviderType.ProviderTypeID()
    '            objProviderType = Nothing

    '            If blnLogoChanged = True Then
    '                If optBrowse.Checked = False Then
    '                    picSignature.Image = Nothing
    '                    If File.Exists(Application.StartupPath & "\DoctorSign.tif") = True Then
    '                        picSignature.Image = Image.FromFile(Application.StartupPath & "\DoctorSign.tif")
    '                        picSignature.SizeMode = PictureBoxSizeMode.CenterImage
    '                    End If
    '                End If

    '                If IsNothing(picSignature.Image) = False Then
    '                    .Signature = picSignature.Image
    '                Else
    '                    .Signature = Nothing
    '                End If
    '            End If

    '            If blnModify = False Then
    '                .UserName = txtUserName.Text
    '                Dim objEncryption As New clsEncryption
    '                .Password = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
    '                .NickName = objEncryption.EncryptToBase64String(txtNickName.Text, constEncryptDecryptKey)
    '                objEncryption = Nothing
    '            End If
    '        End With
    '        gstrCategory = Trim(txtFirstName.Text) & Space(2) & Trim(txtMiddleName.Text) & Space(2) & Trim(txtLastName.Text)
    '        If blnModify = True Then
    '            If objProvider.UpdateProviderDetails(txtFirstName.Tag) = True Then
    '                objProvider.ProviderID = CType(txtFirstName.Tag, Int32)
    '                UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update)
    '                If gblnIsSurescriptEnabled Then
    '                    AddUpdatePrescriber(objProvider)
    '                Else

    '                End If

    '                objProvider = Nothing
    '                Me.DialogResult = DialogResult.OK
    '                Me.Close()
    '            Else
    '                MessageBox.Show("Unable to update Provider Details due to " & objProvider.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                UpdateErrorLog("Unable to update Provider Details due to " & objProvider.ErrorMessage, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update, True)
    '                objProvider = Nothing
    '            End If
    '        Else
    '            If objProvider.InsertProviderDetails = True Then
    '                If gblnIsSurescriptEnabled Then
    '                    AddUpdatePrescriber(objProvider)
    '                Else

    '                End If

    '                objProvider = Nothing
    '                UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Add)
    '                Me.DialogResult = DialogResult.OK
    '                Me.Close()
    '            Else
    '                MessageBox.Show("Unable to insert Doctor Details", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                UpdateErrorLog("Unable to Add Provider Details due to " & objProvider.ErrorMessage, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Add, True)
    '                objProvider = Nothing
    '            End If
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        UpdateErrorLog("Unable to Add/update Provider Details due to " & objErr.ToString, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
    '    Finally

    '    End Try
    'End Sub

    'sarika for removing provider validations
    '   Private Sub AddUpdatePrescriber(ByVal objProvider As clsProvider)
    Private Function AddUpdatePrescriber(ByVal objProvider As clsProvider) As String
        Dim strSurescriptMsg As New System.Text.StringBuilder
        Dim bReturned As Boolean = False

        Try

            objPrescriber = New AddUpdatePrescriber
            If chkCIMessage.Checked = True Or ChkCIEvent.Checked = True Then
                objPrescriber.IsSecureMessage = True
                gblnSecureMessage4dot5 = True
            End If

            objPrescriber.ProviderObject = objProvider
            objPrescriber.ProviderObject.ServiceLevel = objProvider.ServiceLevel
            If rbPrescriber.Checked Then
                bReturned = objPrescriber.AddPrescriber()
                If bReturned Then
                    objProvider.UpdateSPIDetails(True)
                End If
            ElseIf rbPrescriberLocation.Checked Then
                bReturned = objPrescriber.AddPrescriberLocation()
                If bReturned Then
                    objProvider.UpdateSPIDetails(True)
                End If
            ElseIf rbUpdate.Checked Then
                If objProvider.IsCIMessageEnabled AndAlso objProvider.IsDirectMessageEnabled Then
                    If objProvider.IsDirectAddressExists Then
                        bReturned = objPrescriber.UpdatePrescriberLocation(bIsServiceLevelDisabled, True)
                    Else
                        bReturned = objPrescriber.UpdatePrescriberLocation(bIsServiceLevelDisabled, False)
                    End If
                Else
                    bReturned = objPrescriber.UpdatePrescriberLocation(bIsServiceLevelDisabled, True)

                    If txtDirectAddress.Tag IsNot Nothing Then
                        Dim DirectAddressArray() As String = txtDirectAddress.Tag
                        gloSurescriptGeneral.DeleteDirectAddressProvider(DirectAddressArray(0) + "@" + DirectAddressArray(1))

                        Array.Clear(DirectAddressArray, 0, DirectAddressArray.Length)
                        DirectAddressArray = Nothing
                    End If
                End If

                If bReturned Then
                    objProvider.UpdateSPIDetails(IsEPATurnedOn)
                End If

            End If

            Return objPrescriber.strMessage & strSurescriptMsg.ToString

        Catch ex As Exception

        Finally
            objPrescriber = Nothing
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
            '  picSignature.SizeMode = PictureBoxSizeMode.StretchImage
            With dlgOpenFile
                .Title = "Select Clinic Logo"
                .Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With
            If dlgOpenFile.ShowDialog = DialogResult.OK Then

                picSignature.Visible = True
                picSignature.SizeMode = PictureBoxSizeMode.AutoSize
                '' Removed Signature Pad Control 20090603
                'AxSigPlus1.Visible = False
                picSignature.Image = Image.FromFile(dlgOpenFile.FileName)
                ImgWidth = Image.FromFile(dlgOpenFile.FileName).Width
                'picSignature.SizeMode = PictureBoxSizeMode.AutoSize
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
            UpdateErrorLog("Uanble to capture the provider Signature due to " & ex.Message, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
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
            MessageBox.Show(ex.ToString, "Fill provider Types", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If ChkCIEvent.Checked = True Or chkCIMessage.Checked = True Then
                pnlSPI.Visible = True
            Else
                pnlSPI.Visible = False

            End If
            txtSPI.Visible = False
            lblRoot.Visible = False
            dtpActiveStartTime.Value = DateTime.Now
            dtpActiveStartTime.Enabled = True
            dtpActiveEndTime.Value = DateTime.Now.AddYears(1)
            dtpActiveEndTime.Enabled = True
            DirectAddress()
        End If
    End Sub

    Private Sub rbPrescriberLocation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPrescriberLocation.CheckedChanged
        If rbPrescriberLocation.Checked Then
            pnlSPI.Visible = True
            txtSPI.Clear()
            txtSPI.Visible = True
            txtSPI.Enabled = True
            lblRoot.Visible = True
            lblSPI.Text = SPI

            lblLabelSPI.Visible = False
            lblDirectAddressValue.Visible = False
            lblDirectAddress.Visible = False
            txtDirectAddress.Visible = False
            txtDirectAddress.Enabled = False


        End If
    End Sub

    Private Sub rbUpdate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbUpdate.CheckedChanged
        If rbUpdate.Checked Then
            pnlSPI.Visible = True
            txtSPI.Visible = False
            lblRoot.Visible = False
            lblSPI.Text = SPI

        End If
    End Sub

    Private Sub chckDisable_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chckDisable.CheckedChanged
        If chckDisable.Checked Then
            chckNew.Checked = False
            chckRefill.Checked = False
            ChckRxFill.Checked = False
            ChckChange.Checked = False
            ChckCancel.Checked = False
            chkCIMessage.Checked = False
            ChkCIEvent.Checked = False
            ChckEPCS.Checked = False
            ChkePA.Checked = False            
            chkPDR.Checked = False
            chkPDMP.Checked = False

            chckNew.Enabled = False
            chckRefill.Enabled = False
            chkCIMessage.Enabled = False
            ChkCIEvent.Enabled = False
            ChckEPCS.Enabled = False
            dtpActiveEndTime.Value = DateTime.Now
            dtpActiveEndTime.Enabled = False
            ChkePA.Enabled = False
            chkPDR.Enabled = False
            chkPDMP.Enabled = False
            ChckRxFill.Enabled = False
            ChckChange.Enabled = False
            ChckCancel.Enabled = False
        Else
            chckNew.Enabled = True
            chckRefill.Enabled = True           
            If IsDirectMessageEnable = True Then
                chkCIMessage.Enabled = True
            Else
                chkCIMessage.Enabled = False
            End If

            ChkCIEvent.Enabled = True
            Dim organizationFlag As Boolean = frmSettings_New.GetClinicInformation("bIsEpcsEnable")
            If organizationFlag Then
                ChckEPCS.Enabled = True
            Else
                ChckEPCS.Enabled = False
                btnInvitePrescriber.Enabled = False
                btnEnrollPrescriber.Enabled = False
            End If
            dtpActiveEndTime.Enabled = True
            ChkePA.Enabled = True
            chkPDR.Enabled = True
            chkPDMP.Enabled = True
            ChckRxFill.Enabled = True
            ChckChange.Enabled = True
            ChckCancel.Enabled = True
        End If
    End Sub
    Private Function GetServiceLevelCode() As String
        'If chckDisable.Checked Then
        '    Return "0000000000000000"
        'End If
        'If chckNew.Checked And chckRefill.Checked Then
        '    Return "0000000000000011"
        'End If
        'If chckNew.Checked Then
        '    Return "0000000000000001"
        'End If
        'If chckRefill.Checked Then
        '    Return "0000000000000010"
        'End If
        'Return ""
        Dim sServicelevel As String = "0000000000000000"
        If chckDisable.Checked Then
            Return sServicelevel
        Else

            If chckNew.Checked Then
                Mid(sServicelevel, 16, 1) = 1
            End If
            If chckRefill.Checked Then
                Mid(sServicelevel, 15, 1) = 1
            End If
            If ChckChange.Checked Then
                Mid(sServicelevel, 14, 1) = 1
            End If
            If ChckRxFill.Checked Then
                Mid(sServicelevel, 13, 1) = 1
            End If
            If ChckCancel.Checked Then
                Mid(sServicelevel, 12, 1) = 1
            End If
            If ChckEPCS.Checked Then
                Mid(sServicelevel, 5, 1) = 1
            End If
            If chkCIMessage.Checked Then
                Mid(sServicelevel, 2, 1) = 1
            End If
            If ChkCIEvent.Checked Then
                Mid(sServicelevel, 1, 1) = 1
            End If
            If ChkePA.Checked Then
                Mid(sServicelevel, 9, 1) = 1
            End If
            Return sServicelevel
        End If

    End Function

    Private Sub txtSPI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or e.KeyChar = ChrW(8)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click

        Try
            Dim IsLicenseused As Boolean = False
            'Mandatory fields are prefix, first name and last name

            'If Trim(txtPrefix.Text) = "" Then                                                                                       ''Dhruv 20100216 As they dont required the field as the mandatory.. 
            '    MessageBox.Show("Please enter Prefix", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtPrefix.Focus()
            '    Exit Sub                                 
            'End If                                                                                                                 ''End---------Dhruv

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
            If mskBMPhoneNo.Text <> "" Then
                If (mskBMPhoneNo.IsValidated = False) Then
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

            'END Provider Physical Address Related Validation

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
                'If Regex.IsMatch(txtBMURL.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                If (CheckURLAddress(txtBMURL.Text.Trim()) = False) Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBMURL.Focus()
                    Exit Sub
                End If
            End If
            'End of URL Validation

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            Me.Cursor = Cursors.Default
            If (txtBPracEMail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtBMEmailAddress.Text.Trim()) = False Then
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
                'If Regex.IsMatch(txtBPracUrl.Text.Trim(), "^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$") = False Then
                If (CheckURLAddress(txtBPracUrl.Text.Trim()) = False) Then
                    MessageBox.Show("Please enter a valid url.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtBPracUrl.Focus()
                    Exit Sub
                End If
            End If
            'End of URL Validation

            ' Added by Rahul Patel on 08-09-2010
            ' For email id validation
            Me.Cursor = Cursors.Default
            If (txtCompanyEmail.Text.Trim() <> "") Then
                If CheckEmailAddress(txtBMEmailAddress.Text.Trim()) = False Then
                    MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCompanyEmail.Focus()
                    Exit Sub
                End If
            End If
            ' End of Email id Validation
            ' Added by Rahul Patel on 09-09-2010
            ' For UPIN proper format i. one alphabhet and 4 or 5 character
            Me.Cursor = Cursors.Default
            If (txtUPIN.Text.Trim() <> "") Then
                If Regex.IsMatch(txtUPIN.Text.Trim(), "^[a-zA-Z]{1}[0-9]{4,5}$") = False Then
                    MessageBox.Show("Please enter a valid UPIN.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtUPIN.Focus()
                    Exit Sub
                End If
            End If

            Me.Cursor = Cursors.Default
            If Len(Trim(txtDEA.Text)) > 0 And Len(Trim(txtDEA.Text)) < 9 Then
                MessageBox.Show("Please enter DEA number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDEA.Focus()
                Exit Sub
            ElseIf ChckEPCS.Checked And Len(Trim(txtDEA.Text)) >= 0 And Len(Trim(txtDEA.Text)) < 9 Then
                MessageBox.Show("Please enter DEA number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDEA.Focus()
                Exit Sub
            End If

            If Len(Trim(txtNADEAN.Text)) > 0 And Len(Trim(txtNADEAN.Text)) < 9 Then
                MessageBox.Show("Please enter NADEAN number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNADEAN.Focus()
                Exit Sub
            End If

            Me.Cursor = Cursors.Default

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

            'As per discuusion No length or content validation on the new field
            'If Len(Trim(txtDPSNumber.Text)) > 0 And Len(Trim(txtDPSNumber.Text)) < 9 Then
            '    MessageBox.Show("DPS Number Details Incomplete", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtDPSNumber.Focus()
            '    Exit Sub
            'End If

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

            If udMinScale.Value <= udMinScale.Minimum Or udMinScale.Value >= udMinScale.Maximum Then
                MessageBox.Show("Signature minimum scale should be between 0.3 and 1.0.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                udMinScale.Value = 0.3
                udMinScale.Focus()
                Exit Sub
            End If

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




            Dim oProvider As New clsProvider(gstrConnectionString)
            If txtNPI.Text.Trim = "" Then
                MessageBox.Show("Please enter NPI number", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNPI.Focus()
                Exit Sub
            Else
                If txtNPI.Text.Length < 10 Or txtNPI.Text.Length > 10 Then ''''''NPI should be 10 digits only, if NPI is < 10 or > 10 digits then give message that NPI should be exactly 10 digits only. 
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

            '''''''''''''''''''''''''''Epcs'''''''''''''''''''

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
            If btnLicenseRefresh.Tag <> "valid" Then
                If ISDEMOLicensce = False And lblAusStatus.Text.Trim <> "4" Then
                    If Trim(txtLicenseKey.Text) <> "" Then
                        If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                            Me.Cursor = Cursors.WaitCursor
                            Dim ausmessage As String

                            ''Dim ausmessage As String = oProvider.ValidateLicenseKey(txtLicenseKey.Text.Trim, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, gstrClinicExternalCode, _nProviderID, _nAUSPortalID)
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
                                    Dim sServicelvl As String = ""
                                    Dim bisPDR As Boolean = False

                                    Dim bisPDMP As Boolean = False

                                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                                        sServicelvl = oLicense.GetLicensedServiceLevel(txtLicenseKey.Text.Trim)
                                        _nAUSPortalID = oLicense.GetAUSPortalID(txtLicenseKey.Text.Trim)
                                        bisPDR = oLicense.getLicensedPDRFlag(txtLicenseKey.Text)

                                        bisPDMP = oLicense.getLicensedPDMPFlag(txtLicenseKey.Text)
                                    End Using
                                    'If _nAUSPortalID <> 0 Then
                                    '    oProvider.AUSPortalID = _nAUSPortalID
                                    'End If
                                    SetLicenceServiceLevels(sServicelvl, bisPDR, bisPDMP)
                                    chckDisable.Enabled = False
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

            Dim IsEpcsEnable As Boolean = ChckEPCS.Checked
            If ChckEPCS.Checked And ChckEPCS.Enabled Then
                IsEpcsEnable = GetEpcsGoldPrescriberStatus()
                If IsEpcsEnable Then
                    ChckEPCS.Checked = True
                Else
                    ChckEPCS.Checked = False
                    chkPDMP.Checked = False
                    Exit Sub
                End If
            End If
            If flagControlSubstanceEnable <> IsEpcsEnable Then
                Dim objAudit1 As New clsAudit
                If IsEpcsEnable Then
                    ''BDO Audit
                    objAudit1.CreateLog(clsAudit.enmActivityType.Modify, "Controlled substance Service Level Enabled for " & txtFirstName.Text.Trim() & " " & txtLastName.Text.Trim(), gstrLoginName, gstrClientMachineName)
                Else
                    ''BDO Audit
                    objAudit1.CreateLog(clsAudit.enmActivityType.Modify, "Controlled substance Service Level Disabled for " & txtFirstName.Text.Trim() & " " & txtLastName.Text.Trim(), gstrLoginName, gstrClientMachineName)
                End If
                objAudit1 = Nothing
            End If
            ''''''''''''''''''''''''''Epcs''''''''''''''''''

            '''''''''''''''''''''''''''
            '''''''''''''''''''''''''''
            oProvider.ProviderID = _nProviderID
            oProvider.Prefix = txtPrefix.Text.Trim()
            oProvider.FirstName = txtFirstName.Text.Trim()
            oProvider.MiddleName = txtMiddleName.Text.Trim()
            oProvider.LastName = txtLastName.Text.Trim()
            oProvider.Suffix = txtSuffix.Text.Trim()
            oProvider.BMContactName = txtBMContact.Text.Trim()
            ''Dhruv
            'oProvider.BMAddress1 = txtBMAddress1.Text.Trim()
            'oProvider.BMAddress2 = txtBMAddress2.Text.Trim()
            'oProvider.BMState = cmbBMstate.Text.Trim()
            'oProvider.BMCity = txtBMCity.Text.Trim()
            'oProvider.BMZIP = txtBMZip.Text.Trim()
            oProvider.BMAddress1 = oBussinessAddressContol.txtAddress1.Text     ''add1
            oProvider.BMAddress2 = oBussinessAddressContol.txtAddress2.Text     ''add2
            oProvider.BMState = oBussinessAddressContol.cmbState.Text           ''state
            oProvider.BMCity = oBussinessAddressContol.txtCity.Text             ''city
            oProvider.BMZIP = oBussinessAddressContol.txtZip.Text               ''zip
            oProvider.BMCountry = oBussinessAddressContol.cmbCountry.Text       ''Country
            oProvider.BMCounty = oBussinessAddressContol.txtCounty.Text        ''County
            ''End------Dhruv


            ''Practice
            oProvider.BPracContactName = txtBPracContactName.Text.Trim()
            'oProvider.BPracAddress1 = txtBPracAddress1.Text.Trim()
            'oProvider.BPracAddress2 = txtBPracAddress2.Text.Trim()
            'oProvider.BPracState = cmbBPracState.Text.Trim()
            'oProvider.BPracCity = txtBPracCity.Text.Trim()
            'oProvider.BPracZIP = txtBPracZIP.Text.Trim()
            oProvider.BPracAddress1 = oPracticeAddressContol.txtAddress1.Text     ''add1
            oProvider.BPracAddress2 = oPracticeAddressContol.txtAddress2.Text     ''add2
            oProvider.BPracState = oPracticeAddressContol.cmbState.Text           ''state
            oProvider.BPracCity = oPracticeAddressContol.txtCity.Text             ''city
            oProvider.BPracZIP = oPracticeAddressContol.txtZip.Text               ''zip
            oProvider.BPracCountry = oPracticeAddressContol.cmbCountry.Text       ''Country
            oProvider.BPracCounty = oPracticeAddressContol.txtCounty.Text        ''County

            oProvider.BMPhone = mskBMPhoneNo.Text.Trim()
            oProvider.BMPager = txtBMPager.Text.Trim()
            oProvider.BMFAX = txtBMFAX.Text.Trim()
            oProvider.BMEmail = txtBMEmailAddress.Text.Trim()
            If mtxtDOB.MaskCompleted Then
                oProvider.dtDOB = Convert.ToDateTime(mtxtDOB.Text)
            End If

            If rbUpdate.Checked Then
                bIsServiceLevelDisabled = False
                If chckDisable.Checked Then
                    bIsServiceLevelDisabled = True
                ElseIf ChkCIEvent.Checked = False AndAlso chkCIMessage.Checked = False Then
                    bIsServiceLevelDisabled = True
                    'ElseIf chkCIMessage.Checked = True Then
                    '    bIsServiceLevelDisabled = False
                End If

                If lblDirectAddressValue.Text.Trim() = "" Then

                End If
            Else
                bIsServiceLevelDisabled = False
            End If

            If txtDirectAddress.Text.Trim() <> "" Then
                oProvider.DirectAddress = txtDirectAddress.Text.Trim() & lblDirectAddressValue.Text
            Else
                oProvider.DirectAddress = ""
            End If

            oProvider.BMURL = txtBMURL.Text.Trim()
            oProvider.BPracPhone = maskedBpracPhno.Text.Trim()
            oProvider.BPracPager = txtBPracPager.Text.Trim()
            oProvider.BPracFAX = txtBPracFax.Text.Trim()
            oProvider.BPracEmail = txtBPracEMail.Text.Trim()
            oProvider.BPracURL = txtBPracUrl.Text.Trim()


            ''Company
            oProvider.ComapanyName = txtCompanyName.Text
            oProvider.ComapanyContactName = txtCompanyContactName.Text

            'oProvider.ComapanyAddress1 = txtCompanyAddress1.Text
            'oProvider.ComapanyAddress2 = txtCompanyAddress2.Text
            'oProvider.ComapanyCity = txtCompanyCity.Text
            'oProvider.ComapanyState = ComboBox1.Text
            'oProvider.ComapanyZip = txtCompanyZip.Text

            oProvider.ComapanyAddress1 = oCompanyAddressContol.txtAddress1.Text     ''add1
            oProvider.ComapanyAddress2 = oCompanyAddressContol.txtAddress2.Text     ''add2
            oProvider.ComapanyCity = oCompanyAddressContol.txtCity.Text           ''state
            oProvider.ComapanyState = oCompanyAddressContol.cmbState.Text             ''city
            oProvider.ComapanyZip = oCompanyAddressContol.txtZip.Text               ''zip
            oProvider.ComapanyCountry = oCompanyAddressContol.cmbCountry.Text       ''Country
            oProvider.ComapanyCounty = oCompanyAddressContol.txtCounty.Text        ''County


            oProvider.ComapanyPhone = txtCompanyPhone.Text
            oProvider.ComapanyFax = txtCompanyFax.Text
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

            oProvider.DEA = txtDEA.Text.Trim()

            oProvider.NADEAN = txtNADEAN.Text.Trim()

            '''
            'If Provider Type Is Changed then Check Weather it have any Association
            Dim isAssociated As Boolean = False
            Dim strMsg As String = ""
            If pType <> Convert.ToInt64(cmbDoctorType.SelectedValue) Then
                If objNewProvider.CheckAssociation(_nProviderID) Then
                    isAssociated = True
                    If pType = 0 And Convert.ToInt64(cmbDoctorType.SelectedValue) = 1 Then
                        strMsg = "This provider is associated with at least one junior provider. If you modify this provider then his/her junior provider associations will also be removed automatically. Are you sure you want to Modify this provider?"
                    ElseIf pType = 1 And Convert.ToInt64(cmbDoctorType.SelectedValue) = 0 Then
                        strMsg = "This provider is associated with at least one senior provider. If you Modify this provider then his/her senior provider associations will also be removed automatically. Are you sure you want to Modify this provider?"
                    End If
                End If
            End If
            '''

            oProvider.ProviderTypeID = Convert.ToInt64(cmbDoctorType.SelectedValue)
            oProvider.Description = Convert.ToString(cmbDoctorType.Text)

            oProvider.NPI = txtNPI.Text.Trim()
            oProvider.UPIN = txtUPIN.Text.Trim()
            oProvider.StateMedicalNo = txtStateMedicalLicenseNo.Text.Trim()
            ''start :: External Code
            oProvider.ExternalCode = txtExternalCode.Text.Trim()
            ''End :: External Code

            oProvider.DPSNumber = txtDPSNumber.Text.Trim()
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

            Dim sFont() As String = Nothing
            'Resolving Bug No 72619::Provider registration : Application throws an exception while registering new Provider
            If txtFont.Text <> "" Then
                sFont = txtFont.Text.Split(",")
                If sFont.Length > 1 Then
                    oProvider.SignFont = sFont(0)
                    oProvider.SignFontSize = sFont(1)
                Else
                    oProvider.SignFont = "Arial"
                    oProvider.SignFontSize = 12
                End If
            Else
                oProvider.SignFont = "Arial"
                oProvider.SignFontSize = 12
            End If

            sFont = Nothing

            oProvider.MaxSignScale = udMaxScale.Value
            oProvider.MinSignScale = udMinScale.Value



            oProvider.ClinicID = _nClinicID

            'Provider Details
            If c1ProviderIdentification.Rows.Count >= 3 Then
                oProvider.ProviderDetails.MedicareID = Convert.ToString(c1ProviderIdentification.GetData(1, 1))
                oProvider.ProviderDetails.MedicaidID = Convert.ToString(c1ProviderIdentification.GetData(2, 1))

                Dim _Name As String = ""
                Dim _Value As String = ""
                For i As Integer = 3 To c1ProviderIdentification.Rows.Count - 1
                    _Name = Convert.ToString(c1ProviderIdentification.GetData(i, 0))
                    _Value = Convert.ToString(c1ProviderIdentification.GetData(i, 1))

                    If _Name.Trim() <> "" Then
                        Dim oOtherDetails As New ProviderDetails.structOtherIdentifications
                        oOtherDetails.Code = _Value.Trim()
                        oOtherDetails.Description = _Name.Trim()
                        oProvider.ProviderDetails.OtherIdentifications.Add(oOtherDetails)
                    End If
                Next
            End If

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

            oProvider.LicenceKey = txtLicenseKey.Text
            oProvider.AUSStatus = lblAusStatus.Text
            oProvider.AUSPortalID = _nAUSPortalID

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
            _sValue = txtUPIN.Text.Trim()
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
                ' oProvider.PrescriberLocation = True
                oProvider.PrescriberLocation = False
            End If

            oProvider.ActiveStartTime = dtpActiveStartTime.Value
            oProvider.ActiveEndTime = dtpActiveEndTime.Value
            oProvider.ServiceLevel = GetServiceLevelCode()

            Me.AfterServiceLevel = oProvider.ServiceLevel

            If ISDEMOLicensce = False And lblAusStatus.Text.Trim <> "4" Then
                If _nProviderID <> 0 Then
                    If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                        If oProvider.ServiceLevel <> TempserviceLevel Then
                            MessageBox.Show("Your TRIARQ Health subscription has been modified. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End If
                End If
            End If
            '''''''''''''''''''''''''''
            '''''''''''''''''''''''''''


            'With oProvider
            '    .FirstName = txtFirstName.Text
            '    .MiddleName = txtMiddleName.Text
            '    .LastName = txtLastName.Text
            '    .Address = txtAddress.Text
            '    .Street = txtStreet.Text
            '    .City = txtCity.Text
            '    .State = txtState.Text
            '    .ZIP = txtZIP.Text
            '    .Phone = txtPhoneNo.Text
            '    .Mobile = txtMobileNo.Text
            '    .FAX = txtFAX.Text
            '    .Email = txtEmailAddress.Text
            '    .URL = txtURL.Text
            '    .DEA = txtDEA.Text
            '    .Pager = txtPager.Text
            '    .NPI = txtNPI.Text
            '    .UPIN = txtUPIN.Text
            '    .StateMedicalNo = txtStateMedicalLicenseNo.Text

            '    'sarika 7th june 07
            '    .Prefix = txtPrefix.Text
            '    '------------------------------------
            '    If rbUpdate.Checked Then
            '        .SPI = lblSPI.Text
            '        oProvider.PrescriberLocation = False
            '    ElseIf rbPrescriberLocation.Checked Then
            '        .RootSPI = txtSPI.Text
            '        oProvider.PrescriberLocation = True
            '    End If



            '    .ActiveStartTime = dtpActiveStartTime.Value
            '    .ActiveEndTime = dtpActiveEndTime.Value

            '    .ServiceLevel = GetServiceLevelCode()

            '    If optMale.Checked = True Then
            '        .Gender = "Male"
            '    Else
            '        .Gender = "Female"
            '    End If

            '    Dim objProviderType As New clsProviderType(gstrConnectionString)
            '    objProviderType.SearchProviderType(cmbDoctorType.Text)
            '    .ProviderTypeID = objProviderType.ProviderTypeID()
            '    objProviderType = Nothing

            '    If blnLogoChanged = True Then
            '        If optBrowse.Checked = False Then
            '            picSignature.Image = Nothing
            '            If File.Exists(Application.StartupPath & "\DoctorSign.tif") = True Then
            '                picSignature.Image = Image.FromFile(Application.StartupPath & "\DoctorSign.tif")
            '                picSignature.SizeMode = PictureBoxSizeMode.CenterImage
            '            End If
            '        End If

            '        If IsNothing(picSignature.Image) = False Then
            '            .Signature = picSignature.Image
            '        Else
            '            .Signature = Nothing
            '        End If
            '    End If

            '    If blnModify = False Then
            '        .UserName = txtUserName.Text
            '        Dim objEncryption As New clsEncryption
            '        .Password = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
            '        .NickName = objEncryption.EncryptToBase64String(txtNickName.Text, constEncryptDecryptKey)
            '        objEncryption = Nothing
            '    End If
            'End With

            If rbNo.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.No
            ElseIf rbAlways.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.Always
            ElseIf rbUsePlanSetting.Checked.Equals(True) Then
                oProvider.PARequired = clsProvider.PriorAuthorizationRequired.UsePlanSetting
            End If


            gstrCategory = Trim(txtFirstName.Text) & Space(2) & Trim(txtMiddleName.Text) & Space(2) & Trim(txtLastName.Text)

            If isAssociated Then
                Dim dResult = MessageBox.Show(strMsg, "Associated Provider", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If dResult.ToString() = "No" Then
                    cmbDoctorType.Focus()
                    Exit Sub
                End If
            End If
            oProvider.RequireSupervisingProviderforeRx = chkRequire_Supervising_Provider_for_eRx.CheckState

            Dim ogloDBLayer As gloSureScriptDBLayer = New gloSureScriptDBLayer
            Dim dtClinicDetails As DataTable = ogloDBLayer.GetClinicDetails(_nClinicID, gstrConnectionString)
            If dtClinicDetails IsNot Nothing AndAlso dtClinicDetails.Rows.Count > 0 Then
                oProvider.AUSID = dtClinicDetails.Rows(0)("sClinicName")
                oProvider.ClinicName = dtClinicDetails.Rows(0)("sExternalcode")
            Else
                oProvider.AUSID = ""
                oProvider.ClinicName = ""
            End If

            Dim objSettings As New clsSettings
            If objSettings.GetSettings() = True Then
                oProvider.DatabaseName = objSettings.DBVersion
                oProvider.gloSuiteVersion = objSettings.AppVersion
                gloSureScriptInterface.IsLunhDigitCheckAlgorithmEnble = objSettings.IsLuhnDigitCheckAlgorithmEnable
            Else
                oProvider.DatabaseName = ""
                oProvider.gloSuiteVersion = ""
            End If

            oProvider.IsDirectMessageEnabled = ChkCIEvent.Checked
            oProvider.IsCIMessageEnabled = chkCIMessage.Checked

            oProvider.IsServiceLevelDisabled = chckDisable.Checked
            oProvider.IsNewRxEnabled = chckNew.Checked
            oProvider.IsRefillEnabled = chckRefill.Checked
            oProvider.IsRxChangeEnabled = ChckChange.Checked
            oProvider.IsRxCancelEnabled = ChckCancel.Checked
            oProvider.IsRxFillEnabled = ChckRxFill.Checked
            oProvider.IsControlledSubstance = ChckEPCS.Checked
            oProvider.IsePAEnabled = ChkePA.Checked
            oProvider.CreatedDateTime = Nothing
            oProvider.ModifiedDateTime = Nothing
            oProvider.IsPDREnabled = chkPDR.Checked
            oProvider.IsPDMPEnabled = chkPDMP.Checked
            If txtDirectAddress.Tag IsNot Nothing Then
                oProvider.IsDirectAddressExists = True
            End If

            If rbPrescriberLocation.Checked Then
                If ChkCIEvent.Checked Or chkCIMessage.Checked Then
                    Dim oPrescriber As New Prescriber
                    oPrescriber.ProviderID = oProvider.ProviderID
                    oPrescriber.PrescriberID = txtSPI.Text
                    'oPrescriber.ActiveStartDate = getUTCDate(oProvider.ActiveStartTime)
                    'oPrescriber.ActiveEndDate = getUTCDate(oProvider.ActiveStartTime)
                    oPrescriber.ActiveStartDate = oProvider.ActiveStartTime.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
                    oPrescriber.ActiveEndDate = oProvider.ActiveEndTime.ToString("yyyy-MM-ddTHH:mm:ss.FZ")
                    oPrescriber.DirectAddress = txtDirectAddress.Text + lblDirectAddressValue.Text

                    Dim oAddUpdatePrescriber As AddUpdatePrescriber = New AddUpdatePrescriber
                    Dim sMessage As String = String.Empty
                    sMessage = oAddUpdatePrescriber.GetPrescriber(oPrescriber)

                    If sMessage <> "True" Then

                        If sMessage.Contains("direct_address_exists") Then
                            MessageBox.Show("Provider with Direct Address already exists. Please choose different Direct Address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If ChkCIEvent.Checked Or chkCIMessage.Checked Then
                                lblDirectAddress.Visible = True
                                txtDirectAddress.Visible = True
                                txtDirectAddress.Enabled = True
                                lblDirectAddressValue.Visible = True
                                SetDirectAddresses()
                            End If
                        Else
                            MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        'MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Exit Sub

                    End If
                    'Dim ogloInterface As New gloSureScriptInterface
                    'ogloInterface.GenerateGetPrescriber(oPrescriber)
                End If

            End If
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
            Else
                bsendprovider = False
            End If
            

            _nProviderID = oProvider.SaveProvider(ISDEMOLicensce, nProviderUserID)
            If bsendprovider = True Then
                If lblAusStatus.Text.Trim <> "4" Then
                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                        oLicense.SendProviderForApproval(_nProviderID)
                    End Using
                End If
            End If


            Me.AfterServiceLevel = oProvider.ServiceLevel

            ''Sanjog 
            SaveSignature(_nProviderID)
            ''Sanjog
            'Madan-- Added on 20100416-- for Emdeon LabId
            If _nProviderID > 0 Then
                'If txtLabId.Text <> "" Then
                SaveEmdeonLabId(_nProviderID, txtLabId.Text.Trim())
                'End If

            End If


            'End Madan
            If _nProviderID > 0 Then
                'oProvider.ProviderID = CType(txtFirstName.Tag, Int32)
                If txtLicenseKey.Text.Trim <> "" Then
                    UpdateErrorLog("Doctor successfully Modified", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update)
                    If gblnIsSurescriptEnabled Then
                        If txtDirectAddress.Text.Trim() <> "" Then
                            oProvider.DirectAddress = txtDirectAddress.Text.Trim() & lblDirectAddressValue.Text
                        Else
                            oProvider.DirectAddress = ""
                        End If

                        'sarika for removing provider validations
                        strMessage = AddUpdatePrescriber(oProvider)
                        If strMessage.Trim <> "" Then
                            If strMessage.Trim.EndsWith(",") Then
                                strMessage = strMessage.Substring(0, Len(strMessage.Trim) - 1)
                            End If
                            If strMessage.Contains("Direct Address already exists") Then
                                MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            Else
                                If MessageBox.Show(strMessage & vbCrLf & "Do you want to insert the missing data?" & vbCrLf & "Click No to insert the Provider." & vbCrLf & "Click Yes to enter the missing data for Surescript.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                    Exit Sub
                                End If
                            End If

                        End If
                    End If

                    oProvider = Nothing
                    ' Me.DialogResult = DialogResult.OK
                    If blnIsSurescriptError = True Then
                        blnIsSurescriptError = False
                        Exit Sub
                    Else
                        Me.AuditServiceLevelChanges(BeforeServiceLevel, AfterServiceLevel)
                    End If
                End If
                Me.Close()
            Else
                MessageBox.Show("Unable to update Provider Details ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                UpdateErrorLog("Unable to update Provider Details ", mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Update, True)
                oProvider = Nothing
            End If

            If Me.UserRolesModified Then
                Dim ePADBLayer As New ePADatabaseLayer()
                Dim list As List(Of EPARole) = Me.listPreparer.Union(Me.listReviewer).Union(Me.listSubmitter).ToList()

                ePADBLayer.SaveRoles(_nProviderID, list)
                ePADBLayer = Nothing

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, "EPA Role(s) modified", gloAuditTrail.ActivityOutCome.Success)
            End If

            Me.DisposeEPA()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateErrorLog("Unable to Add/update Provider Details due to " & ex.ToString, mdlFunctions.enmErrorOccuredForm.Doctor, mdlFunctions.enmOperation.Others, True)
        Finally

        End Try
    End Sub
    Private Sub SetDemoLicenseMode(ByVal sNPI As String)
        Dim oProvider As New clsProvider(gstrConnectionString)
        Try
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
        Catch ex As Exception
            MessageBox.Show("SetDemoLicenseMode :" & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oProvider = Nothing
        End Try
    End Sub
    Public Function SetPrescriber(ByVal oProvider As clsProvider) As Prescriber
        Dim oPrescriber As New Prescriber

        Try

            oPrescriber.AUSID = Convert.ToString(oProvider.AUSID)
            oPrescriber.ClinicName = Convert.ToString(oProvider.ClinicName)

            oPrescriber.PrescriberName.FirstName = Convert.ToString(oProvider.FirstName)
            oPrescriber.PrescriberName.MiddleName = Convert.ToString(oProvider.MiddleName)
            oPrescriber.PrescriberName.LastName = Convert.ToString(oProvider.LastName)
            oPrescriber.Gender = Convert.ToString(oProvider.Gender)
            oPrescriber.DEA = Convert.ToString(oProvider.DEA)
            oPrescriber.NADEAN = Convert.ToString(oProvider.NADEAN)

            oPrescriber.PrescriberAddress.Address1 = Convert.ToString(oProvider.BMAddress1)
            oPrescriber.PrescriberAddress.Address2 = Convert.ToString(oProvider.BMAddress2)
            oPrescriber.PrescriberAddress.City = Convert.ToString(oProvider.BMCity)
            oPrescriber.PrescriberAddress.State = Convert.ToString(oProvider.BMState)
            oPrescriber.PrescriberAddress.Zip = Convert.ToString(oProvider.BMZIP)


            oPrescriber.PrescriberPhone.Phone = Convert.ToString(oProvider.BMPhone)
            oPrescriber.NPI = Convert.ToString(oProvider.NPI)
            oPrescriber.DirectAddress = Convert.ToString(oProvider.FirstName & oProvider.LastName & "@glostream.cert.direct-ci.com")
            oPrescriber.IsDirectMessageEnabled = Convert.ToString(oProvider.IsDirectMessageEnabled)
            oPrescriber.IsNewRxEnabled = Convert.ToString(oProvider.IsNewRxEnabled)
            oPrescriber.IsePAEnabled = Convert.ToString(oProvider.IsePAEnabled)

            oPrescriber.DatabaseName = Convert.ToString(oProvider.DatabaseName)
            oPrescriber.gloSuiteVersion = Convert.ToString(oProvider.gloSuiteVersion)


            'Need to add PrescriberID
            Return oPrescriber
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function getUTCDate(ByVal dtactivedate As DateTime) As String
        Dim oUTCTime As DateTime
        Dim localZone As TimeZone = TimeZone.CurrentTimeZone
        oUTCTime = localZone.ToUniversalTime(dtactivedate)

        Dim dtdate As DateTime = oUTCTime
        Dim strdate As String = Format(dtdate, "yyyy-MM-dd")
        Dim strtime As String = Format(dtdate, "hh:mm:ss")
        Dim strUTCFormat As String = dtdate.Year.ToString & "-" & "0" & dtdate.Month.ToString & "-" & dtdate.Day.ToString & "T" & dtdate.Hour.ToString & ":" & dtdate.Minute.ToString & ":" & dtdate.Second.ToString & ".0Z"
        strUTCFormat = strdate & "T" & strtime & ".0Z"
        'strUTCFormat = strdate & "T" & "00:00:00" & ":0Z"
        Return strUTCFormat
    End Function

    Private Sub objPrescriber_SurescriptError() Handles objPrescriber.SurescriptError
        blnIsSurescriptError = True
    End Sub

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
                cmbDoctorType.SelectedIndex = -1
            End If
        Catch ex As Exception
        Finally
            oProvider = Nothing
        End Try
    End Sub

    Private Sub LoadProvider(ByVal ProviderID As Int64)
        IsLoading = True
        DetachEvents()
        Dim oProvider As New clsProvider(mdlGeneral.GetConnectionString)
        oProvider.GetProvider(ProviderID)
        TempProvider = New gloAUSLibrary.Class.TempProviderdata
        Try

            grpUserDetails.Enabled = False

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
            ''Bussiness
            ''Comment by Dhruv 20100312
            'txtBMAddress1.Text = oProvider.BMAddress1
            'txtBMAddress2.Text = oProvider.BMAddress2
            'cmbBMstate.Text = oProvider.BMState
            'txtBMCity.Text = oProvider.BMCity
            'txtBMZip.Text = oProvider.BMZIP
            'txtBMCity.Text = oProvider.BMCity
            ''End------------------------Commenting
            oBussinessAddressContol.isFormLoading = True
            oBussinessAddressContol.txtAddress1.Text = oProvider.BMAddress1         ''Add1
            oBussinessAddressContol.txtAddress2.Text = oProvider.BMAddress2         ''Add2
            oBussinessAddressContol.txtCity.Text = oProvider.BMCity                 ''city
            oBussinessAddressContol.txtZip.Text = oProvider.BMZIP                   ''Zip
            oBussinessAddressContol.cmbCountry.Text = oProvider.BMCountry         ''Country
            oBussinessAddressContol.txtCounty.Text = oProvider.BMCounty           ''county
            oBussinessAddressContol.cmbState.Text = oProvider.BMState               ''state

            oBussinessAddressContol.isFormLoading = False

            txtBPracContactName.Text = oProvider.BPracContactName


            ''Practice
            ''Comment by Dhruv 20100312
            'txtBPracAddress1.Text = oProvider.BPracAddress1
            'txtBPracAddress2.Text = oProvider.BPracAddress2
            'cmbBPracState.Text = oProvider.BPracState
            'txtBPracCity.Text = oProvider.BPracCity
            'txtBPracZIP.Text = oProvider.BPracZIP
            'txtBPracCity.Text = oProvider.BPracCity
            ''End------------------------Commenting

            oPracticeAddressContol.isFormLoading = True
            oPracticeAddressContol.txtAddress1.Text = oProvider.BPracAddress1          ''Add1
            oPracticeAddressContol.txtAddress2.Text = oProvider.BPracAddress2         ''Add2
            oPracticeAddressContol.txtCity.Text = oProvider.BPracCity                  ''city
            oPracticeAddressContol.txtZip.Text = oProvider.BPracZIP                    ''Zip
            oPracticeAddressContol.cmbCountry.Text = oProvider.BPracCountry         ''Country
            oPracticeAddressContol.txtCounty.Text = oProvider.BPracCounty           ''county
            oPracticeAddressContol.cmbState.Text = oProvider.BPracState                ''state

            oPracticeAddressContol.isFormLoading = False

            mskMobileNo.Text = oProvider.Mobile
            pType = oProvider.ProviderTypeID
            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID
            cmbDoctorType.Text = oProvider.Description
            If oProvider.Taxonomy <> "" Then
                txtTaxonomy.Text = (oProvider.Taxonomy & "-") + oProvider.TaxonomyDesc
            End If
            mtxt_SSNno.Text = oProvider.SSNno
            txt_EmployerID.Text = oProvider.EmployerID

            txtDEA.Text = oProvider.DEA

            txtNADEAN.Text = oProvider.NADEAN

            If oProvider.Gender = "Male" Then
                optMale.Checked = True
                'if
            ElseIf oProvider.Gender = "Female" Then
                optFemale.Checked = True
            End If
            'else
            mskBMPhoneNo.Text = oProvider.BMPhone
            txtBMEmailAddress.Text = oProvider.BMEmail

            '' DIRECT ADDRESS
            Dim strDirectAddress As String = ""
            strDirectAddress = oProvider.DirectAddress

            If strDirectAddress <> "" Then
                pnlSPI.Visible = True
                Dim strArrDirectAddress As String() = strDirectAddress.Split("@")
                If strArrDirectAddress.Length = 2 Then
                    txtDirectAddress.Text = strArrDirectAddress(0)
                    txtDirectAddress.Tag = strArrDirectAddress
                    lblDirectAddressValue.Text = "@" + strArrDirectAddress(1)

                End If
            End If

            'lblDirectAddressValue.Text = oProvider.DirectAddress  ''Direct Address

            txtBMFAX.Text = oProvider.BMFAX
            txtBMPager.Text = oProvider.BMPager
            txtBMURL.Text = oProvider.BMURL

            maskedBpracPhno.Text = oProvider.BPracPhone
            txtBPracEMail.Text = oProvider.BPracEmail
            txtBPracFax.Text = oProvider.BPracFAX
            txtBPracPager.Text = oProvider.BPracPager
            txtBPracUrl.Text = oProvider.BPracURL

            txtCompanyName.Text = oProvider.ComapanyName
            txtCompanyContactName.Text = oProvider.ComapanyContactName

            'If oProvider.Signature IsNot Nothing Then
            '    If oProvider.SignWidth <> 0 Then
            '        ImgWidth = oProvider.SignWidth
            '    End If
            'End If



            ''Company 
            ''Comment by Dhruv 20100312
            'txtCompanyAddress1.Text = oProvider.ComapanyAddress1
            'txtCompanyAddress2.Text = oProvider.ComapanyAddress2
            'txtCompanyCity.Text = oProvider.ComapanyCity
            'ComboBox1.Text = oProvider.ComapanyState
            'txtCompanyZip.Text = oProvider.ComapanyZip
            ''End------------------------Commenting
            oCompanyAddressContol.isFormLoading = True
            oCompanyAddressContol.txtAddress1.Text = oProvider.ComapanyAddress1          ''Add1
            oCompanyAddressContol.txtAddress2.Text = oProvider.ComapanyAddress2          ''Add2
            oCompanyAddressContol.txtCity.Text = oProvider.ComapanyCity                   ''city
            oCompanyAddressContol.txtZip.Text = oProvider.ComapanyZip                     ''Zip
            oCompanyAddressContol.cmbCountry.Text = oProvider.ComapanyCountry            ''Country
            oCompanyAddressContol.txtCounty.Text = oProvider.ComapanyCounty             ''county
            oCompanyAddressContol.cmbState.Text = oProvider.ComapanyState                 ''state

            oCompanyAddressContol.isFormLoading = False



            txtCompanyPhone.Text = oProvider.ComapanyPhone
            txtCompanyFax.Text = oProvider.ComapanyFax
            txtCompanyEmail.Text = oProvider.ComapanyEmail

            'Added by Anil on 20090310
            txtCompanyNPI.Text = oProvider.ComapanyNPI
            txtCompanyTaxID.Text = oProvider.ComapanyTaxID
            txtCmpTaxonomyCode.Text = oProvider.CompanyTaxonomyCode

            txtUPIN.Text = oProvider.UPIN
            txtNPI.Text = oProvider.NPI
            txtStateMedicalLicenseNo.Text = oProvider.StateMedicalNo
            ''Start :: External Code
            txtExternalCode.Text = oProvider.ExternalCode
            ''End :: External Code
            txtDPSNumber.Text = oProvider.DPSNumber

            cmbDoctorType.SelectedValue = oProvider.ProviderTypeID
            'Madan-- Added on 20100416-- for Emdeon LabId
            If _nProviderID > 0 Then

                Dim _eLabId As String = String.Empty
                _eLabId = GetEmdeonLabId(_nProviderID)

                If _eLabId <> "" Then
                    'Set value read for a lab id against a provider.
                    txtLabId.Text = _eLabId
                End If

            End If
            'End Madan

            If oProvider.Signature IsNot Nothing Then
                picSignature.Image = oProvider.Signature
                ImgWidth = oProvider.Signature.Width
                'picSignature.SizeMode = PictureBoxSizeMode.CenterImage
                picSignature.Visible = True
            End If

            txtFont.Text = Convert.ToString(oProvider.SignFont) + "," + Convert.ToString(oProvider.SignFontSize)
            udMaxScale.Value = oProvider.MaxSignScale
            udMinScale.Value = oProvider.MinSignScale

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
            chkRequire_Supervising_Provider_for_eRx.Checked = oProvider.RequireSupervisingProviderforeRx
            chkPDR.Checked = oProvider.IsPDREnabled
            chkPDMP.Checked = oProvider.IsPDMPEnabled
            'c1ProviderIdentification.AllowAddNew = False
            'c1ProviderIdentification.SetData(1, 1, oProvider.ProviderDetails.MedicareID)
            'c1ProviderIdentification.SetData(2, 1, oProvider.ProviderDetails.MedicaidID)

            'Dim RowIndex As Int32 = 0
            'For i As Integer = 1 To oProvider.ProviderDetails.OtherIdentifications.Count
            '    c1ProviderIdentification.Rows.Add()
            '    RowIndex = c1ProviderIdentification.Rows.Count - 1
            '    c1ProviderIdentification.SetData(RowIndex, 0, CType(oProvider.ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Description)
            '    c1ProviderIdentification.SetData(RowIndex, 1, CType(oProvider.ProviderDetails.OtherIdentifications(i), ProviderDetails.structOtherIdentifications).Code)
            'Next

            ' c1ProviderIdentification.AllowAddNew = True

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


            'Dim strServiceLevel As String
            'If IsDBNull(oProvider.ServiceLevel) Or oProvider.ServiceLevel Is Nothing Or oProvider.ServiceLevel = "" Then
            '    strServiceLevel = ""
            'Else
            '    strServiceLevel = oProvider.ServiceLevel
            'End If
            'If strServiceLevel = "" Then
            '    chckDisable.Checked = True
            'ElseIf strServiceLevel = "0000000000000000" Then
            '    chckDisable.Checked = True
            'ElseIf strServiceLevel = "0000000000000001" Then
            '    chckDisable.Checked = False
            '    chckRefill.Checked = False
            '    chckNew.Checked = True
            'ElseIf strServiceLevel = "0000000000000010" Then
            '    chckDisable.Checked = False
            '    chckNew.Checked = False
            '    chckRefill.Checked = True
            'ElseIf strServiceLevel = "0000000000000011" Then
            '    chckDisable.Checked = False
            '    chckRefill.Checked = True
            '    chckNew.Checked = True
            'End If

            TempLicensekey = oProvider.LicenceKey
            txtLicenseKey.Text = oProvider.LicenceKey
            lblAusStatus.Text = oProvider.AUSStatus
            TempserviceLevel = oProvider.ServiceLevel
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
            TempProvider.ServiceLevel = oProvider.ServiceLevel
            TempProvider.licenseKey = oProvider.LicenceKey
            TempProvider.ISPDR = oProvider.IsPDREnabled

            SetServiceLevels(Convert.ToString(oProvider.ServiceLevel))

            SetDemoLicenseMode(txtNPI.Text.Trim)

            Me.BeforeServiceLevel = Convert.ToString(oProvider.ServiceLevel)

            flagControlSubstanceEnable = ChckEPCS.Checked

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
                rbPrescriber.Enabled = True

                rbUpdate.Enabled = False
                rbUpdate.Checked = False

                rbPrescriberLocation.Enabled = True
                rbPrescriberLocation.Checked = False

                pnlSPI.Visible = False
            Else
                rbPrescriber.Enabled = False
                ' lblPreDetails.Visible = False
                pnlSPI.Visible = True
                txtSPI.Visible = False
                lblRoot.Visible = False
                lblSPI.Text = oProvider.SPI
                ProviderSPI = oProvider.SPI
                txtSPI.Enabled = False
                rbPrescriberLocation.Enabled = False
                rbUpdate.Checked = True
            End If

            'If oProvider.ProviderID > 0 Then
            '    rbPrescriberLocation.Enabled = False
            '    rbUpdate.Enabled = True
            '    rbUpdate.Checked = True
            'Else
            '    rbPrescriber.Checked = True
            '    pnlSPI.Visible = False
            '    rbUpdate.Enabled = False
            '    rbPrescriberLocation.Enabled = True
            'End If

            txtDirectAddress.Enabled = True
            If rbUpdate.Checked = True Then
                If ChkCIEvent.Checked = True Or chkCIMessage.Checked = True Then
                    If txtDirectAddress.Text.Trim().Length > 0 Then
                        txtDirectAddress.Enabled = False
                    End If
                    pnlSPI.Visible = True
                Else
                    pnlSPI.Visible = False
                End If
            End If

            DirectAddress()

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            IsLoading = False
            AttachEvents()
        End Try
    End Sub
    Private Sub SetServiceLevels(ByVal sServiceleval As String)
        Dim i As Integer
        If (sServiceleval = "" Or sServiceleval = "0000000000000000") AndAlso chkPDR.Checked = False AndAlso chkPDMP.Checked = False Then
            chckDisable.Checked = True
        Else
            chckDisable.Checked = False

            For i = 1 To 16
                Select Case i
                    Case 1
                        If Mid(sServiceleval, i, 1) = 0 Then ChkCIEvent.Checked = False Else ChkCIEvent.Checked = True
                    Case 2
                        If Mid(sServiceleval, i, 1) = 0 Then chkCIMessage.Checked = False Else chkCIMessage.Checked = True
                    Case 3
                    Case 4
                    Case 5
                        If Mid(sServiceleval, i, 1) = 0 Then ChckEPCS.Checked = False Else ChckEPCS.Checked = True
                    Case 6
                    Case 7
                    Case 8
                    Case 9
                        If Mid(sServiceleval, i, 1) = 0 Then ChkePA.Checked = False Else ChkePA.Checked = True
                    Case 10
                    Case 11
                    Case 12
                        If Mid(sServiceleval, i, 1) = 0 Then ChckCancel.Checked = False Else ChckCancel.Checked = True
                    Case 13
                        If Mid(sServiceleval, i, 1) = 0 Then ChckRxFill.Checked = False Else ChckRxFill.Checked = True
                    Case 14
                        If Mid(sServiceleval, i, 1) = 0 Then ChckChange.Checked = False Else ChckChange.Checked = True
                    Case 15
                        If Mid(sServiceleval, i, 1) = 0 Then chckRefill.Checked = False Else chckRefill.Checked = True
                    Case 16
                        If Mid(sServiceleval, i, 1) = 0 Then chckNew.Checked = False Else chckNew.Checked = True
                End Select

            Next
        End If
    End Sub
    Private Sub SetLicenceServiceLevels(ByVal sServiceleval As String, ByVal bPDR As Boolean, ByVal bPDMP As Boolean)
        If ISDEMOLicensce = False Then
            Dim i As Integer
            If (sServiceleval = "" Or sServiceleval = "0000000000000000") AndAlso chkPDR.Checked = False AndAlso chkPDMP.Checked = False Then
                chckDisable.Checked = True
                chckDisable.Enabled = False
            Else
                chckDisable.Checked = False

                For i = 1 To 16
                    Select Case i
                        Case 1
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChkCIEvent.Checked = False
                                ChkCIEvent.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    ChkCIEvent.Checked = True
                                End If
                                ' ChkCIEvent.Enabled = False
                            End If
                        Case 2
                            'If Mid(sServiceleval, i, 1) = 0 Then chkCIMessage.Checked = False Else chkCIMessage.Checked = True
                            If Mid(sServiceleval, i, 1) = 0 Then
                                chkCIMessage.Checked = False
                                chkCIMessage.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    chkCIMessage.Checked = True
                                    chkCIMessage.Enabled = True
                                End If
                                'chkCIMessage.Enabled = False
                            End If
                        Case 3
                        Case 4
                        Case 5
                            ' If Mid(sServiceleval, i, 1) = 0 Then ChckEPCS.Checked = False Else ChckEPCS.Checked = True
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChckEPCS.Checked = False
                                ChckEPCS.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    If ChckEPCS.Enabled = True Then
                                        ChckEPCS.Checked = True
                                        ChckEPCS.Enabled = True
                                    End If
                                End If
                                ' ChckEPCS.Enabled = False
                            End If
                        Case 6
                        Case 7
                        Case 8
                        Case 9
                            'If Mid(sServiceleval, i, 1) = 0 Then ChkePA.Checked = False Else ChkePA.Checked = True
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChkePA.Checked = False
                                ChkePA.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    ChkePA.Checked = True
                                    ChkePA.Enabled = True
                                End If
                                ' ChkePA.Enabled = False
                            End If
                        Case 10
                        Case 11
                        Case 12
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChckCancel.Checked = False
                                ChckCancel.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    ChckCancel.Checked = True
                                    ChckCancel.Enabled = True
                                End If
                            End If
                        Case 13
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChckRxFill.Checked = False
                                ChckRxFill.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    ChckRxFill.Checked = True
                                    ChckRxFill.Enabled = True
                                End If
                            End If
                        Case 14
                            If Mid(sServiceleval, i, 1) = 0 Then
                                ChckChange.Checked = False
                                ChckChange.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    ChckChange.Checked = True
                                    ChckChange.Enabled = True
                                End If
                            End If
                        Case 15
                            ' If Mid(sServiceleval, i, 1) = 0 Then chckRefill.Checked = False Else chckRefill.Checked = True
                            If Mid(sServiceleval, i, 1) = 0 Then
                                chckRefill.Checked = False
                                chckRefill.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    chckRefill.Checked = True
                                    chckRefill.Enabled = True
                                End If
                                ' chckRefill.Enabled = False
                            End If
                        Case 16
                            'If Mid(sServiceleval, i, 1) = 0 Then chckNew.Checked = False Else chckNew.Checked = True
                            If Mid(sServiceleval, i, 1) = 0 Then
                                chckNew.Checked = False
                                chckNew.Enabled = False
                            Else
                                If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                                    chckNew.Checked = True
                                    chckNew.Enabled = True
                                End If
                                'chckNew.Enabled = False
                            End If
                    End Select

                Next
                '''' -------- Set PDR ---- 
                'If bPDR = True Then chkPDR.Enabled = True Else chkPDR.Enabled = False
                'chkPDR.Checked = bPDR

                If bPDR = False Then
                    chkPDR.Checked = False
                    chkPDR.Enabled = False                    
                Else
                    If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                        chkPDR.Checked = True
                        chkPDR.Enabled = True                     
                    End If
                End If

                '''' -------- Set PDMP ----
                If bPDMP = False Then
                    chkPDMP.Checked = False
                    chkPDMP.Enabled = False
                Else
                    If TempLicensekey.Trim <> txtLicenseKey.Text.Trim Then
                        chkPDMP.Checked = True
                        chkPDMP.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub



    'Private Sub DesignGrid()
    '    c1ProviderIdentification.Rows.Fixed = 1
    '    c1ProviderIdentification.Cols.Fixed = 0
    '    c1ProviderIdentification.Rows.Count = 4
    '    c1ProviderIdentification.Cols.Count = 2

    '    c1ProviderIdentification.SetData(0, 0, "Description")
    '    c1ProviderIdentification.SetData(0, 1, "Value")


    '    c1ProviderIdentification.SetData(1, 0, "Medicare ID")
    '    c1ProviderIdentification.SetData(1, 1, "")

    '    c1ProviderIdentification.SetData(2, 0, "Medicaid ID")
    '    c1ProviderIdentification.SetData(2, 1, "")

    '    Dim _width As Int32 = gbProviderIdentification.Width

    '    c1ProviderIdentification.Cols(0).Width = Convert.ToInt32(_width * 0.5)
    '    c1ProviderIdentification.Cols(1).Width = Convert.ToInt32(_width * 0.2)

    '    c1ProviderIdentification.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
    '    c1ProviderIdentification.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

    '    c1ProviderIdentification.Update()
    'End Sub
    Private Sub DesignGrid()
        'Design Additional Billing IDs Grid 
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
        Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloEMRAdmin.mdlGeneral.GetConnectionString)
        dtQualifierID = ogloSettings.getIDQualifiers(3, _nProviderID, True)

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

        Dim _width As Int32 = Panel8.Width
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
        Dim ogloSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloEMRAdmin.mdlGeneral.GetConnectionString)
        dtQualifierID = ogloSettings.getIDQualifiers(4, _nProviderID, True)

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


    Private Sub c1ProviderIdentification_StartEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        c1ProviderIdentification.Editor = CType(c1ProviderIdentification.Editor, TextBox)
    End Sub

    Private Sub c1ProviderIdentification_SetupEditor(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
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

    Private Sub c1ProviderIdentification_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        ''gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, DirectCast(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
    End Sub

    Private Sub c1ProviderIdentification_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        If (e.Col = 0 AndAlso e.Row = 1) OrElse (e.Col = 0 AndAlso e.Row = 2) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub chkAddressasAbove_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAddressasAbove.CheckedChanged
        If chkAddressasAbove.Checked = True Then
            ''commented By Dhruv
            'txtBPracAddress1.Text = txtBMAddress1.Text.Trim()
            'txtBPracAddress2.Text = txtBMAddress2.Text.Trim()
            'cmbBPracState.Text = cmbBMstate.Text.Trim()
            'txtBPracCity.Text = txtBMCity.Text.Trim()
            '_isTextBoxLoading = True
            'txtBPracZIP.Text = txtBMZip.Text.Trim()
            '_isTextBoxLoading = False
            'maskedBpracPhno.Text = mskBMPhoneNo.Text
            'txtBPracFax.Text = txtBMFAX.Text.Trim()
            'txtBPracPager.Text = txtBMPager.Text.Trim()
            'txtBPracEMail.Text = txtBMEmailAddress.Text.Trim()
            'txtBPracUrl.Text = txtBMURL.Text.Trim()

            ''Dhruv - > It work the same as above check box 
            ''If the value is assigned then it will assigned it to the PracticeAddress 
            txtBPracContactName.Text = txtBMContact.Text
            oPracticeAddressContol.isFormLoading = True         ''maily used for the zip code panel visible true/false
            oPracticeAddressContol.txtAddress1.Text = oBussinessAddressContol.txtAddress1.Text        ''Add1
            oPracticeAddressContol.txtAddress2.Text = oBussinessAddressContol.txtAddress2.Text           ''Add2
            oPracticeAddressContol.txtCity.Text = oBussinessAddressContol.txtCity.Text                  ''Zip
            oPracticeAddressContol.cmbCountry.Text = oBussinessAddressContol.cmbCountry.Text          ''Country
            oPracticeAddressContol.txtCounty.Text = oBussinessAddressContol.txtCounty.Text           ''county
            oPracticeAddressContol.txtZip.Text = oBussinessAddressContol.txtZip.Text
            oPracticeAddressContol.cmbState.Text = oBussinessAddressContol.cmbState.Text               ''state

            oPracticeAddressContol.isFormLoading = False         ''maily used for the zip code panel visible true/false
            txtBPracPager.Text = txtBMPager.Text
            txtBPracFax.Text = txtBMFAX.Text
            maskedBpracPhno.Text = mskBMPhoneNo.Text
            txtBPracEMail.Text = txtBMEmailAddress.Text
            txtBPracUrl.Text = txtBMURL.Text

            ''txtBPracContactName.Text = oProvider.BPracContactName
            ''Dhruv -> Checked for the UnChecked operation
        ElseIf (chkAddressasAbove.Checked = False) Then
            txtBPracContactName.Text = ""
            oPracticeAddressContol.isFormLoading = True ''''maily used for the zip code panel visible true/false
            oPracticeAddressContol.txtAddress1.Text = ""
            oPracticeAddressContol.txtAddress2.Text = ""
            oPracticeAddressContol.txtCity.Text = ""
            oPracticeAddressContol.cmbState.Text = ""
            oPracticeAddressContol.cmbCountry.Text = ""
            oPracticeAddressContol.txtCounty.Text = ""
            oPracticeAddressContol.txtZip.Text = ""
            oPracticeAddressContol.isFormLoading = False   ''maily used for the zip code panel visible true/false
            txtBPracPager.Text = ""
            txtBPracFax.Text = ""
            maskedBpracPhno.Text = ""
            txtBPracEMail.Text = ""
            txtBPracUrl.Text = ""


        End If
    End Sub

    'Private Sub chkCompanyAsAbove_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCompanyAsAbove.CheckedChanged
    '    If chkCompanyAsAbove.Checked = True Then
    '        txtCompanyAddress1.Text = txtBMAddress1.Text.Trim()
    '        txtCompanyAddress2.Text = txtBMAddress2.Text.Trim()
    '        ComboBox1.Text = cmbBMstate.Text.Trim()
    '        txtCompanyCity.Text = txtBMCity.Text.Trim()
    '        _isTextBoxLoading = True
    '        txtCompanyZip.Text = txtBMZip.Text.Trim()
    '        _isTextBoxLoading = False
    '        txtCompanyPhone.Text = mskBMPhoneNo.Text
    '        txtCompanyFax.Text = txtBMFAX.Text.Trim()
    '        txtCompanyEmail.Text = txtBMEmailAddress.Text.Trim()
    '    End If
    'End Sub

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

    Private Sub txtBMZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBMZip.GotFocus
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Zip Code Get Store Temperorly in _TempZipText variable
        Try
            _ZipTextType = enumZipTextType.BusinessZip
            _TempZipText = txtBMZip.Text.Trim()
        Catch
        End Try
        'End Code By Dipak
    End Sub

    Private Sub txtBMZip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBMZip.KeyDown
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Code added for detects '' HITS UP / DOWN '' and Set Focus to ZipControls Starting Row
        Try
            _ZipTextType = enumZipTextType.BusinessZip
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                '' HITS UP / DOWN ''
                If pnlInternalControl.Visible Then
                    e.SuppressKeyPress = True
                    e.Handled = True
                    oZipcontrol.C1GridList.Focus()
                    oZipcontrol.C1GridList.[Select](oZipcontrol.C1GridList.RowSel, 0)
                End If
            End If
        Catch
        End Try
    End Sub
    'Code comented by dipak 20090912 : Code No Longer use as ZipControl is implemented 
    'Previously code Numeric_KeyPress is Handles KeyPress Event of 
    'Private Sub Numeric_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtBMFAX.KeyPress, txtBMZip.KeyPress, txtBMPager.KeyPress, txtCompanyFax.KeyPress, txtCompanyZip.KeyPress, txtBPracFax.KeyPress, txtBPracZIP.KeyPress, txtBPracPager.KeyPress
    '    'code to allow nos only 
    '    If Not ((e.KeyChar >= Convert.ToChar(48) AndAlso e.KeyChar <= Convert.ToChar(57)) OrElse e.KeyChar = Convert.ToChar(8)) Then
    '        e.Handled = True
    '    End If
    'End Sub

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
#Region "Comman Methods For Zip Control Implementation"
    Private Sub oZipcontrol_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak  20090911 for show ZipControl
        'code get a selected zip info  and store in variables
        Try
            If oZipcontrol.C1GridList.Row < 0 Then
                Exit Sub
            End If
            Dim _Zip As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 0).ToString()
            Dim _City As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 1).ToString()
            Dim _ID As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 2).ToString()
            Dim _County As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 3).ToString()
            Dim _State As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 4).ToString()
            Dim _AreaCode As String = oZipcontrol.C1GridList.GetData(oZipcontrol.C1GridList.Row, 5).ToString()
            _isTextBoxLoading = True

            Select Case _ZipTextType
                'code assign a selected zip info to controls
                Case enumZipTextType.BusinessZip
                    txtBMZip.Text = _Zip
                    txtBMZip.Tag = _ID
                    txtBMCity.Text = _City
                    txtBMCity.Tag = _AreaCode
                    cmbBMstate.Text = _State
                    If pnlInternalControl.Visible = True Then
                        pnlInternalControl.Visible = False
                        txtBMCity.Focus()

                    End If
                    Exit Select
                Case enumZipTextType.BusinessPracticeZip

                    txtBPracZIP.Text = _Zip
                    txtBPracZIP.Tag = _ID
                    txtBPracCity.Text = _City
                    txtBPracCity.Tag = _AreaCode
                    cmbBPracState.Text = _State
                    If pnlBPractInternalControl.Visible = True Then
                        pnlBPractInternalControl.Visible = False
                        txtBPracCity.Focus()
                    End If
                    Exit Select
                Case enumZipTextType.CompanyZip

                    txtCompanyZip.Text = _Zip
                    txtCompanyZip.Tag = _ID
                    txtCompanyCity.Text = _City
                    txtCompanyCity.Tag = _AreaCode
                    ComboBox1.Text = _State
                    If pnlCompanyInternalControl.Visible = True Then
                        pnlCompanyInternalControl.Visible = False
                        txtCompanyCity.Focus()
                    End If

                    Exit Select
            End Select
            ' End Select

            _isTextBoxLoading = False
            _isZipItemSelected = True
            isSearchControlOpen = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub oZipcontrol_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak  20090911 for show ZipControl
        Try
            CloseInternalControl()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
        End Try
    End Sub
    'COMMENTED BY SHUBHANGI 20100906
    'Public Function OpenInternalControl(ByVal ControlType As gloPatient.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
    'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
    Public Function OpenInternalControl(ByVal ControlType As gloAddress.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
        'code added by dipak  20090911 for show ZipControl
        'code add control in display zip control here as parameter passed
        Dim _result As Boolean = False
        _isZipItemSelected = False
        Try

            If oZipcontrol IsNot Nothing Then
                CloseInternalControl()
            End If
            'COMMENTED BY SHUBHANGI 20100906
            'oZipcontrol = New gloPatient.gloZipcontrol(ControlType, False, 0, 0, 0, gloEMRAdmin.mdlGeneral.GetConnectionString())
            'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
            oZipcontrol = New gloAddress.gloZipcontrol(ControlType, False, 0, 0, 0, gloEMRAdmin.mdlGeneral.GetConnectionString())
            AddHandler oZipcontrol.ItemSelectedclick, AddressOf oZipcontrol_ItemSelected
            AddHandler oZipcontrol.InternalGridKeyDownclick, AddressOf oZipcontrol_InternalGridKeyDown
            oZipcontrol.ControlHeader = ControlHeader
            oZipcontrol.ShowHeader = False
            oZipcontrol.Dock = DockStyle.Fill
            Select Case _ZipTextType
                Case enumZipTextType.BusinessZip
                    pnlInternalControl.BringToFront()
                    pnlInternalControl.Visible = True
                    pnlInternalControl.Controls.Add(oZipcontrol)
                    Exit Select
                Case enumZipTextType.BusinessPracticeZip
                    pnlBPractInternalControl.BringToFront()
                    pnlBPractInternalControl.Visible = True
                    pnlBPractInternalControl.Controls.Add(oZipcontrol)
                    Exit Select
                Case enumZipTextType.CompanyZip
                    pnlCompanyInternalControl.BringToFront()
                    pnlCompanyInternalControl.Visible = True
                    pnlCompanyInternalControl.Controls.Add(oZipcontrol)
            End Select

            If Not String.IsNullOrEmpty(SearchText) Then
                'COMMENTED BY SHUBHANGI 20100906
                'oZipcontrol.Search(SearchText, gloPatient.SearchColumn.Code)
                'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
                oZipcontrol.Search(SearchText, gloAddress.SearchColumn.Code)
            End If
            oZipcontrol.Show()
            _result = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            _result = False
        Finally

        End Try

        isSearchControlOpen = True
        Return _result
    End Function
    Private Function CloseInternalControl() As Boolean
        'code added by dipak  20090911 for show ZipControl
        ' to remove controls from panel
        If oZipcontrol IsNot Nothing Then

            _isTextBoxLoading = True
            For i As Integer = 0 To pnlInternalControl.Controls.Count - 1
                pnlInternalControl.Controls.RemoveAt(i)
            Next
            If oZipcontrol IsNot Nothing Then
                oZipcontrol.Dispose()
                oZipcontrol = Nothing
            End If
            _isTextBoxLoading = False
        End If
        Return _isTextBoxLoading
    End Function
#End Region

    Private Sub txtBMZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMZip.KeyPress
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip
        Try
            '_isTextBoxLoading is set false to track text change event occure due to keypress
            _isTextBoxLoading = False
            _ZipTextType = enumZipTextType.BusinessZip
            If e.KeyChar = Convert.ToChar(13) Then
                '' HITS ENTER BUTTON ''
                If pnlInternalControl.Visible Then

                    oZipcontrol_ItemSelected(Nothing, Nothing)
                End If
            ElseIf e.KeyChar = Convert.ToChar(27) Then
                '' HITS ESCAPE ''
                If txtBMZip.Text = "" AndAlso txtBMCity.Text = "" AndAlso cmbBMstate.Text = "" Then
                    _TempZipText = txtBMZip.Text
                End If
                txtBMCity.Focus()
            End If

            ''we are allowing only alphanumeric charactors for according referring the information from the link below  
            '' http://www.postalcodedownload.com/
            'The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
            ''an alphabetic character and "N" represents a numeric character. 
            If Not e.KeyChar = Convert.ToChar(8) Then
                If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-zA-Z]*$") = False Then
                    e.Handled = True
                End If
            End If
        Catch

        End Try
    End Sub

    Private Sub txtBMZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBMZip.LostFocus
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'code added for make Zip Control Invisible while los focus
        _ZipTextType = enumZipTextType.BusinessZip
        If oZipcontrol IsNot Nothing Then
            If _isZipItemSelected = False And oZipcontrol.C1GridList.Focused = False And oZipcontrol.Focused = False Then
                _isTextBoxLoading = True
                If txtBMCity.Text = "" AndAlso txtBMZip.Text = "" Then
                    _TempZipText = txtBMZip.Text
                End If
                pnlInternalControl.Visible = False
                _isTextBoxLoading = False
            End If
        End If
    End Sub

    Private Sub txtBMZip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBMZip.TextChanged
        'code added by dipak  20090912 for show ZipControl
        Dim pt As Point
        Try
            _ZipTextType = enumZipTextType.BusinessZip
            pnlInternalControl.BringToFront()
            'code added for show ZipControl except Form Loading And TextLoading(mean zipControl Apears only when user try enter zip using keyboard
            If isFormLoading = False And _isTextBoxLoading = False Then
                If pnlInternalControl.Visible = False Then

                    ' pnlInternalControl.Top = txtBMZip.Top + 28
                    'pnlInternalControl.Left = txtBMZip.Top
                    pnlInternalControl.Visible = True
                    'COMMENTED BY SHUBHANGI 20100906
                    'OpenInternalControl(gloPatient.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
                    OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    oZipcontrol.FillControl(Convert.ToString(txtBMZip.Text.Trim()))
                Else
                    oZipcontrol.FillControl(Convert.ToString(txtBMZip.Text.Trim()))
                End If
            End If
            '_isTextBoxLoading is set true to track text change event occure due to Assignment or by Keypress
            _isTextBoxLoading = True
        Catch
        Finally
        End Try
        'end code by dipak 20090912
    End Sub
#Region "Business Practice txt zip event region"

    Private Sub txtBPracZIP_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPracZIP.GotFocus
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Zip Code Get Store Temperorly in _TempZipText variable
        Try
            _ZipTextType = enumZipTextType.BusinessPracticeZip
            _TempZipText = txtBPracZIP.Text.Trim()
        Catch
        End Try
        'End Code By Dipak
    End Sub

    Private Sub txtBPracZIP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBPracZIP.KeyDown
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Code added for detects '' HITS UP / DOWN '' and Set Focus to ZipControls Starting Row
        Try
            _ZipTextType = enumZipTextType.BusinessPracticeZip
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                '' HITS UP / DOWN ''
                If pnlBPractInternalControl.Visible Then
                    e.SuppressKeyPress = True
                    e.Handled = True
                    oZipcontrol.C1GridList.Focus()
                    oZipcontrol.C1GridList.[Select](oZipcontrol.C1GridList.RowSel, 0)
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub txtBPracZIP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPracZIP.KeyPress
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip
        Try
            '_isTextBoxLoading is set false to track text change event occure due to keypress
            _isTextBoxLoading = False
            _ZipTextType = enumZipTextType.BusinessPracticeZip
            If e.KeyChar = Convert.ToChar(13) Then
                '' HITS ENTER BUTTON ''
                If pnlBPractInternalControl.Visible Then

                    oZipcontrol_ItemSelected(Nothing, Nothing)
                End If
            ElseIf e.KeyChar = Convert.ToChar(27) Then
                '' HITS ESCAPE ''
                If txtBPracZIP.Text = "" AndAlso txtBPracCity.Text = "" AndAlso cmbBPracState.Text = "" Then
                    _TempZipText = txtBPracZIP.Text
                End If
                txtBPracCity.Focus()
            End If
            ''we are allowing only alphanumeric charactors for according referring the information from the link below  
            '' http://www.postalcodedownload.com/
            'The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
            ''an alphabetic character and "N" represents a numeric character. 
            If Not e.KeyChar = Convert.ToChar(8) Then
                If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-zA-Z]*$") = False Then
                    e.Handled = True
                End If
            End If
        Catch

        End Try
    End Sub

    Private Sub txtBPracZIP_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPracZIP.LostFocus
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'code added for make Zip Control Invisible while los focus
        _ZipTextType = enumZipTextType.BusinessPracticeZip
        If oZipcontrol IsNot Nothing Then
            If _isZipItemSelected = False And oZipcontrol.C1GridList.Focused = False And oZipcontrol.Focused = False Then
                _isTextBoxLoading = True
                If txtBPracCity.Text = "" AndAlso txtBPracZIP.Text = "" Then
                    _TempZipText = txtBPracZIP.Text
                End If
                pnlBPractInternalControl.Visible = False
                _isTextBoxLoading = False
            End If
        End If
    End Sub

    Private Sub txtBPracZIP_MarginChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPracZIP.MarginChanged

    End Sub

    Private Sub txtBPracZIP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPracZIP.TextChanged
        'code added by dipak  20090912 for show ZipControl
        Dim pt As Point
        Try
            _ZipTextType = enumZipTextType.BusinessPracticeZip
            pnlBPractInternalControl.BringToFront()
            'code added for show ZipControl except Form Loading And TextLoading(mean zipControl Apears only when user try enter zip using keyboard
            If isFormLoading = False And _isTextBoxLoading = False Then
                If pnlBPractInternalControl.Visible = False Then

                    ' pnlBPractInternalControl.Top = txtBMZip.Top + 28
                    ' pnlBPractInternalControl.Left = txtBMZip.Top
                    pnlBPractInternalControl.Visible = True
                    'COMMENTED BY SHUBHANGI 20100906
                    'OpenInternalControl(gloPatient.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
                    OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    oZipcontrol.FillControl(Convert.ToString(txtBPracZIP.Text.Trim()))
                Else
                    oZipcontrol.FillControl(Convert.ToString(txtBPracZIP.Text.Trim()))
                End If
            End If
            '_isTextBoxLoading is set true to track text change event occure due to Assignment or by Keypress
            '_isTextBoxLoading = True
        Catch
        Finally
        End Try
        'end code by dipak 20090912
    End Sub

#End Region

#Region "Business Company  txtzip event region"

    Private Sub txtCompanyZip_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Zip Code Get Store Temperorly in _TempZipText variable
        Try
            _ZipTextType = enumZipTextType.CompanyZip
            _TempZipText = txtCompanyZip.Text.Trim()
        Catch
        End Try
        'End Code By Dipak
    End Sub

    Private Sub txtCompanyZip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'Code added for detects '' HITS UP / DOWN '' and Set Focus to ZipControls Starting Row
        Try
            _ZipTextType = enumZipTextType.CompanyZip
            If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
                '' HITS UP / DOWN ''
                If pnlCompanyInternalControl.Visible Then
                    e.SuppressKeyPress = True
                    e.Handled = True
                    oZipcontrol.C1GridList.Focus()
                    oZipcontrol.C1GridList.[Select](oZipcontrol.C1GridList.RowSel, 0)
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub txtCompanyZip_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        ' code Added By Dipak 20090911 for Show Zip Control on text Change of txtZip
        Try
            '_isTextBoxLoading is set false to track text change event occure due to keypress
            _isTextBoxLoading = False
            _ZipTextType = enumZipTextType.CompanyZip
            If e.KeyChar = Convert.ToChar(13) Then
                '' HITS ENTER BUTTON ''
                If pnlCompanyInternalControl.Visible Then

                    oZipcontrol_ItemSelected(Nothing, Nothing)
                End If
            ElseIf e.KeyChar = Convert.ToChar(27) Then
                '' HITS ESCAPE ''
                If txtCompanyZip.Text = "" AndAlso txtCompanyCity.Text = "" AndAlso ComboBox1.Text = "" Then
                    _TempZipText = txtCompanyZip.Text
                End If
                txtCompanyCity.Focus()
            End If
            ''we are allowing only alphanumeric charactors for according referring the information from the link below  
            '' http://www.postalcodedownload.com/
            'The Canadian postal code is a six-character alpha-numeric code in the format "ANA NAN", where "A" represents
            ''an alphabetic character and "N" represents a numeric character. 
            If Not e.KeyChar = Convert.ToChar(8) Then
                If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9a-zA-Z]*$") = False Then
                    e.Handled = True
                End If
            End If

        Catch

        End Try
    End Sub

    Private Sub txtCompanyZip_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        ' code Added By Dipak 20090912 for Show Zip Control on text Change of txtZip
        'code added for make Zip Control Invisible while los focus
        _ZipTextType = enumZipTextType.CompanyZip
        If oZipcontrol IsNot Nothing Then
            If _isZipItemSelected = False And oZipcontrol.C1GridList.Focused = False And oZipcontrol.Focused = False Then
                _isTextBoxLoading = True
                If txtCompanyCity.Text = "" AndAlso txtCompanyZip.Text = "" Then
                    _TempZipText = txtCompanyZip.Text
                End If
                pnlCompanyInternalControl.Visible = False
                _isTextBoxLoading = False
            End If
        End If
    End Sub

    Private Sub txtCompanyZip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'code added by dipak  20090912 for show ZipControl
        Dim pt As Point
        Try
            _ZipTextType = enumZipTextType.CompanyZip
            pnlCompanyInternalControl.BringToFront()
            'code added for show ZipControl except Form Loading And TextLoading(mean zipControl Apears only when user try enter zip using keyboard
            If isFormLoading = False And _isTextBoxLoading = False Then
                If pnlCompanyInternalControl.Visible = False Then
                    pnlCompanyInternalControl.Visible = True
                    'COMMENTED BY SHUBHANGI 20100906
                    'OpenInternalControl(gloPatient.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    'SHUBHANGI 20100906 WE ARE USING gloZipcontrol FROM gloAddress INSTEAD OF gloPatient
                    OpenInternalControl(gloAddress.gloGridListControlType.ZIP, "Zip", False, 0, 0, "")
                    oZipcontrol.FillControl(Convert.ToString(txtCompanyZip.Text.Trim()))
                Else
                    oZipcontrol.FillControl(Convert.ToString(txtCompanyZip.Text.Trim()))
                End If
            End If
            '_isTextBoxLoading is set true to track text change event occure due to Assignment or by Keypress
            _isTextBoxLoading = True
        Catch
        Finally
        End Try
        'end code by dipak 20090912
    End Sub

#End Region

    Private Sub btnCapture_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseHover, btnCapture.MouseHover

    End Sub

    Private Sub btnClear_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.MouseLeave

    End Sub

    Private Sub pnlBussinessAddresssControl_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlBussinessAddresssControl.Paint

    End Sub
    'Madan--Added for retriveing Emdeon LabId -- on 20100416
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
    'Madan--Added for retriveing Emdeon LabId -- on 20100416
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
    'End Madan
    'Commented by Rahul Patel 24-09-2010     
    'Private Sub txtNPI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNPI.KeyPress
    '    Try

    '        Dim chkNumeric As String = txtNPI.Text.Trim()
    '        If e.KeyChar = vbBack Or e.KeyChar = vbCr Then
    '            e.Handled = False
    '        Else

    '            If Char.IsDigit(e.KeyChar) Then

    '            Else
    '                MessageBox.Show("Enter valid numeric value", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                e.Handled = True

    '                Exit Sub
    '                'If e.KeyChar = "." And chkNumeric.Contains(".") = False Then

    '                'Else
    '                '    MessageBox.Show("Enter valid numeric value", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                '    e.Handled = True

    '                '    Exit Sub
    '                'End If
    '            End If


    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub chkCompanyAsAbove_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCompanyAsAbove.CheckedChanged
        If chkCompanyAsAbove.Checked = True Then
            'Rahul Patel - > It work the same as above check box 
            ''If the value is assigned then it will assigned it to the PracticeAddress 
            txtCompanyContactName.Text = txtBMContact.Text
            oCompanyAddressContol.isFormLoading = True         ''maily used for the zip code panel visible true/false
            oCompanyAddressContol.txtAddress1.Text = oBussinessAddressContol.txtAddress1.Text        ''Add1
            oCompanyAddressContol.txtAddress2.Text = oBussinessAddressContol.txtAddress2.Text           ''Add2
            oCompanyAddressContol.txtCity.Text = oBussinessAddressContol.txtCity.Text                  ''Zip
            oCompanyAddressContol.cmbCountry.Text = oBussinessAddressContol.cmbCountry.Text          ''Country
            oCompanyAddressContol.txtCounty.Text = oBussinessAddressContol.txtCounty.Text           ''county
            oCompanyAddressContol.cmbState.Text = oBussinessAddressContol.cmbState.Text               ''state

            oCompanyAddressContol.txtZip.Text = oBussinessAddressContol.txtZip.Text
            oCompanyAddressContol.isFormLoading = False         ''maily used for the zip code panel visible true/false

            txtCompanyFax.Text = txtBMFAX.Text
            txtCompanyPhone.Text = mskBMPhoneNo.Text
            txtCompanyEmail.Text = txtBMEmailAddress.Text

        ElseIf (chkCompanyAsAbove.Checked = False) Then
            txtCompanyContactName.Text = ""
            oCompanyAddressContol.isFormLoading = True ''''maily used for the zip code panel visible true/false
            oCompanyAddressContol.txtAddress1.Text = ""
            oCompanyAddressContol.txtAddress2.Text = ""
            oCompanyAddressContol.txtCity.Text = ""
            oCompanyAddressContol.cmbState.Text = ""
            oCompanyAddressContol.cmbCountry.Text = ""
            oCompanyAddressContol.txtCounty.Text = ""
            oCompanyAddressContol.txtZip.Text = ""
            oCompanyAddressContol.isFormLoading = False   ''maily used for the zip code panel visible true/false
            txtCompanyFax.Text = ""
            txtCompanyPhone.Text = ""
            txtCompanyEmail.Text = ""
        End If
    End Sub
    ' Added by Rahul Patel on 24-09-2010 
    ' For allowing only numeric value of NPI field
    Private Sub txtCompanyNPI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNPI_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNPI.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub

    ' Added by Rahul Patel on 24-09-2010 
    ' For allowing only numeric value of company tax id field
    Private Sub txtCompanyTaxID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub
    ' Added by Rahul Patel on 24-09-2010 
    ' For allowing only numeric value of Employee id field
    Private Sub txt_EmployerID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_EmployerID.KeyPress
        If Regex.IsMatch(e.KeyChar.ToString(), "^[0-9\b]*$") = False Then
            e.Handled = True
        End If
    End Sub
    ''Added On 20101005 by Sanjog for signature 
    Private Sub btnAssignSign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignSign.Click
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
            End If

            oListControl = New gloListControl.gloListControl(mdlGeneral.GetConnectionString, gloListControl.gloListControlType.Users, True, Me.Width)
            oListControl.ClinicID = _nClinicID
            oListControl.ControlHeader = "Assign Signature"
            _CurrentControlType = gloListControl.gloListControlType.Providers

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemAssignClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            If cmbAssignUser.Items.Count > 0 Then
                For i As Int64 = 0 To cmbAssignUser.Items.Count - 1
                    cmbAssignUser.SelectedIndex = i
                    Dim oItem As New gloGeneralItem.gloItem
                    oItem.ID = cmbAssignUser.SelectedValue
                    oItem.Description = cmbAssignUser.Text
                    oListControl.SelectedItems.Add(oItem)
                    oItem = Nothing
                Next
            End If

            Me.Controls.Add(oListControl)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oListControl_ItemAssignClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer
        Try
            cmbAssignUser.DataSource = Nothing
            cmbAssignUser.Items.Clear()
            If oListControl.SelectedItems.Count > 0 Then
                Dim oAddData As AddData
                Dim arrlist As New ArrayList
                For i = 0 To oListControl.SelectedItems.Count - 1
                    Dim str As String = ""
                    str = oListControl.SelectedItems(i).Description.ToString()
                    Dim ValueMem As String = ""
                    ValueMem = oListControl.SelectedItems(i).ID.ToString()
                    oAddData = New AddData(str, ValueMem)
                    arrlist.Add(oAddData)
                    cmbAssignUser.DisplayMember = "CmbDisplayMember"
                    cmbAssignUser.ValueMember = "CmbValueMember"
                    cmbAssignUser.SelectedIndex = -1
                Next
                cmbAssignUser.DataSource = arrlist
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillSignature(ByVal pID As Int64)
        Dim con As New SqlConnection(mdlGeneral.GetConnectionString())
        Dim str As String
        Dim da As SqlDataAdapter
        Dim oAddData As AddData
        Dim arrlist As New ArrayList
        Dim ds As New DataSet
        Try
            str = "select  User_Mst.nUserID,User_Mst.sLoginName from User_Mst INNER JOIN ProviderSignature_DTL ON User_Mst.nUserID=ProviderSignature_DTL.nUserID Where ProviderSignature_DTL.nProviderID=" & pID & ""
            da = New SqlDataAdapter(str, con)
            da.Fill(ds)
            Dim i As Int32

            For i = 0 To ds.Tables(0).Rows.Count - 1
                str = ds.Tables(0).Rows(i)(1).ToString()
                Dim ValueMem As String = ""
                ValueMem = ds.Tables(0).Rows(i)(0).ToString()
                oAddData = New AddData(str, ValueMem)
                arrlist.Add(oAddData)
                cmbAssignUser.DisplayMember = "CmbDisplayMember"
                cmbAssignUser.ValueMember = "CmbValueMember"
                cmbAssignUser.SelectedIndex = -1
            Next

            cmbAssignUser.DataSource = arrlist


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveSignature(ByVal pID As Int64)
        Dim con As New SqlConnection(mdlGeneral.GetConnectionString())
        Dim str As String
        Dim i As Int64
        Dim cmd As SqlCommand
        Try
            str = "delete from ProviderSignature_DTL where nProviderID=" & pID & ""
            con.Open()
            cmd = New SqlCommand(str, con)
            cmd.ExecuteNonQuery()
            Dim oDB As New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString())
            Dim strDisplayMember As String
            Dim strValueMember As String
            If (cmbAssignUser.Items.Count > 0) Then
                For i = 0 To cmbAssignUser.Items.Count - 1

                    Dim oaddData As AddData

                    'cmbAssignUser.SelectedIndex = i


                    oaddData = cmbAssignUser.Items(i)
                    strValueMember = oaddData.CmbValueMember

                    str = "Insert Into ProviderSignature_DTL(nProviderID,nUserID) values(" & pID & "," & strValueMember & ")"
                    cmd = New SqlCommand(str, con)
                    cmd.ExecuteNonQuery()
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub pnl_generalDetails_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub Panel9_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel9.Paint

    End Sub

    Private Sub txtBPracPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPracPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtBPracFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBPracFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub maskedBpracPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedBpracPhno.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtCompanyPhone_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyPhone.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtCompanyFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskPLPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskPLFax_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskPLFax.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub maskedPLPhno_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maskedPLPhno.ErrorMessageInvoked
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

    Private Sub txtBMPager_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBMPager.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub txtBMFAX_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBMFAX.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub mskBMPhoneNo_ErrorMessageInvoked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mskBMPhoneNo.ErrorMessageInvoked
        ValidationFailed = True
    End Sub

    Private Sub TabControl1_Deselecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Deselecting
        e.Cancel = ValidationFailed
        ValidationFailed = False
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



    Private Function GetCatalogslist() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim dtResult As DataTable
        oDB.Connect(False)
        Try
            oDB.Retrive("gsp_GetCDS_Catalog", dtResult)
            Return dtResult
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dtResult
        Finally
            oDB.Disconnect()
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Function
    Private Function GetMultipleSupervisorsforPaperRx() As Boolean
        Dim oclsSetting As clsSettings = Nothing
        Dim ObjValue As Object = Nothing
        Try
            oclsSetting = New clsSettings()
            oclsSetting.GetSetting("Multiple Supervisors for Paper Rx", 0, 1, ObjValue)
            If Not IsNothing(ObjValue) Then
                blnMultipleSupervisorsforPaperRx = ObjValue
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsSetting) Then
                oclsSetting = Nothing
            End If
        End Try
    End Function
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
    Private Sub chkRequire_Supervising_Provider_for_eRx_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRequire_Supervising_Provider_for_eRx.CheckStateChanged
        If chkRequire_Supervising_Provider_for_eRx.CheckState = CheckState.Checked Then
            If blnMultipleSupervisorsforPaperRx = True Then
                MessageBox.Show("Admin setting ‘Multiple Supervisors for Paper Rx’ is ON.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub chkCIMessage_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles chkCIMessage.CheckStateChanged
        If chkCIMessage.Checked = True Then
            ChkCIEvent.Checked = True

        Else
            ChkCIEvent.Checked = False
        End If
        DirectAddress()
    End Sub

    Private Sub ChkCIEvent_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles ChkCIEvent.CheckStateChanged
        DirectAddress()
    End Sub

    Private Sub GenerateDirectAddress()
        Dim _strDirectDomainname As String = ""

        If gblnIsStagingServer = True Then
            _strDirectDomainname = GetDirectAddress(True)
        Else
            _strDirectDomainname = GetDirectAddress(False)
        End If

        If _strDirectDomainname <> "" Then
            lblDirectAddressValue.Text = "@" + _strDirectDomainname
        End If

        If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
            txtDirectAddress.Text = txtFirstName.Text.Trim() + txtLastName.Text.Trim()
        ElseIf txtFirstName.Text = "" And txtLastName.Text <> "" Then
            txtDirectAddress.Text = txtLastName.Text.Trim()
        ElseIf txtFirstName.Text <> "" And txtLastName.Text = "" Then
            txtDirectAddress.Text = txtFirstName.Text.Trim()
        End If
    End Sub

    Private Sub DirectAddress()

        'Dim bPassThrough As Boolean = False

        'If IsLoading = False Then

        If ChkCIEvent.Checked = True And chkCIMessage.Checked = True And rbPrescriber.Checked = True Then

            pnlSPI.Visible = True
            txtDirectAddress.Enabled = True
            txtDirectAddress.Visible = True
            lblDirectAddress.Visible = True
            lblDirectAddressValue.Visible = True

            'If txtSPI.Text <> "" Then
            '    lblRoot.Visible = True
            '    txtSPI.Visible = True
            'Else
            lblRoot.Visible = False
            txtSPI.Visible = False
            'End If

            If lblSPI.Text <> "" Then
                lblSPI.Visible = True
                lblLabelSPI.Visible = True
            Else
                lblSPI.Visible = False
                lblLabelSPI.Visible = False
            End If

            SetDirectAddresses()

        Else
            If pnlSPI.Visible = True Then
                pnlSPI.Visible = False
                txtDirectAddress.Text = ""
                lblDirectAddressValue.Text = ""
            End If
        End If

        If ChkCIEvent.Checked = True And chkCIMessage.Checked = True And rbUpdate.Checked = True Then

            txtDirectAddress.Enabled = True
            pnlSPI.Visible = True

            'If txtSPI.Text <> "" Then
            '    lblRoot.Visible = True
            '    txtSPI.Visible = True
            'Else
            lblRoot.Visible = False
            txtSPI.Visible = False
            'End If

            If lblSPI.Text <> "" Then
                lblSPI.Visible = True
                lblLabelSPI.Visible = True
            Else
                lblSPI.Visible = False
                lblLabelSPI.Visible = False
            End If


            'txtDirectAddress.Enabled = False

            If txtDirectAddress.Tag Is Nothing Then
                SetDirectAddresses()
            Else
                txtDirectAddress.Enabled = False
                txtDirectAddress.Text = DirectCast(txtDirectAddress.Tag, String())(0)
                lblDirectAddressValue.Text = "@" + DirectCast(txtDirectAddress.Tag, String())(1)
            End If
        End If

        If ChkCIEvent.Checked AndAlso chkCIMessage.Checked AndAlso rbPrescriberLocation.Checked Then
            pnlSPI.Visible = True
            'txtSPI.Clear()
            txtSPI.Visible = True
            txtSPI.Enabled = True
            lblRoot.Visible = True
            lblSPI.Text = SPI
            lblDirectAddress.Visible = False
        ElseIf rbPrescriberLocation.Checked Then
            pnlSPI.Visible = True
            txtSPI.Visible = True
            txtSPI.Enabled = True
            lblDirectAddress.Visible = False
            lblSPI.Visible = False
            lblLabelSPI.Visible = False
        End If
        'End If
    End Sub

    Private Sub SetDirectAddresses()
        Dim _strDirectDomainname As String = ""

        If gblnIsStagingServer = True Then
            _strDirectDomainname = GetDirectAddress(True)
        Else
            _strDirectDomainname = GetDirectAddress(False)
        End If

        If _strDirectDomainname <> "" Then
            lblDirectAddressValue.Text = "@" + _strDirectDomainname
        End If

        If txtFirstName.Text <> "" And txtLastName.Text <> "" Then
            txtDirectAddress.Text = txtFirstName.Text.Trim() + txtLastName.Text.Trim()
        ElseIf txtFirstName.Text = "" And txtLastName.Text <> "" Then
            txtDirectAddress.Text = txtLastName.Text.Trim()
        ElseIf txtFirstName.Text <> "" And txtLastName.Text = "" Then
            txtDirectAddress.Text = txtFirstName.Text.Trim()
        End If
    End Sub

    Public Function GetDirectAddress(ByVal IsStaging As Boolean) As String
        Dim conn As New SqlConnection(mdlGeneral.GetConnectionString)
        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String = ""
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text

            If IsStaging = True Then
                _strsql = "select ISNULL(sSettingsValue,'') from settings Where sSettingsName='STAGING_DIRECT_DOMAINNAME'"
            Else
                _strsql = "select ISNULL(sSettingsValue,'') from settings Where sSettingsName='PRODUCTION_DIRECT_DOMAINNAME'"
            End If


            sql.CommandText = _strsql
            sql.Connection = conn

            Dim strAddress As String = sql.ExecuteScalar
            If Not IsDBNull(strAddress) Then
                Return strAddress
            Else
                Return ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Sub txtLastName_Leave(sender As System.Object, e As System.EventArgs) Handles txtLastName.Leave
        If rbPrescriber.Checked Then
            If txtFirstName.Text <> "" Then
                txtDirectAddress.Text = txtFirstName.Text.Trim() + txtLastName.Text.Trim()
            Else
                txtDirectAddress.Text = txtLastName.Text.Trim()
            End If
        End If
    End Sub

    Private Sub txtFirstName_Leave(sender As System.Object, e As System.EventArgs) Handles txtFirstName.Leave
        If rbPrescriber.Checked Then
            If txtFirstName.Text <> "" Then
                txtDirectAddress.Text = txtFirstName.Text.Trim() + txtLastName.Text.Trim()
            Else
                txtDirectAddress.Text = txtFirstName.Text.Trim()
            End If
        End If
    End Sub

    'Private Sub chkCIMessage_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCIMessage.CheckedChanged
    '    DirectAddress()
    'End Sub

    'Private Sub ChkCIEvent_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkCIEvent.CheckedChanged
    '    DirectAddress()
    'End Sub

    Private Sub DetachEvents()
        RemoveHandler Me.rbPrescriber.CheckedChanged, AddressOf rbPrescriber_CheckedChanged
        RemoveHandler Me.rbPrescriberLocation.CheckedChanged, AddressOf rbPrescriberLocation_CheckedChanged
        RemoveHandler Me.rbUpdate.CheckedChanged, AddressOf rbUpdate_CheckedChanged

        RemoveHandler Me.ChkCIEvent.CheckStateChanged, AddressOf ChkCIEvent_CheckStateChanged
        RemoveHandler Me.chkCIMessage.CheckStateChanged, AddressOf chkCIMessage_CheckStateChanged

    End Sub

    Private Sub AttachEvents()
        AddHandler Me.rbPrescriber.CheckedChanged, AddressOf rbPrescriber_CheckedChanged
        AddHandler Me.rbPrescriberLocation.CheckedChanged, AddressOf rbPrescriberLocation_CheckedChanged
        AddHandler Me.rbUpdate.CheckedChanged, AddressOf rbUpdate_CheckedChanged

        AddHandler Me.ChkCIEvent.CheckedChanged, AddressOf ChkCIEvent_CheckStateChanged
        AddHandler Me.chkCIMessage.CheckedChanged, AddressOf chkCIMessage_CheckStateChanged
    End Sub

    Private Sub txtDirectAddress_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDirectAddress.Validating

        Dim charInvalid As New Generic.List(Of Char)
        charInvalid.AddRange("!@#$%^&*()+=\][{}';:/?><`~ ")

        If Convert.ToString(txtDirectAddress.Text).IndexOfAny(charInvalid.ToArray) > -1 Then
            MsgBox("Direct Address cannot contain any blank spaces or special characters.", MsgBoxStyle.Information)
            txtDirectAddress.Focus()
        End If

        charInvalid.Clear()
        charInvalid = Nothing

    End Sub


    Private Sub btnFontDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontDialog.Click
        If fdSignature.ShowDialog() = DialogResult.OK Then
            txtFont.Text = Convert.ToString(fdSignature.Font.FontFamily.Name) + "," + Convert.ToString(Convert.ToInt16(fdSignature.Font.Size))
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
    Private Function GetLoginName(ByVal _nUserID As Long) As String

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oLabId As Object = Nothing
        Dim sLabId As String = String.Empty
        Dim _sqlQuery As String = String.Empty
        Try
            oDB.Connect(False)
            _sqlQuery = "select LTrim (ISNULL(sFirstName,'')+' '+ISNULL(sLastName,'')) as FullName  from User_MST where nUserID  = " & _nUserID
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Activate, "Get LoginName " & sLabId & " " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
    End Function
    Private Function fillPrescriberDetails() As Prescriber
        Dim requestObj As gloSureScript.Prescriber = Nothing
        Try
            requestObj = New gloSureScript.Prescriber

            requestObj.PrescriberName.FirstName = txtFirstName.Text
            requestObj.PrescriberName.LastName = txtLastName.Text
            requestObj.PrescriberName.MiddleName = txtMiddleName.Text
            requestObj.NPI = txtNPI.Text
            If optMale.Checked Then
                requestObj.Gender = "M"
            ElseIf optFemale.Checked Then
                requestObj.Gender = "F"
            End If
            requestObj.PrescriberAddress.Address1 = oBussinessAddressContol.txtAddress1.Text
            requestObj.PrescriberAddress.Address2 = oBussinessAddressContol.txtAddress2.Text
            requestObj.PrescriberAddress.City = oBussinessAddressContol.txtCity.Text
            requestObj.PrescriberAddress.State = oBussinessAddressContol.cmbState.Text
            requestObj.PrescriberAddress.Zip = oBussinessAddressContol.txtZip.Text
            requestObj.ProviderSSN = mtxt_SSNno.Text
            Dim datetime As DateTime = mtxtDOB.Text
            requestObj.DateTimeStamp = datetime.ToString("yyyyMMdd") 'use Field as DOB
            requestObj.DEA = txtDEA.Text
            requestObj.NADEAN = txtNADEAN.Text
            requestObj.PrescriberPhone.Email = txtBMEmailAddress.Text
            Return requestObj
        Catch ex As Exception
            requestObj = Nothing
        Finally
            If Not IsNothing(requestObj) Then
                requestObj.Dispose()
                requestObj = Nothing
            End If
        End Try

    End Function
    Private Function getEpcsRequest(val As EpcsSeviceCall) As EpcsRequest
        Dim request As EpcsRequest = New EpcsRequest
        If EpcsSeviceCall.WSInvitePrescriber = val Then
            Dim requestObj As gloSureScript.Prescriber = fillPrescriberDetails()
            If Not IsNothing(requestObj) Then
                request.FlagEpcsSeviceCall = EpcsSeviceCall.WSInvitePrescriber
                request.RequestBody = requestObj
            End If
        ElseIf EpcsSeviceCall.UILaunchLogicalAccess = val Then
            request.FlagEpcsSeviceCall = EpcsSeviceCall.UILaunchLogicalAccess
            Dim requestbody As New clsUiLaunchLogicalAccessRequest
            requestbody.ApprovedByStaffName = GetLoginName(gnLoginID)
            'txtUserName.Text
            requestbody.CloseWindowOnExit = "n"
            requestbody.PostBackUrl = ""
            Dim uniqueId As Guid = Guid.NewGuid()
            requestbody.SourceTransactionReferenceNumber = uniqueId.ToString()
            requestbody.ValidatingPrescriberNpi = txtNPI.Text '1508869520
            request.RequestBody = requestbody
        ElseIf EpcsSeviceCall.WSGetPrescriberStatus = val Then
            request.FlagEpcsSeviceCall = EpcsSeviceCall.WSGetPrescriberStatus
            Dim requestbody As New clsWsGetPrescriberStatus
            requestbody.Npi = txtNPI.Text '5627182012
            request.RequestBody = requestbody
        End If
        Return request
    End Function
    Private Sub btnInvitePrescriber_Click(sender As System.Object, e As System.EventArgs) Handles btnInvitePrescriber.Click
        If CheckEmailAddress(txtBMEmailAddress.Text.Trim()) = False Then  ''Email Validation
            MessageBox.Show("Please enter a valid email address.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            txtBMEmailAddress.Focus()
            Exit Sub
        End If
        If txtNPI.Text.Trim() = "" Then  ''NPI Validation
            MessageBox.Show("Please enter a NPI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            txtNPI.Focus()
            Exit Sub
        End If
        If Len(Trim(txtDEA.Text)) >= 0 And Len(Trim(txtDEA.Text)) < 9 Then
            MessageBox.Show("Please enter a DEA No.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            txtDEA.Focus()
            Exit Sub
        End If
        If Trim(txtFirstName.Text) = "" Then
            MessageBox.Show("Please enter First Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            txtFirstName.Focus()
            Exit Sub
        End If
        If Trim(txtLastName.Text) = "" Then
            MessageBox.Show("Please enter Last Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            txtLastName.Focus()
            Exit Sub
        End If
        If Not (optMale.Checked Or optFemale.Checked) Then
            MessageBox.Show("Please enter Gender", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            Exit Sub
        End If
        If (oBussinessAddressContol.txtZip.TextLength >= 0 And oBussinessAddressContol.txtZip.TextLength < 4) Then
            MessageBox.Show("Please enter Area code information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            oBussinessAddressContol.txtZip.Select()
            oBussinessAddressContol.txtZip.Focus()
            Exit Sub
        End If
        If oBussinessAddressContol.txtCity.Text = "" Then
            MessageBox.Show("Please enter City", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            oBussinessAddressContol.txtCity.Focus()
            Exit Sub
        End If
        If oBussinessAddressContol.txtAddress1.Text = "" Then
            MessageBox.Show("Please enter Address", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            oBussinessAddressContol.txtAddress1.Focus()
            Exit Sub
        End If
        If oBussinessAddressContol.cmbState.Text = "" Then
            MessageBox.Show("Please enter State", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            oBussinessAddressContol.cmbState.Focus()
            Exit Sub
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
        Else
            MessageBox.Show("Please enter date of birth.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TabControl1.SelectedTab = tbpgProvider
            mtxtDOB.Focus()
            Exit Sub
        End If

        Dim objInterface As clsEPCSHelper = Nothing
        Dim epcsRequest As EpcsRequest = Nothing
        Dim strFileName As String = String.Empty
        Dim flag As Boolean
        Try
            gloSurescriptGeneral.ServerName = gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = gstrDatabaseName
            gloSurescriptGeneral.RootPath = System.Windows.Forms.Application.StartupPath
            gloSurescriptGeneral.blnStagingServer = gblnIsStagingServer
            gloSurescriptGeneral.blnSQLAuthentication = gblnSQLAuthentication
            gloSurescriptGeneral.UserName = gstrSQLUserEMR
            gloSurescriptGeneral.Password = gstrSQLPasswordEMR
            gloSurescriptGeneral.blnNCPDP10dot6 = gbln10dot6Version
            gloSurescriptGeneral.checkDownloadVersion()
            gloSurescriptGeneral.blnSecureMessage4dot5 = gblnSecureMessage4dot5
            gloSurescriptGeneral.sSSPRODUCTIONACCOUNTID = gstrSSPRODUCTIONACCOUNTID ''261 common for both 10.6 and 8.1 portals
            gloSurescriptGeneral.sSSPRODUCTIONPORTALID = gstrSSPRODUCTIONPORTALID ''264 for 8.1 portal
            gloSurescriptGeneral.sSSPRODUCTION10dot6PORTALID = gstrSSPRODUCTION10dot6PORTALID ''1018 for 10.6 portal

            gloSurescriptGeneral.sSTAGING10DOT6ACCOUNTID = gstrSTAGING10DOT6ACCOUNTID ''338 common for both 10.6 and 8.1 portals
            gloSurescriptGeneral.sSTAGING10DOT6PORTALID = gstrSTAGING10DOT6PORTALID ''2273 for 10.6 portal
            gloSurescriptGeneral.sSTAGING8DOT1PORTALID = gstrSTAGING8DOT1PORTALID ''422 for 8.1 portal
            gloSurescriptGeneral.MessageBoxCaption = gstrMessageBoxCaption
            gloSurescriptGeneral.sAusID = frmSettings_New.GetClinicInformation("sExternalcode")
            objInterface = New clsEPCSHelper
            epcsRequest = getEpcsRequest(EpcsSeviceCall.WSInvitePrescriber)
            epcsRequest.RequestHeder = objInterface.getRequestHeder()
            strFileName = objInterface.GenerateWSInvitePrescriber(epcsRequest)
            If strFileName <> "" Then
                If System.IO.File.Exists(strFileName) Then
                    flag = gloSureScript.gloSurescriptGeneral.GeneratePostOrganizationRequest(strFileName, "WSInvitePrescriber")
                End If
            End If
            If flag Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Invite, "Prescriber invited successfull", gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Invite, "Prescriber invited unsuccessfull", gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Invite, "InvitePrescriber Call. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(objInterface) Then
                objInterface.Dispose()
                objInterface = Nothing
            End If
            epcsRequest = Nothing
            flag = False
        End Try
    End Sub
    Private Sub btnEnrollPrescriber_Click(sender As System.Object, e As System.EventArgs) Handles btnEnrollPrescriber.Click
        Dim objInterFace As clsEPCSHelper = New clsEPCSHelper
        Dim flag As Boolean
        Dim strFileName As String = ""
        gloSurescriptGeneral.ServerName = gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = gstrDatabaseName
        gloSurescriptGeneral.RootPath = System.Windows.Forms.Application.StartupPath
        gloSurescriptGeneral.blnStagingServer = gblnIsStagingServer
        gloSurescriptGeneral.blnSQLAuthentication = gblnSQLAuthentication
        gloSurescriptGeneral.UserName = gstrSQLUserEMR
        gloSurescriptGeneral.Password = gstrSQLPasswordEMR
        gloSurescriptGeneral.blnNCPDP10dot6 = gbln10dot6Version
        gloSurescriptGeneral.checkDownloadVersion()
        gloSurescriptGeneral.blnSecureMessage4dot5 = gblnSecureMessage4dot5
        gloSurescriptGeneral.sSSPRODUCTIONACCOUNTID = gstrSSPRODUCTIONACCOUNTID
        gloSurescriptGeneral.sSSPRODUCTIONPORTALID = gstrSSPRODUCTIONPORTALID
        gloSurescriptGeneral.sSSPRODUCTION10dot6PORTALID = gstrSSPRODUCTION10dot6PORTALID

        gloSurescriptGeneral.sSTAGING10DOT6ACCOUNTID = gstrSTAGING10DOT6ACCOUNTID
        gloSurescriptGeneral.sSTAGING10DOT6PORTALID = gstrSTAGING10DOT6PORTALID
        gloSurescriptGeneral.sSTAGING8DOT1PORTALID = gstrSTAGING8DOT1PORTALID
        gloSurescriptGeneral.MessageBoxCaption = gstrMessageBoxCaption
        gloSurescriptGeneral.sAusID = frmSettings_New.GetClinicInformation("sExternalcode")
        Dim epcsRequest As EpcsRequest = getEpcsRequest(EpcsSeviceCall.UILaunchLogicalAccess)
        epcsRequest.RequestHeder = objInterFace.getRequestHeder()
        Try

            strFileName = objInterFace.GenerateUiLaunchLogicalAccess(epcsRequest)
            If strFileName <> "" Then
                If System.IO.File.Exists(strFileName) Then
                    Dim _isInternetAvailable As String = gloSurescriptGeneral.IsInternetConnectionAvailable()
                    If _isInternetAvailable Then
                        Using ofrmprocessepcs As New frmRegisterPrescriber()
                            ofrmprocessepcs.ServiceCall = EpcsSeviceCall.UILaunchLogicalAccess
                            ofrmprocessepcs.StrfileName = strFileName
                            ofrmprocessepcs.ShowDialog()
                        End Using
                    Else
                        MessageBox.Show("You are not connected to the internet.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Activate, "Access the DrFirst Logical Access Control (LAC).", gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Activate, "Access the DrFirst Logical Access Control (LAC) unsuccessfull.", gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Activate, "UiLaunch Logical Access Call Fails." & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            epcsRequest = Nothing
            flag = False
        Finally
            If Not IsNothing(objInterFace) Then
                objInterFace.Dispose()
                objInterFace = Nothing
            End If
        End Try
    End Sub
    Private Sub LinkLabelUILaunchPrescriberArea_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelUILaunchPrescriberArea.LinkClicked
        LinkLabelUILaunchPrescriberArea.LinkVisited = True
        Dim UrlForUILaunchPrescriberArea As String = ""
        If gloSurescriptGeneral.blnStagingServer Then
            UrlForUILaunchPrescriberArea = clsEPCSHelper.sEpcsUrl & "pob/login"
            LinkLabelUILaunchPrescriberArea.LinkArea = New LinkArea(0, 50)
        Else
            UrlForUILaunchPrescriberArea = clsEPCSHelper.sEpcsUrl & "pob/login"
            LinkLabelUILaunchPrescriberArea.LinkArea = New LinkArea(0, 50)
        End If
        System.Diagnostics.Process.Start(UrlForUILaunchPrescriberArea)
    End Sub
    Private Function GetEpcsGoldPrescriberStatus() As Boolean
        Dim objInterFace As clsEPCSHelper = New clsEPCSHelper
        Dim flag As Boolean
        Dim strFileName As String = ""
        gloSurescriptGeneral.ServerName = gstrSQLServerName
        gloSurescriptGeneral.DatabaseName = gstrDatabaseName
        gloSurescriptGeneral.RootPath = System.Windows.Forms.Application.StartupPath
        gloSurescriptGeneral.blnStagingServer = gblnIsStagingServer
        gloSurescriptGeneral.blnSQLAuthentication = gblnSQLAuthentication
        gloSurescriptGeneral.UserName = gstrSQLUserEMR
        gloSurescriptGeneral.Password = gstrSQLPasswordEMR
        gloSurescriptGeneral.blnNCPDP10dot6 = gbln10dot6Version
        gloSurescriptGeneral.checkDownloadVersion()
        gloSurescriptGeneral.blnSecureMessage4dot5 = gblnSecureMessage4dot5
        gloSurescriptGeneral.sSSPRODUCTIONACCOUNTID = gstrSSPRODUCTIONACCOUNTID ''261 common for both 10.6 and 8.1 portals
        gloSurescriptGeneral.sSSPRODUCTIONPORTALID = gstrSSPRODUCTIONPORTALID ''264 for 8.1 portal
        gloSurescriptGeneral.sSSPRODUCTION10dot6PORTALID = gstrSSPRODUCTION10dot6PORTALID ''1018 for 10.6 portal
        gloSurescriptGeneral.sSTAGING10DOT6ACCOUNTID = gstrSTAGING10DOT6ACCOUNTID ''338 common for both 10.6 and 8.1 portals
        gloSurescriptGeneral.sSTAGING10DOT6PORTALID = gstrSTAGING10DOT6PORTALID ''2273 for 10.6 portal
        gloSurescriptGeneral.sSTAGING8DOT1PORTALID = gstrSTAGING8DOT1PORTALID ''422 for 8.1 portal
        gloSurescriptGeneral.MessageBoxCaption = gstrMessageBoxCaption
        gloSurescriptGeneral.sAusID = frmSettings_New.GetClinicInformation("sExternalcode")
        Dim epcsRequest As EpcsRequest = getEpcsRequest(EpcsSeviceCall.WSGetPrescriberStatus)
        epcsRequest.RequestHeder = objInterFace.getRequestHeder()
        Try
            strFileName = objInterFace.GenerateWSGetPrescriberStatus(epcsRequest)
            If strFileName <> "" Then
                If System.IO.File.Exists(strFileName) Then
                    flag = gloSureScript.gloSurescriptGeneral.GeneratePostOrganizationRequest(strFileName, "WSGetPrescriberStatus")
                End If
            End If
            If flag Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "Prescriber status verified with DrFirst", gloAuditTrail.ActivityOutCome.Success)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "Prescriber is registered with DrFirst", gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "Prescriber status verified with DrFirst", gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "Prescriber is not registered with DrFirst", gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "PrescriberStatus Call. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            flag = False
        Finally
            If Not IsNothing(objInterFace) Then
                objInterFace.Dispose()
                objInterFace = Nothing
            End If
        End Try
        Return flag
        Return True
    End Function

#Region "ePA"

    'Private Sub AddDefaultRole()
    '    If String.IsNullOrEmpty(Me.ProviderSPI) = False Then
    '        If Me.ChkePA.Checked Then
    '            pnlPARoles.Enabled = True
    '            If listUsers.Any(Function(p) p.UserID = Me.nProviderUserID) AndAlso Not listSubmitter.Any(Function(p) p.UserID = Me.nProviderUserID) Then
    '                Dim ePARole As EPARole = listUsers.FirstOrDefault(Function(p) p.UserID = Me.nProviderUserID)

    '                If ePARole IsNot Nothing Then
    '                    Me.UserRolesModified = True
    '                    ePARole.EPARole = RoleType.PASubmitter

    '                    listUsers.Remove(ePARole)
    '                    listSubmitter.Add(ePARole)

    '                    Me.RefreshBinding(lstUsers, listUsers)
    '                    Me.RefreshBinding(lstSubmitter, listSubmitter)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub TabControl1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

    '    Try
    '        If TabControl1.SelectedTab.Name = "tbpg_ePASettings" Then
    '            Me.AddDefaultRole()
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, "User Roles" & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    End Try
    'End Sub


    Private Function GetRoles(ByRef DataTable As DataTable, ByVal EPARole As RoleType) As List(Of EPARole)

        Return DataTable.AsEnumerable().Select(Function(p) New EPARole(
                                        Convert.ToInt64(p("nUserID")),
                                        Convert.ToString(p("sLoginName")),
                                        p("nPARoleID"))).Where(Function(p) p.EPARole = EPARole).ToList()

    End Function

    Private Sub FillEPARoles()
        Try
            Dim ePADatabase As New ePADatabaseLayer()
            Using dsRoles As DataSet = ePADatabase.GetUserRoles(Me._nProviderID)

                listUsers = Me.GetRoles(dsRoles.Tables(0), RoleType.None)
                listPreparer = Me.GetRoles(dsRoles.Tables(0), RoleType.PAPreparer)
                listReviewer = Me.GetRoles(dsRoles.Tables(0), RoleType.PAReviewer)
                listSubmitter = Me.GetRoles(dsRoles.Tables(0), RoleType.PASubmitter)

                If dsRoles.Tables(1).Rows.Count > 0 Then
                    Me.nProviderUserID = Convert.ToInt64(dsRoles.Tables(1).Rows(0)("nUserID"))
                End If
            End Using

            Me.RefreshBinding(lstSubmitter, listSubmitter)
            Me.RefreshBinding(lstPreparer, listPreparer)
            Me.RefreshBinding(lstReviewer, listReviewer)
            Me.RefreshBinding(lstUsers, listUsers)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#Region "Add and Remove role"
    Private Sub btnAddRole_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRole.Click
        Try
            If lstUsers.SelectedItem IsNot Nothing AndAlso TypeOf lstUsers.SelectedItem Is EPARole Then

                Me.UserRolesModified = True

                Dim nUserID As Int64 = DirectCast(lstUsers.SelectedItem, EPARole).UserID
                Dim ePARole As EPARole = listUsers.FirstOrDefault(Function(p) p.UserID = nUserID)

                If ePARole IsNot Nothing Then
                    listUsers.Remove(ePARole)
                End If

                Me.RefreshBinding(lstUsers, listUsers)

                If SelectedRoleType = RoleType.PASubmitter Then
                    ePARole.EPARole = RoleType.PASubmitter
                    listSubmitter.Add(ePARole)
                    Me.RefreshBinding(lstSubmitter, listSubmitter)
                ElseIf SelectedRoleType = RoleType.PAReviewer Then
                    ePARole.EPARole = RoleType.PAReviewer
                    listReviewer.Add(ePARole)
                    Me.RefreshBinding(lstReviewer, listReviewer)
                ElseIf SelectedRoleType = RoleType.PAPreparer Then
                    ePARole.EPARole = RoleType.PAPreparer
                    listPreparer.Add(ePARole)
                    Me.RefreshBinding(lstPreparer, listPreparer)
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub btnRemoveRole_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRole.Click
        Try
            Me.UserRolesModified = True

            If SelectedRoleType = RoleType.PASubmitter Then
                Me.AddRemoveUserRoles(listSubmitter, lstSubmitter)
                Me.RefreshBinding(lstSubmitter, listSubmitter)
            ElseIf SelectedRoleType = RoleType.PAReviewer Then
                Me.AddRemoveUserRoles(listReviewer, lstReviewer)
                Me.RefreshBinding(lstReviewer, listReviewer)
            ElseIf SelectedRoleType = RoleType.PAPreparer Then
                Me.AddRemoveUserRoles(listPreparer, lstPreparer)
                Me.RefreshBinding(lstPreparer, listPreparer)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
#End Region

    Private Sub AddRemoveUserRoles(ByRef ListOfEPA As List(Of EPARole), ByRef ListControl As ListBox)
        Try
            If ListControl.SelectedItem IsNot Nothing AndAlso TypeOf ListControl.SelectedItem Is EPARole Then
                Dim ePARole As EPARole = DirectCast(ListControl.SelectedItem, EPARole)
                ePARole.EPARole = RoleType.None
                If ePARole IsNot Nothing Then
                    ListOfEPA.Remove(ePARole)
                    listUsers.Add(ePARole)
                End If
                Me.RefreshBinding(lstUsers, listUsers)
                ePARole = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#Region "Listboxes Focus"
    Private Sub lstSubmitter_Enter(sender As System.Object, e As System.EventArgs) Handles lstSubmitter.Enter
        SelectedRoleType = RoleType.PASubmitter
    End Sub

    Private Sub lstPreparer_Enter(sender As System.Object, e As System.EventArgs) Handles lstPreparer.Enter
        SelectedRoleType = RoleType.PAPreparer
    End Sub

    Private Sub lstReviewer_Enter(sender As System.Object, e As System.EventArgs) Handles lstReviewer.Enter
        SelectedRoleType = RoleType.PAReviewer
    End Sub
#End Region

    Private Sub DisposeEPA()
        If Me.listPreparer IsNot Nothing Then
            listPreparer.Clear()
            listPreparer = Nothing
        End If

        If Me.listReviewer IsNot Nothing Then
            listReviewer.Clear()
            listReviewer = Nothing
        End If

        If Me.listSubmitter IsNot Nothing Then
            listSubmitter.Clear()
            listSubmitter = Nothing
        End If

        If Me.listUsers IsNot Nothing Then
            Me.listUsers.Clear()
            Me.listUsers = Nothing
        End If

    End Sub

#Region "Refresh Bindings"
    Private Sub RefreshBinding(ByRef ListBox As ListBox, ByRef List As List(Of EPARole))
        Try
            ListBox.DataSource = Nothing
            ListBox.DataSource = List.OrderBy(Function(p) p.LoginName).ToList()
            ListBox.DisplayMember = "LoginName"
            ListBox.ValueMember = "UserID"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.UserRoles, ex.Message, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region

    Private Sub ChkePA_CheckStateChanged(sender As System.Object, e As System.EventArgs) Handles ChkePA.CheckStateChanged
        If ChkePA.Checked = True Then
            pnlPARoles.Enabled = True
        Else
            pnlPARoles.Enabled = False
            Me.FillEPARoles()
        End If
    End Sub

#Region "Audit Service Level Changes"
    Private Property UserRolesModified As Boolean = False
    Private Sub AuditServiceLevelChanges(ByVal BeforeServiceLevel As String, ByVal AfterServiceLevel As String)

        Dim sProviderName As String = txtFirstName.Text & " " & txtLastName.Text

        If Not String.IsNullOrEmpty(Me.AfterServiceLevel) Then
            Dim sString As String = ""

            'EPA
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 9, 1) = 1 Then
                    sString = "EPA service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 9, 1) = 0 AndAlso Mid(AfterServiceLevel, 9, 1) = 1 Then
                    sString = "EPA service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 9, 1) = 1 AndAlso Mid(AfterServiceLevel, 9, 1) = 0 Then
                    sString = "EPA service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'New
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 16, 1) = 1 Then
                    sString = "New-Rx service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 16, 1) = 0 AndAlso Mid(AfterServiceLevel, 16, 1) = 1 Then
                    sString = "New-Rx service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 16, 1) = 1 AndAlso Mid(AfterServiceLevel, 16, 1) = 0 Then
                    sString = "New-Rx service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'Refill
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 15, 1) = 1 Then
                    sString = "Refill service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 15, 1) = 0 AndAlso Mid(AfterServiceLevel, 15, 1) = 1 Then
                    sString = "Refill service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 15, 1) = 1 AndAlso Mid(AfterServiceLevel, 15, 1) = 0 Then
                    sString = "Refill service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'Change
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 14, 1) = 1 Then
                    sString = "RxChange service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 14, 1) = 0 AndAlso Mid(AfterServiceLevel, 14, 1) = 1 Then
                    sString = "RxChange service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 14, 1) = 1 AndAlso Mid(AfterServiceLevel, 14, 1) = 0 Then
                    sString = "RxChange service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'RxFill
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 13, 1) = 1 Then
                    sString = "RxFill service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 13, 1) = 0 AndAlso Mid(AfterServiceLevel, 13, 1) = 1 Then
                    sString = "RxFill service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 13, 1) = 1 AndAlso Mid(AfterServiceLevel, 13, 1) = 0 Then
                    sString = "RxFill service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'RxCancel
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 12, 1) = 1 Then
                    sString = "RxCancel service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 12, 1) = 0 AndAlso Mid(AfterServiceLevel, 12, 1) = 1 Then
                    sString = "RxCancel service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 12, 1) = 1 AndAlso Mid(AfterServiceLevel, 12, 1) = 0 Then
                    sString = "RxCancel service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'EPCS
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 5, 1) = 1 Then
                    sString = "Controlled Substance service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 5, 1) = 0 AndAlso Mid(AfterServiceLevel, 5, 1) = 1 Then
                    sString = "Controlled Substance service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 5, 1) = 1 AndAlso Mid(AfterServiceLevel, 5, 1) = 0 Then
                    sString = "Controlled Substance service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            'Direct Msg
            sString = String.Empty
            If String.IsNullOrEmpty(Me.BeforeServiceLevel) Then
                If Mid(AfterServiceLevel, 2, 1) = 1 Then
                    sString = "Direct Message service level enabled for provider " & sProviderName
                End If
            Else
                If Mid(BeforeServiceLevel, 2, 1) = 0 AndAlso Mid(AfterServiceLevel, 2, 1) = 1 Then
                    sString = "Direct Message service level enabled for provider " & sProviderName
                ElseIf Mid(BeforeServiceLevel, 2, 1) = 1 AndAlso Mid(AfterServiceLevel, 2, 1) = 0 Then
                    sString = "Direct Message service level disabled for provider " & sProviderName
                End If
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            sString = String.Empty
            If (PDROnFormLoad = True And chkPDR.Checked = False) Then
                sString = "PDR service level disabled for provider " & sProviderName
            ElseIf (PDROnFormLoad = False And chkPDR.Checked = True) Then
                sString = "PDR service level enabled for provider " & sProviderName
            End If

            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If

            sString = String.Empty
            If (PDNMPOnFormLoad = True And chkPDMP.Checked = False) Then
                sString = "PDMP service level disabled for provider " & sProviderName
            ElseIf (PDNMPOnFormLoad = False And chkPDMP.Checked = True) Then
                sString = "PDMP service level enabled for provider " & sProviderName
            End If
            If Not String.IsNullOrEmpty(sString) AndAlso Not String.IsNullOrWhiteSpace(sString) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.Modify, sString, 0, 0, _nProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If
        End If
    End Sub

    Private Property BeforeServiceLevel As String = ""
    Private Property AfterServiceLevel As String = ""
#End Region

    'Click Event---------
    Private Sub lstSubmitter_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstSubmitter.MouseClick

        btnSubmitter.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_LongOrange
        btnSubmitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnSubmitter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnSubmitter.ForeColor = System.Drawing.Color.Black

        lblSubmitterBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblSubmitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblSubmitterBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblSubmitterLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblSubmitterRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblSubmitterRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblSubmitterTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblSubmitterTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnPreparer

        btnPreparer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnPreparer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnPreparer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnPreparer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblPreparerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblPreparerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblPreparerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnReviewer

        btnReviewer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnReviewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnReviewer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnReviewer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblReviewerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblReviewerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblReviewerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerTopMain.Dock = System.Windows.Forms.DockStyle.Top

    End Sub

    Private Sub lstPreparer_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstPreparer.MouseClick
        btnPreparer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_LongOrange
        btnPreparer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnPreparer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnPreparer.ForeColor = System.Drawing.Color.Black

        lblPreparerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblPreparerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblPreparerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblPreparerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblPreparerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblPreparerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblPreparerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblPreparerTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnReviewer

        btnReviewer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnReviewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnReviewer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnReviewer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblReviewerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblReviewerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblReviewerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblReviewerTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnSubmitter

        btnSubmitter.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnSubmitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnSubmitter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnSubmitter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblSubmitterBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblSubmitterRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblSubmitterTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterTopMain.Dock = System.Windows.Forms.DockStyle.Top


    End Sub

    Private Sub lstReviewer_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstReviewer.MouseClick
        btnReviewer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_LongOrange
        btnReviewer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnReviewer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnReviewer.ForeColor = System.Drawing.Color.Black

        lblReviewerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblReviewerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblReviewerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblReviewerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblReviewerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblReviewerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblReviewerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblReviewerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(0, Byte), Integer))
        lblReviewerTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnSubmitter

        btnSubmitter.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnSubmitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnSubmitter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnSubmitter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblSubmitterBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblSubmitterLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblSubmitterRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblSubmitterTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblSubmitterTopMain.Dock = System.Windows.Forms.DockStyle.Top

        'btnPreparer

        btnPreparer.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        btnPreparer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        btnPreparer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        btnPreparer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))

        lblPreparerHeaderBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerBottomMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom

        lblPreparerLeftMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerLeftMain.Dock = System.Windows.Forms.DockStyle.Left

        lblPreparerRightMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerRightMain.Dock = System.Windows.Forms.DockStyle.Right

        lblPreparerTopMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        lblPreparerTopMain.Dock = System.Windows.Forms.DockStyle.Top

    End Sub
    'Click Event End---------
#End Region

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

                    txtFirstName.ReadOnly = True
                    txtMiddleName.ReadOnly = True
                    txtLastName.ReadOnly = True

                    Dim sServicelvl As String = ""
                    Dim bisPDR As Boolean = False

                    Dim bisPDMP As Boolean = False

                    Using oLicense As New gloAUSLibrary.Class.clsgloLicence(gstrgloAusPortalURL, gstrConnectionString)
                        sServicelvl = oLicense.GetLicensedServiceLevel(txtLicenseKey.Text)
                        _nAUSPortalID = oLicense.GetAUSPortalID(txtLicenseKey.Text)
                        bisPDR = oLicense.getLicensedPDRFlag(txtLicenseKey.Text)

                        bisPDMP = oLicense.getLicensedPDMPFlag(txtLicenseKey.Text)
                    End Using
                    SetLicenceServiceLevels(sServicelvl, bisPDR, bisPDMP)
                    chckDisable.Enabled = False
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
            If TempProviders.ServiceLevel.Trim <> Providers.ServiceLevel.Trim Then
                Return True
            End If
            If TempProviders.ISPDR <> Providers.IsPDREnabled Then
                Return True
            End If
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Status, "ISProviderDataChange. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Function

#End Region
  
    Private Sub txtTaxonomy_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles txtTaxonomy.MouseMove
        C1SuperTooltip1.SetToolTip(txtTaxonomy, txtTaxonomy.Text)
    End Sub

    Private Sub chkPDMP_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkPDMP.CheckedChanged
        Dim icnt As Integer = 0
        Dim cls As New clsProvider
        If chkPDMP.Checked = True Then
            icnt = cls.checkclinicNPI()

            If icnt < 0 Then
                MessageBox.Show("Clinic NPI is mandatory for PDMP. Please enter it through Clinic -> Clinic Details.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                chkPDMP.Checked = False
                Exit Sub
            End If
            If icnt < 10 Then
                MessageBox.Show("Please specify valid Clinic NPI (10 Characters).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                chkPDMP.Checked = False
                Exit Sub
            End If
        End If
    End Sub
    Private Sub ChckEPCS_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChckEPCS.CheckedChanged
      
        If ChckEPCS.Checked = True Then
            chkPDMP.Enabled = True

        Else
            chkPDMP.Enabled = False
            chkPDMP.Checked = False
        End If
    End Sub
End Class



Public Class AddData
    Dim _cmbValueMember As String = ""
    Dim _cmbDisplayMember As String = ""

    Public Sub New(ByVal myDisplayMember As String, ByVal myValueMember As String)
        CmbValueMember = myValueMember
        CmbDisplayMember = myDisplayMember
    End Sub


    Property CmbValueMember() As String
        Get
            Return _cmbValueMember
        End Get
        Set(ByVal value As String)
            _cmbValueMember = value
        End Set
    End Property

    Property CmbDisplayMember() As String
        Get
            Return _cmbDisplayMember
        End Get
        Set(ByVal value As String)
            _cmbDisplayMember = value
        End Set
    End Property

End Class
''Added On 20101005 by Sanjog for signature 
