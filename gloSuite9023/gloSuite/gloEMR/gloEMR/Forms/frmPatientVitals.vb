
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports WAConnSDKATLLib ''imported by Abhijeet on 20110318 for Welch Allyn device interface
Imports System.Collections

Imports gloSettings


Public Class frmPatientVitals
    Inherits System.Windows.Forms.Form
    Dim mergeItem As Integer = 0
    Dim _IsAllItems As Boolean = False
    Public Shared _IsODISettingOn As Boolean = False
    Public Shared _IsDAS28SettingOn As Boolean = False
    Public Shared blnModify As Boolean
    Dim frmCalc_Bmi As frmCalculate_BMI
    ''Start :: BMI Settings
    Public Shared strtxtft As String = ""
    Public Shared strtxtinch As String = ""
    Public Shared strtxtwtoz As String = ""
    Public Shared strtxtwtlbs As String = ""
    Public Shared strtxtwghtlbs As String = ""
    Public Shared strtxtwghtkg As String = ""
    Public Shared strtxtBMI As String = ""
    Public Shared _strtxtODIFromVitals As String = ""
    Public _isformLoad As Boolean = False
    ''End ::  BMI Settings
    Public vitalID As Long
    '' Public _ArrList As New ArrayList
    Public Shared Arrlist As ArrayList
    Private _VisitID As Long
    Private _VisitDate As Date
    Private _VitalID As Long
    Private _PrevVitalID As Long
    Private _PatientID As Long
    Private _IsMakeAsCurrent As Boolean
    Private MeHeight As Integer
    '' to Record lock
    Private _blnRecordLock As Boolean

    Private _DateOfBirth As Date

    'Public Age As String 'comment by sudhir
    Public _AgeInDays As Integer = 0
    Public _AgeInMonths As Double = 0
    Public Years As Int16 = 0
    Public Months As Int16 = 0
    Public Days As Int16 = 0
    Dim THRMin As Double
    Dim THRMax As Double
    Dim dLastperiod As DateTime
    Dim dStatusofLMPeriod As Boolean = False
    Dim _IsTextBoxPresent As Boolean = False

    ''Start :: Checkbox Pain Level
    Dim _blnPainlvl As Boolean = False
    Dim _blnPainWithMedication As Boolean = False
    Dim _blnPainWithoutMedication As Boolean = False
    Dim _blnPainWorst As Boolean = False

    Dim _SectionValue As String
    Dim _SectionName As String
    ''End :: Checkbox Pain Level
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    'Dim _Count As Integer = 0
    Dim _isVitalsHeightCopyForward As Boolean = False
    Dim _strAUSid As String = String.Empty
    Dim _strVitalsKey As String = String.Empty
    Dim _isVitalsDeviceEnabled As Boolean = False
    Dim _NoOfAttemptsToConnectDevice As Int32 = 0

    'added new property for task created from Ob vitals
    Private _nObVitalTask As Int64 = 0
    Private intOBVitalsEditCount As Integer

    '16-Apr-13 Aniket: Resolving Bug Bug #48927
    Public Property OBVitalsEditCount As Integer
        Get
            Return intOBVitalsEditCount
        End Get
        Set(ByVal value As Integer)
            intOBVitalsEditCount = value
        End Set
    End Property

    Public Property OBVitalTask() As Int64
        Get
            Return _nObVitalTask
        End Get
        Set(ByVal value As Int64)
            _nObVitalTask = value
        End Set
    End Property
  
    'OB Vitals
    Dim frmOBVitals As frmOBVital
    Public Shared strtxtBPSittingMax As String = ""
    Public Shared strtxtBPSittingMin As String = ""
    Public Shared strtxtBPStandingMax As String = ""
    Public Shared strtxtBPStandingMin As String = ""
    Public Shared strPainLevel As String = ""
    Public Shared strchkPain As String = ""
    Public Shared strComments As String = ""
    Public Shared vitID As Long = 0
    Public Shared visDate As Date
    Public Shared makeCurrent As Boolean
    Public Shared prevVitID As Long = 0
    Public Shared nVisitID As Long = 0

    Public Shared strchkLMP As String = ""
    Public Shared lmpDate As String = ""
    Public Shared patientID As Long = 0
    Public Shared IsVitalEnabled As Boolean = False
    Public Shared blnMedCatatRisk As Boolean = False ''check if patient has high risk medical category while adding new vitals
    Dim _hashOBVital As New Hashtable()
    Enum WeightIn
        Lbs
        Kg
        LbsOz
    End Enum
    Dim _WaightFlag As WeightIn
    Dim _TempInCelcius As Boolean = False
    Dim _HeadCircumInch As Boolean = False
    Dim _StatureInInch As Boolean = False
    Dim _NeckCircumInch As Boolean = False
    Enum HtFlag
        FtInch = 0
        Inch = 1
        Centimeter = 2
    End Enum
    Dim _HeightFlag As HtFlag

    Dim _Validate As Boolean = True ''This will stop validating event of all textbox controls when false.
    Dim _FocusControl As Control ''It will save textbox control which is to be focused after warning message.


    Private _IsRecordModified As Boolean = False
    Private _IsRecordsLoading As Boolean = True
    Private _IsSaveClicked As Boolean = False

    Dim WithEvents m_oDevices As WADevices

    ''declare a WADeviceData object. it is used to retrieve vitals parameter from device
    Dim m_oDeviceData As WADeviceData
    ''End of changes by Abhijeet on 20110318 for declaring objects used to capture vitals from WelchAllyn Device.

    ''declared this object at form level by Abhijeet on 20110322
    Dim SelectedVitals() As String
    ''End of declaring this object at form level by Abhijeet on 20110322

    Dim oclsDAS As clsDAS = Nothing
    Public Shared blnOpenFromExamForDAS As Boolean = False

#Region " Form Design Controls "

    Dim dv As DataView
    Private objclsPatientVitals As New clsPatientVitals
    Private objclsOBVitals As New ClsOBVitals

    Public myCaller As frmPatientExam
    'variable added by dipak 20090919 for use to call GetdataFromOtherForms() of calling form in frmPatientvitals
    Public myCaller1 As Object
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblVisitDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents tblStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Ok_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtWeightChanged As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtTHRperMax As System.Windows.Forms.TextBox
    Friend WithEvents txtTHRperMin As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblTHRPerc As System.Windows.Forms.Label
    Friend WithEvents txtTHRMax As System.Windows.Forms.TextBox
    Friend WithEvents lblTHR As System.Windows.Forms.Label
    Friend WithEvents txtTHRMin As System.Windows.Forms.TextBox
    Friend WithEvents txtTHR As System.Windows.Forms.TextBox
    Friend WithEvents txtHtInCm As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents label56 As System.Windows.Forms.Label
    Friend WithEvents label55 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtWtOz As System.Windows.Forms.TextBox
    Friend WithEvents txtWtLbs As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureCelcius As System.Windows.Forms.TextBox
    Friend WithEvents txtCircumInch As System.Windows.Forms.TextBox
    Friend WithEvents label54 As System.Windows.Forms.Label
    Friend WithEvents label53 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents label52 As System.Windows.Forms.Label
    Friend WithEvents label51 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents label36 As System.Windows.Forms.Label
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents label34 As System.Windows.Forms.Label
    Private WithEvents label33 As System.Windows.Forms.Label
    Private WithEvents label32 As System.Windows.Forms.Label
    Private WithEvents label31 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents trbPainLevel As System.Windows.Forms.TrackBar
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents label49 As System.Windows.Forms.Label
    Friend WithEvents label48 As System.Windows.Forms.Label
    Friend WithEvents label47 As System.Windows.Forms.Label
    Friend WithEvents txtPEFR3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPEFR2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPEFR1 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtStatureinInch As System.Windows.Forms.TextBox
    Friend WithEvents txtHtInInches As System.Windows.Forms.TextBox
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents tblbtn_Validate_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents dtLastmenstrualperiod As System.Windows.Forms.DateTimePicker
    Friend WithEvents FLpnlHeight As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutpnlMain As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlWeight As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlVisitdate As System.Windows.Forms.Panel
    Friend WithEvents FLpnlBMI As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlFt As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPnlCm As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlInch As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlwtChanged As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnllbsoz As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnllbs As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlKg As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlRespiratoryrate As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Flpnlpulspermin As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlBPSitting As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPnlTemp As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlTempfarenht As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPnlTempcelcius As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlPEFR As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPnHClInch As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlHeadcircumference As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlLastmenstrualperiod As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlStatureInch As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlStature As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlHCCm As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlStatureCm As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlHeartrate As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlPainlevel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlcomments As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLpnlneckcircum As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblneckcircum As System.Windows.Forms.Label
    Friend WithEvents FLpnlneckcircumInch As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtneckcircumInch As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FLneckcircumCm As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtneckcircumCm As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtlefteyepressure As System.Windows.Forms.TextBox
    Friend WithEvents Flpnllefteyepressure As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents FlpnlRighteyepressure As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents txtRighteyepressure As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents tblpnlMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FLPnlBPSetting As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FLPnlBPStanding As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtBPStandingMax As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtBPStandingMin As System.Windows.Forms.TextBox
    Friend WithEvents label50 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents FLPnlSiteforBP As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents cmbSiteforBP As System.Windows.Forms.ComboBox
    Friend WithEvents lblHeight1 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel4 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel6 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel7 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel8 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel9 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel12 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel11 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel10 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents tblCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lblbminor As System.Windows.Forms.Label
    Public blnOpenFromExam As Boolean = False

    '' Start New 
    ''Flow Pannel
    Friend WithEvents FlowLayoutPanel17 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel18 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel19 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel24 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel21 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel15 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel14 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel23 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel25 As System.Windows.Forms.FlowLayoutPanel

    ''label
    Friend WithEvents lblrsprt As System.Windows.Forms.Label
    ''Under Pannel18 
    Friend WithEvents lbltc As System.Windows.Forms.Label
    Friend WithEvents lbltmpf As System.Windows.Forms.Label
    ''Under Pannel19
    Friend WithEvents lblstin As System.Windows.Forms.Label

    Friend WithEvents lblppm As System.Windows.Forms.Label

    ''Under Pannel21
    Friend WithEvents lblhcin As System.Windows.Forms.Label
    Friend WithEvents lblhccm As System.Windows.Forms.Label

    ''Under Pannel15
    Friend WithEvents lblbpsidia As System.Windows.Forms.Label
    Friend WithEvents lblbpsisys As System.Windows.Forms.Label

    ''Under Pannel14
    Friend WithEvents lblbpstsys As System.Windows.Forms.Label
    Friend WithEvents lblbpstdia As System.Windows.Forms.Label

    ''Under Pannel23
    Friend WithEvents lblhin As System.Windows.Forms.Label


    ''Under Pannel25
    Friend WithEvents lblwt As System.Windows.Forms.Label
    Friend WithEvents lblwoz As System.Windows.Forms.Label
    Friend WithEvents lblwkg As System.Windows.Forms.Label
    Friend WithEvents lblstcm As System.Windows.Forms.Label
    Friend WithEvents lblhcm As System.Windows.Forms.Label

    Friend WithEvents lblPEFR As System.Windows.Forms.Label

    ''checkBox For Pain Level
    Friend WithEvents chkpainlvl As System.Windows.Forms.CheckBox
    Friend WithEvents tblbtn_BMI_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_CALBMI_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents FlPnlWithMedication As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents chkPainWithMedication As System.Windows.Forms.CheckBox
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Private WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Private WithEvents trbPainWithMedication As System.Windows.Forms.TrackBar
    Friend WithEvents FlPnlWithoutMedication As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents chkPainWithoutMedication As System.Windows.Forms.CheckBox
    Private WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Private WithEvents Label119 As System.Windows.Forms.Label
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents trbPainWithoutMedication As System.Windows.Forms.TrackBar
    Friend WithEvents FlPnlWorst As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents chkPainWorst As System.Windows.Forms.CheckBox
    Private WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Private WithEvents Label124 As System.Windows.Forms.Label
    Private WithEvents Label125 As System.Windows.Forms.Label
    Private WithEvents Label126 As System.Windows.Forms.Label
    Private WithEvents Label127 As System.Windows.Forms.Label
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Private WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Private WithEvents Label132 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Private WithEvents Label142 As System.Windows.Forms.Label
    Private WithEvents trbPainWorst As System.Windows.Forms.TrackBar
    Friend WithEvents tblbtn_ODI_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents FlPnlODI As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel16 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtODI As System.Windows.Forms.TextBox
    Friend WithEvents Label144 As System.Windows.Forms.Label
    Friend WithEvents tblbtn_Capture_DeviceVitals As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_DAS As System.Windows.Forms.ToolStripButton
    Friend WithEvents FLPnlDAS28 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel22 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtDAS28 As System.Windows.Forms.TextBox
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents tblbtn_OBVitals As System.Windows.Forms.ToolStripButton
    Friend WithEvents FLPnlPulseOx As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel20 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel5 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtPulseOX As System.Windows.Forms.TextBox
    Friend WithEvents lblpox As System.Windows.Forms.Label
    Friend WithEvents FLPnlSupplement As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel26 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtSupplement As System.Windows.Forms.TextBox
    Friend WithEvents FlowLayoutPanel27 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents txtPulseRate As System.Windows.Forms.TextBox
    Friend WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents lblSupplement As System.Windows.Forms.Label
    Friend WithEvents pnlWait As System.Windows.Forms.Panel
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents TLPanOB As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlEctopic As System.Windows.Forms.Panel
    Friend WithEvents txtEctopic As System.Windows.Forms.TextBox
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents pnlAborted_Induced As System.Windows.Forms.Panel
    Friend WithEvents txtAbortedinduced As System.Windows.Forms.TextBox
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents pnlMultipleBirth As System.Windows.Forms.Panel
    Friend WithEvents txtMultipleBirth As System.Windows.Forms.TextBox
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents pnlPremature As System.Windows.Forms.Panel
    Friend WithEvents txtPremature As System.Windows.Forms.TextBox
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents pnlFullTerm As System.Windows.Forms.Panel
    Friend WithEvents txtFullTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents pnlLiving As System.Windows.Forms.Panel
    Friend WithEvents txtLiving As System.Windows.Forms.TextBox
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPreganancy As System.Windows.Forms.TextBox
    Friend WithEvents pnlAborted_Spontaneous As System.Windows.Forms.Panel
    Friend WithEvents txtAbortedSpontaneous As System.Windows.Forms.TextBox
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents pnlTotalPregnancies As System.Windows.Forms.Panel
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Private WithEvents Label136 As System.Windows.Forms.Label
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Private WithEvents Label140 As System.Windows.Forms.Label
    Private WithEvents Label141 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label92 As System.Windows.Forms.Label
    Private WithEvents Label93 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents lblBMIPercentile As System.Windows.Forms.Label
    Friend WithEvents txtBMIPercentile As System.Windows.Forms.TextBox
    Friend WithEvents chkpain As System.Windows.Forms.CheckBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents AxAdvChart As AxGROWTHCHARTLib.AxGrowthChart
    '' End New
    'Public VWlist As New frmVWPatientVitals
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal VitalID As Long, ByVal PatientID As Long, Optional ByVal IsMakeAsCurrent As Boolean = False, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()
        '' when Open From Main Menu
        _VitalID = VitalID
        _VisitDate = Now
        _VisitID = 0
        _PatientID = PatientID
        '' If the Vital is opened for the Make as Current 
        _IsMakeAsCurrent = IsMakeAsCurrent

        '' If Vital Already opened by Other User then do not allow to save it
        _blnRecordLock = blnRecordLock
        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    Public Sub New(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long)
        MyBase.New()
        _VisitID = VisitID
        _VisitDate = Convert.ToDateTime(VisitDate & " " & Format(Now, "hh:mm:ss tt"))
        _PatientID = PatientID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub
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
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If (IsNothing(objclsOBVitals) = False) Then
                objclsOBVitals.Dispose()
                objclsOBVitals = Nothing
            End If
            If (IsNothing(objclsPatientVitals) = False) Then
                objclsPatientVitals.Dispose()
                objclsPatientVitals = Nothing
            End If
            Try
                If (IsNothing(_hashOBVital) = False) Then
                    _hashOBVital.Clear()
                    _hashOBVital = Nothing
                End If
            Catch ex As Exception

            End Try
            Try

                If IsNothing(gloUC_PatientStrip1) = False Then
                    If (Me.Controls.Contains(gloUC_PatientStrip1)) Then
                        Me.Controls.Remove(gloUC_PatientStrip1)
                    End If
                End If
            Catch ex As Exception

            End Try
            Try
                If IsNothing(gloUC_PatientStrip1) = False Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch ex As Exception

            End Try
            Dim dtpControls As DateTimePicker() = {dtLastmenstrualperiod, dtVitals}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
            Catch ex As Exception

            End Try



            CloseOBMedCatList()


        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lbltemp As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtWeightlbs As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightKg As System.Windows.Forms.TextBox
    Friend WithEvents txtBMI As System.Windows.Forms.TextBox
    Friend WithEvents txtTemperature As System.Windows.Forms.TextBox
    Friend WithEvents txtRespiratory As System.Windows.Forms.TextBox
    Friend WithEvents txtPulsePerMinute As System.Windows.Forms.TextBox
    Friend WithEvents txtBPSittingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPSittingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents pnlVitalEntry As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dtVitals As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtft As System.Windows.Forms.TextBox
    Friend WithEvents txtInch As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblCircum As System.Windows.Forms.Label
    Friend WithEvents txtCircum As System.Windows.Forms.TextBox
    Friend WithEvents txtStature As System.Windows.Forms.TextBox
    Friend WithEvents lblStature As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientVitals))
        Me.pnlVitalEntry = New System.Windows.Forms.Panel()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblVisitDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.lblPatient = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.FlowLayoutpnlMain = New System.Windows.Forms.FlowLayoutPanel()
        Me.FLPnlPulseOx = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel20 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel5 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtPulseOX = New System.Windows.Forms.TextBox()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.lblpox = New System.Windows.Forms.Label()
        Me.FLPnlSupplement = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel26 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtSupplement = New System.Windows.Forms.TextBox()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel27 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtPulseRate = New System.Windows.Forms.TextBox()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.lblSupplement = New System.Windows.Forms.Label()
        Me.FLpnlPainlevel = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.chkpain = New System.Windows.Forms.CheckBox()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.label36 = New System.Windows.Forms.Label()
        Me.label35 = New System.Windows.Forms.Label()
        Me.label34 = New System.Windows.Forms.Label()
        Me.label33 = New System.Windows.Forms.Label()
        Me.label32 = New System.Windows.Forms.Label()
        Me.label31 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.trbPainLevel = New System.Windows.Forms.TrackBar()
        Me.FlPnlWithoutMedication = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.chkPainWithoutMedication = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.trbPainWithoutMedication = New System.Windows.Forms.TrackBar()
        Me.FLpnlWeight = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.FLpnllbsoz = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtWtLbs = New System.Windows.Forms.TextBox()
        Me.txtWtOz = New System.Windows.Forms.TextBox()
        Me.label55 = New System.Windows.Forms.Label()
        Me.label56 = New System.Windows.Forms.Label()
        Me.FLpnllbs = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtWeightlbs = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.FLpnlKg = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtWeightKg = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel25 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblwt = New System.Windows.Forms.Label()
        Me.lblwkg = New System.Windows.Forms.Label()
        Me.lblwoz = New System.Windows.Forms.Label()
        Me.tblpnlMain = New System.Windows.Forms.TableLayoutPanel()
        Me.FLPnlSiteforBP = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.cmbSiteforBP = New System.Windows.Forms.ComboBox()
        Me.FLpnlcomments = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.FLpnlHeartrate = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.txtTHR = New System.Windows.Forms.TextBox()
        Me.lblTHR = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtTHRperMax = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtTHRperMin = New System.Windows.Forms.TextBox()
        Me.lblTHRPerc = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.txtTHRMax = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTHRMin = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.FlPnlWorst = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.chkPainWorst = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.trbPainWorst = New System.Windows.Forms.TrackBar()
        Me.FLpnlBMI = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblbminor = New System.Windows.Forms.Label()
        Me.txtBMI = New System.Windows.Forms.TextBox()
        Me.lblBMIPercentile = New System.Windows.Forms.Label()
        Me.txtBMIPercentile = New System.Windows.Forms.TextBox()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.FLpnlRespiratoryrate = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel9 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtRespiratory = New System.Windows.Forms.TextBox()
        Me.FlowLayoutPanel17 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblrsprt = New System.Windows.Forms.Label()
        Me.FLPnlBPStanding = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtBPStandingMax = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtBPStandingMin = New System.Windows.Forms.TextBox()
        Me.label50 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel14 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblbpstsys = New System.Windows.Forms.Label()
        Me.lblbpstdia = New System.Windows.Forms.Label()
        Me.FLpnlBPSitting = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtBPSittingMax = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBPSittingMin = New System.Windows.Forms.TextBox()
        Me.label52 = New System.Windows.Forms.Label()
        Me.label51 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel15 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblbpsisys = New System.Windows.Forms.Label()
        Me.lblbpsidia = New System.Windows.Forms.Label()
        Me.FlpnlRighteyepressure = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel7 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtRighteyepressure = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.FLpnlLastmenstrualperiod = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel12 = New System.Windows.Forms.FlowLayoutPanel()
        Me.dtLastmenstrualperiod = New System.Windows.Forms.DateTimePicker()
        Me.FLpnlPEFR = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel8 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtPEFR1 = New System.Windows.Forms.TextBox()
        Me.lblPEFR = New System.Windows.Forms.Label()
        Me.FLpnlwtChanged = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel11 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtWeightChanged = New System.Windows.Forms.TextBox()
        Me.FLpnlneckcircum = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblneckcircum = New System.Windows.Forms.Label()
        Me.FLpnlneckcircumInch = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtneckcircumInch = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FLneckcircumCm = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtneckcircumCm = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Flpnlpulspermin = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel10 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtPulsePerMinute = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel24 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblppm = New System.Windows.Forms.Label()
        Me.FLpnlStature = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblStature = New System.Windows.Forms.Label()
        Me.FLpnlStatureInch = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtStatureinInch = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.FLpnlStatureCm = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtStature = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel19 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblstin = New System.Windows.Forms.Label()
        Me.lblstcm = New System.Windows.Forms.Label()
        Me.Flpnllefteyepressure = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel6 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtlefteyepressure = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.FLPnlTemp = New System.Windows.Forms.FlowLayoutPanel()
        Me.lbltemp = New System.Windows.Forms.Label()
        Me.FLpnlTempfarenht = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtTemperature = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.FLPnlTempcelcius = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtTemperatureCelcius = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel18 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lbltmpf = New System.Windows.Forms.Label()
        Me.lbltc = New System.Windows.Forms.Label()
        Me.FLpnlHeadcircumference = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCircum = New System.Windows.Forms.Label()
        Me.FLPnHClInch = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtCircumInch = New System.Windows.Forms.TextBox()
        Me.label53 = New System.Windows.Forms.Label()
        Me.FLpnlHCCm = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtCircum = New System.Windows.Forms.TextBox()
        Me.label54 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel21 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblhcin = New System.Windows.Forms.Label()
        Me.lblhccm = New System.Windows.Forms.Label()
        Me.FLPnlDAS28 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel22 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtDAS28 = New System.Windows.Forms.TextBox()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.FlPnlODI = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel16 = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtODI = New System.Windows.Forms.TextBox()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.FlPnlWithMedication = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.chkPainWithMedication = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.trbPainWithMedication = New System.Windows.Forms.TrackBar()
        Me.FLpnlHeight = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblHeight1 = New System.Windows.Forms.Label()
        Me.FLpnlFt = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtft = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtInch = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FLpnlInch = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtHtInInches = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.FLPnlCm = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtHtInCm = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel23 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblhin = New System.Windows.Forms.Label()
        Me.lblhcm = New System.Windows.Forms.Label()
        Me.TLPanOB = New System.Windows.Forms.Panel()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.pnlLiving = New System.Windows.Forms.Panel()
        Me.txtLiving = New System.Windows.Forms.TextBox()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.pnlEctopic = New System.Windows.Forms.Panel()
        Me.txtEctopic = New System.Windows.Forms.TextBox()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.pnlAborted_Induced = New System.Windows.Forms.Panel()
        Me.txtAbortedinduced = New System.Windows.Forms.TextBox()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.pnlFullTerm = New System.Windows.Forms.Panel()
        Me.txtFullTerm = New System.Windows.Forms.TextBox()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.pnlMultipleBirth = New System.Windows.Forms.Panel()
        Me.txtMultipleBirth = New System.Windows.Forms.TextBox()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.pnlAborted_Spontaneous = New System.Windows.Forms.Panel()
        Me.txtAbortedSpontaneous = New System.Windows.Forms.TextBox()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.pnlPremature = New System.Windows.Forms.Panel()
        Me.txtPremature = New System.Windows.Forms.TextBox()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.pnlTotalPregnancies = New System.Windows.Forms.Panel()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.txtTotalPreganancy = New System.Windows.Forms.TextBox()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.pnlVisitdate = New System.Windows.Forms.Panel()
        Me.dtVitals = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.FLPnlBPSetting = New System.Windows.Forms.FlowLayoutPanel()
        Me.label49 = New System.Windows.Forms.Label()
        Me.label48 = New System.Windows.Forms.Label()
        Me.label47 = New System.Windows.Forms.Label()
        Me.txtPEFR3 = New System.Windows.Forms.TextBox()
        Me.txtPEFR2 = New System.Windows.Forms.TextBox()
        Me.tblStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Validate_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblCCD = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_CALBMI_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_ODI_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_DAS = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Capture_DeviceVitals = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_OBVitals = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Ok_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkpainlvl = New System.Windows.Forms.CheckBox()
        Me.pnlWait = New System.Windows.Forms.Panel()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.AxAdvChart = New AxGROWTHCHARTLib.AxGrowthChart()
        Me.pnlVitalEntry.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.FLPnlPulseOx.SuspendLayout()
        Me.FlowLayoutPanel20.SuspendLayout()
        Me.FlowLayoutPanel5.SuspendLayout()
        Me.FLPnlSupplement.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.FlowLayoutPanel26.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.FlowLayoutPanel27.SuspendLayout()
        Me.FLpnlPainlevel.SuspendLayout()
        Me.panel2.SuspendLayout()
        CType(Me.trbPainLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlPnlWithoutMedication.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.trbPainWithoutMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FLpnlWeight.SuspendLayout()
        Me.FLpnllbsoz.SuspendLayout()
        Me.FLpnllbs.SuspendLayout()
        Me.FLpnlKg.SuspendLayout()
        Me.FlowLayoutPanel25.SuspendLayout()
        Me.tblpnlMain.SuspendLayout()
        Me.FLPnlSiteforBP.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FLpnlcomments.SuspendLayout()
        Me.FLpnlHeartrate.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.FlPnlWorst.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.trbPainWorst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FLpnlBMI.SuspendLayout()
        Me.FlowLayoutPanel4.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.FLpnlRespiratoryrate.SuspendLayout()
        Me.FlowLayoutPanel9.SuspendLayout()
        Me.FlowLayoutPanel17.SuspendLayout()
        Me.FLPnlBPStanding.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel14.SuspendLayout()
        Me.FLpnlBPSitting.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.FlowLayoutPanel15.SuspendLayout()
        Me.FlpnlRighteyepressure.SuspendLayout()
        Me.FlowLayoutPanel7.SuspendLayout()
        Me.FLpnlLastmenstrualperiod.SuspendLayout()
        Me.FlowLayoutPanel12.SuspendLayout()
        Me.FLpnlPEFR.SuspendLayout()
        Me.FlowLayoutPanel8.SuspendLayout()
        Me.FLpnlwtChanged.SuspendLayout()
        Me.FlowLayoutPanel11.SuspendLayout()
        Me.FLpnlneckcircum.SuspendLayout()
        Me.FLpnlneckcircumInch.SuspendLayout()
        Me.FLneckcircumCm.SuspendLayout()
        Me.Flpnlpulspermin.SuspendLayout()
        Me.FlowLayoutPanel10.SuspendLayout()
        Me.FlowLayoutPanel24.SuspendLayout()
        Me.FLpnlStature.SuspendLayout()
        Me.FLpnlStatureInch.SuspendLayout()
        Me.FLpnlStatureCm.SuspendLayout()
        Me.FlowLayoutPanel19.SuspendLayout()
        Me.Flpnllefteyepressure.SuspendLayout()
        Me.FlowLayoutPanel6.SuspendLayout()
        Me.FLPnlTemp.SuspendLayout()
        Me.FLpnlTempfarenht.SuspendLayout()
        Me.FLPnlTempcelcius.SuspendLayout()
        Me.FlowLayoutPanel18.SuspendLayout()
        Me.FLpnlHeadcircumference.SuspendLayout()
        Me.FLPnHClInch.SuspendLayout()
        Me.FLpnlHCCm.SuspendLayout()
        Me.FlowLayoutPanel21.SuspendLayout()
        Me.FLPnlDAS28.SuspendLayout()
        Me.FlowLayoutPanel22.SuspendLayout()
        Me.FlPnlODI.SuspendLayout()
        Me.FlowLayoutPanel16.SuspendLayout()
        Me.FlPnlWithMedication.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.trbPainWithMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FLpnlHeight.SuspendLayout()
        Me.FLpnlFt.SuspendLayout()
        Me.FLpnlInch.SuspendLayout()
        Me.FLPnlCm.SuspendLayout()
        Me.FlowLayoutPanel23.SuspendLayout()
        Me.TLPanOB.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnlLiving.SuspendLayout()
        Me.pnlEctopic.SuspendLayout()
        Me.pnlAborted_Induced.SuspendLayout()
        Me.pnlFullTerm.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.pnlMultipleBirth.SuspendLayout()
        Me.pnlAborted_Spontaneous.SuspendLayout()
        Me.pnlPremature.SuspendLayout()
        Me.pnlTotalPregnancies.SuspendLayout()
        Me.pnlVisitdate.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlWait.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.AxAdvChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlVitalEntry
        '
        Me.pnlVitalEntry.AutoScroll = True
        Me.pnlVitalEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlVitalEntry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitalEntry.Controls.Add(Me.Label67)
        Me.pnlVitalEntry.Controls.Add(Me.Label68)
        Me.pnlVitalEntry.Controls.Add(Me.Label69)
        Me.pnlVitalEntry.Controls.Add(Me.Label70)
        Me.pnlVitalEntry.Controls.Add(Me.groupBox1)
        Me.pnlVitalEntry.Controls.Add(Me.FlowLayoutpnlMain)
        Me.pnlVitalEntry.Location = New System.Drawing.Point(0, 56)
        Me.pnlVitalEntry.Name = "pnlVitalEntry"
        Me.pnlVitalEntry.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlVitalEntry.Size = New System.Drawing.Size(0, 0)
        Me.pnlVitalEntry.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label67.Location = New System.Drawing.Point(4, 1532)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(883, 1)
        Me.Label67.TabIndex = 0
        Me.Label67.Text = "label2"
        Me.Label67.Visible = False
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(3, 1535)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 0)
        Me.Label68.TabIndex = 0
        Me.Label68.Text = "label4"
        Me.Label68.Visible = False
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label69.Location = New System.Drawing.Point(887, 1535)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 0)
        Me.Label69.TabIndex = 0
        Me.Label69.Text = "label3"
        Me.Label69.Visible = False
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(3, 1534)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(885, 1)
        Me.Label70.TabIndex = 0
        Me.Label70.Text = "label1"
        Me.Label70.Visible = False
        '
        'groupBox1
        '
        Me.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.groupBox1.BackColor = System.Drawing.Color.Transparent
        Me.groupBox1.Controls.Add(Me.lblVisitDate)
        Me.groupBox1.Controls.Add(Me.Label4)
        Me.groupBox1.Controls.Add(Me.lblPatientName)
        Me.groupBox1.Controls.Add(Me.lblPatient)
        Me.groupBox1.Controls.Add(Me.Label1)
        Me.groupBox1.Controls.Add(Me.lblPatientCode)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBox1.Location = New System.Drawing.Point(3, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(885, 1531)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = False
        '
        'lblVisitDate
        '
        Me.lblVisitDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitDate.Location = New System.Drawing.Point(97, -21)
        Me.lblVisitDate.Name = "lblVisitDate"
        Me.lblVisitDate.Size = New System.Drawing.Size(80, 16)
        Me.lblVisitDate.TabIndex = 0
        Me.lblVisitDate.Text = "08/29/2005"
        Me.lblVisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVisitDate.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, -21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Visit Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label4.Visible = False
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(102, -37)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(72, 15)
        Me.lblPatientName.TabIndex = 0
        Me.lblPatientName.Text = "Mike Dodge"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatientName.Visible = False
        '
        'lblPatient
        '
        Me.lblPatient.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatient.Location = New System.Drawing.Point(6, -37)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Size = New System.Drawing.Size(96, 16)
        Me.lblPatient.TabIndex = 0
        Me.lblPatient.Text = "Patient Name :"
        Me.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPatient.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, -53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Patient Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label1.Visible = False
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.Location = New System.Drawing.Point(113, -53)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(35, 15)
        Me.lblPatientCode.TabIndex = 0
        Me.lblPatientCode.Text = "1001"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatientCode.Visible = False
        '
        'FlowLayoutpnlMain
        '
        Me.FlowLayoutpnlMain.Location = New System.Drawing.Point(2, 603)
        Me.FlowLayoutpnlMain.Name = "FlowLayoutpnlMain"
        Me.FlowLayoutpnlMain.Size = New System.Drawing.Size(888, 12)
        Me.FlowLayoutpnlMain.TabIndex = 0
        Me.FlowLayoutpnlMain.Visible = False
        '
        'FLPnlPulseOx
        '
        Me.FLPnlPulseOx.AutoSize = True
        Me.FLPnlPulseOx.Controls.Add(Me.FlowLayoutPanel20)
        Me.FLPnlPulseOx.Controls.Add(Me.FLPnlSupplement)
        Me.FLPnlPulseOx.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlPulseOx.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLPnlPulseOx.Location = New System.Drawing.Point(3, 714)
        Me.FLPnlPulseOx.Name = "FLPnlPulseOx"
        Me.FLPnlPulseOx.Size = New System.Drawing.Size(577, 98)
        Me.FLPnlPulseOx.TabIndex = 1
        Me.FLPnlPulseOx.Visible = False
        '
        'FlowLayoutPanel20
        '
        Me.FlowLayoutPanel20.Controls.Add(Me.Label12)
        Me.FlowLayoutPanel20.Controls.Add(Me.FlowLayoutPanel5)
        Me.FlowLayoutPanel20.Controls.Add(Me.Label148)
        Me.FlowLayoutPanel20.Controls.Add(Me.Label151)
        Me.FlowLayoutPanel20.Controls.Add(Me.lblpox)
        Me.FlowLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel20.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel20.Name = "FlowLayoutPanel20"
        Me.FlowLayoutPanel20.Size = New System.Drawing.Size(447, 35)
        Me.FlowLayoutPanel20.TabIndex = 4
        Me.FlowLayoutPanel20.TabStop = True
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(3, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(137, 31)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Pulse Ox :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label12, "Pulse Oximetry")
        '
        'FlowLayoutPanel5
        '
        Me.FlowLayoutPanel5.Controls.Add(Me.txtPulseOX)
        Me.FlowLayoutPanel5.Location = New System.Drawing.Point(146, 3)
        Me.FlowLayoutPanel5.Name = "FlowLayoutPanel5"
        Me.FlowLayoutPanel5.Size = New System.Drawing.Size(50, 25)
        Me.FlowLayoutPanel5.TabIndex = 0
        '
        'txtPulseOX
        '
        Me.txtPulseOX.ForeColor = System.Drawing.Color.Black
        Me.txtPulseOX.Location = New System.Drawing.Point(3, 3)
        Me.txtPulseOX.MaxLength = 5
        Me.txtPulseOX.Name = "txtPulseOX"
        Me.txtPulseOX.Size = New System.Drawing.Size(45, 22)
        Me.txtPulseOX.TabIndex = 0
        Me.txtPulseOX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label148
        '
        Me.Label148.Location = New System.Drawing.Point(202, 0)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(16, 31)
        Me.Label148.TabIndex = 3
        Me.Label148.Text = "%"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label151
        '
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label151.Location = New System.Drawing.Point(224, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(72, 31)
        Me.Label151.TabIndex = 4
        Me.Label151.Text = "(Room air)"
        Me.Label151.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblpox
        '
        Me.lblpox.AutoSize = True
        Me.lblpox.BackColor = System.Drawing.Color.Transparent
        Me.lblpox.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblpox.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpox.ForeColor = System.Drawing.Color.Red
        Me.lblpox.Location = New System.Drawing.Point(302, 0)
        Me.lblpox.Name = "lblpox"
        Me.lblpox.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.lblpox.Size = New System.Drawing.Size(43, 31)
        Me.lblpox.TabIndex = 0
        Me.lblpox.Text = "(Systolic)"
        Me.lblpox.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLPnlSupplement
        '
        Me.FLPnlSupplement.AutoSize = True
        Me.FLPnlSupplement.BackColor = System.Drawing.Color.Transparent
        Me.FLPnlSupplement.Controls.Add(Me.Panel7)
        Me.FLPnlSupplement.Controls.Add(Me.FlowLayoutPanel26)
        Me.FLPnlSupplement.Controls.Add(Me.Label152)
        Me.FLPnlSupplement.Controls.Add(Me.Panel6)
        Me.FLPnlSupplement.Controls.Add(Me.FlowLayoutPanel27)
        Me.FLPnlSupplement.Controls.Add(Me.Label150)
        Me.FLPnlSupplement.Controls.Add(Me.lblSupplement)
        Me.FLPnlSupplement.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlSupplement.Location = New System.Drawing.Point(3, 44)
        Me.FLPnlSupplement.Name = "FLPnlSupplement"
        Me.FLPnlSupplement.Size = New System.Drawing.Size(447, 51)
        Me.FLPnlSupplement.TabIndex = 5
        Me.FLPnlSupplement.TabStop = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label147)
        Me.Panel7.Controls.Add(Me.Label157)
        Me.Panel7.Controls.Add(Me.Label156)
        Me.Panel7.Controls.Add(Me.Label149)
        Me.Panel7.Location = New System.Drawing.Point(3, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(137, 28)
        Me.Panel7.TabIndex = 3
        '
        'Label147
        '
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label147.Location = New System.Drawing.Point(23, 0)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(81, 28)
        Me.Label147.TabIndex = 0
        Me.Label147.Text = "Pulse Ox on"
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label147, "Pulse Oximetry on Oxygen")
        '
        'Label157
        '
        Me.Label157.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label157.Location = New System.Drawing.Point(104, 0)
        Me.Label157.Name = "Label157"
        Me.Label157.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label157.Size = New System.Drawing.Size(12, 28)
        Me.Label157.TabIndex = 2
        Me.Label157.Text = "O"
        Me.ToolTip1.SetToolTip(Me.Label157, "Pulse Oximetry on Oxygen")
        '
        'Label156
        '
        Me.Label156.AutoSize = True
        Me.Label156.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.Location = New System.Drawing.Point(116, 0)
        Me.Label156.Name = "Label156"
        Me.Label156.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label156.Size = New System.Drawing.Size(10, 21)
        Me.Label156.TabIndex = 3
        Me.Label156.Text = "2"
        Me.Label156.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.Label156, "Pulse Oximetry on Oxygen")
        '
        'Label149
        '
        Me.Label149.AutoSize = True
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label149.Location = New System.Drawing.Point(126, 0)
        Me.Label149.Name = "Label149"
        Me.Label149.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.Label149.Size = New System.Drawing.Size(11, 22)
        Me.Label149.TabIndex = 4
        Me.Label149.Text = ":"
        '
        'FlowLayoutPanel26
        '
        Me.FlowLayoutPanel26.Controls.Add(Me.txtSupplement)
        Me.FlowLayoutPanel26.Location = New System.Drawing.Point(146, 3)
        Me.FlowLayoutPanel26.Name = "FlowLayoutPanel26"
        Me.FlowLayoutPanel26.Size = New System.Drawing.Size(50, 45)
        Me.FlowLayoutPanel26.TabIndex = 0
        '
        'txtSupplement
        '
        Me.txtSupplement.ForeColor = System.Drawing.Color.Black
        Me.txtSupplement.Location = New System.Drawing.Point(3, 3)
        Me.txtSupplement.MaxLength = 5
        Me.txtSupplement.Name = "txtSupplement"
        Me.txtSupplement.Size = New System.Drawing.Size(45, 22)
        Me.txtSupplement.TabIndex = 0
        Me.txtSupplement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label152
        '
        Me.Label152.Location = New System.Drawing.Point(202, 0)
        Me.Label152.Name = "Label152"
        Me.Label152.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label152.Size = New System.Drawing.Size(16, 25)
        Me.Label152.TabIndex = 4
        Me.Label152.Text = "%"
        Me.Label152.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.Label155)
        Me.Panel6.Controls.Add(Me.Label154)
        Me.Panel6.Controls.Add(Me.Label153)
        Me.Panel6.Location = New System.Drawing.Point(224, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(62, 31)
        Me.Panel6.TabIndex = 2
        '
        'Label155
        '
        Me.Label155.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label155.Location = New System.Drawing.Point(22, 0)
        Me.Label155.Name = "Label155"
        Me.Label155.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Label155.Size = New System.Drawing.Size(46, 31)
        Me.Label155.TabIndex = 4
        Me.Label155.Text = "Rate :"
        Me.ToolTip1.SetToolTip(Me.Label155, "Oxygen Rate")
        '
        'Label154
        '
        Me.Label154.AutoSize = True
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.Location = New System.Drawing.Point(12, 0)
        Me.Label154.Name = "Label154"
        Me.Label154.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Label154.Size = New System.Drawing.Size(10, 21)
        Me.Label154.TabIndex = 3
        Me.Label154.Text = "2"
        Me.Label154.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.ToolTip1.SetToolTip(Me.Label154, "Oxygen Rate")
        '
        'Label153
        '
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.Location = New System.Drawing.Point(0, 0)
        Me.Label153.Name = "Label153"
        Me.Label153.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Label153.Size = New System.Drawing.Size(12, 31)
        Me.Label153.TabIndex = 2
        Me.Label153.Text = "O"
        Me.ToolTip1.SetToolTip(Me.Label153, "Oxygen Rate")
        '
        'FlowLayoutPanel27
        '
        Me.FlowLayoutPanel27.Controls.Add(Me.txtPulseRate)
        Me.FlowLayoutPanel27.Location = New System.Drawing.Point(292, 3)
        Me.FlowLayoutPanel27.Name = "FlowLayoutPanel27"
        Me.FlowLayoutPanel27.Size = New System.Drawing.Size(50, 25)
        Me.FlowLayoutPanel27.TabIndex = 2
        '
        'txtPulseRate
        '
        Me.txtPulseRate.ForeColor = System.Drawing.Color.Black
        Me.txtPulseRate.Location = New System.Drawing.Point(3, 3)
        Me.txtPulseRate.MaxLength = 5
        Me.txtPulseRate.Name = "txtPulseRate"
        Me.txtPulseRate.Size = New System.Drawing.Size(45, 22)
        Me.txtPulseRate.TabIndex = 0
        Me.txtPulseRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label150
        '
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label150.Location = New System.Drawing.Point(348, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(28, 34)
        Me.Label150.TabIndex = 3
        Me.Label150.Text = "lpm"
        Me.Label150.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSupplement
        '
        Me.lblSupplement.BackColor = System.Drawing.Color.Transparent
        Me.lblSupplement.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplement.Location = New System.Drawing.Point(382, 0)
        Me.lblSupplement.Name = "lblSupplement"
        Me.lblSupplement.Size = New System.Drawing.Size(62, 40)
        Me.lblSupplement.TabIndex = 6
        Me.lblSupplement.Text = "(Systolic)"
        Me.lblSupplement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FLpnlPainlevel
        '
        Me.FLpnlPainlevel.AutoSize = True
        Me.FLpnlPainlevel.Controls.Add(Me.Label58)
        Me.FLpnlPainlevel.Controls.Add(Me.chkpain)
        Me.FLpnlPainlevel.Controls.Add(Me.panel2)
        Me.FLpnlPainlevel.Location = New System.Drawing.Point(586, 339)
        Me.FLpnlPainlevel.Name = "FLpnlPainlevel"
        Me.FLpnlPainlevel.Size = New System.Drawing.Size(420, 74)
        Me.FLpnlPainlevel.TabIndex = 0
        Me.FLpnlPainlevel.Visible = False
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(3, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(114, 22)
        Me.Label58.TabIndex = 0
        Me.Label58.Text = "Pain level : Current"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkpain
        '
        Me.chkpain.Location = New System.Drawing.Point(123, 3)
        Me.chkpain.Name = "chkpain"
        Me.chkpain.Padding = New System.Windows.Forms.Padding(3)
        Me.chkpain.Size = New System.Drawing.Size(17, 14)
        Me.chkpain.TabIndex = 1
        Me.chkpain.Text = "Allow"
        Me.chkpain.UseVisualStyleBackColor = True
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panel2.Controls.Add(Me.Label136)
        Me.panel2.Controls.Add(Me.Label137)
        Me.panel2.Controls.Add(Me.Label138)
        Me.panel2.Controls.Add(Me.Label139)
        Me.panel2.Controls.Add(Me.Label140)
        Me.panel2.Controls.Add(Me.Label141)
        Me.panel2.Controls.Add(Me.Label63)
        Me.panel2.Controls.Add(Me.Label64)
        Me.panel2.Controls.Add(Me.Label65)
        Me.panel2.Controls.Add(Me.Label66)
        Me.panel2.Controls.Add(Me.label36)
        Me.panel2.Controls.Add(Me.label35)
        Me.panel2.Controls.Add(Me.label34)
        Me.panel2.Controls.Add(Me.label33)
        Me.panel2.Controls.Add(Me.label32)
        Me.panel2.Controls.Add(Me.label31)
        Me.panel2.Controls.Add(Me.Label37)
        Me.panel2.Controls.Add(Me.Label38)
        Me.panel2.Controls.Add(Me.Label39)
        Me.panel2.Controls.Add(Me.Label40)
        Me.panel2.Controls.Add(Me.Label57)
        Me.panel2.Controls.Add(Me.trbPainLevel)
        Me.panel2.Location = New System.Drawing.Point(146, 3)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(271, 68)
        Me.panel2.TabIndex = 0
        '
        'Label136
        '
        Me.Label136.AutoSize = True
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label136.Location = New System.Drawing.Point(242, 7)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(23, 11)
        Me.Label136.TabIndex = 10
        Me.Label136.Text = "Pain"
        '
        'Label137
        '
        Me.Label137.AutoSize = True
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label137.Location = New System.Drawing.Point(19, 7)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(23, 11)
        Me.Label137.TabIndex = 11
        Me.Label137.Text = "Pain"
        '
        'Label138
        '
        Me.Label138.AutoSize = True
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label138.Location = New System.Drawing.Point(130, 7)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(23, 11)
        Me.Label138.TabIndex = 12
        Me.Label138.Text = "Pain"
        '
        'Label139
        '
        Me.Label139.AutoSize = True
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.Location = New System.Drawing.Point(193, 7)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(52, 11)
        Me.Label139.TabIndex = 7
        Me.Label139.Text = "Unbearable"
        '
        'Label140
        '
        Me.Label140.AutoSize = True
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.Location = New System.Drawing.Point(88, 7)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(44, 11)
        Me.Label140.TabIndex = 8
        Me.Label140.Text = "Moderate"
        '
        'Label141
        '
        Me.Label141.AutoSize = True
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.Location = New System.Drawing.Point(5, 7)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(17, 11)
        Me.Label141.TabIndex = 9
        Me.Label141.Text = "No"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(1, 67)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(269, 1)
        Me.Label63.TabIndex = 0
        Me.Label63.Text = "label2"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(0, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 67)
        Me.Label64.TabIndex = 0
        Me.Label64.Text = "label4"
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(270, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 67)
        Me.Label65.TabIndex = 0
        Me.Label65.Text = "label3"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(271, 1)
        Me.Label66.TabIndex = 0
        Me.Label66.Text = "label1"
        '
        'label36
        '
        Me.label36.AutoSize = True
        Me.label36.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.Location = New System.Drawing.Point(241, 50)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(19, 12)
        Me.label36.TabIndex = 0
        Me.label36.Text = "10"
        '
        'label35
        '
        Me.label35.AutoSize = True
        Me.label35.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label35.Location = New System.Drawing.Point(218, 50)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(12, 12)
        Me.label35.TabIndex = 0
        Me.label35.Text = "9"
        '
        'label34
        '
        Me.label34.AutoSize = True
        Me.label34.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label34.Location = New System.Drawing.Point(195, 50)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(12, 12)
        Me.label34.TabIndex = 0
        Me.label34.Text = "8"
        '
        'label33
        '
        Me.label33.AutoSize = True
        Me.label33.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label33.Location = New System.Drawing.Point(172, 50)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(12, 12)
        Me.label33.TabIndex = 0
        Me.label33.Text = "7"
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.Location = New System.Drawing.Point(149, 50)
        Me.label32.Name = "label32"
        Me.label32.Size = New System.Drawing.Size(12, 12)
        Me.label32.TabIndex = 0
        Me.label32.Text = "6"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.Location = New System.Drawing.Point(126, 50)
        Me.label31.Name = "label31"
        Me.label31.Size = New System.Drawing.Size(12, 12)
        Me.label31.TabIndex = 0
        Me.label31.Text = "5"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(103, 50)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(12, 12)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "4"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(80, 50)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(12, 12)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "3"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(57, 50)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(12, 12)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "2"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(34, 50)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(12, 12)
        Me.Label40.TabIndex = 0
        Me.Label40.Text = "1"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(11, 50)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(12, 12)
        Me.Label57.TabIndex = 0
        Me.Label57.Text = "0"
        '
        'trbPainLevel
        '
        Me.trbPainLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainLevel.LargeChange = 1
        Me.trbPainLevel.Location = New System.Drawing.Point(3, 20)
        Me.trbPainLevel.Name = "trbPainLevel"
        Me.trbPainLevel.Size = New System.Drawing.Size(258, 45)
        Me.trbPainLevel.TabIndex = 0
        '
        'FlPnlWithoutMedication
        '
        Me.FlPnlWithoutMedication.AutoSize = True
        Me.FlPnlWithoutMedication.Controls.Add(Me.Label99)
        Me.FlPnlWithoutMedication.Controls.Add(Me.chkPainWithoutMedication)
        Me.FlPnlWithoutMedication.Controls.Add(Me.Panel4)
        Me.FlPnlWithoutMedication.Location = New System.Drawing.Point(3, 97)
        Me.FlPnlWithoutMedication.Name = "FlPnlWithoutMedication"
        Me.FlPnlWithoutMedication.Size = New System.Drawing.Size(420, 74)
        Me.FlPnlWithoutMedication.TabIndex = 2
        Me.FlPnlWithoutMedication.Visible = False
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.Transparent
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.Location = New System.Drawing.Point(3, 0)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(114, 31)
        Me.Label99.TabIndex = 0
        Me.Label99.Text = "Pain level : Without Medication"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPainWithoutMedication
        '
        Me.chkPainWithoutMedication.Location = New System.Drawing.Point(123, 3)
        Me.chkPainWithoutMedication.Name = "chkPainWithoutMedication"
        Me.chkPainWithoutMedication.Padding = New System.Windows.Forms.Padding(3)
        Me.chkPainWithoutMedication.Size = New System.Drawing.Size(17, 14)
        Me.chkPainWithoutMedication.TabIndex = 1
        Me.chkPainWithoutMedication.Text = "Allow"
        Me.chkPainWithoutMedication.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label114)
        Me.Panel4.Controls.Add(Me.Label118)
        Me.Panel4.Controls.Add(Me.Label116)
        Me.Panel4.Controls.Add(Me.Label100)
        Me.Panel4.Controls.Add(Me.Label101)
        Me.Panel4.Controls.Add(Me.Label102)
        Me.Panel4.Controls.Add(Me.Label103)
        Me.Panel4.Controls.Add(Me.Label104)
        Me.Panel4.Controls.Add(Me.Label105)
        Me.Panel4.Controls.Add(Me.Label106)
        Me.Panel4.Controls.Add(Me.Label107)
        Me.Panel4.Controls.Add(Me.Label108)
        Me.Panel4.Controls.Add(Me.Label109)
        Me.Panel4.Controls.Add(Me.Label110)
        Me.Panel4.Controls.Add(Me.Label111)
        Me.Panel4.Controls.Add(Me.Label112)
        Me.Panel4.Controls.Add(Me.Label113)
        Me.Panel4.Controls.Add(Me.Label115)
        Me.Panel4.Controls.Add(Me.Label117)
        Me.Panel4.Controls.Add(Me.Label119)
        Me.Panel4.Controls.Add(Me.Label120)
        Me.Panel4.Controls.Add(Me.trbPainWithoutMedication)
        Me.Panel4.Location = New System.Drawing.Point(146, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(271, 68)
        Me.Panel4.TabIndex = 0
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(244, 6)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(23, 11)
        Me.Label114.TabIndex = 0
        Me.Label114.Text = "Pain"
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label118.Location = New System.Drawing.Point(21, 6)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(23, 11)
        Me.Label118.TabIndex = 0
        Me.Label118.Text = "Pain"
        '
        'Label116
        '
        Me.Label116.AutoSize = True
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label116.Location = New System.Drawing.Point(132, 6)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(23, 11)
        Me.Label116.TabIndex = 0
        Me.Label116.Text = "Pain"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label100.Location = New System.Drawing.Point(1, 67)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(269, 1)
        Me.Label100.TabIndex = 0
        Me.Label100.Text = "label2"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.Location = New System.Drawing.Point(0, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 67)
        Me.Label101.TabIndex = 0
        Me.Label101.Text = "label4"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label102.Location = New System.Drawing.Point(270, 1)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 67)
        Me.Label102.TabIndex = 0
        Me.Label102.Text = "label3"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.Location = New System.Drawing.Point(0, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(271, 1)
        Me.Label103.TabIndex = 0
        Me.Label103.Text = "label1"
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.Location = New System.Drawing.Point(241, 51)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(19, 12)
        Me.Label104.TabIndex = 0
        Me.Label104.Text = "10"
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.Location = New System.Drawing.Point(218, 51)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(12, 12)
        Me.Label105.TabIndex = 0
        Me.Label105.Text = "9"
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(195, 51)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(12, 12)
        Me.Label106.TabIndex = 0
        Me.Label106.Text = "8"
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.Location = New System.Drawing.Point(172, 51)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(12, 12)
        Me.Label107.TabIndex = 0
        Me.Label107.Text = "7"
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.Location = New System.Drawing.Point(149, 51)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(12, 12)
        Me.Label108.TabIndex = 0
        Me.Label108.Text = "6"
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.Location = New System.Drawing.Point(126, 51)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(12, 12)
        Me.Label109.TabIndex = 0
        Me.Label109.Text = "5"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.Location = New System.Drawing.Point(103, 51)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(12, 12)
        Me.Label110.TabIndex = 0
        Me.Label110.Text = "4"
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.Location = New System.Drawing.Point(80, 51)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(12, 12)
        Me.Label111.TabIndex = 0
        Me.Label111.Text = "3"
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(57, 51)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(12, 12)
        Me.Label112.TabIndex = 0
        Me.Label112.Text = "2"
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.Location = New System.Drawing.Point(34, 51)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(12, 12)
        Me.Label113.TabIndex = 0
        Me.Label113.Text = "1"
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.Location = New System.Drawing.Point(195, 6)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(52, 11)
        Me.Label115.TabIndex = 0
        Me.Label115.Text = "Unbearable"
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.Location = New System.Drawing.Point(90, 6)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(44, 11)
        Me.Label117.TabIndex = 0
        Me.Label117.Text = "Moderate"
        '
        'Label119
        '
        Me.Label119.AutoSize = True
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.Location = New System.Drawing.Point(7, 6)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(17, 11)
        Me.Label119.TabIndex = 0
        Me.Label119.Text = "No"
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.Location = New System.Drawing.Point(11, 51)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(12, 12)
        Me.Label120.TabIndex = 0
        Me.Label120.Text = "0"
        '
        'trbPainWithoutMedication
        '
        Me.trbPainWithoutMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainWithoutMedication.LargeChange = 1
        Me.trbPainWithoutMedication.Location = New System.Drawing.Point(3, 21)
        Me.trbPainWithoutMedication.Name = "trbPainWithoutMedication"
        Me.trbPainWithoutMedication.Size = New System.Drawing.Size(258, 45)
        Me.trbPainWithoutMedication.TabIndex = 0
        '
        'FLpnlWeight
        '
        Me.FLpnlWeight.Controls.Add(Me.lblWeight)
        Me.FLpnlWeight.Controls.Add(Me.FLpnllbsoz)
        Me.FLpnlWeight.Controls.Add(Me.FLpnllbs)
        Me.FLpnlWeight.Controls.Add(Me.FLpnlKg)
        Me.FLpnlWeight.Controls.Add(Me.FlowLayoutPanel25)
        Me.FLpnlWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLpnlWeight.Location = New System.Drawing.Point(586, 619)
        Me.FLpnlWeight.Name = "FLpnlWeight"
        Me.FLpnlWeight.Size = New System.Drawing.Size(614, 48)
        Me.FLpnlWeight.TabIndex = 0
        Me.FLpnlWeight.Visible = False
        '
        'lblWeight
        '
        Me.lblWeight.Location = New System.Drawing.Point(3, 0)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(140, 27)
        Me.lblWeight.TabIndex = 0
        Me.lblWeight.Text = "Weight :"
        Me.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnllbsoz
        '
        Me.FLpnllbsoz.Controls.Add(Me.txtWtLbs)
        Me.FLpnllbsoz.Controls.Add(Me.txtWtOz)
        Me.FLpnllbsoz.Controls.Add(Me.label55)
        Me.FLpnllbsoz.Controls.Add(Me.label56)
        Me.FLpnllbsoz.Location = New System.Drawing.Point(149, 3)
        Me.FLpnllbsoz.Name = "FLpnllbsoz"
        Me.FLpnllbsoz.Size = New System.Drawing.Size(104, 39)
        Me.FLpnllbsoz.TabIndex = 0
        Me.FLpnllbsoz.Visible = False
        '
        'txtWtLbs
        '
        Me.txtWtLbs.ForeColor = System.Drawing.Color.Black
        Me.txtWtLbs.Location = New System.Drawing.Point(3, 3)
        Me.txtWtLbs.MaxLength = 6
        Me.txtWtLbs.Name = "txtWtLbs"
        Me.txtWtLbs.Size = New System.Drawing.Size(45, 22)
        Me.txtWtLbs.TabIndex = 0
        Me.txtWtLbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWtOz
        '
        Me.txtWtOz.ForeColor = System.Drawing.Color.Black
        Me.txtWtOz.Location = New System.Drawing.Point(54, 3)
        Me.txtWtOz.MaxLength = 6
        Me.txtWtOz.Name = "txtWtOz"
        Me.txtWtOz.Size = New System.Drawing.Size(45, 22)
        Me.txtWtOz.TabIndex = 0
        Me.txtWtOz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label55
        '
        Me.label55.BackColor = System.Drawing.Color.Transparent
        Me.label55.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label55.Location = New System.Drawing.Point(3, 28)
        Me.label55.Name = "label55"
        Me.label55.Size = New System.Drawing.Size(45, 12)
        Me.label55.TabIndex = 0
        Me.label55.Text = "(lbs)"
        Me.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label56
        '
        Me.label56.BackColor = System.Drawing.Color.Transparent
        Me.label56.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label56.Location = New System.Drawing.Point(54, 28)
        Me.label56.Name = "label56"
        Me.label56.Size = New System.Drawing.Size(34, 12)
        Me.label56.TabIndex = 0
        Me.label56.Text = "(oz)"
        Me.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnllbs
        '
        Me.FLpnllbs.Controls.Add(Me.txtWeightlbs)
        Me.FLpnllbs.Controls.Add(Me.Label25)
        Me.FLpnllbs.Location = New System.Drawing.Point(259, 3)
        Me.FLpnllbs.Name = "FLpnllbs"
        Me.FLpnllbs.Size = New System.Drawing.Size(50, 39)
        Me.FLpnllbs.TabIndex = 0
        Me.FLpnllbs.Visible = False
        '
        'txtWeightlbs
        '
        Me.txtWeightlbs.ForeColor = System.Drawing.Color.Black
        Me.txtWeightlbs.Location = New System.Drawing.Point(3, 3)
        Me.txtWeightlbs.MaxLength = 7
        Me.txtWeightlbs.Name = "txtWeightlbs"
        Me.txtWeightlbs.Size = New System.Drawing.Size(45, 22)
        Me.txtWeightlbs.TabIndex = 0
        Me.txtWeightlbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 28)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(45, 12)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "(lbs)"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlKg
        '
        Me.FLpnlKg.Controls.Add(Me.txtWeightKg)
        Me.FLpnlKg.Controls.Add(Me.Label24)
        Me.FLpnlKg.Location = New System.Drawing.Point(315, 3)
        Me.FLpnlKg.Name = "FLpnlKg"
        Me.FLpnlKg.Size = New System.Drawing.Size(50, 39)
        Me.FLpnlKg.TabIndex = 0
        Me.FLpnlKg.Visible = False
        '
        'txtWeightKg
        '
        Me.txtWeightKg.ForeColor = System.Drawing.Color.Black
        Me.txtWeightKg.Location = New System.Drawing.Point(3, 3)
        Me.txtWeightKg.MaxLength = 7
        Me.txtWeightKg.Name = "txtWeightKg"
        Me.txtWeightKg.Size = New System.Drawing.Size(45, 22)
        Me.txtWeightKg.TabIndex = 0
        Me.txtWeightKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 28)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(45, 12)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "(kg)"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel25
        '
        Me.FlowLayoutPanel25.AutoSize = True
        Me.FlowLayoutPanel25.Controls.Add(Me.lblwt)
        Me.FlowLayoutPanel25.Controls.Add(Me.lblwkg)
        Me.FlowLayoutPanel25.Controls.Add(Me.lblwoz)
        Me.FlowLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel25.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel25.Location = New System.Drawing.Point(371, 3)
        Me.FlowLayoutPanel25.Name = "FlowLayoutPanel25"
        Me.FlowLayoutPanel25.Size = New System.Drawing.Size(180, 39)
        Me.FlowLayoutPanel25.TabIndex = 5
        Me.FlowLayoutPanel25.TabStop = True
        '
        'lblwt
        '
        Me.lblwt.AutoSize = True
        Me.lblwt.BackColor = System.Drawing.Color.Transparent
        Me.lblwt.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblwt.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwt.ForeColor = System.Drawing.Color.Red
        Me.lblwt.Location = New System.Drawing.Point(3, 0)
        Me.lblwt.Name = "lblwt"
        Me.lblwt.Padding = New System.Windows.Forms.Padding(2)
        Me.lblwt.Size = New System.Drawing.Size(109, 15)
        Me.lblwt.TabIndex = 1
        Me.lblwt.Text = "(22-185) kg 1237676576"
        Me.lblwt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblwkg
        '
        Me.lblwkg.AutoSize = True
        Me.lblwkg.BackColor = System.Drawing.Color.Transparent
        Me.lblwkg.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblwkg.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwkg.ForeColor = System.Drawing.Color.Red
        Me.lblwkg.Location = New System.Drawing.Point(3, 15)
        Me.lblwkg.Name = "lblwkg"
        Me.lblwkg.Padding = New System.Windows.Forms.Padding(2)
        Me.lblwkg.Size = New System.Drawing.Size(115, 15)
        Me.lblwkg.TabIndex = 3
        Me.lblwkg.Text = "(9.9-83.250) kg 58768768"
        Me.lblwkg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblwoz
        '
        Me.lblwoz.AutoSize = True
        Me.lblwoz.BackColor = System.Drawing.Color.Transparent
        Me.lblwoz.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblwoz.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwoz.ForeColor = System.Drawing.Color.Red
        Me.lblwoz.Location = New System.Drawing.Point(124, 0)
        Me.lblwoz.Name = "lblwoz"
        Me.lblwoz.Padding = New System.Windows.Forms.Padding(2)
        Me.lblwoz.Size = New System.Drawing.Size(53, 15)
        Me.lblwoz.TabIndex = 2
        Me.lblwoz.Text = "(352-2960)"
        Me.lblwoz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tblpnlMain
        '
        Me.tblpnlMain.AutoScroll = True
        Me.tblpnlMain.AutoSize = True
        Me.tblpnlMain.ColumnCount = 2
        Me.tblpnlMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.49315!))
        Me.tblpnlMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.50685!))
        Me.tblpnlMain.Controls.Add(Me.FLPnlSiteforBP, 1, 0)
        Me.tblpnlMain.Controls.Add(Me.FLpnlcomments, 0, 0)
        Me.tblpnlMain.Controls.Add(Me.FLpnlHeartrate, 1, 13)
        Me.tblpnlMain.Controls.Add(Me.FlPnlWorst, 1, 1)
        Me.tblpnlMain.Controls.Add(Me.FLpnlBMI, 1, 4)
        Me.tblpnlMain.Controls.Add(Me.FLpnlRespiratoryrate, 0, 8)
        Me.tblpnlMain.Controls.Add(Me.FLPnlBPStanding, 0, 2)
        Me.tblpnlMain.Controls.Add(Me.FLpnlBPSitting, 0, 3)
        Me.tblpnlMain.Controls.Add(Me.FlpnlRighteyepressure, 1, 2)
        Me.tblpnlMain.Controls.Add(Me.FLpnlLastmenstrualperiod, 1, 7)
        Me.tblpnlMain.Controls.Add(Me.FLpnlPEFR, 1, 3)
        Me.tblpnlMain.Controls.Add(Me.FLpnlWeight, 1, 11)
        Me.tblpnlMain.Controls.Add(Me.FLpnlwtChanged, 1, 8)
        Me.tblpnlMain.Controls.Add(Me.FLpnlneckcircum, 0, 9)
        Me.tblpnlMain.Controls.Add(Me.Flpnlpulspermin, 1, 10)
        Me.tblpnlMain.Controls.Add(Me.FLpnlStature, 1, 9)
        Me.tblpnlMain.Controls.Add(Me.Flpnllefteyepressure, 0, 10)
        Me.tblpnlMain.Controls.Add(Me.FLPnlTemp, 0, 7)
        Me.tblpnlMain.Controls.Add(Me.FLpnlHeadcircumference, 0, 11)
        Me.tblpnlMain.Controls.Add(Me.FLPnlDAS28, 0, 12)
        Me.tblpnlMain.Controls.Add(Me.FLPnlPulseOx, 0, 13)
        Me.tblpnlMain.Controls.Add(Me.FlPnlODI, 1, 12)
        Me.tblpnlMain.Controls.Add(Me.FlPnlWithMedication, 0, 6)
        Me.tblpnlMain.Controls.Add(Me.FlPnlWithoutMedication, 0, 1)
        Me.tblpnlMain.Controls.Add(Me.FLpnlPainlevel, 1, 6)
        Me.tblpnlMain.Controls.Add(Me.FLpnlHeight, 0, 4)
        Me.tblpnlMain.Controls.Add(Me.TLPanOB, 1, 30)
        Me.tblpnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblpnlMain.Location = New System.Drawing.Point(0, 0)
        Me.tblpnlMain.Name = "tblpnlMain"
        Me.tblpnlMain.RowCount = 31
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblpnlMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.tblpnlMain.Size = New System.Drawing.Size(1220, 656)
        Me.tblpnlMain.TabIndex = 0
        '
        'FLPnlSiteforBP
        '
        Me.FLPnlSiteforBP.Controls.Add(Me.Label71)
        Me.FLPnlSiteforBP.Controls.Add(Me.FlowLayoutPanel1)
        Me.FLPnlSiteforBP.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlSiteforBP.Location = New System.Drawing.Point(586, 3)
        Me.FLPnlSiteforBP.Name = "FLPnlSiteforBP"
        Me.FLPnlSiteforBP.Size = New System.Drawing.Size(614, 35)
        Me.FLPnlSiteforBP.TabIndex = 0
        Me.FLPnlSiteforBP.Visible = False
        '
        'Label71
        '
        Me.Label71.Location = New System.Drawing.Point(3, 0)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(140, 27)
        Me.Label71.TabIndex = 0
        Me.Label71.Text = "Site For BP :"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label71, "Site For Blood Pressure")
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.cmbSiteforBP)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(142, 28)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'cmbSiteforBP
        '
        Me.cmbSiteforBP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSiteforBP.FormattingEnabled = True
        Me.cmbSiteforBP.Location = New System.Drawing.Point(3, 3)
        Me.cmbSiteforBP.Name = "cmbSiteforBP"
        Me.cmbSiteforBP.Size = New System.Drawing.Size(134, 22)
        Me.cmbSiteforBP.TabIndex = 0
        '
        'FLpnlcomments
        '
        Me.FLpnlcomments.BackColor = System.Drawing.Color.Transparent
        Me.FLpnlcomments.Controls.Add(Me.Label17)
        Me.FLpnlcomments.Controls.Add(Me.txtComment)
        Me.FLpnlcomments.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlcomments.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLpnlcomments.Location = New System.Drawing.Point(3, 3)
        Me.FLpnlcomments.Name = "FLpnlcomments"
        Me.FLpnlcomments.Size = New System.Drawing.Size(577, 88)
        Me.FLpnlcomments.TabIndex = 0
        Me.FLpnlcomments.Visible = False
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(140, 23)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Comments :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComment
        '
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(149, 3)
        Me.txtComment.MaxLength = 255
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(271, 80)
        Me.txtComment.TabIndex = 0
        '
        'FLpnlHeartrate
        '
        Me.FLpnlHeartrate.Controls.Add(Me.Panel11)
        Me.FLpnlHeartrate.Controls.Add(Me.Panel9)
        Me.FLpnlHeartrate.Controls.Add(Me.Panel10)
        Me.FLpnlHeartrate.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlHeartrate.Location = New System.Drawing.Point(586, 714)
        Me.FLpnlHeartrate.Name = "FLpnlHeartrate"
        Me.FLpnlHeartrate.Size = New System.Drawing.Size(614, 94)
        Me.FLpnlHeartrate.TabIndex = 0
        Me.FLpnlHeartrate.Visible = False
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.txtTHR)
        Me.Panel11.Controls.Add(Me.lblTHR)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(3, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(562, 25)
        Me.Panel11.TabIndex = 2
        '
        'txtTHR
        '
        Me.txtTHR.BackColor = System.Drawing.SystemColors.Window
        Me.txtTHR.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtTHR.Enabled = False
        Me.txtTHR.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHR.ForeColor = System.Drawing.Color.Black
        Me.txtTHR.Location = New System.Drawing.Point(258, 0)
        Me.txtTHR.Name = "txtTHR"
        Me.txtTHR.ReadOnly = True
        Me.txtTHR.Size = New System.Drawing.Size(45, 22)
        Me.txtTHR.TabIndex = 0
        Me.txtTHR.Text = "    "
        Me.txtTHR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTHR
        '
        Me.lblTHR.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTHR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHR.Location = New System.Drawing.Point(0, 0)
        Me.lblTHR.Name = "lblTHR"
        Me.lblTHR.Size = New System.Drawing.Size(258, 25)
        Me.lblTHR.TabIndex = 0
        Me.lblTHR.Text = "Estimated maximum heart rate (bpm) :"
        Me.lblTHR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label23)
        Me.Panel9.Controls.Add(Me.txtTHRperMax)
        Me.Panel9.Controls.Add(Me.Label20)
        Me.Panel9.Controls.Add(Me.txtTHRperMin)
        Me.Panel9.Controls.Add(Me.lblTHRPerc)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(3, 34)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(508, 26)
        Me.Panel9.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(385, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 26)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "%"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTHRperMax
        '
        Me.txtTHRperMax.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtTHRperMax.ForeColor = System.Drawing.Color.Black
        Me.txtTHRperMax.Location = New System.Drawing.Point(340, 0)
        Me.txtTHRperMax.MaxLength = 3
        Me.txtTHRperMax.Name = "txtTHRperMax"
        Me.txtTHRperMax.Size = New System.Drawing.Size(45, 22)
        Me.txtTHRperMax.TabIndex = 0
        Me.txtTHRperMax.Text = "85"
        Me.txtTHRperMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(303, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(37, 26)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "%  &&"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTHRperMin
        '
        Me.txtTHRperMin.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtTHRperMin.ForeColor = System.Drawing.Color.Black
        Me.txtTHRperMin.Location = New System.Drawing.Point(258, 0)
        Me.txtTHRperMin.MaxLength = 3
        Me.txtTHRperMin.Name = "txtTHRperMin"
        Me.txtTHRperMin.Size = New System.Drawing.Size(45, 22)
        Me.txtTHRperMin.TabIndex = 0
        Me.txtTHRperMin.Text = "75"
        Me.txtTHRperMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTHRPerc
        '
        Me.lblTHRPerc.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTHRPerc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTHRPerc.Location = New System.Drawing.Point(0, 0)
        Me.lblTHRPerc.Name = "lblTHRPerc"
        Me.lblTHRPerc.Size = New System.Drawing.Size(258, 26)
        Me.lblTHRPerc.TabIndex = 0
        Me.lblTHRPerc.Text = "Estimated target heart rate % between :"
        Me.lblTHRPerc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.txtTHRMax)
        Me.Panel10.Controls.Add(Me.Label22)
        Me.Panel10.Controls.Add(Me.txtTHRMin)
        Me.Panel10.Controls.Add(Me.Label21)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(3, 66)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(529, 26)
        Me.Panel10.TabIndex = 2
        '
        'txtTHRMax
        '
        Me.txtTHRMax.BackColor = System.Drawing.SystemColors.Window
        Me.txtTHRMax.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtTHRMax.Enabled = False
        Me.txtTHRMax.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHRMax.ForeColor = System.Drawing.Color.Black
        Me.txtTHRMax.Location = New System.Drawing.Point(325, 0)
        Me.txtTHRMax.MaxLength = 6
        Me.txtTHRMax.Name = "txtTHRMax"
        Me.txtTHRMax.ReadOnly = True
        Me.txtTHRMax.Size = New System.Drawing.Size(50, 22)
        Me.txtTHRMax.TabIndex = 0
        Me.txtTHRMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(308, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(17, 26)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "&&"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTHRMin
        '
        Me.txtTHRMin.BackColor = System.Drawing.SystemColors.Window
        Me.txtTHRMin.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtTHRMin.Enabled = False
        Me.txtTHRMin.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTHRMin.ForeColor = System.Drawing.Color.Black
        Me.txtTHRMin.Location = New System.Drawing.Point(258, 0)
        Me.txtTHRMin.MaxLength = 6
        Me.txtTHRMin.Name = "txtTHRMin"
        Me.txtTHRMin.ReadOnly = True
        Me.txtTHRMin.Size = New System.Drawing.Size(50, 22)
        Me.txtTHRMin.TabIndex = 0
        Me.txtTHRMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(258, 26)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Estimated target heart rate (bpm) between :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlPnlWorst
        '
        Me.FlPnlWorst.AutoSize = True
        Me.FlPnlWorst.Controls.Add(Me.Label121)
        Me.FlPnlWorst.Controls.Add(Me.chkPainWorst)
        Me.FlPnlWorst.Controls.Add(Me.Panel5)
        Me.FlPnlWorst.Location = New System.Drawing.Point(586, 97)
        Me.FlPnlWorst.Name = "FlPnlWorst"
        Me.FlPnlWorst.Size = New System.Drawing.Size(420, 74)
        Me.FlPnlWorst.TabIndex = 3
        Me.FlPnlWorst.Visible = False
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.Transparent
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.Location = New System.Drawing.Point(3, 0)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(114, 23)
        Me.Label121.TabIndex = 0
        Me.Label121.Text = "Pain level : Worst"
        Me.Label121.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPainWorst
        '
        Me.chkPainWorst.Location = New System.Drawing.Point(123, 3)
        Me.chkPainWorst.Name = "chkPainWorst"
        Me.chkPainWorst.Padding = New System.Windows.Forms.Padding(3)
        Me.chkPainWorst.Size = New System.Drawing.Size(17, 14)
        Me.chkPainWorst.TabIndex = 1
        Me.chkPainWorst.Text = "Allow"
        Me.chkPainWorst.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.Label43)
        Me.Panel5.Controls.Add(Me.Label44)
        Me.Panel5.Controls.Add(Me.Label45)
        Me.Panel5.Controls.Add(Me.Label46)
        Me.Panel5.Controls.Add(Me.Label122)
        Me.Panel5.Controls.Add(Me.Label123)
        Me.Panel5.Controls.Add(Me.Label124)
        Me.Panel5.Controls.Add(Me.Label125)
        Me.Panel5.Controls.Add(Me.Label126)
        Me.Panel5.Controls.Add(Me.Label127)
        Me.Panel5.Controls.Add(Me.Label128)
        Me.Panel5.Controls.Add(Me.Label129)
        Me.Panel5.Controls.Add(Me.Label130)
        Me.Panel5.Controls.Add(Me.Label131)
        Me.Panel5.Controls.Add(Me.Label132)
        Me.Panel5.Controls.Add(Me.Label133)
        Me.Panel5.Controls.Add(Me.Label134)
        Me.Panel5.Controls.Add(Me.Label135)
        Me.Panel5.Controls.Add(Me.Label142)
        Me.Panel5.Controls.Add(Me.trbPainWorst)
        Me.Panel5.Location = New System.Drawing.Point(146, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(271, 68)
        Me.Panel5.TabIndex = 0
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(242, 7)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(23, 11)
        Me.Label41.TabIndex = 10
        Me.Label41.Text = "Pain"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(19, 7)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(23, 11)
        Me.Label42.TabIndex = 11
        Me.Label42.Text = "Pain"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(130, 7)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(23, 11)
        Me.Label43.TabIndex = 12
        Me.Label43.Text = "Pain"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(193, 7)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(52, 11)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "Unbearable"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(88, 7)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(44, 11)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "Moderate"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(5, 7)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(17, 11)
        Me.Label46.TabIndex = 9
        Me.Label46.Text = "No"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(1, 67)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(269, 1)
        Me.Label122.TabIndex = 0
        Me.Label122.Text = "label2"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.Location = New System.Drawing.Point(0, 1)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(1, 67)
        Me.Label123.TabIndex = 0
        Me.Label123.Text = "label4"
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label124.Location = New System.Drawing.Point(270, 1)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(1, 67)
        Me.Label124.TabIndex = 0
        Me.Label124.Text = "label3"
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.Location = New System.Drawing.Point(0, 0)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(271, 1)
        Me.Label125.TabIndex = 0
        Me.Label125.Text = "label1"
        '
        'Label126
        '
        Me.Label126.AutoSize = True
        Me.Label126.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label126.Location = New System.Drawing.Point(241, 51)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(19, 12)
        Me.Label126.TabIndex = 0
        Me.Label126.Text = "10"
        '
        'Label127
        '
        Me.Label127.AutoSize = True
        Me.Label127.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label127.Location = New System.Drawing.Point(218, 51)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(12, 12)
        Me.Label127.TabIndex = 0
        Me.Label127.Text = "9"
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.Location = New System.Drawing.Point(195, 51)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(12, 12)
        Me.Label128.TabIndex = 0
        Me.Label128.Text = "8"
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.Location = New System.Drawing.Point(172, 51)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(12, 12)
        Me.Label129.TabIndex = 0
        Me.Label129.Text = "7"
        '
        'Label130
        '
        Me.Label130.AutoSize = True
        Me.Label130.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.Location = New System.Drawing.Point(149, 51)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(12, 12)
        Me.Label130.TabIndex = 0
        Me.Label130.Text = "6"
        '
        'Label131
        '
        Me.Label131.AutoSize = True
        Me.Label131.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label131.Location = New System.Drawing.Point(126, 51)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(12, 12)
        Me.Label131.TabIndex = 0
        Me.Label131.Text = "5"
        '
        'Label132
        '
        Me.Label132.AutoSize = True
        Me.Label132.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label132.Location = New System.Drawing.Point(103, 51)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(12, 12)
        Me.Label132.TabIndex = 0
        Me.Label132.Text = "4"
        '
        'Label133
        '
        Me.Label133.AutoSize = True
        Me.Label133.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.Location = New System.Drawing.Point(80, 51)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(12, 12)
        Me.Label133.TabIndex = 0
        Me.Label133.Text = "3"
        '
        'Label134
        '
        Me.Label134.AutoSize = True
        Me.Label134.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label134.Location = New System.Drawing.Point(57, 51)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(12, 12)
        Me.Label134.TabIndex = 0
        Me.Label134.Text = "2"
        '
        'Label135
        '
        Me.Label135.AutoSize = True
        Me.Label135.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label135.Location = New System.Drawing.Point(34, 51)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(12, 12)
        Me.Label135.TabIndex = 0
        Me.Label135.Text = "1"
        '
        'Label142
        '
        Me.Label142.AutoSize = True
        Me.Label142.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label142.Location = New System.Drawing.Point(11, 51)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(12, 12)
        Me.Label142.TabIndex = 0
        Me.Label142.Text = "0"
        '
        'trbPainWorst
        '
        Me.trbPainWorst.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainWorst.LargeChange = 1
        Me.trbPainWorst.Location = New System.Drawing.Point(3, 21)
        Me.trbPainWorst.Name = "trbPainWorst"
        Me.trbPainWorst.Size = New System.Drawing.Size(258, 45)
        Me.trbPainWorst.TabIndex = 0
        '
        'FLpnlBMI
        '
        Me.FLpnlBMI.Controls.Add(Me.Label27)
        Me.FLpnlBMI.Controls.Add(Me.FlowLayoutPanel4)
        Me.FLpnlBMI.Controls.Add(Me.lblMsg)
        Me.FLpnlBMI.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlBMI.Location = New System.Drawing.Point(586, 285)
        Me.FLpnlBMI.Name = "FLpnlBMI"
        Me.FLpnlBMI.Size = New System.Drawing.Size(614, 48)
        Me.FLpnlBMI.TabIndex = 0
        Me.FLpnlBMI.Visible = False
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(3, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(140, 27)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "BMI :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.Controls.Add(Me.Panel12)
        Me.FlowLayoutPanel4.Controls.Add(Me.lblBMIPercentile)
        Me.FlowLayoutPanel4.Controls.Add(Me.txtBMIPercentile)
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(220, 45)
        Me.FlowLayoutPanel4.TabIndex = 0
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.lblbminor)
        Me.Panel12.Controls.Add(Me.txtBMI)
        Me.Panel12.Location = New System.Drawing.Point(3, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(76, 40)
        Me.Panel12.TabIndex = 5
        '
        'lblbminor
        '
        Me.lblbminor.AutoSize = True
        Me.lblbminor.BackColor = System.Drawing.Color.Transparent
        Me.lblbminor.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbminor.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblbminor.Location = New System.Drawing.Point(0, 26)
        Me.lblbminor.Name = "lblbminor"
        Me.lblbminor.Size = New System.Drawing.Size(67, 11)
        Me.lblbminor.TabIndex = 3
        Me.lblbminor.Text = "(18.5-24.9)"
        Me.lblbminor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBMI
        '
        Me.txtBMI.ForeColor = System.Drawing.Color.Black
        Me.txtBMI.Location = New System.Drawing.Point(0, 1)
        Me.txtBMI.MaxLength = 5
        Me.txtBMI.Name = "txtBMI"
        Me.txtBMI.Size = New System.Drawing.Size(47, 22)
        Me.txtBMI.TabIndex = 0
        Me.txtBMI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMIPercentile
        '
        Me.lblBMIPercentile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBMIPercentile.AutoSize = True
        Me.lblBMIPercentile.BackColor = System.Drawing.Color.Transparent
        Me.lblBMIPercentile.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblBMIPercentile.Location = New System.Drawing.Point(85, 0)
        Me.lblBMIPercentile.Name = "lblBMIPercentile"
        Me.lblBMIPercentile.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lblBMIPercentile.Size = New System.Drawing.Size(69, 46)
        Me.lblBMIPercentile.TabIndex = 3
        Me.lblBMIPercentile.Text = "Percentile :"
        Me.lblBMIPercentile.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtBMIPercentile
        '
        Me.txtBMIPercentile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBMIPercentile.ForeColor = System.Drawing.Color.Black
        Me.txtBMIPercentile.Location = New System.Drawing.Point(160, 3)
        Me.txtBMIPercentile.MaxLength = 5
        Me.txtBMIPercentile.Name = "txtBMIPercentile"
        Me.txtBMIPercentile.Size = New System.Drawing.Size(45, 22)
        Me.txtBMIPercentile.TabIndex = 4
        Me.txtBMIPercentile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMsg
        '
        Me.lblMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblMsg.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMsg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.Red
        Me.lblMsg.Location = New System.Drawing.Point(375, 0)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(211, 51)
        Me.lblMsg.TabIndex = 2
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FLpnlRespiratoryrate
        '
        Me.FLpnlRespiratoryrate.Controls.Add(Me.Label10)
        Me.FLpnlRespiratoryrate.Controls.Add(Me.FlowLayoutPanel9)
        Me.FLpnlRespiratoryrate.Controls.Add(Me.FlowLayoutPanel17)
        Me.FLpnlRespiratoryrate.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlRespiratoryrate.Location = New System.Drawing.Point(3, 470)
        Me.FLpnlRespiratoryrate.Name = "FLpnlRespiratoryrate"
        Me.FLpnlRespiratoryrate.Size = New System.Drawing.Size(577, 35)
        Me.FLpnlRespiratoryrate.TabIndex = 0
        Me.FLpnlRespiratoryrate.Visible = False
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 22)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Respiratory Rate :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel9
        '
        Me.FlowLayoutPanel9.Controls.Add(Me.txtRespiratory)
        Me.FlowLayoutPanel9.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel9.Name = "FlowLayoutPanel9"
        Me.FlowLayoutPanel9.Size = New System.Drawing.Size(50, 28)
        Me.FlowLayoutPanel9.TabIndex = 0
        '
        'txtRespiratory
        '
        Me.txtRespiratory.ForeColor = System.Drawing.Color.Black
        Me.txtRespiratory.Location = New System.Drawing.Point(3, 3)
        Me.txtRespiratory.MaxLength = 3
        Me.txtRespiratory.Name = "txtRespiratory"
        Me.txtRespiratory.Size = New System.Drawing.Size(45, 22)
        Me.txtRespiratory.TabIndex = 0
        Me.txtRespiratory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FlowLayoutPanel17
        '
        Me.FlowLayoutPanel17.AutoSize = True
        Me.FlowLayoutPanel17.Controls.Add(Me.lblrsprt)
        Me.FlowLayoutPanel17.Location = New System.Drawing.Point(205, 3)
        Me.FlowLayoutPanel17.Name = "FlowLayoutPanel17"
        Me.FlowLayoutPanel17.Size = New System.Drawing.Size(40, 23)
        Me.FlowLayoutPanel17.TabIndex = 3
        Me.FlowLayoutPanel17.TabStop = True
        '
        'lblrsprt
        '
        Me.lblrsprt.AutoSize = True
        Me.lblrsprt.BackColor = System.Drawing.Color.Transparent
        Me.lblrsprt.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblrsprt.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrsprt.ForeColor = System.Drawing.Color.Red
        Me.lblrsprt.Location = New System.Drawing.Point(3, 0)
        Me.lblrsprt.Name = "lblrsprt"
        Me.lblrsprt.Padding = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.lblrsprt.Size = New System.Drawing.Size(34, 23)
        Me.lblrsprt.TabIndex = 0
        Me.lblrsprt.Text = "(123)"
        Me.lblrsprt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLPnlBPStanding
        '
        Me.FLPnlBPStanding.Controls.Add(Me.Label16)
        Me.FLPnlBPStanding.Controls.Add(Me.FlowLayoutPanel2)
        Me.FLPnlBPStanding.Controls.Add(Me.FlowLayoutPanel14)
        Me.FLPnlBPStanding.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlBPStanding.Location = New System.Drawing.Point(3, 177)
        Me.FLPnlBPStanding.Name = "FLPnlBPStanding"
        Me.FLPnlBPStanding.Size = New System.Drawing.Size(577, 48)
        Me.FLPnlBPStanding.TabIndex = 0
        Me.FLPnlBPStanding.Visible = False
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(3, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(140, 27)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "BP Standing :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label16, "Blood Pressure Standing")
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.txtBPStandingMax)
        Me.FlowLayoutPanel2.Controls.Add(Me.Label15)
        Me.FlowLayoutPanel2.Controls.Add(Me.txtBPStandingMin)
        Me.FlowLayoutPanel2.Controls.Add(Me.label50)
        Me.FlowLayoutPanel2.Controls.Add(Me.Label8)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(150, 39)
        Me.FlowLayoutPanel2.TabIndex = 0
        '
        'txtBPStandingMax
        '
        Me.txtBPStandingMax.ForeColor = System.Drawing.Color.Black
        Me.txtBPStandingMax.Location = New System.Drawing.Point(3, 3)
        Me.txtBPStandingMax.MaxLength = 3
        Me.txtBPStandingMax.Name = "txtBPStandingMax"
        Me.txtBPStandingMax.Size = New System.Drawing.Size(45, 22)
        Me.txtBPStandingMax.TabIndex = 0
        Me.txtBPStandingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(54, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(15, 27)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "/"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBPStandingMin
        '
        Me.txtBPStandingMin.ForeColor = System.Drawing.Color.Black
        Me.txtBPStandingMin.Location = New System.Drawing.Point(75, 3)
        Me.txtBPStandingMin.MaxLength = 3
        Me.txtBPStandingMin.Name = "txtBPStandingMin"
        Me.txtBPStandingMin.Size = New System.Drawing.Size(45, 22)
        Me.txtBPStandingMin.TabIndex = 0
        Me.txtBPStandingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label50
        '
        Me.label50.BackColor = System.Drawing.Color.Transparent
        Me.label50.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label50.Location = New System.Drawing.Point(3, 28)
        Me.label50.Name = "label50"
        Me.label50.Size = New System.Drawing.Size(45, 12)
        Me.label50.TabIndex = 0
        Me.label50.Text = "(Systolic)"
        Me.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(54, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "(Diastolic)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel14
        '
        Me.FlowLayoutPanel14.AutoSize = True
        Me.FlowLayoutPanel14.BackColor = System.Drawing.Color.Transparent
        Me.FlowLayoutPanel14.Controls.Add(Me.lblbpstsys)
        Me.FlowLayoutPanel14.Controls.Add(Me.lblbpstdia)
        Me.FlowLayoutPanel14.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel14.Location = New System.Drawing.Point(305, 3)
        Me.FlowLayoutPanel14.Name = "FlowLayoutPanel14"
        Me.FlowLayoutPanel14.Size = New System.Drawing.Size(84, 38)
        Me.FlowLayoutPanel14.TabIndex = 1
        Me.FlowLayoutPanel14.TabStop = True
        '
        'lblbpstsys
        '
        Me.lblbpstsys.AutoSize = True
        Me.lblbpstsys.BackColor = System.Drawing.Color.Transparent
        Me.lblbpstsys.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblbpstsys.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpstsys.ForeColor = System.Drawing.Color.Red
        Me.lblbpstsys.Location = New System.Drawing.Point(3, 0)
        Me.lblbpstsys.Name = "lblbpstsys"
        Me.lblbpstsys.Padding = New System.Windows.Forms.Padding(4)
        Me.lblbpstsys.Size = New System.Drawing.Size(78, 19)
        Me.lblbpstsys.TabIndex = 0
        Me.lblbpstsys.Text = "(129.54-177.80)"
        Me.lblbpstsys.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblbpstdia
        '
        Me.lblbpstdia.AutoSize = True
        Me.lblbpstdia.BackColor = System.Drawing.Color.Transparent
        Me.lblbpstdia.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblbpstdia.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpstdia.ForeColor = System.Drawing.Color.Red
        Me.lblbpstdia.Location = New System.Drawing.Point(3, 19)
        Me.lblbpstdia.Name = "lblbpstdia"
        Me.lblbpstdia.Padding = New System.Windows.Forms.Padding(4)
        Me.lblbpstdia.Size = New System.Drawing.Size(78, 19)
        Me.lblbpstdia.TabIndex = 0
        Me.lblbpstdia.Text = "(129.54-177.80)"
        Me.lblbpstdia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FLpnlBPSitting
        '
        Me.FLpnlBPSitting.Controls.Add(Me.Label14)
        Me.FLpnlBPSitting.Controls.Add(Me.FlowLayoutPanel3)
        Me.FLpnlBPSitting.Controls.Add(Me.FlowLayoutPanel15)
        Me.FLpnlBPSitting.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlBPSitting.Location = New System.Drawing.Point(3, 231)
        Me.FLpnlBPSitting.Name = "FLpnlBPSitting"
        Me.FLpnlBPSitting.Size = New System.Drawing.Size(577, 48)
        Me.FLpnlBPSitting.TabIndex = 0
        Me.FLpnlBPSitting.Visible = False
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(140, 27)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "BP Sitting :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label14, "Blood Pressure Sitting")
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.Controls.Add(Me.txtBPSittingMax)
        Me.FlowLayoutPanel3.Controls.Add(Me.Label13)
        Me.FlowLayoutPanel3.Controls.Add(Me.txtBPSittingMin)
        Me.FlowLayoutPanel3.Controls.Add(Me.label52)
        Me.FlowLayoutPanel3.Controls.Add(Me.label51)
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(137, 39)
        Me.FlowLayoutPanel3.TabIndex = 0
        '
        'txtBPSittingMax
        '
        Me.txtBPSittingMax.ForeColor = System.Drawing.Color.Black
        Me.txtBPSittingMax.Location = New System.Drawing.Point(3, 3)
        Me.txtBPSittingMax.MaxLength = 3
        Me.txtBPSittingMax.Name = "txtBPSittingMax"
        Me.txtBPSittingMax.Size = New System.Drawing.Size(45, 22)
        Me.txtBPSittingMax.TabIndex = 0
        Me.txtBPSittingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(54, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(15, 24)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "/"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBPSittingMin
        '
        Me.txtBPSittingMin.ForeColor = System.Drawing.Color.Black
        Me.txtBPSittingMin.Location = New System.Drawing.Point(75, 3)
        Me.txtBPSittingMin.MaxLength = 3
        Me.txtBPSittingMin.Name = "txtBPSittingMin"
        Me.txtBPSittingMin.Size = New System.Drawing.Size(45, 22)
        Me.txtBPSittingMin.TabIndex = 0
        Me.txtBPSittingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label52
        '
        Me.label52.BackColor = System.Drawing.Color.Transparent
        Me.label52.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label52.Location = New System.Drawing.Point(3, 28)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(47, 12)
        Me.label52.TabIndex = 0
        Me.label52.Text = "(Systolic)"
        Me.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label51
        '
        Me.label51.BackColor = System.Drawing.Color.Transparent
        Me.label51.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label51.Location = New System.Drawing.Point(56, 28)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(64, 12)
        Me.label51.TabIndex = 0
        Me.label51.Text = "(Diastolic)"
        Me.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel15
        '
        Me.FlowLayoutPanel15.Controls.Add(Me.lblbpsisys)
        Me.FlowLayoutPanel15.Controls.Add(Me.lblbpsidia)
        Me.FlowLayoutPanel15.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel15.Location = New System.Drawing.Point(292, 3)
        Me.FlowLayoutPanel15.Name = "FlowLayoutPanel15"
        Me.FlowLayoutPanel15.Size = New System.Drawing.Size(109, 40)
        Me.FlowLayoutPanel15.TabIndex = 2
        Me.FlowLayoutPanel15.TabStop = True
        '
        'lblbpsisys
        '
        Me.lblbpsisys.AutoSize = True
        Me.lblbpsisys.BackColor = System.Drawing.Color.Transparent
        Me.lblbpsisys.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblbpsisys.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpsisys.ForeColor = System.Drawing.Color.Red
        Me.lblbpsisys.Location = New System.Drawing.Point(3, 0)
        Me.lblbpsisys.Name = "lblbpsisys"
        Me.lblbpsisys.Padding = New System.Windows.Forms.Padding(4)
        Me.lblbpsisys.Size = New System.Drawing.Size(78, 19)
        Me.lblbpsisys.TabIndex = 0
        Me.lblbpsisys.Text = "(129.54-177.80)"
        Me.lblbpsisys.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblbpsidia
        '
        Me.lblbpsidia.AutoSize = True
        Me.lblbpsidia.BackColor = System.Drawing.Color.Transparent
        Me.lblbpsidia.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblbpsidia.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpsidia.ForeColor = System.Drawing.Color.Red
        Me.lblbpsidia.Location = New System.Drawing.Point(3, 19)
        Me.lblbpsidia.Name = "lblbpsidia"
        Me.lblbpsidia.Padding = New System.Windows.Forms.Padding(4)
        Me.lblbpsidia.Size = New System.Drawing.Size(78, 19)
        Me.lblbpsidia.TabIndex = 0
        Me.lblbpsidia.Text = "(129.54-177.80)"
        Me.lblbpsidia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FlpnlRighteyepressure
        '
        Me.FlpnlRighteyepressure.Controls.Add(Me.Label73)
        Me.FlpnlRighteyepressure.Controls.Add(Me.FlowLayoutPanel7)
        Me.FlpnlRighteyepressure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlpnlRighteyepressure.Location = New System.Drawing.Point(586, 177)
        Me.FlpnlRighteyepressure.Name = "FlpnlRighteyepressure"
        Me.FlpnlRighteyepressure.Size = New System.Drawing.Size(614, 48)
        Me.FlpnlRighteyepressure.TabIndex = 0
        Me.FlpnlRighteyepressure.Visible = False
        '
        'Label73
        '
        Me.Label73.Location = New System.Drawing.Point(3, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(140, 27)
        Me.Label73.TabIndex = 0
        Me.Label73.Text = "Right Eye Pressure :"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel7
        '
        Me.FlowLayoutPanel7.Controls.Add(Me.txtRighteyepressure)
        Me.FlowLayoutPanel7.Controls.Add(Me.Label75)
        Me.FlowLayoutPanel7.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel7.Name = "FlowLayoutPanel7"
        Me.FlowLayoutPanel7.Size = New System.Drawing.Size(56, 40)
        Me.FlowLayoutPanel7.TabIndex = 0
        '
        'txtRighteyepressure
        '
        Me.txtRighteyepressure.Location = New System.Drawing.Point(3, 3)
        Me.txtRighteyepressure.MaxLength = 5
        Me.txtRighteyepressure.Name = "txtRighteyepressure"
        Me.txtRighteyepressure.Size = New System.Drawing.Size(45, 22)
        Me.txtRighteyepressure.TabIndex = 0
        Me.txtRighteyepressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.Transparent
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(3, 28)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(46, 12)
        Me.Label75.TabIndex = 0
        Me.Label75.Text = "(mm Hg)"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlLastmenstrualperiod
        '
        Me.FLpnlLastmenstrualperiod.AutoSize = True
        Me.FLpnlLastmenstrualperiod.Controls.Add(Me.Label72)
        Me.FLpnlLastmenstrualperiod.Controls.Add(Me.FlowLayoutPanel12)
        Me.FLpnlLastmenstrualperiod.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlLastmenstrualperiod.Location = New System.Drawing.Point(586, 419)
        Me.FLpnlLastmenstrualperiod.Name = "FLpnlLastmenstrualperiod"
        Me.FLpnlLastmenstrualperiod.Size = New System.Drawing.Size(614, 33)
        Me.FLpnlLastmenstrualperiod.TabIndex = 0
        Me.FLpnlLastmenstrualperiod.Visible = False
        '
        'Label72
        '
        Me.Label72.Location = New System.Drawing.Point(3, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(140, 27)
        Me.Label72.TabIndex = 0
        Me.Label72.Text = "Last Menstrual Period :"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel12
        '
        Me.FlowLayoutPanel12.Controls.Add(Me.dtLastmenstrualperiod)
        Me.FlowLayoutPanel12.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel12.Name = "FlowLayoutPanel12"
        Me.FlowLayoutPanel12.Size = New System.Drawing.Size(120, 27)
        Me.FlowLayoutPanel12.TabIndex = 0
        '
        'dtLastmenstrualperiod
        '
        Me.dtLastmenstrualperiod.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtLastmenstrualperiod.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtLastmenstrualperiod.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtLastmenstrualperiod.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtLastmenstrualperiod.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtLastmenstrualperiod.Checked = False
        Me.dtLastmenstrualperiod.CustomFormat = "MM/dd/yyyy"
        Me.dtLastmenstrualperiod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtLastmenstrualperiod.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtLastmenstrualperiod.Location = New System.Drawing.Point(3, 3)
        Me.dtLastmenstrualperiod.Name = "dtLastmenstrualperiod"
        Me.dtLastmenstrualperiod.ShowCheckBox = True
        Me.dtLastmenstrualperiod.Size = New System.Drawing.Size(117, 22)
        Me.dtLastmenstrualperiod.TabIndex = 0
        '
        'FLpnlPEFR
        '
        Me.FLpnlPEFR.Controls.Add(Me.Label30)
        Me.FLpnlPEFR.Controls.Add(Me.FlowLayoutPanel8)
        Me.FLpnlPEFR.Controls.Add(Me.lblPEFR)
        Me.FLpnlPEFR.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlPEFR.Location = New System.Drawing.Point(586, 231)
        Me.FLpnlPEFR.Name = "FLpnlPEFR"
        Me.FLpnlPEFR.Size = New System.Drawing.Size(614, 48)
        Me.FLpnlPEFR.TabIndex = 0
        Me.FLpnlPEFR.Visible = False
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Location = New System.Drawing.Point(3, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(140, 26)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "PEFR :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.Label30, "Peak Expiratory Flow Rate")
        '
        'FlowLayoutPanel8
        '
        Me.FlowLayoutPanel8.Controls.Add(Me.txtPEFR1)
        Me.FlowLayoutPanel8.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel8.Name = "FlowLayoutPanel8"
        Me.FlowLayoutPanel8.Size = New System.Drawing.Size(50, 25)
        Me.FlowLayoutPanel8.TabIndex = 0
        '
        'txtPEFR1
        '
        Me.txtPEFR1.ForeColor = System.Drawing.Color.Black
        Me.txtPEFR1.Location = New System.Drawing.Point(3, 3)
        Me.txtPEFR1.MaxLength = 6
        Me.txtPEFR1.Name = "txtPEFR1"
        Me.txtPEFR1.Size = New System.Drawing.Size(45, 22)
        Me.txtPEFR1.TabIndex = 0
        Me.txtPEFR1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPEFR
        '
        Me.lblPEFR.AutoSize = True
        Me.lblPEFR.BackColor = System.Drawing.Color.Transparent
        Me.lblPEFR.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPEFR.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPEFR.ForeColor = System.Drawing.Color.Red
        Me.lblPEFR.Location = New System.Drawing.Point(205, 0)
        Me.lblPEFR.Name = "lblPEFR"
        Me.lblPEFR.Padding = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.lblPEFR.Size = New System.Drawing.Size(78, 31)
        Me.lblPEFR.TabIndex = 1
        Me.lblPEFR.Text = "(129.54-177.80)"
        Me.lblPEFR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlwtChanged
        '
        Me.FLpnlwtChanged.Controls.Add(Me.Label28)
        Me.FLpnlwtChanged.Controls.Add(Me.FlowLayoutPanel11)
        Me.FLpnlwtChanged.Location = New System.Drawing.Point(586, 470)
        Me.FLpnlwtChanged.Name = "FLpnlwtChanged"
        Me.FLpnlwtChanged.Size = New System.Drawing.Size(430, 35)
        Me.FLpnlwtChanged.TabIndex = 0
        Me.FLpnlwtChanged.Visible = False
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(3, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(140, 27)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Weight change :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel11
        '
        Me.FlowLayoutPanel11.Controls.Add(Me.txtWeightChanged)
        Me.FlowLayoutPanel11.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel11.Name = "FlowLayoutPanel11"
        Me.FlowLayoutPanel11.Size = New System.Drawing.Size(50, 28)
        Me.FlowLayoutPanel11.TabIndex = 0
        '
        'txtWeightChanged
        '
        Me.txtWeightChanged.BackColor = System.Drawing.Color.White
        Me.txtWeightChanged.ForeColor = System.Drawing.Color.Black
        Me.txtWeightChanged.Location = New System.Drawing.Point(3, 3)
        Me.txtWeightChanged.MaxLength = 6
        Me.txtWeightChanged.Name = "txtWeightChanged"
        Me.txtWeightChanged.ReadOnly = True
        Me.txtWeightChanged.Size = New System.Drawing.Size(45, 22)
        Me.txtWeightChanged.TabIndex = 0
        Me.txtWeightChanged.TabStop = False
        Me.txtWeightChanged.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FLpnlneckcircum
        '
        Me.FLpnlneckcircum.Controls.Add(Me.lblneckcircum)
        Me.FLpnlneckcircum.Controls.Add(Me.FLpnlneckcircumInch)
        Me.FLpnlneckcircum.Controls.Add(Me.FLneckcircumCm)
        Me.FLpnlneckcircum.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlneckcircum.Location = New System.Drawing.Point(3, 511)
        Me.FLpnlneckcircum.Name = "FLpnlneckcircum"
        Me.FLpnlneckcircum.Size = New System.Drawing.Size(577, 48)
        Me.FLpnlneckcircum.TabIndex = 0
        Me.FLpnlneckcircum.Visible = False
        '
        'lblneckcircum
        '
        Me.lblneckcircum.Location = New System.Drawing.Point(3, 0)
        Me.lblneckcircum.Name = "lblneckcircum"
        Me.lblneckcircum.Size = New System.Drawing.Size(140, 33)
        Me.lblneckcircum.TabIndex = 0
        Me.lblneckcircum.Text = "Neck Circumference :"
        Me.lblneckcircum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlneckcircumInch
        '
        Me.FLpnlneckcircumInch.Controls.Add(Me.txtneckcircumInch)
        Me.FLpnlneckcircumInch.Controls.Add(Me.Label3)
        Me.FLpnlneckcircumInch.Location = New System.Drawing.Point(149, 3)
        Me.FLpnlneckcircumInch.Name = "FLpnlneckcircumInch"
        Me.FLpnlneckcircumInch.Size = New System.Drawing.Size(56, 44)
        Me.FLpnlneckcircumInch.TabIndex = 0
        Me.FLpnlneckcircumInch.Visible = False
        '
        'txtneckcircumInch
        '
        Me.txtneckcircumInch.Location = New System.Drawing.Point(3, 3)
        Me.txtneckcircumInch.Name = "txtneckcircumInch"
        Me.txtneckcircumInch.Size = New System.Drawing.Size(45, 22)
        Me.txtneckcircumInch.TabIndex = 0
        Me.txtneckcircumInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 10)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "(in)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLneckcircumCm
        '
        Me.FLneckcircumCm.Controls.Add(Me.txtneckcircumCm)
        Me.FLneckcircumCm.Controls.Add(Me.Label5)
        Me.FLneckcircumCm.Location = New System.Drawing.Point(211, 3)
        Me.FLneckcircumCm.Name = "FLneckcircumCm"
        Me.FLneckcircumCm.Size = New System.Drawing.Size(56, 44)
        Me.FLneckcircumCm.TabIndex = 0
        Me.FLneckcircumCm.Visible = False
        '
        'txtneckcircumCm
        '
        Me.txtneckcircumCm.Location = New System.Drawing.Point(3, 3)
        Me.txtneckcircumCm.Name = "txtneckcircumCm"
        Me.txtneckcircumCm.Size = New System.Drawing.Size(45, 22)
        Me.txtneckcircumCm.TabIndex = 0
        Me.txtneckcircumCm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 10)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "(cm)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Flpnlpulspermin
        '
        Me.Flpnlpulspermin.Controls.Add(Me.Label11)
        Me.Flpnlpulspermin.Controls.Add(Me.FlowLayoutPanel10)
        Me.Flpnlpulspermin.Controls.Add(Me.FlowLayoutPanel24)
        Me.Flpnlpulspermin.Dock = System.Windows.Forms.DockStyle.Top
        Me.Flpnlpulspermin.Location = New System.Drawing.Point(586, 565)
        Me.Flpnlpulspermin.Name = "Flpnlpulspermin"
        Me.Flpnlpulspermin.Size = New System.Drawing.Size(614, 48)
        Me.Flpnlpulspermin.TabIndex = 0
        Me.Flpnlpulspermin.Visible = False
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(140, 24)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Pulse per minute :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel10
        '
        Me.FlowLayoutPanel10.Controls.Add(Me.txtPulsePerMinute)
        Me.FlowLayoutPanel10.Controls.Add(Me.Label76)
        Me.FlowLayoutPanel10.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel10.Name = "FlowLayoutPanel10"
        Me.FlowLayoutPanel10.Size = New System.Drawing.Size(50, 39)
        Me.FlowLayoutPanel10.TabIndex = 0
        '
        'txtPulsePerMinute
        '
        Me.txtPulsePerMinute.ForeColor = System.Drawing.Color.Black
        Me.txtPulsePerMinute.Location = New System.Drawing.Point(3, 3)
        Me.txtPulsePerMinute.MaxLength = 3
        Me.txtPulsePerMinute.Name = "txtPulsePerMinute"
        Me.txtPulsePerMinute.Size = New System.Drawing.Size(45, 22)
        Me.txtPulsePerMinute.TabIndex = 0
        Me.txtPulsePerMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.Transparent
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(3, 28)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(45, 11)
        Me.Label76.TabIndex = 0
        Me.Label76.Text = "(bpm)"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel24
        '
        Me.FlowLayoutPanel24.AutoSize = True
        Me.FlowLayoutPanel24.Controls.Add(Me.lblppm)
        Me.FlowLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Right
        Me.FlowLayoutPanel24.Location = New System.Drawing.Point(205, 3)
        Me.FlowLayoutPanel24.Name = "FlowLayoutPanel24"
        Me.FlowLayoutPanel24.Size = New System.Drawing.Size(40, 39)
        Me.FlowLayoutPanel24.TabIndex = 4
        Me.FlowLayoutPanel24.TabStop = True
        '
        'lblppm
        '
        Me.lblppm.AutoSize = True
        Me.lblppm.BackColor = System.Drawing.Color.Transparent
        Me.lblppm.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblppm.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblppm.ForeColor = System.Drawing.Color.Red
        Me.lblppm.Location = New System.Drawing.Point(3, 0)
        Me.lblppm.Name = "lblppm"
        Me.lblppm.Padding = New System.Windows.Forms.Padding(4, 8, 4, 4)
        Me.lblppm.Size = New System.Drawing.Size(34, 23)
        Me.lblppm.TabIndex = 0
        Me.lblppm.Text = "(123)"
        Me.lblppm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlStature
        '
        Me.FLpnlStature.Controls.Add(Me.lblStature)
        Me.FLpnlStature.Controls.Add(Me.FLpnlStatureInch)
        Me.FLpnlStature.Controls.Add(Me.FLpnlStatureCm)
        Me.FLpnlStature.Controls.Add(Me.FlowLayoutPanel19)
        Me.FLpnlStature.Location = New System.Drawing.Point(586, 511)
        Me.FLpnlStature.Name = "FLpnlStature"
        Me.FLpnlStature.Size = New System.Drawing.Size(428, 48)
        Me.FLpnlStature.TabIndex = 0
        Me.FLpnlStature.Visible = False
        '
        'lblStature
        '
        Me.lblStature.Location = New System.Drawing.Point(3, 0)
        Me.lblStature.Name = "lblStature"
        Me.lblStature.Size = New System.Drawing.Size(140, 28)
        Me.lblStature.TabIndex = 0
        Me.lblStature.Text = "Stature :"
        Me.lblStature.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlStatureInch
        '
        Me.FLpnlStatureInch.Controls.Add(Me.txtStatureinInch)
        Me.FLpnlStatureInch.Controls.Add(Me.Label62)
        Me.FLpnlStatureInch.Location = New System.Drawing.Point(149, 3)
        Me.FLpnlStatureInch.Name = "FLpnlStatureInch"
        Me.FLpnlStatureInch.Size = New System.Drawing.Size(56, 40)
        Me.FLpnlStatureInch.TabIndex = 0
        Me.FLpnlStatureInch.Visible = False
        '
        'txtStatureinInch
        '
        Me.txtStatureinInch.ForeColor = System.Drawing.Color.Black
        Me.txtStatureinInch.Location = New System.Drawing.Point(3, 3)
        Me.txtStatureinInch.MaxLength = 5
        Me.txtStatureinInch.Name = "txtStatureinInch"
        Me.txtStatureinInch.Size = New System.Drawing.Size(45, 22)
        Me.txtStatureinInch.TabIndex = 0
        Me.txtStatureinInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(3, 28)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(45, 12)
        Me.Label62.TabIndex = 0
        Me.Label62.Text = "(in)"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlStatureCm
        '
        Me.FLpnlStatureCm.Controls.Add(Me.txtStature)
        Me.FLpnlStatureCm.Controls.Add(Me.Label59)
        Me.FLpnlStatureCm.Location = New System.Drawing.Point(211, 3)
        Me.FLpnlStatureCm.Name = "FLpnlStatureCm"
        Me.FLpnlStatureCm.Size = New System.Drawing.Size(56, 40)
        Me.FLpnlStatureCm.TabIndex = 0
        Me.FLpnlStatureCm.Visible = False
        '
        'txtStature
        '
        Me.txtStature.ForeColor = System.Drawing.Color.Black
        Me.txtStature.Location = New System.Drawing.Point(3, 3)
        Me.txtStature.MaxLength = 5
        Me.txtStature.Name = "txtStature"
        Me.txtStature.Size = New System.Drawing.Size(45, 22)
        Me.txtStature.TabIndex = 0
        Me.txtStature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.Transparent
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(3, 28)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(45, 12)
        Me.Label59.TabIndex = 0
        Me.Label59.Text = "(cm)"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel19
        '
        Me.FlowLayoutPanel19.Controls.Add(Me.lblstin)
        Me.FlowLayoutPanel19.Controls.Add(Me.lblstcm)
        Me.FlowLayoutPanel19.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel19.Location = New System.Drawing.Point(273, 3)
        Me.FlowLayoutPanel19.Name = "FlowLayoutPanel19"
        Me.FlowLayoutPanel19.Size = New System.Drawing.Size(120, 38)
        Me.FlowLayoutPanel19.TabIndex = 4
        Me.FlowLayoutPanel19.TabStop = True
        '
        'lblstin
        '
        Me.lblstin.AutoSize = True
        Me.lblstin.BackColor = System.Drawing.Color.Transparent
        Me.lblstin.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblstin.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstin.ForeColor = System.Drawing.Color.Red
        Me.lblstin.Location = New System.Drawing.Point(3, 0)
        Me.lblstin.Name = "lblstin"
        Me.lblstin.Padding = New System.Windows.Forms.Padding(4)
        Me.lblstin.Size = New System.Drawing.Size(34, 19)
        Me.lblstin.TabIndex = 2
        Me.lblstin.Text = "(123)"
        Me.lblstin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblstcm
        '
        Me.lblstcm.AutoSize = True
        Me.lblstcm.BackColor = System.Drawing.Color.Transparent
        Me.lblstcm.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblstcm.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstcm.ForeColor = System.Drawing.Color.Red
        Me.lblstcm.Location = New System.Drawing.Point(3, 19)
        Me.lblstcm.Name = "lblstcm"
        Me.lblstcm.Padding = New System.Windows.Forms.Padding(4)
        Me.lblstcm.Size = New System.Drawing.Size(34, 19)
        Me.lblstcm.TabIndex = 1
        Me.lblstcm.Text = "(123)"
        Me.lblstcm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Flpnllefteyepressure
        '
        Me.Flpnllefteyepressure.Controls.Add(Me.Label9)
        Me.Flpnllefteyepressure.Controls.Add(Me.FlowLayoutPanel6)
        Me.Flpnllefteyepressure.Dock = System.Windows.Forms.DockStyle.Top
        Me.Flpnllefteyepressure.Location = New System.Drawing.Point(3, 565)
        Me.Flpnllefteyepressure.Name = "Flpnllefteyepressure"
        Me.Flpnllefteyepressure.Size = New System.Drawing.Size(577, 48)
        Me.Flpnllefteyepressure.TabIndex = 0
        Me.Flpnllefteyepressure.Visible = False
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 28)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Left Eye Pressure :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel6
        '
        Me.FlowLayoutPanel6.Controls.Add(Me.txtlefteyepressure)
        Me.FlowLayoutPanel6.Controls.Add(Me.Label74)
        Me.FlowLayoutPanel6.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel6.Name = "FlowLayoutPanel6"
        Me.FlowLayoutPanel6.Size = New System.Drawing.Size(56, 40)
        Me.FlowLayoutPanel6.TabIndex = 0
        '
        'txtlefteyepressure
        '
        Me.txtlefteyepressure.Location = New System.Drawing.Point(3, 3)
        Me.txtlefteyepressure.MaxLength = 5
        Me.txtlefteyepressure.Name = "txtlefteyepressure"
        Me.txtlefteyepressure.Size = New System.Drawing.Size(49, 22)
        Me.txtlefteyepressure.TabIndex = 0
        Me.txtlefteyepressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(3, 28)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(49, 12)
        Me.Label74.TabIndex = 0
        Me.Label74.Text = "(mm Hg)"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLPnlTemp
        '
        Me.FLPnlTemp.AutoSize = True
        Me.FLPnlTemp.Controls.Add(Me.lbltemp)
        Me.FLPnlTemp.Controls.Add(Me.FLpnlTempfarenht)
        Me.FLPnlTemp.Controls.Add(Me.FLPnlTempcelcius)
        Me.FLPnlTemp.Controls.Add(Me.FlowLayoutPanel18)
        Me.FLPnlTemp.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlTemp.Location = New System.Drawing.Point(3, 419)
        Me.FLPnlTemp.Name = "FLPnlTemp"
        Me.FLPnlTemp.Size = New System.Drawing.Size(577, 45)
        Me.FLPnlTemp.TabIndex = 0
        Me.FLPnlTemp.Visible = False
        '
        'lbltemp
        '
        Me.lbltemp.Location = New System.Drawing.Point(3, 0)
        Me.lbltemp.Name = "lbltemp"
        Me.lbltemp.Size = New System.Drawing.Size(140, 29)
        Me.lbltemp.TabIndex = 0
        Me.lbltemp.Text = "Temperature :"
        Me.lbltemp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlTempfarenht
        '
        Me.FLpnlTempfarenht.Controls.Add(Me.txtTemperature)
        Me.FLpnlTempfarenht.Controls.Add(Me.Label29)
        Me.FLpnlTempfarenht.Location = New System.Drawing.Point(149, 3)
        Me.FLpnlTempfarenht.Name = "FLpnlTempfarenht"
        Me.FLpnlTempfarenht.Size = New System.Drawing.Size(56, 38)
        Me.FLpnlTempfarenht.TabIndex = 0
        Me.FLpnlTempfarenht.Visible = False
        '
        'txtTemperature
        '
        Me.txtTemperature.ForeColor = System.Drawing.Color.Black
        Me.txtTemperature.Location = New System.Drawing.Point(3, 3)
        Me.txtTemperature.MaxLength = 6
        Me.txtTemperature.Name = "txtTemperature"
        Me.txtTemperature.Size = New System.Drawing.Size(45, 22)
        Me.txtTemperature.TabIndex = 0
        Me.txtTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(3, 28)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(45, 10)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "(F)"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLPnlTempcelcius
        '
        Me.FLPnlTempcelcius.Controls.Add(Me.txtTemperatureCelcius)
        Me.FLPnlTempcelcius.Controls.Add(Me.Label7)
        Me.FLPnlTempcelcius.Location = New System.Drawing.Point(211, 3)
        Me.FLPnlTempcelcius.Name = "FLPnlTempcelcius"
        Me.FLPnlTempcelcius.Size = New System.Drawing.Size(56, 39)
        Me.FLPnlTempcelcius.TabIndex = 0
        Me.FLPnlTempcelcius.Visible = False
        '
        'txtTemperatureCelcius
        '
        Me.txtTemperatureCelcius.ForeColor = System.Drawing.Color.Black
        Me.txtTemperatureCelcius.Location = New System.Drawing.Point(3, 3)
        Me.txtTemperatureCelcius.MaxLength = 6
        Me.txtTemperatureCelcius.Name = "txtTemperatureCelcius"
        Me.txtTemperatureCelcius.Size = New System.Drawing.Size(45, 22)
        Me.txtTemperatureCelcius.TabIndex = 0
        Me.txtTemperatureCelcius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 10)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "(C)"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel18
        '
        Me.FlowLayoutPanel18.AutoSize = True
        Me.FlowLayoutPanel18.Controls.Add(Me.lbltmpf)
        Me.FlowLayoutPanel18.Controls.Add(Me.lbltc)
        Me.FlowLayoutPanel18.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel18.Location = New System.Drawing.Point(273, 3)
        Me.FlowLayoutPanel18.Name = "FlowLayoutPanel18"
        Me.FlowLayoutPanel18.Size = New System.Drawing.Size(57, 38)
        Me.FlowLayoutPanel18.TabIndex = 3
        Me.FlowLayoutPanel18.TabStop = True
        '
        'lbltmpf
        '
        Me.lbltmpf.AutoSize = True
        Me.lbltmpf.BackColor = System.Drawing.Color.Transparent
        Me.lbltmpf.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbltmpf.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltmpf.ForeColor = System.Drawing.Color.Red
        Me.lbltmpf.Location = New System.Drawing.Point(3, 0)
        Me.lbltmpf.Name = "lbltmpf"
        Me.lbltmpf.Padding = New System.Windows.Forms.Padding(4)
        Me.lbltmpf.Size = New System.Drawing.Size(51, 19)
        Me.lbltmpf.TabIndex = 0
        Me.lbltmpf.Text = "(Systolic)"
        Me.lbltmpf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbltc
        '
        Me.lbltc.AutoSize = True
        Me.lbltc.BackColor = System.Drawing.Color.Transparent
        Me.lbltc.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbltc.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltc.ForeColor = System.Drawing.Color.Red
        Me.lbltc.Location = New System.Drawing.Point(3, 19)
        Me.lbltc.Name = "lbltc"
        Me.lbltc.Padding = New System.Windows.Forms.Padding(4)
        Me.lbltc.Size = New System.Drawing.Size(51, 19)
        Me.lbltc.TabIndex = 1
        Me.lbltc.Text = "(Systolic)"
        Me.lbltc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlHeadcircumference
        '
        Me.FLpnlHeadcircumference.Controls.Add(Me.lblCircum)
        Me.FLpnlHeadcircumference.Controls.Add(Me.FLPnHClInch)
        Me.FLpnlHeadcircumference.Controls.Add(Me.FLpnlHCCm)
        Me.FLpnlHeadcircumference.Controls.Add(Me.FlowLayoutPanel21)
        Me.FLpnlHeadcircumference.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlHeadcircumference.Location = New System.Drawing.Point(3, 619)
        Me.FLpnlHeadcircumference.Name = "FLpnlHeadcircumference"
        Me.FLpnlHeadcircumference.Size = New System.Drawing.Size(577, 48)
        Me.FLpnlHeadcircumference.TabIndex = 0
        Me.FLpnlHeadcircumference.Visible = False
        '
        'lblCircum
        '
        Me.lblCircum.Location = New System.Drawing.Point(3, 0)
        Me.lblCircum.Name = "lblCircum"
        Me.lblCircum.Size = New System.Drawing.Size(140, 33)
        Me.lblCircum.TabIndex = 0
        Me.lblCircum.Text = "Head Circumference :"
        Me.lblCircum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLPnHClInch
        '
        Me.FLPnHClInch.Controls.Add(Me.txtCircumInch)
        Me.FLPnHClInch.Controls.Add(Me.label53)
        Me.FLPnHClInch.Location = New System.Drawing.Point(149, 3)
        Me.FLPnHClInch.Name = "FLPnHClInch"
        Me.FLPnHClInch.Size = New System.Drawing.Size(56, 39)
        Me.FLPnHClInch.TabIndex = 0
        Me.FLPnHClInch.Visible = False
        '
        'txtCircumInch
        '
        Me.txtCircumInch.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtCircumInch.ForeColor = System.Drawing.Color.Black
        Me.txtCircumInch.Location = New System.Drawing.Point(3, 3)
        Me.txtCircumInch.MaxLength = 5
        Me.txtCircumInch.Name = "txtCircumInch"
        Me.txtCircumInch.Size = New System.Drawing.Size(45, 22)
        Me.txtCircumInch.TabIndex = 0
        Me.txtCircumInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label53
        '
        Me.label53.BackColor = System.Drawing.Color.Transparent
        Me.label53.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label53.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label53.Location = New System.Drawing.Point(3, 28)
        Me.label53.Name = "label53"
        Me.label53.Size = New System.Drawing.Size(45, 10)
        Me.label53.TabIndex = 0
        Me.label53.Text = "(in)"
        Me.label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlHCCm
        '
        Me.FLpnlHCCm.Controls.Add(Me.txtCircum)
        Me.FLpnlHCCm.Controls.Add(Me.label54)
        Me.FLpnlHCCm.Location = New System.Drawing.Point(211, 3)
        Me.FLpnlHCCm.Name = "FLpnlHCCm"
        Me.FLpnlHCCm.Size = New System.Drawing.Size(58, 39)
        Me.FLpnlHCCm.TabIndex = 0
        Me.FLpnlHCCm.Visible = False
        '
        'txtCircum
        '
        Me.txtCircum.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtCircum.ForeColor = System.Drawing.Color.Black
        Me.txtCircum.Location = New System.Drawing.Point(3, 3)
        Me.txtCircum.MaxLength = 5
        Me.txtCircum.Name = "txtCircum"
        Me.txtCircum.Size = New System.Drawing.Size(45, 22)
        Me.txtCircum.TabIndex = 0
        Me.txtCircum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label54
        '
        Me.label54.BackColor = System.Drawing.Color.Transparent
        Me.label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label54.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label54.Location = New System.Drawing.Point(3, 28)
        Me.label54.Name = "label54"
        Me.label54.Size = New System.Drawing.Size(46, 10)
        Me.label54.TabIndex = 0
        Me.label54.Text = "(cm)"
        Me.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel21
        '
        Me.FlowLayoutPanel21.AutoSize = True
        Me.FlowLayoutPanel21.Controls.Add(Me.lblhcin)
        Me.FlowLayoutPanel21.Controls.Add(Me.lblhccm)
        Me.FlowLayoutPanel21.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel21.Location = New System.Drawing.Point(275, 3)
        Me.FlowLayoutPanel21.Name = "FlowLayoutPanel21"
        Me.FlowLayoutPanel21.Size = New System.Drawing.Size(57, 38)
        Me.FlowLayoutPanel21.TabIndex = 4
        Me.FlowLayoutPanel21.TabStop = True
        '
        'lblhcin
        '
        Me.lblhcin.AutoSize = True
        Me.lblhcin.BackColor = System.Drawing.Color.Transparent
        Me.lblhcin.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblhcin.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhcin.ForeColor = System.Drawing.Color.Red
        Me.lblhcin.Location = New System.Drawing.Point(3, 0)
        Me.lblhcin.Name = "lblhcin"
        Me.lblhcin.Padding = New System.Windows.Forms.Padding(4)
        Me.lblhcin.Size = New System.Drawing.Size(51, 19)
        Me.lblhcin.TabIndex = 0
        Me.lblhcin.Text = "(Systolic)"
        Me.lblhcin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblhccm
        '
        Me.lblhccm.AutoSize = True
        Me.lblhccm.BackColor = System.Drawing.Color.Transparent
        Me.lblhccm.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblhccm.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhccm.ForeColor = System.Drawing.Color.Red
        Me.lblhccm.Location = New System.Drawing.Point(3, 19)
        Me.lblhccm.Name = "lblhccm"
        Me.lblhccm.Padding = New System.Windows.Forms.Padding(4)
        Me.lblhccm.Size = New System.Drawing.Size(51, 19)
        Me.lblhccm.TabIndex = 1
        Me.lblhccm.Text = "(Systolic)"
        Me.lblhccm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLPnlDAS28
        '
        Me.FLPnlDAS28.Controls.Add(Me.Label145)
        Me.FLPnlDAS28.Controls.Add(Me.FlowLayoutPanel22)
        Me.FLPnlDAS28.Controls.Add(Me.Label146)
        Me.FLPnlDAS28.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLPnlDAS28.Location = New System.Drawing.Point(3, 673)
        Me.FLPnlDAS28.Name = "FLPnlDAS28"
        Me.FLPnlDAS28.Size = New System.Drawing.Size(577, 35)
        Me.FLPnlDAS28.TabIndex = 5
        Me.FLPnlDAS28.Visible = False
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.Transparent
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label145.Location = New System.Drawing.Point(3, 0)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(260, 29)
        Me.Label145.TabIndex = 0
        Me.Label145.Text = "DAS 28 (Use the DAS-28 button to change) :"
        Me.Label145.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel22
        '
        Me.FlowLayoutPanel22.AutoSize = True
        Me.FlowLayoutPanel22.Controls.Add(Me.txtDAS28)
        Me.FlowLayoutPanel22.Location = New System.Drawing.Point(269, 3)
        Me.FlowLayoutPanel22.Name = "FlowLayoutPanel22"
        Me.FlowLayoutPanel22.Size = New System.Drawing.Size(51, 28)
        Me.FlowLayoutPanel22.TabIndex = 0
        '
        'txtDAS28
        '
        Me.txtDAS28.BackColor = System.Drawing.Color.White
        Me.txtDAS28.ForeColor = System.Drawing.Color.Black
        Me.txtDAS28.Location = New System.Drawing.Point(3, 3)
        Me.txtDAS28.MaxLength = 5
        Me.txtDAS28.Name = "txtDAS28"
        Me.txtDAS28.Size = New System.Drawing.Size(45, 22)
        Me.txtDAS28.TabIndex = 0
        Me.txtDAS28.TabStop = False
        Me.txtDAS28.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.Transparent
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(326, 0)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(16, 35)
        Me.Label146.TabIndex = 1
        Me.Label146.Text = "%"
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label146.Visible = False
        '
        'FlPnlODI
        '
        Me.FlPnlODI.Controls.Add(Me.Label143)
        Me.FlPnlODI.Controls.Add(Me.FlowLayoutPanel16)
        Me.FlPnlODI.Controls.Add(Me.Label144)
        Me.FlPnlODI.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlPnlODI.Location = New System.Drawing.Point(586, 673)
        Me.FlPnlODI.Name = "FlPnlODI"
        Me.FlPnlODI.Size = New System.Drawing.Size(614, 35)
        Me.FlPnlODI.TabIndex = 3
        Me.FlPnlODI.Visible = False
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.Transparent
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label143.Location = New System.Drawing.Point(3, 0)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(140, 27)
        Me.Label143.TabIndex = 0
        Me.Label143.Text = "ODI :"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FlowLayoutPanel16
        '
        Me.FlowLayoutPanel16.Controls.Add(Me.txtODI)
        Me.FlowLayoutPanel16.Location = New System.Drawing.Point(149, 3)
        Me.FlowLayoutPanel16.Name = "FlowLayoutPanel16"
        Me.FlowLayoutPanel16.Size = New System.Drawing.Size(50, 28)
        Me.FlowLayoutPanel16.TabIndex = 0
        '
        'txtODI
        '
        Me.txtODI.BackColor = System.Drawing.Color.White
        Me.txtODI.ForeColor = System.Drawing.Color.Black
        Me.txtODI.Location = New System.Drawing.Point(3, 3)
        Me.txtODI.MaxLength = 3
        Me.txtODI.Name = "txtODI"
        Me.txtODI.Size = New System.Drawing.Size(45, 22)
        Me.txtODI.TabIndex = 0
        Me.txtODI.TabStop = False
        Me.txtODI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.Transparent
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label144.Location = New System.Drawing.Point(205, 0)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(16, 35)
        Me.Label144.TabIndex = 1
        Me.Label144.Text = "%"
        Me.Label144.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label144.Visible = False
        '
        'FlPnlWithMedication
        '
        Me.FlPnlWithMedication.AutoSize = True
        Me.FlPnlWithMedication.Controls.Add(Me.Label77)
        Me.FlPnlWithMedication.Controls.Add(Me.chkPainWithMedication)
        Me.FlPnlWithMedication.Controls.Add(Me.Panel3)
        Me.FlPnlWithMedication.Location = New System.Drawing.Point(3, 339)
        Me.FlPnlWithMedication.Name = "FlPnlWithMedication"
        Me.FlPnlWithMedication.Size = New System.Drawing.Size(420, 74)
        Me.FlPnlWithMedication.TabIndex = 1
        Me.FlPnlWithMedication.Visible = False
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.Transparent
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(3, 0)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(114, 31)
        Me.Label77.TabIndex = 0
        Me.Label77.Text = "Pain level : With Medication"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkPainWithMedication
        '
        Me.chkPainWithMedication.Location = New System.Drawing.Point(123, 3)
        Me.chkPainWithMedication.Name = "chkPainWithMedication"
        Me.chkPainWithMedication.Padding = New System.Windows.Forms.Padding(3)
        Me.chkPainWithMedication.Size = New System.Drawing.Size(17, 14)
        Me.chkPainWithMedication.TabIndex = 1
        Me.chkPainWithMedication.Text = "Allow"
        Me.chkPainWithMedication.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label92)
        Me.Panel3.Controls.Add(Me.Label93)
        Me.Panel3.Controls.Add(Me.Label94)
        Me.Panel3.Controls.Add(Me.Label95)
        Me.Panel3.Controls.Add(Me.Label96)
        Me.Panel3.Controls.Add(Me.Label97)
        Me.Panel3.Controls.Add(Me.Label78)
        Me.Panel3.Controls.Add(Me.Label79)
        Me.Panel3.Controls.Add(Me.Label80)
        Me.Panel3.Controls.Add(Me.Label81)
        Me.Panel3.Controls.Add(Me.Label82)
        Me.Panel3.Controls.Add(Me.Label83)
        Me.Panel3.Controls.Add(Me.Label84)
        Me.Panel3.Controls.Add(Me.Label85)
        Me.Panel3.Controls.Add(Me.Label86)
        Me.Panel3.Controls.Add(Me.Label87)
        Me.Panel3.Controls.Add(Me.Label88)
        Me.Panel3.Controls.Add(Me.Label89)
        Me.Panel3.Controls.Add(Me.Label90)
        Me.Panel3.Controls.Add(Me.Label91)
        Me.Panel3.Controls.Add(Me.Label98)
        Me.Panel3.Controls.Add(Me.trbPainWithMedication)
        Me.Panel3.Location = New System.Drawing.Point(146, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(271, 68)
        Me.Panel3.TabIndex = 0
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.Location = New System.Drawing.Point(242, 8)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(23, 11)
        Me.Label92.TabIndex = 4
        Me.Label92.Text = "Pain"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.Location = New System.Drawing.Point(19, 8)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(23, 11)
        Me.Label93.TabIndex = 5
        Me.Label93.Text = "Pain"
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(130, 8)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(23, 11)
        Me.Label94.TabIndex = 6
        Me.Label94.Text = "Pain"
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(193, 8)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(52, 11)
        Me.Label95.TabIndex = 1
        Me.Label95.Text = "Unbearable"
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(88, 8)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(44, 11)
        Me.Label96.TabIndex = 2
        Me.Label96.Text = "Moderate"
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(5, 8)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(17, 11)
        Me.Label97.TabIndex = 3
        Me.Label97.Text = "No"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label78.Location = New System.Drawing.Point(1, 67)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(269, 1)
        Me.Label78.TabIndex = 0
        Me.Label78.Text = "label2"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(0, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 67)
        Me.Label79.TabIndex = 0
        Me.Label79.Text = "label4"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(270, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 67)
        Me.Label80.TabIndex = 0
        Me.Label80.Text = "label3"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(271, 1)
        Me.Label81.TabIndex = 0
        Me.Label81.Text = "label1"
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(241, 51)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(19, 12)
        Me.Label82.TabIndex = 0
        Me.Label82.Text = "10"
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(218, 51)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(12, 12)
        Me.Label83.TabIndex = 0
        Me.Label83.Text = "9"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(195, 51)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(12, 12)
        Me.Label84.TabIndex = 0
        Me.Label84.Text = "8"
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(172, 51)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(12, 12)
        Me.Label85.TabIndex = 0
        Me.Label85.Text = "7"
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.Location = New System.Drawing.Point(149, 51)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(12, 12)
        Me.Label86.TabIndex = 0
        Me.Label86.Text = "6"
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label87.Location = New System.Drawing.Point(126, 51)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(12, 12)
        Me.Label87.TabIndex = 0
        Me.Label87.Text = "5"
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(103, 51)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(12, 12)
        Me.Label88.TabIndex = 0
        Me.Label88.Text = "4"
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.Location = New System.Drawing.Point(80, 51)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(12, 12)
        Me.Label89.TabIndex = 0
        Me.Label89.Text = "3"
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(57, 51)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(12, 12)
        Me.Label90.TabIndex = 0
        Me.Label90.Text = "2"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.Location = New System.Drawing.Point(34, 51)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(12, 12)
        Me.Label91.TabIndex = 0
        Me.Label91.Text = "1"
        '
        'Label98
        '
        Me.Label98.AutoSize = True
        Me.Label98.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(11, 51)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(12, 12)
        Me.Label98.TabIndex = 0
        Me.Label98.Text = "0"
        '
        'trbPainWithMedication
        '
        Me.trbPainWithMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainWithMedication.LargeChange = 1
        Me.trbPainWithMedication.Location = New System.Drawing.Point(3, 21)
        Me.trbPainWithMedication.Name = "trbPainWithMedication"
        Me.trbPainWithMedication.Size = New System.Drawing.Size(258, 45)
        Me.trbPainWithMedication.TabIndex = 0
        '
        'FLpnlHeight
        '
        Me.FLpnlHeight.Controls.Add(Me.lblHeight1)
        Me.FLpnlHeight.Controls.Add(Me.FLpnlFt)
        Me.FLpnlHeight.Controls.Add(Me.FLpnlInch)
        Me.FLpnlHeight.Controls.Add(Me.FLPnlCm)
        Me.FLpnlHeight.Controls.Add(Me.FlowLayoutPanel23)
        Me.FLpnlHeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.FLpnlHeight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FLpnlHeight.Location = New System.Drawing.Point(3, 285)
        Me.FLpnlHeight.Name = "FLpnlHeight"
        Me.FLpnlHeight.Size = New System.Drawing.Size(577, 48)
        Me.FLpnlHeight.TabIndex = 0
        Me.FLpnlHeight.Visible = False
        '
        'lblHeight1
        '
        Me.lblHeight1.Location = New System.Drawing.Point(3, 0)
        Me.lblHeight1.Name = "lblHeight1"
        Me.lblHeight1.Size = New System.Drawing.Size(140, 27)
        Me.lblHeight1.TabIndex = 0
        Me.lblHeight1.Text = "Height/Length :"
        Me.lblHeight1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FLpnlFt
        '
        Me.FLpnlFt.Controls.Add(Me.txtft)
        Me.FLpnlFt.Controls.Add(Me.Label60)
        Me.FLpnlFt.Controls.Add(Me.txtInch)
        Me.FLpnlFt.Controls.Add(Me.Label61)
        Me.FLpnlFt.Controls.Add(Me.Label19)
        Me.FLpnlFt.Controls.Add(Me.Label2)
        Me.FLpnlFt.Location = New System.Drawing.Point(149, 3)
        Me.FLpnlFt.Name = "FLpnlFt"
        Me.FLpnlFt.Size = New System.Drawing.Size(121, 39)
        Me.FLpnlFt.TabIndex = 0
        Me.FLpnlFt.Visible = False
        '
        'txtft
        '
        Me.txtft.ForeColor = System.Drawing.Color.Black
        Me.txtft.Location = New System.Drawing.Point(3, 3)
        Me.txtft.MaxLength = 1
        Me.txtft.Name = "txtft"
        Me.txtft.Size = New System.Drawing.Size(30, 22)
        Me.txtft.TabIndex = 0
        Me.txtft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label60
        '
        Me.Label60.AllowDrop = True
        Me.Label60.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(39, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(11, 14)
        Me.Label60.TabIndex = 0
        Me.Label60.Text = "'"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInch
        '
        Me.txtInch.ForeColor = System.Drawing.Color.Black
        Me.txtInch.Location = New System.Drawing.Point(56, 3)
        Me.txtInch.MaxLength = 5
        Me.txtInch.Name = "txtInch"
        Me.txtInch.Size = New System.Drawing.Size(40, 22)
        Me.txtInch.TabIndex = 0
        Me.txtInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label61
        '
        Me.Label61.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(102, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(16, 14)
        Me.Label61.TabIndex = 0
        Me.Label61.Text = "''"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(31, 10)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "(ft)"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(40, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 10)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "(in)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLpnlInch
        '
        Me.FLpnlInch.Controls.Add(Me.txtHtInInches)
        Me.FLpnlInch.Controls.Add(Me.Label26)
        Me.FLpnlInch.Location = New System.Drawing.Point(276, 3)
        Me.FLpnlInch.Name = "FLpnlInch"
        Me.FLpnlInch.Size = New System.Drawing.Size(49, 40)
        Me.FLpnlInch.TabIndex = 0
        Me.FLpnlInch.Visible = False
        '
        'txtHtInInches
        '
        Me.txtHtInInches.ForeColor = System.Drawing.Color.Black
        Me.txtHtInInches.Location = New System.Drawing.Point(3, 3)
        Me.txtHtInInches.MaxLength = 5
        Me.txtHtInInches.Name = "txtHtInInches"
        Me.txtHtInInches.Size = New System.Drawing.Size(45, 22)
        Me.txtHtInInches.TabIndex = 0
        Me.txtHtInInches.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 28)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 10)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "(in)"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FLPnlCm
        '
        Me.FLPnlCm.Controls.Add(Me.txtHtInCm)
        Me.FLPnlCm.Controls.Add(Me.Label6)
        Me.FLPnlCm.Location = New System.Drawing.Point(331, 3)
        Me.FLPnlCm.Name = "FLPnlCm"
        Me.FLPnlCm.Size = New System.Drawing.Size(54, 40)
        Me.FLPnlCm.TabIndex = 0
        Me.FLPnlCm.Visible = False
        '
        'txtHtInCm
        '
        Me.txtHtInCm.ForeColor = System.Drawing.Color.Black
        Me.txtHtInCm.Location = New System.Drawing.Point(3, 3)
        Me.txtHtInCm.MaxLength = 5
        Me.txtHtInCm.Name = "txtHtInCm"
        Me.txtHtInCm.Size = New System.Drawing.Size(45, 22)
        Me.txtHtInCm.TabIndex = 0
        Me.txtHtInCm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 10)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "(cm)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel23
        '
        Me.FlowLayoutPanel23.Controls.Add(Me.lblhin)
        Me.FlowLayoutPanel23.Controls.Add(Me.lblhcm)
        Me.FlowLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Left
        Me.FlowLayoutPanel23.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel23.Location = New System.Drawing.Point(391, 3)
        Me.FlowLayoutPanel23.Name = "FlowLayoutPanel23"
        Me.FlowLayoutPanel23.Size = New System.Drawing.Size(119, 38)
        Me.FlowLayoutPanel23.TabIndex = 3
        Me.FlowLayoutPanel23.TabStop = True
        '
        'lblhin
        '
        Me.lblhin.AutoSize = True
        Me.lblhin.BackColor = System.Drawing.Color.Transparent
        Me.lblhin.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblhin.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhin.ForeColor = System.Drawing.Color.Red
        Me.lblhin.Location = New System.Drawing.Point(3, 0)
        Me.lblhin.Name = "lblhin"
        Me.lblhin.Padding = New System.Windows.Forms.Padding(4)
        Me.lblhin.Size = New System.Drawing.Size(34, 19)
        Me.lblhin.TabIndex = 1
        Me.lblhin.Text = "(123)"
        Me.lblhin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblhcm
        '
        Me.lblhcm.AutoSize = True
        Me.lblhcm.BackColor = System.Drawing.Color.Transparent
        Me.lblhcm.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblhcm.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhcm.ForeColor = System.Drawing.Color.Red
        Me.lblhcm.Location = New System.Drawing.Point(3, 19)
        Me.lblhcm.Name = "lblhcm"
        Me.lblhcm.Padding = New System.Windows.Forms.Padding(4)
        Me.lblhcm.Size = New System.Drawing.Size(34, 19)
        Me.lblhcm.TabIndex = 2
        Me.lblhcm.Text = "(123)"
        Me.lblhcm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TLPanOB
        '
        Me.TLPanOB.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tblpnlMain.SetColumnSpan(Me.TLPanOB, 2)
        Me.TLPanOB.Controls.Add(Me.pnlRight)
        Me.TLPanOB.Controls.Add(Me.pnlMiddle)
        Me.TLPanOB.Controls.Add(Me.Label167)
        Me.TLPanOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TLPanOB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.TLPanOB.Location = New System.Drawing.Point(3, 818)
        Me.TLPanOB.Name = "TLPanOB"
        Me.TLPanOB.Size = New System.Drawing.Size(526, 144)
        Me.TLPanOB.TabIndex = 2
        Me.TLPanOB.Visible = False
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlRight.Controls.Add(Me.pnlLiving)
        Me.pnlRight.Controls.Add(Me.pnlEctopic)
        Me.pnlRight.Controls.Add(Me.pnlAborted_Induced)
        Me.pnlRight.Controls.Add(Me.pnlFullTerm)
        Me.pnlRight.Location = New System.Drawing.Point(221, 25)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(266, 118)
        Me.pnlRight.TabIndex = 3
        Me.pnlRight.Visible = False
        '
        'pnlLiving
        '
        Me.pnlLiving.Controls.Add(Me.txtLiving)
        Me.pnlLiving.Controls.Add(Me.Label159)
        Me.pnlLiving.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLiving.Location = New System.Drawing.Point(0, 84)
        Me.pnlLiving.Name = "pnlLiving"
        Me.pnlLiving.Size = New System.Drawing.Size(266, 28)
        Me.pnlLiving.TabIndex = 0
        Me.pnlLiving.Visible = False
        '
        'txtLiving
        '
        Me.txtLiving.BackColor = System.Drawing.Color.White
        Me.txtLiving.ForeColor = System.Drawing.Color.Black
        Me.txtLiving.Location = New System.Drawing.Point(157, 2)
        Me.txtLiving.MaxLength = 5
        Me.txtLiving.Name = "txtLiving"
        Me.txtLiving.Size = New System.Drawing.Size(45, 22)
        Me.txtLiving.TabIndex = 0
        Me.txtLiving.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label159
        '
        Me.Label159.AutoSize = True
        Me.Label159.BackColor = System.Drawing.Color.Transparent
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label159.Location = New System.Drawing.Point(109, 6)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(45, 14)
        Me.Label159.TabIndex = 1
        Me.Label159.Text = "Living :"
        Me.Label159.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlEctopic
        '
        Me.pnlEctopic.Controls.Add(Me.txtEctopic)
        Me.pnlEctopic.Controls.Add(Me.Label166)
        Me.pnlEctopic.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEctopic.Location = New System.Drawing.Point(0, 56)
        Me.pnlEctopic.Name = "pnlEctopic"
        Me.pnlEctopic.Size = New System.Drawing.Size(266, 28)
        Me.pnlEctopic.TabIndex = 2
        Me.pnlEctopic.Visible = False
        '
        'txtEctopic
        '
        Me.txtEctopic.BackColor = System.Drawing.Color.White
        Me.txtEctopic.ForeColor = System.Drawing.Color.Black
        Me.txtEctopic.Location = New System.Drawing.Point(157, 3)
        Me.txtEctopic.MaxLength = 5
        Me.txtEctopic.Name = "txtEctopic"
        Me.txtEctopic.Size = New System.Drawing.Size(45, 22)
        Me.txtEctopic.TabIndex = 0
        Me.txtEctopic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label166
        '
        Me.Label166.AutoSize = True
        Me.Label166.BackColor = System.Drawing.Color.Transparent
        Me.Label166.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label166.Location = New System.Drawing.Point(99, 7)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(55, 14)
        Me.Label166.TabIndex = 1
        Me.Label166.Text = "Ectopic :"
        Me.Label166.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlAborted_Induced
        '
        Me.pnlAborted_Induced.Controls.Add(Me.txtAbortedinduced)
        Me.pnlAborted_Induced.Controls.Add(Me.Label163)
        Me.pnlAborted_Induced.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAborted_Induced.Location = New System.Drawing.Point(0, 28)
        Me.pnlAborted_Induced.Name = "pnlAborted_Induced"
        Me.pnlAborted_Induced.Size = New System.Drawing.Size(266, 28)
        Me.pnlAborted_Induced.TabIndex = 1
        Me.pnlAborted_Induced.Visible = False
        '
        'txtAbortedinduced
        '
        Me.txtAbortedinduced.BackColor = System.Drawing.Color.White
        Me.txtAbortedinduced.ForeColor = System.Drawing.Color.Black
        Me.txtAbortedinduced.Location = New System.Drawing.Point(157, 3)
        Me.txtAbortedinduced.MaxLength = 5
        Me.txtAbortedinduced.Name = "txtAbortedinduced"
        Me.txtAbortedinduced.Size = New System.Drawing.Size(45, 22)
        Me.txtAbortedinduced.TabIndex = 0
        Me.txtAbortedinduced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label163
        '
        Me.Label163.AutoSize = True
        Me.Label163.BackColor = System.Drawing.Color.Transparent
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label163.Location = New System.Drawing.Point(35, 7)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(119, 14)
        Me.Label163.TabIndex = 1
        Me.Label163.Text = "Aborted (Induced) :"
        Me.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFullTerm
        '
        Me.pnlFullTerm.Controls.Add(Me.txtFullTerm)
        Me.pnlFullTerm.Controls.Add(Me.Label162)
        Me.pnlFullTerm.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFullTerm.Location = New System.Drawing.Point(0, 0)
        Me.pnlFullTerm.Name = "pnlFullTerm"
        Me.pnlFullTerm.Size = New System.Drawing.Size(266, 28)
        Me.pnlFullTerm.TabIndex = 1
        Me.pnlFullTerm.Visible = False
        '
        'txtFullTerm
        '
        Me.txtFullTerm.BackColor = System.Drawing.Color.White
        Me.txtFullTerm.ForeColor = System.Drawing.Color.Black
        Me.txtFullTerm.Location = New System.Drawing.Point(157, 3)
        Me.txtFullTerm.MaxLength = 5
        Me.txtFullTerm.Name = "txtFullTerm"
        Me.txtFullTerm.Size = New System.Drawing.Size(45, 22)
        Me.txtFullTerm.TabIndex = 0
        Me.txtFullTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label162
        '
        Me.Label162.AutoSize = True
        Me.Label162.BackColor = System.Drawing.Color.Transparent
        Me.Label162.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label162.Location = New System.Drawing.Point(89, 7)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(65, 14)
        Me.Label162.TabIndex = 1
        Me.Label162.Text = "Full Term :"
        Me.Label162.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.Transparent
        Me.pnlMiddle.Controls.Add(Me.pnlMultipleBirth)
        Me.pnlMiddle.Controls.Add(Me.pnlAborted_Spontaneous)
        Me.pnlMiddle.Controls.Add(Me.pnlPremature)
        Me.pnlMiddle.Controls.Add(Me.pnlTotalPregnancies)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 25)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(221, 119)
        Me.pnlMiddle.TabIndex = 2
        Me.pnlMiddle.Visible = False
        '
        'pnlMultipleBirth
        '
        Me.pnlMultipleBirth.Controls.Add(Me.txtMultipleBirth)
        Me.pnlMultipleBirth.Controls.Add(Me.Label168)
        Me.pnlMultipleBirth.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMultipleBirth.Location = New System.Drawing.Point(0, 84)
        Me.pnlMultipleBirth.Name = "pnlMultipleBirth"
        Me.pnlMultipleBirth.Size = New System.Drawing.Size(221, 28)
        Me.pnlMultipleBirth.TabIndex = 3
        Me.pnlMultipleBirth.Visible = False
        '
        'txtMultipleBirth
        '
        Me.txtMultipleBirth.BackColor = System.Drawing.Color.White
        Me.txtMultipleBirth.ForeColor = System.Drawing.Color.Black
        Me.txtMultipleBirth.Location = New System.Drawing.Point(162, 2)
        Me.txtMultipleBirth.MaxLength = 5
        Me.txtMultipleBirth.Name = "txtMultipleBirth"
        Me.txtMultipleBirth.Size = New System.Drawing.Size(45, 22)
        Me.txtMultipleBirth.TabIndex = 0
        Me.txtMultipleBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label168
        '
        Me.Label168.AutoSize = True
        Me.Label168.BackColor = System.Drawing.Color.Transparent
        Me.Label168.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label168.Location = New System.Drawing.Point(72, 6)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(85, 14)
        Me.Label168.TabIndex = 1
        Me.Label168.Text = "Multiple Birth :"
        Me.Label168.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlAborted_Spontaneous
        '
        Me.pnlAborted_Spontaneous.Controls.Add(Me.txtAbortedSpontaneous)
        Me.pnlAborted_Spontaneous.Controls.Add(Me.Label160)
        Me.pnlAborted_Spontaneous.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAborted_Spontaneous.Location = New System.Drawing.Point(0, 56)
        Me.pnlAborted_Spontaneous.Name = "pnlAborted_Spontaneous"
        Me.pnlAborted_Spontaneous.Size = New System.Drawing.Size(221, 28)
        Me.pnlAborted_Spontaneous.TabIndex = 10
        Me.pnlAborted_Spontaneous.Visible = False
        '
        'txtAbortedSpontaneous
        '
        Me.txtAbortedSpontaneous.BackColor = System.Drawing.Color.White
        Me.txtAbortedSpontaneous.ForeColor = System.Drawing.Color.Black
        Me.txtAbortedSpontaneous.Location = New System.Drawing.Point(162, 3)
        Me.txtAbortedSpontaneous.MaxLength = 5
        Me.txtAbortedSpontaneous.Name = "txtAbortedSpontaneous"
        Me.txtAbortedSpontaneous.Size = New System.Drawing.Size(45, 22)
        Me.txtAbortedSpontaneous.TabIndex = 0
        Me.txtAbortedSpontaneous.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label160
        '
        Me.Label160.AutoSize = True
        Me.Label160.BackColor = System.Drawing.Color.Transparent
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label160.Location = New System.Drawing.Point(11, 7)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(146, 14)
        Me.Label160.TabIndex = 1
        Me.Label160.Text = "Aborted (Spontaneous) :"
        Me.Label160.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPremature
        '
        Me.pnlPremature.Controls.Add(Me.txtPremature)
        Me.pnlPremature.Controls.Add(Me.Label165)
        Me.pnlPremature.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPremature.Location = New System.Drawing.Point(0, 28)
        Me.pnlPremature.Name = "pnlPremature"
        Me.pnlPremature.Size = New System.Drawing.Size(221, 28)
        Me.pnlPremature.TabIndex = 2
        Me.pnlPremature.Visible = False
        '
        'txtPremature
        '
        Me.txtPremature.BackColor = System.Drawing.Color.White
        Me.txtPremature.ForeColor = System.Drawing.Color.Black
        Me.txtPremature.Location = New System.Drawing.Point(162, 3)
        Me.txtPremature.MaxLength = 5
        Me.txtPremature.Name = "txtPremature"
        Me.txtPremature.Size = New System.Drawing.Size(45, 22)
        Me.txtPremature.TabIndex = 0
        Me.txtPremature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label165
        '
        Me.Label165.AutoSize = True
        Me.Label165.BackColor = System.Drawing.Color.Transparent
        Me.Label165.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label165.Location = New System.Drawing.Point(85, 7)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(72, 14)
        Me.Label165.TabIndex = 1
        Me.Label165.Text = "Premature :"
        Me.Label165.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTotalPregnancies
        '
        Me.pnlTotalPregnancies.Controls.Add(Me.Label158)
        Me.pnlTotalPregnancies.Controls.Add(Me.txtTotalPreganancy)
        Me.pnlTotalPregnancies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTotalPregnancies.Location = New System.Drawing.Point(0, 0)
        Me.pnlTotalPregnancies.Name = "pnlTotalPregnancies"
        Me.pnlTotalPregnancies.Size = New System.Drawing.Size(221, 28)
        Me.pnlTotalPregnancies.TabIndex = 3
        Me.pnlTotalPregnancies.Visible = False
        '
        'Label158
        '
        Me.Label158.AutoSize = True
        Me.Label158.BackColor = System.Drawing.Color.Transparent
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label158.Location = New System.Drawing.Point(45, 7)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(112, 14)
        Me.Label158.TabIndex = 1
        Me.Label158.Text = "Total Pregnancies :"
        Me.Label158.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalPreganancy
        '
        Me.txtTotalPreganancy.BackColor = System.Drawing.Color.White
        Me.txtTotalPreganancy.ForeColor = System.Drawing.Color.Black
        Me.txtTotalPreganancy.Location = New System.Drawing.Point(162, 3)
        Me.txtTotalPreganancy.MaxLength = 5
        Me.txtTotalPreganancy.Name = "txtTotalPreganancy"
        Me.txtTotalPreganancy.Size = New System.Drawing.Size(45, 22)
        Me.txtTotalPreganancy.TabIndex = 0
        Me.txtTotalPreganancy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label167
        '
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label167.Location = New System.Drawing.Point(0, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Padding = New System.Windows.Forms.Padding(200, 0, 0, 0)
        Me.Label167.Size = New System.Drawing.Size(526, 25)
        Me.Label167.TabIndex = 4
        Me.Label167.Text = "Obstetric History"
        Me.Label167.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlVisitdate
        '
        Me.pnlVisitdate.Controls.Add(Me.dtVitals)
        Me.pnlVisitdate.Controls.Add(Me.Label18)
        Me.pnlVisitdate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVisitdate.Location = New System.Drawing.Point(0, 56)
        Me.pnlVisitdate.Name = "pnlVisitdate"
        Me.pnlVisitdate.Size = New System.Drawing.Size(1220, 28)
        Me.pnlVisitdate.TabIndex = 0
        '
        'dtVitals
        '
        Me.dtVitals.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtVitals.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtVitals.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtVitals.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtVitals.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtVitals.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtVitals.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVitals.Location = New System.Drawing.Point(150, 4)
        Me.dtVitals.Name = "dtVitals"
        Me.dtVitals.Size = New System.Drawing.Size(194, 22)
        Me.dtVitals.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(6, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(142, 21)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Vital Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeight
        '
        Me.lblHeight.Location = New System.Drawing.Point(840, 11)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Padding = New System.Windows.Forms.Padding(0, 0, 0, 30)
        Me.lblHeight.Size = New System.Drawing.Size(40, 23)
        Me.lblHeight.TabIndex = 0
        Me.lblHeight.Text = "Height :"
        Me.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHeight.Visible = False
        '
        'FLPnlBPSetting
        '
        Me.FLPnlBPSetting.Location = New System.Drawing.Point(536, 25)
        Me.FLPnlBPSetting.Name = "FLPnlBPSetting"
        Me.FLPnlBPSetting.Size = New System.Drawing.Size(45, 25)
        Me.FLPnlBPSetting.TabIndex = 0
        Me.FLPnlBPSetting.Visible = False
        '
        'label49
        '
        Me.label49.AutoSize = True
        Me.label49.BackColor = System.Drawing.Color.Transparent
        Me.label49.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label49.Location = New System.Drawing.Point(718, 37)
        Me.label49.Name = "label49"
        Me.label49.Size = New System.Drawing.Size(16, 11)
        Me.label49.TabIndex = 0
        Me.label49.Text = "(3)"
        Me.label49.Visible = False
        '
        'label48
        '
        Me.label48.AutoSize = True
        Me.label48.BackColor = System.Drawing.Color.Transparent
        Me.label48.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label48.Location = New System.Drawing.Point(699, 35)
        Me.label48.Name = "label48"
        Me.label48.Size = New System.Drawing.Size(16, 11)
        Me.label48.TabIndex = 0
        Me.label48.Text = "(2)"
        Me.label48.Visible = False
        '
        'label47
        '
        Me.label47.AutoSize = True
        Me.label47.BackColor = System.Drawing.Color.Transparent
        Me.label47.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label47.Location = New System.Drawing.Point(683, 35)
        Me.label47.Name = "label47"
        Me.label47.Size = New System.Drawing.Size(16, 11)
        Me.label47.TabIndex = 0
        Me.label47.Text = "(1)"
        Me.label47.Visible = False
        '
        'txtPEFR3
        '
        Me.txtPEFR3.ForeColor = System.Drawing.Color.Black
        Me.txtPEFR3.Location = New System.Drawing.Point(698, 12)
        Me.txtPEFR3.MaxLength = 6
        Me.txtPEFR3.Name = "txtPEFR3"
        Me.txtPEFR3.Size = New System.Drawing.Size(54, 22)
        Me.txtPEFR3.TabIndex = 0
        Me.txtPEFR3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPEFR3.Visible = False
        '
        'txtPEFR2
        '
        Me.txtPEFR2.ForeColor = System.Drawing.Color.Black
        Me.txtPEFR2.Location = New System.Drawing.Point(632, 12)
        Me.txtPEFR2.MaxLength = 6
        Me.txtPEFR2.Name = "txtPEFR2"
        Me.txtPEFR2.Size = New System.Drawing.Size(54, 22)
        Me.txtPEFR2.TabIndex = 0
        Me.txtPEFR2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPEFR2.Visible = False
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Validate_32, Me.tblCCD, Me.tblbtn_CALBMI_32, Me.tblbtn_ODI_32, Me.tblbtn_DAS, Me.tblbtn_Capture_DeviceVitals, Me.tblbtn_OBVitals, Me.tblbtn_Ok_32, Me.tblbtn_Close_32})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1220, 53)
        Me.tblStrip.TabIndex = 0
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Validate_32
        '
        Me.tblbtn_Validate_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Validate_32.Image = CType(resources.GetObject("tblbtn_Validate_32.Image"), System.Drawing.Image)
        Me.tblbtn_Validate_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Validate_32.Name = "tblbtn_Validate_32"
        Me.tblbtn_Validate_32.Size = New System.Drawing.Size(60, 50)
        Me.tblbtn_Validate_32.Tag = "Validate"
        Me.tblbtn_Validate_32.Text = "&Validate"
        Me.tblbtn_Validate_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblCCD
        '
        Me.tblCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCD.Image = CType(resources.GetObject("tblCCD.Image"), System.Drawing.Image)
        Me.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCD.Name = "tblCCD"
        Me.tblCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblCCD.Tag = "Generate CCD"
        Me.tblCCD.Text = "&Gen CCD"
        Me.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCD.ToolTipText = "Generate CCD"
        '
        'tblbtn_CALBMI_32
        '
        Me.tblbtn_CALBMI_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_CALBMI_32.Image = CType(resources.GetObject("tblbtn_CALBMI_32.Image"), System.Drawing.Image)
        Me.tblbtn_CALBMI_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_CALBMI_32.Name = "tblbtn_CALBMI_32"
        Me.tblbtn_CALBMI_32.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_CALBMI_32.Tag = "BMI"
        Me.tblbtn_CALBMI_32.Text = "&BMI"
        Me.tblbtn_CALBMI_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_ODI_32
        '
        Me.tblbtn_ODI_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_ODI_32.Image = CType(resources.GetObject("tblbtn_ODI_32.Image"), System.Drawing.Image)
        Me.tblbtn_ODI_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_ODI_32.Name = "tblbtn_ODI_32"
        Me.tblbtn_ODI_32.Size = New System.Drawing.Size(36, 50)
        Me.tblbtn_ODI_32.Tag = "ODI"
        Me.tblbtn_ODI_32.Text = "&ODI"
        Me.tblbtn_ODI_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_DAS
        '
        Me.tblbtn_DAS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_DAS.Image = CType(resources.GetObject("tblbtn_DAS.Image"), System.Drawing.Image)
        Me.tblbtn_DAS.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblbtn_DAS.Name = "tblbtn_DAS"
        Me.tblbtn_DAS.Size = New System.Drawing.Size(58, 50)
        Me.tblbtn_DAS.Tag = "28 DAS"
        Me.tblbtn_DAS.Text = "D&AS-28"
        Me.tblbtn_DAS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_DAS.ToolTipText = "DAS Calculator"
        '
        'tblbtn_Capture_DeviceVitals
        '
        Me.tblbtn_Capture_DeviceVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Capture_DeviceVitals.Image = CType(resources.GetObject("tblbtn_Capture_DeviceVitals.Image"), System.Drawing.Image)
        Me.tblbtn_Capture_DeviceVitals.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Capture_DeviceVitals.Name = "tblbtn_Capture_DeviceVitals"
        Me.tblbtn_Capture_DeviceVitals.Size = New System.Drawing.Size(97, 50)
        Me.tblbtn_Capture_DeviceVitals.Tag = "CaptureDeviceVitals"
        Me.tblbtn_Capture_DeviceVitals.Text = "Capture &Vitals"
        Me.tblbtn_Capture_DeviceVitals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_OBVitals
        '
        Me.tblbtn_OBVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_OBVitals.Image = CType(resources.GetObject("tblbtn_OBVitals.Image"), System.Drawing.Image)
        Me.tblbtn_OBVitals.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_OBVitals.Name = "tblbtn_OBVitals"
        Me.tblbtn_OBVitals.Size = New System.Drawing.Size(65, 50)
        Me.tblbtn_OBVitals.Tag = "OBVitals"
        Me.tblbtn_OBVitals.Text = "OB V&itals"
        Me.tblbtn_OBVitals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Ok_32
        '
        Me.tblbtn_Ok_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Ok_32.Image = CType(resources.GetObject("tblbtn_Ok_32.Image"), System.Drawing.Image)
        Me.tblbtn_Ok_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Ok_32.Name = "tblbtn_Ok_32"
        Me.tblbtn_Ok_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Ok_32.Tag = "Ok"
        Me.tblbtn_Ok_32.Text = "&Save&&Cls"
        Me.tblbtn_Ok_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Ok_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lblHeight)
        Me.Panel1.Controls.Add(Me.FLPnlBPSetting)
        Me.Panel1.Controls.Add(Me.tblStrip)
        Me.Panel1.Controls.Add(Me.txtPEFR3)
        Me.Panel1.Controls.Add(Me.txtPEFR2)
        Me.Panel1.Controls.Add(Me.label47)
        Me.Panel1.Controls.Add(Me.label48)
        Me.Panel1.Controls.Add(Me.label49)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1220, 56)
        Me.Panel1.TabIndex = 0
        '
        'chkpainlvl
        '
        Me.chkpainlvl.AutoSize = True
        Me.chkpainlvl.Location = New System.Drawing.Point(4, 4)
        Me.chkpainlvl.Name = "chkpainlvl"
        Me.chkpainlvl.Padding = New System.Windows.Forms.Padding(3)
        Me.chkpainlvl.Size = New System.Drawing.Size(111, 24)
        Me.chkpainlvl.TabIndex = 1
        Me.chkpainlvl.Text = "Allow Painlevel"
        Me.chkpainlvl.UseVisualStyleBackColor = True
        '
        'pnlWait
        '
        Me.pnlWait.BackColor = System.Drawing.Color.Transparent
        Me.pnlWait.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWait.Controls.Add(Me.Label161)
        Me.pnlWait.Controls.Add(Me.Label164)
        Me.pnlWait.Location = New System.Drawing.Point(248, 330)
        Me.pnlWait.Name = "pnlWait"
        Me.pnlWait.Size = New System.Drawing.Size(423, 80)
        Me.pnlWait.TabIndex = 67
        Me.pnlWait.Visible = False
        '
        'Label161
        '
        Me.Label161.AutoSize = True
        Me.Label161.BackColor = System.Drawing.Color.Transparent
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Location = New System.Drawing.Point(20, 7)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(119, 19)
        Me.Label161.TabIndex = 61
        Me.Label161.Text = "Please wait..."
        '
        'Label164
        '
        Me.Label164.AutoSize = True
        Me.Label164.BackColor = System.Drawing.Color.Transparent
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label164.Location = New System.Drawing.Point(21, 33)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(140, 16)
        Me.Label164.TabIndex = 61
        Me.Label164.Text = "Updating Template..."
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.tblpnlMain)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 84)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1220, 656)
        Me.Panel8.TabIndex = 3
        '
        'AxAdvChart
        '
        Me.AxAdvChart.Enabled = True
        Me.AxAdvChart.Location = New System.Drawing.Point(66, 52)
        Me.AxAdvChart.Name = "AxAdvChart"
        Me.AxAdvChart.Size = New System.Drawing.Size(192, 192)
        Me.AxAdvChart.TabIndex = 0
        '
        'frmPatientVitals
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1220, 740)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.pnlVisitdate)
        Me.Controls.Add(Me.pnlWait)
        Me.Controls.Add(Me.pnlVitalEntry)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmPatientVitals"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Vitals"
        Me.pnlVitalEntry.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.FLPnlPulseOx.ResumeLayout(False)
        Me.FLPnlPulseOx.PerformLayout()
        Me.FlowLayoutPanel20.ResumeLayout(False)
        Me.FlowLayoutPanel20.PerformLayout()
        Me.FlowLayoutPanel5.ResumeLayout(False)
        Me.FlowLayoutPanel5.PerformLayout()
        Me.FLPnlSupplement.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.FlowLayoutPanel26.ResumeLayout(False)
        Me.FlowLayoutPanel26.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.FlowLayoutPanel27.ResumeLayout(False)
        Me.FlowLayoutPanel27.PerformLayout()
        Me.FLpnlPainlevel.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        CType(Me.trbPainLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlPnlWithoutMedication.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.trbPainWithoutMedication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FLpnlWeight.ResumeLayout(False)
        Me.FLpnlWeight.PerformLayout()
        Me.FLpnllbsoz.ResumeLayout(False)
        Me.FLpnllbsoz.PerformLayout()
        Me.FLpnllbs.ResumeLayout(False)
        Me.FLpnllbs.PerformLayout()
        Me.FLpnlKg.ResumeLayout(False)
        Me.FLpnlKg.PerformLayout()
        Me.FlowLayoutPanel25.ResumeLayout(False)
        Me.FlowLayoutPanel25.PerformLayout()
        Me.tblpnlMain.ResumeLayout(False)
        Me.tblpnlMain.PerformLayout()
        Me.FLPnlSiteforBP.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FLpnlcomments.ResumeLayout(False)
        Me.FLpnlcomments.PerformLayout()
        Me.FLpnlHeartrate.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.FlPnlWorst.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.trbPainWorst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FLpnlBMI.ResumeLayout(False)
        Me.FlowLayoutPanel4.ResumeLayout(False)
        Me.FlowLayoutPanel4.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.FLpnlRespiratoryrate.ResumeLayout(False)
        Me.FLpnlRespiratoryrate.PerformLayout()
        Me.FlowLayoutPanel9.ResumeLayout(False)
        Me.FlowLayoutPanel9.PerformLayout()
        Me.FlowLayoutPanel17.ResumeLayout(False)
        Me.FlowLayoutPanel17.PerformLayout()
        Me.FLPnlBPStanding.ResumeLayout(False)
        Me.FLPnlBPStanding.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.FlowLayoutPanel14.ResumeLayout(False)
        Me.FlowLayoutPanel14.PerformLayout()
        Me.FLpnlBPSitting.ResumeLayout(False)
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.FlowLayoutPanel15.ResumeLayout(False)
        Me.FlowLayoutPanel15.PerformLayout()
        Me.FlpnlRighteyepressure.ResumeLayout(False)
        Me.FlowLayoutPanel7.ResumeLayout(False)
        Me.FlowLayoutPanel7.PerformLayout()
        Me.FLpnlLastmenstrualperiod.ResumeLayout(False)
        Me.FlowLayoutPanel12.ResumeLayout(False)
        Me.FLpnlPEFR.ResumeLayout(False)
        Me.FLpnlPEFR.PerformLayout()
        Me.FlowLayoutPanel8.ResumeLayout(False)
        Me.FlowLayoutPanel8.PerformLayout()
        Me.FLpnlwtChanged.ResumeLayout(False)
        Me.FlowLayoutPanel11.ResumeLayout(False)
        Me.FlowLayoutPanel11.PerformLayout()
        Me.FLpnlneckcircum.ResumeLayout(False)
        Me.FLpnlneckcircumInch.ResumeLayout(False)
        Me.FLpnlneckcircumInch.PerformLayout()
        Me.FLneckcircumCm.ResumeLayout(False)
        Me.FLneckcircumCm.PerformLayout()
        Me.Flpnlpulspermin.ResumeLayout(False)
        Me.Flpnlpulspermin.PerformLayout()
        Me.FlowLayoutPanel10.ResumeLayout(False)
        Me.FlowLayoutPanel10.PerformLayout()
        Me.FlowLayoutPanel24.ResumeLayout(False)
        Me.FlowLayoutPanel24.PerformLayout()
        Me.FLpnlStature.ResumeLayout(False)
        Me.FLpnlStatureInch.ResumeLayout(False)
        Me.FLpnlStatureInch.PerformLayout()
        Me.FLpnlStatureCm.ResumeLayout(False)
        Me.FLpnlStatureCm.PerformLayout()
        Me.FlowLayoutPanel19.ResumeLayout(False)
        Me.FlowLayoutPanel19.PerformLayout()
        Me.Flpnllefteyepressure.ResumeLayout(False)
        Me.FlowLayoutPanel6.ResumeLayout(False)
        Me.FlowLayoutPanel6.PerformLayout()
        Me.FLPnlTemp.ResumeLayout(False)
        Me.FLPnlTemp.PerformLayout()
        Me.FLpnlTempfarenht.ResumeLayout(False)
        Me.FLpnlTempfarenht.PerformLayout()
        Me.FLPnlTempcelcius.ResumeLayout(False)
        Me.FLPnlTempcelcius.PerformLayout()
        Me.FlowLayoutPanel18.ResumeLayout(False)
        Me.FlowLayoutPanel18.PerformLayout()
        Me.FLpnlHeadcircumference.ResumeLayout(False)
        Me.FLpnlHeadcircumference.PerformLayout()
        Me.FLPnHClInch.ResumeLayout(False)
        Me.FLPnHClInch.PerformLayout()
        Me.FLpnlHCCm.ResumeLayout(False)
        Me.FLpnlHCCm.PerformLayout()
        Me.FlowLayoutPanel21.ResumeLayout(False)
        Me.FlowLayoutPanel21.PerformLayout()
        Me.FLPnlDAS28.ResumeLayout(False)
        Me.FLPnlDAS28.PerformLayout()
        Me.FlowLayoutPanel22.ResumeLayout(False)
        Me.FlowLayoutPanel22.PerformLayout()
        Me.FlPnlODI.ResumeLayout(False)
        Me.FlowLayoutPanel16.ResumeLayout(False)
        Me.FlowLayoutPanel16.PerformLayout()
        Me.FlPnlWithMedication.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.trbPainWithMedication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FLpnlHeight.ResumeLayout(False)
        Me.FLpnlFt.ResumeLayout(False)
        Me.FLpnlFt.PerformLayout()
        Me.FLpnlInch.ResumeLayout(False)
        Me.FLpnlInch.PerformLayout()
        Me.FLPnlCm.ResumeLayout(False)
        Me.FLPnlCm.PerformLayout()
        Me.FlowLayoutPanel23.ResumeLayout(False)
        Me.FlowLayoutPanel23.PerformLayout()
        Me.TLPanOB.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.pnlLiving.ResumeLayout(False)
        Me.pnlLiving.PerformLayout()
        Me.pnlEctopic.ResumeLayout(False)
        Me.pnlEctopic.PerformLayout()
        Me.pnlAborted_Induced.ResumeLayout(False)
        Me.pnlAborted_Induced.PerformLayout()
        Me.pnlFullTerm.ResumeLayout(False)
        Me.pnlFullTerm.PerformLayout()
        Me.pnlMiddle.ResumeLayout(False)
        Me.pnlMultipleBirth.ResumeLayout(False)
        Me.pnlMultipleBirth.PerformLayout()
        Me.pnlAborted_Spontaneous.ResumeLayout(False)
        Me.pnlAborted_Spontaneous.PerformLayout()
        Me.pnlPremature.ResumeLayout(False)
        Me.pnlPremature.PerformLayout()
        Me.pnlTotalPregnancies.ResumeLayout(False)
        Me.pnlTotalPregnancies.PerformLayout()
        Me.pnlVisitdate.ResumeLayout(False)
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlWait.ResumeLayout(False)
        Me.pnlWait.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.AxAdvChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip = Nothing

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            If MeHeight + gloUC_PatientStrip1.Height <= 720 Then
                Me.Height = MeHeight + gloUC_PatientStrip1.Height
            End If

            'Sanjog - Added on 2011 May 24 to show proper height
            If Me.Height >= 720 Then
                Me.Height = 720
                pnlVitalEntry.AutoScroll = True
            End If
            'Sanjog - Added on 2011 May 24 to show proper height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        If IsNothing(gloUC_PatientStrip1) = False Then
            'SLR: added a check
            If (Me.Controls.Contains(gloUC_PatientStrip1)) Then
                Me.Controls.Remove(gloUC_PatientStrip1)
            End If
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUC_PatientStrip
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.PatientVitals)
            .DTPValue = Format(dtVitals.Value, "MM/dd/yyyy hh:mm:ss tt") ''Added by Mayuri:20100113-To display both current date and time in patientdetailstrip-Issue:#5767

            .DTPEnabled = False
            .SendToBack()
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        ''

        Panel1.SendToBack()
        pnlVitalEntry.BringToFront()
        If mergeItem = 0 Or mergeItem = 2 Then
            If _IsAllItems = True Then
                groupBox1.Height = Val(MeHeight) + 10
            Else
                groupBox1.Height = Val(MeHeight) - 20
            End If

        Else
            groupBox1.Height = Val(MeHeight - 20) + 250
        End If

        groupBox1.BringToFront()

    End Sub

#End Region

    Public Sub FormatAge(ByVal BirthDate As DateTime, ByVal VitalDate As DateTime)
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        _AgeInDays = DateDiff(DateInterval.Day, BirthDate, VitalDate)
        _AgeInMonths = _AgeInDays / 30.4167

        'year and end year. 
        Years = VitalDate.Year - BirthDate.Year
        ' Check if the last year was a full year. 
        If VitalDate < BirthDate.AddYears(Years) AndAlso Years <> 0 Then
            Years -= 1
        End If
        BirthDate = BirthDate.AddYears(Years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = VitalDate.Year Then
            Months = VitalDate.Month - BirthDate.Month
        Else
            Months = (12 - BirthDate.Month) + VitalDate.Month
        End If
        ' Check if the last month was a full month. 
        If VitalDate < BirthDate.AddMonths(Months) AndAlso Months <> 0 Then
            Months -= 1
        End If
        BirthDate = BirthDate.AddMonths(Months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        Days = (VitalDate - BirthDate).Days
        'Return years & " years " & months & " months " & days & " days"
    End Sub

    Private Sub frmPatientVitals_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            Dim result As Windows.Forms.DialogResult

            If _IsRecordModified = True And _IsSaveClicked = False Then
                result = MessageBox.Show("Do you want to save Vitals?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If result = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                    _IsSaveClicked = False
                ElseIf result = Windows.Forms.DialogResult.Yes Then
                    _Validate = False
                    If AddVitals() Then
                        _Validate = True
                    Else
                        e.Cancel = True
                        _Validate = True
                        _IsSaveClicked = False
                    End If
                End If
            End If
            If _IsSaveClicked = False Then
                If OBVitalTask <> 0 Then
                    DeleteVitalTask()
                End If
            End If
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070719
            If _blnRecordLock = False Then
                '' Record is Locked on this Machine only then Unlock it
                UnLock_Transaction(TrnType.PatientVitals, _VitalID, _VitalID, _VisitDate)
            End If

            ''Code By Abhijeet on 20110321 for welch Allyn Device disconnectivity
            Disconnect()
            ''End of code by Abhijeet on 20110321 for welchAllyn device disconnectivity 

            '' <><><> Unlock the Record <><><>
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub frmPatientVitals_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try


            If _IsSaveClicked = True Then
                If IsNothing(myCaller) AndAlso IsNothing(myCaller1) Then
                Else
                    pnlWait.Visible = True
                    pnlWait.BringToFront()
                End If
            End If
            If blnOpenFromExam = True Then
                '14-May-15 Aniket: Bug #83286: EMR: Antepartum Template- In anterpartum template OB viatls table 
                If IsNothing(myCaller) = False Then
                    Me.Cursor = Cursors.WaitCursor

                    If (_IsSaveClicked = True) Then
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.Vitals) '' ("Vitals")
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBVitals)
                        'Resolve Bug #91611: 00001034: Patient Exam DOS liquid link 

                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBGeneticHistory)
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInfectionHistory)
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBInitialPhysicalExamination)
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBMedicalHistory)


                    End If
                    myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.DAS)
                    blnOpenFromExam = False
                    Me.Cursor = Cursors.Default
                End If
            End If
            'code added by dipak 20090921 for reflect changes made in vitals to message form(Liquid data field)
            If Not IsNothing(myCaller1) Then
                If (_IsSaveClicked = True) Then
                    myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.Vitals)

                    '04-May-15 Aniket: Bug #80986 ( Modified): gloEMR: OB vitals liquid link- Application does not allow user quadruple click on OB vitals liquid link
                    If Not IsNothing(myCaller) Then
                        myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.OBVitals)
                    Else
                        myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.OBVitals)
                    End If

                End If
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.DAS)
            End If
            'end code added by dipak 20090921
            Me.Cursor = Cursors.Default

            If Not IsNothing(objclsPatientVitals) Then
                objclsPatientVitals.Dispose()
                objclsPatientVitals = Nothing
            End If

            If IsNothing(gloUC_PatientStrip1) = False Then
                If (Me.Controls.Contains(gloUC_PatientStrip1)) Then
                    Me.Controls.Remove(gloUC_PatientStrip1)
                End If
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If

            'components.Dispose()

            'Aniket Resolving BUGID: 7323
            'Me.Dispose()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Patient Vitals Closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Vitals Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Try


                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

                Dim blnVitalScreenMode As Boolean = False
              
                If Me.WindowState = FormWindowState.Maximized Then
                    blnVitalScreenMode = True
                End If
                gloRegistrySetting.SetRegistryValue("VitalScreenMode", blnVitalScreenMode)

                gloRegistrySetting.CloseRegistryKey()
            Catch ex As Exception

            End Try
            pnlWait.Visible = False
        End Try

    End Sub


    Private Sub frmPatientVitals_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            AxAdvChart.CreateControl()
            Dim cnt As Integer = 0  ''Added by pranit on 20120717
            ''Added on 20150909-To hide horizontal scrollbar
            ''Design chnages in 8070 to resolve vitals resolution issue.
            Dim vwidth As Integer = SystemInformation.VerticalScrollBarWidth
            tblpnlMain.Padding = New Padding(0, 0, vwidth, 0)
            '19-Mar-15 Aniket: Resolving Bug #80451 ( Modified): Vital Screen Changes - Applicatio is giving Arithmatic overflow exception 
            txtFullTerm.MaxLength = 2
            txtPremature.MaxLength = 2
            txtAbortedinduced.MaxLength = 2
            txtEctopic.MaxLength = 2
            txtMultipleBirth.MaxLength = 2
            txtLiving.MaxLength = 2
            txtTotalPreganancy.MaxLength = 2
            txtAbortedSpontaneous.MaxLength = 2

            txtDAS28.Enabled = False
            tblbtn_DAS.Enabled = False
            getSettingsForVitalsDevice()
            ''Added by Abhijeet on 20110331 
            If Not _isVitalsDeviceEnabled = True Then
                tblbtn_Capture_DeviceVitals.Visible = False
            End If
            ''End of changes by Abhijeet on 20110331

            _isformLoad = True

            Dim objVitalNormSettings As New clsSettings
            IsVitalEnabled = objVitalNormSettings.IsSettingEnabled("VITAL_NORMS_ENABLED")
            objVitalNormSettings.Dispose()
            objVitalNormSettings = Nothing



            If blnOpenFromExamForDAS Then
                OpenDAS()
                If IsNothing(oclsDAS) Then
                    Return
                End If
            End If



            Dim rowno As Integer
            Dim colno As Integer
            trbPainLevel.Enabled = False
            trbPainWithMedication.Enabled = False
            trbPainWithoutMedication.Enabled = False
            trbPainWorst.Enabled = False
            FillcmbSiteforBP()
            Call Get_PatientDetails()
            ''
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.VitalsHeightCopyForward) = False Then
                _isVitalsHeightCopyForward = objSettings.VitalsHeightCopyForward
            Else
                _isVitalsHeightCopyForward = True
            End If

            Dim Vitals() As String
            Dim bpcnt As Integer = 0
            Dim str1 As String = ""
            Dim k As Int16

            lblhin.Text = ""
            lblhcm.Text = ""
            lblrsprt.Text = ""
            lbltc.Text = ""
            lbltmpf.Text = ""
            lblpox.Text = ""
            lblhccm.Text = ""
            lblhcin.Text = ""
            lblstcm.Text = ""
            lblstin.Text = ""
            lblppm.Text = ""
            lblbpsidia.Text = ""
            lblbpsidia.Text = ""
            lblbpsisys.Text = ""
            lblbpstdia.Text = ""
            lblbpstsys.Text = ""
            lblwt.Text = ""
            lblwoz.Text = ""
            lblwkg.Text = ""
            ''''''''''''''''' Added by Ujwala
            lblPEFR.Text = ""
            '''''''''''''''''
            lblSupplement.Text = ""  'Added by pranit

            If IsNothing(objSettings.VitalSettingsValue) = False Then

                'If objSettings.VitalSettingsValue <> "" Then

                If objSettings.VitalSettingsValue = "" Then
                    objSettings.VitalSettingsValue = "0.1-Height (ft & in),0.2-Height (in),0.3-Height (cm),1.1-Weight (lbsoz),1.2-Weight (lbs),1.3-Weight (kg),2.0-Weight Change,3.0-BMI,4.0-Respiratory Rate,5.0-Pulse Per Min,6.0-Pulse OX,6.1-Pulse Ox w/Supplemental Oxygen,7.1-BP Sitting,7.2-BP Standing,8.1-Temperature (F),8.2-Temperature (C),9.0-PEFR,10.0-Last Menstrual Period,11.1-Head Circumference (in),11.2-Head Circumference (cm),12.1-Neck Circumference (in),12.2-Neck Circumference (cm),13.1-Stature (in),13.2-Stature (cm),14.0-Left Eye Pressure Over Time,15.0-Right Eye Pressure Over Time,16.0-Heart Rate,17.1-Pain Level : Current,17.2-Pain Level : With Medication,17.3-Pain Level : Without Medication,17.4-Pain Level : Worst,18.0-OB Vitals,19.0-ODI,20.0-Comments,21.0-DAS 28,22.1-Total Pregnancies,22.2-Living,22.3-Full Term,22.4-Multiple Births,22.5-Premature,22.6-Aborted (Spontaneous),22.7-Aborted (Induced),22.8-Ectopic"
                    'Else

                End If
                SelectedVitals = objSettings.VitalSettingsValue.Trim.Split(",")

                ' By Pranit on 17 july 2012
                Dim aForLoop As Integer
                For aForLoop = 0 To SelectedVitals.Length - 1
                    If (SelectedVitals.GetValue(aForLoop).ToString().Contains("0-Pulse OX")) Then
                        cnt = cnt + 1
                    End If
                    If (SelectedVitals.GetValue(aForLoop).ToString().Contains("Pulse Ox w/Supplemental Oxygen")) Then
                        cnt = cnt + 1
                    End If
                Next
                ' End by Pranit on 17 july 2012

                If SelectedVitals.Count >= 31 Then
                    _IsAllItems = True
                Else
                    _IsAllItems = False
                End If
                For k = 0 To SelectedVitals.Length - 1
                    Vitals = SelectedVitals.GetValue(k).ToString().Split("-")
                    If str1 = "" Then
                        str1 = Vitals.GetValue(1)
                    Else
                        str1 = str1 & "," & Vitals.GetValue(1)
                    End If
                Next
                Dim j As Int64


                Dim htpnlFlag As Boolean = False
                Dim wtpnlFlag As Boolean = False
                Dim tmppnlFlag As Boolean = False
                Dim hdcrmFlag As Boolean = False
                Dim strFlag As Boolean = False
                Dim nkcirFlag As Boolean = False
                Dim ObHistoryFlag As Boolean = False
                Dim rowMergeFlag As Boolean = False
                Dim rowMergeNo As Integer = Nothing
                Dim colMergeNo As Integer = Nothing
                Dim bpsite As Integer = 0


                SelectedVitals = str1.Split(",")

                If SelectedVitals.Contains("Total Pregnancies") Then
                    pnlTotalPregnancies.Visible = True
                End If
                If SelectedVitals.Contains("Premature") Then
                    pnlPremature.Visible = True
                End If
                If SelectedVitals.Contains("Aborted (Spontaneous)") Then
                    pnlAborted_Spontaneous.Visible = True
                End If
                If SelectedVitals.Contains("Multiple Births") Then
                    pnlMultipleBirth.Visible = True
                End If
                If SelectedVitals.Contains("Full Term") Then
                    pnlFullTerm.Visible = True
                End If
                If SelectedVitals.Contains("Aborted (Induced)") Then
                    pnlAborted_Induced.Visible = True
                End If
                If SelectedVitals.Contains("Ectopic") Then
                    pnlEctopic.Visible = True
                End If
                If SelectedVitals.Contains("Living") Then
                    pnlLiving.Visible = True
                End If
                If SelectedVitals.Contains("ODI") = True Then
                    tblbtn_ODI_32.Enabled = True
                    _IsODISettingOn = True
                Else
                    tblbtn_ODI_32.Enabled = False
                    _IsODISettingOn = False
                End If

                If SelectedVitals.Contains("OB Vitals") = True Then
                    tblbtn_OBVitals.Enabled = True
                Else
                    tblbtn_OBVitals.Enabled = False
                End If


                Try

                    For m As Integer = 0 To SelectedVitals.Length - 1
                        If SelectedVitals(m) = "BP Sitting" Or SelectedVitals(m) = "BP Standing" Then
                            bpcnt = bpcnt + 1
                        End If
                    Next


                    For j = 0 To SelectedVitals.Length - 1
                        Select Case SelectedVitals(j)

                            Case "Height (ft & in)"
                                If htpnlFlag = False Then


                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        'End If
                                    End If
                                    htpnlFlag = True
                                End If
                                FLpnlHeight.Visible = True
                                FLpnlFt.Visible = True
                                txtft.TabIndex = j
                                txtInch.TabIndex = j + 1
                                _IsTextBoxPresent = True

                            Case "Height (cm)"

                                If htpnlFlag = False Then


                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        'End If
                                    End If
                                    htpnlFlag = True
                                End If




                                FLpnlHeight.Visible = True
                                FLPnlCm.Visible = True

                                txtHtInCm.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Height (in)"

                                If htpnlFlag = False Then


                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here
                                        tblpnlMain.Controls.Add(FLpnlHeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        'End If
                                    End If
                                    htpnlFlag = True
                                End If

                                FLpnlHeight.Visible = True
                                FLpnlInch.Visible = True
                                txtHtInInches.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Weight (lbs)"

                                If wtpnlFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here
                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        'End If
                                    End If
                                    wtpnlFlag = True
                                End If

                                FLpnlWeight.Visible = True
                                FLpnllbs.Visible = True


                                txtWeightlbs.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Weight (lbsoz)"


                                If wtpnlFlag = False Then

                                    If colno = 0 Then

                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here

                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        ' End If
                                    End If
                                    wtpnlFlag = True
                                End If

                                FLpnlWeight.Visible = True
                                FLpnllbsoz.Visible = True

                                txtWtLbs.TabIndex = j
                                txtWtOz.TabIndex = j + 1
                                _IsTextBoxPresent = True


                            Case "Weight (kg)"

                                If wtpnlFlag = False Then

                                    If colno = 0 Then

                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                        'End If
                                    Else ''colno is 1 here

                                        tblpnlMain.Controls.Add(FLpnlWeight, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                        'End If
                                    End If
                                    wtpnlFlag = True
                                End If

                                FLpnlWeight.Visible = True
                                FLpnlKg.Visible = True


                                txtWeightKg.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Weight Change"
                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlwtChanged, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlwtChanged, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlwtChanged.Visible = True

                                txtWeightChanged.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "BMI"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlBMI, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlBMI, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlBMI.Visible = True

                                txtBMI.TabIndex = j
                            Case "ODI"

                                If colno = 0 Then

                                    tblpnlMain.Controls.Add(FlPnlODI, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FlPnlODI, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FlPnlODI.Visible = True

                                txtODI.TabIndex = j
                            Case "DAS 28"
                                _IsDAS28SettingOn = True
                                tblbtn_DAS.Enabled = True
                                If colno = 0 Then

                                    tblpnlMain.Controls.Add(FLPnlDAS28, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        Else
                                            If mergeItem > 1 Then
                                                mergeItem = 0
                                                rowMergeNo = Nothing
                                                rowno = rowno + 2
                                                colno = 0
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = True
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            End If
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLPnlDAS28, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLPnlDAS28.Visible = True

                                txtDAS28.TabIndex = j

                            Case "Respiratory Rate"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlRespiratoryrate, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlRespiratoryrate, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlRespiratoryrate.Visible = True

                                txtRespiratory.TabIndex = j
                                '_IsTextBoxPresent = True
                            Case "Pulse Per Min"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(Flpnlpulspermin, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(Flpnlpulspermin, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                Flpnlpulspermin.Visible = True

                                txtPulsePerMinute.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Pulse OX"

                                If (cnt = 1) Then
                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    FlowLayoutPanel20.AutoSize = True
                                    FLPnlPulseOx.Visible = True
                                    FLPnlSupplement.Visible = False
                                    txtPulseOX.TabIndex = j
                                    _IsTextBoxPresent = True
                                End If

                                If (cnt = 2) Then
                                    If colno = 0 Then
                                        If tblpnlMain.RowStyles(rowno).Height < FLPnlPulseOx.Size.Height Then
                                            tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                            tblpnlMain.SetRowSpan(FLPnlPulseOx, 2)
                                            rowMergeFlag = True

                                            If mergeItem <> 0 Then
                                                If colMergeNo = 1 Then
                                                    colMergeNo = colno
                                                    rowMergeNo = rowno
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                Else
                                                    If mergeItem > 1 Then
                                                        mergeItem = 0
                                                        rowMergeNo = Nothing
                                                        rowno = rowno + 2
                                                        colno = 0
                                                    Else
                                                        colMergeNo = Nothing
                                                        rowMergeNo = Nothing
                                                        rowMergeFlag = True
                                                        colno = 0
                                                        rowno = rowno + 1
                                                        mergeItem = 1
                                                    End If
                                                End If
                                            Else
                                                rowMergeFlag = True
                                                mergeItem = 2
                                                rowMergeNo = rowno
                                                colMergeNo = colno
                                                colno = colno + 1
                                            End If
                                        Else
                                            tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                            colno = colno + 1
                                        End If
                                    Else
                                        If tblpnlMain.RowStyles(rowno).Height < FLPnlPulseOx.Size.Height Then
                                            tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                            tblpnlMain.SetRowSpan(FLPnlPulseOx, 2)
                                            rowMergeFlag = True

                                            If mergeItem <> 0 Then
                                                If colMergeNo = 0 Then
                                                    If mergeItem > 1 Then
                                                        mergeItem = 0
                                                        rowMergeNo = Nothing
                                                        rowno = rowno + 2
                                                        colno = 0
                                                    Else
                                                        colMergeNo = Nothing
                                                        rowMergeNo = Nothing
                                                        rowMergeFlag = False
                                                        colno = 0
                                                        rowno = rowno + 1
                                                        mergeItem = 1
                                                    End If
                                                End If
                                            Else
                                                mergeItem = 1
                                                rowMergeNo = rowno
                                                colMergeNo = colno
                                                colno = 0
                                                rowno = rowno + 1
                                            End If

                                        Else
                                            tblpnlMain.Controls.Add(FLPnlPulseOx, colno, rowno)
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If


                                    FLPnlPulseOx.Visible = True
                                    FLPnlSupplement.Visible = True
                                    txtPulseOX.TabIndex = j
                                    _IsTextBoxPresent = True
                                End If

                            Case "BP Sitting"

                                bpsite = bpsite + 1
                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlBPSitting, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlBPSitting, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                If bpsite = bpcnt Then
                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLPnlSiteforBP, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLPnlSiteforBP, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If

                                End If

                                'FLPnlBPSetting.Visible = True
                                FLpnlBPSitting.Visible = True
                                FLPnlSiteforBP.Visible = True

                                txtBPSittingMax.TabIndex = j
                                txtBPSittingMin.TabIndex = j + 1
                                cmbSiteforBP.TabIndex = j + 2
                                _IsTextBoxPresent = True

                            Case "BP Standing"
                                bpsite = bpsite + 1
                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLPnlBPStanding, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLPnlBPStanding, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If
                                If bpsite = bpcnt Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLPnlSiteforBP, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLPnlSiteforBP, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If

                                End If

                                'FLPnlBPSetting.Visible = True
                                FLPnlBPStanding.Visible = True
                                FLPnlSiteforBP.Visible = True


                                txtBPStandingMax.TabIndex = j
                                txtBPStandingMin.TabIndex = j + 1
                                cmbSiteforBP.TabIndex = j + 2
                                _IsTextBoxPresent = True

                            Case "Temperature (F)"
                                If tmppnlFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLPnlTemp, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLPnlTemp, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    tmppnlFlag = True
                                End If
                                FLPnlTemp.Visible = True
                                FLpnlTempfarenht.Visible = True

                                txtTemperature.TabIndex = j
                                'txtTHRMin.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Temperature (C)"
                                If tmppnlFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLPnlTemp, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLPnlTemp, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    tmppnlFlag = True
                                End If
                                FLPnlTemp.Visible = True
                                FLPnlTempcelcius.Visible = True

                                txtTemperatureCelcius.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "PEFR"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlPEFR, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlPEFR, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlPEFR.Visible = True

                                txtPEFR1.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Last Menstrual Period"


                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FLpnlLastmenstrualperiod, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FLpnlLastmenstrualperiod, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlLastmenstrualperiod.Visible = True
                                dtLastmenstrualperiod.TabIndex = j



                            Case "Head Circumference (in)"

                                If hdcrmFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlHeadcircumference, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlHeadcircumference, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    hdcrmFlag = True
                                End If

                                FLpnlHeadcircumference.Visible = True
                                FLPnHClInch.Visible = True
                                txtHtInInches.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Head Circumference (cm)"

                                If hdcrmFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlHeadcircumference, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlHeadcircumference, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    hdcrmFlag = True
                                End If

                                FLpnlHeadcircumference.Visible = True
                                FLpnlHCCm.Visible = True

                                txtCircum.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Stature (in)"

                                If strFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlStature, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlStature, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    strFlag = True
                                End If

                                FLpnlStature.Visible = True
                                FLpnlStatureInch.Visible = True


                                txtStatureinInch.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Stature (cm)"

                                If strFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlStature, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlStature, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    strFlag = True
                                End If

                                FLpnlStature.Visible = True
                                FLpnlStatureCm.Visible = True

                                txtStature.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Heart Rate"

                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlHeartrate.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlHeartrate, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlHeartrate, 2)

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlHeartrate, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlHeartrate.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlHeartrate, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlHeartrate, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlHeartrate, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlHeartrate.Visible = True

                                'FLpnlHeartrate.TabIndex = j
                                txtTHRperMin.TabIndex = j
                                txtTHRperMax.TabIndex = j + 1

                            Case "Pain Level : Current", "Pain Level"

                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlPainlevel.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlPainlevel, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlPainlevel, 2)

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            Else
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If

                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlPainlevel, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlPainlevel.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlPainlevel, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlPainlevel, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlPainlevel, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FLpnlPainlevel.Visible = True

                                panel2.TabIndex = j
                            Case "Pain Level : With Medication"
                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWithMedication.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWithMedication, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWithMedication, 2)

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            Else
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWithMedication, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWithMedication.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWithMedication, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWithMedication, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWithMedication, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FlPnlWithMedication.Visible = True

                                panel2.TabIndex = j
                            Case "Pain Level : Without Medication"
                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWithoutMedication.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWithoutMedication, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWithoutMedication, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            Else
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWithoutMedication, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWithoutMedication.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWithoutMedication, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWithoutMedication, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If


                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWithoutMedication, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If


                                FlPnlWithoutMedication.Visible = True

                                panel2.TabIndex = j
                            Case "Pain Level : Worst"
                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWorst.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWorst, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWorst, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            Else
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWorst, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FlPnlWorst.Size.Height Then
                                        tblpnlMain.Controls.Add(FlPnlWorst, colno, rowno)
                                        tblpnlMain.SetRowSpan(FlPnlWorst, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If


                                    Else
                                        tblpnlMain.Controls.Add(FlPnlWorst, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FlPnlWorst.Visible = True

                                panel2.TabIndex = j

                            Case "Comments"
                                If colno = 0 Then
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlcomments.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlcomments, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlcomments, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = colno
                                                rowMergeNo = rowno
                                                rowMergeFlag = True
                                                colno = colno + 1
                                                rowno = rowno + 1
                                                mergeItem = 1
                                            Else
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = True
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            rowMergeFlag = True
                                            mergeItem = 2
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlcomments, colno, rowno)
                                        colno = colno + 1
                                    End If
                                Else
                                    If tblpnlMain.RowStyles(rowno).Height < FLpnlcomments.Size.Height Then
                                        tblpnlMain.Controls.Add(FLpnlcomments, colno, rowno)
                                        tblpnlMain.SetRowSpan(FLpnlcomments, 2)
                                        rowMergeFlag = True

                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = 0
                                                    rowMergeNo = Nothing
                                                    rowno = rowno + 2
                                                    colno = 0
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                End If
                                            End If
                                        Else
                                            mergeItem = 1
                                            rowMergeNo = rowno
                                            colMergeNo = colno
                                            colno = 0
                                            rowno = rowno + 1
                                        End If

                                    Else
                                        tblpnlMain.Controls.Add(FLpnlcomments, colno, rowno)
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If


                                FLpnlcomments.Visible = True

                                txtComment.TabIndex = j
                            Case "Neck Circumference (in)"

                                If nkcirFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlneckcircum, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlneckcircum, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    nkcirFlag = True
                                End If
                                FLpnlneckcircum.Visible = True
                                FLpnlneckcircumInch.Visible = True

                                txtneckcircumInch.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Neck Circumference (cm)"

                                If nkcirFlag = False Then

                                    If colno = 0 Then
                                        tblpnlMain.Controls.Add(FLpnlneckcircum, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 1 Then
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        Else
                                            colno = colno + 1
                                        End If
                                    Else
                                        tblpnlMain.Controls.Add(FLpnlneckcircum, colno, rowno)
                                        If mergeItem <> 0 Then
                                            If colMergeNo = 0 Then
                                                If mergeItem > 1 Then
                                                    mergeItem = mergeItem - 1
                                                    rowMergeNo = rowno + 1
                                                    rowno = rowno + 1
                                                Else
                                                    colMergeNo = Nothing
                                                    rowMergeNo = Nothing
                                                    rowMergeFlag = False
                                                    colno = 0
                                                    rowno = rowno + 1
                                                    mergeItem = 0
                                                End If
                                            End If
                                        Else
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If
                                    nkcirFlag = True
                                End If

                                FLpnlneckcircum.Visible = True
                                FLneckcircumCm.Visible = True
                                txtneckcircumCm.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Left Eye Pressure Over Time"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(Flpnllefteyepressure, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(Flpnllefteyepressure, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                Flpnllefteyepressure.Visible = True
                                txtlefteyepressure.TabIndex = j
                                _IsTextBoxPresent = True
                            Case "Right Eye Pressure Over Time"

                                If colno = 0 Then
                                    tblpnlMain.Controls.Add(FlpnlRighteyepressure, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 1 Then
                                            colMergeNo = Nothing
                                            rowMergeNo = Nothing
                                            rowMergeFlag = False
                                            colno = 0
                                            rowno = rowno + 1
                                            mergeItem = 0
                                        End If
                                    Else
                                        colno = colno + 1
                                    End If
                                Else
                                    tblpnlMain.Controls.Add(FlpnlRighteyepressure, colno, rowno)
                                    If mergeItem <> 0 Then
                                        If colMergeNo = 0 Then
                                            If mergeItem > 1 Then
                                                mergeItem = mergeItem - 1
                                                rowMergeNo = rowno + 1
                                                rowno = rowno + 1
                                            Else
                                                colMergeNo = Nothing
                                                rowMergeNo = Nothing
                                                rowMergeFlag = False
                                                colno = 0
                                                rowno = rowno + 1
                                                mergeItem = 0
                                            End If
                                        End If
                                    Else
                                        colno = 0
                                        rowno = rowno + 1
                                    End If
                                End If

                                FlpnlRighteyepressure.Visible = True
                                txtRighteyepressure.TabIndex = j
                                _IsTextBoxPresent = True

                            Case "Total Pregnancies", "Full Term", "Premature", "Aborted (Induced)", "Aborted (Spontaneous)", "Ectopic", "Multiple Births", "Living"
                                If ObHistoryFlag = False Then
                                    If colno = 0 Then
                                        If tblpnlMain.RowStyles(rowno).Height < TLPanOB.Size.Height Then
                                            tblpnlMain.Controls.Add(TLPanOB, colno, rowno)
                                            tblpnlMain.SetRowSpan(TLPanOB, 2)
                                            tblpnlMain.SetColumnSpan(TLPanOB, 1)
                                            rowMergeFlag = True

                                            If mergeItem <> 0 Then
                                                If colMergeNo = 1 Then
                                                    colMergeNo = colno
                                                    rowMergeNo = rowno
                                                    rowMergeFlag = True
                                                    colno = colno + 1
                                                    rowno = rowno + 1
                                                    mergeItem = 1
                                                Else
                                                    If mergeItem > 1 Then
                                                        mergeItem = 0
                                                        rowMergeNo = Nothing
                                                        rowno = rowno + 2
                                                        colno = 0
                                                    Else
                                                        colMergeNo = Nothing
                                                        rowMergeNo = Nothing
                                                        rowMergeFlag = True
                                                        colno = 0
                                                        rowno = rowno + 2
                                                        mergeItem = 1
                                                    End If
                                                End If
                                            Else
                                                rowMergeFlag = True
                                                mergeItem = 2
                                                rowMergeNo = rowno
                                                colMergeNo = colno
                                                colno = colno + 1
                                            End If
                                        Else
                                            tblpnlMain.Controls.Add(TLPanOB, colno, rowno)
                                            colno = colno + 1
                                        End If
                                    Else
                                        rowno = rowno + 1
                                        colno = 0
                                        If tblpnlMain.RowStyles(rowno).Height < TLPanOB.Size.Height Then
                                            tblpnlMain.Controls.Add(TLPanOB, colno, rowno)
                                            tblpnlMain.SetRowSpan(TLPanOB, 2)
                                            tblpnlMain.SetColumnSpan(TLPanOB, 1)
                                            rowMergeFlag = True

                                            If mergeItem <> 0 Then
                                                If colMergeNo = 0 Then
                                                    If mergeItem > 1 Then
                                                        mergeItem = 0
                                                        rowMergeNo = Nothing
                                                        rowno = rowno + 2
                                                        colno = 0
                                                    Else
                                                        colMergeNo = Nothing
                                                        rowMergeNo = Nothing
                                                        rowMergeFlag = False
                                                        colno = 0
                                                        rowno = rowno + 1
                                                        mergeItem = 1
                                                    End If
                                                End If
                                            Else
                                                mergeItem = 1
                                                rowMergeNo = rowno
                                                colMergeNo = colno
                                                colno = 0
                                                rowno = rowno + 1
                                            End If

                                        Else
                                            tblpnlMain.Controls.Add(TLPanOB, colno, rowno)
                                            colno = 0
                                            rowno = rowno + 1
                                        End If
                                    End If

                                    TLPanOB.Visible = True
                                    _IsTextBoxPresent = True
                                    ObHistoryFlag = True
                                End If

                                If Convert.ToString(SelectedVitals(j)) = "Total Pregnancies" Then
                                    pnlMiddle.Visible = True
                                    pnlTotalPregnancies.Visible = True
                                    txtTotalPreganancy.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Living" Then
                                    pnlRight.Visible = True
                                    pnlLiving.Visible = True
                                    txtLiving.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Full Term" Then
                                    pnlRight.Visible = True
                                    pnlFullTerm.Visible = True
                                    txtFullTerm.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Premature" Then
                                    pnlMiddle.Visible = True
                                    pnlPremature.Visible = True
                                    txtPremature.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Multiple Births" Then
                                    pnlMiddle.Visible = True
                                    pnlMultipleBirth.Visible = True
                                    txtMultipleBirth.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Aborted (Induced)" Then
                                    '  pnlRight.BringToFront()
                                    pnlRight.Visible = True
                                    pnlAborted_Induced.Visible = True
                                    txtAbortedinduced.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Aborted (Spontaneous)" Then
                                    pnlMiddle.Visible = True
                                    'pnlRight.BringToFront()
                                    'pnlRight.Visible = True
                                    pnlAborted_Spontaneous.Visible = True
                                    txtAbortedSpontaneous.TabIndex = j
                                ElseIf Convert.ToString(SelectedVitals(j)) = "Ectopic" Then
                                    '   pnlRight.BringToFront()
                                    pnlRight.Visible = True
                                    pnlEctopic.Visible = True
                                    txtEctopic.TabIndex = j


                                End If
                        End Select

                    Next
                Catch ex As Exception

                End Try

                For j = SelectedVitals.Length - 1 To j = 0 Step -1
                    Select Case SelectedVitals(j)

                        Case "Height (ft & in)"
                            FLpnlFt.BringToFront()
                            lblHeight1.BringToFront()
                            FLpnlHeight.BringToFront()


                        Case "Height (cm)"
                            FLPnlCm.BringToFront()
                            lblHeight1.BringToFront()
                            FLpnlHeight.BringToFront()

                        Case "Height (in)"
                            FLpnlInch.BringToFront()
                            lblHeight1.BringToFront()
                            FLpnlHeight.BringToFront()


                        Case "Weight (lbs)"
                            FLpnllbs.BringToFront()
                            lblWeight.BringToFront()
                            FLpnlWeight.BringToFront()

                        Case "Weight (lbsoz)"

                            FLpnllbsoz.BringToFront()
                            lblWeight.BringToFront()
                            FLpnlWeight.BringToFront()


                        Case "Weight (kg)"
                            FLpnlKg.BringToFront()
                            lblWeight.BringToFront()
                            FLpnlWeight.BringToFront()

                        Case "Weight Change"

                            FLpnlwtChanged.BringToFront()
                        Case "BMI"
                            FLpnlBMI.BringToFront()

                        Case "Respiratory Rate"

                            FLpnlRespiratoryrate.BringToFront()

                        Case "Pulse Per Min"

                            Flpnlpulspermin.BringToFront()
                        Case "Pulse OX"

                            FLPnlPulseOx.BringToFront()
                        Case "BP Sitting"



                            FLpnlBPSitting.BringToFront()
                            FLPnlBPSetting.BringToFront()

                        Case "BP Standing"

                            FLPnlBPStanding.BringToFront()
                            FLPnlBPSetting.BringToFront()

                        Case "Temperature (F)"

                            FLpnlTempfarenht.BringToFront()
                            lbltemp.BringToFront()
                            FLPnlTemp.BringToFront()

                        Case "Temperature (C)"

                            FLPnlTempcelcius.BringToFront()
                            lbltemp.BringToFront()
                            FLPnlTemp.BringToFront()
                        Case "PEFR"

                            FLpnlPEFR.BringToFront()
                        Case "Last Menstrual Period"

                            FLpnlLastmenstrualperiod.BringToFront()
                        Case "Head Circumference (in)"

                            FLPnHClInch.BringToFront()
                            lblCircum.BringToFront()
                            FLpnlHeadcircumference.BringToFront()
                            '    'End If

                        Case "Head Circumference (cm)"

                            FLpnlHCCm.BringToFront()
                            lblCircum.BringToFront()
                            FLpnlHeadcircumference.BringToFront()
                        Case "Stature (in)"
                            FLpnlStatureInch.BringToFront()
                            lblStature.BringToFront()
                            FLpnlStature.BringToFront()

                        Case "Stature in (cm)"
                            FLpnlStatureCm.BringToFront()
                            lblStature.BringToFront()
                            FLpnlStature.BringToFront()



                        Case "Heart Rate"


                            FLpnlHeartrate.BringToFront()


                        Case "Pain Level : Current,Pain Level"

                            FLpnlPainlevel.BringToFront()

                        Case "Pain Level : With Medication"

                            FlPnlWithMedication.BringToFront()
                        Case "Pain Level : Without Medication"

                            FlPnlWithoutMedication.BringToFront()
                        Case "Pain Level : Worst"

                            FlPnlWorst.BringToFront()



                        Case "Comments"

                            FLpnlcomments.BringToFront()
                        Case "ODI"
                            FlPnlODI.BringToFront()
                        Case "DAS 28"
                            FLPnlDAS28.BringToFront()
                        Case "Neck Circumference (Inches)"
                            FLpnlneckcircumInch.BringToFront()
                            lblneckcircum.BringToFront()
                            FLpnlneckcircum.BringToFront()

                        Case "Neck Circumference (cm)"

                            FLneckcircumCm.BringToFront()
                            lblneckcircum.BringToFront()
                            FLpnlneckcircum.BringToFront()
                        Case "Left Eye Pressure Over Time"
                            Flpnllefteyepressure.BringToFront()
                        Case "Right Eye Pressure Over Time"
                            FlpnlRighteyepressure.BringToFront()


                    End Select

                Next

            Else
                SetControlsVisible()
            End If
            'End If
            '''''''''''add on 20100624 for Height of form
            'If SelectedVitals.Count >= 27 And SelectedVitals.Count <= 30 Then
            '    rowno = rowno + 1
            'End If
            If rowno = 0 Then
                If mergeItem = 0 Then
                    Me.Height = 237 + (rowno - 1) * 61
                Else
                    Me.Height = 237 + rowno * 61
                End If

            Else
                If mergeItem = 0 Then
                    If colno = 0 Then
                        Me.Height = 237 + (rowno - 2) * 61
                    Else
                        Me.Height = 237 + (rowno - 1) * 61
                    End If
                Else
                    If mergeItem > 1 Then
                        Me.Height = 230 + rowno * 61
                    Else
                        If colno = 0 Then
                            Me.Height = 230 + (rowno - 1) * 61
                        Else
                            Me.Height = 230 + (rowno - 1) * 61
                        End If
                    End If

                End If
            End If

            '''''''''''add on 20100624 for Height of form

            tblpnlMain.BringToFront()
            objSettings.Dispose()
            objSettings = Nothing

            lblPatientCode.Text = strPatientCode
            lblPatientCode.Tag = _PatientID
            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName

            'lblVisitDate.Text = _VisitDate
            Dim CurrentDtTime As String = Format(Now, "hh:mm:ss tt")
            lblVisitDate.Text = Format(_VisitDate, "MM/dd/yyyy") & " " & CurrentDtTime
            lblVisitDate.Tag = _VisitID  '' VisitID
            ''''

            ''
            txtWeightChanged.ReadOnly = True
            txtWeightChanged.BackColor = Color.White
            ''

            '' to Initallize / Reset 
            Call AddVital()
            If mergeItem = 0 Or mergeItem = 2 Then
                If _IsAllItems = True Then
                    MeHeight = Val(Me.Height) + 10
                Else
                    MeHeight = Val(Me.Height) - 30
                End If

            Else
                MeHeight = Val(Me.Height)
            End If



            _DateOfBirth = objclsPatientVitals.GetPatientDOB(_PatientID)

            FormatAge(_DateOfBirth, Now.Date)

            ' BMIPercentile()

            txtWeightlbs.Tag = objclsPatientVitals.GetPrevWeight(_VitalID, lblPatientCode.Tag, dtVitals.Value)
            ''
            ''Added by Mayuri:20101207-Vitals height copy forward settings added
            If _isVitalsHeightCopyForward = True Then
                If _VitalID = 0 Then
                    If Val(txtft.Text) = 0 And Val(txtInch.Text) = 0 Then
                        Dim arrHeight() As String
                        Dim strPrevHeight As String
                        strPrevHeight = objclsPatientVitals.GetPrevHeight(_PatientID)
                        If strPrevHeight <> "" Then
                            arrHeight = GetFtInch(strPrevHeight)

                            If arrHeight.Length > 1 Then
                                txtft.Text = arrHeight(0)
                                txtInch.Text = arrHeight(1)
                            End If
                        End If
                    End If
                End If
            End If
            ''End 20101207

            If _VitalID = 0 Then
                Dim dtOb As DataTable
                dtOb = objclsPatientVitals.GetPrevObHistory(_PatientID)
                If Not IsNothing(dtOb) Then
                    If dtOb.Rows.Count > 0 Then

                        If Convert.ToString(dtOb.Rows(0)("nTotalPregnancies")) = "0" Then
                            txtTotalPreganancy.Text = ""
                        Else
                            txtTotalPreganancy.Text = Convert.ToString(dtOb.Rows(0)("nTotalPregnancies"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nFullTermDeliveries")) = "0" Then
                            txtFullTerm.Text = ""
                        Else
                            txtFullTerm.Text = Convert.ToString(dtOb.Rows(0)("nFullTermDeliveries"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nLivingChildren")) = "0" Then
                            txtLiving.Text = ""
                        Else
                            txtLiving.Text = Convert.ToString(dtOb.Rows(0)("nLivingChildren"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nMultipleBirths")) = "0" Then
                            txtMultipleBirth.Text = ""
                        Else
                            txtMultipleBirth.Text = Convert.ToString(dtOb.Rows(0)("nMultipleBirths"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nPremature")) = "0" Then
                            txtPremature.Text = ""
                        Else
                            txtPremature.Text = Convert.ToString(dtOb.Rows(0)("nPremature"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nAbortionsInduced")) = "0" Then
                            txtAbortedinduced.Text = ""
                        Else
                            txtAbortedinduced.Text = Convert.ToString(dtOb.Rows(0)("nAbortionsInduced"))
                        End If
                        If Convert.ToString(dtOb.Rows(0)("nAbortionsSpontaneous")) = "0" Then
                            txtAbortedSpontaneous.Text = ""
                        Else
                            txtAbortedSpontaneous.Text = Convert.ToString(dtOb.Rows(0)("nAbortionsSpontaneous"))
                        End If

                        If Convert.ToString(dtOb.Rows(0)("nEctopics")) = "0" Then
                            txtEctopic.Text = ""
                        Else
                            txtEctopic.Text = Convert.ToString(dtOb.Rows(0)("nEctopics"))
                        End If

                    End If
                End If
                blnMedCatatRisk = objclsPatientVitals.CheckMedicalCatAtRisk(_PatientID) '' for new vital check it patient is already having Medical Category At Risk Present
                If IsNothing(dtOb) = False Then
                    dtOb.Dispose()
                    dtOb = Nothing
                End If
            End If
            Set_PatientDetailStrip()
            If _VitalID > 0 Then

                Dim dt As DataTable = objclsPatientVitals.SelectPatientVital(_VitalID)

                dtVitals.Tag = _VitalID

                If _IsMakeAsCurrent = True Then

                    _VisitID = GenerateVisitID(_PatientID)
                    lblVisitDate.Tag = Convert.ToString(_VisitID)
                Else
                    lblVisitDate.Tag = dt.Rows(0)(0).ToString
                End If

                lblPatientCode.Tag = dt.Rows(0)(1).ToString


                dtVitals.Value = Format(dt.Rows(0)(2), "MM/dd/yyyy hh:mm:ss tt")
                lblVisitDate.Text = Format(dtVitals.Value, "MM/dd/yyyy")

                If Not TypeOf (dt.Rows(0)(3)) Is System.DBNull Then
                    Dim arrHeight() As String
                    arrHeight = GetFtInch(dt.Rows(0)(3).ToString)
                    txtft.Text = arrHeight(0)
                    txtInch.Text = arrHeight(1)
                End If

                txtTotalPreganancy.Text = Convert.ToString(dt.Rows(0)("TotalPregnancies"))
                txtFullTerm.Text = Convert.ToString(dt.Rows(0)("FullTerm"))
                txtLiving.Text = Convert.ToString(dt.Rows(0)("Living"))
                txtMultipleBirth.Text = Convert.ToString(dt.Rows(0)("MultipleBirth"))
                txtPremature.Text = Convert.ToString(dt.Rows(0)("Premature"))
                txtAbortedinduced.Text = Convert.ToString(dt.Rows(0)("AbortedInduced"))
                txtAbortedSpontaneous.Text = Convert.ToString(dt.Rows(0)("AbortedSpontaneous"))
                txtEctopic.Text = Convert.ToString(dt.Rows(0)("Ectopics"))

                If Val(dt.Rows(0)(4).ToString) = 0 Then
                    txtWeightlbs.Text = ""
                Else
                    txtWeightlbs.Text = Val(dt.Rows(0)(4).ToString)
                End If

                If Val(dt.Rows(0)(5).ToString) = 0 Then
                    txtWeightChanged.Text = ""
                Else
                    txtWeightChanged.Text = Val(dt.Rows(0)(5).ToString)
                End If

                If Val(dt.Rows(0)(6).ToString) = 0 Then
                    txtBMI.Text = ""
                Else
                    txtBMI.Text = Val(dt.Rows(0)(6).ToString)
                End If

                If Val(dt.Rows(0)(7).ToString) = 0 Then
                    txtWeightKg.Text = ""
                Else
                    txtWeightKg.Text = Val(dt.Rows(0)(7).ToString)
                End If

                If Val(dt.Rows(0)(8).ToString) = 0 Then
                    txtTemperature.Text = ""
                Else
                    txtTemperature.Text = Val(dt.Rows(0)(8).ToString)
                End If

                If Val(dt.Rows(0)(9).ToString) = 0 Then
                    txtRespiratory.Text = ""
                Else
                    txtRespiratory.Text = Val(dt.Rows(0)(9).ToString)
                End If

                If Val(dt.Rows(0)(10).ToString) = 0 Then
                    txtPulsePerMinute.Text = ""
                Else
                    txtPulsePerMinute.Text = Val(dt.Rows(0)(10).ToString)
                End If

                If Val(dt.Rows(0)(11).ToString) = 0 Then
                    txtPulseOX.Text = ""
                Else
                    txtPulseOX.Text = Val(dt.Rows(0)(11).ToString)
                End If

                If Val(dt.Rows(0)(12).ToString) = 0 Then
                    txtBPSittingMin.Text = ""
                Else
                    txtBPSittingMin.Text = Val(dt.Rows(0)(12).ToString)
                End If

                If Val(dt.Rows(0)(13).ToString) = 0 Then
                    txtBPSittingMax.Text = ""
                Else
                    txtBPSittingMax.Text = Val(dt.Rows(0)(13).ToString)
                End If

                If Val(dt.Rows(0)(14).ToString) = 0 Then
                    txtBPStandingMin.Text = ""
                Else
                    txtBPStandingMin.Text = Val(dt.Rows(0)(14).ToString)
                End If

                If Val(dt.Rows(0)(15).ToString) = 0 Then
                    txtBPStandingMax.Text = ""
                Else
                    txtBPStandingMax.Text = Val(dt.Rows(0)(15).ToString)
                End If

                txtComment.Text = dt.Rows(0)(16).ToString

                If Val(dt.Rows(0)(17).ToString) = 0 Then
                    txtCircum.Text = ""
                Else
                    txtCircum.Text = Val(dt.Rows(0)(17).ToString)
                End If

                If Val(dt.Rows(0)(18).ToString) = 0 Then
                    txtStature.Text = ""
                Else
                    txtStature.Text = Val(dt.Rows(0)(18).ToString)
                End If

                ' txtft.Focus()

                If _IsMakeAsCurrent = True Then
                    dtVitals.Value = Format(Now, "MM/dd/yyyy hh:mm:ss tt")
                    lblVisitDate.Text = Format(Now, "MM/dd/yyyy")
                    _PrevVitalID = _VitalID
                    _VitalID = 0
                    dtVitals.Tag = 0
                End If
                If Val(dt.Rows(0)(19).ToString) = 0 Then
                    txtTHRperMin.Text = ""
                Else
                    txtTHRperMin.Text = Val(dt.Rows(0)(19).ToString)
                End If

                If Val(dt.Rows(0)(20).ToString) = 0 Then
                    txtTHRperMax.Text = ""
                Else
                    txtTHRperMax.Text = Val(dt.Rows(0)(20).ToString)
                End If

                If Val(dt.Rows(0)(21).ToString) = 0 Then
                    txtTHRMin.Text = "" 'Convert.ToString(220 - (Years * 75 / 100))
                Else
                    txtTHRMin.Text = Val(dt.Rows(0)(21).ToString)
                End If

                If Val(dt.Rows(0)(22).ToString) = 0 Then
                    txtTHRMax.Text = "" 'Convert.ToString(220 - (Years * 85 / 100))
                Else
                    txtTHRMax.Text = Val(dt.Rows(0)(22).ToString)
                End If

                If Val(dt.Rows(0)(23).ToString) = 0 Then
                    txtTHR.Text = Convert.ToString(220 - Years)
                Else
                    txtTHR.Text = Val(dt.Rows(0)(23).ToString)
                End If

                ''sudhir 20081128
                If Val(dt.Rows(0)("dHeightinInch").ToString) = 0 Then
                    txtHtInInches.Text = ""
                Else
                    txtHtInInches.Text = Val(dt.Rows(0)("dHeightinInch").ToString)
                End If

                If Val(dt.Rows(0)("dHeightinCm").ToString) = 0 Then
                    txtHtInCm.Text = ""
                Else
                    txtHtInCm.Text = Val(dt.Rows(0)("dHeightinCm").ToString)
                End If
                'If Val(dt.Rows(0)("nODIPercent").ToString) = 0 Then
                '    txtODI.Text = ""
                'Else
                txtODI.Text = Val(dt.Rows(0)("nODIPercent").ToString)
                If Not blnOpenFromExamForDAS Then
                    txtDAS28.Text = Val(dt.Rows(0)("nDAS28").ToString)
                End If
                ' End If
                Dim arrLbsOz() As String
                If Not IsDBNull(dt.Rows(0)("sWeightinLbsOz")) Then
                    arrLbsOz = GetLbsOz(dt.Rows(0)("sWeightinLbsOz").ToString)
                    txtWtLbs.Text = Val(arrLbsOz(0))
                    If arrLbsOz.Length > 1 Then
                        txtWtOz.Text = Val(arrLbsOz(1))
                    End If
                End If


                If Val(dt.Rows(0)("dTemperatureinCelcius").ToString) = 0 Then
                    txtTemperatureCelcius.Text = ""
                Else
                    txtTemperatureCelcius.Text = Val(dt.Rows(0)("dTemperatureinCelcius").ToString)
                End If


                If dt.Rows(0)("nPainLevel").ToString().Trim() = "" Then
                    chkpain.Checked = False
                    trbPainLevel.Enabled = False
                Else
                    chkpain.Checked = True
                    trbPainLevel.Enabled = True
                    trbPainLevel.Value = Val(dt.Rows(0)("nPainLevel").ToString)

                End If
                If dt.Rows(0)("nPainLevelWithMedication").ToString().Trim() = "" Then
                    chkPainWithMedication.Checked = False
                    trbPainWithMedication.Enabled = False
                Else
                    chkPainWithMedication.Checked = True
                    trbPainWithMedication.Enabled = True
                    trbPainWithMedication.Value = Val(dt.Rows(0)("nPainLevelWithMedication").ToString)
                End If
                If dt.Rows(0)("nPainLevelWithoutMedication").ToString().Trim() = "" Then
                    chkPainWithoutMedication.Checked = False
                    trbPainWithoutMedication.Enabled = False
                Else
                    chkPainWithoutMedication.Checked = True
                    trbPainWithoutMedication.Enabled = True
                    trbPainWithoutMedication.Value = Val(dt.Rows(0)("nPainLevelWithoutMedication").ToString)
                End If
                If dt.Rows(0)("nPainLevelWorst").ToString().Trim() = "" Then
                    chkPainWorst.Checked = False
                    trbPainWorst.Enabled = False
                Else
                    chkPainWorst.Checked = True
                    trbPainWorst.Enabled = True
                    trbPainWorst.Value = Val(dt.Rows(0)("nPainLevelWorst").ToString)
                End If


                If Val(dt.Rows(0)("dPEFR1").ToString) = 0 Then
                    txtPEFR1.Text = ""
                Else
                    txtPEFR1.Text = Val(dt.Rows(0)("dPEFR1").ToString)
                End If

                If Val(dt.Rows(0)("dPEFR2").ToString) = 0 Then
                    txtPEFR2.Text = ""
                Else
                    txtPEFR2.Text = Val(dt.Rows(0)("dPEFR2").ToString)
                End If

                If Val(dt.Rows(0)("dPEFR3").ToString) = 0 Then
                    txtPEFR3.Text = ""
                Else
                    txtPEFR3.Text = Val(dt.Rows(0)("dPEFR3").ToString)
                End If

                If Val(dt.Rows(0)("dHeadCircuminInch").ToString) = 0 Then
                    txtCircumInch.Text = ""
                Else
                    txtCircumInch.Text = Val(dt.Rows(0)("dHeadCircuminInch").ToString)
                End If

                If Val(dt.Rows(0)("dStatureinInch").ToString) = 0 Then
                    txtStatureinInch.Text = ""
                Else
                    txtStatureinInch.Text = Val(dt.Rows(0)("dStatureinInch").ToString)
                End If

                'Dim ageInMonths As Int16 = (Years * 12) + Months
                ''Added by Mayuri:20100612
                If dt.Rows(0)("SiteForBloodPressure").ToString = "" Then
                    cmbSiteforBP.Text = ""
                Else
                    cmbSiteforBP.Text = dt.Rows(0)("SiteForBloodPressure").ToString
                End If

                If Val(dt.Rows(0)("LastMenstrualPeriod").ToString) = 0 Then

                Else
                    dtLastmenstrualperiod.Value = Format(dt.Rows(0)("LastMenstrualPeriod"), "MM/dd/yyyy")
                End If

                If Val(dt.Rows(0)("NeckCircumferanceinCm").ToString) = 0 Then
                    txtneckcircumCm.Text = ""
                Else
                    txtneckcircumCm.Text = Val(dt.Rows(0)("NeckCircumferanceinCm").ToString)
                End If

                If Val(dt.Rows(0)("NeckCircumferanceinInch").ToString) = 0 Then
                    txtneckcircumInch.Text = ""
                Else
                    txtneckcircumInch.Text = Val(dt.Rows(0)("NeckCircumferanceinInch").ToString)
                End If

                If Val(dt.Rows(0)("LeftEyePressure").ToString) = 0 Then
                    txtlefteyepressure.Text = ""
                Else
                    txtlefteyepressure.Text = Val(dt.Rows(0)("LeftEyePressure").ToString)
                End If

                If Val(dt.Rows(0)("RightEyePressure").ToString) = 0 Then
                    txtRighteyepressure.Text = ""
                Else
                    txtRighteyepressure.Text = Val(dt.Rows(0)("RightEyePressure").ToString)
                End If
                If dt.Rows(0)("StatusLMPeriod").ToString <> "" Then

                    If dt.Rows(0)("StatusLMPeriod").ToString = True Then
                        dtLastmenstrualperiod.Checked = True
                    Else
                        dtLastmenstrualperiod.Checked = False
                    End If
                End If


                'By Pranit on 17 july 2012

                If Val(dt.Rows(0)("PulseOxSupplement").ToString) = 0 Then
                    txtSupplement.Text = ""
                Else
                    txtSupplement.Text = Val(dt.Rows(0)("PulseOxSupplement").ToString)
                End If


                If Val(dt.Rows(0)("PulseRate").ToString) = 0 Then
                    txtPulseRate.Text = ""
                Else
                    txtPulseRate.Text = Val(dt.Rows(0)("PulseRate").ToString)
                End If
                'End by Pranit
                dt.Dispose()
                dt = Nothing

            Else
                txtTHRperMin.Text = 75
                txtTHRperMax.Text = 85
                txtTHRMin.Text = Convert.ToString((220 - Years) * (75 / 100))
                txtTHRMax.Text = Convert.ToString((220 - Years) * (85 / 100))
                txtTHR.Text = Convert.ToString(220 - Years)

            End If

            txtBMI.Enabled = False
            txtBMI.BackColor = SystemColors.Window
            txtBMIPercentile.Enabled = False
            txtBMIPercentile.BackColor = SystemColors.Window

            ' Set_PatientDetailStrip()


            Me.Height += 85


            If Me.Height >= 720 Then
                '   Me.AutoScroll = False
                Me.Height = 720
                pnlVitalEntry.AutoScroll = True
                'Panel8.AutoScroll = False
                'tblpnlMain.AutoScroll = False
                'pnlVitalEntry.Height = 900

                'Panel8.Dock = DockStyle.None
                ''   Panel8.AutoScroll = False

                'tblpnlMain.Dock = DockStyle.None
                'Panel8.Height = 900
                'tblpnlMain.Height = 900
            End If
            If (txtSupplement.Text = "") Then
                txtPulseRate.Text = ""
                txtPulseRate.Enabled = False
            Else
                txtPulseRate.Enabled = True
            End If
            '  pt = Me.tblpnlMain.Location

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Vitals Opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)


            Dim tom As New gloCommon.Cls_TabIndexSettings(Me)
            ' '' This method actually sets the order all the way down the control hierarchy.
            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            tom.SetTabOrder(scheme)
            'FLpnlBPSitting.Height = 43
            'FLPnlBPStanding.Height = 43
            For Each ctrl As Control In Me.Controls
                getPatientStripControl(ctrl)
            Next
            Try

          
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            Dim blnVitalScreenMode As Boolean = False
            If IsNothing(gloRegistrySetting.GetRegistryValue("VitalScreenMode")) = False Then
                blnVitalScreenMode = Convert.ToBoolean(gloRegistrySetting.GetRegistryValue("VitalScreenMode"))
            End If
            If (blnVitalScreenMode = True) Then
                Me.WindowState = FormWindowState.Maximized
            End If

                gloRegistrySetting.CloseRegistryKey()
            Catch ex As Exception

            End Try
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            tom = Nothing
            scheme = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                tblbtn_Ok_32.Enabled = False
            End If
            Dim nMonths As Single = 0
            nMonths = Convert.ToSingle(Convert.ToInt16(_AgeInMonths))
            If nMonths > 240 Then
                txtBMIPercentile.Visible = False ''bugid 117253  Percentile Textbox is displaying for the patient above 20 years
                lblBMIPercentile.Visible = False
            End If
            _IsRecordsLoading = False
        End Try
    End Sub

    Private Sub getPatientStripControl(PatStripctrl As Control) ''Finding Controls Inside PatientStrip Controls
        For Each Chchildctrl As Control In PatStripctrl.Controls


            SetControlHeight(Chchildctrl)

            If Chchildctrl.HasChildren Then
                getPatientStripControl(Chchildctrl)
            End If

        Next
    End Sub
    Private Sub SetControlHeight(PatStripctrl As Control) ''
        If (PatStripctrl.GetType() Is GetType(FlowLayoutPanel)) Then
            If (PatStripctrl.Height > 45) Then
                PatStripctrl.Height -= 5
                'Else
                '    If (PatStripctrl.Height >= 40) Then
                '        PatStripctrl.Height -= 24
                '    End If
            End If
            ''setFont(PatStripctrl)
        End If
    End Sub
    Private Sub SetControlsVisible()
        Try
            FLpnlHeight.Visible = True
            FLpnlFt.Visible = True
            FLpnlInch.Visible = True
            FLPnlCm.Visible = True
            FLpnlWeight.Visible = True
            FLpnllbs.Visible = True
            FLpnllbsoz.Visible = True
            FLpnlKg.Visible = True
            FLpnlwtChanged.Visible = True
            FLpnlBMI.Visible = True
            FLpnlRespiratoryrate.Visible = True
            Flpnlpulspermin.Visible = True
            FLPnlPulseOx.Visible = True
            FLPnlBPSetting.Visible = True
            FLpnlBPSitting.Visible = True
            FLPnlBPStanding.Visible = True
            FLPnlSiteforBP.Visible = True
            FLPnlTemp.Visible = True
            FLPnlTempcelcius.Visible = True
            FLpnlTempfarenht.Visible = True
            FLpnlPEFR.Visible = True
            FLpnlLastmenstrualperiod.Visible = True
            FLpnlHeadcircumference.Visible = True
            FLpnlHCCm.Visible = True
            FLPnHClInch.Visible = True
            FLpnlneckcircum.Visible = True
            FLpnlneckcircumInch.Visible = True
            FLneckcircumCm.Visible = True
            FLpnlStature.Visible = True
            FLpnlStatureInch.Visible = True
            FLpnlStatureCm.Visible = True
            Flpnllefteyepressure.Visible = True
            FlpnlRighteyepressure.Visible = True
            FLpnlHeartrate.Visible = True
            FLpnlPainlevel.Visible = True
            FlPnlODI.Visible = True

            FLpnlcomments.Visible = True
            FLPnlDAS28.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillcmbSiteforBP()
        Try
            cmbSiteforBP.Items.Add("Right Arm")
            cmbSiteforBP.Items.Add("Left Arm")
            cmbSiteforBP.Items.Add("Right Leg")
            cmbSiteforBP.Items.Add("Left Leg")
            cmbSiteforBP.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddVital()
        ClearFields()
        pnlVitalEntry.Show()
        dtVitals.Tag = 0    ''''VitalID
        '' to get previous weight in lbs
        txtWeightlbs.Tag = objclsPatientVitals.GetPrevWeight(dtVitals.Tag, lblPatientCode.Tag, dtVitals.Value)
    End Sub

    'Shubhangi 20091125 
    Private Function GetValidString(ByVal sText As String) As String
        Return sText.Replace("&", "").Replace("$", "")
    End Function

    Private Function GetWarningString() As String
        ''To generate string of number of warnings for out of range entries.
        Try

            Dim StrWarning As String = ""
            Dim CountWarning As Int16 = 0
            Dim _FirstFocus As Boolean = False ''To focus TextBox of first Warning only
            Dim minValue As Double = 0
            Dim maxValue As Double = 0

            '' HEIGHT ''
            If txtHtInInches.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Height, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(Val(txtft.Text) * 12 + Val(txtInch.Text)) Or maxValue < Convert.ToDouble(Val(txtft.Text) * 12 + Val(txtInch.Text)) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Height/Length must be in range (" & Convert.ToInt32(minValue) & " - " & Convert.ToInt32(maxValue) & ") Inches" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtft
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' WEIGHT ''
            If txtWeightlbs.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Weight, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtWeightlbs.Text) Or maxValue < Convert.ToDouble(txtWeightlbs.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Weight must be in range (" & minValue & " - " & maxValue & ") lbs" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtWeightlbs
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' RESPIRATORY RATE ''
            If txtRespiratory.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.RespiratoryRate, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtRespiratory.Text) Or maxValue < Convert.ToDouble(txtRespiratory.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Respiratory Rate must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtRespiratory
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' TEMPERATURE ''
            If txtTemperature.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Temperature, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtTemperature.Text) Or maxValue < Convert.ToDouble(txtTemperature.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Temperature must be in range (" & minValue & " - " & maxValue & ") F" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtTemperature
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' BP SYSTOLIC SITTING ''
            If txtBPSittingMax.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPSittingMax.Text) Or maxValue < Convert.ToDouble(txtBPSittingMax.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Systolic Sitting Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPSittingMax
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' BP DIASTOLIC SITTING ''
            If txtBPSittingMin.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPSittingMin.Text) Or maxValue < Convert.ToDouble(txtBPSittingMin.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Diastolic Sitting Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPSittingMin
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' BP SYSTOLIC STANDING ''
            If txtBPStandingMax.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPStandingMax.Text) Or maxValue < Convert.ToDouble(txtBPStandingMax.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Systolic Standing Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPStandingMax
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            ''BP DIASTOLIC STANDING ''
            If txtBPStandingMin.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtBPStandingMin.Text) Or maxValue < Convert.ToDouble(txtBPStandingMin.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Diastolic Standing Blood Pressure must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtBPStandingMin
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' PULSE PER MINUTE 
            If txtPulsePerMinute.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulsePerMinute, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtPulsePerMinute.Text) Or maxValue < Convert.ToDouble(txtPulsePerMinute.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Pulse Rate must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtPulsePerMinute
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' PULSE OX
            If txtPulseOX.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulseOX, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtPulseOX.Text) Or maxValue < Convert.ToDouble(txtPulseOX.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") PulseOX Rate must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtPulseOX
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If


            '' PULSE OX on O2
            If txtSupplement.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulseOX, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtSupplement.Text) Or maxValue < Convert.ToDouble(txtSupplement.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") PulseOX on O2 Rate must be in range (" & minValue & " - " & maxValue & ")" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtSupplement
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' HEAD CIRCUMFERENCE 
            If txtCircum.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.HeadCircumference, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtCircum.Text) Or maxValue < Convert.ToDouble(txtCircum.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Head Circumference must be in range (" & minValue & " - " & maxValue & ") cm" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtPulsePerMinute
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If

            '' STATURE ''
            If txtStature.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Stature, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtStature.Text) Or maxValue < Convert.ToDouble(txtStature.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Stature value must be in range (" & minValue & " - " & maxValue & ") cm" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtStature
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If
            ''Neck Circumferance

            If txtneckcircumCm.Text <> "" Then
                objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.NeckCircumference, minValue, maxValue)
                If minValue <> 0 And maxValue <> 0 Then
                    If minValue > Convert.ToDouble(txtneckcircumCm.Text) Or maxValue < Convert.ToDouble(txtneckcircumCm.Text) Then
                        CountWarning += 1
                        StrWarning = StrWarning & CountWarning & ") Neck Circumference must be in range (" & minValue & " - " & maxValue & ") cm" & Chr(13) & Chr(13)
                        If _FirstFocus = False Then
                            _FocusControl = txtneckcircumCm
                            _FirstFocus = True
                        End If
                    End If
                End If
            End If
            Return StrWarning
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Function GetFtInch(ByVal strHeight As String) As Array
        strHeight = Mid(strHeight, 1, Len(strHeight) - 1)
        Return Split(strHeight, "'", , CompareMethod.Text)
    End Function

    Private Function FtToMtr(ByVal Ft As Decimal, ByVal Inch As Decimal) As Decimal
        Return (Ft * 30.48 + Inch * 2.54) / 100
    End Function

    Private Function GetLbsOz(ByVal strLbsOz As String) As Array
        strLbsOz = strLbsOz.Replace("lbs", "'")
        strLbsOz = strLbsOz.Replace("oz", "'")
        Return Split(strLbsOz, "'", , CompareMethod.Text)
    End Function

    Private Sub AllowDecimal(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            'Allow only numeric and decimal point keys
            If InStr(Trim(Text), ".") <> 0 AndAlso (e.KeyChar = ChrW(46)) Then
                e.Handled = True
            Else
                If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(46)) OrElse (e.KeyChar = ChrW(8))) Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtBMI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMI.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtBMI.Text, e)
    End Sub

    Private Sub txtBPSittingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPSittingMax.KeyPress
        On Error Resume Next
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtBPSittingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPSittingMin.KeyPress
        On Error Resume Next
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtBPStandingMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPStandingMax.KeyPress
        On Error Resume Next
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtBPStandingMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBPStandingMin.KeyPress
        On Error Resume Next
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    Private Sub txtPulseOX_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseOX.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtPulseOX.Text, e)
    End Sub

    Private Sub txtPulsePerMinute_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulsePerMinute.KeyPress
        On Error Resume Next
        ''Allow only numeric and decimal point keys
        AllowDecimal(txtPulsePerMinute.Text, e)
        'If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
        '    e.Handled = True
        'End If
        'Exit Sub
    End Sub

    Private Sub txtRespiratory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRespiratory.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtRespiratory.Text, e)
    End Sub

    Private Sub txtTemperature_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperature.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtTemperature.Text, e)
        _TempInCelcius = False

    End Sub

    Private Sub txtWeightChanged_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightChanged.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWeightChanged.Text, e)
    End Sub


    Private Sub txtft_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtft.KeyPress
        'Allow only numeric and decimal point keys
        'AllowDecimal(txtft.Text, e)
        Try
            If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
                e.Handled = True
            Else
                If (e.KeyChar = ChrW(8)) Then
                    Exit Sub
                Else
                    txtInch.Focus()
                    _HeightFlag = HtFlag.FtInch
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInch.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtInch.Text, e)
        _HeightFlag = HtFlag.FtInch

    End Sub

    Private Sub txtft_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtft.KeyUp
        If Len(txtft.Text) > 0 Or Len(txtInch.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtHtInInches.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
        End If
    End Sub

    Private Sub txtft_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtft.TextChanged

        Dim strHeight As String = txtft.Text
        ''allow numerics only 
        strHeight = GetFormattedString(strHeight, False, False)
        txtft.Text = strHeight

        Calc_WtKg_BMI()
        BMIPercentile()
        If _HeightFlag = HtFlag.FtInch Then
            If txtft.Text = "" And txtInch.Text = "" Then
                txtHtInCm.Clear()
                txtHtInInches.Clear()
            Else
                txtHtInInches.Text = (Val(txtft.Text) * 12) + Val(txtInch.Text)
                txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")
            End If
        End If

    End Sub



    Private Sub txtft_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtft.Validating
        If txtft.TextLength = 1 And txtft.Focused Then
            txtInch.Focus()
        End If

        If txtft.Text = "" Then
            txtft.Text = ""
            Exit Sub
        End If
    End Sub



    Private Sub ClearFields()

        dtVitals.Value = _VisitDate
        lblVisitDate.Text = Format(dtVitals.Value, "MM/dd/yyyy")

        txtBMI.Text = ""
        txtBPSittingMax.Text = ""
        txtBPSittingMin.Text = ""
        txtBPStandingMax.Text = ""
        txtBPStandingMin.Text = ""
        txtft.Text = ""
        txtInch.Text = ""
        txtPulseOX.Text = ""
        txtPulsePerMinute.Text = ""
        txtRespiratory.Text = ""
        txtTemperature.Text = ""
        txtWeightChanged.Text = ""
        txtWeightKg.Text = ""
        txtWeightlbs.Text = ""
        txtComment.Text = ""
    End Sub

    Private Sub txtWeightKg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightKg.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWeightKg.Text, e)
        _WaightFlag = WeightIn.Kg
    End Sub

    Private Sub txtWeightlbs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightlbs.KeyPress
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWeightlbs.Text, e)
        _WaightFlag = WeightIn.Lbs
    End Sub

    Private Sub txtWeightlbs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeightlbs.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtWeightlbs.Text
        strHeight = GetPositiveString(strHeight)
        txtWeightlbs.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtWeightlbs.Text = GetValidString(txtWeightlbs.Text)
        Try
            If _WaightFlag = WeightIn.Lbs Then

                If txtWeightlbs.Text <> "" Then
                    'txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45, "#0.000")
                    txtWeightKg.Text = Format(Val(txtWeightlbs.Text) / 2.20462, "#0.00")

                    txtWtLbs.Text = Decimal.Truncate(Val(txtWeightlbs.Text))
                    Dim _decimalPlaces() As String = Split(txtWeightlbs.Text, ".", , CompareMethod.Text)
                    If _decimalPlaces.Length > 1 Then
                        txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                        If Val(txtWtOz.Text) = 0 Then
                            txtWtOz.Clear()
                        End If
                    Else
                        txtWtOz.Clear()
                    End If

                    If txtWeightlbs.Tag <> 0 Then
                        txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                    End If
                Else
                    txtWeightKg.Clear()
                    txtWeightChanged.Clear()
                    txtWtLbs.Clear()
                    txtWtOz.Clear()
                End If

                Calc_WtKg_BMI()
                BMIPercentile()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try

    End Sub

    Private Sub txtWeightKg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeightKg.TextChanged
        Dim strHeight As String = txtWeightKg.Text
        strHeight = GetPositiveString(strHeight)
        txtWeightKg.Text = strHeight

        txtWeightKg.Text = GetValidString(txtWeightKg.Text)
        Try
            If _WaightFlag = WeightIn.Kg Then
                If txtWeightKg.Text <> "" Then
                    txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 0.45359237, "#0.000")
                    ' txtWeightlbs.Text = Format(Val(txtWeightKg.Text) / 2.2033, "#0.00")

                    txtWtLbs.Text = Decimal.Truncate(Val(txtWeightlbs.Text))
                    Dim _decimalPlaces() As String = Split(txtWeightlbs.Text, ".", , CompareMethod.Text)
                    If _decimalPlaces.Length > 1 Then
                        txtWtOz.Text = CType("0." & _decimalPlaces(1), Decimal) * 16
                    Else
                        txtWtOz.Clear()
                    End If

                    If txtWeightlbs.Tag <> 0 Then
                        txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                    End If
                Else
                    txtWeightlbs.Clear()
                    txtWeightChanged.Clear()
                    txtWtLbs.Clear()
                    txtWtOz.Clear()
                    txtBMIPercentile.Clear()
                End If

                Calc_WtKg_BMI()
                BMIPercentile()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub


    Private Sub txtWeightKg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeightKg.KeyUp
        Try
            If Len(txtWeightKg.Text) > 0 Then
                txtWeightlbs.Enabled = False
                txtWtLbs.Enabled = False
                txtWtOz.Enabled = False
            Else
                txtWeightKg.Enabled = True
                txtWeightlbs.Enabled = True
                txtWtLbs.Enabled = True
                txtWtOz.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeightlbs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeightlbs.KeyUp
        Try
            If Len(txtWeightlbs.Text) > 0 Then
                txtWeightKg.Enabled = False
                txtWtLbs.Enabled = False
                txtWtOz.Enabled = False
            Else
                txtWeightlbs.Enabled = True
                txtWeightKg.Enabled = True
                txtWtLbs.Enabled = True
                txtWtOz.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BMIValue() As Double
        Dim bmi As Double
        Dim lbs As Double
        Dim h2 As Double
        Dim diff As Double
        Dim f_bmi As Double
        Dim idiff As Long

        lbs = Val(txtWtLbs.Text) + Val(txtWtOz.Text) / 16

        If (txtHtInInches.Text = "") Then
            bmi = 0
        Else

            h2 = Val(txtHtInInches.Text)
            If (h2 = 0) Then
                bmi = 0

            Else

                h2 = h2 * h2

                bmi = lbs / h2 * 703
                f_bmi = Math.Floor(bmi)
                diff = bmi - f_bmi
                diff = diff * 10
                idiff = Math.Round(diff)

                If (idiff = 10) Then    ' Need to bump up the whole thing instead
                    bmi = f_bmi + 1
                    idiff = 0
                Else
                    bmi = f_bmi.ToString + "." + idiff.ToString
                End If
            End If

        End If

        Return bmi

    End Function

    Private Function BMIValueHeightInCMandWeightinKG() As Double
        Dim bmi As Double
        Dim m As Double
        Dim h2 As Double
        Dim diff As Double
        Dim f_bmi As Double
        Dim idiff As Long

        If txtHtInCm.Text = "" Or txtWeightKg.Text = "" Then
            bmi = 0
        Else
            m = Val(txtHtInCm.Text) / 100
            If (m = 0) Then
                bmi = 0

            Else
                h2 = m * m

                bmi = Val(txtWeightKg.Text) / h2

                f_bmi = Math.Floor(bmi)

                diff = bmi - f_bmi
                diff = diff * 10

                idiff = Math.Round(diff)

                If (idiff = 10) Then    ' Need to bump up the whole thing instead
                    bmi = f_bmi + 1
                    idiff = 0
                Else
                    bmi = f_bmi.ToString + "." + idiff.ToString
                End If
            End If

        End If
        Return bmi
    End Function

    Private Sub Calc_WtKg_BMI()
        Dim dHeight As Double
        If Val(txtWeightKg.Text) <> 0.0 Then
            dHeight = FtToMtr(Val(txtft.Text), Val(txtInch.Text))
            dHeight = dHeight * dHeight
            If (Val(txtWeightKg.Text) > 0 And dHeight > 0) Then

                Dim bmi As Double

                If txtWeightKg.Enabled = True And txtHtInCm.Enabled = True Then
                    bmi = BMIValueHeightInCMandWeightinKG()
                Else
                    bmi = BMIValue()
                End If

                If bmi.ToString.IndexOf(".") > 0 Then
                    txtBMI.Text = Math.Truncate(bmi).ToString() + "." + bmi.ToString.Substring(bmi.ToString.IndexOf(".") + 1, 1)
                Else
                    txtBMI.Text = Math.Truncate(bmi).ToString() + ".0"
                End If

                'txtBMI.Text = Format(Val(txtWeightKg.Text) / dHeight, "#0.0")
            Else
                txtBMI.Text = ""
            End If
        Else
            txtBMI.Text = ""
        End If

        ''Added Rahul for BMI Messages on 20101005
        If txtBMI.Text.Trim() <> "" Then
            If Years > 20 Or (Years = 20 And (Months > 0 Or Days > 0)) Then
                If Convert.ToSingle(txtBMI.Text) < 16.5 Then
                    lblMsg.Text = "Severely underweight" & " (0-16.5) "
                    lblMsg.ForeColor = Color.Red
                ElseIf Convert.ToSingle(txtBMI.Text) >= 16.5 And Convert.ToSingle(txtBMI.Text) <= 18.4 Then
                    lblMsg.Text = "Underweight" & " (16.5-18.4) "
                    lblMsg.ForeColor = Color.Tomato
                ElseIf Convert.ToSingle(txtBMI.Text) > 18.4 And Convert.ToSingle(txtBMI.Text) <= 24.9 Then
                    lblMsg.Text = "Normal"
                    lblMsg.ForeColor = Color.Green
                ElseIf Convert.ToSingle(txtBMI.Text) > 24.9 And Convert.ToSingle(txtBMI.Text) <= 29.9 Then
                    lblMsg.Text = "Overweight" & " (24.9-29.9) "
                    lblMsg.ForeColor = Color.DarkOrange
                ElseIf Convert.ToSingle(txtBMI.Text) > 29.9 And Convert.ToSingle(txtBMI.Text) <= 34.9 Then
                    lblMsg.Text = "Obese Class I" & " (29.9-34.9) "
                    lblMsg.ForeColor = Color.LightSalmon
                ElseIf Convert.ToSingle(txtBMI.Text) > 34.9 And Convert.ToSingle(txtBMI.Text) <= 39.9 Then
                    lblMsg.Text = "Obese Class II" & " (34.9-39.9) "
                    lblMsg.ForeColor = Color.LightCoral
                ElseIf Convert.ToSingle(txtBMI.Text) > 39.9 Then
                    lblMsg.Text = "Obese Class III" & " (>39.9)"
                    lblMsg.ForeColor = Color.IndianRed
                End If
            Else
                lblMsg.Text = ""
            End If
        Else : lblMsg.Text = ""
        End If 'txtBMI.Text <> ""
        If txtBMIPercentile.Visible = False And lblBMIPercentile.Visible = False Then
            FlowLayoutPanel4.Controls.Add(lblMsg)
            FlowLayoutPanel4.Width = 300
        End If
        
    End Sub

    Private Sub BMIPercentile()
        Dim nMonths As Single = 0
        Dim nBMI As Single = 0
        Dim _BMIPercentile As Single = 0
        Try
            nMonths = Convert.ToSingle(Convert.ToInt16(_AgeInMonths))
            nBMI = Convert.ToSingle(Val(txtBMI.Text))
            If nBMI = 0 Then
                txtBMIPercentile.Clear()
            End If
           
            If IsNothing(gloUC_PatientStrip1) Then
                Exit Sub
            End If
            If (nMonths <= 240 And nMonths >= 24) And (nBMI <> 0) Then
                With AxAdvChart
                    '_BMIPercentile = .GetBMIPercentileForAge2(nMonths, nBMI)
                    .GrowthChartType = 3
                    If gloUC_PatientStrip1.PatientGender = "Male" Then
                        .Gender = 1
                    ElseIf gloUC_PatientStrip1.PatientGender = "Female" Then
                        .Gender = 2
                    ElseIf gloUC_PatientStrip1.PatientGender = "Others" Then
                        .Gender = 3
                    End If


                    Dim inx As Integer
                    .RemoveAllData()

                    .ShowPercentile = 1
                    .ShowPercentilesOnMouseOver = 1
                    Dim i As Single = .GetBMIPercentileForAge2(nMonths, nBMI)
                    Dim _age As Single = DateDiff(DateInterval.Day, gloUC_PatientStrip1.PatientDateOfBirth, CDate(dtVitals.Text), FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1)
                    inx = CLng(.AddNewData())
                    Call .SetAge(inx, CType(_age, Single) / 30.4375) '30.4375 is WHO Standard To Convert Days into Months. by sudhir 20081110

                    Call .SetWeight(inx, CType(txtWeightKg.Text, Single))

                    Call .SetHeight(inx, txtHtInCm.Text)
                    Call .SetTestDate(inx, CDate(dtVitals.Text))
                    Call .SetComments(inx, FormatAge1(gloUC_PatientStrip1.PatientDateOfBirth, dtVitals.Text))   'by sudhir 20081110

                    _BMIPercentile = .GetBMIPercentileForAge(inx)
                    Call .UpdateGraphView()
                End With
                txtBMIPercentile.Text = Math.Round(_BMIPercentile, 0)
            ElseIf (nMonths > 240 Or nMonths < 24) Then
                'Disable the BMI Percentile Label and Textbox
                txtBMIPercentile.Visible = False
                lblBMIPercentile.Visible = False

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Shared Function FormatAge1(ByVal BirthDate As DateTime, ByVal VitalDate As DateTime) As String
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = VitalDate.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 AndAlso BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If VitalDate < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = VitalDate.Year Then
            months = VitalDate.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + VitalDate.Month
        End If
        ' Check if the last month was a full month. 
        If VitalDate < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (VitalDate - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If VitalDate.Day = 29 AndAlso VitalDate.Month = 2 Then
                days += 1
            ElseIf VitalDate.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 AndAlso VitalDate.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        'Return years & " years " & months & " months " & days & " days"
        'Following code to display age in Numeric and Text
        If years = 0 Then
            If months = 0 Then
                If days <= 1 Then
                    Return days & " Day"
                Else
                    Return days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return months & " Month"
                ElseIf days = 1 Then
                    Return months & " Month " & days & " Day"
                Else
                    Return months & " Month " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return months & " Months"
                ElseIf days = 1 Then
                    Return months & " Months " & days & " Day"
                Else
                    Return months & " Months " & days & " Days"
                End If
            End If
        ElseIf years = 1 Then
            If months = 0 Then
                If days = 0 Then
                    Return years & " Year "
                ElseIf days = 1 Then
                    Return years & " Year " & days & " Day"
                Else
                    Return years & " Year " & days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return years & " Year " & months & " Month "
                ElseIf days = 1 Then
                    Return years & " Yr " & months & " Mon " & days & " Day"
                Else
                    Return years & " Yr " & months & " Mon " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return years & " Year " & months & " Months "
                ElseIf days = 1 Then
                    Return years & " Yr " & months & " Mons " & days & " Day"
                Else
                    Return years & " Yr " & months & " Mons " & days & " Days"
                End If
            End If
        ElseIf years > 1 Then
            If months = 0 Then
                If days = 0 Then
                    Return years & " Years "
                ElseIf days = 1 Then
                    Return years & " Years " & days & " Day"
                Else
                    Return years & " Years " & days & " Days"
                End If
            ElseIf months = 1 Then
                If days = 0 Then
                    Return years & " Years " & months & " Month"
                ElseIf days = 1 Then
                    Return years & " Yrs " & months & " Mon " & days & " Day"
                Else
                    Return years & " Yrs " & months & " Mon " & days & " Days"
                End If
            ElseIf months > 1 Then
                If days = 0 Then
                    Return years & " Years " & months & " Months"
                ElseIf days = 1 Then
                    Return years & " Yrs " & months & " Mons " & days & " Day"
                Else
                    Return years & " Yrs " & months & " Mons " & days & " Days"
                End If
            End If
        End If
        Return ""
    End Function
    Private Sub txtInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtInch.Validating
        Try
            If Val(txtft.Text) <= 0 Then
                If Val(txtInch.Text) >= 12 Then 'And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtft.Text = _Ft
                    txtInch.Text = _Inches
                    'Exit Sub
                End If

            Else
                If Val(txtInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtInch.Focus()
                    Exit Sub
                End If
            End If

            If txtInch.Text = "" Then
                txtInch.Text = ""
                Exit Sub
            End If

            Calc_WtKg_BMI()
            BMIPercentile()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtBPSittingMin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPSittingMin.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtBPSittingMin.Text
        strHeight = GetPositiveString(strHeight)
        txtBPSittingMin.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtBPSittingMin.Text = GetValidString(txtBPSittingMin.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtBPStandingMin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPStandingMin.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtBPStandingMin.Text
        strHeight = GetPositiveString(strHeight)
        txtBPStandingMin.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtBPStandingMin.Text = GetValidString(txtBPStandingMin.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPSittingMax_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPSittingMax.TextChanged
        Dim strHeight As String = txtBPSittingMax.Text
        strHeight = GetPositiveString(strHeight)
        txtBPSittingMax.Text = strHeight
        txtBPSittingMax.Text = GetValidString(txtBPSittingMax.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function MinMaxValidator(ByVal MinVal As String, ByVal MaxVal As String) As Boolean
        Dim blnIsValid As Boolean = False

        If MaxVal = "" Then
            Return True
        End If

        If MinVal = "" Then
            Return True
        End If

        If Val(MinVal) > Val(MaxVal) Or Val(MinVal) = Val(MaxVal) Then
            Return False
        Else
            Return True
        End If


    End Function


    Private Sub txtInch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInch.TextChanged
        'Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtInch.Text
        strHeight = GetPositiveString(strHeight)
        txtInch.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtInch.Text = GetValidString(txtInch.Text)
        Try

            Calc_WtKg_BMI()
            BMIPercentile()
            If _HeightFlag = HtFlag.FtInch Then
                If txtft.Text = "" And txtInch.Text = "" Then
                    txtHtInCm.Clear()
                    txtHtInInches.Clear()
                Else
                    txtHtInInches.Text = (Val(txtft.Text) * 12) + Val(txtInch.Text)
                    txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function GetPositiveString(ByVal str As String) As String
        ''Not allow negative values
        If str.Contains("-") Then
            str = str.Replace(str.ToString(), "")
            Return str
            Exit Function
        End If
        Return str
    End Function
    Public Function AllowStringUpto100(ByVal str As String) As String
        ''Not allow negative values
        Try

            Dim i As Int16
            If str <> "" Then
                i = Convert.ToInt16(str)
                If i > 100 Then

                    str = Convert.ToString(i)
                    str = str.Substring(0, str.Length - 1)


                End If
            End If

            Return str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub txtRespiratory_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRespiratory.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtRespiratory.Text
        strHeight = GetPositiveString(strHeight)
        txtRespiratory.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtRespiratory.Text = GetValidString(txtRespiratory.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulsePerMinute_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPulsePerMinute.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtPulsePerMinute.Text
        strHeight = GetPositiveString(strHeight)
        txtPulsePerMinute.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtPulsePerMinute.Text = GetValidString(txtPulsePerMinute.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPulseOX_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPulseOX.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtPulseOX.Text
        strHeight = GetPositiveString(strHeight)
        txtPulseOX.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtPulseOX.Text = GetValidString(txtPulseOX.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperature_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperature.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtTemperature.Text
        strHeight = GetPositiveString(strHeight)
        txtTemperature.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtTemperature.Text = GetValidString(txtTemperature.Text)
        Try
            If _TempInCelcius = False Then
                ''To convert Fahrenheit to Celcius
                If txtTemperature.Text <> "" Then
                    txtTemperatureCelcius.Text = Format((5 / 9) * (Val(txtTemperature.Text) - 32), "#0.00")
                Else
                    txtTemperatureCelcius.Clear()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtBPStandingMax_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPStandingMax.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtBPStandingMax.Text
        strHeight = GetPositiveString(strHeight)
        txtBPStandingMax.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtBPStandingMax.Text = GetValidString(txtBPStandingMax.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCircum_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCircum.KeyUp
        Try
            If Len(txtCircum.Text) > 0 Then
                txtCircumInch.Enabled = False
            Else
                txtCircumInch.Enabled = True
                txtCircum.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub



    Private Sub txtCircum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCircum.TextChanged
        Dim strHeight As String = txtCircum.Text
        strHeight = GetPositiveString(strHeight)
        txtCircum.Text = strHeight
        txtCircum.Text = GetValidString(txtCircum.Text)
        Try
            If _HeadCircumInch = False Then
                If txtCircum.Text <> "" Then
                    txtCircumInch.Text = Format(Val(txtCircum.Text) * 0.3937008, "#0.00")
                Else
                    txtCircumInch.Clear()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCircum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCircum.KeyPress
        Try
            AllowDecimal(txtCircum.Text, e)
            _HeadCircumInch = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtStature_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStature.KeyUp
        Try
            If Len(txtStature.Text) > 0 Then
                txtStatureinInch.Enabled = False
            Else
                txtStatureinInch.Enabled = True
                txtStature.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtStature_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStature.TextChanged
        Dim strHeight As String = txtStature.Text
        strHeight = GetPositiveString(strHeight)
        txtStature.Text = strHeight
        txtStature.Text = GetValidString(txtStature.Text)
        Try
            If _StatureInInch = False Then
                If txtStature.Text <> "" Then
                    txtStatureinInch.Text = Format(Val(txtStature.Text) * 0.3937008, "#0.00")
                Else
                    txtStatureinInch.Clear()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try

    End Sub

    Private Sub txtStature_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStature.KeyPress
        Try
            AllowDecimal(txtStature.Text, e)
            _StatureInInch = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub tblStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip.ItemClicked
        tblStrip.Select()
        Select Case e.ClickedItem.Tag
            Case "Close"
                If IsNothing(Arrlist) = False Then
                    Arrlist.Clear()
                End If
                Me.Close()
            Case "Ok" 'Save + Close

                _IsSaveClicked = True
                _Validate = False
                If AddVitals() = True Then
                    Me.Close()
                Else
                    _IsSaveClicked = False
                End If
                If IsNothing(Arrlist) = False Then
                    Arrlist.Clear()
                End If
                _Validate = True
            Case "BMI"
                ''Start :: 20102010 New Button added
                frmCalc_Bmi = New frmCalculate_BMI()
                frmCalc_Bmi.Text = "Calculate BMI"
                frmPatientVitals.strtxtft = txtft.Text
                frmPatientVitals.strtxtinch = txtInch.Text
                frmPatientVitals.strtxtwtoz = txtWtOz.Text
                frmPatientVitals.strtxtwtlbs = txtWtLbs.Text
                frmPatientVitals.strtxtwghtlbs = txtWeightlbs.Text
                frmPatientVitals.strtxtwghtkg = txtWeightKg.Text
                frmPatientVitals.strtxtBMI = txtBMI.Text
                frmCalc_Bmi.StartPosition = FormStartPosition.CenterParent
                ''Added on 20110105 by Mayuri-We have used same form for both BMI and ODI calculation
                frmCalc_Bmi.tblbtn_Save_32.Visible = False
                frmCalc_Bmi.tblbtn_SavenClose_32.Visible = False
                frmCalc_Bmi.Icon = Global.gloEMR.My.Resources.BMI_Claculater

                frmCalc_Bmi.pnlQuestionaire.Visible = False ''''panel which is used for calculationg ODI which contains questions
                frmCalc_Bmi.Size = New System.Drawing.Size(652, 215)
                frmCalc_Bmi.ShowDialog(IIf(IsNothing(frmCalc_Bmi.Parent), Me, frmCalc_Bmi.Parent))

                frmCalc_Bmi.Dispose()
                frmCalc_Bmi = Nothing

            Case "ODI"
                If lblVisitDate.Tag = 0 Then
                    lblVisitDate.Tag = GenerateVisitID(_PatientID)
                End If
                Dim _strtxtODIFromVitals As String = ""
                _strtxtODIFromVitals = txtODI.Text.Trim()
                If _IsMakeAsCurrent = True Then
                    frmCalc_Bmi = New frmCalculate_BMI(_PrevVitalID, lblVisitDate.Tag, lblPatientCode.Tag, _isformLoad, _strtxtODIFromVitals)
                Else
                    frmCalc_Bmi = New frmCalculate_BMI(dtVitals.Tag, lblVisitDate.Tag, lblPatientCode.Tag, _isformLoad, _strtxtODIFromVitals)
                End If
                frmCalc_Bmi.Text = "Calculate ODI"
                frmCalc_Bmi.StartPosition = FormStartPosition.CenterParent
                frmCalc_Bmi.tblbtn_Reset_32.Visible = False
                frmCalc_Bmi.Icon = Global.gloEMR.My.Resources.Oswestry_Disability_Index

                frmCalc_Bmi.pnlReset.Visible = False    ''panel which is used for calculationg BMI
                _isformLoad = False

                If frmCalc_Bmi.ShowDialog(IIf(IsNothing(frmCalc_Bmi.Parent), Me, frmCalc_Bmi.Parent)) = Windows.Forms.DialogResult.Cancel Then

                    If IsNothing(Arrlist) = False Then
                        If Arrlist.Count > 0 Then
                            txtODI.Text = frmCalculate_BMI.strtxtODI
                        ElseIf txtODI.Text <> frmCalc_Bmi.txtODIPercent.Text And frmCalc_Bmi.IsSavenClose = True Then
                            txtODI.Text = ""
                        End If
                    End If
                End If

                frmCalc_Bmi.Dispose()
                frmCalc_Bmi = Nothing

            Case "Validate"
                _Validate = False
                ValidateAll()
                _Validate = True
                'Code End - Added by sanjog on 20100710 for CCD


            Case "OBVitals"
                ''Start :: 20102010 New Button added
                frmOBVitals = New frmOBVital()
                'frmPatientVitals.strtxtwtlbs = txtWtLbs.Text
                frmPatientVitals.strtxtwghtlbs = txtWeightlbs.Text
                frmPatientVitals.strtxtBPSittingMax = txtBPSittingMax.Text
                frmPatientVitals.strtxtBPSittingMin = txtBPSittingMin.Text
                If (chkpain.Checked = True) Then
                    frmPatientVitals.strchkPain = "1"
                Else
                    frmPatientVitals.strchkPain = "0"
                End If
                frmPatientVitals.strPainLevel = trbPainLevel.Value.ToString()
                frmPatientVitals.strComments = txtComment.Text
                frmPatientVitals.vitID = _VitalID
                frmPatientVitals.visDate = Format(dtVitals.Value, "MM/dd/yyyy hh:mm:ss tt")
                frmPatientVitals.nVisitID = _VisitID
                frmPatientVitals.patientID = _PatientID

                If _IsMakeAsCurrent = True Then
                    prevVitID = _PrevVitalID
                    makeCurrent = True
                Else
                    makeCurrent = False

                End If

                If dtLastmenstrualperiod.Checked = True Then
                    frmPatientVitals.strchkLMP = "1"
                    frmPatientVitals.lmpDate = Format(dtLastmenstrualperiod.Value, "MM/dd/yyyy").ToString()
                Else
                    frmPatientVitals.strchkLMP = "0"
                    frmPatientVitals.lmpDate = Format(System.DateTime.Now, "MM/dd/yyyy").ToString()
                End If


                If (_hashOBVital.Count > 0) Then
                    frmOBVitals._HashTable = _hashOBVital
                End If
                frmOBVitals.ShowDialog(IIf(IsNothing(frmOBVitals.Parent), Me, frmOBVitals.Parent))

                OBVitalTask = frmOBVitals.ObTaskID
                _hashOBVital = frmOBVitals._HashTable
                frmOBVitals.Dispose()
                frmOBVitals = Nothing

                Dim _IsLMP As Boolean = True

                chkpain.Tag = ""

                If _hashOBVital.Count > 0 Then

                    For Each element As DictionaryEntry In _hashOBVital
                        Select Case element.Key
                            Case "txtWeight"
                                _WaightFlag = WeightIn.Lbs
                                txtWeightlbs.Text = Convert.ToString(element.Value)
                                _WaightFlag = WeightIn.LbsOz
                            Case "txtBPSittingMax"
                                txtBPSittingMax.Text = Convert.ToString(element.Value)
                            Case "txtBPSittingMin"
                                txtBPSittingMin.Text = Convert.ToString(element.Value)
                            Case "txtComments"
                                txtComment.Text = Convert.ToString(element.Value)
                            Case "trbPainLevel"
                                If chkpain.Tag = "" Then
                                    chkpain.Checked = True
                                    trbPainLevel.Value = Convert.ToString(element.Value)
                                End If
                            Case "chkPain"
                                '01-Jun-16 Aniket: Resolving Bug #96617: gloEMR : New exam (OB vitals liquid link) : OB Vitals pain level liquid link showing data as user uncheck it
                                If Convert.ToString(element.Value) = "0" Then
                                    chkpain.Tag = "0"
                                    chkpain.Checked = False
                                    trbPainLevel.Value = 0
                                End If
                        End Select


                    Next

                End If







            Case "Generate CCD"
                Dim objfrm As New frmCCDGenerateList(_PatientID)
                objfrm.ChkVitals.Checked = True

                With objfrm
                    .WindowState = FormWindowState.Normal
                    .BringToFront()
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                End With

                objfrm.Dispose()
                objfrm = Nothing

            Case "CaptureDeviceVitals"
                '_strAUSid &= "glo123"
                Dim objEncryption As New gloSecurity.ClsEncryption()
                If objEncryption.EncryptToBase64String(objEncryption.EncryptToBase64String(String.Concat(_strAUSid.ToLower(), "gL0@PPs2k9!"), "87654321"), mdlGeneral.constEncryptDecryptKey) = _strVitalsKey Then
                    If DeviceConnect(Application.StartupPath & "\wasdkconfig.xml") Then
                        For nCnt As Int16 = 0 To _NoOfAttemptsToConnectDevice
                            If IsNothing(m_oDeviceData) Then
                                System.Threading.Thread.Sleep(1000)
                            End If
                        Next
                        CaptureDeviceVitals()
                    End If
                Else
                    MessageBox.Show("Vital device interface authentication failed, please check activation key", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                'SLR: Added on 11/18/2015
                objEncryption.Dispose()
                objEncryption = Nothing
            Case "28 DAS"

                OpenDAS()

        End Select
    End Sub

    Public Sub OpenDAS()
        Try
            Dim objfrm As frmDASCalculator
            If _IsMakeAsCurrent Then
                objfrm = New frmDASCalculator(_PrevVitalID, _PatientID)
            Else
                objfrm = New frmDASCalculator(_VitalID, _PatientID)
            End If

            If Not IsNothing(oclsDAS) Then
                objfrm.oclsDAS = oclsDAS
            End If
            If _IsMakeAsCurrent Then
                objfrm.IsmakeasCurrent = True
            Else
                objfrm.IsmakeasCurrent = False
            End If
            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
                If .DlogResult Then
                    oclsDAS = .oclsDAS
                    txtDAS28.Text = oclsDAS.DASValue
                Else
                    If blnOpenFromExamForDAS Then
                        Me.Close()
                    End If
                End If
            End With
            'If (IsNothing(objfrm.oclsDAS.DASImage) = False) Then
            '    objfrm.oclsDAS.DASImage.Dispose()
            '    objfrm.oclsDAS.DASImage = Nothing
            'End If
            objfrm.Dispose()
            objfrm = Nothing

        Catch ex As Exception

        End Try
    End Sub

    ''Added by Mayuri:20110105-For calculating ODI
    Public Function ODIPercentCalculation() As String
        Try
            Dim j As Int16
            Dim k As Int16
            Dim i As Int16
            Dim Str As String = ""

            If Arrlist.Count > 0 Then
                For i = 0 To Arrlist.Count - 1
                    Dim lst As myList = Arrlist(i)
                    With lst

                        j = j + Convert.ToInt16(lst.Description)

                    End With
                Next
                k = j * 2
                Str = Convert.ToString(k)
            End If

            Return Str
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End Try
    End Function
    ''End Code Added by Mayuri:20110105-For calculating ODI

    'Check whether we enter any vital or not
    Private Function IsVitalsEntered() As Boolean

        Dim _Count As Integer
        For Each oText As Control In tblpnlMain.Controls

            If IsVitalsEntered1(oText) = True Then
                _Count = +1
                Exit For
            End If

        Next
        If _Count = 0 Then
            MessageBox.Show("Enter at least one Vital Statistics.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function



    Private Function IsVitalsEntered1(ByVal _control As Control) As Boolean

        For Each oText As Control In _control.Controls
            If TypeOf oText Is TextBox Then
                If oText.Visible = True Then
                    If oText.Name <> "txtComment" And oText.Name <> "txtTHRperMax" And oText.Name <> "txtTHRperMin" And oText.Name <> "txtTHR" And oText.Name <> "txtTHRMin" And oText.Name <> "txtTHRMax" Then
                        If (Val(oText.Text) <> 0 Or oText.Text <> "") Then
                            Return True
                        End If
                    End If
                End If
            Else
                If IsVitalsEntered1(oText) = True Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function
    Private Function AddVitals() As Boolean
        Try
            '   Dim dLastmenstrualperiod As DateTime
            '  Dim objtxt As Control
            Dim strText As String
            Dim strHeight As String
            Dim strWeight As String 'sudhir 20081128
            strText = "0.00"

            'Check whether we enter any vital or not & according to that return values
            If _IsTextBoxPresent = True Then
                If IsVitalsEntered() = False Then
                    Return False
                End If
            End If

            If Val(txtft.Text) <= 0 Then

                If Val(txtInch.Text) >= 12 And Val(txtInch.Text) <= 84 Then
                    Dim _Ft As Decimal
                    Dim _Inches As Decimal
                    Dim _TotalInches As Decimal = Val(txtInch.Text)

                    _Ft = Math.Floor(_TotalInches / 12)
                    _Inches = Math.Round(_TotalInches Mod 12, 2)
                    txtft.Text = _Ft
                    txtInch.Text = _Inches

                End If
            Else
                If Val(txtInch.Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtInch.Focus()
                    Return False
                End If
            End If

            ''Lbs Oz Validation
            If Val(txtWtLbs.Text) <= 0 Then
                If Val(txtWtOz.Text) >= 16 Then
                    txtWtLbs.Text = Val(txtWtOz.Text) \ 16
                    txtWtOz.Text = Val(txtWtOz.Text) Mod 16
                End If
            Else
                If Val(txtWtOz.Text) >= 16 Then
                    'Added for Bug #90059: glOEMR: VItals- Application displays Invalid OZ message twice
                    ' MessageBox.Show("Invalid value of Oz", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWtOz.Focus()
                    Return False
                End If
            End If
            ''Added on 20100629 by sanjog
            If txtTHRperMax.Text <> "" And txtTHRperMin.Text <> "" Then
                If Val(txtTHRperMax.Text) <= Val(txtTHRperMin.Text) Then
                    MessageBox.Show("Invalid value of Estimated Target Heart Rate", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            Else
                MessageBox.Show("Enter both values of Estimated Target Heart Rate", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            ''Added on 20100629 by sanjog
            ''sudhir '' Called again for validation according to age from vital date...
            FormatAge(_DateOfBirth, CType(dtVitals.Value, DateTime))

            If IsVitalEnabled Then
                ''Function to Generate Warning for out of range Entries 
                Dim _strWarning As String = GetWarningString()
                If _strWarning <> "" Then
                    If MessageBox.Show("Values entered are not within normal range," & Chr(13) & "Do you want to save the records?" & Chr(13) & Chr(13) & Chr(13) & _strWarning & Chr(13) & "YES - To Save current values " & Space(70) & Chr(13) & Chr(13) & " NO - To Modify values", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                        _FocusControl.Enabled = True
                        _FocusControl.Select()
                        Return False
                    End If

                End If
            End If

            If txtft.Text = "" And txtInch.Text = "" Then
                strHeight = ""
            Else
                If txtft.Text.Trim.Length = 0 Then
                    txtft.Text = 0
                End If
                If txtInch.Text.Trim.Length = 0 Then
                    txtInch.Text = 0
                End If
                strHeight = txtft.Text + "'" + txtInch.Text + "''"
            End If

            If txtWtLbs.Text <> "" Or txtWtOz.Text <> "" Then
                strWeight = Val(txtWtLbs.Text) & "lbs  " & Val(txtWtOz.Text) & "oz"
            Else
                strWeight = Nothing
            End If

            If lblVisitDate.Tag = 0 Then
                ''Added paramter date to generate visit of selected vital date Case NO #00000064 : EMR Settings
                Try
                    lblVisitDate.Tag = GenerateVisitID(dtVitals.Value.Date, _PatientID)
                Catch ex As Exception

                End Try

            End If

            If txtTHRperMin.Text = "" Then
                txtTHRMin.Text = ""
            End If
            If txtTHRperMax.Text = "" Then
                txtTHRMax.Text = ""
            End If

            If dtLastmenstrualperiod.Checked = False Then
                dLastperiod = System.DateTime.Now
                dStatusofLMPeriod = 0
            Else
                'Bug #52424: 00000485 : Vitals
                'Comparing Date instead of Date time to resolve the issue
                If dtLastmenstrualperiod.Value.Date <= dtVitals.Value.Date Then
                    If dtLastmenstrualperiod.Value.Date <= System.DateTime.Now.Date Then
                        dLastperiod = dtLastmenstrualperiod.Value
                        dStatusofLMPeriod = 1
                    Else
                        If dtLastmenstrualperiod.Visible = True Then
                            MessageBox.Show("Please enter valid Last Menstrual Period date", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            AddVitals = Nothing
                            Exit Function
                        Else
                            dLastperiod = System.DateTime.Now
                            dStatusofLMPeriod = 0
                        End If
                    End If
                Else
                    If dtLastmenstrualperiod.Visible = True Then
                        MessageBox.Show("Please enter valid Last Menstrual Period date", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        AddVitals = Nothing
                        Exit Function
                    Else
                        dLastperiod = System.DateTime.Now
                        dStatusofLMPeriod = 0
                    End If

                End If


            End If

            If (txtBPSittingMax.Text <> "" And txtBPSittingMin.Text = "") Or (txtBPSittingMax.Text = "" And txtBPSittingMin.Text <> "") Then
                MessageBox.Show("Enter all details of BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                AddVitals = Nothing
                Exit Function
            End If

            If (txtBPStandingMax.Text <> "" And txtBPStandingMin.Text = "") Or (txtBPStandingMax.Text = "" And txtBPStandingMin.Text <> "") Then
                MessageBox.Show("Enter all details of BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                AddVitals = Nothing
                Exit Function
            End If

            _blnPainlvl = chkpain.Checked
            _blnPainWithMedication = chkPainWithMedication.Checked
            _blnPainWithoutMedication = chkPainWithoutMedication.Checked
            _blnPainWorst = chkPainWorst.Checked

            '  Dim odipercent As Int16

            vitalID = objclsPatientVitals.AddNewVitals(dtVitals.Tag, lblVisitDate.Tag, lblPatientCode.Tag, Format(dtVitals.Value, "MM/dd/yyyy") & " " & Format(dtVitals.Value, "hh:mm:ss tt"), strHeight, Val(txtWeightlbs.Text), Val(txtWeightChanged.Text), Val(txtBMI.Text), Val(txtWeightKg.Text), Val(txtTemperature.Text), Val(txtRespiratory.Text), Val(txtPulsePerMinute.Text), Val(txtPulseOX.Text), Val(txtBPSittingMin.Text), Val(txtBPSittingMax.Text), Val(txtBPStandingMin.Text), Val(txtBPStandingMax.Text), Trim(txtComment.Text), Val(txtCircum.Text), Val(txtStature.Text), Val(txtTHRperMax.Text), Val(txtTHRMax.Text), Val(txtTHRperMin.Text), Val(txtTHRMin.Text), Val(txtTHR.Text), Val(txtHtInInches.Text), Val(txtHtInCm.Text), strWeight, Val(txtTemperatureCelcius.Text), trbPainLevel.Value, Val(txtPEFR1.Text), Val(txtPEFR2.Text), Val(txtPEFR3.Text), Val(txtCircumInch.Text), Val(txtStatureinInch.Text), cmbSiteforBP.Text, dLastperiod, Val(txtneckcircumCm.Text), Val(txtneckcircumInch.Text), Val(txtlefteyepressure.Text), Val(txtRighteyepressure.Text), dStatusofLMPeriod, trbPainWithMedication.Value, trbPainWithoutMedication.Value, trbPainWorst.Value, Val(txtODI.Text), Val(txtDAS28.Text), Val(txtSupplement.Text), Val(txtPulseRate.Text), _blnPainlvl, _blnPainWithMedication, _blnPainWithoutMedication, _blnPainWorst, Val(txtTotalPreganancy.Text), Val(txtFullTerm.Text), Val(txtLiving.Text), Val(txtMultipleBirth.Text), Val(txtPremature.Text), Val(txtAbortedinduced.Text), Val(txtAbortedSpontaneous.Text), Val(txtEctopic.Text), Val(txtBMIPercentile.Text))

            If Not IsNothing(oclsDAS) Then
                oclsDAS.nVitalID = vitalID
                Dim did As Int64
                did = oclsDAS.InUp_DAS()
                If (Not IsNothing(oclsDAS.DASImage) = False) Then
                    oclsDAS.DASImage.Dispose()
                    oclsDAS.DASImage = Nothing
                End If
                oclsDAS = Nothing
            End If

            If IsNothing(Arrlist) = False Then
                If Arrlist.Count > 0 Then 'If DeleteVitalsODI(vitalID) = True Then
                    If objclsPatientVitals.AddODIAnswers(vitalID, lblVisitDate.Tag, lblPatientCode.Tag, Format(dtVitals.Value, "MM/dd/yyyy") & " " & Format(dtVitals.Value, "hh:mm:ss tt"), Arrlist) = True Then
                    End If
                Else
                    If objclsPatientVitals.AddODIAnswers(vitalID, lblVisitDate.Tag, lblPatientCode.Tag, Format(dtVitals.Value, "MM/dd/yyyy") & " " & Format(dtVitals.Value, "hh:mm:ss tt"), Arrlist) = True Then
                    End If
                End If
            End If

            If _hashOBVital.Count > 0 Then
                Dim ObCaseID As Long = 0
                ObCaseID = GenerateOBCase(dtVitals.Value)
                objclsOBVitals.AddOBVITALS(vitalID, lblVisitDate.Tag, lblPatientCode.Tag, _hashOBVital, ObCaseID, blnopenOBPopup)
            End If

            If OBVitalTask <> 0 Then
                AddVitalTask(vitalID)
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function DeleteVitalsODI(ByVal nVitalID As Long) As Boolean
        Dim _strSQL As String = ""
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            _strSQL = "DELETE from VitalsODI where nVitalID = " & nVitalID
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _strSQL
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            Return True
        Catch ex As SqlException
            Return False
        Finally
            objCon.Dispose()
            objCon = Nothing
        End Try
    End Function
    Private Sub ValidateAll()
        If _IsTextBoxPresent = True Then
            If IsVitalsEntered() = False Then
                Exit Sub
            End If
        End If

        ''sudhir '' Called again for validation according to age from vital date...
        FormatAge(_DateOfBirth, CType(dtVitals.Value, DateTime))

        ''Function to Generate Warning for out of range Entries 
        Dim _strWarning As String = GetWarningString()
        If _strWarning <> "" Then
            MessageBox.Show("Values entered are not within normal range," & Chr(13) & Chr(13) & Chr(13) & _strWarning & Space(70), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _FocusControl.Enabled = True
            _FocusControl.Select()
        Else
            MessageBox.Show("Entered values of Vitals are within the normal range. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub





    Private Sub txtTHRperMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTHRperMin.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtTHRperMin.Text, e)
    End Sub

    Private Sub txtTHRperMin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTHRperMin.TextChanged
        Try
            ''Added by Mayuri:20100625-do not allow negative values after copy-paste
            Dim strHeight As String = txtTHRperMin.Text
            strHeight = GetPositiveString(strHeight)
            txtTHRperMin.Text = strHeight
            txtTHRperMin.Text = GetValidString(txtTHRperMin.Text)
            If txtTHRperMin.Text = "" Then
                txtTHRMin.Text = ""
                txtTHRperMin.Select()
                Exit Sub
            End If
            If txtTHRperMin.Text = "." Then
                Exit Sub
            End If
            If IsNumeric(txtTHRperMin.Text) = False Then
                Exit Sub
            End If

            THRMin = Convert.ToDouble(txtTHRperMin.Text)
            txtTHRMin.Text = ((220 - Years) * (THRMin / 100)).ToString(".00")
            If Len(Trim(txtTHRperMin.Text)) = 5 Then
                txtTHRperMax.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTHRperMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTHRperMin.Validating
        Try
            If txtTHRperMin.TextLength > 0 And _Validate = True Then

            Else
                txtTHRMin.Text = ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTHRperMax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTHRperMax.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtTHRperMax.Text, e)
    End Sub

    Private Sub txtTHRperMax_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTHRperMax.TextChanged
        Try
            ''Added by Mayuri:20100625-do not allow negative values after copy-paste
            Dim strHeight As String = txtTHRperMax.Text
            strHeight = GetPositiveString(strHeight)
            txtTHRperMax.Text = strHeight
            txtTHRperMax.Text = GetValidString(txtTHRperMax.Text)
            If txtTHRperMax.Text = "" Then
                txtTHRMax.Text = ""
                txtTHRperMax.Select()
                Exit Sub
            End If
            If txtTHRperMax.Text = "." Then
                Exit Sub
            End If

            If IsNumeric(txtTHRperMax.Text) = False Then
                Exit Sub
            End If

            THRMax = Convert.ToDouble(txtTHRperMax.Text)
            txtTHRMax.Text = ((220 - Years) * (THRMax / 100)).ToString(".00")
            If Len(Trim(txtTHRperMax.Text)) = 5 Then
                txtComment.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTHRperMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTHRperMax.Validating
        Try
            If txtTHRperMax.TextLength > 0 And _Validate = True Then

            Else
                txtTHRMax.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub dtVitals_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtVitals.ValueChanged
        Try
            '' Previous Weight
            txtWeightlbs.Tag = objclsPatientVitals.GetPrevWeight(_VitalID, lblPatientCode.Tag, dtVitals.Value)

            If txtWeightlbs.Tag <> 0 And txtWeightlbs.Text.Trim() <> "" Then
                txtWeightChanged.Text = Val(txtWeightlbs.Text) - txtWeightlbs.Tag
            Else
                txtWeightChanged.Text = ""
            End If
            ''
            BMIPercentile()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtHtInCm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHtInCm.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtHtInCm.Text, e)
        _HeightFlag = HtFlag.Centimeter
    End Sub

    Private Sub txtHtInInches_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHtInInches.KeyPress

        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtHtInInches.Text, e)
        _HeightFlag = HtFlag.Inch
    End Sub

    Private Sub txtWtLbs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWtLbs.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWtLbs.Text, e)
        _WaightFlag = WeightIn.LbsOz
    End Sub

    Private Sub txtWtOz_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWtOz.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtWtOz.Text, e)
        _WaightFlag = WeightIn.LbsOz
    End Sub

    Private Sub txtTemperatureCelcius_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTemperatureCelcius.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtTemperatureCelcius.Text, e)
        _TempInCelcius = True
    End Sub

    Private Sub txtCircumInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCircumInch.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtCircumInch.Text, e)
        _HeadCircumInch = True
    End Sub

    Private Sub txtPEFR1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPEFR1.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtPEFR1.Text, e)
    End Sub

    Private Sub txtPEFR2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPEFR2.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtPEFR2.Text, e)
    End Sub

    Private Sub txtPEFR3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPEFR3.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtPEFR3.Text, e)
    End Sub

    Private Sub txtStatureinInch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStatureinInch.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtStatureinInch.Text, e)
        _StatureInInch = True
    End Sub

    Private Sub txtTemperature_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTemperature.KeyUp
        Try
            If Len(txtTemperature.Text) > 0 Then
                txtTemperatureCelcius.Enabled = False
            Else
                txtTemperatureCelcius.Enabled = True
                txtTemperature.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureCelcius_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTemperatureCelcius.KeyUp
        Try
            If Len(txtTemperatureCelcius.Text) > 0 Then
                txtTemperature.Enabled = False
            Else
                txtTemperatureCelcius.Enabled = True
                txtTemperature.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtTemperatureCelcius_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperatureCelcius.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtTemperatureCelcius.Text
        strHeight = GetPositiveString(strHeight)
        txtTemperatureCelcius.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtTemperatureCelcius.Text = GetValidString(txtTemperatureCelcius.Text)
        Try
            If _TempInCelcius = True Then
                ''To convert Celcius to Fahrenheit
                If txtTemperatureCelcius.Text <> "" Then
                    txtTemperature.Text = (Format((9 / 5) * Val(txtTemperatureCelcius.Text) + 32, "#0"))
                Else
                    txtTemperature.Clear()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCircumInch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCircumInch.KeyUp
        Try
            If Len(txtCircumInch.Text) > 0 Then
                txtCircum.Enabled = False
            Else
                txtCircum.Enabled = True
                txtCircumInch.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtCircumInch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCircumInch.TextChanged
        Try
            ''Added by Mayuri:20100625-do not allow negative values after copy-paste
            Dim strHeight As String = txtCircumInch.Text
            strHeight = GetPositiveString(strHeight)
            txtCircumInch.Text = strHeight
            txtCircumInch.Text = GetValidString(txtCircumInch.Text)

            If _HeadCircumInch = True Then
                If txtCircumInch.Text <> "" Then
                    txtCircum.Text = Format(Val(txtCircumInch.Text) * 2.54, "#0.00")
                Else
                    txtCircum.Clear()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try

    End Sub

    Private Sub txtStatureinInch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStatureinInch.KeyUp
        Try
            If Len(txtStatureinInch.Text) > 0 Then
                txtStature.Enabled = False
            Else
                txtStature.Enabled = True
                txtStatureinInch.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtStatureinInch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStatureinInch.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtStatureinInch.Text
        strHeight = GetPositiveString(strHeight)
        txtStatureinInch.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtStatureinInch.Text = GetValidString(txtStatureinInch.Text)
        Try
            If _StatureInInch = True Then
                If txtStatureinInch.Text <> "" Then
                    txtStature.Text = Format(Val(txtStatureinInch.Text) * 2.54, "#0.0")
                Else
                    txtStature.Clear()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtHtInInches_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHtInInches.KeyUp
        If Len(txtHtInInches.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtft.Enabled = False
            txtInch.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
            txtHtInInches.Enabled = True
        End If
    End Sub

    Private Sub txtHtInCm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHtInCm.KeyUp
        If Len(txtHtInCm.Text) > 0 Then
            txtHtInInches.Enabled = False
            txtft.Enabled = False
            txtInch.Enabled = False
        Else
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
            txtHtInCm.Enabled = True
        End If
    End Sub

    Private Sub txtInch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInch.KeyUp
        If Len(txtInch.Text) > 0 Or Len(txtft.Text) > 0 Then
            txtHtInCm.Enabled = False
            txtHtInInches.Enabled = False
        Else
            txtHtInCm.Enabled = True
            txtHtInInches.Enabled = True
            txtft.Enabled = True
            txtInch.Enabled = True
        End If
    End Sub

    Private Sub txtHtInInches_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHtInInches.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtHtInInches.Text
        strHeight = GetPositiveString(strHeight)
        txtHtInInches.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtHtInInches.Text = GetValidString(txtHtInInches.Text)
        If _HeightFlag = HtFlag.Inch Then
            If txtHtInInches.Text <> "" Then
                txtHtInCm.Text = Format(Val(txtHtInInches.Text) * 2.54, "#0.00")

                '' GLO2012-0016033 : Unable to enter infant length of 60cm
                '' Display inches in decimal
                txtInch.Text = Format(Convert.ToDecimal(Val(txtHtInInches.Text) Mod 12), "#0.00")
                txtft.Text = Split(CType(Val(txtHtInInches.Text) / 12, String), ".", , CompareMethod.Text)(0)

                '' GLO2012-0016033 : Unable to enter infant length of 60cm
                '' if inches = 12 then convert inches into ft and set inches to zero
                If Convert.ToDecimal(Val(txtInch.Text)) = 12 Then
                    txtft.Text = Convert.ToInt64(Val(txtft.Text)) + 1
                    txtInch.Text = 0
                End If
            Else
                txtft.Clear()
                txtInch.Clear()
                txtHtInCm.Clear()
            End If
        End If
        '' Chetan Added 
        txtHtInInches.ForeColor = Color.Black
    End Sub

    Private Sub txtHtInCm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHtInCm.TextChanged
        'Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtHtInCm.Text
        strHeight = GetPositiveString(strHeight)
        txtHtInCm.Text = strHeight

        'Shubhangi 20091125
        'Validation for special chaacters.
        txtHtInCm.Text = GetValidString(txtHtInCm.Text)
        If _HeightFlag = HtFlag.Centimeter Then
            If txtHtInCm.Text <> "" Then
                txtHtInInches.Text = Format(Val(txtHtInCm.Text) * 0.3937008, "#0.00")
                '' GLO2012-0016033 : Unable to enter infant length of 60cm
                '' Display inches in decimal
                txtInch.Text = Format(Convert.ToDecimal(Val(txtHtInInches.Text) Mod 12), "#0.00")
                txtft.Text = Split(CType(Val(txtHtInInches.Text) / 12, String), ".", , CompareMethod.Text)(0)

                '' GLO2012-0016033 : Unable to enter infant length of 60cm
                '' if inches = 12 then convert inches into ft and set inches to zero
                If Convert.ToDecimal(Val(txtInch.Text)) = 12 Then
                    txtft.Text = Convert.ToInt64(Val(txtft.Text)) + 1
                    txtInch.Text = 0
                End If

            Else
                txtft.Clear()
                txtInch.Clear()
                txtHtInInches.Clear()
            End If
        End If
    End Sub

    Private Sub txtWtOz_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWtOz.KeyUp
        If Len(txtWtLbs.Text) > 0 Or Len(txtWtOz.Text) > 0 Then
            txtWeightlbs.Enabled = False
            txtWeightKg.Enabled = False
        Else
            txtWeightlbs.Enabled = True
            txtWeightKg.Enabled = True
            txtWtLbs.Enabled = True
            txtWtOz.Enabled = True
        End If
    End Sub

    Private Sub txtWtOz_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtOz.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtWtOz.Text
        strHeight = GetPositiveString(strHeight)
        txtWtOz.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.  
        txtWtOz.Text = GetValidString(txtWtOz.Text)
        If _WaightFlag = WeightIn.LbsOz Then
            If Val(txtWtLbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                txtWeightlbs.Text = Format(Val(txtWtLbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45359237, "#0.000")
                'txtWeightKg.Text = Format(Val(txtWeightlbs.Text) / 2.2033, "#0.00")
                If txtWeightlbs.Tag <> 0 Then
                    txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                End If
            Else
                txtWeightlbs.Clear()
                txtWeightKg.Clear()
                txtWeightChanged.Clear()
            End If
            Calc_WtKg_BMI()
            BMIPercentile()
        End If
    End Sub

    Private Sub txtWtOz_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWtOz.Validating
        Try
            If Val(txtWtLbs.Text) <= 0 Then
                If Val(txtWtOz.Text) >= 16 Then
                    txtWtLbs.Text = Val(txtWtOz.Text) \ 16
                    txtWtOz.Text = Format(Val(txtWtOz.Text) Mod 16, "#0.00")
                End If
            Else
                If Val(txtWtOz.Text) >= 16 Then
                    MessageBox.Show("Invalid value of Oz", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtWtOz.Focus()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtWtLbs_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWtLbs.KeyUp
        If Len(txtWtLbs.Text) > 0 Or Len(txtWtOz.Text) > 0 Then
            txtWeightlbs.Enabled = False
            txtWeightKg.Enabled = False
        Else
            txtWeightlbs.Enabled = True
            txtWeightKg.Enabled = True
            txtWtLbs.Enabled = True
            txtWtOz.Enabled = True
        End If
    End Sub



    Private Sub txtWtLbs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtLbs.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtWtLbs.Text
        strHeight = GetPositiveString(strHeight)
        txtWtLbs.Text = strHeight
        'Shubhangi 20091125
        'Validation for special chaacters.
        txtWtLbs.Text = GetValidString(txtWtLbs.Text)
        If _WaightFlag = WeightIn.LbsOz Then
            If Val(txtWtLbs.Text) > 0 Or Val(txtWtOz.Text) > 0 Then
                txtWeightlbs.Text = Format(Val(txtWtLbs.Text) + Val(txtWtOz.Text) / 16, "#0.000")
                'txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45, "#0.000")
                ''for Intuit portal certification : 20140923
                txtWeightKg.Text = Format(Val(txtWeightlbs.Text) * 0.45359237, "#0.00")
                If txtWeightlbs.Tag <> 0 Then
                    txtWeightChanged.Text = Format((Val(txtWeightlbs.Text) - txtWeightlbs.Tag), "#0.00")
                End If
            Else
                txtWeightlbs.Clear()
                txtWeightKg.Clear()
                txtWeightChanged.Clear()
            End If
            Calc_WtKg_BMI()
            BMIPercentile()
        End If
    End Sub


    Private Sub AllTextChanged_Event(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBMI.TextChanged, txtODI.TextChanged, txtlefteyepressure.TextChanged, txtRighteyepressure.TextChanged, txtPEFR1.TextChanged, txtPulseOX.TextChanged, txtSupplement.TextChanged, txtPulsePerMinute.TextChanged, txtPulseRate.TextChanged, txtBPStandingMin.TextChanged, txtBPStandingMax.TextChanged, txtBPSittingMax.TextChanged, txtBPSittingMin.TextChanged, txtCircum.TextChanged, txtCircumInch.TextChanged, txtComment.TextChanged, txtft.TextChanged, txtHtInCm.TextChanged, txtHtInInches.TextChanged, txtInch.TextChanged, txtPEFR2.TextChanged, txtPEFR3.TextChanged, txtRespiratory.TextChanged, txtStature.TextChanged, txtStatureinInch.TextChanged, txtTemperature.TextChanged, txtTemperatureCelcius.TextChanged, txtTHR.TextChanged, txtTHRMax.TextChanged, txtTHRMin.TextChanged, txtTHRperMax.TextChanged, txtTHRMin.TextChanged, txtTHRperMax.TextChanged, txtTHRperMin.TextChanged, txtWeightChanged.TextChanged, txtWeightKg.TextChanged, txtWeightKg.TextChanged, txtWeightlbs.TextChanged, txtWtLbs.TextChanged, txtWtOz.TextChanged, txtneckcircumCm.TextChanged, txtneckcircumInch.TextChanged
        If (sender.name <> "txtComment") Then
            Dim txt As TextBox
            txt = sender
            If (IsNothing(txt)) Then
                Exit Sub
            End If
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If
        End If

        If _IsRecordsLoading = False Then
            _IsRecordModified = True
        End If
    End Sub

    Private Sub txtODI_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtODI.KeyUp
        Dim strODI As String = txtODI.Text
        Dim i As Int16
        If strODI <> "" Then
            i = Convert.ToInt16(strODI)
        End If

        If i > 100 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtODI_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtODI.TextChanged
        Dim strODI As String = txtODI.Text



        strODI = GetPositiveString(strODI)
        strODI = AllowStringUpto100(strODI)
        txtODI.Text = strODI
        txtODI.Text = GetValidString(txtODI.Text)
    End Sub
    'Shubhangi 20100212 for Case No: GLO2010-0004416
    Private Sub AllTextValidating_Event(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMI.Validating, txtBPSittingMax.Validating, txtBPSittingMin.Validating, txtCircum.Validating, txtCircumInch.Validating, txtneckcircumCm.Validating, txtneckcircumInch.Validating, txtft.Validating, txtHtInCm.Validating, txtHtInInches.Validating, txtInch.Validating, txtPEFR1.Validating, txtPEFR2.Validating, txtPEFR3.Validating, txtPulsePerMinute.Validating, txtRespiratory.Validating, txtStature.Validating, txtStatureinInch.Validating, txtTemperature.Validating, txtTemperatureCelcius.Validating, txtTHR.Validating, txtTHRMax.Validating, txtTHRMin.Validating, txtTHRperMax.Validating, txtTHRMin.Validating, txtTHRperMax.Validating, txtTHRperMin.Validating, txtWeightChanged.Validating, txtWeightKg.Validating, txtWeightKg.Validating, txtWeightlbs.Validating, txtWtLbs.Validating, txtWtOz.Validating, txtTHRperMin.Validating, txtTHRperMax.Validating, txtlefteyepressure.Validating, txtRighteyepressure.Validating, txtBPStandingMax.Validating, txtBPStandingMin.Validating, txtODI.Validating, txtPulseOX.Validating, txtSupplement.Validating
        Try
            Dim txt As TextBox
            txt = sender
            If (IsNothing(txt)) Then
                Exit Sub
            End If
            If IsNumeric(txt.Text) = False Then
                txt.Text = ""
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Private Sub trbPainLevel_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trbPainLevel.ValueChanged
        If _IsRecordsLoading = False Then
            _IsRecordModified = True
        End If
    End Sub


    Private Sub dtLastmenstrualperiod_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtLastmenstrualperiod.ValueChanged

    End Sub


    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click

    End Sub

    Private Sub Label26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label26.Click

    End Sub

    Private Sub Label28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label28.Click

    End Sub

    Private Sub groupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles groupBox1.Enter

    End Sub

    Private Sub txtneckcircumInch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtneckcircumInch.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtneckcircumInch.Text, e)
        _NeckCircumInch = True
    End Sub

    Private Sub txtneckcircumInch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtneckcircumInch.KeyUp
        Try
            If Len(txtneckcircumInch.Text) > 0 Then
                txtneckcircumCm.Enabled = False
            Else
                txtneckcircumCm.Enabled = True
                txtneckcircumInch.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtneckcircumInch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtneckcircumInch.TextChanged
        Try
            ''Added by Mayuri:20100625-do not allow negative values after copy-paste
            Dim strHeight As String = txtneckcircumInch.Text
            strHeight = GetPositiveString(strHeight)
            txtneckcircumInch.Text = strHeight
            txtneckcircumInch.Text = GetValidString(txtneckcircumInch.Text)

            If _NeckCircumInch = True Then
                If txtneckcircumInch.Text <> "" Then
                    txtneckcircumCm.Text = Format(Val(txtneckcircumInch.Text) * 2.54, "#0.00")
                Else
                    txtneckcircumCm.Clear()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtneckcircumCm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtneckcircumCm.KeyPress
        Try
            AllowDecimal(txtneckcircumCm.Text, e)
            _NeckCircumInch = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtneckcircumCm_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtneckcircumCm.KeyUp
        Try
            If Len(txtneckcircumCm.Text) > 0 Then
                txtneckcircumInch.Enabled = False
            Else
                txtneckcircumInch.Enabled = True
                txtneckcircumCm.Enabled = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        End Try
    End Sub

    Private Sub txtneckcircumCm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtneckcircumCm.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtneckcircumCm.Text
        strHeight = GetPositiveString(strHeight)
        txtneckcircumCm.Text = strHeight
        txtneckcircumCm.Text = GetValidString(txtneckcircumCm.Text)
        Try
            If _NeckCircumInch = False Then
                If txtneckcircumCm.Text <> "" Then
                    txtneckcircumInch.Text = Format(Val(txtneckcircumCm.Text) * 0.3937008, "#0.00")
                Else
                    txtneckcircumInch.Clear()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtlefteyepressure_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlefteyepressure.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtlefteyepressure.Text, e)
    End Sub

    Private Sub txtRighteyepressure_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRighteyepressure.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtRighteyepressure.Text, e)
    End Sub



    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click

    End Sub

    Private Sub txtPEFR1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPEFR1.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtPEFR1.Text
        strHeight = GetPositiveString(strHeight)
        txtPEFR1.Text = strHeight
        txtPEFR1.Text = GetValidString(txtPEFR1.Text)
    End Sub

    Private Sub txtlefteyepressure_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlefteyepressure.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtlefteyepressure.Text
        strHeight = GetPositiveString(strHeight)
        txtlefteyepressure.Text = strHeight
        txtlefteyepressure.Text = GetValidString(txtlefteyepressure.Text)
    End Sub

    Private Sub txtRighteyepressure_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRighteyepressure.TextChanged
        ''Added by Mayuri:20100625-do not allow negative values after copy-paste
        Dim strHeight As String = txtRighteyepressure.Text
        strHeight = GetPositiveString(strHeight)
        txtRighteyepressure.Text = strHeight
        txtRighteyepressure.Text = GetValidString(txtRighteyepressure.Text)
    End Sub
    '   Dim ToolTip2 As System.Windows.Forms.ToolTip
    ' Private Sub ValidateTextBox2(sender As System.Object, validationvalue As String)

    ' If Val(TextBox2.Text) > 500 Then
    '  ShowToolTip(sender, validationvalue)
    ' Else
    '  HideToolTip(sender, e)
    ' End If

    '  End Sub
#Region "Change the Color of the text "
    'Public Sub ShowValueRange(ByVal sender As Object)
    'Try
    '    If IsVitalEnabled Then
    '        Dim txt As TextBox = TryCast(sender, TextBox)
    '        Dim textString As String = txt.Name
    '        ValidateTextBox2(sender, ValidateVitalsAge(textString))
    '    End If
    'Catch ex As Exception

    'End Try

    'End Sub
    'Dim objsender As Object = Nothing
    Public Sub ChangeColorofInvalidValues(ByVal sender As Object)
        Try

            If IsVitalEnabled Then


                Dim txt As TextBox = TryCast(sender, TextBox)
                If (IsNothing(txt)) Then
                    Exit Sub
                End If
                Dim textString As String = txt.Name
                'If (Not IsNothing(objsender)) Then
                '    If (TryCast(objsender, TextBox).Name <> txt.Name) Then
                '        ShowValueRange(objsender)
                '        objsender = txt
                '    End If
                'Else

                '    objsender = txt
                'End If

                '  ValidateTextBox2(sender, ValidateVitalsAge(textString))
                Select Case txt.Name

                    Case "txtHtInInches", "txtft", "txtInch", "txtHtInCm"
                        If txtHtInInches.Text.Trim() <> "" Then
                            lblhin.Text = ValidateVitalsAge(textString)
                            If (lblhin.Text.Trim() <> "") Then
                                lblhin.Text = "in " & lblhin.Text
                                lblhcm.Text = "cm " & lblhcm.Text
                            End If
                        Else
                            txtft.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                            txtft.ForeColor = Color.Black
                            txtInch.BackColor = Color.White
                            txtInch.ForeColor = Color.Black
                            txtft.BackColor = Color.White
                            txtHtInInches.ForeColor = Color.Black
                            txtHtInInches.BackColor = Color.White
                            txtHtInCm.ForeColor = Color.Black
                            txtHtInCm.BackColor = Color.White
                            lblHeight.Text = ""
                            lblhin.Text = ""
                            lblhcm.Text = ""

                        End If

                    Case "txtRespiratory"
                        If txtRespiratory.Text.Trim() <> "" Then

                            lblrsprt.Text = ValidateVitalsAge(textString)
                            If (lblrsprt.Text.Trim() <> "") Then
                                lblrsprt.Text = "Resp. " & lblrsprt.Text
                            End If
                        Else
                            lblrsprt.Text = ""
                            txtRespiratory.ForeColor = Color.Black
                            txtRespiratory.BackColor = Color.White
                            txtRespiratory.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        End If

                    Case "txtTemperatureCelcius", "txtTemperature"
                        If txtTemperature.Text.Trim() <> "" Then
                            lbltmpf.Text = ValidateVitalsAge(textString)
                            If (lbltmpf.Text.Trim() <> "") Then
                                lbltmpf.Text = "F " & lbltmpf.Text
                                lbltc.Text = "C " & lbltc.Text
                            End If
                            lbltmpf.ForeColor = Color.Red
                            lbltc.ForeColor = Color.Red
                        Else
                            txtTemperature.ForeColor = Color.Black
                            txtTemperatureCelcius.ForeColor = Color.Black
                            txtTemperature.BackColor = Color.White
                            txtTemperatureCelcius.BackColor = Color.White
                            txtTemperatureCelcius.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                            txtTemperature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                            lbltmpf.Text = ""
                            lbltc.Text = ""
                        End If

                    Case "txtPulseOX"
                        If txtPulseOX.Text.Trim() <> "" Then
                            lblpox.Text = ValidateVitalsAge(textString)
                            If (lblpox.Text.Trim() <> "") Then
                                lblpox.Text = "Pul.Ox " & lblpox.Text
                            End If
                        Else
                            txtPulseOX.ForeColor = Color.Black
                            txtPulseOX.BackColor = Color.White
                            lblpox.Text = ""
                            txtPulseOX.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                        End If

                    Case "txtSupplement"
                            If txtSupplement.Text.Trim() <> "" Then
                                lblSupplement.Text = ValidateVitalsAge(textString)
                            If (lblSupplement.Text.Trim() <> "") Then
                                lblSupplement.Text = "Pul.Ox2 " & lblSupplement.Text
                            End If
                        Else
                            txtSupplement.ForeColor = Color.Black
                            txtSupplement.BackColor = Color.White
                            lblSupplement.Text = ""
                            txtSupplement.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                            End If

                    Case "txtCircumInch", "txtCircum"
                            If txtCircum.Text.Trim() <> "" Then
                                lblhccm.Text = ValidateVitalsAge(textString)
                                If (lblhccm.Text.Trim() <> "") Then
                                    lblhccm.Text = "cm " & lblhccm.Text
                                End If
                                If (lblhcin.Text.Trim() <> "") Then
                                    lblhcin.Text = "in " & lblhcin.Text
                                End If
                                lblhccm.ForeColor = Color.Red
                                lblhcin.ForeColor = Color.Red
                            Else
                                txtCircum.ForeColor = Color.Black
                                txtCircum.BackColor = Color.White
                                txtCircumInch.BackColor = Color.White
                                lblhccm.Text = ""
                                lblhcin.Text = ""
                                txtCircumInch.ForeColor = Color.Black
                                txtCircumInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                txtCircum.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) ' IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                            End If

                    Case "txtStatureinInch", "txtStature"
                            If txtStature.Text.Trim() <> "" Then

                                lblstcm.Text = ValidateVitalsAge(textString)
                                If (lblstcm.Text.Trim() <> "") Then
                                    lblstcm.Text = "cm " & lblstcm.Text
                                End If
                                If (lblstin.Text.Trim() <> "") Then
                                    lblstin.Text = "in " & lblstin.Text
                                End If
                                lblstcm.ForeColor = Color.Red
                                lblstin.ForeColor = Color.Red
                            Else
                                txtStature.ForeColor = Color.Black
                                txtStatureinInch.ForeColor = Color.Black
                                txtStature.BackColor = Color.White
                                txtStatureinInch.BackColor = Color.White
                                lblstcm.Text = ""
                                lblstin.Text = ""
                                txtStature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                txtStatureinInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)


                            End If

                    Case "txtPulsePerMinute"
                            If txtPulsePerMinute.Text.Trim() <> "" Then
                                lblppm.Text = ValidateVitalsAge(textString)
                                If (lblppm.Text.Trim() <> "") Then
                                    lblppm.Text = "bpm " & lblppm.Text
                                End If
                            Else
                                txtPulsePerMinute.ForeColor = Color.Black
                                txtPulsePerMinute.BackColor = Color.White
                                lblppm.Text = ""
                                txtPulsePerMinute.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)


                            End If

                    Case "txtBPSittingMin"
                            If txtBPSittingMin.Text.Trim() <> "" Then
                                lblbpsidia.Text = ValidateVitalsAge(textString)
                                If (lblbpsidia.Text.Trim() <> "") Then
                                    lblbpsidia.Text = "Diastolic " & lblbpsidia.Text
                                End If
                            Else

                                lblbpsidia.Text = ""
                                txtBPSittingMin.BackColor = Color.White
                                txtBPSittingMin.ForeColor = Color.Black
                                txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                            End If

                    Case "txtBPSittingMax"
                            If txtBPSittingMax.Text.Trim() <> "" Then
                                lblbpsisys.Text = ValidateVitalsAge(textString)
                                If (lblbpsisys.Text.Trim() <> "") Then
                                    lblbpsisys.Text = " Systolic " & lblbpsisys.Text
                                End If
                            Else
                                txtBPSittingMax.ForeColor = Color.Black
                                txtBPSittingMax.BackColor = Color.White
                                lblbpsisys.Text = ""
                                txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                            End If

                    Case "txtBPStandingMin"

                            If txtBPStandingMin.Text.Trim() <> "" Then
                                lblbpstdia.Text = ValidateVitalsAge(textString)
                                If (lblbpstdia.Text.Trim() <> "") Then
                                    lblbpstdia.Text = " Diastolic " & lblbpstdia.Text
                                End If
                            Else
                                txtBPStandingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                lblbpstdia.Text = ""
                                txtBPStandingMin.BackColor = Color.White
                                txtBPStandingMin.ForeColor = Color.Black
                            End If


                    Case "txtBPStandingMax"
                            If txtBPStandingMax.Text.Trim() <> "" Then
                                lblbpstsys.Text = ValidateVitalsAge(textString)
                                If (lblbpstsys.Text.Trim() <> "") Then
                                    lblbpstsys.Text = " Systolic " & lblbpstsys.Text
                                End If
                            Else
                                txtBPStandingMax.ForeColor = Color.Black
                                lblbpstsys.Text = ""
                                txtBPStandingMax.BackColor = Color.White
                                txtBPStandingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular)' New Font(Me.Font, FontStyle.Regular)

                            End If

                    Case "txtWeightlbs", "txtWtLbs", "txtWeightKg", "txtWtOz"
                            If txtWeightlbs.Text.Trim <> "" Then
                                lblwt.Text = ValidateVitalsAge(textString)
                                If (lblwkg.Text.Trim() <> "") Then
                                    lblwkg.Text = "kg " & lblwkg.Text
                                    lblwt.Text = "lbs " & lblwt.Text
                                End If

                            Else
                                txtWeightlbs.ForeColor = Color.Black
                                txtWtLbs.ForeColor = Color.Black
                                txtWeightKg.ForeColor = Color.Black
                                txtWtOz.ForeColor = Color.Black
                                txtWeightlbs.BackColor = Color.White
                                txtWtLbs.BackColor = Color.White
                                txtWeightKg.BackColor = Color.White
                                txtWtOz.BackColor = Color.White
                                lblwt.Text = ""
                                lblwoz.Text = ""
                                lblwkg.Text = ""
                                lblwt.Text = ""
                                txtWtLbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                txtWeightKg.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) New Font(Me.Font, FontStyle.Regular)
                                txtWtOz.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                txtWeightlbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                            End If

                    Case "txtPEFR1"
                            '''''''Added by Ujwala Atre as on 20100918 - for PEFR Validation
                            If txtPEFR1.Text.Trim <> "" Then
                                If ValidtPEFR() = True Then
                                    txtPEFR1.ForeColor = Color.Black
                                    txtPEFR1.BackColor = Color.White
                                    txtPEFR1.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                                Else
                                    txtPEFR1.ForeColor = Color.White
                                    txtPEFR1.BackColor = Color.Red
                                    txtPEFR1.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                                    lblPEFR.ForeColor = Color.Red
                                End If
                            End If
                            '''''''Added by Ujwala Atre as on 20100918 - for PEFR Validation

                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim minTempValue As Double = 0
    Dim maxTempValue As Double = 0
    Public Function ValidateVitalsAge(ByVal txtName As String) As String

        Dim myRespristoryRate As String = ""
        minTempValue = 0
        maxTempValue = 0
        Select Case txtName

            Case "txtRespiratory"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.RespiratoryRate, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtRespiratory.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtRespiratory.Text.Trim()) > maxTempValue) Then
                        txtRespiratory.ForeColor = Color.White
                        txtRespiratory.BackColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblrsprt.ForeColor = Color.Red
                        txtRespiratory.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) ' IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)

                    Else
                        txtRespiratory.ForeColor = Color.Black
                        txtRespiratory.BackColor = Color.White
                        txtRespiratory.ForeColor = Color.Black
                        txtRespiratory.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        lblrsprt.Text = ""
                    End If
                End If

            Case "txtPulseOX"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulseOX, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtPulseOX.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtPulseOX.Text.Trim()) > maxTempValue) Then
                        txtPulseOX.ForeColor = Color.White
                        txtPulseOX.BackColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblpox.ForeColor = Color.Red
                        txtPulseOX.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        txtPulseOX.ForeColor = Color.Black
                        txtPulseOX.BackColor = Color.White
                        txtPulseOX.ForeColor = Color.Black
                        lblpox.Text = ""
                        txtPulseOX.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                    End If
                End If


            Case "txtSupplement"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulseOX, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtSupplement.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtSupplement.Text.Trim()) > maxTempValue) Then
                        txtSupplement.ForeColor = Color.White
                        txtSupplement.BackColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblSupplement.ForeColor = Color.Red
                        txtSupplement.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        txtSupplement.ForeColor = Color.Black
                        txtSupplement.BackColor = Color.White
                        txtSupplement.ForeColor = Color.Black
                        lblSupplement.Text = ""
                        txtSupplement.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                    End If
                End If

            Case "txtTemperatureCelcius", "txtTemperature"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Temperature, minTempValue, maxTempValue)
                    If (Convert.ToDouble(txtTemperature.Text.Trim()) < minTempValue) Or (Convert.ToDouble(txtTemperature.Text.Trim()) > maxTempValue) Then
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        Dim minTemp As String = ""
                        Dim maxTemp As String = ""
                        minTemp = Format((5 / 9) * (minTempValue - 32), "#0.00")
                        maxTemp = Format((5 / 9) * (maxTempValue - 32), "#0.00")
                        lbltc.Text = "(" & minTemp & "-" & maxTemp & ")"
                        txtTemperature.ForeColor = Color.White
                        txtTemperatureCelcius.ForeColor = Color.White
                        txtTemperature.BackColor = Color.Red
                        txtTemperatureCelcius.BackColor = Color.Red
                        txtTemperatureCelcius.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular)'New Font(Me.Font, FontStyle.Bold)
                        txtTemperature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular)' New Font(Me.Font, FontStyle.Bold)
                    Else

                        txtTemperature.ForeColor = Color.Black
                        txtTemperatureCelcius.ForeColor = Color.Black
                        txtTemperature.BackColor = Color.White
                        txtTemperatureCelcius.BackColor = Color.White
                        txtTemperatureCelcius.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        txtTemperature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        lbltc.Text = ""
                        lbltmpf.Text = ""
                    End If
                End If

            Case "txtStatureinInch", "txtStature" ''done
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Stature, minTempValue, maxTempValue)
                    If (Convert.ToDouble(txtStature.Text.Trim()) < minTempValue) Or (Convert.ToDouble(txtStature.Text.Trim()) > maxTempValue) Then
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        Dim _StratureMin As Double = 0
                        Dim _StratureMax As Double = 0
                        _StratureMin = Format(minTempValue * 0.3937008, "#0.00")
                        _StratureMax = Format(maxTempValue * 0.3937008, "#0.00")
                        lblstin.Text = "(" & _StratureMin & "-" & _StratureMax & ")"
                        txtStature.BackColor = Color.Red
                        txtStatureinInch.BackColor = Color.Red
                        txtStature.ForeColor = Color.White
                        txtStatureinInch.ForeColor = Color.White
                        txtStature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        txtStatureinInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else

                        txtStature.ForeColor = Color.Black
                        txtStature.BackColor = Color.White
                        txtStatureinInch.BackColor = Color.White
                        txtStatureinInch.ForeColor = Color.Black
                        lblstcm.Text = ""
                        lblstin.Text = ""
                        txtStature.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        txtStatureinInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                    End If

                End If


            Case "txtPulsePerMinute" ''Done
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.PulsePerMinute, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtPulsePerMinute.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtPulsePerMinute.Text.Trim()) > maxTempValue) Then
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        txtPulsePerMinute.BackColor = Color.Red
                        txtPulsePerMinute.ForeColor = Color.White
                        lblppm.ForeColor = Color.Red
                        txtPulsePerMinute.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        txtPulsePerMinute.ForeColor = Color.Black
                        txtPulsePerMinute.BackColor = Color.White
                        lblppm.Text = ""
                        txtPulsePerMinute.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                    End If
                End If

            Case "txtCircumInch", "txtCircum"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.HeadCircumference, minTempValue, maxTempValue)
                    If (Convert.ToDouble(txtCircum.Text.Trim()) < minTempValue) Or (Convert.ToDouble(txtCircum.Text.Trim()) > maxTempValue) Then
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        Dim _minHeadCirf As Double = 0
                        Dim _maxHeadCirf As Double = 0
                        '0.3937008
                        _minHeadCirf = Format(minTempValue * 0.3937008, "#0.00")
                        _maxHeadCirf = Format(maxTempValue * 0.3937008, "#0.00")
                        lblhcin.Text = "(" & _minHeadCirf & "-" & _maxHeadCirf & ")"
                        txtCircum.ForeColor = Color.White
                        txtCircum.BackColor = Color.Red
                        txtCircumInch.ForeColor = Color.White
                        txtCircumInch.BackColor = Color.Red
                        txtCircumInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        txtCircum.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        lblhccm.ForeColor = Color.Red
                        lblhcin.ForeColor = Color.Red
                    Else
                        txtCircum.ForeColor = Color.Black
                        txtCircum.BackColor = Color.White
                        txtCircumInch.BackColor = Color.White
                        lblhccm.Text = ""
                        lblhcin.Text = ""
                        txtCircumInch.ForeColor = Color.Black
                        txtCircumInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        txtCircum.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular)'New Font(Me.Font, FontStyle.Regular)

                    End If
                End If

            Case "txtBPSittingMin"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPSittingMin.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPSittingMin.Text.Trim()) > maxTempValue) Then
                        txtBPSittingMin.ForeColor = Color.Red
                        txtBPSittingMin.BackColor = Color.Red
                        txtBPSittingMin.ForeColor = Color.White
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblbpsidia.ForeColor = Color.Red
                        txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        lblbpsidia.Text = ""
                        txtBPSittingMin.BackColor = Color.White
                        txtBPSittingMin.ForeColor = Color.Black
                        txtBPSittingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                    End If
                End If

            Case "txtBPSittingMax"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPSittingMax.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPSittingMax.Text.Trim()) > maxTempValue) Then
                        txtBPSittingMax.ForeColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblbpsisys.ForeColor = Color.Red
                        txtBPSittingMax.BackColor = Color.Red
                        txtBPSittingMax.ForeColor = Color.White
                        txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular)'New Font(Me.Font, FontStyle.Bold)

                    Else
                        txtBPSittingMax.ForeColor = Color.Black
                        txtBPSittingMax.BackColor = Color.White
                        lblbpsisys.Text = ""
                        txtBPSittingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                    End If
                End If

            Case "txtBPStandingMin"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPDiastolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPStandingMin.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPStandingMin.Text.Trim()) > maxTempValue) Then
                        txtBPStandingMin.BackColor = Color.Red
                        txtBPStandingMin.ForeColor = Color.White
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        lblbpstdia.ForeColor = Color.Red
                        txtBPStandingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                    Else
                        txtBPStandingMin.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        lblbpstdia.Text = ""
                        txtBPStandingMin.BackColor = Color.White
                        txtBPStandingMin.ForeColor = Color.Black

                    End If
                End If

            Case "txtBPStandingMax"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.BPSystolic, minTempValue, maxTempValue)
                    If (Convert.ToInt32(txtBPStandingMax.Text.Trim()) < minTempValue) Or (Convert.ToInt32(txtBPStandingMax.Text.Trim()) > maxTempValue) Then
                        txtBPStandingMax.ForeColor = Color.Red
                        lblbpstsys.ForeColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"
                        txtBPStandingMax.BackColor = Color.Red
                        txtBPStandingMax.ForeColor = Color.White
                        txtBPStandingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)

                    Else
                        txtBPStandingMax.ForeColor = Color.Black
                        lblbpstsys.Text = ""
                        txtBPStandingMax.BackColor = Color.White
                        txtBPStandingMax.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                    End If
                End If

            Case "txtHtInInches", "txtft", "txtInch", "txtHtInCm"
                If txtHtInInches.Text <> "" Then
                    If Not IsNothing(objclsPatientVitals) Then
                        objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Height, minTempValue, maxTempValue)
                        If (Convert.ToSingle(txtHtInInches.Text.Trim()) < minTempValue) Or (Convert.ToSingle(txtHtInInches.Text.Trim()) > maxTempValue) Then

                            Dim _minHeightInCm As Double = 0
                            Dim _maxHeightInCm As Double = 0

                            _minHeightInCm = Format(minTempValue * 2.54, "#0.00")

                            _maxHeightInCm = Format(maxTempValue * 2.54, "#0.00")

                            lblhcm.Text = "(" & _minHeightInCm & "-" & _maxHeightInCm & ")"
                            If txtft.Text <> "" Then
                                txtft.ForeColor = Color.White
                                txtft.BackColor = Color.Red
                                txtft.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                            End If

                            If txtInch.Text <> "" Then
                                txtInch.ForeColor = Color.White
                                txtInch.BackColor = Color.Red
                                txtInch.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                            End If

                            txtHtInCm.ForeColor = Color.White
                            txtHtInCm.BackColor = Color.Red
                            txtHtInCm.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)

                            txtHtInInches.ForeColor = Color.White
                            txtHtInInches.BackColor = Color.Red
                            txtHtInInches.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)

                            myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"

                            lblhin.ForeColor = Color.Red
                            lblhcm.ForeColor = Color.Red

                        Else
                            txtft.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                            txtft.ForeColor = Color.Black
                            txtInch.BackColor = Color.White

                            txtInch.ForeColor = Color.Black
                            txtft.BackColor = Color.White

                            txtHtInCm.ForeColor = Color.Black
                            txtHtInCm.BackColor = Color.White

                            txtHtInInches.ForeColor = Color.Black
                            txtHtInInches.BackColor = Color.White

                            lblHeight.Text = ""
                            lblhin.Text = ""
                            lblhcm.Text = ""

                        End If
                    End If
                End If



            Case "txtWeightlbs", "txtWtLbs", "txtWeightKg", "txtWtOz"
                If Not IsNothing(objclsPatientVitals) Then
                    objclsPatientVitals.GetMinMaxValues(_AgeInMonths, strPatientGender, gloStream.Vitals.Supportings.VitalTypes.Weight, minTempValue, maxTempValue)
                    If (Convert.ToSingle(txtWeightlbs.Text.Trim()) < minTempValue) Or (Convert.ToSingle(txtWeightlbs.Text.Trim()) > maxTempValue) Then
                        txtWeightlbs.BackColor = Color.Red
                        txtWtLbs.BackColor = Color.Red
                        txtWtLbs.ForeColor = Color.White

                        txtWeightKg.ForeColor = Color.White
                        txtWeightKg.BackColor = Color.Red
                        txtWtOz.BackColor = Color.Red
                        txtWtOz.ForeColor = Color.White
                        lblwt.ForeColor = Color.Red
                        lblwoz.ForeColor = Color.Red
                        lblwkg.ForeColor = Color.Red
                        myRespristoryRate = "(" & minTempValue & "-" & maxTempValue & ")"

                        Dim _minWeigthInKg As Double = 0
                        Dim _maxWeigthInKg As Double = 0

                        _minWeigthInKg = Format(minTempValue * 0.45359237, "#0.000")
                        _maxWeigthInKg = Format(maxTempValue * 0.45359237, "#0.000")
                        lblwkg.Text = "(" & _minWeigthInKg & "-" & _maxWeigthInKg & ")"


                        If txtWtOz.Text = "" Then
                            txtWtOz.ForeColor = Color.Black
                            txtWtOz.BackColor = Color.White
                        End If

                        If txtWtLbs.Text = "" Then
                            txtWtLbs.ForeColor = Color.Black
                            txtWtLbs.BackColor = Color.White
                        End If

                        txtWtLbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        txtWeightKg.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        txtWtOz.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)
                        txtWeightlbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold) 'IIf(Font.Height = 8.25F, gFont_SMALL_BOLD, IIf(Font.Height = 9.0F, gFont_BOLD, New Font(Font, FontStyle.Bold))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold)

                    Else
                        txtWeightlbs.ForeColor = Color.Black
                        txtWtLbs.ForeColor = Color.Black
                        txtWeightKg.ForeColor = Color.Black
                        txtWtOz.ForeColor = Color.Black
                        txtWeightlbs.BackColor = Color.White
                        txtWtLbs.BackColor = Color.White
                        txtWeightKg.BackColor = Color.White
                        txtWtOz.BackColor = Color.White
                        lblwt.Text = ""
                        lblwoz.Text = ""
                        lblwkg.Text = ""
                        lblwt.Text = ""
                        txtWtLbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular)
                        txtWeightKg.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        txtWtOz.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)
                        txtWeightlbs.Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular) 'IIf(Font.Height = 8.25F, gFont_SMALL, IIf(Font.Height = 9.0F, gFont, New Font(Font, FontStyle.Regular))) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Bold) 'New Font(Me.Font, FontStyle.Regular) 'New Font(Me.Font, FontStyle.Regular)

                    End If
                End If

        End Select
        Return myRespristoryRate
    End Function
    Function ValidtPEFR() As Boolean
        '''''''Added by Ujwala Atre as on 20100918 - for PEFR Validation
        ValidtPEFR = True
        lblPEFR.Text = ""

        Dim VPEFR As Decimal = 0
        Dim Rng1 As Decimal = 0
        Dim Rng2 As Decimal = 0
        If IsNumeric(txtHtInCm.Text) And txtHtInCm.Text.Trim <> "" And txtHtInCm.Text.Trim <> "0" Then
            If txtPEFR1.Text.Trim <> "" Then
                VPEFR = Math.Round((Val(txtHtInCm.Text) - 80) * 5)
                Rng1 = Math.Round(VPEFR) - 100
                Rng2 = Math.Round(VPEFR) + 100
                If Not (Math.Round(Val(txtPEFR1.Text)) >= Rng1 And Math.Round(Val(txtPEFR1.Text)) <= Rng2) Then
                    lblPEFR.Text = "(" & Str(Rng1) & " - " & Str(Rng2) & ")"
                    ValidtPEFR = False
                End If
            End If
        End If

        Return ValidtPEFR
        '''''''Added by Ujwala Atre as on 20100918 - for PEFR Validation
    End Function

#End Region
#Region "TextChange Event"
    ''Height
    Private Sub txtft_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtft.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtInch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInch.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtHtInCm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHtInCm.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtHtInInches_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHtInInches.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    ''Temperature
    Private Sub txtTemperature_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperature.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtTemperatureCelcius_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperatureCelcius.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    ''weight in OZ,LBS,KG 

    Private Sub txtWeightlbs_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeightlbs.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    Private Sub txtWeightKg_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWeightKg.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    Private Sub txtWtOz_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtOz.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtWtLbs_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWtLbs.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    ''pulseOX
    Private Sub txtPulseOX_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPulseOX.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    ''pulse per minute
    Private Sub txtPulsePerMinute_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPulsePerMinute.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    ''bpSitting
    Private Sub txtBPSittingMax_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPSittingMax.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtBPSittingMin_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPSittingMin.Leave
        ChangeColorofInvalidValues(sender)
    End Sub

    ''bpStanding 
    Private Sub txtBPStandingMin_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPStandingMin.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtBPStandingMax_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBPStandingMax.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    ''Stature
    Private Sub txtStature_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStature.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtStatureinInch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStatureinInch.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    ''Circumference
    Private Sub txtCircum_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCircum.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    Private Sub txtCircumInch_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCircumInch.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
    ''REespestory rate
    Private Sub txtRespiratory_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRespiratory.Leave
        ChangeColorofInvalidValues(sender)
    End Sub
#End Region
    'Dim pt As Point = Nothing
    'Private Sub ShowToolTip(sender As System.Object, Message As String)
    '    If (Message.Trim() <> "") Then


    '        If IsNothing(ToolTip2) = False Then

    '            If ToolTip2.Tag <> sender.Name Then
    '                ToolTip2.Dispose()
    '                ToolTip2 = Nothing
    '            End If

    '        End If

    '        If IsNothing(ToolTip2) = True Then
    '            ToolTip2 = New ToolTip
    '            ToolTip2.Tag = sender.Name
    '            ToolTip2.IsBalloon = True
    '            ToolTip2.ForeColor = Color.Red

    '        End If
    '        ''  Dim loc As Point = sender.PointToScreen(Point.Empty)
    '        Dim loc As Point = sender.FindForm().PointToClient(
    '  sender.Parent.PointToScreen(sender.Location))
    '        ToolTip2.Show(Message, Me, Loc.X, Loc.Y, 10000)
    '        'ToolTip2.Show(Message, Me, sender.Location.Left + pt.X, sender.Location.Top + pt.Y, 10000)
    '    End If
    'End Sub

    'Private Sub HideToolTip(sender As System.Object)
    '    If IsNothing(ToolTip2) = False Then
    '        If ToolTip2.Tag = sender.Name Then
    '            ToolTip2.Dispose()
    '            ToolTip2 = Nothing
    '        End If

    '    End If
    'End Sub

    Private Sub txtTemperature_TextAlignChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTemperature.TextAlignChanged

    End Sub

    ''Start :: Pain Level
    Private Sub chkpain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkpain.Click
        If chkpain.Checked = True Then
            trbPainLevel.Enabled = True
        Else
            trbPainLevel.Enabled = False
        End If
    End Sub
    ''End :: Pain Level

    Private Sub chkPainWithMedication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPainWithMedication.Click
        If chkPainWithMedication.Checked = True Then
            trbPainWithMedication.Enabled = True
        Else
            trbPainWithMedication.Enabled = False
        End If
    End Sub

    Private Sub chkPainWithoutMedication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPainWithoutMedication.Click
        If chkPainWithoutMedication.Checked = True Then
            trbPainWithoutMedication.Enabled = True
        Else
            trbPainWithoutMedication.Enabled = False
        End If
    End Sub

    Private Sub chkPainWorst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPainWorst.Click
        If chkPainWorst.Checked = True Then
            trbPainWorst.Enabled = True
        Else
            trbPainWorst.Enabled = False
        End If
    End Sub

    Private Sub txtODI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtODI.KeyPress
        On Error Resume Next

        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
        Exit Sub
    End Sub

    ''following code added by Abhijeet on 20110318 for capturing vitals from WelchAllyn device 
    Function DeviceConnect(ByVal confgiFilename As String) As Boolean
        'Connect to the SDK
        Return Connect(confgiFilename)
    End Function

    Function Connect(ByVal configFilename As String) As Boolean
        Dim boolReturn As Boolean = False
        Try
            If gm_oConnectivity Is Nothing Then

                Try
                    'Instantiate a new SDK object
                    If gm_oConnectivity Is Nothing Then
                        gm_oConnectivity = New WAConnectivityATLClass
                    End If

                    'Obtain the WADevices onject
                    m_oDevices = gm_oConnectivity.GetDevices

                Catch ex As Exception
                    MessageBox.Show("Vital device SDK not found. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End Try

            Else '' connectivity available

                'Obtain the WADevices object
                m_oDevices = gm_oConnectivity.GetDevices
            End If

            If Not gm_oConnectivity.IsConnected() Then
                Call gm_oConnectivity.Connect(configFilename)
            End If

            boolReturn = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Connection With Vital Device SDK : " & ex.ToString, False)
        End Try
        Return boolReturn

    End Function

    'Disconnect from the SDK
    Sub Disconnect()
        Try
            If Not IsNothing(gm_oConnectivity) Then
                If gm_oConnectivity.IsConnected() Then
                    gm_oConnectivity.Disconnect()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Disconnecting With Vital Device SDK : " & ex.ToString, False)
        End Try
    End Sub

    'This method is a callback function called from the SDK when a device is plugged in to the PC
    Public Sub m_oDevices_DeviceArrival(ByVal bszDeviceID As String, ByVal pDevice As WAConnSDKATLLib.WADeviceData) Handles m_oDevices.DeviceArrival
        Try
            m_oDeviceData = pDevice
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Vital Device Arrival Event : " & ex.ToString, False)
        End Try
    End Sub

    'This method is a callback function called from the SDK when a device is removed
    Public Sub m_oDevices_DeviceRemoval(ByVal bszDeviceID As String) Handles m_oDevices.DeviceRemoval
        Try
            m_oDeviceData = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error In Vital Device Removal Event : " & ex.ToString, False)
        End Try
    End Sub

    Sub CaptureDeviceVitals()

        Try
            'Problem : 00000352
            'Description : The Vital Device that is at the practice is unable to capture vitals.
            'Solution : Catch the exception and log it in log file instead of Throwing the exception.

            If Not IsNothing(m_oDeviceData) Then

                If _VitalID > 0 Then
                    If Not MessageBox.Show("Do you want to overwrite patient vitals with vitals from device ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Return
                    End If
                End If

                ''Get Height Data
                If SelectedVitals.Contains("Height (ft & in)") Or SelectedVitals.Contains("Height (cm)") Or SelectedVitals.Contains("Height (in)") Then
                    GetHeightData()
                End If

                ''Get Weight Data
                If SelectedVitals.Contains("Weight (lbs)") Or SelectedVitals.Contains("Weight (lbsoz)") Or SelectedVitals.Contains("Weight (kg)") Then
                    GetWeightData()
                End If

                ''Get BP Data
                If SelectedVitals.Contains("BP Sitting") Then
                    GetNIBPData()
                End If

                ''Get Tempature Data
                If SelectedVitals.Contains("Temperature (F)") Or SelectedVitals.Contains("Temperature (C)") Then
                    GetTemperatureData()
                End If

                ''Get Pain level Current Data
                If SelectedVitals.Contains("Pain Level : Current") Or SelectedVitals.Contains("Pain Level") Then
                    GetPainData()
                End If

                ''Get pulse per minute
                If SelectedVitals.Contains("Pulse Per Min") Then
                    GetPulsePerMinute()
                End If

                ''Get Respiratory Rate Data
                If SelectedVitals.Contains("Respiratory Rate") Then
                    GetRespirationData()
                End If

            Else
                MessageBox.Show("Unable to detect vital device.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error while retrieving vitals from device : ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals from device : " & ex.ToString, False)
        End Try

    End Sub


    Sub GetHeightData()

        Try
            If IsNothing(m_oDeviceData) Then
                Return
            End If

            'Get the height data object
            Dim heightData As IHeightData
            heightData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_HeightDataID)

            'Poll the device for the height data
            heightData.Request()

            If heightData.HEIGHT > 0 Then
                Dim heightInMM As Int32 = heightData.HEIGHT
                _HeightFlag = HtFlag.Centimeter
                txtHtInCm.Text = Format((heightInMM / 10), "#0.00")
                ChangeColorofInvalidValues(txtHtInCm)
            Else
                txtHtInCm.Text = ""
                ChangeColorofInvalidValues(txtHtInCm)
            End If

            heightData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Height) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Height) from device : " & ex.ToString, False)
        End Try

    End Sub

    ''End of changes by Abhijeet on 20110318 for capturing vitals from WelchAllyn device

    Sub GetWeightData()

        Try
            'We need the device data object to poll the device for any data      
            If IsNothing(m_oDeviceData) Then
                Return
            End If

            'Get the weight data object
            Dim weightData As IWeightData
            weightData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_WeightDataID)

            'Poll the device for the weight data
            weightData.Request()

            If weightData.WEIGHT > 0 Then
                _WaightFlag = WeightIn.Kg
                Dim weightIngms As Int32 = weightData.WEIGHT
                txtWeightKg.Text = Format(Val(weightIngms / 1000), "#0.00")
                ChangeColorofInvalidValues(txtWeightKg)
            Else
                txtWeightKg.Text = ""
                ChangeColorofInvalidValues(txtWeightKg)
            End If

            weightData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Weight) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Weight) from device : " & ex.ToString, False)
        End Try

    End Sub

    'Poll for the device for NIBP data
    Sub GetNIBPData()

        Try
            'We need the device data object to poll the device for any data
            If IsNothing(m_oDeviceData) Then
                Return
            End If

            'Get the NIBP data object
            Dim nibpData As INIBPData
            nibpData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_NIBPDataID)

            'Poll the device for the NIBP data
            nibpData.Request()

            txtBPSittingMax.Text = Math.Round((nibpData.Systolic / 100), 0)
            ChangeColorofInvalidValues(txtBPSittingMax)
            txtBPSittingMin.Text = Math.Round((nibpData.Diastolic / 100), 0)
            ChangeColorofInvalidValues(txtBPSittingMin)


            nibpData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (BP) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (BP) from device : " & ex.ToString, False)
        End Try

    End Sub

    'Poll for the device for temperature data
    Sub GetTemperatureData()

        Try
            'We need the device data object to poll the device for any data
            If m_oDeviceData Is Nothing Then
                Return
            End If

            'Get the temperature data object
            Dim temperatureData As ITempData
            temperatureData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_TempDataID)

            'Poll the device for the temperature data
            temperatureData.Request()

            Dim tempKelvin As String = temperatureData.TEMPERATURE.ToString()
            _TempInCelcius = True
            txtTemperatureCelcius.Text = Val(tempKelvin) - 273.15
            ChangeColorofInvalidValues(txtTemperatureCelcius)

            temperatureData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Temperature) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Temperature) from device : " & ex.ToString, False)
        End Try

    End Sub

    'Poll for the device for Pain data
    Sub GetPainData()
        Try
            ''Considering Current pain level

            'We need the device data object to poll the device for any data
            If m_oDeviceData Is Nothing Then
                Return
            End If

            'Get the Pain data object
            Dim painData As IPainData
            painData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_PainDataID)

            'Poll the device for the pain data
            painData.Request()

            chkpain.Checked = True
            trbPainLevel.Enabled = True
            trbPainLevel.Value = painData.PainIndex


            painData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Pain) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Pain) from device : " & ex.ToString, False)
        End Try

    End Sub

    'Poll for the device for Respiration data
    Sub GetRespirationData()

        Try
            'We need the device data object to poll the device for any data
            If m_oDeviceData Is Nothing Then
                Return
            End If

            'Get the Respiration data object
            Dim respData As IRespData
            respData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_RespDataID)

            'Poll the device for the Respiration data
            respData.Request()

            txtRespiratory.Text = respData.Respiration.ToString()
            ChangeColorofInvalidValues(txtRespiratory)

            respData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Respiration) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Respiration) from device : " & ex.ToString, False)
        End Try

    End Sub

    Sub GetPulsePerMinute()
        Try
            'We need the device data object to poll the device for any data
            If IsNothing(m_oDeviceData) Then
                Return
            End If

            'Get the NIBP data object
            Dim nibpData As INIBPData
            nibpData = m_oDeviceData.GetDatum().Item(WASDKObjectID.WA_NIBPDataID)

            'Poll the device for the NIBP data
            nibpData.Request()

            txtPulsePerMinute.Text = nibpData.HR

            nibpData = Nothing

        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Pulse) from device : " & ex.ErrorCode & " : " & ex.Message, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Error while retrieving vitals (Pulse) from device : " & ex.ToString, False)
        End Try

    End Sub

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Try
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            If IsNothing(oPatient) = False Then
                oPatient.Dispose()
                oPatient = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Private Function GetClinicCode() As String
        Dim strClinicCode As String = String.Empty
        Dim dtClinicCode As DataTable = Nothing
        Dim odb As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            odb.Connect(False)
            Dim _sqlQuery As String = "select sExternalCode from Clinic_MST"
            odb.Retrive_Query(_sqlQuery, dtClinicCode)
            If (IsNothing(dtClinicCode) = False) Then
                If dtClinicCode.Rows.Count > 0 Then
                    For i As Integer = 0 To dtClinicCode.Rows.Count - 1
                        strClinicCode = Convert.ToString(dtClinicCode.Rows(i)("sExternalCode"))
                    Next
                End If
            End If


            odb.Disconnect()

        Catch Ex As Exception
            strClinicCode = String.Empty
        Finally
            If Not IsNothing(dtClinicCode) Then
                dtClinicCode.Dispose()
                dtClinicCode = Nothing
            End If
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
        End Try
        Return strClinicCode
    End Function
    Private Function getSettingsForVitalsDevice()
        Dim dtClinicCode As DataTable = Nothing
        Dim odb As gloDatabaseLayer.DBLayer = Nothing
        Try
            _strAUSid = GetClinicCode()
            odb = New gloDatabaseLayer.DBLayer(GetConnectionString)
            odb.Connect(False)
            odb.Retrive_Query("select distinct sSettingsName,sSettingsValue from Settings where sSettingsName In ('VITALDEVICEKEY','USEVITALDEVICE','NOOFATTEMPTTOCONNECTVITALDEVICE')", dtClinicCode)
            odb.Disconnect()
            If (Not IsNothing(dtClinicCode)) AndAlso dtClinicCode.Rows.Count > 0 Then
                For Each oDataRow As DataRow In dtClinicCode.Rows
                    Select Case Convert.ToString(oDataRow("sSettingsName"))
                        Case "VITALDEVICEKEY"
                            _strVitalsKey = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "USEVITALDEVICE"
                            _isVitalsDeviceEnabled = Convert.ToString(oDataRow("sSettingsValue"))
                        Case "NOOFATTEMPTTOCONNECTVITALDEVICE"
                            If Not IsNothing(oDataRow("sSettingsValue")) AndAlso Convert.ToString(oDataRow("sSettingsValue")) <> String.Empty Then
                                _NoOfAttemptsToConnectDevice = Convert.ToInt32(oDataRow("sSettingsValue"))
                            Else
                                _NoOfAttemptsToConnectDevice = 5
                            End If
                    End Select
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(dtClinicCode) Then
                dtClinicCode.Dispose()
                dtClinicCode = Nothing
            End If
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Private Function getMedicaCategoryAtRisk() As String

        Dim odb As gloDatabaseLayer.DBLayer = Nothing
        Try

            odb = New gloDatabaseLayer.DBLayer(GetConnectionString)
            odb.Connect(False)
            Dim obj As Object = odb.ExecuteScalar_Query("select top 1 sMedicalCategory from MedicalCategory_MST where ISNULL(bisHighRisk,0)=1")
            odb.Disconnect()

            Return Convert.ToString(obj)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
        End Try
        Return Nothing
    End Function

    'code added to create task from OB Vitals
    Private Sub AddVitalTask(ByVal vitalID As Long)
        Dim _strSQL As String = ""
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            _strSQL = "Update TM_TaskMST set nReferenceID1=" & vitalID & " where nTaskGroupID = " & OBVitalTask
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _strSQL
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()


        Catch ex As SqlException
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        Finally
            If objCon.State <> ConnectionState.Closed Then
                objCon.Close()
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub
    'code added to delete task from OB Vitals
    Private Sub DeleteVitalTask()
        Dim _strSQL As String = ""
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteTaskGroup"
            objCmd.Parameters.AddWithValue("@TaskGroupID", OBVitalTask)
            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
        Catch ex As SqlException
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption)
        Finally
            If objCon.State <> ConnectionState.Closed Then
                objCon.Close()
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub
    Private Sub txtSupplement_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplement.TextChanged
        'do not allow negative values after copy-paste
        Dim strHeight As String = txtSupplement.Text
        strHeight = GetPositiveString(strHeight)
        txtSupplement.Text = strHeight
        'Validation for special characters.
        txtSupplement.Text = GetValidString(txtSupplement.Text)

        If (txtSupplement.Text = "") Then
            txtPulseRate.Text = ""
            txtPulseRate.Enabled = False
        Else
            txtPulseRate.Enabled = True
        End If

        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSupplement_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplement.Leave
        ChangeColorofInvalidValues(sender)
        If (txtSupplement.Text = "") Then
            txtPulseRate.Text = ""
            txtPulseRate.Enabled = False
        Else
            txtPulseRate.Enabled = True
        End If

    End Sub

    Private Sub txtSupplement_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplement.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtSupplement.Text, e)
    End Sub

    Private Sub txtPulseRate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPulseRate.KeyPress
        On Error Resume Next
        'Allow only numeric and decimal point keys
        AllowDecimal(txtPulseRate.Text, e)
    End Sub

    Private Sub txtPulseRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPulseRate.TextChanged
        'do not allow negative values after copy-paste
        Dim strHeight As String = txtPulseRate.Text
        strHeight = GetPositiveString(strHeight)
        txtPulseRate.Text = strHeight
        'Validation for special characters.
        txtPulseRate.Text = GetValidString(txtPulseRate.Text)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim frmOBMedCatlist As Form = Nothing
    Dim oMedicalCategory As gloListControl.gloListControl = Nothing
    Dim blnopenOBPopup As Boolean = False
    Private Function GenerateOBCase(ByVal dtvitaldate As Date) As Long
        Dim CaseIDforOBvital As Long = 0
        Try
            CaseIDforOBvital = CreateOBCase()
            If CaseIDforOBvital <= 0 Then
                '' Before case create Need to Check medical History Assigned or not 
                Dim setting As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString)
                Dim OBSpecilityActive As Object = Nothing
                setting.GetSetting("OB SPECIALITY", OBSpecilityActive)
                'SLR: Added on 11/18/2015
                setting.Dispose()
                setting = Nothing

                If OBSpecilityActive.ToString() = "1" Then
                    Dim dtdata As DataTable = GetPatientOBMedicalCategory()
                    Dim MedicalCategory As String = ""
                    If (IsNothing(dtdata) = False) Then
                        If dtdata.Rows.Count <= 0 Then
                            MedicalCategory = ""
                        Else
                            MedicalCategory = Convert.ToString(dtdata.Rows(0)("sMedicalCategory"))
                        End If
                    End If
                    If MedicalCategory <> "" Then
                        CaseIDforOBvital = InsertDefaultOBCase(MedicalCategory, dtvitaldate)
                    Else
                        Dim strSingleMedicalCategory As String = IsSingleCategoryPresent()
                        If strSingleMedicalCategory = "" Then
                            'If IsNothing(dtdata) = False Then
                            '    If dtdata.Rows.Count > 0 Then
                            'Open Popup for Assigning Medical category to patient
                            OpenOBCategoryAssignpopup(dtdata)
                            blnopenOBPopup = True
                            Dim NewMedicalCategory As String = ""
                            Dim dtRecheck As DataTable = GetPatientOBMedicalCategory()
                            If (IsNothing(dtRecheck) = False) Then
                                If dtRecheck.Rows.Count <= 0 Then
                                    NewMedicalCategory = ""
                                Else
                                    NewMedicalCategory = Convert.ToString(dtRecheck.Rows(0)("sMedicalCategory"))
                                End If
                            End If
                            If NewMedicalCategory <> "" Then
                                CaseIDforOBvital = InsertDefaultOBCase(NewMedicalCategory, dtvitaldate)
                            End If

                            If IsNothing(dtRecheck) = False Then
                                dtRecheck.Dispose()
                                dtRecheck = Nothing
                            End If

                        Else
                            CaseIDforOBvital = InsertDefaultOBCase(strSingleMedicalCategory, dtvitaldate)
                        End If
                    End If
                    If IsNothing(dtdata) = False Then
                        dtdata.Dispose()
                        dtdata = Nothing
                    End If

                End If
            End If
            Return CaseIDforOBvital
        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return CaseIDforOBvital
        End Try
    End Function

#Region "Medicalcategory Control"
    Private Sub CloseOBMedCatList()
        Try
            If IsNothing(frmOBMedCatlist) = False Then
                RemoveHandler frmOBMedCatlist.Shown, AddressOf frmOBMedCatlist_Shown
            End If
        Catch ex As Exception

        End Try
        Try
            If oMedicalCategory IsNot Nothing Then
                RemoveHandler oMedicalCategory.ItemSelectedClick, AddressOf oMedicalCategory_ItemSelectedClick
                RemoveHandler oMedicalCategory.ItemClosedClick, AddressOf oMedicalCategory_ItemClosedClick
                Try
                    If IsNothing(frmOBMedCatlist) = False Then
                        frmOBMedCatlist.Controls.Remove(oMedicalCategory)
                    End If
                Catch ex As Exception

                End Try
                Try
                    oMedicalCategory.Dispose()
                    oMedicalCategory = Nothing
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try
        Try
            If IsNothing(frmOBMedCatlist) = False Then
                frmOBMedCatlist.Close()
                frmOBMedCatlist.Dispose()
                frmOBMedCatlist = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Dim strmedcatatrisk As String = ""
    Private Sub OpenOBCategoryAssignpopup(ByVal _dt As DataTable)
        Try
            CloseOBMedCatList()
            frmOBMedCatlist = New Form()
            frmOBMedCatlist.StartPosition = FormStartPosition.CenterScreen
            frmOBMedCatlist.Width = 700
            frmOBMedCatlist.Text = "OB Medical Category"
            AddHandler frmOBMedCatlist.Shown, AddressOf frmOBMedCatlist_Shown
            oMedicalCategory = New gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.OBMedicalCategory, True, frmOBMedCatlist.Width)
            oMedicalCategory.ControlHeader = "OB Medical Category"
            AddHandler oMedicalCategory.ItemSelectedClick, AddressOf oMedicalCategory_ItemSelectedClick
            AddHandler oMedicalCategory.ItemClosedClick, AddressOf oMedicalCategory_ItemClosedClick
            frmOBMedCatlist.Controls.Add(oMedicalCategory)
            oMedicalCategory.Dock = DockStyle.Fill
            oMedicalCategory.BringToFront()
            If Not _dt Is Nothing Then
                If (_dt.Rows.Count = 0) Then
                    If Not IsNothing(_hashOBVital) Then ''added for bugid 83031
                        If (Convert.ToString(_hashOBVital("chkOBrsk")) = "1") Then
                            strmedcatatrisk = getMedicaCategoryAtRisk()
                        End If
                    End If
                End If
            End If
            If _dt IsNot Nothing Then
                For Each dr As DataRow In _dt.Rows
                    oMedicalCategory.SelectedItems.Add(Convert.ToInt64(dr("nMedicalCategoryID")), Convert.ToString(dr("sMedicalCategory")))
                Next
            End If

            ''  oMedicalCategory.OpenControl()
            oMedicalCategory.ShowHeaderPanel(False)
            frmOBMedCatlist.ShowDialog((If((frmOBMedCatlist.Parent Is Nothing), Me, frmOBMedCatlist.Parent)))
            CloseOBMedCatList()
            'RemoveHandler frmOBMedCatlist.Shown, AddressOf frmOBMedCatlist_Shown

            'If frmOBMedCatlist IsNot Nothing Then
            '    RemoveHandler oMedicalCategory.ItemSelectedClick, AddressOf oMedicalCategory_ItemSelectedClick
            '    RemoveHandler oMedicalCategory.ItemClosedClick, AddressOf oMedicalCategory_ItemClosedClick
            '    frmOBMedCatlist.Controls.Remove(oMedicalCategory)
            '    oMedicalCategory.Dispose()
            '    oMedicalCategory = Nothing
            '    frmOBMedCatlist.Close()
            '    frmOBMedCatlist.Dispose()
            '    frmOBMedCatlist = Nothing
            'End If

        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub frmOBMedCatlist_Shown(sender As Object, e As EventArgs)
        Try
            Dim oglolistcontrol As gloListControl.gloListControl = TryCast(frmOBMedCatlist.Controls(0), gloListControl.gloListControl)
            If IsNothing(oglolistcontrol) = False Then
                oglolistcontrol.OpenControl()
                If (strmedcatatrisk <> "") Then
                    If Not IsNothing(oglolistcontrol.dgListView) Then
                        If Not IsNothing(oglolistcontrol.dgListView.DataSource) Then
                            If Not IsNothing(CType(oglolistcontrol.dgListView.DataSource, DataView)) Then
                                Dim dr As DataRow() = CType(oglolistcontrol.dgListView.DataSource, DataView).Table.Select("sMedicalCategory='" & strmedcatatrisk & "'")

                                If (dr.Length > 0) Then
                                    dr(0)("Check1") = True
                                    Dim rowno As Integer = CType(oglolistcontrol.dgListView.DataSource, DataView).Table.Rows.IndexOf(dr(0))
                                    If (rowno <> -1) Then
                                        oglolistcontrol.dgListView.Rows(rowno).Selected = True
                                        oglolistcontrol.dgListView.Rows(rowno).Cells(0).Value = True
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oMedicalCategory_ItemSelectedClick(sender As Object, e As EventArgs)
        Dim dtMedCat As New DataTable()
        Dim dcPatID As New DataColumn("nPatientID")
        Dim dcMedCatID As New DataColumn("nMedicalCategoryId")
        dtMedCat.Columns.Add(dcPatID)
        dtMedCat.Columns.Add(dcMedCatID)
        If oMedicalCategory.SelectedItems.Count > 0 Then
            For i As Integer = 0 To oMedicalCategory.SelectedItems.Count - 1
                Dim dr As DataRow = dtMedCat.NewRow()
                dr("nPatientID") = Convert.ToInt64(_PatientID)
                dr("nMedicalCategoryId") = Convert.ToInt64(oMedicalCategory.SelectedItems(i).ID)
                dtMedCat.Rows.Add(dr)
            Next
        Else
            Dim dr As DataRow = dtMedCat.NewRow()
            dr("nPatientID") = Convert.ToInt64(_PatientID)
            dr("nMedicalCategoryId") = 0
            dtMedCat.Rows.Add(dr)
        End If
        SaveMedicalCategory(dtMedCat)
        'If frmOBMedCatlist IsNot Nothing Then
        '    frmOBMedCatlist.Close()
        '    frmOBMedCatlist.Dispose()
        '    frmOBMedCatlist = Nothing
        'End If
        CloseOBMedCatList()
        If IsNothing(dtMedCat) = False Then
            dtMedCat.Dispose()
            dtMedCat = Nothing
        End If
    End Sub

    Public Sub oMedicalCategory_ItemClosedClick(sender As Object, e As EventArgs)
        'If frmOBMedCatlist IsNot Nothing Then
        '    frmOBMedCatlist.Close()
        '    frmOBMedCatlist.Dispose()
        '    frmOBMedCatlist = Nothing
        'End If
        CloseOBMedCatList()
    End Sub

    Private Sub SaveMedicalCategory(Optional ByVal dt As DataTable = Nothing)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim _dtResult As DataTable = Nothing
        Try
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@tvpMedicalCategory", dt, ParameterDirection.Input, SqlDbType.Structured)
            oParam.Add("@PatientId", Convert.ToInt64(_PatientID), ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@IsFromCases", True, ParameterDirection.Input, SqlDbType.Bit)

            oDB.Connect(False)
            oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oParam, _dtResult)

            oDB.Disconnect()
        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParam) = False Then
                oParam.Dispose()
                oParam = Nothing
            End If
            If Not IsNothing(_dtResult) Then
                _dtResult.Dispose()
                _dtResult = Nothing
            End If
        End Try
    End Sub



#End Region


    Public Function InsertDefaultOBCase(ByVal MedicalCategory As String, ByVal dtVitalDate As DateTime) As Long
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim OBCaseID As Long = 0
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@MedicalCategory", MedicalCategory, ParameterDirection.Input, SqlDbType.NVarChar)
            oParam.Add("@CaseID", 0, ParameterDirection.Output, SqlDbType.BigInt)
            oParam.Add("@dtVitaldate", dtVitalDate, ParameterDirection.Input, SqlDbType.DateTime)
            Dim intResult As Integer = oDB.Execute("gsp_InsertDefaultOBCase ", oParam, OBCaseID)
            oParam.Dispose()
            oParam = Nothing
            Return OBCaseID
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return OBCaseID
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Function GetPatientOBMedicalCategory() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtdata As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientId", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetPatientOBMedicalCat", oParam, dtdata)
            oDB.Disconnect()
            Return dtdata
        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParam) = False Then
                oParam.Dispose()
                oParam = Nothing
            End If
            If IsNothing(dtdata) = False Then
                dtdata.Dispose()
                dtdata = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Check Active OB Case for OBPregnancy.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateOBCase() As Long
        Dim IsCreate As Long = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtdata As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientId", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetActiveOBCases", oParam, dtdata)
            oDB.Disconnect()
            If (IsNothing(dtdata) = False) Then
                If dtdata.Rows.Count <= 0 Then
                    IsCreate = 0
                Else
                    IsCreate = Convert.ToUInt64(dtdata.Rows(0)("nCaseId"))
                End If
            End If
            Return IsCreate
        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return IsCreate
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParam) = False Then
                oParam.Dispose()
                oParam = Nothing
            End If
            If IsNothing(dtdata) = False Then
                dtdata.Dispose()
                dtdata = Nothing
            End If
        End Try
    End Function

    Private Function IsSingleCategoryPresent() As String
        Dim SingleCategoryname As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dtdata As DataTable = Nothing
        Try
            oDB.Connect(False)
            Dim strQry As String = "SELECT  MedicalCategory_Mst.nMedicalCategoryID as nMedicalCategoryID, MedicalCategory_Mst.sMedicalCategory as sMedicalCategory FROM MedicalCategory_Mst INNER JOIN  MedicalCategoryOB ON MedicalCategory_Mst.nMedicalCategoryID = MedicalCategoryOB.nMedicalCategoryID order by sMedicalCategory "
            oDB.Retrive_Query(strQry, dtdata)
            oDB.Disconnect()
            If (IsNothing(dtdata) = False) Then
                ' If Only One Medical Category Present then Direcly set that to patient, instead of showing popup to select & Save
                If dtdata.Rows.Count = 1 Then
                    Dim dtMedCat As New DataTable()
                    Dim dcPatID As New DataColumn("nPatientID")
                    Dim dcMedCatID As New DataColumn("nMedicalCategoryId")
                    dtMedCat.Columns.Add(dcPatID)
                    dtMedCat.Columns.Add(dcMedCatID)

                    For i As Integer = 0 To dtdata.Rows.Count - 1
                        Dim dr As DataRow = dtMedCat.NewRow()
                        dr("nPatientID") = Convert.ToInt64(_PatientID)
                        dr("nMedicalCategoryId") = Convert.ToInt64(dtdata.Rows(0)("nMedicalCategoryID"))
                        dtMedCat.Rows.Add(dr)
                    Next
                    SaveMedicalCategory(dtMedCat)
                    SingleCategoryname = Convert.ToString(dtdata.Rows(0)("sMedicalCategory"))
                    If IsNothing(dtMedCat) = False Then
                        dtMedCat.Dispose()
                        dtMedCat = Nothing
                    End If
                End If
            End If
            Return SingleCategoryname
        Catch ex As Exception
            MessageBox.Show("Error on OB Vital." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return SingleCategoryname
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(dtdata) = False Then
                dtdata.Dispose()
                dtdata = Nothing
            End If
        End Try
    End Function


    'Validations

    'Private Sub txtTotalPregenancies_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalPregenancies.KeyPress
    '    'Allow only numeric and Not decimal point keys
    '    If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
    '        e.Handled = True
    '    End If
    'End Sub

    '19-Mar-15 Aniket: Resolving Bug #80450: Vital Screen Changes - Total Pregnancies,Living Aborted premauture etc. text box accetping other than numeric value.
    Private Sub txtFullTerm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFullTerm.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtPremature_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPremature.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    'Private Sub txtAbortedSpontanoues_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbortedSpontanoues.KeyPress
    '    'Allow only numeric and Not decimal point keys
    '    If Not ((e.KeyChar >= ChrW(48) And e.KeyChar <= ChrW(57)) Or (e.KeyChar = ChrW(8))) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub txtAbortedInduced_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbortedinduced.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEctopic_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEctopic.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMultipleBirth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMultipleBirth.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLiving_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLiving.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTotalPreganancy_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalPreganancy.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAbortedSpontaneous_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbortedSpontaneous.KeyPress
        'Allow only numeric and Not decimal point keys
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub

    
End Class

