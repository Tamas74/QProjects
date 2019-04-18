Imports System.Data
Imports System.Data.SqlClient

Public Class clsDMS
    Dim _dv As DataView = Nothing ' For dataView , when fill fetchdata set dataview to dv and its assign to GetdataView Property
    Dim _NewScanFolder As String ' its use to new scan folder and its value set from GetnewScanFolder procedure

    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(_dv) Then
            _dv.Dispose()
            _dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If
        
        

    End Sub
    Public ReadOnly Property GetDataView()
        Get
            Return _dv
        End Get
    End Property
    Public Sub FetchData()
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)
        Try


            Cmd = New SqlCommand("gsp_FillPatFullName", conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            adpt.Dispose()
            adpt = Nothing
            _dv = dt.Copy().DefaultView
            dt.Dispose()
            dt = Nothing
            conn.Close()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMS -- FetchData -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("clsDMS -- FetchData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If



            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub

    Public Sub HideColumns(ByVal dgControl As DataGrid, ByVal oDataView As DataView)
        Dim ts As DataGridTableStyle = New DataGridTableStyle
        Try
            ts.MappingName = oDataView.Table.TableName
            ts.ReadOnly = True
            'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
            ts.HeaderBackColor = System.Drawing.Color.WhiteSmoke
            ts.HeaderFont = gloGlobal.clsgloFont.gFontArial_Bold 'New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            ts.RowHeadersVisible = False
            ts.ColumnHeadersVisible = True


            ts.PreferredRowHeight = 20


            ' ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, TemplateGalleryNameCol, CategoryCol, ProviderCol, DescriptionCol})
            Dim dgID As New DataGridTextBoxColumn

            With dgID
                .MappingName = oDataView.Table.Columns(0).ColumnName
                .Alignment = HorizontalAlignment.Center
                .Width = 0
                .NullText = ""
            End With

            Dim dgCol1 As New DataGridTextBoxColumn
            With dgCol1
                .MappingName = oDataView.Table.Columns(1).ColumnName
                .HeaderText = "Code"
                .Width = 75
                .NullText = ""
            End With

            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = oDataView.Table.Columns(2).ColumnName
                .HeaderText = "Name"
                .Width = (dgControl.Width) - (25 + 68)
                .NullText = ""
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2})
            dgControl.TableStyles.Clear()
            dgControl.TableStyles.Add(ts)
        Catch ex As Exception
            UpdateLog("clsDMS -- HideColumns -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function Get_FolderToTreeItem(ByVal sFolderName As String) As String
        Get_FolderToTreeItem = ""
        If Trim(sFolderName) <> "" Then ' 08182005 050338 PM
            Get_FolderToTreeItem = Mid(sFolderName, 1, 2) & "/" & Mid(sFolderName, 3, 2) & "/" & Mid(sFolderName, 5, 5) & Mid(sFolderName, 10, 2) & ":" & Mid(sFolderName, 12, 2) & ":" & Mid(sFolderName, 14)
        End If
    End Function

    Public Function Get_TreeItemToFolder(ByVal sTreeItem As String) As String
        Get_TreeItemToFolder = ""
        If Trim(sTreeItem) <> "" Then ' 08/21/2005 11:24:24 PM
            Get_TreeItemToFolder = Replace(Replace(sTreeItem, "/", ""), ":", "")
        End If
    End Function

    Public Function Get_FileToTreeItem(ByVal sFileName As String) As String
        Get_FileToTreeItem = ""
        If Trim(sFileName) <> "" Then ' 08182005 050338 PM-1.pdf
            If Mid(sFileName, Len(sFileName) - 4) = ".pdf" Then
                sFileName = Mid(sFileName, 1, Len(sFileName) - 4)
            End If
            Get_FileToTreeItem = Mid(sFileName, 1, 2) & "/" & Mid(sFileName, 3, 2) & "/" & Mid(sFileName, 5, 5) & Mid(sFileName, 10, 2) & ":" & Mid(sFileName, 12, 2) & ":" & Mid(sFileName, 14)
        End If
    End Function

    Public Function Get_TreeItemToFile(ByVal sTreeItem As String) As String
        Get_TreeItemToFile = ""
        If Trim(sTreeItem) <> "" Then ' 08/21/2005 11:24:24 PM
            Get_TreeItemToFile = Replace(Replace(sTreeItem, "/", ""), ":", "")
        End If
    End Function


    Public ReadOnly Property NewUnCategorisedFolder()
        Get
            Return _NewScanFolder ' its use to set file name of new folder
        End Get
    End Property

    Public Function Get_NewScanFolderName() As String
        Get_NewScanFolderName = "" : _NewScanFolder = ""
        Dim sRootPath As String = DMSRootPath
        Dim sRootFolder As String = DMSRootFolder
        Dim sScanPath As String = DMSScanContainer
        Try
            Dim sNewFolder As String = Format(Date.Now.Date, "MM/dd/yyyy") & " " & Format(Date.Now, "hh:mm:ss tt")
            sNewFolder = Replace(Replace(sNewFolder, "/", ""), ":", "")
            Get_NewScanFolderName = sRootPath & "\" & sRootFolder & "\" & sScanPath & "\" & sNewFolder
            If System.IO.Directory.Exists(Get_NewScanFolderName) = False Then
                MkDir(Get_NewScanFolderName)
                Get_NewScanFolderName = Get_NewScanFolderName
                _NewScanFolder = sNewFolder
            Else
                Dim i As Integer
                For i = 1 To 32000
                    If System.IO.Directory.Exists(Get_NewScanFolderName & i) = False Then
                        MkDir(Get_NewScanFolderName & i)
                        Get_NewScanFolderName = Get_NewScanFolderName & i
                        _NewScanFolder = sNewFolder & i
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateLog("clsDMS -- Get_NewScanFolderName -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Function Get_ImportNewFldrPath(ByVal lPatientId As Long) As String
        Get_ImportNewFldrPath = ""
        Dim _ImportNewFldrPath As String
        Try
            Dim sNewFolder As String = Format(Date.Now.Date, "MM/dd/yyyy") & " " & Format(Date.Now, "hh:mm:ss tt")
            sNewFolder = Replace(Replace(sNewFolder, "/", ""), ":", "")

            _ImportNewFldrPath = DMSRootPath & "\" & DMSRootFolder & "\" & DMSScanContainer & "\" & lPatientId
            If System.IO.Directory.Exists(_ImportNewFldrPath) = False Then
                MkDir(_ImportNewFldrPath)
            End If
            _ImportNewFldrPath = DMSRootPath & "\" & DMSRootFolder & "\" & DMSScanContainer & "\" & lPatientId & "\" & sNewFolder

            If System.IO.Directory.Exists(_ImportNewFldrPath) = False Then
                MkDir(_ImportNewFldrPath)
                Get_ImportNewFldrPath = _ImportNewFldrPath
            Else
                Dim i As Integer
                For i = 1 To 32000
                    If System.IO.Directory.Exists(_ImportNewFldrPath & i) = False Then
                        MkDir(_ImportNewFldrPath & i)
                        Get_ImportNewFldrPath = _ImportNewFldrPath & i
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateLog("clsDMS -- Get_ImportNewFldrPath -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Function Get_CategoryNewFldrName(ByVal lPatientId As Long, ByVal sCategory As String) As String
        Get_CategoryNewFldrName = ""
        Dim _CategoryNewFldrPath As String
        Try
            Dim sNewFolder As String = Format(Date.Now.Date, "MM/dd/yyyy") & " " & Format(Date.Now, "hh:mm:ss tt")
            sNewFolder = Replace(Replace(sNewFolder, "/", ""), ":", "")

            _CategoryNewFldrPath = DMSRootPath & "\" & DMSRootFolder & "\" & DMSGeneralContainer & "\" & sCategory & "\" & lPatientId & "\" & sNewFolder

            If System.IO.Directory.Exists(_CategoryNewFldrPath) = False Then
                Get_CategoryNewFldrName = sNewFolder
            Else
                Dim i As Integer
                For i = 1 To 32000
                    If System.IO.Directory.Exists(_CategoryNewFldrPath & i) = False Then
                        MkDir(_CategoryNewFldrPath & i)
                        Get_CategoryNewFldrName = sNewFolder & i
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateLog("clsDMS -- Get_CategoryNewFldrName -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Function GetCatDocFldrList(ByVal nDocCategory As Integer, ByVal sCategory As String, ByVal lPatientId As Long, ByVal dtCustomDate As Date) As Collection
        Dim _tmpGetCatDocFldrList As New Collection
        'Dim i As Long
        Try
            Dim dtDate As Date
            Dim dtToday As Date = Format(Date.Now.Date, "MM/dd/yyyyy")
            Dim dtYesterDay As Date = Format(Date.Now.Date.AddDays(-1), "MM/dd/yyyy")
            Dim dtWeekStart As Date = Format(Date.Now.Date.AddDays(-7), "MM/dd/yyyy")
            Dim dtWeekEnd As Date = Format(Date.Now.Date, "MM/dd/yyyy")
            Dim dtMonthStart As Date = Format(Date.Now.Date.AddMonths(-1), "MM/dd/yyyy")
            Dim dtMonthEnd As Date = Format(Date.Now.Date, "MM/dd/yyyy")

            Dim sTodaysDate As String = DateAsString(dtToday)             ' Todays Date
            Dim sYesterDay As String = DateAsString(dtYesterDay)          ' Yesterdays Date
            Dim sWeekStartDate As String = DateAsString(dtWeekStart)      ' Last Week Start Date
            Dim sWeekEndDate As String = DateAsString(dtWeekEnd)          ' Last Week Last Date
            Dim sMonthStartDate As String = DateAsString(dtMonthStart)    ' Last Month Start Date
            Dim sMonthEndDate As String = DateAsString(dtMonthEnd)        ' Last Month End Date

            Dim _DocumentPath As String = DMSRootPath & "\" & DMSRootFolder & "\" & DMSGeneralContainer & "\" & sCategory & "\" & lPatientId

            Dim oFolder As System.IO.DirectoryInfo

            Dim oRootFolder As New System.IO.DirectoryInfo(_DocumentPath)              ' Set Patient Folder 

            If System.IO.Directory.Exists(_DocumentPath) = True Then
                Select Case nDocCategory
                    Case DocumentCriteria.CustomDateDocument ' Custom Date Document
                        dtDate = Format(DateSerial(dtCustomDate.Year, dtCustomDate.Month, dtCustomDate.Day), "MM/dd/yyyy")
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories(If(Val(dtDate.Month) <= 9, "0" & Val(dtDate.Month), Val(dtDate.Month)) & If(Val(dtDate.Day) <= 9, "0" & Val(dtDate.Day), Val(dtDate.Day)) & Val(dtDate.Year) & "*")  ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            _tmpGetCatDocFldrList.Add(oFolder.FullName)
                        Next

                    Case DocumentCriteria.TodaysDocument ' Todays Document
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories(sTodaysDate & "*")       ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            _tmpGetCatDocFldrList.Add(oFolder.FullName)
                        Next

                    Case DocumentCriteria.YesterDaysDocument ' YesterDays Document
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories(sYesterDay & "*")      ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            _tmpGetCatDocFldrList.Add(oFolder.FullName)
                        Next

                    Case DocumentCriteria.LastWeeksDocument ' ThisWeeks Document
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()      ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            dtDate = Format(DateSerial(Mid(oFolder.Name, 5, 4), Mid(oFolder.Name, 1, 2), Mid(oFolder.Name, 3, 2)), "MM/dd/yyyy")
                            If dtDate >= dtWeekStart AndAlso dtDate <= dtWeekEnd Then
                                _tmpGetCatDocFldrList.Add(oFolder.FullName)
                            End If
                        Next

                    Case DocumentCriteria.LastMonthsDocument ' ThisMonths Document
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()      ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            dtDate = Format(DateSerial(Mid(oFolder.Name, 5, 4), Mid(oFolder.Name, 1, 2), Mid(oFolder.Name, 3, 2)), "MM/dd/yyyy")
                            If dtDate >= dtMonthStart AndAlso dtDate <= dtMonthEnd Then
                                _tmpGetCatDocFldrList.Add(oFolder.FullName)
                            End If
                        Next

                    Case DocumentCriteria.AllDocument ' Previous Document
                        Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()      ' Get Folders in Patient Folder
                        For Each oFolder In oFolders
                            _tmpGetCatDocFldrList.Add(oFolder.FullName)
                        Next
                End Select
            End If ' If Directory.Exists(_DocumentPath) = True Then
            Return _tmpGetCatDocFldrList
        Catch ex As Exception
            UpdateLog("clsDMS -- GetCatDocFldrList -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetFileList(ByVal sFolerPath As String, ByVal sFileExt As String) As Collection
        Dim _tmpGetFileList As New Collection
        Try
            If System.IO.Directory.Exists(sFolerPath) = True Then
                Dim oFolder As New System.IO.DirectoryInfo(sFolerPath)
                Dim oFiles As System.IO.FileInfo() = oFolder.GetFiles(sFileExt)

                If oFiles.Length > 0 Then
                    Dim oFile As System.IO.FileInfo
                    For Each oFile In oFiles
                        _tmpGetFileList.Add(oFile.FullName)
                    Next
                End If

            End If
            Return _tmpGetFileList
        Catch ex As Exception
            UpdateLog("clsDMS -- GetFileList -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetFolderList(ByVal sFolerPath As String) As Collection
        Dim _tmpGetFolderList As New Collection
        Try
            If System.IO.Directory.Exists(sFolerPath) = True Then
                Dim oRootFolder As New System.IO.DirectoryInfo(sFolerPath)
                Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()

                If oFolders.Length > 0 Then
                    Dim oFolder As System.IO.DirectoryInfo
                    For Each oFolder In oFolders
                        _tmpGetFolderList.Add(oFolder.FullName)
                    Next
                End If
            End If

            Return _tmpGetFolderList
        Catch ex As Exception
            UpdateLog("clsDMS -- GetFolderList -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function IsMergeAvailable(ByVal sCatFolderPath As String) As Boolean
        Dim _IsMergeAvailable As Boolean = False
        Try
            If System.IO.Directory.Exists(sCatFolderPath) = True Then
                Dim oRootFolder As New System.IO.DirectoryInfo(sCatFolderPath)
                Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()

                If oFolders.Length > 0 Then
                    Dim oFolder As System.IO.DirectoryInfo
                    For Each oFolder In oFolders
                        Dim oFiles As System.IO.FileInfo() = oFolder.GetFiles("*.pdf")
                        If oFiles.Length > 0 Then
                            _IsMergeAvailable = True
                            Exit For
                        End If
                    Next
                End If
            End If

            Return _IsMergeAvailable
        Catch ex As Exception
            UpdateLog("clsDMS -- IsMergeAvailable -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    'Private Function Get_NodeCount(ByVal trvCount) As Integer
    '    ' Get the collection of TreeNodes.
    '    Dim myNodeCollection As TreeNodeCollection = trvCount.Nodes
    '    Dim myCount As Integer = myNodeCollection.Count
    '    Dim myLabel As Label

    '    myLabel.Text += "Number of nodes in the collection :" + myCount.ToString()

    '    myLabel.Text += ControlChars.NewLine + ControlChars.NewLine + _
    '      "Elements of the Array after Copying from the collection :" + ControlChars.NewLine

    '    ' Create an Object array.
    '    Dim myArray(myCount - 1) As Object

    '    ' Copy the collection into an array.
    '    myNodeCollection.CopyTo(myArray, 0)
    '    Dim i As Integer
    '    For i = 0 To myArray.Length - 1
    '        myLabel.Text += CType(myArray(i), TreeNode).Text + ControlChars.NewLine
    '    Next i
    'End Function
    'Sub DisplayChildrenCount(ByVal nodes As TreeNodeCollection, ByVal display As Boolean)
    '    ' append the number of children to the node's Text
    '    ' (after deleting existing count, if any)

    '    Dim node As TreeNode
    '    For Each node In nodes
    '        ' Remove any child count at the end of the Text, if there.
    '        node.Text = System.Text.RegularExpressions.Regex.Replace(node.Text, " \[.*\]$", "")
    '        ' Add it again, if so requested.
    '        If display Then
    '            node.Text += " [" & CStr(node.Nodes.Count) & "]"
    '        End If
    '        ' Recurse over child nodes.
    '        DisplayChildrenCount(node.Nodes, display)
    '    Next
    'End Sub
End Class
