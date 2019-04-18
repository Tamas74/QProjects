Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloGeneral

Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer

Namespace gloEMRMedication
    Public Class MedicationBusinessLayer
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _CurrentVisitID As Int64
        Private _PastVisitID As Int64
        Private _PrevVisitIDforSupervisingProvider As Int64
        Private _CurrentVisitDate As Date
        Private _PastVisitDate As Date
        Public _MostRecentVisitID_Mx As Int64 'Incident #55315: 00016572 : Carry forward issue
        Private _Medicationdate As DateTime
        Private _Medications As Medications

        Private eTransactionMode As _TransactionMode
        Private m_FilterType As String
        Public Event MedicationSaveStatus(ByVal blnsaved As Boolean)
        Public Event RollRowsCount(ByVal FilterStatus As String) 'event created so that the flex grid is refreshed
        Public Event DrugDuplication(ByVal ValidationMessage As String)
        Public Event DisplayMessage(ByVal strVisitdate As DateTime)
        Public Event MedicationDeleted()
        Public Event Recordlock(ByVal blnRecordLock As Boolean)
        Public Event LockWindowForUpdate()
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
        Dim _PatientID As Long
        '----------------------------------------
        Private _MedicalConditions As Collection
        '----------------------------------------
        Private _intCurrentVisitID As Int64
        Private _Histories As Histories

        Enum _TransactionMode
            Add
            Edit
        End Enum


        Public Sub New(ByVal PatientID As Long)
            MyBase.New()
            _PatientID = PatientID
            Me.FilterType = "Active"
        End Sub


        Private Sub InitialiseObjects()
            _Medications = New Medications
            _Histories = New Histories
        End Sub

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If Not IsNothing(_Medications) Then
                        _Medications.Dispose()
                        _Medications = Nothing
                    End If
                    If Not IsNothing(_Histories) Then
                        _Histories.Dispose()
                        _Histories = Nothing
                    End If
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

        Dim _routes As List(Of String)
        Public Property routes() As List(Of String)
            Get
                Return _routes
            End Get
            Set(ByVal Value As List(Of String))
                _routes = Value
            End Set
        End Property

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
        '------------
        Public ReadOnly Property GetCurrentUserName() As String
            Get
                Return globalSecurity.gstrLoginName
            End Get

        End Property
        Public Property FilterType() As String
            Get
                Return m_FilterType
            End Get
            Set(ByVal Value As String)
                m_FilterType = Value
            End Set
        End Property
        Public Property TransactionMode() As _TransactionMode
            Get
                Return eTransactionMode
            End Get
            Set(ByVal value As _TransactionMode)
                eTransactionMode = value
            End Set
        End Property

        Public Property CurrentVisitID() As Int64
            Get
                Return _CurrentVisitID
            End Get
            Set(ByVal value As Int64)
                _CurrentVisitID = value
            End Set
        End Property
        Public Property PastVisitID() As Int64
            Get
                Return _PastVisitID
            End Get
            Set(ByVal value As Int64)
                _PastVisitID = value
            End Set
        End Property
        Public Property PrevVisitIDforSupervisingProvider() As Int64
            Get
                Return _PrevVisitIDforSupervisingProvider
            End Get
            Set(ByVal value As Int64)
                _PrevVisitIDforSupervisingProvider = value
            End Set
        End Property
        Public Property CurrentVisitDate() As Date
            Get
                Return _CurrentVisitDate
            End Get
            Set(ByVal value As Date)
                _CurrentVisitDate = value
            End Set
        End Property
        Public Property PastVisitDate() As Date
            Get
                Return _PastVisitDate
            End Get
            Set(ByVal value As Date)
                _PastVisitDate = value
            End Set
        End Property
        Public Property Medicationdate() As DateTime
            Get
                Return _Medicationdate
            End Get
            Set(ByVal value As DateTime)
                _Medicationdate = value
            End Set
        End Property
        Public Property MedicationCol() As Medications
            Get
                If IsNothing(_Medications) Then
                    _Medications = New Medications
                End If
                Return _Medications
            End Get
            Set(ByVal value As Medications)
                _Medications = value
            End Set
        End Property

        Public ReadOnly Property HistoriesCol() As Histories
            Get
                Return _Histories
            End Get
        End Property

        Public Property MedicalCondtionCol() As Collection
            Get
                Return _MedicalConditions
            End Get
            Set(ByVal value As Collection)
                _MedicalConditions = value
            End Set
        End Property

        Private Function SearchMedication(ByVal id As Int64) As Medication 'ClsPrescription
            Dim objnewmedication As Medication = Nothing 'ClsPrescription
            Try
                For Each objnewmedication In _Medications 'ArrMedicationCol
                    If objnewmedication.MedicationID = id Then
                        Return objnewmedication
                        'Exit Function
                    End If
                Next
                Return Nothing

            Catch ex As Exception
                Return Nothing

            Finally
                If Not IsNothing(objnewmedication) Then
                    objnewmedication.Dispose()
                    objnewmedication = Nothing
                End If
            End Try

        End Function

        Friend Function FetchVisitDate(ByVal id As Int64) As Object
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim tmpvisitdate As DateTime
            Try
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
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
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

        Public Function FetchPastVisitDate() As Date
            Try
                _PastVisitDate = FetchVisitDate(_PastVisitID)
                _CurrentVisitDate = _PastVisitDate
                Return _PastVisitDate
            Catch ex As Exception
                Dim objex As New MedicationBusinessLayerException
                objex.ErrMessage = "Error Fetching Visit Date"
                Throw objex
            Finally

            End Try
        End Function

        Public Function FetchMostRecentVisit() As Int64
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim tmpvisitid As Int64
            Try
                Dim objParameter As DBParameter
                objParameter = New DBParameter
                objParameter.Value = _PatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing
                objParameter = New DBParameter
                objParameter.Value = _PastVisitID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing
                tmpvisitid = _gloEMRDatabase.GetDataValue("gsp_GetMostRecentVisitID_Mx")

                If Not IsDBNull(tmpvisitid) Then
                    _MostRecentVisitID_Mx = tmpvisitid
                Else
                    _MostRecentVisitID_Mx = 0
                End If
                Return _MostRecentVisitID_Mx
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching Visit id : " + ex.Message.ToString
                Throw objex
                _MostRecentVisitID_Mx = Nothing
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Private Function getServerTime() As DateTime
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
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error Retrieving Server time"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function SaveMedication(ByRef visitid As Int64, ByVal _VisitDate As DateTime, ByVal enm As MedicationBusinessLayer._TransactionMode, ByRef _Medications As Medications, ByVal m_FiterType As String, Optional ByVal flag As Boolean = False) As Boolean
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer

            'Dim _DBParameter As DBParameter
            Dim Medicationdate As DateTime
            Dim _Medication As Medication
            Dim tempvisitid As Int64
            Dim i As Int16

            _Medication = _Medications.Item(0)

            Try
                If enm = _TransactionMode.Edit Then
                    'DeleteMedication(_Medication.VisitID, _VisitDate, m_FiterType)
                    Medicationdate = _VisitDate
                    tempvisitid = visitid
                    If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnCustomMediEdited = True Then ''''for CCHIT11 audit log  gblnCustomMediEdited
                        ' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.ModifyMedication, gloAuditTrail.ActivityType.Modify, "Medication modified", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                ElseIf enm = _TransactionMode.Add Then
                    If visitid = 0 Then
                        Dim _Visit As New Visit
                        Dim _VisitBusinessLayer As New VisitBusinessLayer
                        _Visit.PatientID = _PatientID
                        _Visit.VisitID = 0
                        _Visit.VisitDate = _VisitDate

                        _VisitBusinessLayer.VisitObject = _Visit
                        If _VisitBusinessLayer.InsertVisit Then
                            tempvisitid = _Visit.VisitID
                            visitid = tempvisitid
                        End If
                        If Not IsNothing(_VisitBusinessLayer) Then
                            _VisitBusinessLayer.Dispose()
                            _VisitBusinessLayer = Nothing
                        End If
                        If Not IsNothing(_Visit) Then
                            _Visit.Dispose()
                            _Visit = Nothing
                        End If
                    Else
                        tempvisitid = visitid
                    End If
                    Medicationdate = _VisitDate
                    If flag = False Then
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.Add, "Medication added", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    End If
                End If

                Dim tempServerDtTime As DateTime
                tempServerDtTime = getServerTime()
                'Fill the collection in dataset table
                For i = 0 To _Medications.Count - 1
                    If flag = True Then
                        If _Medications.Item(i).Renewed <> "" Then
                            Dim strSql As String = "select nMedicationID from medication where sMedication = '" & _Medications.Item(i).Medication & "'  and nVisitID = " & visitid & " and nPatientID = " & _PatientID & " and sstatus=''"
                            Dim MedID As Long = _gloEMRDatabase.GetDataValue(strSql, False)
                            If MedID <> 0 Then
                                Dim strsql_MedUpdate = "update Medication set sDosage= '" & _Medications.Item(i).Dosage & "',sRoute = '" & _Medications.Item(i).Route & "',sFrequency = '" & _Medications.Item(i).Frequency & "',sDuration = '" & _Medications.Item(i).Duration & "',sAmount = '" & _Medications.Item(i).Amount & "',sRenewed = '" & _Medications.Item(i).Renewed & "' where nMedicationID = " & MedID & " and nVisitID = " & visitid & " and nPatientID = " & _PatientID & ""
                                Dim MedUpdate_retval As Boolean
                                MedUpdate_retval = _gloEMRDatabase.GetDataValue(strsql_MedUpdate, False)
                            Else
                                _Medication = _Medications.Item(i)
                                If _Medication.State <> "U" Then
                                    Dim newMxRow As DataRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").NewRow()
                                    newMxRow.Item("nMedicationId") = _Medication.MedicationID
                                    If enm = _TransactionMode.Edit Then
                                        If _Medication.VisitID = 0 Then
                                            newMxRow.Item("nVisitId") = tempvisitid
                                        Else
                                            newMxRow.Item("nVisitId") = _Medication.VisitID
                                        End If
                                    ElseIf enm = _TransactionMode.Add Then
                                        newMxRow.Item("nVisitId") = tempvisitid
                                    End If

                                    newMxRow.Item("nPatientID") = _PatientID
                                    newMxRow.Item("sMedication") = _Medication.Medication
                                    newMxRow.Item("sDosage") = _Medication.Dosage
                                    newMxRow.Item("sRoute") = _Medication.Route
                                    newMxRow.Item("sFrequency") = _Medication.Frequency
                                    newMxRow.Item("sDuration") = _Medication.Duration

                                    If Not IsNothing(_Medication.Startdate) Then
                                        If _Medication.Startdate = "12:00:00 AM" Then
                                            newMxRow.Item("dtstartdate") = Now
                                        Else
                                            newMxRow.Item("dtstartdate") = _Medication.Startdate
                                        End If
                                    Else
                                        newMxRow.Item("dtstartdate") = Now
                                    End If

                                    If _Medication.CheckEndDate = True Then
                                        If Not IsNothing(_Medication.Enddate) Then
                                            If _Medication.Enddate = "12:00:00 AM" Then
                                                newMxRow.Item("dtEnddate") = DBNull.Value
                                            Else
                                                newMxRow.Item("dtEnddate") = _Medication.Enddate
                                            End If
                                        Else
                                            newMxRow.Item("dtEnddate") = DBNull.Value
                                        End If
                                    End If

                                    newMxRow.Item("sAmount") = _Medication.Amount
                                    If enm = _TransactionMode.Edit Then
                                        If flag = True Or _Medication.MedicationID = 0 Then 'we check the flag value because we need to reflect the appropriate date i.e if its added from the Rx module the it will show the Rx date else will show the Mx date
                                            If _Medication._PrescriptionId = 0 Then
                                                _Medication.Medicationdate = Medicationdate
                                                newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                            Else
                                                _Medication.Medicationdate = tempServerDtTime
                                                newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate ' .Medicationdate
                                            End If
                                        Else
                                            _Medication.Medicationdate = Medicationdate
                                            newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                        End If

                                    ElseIf enm = _TransactionMode.Add Then
                                        If flag = True Then
                                            newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                        Else
                                            _Medication.Medicationdate = Medicationdate
                                            newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                        End If
                                    End If

                                    newMxRow.Item("sStatus") = _Medication.Status
                                    newMxRow.Item("sReason") = _Medication.Reason
                                    newMxRow.Item("mpid") = _Medication.mpid

                                    newMxRow.Item("sUserName") = _Medication.UserName
                                    newMxRow.Item("sUpdatedBy") = _Medication.UpdatedByUserName
                                    newMxRow.Item("nPrescriptionID") = _Medications.Item(i)._PrescriptionId
                                    newMxRow.Item("sRenewed") = _Medication.Renewed
                                    newMxRow.Item("sNDCCode") = _Medications.Item(i).NDCCode
                                    newMxRow.Item("nIsNarcotic") = _Medications.Item(i).IsNarcotics
                                    newMxRow.Item("sDrugForm") = _Medications.Item(i).DosageForm
                                    newMxRow.Item("sStrengthUnit") = _Medications.Item(i).StrengthUnit
                                    newMxRow.Item("Rx_sRefills") = _Medications.Item(i).Rx_Refills
                                    newMxRow.Item("Rx_sNotes") = _Medications.Item(i).Rx_Notes
                                    newMxRow.Item("Rx_sMethod") = _Medications.Item(i).Rx_Method
                                    newMxRow.Item("Rx_bMaySubstitute") = _Medications.Item(i).Rx_MaySubstitute
                                    newMxRow.Item("Rx_nDrugID") = _Medications.Item(i).Rx_DrugID
                                    newMxRow.Item("Rx_blnflag") = _Medications.Item(i).Rx_blnflag
                                    newMxRow.Item("Rx_sLotNo") = _Medications.Item(i).Rx_LotNo
                                    If Not IsNothing(_Medications.Item(i).Rx_Expirationdate) Then
                                        If _Medications.Item(i).Rx_Expirationdate = "12:00:00 AM" Then
                                            newMxRow.Item("Rx_dtExpirationdate") = DBNull.Value
                                        Else
                                            newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                                        End If
                                    Else
                                        newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                                    End If

                                    newMxRow.Item("Rx_nProviderId") = _Medications.Item(i).Rx_ProviderId
                                    newMxRow.Item("Rx_sChiefComplaints") = _Medications.Item(i).Rx_ChiefComplaints
                                    newMxRow.Item("Rx_sStatus") = _Medications.Item(i).Rx_Status
                                    newMxRow.Item("Rx_sRxReferenceNumber") = _Medications.Item(i).Rx_RxReferenceNumber
                                    newMxRow.Item("Rx_sRefillQualifier") = _Medications.Item(i).Rx_RefillQualifier
                                    newMxRow.Item("Rx_nPharmacyId") = _Medications.Item(i).Rx_PharmacyId
                                    newMxRow.Item("Rx_sNCPDPID") = _Medications.Item(i).Rx_NCPDPID
                                    newMxRow.Item("Rx_nContactID") = _Medications.Item(i).Rx_ContactID
                                    newMxRow.Item("Rx_sName") = _Medications.Item(i).Rx_PhName
                                    newMxRow.Item("Rx_sAddressline1") = _Medications.Item(i).Rx_Addressline1
                                    newMxRow.Item("Rx_sAddressline2") = _Medications.Item(i).Rx_Addressline2
                                    newMxRow.Item("Rx_sCity") = _Medications.Item(i).Rx_City
                                    newMxRow.Item("Rx_sState") = _Medications.Item(i).Rx_State
                                    newMxRow.Item("Rx_sZip") = _Medications.Item(i).Rx_Zip
                                    newMxRow.Item("Rx_sEmail") = _Medications.Item(i).Rx_Email
                                    newMxRow.Item("Rx_sFax") = _Medications.Item(i).Rx_Fax
                                    newMxRow.Item("Rx_sPhone") = _Medications.Item(i).Rx_Phone
                                    newMxRow.Item("Rx_sServiceLevel") = _Medications.Item(i).Rx_ServiceLevel
                                    newMxRow.Item("Rx_sPrescriberNotes") = _Medications.Item(i).Rx_PrescriberNotes
                                    newMxRow.Item("Rx_eRxStatus") = _Medications.Item(i).Rx_eRxStatus
                                    newMxRow.Item("Rx_eRxStatusMessage") = _Medications.Item(i).Rx_eRxStatusMessage
                                    newMxRow.Item("sPBMSourceName") = _Medications.Item(i).PBMSourceName
                                    newMxRow.Item("RxMed_DMSID") = _Medications.Item(i).RxMedDMSID
                                    newMxRow.Item("RowState") = _Medications.Item(i).State
                                    newMxRow.Item("Rx_IsCPOEOrder") = _Medications.Item(i).Rx_CPOEOrder
                                    newMxRow.Item("sReasonConceptID") = _Medications.Item(i).ReasonConceptID
                                    newMxRow.Item("sReasonConceptDesc") = _Medications.Item(i).ReasonConceptDesc

                                    newMxRow.Item("sValuesetOID") = _Medications.Item(i).CQMCategories
                                    newMxRow.Item("sValueSetName") = _Medications.Item(i).CQMDesc

                                    newMxRow.Item("Rx_IsMedicationAdministered") = _Medications.Item(i).Rx_MedicationAdministered
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Add(newMxRow)
                                    If Not IsNothing(newMxRow) Then
                                        newMxRow = Nothing
                                    End If
                                End If
                            End If
                        Else
                            _Medication = _Medications.Item(i)
                            If _Medication.State <> "U" Then
                                Dim newMxRow As DataRow =
                                   gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").NewRow()
                                newMxRow.Item("nMedicationId") = _Medication.MedicationID
                                If enm = _TransactionMode.Edit Then
                                    If _Medication.VisitID = 0 Then
                                        newMxRow.Item("nVisitId") = tempvisitid
                                    Else
                                        newMxRow.Item("nVisitId") = _Medication.VisitID
                                    End If
                                ElseIf enm = _TransactionMode.Add Then
                                    newMxRow.Item("nVisitId") = tempvisitid
                                End If

                                newMxRow.Item("nPatientID") = _PatientID
                                newMxRow.Item("sMedication") = _Medication.Medication
                                newMxRow.Item("sDosage") = _Medication.Dosage
                                newMxRow.Item("sRoute") = _Medication.Route
                                newMxRow.Item("sFrequency") = _Medication.Frequency
                                newMxRow.Item("sDuration") = _Medication.Duration

                                If Not IsNothing(_Medication.Startdate) Then
                                    If _Medication.Startdate = "12:00:00 AM" Then
                                        newMxRow.Item("dtstartdate") = Now
                                    Else
                                        newMxRow.Item("dtstartdate") = _Medication.Startdate
                                    End If
                                Else
                                    newMxRow.Item("dtstartdate") = Now
                                End If

                                If _Medication.CheckEndDate = True Then
                                    If Not IsNothing(_Medication.Enddate) Then
                                        If _Medication.Enddate = "12:00:00 AM" Then
                                            newMxRow.Item("dtEnddate") = DBNull.Value
                                        Else
                                            newMxRow.Item("dtEnddate") = _Medication.Enddate
                                        End If
                                    Else
                                        newMxRow.Item("dtEnddate") = DBNull.Value
                                    End If
                                End If

                                newMxRow.Item("sAmount") = _Medication.Amount
                                If enm = _TransactionMode.Edit Then
                                    If flag = True Or _Medication.MedicationID = 0 Then 'we check the flag value because we need to reflect the appropriate date i.e if its added from the Rx module the it will show the Rx date else will show the Mx date
                                        If _Medication._PrescriptionId = 0 Then
                                            _Medication.Medicationdate = Medicationdate
                                            newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                        Else
                                            _Medication.Medicationdate = tempServerDtTime
                                            newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate ' .Medicationdate
                                        End If
                                    Else
                                        _Medication.Medicationdate = Medicationdate
                                        newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                    End If

                                ElseIf enm = _TransactionMode.Add Then
                                    If flag = True Then
                                        newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                    Else
                                        _Medication.Medicationdate = Medicationdate
                                        newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                    End If
                                End If

                                newMxRow.Item("sStatus") = _Medication.Status
                                newMxRow.Item("sReason") = _Medication.Reason
                                newMxRow.Item("mpid") = _Medication.mpid
                                newMxRow.Item("sUserName") = _Medication.UserName
                                newMxRow.Item("sUpdatedBy") = _Medication.UpdatedByUserName
                                newMxRow.Item("nPrescriptionID") = _Medications.Item(i)._PrescriptionId
                                newMxRow.Item("sRenewed") = _Medication.Renewed
                                newMxRow.Item("sNDCCode") = _Medications.Item(i).NDCCode
                                newMxRow.Item("nIsNarcotic") = _Medications.Item(i).IsNarcotics
                                newMxRow.Item("sDrugForm") = _Medications.Item(i).DosageForm
                                newMxRow.Item("sStrengthUnit") = _Medications.Item(i).StrengthUnit
                                newMxRow.Item("Rx_sRefills") = _Medications.Item(i).Rx_Refills
                                newMxRow.Item("Rx_sNotes") = _Medications.Item(i).Rx_Notes
                                newMxRow.Item("Rx_sMethod") = _Medications.Item(i).Rx_Method
                                newMxRow.Item("Rx_bMaySubstitute") = _Medications.Item(i).Rx_MaySubstitute
                                newMxRow.Item("Rx_nDrugID") = _Medications.Item(i).Rx_DrugID
                                newMxRow.Item("Rx_blnflag") = _Medications.Item(i).Rx_blnflag
                                newMxRow.Item("Rx_sLotNo") = _Medications.Item(i).Rx_LotNo
                                If Not IsNothing(_Medications.Item(i).Rx_Expirationdate) Then
                                    If _Medications.Item(i).Rx_Expirationdate = "12:00:00 AM" Then
                                        newMxRow.Item("Rx_dtExpirationdate") = DBNull.Value
                                    Else
                                        newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                                    End If
                                Else
                                    newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                                End If

                                newMxRow.Item("Rx_nProviderId") = _Medications.Item(i).Rx_ProviderId
                                newMxRow.Item("Rx_sChiefComplaints") = _Medications.Item(i).Rx_ChiefComplaints
                                newMxRow.Item("Rx_sStatus") = _Medications.Item(i).Rx_Status
                                newMxRow.Item("Rx_sRxReferenceNumber") = _Medications.Item(i).Rx_RxReferenceNumber
                                newMxRow.Item("Rx_sRefillQualifier") = _Medications.Item(i).Rx_RefillQualifier
                                newMxRow.Item("Rx_nPharmacyId") = _Medications.Item(i).Rx_PharmacyId
                                newMxRow.Item("Rx_sNCPDPID") = _Medications.Item(i).Rx_NCPDPID
                                newMxRow.Item("Rx_nContactID") = _Medications.Item(i).Rx_ContactID
                                newMxRow.Item("Rx_sName") = _Medications.Item(i).Rx_PhName
                                newMxRow.Item("Rx_sAddressline1") = _Medications.Item(i).Rx_Addressline1
                                newMxRow.Item("Rx_sAddressline2") = _Medications.Item(i).Rx_Addressline2
                                newMxRow.Item("Rx_sCity") = _Medications.Item(i).Rx_City
                                newMxRow.Item("Rx_sState") = _Medications.Item(i).Rx_State
                                newMxRow.Item("Rx_sZip") = _Medications.Item(i).Rx_Zip
                                newMxRow.Item("Rx_sEmail") = _Medications.Item(i).Rx_Email
                                newMxRow.Item("Rx_sFax") = _Medications.Item(i).Rx_Fax
                                newMxRow.Item("Rx_sPhone") = _Medications.Item(i).Rx_Phone
                                newMxRow.Item("Rx_sServiceLevel") = _Medications.Item(i).Rx_ServiceLevel
                                newMxRow.Item("Rx_sPrescriberNotes") = _Medications.Item(i).Rx_PrescriberNotes
                                newMxRow.Item("Rx_eRxStatus") = _Medications.Item(i).Rx_eRxStatus
                                newMxRow.Item("Rx_eRxStatusMessage") = _Medications.Item(i).Rx_eRxStatusMessage
                                newMxRow.Item("sPBMSourceName") = _Medications.Item(i).PBMSourceName
                                newMxRow.Item("RxMed_DMSID") = _Medications.Item(i).RxMedDMSID
                                newMxRow.Item("Rx_IsCPOEOrder") = _Medications.Item(i).Rx_CPOEOrder
                                newMxRow.Item("sReasonConceptID") = _Medications.Item(i).ReasonConceptID
                                newMxRow.Item("sReasonConceptDesc") = _Medications.Item(i).ReasonConceptDesc

                                newMxRow.Item("sValuesetOID") = _Medications.Item(i).CQMCategories
                                newMxRow.Item("sValueSetName") = _Medications.Item(i).CQMDesc
                                newMxRow.Item("Rx_IsMedicationAdministered") = _Medications.Item(i).Rx_MedicationAdministered
                                newMxRow.Item("RowState") = _Medications.Item(i).State
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Add(newMxRow)
                                If Not IsNothing(newMxRow) Then
                                    newMxRow = Nothing
                                End If
                            End If
                        End If
                    Else
                        _Medication = _Medications.Item(i)
                        If _Medication.State <> "U" Then
                            Dim newMxRow As DataRow = gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").NewRow()
                            newMxRow.Item("nMedicationId") = _Medication.MedicationID
                            If enm = _TransactionMode.Edit Then
                                If _Medication.VisitID = 0 Then
                                    newMxRow.Item("nVisitId") = tempvisitid
                                Else
                                    'Added visitid for Drugs carry forward issue.
                                    newMxRow.Item("nVisitId") = visitid
                                End If
                            ElseIf enm = _TransactionMode.Add Then
                                newMxRow.Item("nVisitId") = tempvisitid
                            End If

                            newMxRow.Item("nPatientID") = _PatientID
                            newMxRow.Item("sMedication") = _Medication.Medication
                            newMxRow.Item("sDosage") = _Medication.Dosage
                            newMxRow.Item("sRoute") = _Medication.Route
                            newMxRow.Item("sFrequency") = _Medication.Frequency
                            newMxRow.Item("sDuration") = _Medication.Duration

                            If Not IsNothing(_Medication.Startdate) Then
                                If _Medication.Startdate = "12:00:00 AM" Then
                                    newMxRow.Item("dtstartdate") = Now
                                Else
                                    newMxRow.Item("dtstartdate") = _Medication.Startdate
                                End If
                            Else
                                newMxRow.Item("dtstartdate") = Now
                            End If

                            If _Medication.CheckEndDate = True Then
                                If Not IsNothing(_Medication.Enddate) Then
                                    If _Medication.Enddate = "12:00:00 AM" Then
                                        newMxRow.Item("dtEnddate") = DBNull.Value
                                    Else
                                        newMxRow.Item("dtEnddate") = _Medication.Enddate
                                    End If
                                Else
                                    newMxRow.Item("dtEnddate") = DBNull.Value
                                End If
                            End If

                            newMxRow.Item("sAmount") = _Medication.Amount
                            If enm = _TransactionMode.Edit Then
                                If flag = True Or _Medication.MedicationID = 0 Then 'we check the flag value because we need to reflect the appropriate date i.e if its added from the Rx module the it will show the Rx date else will show the Mx date
                                    If _Medication._PrescriptionId = 0 Then
                                        _Medication.Medicationdate = Medicationdate
                                        newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                    Else
                                        _Medication.Medicationdate = tempServerDtTime
                                        newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate ' .Medicationdate
                                    End If
                                Else
                                    _Medication.Medicationdate = Medicationdate
                                    newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                End If

                            ElseIf enm = _TransactionMode.Add Then
                                If flag = True Then
                                    newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                Else
                                    _Medication.Medicationdate = Medicationdate
                                    newMxRow.Item("dtMedicationDate") = _Medication.Medicationdate
                                End If
                            End If

                            newMxRow.Item("sStatus") = _Medication.Status
                            newMxRow.Item("sReason") = _Medication.Reason
                            newMxRow.Item("mpid") = _Medication.mpid
                            newMxRow.Item("sUserName") = globalSecurity.gstrLoginName ''''code added  for problem 00000558
                            newMxRow.Item("sUpdatedBy") = _Medication.UpdatedByUserName
                            newMxRow.Item("nPrescriptionID") = _Medications.Item(i)._PrescriptionId
                            newMxRow.Item("sRenewed") = _Medication.Renewed
                            newMxRow.Item("sNDCCode") = _Medications.Item(i).NDCCode
                            newMxRow.Item("nIsNarcotic") = _Medications.Item(i).IsNarcotics
                            newMxRow.Item("sDrugForm") = _Medications.Item(i).DosageForm
                            newMxRow.Item("sStrengthUnit") = _Medications.Item(i).StrengthUnit
                            newMxRow.Item("Rx_sRefills") = _Medications.Item(i).Rx_Refills
                            newMxRow.Item("Rx_sNotes") = _Medications.Item(i).Rx_Notes
                            newMxRow.Item("Rx_sMethod") = _Medications.Item(i).Rx_Method
                            newMxRow.Item("Rx_bMaySubstitute") = _Medications.Item(i).Rx_MaySubstitute
                            newMxRow.Item("Rx_nDrugID") = _Medications.Item(i).Rx_DrugID
                            newMxRow.Item("Rx_blnflag") = _Medications.Item(i).Rx_blnflag
                            newMxRow.Item("Rx_sLotNo") = _Medications.Item(i).Rx_LotNo
                            If Not IsNothing(_Medications.Item(i).Rx_Expirationdate) Then
                                If _Medications.Item(i).Rx_Expirationdate = "12:00:00 AM" Then
                                    newMxRow.Item("Rx_dtExpirationdate") = DBNull.Value
                                Else
                                    newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                                End If
                            Else
                                newMxRow.Item("Rx_dtExpirationdate") = _Medications.Item(i).Rx_Expirationdate
                            End If

                            newMxRow.Item("Rx_nProviderId") = _Medications.Item(i).Rx_ProviderId
                            newMxRow.Item("Rx_sChiefComplaints") = _Medications.Item(i).Rx_ChiefComplaints
                            newMxRow.Item("Rx_sStatus") = _Medications.Item(i).Rx_Status
                            newMxRow.Item("Rx_sRxReferenceNumber") = _Medications.Item(i).Rx_RxReferenceNumber
                            newMxRow.Item("Rx_sRefillQualifier") = _Medications.Item(i).Rx_RefillQualifier
                            newMxRow.Item("Rx_nPharmacyId") = _Medications.Item(i).Rx_PharmacyId
                            newMxRow.Item("Rx_sNCPDPID") = _Medications.Item(i).Rx_NCPDPID
                            newMxRow.Item("Rx_nContactID") = _Medications.Item(i).Rx_ContactID
                            newMxRow.Item("Rx_sName") = _Medications.Item(i).Rx_PhName
                            newMxRow.Item("Rx_sAddressline1") = _Medications.Item(i).Rx_Addressline1
                            newMxRow.Item("Rx_sAddressline2") = _Medications.Item(i).Rx_Addressline2
                            newMxRow.Item("Rx_sCity") = _Medications.Item(i).Rx_City
                            newMxRow.Item("Rx_sState") = _Medications.Item(i).Rx_State
                            newMxRow.Item("Rx_sZip") = _Medications.Item(i).Rx_Zip
                            newMxRow.Item("Rx_sEmail") = _Medications.Item(i).Rx_Email
                            newMxRow.Item("Rx_sFax") = _Medications.Item(i).Rx_Fax
                            newMxRow.Item("Rx_sPhone") = _Medications.Item(i).Rx_Phone
                            newMxRow.Item("Rx_sServiceLevel") = _Medications.Item(i).Rx_ServiceLevel
                            newMxRow.Item("Rx_sPrescriberNotes") = _Medications.Item(i).Rx_PrescriberNotes
                            newMxRow.Item("Rx_eRxStatus") = _Medications.Item(i).Rx_eRxStatus
                            newMxRow.Item("Rx_eRxStatusMessage") = _Medications.Item(i).Rx_eRxStatusMessage
                            newMxRow.Item("sPBMSourceName") = _Medications.Item(i).PBMSourceName
                            newMxRow.Item("RxMed_DMSID") = _Medications.Item(i).RxMedDMSID
                            newMxRow.Item("Rx_IsCPOEOrder") = _Medications.Item(i).Rx_CPOEOrder
                            newMxRow.Item("sReasonConceptID") = _Medications.Item(i).ReasonConceptID
                            newMxRow.Item("sReasonConceptDesc") = _Medications.Item(i).ReasonConceptDesc

                            newMxRow.Item("sValuesetOID") = _Medications.Item(i).CQMCategories
                            newMxRow.Item("sValueSetName") = _Medications.Item(i).CQMDesc
                            newMxRow.Item("Rx_IsMedicationAdministered") = _Medications.Item(i).Rx_MedicationAdministered
                            newMxRow.Item("RowState") = _Medications.Item(i).State
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.ds.Tables("Medication").Rows.Add(newMxRow)
                            If Not IsNothing(newMxRow) Then
                                newMxRow = Nothing
                            End If
                        End If
                    End If
                Next

                Return True
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = ex.Message
                Return False
            Finally
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try

        End Function

        Public Function SaveMedication() As Boolean
            Dim _RxBusiness As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)
            Try
                If _Medications.Count > 0 Then
                    If SaveMedication(_CurrentVisitID, _CurrentVisitDate, TransactionMode, _Medications, m_FilterType) Then
                        TransactionMode = _TransactionMode.Edit
                        Medicationdate = _Medications.Item(0).Medicationdate
                        If IsNothing(_RxBusiness) = False Then
                            _RxBusiness.Dispose()
                            _RxBusiness = Nothing
                        End If
                        If Not IsNothing(_Medications) Then
                            _Medications.Clear()
                        End If
                        m_FilterType = "Active"
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
                Return Nothing
            Catch ex As Exception
                SaveMedication = Nothing
                Throw ex
            Finally
                If IsNothing(_RxBusiness) = False Then
                    _RxBusiness.Dispose()
                    _RxBusiness = Nothing
                End If
            End Try
        End Function

        Private Function FetchMedicationforUpdate(ByVal VisitId As Int64, ByVal m_FilterType As String, ByVal dtMedicationdate As DateTime, ByVal _Medications As Medications, Optional ByVal intflag As Int16 = 0) As Boolean
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim objParameter As DBParameter
            Dim _Medication As Medication
            Dim dt As New DataTable
            Try
                objParameter = New DBParameter
                objParameter.Value = VisitId
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = dtMedicationdate.Date
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@dtMedicationdate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = _PatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = m_FilterType
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@type"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                objParameter = New DBParameter
                objParameter.Value = intflag
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@flag"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                objParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_scanMedication")

                For i As Int16 = 0 To dt.Rows.Count - 1
                    _Medication = New Medication

                    _Medication.MedicationID = dt.Rows(i)(0)                    
                    _Medication.VisitID = dt.Rows(i)(1)
                    _Medication.PatientID = dt.Rows(i)(2)

                    _Medication.Medication = dt.Rows(i)(3)
                    _Medication.Dosage = dt.Rows(i)(4)
                    _Medication.Route = dt.Rows(i)(5)


                    _Medication.Frequency = dt.Rows(i)(6)
                    _Medication.Duration = dt.Rows(i)(7)
                    _Medication.Startdate = dt.Rows(i)(8)                    

                    If Not IsDBNull(dt.Rows(i)(9)) Then
                        _Medication.Enddate = dt.Rows(i)(9)
                        _Medication.CheckEndDate = True                        
                    Else
                        _Medication.CheckEndDate = False                        
                    End If

                    _Medication.Medicationdate = dt.Rows(i)(10)
                    _Medication.Amount = dt.Rows(i)(11)
                    _Medication.Status = dt.Rows(i)(12)                    

                    _Medication.Reason = dt.Rows(i)(13)
                    _Medication.mpid = dt.Rows(i)(14)
                    _Medication.UserName = dt.Rows(i)(15)                    

                    _Medication.UpdatedByUserName = dt.Rows(i)("UpdatedBy")
                    _Medication._PrescriptionId = dt.Rows(i)(16)
                    _Medication.Renewed = dt.Rows(i)(17)

                    If Not IsDBNull(dt.Rows(i)("NDCCode")) Then
                        _Medication.NDCCode = dt.Rows(i)("NDCCode")
                    Else
                        _Medication.NDCCode = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sRxNormCode")) Then
                        _Medication.RxNormCode = dt.Rows(i)("sRxNormCode")
                    Else
                        _Medication.RxNormCode = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sReasonConceptID")) Then
                        _Medication.ReasonConceptID = dt.Rows(i)("sReasonConceptID")
                    Else
                        _Medication.ReasonConceptID = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sReasonConceptDesc")) Then
                        _Medication.ReasonConceptDesc = dt.Rows(i)("sReasonConceptDesc")
                    Else
                        _Medication.ReasonConceptDesc = ""
                    End If
                    '------

                    If Not IsDBNull(dt.Rows(i)("sValuesetOID")) Then
                        _Medication.CQMCategories = dt.Rows(i)("sValuesetOID")
                    Else
                        _Medication.CQMCategories = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sValueSetName")) Then
                        _Medication.CQMDesc = dt.Rows(i)("sValueSetName")
                    Else
                        _Medication.CQMDesc = ""
                    End If


                    '----------------------



                    If Not IsDBNull(dt.Rows(i)("IsNarcotic")) Then
                        _Medication.IsNarcotics = dt.Rows(i)("IsNarcotic")
                    Else
                        _Medication.IsNarcotics = 0
                    End If
                    If Not IsDBNull(dt.Rows(i)("DrugForm")) Then
                        _Medication.DosageForm = dt.Rows(i)("DrugForm")
                    Else
                        _Medication.DosageForm = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("StrengthUnit")) Then
                        _Medication.StrengthUnit = dt.Rows(i)("StrengthUnit")
                    Else
                        _Medication.StrengthUnit = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("PBMSourceName")) Then
                        _Medication.PBMSourceName = dt.Rows(i)("PBMSourceName")
                    Else
                        _Medication.PBMSourceName = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_sRefills")) Then
                        _Medication.Rx_Refills = dt.Rows(i)("Rx_sRefills")
                    Else
                        _Medication.Rx_Refills = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_sNotes")) Then
                        _Medication.Rx_Notes = dt.Rows(i)("Rx_sNotes")
                    Else
                        _Medication.Rx_Notes = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_sMethod")) Then
                        _Medication.Rx_Method = dt.Rows(i)("Rx_sMethod")
                    Else
                        _Medication.Rx_Method = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_bMaySubstitute")) Then
                        _Medication.Rx_MaySubstitute = dt.Rows(i)("Rx_bMaySubstitute")
                    Else
                        _Medication.Rx_MaySubstitute = "True"
                    End If

                    _Medication.Rx_ProviderId = dt.Rows(i)("Rx_nProviderId")
                    _Medication.Rx_PharmacyId = dt.Rows(i)("Rx_nPharmacyId")

                    If Not IsDBNull(dt.Rows(i)("Rx_sNCPDPID")) Then
                        _Medication.Rx_NCPDPID = dt.Rows(i)("Rx_sNCPDPID")
                    Else
                        _Medication.Rx_NCPDPID = ""
                    End If

                    _Medication.Rx_ContactID = dt.Rows(i)("Rx_nContactID")                    

                    If Not IsDBNull(dt.Rows(i)("Rx_sName")) Then
                        _Medication.Rx_PhName = dt.Rows(i)("Rx_sName")
                    Else
                        _Medication.Rx_PhName = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_sPrescriberNotes")) Then
                        _Medication.Rx_PrescriberNotes = dt.Rows(i)("Rx_sPrescriberNotes")
                    Else
                        _Medication.Rx_PrescriberNotes = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("Rx_eRxStatus")) Then
                        _Medication.Rx_eRxStatus = dt.Rows(i)("Rx_eRxStatus")
                    Else
                        _Medication.Rx_eRxStatus = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("RxMed_DMSID")) Then
                        _Medication.RxMedDMSID = dt.Rows(i)("RxMed_DMSID")
                    Else
                        _Medication.RxMedDMSID = 0
                    End If

                    _Medication.Rx_CPOEOrder = dt.Rows(i)("Rx_IsCPOEOrder")                    
                    _Medication.Rx_MedicationAdministered = dt.Rows(i)("Rx_IsMedicationAdministered")                    

                    _Medication.State = "U"

                    _Medication.ItemNumber = _Medications.Count
                    _Medications.Add(_Medication)

                    If Not IsNothing(_Medication) Then
                        _Medication.Dispose()
                        _Medication = Nothing
                    End If
                Next

                Return True

            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
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

        Public Sub FetchMedicationforUpdate(Optional ByVal Flag As Boolean = False)
            Try
                If Flag = True Then
                    RaiseEvent MedicationSaveStatus(False)
                Else
                    FetchMedicationforUpdate(_CurrentVisitID, m_FilterType, _CurrentVisitDate, _Medications)
                    RaiseEvent RollRowsCount(m_FilterType)
                    RaiseEvent MedicationSaveStatus(True)
                End If
            Catch ex As Exception
            Finally

            End Try
        End Sub

        Public Function PopulateMedicationHistory(ByVal visitid As Long, ByVal intflag As Int32, ByVal visitdate As DateTime, ByVal m_filtertype As String) As Medications
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim _Medications As New Medications
            Dim _Medication As Medication
            Dim dt As New DataTable
            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID 'globalPatient.gnPatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = intflag
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Int
                _DBParameter.Name = "@flag"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = visitdate.Date
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtvisitdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = m_filtertype
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.VarChar
                _DBParameter.Size = 20
                _DBParameter.Name = "@type"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                If intflag = 2 Then
                    _DBParameter = New DBParameter
                    _DBParameter.Value = visitid
                    _DBParameter.Direction = ParameterDirection.Input
                    _DBParameter.DataType = SqlDbType.BigInt
                    _DBParameter.Name = "@visitid"
                    _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                    _DBParameter = Nothing
                End If
                dt = _gloEMRDatabase.GetDataTable("gsp_GetMedicationHistory")

                Dim i As Int16
                For i = 0 To dt.Rows.Count - 1
                    _Medication = New Medication
                    _Medication.VisitID = dt.Rows(i)(1)
                    _Medication.PatientID = dt.Rows(i)(2)
                    _Medication.Medication = dt.Rows(i)(3)
                    _Medication.Dosage = dt.Rows(i)(4)

                    If Not IsNothing(dt.Rows(i)(14)) Then
                        _Medication.mpid = CType(dt.Rows(i)(14), Int32)
                    End If

                    'CS#377103 integrated from 9000
                    'If _Medication.mpid <> 0 Then
                    '    Dim RoutesList As New List(Of String)
                    '    Using objPrescriptioLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                    '        RoutesList = objPrescriptioLayer.GetDrugRoutes(_Medication.mpid)
                    '    End Using

                    '    If RoutesList IsNot Nothing Then
                    '        If RoutesList.Count > 2 Then
                    '            _Medication.routes = RoutesList
                    '        End If
                    '        '        _Medication.Route = dt.Rows(i)(5)
                    '        '    Else
                    '        '        _Medication.Route = dt.Rows(i)(5)
                    '        '    End If
                    '        'Else
                    '    End If
                    'End If

                    _Medication.Route = dt.Rows(i)(5)

                    _Medication.Frequency = dt.Rows(i)(6)
                    _Medication.Duration = dt.Rows(i)(7)

                    If intflag = 2 Then
                        'previously we passed current date .i.e. Now.Date. 
                        'Now we pass the old start date. changed on 31 Oct'08 Friday.
                        _Medication.Startdate = dt.Rows(i)("dtStartDate")
                    Else
                        _Medication.Startdate = dt.Rows(i)(8)
                    End If


                    If Not IsDBNull(dt.Rows(i)(9)) Then
                        _Medication.Enddate = dt.Rows(i)(9)
                        _Medication.CheckEndDate = True
                    Else
                        _Medication.CheckEndDate = False
                    End If

                    _Medication.Medicationdate = dt.Rows(i)(10)
                    _Medication.Amount = dt.Rows(i)(11)

                    _Medication.Status = dt.Rows(i)(12)
                    _Medication.Reason = dt.Rows(i)(13)


                    'code added by sagar to add the user name on 23 may 2007
                    _Medication.UserName = dt.Rows(i)(15)
                    _Medication.UpdatedByUserName = dt.Rows(i)("UpdatedBy")
                    If Not IsDBNull(dt.Rows(i)(16)) Then
                        _Medication._PrescriptionId = dt.Rows(i)(16)
                    End If
                    If Not IsNothing(dt.Rows(i)(0)) Then
                        _Medication.MedicationID = CType(dt.Rows(i)(0), System.Int64)
                    Else
                        _Medication.MedicationID = 0
                    End If

                    _Medication.Renewed = dt.Rows(i)(17)

                    _Medication.NDCCode = dt.Rows(i)("NDCCode")

                    If Not IsDBNull(dt.Rows(i)("sRxNormCode")) Then
                        _Medication.RxNormCode = dt.Rows(i)("sRxNormCode")
                    End If

                    _Medication.ReasonConceptID = dt.Rows(i)("sReasonConceptID")
                    _Medication.ReasonConceptDesc = dt.Rows(i)("sReasonConceptDesc")


                    _Medication.CQMCategories = dt.Rows(i)("sValuesetOID")
                    _Medication.CQMDesc = dt.Rows(i)("sValueSetName")


                    _Medication.IsNarcotics = dt.Rows(i)("IsNarcotic")
                    _Medication.DosageForm = dt.Rows(i)("DrugForm")
                    _Medication.StrengthUnit = dt.Rows(i)("StrengthUnit")
                    _Medication.Rx_Method = dt.Rows(i)("sMethod")
                    _Medication.PBMSourceName = dt.Rows(i)("PBMName")

                    _Medication.Rx_MaySubstitute = dt.Rows(i)("Rx_bMaySubstitute") '''''''fixed issue 6111

                    _Medication.Rx_PhName = dt.Rows(i)("sName") '''''''fixed issue 6645
                    _Medication.Rx_Notes = dt.Rows(i)("sNotes") '''''''fixed issue 6645
                    _Medication.Rx_PrescriberNotes = dt.Rows(i)("sPrescriberNotes") '''''''fixed issue 6645

                    _Medication.RxMedDMSID = dt.Rows(i)("RxMed_DMSID") '''''''for CCHIT11 save DMSID

                    _Medication.Rx_CPOEOrder = dt.Rows(i)("CPOEOrder") '''''''for CCHIT11 save DMSID
                    'added new property used in TVP for saving medication
                    _Medication.Rx_MedicationAdministered = dt.Rows(i)("MedicationAdministered")
                    _Medication.Refills = dt.Rows(i)("sRefills") ''Incident #00020742--assign sRefills value for both medicatio refill properties
                    _Medication.Rx_Refills = dt.Rows(i)("sRefills") ''Incident #00020742--assign sRefills value for both medicatio refill properties

                    If intflag = 2 Then
                        If m_filtertype = "Unknown" Or m_filtertype = "All" Then
                            _Medication.State = "U"
                        Else
                            _Medication.State = "A"
                        End If
                    Else
                        _Medication.State = "U"
                    End If
                    _Medication.ItemNumber = _Medications.Count
                    _Medications.Add(_Medication)
                    _Medication = Nothing
                Next
                Return _Medications
            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Medication"
                Throw objex
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                'If Not IsNothing(_Medications) Then
                '    _Medications.Dispose()
                '    _Medications = Nothing
                'End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function GetVisitForHistory(ByVal visitid As Long, ByVal intflag As Int32, ByVal visitdate As DateTime, ByVal m_filtertype As String) As ArrayList
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim dt As DataTable = Nothing
            Dim arrlist As New ArrayList
            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID 'globalPatient.gnPatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = intflag
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Int
                _DBParameter.Name = "@flag"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = visitdate.Date
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtvisitdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = m_filtertype
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.VarChar
                _DBParameter.Size = 20
                _DBParameter.Name = "@type"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                If intflag = 2 Then
                    _DBParameter = New DBParameter
                    _DBParameter.Value = visitid
                    _DBParameter.Direction = ParameterDirection.Input
                    _DBParameter.DataType = SqlDbType.BigInt
                    _DBParameter.Name = "@visitid"
                    _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                    _DBParameter = Nothing
                End If

                dt = _gloEMRDatabase.GetDataTable("gsp_GetMedicationHistory")
                If dt.Rows.Count > 0 Then
                    arrlist.Add(dt.Rows(0)(0))
                    arrlist.Add(dt.Rows(0)(1))
                End If

                Return arrlist
            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Medication"
                Throw objex
            Finally
                If Not IsNothing(arrlist) Then
                    arrlist = Nothing
                End If
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

        Friend Function FetchDrugDetails(ByVal ArrDrugs As ArrayList, ByRef _Medications As Medications, ByVal _CurrentVisitID As Int64)
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim objParameter As DBParameter
            Dim _Medication As New Medication

            Try
                Dim i As Int16
                For i = 1 To ArrDrugs.Count
                    objParameter = New DBParameter
                    objParameter.Value = CType(ArrDrugs.Item(i), Int64)
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.DataType = SqlDbType.BigInt
                    objParameter.Name = "@DrugsID"
                    _gloEMRDatabase.DBParametersCol.Add(objParameter)
                    objParameter = Nothing

                    Dim dt As DataTable = Nothing
                    dt = _gloEMRDatabase.GetDataTable("gsp_scanDrugs_Mst")
                    _Medication = New Medication
                    _Medication.Medication = dt.Rows(0)(0)
                    _Medication.Dosage = dt.Rows(0)(2)
                    _Medication.Route = dt.Rows(0)(3)
                    _Medication.Frequency = dt.Rows(0)(4)
                    _Medication.Duration = dt.Rows(0)(5)
                    _Medication.Amount = dt.Rows(0)(7)
                    _Medication.VisitID = _CurrentVisitID
                    _Medication.Startdate = Now.Date
                    _Medication.MedicationID = 0
                    _Medication.DDID = dt.Rows(0)(10)
                    _Medication.UserName = globalSecurity.gstrLoginName

                    _Medication.NDCCode = dt.Rows(0)("NDCCode")
                    _Medication.IsNarcotics = dt.Rows(0)("IsNarcotics")
                    _Medication.DosageForm = dt.Rows(0)("DrugForm")
                    _Medication.StrengthUnit = dt.Rows(0)("DrugQtyQualifier")
                    _Medication.State = "A"
                    _Medication.ItemNumber = _Medications.Count
                    _Medications.Add(_Medication)
                    _Medication.Dispose()
                    _Medication = Nothing
                    dt.Dispose()
                    dt = Nothing
                Next
                Return _Medications
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Sub PopulateMedicationHistory(ByVal ArrDrugs As ArrayList, ByRef blncancel As Boolean, Optional ByVal formLock As Boolean = False, Optional ByVal ViewMode As Boolean = False)
            Dim _RxBusinesslayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)
            Dim tempvisitid As Int64
            Dim tempdate As DateTime
            Dim arrlist As New ArrayList
            Try
                If IsNothing(_Medications) Then
                    _Medications = New Medications
                End If
              
                arrlist = GetVisitForHistory(_CurrentVisitID, 3, _CurrentVisitDate, m_FilterType)
                If arrlist.Count > 0 Then
                    _CurrentVisitID = CType(arrlist.Item(0), Int64)
                    _PrevVisitIDforSupervisingProvider = _CurrentVisitID
                    _CurrentVisitDate = CType(arrlist.Item(1), Date)
                    _PastVisitID = _CurrentVisitID
                    _PastVisitDate = _CurrentVisitDate


                    If formLock = True Then
                        If ViewMode = True Then
                            RaiseEvent Recordlock(ViewMode)
                        Else
                            blncancel = False
                            Exit Sub
                        End If
                    End If
                    TransactionMode = _TransactionMode.Edit
                    m_FilterType = "Active"
                    _Medications.Dispose()
                    _Medications = PopulateMedicationHistory(_CurrentVisitID, 0, _CurrentVisitDate, m_FilterType)
                    If _Medications.Count > 0 Then
                        _Medicationdate = _Medications.Item(0).Medicationdate
                        If Not IsNothing(ArrDrugs) Then
                            If ArrDrugs.Count > 0 Then
                                FetchDrugDetails(ArrDrugs, _Medications, _CurrentVisitID)
                            End If
                        End If
                    Else
                        _Medicationdate = _CurrentVisitDate
                    End If
                    blncancel = True
                Else
                    If IsNothing(ArrDrugs) Then
                        arrlist = GetVisitForHistory(_CurrentVisitID, 1, _CurrentVisitDate, m_FilterType)
                        If arrlist.Count > 0 Then
                            tempvisitid = CType(arrlist.Item(0), Int64)
                            tempdate = CType(arrlist.Item(1), DateTime)

                            If formLock = True Then
                                If ViewMode = True Then
                                    RaiseEvent Recordlock(ViewMode)
                                Else
                                    blncancel = False
                                    Exit Sub
                                End If
                            End If

                            If formLock = False Then
                                RaiseEvent DisplayMessage(tempdate.Date)
                            End If

                            _Medications.Dispose()
                            m_FilterType = "Active"
                            _Medications = PopulateMedicationHistory(tempvisitid, 2, tempdate, m_FilterType)

                            TransactionMode = _TransactionMode.Add
                            If _Medications.Count > 0 Then
                                _PrevVisitIDforSupervisingProvider = tempvisitid
                                _PastVisitID = _CurrentVisitID
                                _PastVisitDate = _CurrentVisitDate
                                _Medicationdate = _CurrentVisitDate
                                blncancel = True
                                RaiseEvent LockWindowForUpdate()
                            End If
                        Else
                            If formLock = True Then
                                If ViewMode = True Then
                                    RaiseEvent Recordlock(ViewMode)
                                    _PastVisitID = _CurrentVisitID
                                    _PastVisitDate = _CurrentVisitDate
                                    _Medicationdate = _CurrentVisitDate
                                    blncancel = True
                                    TransactionMode = _TransactionMode.Add
                                    RaiseEvent LockWindowForUpdate()
                                Else
                                    blncancel = False
                                    Exit Sub
                                End If
                            Else
                                _PastVisitID = _CurrentVisitID
                                _PastVisitDate = _CurrentVisitDate
                                _Medicationdate = _CurrentVisitDate
                                blncancel = True
                                TransactionMode = _TransactionMode.Add
                                RaiseEvent LockWindowForUpdate()
                            End If
                        End If
                    Else

                        If formLock = True Then
                            If ViewMode = True Then
                                _CurrentVisitDate = Now.Date
                                _PastVisitID = _CurrentVisitID
                                _PastVisitDate = _CurrentVisitDate
                                _Medicationdate = _CurrentVisitDate
                                FetchDrugDetails(ArrDrugs, _Medications, _CurrentVisitID)
                                TransactionMode = _TransactionMode.Add
                                blncancel = True
                            Else
                                blncancel = False
                                Exit Sub
                            End If
                        Else
                            _CurrentVisitDate = Now.Date
                            _PastVisitID = _CurrentVisitID
                            _PastVisitDate = _CurrentVisitDate
                            _Medicationdate = _CurrentVisitDate
                            FetchDrugDetails(ArrDrugs, _Medications, _CurrentVisitID)
                            TransactionMode = _TransactionMode.Add
                            blncancel = True
                        End If
                    End If
                End If
                RaiseEvent LockWindowForUpdate()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Finally
                If Not IsNothing(arrlist) Then
                    arrlist = Nothing
                End If
                If Not IsNothing(_RxBusinesslayer) Then
                    _RxBusinesslayer.Dispose()
                    _RxBusinesslayer = Nothing
                End If

            End Try
        End Sub

        Public Sub FetchMedicationforUpdate(ByVal dtMedicationdate As DateTime, Optional ByVal intflag As Int16 = 0, Optional ByVal m_FilterType As String = Nothing)
            Dim _Medication As Medication = Nothing
            Try
                If IsNothing(_Medications) Then
                    _Medications = New Medications
                End If                
                If FetchMedicationforUpdate(_CurrentVisitID, m_FilterType, dtMedicationdate, _Medications, intflag) Then
                    If _Medications.Count > 0 Then

                        eTransactionMode = _TransactionMode.Edit
                        _Medication = _Medications.Item(0)
                        _CurrentVisitID = _Medication.VisitID
                        _Medicationdate = _Medication.Medicationdate

                        If _CurrentVisitID <> 0 Then

                            _CurrentVisitDate = FetchVisitDate(_CurrentVisitID)
                        End If
                    Else
                        If _CurrentVisitID <> 0 Then
                            _CurrentVisitDate = FetchVisitDate(_CurrentVisitID)
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error Updating Medication Details."
                Throw objex

            Finally
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
                
            End Try
        End Sub

        Private Function GetDosageformCode(ByVal NDCCode As String) As String
            Dim routeDescription As String = String.Empty
            Try
                Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                    routeDescription = oDIBGSHelper.GetDrugForm(NDCCode)
                End Using
            Catch
                routeDescription = ""
            End Try
            Return routeDescription
        End Function

        Private Function FetchDrugDetailsByDrugid(ByVal Drugid As Int64) As Medication
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim _Medication As Medication = Nothing
            Dim dt As DataTable = Nothing
            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = Drugid
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@DrugsID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_scanDrugs_Mst")
                _Medication = New Medication
                _Medication.Medication = dt.Rows(0)(0)
                _Medication.Dosage = dt.Rows(0)(2)
                _Medication.Route = dt.Rows(0)(3)
                _Medication.Frequency = dt.Rows(0)(4)
                _Medication.Duration = dt.Rows(0)(5)
                _Medication.Amount = dt.Rows(0)(7)
                _Medication.DDID = dt.Rows(0)(10)
                _Medication.UserName = globalSecurity.gstrLoginName

                _Medication.IsNarcotics = dt.Rows(0)("IsNarcotics")
                _Medication.NDCCode = dt.Rows(0)("NDCCode")

                If IsNothing(_Medication.DosageForm) Then
                    _Medication.DosageForm = dt.Rows(0)("DrugForm")
                ElseIf _Medication.DosageForm = "" Then
                    _Medication.DosageForm = dt.Rows(0)("DrugForm")
                End If
                If IsNothing(_Medication.DosageForm) Or _Medication.DosageForm = "" Then
                    _Medication.DosageForm = GetDosageformCode(_Medication.NDCCode)
                End If


                _Medication.Rx_DrugID = Drugid

                _Medication.Rx_MaySubstitute = True '''''''''''by default set this to true becaz when we will refill any drug that is added directly to medication then the by default value of maysubstitute flag will be true as done in prescription form
                _Medication.State = "A"
                Return _Medication
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function FetchDrugDetails(ByVal Drugid As Int64) As Boolean
             Dim _Medication As Medication = Nothing
            Try
                _Medication = FetchDrugDetailsByDrugid(Drugid)
                _Medication.Rx_CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                _Medication.MedicationID = 0
                _Medication.VisitID = _CurrentVisitID
                _Medication.Startdate = Now.Date
                If TransactionMode = _TransactionMode.Edit Then
                    _Medication.Medicationdate = _Medicationdate

                End If
                _Medication.ItemNumber = _Medications.Count
                _Medications.Add(_Medication)
                Return True

            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
                Return False
            Finally
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
                
            End Try
        End Function

        Friend Function FetchDrugDetailsBySig(ByVal NDCCode As String, Optional ByVal SIGid As Long = 0) As Medication
            Dim objMedication As New Medication
            Dim dt As DataTable = Nothing
            Try
                Dim _oRxbusinesLayer As New gloEMRPrescription.RxBusinesslayer(_PatientID)

                If SIGid = 0 Then
                    dt = _oRxbusinesLayer.RetriveDrugDetails(0, NDCCode)
                Else
                    dt = _oRxbusinesLayer.RetriveDrugDetailsBySigID(SIGid)
                End If
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then



                    objMedication.Rx_DrugID = dt.Rows(0)("nDrugsID")
                    objMedication.Medication = dt.Rows(0)("sDrugName")
                    objMedication.Dosage = dt.Rows(0)("sDosage")
                    objMedication.Route = dt.Rows(0)("sRoute")
                    objMedication.Frequency = dt.Rows(0)("sFrequency")
                    objMedication.Duration = dt.Rows(0)("sDuration")
                    objMedication.UserName = globalSecurity.gstrLoginName
                    objMedication.IsNarcotics = dt.Rows(0)("IsNarcotics")
                    objMedication.mpid = dt.Rows(0)("mpid")
                    objMedication.NDCCode = dt.Rows(0)("NDCCode")
                    objMedication.Amount = dt.Rows(0)("sAmount")

                    '''''''''''by default set this to true becaz when we will refill any drug that is added directly to medication then the by default value of maysubstitute flag will be true as done in prescription form
                    objMedication.Rx_MaySubstitute = True
                    objMedication.State = "A"
                    objMedication.Rx_MaySubstitute = True

                    If IsNothing(objMedication.DosageForm) Then
                        objMedication.DosageForm = dt.Rows(0)("DrugForm")
                    ElseIf objMedication.DosageForm = "" Then
                        objMedication.DosageForm = dt.Rows(0)("DrugForm")
                    End If

                    If IsNothing(objMedication.DosageForm) Or objMedication.DosageForm = "" Then
                        objMedication.DosageForm = GetDosageformCode(NDCCode) 'this will return the selected drug form i.e. solution, tablet etc....
                    End If
                End If

                Return objMedication
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching drug details."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(objMedication) Then
                    objMedication.Dispose()
                    objMedication = Nothing
                End If

            End Try
        End Function

        Public Sub AddNewMedication(ByVal Row As DataRow, ByVal Routes As List(Of String))
            Dim medication As New Medication()
            Try
                medication.Rx_DrugID = Row("nDrugsID")
                medication.Medication = Row("sDrugName")
                medication.Dosage = Row("sDosage")
                medication.routes = Routes

                If medication.routes IsNot Nothing Then
                    If medication.routes.Count <= 2 Then 'One BLANK entry added on first position
                        medication.Route = Row("sRoute")
                    End If
                Else
                    medication.Route = Row("sRoute")
                End If

                medication.Frequency = Row("sFrequency")
                medication.Duration = Row("sDuration")
                medication.UserName = globalSecurity.gstrLoginName
                medication.IsNarcotics = Row("IsNarcotics")
                medication.mpid = Row("mpid")
                medication.NDCCode = Row("NDCCode")
                medication.Amount = Row("sAmount")

                '''''''''''by default set this to true becaz when we will refill any drug that is added directly to medication then the by default value of maysubstitute flag will be true as done in prescription form
                medication.Rx_MaySubstitute = True
                medication.State = "A"
                medication.Rx_MaySubstitute = True

                If IsNothing(medication.DosageForm) Then
                    medication.DosageForm = Row("DrugForm")
                ElseIf medication.DosageForm = "" Then
                    medication.DosageForm = Row("DrugForm")
                End If

                If IsNothing(medication.DosageForm) Or medication.DosageForm = "" Then
                    medication.DosageForm = GetDosageformCode(Row("NDCCode"))
                End If

                medication.Rx_CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                medication.MedicationID = 0
                medication.VisitID = _CurrentVisitID
                medication.Startdate = Now.Date
                If TransactionMode = _TransactionMode.Edit Then
                    medication.Medicationdate = _Medicationdate
                End If
                medication.ItemNumber = _Medications.Count
                _Medications.Add(medication)

            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
            End Try
        End Sub

        Public Function FetchDrugDetails(ByVal NDCCode As String, Optional ByVal SIGid As Long = 0) As Boolean
            Dim _Medication As New Medication
            Try
                _Medication = FetchDrugDetailsBySig(NDCCode, SIGid)
                _Medication.Rx_CPOEOrder = GetProviderIDForUser(gloSureScript.gloSurescriptGeneral.gnLoginUserId)
                _Medication.MedicationID = 0
                _Medication.VisitID = _CurrentVisitID
                _Medication.Startdate = Now.Date
                If TransactionMode = _TransactionMode.Edit Then
                    _Medication.Medicationdate = _Medicationdate
                End If
                _Medication.ItemNumber = _Medications.Count
                _Medications.Add(_Medication)
                Return True

            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error fetching Drug Details."
                Throw objex
                Return False
            Finally
                If Not IsNothing(_Medication) Then
                    _Medication.Dispose()
                    _Medication = Nothing
                End If
            End Try
        End Function

        Public Function GetMedicationPrescriptionCount(ByVal VisitID As Long, ByVal gnThresholdSetting As Double) As Int64
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Dim _DBParameter As DBParameter
            Try

                _DBParameter = New DBParameter
                _DBParameter.Value = VisitID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nVisitId"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = gnThresholdSetting
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Float
                _DBParameter.Name = "@ThresholdValue"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                Dim Count As Integer

                Count = _gloEMRDatabase.GetDataValue("gsp_MedicationRxCount", True)

                If Count > 0 Then
                    Return Count
                Else
                    Return 0
                End If
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error Retrieving Data"
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
            Dim dt As DataTable = Nothing
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
                _DBParameter.Value = "M"
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


            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error Checking Record Count"
                CheckRecordCount = Nothing
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

        Public Function FetchMedicationforView(ByVal VisitId As Int64, ByVal dtMedicationdate As DateTime) As Medications
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim _Medications As New Medications
            Dim _Medication As Medication
            Dim dt As DataTable = Nothing
            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = VisitId
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = dtMedicationdate.Date
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtMedicationdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                dt = _gloEMRDatabase.GetDataTable("gsp_scanMedication")

                For i As Int16 = 0 To dt.Rows.Count - 1
                    _Medication = New Medication
                    _Medication.MedicationID = dt.Rows(i)(0)
                    _Medication.Medication = dt.Rows(i)(1)
                    _Medication.Dosage = dt.Rows(i)(2)
                    _Medication.Route = dt.Rows(i)(3)
                    _Medication.Frequency = dt.Rows(i)(4)
                    _Medication.Duration = dt.Rows(i)(5)
                    If (dt.Rows(i).IsNull(6)) Then
                        _Medication.Enddate = Now.Date
                    Else
                        _Medication.Enddate = dt.Rows(i)(6)
                    End If
                    _Medication.Amount = dt.Rows(i)(7)
                    _Medication.Status = dt.Rows(i)(8)
                    _Medication.Reason = dt.Rows(i)(9)
                    _Medication._PrescriptionId = dt.Rows(i)(10)
                    _Medication.Renewed = dt.Rows(i)(11)

                    If Not IsDBNull(dt.Rows(i)("sReasonConceptID")) Then
                        _Medication.ReasonConceptID = dt.Rows(i)("sReasonConceptID")
                    Else
                        _Medication.ReasonConceptID = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sReasonConceptDesc")) Then
                        _Medication.ReasonConceptDesc = dt.Rows(i)("sReasonConceptDesc")
                    Else
                        _Medication.ReasonConceptDesc = ""
                    End If
                    '---------
                    If Not IsDBNull(dt.Rows(i)("sValuesetOID")) Then
                        _Medication.CQMCategories = dt.Rows(i)("sValuesetOID")
                    Else
                        _Medication.CQMCategories = ""
                    End If

                    If Not IsDBNull(dt.Rows(i)("sValueSetName")) Then
                        _Medication.CQMDesc = dt.Rows(i)("sValueSetName")
                    Else
                        _Medication.CQMDesc = ""
                    End If

                    '---------
                    _Medication.ItemNumber = _Medications.Count
                    _Medications.Add(_Medication)
                    _Medication = Nothing
                Next
                Return _Medications
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching prescription for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(_Medications) Then
                    _Medications.Dispose()
                    _Medications = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function FillMedication(ByVal strflag As Char) As Medications
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim objParameter As DBParameter
            Dim _Medications As New Medications
            Dim _Medication As Medication
            Dim dt As DataTable = Nothing
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

                dt = _gloEMRDatabase.GetDataTable("gsp_ViewMedication")

                For i As Int16 = 0 To dt.Rows.Count - 1
                    _Medication = New Medication

                    _Medication.VisitID = dt.Rows(i)(0)
                    _Medication.Medicationdate = dt.Rows(i)(1)
                    _Medication.ItemNumber = _Medications.Count
                    _Medications.Add(_Medication)
                    _Medication = Nothing
                Next
                Return _Medications
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching Medication for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                _Medication = Nothing
                _Medications = Nothing

                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function FetchMedicationforView(ByVal VisitId As Int64) As DataTable
            Dim _gloEMRDatabase As New DataBaseLayer
            Dim _DBParameter As DBParameter
            Dim oDt As DataTable = Nothing

            Try
                _DBParameter = New DBParameter
                _DBParameter.Value = VisitId
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nVisitID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = Now.Date
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.DateTime
                _DBParameter.Name = "@dtMedicationdate"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = _PatientID 'globalPatient.gnPatientID
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.BigInt
                _DBParameter.Name = "@nPatientID"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                _DBParameter = New DBParameter
                _DBParameter.Value = 2
                _DBParameter.Direction = ParameterDirection.Input
                _DBParameter.DataType = SqlDbType.Int
                _DBParameter.Name = "@flag"
                _gloEMRDatabase.DBParametersCol.Add(_DBParameter)
                _DBParameter = Nothing

                oDt = _gloEMRDatabase.GetDataTable("gsp_scanMedication")
                Return oDt
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error while fetching Medication for viewing."
                Throw objex
                Return Nothing
            Finally
                If Not IsNothing(oDt) Then
                    oDt.Dispose()
                    oDt = Nothing
                End If
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
            End Try
        End Function

        Public Function getServerTime(ByVal id As Int64) As DataTable
            Dim dt As DataTable = Nothing
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            Try
                Dim strquery As String = "Select dbo.gloGetDate(),dtprescriptiondate from Prescription where nprescriptionid= " & id
                dt = _gloEMRDatabase.GetDataTable_Query(strquery)

                Return dt
            Catch ex As Exception
                Dim objex As New MedicationDatabaseLayerException
                objex.ErrMessage = "Error Retrieving PrescriptionDate"
                Throw objex
            Finally
                If Not IsNothing(_gloEMRDatabase) Then
                    _gloEMRDatabase.Dispose()
                    _gloEMRDatabase = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
            End Try
        End Function

        Public Function GetPharmacyDetails(ByVal _Pharmacyid As Int64) As DataTable
            Dim _oRxbusinesslayer As New gloEMRPrescription.RxBusinesslayer(_PatientID)
            Dim dtPhDetails As DataTable = Nothing
            Try
                dtPhDetails = _oRxbusinesslayer.GetPharmacyDetails(_Pharmacyid)
                Return dtPhDetails
            Catch ex As Exception
                Dim objex As New MedicationBusinessLayerException
                objex.ErrMessage = "Error Fetching Current Medical Conditions for the patient"
                Throw objex
            Finally

                If Not IsNothing(dtPhDetails) Then
                    dtPhDetails.Dispose()
                    dtPhDetails = Nothing
                End If
                If Not IsNothing(_oRxbusinesslayer) Then
                    _oRxbusinesslayer.Dispose()
                    _oRxbusinesslayer = Nothing
                End If
            End Try

        End Function

        Public Function CheckIsCPOEProvider(ByVal UserID As Int64) As Boolean
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
                        dt.Dispose()
                        dt = Nothing
                    End If
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

        Public Function GetProviderIDForUser(ByVal UserID As Int64) As Boolean
            Dim oDB As New DataBaseLayer
            Dim IsCPOEProvider As Boolean = False
            Try
                IsCPOEProvider = CheckIsCPOEProvider(UserID)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                'ProID = 0
            Finally
                oDB.Dispose()
            End Try
            Return IsCPOEProvider
            'If ProID > 0 Then
            '    Return False
            'Else
            '    Return True
            'End If
        End Function

    End Class

    Public Class MedicationBusinessLayerException
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
    End Class
    Public Class MedicationDatabaseLayerException
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
    End Class
End Namespace
