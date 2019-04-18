Public Class clsVoiceCommands
    Public clVoiceCommands As New Collection


    Public Sub New()
        clVoiceCommands.Add("CPT")
        clVoiceCommands.Add("ICD9")
        clVoiceCommands.Add("Drugs")
        clVoiceCommands.Add("SIG")
        clVoiceCommands.Add("Close Window")
        clVoiceCommands.Add("Add CPT")
        clVoiceCommands.Add("Pick Address", "123")
    End Sub
End Class

Public Class clsTemplateVoiceCommands
    Public clVoiceCommands As New Collection

    Public Sub ClearCommands()
        Dim nCount As Integer
        For nCount = clVoiceCommands.Count To 1 Step -1
            clVoiceCommands.Remove(nCount)
        Next
    End Sub

    Public Sub AddCommands(ByVal strText As String)
        clVoiceCommands.Add("Patient " & strText)
    End Sub
End Class
