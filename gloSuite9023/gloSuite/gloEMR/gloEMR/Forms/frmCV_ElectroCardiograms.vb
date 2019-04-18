
Imports gloUserControlLibrary
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Text



Public Class frmCV_ElectroCardiograms

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
    Public MainCathID As Long = 0
    Dim Sr As Int32 = 0
    Dim r As Integer

    Dim sSelectedCPT As String = ""
    Dim sSelectedTestTp As String = ""

    Public blnIsNew As Boolean
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing
    Dim objCath As New Cls_CardioVasculars



    Private Sub tsECG_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsECG.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveECG()
                    Me.Close()
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveECG()

        Try
            Dim Arrlist As New ArrayList
            Dim objECG As New clsCVElectroCardioGrams
            Dim i As Integer ', j As Integer
            ''''''''''''''
            '''''''''''''
            'If mPatientID = 0 Then
            '    mPatientID = gnPatientID
            'End If


            If mClinicID = 0 Then
                mClinicID = 1
            End If

            If mdtProcedureDate = Nothing Then
                mdtProcedureDate = DtProcDate.Value.Date
            End If

            If mVisitID = 0 Then
                mVisitID = mVisitID
                '    Try
                '        mVisitID = GenerateVisitID(DtProcDate.Value.Date, mPatientID)
                '    Catch ex As Exception
                '        mVisitID = 0
                '    End Try
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
            'MainCathID = objCathDBLayer.SaveCatheterization(mCathID, mClinicID, mPatientID, mExamId, mVisitID, DtProcDate.Value.Date, 0, "", "", strPhysicians.ToString(), strInterventions.ToString(), txtPR.Text, txtQT.Text, txtQTc.Text, txtORS_Duration.Text, txtP_Axis.Text, "", txtT_Axis.Text, "", "", txtQRS_Axis.Text, "", "", "", "", "", "", "", "", "", True)

            MainCathID = objECG.SaveElectroCardioGrams(mCathID, mPatientID, mExamId, mVisitID, mClinicID, DateTime.Now.Date, dtReviewDate.Value.Date, DtProcDate.Value.ToString(), 0, "", "", cmbCardioEcgType.SelectedItem, txtPR.Text, txtQT.Text, txtQTc.Text, txtORS_Duration.Text, txtP_Axis.Text, txtQRS_Axis.Text, txtT_Axis.Text, txtECGEnterpretaion.Text, strPhysicians.ToString(), strInterventions.ToString(), True)

            ''''''''''''''
            ''''''''''''''' all values for master entry

            ''''''''''''''' all values for detail entry   

            C1CPTTest.Sort(C1.Win.C1FlexGrid.SortFlags.Ascending, 0)
            For i = 0 To C1CPTTest.Rows.Count - 1
                If C1CPTTest.GetDataDisplay(i, 0).ToString().Trim <> "" Then
                    'mCathID = objCathDBLayer.SaveCatheterization((MainCathID + (i + 1)), mClinicID, mPatientID, mExamId, mVisitID, DtProcDate.Value.Date, MainCathID, C1CPTTest.GetDataDisplay(i, 0).ToString(), C1CPTTest.GetDataDisplay(i, 1).ToString())
                    mCathID = objECG.SaveElectroCardioGrams((MainCathID + (i + 1)), mPatientID, mExamId, mVisitID, mClinicID, DateTime.Now.Date, DateTime.Now.Date, DtProcDate.Value.ToString(), MainCathID, C1CPTTest.GetDataDisplay(i, 0).ToString(), C1CPTTest.GetDataDisplay(i, 1).ToString())
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
            MessageBox.Show(ex.Message, "ElectronicCardioGrams", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        ElseIf strLst = "reviewPhysician" Then
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
            ElseIf strLst = "reviewPhysician" Then
                dt = FillPhysician()
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
            MessageBox.Show(ex.Message, "ElectroCardioGrams", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ElectroCardioGrams", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                lstTestType.Items.Clear()
                C1CPTTest.Clear()
            ElseIf strLst = "testtype" Then
                lstTestType.Items.Clear()
            ElseIf strLst = "reviewPhysician" Then
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

                    ElseIf strLst = "reviewPhysician" Then
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

    Private Sub frmCV_ElectroCardiograms_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            objCath.Dispose()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Close, "Close Catheterization", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Close, "Close ElectroCardioGram", 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
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

    Private Sub frmCV_ElectroCardiograms_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '''''''''Patient Strip
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
            'Check if form is loaded in edit mode
            DtProcDate.Enabled = False
            '' Fill()
        Else

            DtProcDate.Value = Now
            'if form is loaded in new mode then check if visit already exists
            If mVisitID = 0 Then
                'if visit does not exists then load the form in new mode

            Else
                'if visit exists for the given date then pull the data for that date                
            End If
            DtProcDate.Enabled = True
        End If
        FillECGType()
        FillECG()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub BtnClearAllTesttype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearAllTesttype.Click
        '''''''''''''''''to clear test types from selected cpt - lstTestType.Items.Clear()       
        Dim i As Integer
        If sSelectedCPT <> "" Then
            For i = 0 To lstTestType.Items.Count - 1
                CPTTestTp(lstCPTcode.SelectedItems(0).ToString(), lstTestType.Items(0).ToString(), True)
                If lstTestType.Items.Count > 0 Then
                    lstTestType.Items.Remove(lstTestType.Items(0))
                End If
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

    Private Sub btnBrowseIntervn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub btnBrowsePhyID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePhyID.Click, btnBrowseIntervn.Click
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

    Private Sub FillECG()
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
                '_strSQL = "select * from CV_ElectroCardioGrams where npatientid=" & mPatientID & " and dtGivenDate ='" & mdtProcedureDate & "' and nvisitid=" & mVisitID & "  order by ngroupid"
                _strSQL = "select sCPTCode,sTestType, ngroupid,sOrderInPhysician,sReviewInPhysician,sPR " _
                    & " sPR,sQT,sQTc,sORSDuration,sPAxis,sTAxis,sQRSAxis,sECGInterpretation,dtReviewDate,sECGPerform,dtGivenDate " _
                    & " from CV_ElectroCardioGrams where npatientid=" & mPatientID & " and CONVERT(date,dtGivenDate,111) ='" & mdtProcedureDate & "' and nvisitid=" & mVisitID & "  AND (sOrderId='' OR sOrderId=null) AND (sTestId='' OR sTestId=null) order by ngroupid"
                oCPT = oDB.GetDataTable_Query(_strSQL)
                If Not oCPT Is Nothing Then
                    For i = 0 To oCPT.Rows.Count - 1
                        If oCPT.Rows(i).Item("sCPTCode").ToString().Trim = "" AndAlso oCPT.Rows(i).Item("ngroupid").ToString().Trim() = "0" Then
                            'fill all details except cpt & testtypes                        
                            DtProcDate.Value = mdtProcedureDate
                            DtProcDate.Enabled = False

                            '''''''Physicians
                            Dim strPhysicians() As String
                            strPhysicians = oCPT.Rows(i).Item("sOrderInPhysician").ToString.Split("|")
                            For j = 0 To strPhysicians.Length - 1
                                lstPhysicians.Items.Add(strPhysicians(j).ToString().Trim())
                            Next
                            '''''''Physicians

                            '''''''Intervention
                            Dim strIntervention() As String
                            strIntervention = oCPT.Rows(i).Item("sReviewInPhysician").ToString.Split("|")
                            For j = 0 To strIntervention.Length - 1
                                lstIntervention.Items.Add(strIntervention(j).ToString().Trim())
                            Next
                            '''''''Intervention

                            'objCath.sInterventionType = strInterventions.ToString()
                            txtPR.Text = oCPT.Rows(i).Item("sPR").ToString()
                            txtQT.Text = oCPT.Rows(i).Item("sQT").ToString()
                            txtQTc.Text = oCPT.Rows(i).Item("sQTc").ToString()
                            txtORS_Duration.Text = oCPT.Rows(i).Item("sORSDuration").ToString()
                            txtP_Axis.Text = oCPT.Rows(i).Item("sPAxis").ToString()
                            txtT_Axis.Text = oCPT.Rows(i).Item("sTAxis").ToString()
                            txtQRS_Axis.Text = oCPT.Rows(i).Item("sQRSAxis").ToString()
                            txtECGEnterpretaion.Text = oCPT.Rows(i).Item("sECGInterpretation").ToString()
                            dtReviewDate.Value = oCPT.Rows(i).Item("dtReviewDate").ToString()
                            cmbCardioEcgType.SelectedItem = oCPT.Rows(i).Item("sECGPerform").ToString()
                            DtProcDate.Value = Convert.ToDateTime(oCPT.Rows(i).Item("dtGivenDate").ToString())
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
                    '_strSQL = "select distinct sCPTCode from CV_ElectroCardioGrams where npatientid=" & mPatientID & " and dtGivenDate ='" & mdtProcedureDate & "' and nvisitid=" & mVisitID & "   and sCPTCode<>''"
                    _strSQL = "select distinct sCPTCode from CV_ElectroCardioGrams where npatientid=" & mPatientID & " and CONVERT(date,dtGivenDate,111) ='" & mdtProcedureDate & "' and nvisitid=" & mVisitID & "   and sCPTCode<>''"
                    oCPT = oDB.GetDataTable_Query(_strSQL)
                    If Not oCPT Is Nothing Then
                        For i = 0 To oCPT.Rows.Count - 1
                            lstCPTcode.Items.Add(oCPT.Rows(i).Item("sCPTCode").ToString())
                            If i = 0 Then
                                lstCPTcode.SelectedItem = oCPT.Rows(0).Item("sCPTCode").ToString()
                            End If
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


    Private Sub btnBrowseIntervn_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseReviewPhysician.Click
        strLst = "reviewPhysician"
        LoadUserGrid()
        ''''''''
        SetCheckValues(lstIntervention) 'here lstIntervention means reviewPhysician list.
        ''''''''
        pnlCustomTask.BringToFront()

        ''''''''
        LockControls(Me, pnlCustomTask)
        ''''''''
    End Sub

    Private Sub btn_CardioEcgType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CardioEcgType.Click
        Dim frm As New CategoryMaster
        frm.Text = "Add Category"
        frm.cmbCategoryType.SelectedItem = "Cardio ECG Type"
        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
        If frm.txtCategoryDesc.Text <> "" Then
            FillECGType()
            cmbCardioEcgType.SelectedItem = frm.txtCategoryDesc.Text
        End If
        frm.Dispose()
        frm = Nothing
    End Sub

    Public Sub FillECGType()
        Dim oDB As New DataBaseLayer
        Dim _strSQL As String = ""
        Dim i As Integer

        cmbCardioEcgType.Items.Clear()
        Try

            _strSQL = "Select nCategoryID,sDescription,sCategoryType,nClinicID,bIsBlocked,sCode From Category_MST Where sCategoryType = 'Cardio ECG Type'"
            oCPT = oDB.GetDataTable_Query(_strSQL)
            If Not oCPT Is Nothing Then
                For i = 0 To oCPT.Rows.Count - 1
                    cmbCardioEcgType.Items.Add(oCPT.Rows(i).Item("sDescription").ToString())
                Next
            End If

        Catch ex As Exception

        Finally
            oDB = Nothing
        End Try
    End Sub
End Class