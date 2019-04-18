'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class DataGridPasswordcharTextBoxColumn
    Inherits DataGridTextBoxColumn

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer)
        createPasswordString(g, bounds, source, rowNum)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal alignToRight As Boolean)
        createPasswordString(g, bounds, source, rowNum)
    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As System.Drawing.Brush, ByVal foreBrush As System.Drawing.Brush, ByVal alignToRight As Boolean)
        createPasswordString(g, bounds, source, rowNum)

    End Sub

    Private Function createPasswordString(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer) As Boolean
        Dim temp As String
        Dim oVal As Object
        oVal = MyBase.GetColumnValueAtRow(source, rowNum)
        If oVal.GetType Is GetType(DBNull) Then
            temp = ""
        Else
            temp = CStr(oVal)
        End If
        temp = Regex.Replace(temp, ".", "*")
        MyBase.PaintText(g, bounds, temp, False)

        Return True
    End Function


End Class
