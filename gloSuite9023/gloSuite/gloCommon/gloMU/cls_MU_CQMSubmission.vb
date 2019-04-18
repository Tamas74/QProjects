Imports System.Xml
Imports System.IO


Public Class cls_MU_CQMSubmission

    Private _CreateDate As String
    Public Property CreateDate() As String
        Get
            Return _CreateDate
        End Get
        Set(ByVal value As String)
            _CreateDate = value
        End Set
    End Property


    Private _Createtime As String
    Public Property CreateTime() As String
        Get
            Return _Createtime
        End Get
        Set(ByVal value As String)
            _Createtime = value
        End Set
    End Property

    Private _CreateBy As String
    Public Property CreateBy() As String
        Get
            Return _CreateBy
        End Get
        Set(ByVal value As String)
            _CreateBy = value
        End Set
    End Property

    Private _Version As String
    Public Property Version() As String
        Get
            Return _Version
        End Get
        Set(ByVal value As String)
            _Version = value
        End Set
    End Property

    Private _FileNumber As Int64
    Public Property FileNumber() As Int64
        Get
            Return _FileNumber
        End Get
        Set(ByVal value As Int64)
            _FileNumber = value
        End Set
    End Property

    Private _NumberOfFiles As Int64
    Public Property NumberOfFiles() As Int64
        Get
            Return _NumberOfFiles
        End Get
        Set(ByVal value As Int64)
            _NumberOfFiles = value
        End Set
    End Property



    Private _RegistryName As String
    Public Property RegistryName() As String
        Get
            Return _RegistryName
        End Get
        Set(ByVal value As String)
            _RegistryName = value
        End Set
    End Property

    Private _RegistryID As Int64
    Public Property RegistryID() As Int64
        Get
            Return _RegistryID
        End Get
        Set(ByVal value As Int64)
            _RegistryID = value
        End Set
    End Property

    Private _SubmissionMethod As String
    Public Property SubmissionMethod() As String
        Get
            Return _SubmissionMethod
        End Get
        Set(ByVal value As String)
            _SubmissionMethod = value
        End Set
    End Property


    Private _MeasureGroupID As String
    Public Property MeasureGroupID() As String
        Get
            Return _MeasureGroupID
        End Get
        Set(ByVal value As String)
            _MeasureGroupID = value
        End Set
    End Property

    Private _NPI As String
    Public Property NPI() As String
        Get
            Return _NPI
        End Get
        Set(ByVal value As String)
            _NPI = value
        End Set
    End Property

    Private _TIN As String
    Public Property TIN() As String
        Get
            Return _TIN
        End Get
        Set(ByVal value As String)
            _TIN = value
        End Set
    End Property

    Private _WaiverSign As String
    Public Property WaiverSign() As String
        Get
            Return _WaiverSign
        End Get
        Set(ByVal value As String)
            _WaiverSign = value
        End Set
    End Property


    Private _EnCounterFromDate As String
    Public Property EnCounterFromDate() As String
        Get
            Return _EnCounterFromDate
        End Get
        Set(ByVal value As String)
            _EnCounterFromDate = value
        End Set
    End Property

    Private _EnCounterToDate As String
    Public Property EnCounterToDate() As String
        Get
            Return _EnCounterToDate
        End Get
        Set(ByVal value As String)
            _EnCounterToDate = value
        End Set
    End Property

    Private _FilePath As String
    Public Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

    Private _Measures As Measures
    Public Property Measures() As Measures
        Get
            Return _Measures
        End Get
        Set(ByVal value As Measures)
            _Measures = value
        End Set
    End Property


End Class


Public Class Measures
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

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
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As Measure
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Measure)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oMeasure As Measure)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oMeasure)
    End Sub
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class


Public Class Measure
    Private _PQRIMEasureNumber As Int64
    Public Property PQRIMEasureNumber() As Int64
        Get
            Return _PQRIMEasureNumber
        End Get
        Set(ByVal value As Int64)
            _PQRIMEasureNumber = value
        End Set
    End Property

    Private _EligibleInstances As Int64
    Public Property EligibleInstances() As Int64
        Get
            Return _EligibleInstances
        End Get
        Set(ByVal value As Int64)
            _EligibleInstances = value
        End Set
    End Property

    Private _MeetPerformanceInstances As Int64
    Public Property MeetPerformanceInstances() As Int64
        Get
            Return _MeetPerformanceInstances
        End Get
        Set(ByVal value As Int64)
            _MeetPerformanceInstances = value
        End Set
    End Property

    Private _PerformanceExclusionInstances As Int64
    Public Property PerformanceExclusionInstances() As Int64
        Get
            Return _PerformanceExclusionInstances
        End Get
        Set(ByVal value As Int64)
            _PerformanceExclusionInstances = value
        End Set
    End Property

    Private _PerformanceNotMetInstances As Int64
    Public Property PerformanceNotMetInstances() As Int64
        Get
            Return _PerformanceNotMetInstances
        End Get
        Set(ByVal value As Int64)
            _PerformanceNotMetInstances = value
        End Set
    End Property


    Private _ReportingRate As String
    Public Property ReportingRate() As String
        Get
            Return _ReportingRate
        End Get
        Set(ByVal value As String)
            _ReportingRate = value
        End Set
    End Property

    Private _PerformanceRate As String
    Public Property PerformanceRate() As String
        Get
            Return _PerformanceRate
        End Get
        Set(ByVal value As String)
            _PerformanceRate = value
        End Set
    End Property

End Class
