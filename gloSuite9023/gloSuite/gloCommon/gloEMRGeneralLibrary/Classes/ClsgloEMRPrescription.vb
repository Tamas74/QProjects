Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloSureScript
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Web.Script.Serialization
Imports System.Linq
Imports System.Configuration
Imports System.IO
Imports gloSureScript.gloSureScriptInterface
Imports System.Text
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports gloGlobal.Schemas.PDR
Imports schema = gloGlobal.Schemas.Surescript
Imports LocalSchema = gloGlobal.SS

Namespace gloEMRPrescription

    Public Class RefRequest
        Implements IDisposable

        Public Sub New()

        End Sub

        Public Sub New(ByVal RefReq_TransactionID As Long, ByVal RefReq_RequestedRefillQty As String, ByVal RefReq_ReferenceNumber As String, ByVal RefReq_DateReceived As DateTime, ByVal RefReq_MessageId As String, ByVal RefReq_NDCCode As String, Optional ByVal RefReq_ProviderId As String = "", Optional ByVal RefReq_PharmacyId As String = "", Optional ByVal RefReq_PatientId As Long = 0)
            TransactionID = RefReq_TransactionID
            TransactionID = RefReq_TransactionID
            RefillRequestQty = RefReq_RequestedRefillQty
            ReferenceNumber = RefReq_ReferenceNumber
            MessageID = RefReq_MessageId
            DateReceived = RefReq_DateReceived
            NDCCode = RefReq_NDCCode
            PharmacyID = RefReq_PharmacyId
            ProviderID = RefReq_ProviderId
            PatientID = RefReq_PatientId
        End Sub

        Public Property TransactionID As Long = 0
        Public Property MessageID As String = ""
        Public Property ProviderID As String = ""
        Public Property PatientID As Long = 0

        Public Property NDCCode As String = ""
        Public Property RefillRequestQty As String = ""
        Public Property ReferenceNumber As String = ""
        Public Property DateReceived As DateTime
        Public Property PharmacyID As String = ""

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Class RxBusinesslayer
        Implements IDisposable

        Enum PrintFaxRx
            PrintRx
            FaxRx
        End Enum

        Public Enum EligibilityStatus
            NotChecked
            Passed
            Failed
        End Enum

        Dim InternetMsgCnt As Int16 = 0
        ' Public Shared _EMRConnectionString As String = "" ''''var will contain EMR connection string whnever we are working with Formulary database string       
        'Formulary
        Public _RxHubFormulary As gloEMRGeneralLibrary.gloEMRActors.RxHubFormulary
        Public _ds As DataSet 'uesd for saving Rx Meds with TVP(6040)
        Public Shared _RxTransactionID As Long = 0
        Public Shared PONNumber As Long = 0
        Public ERxStatus As String = String.Empty
        Public ERxStatusMessage As String = String.Empty
        Dim _PBMName As String = ""
        Dim _FormularyID As String
        Dim _CopayID As String
        Dim _CoverageID As String
        Dim _AlternativeID As String
        Dim _CopayID_AbbrevatedCopay As String
        Dim _DrugPackGPI As String = ""
        Dim dtMedicationData As DataTable = Nothing

        Dim ArryLstNDC As New ArrayList 'this array list will hold the coolection of NDC against the Drug Ids and will be used to query the coverage and copay information.
        Dim _sFormularyStatusPreferencelevel As String = "" 'to show whether the durg Prefference level
        Dim _C1RxFormularyGrid As New C1.Win.C1FlexGrid.C1FlexGrid
        Dim _C1RxFormularyIndicatorGrid As New C1.Win.C1FlexGrid.C1FlexGrid
        Dim blnStepMedicationHeadingAdded As Boolean = False 'grid caption for Stemp medication not to be repeated
        Dim _PayerAlternativesExist As Boolean = False 'if payer alternative exist then dont call the Therapautic alternatives
        Dim _DrugPack_Thirdprtyrestrcode As String = "" 'this wil help to determine whether the Drug type is of type "Supplies" .ie if value is 3/5 then it is "Supplies"

        Dim strbldFormularyAlternativeGrid_Temp As New System.Text.StringBuilder
        Dim strbldFormularyRichtext_Temp As New System.Text.StringBuilder


        Dim IsDrugExclusion As Boolean = False 'if the coverage factor is Drug Exclusion(DE) then set the flag = true and the formulary status of the drug will be "Not Reimbursable"
        'Formulary

        Private _Prescriptions As Prescriptions
        Private _Prescriptions_SelectedPBM As Prescriptions

        Private _Formularys As RxHubFormularys

        Private _Medications As Collection
        '----------------------------------------
        Private _MedicalConditions As Collection
        '----------------------------------------
        Public oOldgloPrescription As EPrescription
        Private WithEvents ogloInterface As gloSureScriptInterface
        'supriya 24/7/2008
        'Flag to check if message is valid or not,if any validation fails the flag is set to true
        Private MessageInValid As Boolean = False
        'supriya 24/7/2008
        Private _PatientID As Long

        Private _SupervisingProviderID As Int64
        Public eRxSelecteditem As Int64
        Public eRxResponseitem As Int64
        Public _eRxStatusMessage As String = ""
        Public _eRxStatus As String = ""
        Public _eRxStatusmessagecode As String = ""
        Public oProcessERxPrescription As EPrescription

        Private RxChangeRequest As LocalSchema.RxChangeRequest

        Private ECPSeRxDrugs As New List(Of Int64)

        Dim _routes As List(Of String)
        Public Property routes() As List(Of String)
            Get
                Return _routes
            End Get
            Set(ByVal Value As List(Of String))
                _routes = Value
            End Set
        End Property

        Private ReadOnly Property IsChangeRequest As Boolean
            Get
                If RxChangeRequest IsNot Nothing Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        Public Sub New(ByVal PatientID As Long)
            MyBase.New()
            InitialiseObjects()
            _PatientID = PatientID
            InitialiseFonts()
        End Sub

        Public Sub New(ByVal ChangeRequest As LocalSchema.RxChangeRequest)
            MyBase.New()
            InitialiseObjects()

            Me._PatientID = ChangeRequest.PatientID
            Me.RxChangeRequest = ChangeRequest
            Me.OldPharmacyId = ChangeRequest.PharmacyID

            InitialiseFonts()
        End Sub

        Private Sub InitialiseObjects()
            _Prescriptions = New Prescriptions
            _Prescriptions_SelectedPBM = New Prescriptions
            _Histories = New Histories
            oOldgloPrescription = New EPrescription
            'EAR Formulary
            _RxHubFormulary = New gloEMRGeneralLibrary.gloEMRActors.RxHubFormulary
        End Sub
        Public Event PrescriptionSaved(ByVal SaveStatus As Boolean)
        Public Event SurescriptMessageInvalidated()

        Public Event PrintPDRPrograms(ByVal P As List(Of ProgramResponse))

        Public Event UpdateRx_eRxStatus(ByVal eRxStatus As String, ByVal _eRxStatusMessage As String, ByVal eRx_DrgNDCCode As String, ByVal ItemNumber As Integer, ByVal IseRxed As Integer) ' bug 13911 modified added new parameter itemnumber to fix 
        Private SELECTIONFONT_BOLD As System.Drawing.Font
        Private SELECTIONFONT_REGULAR As System.Drawing.Font
        Private SELECTIONFONT_BOLD_UNDERLINE As System.Drawing.Font

#Region "private variables"
        Private _oldProviderID As Int64
        Private _oldPharmacyID As Int64
        Private _intCurrentVisitID As Int64
        Private _ProviderId As Long
        Private _PharmacyId As Long
        Private _intPastVisitID As Int64
        Private eTransactionMode As _TransactionMode
        Private _TempVisitID As Int64
        Private _TempDate As DateTime
        Private _VisitDate As DateTime
        Private _PastVisitDate As DateTime
        Private _Histories As Histories
        Private _tmpCheckStates As tmpCheckStates
        Private _Prescriptiondate As DateTime
        Private _PrintStatus As String
        Public Event PrintFaxPrescription(ByVal enumPrintFax As PrintFaxRx)
        Public Event PrescriptionDeleted()


        'For Pharmacy
        Private _PhNCPDPID As String = ""
        Private _PhContactID As Int64
        Private _PharmacyName As String = ""
        Private _PhAddressline1 As String = ""
        Private _PhAddressline2 As String = ""
        Private _PhCity As String = ""
        Private _PhState As String = ""
        Private _PhZip As String = ""
        Private _PhEmail As String = ""
        Private _PhFax As String = ""
        Private _PhPhone As String = ""
        Private _PhServiceLevel As String = ""
        'For Pharmacy

        Enum _TransactionMode
            Add
            Edit
        End Enum
#End Region

#Region "properties"
        Public ReadOnly Property GetCurrentUserName() As String
            Get
                Return globalSecurity.gstrLoginName
            End Get

        End Property

        'For Pharmacy
        Public Property PhNCPDPID() As String
            Get
                Return _PhNCPDPID
            End Get
            Set(ByVal value As String)
                _PhNCPDPID = value
            End Set
        End Property

        Public Property PhContactID() As Int64
            Get
                Return _PhContactID
            End Get
            Set(ByVal value As Int64)
                _PhContactID = value
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

        Public Property PhAddressline1() As String
            Get
                Return _PhAddressline1
            End Get
            Set(ByVal value As String)
                _PhAddressline1 = value
            End Set
        End Property
        Public Property PhAddressline2() As String
            Get
                Return _PhAddressline2
            End Get
            Set(ByVal value As String)
                _PhAddressline2 = value
            End Set
        End Property

        Public Property PhCity() As String
            Get
                Return _PhCity
            End Get
            Set(ByVal value As String)
                _PhCity = value
            End Set
        End Property
        Public Property PhState() As String
            Get
                Return _PhState
            End Get
            Set(ByVal value As String)
                _PhState = value
            End Set
        End Property

        Public Property PhZip() As String
            Get
                Return _PhZip
            End Get
            Set(ByVal value As String)
                _PhZip = value
            End Set
        End Property
        Public Property PhEmail() As String
            Get
                Return _PhEmail
            End Get
            Set(ByVal value As String)
                _PhEmail = value
            End Set
        End Property
        Public Property PhFax() As String
            Get
                Return _PhFax
            End Get
            Set(ByVal value As String)
                _PhFax = value
            End Set
        End Property
        Public Property PhPhone() As String
            Get
                Return _PhPhone
            End Get
            Set(ByVal value As String)
                _PhPhone = value
            End Set
        End Property

        Public Property PhServiceLevel() As String
            Get
                Return _PhServiceLevel
            End Get
            Set(ByVal value As String)
                _PhServiceLevel = value
            End Set
        End Property
        'For Pharmacy

        Public Property PrescriptionDate() As DateTime
            Get
                Return _Prescriptiondate
            End Get
            Set(ByVal value As DateTime)
                _Prescriptiondate = value
            End Set
        End Property
        Public Property MedicationCol() As Collection
            Get
                Return _Medications
            End Get
            Set(ByVal value As Collection)
                _Medications = value
            End Set
        End Property

        '-------------------------------------------------------
        Public Property MedicalCondtionCol() As Collection
            Get
                Return _MedicalConditions
            End Get
            Set(ByVal value As Collection)
                _MedicalConditions = value
            End Set
        End Property
        '-------------------------------------------------------

        Public Property PastVisitDate() As DateTime
            Get
                Return _PastVisitDate
            End Get
            Set(ByVal value As DateTime)
                _PastVisitDate = value
            End Set
        End Property
        Public Property PrintStatus() As String
            Get
                Return _PrintStatus
            End Get
            Set(ByVal value As String)
                _PrintStatus = value
            End Set
        End Property
        Public Property TmpCheckStatesCol() As tmpCheckStates
            Get
                If IsNothing(_tmpCheckStates) Then
                    _tmpCheckStates = New tmpCheckStates
                End If
                Return _tmpCheckStates
            End Get
            Set(ByVal value As tmpCheckStates)
                _tmpCheckStates = value
            End Set
        End Property
        Public ReadOnly Property HistoriesCol() As Histories
            Get
                Return _Histories
            End Get
        End Property
        Public Property Visitdate() As DateTime
            Get
                Return _VisitDate
            End Get
            Set(ByVal value As DateTime)
                _VisitDate = value
            End Set
        End Property
        Public Property TempDate() As DateTime
            Get
                Return _TempDate
            End Get
            Set(ByVal value As DateTime)
                _TempDate = value
            End Set
        End Property
        Public Property PrescriptionCol() As Prescriptions
            Get
                If IsNothing(_Prescriptions) Then ' code added by sagar
                    _Prescriptions = New Prescriptions

                End If
                Return _Prescriptions
            End Get
            Set(ByVal value As Prescriptions)
                _Prescriptions = value
            End Set
        End Property


        Public Property PrescriptionCol_SelectedPBM() As Prescriptions
            Get
                If IsNothing(_Prescriptions_SelectedPBM) Then ' code added by sagar
                    _Prescriptions_SelectedPBM = New Prescriptions

                End If
                Return _Prescriptions_SelectedPBM
            End Get
            Set(ByVal value As Prescriptions)
                _Prescriptions_SelectedPBM = value
            End Set
        End Property

        'RxHub Formularys Collection

        Public Property TransactionMode() As _TransactionMode
            Get
                Return eTransactionMode
            End Get
            Set(ByVal value As _TransactionMode)
                eTransactionMode = value
            End Set
        End Property
        Public Property TempVisitID() As Int64
            Get
                Return _TempVisitID
            End Get
            Set(ByVal value As Int64)
                _TempVisitID = value
            End Set
        End Property

        Public Property CurrentVisitID() As Int64
            Get
                Return _intCurrentVisitID
            End Get
            Set(ByVal value As Int64)
                _intCurrentVisitID = value
            End Set
        End Property

        Public Property PastVisitID() As Int64
            Get
                Return _intPastVisitID
            End Get
            Set(ByVal value As Int64)
                _intPastVisitID = value
            End Set
        End Property
        Public Property ProviderID() As Int64
            Get
                Return _ProviderId
            End Get
            Set(ByVal value As Int64)
                _ProviderId = value
            End Set
        End Property

        Dim _PatientProviderId As Int64
        Public Property PatientProviderId() As Int64
            Get
                Return _PatientProviderId
            End Get
            Set(ByVal value As Int64)
                _PatientProviderId = value
            End Set
        End Property

        Public Property PharmacyId() As Int64
            Get
                Return _PharmacyId
            End Get
            Set(ByVal value As Int64)
                _PharmacyId = value
            End Set
        End Property
        Public Property OldPharmacyId() As Int64
            Get
                Return _oldPharmacyID
            End Get
            Set(ByVal value As Int64)
                _oldPharmacyID = value
            End Set
        End Property
        Public Property OldProviderID() As Int64
            Get
                Return _oldProviderID
            End Get
            Set(ByVal value As Int64)
                _oldProviderID = value
            End Set
        End Property
        Public Property SupervisingProviderID() As Int64
            Get
                Return _SupervisingProviderID
            End Get
            Set(ByVal value As Int64)
                _SupervisingProviderID = value
            End Set
        End Property
        Public blnIncludeOTCDrugs As Boolean = False
        Public blnIsEPCSEnable As Boolean = False
        Public bDiscardAllclick As Boolean = False
#End Region
#Region "EPCS Request Header Property"
        Public Property OrganizationName() As String
        Public Property OrganizationLabel() As String
        Public Property VendorName() As String
        Public Property VendorLabel() As String
        Public Property VendorNodeLabel() As String
        Public Property VendorNodeName() As String
        Public Property AppName() As String
        Public Property ApplicationVersion() As String
        Public Property SourceOrganizationId() As String
        Public Property HeaderDate() As Date
        Public Property EpcsGoldUrl() As String
        Public Property SharedSecretKey() As String
#End Region

        Private Sub InitialiseFonts()
            SELECTIONFONT_BOLD = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            SELECTIONFONT_REGULAR = New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
            SELECTIONFONT_BOLD_UNDERLINE = New System.Drawing.Font("Tahoma", 11.0F, System.Drawing.FontStyle.Bold + System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CByte(0))

        End Sub

        Private Sub DisposeFonts()
            If Not SELECTIONFONT_BOLD_UNDERLINE Is Nothing Then
                SELECTIONFONT_BOLD_UNDERLINE.Dispose()
                SELECTIONFONT_BOLD_UNDERLINE = Nothing
            End If
            If Not SELECTIONFONT_BOLD Is Nothing Then
                SELECTIONFONT_BOLD.Dispose()
                SELECTIONFONT_BOLD = Nothing
            End If
            If Not SELECTIONFONT_REGULAR Is Nothing Then
                SELECTIONFONT_REGULAR.Dispose()
                SELECTIONFONT_REGULAR = Nothing
            End If
        End Sub
        Public Sub UpdateGrid(ByVal eRxStatus As String, ByVal _eRxStatusMessage As String, ByVal eRx_DrgNDCCode As String, ByVal ItemNumber As Integer, ByVal IseRxed As Integer)
            RaiseEvent UpdateRx_eRxStatus(eRxStatus, _eRxStatusMessage, eRx_DrgNDCCode, ItemNumber, IseRxed)
        End Sub

        Public Function CheckIsNarcotics(ByVal DrugId As Int64) As Boolean
            Try
                For i As Int32 = 0 To _Prescriptions.Count - 1
                    If _Prescriptions.Item(i).DrugID = DrugId Then
                        If _Prescriptions.Item(i).IsNarcotics > 0 Then
                            Return True
                            'Exit Function
                        End If
                    End If
                Next
                Return False
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                CheckIsNarcotics = Nothing
                Throw objex
            End Try

        End Function

        Public Function GetClinicName() As String
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select sClinicName from Clinic_Mst"
                Dim ClinicName As String = ogloEMRDatabase.GetDataValue(_strSQl, False)

                If IsDBNull(ClinicName) Then
                    Return ""
                Else
                    Return ClinicName
                End If
            Catch ex As Exception
                Return ""
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function


        Public Function Get271Information(ByVal _npatientID As Long) As DataTable
            '''''Move sql query to SP
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim objParameter As DBParameter = Nothing
            Dim eligibilityInfo As DataTable = Nothing
            Dim CPOEFlag As Boolean = False
            Try
                objParameter = New DBParameter
                objParameter.Value = _npatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing
                eligibilityInfo = _gloEMRDatabase.GetDataTable("gsp_getRxEligibilityInfo")
                If Not IsNothing(eligibilityInfo) Then
                    If eligibilityInfo.Rows.Count > 0 Then
                        Return eligibilityInfo
                    End If
                End If
                Return eligibilityInfo
            Catch ex As Exception
                Dim objex As New PrescriptionException
                Throw objex
            Finally
                If clsgeneral._EMRConnectionString.Any() Then
                    DataBaseLayer.ConnectionString = clsgeneral._EMRConnectionString
                End If
                If Not IsNothing(objParameter) Then
                    objParameter = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function getClinicLogo() As System.Drawing.Image
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "Select imgClinicLogo from clinic_mst"

                Dim objImage As Object = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsDBNull(objImage) OrElse IsNothing(objImage) Then
                    Return Nothing
                Else
                    Dim arrPicture() As Byte = CType(objImage, Byte())
                    Dim ms As New System.IO.MemoryStream(arrPicture)
                    Dim imgCliniclogo As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                    Try
                        ms.Close()
                        If Not IsNothing(ms) Then
                            ms.Dispose()
                            ms = Nothing
                        End If
                    Catch ex As Exception

                    End Try

                    If IsNothing(imgCliniclogo) Then
                        Return Nothing
                    Else
                        Return imgCliniclogo
                    End If

                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving Clinic Logo"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function FetchDrugDetailsByNDC(ByVal NDCCode As String) As Prescription
            Dim obj As Prescription = Nothing
            Try
                obj = FetchDrugDetailsByMpidOrNdc(0, NDCCode, 0)
                '' Check for alternative NDC for similar mpid & get the details
                If IsNothing(obj) Then
                    Dim mpid As Int32 = 0
                    Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        mpid = oDIBHelper.GetMarketedProductId(NDCCode)
                    End Using
                    obj = FetchDrugDetailsByMpidOrNdc(mpid, NDCCode, 0)
                End If
                Return obj
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            End Try
        End Function

        Public Function GetPatientVitals(ByVal _nPatientId As Long) As DataTable
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "SELECT nVitalID AS VitalID, nVisitID AS VisitID , nPatientID AS PatientID , dtVitalDate AS VitalDate , isnull(dWeightinlbs,'0') AS Weightinlbs , isnull(dWeightinKg,'0') AS WeightinKg FROM Vitals where nPatientID = " & _nPatientId & " order by dtvitaldate desc"
                Dim dtPatVitals As DataTable = ogloEMRDatabase.GetDataTable_Query(_strSQl)
                Return dtPatVitals
            Catch ex As Exception
                Dim objex As New PrescriptionException
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function GetDIOverrideReason() As DataTable
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try

                Dim _strSQl As String = ""
                _strSQl = "SELECT  nCategoryid nDIReasonID,sDescription sDIReason,nClinicID,sCode FROM category_mst WHERE UPPER(sCategoryType) ='DI Override Reason' AND bIsBlocked = '" & False & "' AND nClinicID = 1 order by sDescription "

                Dim dtDIReason As DataTable = ogloEMRDatabase.GetDataTable_Query(_strSQl)

                Return dtDIReason

            Catch ex As Exception
                Dim objex As New PrescriptionException
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function GetDrugIdsforPrint(ByVal dtPrescriptiondate As DateTime, Optional ByVal blnIsFax As Boolean = False) As String
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = dtPrescriptiondate
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@RxDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _PatientID 'globalPatient.gnPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                objParameter = New DBParameter
                If blnIsFax Then
                    objParameter.Value = 1
                Else
                    objParameter.Value = 0
                End If

                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Bit
                objParameter.Name = "@IsFax"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                Dim DrugIDs As String = ""
                Dim NarcoticDrugIDs As String = ""
                dt = _gloEMRDatabase.GetDataTable("GetDrugIdsToPrint")
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        If blnIsFax = False Then
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                DrugIDs = DrugIDs & CType(dt.Rows(i)(2), String)
                                If i < dt.Rows.Count - 1 Then
                                    DrugIDs = DrugIDs & ","
                                End If
                            Next
                        Else
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                If CType(dt.Rows(i)(1), Long) > 0 Then
                                    If NarcoticDrugIDs <> "" Then
                                        NarcoticDrugIDs = NarcoticDrugIDs & "," & CType(dt.Rows(i)(2), String)
                                    Else
                                        NarcoticDrugIDs = CType(dt.Rows(i)(2), String)
                                    End If
                                Else
                                    If DrugIDs <> "" Then
                                        DrugIDs = DrugIDs & "," & CType(dt.Rows(i)(2), String)
                                    Else
                                        DrugIDs = CType(dt.Rows(i)(2), String)
                                    End If
                                End If

                            Next
                        End If
                        If NarcoticDrugIDs.Length > 0 Then
                            DrugIDs = DrugIDs & "|" & NarcoticDrugIDs
                        End If
                        Return DrugIDs
                    Else
                        Return ""
                    End If

                Else
                    Return ""
                End If

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing

            Finally
                'objPrescription = Nothing
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        ''added in 6030 to get prescription of checked for bug 11452
        Public Function GetPrescriptionIdsforPrint(ByVal dtPrescriptiondate As DateTime, Optional ByVal blnIsFax As Boolean = False) As String
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim dt As DataTable = Nothing
            Try
                objParameter = New DBParameter
                objParameter.Value = dtPrescriptiondate
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@RxDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _PatientID 'globalPatient.gnPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                objParameter = New DBParameter
                If blnIsFax Then
                    objParameter.Value = 1
                Else
                    objParameter.Value = 0
                End If

                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Bit
                objParameter.Name = "@IsFax"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter and then
                objParameter = Nothing

                Dim DrugIDs As String = ""
                Dim NarcoticDrugIDs As String = ""

                dt = _gloEMRDatabase.GetDataTable("GetPrescriptionIdsToPrint")

                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        If blnIsFax = False Then
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                'DrugIDs = DrugIDs & CType(dt.Rows(i)(2), String)
                                DrugIDs = DrugIDs & CType(dt.Rows(i)(0), String)
                                If i < dt.Rows.Count - 1 Then
                                    DrugIDs = DrugIDs & ","
                                End If
                            Next
                        Else
                            For i As Int32 = 0 To dt.Rows.Count - 1
                                If CType(dt.Rows(i)(1), Long) > 0 Then
                                    If NarcoticDrugIDs <> "" Then
                                        NarcoticDrugIDs = NarcoticDrugIDs & "," & CType(dt.Rows(i)(2), String)
                                    Else
                                        NarcoticDrugIDs = CType(dt.Rows(i)(2), String)
                                    End If
                                Else
                                    If DrugIDs <> "" Then
                                        DrugIDs = DrugIDs & "," & CType(dt.Rows(i)(2), String)
                                    Else
                                        DrugIDs = CType(dt.Rows(i)(2), String)
                                    End If
                                End If

                            Next
                        End If
                        If NarcoticDrugIDs.Length > 0 Then
                            DrugIDs = DrugIDs & "|" & NarcoticDrugIDs
                        End If
                        Return DrugIDs
                    Else
                        Return ""
                    End If

                Else
                    Return ""
                End If

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing

            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function GetDrugIDByNDC(ByVal NDCCode As String) As Int64

            Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select isnull(nDrugsID,0) from Drugs_Mst where sNDCCode = '" & NDCCode & "'"
                Dim nDrugID As Int64 = 0
                nDrugID = ogloEMRDatabase.GetDataValue(_strSQl, False)

                If IsDBNull(nDrugID) Then
                    Return 0
                Else
                    Return nDrugID
                End If
            Catch ex As Exception

                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
                Return 0
                'Dim objex As New PrescriptionException
                'objex.ErrMessage = ""
                'Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function

        ''created for print issue from refill of medication ///same function write in clsgloEMRPrescription.vb So make same changes for that also 
        Public Function GetDrugIDFromSig(ByVal DrugName As String, ByVal Dosage As String, ByVal Route As String, ByVal Frequency As String, ByVal Duration As String) As Int64
            Dim _gloEMRDatabase As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
            Dim DrugID As Int64 = 0
            Try
                Dim _strsql As String = ""
                _strsql = "select isnull(nDrugsID,0) from drugs_Mst where sDrugname = '" & DrugName & "' and (sDosage = '" & Dosage & "' or sDosage IS NULL) and (sRoute = '" & Route & "'or sRoute IS NULL) and (sFrequency = '" & Frequency & "' or sFrequency IS NULL ) and ( sDuration = '" & Duration & "' or sDuration IS NULL )"

                Dim dt As DataTable = _gloEMRDatabase.GetDataTable_Query(_strsql)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        DrugID = CType(dt.Rows(0)(0), Int64)
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                If IsDBNull(DrugID) Then
                    Return 0
                Else
                    Return DrugID
                End If
            Catch ex As Exception
                Return 0
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        ''created for print issue from refill of medication
        Public Function GetDrugIdBympid(ByVal mpid As Int32) As Int64

            Dim ogloEMRDatabase As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select isnull(nDrugsID,0) from Drugs_Mst where mpid = " & mpid.ToString()
                Dim nDrugID As Int64 = 0
                nDrugID = ogloEMRDatabase.GetDataValue(_strSQl, False)

                If IsDBNull(nDrugID) Then
                    Return 0
                Else
                    Return nDrugID
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
                Return 0
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function GetDosageformCode(ByVal ndc As String) As String
            Dim routeDescription As String = String.Empty
            Try
                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    routeDescription = oDIBGSHelper.GetDrugForm(ndc)
                End Using
            Catch
                routeDescription = ""
            End Try
            Return routeDescription
        End Function



        Public Function GetRxType(ByVal mpid As Int32) As String
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select TOP 1 RxType from Drugs_Mst where mpid = '" & Convert.ToString(mpid) & "'"
                Dim sReturned As String = ""
                sReturned = ogloEMRDatabase.GetDataValue(_strSQl, False)

                If IsDBNull(sReturned) Then
                    Return ""
                Else
                    Return sReturned
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function GetPharmacyNPIFromNCPDID(ByVal NCPDPID As String) As String
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select  ISNULL(sPharmacyNPI,'')  from Contacts_mst where sContactType='Pharmacy' and sNCPDPID = '" & NCPDPID & "'"
                Dim PharmacyNPI As String = ""
                PharmacyNPI = ogloEMRDatabase.GetDataValue(_strSQl, False)

                If IsDBNull(PharmacyNPI) Then
                    Return ""
                Else
                    Return PharmacyNPI
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Public Function GetNCPDPID_PharmStatus(ByVal PharmacyId As String) As String
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strGetNCPDPIDPharmStatSQl = "SELECT ( isnull(sNCPDPID,'') + '|' + isnull(sPharmacyStatus,'')) as ExtCodePhStatus FROM Contacts_MST where nContactID = " & PharmacyId
                Dim NCPDPID_PharmStatus As String = ogloEMRDatabase.GetDataValue(_strGetNCPDPIDPharmStatSQl, False)

                If IsNothing(NCPDPID_PharmStatus) Then
                    _strGetNCPDPIDPharmStatSQl = "SELECT     ( isnull(Contacts_MST.sNCPDPID,'') + '|' + isnull(Contacts_MST.sPharmacyStatus,'')) as ExtCodePhStatus " _
                                                & " FROM  Contacts_MST INNER JOIN " _
                                                & " Patient_DTL ON Patient_DTL.sNCPDPID = Contacts_MST.sNCPDPID WHERE     Patient_DTL.nPatientID = " & _PatientID.ToString()
                    NCPDPID_PharmStatus = ogloEMRDatabase.GetDataValue(_strGetNCPDPIDPharmStatSQl, False)
                    Return NCPDPID_PharmStatus
                Else
                    Return NCPDPID_PharmStatus
                End If



            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Sub getDrugSummaryInfo(ByVal objDrugSummaryInfo As Drug, ByVal Refillqty As String)
            Try
                If Not IsNothing(oOldgloPrescription) Then
                    objDrugSummaryInfo.DrugsName = oOldgloPrescription.DrugsCol.Item(0).DrugName
                    objDrugSummaryInfo.Dosage = oOldgloPrescription.DrugsCol.Item(0).DrugFrequency
                    objDrugSummaryInfo.Amount = oOldgloPrescription.DrugsCol.Item(0).DrugQuantity
                    objDrugSummaryInfo.Duration = Refillqty
                    objDrugSummaryInfo.Route = oOldgloPrescription.DrugsCol.Item(0).RefillsQualifier
                End If

            Catch ex As Exception

            End Try

        End Sub

        Private Function getRxRefillRequestbyTransactionid(ByVal nRxTranasactionId As Long) As Prescription
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer

            Dim objPrescription As New Prescription

            Try
                Dim dt As DataTable 'SLR: new not needed
                Dim _strGetRefillRxSQl = "select p.nPatientID as 'PatientID',isnull(p.sMedication,'') as Medication,isnull(p.sDosage,'') as Dosage,isnull(p.sRoute,'') as Route,isnull(p.nDrugID,0) as DrugID,ISNULL(p.sNDCCode,'') as NDCCode,ISNULL(p.mpid,0)as mpid,isnull(p.sUserName,'') as UserName,isnull(p.sChiefComplaints,'') as ChiefComplaints,isnull(d.nIsNarcotics,0) as IsNarcotics,isnull(p.sDrugForm,'') as DrugForm,isnull(p.ProblemID,'') as ProblemID from prescription p inner join drugs_mst d on p.mpid=d.mpid where nPrescriptionId = " & nRxTranasactionId

                dt = _gloEMRDatabase.GetDataTable_Query(_strGetRefillRxSQl)

                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then

                        objPrescription.PatientID = dt.Rows(0)("PatientID")
                        objPrescription.Medication = dt.Rows(0)("Medication")
                        objPrescription.Dosage = dt.Rows(0)("Dosage")
                        objPrescription.Route = dt.Rows(0)("Route")
                        objPrescription.DosageForm = dt.Rows(0)("DrugForm")

                        ''For Resolving case no GLO2011-0013746  i.e eRx Refill Request Issues
                        objPrescription.NDCCode = dt.Rows(0)("NDCCode")
                        objPrescription.mpid = dt.Rows(0)("mpid")
                        objPrescription.DrugID = dt.Rows(0)("DrugID")
                        ''For Resolving Problem 00000208
                        ''Start
                        'objPrescription.UserName = dt.Rows(0)("UserName")
                        objPrescription.UserName = globalSecurity.gstrLoginName
                        'End
                        objPrescription.ChiefComplaint = dt.Rows(0)("ChiefComplaints")
                        objPrescription.Problems = dt.Rows(0)("ProblemID")
                        objPrescription.IsNarcotics = dt.Rows(0)("IsNarcotics")
                        objPrescription.Status = "Approved"
                        objPrescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName
                        'objPrescription.RefillQuantity = RxRefillRequestQty
                        objPrescription.State = "A"
                        'objPrescriptions.Add(objPrescription) 'SLR: not used
                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
                Return objPrescription

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching prescription for refill."
                Throw objex
                Return Nothing
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try

        End Function


        Public Function GetProviderIDForUser(ByVal UserID As Int64) As Boolean
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim objParameter As DBParameter = Nothing
            Dim dt As DataTable = Nothing
            Dim CPOEFlag As Boolean = False
            Try
                objParameter = New DBParameter
                objParameter.Value = UserID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@UserID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: FRee objParameter
                objParameter = Nothing
                dt = _gloEMRDatabase.GetDataTable("gsp_IsCPOEOrder")
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        CPOEFlag = If(dt.Rows(0)(0) = 0, True, False)

                    End If
                    dt.Dispose()
                    dt = Nothing
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(objParameter) Then
                    objParameter = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
            Return CPOEFlag
        End Function

        Public Function RetriveDrugDetailsBySigID(ByVal sigID As Long) As DataTable
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing 'SLR: new is not needed

            Try
                objParameter = New DBParameter
                objParameter.Value = sigID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@SIGID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_ScanDrugProvAssociation")
                If Not IsNothing(dt) Then
                    If dt.Rows.Count <= 0 Then
                        dt.Dispose()
                        dt = Nothing
                        Return Nothing
                    End If
                End If
                Return dt
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function RetriveDrugDetails(ByVal mpID As Int32, ByVal ndc As String) As DataTable
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing 'SLR: new is not needed

            Try
                objParameter = New DBParameter
                objParameter.Value = mpID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@mpid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = ndc
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@NDCCode"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                'SLR: Free objParameter
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_ScanDRUGS_MST_NDCCode")
                ''For Resolving case no GLO2011-0013746  i.e eRx Refill Request Issues
                If Not IsNothing(dt) Then
                    If dt.Rows.Count <= 0 Then
                        'SLR: Free dt
                        dt.Dispose()
                        dt = Nothing
                        Return Nothing
                    End If
                End If
                Return dt
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function FetchDrugDetailsByMpidOrNdc(ByVal mpid As Int32, ByVal NDCCode As String, ByVal SIGid As Long) As Prescription

            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing
            Try
                If SIGid = 0 Then
                    dt = RetriveDrugDetails(mpid, NDCCode)
                Else
                    dt = RetriveDrugDetailsBySigID(SIGid)
                End If

                If IsNothing(dt) Then
                    Return objPrescription
                End If

                objPrescription = New Prescription
                objPrescription.DrugID = dt.Rows(0)("nDrugsID")
                objPrescription.Medication = dt.Rows(0)("sDrugName")
                objPrescription.Dosage = dt.Rows(0)("sDosage")
                objPrescription.Route = dt.Rows(0)("sRoute")
                objPrescription.Frequency = dt.Rows(0)("sFrequency")
                objPrescription.Duration = dt.Rows(0)("sDuration")
                objPrescription.UserName = globalSecurity.gstrLoginName
                objPrescription.IsNarcotics = dt.Rows(0)("IsNarcotics")
                objPrescription.mpid = dt.Rows(0)("mpid")
                objPrescription.NDCCode = dt.Rows(0)("NDCCode")
                objPrescription.RxType = dt.Rows(0)("DrugType")

                If SIGid = 0 Then
                    objPrescription.Refills = "0"
                Else
                    objPrescription.Refills = dt.Rows(0)("sRefills")
                End If

                If IsNothing(objPrescription.DosageForm) Then
                    objPrescription.DosageForm = dt.Rows(0)("DrugForm")
                ElseIf objPrescription.DosageForm = "" Then
                    objPrescription.DosageForm = dt.Rows(0)("DrugForm")
                End If

                If IsNothing(objPrescription.DosageForm) Or objPrescription.DosageForm = "" Then
                    objPrescription.DosageForm = GetDosageformCode(objPrescription.NDCCode)
                End If

                objPrescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                objPrescription.State = "A"

                Dim amt As String = Convert.ToString(dt.Rows(0)("sAmount")).Trim()
                Dim potencyCode As String = dt.Rows(0)("spotencycode")
                Dim potencyUnit As String = dt.Rows(0)("sPotencyUnit")

                objPrescription.PotencyCode = potencyCode
                objPrescription.PotencyUnit = potencyUnit

                objPrescription.AlternativeFormId = dt.Rows(0)("AlternativeFormID")

                If Not String.IsNullOrWhiteSpace(amt) Then
                    If Not IsNumeric(amt) Then
                        Dim qty As String() = amt.Split(" ")
                        If (qty.Length >= 1) Then
                            amt = qty(0)
                        End If
                    End If
                    objPrescription.Amount = amt + " " + potencyUnit
                End If

                Return objPrescription

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Function

        Private Function FetchDrugDetailsRevised(ByVal drugList As ArrayList, ByVal visitID As Int64, ByVal providerID As Int64, ByVal pharmacyID As Int64, ByVal epcsEnabled As Boolean) As List(Of Prescription)

            Dim prescriptions As New List(Of Prescription)
            Dim dtDefaultPhDetails As DataTable = Nothing
            Dim dt As DataTable = Nothing

            If pharmacyID <> 0 Then
                dtDefaultPhDetails = GetPharmacyDetails(pharmacyID)
            End If

            Try
                If Not IsNothing(drugList) Then

                    For Each drug As Object In drugList
                        If TypeOf (drug) Is gloEMRActors.Drug Then
                            Dim _drug As gloEMRActors.Drug = DirectCast(drug, gloEMRActors.Drug)
                            dt = RetriveDrugDetails(_drug.mpid, _drug.NDCCode)
                        Else
                            Dim _drug As Int32 = DirectCast(drug, Int32)
                            dt = RetriveDrugDetails(drug, "")
                        End If

                        Using objPrescription As New Prescription
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objPrescription.Medication = dt.Rows(0)("sDrugName")
                                objPrescription.RxType = dt.Rows(0)("DrugType")
                                objPrescription.Dosage = dt.Rows(0)("sDosage")
                                objPrescription.Route = dt.Rows(0)("sRoute")
                                objPrescription.Frequency = dt.Rows(0)("sFrequency")
                                objPrescription.Duration = dt.Rows(0)("sDuration")
                                objPrescription.Amount = dt.Rows(0)("sAmount")
                                objPrescription.DrugID = dt.Rows(0)("nDrugsID")
                                objPrescription.NDCCode = dt.Rows(0)("NDCCode")
                                objPrescription.mpid = dt.Rows(0)("mpid")
                                objPrescription.DosageForm = dt.Rows(0)("DrugForm")
                                objPrescription.StrengthUnit = dt.Rows(0)("sDrugQtyQualifier")
                                objPrescription.IsNarcotics = dt.Rows(0)("IsNarcotics")
                                objPrescription.ProviderID = providerID
                                objPrescription.Refills = "0"
                                objPrescription.Maysubstitute = True
                                objPrescription.State = "A"
                                objPrescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                                objPrescription.UserName = globalSecurity.gstrLoginName
                                objPrescription.VisitID = visitID
                                objPrescription.Startdate = Now.Date
                                objPrescription.PrescriptionID = 0
                                objPrescription.IseRxed = 0
                            End If
                            objPrescription.PharmacyID = pharmacyID '''''assing the pharmacy details retrieved in the dtDefaultPhDetails
                            objPrescription.PhContactID = pharmacyID ''''is same as contact id

                            Dim amt As String = Convert.ToString(dt.Rows(0)("sAmount")).Trim()
                            Dim potencyCode As String = dt.Rows(0)("spotencycode")
                            Dim potencyUnit As String = dt.Rows(0)("sPotencyUnit")

                            objPrescription.PotencyCode = potencyCode
                            objPrescription.PotencyUnit = potencyUnit

                            If Not String.IsNullOrWhiteSpace(amt) Then
                                If Not IsNumeric(amt) Then
                                    Dim qty As String() = amt.Split(" ")
                                    If (qty.Length >= 1) Then
                                        amt = qty(0)
                                    End If
                                End If
                                objPrescription.Amount = amt + " " + potencyUnit
                            End If

                            objPrescription.AlternativeFormId = dt.Rows(0)("AlternativeFormID")

                            If Not IsNothing(dtDefaultPhDetails) Then
                                If dtDefaultPhDetails.Rows.Count > 0 Then
                                    objPrescription.PharmacyName = dtDefaultPhDetails.Rows(0)("sName")
                                    objPrescription.PhAddressline1 = dtDefaultPhDetails.Rows(0)("sAddressLine1")
                                    objPrescription.PhAddressline2 = dtDefaultPhDetails.Rows(0)("sAddressLine2")
                                    objPrescription.PhCity = dtDefaultPhDetails.Rows(0)("sCity")
                                    objPrescription.PhState = dtDefaultPhDetails.Rows(0)("sState")
                                    objPrescription.PhZip = dtDefaultPhDetails.Rows(0)("sZIP")
                                    objPrescription.PhPhone = dtDefaultPhDetails.Rows(0)("sPhone")
                                    objPrescription.PhFax = dtDefaultPhDetails.Rows(0)("sFax")
                                    objPrescription.PhEmail = dtDefaultPhDetails.Rows(0)("sEmail")
                                    objPrescription.PhServiceLevel = dtDefaultPhDetails.Rows(0)("sServiceLevel")
                                    objPrescription.PhNCPDPID = dtDefaultPhDetails.Rows(0)("sNCPDPID")
                                End If
                            End If

                            If objPrescription.IsNarcotics > 0 Then
                                If epcsEnabled = False Then   '' Check For EPCS Enabled
                                    objPrescription.Method = "Print"
                                Else
                                    objPrescription.Method = "eRx"
                                End If
                            Else
                                If objPrescription.PhNCPDPID <> "" Then
                                    objPrescription.Method = "eRx"
                                Else
                                    objPrescription.Method = "Print"
                                End If
                            End If

                            If Not objPrescription.Medication = "" Then
                                prescriptions.Add(objPrescription)
                            End If
                        End Using
                    Next
                End If
                Return prescriptions
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(dtDefaultPhDetails) Then
                    dtDefaultPhDetails.Dispose()
                    dtDefaultPhDetails = Nothing
                End If
            End Try
        End Function

        Public Function FetchRefillDrugDetailsRevised(ByVal pharmacyID As Int64, ByVal mpid As Int32, ByVal ndc As String) As Prescription
            Dim dtDefaultPhDetails As DataTable = Nothing
            Dim objPrescription As New Prescription()

            If pharmacyID <> 0 Then
                dtDefaultPhDetails = GetPharmacyDetails(pharmacyID)
            End If

            Try
                If Not IsNothing(ndc) Then
                    Using dt = RetriveDrugDetails(mpid, ndc)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            objPrescription.Medication = dt.Rows(0)("sDrugName")
                            objPrescription.RxType = dt.Rows(0)("DrugType")
                            objPrescription.DrugID = dt.Rows(0)("nDrugsID")
                            objPrescription.mpid = dt.Rows(0)("mpid")
                            objPrescription.IsNarcotics = dt.Rows(0)("IsNarcotics")
                            objPrescription.NDCCode = dt.Rows(0)("NDCCode")
                            objPrescription.Dosage = dt.Rows(0)("sDosage")
                            objPrescription.AlternativeFormId = dt.Rows(0)("AlternativeFormID")

                            Dim potencyCode As String = dt.Rows(0)("spotencycode")
                            Dim potencyUnit As String = dt.Rows(0)("sPotencyUnit")

                            objPrescription.PotencyCode = potencyCode
                            objPrescription.PotencyUnit = potencyUnit

                            If Not IsNothing(dtDefaultPhDetails) Then
                                If dtDefaultPhDetails.Rows.Count > 0 Then
                                    objPrescription.PharmacyID = pharmacyID
                                    objPrescription.PhContactID = dtDefaultPhDetails.Rows(0)("nContactID")
                                    objPrescription.PharmacyName = dtDefaultPhDetails.Rows(0)("sName")
                                    objPrescription.PhAddressline1 = dtDefaultPhDetails.Rows(0)("sAddressLine1")
                                    objPrescription.PhAddressline2 = dtDefaultPhDetails.Rows(0)("sAddressLine2")
                                    objPrescription.PhCity = dtDefaultPhDetails.Rows(0)("sCity")
                                    objPrescription.PhState = dtDefaultPhDetails.Rows(0)("sState")
                                    objPrescription.PhZip = dtDefaultPhDetails.Rows(0)("sZIP")
                                    objPrescription.PhPhone = dtDefaultPhDetails.Rows(0)("sPhone")
                                    objPrescription.PhFax = dtDefaultPhDetails.Rows(0)("sFax")
                                    objPrescription.PhEmail = dtDefaultPhDetails.Rows(0)("sEmail")
                                    objPrescription.PhServiceLevel = dtDefaultPhDetails.Rows(0)("sServiceLevel")
                                    objPrescription.PhNCPDPID = dtDefaultPhDetails.Rows(0)("sNCPDPID")
                                End If
                            End If
                        End If
                    End Using
                End If
                Return objPrescription
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
            Catch e As Exception
                Return Nothing
            Finally
                If Not IsNothing(dtDefaultPhDetails) Then
                    dtDefaultPhDetails.Dispose()
                    dtDefaultPhDetails = Nothing
                End If
            End Try
        End Function
        Public Function getRxChangeRequest(ByVal TransactionID As Long, ByVal ChangeRequestPharmacyID As Long, ByVal medReq As gloGlobal.Schemas.Surescript.RxChangePrescribedMedicationType) As Boolean
            Dim objPrescription As Prescription = Nothing
            Dim helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()

            '' Get the prescription based on the TransactionID
            'If changeRequest.TransactionID <> 0 Then
            '    objPrescription = helper.GetPrescriptionByTransactionID(changeRequest.TransactionID)
            'End If

            _RxTransactionID = TransactionID

            '' Get the prescription based on the NDC if not found by transactionID
            If medReq.DrugCoded IsNot Nothing AndAlso medReq.DrugCoded.ProductCode IsNot Nothing Then
                If IsNothing(objPrescription) AndAlso Not String.IsNullOrWhiteSpace(medReq.DrugCoded.ProductCode) Then
                    objPrescription = FetchDrugDetailsByNDC(medReq.DrugCoded.ProductCode)
                End If
            End If


            If IsNothing(objPrescription) Then
                gloSurescriptGeneral.InformationMessage("No Prescription available for this Change Request")
                getRxChangeRequest = Nothing
                Exit Function
            End If

            objPrescription.PatientID = _PatientID
            'objPrescription.PrescriptionID = changeRequest.TransactionID
            objPrescription.VisitID = Me._intCurrentVisitID   'Set VisitId as passed from Visit form
            objPrescription.ProviderID = Me.ProviderID
            objPrescription.PharmacyID = ChangeRequestPharmacyID

            objPrescription.Startdate = Now.Date

            If objPrescription.IsNarcotics > 1 And blnIsEPCSEnable = True Then
                objPrescription.RefillQuantity = "0"

                If medReq.Refills IsNot Nothing Then
                    If medReq.Refills.Value IsNot Nothing AndAlso medReq.Refills.Value.Trim = "" Then
                        objPrescription.RefillQuantity = medReq.Refills.Value
                    End If

                    If medReq.Refills.Qualifier IsNot Nothing AndAlso medReq.Refills.Qualifier.Trim <> "" Then
                        If medReq.Refills.Qualifier.Trim.ToUpper = "PRN" Then  '' Do not Send PRN For CS Drug 
                            objPrescription.RefillQualifier = "R"
                        Else
                            objPrescription.RefillQualifier = medReq.Refills.Qualifier
                        End If
                    End If
                End If
            Else
                objPrescription.RefillQuantity = "0"

                If medReq.Refills IsNot Nothing Then
                    If medReq.Refills.Value IsNot Nothing Then
                        objPrescription.RefillQuantity = medReq.Refills.Value
                    End If

                    If medReq.Refills.Qualifier IsNot Nothing Then
                        If medReq.Refills.Qualifier.Trim <> "" Then
                            objPrescription.RefillQualifier = medReq.Refills.Qualifier
                        End If
                    End If
                End If
            End If

            If medReq.Substitutions IsNot Nothing AndAlso medReq.Substitutions = "0" Then
                objPrescription.Maysubstitute = True
            Else
                objPrescription.Maysubstitute = False
            End If

            If String.IsNullOrEmpty(objPrescription.PotencyCode) Then
                objPrescription.PotencyCode = medReq.Quantity.PotencyUnitCode
            End If

            objPrescription.Refills = objPrescription.RefillQuantity

            objPrescription.Frequency = medReq.Directions

            Dim amt As String = String.Empty

            If medReq.Quantity IsNot Nothing AndAlso medReq.Quantity.Value IsNot Nothing Then
                amt = Convert.ToString(medReq.Quantity.Value).Trim()
            End If

            If Not String.IsNullOrWhiteSpace(amt) Then
                If Not IsNumeric(amt) Then
                    Dim qty As String() = amt.Split(" ")
                    If (qty.Length >= 1) Then
                        'objPrescription.Amount = qty(0)
                        amt = qty(0)
                    End If
                End If
                objPrescription.Amount = amt + " " + objPrescription.PotencyUnit
            End If

            If Not IsNothing(objPrescription.Amount) Then
                If objPrescription.Amount <> "" Then
                    If objPrescription.Amount.Contains("Unspecified") Then
                        objPrescription.Amount = objPrescription.Amount.Trim.Replace("Unspecified", "")
                        If objPrescription.PotencyCode = "C38046" Then
                            objPrescription.PotencyCode = ""
                        End If
                    End If
                End If
            End If

            objPrescription.Duration = medReq.DaysSupply
            objPrescription.Notes = medReq.Note

            objPrescription.Status = "Approved"
            objPrescription.MessageType = "RxChangeRequest"

            If objPrescription.IsNarcotics > 0 Then
                If blnIsEPCSEnable = False Then   '' Check For EPCS Enabled
                    objPrescription.Method = "Print"
                Else
                    objPrescription.Method = "eRx"
                End If
            Else
                objPrescription.Method = "eRx"
            End If

            objPrescription.Renewed = "Changed" & " " & Now & " " & globalSecurity.gstrLoginName

            If objPrescription.Duration IsNot Nothing AndAlso objPrescription.Duration.Trim.Length > 0 Then
                If IsNumeric(objPrescription.Duration.Trim) Then
                    objPrescription.Enddate = DateAdd(DateInterval.Day, CType(objPrescription.Duration.Trim, Double), objPrescription.Startdate)
                    objPrescription.CheckEndDate = True
                End If
            End If

            If Me.TransactionMode = _TransactionMode.Edit Then
                objPrescription.Prescriptiondate = Me.PrescriptionDate    'Set date as Old Prescription Date
            End If

            If objPrescription.mpid = 0 Or objPrescription.mpid = Nothing Then
                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    objPrescription.mpid = oDIBHelper.GetMarketedProductId(objPrescription.NDCCode)
                End Using
            End If
            If (Not objPrescription.RxType.Any()) Then
                objPrescription.RxType = Me.GetRxType(objPrescription.mpid)
            End If

            Using dtDefaultPhDetails As DataTable = GetPharmacyDetails(ChangeRequestPharmacyID)
                If Not IsNothing(dtDefaultPhDetails) Then
                    If dtDefaultPhDetails.Rows.Count > 0 Then
                        objPrescription.PharmacyID = ChangeRequestPharmacyID
                        objPrescription.PhContactID = dtDefaultPhDetails.Rows(0)("nContactID")
                        objPrescription.PharmacyName = dtDefaultPhDetails.Rows(0)("sName")
                        objPrescription.PhAddressline1 = dtDefaultPhDetails.Rows(0)("sAddressLine1")
                        objPrescription.PhAddressline2 = dtDefaultPhDetails.Rows(0)("sAddressLine2")
                        objPrescription.PhCity = dtDefaultPhDetails.Rows(0)("sCity")
                        objPrescription.PhState = dtDefaultPhDetails.Rows(0)("sState")
                        objPrescription.PhZip = dtDefaultPhDetails.Rows(0)("sZIP")
                        objPrescription.PhPhone = dtDefaultPhDetails.Rows(0)("sPhone")
                        objPrescription.PhFax = dtDefaultPhDetails.Rows(0)("sFax")
                        objPrescription.PhEmail = dtDefaultPhDetails.Rows(0)("sEmail")
                        objPrescription.PhServiceLevel = dtDefaultPhDetails.Rows(0)("sServiceLevel")
                        objPrescription.PhNCPDPID = dtDefaultPhDetails.Rows(0)("sNCPDPID")
                    End If
                End If
            End Using

            If Not IsNothing(objPrescription) Then
                _Prescriptions.Add(objPrescription)
            End If

            Return True
        End Function

        Public Function getRxChangeRequest(ByVal TransactionID As Long, ByVal ChangeRequestPharmacyID As Long, ByVal medReq As gloGlobal.Schemas.Surescript.RxChangeDispensedMedicationType) As Boolean
            Dim objPrescription As Prescription = Nothing
            Dim helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()

            '' Get the prescription based on the TransactionID
            'If changeRequest.TransactionID <> 0 Then
            '    objPrescription = helper.GetPrescriptionByTransactionID(changeRequest.TransactionID)
            'End If

            _RxTransactionID = TransactionID

            '' Get the prescription based on the NDC if not found by transactionID
            If medReq.DrugCoded IsNot Nothing AndAlso medReq.DrugCoded.ProductCode IsNot Nothing Then
                If IsNothing(objPrescription) AndAlso Not String.IsNullOrWhiteSpace(medReq.DrugCoded.ProductCode) Then
                    objPrescription = FetchDrugDetailsByNDC(medReq.DrugCoded.ProductCode)
                End If
            End If


            If IsNothing(objPrescription) Then
                gloSurescriptGeneral.InformationMessage("No Prescription available for this Change Request")
                getRxChangeRequest = Nothing
                Exit Function
            End If

            objPrescription.PatientID = _PatientID
            'objPrescription.PrescriptionID = changeRequest.TransactionID
            objPrescription.VisitID = Me._intCurrentVisitID   'Set VisitId as passed from Visit form
            objPrescription.ProviderID = Me.ProviderID
            objPrescription.PharmacyID = ChangeRequestPharmacyID

            objPrescription.Startdate = Now.Date

            If objPrescription.IsNarcotics > 1 And blnIsEPCSEnable = True Then
                objPrescription.RefillQuantity = "0"

                If medReq.Refills IsNot Nothing Then
                    If medReq.Refills.Value IsNot Nothing AndAlso medReq.Refills.Value.Trim = "" Then
                        objPrescription.RefillQuantity = medReq.Refills.Value
                    End If

                    If medReq.Refills.Qualifier IsNot Nothing AndAlso medReq.Refills.Qualifier.Trim <> "" Then
                        If medReq.Refills.Qualifier.Trim.ToUpper = "PRN" Then  '' Do not Send PRN For CS Drug 
                            objPrescription.RefillQualifier = "R"
                        Else
                            objPrescription.RefillQualifier = medReq.Refills.Qualifier
                        End If
                    End If
                End If
            Else
                objPrescription.RefillQuantity = "0"

                If medReq.Refills IsNot Nothing Then
                    If medReq.Refills.Value IsNot Nothing Then
                        objPrescription.RefillQuantity = medReq.Refills.Value
                    End If

                    If medReq.Refills.Qualifier IsNot Nothing Then
                        If medReq.Refills.Qualifier.Trim <> "" Then
                            objPrescription.RefillQualifier = medReq.Refills.Qualifier
                        End If
                    End If
                End If
            End If

            If medReq.Substitutions IsNot Nothing AndAlso medReq.Substitutions = "0" Then
                objPrescription.Maysubstitute = True
            Else
                objPrescription.Maysubstitute = False
            End If

            If String.IsNullOrEmpty(objPrescription.PotencyCode) Then
                objPrescription.PotencyCode = medReq.Quantity.PotencyUnitCode
            End If

            objPrescription.Refills = objPrescription.RefillQuantity

            objPrescription.Frequency = medReq.Directions

            Dim amt As String = String.Empty

            If medReq.Quantity IsNot Nothing AndAlso medReq.Quantity.Value IsNot Nothing Then
                amt = Convert.ToString(medReq.Quantity.Value).Trim()
            End If

            If Not String.IsNullOrWhiteSpace(amt) Then
                If Not IsNumeric(amt) Then
                    Dim qty As String() = amt.Split(" ")
                    If (qty.Length >= 1) Then
                        'objPrescription.Amount = qty(0)
                        amt = qty(0)
                    End If
                End If
                objPrescription.Amount = amt + " " + objPrescription.PotencyUnit
            End If

            If Not IsNothing(objPrescription.Amount) Then
                If objPrescription.Amount <> "" Then
                    If objPrescription.Amount.Contains("Unspecified") Then
                        objPrescription.Amount = objPrescription.Amount.Trim.Replace("Unspecified", "")
                        If objPrescription.PotencyCode = "C38046" Then
                            objPrescription.PotencyCode = ""
                        End If
                    End If
                End If
            End If

            objPrescription.Duration = medReq.DaysSupply
            objPrescription.Notes = medReq.Note

            '''''
            Dim sProblems As String = ""
            Dim sChiefComp As String = ""

            If medReq.Diagnosis IsNot Nothing Then

                Using oPrescriptionLayer As New PrescriptionBusinessLayer()
                    Dim dr As DataRow = Nothing

                    If medReq.Diagnosis(0).Primary IsNot Nothing Then
                        dr = oPrescriptionLayer.GetProblemIds(medReq.Diagnosis(0).Primary.Qualifier, medReq.Diagnosis(0).Primary.Value)

                        If dr IsNot Nothing Then
                            sProblems = dr("ProblemID")
                            sChiefComp = dr("sDescription")
                        End If
                    End If

                    If medReq.Diagnosis(0).Secondary IsNot Nothing Then
                        dr = oPrescriptionLayer.GetProblemIds(medReq.Diagnosis(0).Secondary.Qualifier, medReq.Diagnosis(0).Secondary.Value)

                        If dr IsNot Nothing Then
                            sProblems = sProblems & "|" & dr("ProblemID")
                            sChiefComp = sChiefComp & "|" & dr("sDescription")
                        End If
                    End If

                End Using
            End If

            If Not String.IsNullOrEmpty(sProblems) Then
                objPrescription.Problems = sProblems
            End If

            If Not String.IsNullOrEmpty(sChiefComp) Then
                objPrescription.ChiefComplaint = sChiefComp
            End If

            '''''
            objPrescription.Status = "Approved"
            objPrescription.MessageType = "RxChangeRequest"

            If objPrescription.IsNarcotics > 0 Then
                If blnIsEPCSEnable = False Then   '' Check For EPCS Enabled
                    objPrescription.Method = "Print"
                Else
                    objPrescription.Method = "eRx"
                End If
            Else
                objPrescription.Method = "eRx"
            End If

            objPrescription.Renewed = "Changed" & " " & Now & " " & globalSecurity.gstrLoginName

            If objPrescription.Duration IsNot Nothing AndAlso objPrescription.Duration.Trim.Length > 0 Then
                If IsNumeric(objPrescription.Duration.Trim) Then
                    objPrescription.Enddate = DateAdd(DateInterval.Day, CType(objPrescription.Duration.Trim, Double), objPrescription.Startdate).AddDays(If(CType(objPrescription.Duration.Trim, Double) = 0, 0, -1))
                    objPrescription.CheckEndDate = True
                End If
            End If

            If Me.TransactionMode = _TransactionMode.Edit Then
                objPrescription.Prescriptiondate = Me.PrescriptionDate    'Set date as Old Prescription Date
            End If

            If objPrescription.mpid = 0 Or objPrescription.mpid = Nothing Then
                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    objPrescription.mpid = oDIBHelper.GetMarketedProductId(objPrescription.NDCCode)
                End Using
            End If
            If (Not objPrescription.RxType.Any()) Then
                objPrescription.RxType = Me.GetRxType(objPrescription.mpid)
            End If

            Using dtDefaultPhDetails As DataTable = GetPharmacyDetails(ChangeRequestPharmacyID)
                If Not IsNothing(dtDefaultPhDetails) Then
                    If dtDefaultPhDetails.Rows.Count > 0 Then
                        objPrescription.PharmacyID = ChangeRequestPharmacyID
                        objPrescription.PhContactID = dtDefaultPhDetails.Rows(0)("nContactID")
                        objPrescription.PharmacyName = dtDefaultPhDetails.Rows(0)("sName")
                        objPrescription.PhAddressline1 = dtDefaultPhDetails.Rows(0)("sAddressLine1")
                        objPrescription.PhAddressline2 = dtDefaultPhDetails.Rows(0)("sAddressLine2")
                        objPrescription.PhCity = dtDefaultPhDetails.Rows(0)("sCity")
                        objPrescription.PhState = dtDefaultPhDetails.Rows(0)("sState")
                        objPrescription.PhZip = dtDefaultPhDetails.Rows(0)("sZIP")
                        objPrescription.PhPhone = dtDefaultPhDetails.Rows(0)("sPhone")
                        objPrescription.PhFax = dtDefaultPhDetails.Rows(0)("sFax")
                        objPrescription.PhEmail = dtDefaultPhDetails.Rows(0)("sEmail")
                        objPrescription.PhServiceLevel = dtDefaultPhDetails.Rows(0)("sServiceLevel")
                        objPrescription.PhNCPDPID = dtDefaultPhDetails.Rows(0)("sNCPDPID")
                    End If
                End If
            End Using

            If Not IsNothing(objPrescription) Then
                _Prescriptions.Add(objPrescription)
            End If

            Return True
        End Function

        Private Function GetEDrug(ByVal Drug As EPrescription) As EDrug
            Dim returned As EDrug = Nothing
            Try
                If Drug IsNot Nothing AndAlso Drug.DrugsCol IsNot Nothing AndAlso Drug.DrugsCol.Item(0) IsNot Nothing Then
                    returned = DirectCast(Drug.DrugsCol.Item(0), EDrug)
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while loading Refill Request."
                Throw objex
            End Try
            Return returned
        End Function

        Public Function getRxRefillRequest(ByVal nRxTransactionId As Long, ByVal RxRefillQty As String, ByVal dtdatereceived As DateTime, ByVal sReferenceNumber As String, ByVal messageID As String, Optional ByVal NDCCode As String = "") As Boolean
            Dim oRefillRequest As New RefillRequest
            Dim objPrescription As Prescription = Nothing
            Try

                _RxTransactionID = nRxTransactionId

                oRefillRequest.GetDrugDetailsToRefill(messageID, sReferenceNumber, nRxTransactionId)

                oOldgloPrescription = oRefillRequest.ogloPrescription

                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    oOldgloPrescription.DrugsCol.Item(0).mpid = oDIBHelper.GetMarketedProductId(oOldgloPrescription.DrugsCol.Item(0).MDProductCode)
                End Using

                If nRxTransactionId <> 0 Then
                    If oOldgloPrescription.DrugsCol.Item(0).DrugName = oOldgloPrescription.DrugsCol.Item(0).MDDrugName Then
                        objPrescription = getRxRefillRequestbyTransactionid(nRxTransactionId)

                        If IsNothing(objPrescription) Then
                            gloSurescriptGeneral.InformationMessage("No Prescription available for this Refill Request")
                            getRxRefillRequest = Nothing
                            Exit Function
                        Else
                            If objPrescription.Medication = "" Then
                                If NDCCode <> "" Then
                                    objPrescription = FetchDrugDetailsByNDC(NDCCode)

                                    objPrescription.Status = "Approved"
                                    objPrescription.MessageType = "RefillRequest"
                                    objPrescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName
                                    objPrescription.RefillQuantity = RxRefillQty
                                Else
                                    gloSurescriptGeneral.InformationMessage("Drug " & oOldgloPrescription.DrugsCol.Item(0).DrugName & " requested for Refill is not available")
                                    getRxRefillRequest = Nothing
                                    Exit Function
                                End If
                            End If
                            '' End -------------
                        End If
                    Else
                        If NDCCode <> "" Then
                            objPrescription = FetchDrugDetailsByNDC(NDCCode)
                        Else
                            gloSurescriptGeneral.InformationMessage("Drug " & oOldgloPrescription.DrugsCol.Item(0).DrugName & " requested for Refill is not available")
                            getRxRefillRequest = Nothing
                            Exit Function
                        End If
                    End If
                Else
                    ''''code commented becaz we will check with NDC code instead of DrugId
                    'If drugid > 0 Then
                    '    objPrescription = objRxDBLayer.FetchDrugDetails(drugid)
                    'Else
                    '    gloSurescriptGeneral.InformationMessage("Drug " & oOldgloPrescription.DrugsCol.Item(0).DrugName & " requested for Refill is not available")
                    '    Exit Function
                    'End If
                    If NDCCode <> "" Then
                        objPrescription = FetchDrugDetailsByNDC(NDCCode)
                    Else
                        gloSurescriptGeneral.InformationMessage("Drug " & oOldgloPrescription.DrugsCol.Item(0).DrugName & " requested for Refill is not available")
                        getRxRefillRequest = Nothing
                        Exit Function
                    End If
                End If

                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    If oDIBHelper.IsObsolute(NDCCode) = True Then
                        objPrescription.NDCCode = NDCCode
                    End If
                End Using

                objPrescription.PatientID = _PatientID 'globalPatient.gnPatientID
                objPrescription.PrescriptionID = 0
                objPrescription.IseRxed = 0
                objPrescription.VisitID = Me._intCurrentVisitID   'Set VisitId as passed from Visit form
                objPrescription.ProviderID = Me.ProviderID
                objPrescription.PharmacyID = Me.PharmacyId
                objPrescription.Startdate = Now.Date

                If Not IsNothing(oRefillRequest.ogloPrescription) Then
                    ' gloSurescriptGeneral.checkDownloadVersion()
                    If objPrescription.IsNarcotics > 1 And blnIsEPCSEnable = True Then
                        If oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDRefillQuantity.Trim = "" Then
                            objPrescription.RefillQuantity = "0"
                        Else
                            objPrescription.RefillQuantity = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDRefillQuantity
                        End If
                        If oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier.Trim <> "" Then
                            If oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier.Trim.ToUpper = "PRN" Then  '' Do not Send PRN For CS Drug 
                                objPrescription.RefillQualifier = "P"
                            Else
                                objPrescription.RefillQualifier = oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier
                            End If
                        End If
                    Else
                        objPrescription.RefillQuantity = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDRefillQuantity
                        If oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier.Trim <> "" Then
                            objPrescription.RefillQualifier = oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier
                        End If
                    End If

                    objPrescription.Maysubstitute = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDbMaySubstitutions
                    If String.IsNullOrEmpty(objPrescription.PotencyCode) Then
                        objPrescription.PotencyCode = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDPotencyUnitCode
                    End If
                    oRefillRequest.ogloPrescription.DrugsCol.Item(0).PotencyCode = objPrescription.PotencyCode

                    objPrescription.Refills = objPrescription.RefillQuantity
                    ''If oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier.Trim <> "" Then
                    ''    objPrescription.RefillQualifier = oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier
                    ''End If
                    objPrescription.Frequency = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDFrequency
                    objPrescription.Amount = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDQuantity

                    If Not IsNothing(objPrescription.Amount) Then
                        If objPrescription.Amount <> "" Then
                            If objPrescription.Amount.Contains("Unspecified") Then
                                objPrescription.Amount = objPrescription.Amount.Trim.Replace("Unspecified", "")
                                If objPrescription.PotencyCode = "C38046" Then
                                    objPrescription.PotencyCode = ""
                                    oRefillRequest.ogloPrescription.DrugsCol.Item(0).PotencyCode = ""
                                End If
                            End If
                        End If
                    End If
                    objPrescription.Duration = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDDuration
                    objPrescription.Notes = oRefillRequest.ogloPrescription.DrugsCol.Item(0).MDNotes
                    If oOldgloPrescription.DrugsCol.Item(0).Drugform.Trim = "" Then
                        oOldgloPrescription.DrugsCol.Item(0).Drugform = objPrescription.DosageForm
                    End If

                    objPrescription.Status = "Approved"
                    objPrescription.MessageType = "RefillRequest"
                    ''whenever a drug is approved from the refill request screen then bydefault the method will be "eRx"
                    If objPrescription.IsNarcotics > 0 Then
                        If blnIsEPCSEnable = False Then   '' Check For EPCS Enabled
                            objPrescription.Method = "Print"
                        Else
                            objPrescription.Method = "eRx"
                        End If
                    Else
                        objPrescription.Method = "eRx"
                    End If


                    objPrescription.Renewed = "Renewed" & " " & Now & " " & globalSecurity.gstrLoginName

                    If objPrescription.Duration.Trim.Length > 0 Then
                        If IsNumeric(objPrescription.Duration.Trim) Then
                            objPrescription.Enddate = DateAdd(DateInterval.Day, CType(objPrescription.Duration.Trim, Double), objPrescription.Startdate).AddDays(If(CType(objPrescription.Duration.Trim, Double) = 0, 0, -1))
                            objPrescription.CheckEndDate = True
                        End If
                    End If



                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyName) Then
                        objPrescription.PharmacyName = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyName
                    Else
                        objPrescription.PharmacyName = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PhServiceLevel) Then
                        objPrescription.PhServiceLevel = oRefillRequest.ogloPrescription.RxPharmacy.PhServiceLevel
                    Else
                        objPrescription.PhServiceLevel = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyID) Then
                        objPrescription.PhNCPDPID = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyID
                    Else
                        objPrescription.PhNCPDPID = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.ContactID) Then
                        ''''as both PharmacyID and PhContactID samee
                        objPrescription.PharmacyID = oRefillRequest.ogloPrescription.RxPharmacy.ContactID
                        objPrescription.PhContactID = oRefillRequest.ogloPrescription.RxPharmacy.ContactID
                    Else
                        objPrescription.PharmacyID = 0 ''''issue reported from kari's database
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.City) Then
                        objPrescription.PhCity = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.City
                    Else
                        objPrescription.PhCity = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.State) Then
                        objPrescription.PhState = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.State
                    Else
                        objPrescription.PhState = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Zip) Then
                        objPrescription.PhZip = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Zip
                    Else
                        objPrescription.PhZip = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Phone) Then
                        objPrescription.PhPhone = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Phone
                    Else
                        objPrescription.PhPhone = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Fax) Then
                        objPrescription.PhFax = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Fax
                    Else
                        objPrescription.PhFax = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Email) Then
                        objPrescription.PhEmail = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyPhone.Email
                    Else
                        objPrescription.PhEmail = ""
                    End If

                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Address1) Then
                        objPrescription.PhAddressline1 = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Address1
                    Else
                        objPrescription.PhAddressline1 = ""
                    End If
                    If Not IsNothing(oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Address2) Then
                        objPrescription.PhAddressline2 = oRefillRequest.ogloPrescription.RxPharmacy.PharmacyAddress.Address2
                    Else
                        objPrescription.PhAddressline2 = ""
                    End If


                    objPrescription.FileData = oRefillRequest.ogloPrescription.FileData
                End If

                If Me.TransactionMode = _TransactionMode.Edit Then
                    objPrescription.Prescriptiondate = Me.PrescriptionDate    'Set date as Old Prescription Date
                End If

                If objPrescription.mpid = 0 Or objPrescription.mpid = Nothing Then
                    Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        objPrescription.mpid = oDIBHelper.GetMarketedProductId(objPrescription.NDCCode)
                    End Using
                End If
                If (Not objPrescription.RxType.Any()) Then
                    objPrescription.RxType = Me.GetRxType(objPrescription.mpid)
                End If

                If Not IsNothing(objPrescription) Then
                    _Prescriptions.Add(objPrescription)
                End If
                Return True
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                getRxRefillRequest = Nothing
            Catch ex As PrescriptionException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                getRxRefillRequest = Nothing
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error refilling Prescription Details."
                getRxRefillRequest = Nothing
                Throw objex
                Return Nothing
            Finally
                'SLR: It should not be disposed
                'If Not IsNothing(objPrescription) Then
                '    objPrescription.Dispose()
                '    objPrescription = Nothing
                'End If
                If Not IsNothing(oRefillRequest) Then
                    oRefillRequest.Dispose()
                    oRefillRequest = Nothing
                End If
            End Try
        End Function

        Public Function ERxForDeniedPrescriptionItem(Optional ByVal MessageID As String = "", Optional ByVal sPrescriberFirstName As String = "", Optional ByVal sPrescriberLastName As String = "", Optional ByVal sPrescriberMiddleName As String = "", Optional ByVal SupervisorId As Int64 = 0, Optional ByVal FormularysCol As BenefitsCoordinations = Nothing, Optional ByVal RxRefernceNumber As String = "", Optional ByVal blnNcpdp As Boolean = False) As Boolean
            Dim ogloPrescription As EPrescription = Nothing
            Dim objDrug As gloSureScript.EDrug = Nothing
            Dim oSurescriptDBLayer As New gloSureScriptDBLayer
            Dim ogloInterface As gloSureScriptInterface = Nothing
            Dim ValidateDrugBuilder As System.Text.StringBuilder = Nothing

            gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
            'gloSurescriptGeneral.sUserName = globalSecurity.gstrLoginName
            'gloSurescriptGeneral.sPassword = globalSecurity.gstrLoginPassword
            gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
            gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer

            Try
                If _Prescriptions.Count > 0 Then
                    Dim objPrescription As Prescription = Nothing
                    ogloInterface = New gloSureScriptInterface
                    Dim ClinicName As String = GetClinicName()
                    Dim ierxcnt As Int32 = 0
                    Dim strMessageType As String = ""
                    'Dim dttable As DataTable = Nothing
                    Dim dtpotencyCode As DataTable = Nothing

                    For icnt As Int32 = 0 To _Prescriptions.Count - 1
                        If _Prescriptions.Item(icnt).Status <> "D" Then

                            If _Prescriptions.Item(icnt).Status = "DeniedWNewRx" Or _Prescriptions.Item(icnt).Status = "Denied" Then
                                'SLR: Free exisitng memeory and then
                                If Not ValidateDrugBuilder Is Nothing Then
                                    ValidateDrugBuilder = Nothing
                                End If
                                ValidateDrugBuilder = New System.Text.StringBuilder
                                strMessageType = _Prescriptions.Item(icnt).Status
                                _Prescriptions.Item(icnt).Status = ""
                                objPrescription = _Prescriptions.Item(icnt)


                                'SLR: Free exisitng memeory and then
                                If Not ogloPrescription Is Nothing Then
                                    ogloPrescription.Dispose()
                                    ogloPrescription = Nothing
                                End If
                                ogloPrescription = New EPrescription
                                'SLR: Free exisitng memeory and then
                                If Not objDrug Is Nothing Then
                                    objDrug.Dispose()
                                    objDrug = Nothing
                                End If
                                objDrug = New gloSureScript.EDrug
                                ogloPrescription.RxTransactionID = _Prescriptions.Item(icnt).PrescriptionID
                                ogloPrescription.RxPrescriber.ProviderID = _Prescriptions.Item(icnt).ProviderID
                                ogloPrescription.RxPharmacy.ContactID = _Prescriptions.Item(icnt).PharmacyID
                                ogloPrescription.RxSupervisorProviderID = SupervisorId
                                ogloPrescription.ClinicName = ClinicName
                                If ValidateDrugBuilder.Length <= 0 Then

                                    If ValidateDrugBuilder.Length <= 0 Then
                                        oSurescriptDBLayer.GetERxDetailsFromIDs(ogloPrescription, _Prescriptions.Item(icnt).PatientID, _Prescriptions.Item(icnt).ProviderID, _Prescriptions.Item(icnt).PhNCPDPID, ogloPrescription.RxSupervisorProviderID)
                                        'End If

                                        If Not IsNothing(FormularysCol) Then

                                            If FormularysCol.Count > 0 Then
                                                For i As Int16 = 0 To FormularysCol.Count - 1
                                                    ogloPrescription.FormularyCol.Add(FormularysCol.Item(i))
                                                Next
                                            End If

                                        End If
                                        Dim strDuration As String = ""

                                        If Not IsNothing(_Prescriptions.Item(icnt).Duration) Then
                                            Dim retval As String() = SplitDrug(_Prescriptions.Item(icnt).Duration)

                                            If Not IsNothing(retval) Then
                                                If retval.Length > 1 Then
                                                    strDuration = retval(0)
                                                Else
                                                    strDuration = _Prescriptions.Item(icnt).Duration
                                                End If

                                            Else
                                                strDuration = _Prescriptions.Item(icnt).Duration
                                            End If
                                        Else
                                            strDuration = ""
                                        End If

                                        objDrug.DrugName = _Prescriptions.Item(icnt).Medication

                                        objDrug.Dosage = _Prescriptions.Item(icnt).Dosage.Trim
                                        objDrug.Drugform = _Prescriptions.Item(icnt).DosageForm.Trim
                                        objDrug.DrugRoute = _Prescriptions.Item(icnt).Route
                                        objDrug.DrugFrequency = _Prescriptions.Item(icnt).Frequency
                                        objDrug.DrugDuration = strDuration
                                        objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount
                                        objDrug.Directions = objDrug.DrugFrequency.Trim

                                        objDrug.RefillQuantity = _Prescriptions.Item(icnt).Refills.Trim
                                        objDrug.RefillsQualifier = _Prescriptions.Item(icnt).RefillQualifier
                                        objDrug.MaySubstitute = _Prescriptions.Item(icnt).Maysubstitute
                                        objDrug.WrittenDate = _Prescriptions.Item(icnt).Prescriptiondate
                                        objDrug.PrescriptionID = _Prescriptions.Item(icnt).PrescriptionID

                                        objDrug.IseRxed = _Prescriptions.Item(icnt).IseRxed

                                        objDrug.MessageFrom = "mailto:" & ogloPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"

                                        objDrug.MessageTo = "mailto:" & ogloPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
                                        objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID

                                        objDrug.Notes = _Prescriptions.Item(icnt).Notes
                                        objDrug.RelatesToMessageId = MessageID
                                        objDrug.RxReferenceNumber = RxRefernceNumber
                                        objDrug.WrittenDate = _Prescriptions.Item(icnt).Prescriptiondate
                                        'For De-Normalization
                                        'commented to remove the dependancy on the drugs table. pass the value from the _Prescriptions collection.


                                        objDrug.PotencyCode = Convert.ToString(_Prescriptions.Item(icnt).PotencyCode)
                                        If objDrug.PotencyCode = "" Then
                                            ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug Potency Code is not available for " & _Prescriptions.Item(icnt).Medication & ",Please Update Drug Dosage" & vbCrLf)
                                        End If

                                        objDrug.ProdCode = _Prescriptions.Item(icnt).NDCCode
                                        objDrug.ProdCodeQualifier = "ND"
                                        ogloPrescription.DrugsCol.Add(objDrug)

                                        Dim blnFilePost As Boolean = False

                                        blnFilePost = ogloInterface.Generate10dot6XMLforNewRx(ogloPrescription, 0, ierxcnt, strMessageType, MessageID, blnNcpdp)

                                        If blnFilePost = True Then
                                            If ogloInterface.StatusMessageType.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                                eRxResponseitem = _Prescriptions.Item(icnt).ItemNumber
                                                ogloPrescription.DrugsCol.Item(0).TransactionID = ogloPrescription.DrugsCol.Item(0).PrescriptionID
                                                oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(0), SureScriptMessage))
                                                _eRxStatus = ogloInterface.MessageName
                                                _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                                                If _eRxStatus <> "" Then

                                                    Select Case _eRxStatus
                                                        Case "Status"
                                                            Select Case ogloInterface.MessagestatusCode
                                                                Case "000"
                                                                    _eRxStatus = "Transmission successful"
                                                                Case "010"
                                                                    _eRxStatus = "Successfully accepted by ultimate receiver"
                                                                Case Else
                                                                    _eRxStatus = "Posted Successfully"

                                                            End Select

                                                        Case "Verify" 'for case verify we have same value in message variable as of status case
                                                            _eRxStatus = "Posted Successfully"
                                                        Case "Error"
                                                            _eRxStatus = "Could not be Posted Successfully"
                                                            _eRxStatusMessage = ogloInterface.StatusMessageType
                                                    End Select
                                                End If
                                            End If
                                        Else
                                            If ogloInterface.ValidationMessage.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessage, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                                ERxForDeniedPrescriptionItem = Nothing
                                                Exit Function
                                            End If
                                        End If

                                        If Not IsNothing(ogloInterface) Then
                                            If ogloInterface.ValidationMessageBuilderforDrug.ToString.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderforDrug.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                            End If
                                            If ogloInterface.ValidationMessageBuilderfor10dot6.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderfor10dot6.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                            End If
                                        End If
                                        Exit For
                                    End If
                                Else
                                    ogloInterface.ValidationMessageBuilderforDrug.Append(ValidateDrugBuilder.ToString & vbCrLf)
                                    If Not IsNothing(ogloInterface) Then
                                        If ogloInterface.ValidationMessageBuilderforDrug.ToString.Length > 0 Then
                                            System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderforDrug.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        End If
                                    End If
                                    Exit For
                                End If
                                ValidateDrugBuilder.Remove(0, ValidateDrugBuilder.Length)
                            End If
                        End If
                    Next

                    'There is one denied object in the list
                    If Not IsNothing(objPrescription) Then
                        If _Prescriptions.Count > 1 Then
                            'SLR: to 0 or -1?
                            For icnt As Int32 = _Prescriptions.Count - 1 To 0 Step -1
                                If Not _Prescriptions.Item(icnt) Is objPrescription Then
                                    _Prescriptions.Remove(icnt)

                                End If
                            Next
                        End If
                        'RaiseEvent RxSavedForDeniedWithNewRxToFollow()
                        Return True
                    End If

                    Return True
                Else
                    ERxForDeniedPrescriptionItem = Nothing
                End If
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                ERxForDeniedPrescriptionItem = Nothing
            Catch ex As PrescriptionException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                ERxForDeniedPrescriptionItem = Nothing
            Catch ex As Exception
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                Dim Objex As New PrescriptionException
                Objex.ErrMessage = "Error Posting Prescription"
                ERxForDeniedPrescriptionItem = Nothing
                Throw Objex
            Finally
                'SLR: Finally free all memories.
                If Not objDrug Is Nothing Then
                    objDrug.Dispose()
                    objDrug = Nothing
                End If
                If Not oSurescriptDBLayer Is Nothing Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
                If Not ogloInterface Is Nothing Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
                If Not ValidateDrugBuilder Is Nothing Then
                    ValidateDrugBuilder = Nothing
                End If
            End Try
        End Function

        Public Sub ProcessERxData(Optional ByVal FormularysCol As BenefitsCoordinations = Nothing)
            Dim oSurescriptDBLayer As gloSureScriptDBLayer = Nothing
            Try
                oSurescriptDBLayer = New gloSureScriptDBLayer()
                gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
                gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
                oProcessERxPrescription = GetElementsForERx(FormularysCol)
                oSurescriptDBLayer.GetERxDetailsFromIDs(oProcessERxPrescription, _PatientID, _ProviderId, 0, SupervisingProviderID)
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oSurescriptDBLayer) Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
            End Try
        End Sub

        Public Function GetElementsForERx(Optional ByVal FormularysCol As BenefitsCoordinations = Nothing) As EPrescription
            Dim ogloPrescription As EPrescription = Nothing
            Dim objDrug As gloSureScript.EDrug = Nothing
            Dim dttable As DataTable = Nothing
            Try
                If _Prescriptions.Count > 0 Then

                    ogloPrescription = New EPrescription

                    Dim ClinicName As String = GetClinicName() ''Clinic Name 
                    If Not IsNothing(ogloInterface) Then
                        ogloInterface.Dispose()
                        ogloInterface = Nothing
                    End If
                    ogloInterface = New gloSureScriptInterface
                    ogloPrescription.ClinicName = ClinicName
                    ogloPrescription.PatientID = _Prescriptions.Item(0).PatientID
                    ogloPrescription.ProviderID = _Prescriptions.Item(0).ProviderID

                    'Diagnosis

                    Dim sICDRevPrimary As String = Nothing
                    Dim sICDCodePrimary As String = Nothing

                    Dim sICDRevSecondary As String = Nothing
                    Dim sICDCodeSecondary As String = Nothing

                    Dim dtDiagnosis As DataTable = Nothing
                   

                    For icnt As Int32 = 0 To _Prescriptions.Count - 1
                        dttable = Nothing
                        ''Check Issue Method
                        'Resolving Bug #88628: 00000995 : the system tells us that "This is a controlled substance would you like to print?" Issue method will already be set to Print '
                        If (_Prescriptions.Item(icnt).Method <> "eRx" And (_Prescriptions.Item(icnt).Method = "OTC" And blnIncludeOTCDrugs = False)) Or _Prescriptions.Item(icnt).Method = "Print" Then
                            Continue For
                        End If

                        If Not String.IsNullOrEmpty(_Prescriptions.Item(icnt).Problems) AndAlso Not String.IsNullOrWhiteSpace(_Prescriptions.Item(icnt).Problems) Then
                            Using oPrescriptionLayer As New PrescriptionBusinessLayer()
                                dtDiagnosis = oPrescriptionLayer.GetDiagnosisCodes(_Prescriptions.Item(icnt).Problems)

                                If dtDiagnosis IsNot Nothing Then
                                    For Each row As DataRow In dtDiagnosis.Rows
                                        If IsNothing(sICDCodePrimary) Then
                                            sICDRevPrimary = Convert.ToString(row("sICDRevision"))
                                            sICDCodePrimary = Convert.ToString(row("sICD9Code"))
                                        Else
                                            If IsNothing(sICDCodeSecondary) Then
                                                sICDRevSecondary = Convert.ToString(row("sICDRevision"))
                                                sICDCodeSecondary = Convert.ToString(row("sICD9Code"))
                                            End If
                                        End If
                                    Next
                                End If

                            End Using
                        End If
                        dtDiagnosis = Nothing
                        ''Check Rowstate
                        If _Prescriptions.Item(icnt).Status <> "D" Then
                            Dim matchfound As Boolean = False
                            Dim k As Int32 = Nothing
                            For j As Int32 = 0 To _tmpCheckStates.Count - 1
                                If _tmpCheckStates.Item(j).ItemNumber = _Prescriptions.Item(icnt).ItemNumber Then
                                    matchfound = True
                                    k = j
                                    Exit For
                                End If
                            Next
                            If matchfound = True Then
                                If _tmpCheckStates.Item(k).CheckState = True Then

                                    'Check Narcotic Drug
                                    Dim blnscheduleddrug As Boolean = False
                                    Select Case _Prescriptions.Item(icnt).IsNarcotics
                                        ''BDO Audit
                                        Case 2, 3, 4, 5
                                            If blnIsEPCSEnable = False Then ''EPCS iS OFF
                                                'because the medication is a controlled substance, it cannot be sent electronically. The Rx will be printed and needs a wet signature before it can be faxed to the pharmacy or handed to the patient
                                                System.Windows.Forms.MessageBox.Show("Because the medication " & _Prescriptions.Item(icnt).Medication & " is a controlled substance, it cannot be sent electronically. The Rx will be printed and needs a wet signature before it can be faxed to the pharmacy or handed to the patient", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                                                blnscheduleddrug = True
                                            Else  ''EPCS is ON
                                                If Convert.ToString(_Prescriptions.Item(icnt).EPCSeRxStatus).ToUpper = "PRINTED" Then
                                                    System.Windows.Forms.MessageBox.Show("Because the medication " & _Prescriptions.Item(icnt).Medication & " is a controlled substance and it was already printed, it cannot be sent electronically. ", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                                                    _Prescriptions.Item(icnt).FlagtoDeletePrescription = False
                                                    blnscheduleddrug = True
                                                End If
                                            End If
                                    End Select

                                    If _Prescriptions.Item(icnt).NDCCode.ToUpper().Contains("GLO") Then
                                        System.Windows.Forms.MessageBox.Show("Because the medication " & _Prescriptions.Item(icnt).Medication & " is an uncoded drug, it cannot be sent electronically.", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                                        blnscheduleddrug = True
                                    End If
                                    ''CPOE Check
                                    Dim _IsCPOE As DialogResult
                                    If Not (_Prescriptions.Item(icnt).Method = "eRx" OrElse (_Prescriptions.Item(icnt).Method = "OTC" AndAlso blnIncludeOTCDrugs = True)) Then
                                        Continue For
                                    End If
                                    If blnscheduleddrug = False Then 'not Narcotic or ndccode not contain sting GLO 
                                        If _tmpCheckStates.Item(k).PrescriptionID <> 0 And _tmpCheckStates.Item(k).CPOEOrder = True Then
                                            Dim sDrugName As String = _Prescriptions.Item(icnt).Medication
                                            _IsCPOE = MessageBox.Show("This prescription " & sDrugName & " was already ordered.  Are you sure you wish to eRx?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                            If _IsCPOE = System.Windows.Forms.DialogResult.Yes Then
                                                'Drug Details
                                                If Not IsNothing(FormularysCol) Then
                                                    If FormularysCol.Count > 0 Then
                                                        For i As Int16 = 0 To FormularysCol.Count - 1
                                                            ogloPrescription.FormularyCol.Add(FormularysCol.Item(i))
                                                        Next
                                                    End If
                                                End If
                                                If Not objDrug Is Nothing Then
                                                    objDrug.Dispose()
                                                    objDrug = Nothing
                                                End If
                                                objDrug = New gloSureScript.EDrug
                                                objDrug.IsNarcotics = _Prescriptions.Item(icnt).IsNarcotics
                                                objDrug.Drugform = _Prescriptions.Item(icnt).DosageForm
                                                objDrug.DrugStrength = _Prescriptions.Item(icnt).Dosage
                                                objDrug.ProdCode = _Prescriptions.Item(icnt).NDCCode
                                                objDrug.ProdCodeQualifier = "ND"
                                                objDrug.DrugFrequency = _Prescriptions.Item(icnt).Frequency
                                                objDrug.PotencyCode = _Prescriptions.Item(icnt).PotencyCode
                                                objDrug.StrengthUnits = _Prescriptions.Item(icnt).StrengthUnit


                                                objDrug.DrugName = _Prescriptions.Item(icnt).Medication
                                                objDrug.Dosage = _Prescriptions.Item(icnt).Dosage
                                                '    objDrug.DrugDuration = _Prescriptions.Item(icnt).Duration.Trim


                                                Dim nDaysSupply As Integer = 0
                                                If _Prescriptions.Item(icnt).Duration.Trim.Length > 0 AndAlso Val(_Prescriptions.Item(icnt).Duration) <> 0 Then
                                                    If IsNumeric(_Prescriptions.Item(icnt).Duration) Then
                                                        nDaysSupply = Val(_Prescriptions.Item(icnt).Duration)
                                                    Else
                                                        Dim nDuration As String() = Nothing
                                                        Dim numberofDays As Integer
                                                        nDuration = _Prescriptions.Item(icnt).Duration.Trim.Split(" ")
                                                        If nDuration.Length > 0 Then
                                                            Select Case nDuration(1).ToUpper
                                                                Case "MONTHS"
                                                                    numberofDays = 30
                                                                Case "DAYS"
                                                                    numberofDays = 1
                                                                Case "WEEKS"
                                                                    numberofDays = 7
                                                            End Select
                                                            nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                                                        End If
                                                    End If
                                                    objDrug.DrugDuration = nDaysSupply
                                                Else
                                                    objDrug.DrugDuration = ""
                                                End If

                                                If _Prescriptions.Item(icnt).Amount.Trim <> "" Then 'fixed bug 5453
                                                    Dim strDispense As String() = Split(_Prescriptions.Item(icnt).Amount.Trim, " ")
                                                    If strDispense.Length > 1 Then
                                                        objDrug.DrugQuantity = strDispense(0)
                                                    Else
                                                        objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                                    End If
                                                Else
                                                    objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                                End If

                                                objDrug.Directions = objDrug.DrugFrequency.Trim & " " & objDrug.DrugRoute.Trim

                                                objDrug.RefillQuantity = _Prescriptions.Item(icnt).Refills.Trim
                                                If IsNothing(_Prescriptions.Item(icnt).RefillQualifier) Then
                                                    _Prescriptions.Item(icnt).RefillQualifier = "R"
                                                    '' Bug #70507: 00000723: NewRx missing Refill value Tag. Changes added to check blank value.
                                                ElseIf _Prescriptions.Item(icnt).RefillQualifier.Trim() = "" Then
                                                    _Prescriptions.Item(icnt).RefillQualifier = "R"
                                                End If
                                                objDrug.RefillsQualifier = _Prescriptions.Item(icnt).RefillQualifier
                                                objDrug.MaySubstitute = _Prescriptions.Item(icnt).Maysubstitute
                                                objDrug.WrittenDate = _Prescriptions.Item(icnt).Prescriptiondate
                                                objDrug.PrescriptionID = _Prescriptions.Item(icnt).PrescriptionID
                                                objDrug.IseRxed = _Prescriptions.Item(icnt).IseRxed
                                                objDrug.MessageFrom = "mailto:" & ogloPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                                                objDrug.MessageTo = "mailto:" & _Prescriptions.Item(icnt).PhNCPDPID & ".ncpdp@surescripts.com"
                                                objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID

                                                objDrug.mpid = _Prescriptions.Item(icnt).mpid ''''for MU NewRxEDIFACT file

                                                objDrug.Notes = _Prescriptions.Item(icnt).Notes
                                                'Drug Details

                                                'Pharmacy Details
                                                objDrug.PhContactID = _Prescriptions.Item(icnt).PhContactID
                                                objDrug.PhNCPDPID = _Prescriptions.Item(icnt).PhNCPDPID
                                                objDrug.PharmacyName = _Prescriptions.Item(icnt).PharmacyName
                                                objDrug.PhAddressline1 = _Prescriptions.Item(icnt).PhAddressline1
                                                objDrug.PhAddressline2 = _Prescriptions.Item(icnt).PhAddressline2
                                                objDrug.PhCity = _Prescriptions.Item(icnt).PhCity
                                                objDrug.PhState = _Prescriptions.Item(icnt).PhState
                                                objDrug.PhZip = _Prescriptions.Item(icnt).PhZip
                                                objDrug.PhPhone = _Prescriptions.Item(icnt).PhPhone
                                                objDrug.PhFax = _Prescriptions.Item(icnt).PhFax
                                                objDrug.PhNPI = GetPharmacyNPIFromNCPDID(objDrug.PhNCPDPID)
                                                objDrug.MessageType = _Prescriptions.Item(icnt).MessageType
                                                objDrug.ItemNumber = _Prescriptions.Item(icnt).ItemNumber
                                                objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID
                                                objDrug.IseRxed = _Prescriptions.Item(icnt).IseRxed
                                                If Not IsNothing(oOldgloPrescription) Then
                                                    If Not IsNothing(oOldgloPrescription.FileData) Then
                                                        objDrug.FileData = oOldgloPrescription.FileData
                                                    End If
                                                    If oOldgloPrescription.DrugsCol.Count > 0 Then
                                                        objDrug.RelatesToMessageId = oOldgloPrescription.DrugsCol.Item(0).RelatesToMessageId
                                                    End If
                                                End If

                                                'Diagnosis

                                                If Not String.IsNullOrEmpty(sICDRevPrimary) And Not String.IsNullOrEmpty(sICDCodePrimary) Then
                                                    If sICDRevPrimary = "10" Then
                                                        objDrug.PrimaryDXQualifier = "ABF"
                                                    Else
                                                        objDrug.PrimaryDXQualifier = "DX"
                                                    End If
                                                    objDrug.PrimaryDXValue = sICDCodePrimary

                                                End If

                                                If Not String.IsNullOrEmpty(sICDRevSecondary) And Not String.IsNullOrEmpty(sICDCodeSecondary) Then
                                                    If sICDRevSecondary = "10" Then
                                                        objDrug.SecondaryDXQualifier = "ABF"
                                                    Else
                                                        objDrug.SecondaryDXQualifier = "DX"
                                                    End If
                                                    objDrug.SecondaryDXValue = sICDCodeSecondary

                                                End If

                                                'Pharmacy Details
                                                Using ePAInsertUpdate As New EPABusinesslayer()
                                                    If Not String.IsNullOrEmpty(_Prescriptions.Item(icnt).PAReferenceID) AndAlso ePAInsertUpdate.epa_IsManualPriorAuthorization(_PatientID, _Prescriptions.Item(icnt).PAReferenceID) Then
                                                        objDrug.PriorAuthorizationStatus = "APPROVED"
                                                        objDrug.PriorAuthorizationValue = _Prescriptions.Item(icnt).PriorAuthorizationNumber
                                                    Else
                                                        objDrug.PriorAuthorizationStatus = _Prescriptions.Item(icnt).PriorAuthorizationStatus
                                                        objDrug.PriorAuthorizationValue = _Prescriptions.Item(icnt).PriorAuthorizationNumber
                                                    End If
                                                End Using
                                                If objDrug.MessageType = "NewRx" OrElse objDrug.MessageType = "RxChangeRequest" Then
                                                    If _Prescriptions.Item(icnt).PCTransactionID > 0 Then
                                                        objDrug.PCTransactionID = _Prescriptions.Item(icnt).PCTransactionID
                                                        objDrug.PCPrograms = _Prescriptions.Item(icnt).PDRPrograms
                                                        Dim PhNotes As String = String.Empty
                                                        If Not IsNothing(objDrug.PCPrograms) AndAlso Not IsNothing(objDrug.PCPrograms.Programs) Then
                                                            For Each item As Program In objDrug.PCPrograms.Programs
                                                                If String.IsNullOrWhiteSpace(PhNotes) Then
                                                                    PhNotes = item.paymentNotes
                                                                Else
                                                                    PhNotes = PhNotes + ", [" + item.paymentNotes + "]"
                                                                End If
                                                            Next
                                                            If String.IsNullOrWhiteSpace(objDrug.Notes) Then
                                                                objDrug.Notes = PhNotes
                                                            Else
                                                                objDrug.Notes = objDrug.Notes + ", " + PhNotes
                                                            End If
                                                        End If
                                                    End If
                                                End If

                                                ogloPrescription.DrugsCol.Add(objDrug)
                                            End If
                                        Else ''not CPOE
                                            'Drug Details
                                            If Not IsNothing(FormularysCol) Then
                                                If FormularysCol.Count > 0 Then
                                                    For i As Int16 = 0 To FormularysCol.Count - 1
                                                        ogloPrescription.FormularyCol.Add(FormularysCol.Item(i))
                                                    Next
                                                End If
                                            End If
                                            If Not objDrug Is Nothing Then
                                                objDrug.Dispose()
                                                objDrug = Nothing
                                            End If
                                            objDrug = New gloSureScript.EDrug
                                            objDrug.IsNarcotics = _Prescriptions.Item(icnt).IsNarcotics
                                            objDrug.Drugform = _Prescriptions.Item(icnt).DosageForm
                                            objDrug.DrugStrength = _Prescriptions.Item(icnt).Dosage
                                            objDrug.ProdCode = _Prescriptions.Item(icnt).NDCCode
                                            objDrug.ProdCodeQualifier = "ND"
                                            objDrug.DrugFrequency = _Prescriptions.Item(icnt).Frequency
                                            objDrug.PotencyCode = _Prescriptions.Item(icnt).PotencyCode
                                            objDrug.StrengthUnits = _Prescriptions.Item(icnt).StrengthUnit

                                            objDrug.DrugName = _Prescriptions.Item(icnt).Medication
                                            objDrug.Dosage = _Prescriptions.Item(icnt).Dosage
                                            ' objDrug.DrugDuration = _Prescriptions.Item(icnt).Duration.Trim

                                            Dim nDaysSupply As Integer = 0
                                            If _Prescriptions.Item(icnt).Duration.Trim.Length > 0 AndAlso Val(_Prescriptions.Item(icnt).Duration) <> 0 Then
                                                If IsNumeric(_Prescriptions.Item(icnt).Duration) Then
                                                    nDaysSupply = Val(_Prescriptions.Item(icnt).Duration)
                                                Else
                                                    Dim nDuration As String() = Nothing
                                                    Dim numberofDays As Integer
                                                    nDuration = _Prescriptions.Item(icnt).Duration.Trim.Split(" ")
                                                    If nDuration.Length > 0 Then
                                                        Select Case nDuration(1).ToUpper
                                                            Case "MONTHS"
                                                                numberofDays = 30
                                                            Case "DAYS"
                                                                numberofDays = 1
                                                            Case "WEEKS"
                                                                numberofDays = 7
                                                        End Select
                                                        nDaysSupply = numberofDays * CType(nDuration(0), Integer)
                                                    End If
                                                End If
                                                objDrug.DrugDuration = nDaysSupply
                                            Else
                                                objDrug.DrugDuration = ""
                                            End If

                                            If _Prescriptions.Item(icnt).Amount.Trim <> "" Then 'fixed bug 5453
                                                Dim strDispense As String() = Split(_Prescriptions.Item(icnt).Amount.Trim, " ")
                                                If strDispense.Length > 1 Then
                                                    objDrug.DrugQuantity = strDispense(0)
                                                Else
                                                    objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                                End If
                                            Else
                                                objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                            End If

                                            objDrug.Directions = objDrug.DrugFrequency.Trim & " " & objDrug.DrugRoute.Trim

                                            objDrug.RefillQuantity = _Prescriptions.Item(icnt).Refills.Trim
                                            If IsNothing(_Prescriptions.Item(icnt).RefillQualifier) Then
                                                _Prescriptions.Item(icnt).RefillQualifier = "R"
                                                '' Bug #70507: 00000723: NewRx missing Refill value Tag. Changes added to check blank value.
                                            ElseIf _Prescriptions.Item(icnt).RefillQualifier.Trim() = "" Then
                                                _Prescriptions.Item(icnt).RefillQualifier = "R"
                                            End If
                                            objDrug.RefillsQualifier = _Prescriptions.Item(icnt).RefillQualifier
                                            objDrug.MaySubstitute = _Prescriptions.Item(icnt).Maysubstitute
                                            objDrug.WrittenDate = _Prescriptions.Item(icnt).Prescriptiondate
                                            objDrug.PrescriptionID = _Prescriptions.Item(icnt).PrescriptionID
                                            objDrug.IseRxed = _Prescriptions.Item(icnt).IseRxed
                                            objDrug.MessageFrom = "mailto:" & ogloPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                                            objDrug.MessageTo = "mailto:" & _Prescriptions.Item(icnt).PhNCPDPID & ".ncpdp@surescripts.com"
                                            objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID

                                            objDrug.mpid = _Prescriptions.Item(icnt).mpid ''''for MU NewRxEDIFACT file

                                            objDrug.Notes = _Prescriptions.Item(icnt).Notes
                                            'Drug Details

                                            'Pharmacy Details
                                            objDrug.PhContactID = _Prescriptions.Item(icnt).PhContactID
                                            objDrug.PhNCPDPID = _Prescriptions.Item(icnt).PhNCPDPID
                                            objDrug.PharmacyName = _Prescriptions.Item(icnt).PharmacyName
                                            objDrug.PhAddressline1 = _Prescriptions.Item(icnt).PhAddressline1
                                            objDrug.PhAddressline2 = _Prescriptions.Item(icnt).PhAddressline2
                                            objDrug.PhCity = _Prescriptions.Item(icnt).PhCity
                                            objDrug.PhState = _Prescriptions.Item(icnt).PhState
                                            objDrug.PhZip = _Prescriptions.Item(icnt).PhZip
                                            objDrug.PhPhone = _Prescriptions.Item(icnt).PhPhone
                                            objDrug.PhFax = _Prescriptions.Item(icnt).PhFax
                                            objDrug.PhNPI = GetPharmacyNPIFromNCPDID(objDrug.PhNCPDPID)
                                            objDrug.MessageType = _Prescriptions.Item(icnt).MessageType
                                            objDrug.ItemNumber = _Prescriptions.Item(icnt).ItemNumber
                                            objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID

                                            If Not IsNothing(oOldgloPrescription) Then
                                                If Not IsNothing(oOldgloPrescription.FileData) Then
                                                    objDrug.FileData = oOldgloPrescription.FileData
                                                End If
                                                If oOldgloPrescription.DrugsCol.Count > 0 Then
                                                    objDrug.RelatesToMessageId = oOldgloPrescription.DrugsCol.Item(0).RelatesToMessageId
                                                End If
                                            End If

                                            'Diagnosis

                                            If Not String.IsNullOrEmpty(sICDRevPrimary) And Not String.IsNullOrEmpty(sICDCodePrimary) Then
                                                If sICDRevPrimary = "10" Then
                                                    objDrug.PrimaryDXQualifier = "ABF"
                                                Else
                                                    objDrug.PrimaryDXQualifier = "DX"
                                                End If
                                                objDrug.PrimaryDXValue = sICDCodePrimary

                                            End If

                                            If Not String.IsNullOrEmpty(sICDRevSecondary) And Not String.IsNullOrEmpty(sICDCodeSecondary) Then
                                                If sICDRevSecondary = "10" Then
                                                    objDrug.SecondaryDXQualifier = "ABF"
                                                Else
                                                    objDrug.SecondaryDXQualifier = "DX"
                                                End If
                                                objDrug.SecondaryDXValue = sICDCodeSecondary

                                            End If
                                            Using ePAInsertUpdate As New EPABusinesslayer()
                                                If Not String.IsNullOrEmpty(_Prescriptions.Item(icnt).PAReferenceID) AndAlso ePAInsertUpdate.epa_IsManualPriorAuthorization(_PatientID, _Prescriptions.Item(icnt).PAReferenceID) Then
                                                    objDrug.PriorAuthorizationStatus = "APPROVED"
                                                    objDrug.PriorAuthorizationValue = _Prescriptions.Item(icnt).PriorAuthorizationNumber
                                                Else
                                                    objDrug.PriorAuthorizationStatus = _Prescriptions.Item(icnt).PriorAuthorizationStatus
                                                    objDrug.PriorAuthorizationValue = _Prescriptions.Item(icnt).PriorAuthorizationNumber
                                                End If
                                            End Using

                                            If objDrug.MessageType = "NewRx" OrElse objDrug.MessageType = "RxChangeRequest" Then
                                                If _Prescriptions.Item(icnt).PCTransactionID > 0 Then
                                                    objDrug.PCTransactionID = _Prescriptions.Item(icnt).PCTransactionID
                                                    objDrug.PCPrograms = _Prescriptions.Item(icnt).PDRPrograms
                                                    Dim PhNotes As String = String.Empty
                                                    If Not IsNothing(objDrug.PCPrograms) AndAlso Not IsNothing(objDrug.PCPrograms.Programs) Then
                                                        For Each item As Program In objDrug.PCPrograms.Programs
                                                            If String.IsNullOrWhiteSpace(PhNotes) Then
                                                                PhNotes = item.paymentNotes
                                                            Else
                                                                PhNotes = PhNotes + ", [" + item.paymentNotes + "]"
                                                            End If
                                                        Next
                                                        If String.IsNullOrWhiteSpace(objDrug.Notes) Then
                                                            objDrug.Notes = PhNotes
                                                        Else
                                                            objDrug.Notes = objDrug.Notes + ", " + PhNotes
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            ogloPrescription.DrugsCol.Add(objDrug)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        sICDRevPrimary = Nothing
                        sICDCodePrimary = Nothing

                        sICDRevSecondary = Nothing
                        sICDCodeSecondary = Nothing
                    Next
                End If
                Return ogloPrescription
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetLastSavedPotencyCode(ByVal ndcCode As String, ByVal mpid As Int32) As String
            Dim con As SqlConnection = Nothing
            Dim cmdVisits As SqlCommand = Nothing
            Dim objParam As SqlParameter = Nothing
            Dim objFlagParam As SqlParameter = Nothing

            Dim sPotencyCode As String = ""
            Dim sPotencyUnit As String = ""

            Try

                con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
                cmdVisits = New SqlCommand("gsp_GetLastSavedPotencyCode", con)
                cmdVisits.CommandType = CommandType.StoredProcedure

                objParam = cmdVisits.Parameters.Add("@NDCcode", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = ndcCode

                objParam = cmdVisits.Parameters.Add("@mpid", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = mpid

                objParam = cmdVisits.Parameters.Add("@sPotencyCode", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Output
                objParam.Size = 50

                objParam = cmdVisits.Parameters.Add("@sPotencyUnit", SqlDbType.NVarChar)
                objParam.Direction = ParameterDirection.Output
                objParam.Size = 255


                con.Open()
                cmdVisits.ExecuteNonQuery()
                con.Close()

                sPotencyCode = Convert.ToString(cmdVisits.Parameters("@sPotencyCode").Value)
                sPotencyUnit = Convert.ToString(cmdVisits.Parameters("@sPotencyUnit").Value)

                objFlagParam = Nothing

                cmdVisits.Parameters.Clear()
                cmdVisits.Dispose()
                cmdVisits = Nothing

                con.Dispose()
                con = Nothing

                Return sPotencyCode
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally

            End Try
        End Function

        Public Shared Function GetDrugPotencyCode(ByVal UOMDescription As String) As DataTable
            'Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Dim dttable As DataTable = Nothing
            Dim _gloEMRDatabase As DataBaseLayer = New DataBaseLayer
            Try

                Dim _strSQl = "Select sPotencycode,sDescription  from PotencyCodeMaster where sDescription='" & UOMDescription.Trim & "'"
                dttable = _gloEMRDatabase.GetDataTable_Query(_strSQl)

                Return dttable

            Catch ex As Exception
                Return dttable
                If Not IsNothing(dttable) Then
                    dttable.Dispose()
                    dttable = Nothing
                End If
                Throw ex
            Finally

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Private Function ValidateEPCSData(ByRef ogloPrescription As EPrescription, ByVal DrugIndex As Int32) As Boolean
            Dim blnIsValid As Boolean = True
            Dim PrescriberMessageEPCSeRx As String = ""
            Dim PatientMessageEPCSeRx As String = ""
            Dim DrugAlreadyERXed As String = ""
            Dim sMessage As String = ""
            Try
                If Not IsNothing(ogloPrescription) Then

                    If Not IsNothing(ogloPrescription.DrugsCol.Item(DrugIndex)) Then
                        If ECPSeRxDrugs.Contains(ogloPrescription.DrugsCol.Item(DrugIndex).PrescriptionID) OrElse ogloPrescription.DrugsCol.Item(DrugIndex).IseRxed > 0 Then
                            DrugAlreadyERXed = "The prescription " + ogloPrescription.DrugsCol.Item(DrugIndex).DrugName + " has already been sent successfully and cannot be sent again."
                            System.Windows.Forms.MessageBox.Show(DrugAlreadyERXed, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            blnIsValid = False
                        End If
                    End If

                    If blnIsValid = True Then
                        '''''''''''''''''Prescriber Validation------------------------------------------------------------------
                        If Not IsNothing(ogloPrescription.RxPrescriber.PrescriberName.LastName) Then
                            If ogloPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length = 0 Then
                                PrescriberMessageEPCSeRx = "LastName,"
                            End If
                        End If

                        If Not IsNothing(ogloPrescription.RxPrescriber.PrescriberName.FirstName) Then
                            If ogloPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length = 0 Then
                                PrescriberMessageEPCSeRx = PrescriberMessageEPCSeRx & "FirstName,"
                            End If
                        End If

                        If Not IsNothing(ogloPrescription.RxPrescriber.PrescriberDEA) Then
                            If ogloPrescription.RxPrescriber.PrescriberDEA.Trim.Length = 0 Then
                                PrescriberMessageEPCSeRx = PrescriberMessageEPCSeRx & "DEA,"
                            End If
                        End If

                        If PrescriberMessageEPCSeRx.Length > 0 Then
                            PrescriberMessageEPCSeRx = "Prescriber's " & PrescriberMessageEPCSeRx
                        End If

                        '''''''''Patient Validation------------------------------------------------------------------------------------------------- 
                        If Not IsNothing(ogloPrescription.RxPatient.PatientName.LastName) Then
                            If ogloPrescription.RxPatient.PatientName.LastName.Trim.Length = 0 Then
                                PatientMessageEPCSeRx = "LastName,"
                            End If
                        End If

                        If Not IsNothing(ogloPrescription.RxPatient.PatientName.FirstName) Then
                            If ogloPrescription.RxPatient.PatientName.FirstName.Trim.Length = 0 Then
                                PatientMessageEPCSeRx = PatientMessageEPCSeRx & "FirstName,"
                            End If
                        End If

                        If Not IsNothing(ogloPrescription.RxPatient.PatientAddress.Address1) Then
                            If ogloPrescription.RxPatient.PatientAddress.Address1.Trim.Length = 0 Then
                                PatientMessageEPCSeRx = PatientMessageEPCSeRx & "Address Line1,"
                            End If
                        End If

                        If Not IsNothing(ogloPrescription.RxPatient.PatientAddress.City) Then
                            If ogloPrescription.RxPatient.PatientAddress.City.Trim.Length = 0 Then
                                PatientMessageEPCSeRx = PatientMessageEPCSeRx & "City,"
                            End If
                        End If
                        If Not IsNothing(ogloPrescription.RxPatient.PatientAddress.State) Then
                            If ogloPrescription.RxPatient.PatientAddress.State.Trim.Length = 0 Then
                                PatientMessageEPCSeRx = PatientMessageEPCSeRx & "State,"
                            End If
                        End If

                        If PatientMessageEPCSeRx.Length > 0 Then
                            PatientMessageEPCSeRx = "Patient's " & PatientMessageEPCSeRx
                        End If
                        sMessage = PrescriberMessageEPCSeRx & PatientMessageEPCSeRx
                        If sMessage.Trim.Length > 0 Then
                            sMessage = sMessage.Substring(0, sMessage.Length - 1)
                            System.Windows.Forms.MessageBox.Show("This Prescription request cannot be sent electronically because the following data is missing. " & vbCrLf & sMessage.ToString & "." & vbCrLf & "Please add this data from Patient demographics.  ", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            blnIsValid = False
                        End If
                    End If

                End If

                Return blnIsValid
            Catch ex As Exception
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Return Nothing
            End Try
        End Function

        Public Sub AttemptEPCSDrugCheck(ByRef ogloPrescription As EPrescription, ByVal _clinicname As String, ByVal strPrescriberNADEAN As String)
            Dim ogloInterface As gloSureScriptInterface = Nothing
            Try
                ogloInterface = New gloSureScriptInterface
                ogloInterface.ClinicName = _clinicname
                ogloInterface.VendorName = VendorName
                ogloInterface.VendorLabel = VendorLabel
                ogloInterface.VendorNodeName = VendorNodeName
                ogloInterface.VendorNodeLabel = VendorNodeLabel
                ogloInterface.ApplicationVersion = ApplicationVersion
                If Not IsNothing(ogloPrescription) Then
                    Dim sEPCSDrugCheckFilePath As String = String.Empty
                    For i As Int16 = 0 To ogloPrescription.DrugsCol.Count - 1
                        If ogloPrescription.DrugsCol.Item(i).IsNarcotics > 1 Then
                            If ValidateEPCSData(ogloPrescription, i) = False Then
                                Exit Sub
                            End If

                            Dim _IsEPCSDrugCheckSuccess As Boolean
                            Dim sDEAScheduleCodeOUT As String = Nothing

                            sEPCSDrugCheckFilePath = ogloInterface.GenerateEPCSDrugCheck(ogloPrescription, i, EpcsSeviceCall.WSEPCSDrugCheckService)
                            If sEPCSDrugCheckFilePath <> "" Then
                                Try
                                    _IsEPCSDrugCheckSuccess = GenerateWSEPCSDrugCheckServiceReq(ogloPrescription, i, sEPCSDrugCheckFilePath, "WSEPCSDrugCheckService", sDEAScheduleCodeOUT)
                                    If _IsEPCSDrugCheckSuccess = False Then
                                        gloSurescriptGeneral.sDIBServiceURL = gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL
                                        sEPCSDrugCheckFilePath = ogloInterface.GenerateEPCSDrugCheck(ogloPrescription, i, EpcsSeviceCall.WSEPCSDrugCheckServiceMultiple)
                                        _IsEPCSDrugCheckSuccess = GenerateWSEPCSDrugCheckServiceReqMultiple(ogloPrescription, i, sEPCSDrugCheckFilePath, "WSEPCSDrugCheckService", sDEAScheduleCodeOUT)
                                    End If

                                    If _IsEPCSDrugCheckSuccess = True Then
                                        ''BDO Audit
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Controlled substance drug is valid to transmit electronically", _PatientID, 0, _ProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                    Else
                                        ''BDO Audit
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Controlled substance drug is invalid to transmit electronically", _PatientID, 0, _ProviderId, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                                    End If

                                Catch ex As Exception
                                    Dim err As String = ex.Message
                                    If (err = "NADEAN") Then
                                        ogloPrescription.DrugsCol.Item(i).EPCSDEASchedule = sDEAScheduleCodeOUT

                                        If ogloPrescription.DrugsCol.Item(i).Notes.Contains("NADEAN") Then
                                            _IsEPCSDrugCheckSuccess = True
                                        Else
                                            If strPrescriberNADEAN <> "" Then
                                                ogloPrescription.DrugsCol.Item(i).Notes = "NADEAN : " + strPrescriberNADEAN + " " + ogloPrescription.DrugsCol.Item(i).Notes
                                                _IsEPCSDrugCheckSuccess = True
                                            Else
                                                If MessageBox.Show("This drug requires your Narcotics Addiction DEA Number(NADEAN)" & vbCrLf & "Do you want to enter it now?", "QEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                                    Dim objfrmProviderNADEAN As frmProviderNADEAN = Nothing
                                                    Dim sNADEANNumber As String = ""

                                                    objfrmProviderNADEAN = New frmProviderNADEAN()

                                                    objfrmProviderNADEAN.ShowDialog(objfrmProviderNADEAN.Parent)

                                                    sNADEANNumber = objfrmProviderNADEAN.NADEANNumber

                                                    If sNADEANNumber <> "" Then
                                                        ogloPrescription.DrugsCol.Item(i).Notes = "NADEAN : " + sNADEANNumber + " " + ogloPrescription.DrugsCol.Item(i).Notes

                                                        _IsEPCSDrugCheckSuccess = True
                                                    Else
                                                        MessageBox.Show("Because the prescription " & ogloPrescription.DrugsCol.Item(i).DrugName & " is a Narcotics Addiction DEA Number (NADEAN) it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                    End If

                                                Else
                                                    MessageBox.Show("Because the prescription " & ogloPrescription.DrugsCol.Item(i).DrugName & " is a Narcotics Addiction DEA Number (NADEAN) it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                End If
                                            End If
                                        End If
                                    ElseIf (err = "GHB") Then
                                        MessageBox.Show("Because the prescription " & ogloPrescription.DrugsCol.Item(i).DrugName & " is of type gamma hydroxybutyric acid (GHB), it cannot be sent electronically.  This prescription can be printed and will require a wet signature before handed over to patient.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Else
                                        ''BDO Audit
                                        gloSurescriptGeneral.UpdateLog("Error in EPCS Drug Check Responce: " & ex.Message)
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in WSEPCSDrugCheckService Responce. ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                                        MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    End If
                                End Try
                                ogloPrescription.DrugsCol.Item(i).IsEPCSDrugCheckSuccess = _IsEPCSDrugCheckSuccess
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                'BDO Audit
                gloSurescriptGeneral.UpdateLog("Error in EPCS Drug Check Responce: " & ex.Message)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in WSEPCSDrugCheckService Responce. ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

                Throw ex
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
        End Sub

        Public Sub GenerateFileinQueue(ByRef ogloPrescription As EPrescription, ByVal _clinicname As String, ByRef _RxBusinessLayer As RxBusinesslayer)

            Dim ogloInterface As gloSureScriptInterface = Nothing

            Try
                ogloInterface = New gloSureScriptInterface
                ogloInterface.ClinicName = _clinicname
                ogloInterface.VendorName = VendorName
                ogloInterface.VendorLabel = VendorLabel
                ogloInterface.VendorNodeName = VendorNodeName
                ogloInterface.VendorNodeLabel = VendorNodeLabel
                ogloInterface.ApplicationVersion = ApplicationVersion

                If Not IsNothing(ogloPrescription) Then

                    Dim ReturnSendFilePath As String = ""
                    Dim ReturnSendDNTFFilePath2 As String = ""
                    Dim erxFiles As New List(Of KeyValuePair(Of String, String))()

                    Dim responseMessage As schema.MessageType = Nothing
                    Dim drug As EDrug = Nothing
                    Dim responsetype As Integer

                    Dim count As Integer = ogloPrescription.DrugsCol.Count

                    For rowIndex As Int16 = 0 To ogloPrescription.DrugsCol.Count - 1

                        drug = ogloPrescription.DrugsCol.Item(rowIndex)
                        responsetype = 0

                        ReturnSendFilePath = ""
                        ReturnSendDNTFFilePath2 = ""

                        If drug.MessageType = "NewRx" Then
                            drug.MessageType = gloSureScriptInterface.SentMessageType.eNewRx
                            If drug.IsNarcotics > 1 Then
                                If drug.IsEPCSDrugCheckSuccess = True Then
                                    responsetype = ShowERxdata(ogloPrescription, oOldgloPrescription, count, rowIndex, 0, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode, "")
                                    If responsetype = -1 Then
                                        bDiscardAllclick = True
                                        Exit Sub
                                    End If
                                    If ReturnSendFilePath <> "" Then
                                        drug.eRxFilePath = ReturnSendFilePath
                                        erxFiles.Add(New KeyValuePair(Of String, String)(drug.PrescriptionID, ReturnSendFilePath))
                                        ReturnSendFilePath = ""
                                    Else
                                        drug.eRxFilePath = ogloInterface.GenerateMultipleMU210dot6XMLforNewRx(ogloPrescription, rowIndex, 0)
                                        erxFiles.Add(New KeyValuePair(Of String, String)(drug.PrescriptionID, drug.eRxFilePath))
                                    End If
                                End If
                            Else
                                responsetype = ShowERxdata(ogloPrescription, oOldgloPrescription, count, rowIndex, 0, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode, "")
                                If responsetype = -1 Then
                                    bDiscardAllclick = True
                                    Exit Sub
                                End If
                                If ReturnSendFilePath <> "" Then
                                    drug.eRxFilePath = ReturnSendFilePath
                                    ReturnSendFilePath = ""
                                Else
                                    drug.eRxFilePath = ogloInterface.GenerateMultipleMU210dot6XMLforNewRx(ogloPrescription, rowIndex, 0)
                                End If
                            End If
                        Else
                            If drug.MessageType = "RxChangeRequest" Then
                                If drug.IsNarcotics > 1 Then
                                    If drug.IsEPCSDrugCheckSuccess = True Then
                                        responsetype = CompareCollectionsForRxChange(rowIndex)
                                        responsetype = ShowERxdataForRxChange(ogloPrescription, count, rowIndex, responsetype, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode)
                                    End If
                                Else
                                    responsetype = CompareCollectionsForRxChange(rowIndex)
                                    responsetype = ShowERxdataForRxChange(ogloPrescription, count, rowIndex, responsetype, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode)
                                End If
                            Else
                                If drug.IsNarcotics > 1 Then
                                    If drug.IsEPCSDrugCheckSuccess = True Then
                                        responsetype = CompareCollections(rowIndex)
                                        responsetype = ShowERxdata(ogloPrescription, oOldgloPrescription, count, rowIndex, responsetype, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode, ReturnSendDNTFFilePath2)
                                    End If
                                Else
                                    responsetype = CompareCollections(rowIndex)
                                    responsetype = ShowERxdata(ogloPrescription, oOldgloPrescription, count, rowIndex, responsetype, ReturnSendFilePath, drug.PotencyCode, drug.MDPotencyUnitCode, ReturnSendDNTFFilePath2)
                                End If
                            End If

                            If responsetype = -1 Then
                                bDiscardAllclick = True
                                Exit Sub
                            End If

                            Dim status As String = ""

                            Select Case responsetype
                                Case 0
                                    drug.MessageType = gloSureScriptInterface.SentMessageType.Cancel
                                Case 1
                                    If drug.PhNCPDPID <> oOldgloPrescription.RxPharmacy.PharmacyID Then
                                        drug.MessageType = gloSureScriptInterface.SentMessageType.eDenied
                                    Else
                                        drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
                                    End If
                                Case 2
                                    drug.MessageType = gloSureScriptInterface.SentMessageType.eApproved

                                Case 3
                                    drug.MessageType = gloSureScriptInterface.SentMessageType.eApprovedWithChanges
                                Case 4
                                    drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow
                            End Select

                            If drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow Then
                                If ReturnSendDNTFFilePath2 <> "" Then
                                    drug.eRxFilePath2 = ReturnSendDNTFFilePath2
                                    ReturnSendDNTFFilePath2 = String.Empty
                                Else
                                    drug.eRxFilePath2 = ogloInterface.GenerateRefillResponse10dot6New(oOldgloPrescription, status, "", "")
                                End If
                            End If

                            If drug.IsNarcotics > 1 Then
                                If drug.IsEPCSDrugCheckSuccess = True Then
                                    If ReturnSendFilePath <> "" Then
                                        drug.eRxFilePath = ReturnSendFilePath
                                        erxFiles.Add(New KeyValuePair(Of String, String)(drug.PrescriptionID, ReturnSendFilePath))
                                        ReturnSendFilePath = String.Empty
                                    Else
                                        If drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow Then
                                            Dim oldrowindex As Integer = rowIndex
                                            If (oOldgloPrescription.DrugsCol.Count < oldrowindex) Then
                                                oldrowindex = oOldgloPrescription.DrugsCol.Count - 1
                                            End If
                                            If (oldrowindex >= 0) Then
                                                drug.eRxFilePath = ogloInterface.GenerateMultipleMU210dot6XMLforNewRx(ogloPrescription, rowIndex, 0, "eDeniedWithNewRxToFollow", oOldgloPrescription.DrugsCol.Item(oldrowindex).MessageID)
                                            End If
                                        Else
                                            drug.eRxFilePath = ogloInterface.GenerateRefillResponse10dot6New(ogloPrescription, status, "", "")
                                        End If
                                        erxFiles.Add(New KeyValuePair(Of String, String)(drug.PrescriptionID, drug.eRxFilePath))
                                    End If
                                End If
                            Else
                                If ReturnSendFilePath <> "" Then
                                    drug.eRxFilePath = ReturnSendFilePath
                                    ReturnSendFilePath = ""
                                Else
                                    If drug.MessageType = gloSureScriptInterface.SentMessageType.eDeniedWithNewRxToFollow Then
                                        Dim oldrowindex As Integer = rowIndex
                                        If (oOldgloPrescription.DrugsCol.Count < oldrowindex) Then
                                            oldrowindex = oOldgloPrescription.DrugsCol.Count - 1
                                        End If
                                        If (oldrowindex >= 0) Then
                                            drug.eRxFilePath = ogloInterface.GenerateMultipleMU210dot6XMLforNewRx(ogloPrescription, rowIndex, 0, "eDeniedWithNewRxToFollow", oOldgloPrescription.DrugsCol.Item(oldrowindex).MessageID)
                                        End If
                                    Else
                                        drug.eRxFilePath = ogloInterface.GenerateRefillResponse10dot6New(ogloPrescription, status, "", "")
                                    End If
                                End If
                            End If
                        End If
                    Next

                    If erxFiles.Count > 0 Then
                        ogloPrescription.eRxMultipleFilePath = erxFiles
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " EPCS file creating started.", gloAuditTrail.ActivityOutCome.Success)
                        ogloPrescription.eRxUILaunchSigningFilePath = ogloInterface.GenerateEPCSUIlaunchSigning(ogloPrescription, 0, Nothing, EpcsSeviceCall.UILaunchSigning)

                        If ogloPrescription.eRxUILaunchSigningFilePath <> "" Then
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " EPCS file created Successfully.", gloAuditTrail.ActivityOutCome.Success)
                        Else
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " EPCS file created unSuccessfully.", gloAuditTrail.ActivityOutCome.Success)
                        End If

                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " Prescription status file creation started.", gloAuditTrail.ActivityOutCome.Success)
                        ogloPrescription.eRxWSGetPrescriptionStatusFilePath = ogloInterface.GenerateEPCSWSGetPrescriptionStatus(ogloPrescription, 0, EpcsSeviceCall.WSGetPrescriptionStatus)

                        If ogloPrescription.eRxWSGetPrescriptionStatusFilePath <> "" Then
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " Prescription status file created Successfully.", gloAuditTrail.ActivityOutCome.Success)
                        Else
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, " Failed to create Prescription status file.", gloAuditTrail.ActivityOutCome.Success)
                        End If
                    End If
                End If

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
        End Sub


        Private Function ShowERxdataForRxChange(ByRef ogloPrescription As EPrescription, ByVal count As Integer, ByVal row As Int16, ByVal responseType As Int16, ByRef ReturnSendFilePath As String, ByVal _PotencyCode As String, ByVal _MDPotencyCode As String) As Int16

            Dim objFrmeRxSummary As frmeRxSummary = Nothing
            objFrmeRxSummary = New frmeRxSummary(ogloPrescription, RxChangeRequest.FileDataXML)

            Dim _Result As Int16 = -1
            Try
                objFrmeRxSummary.Text = String.Concat(New String() {"Electronic Rx Review (" & row + 1 & " of " & count & ")"})
                Select Case responseType
                    Case 3 '' Approvewithchanges
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = True
                    Case 1 ''DNTF
                        objFrmeRxSummary.DNTF = True
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 2 ''Approve
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = True
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 0 ''NewRx
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = True
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 4 ''DNTF & Approvewithchanges
                        objFrmeRxSummary.DNTF = True
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = True
                End Select

                'If Not IsNothing(ResponseMessage) = "" Then
                objFrmeRxSummary.dtMedication = dtMedicationData
                objFrmeRxSummary.intIndex = row
                objFrmeRxSummary.PotencyCode = _PotencyCode
                objFrmeRxSummary.MDPotencyCode = _MDPotencyCode

                objFrmeRxSummary.ReturnFilePathtoSendForeRx = ""
                objFrmeRxSummary.ShowDialog(objFrmeRxSummary.Parent)

                ReturnSendFilePath = objFrmeRxSummary.ReturnFilePathtoSendForeRx

                _Result = objFrmeRxSummary.ReturnResponce
                objFrmeRxSummary.Dispose()
                objFrmeRxSummary = Nothing
                'End If

            Catch exception1 As Exception
                MessageBox.Show(exception1.ToString, "gloEMR")

                If Not Information.IsNothing(objFrmeRxSummary) Then
                    objFrmeRxSummary.Dispose()
                    objFrmeRxSummary = Nothing
                End If

                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, exception1.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                exception1 = Nothing
            End Try
            Return _Result
        End Function



        Private Function ShowERxdata(ByRef ogloPrescription As EPrescription, ByRef oOldgloPrescription As EPrescription, ByVal count As Integer, ByVal row As Int16, ByVal responseType As Int16, ByRef ReturnSendFilePath As String, ByVal _PotencyCode As String, ByVal _MDPotencyCode As String, ByRef ReturnSendDNTFFilePath2 As String) As Int16

            Dim objFrmeRxSummary As frmeRxSummary = Nothing

            If IsChangeRequest Then
                objFrmeRxSummary = New frmeRxSummary(ogloPrescription, RxChangeRequest.FileDataXML)
            Else
                objFrmeRxSummary = New frmeRxSummary(ogloPrescription, oOldgloPrescription)
            End If


            Dim _Result As Int16 = -1
            Try
                ' oglointerface = New gloSureScript.gloSureScriptInterface
                'Dim strviewFile As String
                objFrmeRxSummary.Text = String.Concat(New String() {"Electronic Rx Review (" & row + 1 & " of " & count & ")"})
                Select Case responseType
                    Case 3 '' Approvewithchanges
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = True
                    Case 1 ''DNTF
                        objFrmeRxSummary.DNTF = True
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 2 ''Approve
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = True
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 0 ''NewRx
                        objFrmeRxSummary.DNTF = False
                        objFrmeRxSummary.NewRx = True
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = False
                    Case 4 ''DNTF & Approvewithchanges
                        objFrmeRxSummary.DNTF = True
                        objFrmeRxSummary.NewRx = False
                        objFrmeRxSummary.Approve = False
                        objFrmeRxSummary.Approvewithchanges = True
                End Select

                If ReturnSendFilePath = "" Then
                    'strviewFile = (clsgeneral.gstrTempDirPath & DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".xml")
                    'Dim strViewTransformfile As String = ""
                    'File.Copy(eRxFilePath, strviewFile)
                    'strViewTransformfile = oglointerface.ViewXML(strviewFile, _PotencyCode, _MDPotencyCode)
                    'objFrmeRxSummary.XMLFile = strViewTransformfile
                    objFrmeRxSummary.dtMedication = dtMedicationData
                    objFrmeRxSummary.intIndex = row
                    objFrmeRxSummary.PotencyCode = _PotencyCode
                    objFrmeRxSummary.MDPotencyCode = _MDPotencyCode
                    ' objFrmeRxSummary.DNTFMessageID = _DNTFMessageID

                    objFrmeRxSummary.ReturnFilePathtoSendForeRx = ""
                    objFrmeRxSummary.ShowDialog(objFrmeRxSummary.Parent)
                    _Result = objFrmeRxSummary.ReturnResponce
                    ReturnSendFilePath = objFrmeRxSummary.ReturnFilePathtoSendForeRx
                    ReturnSendDNTFFilePath2 = objFrmeRxSummary.ReturnFilePathtoSendDNTFForeRx2
                    objFrmeRxSummary.Dispose()
                    objFrmeRxSummary = Nothing
                    'If File.Exists(strviewFile) Then
                    '    File.Delete(strviewFile)
                    'End If
                End If

            Catch exception1 As Exception
                MessageBox.Show(exception1.ToString, "gloEMR")

                If Not Information.IsNothing(objFrmeRxSummary) Then
                    objFrmeRxSummary.Dispose()
                    objFrmeRxSummary = Nothing
                End If

                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, exception1.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                exception1 = Nothing
            End Try
            Return _Result
        End Function

        Private Function AddComparedata(ByVal sDescription As String, ByVal sMedicationPrescribed As String, ByVal sMedicationDispenced As String)
            Try
                If IsNothing(dtMedicationData) Then
                    dtMedicationData = New DataTable
                    Dim dcDescription As New DataColumn("Description", GetType(String))
                    Dim dcMedPrescribed As New DataColumn("MedicationPrescribed", GetType(String))
                    Dim dcMedDispenced As New DataColumn("MedicationDispenced", GetType(String))

                    dtMedicationData.Columns.Add(dcDescription)
                    dtMedicationData.Columns.Add(dcMedPrescribed)
                    dtMedicationData.Columns.Add(dcMedDispenced)
                End If
                Dim dtRow As DataRow
                dtRow = dtMedicationData.NewRow()
                dtRow.Item("Description") = sDescription
                dtRow.Item("MedicationPrescribed") = sMedicationPrescribed
                dtRow.Item("MedicationDispenced") = sMedicationDispenced
                dtMedicationData.Rows.Add(dtRow)

            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

        Public Function CompareCollectionsForRxChange(ByVal selecteditem As Integer) As Int16
            Dim intResponse As Integer = 0
            Dim ValidationMessageBuilder As System.Text.StringBuilder = Nothing

            Dim sChangeReqXML As String = RxChangeRequest.FileDataXML

            Dim SSChangeRequest As schema.RxChangeRequest = Nothing
            Dim SSMessageData As schema.MessageType = Nothing
            Dim xmlSerializer As Xml.Serialization.XmlSerializer = Nothing

            Dim medRequested As gloGlobal.Schemas.Surescript.RxChangeDispensedMedicationType = RxChangeRequest.MedRequested
            Dim medPrescribed As gloGlobal.Schemas.Surescript.RxChangePrescribedMedicationType = RxChangeRequest.MedPrescribed
            Dim changedMedication As gloSureScript.EDrug = oProcessERxPrescription.DrugsCol.Item(selecteditem)

            If sChangeReqXML IsNot Nothing Then
                Using reader As New StringReader(sChangeReqXML)
                    SSMessageData = New schema.MessageType()
                    xmlSerializer = New Xml.Serialization.XmlSerializer(SSMessageData.GetType())
                    SSMessageData = xmlSerializer.Deserialize(reader)
                End Using
            End If

            If SSMessageData IsNot Nothing Then
                If SSMessageData.Body IsNot Nothing AndAlso SSMessageData.Body.Item IsNot Nothing Then
                    If TypeOf (SSMessageData.Body.Item) Is schema.RxChangeRequest Then
                        SSChangeRequest = DirectCast(SSMessageData.Body.Item, schema.RxChangeRequest)
                    End If
                End If
            End If

            If SSChangeRequest IsNot Nothing Then

                ValidationMessageBuilder = New System.Text.StringBuilder

                Dim requestType As String = SSChangeRequest.Request.ChangeRequestType.ToString()
                Dim sQuantityArray As String() = Nothing
                Dim nMDDuration As String() = Nothing
                Dim sMDDurationUnit As String = ""
                Dim sMDDaysSupply As String = ""

                Dim idenPharmacy As New schema.MandatoryProviderIDType
                idenPharmacy = SSChangeRequest.Pharmacy.Identification
                Dim sPharmacyNPI As String = ""
                Dim sPharmacyNCPDPID As String = ""

                For i As Int16 = 0 To idenPharmacy.Items.Count - 1
                    If idenPharmacy.ItemsElementName(i).ToString().Trim = "NCPDPID" Then
                        sPharmacyNCPDPID = idenPharmacy.Items(i).ToString().Trim
                    End If
                    If idenPharmacy.ItemsElementName(i).ToString().Trim = "NPI" Then
                        sPharmacyNPI = idenPharmacy.Items(i).ToString().Trim
                    End If
                Next

                Dim sPrescriberNPI As String = Nothing
                Dim sPrescriberDEA As String = Nothing
                Dim sPrescriberSSN As String = Nothing
                Dim sPrescriberPhone As String = Nothing
                Dim sPrescriberFax As String = Nothing

                For Each comnitem As schema.CommunicationType In SSChangeRequest.Prescriber.CommunicationNumbers
                    If comnitem IsNot Nothing Then
                        If comnitem.Qualifier.Trim = "TE" Then
                            sPrescriberPhone = comnitem.Number.Trim
                        ElseIf comnitem.Qualifier.Trim = "FX" Then
                            sPrescriberFax = comnitem.Number.Trim
                        End If
                    End If
                Next

                Dim PrescriberIdend As New schema.MandatoryProviderIDType
                PrescriberIdend = SSChangeRequest.Prescriber.Identification

                For i As Int16 = 0 To PrescriberIdend.Items.Count - 1
                    If PrescriberIdend.ItemsElementName(i).ToString().Trim = "NPI" Then
                        sPrescriberNPI = PrescriberIdend.Items(i).ToString().Trim
                    End If

                    If PrescriberIdend.ItemsElementName(i).ToString().Trim = "DEANumber" Then
                        sPrescriberDEA = PrescriberIdend.Items(i).ToString().Trim
                    End If
                    If PrescriberIdend.ItemsElementName(i).ToString().Trim = "StateLicenseNumber" Then
                        sPrescriberSSN = PrescriberIdend.Items(i).ToString().Trim
                    End If
                Next

                Dim bMaySubstitute As Boolean = False

                If medRequested IsNot Nothing Then
                    If medRequested.Quantity.Value IsNot Nothing Then
                        sQuantityArray = medRequested.Quantity.Value.ToString.Split(" ") ' oOldgloPrescription.DrugsCol.Item(0).MDQuantity.Split(" ")
                    End If

                    If medRequested.Substitutions IsNot Nothing AndAlso medRequested.Substitutions = "0" Then
                        bMaySubstitute = True
                    Else
                        bMaySubstitute = False
                    End If

                    If medRequested.DaysSupply IsNot Nothing Then
                        sMDDaysSupply = medRequested.DaysSupply.Trim
                    End If
                ElseIf medPrescribed IsNot Nothing Then
                    If medPrescribed.Quantity.Value IsNot Nothing Then
                        sQuantityArray = medPrescribed.Quantity.Value.ToString.Split(" ") ' oOldgloPrescription.DrugsCol.Item(0).MDQuantity.Split(" ")
                    End If

                    If medPrescribed.Substitutions IsNot Nothing AndAlso medPrescribed.Substitutions = "0" Then
                        bMaySubstitute = True
                    Else
                        bMaySubstitute = False
                    End If

                    If medPrescribed.DaysSupply IsNot Nothing Then
                        sMDDaysSupply = medPrescribed.DaysSupply.Trim
                    End If
                End If

                If (requestType = "T") Then
                    If medRequested IsNot Nothing Then
                        If medRequested.Directions IsNot Nothing Then
                            If changedMedication.Directions.Trim.ToUpper <> medRequested.Directions.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug Directions ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If

                        If sQuantityArray.Count >= 0 Then
                            If changedMedication.DrugQuantity.Trim.ToUpper <> sQuantityArray(0).ToUpper Then
                                ValidationMessageBuilder.Append("Drug Quantity ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If

                        If changedMedication.DrugDuration.Trim.ToUpper <> sMDDaysSupply.Trim.ToUpper Then    '''''oOldgloPrescription.DrugsCol.Item(0).MDDuration
                            ValidationMessageBuilder.Append("Drug Duration ")
                            intResponse = 3 'ApprovewithChanges
                        End If

                        If medRequested.Quantity.PotencyUnitCode IsNot Nothing Then
                            If changedMedication.PotencyCode.Trim.ToUpper <> medRequested.Quantity.PotencyUnitCode.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug Potency Code ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If

                        If medRequested.DrugCoded.ProductCode IsNot Nothing Then
                            If changedMedication.ProdCode.Trim.ToUpper <> medRequested.DrugCoded.ProductCode.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug NDCCode ")
                                intResponse = 3 'ApprovewithChanges
                            End If

                        End If

                        If medRequested.Refills.Value IsNot Nothing Then
                            If medRequested.Refills.Value.Trim <> "" Then
                                If medRequested.Refills.Value.Trim <> "0" Then
                                    If Val(changedMedication.RefillQuantity.Trim.ToUpper) <> Val(medRequested.Refills.Value.Trim.ToUpper) Then
                                        ValidationMessageBuilder.Append("Refill Quantity ")
                                        intResponse = 3 'Approve with changes
                                    End If
                                End If
                            End If
                        End If

                        If medRequested.DrugCoded.ProductCode IsNot Nothing Then
                            If medRequested.DrugCoded.ProductCode.Trim.Length <> 11 Then
                                ValidationMessageBuilder.Append("Invalid NDC-11 formatted value ")
                                intResponse = 3 'Approve with changes
                            End If
                        End If

                    ElseIf medPrescribed IsNot Nothing Then
                        If medPrescribed.Directions IsNot Nothing Then
                            If changedMedication.Directions.Trim.ToUpper <> medPrescribed.Directions.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug Directions ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If


                        If sQuantityArray.Count >= 0 Then
                            If changedMedication.DrugQuantity.Trim.ToUpper <> sQuantityArray(0).ToUpper Then
                                ValidationMessageBuilder.Append("Drug Quantity ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If

                        If changedMedication.DrugDuration.Trim.ToUpper <> sMDDaysSupply.Trim.ToUpper Then    '''''oOldgloPrescription.DrugsCol.Item(0).MDDuration
                            ValidationMessageBuilder.Append("Drug Duration ")
                            intResponse = 3 'ApprovewithChanges
                        End If

                        If medPrescribed.Quantity.PotencyUnitCode IsNot Nothing Then
                            If changedMedication.PotencyCode.Trim.ToUpper <> medPrescribed.Quantity.PotencyUnitCode.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug Potency Code ")
                                intResponse = 3 'ApprovewithChanges
                            End If
                        End If

                        If medPrescribed.DrugCoded.ProductCode IsNot Nothing Then
                            If changedMedication.ProdCode.Trim.ToUpper <> medPrescribed.DrugCoded.ProductCode.Trim.ToUpper Then
                                ValidationMessageBuilder.Append("Drug NDCCode ")
                                intResponse = 3 'ApprovewithChanges
                            End If

                        End If

                        If medPrescribed.Refills.Value IsNot Nothing Then
                            If medPrescribed.Refills.Value.Trim <> "" Then
                                If medPrescribed.Refills.Value.Trim <> "0" Then
                                    If Val(changedMedication.RefillQuantity.Trim) <> Val(medPrescribed.Refills.Value.Trim) Then
                                        ValidationMessageBuilder.Append("Refill Quantity ")
                                        intResponse = 3 'Approve with changes
                                    End If
                                End If
                            End If
                        End If

                        If medPrescribed.DrugCoded.ProductCode IsNot Nothing Then
                            If medPrescribed.DrugCoded.ProductCode.Trim.Length <> 11 Then
                                ValidationMessageBuilder.Append("Invalid NDC-11 formatted value ")
                                intResponse = 3 'Approve with changes
                            End If
                        End If
                    Else
                        intResponse = 3
                    End If

                ElseIf (requestType = "G") Then
                    intResponse = 2

                ElseIf (requestType = "P") Then
                    intResponse = 2
                End If

                If intResponse = 3 Then
                    If Not IsNothing(dtMedicationData) Then
                        dtMedicationData.Dispose()
                        dtMedicationData = Nothing
                    End If

                    If medRequested IsNot Nothing Then
                        AddComparedata("Drug", changedMedication.DrugName.Trim, medRequested.DrugDescription.Trim)
                        AddComparedata("Refills", changedMedication.RefillQuantity, medRequested.Refills.Value)
                        AddComparedata("RefillQualifier", changedMedication.RefillsQualifier, medRequested.Refills.Qualifier)
                        AddComparedata("Substitution flag", If(changedMedication.MaySubstitute = True, "Yes", "No"), If(bMaySubstitute = True, "Yes", "No"))
                        AddComparedata("Pharmacy Notes", changedMedication.Notes.Trim, If(medRequested.Note IsNot Nothing, medRequested.Note.Trim, ""))
                        AddComparedata("Pharmacy NCPDPID", changedMedication.PhNCPDPID.Trim, If(sPharmacyNCPDPID IsNot Nothing, sPharmacyNCPDPID.Trim, ""))
                        AddComparedata("Drug Directions", changedMedication.Directions.Trim, If(medRequested.Directions IsNot Nothing, medRequested.Directions.Trim, ""))
                        AddComparedata("Drug Quantity", changedMedication.DrugQuantity.Trim, If(sQuantityArray.Count >= 0, sQuantityArray(0), ""))
                        AddComparedata("Drug Duration", If(changedMedication.DrugDuration.Trim <> "", changedMedication.DrugDuration.Trim & " days", ""), If(sMDDaysSupply <> "", sMDDaysSupply.Trim & " days", ""))
                        AddComparedata("Drug Potency Code", changedMedication.PotencyCode.Trim, If(medRequested.Quantity.PotencyUnitCode IsNot Nothing, medRequested.Quantity.PotencyUnitCode.Trim, ""))
                        AddComparedata("Drug NDCCode", changedMedication.ProdCode.Trim, If(medRequested.DrugCoded.ProductCode IsNot Nothing, medRequested.DrugCoded.ProductCode.Trim, ""))
                        AddComparedata("PharmacyName", changedMedication.PharmacyName, SSChangeRequest.Pharmacy.StoreName)

                    ElseIf medPrescribed IsNot Nothing Then
                        AddComparedata("Drug", changedMedication.DrugName.Trim, medPrescribed.DrugDescription.Trim)
                        AddComparedata("Refills", changedMedication.RefillQuantity, medPrescribed.Refills.Value)
                        AddComparedata("RefillQualifier", changedMedication.RefillsQualifier, medPrescribed.Refills.Qualifier)
                        AddComparedata("Substitution flag", If(changedMedication.MaySubstitute = True, "Yes", "No"), If(bMaySubstitute = True, "Yes", "No"))
                        AddComparedata("Pharmacy Notes", changedMedication.Notes.Trim, If(medPrescribed.Note IsNot Nothing, medPrescribed.Note.Trim, ""))
                        AddComparedata("Pharmacy NCPDPID", changedMedication.PhNCPDPID.Trim, If(sPharmacyNCPDPID IsNot Nothing, sPharmacyNCPDPID.Trim, ""))
                        AddComparedata("Drug Directions", changedMedication.Directions.Trim, If(medPrescribed.Directions IsNot Nothing, medPrescribed.Directions.Trim, ""))
                        AddComparedata("Drug Quantity", changedMedication.DrugQuantity.Trim, If(sQuantityArray.Count >= 0, sQuantityArray(0), ""))
                        AddComparedata("Drug Duration", If(changedMedication.DrugDuration.Trim <> "", changedMedication.DrugDuration.Trim & " days", ""), If(sMDDaysSupply <> "", sMDDaysSupply.Trim & " days", ""))
                        AddComparedata("Drug Potency Code", changedMedication.PotencyCode.Trim, If(medPrescribed.Quantity.PotencyUnitCode IsNot Nothing, medPrescribed.Quantity.PotencyUnitCode.Trim, ""))
                        AddComparedata("Drug NDCCode", changedMedication.ProdCode.Trim, If(medPrescribed.DrugCoded.ProductCode IsNot Nothing, medPrescribed.DrugCoded.ProductCode.Trim, ""))
                        AddComparedata("PharmacyName", changedMedication.PharmacyName, SSChangeRequest.Pharmacy.StoreName)
                    End If

                    AddComparedata("NPI", oProcessERxPrescription.RxPrescriber.PrescriberNPI.Trim, If(sPrescriberNPI IsNot Nothing, sPrescriberNPI, ""))
                    AddComparedata("DEA", oProcessERxPrescription.RxPrescriber.PrescriberDEA.Trim, If(sPrescriberDEA IsNot Nothing, sPrescriberDEA, ""))
                    AddComparedata("SSN", oProcessERxPrescription.RxPrescriber.PrescriberSSN.Trim, If(sPrescriberSSN IsNot Nothing, sPrescriberSSN, ""))
                    AddComparedata("PrescriberAdd1", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address1.Trim, If(SSChangeRequest.Prescriber.Address.AddressLine1 IsNot Nothing, SSChangeRequest.Prescriber.Address.AddressLine1.Trim, ""))
                    AddComparedata("PrescriberAdd2", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address2.Trim, If(SSChangeRequest.Prescriber.Address.AddressLine2 IsNot Nothing, SSChangeRequest.Prescriber.Address.AddressLine2.Trim, ""))
                    AddComparedata("PrescriberCity", oProcessERxPrescription.RxPrescriber.PrescriberAddress.City.Trim, If(SSChangeRequest.Prescriber.Address.City IsNot Nothing, SSChangeRequest.Prescriber.Address.City.Trim, ""))
                    AddComparedata("PrescriberState", oProcessERxPrescription.RxPrescriber.PrescriberAddress.State.Trim, If(SSChangeRequest.Prescriber.Address.State IsNot Nothing, SSChangeRequest.Prescriber.Address.State.Trim, ""))
                    AddComparedata("PrescriberZip", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Zip.Trim, If(SSChangeRequest.Prescriber.Address.ZipCode IsNot Nothing, SSChangeRequest.Prescriber.Address.ZipCode.Trim, ""))
                    AddComparedata("PrescriberPhone", oProcessERxPrescription.RxPrescriber.PrescriberPhone.Phone.Trim, If(sPrescriberPhone IsNot Nothing, sPrescriberPhone, ""))
                    AddComparedata("PrescriberFax", oProcessERxPrescription.RxPrescriber.PrescriberPhone.Fax.Trim, If(sPrescriberFax IsNot Nothing, sPrescriberFax, ""))

                Else
                    intResponse = 2
                End If

            End If

            Return intResponse

        End Function

        Public Function CompareCollections(ByVal selecteditem As Integer) As Int16
            Dim intResponse As Integer = 0
            Dim ValidationMessageBuilder As System.Text.StringBuilder = Nothing
            Dim objdrug As gloSureScript.EDrug = Nothing
            Try

                If Not IsNothing(oProcessERxPrescription) Then
                    If PrescriptionCol.Count > 0 Then
                        For icnt As Int16 = 0 To oProcessERxPrescription.DrugsCol.Count - 1
                            If icnt = selecteditem Then
                                Dim blnDTNF_ApprooveWithChanges As Boolean = False
                                Dim blnDTNF As Boolean = False
                                Dim blnApprovewithChanges As Boolean = False
                                ValidationMessageBuilder = New System.Text.StringBuilder
                                If oOldgloPrescription.RxPrescriber.PrescriberName.FirstName.Trim <> "" Then
                                    If oProcessERxPrescription.RxPrescriber.PrescriberName.FirstName.Trim.Length > 35 Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberName.FirstName.Trim.ToUpper.Substring(0, 35) <> oOldgloPrescription.RxPrescriber.PrescriberName.FirstName.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber First Name ")
                                            intResponse = 1
                                            blnDTNF = True
                                        End If
                                    Else
                                        If oProcessERxPrescription.RxPrescriber.PrescriberName.FirstName.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberName.FirstName.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber First Name ")
                                            intResponse = 1
                                            blnDTNF = True
                                        End If
                                    End If
                                End If
                                If oOldgloPrescription.RxPrescriber.PrescriberName.LastName.Trim <> "" Then
                                    If oProcessERxPrescription.RxPrescriber.PrescriberName.LastName.Trim.Length > 35 Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberName.LastName.Trim.ToUpper.Substring(0, 35) <> oOldgloPrescription.RxPrescriber.PrescriberName.LastName.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber Last Name ")
                                            intResponse = 1
                                            blnDTNF = True
                                        End If
                                    Else
                                        If oProcessERxPrescription.RxPrescriber.PrescriberName.LastName.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberName.LastName.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber Last Name ")
                                            intResponse = 1
                                            blnDTNF = True
                                        End If
                                    End If
                                End If
                                'Dim nDaysSupply As String = ""
                                'Dim sDurationUnit As String = ""


                                objdrug = oProcessERxPrescription.DrugsCol.Item(icnt)


                                'If objdrug.DrugDuration.Length > 0 AndAlso Val(objdrug.DrugDuration) <> 0 Then
                                '    Dim nDuration As String() = Nothing
                                '    nDuration = objdrug.DrugDuration.Trim.Split(" ")
                                '    If nDuration.Length > 0 Then
                                '        If nDuration.Length <= 1 Then
                                '            nDaysSupply = nDuration(0)
                                '            sDurationUnit = ""
                                '        Else
                                '            nDaysSupply = nDuration(0)
                                '            sDurationUnit = nDuration(1)
                                '        End If
                                '    End If
                                'Else
                                '    If objdrug.DrugDuration = "" Then
                                '        nDaysSupply = objdrug.DrugDuration
                                '    Else
                                '        Dim nDuration As String() = Nothing
                                '        nDuration = objdrug.DrugDuration.Trim.Split(" ")
                                '        If nDuration.Length > 0 Then
                                '            If nDuration.Length <= 1 Then
                                '                nDaysSupply = nDuration(0)
                                '                sDurationUnit = ""
                                '            Else
                                '                nDaysSupply = nDuration(0)
                                '                sDurationUnit = nDuration(1)
                                '            End If
                                '        End If
                                '    End If
                                'End If

                                Dim sQuantityArray As String()
                                sQuantityArray = oOldgloPrescription.DrugsCol.Item(0).MDQuantity.Split(" ")


                                If objdrug.MaySubstitute <> oOldgloPrescription.DrugsCol.Item(0).MDbMaySubstitutions Then
                                    ValidationMessageBuilder.Append("Substitution flag ")
                                    intResponse = 1 'DNTF
                                    blnDTNF = True
                                End If
                                If objdrug.Notes.Trim.ToUpper <> oOldgloPrescription.DrugsCol.Item(0).MDNotes.Trim.ToUpper Then
                                    ValidationMessageBuilder.Append("Pharmacy Notes ")
                                    intResponse = 1 'DNTF
                                    blnDTNF = True
                                End If
                                If objdrug.PhNCPDPID.Trim.ToUpper <> oOldgloPrescription.RxPharmacy.PharmacyID.Trim.ToUpper Then
                                    ValidationMessageBuilder.Append("Pharmacy NCPDPID ")
                                    intResponse = 1 'DNTF 
                                    blnDTNF = True
                                End If
                                If objdrug.Directions.Trim.ToUpper <> oOldgloPrescription.DrugsCol.Item(0).MDFrequency.Trim.ToUpper Then
                                    ValidationMessageBuilder.Append("Drug Directions ")
                                    intResponse = 1 'DNTF
                                    blnDTNF = True
                                End If
                                If objdrug.DrugQuantity.Trim.ToUpper <> sQuantityArray(0).ToUpper Then
                                    ValidationMessageBuilder.Append("Drug Quantity ")
                                    intResponse = 1 'DNTF
                                    blnDTNF = True
                                End If

                                Dim nMDDuration As String() = Nothing
                                Dim sMDDurationUnit As String = ""
                                Dim nMDDaysSupply As String = ""
                                nMDDuration = oOldgloPrescription.DrugsCol.Item(0).MDDuration.Trim.Split(" ")
                                If nMDDuration.Length > 0 Then
                                    If nMDDuration.Length <= 1 Then
                                        nMDDaysSupply = nMDDuration(0)
                                        sMDDurationUnit = ""
                                    Else
                                        nMDDaysSupply = nMDDuration(0)
                                        sMDDurationUnit = nMDDuration(1)
                                    End If
                                End If

                                If objdrug.DrugDuration.Trim <> nMDDaysSupply.Trim Then    '''''oOldgloPrescription.DrugsCol.Item(0).MDDuration
                                    ValidationMessageBuilder.Append("Drug Duration ")
                                    intResponse = 1 'DNTF
                                    blnDTNF = True
                                End If

                                If objdrug.PotencyCode.Trim.ToUpper <> oOldgloPrescription.DrugsCol.Item(0).MDPotencyUnitCode.Trim.ToUpper Then
                                    ValidationMessageBuilder.Append("Drug Potency Code ")
                                    intResponse = 1 'DNTF 
                                    blnDTNF = True
                                End If





                                If blnDTNF = False And blnDTNF_ApprooveWithChanges = False Then

                                    If objdrug.ProdCode.Trim.ToUpper <> oOldgloPrescription.DrugsCol.Item(0).MDProductCode.Trim.ToUpper Then
                                        ValidationMessageBuilder.Append("Drug NDCCode ")
                                        intResponse = 3 'ApprovewithChanges
                                        blnApprovewithChanges = True
                                    End If

                                    ' If blnDTNF = False Then
                                    If oOldgloPrescription.DrugsCol.Item(0).RefillQuantity.Trim <> "" Then
                                        If oOldgloPrescription.DrugsCol.Item(0).RefillQuantity.Trim <> "0" Then
                                            If Val(objdrug.RefillQuantity.Trim) <> Val(oOldgloPrescription.DrugsCol.Item(0).RefillQuantity.Trim) Then
                                                ValidationMessageBuilder.Append("Refill Quantity ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        End If
                                    End If

                                    If oOldgloPrescription.RxPrescriber.PrescriberNPI.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberNPI.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberNPI.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber NPI ")
                                            intResponse = 3 'Approve with changes
                                        End If
                                    End If

                                    If blnIsEPCSEnable = True Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberDEA.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberDEA.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber DEA ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    Else
                                        If oOldgloPrescription.RxPrescriber.PrescriberDEA.Trim <> "" Then
                                            If oProcessERxPrescription.RxPrescriber.PrescriberDEA.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberDEA.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber DEA ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        End If
                                    End If

                                    If blnIsEPCSEnable = True Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberSSN.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberSSN.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber SSN ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    Else
                                        If oOldgloPrescription.RxPrescriber.PrescriberSSN.Trim <> "" Then
                                            If oProcessERxPrescription.RxPrescriber.PrescriberSSN.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberSSN.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber SSN ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        End If
                                    End If

                                    If oOldgloPrescription.RxPrescriber.PrescriberAddress.Address1.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.Length > 35 Then
                                            If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.ToUpper.Substring(0, 35) <> oOldgloPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber Address1 ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        Else
                                            If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberAddress.Address1.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber Address1 ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberAddress.Address2.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.Trim.Length > 35 Then
                                            If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.ToUpper.Substring(0, 35) <> oOldgloPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber Address2 ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        Else
                                            If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberAddress.Address2.Trim.ToUpper Then
                                                ValidationMessageBuilder.Append("Prescriber Address2 ")
                                                intResponse = 3 'Approve with changes
                                                blnApprovewithChanges = True
                                            End If
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberAddress.City.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberAddress.City.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberAddress.City.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber City ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberAddress.State.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberAddress.State.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberAddress.State.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber State ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberAddress.Zip.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberAddress.Zip.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber Zip ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberPhone.Phone.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberPhone.Phone.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberPhone.Phone.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber Phone ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    End If
                                    If oOldgloPrescription.RxPrescriber.PrescriberPhone.Fax.Trim <> "" Then
                                        If oProcessERxPrescription.RxPrescriber.PrescriberPhone.Fax.Trim.ToUpper <> oOldgloPrescription.RxPrescriber.PrescriberPhone.Fax.Trim.ToUpper Then
                                            ValidationMessageBuilder.Append("Prescriber Fax ")
                                            intResponse = 3 'Approve with changes
                                            blnApprovewithChanges = True
                                        End If
                                    End If

                                    If oOldgloPrescription.DrugsCol.Item(0).ProdCode.Trim.Length <> 11 Then
                                        ValidationMessageBuilder.Append("Invalid NDC-11 formatted value ")
                                        intResponse = 3 'Approve with changes
                                        blnApprovewithChanges = True
                                    End If



                                    If blnDTNF = False And blnApprovewithChanges = False And blnDTNF_ApprooveWithChanges = False Then
                                        intResponse = 2 ''Approve
                                    End If

                                End If

                                If intResponse = 1 Or intResponse = 3 Or intResponse = 4 Then

                                    If Not IsNothing(dtMedicationData) Then
                                        dtMedicationData.Dispose()
                                        dtMedicationData = Nothing
                                    End If

                                    'AddComparedata("Drug", objdrug.DrugName & " " & objdrug.DrugStrength & " " & objdrug.StrengthUnits, oOldgloPrescription.DrugsCol.Item(0).MDDrugName)
                                    AddComparedata("Drug", objdrug.DrugName, oOldgloPrescription.DrugsCol.Item(0).MDDrugName)
                                    AddComparedata("Refills", objdrug.RefillQuantity, oOldgloPrescription.DrugsCol.Item(0).MDRefillQuantity)
                                    If objdrug.RefillsQualifier = "P" Or objdrug.RefillsQualifier = "R" Or objdrug.RefillsQualifier = "" Then
                                        AddComparedata("RefillQualifier", "A", oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier)
                                    Else
                                        AddComparedata("RefillQualifier", objdrug.RefillsQualifier, oOldgloPrescription.DrugsCol.Item(0).MDRefillQualifier)
                                    End If

                                    AddComparedata("Substitution flag", If(objdrug.MaySubstitute = True, "Yes", "No"), If(oOldgloPrescription.DrugsCol.Item(0).MDbMaySubstitutions = True, "Yes", "No"))
                                    AddComparedata("Pharmacy Notes", objdrug.Notes.Trim, oOldgloPrescription.DrugsCol.Item(0).MDNotes.Trim)
                                    AddComparedata("Pharmacy NCPDPID", objdrug.PhNCPDPID.Trim, oOldgloPrescription.RxPharmacy.PharmacyID.Trim)
                                    AddComparedata("Drug Directions", objdrug.Directions.Trim, oOldgloPrescription.DrugsCol.Item(0).MDFrequency.Trim)
                                    AddComparedata("Drug Quantity", objdrug.DrugQuantity.Trim, sQuantityArray(0))
                                    AddComparedata("Drug Duration", If(objdrug.DrugDuration.Trim <> "", objdrug.DrugDuration.Trim & " days", ""), If(oOldgloPrescription.DrugsCol.Item(0).MDDuration.Trim = "", "", oOldgloPrescription.DrugsCol.Item(0).MDDuration & " days"))
                                    AddComparedata("Drug Potency Code", objdrug.PotencyCode.Trim, oOldgloPrescription.DrugsCol.Item(0).MDPotencyUnitCode.Trim)
                                    AddComparedata("Drug NDCCode", objdrug.ProdCode.Trim, oOldgloPrescription.DrugsCol.Item(0).MDProductCode.Trim)

                                    AddComparedata("NPI", oProcessERxPrescription.RxPrescriber.PrescriberNPI.Trim, oOldgloPrescription.RxPrescriber.PrescriberNPI.Trim)
                                    AddComparedata("DEA", oProcessERxPrescription.RxPrescriber.PrescriberDEA.Trim, oOldgloPrescription.RxPrescriber.PrescriberDEA.Trim)
                                    AddComparedata("SSN", oProcessERxPrescription.RxPrescriber.PrescriberSSN.Trim, oOldgloPrescription.RxPrescriber.PrescriberSSN.Trim)
                                    AddComparedata("PrescriberAdd1", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address1.Trim, oOldgloPrescription.RxPrescriber.PrescriberAddress.Address1.Trim)
                                    AddComparedata("PrescriberAdd2", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Address2.Trim, oOldgloPrescription.RxPrescriber.PrescriberAddress.Address2.Trim)
                                    AddComparedata("PrescriberCity", oProcessERxPrescription.RxPrescriber.PrescriberAddress.City.Trim, oOldgloPrescription.RxPrescriber.PrescriberAddress.City.Trim)
                                    AddComparedata("PrescriberState", oProcessERxPrescription.RxPrescriber.PrescriberAddress.State.Trim, oOldgloPrescription.RxPrescriber.PrescriberAddress.State.Trim)
                                    AddComparedata("PrescriberZip", oProcessERxPrescription.RxPrescriber.PrescriberAddress.Zip.Trim, oOldgloPrescription.RxPrescriber.PrescriberAddress.Zip.Trim)
                                    AddComparedata("PrescriberPhone", oProcessERxPrescription.RxPrescriber.PrescriberPhone.Phone.Trim, oOldgloPrescription.RxPrescriber.PrescriberPhone.Phone.Trim)
                                    AddComparedata("PrescriberFax", oProcessERxPrescription.RxPrescriber.PrescriberPhone.Fax.Trim, oOldgloPrescription.RxPrescriber.PrescriberPhone.Fax.Trim)

                                    AddComparedata("PharmacyName", objdrug.PharmacyName, oOldgloPrescription.RxPharmacy.PharmacyName.Trim)

                                    'Using oApprove As New frmConfirmApproove(dtMedicationData)
                                    '    oApprove.ShowDialog()
                                    '    intResponse = oApprove.nResponce
                                    '    blnDTNF = True
                                    'End Using
                                End If
                            End If ''icnt
                        Next
                    End If
                End If


            Catch ex As Exception
                Throw ex
            Finally
                'If Not IsNothing(dtMedicationData) Then
                '    dtMedicationData.Dispose()
                '    dtMedicationData = Nothing
                'End If
            End Try
            Return intResponse
        End Function

        Dim WithEvents ss_helper As gloSureScript.gloSurescriptsHelper = Nothing


        'Public Sub CancelRxRequest(Index As Integer, erxMessageId As String, PrescriptionID As String, dseRx As DataSet)
        '    Dim RetMsg As String = Nothing


        '    MessageBox.Show(RetMsg)
        '    'Dim _eRxStatus As String = ""
        '    'Dim _eRxStatusMessage As String = ""
        '    'If ogloInterface.StatusMessageType.Length > 0 Then
        '    '    ' System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

        '    '    _eRxStatus = ogloInterface.MessageName
        '    '    _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
        '    '    If _eRxStatus <> "" Then

        '    '        'Select Case _eRxStatus
        '    '        '    Case "Status"
        '    '        '        _eRxStatus = "Posted Successfully"
        '    '        '    Case "Verify" 'for case verify we have same value in message variable as of status case
        '    '        '        _eRxStatus = "Posted Successfully"
        '    '        '    Case "Error"
        '    '        '        _eRxStatus = "Could not be Posted Successfully"
        '    '        '        _eRxStatusMessage = ogloInterface.StatusMessageType
        '    '        'End Select

        '    '        Select Case _eRxStatus
        '    '            Case "Status"
        '    '                Select Case ogloInterface.MessagestatusCode
        '    '                    Case "000"
        '    '                        _eRxStatus = "Transmission successful"
        '    '                    Case "010"
        '    '                        _eRxStatus = "Successfully accepted by ultimate receiver"
        '    '                    Case Else
        '    '                        _eRxStatus = "Posted Successfully"
        '    '                End Select

        '    '                If DNTF = False Then
        '    '                    If ogloPrescription.DrugsCol.Item(i).PCPrograms IsNot Nothing Then
        '    '                        PDRProgramsToPrint.Add(ogloPrescription.DrugsCol.Item(i).PCPrograms)
        '    '                    End If
        '    '                End If


        '    '            Case "Verify" 'for case verify we have same value in message variable as of status case
        '    '                _eRxStatus = "Posted Successfully"

        '    '                If DNTF = False Then
        '    '                    If ogloPrescription.DrugsCol.Item(i).PCPrograms IsNot Nothing Then
        '    '                        PDRProgramsToPrint.Add(ogloPrescription.DrugsCol.Item(i).PCPrograms)
        '    '                    End If
        '    '                End If

        '    '            Case "Error"
        '    '                _eRxStatus = "Could not be Posted Successfully"
        '    '                _eRxStatusMessage = ogloInterface.StatusMessageType
        '    '        End Select
        '    '    End If
        '    'End If

        '    'If gloSurescriptGeneral._isInternetAvailable = False Then
        '    '    If InternetMsgCnt = 0 Then
        '    '        System.Windows.Forms.MessageBox.Show("This eRx will not be sent now as no internet connection is available. It will be queued and sent when internet connection will again be detected. Do not send it again.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        '    '        InternetMsgCnt = InternetMsgCnt + 1
        '    '    End If

        '    '    _eRxStatus = "Internet connection not available"
        '    '    _eRxStatusMessage = "Internet connection not available"
        '    '    ogloPrescription.DrugsCol.Item(i).TransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID

        '    '    oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(i), SureScriptMessage))
        '    '    'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
        '    '    UpdatePrescription(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).PrescriptionID) '
        '    '    RaiseEvent UpdateRx_eRxStatus(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).ProdCode, ogloPrescription.DrugsCol.Item(i).ItemNumber) 'bug 13911 in 6030


        '    'Else
        '    '    ogloPrescription.DrugsCol.Item(i).TransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID
        '    '    oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(i), SureScriptMessage))

        '    '    If ogloPrescription.DrugsCol.Item(i).MessageType <> gloSureScriptInterface.SentMessageType.eNewRx Then
        '    '        ogloPrescription.RxTransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID
        '    '        ''ogloPrescription.DrugsCol.Item(i).EPCSDNTFRelatesToMessageId = ogloInterface.ResEPCSDNTFRelatesToMessageId ''To Send In DNTF in NewRx
        '    '        If IsChangeRequest AndAlso ogloInterface.MessageName = "Status" Then
        '    '            Using p As New PrescriptionBusinessLayer()
        '    '                p.UpdateRxChangeStatus(Me.RxChangeRequest.MessageID, ogloPrescription.DrugsCol.Item(i).MessageType)
        '    '            End Using
        '    '        Else
        '    '            If ogloInterface.MessageName = "Status" Then
        '    '                ogloInterface.UpdateRefillStatus(ogloPrescription, ogloPrescription.DrugsCol.Item(i).MessageType, i)
        '    '            End If
        '    '        End If
        '    '        For j As Integer = 0 To PrescriptionCol.Count - 1
        '    '            If ogloPrescription.DrugsCol.Item(i).ItemNumber = PrescriptionCol.Item(j).ItemNumber Then
        '    '                PrescriptionCol.Item(j).MessageType = ""
        '    '            End If
        '    '        Next
        '    '        'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
        '    '    End If
        '    '    UpdatePrescription(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).PrescriptionID) '
        '    '    RaiseEvent UpdateRx_eRxStatus(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).ProdCode, ogloPrescription.DrugsCol.Item(i).ItemNumber) 'bug 13911 in 6030

        '    'End If

        'End Sub

        Public Sub PostFileinQueue(ByRef ogloPrescription As EPrescription, ByVal requestType As String)
            Dim strstatusmessage1 As String = ""
            Dim strstatusmessage2 As String = ""
            Dim strSucessfulMessage As String = ""
            Dim strUnSucessfulMessage As String = ""
            '' Dim _sRelatesToMessageId As String = ""
            Dim bIsDNTF As Boolean = False
            Try
                PDRProgramsToPrint.Clear()

                For i As Int16 = 0 To ogloPrescription.DrugsCol.Count - 1

                    Dim drug As EDrug = ogloPrescription.DrugsCol.Item(i)

                    If drug.IsNarcotics < 2 Then  ''Don't Send CS drug to SS 

                        If drug.eRxFilePath2 <> "" Then
                            strstatusmessage2 = SendFileAndGetResponse(ogloPrescription, i, True, requestType)
                            bIsDNTF = True
                        End If

                        If drug.eRxFilePath <> "" Then
                            strstatusmessage1 = SendFileAndGetResponse(ogloPrescription, i, False, requestType)
                        End If

                        If strstatusmessage2 <> "" And strstatusmessage1 <> "" Then
                            strstatusmessage1 = strstatusmessage2 & vbCrLf & vbCrLf & strstatusmessage1
                        End If

                        If strstatusmessage1.Contains("Could not be Posted Successfully") Then ''Or strstatusmessage1.Contains("Error : ")
                            Dim enumResponseType As gloSureScriptInterface.SentMessageType
                            [Enum].TryParse(ogloPrescription.DrugsCol.Item(i).MessageType, enumResponseType)

                            Dim sResponseString As String = ""
                            sResponseString = Me.GetResponseString(enumResponseType)

                            If ogloPrescription.DrugsCol.Item(i).MessageName = "RxChangeResponse" Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeResponse, gloAuditTrail.ActivityType.Send, sResponseString + "RxChange response could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                            ElseIf ogloPrescription.DrugsCol.Item(i).MessageName = "RefillResponse" Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, sResponseString + "Refill response could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                            ElseIf ogloPrescription.DrugsCol.Item(i).MessageName = "NewRx" Then
                                If bIsDNTF Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, "Denied With NewRx to follow Refill response could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                                End If
                            End If

                            If strUnSucessfulMessage <> "" Then
                                strUnSucessfulMessage = strUnSucessfulMessage & vbCrLf & vbCrLf & strstatusmessage1
                            Else
                                strUnSucessfulMessage = strstatusmessage1
                            End If
                        Else
                            Dim enumResponseType As gloSureScriptInterface.SentMessageType
                            Dim sResponseString As String = ""

                            [Enum].TryParse(ogloPrescription.DrugsCol.Item(i).MessageType, enumResponseType)
                            sResponseString = Me.GetResponseString(enumResponseType)

                            If ogloPrescription.DrugsCol.Item(i).MessageName = "RxChangeResponse" Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeResponse, gloAuditTrail.ActivityType.Send, sResponseString + "RxChange response sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                            ElseIf ogloPrescription.DrugsCol.Item(i).MessageName = "RefillResponse" Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, sResponseString + "Refill response sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                            ElseIf ogloPrescription.DrugsCol.Item(i).MessageName = "NewRx" Then
                                If bIsDNTF Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, "Denied With NewRx to follow Refill response sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(i).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                                End If
                            End If

                            If strSucessfulMessage <> "" Then
                                strSucessfulMessage = strSucessfulMessage & vbCrLf & vbCrLf & strstatusmessage1
                            Else
                                strSucessfulMessage = strstatusmessage1
                            End If
                        End If
                    Else
                        '' --- EPCS Deny message Send
                        If drug.eRxFilePath2 <> "" Then
                            If drug.EPCSPrescriptionStatusLabel <> "FAILURE" Then
                                strstatusmessage2 = SendFileAndGetResponse(ogloPrescription, i, True, requestType)
                                bIsDNTF = True
                            End If
                        End If
                    End If   ''*** end Don't Send CS drug to SS 
                Next

                ''''------ *** For EPCS File Send Call ***
                If ogloPrescription.eRxUILaunchSigningFilePath <> "" Then
                    LaunchUISigning(ogloPrescription.eRxUILaunchSigningFilePath)
                    PostEPCSWSGetPrescriptionStatusFileinQueue(ogloPrescription)
                End If

                For j As Int16 = 0 To ogloPrescription.DrugsCol.Count - 1
                    If ogloPrescription.DrugsCol.Item(j).IsNarcotics > 1 Then   '' For CS Drug only
                        If Not IsNothing(ogloPrescription.DrugsCol.Item(j).EPCSPrescriptionStatusMessage) Then
                            strstatusmessage1 = ogloPrescription.DrugsCol.Item(j).EPCSPrescriptionStatusMessage
                        Else
                            strstatusmessage1 = ""
                        End If

                        If Not IsNothing(ogloPrescription.DrugsCol.Item(j).EPCSPrescriptionStatusLabel) Then
                            If ogloPrescription.DrugsCol.Item(j).EPCSPrescriptionStatusLabel <> "FAILURE" Then
                                ogloPrescription.DrugsCol.Item(j).IsERXSuccessful = True
                                Dim enumResponseType As gloSureScriptInterface.SentMessageType
                                [Enum].TryParse(ogloPrescription.DrugsCol.Item(j).MessageType, enumResponseType)

                                Dim sResponseString As String = ""
                                sResponseString = Me.GetResponseString(enumResponseType)

                                If ogloPrescription.DrugsCol.Item(j).MessageName = "RxChangeResponse" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeResponse, gloAuditTrail.ActivityType.Send, sResponseString + "RxChange response for controlled substance sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                                ElseIf ogloPrescription.DrugsCol.Item(j).MessageName = "RefillResponse" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, sResponseString + "Refill response for controlled substance sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                                ElseIf ogloPrescription.DrugsCol.Item(j).MessageName = "NewRx" Then
                                    If bIsDNTF Then
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, "Denied With NewRx to follow Refill response for controlled substance sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Success)
                                    End If
                                End If

                                If Not ECPSeRxDrugs.Contains(ogloPrescription.DrugsCol.Item(j).PrescriptionID) AndAlso ogloPrescription.DrugsCol.Item(j).IsNarcotics > 1 Then
                                    ECPSeRxDrugs.Add(ogloPrescription.DrugsCol.Item(j).PrescriptionID)
                                End If

                                SetEPCSStatusMessage(ogloPrescription, j, "Successfully accepted by ultimate receiver")
                                If ogloPrescription.DrugsCol.Item(j).PCPrograms IsNot Nothing Then
                                    PDRProgramsToPrint.Add(ogloPrescription.DrugsCol.Item(j).PCPrograms)
                                End If
                            Else
                                Dim enumResponseType As gloSureScriptInterface.SentMessageType
                                [Enum].TryParse(ogloPrescription.DrugsCol.Item(j).MessageType, enumResponseType)

                                Dim sResponseString As String = ""
                                sResponseString = Me.GetResponseString(enumResponseType)

                                If ogloPrescription.DrugsCol.Item(j).MessageName = "RxChangeResponse" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeResponse, gloAuditTrail.ActivityType.Send, sResponseString + "RxChange response for controlled substance could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                                ElseIf ogloPrescription.DrugsCol.Item(j).MessageName = "RefillResponse" Then
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, sResponseString + "Refill response for controlled substance could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                                ElseIf ogloPrescription.DrugsCol.Item(j).MessageName = "NewRx" Then
                                    If bIsDNTF Then
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, "Denied With NewRx to follow Refill response for controlled substance could not be sent", ogloPrescription.PatientID, ogloPrescription.DrugsCol.Item(j).PrescriptionID, ogloPrescription.ProviderID, gloAuditTrail.ActivityOutCome.Failure)
                                    End If
                                End If

                                UpdatePrescription("Could not be Posted Successfully", "", ogloPrescription.DrugsCol.Item(j).PrescriptionID)
                                RaiseEvent UpdateRx_eRxStatus("Could not be Posted Successfully", "", ogloPrescription.DrugsCol.Item(j).ProdCode, ogloPrescription.DrugsCol.Item(j).ItemNumber, ogloPrescription.DrugsCol.Item(j).IseRxed)
                                For k As Integer = 0 To PrescriptionCol.Count - 1
                                    If ogloPrescription.DrugsCol.Item(j).ItemNumber = PrescriptionCol.Item(k).ItemNumber Then
                                        PrescriptionCol.Item(k).FlagtoDeletePrescription = False
                                    End If
                                Next
                            End If
                            'End If
                        End If
                        If strstatusmessage2 <> "" And strstatusmessage1 <> "" Then
                            strstatusmessage1 = strstatusmessage2 & vbCrLf & vbCrLf & strstatusmessage1
                        End If
                        If strstatusmessage1.Contains("Error : ") Then ''strstatusmessage1.Contains("Could not be Posted Successfully") Or
                            If strUnSucessfulMessage <> "" Then
                                strUnSucessfulMessage = strUnSucessfulMessage & vbCrLf & vbCrLf & strstatusmessage1
                            Else
                                strUnSucessfulMessage = strstatusmessage1
                            End If
                        Else
                            If strSucessfulMessage <> "" Then
                                strSucessfulMessage = strSucessfulMessage & vbCrLf & vbCrLf & strstatusmessage1
                            Else
                                strSucessfulMessage = strstatusmessage1
                            End If
                        End If
                    End If
                Next

                ''***-- End EPCS Call****
                If strSucessfulMessage.Length > 0 Then
                    System.Windows.Forms.MessageBox.Show(strSucessfulMessage, "Prescription", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                End If
                If strUnSucessfulMessage.Length > 0 Then
                    System.Windows.Forms.MessageBox.Show(strUnSucessfulMessage, "Prescription", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                End If


                RaiseEvent PrintPDRPrograms(PDRProgramsToPrint)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub LaunchUISigning(ByVal eRxUILaunchSigningFilePath As String)
            Try
                If eRxUILaunchSigningFilePath <> "" Then
                    Using frmEPCSUI As New frmlaunchEPCSui("UILaunchSigning")
                        frmEPCSUI.urlForEpcs = EpcsGoldUrl
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "EPCS UI Launched", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        frmEPCSUI.StrfileName = eRxUILaunchSigningFilePath  '_RxBusinessLayer.oProcessERxPrescription.DrugsCol.Item(j).eRxUILaunchSigningFilePath
                        frmEPCSUI.blnStagingServer = True
                        ' Dim obj As New clsencryption
                        ''obj.EncryptToBase64String(ConfigurationManager.AppSettings("sharedSecret"), "123456789")
                        frmEPCSUI.DecryptedSharedSecretKey = DecryptFromBase64String(SharedSecretKey, "123456789")   '"17f3587cd0bf48958f5034ce73302585"
                        'obj = Nothing
                        frmEPCSUI.ShowDialog()
                    End Using
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Dim key() As Byte = {} ' we are going to pass in the key portion in our method calls
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Public Function DecryptFromBase64String(ByVal stringToDecrypt As String, ByVal sEncryptionKey As String) As String
            Dim inputByteArray(stringToDecrypt.Length) As Byte
            ' Note: The DES CryptoService only accepts certain key byte lengths
            ' We are going to make things easy by insisting on an 8 byte legal key length
            Dim des As New DESCryptoServiceProvider
            Dim ms As New MemoryStream
            Dim cs As CryptoStream = Nothing
            Try
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8))

                ' we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                inputByteArray = Convert.FromBase64String(stringToDecrypt)
                ' now decrypt the regular string

                cs = New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch e As Exception
                Return e.Message
            Finally
                des.Dispose()
                ms.Dispose()
                If (IsNothing(cs) = False) Then
                    cs.Dispose()
                End If
            End Try
        End Function

        Public Function GenerateWSEPCSDrugCheckServiceReqMultiple(ByRef ogloPrescription As EPrescription, ByVal i As Int16, ByVal strfilepath As String, serviceName As String, Optional ByRef sDEAScheduleCodeOUT As String = Nothing) As Boolean

            Dim oSSGenral As New gloSureScript.gloSurescriptGeneral
            Dim sStatus As Boolean
            Dim _EPCSDEASchedule As String = Nothing
            Dim sDEAScheduleCode As String = Nothing

            Try
                gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
                gloSurescriptGeneral.checkDownloadVersion()
                Dim strfilename As String = oSSGenral.ExtractXMLEpcs(oSSGenral.ConvertFiletoBinaryEpcs(strfilepath, serviceName))

                If strfilename <> "" Then
                    Dim _res As SS_Resp_WSEPCSDrugCheckServiceMultiple.EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType
                    _res = ReadResponseOfEpcsDrugCheckMultiple(strfilename, EpcsSeviceCall.WSEPCSDrugCheckService, ogloPrescription.PatientID.ToString(), ogloPrescription.DrugsCol.Item(i).DrugName, sDEAScheduleCode)

                    ogloPrescription.DrugsCol.Item(i).EPCSDEASchedule = _res.DEASchedule
                    ogloPrescription.DrugsCol.Item(i).ProdCode = _res.NdcId
                    _Prescriptions.Item(i).NDCCode = _res.NdcId     '''''''Update NDC in Collection in EPCS Case
                    gloSurescriptGeneral.UpdatePresMedNDCs(_res.NdcId, ogloPrescription.DrugsCol.Item(i).PrescriptionID) ''''Update NDC Used In Prescription and  Medication  

                    If _res.DEASchedule = "" Then
                        gloSurescriptGeneral.UpdatePrescriptionStatusForCS("FAILURE", ogloPrescription.DrugsCol.Item(i).PrescriptionID)
                        MessageBox.Show(_res.Status, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        sStatus = False
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error in Drug Check response.", gloAuditTrail.ActivityOutCome.Failure)
                    Else
                        sStatus = True
                    End If
                    Return sStatus
                Else
                    Return False
                End If

            Catch ex As Exception
                sDEAScheduleCodeOUT = sDEAScheduleCode
                ''BDO Audit
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error while Posting Prescription Status File.", ogloPrescription.PatientID.ToString(), 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                Throw ex
            Finally
                If Not IsNothing(oSSGenral) Then
                    oSSGenral = Nothing
                End If
            End Try
        End Function

        Public Shared Function GenerateWSEPCSDrugCheckServiceReq(ByRef ogloPrescription As EPrescription, ByVal i As Int16, ByVal strfilepath As String, serviceName As String, Optional ByRef sDEAScheduleCodeOUT As String = Nothing) As Boolean

            Dim oSSGenral As New gloSureScript.gloSurescriptGeneral
            Dim sStatus As Boolean
            Dim sDEAScheduleCode As String = Nothing

            Try
                gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
                gloSurescriptGeneral.checkDownloadVersion()
                Dim strfilename As String = oSSGenral.ExtractXMLEpcs(oSSGenral.ConvertFiletoBinaryEpcs(strfilepath, serviceName))

                If strfilename <> "" Then

                    Dim _EPCSDEASchedule As String = Nothing
                    _EPCSDEASchedule = ReadResponseOfEpcsDrugCheck(strfilename, EpcsSeviceCall.WSEPCSDrugCheckService, ogloPrescription.PatientID.ToString(), ogloPrescription.DrugsCol.Item(i).DrugName, sDEAScheduleCode)

                    If _EPCSDEASchedule = "" Then
                        sStatus = False
                    Else
                        sStatus = True
                        ogloPrescription.DrugsCol.Item(i).EPCSDEASchedule = _EPCSDEASchedule
                    End If

                    Return sStatus
                Else
                    Return False
                End If

            Catch ex As Exception
                sDEAScheduleCodeOUT = sDEAScheduleCode
                ''BDO Audit
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error while Posting Prescription Status File.", ogloPrescription.PatientID.ToString(), 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                Throw ex
            Finally
                If Not IsNothing(oSSGenral) Then
                    oSSGenral = Nothing
                End If
            End Try

        End Function

        Public Shared Function GenerateWSEPCSWSGetPrescriptionStatusReq(ByVal strfilepath As String, serviceName As String, ByRef ogloPrescription As EPrescription) As Boolean

            Dim oSSGenral As New gloSureScript.gloSurescriptGeneral
            Dim sStatus As Boolean
            Try
                gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
                gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
                gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
                gloSurescriptGeneral.checkDownloadVersion()
                Dim strfilename As String = oSSGenral.ExtractXMLEpcs(oSSGenral.ConvertFiletoBinaryEpcs(strfilepath, serviceName))
                If strfilename <> "" Then
                    sStatus = ReadResponseOfEpcs(strfilename, serviceName, EpcsSeviceCall.WSGetPrescriptionStatus, ogloPrescription)

                    Return sStatus
                Else
                    Return False
                End If
            Catch ex As Exception
                ''BDO Audit
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error while Posting EPCS Drug Check Service File.", ogloPrescription.PatientID.ToString(), 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                Throw New GloSurescriptException(ex.Message)
            Finally
                If Not IsNothing(oSSGenral) Then
                    oSSGenral = Nothing
                End If
            End Try

        End Function

        Public Sub PostEPCSWSGetPrescriptionStatusFileinQueue(ByRef ogloPrescription As EPrescription)

            Try
                If ogloPrescription.eRxWSGetPrescriptionStatusFilePath <> "" Then
                    ' ''BDO Audit
                    GenerateWSEPCSWSGetPrescriptionStatusReq(ogloPrescription.eRxWSGetPrescriptionStatusFilePath, "WSGetPrescriptionStatus", ogloPrescription)
                End If
            Catch ex As Exception
                ''BDO Audit
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.eRx, gloAuditTrail.ActivityType.EpcseRx, "Error while posting Prescription Status File.", _PatientID, 0, _ProviderId, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                Throw ex
            End Try
        End Sub

        Public PDRProgramsToPrint As New List(Of ProgramResponse)

        Public Function SendFileAndGetResponse(ByRef ogloPrescription As EPrescription, ByVal i As Integer, ByVal DNTF As Boolean, ByVal requestType As String) As String
            gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
            gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
            gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
            gloSurescriptGeneral.checkDownloadVersion()
            Dim ogloInterface As gloSureScriptInterface = Nothing
            Dim oSurescriptDBLayer As gloSureScriptDBLayer = Nothing
            Dim sOldDrugName As String = ""
            Dim bSuccess As Boolean = True
            Try
                ogloInterface = New gloSureScriptInterface
                oSurescriptDBLayer = New gloSureScriptDBLayer

                If DNTF Then
                    Using oOldDrug As EDrug = oOldgloPrescription.DrugsCol(0)
                        sOldDrugName = oOldDrug.DrugName
                    End Using
                    bSuccess = ogloInterface.PostXMLFile(ogloPrescription, i, DNTF, requestType, sOldDrugName)
                Else
                    bSuccess = ogloInterface.PostXMLFile(ogloPrescription, i, DNTF, requestType)
                End If

                If bSuccess Then
                    If ogloPrescription.DrugsCol.Item(i).eRxFilePath <> "" Then
                        If ogloPrescription.DrugsCol.Item(i).eRxFilePath <> "" Then
                            Dim _eRxStatus As String = ""
                            Dim _eRxStatusMessage As String = ""
                            If ogloInterface.StatusMessageType.Length > 0 Then
                                ' System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

                                _eRxStatus = ogloInterface.MessageName
                                _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                                If _eRxStatus <> "" Then

                                    'Select Case _eRxStatus
                                    '    Case "Status"
                                    '        _eRxStatus = "Posted Successfully"
                                    '    Case "Verify" 'for case verify we have same value in message variable as of status case
                                    '        _eRxStatus = "Posted Successfully"
                                    '    Case "Error"
                                    '        _eRxStatus = "Could not be Posted Successfully"
                                    '        _eRxStatusMessage = ogloInterface.StatusMessageType
                                    'End Select

                                    Select Case _eRxStatus
                                        Case "Status"
                                            Select Case ogloInterface.MessagestatusCode
                                                Case "000"
                                                    _eRxStatus = "Transmission successful"
                                                Case "010"
                                                    _eRxStatus = "Successfully accepted by ultimate receiver"
                                                Case Else
                                                    _eRxStatus = "Posted Successfully"
                                            End Select

                                            If DNTF = False Then
                                                If ogloPrescription.DrugsCol.Item(i).PCPrograms IsNot Nothing Then
                                                    PDRProgramsToPrint.Add(ogloPrescription.DrugsCol.Item(i).PCPrograms)
                                                End If
                                            End If
                                            PrescriptionCol.Item(i).IseRxed = PrescriptionCol.Item(i).IseRxed + 1
                                            TmpCheckStatesCol.Item(i).IseRxed = TmpCheckStatesCol.Item(i).IseRxed + 1
                                            ogloPrescription.DrugsCol.Item(i).IseRxed = ogloPrescription.DrugsCol.Item(i).IseRxed + 1
                                            ogloPrescription.DrugsCol.Item(i).IsERXSuccessful = True
                                        Case "Verify" 'for case verify we have same value in message variable as of status case
                                            _eRxStatus = "Posted Successfully"

                                            If DNTF = False Then
                                                If ogloPrescription.DrugsCol.Item(i).PCPrograms IsNot Nothing Then
                                                    PDRProgramsToPrint.Add(ogloPrescription.DrugsCol.Item(i).PCPrograms)
                                                End If
                                            End If
                                            PrescriptionCol.Item(i).IseRxed = PrescriptionCol.Item(i).IseRxed + 1
                                            TmpCheckStatesCol.Item(i).IseRxed = TmpCheckStatesCol.Item(i).IseRxed + 1
                                            ogloPrescription.DrugsCol.Item(i).IseRxed = ogloPrescription.DrugsCol.Item(i).IseRxed + 1
                                            ogloPrescription.DrugsCol.Item(i).IsERXSuccessful = True
                                        Case "Error"
                                            _eRxStatus = "Could not be Posted Successfully"
                                            _eRxStatusMessage = ogloInterface.StatusMessageType
                                            ogloPrescription.DrugsCol.Item(i).IsERXSuccessful = False
                                    End Select
                                End If
                            End If

                            If gloSurescriptGeneral._isInternetAvailable = False Then
                                If InternetMsgCnt = 0 Then
                                    System.Windows.Forms.MessageBox.Show("This eRx will not be sent now as no internet connection is available. It will be queued and sent when internet connection will again be detected. Do not send it again.", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                    InternetMsgCnt = InternetMsgCnt + 1
                                End If

                                _eRxStatus = "Internet connection not available"
                                _eRxStatusMessage = "Internet connection not available"
                                ogloPrescription.DrugsCol.Item(i).TransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID

                                oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(i), SureScriptMessage))
                                'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                                UpdatePrescription(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).PrescriptionID) '
                                RaiseEvent UpdateRx_eRxStatus(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).ProdCode, ogloPrescription.DrugsCol.Item(i).ItemNumber, ogloPrescription.DrugsCol.Item(i).IseRxed) 'bug 13911 in 6030


                            Else
                                ogloPrescription.DrugsCol.Item(i).TransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID
                                oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(i), SureScriptMessage))

                                If ogloPrescription.DrugsCol.Item(i).MessageType <> gloSureScriptInterface.SentMessageType.eNewRx Then
                                    ogloPrescription.RxTransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID
                                    ''ogloPrescription.DrugsCol.Item(i).EPCSDNTFRelatesToMessageId = ogloInterface.ResEPCSDNTFRelatesToMessageId ''To Send In DNTF in NewRx
                                    If IsChangeRequest AndAlso ogloInterface.MessageName = "Status" Then
                                        Using p As New PrescriptionBusinessLayer()
                                            p.UpdateRxMessageStatus(Me.RxChangeRequest.MessageID, ogloPrescription.DrugsCol.Item(i).MessageType, "RxChange")
                                        End Using
                                    Else
                                        If ogloInterface.MessageName = "Status" Then
                                            ogloInterface.UpdateRefillStatus(ogloPrescription, ogloPrescription.DrugsCol.Item(i).MessageType, i)
                                        End If
                                    End If
                                    For j As Integer = 0 To PrescriptionCol.Count - 1
                                        If ogloPrescription.DrugsCol.Item(i).ItemNumber = PrescriptionCol.Item(j).ItemNumber Then
                                            If ogloPrescription.DrugsCol.Item(i).MessageName = "RxChangeResponse" OrElse ogloPrescription.DrugsCol.Item(i).MessageName = "RefillResponse" Then
                                                If ogloPrescription.DrugsCol.Item(i).IsERXSuccessful Then
                                                    PrescriptionCol.Item(j).MessageType = ""
                                                End If
                                            Else
                                                PrescriptionCol.Item(j).MessageType = ""
                                            End If
                                        End If
                                    Next
                                    'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                                End If
                                If (ogloInterface.MessageName <> "Error") Then
                                    UpdatePrescription(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).PrescriptionID) '
                                    RaiseEvent UpdateRx_eRxStatus(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).ProdCode, ogloPrescription.DrugsCol.Item(i).ItemNumber, ogloPrescription.DrugsCol.Item(i).IseRxed) 'bug 13911 in 6030
                                End If
                            End If

                        End If
                    End If
                End If

                Return ogloInterface.StatusMessageType

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
                If Not IsNothing(oSurescriptDBLayer) Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
            End Try
        End Function

        Public Function RxHistory_ERxPrescription(ByVal RxHist_eRxPrescription As Prescriptions) As Boolean
            Dim ogloPrescription As EPrescription = Nothing
            Dim objDrug As gloSureScript.EDrug = Nothing
            Dim oSurescriptDBLayer As New gloSureScriptDBLayer
            Dim ValidateDrugBuilder As System.Text.StringBuilder = Nothing

            Dim ogloInterface As gloSureScriptInterface = Nothing
            gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
            'gloSurescriptGeneral.sUserName = globalSecurity.gstrLoginName
            'gloSurescriptGeneral.sPassword = globalSecurity.gstrLoginPassword
            gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
            gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer

            Dim _eRxStatus As String = ""
            Dim _eRxStatusMessage As String = ""
            Try
                If Not IsNothing(RxHist_eRxPrescription) Then
                    _Prescriptions = RxHist_eRxPrescription
                End If
                If _Prescriptions.Count > 0 Then
                    Dim ClinicName As String = GetClinicName()
                    ogloInterface = New gloSureScriptInterface
                    Dim ierxcnt As Int32 = 0
                    For icnt As Int32 = 0 To _Prescriptions.Count - 1
                        If _Prescriptions.Item(icnt).Status <> "D" Then

                            Dim blnscheduleddrug As Boolean = False
                            Select Case _Prescriptions.Item(icnt).IsNarcotics

                                Case 2, 3, 4, 5
                                    If blnIsEPCSEnable = False Then
                                        '                                        System.Windows.Forms.MessageBox.Show(_Prescriptions.Item(icnt).Medication & " is a Schedule drug ,hence cannot be eRxed", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                                        'because the medication is a controlled substance, it cannot be sent electronically. The Rx will be printed and needs a wet signature before it can be faxed to the pharmacy or handed to the patient
                                        System.Windows.Forms.MessageBox.Show("Because the medication " & _Prescriptions.Item(icnt).Medication & " is a controlled substance, it cannot be sent electronically. The Rx will be printed and needs a wet signature before it can be faxed to the pharmacy or handed to the patient", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                                        blnscheduleddrug = True
                                    End If
                            End Select

                            If blnscheduleddrug = False Then
                                'SLR: Free exisitng memeory and then
                                If Not ValidateDrugBuilder Is Nothing Then
                                    ValidateDrugBuilder = Nothing
                                End If
                                ValidateDrugBuilder = New System.Text.StringBuilder
                                'SLR: Free exisitng memeory and then
                                If Not ogloPrescription Is Nothing Then
                                    ogloPrescription.Dispose()
                                    ogloPrescription = Nothing
                                End If
                                ogloPrescription = New EPrescription
                                'SLR: Free exisitng memeory and then
                                If Not objDrug Is Nothing Then
                                    objDrug.Dispose()
                                    objDrug = Nothing
                                End If
                                objDrug = New gloSureScript.EDrug

                                ogloPrescription.RxTransactionID = _Prescriptions.Item(icnt).PrescriptionID

                                'get patient provider id if it is 0
                                If _Prescriptions.Item(icnt).ProviderID = 0 Then
                                    Dim _nPatProvid As Long = GetProviderID(_Prescriptions.Item(icnt).PatientID)
                                    ogloPrescription.RxPrescriber.ProviderID = _nPatProvid
                                Else
                                    ogloPrescription.RxPrescriber.ProviderID = _Prescriptions.Item(icnt).ProviderID
                                End If


                                ogloPrescription.RxPharmacy.ContactID = _Prescriptions.Item(icnt).PhContactID
                                ogloPrescription.ClinicName = ClinicName

                                'For De - Normalization
                                '-------commented to remove the dependancy from the drugs table, retrieve it from the _Prescriptions collection object.
                                'objDrug.Drugform = GetDosageformCode(_Prescriptions.Item(icnt).DrugID)
                                objDrug.Drugform = _Prescriptions.Item(icnt).DosageForm
                                'For De - Normalization

                                If IsNothing(objDrug.Drugform) Then
                                    'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent because the Drug NDC Number is not available,Please Provide Drug NDC Number for this drug", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                    ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug form is not available for " & _Prescriptions.Item(icnt).Medication & vbCrLf)

                                    'RaiseEvent SetFocusInControl(icnt)
                                    'Exit Function
                                ElseIf objDrug.Drugform = "" Then
                                    ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug form is not available for " & _Prescriptions.Item(icnt).Medication & vbCrLf)

                                End If
                                Dim ndcnumber As String = ""

                                'For De - Normalization
                                '-------commented becaz instead of using the NDC code from the function GetNDCNumber, retrieve it from the _Prescriptions collection object.
                                'ndcnumber = GetNDCNumber(_Prescriptions.Item(icnt).DrugID)
                                ndcnumber = _Prescriptions.Item(icnt).NDCCode

                                'For De - Normalization
                                If ndcnumber = "" Then
                                    'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent because the Drug NDC Number is not available,Please Provide Drug NDC Number for this drug", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                    ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug NDC Number is not available for " & _Prescriptions.Item(icnt).Medication & vbCrLf)

                                    'RaiseEvent SetFocusInControl(icnt)
                                    'Exit Function
                                Else
                                    objDrug.ProdCode = ndcnumber
                                    objDrug.ProdCodeQualifier = "ND"
                                End If

                                objDrug.DrugStrength = _Prescriptions.Item(icnt).Dosage

                                If ValidateDrugBuilder.Length <= 0 Then

                                    If Not IsNothing(objDrug.DrugStrength) Then
                                        If objDrug.DrugStrength.Trim.Length = 0 Then
                                            'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent becuase the Drug Dosage is not available,Please Update Drug Dosage", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                            'RaiseEvent SetFocusInControl(icnt)
                                            'Exit Function
                                            ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug Dosage is not available for " & _Prescriptions.Item(icnt).Medication & ",Please Update Drug Dosage" & vbCrLf)
                                        End If
                                    Else
                                        'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent becuase the Drug Dosage is not available,Please Update Drug Dosage", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        'RaiseEvent SetFocusInControl(icnt)
                                        'Exit Function
                                        ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug Dosage is not available for " & _Prescriptions.Item(icnt).Medication & ",Please Update Drug Dosage" & vbCrLf)

                                    End If

                                    objDrug.DrugFrequency = _Prescriptions.Item(icnt).Frequency
                                    If Not IsNothing(objDrug.DrugFrequency) Then
                                        If objDrug.DrugFrequency.Trim.Length = 0 Then
                                            'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent because the Drug Directions is not available,Please Provide Directions for this drug", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

                                            'RaiseEvent SetFocusInControl(icnt)
                                            'Exit Function
                                            ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug Directions is not available for " & _Prescriptions.Item(icnt).Medication & ",Please Provide Directions for this drug" & vbCrLf)

                                        End If
                                    Else
                                        'System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent because the Drug Directions is not available,Please Provide Directions for this drug", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        'RaiseEvent SetFocusInControl(icnt)
                                        'Exit Function
                                        ValidateDrugBuilder.Append("This Prescription Request cannot be sent because the Drug Directions is not available for " & _Prescriptions.Item(icnt).Medication & ",Please Provide Directions for this drug" & vbCrLf)

                                    End If
                                    If ValidateDrugBuilder.Length <= 0 Then


                                        oSurescriptDBLayer.GetPrescriptionPatientPharmacy(ogloPrescription)

                                        objDrug.DrugName = _Prescriptions.Item(icnt).Medication

                                        Dim strDuration As String = ""
                                        If Not IsNothing(_Prescriptions.Item(icnt).Duration) Then
                                            Dim retval As String() = SplitDrug(_Prescriptions.Item(icnt).Duration.Trim)

                                            If Not IsNothing(retval) Then
                                                If retval.Length > 1 Then
                                                    strDuration = retval(0)
                                                Else
                                                    strDuration = _Prescriptions.Item(icnt).Duration.Trim
                                                End If

                                            Else
                                                strDuration = _Prescriptions.Item(icnt).Duration
                                            End If
                                        Else
                                            strDuration = ""
                                        End If


                                        objDrug.Dosage = _Prescriptions.Item(icnt).Dosage

                                        'objDrug.DrugRoute = _Prescriptions.Item(icnt).Route
                                        'If Not IsNothing(objDrug.DrugRoute) Then
                                        '    If objDrug.DrugRoute.Trim.Length = 0 Then
                                        '        System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent becuase the Drug Route is not available,Please Update Drug Route", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        '        Exit Function
                                        '    End If
                                        'Else
                                        '    System.Windows.Forms.MessageBox.Show("This Prescription Request cannot be sent becuase the Drug Route is not available,Please Update Drug Route", "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        '    Exit Function
                                        'End If
                                        objDrug.DrugDuration = strDuration

                                        If _Prescriptions.Item(icnt).Amount.Trim <> "" Then 'fixed bug 5453
                                            Dim strDispense As String() = Split(_Prescriptions.Item(icnt).Amount.Trim, " ")
                                            If strDispense.Length > 1 Then
                                                objDrug.DrugQuantity = strDispense(0)
                                                'txtDoseUnit.Text = strDispense(1)
                                            Else
                                                objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                                'txtDoseUnit.Text = ""
                                            End If
                                        Else
                                            objDrug.DrugQuantity = _Prescriptions.Item(icnt).Amount.Trim
                                            'txtDoseUnit.Text = ""
                                        End If

                                        '                                    objDrug.Directions = objDrug.DrugStrength.Trim & " " & objDrug.DrugRoute.Trim & " " & objDrug.DrugFrequency.Trim & " for " & _Prescriptions.Item(icnt).Duration.Trim
                                        objDrug.Directions = objDrug.DrugFrequency.Trim & " " & objDrug.DrugRoute.Trim

                                        objDrug.RefillQuantity = _Prescriptions.Item(icnt).Refills.Trim
                                        If IsNothing(_Prescriptions.Item(icnt).RefillQualifier) Then
                                            _Prescriptions.Item(icnt).RefillQualifier = "R"
                                            '' Bug #70507: 00000723: NewRx missing Refill value Tag. Changes added to check blank value.
                                        ElseIf _Prescriptions.Item(icnt).RefillQualifier.Trim() = "" Then
                                            _Prescriptions.Item(icnt).RefillQualifier = "R"
                                        End If
                                        objDrug.RefillsQualifier = _Prescriptions.Item(icnt).RefillQualifier
                                        objDrug.MaySubstitute = _Prescriptions.Item(icnt).Maysubstitute
                                        objDrug.WrittenDate = _Prescriptions.Item(icnt).Prescriptiondate
                                        objDrug.PrescriptionID = _Prescriptions.Item(icnt).PrescriptionID
                                        objDrug.MessageFrom = "mailto:" & ogloPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                                        objDrug.MessageTo = "mailto:" & ogloPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
                                        objDrug.TransactionID = _Prescriptions.Item(icnt).PrescriptionID
                                        objDrug.IseRxed = _Prescriptions.Item(icnt).IseRxed


                                        objDrug.Notes = _Prescriptions.Item(icnt).Notes
                                        ogloPrescription.DrugsCol.Add(objDrug)

                                        If ogloInterface.GenerateXMLforNewRx(ogloPrescription, 0, ierxcnt) Then

                                            If ogloInterface.StatusMessageType.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

                                                '_eRxStatus = ogloInterface.MessageName
                                                '_eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                                                'If _eRxStatus <> "" Then

                                                '    Select Case _eRxStatus
                                                '        Case "Status"
                                                '            _eRxStatus = "Posted Sucessfully"
                                                '        Case "Verify" 'for case verify we have same value in message variable as of status case
                                                '            _eRxStatus = "Posted Sucessfully"
                                                '        Case "Error"
                                                '            _eRxStatus = "Error"

                                                '    End Select

                                                'End If
                                            End If

                                            ogloPrescription.DrugsCol.Item(0).TransactionID = ogloPrescription.DrugsCol.Item(0).PrescriptionID
                                            oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(0), SureScriptMessage))

                                            ''Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                                            'UpdatePrescription(_eRxStatus, _Prescriptions.Item(icnt).PrescriptionID) '
                                            'RaiseEvent UpdateRx_eRxStatus(_eRxStatus, objDrug.ProdCode)

                                            'Else
                                            '    _eRxStatus = "Cannot post the message. Please Check Validations"
                                            '    'Update the Prescription table with the updated values of eRxStatus against the rescpective PrescriptionID and NDC Code
                                            '    UpdatePrescription(_eRxStatus, _Prescriptions.Item(icnt).PrescriptionID) '
                                            '    RaiseEvent UpdateRx_eRxStatus(_eRxStatus, objDrug.ProdCode)

                                            If ogloInterface.ValidationMessage.Length > 0 Then
                                                System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessage, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                                RxHistory_ERxPrescription = Nothing
                                                Exit Function
                                            End If
                                        End If
                                        ierxcnt = ierxcnt + 1
                                    Else
                                        ogloInterface.ValidationMessageBuilderforDrug.Append(ValidateDrugBuilder.ToString & vbCrLf)
                                    End If
                                    'Drug NDC code is not available or drug form is not available
                                Else
                                    ogloInterface.ValidationMessageBuilderforDrug.Append(ValidateDrugBuilder.ToString & vbCrLf)
                                End If
                                ValidateDrugBuilder.Remove(0, ValidateDrugBuilder.Length)
                            End If

                        End If
                    Next
                    If Not IsNothing(ogloInterface) Then
                        If ogloInterface.ValidationMessageBuilderforDrug.Length > 0 Then
                            System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderforDrug.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        End If
                    End If
                End If
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Catch ex As PrescriptionException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Catch ex As Exception
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                Dim Objex As New PrescriptionException
                Objex.ErrMessage = "Error Posting Prescription"
                Throw Objex
            Finally
                'SLR: Finally free all memories
                If Not IsNothing(oSurescriptDBLayer) Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
                'SLR: FRee other memories such as validatedrugbuilder, oglointerface,  etc,,,
                If Not ValidateDrugBuilder Is Nothing Then
                    ValidateDrugBuilder = Nothing
                End If
                If Not objDrug Is Nothing Then
                    objDrug.Dispose()
                    objDrug = Nothing
                End If
                If Not ogloInterface Is Nothing Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
            RxHistory_ERxPrescription = Nothing
        End Function

        Public Function IsPDREnabledForProvider(ByVal nProviderID As Int64) As Boolean

            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dtPDRRole As DataTable = Nothing
            Dim bReturned As Boolean = False
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dtPDRRole = oDB.GetDataTable("gsp_IsPDREnabledForProvider")

                If dtPDRRole IsNot Nothing AndAlso dtPDRRole.Rows.Count > 0 Then
                    If Convert.ToString(dtPDRRole.Rows(0)("sValue")) = "1" Then
                        bReturned = True
                    End If
                End If

                Return bReturned
            Catch ex As SqlException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(dtPDRRole) Then
                    dtPDRRole.Dispose()
                    dtPDRRole = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function
        Public Function IsPDMPEnabledForProvider(ByVal nProviderID As Int64) As Boolean

            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dtPDRRole As DataTable = Nothing
            Dim bReturned As Boolean = False
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dtPDRRole = oDB.GetDataTable("gsp_IsPDMPEnabledForProvider")

                If dtPDRRole IsNot Nothing AndAlso dtPDRRole.Rows.Count > 0 Then
                    If Convert.ToString(dtPDRRole.Rows(0)("sValue")) = "1" Then
                        bReturned = True
                    End If
                End If

                Return bReturned
            Catch ex As SqlException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(dtPDRRole) Then
                    dtPDRRole.Dispose()
                    dtPDRRole = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function


        Public Sub DeletePrescriptionItem(ByVal PrescriptionID As Int64, ByVal visitid As Int64, ByVal _LocalPatientID As Long)
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Try
                oDB = New DataBaseLayer

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPrescriptionID"
                oParameter.Value = PrescriptionID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nVisitId"
                oParameter.Value = visitid
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nPatientId"
                oParameter.Value = _LocalPatientID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oDB.ExecuteNon_Query("gsp_DeleteMedPresc")

            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(oParameter) Then
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try

        End Sub

        Public Sub UpdatePrescription(ByVal eRxStatus As String, ByVal eRxStatusMessage As String, ByVal PrescriptionID As Long, Optional ByVal IssuMethod As String = "")
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim rowUpdated As Int64 = 0
            Dim strquery As String = ""
            Try
                If eRxStatusMessage <> "" Then
                    eRxStatusMessage = eRxStatusMessage.Replace("'", "")
                End If

                If eRxStatus = "Posted Successfully" Then
                    strquery = "update prescription set messageType='', erxstatus = '" & eRxStatus & "',eRxStatusMessage = '" & eRxStatusMessage & "'  where nprescriptionid = " & PrescriptionID & "   "
                Else
                    strquery = "update prescription set  messageType='', erxstatus = '" & eRxStatus & "',eRxStatusMessage = '" & eRxStatusMessage & "'  where nprescriptionid = " & PrescriptionID & "   "
                End If
                If IssuMethod <> "" Then
                    strquery = "update prescription set  messageType='', erxstatus = '" & eRxStatus & "',eRxStatusMessage = '" & eRxStatusMessage & "',sMethod = '" & IssuMethod & "'  where nprescriptionid = " & PrescriptionID & "   "
                End If
                _gloEMRDatabase.GetDataValue(strquery, False)



            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving Provider ID"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Sub

        Public Function SetEPCSStatusMessage(ByRef ogloPrescription As EPrescription, ByVal i As Integer, ByVal _eRxStatus As String) As String
            gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
            gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
            gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
            gloSurescriptGeneral.checkDownloadVersion()
            Dim ogloInterface As gloSureScriptInterface = Nothing
            Dim oSurescriptDBLayer As gloSureScriptDBLayer = Nothing
            Try
                ogloInterface = New gloSureScriptInterface
                oSurescriptDBLayer = New gloSureScriptDBLayer



                If ogloPrescription.DrugsCol.Item(i).eRxFilePath <> "" Then
                    'Dim _eRxStatus As String = ""
                    'Dim _eRxStatusMessage As String = ""
                    'If ogloInterface.StatusMessageType.Length > 0 Then
                    '    ' System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

                    '    _eRxStatus = ogloInterface.MessageName
                    '    _eRxStatusMessage = "" 'ogloInterface.StatusMessageType
                    '    If _eRxStatus <> "" Then


                    '        Select Case _eRxStatus
                    '            Case "Status"
                    '                Select Case ogloInterface.MessagestatusCode
                    '                    Case "000"
                    '                        _eRxStatus = "Transmission successful"
                    '                    Case "010"
                    '                        _eRxStatus = "Successfully accepted by ultimate receiver"
                    '                    Case Else
                    '                        _eRxStatus = "Posted Successfully"

                    '                End Select

                    '            Case "Verify" 'for case verify we have same value in message variable as of status case
                    '                _eRxStatus = "Posted Successfully"
                    '            Case "Error"
                    '                _eRxStatus = "Could not be Posted Successfully"
                    '                _eRxStatusMessage = ogloInterface.StatusMessageType
                    '        End Select
                    '    End If
                    'End If

                    ogloPrescription.DrugsCol.Item(i).TransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID
                    oSurescriptDBLayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(i), SureScriptMessage))


                    If ogloPrescription.DrugsCol.Item(i).MessageType <> gloSureScriptInterface.SentMessageType.eNewRx Then
                        ogloPrescription.RxTransactionID = ogloPrescription.DrugsCol.Item(i).PrescriptionID

                        If Me.IsChangeRequest AndAlso ogloPrescription.DrugsCol.Item(i).EPCSPrescriptionStatusLabel.ToUpper() = "SUCCESS" Then
                            Using p As New PrescriptionBusinessLayer()
                                p.UpdateRxMessageStatus(Me.RxChangeRequest.MessageID, ogloPrescription.DrugsCol.Item(i).MessageType, "RxChange")
                            End Using
                        ElseIf ogloPrescription.DrugsCol.Item(i).EPCSPrescriptionStatusLabel.ToUpper() = "SUCCESS" Then
                            ogloInterface.UpdateRefillStatus(ogloPrescription, ogloPrescription.DrugsCol.Item(i).MessageType, i)
                        End If

                        For j As Integer = 0 To PrescriptionCol.Count - 1
                            If ogloPrescription.DrugsCol.Item(i).ItemNumber = PrescriptionCol.Item(j).ItemNumber Then
                                PrescriptionCol.Item(j).MessageType = ""
                            End If
                        Next
                    End If
                    UpdatePrescription(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).PrescriptionID) '
                    RaiseEvent UpdateRx_eRxStatus(_eRxStatus, _eRxStatusMessage, ogloPrescription.DrugsCol.Item(i).ProdCode, ogloPrescription.DrugsCol.Item(i).ItemNumber, ogloPrescription.DrugsCol.Item(i).IseRxed) 'bug 13911 in 6030

                End If

                Return ogloInterface.StatusMessageType
            Catch ex As Exception
                Throw ex
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
                If Not IsNothing(oSurescriptDBLayer) Then
                    oSurescriptDBLayer.Dispose()
                    oSurescriptDBLayer = Nothing
                End If
            End Try
        End Function

        Private Function SplitDrug(ByVal dosage As String) As Array
            Try
                Dim _result As String()
                _result = dosage.Split(" ")
                Return _result
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Function GetLatestEPCSStatuslabel(ByVal PrescriptionID As Int64, ByVal _IsNarcotics As Short) As String
            Dim _gloEMRDatabase As DataBaseLayer
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim strResult As String = ""
            Try
                objParameter = New DBParameter
                objParameter.Value = PrescriptionID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@PrescriptionId"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                strResult = Convert.ToString(_gloEMRDatabase.GetDataValue("gsp_GetEPCSStatus"))
            Catch ex As gloDBException
                strResult = Nothing
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ex.ToString()
                strResult = Nothing
                Throw objex
                Return False
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
            Return strResult
        End Function

        Public Function getDosageFreqValue(ByVal DosageFreqText As String) As DataTable
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim dtDosageFreq As DataTable = Nothing 'SLR: new is not needed
            Try
                Dim strquery As String = "select isnull(Abbrevation,'') as Abbrevation, isnull(Meaning,'') as Meaning, isnull([Value],'1') as AbbrValue from Frequency_Abbreviation where  Meaning = '" & DosageFreqText & "'"

                dtDosageFreq = _gloEMRDatabase.GetDataTable_Query(strquery)

                Return dtDosageFreq

            Catch ex As Exception
                Return dtDosageFreq
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Private Function GetProviderIDByPatientid(ByVal nPatientId As Long) As Int64
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "select isnull(nproviderid,0) from Patient where npatientid = " & nPatientId & " "
                Dim ProviderId As Int64 = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsDBNull(ProviderId) Then
                    Return 0
                Else
                    Return ProviderId
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving Provider ID"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function GetProviderID(ByVal nPatientId As Long, Optional ByRef nPatientProviderId As Long = 0) As Long
            Try
                nPatientProviderId = GetProviderIDByPatientid(nPatientId)
                If String.IsNullOrEmpty(ProviderID) = True Or Convert.ToString(ProviderID) = 0 Then
                    _oldProviderID = nPatientProviderId ''_RxDBLayer.GetProviderID(nPatientId)
                Else
                    _oldProviderID = ProviderID
                End If

                Return _oldProviderID
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = ""
                Throw objex
            Finally

            End Try
        End Function

        Public Function AddProvidersDrugs(ByVal providerId As Long, ByVal DrugId As Long, ByVal _drugname As String, ByVal _dosage As String, ByVal _route As String, ByVal _frequency As String, ByVal _duration As String, ByVal _drugform As String, ByVal _ndccode As String, ByVal _isnarcotics As Int64, ByVal _sDrugQtyQualifier As String, ByVal _sRefill As String, ByVal _nSIGid As Long, ByVal _sDispAmt As String, ByVal mpid As Int32) As Boolean
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter

            Try
                objParameter = New DBParameter
                objParameter.Value = DrugId
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@DrugId"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = providerId
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@ProviderId"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _drugname
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@DrugName"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _dosage
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Dosage"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _route
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Route"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _frequency
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Frequency"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _duration
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Duration"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _drugform
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@DrugForm"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _ndccode
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@NDCCode"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _isnarcotics
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@IsNarcotics"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing


                objParameter = New DBParameter
                objParameter.Value = _sDrugQtyQualifier
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sDrugQtyQualifier"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _sRefill
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sRefills"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _nSIGid
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nSIGID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _sDispAmt
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sAmount"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = mpid
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@mpid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                Dim Retval As Boolean = True
                Retval = _gloEMRDatabase.Add("gsp_InsertProvidersDrugs")

                Return True
            Catch ex As gloDBException
                Return False
            Catch ex As gloEMRActorsException
                Return False
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return False
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function


        Public Function GetPatientProviderSPIID(ByVal nPatientId As Long) As String
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim sProviderSPIid As String = ""
            Try
                Dim strquery As String = "SELECT     isnull(Provider_MST.sSPIID,'') as sSPIID  FROM Patient INNER JOIN " _
                & " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID where patient.npatientid = " & nPatientId & " "

                sProviderSPIid = _gloEMRDatabase.GetDataValue(strquery, False)

                Return sProviderSPIid

            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving Provider ID"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Sub GetPharmacyID(ByVal nPatientId As Long)

            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "select isnull(nContactId,0) as PharmacyId from Patient_DTL where nContactFlag =1 and npatientid = " & nPatientId & " AND ISNULL(nContactStatus,0) = 1"
                Dim PharmacyId As Int64 = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsDBNull(PharmacyId) Then
                    _oldPharmacyID = 0
                Else
                    _oldPharmacyID = PharmacyId
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving pharmacy ID"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Sub

        Public Function GetDefaultPharmacyId(ByVal nPatientId As Long) As Long
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "select isnull(nContactId,0) as PharmacyId from Patient_DTL where nContactFlag =1 and npatientid = " & nPatientId & " AND ISNULL(nContactStatus,0) = 1"
                Dim PharmacyId As Int64 = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsDBNull(PharmacyId) Then
                    Return 0
                Else
                    Return PharmacyId
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving pharmacy ID"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Friend Function SavePrescription(ByRef visitid As Int64, ByVal _VisitDate As DateTime, ByVal enm As RxBusinesslayer._TransactionMode, ByRef _Prescriptions As Prescriptions)
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim Prescriptiondate As DateTime
            Dim _Prescription As Prescription
            'this object is created becaz we nee to add/save the items that r inserted from the Prescription form into the Medication form
            'therefore we need to create the business layer obj of Medication & using it call the SaveMedication() of the Medication database layer                
            'The _MedRxCol represents the collection that will contain all the items that r added from the Prescription form and will be passed to the database layer of Medication module.                                
            Dim i As Int16
            'the flag represents that whether the items are inserted from the Prescrition layer i.e. if flag=true then items are inserted from the Prescription Module                

            _Prescription = _Prescriptions.Item(0)

            Dim _ArryLst_formulayRxId As New ArrayList
            Try

                If enm = _TransactionMode.Edit Then
                    ' pass the third parameter as true so that first it will delete any mx item that are entered form the Rx module then it will delete the Rx items from respective tables.
                    'DeletePrescription(_Prescription.VisitID, _Prescription.Prescriptiondate, True)
                    Prescriptiondate = _Prescription.Prescriptiondate
                    'm_dtdate = Prescriptiondate
                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomPrescEdited = True Then ''''for CCHIT11 audit log  gblnCustomPrescEdited 
                        ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ModifyPrescription, gloAuditTrail.ActivityType.Modify, "Prescription modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                ElseIf enm = _TransactionMode.Add Then
                    If visitid = 0 Then
                        Dim _Visit As New Visit
                        Dim _VisitBusinessLayer As New VisitBusinessLayer

                        _Visit.PatientID = _PatientID ''_Prescriptions.Item(0).PatientID  ''globalPatient.gnPatientID
                        _Visit.VisitID = 0
                        _Visit.VisitDate = _VisitDate

                        _VisitBusinessLayer.VisitObject = _Visit
                        If _VisitBusinessLayer.InsertVisit Then
                            visitid = _Visit.VisitID
                        End If
                        'Else
                        '    tempvisitid = visitid
                        'SLR: Free _visitBusinessLayer and _Visit
                        If Not _VisitBusinessLayer Is Nothing Then
                            _VisitBusinessLayer.Dispose()
                            _VisitBusinessLayer = Nothing
                        End If
                        If Not _Visit Is Nothing Then
                            _Visit.Dispose()
                            _Visit = Nothing
                        End If
                    End If
                    Prescriptiondate = Now
                    'm_dtdate = Prescriptiondate

                End If

                Dim dtPharmacyDetails As DataTable = Nothing
                Dim tempPharmacyid As Long = 0
                ''Fill the collection in dataset table
                For i = 0 To _Prescriptions.Count - 1
                    _Prescription = _Prescriptions.Item(i)

                    'Resolve Incident #88659: 00053537: eRx CommunicationNumber missing 
                    'Before Save take all the pharmacy details from Contacts_Mst and then Save the Record
                    'Directly we have taken from Contact_Mst without checking whether phone no. exists or not because it may happen that pharmacy phone no. is changed

                    If _Prescription.PharmacyID <> 0 Then
                        If _Prescription.PharmacyID <> tempPharmacyid Then
                            tempPharmacyid = _Prescription.PharmacyID
                            If Not IsNothing(dtPharmacyDetails) Then
                                dtPharmacyDetails = Nothing
                            End If
                            dtPharmacyDetails = GetPharmacyDetails(tempPharmacyid)
                        End If


                        If Not IsNothing(dtPharmacyDetails) Then
                            If dtPharmacyDetails.Rows.Count > 0 Then
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sNCPDPID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sNCPDPID")) Then
                                    _Prescription.PhNCPDPID = dtPharmacyDetails.Rows(0)("sNCPDPID")
                                Else
                                    _Prescription.PhNCPDPID = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("nContactID")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("nContactID")) Then
                                    _Prescription.PhContactID = dtPharmacyDetails.Rows(0)("nContactID")
                                Else
                                    _Prescription.PhContactID = 0
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sName")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sName")) Then
                                    _Prescription.PharmacyName = dtPharmacyDetails.Rows(0)("sName")
                                Else
                                    _Prescription.PharmacyName = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine1")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine1")) Then
                                    _Prescription.PhAddressline1 = dtPharmacyDetails.Rows(0)("sAddressLine1")
                                Else
                                    _Prescription.PhAddressline1 = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sAddressLine2")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sAddressLine2")) Then
                                    _Prescription.PhAddressline2 = dtPharmacyDetails.Rows(0)("sAddressLine2")
                                Else
                                    _Prescription.PhAddressline2 = ""
                                End If

                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sCity")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sCity")) Then
                                    _Prescription.PhCity = dtPharmacyDetails.Rows(0)("sCity")
                                Else
                                    _Prescription.PhCity = ""
                                End If

                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sState")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sState")) Then
                                    _Prescription.PhState = dtPharmacyDetails.Rows(0)("sState")
                                Else
                                    _Prescription.PhState = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sZIP")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sZIP")) Then
                                    _Prescription.PhZip = dtPharmacyDetails.Rows(0)("sZIP")
                                Else
                                    _Prescription.PhZip = ""
                                End If

                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sEmail")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sEmail")) Then
                                    _Prescription.PhEmail = dtPharmacyDetails.Rows(0)("sEmail")
                                Else
                                    _Prescription.PhEmail = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sFax")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sFax")) Then
                                    _Prescription.PhFax = dtPharmacyDetails.Rows(0)("sFax")
                                Else
                                    _Prescription.PhFax = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sPhone")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sPhone")) Then
                                    _Prescription.PhPhone = dtPharmacyDetails.Rows(0)("sPhone")
                                Else
                                    _Prescription.PhPhone = ""
                                End If
                                If Not IsNothing(dtPharmacyDetails.Rows(0)("sServiceLevel")) AndAlso Not IsDBNull(dtPharmacyDetails.Rows(0)("sServiceLevel")) Then
                                    _Prescription.PhServiceLevel = dtPharmacyDetails.Rows(0)("sServiceLevel")
                                Else
                                    _Prescription.PhServiceLevel = ""
                                End If
                            End If
                        End If
                    End If

                    If _Prescription.State <> "U" Then
                        Dim newRxRow As DataRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").NewRow()
                        If _Prescription.PrescriptionID <> 0 Then
                            newRxRow.Item("nPrescriptionID") = _Prescription.PrescriptionID
                        Else
                            newRxRow.Item("nPrescriptionID") = 0
                        End If

                        If enm = _TransactionMode.Edit Then
                            newRxRow.Item("nVisitID") = If(_Prescription.VisitID = 0, visitid, _Prescription.VisitID)
                        ElseIf enm = _TransactionMode.Add Then
                            newRxRow.Item("nVisitID") = visitid
                        End If

                        newRxRow.Item("nPatientID") = _PatientID
                        newRxRow.Item("sMedication") = _Prescription.Medication
                        newRxRow.Item("sDosage") = _Prescription.Dosage
                        newRxRow.Item("sRoute") = _Prescription.Route
                        newRxRow.Item("sFrequency") = _Prescription.Frequency
                        newRxRow.Item("sRefills") = _Prescription.Refills
                        newRxRow.Item("sDuration") = _Prescription.Duration

                        If Not IsNothing(_Prescription.Startdate) Then
                            If _Prescription.Startdate = "12:00:00 AM" Then
                                newRxRow.Item("dtStartDate") = Now
                            Else
                                newRxRow.Item("dtStartDate") = _Prescription.Startdate
                            End If
                        Else
                            newRxRow.Item("dtStartDate") = Now
                        End If

                        If _Prescription.CheckEndDate = True Then
                            If Not IsNothing(_Prescription.Enddate) Then
                                If _Prescription.Enddate = "12:00:00 AM" Then
                                    newRxRow.Item("dtEndDate") = DBNull.Value
                                Else
                                    newRxRow.Item("dtEndDate") = _Prescription.Enddate

                                End If
                            Else
                                newRxRow.Item("dtEndDate") = Now
                            End If
                        End If

                        'newRxRow.Item("sNotes") = _Prescription.Notes.Replace("'", "''''")

                        '_Prescription.Notes = _Prescription.Notes
                        'If Not _Prescription.Notes.ToLower().Contains("prior authorization status") AndAlso Not String.IsNullOrWhiteSpace(_Prescription.PriorAuthorizationStatus) Then
                        '    _Prescription.Notes = _Prescription.Notes + " Prior Authorization Status: " + _Prescription.PriorAuthorizationStatus
                        'End If

                        'If Not _Prescription.Notes.ToLower.Contains("prior authorization number") AndAlso Not String.IsNullOrWhiteSpace(_Prescription.PriorAuthorizationNumber) Then
                        '    _Prescription.Notes = _Prescription.Notes + " Prior Authorization Number: " + _Prescription.PriorAuthorizationNumber
                        'End If

                        newRxRow.Item("sNotes") = _Prescription.Notes
                        newRxRow.Item("sMethod") = _Prescription.Method
                        If _Prescription.Maysubstitute Then
                            newRxRow.Item("bMaySubstitute") = 0
                        Else
                            newRxRow.Item("bMaySubstitute") = 1
                        End If


                        If enm = _TransactionMode.Edit Then
                            If _Prescription.Status = "DeniedWNewRx" Or _Prescription.Status = "Denied" Then
                                _Prescription.Prescriptiondate = Prescriptiondate
                                newRxRow.Item("dtPrescriptionDate") = _Prescription.Prescriptiondate
                            ElseIf _Prescription.Status = "Approved" Then
                                'If _RxTransactionID <> 0 Then
                                '    UpdateMedicationforApprovedRefillRequest(_RxTransactionID)
                                'End If
                                'If PONNumber <> 0 Then
                                '    newRxRow.Item("nPrescriptionID") = PONNumber
                                'End If
                                newRxRow.Item("dtPrescriptionDate") = Prescriptiondate
                                _Prescription.Prescriptiondate = Prescriptiondate
                            Else
                                'newRxRow.Item("dtPrescriptionDate") = _Prescription.Prescriptiondate.ToLongDateString()
                                newRxRow.Item("dtPrescriptionDate") = Prescriptiondate
                                _Prescription.Prescriptiondate = Prescriptiondate
                            End If
                        Else
                            If _Prescription.Status = "DeniedWNewRx" Or _Prescription.Status = "Denied" Then
                                _Prescription.Prescriptiondate = Now
                                newRxRow.Item("dtPrescriptionDate") = _Prescription.Prescriptiondate
                            ElseIf _Prescription.Status = "Approved" Then
                                'If _RxTransactionID <> 0 Then
                                '    UpdateMedicationforApprovedRefillRequest(_RxTransactionID)
                                'End If
                                'If PONNumber <> 0 Then
                                '    newRxRow.Item("nPrescriptionID") = PONNumber
                                'End If
                                newRxRow.Item("dtPrescriptionDate") = Prescriptiondate
                                _Prescription.Prescriptiondate = Prescriptiondate
                            Else
                                newRxRow.Item("dtPrescriptionDate") = Prescriptiondate
                                _Prescription.Prescriptiondate = Prescriptiondate
                            End If
                        End If
                        newRxRow.Item("sAmount") = _Prescription.Amount

                        Dim nDrugID As Int64 = 0
                        If _Prescription.DrugID = 0 Then
                            If _Prescription.NDCCode <> "" Then
                                nDrugID = GetDrugIDByNDC(_Prescription.NDCCode)
                            Else
                                If _Prescription.mpid <> 0 Then
                                    nDrugID = GetDrugIdBympid(_Prescription.mpid)
                                Else
                                    Dim sDrugName As String = _Prescription.Medication
                                    Dim sDosage As String = _Prescription.Dosage
                                    Dim sRoute As String = _Prescription.Route
                                    Dim sFrequency As String = _Prescription.Frequency
                                    Dim sDuration As String = _Prescription.Duration
                                    nDrugID = GetDrugIDFromSig(sDrugName, sDosage, sRoute, sFrequency, sDuration)
                                End If
                            End If
                        End If

                        If IsNothing(_Prescription.DrugID) Then
                            newRxRow.Item("nDrugID") = nDrugID
                        ElseIf _Prescription.DrugID = 0 Then
                            newRxRow.Item("nDrugID") = nDrugID
                        Else
                            newRxRow.Item("nDrugID") = _Prescription.DrugID
                        End If

                        newRxRow.Item("sLotNo") = _Prescription.LotNo

                        If _Prescription.CheckExpiryDate = True Then
                            If Not IsNothing(_Prescription.ExpirationDate) Then
                                If _Prescription.ExpirationDate = "12:00:00 AM" Then
                                    newRxRow.Item("dtExpirationdate") = DBNull.Value
                                Else
                                    newRxRow.Item("dtExpirationdate") = _Prescription.ExpirationDate
                                End If
                            Else
                                newRxRow.Item("dtExpirationdate") = Now
                            End If
                        End If

                        newRxRow.Item("sUserName") = _Prescription.UserName
                        newRxRow.Item("nProviderId") = _Prescription.ProviderID
                        newRxRow.Item("sChiefComplaints") = _Prescription.ChiefComplaint
                        newRxRow.Item("ProblemID") = _Prescription.Problems
                        newRxRow.Item("sDrugForm") = _Prescription.DosageForm
                        'newRxRow.Item("sRenewed") = ""
                        newRxRow.Item("sReasonToOverrideDI") = _Prescription.ReasontoOverride
                        If IsNothing(_Prescription.PharmacyID) Then
                            newRxRow.Item("nPharmacyId") = 0
                        Else
                            newRxRow.Item("nPharmacyId") = _Prescription.PharmacyID
                        End If

                        If IsNothing(_Prescription.RefillQualifier) Then
                            newRxRow.Item("sRefillQualifier") = "R"
                        Else
                            newRxRow.Item("sRefillQualifier") = _Prescription.RefillQualifier
                        End If

                        newRxRow.Item("sNDCCode") = _Prescription.NDCCode
                        If IsNothing(_Prescription.IsNarcotics) Then
                            newRxRow.Item("nIsNarcotic") = 0
                        Else
                            newRxRow.Item("nIsNarcotic") = _Prescription.IsNarcotics
                        End If

                        newRxRow.Item("mpid") = _Prescription.mpid
                        newRxRow.Item("sDrugForm") = _Prescription.DosageForm
                        newRxRow.Item("sStrengthUnit") = _Prescription.StrengthUnit
                        newRxRow.Item("sNCPDPID") = _Prescription.PhNCPDPID
                        newRxRow.Item("nContactID") = _Prescription.PhContactID
                        newRxRow.Item("sName") = _Prescription.PharmacyName
                        newRxRow.Item("sAddressline1") = _Prescription.PhAddressline1
                        newRxRow.Item("sAddressline2") = _Prescription.PhAddressline2
                        newRxRow.Item("sCity") = _Prescription.PhCity
                        newRxRow.Item("sState") = _Prescription.PhState
                        newRxRow.Item("sZip") = _Prescription.PhZip
                        newRxRow.Item("sEmail") = _Prescription.PhEmail
                        newRxRow.Item("sFax") = _Prescription.PhFax
                        newRxRow.Item("sPhone") = _Prescription.PhPhone
                        newRxRow.Item("sServiceLevel") = _Prescription.PhServiceLevel
                        newRxRow.Item("sPrescriberNotes") = _Prescription.PrescriberNotes
                        newRxRow.Item("eRxStatus") = _Prescription.eRxStatus
                        newRxRow.Item("blnFlag") = _Prescription.Flag
                        newRxRow.Item("eRxStatusMessage") = _Prescription.eRxStatusMessage
                        If _Prescription.Status = "D" Then
                            _Prescription.State = "D"
                        End If
                        newRxRow.Item("sRenewed") = _Prescription.Renewed
                        If _Prescription.CPOEOrder = True Then
                            newRxRow.Item("IsCPOEOrder") = 1
                        Else
                            newRxRow.Item("IsCPOEOrder") = 0
                        End If

                        newRxRow.Item("RowState") = _Prescription.State
                        newRxRow.Item("ItemNumber") = _Prescription.ItemNumber
                        newRxRow.Item("MessageType") = _Prescription.MessageType
                        newRxRow.Item("MessageID") = _Prescription.MessageID
                        newRxRow.Item("bIsFormularyQueried") = _Prescription.IsFormularyQueried
                        If _Prescription.MedicationAdministered = True Then
                            newRxRow.Item("IsMedicationAdministered") = 1
                        Else
                            newRxRow.Item("IsMedicationAdministered") = 0
                        End If
                        newRxRow.Item("PAReferenceID") = _Prescription.PAReferenceID
                        newRxRow.Item("nPCTransactionID") = _Prescription.PCTransactionID

                        gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Rows.Add(newRxRow)
                        'SLR: Free newRxRow
                        If Not newRxRow Is Nothing Then
                            newRxRow = Nothing
                        End If

                        Dim newPCRow As DataRow = Nothing

                        If TypeOf (_Prescription.PDRPrograms) Is ProgramResponse Then
                            Dim programResponse As ProgramResponse = DirectCast(_Prescription.PDRPrograms, ProgramResponse)

                            For Each Program As Program In programResponse.Programs
                                newPCRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PatientCommunication").NewRow()
                                newPCRow.Item("nRxNumber") = programResponse.RxNumber
                                newPCRow.Item("sPBMMemberID") = DBNull.Value
                                newPCRow.Item("sNDCCode") = _Prescription.NDCCode
                                newPCRow.Item("nMPID") = _Prescription.mpid
                                newPCRow.Item("sRefills") = _Prescription.Refills
                                newPCRow.Item("sQuantity") = _Prescription.Amount.Split(" ")(0)
                                newPCRow.Item("sDaysSupplied") = _Prescription.DaysSupply
                                newPCRow.Item("sSig") = _Prescription.Frequency
                                newPCRow.Item("sDose") = _Prescription.Dosage

                                Select Case _Prescription.Method.ToUpper()
                                    Case "FAX"
                                        newPCRow.Item("sPrescriptionForm") = "F"
                                    Case "PRINT", "OTC", "SAMPLE", "HANDWRITTEN"
                                        newPCRow.Item("sPrescriptionForm") = "H"
                                    Case "PHONE"
                                        newPCRow.Item("sPrescriptionForm") = "P"
                                    Case "ERX"
                                        newPCRow.Item("sPrescriptionForm") = "P"
                                End Select

                                newPCRow.Item("nTransactionID") = _Prescription.PCTransactionID
                                newPCRow.Item("nProgramID") = Program.id
                                newPCRow.Item("sProgramName") = Program.name
                                newPCRow.Item("nPaid") = Program.paid
                                newPCRow.Item("sPaymentNotes") = Program.paymentNotes
                                newPCRow.Item("sContent") = Program.image

                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PatientCommunication").Rows.Add(newPCRow)
                                newPCRow = Nothing
                            Next

                        End If

                    End If

                Next

            Catch ex As Exception
                _ArryLst_formulayRxId.Clear()
                _ArryLst_formulayRxId = Nothing
                Throw ex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
                _ArryLst_formulayRxId.Clear()
            End Try
            Return Nothing
        End Function

        Public Function SavePrescription(ByVal nLoginProviderId As Long) As Boolean

            Try
                gloSurescriptGeneral.UpdateLog("Entering clsGloEMRPrescription.SavePrescription")

                If _Prescriptions.Count > 0 Then
                    gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
                    gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
                    PONNumber = gloSurescriptGeneral.GetUniqueID()

                    If SavePrescription(_intCurrentVisitID, _VisitDate, eTransactionMode, _Prescriptions) = Nothing Then

                        TransactionMode = _TransactionMode.Edit
                        PrescriptionDate = _Prescriptions.Item(0).Prescriptiondate

                        ''Assign login provider
                        If nLoginProviderId <> 0 Then
                            _ProviderId = nLoginProviderId
                        Else
                            _ProviderId = _Prescriptions.Item(0).ProviderID
                        End If

                        ''as per discussion bug #7489 in glo6010 instead of showing the the drugs pharmacy id as patient id set the patients default pharmacy id
                        Dim DefaultPharmID As Long = GetDefaultPharmacyId(_PatientID)
                        If DefaultPharmID <> 0 Then
                            _PharmacyId = DefaultPharmID
                        Else
                            _PharmacyId = _Prescriptions.Item(0).PharmacyID
                        End If

                        RaiseEvent PrescriptionSaved(True)

                        Return True
                    End If

                Else
                    If TransactionMode = _TransactionMode.Edit Then
                        If System.Windows.Forms.MessageBox.Show("Do you wish to Delete this transaction?", "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                            'If _RxDBLayer.DeletePrescription(_intCurrentVisitID, TempDate) Then
                            TransactionMode = _TransactionMode.Add
                        End If
                    End If
                End If
                Return Nothing
            Catch ex As Exception
                Dim Objex As New PrescriptionException
                Objex.ErrMessage = "Error Saving Prescription : " & ex.Message
                SavePrescription = Nothing
                Throw Objex
            Finally

            End Try
        End Function

        Public Sub LoadSchema()
            '_ds = New DataSet
            Dim da As New SqlDataAdapter
            Dim con As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Dim cmd As SqlCommand = Nothing
            Try
                cmd = New SqlCommand("gsp_LoadSchema", con)
                cmd.CommandType = CommandType.StoredProcedure
                con.Open()
                da.SelectCommand = cmd
                If Not IsNothing(gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds) Then
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Dispose()
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds = Nothing
                End If
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds = New DataSet
                da.Fill(gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds)
                con.Close()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Table").TableName = "Prescription"
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Table1").TableName = "Medication"
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Table2").TableName = "PrescriptionProvider"
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Columns.Add("sRenewed")
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Columns.Add("RowState")
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Columns.Add("RowState")

                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Table3").TableName = "MedicationReconcillation"
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Columns.Add("RowState")
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation").Columns.Add("nReconcillationType")
                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Table4").TableName = "PatientCommunication"

            Catch ex As Exception
                Throw ex
            Finally
                If IsNothing(da) = False Then
                    da.Dispose()
                    da = Nothing
                End If
                If IsNothing(con) = False Then
                    con.Dispose()
                    con = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            End Try
        End Sub

        'Public Sub SavePrescriptionsIssued(ByVal sPrescriptionIds As String, ByVal bIseRx As Boolean, ByVal UserSessionId As Long)
        '    Dim Con As SqlConnection = Nothing
        '    Dim cmd As SqlCommand = Nothing
        '    Dim _param As SqlParameter = Nothing
        '    Try
        '        'Save PrescriptionIDs in PrescriptionIssued Table
        '        Con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)

        '        Con.Open()

        '        cmd = New SqlCommand("gsp_InPrescriptionIssued", Con)
        '        cmd.CommandType = CommandType.StoredProcedure

        '        _param = cmd.Parameters.AddWithValue("@PrescriptionIds", sPrescriptionIds)
        '        _param.SqlDbType = SqlDbType.VarChar
        '        _param.Size = 1000
        '        _param = Nothing

        '        _param = cmd.Parameters.AddWithValue("@IseRx", bIseRx)
        '        _param.SqlDbType = SqlDbType.Bit
        '        _param = Nothing

        '        _param = cmd.Parameters.AddWithValue("@UserSessionId", UserSessionId)
        '        _param.SqlDbType = SqlDbType.BigInt
        '        _param = Nothing

        '        cmd.CommandTimeout = 0
        '        cmd.ExecuteNonQuery()

        '        Con.Close()

        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '        ex = Nothing
        '    Finally
        '        If IsNothing(Con) = False Then
        '            Con.Dispose()
        '            Con = Nothing
        '        End If
        '        If IsNothing(cmd) = False Then
        '            cmd.Parameters.Clear()
        '            cmd.Dispose()
        '            cmd = Nothing
        '        End If
        '        If IsNothing(_param) = False Then
        '            _param = Nothing
        '        End If
        '    End Try

        'End Sub

        Public Sub SaveRx_TVP(Optional ByVal IsAddUpdateinSubsequentVisits As Boolean = False, Optional ByVal _IsPastVisit As Boolean = False, Optional ByVal _PatientId As Long = 0)
            Dim Con As SqlConnection = Nothing
            'Dim da As SqlDataAdapter
            Dim cmd As SqlCommand = Nothing
            Dim _param As SqlParameter = Nothing
            Dim sPrescriptionIDs As String = String.Empty
            Dim sMedicationIDs As String = String.Empty
            Dim _ArraysPrescriptionIDs As String()
            Dim _ArrayssMedicationIDs As String()
            Try

                _ArraysPrescriptionIDs = Nothing
                _ArrayssMedicationIDs = Nothing

                Con = New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
                'cmd = New SqlCommand("gsp_InupDeleteMedicationPrescription", Con)
                cmd = New SqlCommand("gsp_InUpDel_RxMeds", Con)
                cmd.CommandType = CommandType.StoredProcedure
                _param = cmd.Parameters.AddWithValue("@Rx_SavePresciption", gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription"))
                _param.SqlDbType = SqlDbType.Structured
                _param = Nothing
                _param = cmd.Parameters.AddWithValue("@Rx_SaveMedication", gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication"))
                _param.SqlDbType = SqlDbType.Structured
                _param = Nothing
                _param = cmd.Parameters.AddWithValue("@Rx_SupervisingProvider", gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PrescriptionProvider"))
                _param.SqlDbType = SqlDbType.Structured
                _param = cmd.Parameters.AddWithValue("@Rx_MedicationReconcillation", gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("MedicationReconcillation"))
                _param.SqlDbType = SqlDbType.Structured
                _param = cmd.Parameters.AddWithValue("@Rx_PatientCommunication", gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("PatientCommunication"))
                _param.SqlDbType = SqlDbType.Structured
                'Incident #55315: 00016572 : Carry forward issue
                'IsPastVisit and IsAddUpdateinSubsequentVisits added.
                _param = cmd.Parameters.AddWithValue("@IsPastVisit", _IsPastVisit)
                _param.SqlDbType = SqlDbType.Bit
                _param.Direction = ParameterDirection.Input
                _param = cmd.Parameters.AddWithValue("@IsAddUpdateinSubsequentVisits", IsAddUpdateinSubsequentVisits)
                _param.SqlDbType = SqlDbType.Bit
                _param.Direction = ParameterDirection.Input
                _param = cmd.Parameters.AddWithValue("@sMedicationIDs", "")
                _param.SqlDbType = SqlDbType.VarChar
                _param.Size = 1000
                _param.Direction = ParameterDirection.InputOutput


                _param = cmd.Parameters.AddWithValue("@sPrescriptionIDs", "")
                _param.SqlDbType = SqlDbType.VarChar
                _param.Size = 1000
                _param.Direction = ParameterDirection.InputOutput

                _param = cmd.Parameters.AddWithValue("@nProviderid", gloGlobal.gloPMGlobal.LoginProviderID)
                _param.SqlDbType = SqlDbType.BigInt

                _param.Direction = ParameterDirection.Input

                _param = cmd.Parameters.AddWithValue("@nPatientID", _PatientId)
                _param.SqlDbType = SqlDbType.BigInt

                _param.Direction = ParameterDirection.Input


                Con.Open()
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()

                sMedicationIDs = Convert.ToString(cmd.Parameters("@sMedicationIDs").Value)
                sPrescriptionIDs = Convert.ToString(cmd.Parameters("@sPrescriptionIDs").Value)

                Con.Close()

                If sMedicationIDs <> String.Empty Then
                    _ArrayssMedicationIDs = sMedicationIDs.Split(",")
                End If

                If sPrescriptionIDs <> String.Empty Then
                    _ArraysPrescriptionIDs = sPrescriptionIDs.Split(",")
                End If

                'Incident #55315: 00016572 : Carry forward issue
                'Log Audit trail for user action to carryforward medications
                If IsAddUpdateinSubsequentVisits Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, "Past Medication is updated & carry forwarded to subsequent visits", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf _IsPastVisit And Not IsAddUpdateinSubsequentVisits Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, "Past Medication is updated & but not carry forwarded to subsequent visits per user request", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If

                If IsNothing(_ArrayssMedicationIDs) = False Then
                    If _ArrayssMedicationIDs.Length > 0 Then
                        For icnt As Int16 = 0 To _ArrayssMedicationIDs.Length - 1
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, "Medication Added", _PatientID, _ArrayssMedicationIDs(icnt), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Next
                    End If
                End If

                If IsNothing(_ArraysPrescriptionIDs) = False Then
                    If _ArraysPrescriptionIDs.Length > 0 Then
                        For icnt As Int16 = 0 To _ArraysPrescriptionIDs.Length - 1
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Add, "Prescription Added", _PatientID, _ArraysPrescriptionIDs(icnt), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Next
                    End If
                End If


                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds IsNot Nothing Then

                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Rows.Count > 0 Then
                        Dim UpdateRow As DataRow() = Nothing
                        Dim DeleteRow As DataRow() = Nothing
                        UpdateRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Select("RowState='M'")
                        DeleteRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Prescription").Select("RowState='D'")
                        If UpdateRow.Length > 0 Then
                            For Each DataRow As DataRow In UpdateRow
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.ModifyPrescription, gloAuditTrail.ActivityType.Modify, "Prescription Saved", _PatientID, DataRow("nPrescriptionID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                        End If

                        If DeleteRow.Length > 0 Then
                            For Each DataRow As DataRow In DeleteRow
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DeletePrescription, gloAuditTrail.ActivityType.Delete, "Prescription Deleted", _PatientID, DataRow("nPrescriptionID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                        End If

                        UpdateRow = Nothing
                        DeleteRow = Nothing
                    End If

                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Count > 0 Then
                        Dim UpdateRow As DataRow() = Nothing
                        Dim DeleteRow As DataRow() = Nothing
                        UpdateRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Select("RowState='M'")
                        DeleteRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Select("RowState='D'")

                        If UpdateRow.Length > 0 Then
                            For Each DataRow As DataRow In UpdateRow
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Medication Save", _PatientID, DataRow("nMedicationID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                        End If

                        If DeleteRow.Length > 0 Then
                            For Each DataRow As DataRow In DeleteRow
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.DeleteMedication, gloAuditTrail.ActivityType.Delete, "Medication Deleted", _PatientID, DataRow("nMedicationID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                        End If

                        UpdateRow = Nothing
                        DeleteRow = Nothing

                    End If

                End If



                '_MedicationDBLayer.FetchMedicationforUpdate(_CurrentVisitID, m_FilterType, _CurrentVisitDate, _Medications, _OldMedications)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
                Throw ex
            Finally
                If IsNothing(Con) = False Then
                    Con.Dispose()
                    Con = Nothing
                End If
                If IsNothing(cmd) = False Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If IsNothing(_ds) = False Then
                    _ds.Dispose()
                    _ds = Nothing
                End If
                If IsNothing(_param) = False Then
                    _param = Nothing
                End If
            End Try
        End Sub

        Public Function FillSigControls(ByVal id As Long) As DataTable
            Dim _gloEMRDatabase = New DataBaseLayer
            Dim odt As DataTable
            Try

                If id = 0 Then
                    odt = _gloEMRDatabase.GetDataTable("gsp_FillSig_Mst")
                    Return odt
                Else
                    Return Nothing
                End If

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching Prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                _gloEMRDatabase.Dispose()
            End Try

        End Function

        Public Function FillSigControls_WithDrugProvAsso(ByVal Patientid As Long, ByVal DrugName As String) As DataTable
            Dim _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim odt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = Patientid
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@npatientid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = DrugName
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@sDrugName"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                odt = _gloEMRDatabase.GetDataTable("gsp_FillDrugProviderAssociation_Sig")
                Return odt

            Catch ex As gloDBException
                Throw ex
                Return Nothing
            Catch ex As gloEMRActorsException
                Throw ex
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching Prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                _gloEMRDatabase.Dispose()
            End Try
        End Function

        Public Function CheckForDeniedDrug10dot6(Optional ByRef PrescriptionId As Int16 = 0, Optional ByVal blnDTNF As Boolean = False) As Int16
            Try
                gloSurescriptGeneral.UpdateLog("Entering clsGloEMRPrescription.CheckForDeniedDrug")

                If Not IsNothing(_Prescriptions) Then
                    If _Prescriptions.Count > 0 Then
                        For icnt As Int32 = 0 To _Prescriptions.Count - 1
                            Dim drugdosage As String = ""
                            drugdosage = _Prescriptions.Item(icnt).Dosage
                            If _Prescriptions.Item(icnt).Status = "Approved" Then

                                If blnIsEPCSEnable = False Then   '' Check For EPCS Enabled

                                    If _Prescriptions.Item(icnt).IsNarcotics = 2 Or _Prescriptions.Item(icnt).IsNarcotics = 3 Or _Prescriptions.Item(icnt).IsNarcotics = 4 Or _Prescriptions.Item(icnt).IsNarcotics = 5 Then
                                        If System.Windows.Forms.MessageBox.Show("This Refill Request is for a controlled substance (Schedule III – V)," & _Prescriptions.Item(icnt).Medication & vbCrLf & "The message Deny with New Prescription to follow shall be sent ," & vbCrLf & " Do you want to continue?", "gloEMR", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                            If SendDeniedWithNewRxToFollowMessage("Approved drug is Schedule 3 to 5, Rx will be faxed to pharmacy") Then
                                                _Prescriptions.Item(icnt).Status = "Denied"
                                                _Prescriptions.Item(icnt).Renewed = ""
                                                If _Prescriptions.Count = 1 Then
                                                    'Make sure to call Print Rx for narcotic drug for this particular drug
                                                    Return 4
                                                Else
                                                    ''Make sure to call Print Rx for narcotic drug for this particular drug
                                                    PrescriptionId = icnt
                                                    Return 5
                                                End If

                                            Else
                                                Return 3
                                            End If

                                        Else
                                            Return 2
                                        End If
                                        Exit Function
                                    End If
                                End If '' ------EPCS

                                If blnDTNF Then
                                    If SendDeniedWithNewRxToFollowMessage() Then
                                        _Prescriptions.Item(icnt).Status = "DeniedWNewRx"
                                        _Prescriptions.Item(icnt).Renewed = ""
                                        Return 1
                                    Else
                                        Return 3
                                    End If
                                Else
                                    Return 0
                                    Exit Function
                                End If

                            End If
                            drugdosage = Nothing
                        Next
                    End If
                End If

                gloSurescriptGeneral.UpdateLog("Exiting clsGloEMRPrescription.CheckForDeniedDrug")
                Return -1
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred Checking Whether to send DeniedWithNewRxToFollow message: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return -1
            Catch ex As Exception
                gloSurescriptGeneral.UpdateLog("Error Occurred Checking Whether to send DeniedWithNewRxToFollow message: " & ex.Message)
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                CheckForDeniedDrug10dot6 = Nothing
                Throw obj

            Finally

            End Try
        End Function

        Private Function SendDeniedWithNewRxToFollowMessage(Optional ByVal strnotes As String = "", Optional ByVal selecteditem As Integer = 0) As Boolean
            'code commented by supriya 24/7/2008
            'Dim ogloInterface As New gloSureScriptInterface
            'code commented by supriya 24/7/2008
            Dim blnIsValid As Boolean
            Try
                'code added by supriya 24/7/2008
                MessageInValid = False
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
                ogloInterface = New gloSureScriptInterface
                'code added by supriya 24/7/2008
                Dim estatus As RefillStatus
                If _Prescriptions.Item(selecteditem).PhNCPDPID <> oOldgloPrescription.RxPharmacy.PharmacyID Then
                    estatus = RefillStatus.eDenied
                Else
                    estatus = RefillStatus.eDeniedWithNewRxToFollow
                End If

                gloSurescriptGeneral.checkDownloadVersion()

                If oOldgloPrescription.DrugsCol.Item(0).RefillsQualifier = "" Then
                    oOldgloPrescription.DrugsCol.Item(0).RefillsQualifier = "R"
                End If
                Dim blnflag As Boolean = False

                blnflag = ogloInterface.GenerateRefillResponseForDNTF(0, oOldgloPrescription, estatus, 0, "", strnotes)

                If blnflag Then
                    If ogloInterface.StatusMessageType.Length > 0 Then
                        System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End If

                    If Me.IsChangeRequest AndAlso ogloInterface.MessageName = "Status" Then
                        Using p As New PrescriptionBusinessLayer()
                            p.UpdateRxMessageStatus(Me.RxChangeRequest.MessageID, oOldgloPrescription.DrugsCol.Item(0).MessageType, "RxChange")
                        End Using
                    ElseIf ogloInterface.MessageName = "Status" Then
                        ogloInterface.UpdateRefillStatus(oOldgloPrescription, estatus, 0)
                        blnIsValid = True
                    Else
                        If ogloInterface.ValidationMessage.Length > 0 Then
                            System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessage, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        End If
                        blnIsValid = False
                    End If

                Else

                    blnIsValid = False
                End If
                If ogloInterface.ValidationMessageBuilderforDrug.ToString.Length > 0 Then
                    System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderforDrug.ToString, "gloEMR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                End If
                Return blnIsValid
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return Nothing

            Catch ex As Exception
                gloSurescriptGeneral.UpdateLog("Error Occurred processing NewRx: " & ex.Message)
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                SendDeniedWithNewRxToFollowMessage = Nothing
                Throw obj
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try

        End Function

        Public Sub InsertIntoResponseTransaction(ByVal Response As SureScriptResponseMessage, ByVal Flag As Boolean) 'Handles ogloInterface.MessageResponse
            Dim DBLayer As PrescriptionBusinessLayer = Nothing

            Try
                DBLayer = New PrescriptionBusinessLayer()
                DBLayer.InsertResponseTransaction(Response.ApprovalStatus, Response.StatusMessageType, Response.DenialReason, Response.Notes, Response.MessageID, Response.Denialcode, Response.RefReqPatientId, Response.ProviderId, Flag)
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while inserting response."
                Throw objex
            End Try
        End Sub

        Public Function FillProblemlist() As System.Data.DataTable
            Dim _gloEMRDatabase = New DataBaseLayer
            Dim odt As DataTable
            Try
                Dim strquery As String = "select  nproblemid,'Chief Complaints'=sCheifComplaint from problemlist where npatientid=" & _PatientID 'glogeneral.globalPatient.gnPatientID                    
                odt = _gloEMRDatabase.GetDataTable_Query(strquery)
                Return odt
            Catch ex As gloDBException
                Throw ex
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Prescription for viewing."
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function CheckRecordCount(ByVal strname As String) As Boolean
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim dt As DataTable = Nothing 'SLR: new not needed
            Try

                _DBParameter = New DBParameter
                _DBParameter.Value = strname
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Char
                _DBParameter.Name = "@Interval"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID 'globalPatient.gnPatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = Format(Now, "MM/dd/yyyy hh:mm:ss")
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtSysDate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = "P"
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Char
                _DBParameter.Name = "@formstatus"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_CheckRecordCount")
                If dt.Rows.Count > 0 Then

                    If dt.Rows(0)(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
                'dt.Clear()
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Checking Record Count"
                CheckRecordCount = Nothing
                Throw objex
            Finally
                If Not dt Is Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function FetchDrugDetailsMain(ByVal ArrDrugs As ArrayList) As Boolean
            Try
                _Prescriptions.AddRange(FetchDrugDetailsRevised(ArrDrugs, _intCurrentVisitID, _ProviderId, _PharmacyId, blnIsEPCSEnable))
                Return True
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
                Return False
            Finally
            End Try
        End Function

        Public Function CheckObsoleteDrug(ByVal DrugNDCCode As String) As Boolean
            Dim val As Boolean = False
            Try
                Using ogloGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    If ogloGSHelper.IsDrugExist(Convert.ToString(DrugNDCCode)) = False Then
                        val = True
                    End If
                End Using
            Catch ex As Exception

            Finally

            End Try
            Return val
        End Function


        Public Sub AddNewPrescription(ByVal row As DataRow, ByVal Routes As List(Of String))


            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing
            Try
                If Not IsNothing(row) Then

                    objPrescription = New Prescription
                    objPrescription.DrugID = row("nDrugsID")
                    objPrescription.Medication = row("sDrugName")
                    objPrescription.Dosage = row("sDosage")
                    objPrescription.routes = Routes

                    If objPrescription.routes IsNot Nothing Then
                        If objPrescription.routes.Count <= 2 Then 'One BLANK entry added on first position
                            objPrescription.Route = row("sRoute")
                        End If
                    Else
                        objPrescription.Route = row("sRoute")
                    End If

                    objPrescription.Frequency = row("sFrequency")
                    objPrescription.Duration = row("sDuration")
                    objPrescription.UserName = globalSecurity.gstrLoginName
                    objPrescription.IsNarcotics = row("IsNarcotics")
                    objPrescription.mpid = row("mpid")
                    objPrescription.NDCCode = row("NDCCode")
                    objPrescription.RxType = row("DrugType")

                    If row.Table.Columns.Contains("sRefills") Then
                        objPrescription.Refills = row("sRefills")
                    Else
                        objPrescription.Refills = "0"
                    End If

                    If IsNothing(objPrescription.DosageForm) Then
                        objPrescription.DosageForm = row("DrugForm")
                    ElseIf objPrescription.DosageForm = "" Then
                        objPrescription.DosageForm = row("DrugForm")
                    End If

                    If IsNothing(objPrescription.DosageForm) Or objPrescription.DosageForm = "" Then
                        objPrescription.DosageForm = GetDosageformCode(objPrescription.NDCCode)
                    End If

                    objPrescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                    objPrescription.State = "A"

                    Dim amt As String = Convert.ToString(row("sAmount")).Trim()
                    Dim potencyCode As String = row("spotencycode")
                    Dim potencyUnit As String = row("sPotencyUnit")

                    objPrescription.PotencyCode = potencyCode
                    objPrescription.PotencyUnit = potencyUnit

                    If Not String.IsNullOrWhiteSpace(amt) Then
                        If Not IsNumeric(amt) Then
                            Dim qty As String() = amt.Split(" ")
                            If (qty.Length >= 1) Then
                                amt = qty(0)
                            End If
                        End If
                        objPrescription.Amount = amt + " " + potencyUnit
                    End If

                    objPrescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                    objPrescription.PrescriptionID = 0
                    objPrescription.IseRxed = 0
                    objPrescription.VisitID = _intCurrentVisitID
                    objPrescription.Startdate = Now.Date

                    If TransactionMode = _TransactionMode.Edit Then
                        objPrescription.Prescriptiondate = _Prescriptiondate
                    End If

                    objPrescription.ProviderID = _ProviderId
                    objPrescription.PharmacyID = _PharmacyId
                    If objPrescription.IsNarcotics = 0 Then  'not narcotic
                        If objPrescription.RxType.ToUpper = "BRANDOTC" Or objPrescription.RxType.ToUpper = "GENERICOTC" Or objPrescription.RxType.ToUpper = "SUPPLYOTC" Then
                            objPrescription.Method = "OTC"
                        Else
                            objPrescription.Method = "eRx"
                        End If

                    Else
                        objPrescription.Method = "Print"
                    End If


                    If RxChangeRequest IsNot Nothing AndAlso Me.RxChangeRequest.Type = gloGlobal.SS.ChangeRequestType.TherapeuticSubstitution Then
                        If Me.RxChangeRequest.MedRequested Is Nothing Then
                            objPrescription.MessageType = "RxChangeRequest"
                            objPrescription.Renewed = "Changed" & " " & Now & " " & globalSecurity.gstrLoginName
                            objPrescription.PharmacyID = Me.RxChangeRequest.PharmacyID
                        End If
                    End If
                    objPrescription.AlternativeFormId = row("AlternativeFormID")

                    _Prescriptions.Add(objPrescription)

                End If

            Catch ex As gloDBException

            Catch ex As gloEMRActorsException

            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching drug details."
                Throw objex

            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Sub

        Public Function FetchDrugDetails(ByVal ndc As String) As Boolean
            Dim objprescription As Prescription = Nothing
            Try
                objprescription = FetchDrugDetailsByNDC(ndc)

                objprescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                objprescription.PrescriptionID = 0
                objprescription.IseRxed = 0
                'Set visit id to current visit id
                objprescription.VisitID = _intCurrentVisitID
                'Set Start date to current date for new item
                objprescription.Startdate = Now.Date
                If TransactionMode = _TransactionMode.Edit Then
                    objprescription.Prescriptiondate = _Prescriptiondate
                    'objprescription.ProviderID = _ProviderId
                End If
                objprescription.ProviderID = _ProviderId
                objprescription.PharmacyID = _PharmacyId

                ' added on 20091010 for "SendRX" functionality
                If objprescription.IsNarcotics = 0 Then  'not narcotic
                    If objprescription.RxType.ToUpper = "BRANDOTC" Or objprescription.RxType.ToUpper = "GENERICOTC" Or objprescription.RxType.ToUpper = "SUPPLYOTC" Then
                        objprescription.Method = "OTC"
                    Else
                        objprescription.Method = "eRx"
                    End If

                Else
                    objprescription.Method = "Print"
                End If


                _Prescriptions.Add(objprescription)
                Return True
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
                Return False
            Finally
                'SLR: DOn't free it, otherwise _Prescriptions will contain wrong memory
                'objprescription.Dispose()
                objprescription = Nothing
            End Try
        End Function

        Public Function FetchDrugDetailsForProviderFav(ByVal mpid As Int64, ByVal ndccode As String, ByVal SIGid As Long) As Boolean
            Dim _oRxbusinessLayer As New RxBusinesslayer(_PatientID)
            Dim objprescription As Prescription = Nothing
            Try
                objprescription = _oRxbusinessLayer.FetchDrugDetailsByMpidOrNdc(mpid, ndccode, SIGid)

                objprescription.CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                objprescription.PrescriptionID = 0
                objprescription.IseRxed = 0
                'Set visit id to current visit id
                objprescription.VisitID = _intCurrentVisitID
                'Set Start date to current date for new item
                objprescription.Startdate = Now.Date
                If TransactionMode = _TransactionMode.Edit Then
                    objprescription.Prescriptiondate = _Prescriptiondate
                    'objprescription.ProviderID = _ProviderId
                End If
                objprescription.ProviderID = _ProviderId
                objprescription.PharmacyID = _PharmacyId

                ' added on 20091010 for "SendRX" functionality
                If objprescription.IsNarcotics = 0 Then  'not narcotic
                    If objprescription.RxType.ToUpper = "BRANDOTC" Or objprescription.RxType.ToUpper = "GENERICOTC" Or objprescription.RxType.ToUpper = "SUPPLYOTC" Then
                        objprescription.Method = "OTC"
                    Else
                        objprescription.Method = "eRx"
                    End If

                Else
                    objprescription.Method = "Print"
                End If


                _Prescriptions.Add(objprescription)
                Return True
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
                Return False
            Finally
                objprescription = Nothing
                _oRxbusinessLayer.Dispose()
                _oRxbusinessLayer = Nothing
            End Try
        End Function

        Public Function GetClassifiedDrugsByMPID(ByVal ConceptTreeID As Integer) As List(Of Int32)
            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                Return oDIBGSHelper.GetClassifiedDrugs(ConceptTreeID)
            End Using
        End Function

        Public Function GetClassifiedTree(ByVal ConceptTreeID As Integer) As DataTable
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = ConceptTreeID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@ParentConceptTreeID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("GSDD_GetClassifiedTree")

                If Not IsNothing(dt) Then
                    If dt.Rows.Count <= 0 Then
                        dt.Dispose()
                        dt = Nothing
                        Return Nothing
                    End If
                End If

                Return dt

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Classified Drugs."
                Throw objex
                Return Nothing
            Finally
                'objPrescription = Nothing
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function Get_DrugDetails(ByVal _dtTVP As DataTable) As DataTable
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = _dtTVP
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Structured
                objParameter.Name = "@MPIDs"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("GSDD_GetDrugDetails")

                If Not IsNothing(dt) Then
                    If dt.Rows.Count <= 0 Then
                        dt.Dispose()
                        dt = Nothing
                        Return Nothing
                    End If
                End If

                Return dt

            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Classified Drugs."
                Throw objex
                Return Nothing
            Finally
                'objPrescription = Nothing
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function FillProviderControls(ByVal id As Long, ByRef nLoginProviderID As Long, Optional ByVal strsearch As String = "") As DataTable

            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter

            Dim dtDrugs As DataTable = Nothing
            Try
                objParameter = New DBParameter
                objParameter.Value = id
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@npatientid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = nLoginProviderID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nLoginProviderid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = strsearch
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@drugletter"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dtDrugs = _gloEMRDatabase.GetDataTable("gsp_Fill_DrugProviderAssociation")


            Catch ex As gloDBException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                _gloEMRDatabase.Dispose()
            End Try

            Return dtDrugs
        End Function

        Public Function GetProviderFrequentlyUsedDrugs(ByVal _ProviderId As Long, Optional ByVal keyword As String = "") As DataTable

            Dim dbHelper As DataBaseLayer = New DataBaseLayer
            Dim param As DBParameter
            Dim dtDrugs As DataTable = Nothing

            Try
                param = New DBParameter
                param.Value = LCase(keyword)
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Char
                param.Name = "@drugletter"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = _ProviderId
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.BigInt
                param.Name = "@nProviderID"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = Format(Now.Date, "MM/dd/yyyy")
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.DateTime
                param.Name = "@dtSysDate"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                dtDrugs = dbHelper.GetDataTable("gsp_FillProviderFrequentlyDrugs")

                Return dtDrugs

            Catch ex As FormularyServiceException
                Throw ex
                Return Nothing
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not dtDrugs Is Nothing Then
                    dtDrugs.Dispose()
                    dtDrugs = Nothing
                End If
            End Try
        End Function

        Public Function GetFrequentlyUsedDrugs(Optional ByVal keyword As String = "") As DataTable

            Dim dbHelper As DataBaseLayer = New DataBaseLayer
            Dim param As DBParameter
            Dim dtDrugs As DataTable = Nothing

            Try
                param = New DBParameter
                param.Value = LCase(keyword)
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Char
                param.Name = "@drugletter"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = 13
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Int
                param.Name = "@flag"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = _PatientID
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.BigInt
                param.Name = "@PatientID"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = Format(Now.Date, "MM/dd/yyyy")
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.DateTime
                param.Name = "@dtSysDate"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                dtDrugs = dbHelper.GetDataTable("gsp_FillDrugs_Mst")

                Return dtDrugs

            Catch ex As FormularyServiceException
                Throw ex
                Return Nothing
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not dtDrugs Is Nothing Then
                    dtDrugs.Dispose()
                    dtDrugs = Nothing
                End If
            End Try
        End Function

        Public Function GetPracticeFavoritesDrugs(Optional ByVal keyword As String = "") As DataTable

            Dim dbHelper As DataBaseLayer = New DataBaseLayer
            Dim param As DBParameter
            Dim dtDrugs As DataTable = Nothing

            Try
                param = New DBParameter
                param.Value = LCase(keyword)
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Char
                param.Name = "@drugletter"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = 12
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Int
                param.Name = "@flag"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = _PatientID
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.BigInt
                param.Name = "@PatientID"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                dtDrugs = dbHelper.GetDataTable("gsp_FillDrugs_Mst")

                Return dtDrugs

            Catch ex As FormularyServiceException
                Throw ex
                Return Nothing
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not dtDrugs Is Nothing Then
                    dtDrugs.Dispose()
                    dtDrugs = Nothing
                End If
            End Try
        End Function

        Public Function GetAllDrugs(Optional ByVal keyword As String = "") As DataTable

            Dim dbHelper As DataBaseLayer = New DataBaseLayer
            Dim param As DBParameter
            Dim dtDrugs As DataTable = Nothing

            Try
                param = New DBParameter
                param.Value = LCase(keyword)
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Char
                param.Name = "@drugletter"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = 11
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.Int
                param.Name = "@flag"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                param = New DBParameter
                param.Value = _PatientID
                param.Direction = ParameterDirection.Input
                param.DataType = SqlDbType.BigInt
                param.Name = "@PatientID"
                dbHelper.DBParametersCol.Add(param)
                param = Nothing

                dtDrugs = dbHelper.GetDataTable("gsp_FillDrugs_Mst")

                Return dtDrugs

            Catch ex As FormularyServiceException
                Throw ex
                Return Nothing
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not dtDrugs Is Nothing Then
                    dtDrugs.Dispose()
                    dtDrugs = Nothing
                End If
            End Try
        End Function

        Public Function FetchPrescriptionforView(ByVal VisitId As Int64, ByVal dtPrescriptiondate As DateTime) As Prescriptions
            Dim objPrescriptions As Prescriptions = Nothing
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing 'SLR: no neew neded

            Try
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dt = helper.GetPrescriptionsByPatient(_PatientID, dtPrescriptiondate, VisitId)
                End Using

                objPrescriptions = New Prescriptions

                If dt IsNot Nothing Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        objPrescription = New Prescription

                        objPrescription.PrescriptionID = dt.Rows(i)("PrescriptionID")
                        objPrescription.IseRxed = dt.Rows(i)("IseRxed")
                        objPrescription.VisitID = dt.Rows(i)("VisitID")
                        objPrescription.PatientID = dt.Rows(i)("PatientID")
                        objPrescription.Medication = dt.Rows(i)("Medication")
                        objPrescription.Dosage = dt.Rows(i)("sDosage")
                        objPrescription.Route = dt.Rows(i)("sRoute")
                        objPrescription.Frequency = dt.Rows(i)("sFrequency")
                        objPrescription.Refills = dt.Rows(i)("sRefills")
                        objPrescription.Duration = dt.Rows(i)("sDuration")

                        If Not IsDBNull(dt.Rows(i)("PrescriptionDate")) Then
                            objPrescription.Prescriptiondate = dt.Rows(i)("PrescriptionDate")
                        Else
                            objPrescription.CheckEndDate = "12:00:00 AM"
                        End If

                        If Not IsDBNull(dt.Rows(i)("EndDate")) Then
                            objPrescription.Enddate = dt.Rows(i)("EndDate")
                            objPrescription.CheckEndDate = True
                        Else
                            objPrescription.CheckEndDate = False
                        End If

                        objPrescription.Notes = dt.Rows(i)("sNotes")
                        objPrescription.Method = dt.Rows(i)("sMethod")
                        objPrescription.Amount = dt.Rows(i)("sAmount")
                        objPrescription.DrugID = dt.Rows(i)("DrugID")
                        objPrescription.LotNo = dt.Rows(i)("LotNo")
                        objPrescription.UserName = dt.Rows(i)("UserName")
                        objPrescription.ProviderID = dt.Rows(i)("ProviderId")
                        objPrescription.ChiefComplaint = dt.Rows(i)("ChiefComplaints")
                        objPrescription.Problems = dt.Rows(i)("ProblemID")

                        objPrescription.ReasontoOverride = dt.Rows(i)("Reason")
                        objPrescription.RefillQualifier = dt.Rows(i)("RefillQualifier")
                        objPrescription.NDCCode = dt.Rows(i)("NDCCode")
                        objPrescription.IsNarcotics = dt.Rows(i)("IsNarcotic")
                        objPrescription.mpid = dt.Rows(i)("mpid")
                        objPrescription.DosageForm = dt.Rows(i)("DrugForm")
                        objPrescription.StrengthUnit = dt.Rows(i)("StrengthUnit")
                        objPrescription.PhNCPDPID = dt.Rows(i)("PhNCPDPID")
                        objPrescription.PhContactID = dt.Rows(i)("PhContactID")
                        objPrescription.PharmacyName = dt.Rows(i)("PharmacyName")
                        objPrescription.PhAddressline1 = dt.Rows(i)("PhAddressline1")
                        objPrescription.PhAddressline2 = dt.Rows(i)("PhAddressline2")
                        objPrescription.PhCity = dt.Rows(i)("PhCity")
                        objPrescription.PhState = dt.Rows(i)("PhState")
                        objPrescription.PhZip = dt.Rows(i)("PhZip")
                        objPrescription.PhEmail = dt.Rows(i)("PhEmail")
                        objPrescription.PhFax = dt.Rows(i)("PhFax")
                        objPrescription.PhPhone = dt.Rows(i)("PhPhone")
                        objPrescription.PhServiceLevel = dt.Rows(i)("PhServiceLevel")
                        objPrescription.PrescriberNotes = dt.Rows(i)("PrescriberNotes")
                        objPrescription.eRxStatus = dt.Rows(i)("eRxStatus")
                        objPrescription.PAReferenceID = dt.Rows(i)("PAReferenceID")
                        objPrescription.PCTransactionID = dt.Rows(i)("nPCTransactionID")
                        If objPrescription.PCTransactionID > 0 Then
                            Using dblayer As New PatientCommunicationBusinessLayer
                                objPrescription.PDRPrograms = dblayer.GetAllPrograms(objPrescription.PCTransactionID)
                            End Using
                        End If
                        ''''EPCS Status
                        ''objPrescription.EPCSeRxStatus = dt.Rows(i)("sEpcsStatus")

                        objPrescriptions.Add(objPrescription)
                    Next

                End If

                Return objPrescriptions

            Catch ex As gloDBException
                Throw ex
                Return Nothing
            Catch ex As gloEMRActorsException
                Throw ex
                Return Nothing
            Catch ex As PrescriptionException
                Throw ex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Function

        Friend Function FetchPastVisitDate(ByVal id As Int64) As Object
            Dim tmpvisitdate As DateTime
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            Try
                _gloEMRDatabase = New DataBaseLayer
                Dim objParameter As DBParameter
                objParameter = New DBParameter
                objParameter.Value = id
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                tmpvisitdate = _gloEMRDatabase.GetDataValue("gsp_GetVisitDate")

                If Not IsDBNull(tmpvisitdate) Then
                    Return tmpvisitdate
                Else
                    Return Nothing
                End If
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Visit date"
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Sub FetchPastVisitDate()
            Dim tmpvisitdate As DateTime
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            Try
                _gloEMRDatabase = New DataBaseLayer
                Dim objParameter As DBParameter
                objParameter = New DBParameter
                objParameter.Value = _intPastVisitID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                tmpvisitdate = _gloEMRDatabase.GetDataValue("gsp_GetVisitDate")

                If Not IsDBNull(tmpvisitdate) Then
                    _PastVisitDate = tmpvisitdate
                    _VisitDate = tmpvisitdate
                Else
                    _PastVisitDate = Nothing
                    _VisitDate = Nothing
                End If
            Catch ex As gloDBException
                _PastVisitDate = Nothing
                _VisitDate = Nothing
            Catch ex As gloEMRActorsException
                _PastVisitDate = Nothing
                _VisitDate = Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Visit date"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Sub

        Private Function FetchPrescriptionforUpdate(ByVal VisitId As Int64, ByVal dtPrescriptiondate As DateTime, ByVal _Prescriptions As Prescriptions, Optional ByVal intflag As Int16 = 0, Optional ByVal TransactionID As Int64 = 0, Optional ByVal changeRequestType As gloGlobal.SS.ChangeRequestType = gloGlobal.SS.ChangeRequestType.None) As Boolean
            Dim objPrescription As Prescription = Nothing
            Dim dt As DataTable = Nothing
            Try
                Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    dt = helper.GetPrescriptionsByPatient(_PatientID, dtPrescriptiondate, VisitId)
                End Using

                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        If intflag = 1 Then
                            Dim _ServerTime As DateTime
                            _ServerTime = getServerTime()
                            If DateDiff(DateInterval.Minute, dt.Rows.Item(0)(14), _ServerTime) > clsgeneral.gnThresholdSetting Then
                                FetchPrescriptionforUpdate = Nothing
                                dt.Dispose()
                                dt = Nothing
                                Exit Function
                            End If
                        End If
                    End If
                End If

                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        For i As Int16 = 0 To dt.Rows.Count - 1

                            If TransactionID = dt.Rows(i)("PrescriptionID") Then
                                If Not changeRequestType = gloGlobal.SS.ChangeRequestType.PriorAuthorizationRequired AndAlso "RxChangeRequest" <> Convert.ToString(dt.Rows(i)("MessageType")) Then
                                    Continue For
                                End If
                            End If

                            objPrescription = New Prescription

                            objPrescription.PrescriptionID = dt.Rows(i)("PrescriptionID")
                            objPrescription.IseRxed = dt.Rows(i)("IseRxed")
                            objPrescription.VisitID = dt.Rows(i)("VisitID")
                            objPrescription.PatientID = dt.Rows(i)("PatientID")
                            objPrescription.Medication = dt.Rows(i)("Medication")
                            objPrescription.Dosage = dt.Rows(i)("sDosage")

                            objPrescription.Frequency = dt.Rows(i)("sFrequency")

                            If Not IsDBNull(dt.Rows(i)("mpid")) Then ' code added by sagar
                                objPrescription.mpid = dt.Rows(i)("mpid")
                            Else
                                objPrescription.mpid = 0
                            End If

                            'CS#377102 integrated from 9000
                            'If objPrescription.mpid <> 0 Then
                            '    Dim RoutesList As New List(Of String)
                            '    Using objPrescriptioLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                            '        RoutesList = objPrescriptioLayer.GetDrugRoutes(objPrescription.mpid)
                            '    End Using

                            '    If RoutesList IsNot Nothing Then
                            '        If RoutesList.Count > 2 Then
                            '            objPrescription.routes = RoutesList
                            '        End If
                            '    End If
                            'End If

                            objPrescription.Route = dt.Rows(i)("sRoute")
                            objPrescription.Refills = dt.Rows(i)("sRefills")
                            If Not IsNothing(objPrescription.Refills) Then
                                If objPrescription.Refills.Trim.Length = 0 Then
                                    objPrescription.Refills = "0"
                                End If
                            Else
                                objPrescription.Refills = "0"
                            End If

                            objPrescription.Duration = dt.Rows(i)("sDuration")
                            objPrescription.Startdate = dt.Rows(i)("StartDate")
                            'If Not (dt.Rows(i).IsNull(10)) Then
                            If Not IsDBNull(dt.Rows(i)("EndDate")) Then  'code added by sagar
                                objPrescription.Enddate = CType(dt.Rows(i)("EndDate"), System.DateTime) 'dt.Rows(i)(10)
                                objPrescription.CheckEndDate = True
                            Else
                                objPrescription.CheckEndDate = False
                            End If

                            objPrescription.Notes = dt.Rows(i)("sNotes") 'this is for Pharmacy Notes
                            objPrescription.PrescriberNotes = dt.Rows(i)("PrescriberNotes") 'this is for Prescriber Notes
                            objPrescription.Method = dt.Rows(i)("sMethod")
                            If dt.Rows(i)("MaySubstitute") = 0 Then
                                objPrescription.Maysubstitute = True
                            Else
                                objPrescription.Maysubstitute = False
                            End If

                            objPrescription.Prescriptiondate = dt.Rows(i)("PrescriptionDate")

                            objPrescription.DrugID = dt.Rows(i)("DrugID")
                            objPrescription.LotNo = dt.Rows(i)("LotNo")

                            'If Not (dt.Rows(i).IsNull(18)) Then
                            If Not IsDBNull(dt.Rows(i)("Expirationdate")) Then ' code added by sagar
                                objPrescription.ExpirationDate = CType(dt.Rows(i)("Expirationdate"), System.DateTime) 'dt.Rows(i)(18)
                                objPrescription.CheckExpiryDate = True
                            Else
                                objPrescription.CheckExpiryDate = False
                            End If

                            If Not IsDBNull(dt.Rows(i)("UserName")) Then ' code added by sagar
                                objPrescription.UserName = dt.Rows(i)("UserName")
                            Else
                                objPrescription.UserName = ""
                            End If
                            If Not IsDBNull(dt.Rows(i)("ProviderId")) Then ' code added by sagar
                                objPrescription.ProviderID = dt.Rows(i)("ProviderId")
                            Else
                                objPrescription.ProviderID = 0
                            End If


                            If Not IsDBNull(dt.Rows(i)("ChiefComplaints")) Then ' code added by sagar
                                objPrescription.ChiefComplaint = dt.Rows(i)("ChiefComplaints")
                            Else
                                objPrescription.ChiefComplaint = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("ProblemID")) Then ' code added by sagar
                                objPrescription.Problems = dt.Rows(i)("ProblemID")
                            Else
                                objPrescription.Problems = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("Renewed")) Then ' code added by sagar
                                objPrescription.Renewed = dt.Rows(i)("Renewed")
                            Else
                                objPrescription.Renewed = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("Reason")) Then ' code added by supriya
                                objPrescription.ReasontoOverride = dt.Rows(i)("Reason")
                            Else
                                objPrescription.ReasontoOverride = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhContactID")) Then ' code added by sagar
                                objPrescription.PhContactID = dt.Rows(i)("PhContactID")
                                objPrescription.PharmacyID = objPrescription.PhContactID
                            Else
                                objPrescription.PhContactID = 0
                                objPrescription.PharmacyID = objPrescription.PhContactID
                            End If

                            'If Not IsDBNull(dt.Rows(i)("PharmacyId")) Then ' code added by sagar
                            '    objPrescription.PharmacyID = dt.Rows(i)("PharmacyId")
                            'Else
                            '    objPrescription.PharmacyID = 0
                            'End If

                            If Not IsDBNull(dt.Rows(i)("PharmacyName")) Then ' code added by sagar
                                objPrescription.PharmacyName = dt.Rows(i)("PharmacyName")
                            Else
                                objPrescription.PharmacyName = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhNCPDPID")) Then ' code added by sagar
                                objPrescription.PhNCPDPID = dt.Rows(i)("PhNCPDPID")
                            Else
                                objPrescription.PhNCPDPID = ""
                            End If



                            If Not IsDBNull(dt.Rows(i)("PhAddressline1")) Then ' code added by sagar
                                objPrescription.PhAddressline1 = dt.Rows(i)("PhAddressline1")
                            Else
                                objPrescription.PhAddressline1 = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhAddressline2")) Then ' code added by sagar
                                objPrescription.PhAddressline2 = dt.Rows(i)("PhAddressline2")
                            Else
                                objPrescription.PhAddressline2 = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhCity")) Then ' code added by sagar
                                objPrescription.PhCity = dt.Rows(i)("PhCity")
                            Else
                                objPrescription.PhCity = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhState")) Then ' code added by sagar
                                objPrescription.PhState = dt.Rows(i)("PhState")
                            Else
                                objPrescription.PhState = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhZip")) Then ' code added by sagar
                                objPrescription.PhZip = dt.Rows(i)("PhZip")
                            Else
                                objPrescription.PhZip = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhEmail")) Then ' code added by sagar
                                objPrescription.PhEmail = dt.Rows(i)("PhEmail")
                            Else
                                objPrescription.PhEmail = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhFax")) Then ' code added by sagar
                                objPrescription.PhFax = dt.Rows(i)("PhFax")
                            Else
                                objPrescription.PhFax = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhPhone")) Then ' code added by sagar
                                objPrescription.PhPhone = dt.Rows(i)("PhPhone")
                            Else
                                objPrescription.PhPhone = ""
                            End If

                            If Not IsDBNull(dt.Rows(i)("PhServiceLevel")) Then ' code added by sagar
                                objPrescription.PhServiceLevel = dt.Rows(i)("PhServiceLevel")
                            Else
                                objPrescription.PhServiceLevel = ""
                            End If


                            'DBnull is handled in the query, also the value will not be null as there this field will contain value of "PRN" / "R" depending on teh chkPRN check box is selected on the custom prescription control.
                            objPrescription.RefillQualifier = dt.Rows(i)("RefillQualifier")


                            'For De-Normalization
                            '----NDCCode
                            If Not IsDBNull(dt.Rows(i)("NDCCode")) Then ' code added by supriya
                                objPrescription.NDCCode = dt.Rows(i)("NDCCode")
                            Else
                                objPrescription.NDCCode = ""
                            End If

                            '----IsNarcotic - to get narcotic type value 
                            If Not IsDBNull(dt.Rows(i)("IsNarcotic")) Then ' code added by sagar
                                objPrescription.IsNarcotics = dt.Rows(i)("IsNarcotic")
                            Else
                                objPrescription.IsNarcotics = 0
                            End If



                            '----DrugForm
                            If Not IsDBNull(dt.Rows(i)("DrugForm")) Then ' code added by sagar
                                objPrescription.DosageForm = dt.Rows(i)("DrugForm")
                            Else
                                objPrescription.DosageForm = ""
                            End If

                            '----Drug Quantity Qualifier
                            If Not IsDBNull(dt.Rows(i)("StrengthUnit")) Then ' code added by sagar
                                objPrescription.StrengthUnit = dt.Rows(i)("StrengthUnit")
                            Else
                                objPrescription.StrengthUnit = ""
                            End If

                            '----eRxStatus
                            If Not IsDBNull(dt.Rows(i)("eRxStatus")) Then
                                objPrescription.eRxStatus = dt.Rows(i)("eRxStatus")
                            Else
                                objPrescription.eRxStatus = ""
                            End If

                            '----eRxStatusMessage
                            If Not IsDBNull(dt.Rows(i)("eRxStatusMessage")) Then
                                objPrescription.eRxStatusMessage = dt.Rows(i)("eRxStatusMessage")
                            Else
                                objPrescription.eRxStatusMessage = ""
                            End If

                            '----RxType
                            If dt.Columns.Contains("RxType") Then
                                If Not IsDBNull(dt.Rows(i)("RxType")) Then
                                    If Convert.ToString(dt.Rows(i)("RxType")) <> "" Then
                                        objPrescription.RxType = dt.Rows(i)("RxType")
                                    Else
                                        objPrescription.RxType = Me.GetRxType(objPrescription.mpid)
                                    End If

                                End If
                            End If

                            '----EpcsStatus  ''BDO Audit
                            If Not IsDBNull(dt.Rows(i)("EpcsStatus")) Then
                                objPrescription.EPCSeRxStatus = dt.Rows(i)("EpcsStatus")
                            Else
                                objPrescription.EPCSeRxStatus = ""
                            End If

                            objPrescription.Flag = (dt.Rows(i)("blnFlag"))
                            objPrescription.CPOEOrder = (dt.Rows(i)("CPOEOrder"))
                            objPrescription.MedicationAdministered = (dt.Rows(i)("MedicationAdministered"))
                            objPrescription.MessageType = (dt.Rows(i)("MessageType"))
                            objPrescription.MessageID = (dt.Rows(i)("MessageID"))
                            objPrescription.IsFormularyQueried = (dt.Rows(i)("FormularyQueried"))
                            objPrescription.PAReferenceID = dt.Rows(i)("PAReferenceID")
                            objPrescription.PCTransactionID = dt.Rows(i)("nPCTransactionID")
                            If objPrescription.PCTransactionID > 0 Then
                                Using dblayer As New PatientCommunicationBusinessLayer
                                    objPrescription.PDRPrograms = dblayer.GetAllPrograms(objPrescription.PCTransactionID)
                                End Using
                            End If


                            objPrescription.State = "U"

                            objPrescription.Amount = Convert.ToString(dt.Rows(i)("sAmount")).Trim()

                            Dim potencyCode As String = dt.Rows(i)("spotencycode")

                            If String.IsNullOrWhiteSpace(potencyCode) Then
                                potencyCode = GetLastSavedPotencyCode(objPrescription.NDCCode, objPrescription.mpid)
                            End If

                            objPrescription.PotencyCode = potencyCode
                            objPrescription.AlternativeFormId = dt.Rows(i)("AlternativeFormID")

                            _Prescriptions.Add(objPrescription)

                            'SLR: Don't free
                            'objPrescription.Dispose()
                            'objPrescription = Nothing
                        Next
                        Return True


                        'End If
                    Else
                        Return False

                    End If
                Else
                    Return False
                End If



            Catch ex As gloDBException
                Return False
            Catch ex As gloEMRActorsException
                Return False
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching prescription for viewing."
                FetchPrescriptionforUpdate = Nothing
                Throw objex
            Finally
                'objPrescription = Nothing
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                'If Not IsNothing(_gloEMRDatabase) Then
                '    _gloEMRDatabase.Dispose()
                '    _gloEMRDatabase = Nothing
                'End If
            End Try
        End Function

        Public Sub FetchPrescriptionforUpdate(ByVal dtPrescriptiondate As DateTime, Optional ByVal intflag As Int16 = 0, Optional ByVal TransactionID As Int64 = 0, Optional ByVal changeRequestType As gloGlobal.SS.ChangeRequestType = gloGlobal.SS.ChangeRequestType.None)
            Try
                If Not IsNothing(_Prescriptions) Then
                    _Prescriptions.Dispose()
                    _Prescriptions = Nothing
                    _Prescriptions = New Prescriptions
                End If


                If FetchPrescriptionforUpdate(_intCurrentVisitID, dtPrescriptiondate, _Prescriptions, intflag, TransactionID, changeRequestType) Then
                    If _Prescriptions.Count > 0 Then
                        Dim _Prescription As Prescription
                        eTransactionMode = _TransactionMode.Edit
                        _Prescription = PrescriptionCol.Item(0)
                        'new logic
                        _intCurrentVisitID = _Prescription.VisitID
                        _Prescriptiondate = _Prescription.Prescriptiondate
                        '_ProviderId = _Prescription.ProviderID
                        If changeRequestType = gloGlobal.SS.ChangeRequestType.PriorAuthorizationRequired Then                            
                            _Prescription.Status = "Approved"
                            _Prescription.MessageType = "RxChangeRequest"
                            _Prescription.Renewed = "Changed" & " " & Now & " " & globalSecurity.gstrLoginName

                            If RxChangeRequest.MedPrescribed.Refills IsNot Nothing Then
                                _Prescription.RefillQualifier = RxChangeRequest.MedPrescribed.Refills.Qualifier
                            End If
                        End If
                        'get the visitdate for the given visitid
                        If _intCurrentVisitID <> 0 Then
                            _VisitDate = FetchPastVisitDate(_intCurrentVisitID)
                        End If
                    Else
                        If _intCurrentVisitID <> 0 Then
                            _VisitDate = FetchPastVisitDate(_intCurrentVisitID)
                        End If
                    End If
                End If

            Catch ex As PrescriptionException

            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error updating Prescription Details."
                Throw objex

            Finally

            End Try
        End Sub

        Public Function FillPrescription(ByVal strflag As Char) As Prescriptions
            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objPrescriptions As Prescriptions
            Dim objPrescription As Prescription
            Dim dt As DataTable = Nothing 'SLR: new is not needed

            Try
                objParameter = New DBParameter
                objParameter.Value = strflag
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Char
                objParameter.Name = "@Interval"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _PatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = Format(Now, "MM/dd/yyyy hh:mm:ss")
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@dtSysDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_ViewPrescription")
                objPrescriptions = New Prescriptions

                For i As Int16 = 0 To dt.Rows.Count - 1
                    objPrescription = New Prescription

                    objPrescription.VisitID = dt.Rows(i)(0)
                    objPrescription.Prescriptiondate = dt.Rows(i)(1)

                    objPrescriptions.Add(objPrescription)
                Next
                Return objPrescriptions
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing

            Finally
                objPrescription = Nothing
                If Not dt Is Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If

            End Try
        End Function

        Private Function SplitAllergy(ByVal AllergyType As String) As Array
            Try
                Dim _result As String()
                _result = AllergyType.Split("|")
                Return _result
            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Private Function GetHistory_CategoryWise(ByVal VisitID As Long) As Histories

            Dim _gloEMRDatabase As DataBaseLayer = Nothing
            _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim objHistories As Histories = Nothing
            Dim dt As DataTable = Nothing  'SLR: new not needed
            Dim objHistory As History = Nothing

            Try
                objParameter = New DBParameter
                objParameter.Value = _PatientID 'globalPatient.gnPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = "All"
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Category"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = "-InActive"
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@Comments"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = VisitID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@VisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                If VisitID = 0 Then
                    objParameter = New DBParameter
                    objParameter.Value = Now.Date
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.DataType = SqlDbType.DateTime
                    objParameter.Name = "@VisitDate"
                    _gloEMRDatabase.DBParametersCol.Add(objParameter)
                    objParameter = Nothing
                End If


                dt = _gloEMRDatabase.GetDataTable("gsp_GetHistory_CategoryWise")
                objHistories = New Histories

                For i As Integer = 0 To dt.Rows.Count - 1
                    objHistory = New History

                    objHistory.HistoryItem = dt.Rows(i)(0)
                    objHistory.Comments = dt.Rows(i)(1)
                    objHistory.DrugID = dt.Rows(i)(2)
                    'After History Denormalization
                    objHistory.DrugName = dt.Rows(i)("sDrugName")
                    objHistory.DrugDosage = dt.Rows(i)("sDosage")
                    objHistory.NDCCode = dt.Rows(i)("sNDCCode")
                    objHistory.HistoryCategory = Convert.ToString(dt.Rows(i)("sHistoryCategory"))
                    objHistory.AllergyClassID = Convert.ToString(dt.Rows(i)("sAllergyClassID"))
                    'After History Denormalization

                    ''''for CCHIT11
                    ''''get the allergies and add only those allergies in history collection which are "Active"
                    Dim AllergyType As String = dt.Rows(i)("sReaction")
                    Dim retval As String() = SplitAllergy(AllergyType)

                    If Not IsNothing(retval) Then
                        If retval.Length > 1 Then
                            AllergyType = retval(1)
                        End If

                    Else
                        AllergyType = ""
                    End If


                    If AllergyType = "Active" Then
                        objHistories.Add(objHistory)
                    End If
                Next

                Return objHistories
            Catch ex As gloDBException
                Return Nothing
            Catch ex As gloEMRActorsException
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException

                objex.ErrMessage = "Error while fetching History data."
                Throw objex
                Return Nothing

            Finally
                'SLR: Free dt
                If Not dt Is Nothing Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function
        Public Function GetHistory_CategoryWise() As String
            Dim strHistories As New System.Text.StringBuilder
            Dim _History As History = Nothing
            Try
                _Histories = GetHistory_CategoryWise(_intCurrentVisitID)
                If _Histories.Count > 0 Then

                    For i As Integer = 0 To _Histories.Count - 1
                        _History = _Histories.Item(i)
                        If i = 0 Then
                            If _History.Comments = "" Then
                                strHistories.Append(_History.HistoryItem)
                            Else
                                strHistories.Append(_History.HistoryItem)
                                'strHistories.Append(" --- ")
                                'strHistories.Append(_History.Comments)
                            End If
                        Else
                            strHistories.Append(vbCrLf)
                            If _History.Comments = "" Then
                                strHistories.Append(_History.HistoryItem)
                            Else
                                strHistories.Append(_History.HistoryItem)
                                'strHistories.Append(" --- ")
                                'strHistories.Append(_History.Comments)
                            End If
                        End If


                    Next

                End If
                If strHistories.ToString.Length = 0 Then
                    'strHistories.Append("No Allergies")
                    strHistories.Append("No Known Drug Allergies")
                End If

                Return strHistories.ToString
            Catch ex As PrescriptionException
                Return Nothing
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Populating Allergies."
                Throw objex
                Return Nothing
            Finally
                'Cleanup String builder object
                If IsNothing(strHistories) = False Then
                    strHistories = Nothing
                End If
            End Try
        End Function

        Public Function PrvRxInstringSearch(ByVal VisitId As Int64, ByVal PatientId As Int64) As DataTable
            Dim _gloEMRDatabase = New DataBaseLayer
            Dim odt As DataTable
            Dim strquery As String = ""
            Try
                If VisitId <> 0 Then

                    strquery = "SELECT     nPrescriptionID, nVisitID, nPatientID, (convert(varchar(30),dtPrescriptionDate,101) + space(1) + convert(varchar(30),dtPrescriptionDate,108)) as  dtPrescriptionDate, " _
                    & " isnull(sMedication, '') as 'Drug Name'," _
                    & " ISNULL(sDosage, '') AS 'Dosage', ISNULL(sRoute, '') AS 'Route', ISNULL(sFrequency, '') AS 'Frequency', ISNULL(sDuration, '') AS 'Duration', " _
                    & " ISNULL(sRoute, '') + ' ' + ISNULL(sFrequency, '') + ' ' + ISNULL(sDuration, '') AS 'Sig', ISNULL(sAmount, '') AS 'Amount', ISNULL(sRefills, '') AS 'Refills', " _
                    & " CASE bmaysubstitute WHEN 1 THEN 'No' WHEN 0 THEN 'Yes' END AS 'bmaysubstitute', sMedication,  " _
                    & "  dtStartDate, dtEndDate, ISNULL(sNotes, '') AS 'Notes', " _
                    & "  ISNULL(sMethod, '') AS 'Method', nDrugID, ISNULL(nProviderId, 0) AS ProviderId, ISNULL(sChiefComplaints, '') AS ChiefComplaints, " _
                    & " ISNULL(sNDCCode, '') AS NDCCode, ISNULL(nIsNarcotic, 0) AS IsNarcotic, ISNULL(mpid, 0) AS mpid, isnull(sDrugForm,'') as DrugForm, isnull(sStrengthUnit,'') as StrengthUnit, isnull(nPharmacyId,0) as PharmacyId, " _
                    & " isnull(sNCPDPID,'') as PhNCPDPID,isnull(nContactID,0) as PhContactID,isnull(sName,'') as PharmacyName,isnull(sAddressline1,'') as PhAddressline1,isnull(sAddressline2,'') as PhAddressline2,isnull(sCity,'') as PhCity, " _
                    & " isnull(sState,'') as PhState,isnull(sZip,'') as PhZip,isnull(sEmail,'') as PhEmail,isnull(sFax,'') as PhFax,isnull(sPhone,'') as PhPhone,isnull(sServiceLevel,'') as PhServiceLevel ,isnull(Prescription.sPrescriberNotes,'') as PrescriberNotes ,  " _
                    & " isnull(Prescription.eRxStatus,'') as eRxStatus," _
                    & " ISNULL(sMedication, '')  AS 'Drug' " _
                    & " FROM  Prescription  WHERE     nPatientID = " & PatientId & " AND nVisitID = " & VisitId & "  order by dtStartDate desc "
                Else

                    strquery = " SELECT     nPrescriptionID, nVisitID, nPatientID,(convert(varchar(30),dtPrescriptionDate,101) + space(1) + convert(varchar(30),dtPrescriptionDate,108)) as  dtPrescriptionDate, " _
                    & " isnull(sMedication, '') as 'Drug Name'," _
                    & " ISNULL(sDosage, '') AS 'Dosage',  ISNULL(sRoute, '') AS 'Route', " _
                    & " ISNULL(sFrequency, '') AS 'Frequency', ISNULL(sDuration, '') AS 'Duration', " _
                    & " ISNULL(sRoute, '')  + ' ' + ISNULL(sFrequency, '') + ' ' + ISNULL(sDuration, '') AS 'Sig', " _
                    & " ISNULL(sAmount, '') AS 'Amount', ISNULL(sRefills, '') AS 'Refills',  " _
                    & " CASE bmaysubstitute WHEN 1 THEN 'No' WHEN 0 THEN 'Yes' END AS 'bmaysubstitute', " _
                    & " sMedication, dtStartDate, dtEndDate, ISNULL(sNotes, '')  AS 'Notes', ISNULL(sMethod, '') AS 'Method', " _
                    & " nDrugID, ISNULL(nProviderId, 0) AS ProviderId, ISNULL(sChiefComplaints, '') AS ChiefComplaints,  " _
                    & " ISNULL(sNDCCode, '') AS NDCCode, ISNULL(nIsNarcotic, 0) AS IsNarcotic, " _
                    & " ISNULL(mpid, 0) AS mpid, isnull(sDrugForm,'') as DrugForm, isnull(sStrengthUnit,'') as StrengthUnit, isnull(nPharmacyId,0) as PharmacyId, " _
                    & " isnull(sNCPDPID,'') as PhNCPDPID,isnull(nContactID,0) as PhContactID,isnull(sName,'') as PharmacyName, " _
                    & " isnull(sAddressline1,'') as PhAddressline1,isnull(sAddressline2,'') as PhAddressline2, " _
                    & " isnull(sCity,'') as PhCity,   isnull(sState,'') as PhState,isnull(sZip,'') as PhZip,isnull(sEmail,'') as PhEmail, " _
                    & " isnull(sFax,'') as PhFax,isnull(sPhone,'') as PhPhone,isnull(sServiceLevel,'') as PhServiceLevel , " _
                    & " isnull(Prescription.sPrescriberNotes,'') as PrescriberNotes ,  isnull(Prescription.eRxStatus,'') as eRxStatus , " _
                    & " ISNULL(sMedication, '')  AS 'Drug' " _
                    & " FROM Prescription  WHERE nPatientID = " & PatientId & " order by dtStartDate desc "

                End If

                odt = _gloEMRDatabase.GetDataTable_Query(strquery)
                Return odt
            Catch ex As gloDBException
                Throw ex
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Prescription for viewing."
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function getServerTime() As DateTime
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "Select dbo.gloGetDate()"
                Dim _ServerDateTime As DateTime = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsNothing(_ServerDateTime) Then
                    Return Nothing
                Else
                    Return _ServerDateTime
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving server time"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function RetrieveRepresentative_NDC(ByVal sNDCCode As String, ByVal mpid As Int32) As String

            Dim _gloEMRDatabase = New DataBaseLayer
            Dim objParameter As DBParameter
            Dim odt As DataTable = Nothing
            Dim Ndc As String = ""
            Try
                objParameter = New DBParameter
                objParameter.Value = mpid
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@mpid"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = sNDCCode
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@NDCCode"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                odt = _gloEMRDatabase.GetDataTable("gsp_GetRepresentativeNDC")
                If Not IsNothing(odt) Then
                    If odt.Rows.Count > 0 Then
                        Ndc = Convert.ToString(odt.Rows(0)("sNDCCode"))
                    End If
                End If

            Catch ex As gloDBException
                Throw ex
                Return Nothing
            Catch ex As gloEMRActorsException
                Throw ex
                Return Nothing
            Catch ex As PrescriptionException
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error while fetching Ndccode."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(odt) Then
                    odt.Dispose()
                    odt = Nothing
                End If
                _gloEMRDatabase.Dispose()
            End Try
            Return Ndc
        End Function

        Public Function RetrieveDrugID_FROM_NDC(ByVal sNDCCode As String) As Int64
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "select isnull(nDrugsID,0) from Drugs_mst where sNDCCode='" & sNDCCode & "'"
                Dim drugid As Int64 = _gloEMRDatabase.GetDataValue(strquery, False)

                If IsDBNull(drugid) Then
                    Return 0
                Else
                    Return drugid
                End If
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Retrieving Drug ID from NDC"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function GetLatestMedication() As Collection
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim MedicationCol As New Collection
            Dim dt As DataTable = Nothing  'SLR: new is not needdd
            Try

                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID 'globalPatient.gnPatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = 0
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@VisitId"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = Now.Date
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtsystemdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing


                dt = _gloEMRDatabase.GetDataTable("gsp_GetLatestMedications")

                Dim i As Int16
                For i = 0 To dt.Rows.Count - 1
                    MedicationCol.Add(dt.Rows(i)(0))
                Next
                _Medications = MedicationCol
                Return _Medications
            Catch ex As Exception
                Dim objex As New PrescriptionException
                objex.ErrMessage = "Error Fetching Current Medications for Patient"
                Throw objex
            Finally
                If (IsNothing(dt) = False) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function

        Public Function GetPatientProviderDetails(ByVal nProviderID As Int64) As DataTable
            Dim oDB As DataBaseLayer = Nothing
            Dim oParameter As DBParameter = Nothing
            Dim dtprovider As DataTable = Nothing
            Try
                oDB = New DataBaseLayer
                oParameter = New DBParameter

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.BigInt
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@nProviderID"
                oParameter.Value = nProviderID
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                dtprovider = oDB.GetDataTable("gsp_GetPatientProviderDetails")
                Return dtprovider
            Catch ex As SqlException
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                    oParameter = Nothing
                End If
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
            End Try
        End Function

        Public Function GetPharmacyDetails(ByVal _Pharmacyid As Int64) As DataTable
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strSQl = "select nContactID, isnull(sName,'') as sName, isnull(sContact,'') as sContact, isnull(sStreet,'') as sStreet,  isnull(sCity,'') as sCity, isnull(sState,'') as sState, isnull(sZIP,'') as sZIP, " _
                            & " isnull(sPhone,'') as sPhone, isnull(sFax,'') as sFax, isnull(sEmail,'') as sEmail, isnull(sServiceLevel,'') as sServiceLevel, isnull(sAddressLine1,'') as sAddressLine1, isnull(sAddressLine2,'') as sAddressLine2, " _
                            & " isnull(sNCPDPID,'') as sNCPDPID, isnull(sPharmacyNPI,'') as sNPI from Contacts_MST where sContactType = 'Pharmacy' and nContactID = " & _Pharmacyid.ToString()

                Dim dtPharmacydetails As DataTable = ogloEMRDatabase.GetDataTable_Query(_strSQl)
                Return dtPharmacydetails
            Catch ex As Exception
                Dim objex As New PrescriptionException
                Throw objex
            Finally
                If Not IsNothing(ogloEMRDatabase) Then
                    ogloEMRDatabase.Dispose()
                    ogloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#Region "RXDBlayer in EMR Classes"

        Private Dv As DataView

        Public Function FillControls(ByVal id As Long, Optional ByVal strsearch As String = "") As DataTable
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable
            Dim Cmd As SqlCommand = Nothing
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                If id = 0 Then
                    Cmd = New SqlCommand("gsp_FillSig_Mst", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    adpt.SelectCommand = Cmd
                Else
                    Cmd = New SqlCommand("gsp_FillDrugs_Mst", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure
                    adpt.SelectCommand = Cmd

                    Dim objParam As SqlParameter

                    objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = LCase(strsearch)


                    objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = id

                    objParam = Cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = _PatientID

                    If id = 3 Then
                        objParam = Cmd.Parameters.Add("@dtSysDate", SqlDbType.DateTime)
                        objParam.Direction = ParameterDirection.Input
                        objParam.Value = Format(Now.Date, "MM/dd/yyyy")
                    End If

                    objParam = Nothing
                End If
                adpt.Fill(dt)
                If (IsNothing(Dv) = False) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                Dv = dt.DefaultView
                Conn.Close()
                Return dt

                'Dim dreader As SqlDataReader
                'Conn.Open()
                'dreader = Cmd.ExecuteReader()

                'Do While dreader.Read
                '    Dim i As Integer
                '    i = dreader("nSpecialtyID")

                'Loop
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
                Return Nothing
            Finally
                If Not IsNothing(Cmd) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                If Not IsNothing(adpt) Then
                    adpt.Dispose()
                    adpt = Nothing
                End If
            End Try
        End Function

        Public Function Fill_LockImplantDevices(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
            Dim Cmd As SqlCommand = Nothing
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = MachinName

                objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = TransactionType

                objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 0

                sqladpt.SelectCommand = Cmd

                sqladpt.Fill(dt)

                Conn.Close()
                objParam = Nothing
                Return dt
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Conn.Close()
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                If Not IsNothing(Cmd) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                If Not IsNothing(sqladpt) Then
                    sqladpt.Dispose()
                    sqladpt = Nothing
                End If
            End Try
        End Function

        Public Function Fill_LockPrescription(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
            Dim Cmd As SqlCommand = Nothing
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = MachinName

                objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = TransactionType

                objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 0

                sqladpt.SelectCommand = Cmd

                sqladpt.Fill(dt)

                Conn.Close()
                objParam = Nothing
                Return dt
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Conn.Close()
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                If Not IsNothing(Cmd) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                If Not IsNothing(sqladpt) Then
                    sqladpt.Dispose()
                    sqladpt = Nothing
                End If
            End Try
        End Function

        Public Sub SaveRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime, ByVal nJuniorProviderID As Int64, ByVal dtProviderAssociation As DataTable)
            Dim oTransaction As SqlTransaction
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            If Conn.State = ConnectionState.Closed Then Conn.Open()

            oTransaction = Conn.BeginTransaction()
            Dim cmd As SqlCommand = Nothing
            Try

                If dtProviderAssociation IsNot Nothing Then
                    If dtProviderAssociation.Rows.Count > 0 Then
                        Dim _Query As String


                        _Query = "DELETE FROM Rx_ProviderAssociation WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " AND " _
                        & " CONVERT(VARCHAR,dtPrescriptionDate,101) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),101) AND " _
                        & " CONVERT(VARCHAR,dtPrescriptionDate,108) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),108) AND " _
                        & " nJuniorProviderID = " & nJuniorProviderID & ""
                        cmd = New SqlCommand
                        cmd.Connection = Conn
                        cmd.Transaction = oTransaction
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = _Query
                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = Conn
                        cmd.Transaction = oTransaction
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "gsp_InsertRxProviderAssociation"

                        For i As Integer = 0 To dtProviderAssociation.Rows.Count - 1
                            cmd.Parameters.Clear()

                            cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@dtPrescriptionDate", SqlDbType.DateTime)
                            cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@sProviderName", SqlDbType.VarChar)
                            cmd.Parameters.Add("@sDEA", SqlDbType.VarChar)
                            cmd.Parameters.Add("@nJuniorProviderID", SqlDbType.BigInt)

                            cmd.Parameters("@nPatientID").Value = nPatientID
                            cmd.Parameters("@nVisitID").Value = nVisitID
                            cmd.Parameters("@dtPrescriptionDate").Value = dtPrescriptionDate

                            cmd.Parameters("@nProviderID").Value = Convert.ToString(dtProviderAssociation.Rows(i)("nProviderID"))
                            cmd.Parameters("@sProviderName").Value = Convert.ToString(dtProviderAssociation.Rows(i)("sDescription"))
                            cmd.Parameters("@sDEA").Value = Convert.ToString(dtProviderAssociation.Rows(i)("sDEA"))

                            cmd.Parameters("@nJuniorProviderID").Value = nJuniorProviderID

                            cmd.ExecuteNonQuery()
                        Next

                        oTransaction.Commit()

                    End If
                End If

            Catch ex As Exception
                oTransaction.Rollback()
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If Not IsNothing(oTransaction) Then
                    oTransaction.Dispose()
                    oTransaction = Nothing
                End If
            End Try
        End Sub

        Public Sub SaveRxProviderAssociation(ByVal nJuniorProviderID As Int64, ByVal dtProviderAssociation As DataTable)
            Dim oTransaction As SqlTransaction
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            If Conn.State = ConnectionState.Closed Then Conn.Open()

            oTransaction = Conn.BeginTransaction()
            Dim cmd As SqlCommand = Nothing
            Try

                If dtProviderAssociation IsNot Nothing Then
                    If dtProviderAssociation.Rows.Count > 0 Then


                        Dim _Query As String


                        _Query = "DELETE FROM Rx_ProviderAssociation WHERE nPatientID = 0 AND nVisitID = 0 AND nJuniorProviderID = " & nJuniorProviderID & ""
                        cmd = New SqlCommand
                        cmd.Connection = Conn
                        cmd.Transaction = oTransaction
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = _Query
                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = Conn
                        cmd.Transaction = oTransaction
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "gsp_InsertRxProviderAssociation"

                        For i As Integer = 0 To dtProviderAssociation.Rows.Count - 1
                            cmd.Parameters.Clear()

                            cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@dtPrescriptionDate", SqlDbType.DateTime)
                            cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                            cmd.Parameters.Add("@sProviderName", SqlDbType.VarChar)
                            cmd.Parameters.Add("@sDEA", SqlDbType.VarChar)
                            cmd.Parameters.Add("@nJuniorProviderID", SqlDbType.BigInt)
                            cmd.Parameters("@nPatientID").Value = 0
                            cmd.Parameters("@nVisitID").Value = 0
                            cmd.Parameters("@dtPrescriptionDate").Value = Now
                            cmd.Parameters("@nProviderID").Value = Convert.ToString(dtProviderAssociation.Rows(i)("nProviderID"))
                            cmd.Parameters("@sProviderName").Value = Convert.ToString(dtProviderAssociation.Rows(i)("sDescription"))
                            cmd.Parameters("@sDEA").Value = Convert.ToString(dtProviderAssociation.Rows(i)("sDEA"))
                            cmd.Parameters("@nJuniorProviderID").Value = nJuniorProviderID

                            cmd.ExecuteNonQuery()
                        Next

                        oTransaction.Commit()

                    End If
                End If

            Catch ex As Exception
                oTransaction.Rollback()
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If Not IsNothing(cmd) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                If (IsNothing(oTransaction) = False) Then
                    oTransaction.Dispose()
                    oTransaction = Nothing
                End If
            End Try
        End Sub

        Public Sub SaveRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime, ByVal nJuniorProviderID As Int64)
            Dim Cmd As SqlCommand = Nothing
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                Dim query As String
                query = " INSERT INTO Rx_ProviderAssociation " _
                      & " SELECT " & nPatientID & ", " & nVisitID & ", '" & dtPrescriptionDate & "', nProviderID, sProviderName, sDEA, nJuniorProviderID " _
                      & " FROM Rx_ProviderAssociation WHERE nJuniorProviderID = " & nJuniorProviderID & " AND nPatientID = 0 AND nVisitID = 0 "
                Cmd = New SqlCommand(query, Conn)
                If Conn.State = ConnectionState.Closed Then Conn.Open()
                Cmd.ExecuteNonQuery()
                Conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If Not IsNothing(Cmd) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
            End Try
        End Sub

        Public Shared Function GetRxProviderAssociation(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime) As DataTable
            Dim adp As SqlDataAdapter = Nothing
            Dim dtAssociation As DataTable = Nothing
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)

            Try

                Dim _query As String = " SELECT nProviderID, sProviderName, sDEA FROM Rx_ProviderAssociation WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " AND " _
                & " CONVERT(VARCHAR,dtPrescriptionDate,101) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),101) AND " _
                & " CONVERT(VARCHAR,dtPrescriptionDate,108) = CONVERT(VARCHAR,CONVERT(DATETIME,'" & dtPrescriptionDate & "'),108)"
                adp = New SqlDataAdapter(_query, Conn)
                dtAssociation = New DataTable
                adp.Fill(dtAssociation)
                If dtAssociation IsNot Nothing Then
                    Return dtAssociation
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                'If Not IsNothing(dtAssociation) Then
                '    dtAssociation.Dispose()
                '    dtAssociation = Nothing
                'End If
                If Not IsNothing(adp) Then
                    adp.Dispose()
                    adp = Nothing
                End If
            End Try
        End Function

        Public Shared Function GetRxProviderAssociation(ByVal nJuniorProviderID As Int64) As DataTable
            Dim adp As SqlDataAdapter = Nothing
            Dim dtAssociation As DataTable = Nothing
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                Dim _query As String = " SELECT nProviderID, sProviderName, sDEA FROM Rx_ProviderAssociation WHERE nPatientID = 0 AND nVisitID = 0 AND nJuniorProviderID = " & nJuniorProviderID & ""
                adp = New SqlDataAdapter(_query, Conn)
                dtAssociation = New DataTable
                adp.Fill(dtAssociation)
                If dtAssociation IsNot Nothing Then
                    Return dtAssociation
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Nothing
            Finally
                'If Not IsNothing(dtAssociation) Then
                '    dtAssociation.Dispose()
                '    dtAssociation = Nothing
                'End If
                If Not IsNothing(adp) Then
                    adp.Dispose()
                    adp = Nothing
                End If
            End Try
        End Function
        Public Function SaveOPIDAgreement(ByVal npatientID As Long, ByVal signflag As Boolean, ByVal dtSignedDate As DateTime, ByVal SignedComments As String, ByVal AgreementID As Long) As Integer
            Dim con As SqlConnection = Nothing
            Dim cmdVisits As SqlCommand = Nothing
            Dim objParam As SqlParameter = Nothing
            Dim objFlagParam As SqlParameter = Nothing



            Try

                con = New SqlClient.SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
                cmdVisits = New SqlCommand("SP_InUp_OPID_SignedAgreement", con)
                cmdVisits.CommandType = CommandType.StoredProcedure

                objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = npatientID

                objParam = cmdVisits.Parameters.Add("@IsSignedAgreement", SqlDbType.Bit)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = signflag

                objParam = cmdVisits.Parameters.Add("@dtSignedDate", SqlDbType.DateTime)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = dtSignedDate

                objParam = cmdVisits.Parameters.Add("@sInternalnotes", SqlDbType.VarChar)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = SignedComments

                objParam = cmdVisits.Parameters.Add("@nOpioid", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.InputOutput
                objParam.Value = AgreementID

                con.Open()
                cmdVisits.ExecuteNonQuery()
                AgreementID = objParam.Value
                con.Close()



                objFlagParam = Nothing

                cmdVisits.Parameters.Clear()




                Return AgreementID
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(con) Then
                    con.Dispose()
                    con = Nothing

                End If
                If Not IsNothing(cmdVisits) Then
                    cmdVisits.Dispose()
                    cmdVisits = Nothing
                End If



            End Try
        End Function
        Public Function getOPIDAgreement() As DataSet
            Dim con As SqlConnection = Nothing
            Dim cmdVisits As SqlCommand = Nothing
            Dim objParam As SqlParameter = Nothing
            Dim objFlagParam As SqlParameter = Nothing
            Dim icnt As Integer = 0


            Try

                con = New SqlClient.SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
                cmdVisits = New SqlCommand("gsp_getOpioidTreatmentAgreement", con)
                cmdVisits.CommandType = CommandType.StoredProcedure

                objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = _PatientID
                con.Open()

                Dim dt As DataTable = New DataTable
                Dim ds As DataSet = New DataSet

                Dim d As SqlDataAdapter = New SqlDataAdapter(cmdVisits)
                d.Fill(ds)
                If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "AgreementVerified"
                    ds.Tables(1).TableName = "AgreementScanned"
                End If

                objFlagParam = Nothing

                cmdVisits.Parameters.Clear()



                Return ds
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                If Not IsNothing(con) Then
                    con.Dispose()
                    con = Nothing

                End If
                If Not IsNothing(cmdVisits) Then
                    cmdVisits.Dispose()
                    cmdVisits = Nothing
                End If



            End Try
        End Function

        Public Shared Function GetRxProviderAssociationSettings() As Boolean
            Dim Cmd As SqlCommand = Nothing
            Dim Conn As New SqlConnection(gloEMRDatabase.DataBaseLayer.ConnectionString)
            Try
                Dim query As String = " select ISNULL(sSettingsValue,'') AS sSettingsValue from settings " _
                    & " where sSettingsName='MULTIPLE SUPERVISORS FOR PAPER RX'"
                Cmd = New SqlCommand(query, Conn)
                If Conn.State = ConnectionState.Closed Then Conn.Open()
                Dim oResult As Object = Cmd.ExecuteScalar
                Conn.Close()
                If oResult IsNot Nothing Then
                    '' gblnMultipleSupervisorsforPaperRx = CType(oResult, Boolean)
                    Return CType(oResult, Boolean)
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            Finally
                If Not IsNothing(Cmd) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
            End Try
        End Function

#End Region


        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                End If

                ' TODO: free shared unmanaged resources
            End If
            'SLR: Free ArrayListNDC, _C1RxFormularyGrid, _C1RxFormularyIndicatorGrid, strbldFormularyAlternativeGrid_Temp, strbldFormularyRichtext_Temp
            'SLR: also check what is in disposing here and viceversa
            If Not ArryLstNDC Is Nothing Then
                ArryLstNDC = Nothing
            End If
            If Not _C1RxFormularyGrid Is Nothing Then
                _C1RxFormularyGrid.Dispose()
                _C1RxFormularyGrid = Nothing
            End If
            If Not _C1RxFormularyIndicatorGrid Is Nothing Then
                _C1RxFormularyIndicatorGrid.Dispose()
                _C1RxFormularyIndicatorGrid = Nothing
            End If
            If Not strbldFormularyAlternativeGrid_Temp Is Nothing Then
                strbldFormularyAlternativeGrid_Temp = Nothing
            End If
            If Not strbldFormularyRichtext_Temp Is Nothing Then
                strbldFormularyRichtext_Temp = Nothing
            End If
            If Not IsNothing(_RxHubFormulary) Then
                _RxHubFormulary.Dispose()
                _RxHubFormulary = Nothing
            End If
            If Not IsNothing(_Histories) Then
                _Histories.Dispose()
                _Histories = Nothing
            End If
            If Not IsNothing(_Formularys) Then
                _Formularys.Dispose()
                _Formularys = Nothing
            End If
            If Not IsNothing(_Prescriptions) Then
                _Prescriptions.Clear()
                _Prescriptions.Dispose()
                _Prescriptions = Nothing
            End If
            If Not IsNothing(_tmpCheckStates) Then
                _tmpCheckStates.Dispose()
                _tmpCheckStates = Nothing
            End If
            If Not IsNothing(_Prescriptions_SelectedPBM) Then
                _Prescriptions_SelectedPBM.Dispose()
                _Prescriptions_SelectedPBM = Nothing
            End If
            If Not IsNothing(_Medications) Then
                _Medications.Clear()
                _Medications = Nothing
            End If
            If Not IsNothing(oOldgloPrescription) Then
                If Not IsNothing(oOldgloPrescription.DrugsCol) Then
                    oOldgloPrescription.DrugsCol.Clear()
                    oOldgloPrescription.DrugsCol.Dispose()
                End If
                oOldgloPrescription.Dispose()
                oOldgloPrescription = Nothing
            End If
            If Not IsNothing(dtMedicationData) Then
                dtMedicationData.Dispose()
                dtMedicationData = Nothing
            End If
            DisposeFonts()
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support 32"
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Private Sub ogloInterface_MessageInvalidated() Handles ogloInterface.MessageInvalidated
            MessageInValid = True
            RaiseEvent SurescriptMessageInvalidated()
        End Sub

        Private Function GetResponseString(enumResponseType As SentMessageType) As String
            Dim sReturned As String = ""
            Try
                Select Case enumResponseType
                    Case SentMessageType.Cancel
                        sReturned = "Cancel "
                    Case SentMessageType.eApproved
                        sReturned = "Approved "
                    Case SentMessageType.eApprovedWithChanges
                        sReturned = "Approved With Changes "
                    Case SentMessageType.eDenied
                        sReturned = "Denied "
                    Case SentMessageType.eDeniedWithNewRxToFollow
                        sReturned = "Denied With New RX to follow "
                    Case SentMessageType.eNewRx
                        sReturned = "New Rx "
                End Select
            Catch ex As Exception

            End Try
            Return sReturned
        End Function

    End Class

    Public Class PrescriptionException
        Inherits ApplicationException
        Private _ErrMessage As String
        Private _ErrCode As String
        Public Property ErrMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal Value As String)
                _ErrMessage = Value
            End Set
        End Property

        Public Property ErrCode() As String
            Get
                Return _ErrCode
            End Get
            Set(ByVal Value As String)
                _ErrCode = Value
            End Set
        End Property

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal errmessage As String)
            MyBase.New(errmessage)
        End Sub
    End Class



End Namespace 'end of namespace
