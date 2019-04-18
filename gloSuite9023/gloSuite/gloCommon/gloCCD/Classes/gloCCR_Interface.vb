Imports System.Xml
Imports System.IO
Imports System.Data.SqlClient
Imports gloPatient
Public Class gloCCR_Interface
    Implements IDisposable
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private mPatient As Patient
    Private strMessageLog As System.Text.StringBuilder
    Private mCCRtime As String = ""
    Private mPatientID As Int64
    Private sPatientActorID As String = ""
    Private sCCRVersion As String = ""
    Private sUserName As String = ""
    Private nLoginID As Int64 = 0
    Private _ClinicID As Int64 = 1
    Private _sFileType As String = ""
    ''Code Start - Add By Rohit on 20101008
    Private oPatient As gloPatient.Patient
    'Private oVitals As Vitals
    ' Private oProblem As Problems
    Private oInsurance As gloPatient.Insurance
    '  Private oGuarantor As gloPatient.PatientOtherContact
    Private oMedication As Medication
    '  Private oEncounters As Encounters
    Private oUser As User
    Private _sClinicName As String
    Private _sSourceName As String
    Private _sListName As String
    Private _nStatus As String
    Private _CCDID As Int64
    ' Private _bIsReconciled As String
    ' Private _bIsSkipped As String
    Dim _objCCDDatabaseLayer As gloCCDDatabaseLayer
    Private sTodayDate As String
    Dim _GeneratedLists As String = ""
    ''Code End - Add By Rohit on 20101008
    Dim _nModule As String
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

    Public Sub New()

    End Sub
    Public Property ClinicID() As Int64
        Get
            Return _ClinicID
        End Get
        Set(ByVal value As Int64)
            _ClinicID = value
        End Set
    End Property
    Public Property UserID() As Int64
        Get
            Return nLoginID
        End Get
        Set(ByVal value As Int64)
            nLoginID = value
        End Set
    End Property
    Public Property CCRVersion() As String
        Get
            Return sCCRVersion
        End Get
        Set(ByVal value As String)
            sCCRVersion = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return sUserName
        End Get
        Set(ByVal value As String)
            sUserName = value
        End Set
    End Property

    Public Property FileType() As String
        Get
            Return _sFileType
        End Get
        Set(ByVal value As String)
            _sFileType = value
        End Set
    End Property
    Public Property EffectiveTime() As String
        Get
            Return mCCRtime
        End Get
        Set(ByVal value As String)
            mCCRtime = value
        End Set
    End Property
    Public Property ClinicName() As String
        Get
            Return _sClinicName
        End Get
        Set(ByVal value As String)
            _sClinicName = value
        End Set
    End Property
  
    Public Property SourceName() As String
        Get
            Return _sSourceName
        End Get
        Set(ByVal value As String)
            _sSourceName = value
        End Set
    End Property
    Public Property ListName() As String
        Get
            Return _sListName
        End Get
        Set(ByVal value As String)
            _sListName = value
        End Set
    End Property
    Public Property Status() As Integer
        Get
            Return _nStatus
        End Get
        Set(ByVal value As Integer)
            _nStatus = value
        End Set
    End Property
    Public Property CCDID() As Int64
        Get
            Return _CCDID
        End Get
        Set(ByVal value As Int64)
            _CCDID = value
        End Set
    End Property
   
    'Public Property bIsSkipped() As Boolean
    '    Get
    '        Return _bIsSkipped
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _bIsSkipped = value
    '    End Set
    'End Property

    'Code Added by kanchan -In new function we done code changes while MU certification for new CCR standard
    Public Function MUOld_ExtractClinicalInformation() As Patient
        Dim areader As XmlReader
        Dim areaderchild As XmlReader = Nothing
        Dim count As Int32 = 0
        Try
            Dim oXMLSettings As New Xml.XmlReaderSettings()
            areader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
            Dim strnodevalue As String = ""
            While areader.Read

                If areader.NodeType = XmlNodeType.Element Then
                    Select Case areader.Name

                        Case "AdvanceDirectives"
                        Case "FunctionalStatus"
                        Case "Problems"
                        Case "FamilyHistory"
                        Case "SocialHistory"
                        Case "Medications"
                        Case "MedicalEquipment"
                        Case "Immunizations"
                        Case "VitalSigns"
                        Case "Results"
                        Case "Procedures"
                        Case "Encounters"
                        Case "PlanOfCare"
                            areader.Skip()
                            Continue While
                        Case "DateTime"
                            If count = 0 Then
                                areaderchild = areader.ReadSubtree
                                ReadDateTime(areaderchild)
                                count = count + 1
                            End If

                        Case "ccr:Patient"
                            areaderchild = areader.ReadSubtree
                            ReadPatient(areaderchild)

                        Case "ccr:Actors"
                            mPatient = New Patient
                            'Read patient information using xmlDocument object
                            areaderchild = areader.ReadSubtree
                            If Not (ReadPatientDetails(areaderchild)) Then
                                'false- some data is missing
                                'gloCCDDatabaseLayer.InserttoCCDMessagelog()
                                'Exit Function
                            End If

                            'Case "documentationOf"
                            '    areaderchild = areader.ReadSubtree
                            '    'If Not (ReadPatientProviders(areaderchild)) Then
                            '    '    gloCCDDatabaseLayer.InserttoCCDMessagelog()
                            '    '    Exit Function
                            '    'End If
                            'Case "section"
                            '    areaderchild = areader.ReadSubtree
                            '    ReadComponent(areaderchild)
                            'Case "participant"
                            '    areaderchild = areader.ReadSubtree
                            '    ReadPatientSupport(areaderchild)
                    End Select
                    'areader.ReadToFollowing("Section")
                    'areader.Skip()


                End If

            End While

            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If
            If Not IsNothing(areaderchild) Then
                areaderchild.Close()
                areaderchild = Nothing
            End If

            'Dim strResult As String = ""


            If Not IsNothing(mPatient) Then
                Return mPatient
            Else
                Return Nothing
            End If
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)

        Finally

        End Try

    End Function


    '--- *** Read Clinic name from CCR file for reconcilation process **---
    Public Function ReadClinicNameCCR() As String
        Dim areader As XmlReader = Nothing
        Dim areaderchild As XmlReader = Nothing
        areader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
        Dim areaderchild1 As XmlReader = Nothing

        'Dim objMessagelogdetails As CCDMessageLogDetail
        Dim ClinicName As String = ""
        Try

            While areader.Read
                If areader.NodeType = XmlNodeType.Element Then
                    If areader.Name.ToLower() = "ccr:actor" Then
                        areaderchild = areader.ReadSubtree

                        While areaderchild.Read
                            If areader.NodeType = XmlNodeType.Element Then
                                If areader.Name.ToLower() = "ccr:organization" Then
                                    areaderchild1 = areaderchild.ReadSubtree
                                    While areaderchild1.Read
                                        If areaderchild1.NodeType = XmlNodeType.Element Then
                                            If areaderchild1.Name.ToLower() = "ccr:name" Then
                                                ClinicName = areaderchild1.ReadInnerXml()
                                                Exit While
                                            End If
                                        End If
                                    End While

                                End If

                            End If
                        End While

                    End If
                End If
            End While

            Return ClinicName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(areader) Then
                areader.Close()
            End If
            If Not IsNothing(areaderchild1) Then
                areaderchild1.Close()
            End If
            If Not IsNothing(areaderchild) Then
                areaderchild.Close()
            End If
        End Try

    End Function



    Public Function ReadCreatedDateCCR() As String
        Dim areader As XmlReader
        '    Dim areaderchild As XmlReader
        Dim CreatedDate As String = ""
        Try
            'Dim oXMLSettings As New Xml.XmlReaderSettings()
            areader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
            Dim strnodevalue As String = ""
            While areader.Read
                If areader.NodeType = XmlNodeType.Element Then
                    If areader.Name = "ccr:ExactDateTime" Then
                        'areaderchild = areader.ReadSubtree
                        CreatedDate = areader.ReadString.ToString()
                        Exit While

                    End If
                End If
            End While

            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If

            Return CreatedDate

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally

        End Try

    End Function



    Private Function ReadDateTime(ByVal xreader As XmlReader) As String
        Try
            'Dim count As Int32 = 0
            While xreader.Read
                'If count = 0 Then
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "ExactDateTime"
                            Dim _Date As String = xreader.ReadInnerXml()
                            mCCRtime = ConvertCCRDateTime(_Date)
                            ' count = count + 1
                            'Case "AdvanceDirectives"
                            'Case "FunctionalStatus"
                            'Case "Problems"
                            'Case "FamilyHistory"
                            'Case "SocialHistory"
                            'Case "Medications"
                            'Case "MedicalEquipment"
                            'Case "Immunizations"
                            'Case "VitalSigns"
                            'Case "Results"
                            'Case "Procedures"
                            'Case "Encounters"
                            'Case "PlanOfCare"
                            'Case "Actors"
                            '    xreader.Skip()
                    End Select
                End If

                'End If
            End While
            Return mCCRtime
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        End Try
        'Return Nothing
    End Function

    Private Function ReadPatient(ByVal xreader As XmlReader) As Boolean
        Try
            While xreader.Read
                If xreader.NodeType = XmlNodeType.Element Then
                    Select Case xreader.Name
                        Case "ActorID"
                            sPatientActorID = xreader.ReadInnerXml()
                    End Select
                End If
            End While
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
        Return Nothing
    End Function
    'Code Added by kanchan -In new function we done code changes while MU certification for new CCR standard
    Private Function ReadPatientDetails(ByVal areader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderPerson As XmlReader
        Dim areaderchild1 As XmlReader
        Dim areaderchild2 As XmlReader
        Dim areaderchild3 As XmlReader
        Dim _ActorID As String = ""
        Dim objMessagelogdetails As CCDMessageLogDetail = Nothing
        Try
            objMessagelogdetails = New CCDMessageLogDetail

            If Not IsNothing(areader) Then
                While areader.Read
                    If areader.NodeType = XmlNodeType.Element Then
                        Select Case areader.Name
                            Case "ccr:Actor"
                                areaderchild = areader.ReadSubtree

                                While areaderchild.Read
                                    If areaderchild.NodeType = XmlNodeType.Element Then
                                        Select Case areaderchild.Name
                                            Case "ccr:ActorObjectID"
                                                _ActorID = areaderchild.ReadInnerXml
                                                If Not IsNothing(sPatientActorID) AndAlso Not IsNothing(_ActorID) Then
                                                    If sPatientActorID.Trim <> _ActorID.Trim Then
                                                        Continue While
                                                    End If
                                                End If

                                            Case "ccr:Person"
                                                'If sPatientActorID <> _ActorID Then
                                                '    Continue While
                                                'End If
                                                If _ActorID <> "PatientID_01" Then
                                                    Continue While
                                                End If
                                                areaderPerson = areaderchild.ReadSubtree
                                                While areaderPerson.Read
                                                    If areaderPerson.NodeType = XmlNodeType.Element Then
                                                        Select Case areaderPerson.Name
                                                            Case "ccr:BirthName"
                                                                areaderchild1 = areaderPerson.ReadSubtree
                                                                While areaderchild1.Read
                                                                    If areaderchild1.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild1.Name = "ccr:Given" Then
                                                                            mPatient.PatientName.FirstName = areaderchild1.ReadInnerXml
                                                                            If mPatient.PatientName.FirstName = "" Then
                                                                                strMessageLog.Append("Patient: given (FirstName) value not present")
                                                                            End If
                                                                        End If
                                                                        If areaderchild1.Name = "ccr:Family" Then
                                                                            mPatient.PatientName.LastName = areaderchild1.ReadInnerXml
                                                                            If mPatient.PatientName.LastName = "" Then
                                                                                strMessageLog.Append("Patient: Family (LastName) value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While
                                                            Case "ccr:DateOfBirth"
                                                                areaderchild2 = areaderPerson.ReadSubtree
                                                                While areaderchild2.Read
                                                                    If areaderchild2.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild2.Name = "ccr:ExactDateTime" Then
                                                                            mPatient.DateofBirth = areaderchild2.ReadInnerXml
                                                                            mPatient.DateofBirth = ConvertCCRDateTime(mPatient.DateofBirth)
                                                                            If mPatient.DateofBirth = "" Then
                                                                                strMessageLog.Append("Patient: DateofBirth value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While
                                                            Case "ccr:Gender"
                                                                areaderchild3 = areaderPerson.ReadSubtree
                                                                While areaderchild3.Read
                                                                    If areaderchild3.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild3.Name = "ccr:Text" Then
                                                                            mPatient.Gender = areaderchild3.ReadInnerXml
                                                                            If mPatient.Gender <> "" Then
                                                                                If mPatient.Gender.Trim.ToUpper() = "F" Or mPatient.Gender.Trim.ToUpper() = "FEMALE" Then
                                                                                    mPatient.Gender = "Female"
                                                                                ElseIf mPatient.Gender.Trim.ToUpper() = "M" Or mPatient.Gender.Trim.ToUpper() = "MALE" Then
                                                                                    mPatient.Gender = "Male"
                                                                                Else
                                                                                    mPatient.Gender = "Other"
                                                                                End If
                                                                            Else
                                                                                strMessageLog.Append("Patient: Gender value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While

                                                        End Select
                                                    End If
                                                End While

                                                'Case "id" 'R
                                                '    mPatient.PatientName.Code = areaderchild.GetAttribute("extension")

                                                'Case "streetAddressLine" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Street = areaderchild.ReadInnerXml
                                                'Case "city" 'O
                                                '    mPatient.PatientName.PersonContactAddress.City = areaderchild.ReadInnerXml
                                                'Case "state" 'O
                                                '    mPatient.PatientName.PersonContactAddress.State = areaderchild.ReadInnerXml
                                                'Case "postalCode" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Zip = areaderchild.ReadInnerXml
                                                'Case "country" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Country = areaderchild.ReadInnerXml
                                                'Case "prefix" 'O
                                                '    mPatient.PatientName.Prefix = areaderchild.ReadInnerXml
                                                'Case "suffix" 'O
                                                '    mPatient.PatientName.Suffix = areaderchild.ReadInnerXml
                                                '    'Code Start-added by kanchan on 20100604 to avoid guardian details
                                                'Case "guardian"
                                                '    areaderchild.Skip()
                                                'Case "maritalStatusCode" '
                                                '    mPatient.MaritalStatus = areaderchild.GetAttribute("displayName")
                                                'Case "religiousAffiliationCode"
                                                '    mPatient.ReligiousAffiliationCode = areaderchild.GetAttribute("displayName")
                                                'Case "ethnicGroupCode"
                                                '    mPatient.ethnicGroupCode = areaderchild.GetAttribute("displayName")
                                                'Case "raceCode"
                                                '    mPatient.RaceCode = areaderchild.GetAttribute("displayName")
                                                'Case "telecom"
                                                '    Select Case areaderchild.GetAttribute("use")

                                                '        Case "HP"
                                                '            mPatient.PatientName.PersonContactPhone.Phone = areaderchild.GetAttribute("value")
                                                '        Case "MC"
                                                '            mPatient.PatientName.PersonContactPhone.Mobile = areaderchild.GetAttribute("value")
                                                '        Case "WP"
                                                '            mPatient.PatientName.PersonContactPhone.WorkPhone = areaderchild.GetAttribute("value")
                                                '        Case "HV"
                                                '            mPatient.PatientName.PersonContactPhone.VacationPhone = areaderchild.GetAttribute("value")
                                                '        Case Else
                                                '            If mPatient.PatientName.PersonContactPhone.Email <> "" Then
                                                '                mPatient.PatientName.PersonContactPhone.URL = areaderchild.GetAttribute("value")
                                                '            Else
                                                '                mPatient.PatientName.PersonContactPhone.Email = areaderchild.GetAttribute("value")
                                                '            End If
                                                '    End Select
                                                'Case "country"
                                                '    mPatient.PatientName.PersonContactAddress.Country = areaderchild.GetAttribute("displayName")

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
            Return False
        Finally
            areaderchild = Nothing
            areaderPerson = Nothing
            areaderchild1 = Nothing
            areaderchild2 = Nothing
            areaderchild3 = Nothing
            _ActorID = Nothing
            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If

        End Try
    End Function

    Private Function ConvertCCRDateTime(ByVal _Date As String) As DateTime
        Dim CurrentDate As DateTime = Now
        Try
            'Dim ret As String = strIN
            'DateString = ""
            'If Len(strIN) < 8 Then Exit Function
            'ret = Right$(ret, 2) & " " & MonthName(CLng(Mid$(ret, 5, 2)), True) & " " & Left$(ret, 4)
            'DateString = Format(CDate(ret), "MM/dd/yyyy")

            '// REMARK - We need to convert date as per various date time stampe mthod

            If Len(_Date) < 10 Then
                ConvertCCRDateTime = Nothing
                Exit Function
            End If

            'Dim temp As String() = _Date.Split("+")
            'If Not IsNothing(temp) Then
            '    If temp.Length > 0 Then
            '        _Date = temp(0).Replace("T", " ")
            '        CurrentDate = CType(_Date, DateTime)
            '    End If
            'End If
            '
            Dim _DateAsString As String = Mid(_Date, 1, 10)

            'Date Format: 1932-09-24
            CurrentDate = DateSerial(Mid(_DateAsString, 1, 4), Val(Mid(_DateAsString, 6, 2)), Val(Mid(_DateAsString, 9, 2)))
            If IsDate(_Date) = False Then
                CurrentDate = "12:00:00 AM"
            End If
            _DateAsString = Nothing

            Return CurrentDate
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        End Try
    End Function

    'Code Added by kanchan on 20100925 for maintaing our old standarad of CCR,
    'In new function we done code changes while MU certification for new CCR standard
    Public Function CCHITOld_ExtractClinicalInformation() As Patient
        Dim areader As XmlReader
        Dim areaderchild As XmlReader = Nothing
        Dim count As Int32 = 0
        Try
            'Dim oXMLSettings As New Xml.XmlReaderSettings()
            areader = XmlReader.Create(gloLibCCDGeneral.CCDFilePath)
            'Dim strnodevalue As String = ""
            While areader.Read

                If areader.NodeType = XmlNodeType.Element Then
                    Select Case areader.Name

                        Case "AdvanceDirectives"
                        Case "FunctionalStatus"
                        Case "Problems"
                        Case "FamilyHistory"
                        Case "SocialHistory"
                        Case "Medications"
                        Case "MedicalEquipment"
                        Case "Immunizations"
                        Case "VitalSigns"
                        Case "Results"
                        Case "Procedures"
                        Case "Encounters"
                        Case "PlanOfCare"
                            areader.Skip()
                            Continue While
                        Case "DateTime"
                            If count = 0 Then
                                areaderchild = areader.ReadSubtree
                                ReadDateTime(areaderchild)
                                count = count + 1
                            End If

                        Case "Patient"
                            areaderchild = areader.ReadSubtree
                            ReadPatient(areaderchild)

                        Case "Actors"
                            mPatient = New Patient
                            'Read patient information using xmlDocument object
                            areaderchild = areader.ReadSubtree
                            If Not (ReadPatientDetails(areaderchild)) Then
                                'false- some data is missing
                                'gloCCDDatabaseLayer.InserttoCCDMessagelog()
                                'Exit Function
                            End If

                            'Case "documentationOf"
                            '    areaderchild = areader.ReadSubtree
                            '    'If Not (ReadPatientProviders(areaderchild)) Then
                            '    '    gloCCDDatabaseLayer.InserttoCCDMessagelog()
                            '    '    Exit Function
                            '    'End If
                            'Case "section"
                            '    areaderchild = areader.ReadSubtree
                            '    ReadComponent(areaderchild)
                            'Case "participant"
                            '    areaderchild = areader.ReadSubtree
                            '    ReadPatientSupport(areaderchild)
                    End Select
                    'areader.ReadToFollowing("Section")
                    'areader.Skip()


                End If

            End While

            If Not IsNothing(areader) Then
                areader.Close()
                areader = Nothing
            End If
            If Not IsNothing(areaderchild) Then
                areaderchild.Close()
                areaderchild = Nothing
            End If

            'Dim strResult As String = ""


            If Not IsNothing(mPatient) Then
                Return mPatient
            Else
                Return Nothing
            End If
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)

        Finally

        End Try

    End Function
    Private Function Old_ReadPatientDetails(ByVal areader As XmlReader) As Boolean
        Dim areaderchild As XmlReader
        Dim areaderPerson As XmlReader
        Dim areaderchild1 As XmlReader
        Dim areaderchild2 As XmlReader
        Dim areaderchild3 As XmlReader
        Dim _ActorID As String = ""
        Dim objMessagelogdetails As CCDMessageLogDetail = Nothing
        Try
            objMessagelogdetails = New CCDMessageLogDetail

            If Not IsNothing(areader) Then
                While areader.Read
                    If areader.NodeType = XmlNodeType.Element Then
                        Select Case areader.Name
                            Case "Actor"
                                areaderchild = areader.ReadSubtree

                                While areaderchild.Read
                                    If areaderchild.NodeType = XmlNodeType.Element Then
                                        Select Case areaderchild.Name
                                            Case "ActorObjectID"
                                                _ActorID = areaderchild.ReadInnerXml
                                                If Not IsNothing(sPatientActorID) AndAlso Not IsNothing(_ActorID) Then
                                                    If sPatientActorID.Trim <> _ActorID.Trim Then
                                                        Continue While
                                                    End If
                                                End If

                                            Case "Person"
                                                If sPatientActorID <> _ActorID Then
                                                    Continue While
                                                End If
                                                areaderPerson = areaderchild.ReadSubtree
                                                While areaderPerson.Read
                                                    If areaderPerson.NodeType = XmlNodeType.Element Then
                                                        Select Case areaderPerson.Name
                                                            Case "CurrentName"
                                                                areaderchild1 = areaderPerson.ReadSubtree
                                                                While areaderchild1.Read
                                                                    If areaderchild1.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild1.Name = "Given" Then
                                                                            mPatient.PatientName.FirstName = areaderchild1.ReadInnerXml
                                                                            If mPatient.PatientName.FirstName = "" Then
                                                                                strMessageLog.Append("Patient: given (FirstName) value not present")
                                                                            End If
                                                                        End If
                                                                        If areaderchild1.Name = "Family" Then
                                                                            mPatient.PatientName.LastName = areaderchild1.ReadInnerXml
                                                                            If mPatient.PatientName.LastName = "" Then
                                                                                strMessageLog.Append("Patient: Family (LastName) value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While
                                                            Case "DateOfBirth"
                                                                areaderchild2 = areaderPerson.ReadSubtree
                                                                While areaderchild2.Read
                                                                    If areaderchild2.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild2.Name = "ExactDateTime" Then
                                                                            mPatient.DateofBirth = areaderchild2.ReadInnerXml
                                                                            mPatient.DateofBirth = ConvertCCRDateTime(mPatient.DateofBirth)
                                                                            If mPatient.DateofBirth = "" Then
                                                                                strMessageLog.Append("Patient: DateofBirth value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While
                                                            Case "Gender"
                                                                areaderchild3 = areaderPerson.ReadSubtree
                                                                While areaderchild3.Read
                                                                    If areaderchild3.NodeType = XmlNodeType.Element Then
                                                                        If areaderchild3.Name = "Text" Then
                                                                            mPatient.Gender = areaderchild3.ReadInnerXml
                                                                            If mPatient.Gender <> "" Then
                                                                                If mPatient.Gender.Trim.ToUpper() = "F" Or mPatient.Gender.Trim.ToUpper() = "FEMALE" Then
                                                                                    mPatient.Gender = "Female"
                                                                                ElseIf mPatient.Gender.Trim.ToUpper() = "M" Or mPatient.Gender.Trim.ToUpper() = "MALE" Then
                                                                                    mPatient.Gender = "Male"
                                                                                Else
                                                                                    mPatient.Gender = "Other"
                                                                                End If
                                                                            Else
                                                                                strMessageLog.Append("Patient: Gender value not present")
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End While

                                                        End Select
                                                    End If
                                                End While

                                                'Case "id" 'R
                                                '    mPatient.PatientName.Code = areaderchild.GetAttribute("extension")

                                                'Case "streetAddressLine" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Street = areaderchild.ReadInnerXml
                                                'Case "city" 'O
                                                '    mPatient.PatientName.PersonContactAddress.City = areaderchild.ReadInnerXml
                                                'Case "state" 'O
                                                '    mPatient.PatientName.PersonContactAddress.State = areaderchild.ReadInnerXml
                                                'Case "postalCode" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Zip = areaderchild.ReadInnerXml
                                                'Case "country" 'O
                                                '    mPatient.PatientName.PersonContactAddress.Country = areaderchild.ReadInnerXml
                                                'Case "prefix" 'O
                                                '    mPatient.PatientName.Prefix = areaderchild.ReadInnerXml
                                                'Case "suffix" 'O
                                                '    mPatient.PatientName.Suffix = areaderchild.ReadInnerXml
                                                '    'Code Start-added by kanchan on 20100604 to avoid guardian details
                                                'Case "guardian"
                                                '    areaderchild.Skip()
                                                'Case "maritalStatusCode" '
                                                '    mPatient.MaritalStatus = areaderchild.GetAttribute("displayName")
                                                'Case "religiousAffiliationCode"
                                                '    mPatient.ReligiousAffiliationCode = areaderchild.GetAttribute("displayName")
                                                'Case "ethnicGroupCode"
                                                '    mPatient.ethnicGroupCode = areaderchild.GetAttribute("displayName")
                                                'Case "raceCode"
                                                '    mPatient.RaceCode = areaderchild.GetAttribute("displayName")
                                                'Case "telecom"
                                                '    Select Case areaderchild.GetAttribute("use")

                                                '        Case "HP"
                                                '            mPatient.PatientName.PersonContactPhone.Phone = areaderchild.GetAttribute("value")
                                                '        Case "MC"
                                                '            mPatient.PatientName.PersonContactPhone.Mobile = areaderchild.GetAttribute("value")
                                                '        Case "WP"
                                                '            mPatient.PatientName.PersonContactPhone.WorkPhone = areaderchild.GetAttribute("value")
                                                '        Case "HV"
                                                '            mPatient.PatientName.PersonContactPhone.VacationPhone = areaderchild.GetAttribute("value")
                                                '        Case Else
                                                '            If mPatient.PatientName.PersonContactPhone.Email <> "" Then
                                                '                mPatient.PatientName.PersonContactPhone.URL = areaderchild.GetAttribute("value")
                                                '            Else
                                                '                mPatient.PatientName.PersonContactPhone.Email = areaderchild.GetAttribute("value")
                                                '            End If
                                                '    End Select
                                                'Case "country"
                                                '    mPatient.PatientName.PersonContactAddress.Country = areaderchild.GetAttribute("displayName")

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
            areaderchild = Nothing
            areaderPerson = Nothing
            areaderchild1 = Nothing
            areaderchild2 = Nothing
            areaderchild3 = Nothing
            _ActorID = Nothing
            If Not IsNothing(objMessagelogdetails) Then
                objMessagelogdetails.Dispose()
                objMessagelogdetails = Nothing
            End If

        End Try
    End Function

    Public Function GetCategary() As DataTable
        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring

        Dim cmd As SqlCommand = Nothing
        'Dim cnn As New SqlConnection(_Connectionstring)
        cnn.Open()
        Dim _strSQL As String = ""
        Dim sqlAdpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text

            _strSQL = "select distinct (sCategoryName) from CCD_gloDataDictionary"
            cmd.CommandText = _strSQL
            sqlAdpt.SelectCommand = cmd
            sqlAdpt.Fill(dt)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(sqlAdpt) Then
                sqlAdpt.Dispose()
                sqlAdpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try

    End Function
    Public Function GetCategoryData(ByVal strsegment As String) As DataTable

        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring

        Dim cmd As SqlCommand = Nothing
        'Dim cnn As New SqlConnection(_Connectionstring)
        cnn.Open()
        Dim _strSQL As String = ""
        Dim sqlAdpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text

            _strSQL = "select sTableName + '.'+ sFieldName, sDisplayName, sDataType from CCD_gloDataDictionary where sCategoryName='" & strsegment & "'"
            cmd.CommandText = _strSQL
            sqlAdpt.SelectCommand = cmd
            sqlAdpt.Fill(dt)

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(sqlAdpt) Then
                sqlAdpt.Dispose()
                sqlAdpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try


    End Function

    Public Function InsertUserMappingField(ByVal arry As ArrayList) As Boolean

        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
        Dim sqlparm As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _Id As Int64 = 0
        Dim _strSQL As String = ""
        Try
            If cnn.State = ConnectionState.Closed Then
                cnn.Open()
            End If

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "SELECT nCCRMSTID from CCR_Mapping_MSt where sVersionNo='" & sCCRVersion & "'"
            cmd.CommandText = _strSQL
            _Id = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If _Id <> 0 Then
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                _strSQL = "DELETE FROM CCR_Mapping_DTL where nCCRMSTID=" & _Id & ""
                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            Else
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gsp_GetUniqueID"

                'sqlparm = New SqlParameter()
                sqlparm = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                sqlparm.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                _Id = cmd.Parameters("@ID").Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                _strSQL = "INSERT INTO CCR_Mapping_MST(nCCRMSTID,sVersionNo,nUserID,nClinicID,sCretedBy,dtCreatedOn,sModifiedBy,dtModifiedOn,sMachineName)" _
                & " values(" & _Id & ",1.0," & nLoginID & "," & _ClinicID & ",'" & sUserName & "','" & Now & "','" & sUserName & "','" & Now & "','" & System.Environment.MachineName & "')"
                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Dispose()

            Dim obj As ClsUserMappingField

            For i As Int32 = 0 To arry.Count - 1

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gsp_GetUniqueID"

                'sqlparm = New SqlParameter()
                sqlparm = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                sqlparm.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                Dim _DTLId As Int64 = cmd.Parameters("@ID").Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                obj = CType(arry.Item(i), ClsUserMappingField)
                _strSQL = "INSERT INTO CCR_Mapping_DTL (nCCRMSTID,nCCRDTLID,sModuleName,sXMLElementPath,sXMLAttributeName,gloEMRFieldName,gloEMRDisplayName,sSingleMultipleEntity,sNodeType,sReferencePath,sReferenceValue)" _
                & " values( " & _Id & "," & _DTLId & ",'" & obj.gloEMRModuleName & "','" & obj.OtherFieldName & "','" & obj.EventName & "','" & obj.gloEMRFieldName & "','" & obj.gloEMRDisplayName & "','" & obj.ReferenceNode & "','" & obj.NodeType & "','" & obj.ReferenceNodePath & "','" & obj.ReferenceNodeValue & "')"

                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                obj = Nothing
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            Next
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlparm) Then
                sqlparm = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Public Function InsertUserCCDMappingField(ByVal arry As ArrayList) As Boolean

        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
        Dim sqlparm As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _Id As Int64 = 0
        Dim _strSQL As String = ""
        Try
            If cnn.State = ConnectionState.Closed Then
                cnn.Open()
            End If

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "SELECT nCCRMSTID from CCD_Mapping_MSt where sVersionNo='" & sCCRVersion & "'"
            cmd.CommandText = _strSQL
            _Id = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If _Id <> 0 Then
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                _strSQL = "DELETE FROM CCD_Mapping_DTL where nCCRMSTID=" & _Id & ""
                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            Else
                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gsp_GetUniqueID"

                '  sqlparm = New SqlParameter()
                sqlparm = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                sqlparm.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                _Id = cmd.Parameters("@ID").Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                _strSQL = "INSERT INTO CCD_Mapping_MST(nCCRMSTID,sVersionNo,nUserID,nClinicID,sCretedBy,dtCreatedOn,sModifiedBy,dtModifiedOn,sMachineName)" _
                & " values(" & _Id & ",1.0," & nLoginID & "," & _ClinicID & ",'" & sUserName & "','" & Now & "','" & sUserName & "','" & Now & "','" & System.Environment.MachineName & "')"
                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Dispose()
            Dim obj As ClsUserMappingField

            For i As Int32 = 0 To arry.Count - 1

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gsp_GetUniqueID"

                '  sqlparm = New SqlParameter()
                sqlparm = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                sqlparm.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                Dim _DTLId As Int64 = cmd.Parameters("@ID").Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text
                obj = CType(arry.Item(i), ClsUserMappingField)
                _strSQL = "INSERT INTO CCD_Mapping_DTL (nCCRMSTID,nCCRDTLID,sModuleName,sXMLElementPath,sXMLAttributeName,gloEMRFieldName,gloEMRDisplayName,sSingleMultipleEntity,sNodeType,sReferencePath,sReferenceValue)" _
                & " values( " & _Id & "," & _DTLId & ",'" & obj.gloEMRModuleName & "','" & obj.OtherFieldName & "','" & obj.EventName & "','" & obj.gloEMRFieldName & "','" & obj.gloEMRDisplayName & "','" & obj.ReferenceNode & "','" & obj.NodeType & "','" & obj.ReferenceNodePath & "','" & obj.ReferenceNodeValue & "')"

                cmd.CommandText = _strSQL
                cmd.ExecuteNonQuery()
                obj = Nothing
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            Next
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlparm) Then
                sqlparm = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Public Function DefaultMappingRestore(ByVal sCCRVer As String, ByVal _nModuleName As String) As DataTable

        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
        Dim cmd As SqlCommand = Nothing
        If cnn.State = ConnectionState.Closed Then cnn.Open()
        Dim _MStID As Int64
        Dim _strSQL As String = ""
        Dim sqlAdpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text

            _strSQL = "SELECT nCCRMSTID from CCR_Mapping_MSt where sVersionNo='" & sCCRVer & "' "

            cmd.CommandText = _strSQL
            _MStID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If (_MStID > 0) Then

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text

                If _nModuleName = "" Then
                    '    _strSQL = "SELECT * from CCR_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                    '& " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"

                    _strSQL = "SELECT  nCCRMSTID, nCCRDTLID, sModuleName, sXMLElementPath, sXMLAttributeName, gloEMRFieldName, gloEMRDisplayName, sSingleMultipleEntity, " _
                             & " sNodeType, sReferencePath, sReferenceValue, nMappingID, sTableName, sFieldName, sDisplayName, sCategoryName, sDataType " _
                             & " from CCR_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                              & " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"
                Else
                    '    _strSQL = "SELECT * from CCR_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                    '& " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and sModuleName='" & _nModuleName & "' and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"

                    _strSQL = "SELECT  nCCRMSTID, nCCRDTLID, sModuleName, sXMLElementPath, sXMLAttributeName, gloEMRFieldName, gloEMRDisplayName, sSingleMultipleEntity, " _
                              & " sNodeType, sReferencePath, sReferenceValue, nMappingID, sTableName, sFieldName, sDisplayName, sCategoryName, sDataType " _
                              & " from CCR_Mapping_DTL c left outer join CCD_gloDataDictionary DD " _
                              & " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and sModuleName='" & _nModuleName & "' and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"

                End If

                cmd.CommandText = _strSQL
                sqlAdpt.SelectCommand = cmd
                sqlAdpt.Fill(dt)

            End If
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(sqlAdpt) Then
                sqlAdpt.Dispose()
                sqlAdpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Public Function DefaultCCDMappingRestore(ByVal sCCRVer As String, ByVal _nModuleName As String) As DataTable

        Dim cnn As New SqlConnection()
        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
        Dim cmd As SqlCommand = Nothing
        If cnn.State = ConnectionState.Closed Then cnn.Open()
        Dim _MStID As Int64
        Dim _strSQL As String = ""
        Dim sqlAdpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text

            _strSQL = "SELECT nCCRMSTID from CCD_Mapping_MSt where sVersionNo='" & sCCRVer & "' "

            cmd.CommandText = _strSQL
            _MStID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If (_MStID > 0) Then

                cmd = New SqlCommand
                cmd.Connection = cnn
                cmd.CommandType = CommandType.Text

                If _nModuleName = "" Then
                    '    _strSQL = "SELECT * from CCD_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                    '& " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"
                    _strSQL = "SELECT  nCCRMSTID, nCCRDTLID, sModuleName, sXMLElementPath, sXMLAttributeName, gloEMRFieldName, gloEMRDisplayName, sSingleMultipleEntity, " _
                             & " sNodeType, sReferencePath, sReferenceValue, nMappingID, sTableName, sFieldName, sDisplayName, sCategoryName, sDataType " _
                            & " from CCD_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                             & " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"
                Else
                    '    _strSQL = "SELECT * from CCD_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                    '& " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and sModuleName='" & _nModuleName & "' and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"
                    _strSQL = "SELECT  nCCRMSTID, nCCRDTLID, sModuleName, sXMLElementPath, sXMLAttributeName, gloEMRFieldName, gloEMRDisplayName, sSingleMultipleEntity, " _
                             & " sNodeType, sReferencePath, sReferenceValue, nMappingID, sTableName, sFieldName, sDisplayName, sCategoryName, sDataType " _
                             & " from CCD_Mapping_DTL c left outer join CCD_gloDataDictionary DD" _
                             & " on c.gloEMRFieldName=DD.sTableName + '.' + sFieldName where nCCRMSTID = " & _MStID & " and sModuleName='" & _nModuleName & "' and c.gloEMRDisplayName=DD.sDisplayName and c.sModuleName=DD.sCategoryName ORDER BY nMappingID ASC"

                End If

                cmd.CommandText = _strSQL
                sqlAdpt.SelectCommand = cmd
                sqlAdpt.Fill(dt)

            End If
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(sqlAdpt) Then
                sqlAdpt.Dispose()
                sqlAdpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Private Function FetchNodeValue(ByVal NodePath As String) As ArrayList
        Dim _documentprefix As String = ""
        Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        Dim _nodeKeyValue As New Collection
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim _Node As XmlNodeList = Nothing
        Try
            doc.Load(gloLibCCDGeneral.CCDFilePath)

            nsmgr = New XmlNamespaceManager(doc.NameTable)
            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(NodePath, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(NodePath, "")
            End If

            'Dim _Node As XmlNode = doc.SelectSingleNode(_xPath, nsmgr)
            '_NodeValue = _Node.InnerXml

            _Node = doc.SelectNodes(_xPath, nsmgr)



            Dim _singleNode As XmlNode = doc.SelectSingleNode(_xPath, nsmgr)
            If Not IsNothing(_Node) Then
                If _Node.Count > 0 Then
                    For i As Int32 = 0 To _Node.Count - 1

                        _nodeKeyValue.Add(_Node(i).InnerXml.ToString(), _Node(i).ParentNode.GetHashCode())
                        _NodeValue.Add(_Node(i).InnerXml.ToString())

                    Next
                Else
                    _NodeValue = Nothing
                End If
            Else
                _NodeValue = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            _xPath = Nothing
            _Node = Nothing
            doc = Nothing
            nsmgr = Nothing
        End Try

        Return _NodeValue
    End Function


    Private Function FetchNodeValue(ByVal NodePath As String, ByVal NodeType As String, ByVal NodeName As String) As ArrayList
        Dim _documentprefix As String = ""
        Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        Dim nsmgr As XmlNamespaceManager
        Dim _Node As XmlNodeList
        Try
            doc.Load(gloLibCCDGeneral.CCDFilePath)

            nsmgr = New XmlNamespaceManager(doc.NameTable)
            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(NodePath, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(NodePath, "")
            End If

            'Dim _Node As XmlNode = doc.SelectSingleNode(_xPath, nsmgr)
            '_NodeValue = _Node.InnerXml


            If NodeType = "Element" Then
                _Node = doc.SelectNodes(_xPath, nsmgr)
                If Not IsNothing(_Node) Then
                    If _Node.Count > 0 Then
                        For i As Int32 = 0 To _Node.Count - 1
                            _NodeValue.Add(_Node(i).InnerXml.ToString())
                        Next
                    Else
                        _NodeValue = Nothing
                    End If
                Else
                    _NodeValue = Nothing
                End If
            ElseIf NodeType = "Attribute" Then
                'Dim index As Int32 = _xPath.LastIndexOf("/")
                '_xPath = _xPath.Remove(index)
                _Node = doc.SelectNodes(_xPath, nsmgr)
                If Not IsNothing(_Node) Then
                    If _Node.Count > 0 Then
                        For i As Int32 = 0 To _Node.Count - 1
                            _NodeValue.Add(_Node(i).Attributes(NodeName).InnerText.ToString())
                        Next
                    Else
                        _NodeValue = Nothing
                    End If
                Else
                    _NodeValue = Nothing
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            _xPath = Nothing
            doc = Nothing
            nsmgr = Nothing
            _Node = Nothing
        End Try
        Return _NodeValue
    End Function

    ''' <summary>
    ''' Fetch Data from CCR file with reference value
    ''' </summary>
    ''' <param name="ParentNodePath"></param>
    ''' <param name="NodeName"></param>
    ''' <param name="RefNodeName"></param>
    ''' <param name="NodeType"></param>
    ''' <returns>Node Data and Reference node data</returns>
    ''' <remarks></remarks>
    ''' 

    Private Function FetchNodeValue(ByVal ParentNodePath As String, ByVal NodeName As String, ByVal RefNodeName As String, ByVal NodeType As String) As ArrayList
        Dim _documentprefix As String = ""
        'Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        doc.Load(gloLibCCDGeneral.CCDFilePath)
        Dim nsmgr As New XmlNamespaceManager(doc.NameTable)
        Dim ModuleAdd As String = ""
        Dim NodeAddress As String = ""
        Dim _Node As XmlNodeList
        Dim _NodeData As XmlNodeList
        Dim nodevalue As String
        Dim refnodevalue As String
        NodeAddress = ParentNodePath.Substring(ParentNodePath.IndexOf("Body\") + 5)
        ModuleAdd = ParentNodePath.Substring(0, ParentNodePath.IndexOf("Body\") + 5 + NodeAddress.IndexOf("\"))
        NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf("\") + 1)
        ModuleAdd = ModuleAdd + "\" + NodeAddress.Substring(0, NodeAddress.IndexOf("\"))
        If (NodeAddress.Contains("\")) Then
            NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf("\") + 1)
        End If

        Try

            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                ModuleAdd = GetPrefixedXPath(ModuleAdd, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                ModuleAdd = GetPrefixedXPath(ModuleAdd, "")
            End If
            _Node = doc.SelectNodes(ModuleAdd, nsmgr)
            If Not IsNothing(_Node) Then
                If _Node.Count > 0 Then
                    For i As Int32 = 0 To _Node.Count - 1
                        nodevalue = ""
                        refnodevalue = ""
                        If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                            _documentprefix = "CR"
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            NodeAddress = GetPrefixedXPath(NodeAddress, _documentprefix)
                        Else
                            _documentprefix = doc.DocumentElement.Prefix
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            NodeAddress = GetPrefixedXPath(NodeAddress, "")
                        End If
                        _NodeData = _Node(i).SelectNodes(NodeAddress, nsmgr)

                        For x As Int32 = 0 To _NodeData(0).ChildNodes.Count - 1
                            If (_NodeData(0).ChildNodes(x).Name = NodeName) Then
                                nodevalue = _NodeData(0).ChildNodes(x).InnerText
                                '  _NodeValue.Insert(i, _Node(i).ChildNodes(x).InnerText)
                            End If
                            If (_NodeData(0).ChildNodes(x).Name = RefNodeName) Then
                                refnodevalue = _NodeData(0).ChildNodes(x).InnerText
                                ' _NodeValue.Insert(i, "*,*" + _Node(i).ChildNodes(x).InnerText)
                            End If
                        Next
                        _NodeValue.Insert(i, nodevalue + "*,*" + refnodevalue)
                    Next
                Else
                    _NodeValue = Nothing
                End If
            Else
                _NodeValue = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            doc = Nothing
            nsmgr = Nothing
            ModuleAdd = Nothing
            NodeAddress = Nothing
            _Node = Nothing
            _NodeData = Nothing
            nodevalue = Nothing
            refnodevalue = Nothing
        End Try

        Return _NodeValue

    End Function

    Private Function FetchNodeValue(ByVal ParentNodePath As String, ByVal NodeName As String) As ArrayList
        Dim _documentprefix As String = ""
        'Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        doc.Load(gloLibCCDGeneral.CCDFilePath)
        Dim nsmgr As New XmlNamespaceManager(doc.NameTable)
        Dim ModuleAdd As String = ""
        Dim NodeAddress As String = ""
        NodeAddress = ParentNodePath.Substring(ParentNodePath.IndexOf("Body\") + 5)
        ModuleAdd = ParentNodePath.Substring(0, ParentNodePath.IndexOf("Body\") + 5 + NodeAddress.IndexOf("\"))
        NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf("\") + 1)
        ModuleAdd = ModuleAdd + "\" + NodeAddress.Substring(0, NodeAddress.IndexOf("\"))
        If (NodeAddress.Contains("\")) Then
            NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf("\") + 1)
        End If
        Dim _Node As XmlNodeList
        Dim _NodeData As XmlNodeList
        Dim nodevalue As String

        Try
            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                ModuleAdd = GetPrefixedXPath(ModuleAdd, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                ModuleAdd = GetPrefixedXPath(ModuleAdd, "")
            End If
            _Node = doc.SelectNodes(ModuleAdd, nsmgr)

            If Not IsNothing(_Node) Then
                If _Node.Count > 0 Then
                    For i As Int32 = 0 To _Node.Count - 1
                        nodevalue = ""
                        If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                            _documentprefix = "CR"
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            NodeAddress = GetPrefixedXPath(NodeAddress, _documentprefix)
                        Else
                            _documentprefix = doc.DocumentElement.Prefix
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            NodeAddress = GetPrefixedXPath(NodeAddress, "")
                        End If
                        _NodeData = _Node(i).SelectNodes(NodeAddress, nsmgr)
                        For x As Int32 = 0 To _NodeData.Count - 1
                            If (_NodeData(x).Name = NodeName) Then
                                nodevalue = _NodeData(x).InnerText
                                '  _NodeValue.Insert(i, _Node(i).ChildNodes(x).InnerText)
                            End If
                        Next
                        _NodeValue.Insert(i, nodevalue)

                    Next
                Else
                    _NodeValue = Nothing
                End If
            Else
                _NodeValue = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            doc = Nothing
            nsmgr = Nothing
            ModuleAdd = Nothing
            NodeAddress = Nothing
            _Node = Nothing
            _NodeData = Nothing
            nodevalue = Nothing
        End Try

        Return _NodeValue

    End Function

    Private Function GetDataTable(ByVal Sqlquery As String) As DataTable
        Dim sqlcon As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As New DataTable
        Dim sqldata As SqlDataAdapter
        Try
            sqlcon.ConnectionString = gloLibCCDGeneral.Connectionstring
            sqlcon.Open()
            cmd.Connection = sqlcon
            cmd.CommandText = Sqlquery
            sqldata = New SqlDataAdapter(cmd)
            sqldata.Fill(dt)
            sqldata.Dispose()
            sqldata = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlcon.Close()
            sqlcon.Dispose()
            sqlcon = Nothing
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' Fetch Data from CCD file with reference value
    ''' </summary>
    ''' <param name="Modulename"></param>
    ''' <param name="NodeName"></param>
    ''' <returns>Node Data and Reference node data</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function RetriveNodeValue(ByVal Modulename As String, ByVal NodeName As String, ByVal NodePath As String, ByVal VersionNo As String) As ArrayList

        Dim _documentprefix As String = ""
        Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        'Dim _TempNode As New ArrayList
        Dim _Node As XmlNodeList
        Dim _DataNode As XmlNodeList
        Dim _EntryNode As XmlNodeList
        Dim ParentNodePath As String = ""
        Dim dt As New DataTable
        Dim doc As XmlDocument = New System.Xml.XmlDocument()

        Dim tempaddress As String = ""
        Dim attribute As String = ""
        Dim refattribute As String = ""
        Dim refattributevalue As String = ""
        Dim sectiondt As New DataTable
        Dim section As String = "'"

        Dim nodedata As String = ""
        Dim refnodedata As String = ""
        Dim refaddress As String = ""
        Dim entryaddress As String = ""
        Try

            ' dt = GetDataTable("SELECT * FROM CCD_Modules where VersionNo='" + VersionNo + "' and SectionName='" + Modulename + "'")
            dt = GetDataTable("SELECT SectionID, VersionNo, SectionName, TemplateId, TemplatePath FROM CCD_Modules where VersionNo='" + VersionNo + "' and SectionName='" + Modulename + "'")

            If Not IsNothing(dt) Then
                If (dt.Rows.Count > 0) Then
                    ParentNodePath = Convert.ToString(dt.Rows(0)("TemplatePath"))
                End If
            End If


            doc.Load(gloLibCCDGeneral.CCDFilePath)
            Dim nsmgr As New XmlNamespaceManager(doc.NameTable)

            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(ParentNodePath, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(ParentNodePath, "")
            End If


            _Node = doc.SelectNodes(_xPath, nsmgr)
            If Not IsNothing(_Node) Then
                If _Node.Count > 0 Then
                    For i As Int32 = 0 To _Node.Count - 1

                        tempaddress = "templateId"
                        If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                            _documentprefix = "CR"
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            tempaddress = GetPrefixedXPath(tempaddress, _documentprefix)
                        Else
                            _documentprefix = doc.DocumentElement.Prefix
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            tempaddress = GetPrefixedXPath(tempaddress, "")
                        End If

                        _DataNode = _Node(i).SelectNodes(tempaddress, nsmgr)
                        section = ""
                        For x As Int32 = 0 To _DataNode.Count - 1
                            Dim tempalateadd As String = _DataNode(x).Attributes("root").Value.ToString()
                            Dim dr() As System.Data.DataRow
                            dr = dt.Select("TemplateId='" + tempalateadd + "'")
                            If (dr.Length > 0) Then
                                section = dr(0).Item("SectionName").ToString()
                                Exit For
                            End If
                        Next
                        If section.Trim().Length = 0 Then
                            Continue For
                        End If
                        ' sectiondt = GetDataTable("select * from CCD_Mapping_DTL where sNodeType='Attribute' and sModuleName='" + section + "' and  sXMLAttributeName='" + NodeName + "' and sXMLElementPath='" + NodePath + "'")
                        sectiondt = GetDataTable("select sXMLElementPath,sReferencePath,sXMLAttributeName,sSingleMultipleEntity,sReferenceValue  from CCD_Mapping_DTL where sNodeType='Attribute' and sModuleName='" + section + "' and  sXMLAttributeName='" + NodeName + "' and sXMLElementPath='" + NodePath + "'")
                        For x As Int32 = 0 To sectiondt.Rows.Count - 1
                            entryaddress = "entry"
                            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                                _documentprefix = "CR"
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                entryaddress = GetPrefixedXPath(entryaddress, _documentprefix)
                            Else
                                _documentprefix = doc.DocumentElement.Prefix
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                entryaddress = GetPrefixedXPath(entryaddress, "")
                            End If
                            _EntryNode = _Node(i).SelectNodes(entryaddress, nsmgr)

                            tempaddress = sectiondt.Rows(x).Item("sXMLElementPath").ToString().Trim
                            refaddress = sectiondt.Rows(x).Item("sReferencePath").ToString().Trim
                            tempaddress = tempaddress.Remove(0, tempaddress.IndexOf("entry\") + 6)
                            refaddress = refaddress.Remove(0, refaddress.IndexOf("entry\") + 6)
                            If (tempaddress.Length > refaddress.Length) Then
                                tempaddress = refaddress
                            End If
                            attribute = sectiondt.Rows(x).Item("sXMLAttributeName").ToString().Trim
                            refattribute = sectiondt.Rows(x).Item("sSingleMultipleEntity").ToString().Trim
                            refattributevalue = sectiondt.Rows(x).Item("sReferenceValue").ToString().Trim
                            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                                _documentprefix = "CR"
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                tempaddress = GetPrefixedXPath(tempaddress, _documentprefix)
                            Else
                                _documentprefix = doc.DocumentElement.Prefix
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                tempaddress = GetPrefixedXPath(tempaddress, "")
                            End If
                            'tempaddress = tempaddress + "[@" + refattribute + "='" + refattributevalue + "']"                      

                            For entrycount As Int32 = 0 To _EntryNode.Count - 1
                                _DataNode = _EntryNode(entrycount).SelectNodes(tempaddress, nsmgr)

                                nodedata = RecFunction(_DataNode, attribute)
                                refnodedata = RecFunction(_DataNode, refattribute)

                                'For y As Int32 = 0 To _DataNode.Count - 1
                                '    If (Not _DataNode(y).Attributes(attribute) Is Nothing) Then
                                '        nodedata = _DataNode(y).Attributes(attribute).Value.ToString()
                                '    ElseIf (_DataNode(y).Name = attribute) Then
                                '        nodedata = _DataNode(y).InnerText
                                '    Else
                                '        For k As Int32 = 0 To _DataNode(y).ChildNodes.Count - 1
                                '            If (Not _DataNode(y).ChildNodes(k).Attributes(attribute) Is Nothing) Then
                                '                nodedata = _DataNode(y).ChildNodes(k).Attributes(attribute).Value.ToString()
                                '            ElseIf (_DataNode(y).ChildNodes(k).Name = attribute) Then
                                '                nodedata = _DataNode(y).ChildNodes(k).InnerText
                                '            Else
                                '                For j As Int32 = 0 To _DataNode(y).ChildNodes(k).ChildNodes.Count - 1
                                '                    If (Not _DataNode(y).ChildNodes(k).ChildNodes(j).Attributes(attribute) Is Nothing) Then
                                '                        nodedata = _DataNode(y).ChildNodes(k).ChildNodes(j).Attributes(attribute).Value.ToString()
                                '                    ElseIf (_DataNode(y).ChildNodes(k).ChildNodes(j).Name = attribute) Then
                                '                        nodedata = _DataNode(y).ChildNodes(k).ChildNodes(j).InnerText
                                '                    Else
                                '                        For t As Int32 = 0 To _DataNode(y).ChildNodes(k).ChildNodes(j).ChildNodes.Count - 1
                                '                            If (Not _DataNode(y).ChildNodes(k).ChildNodes(j).ChildNodes(t).Attributes(attribute) Is Nothing) Then
                                '                                nodedata = _DataNode(y).ChildNodes(k).ChildNodes(j).ChildNodes(t).Attributes(attribute).Value.ToString()
                                '                            ElseIf (_DataNode(y).ChildNodes(k).ChildNodes(j).ChildNodes(t).Name = attribute) Then
                                '                                nodedata = _DataNode(y).ChildNodes(k).ChildNodes(j).ChildNodes(t).InnerText
                                '                            End If
                                '                        Next
                                '                    End If
                                '                Next
                                '            End If
                                '        Next
                                '    End If

                                '    If (Not _DataNode(y).Attributes(refattribute) Is Nothing) Then
                                '        refnodedata = _DataNode(y).Attributes(refattribute).Value.ToString()
                                '    ElseIf (_DataNode(y).Name = refattribute) Then
                                '        refnodedata = _DataNode(y).InnerText
                                '    Else
                                '        For z As Int32 = 0 To _DataNode(y).ChildNodes.Count - 1
                                '            If (Not _DataNode(y).ChildNodes(z).Attributes(refattribute) Is Nothing) Then
                                '                refnodedata = _DataNode(y).ChildNodes(z).Attributes(refattribute).Value.ToString()
                                '            ElseIf (_DataNode(y).ChildNodes(z).Name = refattribute) Then
                                '                refnodedata = _DataNode(y).ChildNodes(z).InnerText
                                '            Else
                                '                For j As Int32 = 0 To _DataNode(y).ChildNodes(z).ChildNodes.Count - 1
                                '                    If (Not _DataNode(y).ChildNodes(z).ChildNodes(j).Attributes(refattribute) Is Nothing) Then
                                '                        refnodedata = _DataNode(y).ChildNodes(z).ChildNodes(j).Attributes(refattribute).Value.ToString()
                                '                    ElseIf (_DataNode(y).ChildNodes(z).ChildNodes(j).Name = refattribute) Then
                                '                        refnodedata = _DataNode(y).ChildNodes(z).ChildNodes(j).InnerText
                                '                    Else
                                '                        For t As Int32 = 0 To _DataNode(y).ChildNodes(z).ChildNodes(j).ChildNodes.Count - 1
                                '                            If (Not _DataNode(y).ChildNodes(z).ChildNodes(j).ChildNodes(t).Attributes(refattribute) Is Nothing) Then
                                '                                refnodedata = _DataNode(y).ChildNodes(z).ChildNodes(j).ChildNodes(t).Attributes(refattribute).Value.ToString()
                                '                            ElseIf (_DataNode(y).ChildNodes(z).ChildNodes(j).ChildNodes(t).Name = refattribute) Then
                                '                                refnodedata = _DataNode(y).ChildNodes(z).ChildNodes(j).ChildNodes(t).InnerText
                                '                            End If
                                '                        Next
                                '                    End If
                                '                Next
                                '            End If
                                '        Next
                                '    End If


                                '    '_NodeValue.Add(_DataNode(y).Attributes(attribute).Value.ToString())
                                '    'For z As Int32 = 0 To _DataNode(y).ChildNodes.Count - 1

                                '    'Next
                                'Next
                                _NodeValue.Add(nodedata + "*,*" + refnodedata)
                            Next
                        Next
                        If _NodeValue.Count > 0 Then
                            Return _NodeValue
                        End If

                        '  tempaddress = "entry\act\entryRelationship\observation\value"
                        'If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                        '    _documentprefix = "CR"
                        '    nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                        '    tempaddress = GetPrefixedXPath(tempaddress, _documentprefix)
                        'Else
                        '    _documentprefix = doc.DocumentElement.Prefix
                        '    nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                        '    tempaddress = GetPrefixedXPath(tempaddress, "")
                        'End If
                        ''_DataNode = doc.SelectNodes(getproblemdata, nsmgr)
                        ''Dim tempdata As String = _DataNode(i).Attributes("statusCode").InnerText.ToString()
                        '_DataNode = _Node(i).SelectNodes(tempaddress, nsmgr)
                        'For x As Int32 = 0 To _DataNode.Count - 1
                        '    '  Dim tempdata As String = _DataNode(x).Attributes("value").Value.ToString()
                        'Next


                    Next
                Else
                    _NodeValue = Nothing
                End If
            Else
                _NodeValue = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            _xPath = Nothing
            _Node = Nothing
            _DataNode = Nothing
            _EntryNode = Nothing
            ParentNodePath = Nothing
            doc = Nothing

            tempaddress = Nothing
            attribute = Nothing
            refattribute = Nothing
            refattributevalue = Nothing
            sectiondt = Nothing
            section = Nothing

            nodedata = Nothing
            refnodedata = Nothing
            refaddress = Nothing
            entryaddress = Nothing

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
        Return _NodeValue
    End Function

    ''' <summary>
    ''' Fetch Data from CCD file with reference value
    ''' </summary>
    ''' <param name="Modulename"></param>
    ''' <param name="NodeName"></param>
    ''' <returns>Node Data and Reference node data</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function RetriveNodeValue(ByVal Modulename As String, ByVal NodeName As String, ByVal NodePath As String, ByVal VersionNo As String, ByVal RefFlag As Boolean) As ArrayList

        Dim _documentprefix As String = ""
        Dim _xPath As String = ""
        Dim _NodeValue As New ArrayList
        'Dim _TempNode As New ArrayList
        Dim _Node As XmlNodeList
        Dim _DataNode As XmlNodeList
        Dim _EntryNode As XmlNodeList
        Dim ParentNodePath As String = ""
        Dim dt As New DataTable
        Dim doc As XmlDocument = New System.Xml.XmlDocument()
        Dim nsmgr As XmlNamespaceManager = Nothing

        Dim tempaddress As String = ""
        Dim attribute As String = ""
        'Dim refattribute As String = ""
        'Dim refattributevalue As String = ""
        Dim sectiondt As New DataTable
        Dim section As String = "'"

        Dim nodedata As String = ""
        Dim entryaddress As String = ""
        Try

            'dt = GetDataTable("SELECT * FROM CCD_Modules where VersionNo='" + VersionNo + "' and SectionName='" + Modulename + "'")
            dt = GetDataTable("SELECT SectionID, VersionNo, SectionName, TemplateId, TemplatePath FROM CCD_Modules where VersionNo='" + VersionNo + "' and SectionName='" + Modulename + "'")
            If Not IsNothing(dt) Then
                If (dt.Rows.Count > 0) Then
                    ParentNodePath = Convert.ToString(dt.Rows(0)("TemplatePath"))
                End If
            End If


            doc.Load(gloLibCCDGeneral.CCDFilePath)
            nsmgr = New XmlNamespaceManager(doc.NameTable)

            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                _documentprefix = "CR"
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(ParentNodePath, _documentprefix)
            Else
                _documentprefix = doc.DocumentElement.Prefix
                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                _xPath = GetPrefixedXPath(ParentNodePath, "")
            End If


            _Node = doc.SelectNodes(_xPath, nsmgr)
            If Not IsNothing(_Node) Then
                If _Node.Count > 0 Then
                    For i As Int32 = 0 To _Node.Count - 1

                        tempaddress = "templateId"
                        If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                            _documentprefix = "CR"
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            tempaddress = GetPrefixedXPath(tempaddress, _documentprefix)
                        Else
                            _documentprefix = doc.DocumentElement.Prefix
                            nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                            tempaddress = GetPrefixedXPath(tempaddress, "")
                        End If

                        _DataNode = _Node(i).SelectNodes(tempaddress, nsmgr)
                        section = ""
                        For x As Int32 = 0 To _DataNode.Count - 1
                            Dim tempalateadd As String = _DataNode(x).Attributes("root").Value.ToString()
                            Dim dr() As System.Data.DataRow
                            dr = dt.Select("TemplateId='" + tempalateadd + "'")
                            If (dr.Length > 0) Then
                                section = dr(0).Item("SectionName").ToString()
                                Exit For
                            End If
                        Next
                        If section.Trim().Length = 0 Then
                            Continue For
                        End If
                        ' sectiondt = GetDataTable("select * from CCD_Mapping_DTL where sNodeType='Attribute' and sModuleName='" + section + "' and  sXMLAttributeName='" + NodeName + "' and sXMLElementPath='" + NodePath + "'")
                        sectiondt = GetDataTable("select sXMLElementPath,sXMLAttributeName  from CCD_Mapping_DTL where sNodeType='Attribute' and sModuleName='" + section + "' and  sXMLAttributeName='" + NodeName + "' and sXMLElementPath='" + NodePath + "'")
                        For x As Int32 = 0 To sectiondt.Rows.Count - 1
                            entryaddress = "entry"
                            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                                _documentprefix = "CR"
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                entryaddress = GetPrefixedXPath(entryaddress, _documentprefix)
                            Else
                                _documentprefix = doc.DocumentElement.Prefix
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                entryaddress = GetPrefixedXPath(entryaddress, "")
                            End If
                            _EntryNode = _Node(i).SelectNodes(entryaddress, nsmgr)

                            tempaddress = sectiondt.Rows(x).Item("sXMLElementPath").ToString().Trim

                            tempaddress = tempaddress.Remove(0, tempaddress.IndexOf("entry\") + 6)

                            attribute = sectiondt.Rows(x).Item("sXMLAttributeName").ToString().Trim


                            If doc.DocumentElement.Prefix Is Nothing OrElse doc.DocumentElement.Prefix.Trim() = "" Then
                                _documentprefix = "CR"
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                tempaddress = GetPrefixedXPath(tempaddress, _documentprefix)
                            Else
                                _documentprefix = doc.DocumentElement.Prefix
                                nsmgr.AddNamespace(_documentprefix, doc.DocumentElement.NamespaceURI)
                                tempaddress = GetPrefixedXPath(tempaddress, "")
                            End If
                            'tempaddress = tempaddress + "[@" + refattribute + "='" + refattributevalue + "']"                      

                            For entrycount As Int32 = 0 To _EntryNode.Count - 1
                                _DataNode = _EntryNode(entrycount).SelectNodes(tempaddress, nsmgr)
                                nodedata = RecFunction(_DataNode, attribute)

                                _NodeValue.Add(nodedata)
                            Next
                        Next
                        If _NodeValue.Count > 0 Then
                            Return _NodeValue
                        End If
                    Next
                Else
                    _NodeValue = Nothing
                End If
            Else
                _NodeValue = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            _documentprefix = Nothing
            _xPath = Nothing
            _Node = Nothing
            _DataNode = Nothing
            _EntryNode = Nothing
            ParentNodePath = Nothing
            doc = Nothing
            nsmgr = Nothing
            tempaddress = Nothing
            attribute = Nothing
            sectiondt = Nothing
            section = Nothing
            nodedata = Nothing
            entryaddress = Nothing

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
        Return _NodeValue
    End Function

    ''' <summary>
    ''' Retrive data from Parent tree
    ''' </summary>
    ''' <param name="DataNode"></param>
    ''' <param name="Attributevalue"></param>
    ''' <returns>Node value</returns>
    ''' <remarks></remarks>
    Private Function RecFunction(ByVal DataNode As XmlNodeList, ByVal Attributevalue As String) As String
        Try
            Dim nodedata As String = ""
            For y As Int32 = 0 To DataNode.Count - 1
                If (Not DataNode(y).Attributes(Attributevalue) Is Nothing) Then
                    nodedata = DataNode(y).Attributes(Attributevalue).Value.ToString()
                    Exit For
                ElseIf (DataNode(y).Name = Attributevalue) Then
                    nodedata = DataNode(y).InnerText
                    Exit For
                Else
                    nodedata = RecFunction(DataNode(y).ChildNodes, Attributevalue)
                End If
            Next
            Return nodedata
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        End Try
    End Function


    Private Function GetPrefixedXPath(ByVal XPath As String, ByVal Prefix As String) As String
        Dim _prefixXPath As String = ""

        Try
            XPath = XPath.Replace("\", "/")

            If Prefix.Trim() <> "" Then
                _prefixXPath = XPath.Replace("/", "/" & Prefix.Trim() & ":")
                _prefixXPath = Prefix & ":" & _prefixXPath
            Else
                _prefixXPath = XPath
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            _prefixXPath = ""
        End Try

        Return _prefixXPath
    End Function

    Public Function ExtractClinicalInformation() As Patient
        Try

            mPatient = New Patient
            oPatient = New gloPatient.Patient()
            'ogloPatientHistory = New gloPatientHistory()
            'oVitals = New Vitals()
            ' oProblem = New Problems()
            oInsurance = New gloPatient.Insurance()
            oMedication = New Medication()
            '  oGuarantor = New gloPatient.PatientOtherContact
            '  oEncounters = New Encounters()
            oUser = New User()
            Dim dt As DataTable
            sTodayDate = DateTime.Now()
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()
            '    sTodayDate = _objCCDDatabaseLayer.ConvertDateTime(sTodayDate)
            ' sTodayDate = _objCCDDatabaseLayer.ConvertDateTime(sTodayDate)


            ' For Patient Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Patient")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Patient")
            End If
            FillPatientData(dt)

            'For histry Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Allergies")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Allergies")
            End If
            FillPatientHistory(dt)

            'For Vital Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Vitals")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Vitals")
            End If
            FillPatientVital(dt)
            'End If

            'For Problem Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "ProblemList")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "ProblemList")
            End If
            FillPatientProblem(dt)

            'For   Patient Insurance Detail Module

            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Patient Insurance Detail")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Patient Insurance Detail")
            End If

            FillPatientInsurance(dt)

            'For Patient Guarantor
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Guarantor")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Guarantor")
            End If
            FillPatientGuarantor(dt)

            'For Medication Guarantor
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Medication")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Medication")
            End If
            FillPatientMedications(dt)

            'For Family History
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Family History")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Family History")
            End If

            FillPatientFamilyHistory(dt)

            'For Social History
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Social History")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Social History")
            End If
            FillPatientSocialHistory(dt)

            'For Social Immunization
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Immunization")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Immunization")
            End If
            FillPatientImmunization(dt)

            'For Lab Result
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Lab")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Lab")
            End If

            FillPatientLabResult(dt)

            'For Encounter
            'dt = DefaultMappingRestore(sCCRVersion, "Encounters")
            'oEncounters.EncounterDate = _NodeValue
            'FillPatientEncounter(_ID, _NodeValue)
            'End If
            'mPatient.PatientEncounters.Add(oEncounters)
            mPatient.PatientDemographics = oPatient
            oUser.ClinicID = _ClinicID
            oUser.UserID = nLoginID
            oUser.UserName = sUserName
            oUser.Version = sCCRVersion
            mPatient.UserDetails = oUser
            Return mPatient
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        End Try
    End Function
    Public Function ReconcilePatientInformation() As Patient
        Dim dt As DataTable = Nothing
        Try
            mPatient = New Patient
            oPatient = New gloPatient.Patient()


            '   oProblem = New Problems()

            oMedication = New Medication()
            'oGuarantor = New gloPatient.PatientOtherContact
            'oEncounters = New Encounters()
            oUser = New User()
            sTodayDate = DateTime.Now()
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()



            'For histry Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Allergies")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Allergies")
            End If
            FillPatientHistory(dt)

            ''For Family History
            'If _sFileType = "CCD" Then
            '    dt = DefaultCCDMappingRestore(sCCRVersion, "Family History")
            'Else
            '    dt = DefaultMappingRestore(sCCRVersion, "Family History")
            'End If

            'FillPatientFamilyHistory(dt)

            ''For Social History
            'If _sFileType = "CCD" Then
            '    dt = DefaultCCDMappingRestore(sCCRVersion, "Social History")
            'Else
            '    dt = DefaultMappingRestore(sCCRVersion, "Social History")
            'End If
            'FillPatientSocialHistory(dt)


            'For Problem Module
            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "ProblemList")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "ProblemList")
            End If
            FillPatientProblem(dt)


            If _sFileType = "CCD" Then
                dt = DefaultCCDMappingRestore(sCCRVersion, "Medication")
            Else
                dt = DefaultMappingRestore(sCCRVersion, "Medication")
            End If
            FillPatientMedications(dt)


            mPatient.PatientDemographics = oPatient
            oUser.ClinicID = _ClinicID
            oUser.UserID = nLoginID
            oUser.UserName = sUserName
            oUser.Version = sCCRVersion
            mPatient.UserDetails = oUser
            Return mPatient
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Function
    ' Add By Rohit on 20101110 to Fill Patient Info
    Public Sub FillPatientData(ByVal dt As DataTable)

        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList

        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    For i As Int32 = 0 To dt.Rows.Count - 1
                        _NodeArrayList = New ArrayList()
                        _NodeValue = ""
                        _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sNodeType").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        If Not IsNothing(_NodeArrayList) Then
                            If _NodeArrayList.Count > 0 Then
                                _NodeValue = _NodeArrayList.Item(0).ToString
                            End If
                        End If


                        _ID = dt.Rows(i)("nMappingID")
                        Select Case _ID
                            Case ClseNum.cls_enum_Fields.Patien_Code
                                Dim temp As New gloPatient.gloPatient(gloLibCCDGeneral.Connectionstring)
                                oPatient.DemographicsDetail.PatientCode = temp.GeneratePatientCode()
                                oPatient.DemographicsDetail.PatientExternalCode = _NodeValue
                                oPatient.DemographicsDetail.PatientProviderID = _objCCDDatabaseLayer.getDefaultProviderId()
                                oPatient.DemographicsDetail.PatientLanguage = _objCCDDatabaseLayer.getDefaultLocation()
                            Case ClseNum.cls_enum_Fields.Patient_First_Name
                                oPatient.DemographicsDetail.PatientFirstName = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Middle_Name
                                oPatient.DemographicsDetail.PatientMiddleName = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Last_Name
                                oPatient.DemographicsDetail.PatientLastName = _NodeValue
                            Case ClseNum.cls_enum_Fields.SSN_Number
                                oPatient.DemographicsDetail.PatientSSN = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Date_Time_of_Birth
                                oPatient.DemographicsDetail.PatientDOB = _objCCDDatabaseLayer.ConvertDateTime(_NodeValue)
                            Case ClseNum.cls_enum_Fields.Patient_Gender
                                oPatient.DemographicsDetail.PatientGender = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Marital_Status
                                oPatient.DemographicsDetail.PatientMaritalStatus = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Address_Line1
                                oPatient.DemographicsDetail.PatientAddress1 = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Address_Line2
                                oPatient.DemographicsDetail.PatientAddress2 = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_City
                                oPatient.DemographicsDetail.PatientCity = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_State
                                oPatient.DemographicsDetail.PatientState = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_ZIP
                                oPatient.DemographicsDetail.PatientZip = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_County
                                oPatient.DemographicsDetail.PatientCounty = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Country
                                oPatient.DemographicsDetail.PatientCountry = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Phone_Number
                                _NodeValue = _NodeValue.ToLower().Trim()
                                oPatient.DemographicsDetail.PatientPhone = _NodeValue.Replace(":+1", "").Replace("-", "").Replace(":1", "").Replace("tel", "")
                            Case ClseNum.cls_enum_Fields.Patient_Mobile_Number
                                _NodeValue = _NodeValue.ToLower().Trim()
                                oPatient.DemographicsDetail.PatientMobile = _NodeValue.Replace(":+1", "").Replace("-", "").Replace(":1", "").Replace("tel", "")
                                'oPatient.DemographicsDetail.PatientEmail = _NodeValue
                            Case ClseNum.cls_enum_Fields.Mother_First_Name
                                oPatient.GuardianDetail.PatientMotherFirstName = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Occupation
                                oPatient.DemographicsDetail.PatientOccupation = _NodeValue
                            Case ClseNum.cls_enum_Fields.Employment_Status
                                oPatient.OccupationDetail.PatientEmploymentStatus = _NodeValue
                            Case ClseNum.cls_enum_Fields.Place_of_Employment
                                oPatient.OccupationDetail.PatientPlaceofEmployment = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_Address_Line1
                                oPatient.OccupationDetail.PatientWorkAddress1 = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_Address_Line2
                                oPatient.OccupationDetail.PatientWorkAddress2 = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_City
                                oPatient.OccupationDetail.PatientWorkCity = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_State
                                oPatient.OccupationDetail.PatientWorkState = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_ZIP
                                oPatient.OccupationDetail.PatientWorkZip = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_Phone_Number
                                _NodeValue = _NodeValue.ToLower().Trim()
                                oPatient.OccupationDetail.PatientWorkPhone = _NodeValue.Replace(":+1", "").Replace("-", "").Replace(":1", "").Replace("tel", "")
                            Case ClseNum.cls_enum_Fields.Race
                                oPatient.DemographicsDetail.PatientRace = _NodeValue
                            Case ClseNum.cls_enum_Fields.Mother_Middle_Name
                                oPatient.GuardianDetail.PatientMotherMiddleName = _NodeValue
                            Case ClseNum.cls_enum_Fields.Mother_Last_Name
                                oPatient.GuardianDetail.PatientMotherLastName = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Registration_Date
                                oPatient.PatientDemographicOtherInfo.RegistrationDate = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Fax
                                oPatient.DemographicsDetail.PatientFax = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Work_Fax
                                oPatient.OccupationDetail.PatientWorkFax = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Emergency_Contact
                                oPatient.DemographicsDetail.EmergencyContact = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Emergency_Phone
                                oPatient.DemographicsDetail.EmergencyPhone = _NodeValue
                            Case ClseNum.cls_enum_Fields.Patient_Status
                                oPatient.PatientDemographicOtherInfo.Status = _NodeValue
                        End Select
                    Next
                End If
                mPatient.PatientDemographics = oPatient
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            _NodeValue = Nothing
            _NodeArrayList = Nothing
            'If Not IsDBNull(oPatient) Then
            '    oPatient.Dispose()
            'End If
        End Try
    End Sub
    'END By Rohit on 20101110

    ' Add By Rohit on 20101110 to Fill Patient Insurance Info
    Public Sub FillPatientInsurance(ByVal dt As DataTable)
        Dim oInsurance As gloPatient.Insurance = Nothing
        Dim oInsuranceCol As gloPatient.Insurances = Nothing

        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList

        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oInsuranceCol = New gloPatient.Insurances()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If
                    _ID = dt.Rows(i)("nMappingID")
                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Subscriber_Policy_Number
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                        oInsurance = New gloPatient.Insurance()
                                        oInsurance.SubscriberPolicy = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        oInsuranceCol.Add(oInsurance)
                                        oInsurance.Dispose()
                                        oInsurance = Nothing
                                    End If
                                Next

                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Insurance_Subscriber_ID
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oInsuranceCol.Item(cnt).SubscriberID = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Insurance_Group
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oInsuranceCol.Item(cnt).Group = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Insurance_Employer
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).Employer = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Insureds_Date_of_Birth
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).DOB = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.CopayER
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CopayER = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.CopayOV
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CopayOV = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.CopaySP
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CopaySP = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.CopayUC
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CopayUC = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Effective_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).EffectiveDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Expiry_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).ExpiryDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_First_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberFName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_Middle_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberMName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_Last_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberLName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.RelationShipID
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).RelationshipID = Convert.ToInt32(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.RelationShip
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).RelationshipName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Deductable_amount
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).DeductableAmount = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Coverage_Percent
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CoveragePercent = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.CoPay
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).CoPay = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Assignment_of_Benifit
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).AssignmentofBenefit = Convert.ToByte(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Start_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).StartDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.End_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).EndDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Insurance_Flag
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).InsuranceFlag = Convert.ToInt32(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_Gender
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberGender = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_AddressLine1
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberAddr1 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_AddressLine2
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberAddr2 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_State
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberState = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_City
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberCity = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Subscriber_Zip
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).SubscriberZip = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_External_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).InsuranceTypeCode = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_Phone
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).InsurancePhone = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).InsuranceName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_AddressLine1
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).AddrressLine1 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_AddressLine2
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).AddrressLine2 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_City
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).City = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_State
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).State = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_ZIP
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).ZIP = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_Fax
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).Fax = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_Email
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).Email = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_URL
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).URL = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Patient_Insurance_Phone
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oInsuranceCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oInsuranceCol.Item(cnt)) Then
                                        oInsuranceCol.Item(cnt).Phone = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oInsuranceCol) Then
                    mPatient.PatientDemographics.Insurances = oInsuranceCol
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oInsuranceCol) Then
                '     oInsuranceCol.Dispose() ' SLR: Should not be disposed since it is referenced in  mPatient.PatientDemographics.Insurances
                oInsuranceCol = Nothing
            End If
        End Try
    End Sub
    ' END By Rohit on 20101110

    ' Add By Rohit on 20101110 to Fill Patient Other Contact Info
    Public Sub FillPatientGuarantor(ByVal dt As DataTable)
        Dim oGuarantor As gloPatient.PatientOtherContact = Nothing
        Dim oGuarantorCol As gloPatient.PatientOtherContacts = Nothing

        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList = Nothing

        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oGuarantorCol = New gloPatient.PatientOtherContacts()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '_NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If

                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Guarantor_First_Name
                            If Not IsNothing(_NodeArrayList) AndAlso _NodeArrayList.Count > 0 Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oGuarantor = New gloPatient.PatientOtherContact()
                                    oGuarantor.FirstName = _NodeArrayList(cnt).ToString()
                                    oGuarantorCol.Add(oGuarantor)
                                    oGuarantor = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Date_of_Birth
                            If Not IsNothing(_NodeArrayList) AndAlso _NodeArrayList.Count > 0 AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).DOB = _objCCDDatabaseLayer.ConvertDateTimeinInt(_NodeArrayList(cnt).ToString()).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Last_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).LastName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Middle_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).MiddleName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Sex
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).Gender = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Relationship
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).Relation = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Address_Line1
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).AddressLine1 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Address_Line2
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).AddressLine2 = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_City
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).City = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_State
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).State = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_ZIP
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).Zip = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Country
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).Country = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_Phone
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).Phone = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Guarantor_SSN_Number
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oGuarantorCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oGuarantorCol.Item(cnt)) Then
                                        oGuarantorCol.Item(cnt).SSN = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oGuarantorCol) Then
                    mPatient.PatientDemographics.PatientGuarantors = oGuarantorCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oGuarantorCol) Then
                oGuarantorCol.Dispose()
            End If
            _NodeValue = Nothing
            _NodeArrayList = Nothing
        End Try
    End Sub
    ' END By Rohit on 20101110

    ' Add By Rohit on 20101110 to Fill Medication Info
    Public Sub FillPatientMedications(ByVal dt As DataTable)
        Dim oMedicationCol As MedicationsCol = Nothing
        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList = Nothing
        Dim oMedication As Medication = Nothing

        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oMedicationCol = New MedicationsCol()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '  _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())

                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If
                    _NodeValue = dt.Rows(i)("sReferenceValue")

                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Medication_Date
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oMedication = New Medication()
                                    oMedication.MedicationDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    oMedicationCol.Add(oMedication)
                                    oMedication = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_RxNorm_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oMedicationCol.Item(cnt).RxNormCode = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).GenericName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Dosage_of_Medication
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).DrugQuantity = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Frequency_of_Medication
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).Frequency = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_Route
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).Route = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_Amount
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).DrugStrength = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Refills
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).Refills = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_Status
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).ToString().ToUpper() = "ACTIVE" Then
                                            oMedicationCol.Item(cnt).Status = ""
                                        Else
                                            oMedicationCol.Item(cnt).Status = _NodeArrayList(cnt).ToString()
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Medication_unit
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oMedicationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oMedicationCol.Item(cnt)) Then
                                        oMedicationCol.Item(cnt).StrengthUnits = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oMedicationCol) Then
                    mPatient.PatientMedications = oMedicationCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oMedicationCol) Then
                oMedicationCol.Dispose()
            End If
            _NodeValue = Nothing
            _NodeArrayList = Nothing
        End Try
    End Sub
    ' END By Rohit on 20101110

    Public Sub FillPatientHistory(ByVal dt As DataTable)
        Dim oHistoryCol As gloPatientHistoryCol = Nothing
        Dim _ID As Int32 = 0
        Dim _NodeValue As String = ""
        Dim _NodeArrayList As ArrayList = Nothing
        Dim ogloPatientHistory As gloPatientHistory = Nothing
        Dim _Category As String = ""
        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oHistoryCol = New gloPatientHistoryCol()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '     _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    _Category = dt.Rows(i)("sCategoryName").ToString()
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If

                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.History_Item
                            If Not IsNothing(_NodeArrayList) Then ' And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    ogloPatientHistory = New gloPatientHistory()
                                    ogloPatientHistory.HistoryItem = _NodeArrayList(cnt).ToString()
                                    ogloPatientHistory.DrugName = _NodeArrayList(cnt).ToString()
                                    ogloPatientHistory.HistoryCategory = _Category
                                    ogloPatientHistory.Comments = ""
                                    oHistoryCol.Add(ogloPatientHistory)
                                    ogloPatientHistory = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.History_Reaction
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        oHistoryCol.Item(cnt).Reaction = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.HComments
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        oHistoryCol.Item(cnt).Comments = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.History_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        If Not IsNothing(_NodeArrayList(cnt).ToString()) AndAlso _NodeArrayList(cnt).ToString() <> "" Then
                                            oHistoryCol.Item(cnt).DOEAllergy = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                        Else
                                            oHistoryCol.Item(cnt).DOEAllergy = sTodayDate
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.History_Status
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        oHistoryCol.Item(cnt).Status = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.History_RxNorm_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        If (_NodeArrayList(cnt).Contains("*,*")) Then
                                            If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                                oHistoryCol.Item(cnt).RxNormCode = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                            End If
                                        Else
                                            oHistoryCol.Item(cnt).RxNormCode = _NodeArrayList(cnt).ToString()
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.History_Snomed_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oHistoryCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oHistoryCol.Item(cnt)) Then
                                        If (_NodeArrayList(cnt).Contains("*,*")) Then
                                            '  If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            If _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*")) = """" Then
                                                oHistoryCol.Item(cnt).ConceptId = ""
                                            Else
                                                oHistoryCol.Item(cnt).ConceptId = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                            End If

                                            'End If
                                        Else
                                            oHistoryCol.Item(cnt).ConceptId = _NodeArrayList(cnt).ToString()
                                        End If
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oHistoryCol) Then
                    mPatient.PatientHistory = oHistoryCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oHistoryCol) Then
                oHistoryCol.Dispose()
            End If
            _NodeValue = Nothing
            _NodeArrayList = Nothing
            _Category = Nothing
        End Try
    End Sub

    'Add By Rohit on 20101011 to Fill Patient Vital Info
    Public Sub FillPatientVital(ByVal dt As DataTable)
        Dim oVitalCol As VitalsCol = Nothing
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList
        Dim oVital As Vitals
        Dim _NodeValue As String = ""
        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oVitalCol = New VitalsCol()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '_NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If
                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Vital_Date
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oVital = New Vitals()
                                    If Not IsNothing(_NodeArrayList(cnt).ToString()) AndAlso _NodeArrayList(cnt).ToString() <> "" Then
                                        oVital.VitalDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    Else
                                        oVital.VitalDate = sTodayDate
                                    End If
                                    oVitalCol.Add(oVital)
                                    oVital = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Blood_Pressure_Sitting_Max
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).BloodPressureSittingMax = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Blood_Pressure_Sitting_Min
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then ' And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).BloodPressureSittingMin = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Temperature_in_Celcius
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).Temperature = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Pulse_Per_Minute
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).PulsePerMinute = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Weight_in_lbs
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1

                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).Weightinlbs = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If


                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Respiratory_Rate
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then '_NodeArrayList.Count > 0 And 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).RespiratoryRate = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Height_in_inch
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).HeightinInch = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.VComments
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).Comment = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.dPulseOx
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oVitalCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oVitalCol.Item(cnt)) Then
                                        oVitalCol.Item(cnt).PulseOx = Convert.ToDecimal(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oVitalCol) Then
                    mPatient.PatientVitals = oVitalCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            If Not IsNothing(oVitalCol) Then
                oVitalCol.Dispose()
            End If
        Finally
            _NodeArrayList = Nothing
            _NodeValue = Nothing
        End Try
    End Sub
    'END By Rohit on 20101110

    'Add By Rohit on 20101110 to Fill Patient Problem Info
    Public Sub FillPatientProblem(ByVal dt As DataTable)
        Dim oProblem As Problems
        Dim oProblemCol As New ProblemsCol
        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList

        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oProblemCol = New ProblemsCol()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()

                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If

                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")
                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Date_Of_Service
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oProblem = New Problems()
                                    If Not IsNothing(_NodeArrayList(cnt).ToString()) AndAlso _NodeArrayList(cnt).ToString() <> "" Then
                                        oProblem.DateOfService = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    Else
                                        oProblem.DateOfService = sTodayDate
                                    End If
                                    oProblem.Immediacy = 3
                                    oProblemCol.Add(oProblem)
                                    oProblem = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Cheif_Complaint
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oProblemCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oProblemCol.Item(cnt)) Then
                                        oProblemCol.Item(cnt).Condition = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.ICD9_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oProblemCol) Then 'And _NodeArrayList.Count > 0 
                                Dim temp As Int32 = 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If temp < oProblemCol.Count Then
                                        If Not IsNothing(oProblemCol.Item(temp)) Then
                                            If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                                If (_NodeArrayList(cnt).Contains("*,*")) Then
                                                    oProblemCol.Item(temp).ICD9Code = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                                    oProblemCol.Item(temp).ICD9 = _NodeValue
                                                    temp += 1
                                                Else
                                                    oProblemCol.Item(temp).ICD9Code = _NodeArrayList(cnt).ToString()
                                                    oProblemCol.Item(temp).ICD9 = _NodeValue
                                                    temp += 1
                                                End If

                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Problem_Status
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oProblemCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oProblemCol.Item(cnt)) Then
                                        oProblemCol.Item(cnt).ConditionStatus = _NodeArrayList(cnt).ToString()
                                        Select Case _NodeArrayList(cnt).ToString().ToUpper()
                                            Case "RESOLVED"
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.Resolved
                                            Case "ACTIVE"
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.Active
                                            Case "INACTIVE"
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.Inactive
                                            Case "CHRONIC"
                                                oProblemCol.Item(cnt).Immediacy = 2
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.Active
                                            Case "ALL"
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.All
                                            Case Else
                                                oProblemCol.Item(cnt).ProblemStatus = Problems.Status.Active
                                        End Select
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oProblemCol) Then
                    mPatient.PatientProblems = oProblemCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            If Not IsNothing(oProblemCol) Then
                oProblemCol.Dispose()
            End If
        Finally
            _NodeValue = Nothing
            _NodeArrayList = Nothing
        End Try
    End Sub

    'Add By Rohit on 20101110 to Fill Patient Famliy History
    Public Sub FillPatientFamilyHistory(ByVal dt As DataTable)
        Dim oFHistoryCol As gloPatientHistoryCol = Nothing
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList
        Dim ogloPatientHistory As gloPatientHistory
        Dim _Category As String = ""
        Dim _NodeValue As String = ""
        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oFHistoryCol = New gloPatientHistoryCol()
                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '_NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If
                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _Category = dt.Rows(i)("sCategoryName").ToString()
                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Family_History_Item
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    ogloPatientHistory = New gloPatientHistory()
                                    ogloPatientHistory.HistoryItem = _NodeArrayList(cnt).ToString()
                                    ogloPatientHistory.HistoryCategory = _Category
                                    ogloPatientHistory.Comments = ""
                                    oFHistoryCol.Add(ogloPatientHistory)
                                    ogloPatientHistory = Nothing
                                Next
                            Else
                                Exit Sub
                            End If

                        Case ClseNum.cls_enum_Fields.Family_Comments
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oFHistoryCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oFHistoryCol.Item(cnt)) Then
                                        oFHistoryCol.Item(cnt).Comments = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If

                        Case ClseNum.cls_enum_Fields.Family_History_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oFHistoryCol) Then ' And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oFHistoryCol.Item(cnt)) Then
                                        If Not IsNothing(_NodeArrayList(cnt).ToString()) AndAlso _NodeArrayList(cnt).ToString() <> "" Then
                                            oFHistoryCol.Item(cnt).DOEAllergy = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                        Else
                                            oFHistoryCol.Item(cnt).DOEAllergy = sTodayDate
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.History_Status
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oFHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oFHistoryCol.Item(cnt)) Then
                                        oFHistoryCol.Item(cnt).Status = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Family_RxNorm_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oFHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oFHistoryCol.Item(cnt)) Then
                                        oFHistoryCol.Item(cnt).RxNormCode = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Family_Snomed_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oFHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oFHistoryCol.Item(cnt)) Then
                                        oFHistoryCol.Item(cnt).ConceptId = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oFHistoryCol) Then
                    Dim sFHistory As gloPatientHistoryCol = oFHistoryCol
                    For Each oFHist As gloPatientHistory In sFHistory
                        If Not IsNothing(oFHist) Then
                            mPatient.PatientHistory.Add(oFHist)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oFHistoryCol) Then
                oFHistoryCol.Dispose()
            End If
            _NodeArrayList = Nothing
            _Category = Nothing
            _NodeValue = Nothing
        End Try
    End Sub

    'Add By Rohit on 20101210 to Fill Patient Social History
    Public Sub FillPatientSocialHistory(ByVal dt As DataTable)
        Dim oSHistoryCol As gloPatientHistoryCol = Nothing
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList
        Dim ogloPatientHistory As gloPatientHistory
        Dim _Category As String = ""
        Dim _NodeValue As String = ""
        Try
            _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oSHistoryCol = New gloPatientHistoryCol()

                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    ' _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If
                    _Category = dt.Rows(i)("sCategoryName").ToString()
                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Social_History_Item
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    ogloPatientHistory = New gloPatientHistory()
                                    ogloPatientHistory.HistoryItem = _NodeArrayList(cnt).ToString()
                                    ogloPatientHistory.HistoryCategory = _Category
                                    ogloPatientHistory.Comments = ""
                                    oSHistoryCol.Add(ogloPatientHistory)
                                    ogloPatientHistory = Nothing
                                Next
                            Else
                                Exit Sub
                            End If

                        Case ClseNum.cls_enum_Fields.Social_Comments
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oSHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oSHistoryCol.Item(cnt)) Then
                                        oSHistoryCol.Item(cnt).Comments = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If

                        Case ClseNum.cls_enum_Fields.Social_History_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oSHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oSHistoryCol.Item(cnt)) Then
                                        If Not IsNothing(_NodeArrayList(cnt).ToString()) AndAlso _NodeArrayList(cnt).ToString() <> "" Then
                                            oSHistoryCol.Item(cnt).DOEAllergy = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                        Else
                                            oSHistoryCol.Item(cnt).DOEAllergy = sTodayDate
                                        End If
                                    End If
                                Next
                            End If
                            'Case ClseNum.cls_enum_Fields.History_Status
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oHistoryCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oHistoryCol.Item(cnt)) Then
                            '                oHistoryCol.Item(cnt).Status = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                        Case ClseNum.cls_enum_Fields.Social_RxNorm_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oSHistoryCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oSHistoryCol.Item(cnt)) Then
                                        oSHistoryCol.Item(cnt).RxNormCode = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Social_Snomed_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oSHistoryCol) Then ' And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oSHistoryCol.Item(cnt)) Then
                                        oSHistoryCol.Item(cnt).ConceptId = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next

                If Not IsNothing(oSHistoryCol) Then
                    Dim sHistory As gloPatientHistoryCol = oSHistoryCol
                    For Each oHist As gloPatientHistory In sHistory
                        If Not IsNothing(oHist) Then
                            mPatient.PatientHistory.Add(oHist)
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oSHistoryCol) Then
                oSHistoryCol.Dispose()
            End If
            _NodeArrayList = Nothing
            _Category = Nothing
            _NodeValue = Nothing
        End Try
    End Sub
    ' END By Rohit on 20101110
    'End of Programming

    'Add By Rohit on 20101410 to Fill Patient Social History
    Public Sub FillPatientImmunization(ByVal dt As DataTable)
        Dim oImmunizationCol As ImmunizationCol = Nothing
        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList = Nothing
        Dim oImmunization As Immunization = Nothing

        Try

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oImmunizationCol = New ImmunizationCol()

                _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    '_NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If

                    _NodeValue = dt.Rows(i)("sReferenceValue")

                    _ID = dt.Rows(i)("nMappingID")

                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Immunization_Item_Name
                            If Not IsNothing(_NodeArrayList) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oImmunization = New Immunization()
                                    oImmunization.VaccineName = (_NodeArrayList(cnt).ToString())
                                    oImmunizationCol.Add(oImmunization)
                                    oImmunization = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Visit_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).ImmunizationDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Immunization_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).ImmunizationDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Immunization_Notes
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).ImmunizationNotes = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Transaction_time_given
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).TransactionTimeGiven = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Vaccine_code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oImmunizationCol.Item(cnt).VaccineCode = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Lot_number
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oImmunizationCol.Item(cnt).LotNumber = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next 'And _NodeArrayList.Count > 0 
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Site
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).Site = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Route
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).Route = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Dose
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).Dose = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Manufacturer
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).Manufacture = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Expiration_date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).ExpirationDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_Due_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).DueDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Im_CPT_code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oImmunizationCol) Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oImmunizationCol.Item(cnt)) Then
                                        oImmunizationCol.Item(cnt).CPTCode = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                    End Select
                Next
                If Not IsNothing(oImmunizationCol) Then
                    mPatient.PatientImmunizations = oImmunizationCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(oImmunizationCol) Then
                oImmunizationCol.Dispose()
            End If
            _NodeValue = Nothing
            _NodeArrayList = Nothing
        End Try
    End Sub

    'Add By Rohit on 20101015 to Fill Patient Lab Result
    Public Sub FillPatientLabResult(ByVal dt As DataTable)
        Dim oLabResult As LabResults

        Dim oLabResultCol As New LabResultsCol
        Dim _NodeValue As String = ""
        Dim _ID As Int32 = 0
        Dim _NodeArrayList As ArrayList

        Try

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                oLabResultCol = New LabResultsCol()
                _objCCDDatabaseLayer = New gloCCDDatabaseLayer()

                For i As Int32 = 0 To dt.Rows.Count - 1
                    _NodeArrayList = New ArrayList()
                    ' _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString())
                    If _sFileType = "CCD" Then
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5")
                        Else
                            _NodeArrayList = RetriveNodeValue(dt.Rows(i)("sModuleName").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sXMLElementPath").ToString(), "2.5", False)
                        End If
                    Else
                        If dt.Rows(i)("sReferencePath") <> "" Then
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sReferencePath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString(), dt.Rows(i)("sSingleMultipleEntity").ToString(), dt.Rows(i)("sNodeType").ToString())
                        Else
                            _NodeArrayList = FetchNodeValue(dt.Rows(i)("sXMLElementPath").ToString(), dt.Rows(i)("sXMLAttributeName").ToString())
                        End If
                    End If

                    _NodeValue = dt.Rows(i)("sReferenceValue")
                    _ID = dt.Rows(i)("nMappingID")




                    Select Case _ID
                        Case ClseNum.cls_enum_Fields.Lab_Test_Name
                            If Not IsNothing(_NodeArrayList) AndAlso _NodeArrayList.Count > 0 Then
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    oLabResult = New LabResults()
                                    oLabResult.TestName = _NodeArrayList(cnt).ToString()
                                    oLabResult.ResultName = _NodeArrayList(cnt).ToString()
                                    oLabResultCol.Add(oLabResult)
                                    oLabResult = Nothing
                                Next
                            Else
                                Exit Sub
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Order_Provider_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ProviderName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_OrderNo_ID
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).OrderNo = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Test_Code
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).TestCode = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Name
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultName = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Value
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then '_NodeArrayList.Count > 0 And 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultValue = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Units
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And_NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultUnit = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Range
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultRange = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Abnormal_Flag
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).AbnormalFlag = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Type
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then '_NodeArrayList.Count > 0 And 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultType = _NodeArrayList(cnt).ToString()
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_LOINCID
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        If _NodeArrayList(cnt).Contains(_NodeValue) Then
                                            oLabResultCol.Item(cnt).ResultLOINCID = _NodeArrayList(cnt).ToString().Remove(_NodeArrayList(cnt).ToString().IndexOf("*,*"))
                                        End If
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Specimen_Received_Date
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).SpecimenDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                        Case ClseNum.cls_enum_Fields.Lab_Result_Transfer_DateTime
                            If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0 
                                For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                                    If Not IsNothing(oLabResultCol.Item(cnt)) Then
                                        oLabResultCol.Item(cnt).ResultDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                        oLabResultCol.Item(cnt).SpecimenDate = _objCCDDatabaseLayer.ConvertDateTime(_NodeArrayList(cnt).ToString())
                                    End If
                                Next
                            End If
                            'Case ClseNum.cls_enum_Fields.Lab_Result_Data_Type
                            '    If Not IsNothing(_NodeArrayList) AndAlso Not IsNothing(oLabResultCol) Then 'And _NodeArrayList.Count > 0
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '   oLabResultCol.Item(cnt).da = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                            'Case ClseNum.cls_enum_Fields.Lab_Result_Alternate_Name
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oLabResultCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '    oLabResultCol.Item(cnt).Resul = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                            'Case ClseNum.cls_enum_Fields.Lab_Result_Alternate_Code
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oLabResultCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '    oLabResultCol.Item(cnt). = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                            'Case ClseNum.cls_enum_Fields.Lab_File_Order_Identifier
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oLabResultCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '    oLabResultCol.Item(cnt). = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                            'Case ClseNum.cls_enum_Fields.Lab_Producer_Identifier
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oLabResultCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '    oLabResultCol.Item(cnt). = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                            'Case ClseNum.cls_enum_Fields.Lab_Order_Provider_Code
                            '    If Not IsNothing(_NodeArrayList) And _NodeArrayList.Count > 0 And Not IsNothing(oLabResultCol) Then
                            '        For cnt As Int32 = 0 To _NodeArrayList.Count - 1
                            '            If Not IsNothing(oLabResultCol.Item(cnt)) Then
                            '                '    oLabResultCol.Item(cnt). = _NodeArrayList(cnt).ToString()
                            '            End If
                            '        Next
                            '    End If
                    End Select
                    ' Dim olabTestCol As LabTestCol = oLabResultCol
                Next
                If Not IsNothing(oLabResultCol) Then
                    mPatient.PatientLabResult = oLabResultCol
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            If Not IsNothing(oLabResultCol) Then
                oLabResultCol.Dispose()
            End If
        Finally
            _NodeValue = Nothing
            _NodeArrayList = Nothing
        End Try
    End Sub
    
 
End Class

