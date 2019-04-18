Public Enum enuSort
    eNDCAsc
    eNDCDesc
    eFormularyStatusAsc
    eFormularyStatusDesc
End Enum

Public Class CMySort
    Implements System.Collections.Generic.IComparer(Of FormularyDetails)


    Dim eSortColumn As enuSort
    Sub New(ByVal enSortColumn As enuSort)
        eSortColumn = enSortColumn
    End Sub
    'Public Function Compare(ByVal x As Object, _
    '    ByVal y As Object) As Integer Implements _
    '    System.Collections.Generic.IComparer(Of FormularyDetails)
    '    Select Case eSortColumn
    '        Case eSortColumn.eNDCAsc
    '            Compare = CType(x, FormularyDetails).NDCCode < CType(y, FormularyDetails).NDCCode   'asc
    '        Case eSortColumn.eNDCDesc
    '            Compare = CType(x, FormularyDetails).NDCCode > CType(y, FormularyDetails).NDCCode   'decs
    '        Case eSortColumn.eFormularyStatusAsc
    '            Compare = CType(x, FormularyDetails).FormularyStatus < CType(y, FormularyDetails).FormularyStatus   'asc
    '        Case eSortColumn.eFormularyStatusDesc
    '            Compare = CType(x, FormularyDetails).FormularyStatus > CType(y, FormularyDetails).FormularyStatus   'desc
    '    End Select
    'End Function

    'Public Function Compare1(ByVal x As FormularyDetails, ByVal y As FormularyDetails) As Integer Implements System.Collections.Generic.IComparer(Of FormularyDetails).Compare

    'End Function

    Public Function Compare(ByVal x As FormularyDetails, ByVal y As FormularyDetails) As Integer Implements System.Collections.Generic.IComparer(Of FormularyDetails).Compare
        Select Case eSortColumn
            Case enuSort.eNDCAsc
                Compare = CType(x, FormularyDetails).NDCCode < CType(y, FormularyDetails).NDCCode   'asc
            Case enuSort.eNDCDesc
                Compare = CType(x, FormularyDetails).NDCCode > CType(y, FormularyDetails).NDCCode   'decs
            Case enuSort.eFormularyStatusAsc
                Compare = CType(x, FormularyDetails).FormularyStatus < CType(y, FormularyDetails).FormularyStatus   'asc
            Case enuSort.eFormularyStatusDesc
                Compare = CType(x, FormularyDetails).FormularyStatus > CType(y, FormularyDetails).FormularyStatus   'desc
            Case Else
                Compare = Nothing
        End Select
    End Function
End Class
Public Class FormularyDetails
    Private mNDCCode As String
    Private mFormularyStatus As String

    Public Property NDCCode() As String
        Get
            Return mNDCCode

        End Get
        Set(ByVal value As String)
            mNDCCode = value
        End Set
    End Property

    Public Property FormularyStatus() As String
        Get
            Return mFormularyStatus

        End Get
        Set(ByVal value As String)
            mFormularyStatus = value
        End Set
    End Property


    Public Sub New(ByVal sNDC As String, ByVal sFormulary As String)
        mNDCCode = sNDC
        mFormularyStatus = sFormulary
    End Sub
End Class