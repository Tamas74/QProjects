Public Class frmLabTests

    Public strTestName As String = ""
    Public strSerchText As String = ""

    Private dtTests As DataTable

    Private Sub frmLabTests_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillTest()

        c1TestLibrary.Cols(0).Width = c1TestLibrary.Width * 0.99
        'If Not strSerchText = "" Then
        '    txtListSearch.Text = strSerchText
        'End If
    End Sub
    Private Sub FillTest()
        Try
            Dim ocls As New clsDASSettings
            Dim dt As DataTable
            dt = ocls.GetLabTest()
            If dt.Rows.Count > 0 Then
                c1TestLibrary.DataSource = dt.DefaultView
                dtTests = dt
            Else
                c1TestLibrary.DataSource = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If c1TestLibrary.Rows.Count > 1 Then
                strTestName = c1TestLibrary.GetData(c1TestLibrary.RowSel, 0)
            End If
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtListSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtListSearch.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            If c1TestLibrary.Rows.Count >= 0 Then
                c1TestLibrary.Select(0, 0)
            End If
        End If
    End Sub

    Private Sub txtListSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtListSearch.TextChanged
        Try
            Dim dvlab As DataView

            'FillTest()

            dvlab = dtTests.DefaultView

            Dim strLab As String
            If Trim(txtListSearch.Text) <> "" Then
                strLab = Replace(txtListSearch.Text, "'", "''")

                ''If search string starts with '*' char then repalce all '*' chars except the one at start  
                If (strLab.StartsWith("*") = True) Then

                    strLab = Replace(strLab, "*", "") & ""
                    strLab = "*" + strLab
                Else
                    strLab = Replace(strLab, "*", "") & ""
                End If
                '''' for special characters in search.
                strLab = Replace(strLab, "[", "") & ""
                strLab = mdlGeneral.ReplaceSpecialCharacters(strLab)
            Else
                strLab = ""
                FillTest()
            End If
            c1TestLibrary.Cols(0).Width = c1TestLibrary.Width * 0.99
            If Not strLab = "" Then
                dvlab.RowFilter = dvlab.Table.Columns(0).ColumnName & " Like '%" & strLab & "%'  "
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtListSearch.Text = ""
    End Sub

    
    Private Sub c1TestLibrary_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1TestLibrary.DoubleClick
        Try
            If c1TestLibrary.Rows.Count > 1 Then
                strTestName = c1TestLibrary.GetData(c1TestLibrary.RowSel, 0)
            End If
            Me.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class