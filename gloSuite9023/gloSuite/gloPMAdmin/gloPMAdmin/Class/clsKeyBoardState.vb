'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Public Class clsKeyBoardState
    Declare Function GetKeyboardState Lib "user32" Alias "GetKeyboardState" (ByVal pbKeyState() As Byte) As Long

#Region " Private Variables"
    Private KeyCode As Integer
#End Region

#Region " Public Functions"
    Public Sub New(ByVal keycode As Integer)
        Me.KeyCode = keycode
    End Sub
    Public Function KeyState() As Boolean
        Dim state(256) As Byte
        GetKeyboardState(state)
        Return (IIf(state(Me.KeyCode) = 1, True, False))
    End Function
#End Region
End Class
