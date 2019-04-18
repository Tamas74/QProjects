Imports System.Data
Imports System.Data.SqlClient

Public Class Cls_Echocardiogram
    Implements IDisposable
    Private disposedValue As Boolean = False
    Private _npatientID As Int64 =0
    Private _dtprocdate As Date = Nothing
    Private _LVDiastolic As String = ""
    Private _scptcode As String = ""
    Private _sprocedures As String = ""

    Private _aortic As String = ""
    Private _mitral As String = ""
    Private _laarea As String = ""
    Private _avarea As String = ""
    Private _mvarea As String = ""

    Private _lvsystvol As String = ""
    Private _lvmass As String = ""
    Private _IDofinterpatphys As String = ""
    Private _Narrativesummary As String = ""
    Private _lvedd As String = ""
    Private _lvesdvc As String = ""
    Private _lvpostwallthik As String = ""
    Private _septalthik As String = ""
    Private _ExamId As Int64 = 0
    Private _VisitID As Int64 = 0

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



    Public Property ExamID() As Int64
        Get
            Return _ExamId
        End Get
        Set(ByVal value As Int64)
            _ExamId = value
        End Set
    End Property

    Public Property VisitID() As Int64
        Get
            Return _VisitID
        End Get
        Set(ByVal value As Int64)
            _VisitID = value
        End Set
    End Property

    Public Property nPatientID() As Int64
        Get
            Return _npatientID
        End Get
        Set(ByVal value As Int64)
            _npatientID = value
        End Set
    End Property


    Public Property dtprocdate() As Date
        Get
            Return _dtprocdate
        End Get
        Set(ByVal value As Date)
            _dtprocdate = value
        End Set
    End Property


    Public Property LVDiastolic() As String
        Get
            Return _LVDiastolic
        End Get
        Set(ByVal value As String)
            _LVDiastolic = value
        End Set
    End Property



    Public Property scptcode() As String
        Get
            Return _scptcode
        End Get
        Set(ByVal value As String)
            _scptcode = value
        End Set
    End Property
    Public Property sprocedures() As String
        Get
            Return _sprocedures
        End Get
        Set(ByVal value As String)
            _sprocedures = value
        End Set
    End Property




    Public Property aortic() As String
        Get
            Return _aortic
        End Get
        Set(ByVal value As String)
            _aortic = value
        End Set
    End Property


    Public Property mitral() As String
        Get
            Return _mitral
        End Get
        Set(ByVal value As String)
            _mitral = value
        End Set
    End Property


    Public Property laarea() As String
        Get
            Return _laarea
        End Get
        Set(ByVal value As String)
            _laarea = value
        End Set
    End Property


    Public Property avarea() As String
        Get
            Return _avarea
        End Get
        Set(ByVal value As String)
            _avarea = value
        End Set
    End Property



    Public Property mvarea() As String
        Get
            Return _mvarea
        End Get
        Set(ByVal value As String)
            _mvarea = value
        End Set
    End Property

    'Public Property cvdiastvol() As String
    '    Get
    '        Return _cvdiastvol
    '    End Get
    '    Set(ByVal value As String)
    '        _cvdiastvol = value
    '    End Set
    'End Property



    Public Property lvsystvol() As String
        Get
            Return _lvsystvol
        End Get
        Set(ByVal value As String)
            _lvsystvol = value
        End Set
    End Property


    Public Property lvmass() As String
        Get
            Return _lvmass
        End Get
        Set(ByVal value As String)
            _lvmass = value
        End Set
    End Property


    Public Property IDofinterpatphys() As String
        Get
            Return _IDofinterpatphys
        End Get
        Set(ByVal value As String)
            _IDofinterpatphys = value
        End Set
    End Property



    Public Property Narrativesummary() As String
        Get
            Return _Narrativesummary
        End Get
        Set(ByVal value As String)
            _Narrativesummary = value
        End Set
    End Property


    Public Property lvedd() As String
        Get
            Return _lvedd
        End Get
        Set(ByVal value As String)
            _lvedd = value
        End Set
    End Property

    Public Property lvesdvc() As String
        Get
            Return _lvesdvc
        End Get
        Set(ByVal value As String)
            _lvesdvc = value
        End Set
    End Property

  
    Public Property lvpostwallthik() As String
        Get
            Return _lvpostwallthik
        End Get
        Set(ByVal value As String)
            _lvpostwallthik = value
        End Set
    End Property



    Public Property septalthik() As String
        Get
            Return _septalthik
        End Get
        Set(ByVal value As String)
            _septalthik = value
        End Set
    End Property







End Class


Public Class Cls_Echocardiograms
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
    Public ReadOnly Property Item(ByVal index As Integer) As Cls_Echocardiogram
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), Cls_Echocardiogram)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As Cls_Echocardiogram)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                Me.Clear()
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
