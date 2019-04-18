
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Text
Imports System.Text.RegularExpressions


Public Class frmCV_StressTests

    Private WithEvents dgCustomGrid As CustomTask
    Private Col_Check As Integer = 2
    Private Col_Name As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Count As Integer = 3
    Dim _TempRx As String
    Dim _Temprow As Int32

    ' Dim oDiag As New DataTable
    'Dim oCPT As New DataTable
    Dim strLst As String = ""
    'Dim dt As DataTable
    ' Dim CptList As New ArrayList

    Private mPatientID As Int64
    Private mVisitID As Int64
    Private mExamId As Int64
    Private mClinicID As Int64
    Private mdtDateOfStudy As DateTime
    Private mStressID As Long = 0
    Private MainStressID As Long = 0
    Dim Sr As Int32 = 0
    Dim r As Integer

    Dim sSelectedCPT As String = ""
    Dim sSelectedTestTp As String = ""

    Public blnIsNew As Boolean
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Dim objCath As New Cls_CardioVasculars



    Private Sub tsCatheterization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsCatheterization.ItemClicked
        Dim sErrorMessage = ""
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    sErrorMessage = ValidateForm()
                    If sErrorMessage = "" Then
                        SaveStressTest()
                        Me.Close()
                    Else
                        MessageBox.Show(sErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateForm() As String
        Dim sReturnMessage As String = ""
        Try
            If Not IsPositiveNumber(txt_TotExTime.Text) AndAlso Not txt_TotExTime.Text = Nothing Then
                sReturnMessage = "Total Exercise Time is not in correct format."
                txt_TotExTime.Focus()
                Exit Try
            End If


            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_EjectionFraction.Text) AndAlso Not txt_EjectionFraction.Text = Nothing Then
                    sReturnMessage = "Ejection Fraction is not in correct format."
                    txt_EjectionFraction.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_RestHeartRt.Text) AndAlso Not txt_RestHeartRt.Text = Nothing Then
                    sReturnMessage = "Resting Heart Rate is not in correct format."
                    txt_RestHeartRt.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_RestBPMax.Text) AndAlso Not txt_RestBPMax.Text = Nothing Then
                    sReturnMessage = "Resting BP Max is not in correct format. "
                    txt_RestBPMax.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_RestBPMin.Text) AndAlso Not txt_RestBPMin.Text = Nothing Then
                    sReturnMessage = "Resting BP Min is not in correct format. "
                    txt_RestBPMin.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_PeakHeartRt.Text) AndAlso Not txt_PeakHeartRt.Text = Nothing Then
                    sReturnMessage = "Peak Heart Rate is not in correct format. "
                    txt_PeakHeartRt.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_PeakBPMax.Text) AndAlso Not txt_PeakBPMax.Text = Nothing Then
                    sReturnMessage = "Peak BP Max is not in correct format. "
                    txt_PeakBPMax.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txt_PeakBPMin.Text) AndAlso Not txt_PeakBPMin.Text = Nothing Then
                    sReturnMessage = "Peak BP Min is not in correct format. "
                    txt_PeakBPMin.Focus()
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return sReturnMessage

    End Function

    Private Function SaveStressTest()

        Try
            '   Dim objCath As Cls_CardioVascular
            ' Dim Arrlist As New ArrayList

            Dim i As Integer ', j As Integer
            ''''''''''''''
            '''''''''''''
            If mPatientID = 0 Then
                mPatientID = mPatientID
            End If


            If mClinicID = 0 Then
                mClinicID = 1
            End If

            If mdtDateOfStudy = Nothing Then
                mdtDateOfStudy = DtDateOfStudy.Value.Date
            End If

            ''''''''''
            If mVisitID = 0 Then
                mVisitID = mVisitID
                'Try
                '    mVisitID = GenerateVisitID(DtDateOfStudy.Value.Date, mPatientID)
                'Catch ex As Exception
                '    mVisitID = 0
                'End Try
            End If
            ''''''''''

            ''''''''''
            ''If mdtDateOfStudy <> DtDateOfStudy.Value.Date Then
            ''    Try
            ''        mVisitID = GenerateVisitID(DtDateOfStudy.Value.Date, mPatientID)
            ''    Catch ex As Exception
            ''        mVisitID = 0
            ''    End Try
            ''End If
            ''''''''''

            '''''''Physicians
            Dim strPhysicians As New StringBuilder
            For i = 0 To lstPhysicians.Items.Count - 1
                If i > 0 Then
                    strPhysicians.Append(" | ")
                End If
                strPhysicians.Append(lstPhysicians.Items(i).ToString())
            Next
            '''''''Physicians


            ''''''''Add all CPTs to Grid
            Dim x As Integer
            Dim bCPTFound As Boolean = False
            For x = 0 To lstCPTcode.Items.Count - 1
                For i = 0 To C1CPTTest.Rows.Count - 1
                    If i < C1CPTTest.Rows.Count Then
                        If C1CPTTest.GetDataDisplay(i, 0).ToString.Trim = lstCPTcode.Items(x).ToString.Trim Then
                            bCPTFound = True
                            Exit For
                        End If
                    End If
                Next
                If bCPTFound = False Then
                    '''''' for only cpt - if testtype is blank=                   
                    C1CPTTest.Rows.Add()
                    If Sr >= C1CPTTest.Rows.Count Then
                        Sr = C1CPTTest.Rows.Count - 1
                    End If
                    C1CPTTest.SetData(Sr, 0, lstCPTcode.Items(x).ToString().Trim())
                    C1CPTTest.SetData(Sr, 1, "")
                    Sr = Sr + 1
                Else
                    bCPTFound = False
                End If
            Next
            ''''''''Add all CPTs to Grid



            ''''''''''''''' all values for master entry

            ''''''''''''''
            If txt_RestHeartRt.Text.Trim = "" Then txt_RestHeartRt.Text = "0"
            If txt_RestBPMin.Text.Trim = "" Then txt_RestBPMin.Text = "0"
            If txt_RestBPMax.Text.Trim = "" Then txt_RestBPMax.Text = "0"
            If txt_PeakHeartRt.Text.Trim = "" Then txt_PeakHeartRt.Text = "0"
            If txt_PeakBPMin.Text.Trim = "" Then txt_PeakBPMin.Text = "0"
            If txt_PeakBPMax.Text.Trim = "" Then txt_PeakBPMax.Text = "0"
            Dim objStressDBLayer As New ClsCVStressDbLayer
            MainStressID = objStressDBLayer.SaveStressTest(mStressID, mClinicID, mPatientID, mExamId, mVisitID, DtDateOfStudy.Value.Date, 0, "", "", strPhysicians.ToString(), "", txt_TotExTime.Text, CType(txt_RestHeartRt.Text, Long), CType(txt_RestBPMin.Text, Long), CType(txt_RestBPMax.Text, Long), CType(txt_PeakHeartRt.Text, Long), CType(txt_PeakBPMin.Text, Long), CType(txt_PeakBPMax.Text, Long), txt_EjectionFraction.Text, txtNarrativeSummary.Text, True)
            ''''''''''''''
            ''''''''''''''' all values for master entry

            ''''''''''''''' all values for detail entry   

            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For i = 0 To C1CPTTest.Rows.Count - 1
                If C1CPTTest.GetDataDisplay(i, 0).ToString().Trim <> "" Then
                    mStressID = objStressDBLayer.SaveStressTest((MainStressID + (i + 1)), mClinicID, mPatientID, mExamId, mVisitID, DtDateOfStudy.Value.Date, MainStressID, C1CPTTest.GetDataDisplay(i, 0).ToString(), C1CPTTest.GetDataDisplay(i, 1).ToString(), , C1CPTTest.GetDataDisplay(i, 2).ToString())
                End If
            Next
            objStressDBLayer = Nothing

            ''''''''''''''' all values for detail entry

            ''''''''''''''            

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return Nothing

    End Function


    Private Sub btnBrowseCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCPT.Click
        strLst = "cpt"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstCPTcode)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub


    Private Sub btnClearCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.Click
        While (lstCPTcode.SelectedItems.Count > 0)
            CPTTestTp(lstCPTcode.SelectedItems(0).ToString(), , True)
            lstCPTcode.Items.Remove(lstCPTcode.SelectedItems(0))
        End While
    End Sub

    Private Sub BtnClearAllCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllCPT.Click
        lstCPTcode.Items.Clear()
        C1CPTTest.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
        lstTestType.Items.Clear()
    End Sub


    Private Sub LoadUserGrid()
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                pnlCustomTask.Width = 450
               

                dgCustomGrid.Width = pnlCustomTask.Width

                pnlCustomTask.Height = 250
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlCustomTask.Height
                dgCustomGrid.txtsearch.Width = 150


                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False

                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Cardiac Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Remove customGrid control to form 
    Private Sub RemoveControl()
        If Not IsNothing(dgCustomGrid) Then
            'pnlWordObj.Controls.Remove(dgCustomGrid)
            pnlCustomTask.Controls.Remove(dgCustomGrid)
            dgCustomGrid.Visible = False
            dgCustomGrid.Dispose()
            dgCustomGrid = Nothing
        End If
    End Sub

    'Add customGrid control to form 
    Private Sub AddControl()

        If Not IsNothing(dgCustomGrid) Then
            RemoveControl()
        End If
        dgCustomGrid = New CustomTask
        pnlCustomTask.Controls.Add(dgCustomGrid)
        pnlCustomTask.BringToFront()
        ''''''''''''''''''''''
        ''dgCustomGrid.lblCaption.Visible = True
        ''''''''''''''''''''''

        Dim y As Int64
        Dim x As Int64

        If strLst = "cpt" Then
            y = 210
            x = 210
            'dgCustomGrid.lblCaption.Text = " CPT List "
        ElseIf strLst = "testtype" Then
            y = 210
            x = 290 '' btnBrowseTesttype.Left '' 410

            'dgCustomGrid.lblCaption.Text = " Test Type List "
        ElseIf strLst = "physician" Then
            y = 210
            x = 460 '' 640
            'dgCustomGrid.lblCaption.Text = " Physician List "
        End If

        pnlCustomTask.Location = New Point(x, y)
        pnlCustomTask.Visible = True
        dgCustomGrid.Visible = True
        pnlCustomTask.BringToFront()
        dgCustomGrid.BringToFront()


    End Sub

    Private Sub BindUserGrid()
        Try
            Dim dt As DataTable = Nothing
            If strLst = "cpt" Then
                dt = FillCPT()
                pnlCustomTask.Width = 450
            ElseIf strLst = "testtype" Then
                dt = FillTestType()
                pnlCustomTask.Width = 400
            ElseIf strLst = "physician" Then
                dt = FillPhysician()
                pnlCustomTask.Width = 400
            End If
            ''''''''
            dgCustomGrid.Width = pnlCustomTask.Width
            ''''''''
            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.Copy().DefaultView)
            End If
            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.1
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            dgCustomGrid.C1Task.ScrollBars = ScrollBars.Both
            ''dgCustomGrid.C1Task.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible
            dgCustomGrid.C1Task.ExtendLastCol = True
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Catheterization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CustomDrugsGridStyle()

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_Count
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 0.1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Name, "Name")
            .Cols(Col_Name).Width = _TotalWidth * 0.45
            ' .Cols(Col_DrugName).AllowEditing = False

        End With

    End Sub

    Private Sub SetCheckValues(ByVal LstBx As ListBox)
        Dim k As Integer
        For k = 0 To LstBx.Items.Count - 1
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.GetItem(i, 1).ToString.Trim = LstBx.Items(k).ToString.Trim Then
                    dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlCustomTask.Visible = False

        '''''''''''''''
        LockControls(Me, pnlCustomTask, True)
        '''''''''''''''
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try
            Dim i As Int32
            'Dim j As Int32-

            '''''''''''''''
            LockControls(Me, pnlCustomTask, True)
            '''''''''''''''

            '''''''''''''''''''''
            If strLst = "cpt" Then
                lstCPTcode.Items.Clear()
                C1CPTTest.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            ElseIf strLst = "testtype" Then
                lstTestType.Items.Clear()
            ElseIf strLst = "physician" Then
                lstPhysicians.Items.Clear()
            End If
            '''''''''''''''''''''           
            ''''''''''''
            If strLst = "testtype" Then
                If C1CPTTstResult.Rows.Count > 0 Then
                    C1CPTTstResult.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
                    C1CPTTstResult.Rows.Remove(0)
                End If
                
                For j As Integer = 0 To C1CPTTest.Rows.Count - 1
                    C1CPTTstResult.Rows.Add()
                    For k As Integer = 0 To 2
                        If Not (C1CPTTest.Rows(j)(k) Is Nothing) Then
                            C1CPTTstResult.SetData(j, k, C1CPTTest.Rows(j)(k).ToString())
                        End If

                    Next
                Next
            End If
            ''''''''''''
            '''''''''''''''''''''
            dgCustomGrid.txtsearch.Text = ""
            '''''''''''''''''''''

            For i = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If strLst = "cpt" Then
                        lstCPTcode.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)

                        '''''''
                        ''''''' CPTTestTp(lstCPTcode.Items(lstCPTcode.Items.Count - 1).ToString(), , , True)
                        '''''''

                    ElseIf strLst = "testtype" Then
                        lstTestType.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)

                        '' sSelectedTestTp = dgCustomGrid.GetItem(i, 1).ToString

                        If lstCPTcode.SelectedItems.Count = 1 Then
                            CPTTestTp(lstCPTcode.SelectedItem.ToString())
                        End If
                        ''''''''''''


                    ElseIf strLst = "physician" Then
                        lstPhysicians.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    End If
                End If
            Next

            ''''''''''''
            If strLst = "testtype" Then
                Dim r As Integer = 0

                C1CPTTstResult.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
                C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
                If C1CPTTstResult.Rows.Count > 0 Then
                    C1CPTTstResult.Row = 0
                    C1CPTTest.Row = 0
                    For j As Integer = 0 To C1CPTTest.Rows.Count - 1
                        If C1CPTTstResult.Rows.Count > 0 Then
                            r = C1CPTTstResult.FindRow(C1CPTTest.Rows(j)(0), C1CPTTstResult.Row, 0, False, True, False)
                            If r >= 0 Then
                                '''''                    
                                If (C1CPTTest.GetDataDisplay(j, 0).ToString() = C1CPTTstResult.GetDataDisplay(r, 0).ToString()) And (C1CPTTest.GetDataDisplay(j, 1).ToString() = C1CPTTstResult.GetDataDisplay(r, 1).ToString()) Then
                                    C1CPTTest.SetData(j, 2, C1CPTTstResult.Rows(r)(2))
                                End If
                            End If
                        End If
                        If C1CPTTstResult.Rows.Count > r + 1 Then
                            C1CPTTstResult.Row = r + 1
                        End If
                    Next
                End If
                ' '' ''C1CPTTstResult.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            End If

            ''''''''''''

            ''  LstDiagnosis.Enabled = True
            pnlCustomTask.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))

            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGrid.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGrid.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(0).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "
            'OR " _
            '                                                 & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
            '                                                & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "



            dgCustomGrid.Enabled = False
            dgCustomGrid.datasource(dvPatient)
            dgCustomGrid.Enabled = True
            Me.Cursor = Cursors.Default
            dgCustomGrid.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CPTTestTp(ByVal sCPT As String, Optional ByVal sTestTp As String = "", Optional ByVal RemoveItm As Boolean = False, Optional ByVal AddCpt As Boolean = False, Optional ByVal AddResult As Boolean = False, Optional ByVal sTestReasult As String = "")

        Dim j As Integer

        ''If sTestReasult = "" And cmbresult.Text <> "" Then
        ''    sTestReasult = cmbresult.Text
        ''End If
        ''''''''''
        If AddResult = True Then
            ''''''''''
            C1CPTTest.Row = 0
            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For j = 0 To C1CPTTest.Rows.Count - 1
                r = C1CPTTest.FindRow(sCPT, C1CPTTest.Row, 0, False, True, False)
                If r >= 0 Then
                    '''''                    
                    If (sCPT = C1CPTTest.GetDataDisplay(r, 0).ToString()) And (sTestTp = C1CPTTest.GetDataDisplay(r, 1).ToString()) Then
                        C1CPTTest.SetData(r, 2, sTestReasult)
                        Exit Sub
                    End If
                    If C1CPTTest.Rows.Count > r + 1 Then
                        C1CPTTest.Row = r + 1
                    End If
                End If
            Next
            ''''''''''
        End If
        ''''''''''

        For j = 0 To C1CPTTest.Rows.Count - 1
            If j < C1CPTTest.Rows.Count - 1 Then
                If C1CPTTest.GetDataDisplay(j, 0).ToString().Trim() = "" Then
                    C1CPTTest.Rows.Remove(j)
                End If
            End If
        Next
        If RemoveItm = False Then
            ''''''remove data
            If Sr > 0 Then
                If sTestTp <> "" Then
                    'C1CPTTest.Row = C1CPTTest.FindRow(sTestTp, 0, 1, False, True, False)
                    'If C1CPTTest.Row >= 0 Then
                    '    C1CPTTest.Rows.Remove(C1CPTTest.Row)
                    'End If
                Else
                    For j = 0 To C1CPTTest.Rows.Count - 1
                        C1CPTTest.Row = C1CPTTest.FindRow(sCPT, 0, 0, False, True, False)
                        If C1CPTTest.Row >= 0 Then
                            C1CPTTest.Rows.Remove(C1CPTTest.Row)
                        End If
                    Next
                End If
                Sr = C1CPTTest.Rows.Count
            End If
            ''''''add data
            If sTestTp <> "" Then
                C1CPTTest.Rows.Add()
                C1CPTTest.SetData(Sr, 0, sCPT)
                C1CPTTest.SetData(Sr, 1, sTestTp)
                ''''''''''
                '' ''C1CPTTest.SetData(Sr, 2, sTestReasult)
                ''''''''''
                Sr = Sr + 1
            Else
                If AddCpt = True Then
                    '''''' for only cpt  
                    ' '' ''AddVal(lstCPTcode)
                    ''C1CPTTest.Rows.Add()
                    ''C1CPTTest.SetData(Sr, 0, sCPT)
                    ''C1CPTTest.SetData(Sr, 1, "")
                    ''Sr = Sr + 1
                Else
                    If lstTestType.Items.Count > 0 Then
                        For j = 0 To lstTestType.Items.Count - 1
                            C1CPTTest.Rows.Add()
                            C1CPTTest.SetData(Sr, 0, sCPT)
                            C1CPTTest.SetData(Sr, 1, lstTestType.Items(j).ToString())
                            ''''''''''
                            '' ''C1CPTTest.SetData(Sr, 2, sTestReasult)
                            ''''''''''
                            Sr = Sr + 1
                        Next
                    Else
                        '''''''' for only cpt - if testtype is blank
                        ''For j = 0 To lstCPTcode.Items.Count - 1
                        ''    C1CPTTest.Rows.Add()
                        ''    C1CPTTest.SetData(Sr, 0, sCPT)
                        ''    C1CPTTest.SetData(Sr, 1, "")
                        ''    Sr = Sr + 1
                        ''Next
                    End If
                End If

            End If
        Else
            ''''''remove data only

            If sTestTp <> "" Then
                Dim r As Integer
                C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
                If C1CPTTest.Row < 0 Then
                    C1CPTTest.Row = 0
                End If
                C1CPTTest.Row = 0
                For j = 0 To C1CPTTest.Rows.Count - 1
                    r = C1CPTTest.FindRow(sSelectedCPT, C1CPTTest.Row, 0, False, True, False)
                    If r >= 0 Then
                        ''''' 
                        ''If C1CPTTest.Rows.Count > r + 1 Then
                        ''    C1CPTTest.Row = r + 1
                        ''End If
                        If (sTestTp = C1CPTTest.GetDataDisplay(r, 1).ToString()) Then
                            ''''''''C1CPTTest.Row = r
                            ''C1CPTTest.FindRow(sTestTp, r, 1, False, True, False)
                            If r >= 0 And r < C1CPTTest.Rows.Count Then
                                C1CPTTest.Rows.Remove(r)
                            End If
                            Exit For
                        End If
                        If C1CPTTest.Rows.Count > r + 1 Then
                            C1CPTTest.Row = r + 1
                        End If
                    End If
                Next
            Else
                For j = 0 To C1CPTTest.Rows.Count - 1
                    C1CPTTest.Row = C1CPTTest.FindRow(sCPT, 0, 0, False, True, False)
                    If C1CPTTest.Row >= 0 Then
                        '''''''remove from listbox
                        lstTestType.Items.Remove(C1CPTTest.GetDataDisplay(C1CPTTest.Row, 1).ToString())
                        '''''''''''''''''''''''''''''
                        C1CPTTest.Rows.Remove(C1CPTTest.Row)
                    End If
                Next
            End If
        End If
    End Sub

    Private Function AddVal(ByVal LstBx As ListBox) As Boolean
        For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
            If dgCustomGrid.GetItem(i, 0).ToString.Trim <> "" Then
                LstBx.Items.Add(dgCustomGrid.GetItem(i, 0).ToString.Trim)
            End If
        Next
        Return Nothing
    End Function

    Public Function FillCPT() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "select Distinct (isNull(rtrim(sCPTcode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where  sCPTcode <>'' AND sDescription<>''"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function FillResults() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sDescription as [Test Type] from Category_MST where sCategoryType='Cardio Test Result' union (Select ' ' as [Test Type] from Category_MST where 1=1)  order by sDescription"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function FillTestType() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sDescription as [Test Type] from Category_MST where sCategoryType='Cardio Test Type' order by sDescription"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function


    Public Function FillPhysician() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sLoginName + ' - ' + isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as [Login Name] from User_MST order by sLoginName "
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Sub New(ByVal PatientId As Int64, ByVal VisitID As Int64, ByVal dtStudyDate As Date, Optional ByVal ExamID As Int64 = 0, Optional ByVal ClinicID As Int64 = 1)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        mPatientID = PatientId
        mVisitID = VisitID
        mdtDateOfStudy = dtStudyDate
        dtDateOfStudy.Value = mdtDateOfStudy
        mExamId = ExamID
        mClinicID = ClinicID
    End Sub

    ''Public Sub New()

    ''    ' This call is required by the Windows Form Designer.
    ''    InitializeComponent()

    ''    ' Add any initialization after the InitializeComponent() call.

    ''End Sub

    Private Sub frmCV_StressTests_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If IsNothing(gloUC_PatientStrip1) = False Then
                Me.Controls.Remove(gloUC_PatientStrip1)
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
            objCath.Dispose()

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, "Close Stress Test", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_StressTests_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '''''''''Patient Strip
        If IsNothing(gloUC_PatientStrip1) = False Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        gloUC_PatientStrip1.BringToFront()
        pnlMain.BringToFront()
        gloUC_PatientStrip1.ShowDetail(mPatientID, gloUC_PatientStrip.enumFormName.None)
        Me.Controls.Add(gloUC_PatientStrip1)
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        '''''''''Patient Strip
        ' '' ''If blnIsNew = False Then
        ' '' ''    'Check if form is loaded in edit mode
        ' '' ''    DtDateOfStudy.Enabled = False
        ' '' ''    '' Fill()
        ' '' ''Else
        ' '' ''    'if form is loaded in new mode then check if visit already exists
        ' '' ''    If mVisitID = 0 Then
        ' '' ''        'if visit does not exists then load the form in new mode                
        ' '' ''    Else
        ' '' ''        'if visit exists for the given date then pull the data for that date                
        ' '' ''    End If
        ' '' ''    DtDateOfStudy.Enabled = True
        ' '' ''End If
        ''''''''''''''''''
        Dim dtRslts As DataTable
        dtRslts = FillResults()
        cmbresult.Items.Clear()
        If Not IsNothing(dtRslts) Then            
            ''cmbresult.DataSource() = dtRslts
            ''cmbresult.DisplayMember = dtRslts.Columns(0).ColumnName.ToString()
            ''cmbresult.ValueMember = dtRslts.Columns(0).ColumnName.ToString()
            For Each r As DataRow In dtRslts.Rows
                cmbresult.Items.Add(r(0).ToString())
            Next
            dtRslts.Dispose()
            dtRslts = Nothing
        End If    
        ''''''''''''''''''
        FillStressTest()
        '''''''''

    End Sub

    Public Function IsNumber(ByVal strNumber As [String]) As Boolean
        Try
            Dim objNotNumberPattern As New Regex("[^0-9.-]")
            Dim objTwoDotPattern As New Regex("[0-9]*[.][0-9]*[.][0-9]*")
            Dim objTwoMinusPattern As New Regex("[0-9]*[-][0-9]*[-][0-9]*")
            Dim strValidRealPattern As [String] = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$"
            Dim strValidIntegerPattern As [String] = "^([-]|[0-9])[0-9]*$"
            Dim objNumberPattern As New Regex("(" & strValidRealPattern & ")|(" & strValidIntegerPattern & ")")
            Return Not objNotNumberPattern.IsMatch(strNumber) AndAlso Not objTwoDotPattern.IsMatch(strNumber) AndAlso Not objTwoMinusPattern.IsMatch(strNumber) AndAlso objNumberPattern.IsMatch(strNumber)

            'objNotNumberPattern = Nothing
            'objTwoDotPattern = Nothing
            'objTwoMinusPattern = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Public Function IsPositiveNumber(ByVal strNumber As [String]) As Boolean
        Try
            Dim objNotPositivePattern As New Regex("[^0-9.]")
            Dim objPositivePattern As New Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$")
            Dim objTwoDotPattern As New Regex("[0-9]*[.][0-9]*[.][0-9]*")
            Return Not objNotPositivePattern.IsMatch(strNumber) AndAlso objPositivePattern.IsMatch(strNumber) AndAlso Not objTwoDotPattern.IsMatch(strNumber)

            'objNotPositivePattern = Nothing
            'objPositivePattern = Nothing
            'objTwoDotPattern = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub BtnClearAllTesttype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllTesttype.Click
        '''''''''''''''''to clear test types from selected cpt - lstTestType.Items.Clear()       
        Dim i As Integer
        If sSelectedCPT <> "" Then
            For i = 0 To lstTestType.Items.Count - 1
                CPTTestTp(lstCPTcode.SelectedItems(0).ToString(), lstTestType.Items(0).ToString(), True)
                lstTestType.Items.Remove(lstTestType.Items(0))
            Next
        Else
            lstTestType.Items.Clear()
        End If
    End Sub

    Private Sub btnClearTesttype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTesttype.Click
        While (lstTestType.SelectedItems.Count = 1)
            If sSelectedCPT <> "" Then
                If sSelectedTestTp = "" Then
                    sSelectedTestTp = lstTestType.SelectedItems(0).ToString()
                End If
                CPTTestTp(sSelectedCPT, sSelectedTestTp, True)
            End If
            lstTestType.Items.Remove(lstTestType.SelectedItems(0))
        End While
    End Sub

    Private Sub btnBrowseTesttype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTesttype.Click
        If lstCPTcode.SelectedItems.Count <> 1 Then
            MessageBox.Show("Please select CPT Code to choose Test Types ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            lstCPTcode.Focus()
            Exit Sub
        End If
        strLst = "testtype"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstTestType)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub

    Private Sub btnBrowsePhyID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePhyID.Click
        strLst = "physician"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstPhysicians)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub

    Private Sub btnClearPhyID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPhyID.Click
        While (lstPhysicians.SelectedItems.Count > 0)
            lstPhysicians.Items.Remove(lstPhysicians.SelectedItems(0))
        End While
    End Sub

    Private Sub BtnClearAllPhyID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllPhyID.Click
        lstPhysicians.Items.Clear()
    End Sub


    Private Sub lstCPTcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCPTcode.SelectedIndexChanged
        'If lstCPTcode.SelectedItems.Count = 1 Then
        '    sSelectedCPT = lstCPTcode.SelectedItem.ToString()

        'End If
    End Sub

    Private Sub lstCPTcode_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCPTcode.SelectedValueChanged
        If lstCPTcode.SelectedItems.Count = 1 Then
            sSelectedCPT = lstCPTcode.SelectedItem.ToString()
            lstTestType.Items.Clear()
            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For r = 0 To C1CPTTest.Rows.Count - 1
                If sSelectedCPT = C1CPTTest.GetDataDisplay(r, 0).ToString Then
                    lstTestType.Items.Add(C1CPTTest.GetDataDisplay(r, 1).ToString)
                End If
            Next
        Else
            sSelectedCPT = ""
        End If
    End Sub

    Private Sub lstTestType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTestType.SelectedValueChanged
        If lstTestType.SelectedItems.Count = 1 Then
            sSelectedTestTp = lstTestType.SelectedItem.ToString()
            '''''''''''''
            'If sSelectedCPT Then
            '    sSelectedCPT = lstCPTcode.SelectedItem.ToString()
            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For r = 0 To C1CPTTest.Rows.Count - 1
                If sSelectedCPT = C1CPTTest.GetDataDisplay(r, 0).ToString And sSelectedTestTp = C1CPTTest.GetDataDisplay(r, 1).ToString Then
                    ''cmbresult.SelectedText = ""
                    ''  cmbresult.SelectedText = (C1CPTTest.GetDataDisplay(r, 2).ToString)
                    setResultCombo(C1CPTTest.GetDataDisplay(r, 2).ToString)
                    Exit Sub
                End If
            Next
            'Else
            '    sSelectedCPT = ""
            'End If
            '''''''''''''
        Else
            sSelectedTestTp = ""
        End If
    End Sub

    Private Sub setResultCombo(Optional ByVal sVal As String = "")
        For i As Integer = 0 To cmbresult.Items.Count - 1
            If cmbresult.Items(i).ToString.Trim = sVal.Trim Then
                cmbresult.SelectedIndex = i
                'cmbresult.SelectedText
            End If
        Next         
    End Sub

    Public Function GetStressTest(ByVal mPatientID As Int64, ByVal mProcedureDate As Date, ByVal mVisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

            Dim strQuery As String = "select isnull(nElectroPhysiologyID,0) as ElectroPhysiologyID, isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0) as VisitID, isnull(nClinicID,0) as ClinicID, isnull(dtProcedureDate,0) as ProcedureDate, isnull(sCPTCode,'') as CPTCode, isnull(sProcedures,'') as Procedures,isnull(sUserProvider,'') as UserProvider from CV_ElectroPhysiology where nPatientID =  " & mPatientID & " and nVisitID = " & mVisitID & " and dtProcedureDate='" & mProcedureDate & "' "

            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
            Return Nothing
        End Try

    End Function

    Private Sub FillStressTest()
        Dim i As Integer
        Dim j As Integer
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try
            '''''''''''''''''
            lstCPTcode.Items.Clear()
            lstTestType.Items.Clear()
            lstPhysicians.Items.Clear()
            C1CPTTest.Rows.Count = 0
            Sr = 0
            '''''''''''''''''
            '' ''If blnIsNew = False Then
            _strSQL = "select sCPT,sTestType,sResult,ngroupid,sUserName,sTotExerciseTime,sEjectionFraction," _
                & " nRestingHeartRate,nRestingBPMin,nRestingBPMax,nPeakHeartRate,nPeakBPMin,nPeakBPMax,sNarrativeSummary " _
                & " from CV_StressTest where npatientid=" & mPatientID & " and dtDateOfStudy ='" & mdtDateOfStudy & "' and nvisitid=" & mVisitID & "  order by ngroupid"
            Dim oCPT As DataTable = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                If oCPT.Rows.Count > 0 Then
                    ''''''' old rec
                    DtDateOfStudy.Enabled = False
                Else
                    ''''''' new rec
                    DtDateOfStudy.Enabled = True
                End If
                For i = 0 To oCPT.Rows.Count - 1
                    If oCPT.Rows(i).Item("sCPT").ToString().Trim = "" And oCPT.Rows(i).Item("ngroupid").ToString().Trim() = "0" Then
                        'fill all details except cpt & testtypes                        
                        DtDateOfStudy.Value = mdtDateOfStudy
                        DtDateOfStudy.Enabled = False

                        '''''''Physicians
                        Dim strPhysicians() As String
                        strPhysicians = oCPT.Rows(i).Item("sUserName").ToString.Split("|")
                        For j = 0 To strPhysicians.Length - 1
                            lstPhysicians.Items.Add(strPhysicians(j).ToString().Trim())
                        Next
                        '''''''Physicians
                        ' ''cmbresult.Text = oCPT.Rows(i).Item("sResult").ToString()
                        txt_TotExTime.Text = oCPT.Rows(i).Item("sTotExerciseTime").ToString()
                        txt_EjectionFraction.Text = oCPT.Rows(i).Item("sEjectionFraction").ToString()

                        txt_RestHeartRt.Text = oCPT.Rows(i).Item("nRestingHeartRate").ToString().Trim()
                        If (txt_RestHeartRt.Text = "0") Then
                            txt_RestHeartRt.Text = ""
                        End If

                        txt_RestBPMin.Text = oCPT.Rows(i).Item("nRestingBPMin").ToString().Trim()
                        If (txt_RestBPMin.Text = "0") Then
                            txt_RestBPMin.Text = ""
                        End If

                        txt_RestBPMax.Text = oCPT.Rows(i).Item("nRestingBPMax").ToString().Trim()
                        If (txt_RestBPMax.Text = "0") Then
                            txt_RestBPMax.Text = ""
                        End If

                        txt_PeakHeartRt.Text = oCPT.Rows(i).Item("nPeakHeartRate").ToString().Trim()
                        If (txt_PeakHeartRt.Text = "0") Then
                            txt_PeakHeartRt.Text = ""
                        End If

                        txt_PeakBPMin.Text = oCPT.Rows(i).Item("nPeakBPMin").ToString().Trim()
                        If (txt_PeakBPMin.Text = "0") Then
                            txt_PeakBPMin.Text = ""
                        End If

                        txt_PeakBPMax.Text = oCPT.Rows(i).Item("nPeakBPMax").ToString().Trim()

                        If (txt_PeakBPMax.Text = "0") Then
                            txt_PeakBPMax.Text = ""
                        End If


                        txtNarrativeSummary.Text = oCPT.Rows(i).Item("sNarrativeSummary").ToString()
                        ''''''''''''
                    Else
                        'fill cpt & testtypes in grid
                        ''lstCPTcode.Items.Add(oCPT.Rows(i).Item("sCPT").ToString())
                        ''''''''''''''''
                        C1CPTTest.Rows.Add()
                        C1CPTTest.SetData(Sr, 0, oCPT.Rows(i).Item("sCPT").ToString())
                        C1CPTTest.SetData(Sr, 1, oCPT.Rows(i).Item("sTestType").ToString())
                        C1CPTTest.SetData(Sr, 2, oCPT.Rows(i).Item("sResult").ToString())
                        ''''''''''''''''
                        Sr = Sr + 1
                    End If
                Next

                'fill cpt in listbox                  
                oCPT.Dispose()
                oCPT = Nothing

                _strSQL = "select distinct sCPT from CV_StressTest where npatientid=" & mPatientID & " and dtDateOfStudy ='" & mdtDateOfStudy & "' and nvisitid=" & mVisitID & "   and sCPT<>''"
                oCPT = oDB.GetDataTable_Query(_strSQL)
                If Not oCPT Is Nothing Then
                    For i = 0 To oCPT.Rows.Count - 1
                        lstCPTcode.Items.Add(oCPT.Rows(i).Item("sCPT").ToString())
                        ''''''''''''''''
                    Next
                    oCPT.Dispose()
                    oCPT = Nothing
                End If                
            Else
                ''''''' new rec
                DtDateOfStudy.Enabled = True
            End If
            '' ''End If
        Catch ex As Exception

        Finally

            oDB.Dispose()
        End Try

    End Sub

    Private Sub lstCPTcode_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCPTcode.MouseMove
        objCath.SetListBoxToolTip(lstCPTcode, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstTestType_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstTestType.MouseMove
        objCath.SetListBoxToolTip(lstTestType, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstPhysicians_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPhysicians.MouseMove
        objCath.SetListBoxToolTip(lstPhysicians, C1SuperTooltip1, Control.MousePosition)
    End Sub


    Private Sub cmbresult_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbresult.SelectedValueChanged
        '''''''        
        If lstCPTcode.SelectedItems.Count = 1 And lstTestType.SelectedItems.Count = 1 Then
            CPTTestTp(lstCPTcode.SelectedItem.ToString(), lstTestType.SelectedItem.ToString(), , , True, cmbresult.Text)
        Else
            If cmbresult.Text.Trim() <> "" And cmbresult.Text.Trim() <> "System.Data.DataRowView" Then
                If lstCPTcode.SelectedItems.Count <> 1 And lstTestType.SelectedItems.Count <> 1 Then
                    MessageBox.Show("Please select CPT & Test Type to Set Result ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lstCPTcode.Focus()
                ElseIf lstCPTcode.SelectedItems.Count <> 1 Then
                    MessageBox.Show("Please select CPT to Set Result ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lstCPTcode.Focus()
                ElseIf lstTestType.SelectedItems.Count <> 1 Then
                    MessageBox.Show("Please select Test Type to Set Result ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    lstTestType.Focus()
                End If
                cmbresult.SelectedIndex = -1
            End If
        End If
        '''''''
    End Sub

    Private Sub txt_TotExTime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_TotExTime.KeyDown

      

    End Sub

    Private Sub txt_TotExTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_TotExTime.KeyPress
        'Dim oRegx As Regex = New Regex("")
        'If oRegx.IsMatch(e.KeyChar.ToString(), "[^0-9.-]") Then
        '    e.Handled = True

        'Else
        '    e.Handled = False
        'End If
    End Sub

    Private Sub txt_TotExTime_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_TotExTime.Validating
        Try
            If Not IsPositiveNumber(txt_TotExTime.Text) AndAlso Not txt_TotExTime.Text = Nothing Then
                MessageBox.Show("Total Exercise Time is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_TotExTime.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub txt_EjectionFraction_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_EjectionFraction.Validating
        Try
            If Not IsPositiveNumber(txt_EjectionFraction.Text) AndAlso Not txt_EjectionFraction.Text = Nothing Then
                MessageBox.Show("Ejection Fraction is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_EjectionFraction.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_RestHeartRt_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_RestHeartRt.Validating
        Try
            If Not IsPositiveNumber(txt_RestHeartRt.Text) AndAlso Not txt_RestHeartRt.Text = Nothing Then
                MessageBox.Show("Resting Heart Rate is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_RestHeartRt.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_RestBPMax_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_RestBPMax.Validating
        Try
            If Not IsPositiveNumber(txt_RestBPMax.Text) AndAlso Not txt_RestBPMax.Text = Nothing Then
                MessageBox.Show("Resting BP Max is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_RestBPMax.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_RestBPMin_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_RestBPMin.Validating
        Try
            If Not IsPositiveNumber(txt_RestBPMin.Text) AndAlso Not txt_RestBPMin.Text = Nothing Then
                MessageBox.Show("Resting BP Min is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_RestBPMin.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_PeakHeartRt_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_PeakHeartRt.Validating
        Try
            If Not IsPositiveNumber(txt_PeakHeartRt.Text) AndAlso Not txt_PeakHeartRt.Text = Nothing Then
                MessageBox.Show("Peak Heart Rate is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_PeakHeartRt.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_PeakBPMax_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_PeakBPMax.Validating
        Try
            If Not IsPositiveNumber(txt_PeakBPMax.Text) AndAlso Not txt_PeakBPMax.Text = Nothing Then
                MessageBox.Show("Peak BP Max is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_PeakBPMax.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txt_PeakBPMin_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_PeakBPMin.Validating
        Try
            If Not IsPositiveNumber(txt_PeakBPMin.Text) AndAlso Not txt_PeakBPMin.Text = Nothing Then
                MessageBox.Show("Peak BP Min is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_PeakBPMin.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class