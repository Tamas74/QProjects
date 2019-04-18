Imports System
Imports System.IO


Namespace gloEARSections

#Region "class PendingEAR & Collection class"

    Public Class ClsPendingEAR
        Implements IDisposable

        '\\ m_nReportID, sReportFile, dtRptGeneratedDate, dtStartDate, dtEndDate, nNoOfAttempts, sStatus, sErrorCode
        Private m_sReportFile As String = ""
        Private m_dtRptGeneratedDate As DateTime
        Private m_dtStartDate As DateTime
        Private m_dtEndDate As DateTime
        Private m_nNoOfAttempts As Int32 = 0
        Private m_sStatus As String = ""
        Private m_sErrorCode As String = ""

        Public Property ReportFile() As String
            Get
                Return m_sReportFile
            End Get
            Set(ByVal value As String)
                m_sReportFile = value
            End Set
        End Property
        Public Property RptGeneratedDate() As DateTime
            Get
                Return m_dtRptGeneratedDate
            End Get
            Set(ByVal value As DateTime)
                m_dtRptGeneratedDate = value
            End Set
        End Property
        Public Property StartDate() As DateTime
            Get
                Return m_dtStartDate
            End Get
            Set(ByVal value As DateTime)
                m_dtStartDate = value
            End Set
        End Property
        Public Property EndDate() As DateTime
            Get
                Return m_dtEndDate
            End Get
            Set(ByVal value As DateTime)
                m_dtEndDate = value
            End Set
        End Property
        Public Property NoOfAttempt() As Int32
            Get
                Return m_nNoOfAttempts
            End Get
            Set(ByVal value As Int32)
                m_nNoOfAttempts = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return m_sStatus
            End Get
            Set(ByVal value As String)
                m_sStatus = value
            End Set
        End Property
        Public Property ErrorCode() As String
            Get
                Return m_sErrorCode
            End Get
            Set(ByVal value As String)
                m_sErrorCode = value
            End Set
        End Property

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

    End Class

    '    Public Class ClsClientCollection
    '        Implements IDisposable

    '        Protected _innerlist As ArrayList


    '        Public Sub New()
    '            _innerlist = New ArrayList()
    '        End Sub

    '        Private disposedValue As Boolean = False        ' To detect redundant calls

    '        ' IDisposable
    '        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    '            If Not Me.disposedValue Then
    '                If disposing Then
    '                    ' TODO: free managed resources when explicitly called
    '                End If

    '                ' TODO: free shared unmanaged resources
    '            End If
    '            Me.disposedValue = True
    '        End Sub

    '#Region " IDisposable Support "
    '        ' This code added by Visual Basic to correctly implement the disposable pattern.
    '        Public Sub Dispose() Implements IDisposable.Dispose
    '            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '            Dispose(True)
    '            GC.SuppressFinalize(Me)
    '        End Sub
    '#End Region

    '        Public ReadOnly Property Count() As Integer
    '            Get
    '                If Not IsNothing(_innerlist) Then
    '                    Return _innerlist.Count
    '                Else
    '                    Return 0
    '                End If

    '            End Get
    '        End Property

    '        Public Sub Add(ByVal item As ClsClients)
    '            If Not IsNothing(_innerlist) Then
    '                _innerlist.Add(item)
    '            End If
    '        End Sub

    '        Public Function Add(ByVal SendingApplicationName As String)
    '            Dim item As New ClsClients(SendingApplicationName)
    '            _innerlist.Add(item)
    '        End Function

    '        Public Function Add(ByVal SendingApplicationName As String, ByVal SendingApplicationFacility As String, ByVal sAppField As String)
    '            Dim item As New ClsClients(SendingApplicationName, SendingApplicationName, SendingApplicationFacility, sAppField)
    '            _innerlist.Add(item)
    '        End Function

    '        Public Sub Insert(ByVal index As Integer, ByVal item As ClsClients)
    '            _innerlist.Insert(index, item)
    '        End Sub

    '        Public Function Remove(ByVal item As ClsClients) As Boolean
    '            Dim result As Boolean = False
    '            Dim obj As ClsClients

    '            For i As Integer = 0 To _innerlist.Count - 1
    '                'store current index being checked 
    '                obj = New ClsClients()
    '                obj = CType(_innerlist(i), ClsClients)
    '                If obj.SendingApplicationName = item.SendingApplicationName AndAlso obj.SendingApplicationFacility = item.SendingApplicationFacility Then
    '                    _innerlist.RemoveAt(i)
    '                    result = True
    '                    Exit For
    '                End If
    '                obj = Nothing
    '            Next

    '            Return result
    '        End Function

    '        Public Function RemoveAt(ByVal index As Integer) As Boolean
    '            Dim result As Boolean = False
    '            _innerlist.RemoveAt(index)
    '            result = True
    '            Return result
    '        End Function

    '        Public Sub Clear()
    '            _innerlist.Clear()
    '        End Sub

    '        Default Public ReadOnly Property Item(ByVal index As Integer) As ClsClients
    '            Get
    '                Return CType(_innerlist(index), ClsClients)
    '            End Get
    '        End Property

    '        Public Function Contains(ByVal item As ClsClients) As Boolean
    '            Return _innerlist.Contains(item)
    '        End Function

    '        Public Function IndexOf(ByVal item As ClsClients) As Integer
    '            Return _innerlist.IndexOf(item)
    '        End Function

    '        Public Sub CopyTo(ByVal array As ClsClients(), ByVal index As Integer)
    '            _innerlist.CopyTo(array, index)
    '        End Sub

    '    End Class

    Public Class ClsPendingEARCollection
        Implements IDisposable
        Protected _innerlist As ArrayList

        Public Sub New()
            _innerlist = New ArrayList()
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                If Not IsNothing(_innerlist) Then
                    Return _innerlist.Count
                Else
                    Return 0
                End If
            End Get
        End Property

        Public Sub Add(ByVal item As ClsPendingEAR)
            If Not IsNothing(_innerlist) Then
                _innerlist.Add(item)
            End If
        End Sub

        Public Sub Insert(ByVal index As Integer, ByVal item As ClsPendingEAR)
            _innerlist.Insert(index, item)
        End Sub

        Public Function Remove(ByVal item As ClsPendingEAR) As Boolean
            Dim result As Boolean = False
            Dim obj As ClsPendingEAR

            For i As Integer = 0 To _innerlist.Count - 1
                'store current index being checked 
                obj = New ClsPendingEAR()
                obj = CType(_innerlist(i), ClsPendingEAR)
                If obj.ReportFile = item.ReportFile Then
                    _innerlist.RemoveAt(i)
                    result = True
                    Exit For
                End If
                obj = Nothing
            Next

            Return result
        End Function

        Public Function RemoveAt(ByVal index As Integer) As Boolean
            Dim result As Boolean = False
            _innerlist.RemoveAt(index)
            result = True
            Return result
        End Function

        Public Sub Clear()
            _innerlist.Clear()
        End Sub

        Default Public ReadOnly Property Item(ByVal index As Integer) As ClsPendingEAR
            Get
                Return CType(_innerlist(index), ClsPendingEAR)
            End Get
        End Property

        Public Function Contains(ByVal item As ClsPendingEAR) As Boolean
            Return _innerlist.Contains(item)
        End Function

        Public Function IndexOf(ByVal item As ClsPendingEAR) As Integer
            Return _innerlist.IndexOf(item)
        End Function

        Public Sub CopyTo(ByVal array As ClsPendingEAR(), ByVal index As Integer)
            _innerlist.CopyTo(array, index)
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

    End Class

#End Region



    'Public Class gloEAR
    '    Public ReadOnly Property ExamNewDocumentName() As String
    '        Get
    '            Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
    '            Dim _NewDocumentName As String = ""
    '            Dim _Extension As String = ".docx"
    '            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

    '            Dim i As Integer = 0
    '            _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _Extension
    '            While File.Exists(_Path & "\" & _NewDocumentName) = True
    '                i = i + 1
    '                _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _Extension
    '            End While
    '            Return _Path & "\" & _NewDocumentName
    '        End Get
    '    End Property
    'End Class



    Public Class gloEARFileHeader
        Implements IDisposable

#Region "Public Variables"
        'date format required
        'yyyymmdd

        '############################################################################
        'MANDATORY FIELDS
        '############################################################################

        'Identifies record type
        Public RecordType As String = "HDR"

        'Version number of the Specification
        Public Version As String = "10"

        'ID as assigned by RxHub identifying Participant sending the file. From a
        'POC, this contains the POC’s participant ID. To the PBMs, this would contain RxHub’s Participant ID
        Public SenderID As String = ""

        'Password for this Participant identified in field 3 (Sender ID).
        Public SenderParticipantPwd As String = ""

        'ID identifying the receiver of the report file. If the sender is the POC system, 
        'the receiver is RXHUB. If the sender of the report is RXHUB, the receiver ID 
        'indicates a Payer/PBM
        Public ReceiverID As String = "RXHUB"

        ' Name of the source providing the report data(If POC to RxHub, the source name is the POC system name
        'If RxHub to payer/PBM this field is not filled in.) "SAMPLEPOC"
        Public SourceName As String = ""

        'Unique identifier defined by the sender
        Public TransmissionControlNumber As String = ""

        'date when transaction was created (D*-CCYYMMDD)
        Public TransmissionDate As String = ""
        'time when transaction was created (HHMMSSDD)
        Public TransmissionTime As String = ""

        'identifier telling the type of transaction ex., EAR=> ePrescribing Activity Report
        Public TransmissionFileType As String = "EAR"

        'Filetype T=> Test and P=>Production
        Public FileType As String = "T"

        ' date the file was extracted from the publisher's system
        Public ExtractDate As String = ""

        '############################################################################
        'OPTIONAL FIELDS (whose values will be sent as blank)
        '############################################################################

        Public TransmissionAction As String = ""

#End Region

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


    End Class

    Public Class gloEARReportHeader
        Implements IDisposable

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


#Region "Public Variables"
        '############################################################################
        'MANDATORY FIELDS
        '############################################################################

        'Record type of file – eprescribing list header = RHD
        Public RecordType As String = "RHD"

        'CCYYMMDD The report must start on a Sunday. (20060920)
        Public ReportStartDate As String = ""

        'CCYYMMDD The report must end on a Saturday.
        Public ReportEndDate As String = ""

        'This fields identifies whether this is the first transmission of a report or a  retransmission.
        '(N=New Report R=Retransmission)
        Public TransmissionAction As String = ""

#End Region

    End Class

    Public Class gloEARReportDetailAggregate
        Implements IDisposable

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

#Region "Public variables"

        '############################################################################
        'MANDATORY FIELDS
        '############################################################################

        Public RecordType As String = "ARA"

        'This contains the Participant ID of the source of the data, the sending POC. 
        Public SourceParticipantID As String = ""

        'Contains the participant ID of the ultimate destination of the data. For the 
        'aggregate records this will either be the Participant ID of RxHub or a PBM.
        Public DestinationParticipantID As String = ""

        'At least one of the three prescriber ids are required (DEA,NPI, State License#). DEA 
        'or NPI (as it is available) are preferred.
        Public PrescriberDEA As String = ""

        Public PrescriberNPI As String = ""

        Public PrescriberStateLicenseNumber As String = ""

        'Required if State License # is provided. (C)
        Public PrescriberState As String = ""

        'Required if State License # is provided. (C)
        Public PrescriberZIPCODE As String = ""



        'CCYYMMDD. Must be within the start and end report date of the header.
        Public PrescriptionDate As String = ""

        'Number of electronically routed prescriptions written by this prescriber for this aggregate group
        'during the report period. RxHub routed prescriptions should not be included in this count.
        Public ElectronicPrescriptionCount As String = 0

        'Number of fax routed  prescriptions written  by this prescriber for  this aggregate group
        'during the report period.
        Public FaxPrescriptionCount As String = ""

        'Number of printed prescriptions written by this prescriber for this aggregate group 
        'during the report period
        Public PrintedPrescriptionCount As String = ""

        'Number of prescriptions routed through RxHub by this prescriber for this aggregate group
        'during the report period
        Public RxHubRoutedPrescriptionCount As String = ""

        '############################################################################
        'OPTIONAL FIELDS
        '############################################################################

        '  Aggregate Record prescriber ID. Value is established by the Technology Vendor.
        'This should not be related to any known identifier like DEA, NPI or state ID.
        'Used for RxHub consolidated reports. If prescriber wants their data deidentified
        'they should use this field.
        Public PrescriberConfidentialIdentifier As String = ""

#End Region

    End Class

    Public Class gloEARReportDetailAggregates
        Inherits gloBaseCollection

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a gloEARReportDetailAggregate object.
        Public ReadOnly Property Item(ByVal index As Integer) As gloEARReportDetailAggregate
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), gloEARReportDetailAggregate)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _RptAggregate As gloEARReportDetailAggregate)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_RptAggregate)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub

        Public Sub New()

        End Sub

    End Class


    Public Class gloEARReportDetailRxLevel
        Implements IDisposable

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

#Region "Public variables"



        Public RecordType As String = "ARD"
        Public SourceParticipantID As String = ""
        Public DestinationParticipantId As String = ""
        Public PrescriberDEA As String = ""
        Public PrescriberNPI As String = ""
        Public PrescriberStateLicenceNumber As String = ""
        Public PrescriberState As String = ""
        Public PrescriberZIPCODE As String = ""

        '///////
        Public PrescriberConfidentialIdentifier As String = ""
        '////////////

        Public PrescriptionDate As String = ""
        Public PrescriberFirstName As String = ""
        Public PrescriberLastName As String = ""

        '//This data will come from the rx_271 tables.
        Public HealthPlanID As String = ""
        Public HealthPlanGroupID As String = ""

        '//this id would come from the Prescription table
        Public PrescriptionID As String = ""

        '//This data will come from the rx_271 tables.
        Public FormularyID As String = ""
        Public AlternativesID As String = ""
        Public CoverageID As String = ""
        Public CopayID As String = ""

        '//Will come from the RxH_eRxFormularyDetails table
        Public FormularyStatus As String = ""
        Public FlatCoPayAmount As String = ""
        Public PercentCoPayRate As String = ""
        Public FirstCoPayTerm As String = ""
        Public CoPayTier As String = ""

        Public PrescribedAgeLimitCoverageStatus As String = ""
        Public PrescribedDrugExclusionCoverageStatus As String = ""
        Public PrescribedGenderLimitCoverageStatus As String = ""
        Public PrescribedMedicalNecessityCoverageStatus As String = ""
        Public PrescribedPriorAuthorizationCoverageStatus As String = ""
        Public PrescribedQuantityLimitCoverageStatus As String = ""
        Public PrescribedDrugSpecificResourceLinkCoverageStatus As String = ""
        Public PrescribedSummaryLevelResourceLinkCoverageStatus As String = ""
        Public PrescribedStepMedicationCoverageStatus As String = ""
        Public PrescribedStepTherapyCoverageStatus As String = ""
        Public PrescribedTextMessageCoverageStatus As String = ""
        Public PrescribedNDCCode As String = ""
        Public PrescribedRXNORMCode As String = ""
        Public PrescribedDrugName As String = ""
        Public PrescribedDrugStrength As String = ""
        Public PrescribedDosageForm As String = ""
        Public PrescribedQuantity As String = ""
        Public PrescribedDrugType As String = ""
        Public PrescribedRefills As String = ""
        Public DispenseasWritten As String = ""
        Public MailOrderBenefitUtilized As String = ""
        Public Initiative As String = ""
        Public Platform As String = ""
        Public PrescriptionDeliveryMethod As String = ""
        Public DURIndicator As String = ""
        Public OriginalScriptInitialFormularyStatus As String = ""
        Public OriginalScriptFlatCopayAmount As String = ""
        Public OriginalScriptCopayRate As String = ""
        Public OriginalScriptFirstCopayTerm As String = ""
        Public OriginalScriptCopayTier As String = ""
        Public OriginalScriptNDC As String = ""
        Public OriginalScriptRxNorm As String = ""
        Public OriginalScriptDrugName As String = ""
        Public OriginalScriptCoverageIndicator As String = ""
        Public OriginalScriptTextMessageDisplayed As String = ""
        Public OriginalScriptResourceLinkDisplayed As String = ""

        '///////////
#End Region


    End Class

    Public Class gloEARReportDetailRxLevels
        Inherits gloBaseCollection
        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As gloEARReportDetailRxLevel
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), gloEARReportDetailRxLevel)
            End Get
        End Property
        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _RptDetailRxLevel As gloEARReportDetailRxLevel)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_RptDetailRxLevel)
        End Sub
        Public Overrides Sub Dispose()
            Me.Clear()
        End Sub

        Public Sub New()

        End Sub
    End Class

    Public Class gloEARReportTrailer
        Implements IDisposable

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


        Public RecordType As String = "RTR"

        'Total records in the list. Do not include the list header and trailer records 
        'in this count.
        Public TotalRecords As String = ""
    End Class

    Public Class gloEARFileTrailer
        Implements IDisposable

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


        Public RecordType As String = "TRL"

        'Total records in file. Do not include the file header and trailer in this count. 
        'Count should be the total records in file minus 2.
        Public TotalRecords As String = ""

    End Class

    Public Class gloBaseCollection
        Inherits CollectionBase
        'Remove Item at specified index
        Public Sub Remove(ByVal index As Integer)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no object exists, a messagebox is shown and the operation is 
                ' cancelled.
                'System.Windows.Forms.MessageBox.Show("Index not valid!")
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub

        Public Overridable Sub Dispose()
            Me.Clear()
        End Sub

    End Class

End Namespace


