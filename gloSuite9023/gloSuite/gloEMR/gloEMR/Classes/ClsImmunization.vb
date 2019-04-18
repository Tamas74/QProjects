Imports System.Data.SqlClient
Imports System.IO
Public Class ClsImmunization
    Implements IDisposable

    Private sManufacturer As String
    Private sVaccineName As String
    Private sItemCounterId As Int16
    Private sRecordType As String
    Private sMCIRID As String
    Private sPatientId As String
    Private sDateofEncounter As String
    Private sCPTCode As String
    Private sManufacturerCode As String
    Private sLotNumber As String
    Private sDoseAmount As String

    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
    Private sDoseUnit As String
    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

    Private sReasonforNonAdmin As String


    Private sPersonFName As String
    Private sPersonMName As String
    Private sPersonLName As String
    Private sPersonDOB As String
    Private sPersonCounty As String
    Private sPersonGender As String
    Private sPersonState As String
    Private sPersonCity As String
    Private sPersonZip As String
    Private sPersonAddress As String


    Private sPersonMotherFName As String
    Private sPersonMotherMName As String
    Private sPersonMotherLName As String
    Private sPersonMotherCounty As String
    Private sPersonMotherState As String
    Private sPersonMotherCity As String
    Private sPersonMotherZip As String
    Private sPersonMotherAddress As String

    Private sPersonFatherFName As String
    Private sPersonFatherMName As String
    Private sPersonFatherLName As String
    Private sPersonFatherCounty As String
    Private sPersonFatherState As String
    Private sPersonFatherCity As String
    Private sPersonFatherZip As String
    Private sPersonFatherAddress As String

    Private sPersonGuardianFName As String
    Private sPersonGuardianMName As String
    Private sPersonGuardianLName As String
    Private sPersonGuardianCounty As String
    Private sPersonGuardianState As String
    Private sPersonGuardianCity As String
    Private sPersonGuardianZip As String
    Private sPersonGuardianAddress As String


    Friend PersonSuffix As String              'NR
    Friend BirthLocationfacility As String     'NR
    Friend BirthLocationCounty As String       'NR
    Friend BirthLocationState As String        'NR
    Friend PersonDOD As String                 'NR   Date of Death
    Friend PersonWICNo As String                'NR
    Private sResponsiblePersonFName As String
    Friend ResponsiblePersonMName As String     'NR
    Private sResponsiblePersonLName As String
    Friend ResponsibleSuffix As String          'NR
    Private sResponsiblePersonStreet As String
    Private sResponsiblePersonCity As String
    Private sResponsiblePersonState As String
    Private sResponsiblePersonCounty As String
    Private sResponsiblePersonZip As String
    Private sResponsiblePersonPhone As String
    Private sReminderRecall As String
    Friend MotherFName As String                'NR
    Friend MotherLName As String                'NR
    Friend MotherMaidenName As String           'NR
    Friend ProvidersMCIRID As String            'NR
    Private sVaccByOtherProvider As String
    Private sVaccEligibilityCode As String
    Private sVaccineEligibilityDesc As String
    Private sVaccSiteCode As String
    Private sVaccRouteCode As String
    Friend InitialsofPersonAdministering As String  'NR
    Private sToBePOCForReminders As String
    Private sVaccineCode As String
    Friend PersonMedicAidNumber As String       'NR
    Friend OldVaccineCode As String           'Obsolete
    Friend PersonMedicaidNo As String         'Obsolete
    Friend PersonSSN As String                'Obsolete
    Friend RespPartySSN As String             'Obsolete
    Friend MotherSSN As String                'Obsolete
    Friend Reserved As String                 'Obsolete
    
    Public Property VaccineEligibilityDesc() As String
        Get
            Return sVaccineEligibilityDesc
        End Get
        Set(ByVal value As String)
            sVaccineEligibilityDesc = value
        End Set
    End Property
    Public Property Manufacturer() As String
        Get
            Return sManufacturer
        End Get
        Set(ByVal value As String)
            sManufacturer = value
        End Set
    End Property
    Public Property VaccineName() As String
        Get
            Return sVaccineName
        End Get
        Set(ByVal value As String)
            sVaccineName = value
        End Set
    End Property

    Public Property ItemCounterId() As Int16
        Get
            Return sItemCounterId
        End Get
        Set(ByVal value As Int16)
            sItemCounterId = value
        End Set
    End Property

    Public Property VaccEligibilityCode() As String
        Get
            Return sVaccEligibilityCode
        End Get
        Set(ByVal value As String)
            sVaccEligibilityCode = value
        End Set
    End Property
    Public Property VaccSiteCode() As String
        Get
            Return sVaccSiteCode
        End Get
        Set(ByVal value As String)
            sVaccSiteCode = value
        End Set
    End Property
    Public Property VaccRouteCode() As String
        Get
            Return sVaccRouteCode
        End Get
        Set(ByVal value As String)
            sVaccRouteCode = value
        End Set
    End Property
    Public Property ToBePOCForReminders() As String
        Get
            Return sToBePOCForReminders
        End Get
        Set(ByVal value As String)
            sToBePOCForReminders = value
        End Set
    End Property
    Public Property VaccineCode() As String
        Get
            Return sVaccineCode
        End Get
        Set(ByVal value As String)
            sVaccineCode = value
        End Set
    End Property
    Public Property RecordType() As String
        Get
            Return sRecordType
        End Get
        Set(ByVal value As String)
            sRecordType = value
        End Set
    End Property
    Public Property MCIRID() As String
        Get
            Return sMCIRID
        End Get
        Set(ByVal value As String)
            sMCIRID = value
        End Set
    End Property
    Public Property PatientID() As String
        Get
            Return sPatientId
        End Get
        Set(ByVal value As String)
            sPatientId = value
        End Set
    End Property
    Public Property DateofEncounter() As String
        Get
            Return sDateofEncounter
        End Get
        Set(ByVal value As String)
            sDateofEncounter = value
        End Set
    End Property
    Public Property CPTCode() As String
        Get
            Return sCPTCode
        End Get
        Set(ByVal value As String)
            sCPTCode = value
        End Set
    End Property
    Public Property ManufacturerCode() As String
        Get
            Return sManufacturerCode
        End Get
        Set(ByVal value As String)
            sManufacturerCode = value
        End Set
    End Property
    Public Property LotNumber() As String
        Get
            Return sLotNumber
        End Get
        Set(ByVal value As String)
            sLotNumber = value
        End Set
    End Property
    Public Property DoseAmount() As String
        Get
            Return sDoseAmount
        End Get
        Set(ByVal value As String)
            sDoseAmount = value
        End Set
    End Property

    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
    Public Property DoseUnit() As String
        Get
            Return sDoseUnit
        End Get
        Set(ByVal value As String)
            sDoseUnit = value
        End Set
    End Property
    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

    Public Property ReasonForNonAdmin() As String
        Get
            Return sReasonforNonAdmin
        End Get
        Set(ByVal value As String)
            sReasonforNonAdmin = value
        End Set
    End Property

    '--------Patient Information
    Public Property PersonFName() As String
        Get
            Return sPersonFName
        End Get
        Set(ByVal value As String)
            sPersonFName = value
        End Set
    End Property
    Public Property PersonMName() As String
        Get
            Return sPersonMName
        End Get
        Set(ByVal value As String)
            sPersonMName = value
        End Set
    End Property
    Public Property PersonLName() As String
        Get
            Return sPersonLName
        End Get
        Set(ByVal value As String)
            sPersonLName = value
        End Set
    End Property
    Public Property PersonDOB() As String
        Get
            Return sPersonDOB
        End Get
        Set(ByVal value As String)
            sPersonDOB = value
        End Set
    End Property
    Public Property PersonCounty() As String
        Get
            Return sPersonCounty
        End Get
        Set(ByVal value As String)
            sPersonCounty = value
        End Set
    End Property
    Public Property PersonGender() As String
        Get
            Return sPersonGender
        End Get
        Set(ByVal value As String)
            sPersonGender = value
        End Set
    End Property
    Public Property PersonState() As String
        Get
            Return sPersonState
        End Get
        Set(ByVal value As String)
            sPersonState = value
        End Set
    End Property
    Public Property PersonCity() As String
        Get
            Return sPersonCity
        End Get
        Set(ByVal value As String)
            sPersonCity = value
        End Set
    End Property
    Public Property PersonZip() As String
        Get
            Return sPersonZip
        End Get
        Set(ByVal value As String)
            sPersonZip = value
        End Set
    End Property
    Public Property PersonAddress() As String
        Get
            Return sPersonAddress
        End Get
        Set(ByVal value As String)
            sPersonAddress = value
        End Set
    End Property
    '--------Patient Information


    '--------Patient Mother Information
    Public Property PersonMotherFName() As String
        Get
            Return sPersonMotherFName
        End Get
        Set(ByVal value As String)
            sPersonMotherFName = value
        End Set
    End Property
    Public Property PersonMotherMName() As String
        Get
            Return sPersonMotherMName
        End Get
        Set(ByVal value As String)
            sPersonMotherMName = value
        End Set
    End Property
    Public Property PersonMotherLName() As String
        Get
            Return sPersonMotherLName
        End Get
        Set(ByVal value As String)
            sPersonMotherLName = value
        End Set
    End Property
    Public Property PersonMotherAddress() As String
        Get
            Return sPersonMotherAddress
        End Get
        Set(ByVal value As String)
            sPersonMotherAddress = value
        End Set
    End Property
    Public Property PersonMotherCity() As String
        Get
            Return sPersonMotherCity
        End Get
        Set(ByVal value As String)
            sPersonMotherCity = value
        End Set
    End Property
    Public Property PersonMotherState() As String
        Get
            Return sPersonMotherState
        End Get
        Set(ByVal value As String)
            sPersonMotherState = value
        End Set
    End Property
    Public Property PersonMotherZip() As String
        Get
            Return sPersonMotherZip
        End Get
        Set(ByVal value As String)
            sPersonMotherZip = value
        End Set
    End Property
    Public Property PersonMotherCounty() As String
        Get
            Return sPersonMotherCounty
        End Get
        Set(ByVal value As String)
            sPersonMotherCounty = value
        End Set
    End Property
    '--------Patient Mother Information


    '--------Patient Father Information
    Public Property PersonFatherFName() As String
        Get
            Return sPersonFatherFName
        End Get
        Set(ByVal value As String)
            sPersonFatherFName = value
        End Set
    End Property
    Public Property PersonFatherMName() As String
        Get
            Return sPersonFatherMName
        End Get
        Set(ByVal value As String)
            sPersonFatherMName = value
        End Set
    End Property
    Public Property PersonFatherLName() As String
        Get
            Return sPersonFatherLName
        End Get
        Set(ByVal value As String)
            sPersonFatherLName = value
        End Set
    End Property
    Public Property PersonFatherAddress() As String
        Get
            Return sPersonFatherAddress
        End Get
        Set(ByVal value As String)
            sPersonFatherAddress = value
        End Set
    End Property
    Public Property PersonFatherCity() As String
        Get
            Return sPersonFatherCity
        End Get
        Set(ByVal value As String)
            sPersonFatherCity = value
        End Set
    End Property
    Public Property PersonFatherState() As String
        Get
            Return sPersonFatherState
        End Get
        Set(ByVal value As String)
            sPersonFatherState = value
        End Set
    End Property
    Public Property PersonFatherZip() As String
        Get
            Return sPersonFatherZip
        End Get
        Set(ByVal value As String)
            sPersonFatherZip = value
        End Set
    End Property
    Public Property PersonFatherCounty() As String
        Get
            Return sPersonFatherCounty
        End Get
        Set(ByVal value As String)
            sPersonFatherCounty = value
        End Set
    End Property
    '--------Patient Father Information



    '--------Patient Guardian Information
    Public Property PersonGuardianFName() As String
        Get
            Return sPersonGuardianFName
        End Get
        Set(ByVal value As String)
            sPersonGuardianFName = value
        End Set
    End Property
    Public Property PersonGuardianMName() As String
        Get
            Return sPersonGuardianMName
        End Get
        Set(ByVal value As String)
            sPersonGuardianMName = value
        End Set
    End Property
    Public Property PersonGuardianLName() As String
        Get
            Return sPersonGuardianLName
        End Get
        Set(ByVal value As String)
            sPersonGuardianLName = value
        End Set
    End Property
    Public Property PersonGuardianAddress() As String
        Get
            Return sPersonGuardianAddress
        End Get
        Set(ByVal value As String)
            sPersonGuardianAddress = value
        End Set
    End Property
    Public Property PersonGuardianCity() As String
        Get
            Return sPersonGuardianCity
        End Get
        Set(ByVal value As String)
            sPersonGuardianCity = value
        End Set
    End Property
    Public Property PersonGuardianState() As String
        Get
            Return sPersonGuardianState
        End Get
        Set(ByVal value As String)
            sPersonGuardianState = value
        End Set
    End Property
    Public Property PersonGuardianZip() As String
        Get
            Return sPersonGuardianZip
        End Get
        Set(ByVal value As String)
            sPersonGuardianZip = value
        End Set
    End Property
    Public Property PersonGuardianCounty() As String
        Get
            Return sPersonGuardianCounty
        End Get
        Set(ByVal value As String)
            sPersonGuardianCounty = value
        End Set
    End Property
    '--------Patient Guardian Information



    Public Property ResponsiblePersonFName() As String
        Get
            Return sResponsiblePersonFName
        End Get
        Set(ByVal value As String)
            sResponsiblePersonFName = value
        End Set
    End Property
    Public Property ResponsiblePersonLName() As String
        Get
            Return sResponsiblePersonLName
        End Get
        Set(ByVal value As String)
            sResponsiblePersonLName = value
        End Set
    End Property
    Public Property ResponsiblePersonStreet() As String
        Get
            Return sResponsiblePersonStreet
        End Get
        Set(ByVal value As String)
            sResponsiblePersonStreet = value
        End Set
    End Property
    Public Property ResponsiblePersonCity() As String
        Get
            Return sResponsiblePersonCity
        End Get
        Set(ByVal value As String)
            sResponsiblePersonCity = value
        End Set
    End Property
    Public Property ResponsiblePersonState() As String
        Get
            Return sResponsiblePersonState
        End Get
        Set(ByVal value As String)
            sResponsiblePersonState = value
        End Set
    End Property
    Public Property ResponsiblePersonZip() As String
        Get
            Return sResponsiblePersonZip
        End Get
        Set(ByVal value As String)
            sResponsiblePersonZip = value
        End Set

    End Property
    Public Property ResponsiblePersonPhone() As String
        Get
            Return sResponsiblePersonPhone
        End Get
        Set(ByVal value As String)
            sResponsiblePersonPhone = value
        End Set
    End Property
    Public Property ResponsiblePersonCounty() As String
        Get
            Return sResponsiblePersonCounty
        End Get
        Set(ByVal value As String)
            sResponsiblePersonCounty = value
        End Set
    End Property
    Public Property ReminderRecall() As String
        Get
            Return sReminderRecall
        End Get
        Set(ByVal value As String)
            sReminderRecall = value
        End Set
    End Property
    'sVaccByOtherProvider
    Public Property VaccByOtherProvider() As String
        Get
            Return sVaccByOtherProvider
        End Get
        Set(ByVal value As String)
            sVaccByOtherProvider = value
        End Set
    End Property
    Sub InitialiseObjects()
        sRecordType = ""
        sMCIRID = Strings.Space(12)
        sPatientId = ""
        sDateofEncounter = ""
        sCPTCode = ""
        sManufacturerCode = ""
        sLotNumber = ""
        sDoseAmount = ""
        sReasonforNonAdmin = ""
        sPersonFName = ""
        sPersonMName = Strings.Space(40)
        sPersonLName = ""
        sPersonDOB = ""
        sPersonCounty = ""
        sPersonGender = ""
        PersonSuffix = Strings.Space(10)
        BirthLocationfacility = Strings.Space(50)
        BirthLocationCounty = Strings.Space(2)
        BirthLocationState = Strings.Space(3)
        PersonDOD = Strings.Space(8) 'Date of Death
        PersonWICNo = Strings.Space(11)
        sResponsiblePersonFName = ""
        ResponsiblePersonMName = Strings.Space(1)
        sResponsiblePersonLName = ""
        ResponsibleSuffix = Strings.Space(10)
        sResponsiblePersonStreet = ""
        sResponsiblePersonCity = ""
        sResponsiblePersonState = ""
        sResponsiblePersonCounty = ""
        sResponsiblePersonZip = ""
        sResponsiblePersonPhone = ""
        ReminderRecall = ""
        MotherFName = Strings.Space(40)
        MotherLName = Strings.Space(40)
        MotherMaidenName = Strings.Space(40)
        ProvidersMCIRID = Strings.Space(12)
        sVaccByOtherProvider = ""
        sVaccEligibilityCode = ""
        sVaccSiteCode = ""
        sVaccRouteCode = ""
        InitialsofPersonAdministering = Strings.Space(3)
        sToBePOCForReminders = ""
        sVaccineCode = ""
        PersonMedicAidNumber = Strings.Space(10)
        MotherSSN = Strings.Space(9)
        OldVaccineCode = Strings.Space(2)
        PersonMedicaidNo = Strings.Space(8)
        PersonSSN = Strings.Space(9)
        Reserved = Strings.Space(16)
        RespPartySSN = Strings.Space(9)
        sVaccineName = ""
        sVaccineEligibilityDesc = ""
        sManufacturer = ""
    End Sub


    Private disposedValue As Boolean = False        ' To detect redundant calls

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
        InitialiseObjects()
    End Sub
End Class
Public Class ClsImmunizationException
    Inherits ApplicationException

    Public Sub New(ByVal strmessage As String)
        MyBase.New(strmessage)
    End Sub
End Class
Public Class ClsImmunizationReport
    Implements IDisposable
    'Private objImmunization As ClsImmunization
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Public Event ShowMessage(ByVal strMessage As String, ByRef ID As Int16)
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
    Private StrMessage As String = ""
    Private StrMessageBuilder As New System.Text.StringBuilder
    Public Property ValidationMessage() As String
        Get
            Return StrMessage
        End Get
        Set(ByVal value As String)
            StrMessage = value
        End Set
    End Property
#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Function GenerateImuunizationReport(ByVal arrlst As List(Of ClsImmunization)) As Int32
        Try
            Dim sfilename As String = "MCIR_"
            sfilename = sfilename & CType(Now, String)
            sfilename = Replace(sfilename, "^", "")
            sfilename = Replace(sfilename, "/", "")
            sfilename = Replace(sfilename, ":", "")

            sfilename = Trim(sfilename)
            sfilename = LTrim(RTrim(sfilename))
            sfilename = Replace(sfilename, " ", "")
            For i As Int32 = 0 To arrlst.Count - 1
                GenerateImmunizationReport(arrlst.Item(i), sfilename, i)
            Next
            If StrMessageBuilder.ToString.Length > 0 Then
                StrMessage = StrMessageBuilder.ToString
                Dim id As Int16
                RaiseEvent ShowMessage(StrMessage, id)
                Return id
            Else
                For i As Int32 = 0 To arrlst.Count - 1
                    GenerateFinalReport(arrlst.Item(i), sfilename, i)
                Next
                Return 0
            End If
        Catch ex As ClsImmunizationException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Private Sub GenerateFinalReport(ByVal objImmunization As ClsImmunization, ByVal sfilename As String, ByVal icnt As Integer)
        Dim strImmunizationReport As New System.Text.StringBuilder
        Try
            strImmunizationReport.Append(objImmunization.RecordType)
            strImmunizationReport.Append(objImmunization.MCIRID)
            strImmunizationReport.Append(objImmunization.PatientID)
            strImmunizationReport.Append(objImmunization.DateofEncounter)
            strImmunizationReport.Append(objImmunization.OldVaccineCode)        'Obsolete
            strImmunizationReport.Append(objImmunization.CPTCode)
            strImmunizationReport.Append(objImmunization.ManufacturerCode)
            strImmunizationReport.Append(objImmunization.LotNumber)
            strImmunizationReport.Append(objImmunization.DoseAmount)
            strImmunizationReport.Append(objImmunization.ReasonForNonAdmin)
            strImmunizationReport.Append(objImmunization.PersonFName)
            strImmunizationReport.Append(objImmunization.PersonLName)
            strImmunizationReport.Append(objImmunization.PersonMName)
            strImmunizationReport.Append(objImmunization.PersonDOB)
            strImmunizationReport.Append(objImmunization.PersonCounty)
            strImmunizationReport.Append(objImmunization.PersonGender)
            strImmunizationReport.Append(objImmunization.PersonSuffix)
            strImmunizationReport.Append(objImmunization.BirthLocationfacility)
            strImmunizationReport.Append(objImmunization.BirthLocationCounty)
            strImmunizationReport.Append(objImmunization.BirthLocationState)
            strImmunizationReport.Append(objImmunization.PersonDOD)                 'Date of Death
            strImmunizationReport.Append(objImmunization.PersonMedicaidNo)          'Obsolete
            strImmunizationReport.Append(objImmunization.PersonWICNo)
            strImmunizationReport.Append(objImmunization.PersonSSN)                 'Obsolete 
            strImmunizationReport.Append(objImmunization.ResponsiblePersonLName)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonFName)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonMName)
            strImmunizationReport.Append(objImmunization.ResponsibleSuffix)
            strImmunizationReport.Append(objImmunization.RespPartySSN)              'Obsolete
            strImmunizationReport.Append(objImmunization.ResponsiblePersonStreet)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonCity)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonState)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonCounty)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonZip)
            strImmunizationReport.Append(objImmunization.ResponsiblePersonPhone)
            strImmunizationReport.Append(objImmunization.ReminderRecall)
            strImmunizationReport.Append(objImmunization.MotherFName)
            strImmunizationReport.Append(objImmunization.MotherLName)
            strImmunizationReport.Append(objImmunization.MotherSSN)                 'Obsolete
            strImmunizationReport.Append(objImmunization.MotherMaidenName)
            strImmunizationReport.Append(objImmunization.ProvidersMCIRID)
            strImmunizationReport.Append(objImmunization.VaccByOtherProvider)
            strImmunizationReport.Append(objImmunization.VaccEligibilityCode)
            strImmunizationReport.Append(objImmunization.VaccSiteCode)
            strImmunizationReport.Append(objImmunization.VaccRouteCode)
            strImmunizationReport.Append(objImmunization.InitialsofPersonAdministering)
            strImmunizationReport.Append(objImmunization.ToBePOCForReminders)
            strImmunizationReport.Append(objImmunization.VaccineCode)
            strImmunizationReport.Append(objImmunization.Reserved)                  'Obsolete
            strImmunizationReport.Append(objImmunization.PersonMedicAidNumber)
            strImmunizationReport.Append(vbCrLf)

            Dim objFile As New System.IO.StreamWriter(gstrMCIRReportPath & "\" & sfilename & ".txt", True)
            objFile.WriteLine(strImmunizationReport.ToString())
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
            'Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Function GenerateImmunizationReport(ByVal objImmunization As ClsImmunization, ByVal sfilename As String, ByVal icnt As Integer) As Int16
        Dim strImmunizationReport As New System.Text.StringBuilder
        Try
            If GetPersonData(objImmunization, icnt) Then
                'If GetVaccineData(objImmunization) Then

                If GetManufacturerData(objImmunization) Then
                    If GetEligibilityData(objImmunization) Then
                        If ValidatePatientInfo(objImmunization, icnt) Then
                            If ValidateData(objImmunization) Then
                                Insertspacesforpatient(objImmunization, icnt)
                                InsertSpaces(objImmunization)
                                If ValidateLengthforPatient(objImmunization, icnt) Then
                                    If ValidateLength(objImmunization) Then
                                        Return 0
                                       
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                'End If
            End If
            Return Nothing
        Catch ex As ClsImmunizationException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GenerateImmunizationReport = Nothing
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GenerateImmunizationReport = Nothing
            Throw New ClsImmunizationException(ex.ToString)

        End Try
    End Function


    'Private Function GetPersonData(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
    '    Dim sqlconn As SqlConnection
    '    Dim sqlcmd As SqlCommand
    '    Dim strsqlconn As String = ""
    '    strsqlconn = GetConnectionString()
    '    sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
    '    sqlconn.Open()
    '    Dim strsql As String = ""
    '    Dim dr As SqlDataReader
    '    Try
    '        'strsql = "select distinct isnull(sFirstName,''),isnull(sMiddleName,''),isnull(sLastName,''), " & _
    '        '        "isnull(sGender,''),isnull(sGuardian_fName,''),isnull(sGuardian_lName,'')," & _
    '        '        "isnull(sGuardian_Address1,'') + space(1) + isnull(sGuardian_Address2,''),isnull(sGuardian_City,'')," & _
    '        '        "isnull(sGuardian_State,''),isnull(sGuardian_County,''),isnull(sGuardian_ZIP,'')," & _
    '        '        "isnull(sGuardian_Phone,'') ,nProviderID,isnull(CountyCode,''),dtdob " & _
    '        '        "from Patient left outer join csz_mst on patient.scounty=csz_mst.county " & _
    '        '        "where sPatientCode='" & objImmunization.PatientID & "'"

    '        strsql = "SELECT DISTINCT ISNULL(Patient.sFirstName, ''), ISNULL(Patient.sMiddleName, ''), ISNULL(Patient.sLastName, '') ," & _
    '                  "ISNULL(Patient.sGender,''), ISNULL(Patient.sGuardian_fName, ''), ISNULL(Patient.sGuardian_lName, '')," & _
    '                  "ISNULL(Patient.sGuardian_Address1, '') + SPACE(1) + ISNULL(Patient.sGuardian_Address2, ''), ISNULL(Patient.sGuardian_City, ''), " & _
    '                  "ISNULL(Patient.sGuardian_State, '') , ISNULL(Patient.sGuardian_County, '') , ISNULL(Patient.sGuardian_ZIP, '')," & _
    '                  "ISNULL(Patient.sGuardian_Phone, '') , Patient.nProviderID,ISNULL(County_Dtl.CountyCode,''), Patient.dtDOB " & _
    '                  "FROM Patient left outer join County_Dtl ON Patient.sZIP = County_Dtl.ZIP where sPatientCode= '" & objImmunization.PatientID & "'"
    '        sqlcmd = New SqlCommand(strsql, sqlconn)
    '        dr = sqlcmd.ExecuteReader
    '        If Not IsNothing(dr) Then
    '            While dr.Read
    '                objImmunization.PersonFName = CType(dr.Item(0), String)
    '                'objImmunization.PersonLName = CType(dr.Item(1), String)
    '                objImmunization.PersonLName = CType(dr.Item(2), String)
    '                Select Case CType(dr.Item(3), String)
    '                    Case "Male"
    '                        objImmunization.PersonGender = "M"

    '                    Case "Female"
    '                        objImmunization.PersonGender = "F"

    '                End Select
    '                objImmunization.ResponsiblePersonFName = CType(dr.Item(4), String)
    '                objImmunization.ResponsiblePersonLName = CType(dr.Item(5), String)
    '                objImmunization.ResponsiblePersonStreet = CType(dr.Item(6), String)
    '                objImmunization.ResponsiblePersonCity = CType(dr.Item(7), String)
    '                objImmunization.ResponsiblePersonState = CType(dr.Item(8), String)
    '                objImmunization.ResponsiblePersonCounty = CType(dr.Item(9), String)
    '                objImmunization.ResponsiblePersonZip = CType(dr.Item(10), String)
    '                objImmunization.ResponsiblePersonPhone = CType(dr.Item(11), String)
    '                'If CType(dr.Item(11), String) <> objImmunization.VaccByOtherProvider Then
    '                objImmunization.VaccByOtherProvider = "O"
    '                objImmunization.ToBePOCForReminders = "N"
    '                'Else
    '                'objImmunization.VaccByOtherProvider = "U"
    '                'End If
    '                objImmunization.PersonCounty = CType(dr.Item(13), String)
    '                objImmunization.PersonDOB = CType(dr.Item(14), String)
    '            End While
    '            dr.Close()
    '            Return True
    '        End If

    '    Catch ex As Exception
    '        Throw New ClsImmunizationException(ex.ToString)

    '    Finally
    '        If Not IsNothing(dr) Then
    '            dr.Close()
    '            dr = Nothing
    '        End If
    '        If Not IsNothing(sqlcmd) Then
    '            sqlcmd.Dispose()
    '            sqlcmd = Nothing
    '        End If
    '        If Not IsNothing(sqlconn) Then
    '            If sqlconn.State = ConnectionState.Open Then
    '                sqlconn.Close()
    '            End If
    '            sqlconn.Dispose()
    '            sqlconn = Nothing
    '        End If
    '    End Try
    'End Function



    Public Function GetPersonData(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
        Dim sqlconn As SqlConnection
        Dim sqlcmd As SqlCommand = Nothing
        Dim strsqlconn As String = ""
        strsqlconn = GetConnectionString()
        sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
        sqlconn.Open()
        Dim strsql As String = ""
        Dim dr As SqlDataReader = Nothing
        Try
            'strsql = "select distinct isnull(sFirstName,''),isnull(sMiddleName,''),isnull(sLastName,''), " & _
            '        "isnull(sGender,''),isnull(sGuardian_fName,''),isnull(sGuardian_lName,'')," & _
            '        "isnull(sGuardian_Address1,'') + space(1) + isnull(sGuardian_Address2,''),isnull(sGuardian_City,'')," & _
            '        "isnull(sGuardian_State,''),isnull(sGuardian_County,''),isnull(sGuardian_ZIP,'')," & _
            '        "isnull(sGuardian_Phone,'') ,nProviderID,isnull(CountyCode,''),dtdob " & _
            '        "from Patient left outer join csz_mst on patient.scounty=csz_mst.county " & _
            '        "where sPatientCode='" & objImmunization.PatientID & "'"

            'strsql = "SELECT DISTINCT ISNULL(Patient.sFirstName, ''), ISNULL(Patient.sMiddleName, ''), ISNULL(Patient.sLastName, '') ," & _
            '          "ISNULL(Patient.sGender,''), ISNULL(Patient.sGuardian_fName, ''), ISNULL(Patient.sGuardian_lName, '')," & _
            '          "ISNULL(Patient.sGuardian_Address1, '') + SPACE(1) + ISNULL(Patient.sGuardian_Address2, ''), ISNULL(Patient.sGuardian_City, ''), " & _
            '          "ISNULL(Patient.sGuardian_State, '') , ISNULL(Patient.sGuardian_County, '') , ISNULL(Patient.sGuardian_ZIP, '')," & _
            '          "ISNULL(Patient.sGuardian_Phone, '') , Patient.nProviderID,ISNULL(County_Dtl.CountyCode,''), Patient.dtDOB " & _
            '          "FROM Patient left outer join County_Dtl ON Patient.sZIP = County_Dtl.ZIP where sPatientCode= '" & objImmunization.PatientID & "'"


            'strsql = "SELECT distinct isnull(sFirstName,'') as PatFname, isnull(sMiddleName,'') as PatMName, isnull(sLastName,'') as PatLName , isnull(dtDOB,'') as PatDOB , isnull(sGender,'') as PatGender , isnull(sMaritalStatus,'') as PatMaritalStat , isnull(sAddressLine1,'') + Space(1) + isnull(sAddressLine2,'') as PatAddress, isnull(sCity,'') as PatCity , isnull(sState,'') as PatState , isnull(patient.sZIP,'') as PatZip, isnull(county_dtl.CountyCode,'') as PatCounty,nProviderID as PatProviderId, " & _
            '         "isnull(sMother_fName,'') as PatMotherFname, isnull(sMother_mName,'') as PatMotherMName, isnull(sMother_lName,'') as PatMotherLName, isnull(sMother_Address1,'') + space(1) + isnull(sMother_Address2,'') as PatMotherAddress, isnull(sMother_City,'') as PatMotherCity, isnull(sMother_State,'') as PatMotherState, isnull(sMother_ZIP,'') as PatMotherZip, " & _
            '         "isnull(sMother_County,'') as PatMotherCounty, isnull(sFather_fName,'') as PatFatherFname, isnull(sFather_mName,'') as PatFatherMName, isnull(sFather_lName,'') as PatFatherLName, isnull(sFather_Address1,'') + space(1) + isnull(sFather_Address2,'') as PatFatherAddress, isnull(sFather_City,'') as PatFatherCity, isnull(sFather_State,'') as PatFatherState, isnull(sFather_ZIP,'') as PatFatherZip, " & _
            '         "isnull(sFather_County,'') as PatFatherCounty, isnull(sGuardian_fName,'') as PatGuardianFName, isnull(sGuardian_mName,'') as PatGuardianMName, isnull(sGuardian_lName,'') as PatGuardianLName, isnull(sGuardian_Address1,'') + Space(1) + isnull(sGuardian_Address2,'') as PatGuardianAddress, isnull(sGuardian_City,'') as PatGuardianCity, " & _
            '         "isnull(sGuardian_State,'') as PatGuardianState, isnull(sGuardian_ZIP,'') as patGuardianZip, isnull(sGuardian_County,'') as PatGuardianCounty" & _
            '         " FROM Patient left outer join csz_mst on patient.szip = convert(varchar(50), csz_mst.zip) left outer join county_dtl on convert(varchar(50),csz_mst.zip) = county_dtl.zip where sPatientCode= " & "'" & "" & objImmunization.PatientID.Replace("'", "" & "'" & "'") & "" & "'" & ""

            strsql = "SELECT distinct isnull(sFirstName,'') as PatFname, isnull(sMiddleName,'') as PatMName, isnull(sLastName,'') as PatLName , isnull(dtDOB,'') as PatDOB , isnull(sGender,'') as PatGender , isnull(sMaritalStatus,'') as PatMaritalStat , isnull(sAddressLine1,'') + Space(1) + isnull(sAddressLine2,'') as PatAddress, isnull(sCity,'') as PatCity , isnull(sState,'') as PatState , isnull(patient.sZIP,'') as PatZip, isnull(county_dtl.CountyCode,'') as PatCounty,nProviderID as PatProviderId, " & _
                                          "isnull(sMother_fName,'') as PatMotherFname, isnull(sMother_mName,'') as PatMotherMName, isnull(sMother_lName,'') as PatMotherLName, isnull(sMother_Address1,'') + space(1) + isnull(sMother_Address2,'') as PatMotherAddress, isnull(sMother_City,'') as PatMotherCity, isnull(sMother_State,'') as PatMotherState, isnull(sMother_ZIP,'') as PatMotherZip, " & _
                                          "isnull(sMother_County,'') as PatMotherCounty, isnull(sFather_fName,'') as PatFatherFname, isnull(sFather_mName,'') as PatFatherMName, isnull(sFather_lName,'') as PatFatherLName, isnull(sFather_Address1,'') + space(1) + isnull(sFather_Address2,'') as PatFatherAddress, isnull(sFather_City,'') as PatFatherCity, isnull(sFather_State,'') as PatFatherState, isnull(sFather_ZIP,'') as PatFatherZip, " & _
                                          "isnull(sFather_County,'') as PatFatherCounty, isnull(sGuardian_fName,'') as PatGuardianFName, isnull(sGuardian_mName,'') as PatGuardianMName, isnull(sGuardian_lName,'') as PatGuardianLName, isnull(sGuardian_Address1,'') + Space(1) + isnull(sGuardian_Address2,'') as PatGuardianAddress, isnull(sGuardian_City,'') as PatGuardianCity, " & _
                                          "isnull(sGuardian_State,'') as PatGuardianState, isnull(sGuardian_ZIP,'') as patGuardianZip, isnull(sGuardian_County,'') as PatGuardianCounty" & _
                                          " FROM Patient left outer join county_dtl on patient.szip = county_dtl.zip where sPatientCode= " & "'" & "" & objImmunization.PatientID.Replace("'", "" & "'" & "'") & "" & "'" & ""

            'strsql = "select * from patient where sPatientCode = " & "'" & "" & objImmunization.PatientID & "" & "'" & ""

            sqlcmd = New SqlCommand(strsql, sqlconn)
            dr = sqlcmd.ExecuteReader
            If Not IsNothing(dr) Then
                While dr.Read

                    '--------Patient Info
                    objImmunization.PersonFName = CType(dr.Item("PatFname"), String)
                    objImmunization.PersonMName = CType(dr.Item("PatMName"), String)
                    objImmunization.PersonLName = CType(dr.Item("PatLName"), String)
                    Select Case CType(dr.Item("PatGender"), String)
                        Case "Male"
                            objImmunization.PersonGender = "M"

                        Case "Female"
                            objImmunization.PersonGender = "F"

                    End Select
                    objImmunization.PersonCounty = CType(dr.Item("PatCounty"), String)
                    objImmunization.PersonDOB = CType(dr.Item("PatDOB"), String)
                    objImmunization.PersonState = CType(dr.Item("PatState"), String)
                    objImmunization.PersonCity = CType(dr.Item("PatCity"), String)
                    objImmunization.PersonZip = CType(dr.Item("PatZip"), String)
                    objImmunization.PersonAddress = CType(dr.Item("PatAddress"), String)
                    '--------Patient Info

                    '--------Patient Mother Info
                    objImmunization.PersonMotherFName = CType(dr.Item("PatMotherFname"), String)
                    objImmunization.PersonMotherMName = CType(dr.Item("PatMotherMName"), String)
                    objImmunization.PersonMotherLName = CType(dr.Item("PatMotherLName"), String)
                    objImmunization.PersonMotherAddress = CType(dr.Item("PatMotherAddress"), String)
                    objImmunization.PersonMotherCity = CType(dr.Item("PatMotherCity"), String)
                    objImmunization.PersonMotherState = CType(dr.Item("PatMotherState"), String)
                    objImmunization.PersonMotherZip = CType(dr.Item("PatMotherZip"), String)
                    objImmunization.PersonMotherCounty = CType(dr.Item("PatMotherCounty"), String)
                    '--------Patient Mother Info

                    '--------Patient Father Info
                    objImmunization.PersonFatherFName = CType(dr.Item("PatFatherFname"), String)
                    objImmunization.PersonFatherMName = CType(dr.Item("PatFatherMName"), String)
                    objImmunization.PersonFatherLName = CType(dr.Item("PatFatherLName"), String)
                    objImmunization.PersonFatherAddress = CType(dr.Item("PatFatherAddress"), String)
                    objImmunization.PersonFatherCity = CType(dr.Item("PatFatherCity"), String)
                    objImmunization.PersonFatherState = CType(dr.Item("PatFatherState"), String)
                    objImmunization.PersonFatherZip = CType(dr.Item("PatFatherZip"), String)
                    objImmunization.PersonFatherCounty = CType(dr.Item("PatFatherCounty"), String)
                    '--------Patient Father Info


                    '--------Patient Guardian Info
                    objImmunization.PersonGuardianFName = CType(dr.Item("PatGuardianFName"), String)
                    objImmunization.PersonGuardianMName = CType(dr.Item("PatGuardianMName"), String)
                    objImmunization.PersonGuardianLName = CType(dr.Item("PatGuardianLName"), String)
                    objImmunization.PersonGuardianAddress = CType(dr.Item("PatGuardianAddress"), String)
                    objImmunization.PersonGuardianCity = CType(dr.Item("PatGuardianCity"), String)
                    objImmunization.PersonGuardianState = CType(dr.Item("PatGuardianState"), String)
                    objImmunization.PersonGuardianZip = CType(dr.Item("patGuardianZip"), String)
                    objImmunization.PersonGuardianCounty = CType(dr.Item("PatGuardianCounty"), String)
                    '--------Patient Guardian Info

                    'objImmunization.ResponsiblePersonFName = CType(dr.Item(4), String)
                    'objImmunization.ResponsiblePersonLName = CType(dr.Item(5), String)
                    'objImmunization.ResponsiblePersonStreet = CType(dr.Item(6), String)
                    'objImmunization.ResponsiblePersonCity = CType(dr.Item(7), String)
                    'objImmunization.ResponsiblePersonState = CType(dr.Item(8), String)
                    'objImmunization.ResponsiblePersonCounty = CType(dr.Item(9), String)
                    'objImmunization.ResponsiblePersonZip = CType(dr.Item(10), String)
                    'objImmunization.ResponsiblePersonPhone = CType(dr.Item(11), String)
                    'If CType(dr.Item(11), String) <> objImmunization.VaccByOtherProvider Then
                    objImmunization.VaccByOtherProvider = "O"
                    objImmunization.ToBePOCForReminders = "N"
                    'Else
                    'objImmunization.VaccByOtherProvider = "U"
                    'End If

                End While
                dr.Close()
                Return True
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GetPersonData = Nothing
            Throw New ClsImmunizationException(ex.ToString)

        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr = Nothing
            End If
            If Not IsNothing(sqlcmd) Then
                sqlcmd.Parameters.Clear()
                sqlcmd.Dispose()
                sqlcmd = Nothing
            End If
            If Not IsNothing(sqlconn) Then
                If sqlconn.State = ConnectionState.Open Then
                    sqlconn.Close()
                End If
                sqlconn.Dispose()
                sqlconn = Nothing
            End If
        End Try
    End Function


    Private Function GetVaccineData(ByVal objImmunization As ClsImmunization) As Boolean
        Dim sqlconn As SqlConnection
        Dim sqlcmd As SqlCommand = Nothing
        Dim strsqlconn As String = ""
        strsqlconn = GetConnectionString()
        sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
        sqlconn.Open()
        Dim strsql As String = ""
        Dim dr As SqlDataReader = Nothing
        Try
            strsql = "select isnull(im_vaccine_code,''),isnull(im_cpt_code,'') from  IM_MST where im_item_Id=" & objImmunization.VaccineName & ""
            sqlcmd = New SqlCommand(strsql, sqlconn)
            dr = sqlcmd.ExecuteReader
            If Not IsNothing(dr) Then
                While dr.Read

                    objImmunization.VaccineCode = CType(dr.Item(0), String)
                    objImmunization.CPTCode = CType(dr.Item(1), String)
                End While
                dr.Close()
                Return True

            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GetVaccineData = Nothing
            Throw New ClsImmunizationException(ex.ToString)

        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr = Nothing
            End If
            If Not IsNothing(sqlcmd) Then
                sqlcmd.Parameters.Clear()
                sqlcmd.Dispose()
                sqlcmd = Nothing
            End If
            If Not IsNothing(sqlconn) Then
                If sqlconn.State = ConnectionState.Open Then
                    sqlconn.Close()
                End If
                sqlconn.Dispose()
                sqlconn = Nothing
            End If
        End Try
    End Function
    Private Function GetManufacturerData(ByVal objImmunization As ClsImmunization) As Boolean
        Dim sqlconn As SqlConnection
        Dim sqlcmd As SqlCommand = Nothing
        Dim strsqlconn As String = ""
        strsqlconn = GetConnectionString()
        sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
        sqlconn.Open()
        Dim strsql As String = ""
        Dim dr As SqlDataReader = Nothing
        Try
            strsql = "select isnull(sManufacturerCode,'') from  Im_Manufacturers where sManufacturer='" & objImmunization.Manufacturer & "' and sCodeType ='Manufacturer'"
            sqlcmd = New SqlCommand(strsql, sqlconn)
            dr = sqlcmd.ExecuteReader
            If Not IsNothing(dr) Then
                While dr.Read
                    objImmunization.ManufacturerCode = CType(dr.Item(0), String)
                End While
                dr.Close()
                Return True

            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GetManufacturerData = Nothing
            Throw New ClsImmunizationException(ex.ToString)

        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr = Nothing
            End If
            If Not IsNothing(sqlcmd) Then
                sqlcmd.Parameters.Clear()
                sqlcmd.Dispose()
                sqlcmd = Nothing
            End If
            If Not IsNothing(sqlconn) Then
                If sqlconn.State = ConnectionState.Open Then
                    sqlconn.Close()
                End If
                sqlconn.Dispose()
                sqlconn = Nothing
            End If
        End Try
    End Function
    Private Function GetEligibilityData(ByVal objImmunization As ClsImmunization) As Boolean
        Dim sqlconn As SqlConnection
        Dim sqlcmd As SqlCommand = Nothing
        Dim strsqlconn As String = ""
        strsqlconn = GetConnectionString()
        sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
        sqlconn.Open()
        Dim strsql As String = ""
        Dim dr As SqlDataReader = Nothing
        Try
            strsql = "select isnull(sManufacturerCode,'') from  Im_Manufacturers where sManufacturer='" & objImmunization.VaccineEligibilityDesc & "' and sCodeType ='VaccineEligibility'"
            sqlcmd = New SqlCommand(strsql, sqlconn)
            dr = sqlcmd.ExecuteReader
            If Not IsNothing(dr) Then
                While dr.Read
                    objImmunization.VaccEligibilityCode = CType(dr.Item(0), String)
                End While
                dr.Close()
                Return True

            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            GetEligibilityData = Nothing
            Throw New ClsImmunizationException(ex.ToString)

        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr = Nothing
            End If
            If Not IsNothing(sqlcmd) Then
                sqlcmd.Parameters.Clear()
                sqlcmd.Dispose()
                sqlcmd = Nothing
            End If
            If Not IsNothing(sqlconn) Then
                If sqlconn.State = ConnectionState.Open Then
                    sqlconn.Close()
                End If
                sqlconn.Dispose()
                sqlconn = Nothing
            End If
        End Try
    End Function
    Private Function Insertspacesforpatient(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
        Try

            If objImmunization.PatientID.Trim.Length < 20 Then
                objImmunization.PatientID = objImmunization.PatientID.Trim & Strings.Space(20 - objImmunization.PatientID.Trim.Length)
            End If

            If objImmunization.PersonFName.Trim.Length < 40 Then
                objImmunization.PersonFName = objImmunization.PersonFName.Trim & Strings.Space(40 - objImmunization.PersonFName.Trim.Length)
            End If
            If objImmunization.PersonLName.Trim.Length < 40 Then
                objImmunization.PersonLName = objImmunization.PersonLName.Trim & Strings.Space(40 - objImmunization.PersonLName.Trim.Length)
            End If

            If objImmunization.ResponsiblePersonFName.Trim.Length < 40 Then
                objImmunization.ResponsiblePersonFName = objImmunization.ResponsiblePersonFName.Trim & Strings.Space(40 - objImmunization.ResponsiblePersonFName.Trim.Length)
            End If
            If objImmunization.ResponsiblePersonLName.Trim.Length < 40 Then
                objImmunization.ResponsiblePersonLName = objImmunization.ResponsiblePersonLName.Trim & Strings.Space(40 - objImmunization.ResponsiblePersonLName.Trim.Length)
            End If
            If objImmunization.ResponsiblePersonCity.Trim.Length < 30 Then
                objImmunization.ResponsiblePersonCity = objImmunization.ResponsiblePersonCity.Trim & Strings.Space(30 - objImmunization.ResponsiblePersonCity.Trim.Length)
            End If
            If objImmunization.ResponsiblePersonStreet.Trim.Length < 40 Then
                objImmunization.ResponsiblePersonStreet = objImmunization.ResponsiblePersonStreet.Trim & Strings.Space(40 - objImmunization.ResponsiblePersonStreet.Trim.Length)
            End If
            If objImmunization.ResponsiblePersonState.Trim.Length < 3 Then
                objImmunization.ResponsiblePersonState = objImmunization.ResponsiblePersonState.Trim & Strings.Space(3 - objImmunization.ResponsiblePersonState.Trim.Length)
            End If
            If objImmunization.ResponsiblePersonZip.Trim.Length < 10 Then
                objImmunization.ResponsiblePersonZip = objImmunization.ResponsiblePersonZip.Trim & Strings.Space(10 - objImmunization.ResponsiblePersonZip.Trim.Length)
            End If
            'If objImmunization.ResponsiblePersonCounty.Trim.Length < 6 Then
            '    objImmunization.ResponsiblePersonCounty = objImmunization.ResponsiblePersonCounty.Trim & Strings.Space(6) ' - objImmunization.ResponsiblePersonCounty.Trim.Length)
            'End If

            If objImmunization.ResponsiblePersonCounty.Trim.Length <> 0 Then
                objImmunization.ResponsiblePersonCounty = ""
                objImmunization.ResponsiblePersonCounty = Strings.Space(6) ' - objImmunization.ResponsiblePersonCounty.Trim.Length)
            End If
            If objImmunization.PersonDOB.Trim.Length <> 0 Then
                Dim dtdate As Date
                dtdate = Convert.ToDateTime(objImmunization.PersonDOB)
                Dim strday As String = ""
                Dim strmonth As String = ""
                Dim stryear As String = ""

                strday = CType(dtdate.Day, String)
                If strday.Length = 1 Then
                    strday = "0" & strday
                End If

                strmonth = CType(dtdate.Month, String)
                If strmonth.Length = 1 Then
                    strmonth = "0" & strmonth
                End If
                stryear = CType(dtdate.Year, String)

                objImmunization.PersonDOB = stryear & strmonth & strday
            End If
            If objImmunization.DateofEncounter.Trim.Length <> 0 Then
                Dim dtdate As Date
                dtdate = Convert.ToDateTime(objImmunization.DateofEncounter)
                Dim strday As String = ""
                Dim strmonth As String = ""
                Dim stryear As String = ""

                strday = CType(dtdate.Day, String)
                If strday.Length = 1 Then
                    strday = "0" & strday
                End If

                strmonth = CType(dtdate.Month, String)
                If strmonth.Length = 1 Then
                    strmonth = "0" & strmonth
                End If

                stryear = CType(dtdate.Year, String)
                objImmunization.DateofEncounter = stryear & strmonth & strday
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return Nothing
    End Function
    Private Function InsertSpaces(ByVal objImmunization As ClsImmunization) As Boolean
        Try
            If objImmunization.CPTCode.Trim.Length < 5 Then
                objImmunization.CPTCode = objImmunization.CPTCode.Trim & Strings.Space(5 - objImmunization.CPTCode.Trim.Length)
            End If

            If objImmunization.LotNumber.Trim.Length < 20 Then
                objImmunization.LotNumber = objImmunization.LotNumber.Trim & Strings.Space(20 - objImmunization.LotNumber.Trim.Length)
            End If

            If objImmunization.DoseAmount.Trim.Length < 5 Then
                objImmunization.DoseAmount = objImmunization.DoseAmount.Trim & Strings.Space(5 - objImmunization.DoseAmount.Trim.Length)
            End If
           
            If objImmunization.VaccineCode.Trim.Length < 4 Then
                objImmunization.VaccineCode = objImmunization.VaccineCode.Trim & Strings.Space(4 - objImmunization.VaccineCode.Trim.Length)
            End If
            If objImmunization.VaccSiteCode.Trim.Length <> 0 Then
                Select Case objImmunization.VaccSiteCode

                    Case "LA"
                        objImmunization.VaccSiteCode = "L"
                    Case "LT"
                        objImmunization.VaccSiteCode = "T"

                    Case "RA"
                        objImmunization.VaccSiteCode = "R"

                    Case "RT"
                        objImmunization.VaccSiteCode = "H"

                End Select
            Else
                objImmunization.VaccSiteCode = objImmunization.VaccSiteCode.Trim & Strings.Space(1)
            End If
            If objImmunization.VaccRouteCode.Trim.Length <> 0 Then
                Select Case objImmunization.VaccRouteCode

                    Case "IM"
                        objImmunization.VaccRouteCode = "M"
                    Case "SC"
                        objImmunization.VaccRouteCode = "S"

                    Case "PO"
                        objImmunization.VaccRouteCode = "O"

                    Case "ID"
                        objImmunization.VaccRouteCode = "D"

                End Select
            Else
                objImmunization.VaccRouteCode = objImmunization.VaccRouteCode.Trim & Strings.Space(1)
            End If
            
            If objImmunization.ReasonForNonAdmin.Trim.Length = 0 Then
                objImmunization.ReasonForNonAdmin = Strings.Space(2)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New ClsImmunizationException(ex.ToString)

        End Try
        Return Nothing
    End Function


    'Orignal
    'Private Function ValidatePatientInfo(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
    '    Try
    '        If icnt = 0 Then


    '            If objImmunization.RecordType.Trim.Length = 0 Then
    '                'MessageBox.Show("RecordType missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("RecordType missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.PatientID.Trim.Length = 0 Then
    '                'MessageBox.Show("PatientID missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("PatientID missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.DateofEncounter.Trim.Length = 0 Then
    '                'MessageBox.Show(vbCrLf & "Date of Encounter missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Date of Encounter missing for " & objImmunization.VaccineName & vbCrLf)
    '            End If
    '            If objImmunization.PersonFName.Trim.Length = 0 Then
    '                'MessageBox.Show("Person first Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Person first Name missing" & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.PersonLName.Trim.Length = 0 Then
    '                'MessageBox.Show("Person Last Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Person Last Name missing" & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.PersonDOB.Trim.Length = 0 Then
    '                'MessageBox.Show("Person DOB missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Person DOB missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.PersonCounty.Trim.Length = 0 Then
    '                'MessageBox.Show("Person County missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Person County missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.PersonGender.Trim.Length = 0 Then
    '                'MessageBox.Show("Person Gender missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Person Gender missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonFName.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person First Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person First Name missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonLName.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person Last Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person Last Name missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonCity.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person City missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person City missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonState.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person State missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person State missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonStreet.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person Street missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person Street missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonZip.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person Zip missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person Zip missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '            If objImmunization.ResponsiblePersonCounty.Trim.Length = 0 Then
    '                'MessageBox.Show("Responsible Person Country missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                StrMessageBuilder.Append("Responsible Person Zip missing " & vbCrLf)
    '                'Exit Function
    '            End If
    '        End If
    '        Return True
    '    Catch ex As Exception

    '    End Try

    'End Function

    '''********************Orignal

    Private Function ValidatePatientInfo(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
        Try
            If icnt = 0 Then

               

                If objImmunization.RecordType.Trim.Length = 0 Then
                    'MessageBox.Show("RecordType missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    StrMessageBuilder.Append("RecordType missing " & vbCrLf)
                    'Exit Function
                End If
                If objImmunization.PatientID.Trim.Length = 0 Then
                    'MessageBox.Show("PatientID missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    StrMessageBuilder.Append("PatientID missing " & vbCrLf)
                    'Exit Function
                End If
                If objImmunization.DateofEncounter.Trim.Length = 0 Then
                    'MessageBox.Show(vbCrLf & "Date of Encounter missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    StrMessageBuilder.Append("Date of Encounter missing for " & objImmunization.VaccineName & objImmunization.ItemCounterId & vbCrLf)
                End If

                '-----Person
                Dim strbldPatientInfo As New System.Text.StringBuilder
                Dim BoolPatientInfo As Boolean = False
                If objImmunization.PersonFName.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("First Name, ")
                    'MessageBox.Show("Person first Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person first Name missing" & vbCrLf)
                    'Person = Person & "first Name, "

                    'Exit Function
                End If
                If objImmunization.PersonLName.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("Last Name, ")
                    'MessageBox.Show("Person Last Name missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person Last Name missing" & vbCrLf)
                    'Person = Person & "Last Name, "
                    'Exit Function
                End If
                If objImmunization.PersonDOB.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("DOB, ")
                    'MessageBox.Show("Person DOB missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person DOB missing " & vbCrLf)
                    'Person = Person & "DOB, "
                    'Exit Function
                End If
                If objImmunization.PersonGender.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("Gender, ")
                    'MessageBox.Show("Person Gender missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person Gender missing " & vbCrLf)
                    'Person = Person & "Gender, "
                    'Exit Function
                End If
                If objImmunization.PersonAddress.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("Address, ")
                    'MessageBox.Show("Person Gender missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person Gender missing " & vbCrLf)
                    'Person = Person & "Gender, "
                    'Exit Function
                End If
                If objImmunization.PersonCounty.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("County, ")
                    'MessageBox.Show("Person County missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person County missing " & vbCrLf)
                    'Person = Person & "County, "
                    'Exit Function
                End If
                If objImmunization.PersonCity.Trim.Length = 0 Then
                    BoolPatientInfo = True
                    strbldPatientInfo.Append("City, ")
                    'MessageBox.Show("Person Gender missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'StrMessageBuilder.Append("Person Gender missing " & vbCrLf)
                    'Person = Person & "City, "
                    'Exit Function
                End If

                '-----Person
                If BoolPatientInfo Then
                    strbldPatientInfo.Append(" missing for Patient" & vbCrLf)
                    StrMessageBuilder.Append(strbldPatientInfo)
                End If
                'If Person.Length > 8 Then
                '    StrMessageBuilder.Append(Person & " missing" & vbCrLf)
                'End If


                '''''''''''validation wrt to Responsible Person i.e. Guardian
                'first check the Guardians information, if any one text is missing then go for Mothers information, if any on text is missing check for Fathers information.
                ''if all the guardian info is present dont check for Mothers and fathers Info and exit out.
                'if any of guardians, mothers and fathers info is missing then show the missing guardian info only.

                Dim StrbldGuardianInfo As New System.Text.StringBuilder
                Dim strbldMotherInfo As New System.Text.StringBuilder
                Dim strbldFatherInfo As New System.Text.StringBuilder

                Dim BoolGuardInfoMissing As Boolean = False
                If objImmunization.PersonGuardianFName.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    StrbldGuardianInfo.Append("First Name, ")
                End If
                If objImmunization.PersonGuardianLName.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " Last Name, "
                    StrbldGuardianInfo.Append("Last Name, ")
                End If
                If objImmunization.PersonGuardianAddress.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " Address, "
                    StrbldGuardianInfo.Append("Address, ")
                End If
                If objImmunization.PersonGuardianCity.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " City, "
                    StrbldGuardianInfo.Append("City, ")
                End If
                If objImmunization.PersonGuardianState.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " State, "
                    StrbldGuardianInfo.Append("State, ")
                End If
                If objImmunization.PersonGuardianZip.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " Zip, "
                    StrbldGuardianInfo.Append("Zip, ")
                End If
                If objImmunization.PersonGuardianCounty.Trim.Length = 0 Then
                    BoolGuardInfoMissing = True
                    'ResponsiblePerson = ResponsiblePerson & " County, "
                    StrbldGuardianInfo.Append("County, ")
                End If

                If BoolGuardInfoMissing Then 'i.e guardian info is missing
                    'since the BoolGuardInfoMissing variable is true that means one/all the guardian info is missing so check for mothers info

                    Dim BoolMotherInfoMissing As Boolean = False

                    If objImmunization.PersonMotherFName.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        strbldMotherInfo.Append("Mother First Name, ")
                    End If
                    If objImmunization.PersonMotherLName.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " Last Name, "
                        strbldMotherInfo.Append("Mother Last Name, ")
                    End If
                    If objImmunization.PersonMotherAddress.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " Address, "
                        strbldMotherInfo.Append("Mother Address, ")
                    End If
                    If objImmunization.PersonMotherCity.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " City, "
                        strbldMotherInfo.Append("Mother City, ")
                    End If
                    If objImmunization.PersonMotherState.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " State, "
                        strbldMotherInfo.Append("Mother State, ")
                    End If
                    If objImmunization.PersonMotherZip.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " Zip, "
                        strbldMotherInfo.Append("Mother Zip, ")
                    End If
                    If objImmunization.PersonMotherCounty.Trim.Length = 0 Then
                        BoolMotherInfoMissing = True
                        'ResponsiblePersonMother = ResponsiblePersonMother & " County, "
                        strbldMotherInfo.Append("Mother County, ")
                    End If

                    If BoolMotherInfoMissing Then
                        'since the BoolMotherInfoMissing variable is true that means one/all the Mother info is missing so check for Fathers info
                        Dim BoolFatherInfoMissing As Boolean = False

                        If objImmunization.PersonFatherFName.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            strbldFatherInfo.Append("Father First Name, ")
                        End If
                        If objImmunization.PersonFatherLName.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " Last Name, "
                            strbldFatherInfo.Append("Father Last Name, ")
                        End If
                        If objImmunization.PersonFatherAddress.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " Address, "
                            strbldFatherInfo.Append("Father Address, ")
                        End If
                        If objImmunization.PersonFatherCity.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " City, "
                            strbldFatherInfo.Append("Father City, ")
                        End If
                        If objImmunization.PersonFatherState.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " State, "
                            strbldFatherInfo.Append("Father State, ")
                        End If
                        If objImmunization.PersonFatherZip.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " Zip, "
                            strbldFatherInfo.Append("Father Zip, ")
                        End If
                        If objImmunization.PersonFatherCounty.Trim.Length = 0 Then
                            BoolFatherInfoMissing = True
                            'ResponsiblePersonFather = ResponsiblePersonFather & " County, "
                            strbldFatherInfo.Append("Father County, ")
                        End If

                        If BoolFatherInfoMissing Then
                            'since the BoolFatherInfoMissing variable is true that means one/all the Fathers info is missing, therefore show the missing guardian info atlast
                            StrbldGuardianInfo.Append(" missing for Guardian" & vbCrLf)
                            StrMessageBuilder.Append(StrbldGuardianInfo)

                        Else 'since all fathersinfo is present dont check any other info becaz fathers info is present as guardian info.
                            'dont check any other info becaz fathers info is present as guardian info.
                           
                        End If

                    Else 'mothers all info is present, so dont check for fathersinfo becaz the mothers info is present as guardian info.
                        'dont check for fathersinfo becaz the mothers info is present as guardian info.
                    End If

                Else 'all guardian info is present.
                    ''if all the guardian info is present dont check for Mothers and fathers Info and exit out.
                End If


            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New ClsImmunizationException(ex.ToString)
        End Try

    End Function



    '---------Original
    'Private Function ValidateData(ByVal objImmunization As ClsImmunization) As Boolean
    '    Try

    '        If objImmunization.CPTCode.Trim.Length = 0 Then
    '            'MessageBox.Show("CPT Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("CPT Code missing for " & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.ManufacturerCode.Trim.Length = 0 Then
    '            'MessageBox.Show("ManufacturerCode missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("ManufacturerCode missing for " & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.LotNumber.Trim.Length = 0 Then
    '            'MessageBox.Show("Lot Number missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Lot Number missing for " & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.DoseAmount.Trim.Length = 0 Then
    '            'MessageBox.Show("Dose Amount missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Dose Amount missing for " & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.ReminderRecall.Trim.Length = 0 Then
    '            'MessageBox.Show("Reminder Recall  missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Reminder Recall  missing" & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.VaccByOtherProvider.Trim.Length = 0 Then
    '            'MessageBox.Show("Vaccination by other provider missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Vaccination by other provider missing " & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.VaccEligibilityCode.Trim.Length = 0 Then
    '            'MessageBox.Show("Vaccine Eligibility Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Vaccine Eligibility Code missing" & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If
    '        If objImmunization.ToBePOCForReminders.Trim.Length = 0 Then
    '            'MessageBox.Show("To be POC For Reminders missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            'Exit Function
    '        End If
    '        If objImmunization.VaccineCode.Trim.Length = 0 Then
    '            'MessageBox.Show("Vaccine Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            StrMessageBuilder.Append("Vaccine Code missing" & objImmunization.VaccineName & vbCrLf)
    '            'Exit Function
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        Throw New ClsImmunizationException(ex.ToString)

    '    End Try
    'End Function
    '---------Original


    Private Function ValidateData(ByVal objImmunization As ClsImmunization) As Boolean
        Try
            Dim MissingData As String = ""

            '--------for Vaccine name
            If objImmunization.CPTCode.Trim.Length = 0 Then
                'MessageBox.Show("CPT Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("CPT Code missing for " & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & "CPT Code, "
                'Exit Function
            End If
            If objImmunization.ManufacturerCode.Trim.Length = 0 Then
                'MessageBox.Show("ManufacturerCode missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("ManufacturerCode missing for " & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & "Manufacturer Code, "
                'Exit Function
            End If
            If objImmunization.LotNumber.Trim.Length = 0 Then
                'MessageBox.Show("Lot Number missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("Lot Number missing for " & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & "Lot Number, "
                'Exit Function
            End If
            If objImmunization.DoseAmount.Trim.Length = 0 Then
                'MessageBox.Show("Dose Amount missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("Dose Amount missing for " & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & "Dose Amount, "
                'Exit Function
            End If
            If objImmunization.ReminderRecall.Trim.Length = 0 Then
                'MessageBox.Show("Reminder Recall  missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("Reminder Recall  missing" & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & "Reminder Recall, "
                'Exit Function
            End If
            '--------for Vaccine name

            If MissingData <> "" Then
                StrMessageBuilder.Append(MissingData & " missing for " & objImmunization.VaccineName & objImmunization.ItemCounterId & vbCrLf)
                MissingData = ""
            End If


            If objImmunization.VaccByOtherProvider.Trim.Length = 0 Then
                'MessageBox.Show("Vaccination by other provider missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                StrMessageBuilder.Append("Vaccination by other provider missing " & vbCrLf)
                'Exit Function
            End If

            If objImmunization.VaccEligibilityCode.Trim.Length = 0 Then
                'MessageBox.Show("Vaccine Eligibility Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'StrMessageBuilder.Append("Vaccine Eligibility Code missing" & objImmunization.VaccineName & vbCrLf)
                MissingData = MissingData & " Vaccine Eligibility Code, "
                'Exit Function
            End If
            If objImmunization.ToBePOCForReminders.Trim.Length = 0 Then
                'MessageBox.Show("To be POC For Reminders missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccineCode.Trim.Length = 0 Then
                'MessageBox.Show("Vaccine Code missing", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                StrMessageBuilder.Append("Vaccine Code missing" & objImmunization.VaccineName & objImmunization.ItemCounterId & vbCrLf)
                MissingData = MissingData & " Vaccine Code, "
                'Exit Function
            End If

            If MissingData <> "" Then
                StrMessageBuilder.Append(MissingData & " missing for " & objImmunization.VaccineName & objImmunization.ItemCounterId & vbCrLf)
                MissingData = ""
            End If

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New ClsImmunizationException(ex.ToString)

        End Try
    End Function

    Private Function ValidateLengthforPatient(ByVal objImmunization As ClsImmunization, ByVal icnt As Int32) As Boolean
        Try
            If icnt = 0 Then


                If objImmunization.RecordType.Length > 1 Then
                    objImmunization.RecordType = objImmunization.RecordType.Substring(0, 1)
                    'MessageBox.Show("RecordType Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PatientID.Length > 20 Then
                    objImmunization.PatientID = objImmunization.PatientID.Substring(0, 20)
                    'MessageBox.Show("PatientID Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.DateofEncounter.Length > 8 Then
                    objImmunization.DateofEncounter = objImmunization.DateofEncounter.Substring(0, 8)
                    'MessageBox.Show("Date of Encounter Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PersonFName.Length > 40 Then
                    objImmunization.PersonFName = objImmunization.PersonFName.Substring(0, 40)
                    'MessageBox.Show("Person first Name Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PersonLName.Length > 40 Then
                    objImmunization.PersonLName = objImmunization.PersonLName.Substring(0, 40)
                    'MessageBox.Show("Person Last Name Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PersonDOB.Length > 8 Then
                    objImmunization.PersonDOB.Substring(0, 8)
                    'MessageBox.Show("Person DOB Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PersonCounty.Length > 2 Then
                    objImmunization.PersonCounty = objImmunization.PersonCounty.Substring(0, 2)
                    'MessageBox.Show("Person County Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.PersonGender.Length > 1 Then
                    objImmunization.PersonGender = objImmunization.PersonGender.Substring(0, 1)
                    'MessageBox.Show("Person Gender Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonFName.Length > 40 Then
                    objImmunization.ResponsiblePersonFName = objImmunization.ResponsiblePersonFName.Substring(0, 40)
                    'MessageBox.Show("Responsible Person First Name Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonLName.Length > 40 Then
                    objImmunization.ResponsiblePersonLName = objImmunization.ResponsiblePersonLName.Substring(0, 40)
                    'MessageBox.Show("Responsible Person Last Name Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonStreet.Length > 40 Then
                    objImmunization.ResponsiblePersonStreet = objImmunization.ResponsiblePersonStreet.Substring(0, 40)
                    'MessageBox.Show("Responsible Person Street Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonCity.Length > 30 Then
                    objImmunization.ResponsiblePersonCity = objImmunization.ResponsiblePersonCity.Substring(0, 30)
                    'MessageBox.Show("Responsible Person City Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonState.Length > 3 Then
                    objImmunization.ResponsiblePersonState = objImmunization.ResponsiblePersonState.Substring(0, 3)
                    'MessageBox.Show("Responsible Person State Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonCounty.Length > 6 Then
                    objImmunization.ResponsiblePersonCounty = objImmunization.ResponsiblePersonCounty.Substring(0, 6)
                    'MessageBox.Show("Responsible Person County Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
                If objImmunization.ResponsiblePersonZip.Length > 10 Then
                    objImmunization.ResponsiblePersonZip = objImmunization.ResponsiblePersonZip.Substring(0, 10)
                    'MessageBox.Show("Responsible Person Zip  Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Exit Function
                End If
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New ClsImmunizationException(ex.ToString)
        End Try
    End Function


    Private Function ValidateLength(ByVal objImmunization As ClsImmunization) As Boolean
        Try
            
            If objImmunization.CPTCode.Length > 5 Then
                objImmunization.CPTCode = objImmunization.CPTCode.Substring(0, 5)
                'MessageBox.Show("CPT Code Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.ManufacturerCode.Length > 3 Then
                objImmunization.ManufacturerCode = objImmunization.ManufacturerCode.Substring(0, 3)
                'MessageBox.Show("ManufacturerCode Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.LotNumber.Length > 20 Then
                objImmunization.LotNumber = objImmunization.LotNumber.Substring(0, 20)
                'MessageBox.Show("Lot Number Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.ReasonForNonAdmin.Length > 2 Then
                objImmunization.ReasonForNonAdmin = objImmunization.ReasonForNonAdmin.Substring(0, 2)
                'MessageBox.Show("Reason For Non Administration is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.DoseAmount.Length > 5 Then
                objImmunization.DoseAmount = objImmunization.DoseAmount.Substring(0, 5)
                'MessageBox.Show("Dose Amount Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If

            If objImmunization.ReminderRecall.Length > 1 Then
                objImmunization.ReminderRecall = objImmunization.ReminderRecall.Substring(0, 1)
                'MessageBox.Show("Reminder Recall  Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccByOtherProvider.Length > 1 Then
                objImmunization.VaccByOtherProvider = objImmunization.VaccByOtherProvider.Substring(0, 1)
                'MessageBox.Show("Vaccination by other provider Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccEligibilityCode.Length > 1 Then
                objImmunization.VaccEligibilityCode = objImmunization.VaccEligibilityCode.Substring(0, 1)
                'MessageBox.Show("Vaccine Eligibility Code Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccSiteCode.Length > 1 Then
                objImmunization.VaccSiteCode = objImmunization.VaccSiteCode.Substring(0, 1)
                'MessageBox.Show("Vaccine Site Code Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccRouteCode.Length > 1 Then
                objImmunization.VaccRouteCode = objImmunization.VaccRouteCode.Substring(0, 1)
                'MessageBox.Show("Vaccine Route Code Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.ToBePOCForReminders.Length > 1 Then
                objImmunization.ToBePOCForReminders = objImmunization.ToBePOCForReminders.Substring(0, 1)
                'MessageBox.Show("To be POC For Reminders Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            If objImmunization.VaccineCode.Length > 4 Then
                objImmunization.VaccineCode = objImmunization.VaccineCode.Substring(0, 4)
                'MessageBox.Show("Vaccine Code Length is not valid", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Exit Function
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New ClsImmunizationException(ex.ToString)
        End Try
    End Function

    Public Sub New()
        'objImmunization = oImmunization
    End Sub
End Class

