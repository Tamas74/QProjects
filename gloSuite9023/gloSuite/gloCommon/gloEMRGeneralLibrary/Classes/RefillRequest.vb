Imports gloSureScript
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRDatabase

Namespace gloEMRPrescription
    Public Class RefillRequest
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls
        Public ogloPrescription As EPrescription

        ' IDisposable



        'variable for Patient Information property procedures
        Private mPatientName As String
        Private mPatientDOB As String
        Private mPatientGender As String
        Private mPatientAddress As String
        Private mPatientAddress2 As String
        Private mPatientPhone As String
        Private mPatientCity As String
        Private mPatientState As String
        Private mPatientZip As String

        'variable for Pharmacy Information property procedures
        Private mPharmacyID As String
        Private mPharmacyName As String
        Private mPharmacyAddress As String
        Private mPharmacyAddress2 As String
        Private mPharmacyPhone As String
        Private mPharmacyFax As String
        Private mPharmacyCity As String
        Private mPharmacyState As String
        Private mPharmacyZip As String
        Private mPharmacyNCPDPID As String
        Private mPharmacyNPI As String

        'variable for Provider (means Prescriber)Information property procedures
        Private mProviderID As String
        Private mProviderName As String
        Private mProviderFirstName As String
        Private mProviderLastName As String
        Private mProdcode As String = ""
        Private mProdQualifier As String = ""
        Private mProviderAddress As String
        Private mProviderAddress2 As String
        Private mProviderPhone As String
        Private mProviderFax As String
        Private mProviderCity As String
        Private mProviderState As String
        Private mProviderZip As String
        Private mPrescriberNPI As String
        Private mPrescriberSSN As String
        Private mPrescriberDEA As String

        Private mFileData As Byte() = Nothing

        Private mdtLastdate As String
        Private mMDdtLastdate As String
        Private mDuration As String
        Private mMDDuration As String

        '****************Patient Information property procedures**********************************
        Public Property PatientName() As String
            Get
                Return mPatientName
            End Get
            Set(ByVal value As String)
                mPatientName = value
            End Set
        End Property


        Public Property PatientDOB() As String
            Get
                Return mPatientDOB
            End Get
            Set(ByVal value As String)
                mPatientDOB = value
            End Set
        End Property


        Public Property PatientGender() As String
            Get
                Return mPatientGender
            End Get
            Set(ByVal value As String)
                mPatientGender = value
            End Set
        End Property


        Public Property PatientAddress() As String
            Get
                Return mPatientAddress
            End Get
            Set(ByVal value As String)
                mPatientAddress = value
            End Set
        End Property

        Public Property PatientAddress2() As String
            Get
                Return mPatientAddress2
            End Get
            Set(ByVal value As String)
                mPatientAddress2 = value
            End Set
        End Property

        Public Property PatientPhone() As String
            Get
                Return mPatientPhone
            End Get
            Set(ByVal value As String)
                mPatientPhone = value
            End Set
        End Property


        Public Property PatientCity() As String
            Get
                Return mPatientCity
            End Get
            Set(ByVal value As String)
                mPatientCity = value
            End Set
        End Property


        Public Property PatientState() As String
            Get
                Return mPatientState
            End Get
            Set(ByVal value As String)
                mPatientState = value
            End Set
        End Property


        Public Property PatientZip() As String
            Get
                Return mPatientZip
            End Get
            Set(ByVal value As String)
                mPatientZip = value
            End Set
        End Property
        '***********Patient Information property procedures***************************************

        '****************Pharmacy Information property procedures**********************************
        Public Property PharmacyID() As String
            Get
                Return mPharmacyID
            End Get
            Set(ByVal value As String)
                mPharmacyID = value
            End Set
        End Property


        Public Property PharmacyName() As String
            Get
                Return mPharmacyName
            End Get
            Set(ByVal value As String)
                mPharmacyName = value
            End Set
        End Property


        Public Property PharmacyAddress() As String
            Get
                Return mPharmacyAddress
            End Get
            Set(ByVal value As String)
                mPharmacyAddress = value
            End Set
        End Property

        Public Property PharmacyAddress2() As String
            Get
                Return mPharmacyAddress2
            End Get
            Set(ByVal value As String)
                mPharmacyAddress2 = value
            End Set
        End Property


        Public Property PharmacyPhone() As String
            Get
                Return mPharmacyPhone
            End Get
            Set(ByVal value As String)
                mPharmacyPhone = value
            End Set
        End Property
        Public Property PharmacyFax() As String
            Get
                Return mPharmacyFax
            End Get
            Set(ByVal value As String)
                mPharmacyFax = value
            End Set
        End Property

        Public Property PharmacyCity() As String
            Get
                Return mPharmacyCity
            End Get
            Set(ByVal value As String)
                mPharmacyCity = value
            End Set
        End Property


        Public Property PharmacyState() As String
            Get
                Return mPharmacyState
            End Get
            Set(ByVal value As String)
                mPharmacyState = value
            End Set
        End Property


        Public Property PharmacyZip() As String
            Get
                Return mPharmacyZip
            End Get
            Set(ByVal value As String)
                mPharmacyZip = value
            End Set
        End Property

        Public Property PharmacyNCPDPID() As String
            Get
                Return mPharmacyNCPDPID
            End Get
            Set(ByVal value As String)
                mPharmacyNCPDPID = value
            End Set
        End Property
        Public Property PharmacyNPI() As String
            Get
                Return mPharmacyNPI
            End Get
            Set(ByVal value As String)
                mPharmacyNPI = value
            End Set
        End Property

        '****************Pharmacy Information property procedures**********************************

        '****************Provider (means Prescriber) Information property procedures**********************************
        Public Property ProviderID() As String
            Get
                Return mProviderID
            End Get
            Set(ByVal value As String)
                mProviderID = value
            End Set

        End Property

        Public Property ProviderName() As String
            Get
                Return mProviderName
            End Get
            Set(ByVal value As String)
                mProviderName = value
            End Set
        End Property
        Public Property ProviderFirstName() As String
            Get
                Return mProviderFirstName
            End Get
            Set(ByVal value As String)
                mProviderFirstName = value
            End Set
        End Property
        Public Property ProviderLastName() As String
            Get
                Return mProviderLastName
            End Get
            Set(ByVal value As String)
                mProviderLastName = value
            End Set
        End Property

        Public Property ProviderAddress() As String
            Get
                Return mProviderAddress
            End Get
            Set(ByVal value As String)
                mProviderAddress = value
            End Set
        End Property
        Public Property ProviderAddress2() As String
            Get
                Return mProviderAddress2
            End Get
            Set(ByVal value As String)
                mProviderAddress2 = value
            End Set
        End Property

        Public Property ProviderPhone() As String
            Get
                Return mProviderPhone
            End Get
            Set(ByVal value As String)
                mProviderPhone = value
            End Set
        End Property
        Public Property ProviderFax() As String
            Get
                Return mProviderFax
            End Get
            Set(ByVal value As String)
                mProviderFax = value
            End Set
        End Property

        Public Property ProviderCity() As String
            Get
                Return mProviderCity
            End Get
            Set(ByVal value As String)
                mProviderCity = value
            End Set
        End Property


        Public Property ProviderState() As String
            Get
                Return mProviderState
            End Get
            Set(ByVal value As String)
                mProviderState = value
            End Set
        End Property


        Public Property ProviderZip() As String
            Get
                Return mProviderZip
            End Get
            Set(ByVal value As String)
                mProviderZip = value
            End Set
        End Property
        Public Property PrescriberNPI() As String
            Get
                Return mPrescriberNPI
            End Get
            Set(ByVal value As String)
                mPrescriberNPI = value
            End Set
        End Property
        Public Property PrescriberSSN() As String
            Get
                Return mPrescriberSSN
            End Get
            Set(ByVal value As String)
                mPrescriberSSN = value
            End Set
        End Property
        Public Property PrescriberDEA() As String
            Get
                Return mPrescriberDEA
            End Get
            Set(ByVal value As String)
                mPrescriberDEA = value
            End Set
        End Property
        '**** 
        Public Property Duration() As String
            Get
                Return mDuration
            End Get
            Set(ByVal value As String)
                mDuration = value
            End Set
        End Property


        Public Property MDDuration() As String
            Get
                Return mMDDuration
            End Get
            Set(ByVal value As String)
                mMDDuration = value
            End Set
        End Property

        Public Property DTlastdate() As String
            Get
                Return mdtLastdate
            End Get
            Set(ByVal value As String)
                mdtLastdate = value
            End Set
        End Property
        Public Property MDDTlastdate() As String
            Get
                Return mMDdtLastdate
            End Get
            Set(ByVal value As String)
                mMDdtLastdate = value
            End Set
        End Property
        Public Property FileData() As Byte()
            Get
                Return mFileData
            End Get
            Set(ByVal value As Byte())
                mFileData = value
            End Set
        End Property

        '****************Provider (means Prescriber) Information property procedures**********************************

        Public Sub mydispose()
            If Not IsNothing(ogloPrescription) Then
                ogloPrescription.Dispose()
                ogloPrescription = Nothing
            End If
        End Sub
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
        'Get All Pending RefillRequests
        Public Function GetAllPendingRefills(ByVal ProviderId As String) As DataTable
            Dim ogloInterface As New gloSureScriptInterface
            Dim dt As DataTable = Nothing
            Try

                dt = ogloInterface.GetPendingRefills(ProviderId)
                Return dt
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return Nothing
            Catch ex As Exception
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                Throw obj
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
        End Function
        'Get List of all Prescribers
        Public Function GetPrescriberList() As DataTable
            Dim osurescriptdblayer As New gloSureScriptDBLayer
            Dim dt As DataTable = Nothing
            Try
                dt = osurescriptdblayer.GetPrescriberList
                Return dt
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(osurescriptdblayer) Then
                    osurescriptdblayer.Dispose()
                    osurescriptdblayer = Nothing
                End If
            End Try
        End Function

        Public Sub New(Optional ByVal SQLUserNameEMR As String = "", Optional ByVal SQLPasswordEMR As String = "")
            gloSurescriptGeneral.ServerName = globalSecurity.gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = globalSecurity.gstrDatabaseName
            If SQLUserNameEMR <> "" Then
                gloSurescriptGeneral.sUserName = SQLUserNameEMR
                gloSurescriptGeneral.sPassword = SQLPasswordEMR
            End If
            gloSurescriptGeneral.RootPath = clsgeneral.StartUpPath
            gloSurescriptGeneral.blnIsStagingServer = clsgeneral.gblnIsStagingServer
        End Sub
        'Get The drug details of the Prescription item to Refill
        Public Sub GetDrugDetailsToRefill(ByVal MessageId As String, Optional ByVal Rxreferencenumber As String = Nothing, Optional ByVal RxTransactionID As String = Nothing)
            Dim ogloInterface As gloSureScriptInterface
            ogloInterface = New gloSureScriptInterface
            Try
                ogloPrescription = ogloInterface.GetDrugsToRefill(MessageId, Rxreferencenumber, RxTransactionID)

            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Catch ex As Exception
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try

        End Sub
        Public Sub SetRefillRequestData()
            Try
                'get data from ogloPrescription and set it to Property procedures
                If Not IsNothing(ogloPrescription) Then

                    'assign values to patient information property proc
                    mPatientName = ogloPrescription.RxPatient.PatientName.FirstName & " " & ogloPrescription.RxPatient.PatientName.LastName
                    mPatientDOB = ogloPrescription.RxPatient.DateofBirth
                    mPatientGender = ogloPrescription.RxPatient.Gender
                    mPatientAddress = ogloPrescription.RxPatient.PatientAddress.Address1
                    mPatientAddress2 = ogloPrescription.RxPatient.PatientAddress.Address2
                    mPatientPhone = ogloPrescription.RxPatient.PatientPhone.Phone
                    mPatientCity = ogloPrescription.RxPatient.PatientAddress.City
                    mPatientState = ogloPrescription.RxPatient.PatientAddress.State
                    mPatientZip = ogloPrescription.RxPatient.PatientAddress.Zip


                    'assign values to Pharmacy information property proc
                    mPharmacyName = ogloPrescription.RxPharmacy.PharmacyName
                    mPharmacyAddress = ogloPrescription.RxPharmacy.PharmacyAddress.Address1
                    mPharmacyAddress2 = ogloPrescription.RxPharmacy.PharmacyAddress.Address2
                    mPharmacyPhone = ogloPrescription.RxPharmacy.PharmacyPhone.Phone
                    mPharmacyFax = ogloPrescription.RxPharmacy.PharmacyPhone.Fax
                    mPharmacyCity = ogloPrescription.RxPharmacy.PharmacyAddress.City
                    mPharmacyState = ogloPrescription.RxPharmacy.PharmacyAddress.State
                    mPharmacyZip = ogloPrescription.RxPharmacy.PharmacyAddress.Zip
                    mPharmacyNPI = ogloPrescription.RxPharmacy.PharmacyNPI



                    'assign values to Provider (means Prescriber) information property proc
                    mProviderName = ogloPrescription.RxPrescriber.PrescriberName.FirstName & " " & ogloPrescription.RxPrescriber.PrescriberName.LastName
                    mProviderFirstName = ogloPrescription.RxPrescriber.PrescriberName.FirstName
                    mProviderLastName = ogloPrescription.RxPrescriber.PrescriberName.LastName
                    mProviderAddress = ogloPrescription.RxPrescriber.PrescriberAddress.Address1
                    mProviderAddress2 = ogloPrescription.RxPrescriber.PrescriberAddress.Address2
                    mProviderPhone = ogloPrescription.RxPrescriber.PrescriberPhone.Phone
                    mProviderFax = ogloPrescription.RxPrescriber.PrescriberPhone.Fax
                    mProviderCity = ogloPrescription.RxPrescriber.PrescriberAddress.City
                    mProviderState = ogloPrescription.RxPrescriber.PrescriberAddress.State
                    mProviderZip = ogloPrescription.RxPrescriber.PrescriberAddress.Zip
                    mPrescriberNPI = ogloPrescription.RxPrescriber.PrescriberNPI
                    mPrescriberSSN = ogloPrescription.RxPrescriber.PrescriberSSN
                    mPrescriberDEA = ogloPrescription.RxPrescriber.PrescriberDEA

                    mFileData = ogloPrescription.FileData

                    If ogloPrescription.DrugsCol.Count() > 0 Then
                        mDuration = ogloPrescription.DrugsCol.Item(0).DrugDuration
                        mMDDuration = ogloPrescription.DrugsCol.Item(0).MDDuration
                        mdtLastdate = ogloPrescription.DrugsCol.Item(0).LastfillDate
                        mMDdtLastdate = ogloPrescription.DrugsCol.Item(0).MDdtlastdate
                    End If
                End If
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Catch ex As Exception
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                Throw obj
            Finally

            End Try
        End Sub




        ''' <summary>
        ''' 'this will update the status of is alert flag when the menu is clicked on the grid
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub UpdateStatusCancel(ByVal Rxreferencenumber As String, ByVal MessageId As String)
            Dim ogloInterface As gloSureScriptInterface = Nothing
            Try
                ogloInterface = New gloSureScriptInterface
                ogloInterface.UpdateStatusCancel(Rxreferencenumber, MessageId)
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Catch ex As Exception
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                Throw obj
            Finally
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
        End Sub

        'Generate a denied Refill Response
        Public Function GenerateDeniedRefillResponse(ByVal denialreason As String, ByVal strnotes As String, Optional ByVal RefReqPatientId As String = "", Optional ByVal RefReqNDCCode As String = "", Optional ByVal bIsProviderEPCSEnable As Boolean = False) As Boolean
            Dim ogloInterface As New gloSureScriptInterface
            gloSurescriptGeneral.checkDownloadVersion()
            Dim drugtype As Int16 = 0
            Dim estatus As RefillStatus = Nothing
            Try
                estatus = RefillStatus.eDenied
                If RefReqNDCCode = "" Or RefReqNDCCode = "0" Then
                    drugtype = 0
                Else
                    drugtype = GetDrugType_NDCCode(RefReqNDCCode)
                End If
                If bIsProviderEPCSEnable = False Then
                    If drugtype = 2 Then
                        'strnotes = strnotes & " Schedule 2 drug cannot be refilled"
                        strnotes = "Schedule 2 drug cannot be refilled"
                        gloSurescriptGeneral.InformationMessage(ogloPrescription.DrugsCol.Item(0).DrugName & " is a scheduled drug ,Note:Schedule 2 drug cannot be refilled shall be sent")
                    ElseIf drugtype = 3 Or drugtype = 4 Or drugtype = 5 Then
                        'strnotes = strnotes & " Scheduled drug please call or fax"
                        'strnotes = "Scheduled drug please call or fax"
                        'gloSurescriptGeneral.InformationMessage("Drug " & ogloPrescription.DrugsCol.Item(0).DrugName & " found to be a scheduled drug " & drugtype & " ,Note:Scheduled drug please call or fax shall be sent")
                        gloSurescriptGeneral.InformationMessage(ogloPrescription.DrugsCol.Item(0).DrugName & " is a scheduled " & drugtype & " drug ")
                    End If
                End If
                If ogloPrescription.DrugsCol.Item(0).RefillsQualifier = "" Then
                    ogloPrescription.DrugsCol.Item(0).RefillsQualifier = "R"
                End If
                Dim blnValidation As Boolean = False
                ogloPrescription.DrugsCol.Item(0).eRxFilePath = ogloInterface.GenerateRefillResponse10dot6New(ogloPrescription, estatus, GetDenialReasonCode(denialreason), strnotes, RefReqPatientId)
                Dim bSuccess As Boolean = ogloInterface.PostXMLFile(ogloPrescription, 0, False, "Refill")
                InsertintoTransaction(ogloInterface, RefReqPatientId, denialreason, strnotes)
                blnValidation = True

                If bSuccess Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillResponce, gloAuditTrail.ActivityType.Send, "Refill request denied", RefReqPatientId, 0, ProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                End If
                If blnValidation Then
                    If ogloInterface.StatusMessageType.Length > 0 Then
                        System.Windows.Forms.MessageBox.Show(ogloInterface.StatusMessageType, "Prescription", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End If
                    If ogloInterface.MessageName = "Status" Then
                        ogloInterface.UpdateRefillStatus(ogloPrescription, estatus, 0)
                    End If
                    Return True
                Else
                    If ogloInterface.ValidationMessage.Length > 0 Then
                        System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessage, "Prescription", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End If
                    Return False
                End If
                If Not IsNothing(ogloInterface) Then
                    If ogloInterface.ValidationMessageBuilderforDrug.ToString.Length > 0 Then
                        System.Windows.Forms.MessageBox.Show(ogloInterface.ValidationMessageBuilderforDrug.ToString, "Prescription", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End If
                End If
                Return False
            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)
                Return Nothing
            Catch ex As Exception
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                GenerateDeniedRefillResponse = Nothing
                Throw obj
            Finally
                If Not IsNothing(estatus) Then
                    estatus = Nothing
                End If
                If Not IsNothing(ogloInterface) Then
                    ogloInterface.Dispose()
                    ogloInterface = Nothing
                End If
            End Try
        End Function
        Public Sub InsertintoTransaction(ByRef ogloInterface As gloSureScriptInterface, ByVal PatientID As Int64, ByVal DenialReason As String, ByVal strnotes As String)
            Dim osurescriptdblayer As New gloSureScriptDBLayer
            Dim oResponse As New SureScriptResponseMessage
            Dim DBLayer As New PrescriptionBusinessLayer()
            Try
                osurescriptdblayer.InsertintoMessageTransaction(CType(ogloPrescription.DrugsCol.Item(0), SureScriptMessage))
                oResponse.RefReqPatientId = PatientID
                oResponse.ProviderId = ogloPrescription.ProviderID
                oResponse.ApprovalStatus = False
                oResponse.MessageID = ogloPrescription.DrugsCol.Item(0).MessageID
                oResponse.Denialcode = GetDenialReasonCode(DenialReason)
                oResponse.DenialReason = DenialReason
                oResponse.Notes = strnotes
                oResponse.StatusMessageType = ogloInterface.StatusMessageType                
                DBLayer.InsertResponseTransaction(oResponse.ApprovalStatus, oResponse.StatusMessageType, oResponse.DenialReason, oResponse.Notes, oResponse.MessageID, oResponse.Denialcode, oResponse.RefReqPatientId, oResponse.ProviderId, True)
            Catch ex As Exception
                gloSurescriptGeneral.ErrorMessage(ex.Message)
            Finally
                If Not IsNothing(oResponse) Then
                    oResponse.Dispose()
                    oResponse = Nothing
                End If
                If Not IsNothing(osurescriptdblayer) Then
                    osurescriptdblayer.Dispose()
                    osurescriptdblayer = Nothing
                End If
                If DBLayer IsNot Nothing Then
                    DBLayer.Dispose()
                    DBLayer = Nothing
                End If
            End Try
        End Sub

        'Get the Denial Codes 
        Public Function GetDenialReasonCode(ByVal denialreason As String) As String
            Try
                Select Case denialreason
                    Case "Patient Unknown to the Prescriber"
                        Return "AA"
                    Case "Patient never under Prescriber care"
                        Return "AB"
                    Case "Patient no longer under Prescriber care"
                        Return "AC"
                    Case "Patient has requested refill too soon"
                        Return "AD"
                    Case "Medication never prescribed for the patient"
                        Return "AE"
                    Case "Patient should contact Prescriber first"
                        Return "AF"
                    Case "Refill not appropriate"
                        Return "AG"
                    Case "Patient has picked up prescription"
                        Return "AH"
                    Case "Patient has picked up partial fill of prescription"
                        Return "AJ"
                    Case "Patient has not picked up prescription, drug returned to stock"
                        Return "AK"
                    Case "Patient needs appointment"
                        Return "AM"
                    Case "Prescriber not associated with this practice or location."
                        Return "AN"
                    Case "No attempt will be made to obtain Prior Authorization."
                        Return "AO"
                    Case "Request already responded to by other means (e.g. phone or fax)"
                        Return "AP"
                End Select

                Return ""
            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Sub GetPrescriberPharmacyID(ByVal Rxreferencenumber As String, ByVal dtdatereceived As DateTime, ByVal MessageId As String)
            Dim objSureScriptDBLayer As New gloSureScriptDBLayer
            Dim dt As DataTable = Nothing
            Try
                dt = objSureScriptDBLayer.GetPharmacyandPrescriberID(Rxreferencenumber, dtdatereceived, MessageId)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        mProviderID = dt.Rows(0)("nProviderID")
                        mPharmacyNCPDPID = dt.Rows(0)("sNCPDPID")
                        mProdcode = dt.Rows(0)("sProductCode")
                        mProdQualifier = dt.Rows(0)("sProductCodeQualifier")
                        mPharmacyID = dt.Rows(0)("nPhContactID") ''''this containst the Pharmacy Contact id from contact_MSt table
                    End If
                End If
            Catch ex As gloSurescriptDBException
                gloSurescriptGeneral.ErrorMessage(ex.Message)

            Catch ex As GloSurescriptException
                gloSurescriptGeneral.ErrorMessage(ex.Message)

            Catch ex As Exception
                Dim obj As New gloEMRPrescription.PrescriptionException(ex.Message)
                Throw obj

            Finally
                If Not IsNothing(objSureScriptDBLayer) Then
                    objSureScriptDBLayer.Dispose()
                    objSureScriptDBLayer = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Sub
        Public Function ValidateDrug() As Int16
            Dim drugtype As Int16 = GetDrugType(ogloPrescription.DrugsCol.Item(0).DrugName, ogloPrescription.RxTransactionID, 0)
            Return drugtype
        End Function

        ''' <summary>
        ''' This function is called from Pending refill request from
        ''' </summary>
        ''' <param name="DrugName"></param>
        ''' <param name="nRxTransactionId"></param>
        ''' <param name="Drug_id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDrugType(ByVal DrugName As String, ByVal nRxTransactionId As String, ByRef Drug_id As Int64, Optional ByVal mRefReqNDCCode As String = "") As Int16
            Dim _isnarcotic As Int16 = -1

            Try
                If nRxTransactionId <> 0 Then
                    Return GetDrugType(nRxTransactionId)
                Else
                    If mRefReqNDCCode <> "" Then
                        mProdcode = mRefReqNDCCode
                    End If
                    If Not IsNothing(mProdQualifier) Then
                        If mProdQualifier = "ND" AndAlso mProdcode <> "" Then
                            Dim productcode As String = ""

                            Select Case mProdcode.Length
                                Case 11
                                    productcode = mProdcode
                                Case 10
                                    productcode = "0" & mProdcode
                                Case 9
                                    productcode = "00" & mProdcode
                            End Select
                            _isnarcotic = GetDrugInfo(productcode)

                        Else
                            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                _isnarcotic = oDIBGSHelper.IsNarcoticFromDrugName(DrugName)
                            End Using
                        End If
                    Else
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                            _isnarcotic = oDIBGSHelper.IsNarcoticFromDrugName(DrugName)
                        End Using
                    End If

                End If
                Return _isnarcotic

            Catch ex As Exception
                Return _isnarcotic
            End Try
        End Function



        ''' <summary>
        ''' 'This function is called from Refill Request control of Rx form
        ''' </summary>
        ''' <param name="DrugName"></param>
        ''' <param name="nRxTransactionId"></param>
        ''' <param name="Drug_id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDrugType_Rx(ByVal DrugName As String, ByVal nRxTransactionId As String, ByRef Drug_id As Int64) As Int16
            Dim _isnarcotic As Int64 = -1

            Try
                If nRxTransactionId <> 0 Then
                    Return GetDrugType(nRxTransactionId)
                Else

                    If Drug_id <> 0 Then
                        If Drug_id > 0 Then
                            Return GetDrugType("", Drug_id)
                        Else
                            Return -1
                        End If
                    End If



                    If Not IsNothing(mProdQualifier) Then
                        If mProdQualifier = "ND" AndAlso mProdcode <> "" Then
                            Dim productcode As String = ""

                            Select Case mProdcode.Length
                                Case 11
                                    productcode = mProdcode
                                Case 10
                                    productcode = "0" & mProdcode
                                Case 9
                                    productcode = "00" & mProdcode
                            End Select
                            _isnarcotic = GetDrugInfo(productcode)

                        Else
                            Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                _isnarcotic = oDIBGSHelper.IsNarcoticFromDrugName(DrugName)
                            End Using
                        End If
                    Else
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                            _isnarcotic = oDIBGSHelper.IsNarcoticFromDrugName(DrugName)
                        End Using
                    End If
                End If
                Return _isnarcotic
            Catch ex As Exception
                Return -1
            End Try
        End Function

   

       Public Function GetDrugInfoFromNDCCode(ByVal _NDCCode As String) As gloGlobal.DIB.DrugDetails
            
            Dim oDrugInfo As gloGlobal.DIB.DrugDetails = Nothing
            Try
                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    oDrugInfo = oDIBGSHelper.GetDrugsInfoGSDDforRx_Saving(_NDCCode)
                End Using

                Return oDrugInfo
            Catch ex As Exception
                Return Nothing
            End Try
        End Function



       Public Function GetDrugInfoFromGPI(ByVal _GPICode As String, ByVal _Drugname As String) As gloGlobal.DIB.DrugDetails
          
            Dim oDrugInfo As gloGlobal.DIB.DrugDetails = Nothing
            Try
                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    oDrugInfo = oDIBGSHelper.GetDrugsInfoGSDDforRx_SavingByDrugName(_Drugname)
                End Using
                Return oDrugInfo
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


          Public Function GetDrugInfoFromRxNorm(ByVal _RxNormCode As String) As gloGlobal.DIB.DrugDetails
          
            Dim oDrugInfo As gloGlobal.DIB.DrugDetails = Nothing
            Try
                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    oDrugInfo = oDIBGSHelper.GetDrugsInfoGSDDforRx_SavingByRxNorm(_RxNormCode)
                End Using

                If Not IsNothing(oDrugInfo) Then
                    Dim _NDCCode As String = oDrugInfo.NDC

                    If Not IsNothing(oDrugInfo) Then
                        oDrugInfo.Dispose()
                        oDrugInfo = Nothing
                    End If

                    Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        oDrugInfo = oDIBGSHelper.GetDrugsInfoGSDDforRx_Saving(_NDCCode)
                    End Using
                End If
                Return oDrugInfo
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
      
        Public Function GetDrugType_NDCCode(ByVal NDCCode As String) As Int16
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _strsql As String
            Try

                _strsql = "select isnull(d.nIsNarcotics,0) from drugs_mst d where sNDCCode = '" & NDCCode & "'"


                Dim drugtype As Int16 = _gloEMRDatabase.GetDataValue(_strsql, False)
                If Not IsDBNull(drugtype) Then
                    Return drugtype
                Else
                    Return 0
                End If
            Catch ex As gloEMRDatabase.gloDBException
                Return -1
            Catch ex As Exception
                Return -1
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function



        Public Function GetRxMedsDrugInfo(ByVal RxTransactionID As String, Optional ByVal DrugId As Int64 = 0) As DataTable
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _strsql As String
            Dim dtRxMedsDrugInfo As DataTable = Nothing
            Try
                'Bug #60577: 00000578 : Refill Requests. ERROR MESSAGE
                'NDC Code considered from drug master instead of Pescription.
                _strsql = "select nDrugID, isnull(d.sNDCCode,'') as sNDCCode, isnull(d.nIsNarcotics,0) as nIsNarcotics from Prescription p inner join drugs_mst d on p.mpid=d.mpid where p.nprescriptionid=" & RxTransactionID & ""

                dtRxMedsDrugInfo = _gloEMRDatabase.GetDataTable_Query(_strsql)

                Return dtRxMedsDrugInfo

            Catch ex As gloEMRDatabase.gloDBException
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
                'If Not IsNothing(dtRxMedsDrugInfo) Then
                '    dtRxMedsDrugInfo.Dispose()
                '    dtRxMedsDrugInfo = Nothing
                'End If
            End Try
        End Function

        Public Function GetDrugInfo(Optional ByVal mRefReqNDCCode As String = "") As Int64
            Dim drugtype As Int64 = -1
            Try
                If mRefReqNDCCode <> "" Then
                    mProdcode = mRefReqNDCCode
                    Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                        drugtype = oDIBGSHelper.IsNarcotic(mRefReqNDCCode)
                    End Using

                    Return drugtype
                Else
                    Return drugtype
                End If

            Catch ex As Exception
                Return drugtype
            End Try
        End Function

        Public Function GetDrugtype(ByVal RxTransactionID As String, Optional ByVal DrugId As Int64 = 0) As Int16
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _strsql As String
            Try
                If DrugId = 0 Then
                    _strsql = "select isnull(d.nIsNarcotics,0) from Prescription p inner join drugs_mst d on p.ndrugid=d.ndrugsid where p.nprescriptionid=" & RxTransactionID & ""
                Else
                    _strsql = "select isnull(d.nIsNarcotics,0) from drugs_mst d where ndrugsid=" & DrugId & ""
                End If

                Dim drugtype As Int16 = _gloEMRDatabase.GetDataValue(_strsql, False)
                If Not IsDBNull(drugtype) Then
                    Return drugtype
                Else
                    Return 0
                End If
            Catch ex As gloEMRDatabase.gloDBException
                Return -1
            Catch ex As Exception
                Return -1
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function
        Public Function GetOrderID(ByVal RxTransactionID As String) As Boolean
            Dim ogloEMRDatabase As gloEMRDatabase.DataBaseLayer = New DataBaseLayer
            Try
                Dim _strGetExtCdSQl = "SELECT npatientid FROM Prescription where nPrescriptionID = " & RxTransactionID
                Dim PatientID As Int64 = ogloEMRDatabase.GetDataValue(_strGetExtCdSQl, False)

                If Not IsDBNull(PatientID) AndAlso PatientID <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As gloEMRDatabase.gloDBException
                Throw ex
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
    End Class
    'C-R
End Namespace