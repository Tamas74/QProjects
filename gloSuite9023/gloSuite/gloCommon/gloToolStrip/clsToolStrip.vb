Imports System.Data.SqlClient

Public Class clsToolStrip

    Public Sub SaveButtons(ByVal nUserID As Int64, ByVal sModule As String, ByVal arrButtons As ArrayList)
        Dim con As SqlConnection
        Dim cmd As SqlCommand = Nothing
        Try

            If arrButtons IsNot Nothing Then
                If arrButtons.Count > 0 Then
                    '' CREATE STRING TO SAVE ''
                    Dim _ButtonString As String = ""

                    For i As Integer = 0 To arrButtons.Count - 1
                        If i <> arrButtons.Count - 1 Then
                            _ButtonString = _ButtonString & arrButtons(i) & ","
                        Else
                            _ButtonString = _ButtonString & arrButtons(i)
                        End If
                    Next

                    '' DELETE EXISTING RECORDS ''
                    con = New SqlConnection(sConnectionString)
                    Dim sQuery As String
                    sQuery = " DELETE FROM ToolBarButtons WHERE nUserId = " & nUserID & " AND sModule = '" & sModule & "'"
                    cmd = New SqlCommand(sQuery, con)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    '' SAVE NEW SETTINGS ''
                    sQuery = ""
                    sQuery = " INSERT INTO ToolBarButtons(nUserId, sModule, sButtons) VALUES (" & nUserID & ",'" & sModule & "','" & _ButtonString & "')"
                    cmd = New SqlCommand(sQuery, con)
                    cmd.ExecuteNonQuery()

                    con.Close()
                    con.Dispose()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Sub

    Public Function GetButtons(ByVal nUserID As Int64, ByVal sModuleName As String) As ArrayList
        Dim arrButtons As New ArrayList
        Dim con As SqlConnection
        Dim cmd As SqlCommand = Nothing
        Try
         
            Dim sQuery As String
            Dim oResult As Object
            Dim _Buttons As String = ""

            '' READ SETTING FROM DB ''
            con = New SqlConnection(sConnectionString)
            sQuery = " SELECT sButtons FROM ToolBarButtons WHERE nUserId = " & nUserID & " AND sModule = '" & sModuleName & "'"
            cmd = New SqlCommand(sQuery, con)
            con.Open()
            oResult = cmd.ExecuteScalar
            con.Close()
            con.Dispose()
            If oResult IsNot Nothing Then
                _Buttons = oResult.ToString
            End If

            '' SPLIT STRING AND PUT IT IN ARRAYLIST ''
            If _Buttons <> "" Then
                Dim _SplitButtons As String()
                _SplitButtons = _Buttons.Split(",")

                For i As Integer = 0 To _SplitButtons.Length - 1
                    arrButtons.Add(_SplitButtons(i))
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        Return arrButtons
    End Function

End Class
