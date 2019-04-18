Imports gloCCDExchange
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Xml
Imports gloCCDSchema




Partial Public Class gloCDAWriter
    Implements IDisposable
    Dim _CCDFilePath As String
    Dim _PracticeID As String = ""
    Dim _IntuitPatientID As Int64 = 0
    Dim _IntuitProviderID As Int64 = 0
    Dim _PatientID As Int64 = 0
    Dim _PracticeProviderID As Int64 = 0
    Dim _IsIntuit As Boolean = False
    Public Property CCDFilePath() As String
        Get
            Return _CCDFilePath
        End Get
        Set(ByVal value As String)
            _CCDFilePath = value
        End Set
    End Property
    Public Property PracticeID() As String
        Get
            Return _PracticeID
        End Get
        Set(ByVal value As String)
            _PracticeID = value
        End Set
    End Property
    Public Property IntuitPatientID() As Int64
        Get
            Return _IntuitPatientID
        End Get
        Set(ByVal value As Int64)
            _IntuitPatientID = value
        End Set
    End Property
    Public Property IntuitProviderID() As Int64
        Get
            Return _IntuitProviderID
        End Get
        Set(ByVal value As Int64)
            _IntuitProviderID = value
        End Set
    End Property
    Public Property PracticeProviderID() As Int64
        Get
            Return _PracticeProviderID
        End Get
        Set(ByVal value As Int64)
            _PracticeProviderID = value
        End Set
    End Property

    Public Property PatientID() As Int64
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Int64)
            _PatientID = value
        End Set
    End Property
    Public Property IsIntuit() As Boolean
        Get
            Return _IsIntuit
        End Get
        Set(ByVal value As Boolean)
            _IsIntuit = value
        End Set
    End Property
    Public Function GenerateCCDExchange(ByVal PatientLastName As String) As String
        Dim strfilepath As String = ""
        Dim CCDExchangeDoc As New CcdExchange()


        Try

            strfilepath = GenerateFileName(PatientLastName)
            strfilepath = strfilepath.Replace("CDA_", "INTUITCDA_")

            'CCD Message Header
            CCDExchangeDoc = GetCCDExchangeInitialization()

            CCDExchangeDoc.PatientDemographics = GetPatientDemographics()

            CCDExchangeDoc.PracticeInformation = GetPracticeInfo()

            CCDExchangeDoc.Ccd = GetCCDData()

            Try
                gloSerialization.SetCCDExchange(strfilepath, CCDExchangeDoc, "")

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                ex = Nothing
                Return ""
            End Try
            If _msgString <> "" Then
                'MessageBox.Show(_msgString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(_msgString, False)
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            'If Not IsNothing(QRDAIDoc.recordTarget(0)) Then
            '    QRDAIDoc.recordTarget(0) = Nothing
            'End If
            'If Not IsNothing(QRDAIDoc.author(0)) Then
            '    QRDAIDoc.author(0) = Nothing
            'End If
            'If Not IsNothing(QRDAIDoc.custodian) Then
            '    QRDAIDoc.custodian = Nothing
            'End If
            'If Not IsNothing(QRDAIDoc.component) Then
            '    QRDAIDoc.component = Nothing
            'End If
            'If Not IsNothing(QRDAIDoc) Then
            '    QRDAIDoc = Nothing
            'End If
            'If Not IsNothing(oCodingSystemMaster) Then
            '    oCodingSystemMaster.Dispose()
            '    oCodingSystemMaster = Nothing
            'End If
            'If Not IsNothing(oTemplateIDMaster) Then
            '    oTemplateIDMaster.Dispose()
            '    oTemplateIDMaster = Nothing
            'End If
            CCDExchangeDoc = Nothing
        End Try

        Return strfilepath
    End Function

    Private Function GetCCDExchangeInitialization() As gloCCDExchange.CcdExchange
        Try


            Dim CCDExchangeDoc As New CcdExchange
            ''CCDExchangeDoc.SchemaLocation = "http://schema.intuit.com/health/ccd/v1"
            CCDExchangeDoc.CcdMessageHeaders = New CcdMessageHeaders
            ''Sender
            CCDExchangeDoc.CcdMessageHeaders.Sender = New Device
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceName = ""
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceVersion = ""
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceLocalTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceLocalTime = DateTime.Now.Date
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceUTCTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Sender.deviceUTCTime = DateTime.UtcNow.Date
            CCDExchangeDoc.CcdMessageHeaders.Sender.vendorName = ""
            ''DeviceArguments
            CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments = New NameValueMap(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0) = New NameValueMap
            CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0).KeyValuePair = New NameValueMapKeyValuePair(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0).KeyValuePair(0) = New NameValueMapKeyValuePair
            CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0).KeyValuePair(0).Key = "DataSource"
            If _PracticeID <> "" Then
                CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0).KeyValuePair(0).Value = _PracticeID
            Else
                CCDExchangeDoc.CcdMessageHeaders.Sender.DeviceArguments(0).KeyValuePair(0).Value = ""
            End If



            ''Partner
            CCDExchangeDoc.CcdMessageHeaders.Partner = New Device
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceName = ""
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceVersion = ""
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceLocalTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceLocalTime = DateTime.Now.Date
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceUTCTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Partner.deviceUTCTime = DateTime.Now.Date
            CCDExchangeDoc.CcdMessageHeaders.Partner.vendorName = ""
            ''DeviceArguments
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments = New NameValueMap(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments(0) = New NameValueMap
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments(0).KeyValuePair = New NameValueMapKeyValuePair(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments(0).KeyValuePair(0) = New NameValueMapKeyValuePair
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments(0).KeyValuePair(0).Key = "DataPartner"
            CCDExchangeDoc.CcdMessageHeaders.Partner.DeviceArguments(0).KeyValuePair(0).Value = "DataPartner name"


            ''Destination
            CCDExchangeDoc.CcdMessageHeaders.Destination = New Device
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceName = ""
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceVersion = ""
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceLocalTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceLocalTime = DateTime.Now.Date
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceUTCTimeSpecified = True
            CCDExchangeDoc.CcdMessageHeaders.Destination.deviceUTCTime = DateTime.Now.Date
            CCDExchangeDoc.CcdMessageHeaders.Destination.vendorName = ""
            ''DeviceArguments
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments = New NameValueMap(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments(0) = New NameValueMap
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments(0).KeyValuePair = New NameValueMapKeyValuePair(0) {}
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments(0).KeyValuePair(0) = New NameValueMapKeyValuePair
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments(0).KeyValuePair(0).Key = "Destination"
            CCDExchangeDoc.CcdMessageHeaders.Destination.DeviceArguments(0).KeyValuePair(0).Value = "PORTAL"

            'Last Updated'
            'CCDExchangeDoc.CcdMessageHeaders.LastUpdated = New Date
            CCDExchangeDoc.CcdMessageHeaders.LastUpdated = DateTime.UtcNow.Date

            ''Message ID
            CCDExchangeDoc.CcdMessageHeaders.MessageId = Guid.NewGuid.ToString()

            ''Routing Map
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap = New NameValueMap
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair = New NameValueMapKeyValuePair(1) {}
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(0) = New NameValueMapKeyValuePair
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(0).Key = "Key1"
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(0).Value = "Value1"
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(1) = New NameValueMapKeyValuePair
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(1).Key = "Key2"
            CCDExchangeDoc.CcdMessageHeaders.RoutingMap.KeyValuePair(1).Value = "Value2"


            Return CCDExchangeDoc
        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "Initialization"
            Else
                _msgString = _msgString & vbNewLine & "Initialization"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing
        End Try
    End Function

    Private Function GetPatientDemographics() As PatientDemographics
        Dim _patientDemo As New PatientDemographics()
        '   Dim _CodeSysItem As CodeSystemItem = Nothing
        Dim _oCDADataExtraction As New gloCDADataExtraction

        Try

            ''PatientIdentifier
            _patientDemo.PatientIdentifier = New PatientIdentifiers
            If _PatientID <> 0 Then
                _patientDemo.PatientIdentifier.PracticePatientId = Convert.ToString(_PatientID)
            Else
                _patientDemo.PatientIdentifier.PracticePatientId = ""
            End If
            If _IntuitPatientID <> 0 Then
                _patientDemo.PatientIdentifier.IntuitPatientId = Convert.ToString(_IntuitPatientID)
            Else
                _patientDemo.PatientIdentifier.IntuitPatientId = ""
            End If


            _patientDemo.Name = New gloCCDExchange.PersonName
            _patientDemo.Name.ItemsElementName = New gloCCDExchange.ItemsChoiceType(4) {}
            _patientDemo.Name.ItemsElementName(0) = gloCCDExchange.ItemsChoiceType.Prefix
            _patientDemo.Name.ItemsElementName(1) = gloCCDExchange.ItemsChoiceType.FirstName
            _patientDemo.Name.ItemsElementName(2) = gloCCDExchange.ItemsChoiceType.MiddleName
            _patientDemo.Name.ItemsElementName(3) = gloCCDExchange.ItemsChoiceType.LastName
            _patientDemo.Name.ItemsElementName(4) = gloCCDExchange.ItemsChoiceType.Suffix

            _patientDemo.Name.Items = New String(4) {}
            If mPatient.PatientName.Prefix IsNot Nothing AndAlso mPatient.PatientName.Prefix <> "" Then
                _patientDemo.Name.Items(0) = mPatient.PatientName.Prefix
            Else
                _patientDemo.Name.Items(0) = ""
            End If
            If mPatient.PatientName.FirstName IsNot Nothing AndAlso mPatient.PatientName.FirstName <> "" Then
                _patientDemo.Name.Items(1) = mPatient.PatientName.FirstName
            Else
                _patientDemo.Name.Items(1) = ""
            End If
            If mPatient.PatientName.MiddleName IsNot Nothing AndAlso mPatient.PatientName.MiddleName <> "" Then
                _patientDemo.Name.Items(2) = mPatient.PatientName.MiddleName
            Else
                _patientDemo.Name.Items(2) = ""
            End If
            If mPatient.PatientName.LastName IsNot Nothing AndAlso mPatient.PatientName.LastName <> "" Then
                _patientDemo.Name.Items(3) = mPatient.PatientName.LastName
            Else
                _patientDemo.Name.Items(3) = ""
            End If
            If mPatient.PatientName.Suffix IsNot Nothing AndAlso mPatient.PatientName.Suffix <> "" Then
                _patientDemo.Name.Items(4) = mPatient.PatientName.Suffix
            Else
                _patientDemo.Name.Items(4) = ""
            End If





            ''Address
            _patientDemo.Address = New AddressInformation
            If mPatient.PatientName.PersonContactAddress.Street IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.Street <> "" Then
                _patientDemo.Address.Line1 = mPatient.PatientName.PersonContactAddress.Street
            Else
                _patientDemo.Address.Line1 = ""
            End If

            If mPatient.PatientName.PersonContactAddress.AddressLine2 IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.AddressLine2 <> "" Then
                _patientDemo.Address.Line2 = mPatient.PatientName.PersonContactAddress.AddressLine2
            Else
                _patientDemo.Address.Line2 = ""
            End If

            If mPatient.PatientName.PersonContactAddress.City IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.City <> "" Then
                _patientDemo.Address.City = mPatient.PatientName.PersonContactAddress.City
            Else
                _patientDemo.Address.City = ""
            End If
            If mPatient.PatientName.PersonContactAddress.State IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.State <> "" Then
                _patientDemo.Address.State = mPatient.PatientName.PersonContactAddress.State
            Else

                _patientDemo.Address.State = ""

            End If
            If mPatient.PatientName.PersonContactAddress.Country IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.Country <> "" Then
                _patientDemo.Address.Country = mPatient.PatientName.PersonContactAddress.Country
            Else

                _patientDemo.Address.Country = ""
            End If
            If mPatient.PatientName.PersonContactAddress.Zip IsNot Nothing AndAlso mPatient.PatientName.PersonContactAddress.Zip <> "" Then
                _patientDemo.Address.ZipCode = mPatient.PatientName.PersonContactAddress.Zip
            Else

                _patientDemo.Address.ZipCode = ""
            End If


            ''Contact
            _patientDemo.Contact = New ContactPersonal
            _patientDemo.Contact.ContactName = New gloCCDExchange.PersonName
            _patientDemo.Contact.ContactName.ItemsElementName = New ItemsChoiceType(4) {}

            _patientDemo.Contact.ContactName.ItemsElementName(0) = gloCCDExchange.ItemsChoiceType.Prefix
            _patientDemo.Contact.ContactName.ItemsElementName(1) = gloCCDExchange.ItemsChoiceType.FirstName
            _patientDemo.Contact.ContactName.ItemsElementName(2) = gloCCDExchange.ItemsChoiceType.MiddleName
            _patientDemo.Contact.ContactName.ItemsElementName(3) = gloCCDExchange.ItemsChoiceType.LastName
            _patientDemo.Contact.ContactName.ItemsElementName(4) = gloCCDExchange.ItemsChoiceType.Suffix

            _patientDemo.Contact.ContactName.Items = New String(4) {}
            _patientDemo.Contact.ContactName.Items(0) = ""
            _patientDemo.Contact.ContactName.Items(1) = ""
            _patientDemo.Contact.ContactName.Items(2) = ""
            _patientDemo.Contact.ContactName.Items(3) = ""
            _patientDemo.Contact.ContactName.Items(4) = ""



            ' ''Email
            If mPatient.Email IsNot Nothing AndAlso mPatient.Email <> "" Then
                _patientDemo.Contact.Email = mPatient.Email
            Else
                _patientDemo.Contact.Email = ""
            End If


            ' ''Telephone
            _patientDemo.Contact.Telephone = New Telephone(0) {}
            _patientDemo.Contact.Telephone(0) = New Telephone

            If mPatient.Phone IsNot Nothing AndAlso mPatient.Phone <> "" Then
                _patientDemo.Contact.Telephone(0).Type = TelephoneTypeEnum.HOME
                _patientDemo.Contact.Telephone(0).Number = String.Format("{0:tel:+1(###) ###-####}", Long.Parse(mPatient.Phone))
                _patientDemo.Contact.Telephone(0).Primary = True
            Else
                _patientDemo.Contact.Telephone(0) = Nothing
                '_patientDemo.Contact.Telephone(0).Number = Nothing
            End If

            ''Gender
            If mPatient.Gender IsNot Nothing AndAlso mPatient.Gender <> "" Then
                _patientDemo.Gender = mPatient.Gender
            Else
                _patientDemo.Gender = ""
            End If

            ''MarritalStatus
            If mPatient.MaritalStatus IsNot Nothing AndAlso mPatient.MaritalStatus <> "" Then
                _patientDemo.MaritalStatusSpecified = True
                If mPatient.MaritalStatus = "Married" Then
                    _patientDemo.MaritalStatus = MaritalStatusEnum.MARRIED
                ElseIf mPatient.MaritalStatus = "Single" Then
                    _patientDemo.MaritalStatus = MaritalStatusEnum.SINGLE
                ElseIf mPatient.MaritalStatus = "Widowed" Then
                    _patientDemo.MaritalStatus = MaritalStatusEnum.WIDOWED
                ElseIf mPatient.MaritalStatus = "Divorced" Then
                    _patientDemo.MaritalStatus = MaritalStatusEnum.DIVORCED

                End If
                'Else
                '    _patientDemo.MaritalStatus = ""
            End If

            ''Race
            If mPatient.Race IsNot Nothing AndAlso mPatient.Race <> "" Then
                Dim _Races() As String
                _Races = mPatient.Race.Trim.Split(",")
                _patientDemo.RaceSpecified = True
                If mPatient.Race = "Asian" Then

                    _patientDemo.Race = RaceEnum.Asian
                ElseIf mPatient.Race = "American Indian or Alaska Native" Then

                    _patientDemo.Race = RaceEnum.AmericanIndianorAlaskaNative
                ElseIf mPatient.Race = "Black or African American" Then

                    _patientDemo.Race = RaceEnum.BlackorAfricanAmerican
                ElseIf _Races.Length > 1 Then

                    _patientDemo.Race = RaceEnum.Morethanonerace
                ElseIf mPatient.Race = "Native Hawaiian or Other Pacific Islander" Then

                    _patientDemo.Race = RaceEnum.NativeHawaiianorOtherPacificIslander
                ElseIf mPatient.Race = "Declined to specify" Then

                    _patientDemo.Race = RaceEnum.Unreportedorrefusedtoreport
                ElseIf mPatient.Race = "White" Then

                    _patientDemo.Race = RaceEnum.White
                End If
                'Else
                '    _patientDemo.Race = ""
            End If

            If mPatient.Ethnicity IsNot Nothing AndAlso mPatient.Ethnicity <> "" Then
                _patientDemo.EthnicitySpecified = True
                If mPatient.Ethnicity = "Hispanic Or Latino" Then

                    _patientDemo.Ethnicity = EthnicityEnum.HispanicorLatino
                ElseIf mPatient.Ethnicity = "Not Hispanic Or Latino" Then

                    _patientDemo.Ethnicity = EthnicityEnum.NotHispanicorLatino
                ElseIf mPatient.Ethnicity = "Declined to specify" Then

                    _patientDemo.Ethnicity = EthnicityEnum.Unreported
                End If
                'Else
                '    _patientDemo.Ethnicity = ""
            End If

            If mPatient.Language IsNot Nothing AndAlso mPatient.Language <> "" Then
                _patientDemo.LanguageSpecified = True
                If mPatient.Language = "Spanish; Castilian" Then
                    _patientDemo.Language = LanguageEnum.Spanish
                ElseIf mPatient.Language = "Arabic" Then
                    _patientDemo.Language = LanguageEnum.Arabic
                ElseIf mPatient.Language = "Chinese" Then
                    _patientDemo.Language = LanguageEnum.Chinese
                ElseIf mPatient.Language = "English" Then
                    _patientDemo.Language = LanguageEnum.English
                ElseIf mPatient.Language = "French" Then
                    _patientDemo.Language = LanguageEnum.French
                ElseIf mPatient.Language = "German" Then
                    _patientDemo.Language = LanguageEnum.German
                ElseIf mPatient.Language = "Italian" Then
                    _patientDemo.Language = LanguageEnum.Italian
                ElseIf mPatient.Language = "Japanese" Then
                    _patientDemo.Language = LanguageEnum.Japanese
                ElseIf mPatient.Language = "Korean" Then
                    _patientDemo.Language = LanguageEnum.Korean
                ElseIf mPatient.Language = "Polish" Then
                    _patientDemo.Language = LanguageEnum.Polish
                ElseIf mPatient.Language = "Portuguese" Then
                    _patientDemo.Language = LanguageEnum.Portuguese
                ElseIf mPatient.Language = "Russian" Then
                    _patientDemo.Language = LanguageEnum.Russian
                ElseIf mPatient.Language = "Urdu" Then
                    _patientDemo.Language = LanguageEnum.Urdu
                ElseIf mPatient.Language = "Vietnamese" Then
                    _patientDemo.Language = LanguageEnum.Vietnamese
                End If
                'Else
                '    _patientDemo.Language = ""
            End If
            ''Fax,Mail,Phone
            If mPatient.CommPreference IsNot Nothing AndAlso mPatient.CommPreference <> "" Then
                _patientDemo.PreferredCommunicationMethodSpecified = True
                If mPatient.CommPreference = "Phone" Then
                    _patientDemo.PreferredCommunicationMethod = CommunicationMethodEnum.HomePhone
                ElseIf mPatient.CommPreference = "Mobile" Then
                    _patientDemo.PreferredCommunicationMethod = CommunicationMethodEnum.MobilePhone
                ElseIf mPatient.CommPreference = "Patient Portal Message" Then
                    _patientDemo.PreferredCommunicationMethod = CommunicationMethodEnum.SecureEmail
                ElseIf mPatient.CommPreference = "Email" Then
                    _patientDemo.PreferredCommunicationMethod = CommunicationMethodEnum.USMail
                    'ElseIf mPatient.CommPreference = "" Then
                    '    _patientDemo.PreferredCommunicationMethod = CommunicationMethodEnum.WorkPhone
                End If

                'Else

                '    _patientDemo.PreferredCommunicationMethod = ""
            End If

            If mPatient.DateofBirth IsNot Nothing AndAlso mPatient.DateofBirth <> "" Then
                _patientDemo.DateOfBirthSpecified = True
                _patientDemo.DateOfBirth = (Convert.ToDateTime(mPatient.DateofBirth))
            End If

            Return _patientDemo
        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "Record Target Section"
            Else
                _msgString = _msgString & vbNewLine & "Record Target Section"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing
        Finally

            If Not IsNothing(_oCDADataExtraction) Then
                _oCDADataExtraction.Dispose()
                _oCDADataExtraction = Nothing
            End If
            'If Not IsNothing(_recordTarget) Then
            '    _recordTarget = Nothing
            'End If
        End Try
    End Function
    Private Function GetPracticeInfo() As PracticeInformation
        Dim _practiceInfo As New PracticeInformation()
        'Dim _oCDADataExtraction As New gloCDADataExtraction
        Try

            _practiceInfo.PracticeIdentifier = New PracticeIdentifiers
            If mPatient.PracticeContact.PracticeID IsNot Nothing AndAlso mPatient.PracticeContact.PracticeID <> "" Then
                _practiceInfo.PracticeIdentifier.PracticeId = mPatient.PracticeContact.PracticeID
            Else
                _practiceInfo.PracticeIdentifier.PracticeId = ""
            End If
            If _PracticeID <> "" Then
                _practiceInfo.PracticeIdentifier.IntuitPracticeId = _PracticeID
            Else
                _practiceInfo.PracticeIdentifier.IntuitPracticeId = ""
            End If



            ''Practice Name
            If mPatient.Clinic.ClinicName IsNot Nothing AndAlso mPatient.Clinic.ClinicName <> "" Then
                _practiceInfo.PracticeName = mPatient.Clinic.ClinicName
            Else
                _practiceInfo.PracticeName = ""
            End If

            _practiceInfo.Address = New AddressInformation
            If mPatient.Clinic.PersonContactAddress.Street IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.Street <> "" Then
                _practiceInfo.Address.Line1 = mPatient.Clinic.PersonContactAddress.Street
            Else
                _practiceInfo.Address.Line1 = ""
            End If

            If mPatient.Clinic.PersonContactAddress.AddressLine2 IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.AddressLine2 <> "" Then
                _practiceInfo.Address.Line2 = mPatient.Clinic.PersonContactAddress.AddressLine2
            Else
                _practiceInfo.Address.Line2 = ""
            End If

            If mPatient.Clinic.PersonContactAddress.City IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.City <> "" Then
                _practiceInfo.Address.City = mPatient.Clinic.PersonContactAddress.City
            Else
                _practiceInfo.Address.City = ""
            End If

            If mPatient.Clinic.PersonContactAddress.State IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.State <> "" Then
                _practiceInfo.Address.State = mPatient.Clinic.PersonContactAddress.State
            Else
                _practiceInfo.Address.State = ""
            End If

            If mPatient.Clinic.PersonContactAddress.Country IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.Country <> "" Then
                _practiceInfo.Address.Country = mPatient.Clinic.PersonContactAddress.Country
            Else
                _practiceInfo.Address.Country = ""
            End If

            If mPatient.Clinic.PersonContactAddress.Zip IsNot Nothing AndAlso mPatient.Clinic.PersonContactAddress.Zip <> "" Then
                _practiceInfo.Address.ZipCode = mPatient.Clinic.PersonContactAddress.Zip
            Else
                _practiceInfo.Address.ZipCode = ""
            End If


            ''Contact
            _practiceInfo.Contact = New ContactPersonal
            _practiceInfo.Contact.ContactName = New gloCCDExchange.PersonName

            _practiceInfo.Contact.ContactName.ItemsElementName = New ItemsChoiceType(4) {}

            _practiceInfo.Contact.ContactName.ItemsElementName(0) = gloCCDExchange.ItemsChoiceType.Prefix
            _practiceInfo.Contact.ContactName.ItemsElementName(1) = gloCCDExchange.ItemsChoiceType.FirstName
            _practiceInfo.Contact.ContactName.ItemsElementName(2) = gloCCDExchange.ItemsChoiceType.MiddleName
            _practiceInfo.Contact.ContactName.ItemsElementName(3) = gloCCDExchange.ItemsChoiceType.LastName
            _practiceInfo.Contact.ContactName.ItemsElementName(4) = gloCCDExchange.ItemsChoiceType.Suffix

            _practiceInfo.Contact.ContactName.Items = New String(4) {}
            _practiceInfo.Contact.ContactName.Items(0) = ""
            'If mPatient.PracticeContact.ContactName IsNot Nothing AndAlso mPatient.PracticeContact.ContactName <> "" Then
            '    _practiceInfo.Contact.ContactName.Items(1) = mPatient.PracticeContact.ContactName
            'Else
            '    _practiceInfo.Contact.ContactName.Items(1) = ""
            'End If
            _practiceInfo.Contact.ContactName.Items(1) = ""
            _practiceInfo.Contact.ContactName.Items(2) = ""
            _practiceInfo.Contact.ContactName.Items(3) = ""
            _practiceInfo.Contact.ContactName.Items(4) = ""

            If mPatient.PracticeContact.ContactEmail IsNot Nothing AndAlso mPatient.PracticeContact.ContactEmail <> "" Then
                _practiceInfo.Contact.Email = mPatient.PracticeContact.ContactEmail
            Else
                _practiceInfo.Contact.Email = ""
            End If

            _practiceInfo.Contact.Telephone = New Telephone(0) {}
            _practiceInfo.Contact.Telephone(0) = New Telephone

            If mPatient.PracticeContact.PersonContactPhone.Phone IsNot Nothing AndAlso mPatient.PracticeContact.PersonContactPhone.Phone <> "" Then
                _practiceInfo.Contact.Telephone(0).Type = TelephoneTypeEnum.HOME
                _practiceInfo.Contact.Telephone(0).Number = String.Format("{0:tel:+1(###) ###-####}", Long.Parse(mPatient.PracticeContact.PersonContactPhone.Phone))
                _practiceInfo.Contact.Telephone(0).Primary = True
            Else
                _practiceInfo.Contact.Telephone(0) = Nothing
                ' _practiceInfo.Contact.Telephone(0).Number = Nothing
            End If


            ''Provider
            _practiceInfo.Provider = New gloCCDExchange.Provider(0) {}
            _practiceInfo.Provider(0) = New Provider
            _practiceInfo.Provider(0).Role = "Prescribing clinician"
            _practiceInfo.Provider(0).ProviderIdentifier = New ProviderIdentifiers
            If _PracticeProviderID <> 0 Then
                _practiceInfo.Provider(0).ProviderIdentifier.PracticeProviderId = _PracticeProviderID
            Else
                _practiceInfo.Provider(0).ProviderIdentifier.PracticeProviderId = ""
            End If

            If _IntuitProviderID <> 0 Then
                _practiceInfo.Provider(0).ProviderIdentifier.IntuitProviderId = _IntuitProviderID
            Else
                _practiceInfo.Provider(0).ProviderIdentifier.IntuitProviderId = ""
            End If


            ''Demographics
            _practiceInfo.Provider(0).Demographics = New ProviderDemographics
            _practiceInfo.Provider(0).Demographics.Contact = New ContactPersonal
            _practiceInfo.Provider(0).Demographics.Contact.ContactName = New gloCCDExchange.PersonName
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName = New gloCCDExchange.ItemsChoiceType(4) {}

            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName(0) = gloCCDExchange.ItemsChoiceType.Prefix
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName(1) = gloCCDExchange.ItemsChoiceType.FirstName
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName(2) = gloCCDExchange.ItemsChoiceType.MiddleName
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName(3) = gloCCDExchange.ItemsChoiceType.LastName
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.ItemsElementName(4) = gloCCDExchange.ItemsChoiceType.Suffix

            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items = New String(4) {}
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items(0) = ""
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items(1) = ""
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items(2) = ""
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items(3) = ""
            _practiceInfo.Provider(0).Demographics.Contact.ContactName.Items(4) = ""

            _practiceInfo.Provider(0).Demographics.Contact.Email = ""
            _practiceInfo.Provider(0).Demographics.Contact.Telephone = New Telephone(0) {}
            _practiceInfo.Provider(0).Demographics.Contact.Telephone(0) = New Telephone

            If mPatient.PatientName.PersonContactPhone.Phone IsNot Nothing AndAlso mPatient.PatientName.PersonContactPhone.Phone <> "" Then
                _practiceInfo.Provider(0).Demographics.Contact.Telephone(0).Type = TelephoneTypeEnum.HOME
                _practiceInfo.Provider(0).Demographics.Contact.Telephone(0).Number = String.Format("{0:tel:+1(###) ###-####}", Long.Parse(mPatient.PatientName.PersonContactPhone.Phone))
                _practiceInfo.Provider(0).Demographics.Contact.Telephone(0).Primary = True
            Else
                _practiceInfo.Provider(0).Demographics.Contact.Telephone(0) = Nothing
                ' _practiceInfo.Provider(0).Demographics.Contact.Telephone(0).Number = Nothing
            End If



            _practiceInfo.Provider(0).Demographics.Address = New AddressInformation(0) {}
            _practiceInfo.Provider(0).Demographics.Address(0) = New AddressInformation
            _practiceInfo.Provider(0).Demographics.Address(0).Line1 = ""

            _practiceInfo.Provider(0).Demographics.Address(0).Line2 = ""
            _practiceInfo.Provider(0).Demographics.Address(0).City = ""
            _practiceInfo.Provider(0).Demographics.Address(0).State = ""
            _practiceInfo.Provider(0).Demographics.Address(0).Country = ""
            _practiceInfo.Provider(0).Demographics.Address(0).ZipCode = ""


            Return _practiceInfo



        Catch ex As Exception
            If _msgString = "" Then

                _msgString = vbNewLine & _errormsg & vbNewLine & "Practice Information"
            Else
                _msgString = _msgString & vbNewLine & "Practice Information"
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            '  MessageBox.Show("Error:" & ex.ToString())
            Return Nothing
        End Try
    End Function
    Private Function GetCCDData() As CcdExchangeCcd
        Dim _CCD As New CcdExchangeCcd()

        'Dim oCDADataExtraction As New gloCDADataExtraction
        Dim ccdstring As String = ""
        Dim doc As New XmlDocument
        Dim sw As New StringWriter

        Try
            ''  _CCD.CcdXml =New String (){ "'&lt;?xml version="1.0" encoding="utf-8"?&gt;&lt;ClinicalDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" moodCode="EVN" xmlns="urn:hl7-org:v3"&gt; &lt;/ClinicalDocument&gt;'"}
            ' _CCD.CcdXml = "<?xml version=""1.0"" encoding=""utf-8"" ?> <ClinicalDocument xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" moodCode=""EVN"" xmlns=""urn:hl7-org:v3""> </ClinicalDocument>"
            ''  _CCD.CcdXml = "&lt;?xml version="1.0" encoding="utf-8"?&gt;&lt;ClinicalDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" moodCode="EVN" xmlns="urn:hl7-org:v3"&gt; &lt;/ClinicalDocument&gt;"
            If _CCDFilePath <> "" Then
                '   Dim arrByte As Byte() = ConvertFiletoBinary(_CCDFilePath)
                ' ccdstring = Encoding.GetEncoding(1252).GetString(arrByte)
                ''  ccdstring = Encoding.UTF8.GetString(arrByte, 0, Convert.ToInt16(arrByte.Length))
                ' _CCD.CcdXml = ccdstring
                ' Dim XMLarrByte As Byte()() = Nothing

                ''
                doc.Load(_CCDFilePath)
                Dim tx As New XmlTextWriter(sw)
                doc.WriteTo(tx)
                ccdstring = sw.ToString()
                _CCD.CcdXml = ccdstring
                tx = Nothing
                ''
            End If


            Return _CCD
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            ccdstring = Nothing
            doc = Nothing
            If Not IsNothing(sw) Then
                sw.Dispose()
                sw = Nothing
            End If

        End Try

    End Function

    Public Function ConvertFiletoBinary(ByVal strFileName As String) As Byte()
        If File.Exists(strFileName) Then
            Dim oFile As FileStream = Nothing
            Dim oReader As BinaryReader = Nothing
            Try
                oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)
                oReader = New BinaryReader(oFile)
                Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
                Return bytesRead

            Catch ex As IOException
                Throw New gloCCDException(ex.ToString)
            Catch ex As Exception
                Throw New gloCCDException(ex.ToString)
            Finally
                If Not IsNothing(oFile) Then
                    oFile.Close()
                    oFile.Dispose()
                    oFile = Nothing
                End If
                If Not IsNothing(oReader) Then
                    oReader.Close()
                    oReader.Dispose()
                    oReader = Nothing
                End If

            End Try
        Else
            Return Nothing
        End If
    End Function
End Class
