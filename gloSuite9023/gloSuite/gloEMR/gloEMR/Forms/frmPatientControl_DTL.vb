Imports System.Data.SqlClient

Public Class frmPatientControl_DTL

    Dim PrevNode As TreeNode
    Dim dtPatientControl_DTL As DataTable


    Private Sub frmPatientControl_DTL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Fill treeview with Nodes
            Call Fill_trvModule()
            'Fill cheklist with item and set cheked item form database
            Call Fill_chklistmodule()
            'By default select First Node in treeview  
            trvmodule.Nodes.Item(0).Expand()
            trvmodule.SelectedNode = trvmodule.Nodes.Item(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub Fill_trvModule()
        Try
            'Add item to treeview Node
            '    gloUC_PatientStrip.enumPatientInfo.
            Dim i, j As Int16



            Dim sModuleName As String
            For i = 1 To [Enum].GetValues(GetType(gloUserControlLibrary.gloUC_PatientStrip.enumFormName)).Length - 1
                sModuleName = [Enum].GetValues(GetType(gloUserControlLibrary.gloUC_PatientStrip.enumFormName)).GetValue(i).ToString
                If sModuleName = "Medication" Then     ''Bugs no 1222 - remove medication
                    Continue For
                ElseIf [Enum].GetValues(GetType(gloUserControlLibrary.gloUC_PatientStrip.enumFormName)).GetValue(i) = 1 Then
                    sModuleName = "Prescription/Medication"
                End If
                trvmodule.Nodes.Add(BuildString(sModuleName))
            Next




            'Fill dataTable with selected value for cheklist item
            Dim strSelectQry As String
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim dt As DataTable
            strSelectQry = "Select nModule,sInfo from PatientControl_DTL Order By nModule "
            oDB.Connect(GetConnectionString)
            dt = oDB.ReadQueryDataTable(strSelectQry)
            oDB.Disconnect()

            'Loop for trerview item
            For i = 0 To trvmodule.GetNodeCount(False) - 1
                Dim str As String = ""
                If IsNothing(dt) = False Then
                    'Loop for datatable
                    For j = 0 To dt.Rows.Count - 1
                        'if treeview Node is equal to value present in datatable for cheklist then fill str
                        If dt.Rows(j)("nModule") = i + 1 Then
                            If str = "" Then
                                str = dt.Rows(j)("sInfo")
                            Else
                                str = str & "," & dt.Rows(j)("sInfo")
                            End If
                        End If
                    Next
                End If
                'Add str to Treeview node
                trvmodule.Nodes(i).Tag = str

            Next
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

            oDB.Dispose()
            oDB = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub Fill_chklistmodule()

        Dim sPatientDetail As String
        For i As Integer = 0 To [Enum].GetValues(GetType(gloUserControlLibrary.gloUC_PatientStrip.enumPatientInfo)).Length - 1
            sPatientDetail = [Enum].GetValues(GetType(gloUserControlLibrary.gloUC_PatientStrip.enumPatientInfo)).GetValue(i).ToString
            chklistmodule.Items.Add(BuildString(sPatientDetail))
        Next



    End Sub

    Private Sub trvmodule_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvmodule.BeforeSelect
        Try
            'set previous node cheked item to collection
            If IsNothing(PrevNode) = False Then
                PrevNode.Tag = SetCollection()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub trvmodule_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvmodule.AfterSelect
        Try

            'set value for checklist item 
            If IsNothing(trvmodule.SelectedNode) = False Then
                SetValueChecked(trvmodule.SelectedNode.Tag)
                PrevNode = trvmodule.SelectedNode
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Function SetCollection() As String
        Try

            Dim str As String = ""
            For i As Integer = 0 To chklistmodule.Items.Count - 1
                If chklistmodule.GetItemChecked(i) = True Then
                    If str = "" Then
                        str = i
                    Else
                        str = str & "," & i
                    End If
                End If
            Next
            Return str
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Private Sub SetValueChecked(ByVal str As String)
        Try
            'set checked value for chklistBox item
            Dim strSplit() As String
            Dim i, j As Int16
            Dim intCount As Integer

            For j = 0 To chklistmodule.Items.Count - 1
                chklistmodule.SetItemChecked(j, False)
            Next

            If str = "" Then
                'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
                SetSelectAll()
                Exit Sub
            End If

            strSplit = Split(str, ",")

            For i = 0 To strSplit.Length - 1
                For j = 0 To chklistmodule.Items.Count - 1
                    If Convert.ToUInt16(strSplit(i)) = j Then
                        chklistmodule.SetItemChecked(j, True)
                        intCount += 1
                    End If
                Next
            Next

            'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
            If chklistmodule.Items.Count = intCount Then
                SetClearAll()
            Else
                SetSelectAll()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SavePtControlDTL()
        Try
            '' To Save the Last Selected Patient Details in tag of the Form
            Dim etrv As System.Windows.Forms.TreeViewCancelEventArgs = Nothing
            Dim sender As Object = Nothing
            trvmodule_BeforeSelect(sender, etrv)
            'save checked item to database
            If (IsNothing(dtPatientControl_DTL) = False) Then
                dtPatientControl_DTL.Dispose()
                dtPatientControl_DTL = Nothing
            End If
            dtPatientControl_DTL = New DataTable()
            Dim c1 As New DataColumn("nModule", System.Type.GetType("System.Int64"))
            dtPatientControl_DTL.Columns.Add(c1)
            Dim c2 As New DataColumn("sInfo", System.Type.GetType("System.String"))
            dtPatientControl_DTL.Columns.Add(c2)
            For i As Int16 = 0 To trvmodule.GetNodeCount(False) - 1
                SaveContolInfo(i + 1, trvmodule.Nodes(i).Tag)
            Next
            CopyDataToPatientControl_DTL(GetConnectionString(), dtPatientControl_DTL)
            'Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''funnction is re writen for performance optimization 
    Private Sub SaveContolInfo(ByVal nModule As Int16, ByVal sInfo As String)
        Try
            Dim strinfosave() As String
            strinfosave = Split(sInfo, ",")
            'Delete previous data from dataTable for selected Node
            If sInfo <> "" Then
                'Fill new data for selected Node
                Dim MyRow As DataRow
                For i As Integer = 0 To strinfosave.Length - 1
                    MyRow = dtPatientControl_DTL.NewRow()
                    MyRow("nModule") = nModule
                    MyRow("sInfo") = strinfosave(i)
                    dtPatientControl_DTL.Rows.Add(MyRow)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub tlsp_PtControlCustomization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PtControlCustomization.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SavePtControlDTL()
                Case "Close"
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
    Private Sub SetSelectAll()
        tlbtnSelectAll.Tag = "Select"
        tlbtnSelectAll.Text = "Select All"
        tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
        tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
        tlbtnSelectAll.ToolTipText = "Select All "

    End Sub

    'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
    Private Sub SetClearAll()
        tlbtnSelectAll.Tag = "Clear"
        tlbtnSelectAll.Text = "Clear All"
        tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
        tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
        tlbtnSelectAll.ToolTipText = "Clear All "

    End Sub

    Private Sub tlbtnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbtnSelectAll.Click
        Try
            tlbtnSelectAll.BackgroundImage = Nothing
            If tlbtnSelectAll.Tag.ToString() = "Select" Then
                'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
                SetClearAll()
                SelectAllClearAll(True)
            Else
                'Bug #82408: EMR: Patint Control CUstomization- Select all button funcytionality io not working properly
                SetSelectAll()
                SelectAllClearAll(False)
            End If
        Catch ex As Exception

            Throw
        End Try
    End Sub

    Private Sub SelectAllClearAll(ByVal [select] As Boolean)
        For i As Integer = 0 To chklistmodule.Items.Count - 1
            If [select] = True Then
                chklistmodule.SetItemChecked(i, True)
            Else
                chklistmodule.SetItemChecked(i, False)
            End If
        Next
    End Sub


    Private Function BuildString(ByVal sText As String) As String
        Try
            Dim sModifiedText As New System.Text.StringBuilder

            '' FIRST CHARACTER IS NOT IN LOOP '' SO APPEND IT HERE ''
            sModifiedText.Append(sText.Chars(0))

            '' FIND CAPITAL AND INSERT SPACE ''
            For iChr As Integer = 1 To sText.Length - 1
                If iChr <> sText.Length - 1 Then
                    '' IF RIGHT MOST CAPITAL CHAR FOUND '' INSERT SPACE BEFORE THAT CHAR ''
                    If Char.IsUpper(sText.Chars(iChr)) And (Char.IsUpper(sText.Chars(iChr + 1)) = False) Then
                        sModifiedText.Append(" ")
                    End If
                End If
                sModifiedText.Append(sText.Chars(iChr))
            Next
            ''Added by Mayuri:20100416-To replace Or,Of and Pcp
            Dim str As String = ""
            str = sModifiedText.ToString

            If str.Contains(" Of ") Then
                str = str.Replace(" Of ", " of ")
            End If

            If str.Contains(" Or ") Then
                str = str.Replace(" Or ", " or ")
            End If

            If str.Contains(" Pcp") Then
                str = str.Replace(" Pcp", " PCP")
            End If

            Return str.ToString

        Catch ex As Exception
            Return sText
        End Try
    End Function

    ''' <summary>
    ''' Function writen for insert bulk data to PatientControl_DTL table
    ''' </summary>
    ''' <param name="connectionString"></param>
    ''' <param name="table"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub CopyDataToPatientControl_DTL(ByVal connectionString As [String], ByVal table As DataTable)
        Try
            'Retrieving old records in dataTable 
            Dim strSelectQry As String
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim dtLoad As DataTable
            strSelectQry = "Select nModule,sInfo from PatientControl_DTL Order By nModule "
            oDB.Connect(GetConnectionString)
            dtLoad = oDB.ReadQueryDataTable(strSelectQry)
            oDB.Disconnect()

            Dim mapping1 As New SqlBulkCopyColumnMapping("nModule", "nModule")
            Dim mapping2 As New SqlBulkCopyColumnMapping("sInfo", "sInfo")
            Dim oCon As New SqlConnection(GetConnectionString)
            Dim oCmd As New SqlCommand
            Dim dbTran As SqlTransaction
            oCmd.Connection = oCon
            oCmd.CommandType = CommandType.Text
            oCmd.CommandText = "Delete PatientControl_DTL"
            oCon.Open()
            dbTran = oCon.BeginTransaction()
            oCmd.Transaction = dbTran
            oCmd.ExecuteNonQuery()
            'Delete previous data from dataTable for selected Node

            Dim bulkCopy As New SqlBulkCopy(oCon, SqlBulkCopyOptions.Default, dbTran)
            bulkCopy.BatchSize = table.Rows.Count
            bulkCopy.BulkCopyTimeout = 0

            bulkCopy.ColumnMappings.Add(mapping1)
            bulkCopy.ColumnMappings.Add(mapping2)
            bulkCopy.DestinationTableName = "PatientControl_DTL"
            'bulkCopy.SqlRowsCopied += New SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied)
            AddHandler bulkCopy.SqlRowsCopied, AddressOf bulkCopy_SqlRowsCopied
            bulkCopy.NotifyAfter = table.Rows.Count
            bulkCopy.WriteToServer(table)
            bulkCopy.Close()

            dbTran.Commit()
            oCon.Close()

            AddAuditLogs(dtLoad, table)


            If (IsNothing(dtPatientControl_DTL) = False) Then
                dtPatientControl_DTL.Dispose()
                dtPatientControl_DTL = Nothing
            End If

            bulkCopy = Nothing
            mapping1 = Nothing
            mapping2 = Nothing


            If oCmd IsNot Nothing Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If
            oCon.Dispose()
            oCon = Nothing
            dbTran.Dispose()
            dbTran = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddAuditLogs(ByVal dtLoad As DataTable, ByVal dtSave As DataTable)
        Dim sAuditLog As String = ""

        sAuditLog = GetChangedPCModules(dtLoad, dtSave)

        Dim sSeparateAuditLogLine As String() = sAuditLog.Split("|")


        Dim i As Integer
        Dim sModuleName As String = ""
        Dim ResultModuleName As String = ""

        If sAuditLog <> "" Then
            For i = 0 To sSeparateAuditLogLine.Length - 1
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.PatientControlCustomization, gloAuditTrail.ActivityType.Save, sSeparateAuditLogLine(i), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Next
        Else
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.PatientControlCustomization, gloAuditTrail.ActivityType.Save, "Viewed and closed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If

    End Sub


    Private Function GetChangedPCModules(ByVal dtLoad As DataTable, ByVal dtSave As DataTable) As String

        Dim Con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _param As SqlParameter = Nothing
        Dim sAuditLog As String = String.Empty

        Try

            Con = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("SelectChangedPCModules", Con)
            cmd.CommandType = CommandType.StoredProcedure

            _param = cmd.Parameters.AddWithValue("@OTable", dtLoad)
            _param.SqlDbType = SqlDbType.Structured
            _param = Nothing

            _param = cmd.Parameters.AddWithValue("@NTable", dtSave)
            _param.SqlDbType = SqlDbType.Structured
            _param = Nothing

            _param = cmd.Parameters.AddWithValue("@sAuditLog", "")
            _param.SqlDbType = SqlDbType.VarChar
            _param.Direction = ParameterDirection.InputOutput
            _param.Size = 8000

            Con.Open()
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()

            sAuditLog = Convert.ToString(cmd.Parameters("@sAuditLog").Value)
            Con.Close()

            Return sAuditLog

            '_MedicationDBLayer.FetchMedicationforUpdate(_CurrentVisitID, m_FilterType, _CurrentVisitDate, _Medications, _OldMedications)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            Throw ex
        Finally
            If IsNothing(Con) = False Then
                Con.Dispose()
                Con = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If IsNothing(_param) = False Then
                _param = Nothing
            End If

        End Try
    End Function


    ''' <summary>
    ''' Function writen for event SqlRowsCopied indicate rows copied by using bulk copy.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub bulkCopy_SqlRowsCopied(ByVal sender As Object, ByVal e As SqlRowsCopiedEventArgs)
        'MessageBox.Show([String].Format("{0} Rows have been copied.", e.RowsCopied.ToString()))
        Me.Close()
    End Sub

End Class
