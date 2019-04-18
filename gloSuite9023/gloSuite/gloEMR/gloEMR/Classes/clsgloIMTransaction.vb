
Public Class clsgloIMTransaction

    Private _transaction_id As Decimal
    Private _nLocationID As Decimal

    Private _nCategoryID As Decimal

    Private _transaction_date As DateTime
    Private _admin_repo_refused As String

    Private _administered_id As Decimal
    Private _ProviderID As Decimal

    Private _sku As String
    Private _vaccine As String
    Private _tradeName As String
    Private _manufacturer As String
    Private _lot_number As String
    Private _expiration_date As DateTime
    Private _dosage_given As Decimal
    Private _amount_given As Decimal
    Private _units As String
    Private _route As String
    Private _Site As String
    Private _bvis_given As Boolean
    Private _vis As String
    Private _publication_date As DateTime
    Private _publicity_effective_date As DateTime
    Private _refusal_reason As String = String.Empty
    Private _refusal_reasonCode As String = String.Empty

    Private _Cqm_Code As String = String.Empty
    Private _Cqm_Desc As String = String.Empty
    Private _refusal_comments As String
    Private _refused_by As String
    Private _Cqmlbl_by As String

    Private _reminder As Boolean
    Private _due_date As DateTime
    Private _Diagnosis_code As String
    Private _cpt_code As String
    ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
    Private _ndc_code As String
    Private _funding As String
    Private _immunizationfunding As String
    Private _notes As String
    Private _user_id As Decimal
    Private _PatientID As Decimal

    Private _bPatientHasAReaction As Boolean
    Private _OnsetDate As DateTime

    Private _sAdverseEvent As String
    Private _bPatientDied As Boolean
    Private _PatientDieddate As DateTime
    Private _bLifeThreateningIllness As Boolean
    Private _bRequiredEmergencyRoom As Boolean
    Private _bRequiredHospitalization As Boolean
    Private _HospitalizationDays As Integer
    Private _bResultedInProlongation As Boolean
    Private _bResultedInPermDisability As Boolean
    Private _bNoneOfTheAbove As Boolean
    Private _sPatientRecovered As String
    Private _Status As String
    Private _Reaction As String
    Private _visDocumentID As Int64
    Private _VisAssociatedDocumentID As Int64

    Private _sConceptId As String = ""
    Private _spublicityCode As String = ""
    Private _spublicityCodeDescription As String = ""
    Private _dtPublicityCodeLastUpdatedDateTime As DateTime = DateTime.MinValue
    Private _ICDType As Integer ''added for ICD10 implementation
    Private _DtUncertainFormulationCvx As DataTable 'Added By Manoj Jadhav on 20130928 Mu2 Transfer to immunization Registry - for sharing uncertain formulation CVX 
    Private _DtImmunizationVIS As DataTable
    Private _OrderingProviderID As Int64
    Private _OrderingProviderType As Int32
    Public Property OrderingProviderID() As Int64
        Get
            Return _OrderingProviderID
        End Get
        Set(ByVal value As Int64)
            _OrderingProviderID = value
        End Set
    End Property
    Public Property OrderingProviderType() As Int32
        Get
            Return _OrderingProviderType
        End Get
        Set(ByVal value As Int32)
            _OrderingProviderType = value
        End Set
    End Property

    Public Property DtUncertainFormulationCVX() As DataTable
        Get
            Return _DtUncertainFormulationCvx
        End Get
        Set(value As DataTable)
            _DtUncertainFormulationCvx = value
        End Set
    End Property

    Public Property DtImmunizationVIS() As DataTable
        Get
            Return _DtImmunizationVIS
        End Get
        Set(value As DataTable)
            _DtImmunizationVIS = value
        End Set
    End Property
    Public Property SnoMedConceptID() As String
        Get
            Return _sConceptId
        End Get
        Set(value As String)
            _sConceptId = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
        End Set
    End Property
    Private _sConceptIdDescription As String = ""

    Public Property SnoMedConceptDescription() As String
        Get
            Return _sConceptIdDescription
        End Get
        Set(value As String)
            _sConceptIdDescription = value
        End Set
    End Property


    Private _ImmunizationStatus As String = ""

    Public Property ImmunizationStatus() As String
        Get
            Return _ImmunizationStatus
        End Get
        Set(value As String)
            _ImmunizationStatus = value
        End Set
    End Property

    'varchar(1000)	    VIS	
    Public TranctionID As Int64 = -1
    Public Property Reaction() As String
        Get
            Return _Reaction
        End Get
        Set(ByVal Value As String)
            _Reaction = Value
        End Set
    End Property

    Public Property ProviderID() As Decimal
        Get
            Return _ProviderID
        End Get
        Set(ByVal Value As Decimal)
            _ProviderID = Value
        End Set
    End Property

    Public Property OnsetDate() As DateTime
        Get
            Return _OnsetDate
        End Get
        Set(ByVal Value As DateTime)
            _OnsetDate = Value
        End Set
    End Property

    Public Property bPatientHasAReaction() As Boolean
        Get
            Return _bPatientHasAReaction
        End Get
        Set(ByVal Value As Boolean)
            _bPatientHasAReaction = Value
        End Set
    End Property

    Public Property sPatientRecovered() As String
        Get
            Return _sPatientRecovered
        End Get
        Set(ByVal Value As String)
            _sPatientRecovered = Value
        End Set
    End Property

    Public Property bNoneOfTheAbove() As Boolean
        Get
            Return _bNoneOfTheAbove
        End Get
        Set(ByVal Value As Boolean)
            _bNoneOfTheAbove = Value
        End Set
    End Property

    Public Property bResultedInPermDisability() As Boolean
        Get
            Return _bResultedInPermDisability
        End Get
        Set(ByVal Value As Boolean)
            _bResultedInPermDisability = Value
        End Set
    End Property

    Public Property bResultedInProlongation() As Boolean
        Get
            Return _bResultedInProlongation
        End Get
        Set(ByVal Value As Boolean)
            _bResultedInProlongation = Value
        End Set
    End Property

    Public Property HospitalizationDays() As Integer
        Get
            Return _HospitalizationDays
        End Get
        Set(ByVal Value As Integer)
            _HospitalizationDays = Value
        End Set
    End Property

    Public Property bRequiredHospitalization() As Boolean
        Get
            Return _bRequiredHospitalization
        End Get
        Set(ByVal Value As Boolean)
            _bRequiredHospitalization = Value
        End Set
    End Property

    Public Property bRequiredEmergencyRoom() As Boolean
        Get
            Return _bRequiredEmergencyRoom
        End Get
        Set(ByVal Value As Boolean)
            _bRequiredEmergencyRoom = Value
        End Set
    End Property

    Public Property bLifeThreateningIllness() As Boolean
        Get
            Return _bLifeThreateningIllness
        End Get
        Set(ByVal Value As Boolean)
            _bLifeThreateningIllness = Value
        End Set
    End Property

    Public Property PatientDieddate() As DateTime
        Get
            Return _PatientDieddate
        End Get
        Set(ByVal Value As DateTime)
            _PatientDieddate = Value
        End Set
    End Property

    Public Property bPatientDied() As Boolean
        Get
            Return _bPatientDied
        End Get
        Set(ByVal Value As Boolean)
            _bPatientDied = Value
        End Set
    End Property

    Public Property sAdverseEvent() As String
        Get
            Return _sAdverseEvent
        End Get
        Set(ByVal Value As String)
            _sAdverseEvent = Value
        End Set
    End Property

    Public Property PatientID() As Decimal
        Get
            Return _PatientID
        End Get
        Set(ByVal Value As Decimal)
            _PatientID = Value
        End Set
    End Property

    Public Property user_id() As Decimal
        Get
            Return _user_id
        End Get
        Set(ByVal Value As Decimal)
            _user_id = Value
        End Set
    End Property

    Public Property notes() As String
        Get
            Return _notes
        End Get
        Set(ByVal Value As String)
            _notes = Value
        End Set
    End Property

    Public Property funding() As String
        Get
            Return _funding
        End Get
        Set(ByVal Value As String)
            _funding = Value
        End Set
    End Property

    Public Property immunizationfunding() As String
        Get
            Return _immunizationfunding
        End Get
        Set(ByVal Value As String)
            _immunizationfunding = Value
        End Set
    End Property
    ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
    Public Property ndc_code() As String
        Get
            Return _ndc_code
        End Get
        Set(ByVal Value As String)
            _ndc_code = Value
        End Set
    End Property

    Public Property cpt_code() As String
        Get
            Return _cpt_code
        End Get
        Set(ByVal Value As String)
            _cpt_code = Value
        End Set
    End Property

    Public Property Diagnosis_code() As String
        Get
            Return _Diagnosis_code
        End Get
        Set(ByVal Value As String)
            _Diagnosis_code = Value
        End Set
    End Property

    Public Property due_date() As DateTime
        Get
            Return _due_date
        End Get
        Set(ByVal Value As DateTime)
            _due_date = Value
        End Set
    End Property

    Public Property reminder() As Boolean
        Get
            Return _reminder
        End Get
        Set(ByVal Value As Boolean)
            _reminder = Value
        End Set
    End Property

    Public Property refused_by() As String
        Get
            Return _refused_by
        End Get
        Set(ByVal Value As String)
            _refused_by = Value
        End Set
    End Property


    Public Property Cqmlbl_by() As String
        Get
            Return _Cqmlbl_by
        End Get
        Set(ByVal Value As String)
            _Cqmlbl_by = Value
        End Set
    End Property


    Public Property refusal_comments() As String
        Get
            Return _refusal_comments
        End Get
        Set(ByVal Value As String)
            _refusal_comments = Value
        End Set
    End Property

    Public Property refusal_reason() As String
        Get
            Return _refusal_reason
        End Get
        Set(ByVal Value As String)
            _refusal_reason = Value
        End Set
    End Property
    Public Property refusal_reasonCode() As String
        Get
            Return _refusal_reasonCode
        End Get
        Set(ByVal Value As String)
            _refusal_reasonCode = Value
        End Set
    End Property

    'cqm
    Public Property CQM_Code() As String
        Get
            Return _Cqm_Code
        End Get
        Set(ByVal Value As String)
            _Cqm_Code = Value
        End Set
    End Property
    Public Property CQM_Desc() As String
        Get
            Return _Cqm_Desc

        End Get
        Set(ByVal Value As String)
            _Cqm_Desc = Value
        End Set
    End Property

    Public Property publication_date() As DateTime
        Get
            Return _publication_date
        End Get
        Set(ByVal Value As DateTime)
            _publication_date = Value
        End Set
    End Property

    Public Property publicity_effective_date() As DateTime
        Get
            Return _publicity_effective_date
        End Get
        Set(ByVal Value As DateTime)
            _publicity_effective_date = Value
        End Set
    End Property

    Public Property vis() As String
        Get
            Return _vis
        End Get
        Set(ByVal Value As String)
            _vis = Value
        End Set
    End Property
    Public Property visDocumentID() As String
        Get
            Return _visDocumentID
        End Get
        Set(ByVal Value As String)
            _visDocumentID = Value
        End Set
    End Property

    Public Property VisAssociatedDocumentID() As String
        Get
            Return _VisAssociatedDocumentID
        End Get
        Set(ByVal Value As String)
            _VisAssociatedDocumentID = Value
        End Set
    End Property
    Public Property bvis_given() As Boolean
        Get
            Return _bvis_given
        End Get
        Set(ByVal Value As Boolean)
            _bvis_given = Value
        End Set
    End Property

    Public Property Site() As String
        Get
            Return _Site
        End Get
        Set(ByVal Value As String)
            _Site = Value
        End Set
    End Property

    Public Property route() As String
        Get
            Return _route
        End Get
        Set(ByVal Value As String)
            _route = Value
        End Set
    End Property

    Public Property units() As String
        Get
            Return _units
        End Get
        Set(ByVal Value As String)
            _units = Value
        End Set
    End Property

    Public Property amount_given() As Decimal
        Get
            Return _amount_given
        End Get
        Set(ByVal Value As Decimal)
            _amount_given = Value
        End Set
    End Property

    Public Property dosage_given() As Decimal
        Get
            Return _dosage_given
        End Get
        Set(ByVal Value As Decimal)
            _dosage_given = Value
        End Set
    End Property

    Public Property expiration_date() As DateTime
        Get
            Return _expiration_date
        End Get
        Set(ByVal Value As DateTime)
            _expiration_date = Value
        End Set
    End Property

    Public Property lot_number() As String
        Get
            Return _lot_number
        End Get
        Set(ByVal Value As String)
            _lot_number = Value
        End Set
    End Property

    Public Property manufacturer() As String
        Get
            Return _manufacturer
        End Get
        Set(ByVal Value As String)
            _manufacturer = Value
        End Set
    End Property

    Public Property tradeName() As String
        Get
            Return _tradeName
        End Get
        Set(ByVal Value As String)
            _tradeName = Value
        End Set
    End Property

    Public Property vaccine() As String
        Get
            Return _vaccine
        End Get
        Set(ByVal Value As String)
            _vaccine = Value
        End Set
    End Property

    Public Property sku() As String
        Get
            Return _sku
        End Get
        Set(ByVal Value As String)
            _sku = Value
        End Set
    End Property

    Public Property administered_id() As Decimal
        Get
            Return _administered_id
        End Get
        Set(ByVal Value As Decimal)
            _administered_id = Value
        End Set
    End Property

    Public Property admin_repo_refused() As String
        Get
            Return _admin_repo_refused
        End Get
        Set(ByVal Value As String)
            _admin_repo_refused = Value
        End Set
    End Property

    Public Property transaction_date() As DateTime
        Get
            Return _transaction_date
        End Get
        Set(ByVal Value As DateTime)
            _transaction_date = Value
        End Set
    End Property

    Public Property transaction_id() As Decimal
        Get
            Return _transaction_id
        End Get
        Set(ByVal Value As Decimal)
            _transaction_id = Value
        End Set
    End Property

    Public Property nLocationID() As Decimal
        Get
            Return _nLocationID
        End Get
        Set(ByVal Value As Decimal)
            _nLocationID = Value
        End Set
    End Property

    Public Property nCategoryID() As Decimal
        Get
            Return _nCategoryID
        End Get
        Set(ByVal Value As Decimal)
            _nCategoryID = Value
        End Set
    End Property

    Public Property PublicityCode() As String
        Get
            Return _spublicityCode
        End Get
        Set(ByVal Value As String)
            _spublicityCode = Value
        End Set
    End Property

    Public Property PublicityCodeDescription() As String
        Get
            Return _spublicityCodeDescription
        End Get
        Set(ByVal Value As String)
            _spublicityCodeDescription = Value
        End Set
    End Property
    Public Property ICDType As Integer ''added for ICD10 implementation
        Get
            Return _ICDType
        End Get
        Set(ByVal Value As Integer)
            _ICDType = Value
        End Set
    End Property
    'Private _dtPublicityCodeLastUpdatedDateTime As DateTime = DateTime.MinValue
    Public ReadOnly Property PublicityCodeLastUpdatedDateTime() As DateTime
        Get
            Return _dtPublicityCodeLastUpdatedDateTime
        End Get
        'Set(value As DateTime)
        '    _dtPublicityCodeLastUpdatedDateTime = value
        'End Set
    End Property



    Public Function AddIMTransaction() As Int64
        'Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@transaction_id", _transaction_id, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@transaction_date", _transaction_date, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@admin_repo_refused", _admin_repo_refused, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@administered_id", _administered_id, ParameterDirection.Input, SqlDbType.BigInt)

            oDBParameters.Add("@nLocationID", _nLocationID, ParameterDirection.Input, SqlDbType.BigInt)


            oDBParameters.Add("@nProviderID", _ProviderID, ParameterDirection.Input, SqlDbType.BigInt)

            oDBParameters.Add("@sku", _sku, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@vaccine", _vaccine, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@tradeName", _tradeName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@manufacturer", _manufacturer, ParameterDirection.Input, SqlDbType.Text)

            oDBParameters.Add("@lot_number", _lot_number, ParameterDirection.Input, SqlDbType.Text)

            If _expiration_date <> "12:00:00 AM" Then
                oDBParameters.Add("@expiration_date", _expiration_date, ParameterDirection.Input, SqlDbType.DateTime)
            End If

            oDBParameters.Add("@dosage_given", _dosage_given, ParameterDirection.Input, SqlDbType.Decimal)
            oDBParameters.Add("@amount_given", _amount_given, ParameterDirection.Input, SqlDbType.Decimal)

            oDBParameters.Add("@units", _units, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@route", _route, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@Site", _Site, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@bvis_given", _bvis_given, ParameterDirection.Input, SqlDbType.Bit)

            oDBParameters.Add("@vis", _vis, ParameterDirection.Input, SqlDbType.Text)

            If _publication_date <> "12:00:00 AM" Then
                oDBParameters.Add("@publication_date", _publication_date, ParameterDirection.Input, SqlDbType.DateTime)
            End If

            oDBParameters.Add("@refusal_reason", _refusal_reason, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@refusal_comments", _refusal_comments, ParameterDirection.Input, SqlDbType.Text)

            oDBParameters.Add("@refused_by", _refused_by, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@reminder", _reminder, ParameterDirection.Input, SqlDbType.Bit)

            If _due_date <> "12:00:00 AM" Then
                oDBParameters.Add("@due_date", _due_date, ParameterDirection.Input, SqlDbType.DateTime)
            End If

            oDBParameters.Add("@Diagnosis_code", _Diagnosis_code, ParameterDirection.Input, SqlDbType.Text)

            oDBParameters.Add("@cpt_code", _cpt_code, ParameterDirection.Input, SqlDbType.Text)
            ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS  
            oDBParameters.Add("@ndc_code", _ndc_code, ParameterDirection.Input, SqlDbType.NVarChar)
            oDBParameters.Add("@funding", _funding, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@immunizationfunding", _immunizationfunding, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@notes", notes, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@user_id", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)


            oDBParameters.Add("@bPatientHasAReaction", _bPatientHasAReaction, ParameterDirection.Input, SqlDbType.Bit)



            oDBParameters.Add("@dtOnsetDate", _OnsetDate, ParameterDirection.Input, SqlDbType.DateTime)

            oDBParameters.Add("@sAdverseEvent", _sAdverseEvent, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@bPatientDied", _bPatientDied, ParameterDirection.Input, SqlDbType.Bit)

            If _bPatientDied = True Then
                oDBParameters.Add("@PatientDieddate", _PatientDieddate, ParameterDirection.Input, SqlDbType.DateTime)
            End If

            oDBParameters.Add("@bLifeThreateningIllness", _bLifeThreateningIllness, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bRequiredEmergencyRoom", _bRequiredEmergencyRoom, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bRequiredHospitalization", _bRequiredHospitalization, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@HospitalizationDays", _HospitalizationDays, ParameterDirection.Input, SqlDbType.Int)

            oDBParameters.Add("@bResultedInProlongation", _bResultedInProlongation, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bResultedInPermDisability", _bResultedInPermDisability, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bNoneOfTheAbove", _bNoneOfTheAbove, ParameterDirection.Input, SqlDbType.Bit)

            oDBParameters.Add("@sPatientRecovered", _sPatientRecovered, ParameterDirection.Input, SqlDbType.Char)

            oDBParameters.Add("@sReaction", _Reaction, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@nDocumentID", _visDocumentID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@biMasterID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nMasterDocumentID", _VisAssociatedDocumentID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nCategoryID", _nCategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@transaction_id_Op", _VisAssociatedDocumentID, ParameterDirection.Output, SqlDbType.BigInt)
            oDBParameters.Add("@sSnoMedConceptID", _sConceptId, ParameterDirection.Input, SqlDbType.VarChar, 100)
            oDBParameters.Add("@sSnoMedConceptDescription", _sConceptIdDescription, ParameterDirection.Input, SqlDbType.VarChar, 1000)

            '@sPublicityCode as varchar(10) = null,  
            '@sPublicityCodeDesc as varchar(MAX) = null, 
            oDBParameters.Add("@sPublicityCode", _spublicityCode, ParameterDirection.Input, SqlDbType.VarChar, 10)
            oDBParameters.Add("@sPublicityCodeDesc", _spublicityCodeDescription, ParameterDirection.Input, SqlDbType.VarChar, 1000)

            'Parametor Added By Manoj Jadhav on 20130928 Mu2 Transfer to immunization Registry - for sharing uncertain formulation CVX 
            oDBParameters.Add("@TVP_UncertainFormulationCVX", DtUncertainFormulationCVX, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@nOderingProviderID", _OrderingProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOrderingProviderType", _OrderingProviderType, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@nICDRevision", _ICDType, ParameterDirection.Input, SqlDbType.SmallInt) ''added for ICD10 implementation
            oDBParameters.Add("@sRufusalReasonCode", _refusal_reasonCode, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sValuesetOID", _Cqm_Code, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sValueset", _Cqm_Desc, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sStatus", _Status, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@PublicityCodeLastUpdated", _publicity_effective_date, ParameterDirection.Input, SqlDbType.Date)

            oDBParameters.Add("@TVP_ImmunizationVIS", DtImmunizationVIS, ParameterDirection.Input, SqlDbType.Structured)

            TranctionID = -1
            oDB.Execute("IM_AddUpdateTransactionLine", oDBParameters, TranctionID)



            'Developer: Mitesh Patel
            'Date:31-Jan-2012
            'Prd: Immunization 
            'Add Entry For HL7 message queue 

            Dim biTransactionID As Int64 = 0

            oDBParameters.Clear()
            oDBParameters.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@biMasterID", 0, ParameterDirection.Output, SqlDbType.BigInt)


            oDB.Execute("GetIMMasterIDForPatient", oDBParameters, biTransactionID)

            AddMessageQueue(biTransactionID, _PatientID, TranctionID, _ImmunizationStatus)

            'oDB.Disconnect()
            'oDBParameters.Dispose()
            'oDBParameters = Nothing

            'oDB.Dispose()
            'oDB = Nothing
            Return TranctionID
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If (IsNothing(oDBParameters) = False) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If (IsNothing(oDB) = False) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function
    Public Function AddIMHistory(ByVal nAuditTrailID As Int64, ByVal sActivityType As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim _objHistoryId As Object = Nothing
        Dim _nHistoryId As Int64
        Try
            oDB.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@transaction_id", TranctionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nAuditTrailID", nAuditTrailID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sActivityType", sActivityType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nHistoryId", _nHistoryId, ParameterDirection.Output, SqlDbType.BigInt)

            oDB.Execute("IM_AddHistoryLine", oDBParameters, _objHistoryId)
            If (_objHistoryId IsNot Nothing) Then
                _nHistoryId = Convert.ToInt64(_objHistoryId)
            End If

            Return _nHistoryId
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function UpdateIMHistory(ByVal nAuditTrailID As Int64, ByVal nHistoryId As Int64)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@nAuditTrailID", nAuditTrailID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nHistoryId", nHistoryId, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Execute("IM_HistoryUpdate", oDBParameters)
            oDB.Disconnect() 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return Nothing
    End Function


    Public Function ShowImmunizationHistory() As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@transaction_id", TranctionID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("IM_GetHistory", oDBParameters, dtIM)

        Catch ex As Exception


            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dtIM

    End Function


    Public Function ShowImmunization() As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("IM_HistoryList", oDBParameters, dtIM)

        Catch ex As Exception


            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dtIM

    End Function

    Public Sub AddMessageQueue(ByVal Trans_ID As Int64, ByVal PatientId As Int64, Optional ByVal IM_TranctionID As Int64 = 0, Optional ByVal _InternalImmunizationStatus As String = "")
        Dim oGeneralInterface As New clsGeneralInterface()

        Try

            ' If gblnSendChargesToHL7 = True Then
            If gblnHL7SENDOUTBOUNDGLOEMR = True AndAlso gblnSendImmunization = True AndAlso _InternalImmunizationStatus <> "D" Then
                oGeneralInterface.SendImmunization("V04", Trans_ID, PatientId)
            End If

            If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format Then
                oGeneralInterface.SendImmunization("V04IR", Trans_ID, PatientId, IM_TranctionID, _InternalImmunizationStatus)
            End If

            ' End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oGeneralInterface) Then
                oGeneralInterface.Dispose()
                oGeneralInterface = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Select list in datatable
    ''' </summary>
    ''' <param name="strSQL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetList(ByVal strSQL As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim dtRoute As DataTable = Nothing
        Try
            oDB.Connect(False)
            oDB.Retrive_Query(strSQL, dtRoute)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            Return dtRoute
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If Not IsNothing(dtRoute) Then
                dtRoute.Dispose()
                dtRoute = Nothing
            End If
        End Try
    End Function

End Class
