
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Text
Imports System.Text.RegularExpressions


Public Class frmCV_Catheterization

    Private WithEvents dgCustomGrid As CustomTask
    Private Col_Check As Integer = 2
    Private Col_Name As Integer = 0
    Private Col_Dosage As Integer = 1
    Private Col_Count As Integer = 3
    Dim _TempRx As String
    Dim _Temprow As Int32

    Dim oDiag As New DataTable
    Dim oCPT As New DataTable
    Dim strLst As String = ""
    Dim dt As DataTable
    Dim CptList As New ArrayList

    Private mPatientID As Int64
    Private mVisitID As Int64
    Private mExamId As Int64
    Private mClinicID As Int64
    Private mdtProcedureDate As DateTime
    Private mCathID As Long = 0
    Private MainCathID As Long = 0
    Dim Sr As Int32 = 0
    Dim r As Integer

    Dim sSelectedCPT As String = ""
    Dim sSelectedTestTp As String = ""

    Public blnIsNew As Boolean
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Dim objCath As New Cls_CardioVasculars



    Private Sub tsCatheterization_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsCatheterization.ItemClicked
        Dim sErrorMessage As String = ""
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    sErrorMessage = ValidateForm()
                    If sErrorMessage = "" Then
                        SaveCatheterization()
                        Me.Close()
                    Else
                        MessageBox.Show(sErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveCatheterization()

        Try
            Dim Arrlist As New ArrayList
            Dim objCathDBLayer As New ClsCVCatheterizationDbLayer
            Dim i As Integer ', j As Integer
            ''''''''''''''
            '''''''''''''
            If mPatientID = 0 Then
                mPatientID = mPatientID
            End If


            If mClinicID = 0 Then
                mClinicID = 1
            End If

            If mdtProcedureDate = Nothing Then
                mdtProcedureDate = DtProcDate.Value.Date
            End If

            If mVisitID = 0 Then
                Try
                    mVisitID = GenerateVisitID(DtProcDate.Value.Date, mPatientID)
                Catch ex As Exception
                    mVisitID = 0
                End Try
            End If
            ''''''''''''

            '''''''''''
            'If mdtProcedureDate <> DtProcDate.Value.Date Then
            '    Try
            '        mVisitID = GenerateVisitID(DtProcDate.Value.Date, mPatientID)
            '    Catch ex As Exception
            '        mVisitID = 0
            '    End Try
            'End If
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

            '''''''Interventions
            Dim strInterventions As New StringBuilder
            For i = 0 To lstIntervention.Items.Count - 1
                If i > 0 Then
                    strInterventions.Append(" | ")
                End If
                strInterventions.Append(lstIntervention.Items(i).ToString())
            Next
            '''''''Interventions

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
            '' mCathID = objCathDBLayer.SaveCatheterization(Arrlist, True)
            MainCathID = objCathDBLayer.SaveCatheterization(mCathID, mClinicID, mPatientID, mExamId, mVisitID, DtProcDate.Value.Date, 0, "", "", strPhysicians.ToString(), strInterventions.ToString(), txtP_RA.Text, txtP_LA.Text, txtP_RightPulmonary.Text, txtP_LeftPulmonary.Text, txtP_RV.Text, txtP_LV.Text, txtP_Peak.Text, txtP_Diastolic.Text, txtP_Mean.Text, txtP_PA.Text, txtS_IVC.Text, txtS_SVC.Text, txtS_RA.Text, txtS_RV.Text, txtS_PA.Text, txtLV_EjectionFraction.Text, txtLV_DiastolicVolume.Text, txtLV_SystolicVolume.Text, txtNarrativeSummary.Text, True)
            ''''''''''''''
            ''''''''''''''' all values for master entry

            ''''''''''''''' all values for detail entry   

            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For i = 0 To C1CPTTest.Rows.Count - 1
                If C1CPTTest.GetDataDisplay(i, 0).ToString().Trim <> "" Then
                    mCathID = objCathDBLayer.SaveCatheterization((MainCathID + (i + 1)), mClinicID, mPatientID, mExamId, mVisitID, DtProcDate.Value.Date, MainCathID, C1CPTTest.GetDataDisplay(i, 0).ToString(), C1CPTTest.GetDataDisplay(i, 1).ToString())
                End If
            Next

            ''''''''''''''' all values for detail entry

            ''''''''''''''            

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try


    End Sub


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
                pnlCustomTask.Width = 600
                dgCustomGrid.Width = pnlCustomTask.Width

                pnlCustomTask.Height = 250
                'pnlcustomTask.Width = dgCustomGrid.Width
                dgCustomGrid.Height = pnlCustomTask.Height
                dgCustomGrid.txtsearch.Width = 300


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
            y = 220
            x = 200
            'dgCustomGrid.lblCaption.Text = " CPT List "
        ElseIf strLst = "testtype" Then
            y = 220
            x = 520
            'dgCustomGrid.lblCaption.Text = " Test Type List "
        ElseIf strLst = "intervention" Then
            y = 220
            x = 550
            'dgCustomGrid.lblCaption.Text = " Intervention Type List "
        ElseIf strLst = "physician" Then
            y = 220
            x = 560
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

            If strLst = "cpt" Then
                dt = FillCPT()
            ElseIf strLst = "testtype" Then
                dt = FillTestType()
            ElseIf strLst = "intervention" Then
                dt = FillInterVention()
            ElseIf strLst = "physician" Then
                dt = FillPhysician()
            End If

            CustomDrugsGridStyle()
            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)

            If Not IsNothing(dt) Then
                ''dt.Columns("sICD9Display").Caption = "Diagnosis Name"
                dgCustomGrid.datasource(dt.DefaultView)
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
            dgCustomGrid.C1Task.ExtendLastCol = True

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
            ''''''''''''''''''
            LockControls(Me, pnlCustomTask, True)
            ''''''''''''''''''

            '''''''''''''''''''''
            If strLst = "cpt" Then
                lstCPTcode.Items.Clear()
                C1CPTTest.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            ElseIf strLst = "testtype" Then
                lstTestType.Items.Clear()
            ElseIf strLst = "intervention" Then
                lstIntervention.Items.Clear()
            ElseIf strLst = "physician" Then
                lstPhysicians.Items.Clear()
            End If
            '''''''''''''''''''''

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
                        '''''''
                        If lstCPTcode.SelectedItems.Count = 1 Then
                            CPTTestTp(lstCPTcode.SelectedItem.ToString())
                        End If
                        '''''''

                    ElseIf strLst = "intervention" Then
                        lstIntervention.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    ElseIf strLst = "physician" Then
                        lstPhysicians.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)
                    End If
                End If
            Next

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
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGrid.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CPTTestTp(ByVal sCPT As String, Optional ByVal sTestTp As String = "", Optional ByVal RemoveItm As Boolean = False, Optional ByVal AddCpt As Boolean = False)
        Dim j As Integer

       
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
                            If r >= 0 AndAlso r < C1CPTTest.Rows.Count Then
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

            _strSQL = "select Distinct (isNull(rtrim(sCPTCode),'') + ' - ' + isNull(ltrim(sDescription),'')) as [CPT] from CPT_MST Where  sCPTCode <>'' AND sDescription<>''"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function

    Public Function FillTestType() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sDescription as [Test Type] from Category_MST where sCategoryType='Cardio Test Type' order by sDescription"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function


    Public Function FillInterVention() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sDescription as [InterVention Type] from Category_MST where sCategoryType='Intervention' order by sDescription"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function

    Public Function FillPhysician() As DataTable
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""

        Try

            _strSQL = "Select sLoginName + ' - ' + isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as [Login Name] from User_MST order by sLoginName "
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                Return oCPT
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB = Nothing
        End Try
    End Function

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


    Public Sub New(ByVal PatientId As Int64, ByVal dtProcedureDate As Date, Optional ByVal VisitID As Int64 = 0, Optional ByVal ExamID As Int64 = 0, Optional ByVal ClinicID As Int64 = 1)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        mPatientID = PatientId
        mVisitID = VisitID
        mdtProcedureDate = dtProcedureDate       
        DtProcDate.Value = mdtProcedureDate        
        mExamId = ExamID
        mClinicID = ClinicID        
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmCV_Catheterization_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            objCath.Dispose()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Close, "Close Catheterization", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Close, "Close Catheterization", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_Catheterization_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        gloUC_PatientStrip1.BringToFront()
        pnlToolStrip.SendToBack()
        pnlMain.BringToFront()
        gloUC_PatientStrip1.ShowDetail(mPatientID, gloUC_PatientStrip.enumFormName.None)
        Me.Controls.Add(gloUC_PatientStrip1)
        


        '''''''''Patient Strip
        If blnIsNew = False Then
            DtProcDate.Enabled = False
        Else
            'if form is loaded in new mode then check if visit already exists
            If mVisitID = 0 Then
                'if visit does not exists then load the form in new mode                
            Else
                'if visit exists for the given date then pull the data for that date                
            End If
            DtProcDate.Enabled = True
        End If
        FillCatheterization()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

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

    Private Sub btnBrowseIntervn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseIntervn.Click
        strLst = "intervention"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstIntervention)
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub

    Private Sub btnClearIntervn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearIntervn.Click
        While (lstIntervention.SelectedItems.Count > 0)           
            lstIntervention.Items.Remove(lstIntervention.SelectedItems(0))
        End While
    End Sub

    Private Sub BtnClearAllIntervn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllIntervn.Click
        lstIntervention.Items.Clear()
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
        Else
            sSelectedTestTp = ""
        End If
    End Sub

    Private Sub FillCatheterization()
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
            If blnIsNew = False Then
                _strSQL = "select nGroupID, sCPTCode, sTestType, sPhysicianName, sInterventionType, sRAPressure, sLAPressure, sRPulmonary, sLPulmonary,  sRV, sLV, sPeak, " _
                          & " sDiastolic, sMean, sPAPressure,  sIVC, sSVC, sRASaturations, sRVSaturations,  sPASaturations,  sLVEjectionFraction, sLVDiastolicVol, " _
                          & " sLVSystolicVol, sNarrativeSummary " _
                          & " from CV_Catheterization " _
                          & " where npatientid=" & mPatientID & " " _
                          & " and dtproceduredate ='" & mdtProcedureDate & "' " _
                          & " and nvisitid=" & mVisitID & "  order by ngroupid"
                oCPT = oDB.GetDataTable_Query(_strSQL)
                If Not oCPT Is Nothing Then
                    For i = 0 To oCPT.Rows.Count - 1
                        If oCPT.Rows(i).Item("sCPTCode").ToString().Trim = "" AndAlso oCPT.Rows(i).Item("ngroupid").ToString().Trim() = "0" Then
                            'fill all details except cpt & testtypes                        
                            DtProcDate.Value = mdtProcedureDate
                            DtProcDate.Enabled = False

                            '''''''Physicians
                            Dim strPhysicians() As String
                            strPhysicians = oCPT.Rows(i).Item("sPhysicianName").ToString.Split("|")
                            For j = 0 To strPhysicians.Length - 1
                                lstPhysicians.Items.Add(strPhysicians(j).ToString().Trim())
                            Next
                            '''''''Physicians

                            '''''''Intervention
                            Dim strIntervention() As String
                            strIntervention = oCPT.Rows(i).Item("sInterventionType").ToString.Split("|")
                            For j = 0 To strIntervention.Length - 1
                                lstIntervention.Items.Add(strIntervention(j).ToString().Trim())
                            Next
                            '''''''Intervention

                            'objCath.sInterventionType = strInterventions.ToString()
                            txtP_RA.Text = oCPT.Rows(i).Item("sRaPressure").ToString()
                            txtP_LA.Text = oCPT.Rows(i).Item("sLaPressure").ToString()
                            txtP_RightPulmonary.Text = oCPT.Rows(i).Item("sRPulmonary").ToString()
                            txtP_LeftPulmonary.Text = oCPT.Rows(i).Item("sLPulmonary").ToString()
                            txtP_RV.Text = oCPT.Rows(i).Item("sRV").ToString()
                            txtP_LV.Text = oCPT.Rows(i).Item("sLV").ToString()
                            txtP_Peak.Text = oCPT.Rows(i).Item("sPeak").ToString()
                            txtP_Diastolic.Text = oCPT.Rows(i).Item("sDiastolic").ToString()
                            txtP_Mean.Text = oCPT.Rows(i).Item("sMean").ToString()
                            txtP_PA.Text = oCPT.Rows(i).Item("sPaPressure").ToString()
                            txtS_IVC.Text = oCPT.Rows(i).Item("sIVc").ToString()
                            txtS_SVC.Text = oCPT.Rows(i).Item("sSvc").ToString()
                            txtS_RA.Text = oCPT.Rows(i).Item("sRASaturations").ToString()
                            txtS_RV.Text = oCPT.Rows(i).Item("sRVSAturations").ToString()
                            txtS_PA.Text = oCPT.Rows(i).Item("sPASaturations").ToString()

                            txtLV_EjectionFraction.Text = oCPT.Rows(i).Item("sLVEjectionFraction").ToString()
                            txtLV_DiastolicVolume.Text = oCPT.Rows(i).Item("sLVDiastolicVol").ToString()
                            txtLV_SystolicVolume.Text = oCPT.Rows(i).Item("sLVSystolicVol").ToString()
                            txtNarrativeSummary.Text = oCPT.Rows(i).Item("sNarrativeSummary").ToString()
                            ''''''''''''
                        Else
                            'fill cpt & testtypes in grid
                            ''lstCPTcode.Items.Add(oCPT.Rows(i).Item("sCPTCode").ToString())
                            ''''''''''''''''
                            C1CPTTest.Rows.Add()
                            C1CPTTest.SetData(Sr, 0, oCPT.Rows(i).Item("sCPTCode").ToString())
                            C1CPTTest.SetData(Sr, 1, oCPT.Rows(i).Item("sTestType").ToString())
                            ''''''''''''''''
                            Sr = Sr + 1
                        End If
                    Next

                    'fill cpt in listbox                  
                    oCPT = New DataTable
                    _strSQL = "select distinct sCPTCode from CV_Catheterization where npatientid=" & mPatientID & " and dtproceduredate ='" & mdtProcedureDate & "' and nvisitid=" & mVisitID & "   and sCPTCode<>''"
                    oCPT = oDB.GetDataTable_Query(_strSQL)
                    If Not oCPT Is Nothing Then
                        For i = 0 To oCPT.Rows.Count - 1
                            lstCPTcode.Items.Add(oCPT.Rows(i).Item("sCPTCode").ToString())
                            ''''''''''''''''
                        Next
                    End If
                End If
            End If
        Catch ex As Exception

        Finally
            oCPT.Dispose()
            oDB.Dispose()
        End Try

    End Sub

    Private Sub lstCPTcode_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCPTcode.MouseMove
        objCath.SetListBoxToolTip(lstCPTcode, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstTestType_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstTestType.MouseMove
        objCath.SetListBoxToolTip(lstTestType, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstIntervention_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstIntervention.MouseMove
        objCath.SetListBoxToolTip(lstIntervention, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub lstPhysicians_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPhysicians.MouseMove
        objCath.SetListBoxToolTip(lstPhysicians, C1SuperTooltip1, Control.MousePosition)
    End Sub

    Private Sub txtS_IVC_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtS_IVC.Validating
        Try
            If Not IsPositiveNumber(txtS_IVC.Text) AndAlso Not txtS_IVC.Text = Nothing Then
                MessageBox.Show("Saturation IVC is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtS_IVC.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtS_SVC_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtS_SVC.Validating
        Try
            If Not IsPositiveNumber(txtS_SVC.Text) AndAlso Not txtS_SVC.Text = Nothing Then
                MessageBox.Show("Saturation SVC is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtS_SVC.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtS_RA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtS_RA.Validating
        Try
            If Not IsPositiveNumber(txtS_RA.Text) AndAlso Not txtS_RA.Text = Nothing Then
                MessageBox.Show("Saturation RA is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtS_RA.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtS_RV_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtS_RV.Validating
        Try
            If Not IsPositiveNumber(txtS_RV.Text) AndAlso Not txtS_RV.Text = Nothing Then
                MessageBox.Show("Saturation RV is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtS_RV.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtS_PA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtS_PA.Validating
        Try
            If Not IsPositiveNumber(txtS_PA.Text) AndAlso Not txtS_PA.Text = Nothing Then
                MessageBox.Show("Saturation PA is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtS_PA.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtLV_EjectionFraction_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtLV_EjectionFraction.Validating
        Try
            If Not IsPositiveNumber(txtLV_EjectionFraction.Text) AndAlso Not txtLV_EjectionFraction.Text = Nothing Then
                MessageBox.Show("Left Ventricular Ejection Fraction is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtLV_EjectionFraction.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtLV_DiastolicVolume_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtLV_DiastolicVolume.Validating
        Try
            If Not IsPositiveNumber(txtLV_DiastolicVolume.Text) AndAlso Not txtLV_DiastolicVolume.Text = Nothing Then
                MessageBox.Show("Left Ventricular Diastolic Volume is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtLV_DiastolicVolume.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtLV_SystolicVolume_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtLV_SystolicVolume.Validating
        Try
            If Not IsPositiveNumber(txtLV_SystolicVolume.Text) AndAlso Not txtLV_SystolicVolume.Text = Nothing Then
                MessageBox.Show("Left Ventricular Systolic Volume is not in correct format. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtLV_SystolicVolume.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ValidateForm() As String
        Dim sReturnMessage As String = ""
        Try
            If Not IsPositiveNumber(txtS_IVC.Text) AndAlso Not txtS_IVC.Text = Nothing Then
                sReturnMessage = "Saturation IVC is not in correct format. "
                txtS_IVC.Focus()
                Exit Try
            End If


            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtS_SVC.Text) AndAlso Not txtS_SVC.Text = Nothing Then
                    sReturnMessage = "Saturation SVC is not in correct format. "
                    txtS_SVC.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtS_RA.Text) AndAlso Not txtS_RA.Text = Nothing Then
                    sReturnMessage = "Saturation RA is not in correct format. "
                    txtS_RA.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtS_RV.Text) AndAlso Not txtS_RV.Text = Nothing Then
                    sReturnMessage = "Saturation RV is not in correct format. "
                    txtS_RV.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtS_PA.Text) AndAlso Not txtS_PA.Text = Nothing Then
                    sReturnMessage = "Saturation PA is not in correct format. "
                    txtS_PA.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtLV_EjectionFraction.Text) AndAlso Not txtLV_EjectionFraction.Text = Nothing Then
                    sReturnMessage = "Left Ventricular Ejection Fraction is not in correct format. "
                    txtLV_EjectionFraction.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtLV_DiastolicVolume.Text) AndAlso Not txtLV_DiastolicVolume.Text = Nothing Then
                    sReturnMessage = "Left Ventricular Diastolic Volume is not in correct format. "
                    txtLV_DiastolicVolume.Focus()
                    Exit Try
                End If
            End If

            If sReturnMessage = "" Then
                If Not IsPositiveNumber(txtLV_SystolicVolume.Text) AndAlso Not txtLV_SystolicVolume.Text = Nothing Then
                    sReturnMessage = "Left Ventricular Systolic Volume is not in correct format. "
                    txtLV_SystolicVolume.Focus()
                    Exit Try
                End If
            End If

           
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return sReturnMessage

    End Function
End Class