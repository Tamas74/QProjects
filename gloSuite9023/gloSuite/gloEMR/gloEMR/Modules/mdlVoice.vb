Module mdlVoice
    Enum enmVoiceCommandCase
        TitleCase
        LowerCase
        UpperCase
        None
        All
    End Enum
    Enum enmResultsBoxPosition
        TopLeft
        TopRight
        BottomLeft
        BottomRight
    End Enum


    Public gblnSelectSpeakerByTopic As Boolean = True

    Public gblnVoiceSettingsChange As Boolean = False
    Public genmResultsBoxPosition As enmResultsBoxPosition
    Public gblnResultsBoxVisible As Boolean

    Public vVoiceMenu As DNSTools.IDgnVoiceMenu
    Public nVoiceMenu As Integer = 0

    Public enmCommandCase As enmVoiceCommandCase = enmVoiceCommandCase.All

    Public Sub RemoveVoiceCommands()
        Dim nLoop As Integer
        For nLoop = nVoiceMenu To 1 Step -1
            vVoiceMenu.Remove(nLoop)
        Next
        nVoiceMenu = 0
    End Sub
    Public Sub RemoveVoiceCommands(ByVal nFrom As Integer)
        Dim nLoop As Integer
        For nLoop = nVoiceMenu To nFrom Step -1
            vVoiceMenu.Remove(nLoop)
        Next
        nVoiceMenu = nFrom - 1
    End Sub

    Public Sub RemoveDatagridVoiceCommands(ByVal nVoiceCommands As Integer)
        Dim nLoop As Integer
        For nLoop = 30000 + nVoiceCommands To 30000 Step -1
            vVoiceMenu.Remove(nLoop)
        Next
        'Changes Pankaj
        nVoiceMenu = 30000
    End Sub

    Public Sub AddDatagridVoiceCommand(ByVal nVoiceCommandID As Integer, ByVal strCommand As String, Optional ByVal strCommand2 As String = "", Optional ByVal strCommand3 As String = "", Optional ByVal strCommand4 As String = "")
        strCommand = Replace(Replace(Replace(Replace(strCommand, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand2 = Replace(Replace(Replace(Replace(strCommand2, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand3 = Replace(Replace(Replace(Replace(strCommand3, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand4 = Replace(Replace(Replace(Replace(strCommand4, "*", ""), "<", ""), "=", ""), ">", "")


        nVoiceCommandID = 30000 + nVoiceCommandID
        Select Case enmCommandCase
            Case enmVoiceCommandCase.LowerCase
                strCommand = LCase(strCommand)
                strCommand2 = LCase(strCommand2)
                strCommand3 = LCase(strCommand3)
                strCommand4 = LCase(strCommand4)
            Case enmVoiceCommandCase.TitleCase
                strCommand = StrConv(strCommand, VbStrConv.ProperCase)
                strCommand2 = StrConv(strCommand2, VbStrConv.ProperCase)
                strCommand3 = StrConv(strCommand3, VbStrConv.ProperCase)
                strCommand4 = StrConv(strCommand4, VbStrConv.ProperCase)
            Case enmVoiceCommandCase.UpperCase
                strCommand = UCase(strCommand)
                strCommand2 = UCase(strCommand2)
                strCommand3 = UCase(strCommand3)
                strCommand4 = UCase(strCommand4)
        End Select

        nVoiceMenu = nVoiceCommandID
        vVoiceMenu.Add(nVoiceCommandID, strCommand, "", "")
        If enmCommandCase = enmVoiceCommandCase.All Then
            vVoiceMenu.Add(nVoiceCommandID, LCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceCommandID, UCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceCommandID, StrConv(strCommand, VbStrConv.ProperCase), "", "")
        End If
        If Trim(strCommand2) <> "" Then
            vVoiceMenu.Add(nVoiceCommandID, strCommand2, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceCommandID, LCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceCommandID, UCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceCommandID, StrConv(strCommand2, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand3) <> "" Then
            vVoiceMenu.Add(nVoiceCommandID, strCommand3, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceCommandID, LCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceCommandID, UCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceCommandID, StrConv(strCommand3, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand4) <> "" Then
            vVoiceMenu.Add(nVoiceCommandID, strCommand4, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceCommandID, LCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceCommandID, UCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceCommandID, StrConv(strCommand4, VbStrConv.ProperCase), "", "")
            End If
        End If
    End Sub

    Public Sub AddVoiceCommand(ByVal nVoiceMenuFrom As Integer, ByVal strCommand As String, Optional ByVal strCommand2 As String = "", Optional ByVal strCommand3 As String = "", Optional ByVal strCommand4 As String = "")
        strCommand = Replace(Replace(Replace(Replace(strCommand, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand2 = Replace(Replace(Replace(Replace(strCommand2, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand3 = Replace(Replace(Replace(Replace(strCommand3, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand4 = Replace(Replace(Replace(Replace(strCommand4, "*", ""), "<", ""), "=", ""), ">", "")



        'nVoiceMenu = nVoiceMenu + nVoiceMenuFrom + 1
        If nVoiceMenu < nVoiceMenuFrom Then
            nVoiceMenu = nVoiceMenuFrom + 1
        Else
            nVoiceMenu = nVoiceMenu + 1
        End If

        Select Case enmCommandCase
            Case enmVoiceCommandCase.LowerCase
                strCommand = LCase(strCommand)
                strCommand2 = LCase(strCommand2)
                strCommand3 = LCase(strCommand3)
                strCommand4 = LCase(strCommand4)
            Case enmVoiceCommandCase.TitleCase
                strCommand = StrConv(strCommand, VbStrConv.ProperCase)
                strCommand2 = StrConv(strCommand2, VbStrConv.ProperCase)
                strCommand3 = StrConv(strCommand3, VbStrConv.ProperCase)
                strCommand4 = StrConv(strCommand4, VbStrConv.ProperCase)
            Case enmVoiceCommandCase.UpperCase
                strCommand = UCase(strCommand)
                strCommand2 = UCase(strCommand2)
                strCommand3 = UCase(strCommand3)
                strCommand4 = UCase(strCommand4)
        End Select
        vVoiceMenu.Add(nVoiceMenu, strCommand, "", "")
        If enmCommandCase = enmVoiceCommandCase.All Then
            vVoiceMenu.Add(nVoiceMenu, LCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceMenu, UCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand, VbStrConv.ProperCase), "", "")
        End If
        If Trim(strCommand2) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand2, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand2, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand3) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand3, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand3, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand4) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand4, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand4, VbStrConv.ProperCase), "", "")
            End If
        End If
    End Sub

    Public Sub AddVoiceCommand(ByVal strCommand As String, Optional ByVal strCommand2 As String = "", Optional ByVal strCommand3 As String = "", Optional ByVal strCommand4 As String = "")
        strCommand = Replace(Replace(Replace(Replace(strCommand, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand2 = Replace(Replace(Replace(Replace(strCommand2, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand3 = Replace(Replace(Replace(Replace(strCommand3, "*", ""), "<", ""), "=", ""), ">", "")
        strCommand4 = Replace(Replace(Replace(Replace(strCommand4, "*", ""), "<", ""), "=", ""), ">", "")



        nVoiceMenu = nVoiceMenu + 1
        Select Case enmCommandCase
            Case enmVoiceCommandCase.LowerCase
                strCommand = LCase(strCommand)
                strCommand2 = LCase(strCommand2)
                strCommand3 = LCase(strCommand3)
                strCommand4 = LCase(strCommand4)
            Case enmVoiceCommandCase.TitleCase
                strCommand = StrConv(strCommand, VbStrConv.ProperCase)
                strCommand2 = StrConv(strCommand2, VbStrConv.ProperCase)
                strCommand3 = StrConv(strCommand3, VbStrConv.ProperCase)
                strCommand4 = StrConv(strCommand4, VbStrConv.ProperCase)
            Case enmVoiceCommandCase.UpperCase
                strCommand = UCase(strCommand)
                strCommand2 = UCase(strCommand2)
                strCommand3 = UCase(strCommand3)
                strCommand4 = UCase(strCommand4)
        End Select
        vVoiceMenu.Add(nVoiceMenu, strCommand, "", "")
        If enmCommandCase = enmVoiceCommandCase.All Then
            vVoiceMenu.Add(nVoiceMenu, LCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceMenu, UCase(strCommand), "", "")
            vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand, VbStrConv.ProperCase), "", "")
        End If
        If Trim(strCommand2) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand2, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand2), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand2, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand3) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand3, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand3), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand3, VbStrConv.ProperCase), "", "")
            End If
        End If
        If Trim(strCommand4) <> "" Then
            vVoiceMenu.Add(nVoiceMenu, strCommand4, "", "")
            If enmCommandCase = enmVoiceCommandCase.All Then
                vVoiceMenu.Add(nVoiceMenu, LCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceMenu, UCase(strCommand4), "", "")
                vVoiceMenu.Add(nVoiceMenu, StrConv(strCommand4, VbStrConv.ProperCase), "", "")
            End If
        End If
    End Sub

End Module
