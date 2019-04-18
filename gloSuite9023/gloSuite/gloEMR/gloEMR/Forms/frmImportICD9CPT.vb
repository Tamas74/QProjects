Imports System.IO
Public Class frmImportICD9CPT
    Dim oICD9Coll As New CollICD9CPT
    Dim oCPTColl As New CollICD9CPT
    Dim strFileName As String = ""
    Dim oICD9CPT As DBICD9CPT
    Dim IsDeletePreviousICD9 As Boolean = False
    Dim IsDeletePreviousCPT As Boolean = False

    Private Sub btnbrowsefileICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowsefileICD9.Click
        Try
            oICD9Coll = New CollICD9CPT
            openfileICD9CPT.FileName = ""
            openfileICD9CPT.Filter = "Text Files (*.txt)|*.txt"
            If openfileICD9CPT.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                strFileName = openfileICD9CPT.FileName
                If File.Exists(strFileName) Then
                    If ReadDataICD9(strFileName) Then
                        txtFilePathICD9.Text = strFileName
                        btn_tls_Import.Enabled = True
                    Else
                        btn_tls_Import.Enabled = False
                        MessageBox.Show("Error in File Reading,Please Check file", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function ReadDataICD9(ByVal _strFileName As String) As Boolean
        Try
            Dim stArr() As String
            Dim strTXT As String
            Dim oFile As FileStream = New FileStream(_strFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim oReader As StreamReader = New StreamReader(oFile)
            Dim oICD9 As ICD9CPT 'Pharmacy

            Do While oReader.Peek() <> -1

                strTXT = oReader.ReadLine()
                oICD9 = New ICD9CPT
                stArr = strTXT.Split(vbTab)
                oICD9.ICD9Code = stArr(0)
                oICD9.ICD9Indicator = stArr(1)
                oICD9.ICD9CodeStatus = stArr(2)
                oICD9.ICD9DescriptionShort = stArr(3)
                oICD9.ICD9DescriptionMedium = stArr(4)
                oICD9.ICD9DescriptionLong = stArr(5)
                oICD9Coll.Add(oICD9)
            Loop
            If IsNothing(oICD9Coll) Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub ImportICD9CPT()

        If tbImportICD9CPT.SelectedTab.Text = "ICD9" Then
            Try
                Me.Cursor = Cursors.WaitCursor
                oICD9CPT = New DBICD9CPT
                'prgImportICD9CPT.Visible = True
                'prgImportICD9CPT.Minimum = 0
                'prgImportICD9CPT.Maximum = oICD9Coll.Count - 1
                IsDeletePreviousICD9 = chkDeleteICD9.Checked
                If oICD9CPT.FillICD9Gallery(oICD9Coll, prgsBar, IsDeletePreviousICD9) Then
                    txtFilePathICD9.Text = ""
                    chkDeleteICD9.Checked = False
                    btn_tls_Import.Enabled = False
                End If

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                oICD9CPT = New DBICD9CPT
                IsDeletePreviousCPT = chkDeleteCPT.Checked
                If oICD9CPT.FillCPTGallery(oCPTColl, prgsBar, IsDeletePreviousCPT) Then
                    txtFilePathCPT.Text = ""
                    chkDeleteCPT.Checked = False
                    btn_tls_Import.Enabled = False
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnbrowsefileCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowsefileCPT.Click
        oICD9Coll = New CollICD9CPT

        openfileICD9CPT.Filter = "Text Files (*.txt)|*.txt"
        openfileICD9CPT.FileName = ""
        If openfileICD9CPT.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
            strFileName = openfileICD9CPT.FileName
            If File.Exists(strFileName) Then
                If ReadDataCPT(strFileName) Then
                    txtFilePathCPT.Text = strFileName
                    btn_tls_Import.Enabled = True
                Else
                    btn_tls_Import.Enabled = False
                    MessageBox.Show("Error in File Reading,Please Check file", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub

    Public Function ReadDataCPT(ByVal _strFileName As String) As Boolean
        Try

            Dim strTXT As String
            Dim oFile As FileStream = New FileStream(_strFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim oReader As StreamReader = New StreamReader(oFile)
            Dim oCPT As ICD9CPT
            Dim nCount As Integer = 0
            oCPTColl = New CollICD9CPT
            Do While oReader.Peek() <> -1
                nCount = nCount + 1
                strTXT = oReader.ReadLine()
                If nCount >= 14 Then
                    oCPT = New ICD9CPT
                    oCPT.CPTCode = strTXT.Substring(0, 5).Trim
                    oCPT.CPTDescription = strTXT.Substring(6, 55).Trim
                    oCPTColl.Add(oCPT)
                End If
            Loop
            If IsNothing(oCPTColl) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    Private Sub tbImportICD9CPT_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFilePathCPT.Text = ""
        txtFilePathICD9.Text = ""
        btn_tls_Import.Enabled = False
        oCPTColl = Nothing
        oICD9Coll = Nothing
    End Sub

    Private Sub tlsImportICD9CPT_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsImportICD9CPT.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Import"
                ImportICD9CPT()
            Case "Close"
                Me.Close()


        End Select
    End Sub
End Class