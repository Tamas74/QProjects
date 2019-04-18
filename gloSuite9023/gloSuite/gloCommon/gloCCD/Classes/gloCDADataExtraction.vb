Imports System.Data.SqlClient
Imports System.IO
Imports gloCCDSchema
Imports System.Xml
Imports System.Xml.Serialization
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports gloCCRSchema



Public Class gloCDADataExtraction
    Implements IDisposable
    Dim _VisitID As Int64 = 0
    Dim _FromDate As String
    Dim _ToDate As String

    ''Added for MU2 Patient Portal on 20130905
    Public _dtDocTimeStamp As DateTime = Nothing
    ''End
    ''20121003
    Private blIsOwnedbyPastExam As Boolean = False
    Private dtDOS As DateTime = System.DateTime.Now
    Dim _strmsg As String = ""
    Private _nExamID As Int64
    Private _IsExportSummary As Boolean
    'Dim mPatient As Patient = Nothing
    Dim oLabTestCol1 As gloCCDLibrary.LabTestCol = Nothing

    Dim _CCDAPatientidentifier As String = ""
    Dim _OID As String = ""

    Dim dsCCDAPatientConsent As New DataSet
    Dim ReportingYear As String = ""

    Public Property CCDAPatientidentifier() As String
        Get
            Return _CCDAPatientidentifier
        End Get
        Set(ByVal value As String)
            _CCDAPatientidentifier = value
        End Set
    End Property
    Public Property OID() As String
        Get
            Return _OID
        End Get
        Set(ByVal value As String)
            _OID = value
        End Set
    End Property



    Public Property IsExportSummary() As Boolean
        Get
            Return _IsExportSummary
        End Get
        Set(ByVal value As Boolean)
            _IsExportSummary = value
        End Set
    End Property
    Public Property strmsg() As String
        Get
            Return _strmsg
        End Get
        Set(ByVal value As String)
            _strmsg = value
        End Set
    End Property
    Public Property nExamId() As Int64
        Get
            Return _nExamID
        End Get
        Set(ByVal value As Int64)
            _nExamID = value
        End Set
    End Property
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Function GetSettings_CCDAPatientIdentifier() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        ' Dim _CCDAPatientidentifier As String = ""
        Dim osqlpara As SqlParameter = Nothing
        Try
            objCon.ConnectionString = gloLibCCDGeneral.Connectionstring
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CCDA_UniquePatientIdentifier"
            objCmd.Connection = objCon

            objCon.Open()
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@IsDefault"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Boolean
            osqlpara.Value = True
            objCmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            objDA.Dispose() : objDA = Nothing

            'Dim nCount As Integer
            '' For nCount = 0 To dtTable.Rows.Count - 1
            'If Not IsNothing(dtTable) Then
            '    If dtTable.Rows.Count > 0 Then
            '        _CCDAPatientidentifier = CType(dtTable.Rows(nCount).Item("sShortName"), String)
            '    End If

            'End If
            Return dtTable

            '  Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If

            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If

            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If

        End Try
        Return dtTable
    End Function
    Public Function GenerateClinicalInformation(ByVal PatientID As Int64, ByVal LoginID As Int64, Optional ByVal CCDSection As String = "", Optional ByVal _VisitId As Int64 = 0, Optional ByVal FromDate As String = Nothing, Optional ByVal ToDate As String = Nothing, Optional ByVal _FinalCCDFilePath As String = Nothing, Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000", Optional ByVal base64String As String = "") As String
        Dim dtvitals As DataTable = Nothing

        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing

        blIsOwnedbyPastExam = _IsOwnByPastExam
        If (IsNothing(_DateOfservice)) Then
            _DateOfservice = "1/1/2000"
        End If
        dtDOS = Convert.ToDateTime(_DateOfservice)
        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing

        Try
            If PatientID = 0 Then
                Return ""
            End If

            _PatientID = PatientID
            mCCDSection = CCDSection
            If (IsNothing(mCCDSection)) Then
                mCCDSection = ""
            End If
            Me._VisitID = _VisitId
            _FromDate = FromDate
            _ToDate = ToDate

            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)

            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))

            mPatient.PatientID = PatientID
            If IsNothing(mPatient) Then
                Return ""
            End If

            mPatient.Races = GetPatientRaceDetails(dsPatient.Tables("Race"))
            mPatient.Ethnicities = GetPatientEthnicityDetails(dsPatient.Tables("Ethnicity"))
            'loginproviderinfo = GetLoginProviderInfo(dsPatient.Tables("User"))
            mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))
            mPatient.PatientEncounters = GetPatientEncounter(_PatientID, 0)
            mPatient.Chiefcomplaint = GetChiefComplaint(_PatientID)
            mPatient.ClinicalInstruction = GetPatientClinicalInstruction(_PatientID, Me._VisitID)
            mPatient.PatientInsurances = GetPatientInsuranceInfo(dsPatient.Tables("Insurances"))
            mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID, nExamId)
            mPatient.PatientEducation = GetPatientEducation(_PatientID)
            'mPatient.PatientCarePlan = GetPatientCarePlan(_PatientID)
            mPatient.FutureScheduleApt = GetPatientFutureAppointment(_PatientID)
            mPatient.ReferralstoProvider = GetReferralstoOtherProvider(_PatientID)
            mPatient.FutureScheduleTests = GetFutureScheduleTests(_PatientID)
            mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)

            If mCCDSection.Contains("Allergy") = True Or mCCDSection = "All" Then
                mPatient.PatientAllergies = GetLatestAllergiesinfo_New(_PatientID)
            End If
            If mCCDSection.Contains("Medications") = True Or mCCDSection = "All" Then
                mPatient.PatientMedications = GetLatestMedicationinfo_New(_PatientID)
            End If
            'If mCCDSection.Contains("AdvanceDirectives") = True Or mCCDSection = "All" Then
            '    mPatient.Advancedirective = ogloCCDDBLayer.GetPatientAdvDirectives(mPatientID)
            'End If
            'If mCCDSection.Contains("FamilyHistory") = True Or mCCDSection = "All" Then
            '    mPatient.PatientFamilyHistory = ogloCCDDBLayer.GetPatientFamilyHistory(mPatientID)
            'End If
            If mCCDSection.Contains("SocialHistory") = True Or mCCDSection = "All" Then
                mPatient.PatientSocialHistory = GetPatientSocialHistory(_PatientID)
            End If
            If mCCDSection.Contains("Procedures") = True Or mCCDSection = "All" Then
                mPatient.PatientProcedures = GetPatientProcedure(_PatientID)
            End If
            'If mCCDSection.Contains("Encounter") = True Or mCCDSection = "All" Then
            '    mPatient.PatientEncounters = GetPatientEncounter(_PatientID)
            'End If
            'Dim dtvitals As New DataTable
            If mCCDSection.Contains("Vitals") = True Or mCCDSection = "All" Then
                dtvitals = GetPatientVitalsinDT(_PatientID)
            Else
                dtvitals = New DataTable
            End If

            If mCCDSection.Contains("Problems") = True Or mCCDSection = "All" Then
                mPatient.PatientProblems = GetPatientProblems(_PatientID)
            End If
            If mCCDSection.Contains("Results") = True Or mCCDSection = "All" Then
                mPatient.LabTests = GetLabTestsWithResult(_PatientID)
            End If
            If mCCDSection.Contains("Immunization") = True Or mCCDSection = "All" Then
                mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
            End If

            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.mPatient = mPatient
            objgloCDAWriter.dtvitals = dtvitals
            mStrXMLfile = objgloCDAWriter.GeneratePatientCDA(_FinalCCDFilePath, mPatient.PatientName.LastName, mPatient.PatientName.Code)

            _VisitId = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Finally
            strmsg = objgloCDAWriter.msgString
            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(dtvitals) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing
        End Try
    End Function

    Public Function SaveCDAConsent(ByVal _nCCDAID As Int64, ByVal _nPatientId As Int64, ByVal _sSectionName As String, ByVal _sCDAPrivacyText As String, ByVal _CDAPurposeofUse As String) As Int64
        Dim nId As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim sqlParam As SqlParameter = Nothing

        Try

            cmd = New SqlCommand("gsp_InUpCCDAPatientConsent", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _nPatientId

            sqlParam = cmd.Parameters.Add("@sSectionName", SqlDbType.VarChar, 5000)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _sSectionName

            sqlParam = cmd.Parameters.Add("@sCDAPrivacyText", SqlDbType.VarChar, 5000)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _sCDAPrivacyText

            sqlParam = cmd.Parameters.Add("@sPurposeofUse", SqlDbType.VarChar, 5000)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _CDAPurposeofUse

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            nId = Convert.ToInt64(cmd.Parameters("@nId").Value)
            Return nId
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Function INUPCDAPurposeofUse(ByVal _nCCDAID As Int64, ByVal _nPatientId As Int64, ByVal _CDAPurposeofUse As String) As Int64
        Dim nId As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim sqlParam As SqlParameter = Nothing

        Try

            cmd = New SqlCommand("gsp_InUpCCDAPurposeofUseCodes", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _nPatientId

            sqlParam = cmd.Parameters.Add("@sPurposeofUse", SqlDbType.VarChar, 5000)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _CDAPurposeofUse

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            nId = Convert.ToInt64(cmd.Parameters("@nId").Value)
            Return nId
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Function getCCDAPatientConsentVal(ByVal _nPatientId As Int64) As DataSet

        ''Dim clCCDAPatientConsent As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        ''Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim ds As DataSet = New DataSet
        Try
            objCon.ConnectionString = gloLibCCDGeneral.Connectionstring

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_getCCDAPatientConsent"
            objCmd.Connection = objCon


            sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _nPatientId

            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            da = New SqlDataAdapter(objCmd)
            da.Fill(ds)
            ''objSQLDataReader = objCmd.ExecuteReader
            ''While objSQLDataReader.Read
            ''    clCCDAPatientConsent.Add(objSQLDataReader.Item(0))
            ''End While
            ''objSQLDataReader.Close()
            objCon.Close()
            ''objSQLDataReader = Nothing

            Return ds

        Catch ex As Exception
            ''MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''Throw ex
            Throw New gloCCDException(ex.ToString())
            Return Nothing
        Finally
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


    End Function

    'Public Function getCCDADataEnterer() As DataTable

    '    ''Dim clCCDAPatientConsent As New Collection
    '    Dim objCon As New SqlConnection
    '    Dim objCmd As New SqlCommand
    '    ''Dim objSQLDataReader As SqlDataReader
    '    Dim sqlParam As SqlParameter = Nothing
    '    Dim da As SqlDataAdapter = New SqlDataAdapter
    '    Dim dt As DataTable = New DataTable
    '    Try
    '        objCon.ConnectionString = gloLibCCDGeneral.Connectionstring

    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "gsp_getdataenterer"
    '        objCmd.Connection = objCon



    '        If objCon.State = ConnectionState.Closed Then
    '            objCon.Open()
    '        End If

    '        da = New SqlDataAdapter(objCmd)
    '        da.Fill(dt)




    '        If Not IsNothing(dt) Then
    '            If dt.Rows.Count > 0 Then
    '                oClinic.ClinicName = Convert.ToString(dt.Rows(0)("ClinicName"))
    '                ''As per QualityNet After First Validation on CMS
    '                oClinic.ClinicTaxID = Convert.ToString(dt.Rows(0)("sTAXID"))
    '                oClinic.PersonContactAddress.Street = Convert.ToString(dt.Rows(0)("sAddress1")) + " " + Convert.ToString(dt.Rows(0)("sStreet"))
    '                oClinic.PersonContactAddress.City = Convert.ToString(dt.Rows(0)("sCity"))
    '                oClinic.PersonContactAddress.State = Convert.ToString(dt.Rows(0)("sState"))
    '                oClinic.PersonContactAddress.Zip = Convert.ToString(dt.Rows(0)("sZIP"))
    '                oClinic.PersonContactAddress.Country = Convert.ToString(dt.Rows(0)("sCountry"))
    '                oClinic.PersonContactPhone.Phone = Convert.ToString(dt.Rows(0)("sPhoneNo"))
    '                oClinic.PersonContactPhone.Phone = Convert.ToString(dt.Rows(0)("sPhoneNo"))
    '                oClinic.TaxonomyCode = Convert.ToString(dt.Rows(0)("sTaxonomyCode"))
    '                oClinic.TaxonomyDesc = Convert.ToString(dt.Rows(0)("sTaxonomyClassification"))
    '            End If
    '        End If
    '        ''objSQLDataReader = objCmd.ExecuteReader
    '        ''While objSQLDataReader.Read
    '        ''    clCCDAPatientConsent.Add(objSQLDataReader.Item(0))
    '        ''End While
    '        ''objSQLDataReader.Close()
    '        objCon.Close()
    '        ''objSQLDataReader = Nothing

    '        Return dt

    '    Catch ex As Exception
    '        ''MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ''Throw ex
    '        Throw New gloCCDException(ex.ToString())
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(objCmd) Then
    '            objCmd.Parameters.Clear()
    '            objCmd.Dispose()
    '            objCmd = Nothing
    '        End If
    '        If Not IsNothing(objCon) Then
    '            objCon.Dispose()
    '            objCon = Nothing
    '        End If
    '    End Try


    'End Function

    Public Sub GetPatientConsentforCCDA(Optional ByVal _nPatientId As Int64 = 0)

        dsCCDAPatientConsent = getCCDAPatientConsentVal(_nPatientId)

        '' for Patient Consent 
        gloLibCCDGeneral.dvCCDAPatientConsent = dsCCDAPatientConsent.Tables(0).DefaultView
        '' for Patient Privacy Text 
        gloLibCCDGeneral.dvCCDAPatientPrivacyText = dsCCDAPatientConsent.Tables(1).DefaultView
        '' for Patient Purpose of Use 
        gloLibCCDGeneral.dvCCDAPatientPurposeofUse = dsCCDAPatientConsent.Tables(2).DefaultView
        '' for Privacy Text from Admin settings
        gloLibCCDGeneral.dvCCDAPatientPrivacyTextSVal = dsCCDAPatientConsent.Tables(3).DefaultView
    End Sub

    Public Function GenerateClinicalInformation(ByVal PatientID As Int64, ByVal LoginID As Int64, ByVal CDAParameters As gloCDAWriterParameters, Optional ByVal _VisitId As Int64 = 0, Optional ByVal FromDate As String = Nothing, Optional ByVal ToDate As String = Nothing, Optional ByVal _FinalCCDFilePath As String = Nothing, Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000", Optional ByVal OrderID As Int64 = 0, Optional ByVal strSelectedFilePath As String = "", Optional ByVal base64String As String = "") As String
        Dim dtvitals As DataTable = Nothing
        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing

        blIsOwnedbyPastExam = _IsOwnByPastExam
        If (IsNothing(_DateOfservice)) Then
            _DateOfservice = "1/1/2000"
        End If
        dtDOS = Convert.ToDateTime(_DateOfservice)
        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing
        'Dim loginproviderinfo As LoginProvider = Nothing
        Try
            If PatientID = 0 Then
                Return ""
            End If


            _PatientID = PatientID

            Me._VisitID = _VisitId
            _FromDate = FromDate
            _ToDate = ToDate
            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)


            ''common MU dataset
            ''Demographics
            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))
            If IsNothing(dsPatient.Tables("PatientOtherDetails")) = False AndAlso dsPatient.Tables("PatientOtherDetails").Rows.Count > 0 Then
                mPatient.PatientName.PreviousFirstName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevFname")
                mPatient.PatientName.PreviousMiddleName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevMname")
                mPatient.PatientName.PreviousLastName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevLname")
                mPatient.PatientName.BirthSex = dsPatient.Tables("PatientOtherDetails").Rows(0)("BirthSex")
            End If
            mPatient.PatientID = PatientID

            If IsNothing(mPatient) Then
                Return ""
            End If
            mPatient.Races = GetPatientRaceDetails(dsPatient.Tables("Race"))
            mPatient.Ethnicities = GetPatientEthnicityDetails(dsPatient.Tables("Ethnicity"))
            mPatient.LoginProvider = GetLoginProviderInfo(dsPatient.Tables("User"))
            mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))

            mPatient.Author = GetAuthorInfo(_PatientID, OrderID)
            mPatient.InfoRecipent = GetInformationRecipient(_PatientID, OrderID)
            mPatient.PatientEncounters = GetPatientEncounter(_PatientID, OrderID)
            mPatient.PatientInsurances = GetPatientInsuranceInfo(dsPatient.Tables("Insurance"))

            ''Smoking Status
            If CDAParameters.SocialHistory Then
                mPatient.PatientSocialHistory = GetPatientSocialHistory(_PatientID)
            End If
            ''Problems
            If CDAParameters.Problems Then
                mPatient.PatientProblems = GetPatientProblems(_PatientID)
            End If
            ''Medications
            If CDAParameters.Medications Then
                mPatient.PatientMedications = GetLatestMedicationinfo_New(_PatientID)
            End If
            ''Medicaion Allergies
            If CDAParameters.Allergies Then
                mPatient.PatientAllergies = GetLatestAllergiesinfo_New(_PatientID)
            End If

            ''Laboratory Value(s)/result(s)
            If CDAParameters.LaboratoryResult Then
                'mPatient.Orders = GetorderComments(_PatientID)
                mPatient.Orders = GetLabTestsWithResultCDA(_PatientID)
                mPatient.LabTests = oLabTestCol1

            End If
            ''Vital Signs
            If CDAParameters.VitalSigns Then
                dtvitals = GetPatientVitalsinDT(_PatientID)
            Else
                dtvitals = New DataTable
            End If
            ''Care Plan fields(s),including goals and instructions
            'If CDAParameters.CarePlan_GoalsAndInstructions Then
            '    mPatient.PatientCarePlan = GetPatientCarePlan(_PatientID)
            'End If
            ''Procedures
            If CDAParameters.Procedures Then
                mPatient.PatientProcedures = GetPatientProcedure(_PatientID)
                mPatient.ImplantCol = getPatientImplantableDevice(_PatientID)
            End If
            ''Family History
            If CDAParameters.FamilyHistory Then
                mPatient.PatientFamilyHistory = GetPatientFamilyHistory(_PatientID)
            End If
            If CDAParameters.ClinicalInstructions Then
                mPatient.ClinicalInstruction = GetPatientClinicalInstruction(_PatientID, Me._VisitID)
            End If
            If CDAParameters.Implant Then
                mPatient.ImplantCol = getPatientImplantableDevice(_PatientID)
                mPatient.PatientProcedures = GetPatientProcedure(_PatientID)
            End If
            If CDAParameters.HealthConcern Then
                Dim HealthConcernList As HealthConcernCol = Nothing
                HealthConcernList = getCDAHealthConcerns(_PatientID)
                If Not IsNothing(HealthConcernList) Then
                    mPatient.HealthConcernCol = HealthConcernList
                End If
                'Done for Bug ID Bug #114885  

                'If IsNothing(mPatient.PatientProblems) Then
                '    mPatient.PatientProblems = GetPatientProblems(_PatientID)
                'Else
                '    If mPatient.PatientProblems.Count = 0 Then
                '        mPatient.PatientProblems = GetPatientProblems(_PatientID)
                '    End If
                'End If
            End If
            If CDAParameters.Goals Then
                Dim GoalList As GoalsCol = Nothing
                GoalList = getCDAGoals(_PatientID)
                If Not IsNothing(GoalList) Then
                    mPatient.GoalsCol = GoalList
                End If
                'Done for Bug ID Bug #114885  
                'If IsNothing(mPatient.PatientProblems) Then
                '    mPatient.PatientProblems = GetPatientProblems(_PatientID)
                'End If
                If IsNothing(mPatient.HealthConcernCol) Then
                    mPatient.HealthConcernCol = getCDAHealthConcerns(_PatientID)
                Else
                    If mPatient.HealthConcernCol.Count = 0 Then
                        mPatient.HealthConcernCol = getCDAHealthConcerns(_PatientID)
                    End If
                End If
            End If
            'If CDAParameters.Interventions Then
            '    Dim Interventionlist As InterventionCol = Nothing
            '    Interventionlist = getCDAInterventions(_PatientID)
            '    If Not IsNothing(Interventionlist) Then
            '        mPatient.InterventionCol = Interventionlist
            '    End If
            'End If




            ''Care team members(s)
            ' If CDAParameters.CareTeamMember Then
            If CDAParameters.CDAFileType = CDAFileTypeEnum.ClinicalSummary Or CDAParameters.CDAFileType = CDAFileTypeEnum.CareRecordSummary Then
                mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID, nExamId, OrderID)
            Else
                mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID, 0)
            End If
            ' End If
            If CDAParameters.Assessments Then
                mPatient.Assessment = GetAssessments(_PatientID)
            End If
            If CDAParameters.CDAFileType = CDAFileTypeEnum.ClinicalSummary Then
                If CDAParameters.ProviderName Then
                    '  mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
                End If
                If CDAParameters.OfficeContact Then
                    ''Clinic Info
                End If
                If CDAParameters.Visit_DateAndLocation Then
                    ''Included in Encounter
                    '
                End If
                If CDAParameters.ChiefComplaint Then
                    mPatient.Chiefcomplaint = GetChiefComplaint(_PatientID)
                End If
                If CDAParameters.ImmunizationsAdministered Then
                    mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
                End If
                If CDAParameters.MedicationsAdministered Then
                    mPatient.PatientMedicationsAdmin = GetMedicationAdministeredinfo(_PatientID)
                End If
                If CDAParameters.DiagnosticTestsPending Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    If CDAParameters.TreatmentPlan = False Then
                        mPatient.PlannedModuleCol = FillPendingTests(mPatient.PendingTests)
                    End If

                End If

                If CDAParameters.FutureAppointments Then
                    mPatient.FutureScheduleApt = GetPatientFutureAppointment(_PatientID)
                End If
                If CDAParameters.ReferralsToOtherProviders Then
                    mPatient.ReferralstoProvider = GetReferralstoOtherProvider(_PatientID)
                End If
                If CDAParameters.FutureScheduledTests Then
                    mPatient.FutureScheduleTests = GetFutureScheduleTests(_PatientID)
                End If
                If CDAParameters.RecommendedPatientDecisionAids Then
                    mPatient.PatientEducation = GetPatientEducation(_PatientID)
                End If
                If CDAParameters.TreatmentPlan Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    mPatient.PlannedModuleCol = getCDAPlanOfTreatment(_PatientID, mPatient.PendingTests)
                End If
            ElseIf CDAParameters.CDAFileType = CDAFileTypeEnum.CareRecordSummary Then

                If CDAParameters.EncounterDiagnoses Then
                    mPatient.EncounterDiagnosis = GetPatientProblems(_PatientID, True)
                End If
                If CDAParameters.Immunizations Then
                    mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
                End If
                If CDAParameters.CognitiveStatus Then
                    'mPatient.CognitiveStatus = GetPatientCognitiveStatus(_PatientID)
                    mPatient.MentalStatus = GetPatientMentalStatus(_PatientID)
                End If
                If CDAParameters.FunctionalStatus Then
                    mPatient.FunctionalStatus = GetFunctionalAndCognitiveStatus(_PatientID)
                End If
                If CDAParameters.ReasonForReferral Then
                    mPatient.ReasonForReferral = GetReasonforReferral(OrderID, _PatientID)
                End If
                If CDAParameters.ReferringProvider Then
                    mPatient.PatientEncounters = GetPatientEncounter(_PatientID, OrderID)
                End If
                If CDAParameters.Visit_DateAndLocation Then
                    mPatient.PatientVisitDateAndLocation = GetPatientVisitDateAndLocation(_PatientID)
                End If
                If CDAParameters.LaboratoryTest Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    If CDAParameters.TreatmentPlan = False Then
                        mPatient.PlannedModuleCol = FillPendingTests(mPatient.PendingTests)
                    End If
                End If
                If CDAParameters.TreatmentPlan Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    mPatient.PlannedModuleCol = getCDAPlanOfTreatment(_PatientID, mPatient.PendingTests)
                End If
            ElseIf CDAParameters.CDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
                'mPatient.PatientEncounters = Nothing
                If CDAParameters.EncounterDiagnoses Then
                    mPatient.EncounterDiagnosis = GetPatientProblems(_PatientID, True)
                End If
                If CDAParameters.Immunizations Then
                    mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
                End If
                If CDAParameters.CognitiveStatus Then
                    'mPatient.CognitiveStatus = GetPatientCognitiveStatus(_PatientID)
                    mPatient.MentalStatus = GetPatientMentalStatus(_PatientID)
                End If
                If CDAParameters.FunctionalStatus Then
                    mPatient.FunctionalStatus = GetFunctionalAndCognitiveStatus(_PatientID)
                End If
                If CDAParameters.ReasonForReferral Then
                    mPatient.ReasonForReferral = GetReasonforReferral(OrderID, _PatientID)
                End If
                If CDAParameters.ReferringProvider Then
                    mPatient.PatientEncounters = GetPatientEncounter(_PatientID, OrderID)
                End If
                If CDAParameters.Visit_DateAndLocation Then
                    mPatient.PatientVisitDateAndLocation = GetPatientVisitDateAndLocation(_PatientID)
                End If
                If CDAParameters.LaboratoryTest Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    If CDAParameters.TreatmentPlan = False Then
                        mPatient.PlannedModuleCol = FillPendingTests(mPatient.PendingTests)
                    End If

                End If
                If CDAParameters.TreatmentPlan Then
                    mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)
                    mPatient.PlannedModuleCol = getCDAPlanOfTreatment(_PatientID, mPatient.PendingTests)
                End If
            ElseIf CDAParameters.CDAFileType = CDAFileTypeEnum.CarePlan Then
                If CDAParameters.EncounterDiagnoses Then
                    mPatient.EncounterDiagnosis = GetPatientProblems(_PatientID, True)
                End If
                If CDAParameters.Interventions Then
                    Dim Interventionlist As InterventionCol = Nothing
                    Interventionlist = getCDAInterventions(_PatientID)
                    If Not IsNothing(Interventionlist) Then
                        mPatient.InterventionCol = Interventionlist
                    End If
                End If
                If CDAParameters.Outcomes Then
                    mPatient.OutcomeCol = getCDAOutcome(_PatientID)
                End If
                If CDAParameters.Immunizations Then
                    mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
                End If
            End If


            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.mPatient = mPatient

            objgloCDAWriter.dtvitals = dtvitals
            objgloCDAWriter.CDAWritingParams = CDAParameters
            objgloCDAWriter.SelectedFilePath = strSelectedFilePath
            objgloCDAWriter.IsExportSummary = IsExportSummary
            If base64String <> String.Empty Then
                CDAParameters.CDAFileType = CDAFileTypeEnum.UnstructuredCDA
            End If

            objgloCDAWriter.nQRDAFileType = CDAParameters.CDAFileType
            objgloCDAWriter.CCDAPatientidentifier = _CCDAPatientidentifier
            objgloCDAWriter.OID = _OID
            mStrXMLfile = objgloCDAWriter.GeneratePatientCDA(_FinalCCDFilePath, mPatient.PatientName.LastName, mPatient.PatientName.Code, base64String)
            'mStrXMLfile = objgloCDAWriter.GeneratePatientCDA(_FinalCCDFilePath, mPatient.PatientName.LastName, mPatient.PatientName.Code)


            _VisitId = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Finally
            strmsg = objgloCDAWriter.msgString
            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(dtvitals) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing
        End Try
    End Function

    Public Function GenerateClinicalInformationForNonXml(ByVal PatientID As Int64, Optional ByVal LoginID As Int64 = 0, Optional ByVal base64string As String = "", Optional ByVal _FileMediaType As String = "", Optional ByVal ActualFileName As String = "") As String
        Dim dtvitals As DataTable = Nothing
        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing
        Dim _FinalCCDFilePath As String = ""

        'blIsOwnedbyPastExam = _IsOwnByPastExam
        'If (IsNothing(_DateOfservice)) Then
        '    _DateOfservice = "1/1/2000"
        'End If
        'dtDOS = Convert.ToDateTime(_DateOfservice)
        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing
        'Dim loginproviderinfo As LoginProvider = Nothing
        Try
            If PatientID = 0 Then
                Return ""
            End If


            _PatientID = PatientID

            Me._VisitID = _VisitID
            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)

            ''common MU dataset
            ''Demographics
            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))
            If IsNothing(dsPatient.Tables("PatientOtherDetails")) = False AndAlso dsPatient.Tables("PatientOtherDetails").Rows.Count > 0 Then
                mPatient.PatientName.PreviousFirstName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevFname")
                mPatient.PatientName.PreviousMiddleName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevMname")
                mPatient.PatientName.PreviousLastName = dsPatient.Tables("PatientOtherDetails").Rows(0)("sPatientPrevLname")
                mPatient.PatientName.BirthSex = dsPatient.Tables("PatientOtherDetails").Rows(0)("BirthSex")
            End If
            mPatient.PatientID = PatientID

            If IsNothing(mPatient) Then
                Return ""
            End If
            mPatient.Races = GetPatientRaceDetails(dsPatient.Tables("Race"))
            mPatient.Ethnicities = GetPatientEthnicityDetails(dsPatient.Tables("Ethnicity"))
            mPatient.LoginProvider = GetLoginProviderInfo(dsPatient.Tables("User"))
            mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))

            'Send Order Id as 0
            mPatient.Author = GetAuthorInfo(_PatientID, 0)
            mPatient.InfoRecipent = GetInformationRecipient(_PatientID, 0)
            mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID, 0)
            'mPatient.PatientInsurances = GetPatientInsuranceInfo(dsPatient.Tables("Insurance"))

            ''Smoking Status

            'If CDAParameters.Interventions Then
            '    Dim Interventionlist As InterventionCol = Nothing
            '    Interventionlist = getCDAInterventions(_PatientID)
            '    If Not IsNothing(Interventionlist) Then
            '        mPatient.InterventionCol = Interventionlist
            '    End If
            'End If

            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.mPatient = mPatient

            objgloCDAWriter.dtvitals = dtvitals
            'objgloCDAWriter.CDAWritingParams = CDAParameters
            'objgloCDAWriter.SelectedFilePath = strSelectedFilePath
            objgloCDAWriter.IsExportSummary = IsExportSummary
            Dim CDAParameters As gloCDAWriterParameters = New gloCDAWriterParameters
            If base64string <> String.Empty Then
                CDAParameters.CDAFileType = CDAFileTypeEnum.UnstructuredCDA
                objgloCDAWriter.CDAWritingParams = CDAParameters
                objgloCDAWriter.FileMediaType = _FileMediaType
                objgloCDAWriter.ActualFileName = ActualFileName
            End If

            objgloCDAWriter.nQRDAFileType = CDAParameters.CDAFileType
            objgloCDAWriter.CCDAPatientidentifier = _CCDAPatientidentifier
            objgloCDAWriter.OID = _OID

            mStrXMLfile = objgloCDAWriter.GeneratePatientCDA(_FinalCCDFilePath, mPatient.PatientName.LastName, mPatient.PatientName.Code, base64string)
            'mStrXMLfile = objgloCDAWriter.GeneratePatientCDA(_FinalCCDFilePath, mPatient.PatientName.LastName, mPatient.PatientName.Code)


            _VisitID = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            'Return ""
            Throw ex
        Finally
            If Not IsNothing(objgloCDAWriter) Then
                strmsg = objgloCDAWriter.msgString
            End If


            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(dtvitals) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing
        End Try
    End Function


    'Public Function GetFunctionalAndCognitiveStatus(ByVal _PatientID As Int64) As AllergiesCol
    '    Dim _strSQL As String = ""
    '    Dim cmd As SqlCommand = Nothing
    '    Dim cnn As New SqlConnection()
    '    Dim osqlpara As SqlParameter
    '    Dim _oCol As AllergiesCol
    '    Dim _oStatus As Allergies
    '    Dim sdap As SqlDataAdapter
    '    Dim dt As New DataTable
    '    Try
    '        _oCol = New AllergiesCol()
    '        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
    '        cnn.Open()
    '        cmd = New SqlCommand
    '        cmd.Connection = cnn
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.CommandText = "gsp_CCDgetFunctionalStatus"

    '        osqlpara = New SqlParameter
    '        osqlpara.ParameterName = "@nPatientID"
    '        osqlpara.Direction = ParameterDirection.Input
    '        osqlpara.DbType = DbType.Int64
    '        osqlpara.Value = _PatientID
    '        cmd.Parameters.Add(osqlpara)
    '        osqlpara = Nothing
    '        sdap = New SqlDataAdapter(cmd)
    '        sdap.Fill(dt)
    '        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                _oStatus = New Allergies
    '                _oStatus.ProductName = Convert.ToString(dt.Rows(i)("sHistoryItem"))
    '                _oStatus.EffectiveStartTime = dt.Rows(i)("DOEAllergy")
    '                _oStatus.Status = Convert.ToString(dt.Rows(i)("sStatus"))
    '                _oStatus.ConceptID = Convert.ToString(dt.Rows(i)("sConceptID"))
    '                _oCol.Add(_oStatus)
    '                If Not IsNothing(_oStatus) Then
    '                    _oStatus.Dispose()
    '                    _oStatus = Nothing
    '                End If
    '            Next
    '        End If

    '        Return _oCol
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(cmd) Then
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '        If Not IsNothing(cnn) Then
    '            If cnn.State = ConnectionState.Open Then
    '                cnn.Close()
    '            End If
    '            cnn.Dispose()
    '            cnn = Nothing
    '        End If
    '        _strSQL = ""
    '    End Try
    'End Function
#Region "CCD Exchange"
    ''Generate Ambulatory(Patient Specific/Full) CCD and transform to the Intuit Specific CCD 
    Public Function GenerateClinicalInfoWithCCDExchnage(ByVal PatientID As Int64, ByVal LoginID As Int64, ByVal CDAParameters As gloCDAWriterParameters, ByVal iAmPracticeId As String, ByVal MemberId As Long, ByVal IntuitProviderID As Int64, ByVal PracticeProviderID As Int64, ByVal IntegrationID As String) As String
        Dim dtvitals As DataTable = Nothing
        Dim dtPractice As DataTable = Nothing
        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile, mCCDExchangeFile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing


        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing


        Try
            If PatientID = 0 Then
                Return ""
            End If

            _PatientID = PatientID

            Me._VisitID = _VisitID

            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)
            ''common MU dataset
            ''Demographics
            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))

            If IsNothing(mPatient) Then
                Return ""
            End If
            mPatient.PatientID = PatientID
            mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))

            dtPractice = GetPracticeInfo()
            mPatient.PracticeContact = GetPracticeContact(dtPractice, IntegrationID)


            mPatient.PatientEncounters = GetPatientEncounter(_PatientID, 0)
            mPatient.PatientInsurances = GetPatientInsuranceInfo(dsPatient.Tables("Insurance"))
            ''Smoking Status

            mPatient.PatientSocialHistory = GetPatientSocialHistory(_PatientID)

            ''Problems

            mPatient.PatientProblems = GetPatientProblems(_PatientID)

            ''Medications

            mPatient.PatientMedications = GetLatestMedicationinfo_New(_PatientID)

            ''Medicaion Allergies

            mPatient.PatientAllergies = GetLatestAllergiesinfo_New(_PatientID)

            ''Laboratory Value(s)/result(s)

            mPatient.LabTests = GetLabTestsWithResult(_PatientID)

            ''Vital Signs

            dtvitals = GetPatientVitalsinDT(_PatientID)

            ''Care Plan fields(s),including goals and instructions

            'mPatient.PatientCarePlan = GetPatientCarePlan(_PatientID)

            ''Procedures

            mPatient.PatientProcedures = GetPatientProcedure(_PatientID)

            ''Care team members(s)
            ' If CDAParameters.CareTeamMember Then

            mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID, 0)

            ' End If

            mPatient.PendingTests = GetDiagnosticPendingTests(_PatientID)

            'Code start-Added by kanchan on 20140529
            'In 8020 sprint we add following section in CDA

            mPatient.PatientFamilyHistory = GetPatientFamilyHistory(_PatientID)

            mPatient.ClinicalInstruction = GetPatientClinicalInstruction(_PatientID, 0)

            mPatient.PatientImmunizations = GetPatientImmunizations(_PatientID)
            'Code end-Added by kanchan on 20140529


            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.mPatient = mPatient
            objgloCDAWriter.dtvitals = dtvitals

            objgloCDAWriter.PracticeID = iAmPracticeId
            objgloCDAWriter.IntuitPatientID = MemberId
            objgloCDAWriter.IntuitProviderID = IntuitProviderID
            objgloCDAWriter.PracticeProviderID = PracticeProviderID
            objgloCDAWriter.PatientID = _PatientID
            objgloCDAWriter.IsIntuit = True



            '' objgloCDAWriter.SelectedFilePath = strSelectedFilePath
            'objgloCDAWriter.IsExportSummary = IsExportSummary
            objgloCDAWriter.CDAWritingParams = CDAParameters

            'Start- Date: 29 May 2014, Sagar Ghodke: Include 'Patient Copy' text for CDA that is going
            'out of glo for Patient
            objgloCDAWriter.CDAWritingParams.MarkPatientCopy = True
            'End - Date: 29 May 2014, Sagar Ghodke: Include 'Patient Copy' text for CDA that is goings
            objgloCDAWriter.nQRDAFileType = CDAParameters.CDAFileType
            mStrXMLfile = objgloCDAWriter.GeneratePatientCDA("", mPatient.PatientName.LastName, mPatient.PatientName.Code)
            objgloCDAWriter.CCDFilePath = mStrXMLfile
            ''We are generating Intuit CDA file by passing CDA file as an attachemnt in it
            mCCDExchangeFile = objgloCDAWriter.GenerateCCDExchange(mPatient.PatientName.LastName)

            _VisitID = 0

            Dim traceSwitchMain As TraceSwitch
            traceSwitchMain = New TraceSwitch("TraceSwitch", "TraceSwitch Implementation")

            If (traceSwitchMain.TraceWarning = False) Then
                If File.Exists(mStrXMLfile) = True Then
                    File.Delete(mStrXMLfile)
                End If
            End If
            traceSwitchMain = Nothing

            Return mCCDExchangeFile
        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            mStrXMLfile = Nothing
            strmsg = objgloCDAWriter.msgString
            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(dtvitals) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            If Not IsNothing(dtPractice) Then
                dtPractice.Dispose()
                dtPractice = Nothing
            End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing



        End Try
    End Function


    ''Generate Clinical(Visit Specific) CCD and transform to the Intuit Specific CCD 
    Public Function GenerateClinicalSummaryWithCCDExchnage(ByVal PatientID As Int64, ByVal LoginID As Int64, ByVal CDAParameters As gloCDAWriterParameters, ByVal iAmPracticeId As String, ByVal MemberId As Long, ByVal IntuitProviderID As Int64, ByVal PracticeProviderID As Int64, ByVal ccdImage() As Byte, ByVal IntegrationID As String) As String
        Dim dtvitals As DataTable = Nothing
        Dim dtPractice As DataTable = Nothing
        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile, mCCDExchangeFile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing
        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing
        Dim content As Byte() = Nothing
        Dim doc As New XmlDocument()

        Try
            If PatientID = 0 Then
                Return ""
            End If

            _PatientID = PatientID
            Me._VisitID = _VisitID
            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)
            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))
            dtPractice = GetPracticeInfo()
            mPatient.PracticeContact = GetPracticeContact(dtPractice, IntegrationID)
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))

            objgloCDAWriter = New gloCDAWriter
            mStrXMLfile = objgloCDAWriter.GenerateFileName(mPatient.PatientName.LastName)
            Try
                'Dim xmlStream As MemoryStream = New MemoryStream
                'Dim xmlDoc As Xml.XmlDocument = New Xml.XmlDocument
                'xmlStream.Write(ccdImage, 0, ccdImage.Length)
                'xmlStream.Position = 0
                'xmlDoc.Load(xmlStream)

                content = CType(ccdImage, Byte())
                'Dim stream As MemoryStream = New MemoryStream(content)
                'If stream Is Nothing Then
                '    Return Nothing
                'End If

                Dim oFile As New System.IO.FileStream(mStrXMLfile, System.IO.FileMode.Create)
                If oFile Is Nothing Then
                    'If Not IsNothing(stream) Then
                    '    stream.Dispose()
                    '    stream = Nothing
                    'End If
                    Return Nothing
                End If
                oFile.Write(content, 0, content.Length)
                ' stream.WriteTo(oFile)

                'Array.Resize(content, 0)
                'content = Nothing
                oFile.Flush()
                oFile.Close()
                If Not IsNothing(oFile) Then
                    oFile.Dispose()
                    oFile = Nothing
                End If
                doc.Load(mStrXMLfile)
                Dim type As String = "type=""text/xsl"" href=""http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl"""
                doc.FirstChild.NextSibling.InnerText = type
                doc.Save(mStrXMLfile)
                type = Nothing

                'If Not IsNothing(stream) Then
                '    stream.Dispose()
                '    stream = Nothing
                'End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                ex = Nothing
            End Try


            objgloCDAWriter.mPatient = mPatient
            objgloCDAWriter.PracticeID = iAmPracticeId
            objgloCDAWriter.IntuitPatientID = MemberId
            objgloCDAWriter.IntuitProviderID = IntuitProviderID
            objgloCDAWriter.PracticeProviderID = PracticeProviderID
            objgloCDAWriter.PatientID = _PatientID
            objgloCDAWriter.IsIntuit = True
            objgloCDAWriter.CCDFilePath = mStrXMLfile
            mCCDExchangeFile = objgloCDAWriter.GenerateCCDExchange(mPatient.PatientName.LastName)

            _VisitID = 0

            Dim traceSwitchMain As TraceSwitch
            traceSwitchMain = New TraceSwitch("TraceSwitch", "TraceSwitch Implementation")

            If (traceSwitchMain.TraceWarning = False) Then
                If File.Exists(mStrXMLfile) = True Then
                    File.Delete(mStrXMLfile)
                End If
            End If
            traceSwitchMain = Nothing

            Return mCCDExchangeFile
        Catch ex As gloCCDException

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            mStrXMLfile = Nothing
            content = Nothing
            doc = Nothing
            strmsg = objgloCDAWriter.msgString
            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            If Not IsNothing(dtvitals) Then
                dtvitals.Dispose()
                dtvitals = Nothing
            End If
            If Not IsNothing(dtPractice) Then
                dtPractice.Dispose()
                dtPractice = Nothing
            End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing



        End Try
    End Function


#End Region
#Region "QRDAIII section"

#End Region

    Public Function GetQRDAIIIInformation(ByVal dtProviders As DataTable) As DataSet

        Dim dsMeasure As New DataSet
        Dim daMeasure As SqlDataAdapter = Nothing
        'Dim cmd As SqlCommand = Nothing
        'Dim osqlpara As SqlParameter = Nothing
        'Dim oConnection As SqlConnection = Nothing

        Dim oDB As New gloDatabaseLayer.DBLayer(gloLibCCDGeneral.Connectionstring)
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As New gloDatabaseLayer.DBParameters

        Try

            oDB.Connect(False)

            'cmd = New SqlCommand
            'oConnection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            'cmd.Connection = oConnection
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "MU_QRDAIII_GetClinicProviderDetails"

            'osqlpara = New SqlParameter
            'osqlpara.ParameterName = "@TVP_Providers"
            'osqlpara.Direction = ParameterDirection.Input
            'osqlpara.DbType = DbType.Object
            'osqlpara.Value = nProviderID


            If IsNothing(dtProviders) = False Then
                If (dtProviders.Columns.Contains("ProviderName")) Then
                    dtProviders.Columns.Remove("ProviderName")
                End If

                If (dtProviders.Columns.Contains("sEmployerID")) Then
                    dtProviders.Columns.Remove("sEmployerID")
                End If

                If IsNothing(dtProviders.Columns.Contains("nProviderID")) Then
                    dtProviders.Columns("nProviderID").ColumnName = "ProviderID"
                End If
            End If

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@TVP_Providers"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.Structured
            oParameter.Value = dtProviders
            oParameters.Add(oParameter)
            oParameter = Nothing

            'cmd.Parameters.Add(osqlpara)
            'osqlpara = Nothing

            'oParameters.Add(oParameter)

            'daMeasure = New SqlDataAdapter(cmd)
            'daMeasure.Fill(dsMeasure)

            oDB.Retrive("MU_QRDAIII_GetClinicProviderDetails", oParameters, dsMeasure)
            oDB.Disconnect()

            dsMeasure.Tables(0).TableName = "Clinic"
            dsMeasure.Tables(1).TableName = "Provider"

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally

            If IsNothing(oParameter) = False Then
                oParameter.Dispose()
                oParameter = Nothing
            End If

            If Not IsNothing(daMeasure) Then
                daMeasure.Dispose()
                daMeasure = Nothing
            End If
            'If Not IsNothing(oConnection) Then
            '    oConnection.Close()
            '    oConnection.Dispose()
            '    oConnection = Nothing
            'End If
            'If Not IsNothing(cmd) Then
            '    cmd.Parameters.Clear()
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If
            'If Not IsNothing(osqlpara) Then
            '    osqlpara = Nothing
            'End If
        End Try
        Return dsMeasure
    End Function
#Region "QRDAI Section"
    Public Function GenerateQRDAIInformation(ByVal PatientID As Int64, ByVal LoginID As Int64, ByVal FromDate As String, ByVal ToDate As String, ByVal _dtMeasures As DataTable, ByVal _dtQRDA1Data As DataTable, Optional ByVal _SelectedFilePath As String = "", Optional ByVal _isGroupReporting As Boolean = False, Optional ByVal _ReportingYear As String = "") As String
        'Dim dtvitals As DataTable = Nothing
        Dim dsPatient As DataSet = Nothing
        Dim mCCDSection, mStrXMLfile As String
        Dim objgloCDAWriter As gloCDAWriter = Nothing


        Dim _PatientID As Int64 = 0
        Dim mPatient As Patient = Nothing

        Try
            If PatientID = 0 Then
                Return ""
            End If
            ReportingYear = _ReportingYear

            _PatientID = PatientID

            _FromDate = FromDate
            _ToDate = ToDate
            dsPatient = GetPatientDetailInformation(_PatientID, LoginID)
            mPatient = GetPatientInformation(dsPatient.Tables("Patient"))

            If IsNothing(mPatient) Then
                Return ""
            End If
            mPatient.Races = GetPatientRaceDetails(dsPatient.Tables("Race"))
            mPatient.Ethnicities = GetPatientEthnicityDetails(dsPatient.Tables("Ethnicity"))
            mPatient.PatientID = PatientID
            mPatient.PatientProviders = GetPatientProviderInfo(dsPatient.Tables("Provider"))
            mPatient.Clinic = GetClinicInfo(dsPatient.Tables("Clinic"))
            mPatient.Author = GetAuthorInfo(_PatientID, 0)
            '  mPatient.PatientEncounters = GetPatientEncounter(_PatientID)
            '  mPatient.Chiefcomplaint = GetChiefComplaint(_PatientID)
            ' mPatient.ClinicalInstruction = GetPatientClinicalInstruction(_PatientID, Me._VisitID)
            mPatient.PatientCareTeam = GetPatientCareTeamInfo(_PatientID)


            objgloCDAWriter = New gloCDAWriter
            objgloCDAWriter.mPatient = mPatient
            objgloCDAWriter.MeasurementStartDate = _FromDate
            objgloCDAWriter.MeasurementEndDate = _ToDate
            objgloCDAWriter.dtMeasures = _dtMeasures

            If (Not IsNothing(_dtMeasures)) Then
                If (_dtMeasures.Rows.Count > 0) Then
                    If (_dtMeasures.Columns.Contains("MeasureNumber")) Then
                        '  If (_dtMeasures.Rows(0)("MeasureNumber") = "NQF0419") Then
                        If (_dtQRDA1Data.Columns.Contains("npatientid") AndAlso _dtQRDA1Data.Columns.Contains("category") AndAlso _dtQRDA1Data.Columns.Contains("CodeType") AndAlso _dtQRDA1Data.Columns.Contains("TransactionDate") AndAlso _dtQRDA1Data.Columns.Contains("sIcd9Code") AndAlso _dtQRDA1Data.Columns.Contains("sConceptID") AndAlso _dtQRDA1Data.Columns.Contains("sCPTCode") AndAlso _dtQRDA1Data.Columns.Contains("dtDischargeDate")) Then
                            'Dim dv As DataView = New DataView(_dtQRDA1Data)
                            If (_dtQRDA1Data.Rows.Count > 0) Then
                                Dim _dtnewdata As DataTable = _dtQRDA1Data.Clone()

                                _dtnewdata.ImportRow(_dtQRDA1Data.Rows(0))
                                Dim _firstrow As Boolean = True

                                'If (_PatientID = "226546671567294083") Then
                                '    Dim pat As String = ""
                                'End If
                                Dim smallqry As Integer = 0

                                For Each dr As DataRow In _dtQRDA1Data.Rows
                                    Dim strsql As String = "" ''"npatientid='" & dr("npatientid").ToString() & "'"
                                    '  Dim drr As DataRow() = _dtnewdata.Select(strsql)
                                    '  strsql = "npatientid='" & dr("npatientid").ToString() & "' And  " & " category='" & dr("category").ToString().Trim() & "'"

                                    strsql = "npatientid='" & dr("npatientid").ToString() & "' And  " & " category='" & dr("category").ToString() & "' And  CodeType='" & dr("CodeType").ToString() & "'"

                                    smallqry = 0
                                    If (dr("TransactionDate").ToString.Trim() <> "") Then
                                        strsql = strsql & " And TransactionDate='" & dr("TransactionDate").ToString() & "'"
                                    End If
                                    If (dr("sIcd9Code").ToString.Trim() <> "") Then
                                        strsql = strsql & " And sIcd9Code='" & dr("sIcd9Code").ToString() & "'"
                                        smallqry = 1
                                    End If

                                    If (dr("sConceptID").ToString.Trim() <> "") Then
                                        strsql = strsql & " And sConceptID='" & dr("sConceptID").ToString() & "'"
                                        smallqry = 1
                                    End If


                                    If (dr("sCPTCode").ToString.Trim() <> "") Then
                                        strsql = strsql & " And sCPTCode='" & dr("sCPTCode").ToString() & "'"
                                        smallqry = 1
                                    End If

                                    If (dr("dtDischargeDate").ToString.Trim() <> "") Then
                                        strsql = strsql & " And dtDischargeDate='" & dr("dtDischargeDate").ToString() & "'"
                                    End If

                                    If (_dtQRDA1Data.Columns.Contains("sReasonConceptID")) Then

                                        If (dr("sReasonConceptID").ToString.Trim() <> "") Then
                                            strsql = strsql & " And sReasonConceptID='" & dr("sReasonConceptID").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("sReasonICD9")) Then
                                        If (dr("sReasonICD9").ToString.Trim() <> "") Then
                                            strsql = strsql & " And sReasonICD9='" & dr("sReasonICD9").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("sReasonLOINC")) Then
                                        If (dr("sReasonLOINC").ToString.Trim() <> "") Then
                                            strsql = strsql & " And sReasonLOINC='" & dr("sReasonLOINC").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("sReasonICD10")) Then

                                        If (dr("sReasonICD10").ToString.Trim() <> "") Then
                                            strsql = strsql & " And sReasonICD10='" & dr("sReasonICD10").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("sReasonCode")) Then
                                        If (dr("sReasonCode").ToString.Trim() <> "") Then
                                            strsql = strsql & " And sReasonCode='" & dr("sReasonCode").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("CVX")) Then
                                        If (dr("CVX").ToString.Trim() <> "") Then
                                            strsql = strsql & " And CVX='" & dr("CVX").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("LOINC")) Then
                                        If (dr("LOINC").ToString.Trim() <> "") Then
                                            strsql = strsql & " And LOINC='" & dr("LOINC").ToString() & "'"
                                        End If
                                    End If
                                    If (_dtQRDA1Data.Columns.Contains("RXCUI")) Then
                                        If (dr("RXCUI").ToString.Trim() <> "") Then
                                            strsql = strsql & " And RXCUI='" & dr("RXCUI").ToString() & "'"
                                        End If
                                    End If
                                    'strsql = "npatientid='" & dr("npatientid").ToString() & "' And  " & " category='" & dr("category").ToString().Trim() & "' And  CodeType='" & dr("CodeType").ToString().Trim() & "' And TransactionDate='" & dr("TransactionDate").ToString().Trim() & "' And sIcd9Code='" & dr("sIcd9Code").ToString() & "' And sConceptID ='" & dr("sConceptID").ToString().Trim() & "'"


                                    'strsql = "npatientid='" & dr("npatientid").ToString() & "' And  " & " category='" & dr("category").ToString().Trim() & "' And  CodeType='" & dr("CodeType").ToString().Trim() & "' And TransactionDate='" & dr("TransactionDate").ToString().Trim() & "' And sIcd9Code='" & dr("sIcd9Code").ToString() & "' And sConceptID ='" & dr("sConceptID").ToString().Trim() & "' And  sCPTCode='" & dr("sCPTCode").ToString().Trim() & "'"


                                    'strsql = "npatientid='" & dr("npatientid").ToString() & "' And  " & " category='" & dr("category").ToString().Trim() & "' And  CodeType='" & dr("CodeType").ToString().Trim() & "' And TransactionDate='" & dr("TransactionDate").ToString().Trim() & "' And sIcd9Code='" & dr("sIcd9Code").ToString() & "' And sConceptID ='" & dr("sConceptID").ToString().Trim() & "' And  sCPTCode='" & dr("sCPTCode").ToString().Trim() & "' And dtDischargeDate='" & dr("dtDischargeDate").ToString().Trim() & "'"
                                    If (smallqry = 0) AndAlso _firstrow = False Then
                                        _dtnewdata.ImportRow(dr)
                                    Else
                                        Dim drr As DataRow() = _dtnewdata.Select(strsql)

                                        If (drr.Length = 0) Then
                                            _dtnewdata.ImportRow(dr)
                                        End If
                                    End If
                                    _firstrow = False
                                    smallqry = 0
                                Next
                                _dtQRDA1Data = _dtnewdata
                            End If
                            '' _dtQRDA1Data = dv.ToTable(True, "npatientid", "category", "CodeType", "TransactionDate", "sIcd9Code", "sConceptID", "sCPTCode", "dtDischargeDate",
                            '"PopCriteriaDesc")
                        End If
                        'End If

                    End If
                End If
            End If
            objgloCDAWriter.dtQRDA1Data = _dtQRDA1Data
            If _isGroupReporting Then
                objgloCDAWriter.IsGroupReporting = True

            End If
            objgloCDAWriter.nQRDAFileType = gloCCDSchema.CDAFileTypeEnum.QRDA1

            objgloCDAWriter.SelectedQRDAIFilePath = _SelectedFilePath
            objgloCDAWriter.ReportingYear = ReportingYear

            mStrXMLfile = objgloCDAWriter.GenerateQRDAI(mPatient.PatientName.LastName, mPatient.PatientName.FirstName)


            _VisitID = 0

            Return mStrXMLfile
        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            If Not IsNothing(dsPatient) Then
                dsPatient.Dispose()
                dsPatient = Nothing
            End If
            If Not IsNothing(objgloCDAWriter) Then
                objgloCDAWriter.Dispose()
                objgloCDAWriter = Nothing
            End If
            'If Not IsNothing(dtvitals) Then
            '    dtvitals.Dispose()
            '    dtvitals = Nothing
            'End If
            If (IsNothing(mPatient) = False) Then
                mPatient.Dispose()
                mPatient = Nothing
            End If
            mCCDSection = String.Empty
            _PatientID = Nothing
        End Try
    End Function
#End Region
    Public Function GetPatientDetailInformation(ByVal nPatientID As Int64, ByVal nLoginID As Int64) As DataSet
        Dim dsPatient As New DataSet
        Dim daPatient As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oConnection As SqlConnection = Nothing
        Try
            cmd = New SqlCommand
            oConnection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.Connection = oConnection
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCD_GetPatientInfo"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@LoginID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nLoginID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            daPatient = New SqlDataAdapter(cmd)
            daPatient.Fill(dsPatient)
            dsPatient.Tables(0).TableName = "Patient"
            dsPatient.Tables(1).TableName = "User"
            dsPatient.Tables(2).TableName = "Clinic"
            dsPatient.Tables(3).TableName = "Insurance"
            dsPatient.Tables(4).TableName = "Setting"
            dsPatient.Tables(5).TableName = "Provider"
            dsPatient.Tables(6).TableName = "PatientOtherDetails"
            dsPatient.Tables(7).TableName = "Race"
            dsPatient.Tables(8).TableName = "Ethnicity"

            Return dsPatient
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(daPatient) Then
                daPatient.Dispose()
                daPatient = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oConnection) Then
                oConnection.Close()
                oConnection.Dispose()
                oConnection = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(dsPatient) Then
            '    dsPatient.Dispose()
            '    dsPatient = Nothing
            'End If
        End Try
    End Function

    Public Function GetPracticeInfo() As DataTable
        Dim dtClinic As New DataTable
        Dim daClinic As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oConnection As SqlConnection = Nothing
        Try
            cmd = New SqlCommand
            oConnection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.Connection = oConnection

            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_ScanClinic"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@ClinicID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = 1

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            daClinic = New SqlDataAdapter(cmd)
            daClinic.Fill(dtClinic)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            If Not IsNothing(daClinic) Then
                daClinic.Dispose()
                daClinic = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oConnection) Then
                oConnection.Close()
                oConnection.Dispose()
                oConnection = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        Return dtClinic
    End Function
    Public Function GetPatientInformation(ByVal _table As DataTable) As gloCCDLibrary.Patient
        Dim opatient As gloCCDLibrary.Patient
        opatient = New gloCCDLibrary.Patient
        Try

            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then

                    opatient.PatientName.Code = _table.Rows(0)("spatientcode")
                    opatient.PatientName.FirstName = _table.Rows(0)("sFirstName")
                    opatient.PatientName.MiddleName = _table.Rows(0)("sMiddleName")
                    opatient.PatientName.LastName = _table.Rows(0)("sLastName")
                    opatient.Gender = _table.Rows(0)("sGender")
                    opatient.DateofBirth = _table.Rows(0)("dtDOB")
                    opatient.PatientName.PersonContactAddress.Street = _table.Rows(0)("sAddressLine1")
                    opatient.PatientName.PersonContactAddress.AddressLine2 = _table.Rows(0)("sAddressLine2")
                    opatient.PatientName.PersonContactAddress.City = _table.Rows(0)("sCity")
                    opatient.PatientName.PersonContactAddress.State = _table.Rows(0)("sState")
                    opatient.PatientName.PersonContactAddress.Zip = _table.Rows(0)("sZip")
                    opatient.PatientName.PersonContactAddress.Country = _table.Rows(0)("sCountry")
                    opatient.County = _table.Rows(0)("sCounty")
                    opatient.SSN = _table.Rows(0)("nSSN")
                    opatient.MaritalStatus = _table.Rows(0)("sMaritalStatus")
                    opatient.Phone = _table.Rows(0)("sPhone")
                    opatient.Mobile = _table.Rows(0)("sMobile")
                    opatient.Email = _table.Rows(0)("sEmail")
                    opatient.Race = _table.Rows(0)("sRace")
                    opatient.Guardian_fName = _table.Rows(0)("sGuardian_fName")
                    opatient.Guardian_mName = _table.Rows(0)("sGuardian_mName")
                    opatient.Guardian_lName = _table.Rows(0)("sGuardian_lName")
                    opatient.Guardian_Address1 = _table.Rows(0)("sGuardian_Address1")
                    opatient.Guardian_Address2 = _table.Rows(0)("sGuardian_Address2")
                    opatient.Guardian_City = _table.Rows(0)("sGuardian_City")
                    opatient.Guardian_State = _table.Rows(0)("sGuardian_State")
                    opatient.Guardian_ZIP = _table.Rows(0)("sGuardian_ZIP")
                    opatient.Guardian_County = _table.Rows(0)("sGuardian_County")
                    opatient.Guardian_Phone = _table.Rows(0)("sGuardian_Phone")
                    opatient.Guardian_Email = _table.Rows(0)("sGuardian_Email")
                    opatient.Guardian_Country = _table.Rows(0)("sGuardian_Country")
                    opatient.Ethnicity = _table.Rows(0)("sEthn")
                    opatient.Language = _table.Rows(0)("sLang")

                    If IsDBNull(_table.Rows(0)("RaceCode")) = False Then
                        opatient.RaceCode = _table.Rows(0)("RaceCode")
                    Else
                        opatient.RaceCode = ""
                    End If
                    If IsDBNull(_table.Rows(0)("EthnCode")) = False Then
                        opatient.ethnicGroupCode = _table.Rows(0)("EthnCode")
                    Else
                        opatient.ethnicGroupCode = ""
                    End If
                    If IsDBNull(_table.Rows(0)("LangCode")) = False Then
                        opatient.LanguageCode = _table.Rows(0)("LangCode")
                    Else
                        opatient.LanguageCode = ""
                    End If
                    opatient.CommPreference = _table.Rows(0)("sCommunicationPreference")
                    opatient.PatientName.Suffix = _table.Rows(0)("Suffix")
                End If
            End If
            Return opatient

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(opatient) Then
                opatient.Dispose()
                opatient = Nothing
            End If
        End Try

    End Function

    Public Function GetPatientRaceDetails(ByVal _table As DataTable) As gloCCDLibrary.RaceCol
        Dim oRaceCol As gloCCDLibrary.RaceCol = Nothing
        Dim oCDCCol As gloCCDLibrary.CDCCol = Nothing
        Dim oRaceDetails As gloCCDLibrary.RaceDetails = Nothing
        Dim dv As DataView = Nothing
        Try
            oRaceCol = New gloCCDLibrary.RaceCol
            Dim _RaceCount As Int64 = 0
            oRaceDetails = New gloCCDLibrary.RaceDetails
            If Not IsNothing(_table) Then
                Dim _OMBCode As String = ""
                dv = _table.DefaultView
                If _table.Rows.Count > 0 Then
                    For i As Integer = 0 To _table.Rows.Count - 1

                        oCDCCol = New gloCCDLibrary.CDCCol
                        oRaceDetails = New gloCCDLibrary.RaceDetails()
                        '   If Convert.ToString(_table.Rows(i)("OMBCode")) <> Convert.ToString(_table.Rows(i)("CDCCode")) Then


                        oRaceDetails.OMBCode = Convert.ToString(_table.Rows(i)("OMBCode"))
                        oRaceDetails.OMBDescription = Convert.ToString(_table.Rows(i)("OMBDescription"))
                        oRaceDetails.CDCCode = Convert.ToString(_table.Rows(i)("CDCCode"))
                        oRaceDetails.CDCDescription = Convert.ToString(_table.Rows(i)("CDCDescription"))
                        '  If Convert.ToString(_table.Rows(i)("OMBCode")) <> Convert.ToString(_table.Rows(i)("CDCCode")) Then

                        '   oRaceCol.Add(oRaceDetails)

                        If _OMBCode <> oRaceDetails.OMBCode Then
                            _OMBCode = oRaceDetails.OMBCode



                            dv.RowFilter = "OMBCode='" & oRaceDetails.OMBCode & "'"
                            'If Convert.ToString(_table.Rows(i)("OMBCode")) <> Convert.ToString(_table.Rows(i)("CDCCode")) Then
                            If dv.Count > 0 Then


                                For j As Integer = 0 To dv.ToTable.Rows.Count - 1
                                    'If _OMBCode <> oRaceDetails.OMBCode Then
                                    '    _OMBCode = oRaceDetails.OMBCode

                                    Dim oCDCRaceDetails As New CDCRaceDetails
                                    If oRaceDetails.OMBCode <> oCDCRaceDetails.CDCCode Then

                                        If oRaceDetails.OMBCode <> Convert.ToString(dv.ToTable.Rows(j)("CDCCode")) Then
                                            oCDCRaceDetails.CDCCode = Convert.ToString(dv.ToTable.Rows(j)("CDCCode"))
                                            oCDCRaceDetails.CDCDescription = Convert.ToString(dv.ToTable.Rows(j)("CDCDescription"))
                                            ' oCDCCol = New CDCCol
                                            oCDCCol.Add(oCDCRaceDetails)
                                            _RaceCount = _RaceCount + 1
                                            oCDCRaceDetails.Dispose()
                                            oCDCRaceDetails = Nothing
                                            oRaceDetails.CDCCol = oCDCCol
                                        End If
                                    End If
                                    ' Exit For
                                    'oRaceDetails.Dispose()
                                    'oRaceDetails = Nothing
                                    ' End If
                                Next
                                oCDCCol.Dispose()
                                oCDCCol = Nothing

                            End If
                            'End If
                            _RaceCount = _RaceCount + 1

                            oRaceCol.Add(oRaceDetails)
                        End If
                        '  End If
                    Next
                End If
            End If
            oRaceCol.RaceCount = _RaceCount
            Return oRaceCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oRaceDetails) Then
                oRaceDetails.Dispose()
                oRaceDetails = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientEthnicityDetails(ByVal _table As DataTable) As gloCCDLibrary.EthnicityCol
        Dim oEthnicityCol As gloCCDLibrary.EthnicityCol = Nothing
        Dim oCDCCol As gloCCDLibrary.CDCCol = Nothing
        Dim oRaceDetails As gloCCDLibrary.RaceDetails = Nothing
        Dim dv As DataView = Nothing
        Dim _EthCount As Int64 = 0
        Try
            oEthnicityCol = New gloCCDLibrary.EthnicityCol

            oRaceDetails = New gloCCDLibrary.RaceDetails
            If Not IsNothing(_table) Then
                Dim _OMBCode As String = ""
                dv = _table.DefaultView
                If _table.Rows.Count > 0 Then
                    For i As Integer = 0 To _table.Rows.Count - 1
                        oCDCCol = New gloCCDLibrary.CDCCol
                        oRaceDetails = New gloCCDLibrary.RaceDetails()
                        oRaceDetails.OMBCode = Convert.ToString(_table.Rows(i)("OMBCode"))
                        oRaceDetails.OMBDescription = Convert.ToString(_table.Rows(i)("OMBDescription"))
                        '  If Convert.ToString(_table.Rows(i)("OMBCode")) <> Convert.ToString(_table.Rows(i)("CDCCode")) Then



                        If _OMBCode <> oRaceDetails.OMBCode Then
                            _OMBCode = oRaceDetails.OMBCode



                            dv.RowFilter = "OMBCode='" & oRaceDetails.OMBCode & "'"
                            'If Convert.ToString(_table.Rows(i)("OMBCode")) <> Convert.ToString(_table.Rows(i)("CDCCode")) Then
                            If dv.Count > 0 Then


                                For j As Integer = 0 To dv.ToTable.Rows.Count - 1
                                    'If _OMBCode <> oRaceDetails.OMBCode Then
                                    '    _OMBCode = oRaceDetails.OMBCode

                                    Dim oCDCRaceDetails As New CDCRaceDetails
                                    If oRaceDetails.OMBCode <> oCDCRaceDetails.CDCCode Then

                                        If oRaceDetails.OMBCode <> Convert.ToString(dv.ToTable.Rows(j)("CDCCode")) Then
                                            oCDCRaceDetails.CDCCode = Convert.ToString(dv.ToTable.Rows(j)("CDCCode"))
                                            oCDCRaceDetails.CDCDescription = Convert.ToString(dv.ToTable.Rows(j)("CDCDescription"))
                                            ' oCDCCol = New CDCCol
                                            oCDCCol.Add(oCDCRaceDetails)
                                            _EthCount = _EthCount + 1
                                            oCDCRaceDetails.Dispose()
                                            oCDCRaceDetails = Nothing
                                            oRaceDetails.CDCCol = oCDCCol
                                        End If
                                    End If
                                    ' Exit For
                                    'oRaceDetails.Dispose()
                                    'oRaceDetails = Nothing
                                    ' End If
                                Next
                                oCDCCol.Dispose()
                                oCDCCol = Nothing

                            End If
                            'End If
                            oEthnicityCol.Add(oRaceDetails)
                            _EthCount = _EthCount + 1
                        End If
                        ' End If
                    Next
                End If
            End If
            oEthnicityCol.EthCount = _EthCount
            Return oEthnicityCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally

            If Not IsNothing(oRaceDetails) Then
                oRaceDetails.Dispose()
                oRaceDetails = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientProviderInfo(ByVal _table As DataTable) As gloCCDLibrary.ProviderCol
        Dim oPatientProvider As gloCCDLibrary.ProviderCol = Nothing
        Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
        Try
            oPatientProvider = New gloCCDLibrary.ProviderCol

            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    ''As per QualityNet after first validation
                    For k As Integer = 0 To _table.Rows.Count - 1

                        oProvider = New gloCCDLibrary.PatientProvider
                        oProvider.NPI = _table.Rows(k)("sNPI")
                        oProvider.ProvTaxID = Convert.ToString(_table.Rows(k)("sTaxID"))
                        oProvider.LastName = Convert.ToString(_table.Rows(k)("sLastName"))
                        oProvider.MiddleName = Convert.ToString(_table.Rows(k)("sMiddleName"))
                        oProvider.FirstName = Convert.ToString(_table.Rows(k)("sFirstName"))
                        oProvider.StreetAddress = Convert.ToString(_table.Rows(k)("sAddress")) + " " + Convert.ToString(_table.Rows(k)("sStreet"))
                        oProvider.City = Convert.ToString(_table.Rows(k)("sCity"))
                        oProvider.State = Convert.ToString(_table.Rows(k)("sState"))
                        oProvider.zip = Convert.ToString(_table.Rows(k)("sZIP"))
                        oProvider.WorkPhone = Convert.ToString(_table.Rows(k)("sPhoneNo"))
                        oProvider.MobilePhone = Convert.ToString(_table.Rows(k)("sMobileNo"))
                        oProvider.Suffix = Convert.ToString(_table.Rows(k)("sSuffix"))
                        oProvider.Prefix = Convert.ToString(_table.Rows(k)("sPrefix"))

                        oPatientProvider.Add(oProvider)
                    Next
                End If
            End If

            Return oPatientProvider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            'If Not IsNothing(oProvider) Then
            '    oProvider.Dispose()
            '    oProvider = Nothing
            'End If
            If Not IsNothing(oPatientProvider) Then
                oPatientProvider.Dispose()
                oPatientProvider = Nothing
            End If
        End Try
    End Function
    Public Function GetLoginProviderInfo(ByVal _table As DataTable) As gloCCDLibrary.LoginProvider
        Dim loginprovider As gloCCDLibrary.LoginProvider = Nothing
        Try
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    ''As per QualityNet after first validation
                    loginprovider = New gloCCDLibrary.LoginProvider()
                    loginprovider.NPI = _table.Rows(0)("sNPI")
                    loginprovider.ProvTaxID = _table.Rows(0)("sTaxID")
                    loginprovider.LastName = _table.Rows(0)("sLastName")
                    loginprovider.MiddleName = _table.Rows(0)("sMiddleName")
                    loginprovider.FirstName = _table.Rows(0)("sFirstName")
                    loginprovider.StreetAddress = _table.Rows(0)("sAddress") + " " + _table.Rows(0)("sStreet")
                    loginprovider.City = _table.Rows(0)("sCity")
                    loginprovider.State = _table.Rows(0)("sState")
                    loginprovider.zip = _table.Rows(0)("sZIP")
                    loginprovider.WorkPhone = _table.Rows(0)("sPhoneNo")
                    loginprovider.MobilePhone = _table.Rows(0)("sMobileNo")
                    loginprovider.Suffix = _table.Rows(0)("sSuffix")
                    loginprovider.Prefix = _table.Rows(0)("sPrefix")


                End If
            End If

            Return loginprovider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            'If Not IsNothing(oProvider) Then
            '    oProvider.Dispose()
            '    oProvider = Nothing
            'End If
            If Not IsNothing(loginprovider) Then
                loginprovider.Dispose()
                loginprovider = Nothing
            End If
        End Try
    End Function

    Public Function GetPatientInsuranceInfo(ByVal _table As DataTable) As gloCCDLibrary.InsuranceCol
        Dim oPatientInsurance As gloCCDLibrary.InsuranceCol = Nothing
        Dim oInsurance As gloCCDLibrary.Insurance = Nothing
        Try
            oPatientInsurance = New gloCCDLibrary.InsuranceCol()
            If Not IsNothing(_table) Then
                For i As Integer = 0 To _table.Rows.Count - 1
                    oInsurance = New gloCCDLibrary.Insurance()
                    '<Insurance Plan details>
                    oInsurance.InsuranceName = Convert.ToString(_table.Rows(i)("sInsuranceName"))
                    oInsurance.InsuranceId = Convert.ToString(_table.Rows(i)("sSubscriberId"))
                    oInsurance.GroupNo = Convert.ToString(_table.Rows(i)("sGroup"))
                    oInsurance.InsRelation = Convert.ToString(_table.Rows(i)("sRelationShip"))
                    If Not _table.Rows(i)("dtStartDate") Is DBNull.Value Then
                        oInsurance.InsStartdate = _table.Rows(i)("dtStartDate")
                    End If
                    If Not _table.Rows(i)("dtEndDate") Is DBNull.Value Then
                        oInsurance.InsEndDate = _table.Rows(i)("dtEndDate")
                    End If
                    oInsurance.InsTypeCode = Convert.ToString(_table.Rows(i)("InsTypeCode"))
                    oInsurance.InsuranceType = Convert.ToString(_table.Rows(i)("InsTypeDesc"))
                    '<Insurance Subscriber details>
                    oInsurance.InsuranceSubscriber1.FirstName = Convert.ToString(_table.Rows(i)("sSubFName"))
                    oInsurance.InsuranceSubscriber1.MiddleName = Convert.ToString(_table.Rows(i)("sSubMName"))
                    oInsurance.InsuranceSubscriber1.LastName = Convert.ToString(_table.Rows(i)("sSubLName"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.Street = Convert.ToString(_table.Rows(i)("sSubscriberAddressLine1"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.AddressLine2 = Convert.ToString(_table.Rows(i)("sSubscriberAddressLine2"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.City = Convert.ToString(_table.Rows(i)("sSubscriberCity"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.State = Convert.ToString(_table.Rows(i)("sSubscriberState"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.Zip = Convert.ToString(_table.Rows(i)("sSubscriberZip"))
                    oInsurance.InsuranceSubscriber1.PersonContactAddress.Country = Convert.ToString(_table.Rows(i)("sSubscriberCountry"))
                    '<Insurance Company details>
                    oInsurance.InsCompanyName = Convert.ToString(_table.Rows(i)("sInsCompName"))
                    oInsurance.InsSubsAddressLine1 = Convert.ToString(_table.Rows(i)("sInsCompAddressLine1"))
                    oInsurance.InsSubsAddressLine2 = Convert.ToString(_table.Rows(i)("sInsCompAddressLine2"))
                    oInsurance.InsSubsCity = Convert.ToString(_table.Rows(i)("sInsCompsCity"))
                    oInsurance.InsSubsState = Convert.ToString(_table.Rows(i)("sInsCompsState"))
                    oInsurance.InsSubsZip = Convert.ToString(_table.Rows(i)("sInsCompsZip"))
                    oInsurance.InsCompanyPhoneNumber = Convert.ToString(_table.Rows(i)("sInsCompPhone"))
                    oPatientInsurance.Add(oInsurance)
                Next
            End If
            Return oPatientInsurance
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oInsurance) Then
                oInsurance.Dispose()
                oInsurance = Nothing
            End If

            If Not IsNothing(oPatientInsurance) Then
                oPatientInsurance.Dispose()
                oPatientInsurance = Nothing
            End If
        End Try
    End Function
    Public Function GetClinicInfo(ByVal _table As DataTable) As gloCCDLibrary.Clinic
        Dim oClinic As gloCCDLibrary.Clinic = Nothing
        Try
            oClinic = New gloCCDLibrary.Clinic
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    oClinic.ClinicName = Convert.ToString(_table.Rows(0)("ClinicName"))
                    ''As per QualityNet After First Validation on CMS
                    oClinic.ClinicTaxID = Convert.ToString(_table.Rows(0)("sTAXID"))
                    oClinic.PersonContactAddress.Street = Convert.ToString(_table.Rows(0)("sAddress1")) + " " + Convert.ToString(_table.Rows(0)("sStreet"))
                    oClinic.PersonContactAddress.City = Convert.ToString(_table.Rows(0)("sCity"))
                    oClinic.PersonContactAddress.State = Convert.ToString(_table.Rows(0)("sState"))
                    oClinic.PersonContactAddress.Zip = Convert.ToString(_table.Rows(0)("sZIP"))
                    oClinic.PersonContactAddress.Country = Convert.ToString(_table.Rows(0)("sCountry"))
                    oClinic.PersonContactPhone.Phone = Convert.ToString(_table.Rows(0)("sPhoneNo"))
                    oClinic.PersonContactPhone.Phone = Convert.ToString(_table.Rows(0)("sPhoneNo"))
                    oClinic.TaxonomyCode = Convert.ToString(_table.Rows(0)("sTaxonomyCode"))
                    oClinic.TaxonomyDesc = Convert.ToString(_table.Rows(0)("sTaxonomyClassification"))
                    If Convert.ToString(_table.Rows(0)("sContactName")).Trim() <> "" Then
                        Dim strconname As String = _table.Rows(0)("sContactName").Trim()
                        Dim splstrconname As String() = strconname.Split(" ")
                        If (splstrconname.Length > 1) Then
                            oClinic.OfficeContactFirstname = splstrconname(0)
                            oClinic.OfficeContactLastname = splstrconname(1)
                        ElseIf (splstrconname.Length = 1) Then
                            oClinic.OfficeContactFirstname = splstrconname(0)
                        End If
                    End If
                    oClinic.ClinicId = _table.Rows(0)("nclinicId")
                End If
            End If
            Return oClinic
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oClinic) Then
                oClinic.Dispose()
                oClinic = Nothing
            End If
        End Try
    End Function
    Public Function GetPracticeContact(ByVal _table As DataTable, ByVal IntegrationID As String) As gloCCDLibrary.PracticeContact
        Dim oPracticeContact As gloCCDLibrary.PracticeContact = Nothing
        Try
            oPracticeContact = New gloCCDLibrary.PracticeContact
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    oPracticeContact.ContactName = _table.Rows(0)("sPLContactName")
                    oPracticeContact.PersonContactAddress.Street = _table.Rows(0)("sPLAddressline1")
                    oPracticeContact.PersonContactAddress.AddressLine2 = _table.Rows(0)("sPLAddressline2")
                    oPracticeContact.PersonContactAddress.City = _table.Rows(0)("sPLCity")
                    oPracticeContact.PersonContactAddress.State = _table.Rows(0)("sPLState")
                    oPracticeContact.PersonContactAddress.Zip = _table.Rows(0)("sPLZIP")
                    oPracticeContact.PersonContactAddress.Country = _table.Rows(0)("sPLCountry")
                    oPracticeContact.PersonContactPhone.Phone = _table.Rows(0)("sPLPhoneNo")
                    oPracticeContact.ContactEmail = _table.Rows(0)("sPLEmail")
                    oPracticeContact.PracticeID = IntegrationID ' _table.Rows(0)("sExternalCode")
                End If
            End If
            Return oPracticeContact
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally

        End Try
    End Function
    Public Function GetLatestAllergiesinfo_New(ByVal npatientid As Int64) As gloCCDLibrary.AllergiesCol

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oAllergies As gloCCDLibrary.Allergies = Nothing
        Dim oAllergiesCol As gloCCDLibrary.AllergiesCol = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim ds As New DataSet
        Dim da As SqlDataAdapter = Nothing
        Dim dtAllergy As DataTable = Nothing
        Dim dtrxnorm As DataTable = Nothing

        Try
            'oAllergies = New gloCCDLibrary.Allergies
            oAllergiesCol = New gloCCDLibrary.AllergiesCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDAGetPatientAllergies"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@visitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = 0

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            If IsNothing(ds) = False Then
                If ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "Allergy"
                    ds.Tables(1).TableName = "RxNorm"
                End If
            End If

            If IsNothing(ds.Tables("Allergy")) = False Then
                dtAllergy = ds.Tables("Allergy")
            Else
                dtAllergy = New DataTable
            End If
            If IsNothing(ds.Tables("RxNorm")) = False Then
                dtrxnorm = ds.Tables("RxNorm")
            Else
                dtrxnorm = New DataTable
            End If

            If dtAllergy.Rows.Count > 0 Then

                For i As Integer = 0 To dtAllergy.Rows.Count - 1

                    oAllergies = New Allergies
                    oAllergies.ProductName = dtAllergy.Rows(i)("HistoryItem")
                    oAllergies.ProductCode = dtAllergy.Rows(i)("sNDCCode")
                    If Convert.ToString(dtAllergy.Rows(i)("RxNormID")) <> "" Then
                        oAllergies.RxNormID = Convert.ToString(dtAllergy.Rows(i)("RxNormID"))
                    Else
                        If oAllergies.ProductCode <> "" Then
                            Dim _sNDCCode As String = oAllergies.ProductCode
                            If _sNDCCode <> "" Then
                                If IsNothing(dtrxnorm) = False Then
                                    If dtrxnorm.Rows.Count > 0 Then
                                        For j As Integer = 0 To dtrxnorm.Rows.Count - 1
                                            If dtrxnorm.Rows(j)("ATV") = oAllergies.ProductCode Then
                                                oAllergies.RxNormID = dtrxnorm.Rows(j)("RxNorm")
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If

                                If IsNothing(oAllergies.RxNormID) Then
                                    oAllergies.RxNormID = ""
                                End If
                                If oAllergies.RxNormID <> "" Then
                                    oAllergies.ProductCode = oAllergies.RxNormID
                                Else
                                    oAllergies.ProductCode = ""
                                End If

                            End If

                        End If
                    End If

                    oAllergies.Reaction = dtAllergy.Rows(i)("sReaction")
                    oAllergies.ConceptID = dtAllergy.Rows(i)("sConceptID")

                    'oAllergies.EffectiveStartTime = dtAllergy.Rows(i)("HistoryDate").ToString()
                    oAllergies.EffectiveStartTime = dtAllergy.Rows(i)("ConcernStartDate").ToString()
                    oAllergies.EffectiveEndTime = dtAllergy.Rows(i)("ConcernEndDate").ToString()
                    oAllergies.AllergyStartTime = dtAllergy.Rows(i)("AllergyStartDate").ToString()
                    oAllergies.Severity = dtAllergy.Rows(i)("AllergySeverity").ToString()
                    oAllergies.ConcernStatus = dtAllergy.Rows(i)("ProcStatus").ToString()
                    oAllergies.AllergyEndTime = dtAllergy.Rows(i)("ObservationEndDate").ToString()
                    oAllergies.AllergyIntoleranceType = dtAllergy.Rows(i)("AllergyIntoleranceType").ToString()
                    If Not IsNothing(oAllergies.Reaction) Then
                        If oAllergies.Reaction <> "" Then
                            Dim temp As String() = oAllergies.Reaction.Split("|")
                            If Not IsNothing(temp) Then
                                oAllergies.Reaction = temp(0)
                                oAllergies.Status = temp(1)
                                oAllergies.ReactionCode = SearchSnomedID(oAllergies.Reaction.Trim)
                                If Not IsNothing(oAllergies.Status) Then
                                    If oAllergies.Status.Trim() <> "Active" Then
                                        oAllergies.Dispose()
                                        oAllergies = Nothing
                                        Continue For
                                    End If
                                End If
                            End If
                        End If
                    End If

                    oAllergiesCol.Add(oAllergies)
                    '           oAllergies.Dispose()
                    oAllergies = Nothing
                Next
            End If
            If Not IsNothing(dtAllergy) Then
                dtAllergy.Dispose()
                dtAllergy = Nothing
            End If
            If Not IsNothing(dtrxnorm) Then
                dtrxnorm.Dispose()
                dtrxnorm = Nothing
            End If
            oAllergies = Nothing

            cnn.Close()

            Return oAllergiesCol

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
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
            'If Not IsNothing(oAllergies) Then
            '    oAllergies.Dispose()
            '    oAllergies = Nothing
            'End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(oAllergiesCol) Then
                oAllergiesCol.Dispose()
                oAllergiesCol = Nothing
            End If
        End Try
    End Function

    Private Function GetDIBSettingsURL() As String

        Dim cmdMain As SqlCommand = Nothing
        Dim strDUBURL As String = ""

        Try

            cmdMain = New SqlCommand
            cmdMain.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmdMain.CommandType = CommandType.StoredProcedure
            cmdMain.CommandText = "gspGetDIBURL"

            cmdMain.Connection.Open()
            strDUBURL = cmdMain.ExecuteScalar

            cmdMain.Connection.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())

        Finally

            If IsNothing(cmdMain.Connection) = False Then
                cmdMain.Connection.Dispose()
                cmdMain.Connection = Nothing
            End If

            If IsNothing(cmdMain) = False Then
                cmdMain.Dispose()
                cmdMain = Nothing
            End If

        End Try


        Return strDUBURL

    End Function


    Public Function GetLatestMedicationinfo_New(ByVal npatientid As Int64) As gloCCDLibrary.MedicationsCol
        Dim cmd As SqlCommand = Nothing
        Dim omedication As gloCCDLibrary.Medication = Nothing
        Dim omedicationCol As gloCCDLibrary.MedicationsCol = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim ds As New DataSet
        Dim osqlpara As SqlParameter = Nothing
        Dim dtMedication As DataTable = Nothing
        Dim dtFDA_HL7Codes As DataTable = Nothing
        Try
            'omedication = New gloCCDLibrary.Medication
            omedicationCol = New gloCCDLibrary.MedicationsCol

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDLatestMedications"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = 0

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@dtsystemdate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            If IsNothing(ds) = False Then
                If ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "Medication"

                End If
            End If

            dtMedication = ds.Tables("Medication")
            If IsNothing(ds.Tables("Medication")) = False Then
                If ds.Tables("Medication").Rows.Count > 0 Then
                    Dim ndcList As New List(Of String)
                    Using dtNDCs As DataTable = New DataView(dtMedication).ToTable(False, New String() {"NDCCode"})
                        If Not IsNothing(dtNDCs) Then
                            If Not IsNothing(dtNDCs) Or dtNDCs.Rows.Count > 0 Then
                                ndcList = dtNDCs.AsEnumerable().[Select](Of String)(Function(q) Convert.ToString(q("NDCCode"))).ToList()
                            End If
                        End If
                    End Using
                    Dim oDrugInfo As New gloGlobal.DIB.ResultSetRxnorm
                    Try

                        '09-Nov-17 Aniket: Resolving Bug #110226: API : CDA > RnNorm Code and Generic Name is not getting display in Medication Section 
                        If IsNothing(gloLibCCDGeneral.sDIBServiceURL) = True OrElse gloLibCCDGeneral.sDIBServiceURL = "" Then
                            gloLibCCDGeneral.sDIBServiceURL = GetDIBSettingsURL()
                        End If

                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                            oDrugInfo = oDIBGSHelper.GetRxnormGenericName(ndcList)
                        End Using
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try

                    Using DrugRouteTable As DataTable = New DataView(dtMedication).ToTable(False, New String() {"DrugRoute"})
                        dtFDA_HL7Codes = gloCCDInterface.GetFDAHL7ForRouteCode(DrugRouteTable)
                    End Using
                    For i As Integer = 0 To dtMedication.Rows.Count - 1
                        omedication = New Medication
                        If IsDBNull(dtMedication.Rows(i)("NDCCode")) = False Then
                            If dtMedication.Rows(i)("NDCCode") <> "" Then
                                omedication.ProdCode = dtMedication.Rows(i)("NDCCode")
                            Else
                                omedication.ProdCode = ""
                            End If
                        Else
                            omedication.ProdCode = ""
                        End If

                        omedication.DrugName = dtMedication.Rows(i)("sMedication")
                        omedication.DrugStrength = dtMedication.Rows(i)("sDosage")
                        omedication.DrugQuantity = dtMedication.Rows(i)("sAmount")
                        omedication.Days = dtMedication.Rows(i)("Duration")
                        If IsDBNull(dtMedication.Rows(i)("Refills")) Then
                            omedication.Refills = 0
                        ElseIf dtMedication.Rows(i)("Refills") = "" Then
                            omedication.Refills = 0
                        Else
                            omedication.Refills = dtMedication.Rows(i)("Refills")
                        End If
                        omedication.Frequency = dtMedication.Rows(i)("Frequency")
                        omedication.Pharmacy = dtMedication.Rows(i)("Pharmacy")
                        omedication.MedicationDate = dtMedication.Rows(i)("MedicationDate").ToString()
                        omedication.Status = dtMedication.Rows(i)("Status")
                        If omedication.Status = "" Then
                            omedication.Status = "Active"
                        End If
                        omedication.Route = dtMedication.Rows(i)("DrugRoute")
                        omedication.CheifComplaint = dtMedication.Rows(i)("Rx_sChiefComplaints")
                        omedication.DrugForm = dtMedication.Rows(i)("sDrugForm")
                        If (Convert.ToString(dtMedication.Rows(i)("sRxNormCode")).Trim() <> "") Then ''if rxnorm code exist set it other wise pull it from service
                            omedication.RxNormCode = dtMedication.Rows(i)("sRxNormCode")
                        Else
                            If IsNothing(oDrugInfo) = False Then
                                If IsNothing(oDrugInfo) = False Then
                                    If oDrugInfo.lgrx.Count > 0 Then
                                        For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                            If item.Ndc = omedication.ProdCode Then
                                                omedication.RxNormCode = item.Rxnorm
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If

                        If Not IsNothing(omedication.ProdCode) Then
                            If omedication.ProdCode <> "" Then
                                If IsNothing(oDrugInfo) = False Then
                                    If oDrugInfo.lgrx.Count > 0 Then
                                        For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                            If item.Ndc = omedication.ProdCode Then
                                                omedication.GenericName = item.Genericname
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If
                        If Not IsNothing(omedication.Route) Then
                            If IsNothing(dtFDA_HL7Codes) = False Then
                                If dtFDA_HL7Codes.Rows.Count > 0 Then
                                    For Each itemRow As DataRow In dtFDA_HL7Codes.Rows
                                        If itemRow("ConceptName").ToString().ToUpper() = omedication.Route.ToString().ToUpper() Then
                                            If itemRow("CodeSystem") = "FDA" Then
                                                omedication.FDACode = itemRow("ConceptCode")
                                            End If
                                            If itemRow("CodeSystem") = "HL7" Then
                                                omedication.HL7Code = itemRow("ConceptCode")
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        If Not IsNothing(omedication.Frequency) Then
                            If omedication.Frequency <> "" Then
                                If omedication.Frequency.Contains("-") Then
                                    Dim Frequency As String() = omedication.Frequency.Split("-")
                                    If Not IsNothing(Frequency) Then
                                        If Frequency.Length = 2 Then
                                            omedication.FrequencyAbb = If(Frequency(0) IsNot Nothing AndAlso Frequency(0) <> "", Frequency(0), Nothing)
                                            omedication.FrequencyDesc = If(Frequency(1) IsNot Nothing AndAlso Frequency(1) <> "", Frequency(1), Nothing)
                                        Else
                                            omedication.FrequencyAbb = Nothing
                                            omedication.FrequencyDesc = Nothing
                                        End If
                                    End If
                                Else
                                    omedication.FrequencyAbb = Nothing
                                    omedication.FrequencyDesc = omedication.Frequency
                                End If

                            End If
                        End If
                        omedication.IsMedicationAdministered = dtMedication.Rows(i)("IsAdministered")
                        omedication.StartDate = dtMedication.Rows(i)("StartDate")
                        omedication.EndDate = Convert.ToString(dtMedication.Rows(i)("EndDate"))
                        omedication.MedicationID = dtMedication.Rows(i)("nMedicationId")
                        omedication.PreviousMedicationid = dtMedication.Rows(i)("PreviousMedicationid")
                        omedicationCol.Add(omedication)
                        '           omedication.Dispose()
                        omedication = Nothing
                    Next
                End If
            End If
            omedication = Nothing

            Return omedicationCol

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtFDA_HL7Codes) Then
                dtFDA_HL7Codes.Dispose()
                dtFDA_HL7Codes = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(dtMedication) Then
                dtMedication.Dispose()
                dtMedication = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            'If Not IsNothing(omedication) Then
            '    omedication.Dispose()
            '    omedication = Nothing
            'End If
            If Not IsNothing(omedicationCol) Then
                omedicationCol.Dispose()
                omedicationCol = Nothing
            End If
        End Try


    End Function

    Public Function GetMedicationAdministeredinfo(ByVal npatientid As Int64) As gloCCDLibrary.MedicationsCol
        Dim cmd As SqlCommand = Nothing
        Dim omedication As gloCCDLibrary.Medication = Nothing
        Dim omedicationCol As gloCCDLibrary.MedicationsCol = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim ds As New DataSet
        Dim osqlpara As SqlParameter = Nothing
        Dim dtMedication As DataTable = Nothing
        Dim dtFDA_HL7Codes As DataTable = Nothing
        Try
            'omedication = New gloCCDLibrary.Medication
            omedicationCol = New gloCCDLibrary.MedicationsCol

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDLatestMedications"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = 0

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@dtsystemdate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@IsMedAdministered"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Boolean
            osqlpara.Value = "true"

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            If IsNothing(ds) = False Then
                If ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "Medication"
                End If
            End If

            dtMedication = ds.Tables("Medication")
            If IsNothing(ds.Tables("Medication")) = False Then
                If ds.Tables("Medication").Rows.Count > 0 Then
                    Dim ndcList As New List(Of String)
                    Using dtNDCs As DataTable = New DataView(dtMedication).ToTable(False, New String() {"NDCCode"})
                        If Not IsNothing(dtNDCs) Then
                            If Not IsNothing(dtNDCs) Or dtNDCs.Rows.Count > 0 Then
                                ndcList = dtNDCs.AsEnumerable().[Select](Of String)(Function(q) Convert.ToString(q("NDCCode"))).ToList()
                            End If
                        End If
                    End Using
                    Dim oDrugInfo As New gloGlobal.DIB.ResultSetRxnorm
                    Try
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                            oDrugInfo = oDIBGSHelper.GetRxnormGenericName(ndcList)
                        End Using
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try

                    Using DrugRouteTable As DataTable = New DataView(dtMedication).ToTable(False, New String() {"DrugRoute"})
                        dtFDA_HL7Codes = gloCCDInterface.GetFDAHL7ForRouteCode(DrugRouteTable)
                    End Using

                    For i As Integer = 0 To dtMedication.Rows.Count - 1
                        omedication = New Medication

                        If IsDBNull(dtMedication.Rows(i)("NDCCode")) = False Then
                            If dtMedication.Rows(i)("NDCCode") <> "" Then
                                omedication.ProdCode = dtMedication.Rows(i)("NDCCode")
                            Else
                                omedication.ProdCode = ""
                            End If
                        Else
                            omedication.ProdCode = ""
                        End If

                        omedication.DrugName = dtMedication.Rows(i)("sMedication")
                        omedication.DrugStrength = dtMedication.Rows(i)("sDosage")
                        omedication.DrugQuantity = dtMedication.Rows(i)("sAmount")
                        omedication.Days = dtMedication.Rows(i)("Duration")
                        If IsDBNull(dtMedication.Rows(i)("Refills")) Then
                            omedication.Refills = 0
                        ElseIf dtMedication.Rows(i)("Refills") = "" Then
                            omedication.Refills = 0
                        Else
                            omedication.Refills = dtMedication.Rows(i)("Refills")
                        End If
                        omedication.Frequency = dtMedication.Rows(i)("Frequency")
                        omedication.Pharmacy = dtMedication.Rows(i)("Pharmacy")
                        omedication.MedicationDate = dtMedication.Rows(i)("MedicationDate").ToString()
                        omedication.Status = dtMedication.Rows(i)("Status")
                        If omedication.Status = "" Then
                            omedication.Status = "Active"
                        End If
                        omedication.Route = dtMedication.Rows(i)("DrugRoute")
                        omedication.CheifComplaint = dtMedication.Rows(i)("Rx_sChiefComplaints")
                        omedication.DrugForm = dtMedication.Rows(i)("sDrugForm")

                        If IsNothing(oDrugInfo) = False Then
                            If IsNothing(oDrugInfo) = False Then
                                If oDrugInfo.lgrx.Count > 0 Then
                                    For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                        If item.Ndc = omedication.ProdCode Then
                                            omedication.RxNormCode = item.Rxnorm
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If
                        If Not IsNothing(omedication.ProdCode) Then
                            If omedication.ProdCode <> "" Then
                                If IsNothing(oDrugInfo) = False Then
                                    If oDrugInfo.lgrx.Count > 0 Then
                                        For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                            If item.Ndc = omedication.ProdCode Then
                                                omedication.GenericName = item.Genericname
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If
                        If Not IsNothing(omedication.Route) Then
                            If IsNothing(dtFDA_HL7Codes) = False Then
                                If dtFDA_HL7Codes.Rows.Count > 0 Then
                                    For Each itemRow As DataRow In dtFDA_HL7Codes.Rows
                                        If itemRow("ConceptName").ToString().ToUpper() = omedication.Route.ToString().ToUpper() Then
                                            If itemRow("CodeSystem") = "FDA" Then
                                                omedication.FDACode = itemRow("ConceptCode")
                                            End If
                                            If itemRow("CodeSystem") = "HL7" Then
                                                omedication.HL7Code = itemRow("ConceptCode")
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        If Not IsNothing(omedication.Frequency) Then
                            If omedication.Frequency <> "" Then
                                If omedication.Frequency.Contains("-") Then
                                    Dim Frequency As String() = omedication.Frequency.Split("-")
                                    If Not IsNothing(Frequency) Then
                                        If Frequency.Length = 2 Then
                                            omedication.FrequencyAbb = If(Frequency(0) IsNot Nothing AndAlso Frequency(0) <> "", Frequency(0), Nothing)
                                            omedication.FrequencyDesc = If(Frequency(1) IsNot Nothing AndAlso Frequency(1) <> "", Frequency(1), Nothing)
                                        Else
                                            omedication.FrequencyAbb = Nothing
                                            omedication.FrequencyDesc = Nothing
                                        End If
                                    End If
                                Else
                                    omedication.FrequencyAbb = Nothing
                                    omedication.FrequencyDesc = omedication.Frequency
                                End If

                            End If
                        End If
                        omedication.IsMedicationAdministered = dtMedication.Rows(i)("IsAdministered")
                        omedication.StartDate = dtMedication.Rows(i)("StartDate")
                        omedicationCol.Add(omedication)
                        'omedication.Dispose()
                        omedication = Nothing
                    Next
                End If
            End If
            omedication = Nothing

            Return omedicationCol

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(dtFDA_HL7Codes) Then
                dtFDA_HL7Codes.Dispose()
                dtFDA_HL7Codes = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
            If Not IsNothing(dtMedication) Then
                dtMedication.Dispose()
                dtMedication = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            'If Not IsNothing(omedication) Then
            '    omedication.Dispose()
            '    omedication = Nothing
            'End If
            If Not IsNothing(omedicationCol) Then
                omedicationCol.Dispose()
                omedicationCol = Nothing
            End If
        End Try

    End Function
    Public Function GetPatientProblems(ByVal npatientid As Int64, Optional ByVal _IsEncounterDx As Boolean = False) As gloCCDLibrary.ProblemsCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oProblems As gloCCDLibrary.Problems = Nothing
        Dim oProblemsCol As gloCCDLibrary.ProblemsCol = Nothing

        Dim osqlpara As SqlParameter = Nothing

        Try
            '   oProblems = New gloCCDLibrary.Problems
            oProblemsCol = New gloCCDLibrary.ProblemsCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            'Code added by kanchan on 20100629, if true then take data from new problemlist table
            'If _IsNewProblemList = True Then
            '    cmd.CommandText = "sp_CCDPatientNewProblems"
            'Else
            'End If
            cmd.CommandText = "gsp_CCDPatientProblems"


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = 0
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate   ''from date and todate pass for certification 2015
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@IsEncounterDiagnosis"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Boolean
            osqlpara.Value = _IsEncounterDx
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oProblems = New Problems
                    oProblems.Condition = oDataReader.Item("Condition")
                    oProblems.DateOfService = oDataReader.Item("DateofService")


                    'Code Start-Added by kanchan on 20100916
                    oProblems.ICD9 = oDataReader.Item("sICD9")
                    oProblems.ICD9Code = oDataReader.Item("ICD9Code")
                    oProblems.ConceptID = oDataReader.Item("ConceptID")
                    oProblems.ProblemType = oDataReader.Item("sProblemType")
                    oProblems.ResolvedDate = Convert.ToString(oDataReader.Item("dtResolvedDate"))
                    oProblems.ConcernStartDate = Convert.ToString(oDataReader.Item("ConcernStartDate"))
                    oProblems.ConcernEndDate = Convert.ToString(oDataReader.Item("ConcernEndDate"))

                    ''Added ICD 10 revision
                    oProblems.ICDRevision = oDataReader.Item("nICDRevision")
                    oProblems.ProblemID = oDataReader.Item("ProblemId")
                    If oDataReader.Item("ConditionStatus") = "0" Or Convert.ToString(oDataReader.Item("sReaction")) <> "" Then
                        If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                            If oDataReader.Item("sReaction").ToString() <> "" Then
                                Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                                If Not IsNothing(temp) Then

                                    oProblems.ProblmReaction = temp(1)
                                    If Not IsNothing(oProblems.ProblmReaction) Then
                                        If oProblems.ProblmReaction.Trim() <> "Active" Then
                                            oProblems.Dispose()
                                            oProblems = Nothing
                                        Else
                                            oProblems.ConditionStatus = "Active"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If oDataReader.Item("ConditionStatus") = "1" Then
                            oProblems.ConditionStatus = "Completed"
                        ElseIf oDataReader.Item("ConditionStatus") = "2" Then
                            oProblems.ConditionStatus = "Active"
                        ElseIf oDataReader.Item("ConditionStatus") = "3" Then
                            oProblems.ConditionStatus = "InActive"
                        ElseIf oDataReader.Item("ConditionStatus") = "4" Then
                            oProblems.ConditionStatus = "Chronic"
                        ElseIf oDataReader.Item("ConditionStatus") = "5" Then
                            oProblems.ConditionStatus = "All"
                        End If
                    End If
                    oProblems.EncounterTypeCode = Convert.ToString(oDataReader.Item("EncounterTypeCode"))
                    oProblems.sConcernStatus = Convert.ToString(oDataReader.Item("ConcernStatus"))
                    'End If
                    If IsNothing(oProblems) = False Then
                        oProblemsCol.Add(oProblems)
                        '              oProblems.Dispose()
                        oProblems = Nothing
                    End If

                End If
            End While
            oProblems = Nothing
            oDataReader.Close()
            oDataReader.Dispose()
            cnn.Close()

            Return oProblemsCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            'If Not IsNothing(_table) Then
            '    _table.Dispose()
            '    _table = Nothing
            'End If
            If Not IsNothing(oProblemsCol) Then
                oProblemsCol.Dispose()
                oProblemsCol = Nothing
            End If
        End Try


    End Function
    Public Function GetPatientVitalsinDT(ByVal npatientid As Int64) As DataTable
        'Dim ogloDB As New gloDataBase
        Dim dtVitals As DataTable = Nothing
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As New SqlDataAdapter
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        'Dim oVitals As gloCCDLibrary.Vitals
        'Dim oVitalsCol As gloCCDLibrary.VitalsCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oVitals = New gloCCDLibrary.Vitals
            'oVitalsCol = New gloCCDLibrary.VitalsCol

            dtVitals = New DataTable
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientVitals"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            oDataReader.SelectCommand = cmd
            oDataReader.Fill(dtVitals)


            'While oDataReader.Read
            '    If oDataReader.HasRows() Then
            '        oVitals = New Vitals

            '        oVitals.VitalDate = oDataReader.Item("dtVitalDate")
            '        oVitals.BloodPressureSittingMax = oDataReader.Item("dBloodPressureSittingMax")
            '        oVitals.BloodPressureSittingMin = oDataReader.Item("dBloodPressureSittingMin")
            '        oVitals.PulsePerMinute = oDataReader.Item("dPulsePerMinute")
            '        oVitals.RespiratoryRate = oDataReader.Item("dRespiratoryRate")
            '        oVitals.Temperature = oDataReader.Item("dTemperature")
            '        oVitals.HeightinInch = oDataReader.Item("dHeightinInch")
            '        oVitals.WeightinKg = oDataReader.Item("dWeightinKg")
            '        oVitals.BMI = oDataReader.Item("dBMI")
            '        oVitals.BSA = oDataReader.Item("BodySurfaceArea")
            '        oVitalsCol.Add(oVitals)
            '        oVitals.Dispose()
            '        oVitals = Nothing
            '    End If
            'End While
            'oVitals = Nothing
            'oDataReader.Close()

            cnn.Close()

            Return dtVitals


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
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
            'If Not IsNothing(dtVitals) Then
            '    dtVitals.Dispose()
            '    dtVitals = Nothing
            'End If
        End Try

    End Function
    Public Function SearchSnomedID(ByVal SearchText As String) As String
        Dim _con As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim _sqlcmd As SqlCommand = Nothing
        Dim _sqlda As New SqlDataAdapter()
        Dim _SnoMedCode As String = ""
        Dim dt As New DataTable()
        Try

            _sqlcmd = New SqlCommand("History_SearchsnomedString", _con)
            _sqlcmd.CommandType = CommandType.StoredProcedure

            _sqlcmd.Parameters.Add("@tempstring", SqlDbType.NVarChar).Value = SearchText
            _sqlda.SelectCommand = _sqlcmd
            _sqlda.Fill(dt)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim results As DataRow() = dt.Select("TermDescription = '" & SearchText & "'")
                If results IsNot Nothing AndAlso results.Length > 0 Then
                    _SnoMedCode = results(0)("CONCEPTID").ToString()
                End If
            End If

            Return _SnoMedCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(_con) Then

                _con.Dispose()
                _con = Nothing
            End If
            If Not IsNothing(_sqlda) Then
                _sqlda.Dispose()
                _sqlda = Nothing
            End If
            If Not IsNothing(_sqlcmd) Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function
    Public Function GetorderComments(ByVal dtcomments1 As DataTable, ByVal labom_orderid As Int64) As gloCCDLibrary.OrderCommentsCol

        Dim ocomments As gloCCDLibrary.OrderComments = Nothing
        Dim ocommentscol As gloCCDLibrary.OrderCommentsCol = Nothing

        'Dim _FillOrderID As String = ""
        Dim dvResults As New DataView

        Try

            If IsNothing(dtcomments1) = False Then
                dvResults = dtcomments1.Copy().DefaultView
            End If
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then

                    '_FillOrderID = labom_orderid
                    ocommentscol = New gloCCDLibrary.OrderCommentsCol
                    Dim strfilter As String = ""

                    strfilter = "labom_OrderID = '" & labom_orderid & "'"

                    dvResults.RowFilter = strfilter
                    For j As Integer = 0 To dvResults.Count - 1
                        ocomments = New gloCCDLibrary.OrderComments
                        ocomments.ReviewedBy = Convert.ToString(dvResults(j)("ReviewdBy"))
                        ocomments.Labom_OrderId = Convert.ToString(dvResults(j)("labom_orderid"))
                        ocomments.Patientnote = Convert.ToString(dvResults(j)("patientnote"))
                        ocommentscol.Add(ocomments)
                        ocomments = Nothing
                    Next
                End If
            End If
            Return ocommentscol
        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally

            'If Not IsNothing(dtcomments) Then
            '    dtcomments.Dispose()
            '    dtcomments = Nothing
            'End If

            'If Not IsNothing(dvResults) Then
            '    dvResults.Dispose()
            '    dvResults = Nothing
            'End If
        End Try
    End Function


    Public Function GetLabTestsWithResult(ByVal npatientid As Int64) As gloCCDLibrary.LabTestCol

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oLabResults As gloCCDLibrary.LabResults = Nothing
        Dim oLabResultsCol As gloCCDLibrary.LabResultsCol = Nothing
        Dim dtResults As New DataTable


        Dim da As SqlDataAdapter = Nothing
        Dim oLabTest As gloCCDLibrary.LabTest = Nothing
        Dim oLabTestCol As gloCCDLibrary.LabTestCol = Nothing


        Dim dvResults As New DataView


        Dim osqlpara As SqlParameter = Nothing

        Try


            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDALabResults"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtResults)


            If IsNothing(dtResults) = False Then
                dvResults = dtResults.DefaultView
            End If
            Dim dt As DateTime
            Dim _testname As String = ""
            Dim _FillTestCode As String = ""
            Dim _FillOrderID As String = ""
            Dim _FillTestName As String = ""
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then
                    oLabTestCol = New gloCCDLibrary.LabTestCol


                    For i As Integer = 0 To dvResults.Table.Rows.Count - 1
                        oLabResultsCol = New gloCCDLibrary.LabResultsCol
                        oLabTest = New gloCCDLibrary.LabTest

                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(i)("labotd_TestID")) Or _FillOrderID <> Convert.ToString(dvResults.Table.Rows(i)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(i)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(i)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(i)("labotd_TestName"))






                            Dim strfilter As String = ""
                            'Dim _resultnumber As Integer
                            '_resultnumber = 0
                            strfilter = "labotd_TestID = '" & dvResults.Table.Rows(i)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(i)("labom_OrderID").ToString() & "' "

                            dvResults.RowFilter = strfilter




                            For j As Integer = 0 To dvResults.Count - 1
                                oLabResults = New gloCCDLibrary.LabResults
                                'oLabResults.LabFacility = dvResults.Table.Rows(i)("labom_ReceivingFacilityCode")
                                'oLabResults.OrderNo = dvResults.Table.Rows(i)("labom_OrderNoID")
                                'oLabResults.ProviderName = dvResults.Table.Rows(i)("labom_ProviderName")
                                If IsNothing(dvResults(j)("labotd_TestID")) = False Then

                                    If IsNothing(dvResults(j)("labotd_TestID")) = False Then

                                        If Convert.ToString(dvResults(j)("labotd_TestID")) <> "" Then
                                            _testname = dvResults(j)("labotd_TestName")
                                            '_resultnumber = dvResults(j)("labotr_testresultnumber")


                                            'Bug #45579: 00000391 : Lab Orders : CCD shows wrong flag status
                                            'Previously there was dtResults is used instead of dvResults
                                            'oLabResults.AbnormalFlag = dvResults(j)("labotrd_AbnormalFlag")

                                            If IsDBNull(dvResults(j)("labotrd_ResultDateTime")) OrElse dvResults(j)("labotrd_ResultDateTime") = "" Then
                                                oLabResults.ResultDate = ""
                                            Else
                                                dt = dvResults(j)("labotrd_ResultDateTime")
                                                oLabResults.ResultDate = dt.ToString()
                                            End If
                                            oLabResults.ResultName = dvResults(j)("labotrd_ResultName")
                                            oLabResults.ResultRange = dvResults(j)("labotrd_ResultRange")
                                            oLabResults.ResultUnit = dvResults(j)("labotrd_ResultUnit")
                                            oLabResults.ResultValue = dvResults(j)("labotrd_ResultValue")

                                            If IsDBNull(dvResults(j)("labotr_SpecimenReceivedDateTime")) OrElse dvResults(j)("labotr_SpecimenReceivedDateTime") = "" Then
                                                oLabResults.SpecimenDate = ""
                                            Else
                                                dt = dvResults(j)("labotr_SpecimenReceivedDateTime")
                                                oLabResults.SpecimenDate = dt.ToString()
                                            End If



                                            oLabResults.TestName = dvResults(j)("labotd_TestName")
                                            oLabResults.ResultComment = dvResults(j)("labotrd_ResultComment")
                                            oLabResults.ResultLOINCID = dvResults(j)("labotrd_LOINCID")

                                            'oLabResults.TestCode = dvResults(j)("labtm_Code")
                                            'oLabResults.ResultType = dvResults(j)("labotd_TestType")
                                            'If IsNothing(dvResults(j)("IsAkw")) = False Then
                                            '    If Convert.ToString(dvResults(j)("IsAkw")) <> "" Then
                                            '        If Convert.ToString(dvResults(j)("IsAkw")) <> "0" Then
                                            '            oLabResults.IsAcknowledge = True
                                            '        Else
                                            '            oLabResults.IsAcknowledge = False
                                            '        End If
                                            '    End If

                                            'End If
                                            oLabResults.TestLOINCID = dvResults(j)("labotd_LOINCCode")
                                            oLabResults.OrderStatusNo = dvResults(j)("OrderStatusNumber")
                                        End If
                                    End If
                                End If
                                oLabResultsCol.Add(oLabResults)
                                'oLabResults.Dispose()
                                oLabResults = Nothing
                            Next







                            oLabTest.LabResults = oLabResultsCol
                            oLabTestCol.Add(oLabTest)
                            'oLabTest.Dispose()
                            oLabTest = Nothing
                        End If
                    Next
                End If
            End If
            'If IsNothing(oLabTest) = False Then
            '    oLabTest.Dispose()
            '    oLabTest = Nothing
            'End If

            Return oLabTestCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
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
            If Not IsNothing(dtResults) Then
                dtResults.Dispose()
                dtResults = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(dvResults) Then
                dvResults.Dispose()
                dvResults = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oLabTestCol) Then
                oLabTestCol.Dispose()
                oLabTestCol = Nothing
            End If
        End Try
    End Function

    Public Function GetLabTestsWithResultCDA(ByVal npatientid As Int64) As gloCCDLibrary.OrderCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oLabResults As gloCCDLibrary.LabResults = Nothing
        Dim oLabResultsCol As gloCCDLibrary.LabResultsCol = Nothing
        Dim dtResults As New DataTable
        Dim dtcomments As New DataTable
        Dim dsResults As New DataSet
        Dim da As SqlDataAdapter = Nothing
        Dim oLabTest As gloCCDLibrary.LabTest = Nothing
        Dim oLabTestCol As gloCCDLibrary.LabTestCol = Nothing

        Dim orders As gloCCDLibrary.Order = Nothing
        Dim orderscol As gloCCDLibrary.OrderCol = Nothing
        Dim dvResults As New DataView
        Dim dvResults1 As New DataView
        Dim dvordersmain As New DataView
        Dim dtorders As New DataTable
        Dim osqlpara As SqlParameter = Nothing
        Dim ocommentscol As gloCCDLibrary.OrderCommentsCol = Nothing
        Try

            oLabTestCol1 = New gloCCDLibrary.LabTestCol
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDALabResults"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dsResults)
            If Not IsNothing(dsResults) Then
                dtResults = dsResults.Tables(0)
                'dtorders = dsResults.Tables(1)
                dtcomments = dsResults.Tables(1)
            End If
            'dtorders.Columns.Add("labom_OrderID")

            If IsNothing(dtResults) = False Then

                If dtResults.Rows.Count > 0 Then
                    dvResults = dtResults.DefaultView
                    dtorders = dtResults.AsEnumerable().GroupBy(Function(r) r.Field(Of Decimal)("labom_OrderID")).[Select](Function(g) g.First()).CopyToDataTable()
                End If




            End If
            'If IsNothing(dtorders) = False Then
            '    dvorders = dtorders.DefaultView
            'End If

            Dim dt As DateTime
            Dim dtTestDate As DateTime
            Dim _testname As String = ""
            Dim _FillTestCode As String = ""
            Dim _FillOrderID As String = ""
            Dim _FillTestName As String = ""
            Dim MainFillOrderId As String = ""
            'Dim temporderid As String = ""
            'Dim tempstrfilter As String = ""

            'If Not IsNothing(dtResults) Then
            '    If dtResults.Rows.Count > 0 Then
            '        For temp As Integer = 0 To dtResults.Rows.Count - 1
            '            If temporderid <> Convert.ToString(dtResults.Rows(temp)("labom_OrderID")) Then
            '                temporderid = Convert.ToString(dtResults.Rows(temp)("labom_OrderID"))
            '                Dim dvorders As New DataView
            '                dvorders = dtorders.Copy().DefaultView
            '                tempstrfilter = "labom_OrderID = '" & temporderid & "'"
            '                dvorders.RowFilter = tempstrfilter
            '                If dvorders.Count <= 0 Then
            '                    dtorders.Rows.Add(temporderid)
            '                End If

            '            End If

            '        Next

            '    End If

            'End If

            If Not IsNothing(dtorders) Then
                If dtorders.Rows.Count > 0 Then

                    dvordersmain = dtorders.Copy().DefaultView
                End If

            End If

            If IsNothing(dvordersmain) = False Then
                If dvordersmain.Count > 0 Then



                    orderscol = New gloCCDLibrary.OrderCol
                    For i As Integer = 0 To dvordersmain.Table.Rows.Count - 1


                        orders = New gloCCDLibrary.Order


                        If MainFillOrderId <> Convert.ToString(dvordersmain.Table.Rows(i)("labom_OrderID")) Then
                            MainFillOrderId = Convert.ToString(dvordersmain.Table.Rows(i)("labom_OrderID"))
                            Dim strfilter As String = ""
                            strfilter = "labom_OrderID = '" & dvordersmain.Table.Rows(i)("labom_OrderID").ToString() & "'"


                            ocommentscol = New gloCCDLibrary.OrderCommentsCol
                            ocommentscol = GetorderComments(dtcomments, MainFillOrderId)
                            oLabTestCol = New gloCCDLibrary.LabTestCol

                            dvResults.RowFilter = strfilter
                            For k As Integer = 0 To dvResults.Count - 1

                                oLabTest = New gloCCDLibrary.LabTest
                                If _FillTestCode <> Convert.ToString(dvResults(k)("labotd_TestID")) Or _FillOrderID <> Convert.ToString(dvResults(k)("labom_OrderID")) Then
                                    _FillTestCode = Convert.ToString(dvResults(k)("labotd_TestID"))
                                    _FillOrderID = Convert.ToString(dvResults(k)("labom_OrderID"))
                                    _FillTestName = Convert.ToString(dvResults(k)("labotd_TestName"))


                                    strfilter = ""

                                    dvResults1 = dvResults.ToTable().Copy().DefaultView
                                    strfilter = "labotd_TestID = '" & dvResults1(k)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults1(k)("labom_OrderID").ToString() & "' "
                                    dvResults1.RowFilter = strfilter
                                    oLabResultsCol = New gloCCDLibrary.LabResultsCol
                                    Dim lablocation As New LabLocation

                                    lablocation.LabLocationName = Convert.ToString(dvResults(k)("labci_ContactName"))
                                    lablocation.LabLocationTelephone = Convert.ToString(dvResults(k)("sPhone"))
                                    lablocation.LabLocationAdd1 = Convert.ToString(dvResults(k)("sAddressLine1"))
                                    lablocation.LabLocationAdd2 = Convert.ToString(dvResults(k)("sAddressLine2"))
                                    lablocation.LabCity = Convert.ToString(dvResults(k)("sCity"))
                                    lablocation.LabState = Convert.ToString(dvResults(k)("sState"))
                                    lablocation.LabZip = Convert.ToString(dvResults(k)("sZip"))
                                    oLabTest.LabLocationDetails = lablocation
                                    oLabTest.OrderId = Convert.ToString(dvResults(k)("labom_OrderID"))
                                    oLabTest.TestId = Convert.ToString(dvResults(k)("labotd_TestID"))
                                    For j As Integer = 0 To dvResults1.Count - 1
                                        oLabResults = New gloCCDLibrary.LabResults
                                        'oLabResults.LabFacility = dvResults.Table.Rows(i)("labom_ReceivingFacilityCode")
                                        'oLabResults.OrderNo = dvResults.Table.Rows(i)("labom_OrderNoID")
                                        'oLabResults.ProviderName = dvResults.Table.Rows(i)("labom_ProviderName")
                                        If IsNothing(dvResults1(j)("labotd_TestID")) = False Then

                                            If IsNothing(dvResults1(j)("labotd_TestID")) = False Then

                                                If Convert.ToString(dvResults1(j)("labotd_TestID")) <> "" Then
                                                    _testname = dvResults1(j)("labotd_TestName")
                                                    '_resultnumber = dvResults(j)("labotr_testresultnumber")

                                                    'Bug #45579: 00000391 : Lab Orders : CCD shows wrong flag status
                                                    'Previously there was dtResults is used instead of dvResults
                                                    oLabResults.AbnormalFlag = dvResults1(j)("labotrd_AbnormalFlag")

                                                    If IsDBNull(dvResults1(j)("labotrd_ResultDateTime")) OrElse dvResults1(j)("labotrd_ResultDateTime") = "" Then
                                                        oLabResults.ResultDate = ""
                                                    Else
                                                        dt = dvResults1(j)("labotrd_ResultDateTime")
                                                        oLabResults.ResultDate = dt.ToString()
                                                    End If
                                                    oLabResults.ResultName = dvResults1(j)("labotrd_ResultName")
                                                    oLabResults.ResultRange = dvResults1(j)("labotrd_ResultRange")
                                                    oLabResults.ResultUnit = dvResults1(j)("labotrd_ResultUnit")
                                                    oLabResults.ResultValue = dvResults1(j)("labotrd_ResultValue")

                                                    If IsDBNull(dvResults1(j)("labotr_SpecimenReceivedDateTime")) OrElse dvResults1(j)("labotr_SpecimenReceivedDateTime") = "" Then
                                                        oLabResults.SpecimenDate = ""
                                                    Else
                                                        dt = dvResults1(j)("labotr_SpecimenReceivedDateTime")
                                                        oLabResults.SpecimenDate = dt.ToString()
                                                    End If

                                                    oLabResults.TestName = dvResults1(j)("labotd_TestName")
                                                    oLabResults.ResultComment = dvResults1(j)("labotrd_ResultComment")
                                                    oLabResults.ResultLOINCID = dvResults1(j)("labotrd_LOINCID")

                                                    'oLabResults.TestCode = dvResults(j)("labtm_Code")
                                                    'oLabResults.ResultType = dvResults(j)("labotd_TestType")
                                                    'If IsNothing(dvResults(j)("IsAkw")) = False Then
                                                    '    If Convert.ToString(dvResults(j)("IsAkw")) <> "" Then
                                                    '        If Convert.ToString(dvResults(j)("IsAkw")) <> "0" Then
                                                    '            oLabResults.IsAcknowledge = True
                                                    '        Else
                                                    '            oLabResults.IsAcknowledge = False
                                                    '        End If
                                                    '    End If

                                                    'End If
                                                    oLabResults.ResultType = dvResults1(j)("Resutltype")
                                                    oLabResults.TestLOINCID = dvResults1(j)("labotd_LOINCCode")
                                                    oLabResults.OrderStatusNo = dvResults1(j)("OrderStatusNumber")
                                                    If Not IsNothing(dvResults1(j)("TestDate")) AndAlso dvResults1(j)("TestDate") <> "" Then
                                                        dtTestDate = dvResults1(j)("TestDate")
                                                        oLabResults.ScheduleDate = dtTestDate
                                                    End If


                                                End If
                                            End If
                                        End If
                                        oLabResultsCol.Add(oLabResults)

                                        oLabResults = Nothing
                                    Next

                                    oLabTest.LabResults = oLabResultsCol
                                    oLabTestCol.Add(oLabTest)
                                    oLabTestCol1.Add(oLabTest)
                                    'If Not IsNothing(mPatient) Then
                                    '    mPatient.LabTests = oLabTestCol
                                    'End If
                                    'oLabTest.Dispose()
                                    oLabTest = Nothing
                                End If
                            Next
                            '_FillOrderID = ""
                            orders.OrderTests = oLabTestCol
                            orders.OrderComments = ocommentscol
                            orderscol.Add(orders)
                            orders = Nothing
                        End If


                    Next
                End If
            End If
            'If IsNothing(oLabTest) = False Then
            '    oLabTest.Dispose()
            '    oLabTest = Nothing
            'End If

            'If Not IsNothing(mPatient) Then
            '    mPatient.LabTests = oLabTestCol1
            'End If
            Return orderscol

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
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
            If Not IsNothing(dtResults) Then
                dtResults.Dispose()
                dtResults = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(dvResults) Then
                dvResults.Dispose()
                dvResults = Nothing
            End If
            If Not IsNothing(dvResults1) Then
                dvResults1.Dispose()
                dvResults1 = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oLabTestCol) Then
                oLabTestCol.Dispose()
                oLabTestCol = Nothing
            End If
            If Not IsNothing(ocommentscol) Then
                ocommentscol.Dispose()
                ocommentscol = Nothing
            End If
            If Not IsNothing(dsResults) Then
                dsResults.Dispose()
                dsResults = Nothing
            End If
            'If Not IsNothing(dtcomments) Then
            '    dtcomments.Dispose()
            '    dtcomments = Nothing
            'End If


        End Try
    End Function

    Public Function GetPatientSocialHistory(ByVal npatientid As Int64) As gloCCDLibrary.SocialHistoryCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oSocialHistory As gloCCDLibrary.SocialHistory = Nothing
        Dim oSocialHistoryCol As gloCCDLibrary.SocialHistoryCol = Nothing

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oSocialHistory = New gloCCDLibrary.SocialHistory
            oSocialHistoryCol = New gloCCDLibrary.SocialHistoryCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDAPatientSocialHistory"


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate ''from date passed for 2015 certification
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate  ''to date passed for 2015 certification
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oSocialHistory = New SocialHistory
                    oSocialHistory.SocialHxDescription = oDataReader.Item("sHistoryItem")
                    oSocialHistory.SocialHxQualifiers = ""
                    oSocialHistory.SocialHxComments = oDataReader.Item("sComments")
                    If IsDBNull(oDataReader.Item("dtVisitDate")) Then
                        oSocialHistory.SocialHxDateReported = ""
                    Else
                        oSocialHistory.SocialHxDateReported = oDataReader.Item("dtVisitDate").ToString() ''''''Insurance company start date
                    End If
                    oSocialHistory.SocialHxConceptID = oDataReader.Item("sConceptID")
                    oSocialHistory.SocialHxCategory = oDataReader.Item("sHistoryCategory")
                    oSocialHistory.SocialOnsetDate = oDataReader.Item("dtOnsetDate")
                    oSocialHistory.SocialEndDate = oDataReader.Item("dtObservationEndDate")
                    oSocialHistory.TobaccoUseCode = oDataReader.Item("sTobaccoUseCode")
                    oSocialHistory.SnomedDesc = oDataReader.Item("SnomedDescription")
                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                            Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                            If Not IsNothing(temp) Then
                                If temp.Length > 0 Then
                                    If Not IsNothing(temp(0)) Then
                                        oSocialHistory.HistoricalStatusDesc = temp(0)
                                    End If
                                End If
                                If temp.Length > 1 Then
                                    If Not IsNothing(temp(1)) Then
                                        oSocialHistory.SocialHxStatus = temp(1)
                                    End If
                                End If
                                If Not IsNothing(oSocialHistory.SocialHxStatus) Then
                                    If oSocialHistory.SocialHxStatus.Trim() <> "Active" Then
                                        oSocialHistory.Dispose()
                                        oSocialHistory = Nothing
                                    End If
                                End If
                            End If
                        End If
                    End If
                    If IsNothing(oSocialHistory) = False Then
                        oSocialHistoryCol.Add(oSocialHistory)
                        '           oSocialHistory.Dispose()
                        oSocialHistory = Nothing
                    End If

                End If
            End While
            oSocialHistory = Nothing
            oDataReader.Close()

            cnn.Close()

            Return oSocialHistoryCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
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
        End Try
        If Not IsNothing(oSocialHistoryCol) Then
            oSocialHistoryCol.Dispose()
            oSocialHistoryCol = Nothing
        End If

    End Function
    Public Function GetPatientProcedure(ByVal npatientid As Int64) As gloCCDLibrary.ProceduresCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oProcedures As gloCCDLibrary.Procedures = Nothing
        Dim oProceduresCol As gloCCDLibrary.ProceduresCol = Nothing

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oProcedures = New gloCCDLibrary.Procedures
            oProceduresCol = New gloCCDLibrary.ProceduresCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientProcedure"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oProcedures = New Procedures
                    oProcedures.ExamName = oDataReader.Item("ExamName")
                    oProcedures.CPTCode = oDataReader.Item("sCPTcode")
                    oProcedures.CPTDescription = oDataReader.Item("sCPTDescription")
                    oProcedures.ICD9_code = oDataReader.Item("sICD9Code")
                    oProcedures.ICD9Description = oDataReader.Item("sICD9Description")
                    oProcedures.ProviderFirstName = oDataReader.Item("ProvFirstName")
                    oProcedures.ProviderMiddleName = oDataReader.Item("ProvMName")
                    oProcedures.ProviderLastName = oDataReader.Item("ProvLastName")
                    oProcedures.ProviderSuffix = oDataReader.Item("ProvSuffix")
                    oProcedures.DateOfService = oDataReader.Item("DateOfService")
                    oProcedures.SnomedCode = oDataReader.Item("sConceptID")
                    ''Added ICD 10 revision
                    oProcedures.ICDRevision = oDataReader.Item("nICDRevision")
                    If oProcedures.DateOfService = "1/1/1900" Then
                        oProcedures.DateOfService = ""

                    End If
                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                            Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                            If Not IsNothing(temp) Then

                                oProcedures.ProcedureStatus = temp(1)
                                If Not IsNothing(oProcedures.ProcedureStatus) Then
                                    If oProcedures.ProcedureStatus.Trim() <> "Active" Then
                                        oProcedures.Dispose()
                                        oProcedures = Nothing

                                    End If
                                End If
                            End If
                        End If
                    End If
                    oProcedures.ImplantDeviceId = oDataReader.Item("ImplantDeviceId")
                    oProcedures.SnomedDescription = oDataReader.Item("sDescription")
                    oProcedures.Status = Convert.ToString(oDataReader.Item("sStatus")).ToLower
                    If IsNothing(oProcedures) = False Then
                        oProceduresCol.Add(oProcedures)
                        '           oProcedures.Dispose()
                        oProcedures = Nothing
                    End If

                End If
            End While
            oProcedures = Nothing
            oDataReader.Close()


            cnn.Close()

            Return oProceduresCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
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
            If Not IsNothing(oProceduresCol) Then
                oProceduresCol.Dispose()
                oProceduresCol = Nothing
            End If
        End Try


    End Function
    Public Function GetPatientEncounter(ByVal npatientid As Int64, ByVal orderid As Int64) As gloCCDLibrary.EncountersCol
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oEncounters As gloCCDLibrary.Encounters = Nothing
        Dim oEncountersCol As gloCCDLibrary.EncountersCol = Nothing

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oEncounters = New gloCCDLibrary.Encounters
            oEncountersCol = New gloCCDLibrary.EncountersCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientEncounter"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@OrderID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = orderid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oEncounters = New Encounters
                    oEncounters.ExamName = oDataReader.Item("sExamName")
                    oEncounters.ProvFirstName = oDataReader.Item("ProvFirstName")
                    oEncounters.ProvMName = oDataReader.Item("ProvMiddleName")
                    oEncounters.ProvLastName = oDataReader.Item("ProvLastName")
                    oEncounters.ProvSuffix = oDataReader.Item("ProvSuffix")
                    oEncounters.Location = Convert.ToString(oDataReader.Item("sLocation"))
                    oEncounters.DateOfService = oDataReader.Item("dtVisitDate").ToString()
                    oEncounters.ExamID = oDataReader.Item("nExamID").ToString()
                    oEncounters.ProvNPI = oDataReader.Item("ProvNPI").ToString()
                    oEncounters.StreetAddress = oDataReader.Item("ProvAddress").ToString() + If(oDataReader.Item("ProvStreet").ToString() <> "", ", ", "") + oDataReader.Item("ProvStreet").ToString()
                    oEncounters.City = oDataReader.Item("ProvCity").ToString()
                    oEncounters.State = oDataReader.Item("ProvState").ToString()
                    oEncounters.PostalCode = oDataReader.Item("ProvZIP").ToString()
                    oEncounters.WorkPhone = oDataReader.Item("ProvPhNo").ToString()
                    oEncounters.Prefix = oDataReader.Item("ProvPrefix").ToString()
                    Dim dt As DataTable = GetLocation(npatientid, oEncounters.DateOfService)
                    If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                        oEncounters.Location = dt.Rows(0)("sLocation").ToString()
                        oEncounters.LocationAdd = New ContactAddress
                        oEncounters.LocationAdd.Street = (dt.Rows(0)("sAddressLine1").ToString())
                        oEncounters.LocationAdd.AddressLine2 = dt.Rows(0)("sAddressLine2").ToString()
                        oEncounters.LocationAdd.City = dt.Rows(0)("sCity").ToString()
                        oEncounters.LocationAdd.State = dt.Rows(0)("sState").ToString()
                        oEncounters.LocationAdd.Zip = dt.Rows(0)("sZIP").ToString()
                        oEncounters.LocationAdd.Country = dt.Rows(0)("sCountry").ToString()
                        oEncounters.StartDateTime = dt.Rows(0)("StartDate").ToString()
                        oEncounters.EndDateTime = dt.Rows(0)("EndDate").ToString()
                    Else
                        If Not IsNothing(dt) Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                        dt = GetPatientLocation(npatientid, gloDateMaster.gloDate.DateAsNumber(oDataReader.Item("dtVisitDate")))
                        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                            oEncounters.Location = dt.Rows(0)("sLocation").ToString()
                            oEncounters.LocationAdd = New ContactAddress
                            oEncounters.LocationAdd.Street = (dt.Rows(0)("sAddressLine1").ToString())
                            oEncounters.LocationAdd.AddressLine2 = dt.Rows(0)("sAddressLine2").ToString()
                            oEncounters.LocationAdd.City = dt.Rows(0)("sCity").ToString()
                            oEncounters.LocationAdd.State = dt.Rows(0)("sState").ToString()
                            oEncounters.LocationAdd.Zip = dt.Rows(0)("sZIP").ToString()
                            oEncounters.LocationAdd.Country = dt.Rows(0)("sCountry").ToString()
                            oEncounters.StartDateTime = dt.Rows(0)("StartDate").ToString()
                            oEncounters.EndDateTime = dt.Rows(0)("EndDate").ToString()
                        End If
                    End If
                    If dt IsNot Nothing Then
                        dt.Dispose()
                        dt = Nothing
                    End If

                    'oEncounters.EncounterCode = oDataReader.Item("sCPTcode")
                    'oEncounters.EncounterName = oDataReader.Item("sCPTDescription")
                    ''integrated by Mayuri:20120814-from glosuite7010
                    ' oEncounters.Location = GetEncounterLocation(npatientid)
                    oEncountersCol.Add(oEncounters)
                    'oEncounters.Dispose()
                    oEncounters = Nothing
                End If
            End While
            oEncounters = Nothing
            oDataReader.Close()

            cnn.Close()

            Return oEncountersCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
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
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oEncountersCol) Then
                oEncountersCol.Dispose()
                oEncountersCol = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientVisitDateAndLocation(ByVal npatientid As Int64) As gloCCDLibrary.EncountersCol
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oEncounters As gloCCDLibrary.Encounters = Nothing
        Dim oEncountersCol As gloCCDLibrary.EncountersCol = Nothing

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oEncounters = New gloCCDLibrary.Encounters
            oEncountersCol = New gloCCDLibrary.EncountersCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Gsp_ccdpatientVisitDateAndLocation"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oEncounters = New Encounters
                    oEncounters.Location = Convert.ToString(oDataReader.Item("sLocation"))
                    oEncounters.LocationAdd = New ContactAddress
                    oEncounters.LocationAdd.Street = Convert.ToString(oDataReader.Item("sAddressLine1"))
                    oEncounters.LocationAdd.AddressLine2 = Convert.ToString(oDataReader.Item("sAddressLine2"))
                    oEncounters.LocationAdd.City = Convert.ToString(oDataReader.Item("sCity"))
                    oEncounters.LocationAdd.State = Convert.ToString(oDataReader.Item("sState"))
                    oEncounters.LocationAdd.Zip = Convert.ToString(oDataReader.Item("sZIP"))
                    oEncounters.LocationAdd.Country = Convert.ToString(oDataReader.Item("sCountry"))
                    oEncounters.StartDateTime = Convert.ToString(oDataReader.Item("StartDate"))
                    oEncounters.EndDateTime = Convert.ToString(oDataReader.Item("EndDate"))

                    oEncountersCol.Add(oEncounters)
                    oEncounters = Nothing
                End If
            End While
            oEncounters = Nothing
            oDataReader.Close()

            cnn.Close()

            Return oEncountersCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
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
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oEncountersCol) Then
                oEncountersCol.Dispose()
                oEncountersCol = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientImmunizations(ByVal npatientid As Int64) As gloCCDLibrary.ImmunizationCol
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oImmunization As gloCCDLibrary.Immunization = Nothing
        Dim oImmunizationCol As gloCCDLibrary.ImmunizationCol = Nothing
        Dim osqlpara As SqlParameter = Nothing

        Try
            'oImmunization = New gloCCDLibrary.Immunization
            oImmunizationCol = New gloCCDLibrary.ImmunizationCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientImmunizations"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oImmunization = New Immunization

                    oImmunization.VaccineName = oDataReader.Item("VaccName")
                    oImmunization.VaccineCode = oDataReader.Item("VaccCode")
                    oImmunization.Route = oDataReader.Item("VacRoute")
                    If Not IsNothing(oImmunization.Route) Then
                        If oImmunization.Route <> "" Then
                            oImmunization.RouteCode = GetCodeForCategory(oImmunization.Route, "Route")
                            If IsDBNull(oDataReader.Item("RouteCode")) = False Then
                                If (oDataReader.Item("RouteCode")) <> "" Then
                                    oImmunization.RouteCode = oDataReader.Item("RouteCode")
                                Else
                                    oImmunization.RouteCode = ""
                                End If
                            Else
                                oImmunization.RouteCode = ""
                            End If
                        Else
                            oImmunization.RouteCode = ""
                            oImmunization.Route = ""
                        End If
                    End If

                    If IsDBNull(oDataReader.Item("VaccDateGiven")) Then
                        oImmunization.ImmunizationDate = ""
                    Else
                        If (Convert.ToString(oDataReader.Item("VaccDateGiven")).Length > 0) Then
                            oImmunization.ImmunizationDate = Convert.ToString(oDataReader.Item("VaccDateGiven"))
                        End If
                    End If

                    oImmunization.ImmunizationStatus = Convert.ToString(oDataReader.Item("Imm_Status"))
                    oImmunization.LotNumber = Convert.ToString(oDataReader.Item("LotNumber"))
                    oImmunization.Manufacture = Convert.ToString(oDataReader.Item("Manufacturer"))
                    oImmunization.ImmtransactionId = oDataReader.Item("im_trn_Trans_Id")
                    oImmunization.ImmunizationNotes = oDataReader.Item("AdditionalNotes")
                    oImmunization.NDCCODE = oDataReader.Item("NDCCode")
                    oImmunizationCol.Add(oImmunization)
                    '       oImmunization.Dispose()
                    oImmunization = Nothing
                End If
            End While
            oImmunization = Nothing
            oDataReader.Close()

            cnn.Close()

            Return oImmunizationCol


        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Throw New gloCCDException(ex.ToString())
        Finally
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
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(oImmunizationCol) Then
                oImmunizationCol.Dispose()
                oImmunizationCol = Nothing
            End If
            'If Not IsNothing(oImmunization) Then
            '    oImmunization.Dispose()
            '    oImmunization = Nothing
            'End If
        End Try
    End Function
    Public Function GetCodeForCategory(ByVal _Description As String, ByVal _CategoryType As String) As String
        Dim _strSQL As String = ""
        Dim _CategoryCode As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "select isnull(sCode,'') from category_mst where sCategoryType='" & _CategoryType & "' and sDescription='" & _Description.Replace("'", "''") & "'"

            cmd.CommandText = _strSQL

            _CategoryCode = cmd.ExecuteScalar
            If IsNothing(_CategoryCode) Then
                _CategoryCode = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _CategoryCode
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
            Return ""
        Finally
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
    'Public Function SaveExportedCCD(ByVal _PatientID As Int64, ByVal sCCDFilePath As String, ByVal _Notes As String, Optional ByVal _sFileCreatedFrom As String = Nothing, Optional ByVal _ISGeneratedAtPatientRequest As Boolean = False, Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000", Optional ByVal FileType As String = "CCD", Optional ByVal nExamID As Int64 = 0) As Boolean
    '    Dim cmd As SqlCommand = Nothing
    '    Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
    '    Dim sqlParam As SqlParameter = Nothing
    '    Try
    '        Dim XMLarrByte As Byte() = ConvertFiletoBinary(sCCDFilePath)


    '        cmd = New SqlCommand("CCD_ExportedFiles", conn)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = 0

    '        sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _PatientID

    '        sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 100)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = mPatient.PatientName.FirstName

    '        sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 100)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = mPatient.PatientName.LastName

    '        If _IsOwnByPastExam Then
    '            sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = Convert.ToDateTime(_DateOfservice)
    '        Else
    '            sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = DateTime.Now
    '        End If

    '        sqlParam = cmd.Parameters.Add("@iData", SqlDbType.Image)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = Nothing

    '        sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = sCCDFilePath

    '        sqlParam = cmd.Parameters.Add("@iXMLData", SqlDbType.Image)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = XMLarrByte

    '        sqlParam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = FileType


    '        sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 100)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _Notes

    '        Dim _fileHashValue As String = ""
    '        Dim _fileHashAlgorithmType As String = ""


    '        _fileHashValue = gloSecurity.gloDataHashing.GetSHA1Hash(sCCDFilePath, _fileHashAlgorithmType)

    '        sqlParam = cmd.Parameters.Add("@sHashValue", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _fileHashValue

    '        sqlParam = cmd.Parameters.Add("@sHashAlgoType", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _fileHashAlgorithmType

    '        sqlParam = cmd.Parameters.Add("@sFileCreatedFrom", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _sFileCreatedFrom

    '        sqlParam = cmd.Parameters.Add("@IsPatientRequest", SqlDbType.Bit)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = _ISGeneratedAtPatientRequest

    '        sqlParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = nExamID

    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        cmd.ExecuteNonQuery()

    '    Catch ex As Exception
    '        Throw New gloCCDException(ex.ToString)
    '    Finally
    '        If Not IsNothing(conn) Then
    '            conn.Close()
    '            conn.Dispose()
    '            conn = Nothing
    '        End If

    '        If Not IsNothing(cmd) Then
    '            cmd.Parameters.Clear()
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '        If Not IsNothing(sqlParam) Then
    '            sqlParam = Nothing
    '        End If
    '    End Try
    '    Return Nothing
    'End Function
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
    Public Function GetLocation(ByVal _PatientID As Int64, ByVal _VisitDate As Date) As DataTable
        Dim _strSQL As String = ""
        Dim _dtResult As DataTable = Nothing
        Dim sqladp As SqlDataAdapter = Nothing
        Dim _AptDat As String = Nothing
        Try
            _AptDat = _VisitDate.ToString("yyyyMMdd")
            _strSQL = " SELECT TOP (1) loc.sLocation,sAddressLine1 ,sAddressLine2,sCity ,sState ,sZIP ,sCountry ," _
            & " CONVERT(varchar, apt.dtStartDate )+ CONVERT(varchar,apt.dtStartTime) as StartDate ," _
            & " CONVERT(varchar,apt.dtEndDate) + CONVERT(varchar,apt.dtEndTime ) as EndDate" _
            & " FROM  AS_Appointment_MST  apt INNER JOIN  AS_Appointment_DTL " _
            & " ON apt.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID " _
            & "inner join dbo.AB_Location loc on apt.nLocationID =loc.nLocationID " _
            & " WHERE ((AS_Appointment_DTL.nRefID=0) AND nUsedStatus NOT IN (5,6,7)" _
            & "and nPatientID = " & _PatientID & " and apt.dtStartDate =" & _AptDat & ")"
            sqladp = New SqlDataAdapter(_strSQL, gloLibCCDGeneral.Connectionstring)
            _dtResult = New DataTable
            sqladp.Fill(_dtResult)

            Return _dtResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(sqladp) Then
                sqladp.Dispose()
                sqladp = Nothing
            End If
            'If Not IsNothing(_dtResult) Then
            '    _dtResult.Dispose()
            '    _dtResult = Nothing
            'End If
            _strSQL = ""
            _AptDat = ""

        End Try
    End Function
    Public Function GetPatientLocation(ByVal _PatientID As Int64, ByVal _VisitDate As Integer) As DataTable
        Dim _strSQL As String = ""
        Dim _dtResult As DataTable = Nothing
        Dim sqladp As SqlDataAdapter = Nothing
        Dim _AptDat As String = Nothing
        Try
            _AptDat = _VisitDate.ToString("yyyyMMdd")
            _strSQL = " SELECT TOP (1) AB_Location.sLocation,AB_Location.sAddressLine1 ,AB_Location.sAddressLine2,AB_Location.sCity ,AB_Location.sState ,AB_Location.sZIP ,AB_Location.sCountry ,'" & _VisitDate & "'  as StartDate , '" & _VisitDate & "' as EndDate FROM Patient inner join AB_Location on AB_Location.sLocation =patient.sLocation WHERE nPatientID = " & _PatientID & ""
            sqladp = New SqlDataAdapter(_strSQL, gloLibCCDGeneral.Connectionstring)
            _dtResult = New DataTable
            sqladp.Fill(_dtResult)

            Return _dtResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            If Not IsNothing(sqladp) Then
                sqladp.Dispose()
                sqladp = Nothing
            End If
            'If Not IsNothing(_dtResult) Then
            '    _dtResult.Dispose()
            '    _dtResult = Nothing
            'End If
            _strSQL = ""
            _AptDat = ""

        End Try
    End Function
    Public Function GetChiefComplaint(ByVal _PatientID As Int64) As String
        Dim _strSQL As String = ""
        Dim _ChiefComplaint As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "select sChiefComplaint from PatientChiefComplaint where nPatientID = " & _PatientID _
            & " and nVisitID =" & _VisitID

            cmd.CommandText = _strSQL

            _ChiefComplaint = cmd.ExecuteScalar
            If IsNothing(_ChiefComplaint) Then
                _ChiefComplaint = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _ChiefComplaint
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return ""
        Finally
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
            _strSQL = ""

        End Try
    End Function
    Public Function GetPatientClinicalInstruction(ByVal _PatientID As Int64, ByVal _VisitID As Int64) As PatientClinicalInstructionCol


        Dim cmd As SqlCommand = Nothing
        Dim cnn As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dtPatientClinicalInstruction As DataTable = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oPatientClinicalInstructionCol As PatientClinicalInstructionCol = Nothing
        Dim oPatientClinicalInstruction As PatientClinicalInstruction = Nothing

        Try
            cnn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientClinicalInstruction"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _PatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing



            da = New SqlDataAdapter(cmd)

            cnn.Open()
            dtPatientClinicalInstruction = New DataTable()
            da.Fill(dtPatientClinicalInstruction)
            cnn.Close()

            If dtPatientClinicalInstruction IsNot Nothing AndAlso dtPatientClinicalInstruction.Rows.Count > 0 Then

                oPatientClinicalInstructionCol = New PatientClinicalInstructionCol
                For i As Integer = 0 To dtPatientClinicalInstruction.Rows.Count - 1

                    If Convert.ToString(dtPatientClinicalInstruction.Rows(i)("sInstructionDtl")).Trim() <> "" Then

                        oPatientClinicalInstruction = New PatientClinicalInstruction
                        oPatientClinicalInstruction.Instructions = dtPatientClinicalInstruction.Rows(i)("sInstructionDtl")
                        oPatientClinicalInstructionCol.Add(oPatientClinicalInstruction)
                        'oPatientClinicalInstruction.Dispose()
                        oPatientClinicalInstruction = Nothing


                    End If
                Next

            End If

            Return oPatientClinicalInstructionCol

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return oPatientClinicalInstructionCol
        Finally


            'If Not IsNothing(oPatientClinicalInstruction) Then
            '    oPatientClinicalInstruction.Dispose()
            '    oPatientClinicalInstruction = Nothing
            'End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(dtPatientClinicalInstruction) Then
                dtPatientClinicalInstruction.Dispose()
                dtPatientClinicalInstruction = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
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
            If Not IsNothing(oPatientClinicalInstructionCol) Then
                oPatientClinicalInstructionCol.Dispose()
                oPatientClinicalInstructionCol = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientCareTeamInfo(ByVal nPatientID As Int64, Optional ByVal nExamID As Int64 = 0, Optional ByVal nOrderID As Int64 = 0) As PatientSupportCol
        Dim oPatientSupportCol As PatientSupportCol = Nothing
        Dim oPatientSupport As PatientSupport = Nothing
        Dim dtTeam As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Try
            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCD_GetCareTeamInfo"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@ExamID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64

            osqlpara.Value = nExamID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@OrderID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64

            osqlpara.Value = nOrderID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtTeam)

            If dtTeam IsNot Nothing AndAlso dtTeam.Rows.Count > 0 Then
                oPatientSupportCol = New PatientSupportCol
                For i As Integer = 0 To dtTeam.Rows.Count - 1
                    oPatientSupport = New PatientSupport
                    oPatientSupport.PersonName.Prefix = Convert.ToString(dtTeam.Rows(i)("sPrefix"))
                    oPatientSupport.PersonName.FirstName = Convert.ToString(dtTeam.Rows(i)("sFirstName"))
                    oPatientSupport.PersonName.MiddleName = Convert.ToString(dtTeam.Rows(i)("sMiddleName"))
                    oPatientSupport.PersonName.LastName = Convert.ToString(dtTeam.Rows(i)("sLastName"))
                    oPatientSupport.PersonName.Suffix = Convert.ToString(dtTeam.Rows(i)("sSuffix"))
                    oPatientSupport.PersonContactAddress.Street = Convert.ToString(dtTeam.Rows(i)("sAddressLine1"))
                    oPatientSupport.PersonContactAddress.AddressLine2 = Convert.ToString(dtTeam.Rows(i)("sAddressLine2"))
                    oPatientSupport.PersonContactAddress.City = Convert.ToString(dtTeam.Rows(i)("sCity"))
                    oPatientSupport.PersonContactAddress.State = Convert.ToString(dtTeam.Rows(i)("sState"))
                    oPatientSupport.PersonContactAddress.Zip = Convert.ToString(dtTeam.Rows(i)("sZIP"))
                    oPatientSupport.PersonContactPhone.Phone = Convert.ToString(dtTeam.Rows(i)("sPhone"))
                    ''As per QualityNet after first validation
                    oPatientSupport.PersonName.ProvTaXID = Convert.ToString(dtTeam.Rows(i)("sCompanyTaxID"))
                    oPatientSupport.PersonName.ProvNPI = Convert.ToString(dtTeam.Rows(i)("sNPI"))
                    oPatientSupport.PersonName.ContactFlag = Convert.ToString(dtTeam.Rows(i)("nContactFlag"))
                    oPatientSupport.PersonName.TaxonomyCode = Convert.ToString(dtTeam.Rows(i)("sTaxonomy"))
                    oPatientSupport.PersonContactAddress.Country = Convert.ToString(dtTeam.Rows(i)("sCountry"))
                    oPatientSupport.PersonContactAddress.County = Convert.ToString(dtTeam.Rows(i)("sCounty"))
                    oPatientSupportCol.Add(oPatientSupport)
                    oPatientSupport.Dispose()
                    oPatientSupport = Nothing
                Next
            End If
            Return oPatientSupportCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtTeam) Then
                dtTeam.Dispose()
                dtTeam = Nothing
            End If
            'If Not IsNothing(oPatientSupport) Then
            '    oPatientSupport.Dispose()
            '    oPatientSupport = Nothing
            'End If
            If Not IsNothing(oPatientSupportCol) Then
                oPatientSupportCol.Dispose()
                oPatientSupportCol = Nothing
            End If
        End Try

    End Function
    Public Function GetPatientEducation(ByVal nPatientID As Int64) As PatientEducationCol
        Dim dtPatientEducation As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oPatientEducationCol As PatientEducationCol = Nothing
        Dim oPatientEducation As PatientEducation = Nothing
        Try

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetPatientEducation"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtPatientEducation)

            If dtPatientEducation IsNot Nothing AndAlso dtPatientEducation.Rows.Count > 0 Then
                oPatientEducationCol = New PatientEducationCol
                For i As Integer = 0 To dtPatientEducation.Rows.Count - 1
                    oPatientEducation = New PatientEducation
                    oPatientEducation.TemplateName = dtPatientEducation.Rows(i)("sTemplateName")
                    oPatientEducation.VisitDate = dtPatientEducation.Rows(i)("VisitDate")
                    oPatientEducationCol.Add(oPatientEducation)
                    oPatientEducation = Nothing
                Next
            End If
            Return oPatientEducationCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtPatientEducation) Then
                dtPatientEducation.Dispose()
                dtPatientEducation = Nothing
            End If
            'If Not IsNothing(oPatientEducation) Then
            '    oPatientEducation.Dispose()
            '    oPatientEducation = Nothing
            'End If
            If Not IsNothing(oPatientEducationCol) Then
                oPatientEducationCol.Dispose()
                oPatientEducationCol = Nothing
            End If
            osqlpara = Nothing
        End Try

    End Function
    'Public Function GetPatientCarePlan(ByVal nPatientID As Int64) As PatientCarePlanCol
    '    Dim dtPatientCarePlan As New DataTable
    '    Dim da As SqlDataAdapter = Nothing
    '    Dim cmd As SqlCommand = Nothing
    '    Dim osqlpara As SqlParameter = Nothing
    '    Dim oPatientCarePlanCol As PatientCarePlanCol = Nothing
    '    Dim oPatientCarePlan As PatientCarePlan = Nothing
    '    Try

    '        cmd = New SqlCommand
    '        cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.CommandText = "gsp_CCDgetPatientCarePlan"

    '        osqlpara = New SqlParameter
    '        osqlpara.ParameterName = "@PatientID"
    '        osqlpara.Direction = ParameterDirection.Input
    '        osqlpara.DbType = DbType.Int64
    '        osqlpara.Value = nPatientID
    '        cmd.Parameters.Add(osqlpara)
    '        osqlpara = Nothing

    '        da = New SqlDataAdapter(cmd)
    '        da.Fill(dtPatientCarePlan)

    '        If dtPatientCarePlan IsNot Nothing AndAlso dtPatientCarePlan.Rows.Count > 0 Then
    '            oPatientCarePlanCol = New PatientCarePlanCol
    '            For i As Integer = 0 To dtPatientCarePlan.Rows.Count - 1
    '                oPatientCarePlan = New PatientCarePlan
    '                oPatientCarePlan.GoalCol = dtPatientCarePlan.Rows(i)("sGoal")
    '                oPatientCarePlan.Instructions = dtPatientCarePlan.Rows(i)("sInstruction")
    '                oPatientCarePlanCol.Add(oPatientCarePlan)
    '                oPatientCarePlan = Nothing
    '            Next
    '        End If
    '        Return oPatientCarePlanCol
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
    '        ex = Nothing
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(da) Then
    '            da.Dispose()
    '            da = Nothing
    '        End If
    '        osqlpara = Nothing
    '        If Not IsNothing(cmd) Then
    '            cmd.Parameters.Clear()
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '        If Not IsNothing(dtPatientCarePlan) Then
    '            dtPatientCarePlan.Dispose()
    '            dtPatientCarePlan = Nothing
    '        End If

    '        If Not IsNothing(osqlpara) Then
    '            osqlpara = Nothing
    '        End If

    '        If Not IsNothing(oPatientCarePlanCol) Then
    '            oPatientCarePlanCol.Dispose()
    '            oPatientCarePlanCol = Nothing
    '        End If
    '    End Try

    'End Function
    Public Function GetPatientFutureAppointment(ByVal nPatientID As Int64) As AppointmentCol
        Dim dtApt As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oAppointmentCol As AppointmentCol = Nothing
        Dim oAppointment As Appointment = Nothing
        Try

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetFutureAppointment"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtApt)

            If dtApt IsNot Nothing AndAlso dtApt.Rows.Count > 0 Then
                oAppointmentCol = New AppointmentCol
                For i As Integer = 0 To dtApt.Rows.Count - 1
                    oAppointment = New Appointment
                    oAppointment.StartDate = dtApt.Rows(i)("dtStartDate")
                    oAppointment.Provider = dtApt.Rows(i)("sASBaseDesc")
                    oAppointment.Location = dtApt.Rows(i)("sLocationName")
                    oAppointment.Duration = dtApt.Rows(i)("nDuration")
                    oAppointment.AppCity = dtApt.Rows(i)("sCity")
                    oAppointment.AppStreet = dtApt.Rows(i)("sAddressLine1")
                    oAppointment.AppState = dtApt.Rows(i)("sState")
                    oAppointment.AppZip = dtApt.Rows(i)("sZIP")
                    oAppointmentCol.Add(oAppointment)
                    oAppointment = Nothing
                Next
            End If
            Return oAppointmentCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtApt) Then
                dtApt.Dispose()
                dtApt = Nothing
            End If
            'If Not IsNothing(oAppointment) Then
            '    oAppointment.Dispose()
            '    oAppointment = Nothing
            'End If
            If Not IsNothing(oAppointmentCol) Then
                oAppointmentCol.Dispose()
                oAppointmentCol = Nothing
            End If
            osqlpara = Nothing
        End Try

    End Function
    Public Function GetReferralstoOtherProvider(ByVal nPatientID As Int64) As PatientSupportCol
        Dim dtReferrals As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oReferralsCol As PatientSupportCol = Nothing
        Dim oReferrals As PatientSupport = Nothing
        Try

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetReferralsToProvider"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtReferrals)

            If dtReferrals IsNot Nothing AndAlso dtReferrals.Rows.Count > 0 Then
                oReferralsCol = New PatientSupportCol
                For i As Integer = 0 To dtReferrals.Rows.Count - 1
                    oReferrals = New PatientSupport
                    oReferrals.PersonName.FirstName = dtReferrals.Rows(i)("sFirstName")
                    oReferrals.PersonName.LastName = dtReferrals.Rows(i)("sLastName")
                    oReferrals.PersonName.Prefix = dtReferrals.Rows(i)("sDegree")
                    oReferrals.PersonContactAddress.Street = dtReferrals.Rows(i)("sAddressLine1")
                    oReferrals.PersonContactAddress.City = dtReferrals.Rows(i)("sCity")
                    oReferrals.PersonContactAddress.State = dtReferrals.Rows(i)("sState")
                    oReferrals.PersonContactAddress.Zip = dtReferrals.Rows(i)("sZIP")
                    oReferrals.PersonContactPhone.Phone = dtReferrals.Rows(i)("sPhone")
                    oReferrals.Comments = Convert.ToString(dtReferrals.Rows(i)("Comments"))
                    oReferralsCol.Add(oReferrals)
                    oReferrals.Dispose()

                    oReferrals = Nothing
                Next
            End If
            Return oReferralsCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtReferrals) Then
                dtReferrals.Dispose()
                dtReferrals = Nothing
            End If
            'If Not IsNothing(oReferrals) Then
            '    oReferrals.Dispose()
            '    oReferrals = Nothing
            'End If
            If Not IsNothing(oReferralsCol) Then
                oReferralsCol.Dispose()
                oReferralsCol = Nothing
            End If
            osqlpara = Nothing
        End Try

    End Function
    Public Function GetFutureScheduleTests(ByVal nPatientID As Int64) As LabTestCol
        Dim dtTest As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oTestCol As LabTestCol = Nothing
        Dim oTest As LabTest = Nothing
        Try

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetFutureScheduleTest"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtTest)

            If dtTest IsNot Nothing AndAlso dtTest.Rows.Count > 0 Then
                oTestCol = New LabTestCol
                For i As Integer = 0 To dtTest.Rows.Count - 1
                    oTest = New LabTest
                    oTest.TestLOINCcode = dtTest.Rows(i)("LOINCCode")
                    oTest.TestName = dtTest.Rows(i)("Test")
                    oTest.ScheduledDateTime = dtTest.Rows(i)("ScheduledDate")
                    oTest.CPTCode = dtTest.Rows(i)("sCPTCode")
                    oTestCol.Add(oTest)
                    oTest = Nothing
                Next
            End If
            Return oTestCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtTest) Then
                dtTest.Dispose()
                dtTest = Nothing
            End If
            'If Not IsNothing(oTest) Then
            '    oTest.Dispose()
            '    oTest = Nothing
            'End If
            If Not IsNothing(oTestCol) Then
                oTestCol.Dispose()
                oTestCol = Nothing
            End If
            osqlpara = Nothing
        End Try
        Return oTestCol
    End Function
    Public Function GetDiagnosticPendingTests(ByVal nPatientID As Int64) As LabTestCol
        Dim dtTest As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter = Nothing
        Dim oTestCol As LabTestCol = Nothing
        Dim oTest As LabTest = Nothing
        Try

            cmd = New SqlCommand
            cmd.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetPendingTest"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(dtTest)

            If dtTest IsNot Nothing AndAlso dtTest.Rows.Count > 0 Then
                oTestCol = New LabTestCol
                For i As Integer = 0 To dtTest.Rows.Count - 1
                    oTest = New LabTest
                    oTest.TestLOINCcode = dtTest.Rows(i)("LOINCCode")
                    oTest.TestName = dtTest.Rows(i)("Test")
                    oTest.ScheduledDateTime = dtTest.Rows(i)("TestDate")
                    oTestCol.Add(oTest)
                    oTest = Nothing
                Next
            End If
            Return oTestCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            osqlpara = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtTest) Then
                dtTest.Dispose()
                dtTest = Nothing
            End If
            'If Not IsNothing(oTest) Then
            '    oTest.Dispose()
            '    oTest = Nothing
            'End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oTestCol) Then
                oTestCol.Dispose()
                oTestCol = Nothing
            End If
        End Try

    End Function
    Public Function GetFunctionalAndCognitiveStatus(ByVal _PatientID As Int64) As AllergiesCol
        'Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As AllergiesCol
        Dim _oStatus As Allergies
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Try
            _oCol = New AllergiesCol()
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetFunctionalStatus"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _PatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    _oStatus = New Allergies
                    _oStatus.ProductName = Convert.ToString(dt.Rows(i)("sHistoryItem"))
                    _oStatus.EffectiveStartTime = dt.Rows(i)("DOEAllergy")
                    _oStatus.Status = Convert.ToString(dt.Rows(i)("sStatus"))
                    _oStatus.ConceptID = Convert.ToString(dt.Rows(i)("sConceptID"))
                    _oCol.Add(_oStatus)
                    If Not IsNothing(_oStatus) Then
                        _oStatus.Dispose()
                        _oStatus = Nothing
                    End If
                Next
            End If

            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '_strSQL = ""
        End Try
    End Function
    Public Function GetPatientFunctionalStatus(ByVal _PatientID As Int64) As String
        Dim _strSQL As String = ""
        Dim _FunctionalStatus As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "select top 1 sComments from ROS inner join Visits " _
            & " on ROS.nVisitID = Visits.nVisitID where ROS.nPatientID =" & _PatientID & " and " _
            & " sROSCategory ='Functional and Cognitive Ability' and sROSItem ='Functional Ability' order by dtVisitDate desc"

            cmd.CommandText = _strSQL

            _FunctionalStatus = cmd.ExecuteScalar
            If IsNothing(_FunctionalStatus) Then
                _FunctionalStatus = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _FunctionalStatus
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
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
            _strSQL = ""
        End Try
    End Function
    Public Function GetPatientCognitiveStatus(ByVal _PatientID As Int64) As String
        Dim _strSQL As String = ""
        Dim _CognitiveStatus As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            _strSQL = "select top 1 sComments from ROS inner join Visits " _
            & " on ROS.nVisitID = Visits.nVisitID where ROS.nPatientID =" & _PatientID & " and " _
            & " sROSCategory ='Functional and Cognitive Ability' and sROSItem ='Cognitive Ability' order by dtVisitDate desc"

            cmd.CommandText = _strSQL

            _CognitiveStatus = cmd.ExecuteScalar
            If IsNothing(_CognitiveStatus) Then
                _CognitiveStatus = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _CognitiveStatus
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
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
            _strSQL = ""
        End Try
    End Function
    Public Function GetReasonforReferral(ByVal _OrderID As Int64, ByVal _PatientID As Int64) As String
        Dim _strSQL As String = ""
        Dim _ReasonforReferral As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDA_GetReasonforReferral"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@OrderID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _OrderID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _PatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing



            'osqlpara = New SqlParameter
            'osqlpara.ParameterName = "@StartDate"
            'osqlpara.Direction = ParameterDirection.Input
            'osqlpara.DbType = DbType.String
            'osqlpara.Value = _FromDate

            'cmd.Parameters.Add(osqlpara)
            'osqlpara = Nothing

            'osqlpara = New SqlParameter
            'osqlpara.ParameterName = "@EndDate"
            'osqlpara.Direction = ParameterDirection.Input
            'osqlpara.DbType = DbType.String
            'osqlpara.Value = _ToDate

            'cmd.Parameters.Add(osqlpara)
            'osqlpara = Nothing



            'If _OrderID <> 0 Then
            '    _strSQL = "DECLARE @listStr VARCHAR(MAX) SELECT @listStr = COALESCE(@listStr+' , ' , '') + labotd_TestName + ' Scheduled on : ' + ISNULL(CONVERT(varchar, Lab_Order_TestDtl.labotd_TestScheduledDateTime, 101), '') " _
            '               & "  FROM Lab_Order_TestDtl where (labotd_OrderID = " & _OrderID & " and labotd_TestScheduledDateTime is not null ) SELECT @listStr"
            'Else
            '    _strSQL = " DECLARE @listStr VARCHAR(MAX) SELECT @listStr = COALESCE(@listStr+' , ' , '') + labotd_TestName + ' Scheduled on : ' + ISNULL(CONVERT(varchar, Lab_Order_TestDtl.labotd_TestScheduledDateTime, 101), '') " _
            '    & "  FROM Lab_Order_TestDtl where labotd_OrderID = ( select top 1 lo.labom_OrderID  from Lab_Order_MST lo inner join Visits  v" _
            '    & " ON lo.labom_VisitID = v.nVisitID and (labom_PatientID =" & _PatientID & ") order by v.dtVisitDate desc ) and labotd_TestScheduledDateTime is not null" _
            '    & " SELECT @listStr"
            'End If

            'cmd.CommandText = _strSQL

            _ReasonforReferral = Convert.ToString(cmd.ExecuteScalar)
            If IsNothing(_ReasonforReferral) Then
                _ReasonforReferral = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _ReasonforReferral
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = ""
        End Try
    End Function
    Public Function GetAssessments(ByVal _PatientID As Int64) As String
        Dim _strSQL As String = ""
        Dim _Assessment As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDA_GetAssessments"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _PatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            _Assessment = Convert.ToString(cmd.ExecuteScalar)
            If IsNothing(_Assessment) Then
                _Assessment = ""
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _Assessment
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = ""
        End Try
    End Function
    Public Function GetFrequencyDetails(ByVal _FrequencyDesc As String) As DataTable
        Dim _strSQL As String = ""
        Dim _dtResult As DataTable = Nothing
        Dim sqladp As SqlDataAdapter = Nothing
        Try

            _strSQL = " SELECT TOP (1) isnull(sAbbreviation,'') as sAbbreviation,isnull(sDescription,'') as sDescription,isnull(sXsiType,'') as sXsiType,binstitutionSpecified,isnull(sValue,'') as sValue,isnull(sUnit,'') as sUnit FROM CCDA_FrequencyMapping where sDescription  = '" & _FrequencyDesc.Trim & "'"
            sqladp = New SqlDataAdapter(_strSQL, gloLibCCDGeneral.Connectionstring)
            _dtResult = New DataTable
            sqladp.Fill(_dtResult)

            Return _dtResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(sqladp) Then
                sqladp.Dispose()
                sqladp = Nothing
            End If
            _strSQL = ""
            'If Not IsNothing(_dtResult) Then
            '    _dtResult.Dispose()
            '    _dtResult = Nothing
            'End If
        End Try
    End Function
    Public Function GetFrequencyDescription(ByVal _Period As String, ByVal _Unit As String) As String
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim con As SqlConnection = Nothing
        Try
            con = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            _strSQL = " SELECT TOP (1) isnull(sDescription,'') as sDescription FROM CCDA_FrequencyMapping where sValue='" & _Period & "' and sUnit  = '" & _Unit & "' ORDER BY ISNULL(nPrefferedDesc,0)"
            cmd = New SqlCommand(_strSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim _result As Object = cmd.ExecuteScalar()
            If Not IsNothing(_result) Then
                Return _result.ToString()
            End If

            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return ""
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = ""

        End Try

    End Function
    Public Function GetPatientFamilyHistory(ByVal npatientid As Int64) As gloCCDLibrary.FamilyHistoryCol
        'Dim strSQl As String = ""
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oFamilyHistory As gloCCDLibrary.FamilyHistory = Nothing
        Dim oFamilyHistoryCol As gloCCDLibrary.FamilyHistoryCol = Nothing
        Dim osqlpara As SqlParameter = Nothing

        Try
            'oFamilyHistory = New gloCCDLibrary.FamilyHistory
            oFamilyHistoryCol = New gloCCDLibrary.FamilyHistoryCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDAPatientFamilyHistory"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate   ''from date and todate passed for certification

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            cnn.Open()

            oDataReader = cmd.ExecuteReader()



            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oFamilyHistory = New FamilyHistory
                    oFamilyHistory.FmlyHxDescription = oDataReader.Item("sHistoryItem")
                    oFamilyHistory.FmlyHxQualifiers = ""
                    oFamilyHistory.FmlyHxComments = oDataReader.Item("sComments")
                    oFamilyHistory.FmlyHxConceptID = oDataReader.Item("sConceptID")
                    oFamilyHistory.FmlyHxRelation = Convert.ToString(oDataReader.Item("sRelation"))
                    oFamilyHistory.FmlyHxRelConceptID = Convert.ToString(oDataReader.Item("RelationConceptId"))
                    oFamilyHistory.FmlyHxHistoryId = Convert.ToString(oDataReader.Item("nHistoryId"))
                    If IsDBNull(oDataReader.Item("dtVisitDate")) Then
                        oFamilyHistory.FmlyHxDateReported = ""
                    Else
                        oFamilyHistory.FmlyHxDateReported = oDataReader.Item("dtVisitDate").ToString() ''''''Insurance company start date
                    End If

                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                        End If
                        oFamilyHistory.FmlyHxStatus = "Active"
                    End If
                    If IsDBNull(oDataReader.Item("dtOnsetDate")) Then
                        oFamilyHistory.OccurDate = ""
                    Else
                        oFamilyHistory.OccurDate = oDataReader.Item("dtOnsetDate").ToString()
                    End If
                    oFamilyHistory.FmlyMemberCode = oDataReader.Item("MemberCode").ToString()
                    If IsNothing(oFamilyHistory) = False Then
                        oFamilyHistoryCol.Add(oFamilyHistory)
                        '           oFamilyHistory.Dispose()
                        oFamilyHistory = Nothing
                    End If


                End If
            End While
            oFamilyHistory = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If

            Return oFamilyHistoryCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
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
            oDataReader = Nothing
            oFamilyHistory = Nothing
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oFamilyHistoryCol) Then
                oFamilyHistoryCol.Dispose()
                oFamilyHistoryCol = Nothing
            End If
        End Try
    End Function
    Public Function GetAuthorInfo(ByVal npatientid As Int64, ByVal orderid As Int64) As gloCCDLibrary.PatientAuthor
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oAuthor As gloCCDLibrary.PatientAuthor = Nothing
        Dim osqlpara As SqlParameter = Nothing

        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDAgetAuthor"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@OrderID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = orderid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oAuthor = New gloCCDLibrary.PatientAuthor
                    oAuthor.PersonName.FirstName = oDataReader.Item("ProvFirstName")
                    oAuthor.PersonName.MiddleName = oDataReader.Item("ProvMiddleName")
                    oAuthor.PersonName.LastName = oDataReader.Item("ProvLastName")
                    oAuthor.PersonName.Suffix = oDataReader.Item("ProvSuffix")
                    oAuthor.PersonName.Prefix = oDataReader.Item("ProvPrefix").ToString()
                    oAuthor.PersonName.Code = oDataReader.Item("ProvNPI").ToString()
                    oAuthor.PersonContactAddress.Street = oDataReader.Item("ProvAddress").ToString() + If(oDataReader.Item("ProvStreet").ToString() <> "", ",", "") + oDataReader.Item("ProvStreet").ToString()
                    oAuthor.PersonContactAddress.City = oDataReader.Item("ProvCity").ToString()
                    oAuthor.PersonContactAddress.State = oDataReader.Item("ProvState").ToString()
                    oAuthor.PersonContactAddress.Zip = oDataReader.Item("ProvZIP").ToString()
                    oAuthor.Phone = oDataReader.Item("ProvPhNo").ToString()
                    '''' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
                    oAuthor.PersonContactAddress.Country = oDataReader.Item("ProvCountry").ToString()
                    oAuthor.PersonName.TaxonomyCode = oDataReader.Item("ProvTaxonomy").ToString()
                    oAuthor.PersonName.TaxonomyDesc = oDataReader.Item("ProvTaxonomyDesc").ToString()
                    '''' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End
                End If
            End While
            oDataReader.Close()
            cnn.Close()

            Return oAuthor

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
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
            If Not IsNothing(oAuthor) Then
                oAuthor.Dispose()
                oAuthor = Nothing
            End If
        End Try
    End Function
    Public Function GetInformationRecipient(ByVal npatientid As Int64, ByVal orderid As Int64) As gloCCDLibrary.InfoRecipent
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oRecipient As gloCCDLibrary.InfoRecipent = Nothing
        Dim osqlpara As SqlParameter = Nothing

        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CDAInformationRecipient"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@OrderID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = orderid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oRecipient = New gloCCDLibrary.InfoRecipent
                    oRecipient.FirstName = oDataReader.Item("ProvFirstName")
                    oRecipient.MiddleName = oDataReader.Item("ProvMiddleName")
                    oRecipient.LastName = oDataReader.Item("ProvLastName")
                    oRecipient.Suffix = oDataReader.Item("ProvSuffix")
                    oRecipient.Prefix = oDataReader.Item("ProvPrefix").ToString()

                    '''' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End
                End If
            End While
            oDataReader.Close()
            cnn.Close()

            Return oRecipient

        Catch ex As gloCCDException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
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
            If Not IsNothing(oRecipient) Then
                oRecipient.Dispose()
                oRecipient = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientMentalStatus(ByVal _patientid As Int64) As AllergiesCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As AllergiesCol
        Dim _oStatus As Allergies
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Try
            _oCol = New AllergiesCol()
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetMentalStatus"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate   ''from date and todate passed for certification

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    _oStatus = New Allergies
                    _oStatus.ProductName = Convert.ToString(dt.Rows(i)("sHistoryItem"))
                    _oStatus.EffectiveStartTime = dt.Rows(i)("DOEAllergy")
                    _oStatus.Status = Convert.ToString(dt.Rows(i)("sStatus"))
                    _oStatus.ConceptID = Convert.ToString(dt.Rows(i)("sConceptID"))
                    _oCol.Add(_oStatus)
                    If Not IsNothing(_oStatus) Then
                        _oStatus.Dispose()
                        _oStatus = Nothing
                    End If
                Next
            End If

            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '_strSQL = ""
        End Try
    End Function
    Public Function getPatientImplantableDevice(ByVal _patientid As Int64) As gloPatientImplantCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As gloPatientImplantCol
        Dim _oImplant As Implant
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Try
            _oCol = New gloPatientImplantCol()
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDgetImplantableDevice"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Dim implantlist As List(Of Implant) = New List(Of Implant)()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    _oImplant = New Implant
                    _oImplant.UDI = Convert.ToString(dt.Rows(i)("sUDI"))
                    If Not IsDBNull(dt.Rows(i)("dtImplant_Date")) AndAlso Convert.ToString(dt.Rows(i)("dtImplant_Date")) <> "" Then
                        _oImplant.ImplantDate = Convert.ToString(dt.Rows(i)("dtImplant_Date")).Replace("12:00:00 AM", "")
                    Else
                        _oImplant.ImplantDate = ""
                    End If


                    _oImplant.DeviceCode = Convert.ToString(dt.Rows(i)("sConceptID"))
                    _oImplant.Device_Description = Convert.ToString(dt.Rows(i)("sDescription"))
                    _oImplant.DeviceID = Convert.ToString(dt.Rows(i)("nDevicelist_Id"))
                    _oImplant.ISUDI = Convert.ToBoolean(dt.Rows(i)("IsUDI"))
                    _oImplant.ISBloodContainer = Convert.ToBoolean(dt.Rows(i)("IsBloodContainer"))
                    _oImplant.IssueingAgency = Convert.ToString(dt.Rows(i)("sIssuingAgency"))
                    '_oCol.Add(_oImplant)

                    implantlist.Add(_oImplant)
                    '_oCol.implants.Add(_oImplant)
                    If Not IsNothing(_oImplant) Then
                        _oImplant.Dispose()
                        _oImplant = Nothing
                    End If
                Next
            End If
            _oCol.implants = implantlist
            If Not IsNothing(implantlist) Then
                implantlist = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '_strSQL = ""
        End Try
    End Function
    Public Function getCDAHealthConcerns(ByVal _patientid As Int64) As HealthConcernCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As HealthConcernCol
        Dim _Concern As HealthConcern
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        'Dim _PatientCareplan As PatientCarePlan
        'Dim _PatientCarePlanCol As PatientCarePlanCol
        Try
            _oCol = New HealthConcernCol()
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getCDAHealthConcerns"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Dim ConcernList As List(Of HealthConcern) = New List(Of HealthConcern)()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    _Concern = New HealthConcern
                    _Concern.HealthConcernID = Convert.ToString(dt.Rows(i)("nHealthConcernID"))
                    _Concern.HealthConcernName = Convert.ToString(dt.Rows(i)("sHealthConcernName"))
                    _Concern.HealthConcernSNOMEDID = dt.Rows(i)("sHealthConcernSnomedCode")
                    _Concern.HealthConcernSnomedDesc = Convert.ToString(dt.Rows(i)("sHealthConcernSnomedDescription"))
                    _Concern.HealthConcernAuthor = Convert.ToString(dt.Rows(i)("sHealthConcernAuthor"))
                    _Concern.HealthConcernStatus = Convert.ToString(dt.Rows(i)("sHealthConcernStatus"))
                    _Concern.ConcernStartDate = Convert.ToString(dt.Rows(i)("dtHealthConcernStartDate"))
                    _Concern.ConcernEndDate = Convert.ToString(dt.Rows(i)("dtHealthConcernEndDate"))
                    _Concern.AssociateConcernID = Convert.ToString(dt.Rows(i)("nAssociatedConcernId"))
                    _Concern.HealthStatusDesc = Convert.ToString(dt.Rows(i)("HealthStatusDescription"))
                    _Concern.HealthConcernNarrative = Convert.ToString(dt.Rows(i)("HealthConcernNotes"))
                    ConcernList.Add(_Concern)
                    '_oCol.implants.Add(_oImplant)
                    If Not IsNothing(_Concern) Then
                        _Concern.Dispose()
                        _Concern = Nothing
                    End If
                Next
            End If

            _oCol.HealthConcernList = ConcernList
            '_PatientCareplan = New PatientCarePlan
            '_PatientCarePlanCol = New PatientCarePlanCol
            '_PatientCareplan.HealthConcernCol = _oCol
            '_PatientCarePlanCol.Add(_PatientCareplan)
            If Not IsNothing(ConcernList) Then
                ConcernList = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '_strSQL = ""
        End Try
    End Function
    Private Function getCDAGoals(ByVal _patientid As Int64) As GoalsCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As GoalsCol
        Dim _Goal As Goal
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable

        Try
            _oCol = New GoalsCol()
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getCDAGoals"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Dim _GoalList As List(Of Goal) = New List(Of Goal)()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    _Goal = New Goal
                    _Goal.GoalId = Convert.ToString(dt.Rows(i)("nGoalId"))
                    _Goal.GoalName = Convert.ToString(dt.Rows(i)("sGoalName"))
                    _Goal.GoalLoincCode = dt.Rows(i)("sGoalLOINCCode")
                    _Goal.GoalLoincDesc = Convert.ToString(dt.Rows(i)("sGoalLOINCDesc"))
                    _Goal.GoalNarrative = Convert.ToString(dt.Rows(i)("sGoalNarrative"))
                    _Goal.GoalValue = Convert.ToString(dt.Rows(i)("sGoalvalue"))
                    _Goal.GoalUnit = Convert.ToString(dt.Rows(i)("sGoalUnit"))
                    _Goal.GoalDate = Convert.ToString(dt.Rows(i)("dtGoalDate"))
                    _Goal.GoalAuthor = dt.Rows(i)("sGoalAuthor")
                    _Goal.AssociateId = dt.Rows(i)("nAssociationID")
                    _Goal.AssociateType = dt.Rows(i)("nAssociationType")
                    _Goal.GoalAuthorFirstName = dt.Rows(i)("sFirstName")
                    _Goal.GoalAuthorMiddleName = dt.Rows(i)("sMiddleName")
                    _Goal.GoalAuthorLastName = dt.Rows(i)("sLastName")

                    _GoalList.Add(_Goal)
                    '_oCol.implants.Add(_oImplant)
                    If Not IsNothing(_Goal) Then
                        _Goal.Dispose()
                        _Goal = Nothing
                    End If
                Next
            End If

            _oCol.GoalsList = _GoalList
            '_PatientCareplan = New PatientCarePlan
            '_PatientCarePlanCol = New PatientCarePlanCol
            '_PatientCareplan.HealthConcernCol = _oCol
            '_PatientCarePlanCol.Add(_PatientCareplan)
            If Not IsNothing(_GoalList) Then
                _GoalList = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            '_strSQL = ""
        End Try
    End Function
    Public Function SaveExportedCDA(ByVal _PatientID As Int64, ByVal sCCDFilePath As String, ByVal _Notes As String, Optional ByVal _sFileCreatedFrom As String = Nothing, Optional ByVal _ISGeneratedAtPatientRequest As Boolean = False, Optional ByVal _IsOwnByPastExam As Boolean = False, Optional ByVal _DateOfservice As String = "1/1/2000", Optional ByVal FileType As String = "CDA", Optional ByVal nCDAFileType As Int32 = 0, Optional ByVal nExamId As Int64 = 0, Optional ByVal bIsSendToPortal As Boolean = 0, Optional ByVal nOrderId As Int64 = 0, Optional ByVal bIsSendToAPI As Boolean = 0, Optional ByVal bIsFileViewedOnPortal As Boolean = 0, Optional ByVal bIsFileViewedOnAPI As Boolean = 0) As Int64
        Dim nCCDID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(gloLibCCDGeneral.Connectionstring)
        Dim sqlParam As SqlParameter = Nothing
        Dim _fileHashValue As String = ""
        Dim _fileHashAlgorithmType As String = ""
        Dim XMLarrByte As Byte() = Nothing

        Try
            XMLarrByte = ConvertFiletoBinary(sCCDFilePath)


            cmd = New SqlCommand("CDA_ExportedFiles", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nCCDID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _PatientID



            sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 100)
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
            sqlParam.Value = nExamId

            sqlParam = cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nOrderId


            sqlParam = cmd.Parameters.Add("@nCDAFileType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nCDAFileType

            sqlParam = cmd.Parameters.Add("@bIsSendToPortal", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bIsSendToPortal

            sqlParam = cmd.Parameters.Add("@bIsSendToAPI", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bIsSendToAPI

            sqlParam = cmd.Parameters.Add("@bIsFileViewedOnPortal", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bIsFileViewedOnPortal

            sqlParam = cmd.Parameters.Add("@bIsFileViewedOnAPI", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bIsFileViewedOnAPI

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.ExecuteNonQuery()
            nCCDID = Convert.ToInt64(cmd.Parameters("@nCCDID").Value)
            Return nCCDID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString)
        Finally
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            _fileHashValue = Nothing
            _fileHashAlgorithmType = Nothing
            XMLarrByte = Nothing
        End Try
    End Function
    Private Function getCDAInterventions(ByVal _patientid As Int64) As InterventionCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As InterventionCol = Nothing
        Dim _Intervention As Intervention
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim dtintervention As DataTable = Nothing
        Dim dtplannedIntervention As DataTable = Nothing

        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getPatientInterventions"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Dim sNutrition As String = String.Empty
            Dim _InterventionList As List(Of Intervention) = New List(Of Intervention)()
            Dim _PlannedInterventionList As List(Of Intervention) = New List(Of Intervention)()
            _oCol = New InterventionCol()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                Dim dr() As DataRow = dt.Select("sInterventionType='Actual'")
                If dr.Length > 0 Then

                    dtintervention = dr.CopyToDataTable()
                    dr = Nothing
                    If Not IsNothing(dtintervention) AndAlso dtintervention.Rows.Count > 0 Then
                        For i As Integer = 0 To dtintervention.Rows.Count - 1
                            _Intervention = New Intervention
                            _Intervention.InterventionId = Convert.ToString(dtintervention.Rows(i)("nInterventionId"))
                            _Intervention.InterventionName = Convert.ToString(dtintervention.Rows(i)("sInterventionName"))
                            _Intervention.InterventionType = dtintervention.Rows(i)("sInterventionType")
                            _Intervention.InterventionNotes = Convert.ToString(dtintervention.Rows(i)("sInterventionNotes"))
                            _Intervention.InterventionrecordDate = Convert.ToString(dtintervention.Rows(i)("dtInterventionDate"))
                            _Intervention.AssociateId = Convert.ToString(dtintervention.Rows(i)("nAssociationID"))
                            _Intervention.AssociateType = Convert.ToString(dtintervention.Rows(i)("nAssociationType"))
                            _Intervention.RelativeId = Convert.ToString(dtintervention.Rows(i)("nRelatedId"))
                            _Intervention.PlanOfTreatmentId = 0

                            sNutrition = Convert.ToString(dtintervention.Rows(i)("snutritionRecomendation"))
                            _Intervention.NutritionRecomendation = ""
                            _Intervention.NutritionRecomendationDesc = ""
                            Dim sNutritionRecom As String() = sNutrition.Split("#")
                            If sNutritionRecom.Length > 0 Then
                                _Intervention.NutritionRecomendation = sNutritionRecom(0)
                                If sNutritionRecom.Length > 1 Then
                                    _Intervention.NutritionRecomendationDesc = sNutritionRecom(1)
                                End If
                            End If
                            _Intervention.NutritionInstruction = Convert.ToString(dtintervention.Rows(i)("NutritionInstruction"))
                            _InterventionList.Add(_Intervention)
                            If Not IsNothing(_Intervention) Then
                                _Intervention.Dispose()
                                _Intervention = Nothing
                            End If
                        Next
                    End If

                End If
                dr = dt.Select("sInterventionType='Planned'")
                If dr.Length > 0 Then

                    dtplannedIntervention = dr.CopyToDataTable()
                    dr = Nothing
                    'Dim ndcList As New List(Of String)

                    If Not IsNothing(dtplannedIntervention) AndAlso dtplannedIntervention.Rows.Count > 0 Then
                        'Using dtNDCs As DataTable = New DataView(dtplannedIntervention).ToTable(False, New String() {"PlannedMedicationCode"})
                        '    If Not IsNothing(dtNDCs) Then
                        '        If Not IsNothing(dtNDCs) Or dtNDCs.Rows.Count > 0 Then
                        '            ndcList = dtNDCs.AsEnumerable().[Select](Of String)(Function(q) Convert.ToString(q("PlannedMedicationCode"))).ToList()
                        '        End If
                        '    End If
                        'End Using
                        'Dim oDrugInfo As New gloGlobal.DIB.ResultSetRxnorm
                        'Try
                        '    Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                        '        oDrugInfo = oDIBGSHelper.GetRxnormGenericName(ndcList)
                        '    End Using
                        'Catch ex As Exception
                        '    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                        'End Try
                        For i As Integer = 0 To dtplannedIntervention.Rows.Count - 1
                            _Intervention = New Intervention
                            _Intervention.InterventionId = Convert.ToString(dtplannedIntervention.Rows(i)("nInterventionId"))
                            _Intervention.InterventionName = Convert.ToString(dtplannedIntervention.Rows(i)("sInterventionName"))
                            _Intervention.InterventionType = dtplannedIntervention.Rows(i)("sInterventionType")
                            _Intervention.InterventionNotes = Convert.ToString(dtplannedIntervention.Rows(i)("sInterventionNotes"))
                            _Intervention.InterventionrecordDate = Convert.ToString(dtplannedIntervention.Rows(i)("dtInterventionDate"))
                            _Intervention.AssociateId = Convert.ToString(dtplannedIntervention.Rows(i)("nAssociationID"))
                            _Intervention.AssociateType = Convert.ToString(dtplannedIntervention.Rows(i)("nAssociationType"))
                            _Intervention.PlanOfTreatmentId = Convert.ToString(dtplannedIntervention.Rows(i)("nPlanOfTreatmentID"))
                            _Intervention.RelativeId = Convert.ToString(dtplannedIntervention.Rows(i)("nRelatedId"))

                            sNutrition = Convert.ToString(dtplannedIntervention.Rows(i)("snutritionRecomendation"))
                            _Intervention.NutritionRecomendation = ""
                            _Intervention.NutritionRecomendationDesc = ""
                            Dim sNutritionRecom As String() = sNutrition.Split("#")
                            If sNutritionRecom.Length > 0 Then
                                _Intervention.NutritionRecomendation = sNutritionRecom(0)
                                If sNutritionRecom.Length > 1 Then
                                    _Intervention.NutritionRecomendationDesc = sNutritionRecom(1)
                                End If
                            End If
                            _Intervention.NutritionInstruction = Convert.ToString(dtplannedIntervention.Rows(i)("NutritionInstruction"))
                            '_Intervention.PlannedLOINCCode = Convert.ToString(dtplannedIntervention.Rows(i)("ObsCode"))
                            '_Intervention.PlannedObsDate = Convert.ToString(dtplannedIntervention.Rows(i)("PlannedObsDate"))
                            '_Intervention.PlannedEncounterCode = Convert.ToString(dtplannedIntervention.Rows(i)("EncounterCode"))
                            '_Intervention.PlannedEncounterName = Convert.ToString(dtplannedIntervention.Rows(i)("EncounterName"))
                            '_Intervention.PlannedEncounterDate = Convert.ToString(dtplannedIntervention.Rows(i)("PlannedEncounterDate"))
                            '_Intervention.PlannedMedicationCode = Convert.ToString(dtplannedIntervention.Rows(i)("PlannedMedicationCode"))
                            '_Intervention.PlannedMedicatinName = Convert.ToString(dtplannedIntervention.Rows(i)("PlannedMedicationName"))
                            '_Intervention.PlannedmedicationDate = Convert.ToString(dtplannedIntervention.Rows(i)("PlannedStartDate"))
                            '_Intervention.PlannedLOINCName = Convert.ToString(dtplannedIntervention.Rows(i)("TestName"))
                            'If IsNothing(oDrugInfo) = False Then
                            '    If IsNothing(oDrugInfo) = False Then
                            '        If oDrugInfo.lgrx.Count > 0 Then
                            '            For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                            '                If item.Ndc = _Intervention.PlannedMedicationCode Then
                            '                    _Intervention.PlannedRxNormCode = item.Rxnorm
                            '                    Exit For
                            '                End If
                            '            Next
                            '        End If
                            '    End If
                            'End If
                            _PlannedInterventionList.Add(_Intervention)
                            If Not IsNothing(_Intervention) Then
                                _Intervention.Dispose()
                                _Intervention = Nothing
                            End If
                        Next
                    End If
                End If
            End If

            _oCol.InterventionList = _InterventionList
            _oCol.PlannedIntervention = _PlannedInterventionList
            If Not IsNothing(_InterventionList) Then
                _InterventionList = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(dtintervention) Then
                dtintervention.Dispose()
            End If
            If Not IsNothing(dtplannedIntervention) Then
                dtplannedIntervention.Dispose()
            End If
        End Try
    End Function

    Public Function getCDAPlannedInterventionsDetail(ByVal _nPlanOfTreatmentID As Int64) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable

        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getPatientInterventions_PlannedDetail"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPlanOfTreatmentID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _nPlanOfTreatmentID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Return dt
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If
        End Try
    End Function

    Private Function getCDAPlanOfTreatment(ByVal _patientid As Int64, Optional ByVal _LabtestCol As LabTestCol = Nothing) As PlannedModuleCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As PlannedModuleCol = Nothing
        Dim _PlannedCare As PlannedModule
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable

        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getPatientPlanOfTreatment"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)

            Dim _PlannedModuleList As List(Of PlannedModule) = New List(Of PlannedModule)()
            _oCol = New PlannedModuleCol()  ''added for bugid 110133
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        _PlannedCare = New PlannedModule
                        _PlannedCare.PlannedCode = Convert.ToString(dt.Rows(i)("PlannedCode"))
                        _PlannedCare.PlannedName = dt.Rows(i)("PlannedName")
                        _PlannedCare.EffectivePlannedDate = Convert.ToString(dt.Rows(i)("StartDate"))
                        _PlannedCare.Details = Convert.ToString(dt.Rows(i)("Details"))
                        _PlannedCare.PlannedModuletype = Convert.ToString(dt.Rows(i)("PlannedCareType"))
                        _PlannedCare.PlannedStatus = Convert.ToString(dt.Rows(i)("PlannedStatus"))
                        _PlannedCare.PlannedEndDate = Convert.ToString(dt.Rows(i)("EndDate"))
                        If _PlannedCare.PlannedModuletype.Equals("Medication") Then
                            Dim ndcList As New List(Of String)
                            Dim dr As DataRow() = Nothing
                            dr = dt.Select("PlannedCareType='Medication'")
                            If Not IsNothing(dr) Then
                                If dr.Length > 0 Then
                                    Dim dtMedNDC As DataTable = Nothing
                                    dtMedNDC = New DataTable
                                    dtMedNDC = dr.CopyToDataTable()
                                    Using dtNDCs As DataTable = New DataView(dtMedNDC).ToTable(False, New String() {"PlannedCode"})
                                        If Not IsNothing(dtNDCs) Then
                                            If Not IsNothing(dtNDCs) Or dtNDCs.Rows.Count > 0 Then
                                                ndcList = dtNDCs.AsEnumerable().[Select](Of String)(Function(q) Convert.ToString(q("PlannedCode"))).ToList()
                                            End If
                                        End If
                                    End Using
                                    Dim oDrugInfo As New gloGlobal.DIB.ResultSetRxnorm
                                    Try

                                        '09-Nov-17 Aniket: Resolving Bug #110226: API : CDA > RnNorm Code and Generic Name is not getting display in Medication Section 
                                        If IsNothing(gloLibCCDGeneral.sDIBServiceURL) = True OrElse gloLibCCDGeneral.sDIBServiceURL = "" Then
                                            gloLibCCDGeneral.sDIBServiceURL = GetDIBSettingsURL()
                                        End If

                                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                            oDrugInfo = oDIBGSHelper.GetRxnormGenericName(ndcList)
                                        End Using
                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                                    End Try

                                    If IsNothing(oDrugInfo) = False Then
                                        If IsNothing(oDrugInfo) = False Then
                                            If oDrugInfo.lgrx.Count > 0 Then
                                                For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                                    If item.Ndc = _PlannedCare.PlannedCode Then
                                                        _PlannedCare.PlannedRxNormcode = item.Rxnorm
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                End If

                            End If


                        End If


                        _PlannedModuleList.Add(_PlannedCare)
                        If Not IsNothing(_PlannedCare) Then
                            _PlannedCare.Dispose()
                            _PlannedCare = Nothing
                        End If
                    Next
                End If

            End If
            Dim _labtest As LabTest = Nothing
            If Not IsNothing(_LabtestCol) AndAlso _LabtestCol.Count > 0 Then
                For index As Integer = 0 To _LabtestCol.Count - 1
                    _labtest = _LabtestCol.Item(index)
                    _PlannedCare = New PlannedModule
                    _PlannedCare.PlannedCode = _labtest.TestLOINCcode
                    _PlannedCare.PlannedName = _labtest.TestName
                    _PlannedCare.Details = "Pending Lab Test"
                    _PlannedCare.EffectivePlannedDate = _labtest.ScheduledDateTime
                    _PlannedCare.PlannedModuletype = "Order"
                    _PlannedCare.PlannedStatus = "active"
                    _PlannedModuleList.Add(_PlannedCare)
                    If Not IsNothing(_PlannedCare) Then
                        _PlannedCare.Dispose()
                        _PlannedCare = Nothing
                    End If
                Next

            End If

            _oCol.PlannedModuleList = _PlannedModuleList

            If Not IsNothing(_PlannedModuleList) Then
                _PlannedModuleList = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Function getCDAOutcome(ByVal _patientid As Int64) As OutcomeCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Dim _oCol As OutcomeCol = Nothing
        Dim _Outcome As Outcome
        Dim sdap As SqlDataAdapter = Nothing
        Dim dt As New DataTable

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_getCDAOutcomes"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _patientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            sdap = New SqlDataAdapter(cmd)
            sdap.Fill(dt)
            Dim _OutcomeList As List(Of Outcome) = New List(Of Outcome)()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                _oCol = New OutcomeCol()
                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        _Outcome = New Outcome
                        _Outcome.OutcomeId = Convert.ToString(dt.Rows(i)("nOutcomeID"))
                        _Outcome.OutcomeName = Convert.ToString(dt.Rows(i)("OutcomeName"))
                        _Outcome.Outcomestatus = Convert.ToString(dt.Rows(i)("OutcomeStatus"))
                        _Outcome.OutcomeNotes = Convert.ToString(dt.Rows(i)("OutcomeNotes"))
                        _Outcome.Outcomedate = Convert.ToString(dt.Rows(i)("dtOutcomeDate"))
                        _Outcome.AssociateId = Convert.ToString(dt.Rows(i)("AssociatedId"))
                        _Outcome.AssociateType = Convert.ToString(dt.Rows(i)("AssociatedType"))
                        _Outcome.OutcomeValue = Convert.ToString(dt.Rows(i)("OutcomeValue"))
                        _Outcome.OutcomeValueUnit = Convert.ToString(dt.Rows(i)("OutcomeValueUnit"))
                        _OutcomeList.Add(_Outcome)
                        If Not IsNothing(_Outcome) Then
                            _Outcome.Dispose()
                            _Outcome = Nothing
                        End If
                    Next
                End If
            End If
            _oCol.OutcomeList = _OutcomeList
            If Not IsNothing(_OutcomeList) Then
                _OutcomeList = Nothing
            End If
            Return _oCol
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
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function
    Private Function FillPendingTests(Optional ByVal _LabtestCol As LabTestCol = Nothing) As PlannedModuleCol
        Dim _oCol As PlannedModuleCol = Nothing
        Dim _PlannedCare As PlannedModule
        Try
            Dim _PlannedModuleList As List(Of PlannedModule) = New List(Of PlannedModule)()
            Dim _labtest As LabTest = Nothing
            If Not IsNothing(_LabtestCol) AndAlso _LabtestCol.Count > 0 Then
                For index As Integer = 0 To _LabtestCol.Count - 1
                    _labtest = _LabtestCol.Item(index)
                    _PlannedCare = New PlannedModule
                    _PlannedCare.PlannedCode = _labtest.TestLOINCcode
                    _PlannedCare.PlannedName = _labtest.TestName
                    _PlannedCare.Details = "Pending Lab Test"
                    _PlannedCare.EffectivePlannedDate = _labtest.ScheduledDateTime
                    _PlannedCare.PlannedModuletype = "Order"
                    _PlannedCare.PlannedStatus = "active"
                    _PlannedModuleList.Add(_PlannedCare)
                    If Not IsNothing(_PlannedCare) Then
                        _PlannedCare.Dispose()
                        _PlannedCare = Nothing
                    End If
                Next
            End If
            _oCol = New PlannedModuleCol
            _oCol.PlannedModuleList = _PlannedModuleList

            If Not IsNothing(_PlannedModuleList) Then
                _PlannedModuleList = Nothing
            End If
            Return _oCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally

        End Try
    End Function

    Public Function CheckCodeInUCUM(ByVal code As String) As String
        Try
            Dim cmdMain As SqlCommand = Nothing
            Dim _Code As String = ""

            Try

                cmdMain = New SqlCommand
                cmdMain.Connection = New SqlConnection(gloLibCCDGeneral.Connectionstring)
                cmdMain.CommandType = CommandType.StoredProcedure
                cmdMain.CommandText = "gsp_CheckCodeInUCUM"
                Dim osqlpara As SqlParameter = Nothing
                osqlpara = New SqlParameter
                osqlpara.ParameterName = "@Code"
                osqlpara.Direction = ParameterDirection.Input
                osqlpara.DbType = SqlDbType.VarChar
                osqlpara.Value = code
                cmdMain.Parameters.Add(osqlpara)
                osqlpara = Nothing

                cmdMain.Connection.Open()

                _Code = cmdMain.ExecuteScalar

                cmdMain.Connection.Close()

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                Throw New gloCCDException(ex.ToString())

            Finally

                If IsNothing(cmdMain.Connection) = False Then
                    cmdMain.Connection.Dispose()
                    cmdMain.Connection = Nothing
                End If

                If IsNothing(cmdMain) = False Then
                    cmdMain.Dispose()
                    cmdMain = Nothing
                End If

            End Try


            Return _Code
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
