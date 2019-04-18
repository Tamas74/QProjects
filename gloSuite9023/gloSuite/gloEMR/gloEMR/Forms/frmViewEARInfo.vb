Imports System.IO.IsolatedStorage

Public Class frmViewEARInfo


    Dim nPatientId As Int64 = 0 ''''''passed from Rx-Meds


    'Private COL_SELECT As Integer = 0
    Private COL_EARReportFilename As Integer = 0
    Private COL_EARReportGeneratedDate As Integer = 1
    Private COL_EARStartDate As Integer = 2
    Private COL_EAREndDate As Integer = 3
    Private COL_Status As Integer = 4

    Private COL_COUNT As Integer = 5

    Private Sub SetFlexgridColumns()
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            'C1FlexPrescription.SetCellCheck(1, 1, CheckEnum.Unchecked)
            .Cols(0).Width = 30

            '.Cols.Count = 2
            .ExtendLastCol = True
            '.Tree.Column = 1
            'set properties of c1 grid
            'set properties of c1 grid
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width / 10
            'set column value



            '.Cols(COL_SELECT).Width = _Width * 0.2
            .Cols(COL_EARReportFilename).Width = _Width * 3
            .Cols(COL_EARReportGeneratedDate).Width = _Width * 2
            .Cols(COL_EARStartDate).Width = _Width * 1.5
            .Cols(COL_EAREndDate).Width = _Width * 1.5
            .Cols(COL_Status).Width = _Width * 2



            'set column header
            '.SetData(0, COL_SELECT, "")
            .SetData(0, COL_EARReportFilename, "EAR Report Filename")
            .SetData(0, COL_EARReportGeneratedDate, "EAR Report Generated Date")
            .SetData(0, COL_EARStartDate, "EAR Start Date")
            .SetData(0, COL_EAREndDate, "EAR End Date")
            .SetData(0, COL_Status, "Status")



            'set visiblity for column 
            '.Cols(COL_SELECT).Visible = True
            .Cols(COL_EARReportFilename).Visible = True
            .Cols(COL_EARReportGeneratedDate).Visible = True
            .Cols(COL_EARStartDate).Visible = True
            .Cols(COL_EAREndDate).Visible = True
            .Cols(COL_Status).Visible = True


            ' set column editing properties.
            '.Cols(COL_SELECT).AllowEditing = False
            .Cols(COL_EARReportFilename).AllowEditing = False
            .Cols(COL_EARReportGeneratedDate).AllowEditing = False
            .Cols(COL_EARStartDate).AllowEditing = False
            .Cols(COL_EAREndDate).AllowEditing = False
            .Cols(COL_Status).AllowEditing = False



            .ForeColor = Color.Black
        End With
    End Sub


    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlStrpViewEARRequestFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlStrpViewEARRequestFile.Click
        Dim oclsEARFile As clsEARReporting
        Try
            ''''''get the file information of the selected EAR file
            If _Flex.Rows.Count > 1 Then
                oclsEARFile = New clsEARReporting
                '''''if value is checked then read the request string 
                Dim strTempEARFile As String = oclsEARFile.RetrieveDocumentFile("Request", _Flex.GetData(_Flex.RowSel, COL_EARReportFilename))
                If (IsNothing(strTempEARFile) = False) Then
                    If strTempEARFile <> "" Then
                        'rchtxtbxEARRequestFile.Text = My.Computer.FileSystem.ReadAllText(strTempEARFile)
                        'My.Computer.FileSystem.DeleteFile(strTempEARFile)

                        Dim ofrmVwEARFileData As New frmViewEARFiledata(strTempEARFile)
                        ofrmVwEARFileData.ShowInTaskbar = False

                        ofrmVwEARFileData.ShowDialog(IIf(IsNothing(ofrmVwEARFileData.Parent), Me, ofrmVwEARFileData.Parent))
                        ofrmVwEARFileData.Dispose()
                        ofrmVwEARFileData = Nothing

                    Else
                        MessageBox.Show("Invalid EAR request file information", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("Invalid EAR request file information", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                
                oclsEARFile.Dispose()
                oclsEARFile = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  

    Private Sub frmViewEARInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetFlexgridColumns()
            FillloadEARReportFiles()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillloadEARReportFiles()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSQL As String = ""
        Dim dtUploadedReportFiles As DataTable = Nothing


        Try
            strSQL = "select  sReportFile , dtRptGeneratedDate as RptGeneratedDate, dtStartDate as StartDate, dtEndDate as EndDate,  isnull(sStatus,'') as Status from RxH_PendingEAR order by dtRptGeneratedDate desc" ''where sStatus = 'RecievedResponse' 

            oDB.Connect(GetConnectionString)
            dtUploadedReportFiles = oDB.ReadQueryDataTable(strSQL)

            '  C1ReportFiles.DataSource = dtUploadedReportFiles
            If Not IsNothing(dtUploadedReportFiles) Then
                If dtUploadedReportFiles.Rows.Count > 0 Then
                    For i As Int16 = 0 To dtUploadedReportFiles.Rows.Count - 1
                        _Flex.Rows.Add()
                        '_Flex.SetCellCheck(i + 1, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)

                        
                        _Flex.SetData(i + 1, COL_EARReportFilename, dtUploadedReportFiles.Rows(i)("sReportFile"))
                        _Flex.SetData(i + 1, COL_EARReportGeneratedDate, dtUploadedReportFiles.Rows(i)("RptGeneratedDate"))
                        _Flex.SetData(i + 1, COL_EARStartDate, dtUploadedReportFiles.Rows(i)("StartDate"))
                        _Flex.SetData(i + 1, COL_EAREndDate, dtUploadedReportFiles.Rows(i)("EndDate"))
                        _Flex.SetData(i + 1, COL_Status, dtUploadedReportFiles.Rows(i)("Status"))
                    Next
                    
                End If
                dtUploadedReportFiles.Dispose()
                dtUploadedReportFiles = Nothing
            End If
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing




        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    'Public Function GetUploadedFiles() As DataTable


    '    Dim dtUploadedFiles As New DataTable
    '    Dim conn As New SqlConnection
    '    Dim cmd As New SqlCommand
    '    Dim da As New SqlDataAdapter
    '    Dim _strSQL As String = ""

    '    Try
    '        _strSQL = "select nReportID as ReportID, sReportFile as [Report File], dtRptGeneratedDate as [RptGenerated Date], dtStartDate as [Start Date], dtEndDate as [End Date], nNoOfAttempts as [NoOfAttempts], isnull(sStatus,'') as Status, isnull(sErrorCode,'') as ErrorCode from RxH_PendingEAR where sStatus = 'Uploaded' order by dtRptGeneratedDate desc"

    '        conn.ConnectionString = Clsconnect.GetConnectionString()
    '        cmd.Connection = conn
    '        cmd.CommandType = CommandType.Text
    '        cmd.CommandText = _strSQL

    '        da = New SqlDataAdapter(cmd)

    '        da.Fill(dtUploadedFiles)

    '    Catch sqlEx As SqlException
    '        Throw sqlEx
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    '    Return dtUploadedFiles

    'End Function

    Private Sub tlStrpViewEARResponseFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlStrpViewEARResponseFile.Click
        Dim oclsEARFile As clsEARReporting = Nothing
        Try
            ''''''get the file information of the selected EAR file
            If _Flex.Rows.Count > 1 Then
                oclsEARFile = New clsEARReporting
                '''''if value is checked then read the request string 
                Dim strTempEARFile As String = oclsEARFile.RetrieveDocumentFile("Response", _Flex.GetData(_Flex.RowSel, COL_EARReportFilename))
                If (IsNothing(strTempEARFile) = False) Then


                    If strTempEARFile <> "" Then
                        'rchtxtbxEARRequestFile.Text = My.Computer.FileSystem.ReadAllText(strTempEARFile)
                        'My.Computer.FileSystem.DeleteFile(strTempEARFile)

                        Dim ofrmVwEARFileData As New frmViewEARFiledata(strTempEARFile)
                        ofrmVwEARFileData.ShowInTaskbar = False

                        ofrmVwEARFileData.ShowDialog(IIf(IsNothing(ofrmVwEARFileData.Parent), Me, ofrmVwEARFileData.Parent))
                        ofrmVwEARFileData.Dispose()
                        ofrmVwEARFileData = Nothing
                    Else
                        MessageBox.Show("No EAR response received for this request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    MessageBox.Show("No EAR response received for this request", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(oclsEARFile) Then
                oclsEARFile.Dispose()
                oclsEARFile = Nothing
            End If
        Finally
            If Not IsNothing(oclsEARFile) Then
                oclsEARFile.Dispose()
                oclsEARFile = Nothing
            End If
        End Try
    End Sub


#Region "Formulary Controls Mouse Move Events"
    'Private blnFormularyGrdMoving As Boolean = False
    'Private FormularyGrdMouseDownX As Integer
    'Private FormularyGrdMouseDownY As Integer

    'Private blnFormularyCoveragePnlMoving As Boolean = False
    'Private FormularyCovPnlMouseDownX As Integer
    'Private FormularyCovPnlMouseDownY As Integer

    'Private blnFormularyIndicatorPnlMoving As Boolean = False
    'Private FormularyIndicatorPnlMouseDownX As Integer
    'Private FormularyIndicatorPnlMouseDownY As Integer

    'Private Sub pnlFormularyDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyDrugName.MouseDown
    '    Me.Cursor = Cursors.SizeAll
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyGrdMoving = True
    '        FormularyGrdMouseDownX = e.X
    '        FormularyGrdMouseDownY = e.Y
    '    End If
    'End Sub

    'Private Sub pnlFormularyDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyDrugName.MouseUp
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyGrdMoving = False
    '    End If
    '    Me.Cursor = Cursors.Default
    'End Sub



    'Private Sub lblFormularyDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblFormularyDrugName.MouseDown
    '    Me.Cursor = Cursors.SizeAll
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyGrdMoving = True
    '        FormularyGrdMouseDownX = e.X
    '        FormularyGrdMouseDownY = e.Y
    '    End If
    'End Sub

    'Private Sub lblFormularyDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblFormularyDrugName.MouseUp
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyGrdMoving = False
    '    End If
    '    Me.Cursor = Cursors.Default
    'End Sub


    'Private Sub pnlFormularyCoverageHeading_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyCoverageHeading.MouseDown
    '    Me.Cursor = Cursors.SizeAll
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyCoveragePnlMoving = True
    '        FormularyCovPnlMouseDownX = e.X
    '        FormularyCovPnlMouseDownY = e.Y
    '    End If
    'End Sub

    'Private Sub pnlFormularyCoverageHeading_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyCoverageHeading.MouseUp
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyCoveragePnlMoving = False
    '    End If
    '    Me.Cursor = Cursors.Default
    'End Sub

    'Private Sub pnlFormularyCoverageHeading_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlFormularyCoverageHeading.MouseMove
    '    If blnFormularyCoveragePnlMoving Then
    '        With pnlFormularyCoverage
    '            Dim temp As Point = New Point
    '            temp.X = .Location.X + (e.X - FormularyCovPnlMouseDownX)
    '            temp.Y = .Location.Y + (e.Y - FormularyCovPnlMouseDownY)
    '            .Location = temp
    '            .BringToFront()
    '        End With
    '    End If
    'End Sub

    'Private Sub lblAlternativeDrugName_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlternativeDrugName.MouseDown
    '    Me.Cursor = Cursors.SizeAll
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyCoveragePnlMoving = True
    '        FormularyCovPnlMouseDownX = e.X
    '        FormularyCovPnlMouseDownY = e.Y
    '    End If
    'End Sub

    'Private Sub lblAlternativeDrugName_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlternativeDrugName.MouseUp
    '    If e.Button = MouseButtons.Left Then
    '        blnFormularyCoveragePnlMoving = False
    '    End If
    '    Me.Cursor = Cursors.Default
    'End Sub

    'Private Sub lblAlternativeDrugName_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblAlternativeDrugName.MouseMove
    '    If blnFormularyCoveragePnlMoving Then
    '        With pnlFormularyCoverage
    '            Dim temp As Point = New Point
    '            temp.X = .Location.X + (e.X - FormularyCovPnlMouseDownX)
    '            temp.Y = .Location.Y + (e.Y - FormularyCovPnlMouseDownY)
    '            .Location = temp
    '            .BringToFront()
    '        End With
    '    End If
    'End Sub

    'Private Sub rtfFormularyDescription_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles rtfFormularyDescription.LinkClicked
    '    Try
    '        System.Diagnostics.Process.Start(e.LinkText)
    '    Catch ex As Exception

    '    End Try

    'End Sub

   
#End Region
End Class