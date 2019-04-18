Imports gloEMRGeneralLibrary
Imports System.Data.SqlClient
Imports gloPatientPortalCommon
Imports System.Linq
Public Class gloUC_PatientStrip

    Implements IDisposable

    Dim FormNo As Int64
    Dim CntRfr As Integer = 0 'Adden on 20100612-by sanjog for referrals2284
    Dim tooltipStr = ""
    Public PatID As Int64 = 0
    Public DishtblMedcatClr As Dictionary(Of String, String)
    Private intwdthofdtPicker As Integer = 0 '' added for Bugid #93863 
    Private intwdthofDischargedtPicker As Integer = 0 '' added for Bugid #93863 
#Region "private variables"

    Private ControlHeight As Integer = 96
    Private PatientDetailHeight As Integer = 64
    Dim _TimeSpan As TimeSpan = Nothing  ''[PEDETRIC SETTIINGS]   _BIRTH TIME  

    Private _Name As String
    Private _Code As String
    Private _Provider As String
    Private _ProviderID As Int64
    Private _Age As AgeDetail
    Private _DateOfBirth As Date
    Private _BirthTime As String  ''[PEDETRIC SETTIINGS] _BIRTH TIME  
    'Date of Service -- trans date
    Private _TransactionDate As Date = Now

    'sarika 22nd may 07
    Private _PatientPhone As String
    Private _PharmacyName As String
    Private _PharmacyFax As String
    Private _PharmacyPhone As String

    'sarika 24th may 07
    Private _PatientOccupation As String
    Private _Referral As String = ""
    Private _PCP As String
    Private _PatientCellPhone As String
    Private _Gender As String
    Private _HandDominance As String
    Private _PrimaryInsurance As String = ""
    Private _SecondaryInsurance As String = ""
    Private _SSN As String

    'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner
    'Reason: New variable for Property
    Private _GlobalPeriod As String

    Private _MedicalCategory As String
    Private _PMAlert As String  ''added for 8070  changes PM,EMR alert,allergies,nextappt
    Private _EMRAlert As String
    '  Private _Allergies As String
    ' Private _NextAppointment As String
    '-------------------------------------------------
    Private _HideButton As Boolean = False

    'sarika 23rd may 07
    Private _HidePatientPhone As Boolean = True

    Private _HidePharmacyPhone As Boolean = True
    Private _HidePharmacyName As Boolean = True
    Private _HidePharmacyFax As Boolean = True

    'sarika 24th may 07
    Private _HidePatientExam As Boolean = True
    Private _HidePatientOccupation As Boolean = True
    Private _HideReferral As Boolean = True
    Private _HidePCP As Boolean = True
    Private _HidePatientCellPhone As Boolean = True
    Private _HideGender As Boolean = True
    Private _HideHandDominance As Boolean = True
    Private _HidePrimaryInsurance As Boolean = True
    Private _HideSecondaryInsurance As Boolean = True
    Private _HideSSN As Boolean = True

    'Pranit 16 apr 2012
    Private _HideAddress As Boolean = True
    Private _HideAddress1 As Boolean = True
    Private _HideAddress2 As Boolean = True
    Private _HideAddress3 As Boolean = True

    Private _showState As Boolean = False
    Private _showCity As Boolean = False
    Private _showZip As Boolean = False

    ''Sanjog
    Private _HidePhoto As Boolean = True
    Private _HideGlobalPeriod As Boolean = True

    ''sudhir 20081126
    Private _MinimizeStrip As Boolean = False
    Private _ShowAgeInDays As Boolean = False
    Private _IntuitCommunication As Boolean = False


    Private _AgeLimit As Int64
    Private _IsPediatric As Boolean = False ''[PEDETRIC SETTIINGS] _BIRTH TIME  
    Dim dspatient As DataSet = Nothing

    Private _HideMedicalCategory As Boolean = True
    Private strCompMedCategory As String = String.Empty ''use to check previous medical category while saving for refreshing other forms
    Private _HidePMAlert As Boolean = True
    Private _HideEMRAlert As Boolean = True
    Private _HideNextAppointment As Boolean = True
    Private _HideAllergies As Boolean = True
    'Sanjog - Added on 2011 May 3 for patient safety changes
    'Private _myPictureBoxControl() As Byte = Nothing ''gloPicutreBoxControl
    'Private _iPhoto As System.Drawing.Image = Nothing

    'Dim img As System.Drawing.Image = Nothing

#End Region
    Dim nClientMachineID As Integer

    Public Event ControlSizeChanged()
    Public Event Date_Validated()
    Public Event Date_ValueChanged()
    Public Event Date_Validating()
    Public Event Date_DropDown()
    Public Event Date_CloseUp()
    Public Event RxProviderAssociation_Click()
    Public Event btnUpOrDownclick()
    Public RXDatetime As DateTime
    Public Shared hshPatData As New Hashtable
    '' Start Patient Portal
    Public Delegate Sub DelegateSendPortalPatientEducation()
    Public Event eventSendPortalPatientEducation As DelegateSendPortalPatientEducation

    Private IsSendToPortal As Boolean = False
    Public nPatientEducationID As Long = 0

    Private blnPatientPortalEnabled As Boolean = False
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    '' End Patient Portal
    Private oMedicalCategory As gloListControl.gloListControl
    Dim ofrmList As frmViewListControl = Nothing
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If Environment.OSVersion.Platform = PlatformID.Win32NT And Environment.OSVersion.Version.Major >= 6 Then
                cp.ExStyle = cp.ExStyle Or &H2000000
            End If
            Return cp
        End Get
    End Property

    Public Enum enumPatientInfo
        'Gender = 0
        PatientPhoto = 0
        HandDominance = 1
        Occupation = 2
        HomePhone = 3
        CellPhone = 4
        ''Modified by Mayuri:20100416-To fix case No:#0004773-Patient Details - Referring Phys. is pulling the PCP
        PCP = 5
        PharmacyName = 6
        PharmacyPhone = 7
        PharmacyFax = 8
        PrimaryInsurance = 9
        SecondaryInsurance = 10
        SSN = 11
        ''Adden on 20100612-by sanjog for referrals
        Referrals = 12
        ''Added on 20120416- by Pranit
        PharmacyAddress = 13
        'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: New Enum for Global Period
        GlobalPeriodInEffect = 14
        MedicalCategory = 15
        PMAlert = 16
        EMRAlert = 17

        Allergies = 18
        NextAppointment = 19
    End Enum

    Public Enum enumFormName
        None = 0
        Prescription = 1
        PatientExam = 2
        'Medication = 3
        LabOrder = 3
        History = 4
        ROS = 5
        PatientEducation = 6
        PatientLetter = 7
        Referrals = 8
        ReferralLetter = 9
        PTProtocol = 10
        FormGallery = 11
        PatientConsent = 12
        PatientGuideline = 13
        DMTemplate = 14
        PatientVitals = 15
        VitalGraph = 16
        FlowSheet = 17
        Immunization = 18
        HealthPlan = 19
        PatientMessages = 20
        SummaryOfVisit = 21
        PatientSummary = 22
        NurseNotes = 23
        DisclosureManagement = 24
        'Code Added by Mayuri:20090918 To add Triage and ScanDocuments Module in Patient control customization form
        Triage = 25
        ScanDocuments = 26
        DICOM = 27
        ChiefComplaint = 28
        PatientConfidentialInformation = 29
        Timeline = 30
        ClinicalChart = 31
        Intuit = 32
        CCD = 33
        MedicationHistory = 34
        PatientConsentTracking = 35
        OBPlan = 36
        ImplantableDevices = 37
        PHI = 38
        RxFillNotifications = 39
        SummaryCareRecord = 40 ''Enum Added For Summary Care Record form
        Fax = 41
    End Enum

    'Sandip Darade 20091029
    Public IsSizeMaximized As Boolean

#Region "Properties"

    'PEDETRIC SETTIINGS 
    Public Property IsPediatric() As Boolean
        Get
            Return _IsPediatric
        End Get
        Set(ByVal value As Boolean)
            _IsPediatric = value
        End Set
    End Property
    Private _IsEnableCQMCypressTesting As Boolean = True
    Public Property IsEnableCQMCypressTesting() As Boolean
        Get
            Return _IsEnableCQMCypressTesting
        End Get
        Set(ByVal value As Boolean)
            _IsEnableCQMCypressTesting = value
        End Set
    End Property
    Public Property DTPDischargeEnabled() As Boolean
        Get
            Return dtpDischarge.Enabled
        End Get
        Set(ByVal value As Boolean)
            dtpDischarge.Enabled = value
            dtadmitdate.Enabled = value
            If (dtpDischarge.Enabled = False) Then  '' added for Bugid #93863 
                If (intwdthofDischargedtPicker > 0) Then
                    If (dtpDischarge.Width > intwdthofDischargedtPicker) Then
                        dtpDischarge.Width = intwdthofDischargedtPicker - 20
                        dtadmitdate.Width = intwdthofDischargedtPicker - 20
                    End If
                End If
            End If
        End Set
    End Property

    Public Property DTPEnabled() As Boolean
        Get
            Return dtpDate.Enabled
        End Get
        Set(ByVal value As Boolean)
            dtpDate.Enabled = value
            lbldt.Visible = Not value
            If _IsEnableCQMCypressTesting Then
                lbldt.Text = Convert.ToString(dtpDate.Value.ToString("MM/dd/yyyy hh:mm:ss tt"))
            Else
                lbldt.Text = Convert.ToString(dtpDate.Value.ToString("MM/dd/yyyy"))
            End If

            lbldt.BackColor = Color.Transparent
            If (dtpDate.Enabled = False) Then  '' added for Bugid #93863 
                If (intwdthofdtPicker > 0) Then
                    If (dtpDate.Width > intwdthofdtPicker) Then
                        dtpDate.Width = intwdthofdtPicker - 32
                    End If
                End If
            End If
        End Set
    End Property
    Public Property dtpDischargeValue() As DateTime
        Get
            Return dtpDischarge.Value
        End Get
        Set(ByVal value As DateTime)
            dtpDischarge.Value = value
            If (intwdthofDischargedtPicker = 0) Then
                intwdthofDischargedtPicker = dtpDischarge.Width
            End If
            If (dtpDischarge.Enabled = True) Then  '' added for Bugid #93863 
                'If (Convert.ToString(value).Contains(":")) Then
                dtpDischarge.Width = intwdthofDischargedtPicker
                'Else
                '    dtpDischarge.Width = intwdthofDischargedtPicker
                'End If
            End If
        End Set
    End Property

    Public Property dtpadmitValue() As DateTime
        Get
            Return dtadmitdate.Value
        End Get
        Set(ByVal value As DateTime)
            dtadmitdate.Value = value
            'If (intwdthofDischargedtPicker = 0) Then
            '    intwdthofDischargedtPicker = dtadmitdate.Width
            'End If
            '  dtadmitdate.Width
            '  If (dtadmitdate.Enabled = True) Then  '' added for Bugid #93863 
            If (Convert.ToString(value).Contains(":")) Then
                dtadmitdate.Width = intwdthofDischargedtPicker + 15
            Else
                dtadmitdate.Width = intwdthofDischargedtPicker
            End If
            '  Else
            'dtadmitdate.Width = intwdthofDischargedtPicker
            ' End If
        End Set
    End Property

    Public Property DTPValue() As DateTime
        Get
            Return dtpDate.Value
        End Get
        Set(ByVal value As DateTime)
            dtpDate.Value = value
            If (intwdthofdtPicker = 0) Then
                intwdthofdtPicker = dtpDate.Width
            End If
            If (dtpDate.Enabled = True) Then  '' added for Bugid #93863 
                If (Convert.ToString(value).Contains(":")) Then
                    dtpDate.Width = intwdthofdtPicker + 32
                Else
                    dtpDate.Width = intwdthofdtPicker
                End If
            End If
        End Set
    End Property

    'SUDHIR 20100113 'TO FORMAT DATETIMEPICKET'
    Public Property DTPFormat() As DateTimePickerFormat
        Get
            Return dtpDate.Format
        End Get
        Set(ByVal value As DateTimePickerFormat)
            dtpDate.Format = value
        End Set
    End Property

    Public Property btnUpEnable() As Boolean
        Get
            Return btnUp.Enabled
        End Get
        Set(ByVal value As Boolean)
            btnUp.Enabled = value
        End Set
    End Property

    Public Property btnDownEnable() As Boolean
        Get
            Return btnDown.Enabled
        End Get
        Set(ByVal value As Boolean)
            btnDown.Enabled = value
        End Set
    End Property

    Public Property DTP() As DateTimePicker
        Get
            Return dtpDate
        End Get
        Set(ByVal value As DateTimePicker)
            If (IsNothing(dtpDate) = False) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate)
                Catch
                End Try
                dtpDate.Dispose()
                dtpDate = Nothing
            End If
            dtpDate = value
        End Set
    End Property

    Public Property DTPDischargeDate() As DateTimePicker
        Get
            Return DTPDischarge
        End Get
        Set(ByVal value As DateTimePicker)
            If (IsNothing(dtpDischarge) = False) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDischarge)
                Catch
                End Try
                dtpDischarge.Dispose()
                dtpDischarge = Nothing
            End If
            dtpDischarge = value
        End Set
    End Property

    Public Property PatientName() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property PatientCode() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Public Property Provider() As String
        Get
            Return _Provider
        End Get
        Set(ByVal value As String)
            _Provider = value
        End Set
    End Property
    Public Property ProviderID() As Int64
        Get
            Return _ProviderID
        End Get
        Set(ByVal value As Int64)
            _ProviderID = value
        End Set
    End Property

    Public Property PatientAge() As AgeDetail
        Get
            Return _Age
        End Get
        Set(ByVal value As AgeDetail)
            _Age = value
        End Set
    End Property

    Public Property PatientDateOfBirth() As Date
        Get
            Return _DateOfBirth
        End Get
        Set(ByVal value As Date)
            _DateOfBirth = value
        End Set
    End Property

    Public Property TransactionDate() As Date
        Get
            _TransactionDate = dtpDate.Value
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            If IsDate(value) = False Then
                value = Date.Now
            End If

            Try
                '00000686 : Order's not showing up in Lab order's tab
                'when user changes system datetime formats then it does not matches as per below criteria 
                'and current datetime will not assign to datetime control and occurs error like System.ArgumentoutofRangecxception.

                If value.Day = 1 And value.Month = 1 And value.Year = 1 Then
                    value = Date.Now
                End If
            Catch ex As Exception
                value = Date.Now
            End Try

            _TransactionDate = value
            dtpDate.Value = _TransactionDate
        End Set
    End Property

    Public Property PatientPhone() As String
        Get
            Return _PatientPhone
        End Get
        Set(ByVal value As String)
            _PatientPhone = value
        End Set
    End Property

    Public Property PharmacyName() As String
        Get
            Return _PharmacyName
        End Get
        Set(ByVal value As String)
            _PharmacyName = value
        End Set
    End Property

    Public Property PharmacyFax() As String
        Get
            Return _PharmacyFax
        End Get
        Set(ByVal value As String)
            _PharmacyFax = value
        End Set
    End Property

    Public Property PharmacyPhone() As String
        Get
            Return _PharmacyPhone
        End Get
        Set(ByVal value As String)
            _PharmacyPhone = value
        End Set
    End Property

    Public Property PatientOccupation() As String
        Get
            Return _PatientOccupation
        End Get
        Set(ByVal value As String)
            _PatientOccupation = value
        End Set
    End Property

    Public Property Referral() As String
        Get
            Return _Referral
        End Get
        Set(ByVal value As String)
            _Referral = value
        End Set
    End Property

    Public Property PCP() As String
        Get
            Return _PCP
        End Get
        Set(ByVal value As String)
            _PCP = value
        End Set
    End Property

    Public Property PatientCellPhone() As String
        Get
            Return _PatientCellPhone
        End Get
        Set(ByVal value As String)
            _PatientCellPhone = value
        End Set
    End Property

    Public Property PatientGender() As String
        Get
            Return _Gender
        End Get
        Set(ByVal value As String)
            _Gender = value
        End Set
    End Property

    Public Property PatientHandDominance() As String
        Get
            Return _HandDominance
        End Get
        Set(ByVal value As String)
            _HandDominance = value
        End Set
    End Property

    Public Property PrimaryInsurance() As String
        Get
            Return _PrimaryInsurance
        End Get
        Set(ByVal value As String)
            _PrimaryInsurance = value
        End Set
    End Property

    Public Property SecondaryInsurance() As String
        Get
            Return _SecondaryInsurance
        End Get
        Set(ByVal value As String)
            _SecondaryInsurance = value
        End Set
    End Property

    Public Property SSN() As String
        Get
            Return _SSN
        End Get
        Set(ByVal value As String)
            _SSN = value
        End Set
    End Property

    'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: New Property for Global Period
    Public Property GlobalPeriod() As String
        Get
            Return _GlobalPeriod
        End Get
        Set(ByVal value As String)
            _GlobalPeriod = value
        End Set
    End Property

    Public Property HideButton() As Boolean
        Get
            Return _HideButton
        End Get
        Set(ByVal value As Boolean)
            _HideButton = value
            If _HideButton = False Then
                btnUp.Visible = True
                btnDown.Visible = False
                pnlPatientDetail.Visible = True
                'Size
                On Error Resume Next
                pnlPatientDetail.Height = PatientDetailHeight  ''64
                Me.Height = ControlHeight  ''96
            Else
                btnUp.Visible = False
                btnDown.Visible = False
                pnlPatientDetail.Visible = True
            End If
        End Set
    End Property

    Public Property HidePatientPhone() As Boolean
        'sarika 23rd may 07 Hide Patient Phone Panel
        Get
            Return _HidePatientPhone
        End Get
        Set(ByVal value As Boolean)
            _HidePatientPhone = value
            If _HidePatientPhone = False Then
                pnlPatientHomePhone.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPatientHomePhone.Visible = False
            End If
        End Set
    End Property

    Public Property HidePharmacyPhone() As Boolean
        'Hide Pharmacy Phone
        Get
            Return _HidePharmacyPhone
        End Get
        Set(ByVal value As Boolean)
            _HidePharmacyPhone = value
            If _HidePharmacyPhone = False Then
                pnlPharmacyPhone.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPharmacyPhone.Visible = False
            End If
        End Set
    End Property

    'Hide Pharmacy Name
    Public Property HidePharmacyName() As Boolean
        Get
            Return _HidePharmacyName
        End Get
        Set(ByVal value As Boolean)
            _HidePharmacyName = value
            If _HidePharmacyName = False Then
                pnlPharmacyName.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPharmacyName.Visible = False
            End If
        End Set
    End Property

    'Hide Pharmacy Fax
    Public Property HidePharmacyFax() As Boolean
        Get
            Return _HidePharmacyFax
        End Get
        Set(ByVal value As Boolean)
            _HidePharmacyFax = value
            If _HidePharmacyFax = False Then
                pnlPharmacyFax.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPharmacyFax.Visible = False
            End If
        End Set
    End Property

    Public Property HidePatientOccupation() As Boolean
        'sarika 24th may 07 hide Patient Occupation panel
        Get
            Return _HidePatientOccupation
        End Get
        Set(ByVal value As Boolean)
            _HidePatientOccupation = value
            If _HidePatientOccupation = False Then
                pnlOccupation.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlOccupation.Visible = False
            End If
        End Set
    End Property

    Public Property HideReferral() As Boolean
        'hide Referral panel
        Get
            Return _HideReferral
        End Get
        Set(ByVal value As Boolean)
            _HideReferral = value
            If _HideReferral = False Then
                pnlRefPhysician.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlRefPhysician.Visible = False
            End If
        End Set
    End Property

    Public Property HidePatientCellPhone() As Boolean
        'hide Patient Cell Phone panel
        Get
            Return _HidePatientCellPhone
        End Get
        Set(ByVal value As Boolean)
            _HidePatientCellPhone = value
            If _HidePatientCellPhone = False Then
                pnlPatientCellPhone.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPatientCellPhone.Visible = False
            End If
        End Set
    End Property

    Public Property HideGender() As Boolean
        'hide Patient Gender panel
        Get
            Return _HideGender
        End Get
        Set(ByVal value As Boolean)
            _HideGender = value
            If _HideGender = False Then
                On Error Resume Next
                'pnlReferral.Height = 64
                'Me.Height = 96
            Else
                'pnlGender.Visible = False
            End If
        End Set
    End Property

    Public Property HideHandDominance() As Boolean
        Get
            Return _HideHandDominance
        End Get
        Set(ByVal value As Boolean)
            _HideHandDominance = value
            If _HideHandDominance = False Then
                pnlHandDominance.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlHandDominance.Visible = False
            End If
        End Set
    End Property

    Public Property HidePrimaryInsurance() As Boolean
        Get
            Return _HidePrimaryInsurance
        End Get
        Set(ByVal value As Boolean)
            _HidePrimaryInsurance = value
            If _HidePrimaryInsurance = False Then
                pnlPrimaryInsurance.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlPrimaryInsurance.Visible = False
            End If
        End Set
    End Property

    Public Property HideSecondaryInsurance() As Boolean
        Get
            Return _HideSecondaryInsurance
        End Get
        Set(ByVal value As Boolean)
            _HideSecondaryInsurance = value
            If _HideSecondaryInsurance = False Then
                pnlSecondaryInsurance.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlSecondaryInsurance.Visible = False
            End If
        End Set
    End Property

    Public Property HideSSN() As Boolean
        Get
            Return _HideSSN
        End Get
        Set(ByVal value As Boolean)
            _HideSSN = value
            If _HideSSN = False Then
                pnlSSN.Visible = True
                'Size
                On Error Resume Next
            Else
                pnlSSN.Visible = False
            End If
        End Set
    End Property

    Public Property MinimizeStrip() As Boolean
        ''sudhir 20081126
        Get
            Return _MinimizeStrip
        End Get
        Set(ByVal value As Boolean)
            _MinimizeStrip = value
            If value = True Then
                btnUp.PerformClick()  'It will Hide Strip when true
                'Sanjog - Commented on 2011 June 14 to show the new button image
                'btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
                'btnDown.BackgroundImageLayout = ImageLayout.Center
                'Sanjog - Commented on 2011 June 14 to show the new button image
            End If
        End Set
    End Property

    Public Property ShowAgeInDays() As Boolean
        Get
            Return _ShowAgeInDays
        End Get
        Set(ByVal value As Boolean)
            _ShowAgeInDays = value
        End Set
    End Property

    Public Property AgeLimit() As Integer
        Get
            Return _AgeLimit
        End Get
        Set(ByVal value As Integer)
            _AgeLimit = value
        End Set
    End Property

    Public WriteOnly Property ShowRxProviderAssociation() As Boolean
        Set(ByVal value As Boolean)
            btnRxProvider.Visible = value
        End Set
    End Property

    Public Property IntuitCommunication() As Boolean
        Get
            Return _IntuitCommunication
        End Get
        Set(ByVal value As Boolean)
            _IntuitCommunication = value
        End Set
    End Property

#End Region
    Public Sub ShowAdmitDate()
        pnladmitdate.Visible = True
        'If _IsEnableCQMCypressTesting Then
        '    dtadmitdate.Format = "MM/dd/yyyy hh:mm:ss tt"
        'Else
        '    dtadmitdate.Format = "MM/dd/yyyy"
        'End If
    End Sub

    Public Function FormatAge(ByVal BirthDate As DateTime) As AgeDetail
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0

        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 And BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = Now.Year Then
            months = Now.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + Now.Month
        End If
        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 And Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 And Now.Year Mod 4 <> 0 Then
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
        Dim age As New AgeDetail
        'age.Age = years & " Years " & months & " Months " & days & " Days"
        '' Cases

        ''20081119   ''Following Code to Store ExactAge in String
        Dim _AgeStr As String = ""
        If _ShowAgeInDays = True And _AgeLimit >= DateDiff(DateInterval.Day, CType(_DateOfBirth, Date), Date.Now.Date) Then
            If years = 0 Then
                If months = 0 Then
                    If days <= 1 Then
                        _AgeStr = days & " Day"
                    Else
                        _AgeStr = days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Month " & days & " Day"
                    Else
                        _AgeStr = months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Months " & days & " Day"
                    Else
                        _AgeStr = months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Year "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Month "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Months "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Years "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Months " & days & " Days"
                    End If
                End If
            End If
        Else 'ShowAgeInDay is False OR AgeLimit less than Settings.
            If years = 0 Then
                'Added by pravin on 11/25/2008
                '                If months = 0 And months = 1 Then
                If months = 1 Then
                    _AgeStr = months & " Month"
                ElseIf months > 1 Then
                    _AgeStr = months & " Months"
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    _AgeStr = years & " Year "
                ElseIf months = 1 Then
                    _AgeStr = years & " Year " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Year " & months & " Months "
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    _AgeStr = years & " Years "
                ElseIf months = 1 Then
                    _AgeStr = years & " Years " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Years " & months & " Months "
                End If
            End If
            'Added by pravin if age in days  11/25/2008
            If years = 0 And months = 0 Then
                If days <= 1 Then
                    _AgeStr = days & " Day"
                Else
                    _AgeStr = days & " Days"


                End If
            End If
        End If
        age.Age = _AgeStr
        age.Years = years
        age.Months = months
        age.Days = days
        Return age
    End Function
    Public Sub ShowDetail(ByVal PatientID As Int64, Optional ByVal CallingFormName As enumFormName = enumFormName.None, Optional ByVal ExamID As Int64 = 0, Optional ByVal VisitID As Int64 = 0, Optional ByVal ProviderId As Int64 = 0, Optional ByVal blnflag As Boolean = False, Optional ByVal blnRxTranMode As Boolean = False, Optional ByVal blnIsProviderequal As Boolean = False, Optional ByVal Rxvalue As String = "", Optional ByVal IsPharmacyEnabled As Boolean = False, Optional ByVal IsSendEducationMaterial As Boolean = False)
        PatID = PatientID


        'Dim oDB As New gloEMRDatabase.DataBaseLayer
        Dim oDataTable As DataTable = Nothing
        Dim _strAge As String = ""
        Try
            If (IsNothing(dspatient) = False) Then
                dspatient.Dispose()
                dspatient = Nothing
            End If
            dspatient = FillPatientStripDetails(PatientID, CallingFormName, ExamID, VisitID, ProviderId, blnflag, blnRxTranMode, blnIsProviderequal, Rxvalue, IsPharmacyEnabled)
            'SLR: not needed since it is already nothing
            'If IsNothing(oDataTable) = False Then
            '    oDataTable.Dispose()
            '    oDataTable = Nothing
            'End If

            GetSetting() 'to fetch ShowAgeInDays flag
            GetPediatricSetting() 'Get Pediatric Clinic flag
            If IsNothing(dspatient) = False Then
                If dspatient.Tables("Module").Rows.Count > 0 Then
                    oDataTable = dspatient.Tables(0)
                End If
            End If

            If IsSendEducationMaterial = True Then
                IsSendToPortal = True
            End If
            If Not oDataTable Is Nothing Then
                If oDataTable.Rows.Count > 0 Then
                    For i As Integer = 0 To oDataTable.Rows.Count - 1
                        Select Case oDataTable.Rows(i)("sInfo")
                            Case enumPatientInfo.CellPhone
                                _HidePatientCellPhone = False
                            Case enumPatientInfo.PatientPhoto
                                _HidePhoto = False
                            Case enumPatientInfo.HandDominance
                                _HideHandDominance = False
                            Case enumPatientInfo.HomePhone
                                _HidePatientPhone = False
                            Case enumPatientInfo.Occupation
                                _HidePatientOccupation = False
                            Case enumPatientInfo.PharmacyFax
                                _HidePharmacyFax = False
                            Case enumPatientInfo.PharmacyName
                                _HidePharmacyName = False
                            Case enumPatientInfo.PharmacyPhone
                                _HidePharmacyPhone = False
                            Case enumPatientInfo.PrimaryInsurance
                                _HidePrimaryInsurance = False
                            Case enumPatientInfo.PCP
                                _HidePCP = False
                            Case enumPatientInfo.Referrals
                                _HideReferral = False
                            Case enumPatientInfo.SecondaryInsurance
                                _HideSecondaryInsurance = False
                            Case enumPatientInfo.SSN
                                _HideSSN = False
                            Case enumPatientInfo.PharmacyAddress
                                _HideAddress = False
                                'Developer: Sanjog Dhamke
                                'Date: 17 March 2012
                                'Bug PRD Name: Global Period on Patient Banner
                                'Reason: new case for Global Period
                            Case enumPatientInfo.GlobalPeriodInEffect
                                _HideGlobalPeriod = False
                            Case enumPatientInfo.MedicalCategory
                                _HideMedicalCategory = False
                            Case enumPatientInfo.PMAlert
                                _HidePMAlert = False
                            Case enumPatientInfo.EMRAlert
                                _HideEMRAlert = False

                            Case enumPatientInfo.Allergies
                                _HideAllergies = False
                            Case enumPatientInfo.NextAppointment
                                _HideNextAppointment = False
                        End Select
                    Next
                End If
            End If
            'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
            'If IsNothing(oDataTable) = False Then
            '    oDataTable.Dispose()
            '    oDataTable = Nothing
            'End If
            oDataTable = Nothing
            'By Pranit on 16 apr 2012

            Dim txtPharmacyTemp As String = ""
            Dim txtState As String = ""
            Dim txtCity As String = ""
            Dim txtZip As String = ""
            Dim txtAdd1 As String = ""
            Dim txtAdd2 As String = ""
            Dim txtCombineAdd As String = ""

            If _HideAddress = False Then
                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PharmacyAddress1").Rows.Count > 0 Then
                        txtAdd1 = dspatient.Tables("PharmacyAddress1").Rows(0)(0).ToString()
                    End If
                End If

                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PharmacyAddress2").Rows.Count > 0 Then
                        txtAdd2 = dspatient.Tables("PharmacyAddress2").Rows(0)(0).ToString()
                    End If
                End If

                If txtAdd1 <> "" And txtAdd2 <> "" Then
                    txtCombineAdd = txtAdd1 & ", " & txtAdd2
                ElseIf txtAdd1 <> "" And txtAdd2 = "" Then
                    txtCombineAdd = txtAdd1
                ElseIf txtAdd1 = "" And txtAdd2 <= "" Then
                    txtCombineAdd = txtAdd2
                End If

                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PharmaState").Rows.Count > 0 Then
                        txtState = dspatient.Tables("PharmaState").Rows(0)(0).ToString()
                    End If
                    If dspatient.Tables("PharmaCity").Rows.Count > 0 Then
                        txtCity = dspatient.Tables("PharmaCity").Rows(0)(0).ToString()
                    End If
                    If dspatient.Tables("PharmaZip").Rows.Count > 0 Then
                        txtZip = dspatient.Tables("PharmaZip").Rows(0)(0).ToString()
                    End If
                End If

                If txtCity <> "" And txtState <> "" And txtZip <> "" Then
                    txtPharmacyTemp = txtCity & ", " & txtState & ", " & txtZip
                ElseIf txtCity <> "" And txtState <> "" And txtZip = "" Then
                    txtPharmacyTemp = txtCity & ", " & txtState
                ElseIf txtCity <> "" And txtState = "" And txtZip <> "" Then
                    txtPharmacyTemp = txtCity & ", " & txtZip
                ElseIf txtCity <> "" And txtState = "" And txtZip = "" Then
                    txtPharmacyTemp = txtCity
                ElseIf txtCity = "" And txtState <> "" And txtZip <> "" Then
                    txtPharmacyTemp = txtState & ", " & txtZip
                ElseIf txtCity = "" And txtState <> "" And txtZip = "" Then
                    txtPharmacyTemp = txtState
                ElseIf txtCity = "" And txtState = "" And txtZip <> "" Then
                    txtPharmacyTemp = txtZip
                End If


                If txtCombineAdd <> "" And txtPharmacyTemp <> "" Then
                    lblPharmacyAdd.Text = txtCombineAdd & ", " & txtPharmacyTemp
                ElseIf txtCombineAdd <> "" And txtPharmacyTemp = "" Then
                    lblPharmacyAdd.Text = txtCombineAdd
                ElseIf txtCombineAdd = "" And txtPharmacyTemp <= "" Then
                    lblPharmacyAdd.Text = txtPharmacyTemp
                End If

            End If
            'End By Pranit on 16 apr 2012

            If _HidePCP = False Then
                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PCP").Rows.Count > 0 Then
                        _PCP = dspatient.Tables("PCP").Rows(0)("PCP")
                        lblRefPhysicianCap.Text = "PCP :"
                    End If
                End If
            End If

            If _HideReferral = False Then
                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("ReffName").Rows.Count > 0 Then
                        _Referral = Convert.ToString(dspatient.Tables("ReffName").Rows(0)("ReffName"))
                    End If
                End If
            End If

            'get the Provider name 
            If IsNothing(dspatient) = False Then
                _Provider = ""
                If dspatient.Tables("Provider").Rows.Count > 0 Then
                    _Provider = dspatient.Tables("Provider").Rows(0)("sProviderName")
                    _ProviderID = dspatient.Tables("Provider").Rows(0)("ProviderID")
                End If
            End If

            If CallingFormName = enumFormName.Prescription Then
                Get_PatPharmProv_Details(PatientID, ProviderId, Rxvalue, blnRxTranMode, CallingFormName)
                If IsPharmacyEnabled = True Then
                    lblPhName.Image = Global.gloUserControlLibrary.My.Resources.ePharmacy2
                    lblPhName.ImageAlign = ContentAlignment.MiddleLeft
                Else
                    lblPhName.Image = Nothing
                End If
            Else
                If IsNothing(dspatient) = False Then
                    'changes done for bug 11802
                    If dspatient.Tables("PatientInfo").Rows.Count > 0 Then
                        'oDataTable = Nothing
                        oDataTable = dspatient.Tables("PatientInfo")
                        _Code = oDataTable.Rows(0).Item("sPatientCode") & ""
                        _Name = oDataTable.Rows(0).Item("PatientName") & ""

                        If Not IsDBNull(oDataTable.Rows(0).Item("nSSN")) Then
                            _SSN = oDataTable.Rows(0).Item("nSSN") & ""
                        Else
                            _SSN = ""
                        End If

                        _PatientPhone = oDataTable.Rows(0).Item("sPhone") & ""
                        _PatientCellPhone = oDataTable.Rows(0).Item("sMobile") & ""
                        _PatientOccupation = oDataTable.Rows(0).Item("sOccupation") & ""
                        _Gender = oDataTable.Rows(0).Item("sGender") & ""
                        _HandDominance = oDataTable.Rows(0).Item("sHandDominance") & ""

                        _DateOfBirth = oDataTable.Rows(0).Item("dtDOB")
                        _BirthTime = oDataTable.Rows(0).Item("sBirthTime")
                        _strAge = oDataTable.Rows(0).Item("Age") & ""

                        If _Age Is Nothing Then
                            _Age = New AgeDetail
                        End If

                        If IsPediatric = False Then
                            _Age = FormatAge(_DateOfBirth)
                        Else
                            _TimeSpan = GetAgeInHrs(_DateOfBirth, _BirthTime)
                            If Not IsNothing(_TimeSpan) Then
                                If _TimeSpan.TotalDays < 4 Then
                                    _Age.Hours = _TimeSpan.TotalHours
                                    _Age.Age = _TimeSpan.TotalHours.ToString("0") & " Hours"
                                    ''''Hours
                                ElseIf _TimeSpan.TotalDays > 4 And (_TimeSpan.TotalDays <= 28 Or _TimeSpan.Hours = 0) Then
                                    _ShowAgeInDays = True
                                    _Age = FormatAge(_DateOfBirth)
                                    ''''days
                                ElseIf _TimeSpan.TotalDays > 28 And _TimeSpan.TotalDays <= 90 Then
                                    _Age.Age = (_TimeSpan.Days / 7).ToString("0") & " Weeks"
                                    '''' weeks
                                Else
                                    _Age = FormatAge(_DateOfBirth)
                                    If _Age.Years < 2 And _Age.Months >= 0 Then
                                        _Age.Age = (_Age.Years * 12) + _Age.Months & " Months"
                                    End If
                                End If

                            End If
                        End If

                        If CallingFormName <> enumFormName.Prescription Then
                            _Provider = ""
                            _Provider = oDataTable.Rows(0).Item("ProviderName") & ""
                            If Val(oDataTable.Rows(0).Item("ProviderID")) <> 0 Then
                                _ProviderID = oDataTable.Rows(0).Item("ProviderID")
                            Else
                                _ProviderID = 0
                            End If
                        End If
                    End If
                    If IsNothing(dspatient) = False Then
                        If dspatient.Tables("PharmaName").Rows.Count > 0 Then
                            _PharmacyName = dspatient.Tables("PharmaName").Rows(0)(0)
                        End If

                        If dspatient.Tables("PharmaPhone").Rows.Count > 0 Then
                            _PharmacyPhone = dspatient.Tables("PharmaPhone").Rows(0)(0)
                        End If
                        If dspatient.Tables("PharmaFax").Rows.Count > 0 Then
                            _PharmacyFax = dspatient.Tables("PharmaFax").Rows(0)(0)
                        End If
                    End If


                End If
            End If


            If IsNothing(dspatient) = False Then
                If dspatient.Tables("MedicalCategory").Rows.Count > 0 Then
                    _MedicalCategory = Convert.ToString(dspatient.Tables("MedicalCategory").Rows(0)(0))
                End If
                If dspatient.Tables("PMAlert").Rows.Count > 0 Then ''added for 8070  changes PM,EMR alert,allergies,nextappt
                    If (dspatient.Tables("PMAlert").Columns.Count > 1) Then
                        _PMAlert = Convert.ToString(dspatient.Tables("PMAlert").Rows(0)("sAlertName"))
                        MaxColumnPos = 2
                        lbl_Pmalrt.Text = _PMAlert
                        '  lbl_NxtAppt.Text = Convert.ToString(dspatient.Tables("PMAlert").Rows(0)("NextAppointment"))
                        Dim dTotalPatientDue As Decimal = 0
                        Dim dTotalBalance As Decimal = 0
                        Dim dPatCopay As Decimal = 0
                        Dim dBadDebt As Decimal = 0
                        Dim strCopay As String = Convert.ToString(dspatient.Tables("PMAlert").Rows(0)("nCopay"))
                        Dim strcopaybal As String = String.Empty
                        If (strCopay.Contains(",")) Then
                            Dim strsplcopay As String() = strCopay.Split(",")
                            For Len As Integer = 0 To strsplcopay.Length - 1
                                dPatCopay = Convert.ToDecimal(strsplcopay(Len).Trim())
                                If Len = 0 Then
                                    strcopaybal = dPatCopay.ToString("N2")
                                Else
                                    strcopaybal = strcopaybal & ", $ " + dPatCopay.ToString("N2")
                                End If

                            Next
                            strCopay = strcopaybal
                        End If
                        If strCopay.Trim() = "" Then
                            strCopay = "0.00"
                        End If
                        dTotalPatientDue = dspatient.Tables("PMAlert").Rows(0)("PatientBalance")
                        lbl_PatBal.Text = "$ " + dTotalPatientDue.ToString("#0.00")
                        dTotalBalance = dspatient.Tables("PMAlert").Rows(0)("TotalBalance")
                        lbl_TotBal.Text = "$ " + dTotalBalance.ToString("N2")
                        lbl_Copay.Text = "$ " + strCopay
                        dBadDebt = dspatient.Tables("PMAlert").Rows(0)("BadDebt")
                        lblBadDebt.Text = "$ " + dBadDebt.ToString("N2")
                        ' lbl_Allergies.Text = Convert.ToString(dspatient.Tables("PMAlert").Rows(0)("Allergies"))

                        If dspatient.Tables.Contains("IsBadDebt") AndAlso dspatient.Tables("IsBadDebt").Rows.Count > 0 Then
                            lblBadDebtStatus.Visible = True
                        Else
                            lblBadDebtStatus.Visible = False
                        End If
                        Dim cntPMAlert As Integer = 0
                        cntPMAlert = Convert.ToInt32(dspatient.Tables("PMAlert").Rows(0)("CountPMAlert"))
                        If cntPMAlert > 1 Then
                            Label19.Text = "PM Alerts (" + cntPMAlert.ToString() + ") :"
                        End If
                    End If
                End If
                If dspatient.Tables("EMRAlert").Rows.Count > 0 Then

                    _EMRAlert = Convert.ToString(dspatient.Tables("EMRAlert").Rows(0)(0))
                    lbl_Emralrt.Text = _EMRAlert
                End If
                If dspatient.Tables("Allergies").Rows.Count > 0 Then

                    lbl_Allergies.Text = Convert.ToString(dspatient.Tables("Allergies").Rows(0)(0))
                End If
                If dspatient.Tables("NextAppointment").Rows.Count > 0 Then

                    lbl_NxtAppt.Text = Convert.ToString(dspatient.Tables("NextAppointment").Rows(0)(0))
                End If
            End If

            'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: set DB value to variable
            If dspatient.Tables("GlobalPeriod").Rows.Count > 0 Then
                _GlobalPeriod = dspatient.Tables("GlobalPeriod").Rows(0).Item("sDuration")
            Else
                _GlobalPeriod = ""
            End If

            _TransactionDate = dtpDate.Value
            _HideButton = _HideButton

            lblPtName.Text = _Name
            lblPtCode.Text = _Code
            lblPtBorn.Text = _DateOfBirth
            lblPatGender.Text = _Gender
            lblProvider.Text = _Provider
            lblAgeString_New.Text = "(" & _Age.Age & ")"
            lblPhone.Text = _PatientPhone
            If IsNothing(lblPhName.Image) = False Then
                _PharmacyName = Space(6) & _PharmacyName
            End If
            lblPhName.Text = _PharmacyName
            lblPhPhone.Text = _PharmacyPhone
            lblPhFax.Text = _PharmacyFax
            lblOccupation.Text = _PatientOccupation
            lblRefPhysician.Text = _PCP
            lblReferrals.Text = _Referral
            lblPatientCellPhone.Text = _PatientCellPhone
            lblHandDominance.Text = _HandDominance
            lblMedicalCategory.Text = _MedicalCategory
            strCompMedCategory = lblMedicalCategory.Text
            '' lnkMedicalCategory.Text = _MedicalCategory
            If Not IsNothing(_SSN) Then
                lblSSN.Text = gloGlobal.gloGenral.MaskSSN(_SSN)
            Else
                lblSSN.Text = ""
            End If

            'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: set string to label
            lblLastGlobalPeriod.Text = "Global Period in Effect : " + _GlobalPeriod

            If _HideButton = False Then
                btnUp.Visible = True
                btnDown.Visible = False
                pnlPatientDetail.Visible = True
                'Size
                pnlPatientDetail.Height = PatientDetailHeight '' 64
                Me.Height = ControlHeight  ''96
            Else
                btnUp.Visible = False
                btnDown.Visible = False
                pnlPatientDetail.Visible = True
            End If

            Dim _countRows As Integer = 1
            Dim _countColumnPosition As Integer = 0
            TableLayout.Controls.Clear()

            TableLayout.Controls.Add(pnlProvider, 0, 0)
            TableLayout.Controls.Add(pnlTransactionDate, 1, 0)
            'If CallingFormName = enumFormName.CCD Then
            '    dtpDate.Visible = False
            'End If

            'hide patient Hand Dominance panel
            If _HideHandDominance = False Then
                pnlHandDominance.Visible = True
                SetRowAndColumnPosition(pnlHandDominance, _countRows, _countColumnPosition)
            Else
                pnlHandDominance.Visible = False
            End If

            'hide patient occupation panel
            If _HidePatientOccupation = False Then
                '3
                SetRowAndColumnPosition(pnlOccupation, _countRows, _countColumnPosition)

            Else
                pnlOccupation.Visible = False
            End If

            'hide patient phone panel
            If _HidePatientPhone = False Then
                '4
                pnlPatientHomePhone.Visible = True
                SetRowAndColumnPosition(pnlPatientHomePhone, _countRows, _countColumnPosition)

            Else
                pnlPatientHomePhone.Visible = False
            End If

            'hide patient cell phone panel
            If _HidePatientCellPhone = False Then
                '5
                pnlPatientCellPhone.Visible = True
                SetRowAndColumnPosition(pnlPatientCellPhone, _countRows, _countColumnPosition)
            Else
                pnlPatientCellPhone.Visible = False
            End If

            'hide referral panel
            If _HidePCP = False Then
                '6
                pnlRefPhysician.Visible = True
                SetRowAndColumnPosition(pnlRefPhysician, _countRows, _countColumnPosition)

            Else
                pnlRefPhysician.Visible = False
            End If

            'hide pnlReferrals panel
            If _HideReferral = False Then
                pnlReferrals.Visible = True
                SetRowAndColumnPosition(pnlReferrals, _countRows, _countColumnPosition)

            Else
                pnlReferrals.Visible = False
            End If

            'hide patient Primary Insurance panel
            If _HidePrimaryInsurance = False Then
                '10
                pnlPrimaryInsurance.Visible = True
                SetRowAndColumnPosition(pnlPrimaryInsurance, _countRows, _countColumnPosition)

            Else
                pnlPrimaryInsurance.Visible = False
            End If

            'hide patient Secondary Insurance panel
            If _HideSecondaryInsurance = False Then
                '11
                pnlSecondaryInsurance.Visible = True
                SetRowAndColumnPosition(pnlSecondaryInsurance, _countRows, _countColumnPosition)

            Else
                pnlSecondaryInsurance.Visible = False
            End If

            'hide patient Secondary Insurance panel
            If _HideSSN = False Then
                '12
                pnlSSN.Visible = True
                SetRowAndColumnPosition(pnlSSN, _countRows, _countColumnPosition)

            Else
                pnlSSN.Visible = False
            End If

            'hide Pharmacy Phone panel
            If _HidePharmacyPhone = False Then
                '8
                pnlPharmacyPhone.Visible = True
                SetRowAndColumnPosition(pnlPharmacyPhone, _countRows, _countColumnPosition)

            Else
                pnlPharmacyPhone.Visible = False
            End If

            'hide pharmacy fax panel
            If _HidePharmacyFax = False Then
                '9
                pnlPharmacyFax.Visible = True
                SetRowAndColumnPosition(pnlPharmacyFax, _countRows, _countColumnPosition)

            Else
                pnlPharmacyFax.Visible = False
            End If


            ''condition added if PMalert is OFF then only two columns are there of if PM alert is on and row count is greater than 3 because 0,1,2,3 rows in 3 column are reserve for PM balance

            If (_HidePMAlert = False And _countRows < 4) Or (_HidePMAlert = True) Then

                If _HidePharmacyName = False Then
                    '7
                    pnlPharmacyName.Visible = True
                    If (_countColumnPosition = 1) Then
                        _countRows += 1

                    End If

                    TableLayout.Controls.Add(pnlPharmacyName, 0, _countRows)
                    TableLayout.SetColumnSpan(pnlPharmacyName, 2)
                    _countRows += 1
                    _countColumnPosition = 0

                Else
                    pnlPharmacyName.Visible = False
                End If

                ''Hide Pharmacy Address 1 + Address 2 + State + City + Zip
                If _HideAddress = False And lblPharmacyAdd.Text.Trim() <> "" Then
                    '13
                    pnlPharmacyAdd.Visible = True
                    If (_countColumnPosition = 1) Then
                        _countRows += 1
                    End If
                    TableLayout.Controls.Add(pnlPharmacyAdd, 0, _countRows)
                    TableLayout.SetColumnSpan(pnlPharmacyAdd, 2)

                    If (_countColumnPosition > MaxColumnPos) Or (_countRows < 4 And _HidePMAlert = False) Or _HidePMAlert = True Then
                        _countRows += 1
                        _countColumnPosition = 0
                    End If

                Else
                    pnlPharmacyAdd.Visible = False
                End If

            Else
                ''condition added if PMalert is ON  and rowcount is greater than 3 then now other panel can come in 3 column except Pharmacy name and address as it require 2 column size

                'hide pharmacy name panel
                Dim PharmacyCol As Integer = -1
                If _HidePharmacyName = False Then
                    '7
                    pnlPharmacyName.Visible = True
                    If (_countColumnPosition = 1) And _HidePMAlert = True Then
                        _countRows += 1
                        _countColumnPosition = 0
                    ElseIf _countColumnPosition = 2 Then
                        _countRows += 1
                        _countColumnPosition = 0
                    End If

                    TableLayout.Controls.Add(pnlPharmacyName, _countColumnPosition, _countRows) ''  _countColumnPosition 0
                    TableLayout.SetColumnSpan(pnlPharmacyName, 2)
                    PharmacyCol = _countColumnPosition
                    _countColumnPosition += 2
                    If (_countColumnPosition > MaxColumnPos) Then
                        _countRows += 1
                        _countColumnPosition = 0
                    End If
                    '_countColumnPosition = 0
                Else
                    pnlPharmacyName.Visible = False
                End If

                ''Hide Pharmacy Address 1 + Address 2 + State + City + Zip
                If _HideAddress = False And lblPharmacyAdd.Text.Trim() <> "" Then
                    '13
                    pnlPharmacyAdd.Visible = True
                    If PharmacyCol = -1 And _HidePMAlert = True Then  ''pharmacy name  setting is not selected
                        If (_countColumnPosition = 1) Then
                            _countRows += 1
                        End If
                    ElseIf PharmacyCol = -1 Then
                        If _countColumnPosition = 2 Then
                            _countRows += 1
                        End If
                    Else
                        _countRows += 1
                        _countColumnPosition = PharmacyCol
                    End If
                    TableLayout.Controls.Add(pnlPharmacyAdd, _countColumnPosition, _countRows) ''column 0 
                    TableLayout.SetColumnSpan(pnlPharmacyAdd, 2)
                    _countColumnPosition += 2
                    If (_countColumnPosition > MaxColumnPos) Then
                        _countRows += 1
                        _countColumnPosition = 0
                    End If

                Else
                    pnlPharmacyAdd.Visible = False
                End If

            End If

            If _HideMedicalCategory = False Then
                pnlMedicalCategory.Visible = True
                SetRowAndColumnPosition(pnlMedicalCategory, _countRows, _countColumnPosition)

            Else
                pnlMedicalCategory.Visible = False
                _HideMedicalCategory = True
            End If

            'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: set value according to the setting
            If _HideGlobalPeriod = False And Not _GlobalPeriod.Trim() = "" Then
                '14
                pnlGlobalPeriod.Visible = True
            Else
                pnlGlobalPeriod.Visible = False
                _HideGlobalPeriod = True
            End If

            '  If (_HidePMAlert = False) And (_countColumnPosition <= 1) Then
            If _HideAllergies = False Then
                pnlAllergies.Visible = True
                SetRowAndColumnPosition(pnlAllergies, _countRows, _countColumnPosition)
            Else
                pnlAllergies.Visible = False
                _HideAllergies = True
            End If


            If _HideNextAppointment = False Then
                pnlNextAppointment.Visible = True
                SetRowAndColumnPosition(pnlNextAppointment, _countRows, _countColumnPosition)
            Else
                pnlNextAppointment.Visible = False
                _HideNextAppointment = True
            End If
            '' add PMalert and EMR Alert

            If _HidePMAlert = False Then
                pnlPMAlerts.Visible = True

                SetRowAndColumnPosition(pnlPMAlerts, _countRows, _countColumnPosition)
            Else
                pnlPMAlerts.Visible = False
                _HidePMAlert = True
            End If


            If _HideEMRAlert = False Then
                pnlEMRAlerts.Visible = True
                SetRowAndColumnPosition(pnlEMRAlerts, _countRows, _countColumnPosition)
            Else
                pnlEMRAlerts.Visible = False
                _HideEMRAlert = True
            End If
            'if control access from Patient Exam and CQM Cypress testing is Enable then only show Discharge Date
            If CallingFormName = enumFormName.PatientExam And IsEnableCQMCypressTesting Then
                SetRowAndColumnPosition(pnladmitdate, _countRows, _countColumnPosition)
            End If
            If CallingFormName = enumFormName.PatientExam And IsEnableCQMCypressTesting Then
                SetRowAndColumnPosition(pnlDischargeDate, _countRows, _countColumnPosition)
            End If
          

            If (_HidePMAlert = False) Then
                pnlCopay.Visible = True
                pnlCopay.Dock = DockStyle.Right
                TableLayout.Controls.Add(pnlCopay, 2, 0)
                pnlPatientBalance.Visible = True
                pnlPatientBalance.Dock = DockStyle.Right
                lbl_PatBal.Dock = DockStyle.Right
                TableLayout.Controls.Add(pnlPatientBalance, 2, 1)
                pnlTotalBalance.Visible = True
                pnlTotalBalance.Dock = DockStyle.Right
                lbl_TotBal.Dock = DockStyle.Right
                TableLayout.Controls.Add(pnlTotalBalance, 2, 2)
                pnlBadDebt.Visible = True
                pnlBadDebt.Dock = DockStyle.Right
                lblBadDebt.Dock = DockStyle.Right
                TableLayout.Controls.Add(pnlBadDebt, 2, 3)
                If _countRows < 3 Then
                    _countRows = 3
                    _countRows += 1
                End If

            End If

            'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
            'If IsNothing(oDataTable) = False Then
            '    oDataTable.Dispose()
            '    oDataTable = Nothing
            'End If
            oDataTable = Nothing

            If IsNothing(dspatient) = False Then
                If dspatient.Tables("Insurance").Rows.Count > 0 Then
                    oDataTable = dspatient.Tables("Insurance")
                    _SecondaryInsurance = ""
                    _PrimaryInsurance = ""

                    For i As Integer = 0 To oDataTable.Rows.Count - 1
                        If oDataTable.Rows(i).Item("nInsuranceFlag") = 2 Then
                            If _SecondaryInsurance = "" Then
                                _SecondaryInsurance = oDataTable.Rows(i).Item("sInsuranceName")
                            Else
                                _SecondaryInsurance = _SecondaryInsurance & ", " & oDataTable.Rows(i).Item("sInsuranceName")
                            End If
                        ElseIf oDataTable.Rows(i).Item("nInsuranceFlag") = 1 Then
                            If _PrimaryInsurance = "" Then
                                _PrimaryInsurance = oDataTable.Rows(i).Item("sInsuranceName")
                            Else
                                _PrimaryInsurance = _PrimaryInsurance & ", " & oDataTable.Rows(i).Item("sInsuranceName")
                            End If
                        End If
                    Next
                End If
            End If

            lblPrimaryInsurance.Text = _PrimaryInsurance
            lblSecondaryInsurance.Text = _SecondaryInsurance
            _HidePMAlert = True
            Dim ht As Integer
            pnlButton.SendToBack()
            If _countRows >= 1 And _countColumnPosition >= 1 Then
                _countRows = _countRows + 1
                ht = (_countRows * 20)
            Else
                ht = (_countRows * 20)
            End If

            'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: set height according to setting is on or off
            If _HideGlobalPeriod = False And Not _GlobalPeriod.Trim() = "" Then
                ControlHeight = lbl_BorderTOP.Height + pnlButton.Height + lbl_BorderBOTTOM.Height + pnlGlobalPeriod.Height + ht + 6
            Else
                ControlHeight = lbl_BorderTOP.Height + pnlButton.Height + lbl_BorderBOTTOM.Height + ht + 6
            End If

            If _HidePhoto Then
                Me.Height = ControlHeight
                Panel3.Visible = False
            Else
                If ControlHeight < 88 Then
                    'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: set height according to setting is on or off
                    If _HideGlobalPeriod = False And Not _GlobalPeriod.Trim() = "" Then
                        ControlHeight = 88 + pnlGlobalPeriod.Height
                    Else
                        ControlHeight = 88
                    End If
                End If
                Me.Height = ControlHeight
                Panel3.Width = Panel3.Height * 0.9
                GetPatientPhoto(PatientID, PB_Patient)
            End If

            '06212012 :: Show Send Secure Message Button As Per Intuit Setting
            If _IntuitCommunication Then
                btnSendMsg.Visible = GetUserRights()
            Else
                btnSendMsg.Visible = False
            End If

            '06222012 :: If Calling Form Is From Send Intuit Message Form Or Read Intuit Message Form then Hide btnSendMsg
            If CallingFormName = enumFormName.Intuit Then
                btnSendMsg.Visible = False
            End If

            If CallingFormName = enumFormName.MedicationHistory Then
                btnSendMsg.Visible = False
                Label16.Visible = False
                dtpDate.Visible = False
            End If


            FillMedicalCategoryHashTable()
            GetMedicalCategoryImage()

            'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
            'If IsNothing(oDataTable) = False Then
            '    oDataTable.Dispose()
            '    oDataTable = Nothing
            'End If
            oDataTable = Nothing


            If CallingFormName = enumFormName.RxFillNotifications Then
                Label16.Visible = False
                dtpDate.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            lbl_BorderLEFT.SendToBack()
            lbl_BorderRIGHT.SendToBack()
            lbl_BorderTOP.SendToBack()
            lbl_BorderBOTTOM.SendToBack()



        End Try
        If (dtpDate.Enabled = True) Then  '' added for Bugid #93863 
            If (Convert.ToString(dtpDate.Value).Contains(":")) Then
                If (intwdthofdtPicker = 0) Then
                    intwdthofdtPicker = dtpDate.Width
                End If
                dtpDate.Width = intwdthofdtPicker + 32
            End If
        End If
    End Sub
    Dim MaxColumnPos As Integer = 1
    Private Sub SetRowAndColumnPosition(ByVal obj As Object, ByRef RowPos As Integer, ByRef ColPos As Integer)
        If (_HidePMAlert = False) Then
            If RowPos < 4 Then ''if row pos <4 and pm alert setting is on 
                If (ColPos = 0) Then
                    TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                    ColPos = 1
                Else
                    TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                    ColPos = 0
                End If

                If (ColPos = 0) Then
                    RowPos += 1
                End If
            Else
                ''if row pos >=4 and pm alert setting is on then column count is more than 3
                If (ColPos = 0) Then
                    TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                    ColPos = 1
                ElseIf (ColPos = 1) Then
                    TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                    ColPos = 2
                Else
                    TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                    ColPos = 0
                End If

                If (ColPos = 0) Then
                    RowPos += 1
                End If
            End If
            'If RowPos > 3 Then
            '    MaxColumnPos = 2
            'Else
            '    MaxColumnPos = 1
            'End If
        Else
            If (ColPos = 0) Then
                TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                ColPos = 1
            Else
                TableLayout.Controls.Add(CType(obj, Panel), ColPos, RowPos)
                ColPos = 0
            End If

            If (ColPos = 0) Then
                RowPos += 1
            End If
        End If

    End Sub



    Private Function GetUserRights() As Boolean

        Dim intuitUserRights As Boolean = False

        Try
            Dim arrLst As ArrayList = gloGlobal.LoadFromAssembly.RetriveUserRightsForPatientStrip()

            ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal".
            ''change name of right from "Intuit" to "Patient Portal"
            If (IsNothing(arrLst) = True) Then
                intuitUserRights = False
            Else
                If arrLst.Contains("Patient Portal") Then
                    intuitUserRights = True
                Else
                    intuitUserRights = False
                End If
            End If
            
            arrLst = Nothing

            Return intuitUserRights
        Catch ex As Exception
            Return intuitUserRights
        End Try
    End Function

    Private Function FillPatientStripDetails(ByVal PatientID As Int64, Optional ByVal CallingFormName As enumFormName = enumFormName.None, Optional ByVal ExamID As Int64 = 0, Optional ByVal VisitID As Int64 = 0, Optional ByVal ProviderId As Int64 = 0, Optional ByVal blnflag As Boolean = False, Optional ByVal blnRxTranMode As Boolean = False, Optional ByVal blnIsProviderequal As Boolean = False, Optional ByVal Rxvalue As String = "", Optional ByVal IsPharmacyEnabled As Boolean = False) As DataSet
        Dim _sqlconn As SqlConnection = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim _sqlcmd As SqlCommand
        Dim _sqlda As SqlDataAdapter
        Dim ds As New DataSet
        Dim _sqlparam As SqlParameter
        Try
            _sqlcmd = New SqlCommand("GetPatientStripDetails", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure
            _sqlda = New SqlDataAdapter(_sqlcmd)

            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@CallingFormName", SqlDbType.Int)
            _sqlparam.Value = CallingFormName
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            _sqlparam.Value = ExamID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam.Value = VisitID
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@ProviderId", SqlDbType.BigInt)
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam.Value = ProviderId
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@blnflag", SqlDbType.Bit)
            _sqlparam.Value = blnflag
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@blnRxTranMode", SqlDbType.Bit)
            _sqlparam.Value = blnRxTranMode
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@blnIsProviderequal", SqlDbType.Bit)
            _sqlparam.Value = blnIsProviderequal
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@Rxvalue", SqlDbType.VarChar)
            _sqlparam.Value = Rxvalue
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@IsPharmacyEnabled", SqlDbType.Bit)
            _sqlparam.Value = IsPharmacyEnabled
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlda.Fill(ds)

            If Not IsNothing(_sqlda) Then
                _sqlda.Dispose() : _sqlda = Nothing
            End If

            If Not IsNothing(_sqlcmd) Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose() : _sqlcmd = Nothing
            End If

            If IsNothing(_sqlconn) = False Then
                _sqlconn.Dispose() : _sqlconn = Nothing
            End If

            ds.Tables(0).TableName = "Module"
            ds.Tables(1).TableName = "PCP"
            ds.Tables(2).TableName = "PharmaName"
            ds.Tables(3).TableName = "PharmaPhone"
            ds.Tables(4).TableName = "PharmaFax"
            ds.Tables(5).TableName = "ReffName"
            ds.Tables(6).TableName = "Provider"
            ds.Tables(7).TableName = "PatientInfo"
            ds.Tables(8).TableName = "AgeSettings"
            ds.Tables(9).TableName = "PediatricSetting"
            ds.Tables(10).TableName = "Insurance"
            ds.Tables(11).TableName = "PharmacyAddress1"
            ds.Tables(12).TableName = "PharmacyAddress2"
            ds.Tables(13).TableName = "PharmaState"
            ds.Tables(14).TableName = "PharmaCity"
            ds.Tables(15).TableName = "PharmaZip"
            ds.Tables(16).TableName = "IntuitSettings"

            'Developer: Sanjog Dhamke Date: 17 March 2012 Bug PRD Name: Global Period on Patient Banner Reason: to set proper table name in DS
            If CallingFormName = enumFormName.Prescription And blnRxTranMode = False Then
                ds.Tables(17).TableName = "PrescriptionInfo"
                ds.Tables(18).TableName = "GlobalPeriod"
                ds.Tables(19).TableName = "MedicalCategory"
                ds.Tables(20).TableName = "PMAlert"  ''added for 8070  changes PM,EMR alert,allergies,nextappt
                ds.Tables(21).TableName = "EMRAlert"
                ds.Tables(22).TableName = "Allergies"
                ds.Tables(23).TableName = "NextAppointment"
            Else
                ds.Tables(17).TableName = "GlobalPeriod"
                ds.Tables(18).TableName = "MedicalCategory"
                ds.Tables(19).TableName = "PMAlert"  ''added for 8070  changes PM,EMR alert,allergies,nextappt
                ds.Tables(20).TableName = "EMRAlert"
                ds.Tables(21).TableName = "Allergies"
                ds.Tables(22).TableName = "NextAppointment"

                If ds.Tables.Count > 23 Then
                    ds.Tables(23).TableName = "IsBadDebt"
                End If
            End If

            Return ds

        Catch ex As Exception
            Return Nothing
        Finally
            If IsNothing(ds) = False Then
                ds = Nothing
            End If
        End Try
    End Function

    ''added for reteriving only emr alert
    Private Sub FillPatientEMRAlert(ByVal PatientID As Int64)
        Dim _sqlconn As SqlConnection = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
        Dim _sqlcmd As SqlCommand = Nothing

        Dim strEmrAlterName As String = String.Empty
        Dim _sqlparam As SqlParameter
        Try
            _sqlcmd = New SqlCommand("GetPatientStripDetails", _sqlconn)
            _sqlcmd.CommandType = CommandType.StoredProcedure


            _sqlparam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            _sqlparam.Value = PatientID
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing

            _sqlparam = _sqlcmd.Parameters.Add("@OnlyEMRAlert", SqlDbType.Bit)
            _sqlparam.Value = True
            _sqlparam.Direction = ParameterDirection.Input
            _sqlparam = Nothing
            If (_sqlconn.State = ConnectionState.Closed) Then
                _sqlconn.Open()
            End If
            strEmrAlterName = Convert.ToString(_sqlcmd.ExecuteScalar())

            lbl_Emralrt.Text = strEmrAlterName

        Catch ex As Exception
            lbl_Emralrt.Text = ""

        Finally

            If IsNothing(_sqlconn) = False Then
                If _sqlconn.State = ConnectionState.Open Then
                    _sqlconn.Close()
                End If
                _sqlconn.Dispose() : _sqlconn = Nothing
            End If
            If Not IsNothing(_sqlcmd) Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose() : _sqlcmd = Nothing
            End If

        End Try
    End Sub

    Private Function Get_PatPharmProv_Details(ByVal Patient_id As Long, ByVal Provider_id As Long, ByVal id As Long, ByVal blnRxtranmode As Boolean, Optional ByVal CallingFormName As enumFormName = enumFormName.Prescription) As Boolean
        Dim oDataTable As DataTable = Nothing
        Try
            If blnRxtranmode Then
                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PatientInfo").Rows.Count > 0 Then
                        oDataTable = dspatient.Tables("PatientInfo")
                        _Code = oDataTable.Rows(0).Item("sPatientCode") & ""
                        _Name = oDataTable.Rows(0).Item("PatientName") & ""
                        _Gender = oDataTable.Rows(0).Item("sGender") & ""
                        _PatientOccupation = oDataTable.Rows(0).Item("sOccupation") & ""
                        _SSN = oDataTable.Rows(0).Item("nSSN") & ""
                        _HandDominance = oDataTable.Rows(0).Item("sHandDominance") & ""
                        If Not IsDBNull(oDataTable.Rows(0).Item("dtDOB")) Then
                            _DateOfBirth = oDataTable.Rows(0).Item("dtDOB")
                            If _Age Is Nothing Then
                                _Age = New AgeDetail
                            End If
                            ''Added on 20110810
                            If IsPediatric = False Then
                                _Age = FormatAge(_DateOfBirth)

                            Else
                                _TimeSpan = GetAgeInHrs(_DateOfBirth, _BirthTime)
                                If Not IsNothing(_TimeSpan) Then
                                    If _TimeSpan.TotalDays < 4 Then
                                        _Age.Hours = _TimeSpan.TotalHours
                                        _Age.Age = _TimeSpan.TotalHours.ToString("0") & " Hours"
                                        ''''Hours
                                    ElseIf _TimeSpan.TotalDays > 4 And (_TimeSpan.TotalDays <= 28 Or _TimeSpan.Hours = 0) Then
                                        _ShowAgeInDays = True
                                        _Age = FormatAge(_DateOfBirth)
                                        ''''days
                                    ElseIf _TimeSpan.TotalDays > 28 And _TimeSpan.TotalDays <= 90 Then
                                        _Age.Age = (_TimeSpan.Days / 7).ToString("0") & " Weeks"
                                        '''' weeks
                                    Else
                                        _Age = FormatAge(_DateOfBirth)
                                        If _Age.Years < 2 And _Age.Months >= 0 Then
                                            _Age.Age = (_Age.Years * 12) + _Age.Months & " Months"
                                        End If

                                    End If

                                End If
                            End If
                            ''End
                            '_Age = FormatAge(_DateOfBirth)
                        End If
                        _PatientPhone = oDataTable.Rows(0).Item("sPhone") & ""
                        _PatientCellPhone = oDataTable.Rows(0).Item("sMobile") & ""
                    End If
                End If

                'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                'If IsNothing(oDataTable) = False Then
                '    oDataTable.Dispose()
                '    oDataTable = Nothing
                'End If
                oDataTable = Nothing

                If IsNothing(dspatient) = False Then
                    oDataTable = dspatient.Tables("PharmaName")
                    If dspatient.Tables("PharmaName").Rows.Count > 0 Then
                        _PharmacyName = dspatient.Tables("PharmaName").Rows(0)(0)
                    End If
                    'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                    'If IsNothing(oDataTable) = False Then
                    '    oDataTable.Dispose()
                    '    oDataTable = Nothing
                    'End If
                    'oDataTable = Nothing

                    oDataTable = dspatient.Tables("PharmaPhone")
                    If dspatient.Tables("PharmaPhone").Rows.Count > 0 Then
                        _PharmacyPhone = dspatient.Tables("PharmaPhone").Rows(0)(0)
                    End If
                    'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                    'If IsNothing(oDataTable) = False Then
                    '    oDataTable.Dispose()
                    '    oDataTable = Nothing
                    'End If
                    'oDataTable = Nothing

                    oDataTable = dspatient.Tables("PharmaFax")
                    If dspatient.Tables("PharmaFax").Rows.Count > 0 Then
                        _PharmacyFax = dspatient.Tables("PharmaFax").Rows(0)(0)
                    End If
                End If

            Else 'the blnRxtranmode  value is false means the RxTransMode is Edti

                '''''''''''first query
                'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                'If IsNothing(oDataTable) = False Then
                '    oDataTable.Dispose()
                '    oDataTable = Nothing
                'End If
                oDataTable = Nothing

                If IsNothing(dspatient) = False Then
                    oDataTable = dspatient.Tables("PharmaName")
                    If dspatient.Tables("PharmaName").Rows.Count > 0 Then
                        _PharmacyName = dspatient.Tables("PharmaName").Rows(0)(0)
                    End If
                    'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                    'If IsNothing(oDataTable) = False Then
                    '    oDataTable.Dispose()
                    '    oDataTable = Nothing
                    'End If
                    'oDataTable = Nothing

                    oDataTable = dspatient.Tables("PharmaPhone")
                    If dspatient.Tables("PharmaPhone").Rows.Count > 0 Then
                        _PharmacyPhone = dspatient.Tables("PharmaPhone").Rows(0)(0)
                    End If
                    'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                    'If IsNothing(oDataTable) = False Then
                    '    oDataTable.Dispose()
                    '    oDataTable = Nothing
                    'End If
                    ' oDataTable = Nothing

                    oDataTable = dspatient.Tables("PharmaFax")
                    If dspatient.Tables("PharmaFax").Rows.Count > 0 Then
                        _PharmacyFax = dspatient.Tables("PharmaFax").Rows(0)(0)
                    End If
                End If

                'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                'If IsNothing(oDataTable) = False Then
                '    oDataTable.Dispose()
                '    oDataTable = Nothing
                'End If
                oDataTable = Nothing

                If IsNothing(dspatient) = False Then
                    If dspatient.Tables("PatientInfo").Rows.Count > 0 Then
                        oDataTable = dspatient.Tables("PatientInfo")
                        _Code = oDataTable.Rows(0).Item("sPatientCode") & ""
                        _Name = oDataTable.Rows(0).Item("PatientName")
                        If Not IsDBNull(oDataTable.Rows(0).Item("nSSN")) Then
                            _SSN = oDataTable.Rows(0).Item("nSSN") & ""
                        Else
                            _SSN = ""
                        End If

                        If Not IsDBNull(oDataTable.Rows(0).Item("dtDOB")) Then
                            _DateOfBirth = oDataTable.Rows(0).Item("dtDOB")
                            If _Age Is Nothing Then
                                _Age = New AgeDetail
                            End If
                            If IsPediatric = False Then
                                _Age = FormatAge(_DateOfBirth)

                            Else
                                _TimeSpan = GetAgeInHrs(_DateOfBirth, _BirthTime)
                                If Not IsNothing(_TimeSpan) Then
                                    If _TimeSpan.TotalDays < 4 Then
                                        _Age.Hours = _TimeSpan.TotalHours
                                        _Age.Age = _TimeSpan.TotalHours.ToString("0") & " Hours"
                                        ''''Hours
                                    ElseIf _TimeSpan.TotalDays > 4 And (_TimeSpan.TotalDays <= 28 Or _TimeSpan.Hours = 0) Then
                                        _ShowAgeInDays = True
                                        _Age = FormatAge(_DateOfBirth)
                                        ''''days
                                    ElseIf _TimeSpan.TotalDays > 28 And _TimeSpan.TotalDays <= 90 Then
                                        _Age.Age = (_TimeSpan.Days / 7).ToString("0") & " Weeks"
                                        '''' weeks
                                    Else
                                        _Age = FormatAge(_DateOfBirth)
                                        If _Age.Years < 2 And _Age.Months >= 0 Then
                                            _Age.Age = (_Age.Years * 12) + _Age.Months & " Months"
                                        End If

                                    End If

                                End If
                            End If
                        End If

                        _PatientPhone = oDataTable.Rows(0).Item("sPhone") & ""

                        If Not IsDBNull(oDataTable.Rows(0).Item("sMobile")) Then
                            _PatientCellPhone = oDataTable.Rows(0).Item("sMobile") & ""
                        End If

                        If Not IsDBNull(oDataTable.Rows(0).Item("sOccupation")) Then
                            _PatientOccupation = oDataTable.Rows(0).Item("sOccupation") & ""
                        End If

                        If Not IsDBNull(oDataTable.Rows(0).Item("sGender")) Then
                            _Gender = oDataTable.Rows(0).Item("sGender") & ""
                        End If

                        If Not IsDBNull(oDataTable.Rows(0).Item("sHandDominance")) Then
                            _HandDominance = oDataTable.Rows(0).Item("sHandDominance") & ""
                        End If

                        'SLR: 11/19/2015: should not be disposed, since it is only reference from dspatient
                        'If IsNothing(oDataTable) = False Then
                        '    oDataTable.Dispose()
                        '    oDataTable = Nothing
                        'End If
                        oDataTable = Nothing

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return True
    End Function
    Private resourceCulture As Global.System.Globalization.CultureInfo
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Friend Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set(value As Global.System.Globalization.CultureInfo)
            resourceCulture = value
        End Set
    End Property
    Private Sub gloUC_PatientStrip_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnUp.Visible = True
        'Sanjog - Commneted on 2011 June 14 to show new button image 
        'btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP19
        'btnUp.BackgroundImageLayout = ImageLayout.Center
        'Sanjog - Commneted on 2011 June 14 to show new button image 
        btnDown.Visible = False
        pnlPatientDetail.Visible = True




        Dim obj As Object = hshPatData(PatID)
        If obj Is Nothing Then
            hshPatData.Add(PatID, gloGlobal.gloPMGlobal.UserID)
            AddrecentPatient(PatID, gloGlobal.gloPMGlobal.UserID, hshPatData)
        End If
        '  ValidatePortalFeatures()
        IsPatientregisteredOnportal()

        On Error Resume Next
        Me.Height = ControlHeight ''pnlTOP_Border.Height + pnlButton.Height + pnlBOTTOM_Border.Height
        RaiseEvent ControlSizeChanged()
        '  intwdthofdtPicker = dtpDate.Width
    End Sub
    Dim sortedDict = Nothing
    Private Sub FillMedicalCategoryHashTable()
        If IsNothing(DishtblMedcatClr) Then
            DishtblMedcatClr = New Dictionary(Of String, String)

            DishtblMedcatClr.Add("MedicalCategoryImages_1_Brown_TopBrown", "MedicalCategoryImages_1_Brown_BottomBrown")
            DishtblMedcatClr.Add("MedicalCategoryImages_2_Blue_TopSkyBlue", "MedicalCategoryImages_2_Blue_BottomSkyBlue")
            DishtblMedcatClr.Add("MedicalCategoryImages_3_Gray_TopGray", "MedicalCategoryImages_3_Gray_BottomGray")
            DishtblMedcatClr.Add("MedicalCategoryImages_4_GreenTopLightGreen", "MedicalCategoryImages_4_Green_BottomLightGreen")
            DishtblMedcatClr.Add("MedicalCategoryImages_5_TopOrange", "MedicalCategoryImages_5_BottomOrange")

            DishtblMedcatClr.Add("MedicalCategoryImages_6_Pink_TopPink", "MedicalCategoryImages_6_Pink_BottomPink")
            DishtblMedcatClr.Add("MedicalCategoryImages_7_Red_TopRed", "MedicalCategoryImages_7_Red_BottomRed")
            DishtblMedcatClr.Add("MedicalCategoryImages_8_Violet_TopViolet", "MedicalCategoryImages_8_Violet_BottomViolet")
            DishtblMedcatClr.Add("MedicalCategoryImages_9_Yellow_TopYellow", "MedicalCategoryImages_9_Yellow_BottomYellow")
            DishtblMedcatClr.Add("MedicalCategoryImages_91_TopDark_Blue", "MedicalCategoryImages_91_BottomDark_Blue")
            '   DishtblMedcatClr.  
            sortedDict = (From entry In DishtblMedcatClr Order By entry.Key Ascending).ToDictionary(Function(pair) pair.Key, Function(pair) pair.Value)

        End If

    End Sub
    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        IsSizeMaximized = False
        RaiseEvent btnUpOrDownclick()
        'Developer: Sanjog Dhamke
        'Date: 17 March 2012
        'Bug PRD Name: Global Period on Patient Banner
        'Reason: to make panel visible false
        pnlPatientDetail.Visible = False
        pnlGlobalPeriod.Visible = False
        btnUp.Visible = False
        btnDown.Visible = True
        'Added by Mayuri:20100503-To display code and name on patient strip control if patient details panel is hide
        'pnlcode_Name.Visible = True

        'Size
        On Error Resume Next
        pnlPatientDetail.Height = 0
        Me.Height = lbl_BorderTOP.Height + pnlButton.Height + lbl_BorderBOTTOM.Height
        Panel1.Height = Me.Height
        'pnlButton.Height = Me.Height

        RaiseEvent ControlSizeChanged()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        pnlPatientDetail.Visible = True
        'Developer: Sanjog Dhamke
        'Date: 17 March 2012
        'Bug PRD Name: Global Period on Patient Banner
        'Reason: If Global Period is not checked for control then don't show this else show this 
        If _HideGlobalPeriod = False Then
            pnlGlobalPeriod.Visible = True
        End If
        btnUp.Visible = True
        'Added by Mayuri:20100503-To display code and name on patient strip control if patient details panel is hide
        'pnlcode_Name.Visible = False
        'End 20100503
        IsSizeMaximized = True
        RaiseEvent btnUpOrDownclick()
        btnDown.Visible = False
        'Size
        On Error Resume Next
        pnlPatientDetail.Height = PatientDetailHeight   ''64
        Me.Height = ControlHeight   '' 96

        RaiseEvent ControlSizeChanged()

    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        dtpDate.Value = Now
        ' Add any initialization after the InitializeComponent() call.
        _Age = New AgeDetail
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub Rx_RefreshPharmacy(ByVal _PhName As String, ByVal _PhPhone As String, ByVal _PhFax As String, ByVal _PhAddress1 As String, ByVal _PhAddress2 As String, ByVal _PhCity As String, ByVal _PhState As String, ByVal _PhZip As String, ByVal IsPharmacyEnabled As Boolean)
        'added in 6050 For PRD Usability (add Phramcy)
        If IsPharmacyEnabled = True Then
            lblPhName.Image = Nothing
            lblPhName.Image = Global.gloUserControlLibrary.My.Resources.ePharmacy2
            lblPhName.ImageAlign = ContentAlignment.MiddleLeft
        Else
            lblPhName.Image = Nothing
        End If

        _PharmacyName = _PhName
        _PharmacyFax = _PhFax
        _PhPhone = _PhPhone


        If IsNothing(lblPhName.Image) = False Then
            lblPhName.Text = Space(6) & _PharmacyName
        Else
            lblPhName.Text = _PharmacyName
        End If

        lblPhFax.Text = _PhFax
        lblPhPhone.Text = _PhPhone

        Dim txtPharmacyTemp As String = ""
        Dim txtState As String = ""
        Dim txtCity As String = ""
        Dim txtZip As String = ""
        Dim txtAdd1 As String = ""
        Dim txtAdd2 As String = ""
        Dim txtCombineAdd As String = ""

        txtAdd1 = _PhAddress1
        txtAdd2 = _PhAddress2

        If txtAdd1 <> "" And txtAdd2 <> "" Then
            txtCombineAdd = txtAdd1 & ", " & txtAdd2
        ElseIf txtAdd1 <> "" And txtAdd2 = "" Then
            txtCombineAdd = txtAdd1
        ElseIf txtAdd1 = "" And txtAdd2 <= "" Then
            txtCombineAdd = txtAdd2
        End If

        txtState = _PhState
        txtCity = _PhCity
        txtZip = _PhZip

        If txtCity <> "" And txtState <> "" And txtZip <> "" Then
            txtPharmacyTemp = txtCity & ", " & txtState & ", " & txtZip
        ElseIf txtCity <> "" And txtState <> "" And txtZip = "" Then
            txtPharmacyTemp = txtCity & ", " & txtState
        ElseIf txtCity <> "" And txtState = "" And txtZip <> "" Then
            txtPharmacyTemp = txtCity & ", " & txtZip
        ElseIf txtCity <> "" And txtState = "" And txtZip = "" Then
            txtPharmacyTemp = txtCity
        ElseIf txtCity = "" And txtState <> "" And txtZip <> "" Then
            txtPharmacyTemp = txtState & ", " & txtZip
        ElseIf txtCity = "" And txtState <> "" And txtZip = "" Then
            txtPharmacyTemp = txtState
        ElseIf txtCity = "" And txtState = "" And txtZip <> "" Then
            txtPharmacyTemp = txtZip
        End If

        If txtCombineAdd <> "" And txtPharmacyTemp <> "" Then
            lblPharmacyAdd.Text = txtCombineAdd & ", " & txtPharmacyTemp
        ElseIf txtCombineAdd <> "" And txtPharmacyTemp = "" Then
            lblPharmacyAdd.Text = txtCombineAdd
        ElseIf txtCombineAdd = "" And txtPharmacyTemp <= "" Then
            lblPharmacyAdd.Text = txtPharmacyTemp
        End If

    End Sub

    Private Sub dtpDate_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.DropDown
        RaiseEvent Date_DropDown()
    End Sub

    Private Sub dtpDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.Validated
        RaiseEvent Date_Validated()
    End Sub

    Private Sub dtpDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDate.Validating
        RaiseEvent Date_Validating()
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        ' Label2.Select()
        RaiseEvent Date_ValueChanged()
    End Sub

    Private Sub dtpDate_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.CloseUp
        RaiseEvent Date_CloseUp()
    End Sub

    Public Sub SetProviderName(ByVal ProviderName As String, ByVal ProviderID As Int64)
        _Provider = ProviderName
        _ProviderID = ProviderID
        lblProvider.Text = _Provider
    End Sub

    Public Sub GetSetting()
        ''sudhir 20081126 'fuction to fetch admin settings for ShowAgeInDays on PatientStrip

        Dim dt As DataTable = Nothing

        Try
            If IsNothing(dspatient) = False Then
                If dspatient.Tables("AgeSettings").Rows.Count > 0 Then
                    dt = dspatient.Tables("AgeSettings")
                End If
            End If

            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    If Convert.ToString(dt.Rows(0)(0)).ToUpper = "SHOW AGE IN DAYS" And Convert.ToString(dt.Rows(0)(0)).ToUpper <> "" Then
                        _ShowAgeInDays = dt.Rows(0)(1)
                    Else
                        _ShowAgeInDays = dt.Rows(1)(1)
                    End If

                    '' Age Limit in Years, Convert it to Days
                    If Convert.ToString(dt.Rows(1)(0)).ToUpper = "AGE LIMIT" And Convert.ToString(dt.Rows(1)(0)).ToUpper <> "" Then
                        _AgeLimit = CType((dt.Rows(1)(1) * 365), Int32)
                    Else
                        _AgeLimit = CType((dt.Rows(0)(1) * 365), Int32)
                    End If
                    '_AgeLimitForWeeks = dt.Rows(2)(1)
                End If
            End If

            ''06212012 :: Check intuit Settings to show/hide send secure message button
            If IsNothing(dspatient.Tables("IntuitSettings")) = False Then
                If dspatient.Tables("IntuitSettings").Rows.Count > 0 Then
                    If Convert.ToString(dspatient.Tables("IntuitSettings").Rows(0)(0)).ToUpper = "INTUIT FEATURE ENABLE SETTING" And Convert.ToString(dspatient.Tables("IntuitSettings").Rows(0)(0)).ToUpper <> "" Then
                        _IntuitCommunication = dspatient.Tables("IntuitSettings").Rows(0)(1)
                    Else
                        _IntuitCommunication = dspatient.Tables("IntuitSettings").Rows(1)(1)
                    End If
                End If
            End If

            'If IsNothing(dt) = False Then
            '    dt.Dispose() : dt = Nothing
            'End If

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnRxProvider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRxProvider.Click
        RaiseEvent RxProviderAssociation_Click()
    End Sub

    Private Sub lblReferrals_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblReferrals.MouseHover
        'Dim lblrf As New Label
        'lblrf.Text = _Referral
        'lblrf.Height = lblrf.Height * CntRfr
        'lblrf.Width = lblReferrals.Width
        ''lblrf.Location.X = lblReferrals.Location.X + 10
        'lblrf.Show()
        'lblrf.BringToFront()
    End Sub

#Region "[PEDETRIC SETTIINGS] _BIRTH TIME "

    Public Function GetAgeInHrs(ByVal _DateOfBirth As Date, ByVal _BirthTime As String) As TimeSpan
        Dim AgeDiff As TimeSpan = Nothing
        Try
            Dim sDateTime As String = ""
            Dim Bdate As Date
            Bdate = _DateOfBirth.Date
            sDateTime = Bdate.ToShortDateString() & " " & _BirthTime
            AgeDiff = DateTime.Now.Subtract(Convert.ToDateTime(sDateTime))
            Return AgeDiff
        Catch ex As Exception
            Return AgeDiff
        End Try
    End Function

    Public Sub GetPediatricSetting()
        Try
            Dim dt As DataTable = Nothing

            If IsNothing(dspatient) = False Then
                If dspatient.Tables("PediatricSetting").Rows.Count > 0 Then
                    dt = dspatient.Tables("PediatricSetting")
                End If
            End If
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    _IsPediatric = dt.Rows(0)(1)
                End If
            End If

            'If IsNothing(dt) = False Then
            '    dt.Dispose() : dt = Nothing
            'End If

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

#End Region
    Private Sub AddrecentPatient(ByVal PatID As Int64, ByVal UserID As Int64, ByRef hashtbl As Hashtable)
        Dim oDB As New gloEMRDatabase.DataBaseLayer

        Dim _ProcName As String
        Dim deletedpatid As Int64 = -1
        Try

            ''Retrieve the Image for Patient
            '_strSQL = "select iphoto,sGender from patient where npatientid=" & PatientID
            _ProcName = "gsp_InsertRecentPatientUserwise"
            With oDB
                .DBParametersCol.Clear()
                Dim oPara As gloEMRDatabase.DBParameter
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Convert.ToInt64(UserID)
                oPara.Name = "@UserID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing


                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = PatID
                oPara.Name = "@PatientID"
                .DBParametersCol.Add(oPara)
                oPara = Nothing




            End With


            deletedpatid = oDB.GetDataValue(_ProcName)

            If (deletedpatid <> -1) Then
                If (hashtbl.Contains(deletedpatid)) Then
                    hashtbl.Remove(deletedpatid)
                End If
            End If


        Catch
        Finally
            If Not IsNothing(oDB) Then
                oDB.DBParametersCol.Clear()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try


    End Sub

    Private Sub GetPatientPhoto(ByVal PatientID As Int64, ByRef PicBox As PictureBox)
        Dim oDB As New gloEMRDatabase.DataBaseLayer
        Dim dtPatient As DataTable = Nothing
        Dim _strSQL As String
        Dim PatientPhoto As Image = Nothing
        Try

            ''Retrieve the Image for Patient
            '_strSQL = "select iphoto,sGender from patient where npatientid=" & PatientID
            _strSQL = "SELECT Patient_Photo.iphoto,sGender FROM patient LEFT OUTER JOIN Patient_Photo ON patient.nPatientId= Patient_Photo.nPatientId WHERE Patient_Photo.npatientid=" & PatientID
            dtPatient = oDB.GetDataTable_Query(_strSQL)

            _strSQL = Nothing

            oDB.Dispose()
            oDB = Nothing

            PicBox.BackgroundImageLayout = ImageLayout.Center
            PicBox.SizeMode = PictureBoxSizeMode.StretchImage
            PicBox.BackColor = Color.Transparent
            PicBox.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            If dtPatient.Rows.Count > 0 Then  ''rows count checked for bugid 71304
                If TypeOf (dtPatient.Rows(0)("iPhoto")) Is System.DBNull Then
                    'PatientPhoto = Nothing
                    'MyPictureBoxControl = Nothing
                    If dtPatient.Rows(0)("sGender").ToUpper() = "Male".ToUpper() Then
                        PatientPhoto = gloUserControlLibrary.My.Resources.Resources.MalePatient()
                        aspectratio(CType(PatientPhoto, Bitmap), PicBox)
                    ElseIf dtPatient.Rows(0)("sGender").ToUpper() = "Female".ToUpper() Then
                        PatientPhoto = gloUserControlLibrary.My.Resources.Resources.FemalePatient()
                        aspectratio(CType(PatientPhoto, Bitmap), PicBox)
                    ElseIf dtPatient.Rows(0)("sGender").ToUpper() = "Other".ToUpper() Then
                        'Do nothing show blank Image for Other
                    End If
                Else

                    'SLR: static function return to avoid mem leaks
                    'Dim myPixBx As New gloPictureBox.gloPictureBox()
                    'myPixBx.byteImage = CType(dtPatient.Rows(0)("iPhoto"), Byte())
                    'Dim PatientPhoto As Image = myPixBx.copyFrame(True)

                    'myPixBx.Dispose()
                    'myPixBx = Nothing
                    PatientPhoto = gloPictureBox.gloImage.GetImage(CType(dtPatient.Rows(0)("iPhoto"), Byte()), True)
                    If (IsNothing(PatientPhoto) = False) Then
                        aspectratio(CType(PatientPhoto, Bitmap), PicBox)
                        PicBox.BackgroundImageLayout = ImageLayout.Stretch
                        PicBox.SizeMode = PictureBoxSizeMode.CenterImage
                        If IsNothing(PatientPhoto) = False Then
                            PatientPhoto.Dispose()
                            PatientPhoto = Nothing
                        End If
                    Else

                        If dtPatient.Rows(0)("sGender").ToUpper() = "Male".ToUpper() Then
                            PatientPhoto = gloUserControlLibrary.My.Resources.Resources.MalePatient()
                            aspectratio(CType(PatientPhoto, Bitmap), PicBox)
                        ElseIf dtPatient.Rows(0)("sGender").ToUpper() = "Female".ToUpper() Then
                            PatientPhoto = gloUserControlLibrary.My.Resources.Resources.FemalePatient()
                            aspectratio(CType(PatientPhoto, Bitmap), PicBox)
                        ElseIf dtPatient.Rows(0)("sGender").ToUpper() = "Other".ToUpper() Then
                            'Do nothing show blank Image for Other
                        End If

                    End If


                End If

            End If


        Catch ex As Exception

        Finally
            '25-Nov-14 Aniket: Resolving Bug #76393 ( Modified): gloEMR: Patient registration- Application gives exception

            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose() : dtPatient = Nothing
            End If
        End Try

    End Sub

    Private Sub aspectratio(bmp As Bitmap, ByRef PicBox As PictureBox)
        Dim OutputWidth As Double = bmp.Width
        'currnetwidth
        Dim OutputHeight As Double = bmp.Height
        'currentheight
        Dim picOutputwidth As Double = PicBox.Width
        ' desired width
        Dim picOutputheight As Double = PicBox.Height
        ' desired height

        Dim myPicWidth As Double = picOutputwidth
        Dim myPicHeight As Double = picOutputheight
        Dim myScaleX As Double = myPicWidth / CDbl(OutputWidth)
        Dim myScaleY As Double = myPicHeight / CDbl(OutputHeight)
        Dim myStartX As Double = 0
        Dim myStartY As Double = 0

        If myScaleX > myScaleY Then
            myPicWidth = CDbl(OutputWidth) * myScaleY
            myStartX = (CDbl(picOutputwidth) - myPicWidth) / 2
        Else
            myPicHeight = CDbl(OutputHeight) * myScaleX
            myStartY = (CDbl(picOutputheight) - myPicHeight) / 2
        End If
        PicBox.Image = New Bitmap(bmp, New Size(CInt(myPicWidth), CInt(myPicHeight)))
    End Sub

    Private Sub btnSendMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendMsg.Click
        'Reflection Is implemented to Intuit Send Secure Message Form 
        '06202012-Open Send New Secure Message


        Try

            gloGlobal.LoadFromAssembly.OpenIntuitSendMessage(_Age.Age, nPatientEducationID)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try

    End Sub

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.

                Try
                    If (IsNothing(dtpDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate)
                        Catch ex As Exception

                        End Try


                        dtpDate.Dispose()
                        dtpDate = Nothing
                    End If
                Catch
                End Try


                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                ControlHeight = Nothing
                PatientDetailHeight = Nothing
                _TimeSpan = Nothing

                _Name = Nothing
                _Code = Nothing
                _Provider = Nothing
                _ProviderID = Nothing

                _Age = Nothing

                _DateOfBirth = Nothing
                _BirthTime = Nothing
                _TransactionDate = Nothing

                _PatientPhone = Nothing
                _PharmacyName = Nothing
                _PharmacyFax = Nothing
                _PharmacyPhone = Nothing

                _PatientOccupation = Nothing
                _Referral = Nothing
                _PCP = Nothing
                _PatientCellPhone = Nothing
                _Gender = Nothing
                _HandDominance = Nothing
                _PrimaryInsurance = Nothing
                _SecondaryInsurance = Nothing
                _SSN = Nothing

                _GlobalPeriod = Nothing
                _HideButton = Nothing
                _HidePatientPhone = Nothing
                _HidePharmacyPhone = Nothing
                _HidePharmacyName = Nothing
                _HidePharmacyFax = Nothing

                _HidePatientExam = Nothing
                _HidePatientOccupation = Nothing
                _HideReferral = Nothing
                _HidePCP = Nothing
                _HidePatientCellPhone = Nothing
                _HideGender = Nothing
                _HideHandDominance = Nothing
                _HidePrimaryInsurance = Nothing
                _HideSecondaryInsurance = Nothing
                _HideSSN = Nothing

                'Pranit 16 apr 2012
                _HideAddress = Nothing
                _HideAddress1 = Nothing
                _HideAddress2 = Nothing
                _HideAddress3 = Nothing

                _showState = Nothing
                _showCity = Nothing
                _showZip = Nothing

                _HidePhoto = Nothing
                _HideGlobalPeriod = Nothing

                _MinimizeStrip = Nothing
                _ShowAgeInDays = Nothing
                _IntuitCommunication = Nothing


                _AgeLimit = Nothing
                _IsPediatric = Nothing

                If IsNothing(dspatient) = False Then
                    dspatient.Clear()
                    dspatient.Tables.Clear()
                    dspatient.Dispose() : dspatient = Nothing

                End If
                If Not IsNothing(DishtblMedcatClr) Then
                    DishtblMedcatClr.Clear()
                    DishtblMedcatClr = Nothing
                End If

                CloseFrmList()

            End If

            ' Release unmanaged resources. If disposing is false, only the following code is executed.
            ' Note that this is not thread safe. Another thread could start disposing the object
            ' after the managed resources are disposed, but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be implemented by the client.

        End If
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

#Region "patient Portal"

    Public Sub ValidateSecureMessageIcon(IsPatientRegisteredOnPortal As Boolean)
        If IsPatientRegisteredOnPortal = False Then
            btnSendMsg.Image = gloUserControlLibrary.My.Resources.NONEIntuitSecureMessaging
        Else
            btnSendMsg.Image = gloUserControlLibrary.My.Resources.IntuitSecureMessaging
        End If
    End Sub

    Public Sub IsPatientregisteredOnportal()
        Dim clsPatientPortal As New clsgloPatientPortalEmail(gloEMRDatabase.DataBaseLayer.ConnectionString)
        If clsPatientPortal.IsPatientRegisteredOnPortal(PatID, False) = False Then
            btnSendMsg.Image = gloUserControlLibrary.My.Resources.NONEIntuitSecureMessaging
        Else
            btnSendMsg.Image = gloUserControlLibrary.My.Resources.IntuitSecureMessaging
        End If
        If clsPatientPortal.gblnUSEINTUITINTERFACE = False And clsPatientPortal.gblnIntuitCommunication = False Then
            btnSendMsg.Visible = False
        ElseIf clsPatientPortal.gblnPatientPortalEnabled = False And clsPatientPortal.gblnIntuitCommunication = False Then
            btnSendMsg.Visible = False
        End If
        clsPatientPortal = Nothing

    End Sub

#End Region



    Private Shared resUserControlLib As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("gloUserControlLibrary.Resources", GetType(gloUC_PatientStrip).Assembly)

    Private Sub GetMedicalCategoryImage(Optional ByVal dtMedCat As DataTable = Nothing)
        Dim oDB As New gloEMRDatabase.DataBaseLayer
        Dim dtMedColor As DataTable = Nothing
        Dim Caseid As Int64 = 0
        Dim DS As DataSet = Nothing
        Dim strcolor As String = ""
        Dim strborderColor As String = String.Empty
        Dim strbottompanelcolr As String = String.Empty
        Try

            With oDB

                Dim oPara As gloEMRDatabase.DBParameter
                .DBParametersCol.Clear()
                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.Structured
                oPara.Direction = ParameterDirection.Input
                oPara.Value = dtMedCat
                oPara.Name = "@tvpMedicalCategory"
                .DBParametersCol.Add(oPara)
                oPara = Nothing



                oPara = New gloEMRDatabase.DBParameter
                oPara.DataType = SqlDbType.BigInt
                oPara.Direction = ParameterDirection.Input
                oPara.Value = Convert.ToInt64(PatID)
                oPara.Name = "@PatientId"
                .DBParametersCol.Add(oPara)
                oPara = Nothing
            End With

            DS = oDB.GetDataSet("gsp_GetPatientMedicalCategoryColor") ''code change for bugid 83503
            If Not IsNothing(DS) Then
                If (DS.Tables.Count > 0) Then
                    dtMedColor = DS.Tables(0)
                End If
                Try

                    If (Not IsNothing(dtMedCat)) Then
                        If (DS.Tables.Count > 1) Then
                            If (DS.Tables(1).Rows.Count > 0) Then
                                Caseid = Convert.ToInt64(DS.Tables(1).Rows(0)(0))
                            End If

                        End If

                        If Caseid > 0 Then
                            Dim dg As DialogResult = MessageBox.Show("There is an active pregnancy case available for this patient. Would you like to close this case?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If dg = DialogResult.Yes Then
                                OpenSetupCases(Caseid)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    ex = Nothing
                End Try
            End If

            oDB.DBParametersCol.Clear()
            oDB.Dispose()
            oDB = Nothing

            If Not IsNothing(dtMedColor) Then
                If (dtMedColor.Rows.Count > 0) Then
                    strcolor = Convert.ToString(dtMedColor.Rows(0)("ImageColor"))
                    strborderColor = Convert.ToString(dtMedColor.Rows(0)("BorderColor"))
                    strbottompanelcolr = Convert.ToString(dtMedColor.Rows(0)("BottomPanelColor"))

                End If
            End If
            If (strcolor.Trim() <> "") Then
                For Each di As KeyValuePair(Of String, String) In sortedDict
                    If (di.Key.ToString().Contains(strcolor.Trim().Replace(" ", "_"))) Then
                        'Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("gloUserControlLibrary.Resources", GetType(gloUC_PatientStrip).Assembly)
                        pnlButton.BackgroundImage = CType(resUserControlLib.GetObject(Convert.ToString(di.Key), resourceCulture), Image)
                        pnlPatientDetail.BackgroundImage = CType(resUserControlLib.GetObject(Convert.ToString(di.Value), resourceCulture), Image)
                        lbl_BorderBOTTOM.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
                        lbl_BorderRIGHT.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
                        lbl_BorderLEFT.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
                        lbl_BorderTOP.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
                        label27.BackColor = System.Drawing.ColorTranslator.FromHtml(strborderColor)
                        pnlGlobalPeriod.BackColor = System.Drawing.ColorTranslator.FromHtml(strbottompanelcolr)
                        ' lbldt.BackColor = System.Drawing.ColorTranslator.FromHtml(strbottompanelcolr)
                        'temp = Nothing
                        Exit For
                    End If
                Next
            Else
                pnlButton.BackgroundImage = CType(resUserControlLib.GetObject(Convert.ToString("MedicalCategoryImages_5_TopOrange"), resourceCulture), Image)
                pnlPatientDetail.BackgroundImage = CType(resUserControlLib.GetObject(Convert.ToString("MedicalCategoryImages_5_BottomOrange"), resourceCulture), Image)
                lbl_BorderBOTTOM.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")
                lbl_BorderRIGHT.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")
                lbl_BorderLEFT.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")
                lbl_BorderTOP.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")
                label27.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")
                ' lbldt.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9D9685")

                pnlGlobalPeriod.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFEBBE")
                'temp = Nothing
            End If
            If (strcolor.Contains("Pink") Or strcolor.Contains("Red") Or strcolor.Contains("Violet") Or strcolor.Contains("Dark")) Then
                lblPtName.ForeColor = Color.White
                lblBorn.ForeColor = Color.White
                lblPtBorn.ForeColor = Color.White
                lblAgeString_New.ForeColor = Color.White
                lbl_Gender.ForeColor = Color.White
                lblCode.ForeColor = Color.White
                lblPtCode.ForeColor = Color.White
                lblPatGender.ForeColor = Color.White
                lbldt.ForeColor = Color.White

            Else
                lblPtName.ForeColor = Color.Black
                lblBorn.ForeColor = Color.Black
                lblPtBorn.ForeColor = Color.Black
                lblAgeString_New.ForeColor = Color.Black
                lbl_Gender.ForeColor = Color.Black
                lblCode.ForeColor = Color.Black
                lblPtCode.ForeColor = Color.Black
                lblPatGender.ForeColor = Color.Black
                lbldt.ForeColor = Color.Black
            End If
            If strcolor.Contains("Dark") Then
                SetLabelColorForDarkBlue(True)
            Else
                SetLabelColorForDarkBlue(False)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(dtMedColor) Then
                dtMedColor.Dispose()
                dtMedColor = Nothing
            End If
            If Not IsNothing(dtMedCat) Then
                dtMedCat.Dispose()
                dtMedCat = Nothing
            End If
            If Not IsNothing(DS) Then
                DS.Tables.Clear()
                DS.Dispose()
                DS = Nothing
            End If
        End Try
    End Sub
    Private Sub OpenSetupCases(ByVal CaseID As Int64) '' function added for bugid 83503

        Try

            gloGlobal.LoadFromAssembly.OpenSetupCases(PatID, CaseID, If(TypeOf (Me.ParentForm) Is Form, Me.ParentForm, Nothing))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally


        End Try
    End Sub

    Private Sub SetLabelColorForDarkBlue(ByVal blndrkblue As Boolean)
        If blndrkblue = True Then
            lblProviderCap.ForeColor = Color.White
            lblProvider.ForeColor = Color.White
            lblOccupationCap.ForeColor = Color.White
            lblOccupation.ForeColor = Color.White
            Label18.ForeColor = Color.White
            lblHandDominance.ForeColor = Color.White
            lblSecondaryInsuranceCap.ForeColor = Color.White
            lblSecondaryInsurance.ForeColor = Color.White
            lblPhoneCap.ForeColor = Color.White
            lblPhone.ForeColor = Color.White
            lblSSNCap.ForeColor = Color.White
            lblSSN.ForeColor = Color.White
            lblPrimaryInsuranceCap.ForeColor = Color.White
            lblPrimaryInsurance.ForeColor = Color.White
            lblPhNameCap.ForeColor = Color.White
            lblPhName.ForeColor = Color.White
            lblPhPhoneCap.ForeColor = Color.White
            lblPhPhone.ForeColor = Color.White
            lblPatientCellPhoneCap.ForeColor = Color.White
            lblPatientCellPhone.ForeColor = Color.White
            lblPhFaxCap.ForeColor = Color.White
            lblPhFax.ForeColor = Color.White
            Label25.ForeColor = Color.White
            lblReferrals.ForeColor = Color.White
            lblRefPhysicianCap.ForeColor = Color.White
            lblRefPhysician.ForeColor = Color.White
            lblPhName.ForeColor = Color.White
            lblPhNameCap.ForeColor = Color.White
            lblMedicalCategory.ForeColor = Color.White
            lblPharmacyAdd.ForeColor = Color.White
            lbl_Copay.ForeColor = Color.White
            lbl_PatBal.ForeColor = Color.White
            lbl_TotBal.ForeColor = Color.White
            lbl_Allergies.ForeColor = Color.White
            lblBadDebt.ForeColor = Color.White
            lbl_NxtAppt.ForeColor = Color.White
            lbl_Pmalrt.ForeColor = Color.White
            lbl_Emralrt.ForeColor = Color.White
            Label3.ForeColor = Color.White
            Label5.ForeColor = Color.White
            Label7.ForeColor = Color.White
            Label9.ForeColor = Color.White
            Label11.ForeColor = Color.White
            Label13.ForeColor = Color.White
            Label15.ForeColor = Color.White
            Label19.ForeColor = Color.White
            Label16.ForeColor = Color.White
            lbldt.ForeColor = Color.White
        Else
            lblProviderCap.ForeColor = Color.Black
            lblProvider.ForeColor = Color.Black
            lblOccupationCap.ForeColor = Color.Black
            lblOccupation.ForeColor = Color.Black
            Label18.ForeColor = Color.Black
            lblHandDominance.ForeColor = Color.Black
            lblSecondaryInsuranceCap.ForeColor = Color.Black
            lblSecondaryInsurance.ForeColor = Color.Black
            lblPhoneCap.ForeColor = Color.Black
            lblPhone.ForeColor = Color.Black
            lblSSNCap.ForeColor = Color.Black
            lblSSN.ForeColor = Color.Black
            lblPrimaryInsuranceCap.ForeColor = Color.Black
            lblPrimaryInsurance.ForeColor = Color.Black
            lblPhNameCap.ForeColor = Color.Black
            lblPhName.ForeColor = Color.Black
            lblPhPhoneCap.ForeColor = Color.Black
            lblPhPhone.ForeColor = Color.Black
            lblPatientCellPhoneCap.ForeColor = Color.Black
            lblPatientCellPhone.ForeColor = Color.Black
            lblPhFaxCap.ForeColor = Color.Black
            lblPhFax.ForeColor = Color.Black
            Label25.ForeColor = Color.Black
            lblReferrals.ForeColor = Color.Black
            lblRefPhysicianCap.ForeColor = Color.Black
            lblRefPhysician.ForeColor = Color.Black
            lblPhName.ForeColor = Color.Black
            lblPhNameCap.ForeColor = Color.Black
            lblMedicalCategory.ForeColor = Color.Black
            lblPharmacyAdd.ForeColor = Color.Black
            lbl_Copay.ForeColor = Color.Black
            lbl_PatBal.ForeColor = Color.Black
            lbl_TotBal.ForeColor = Color.Black
            lbl_Allergies.ForeColor = Color.Black
            lblBadDebt.ForeColor = Color.Black
            lbl_NxtAppt.ForeColor = Color.Black
            lbl_Pmalrt.ForeColor = Color.Black
            lbl_Emralrt.ForeColor = Color.Black
            Label3.ForeColor = Color.Black
            Label5.ForeColor = Color.Black
            Label7.ForeColor = Color.Black
            Label9.ForeColor = Color.Black
            Label11.ForeColor = Color.Black
            Label13.ForeColor = Color.Black
            Label15.ForeColor = Color.Black
            Label19.ForeColor = Color.Black
            Label16.ForeColor = Color.Black
            lbldt.ForeColor = Color.Black
        End If
    End Sub
    Private Sub getMedicalCategoryItems(ByRef oMedicalCategory As gloListControl.gloListControl, PatientID As Long)

        Dim oDB As New gloEMRDatabase.DataBaseLayer
        Dim dtMedicalCategory As DataTable = Nothing
        Dim _strSQL As New System.Text.StringBuilder()


        Try

            '16-Nov-15 Aniket: Resolving Bug #90666: gloEMR: Medical category: Application does not select particular medical category from available medical categories
            _strSQL.Append("SELECT     Distinct MedicalCategory_Mst.nMedicalCategoryID, MedicalCategory_Mst.sMedicalCategory ")
            _strSQL.Append("FROM         Patient_MedicalCategory INNER JOIN ")
            _strSQL.Append("MedicalCategory_Mst ON Patient_MedicalCategory.nMedicalCategoryID = MedicalCategory_Mst.nMedicalCategoryID ")
            _strSQL.Append("Where bIsActive = 1 And Patient_MedicalCategory.nPatientID = " & PatientID)

            dtMedicalCategory = oDB.GetDataTable_Query(_strSQL.ToString)
            'End If

            _strSQL.Clear()
            _strSQL = Nothing
            'strcatbuil.Clear()
            'strcatbuil = Nothing

            oDB.Dispose()
            oDB = Nothing

            If (Not IsNothing(dtMedicalCategory)) Then
                For Each dr As DataRow In dtMedicalCategory.Rows
                    oMedicalCategory.SelectedItems.Add(Convert.ToInt64(dr("nMedicalCategoryID")), Convert.ToString(dr("sMedicalCategory")))
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            If IsNothing(dtMedicalCategory) = False Then
                dtMedicalCategory.Dispose()
                dtMedicalCategory = Nothing
            End If
        End Try


    End Sub
    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim strbuil As New System.Text.StringBuilder()
        Dim dtMedCat As New DataTable()
        Dim dcPatId As New DataColumn("nPatientID")
        Dim dcMedCatID As New DataColumn("nMedicalCategoryId")
        dtMedCat.Columns.Add(dcPatId)
        dtMedCat.Columns.Add(dcMedCatID)

        If oMedicalCategory.SelectedItems.Count > 0 Then



            For i As Int16 = 0 To oMedicalCategory.SelectedItems.Count - 1
                strbuil.Append(oMedicalCategory.SelectedItems(i).Description)
                If i <> oMedicalCategory.SelectedItems.Count - 1 Then
                    strbuil.Append(", ")
                End If
                Dim dr As DataRow = dtMedCat.NewRow()
                dr("nPatientID") = Convert.ToInt64(PatID)
                dr("nMedicalCategoryId") = Convert.ToInt64(oMedicalCategory.SelectedItems(i).ID)
                dtMedCat.Rows.Add(dr)
            Next

        Else
            Dim dr As DataRow = dtMedCat.NewRow()
            dr("nPatientID") = Convert.ToInt64(PatID)
            dr("nMedicalCategoryId") = 0
            dtMedCat.Rows.Add(dr)

        End If

        GetMedicalCategoryImage(dtMedCat)
        lblMedicalCategory.Text = Convert.ToString(strbuil)
        ''  lnkMedicalCategory.Text = strbuil.ToString()
        Try
            If strCompMedCategory <> lblMedicalCategory.Text Then ''If change in Medical Category then refresh patient banner for other open forms also
                RefreshPatientBannerforOpenForms()
                strCompMedCategory = lblMedicalCategory.Text
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        strbuil.Clear()
        strbuil = Nothing
        'If Not IsNothing(ofrmList) Then
        '    ofrmList.Close()
        '    ofrmList.Dispose()
        '    ofrmList = Nothing
        'End If
        CloseFrmList()
        If IsNothing(dtMedCat) = False Then
            dtMedCat.Dispose()
            dtMedCat = Nothing
        End If
        If Not ofrmList Is Nothing Then
            ofrmList.Visible = False
        End If
    End Sub

    Private Sub RefreshPatientBannerforOpenForms()
        For Each frm As Form In Application.OpenForms
            If ((frm.Name <> "frmSplash") And (frm.Name <> "MainMenu") And (frm.Name <> "frmviewlistcontrol")) Then
                For Each ctrl As Control In frm.Controls
                    If (ctrl.GetType() Is GetType(gloUserControlLibrary.gloUC_PatientStrip)) Then
                        getPatientStripControl(ctrl)
                        GoTo endofouter
                    Else
                        If (ProcessControls(ctrl)) Then
                            GoTo endofouter
                        End If
                    End If

                Next
            End If
endofouter:
        Next
    End Sub
    Private Function ProcessControls(ByVal ctrlContainer As Control) As Boolean
        Dim blnpatstripfound As Boolean = False
        For Each ctrl As Control In ctrlContainer.Controls
            If TypeOf ctrl Is gloUserControlLibrary.gloUC_PatientStrip Then
                getPatientStripControl(ctrl)
                blnpatstripfound = True
                Return blnpatstripfound
            End If

            ' If the control has children, 
            ' recursively call this function 
            If ctrl.HasChildren Then
                ProcessControls(ctrl)
            End If
        Next
        Return False
    End Function
    Private Sub getPatientStripControl(PatStripctrl As Control) ''Finding Controls Inside PatientStrip Controls
        For Each Chchildctrl As Control In PatStripctrl.Controls


            SetColorForOtherOpenForm(Chchildctrl)

            If Chchildctrl.HasChildren Then
                getPatientStripControl(Chchildctrl)
            End If

        Next
    End Sub

    Private Sub SetColorForOtherOpenForm(Childchildctrl As Control) ''Setting Color of   PatientStrip Controls Which Found in Other Open Forms 

        Select Case Childchildctrl.Name
            Case "lblMedicalCategory"
                Childchildctrl.Text = lblMedicalCategory.Text
            Case "pnlButton"
                Childchildctrl.BackgroundImage = pnlButton.BackgroundImage
            Case "pnlPatientDetail"
                Childchildctrl.BackgroundImage = pnlPatientDetail.BackgroundImage
            Case "lbl_BorderBOTTOM",
             "lbl_BorderRIGHT",
             "lbl_BorderLEFT",
             "lbl_BorderTOP",
             "label27"
                Childchildctrl.BackColor = label27.BackColor

            Case "pnlGlobalPeriod"
                Childchildctrl.BackColor = pnlGlobalPeriod.BackColor

            Case "lblPtName",
            "lblBorn",
            "lblAgeString_New",
            "lblPtBorn",
            "lbl_Gender",
            "lblCode",
            "lblPtCode",
            "lblPatGender"
                Childchildctrl.ForeColor = lblPtBorn.ForeColor
            Case "lblProviderCap",
            "lblProvider",
            "lblOccupationCap",
           "lblOccupation",
            "Label18",
            "lblHandDominance",
            "lblSecondaryInsuranceCap",
            "lblSecondaryInsurance",
            "lblPhoneCap",
            "lblPhone",
            "lblSSNCap",
            "lblSSN",
            "lblPrimaryInsuranceCap",
            "lblPrimaryInsurance",
            "lblPhNameCap",
            "lblPhName",
            "lblPhPhoneCap",
            "lblPhPhone",
            "lblPatientCellPhoneCap",
            "lblPatientCellPhone",
            "lblPhFaxCap",
            "lblPhFax",
            "Label25",
            "lblReferrals",
            "lblRefPhysicianCapc",
            "lblRefPhysician",
            "lblPhName",
            "lblPhNameCap",
            "lblMedicalCategory",
            "lblPharmacyAdd",
            "lbl_Copay",
            "lbl_PatBal",
            "lbl_TotBal",
            "lblBadDebt",
            "lbl_Allergies",
            "lbl_NxtAppt",
            "lbl_Pmalrt",
            "lbl_Emralrt",
            "Label19",
            "Label15",
            "Label13",
            "Label11",
            "Label9",
             "Label7",
             "Label5",
          "Label3"
                Childchildctrl.ForeColor = lblProviderCap.ForeColor


        End Select

    End Sub


    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)

        CloseFrmList()
    End Sub
    Private Sub CloseFrmList()
        Try
            If (IsNothing(oMedicalCategory) = False) Then
                RemoveHandler oMedicalCategory.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                RemoveHandler oMedicalCategory.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                Try
                    If IsNothing(ofrmList) = False Then
                        ofrmList.Controls.Remove(oMedicalCategory)
                    End If

                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try

        Try
            If (IsNothing(oMedicalCategory) = False) Then
                oMedicalCategory.Dispose()
                oMedicalCategory = Nothing
            End If
        Catch ex As Exception

        End Try
        Try
            If IsNothing(ofrmList) = False Then
                ofrmList.Close()
                ofrmList.Dispose()
                ofrmList = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub lnkMedicalCategory_Click(sender As System.Object, e As System.EventArgs) Handles lnkMedicalCategory.Click
        Try

            '' _IsSampledBy = True
            'SLR: 11/18 Added for memory leakage
            CloseFrmList()

            ofrmList = New frmViewListControl
            ofrmList.Icon = My.Resources.MedicalCategory
            ofrmList.Text = "Medical Category"
            oMedicalCategory = New gloListControl.gloListControl(gloEMRDatabase.DataBaseLayer.ConnectionString, gloListControl.gloListControlType.MedicalCategory, True, ofrmList.Width)
            oMedicalCategory.ControlHeader = "Medical Category"

            AddHandler oMedicalCategory.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oMedicalCategory.ItemClosedClick, AddressOf oListUsers_ItemClosedClick

            ofrmList.Controls.Add(oMedicalCategory)
            oMedicalCategory.Dock = DockStyle.Fill
            oMedicalCategory.BringToFront()
            If (lblMedicalCategory.Text.Trim() <> "") Then
                getMedicalCategoryItems(oMedicalCategory, PatID)
            End If
            oMedicalCategory.OpenControl()
            oMedicalCategory.ShowHeaderPanel(False)

            ofrmList.ShowDialog(IIf(IsNothing(ofrmList.Parent), Me, ofrmList.Parent))
            'SLR: rewritten as reusable component
            CloseFrmList()

        Catch ex As Exception
            MessageBox.Show("Error on UserListControl" & ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btn_EmrAlerts_Click(sender As System.Object, e As System.EventArgs) Handles btn_EmrAlerts.Click
        ShowEMRAlerts()
    End Sub

    Private Sub ShowEMRAlerts()

        Dim frmCallingForm As Object

        Try

            gloGlobal.LoadFromAssembly.frmPatientAlerts(PatID)

            FillPatientEMRAlert(PatID)

            frmCallingForm = Me.ParentForm

            If IsNothing(frmCallingForm) = False Then

                If IsNothing(frmCallingForm.GetType().GetMethod("GetdataFromOtherForms")) = False Then
                    '5-Oct-15 Aniket: Refresh Patient Alerts Liquid Link
                    frmCallingForm.GetdataFromOtherForms(48)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())

        Finally

        End Try

    End Sub


    Private Sub Label15_Click(sender As System.Object, e As System.EventArgs) Handles Label15.Click

    End Sub

    Private Sub btn_Pm_Alert_Click(sender As System.Object, e As System.EventArgs) Handles btn_Pm_Alert.Click
        OpenPMAlert(PatID)
    End Sub
    Private Sub OpenPMAlert(ByVal PatientID As Int64) ''added for new functionality for PM Alert


        Try
            gloGlobal.LoadFromAssembly.OpenPMAlert(gloEMRDatabase.DataBaseLayer.ConnectionString, PatID)


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally


        End Try
    End Sub

End Class

Public Class AgeDetail
    Public Age As String
    Public Years As Int16
    Public Months As Int16
    Public Days As Int16
    Public Hours As Int64 ''[PEDETRIC SETTIINGS] _BIRTH TIME  

    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
