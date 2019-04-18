Imports System.Data.SqlClient
Public Class clsSettings
#Region "Private Variables"
    'code added by supriya 25/7/2008
    Private m_RxDeclarationText As String
    'CCHIT 08
    Private m_RxFooterNote As String
    'CCHIT 08

    Dim _tmAppointmentStartTime As DateTime
    Dim _tmAppointmentEndTime As DateTime
    Dim _nAppointmentInterval As Int16
    Dim _nPULLCHARTSInterval As Int16
    Dim _nNoOfAttempts As Int16
    Dim m_FAXCompression As String = ""
    Dim m_HPIEnabled As Boolean = False

    Dim m_getMic As Boolean = False
    Dim m_MCIRReportPath As String = ""

    Dim m_AdvancedGrowthChart As Boolean = False ''sudhir 20081126

    Dim m_LocationAddress As Boolean = False

    '<Vinayak>
    Dim m_OMRCategory_History As String
    Dim m_OMRCategory_ROS As String
    Dim m_OMRCategory_PatientRegistration As String
    Dim m_DMSCategory_PatientDirective As String
    Dim m_DMSCategory_Labs As String
    '<Vinayak>

    'CCD file path
    Dim m_CCDFilePath As String = ""
    '''' Pramod
    Dim m_DMSCategory_Radiology As String
    'Supriya
    Dim m_ClinicDISettings As String

    Dim m_NoOfAttempts As Integer = 0

    '' Mahesh 20070723
    Dim _blnRecordLevelLocking As Boolean

    '''''Declared by Anil on 20071119
    Dim _blnAutoPatientCode As Boolean
    '''''''''''''''

    'sarika internet fax 
    Private _blnInternetFax As Boolean = False
    'eFax Login Key
    Private _seFaxUserID As String = ""
    Private _seFaxUserPassword As String = ""
    'sarika internet fax 
    Private _IsSurescriptEnabled As Boolean
    Private _IsStagingServer As Boolean
    Private _SignatureText As String
    Private _CoSignatureText As String



    'sarika for gloEAR
    Private _eARSenderID As String = ""
    Private _eARSenderParticipantPassword As String = ""
    Private _eARDownloadDirectory As String = ""

#End Region
    Private gstrdatabaseConnectionString As String = ""


    Public Sub New(ByVal connstring As String)
        gstrdatabaseConnectionString = connstring
    End Sub


#Region "Public Properties"
    'CCD file path
    Public Property CCDFilePath() As String
        Get
            Return m_CCDFilePath
        End Get
        Set(ByVal Value As String)
            m_CCDFilePath = Value
        End Set
    End Property
    Public Property AppointmentStartTime() As DateTime
        Get
            Return _tmAppointmentStartTime
        End Get
        Set(ByVal Value As DateTime)
            _tmAppointmentStartTime = Value
        End Set
    End Property
    Public Property AppointmentEndTime() As DateTime
        Get
            Return _tmAppointmentEndTime
        End Get
        Set(ByVal Value As DateTime)
            _tmAppointmentEndTime = Value
        End Set
    End Property
    Public Property AppointmentInterval() As Int16
        Get
            Return _nAppointmentInterval
        End Get
        Set(ByVal Value As Int16)
            _nAppointmentInterval = Value
        End Set
    End Property
    Public Property PULLCHARTSInterval() As Int16
        Get
            Return _nPULLCHARTSInterval
        End Get
        Set(ByVal Value As Int16)
            _nPULLCHARTSInterval = Value
        End Set
    End Property
    Public Property FAXNoOfAttempts() As Int16
        Get
            Return _nNoOfAttempts
        End Get
        Set(ByVal Value As Int16)
            _nNoOfAttempts = Value
        End Set
    End Property
    Public Property FAXCompression() As String
        Get
            If Trim(m_FAXCompression) <> "" Then
                Return m_FAXCompression
            Else
                Return "CCITT G3"
            End If

        End Get
        Set(ByVal Value As String)
            m_FAXCompression = Value
        End Set
    End Property

    Public Property HPIEnabled() As Boolean
        Get
            Return m_HPIEnabled
        End Get
        Set(ByVal Value As Boolean)
            m_HPIEnabled = Value
        End Set
    End Property


    Public Property getMic() As Boolean
        Get
            Return m_getMic
        End Get
        Set(ByVal Value As Boolean)
            m_getMic = Value
        End Set
    End Property


    Public Property MCIRReportPath() As String
        Get
            Return m_MCIRReportPath
        End Get
        Set(ByVal Value As String)
            m_MCIRReportPath = Value
        End Set
    End Property

    Public Property ShowAdvancedGrowthChart() As Boolean
        Get
            Return m_AdvancedGrowthChart
        End Get
        Set(ByVal Value As Boolean)
            m_AdvancedGrowthChart = Value
        End Set
    End Property

    Public Property LocationAddress() As Boolean
        Get
            Return m_LocationAddress
        End Get
        Set(ByVal Value As Boolean)
            m_LocationAddress = Value
        End Set
    End Property

    '<Vinayak>
    Public Property OMRCategory_History() As String
        Get
            Return m_OMRCategory_History
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_History = Value
        End Set
    End Property

    Public Property OMRCategory_ROS() As String
        Get
            Return m_OMRCategory_ROS
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_ROS = Value
        End Set
    End Property
    Public Property OMRCategory_PatientRegistration() As String
        Get
            Return m_OMRCategory_PatientRegistration
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_PatientRegistration = Value
        End Set
    End Property

    Public Property DMSCategory_PatientDirective() As String
        Get
            Return m_DMSCategory_PatientDirective
        End Get
        Set(ByVal Value As String)
            m_DMSCategory_PatientDirective = Value
        End Set
    End Property

    Public Property DMSCategory_Labs() As String
        Get
            Return m_DMSCategory_Labs
        End Get
        Set(ByVal value As String)
            m_DMSCategory_Labs = value
        End Set
    End Property

    Public Property DMSCategory_Radiology() As String
        Get
            Return m_DMSCategory_Radiology
        End Get
        Set(ByVal value As String)
            m_DMSCategory_Radiology = value
        End Set
    End Property
    '''' Sarika - 20070212
    Public Property NoOfAttempts() As Integer
        Get
            Return m_NoOfAttempts
        End Get
        Set(ByVal Value As Integer)
            m_NoOfAttempts = Value
        End Set
    End Property
    'Enable/Disable Drug Interaction for spcific machine
    Public Property ClinicDISettings() As String
        Get
            Return m_ClinicDISettings
        End Get
        Set(ByVal Value As String)
            m_ClinicDISettings = Value
        End Set
    End Property
    '<Vinayak>

    'Mahesh 20070723 - Record Level Locking 
    Public Property RecordLevelLocking() As Boolean
        Get
            Return _blnRecordLevelLocking
        End Get
        Set(ByVal value As Boolean)
            _blnRecordLevelLocking = value
        End Set
    End Property

    ''''''''''''''''''''''Property Added by Anil 0n 20071119
    Public Property AutoGeneratePatientCode() As Boolean
        Get
            Return _blnAutoPatientCode
        End Get
        Set(ByVal value As Boolean)
            _blnAutoPatientCode = value
        End Set
    End Property
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    'sarika internet fax
    Public Property InternetFax() As Boolean
        Get
            Return _blnInternetFax
        End Get
        Set(ByVal value As Boolean)
            _blnInternetFax = value
        End Set
    End Property


    'eFax Login Key
    Public Property eFaxUserID() As String
        Get
            Return _seFaxUserID
        End Get
        Set(ByVal value As String)
            _seFaxUserID = value
        End Set
    End Property

    Public Property eFaxUserPassword() As String
        Get
            Return _seFaxUserPassword
        End Get
        Set(ByVal value As String)
            _seFaxUserPassword = value
        End Set
    End Property

    Public Property IsSurescriptEnabled() As Boolean
        Get
            Return _IsSurescriptEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsSurescriptEnabled = value
        End Set
    End Property
    Public Property IsStagingServer() As Boolean
        Get
            Return _IsStagingServer
        End Get
        Set(ByVal value As Boolean)
            _IsStagingServer = value
        End Set
    End Property
    'code addded by supriya 25/7/2008
    Public Property RxDeclarationText() As String
        Get
            Return m_RxDeclarationText
        End Get
        Set(ByVal Value As String)
            m_RxDeclarationText = Value
        End Set
    End Property
    'code added by supriya 25/7/2008

    'CCHIT 08
    Public Property RxFooterNote() As String
        Get
            Return m_RxFooterNote
        End Get
        Set(ByVal Value As String)
            m_RxFooterNote = Value
        End Set
    End Property

    'CCHIT 08
    Public Property SignatureText() As String
        Get
            Return _SignatureText
        End Get
        Set(ByVal value As String)
            _SignatureText = value
        End Set
    End Property

    Public Property CoSignatureText() As String
        Get
            Return _CoSignatureText
        End Get
        Set(ByVal value As String)
            _CoSignatureText = value
        End Set
    End Property

    'sarika internet fax


    'sarika EAR Report
    Public Property eARSenderID() As String
        Get
            Return _eARSenderID
        End Get
        Set(ByVal value As String)
            _eARSenderID = value
        End Set
    End Property

    Public Property eARSenderParticipantPassword() As String
        Get
            Return _eARSenderParticipantPassword
        End Get
        Set(ByVal value As String)
            _eARSenderParticipantPassword = value
        End Set
    End Property

    Public Property eARDownloadDirectory() As String
        Get
            Return _eARDownloadDirectory
        End Get
        Set(ByVal value As String)
            _eARDownloadDirectory = value
        End Set
    End Property
    '--

#End Region

    'Public Sub GetSettings()
    '    Try
    '        Dim objCon As New SqlConnection
    '        objCon.ConnectionString = gstrdatabaseConnectionString
    '        Dim objCmd As New SqlCommand
    '        ' Dim objSQLDataReader As SqlDataReader
    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "sp_FillSettings"
    '        objCmd.Connection = objCon
    '        objCmd.Connection = objCon
    '        objCon.Open()
    '        Dim objDA As New SqlDataAdapter(objCmd)
    '        Dim dtTable As New DataTable
    '        objDA.Fill(dtTable)
    '        objCon.Close()
    '        objCon = Nothing
    '        Dim nCount As Integer
    '        For nCount = 0 To dtTable.Rows.Count - 1

    '            Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
    '                Case "Clinic Start Time".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _tmAppointmentStartTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
    '                    End If
    '                Case "Clinic Closing Time".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _tmAppointmentEndTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
    '                    End If
    '                Case "Appointment Interval".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _nAppointmentInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
    '                    End If
    '                Case "Pull Chart Interval".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _nPULLCHARTSInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
    '                    End If
    '                Case "Max FAX Retries".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _nNoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Int16)
    '                    End If
    '                Case "FAX Compression".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_FAXCompression = dtTable.Rows(nCount).Item(2)
    '                    Else
    '                        m_FAXCompression = "CCITT G3"
    '                    End If
    '                Case "HPI".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
    '                            m_HPIEnabled = False
    '                        Else
    '                            m_HPIEnabled = True
    '                        End If
    '                    Else
    '                        m_HPIEnabled = False
    '                    End If
    '                Case "Pull Address".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
    '                            m_LocationAddress = False
    '                        Else
    '                            m_LocationAddress = True
    '                        End If
    '                    Else
    '                        m_LocationAddress = False
    '                    End If
    '                Case "OMR Category - History".ToUpper '- History
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_OMRCategory_History = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "OMR Category - ROS".ToUpper '- ROS
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_OMRCategory_ROS = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "OMR Category - Patient Registration".ToUpper '- Patient Registration
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_OMRCategory_PatientRegistration = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "OMR Category - Directive".ToUpper '- Patient Directive
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_DMSCategory_PatientDirective = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "Lab Category".ToUpper '- Labs
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_DMSCategory_Labs = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "Radiology Category".ToUpper '-Radiology
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_DMSCategory_Radiology = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If
    '                Case "RxReportPath".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        gstrRxReportpath = CType(dtTable.Rows(nCount).Item(2), String)
    '                    End If

    '                Case "No. Of. Attempts".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_NoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Integer)
    '                    Else
    '                        m_NoOfAttempts = 0
    '                    End If
    '                Case "Clinic DI Settings".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_ClinicDISettings = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        m_ClinicDISettings = 0
    '                    End If
    '                Case "Record Level Locking".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _blnRecordLevelLocking = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        _blnRecordLevelLocking = False
    '                    End If
    '                Case "Threshold Value".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        gnThresholdSetting = CType(dtTable.Rows(nCount).Item(2), Int32)
    '                    Else
    '                        gnThresholdSetting = 420
    '                    End If
    '                    '''''''Code added by Anil on 20071119
    '                Case "Auto-Generate Patient Code".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
    '                            _blnAutoPatientCode = False
    '                        Else
    '                            _blnAutoPatientCode = True
    '                        End If
    '                    Else
    '                        _blnAutoPatientCode = False
    '                    End If
    '                    ''''''''''''''''''''''''''''''''''''''''''''''''''''

    '                    'sarika internet fax
    '                Case "InternetFax".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _blnInternetFax = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        _blnInternetFax = False
    '                    End If

    '                    'eFax Login Key
    '                Case "eFax User ID".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _seFaxUserID = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        _seFaxUserID = ""
    '                    End If
    '                Case "eFax User Password".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _seFaxUserPassword = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        _seFaxUserPassword = ""
    '                    End If

    '                    'sarika internet fax
    '                Case "Surescript Enabled".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _IsSurescriptEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        _IsSurescriptEnabled = False
    '                    End If
    '                Case "StagingServer".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _IsStagingServer = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        _IsStagingServer = False
    '                    End If
    '                    'code added by supriya 25/7/2008
    '                Case UCase("RXDECLARATION")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_RxDeclarationText = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        m_RxDeclarationText = ""
    '                    End If
    '                    'code added by supriya 11/7/2008

    '                    'CCHIT 08
    '                    'Case UCase("RXFOOTERNOTE")
    '                    '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                    '        m_RxFooterNote = CType(dtTable.Rows(nCount).Item(2), String)
    '                    '    Else
    '                    '        m_RxFooterNote = ""
    '                    '    End If
    '                    'CCHIT 08
    '                Case "GENERATEMIC".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_getMic = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        m_getMic = False
    '                    End If

    '                Case "MCIR REPORT PATH".ToUpper
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_MCIRReportPath = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        m_MCIRReportPath = False
    '                    End If
    '                    '' Advanced Growth Chart
    '                Case UCase("Advanced Growth Chart")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_AdvancedGrowthChart = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        m_AdvancedGrowthChart = False
    '                    End If
    '                    'CCD file path
    '                Case UCase("CCD File PATH")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        m_CCDFilePath = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        m_CCDFilePath = ""
    '                    End If
    '                Case UCase("SIGNATURETEXT")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _SignatureText = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        _SignatureText = ""
    '                    End If
    '                Case UCase("COSIGNATURETEXT")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        _CoSignatureText = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        _CoSignatureText = False
    '                    End If
    '                Case UCase("EM CHIEFCOMPLAINT TYPE")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        gstrChiefComplaintType = CType(dtTable.Rows(nCount).Item(2), String)
    '                    Else
    '                        gstrChiefComplaintType = "ChiefComplaint"
    '                    End If

    '                    'Shubhangi 20090306'
    '                Case UCase("OtherPatientType")
    '                    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
    '                        gbOtherPatientType = CType(dtTable.Rows(nCount).Item(2), Boolean)
    '                    Else
    '                        gbOtherPatientType = False
    '                    End If

    '                    'End Shubhangi'
    '            End Select
    '        Next

    '    Catch ex As Exception
    '        MessageBox.Show("Unable to get all settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub

    Public Sub GetSettings_EAR()
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gstrdatabaseConnectionString
            Dim objCmd As New SqlCommand
            ' Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "sp_FillSettings"
            objCmd.Connection = objCon
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objCon.Close()
            objCon = Nothing
            ' Dim nCount As Integer

            _eARSenderID = "T00000000020315"
            _eARSenderParticipantPassword = "FXTXGJVZ0W"
            _eARDownloadDirectory = "I:\gloData"


            'For nCount = 0 To dtTable.Rows.Count - 1

            '    Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
            '        Case UCase("eARSenderID")
            '            If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '                _eARSenderID = CType(dtTable.Rows(nCount).Item(2), String)
            '            Else
            '                _eARSenderID = ""
            '            End If
            '        Case UCase("eARSenderParticipantPassword")
            '            If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '                _eARSenderParticipantPassword = CType(dtTable.Rows(nCount).Item(2), String)
            '            Else
            '                _eARSenderParticipantPassword = ""
            '            End If
            '        Case UCase("eARDirectory")
            '            If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '                _eARDownloadDirectory = CType(dtTable.Rows(nCount).Item(2), String)
            '            Else
            '                _eARDownloadDirectory = ""
            '            End If

            '            'Case "Clinic Start Time".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _tmAppointmentStartTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
            '            '    End If
            '            'Case "Clinic Closing Time".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _tmAppointmentEndTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
            '            '    End If
            '            'Case "Appointment Interval".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _nAppointmentInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
            '            '    End If
            '            'Case "Pull Chart Interval".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _nPULLCHARTSInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
            '            '    End If
            '            'Case "Max FAX Retries".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _nNoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Int16)
            '            '    End If
            '            'Case "FAX Compression".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_FAXCompression = dtTable.Rows(nCount).Item(2)
            '            '    Else
            '            '        m_FAXCompression = "CCITT G3"
            '            '    End If
            '            'Case "HPI".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
            '            '            m_HPIEnabled = False
            '            '        Else
            '            '            m_HPIEnabled = True
            '            '        End If
            '            '    Else
            '            '        m_HPIEnabled = False
            '            '    End If
            '            'Case "Pull Address".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
            '            '            m_LocationAddress = False
            '            '        Else
            '            '            m_LocationAddress = True
            '            '        End If
            '            '    Else
            '            '        m_LocationAddress = False
            '            '    End If
            '            'Case "OMR Category - History".ToUpper '- History
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_OMRCategory_History = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "OMR Category - ROS".ToUpper '- ROS
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_OMRCategory_ROS = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "OMR Category - Patient Registration".ToUpper '- Patient Registration
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_OMRCategory_PatientRegistration = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "OMR Category - Directive".ToUpper '- Patient Directive
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_DMSCategory_PatientDirective = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "Lab Category".ToUpper '- Labs
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_DMSCategory_Labs = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "Radiology Category".ToUpper '-Radiology
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_DMSCategory_Radiology = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If
            '            'Case "RxReportPath".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        gstrRxReportpath = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    End If

            '            'Case "No. Of. Attempts".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_NoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Integer)
            '            '    Else
            '            '        m_NoOfAttempts = 0
            '            '    End If
            '            'Case "Clinic DI Settings".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_ClinicDISettings = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        m_ClinicDISettings = 0
            '            '    End If
            '            'Case "Record Level Locking".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _blnRecordLevelLocking = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        _blnRecordLevelLocking = False
            '            '    End If
            '            'Case "Threshold Value".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        gnThresholdSetting = CType(dtTable.Rows(nCount).Item(2), Int32)
            '            '    Else
            '            '        gnThresholdSetting = 420
            '            '    End If
            '            '    '''''''Code added by Anil on 20071119
            '            'Case "Auto-Generate Patient Code".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
            '            '            _blnAutoPatientCode = False
            '            '        Else
            '            '            _blnAutoPatientCode = True
            '            '        End If
            '            '    Else
            '            '        _blnAutoPatientCode = False
            '            '    End If
            '            '    ''''''''''''''''''''''''''''''''''''''''''''''''''''

            '            '    'sarika internet fax
            '            'Case "InternetFax".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _blnInternetFax = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        _blnInternetFax = False
            '            '    End If

            '            '    'eFax Login Key
            '            'Case "eFax User ID".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _seFaxUserID = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        _seFaxUserID = ""
            '            '    End If
            '            'Case "eFax User Password".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _seFaxUserPassword = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        _seFaxUserPassword = ""
            '            '    End If

            '            '    'sarika internet fax
            '            'Case "Surescript Enabled".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _IsSurescriptEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        _IsSurescriptEnabled = False
            '            '    End If
            '            'Case "StagingServer".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        _IsStagingServer = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        _IsStagingServer = False
            '            '    End If
            '            'code added by supriya 25/7/2008
            '            'Case UCase("RXDECLARATION")
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        RxDeclaration = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        RxDeclaration = ""
            '            '    End If
            '            'code added by supriya 11/7/2008

            '            'CCHIT 08
            '            'Case UCase("RXFOOTERNOTE")
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_RxFooterNote = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        m_RxFooterNote = ""
            '            '    End If
            '            '    'CCHIT 08
            '            'Case "GENERATEMIC".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_getMic = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        m_getMic = False
            '            '    End If

            '            'Case "MCIR REPORT PATH".ToUpper
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_MCIRReportPath = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        m_MCIRReportPath = False
            '            '    End If
            '            '    '' Advanced Growth Chart
            '            'Case UCase("Advanced Growth Chart")
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_AdvancedGrowthChart = CType(dtTable.Rows(nCount).Item(2), Boolean)
            '            '    Else
            '            '        m_AdvancedGrowthChart = False
            '            '    End If
            '            '    'CCD file path
            '            'Case UCase("CCD File PATH")
            '            '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
            '            '        m_CCDFilePath = CType(dtTable.Rows(nCount).Item(2), String)
            '            '    Else
            '            '        m_CCDFilePath = ""
            '            '    End If
            '    End Select
            'Next

        Catch ex As Exception
            'MessageBox.Show("Unable to get all settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Throw ex
        End Try
    End Sub

    Public Sub UpdateEARDirectory(ByVal sEARDirectory As String)
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gstrdatabaseConnectionString
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "sp_UpdateSettings"
            Dim objParaSettingsName As New SqlParameter
            Dim objParaSettingsValue As New SqlParameter

            Dim objParaSettingsClinicID As New SqlParameter
            Dim objParaSettingsUserID As New SqlParameter
            Dim objParaSettingsUserClinicFlag As New SqlParameter

            objCmd.Connection = objCon

            objCon.Open()
            'Clinic Working Time
            ''Sandip Darade 20090417
            '1.	Clinic Start Time
            '2.	Clinic Closing Time
            'Replace this setting internal name (database value) with
            '1. ClinicStartTime
            '2. ClinicEndTime
            objCmd.Parameters.Clear()
            With objParaSettingsName
                .ParameterName = "@SettingsName"
                '.Value = "Clinic Start Time"
                .Value = "eARDirectory"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSettingsName)

            With objParaSettingsValue
                .ParameterName = "@SettingsValue"
                .Value = sEARDirectory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSettingsValue)
            'Sandip Darade 20090420
            ''Add ClinicID, UserID,UserClinicFlag
            With objParaSettingsClinicID
                .ParameterName = "@nClinicID"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaSettingsClinicID)

            With objParaSettingsUserID
                .ParameterName = "@nUserID"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaSettingsUserID)

            With objParaSettingsUserClinicFlag
                .ParameterName = "@nUserClinicFlag"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
            '' End Add ClinicID, UserID,UserClinicFlag
            objCmd.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

  
End Class
