Module mdlcreatetiff
 

    Public Sub IsTiffGenerated(ByVal FileName As String)
        While System.IO.File.Exists(FileName) = False

Open:
            Try
                Rename(FileName, FileName)
            Catch ex As Exception
                GoTo Open
            End Try
            'ProgressBar1.Value = ProgressBar1.Value + ProgressBar1.Step
            'If ProgressBar1.Value = ProgressBar1.Maximum Then
            '    ProgressBar1.Value = 0
            'End If
        End While


    End Sub
End Module
