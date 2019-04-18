Public Class gloCPT
    Inherits gloCODEDetails
    Private _Modifiers As gloModifiers
    Private _ICD9s As gloICD9s
    Private bModifiers As Boolean = False
    Private bICD9 As Boolean = False

    Private _Unit As String
    Public Sub Dispose()
        If (bModifiers) Then
            _Modifiers.Dispose()
            bModifiers = False
        End If
        If (bICD9) Then
            _ICD9s.Dispose()
            bICD9 = False
        End If
    End Sub
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
        End Set
    End Property
    Public Property ICD9Col() As gloICD9s
        Get
            If IsNothing(_ICD9s) Then
                _ICD9s = New gloICD9s
                bICD9 = True
            End If
            Return _ICD9s
        End Get
        Set(ByVal value As gloICD9s)
            If (bICD9) Then
                _ICD9s.Dispose()
                bICD9 = False
            End If
            _ICD9s = value
        End Set
    End Property
    Public Property ModfierCol() As gloModifiers
        Get
            If IsNothing(_Modifiers) Then
                _Modifiers = New gloModifiers
                bModifiers = True
            End If
            Return _Modifiers
        End Get
        Set(ByVal value As gloModifiers)
            If (bModifiers) Then
                _Modifiers.Dispose()
                bModifiers = False
            End If
            _Modifiers = value
        End Set
    End Property
End Class
Public Class gloICD9CPT
    Private _ICD9s As gloICD9s
    'Private _Patient As Patient
    Private _ExamDate As DateTime
    Private _VisitDoctor As gloPersonName
    Private _examid As Int64
    Private _patientKeyId As String
    Private bICD9 As Boolean = False
    Private bVisitDoctor As Boolean = False
    Public Sub Dispose()
        If (bVisitDoctor) Then

            _VisitDoctor = Nothing
            bVisitDoctor = False

        End If
        If (bICD9) Then
            _ICD9s.Dispose()
            bICD9 = False
        End If
    End Sub
    Public Property PatientKeyID() As String
        Get
            Return _patientKeyId
        End Get
        Set(ByVal value As String)
            _patientKeyId = value
        End Set
    End Property
    Public Property ExamID() As Int64
        Get
            Return _examid
        End Get
        Set(ByVal value As Int64)
            _examid = value
        End Set
    End Property
    Public Property VisitDoctor() As gloPersonName
        Get
            If IsNothing(_VisitDoctor) Then
                _VisitDoctor = New gloPersonName
                bVisitDoctor = True
            End If
            Return _VisitDoctor
        End Get
        Set(ByVal value As gloPersonName)
            If (bVisitDoctor) Then
                _VisitDoctor = Nothing
                bVisitDoctor = False

            End If
            _VisitDoctor = value
        End Set
    End Property
    Public Property ExamDate() As String
        Get
            Return _ExamDate
        End Get
        Set(ByVal value As String)
            _ExamDate = value
        End Set
    End Property
    'Public Property PatientObject() As Patient
    '    Get
    '        If IsNothing(_Patient) Then
    '            _Patient = New Patient
    '        End If
    '        Return _Patient
    '    End Get
    '    Set(ByVal value As Patient)
    '        _Patient = value
    '    End Set
    'End Property
    Public Property ICD9Col() As gloICD9s
        Get
            If IsNothing(_ICD9s) Then
                _ICD9s = New gloICD9s
                bICD9 = True
            End If
            Return _ICD9s
        End Get
        Set(ByVal value As gloICD9s)
            If (bICD9) Then
                _ICD9s.Dispose()
                bICD9 = False
            End If
            _ICD9s = value
        End Set
    End Property
End Class
Public Class gloCODEDetails
    Private _code As String
    Private _description As String
    Private _examid As Int64
    Public Property ExamID() As Int64
        Get
            Return _examid
        End Get
        Set(ByVal value As Int64)
            _examid = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
End Class
Public Class gloModifier
    Inherits gloCODEDetails
    'Private _Unit As String
    'Public Property Unit() As String
    '    Get
    '        Return _Unit
    '    End Get
    '    Set(ByVal value As String)
    '        _Unit = value
    '    End Set
    'End Property
End Class
Public Class gloPersonName

    Private _ID As Long
    Private _FirstName As String
    Private _MiddleName As String
    Private _LastName As String
    Private _Code As String

    Public Property ID() As Long
        Get
            Return _ID
        End Get
        Set(ByVal Value As Long)
            _ID = Value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal Value As String)
            _FirstName = Value
        End Set
    End Property

    Public Property MiddleName() As String
        Get
            Return _MiddleName
        End Get
        Set(ByVal Value As String)
            _MiddleName = Value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal Value As String)
            _LastName = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal Value As String)
            _Code = Value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class gloDBCollection
    Inherits CollectionBase

    'Remove Item at specified index
    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a widget at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no widget exists, a messagebox is shown and the operation is 
            ' cancelled.
            'System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub
    ''Remove All items.
    'Public Sub RemoveAll()
    '    If List.Count > 0 Then
    '        Dim i As Int16
    '        For i = List.Count To 1 Step -1
    '            Me.Remove(i)
    '        Next
    '    End If
    'End Sub
    Public Overridable Sub Dispose()
        Me.Clear()
    End Sub
End Class
Public Class gloICD9s
    Inherits gloDBCollection
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a PatientInterface object.
    Public ReadOnly Property Item(ByVal index As Integer) As gloICD9
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the PatientInterface type, then returned to the 
            ' caller.
            Return CType(List.Item(index), gloICD9)
        End Get
    End Property
    ' Restricts to PatientInterface types, items that can be added to the collection.
    Public Sub Add(ByVal _ICD9 As gloICD9)
        ' Invokes Add method of the List object to add a PatientInterface.
        List.Add(_ICD9)
    End Sub
    Public Overrides Sub Dispose()
        Me.Clear()
    End Sub

End Class
Public Class gloCPTs
    Inherits gloDBCollection
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a PatientInterface object.
    Public ReadOnly Property Item(ByVal index As Integer) As gloCPT
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the PatientInterface type, then returned to the 
            ' caller.
            Return CType(List.Item(index), gloCPT)
        End Get
    End Property
    ' Restricts to PatientInterface types, items that can be added to the collection.
    Public Sub Add(ByVal _CPT As gloCPT)
        ' Invokes Add method of the List object to add a PatientInterface.
        List.Add(_CPT)
    End Sub
    Public Overrides Sub Dispose()
        Me.Clear()
    End Sub
End Class
Public Class gloModifiers
    Inherits gloDBCollection
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a PatientInterface object.
    Public ReadOnly Property Item(ByVal index As Integer) As gloModifier
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the PatientInterface type, then returned to the 
            ' caller.
            Return CType(List.Item(index), gloModifier)
        End Get
    End Property
    ' Restricts to PatientInterface types, items that can be added to the collection.
    Public Sub Add(ByVal _Modifier As gloModifier)
        ' Invokes Add method of the List object to add a PatientInterface.
        List.Add(_Modifier)
    End Sub
    Public Overrides Sub Dispose()
        Me.Clear()
    End Sub
End Class
Public Class gloICD9CPTs
    Inherits gloDBCollection
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a PatientInterface object.
    Public ReadOnly Property Item(ByVal index As Integer) As gloICD9CPT
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the PatientInterface type, then returned to the 
            ' caller.
            Return CType(List.Item(index), gloICD9CPT)
        End Get
    End Property
    ' Restricts to PatientInterface types, items that can be added to the collection.
    Public Sub Add(ByVal _ICD9CPT As gloICD9CPT)
        ' Invokes Add method of the List object to add a PatientInterface.
        List.Add(_ICD9CPT)
    End Sub
    Public Overrides Sub Dispose()
        Me.Clear()
    End Sub
End Class

Public Class gloICD9
    Inherits gloCODEDetails

End Class
