Imports System.Xml
Imports System.IO
Imports System.Data.SqlClient
Imports gloSecurity

Public Enum InsuranceType
    epolicy = 1
    eauthorisation = 2
End Enum
Public Class gloCCDInterface
    Implements IDisposable
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPatient As Patient
    Private strMessageLog As System.Text.StringBuilder
    Private mEffectivetime As String = ""
    Private mPatientID As Int64
    Private _IsNewProblemList As Boolean = False 'Added by kanchan on 20100629 

    ''20121003
    Private blIsOwnedbyPastExam As Boolean = False
    Private dtDOS As DateTime = System.DateTime.Now
    ''

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Property EffectiveTime() As String
        Get
            Return mEffectivetime
        End Get
        Set(ByVal value As String)
            mEffectivetime = value
        End Set
    End Property
    Public Property IsNewProblemList() As Boolean
        Get
            Return _IsNewProblemList
        End Get
        Set(ByVal value As Boolean)
            _IsNewProblemList = value
        End Set
    End Property
    Public Property PatientObject() As Patient
        Get
            Return mPatient
        End Get
        Set(ByVal value As Patient)
            mPatient = value
        End Set
    End Property

    Public Function GenerateFileName(Optional ByVal PatientLastName As String = "") As String


        Dim strfilename As String = ""
        strfilename = "CCD_"
        Dim dtdate As DateTime = Date.UtcNow
        Dim strtemp As String

        ''20121003
        If blIsOwnedbyPastExam Then
            dtdate = dtDOS
        End If
        ''

        If PatientLastName = "" Then
            strtemp = strfilename & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
        Else
            strtemp = strfilename & PatientLastName & "_" & dtdate.Month.ToString & dtdate.Day.ToString & dtdate.Year.ToString & dtdate.Hour.ToString & dtdate.Minute.ToString & dtdate.Second.ToString & dtdate.Millisecond.ToString
        End If

        strfilename = gloLibCCDGeneral.CCDFileGenerationPath & strtemp & ".xml"

        Dim i As Integer = 0
        Dim _Path() As String
        'Dim path As String = ""
        Dim oFileInfo1 As System.IO.FileInfo
        oFileInfo1 = New System.IO.FileInfo(strfilename)

        Try

            Do While oFileInfo1.Exists
                oFileInfo1 = Nothing
                oFileInfo1 = New System.IO.FileInfo(strfilename)
                If oFileInfo1.Exists Then
                    '07-Oct-14 Aniket: Bug #74812: gloEMR - Patient details panel- Exam - CCD- Preview - Application is showing an exception
                    'oFileInfo1 = Nothing

                    Dim fileName As String = oFileInfo1.Name
                    Dim directoryName As String = oFileInfo1.DirectoryName
                    fileName = fileName.Replace(".xml", "")
                    If fileName.Contains("-") Then
                        _Path = fileName.Split("-")
                        If _Path.Length > 1 Then
                            fileName = _Path(0)
                        End If
                    End If
                    i += 1
                    strfilename = directoryName & "\" & fileName & "-" & i & ".xml"
                Else
                    '07-Oct-14 Aniket: Bug #74812: gloEMR - Patient details panel- Exam - CCD- Preview - Application is showing an exception
                    'oFileInfo1 = Nothing
                    Exit Do
                End If
            Loop

            Return strfilename


        Catch ex As Exception
            Throw ex

        Finally
            '07-Oct-14 Aniket: Bug #74812: gloEMR - Patient details panel- Exam - CCD- Preview - Application is showing an exception
            oFileInfo1 = Nothing
            strtemp = Nothing
            _Path = Nothing
        End Try

    End Function
    ''added for case CAS-22862-B2C8D4
    Public Function GenerateClinicalInformationold(ByVal PatientID As Int64, ByVal LoginID As Int64, Optional ByVal CCDSection As String = "", Optional ByVal _VisitId As Int64 = 0, Optional ByVal FromDate As String = Nothing, Optional ByVal ToDate As String = Nothing, Optional ByVal _FinalCCDSavePath As String = "", Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000") As String
        Dim ogloCCDDBLayer As New gloCCDDatabaseLayer
        Dim mCCDSection, mStrXMLfile As String
        Dim dsPatient As DataSet = Nothing

        ''
        'Problems No:00000251::20121004
        blIsOwnedbyPastExam = _IsOwnByPastExam
        If (IsNothing(_DateOfservice)) Then
            _DateOfservice = "1/1/2000"
        End If
        dtDOS = Convert.ToDateTime(_DateOfservice)
        ''

        Try
            If PatientID = 0 Then
                Return ""
            End If
            mPatientID = PatientID

            mCCDSection = CCDSection
            If (IsNothing(mCCDSection)) Then
                mCCDSection = ""
            End If
            'Memory Leak
            'ogloCCDDBLayer = New gloCCDDatabaseLayer
            '
            ogloCCDDBLayer.VisitID = _VisitId
            ogloCCDDBLayer.FromDate = FromDate
            ogloCCDDBLayer.ToDate = ToDate
            dsPatient = ogloCCDDBLayer.GetPatientDetailInformation(mPatientID, LoginID)
            mPatient = ogloCCDDBLayer.GetPatientInformation(dsPatient.Tables("Patient"))
            mPatient.PatientID = mPatientID
            'mPatient = ogloCCDDBLayer.GetPatientInfo(mPatientID)
            mPatient.PatientProviders = ogloCCDDBLayer.GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = ogloCCDDBLayer.GetClinicInfo(dsPatient.Tables("Clinic"))
            'mPatient.Author = ogloCCDDBLayer.GetPatientAuthorInfo(LoginID)
            mPatient.Author = ogloCCDDBLayer.GetPatientAuthorInfo_New(dsPatient.Tables("User"), dsPatient.Tables("Clinic"))
            mPatient.PatientInsurances = ogloCCDDBLayer.GetPatientInsuranceInfo_New(dsPatient.Tables("Insurance"))
            'mPatient.PatientInsurances = ogloCCDDBLayer.GetPatientInsuranceInfo(mPatientID)

            If IsNothing(mPatient) Then
                Return ""
            End If
            'If condition Added by kanchan on 20101110
            If mCCDSection.Contains("Allergy") = True Or mCCDSection = "All" Then
                mPatient.PatientAllergies = ogloCCDDBLayer.GetLatestAllergiesinfo_New(mPatientID)
            End If
            If mCCDSection.Contains("Medications") = True Or mCCDSection = "All" Then
                mPatient.PatientMedications = ogloCCDDBLayer.GetLatestMedicationinfo_New(mPatientID)
            End If
            'Code Added by kanchan on 20101103
            If mCCDSection.Contains("AdvanceDirectives") = True Or mCCDSection = "All" Then
                mPatient.Advancedirective = ogloCCDDBLayer.GetPatientAdvDirectives(mPatientID)
            End If
            If mCCDSection.Contains("FamilyHistory") = True Or mCCDSection = "All" Then
                mPatient.PatientFamilyHistory = ogloCCDDBLayer.GetPatientFamilyHistory(mPatientID)
            End If
            If mCCDSection.Contains("SocialHistory") = True Or mCCDSection = "All" Then
                mPatient.PatientSocialHistory = ogloCCDDBLayer.GetPatientSocialHistory(mPatientID)
            End If
            If mCCDSection.Contains("Procedures") = True Or mCCDSection = "All" Then
                mPatient.PatientProcedures = ogloCCDDBLayer.GetPatientProcedure(mPatientID)
            End If
            If mCCDSection.Contains("Encounter") = True Or mCCDSection = "All" Then
                mPatient.PatientEncounters = ogloCCDDBLayer.GetPatientEncounter(mPatientID)
            End If
            Dim dtvitals As DataTable = Nothing
            'mPatient.PatientVitals = ogloCCDDBLayer.GetPatientVitals(mPatientID)
            If mCCDSection.Contains("Vitals") = True Or mCCDSection = "All" Then
                dtvitals = ogloCCDDBLayer.GetPatientVitalsold(mPatientID)  ''    ''added for case CAS-22862-B2C8D4
            Else
                dtvitals = New DataTable
            End If
            'Code commented & added by kanchan on 20100629
            'mPatient.PatientProblems = ogloCCDDBLayer.GetPatientProblems(mPatientID)
            If mCCDSection.Contains("Problems") = True Or mCCDSection = "All" Then
                mPatient.PatientProblems = ogloCCDDBLayer.GetPatientProblems(mPatientID, _IsNewProblemList)
            End If
            If mCCDSection.Contains("Results") = True Or mCCDSection = "All" Then
                mPatient.LabTests = ogloCCDDBLayer.GetLabTestsWithResult(mPatientID)
            End If
            If mCCDSection.Contains("Immunization") = True Or mCCDSection = "All" Then
                mPatient.PatientImmunizations = ogloCCDDBLayer.GetPatientImmunizations(mPatientID)
            End If



            mStrXMLfile = GenerateCCD(mCCDSection, dtvitals, mPatient.PatientName.LastName, _FinalCCDSavePath)

            If (IsNothing(dtvitals) = False) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            ogloCCDDBLayer.VisitID = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            'Memory Leak
            If Not IsNothing(ogloCCDDBLayer) Then
                ogloCCDDBLayer.VisitID = 0
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
        End Try
    End Function

    Public Function GetNonStructuredFilePath(ByVal PatientID As Int64, ByVal AttachmentPath As String) As String
        Dim sReturnedFilePath As String = ""
        Try

        Catch ex As Exception

        End Try
        Return sReturnedFilePath
    End Function

    Public Function GenerateClinicalInformation(ByVal PatientID As Int64, ByVal LoginID As Int64, Optional ByVal CCDSection As String = "", Optional ByVal _VisitId As Int64 = 0, Optional ByVal FromDate As String = Nothing, Optional ByVal ToDate As String = Nothing, Optional ByVal _FinalCCDSavePath As String = "", Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000") As String
        Dim ogloCCDDBLayer As New gloCCDDatabaseLayer
        Dim mCCDSection, mStrXMLfile As String
        Dim dsPatient As DataSet = Nothing

        ''
        'Problems No:00000251::20121004
        blIsOwnedbyPastExam = _IsOwnByPastExam
        If (IsNothing(_DateOfservice)) Then
            _DateOfservice = "1/1/2000"
        End If
        dtDOS = Convert.ToDateTime(_DateOfservice)
        ''

        Try
            If PatientID = 0 Then
                Return ""
            End If
            mPatientID = PatientID

            mCCDSection = CCDSection
            If (IsNothing(mCCDSection)) Then
                mCCDSection = ""
            End If
            'Memory Leak
            'ogloCCDDBLayer = New gloCCDDatabaseLayer
            '
            ogloCCDDBLayer.VisitID = _VisitId
            ogloCCDDBLayer.FromDate = FromDate
            ogloCCDDBLayer.ToDate = ToDate
            dsPatient = ogloCCDDBLayer.GetPatientDetailInformation(mPatientID, LoginID)
            mPatient = ogloCCDDBLayer.GetPatientInformation(dsPatient.Tables("Patient"))
            mPatient.PatientID = mPatientID
            'mPatient = ogloCCDDBLayer.GetPatientInfo(mPatientID)
            mPatient.PatientProviders = ogloCCDDBLayer.GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = ogloCCDDBLayer.GetClinicInfo(dsPatient.Tables("Clinic"))
            'mPatient.Author = ogloCCDDBLayer.GetPatientAuthorInfo(LoginID)
            mPatient.Author = ogloCCDDBLayer.GetPatientAuthorInfo_New(dsPatient.Tables("User"), dsPatient.Tables("Clinic"))
            mPatient.PatientInsurances = ogloCCDDBLayer.GetPatientInsuranceInfo_New(dsPatient.Tables("Insurance"))
            'mPatient.PatientInsurances = ogloCCDDBLayer.GetPatientInsuranceInfo(mPatientID)

            If IsNothing(mPatient) Then
                Return ""
            End If
            'If condition Added by kanchan on 20101110
            If mCCDSection.Contains("Allergy") = True Or mCCDSection = "All" Then
                mPatient.PatientAllergies = ogloCCDDBLayer.GetLatestAllergiesinfo_New(mPatientID)
            End If
            If mCCDSection.Contains("Medications") = True Or mCCDSection = "All" Then
                mPatient.PatientMedications = ogloCCDDBLayer.GetLatestMedicationinfo_New(mPatientID)
            End If
            'Code Added by kanchan on 20101103
            If mCCDSection.Contains("AdvanceDirectives") = True Or mCCDSection = "All" Then
                mPatient.Advancedirective = ogloCCDDBLayer.GetPatientAdvDirectives(mPatientID)
            End If
            If mCCDSection.Contains("FamilyHistory") = True Or mCCDSection = "All" Then
                mPatient.PatientFamilyHistory = ogloCCDDBLayer.GetPatientFamilyHistory(mPatientID)
            End If
            If mCCDSection.Contains("SocialHistory") = True Or mCCDSection = "All" Then
                mPatient.PatientSocialHistory = ogloCCDDBLayer.GetPatientSocialHistory(mPatientID)
            End If
            If mCCDSection.Contains("Procedures") = True Or mCCDSection = "All" Then
                mPatient.PatientProcedures = ogloCCDDBLayer.GetPatientProcedure(mPatientID)
            End If
            If mCCDSection.Contains("Encounter") = True Or mCCDSection = "All" Then
                mPatient.PatientEncounters = ogloCCDDBLayer.GetPatientEncounter(mPatientID)
            End If
            Dim dtvitals As DataTable = Nothing
            'mPatient.PatientVitals = ogloCCDDBLayer.GetPatientVitals(mPatientID)
            If mCCDSection.Contains("Vitals") = True Or mCCDSection = "All" Then
                dtvitals = ogloCCDDBLayer.GetPatientVitalsinDT(mPatientID)
            Else
                dtvitals = New DataTable
            End If
            'Code commented & added by kanchan on 20100629
            'mPatient.PatientProblems = ogloCCDDBLayer.GetPatientProblems(mPatientID)
            If mCCDSection.Contains("Problems") = True Or mCCDSection = "All" Then
                mPatient.PatientProblems = ogloCCDDBLayer.GetPatientProblems(mPatientID, _IsNewProblemList)
            End If
            If mCCDSection.Contains("Results") = True Or mCCDSection = "All" Then
                mPatient.LabTests = ogloCCDDBLayer.GetLabTestsWithResult(mPatientID)
            End If
            If mCCDSection.Contains("Immunization") = True Or mCCDSection = "All" Then
                mPatient.PatientImmunizations = ogloCCDDBLayer.GetPatientImmunizations(mPatientID)
            End If

         
            Dim _ISCCHIT = ogloCCDDBLayer.getCCHITSeetings_New(dsPatient.Tables("Setting"))

            If _ISCCHIT = True Then
                mStrXMLfile = GenerateClinicalInformation_New(mCCDSection, dtvitals, mPatient.PatientName.LastName)
            Else
                mStrXMLfile = GenerateCCD(mCCDSection, dtvitals, mPatient.PatientName.LastName, _FinalCCDSavePath)
            End If
            If (IsNothing(dtvitals) = False) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            ogloCCDDBLayer.VisitID = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            'Memory Leak
            If Not IsNothing(ogloCCDDBLayer) Then
                ogloCCDDBLayer.VisitID = 0
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
        End Try
    End Function



    'Code Start-Added by kanchan on 20100623 for generate CCD as per CCHIT standard

    Public Function GenerateClinicalInformation_New(ByVal CCDSection As String, Optional ByVal dtvitals As DataTable = Nothing, Optional ByVal PatientLastName As String = "") As String
        Dim ogloCCDDBLayer As New gloCCDDatabaseLayer

        Dim strDate As String 'this variable is used for formation date in yyyyMMdd format from the objects
        Dim _dtClinicInfo As DataTable = Nothing

        Dim _clinicName As String = ""
        Dim _clinicAddr As String = ""
        Dim _clinicCity As String = ""
        Dim _clinicState As String = ""
        Dim _cliniPostalCode As String = ""
        Dim _clinicCountry As String = ""
        Dim _clinicCounty As String = ""
        Dim _clinicPhone As String = ""

        Dim xmlwriter As XmlTextWriter = Nothing

        Try

            '    Set Clinic Information - Start 

            _dtClinicInfo = ogloCCDDBLayer.GetClinicData()
            If Not IsNothing(_dtClinicInfo) AndAlso _dtClinicInfo.Rows.Count > 0 Then

                _clinicName = _dtClinicInfo.Rows(0)("ClinicName")
                _clinicAddr = _dtClinicInfo.Rows(0)("AddressLine1")
                _clinicCity = _dtClinicInfo.Rows(0)("City")
                _clinicState = _dtClinicInfo.Rows(0)("State")
                _cliniPostalCode = _dtClinicInfo.Rows(0)("PostalCode")
                _clinicCounty = _dtClinicInfo.Rows(0)("sCounty")
                _clinicCountry = _dtClinicInfo.Rows(0)("Country")
                _clinicPhone = _dtClinicInfo.Rows(0)("Phone")

            End If
            If Not IsNothing(_dtClinicInfo) Then
                _dtClinicInfo.Dispose()
                _dtClinicInfo = Nothing
            End If
            '    Set Clinic Information - End 

            Dim strfilepath As String = GenerateFileName(PatientLastName)

            If System.IO.File.Exists(strfilepath) Then
                System.IO.File.Delete(strfilepath)
            End If
            xmlwriter = New XmlTextWriter(strfilepath, Nothing)
            xmlwriter.Formatting = Formatting.Indented

            xmlwriter.WriteStartDocument()
            '  responseText = "<?xml-stylesheet type='text/xsl' href='" & gloCCDSchema.gloCDAWriterParameters.CDAStyleSheetPath & "'?>"
            'Dim _myStyle As String = "type='text/xsl' href='http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl'"
            Dim _myStyle As String = "type='text/xsl' href='" & gloCCDSchema.gloCDAWriterParameters.CDAStyleSheetPath & "'"

            xmlwriter.WriteProcessingInstruction("xml-stylesheet", _myStyle)

            xmlwriter.WriteStartElement("ClinicalDocument") 'Open the Main Parent Node 
            'xmlwriter.WriteAttributeString("stylesheet", "type='text/xsl' href = 'xslt\Stylesheet.xsl'")

            xmlwriter.WriteAttributeString("xsi:schemaLocation", "urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd")
            xmlwriter.WriteAttributeString("xmlns:sdtc", "urn:hl7-org:sdtc")
            xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlwriter.WriteAttributeString("xmlns", "urn:hl7-org:v3")

            'xmlwriter.WriteStartElement("Header")
            'xmlwriter.WriteEndElement() 'End Header Element
            xmlwriter.WriteStartElement("realmCode")
            xmlwriter.WriteAttributeString("code", "US")
            xmlwriter.WriteEndElement() 'End realmCode Element

            xmlwriter.WriteStartElement("typeId")
            xmlwriter.WriteAttributeString("extension", "POCD_HD000040")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.1.3")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CDA/R2")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.27.1776")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1")
            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C32")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.1")
            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("id")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.72")
            xmlwriter.WriteAttributeString("extension", "Laika C32 Test")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "Laika: An Open Source EHR Testing Framework projectlaika.org")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("code")
            xmlwriter.WriteAttributeString("code", "34133-9")
            xmlwriter.WriteAttributeString("displayName", "Summarization of patient data")
            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
            xmlwriter.WriteEndElement() 'End Code Element

            xmlwriter.WriteElementString("title", mPatient.PatientName.FirstName & " " & mPatient.PatientName.LastName)

            xmlwriter.WriteStartElement("effectiveTime")
            Dim dtTodayDate As String = Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & "000000-0500" ' & Now.Hour & Now.Minute
            xmlwriter.WriteAttributeString("value", dtTodayDate) '"19870618000000-0500"datetimestamp when file generated
            xmlwriter.WriteEndElement() 'End effectiveTime Element

            xmlwriter.WriteStartElement("confidentialityCode")
            xmlwriter.WriteAttributeString("code", "N")
            xmlwriter.WriteEndElement() 'End confidentialityCode

            xmlwriter.WriteStartElement("languageCode")
            xmlwriter.WriteAttributeString("code", "en-US") 'datetimestamp when file generated
            xmlwriter.WriteEndElement() 'End Languagecode Element

            '-------------Record target
            xmlwriter.WriteStartElement("recordTarget")
            xmlwriter.WriteStartElement("patientRole")

            xmlwriter.WriteStartElement("id") 'PatientID for the Patient
            xmlwriter.WriteAttributeString("extension", mPatient.PatientName.Code)
            xmlwriter.WriteAttributeString("root", "CLINICID")
            xmlwriter.WriteEndElement() 'End ID Element


            xmlwriter.WriteStartElement("addr")
            xmlwriter.WriteAttributeString("use", "HP")
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Street) AndAlso mPatient.PatientName.PersonContactAddress.Street <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", mPatient.PatientName.PersonContactAddress.Street)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.City) AndAlso mPatient.PatientName.PersonContactAddress.City <> "" Then
                xmlwriter.WriteElementString("city", mPatient.PatientName.PersonContactAddress.City)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.State) AndAlso mPatient.PatientName.PersonContactAddress.State <> "" Then
                xmlwriter.WriteElementString("state", mPatient.PatientName.PersonContactAddress.State)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Zip) AndAlso mPatient.PatientName.PersonContactAddress.Zip <> "" Then
                xmlwriter.WriteElementString("postalCode", mPatient.PatientName.PersonContactAddress.Zip)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Country) AndAlso mPatient.PatientName.PersonContactAddress.Country <> "" Then
                xmlwriter.WriteElementString("country", mPatient.PatientName.PersonContactAddress.Country)
            End If
            xmlwriter.WriteEndElement() 'End addr Element
            xmlwriter.WriteStartElement("telecom")
            If mPatient.Guardian_Phone <> "" Then
                xmlwriter.WriteAttributeString("use", "HP")
                xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & mPatient.Guardian_Phone.Substring(0, 3) & "-" & mPatient.Guardian_Phone.Substring(3, 3) & "-" & mPatient.Guardian_Phone.Substring(3, 4))
            Else
                xmlwriter.WriteAttributeString("use", "HP")
                xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
            End If
            xmlwriter.WriteEndElement() 'telecom id

            xmlwriter.WriteStartElement("patient")
            xmlwriter.WriteStartElement("name")
            'xmlwriter.WriteElementString("prefix", mPatient.PatientName.Prefix)

            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "CL")
            xmlwriter.WriteValue(mPatient.PatientName.FirstName)
            xmlwriter.WriteEndElement() 'End given Element

            xmlwriter.WriteStartElement("family")
            xmlwriter.WriteAttributeString("qualifier", "BR")
            xmlwriter.WriteValue(mPatient.PatientName.LastName)
            xmlwriter.WriteEndElement() 'End family Element

            xmlwriter.WriteEndElement() 'End name Element

            xmlwriter.WriteStartElement("administrativeGenderCode")
            Select Case mPatient.Gender.ToUpper
                Case "FEMALE"
                    xmlwriter.WriteAttributeString("code", "F")
                    xmlwriter.WriteAttributeString("displayName", "Female")
                Case "MALE"
                    xmlwriter.WriteAttributeString("code", "M")
                    xmlwriter.WriteAttributeString("displayName", "Male")
                Case "OTHER"
                    xmlwriter.WriteAttributeString("code", "UN")
                    xmlwriter.WriteAttributeString("displayName", "Undifferentiated")
            End Select

            xmlwriter.WriteAttributeString("codeSystemName", "HL7 AdministrativeGender")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.1")
            xmlwriter.WriteElementString("originalText", "AdministrativeGender codes are: M (Male), F (Female) or UN (Undifferentiated).")
            xmlwriter.WriteEndElement() 'End administrativeGenderCode Element

            xmlwriter.WriteStartElement("birthTime")
            strDate = Format(CType(mPatient.DateofBirth, Date), "yyyyMMdd")
            If strDate <> "" AndAlso strDate <> Nothing Then
                xmlwriter.WriteAttributeString("value", strDate)
            End If
            xmlwriter.WriteEndElement() 'End BirthTime


            xmlwriter.WriteStartElement("maritalStatusCode")
            Select Case mPatient.MaritalStatus.ToUpper
                Case "SEPARATED"
                    xmlwriter.WriteAttributeString("code", "A")
                    mPatient.MaritalStatus = "Annulled"
                    'Case "Common law"
                    '    xmlwriter.WriteAttributeString("code", "C")
                Case "LEGALLY SEPARATED"
                    xmlwriter.WriteAttributeString("code", "L")
                Case "WIDOWED"
                    xmlwriter.WriteAttributeString("code", "W")
                Case "UNMARRIED"
                    'xmlwriter.WriteAttributeString("code", "UN")
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"
                Case "MARRIED"
                    xmlwriter.WriteAttributeString("code", "M")
                Case "SINGLE"
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"
                Case "DIVORCED"
                    xmlwriter.WriteAttributeString("code", "D")
                Case Else
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"
            End Select
            If mPatient.MaritalStatus <> "" AndAlso mPatient.MaritalStatus <> Nothing Then
                xmlwriter.WriteAttributeString("displayName", mPatient.MaritalStatus)
            End If
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.2")
            xmlwriter.WriteAttributeString("codeSystemName", "HL7 Marital status")
            xmlwriter.WriteEndElement() 'maritalStatusCode END

            If mPatient.Race <> "" AndAlso mPatient.Race <> Nothing Then

                xmlwriter.WriteStartElement("raceCode")

                If Not IsNothing(mPatient.RaceCode) AndAlso mPatient.RaceCode.Trim() <> "" Then
                    xmlwriter.WriteAttributeString("code", mPatient.RaceCode)  '..Patient Race code if defined
                Else
                    'xmlwriter.WriteAttributeString("code", mPatient.RaceCode)  '..Default Race code if not defined
                End If

                xmlwriter.WriteAttributeString("displayName", mPatient.Race)
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.238")
                xmlwriter.WriteAttributeString("codeSystemName", "CDC Race and Ethnicity")
                xmlwriter.WriteEndElement() 'raceCode END

            End If

            xmlwriter.WriteStartElement("languageCommunication")
            If Not IsNothing(mPatient.Language) AndAlso mPatient.Language <> "" Then

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.2")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteEndElement() 'End templateId 

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.2.1")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE/PCC")
                xmlwriter.WriteEndElement() 'End templateId

                xmlwriter.WriteStartElement("languageCode")
                'here to get Laungage-Country eg. "en-US" format we first take the 
                If mPatient.LanguageCode <> "" AndAlso mPatient.LanguageCode <> Nothing Then
                    xmlwriter.WriteAttributeString("code", mPatient.LanguageCode)
                Else
                    xmlwriter.WriteAttributeString("code", "en-US")
                End If
                xmlwriter.WriteEndElement() 'End languageCode  


            End If
            xmlwriter.WriteEndElement() 'End languageCommunication element

            xmlwriter.WriteEndElement() 'End Patient Element
            xmlwriter.WriteEndElement() 'End PatientRole Element
            xmlwriter.WriteEndElement() 'End recordTarget Element

            '---------Record Target
            ''Author Element Starts here ,person who generates the CCD file
            xmlwriter.WriteStartElement("author") 'author Element 

            xmlwriter.WriteStartElement("time")
            xmlwriter.WriteAttributeString("value", Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00"))
            xmlwriter.WriteEndElement() 'End time element


            xmlwriter.WriteStartElement("assignedAuthor")

            xmlwriter.WriteStartElement("id")
            'xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
            xmlwriter.WriteEndElement() 'End id

            xmlwriter.WriteStartElement("addr")

            If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            End If

            If Not IsNothing(_clinicCity) AndAlso _clinicCity.Trim() <> "" Then
                xmlwriter.WriteElementString("city", _clinicCity.Trim())
            End If

            If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
                xmlwriter.WriteElementString("state", _clinicState.Trim())
            End If

            If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
                xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            End If

            If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
                xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            End If

            xmlwriter.WriteEndElement() 'End addr Element

            If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
                xmlwriter.WriteEndElement() 'telecom id
            Else
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If


            'xmlwriter.WriteAttributeString("root", mPatient.Author.PersonName.Code)

            Dim OOID As String = "ABC-" & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & Format(DateTime.Now.Millisecond, "#000") & "-JJ"

            xmlwriter.WriteStartElement("assignedPerson")

            xmlwriter.WriteStartElement("name")
            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "CL")
            xmlwriter.WriteValue(mPatient.Author.PersonName.FirstName)
            xmlwriter.WriteEndElement() 'given
            xmlwriter.WriteEndElement() 'name

            xmlwriter.WriteEndElement() 'End Assigned Person

            xmlwriter.WriteStartElement("representedOrganization")

            xmlwriter.WriteElementString("name", mPatient.Author.Organization.Replace(",", ""))

            If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
                xmlwriter.WriteEndElement() 'telecom id
            Else
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If

            xmlwriter.WriteStartElement("addr")

            If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            End If

            If Not IsNothing(_clinicCity) AndAlso _clinicCity.Trim() <> "" Then
                xmlwriter.WriteElementString("city", _clinicCity.Trim())
            End If

            If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
                xmlwriter.WriteElementString("state", _clinicState.Trim())
            End If

            If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
                xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            End If

            If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
                xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            End If

            xmlwriter.WriteEndElement() 'End addr Element

            xmlwriter.WriteEndElement() 'End representedOrganization

            xmlwriter.WriteEndElement() 'End assignedAuthor         

            xmlwriter.WriteEndElement() 'End Author element

            xmlwriter.WriteStartElement("custodian")
            xmlwriter.WriteStartElement("assignedCustodian")
            xmlwriter.WriteStartElement("representedCustodianOrganization")
            xmlwriter.WriteStartElement("id")
            xmlwriter.WriteEndElement() 'End id
            xmlwriter.WriteElementString("name", mPatient.Author.Organization.Replace(",", ""))

            If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
                xmlwriter.WriteEndElement() 'telecom id
            Else
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If

            xmlwriter.WriteStartElement("addr")

            If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            End If

            If Not IsNothing(_clinicCity) AndAlso _clinicCity.Trim() <> "" Then
                xmlwriter.WriteElementString("city", _clinicCity.Trim())
            End If

            If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
                xmlwriter.WriteElementString("state", _clinicState.Trim())
            End If

            If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
                xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            End If

            If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
                xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            End If

            xmlwriter.WriteEndElement() 'End addr Element

            xmlwriter.WriteEndElement()
            xmlwriter.WriteEndElement()
            xmlwriter.WriteEndElement() 'End custodian



            ' ''#################### Documentation of  Component element 

            xmlwriter.WriteStartElement("documentationOf")
            'xmlwriter.WriteAttributeString("typeCode", "DOC")
            xmlwriter.WriteStartElement("serviceEvent")
            xmlwriter.WriteAttributeString("classCode", "PCPR")

            xmlwriter.WriteStartElement("effectiveTime")
            xmlwriter.WriteStartElement("low")
            xmlwriter.WriteAttributeString("value", dtTodayDate)
            xmlwriter.WriteEndElement() 'End low 
            xmlwriter.WriteStartElement("high")
            xmlwriter.WriteAttributeString("value", dtTodayDate)
            xmlwriter.WriteEndElement() 'End high 
            xmlwriter.WriteEndElement() 'End EffectiveTime

            xmlwriter.WriteEndElement() 'End Service Event
            xmlwriter.WriteEndElement() 'End Documentation


            ' ''#################### Parent Component element 
            xmlwriter.WriteStartElement("component")

            ''----------------structuredBody-------------------
            xmlwriter.WriteStartElement("structuredBody")

            ''$$$$$$$$$$$$$ Saagar K -- Component (Allergies and Alerts) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Allergy") = True Or CCDSection = "All" Then

                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Allergies and Alerts")

                xmlwriter.WriteStartElement("section")
                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.2")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteEndElement() 'templateId element END
                '----------code 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "48765-2")
                xmlwriter.WriteAttributeString("displayName", "Alerts")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Allergies and Alert Problems")
                '-----------text
                xmlwriter.WriteStartElement("text")
                'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Type")
                xmlwriter.WriteElementString("th", "SNOMED Code")
                xmlwriter.WriteElementString("th", "Substance")
                xmlwriter.WriteElementString("th", "Reaction")
                xmlwriter.WriteElementString("th", "Status")
                xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement()
                xmlwriter.WriteStartElement("tbody")
                'Code to check whether data is present or not -Code by Shirish
                If Not IsNothing(mPatient.PatientAllergies) AndAlso mPatient.PatientAllergies.Count > 0 Then
                    For Each oAllergies As Allergies In mPatient.PatientAllergies
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Drug Allergy")
                        xmlwriter.WriteElementString("td", oAllergies.ConceptID)
                        xmlwriter.WriteElementString("td", oAllergies.ProductName)
                        xmlwriter.WriteElementString("td", oAllergies.Reaction)
                        xmlwriter.WriteElementString("td", oAllergies.Status)

                        'xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "5")
                    xmlwriter.WriteEndElement() 'End of td.
                    xmlwriter.WriteEndElement() 'tr element end
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END

                'Dim i As Integer = 0
                If Not IsNothing(mPatient.PatientAllergies) Then

                    For Each oAllergies As Allergies In mPatient.PatientAllergies
                        'i = i + 1

                        '-----------entry
                        xmlwriter.WriteStartElement("entry")
                        '-----------act
                        xmlwriter.WriteStartElement("act")
                        xmlwriter.WriteAttributeString("classCode", "ACT")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")
                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.27")
                        xmlwriter.WriteEndElement() 'templateId element END
                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.6")
                        xmlwriter.WriteEndElement() 'templateId element END


                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END
                        '-----------code
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("nullFlavor", "NA")
                        xmlwriter.WriteEndElement() 'code element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")
                        'xmlwriter.WriteAttributeString("inversionInd", "false")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.18")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END


                        '-----------observation - code
                        xmlwriter.WriteStartElement("code")
                        'retrieve the code from the "Adverse_Event_Types" table
                        oAllergies.AllergyType = "Allergy to Substance" 'for time being madam asked to give har coded value to the porperty procedure
                        xmlwriter.WriteAttributeString("code", "419199007")
                        xmlwriter.WriteAttributeString("displayName", "Allergy to Substance")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteEndElement() 'observation - code element END



                        '-----------statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'statusCode element END

                        strDate = Format(Date.Now, "yyyyMMdd")
                        '-----------effectiveTime
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '-----------observation - participant
                        xmlwriter.WriteStartElement("participant")
                        xmlwriter.WriteAttributeString("typeCode", "CSM")
                        '-----------participant - participantRole
                        xmlwriter.WriteStartElement("participantRole")
                        xmlwriter.WriteAttributeString("classCode", "MANU")
                        '-----------participantRole - playingEntity
                        xmlwriter.WriteStartElement("playingEntity")
                        xmlwriter.WriteAttributeString("classCode", "MMAT")
                        '-----------playingEntity - code

                        'when we retrieve te allergies using the spGetLatestAllergies against that patient then,
                        'we also retrieve the Drug ID against that patient. if the drug id = 0, then pass a harcoded value for RxNorm Code
                        ' else if there is DrugId then get the top first sNDCCode from Drugs_Mst aginst that drugId and pass to the getRxNormCode() and put the RxNorm Code.
                        ' if the RxNormCode against that NDC is empty then pass hardcoded value else pass the RxNormCode value that was retrieved.

                        Dim _sNDCCode As String = ""
                        Dim _sRXNormCode As String = ""
                        If oAllergies.ProductCode <> "" Then
                            'now oAllergies.ProductCode contains the drugId value that is populated using the Sp_GetLatestAllergies proc, 
                            'therefore get the sNDCCode against that drugID and then retrieve the appropriate RxNorm Code
                            '_sNDCCode = ogloCCDDBLayer.GetNDCCode(oAllergies.ProductCode)
                            _sNDCCode = oAllergies.ProductCode

                        Else 'oAllergies.ProductCode =0
                            'means there is no drugId found using the Sp_GetLatestAllergies proc. therefore pass the hardcoded value '1001'
                            'oAllergies.ProductCode = "1001"
                        End If
                        xmlwriter.WriteStartElement("code")
                        If oAllergies.ProductCode <> "" AndAlso oAllergies.ProductCode <> Nothing Then
                            xmlwriter.WriteAttributeString("code", oAllergies.ProductCode)
                        End If

                        If oAllergies.ProductName <> "" AndAlso oAllergies.ProductName <> Nothing Then
                            xmlwriter.WriteAttributeString("displayName", oAllergies.ProductName)
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.88")
                        xmlwriter.WriteAttributeString("codeSystemName", "RxNorm")
                        'xmlwriter.WriteElementString("originalText", oAllergies.ProductName)
                        xmlwriter.WriteEndElement() 'playingEntity - code element END

                        '-----------playingEntity - name
                        xmlwriter.WriteElementString("name", oAllergies.ProductName)
                        xmlwriter.WriteEndElement() 'participantRole - playingEntity element END

                        xmlwriter.WriteEndElement() 'participant - participantRole element END
                        xmlwriter.WriteEndElement() 'observation - participant element END

                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        xmlwriter.WriteEndElement() 'act element END
                        xmlwriter.WriteEndElement() 'entry element END
                    Next
                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END

            End If

            '*****************************Code commented by supriya *****************************************************

            ' ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            ' ''$$$$$$$$$$$$$ Saagar K -- Component (Medications) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Medications") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Medications")

                xmlwriter.WriteStartElement("section")
                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.8")
                xmlwriter.WriteEndElement() 'templateId element END
                '----------code 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "10160-0")
                xmlwriter.WriteAttributeString("displayName", "History of medication use")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")

                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Medications")
                '-----------text
                xmlwriter.WriteStartElement("text")
                'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

                If Not IsNothing(mPatient.PatientMedications) AndAlso mPatient.PatientMedications.Count > 0 Then

                    For Each oMedications As Medication In mPatient.PatientMedications

                        xmlwriter.WriteStartElement("table")
                        xmlwriter.WriteAttributeString("width", "100%")
                        xmlwriter.WriteStartElement("col")
                        xmlwriter.WriteAttributeString("width", "50%")
                        xmlwriter.WriteEndElement() 'col element end
                        xmlwriter.WriteStartElement("col")
                        xmlwriter.WriteAttributeString("width", "50%")
                        xmlwriter.WriteEndElement() 'col element end
                        xmlwriter.WriteStartElement("tbody")
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.DrugName)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end

                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteAttributeString("align", "right")
                        '' GLO2011-0013154 : on CCD for the Medications needs to have the "issued on" changed to "verified on" 
                        '' Issued on changed with Verified on
                        xmlwriter.WriteElementString("content", "Verified on " + oMedications.MedicationDate)

                        'xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end
                        xmlwriter.WriteEndElement() 'Tbody element end
                        xmlwriter.WriteEndElement() ' table element end

                        xmlwriter.WriteStartElement("table")
                        xmlwriter.WriteAttributeString("width", "100%")
                        xmlwriter.WriteAttributeString("border", "1")
                        xmlwriter.WriteStartElement("col")
                        xmlwriter.WriteAttributeString("width", "12%")
                        xmlwriter.WriteEndElement() 'col element end
                        xmlwriter.WriteStartElement("col")
                        xmlwriter.WriteAttributeString("width", "88%")
                        xmlwriter.WriteEndElement() 'col element end
                        xmlwriter.WriteStartElement("tbody")

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "NDC Code:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.ProdCode)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Strength:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.DrugStrength)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Dose:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.DrugQuantity + " " + oMedications.DrugForm)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Quantity:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.DrugQuantity)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Days:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Days)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Refills:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Refills)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Sig:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Frequency)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Route:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Route)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Diagnosis:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.CheifComplaint)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Pharmacy:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Pharmacy)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Status:")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteStartElement("content")
                        xmlwriter.WriteAttributeString("styleCode", "Bold")
                        xmlwriter.WriteElementString("content", oMedications.Status)
                        xmlwriter.WriteEndElement() 'content element end
                        xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end

                        xmlwriter.WriteEndElement() 'Tbody element end
                        xmlwriter.WriteEndElement() ' table element end
                        xmlwriter.WriteStartElement("paragraph")
                        xmlwriter.WriteEndElement() 'paragraph element end

                    Next

                End If
                xmlwriter.WriteEndElement() 'text element END

                If Not IsNothing(mPatient.PatientMedications) AndAlso mPatient.PatientMedications.Count > 0 Then
                    For Each oMedications As Medication In mPatient.PatientMedications

                        '-----------entry
                        xmlwriter.WriteStartElement("entry")

                        '-----------entry - substanceAdministration 
                        xmlwriter.WriteStartElement("substanceAdministration")
                        xmlwriter.WriteAttributeString("classCode", "SBADM")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.24")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END
                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C32")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.8")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END
                        '-------------id

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'End id

                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "Active")
                        xmlwriter.WriteEndElement()

                        '-----------entry - substanceAdministration - consumable  
                        xmlwriter.WriteStartElement("consumable")

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct  
                        xmlwriter.WriteStartElement("manufacturedProduct")
                        xmlwriter.WriteAttributeString("classCode", "MANU")
                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.53")
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END
                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C32")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.9")
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - manufacturedMaterial 
                        xmlwriter.WriteStartElement("manufacturedMaterial")
                        xmlwriter.WriteAttributeString("classCode", "MMAT")
                        'xmlwriter.WriteAttributeString("determinerCode", "KIND")

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - code 
                        xmlwriter.WriteStartElement("code")
                        If oMedications.ProdCode <> "" AndAlso oMedications.ProdCode <> Nothing Then
                            xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
                        End If
                        If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                            xmlwriter.WriteAttributeString("displayName", oMedications.DrugName)
                        End If

                        xmlwriter.WriteAttributeString("codeSystemName", "NDC")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.69")
                        xmlwriter.WriteElementString("originalText", oMedications.DrugName)
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - code element END

                        xmlwriter.WriteElementString("name", oMedications.DrugName)

                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - manufacturedMaterial element END

                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - consumable - manufacturedProduct element END

                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - consumable element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.10")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------code 
                        xmlwriter.WriteStartElement("code")
                        'If oMedications.ProdCode <> "" AndAlso oMedications.ProdCode <> Nothing Then
                        '    xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
                        'End If
                        xmlwriter.WriteAttributeString("code", "73639000")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("displayName", "Prescription Drug")
                        'If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                        '    xmlwriter.WriteAttributeString("displayName", oMedications.DrugName)
                        'End If
                        xmlwriter.WriteEndElement() 'code element end

                        ''-----------statusCode
                        'xmlwriter.WriteStartElement("statusCode")
                        'xmlwriter.WriteAttributeString("code", "completed")
                        'xmlwriter.WriteEndElement() 'statuscode element end

                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "REFR")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.47")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------code 
                        xmlwriter.WriteStartElement("code")
                        'If oMedications.ProdCode <> "" AndAlso oMedications.ProdCode <> Nothing Then
                        '    xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
                        'End If

                        xmlwriter.WriteAttributeString("code", "33999-4")
                        xmlwriter.WriteAttributeString("displayName", "Status")
                        xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                        xmlwriter.WriteEndElement() 'code element end

                        '-----------value 
                        xmlwriter.WriteStartElement("value")
                        'If oMedications.ProdCode <> "" AndAlso oMedications.ProdCode <> Nothing Then
                        '    xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
                        'End If
                        'If oMedications.Status <> "" AndAlso oMedications.Status <> Nothing Then
                        '    xmlwriter.WriteAttributeString("displayName", oMedications.Status)
                        'End If
                        xmlwriter.WriteAttributeString("code", "55561003")
                        xmlwriter.WriteAttributeString("displayName", "Active")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("xsi:type", "CE")
                        xmlwriter.WriteEndElement() 'code element end

                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "REFR")

                        '-----------supply
                        xmlwriter.WriteStartElement("supply")
                        xmlwriter.WriteAttributeString("classCode", "SPLY")
                        xmlwriter.WriteAttributeString("moodCode", "INT")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.8.3")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.34")
                        xmlwriter.WriteEndElement() 'templateId element END
                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7.3")
                        xmlwriter.WriteEndElement() 'templateId element END

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'End id

                        strDate = Format(CType(oMedications.MedicationDate, Date), "yyyyMMdd")
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        xmlwriter.WriteEndElement() 'supply element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        xmlwriter.WriteEndElement() 'entry- substanceAdministration element END

                        xmlwriter.WriteEndElement() 'entry element END

                    Next
                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END
            End If

            xmlwriter.WriteEndElement() '------------structuredBody Element Ends here
            xmlwriter.WriteEndElement() '############Parent component Element Ends here

            xmlwriter.WriteEndElement() 'End Clinical Document Element
            xmlwriter.WriteEndDocument()
            xmlwriter.Close()
            Return strfilepath
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)

        Finally
            'Memory Leak
            If Not IsNothing(ogloCCDDBLayer) Then
                ogloCCDDBLayer.VisitID = 0
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            xmlwriter = Nothing
            strDate = Nothing
            _clinicName = Nothing
            _clinicAddr = Nothing
            _clinicCity = Nothing
            _clinicState = Nothing
            _cliniPostalCode = Nothing
            _clinicCountry = Nothing
            _clinicCounty = Nothing
            _clinicPhone = Nothing

        End Try

    End Function


    'New Main Function...will integrate one by one section and will do live testing for MU certification - Start
    Public Function GenerateCCD(ByVal CCDSection As String, Optional ByVal dtvitals As DataTable = Nothing, Optional ByVal PatientLastName As String = "", Optional ByVal _FinalCCDSavePath As String = "") As String
        Dim dtCPT As DataTable = Nothing
        Dim ogloCCDDBLayer As New gloCCDDatabaseLayer
        Dim dt As DataTable = Nothing
        Dim strDate As String 'this variable is used for formation date in yyyyMMdd format from the objects
        Dim _dtClinicInfo As DataTable = Nothing

        Dim _clinicName As String = ""
        Dim _clinicAddr As String = ""
        Dim _clinicCity As String = ""
        Dim _clinicState As String = ""
        Dim _cliniPostalCode As String = ""
        Dim _clinicCountry As String = ""
        Dim _clinicCounty As String = ""
        Dim _clinicPhone As String = ""
        Dim xmlwriter As XmlTextWriter = Nothing

        Try

            '    Set Clinic Information - Start 

            _dtClinicInfo = ogloCCDDBLayer.GetClinicData()
            If Not IsNothing(_dtClinicInfo) AndAlso _dtClinicInfo.Rows.Count > 0 Then

                _clinicName = _dtClinicInfo.Rows(0)("ClinicName")
                _clinicAddr = _dtClinicInfo.Rows(0)("AddressLine1")
                _clinicCity = _dtClinicInfo.Rows(0)("City")
                _clinicState = _dtClinicInfo.Rows(0)("State")
                _cliniPostalCode = _dtClinicInfo.Rows(0)("PostalCode")
                _clinicCounty = _dtClinicInfo.Rows(0)("sCounty")
                _clinicCountry = _dtClinicInfo.Rows(0)("Country")
                _clinicPhone = _dtClinicInfo.Rows(0)("Phone")

            End If
            If Not IsNothing(_dtClinicInfo) Then
                _dtClinicInfo.Dispose()
                _dtClinicInfo = Nothing
            End If
            Dim strfilepath As String = ""
            '    Set Clinic Information - End 
            If _FinalCCDSavePath <> "" Then
                strfilepath = _FinalCCDSavePath
            Else
                strfilepath = GenerateFileName(PatientLastName)
            End If



            Try
                If System.IO.File.Exists(strfilepath) Then
                    System.IO.File.Delete(strfilepath)
                End If
                xmlwriter = New XmlTextWriter(strfilepath, Nothing)
            Catch ex As Exception
                Throw New gloCCDException("You do not have write permissions for the CCD directory." & vbCrLf & "Contact your system administrator or configure a different directory for CCD.")
                Exit Function
            End Try


            xmlwriter.Formatting = Formatting.Indented

            xmlwriter.WriteStartDocument()
            '
            'Dim _myStyle As String = "type='text/xsl' href='http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl'"
            Dim _myStyle As String = "type='text/xsl' href='" & gloCCDSchema.gloCDAWriterParameters.CDAStyleSheetPath & "'"

            xmlwriter.WriteProcessingInstruction("xml-stylesheet", _myStyle)

            xmlwriter.WriteStartElement("ClinicalDocument") 'Open the Main Parent Node 
            'xmlwriter.WriteAttributeString("stylesheet", "type='text/xsl' href = 'xslt\Stylesheet.xsl'")

            xmlwriter.WriteAttributeString("xsi:schemaLocation", "urn:hl7-org:v3 http://xreg2.nist.gov:8080/hitspValidation/schema/cdar2c32/infrastructure/cda/C32_CDA.xsd")
            xmlwriter.WriteAttributeString("xmlns:sdtc", "urn:hl7-org:sdtc")
            xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlwriter.WriteAttributeString("xmlns", "urn:hl7-org:v3")

            xmlwriter.WriteStartElement("realmCode")
            xmlwriter.WriteAttributeString("code", "US")
            xmlwriter.WriteEndElement() 'End realmCode Element

            xmlwriter.WriteStartElement("typeId")
            xmlwriter.WriteAttributeString("extension", "POCD_HD000040")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.1.3")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.27.1776")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CDA/R2")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.3")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.1")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C32")
            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.1.1")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE/PCC")

            xmlwriter.WriteEndElement() 'End templateId Element

            xmlwriter.WriteStartElement("id")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.72")
            xmlwriter.WriteAttributeString("extension", "Laika C32 Test")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "Laika: An Open Source EHR Testing Framework projectlaika.org")
            xmlwriter.WriteEndElement() 'End TypeId Element

            xmlwriter.WriteStartElement("code")
            xmlwriter.WriteAttributeString("code", "34133-9")
            xmlwriter.WriteAttributeString("displayName", "Summarization of patient data")
            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
            xmlwriter.WriteEndElement() 'End Code Element

            xmlwriter.WriteElementString("title", mPatient.PatientName.FirstName & " " & mPatient.PatientName.LastName)

            xmlwriter.WriteStartElement("effectiveTime")
            Dim dtTodayDate As String = Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & "000000-0500" '& Now.Hour & Now.Minute
            xmlwriter.WriteAttributeString("value", dtTodayDate) '"19870618000000-0500"datetimestamp when file generated
            xmlwriter.WriteEndElement() 'End effectiveTime Element

            xmlwriter.WriteStartElement("confidentialityCode")
            xmlwriter.WriteAttributeString("code", "N")
            xmlwriter.WriteEndElement() 'End confidentialityCode

            xmlwriter.WriteStartElement("languageCode")
            xmlwriter.WriteAttributeString("code", "en-US") 'datetimestamp when file generated
            xmlwriter.WriteEndElement() 'End Languagecode Element

            '-------------Record target
            xmlwriter.WriteStartElement("recordTarget")
            xmlwriter.WriteStartElement("patientRole")

            xmlwriter.WriteStartElement("id") 'PatientID for the Patient
            'xmlwriter.WriteAttributeString("extension", mPatient.PatientName.Code)
            xmlwriter.WriteAttributeString("extension", mPatient.PatientID)
            xmlwriter.WriteAttributeString("root", "CLINICID")
            xmlwriter.WriteEndElement() 'End ID Element


            xmlwriter.WriteStartElement("addr")
            xmlwriter.WriteAttributeString("use", "HP")
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Street) AndAlso mPatient.PatientName.PersonContactAddress.Street <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", mPatient.PatientName.PersonContactAddress.Street)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.AddressLine2) AndAlso mPatient.PatientName.PersonContactAddress.AddressLine2 <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", mPatient.PatientName.PersonContactAddress.AddressLine2)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.City) AndAlso mPatient.PatientName.PersonContactAddress.City <> "" Then
                xmlwriter.WriteElementString("city", mPatient.PatientName.PersonContactAddress.City)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.State) AndAlso mPatient.PatientName.PersonContactAddress.State <> "" Then
                xmlwriter.WriteElementString("state", mPatient.PatientName.PersonContactAddress.State)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Zip) AndAlso mPatient.PatientName.PersonContactAddress.Zip <> "" Then
                xmlwriter.WriteElementString("postalCode", mPatient.PatientName.PersonContactAddress.Zip)
            End If
            If Not IsNothing(mPatient.PatientName.PersonContactAddress.Country) AndAlso mPatient.PatientName.PersonContactAddress.Country <> "" Then
                xmlwriter.WriteElementString("country", mPatient.PatientName.PersonContactAddress.Country)
            End If
            xmlwriter.WriteEndElement() 'End addr Element

            If mPatient.Phone <> "" Then
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("use", "HP")
                xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & mPatient.Phone.Substring(0, 3) & "-" & mPatient.Phone.Substring(3, 3) & "-" & mPatient.Phone.Substring(6, 4))
                xmlwriter.WriteEndElement() 'telecom id

            Else
                xmlwriter.WriteStartElement("telecom")
                'xmlwriter.WriteAttributeString("use", "HP")
                'xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If


            xmlwriter.WriteStartElement("patient")
            xmlwriter.WriteStartElement("name")

            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "CL")
            xmlwriter.WriteValue(mPatient.PatientName.FirstName)
            xmlwriter.WriteEndElement() 'End given Element

            xmlwriter.WriteStartElement("family")
            xmlwriter.WriteAttributeString("qualifier", "BR")
            xmlwriter.WriteValue(mPatient.PatientName.LastName)
            xmlwriter.WriteEndElement() 'End family Element

            xmlwriter.WriteEndElement() 'End name Element

            xmlwriter.WriteStartElement("administrativeGenderCode")
            Select Case mPatient.Gender.ToUpper
                Case "FEMALE"
                    xmlwriter.WriteAttributeString("code", "F")
                    xmlwriter.WriteAttributeString("displayName", "Female")
                Case "MALE"
                    xmlwriter.WriteAttributeString("code", "M")
                    xmlwriter.WriteAttributeString("displayName", "Male")
                Case "OTHER"
                    xmlwriter.WriteAttributeString("code", "UN")
                    xmlwriter.WriteAttributeString("displayName", "Undifferentiated")
            End Select

            xmlwriter.WriteAttributeString("codeSystemName", "HL7 AdministrativeGender")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.1")
            xmlwriter.WriteElementString("originalText", "AdministrativeGender codes are: M (Male), F (Female) or UN (Undifferentiated).")
            xmlwriter.WriteEndElement() 'End administrativeGenderCode Element

            xmlwriter.WriteStartElement("birthTime")
            strDate = Format(CType(mPatient.DateofBirth, Date), "yyyyMMdd")
            If strDate <> "" AndAlso strDate <> Nothing Then
                xmlwriter.WriteAttributeString("value", strDate)
            End If
            xmlwriter.WriteEndElement() 'End BirthTime

            'As per CCD/CCHIT11/HITSP_C32_v_2_5/voc.xml#
            '<code value="A" display_name="Annulled" description="Marriage contract has been declared null and to not have existed" /> 
            '<code value="D" display_name="Divorced" description="Marriage contract has been declared dissolved and inactive" /> 
            '<code value="T" display_name="Domestic partner" description="Person declares that a domestic partner relationship exists." /> 
            '<code value="I" display_name="Interlocutory" description="Subject to an Interlocutory Decree." /> 
            '<code value="L" display_name="Legally Separated" description="" /> 
            '<code value="M" display_name="Married" description="A current marriage contract is active" /> 
            '<code value="S" display_name="Never Married" description="No marriage contract has ever been entered" /> 
            '<code value="P" display_name="Polygamous" description="More than 1 current spouse" /> 
            '<code value="W" display_name="Widowed" description="The spouse has died" />

            'MU standard as per HL7 Marital status
            'a(Separated)
            'D(Divorced)
            'L(Legally Separated)
            'M(Married)
            'P	Domestic partner
            'S	Single
            'T(Unreported)
            'W(Widowed)

            xmlwriter.WriteStartElement("maritalStatusCode")
            Select Case mPatient.MaritalStatus.ToUpper
                Case "SEPARATED"
                    xmlwriter.WriteAttributeString("code", "A")
                    mPatient.MaritalStatus = "Annulled"
                    'Case "Common law"
                    '    xmlwriter.WriteAttributeString("code", "C")
                Case "LEGALLY SEPARATED"
                    xmlwriter.WriteAttributeString("code", "L")
                Case "WIDOWED"
                    xmlwriter.WriteAttributeString("code", "W")
                Case "UNMARRIED"
                    'xmlwriter.WriteAttributeString("code", "UN")
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"
                Case "MARRIED"
                    xmlwriter.WriteAttributeString("code", "M")
                Case "SINGLE"
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"
                Case "DIVORCED"
                    xmlwriter.WriteAttributeString("code", "D")
                Case Else
                    xmlwriter.WriteAttributeString("code", "S")
                    mPatient.MaritalStatus = "Never Married"

            End Select
            If mPatient.MaritalStatus <> "" AndAlso mPatient.MaritalStatus <> Nothing Then
                xmlwriter.WriteAttributeString("displayName", mPatient.MaritalStatus)
            End If
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.2")
            xmlwriter.WriteAttributeString("codeSystemName", "HL7 Marital status")
            xmlwriter.WriteEndElement() 'maritalStatusCode END


            If mPatient.Race <> "" AndAlso mPatient.Race <> Nothing Then

                xmlwriter.WriteStartElement("raceCode")

                If Not IsNothing(mPatient.RaceCode) AndAlso mPatient.RaceCode.Trim() <> "" Then
                    xmlwriter.WriteAttributeString("code", mPatient.RaceCode)  '..Patient Race code if defined
                Else
                    'xmlwriter.WriteAttributeString("code", mPatient.RaceCode)  '..Default Race code if not defined
                End If

                xmlwriter.WriteAttributeString("displayName", mPatient.Race)
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.238")
                xmlwriter.WriteAttributeString("codeSystemName", "CDC Race and Ethnicity")
                xmlwriter.WriteEndElement() 'raceCode END

            End If


            xmlwriter.WriteStartElement("ethnicGroupCode")
            If mPatient.ethnicGroupCode <> "" AndAlso mPatient.ethnicGroupCode <> Nothing Then
                xmlwriter.WriteAttributeString("code", mPatient.ethnicGroupCode)
            End If
            If mPatient.Ethnicity <> "" AndAlso mPatient.Ethnicity <> Nothing Then
                xmlwriter.WriteAttributeString("displayName", mPatient.Ethnicity)
            End If
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.238")
            xmlwriter.WriteAttributeString("codeSystemName", "CDC Race and Ethnicity")
            xmlwriter.WriteEndElement() 'ethnicGroupCode END


            ''Modified by Mayuri:20111215-To remove warnings in NIST Validator
            ''Added address,telecom etc. 
            xmlwriter.WriteStartElement("guardian")
            xmlwriter.WriteAttributeString("classCode", "GUARD")
            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.3")
            xmlwriter.WriteEndElement() 'End templateId
            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.2.4")
            xmlwriter.WriteEndElement() 'End templateId
            xmlwriter.WriteStartElement("templateId")

            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.3")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "C32 Support Module")
            xmlwriter.WriteEndElement() 'End templateId

            xmlwriter.WriteStartElement("id")
            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
            xmlwriter.WriteEndElement() 'End id
            'xmlwriter.WriteAttributeString("classCode", mPatient.PatientSupport.Contacttype)
            xmlwriter.WriteStartElement("code")
            xmlwriter.WriteAttributeString("code", """")
            xmlwriter.WriteAttributeString("displayName", """")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.111")
            xmlwriter.WriteAttributeString("codeSystemName", "RoleCode")
            xmlwriter.WriteEndElement()
            If Not IsNothing(mPatient.Guardian_Address1) AndAlso mPatient.Guardian_Address1 <> "" Then
                xmlwriter.WriteStartElement("addr")
                xmlwriter.WriteElementString("streetAddressLine", mPatient.Guardian_Address1)
                xmlwriter.WriteElementString("city", mPatient.Guardian_City)
                xmlwriter.WriteElementString("state", mPatient.Guardian_State)
                xmlwriter.WriteElementString("postalCode", mPatient.Guardian_ZIP)
                xmlwriter.WriteEndElement() 'End addr
            End If

            xmlwriter.WriteStartElement("telecom")

            If mPatient.Guardian_Phone <> "" AndAlso mPatient.Guardian_Phone <> Nothing Then
                xmlwriter.WriteAttributeString("use", "HP")
                xmlwriter.WriteAttributeString("value", "tel:1" & "-" & mPatient.Guardian_Phone.Substring(0, 3) & "-" & mPatient.Guardian_Phone.Substring(3, 3) & "-" & mPatient.Guardian_Phone.Substring(3, 4))
            End If

            xmlwriter.WriteEndElement() 'telecom id

            xmlwriter.WriteStartElement("guardianPerson")
            xmlwriter.WriteStartElement("name")

            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "CL")
            xmlwriter.WriteValue(mPatient.Guardian_fName)
            xmlwriter.WriteEndElement() 'End given Element

            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "BR")
            xmlwriter.WriteValue(mPatient.Guardian_mName)
            xmlwriter.WriteEndElement() 'End given Element

            xmlwriter.WriteStartElement("family")
            xmlwriter.WriteAttributeString("qualifier", "BR")
            xmlwriter.WriteValue(mPatient.Guardian_lName)
            xmlwriter.WriteEndElement() 'End given Element

            xmlwriter.WriteEndElement() 'name
            xmlwriter.WriteEndElement() 'guardianPerson id

            xmlwriter.WriteEndElement() 'guardian END
            ''START  languageCommunication
            xmlwriter.WriteStartElement("languageCommunication")
            If Not IsNothing(mPatient.Language) AndAlso mPatient.Language <> "" Then

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.2")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteEndElement() 'End templateId 

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.2.1")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE/PCC")
                xmlwriter.WriteEndElement() 'End templateId

                xmlwriter.WriteStartElement("languageCode")
                'here to get Laungage-Country eg. "en-US" format we first take the 
                If mPatient.LanguageCode <> "" AndAlso mPatient.LanguageCode <> Nothing Then
                    xmlwriter.WriteAttributeString("code", mPatient.LanguageCode)
                Else
                    xmlwriter.WriteAttributeString("code", "en-US")
                End If
                xmlwriter.WriteEndElement() 'End languageCode  
            End If
            xmlwriter.WriteEndElement() 'End languageCommunication element


            xmlwriter.WriteEndElement() 'End Patient Element
            xmlwriter.WriteEndElement() 'End PatientRole Element
            xmlwriter.WriteEndElement() 'End recordTarget Element

            '---------Record Target
            ''Author Element Starts here ,person who generates the CCD file
            Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
            If Not IsNothing(mPatient.PatientProviders) AndAlso mPatient.PatientProviders.Count > 0 Then
                oProvider = mPatient.PatientProviders.Item(0)
            End If
            xmlwriter.WriteStartElement("author") 'author Element 

            xmlwriter.WriteStartElement("time")
            xmlwriter.WriteAttributeString("value", dtTodayDate) 'Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & Format(Now.Hour, "#00") & Format(Now.Minute, "#00") & Format(Now.Second, "#00")
            xmlwriter.WriteEndElement() 'End time element


            xmlwriter.WriteStartElement("assignedAuthor")

            xmlwriter.WriteStartElement("id")
            'xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
            xmlwriter.WriteEndElement() 'End id

            xmlwriter.WriteStartElement("addr")
            If oProvider.StreetAddress IsNot Nothing AndAlso oProvider.StreetAddress <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", oProvider.StreetAddress)
            End If
            'If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
            '    xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            'End If
            If oProvider.City IsNot Nothing AndAlso oProvider.City <> "" Then
                xmlwriter.WriteElementString("city", oProvider.City)
            End If
            If oProvider.State IsNot Nothing AndAlso oProvider.State <> "" Then
                xmlwriter.WriteElementString("state", oProvider.State)
            End If
            If oProvider.zip IsNot Nothing AndAlso oProvider.zip <> "" Then
                xmlwriter.WriteElementString("postalCode", oProvider.zip)
            End If
            If oProvider.Country IsNot Nothing AndAlso oProvider.Country <> "" Then
                xmlwriter.WriteElementString("country", oProvider.Country)
            End If
            'If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
            '    xmlwriter.WriteElementString("state", _clinicState.Trim())
            'End If

            'If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
            '    xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            'End If

            'If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
            '    xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            'End If

            xmlwriter.WriteEndElement() 'End addr Element

            If oProvider.WorkPhone IsNot Nothing AndAlso oProvider.WorkPhone <> "" AndAlso oProvider.WorkPhone.Length = 10 Then

                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & oProvider.WorkPhone.Substring(0, 3) & "-" & oProvider.WorkPhone.Substring(3, 3) & "-" & oProvider.WorkPhone.Substring(6, 4))
                xmlwriter.WriteEndElement() 'telecom id
            Else
                xmlwriter.WriteStartElement("telecom")
                '  xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If
            'If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
            '    xmlwriter.WriteStartElement("telecom")
            '    xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
            '    xmlwriter.WriteEndElement() 'telecom id
            'Else
            '    xmlwriter.WriteStartElement("telecom")
            '    xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
            '    xmlwriter.WriteEndElement() 'telecom id
            'End If


            'xmlwriter.WriteAttributeString("root", mPatient.Author.PersonName.Code)

            Dim OOID As String = "ABC-" & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & Format(DateTime.Now.Millisecond, "#000") & "-JJ"

            xmlwriter.WriteStartElement("assignedPerson")

            xmlwriter.WriteStartElement("name")
            xmlwriter.WriteStartElement("given")
            xmlwriter.WriteAttributeString("qualifier", "CL")
            xmlwriter.WriteValue(oProvider.FirstName)
            xmlwriter.WriteEndElement() 'given
            xmlwriter.WriteStartElement("family")
            xmlwriter.WriteValue(oProvider.LastName)
            xmlwriter.WriteEndElement() 'family

            xmlwriter.WriteStartElement("suffix")
            xmlwriter.WriteValue(oProvider.Suffix)
            xmlwriter.WriteEndElement() 'suffix
            xmlwriter.WriteEndElement() 'name


            xmlwriter.WriteEndElement() 'End Assigned Person

            'xmlwriter.WriteStartElement("representedOrganization")

            'xmlwriter.WriteElementString("name", mPatient.Author.Organization.Replace(",", ""))

            'If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
            '    xmlwriter.WriteStartElement("telecom")
            '    xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
            '    xmlwriter.WriteEndElement() 'telecom id
            'Else
            '    xmlwriter.WriteStartElement("telecom")
            '    xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
            '    xmlwriter.WriteEndElement() 'telecom id
            'End If

            'xmlwriter.WriteStartElement("addr")

            'If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
            '    xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            'End If

            'If Not IsNothing(_clinicCity) AndAlso _clinicCity.Trim() <> "" Then
            '    xmlwriter.WriteElementString("city", _clinicCity.Trim())
            'End If

            'If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
            '    xmlwriter.WriteElementString("state", _clinicState.Trim())
            'End If

            'If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
            '    xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            'End If

            'If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
            '    xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            'End If

            'xmlwriter.WriteEndElement() 'End addr Element

            'xmlwriter.WriteEndElement() 'End representedOrganization

            xmlwriter.WriteEndElement() 'End assignedAuthor         

            xmlwriter.WriteEndElement() 'End Author element

            xmlwriter.WriteStartElement("custodian")
            xmlwriter.WriteStartElement("assignedCustodian")
            xmlwriter.WriteStartElement("representedCustodianOrganization")
            xmlwriter.WriteStartElement("id")
            xmlwriter.WriteEndElement() 'End id
            xmlwriter.WriteElementString("name", mPatient.Author.Organization.Replace(",", ""))

            If Not IsNothing(_clinicPhone) AndAlso _clinicPhone.Trim() <> "" AndAlso _clinicPhone.Trim().Length = 10 Then
                xmlwriter.WriteStartElement("telecom")
                xmlwriter.WriteAttributeString("value", "tel:+1" & "-" & _clinicPhone.Substring(0, 3) & "-" & _clinicPhone.Substring(3, 3) & "-" & _clinicPhone.Substring(3, 4))
                xmlwriter.WriteEndElement() 'telecom id
            Else
                xmlwriter.WriteStartElement("telecom")
                '  xmlwriter.WriteAttributeString("value", "tel:+1-000-000-0000")
                xmlwriter.WriteEndElement() 'telecom id
            End If

            xmlwriter.WriteStartElement("addr")

            If Not IsNothing(_clinicAddr) AndAlso _clinicAddr.Trim() <> "" Then
                xmlwriter.WriteElementString("streetAddressLine", _clinicAddr.Trim())
            End If

            If Not IsNothing(_clinicCity) AndAlso _clinicCity.Trim() <> "" Then
                xmlwriter.WriteElementString("city", _clinicCity.Trim())
            End If

            If Not IsNothing(_clinicState) AndAlso _clinicState.Trim() <> "" Then
                xmlwriter.WriteElementString("state", _clinicState.Trim())
            End If

            If Not IsNothing(_cliniPostalCode) AndAlso _cliniPostalCode.Trim() <> "" Then
                xmlwriter.WriteElementString("postalCode", _cliniPostalCode.Trim())
            End If

            If Not IsNothing(_clinicCountry) AndAlso _clinicCountry.Trim() <> "" Then
                xmlwriter.WriteElementString("country", _clinicCountry.Trim())
            End If

            xmlwriter.WriteEndElement() 'End addr Element

            xmlwriter.WriteEndElement()
            xmlwriter.WriteEndElement()
            xmlwriter.WriteEndElement() 'End custodian


            xmlwriter.WriteStartElement("documentationOf")
            'xmlwriter.WriteAttributeString("typeCode", "DOC")
            xmlwriter.WriteStartElement("serviceEvent")
            xmlwriter.WriteAttributeString("classCode", "PCPR")

            xmlwriter.WriteStartElement("effectiveTime")
            xmlwriter.WriteStartElement("low")
            xmlwriter.WriteAttributeString("value", dtTodayDate)
            xmlwriter.WriteEndElement() 'End low 
            xmlwriter.WriteStartElement("high")
            xmlwriter.WriteAttributeString("value", dtTodayDate)
            xmlwriter.WriteEndElement() 'End high 
            xmlwriter.WriteEndElement() 'End EffectiveTime

            xmlwriter.WriteEndElement() 'End Service Event
            xmlwriter.WriteEndElement() 'End Documentation


            ' ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ' ''#################### Parent Component element 
            xmlwriter.WriteStartElement("component")
            ''----------------structuredBody-------------------
            xmlwriter.WriteStartElement("structuredBody")


            ''$$$$$$$$$$$$$ -- Component (INSURANCE) ---START---$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            xmlwriter.WriteStartElement("component")
            xmlwriter.WriteComment("Payers")

            xmlwriter.WriteStartElement("section")

            '------------templateId
            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.101")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
            xmlwriter.WriteEndElement() 'templateId element END

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.1.5.3.7")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
            xmlwriter.WriteEndElement() 'templateId element END

            xmlwriter.WriteStartElement("templateId")
            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.9")
            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Payers Section Template")
            xmlwriter.WriteEndElement() 'templateId element END
            '----------code 

            xmlwriter.WriteComment("Payers section template")

            xmlwriter.WriteStartElement("code")
            xmlwriter.WriteAttributeString("code", "48768-6")
            xmlwriter.WriteAttributeString("displayName", "Payment sources")
            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
            xmlwriter.WriteEndElement() 'code element END

            '-----------title
            xmlwriter.WriteElementString("title", "Insurance")

            xmlwriter.WriteStartElement("text")
            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

            xmlwriter.WriteStartElement("table")
            xmlwriter.WriteAttributeString("border", "1")
            xmlwriter.WriteAttributeString("width", "100%")
            xmlwriter.WriteStartElement("thead")
            xmlwriter.WriteStartElement("tr")
            xmlwriter.WriteElementString("th", "Insurance Plan Name")
            xmlwriter.WriteElementString("th", "Insurance ID")
            xmlwriter.WriteElementString("th", "Group #")
            xmlwriter.WriteElementString("th", "Subscriber Name")
            xmlwriter.WriteElementString("th", "Relation")
            xmlwriter.WriteElementString("th", "Start and End Dates")
            xmlwriter.WriteElementString("th", "Insurance Type")
            xmlwriter.WriteEndElement() '''''''tr End
            xmlwriter.WriteEndElement() ''''''''thead End
            xmlwriter.WriteStartElement("tbody")

            If Not IsNothing(mPatient.PatientInsurances) AndAlso mPatient.PatientInsurances.Count > 0 Then
                For Each oInsurances As Insurance In mPatient.PatientInsurances
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteElementString("td", oInsurances.InsuranceName)
                    xmlwriter.WriteElementString("td", oInsurances.InsuranceId)
                    xmlwriter.WriteElementString("td", oInsurances.GroupNo)
                    xmlwriter.WriteElementString("td", oInsurances.InsSubscriberName)
                    xmlwriter.WriteElementString("td", oInsurances.InsRelation)
                    xmlwriter.WriteElementString("td", oInsurances.InsStartdate & " - " & oInsurances.InsEndDate)
                    xmlwriter.WriteElementString("td", oInsurances.InsuranceType)
                    xmlwriter.WriteEndElement() 'tr element end
                Next
            Else
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteStartElement("td")
                xmlwriter.WriteAttributeString("colspan", "7")
                xmlwriter.WriteEndElement() 'End of td
                xmlwriter.WriteEndElement() 'End of tr
            End If


            xmlwriter.WriteEndElement() 'Tbody element end
            xmlwriter.WriteEndElement() 'Table element end

            xmlwriter.WriteEndElement() 'text element END


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If Not IsNothing(mPatient.PatientInsurances) Then

                For Each oInsurances As Insurance In mPatient.PatientInsurances
                    '-----------entry
                    xmlwriter.WriteStartElement("entry")
                    xmlwriter.WriteAttributeString("typeCode", "DRIV")
                    '-----------act
                    xmlwriter.WriteStartElement("act")
                    xmlwriter.WriteAttributeString("classCode", "ACT")
                    xmlwriter.WriteAttributeString("moodCode", "DEF")
                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.17")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() 'templateId element END
                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.20")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                    xmlwriter.WriteEndElement() 'templateId element END
                    xmlwriter.WriteComment("Coverage entry template")
                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteEndElement() 'id element END
                    '----------code 
                    '' Coverage activity template identifier-Mayuri
                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "48768-6")
                    xmlwriter.WriteAttributeString("displayName", "PAYMENT SOURCES")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                    xmlwriter.WriteEndElement() 'code element END
                    '-----------statusCode
                    xmlwriter.WriteStartElement("statusCode")
                    xmlwriter.WriteAttributeString("code", "completed")
                    xmlwriter.WriteEndElement() 'statusCode element END

                    '-----------entryRelationship
                    xmlwriter.WriteStartElement("entryRelationship")
                    xmlwriter.WriteAttributeString("typeCode", "COMP")

                    xmlwriter.WriteStartElement("act")
                    xmlwriter.WriteAttributeString("classCode", "ACT")
                    xmlwriter.WriteAttributeString("moodCode", "EVN")


                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.26")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                    xmlwriter.WriteEndElement() 'templateId element END
                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.18")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() 'templateId element END
                    ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.5")
                    xmlwriter.WriteEndElement() 'templateId element END

                    xmlwriter.WriteComment("Insurance provider template")


                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteAttributeString("extension", "Group or Contract Number")
                    xmlwriter.WriteEndElement() 'id element END
                    '-----------code
                    xmlwriter.WriteStartElement("code")
                    If Not IsNothing(oInsurances.InsTypeCode) AndAlso oInsurances.InsTypeCode <> "" Then
                        xmlwriter.WriteAttributeString("code", oInsurances.InsTypeCode)
                    Else
                        xmlwriter.WriteAttributeString("code", "OT")
                    End If
                    If Not IsNothing(oInsurances.InsuranceType) AndAlso oInsurances.InsuranceType <> "" Then
                        xmlwriter.WriteAttributeString("displayName", oInsurances.InsuranceType)
                    Else
                        xmlwriter.WriteAttributeString("displayName", "Other")
                    End If
                    xmlwriter.WriteAttributeString("codeSystemName", "X12 Insurance Type code")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.255.1336")
                    '------------translation
                    xmlwriter.WriteStartElement("translation")
                    xmlwriter.WriteAttributeString("code", "EHCPOL")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
                    xmlwriter.WriteAttributeString("displayName", "Extended healthcare")
                    xmlwriter.WriteEndElement() '''''''translation End
                    xmlwriter.WriteEndElement() '''''''code End
                    '-----------statusCode
                    xmlwriter.WriteStartElement("statusCode")
                    xmlwriter.WriteAttributeString("code", "completed")
                    xmlwriter.WriteEndElement() '''''''statusCode End
                    '-----------performer
                    xmlwriter.WriteStartElement("performer")
                    xmlwriter.WriteAttributeString("typeCode", "PRF")
                    '-----------time
                    xmlwriter.WriteStartElement("time")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                    xmlwriter.WriteEndElement() '''''''time End

                    '-----------assignedEntity
                    xmlwriter.WriteStartElement("assignedEntity")

                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteEndElement() 'id element END

                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "PAYOR")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.110")
                    xmlwriter.WriteAttributeString("codeSystemName", "HL7 RoleClassRelationship")
                    xmlwriter.WriteEndElement() '''''''code End
                    '--------------addr
                    xmlwriter.WriteStartElement("addr")
                    If Not IsNothing(oInsurances.InsSubsAddressLine1) Then
                        xmlwriter.WriteElementString("streetAddressLine", oInsurances.InsSubsAddressLine1)
                    End If
                    If Not IsNothing(oInsurances.InsSubsAddressLine2) Then
                        xmlwriter.WriteElementString("streetAddressLine", oInsurances.InsSubsAddressLine2)
                    End If
                    If Not IsNothing(oInsurances.InsSubsCity) Then
                        xmlwriter.WriteElementString("city", oInsurances.InsSubsCity)
                    End If
                    If Not IsNothing(oInsurances.InsSubsState) Then
                        xmlwriter.WriteElementString("state", oInsurances.InsSubsState)
                    End If
                    If Not IsNothing(oInsurances.InsSubsZip) Then
                        xmlwriter.WriteElementString("postalCode", oInsurances.InsSubsZip)
                    End If
                    xmlwriter.WriteEndElement() '''''''addr End

                    xmlwriter.WriteStartElement("telecom")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                    xmlwriter.WriteEndElement() '''''''telecom End

                    xmlwriter.WriteStartElement("representedOrganization")
                    xmlwriter.WriteElementString("name", oInsurances.InsuranceName)
                    xmlwriter.WriteElementString("telecom", "")
                    xmlwriter.WriteElementString("addr", "")
                    xmlwriter.WriteEndElement() ''''''' representedOrganization End

                    xmlwriter.WriteStartElement("sdtc:patient")
                    xmlwriter.WriteStartElement("sdtc:id")
                    xmlwriter.WriteAttributeString("root", "PatientIdAsKnownToPayor")
                    xmlwriter.WriteEndElement() 'sdtc:id
                    xmlwriter.WriteEndElement() 'sdtc:patient

                    xmlwriter.WriteEndElement() '''''''assignedEntity End
                    xmlwriter.WriteEndElement() '''''''performer End

                    '----------participant
                    xmlwriter.WriteStartElement("participant")
                    xmlwriter.WriteAttributeString("typeCode", "HLD")
                    '--------participantRole              
                    xmlwriter.WriteStartElement("participantRole")
                    '-------------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("extension", "SubscriberIDAsKnownToPayor")
                    xmlwriter.WriteAttributeString("root", "PayorAuthorityID")
                    xmlwriter.WriteEndElement() ''''''' id End
                    '-----------------addr
                    xmlwriter.WriteStartElement("addr")
                    xmlwriter.WriteElementString("streetAddressLine", "")
                    xmlwriter.WriteElementString("city", "")
                    xmlwriter.WriteElementString("state", "")
                    xmlwriter.WriteEndElement() '-- addr end

                    xmlwriter.WriteElementString("telecom", "")

                    '-----------------playingEntity
                    xmlwriter.WriteStartElement("playingEntity")
                    'xmlwriter.WriteStartElementString("name", "Patients Mom")
                    If Not IsNothing(oInsurances.InsRelation) Then
                        xmlwriter.WriteElementString("name", oInsurances.InsRelation)
                    End If

                    'xmlwriter.WriteEndElement() '-------name end

                    xmlwriter.WriteStartElement("sdtc:birthTime")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")

                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteEndElement() '-- playingEntity

                    xmlwriter.WriteEndElement() '---participantRole end
                    xmlwriter.WriteEndElement() '---participant end 

                    xmlwriter.WriteStartElement("participant")
                    xmlwriter.WriteAttributeString("typeCode", "COV")

                    '---------------time
                    xmlwriter.WriteStartElement("time")
                    xmlwriter.WriteStartElement("low")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                    xmlwriter.WriteEndElement() '-- low

                    xmlwriter.WriteStartElement("high")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                    xmlwriter.WriteEndElement() '-- high
                    xmlwriter.WriteEndElement() '-- time

                    xmlwriter.WriteStartElement("participantRole")
                    xmlwriter.WriteAttributeString("classCode", "PAT")
                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteAttributeString("extension", "1138345")
                    xmlwriter.WriteEndElement() 'id element END

                    '''''''''''''''''''
                    'Coverage Role Type :http://web.his.uvic.ca/Research/HTG/vocabulary/Terminologies/HL7/Browse.php?Tab=ValueSets&CodeSystem=2.16.840.1.113883.5.111
                    '''''''''''''''''''
                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "SELF")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.111")
                    xmlwriter.WriteAttributeString("displayName", "displayName")
                    xmlwriter.WriteEndElement() '''''''code End

                    '-----------------addr
                    xmlwriter.WriteElementString("addr", "")
                    xmlwriter.WriteElementString("telecom", "")

                    '-----------------playingEntity
                    xmlwriter.WriteStartElement("playingEntity")
                    xmlwriter.WriteElementString("name", "self")

                    xmlwriter.WriteElementString("sdtc:birthTime", "")
                    ''Added on 20111215-Mayuri
                    'xmlwriter.WriteElementString("sdtc:birthTime", mPatient.PatientDemographics.DemographicsDetail.PatientDOB)
                    xmlwriter.WriteEndElement() '-------playingEntity end
                    xmlwriter.WriteEndElement() '-------participantRole end
                    xmlwriter.WriteEndElement() '-------participant end


                    xmlwriter.WriteStartElement("entryRelationship")
                    xmlwriter.WriteAttributeString("typeCode", "REFR")
                    xmlwriter.WriteStartElement("act")
                    xmlwriter.WriteAttributeString("classCode", "ACT")
                    xmlwriter.WriteAttributeString("moodCode", "EVN")

                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.19")
                    xmlwriter.WriteEndElement() 'templateId element END
                    xmlwriter.WriteComment("Authorization activity template")

                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteEndElement() 'id element END

                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("nullFlavor", "NA")
                    xmlwriter.WriteEndElement() ''''''' code End
                    '-------entryRelationship
                    xmlwriter.WriteStartElement("entryRelationship")
                    xmlwriter.WriteAttributeString("typeCode", "SUBJ")
                    '------------procedure
                    xmlwriter.WriteStartElement("procedure")
                    xmlwriter.WriteAttributeString("classCode", "PROC")
                    xmlwriter.WriteAttributeString("moodCode", "PRMS")
                    '--------------code
                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "73761001")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                    xmlwriter.WriteAttributeString("displayName", "Colonoscopy")
                    xmlwriter.WriteEndElement() '--------------code end

                    xmlwriter.WriteEndElement() '--------------procedure

                    xmlwriter.WriteEndElement() '--------------entryRelationship

                    xmlwriter.WriteEndElement() '--------------act end
                    xmlwriter.WriteEndElement() '--------------entryRelationship end

                    '-----------------entryRelationship
                    xmlwriter.WriteStartElement("entryRelationship")
                    xmlwriter.WriteAttributeString("typeCode", "REFR")

                    '-------act
                    xmlwriter.WriteStartElement("act")
                    xmlwriter.WriteAttributeString("classCode", "ACT")
                    xmlwriter.WriteAttributeString("moodCode", "DEF")

                    '-----------id
                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteEndElement() 'id element END

                    '--------------code
                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                    xmlwriter.WriteEndElement() '--------------code end

                    '--------------text
                    xmlwriter.WriteStartElement("text")
                    xmlwriter.WriteStartElement("reference")
                    xmlwriter.WriteAttributeString("value", "PntrToHealthPlanNameInSectionText")
                    xmlwriter.WriteEndElement()  '--------reference
                    xmlwriter.WriteEndElement()  '--------text

                    '--------------statusCode 
                    xmlwriter.WriteStartElement("statusCode")
                    xmlwriter.WriteAttributeString("code", "active")
                    xmlwriter.WriteEndElement()  '--------statusCode

                    xmlwriter.WriteEndElement() '--------------act end
                    xmlwriter.WriteEndElement() '--------------entryRelationship end
                    xmlwriter.WriteEndElement() '--------------act end
                    xmlwriter.WriteEndElement() '--------------entryRelationship end

                    xmlwriter.WriteEndElement() '--------------act end
                    xmlwriter.WriteEndElement() '--------------entry

                Next

            End If
            xmlwriter.WriteEndElement() ''''Section END 
            xmlwriter.WriteEndElement() ''''component END 


            ''$$$$$$$$$$$$$   -- Component (INSURANCE) ---END---$$$$$$$$$$$$$$$$$$


            ''$$$$$$$$$$$$$   -- Component (PROBLEMS) ---START---$$$$$$$$$$$$$$$$$$

            If CCDSection.Contains("Problems") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Problems")
                xmlwriter.WriteStartElement("section")

                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.11")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteEndElement() 'templateId element END

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.6")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteEndElement()

                xmlwriter.WriteComment("Problems section template")

                '----------code 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "11450-4")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("displayName", "Problem list")
                xmlwriter.WriteEndElement() 'code element END

                '-----------title
                xmlwriter.WriteElementString("title", "Conditions or Problems")
                '-----------text

                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Condition")
                xmlwriter.WriteElementString("th", "Effective Dates")
                xmlwriter.WriteElementString("th", "Condition Status")
                xmlwriter.WriteElementString("th", "Problem Type")
                xmlwriter.WriteElementString("th", "SNOMED Code")
                xmlwriter.WriteElementString("th", "ICD9/10 Code")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")

                'Check Whether Problems data exists or not if not exists write blank tbody section
                If Not IsNothing(mPatient.PatientProblems) AndAlso mPatient.PatientProblems.Count > 0 Then
                    For Each oProblems As Problems In mPatient.PatientProblems
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oProblems.Condition)
                        xmlwriter.WriteElementString("td", oProblems.DateOfService)
                        xmlwriter.WriteElementString("td", oProblems.ConditionStatus)
                        xmlwriter.WriteElementString("td", oProblems.ProblemType)
                        xmlwriter.WriteElementString("td", oProblems.ConceptID)
                        xmlwriter.WriteElementString("td", oProblems.ICD9Code)
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "6")
                    xmlwriter.WriteEndElement() 'End of td
                    xmlwriter.WriteEndElement() 'End of tr
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If Not IsNothing(mPatient.PatientProblems) Then

                    For Each oProblems As Problems In mPatient.PatientProblems

                        '----------------entry
                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")
                        '---------------act
                        xmlwriter.WriteStartElement("act")
                        xmlwriter.WriteAttributeString("classCode", "ACT")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.7")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C32")
                        xmlwriter.WriteEndElement() 'templateId element END

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.7")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteEndElement()

                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.27")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5.1")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5.2")
                        xmlwriter.WriteEndElement() 'templateId element END

                        xmlwriter.WriteComment("Problem act template")

                        ''--------------id
                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END

                        '-------------code
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("nullFlavor", "NA")
                        xmlwriter.WriteEndElement() 'id element END

                        '-----------statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        ''According to Diagnosis One For Acrive problems status should be active instead completed
                        xmlwriter.WriteAttributeString("code", "active")
                        xmlwriter.WriteEndElement() 'statusCode element END

                        '-----------effectiveTime
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")
                        xmlwriter.WriteAttributeString("inversionInd", "false")
                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteStartElement("sequenceNumber")

                        xmlwriter.WriteEndElement() '
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '----------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.28")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '----------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() 'templateId element END

                        xmlwriter.WriteComment("Problem observation template")
                        '------------id
                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END

                        '-----------code
                        xmlwriter.WriteStartElement("code")

                        ''identifies whether it is problem/clinical finding etc-Mayuri
                        xmlwriter.WriteAttributeString("code", "55607006")

                        xmlwriter.WriteAttributeString("displayName", "Problem")

                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED-CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteEndElement() 'code element END

                        If Not IsNothing(oProblems.Condition) AndAlso oProblems.Condition <> "" Then
                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PointrToSectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END
                        End If

                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'statusCode element END

                        '-----------effectiveTime
                        xmlwriter.WriteStartElement("effectiveTime")

                        If Not IsNothing(oProblems.DateOfService) AndAlso oProblems.DateOfService <> "" Then
                            strDate = Format(CType(oProblems.DateOfService, Date), "yyyyMMdd")
                            xmlwriter.WriteStartElement("low")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'low element END

                            xmlwriter.WriteStartElement("high")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'high element END
                        Else
                            xmlwriter.WriteStartElement("low")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'low element END

                            xmlwriter.WriteStartElement("high")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'high element END

                        End If
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '-----------value
                        ''Added for Diagnosis One-for pregnancy case only needed conceptid-Mayuri
                        If Not IsNothing(oProblems.ConceptID) AndAlso oProblems.ConceptID <> "" AndAlso oProblems.ConceptID = "77386006" Then
                            xmlwriter.WriteStartElement("value")
                            xmlwriter.WriteAttributeString("xsi:type", "CD")
                            If Not IsNothing(oProblems.Condition) AndAlso oProblems.Condition <> "" Then
                                xmlwriter.WriteAttributeString("displayName", oProblems.Condition)

                            End If
                            If Not IsNothing(oProblems.ConceptID) AndAlso oProblems.ConceptID <> "" Then
                                xmlwriter.WriteAttributeString("code", oProblems.ConceptID)

                            End If
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteEndElement() 'value element END
                        Else

                            xmlwriter.WriteStartElement("value")
                            xmlwriter.WriteAttributeString("xsi:type", "CD")

                            If Not IsNothing(oProblems.ConceptID) AndAlso oProblems.ConceptID <> "" Then
                                xmlwriter.WriteAttributeString("code", oProblems.ConceptID)
                                If Not IsNothing(oProblems.Condition) AndAlso oProblems.Condition <> "" Then
                                    xmlwriter.WriteAttributeString("displayName", oProblems.Condition)

                                End If
                                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            End If


                            xmlwriter.WriteStartElement("translation")
                            If Not IsNothing(oProblems.ICD9Code) AndAlso oProblems.ICD9Code <> "" Then
                                xmlwriter.WriteAttributeString("code", oProblems.ICD9Code)

                            End If
                            If Not IsNothing(oProblems.ICD9) AndAlso oProblems.ICD9 <> "" Then
                                xmlwriter.WriteAttributeString("displayName", oProblems.ICD9)

                            End If

                            If Not IsNothing(oProblems.ICDRevision) AndAlso oProblems.ICDRevision <> 0 Then
                                If oProblems.ICDRevision = 9 Then
                                    xmlwriter.WriteAttributeString("codeSystemName", "ICD-9-CM")
                                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.103")
                                ElseIf oProblems.ICDRevision = 10 Then
                                    xmlwriter.WriteAttributeString("codeSystemName", "ICD10CM")
                                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.90")
                                End If

                            Else
                                xmlwriter.WriteAttributeString("codeSystemName", "ICD-9-CM")
                                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.103")
                            End If

                            xmlwriter.WriteEndElement() ''''''''translation END


                            xmlwriter.WriteEndElement() 'value element END
                        End If
                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        xmlwriter.WriteEndElement() 'act element END

                        xmlwriter.WriteEndElement() 'entry END 

                    Next

                End If
                xmlwriter.WriteEndElement() ''''Section END 
                xmlwriter.WriteEndElement() ''''component END 
            End If

            ''$$$$$$$$$$$$$  -- Component (PROBLEMS) ---END---$$$$$$$$$$$$$$$$$$


            ''$$$$$$$$$$$$$  -- Component (Allergies and Alerts) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Allergy") = True Or CCDSection = "All" Then

                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Allergies and Alerts")

                xmlwriter.WriteStartElement("section")

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.102")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteEndElement() 'templateId element END

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.13")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                xmlwriter.WriteEndElement() 'templateId element END

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.2")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HL7 CCD")
                xmlwriter.WriteEndElement() 'templateId element END
                '----------code 

                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "48765-2")
                xmlwriter.WriteAttributeString("displayName", "Alerts")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Allergies and Alert Problems")
                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Type")
                xmlwriter.WriteElementString("th", "SNOMED Code")
                xmlwriter.WriteElementString("th", "Substance")
                xmlwriter.WriteElementString("th", "Reaction")
                xmlwriter.WriteElementString("th", "Status")
                xmlwriter.WriteElementString("th", "Date Recorded")
                xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement()
                xmlwriter.WriteStartElement("tbody")
                'Code to check whether data is present or not -Code by Shirish
                If Not IsNothing(mPatient.PatientAllergies) AndAlso mPatient.PatientAllergies.Count > 0 Then
                    For Each oAllergies As Allergies In mPatient.PatientAllergies
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", "Drug Allergy")
                        xmlwriter.WriteElementString("td", oAllergies.ConceptID)
                        xmlwriter.WriteElementString("td", oAllergies.ProductName)
                        xmlwriter.WriteElementString("td", oAllergies.Reaction)
                        xmlwriter.WriteElementString("td", oAllergies.Status)
                        xmlwriter.WriteElementString("td", oAllergies.EffectiveStartTime)
                        'xmlwriter.WriteEndElement() 'td element end
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "6")
                    xmlwriter.WriteEndElement() 'End of td.
                    xmlwriter.WriteEndElement() 'tr element end
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END

                'Dim i As Integer = 0
                If Not IsNothing(mPatient.PatientAllergies) Then

                    For Each oAllergies As Allergies In mPatient.PatientAllergies
                        'i = i + 1
                        '-----------entry
                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")
                        '-----------act
                        xmlwriter.WriteStartElement("act")
                        xmlwriter.WriteAttributeString("classCode", "ACT")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")
                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.6")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteEndElement() 'templateId element END
                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.27")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5.1")
                        xmlwriter.WriteEndElement() 'templateId element END
                        '-----------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5.3")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '-----------id
                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END
                        '-----------code
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("nullFlavor", "NA")
                        xmlwriter.WriteEndElement() 'code element END

                        '-----------statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'statusCode element END

                        'Mahesh
                        strDate = Format(Date.Now, "yyyyMMdd")
                        '-----------effectiveTime
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteStartElement("high")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")
                        xmlwriter.WriteAttributeString("inversionInd", "false")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.18")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END

                        '-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.28")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END

                        ''-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.5")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END

                        ''-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.6")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END

                        'xmlwriter.WriteElementString("id", "")


                        '-----------id  Mahesh
                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END

                        '<code code="416098002" codeSystem="2.16.840.1.113883.6.96" displayName="drug allergy" codeSystemName="SNOMED CT"/>
                        '-----------observation - code
                        xmlwriter.WriteStartElement("code")
                        'retrieve the code from the "Adverse_Event_Types" table
                        oAllergies.AllergyType = "Allergy to Substance" 'for time being madam asked to give har coded value to the porperty procedure
                        ''Allergy/Drug Sensitivity, the vocabulary used for adverse event types SHALL be coded as specified in HITSP/C80 Section 2.2.3.4.2 Allergy/Adverse Event Type Adverse Event Entry data element. See HITSP/C83 Section 
                        xmlwriter.WriteAttributeString("code", "416098002")
                        xmlwriter.WriteAttributeString("displayName", "drug allergy")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")

                        xmlwriter.WriteStartElement("translation")
                        xmlwriter.WriteAttributeString("code", "282291009")
                        xmlwriter.WriteAttributeString("displayName", "Diagnosis")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteEndElement() 'translation - code element END
                        xmlwriter.WriteEndElement() 'code - code element END

                        'xmlwriter.WriteElementString("text", "Allergy to Substance")

                        '-----------observation - text
                        xmlwriter.WriteStartElement("text")
                        xmlwriter.WriteStartElement("reference")
                        xmlwriter.WriteAttributeString("value", "PtrToValueInSectionText")
                        xmlwriter.WriteEndElement() 'reference element END
                        xmlwriter.WriteEndElement() 'text element END

                        '-----------statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'statusCode element END

                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '<value xsi:type="CD" /> 
                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("xsi:type", "CD")
                        xmlwriter.WriteEndElement()

                        '-----------observation - participant
                        xmlwriter.WriteStartElement("participant")
                        xmlwriter.WriteAttributeString("typeCode", "CSM")
                        '-----------participant - participantRole
                        xmlwriter.WriteStartElement("participantRole")
                        xmlwriter.WriteAttributeString("classCode", "MANU")

                        xmlwriter.WriteElementString("addr", "")
                        xmlwriter.WriteElementString("telecom", "")

                        '-----------participantRole - playingEntity
                        xmlwriter.WriteStartElement("playingEntity")
                        xmlwriter.WriteAttributeString("classCode", "MMAT")
                        '-----------playingEntity - code

                        'when we retrieve te allergies using the spGetLatestAllergies against that patient then,
                        'we also retrieve the Drug ID against that patient. if the drug id = 0, then pass a harcoded value for RxNorm Code
                        ' else if there is DrugId then get the top first sNDCCode from Drugs_Mst aginst that drugId and pass to the getRxNormCode() and put the RxNorm Code.
                        ' if the RxNormCode against that NDC is empty then pass hardcoded value else pass the RxNormCode value that was retrieved.



                        xmlwriter.WriteStartElement("code")
                        If oAllergies.ProductCode <> "" AndAlso oAllergies.ProductCode <> Nothing Then
                            xmlwriter.WriteAttributeString("code", oAllergies.ProductCode)
                            'Else
                            '    xmlwriter.WriteAttributeString("code", """")
                        End If

                        If oAllergies.ProductName <> "" AndAlso oAllergies.ProductName <> Nothing Then
                            xmlwriter.WriteAttributeString("displayName", oAllergies.ProductName)
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.88")
                        ''Mahesh''xmlwriter.WriteAttributeString("codeSystemName", "RxNorm")
                        'xmlwriter.WriteElementString("originalText", oAllergies.ProductName)

                        'xmlwriter.WriteElementString("text", oAllergies.ProductCode)

                        xmlwriter.WriteStartElement("originalText")
                        xmlwriter.WriteStartElement("reference")
                        xmlwriter.WriteAttributeString("value", "PointrToSectionText")
                        xmlwriter.WriteEndElement() 'reference element END
                        xmlwriter.WriteEndElement() 'originalText - playingEntity element END
                        xmlwriter.WriteEndElement() 'code element END

                        '-----------playingEntity - name
                        If oAllergies.ProductName <> "" AndAlso oAllergies.ProductName <> Nothing Then
                            xmlwriter.WriteElementString("name", oAllergies.ProductName)
                        End If
                        xmlwriter.WriteEndElement() 'participantRole - playingEntity element END

                        xmlwriter.WriteEndElement() 'participant - participantRole element END
                        xmlwriter.WriteEndElement() 'observation - participant element END


                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "MFST")
                        xmlwriter.WriteAttributeString("inversionInd", "true")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.54")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END
                        '-----------observation - code
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "SEV")
                        xmlwriter.WriteAttributeString("displayName", "Status")
                        xmlwriter.WriteAttributeString("codeSystemName", "AlertStatusCode")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
                        xmlwriter.WriteEndElement() 'observation - code element END

                        '-----------observation - Reaction
                        'Dim _reactionCode = ogloCCDDBLayer.GetCodeForCategory(oAllergies.Reaction, "Reaction")

                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteStartElement("text")
                        xmlwriter.WriteStartElement("reference")
                        If oAllergies.Reaction <> "" AndAlso oAllergies.Reaction <> Nothing Then
                            xmlwriter.WriteAttributeString("value", oAllergies.Reaction)
                        End If
                        xmlwriter.WriteEndElement() ''end reference
                        xmlwriter.WriteEndElement() ''End text element
                        ''End code Added by Mayuri:20111215-To remove warnings in NIST Validator
                        '-----------observation - statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'observation - statusCode element END


                        xmlwriter.WriteStartElement("value")
                        If oAllergies.ReactionCode <> "" AndAlso oAllergies.ReactionCode <> Nothing Then
                            xmlwriter.WriteAttributeString("code", oAllergies.ReactionCode)

                            If oAllergies.Reaction <> "" AndAlso oAllergies.Reaction <> Nothing Then
                                xmlwriter.WriteAttributeString("displayName", oAllergies.Reaction)
                            End If

                            'xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            'xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        Else
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        End If

                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("xsi:type", "CD")
                        xmlwriter.WriteEndElement() 'value element END
                        xmlwriter.WriteEndElement() 'observation element END
                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "REFR")
                        'xmlwriter.WriteAttributeString("inversionInd", "false")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------observation - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.39")
                        xmlwriter.WriteEndElement() 'observation - templateId   element END
                        '-----------observation - code
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "33999-4")
                        xmlwriter.WriteAttributeString("displayName", "Status")
                        xmlwriter.WriteAttributeString("codeSystemName", "LIONC")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                        xmlwriter.WriteEndElement() 'observation - code element END
                        '-----------observation - statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'observation - statusCode element END

                        Dim _StatusCode As String = ""
                        If Not IsNothing(oAllergies.Status) Then
                            Select Case oAllergies.Status.ToUpper
                                Case "ACTIVE"
                                    _StatusCode = "55561003"
                                Case "INACTIVE"
                                    _StatusCode = "73425007"
                                Case Else
                                    _StatusCode = "55561003"
                            End Select
                        Else
                            oAllergies.Status = "Active"
                            _StatusCode = "55561003"
                        End If

                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("code", _StatusCode)
                        xmlwriter.WriteAttributeString("displayName", oAllergies.Status)
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteAttributeString("xsi:type", "CE")
                        xmlwriter.WriteEndElement() 'value element END

                        xmlwriter.WriteEndElement() 'observation element END
                        xmlwriter.WriteEndElement() 'entryRelationship element END
                        ''''''''''''''

                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END

                        xmlwriter.WriteEndElement() 'act element END
                        xmlwriter.WriteEndElement() 'entry element END
                    Next

                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END
            End If

            '*****************************Code commented by supriya *****************************************************


            '$$$$$$$$$$$$$  -- Component (VITALS) ---START---$$$$$$$$$$$$$$$$$$

            If CCDSection.Contains("Vitals") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                If Not IsNothing(dtvitals) AndAlso dtvitals.Rows.Count > 0 Then
                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.16")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HL7 CCD")
                    xmlwriter.WriteEndElement() 'templateId element END


                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.119")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                    xmlwriter.WriteEndElement() 'templateId element END


                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.1.5.3.2")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() 'templateId element END


                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.25")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() 'templateId element END


                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "8716-3")
                    xmlwriter.WriteAttributeString("displayName", "Vital signs")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                    xmlwriter.WriteEndElement() 'code element END
                End If

                xmlwriter.WriteElementString("title", "Vital signs")

                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")

                If Not IsNothing(dtvitals) Then
                    If dtvitals.Rows.Count > 0 Then

                        Dim rowcnt As Integer = 0
                        xmlwriter.WriteStartElement("tbody")

                        For i As Integer = 0 To dtvitals.Columns.Count - 1

                            xmlwriter.WriteStartElement("tr")
                            If i = 0 Then
                                xmlwriter.WriteElementString("th", "Vital Name")
                                For j As Integer = 0 To dtvitals.Rows.Count - 1
                                    'If j < 5 Then
                                    xmlwriter.WriteElementString("th", dtvitals.Rows(j)(i))
                                    'End If
                                Next
                            Else
                                Select Case i
                                    Case "1"
                                        xmlwriter.WriteElementString("td", "Systolic BP-Sitting (mm[Hg])")
                                    Case "2"
                                        xmlwriter.WriteElementString("td", "Diastolic BP-Sitting (mm[Hg])")
                                    Case "3"
                                        xmlwriter.WriteElementString("td", "Pulse Rate (/min)")
                                    Case "4"
                                        xmlwriter.WriteElementString("td", "Respiration Rate (/min)")
                                    Case "5"
                                        xmlwriter.WriteElementString("td", "Temp-Oral ([degF])")
                                    Case "6"
                                        xmlwriter.WriteElementString("td", "Height/Length ([in_us])")
                                    Case "7"
                                        xmlwriter.WriteElementString("td", "Height/Length ([cm])")
                                    Case "8"
                                        xmlwriter.WriteElementString("td", "Weight ([lbs_av])")
                                    Case "9"
                                        xmlwriter.WriteElementString("td", "Weight ([kg])")
                                    Case "10"
                                        xmlwriter.WriteElementString("td", "Body Mass Index (kg/m2)")
                                    Case "11"
                                        xmlwriter.WriteElementString("td", "Body Surface Area (m2)")
                                    Case "12"
                                        xmlwriter.WriteElementString("td", "Pulse Ox (%)")
                                End Select

                                For j As Integer = 0 To dtvitals.Rows.Count - 1
                                    'If rowcnt = 5 Then
                                    '    rowcnt = 0
                                    '    'xmlwriter.WriteElementString("td", "")
                                    '    'xmlwriter.WriteEndElement() 'Tbody element end
                                    '    'xmlwriter.WriteStartElement("tbody")
                                    '    If i = 1 Then
                                    '        xmlwriter.WriteEndElement() 'tr element end
                                    '        xmlwriter.WriteStartElement("tr")
                                    '        xmlwriter.WriteElementString("th", "Vital Name")
                                    '        For k As Integer = j To dtvitals.Rows.Count - 1
                                    '            If k < j + 5 Then
                                    '                xmlwriter.WriteElementString("th", dtvitals.Rows(k)(0))
                                    '            End If
                                    '        Next
                                    '        xmlwriter.WriteEndElement() 'tr element end
                                    '    End If
                                    '    i = 1
                                    '    Exit For
                                    'End If
                                    If dtvitals.Rows(j)(i) > 0 Then
                                        xmlwriter.WriteElementString("td", dtvitals.Rows(j)(i))
                                    Else
                                        xmlwriter.WriteElementString("td", "-")
                                    End If

                                    'rowcnt = rowcnt + 1
                                Next
                            End If

                            xmlwriter.WriteEndElement() 'tr element end

                        Next
                        xmlwriter.WriteEndElement() 'Tbody element end
                    Else
                        'table has zero rows- write only coulms
                        xmlwriter.WriteStartElement("thead")
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("th", "Vital Name")
                        xmlwriter.WriteElementString("th", "Systolic BP-Sitting (mm[Hg])")
                        xmlwriter.WriteElementString("th", "Diastolic BP-Sitting (mm[Hg])")
                        xmlwriter.WriteElementString("th", "Pulse Rate (/min)")
                        xmlwriter.WriteElementString("th", "Respiration Rate (/min)")
                        xmlwriter.WriteElementString("th", "Temp-Oral ([degF])")
                        xmlwriter.WriteElementString("th", "Height/Length ([in_us])")
                        xmlwriter.WriteElementString("th", "Height/Length ([cm])")
                        xmlwriter.WriteElementString("th", "Weight ([lbs])")
                        xmlwriter.WriteElementString("th", "Weight ([kg])")
                        xmlwriter.WriteElementString("th", "Body Mass Index (kg/m2)")
                        xmlwriter.WriteElementString("th", "Body Surface Area (m2)")
                        xmlwriter.WriteElementString("th", "Pulse Ox (%)")
                        xmlwriter.WriteEndElement() 'End of tr
                        xmlwriter.WriteEndElement() 'End of thead
                        xmlwriter.WriteStartElement("tbody")
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteAttributeString("colspan", " 10")
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement()
                    End If
                Else
                    'If table is nothing
                    xmlwriter.WriteStartElement("tbody")
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteElementString("td", "")
                    xmlwriter.WriteEndElement() 'End of tr
                    xmlwriter.WriteEndElement() 'End of tbody
                End If

                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END


                If Not IsNothing(dtvitals) Then

                    For i As Integer = 0 To dtvitals.Rows.Count - 1

                        '-----------entry
                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")

                        xmlwriter.WriteStartElement("organizer")
                        xmlwriter.WriteAttributeString("classCode", "CLUSTER")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.32")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'templateId element END


                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.35")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() 'templateId element END


                        '------------templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.1")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() 'templateId element END


                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'id element END

                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "46680005")
                        xmlwriter.WriteAttributeString("displayName", "Vital signs")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteEndElement() 'code element END


                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'id element END

                        strDate = Format(CType(dtvitals.Rows(i)(0), Date), "yyyyMMdd")
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        If dtvitals.Rows(i)("dHeightinInch") > 0 Then
                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("observation")
                            xmlwriter.WriteAttributeString("classCode", "OBS")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.14")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.2")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            xmlwriter.WriteAttributeString("code", "8302-2")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                            xmlwriter.WriteAttributeString("displayName", "Patient Body Height")

                            xmlwriter.WriteStartElement("translation")
                            xmlwriter.WriteAttributeString("code", "50373000")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            xmlwriter.WriteAttributeString("displayName", "Body Height/Length")
                            xmlwriter.WriteEndElement()

                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            If Not IsNothing(dtvitals.Rows(i)("dHeightinInch")) AndAlso dtvitals.Rows(i)("dHeightinInch") <> 0 Then
                                xmlwriter.WriteStartElement("value")
                                xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dHeightinInch"))
                                xmlwriter.WriteAttributeString("unit", "[in_us]")
                                xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                xmlwriter.WriteEndElement() 'value element END
                            End If


                            xmlwriter.WriteStartElement("interpretationCode")
                            xmlwriter.WriteAttributeString("displayName", "Normal")
                            xmlwriter.WriteEndElement() 'interpretationCode END

                            xmlwriter.WriteStartElement("referenceRange")
                            xmlwriter.WriteStartElement("observationRange")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'observationRange END
                            xmlwriter.WriteEndElement() 'referenceRanget END

                            xmlwriter.WriteEndElement() 'observation element END
                            xmlwriter.WriteEndElement() 'component element END
                        End If

                        If dtvitals.Rows(i)("dWeightinlbs") > 0 Then
                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("observation")
                            xmlwriter.WriteAttributeString("classCode", "OBS")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.14")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.2")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END
                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            xmlwriter.WriteAttributeString("code", "3141-9")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                            xmlwriter.WriteAttributeString("displayName", "Patient Body Weight - Measured")

                            xmlwriter.WriteStartElement("translation")
                            xmlwriter.WriteAttributeString("code", "27113001")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            xmlwriter.WriteAttributeString("displayName", "Body Weight")
                            xmlwriter.WriteEndElement()


                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END


                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            If Not IsNothing(dtvitals.Rows(i)("dWeightinlbs")) AndAlso dtvitals.Rows(i)("dWeightinlbs") <> 0 Then
                                xmlwriter.WriteStartElement("value")
                                xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dWeightinlbs"))
                                xmlwriter.WriteAttributeString("unit", "[lb_av]")
                                xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                xmlwriter.WriteEndElement() 'value element END
                            End If

                            xmlwriter.WriteStartElement("interpretationCode")
                            xmlwriter.WriteAttributeString("displayName", "Normal")
                            xmlwriter.WriteEndElement() 'interpretationCode END

                            xmlwriter.WriteStartElement("referenceRange")
                            xmlwriter.WriteStartElement("observationRange")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'observationRange END
                            xmlwriter.WriteEndElement() 'referenceRanget END

                            xmlwriter.WriteEndElement() 'observation element END
                            xmlwriter.WriteEndElement() 'component element END
                        End If

                        If dtvitals.Rows(i)("dBloodPressureSittingMax") > 0 Then
                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("observation")
                            xmlwriter.WriteAttributeString("classCode", "OBS")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.14")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.2")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            xmlwriter.WriteAttributeString("code", "8480-6")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                            xmlwriter.WriteAttributeString("displayName", "Intravascular Systolic")

                            xmlwriter.WriteStartElement("translation")
                            xmlwriter.WriteAttributeString("code", "271649006")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            xmlwriter.WriteAttributeString("displayName", "Systolic BP")
                            xmlwriter.WriteEndElement()
                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            If Not IsNothing(dtvitals.Rows(i)("dBloodPressureSittingMax")) AndAlso dtvitals.Rows(i)("dBloodPressureSittingMax") <> 0 Then
                                xmlwriter.WriteStartElement("value")
                                xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dBloodPressureSittingMax"))
                                xmlwriter.WriteAttributeString("unit", "mm[Hg]")
                                xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                xmlwriter.WriteEndElement() 'value element END
                            End If


                            xmlwriter.WriteStartElement("interpretationCode")
                            xmlwriter.WriteAttributeString("displayName", "Normal")
                            xmlwriter.WriteEndElement() 'interpretationCode END

                            xmlwriter.WriteStartElement("referenceRange")
                            xmlwriter.WriteStartElement("observationRange")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'observationRange END
                            xmlwriter.WriteEndElement() 'referenceRanget END

                            xmlwriter.WriteEndElement() 'observation element END
                            xmlwriter.WriteEndElement() 'component element END
                        End If

                        If dtvitals.Rows(i)("dBloodPressureSittingMin") > 0 Then
                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("observation")
                            xmlwriter.WriteAttributeString("classCode", "OBS")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.14")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.2")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            'Code Added by kanchan on 20101101
                            'xmlwriter.WriteAttributeString("code", "11377-9")
                            xmlwriter.WriteAttributeString("code", "8462-4")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                            xmlwriter.WriteAttributeString("displayName", "Intravascular Diastolic")

                            xmlwriter.WriteStartElement("translation")
                            xmlwriter.WriteAttributeString("code", "271650006")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            xmlwriter.WriteAttributeString("displayName", "Diastolic BP")
                            xmlwriter.WriteEndElement()
                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            If Not IsNothing(dtvitals.Rows(i)("dBloodPressureSittingMin")) AndAlso dtvitals.Rows(i)("dBloodPressureSittingMin") <> 0 Then
                                xmlwriter.WriteStartElement("value")
                                xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dBloodPressureSittingMin"))
                                xmlwriter.WriteAttributeString("unit", "mm[Hg]")
                                xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                xmlwriter.WriteEndElement() 'value element END
                            End If


                            xmlwriter.WriteStartElement("interpretationCode")
                            xmlwriter.WriteAttributeString("displayName", "Normal")
                            xmlwriter.WriteEndElement() 'interpretationCode END

                            xmlwriter.WriteStartElement("referenceRange")
                            xmlwriter.WriteStartElement("observationRange")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'observationRange END
                            xmlwriter.WriteEndElement() 'referenceRanget END


                            xmlwriter.WriteEndElement() 'observation element END
                            xmlwriter.WriteEndElement() 'component element END
                        End If
                        ''Added Pulse Ox-MAYURI-20111216
                        If dtvitals.Rows(i)("dPulseOx") > 0 Then
                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("observation")
                            xmlwriter.WriteAttributeString("classCode", "OBS")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.14")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.2")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END

                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            'Code Added by kanchan on 20101101
                            'xmlwriter.WriteAttributeString("code", "11377-9")
                            xmlwriter.WriteAttributeString("code", "2710-2")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                            ' xmlwriter.WriteAttributeString("displayName", "OXYGEN SATURATION")

                            xmlwriter.WriteStartElement("translation")
                            xmlwriter.WriteAttributeString("code", "252465000")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED")
                            '  xmlwriter.WriteAttributeString("displayName", "Diastolic BP")
                            xmlwriter.WriteEndElement()
                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'text element END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            If Not IsNothing(dtvitals.Rows(i)("dPulseOx")) AndAlso dtvitals.Rows(i)("dPulseOx") <> 0 Then
                                xmlwriter.WriteStartElement("value")
                                xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dPulseOx"))
                                xmlwriter.WriteAttributeString("unit", "%")
                                xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                xmlwriter.WriteEndElement() 'value element END
                            End If


                            xmlwriter.WriteStartElement("interpretationCode")
                            xmlwriter.WriteAttributeString("displayName", "Normal")
                            xmlwriter.WriteEndElement() 'interpretationCode END

                            xmlwriter.WriteStartElement("referenceRange")
                            xmlwriter.WriteStartElement("observationRange")
                            xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                            xmlwriter.WriteEndElement() 'observationRange END
                            xmlwriter.WriteEndElement() 'referenceRanget END


                            xmlwriter.WriteEndElement() 'observation element END
                            xmlwriter.WriteEndElement() 'component element END
                        End If
                        ''End code added for Pulse OX

                        xmlwriter.WriteEndElement() 'organizer element END

                        xmlwriter.WriteEndElement() 'entry element END
                    Next

                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END

            End If

            ''$$$$$$$$$$$$$  -- Component (VITALS) ---END---$$$$$$$$$$$$$$$$$$

            ' ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            ' ''$$$$$$$$$$$$$  -- Component (Medications) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Medications") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Medications")

                xmlwriter.WriteStartElement("section")
                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.112")
                xmlwriter.WriteEndElement() 'templateId element END

                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.19")
                xmlwriter.WriteEndElement() 'templateId element END

                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HL7 CCD")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.8")
                xmlwriter.WriteEndElement() 'templateId element END


                '----------code 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "10160-0")
                xmlwriter.WriteAttributeString("displayName", "History of medication use")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")

                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Medications")
                '-----------text
                xmlwriter.WriteStartElement("text")
                'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")
                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Drug Name")
                xmlwriter.WriteElementString("th", "Generic Name")

                xmlwriter.WriteElementString("th", "Strength")
                xmlwriter.WriteElementString("th", "Dose")
                xmlwriter.WriteElementString("th", "Quantity")
                xmlwriter.WriteElementString("th", "Days")
                xmlwriter.WriteElementString("th", "Refills")
                xmlwriter.WriteElementString("th", "Sig")
                xmlwriter.WriteElementString("th", "Route")
                xmlwriter.WriteElementString("th", "Diagnosis")
                xmlwriter.WriteElementString("th", "Pharmacy")
                xmlwriter.WriteElementString("th", "Status")
                xmlwriter.WriteElementString("th", "NDC Code")
                xmlwriter.WriteElementString("th", "RxNorm Code")
                xmlwriter.WriteElementString("th", "Verified on")

                xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement()
                xmlwriter.WriteStartElement("tbody")
                If Not IsNothing(mPatient.PatientMedications) AndAlso mPatient.PatientMedications.Count > 0 Then

                    For Each oMedications As Medication In mPatient.PatientMedications
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oMedications.DrugName)
                        xmlwriter.WriteElementString("td", oMedications.GenericName)

                        xmlwriter.WriteElementString("td", oMedications.DrugStrength)
                        xmlwriter.WriteElementString("td", oMedications.DrugQuantity + " " + oMedications.DrugForm)
                        xmlwriter.WriteElementString("td", oMedications.DrugQuantity)
                        xmlwriter.WriteElementString("td", oMedications.Days)
                        xmlwriter.WriteElementString("td", oMedications.Refills)
                        xmlwriter.WriteElementString("td", oMedications.Frequency)
                        xmlwriter.WriteElementString("td", oMedications.Route)
                        xmlwriter.WriteElementString("td", oMedications.CheifComplaint)
                        xmlwriter.WriteElementString("td", oMedications.Pharmacy)
                        xmlwriter.WriteElementString("td", oMedications.Status)

                        xmlwriter.WriteElementString("td", oMedications.ProdCode)
                        xmlwriter.WriteElementString("td", oMedications.RxNormCode)
                        xmlwriter.WriteElementString("td", oMedications.MedicationDate)
                        xmlwriter.WriteEndElement() 'tr element end

                    Next


                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "6")
                    xmlwriter.WriteEndElement() 'End of td.
                    xmlwriter.WriteEndElement() 'tr element end
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() ' table element end
                xmlwriter.WriteEndElement() 'text element END


                If Not IsNothing(mPatient.PatientMedications) AndAlso mPatient.PatientMedications.Count > 0 Then

                    For Each oMedications As Medication In mPatient.PatientMedications

                        '-----------entry
                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")

                        '-----------entry - substanceAdministration 
                        xmlwriter.WriteStartElement("substanceAdministration")
                        xmlwriter.WriteAttributeString("classCode", "SBADM")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.8")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.24")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------entry - substanceAdministration - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7.1")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-------------id
                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() 'End id

                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement()

                        '' --------- effectiveTime 
                        xmlwriter.WriteStartElement("effectiveTime")
                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteAttributeString("operator", "A")
                        ''End Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteAttributeString("xsi:type", "IVL_TS")

                        '' LOW
                        If Not IsNothing(oMedications.MedicationDate) AndAlso oMedications.MedicationDate <> "" Then
                            strDate = Format(CType(oMedications.MedicationDate, Date), "yyyyMMdd")
                        Else
                            strDate = dtTodayDate
                        End If

                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement()

                        '' high 
                        xmlwriter.WriteStartElement("high")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement()

                        xmlwriter.WriteEndElement() '' END --------- effectiveTime 


                        '' routeCode   
                        xmlwriter.WriteStartElement("routeCode")
                        If Not IsNothing(oMedications.FDACode) AndAlso oMedications.FDACode <> "" Then
                            xmlwriter.WriteAttributeString("code", oMedications.FDACode)
                        Else
                            xmlwriter.WriteAttributeString("code", """")
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.3.26.1.1")
                        xmlwriter.WriteAttributeString("codeSystemName", "FDA RouteOfAdministration")
                        If Not IsNothing(oMedications.Route) AndAlso oMedications.Route <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oMedications.Route)
                        End If
                        xmlwriter.WriteStartElement("translation")
                        If Not IsNothing(oMedications.HL7Code) AndAlso oMedications.HL7Code <> "" Then
                            xmlwriter.WriteAttributeString("code", oMedications.HL7Code)
                        Else
                            xmlwriter.WriteAttributeString("code", """")
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.112")
                        xmlwriter.WriteAttributeString("codeSystemName", "HL7 RouteOfAdministration")
                        If Not IsNothing(oMedications.Route) AndAlso oMedications.Route <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oMedications.Route)
                        End If
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement() '' END routeCode 

                        '-----------doseQuantity 
                        xmlwriter.WriteStartElement("doseQuantity")
                        xmlwriter.WriteAttributeString("value", 1)
                        xmlwriter.WriteEndElement() 'doseQuantity element end

                        '-----------rateQuantity 
                        xmlwriter.WriteStartElement("rateQuantity")
                        xmlwriter.WriteAttributeString("nullFlavor", "NA")
                        xmlwriter.WriteEndElement() 'doseQuantity element end

                        '-----------entry - substanceAdministration - consumable  
                        xmlwriter.WriteStartElement("consumable")

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct  
                        xmlwriter.WriteStartElement("manufacturedProduct")
                        'xmlwriter.WriteAttributeString("classCode", "MANU")

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.8.2")
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.53")
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7.2")
                        xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - manufacturedMaterial 
                        xmlwriter.WriteStartElement("manufacturedMaterial")

                        '-----------entry - substanceAdministration - consumable - manufacturedProduct - code 
                        xmlwriter.WriteStartElement("code")

                        If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                            xmlwriter.WriteAttributeString("displayName", oMedications.DrugName)
                        End If

                        If Not IsNothing(oMedications.RxNormCode) AndAlso oMedications.RxNormCode <> "" Then
                            xmlwriter.WriteAttributeString("code", oMedications.RxNormCode)
                        Else
                            '  xmlwriter.WriteAttributeString("code", "309362")
                        End If
                        'xmlwriter.WriteAttributeString("code", "309362")
                        xmlwriter.WriteAttributeString("codeSystemName", "RxNorm")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.88")

                        xmlwriter.WriteStartElement("originalText")
                        If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                            xmlwriter.WriteValue(oMedications.DrugName)
                        End If
                        xmlwriter.WriteElementString("reference", "")
                        xmlwriter.WriteEndElement() 'originalText

                        xmlwriter.WriteStartElement("translation")
                        If Not IsNothing(oMedications.ProdCode) AndAlso oMedications.ProdCode <> "" Then
                            xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
                        End If

                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.69")
                        xmlwriter.WriteAttributeString("codeSystemName", "NDC")
                        If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                            xmlwriter.WriteAttributeString("displayName", oMedications.DrugName)
                        End If
                        'xmlwriter.WriteAttributeString("displayName", "Plavix")
                        xmlwriter.WriteEndElement()

                        xmlwriter.WriteEndElement() 'End code
                        If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
                            xmlwriter.WriteElementString("name", oMedications.DrugName)
                        End If

                        xmlwriter.WriteEndElement() 'manufacturedMaterial element END

                        xmlwriter.WriteEndElement() 'manufacturedProduct element END

                        xmlwriter.WriteEndElement() 'consumable element END

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "REFR")

                        '-----------observation
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.47")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------code 
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "33999-4")
                        xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                        xmlwriter.WriteAttributeString("displayName", "Status")
                        xmlwriter.WriteEndElement() 'code element end

                        '-----------value  
                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("xsi:type", "CE")
                        xmlwriter.WriteAttributeString("code", "55561003")
                        xmlwriter.WriteAttributeString("displayName", "Active")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteEndElement() 'value END

                        xmlwriter.WriteEndElement() 'observation element END

                        xmlwriter.WriteEndElement() 'entryRelationship element END


                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "REFR")

                        '-----------observation
                        xmlwriter.WriteStartElement("supply")
                        xmlwriter.WriteAttributeString("classCode", "SPLY")
                        xmlwriter.WriteAttributeString("moodCode", "INT")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.34")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.8.3")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7.3")
                        xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteEndElement() 'id

                        '-----------statusCode
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() 'statuscode element end

                        '-----------effectiveTime
                        strDate = Format(CType(oMedications.MedicationDate, Date), "yyyyMMdd")
                        If strDate = "" Then
                            strDate = dtTodayDate
                        End If
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteAttributeString("xsi:type", "IVL_TS")
                        xmlwriter.WriteStartElement("low")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() 'low element END
                        xmlwriter.WriteStartElement("high")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement() 'high element END
                        xmlwriter.WriteEndElement() 'effectiveTime element END

                        '-----------repeatNumber
                        xmlwriter.WriteStartElement("repeatNumber")
                        xmlwriter.WriteAttributeString("value", "1")
                        xmlwriter.WriteEndElement() 'repeatNumber end

                        '-----------quantity
                        xmlwriter.WriteStartElement("quantity")
                        xmlwriter.WriteAttributeString("value", "75")
                        xmlwriter.WriteAttributeString("unit", "mg")
                        xmlwriter.WriteEndElement() 'repeatNumber end

                        xmlwriter.WriteEndElement() 'supply end
                        xmlwriter.WriteEndElement() 'entryRelationship end

                        '-----------entryRelationship
                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")

                        '-----------supply
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        '-----------templateId 
                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.8.1")
                        xmlwriter.WriteEndElement() 'templateId element END

                        '-----------code 
                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "73639000")
                        xmlwriter.WriteAttributeString("displayName", "Prescription Drug")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteEndElement() 'code element end
                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        '-----------value  
                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("xsi:type", "CE")
                        xmlwriter.WriteAttributeString("code", "55561003")
                        xmlwriter.WriteAttributeString("displayName", "Active")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteEndElement() 'value END
                        ''End Added by Mayuri:20111215-To remove warnings in NIST Validator

                        xmlwriter.WriteEndElement() 'observation end
                        xmlwriter.WriteEndElement() 'entryRelationship end

                        xmlwriter.WriteEndElement() 'entry- substanceAdministration element END

                        xmlwriter.WriteEndElement() 'entry element END

                    Next

                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END
            End If

            '************ Lab Results *******************'

            If CCDSection.Contains("Results") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteComment("Results")

                xmlwriter.WriteStartElement("section")

                '------------templateId
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.14")
                xmlwriter.WriteEndElement() 'templateId element END

                '----------code 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "30954-2")
                xmlwriter.WriteAttributeString("displayName", "Results")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")

                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Diagnostic Results")
                '-----------text
                xmlwriter.WriteStartElement("text")
                'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

                If Not IsNothing(mPatient.LabTests) AndAlso mPatient.LabTests.Count > 0 Then

                    For Each oLabtests As LabTest In mPatient.LabTests

                        If oLabtests.LabResults.Count > 0 Then

                            xmlwriter.WriteStartElement("table")
                            xmlwriter.WriteAttributeString("width", "100%")
                            xmlwriter.WriteAttributeString("border", "1")
                            xmlwriter.WriteStartElement("tbody")

                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", "Lab Facility:")
                            xmlwriter.WriteStartElement("td")

                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            xmlwriter.WriteValue(oLabtests.LabResults.Item(0).LabFacility)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end

                            xmlwriter.WriteElementString("td", "Provider:")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            xmlwriter.WriteValue(oLabtests.LabResults.Item(0).ProviderName)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end

                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", "Requisition:")
                            xmlwriter.WriteStartElement("td")

                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            xmlwriter.WriteValue(oLabtests.LabResults.Item(0).ResultLOINCID)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end

                            xmlwriter.WriteElementString("td", "Specimen:")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            xmlwriter.WriteValue("")
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end

                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", "Collection Date:")
                            xmlwriter.WriteStartElement("td")

                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            If Not IsDBNull(oLabtests.LabResults.Item(0).SpecimenDate) AndAlso oLabtests.LabResults.Item(0).SpecimenDate <> "" Then
                                ' strDate = Format(CType(oLabtests.LabResults.Item(0).SpecimenDate, Date), "yyyyMMdd")
                                strDate = oLabtests.LabResults.Item(0).SpecimenDate.ToString()
                            Else
                                strDate = ""
                            End If

                            xmlwriter.WriteValue(strDate)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end

                            xmlwriter.WriteElementString("td", "Report Date:")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            If Not IsDBNull(oLabtests.LabResults.Item(0).ResultDate) AndAlso oLabtests.LabResults.Item(0).ResultDate <> "" Then
                                '  strDate = Format(CType(oLabtests.LabResults.Item(0).ResultDate, Date), "yyyyMMdd")
                                strDate = oLabtests.LabResults.Item(0).ResultDate.ToString()
                            Else
                                strDate = ""
                            End If
                            xmlwriter.WriteValue(strDate)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end


                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", "")
                            xmlwriter.WriteElementString("td", "")

                            xmlwriter.WriteElementString("td", "Reviewed:")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            ' xmlwriter.WriteValue("Unreviewed")
                            If IsNothing(oLabtests.LabResults.Item(0).IsAcknowledge) = False Then
                                If oLabtests.LabResults.Item(0).IsAcknowledge = True Then
                                    xmlwriter.WriteValue("Reviewed")
                                Else
                                    xmlwriter.WriteValue("Unreviewed")
                                End If
                            End If

                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end

                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteAttributeString("colspan", "2")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            xmlwriter.WriteValue("Test : " & oLabtests.LabResults.Item(0).TestName)
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end

                            xmlwriter.WriteElementString("td", "Test Type:")
                            xmlwriter.WriteStartElement("td")
                            xmlwriter.WriteStartElement("content")
                            xmlwriter.WriteAttributeString("styleCode", "Bold")
                            If Not IsNothing(oLabtests.LabResults.Item(0).ResultType) AndAlso oLabtests.LabResults.Item(0).ResultType <> "" Then
                                xmlwriter.WriteValue(oLabtests.LabResults.Item(0).ResultType)
                            Else
                                xmlwriter.WriteValue("")
                            End If
                            xmlwriter.WriteEndElement() 'content element end
                            xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end

                            xmlwriter.WriteEndElement() 'Tbody element end
                            xmlwriter.WriteEndElement() ' table element end

                            xmlwriter.WriteStartElement("table")
                            xmlwriter.WriteAttributeString("width", "100%")
                            xmlwriter.WriteAttributeString("border", "1")
                            xmlwriter.WriteStartElement("thead")
                            xmlwriter.WriteStartElement("tr")
                            'xmlwriter.WriteElementString("th", "Type")
                            xmlwriter.WriteElementString("th", "Component")
                            xmlwriter.WriteElementString("th", "LOINC Code")
                            xmlwriter.WriteElementString("th", "Abnormal")
                            xmlwriter.WriteElementString("th", "Result")
                            xmlwriter.WriteElementString("th", "Units")
                            xmlwriter.WriteElementString("th", "Ref Range")
                            xmlwriter.WriteEndElement() 'tr element end
                            xmlwriter.WriteEndElement() 'thead element end
                            xmlwriter.WriteStartElement("tbody")

                            If Not IsNothing(oLabtests.LabResults) AndAlso oLabtests.LabResults.Count > 0 Then
                                For Each oLabResults As LabResults In oLabtests.LabResults
                                    xmlwriter.WriteStartElement("tr")
                                    'oLabtests.LabResults.Item(0).ResultName
                                    xmlwriter.WriteElementString("td", oLabResults.ResultName)
                                    xmlwriter.WriteElementString("td", oLabResults.ResultLOINCID)
                                    xmlwriter.WriteStartElement("td")
                                    xmlwriter.WriteAttributeString("align", "center")
                                    xmlwriter.WriteValue(oLabResults.AbnormalFlag)
                                    xmlwriter.WriteEndElement() 'td element end
                                    xmlwriter.WriteStartElement("td")
                                    xmlwriter.WriteAttributeString("align", "center")
                                    xmlwriter.WriteValue(oLabResults.ResultValue)
                                    xmlwriter.WriteEndElement() 'td element end
                                    xmlwriter.WriteStartElement("td")
                                    xmlwriter.WriteAttributeString("align", "center")
                                    xmlwriter.WriteValue(oLabResults.ResultUnit)
                                    xmlwriter.WriteEndElement() 'td element end
                                    xmlwriter.WriteStartElement("td")
                                    xmlwriter.WriteAttributeString("align", "center")
                                    xmlwriter.WriteValue(oLabResults.ResultRange)
                                    xmlwriter.WriteEndElement() 'td element end

                                    xmlwriter.WriteEndElement() 'tr element end
                                    If oLabResults.ResultComment <> "" Then
                                        xmlwriter.WriteStartElement("tr")
                                        xmlwriter.WriteElementString("td", "")
                                        xmlwriter.WriteStartElement("td")
                                        xmlwriter.WriteAttributeString("colspan", "6")
                                        xmlwriter.WriteValue(oLabResults.ResultComment)
                                        xmlwriter.WriteEndElement() 'td element end
                                        xmlwriter.WriteEndElement() 'tr element end
                                    End If
                                Next
                            Else
                                xmlwriter.WriteStartElement("tr")
                                xmlwriter.WriteStartElement("td")
                                xmlwriter.WriteAttributeString("colspan", "6")
                                xmlwriter.WriteEndElement() 'end of td
                                xmlwriter.WriteEndElement() 'end of tr
                            End If
                            xmlwriter.WriteEndElement() 'Tbody element end
                            xmlwriter.WriteEndElement() ' table element end
                            xmlwriter.WriteStartElement("paragraph")
                            xmlwriter.WriteEndElement() ' paragraph element end

                        End If
                    Next

                End If
                xmlwriter.WriteEndElement() 'text element END

                If Not IsNothing(mPatient.LabTests) AndAlso mPatient.LabTests.Count > 0 Then

                    For Each oLabtests As LabTest In mPatient.LabTests

                        If oLabtests.LabResults.Count > 0 Then

                            '-----------entry
                            xmlwriter.WriteStartElement("entry")

                            '-----------entry - organizer 
                            xmlwriter.WriteStartElement("organizer")
                            xmlwriter.WriteAttributeString("classCode", "BATTERY")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")

                            '-----------entry - templateId 
                            xmlwriter.WriteStartElement("templateId")
                            'xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.32")
                            xmlwriter.WriteEndElement() 'entry- templateId element END


                            '-----------entry - id
                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                            xmlwriter.WriteEndElement() 'entry- id element END

                            '-----------entry - code
                            xmlwriter.WriteStartElement("code")
                            xmlwriter.WriteAttributeString("code", "43789009")
                            If Not IsNothing(oLabtests.LabResults.Item(0).TestName) AndAlso oLabtests.LabResults.Item(0).TestName <> "" Then
                                xmlwriter.WriteAttributeString("displayName", oLabtests.LabResults.Item(0).TestName)
                            End If
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                            xmlwriter.WriteEndElement() 'entry - code element END


                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            If Not IsDBNull(oLabtests.LabResults.Item(0).SpecimenDate) AndAlso oLabtests.LabResults.Item(0).SpecimenDate <> "" Then
                                strDate = Format(CType(oLabtests.LabResults.Item(0).SpecimenDate, Date), "yyyyMMdd")
                            Else
                                strDate = dtTodayDate
                            End If
                            'strDate = strDate & Now.Hour & Now.Minute
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END

                            xmlwriter.WriteStartElement("component")
                            xmlwriter.WriteStartElement("procedure")
                            xmlwriter.WriteAttributeString("classCode", "PROC")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.17")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.29")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() 'templateId element END



                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.19")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() 'templateId element END


                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteEndElement() 'id element END

                            xmlwriter.WriteStartElement("code")
                            xmlwriter.WriteAttributeString("code", "43789009")
                            xmlwriter.WriteAttributeString("displayName", "CBC WO DIFFERENTIAL")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                            xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")

                            ''Code here
                            xmlwriter.WriteStartElement("originalText")
                            xmlwriter.WriteString("Extract blood for CBC test")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "Ptr to text  in parent Section")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'originalText END

                            xmlwriter.WriteEndElement() 'code element END

                            xmlwriter.WriteStartElement("text")
                            xmlwriter.WriteString("Extract blood for CBC test. Note that IHE rules require description and reference to go here rather than in originalText of code.")
                            xmlwriter.WriteStartElement("reference")
                            xmlwriter.WriteAttributeString("value", "Ptr to text  in parent Section")
                            xmlwriter.WriteEndElement() 'reference element END
                            xmlwriter.WriteEndElement() 'originalText END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() 'statusCode element END

                            xmlwriter.WriteStartElement("effectiveTime")
                            xmlwriter.WriteAttributeString("value", strDate)
                            xmlwriter.WriteEndElement() 'effectiveTime element END
                            ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                            xmlwriter.WriteStartElement("targetSiteCode")
                            xmlwriter.WriteEndElement() 'targetSiteCode element END
                            ''End ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                            ''Code here
                            xmlwriter.WriteStartElement("performer")
                            xmlwriter.WriteStartElement("assignedEntity")
                            xmlwriter.WriteStartElement("id")
                            xmlwriter.WriteAttributeString("extension", "PseudoMD-1")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.72.5.2")
                            xmlwriter.WriteEndElement() 'ID END

                            xmlwriter.WriteElementString("addr", "See documentationOf in Header")

                            xmlwriter.WriteStartElement("telecom")
                            xmlwriter.WriteEndElement() 'telecom END

                            xmlwriter.WriteEndElement() 'assignedEntity
                            xmlwriter.WriteEndElement() 'performer

                            xmlwriter.WriteEndElement() 'Procedure element END
                            xmlwriter.WriteEndElement() 'component element END

                            If Not IsNothing(oLabtests.LabResults) AndAlso oLabtests.LabResults.Count > 0 Then
                                For Each oLabResults As LabResults In oLabtests.LabResults
                                    xmlwriter.WriteStartElement("component")
                                    xmlwriter.WriteStartElement("observation")
                                    xmlwriter.WriteAttributeString("classCode", "OBS")
                                    xmlwriter.WriteAttributeString("moodCode", "EVN")

                                    xmlwriter.WriteStartElement("templateId")
                                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.15.1")
                                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                                    xmlwriter.WriteEndElement() 'templateId element END

                                    xmlwriter.WriteStartElement("templateId")
                                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
                                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                                    xmlwriter.WriteEndElement() 'templateId element END

                                    xmlwriter.WriteStartElement("templateId")
                                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                                    xmlwriter.WriteEndElement() 'templateId element END

                                    xmlwriter.WriteStartElement("id")
                                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                                    xmlwriter.WriteEndElement() 'id element END

                                    xmlwriter.WriteStartElement("code")
                                    If Not IsDBNull(oLabtests.LabResults.Item(0).ResultLOINCID) AndAlso oLabtests.LabResults.Item(0).ResultLOINCID <> "" Then
                                        xmlwriter.WriteAttributeString("code", oLabtests.LabResults.Item(0).ResultLOINCID)
                                    End If
                                    If Not IsDBNull(oLabtests.LabResults.Item(0).ResultName) AndAlso oLabtests.LabResults.Item(0).ResultName <> "" Then
                                        xmlwriter.WriteAttributeString("displayName", oLabtests.LabResults.Item(0).ResultName)
                                    End If
                                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                                    xmlwriter.WriteEndElement() 'code element END

                                    xmlwriter.WriteStartElement("text")
                                    xmlwriter.WriteStartElement("reference")
                                    xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                                    xmlwriter.WriteEndElement() 'reference element END
                                    xmlwriter.WriteEndElement() 'originalText END

                                    xmlwriter.WriteStartElement("statusCode")
                                    xmlwriter.WriteAttributeString("code", "completed")
                                    xmlwriter.WriteEndElement() 'statusCode element END

                                    xmlwriter.WriteStartElement("effectiveTime")
                                    xmlwriter.WriteAttributeString("value", strDate)
                                    xmlwriter.WriteEndElement() 'effectiveTime element END

                                    ''Modified by Mayuri:20111215-To remove warnings in NIST Validator
                                    ''To allow varchar values in results
                                    If Not IsNothing(oLabtests.LabResults.Item(0).ResultValue) AndAlso oLabtests.LabResults.Item(0).ResultValue <> "" Then
                                        If IsNumeric(oLabtests.LabResults.Item(0).ResultValue) Then
                                            xmlwriter.WriteStartElement("value")
                                            xmlwriter.WriteAttributeString("xsi:type", "PQ")
                                            xmlwriter.WriteAttributeString("value", oLabtests.LabResults.Item(0).ResultValue)
                                            If Not IsNothing(oLabtests.LabResults.Item(0).ResultUnit) AndAlso oLabtests.LabResults.Item(0).ResultUnit <> "" Then
                                                xmlwriter.WriteAttributeString("unit", oLabtests.LabResults.Item(0).ResultUnit)
                                            Else
                                                xmlwriter.WriteAttributeString("unit", "%")
                                            End If
                                            xmlwriter.WriteEndElement() 'value element END
                                        Else
                                            xmlwriter.WriteStartElement("value")
                                            xmlwriter.WriteAttributeString("xsi:type", "ST")
                                            xmlwriter.WriteValue(oLabtests.LabResults.Item(0).ResultValue)
                                            xmlwriter.WriteEndElement() 'value element END

                                        End If

                                    End If
                                    ''End code Modified by Mayuri:20111215-To remove warnings in NIST Validator
                                    xmlwriter.WriteStartElement("interpretationCode")
                                    xmlwriter.WriteAttributeString("code", "N")
                                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.83")
                                    xmlwriter.WriteEndElement() 'interpretationCode END

                                    If Not IsNothing(oLabtests.LabResults.Item(0).ResultRange) AndAlso oLabtests.LabResults.Item(0).ResultRange <> "" Then
                                        xmlwriter.WriteStartElement("referenceRange")
                                        xmlwriter.WriteStartElement("observationRange")
                                        xmlwriter.WriteElementString("text", oLabtests.LabResults.Item(0).ResultRange)
                                        xmlwriter.WriteEndElement() 'observationRange element END
                                        xmlwriter.WriteEndElement() 'referenceRange element END
                                    End If

                                    xmlwriter.WriteEndElement() 'observation element END
                                    xmlwriter.WriteEndElement() 'component element END
                                Next
                            End If
                            xmlwriter.WriteEndElement() 'entry- organizer element END
                            xmlwriter.WriteEndElement() 'entry element END
                        End If
                    Next

                End If

                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END
            End If
            '*****************************Lab Results End *****************************************************


            ''$$$$$$$$$$$$$  -- Component (IMMUNIZATIONS) ---START---$$$$$$$$$$$$$$$$$$

            If CCDSection.Contains("Immunization") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                If Not IsNothing(mPatient.PatientImmunizations) AndAlso mPatient.PatientImmunizations.Count > 0 Then
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.117")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.23")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.6")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HL7 CCD")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "11369-6")
                    xmlwriter.WriteAttributeString("displayName", "Patient Immunizations")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                    xmlwriter.WriteEndElement() ''''''''code END
                End If

                xmlwriter.WriteElementString("title", "Immunizations")

                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Vaccine")
                xmlwriter.WriteElementString("th", "Vaccine Code")
                xmlwriter.WriteElementString("th", "Route")
                xmlwriter.WriteElementString("th", "Route Code")
                xmlwriter.WriteElementString("th", "Administration Date")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")

                If Not IsNothing(mPatient.PatientImmunizations) AndAlso mPatient.PatientImmunizations.Count > 0 Then
                    For Each oImmunization As Immunization In mPatient.PatientImmunizations
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oImmunization.VaccineName)
                        xmlwriter.WriteElementString("td", oImmunization.VaccineCode)
                        xmlwriter.WriteElementString("td", oImmunization.Route)
                        xmlwriter.WriteElementString("td", oImmunization.RouteCode)
                        xmlwriter.WriteElementString("td", oImmunization.ImmunizationDate)
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "5")
                    xmlwriter.WriteEndElement() 'end of td
                    xmlwriter.WriteEndElement() 'end of tr
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                If Not IsNothing(mPatient.PatientImmunizations) AndAlso mPatient.PatientImmunizations.Count > 0 Then
                    For Each oImmunization As Immunization In mPatient.PatientImmunizations
                        '------------entry
                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")

                        xmlwriter.WriteStartElement("substanceAdministration")
                        xmlwriter.WriteAttributeString("classCode", "SBADM")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")
                        xmlwriter.WriteAttributeString("negationInd", "false")

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.24")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.13")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.12")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                        xmlwriter.WriteEndElement() ''''''''id END

                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "IMMUNIZ")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
                        xmlwriter.WriteAttributeString("codeSystemName", "HL7 ActCode")
                        xmlwriter.WriteEndElement() ''''''''code END
                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteStartElement("text")
                        xmlwriter.WriteStartElement("reference")
                        xmlwriter.WriteAttributeString("value", "#xxx")
                        xmlwriter.WriteEndElement() 'reference element END
                        xmlwriter.WriteEndElement() ''''''''text END
                        ''End code added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() ''''''''statusCode END

                        ''Verify strDate Format.
                        If Not IsNothing(oImmunization.ImmunizationDate) AndAlso oImmunization.ImmunizationDate <> "" Then
                            strDate = Format(CType(oImmunization.ImmunizationDate, Date), "yyyyMMdd")
                        Else
                            strDate = dtTodayDate
                        End If

                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteAttributeString("xsi:type", "IVL_TS")
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement() ''''''''effectiveTime END

                        xmlwriter.WriteStartElement("routeCode")
                        If Not IsNothing(oImmunization.RouteCode) AndAlso oImmunization.RouteCode <> "" Then
                            xmlwriter.WriteAttributeString("code", oImmunization.RouteCode)
                        Else
                            'xmlwriter.WriteAttributeString("code", "PO")
                        End If
                        If Not IsNothing(oImmunization.Route) AndAlso oImmunization.Route <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oImmunization.Route)
                        Else
                            'xmlwriter.WriteAttributeString("displayName", "Oral")
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.112")
                        xmlwriter.WriteAttributeString("codeSystemName", "RouteOfAdministration")
                        xmlwriter.WriteEndElement() ''''''''routeCode END
                        ''
                        ''Added by Mayuri:20111215-To remove warnings in NIST Validator
                        xmlwriter.WriteStartElement("approachSiteCode")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteStartElement("originalText")
                        xmlwriter.WriteStartElement("reference")
                        xmlwriter.WriteAttributeString("value", "PtrToSectionText")
                        xmlwriter.WriteEndElement() 'reference element END
                        xmlwriter.WriteEndElement() ''''''''originalText END
                        xmlwriter.WriteEndElement() ''''''''end approachSiteCode
                        ''
                        ''end code added by Mayuri:20111215-To remove warnings in NIST Validator
                        ''Mahesh B
                        xmlwriter.WriteStartElement("doseQuantity")
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement() ''''''''

                        xmlwriter.WriteStartElement("consumable")
                        xmlwriter.WriteStartElement("manufacturedProduct")

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.53")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.7.2")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("manufacturedMaterial")

                        xmlwriter.WriteStartElement("code")
                        If Not IsNothing(oImmunization.VaccineCode) AndAlso oImmunization.VaccineCode <> "" Then
                            xmlwriter.WriteAttributeString("code", oImmunization.VaccineCode)
                        End If
                        If Not IsNothing(oImmunization.VaccineName) AndAlso oImmunization.VaccineName <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oImmunization.VaccineName)
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.59")
                        xmlwriter.WriteStartElement("originalText")
                        If Not IsNothing(oImmunization.VaccineName) AndAlso oImmunization.VaccineName <> "" Then
                            xmlwriter.WriteValue(oImmunization.VaccineName)
                        End If
                        xmlwriter.WriteStartElement("reference")
                        xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                        xmlwriter.WriteEndElement() 'reference element END
                        xmlwriter.WriteEndElement() ''''''''originalText END

                        xmlwriter.WriteEndElement() ''''''''code END
                        xmlwriter.WriteStartElement("name")
                        xmlwriter.WriteEndElement()
                        'If Not IsNothing(oImmunization.LotNumber) AndAlso oImmunization.LotNumber <> "" Then
                        xmlwriter.WriteElementString("lotNumberText", oImmunization.LotNumber)
                        ' End If

                        xmlwriter.WriteEndElement() ''''''''manufacturedMaterial END

                        xmlwriter.WriteEndElement() ''''''''manufacturedProduct END

                        xmlwriter.WriteEndElement() ''''''''consumable END

                        xmlwriter.WriteStartElement("performer")
                        xmlwriter.WriteStartElement("assignedEntity")
                        xmlwriter.WriteElementString("id", "")
                        xmlwriter.WriteElementString("addr", "")
                        xmlwriter.WriteElementString("telecom", "")
                        xmlwriter.WriteEndElement() ''''''''assignedEntity END
                        xmlwriter.WriteEndElement() ''''''''performer END

                        xmlwriter.WriteEndElement() ''''''''substanceAdministration END
                        xmlwriter.WriteEndElement() ''''''''entry END
                    Next
                End If
                xmlwriter.WriteEndElement() 'section element END
                xmlwriter.WriteEndElement() 'component element END

            End If
            ''$$$$$$$$$$$$$  -- Component (IMMUNIZATIONS) ---END---$$$$$$$$$$$$$$$$$$


            ''$$$$$$$$$$$$$  -- Component (FAMILY HISTORY) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("FamilyHistory") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.125")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteEndElement() ''''''''templateId END

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.4")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteEndElement() ''''''''templateId END

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.14")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                xmlwriter.WriteEndElement() ''''''''templateId END

                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "10157-6")
                xmlwriter.WriteAttributeString("displayName", "HISTORY OF FAMILY MEMBER DISEASES")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteEndElement() ''''''''code END

                '-----------title
                xmlwriter.WriteElementString("title", "Family History")

                '-----------text

                xmlwriter.WriteStartElement("text")
                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")

                xmlwriter.WriteElementString("th", "Description")
                xmlwriter.WriteElementString("th", "Family Member")
                'xmlwriter.WriteElementString("th", "Qualifiers")
                xmlwriter.WriteElementString("th", "Comments")
                xmlwriter.WriteElementString("th", "Date Reported")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")

                Dim strRelation As String = ""
                Dim strHistoryItem As String = ""
                Dim strDescription As String = ""
                Dim strComments As String = ""
                Dim strHistoryId As String = ""

                If Not IsNothing(mPatient.PatientFamilyHistory) AndAlso mPatient.PatientFamilyHistory.Count > 0 Then
                    For Each oFamilyHistory As FamilyHistory In mPatient.PatientFamilyHistory
                        If strHistoryId <> oFamilyHistory.FmlyHxHistoryId Then
                            ''If Not strHistoryItem.Contains(oFamilyHistory.FmlyHxDescription) Or strHistoryId <> oFamilyHistory.FmlyHxHistoryId Or strComments <> oFamilyHistory.FmlyHxComments Then 'strComments <> oFamilyHistory.FmlyHxComments Or
                            strHistoryItem = strHistoryItem + "," + oFamilyHistory.FmlyHxDescription
                            strDescription = oFamilyHistory.FmlyHxDescription
                            strComments = oFamilyHistory.FmlyHxComments
                            strHistoryId = oFamilyHistory.FmlyHxHistoryId
                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxDescription)
                            If Not IsNothing(mPatient.PatientFamilyHistory) AndAlso mPatient.PatientFamilyHistory.Count > 0 Then
                                For Each oFamilyHistory1 As FamilyHistory In mPatient.PatientFamilyHistory
                                    If strHistoryId = oFamilyHistory1.FmlyHxHistoryId Then ' And
                                        ''If strDescription = oFamilyHistory1.FmlyHxDescription And strHistoryId = oFamilyHistory1.FmlyHxHistoryId And strComments = oFamilyHistory1.FmlyHxComments Then ' And
                                        If strRelation = "" Then
                                            strRelation = oFamilyHistory1.FmlyHxRelation
                                        Else
                                            strRelation = strRelation + "," + oFamilyHistory1.FmlyHxRelation
                                        End If
                                    End If
                                Next
                            End If
                            xmlwriter.WriteElementString("td", strRelation)
                            xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxComments)
                            xmlwriter.WriteElementString("td", Convert.ToDateTime(oFamilyHistory.FmlyHxDateReported).Date)
                            xmlwriter.WriteEndElement() 'tr element end
                            strRelation = ""
                        End If
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "4")
                    xmlwriter.WriteEndElement()
                    xmlwriter.WriteEndElement()
                End If
                strRelation = Nothing

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end
                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim strEntrySubject As String = "strEntrySubject"
                Dim strEntrySubjectTemp As String = "strEntrySubjectTemp"
                Dim strEntryHistoryItem As String = "strEntryHistoryItem"
                If Not IsNothing(mPatient.PatientFamilyHistory) AndAlso mPatient.PatientFamilyHistory.Count > 0 Then
                    For Each oFamilyHistory As FamilyHistory In mPatient.PatientFamilyHistory
                        If oFamilyHistory.FmlyHxRelation = "" Then
                            oFamilyHistory.FmlyHxRelation = "Unknown"
                        End If
                        If Not strEntrySubject.Contains(oFamilyHistory.FmlyHxRelation) Then
                            '    'xmlwriter.WriteElementString("td", "")
                            '    strEntryHistoryItem = ""
                            'Else
                            xmlwriter.WriteStartElement("entry")
                            xmlwriter.WriteAttributeString("typeCode", "DRIV")
                            xmlwriter.WriteStartElement("organizer")
                            xmlwriter.WriteAttributeString("classCode", "CLUSTER")
                            xmlwriter.WriteAttributeString("moodCode", "EVN")

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.18")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                            xmlwriter.WriteEndElement() ''''''''templateId END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.23")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                            xmlwriter.WriteEndElement() ''''''''templateId END

                            xmlwriter.WriteStartElement("templateId")
                            xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.15")
                            xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                            xmlwriter.WriteEndElement() ''''''''templateId END

                            xmlwriter.WriteStartElement("statusCode")
                            xmlwriter.WriteAttributeString("code", "completed")
                            xmlwriter.WriteEndElement() ''''''''statusCode END

                            xmlwriter.WriteStartElement("subject")
                            xmlwriter.WriteStartElement("relatedSubject")
                            xmlwriter.WriteAttributeString("classCode", "PRS")

                            xmlwriter.WriteStartElement("code")
                            If oFamilyHistory.FmlyHxRelConceptID.ToString() = "" And oFamilyHistory.FmlyHxRelation.ToString() = "Unknown" Then
                                xmlwriter.WriteAttributeString("code", 261665006)
                                xmlwriter.WriteAttributeString("displayName", "Unknown")
                            Else
                                xmlwriter.WriteAttributeString("code", oFamilyHistory.FmlyHxRelConceptID)
                                xmlwriter.WriteAttributeString("displayName", oFamilyHistory.FmlyHxRelation)
                            End If

                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.111")
                            xmlwriter.WriteAttributeString("codeSystemName", "HL7 FamilyMember")
                            xmlwriter.WriteEndElement() ''''''''code END

                            xmlwriter.WriteElementString("addr", "")
                            xmlwriter.WriteElementString("telecom", "")

                            xmlwriter.WriteStartElement("subject")
                            xmlwriter.WriteElementString("name", "")

                            xmlwriter.WriteStartElement("administrativeGenderCode")
                            Select Case mPatient.Gender.ToUpper
                                Case "FEMALE"
                                    xmlwriter.WriteAttributeString("code", "F")
                                    xmlwriter.WriteAttributeString("displayName", "Female")
                                Case "MALE"
                                    xmlwriter.WriteAttributeString("code", "M")
                                    xmlwriter.WriteAttributeString("displayName", "Male")
                                Case "OTHER"
                                    xmlwriter.WriteAttributeString("code", "UN")
                                    xmlwriter.WriteAttributeString("displayName", "Undifferentiated")
                            End Select

                            xmlwriter.WriteAttributeString("codeSystemName", "HL7 AdministrativeGender")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.1")
                            xmlwriter.WriteEndElement() 'End administrativeGenderCode Element

                            xmlwriter.WriteStartElement("birthTime")
                            strDate = Format(CType(mPatient.DateofBirth, Date), "yyyyMMdd")
                            If strDate <> "" AndAlso strDate <> Nothing Then
                                xmlwriter.WriteAttributeString("value", strDate)
                            End If
                            xmlwriter.WriteEndElement() 'End BirthTime

                            xmlwriter.WriteEndElement() ''''''''subject END

                            xmlwriter.WriteEndElement() ''''''''relatedSubject END
                            xmlwriter.WriteEndElement() ''''''''subject END
                            If strEntrySubject = "strEntrySubject" Then
                                strEntrySubject = oFamilyHistory.FmlyHxRelation
                            Else
                                strEntrySubject = strEntrySubject + "," + oFamilyHistory.FmlyHxRelation
                            End If
                            strEntrySubjectTemp = oFamilyHistory.FmlyHxRelation
                            For Each oFamilyHistoryC As FamilyHistory In mPatient.PatientFamilyHistory
                                If oFamilyHistoryC.FmlyHxRelation = "" Then
                                    oFamilyHistoryC.FmlyHxRelation = "Unknown"
                                End If
                                If strEntrySubjectTemp = oFamilyHistoryC.FmlyHxRelation Then
                                    If strEntryHistoryItem <> oFamilyHistoryC.FmlyHxDescription Then


                                        xmlwriter.WriteStartElement("component")

                                        xmlwriter.WriteStartElement("observation")
                                        xmlwriter.WriteAttributeString("classCode", "OBS")
                                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                                        xmlwriter.WriteStartElement("templateId")
                                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.22")
                                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                                        xmlwriter.WriteEndElement() ''''''''templateId END


                                        xmlwriter.WriteStartElement("templateId")
                                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13")
                                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                                        xmlwriter.WriteEndElement() ''''''''templateId END

                                        xmlwriter.WriteStartElement("templateId")
                                        xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.13.3")
                                        xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                                        xmlwriter.WriteEndElement() ''''''''templateId END

                                        xmlwriter.WriteStartElement("id")
                                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                                        xmlwriter.WriteEndElement() ''''''''templateId END

                                        xmlwriter.WriteStartElement("code")
                                        xmlwriter.WriteAttributeString("code", "404684003")
                                        xmlwriter.WriteAttributeString("displayName", "Finding")
                                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                                        xmlwriter.WriteEndElement() ''''''''code END


                                        ''Verify strDate Format.
                                        If Not IsNothing(oFamilyHistory.FmlyHxDateReported) AndAlso oFamilyHistory.FmlyHxDateReported <> "" Then
                                            strDate = Format(CType(oFamilyHistory.FmlyHxDateReported, Date), "yyyyMMdd")
                                        Else
                                            strDate = dtTodayDate
                                        End If

                                        xmlwriter.WriteStartElement("text")
                                        xmlwriter.WriteStartElement("reference")
                                        xmlwriter.WriteAttributeString("value", "PtrToValueInsectionText")
                                        xmlwriter.WriteEndElement() 'reference element END
                                        xmlwriter.WriteEndElement() ''''''''text END

                                        xmlwriter.WriteStartElement("statusCode")
                                        xmlwriter.WriteAttributeString("code", "completed")
                                        xmlwriter.WriteEndElement() ''''''''statusCode END

                                        xmlwriter.WriteStartElement("effectiveTime")
                                        xmlwriter.WriteAttributeString("value", strDate)
                                        xmlwriter.WriteStartElement("low")
                                        xmlwriter.WriteAttributeString("value", strDate)
                                        xmlwriter.WriteEndElement() ''''''''low END
                                        xmlwriter.WriteEndElement() ''''''''effectiveTime END

                                        xmlwriter.WriteStartElement("value")
                                        xmlwriter.WriteAttributeString("xsi:type", "CD")
                                        If Not IsNothing(oFamilyHistoryC.FmlyHxConceptID) AndAlso oFamilyHistoryC.FmlyHxConceptID <> "" Then
                                            xmlwriter.WriteAttributeString("code", oFamilyHistoryC.FmlyHxConceptID)
                                        End If
                                        If Not IsNothing(oFamilyHistoryC.FmlyHxDescription) AndAlso oFamilyHistoryC.FmlyHxDescription <> "" Then
                                            xmlwriter.WriteAttributeString("displayName", oFamilyHistoryC.FmlyHxDescription)
                                            strEntryHistoryItem = oFamilyHistoryC.FmlyHxDescription
                                        End If
                                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                                        xmlwriter.WriteEndElement() ''''''''value END
                                        xmlwriter.WriteEndElement() ''''''''observation END

                                        xmlwriter.WriteEndElement() ''''''''component END
                                    End If
                                End If
                            Next
                            strEntryHistoryItem = ""

                            xmlwriter.WriteEndElement() ''''''''organizer END
                            xmlwriter.WriteEndElement() ''''''''entry END
                        End If
                    Next
                    ''End
                End If
                xmlwriter.WriteEndElement() '''''''section END
                xmlwriter.WriteEndElement() '''''''component END


                strRelation = Nothing
                strHistoryItem = Nothing
                strDescription = Nothing
                strComments = Nothing
                strHistoryId = Nothing
                strEntrySubject = Nothing
                strEntrySubjectTemp = Nothing
                strEntryHistoryItem = Nothing

            End If


            ''$$$$$$$$$$$$$  -- Component (FAMILY HISTORY) ---END---$$$$$$$$$$$$$$$$$$



            ''$$$$$$$$$$$$$  -- Component (SOCIAL HISTORY) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("SocialHistory") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                If Not IsNothing(mPatient.PatientSocialHistory) AndAlso mPatient.PatientSocialHistory.Count > 0 Then
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.15")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.126")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.3.16")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "29762-2")
                    xmlwriter.WriteAttributeString("displayName", "SOCIAL HISTORY")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                    xmlwriter.WriteEndElement() ''''''''code END
                End If

                '-----------title
                xmlwriter.WriteElementString("title", "Social History")

                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Description")
                xmlwriter.WriteElementString("th", "SNOMED Code")
                xmlwriter.WriteElementString("th", "Qualifiers")
                xmlwriter.WriteElementString("th", "Comments")
                xmlwriter.WriteElementString("th", "Date Reported")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")
                If Not IsNothing(mPatient.PatientSocialHistory) AndAlso mPatient.PatientSocialHistory.Count > 0 Then
                    For Each oSocialHistory As SocialHistory In mPatient.PatientSocialHistory
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oSocialHistory.SocialHxDescription)
                        xmlwriter.WriteElementString("td", oSocialHistory.SocialHxConceptID)
                        xmlwriter.WriteElementString("td", oSocialHistory.SocialHxQualifiers)
                        xmlwriter.WriteElementString("td", oSocialHistory.SocialHxComments)
                        xmlwriter.WriteElementString("td", oSocialHistory.SocialHxDateReported)
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "5")
                    xmlwriter.WriteEndElement() 'end of td
                    xmlwriter.WriteEndElement() 'end of tr
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If Not IsNothing(mPatient.PatientSocialHistory) AndAlso mPatient.PatientSocialHistory.Count > 0 Then
                    For Each oSocialHistory As SocialHistory In mPatient.PatientSocialHistory

                        xmlwriter.WriteStartElement("entry")
                        xmlwriter.WriteAttributeString("typeCode", "DRIV")
                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.19")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.33")
                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid().ToString())
                        xmlwriter.WriteEndElement() ''''''''id END

                        xmlwriter.WriteStartElement("code")
                        If Not IsNothing(oSocialHistory.SocialHxConceptID) AndAlso oSocialHistory.SocialHxConceptID <> "" Then
                            xmlwriter.WriteAttributeString("code", oSocialHistory.SocialHxConceptID)
                        Else
                            xmlwriter.WriteAttributeString("code", "261665006")
                        End If
                        If Not IsNothing(oSocialHistory.SocialHxDescription) AndAlso oSocialHistory.SocialHxDescription <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oSocialHistory.SocialHxDescription)
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteEndElement() ''''''''code END

                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() ''''''''statusCode END

                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low")

                        Dim dtDate As Date
                        If Not IsNothing(oSocialHistory.SocialHxDescription) AndAlso oSocialHistory.SocialHxDescription <> "" Then
                            dtDate = oSocialHistory.SocialHxDateReported
                        Else
                            dtDate = dtTodayDate
                        End If

                        xmlwriter.WriteAttributeString("value", dtDate.ToString("yyyyMMdd")) 'Now.ToString("yyyyMMdd"))
                        xmlwriter.WriteEndElement() ''''''''low END
                        xmlwriter.WriteEndElement() ''''''''effectiveTime END

                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("xsi:type", "ST")
                        xmlwriter.WriteValue("None")
                        xmlwriter.WriteEndElement() ''''''''value END

                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SUBJ")
                        xmlwriter.WriteAttributeString("inversionInd", "true")

                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        xmlwriter.WriteStartElement("templateId")
                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.41")
                        xmlwriter.WriteEndElement() ''''''''templateId END

                        xmlwriter.WriteStartElement("code")
                        xmlwriter.WriteAttributeString("code", "ASSERTION")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
                        xmlwriter.WriteEndElement() ''''''''code END

                        xmlwriter.WriteStartElement("statusCode")
                        xmlwriter.WriteAttributeString("code", "completed")
                        xmlwriter.WriteEndElement() ''''''''statusCode END

                        xmlwriter.WriteStartElement("value")
                        xmlwriter.WriteAttributeString("xsi:type", "CD")
                        xmlwriter.WriteAttributeString("code", "404684003")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("displayName", "Clinical finding")
                        xmlwriter.WriteEndElement() ''''''''value END

                        xmlwriter.WriteStartElement("entryRelationship")
                        xmlwriter.WriteAttributeString("typeCode", "SAS")

                        xmlwriter.WriteStartElement("observation")
                        xmlwriter.WriteAttributeString("classCode", "OBS")
                        xmlwriter.WriteAttributeString("moodCode", "EVN")

                        xmlwriter.WriteStartElement("id")
                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid().ToString())
                        xmlwriter.WriteEndElement() ''''''''id END

                        xmlwriter.WriteStartElement("code")
                        If Not IsNothing(oSocialHistory.SocialHxConceptID) AndAlso oSocialHistory.SocialHxConceptID <> "" Then
                            xmlwriter.WriteAttributeString("code", oSocialHistory.SocialHxConceptID)
                        Else
                            xmlwriter.WriteAttributeString("code", "261665006")
                        End If
                        If Not IsNothing(oSocialHistory.SocialHxDescription) AndAlso oSocialHistory.SocialHxDescription <> "" Then
                            xmlwriter.WriteAttributeString("displayName", oSocialHistory.SocialHxDescription)
                        End If
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
                        xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                        xmlwriter.WriteEndElement() ''''''''code END

                        xmlwriter.WriteEndElement() ''''''''observation END
                        xmlwriter.WriteEndElement() ''''''''entryRelationship END

                        xmlwriter.WriteEndElement() ''''''''observation END
                        xmlwriter.WriteEndElement() ''''''''entryRelationship END

                        xmlwriter.WriteEndElement() ''''''''observation END
                        xmlwriter.WriteEndElement() ''''''''entry END

                    Next
                End If
                xmlwriter.WriteEndElement() '''''''section END
                xmlwriter.WriteEndElement() '''''''component END
            End If

            ''$$$$$$$$$$$$$  -- Component (SOCIAL HISTORY) ---END---$$$$$$$$$$$$$$$$$$


            ''$$$$$$$$$$$$$  -- Component (PROCEDURES) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Procedures") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                ''Start-Added by Manoj:20141222-Sharing Template Id for procedure 
                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.12")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                xmlwriter.WriteEndElement()

                xmlwriter.WriteStartElement("templateId")
                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.108")
                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                xmlwriter.WriteEndElement()
                ''END-Added by Manoj:20141222-Sharing Template Id for procedure 


                ''Added by Mayuri:20120216-In discusion with diagnosis One-Loinc Code System 
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "47519-4") 'CCD code Changed by manoj jadhav on 20141222 from 29544-3 to 47519-4 
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("displayName", "History of procedures") 'CCD code Changed by manoj jadhav on 20141222 from 'procedures' to 'History of procedures'
                xmlwriter.WriteEndElement() 'code element END

                xmlwriter.WriteElementString("title", "Procedures")

                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Description")
                xmlwriter.WriteElementString("th", "Procedure")
                xmlwriter.WriteElementString("th", "SNOMED Code")
                xmlwriter.WriteElementString("th", "Diagnosis")
                xmlwriter.WriteElementString("th", "Provider")
                xmlwriter.WriteElementString("th", "Service Date")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")
                If Not IsNothing(mPatient.PatientProcedures) AndAlso mPatient.PatientProcedures.Count > 0 Then
                    For Each oProcedures As Procedures In mPatient.PatientProcedures
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oProcedures.CPTDescription)
                        xmlwriter.WriteElementString("td", oProcedures.CPTCode)
                        xmlwriter.WriteElementString("td", oProcedures.SnomedCode)
                        xmlwriter.WriteElementString("td", oProcedures.ICD9Description)
                        xmlwriter.WriteElementString("td", oProcedures.ProviderLastName & " " & oProcedures.ProviderFirstName & "" & oProcedures.ProviderSuffix)
                        xmlwriter.WriteElementString("td", oProcedures.DateOfService)
                        xmlwriter.WriteEndElement() 'tr element end
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "5")
                    xmlwriter.WriteEndElement()
                    xmlwriter.WriteEndElement() 'tr element end
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''Added by Mayuri:20111215-To remove warnings in NIST Validator
                ''Entry section was missing 
                ''Entry Procedures
                For Each oProcedures As Procedures In mPatient.PatientProcedures


                    '------------entry
                    xmlwriter.WriteStartElement("entry")
                    xmlwriter.WriteAttributeString("typeCode", "DRIV")

                    xmlwriter.WriteStartElement("procedure")
                    xmlwriter.WriteAttributeString("classCode", "PROC")
                    xmlwriter.WriteAttributeString("moodCode", "EVN")

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.17")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.29")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.19")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() ''''''''templateId END



                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                    xmlwriter.WriteEndElement() ''''''''id END

                    xmlwriter.WriteStartElement("code")
                    If Not IsNothing(oProcedures.CPTCode) AndAlso oProcedures.CPTCode <> "" Then
                        xmlwriter.WriteAttributeString("code", oProcedures.CPTCode)
                    End If
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.12")
                    xmlwriter.WriteAttributeString("codeSystemName", "CPT-4")

                    ''Original Text required
                    xmlwriter.WriteStartElement("originalText")
                    If Not IsNothing(oProcedures.CPTDescription) AndAlso oProcedures.CPTDescription <> "" Then
                        xmlwriter.WriteValue(oProcedures.CPTDescription)
                    End If

                    xmlwriter.WriteStartElement("reference")
                    xmlwriter.WriteAttributeString("value", "#Proc1")
                    xmlwriter.WriteEndElement() ''End reference
                    xmlwriter.WriteEndElement() ''End originalText
                    ''Added by MAYURI:20120528-ICD9Procedures code added(Diagnosis One Testing)
                    xmlwriter.WriteStartElement("translation")
                    If Not IsNothing(oProcedures.ICD9_code) AndAlso oProcedures.ICD9_code <> "" Then
                        xmlwriter.WriteAttributeString("code", oProcedures.ICD9_code)
                    End If


                    If Not IsNothing(oProcedures.ICDRevision) AndAlso oProcedures.ICDRevision <> 0 Then
                        If oProcedures.ICDRevision = 9 Then
                            xmlwriter.WriteAttributeString("codeSystemName", "ICD-9-CM")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.104")
                        ElseIf oProcedures.ICDRevision = 10 Then
                            xmlwriter.WriteAttributeString("codeSystemName", "ICD10CM")
                            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.90")
                        End If

                    Else
                        xmlwriter.WriteAttributeString("codeSystemName", "ICD-9-CM")
                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.104")
                    End If



                    xmlwriter.WriteEndElement() ''translation
                    xmlwriter.WriteStartElement("translation")
                    If Not IsNothing(oProcedures.CPTCode) AndAlso oProcedures.CPTCode <> "" Then
                        xmlwriter.WriteAttributeString("code", oProcedures.CPTCode)
                    End If

                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.14")

                    xmlwriter.WriteAttributeString("codeSystemName", "HCP")
                    xmlwriter.WriteEndElement() ''translation
                    xmlwriter.WriteStartElement("translation")
                    If Not IsNothing(oProcedures.SnomedCode) AndAlso oProcedures.SnomedCode <> "" Then
                        xmlwriter.WriteAttributeString("code", oProcedures.SnomedCode)
                    End If

                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")

                    xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
                    xmlwriter.WriteEndElement() ''translation
                    xmlwriter.WriteEndElement() ''End Code

                    ''
                    ''91.46 
                    ''

                    ''Start Text
                    xmlwriter.WriteStartElement("text")
                    xmlwriter.WriteValue("IHE Requires reference to go here instead of originalText of code.")
                    xmlwriter.WriteStartElement("reference") ''Start reference
                    xmlwriter.WriteEndElement() ''End reference
                    xmlwriter.WriteEndElement() ''End Text

                    xmlwriter.WriteStartElement("statusCode")
                    xmlwriter.WriteAttributeString("code", "completed")
                    xmlwriter.WriteEndElement() ''''''''statusCode END

                    If oProcedures.DateOfService <> "" Then
                        strDate = Format(CType(oProcedures.DateOfService, Date), "yyyyMMdd")
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low") 'Added by manoj jadhav on 20150106 for sharing procedures effective Time with low value attribute
                        xmlwriter.WriteAttributeString("value", strDate)
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement() ''''''''effectiveTime END
                    Else
                        xmlwriter.WriteStartElement("effectiveTime")
                        xmlwriter.WriteStartElement("low") 'Added by manoj jadhav on 20150106 for sharing procedures effective Time with low value attribute
                        xmlwriter.WriteAttributeString("nullFlavor", "UNK")
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement()
                    End If

                    xmlwriter.WriteStartElement("targetSiteCode") ''Start targetSiteCode


                    xmlwriter.WriteEndElement() ''End targetSiteCode
                    ''
                    xmlwriter.WriteStartElement("performer")
                    xmlwriter.WriteStartElement("assignedEntity")

                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("addr")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("telecom")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("assignedPerson")
                    xmlwriter.WriteStartElement("name")

                    xmlwriter.WriteEndElement() ''End name
                    xmlwriter.WriteEndElement() ''End assignedPerson

                    xmlwriter.WriteEndElement() ''End assignedEntity
                    xmlwriter.WriteEndElement() ''End performer

                    xmlwriter.WriteStartElement("participant")
                    xmlwriter.WriteAttributeString("typeCode", "DEV")

                    xmlwriter.WriteStartElement("participantRole")
                    xmlwriter.WriteAttributeString("classCode", "MANU")

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.52")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteEndElement() ''''''''id END

                    xmlwriter.WriteStartElement("addr")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("telecom")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("scopingEntity")
                    xmlwriter.WriteEndElement() ''''''''scopingEntity END

                    xmlwriter.WriteEndElement() ''''''''participantRole END
                    xmlwriter.WriteEndElement() ''''''''participant END
                    ''
                    xmlwriter.WriteStartElement("entryRelationship")
                    xmlwriter.WriteAttributeString("typeCode", "REFR")
                    xmlwriter.WriteStartElement("act")
                    xmlwriter.WriteAttributeString("classCode", "ACT")
                    xmlwriter.WriteAttributeString("moodCode", "EVN")

                    '------------templateId
                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.4")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() 'templateId element END

                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteEndElement()

                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("nullFlavor", "NA")
                    xmlwriter.WriteEndElement() ''End code

                    xmlwriter.WriteStartElement("text")
                    xmlwriter.WriteStartElement("reference")
                    xmlwriter.WriteAttributeString("value", "PtrToSectionText")
                    xmlwriter.WriteEndElement() ''End reference

                    xmlwriter.WriteEndElement() ''End text

                    xmlwriter.WriteStartElement("reference")
                    xmlwriter.WriteAttributeString("typeCode", "REFR")
                    xmlwriter.WriteStartElement("externalDocument")
                    xmlwriter.WriteAttributeString("classCode", "DOC")
                    xmlwriter.WriteAttributeString("moodCode", "EVN")

                    xmlwriter.WriteStartElement("id")
                    xmlwriter.WriteEndElement() ''End id

                    xmlwriter.WriteStartElement("text")
                    xmlwriter.WriteValue("Location of Documentation -  URL or other")
                    xmlwriter.WriteEndElement() ''End text
                    xmlwriter.WriteEndElement() ''End  externalDocument
                    xmlwriter.WriteEndElement() ''End reference
                    xmlwriter.WriteEndElement() ''End Act
                    xmlwriter.WriteEndElement() ''entryRelationship
                    ''

                    xmlwriter.WriteEndElement() '''''''''procedure entry

                    xmlwriter.WriteEndElement() '''''''''entry end
                Next
                ''''End Entry Procedures
                xmlwriter.WriteEndElement() '''''''section END
                xmlwriter.WriteEndElement() '''''''component END

            End If
            ''$$$$$$$$$$$$$  -- Component (PROCEDURES) ---END---$$$$$$$$$$$$$$$$$$


            ''$$$$$$$$$$$$$  -- Component (ENCOUNTER) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("Encounter") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")

                If Not IsNothing(mPatient.PatientEncounters) AndAlso mPatient.PatientEncounters.Count > 0 Then

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.127")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP/C83")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.1.5.3.3")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    xmlwriter.WriteStartElement("templateId")
                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.3")
                    xmlwriter.WriteAttributeString("assigningAuthorityName", "HL7 CCD")
                    xmlwriter.WriteEndElement() ''''''''templateId END

                    '------------code
                    xmlwriter.WriteStartElement("code")
                    xmlwriter.WriteAttributeString("code", "46240-8")
                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                    xmlwriter.WriteAttributeString("displayName", "History of encounters")
                    xmlwriter.WriteEndElement() 'code element END
                End If
                xmlwriter.WriteElementString("title", "Encounters")

                '-----------text
                xmlwriter.WriteStartElement("text")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Encounter")
                xmlwriter.WriteElementString("th", "Provider")
                xmlwriter.WriteElementString("th", "Location")
                xmlwriter.WriteElementString("th", "Date")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")
                ' Dim _oencounters As String = ""
                If Not IsNothing(mPatient.PatientEncounters) AndAlso mPatient.PatientEncounters.Count > 0 Then
                    For Each oEncounters As Encounters In mPatient.PatientEncounters
                        '  If _oencounters <> oEncounters.ExamName Then


                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteElementString("td", oEncounters.ExamName)
                        xmlwriter.WriteElementString("td", oEncounters.ProvLastName & " " & oEncounters.ProvFirstName)
                        xmlwriter.WriteElementString("td", oEncounters.Location)
                        xmlwriter.WriteElementString("td", oEncounters.DateOfService)
                        xmlwriter.WriteEndElement() 'tr element end
                        ' _oencounters = oEncounters.ExamName
                        ' End If
                    Next
                Else
                    xmlwriter.WriteStartElement("tr")
                    xmlwriter.WriteStartElement("td")
                    xmlwriter.WriteAttributeString("colspan", "4")
                    xmlwriter.WriteEndElement() 'td element end
                    xmlwriter.WriteEndElement() 'tr element end
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end


                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If Not IsNothing(mPatient.PatientEncounters) AndAlso mPatient.PatientEncounters.Count > 0 Then
                    For Each oEncounters As Encounters In mPatient.PatientEncounters
                        Dim _EncounterCode As String = ""
                        Dim _EncounterDescription As String = ""
                        dtCPT = ogloCCDDBLayer.GetEncounterCodeAndDescription(oEncounters.ExamID, mPatientID.ToString())
                        If Not IsNothing(dtCPT) AndAlso dtCPT.Rows.Count > 0 Then

                            For i As Integer = 0 To dtCPT.Rows.Count - 1
                                _EncounterCode = dtCPT.Rows(i)(0)
                                _EncounterDescription = dtCPT.Rows(i)(1)

                                '------------entry
                                xmlwriter.WriteStartElement("entry")
                                xmlwriter.WriteAttributeString("typeCode", "DRIV")

                                xmlwriter.WriteStartElement("encounter")
                                xmlwriter.WriteAttributeString("classCode", "ENC")
                                xmlwriter.WriteAttributeString("moodCode", "EVN")

                                xmlwriter.WriteStartElement("templateId")
                                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.83.16")
                                xmlwriter.WriteAttributeString("assigningAuthorityName", "HITSP C83")
                                xmlwriter.WriteEndElement() ''''''''templateId END

                                xmlwriter.WriteStartElement("templateId")
                                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.21")
                                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                                xmlwriter.WriteEndElement() ''''''''templateId END

                                xmlwriter.WriteStartElement("templateId")
                                xmlwriter.WriteAttributeString("root", "1.3.6.1.4.1.19376.1.5.3.1.4.14")
                                xmlwriter.WriteAttributeString("assigningAuthorityName", "IHE PCC")
                                xmlwriter.WriteEndElement() ''''''''templateId END

                                xmlwriter.WriteStartElement("id")
                                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
                                xmlwriter.WriteEndElement() ''''''''id END


                                xmlwriter.WriteStartElement("code")
                                If Not IsNothing(_EncounterCode) AndAlso _EncounterCode <> "" Then
                                    xmlwriter.WriteAttributeString("code", _EncounterCode)
                                End If

                                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.12")
                                If Not IsNothing(_EncounterDescription) AndAlso _EncounterDescription <> "" Then
                                    xmlwriter.WriteAttributeString("displayName", _EncounterDescription)
                                End If

                                xmlwriter.WriteAttributeString("codeSystemName", "CPT")
                                xmlwriter.WriteAttributeString("codeSystemVersion", "4")

                                xmlwriter.WriteStartElement("originalText")
                                If Not IsNothing(oEncounters.ExamName) AndAlso oEncounters.ExamName <> "" Then
                                    xmlwriter.WriteValue(oEncounters.ExamName)
                                End If

                                xmlwriter.WriteStartElement("reference")
                                xmlwriter.WriteAttributeString("value", "PointerToTextInSection")
                                xmlwriter.WriteEndElement()
                                xmlwriter.WriteEndElement()
                                '  End If

                                xmlwriter.WriteStartElement("translation")
                                xmlwriter.WriteAttributeString("code", "AMB")
                                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
                                xmlwriter.WriteAttributeString("displayName", "Ambulatory")
                                xmlwriter.WriteAttributeString("codeSystemName", "HL7 ActEncounterCode")
                                xmlwriter.WriteEndElement() ''translation
                                xmlwriter.WriteEndElement() ''code
                                'HL7 ActEncounterCode
                                'http://web.his.uvic.ca/Research/HTG/vocabulary/Terminologies/HL7/Browse.php?Tab=ValueSets&CodeSystem=2.16.840.1.113883.5.4


                                '' xmlwriter.WriteEndElement() ''''''''code END

                                xmlwriter.WriteStartElement("text")
                                xmlwriter.WriteValue("IHE Requires reference to go here instead of originalText of code.")
                                xmlwriter.WriteStartElement("reference")
                                xmlwriter.WriteEndElement()
                                xmlwriter.WriteEndElement()

                                If Not IsNothing(oEncounters.DateOfService) AndAlso oEncounters.DateOfService <> "" Then
                                    strDate = Format(CType(oEncounters.DateOfService, Date), "yyyyMMdd")
                                Else
                                    strDate = dtTodayDate
                                End If

                                xmlwriter.WriteStartElement("effectiveTime")
                                xmlwriter.WriteAttributeString("value", strDate)
                                xmlwriter.WriteEndElement() ''''''''effectiveTime END

                                xmlwriter.WriteStartElement("performer")
                                xmlwriter.WriteStartElement("assignedEntity")

                                xmlwriter.WriteStartElement("id")
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("addr")
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("telecom")
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("assignedPerson")
                                xmlwriter.WriteStartElement("name")
                                If Not IsNothing(oEncounters.ProvFirstName) AndAlso oEncounters.ProvFirstName <> "" Then
                                    If Not IsNothing(oEncounters.ProvLastName) AndAlso oEncounters.ProvLastName <> "" Then
                                        xmlwriter.WriteValue(oEncounters.ProvLastName & " " & oEncounters.ProvFirstName)
                                    Else
                                        xmlwriter.WriteValue(oEncounters.ProvFirstName)
                                    End If
                                End If
                                xmlwriter.WriteEndElement()
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteEndElement()
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("participant")
                                xmlwriter.WriteAttributeString("typeCode", "LOC")

                                xmlwriter.WriteStartElement("templateId")
                                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.45")
                                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
                                xmlwriter.WriteEndElement() ''''''''templateId END

                                xmlwriter.WriteStartElement("participantRole")
                                xmlwriter.WriteAttributeString("classCode", "SDLOC")

                                xmlwriter.WriteStartElement("id")
                                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
                                xmlwriter.WriteEndElement() ''''''''id END

                                xmlwriter.WriteStartElement("addr")
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("telecom")
                                xmlwriter.WriteEndElement()

                                xmlwriter.WriteStartElement("playingEntity")
                                xmlwriter.WriteAttributeString("classCode", "PLC")
                                xmlwriter.WriteElementString("name", mPatient.Author.Organization)
                                xmlwriter.WriteEndElement() ''''''''playingEntity END

                                xmlwriter.WriteEndElement() ''''''''participantRole END
                                xmlwriter.WriteEndElement() ''''''''participant END
                                xmlwriter.WriteEndElement() ''''''''End encounter
                                xmlwriter.WriteEndElement() ''''''''End entry
                            Next
                        End If
                    Next
                End If
                xmlwriter.WriteEndElement() '''''''section END
                xmlwriter.WriteEndElement() '''''''component END
            End If
            ''$$$$$$$$$$$$$  -- Component (ENCOUNTER) ---END---$$$$$$$$$$$$$$$$$$



            ''$$$$$$$$$$$$$  -- Component (ADVANCE DIRECTIVES) ---START---$$$$$$$$$$$$$$$$$$
            If CCDSection.Contains("AdvanceDirectives") = True Or CCDSection = "All" Then
                xmlwriter.WriteStartElement("component")
                xmlwriter.WriteStartElement("section")
                xmlwriter.WriteStartElement("code")
                xmlwriter.WriteAttributeString("code", "42348-3")
                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
                xmlwriter.WriteAttributeString("displayName", "Advance directives")
                xmlwriter.WriteEndElement() 'code element END
                '-----------title
                xmlwriter.WriteElementString("title", "Advance Directives")

                '-----------text
                xmlwriter.WriteStartElement("text")
                'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

                xmlwriter.WriteStartElement("table")
                xmlwriter.WriteAttributeString("border", "1")
                xmlwriter.WriteAttributeString("width", "100%")
                xmlwriter.WriteStartElement("thead")
                xmlwriter.WriteStartElement("tr")
                xmlwriter.WriteElementString("th", "Directive")
                xmlwriter.WriteElementString("th", "Pat Aware")
                xmlwriter.WriteElementString("th", "Third Party")
                xmlwriter.WriteElementString("th", "Verification")
                xmlwriter.WriteElementString("th", "Reviewed")
                xmlwriter.WriteEndElement() '''''''tr End
                xmlwriter.WriteEndElement() ''''''''thead End
                xmlwriter.WriteStartElement("tbody")

                If Not IsNothing(mPatient.Advancedirective) Then
                    If mPatient.Advancedirective.Count > 0 Then
                        For Each oAdvDirectives As Advancedirective In mPatient.Advancedirective
                            xmlwriter.WriteStartElement("tr")
                            xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveName)
                            xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectivePatAware)
                            xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveThirdParty)
                            xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveVerification)
                            xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveReviewed)
                            'xmlwriter.WriteEndElement() 'td element end
                            xmlwriter.WriteEndElement() 'tr element end
                        Next
                    Else
                        xmlwriter.WriteStartElement("tr")
                        xmlwriter.WriteStartElement("td")
                        xmlwriter.WriteAttributeString("colspan", "5")
                        xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement()
                    End If
                End If

                xmlwriter.WriteEndElement() 'Tbody element end
                xmlwriter.WriteEndElement() 'Table element end

                xmlwriter.WriteEndElement() 'text element END
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                xmlwriter.WriteEndElement() 'section End``
                xmlwriter.WriteEndElement() 'component End
            End If
            ''$$$$$$$$$$$$$  -- Component (ADVANCE DIRECTIVES) ---END---$$$$$$$$$$$$$$$$$$


            xmlwriter.WriteEndElement() '------------structuredBody Element Ends here
            xmlwriter.WriteEndElement() '############Parent component Element Ends here

            xmlwriter.WriteEndElement() 'End Clinical Document Element
            xmlwriter.WriteEndDocument()
            xmlwriter.Close()
            Return strfilepath
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)

        Finally
            'Memory Leak
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If


            If Not IsNothing(dtCPT) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If Not IsNothing(ogloCCDDBLayer) Then
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            strDate = Nothing
            If Not IsNothing(_dtClinicInfo) Then
                _dtClinicInfo.Dispose()
                _dtClinicInfo = Nothing
            End If

            _clinicName = Nothing
            _clinicAddr = Nothing
            _clinicCity = Nothing
            _clinicState = Nothing
            _cliniPostalCode = Nothing
            _clinicCountry = Nothing
            _clinicCounty = Nothing
            _clinicPhone = Nothing

            xmlwriter = Nothing

        End Try

    End Function



    Public Function ValidateTags(ByVal tagName As String) As Boolean
        Dim areader As XmlReader
        Dim oXMLSettings As New Xml.XmlReaderSettings()
        Dim strnodevalue As String = ""
        Try

            areader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
            While areader.Read
                If areader.NodeType = XmlNodeType.Element Then
                    strnodevalue = areader.Name
                    If strnodevalue = tagName Then
                        Return True
                    End If
                End If
            End While
            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If
            Return False
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            'Memory Leak
            If Not IsNothing(oXMLSettings) Then
                oXMLSettings = Nothing
            End If
            strnodevalue = Nothing
        End Try
        
    End Function
    Private Function InsertintogloEMR() As Boolean
        Try


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)

        End Try
        Return Nothing
    End Function

    Private Function ReadPatientInformationSource(ByVal areader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderchild1 As XmlReader
        Dim objMessagelogdetails As CCDMessageLogDetail = Nothing

        Try
            objMessagelogdetails = New CCDMessageLogDetail
            If Not IsNothing(areader) Then

                While areader.Read
                    If areader.NodeType = XmlNodeType.Element Then
                        Select Case areader.Name
                            Case "time"
                                mPatient.Author.InformationTime = DateFromHL7(areader.GetAttribute("value"))
                            Case "assignedAuthor"
                                areaderchild = areader.ReadSubtree
                                While areaderchild.Read
                                    If areaderchild.NodeType = XmlNodeType.Element Then
                                        Select Case areaderchild.Name
                                            Case "id" 'R
                                                mPatient.Author.DocumentId = areaderchild.GetAttribute("root")
                                            Case "given" 'R
                                                mPatient.Author.PersonName.FirstName = areaderchild.ReadInnerXml
                                                If mPatient.Author.PersonName.FirstName = "" Then
                                                    strMessageLog.Append("AuthorInfo Patient: given (FirstName) value not present ")
                                                End If
                                            Case "prefix"
                                                mPatient.Author.PersonName.Prefix = areaderchild.ReadInnerXml
                                            Case "suffix"
                                                mPatient.Author.PersonName.Suffix = areaderchild.ReadInnerXml
                                            Case "family" 'R
                                                mPatient.Author.PersonName.LastName = areaderchild.ReadInnerXml
                                                If mPatient.Author.PersonName.LastName = "" Then
                                                    strMessageLog.Append("AuthorInfo Patient: given (LastName) value not present ")
                                                End If
                                            Case "representedOrganization"
                                                areaderchild1 = areaderchild.ReadSubtree
                                                While areaderchild1.Read
                                                    If areaderchild1.NodeType = XmlNodeType.Element Then
                                                        Select Case areaderchild1.Name
                                                            Case "name"
                                                                mPatient.Author.Organization = areaderchild1.ReadInnerXml

                                                        End Select
                                                    End If
                                                End While
                                        End Select
                                    End If
                                End While
                        End Select
                    End If
                End While
            End If
            If Not IsNothing(strMessageLog) Then
                objMessagelogdetails.Description = strMessageLog.ToString
                objMessagelogdetails.Datetime = Now.Date

                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            'Memory Leak
            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If
            areaderchild = Nothing
            areaderchild1 = Nothing
        End Try
    End Function


 
    'Code Changes done by kanchan on 20100712 for Modular CCD Rendering & save
    'Change whole function code as per standard & to save correct & proper data tagwise
    Private Function ReadPatientDetails(ByVal areader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderchild1 As XmlReader
        Dim areaderguardian As XmlReader
        Dim oLanguage As Language
        Dim olanguagecol As New LanguageCol
        Dim objMessagelogdetails As CCDMessageLogDetail = Nothing
        Try
            objMessagelogdetails = New CCDMessageLogDetail

            If Not IsNothing(areader) Then
                mPatient.PatientLanguages = olanguagecol
                While areader.Read
                    If areader.NodeType = XmlNodeType.Element Then
                        Select Case areader.Name
                            Case "recordTarget"
                                areaderchild = areader.ReadSubtree

                                While areaderchild.Read
                                    If areaderchild.NodeType = XmlNodeType.Element Then
                                        Select Case areaderchild.Name
                                            Case "id" 'R
                                                mPatient.PatientName.Code = areaderchild.GetAttribute("extension")

                                            Case "streetAddressLine" 'O
                                                If mPatient.PatientName.PersonContactAddress.Street = "" Then
                                                    mPatient.PatientName.PersonContactAddress.Street = areaderchild.ReadInnerXml
                                                Else
                                                    mPatient.PatientName.PersonContactAddress.AddressLine2 = areaderchild.ReadInnerXml
                                                End If

                                            Case "city" 'O
                                                mPatient.PatientName.PersonContactAddress.City = areaderchild.ReadInnerXml
                                            Case "state" 'O
                                                mPatient.PatientName.PersonContactAddress.State = areaderchild.ReadInnerXml
                                            Case "postalCode" 'O
                                                mPatient.PatientName.PersonContactAddress.Zip = areaderchild.ReadInnerXml
                                            Case "country" 'O
                                                mPatient.PatientName.PersonContactAddress.Country = areaderchild.ReadInnerXml

                                            Case "telecom"
                                                'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
                                                'Format is like this- tel:+1(354) 787-7870'
                                                Select Case areaderchild.GetAttribute("use")
                                                    Case "HP"
                                                        'mPatient.PatientName.PersonContactPhone.Phone = areaderchild.GetAttribute("value")
                                                        Dim _phone As String = areaderchild.GetAttribute("value")
                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                        mPatient.PatientName.PersonContactPhone.Phone = _phone
                                                    Case "MC"
                                                        'mPatient.PatientName.PersonContactPhone.Mobile = areaderchild.GetAttribute("value")
                                                        Dim _phone As String = areaderchild.GetAttribute("value")
                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                        mPatient.PatientName.PersonContactPhone.Mobile = _phone
                                                    Case "WP"
                                                        'mPatient.PatientName.PersonContactPhone.WorkPhone = areaderchild.GetAttribute("value")
                                                        Dim _phone As String = areaderchild.GetAttribute("value")
                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                        mPatient.PatientName.PersonContactPhone.WorkPhone = _phone
                                                    Case "HV"
                                                        'mPatient.PatientName.PersonContactPhone.VacationPhone = areaderchild.GetAttribute("value")
                                                        Dim _phone As String = areaderchild.GetAttribute("value")
                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                        mPatient.PatientName.PersonContactPhone.VacationPhone = _phone
                                                    Case Else
                                                        If mPatient.PatientName.PersonContactPhone.Email <> "" Then
                                                            mPatient.PatientName.PersonContactPhone.URL = areaderchild.GetAttribute("value")
                                                        Else
                                                            mPatient.PatientName.PersonContactPhone.Email = areaderchild.GetAttribute("value")
                                                            If mPatient.PatientName.PersonContactPhone.Email.Contains("@") = False Then
                                                                mPatient.PatientName.PersonContactPhone.Email = ""
                                                            End If
                                                        End If
                                                End Select
                                                'Code End-Added by kanchan on 20100709 for Modular CCD Rendering & save
                                                'Case "country"
                                                '    mPatient.PatientName.PersonContactAddress.Country = areaderchild.GetAttribute("displayName")

                                            Case "prefix" 'O
                                                mPatient.PatientName.Prefix = areaderchild.ReadInnerXml
                                            Case "suffix" 'O
                                                mPatient.PatientName.Suffix = areaderchild.ReadInnerXml

                                            Case "guardian"
                                                'Code Changes done by kanchan on 20100712 for Modular CCD Rendering & save

                                                'Code Start-added by kanchan on 20100604 to avoid guardian details
                                                'areaderchild.Skip()
                                                areaderguardian = areaderchild.ReadSubtree
                                                While areaderguardian.Read
                                                    If areaderguardian.NodeType = XmlNodeType.Element Then
                                                        Select Case areaderguardian.Name
                                                            '<code code="BRO" displayName="Brother" 
                                                            Case "code"
                                                                mPatient.Guardian_Relation = areaderguardian.GetAttribute("displayName")
                                                            Case "streetAddressLine" 'O
                                                                If mPatient.Guardian_Address1 = "" Then
                                                                    mPatient.Guardian_Address1 = areaderguardian.ReadInnerXml
                                                                Else
                                                                    mPatient.Guardian_Address2 = areaderguardian.ReadInnerXml
                                                                End If

                                                            Case "city" 'O
                                                                mPatient.Guardian_City = areaderguardian.ReadInnerXml
                                                            Case "state" 'O
                                                                mPatient.Guardian_State = areaderguardian.ReadInnerXml
                                                            Case "postalCode" 'O
                                                                mPatient.Guardian_ZIP = areaderguardian.ReadInnerXml
                                                            Case "country" 'O
                                                                mPatient.Guardian_Country = areaderguardian.ReadInnerXml
                                                            Case "telecom"
                                                                Select Case areaderguardian.GetAttribute("use")
                                                                    Case "HP"
                                                                        Dim _phone As String = areaderguardian.GetAttribute("value")
                                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                                        mPatient.Guardian_Phone = _phone
                                                                    Case "MC"
                                                                        Dim _phone As String = areaderguardian.GetAttribute("value")
                                                                        _phone = _phone.Replace("tel:+1", "").Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "")
                                                                        mPatient.Guardian_Mobile = _phone
                                                                    Case Else
                                                                        If mPatient.Guardian_Email <> "" Then
                                                                            mPatient.Guardian_Email = areaderguardian.GetAttribute("value")
                                                                        End If
                                                                        If mPatient.Guardian_Email.Contains("@") = False Then
                                                                            mPatient.Guardian_Email = ""
                                                                        End If
                                                                End Select
                                                            Case "name"
                                                                'Dim temp As String = areaderchild.ReadToNextSibling("ethnicGroupCode")
                                                                Dim areaderchild2 As XmlReader
                                                                areaderchild2 = areaderchild.ReadSubtree
                                                                While areaderchild2.Read
                                                                    If areaderchild2.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild2.Name = "given" Then
                                                                            Dim _Qualifier As String = areaderchild2.GetAttribute("qualifier")
                                                                            If _Qualifier <> "" Then
                                                                                Select Case _Qualifier.ToUpper()
                                                                                    Case "CL"
                                                                                        If mPatient.Guardian_fName = "" Then
                                                                                            mPatient.Guardian_fName = areaderchild2.ReadInnerXml
                                                                                        End If
                                                                                    Case "BR"
                                                                                        mPatient.Guardian_mName = areaderchild2.ReadInnerXml
                                                                                End Select
                                                                            Else
                                                                                If mPatient.Guardian_fName = "" Then
                                                                                    mPatient.Guardian_fName = areaderchild2.ReadInnerXml
                                                                                Else
                                                                                    mPatient.Guardian_mName = areaderchild2.ReadInnerXml
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If areaderchild2.Name = "family" Then
                                                                            mPatient.Guardian_lName = areaderchild2.ReadInnerXml
                                                                        End If
                                                                    End If
                                                                End While
                                                        End Select
                                                    End If
                                                End While
                                            Case "name"
                                                'Dim temp As String = areaderchild.ReadToNextSibling("ethnicGroupCode")
                                                areaderchild1 = areaderchild.ReadSubtree
                                                While areaderchild1.Read
                                                    If areaderchild1.NodeType = XmlNodeType.Element Then
                                                        If areaderchild1.Name = "given" Then
                                                            Dim _Qualifier As String = areaderchild1.GetAttribute("qualifier")
                                                            If _Qualifier <> "" Then
                                                                _Qualifier = _Qualifier.ToUpper()
                                                            End If
                                                            Select Case _Qualifier
                                                                Case "CL"
                                                                    If mPatient.PatientName.FirstName = "" Then
                                                                        mPatient.PatientName.FirstName = areaderchild1.ReadInnerXml
                                                                        If mPatient.PatientName.FirstName = "" Then
                                                                            strMessageLog.Append("Patient: given (FirstName) value not present ")
                                                                        End If
                                                                    End If
                                                                Case "BR"
                                                                    mPatient.PatientName.MiddleName = areaderchild1.ReadInnerXml
                                                                Case Else
                                                                    If mPatient.PatientName.FirstName = "" Then
                                                                        mPatient.PatientName.FirstName = areaderchild1.ReadInnerXml
                                                                        If mPatient.PatientName.FirstName = "" Then
                                                                            strMessageLog.Append("Patient: given (FirstName) value not present ")
                                                                        End If
                                                                    Else
                                                                        mPatient.PatientName.MiddleName = areaderchild1.ReadInnerXml
                                                                    End If
                                                            End Select
                                                        End If
                                                        If areaderchild1.Name = "family" Then
                                                            mPatient.PatientName.LastName = areaderchild1.ReadInnerXml
                                                            If mPatient.PatientName.LastName = "" Then
                                                                strMessageLog.Append("Patient: Family (LastName) value not present ")
                                                            End If
                                                        End If

                                                    End If
                                                End While
                                                'Case "given" 'Required for CCHIT inspection
                                                '    mPatient.PatientName.FirstName = areaderchild.ReadInnerXml
                                                '    If mPatient.PatientName.FirstName = "" Then
                                                '        strMessageLog.Append("Patient: given (FirstName) value not present ")
                                                '    End If
                                                'Case "family" 'R
                                                '    mPatient.PatientName.LastName = areaderchild.ReadInnerXml
                                                '    If mPatient.PatientName.LastName = "" Then
                                                '        strMessageLog.Append("Patient: Family (LastName) value not present ")
                                                '    End If
                                            Case "administrativeGenderCode" 'R
                                                mPatient.Gender = areaderchild.GetAttribute("code")
                                                'Code Start-added by kanchan on 20100604 
                                                If mPatient.Gender <> "" Then
                                                    If mPatient.Gender.Trim.ToUpper() = "F" Or mPatient.Gender.Trim.ToUpper() = "FEMALE" Then
                                                        mPatient.Gender = "Female"
                                                    ElseIf mPatient.Gender.Trim.ToUpper() = "M" Or mPatient.Gender.Trim.ToUpper() = "MALE" Then
                                                        mPatient.Gender = "Male"
                                                    Else
                                                        mPatient.Gender = "Other"
                                                    End If
                                                    'Code End-added by kanchan on 20100604 
                                                Else
                                                    strMessageLog.Append("Patient: Gender value not present ")
                                                End If
                                            Case "birthTime" 'R
                                                mPatient.DateofBirth = areaderchild.GetAttribute("value")
                                                mPatient.DateofBirth = DateFromHL7(mPatient.DateofBirth)
                                                If mPatient.DateofBirth = "" Then
                                                    strMessageLog.Append("Patient: DateofBirth value not present ")
                                                End If
                                            Case "maritalStatusCode" '
                                                'Code Start-Added by kanchan on 20100708 for Modular CCD Rendering & save
                                                mPatient.MaritalStatus = areaderchild.GetAttribute("code")
                                                If mPatient.MaritalStatus <> "" Then
                                                    Select Case mPatient.MaritalStatus.ToUpper
                                                        Case "W"
                                                            mPatient.MaritalStatus = "Widowed"
                                                        Case "S"
                                                            mPatient.MaritalStatus = "UnMarried"
                                                        Case "M"
                                                            mPatient.MaritalStatus = "Married"
                                                        Case "L"
                                                            mPatient.MaritalStatus = "Single"
                                                        Case "D"
                                                            mPatient.MaritalStatus = "Divorced"
                                                        Case Else
                                                            mPatient.MaritalStatus = ""
                                                    End Select
                                                Else
                                                    mPatient.MaritalStatus = ""
                                                End If
                                                'Code end-Added by kanchan on 20100708 for Modular CCD Rendering & save
                                            Case "religiousAffiliationCode"
                                                mPatient.ReligiousAffiliationCode = areaderchild.GetAttribute("displayName")
                                            Case "ethnicGroupCode"
                                                mPatient.ethnicGroupCode = areaderchild.GetAttribute("displayName")
                                            Case "raceCode"
                                                'Code Start-Added by kanchan on 20100709 for Modular CCD Rendering & save
                                                'mPatient.RaceCode = areaderchild.GetAttribute("displayName")
                                                mPatient.Race = areaderchild.GetAttribute("displayName")

                                            Case "languageCommunication"
                                                areaderchild1 = areaderchild.ReadSubtree
                                                oLanguage = New Language

                                                mPatient.PatientLanguages.Add(oLanguage)

                                                While areaderchild1.Read
                                                    If areaderchild1.NodeType = XmlNodeType.Element Then
                                                        Select Case areader.Name
                                                            Case "languageCode"

                                                                Dim ogloccdDBlayer As New gloCCDDatabaseLayer


                                                                '''''code to retrieve the language against the language code returned from the CCD file
                                                                Dim _code As String = areaderchild1.GetAttribute("code")
                                                                Dim _languageCode As String = ""
                                                                Dim _countryCode As String = ""
                                                                Dim LangCode As String() = Nothing

                                                                LangCode = _code.Split("-")
                                                                If Not IsNothing(LangCode) Then
                                                                    If LangCode.Length > 0 Then
                                                                        _languageCode = LangCode(0)
                                                                        _countryCode = LangCode(1)
                                                                    End If
                                                                End If
                                                                Dim patLanguage As String = ogloccdDBlayer.getDescription(_languageCode, "Languages")
                                                                Dim patCountry As String = ogloccdDBlayer.getDescription(_countryCode, "countries")

                                                                oLanguage.Language = patLanguage
                                                                'oLanguage.Country = patCountry
                                                                oLanguage.Country = _countryCode


                                                            Case "modeCode"
                                                                oLanguage.Mode = areaderchild1.GetAttribute("displayName")

                                                            Case "preferenceInd"
                                                                oLanguage.Preferred = areaderchild1.GetAttribute("value")
                                                        End Select
                                                    End If

                                                End While
                                        End Select
                                    End If
                                End While
                        End Select
                    End If
                End While
            End If
            If Not IsNothing(strMessageLog) Then
                If strMessageLog.Length > 0 Then
                    objMessagelogdetails.Description = strMessageLog.ToString
                    objMessagelogdetails.Datetime = Now.Date

                    Return False
                End If

                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            'Memory Leak
            If Not IsNothing(olanguagecol) Then
                olanguagecol.Dispose()
                olanguagecol = Nothing
            End If
            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If
            areaderchild = Nothing
            areaderchild1 = Nothing
            areaderguardian = Nothing
            oLanguage = Nothing
        End Try
    End Function

    Private Function ReadComponent(ByVal areader As XmlReader) As Boolean
        Dim TemplateId As String = ""
        Try

            If Not IsNothing(areader) Then

                While areader.Read
                    If areader.NodeType = XmlNodeType.Element Then

                        Select Case areader.Name

                            'identify this section
                            Case "templateId"
                                'is it payors
                                TemplateId = areader.GetAttribute("root")
                                Select Case TemplateId
                                    'Advance directives section
                                    Case "2.16.840.1.113883.10.20.1.1"
                                        'ReadAdvanceDirective(areader)

                                        'Alerts section
                                    Case "2.16.840.1.113883.10.20.1.2"
                                        ReadAllergies(areader)
                                        'Encounters section
                                    Case "2.16.840.1.113883.10.20.1.3"
                                        ReadEncounters(areader)
                                        'Family history section
                                    Case "2.16.840.1.113883.10.20.1.4"


                                        'Functional status section
                                    Case "2.16.840.1.113883.10.20.1.5"



                                        'Immunizations section
                                    Case "2.16.840.1.113883.10.20.1.6"
                                        ReadImmunizations(areader)
                                        'Medical equipment section
                                    Case "2.16.840.1.113883.10.20.1.7"

                                        'Medications section
                                    Case "2.16.840.1.113883.10.20.1.8"

                                        ReadMedication(areader)
                                        'Payers section
                                    Case "2.16.840.1.113883.10.20.1.9"
                                        'ReadInsurance(areader)

                                        'Plan of care section
                                    Case "2.16.840.1.113883.10.20.1.10"


                                        'Problem section
                                    Case "2.16.840.1.113883.10.20.1.11"
                                        ReadConditions(areader)

                                        'Procedures section
                                    Case "2.16.840.1.113883.10.20.1.12"

                                        'Purpose section
                                    Case "2.16.840.1.113883.10.20.1.13"

                                        'Results section
                                    Case "2.16.840.1.113883.10.20.1.14"
                                        ReadResults(areader)
                                        'Social history section
                                    Case "2.16.840.1.113883.10.20.1.15"

                                        'Vital signs section
                                    Case "2.16.840.1.113883.10.20.1.16"
                                        ReadVitals(areader)
                                End Select
                        End Select
                    End If

                End While
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        Finally
            TemplateId = Nothing
        End Try
        Return Nothing
    End Function

    Private Function ValidateSection(ByVal areader As XmlReader) As Boolean
        Try



            'If Not IsNothing(areader) Then
            '    While areader.Read
            '        If areader.NodeType = XmlNodeType.Element Then
            '            areader.ReadStartElement("templateId")
            '            Dim Code As String = areader.ReadElementString("code")
            '            Dim CodeSystem As String = areader.GetAttribute("codeSystem")

            '            Select Case areader.Name
            '                ' check if all the three tags i.e. templateId, code, codeSystem are there in that section


            '            End Select
            '        End If
            '    End While
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
        Return Nothing
    End Function

    Private Function ReadImmunizations(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader = Nothing

        Dim areaderchild1 As XmlReader = Nothing
        Dim oImmunizations As ImmunizationCol = Nothing
        Dim oImmunization As Immunization = Nothing
        Try
            oImmunizations = New ImmunizationCol
            mPatient.PatientImmunizations = oImmunizations
            While xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "substanceAdministration"
                            areaderchild1 = xreader.ReadSubtree
                            oImmunization = New Immunization
                            mPatient.PatientImmunizations.Add(oImmunization)
                            While areaderchild1.Read
                                If areaderchild1.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild1.Name
                                        Case "consumable"
                                            areaderchild = areaderchild1.ReadSubtree
                                            'Loop through the nodes
                                            While areaderchild.Read
                                                If areaderchild.NodeType = XmlNodeType.Element Then
                                                    Select Case areaderchild.Name

                                                        Case "code"
                                                            oImmunization.VaccineName = areaderchild.GetAttribute("displayName")
                                                        Case "lotNumberText"
                                                            oImmunization.LotNumber = areaderchild.ReadInnerXml

                                                    End Select
                                                End If
                                            End While
                                        Case "center"
                                            oImmunization.ImmunizationDate = areaderchild1.GetAttribute("value")
                                            oImmunization.ImmunizationDate = DateFromHL7(oImmunization.ImmunizationDate)
                                    End Select
                                End If
                            End While


                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oImmunizations) Then
                oImmunizations.Dispose()
                oImmunizations = Nothing
            End If

            If Not IsNothing(oImmunization) Then
                oImmunization.Dispose()
                oImmunization = Nothing
            End If
            areaderchild = Nothing
            areaderchild1 = Nothing
        End Try
        Return Nothing
    End Function
    Public Shared Function DateFromHL7(ByVal strIN As String) As Date
        Dim _Date As Date = Now
        Try
            'Dim ret As String = strIN
            'DateString = ""
            'If Len(strIN) < 8 Then Exit Function
            'ret = Right$(ret, 2) & " " & MonthName(CLng(Mid$(ret, 5, 2)), True) & " " & Left$(ret, 4)
            'DateString = Format(CDate(ret), "MM/dd/yyyy")

            '// REMARK - We need to convert date as per various date time stampe mthod

            If Len(strIN) < 8 Then
                DateFromHL7 = Nothing
                Exit Function
            End If

            Dim _DateAsString As String = Mid(strIN, 1, 8)

            '1. yyyymmdd
            _Date = DateSerial(Mid(_DateAsString, 1, 4), Val(Mid(_DateAsString, 5, 2)), Val(Mid(_DateAsString, 7, 2)))
            If IsDate(_Date) = False Then
                _Date = "12:00:00 AM"
            End If
            _DateAsString = Nothing
            Return _Date
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        End Try
    End Function

    Public Shared Function GetFDAHL7ForRouteCode(RoueTable As DataTable) As DataTable
        Try
            Using cn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
                Using cmd As New SqlCommand("gsp_GetFdaHl7CodeForRoute", cn)
                    cmd.CommandType = System.Data.CommandType.StoredProcedure
                    cmd.Parameters.Add(New SqlParameter("@TVP_RouteCodes", RoueTable))

                    Using da As New SqlDataAdapter(cmd)
                        Using dt As DataTable = New System.Data.DataTable()
                            da.Fill(dt)
                            Return dt
                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception
        End Try
        Return Nothing
    End Function

    Private Function ReadPatientSupport(ByVal xreader As XmlReader) As Boolean
        Try
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name

                        Case "prefix"
                            mPatient.PatientSupport.PersonName.Prefix = xreader.ReadInnerXml
                        Case "suffix"
                            mPatient.PatientSupport.PersonName.Suffix = xreader.ReadInnerXml

                        Case "given"
                            mPatient.PatientSupport.PersonName.FirstName = xreader.ReadInnerXml
                        Case "family"
                            mPatient.PatientSupport.PersonName.LastName = xreader.ReadInnerXml
                        Case "low"
                            mPatient.PatientSupport.StartSupport = DateFromHL7(xreader.GetAttribute("value"))
                        Case "high"
                            mPatient.PatientSupport.EndSupport = DateFromHL7(xreader.GetAttribute("value"))
                        Case "associatedEntity"
                            mPatient.PatientSupport.Contacttype = xreader.GetAttribute("classCode")
                        Case "code"
                            mPatient.PatientSupport.RelationShip = xreader.GetAttribute("displayName")
                        Case "streetAddressLine"
                            mPatient.PatientSupport.PersonContactAddress.Street = xreader.ReadInnerXml
                        Case "city"
                            mPatient.PatientSupport.PersonContactAddress.City = xreader.ReadInnerXml
                        Case "state"
                            mPatient.PatientSupport.PersonContactAddress.State = xreader.ReadInnerXml
                        Case "postalCode"
                            mPatient.PatientSupport.PersonContactAddress.Zip = xreader.ReadInnerXml
                        Case "country"
                            mPatient.PatientSupport.PersonContactAddress.Country = xreader.ReadInnerXml
                        Case "telecom"
                            Select Case xreader.GetAttribute("use")
                                Case "MC"
                                    mPatient.PatientSupport.PersonContactPhone.Mobile = xreader.GetAttribute("value")
                                Case "HP"
                                    mPatient.PatientSupport.PersonContactPhone.Phone = xreader.GetAttribute("value")
                                Case "WP"
                                    mPatient.PatientSupport.PersonContactPhone.WorkPhone = xreader.GetAttribute("value")
                                Case "HV"
                                    mPatient.PatientSupport.PersonContactPhone.VacationPhone = xreader.GetAttribute("value")
                                Case Else
                                    If mPatient.PatientSupport.PersonContactPhone.Email <> "" Then
                                        mPatient.PatientSupport.PersonContactPhone.URL = xreader.GetAttribute("value")
                                    Else
                                        mPatient.PatientSupport.PersonContactPhone.Email = xreader.GetAttribute("value")
                                    End If
                            End Select
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
        Return Nothing
    End Function

    Private Function ReadPatientProviders(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderchild1 As XmlReader
        Dim oPatientProviders As New ProviderCol
        Dim oPatientProvider As PatientProvider = Nothing
        strMessageLog = New System.Text.StringBuilder
        Dim objMessagelogdetails As CCDMessageLogDetail
        objMessagelogdetails = New CCDMessageLogDetail

        Try

            While xreader.Read
                mPatient.PatientProviders = oPatientProviders
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "performer"
                            areaderchild = xreader.ReadSubtree
                            'Loop through the nodes
                            oPatientProvider = New PatientProvider
                            mPatient.PatientProviders.Add(oPatientProvider)
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name

                                        Case "functionCode"
                                            oPatientProvider.ProviderRole = areaderchild.GetAttribute("displayName")
                                        Case "prefix"
                                            oPatientProvider.Prefix = areaderchild.ReadInnerXml
                                        Case "suffix"
                                            oPatientProvider.Suffix = areaderchild.ReadInnerXml
                                        Case "originalText"
                                            oPatientProvider.RoleDescription = areaderchild.ReadInnerXml
                                        Case "given" 'R
                                            oPatientProvider.FirstName = areaderchild.ReadInnerXml
                                            If oPatientProvider.FirstName = "" Then
                                                strMessageLog.Append(" PatientProvider: given (FirstName) value not present ")
                                            End If
                                        Case "family" 'R
                                            oPatientProvider.LastName = areaderchild.ReadInnerXml
                                            If oPatientProvider.LastName = "" Then
                                                strMessageLog.Append("PatientProvider: Family (LastName) value not present ")
                                            End If
                                        Case "low" 'R
                                            oPatientProvider.StartServiceDate = DateFromHL7(areaderchild.GetAttribute("value"))
                                            If oPatientProvider.StartServiceDate = "" Then
                                                strMessageLog.Append("PatientProvider: Low (StartServieDate) value not present ")
                                            End If
                                        Case "high" 'R
                                            oPatientProvider.EndServiceDate = DateFromHL7(areaderchild.GetAttribute("value"))
                                            If oPatientProvider.StartServiceDate = "" Then
                                                strMessageLog.Append("PatientProvider: High (EndServiceDate) value not present ")
                                            End If
                                        Case "code"
                                            oPatientProvider.ProviderType = areaderchild.GetAttribute("displayName")
                                        Case "streetAddressLine"
                                            oPatientProvider.StreetAddress = areaderchild.ReadInnerXml
                                        Case "city"
                                            oPatientProvider.City = areaderchild.ReadInnerXml
                                        Case "state"
                                            oPatientProvider.State = areaderchild.ReadInnerXml
                                        Case "postalCode"
                                            oPatientProvider.zip = areaderchild.ReadInnerXml
                                        Case "country"
                                            oPatientProvider.Country = areaderchild.ReadInnerXml
                                        Case "telecom"
                                            Select Case areaderchild.GetAttribute("use")

                                                Case "MC"
                                                    oPatientProvider.MobilePhone = areaderchild.GetAttribute("value")
                                                Case "HP"
                                                    oPatientProvider.HomePhone = areaderchild.GetAttribute("value")
                                                Case "WP"
                                                    oPatientProvider.WorkPhone = areaderchild.GetAttribute("value")
                                                Case "HV"
                                                    oPatientProvider.VacationPhone = areaderchild.GetAttribute("value")
                                                Case Else
                                                    If oPatientProvider.Email <> "" Then
                                                        oPatientProvider.URL = areaderchild.GetAttribute("value")
                                                    Else
                                                        oPatientProvider.Email = areaderchild.GetAttribute("value")
                                                    End If
                                            End Select
                                        Case "representedOrganization"
                                            areaderchild1 = areaderchild.ReadSubtree
                                            While areaderchild1.Read

                                                If areaderchild1.NodeType = XmlNodeType.Element Then
                                                    Select Case areaderchild1.Name
                                                        Case "name"
                                                            oPatientProvider.Organization = areaderchild1.ReadInnerXml
                                                    End Select

                                                End If
                                            End While
                                        Case "patient"
                                            areaderchild1 = areaderchild.ReadSubtree
                                            While areaderchild1.Read

                                                If areaderchild1.NodeType = XmlNodeType.Element Then
                                                    Select Case areaderchild1.Name
                                                        Case "id"
                                                            oPatientProvider.PatientIdentifier = areaderchild1.GetAttribute("root")
                                                    End Select

                                                End If
                                            End While
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While
            If Not IsNothing(strMessageLog) Then
                If strMessageLog.Length > 0 Then
                    objMessagelogdetails.Description = strMessageLog.ToString
                    objMessagelogdetails.Datetime = Now.Date

                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oPatientProvider) Then
                oPatientProvider.Dispose()
                oPatientProvider = Nothing
            End If
            If Not IsNothing(oPatientProviders) Then
                oPatientProviders.Dispose()
                oPatientProviders = Nothing
            End If

            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If
            areaderchild = Nothing
            areaderchild1 = Nothing
        End Try
    End Function


    Private Function ReadVitals(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim oVitals As VitalsCol = Nothing
        Dim oVital As Vitals = Nothing
        '\\addedby suraj on 20081120- for validating (Collecting) mandatory fields
        strMessageLog = New System.Text.StringBuilder
        Dim objMessagelogdetails As CCDMessageLogDetail
        objMessagelogdetails = New CCDMessageLogDetail

        Try
            oVitals = New VitalsCol
            mPatient.PatientVitals = oVitals
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name

                        Case "entry"
                            areaderchild = xreader.ReadSubtree
                            'Dim odoc As New XmlDocument()
                            'odoc.Load(areaderchild)

                            'Retrieve the Condition Value
                            'm_node = odoc.SelectSingleNode("/text/reference")
                            'mPatient.PatientConditions.ConditionType = m_node.Value
                            'Loop through the nodes
                            oVital = New Vitals
                            mPatient.PatientVitals.Add(oVital)
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "id"
                                            oVital.ResultID = areaderchild.GetAttribute("root")
                                            If oVital.ResultID = "" Then
                                                strMessageLog.Append(" Vital Sign: id->root value not present ")
                                            End If
                                        Case "code"
                                            oVital.ResultCode = areaderchild.GetAttribute("code")
                                            If oVital.ResultCode = "" Then
                                                strMessageLog.Append(" Vital Sign: Entry->code->code value not present ")
                                            End If
                                            oVital.CodeSystem = areaderchild.GetAttribute("codeSystemName")
                                            If oVital.CodeSystem = "" Then
                                                strMessageLog.Append(" Vital Sign: entry->Code->Codesystem not preset ")
                                            End If
                                            oVital.ResultCodeDisplay = areaderchild.GetAttribute("displayName")
                                            If oVital.ResultCodeDisplay = "" Then
                                                strMessageLog.Append(" Vital Sign: entry->Code->ResultCodeDisplay not preset ")
                                            End If
                                        Case "statusCode"
                                            oVital.StatusCode = areaderchild.GetAttribute("code")
                                            If oVital.CodeSystem = "" Then
                                                strMessageLog.Append(" Vital Sign: Status code->Code->Codesystem not preset ")
                                            End If
                                        Case "effectiveTime"
                                            oVital.ResultDate = DateFromHL7(areaderchild.GetAttribute("value"))

                                        Case "value"
                                            oVital.ResultUnit = areaderchild.GetAttribute("unit")
                                            oVital.ResultValue = areaderchild.GetAttribute("value")
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While

            If Not IsNothing(strMessageLog) Then
                objMessagelogdetails.Description = strMessageLog.ToString
                objMessagelogdetails.Datetime = Now.Date

                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oVitals) Then
                oVitals.Dispose()
                oVitals = Nothing
            End If
            If Not IsNothing(oVital) Then
                oVital.Dispose()
                oVital = Nothing
            End If
            areaderchild = Nothing
            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If
        End Try
    End Function
    Private Function ReadResults(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim oResults As ResultsCol = Nothing
        Dim oResult As Results = Nothing
        Try
            oResults = New ResultsCol
            mPatient.PatientResults = oResults
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name

                        Case "entry"
                            areaderchild = xreader.ReadSubtree
                            oResult = New Results
                            mPatient.PatientResults.Add(oResult)
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "id"
                                            oResult.ResultID = areaderchild.GetAttribute("root")
                                        Case "code"
                                            oResult.ResultCode = areaderchild.GetAttribute("code")
                                            oResult.CodeSystem = areaderchild.GetAttribute("codeSystemName")
                                            oResult.ResultCodeDisplay = areaderchild.GetAttribute("displayName")
                                        Case "statusCode"
                                            oResult.StatusCode = areaderchild.GetAttribute("code")
                                        Case "effectiveTime"
                                            oResult.ResultDate = areaderchild.GetAttribute("value")
                                            oResult.ResultDate = DateFromHL7(oResult.ResultDate)
                                        Case "value"
                                            oResult.ResultUnit = areaderchild.GetAttribute("unit")
                                            oResult.ResultValue = areaderchild.GetAttribute("value")
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oResults) Then
                oResults.Dispose()
                oResults = Nothing
            End If
            If Not IsNothing(oResult) Then
                oResult.Dispose()
                oResult = Nothing
            End If
            areaderchild = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadPregnancy(ByVal xreader As XmlReader) As Boolean
        'Dim m_node As XmlNode
        Dim areaderchild As XmlReader
        Dim oConditions As Conditions = Nothing
        Try
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name

                        Case "entry"
                            areaderchild = xreader.ReadSubtree
                            'Loop through the nodes
                            oConditions = New Conditions
                            mPatient.PatientConditions.Add(oConditions)
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "low"
                                            oConditions.EffectiveStartTime = areaderchild.GetAttribute("value")
                                            oConditions.EffectiveStartTime = DateFromHL7(oConditions.EffectiveStartTime)
                                        Case "high"
                                            oConditions.EffectiveEndTime = areaderchild.GetAttribute("value")
                                            oConditions.EffectiveEndTime = DateFromHL7(oConditions.EffectiveEndTime)
                                        Case "reference"
                                            oConditions.ProblemType = areaderchild.GetAttribute("value")
                                        Case "code"
                                            oConditions.ConditionType = areaderchild.GetAttribute("displayName")
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oConditions) Then
                oConditions.Dispose()
                oConditions = Nothing
            End If
            areaderchild = Nothing
        End Try
        Return Nothing
    End Function

    Private Function ReadConditions(ByVal xreader As XmlReader) As Boolean
        'Dim m_node As XmlNode
        Dim areaderchild As XmlReader
        Dim oConditions As Conditions = Nothing
        Try
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name

                        Case "entry"
                            areaderchild = xreader.ReadSubtree
                            'Loop through the nodes
                            oConditions = New Conditions
                            mPatient.PatientConditions.Add(oConditions)
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "low"
                                            oConditions.EffectiveStartTime = areaderchild.GetAttribute("value")
                                            oConditions.EffectiveStartTime = DateFromHL7(oConditions.EffectiveStartTime)
                                        Case "high"
                                            oConditions.EffectiveEndTime = areaderchild.GetAttribute("value")
                                            oConditions.EffectiveEndTime = DateFromHL7(oConditions.EffectiveEndTime)
                                        Case "reference"
                                            oConditions.ProblemType = areaderchild.GetAttribute("value")
                                        Case "code"
                                            oConditions.ConditionType = areaderchild.GetAttribute("displayName")
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oConditions) Then
                oConditions.Dispose()
                oConditions = Nothing
            End If
            areaderchild = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadAllergies(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderchild1 As XmlReader
        Dim ogloPatientHistoryCol As gloPatientHistoryCol = Nothing
        Dim ogloPatientHistory As gloPatientHistory = Nothing

        strMessageLog = New System.Text.StringBuilder
        Dim objMessagelogdetails As CCDMessageLogDetail
        objMessagelogdetails = New CCDMessageLogDetail
        'Dim oAllergies As Allergies
        Try
            ogloPatientHistoryCol = New gloPatientHistoryCol()
            mPatient.PatientHistory = ogloPatientHistoryCol
            While xreader.Read
                'Mandatory Field:oPatientHistory.HistoryCategory,oPatientHistory.HistoryItem,oPatientHistory.Comments
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "entry"
                            ogloPatientHistory = New gloPatientHistory
                            mPatient.PatientHistory.Add(ogloPatientHistory)
                            ogloPatientHistory.HistoryCategory = "Allergies"
                            Dim _AllergyDate As String = ""
                            areaderchild = xreader.ReadSubtree
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "code"
                                            'oAllergies.AllergyType = areaderchild.GetAttribute("code")
                                            If ogloPatientHistory.Comments = "" Then
                                                ogloPatientHistory.Comments = areaderchild.GetAttribute("displayName")
                                            End If
                                        Case "low"
                                            'oAllergies.EffectiveStartTime = areaderchild.GetAttribute("value")
                                            'oAllergies.EffectiveStartTime = DateFromHL7(oAllergies.EffectiveStartTime)
                                            _AllergyDate = areaderchild.GetAttribute("value")
                                            If Not IsNothing(_AllergyDate) Then
                                                ogloPatientHistory.DOEAllergy = DateFromHL7(_AllergyDate)
                                            End If

                                        Case "high"
                                            'oAllergies.EffectiveEndTime = areaderchild.GetAttribute("value")
                                            'oAllergies.EffectiveEndTime = DateFromHL7(oAllergies.EffectiveEndTime)
                                            If IsNothing(_AllergyDate) Then
                                                _AllergyDate = areaderchild.GetAttribute("value")
                                                ogloPatientHistory.DOEAllergy = DateFromHL7(_AllergyDate)
                                            End If
                                        Case "playingEntity"
                                            'If areaderchild.AttributeCount = 2 Then
                                            areaderchild1 = areaderchild.ReadSubtree
                                            While areaderchild1.Read
                                                If areaderchild.NodeType = XmlNodeType.Element Then
                                                    Select Case areaderchild1.Name
                                                        Case "code"
                                                            'oAllergies.ProductCode = areaderchild1.GetAttribute("code")
                                                            'oAllergies.ProductName = areaderchild1.GetAttribute("displayName")
                                                            ogloPatientHistory.HistoryItem = areaderchild1.GetAttribute("displayName")
                                                            Exit While
                                                    End Select
                                                End If
                                            End While

                                            'End If

                                    End Select
                                End If

                            End While
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(ogloPatientHistoryCol) Then
                ogloPatientHistoryCol.Dispose()
                ogloPatientHistoryCol = Nothing
            End If

            If Not IsNothing(ogloPatientHistory) Then
                ogloPatientHistory.Dispose()
                ogloPatientHistory = Nothing
            End If

            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If
            areaderchild = Nothing
            areaderchild1 = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadEncounters(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        'Dim areaderchild1 As XmlReader
        Dim oEncounters As EncountersCol = Nothing
        Dim oEncounter As Encounters = Nothing
        Try
            oEncounters = New EncountersCol
            mPatient.PatientEncounters = oEncounters
            While xreader.Read

                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "entry"
                            oEncounter = New Encounters
                            mPatient.PatientEncounters.Add(oEncounter)
                            areaderchild = xreader.ReadSubtree
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name
                                        Case "id"
                                            oEncounter.EncounterId = areaderchild.GetAttribute("root")
                                        Case "streetAddressLine"
                                            oEncounter.StreetAddress = areaderchild.ReadInnerXml
                                        Case "city"
                                            oEncounter.City = areaderchild.ReadInnerXml
                                        Case "state"
                                            oEncounter.State = areaderchild.ReadInnerXml
                                        Case "postalCode"
                                            oEncounter.PostalCode = areaderchild.ReadInnerXml
                                        Case "country"
                                            oEncounter.Country = areaderchild.ReadInnerXml
                                        Case "given"
                                            oEncounter.FirstName = areaderchild.ReadInnerXml
                                        Case "suffix"
                                            oEncounter.Suffix = areaderchild.ReadInnerXml
                                        Case "prefix"
                                            oEncounter.Prefix = areaderchild.ReadInnerXml
                                        Case "family"
                                            oEncounter.LastName = areaderchild.ReadInnerXml
                                        Case "low"
                                            oEncounter.EncounterDate = areaderchild.GetAttribute("value")
                                        Case "telecom"
                                            Select Case areaderchild.GetAttribute("use")

                                                Case "MC"
                                                    oEncounter.MobilePhone = areaderchild.GetAttribute("value")
                                                Case "HP"
                                                    oEncounter.HomePhone = areaderchild.GetAttribute("value")
                                                Case "WP"
                                                    oEncounter.WorkPhone = areaderchild.GetAttribute("value")
                                                Case "HV"
                                                    oEncounter.VacationPhone = areaderchild.GetAttribute("value")

                                                Case Else
                                                    If oEncounter.Email <> "" Then
                                                        oEncounter.URL = areaderchild.GetAttribute("value")
                                                    Else
                                                        oEncounter.Email = areaderchild.GetAttribute("value")
                                                    End If
                                            End Select
                                    End Select
                                End If
                            End While
                    End Select
                End If
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oEncounters) Then
                oEncounters.Dispose()
                oEncounters = Nothing
            End If

            If Not IsNothing(oEncounter) Then
                oEncounter.Dispose()
                oEncounter = Nothing
            End If
            areaderchild = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadMedication(ByVal xreader As XmlReader) As Boolean
        Dim areaderchild As XmlReader = Nothing
        Dim oMedications As MedicationsCol = Nothing
        Dim oMedication As Medication = Nothing
        '\\addedby suraj on 20081120- for validating (Collecting) mandatory fields
        strMessageLog = New System.Text.StringBuilder

        oMedications = New MedicationsCol
        mPatient.PatientMedications = oMedications
        Try
            While xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "substanceAdministration"
                            oMedication = New Medication
                            mPatient.PatientMedications.Add(oMedication)
                        Case "consumable"
                            areaderchild = xreader.ReadSubtree
                            'Loop through the nodes
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name

                                        Case "code" 'R
                                            oMedication.ProdCode = areaderchild.GetAttribute("code") 'R
                                            If oMedication.ProdCode = "" Then
                                                strMessageLog.Append(" Consumable->Code->Code value not Present ")
                                            End If
                                            oMedication.DrugName = areaderchild.GetAttribute("displayName") 'O
                                            oMedication.CodingSystem = areaderchild.GetAttribute("codeSystemName") 'R
                                            If oMedication.CodingSystem = "" Then
                                                strMessageLog.Append(" Consumable->Code->CodingSystem value not Present ")
                                            End If
                                        Case "name" 'R
                                            oMedication.FreeTextBrandName = areaderchild.ReadInnerXml
                                            If oMedication.FreeTextBrandName = "" Then
                                                strMessageLog.Append(" Consumable->name value not present")
                                            End If

                                    End Select
                                End If
                            End While
                        Case "statusCode"
                            oMedication.Status = xreader.GetAttribute("code") 'R
                            If oMedication.Status = "" Then
                                strMessageLog.Append(" StatusCode value not present ")
                            End If
                        Case "supply"
                            areaderchild = xreader.ReadSubtree
                            While areaderchild.Read
                                If areaderchild.NodeType = XmlNodeType.Element Then
                                    Select Case areaderchild.Name

                                        Case "effectiveTime"
                                            oMedication.WrittenDate = areaderchild.GetAttribute("value")
                                            oMedication.WrittenDate = DateFromHL7(oMedication.WrittenDate)
                                        Case "id"
                                            oMedication.StrengthUnits = areaderchild.GetAttribute("root")
                                        Case "quantity"
                                            oMedication.DrugStrength = areaderchild.GetAttribute("value")
                                    End Select
                                End If
                            End While
                        Case "entryRelationship"
                            If xreader.GetAttribute("typeCode") = "SUBJ" Then
                                areaderchild = xreader.ReadSubtree
                                While areaderchild.Read
                                    If areaderchild.NodeType = XmlNodeType.Element Then
                                        Select Case areaderchild.Name
                                            Case "code"
                                                oMedication.MedicationType = areaderchild.GetAttribute("displayName")
                                        End Select
                                    End If
                                End While
                            End If
                    End Select
                End If
            End While
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            'Memory Leak
            If Not IsNothing(oMedications) Then
                oMedications.Dispose()
                oMedications = Nothing
            End If

            If Not IsNothing(oMedication) Then
                oMedication.Dispose()
                oMedication = Nothing
            End If
            areaderchild = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadInsurance(ByVal xreader As XmlReader) As Boolean
        Dim xreaderchild As XmlReader

        Try
            While Not xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "act"
                            'Coverage activity
                            If xreader.GetAttribute("moodCode") = "DEF" Then
                                xreaderchild = xreader.ReadSubtree
                                ReadInsuranceDetails(xreaderchild)
                            End If
                    End Select
                End If
            End While
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            xreaderchild = Nothing
        End Try
        Return Nothing
    End Function
    Private Function ReadInsuranceDetails(ByVal xreader As XmlReader) As Boolean
        'Dim xreaderchild As XmlReader
        Dim eInsuranceType As InsuranceType
        Try
            While Not xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "act"
                            'Policy activity
                            If xreader.GetAttribute("moodCode") = "EVN" Then
                                eInsuranceType = InsuranceType.epolicy
                            End If
                        Case "performer"
                        Case ""
                    End Select
                End If
            End While
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            eInsuranceType = Nothing
        End Try
        Return Nothing
    End Function

    'Code Start-Added by kanchan on 20100610 for CCR & to get File type
    Public Function GetClinicalFileType() As String
        Dim xreader As XmlReader
        Dim myString As String = String.Empty
        Try
            'Dim oXMLSettings As New Xml.XmlReaderSettings()
            xreader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
            While xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "ContinuityOfCareRecord"
                            gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                            myString = "CCR"
                            Exit While
                        Case "ClinicalDocument"
                            gloLibCCDGeneral.ClinicalDocFileType = "CCD"
                            myString = "CCD"
                            Exit While
                        Case "ccr:ContinuityOfCareRecord"
                            gloLibCCDGeneral.ClinicalDocFileType = "CCR"
                            myString = "CCR"
                            Exit While
                    End Select
                End If
            End While
            Return myString
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            xreader = Nothing
        End Try
    End Function

    'Code Start-Added by kanchan on 20100616 for Export CCD View option
    Public Function SaveExportedCCD(ByVal _PatientID As Int64, ByVal sCCDFilePath As String, ByVal _Notes As String, Optional ByVal _sFileCreatedFrom As String = Nothing, Optional ByVal _ISGeneratedAtPatientRequest As Boolean = False, Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000", Optional ByVal FileType As String = "CCD", Optional ByVal nExamID As Int64 = 0) As Boolean
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim _fileHashValue As String = ""
        Dim _fileHashAlgorithmType As String = ""
        Dim arrByte As Byte() = Nothing
        Dim XMLarrByte As Byte() = Nothing

        Try

            arrByte = ConvertFiletoBinary(sCCDFilePath)
            XMLarrByte = ConvertFiletoBinary(sCCDFilePath)
            cmd = New SqlCommand("CCD_ExportedFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _PatientID

            sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mPatient.PatientName.FirstName

            sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mPatient.PatientName.LastName

            ''
            'Problems No:00000251::20121004
            If _IsOwnByPastExam Then
                sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                If (IsNothing(_DateOfservice)) Then
                    _DateOfservice = "1/1/2000"
                End If
                sqlParam.Value = Convert.ToDateTime(_DateOfservice)
            Else
                sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = DateTime.Now
            End If
            '


            sqlParam = cmd.Parameters.Add("@iData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = arrByte

            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sCCDFilePath

            sqlParam = cmd.Parameters.Add("@iXMLData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = XMLarrByte

            sqlParam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FileType


            sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _Notes



            _fileHashValue = gloSecurity.gloDataHashing.GetSHA1Hash(sCCDFilePath, _fileHashAlgorithmType)

            sqlParam = cmd.Parameters.Add("@sHashValue", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashValue

            sqlParam = cmd.Parameters.Add("@sHashAlgoType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashAlgorithmType

            sqlParam = cmd.Parameters.Add("@sFileCreatedFrom", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _sFileCreatedFrom

            sqlParam = cmd.Parameters.Add("@IsPatientRequest", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _ISGeneratedAtPatientRequest


            sqlParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nExamID

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            'Memory Leak
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            _fileHashValue = Nothing
            _fileHashAlgorithmType = Nothing
            arrByte = Nothing
            XMLarrByte = Nothing

        End Try
        Return Nothing
    End Function

    Public Function SaveAsExportedCCD(ByVal _PatientID As Int64, ByVal sCCDFilePath As String, ByVal _Notes As String, Optional ByVal _sFileCreatedFrom As String = Nothing) As Boolean
        Dim ogloCCDDBLayer As gloCCDDatabaseLayer = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim arrByte As Byte() = Nothing
        Dim XMLarrByte As Byte() = Nothing
        Dim _fileHashValue As String = ""
        Dim _fileHashAlgorithmType As String = ""
        Try
            arrByte = ConvertFiletoBinary(sCCDFilePath)
            XMLarrByte = ConvertFiletoBinary(sCCDFilePath)
         
            ogloCCDDBLayer = New gloCCDDatabaseLayer
            mPatient = ogloCCDDBLayer.GetPatientInfo(_PatientID)

            cmd = New SqlCommand("CCD_ExportedFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _PatientID

            sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mPatient.PatientName.FirstName

            sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = mPatient.PatientName.LastName

            sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DateTime.Now

            sqlParam = cmd.Parameters.Add("@iData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = arrByte

            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sCCDFilePath

            sqlParam = cmd.Parameters.Add("@iXMLData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = XMLarrByte

            sqlParam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "CCD"


            sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 100)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _Notes


            _fileHashValue = gloSecurity.gloDataHashing.GetSHA1Hash(sCCDFilePath, _fileHashAlgorithmType)

            sqlParam = cmd.Parameters.Add("@sHashValue", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashValue

            sqlParam = cmd.Parameters.Add("@sHashAlgoType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _fileHashAlgorithmType

            sqlParam = cmd.Parameters.Add("@sFileCreatedFrom", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _sFileCreatedFrom

            '...End code add by Sagar Ghodke on 20100916

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally

            'Memory Leak
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(ogloCCDDBLayer) Then
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            arrByte = Nothing
            XMLarrByte = Nothing
            _fileHashValue = Nothing
            _fileHashAlgorithmType = Nothing
        End Try
        Return Nothing
    End Function
    'Code Start-Added by kanchan on 20100616 for Export CCD View option
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
                End If
                If Not IsNothing(oReader) Then
                    oReader.Close()
                    oReader.Dispose()
                End If

            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Sub New()

    End Sub

    'Public Function GenerateClinicalInformation_2(ByVal CCDSection As String, Optional ByVal dtvitals As DataTable = Nothing) As String
    '    Dim ogloCCDDBLayer As New gloCCDDatabaseLayer

    '    Dim strDate As String 'this variable is used for formation date in yyyyMMdd format from the objects
    '    Try
    '        Dim strfilepath As String = GenerateFileName()

    '        'Dim strfilepath As String = "C:\SampleCCDFile.xml"
    '        Dim xmlwriter As XmlTextWriter = Nothing
    '        If System.IO.File.Exists(strfilepath) Then
    '            System.IO.File.Delete(strfilepath)
    '        End If
    '        xmlwriter = New XmlTextWriter(strfilepath, Nothing)
    '        xmlwriter.Formatting = Formatting.Indented

    '        xmlwriter.WriteStartDocument()
    '        '
    '        'Dim _myStyle As String = "type='text/xsl' href='http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl'"
    '        Dim _myStyle As String = "type='text/xsl' href='" & gloCCDSchema.gloCDAWriterParameters.CDAStyleSheetPath & "'"

    '        xmlwriter.WriteProcessingInstruction("xml-stylesheet", _myStyle)
    '        xmlwriter.WriteStartElement("ClinicalDocument")

    '        xmlwriter.WriteAttributeString("xsi:schemaLocation", "urn:hl7-org:v3 CDA.xsd")
    '        xmlwriter.WriteAttributeString("xmlns:voc", "urn:hl7-org:v3/voc")
    '        xmlwriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
    '        xmlwriter.WriteAttributeString("xmlns", "urn:hl7-org:v3")

    '        xmlwriter.WriteStartElement("typeId")
    '        xmlwriter.WriteAttributeString("extension", "POCD_HD000040")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.1.3")
    '        xmlwriter.WriteEndElement() 'End TypeId Element

    '        xmlwriter.WriteStartElement("templateId")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1")
    '        xmlwriter.WriteEndElement() 'End templateId Element

    '        xmlwriter.WriteStartElement("id")
    '        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '        xmlwriter.WriteEndElement() 'End id Element

    '        xmlwriter.WriteStartElement("code")
    '        xmlwriter.WriteAttributeString("code", "34133-9")
    '        xmlwriter.WriteAttributeString("displayName", "Summarization of patient data")
    '        'xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '        xmlwriter.WriteEndElement() 'End Code Element

    '        xmlwriter.WriteStartElement("effectiveTime")

    '        Dim dtTodayDate As String = Now.Date.Year & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & "000000-" & Now.Hour & Now.Minute

    '        xmlwriter.WriteAttributeString("value", dtTodayDate) '"19870618000000-0500"datetimestamp when file generated
    '        xmlwriter.WriteEndElement() 'End effectiveTime Element

    '        xmlwriter.WriteStartElement("confidentialityCode")
    '        xmlwriter.WriteAttributeString("code", "N")
    '        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.25")
    '        xmlwriter.WriteEndElement() 'End of confidentiality code.

    '        xmlwriter.WriteStartElement("languageCode")
    '        xmlwriter.WriteAttributeString("code", "en-US") 'datetimestamp when file generated
    '        xmlwriter.WriteEndElement() 'End Languagecode Element

    '        '-------------Record target
    '        xmlwriter.WriteStartElement("recordTarget")
    '        xmlwriter.WriteStartElement("patientRole")
    '        xmlwriter.WriteStartElement("id") 'PatientID for the Patient
    '        If mPatient.PatientName.Code <> "" AndAlso mPatient.PatientName.Code <> Nothing Then
    '            xmlwriter.WriteAttributeString("extension", mPatient.PatientName.Code)
    '        End If
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '        xmlwriter.WriteEndElement() 'End ID Element


    '        '********This element is not in standard microsoft format *******
    '        xmlwriter.WriteStartElement("addr")
    '        xmlwriter.WriteElementString("streetAddressLine", mPatient.PatientName.PersonContactAddress.Street)
    '        xmlwriter.WriteElementString("city", mPatient.PatientName.PersonContactAddress.City)
    '        xmlwriter.WriteElementString("state", mPatient.PatientName.PersonContactAddress.State)
    '        xmlwriter.WriteElementString("postalCode", mPatient.PatientName.PersonContactAddress.Zip)
    '        xmlwriter.WriteElementString("country", "US")
    '        xmlwriter.WriteEndElement() 'End addr Element
    '        '*********End of element *********************


    '        xmlwriter.WriteStartElement("patient")
    '        xmlwriter.WriteStartElement("name")
    '        'xmlwriter.WriteElementString("prefix", mPatient.PatientName.Prefix)

    '        xmlwriter.WriteStartElement("given")
    '        'xmlwriter.WriteAttributeString("qualifier", "CL")
    '        xmlwriter.WriteValue(mPatient.PatientName.FirstName)
    '        xmlwriter.WriteEndElement() 'End given Element

    '        xmlwriter.WriteStartElement("family")
    '        'xmlwriter.WriteAttributeString("qualifier", "BR")
    '        xmlwriter.WriteValue(mPatient.PatientName.LastName)
    '        xmlwriter.WriteEndElement() 'End family Element

    '        xmlwriter.WriteEndElement() 'End name Element

    '        xmlwriter.WriteStartElement("administrativeGenderCode")
    '        Select Case mPatient.Gender.ToUpper
    '            Case "FEMALE"
    '                xmlwriter.WriteAttributeString("code", "F")
    '                xmlwriter.WriteAttributeString("displayName", "Female")
    '            Case "MALE"
    '                xmlwriter.WriteAttributeString("code", "M")
    '                xmlwriter.WriteAttributeString("displayName", "Male")
    '            Case "OTHER"
    '                xmlwriter.WriteAttributeString("code", "UN")
    '                xmlwriter.WriteAttributeString("displayName", "Undifferentiated")
    '        End Select

    '        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.1")

    '        xmlwriter.WriteEndElement() 'End administrativeGenderCode Element

    '        xmlwriter.WriteStartElement("birthTime")
    '        strDate = Format(CType(mPatient.DateofBirth, Date), "yyyyMMdd")
    '        If strDate <> "" AndAlso strDate <> Nothing Then
    '            xmlwriter.WriteAttributeString("value", strDate)
    '        End If
    '        xmlwriter.WriteEndElement() 'End BirthTime


    '        xmlwriter.WriteStartElement("maritalStatusCode")
    '        Select Case mPatient.MaritalStatus.ToUpper
    '            Case "WIDOWED"
    '                xmlwriter.WriteAttributeString("code", "W")
    '            Case "UNMARRIED"
    '                xmlwriter.WriteAttributeString("code", "UN")
    '            Case "MARRIED"
    '                xmlwriter.WriteAttributeString("code", "M")
    '            Case "SINGLE"
    '                xmlwriter.WriteAttributeString("code", "S")
    '            Case "DIVORCED"
    '                xmlwriter.WriteAttributeString("code", "D")
    '            Case Else
    '                xmlwriter.WriteAttributeString("code", "UN")
    '        End Select
    '        If mPatient.MaritalStatus <> "" AndAlso mPatient.MaritalStatus <> Nothing Then
    '            xmlwriter.WriteAttributeString("displayName", mPatient.MaritalStatus)
    '        End If
    '        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.2")
    '        xmlwriter.WriteAttributeString("codeSystemName", "MaritalStatusCode")
    '        xmlwriter.WriteEndElement() 'maritalStatusCode END

    '        xmlwriter.WriteStartElement("raceCode")
    '        If mPatient.Race <> "" AndAlso mPatient.Race <> Nothing Then
    '            xmlwriter.WriteAttributeString("code", mPatient.RaceCode)
    '            xmlwriter.WriteAttributeString("displayName", mPatient.Race)
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.238")
    '            xmlwriter.WriteAttributeString("codeSystemName", "CDC Race and Ethnicity")
    '        End If
    '        xmlwriter.WriteEndElement() 'raceCode END

    '        xmlwriter.WriteStartElement("ethnicGroupCode")
    '        If mPatient.ethnicGroupCode <> "" AndAlso mPatient.ethnicGroupCode <> Nothing Then
    '            xmlwriter.WriteAttributeString("code", mPatient.ethnicGroupCode)
    '        End If
    '        If mPatient.Ethnicity <> "" AndAlso mPatient.Ethnicity <> Nothing Then
    '            xmlwriter.WriteAttributeString("displayName", mPatient.Ethnicity)
    '        End If
    '        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.238")
    '        xmlwriter.WriteAttributeString("codeSystemName", "CDC Race and Ethnicity")
    '        xmlwriter.WriteEndElement() 'ethnicGroupCode END

    '        xmlwriter.WriteStartElement("guardian")
    '        xmlwriter.WriteStartElement("templateId")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.3")
    '        xmlwriter.WriteAttributeString("assigningAuthorityName", "C32 Support Module")
    '        xmlwriter.WriteEndElement() 'End templateId

    '        xmlwriter.WriteStartElement("id")
    '        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '        xmlwriter.WriteEndElement() 'End id

    '        xmlwriter.WriteStartElement("addr")
    '        xmlwriter.WriteElementString("streetAddressLine", mPatient.Guardian_Address1)
    '        xmlwriter.WriteElementString("city", mPatient.Guardian_City)
    '        xmlwriter.WriteElementString("state", mPatient.Guardian_State)
    '        xmlwriter.WriteElementString("postalCode", mPatient.Guardian_ZIP)
    '        xmlwriter.WriteEndElement() 'End addr

    '        xmlwriter.WriteStartElement("telecom")

    '        If mPatient.Guardian_Phone <> "" AndAlso mPatient.Guardian_Phone <> Nothing Then
    '            xmlwriter.WriteAttributeString("use", "HP")
    '            xmlwriter.WriteAttributeString("value", "tel:+1" & "(" & mPatient.Guardian_Phone.Substring(0, 3) & ") " & mPatient.Guardian_Phone.Substring(3, 3) & "-" & mPatient.Guardian_Phone.Substring(3, 4))
    '        End If

    '        xmlwriter.WriteEndElement() 'telecom id


    '        xmlwriter.WriteStartElement("guardianPerson")
    '        xmlwriter.WriteStartElement("name")
    '        xmlwriter.WriteElementString("given", mPatient.Guardian_fName)
    '        xmlwriter.WriteElementString("given", mPatient.Guardian_mName)
    '        xmlwriter.WriteElementString("family", mPatient.Guardian_lName)
    '        xmlwriter.WriteEndElement() 'name
    '        xmlwriter.WriteEndElement() 'guardianPerson id

    '        xmlwriter.WriteEndElement() 'guardian END

    '        xmlwriter.WriteStartElement("languageCommunication")
    '        If Not IsNothing(mPatient.Language) AndAlso mPatient.Language <> "" Then

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.2")
    '            xmlwriter.WriteEndElement() 'End templateId  

    '            xmlwriter.WriteStartElement("languageCode")
    '            'here to get Laungage-Country eg. "en-US" format we first take the 
    '            If mPatient.LanguageCode <> "" AndAlso mPatient.LanguageCode <> Nothing Then
    '                xmlwriter.WriteAttributeString("code", mPatient.LanguageCode)
    '            Else
    '                xmlwriter.WriteAttributeString("code", "en-US")
    '            End If
    '            'xmlwriter.WriteAttributeString("code", mPatient.Language)
    '            xmlwriter.WriteEndElement() 'End languageCode  

    '            xmlwriter.WriteStartElement("preferenceInd")
    '            xmlwriter.WriteAttributeString("value", "true")
    '            xmlwriter.WriteEndElement() 'End preferenceInd  
    '        End If
    '        xmlwriter.WriteEndElement() 'End languageCommunication element

    '        xmlwriter.WriteEndElement() 'End Patient Element
    '        xmlwriter.WriteEndElement() 'End PatientRole Element
    '        xmlwriter.WriteEndElement() 'End recordTarget Element


    '        '---------Record Target
    '        ''Author Element Starts here ,person who generates the CCD file
    '        xmlwriter.WriteStartElement("author") 'author Element 

    '        xmlwriter.WriteStartElement("time")
    '        '**************8Comment by Shirish-value not in given format as in standard*****************
    '        xmlwriter.WriteAttributeString("value", Now.Date.Year & Now.Date.Month & Now.Date.Day)
    '        xmlwriter.WriteEndElement() 'End time element


    '        xmlwriter.WriteStartElement("assignedAuthor")

    '        xmlwriter.WriteStartElement("id")
    '        'xmlwriter.WriteAttributeString("root", mPatient.Author.PersonName.Code)

    '        Dim OOID As String = "ABC-" & Format(Now.Date.Month, "#00") & Format(Now.Date.Day, "#00") & Format(DateTime.Now.Millisecond, "#000") & "-JJ"

    '        'xmlwriter.WriteAttributeString("root", OOID) '"ABC-1234567-JJ"
    '        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '        xmlwriter.WriteEndElement() 'End id


    '        xmlwriter.WriteStartElement("assignedPerson")
    '        xmlwriter.WriteStartElement("name")
    '        xmlwriter.WriteStartElement("given")
    '        xmlwriter.WriteAttributeString("qualifier", "CL")
    '        xmlwriter.WriteValue("") ''mPatient.Author.PersonName.FirstName)
    '        xmlwriter.WriteEndElement() 'End given 

    '        xmlwriter.WriteStartElement("family")
    '        '**************Comment by Shirish- This attribute is not in standard format **************
    '        xmlwriter.WriteAttributeString("qualifier", "BR")
    '        xmlwriter.WriteValue("") ''mPatient.Author.PersonName.LastName)
    '        xmlwriter.WriteEndElement() 'End family 
    '        xmlwriter.WriteEndElement() 'End Name 
    '        xmlwriter.WriteEndElement() 'End Assigned Person

    '        xmlwriter.WriteStartElement("representedOrganization")
    '        xmlwriter.WriteStartElement("id")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '        xmlwriter.WriteEndElement() 'End ID
    '        xmlwriter.WriteElementString("name", mPatient.Author.Organization)
    '        xmlwriter.WriteEndElement() 'End representedOrganization
    '        xmlwriter.WriteEndElement() 'End assignedAuthor         

    '        xmlwriter.WriteEndElement() 'End Author element

    '        xmlwriter.WriteStartElement("custodian")
    '        xmlwriter.WriteStartElement("assignedCustodian")
    '        xmlwriter.WriteStartElement("representedCustodianOrganization")
    '        xmlwriter.WriteStartElement("id")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '        xmlwriter.WriteEndElement() 'End of id
    '        xmlwriter.WriteEndElement() ' end of representedCustodianOrganization
    '        xmlwriter.WriteEndElement() 'end of assignedCustodian
    '        xmlwriter.WriteEndElement() 'end of custodian

    '        xmlwriter.WriteStartElement("documentationOf")
    '        xmlwriter.WriteStartElement("serviceEvent")
    '        xmlwriter.WriteAttributeString("classCode", "PCPR")
    '        xmlwriter.WriteStartElement("effectiveTime")
    '        xmlwriter.WriteStartElement("low")
    '        xmlwriter.WriteAttributeString("value", "0")
    '        xmlwriter.WriteEndElement() 'End low 
    '        xmlwriter.WriteStartElement("high")
    '        xmlwriter.WriteAttributeString("value", dtTodayDate)
    '        xmlwriter.WriteEndElement() 'End high 

    '        xmlwriter.WriteEndElement() 'End EffectiveTime

    '        xmlwriter.WriteEndElement() 'End Service Event
    '        xmlwriter.WriteEndElement() 'End Documentation


    '        ' ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '        ' ''#################### Parent Component element 
    '        xmlwriter.WriteStartElement("component")

    '        ''----------------structuredBody-------------------
    '        xmlwriter.WriteStartElement("structuredBody")

    '        ''$$$$$$$$$$$$$ -- Component (INSURANCE) ---START---$$$$$$$$$$$$$$$$$$
    '        xmlwriter.WriteStartElement("component")
    '        xmlwriter.WriteComment("Insurance")
    '        xmlwriter.WriteStartElement("section")

    '        '------------templateId
    '        xmlwriter.WriteStartElement("templateId")
    '        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.9")
    '        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Payers Section Template")
    '        xmlwriter.WriteEndElement() 'templateId element END
    '        ''----------code 
    '        'xmlwriter.WriteStartElement("code")
    '        'xmlwriter.WriteAttributeString("code", "48764-5")
    '        'xmlwriter.WriteAttributeString("displayName", "PAYMENT SOURCES")
    '        ''**************Comment by Shirish in standard file 2.16.840.1.113883.6.1
    '        'xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '        'xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '        'xmlwriter.WriteEndElement() 'code element END
    '        '-----------title
    '        xmlwriter.WriteElementString("title", "Insurance")

    '        xmlwriter.WriteStartElement("text")
    '        'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

    '        xmlwriter.WriteStartElement("table")
    '        xmlwriter.WriteAttributeString("border", "1")
    '        xmlwriter.WriteAttributeString("width", "100%")
    '        xmlwriter.WriteStartElement("thead")
    '        xmlwriter.WriteStartElement("tr")
    '        xmlwriter.WriteElementString("th", "Insurance Plan Name")
    '        xmlwriter.WriteElementString("th", "Insurance ID")
    '        xmlwriter.WriteElementString("th", "Group #")
    '        xmlwriter.WriteElementString("th", "Subscriber Name")
    '        xmlwriter.WriteElementString("th", "Relation")
    '        xmlwriter.WriteElementString("th", "Start and End Dates")
    '        xmlwriter.WriteEndElement() '''''''tr End
    '        xmlwriter.WriteEndElement() ''''''''thead End
    '        xmlwriter.WriteStartElement("tbody")

    '        'Check whether insurance information is present or not -Code Added by Shirish.
    '        If mPatient.PatientInsurances.Count > 0 Then
    '            For Each oInsurances As Insurance In mPatient.PatientInsurances
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", oInsurances.InsuranceName)
    '                xmlwriter.WriteElementString("td", oInsurances.InsuranceId)
    '                xmlwriter.WriteElementString("td", oInsurances.GroupNo)
    '                xmlwriter.WriteElementString("td", oInsurances.InsSubscriberName)
    '                xmlwriter.WriteElementString("td", oInsurances.InsRelation)
    '                xmlwriter.WriteElementString("td", oInsurances.InsStartdate & " - " & oInsurances.InsEndDate)
    '                'xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end
    '            Next
    '        Else
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteStartElement("td")
    '            xmlwriter.WriteAttributeString("colspan", "6")
    '            xmlwriter.WriteEndElement() 'end of td
    '            xmlwriter.WriteEndElement() 'tr element end
    '        End If


    '        xmlwriter.WriteEndElement() 'Tbody element end
    '        xmlwriter.WriteEndElement() 'Table element end

    '        xmlwriter.WriteEndElement() 'text element END
    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        For Each oInsurances As Insurance In mPatient.PatientInsurances
    '            '-----------entry
    '            xmlwriter.WriteStartElement("entry")
    '            xmlwriter.WriteAttributeString("typeCode", "DRIV")
    '            '-----------act
    '            xmlwriter.WriteStartElement("act")
    '            xmlwriter.WriteAttributeString("classCode", "ACT")
    '            xmlwriter.WriteAttributeString("moodCode", "EVN")
    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.30")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Coverage Activity Template")
    '            xmlwriter.WriteEndElement() 'templateId element END

    '            xmlwriter.WriteStartElement("statusCode")
    '            xmlwriter.WriteAttributeString("code", "completed")
    '            xmlwriter.WriteEndElement() 'statusCode element END

    '            xmlwriter.WriteStartElement("informant")
    '            xmlwriter.WriteStartElement("assignedEntity")

    '            xmlwriter.WriteStartElement("id")
    '            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '            xmlwriter.WriteEndElement() 'id element END
    '            xmlwriter.WriteStartElement("representedOrganization")
    '            xmlwriter.WriteStartElement("id")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '            xmlwriter.WriteEndElement() 'id element END
    '            xmlwriter.WriteEndElement() ''''''representedOrganization End
    '            xmlwriter.WriteEndElement() ''''''assignedEntity End
    '            xmlwriter.WriteEndElement() ''''''informant End

    '            '-----------entryRelationship
    '            xmlwriter.WriteStartElement("entryRelationship")
    '            xmlwriter.WriteAttributeString("typeCode", "COMP")

    '            xmlwriter.WriteStartElement("sequenceNumber")
    '            xmlwriter.WriteAttributeString("value", "1")
    '            xmlwriter.WriteEndElement() '''''''sequenceNumber End

    '            xmlwriter.WriteStartElement("act")
    '            xmlwriter.WriteAttributeString("classCode", "ACT")
    '            xmlwriter.WriteAttributeString("moodCode", "EVN")
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.30")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Policy Activity Template")
    '            xmlwriter.WriteEndElement() '''''''templateId End

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.30")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "C32 Insurance Provider Module")
    '            xmlwriter.WriteEndElement() '''''''templateId End

    '            xmlwriter.WriteStartElement("id")
    '            xmlwriter.WriteAttributeString("nullFlavor", "NI")
    '            xmlwriter.WriteEndElement() '''''''id End

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("nullFlavor", "NI")
    '            xmlwriter.WriteEndElement() '''''''code End

    '            xmlwriter.WriteStartElement("statusCode")
    '            xmlwriter.WriteAttributeString("code", "completed")
    '            xmlwriter.WriteEndElement() '''''''statusCode End

    '            xmlwriter.WriteStartElement("performer")
    '            xmlwriter.WriteAttributeString("typeCode", "PRF")
    '            xmlwriter.WriteStartElement("assignedEntity")
    '            xmlwriter.WriteStartElement("id")
    '            xmlwriter.WriteAttributeString("nullFlavor", "NI")
    '            xmlwriter.WriteEndElement() '''''''id End

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "PAYOR")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.110")
    '            xmlwriter.WriteAttributeString("codeSystemName", "RoleClassRelationshipFormal")
    '            xmlwriter.WriteEndElement() '''''''code End

    '            xmlwriter.WriteStartElement("addr")
    '            xmlwriter.WriteElementString("streetAddressLine", oInsurances.InsSubsAddressLine1)
    '            xmlwriter.WriteElementString("streetAddressLine", oInsurances.InsSubsAddressLine2)
    '            xmlwriter.WriteElementString("city", oInsurances.InsSubsCity)
    '            xmlwriter.WriteElementString("state", oInsurances.InsSubsState)
    '            xmlwriter.WriteElementString("postalCode", oInsurances.InsSubsZip)
    '            xmlwriter.WriteEndElement() '''''''addr End


    '            xmlwriter.WriteStartElement("representedOrganization")
    '            xmlwriter.WriteElementString("name", oInsurances.InsuranceName)
    '            xmlwriter.WriteEndElement() ''''''' representedOrganization End

    '            xmlwriter.WriteEndElement() '''''''assignedEntity End
    '            xmlwriter.WriteEndElement() '''''''performer End

    '            xmlwriter.WriteStartElement("participant")
    '            xmlwriter.WriteAttributeString("typeCode", "COV")


    '            xmlwriter.WriteStartElement("participantRole")
    '            xmlwriter.WriteStartElement("id")
    '            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '            xmlwriter.WriteEndElement() ''''''' id End

    '            xmlwriter.WriteStartElement("code")
    '            If Not IsNothing(oInsurances.InsRelation) AndAlso oInsurances.InsRelation <> "" Then
    '                xmlwriter.WriteAttributeString("code", oInsurances.InsRelation)
    '                xmlwriter.WriteAttributeString("displayName", oInsurances.InsRelation)
    '            End If
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.111")
    '            xmlwriter.WriteAttributeString("codeSystemName", "RoleCode")
    '            xmlwriter.WriteEndElement() ''''''' code End
    '            xmlwriter.WriteEndElement() '''''''participantRole  End

    '            xmlwriter.WriteEndElement() ''''''' participant End

    '            xmlwriter.WriteEndElement() '''''''act End

    '            xmlwriter.WriteEndElement() '''''''entryRelationship End
    '            xmlwriter.WriteEndElement() '''''''act End
    '            xmlwriter.WriteEndElement() '''''''''''entry End
    '        Next

    '        xmlwriter.WriteEndElement() ''''Section END 
    '        xmlwriter.WriteEndElement() ''''component END 
    '        ''$$$$$$$$$$$$$ Saagar K -- Component (INSURANCE) ---END---$$$$$$$$$$$$$$$$$$



    '        ''$$$$$$$$$$$$$ Saagar K -- Component (PROBLEMS) ---START---$$$$$$$$$$$$$$$$$$

    '        If CCDSection.Contains("Problems") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.11")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Problems Section Template")
    '            xmlwriter.WriteEndElement() 'templateId element END

    '            '----------code 
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "11450-4")
    '            xmlwriter.WriteAttributeString("displayName", "Problem list")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END

    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Problems")
    '            '-----------text

    '            xmlwriter.WriteStartElement("text")
    '            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Condition")
    '            xmlwriter.WriteElementString("th", "Effective Dates")
    '            xmlwriter.WriteElementString("th", "Condition Status")
    '            xmlwriter.WriteElementString("th", "Problem Type")
    '            xmlwriter.WriteElementString("th", "SNOMED Code")
    '            xmlwriter.WriteElementString("th", "ICD9 Code")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")

    '            'Check Whether Problems data exists or not if not exists write blank tbody section
    '            If mPatient.PatientProblems.Count > 0 Then
    '                For Each oProblems As Problems In mPatient.PatientProblems
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oProblems.Condition)
    '                    xmlwriter.WriteElementString("td", oProblems.DateOfService)
    '                    xmlwriter.WriteElementString("td", oProblems.ConditionStatus)
    '                    xmlwriter.WriteElementString("td", oProblems.ProblemType)
    '                    xmlwriter.WriteElementString("td", oProblems.ConceptID)
    '                    xmlwriter.WriteElementString("td", oProblems.ICD9Code)
    '                    'xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "6")
    '                xmlwriter.WriteEndElement() 'End of td
    '                xmlwriter.WriteEndElement() 'End of tr
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END

    '            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '            For Each oProblems As Problems In mPatient.PatientProblems

    '                '-----------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")

    '                xmlwriter.WriteStartElement("act")
    '                xmlwriter.WriteAttributeString("classCode", "ACT")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.27")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Problem Act Template")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                '-----------id
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END

    '                '-----------code
    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("nullFlavor", "NA")
    '                xmlwriter.WriteEndElement() 'code element END

    '                '-----------statusCode
    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                '-----------effectiveTime
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteStartElement("low")
    '                xmlwriter.WriteAttributeString("value", "20011109")
    '                xmlwriter.WriteEndElement() 'low element END
    '                xmlwriter.WriteEndElement() 'effectiveTime element END

    '                xmlwriter.WriteStartElement("entryRelationship")
    '                'Shirish - As per standards value changed to Refr
    '                xmlwriter.WriteAttributeString("typeCode", "REFR")
    '                xmlwriter.WriteAttributeString("inversionInd", "false")

    '                xmlwriter.WriteStartElement("observation")
    '                xmlwriter.WriteAttributeString("classCode", "OBS")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")
    '                xmlwriter.WriteAttributeString("negationInd", "false")

    '                '-----------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.50")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Problem Observation Template")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                '-----------id 
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END
    '                'End -id element is not found in standard file

    '                '-----------code
    '                xmlwriter.WriteStartElement("code")
    '                If Not IsNothing(oProblems.ConceptID) AndAlso oProblems.ConceptID <> "" Then
    '                    xmlwriter.WriteAttributeString("code", oProblems.ConceptID)
    '                End If
    '                If Not IsNothing(oProblems.Condition) AndAlso oProblems.Condition <> "" Then
    '                    xmlwriter.WriteAttributeString("displayName", oProblems.Condition)
    '                End If
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
    '                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                xmlwriter.WriteEndElement() 'code element END

    '                If Not IsNothing(oProblems.Condition) AndAlso oProblems.Condition <> "" Then
    '                    xmlwriter.WriteStartElement("text")
    '                    xmlwriter.WriteStartElement("reference")
    '                    xmlwriter.WriteAttributeString("value", oProblems.Condition)
    '                    xmlwriter.WriteEndElement() 'reference element END
    '                    xmlwriter.WriteEndElement() 'text element END
    '                End If
    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                '-----------effectiveTime
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteStartElement("low")
    '                xmlwriter.WriteAttributeString("value", "20011109")
    '                xmlwriter.WriteEndElement() 'low element END
    '                xmlwriter.WriteEndElement() 'effectiveTime element END


    '                xmlwriter.WriteStartElement("value")
    '                If Not IsNothing(oProblems.ICD9Code) AndAlso oProblems.ICD9Code <> "" Then
    '                    xmlwriter.WriteAttributeString("code", oProblems.ICD9Code)
    '                End If
    '                If Not IsNothing(oProblems.ICD9) AndAlso oProblems.ICD9 <> "" Then
    '                    xmlwriter.WriteAttributeString("displayName", oProblems.ICD9)
    '                End If

    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.103")
    '                xmlwriter.WriteAttributeString("codeSystemName", "ICD9CM")
    '                xmlwriter.WriteAttributeString("xsi:type", "CD")
    '                xmlwriter.WriteEndElement() 'value element END


    '                xmlwriter.WriteStartElement("informant")
    '                xmlwriter.WriteStartElement("assignedEntity")
    '                '-----------id
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END

    '                xmlwriter.WriteStartElement("representedOrganization")
    '                '-----------id
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '                xmlwriter.WriteEndElement() 'id element END
    '                xmlwriter.WriteEndElement() 'representedOrganization element END

    '                xmlwriter.WriteEndElement() 'assignedEntity element END
    '                xmlwriter.WriteEndElement() 'informant element END

    '                '-----------entryRelationship
    '                xmlwriter.WriteStartElement("entryRelationship")
    '                xmlwriter.WriteAttributeString("typeCode", "REFR")
    '                'xmlwriter.WriteAttributeString("inversionInd", "false")

    '                xmlwriter.WriteStartElement("observation")
    '                xmlwriter.WriteAttributeString("classCode", "OBS")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                '------------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.50")
    '                xmlwriter.WriteEndElement() 'templateId element END
    '                '------------templateId
    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("code", "33999-4")
    '                xmlwriter.WriteAttributeString("displayName", "Status")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '                xmlwriter.WriteEndElement() 'code element END

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                '------------value
    '                If Not IsNothing(oProblems.ConditionStatus) AndAlso oProblems.ConditionStatus <> "" Then
    '                    xmlwriter.WriteStartElement("value")
    '                    Select Case oProblems.ConditionStatus
    '                        Case "Resolved"
    '                            xmlwriter.WriteAttributeString("code", "413322009")
    '                        Case "Active"
    '                            xmlwriter.WriteAttributeString("code", "55561003")
    '                        Case Else
    '                            xmlwriter.WriteAttributeString("code", "55561003")
    '                    End Select
    '                    xmlwriter.WriteAttributeString("displayName", oProblems.ConditionStatus)
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                    xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                    xmlwriter.WriteAttributeString("xsi:type", "CE")
    '                    xmlwriter.WriteEndElement() 'value element END
    '                End If

    '                xmlwriter.WriteEndElement() 'observation element END

    '                xmlwriter.WriteEndElement() 'entryRelationship element END


    '                xmlwriter.WriteEndElement() 'observation element END

    '                xmlwriter.WriteEndElement() 'entryRelationship element END

    '                xmlwriter.WriteEndElement() 'act element END

    '                xmlwriter.WriteEndElement() ''''entry END 

    '            Next

    '            xmlwriter.WriteEndElement() ''''Section END 
    '            xmlwriter.WriteEndElement() ''''component END 
    '        End If

    '        ''$$$$$$$$$$$$$ Saagar K -- Component (PROBLEMS) ---END---$$$$$$$$$$$$$$$$$$


    '        ''$$$$$$$$$$$$$ Saagar K -- Component (Allergies and Alerts) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("Allergy") = True Or CCDSection = "All" Then

    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteComment("Allergies and Alerts")

    '            xmlwriter.WriteStartElement("section")
    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.2")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
    '            xmlwriter.WriteEndElement() 'templateId element END
    '            '----------code 
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "48765-2")
    '            xmlwriter.WriteAttributeString("displayName", "Alerts")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END
    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Allergies and Alert Problems")
    '            '-----------text
    '            xmlwriter.WriteStartElement("text")
    '            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Type")
    '            xmlwriter.WriteElementString("th", "SNOMED Code")
    '            xmlwriter.WriteElementString("th", "Substance")
    '            xmlwriter.WriteElementString("th", "Reaction")
    '            xmlwriter.WriteElementString("th", "Status")
    '            xmlwriter.WriteEndElement()
    '            xmlwriter.WriteEndElement()
    '            xmlwriter.WriteStartElement("tbody")
    '            'Code to check whether data is present or not -Code by Shirish
    '            If mPatient.PatientAllergies.Count > 0 Then
    '                For Each oAllergies As Allergies In mPatient.PatientAllergies
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", "Drug Allergy")
    '                    xmlwriter.WriteElementString("td", oAllergies.ConceptID)
    '                    xmlwriter.WriteElementString("td", oAllergies.ProductName)
    '                    xmlwriter.WriteElementString("td", oAllergies.Reaction)
    '                    xmlwriter.WriteElementString("td", oAllergies.Status)
    '                    xmlwriter.WriteElementString("td", oAllergies.Status)

    '                    'xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "5")
    '                xmlwriter.WriteEndElement() 'End of td.
    '                xmlwriter.WriteEndElement() 'tr element end
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END

    '            'Dim i As Integer = 0
    '            For Each oAllergies As Allergies In mPatient.PatientAllergies
    '                'i = i + 1
    '                '-----------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")
    '                '-----------act
    '                xmlwriter.WriteStartElement("act")
    '                xmlwriter.WriteAttributeString("classCode", "ACT")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")
    '                '-----------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.27")
    '                xmlwriter.WriteEndElement() 'templateId element END
    '                '-----------templateId
    '                'xmlwriter.WriteStartElement("templateId")
    '                'xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.88.11.32.6")
    '                'xmlwriter.WriteEndElement() 'templateId element END
    '                '-----------id
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END
    '                '-----------code
    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("nullFlavor", "NA")
    '                xmlwriter.WriteEndElement() 'code element END

    '                '-----------statusCode
    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                '-----------entryRelationship
    '                xmlwriter.WriteStartElement("entryRelationship")
    '                xmlwriter.WriteAttributeString("typeCode", "SUBJ")

    '                '-----------observation
    '                xmlwriter.WriteStartElement("observation")
    '                xmlwriter.WriteAttributeString("classCode", "OBS")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                '-----------observation - templateId 
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.18")
    '                xmlwriter.WriteEndElement() 'observation - templateId   element END
    '                '-----------observation - code
    '                xmlwriter.WriteStartElement("code")
    '                'retrieve the code from the "Adverse_Event_Types" table
    '                oAllergies.AllergyType = "Allergy to Substance"
    '                xmlwriter.WriteAttributeString("code", "419199007")
    '                xmlwriter.WriteAttributeString("displayName", "Allergy to Substance")
    '                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                xmlwriter.WriteEndElement() 'observation - code element END
    '                '-----------observation - statusCode
    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'observation - statusCode element END

    '                '-----------observation - participant
    '                xmlwriter.WriteStartElement("participant")
    '                xmlwriter.WriteAttributeString("typeCode", "CSM")

    '                '-----------participant - participantRole
    '                xmlwriter.WriteStartElement("participantRole")
    '                xmlwriter.WriteAttributeString("classCode", "MANU")
    '                '-----------participantRole - playingEntity
    '                xmlwriter.WriteStartElement("playingEntity")
    '                xmlwriter.WriteAttributeString("classCode", "MMAT")
    '                '-----------playingEntity - code
    '                xmlwriter.WriteStartElement("code")
    '                'when we retrieve te allergies using the spGetLatestAllergies against that patient then,
    '                'we also retrieve the Drug ID against that patient. if the drug id = 0, then pass a harcoded value for RxNorm Code
    '                ' else if there is DrugId then get the top first sNDCCode from Drugs_Mst aginst that drugId and pass to the getRxNormCode() and put the RxNorm Code.
    '                ' if the RxNormCode against that NDC is empty then pass hardcoded value else pass the RxNormCode value that was retrieved.

    '                Dim _sNDCCode As String = ""
    '                Dim _sRXNormCode As String = ""


    '                If oAllergies.ProductCode <> "" AndAlso oAllergies.ProductCode <> Nothing Then
    '                    xmlwriter.WriteAttributeString("code", oAllergies.ProductCode)
    '                End If

    '                If oAllergies.ProductName <> "" AndAlso oAllergies.ProductName <> Nothing Then
    '                    xmlwriter.WriteAttributeString("displayName", oAllergies.ProductName)
    '                End If
    '                xmlwriter.WriteAttributeString("codeSystemName", "RxNorm")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.88")
    '                xmlwriter.WriteEndElement() 'playingEntity - code element END

    '                '-----------playingEntity - name
    '                xmlwriter.WriteElementString("name", oAllergies.ProductName)
    '                xmlwriter.WriteEndElement() 'participantRole - playingEntity element END

    '                xmlwriter.WriteEndElement() 'participant - participantRole element END

    '                xmlwriter.WriteEndElement() 'observation - participant element END

    '                xmlwriter.WriteEndElement() 'observation element END
    '                xmlwriter.WriteEndElement() 'entryRelationship element END

    '                xmlwriter.WriteEndElement() 'act element END
    '                xmlwriter.WriteEndElement() 'entry element END

    '            Next

    '            xmlwriter.WriteEndElement() 'section element END
    '            xmlwriter.WriteEndElement() 'component element END

    '        End If

    '        '*****************************Code commented by supriya *****************************************************


    '        '$$$$$$$$$$$$$ Saagar K -- Component (VITALS) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("Vitals") = True Or CCDSection = "All" Then

    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.16")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Vitals Signs Section Template")
    '            xmlwriter.WriteEndElement() 'templateId element END

    '            xmlwriter.WriteStartElement("id")
    '            'xmlwriter.WriteAttributeString("root", "be7961c4-5c95-4c8f-b233-d8059341bb9e")
    '            xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '            xmlwriter.WriteEndElement() 'id element END

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "8716-3")
    '            xmlwriter.WriteAttributeString("displayName", "VITAL SIGNS")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END

    '            xmlwriter.WriteElementString("title", "Vital Signs")


    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")

    '            If Not IsNothing(dtvitals) Then
    '                If dtvitals.Rows.Count > 0 Then

    '                    Dim rowcnt As Integer = 0
    '                    xmlwriter.WriteStartElement("tbody")

    '                    For i As Integer = 0 To dtvitals.Columns.Count - 1

    '                        xmlwriter.WriteStartElement("tr")
    '                        If i = 0 Then
    '                            xmlwriter.WriteElementString("th", "Vital Name")
    '                            For j As Integer = 0 To dtvitals.Rows.Count - 1
    '                                'If j < 5 Then
    '                                xmlwriter.WriteElementString("th", dtvitals.Rows(j)(i))
    '                                'End If
    '                            Next
    '                        Else
    '                            Select Case i
    '                                Case "1"
    '                                    xmlwriter.WriteElementString("td", "Systolic BP-Sitting (mm[Hg])")
    '                                Case "2"
    '                                    xmlwriter.WriteElementString("td", "Diastolic BP-Sitting (mm[Hg])")
    '                                Case "3"
    '                                    xmlwriter.WriteElementString("td", "Pulse Rate (/min)")
    '                                Case "4"
    '                                    xmlwriter.WriteElementString("td", "Respiration Rate (/min)")
    '                                Case "5"
    '                                    xmlwriter.WriteElementString("td", "Temp-Oral ([degF])")
    '                                Case "6"
    '                                    xmlwriter.WriteElementString("td", "Height ([in_us])")
    '                                Case "7"
    '                                    xmlwriter.WriteElementString("td", "Weight ([lb_av])")
    '                                Case "8"
    '                                    xmlwriter.WriteElementString("td", "Body Mass Index (kg/m2)")
    '                                Case "9"
    '                                    xmlwriter.WriteElementString("td", "Body Surface Area (m2)")
    '                            End Select

    '                            For j As Integer = 0 To dtvitals.Rows.Count - 1
    '                                'If rowcnt = 5 Then
    '                                '    rowcnt = 0
    '                                '    'xmlwriter.WriteElementString("td", "")
    '                                '    'xmlwriter.WriteEndElement() 'Tbody element end
    '                                '    'xmlwriter.WriteStartElement("tbody")
    '                                '    If i = 1 Then
    '                                '        xmlwriter.WriteEndElement() 'tr element end
    '                                '        xmlwriter.WriteStartElement("tr")
    '                                '        xmlwriter.WriteElementString("th", "Vital Name")
    '                                '        For k As Integer = j To dtvitals.Rows.Count - 1
    '                                '            If k < j + 5 Then
    '                                '                xmlwriter.WriteElementString("th", dtvitals.Rows(k)(0))
    '                                '            End If
    '                                '        Next
    '                                '        xmlwriter.WriteEndElement() 'tr element end
    '                                '    End If
    '                                '    i = 1
    '                                '    Exit For
    '                                'End If

    '                                xmlwriter.WriteElementString("td", dtvitals.Rows(j)(i))
    '                                'rowcnt = rowcnt + 1
    '                            Next
    '                        End If

    '                        xmlwriter.WriteEndElement() 'tr element end

    '                    Next
    '                    xmlwriter.WriteEndElement() 'Tbody element end
    '                Else
    '                    'table has zero rows- write only coulms
    '                    xmlwriter.WriteStartElement("thead")
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("th", "Vital Name")
    '                    xmlwriter.WriteElementString("th", "Systolic BP-Sitting (mm[Hg])")
    '                    xmlwriter.WriteElementString("th", "Diastolic BP-Sitting (mm[Hg])")
    '                    xmlwriter.WriteElementString("th", "Pulse Rate (/min)")
    '                    xmlwriter.WriteElementString("th", "Respiration Rate (/min)")
    '                    xmlwriter.WriteElementString("th", "Temp-Oral ([degF])")
    '                    xmlwriter.WriteElementString("th", "Height ([in_us])")
    '                    xmlwriter.WriteElementString("th", "Weight ([lb_av])")
    '                    xmlwriter.WriteElementString("th", "Body Mass Index (kg/m2)")
    '                    xmlwriter.WriteElementString("th", "Body Surface Area (m2)")
    '                    xmlwriter.WriteEndElement() 'End of tr
    '                    xmlwriter.WriteEndElement() 'End of thead
    '                    xmlwriter.WriteStartElement("tbody")
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteAttributeString("colspan", " 10")
    '                    xmlwriter.WriteEndElement()
    '                    xmlwriter.WriteEndElement()
    '                    xmlwriter.WriteEndElement()
    '                End If
    '            Else
    '                'If table is nothing
    '                xmlwriter.WriteStartElement("tbody")
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "")
    '                xmlwriter.WriteEndElement() 'End of tr
    '                xmlwriter.WriteEndElement() 'End of tbody
    '            End If

    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END


    '            For i As Integer = 0 To dtvitals.Rows.Count - 1

    '                '-----------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")

    '                xmlwriter.WriteStartElement("organizer")
    '                xmlwriter.WriteAttributeString("classCode", "CLUSTER")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                '------------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.35")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Vital Signs Organizer Template")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END

    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("code", "46680005")
    '                xmlwriter.WriteAttributeString("displayName", "VITAL SIGNS")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                xmlwriter.WriteEndElement() 'code element END

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'id element END

    '                strDate = Format(CType(dtvitals.Rows(i)(0), Date), "yyyyMMdd")
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteAttributeString("value", strDate)
    '                xmlwriter.WriteEndElement() 'effectiveTime element END

    '                If dtvitals.Rows(i)("dHeightinInch") > 0 Then
    '                    xmlwriter.WriteStartElement("component")
    '                    xmlwriter.WriteStartElement("observation")
    '                    xmlwriter.WriteAttributeString("classCode", "OBS")
    '                    xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                    xmlwriter.WriteStartElement("templateId")
    '                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
    '                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Result Observation Template")
    '                    xmlwriter.WriteEndElement() 'templateId element END

    '                    xmlwriter.WriteStartElement("id")
    '                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                    xmlwriter.WriteEndElement() 'id element END

    '                    xmlwriter.WriteStartElement("code")
    '                    xmlwriter.WriteAttributeString("code", "50373000")
    '                    xmlwriter.WriteAttributeString("displayName", "Body height")
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                    xmlwriter.WriteEndElement() 'code element END

    '                    xmlwriter.WriteStartElement("statusCode")
    '                    xmlwriter.WriteAttributeString("code", "completed")
    '                    xmlwriter.WriteEndElement() 'statusCode element END

    '                    xmlwriter.WriteStartElement("effectiveTime")
    '                    xmlwriter.WriteAttributeString("value", strDate)
    '                    xmlwriter.WriteEndElement() 'effectiveTime element END

    '                    If Not IsNothing(dtvitals.Rows(i)("dHeightinInch")) AndAlso dtvitals.Rows(i)("dHeightinInch") <> 0 Then
    '                        xmlwriter.WriteStartElement("value")
    '                        xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dHeightinInch"))
    '                        xmlwriter.WriteAttributeString("unit", "in_us")
    '                        xmlwriter.WriteAttributeString("xsi:type", "PQ")
    '                        xmlwriter.WriteEndElement() 'value element END
    '                    End If
    '                    xmlwriter.WriteEndElement() 'observation element END
    '                    xmlwriter.WriteEndElement() 'component element END
    '                End If

    '                If dtvitals.Rows(i)("dWeightinlbs") > 0 Then
    '                    xmlwriter.WriteStartElement("component")
    '                    xmlwriter.WriteStartElement("observation")
    '                    xmlwriter.WriteAttributeString("classCode", "OBS")
    '                    xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                    xmlwriter.WriteStartElement("templateId")
    '                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
    '                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Result Observation Template")
    '                    xmlwriter.WriteEndElement() 'templateId element END

    '                    xmlwriter.WriteStartElement("id")
    '                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                    xmlwriter.WriteEndElement() 'id element END

    '                    xmlwriter.WriteStartElement("code")
    '                    xmlwriter.WriteAttributeString("code", "27113001")
    '                    xmlwriter.WriteAttributeString("displayName", "Body weight")
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                    xmlwriter.WriteEndElement() 'code element END

    '                    xmlwriter.WriteStartElement("statusCode")
    '                    xmlwriter.WriteAttributeString("code", "completed")
    '                    xmlwriter.WriteEndElement() 'statusCode element END

    '                    xmlwriter.WriteStartElement("effectiveTime")
    '                    xmlwriter.WriteAttributeString("value", strDate)
    '                    xmlwriter.WriteEndElement() 'effectiveTime element END

    '                    If Not IsNothing(dtvitals.Rows(i)("dWeightinlbs")) AndAlso dtvitals.Rows(i)("dWeightinlbs") <> 0 Then
    '                        xmlwriter.WriteStartElement("value")
    '                        xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dWeightinlbs"))
    '                        xmlwriter.WriteAttributeString("unit", "lbs")
    '                        xmlwriter.WriteAttributeString("xsi:type", "PQ")
    '                        xmlwriter.WriteEndElement() 'value element END
    '                    End If


    '                    xmlwriter.WriteEndElement() 'observation element END
    '                    xmlwriter.WriteEndElement() 'component element END
    '                End If

    '                If dtvitals.Rows(i)("dBloodPressureSittingMax") > 0 Then
    '                    xmlwriter.WriteStartElement("component")
    '                    xmlwriter.WriteStartElement("observation")
    '                    xmlwriter.WriteAttributeString("classCode", "OBS")
    '                    xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                    xmlwriter.WriteStartElement("templateId")
    '                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
    '                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Result Observation Template")
    '                    xmlwriter.WriteEndElement() 'templateId element END

    '                    xmlwriter.WriteStartElement("id")
    '                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                    xmlwriter.WriteEndElement() 'id element END

    '                    xmlwriter.WriteStartElement("code")
    '                    xmlwriter.WriteAttributeString("code", "271649006")
    '                    xmlwriter.WriteAttributeString("displayName", "Systolic BP")
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                    xmlwriter.WriteEndElement() 'code element END

    '                    xmlwriter.WriteStartElement("statusCode")
    '                    xmlwriter.WriteAttributeString("code", "completed")
    '                    xmlwriter.WriteEndElement() 'statusCode element END

    '                    xmlwriter.WriteStartElement("effectiveTime")
    '                    xmlwriter.WriteAttributeString("value", strDate)
    '                    xmlwriter.WriteEndElement() 'effectiveTime element END

    '                    If Not IsNothing(dtvitals.Rows(i)("dBloodPressureSittingMax")) AndAlso dtvitals.Rows(i)("dBloodPressureSittingMax") <> 0 Then
    '                        xmlwriter.WriteStartElement("value")
    '                        xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dBloodPressureSittingMax"))
    '                        xmlwriter.WriteAttributeString("unit", "mm[Hg]")
    '                        xmlwriter.WriteAttributeString("xsi:type", "PQ")
    '                        xmlwriter.WriteEndElement() 'value element END
    '                    End If

    '                    xmlwriter.WriteEndElement() 'observation element END
    '                    xmlwriter.WriteEndElement() 'component element END
    '                End If

    '                If dtvitals.Rows(i)("dBloodPressureSittingMin") > 0 Then
    '                    xmlwriter.WriteStartElement("component")
    '                    xmlwriter.WriteStartElement("observation")
    '                    xmlwriter.WriteAttributeString("classCode", "OBS")
    '                    xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                    xmlwriter.WriteStartElement("templateId")
    '                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
    '                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Result Observation Template")
    '                    xmlwriter.WriteEndElement() 'templateId element END

    '                    xmlwriter.WriteStartElement("id")
    '                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                    xmlwriter.WriteEndElement() 'id element END

    '                    xmlwriter.WriteStartElement("code")
    '                    xmlwriter.WriteAttributeString("code", "271650006")
    '                    xmlwriter.WriteAttributeString("displayName", "Diastolic BP")
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                    xmlwriter.WriteEndElement() 'code element END

    '                    xmlwriter.WriteStartElement("statusCode")
    '                    xmlwriter.WriteAttributeString("code", "completed")
    '                    xmlwriter.WriteEndElement() 'statusCode element END

    '                    xmlwriter.WriteStartElement("effectiveTime")
    '                    xmlwriter.WriteAttributeString("value", strDate)
    '                    xmlwriter.WriteEndElement() 'effectiveTime element END

    '                    If Not IsNothing(dtvitals.Rows(i)("dBloodPressureSittingMin")) AndAlso dtvitals.Rows(i)("dBloodPressureSittingMin") <> 0 Then
    '                        xmlwriter.WriteStartElement("value")
    '                        xmlwriter.WriteAttributeString("value", dtvitals.Rows(i)("dBloodPressureSittingMin"))
    '                        xmlwriter.WriteAttributeString("unit", "mm[Hg]")
    '                        xmlwriter.WriteAttributeString("xsi:type", "PQ")
    '                        xmlwriter.WriteEndElement() 'value element END
    '                    End If

    '                    xmlwriter.WriteEndElement() 'observation element END
    '                    xmlwriter.WriteEndElement() 'component element END
    '                End If

    '                xmlwriter.WriteEndElement() 'organizer element END

    '                xmlwriter.WriteEndElement() 'entry element END
    '            Next

    '            xmlwriter.WriteEndElement() 'section element END
    '            xmlwriter.WriteEndElement() 'component element END

    '        End If

    '        ''$$$$$$$$$$$$$ Saagar K -- Component (VITALS) ---END---$$$$$$$$$$$$$$$$$$



    '        ' ''++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '        ' ''$$$$$$$$$$$$$ Saagar K -- Component (Medications) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("Medications") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteComment("Medications")

    '            xmlwriter.WriteStartElement("section")
    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            'Shirish -Comment this attrribute is not in standard format file
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.8")
    '            xmlwriter.WriteEndElement() 'templateId element END
    '            '----------code 
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "10160-0")
    '            xmlwriter.WriteAttributeString("displayName", "History of medication use")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")

    '            xmlwriter.WriteEndElement() 'code element END
    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Medications")
    '            '-----------text
    '            xmlwriter.WriteStartElement("text")
    '            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")


    '            For Each oMedications As Medication In mPatient.PatientMedications

    '                xmlwriter.WriteStartElement("table")
    '                xmlwriter.WriteAttributeString("width", "100%")
    '                xmlwriter.WriteStartElement("col")
    '                xmlwriter.WriteAttributeString("width", "50%")
    '                xmlwriter.WriteEndElement() 'col element end
    '                xmlwriter.WriteStartElement("col")
    '                xmlwriter.WriteAttributeString("width", "50%")
    '                xmlwriter.WriteEndElement() 'col element end
    '                xmlwriter.WriteStartElement("tbody")
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.DrugName)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end

    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("align", "right")
    '                xmlwriter.WriteElementString("content", "Issued on " + oMedications.MedicationDate)

    '                'xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end
    '                xmlwriter.WriteEndElement() 'Tbody element end
    '                xmlwriter.WriteEndElement() ' table element end

    '                xmlwriter.WriteStartElement("table")
    '                xmlwriter.WriteAttributeString("width", "100%")
    '                xmlwriter.WriteAttributeString("border", "1")
    '                xmlwriter.WriteStartElement("col")
    '                xmlwriter.WriteAttributeString("width", "12%")
    '                xmlwriter.WriteEndElement() 'col element end
    '                xmlwriter.WriteStartElement("col")
    '                xmlwriter.WriteAttributeString("width", "88%")
    '                xmlwriter.WriteEndElement() 'col element end
    '                xmlwriter.WriteStartElement("tbody")

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "NDC Code:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.ProdCode)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Strength:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.DrugStrength)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Dose:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.DrugQuantity + " " + oMedications.DrugForm)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Quantity:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.DrugQuantity)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Days:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Days)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Refills:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Refills)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Sig:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Frequency)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Route:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Route)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Diagnosis:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.CheifComplaint)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Pharmacy:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Pharmacy)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end

    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteElementString("td", "Status:")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteStartElement("content")
    '                xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                xmlwriter.WriteElementString("content", oMedications.Status)
    '                xmlwriter.WriteEndElement() 'content element end
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end


    '                xmlwriter.WriteEndElement() 'Tbody element end
    '                xmlwriter.WriteEndElement() ' table element end
    '                xmlwriter.WriteStartElement("paragraph")
    '                xmlwriter.WriteEndElement() 'paragraph element end

    '            Next
    '            xmlwriter.WriteEndElement() 'text element END

    '            'Comment by Shirish- <assignedEntry> is not in our code

    '            For Each oMedications As Medication In mPatient.PatientMedications

    '                '-----------entry
    '                xmlwriter.WriteStartElement("entry")

    '                '-----------entry - substanceAdministration 
    '                xmlwriter.WriteStartElement("substanceAdministration")
    '                xmlwriter.WriteAttributeString("classCode", "SBADM")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                '-----------entry - substanceAdministration - templateId 
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")

    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.24")
    '                xmlwriter.WriteEndElement() 'entry- substanceAdministration - templateId element END

    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "cdbd33f0-6cde-11db-9fe1-0800200c9a66")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement()

    '                '---------------
    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "active")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                '-----------entry - substanceAdministration - consumable  
    '                xmlwriter.WriteStartElement("consumable")

    '                '-----------entry - substanceAdministration - consumable - manufacturedProduct  
    '                xmlwriter.WriteStartElement("manufacturedProduct")
    '                xmlwriter.WriteAttributeString("classCode", "MANU")
    '                '-----------entry - substanceAdministration - consumable - manufacturedProduct - templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.53")
    '                xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - templateId element END
    '                '-----------entry - substanceAdministration - consumable - manufacturedProduct - manufacturedMaterial 
    '                xmlwriter.WriteStartElement("manufacturedMaterial")
    '                xmlwriter.WriteAttributeString("classCode", "MMAT")

    '                '-----------entry - substanceAdministration - consumable - manufacturedProduct - code 
    '                xmlwriter.WriteStartElement("code")
    '                If oMedications.ProdCode <> "" AndAlso oMedications.ProdCode <> Nothing Then
    '                    xmlwriter.WriteAttributeString("code", oMedications.ProdCode)
    '                End If
    '                If oMedications.DrugName <> "" AndAlso oMedications.DrugName <> Nothing Then
    '                    xmlwriter.WriteAttributeString("displayName", oMedications.DrugName)
    '                End If

    '                xmlwriter.WriteAttributeString("codeSystemName", "NDC")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.69")
    '                xmlwriter.WriteElementString("originalText", oMedications.DrugName)
    '                xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - code element END

    '                xmlwriter.WriteEndElement() 'entry - substanceAdministration - consumable - manufacturedProduct - manufacturedMaterial element END

    '                xmlwriter.WriteEndElement() 'entry- substanceAdministration - consumable - manufacturedProduct element END

    '                xmlwriter.WriteEndElement() 'entry- substanceAdministration - consumable element END

    '                xmlwriter.WriteEndElement() 'entry- substanceAdministration element END

    '                xmlwriter.WriteEndElement() 'entry element END

    '            Next

    '            xmlwriter.WriteEndElement() 'section element END
    '            xmlwriter.WriteEndElement() 'component element END
    '        End If

    '        '************ Lab Results *******************'

    '        If CCDSection.Contains("Results") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteComment("Results")

    '            xmlwriter.WriteStartElement("section")
    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Results Section Template")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.14")
    '            xmlwriter.WriteEndElement() 'templateId element END
    '            '----------code 
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "30954-2")
    '            xmlwriter.WriteAttributeString("displayName", "Relevant diagnostic tests and/or laboratory data")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")

    '            xmlwriter.WriteEndElement() 'code element END
    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Results")
    '            '-----------text
    '            xmlwriter.WriteStartElement("text")
    '            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")


    '            For Each oLabtests As LabTest In mPatient.LabTests

    '                If oLabtests.LabResults.Count > 0 Then

    '                    xmlwriter.WriteStartElement("table")
    '                    xmlwriter.WriteAttributeString("width", "100%")
    '                    xmlwriter.WriteAttributeString("border", "1")
    '                    xmlwriter.WriteStartElement("tbody")

    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", "Lab Facility:")
    '                    xmlwriter.WriteStartElement("td")

    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue(oLabtests.LabResults.Item(0).LabFacility)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end

    '                    xmlwriter.WriteElementString("td", "Provider:")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue(oLabtests.LabResults.Item(0).ProviderName)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end

    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", "Requisition:")
    '                    xmlwriter.WriteStartElement("td")

    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue(oLabtests.LabResults.Item(0).OrderNo)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end

    '                    xmlwriter.WriteElementString("td", "Specimen:")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue("")
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end

    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", "Collection Date:")
    '                    xmlwriter.WriteStartElement("td")

    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    If oLabtests.LabResults.Item(0).SpecimenDate <> "" And Not IsDBNull(oLabtests.LabResults.Item(0).SpecimenDate) Then
    '                        strDate = Format(CType(oLabtests.LabResults.Item(0).SpecimenDate, Date), "yyyyMMdd")
    '                    End If

    '                    xmlwriter.WriteValue(strDate)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end

    '                    xmlwriter.WriteElementString("td", "Report Date:")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    If oLabtests.LabResults.Item(0).ResultDate <> "" And Not IsDBNull(oLabtests.LabResults.Item(0).ResultDate) Then
    '                        strDate = Format(CType(oLabtests.LabResults.Item(0).ResultDate, Date), "yyyyMMdd")
    '                    End If
    '                    xmlwriter.WriteValue(strDate)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end


    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", "")
    '                    xmlwriter.WriteElementString("td", "")

    '                    xmlwriter.WriteElementString("td", "Reviewed:")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue("Unreviewed")
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end

    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteAttributeString("colspan", "2")
    '                    xmlwriter.WriteStartElement("content")
    '                    xmlwriter.WriteAttributeString("styleCode", "Bold")
    '                    xmlwriter.WriteValue("Test : " & oLabtests.LabResults.Item(0).TestName)
    '                    xmlwriter.WriteEndElement() 'content element end
    '                    xmlwriter.WriteEndElement() 'td element end

    '                    xmlwriter.WriteStartElement("td")
    '                    xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end

    '                    xmlwriter.WriteEndElement() 'Tbody element end
    '                    xmlwriter.WriteEndElement() ' table element end

    '                    xmlwriter.WriteStartElement("table")
    '                    xmlwriter.WriteAttributeString("width", "100%")
    '                    xmlwriter.WriteAttributeString("border", "1")
    '                    xmlwriter.WriteStartElement("thead")
    '                    xmlwriter.WriteStartElement("tr")
    '                    'xmlwriter.WriteElementString("th", "Type")
    '                    xmlwriter.WriteElementString("th", "Component")
    '                    xmlwriter.WriteElementString("th", "LOINC Code")
    '                    xmlwriter.WriteElementString("th", "Abnormal")
    '                    xmlwriter.WriteElementString("th", "Result")
    '                    xmlwriter.WriteElementString("th", "Units")
    '                    xmlwriter.WriteElementString("th", "Ref Range")

    '                    xmlwriter.WriteEndElement() 'tr element end
    '                    xmlwriter.WriteEndElement() 'thead element end

    '                    xmlwriter.WriteStartElement("tbody")

    '                    For Each oLabResults As LabResults In oLabtests.LabResults

    '                        xmlwriter.WriteStartElement("tr")
    '                        'oLabtests.LabResults.Item(0).ResultName
    '                        xmlwriter.WriteElementString("td", oLabResults.ResultName)
    '                        xmlwriter.WriteElementString("td", oLabResults.ResultLOINCID)
    '                        xmlwriter.WriteStartElement("td")
    '                        xmlwriter.WriteAttributeString("align", "center")
    '                        xmlwriter.WriteValue(oLabResults.AbnormalFlag)
    '                        xmlwriter.WriteEndElement() 'td element end
    '                        xmlwriter.WriteStartElement("td")
    '                        xmlwriter.WriteAttributeString("align", "center")
    '                        xmlwriter.WriteValue(oLabResults.ResultValue)
    '                        xmlwriter.WriteEndElement() 'td element end
    '                        xmlwriter.WriteStartElement("td")
    '                        xmlwriter.WriteAttributeString("align", "center")
    '                        xmlwriter.WriteValue(oLabResults.ResultUnit)
    '                        xmlwriter.WriteEndElement() 'td element end
    '                        xmlwriter.WriteStartElement("td")
    '                        xmlwriter.WriteAttributeString("align", "center")
    '                        xmlwriter.WriteValue(oLabResults.ResultRange)
    '                        xmlwriter.WriteEndElement() 'td element end

    '                        xmlwriter.WriteEndElement() 'tr element end
    '                        If oLabResults.ResultComment <> "" Then
    '                            xmlwriter.WriteStartElement("tr")
    '                            xmlwriter.WriteElementString("td", "")
    '                            xmlwriter.WriteStartElement("td")
    '                            xmlwriter.WriteAttributeString("colspan", "4")
    '                            xmlwriter.WriteValue(oLabResults.ResultComment)
    '                            xmlwriter.WriteEndElement() 'td element end
    '                            xmlwriter.WriteEndElement() 'tr element end
    '                        End If
    '                    Next
    '                    xmlwriter.WriteEndElement() 'Tbody element end
    '                    xmlwriter.WriteEndElement() ' table element end
    '                    xmlwriter.WriteStartElement("paragraph")
    '                    xmlwriter.WriteEndElement() ' paragraph element end
    '                End If
    '            Next


    '            xmlwriter.WriteEndElement() 'text element END


    '            For Each oLabtests As LabTest In mPatient.LabTests

    '                If oLabtests.LabResults.Count > 0 Then

    '                    '-----------entry
    '                    xmlwriter.WriteStartElement("entry")

    '                    '-----------entry - organizer 
    '                    xmlwriter.WriteStartElement("organizer")
    '                    xmlwriter.WriteAttributeString("classCode", "BATTERY")
    '                    xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                    '-----------entry - templateId 
    '                    xmlwriter.WriteStartElement("templateId")
    '                    xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Results Organizer Template")
    '                    xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.32")
    '                    xmlwriter.WriteEndElement() 'entry- templateId element END

    '                    '-----------entry - id
    '                    xmlwriter.WriteStartElement("id")
    '                    xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                    xmlwriter.WriteEndElement() 'entry- id element END

    '                    '-----------entry - code
    '                    xmlwriter.WriteStartElement("code")
    '                    xmlwriter.WriteAttributeString("code", "33765-9")
    '                    If Not IsNothing(oLabtests.LabResults.Item(0).TestName) AndAlso oLabtests.LabResults.Item(0).TestName <> "" Then
    '                        xmlwriter.WriteAttributeString("displayName", oLabtests.LabResults.Item(0).TestName)
    '                    End If
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '                    xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '                    xmlwriter.WriteEndElement() 'entry - code element END

    '                    xmlwriter.WriteStartElement("statusCode")
    '                    xmlwriter.WriteAttributeString("code", "completed")
    '                    xmlwriter.WriteEndElement() 'statusCode element END

    '                    xmlwriter.WriteStartElement("effectiveTime")
    '                    If oLabtests.LabResults.Item(0).SpecimenDate <> "" And Not IsDBNull(oLabtests.LabResults.Item(0).SpecimenDate) Then
    '                        strDate = Format(CType(oLabtests.LabResults.Item(0).SpecimenDate, Date), "yyyyMMdd")
    '                    End If
    '                    strDate = strDate & "000000-" & Now.Hour & Now.Minute
    '                    xmlwriter.WriteAttributeString("value", strDate)
    '                    xmlwriter.WriteEndElement() 'effectiveTime element END


    '                    For Each oLabResults As LabResults In oLabtests.LabResults
    '                        xmlwriter.WriteStartElement("component")
    '                        xmlwriter.WriteStartElement("observation")
    '                        xmlwriter.WriteAttributeString("classCode", "OBS")
    '                        xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                        xmlwriter.WriteStartElement("templateId")
    '                        xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.31")
    '                        xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Result Observation Template")
    '                        xmlwriter.WriteEndElement() 'templateId element END

    '                        xmlwriter.WriteStartElement("id")
    '                        xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                        xmlwriter.WriteEndElement() 'id element END

    '                        xmlwriter.WriteStartElement("code")
    '                        xmlwriter.WriteAttributeString("code", "33765-9")
    '                        If oLabtests.LabResults.Item(0).ResultName <> "" And Not IsDBNull(oLabtests.LabResults.Item(0).ResultName) Then
    '                            xmlwriter.WriteAttributeString("displayName", oLabtests.LabResults.Item(0).ResultName)
    '                        End If
    '                        xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '                        xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '                        xmlwriter.WriteEndElement() 'code element END

    '                        xmlwriter.WriteStartElement("statusCode")
    '                        xmlwriter.WriteAttributeString("code", "completed")
    '                        xmlwriter.WriteEndElement() 'statusCode element END

    '                        xmlwriter.WriteStartElement("effectiveTime")
    '                        xmlwriter.WriteAttributeString("value", strDate)
    '                        xmlwriter.WriteEndElement() 'effectiveTime element END

    '                        If Not IsNothing(oLabtests.LabResults.Item(0).ResultValue) AndAlso oLabtests.LabResults.Item(0).ResultValue <> "" Then
    '                            xmlwriter.WriteStartElement("value")
    '                            xmlwriter.WriteAttributeString("xsi:type", "PQ")
    '                            xmlwriter.WriteAttributeString("value", oLabtests.LabResults.Item(0).ResultValue)
    '                            If Not IsNothing(oLabtests.LabResults.Item(0).ResultUnit) AndAlso oLabtests.LabResults.Item(0).ResultUnit <> "" Then
    '                                xmlwriter.WriteAttributeString("unit", oLabtests.LabResults.Item(0).ResultUnit)
    '                            End If
    '                            xmlwriter.WriteEndElement() 'value element END
    '                        End If

    '                        If Not IsNothing(oLabtests.LabResults.Item(0).ResultRange) AndAlso oLabtests.LabResults.Item(0).ResultRange <> "" Then
    '                            xmlwriter.WriteStartElement("referenceRange")
    '                            xmlwriter.WriteStartElement("observationRange")
    '                            xmlwriter.WriteElementString("text", oLabtests.LabResults.Item(0).ResultRange)
    '                            xmlwriter.WriteEndElement() 'observationRange element END
    '                            xmlwriter.WriteEndElement() 'referenceRange element END
    '                        End If

    '                        xmlwriter.WriteEndElement() 'observation element END
    '                        xmlwriter.WriteEndElement() 'component element END
    '                    Next

    '                    xmlwriter.WriteEndElement() 'entry- organizer element END
    '                    xmlwriter.WriteEndElement() 'entry element END
    '                End If
    '            Next

    '            xmlwriter.WriteEndElement() 'section element END
    '            xmlwriter.WriteEndElement() 'component element END
    '        End If
    '        '*****************************Lab Results End *****************************************************

    '        ''$$$$$$$$$$$$$ Saagar K -- Component (IMMUNIZATIONS) ---START---$$$$$$$$$$$$$$$$$$

    '        If CCDSection.Contains("Immunization") = True Or CCDSection = "All" Then

    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.6")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Immunization Section Template")
    '            xmlwriter.WriteEndElement() ''''''''templateId END

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "11369-6")
    '            xmlwriter.WriteAttributeString("displayName", "Patient Immunizations")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() ''''''''code END

    '            xmlwriter.WriteElementString("title", "Immunizations")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Vaccine")
    '            xmlwriter.WriteElementString("th", "Administration Date")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")

    '            If mPatient.PatientImmunizations.Count > 0 Then
    '                For Each oImmunization As Immunization In mPatient.PatientImmunizations
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oImmunization.VaccineName)
    '                    xmlwriter.WriteElementString("td", oImmunization.ImmunizationDate)
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "2")
    '                xmlwriter.WriteEndElement() 'end of td
    '                xmlwriter.WriteEndElement() 'end of tr
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '            For Each oImmunization As Immunization In mPatient.PatientImmunizations
    '                '------------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")
    '                xmlwriter.WriteStartElement("substanceAdministration")
    '                xmlwriter.WriteAttributeString("classCode", "SBADM")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")
    '                'xmlwriter.WriteAttributeString("negationInd", "false")

    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.24")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD")
    '                xmlwriter.WriteEndElement() ''''''''templateId END

    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() ''''''''id END

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() ''''''''statusCode END

    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteAttributeString("xsi:type", "IVL_TS")
    '                xmlwriter.WriteStartElement("center")
    '                xmlwriter.WriteAttributeString("value", dtTodayDate)
    '                xmlwriter.WriteEndElement() ''''''''center END
    '                xmlwriter.WriteEndElement() ''''''''effectiveTime END

    '                If Not IsNothing(oImmunization.RouteCode) AndAlso oImmunization.RouteCode <> "" Then
    '                    xmlwriter.WriteStartElement("routeCode")
    '                    xmlwriter.WriteAttributeString("code", oImmunization.RouteCode)
    '                    xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.112")
    '                    xmlwriter.WriteAttributeString("codeSystemName", "RouteOfAdministration")
    '                    xmlwriter.WriteAttributeString("displayName", oImmunization.Route)
    '                    xmlwriter.WriteEndElement() ''''''''routeCode END
    '                End If

    '                xmlwriter.WriteStartElement("consumable")
    '                xmlwriter.WriteStartElement("manufacturedProduct")
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.53")
    '                xmlwriter.WriteEndElement() ''''''''templateId END

    '                xmlwriter.WriteStartElement("manufacturedMaterial")

    '                xmlwriter.WriteStartElement("code")
    '                If Not IsNothing(oImmunization.VaccineCode) AndAlso oImmunization.VaccineCode <> "" Then
    '                    xmlwriter.WriteAttributeString("code", oImmunization.VaccineCode)
    '                End If
    '                If Not IsNothing(oImmunization.VaccineName) AndAlso oImmunization.VaccineName <> "" Then
    '                    xmlwriter.WriteAttributeString("displayName", oImmunization.VaccineName)
    '                End If
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.59")
    '                ' xmlwriter.WriteElementString("originalText", oImmunization.VaccineName)

    '                xmlwriter.WriteEndElement() ''''''''code END

    '                'xmlwriter.WriteElementString("lotNumberText", "mm245-417-AEB")

    '                xmlwriter.WriteEndElement() ''''''''manufacturedMaterial END

    '                xmlwriter.WriteEndElement() ''''''''manufacturedProduct END

    '                xmlwriter.WriteEndElement() ''''''''consumable END

    '                xmlwriter.WriteEndElement() ''''''''substanceAdministration END
    '                xmlwriter.WriteEndElement() ''''''''entry END
    '            Next

    '            xmlwriter.WriteEndElement() 'section element END
    '            xmlwriter.WriteEndElement() 'component element END

    '        End If
    '        ''$$$$$$$$$$$$$ Saagar K -- Component (IMMUNIZATIONS) ---END---$$$$$$$$$$$$$$$$$$


    '        ''$$$$$$$$$$$$$ Saagar K -- Component (FAMILY HISTORY) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("FamilyHistory") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.4")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Family History Section Template")
    '            xmlwriter.WriteEndElement() ''''''''templateId END

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "10157-6")
    '            xmlwriter.WriteAttributeString("displayName", "HISTORY OF FAMILY MEMBER DISEASES")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() ''''''''code END

    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Family History")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Description")
    '            xmlwriter.WriteElementString("th", "Qualifiers")
    '            xmlwriter.WriteElementString("th", "Comments")
    '            xmlwriter.WriteElementString("th", "Date Reported")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")
    '            If mPatient.PatientFamilyHistory.Count > 0 Then
    '                For Each oFamilyHistory As FamilyHistory In mPatient.PatientFamilyHistory
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxDescription)
    '                    xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxQualifiers)
    '                    xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxComments)
    '                    xmlwriter.WriteElementString("td", oFamilyHistory.FmlyHxDateReported)
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "4")
    '                xmlwriter.WriteEndElement()
    '                xmlwriter.WriteEndElement()
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '            xmlwriter.WriteEndElement() '''''''section END
    '            xmlwriter.WriteEndElement() '''''''component END
    '        End If

    '        ''$$$$$$$$$$$$$ Saagar K -- Component (FAMILY HISTORY) ---END---$$$$$$$$$$$$$$$$$$



    '        ''$$$$$$$$$$$$$ Saagar K -- Component (SOCIAL HISTORY) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("SocialHistory") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.15")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Social History Section Template")
    '            xmlwriter.WriteEndElement() ''''''''templateId END

    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "29762-2")
    '            xmlwriter.WriteAttributeString("displayName", "SOCIAL HISTORY")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() ''''''''code END

    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Social History")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Description")
    '            xmlwriter.WriteElementString("th", "Qualifiers")
    '            xmlwriter.WriteElementString("th", "Comments")
    '            xmlwriter.WriteElementString("th", "Date Reported")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")
    '            If mPatient.PatientSocialHistory.Count > 0 Then
    '                For Each oSocialHistory As SocialHistory In mPatient.PatientSocialHistory
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oSocialHistory.SocialHxDescription)
    '                    xmlwriter.WriteElementString("td", oSocialHistory.SocialHxQualifiers)
    '                    xmlwriter.WriteElementString("td", oSocialHistory.SocialHxComments)
    '                    xmlwriter.WriteElementString("td", oSocialHistory.SocialHxDateReported)
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "4")
    '                xmlwriter.WriteEndElement() 'end of td
    '                xmlwriter.WriteEndElement() 'end of tr
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end
    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '            xmlwriter.WriteEndElement() '''''''section END
    '            xmlwriter.WriteEndElement() '''''''component END
    '        End If

    '        ''$$$$$$$$$$$$$ Saagar K -- Component (SOCIAL HISTORY) ---END---$$$$$$$$$$$$$$$$$$


    '        ''$$$$$$$$$$$$$ Saagar K -- Component (PROCEDURES) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("Procedures") = True Or CCDSection = "All" Then

    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.12")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Procedures Section Template")
    '            xmlwriter.WriteEndElement() ''''''''templateId END

    '            '------------code
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "47519-4")
    '            xmlwriter.WriteAttributeString("displayName", "HISTORY OF PROCEDURES")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END

    '            xmlwriter.WriteElementString("title", "Procedures")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Description")
    '            xmlwriter.WriteElementString("th", "Procedure")
    '            xmlwriter.WriteElementString("th", "Diagnosis")
    '            xmlwriter.WriteElementString("th", "Provider")
    '            xmlwriter.WriteElementString("th", "Service Date")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")
    '            If mPatient.PatientProcedures.Count > 0 Then
    '                For Each oProcedures As Procedures In mPatient.PatientProcedures
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oProcedures.CPTDescription)
    '                    xmlwriter.WriteElementString("td", oProcedures.CPTCode)
    '                    xmlwriter.WriteElementString("td", oProcedures.ICD9Description)
    '                    xmlwriter.WriteElementString("td", oProcedures.ProviderLastName & " " & oProcedures.ProviderFirstName & "" & oProcedures.ProviderSuffix)
    '                    xmlwriter.WriteElementString("td", oProcedures.DateOfService)
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "5")
    '                xmlwriter.WriteEndElement()
    '                xmlwriter.WriteEndElement() 'tr element end
    '            End If

    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '            For Each oProcedures As Procedures In mPatient.PatientProcedures
    '                '------------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")

    '                xmlwriter.WriteStartElement("procedure")
    '                xmlwriter.WriteAttributeString("classCode", "PROC")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.29")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Procedures Activity Template")
    '                xmlwriter.WriteEndElement() ''''''''templateId END

    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() ''''''''id END

    '                xmlwriter.WriteStartElement("code")
    '                If Not IsNothing(oProcedures.CPTCode) AndAlso oProcedures.CPTCode <> "" Then
    '                    xmlwriter.WriteAttributeString("code", oProcedures.CPTCode)
    '                End If
    '                If Not IsNothing(oProcedures.CPTDescription) AndAlso oProcedures.CPTDescription <> "" Then
    '                    xmlwriter.WriteAttributeString("displayName", oProcedures.CPTDescription)
    '                End If

    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.12")
    '                xmlwriter.WriteAttributeString("codeSystemName", "CPT-4")
    '                xmlwriter.WriteEndElement()

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() ''''''''statusCode END

    '                strDate = Format(CType(oProcedures.DateOfService, Date), "yyyyMMdd")
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteAttributeString("value", strDate)
    '                xmlwriter.WriteEndElement() ''''''''effectiveTime END

    '                xmlwriter.WriteEndElement() '''''''''procedure entry

    '                xmlwriter.WriteEndElement() '''''''''END entry

    '            Next

    '            xmlwriter.WriteEndElement() '''''''section END
    '            xmlwriter.WriteEndElement() '''''''component END
    '        End If
    '        ''$$$$$$$$$$$$$ Saagar K -- Component (PROCEDURES) ---END---$$$$$$$$$$$$$$$$$$


    '        ''$$$$$$$$$$$$$ Saagar K -- Component (ENCOUNTER) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("Encounter") = True Or CCDSection = "All" Then


    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.3")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Encounters Section Template")
    '            xmlwriter.WriteEndElement() ''''''''templateId END

    '            '------------code
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "46240-8")
    '            xmlwriter.WriteAttributeString("displayName", "History of encounters")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END

    '            xmlwriter.WriteElementString("title", "Encounters")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Encounter")
    '            xmlwriter.WriteElementString("th", "Provider")
    '            xmlwriter.WriteElementString("th", "Location")
    '            xmlwriter.WriteElementString("th", "Date")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")
    '            If mPatient.PatientEncounters.Count > 0 Then
    '                For Each oEncounters As Encounters In mPatient.PatientEncounters
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oEncounters.ExamName)
    '                    xmlwriter.WriteElementString("td", oEncounters.ProvLastName & " " & oEncounters.ProvFirstName)
    '                    xmlwriter.WriteElementString("td", oEncounters.Location)
    '                    xmlwriter.WriteElementString("td", oEncounters.DateOfService)
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "4")
    '                xmlwriter.WriteEndElement() 'td element end
    '                xmlwriter.WriteEndElement() 'tr element end
    '            End If

    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '            For Each oEncounters As Encounters In mPatient.PatientEncounters
    '                '------------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")

    '                xmlwriter.WriteStartElement("encounter")
    '                xmlwriter.WriteAttributeString("classCode", "ENC")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.21")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Encounter Activity Template")
    '                xmlwriter.WriteEndElement() ''''''''templateId END

    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "d2b4fa4d-060c-4a65-acc9-b554750a1ad7")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() ''''''''id END

    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("code", "GENRL")
    '                xmlwriter.WriteAttributeString("displayName", "General")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.5.4")
    '                xmlwriter.WriteAttributeString("codeSystemName", "ActCode")
    '                xmlwriter.WriteEndElement() ''''''''code END

    '                strDate = Format(CType(oEncounters.DateOfService, Date), "yyyyMMdd")
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteAttributeString("value", dtTodayDate)
    '                xmlwriter.WriteEndElement() ''''''''effectiveTime END

    '                xmlwriter.WriteStartElement("performer")
    '                xmlwriter.WriteStartElement("assignedEntity")
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.140.1.0.6.3")
    '                xmlwriter.WriteAttributeString("extension", "36")
    '                xmlwriter.WriteEndElement() ''''''''id END

    '                xmlwriter.WriteEndElement() ''''''''assignedEntity END
    '                xmlwriter.WriteEndElement() ''''''''performer END

    '                xmlwriter.WriteStartElement("informant")
    '                xmlwriter.WriteStartElement("assignedEntity")
    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "b80957bc-51dd-42d4-b05b-5a340c80819e")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() ''''''''id END

    '                xmlwriter.WriteStartElement("representedOrganization")
    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.140.1.0.6")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '                xmlwriter.WriteEndElement() ''''''''id END
    '                xmlwriter.WriteEndElement() ''''''''representedOrganization END

    '                xmlwriter.WriteEndElement() ''''''''assignedEntity END

    '                xmlwriter.WriteEndElement() ''''''''informant END

    '                xmlwriter.WriteEndElement() ''''''''End encounter

    '                xmlwriter.WriteEndElement() ''''''''End entry
    '            Next

    '            xmlwriter.WriteEndElement() '''''''section END
    '            xmlwriter.WriteEndElement() '''''''component END
    '        End If
    '        ''$$$$$$$$$$$$$ Saagar K -- Component (ENCOUNTER) ---END---$$$$$$$$$$$$$$$$$$



    '        ''$$$$$$$$$$$$$ Saagar K -- Component (ADVANCE DIRECTIVES) ---START---$$$$$$$$$$$$$$$$$$
    '        If CCDSection.Contains("AdvanceDirectives") = True Or CCDSection = "All" Then
    '            xmlwriter.WriteStartElement("component")
    '            xmlwriter.WriteStartElement("section")

    '            '------------templateId
    '            xmlwriter.WriteStartElement("templateId")
    '            xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.1")
    '            xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Advanced Directives Section Template")
    '            xmlwriter.WriteEndElement() 'templateId element END

    '            '------------code
    '            xmlwriter.WriteStartElement("code")
    '            xmlwriter.WriteAttributeString("code", "42348-3")
    '            xmlwriter.WriteAttributeString("displayName", "ADVANCE DIRECTIVES")
    '            xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '            xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '            xmlwriter.WriteEndElement() 'code element END

    '            '-----------title
    '            xmlwriter.WriteElementString("title", "Advance Directives")

    '            '-----------text
    '            xmlwriter.WriteStartElement("text")
    '            'xmlwriter.WriteElementString("text", "See entries below - they should all be represented here too!")

    '            xmlwriter.WriteStartElement("table")
    '            xmlwriter.WriteAttributeString("border", "1")
    '            xmlwriter.WriteAttributeString("width", "100%")
    '            xmlwriter.WriteStartElement("thead")
    '            xmlwriter.WriteStartElement("tr")
    '            xmlwriter.WriteElementString("th", "Directive")
    '            xmlwriter.WriteElementString("th", "Pat Aware")
    '            xmlwriter.WriteElementString("th", "Third Party")
    '            xmlwriter.WriteElementString("th", "Verification")
    '            xmlwriter.WriteElementString("th", "Reviewed")
    '            xmlwriter.WriteEndElement() '''''''tr End
    '            xmlwriter.WriteEndElement() ''''''''thead End
    '            xmlwriter.WriteStartElement("tbody")

    '            If mPatient.Advancedirective.Count > 0 Then
    '                For Each oAdvDirectives As Advancedirective In mPatient.Advancedirective
    '                    xmlwriter.WriteStartElement("tr")
    '                    xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveName)
    '                    xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectivePatAware)
    '                    xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveThirdParty)
    '                    xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveVerification)
    '                    xmlwriter.WriteElementString("td", oAdvDirectives.AdvDirectiveReviewed)
    '                    'xmlwriter.WriteEndElement() 'td element end
    '                    xmlwriter.WriteEndElement() 'tr element end
    '                Next
    '            Else
    '                xmlwriter.WriteStartElement("tr")
    '                xmlwriter.WriteStartElement("td")
    '                xmlwriter.WriteAttributeString("colspan", "5")
    '                xmlwriter.WriteEndElement()
    '                xmlwriter.WriteEndElement()
    '            End If


    '            xmlwriter.WriteEndElement() 'Tbody element end
    '            xmlwriter.WriteEndElement() 'Table element end

    '            xmlwriter.WriteEndElement() 'text element END
    '            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '            For Each oAdvDirectives As Advancedirective In mPatient.Advancedirective
    '                '------------entry
    '                xmlwriter.WriteStartElement("entry")
    '                xmlwriter.WriteAttributeString("typeCode", "DRIV")

    '                xmlwriter.WriteStartElement("observation")
    '                xmlwriter.WriteAttributeString("classCode", "OBS")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                '------------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.17")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Advance Directive Observation Template")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                '------------id
    '                xmlwriter.WriteStartElement("id")
    '                'Comment by Shirish root code is different
    '                'xmlwriter.WriteAttributeString("root", "b9c147b0-9353-4839-8386-2ceddf54332e")"9b54c3c9-1673-49c7-aef9-b037ed72ed27"
    '                'xmlwriter.WriteAttributeString("root", "9b54c3c9-1673-49c7-aef9-b037ed72ed27")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END

    '                xmlwriter.WriteStartElement("code")
    '                If Not IsNothing(oAdvDirectives.AdvanceDirectiveType) AndAlso oAdvDirectives.AdvanceDirectiveType <> "" Then
    '                    xmlwriter.WriteAttributeString("displayName", oAdvDirectives.AdvanceDirectiveType)
    '                End If

    '                xmlwriter.WriteStartElement("originalText")
    '                xmlwriter.WriteStartElement("reference")
    '                xmlwriter.WriteAttributeString("value", "#AdvanceDirective1")
    '                xmlwriter.WriteEndElement() 'reference element END
    '                xmlwriter.WriteEndElement() 'originalText element END

    '                xmlwriter.WriteStartElement("translation")
    '                xmlwriter.WriteAttributeString("code", "71388002")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                xmlwriter.WriteAttributeString("displayName", "Other Directive")
    '                xmlwriter.WriteEndElement() 'translation element END

    '                xmlwriter.WriteEndElement() 'code element END

    '                xmlwriter.WriteStartElement("text")
    '                xmlwriter.WriteStartElement("reference")
    '                xmlwriter.WriteAttributeString("value", "#AdvanceDirective1")
    '                xmlwriter.WriteEndElement() 'reference element END
    '                xmlwriter.WriteEndElement() 'text element END

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                strDate = Format(CType(oAdvDirectives.AdvDirectiveVerification, Date), "yyyyMMdd")
    '                xmlwriter.WriteStartElement("effectiveTime")
    '                xmlwriter.WriteStartElement("low")
    '                xmlwriter.WriteAttributeString("value", strDate)
    '                xmlwriter.WriteEndElement() 'low element END
    '                xmlwriter.WriteStartElement("high")
    '                xmlwriter.WriteAttributeString("nullFlavor", "UNK")
    '                xmlwriter.WriteEndElement() 'high element END
    '                xmlwriter.WriteEndElement() 'effectiveTime element END

    '                xmlwriter.WriteStartElement("informant")
    '                xmlwriter.WriteStartElement("assignedEntity")

    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "12b03ae2-4a85-4b20-942a-21d1dd9bfa5d")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END

    '                xmlwriter.WriteStartElement("representedOrganization")
    '                xmlwriter.WriteStartElement("id")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.19.5")
    '                'xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.3.140.1.0.6")
    '                xmlwriter.WriteEndElement() 'id element END
    '                xmlwriter.WriteEndElement() 'representedOrganization element END

    '                xmlwriter.WriteEndElement() 'assignedEntity element END

    '                xmlwriter.WriteEndElement() 'informant element END

    '                xmlwriter.WriteStartElement("participant")
    '                xmlwriter.WriteAttributeString("typeCode", "VRF")
    '                '------------templateId
    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.58")
    '                xmlwriter.WriteAttributeString("assigningAuthorityName", "CCD Verification of an Advance Directive Observation Template")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                xmlwriter.WriteStartElement("time")
    '                xmlwriter.WriteAttributeString("value", strDate)
    '                xmlwriter.WriteEndElement() 'time element END

    '                xmlwriter.WriteStartElement("awarenessCode")
    '                xmlwriter.WriteAttributeString("displayName", "Yes")
    '                xmlwriter.WriteStartElement("translation")
    '                xmlwriter.WriteAttributeString("code", "F")
    '                xmlwriter.WriteAttributeString("displayName", "Full Awareness")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.11.10310")
    '                xmlwriter.WriteAttributeString("codeSystemName", "TargetAwareness")
    '                xmlwriter.WriteEndElement() 'translation element END
    '                xmlwriter.WriteEndElement() 'awarenessCode element END

    '                xmlwriter.WriteStartElement("participantRole")
    '                xmlwriter.WriteStartElement("id")
    '                'xmlwriter.WriteAttributeString("root", "8b760e21-7326-4519-8f85-1b146c85fb4a")
    '                xmlwriter.WriteAttributeString("root", System.Guid.NewGuid.ToString())
    '                xmlwriter.WriteEndElement() 'id element END
    '                xmlwriter.WriteEndElement() 'participantRole element END

    '                xmlwriter.WriteEndElement() 'participant element END

    '                xmlwriter.WriteStartElement("entryRelationship")
    '                xmlwriter.WriteAttributeString("typeCode", "REFR")
    '                xmlwriter.WriteAttributeString("inversionInd", "false")

    '                xmlwriter.WriteStartElement("observation")
    '                xmlwriter.WriteAttributeString("classCode", "OBS")
    '                xmlwriter.WriteAttributeString("moodCode", "EVN")

    '                xmlwriter.WriteStartElement("templateId")
    '                xmlwriter.WriteAttributeString("root", "2.16.840.1.113883.10.20.1.37")
    '                xmlwriter.WriteEndElement() 'templateId element END

    '                xmlwriter.WriteStartElement("code")
    '                xmlwriter.WriteAttributeString("code", "33999-4")
    '                xmlwriter.WriteAttributeString("displayName", "Status")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.1")
    '                xmlwriter.WriteAttributeString("codeSystemName", "LOINC")
    '                xmlwriter.WriteEndElement() 'code element END

    '                xmlwriter.WriteStartElement("statusCode")
    '                xmlwriter.WriteAttributeString("code", "completed")
    '                xmlwriter.WriteEndElement() 'statusCode element END

    '                xmlwriter.WriteStartElement("value")
    '                xmlwriter.WriteAttributeString("code", "425392003")
    '                xmlwriter.WriteAttributeString("displayName", "Current and Verified")
    '                xmlwriter.WriteAttributeString("codeSystem", "2.16.840.1.113883.6.96")
    '                xmlwriter.WriteAttributeString("codeSystemName", "SNOMED CT")
    '                xmlwriter.WriteAttributeString("xsi:type", "CE")
    '                xmlwriter.WriteEndElement() 'value element END

    '                xmlwriter.WriteEndElement() 'observation element END

    '                xmlwriter.WriteEndElement() 'entryRelationship element END


    '                xmlwriter.WriteEndElement() 'observation element END

    '                xmlwriter.WriteEndElement() 'entry element END
    '            Next

    '            xmlwriter.WriteEndElement() 'section End``
    '            xmlwriter.WriteEndElement() 'component End
    '        End If
    '        ''$$$$$$$$$$$$$ Saagar K -- Component (ADVANCE DIRECTIVES) ---END---$$$$$$$$$$$$$$$$$$



    '        xmlwriter.WriteEndElement() '------------structuredBody Element Ends here
    '        xmlwriter.WriteEndElement() '############Parent component Element Ends here

    '        xmlwriter.WriteEndElement() 'End Clinical Document Element
    '        xmlwriter.WriteEndDocument()
    '        xmlwriter.Close()
    '        Return strfilepath
    '    Catch ex As gloCCDException
    '        Throw ex
    '    Catch ex As Exception
    '        Throw New gloCCDException(ex.ToString)

    '    Finally
    '        'Memory Leak
    '        If Not IsNothing(ogloCCDDBLayer) Then
    '            ogloCCDDBLayer.Dispose()
    '            ogloCCDDBLayer = Nothing
    '        End If

    '    End Try

    'End Function
End Class
