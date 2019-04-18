Public Class ClsCustomCompare
    Implements IComparer

    ' Calls CaseInsensitiveComparer.Compare with the parameters reversed.
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
       Implements IComparer.Compare
        'Return New CaseInsensitiveComparer().Compare(y, x)
        Dim i As Int32 = CType(x, Label).Left - CType(y, Label).Left
        If i > 0 Then
            Return 0
        ElseIf i < 0 Then
            Return 1
        End If
    End Function 'IComparer.Compare
End Class
