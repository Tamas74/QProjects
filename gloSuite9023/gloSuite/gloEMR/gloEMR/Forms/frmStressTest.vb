Imports gloUserControlLibrary
'Imports gloListControl
Imports System.Data.SqlClient
'Imports gloEMR.gloStream.gloDataBase
Imports C1.Win.C1FlexGrid
''Imports glodbInterface
'Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class frmStressTest
    Dim ofrmUser As frmUserList

    Dim _PatientID As Long = 0
    Dim _StressID As Long = 0
    Dim _VisitID As Long = 0
    Dim _ExamID As Long = 0
    Dim _ClinicID As Long = 0


    Dim COL_StressID As Integer = 0
    Dim COL_PatientID As Integer = 1
    Dim COL_ExamID As Integer = 2
    Dim COL_VisitID As Integer = 3
    Dim COL_ClinicID As Integer = 4
    Dim COL_DateofStudy As Integer = 5
    Dim COL_TestType = 6
    Dim COL_Procedure As Integer = 7

    Dim COL_Result = 8
    Dim COL_UserName As Integer = 9
    Dim COL_UserNameButton As Integer = 10
    Dim COLUMN_COUNT As Integer = 11
    Dim struser As String
    Private WithEvents pnlInternalControl As Panel
    Dim ogloGridListControl As New gloBilling.gloGridListControl

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal PatientID As Long, ByVal ExamID As Long, ByVal VisitID As Long)
        MyBase.New()

        '' ProblemID is Zero when Problem List is Open from Patient Exam

        _PatientID = PatientID
        _ExamID = ExamID
        _VisitID = VisitID
        _ClinicID = 1

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Public Function PopulateStressTable(ByVal _PatientID As Long, ByVal _ExamID As Long, ByVal _VisitID As Long, ByVal _ClinicID As Long) As DataTable

        'Declare a variable for connection string
        Dim dt As New DataTable
        Dim sqladpt As SqlDataAdapter
        Dim conn As SqlConnection = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")

        'Declare a variable for command
        'Dim cmd As SqlCommand = New SqlCommand("select * from CV_StressTest where nStressId=" + _StressID + "and nPatientID=" + _PatientID + "and nExamID=" + _ExamID + "nVisitID=" + _VisitID + "and nClinicID=" + _ClinicID, conn)
        Dim strQuery As String = "select isNULL(nStressID,0) as StressID,isnull(nPatientID,0) as PatientID,isnull(nExamID,0) as ExamID,isnull(nVisitID,0) as VisitID,isnull(nClinicID,0) as ClinicID,isnull(dtDateofStudy,0) as DateofStudy,isnull(sTestType,'') as TestType,isnull(sCPT,'') as Procedures,isnull(sUserName,'') as UserName ,isnull(sResult,'') as Result from CV_StressTest where nPatientID=" & _PatientID & " and nVisitID=" & _VisitID & " and nClinicID=" & _ClinicID
        Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
        sqladpt = New SqlDataAdapter
        sqladpt.SelectCommand = cmd

        'Fill data table_
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
        'Return data table
        Return dt

    End Function


    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub frmStressTest_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If (IsNothing(gloUC_PatientStrip1) = False) Then
                gloUC_PatientStrip1.Dispose()
                gloUC_PatientStrip1 = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Declare a variable for controls
            gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

            gloUC_PatientStrip1.Dock = DockStyle.Fill
            gloUC_PatientStrip1.ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.None)
            pnlPatientStrip.Controls.Add(gloUC_PatientStrip1)
            Dim dt As DataTable = PopulateStressTable(_PatientID, _ExamID, _VisitID, _ClinicID)
            SetGridStyle(dt)
            pnlInternalControl = New Panel
            pnlInternalControl.Width = 337
            pnlInternalControl.Height = 221
            pnlInternalControl.Visible = False
            pnlInternalControl.SendToBack()
            C1StressList.Controls.Add(pnlInternalControl)

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    
    Private Sub SetGridStyle(ByVal dt As DataTable)
        'Declare a variable
        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        ' Dim struser As String
        With C1StressList
            Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (.Width - 20) / 11
            .Cols.Count = COLUMN_COUNT
            .Rows.Count = 1
            .AllowEditing = True
            .AllowAddNew = True

            .Styles.ClearUnused()

            .Cols(COL_StressID).Width = .Width * 0
            .Cols(COL_StressID).AllowEditing = False
            .SetData(0, COL_StressID, "StressID")
            .Cols(COL_StressID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False
            .SetData(0, COL_VisitID, "ExamID")
            .Cols(COL_ExamID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .SetData(0, COL_VisitID, "VisitID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_ClinicID).Width = .Width * 0
            .Cols(COL_ClinicID).AllowEditing = False
            .SetData(0, COL_VisitID, "ClinicID")
            .Cols(COL_ClinicID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_DateofStudy).Width = _TotalWidth * 1
            .SetData(0, COL_DateofStudy, "Date")
            .Cols(COL_DateofStudy).DataType = GetType(Date)
            .Cols(COL_DateofStudy).AllowEditing = True

            .Cols(COL_TestType).Width = _TotalWidth * 2.5
            .SetData(0, COL_TestType, "TestType")
            .Cols(COL_TestType).AllowEditing = True

            .Cols(COL_Procedure).Width = _TotalWidth * 2.5
            .SetData(0, COL_Procedure, "Procedures")
            .Cols(COL_Procedure).AllowEditing = True

            'Dim cstyle As CellStyle
            'cStyle = .Styles.Add("BubbleValues")
            'cstyle.ComboList = "..."
            '.Cols(COL_Procedure).Style = cStyle

            .Cols(COL_Result).Width = _TotalWidth * 2.5
            .SetData(0, COL_Result, "Result")
            .Cols(COL_TestType).AllowEditing = True

            'Dim dt1 As DataTable
            'dt1 = fillusercombo()
            'Dim strUserName As New System.Text.StringBuilder
            'For j As Int32 = 0 To dt1.Rows.Count - 1
            '    If j > 0 Then
            '        strUserName.Append("|")
            '    End If
            '    strUserName.Append(dt1.Rows(j)("sLoginName"))
            'Next



            .Cols(COL_UserName).Width = _TotalWidth * 1.5
            .SetData(0, COL_UserName, "UserName")
            .Cols(COL_UserName).AllowEditing = True

            Dim cstyle1 As CellStyle
            ' cstyle1 = .Styles.Add("BubbleValues")
            Try
                If (.Styles.Contains("BubbleValues")) Then
                    cstyle1 = .Styles("BubbleValues")
                Else
                    cstyle1 = .Styles.Add("BubbleValues")

                End If
            Catch ex As Exception
                cstyle1 = .Styles.Add("BubbleValues")

            End Try
            cstyle1.ComboList = "..."
            .Cols(COL_UserNameButton).Style = cstyle1
            .Cols(COL_UserNameButton).Width = _TotalWidth * 0.25
            .ShowButtons = ShowButtonsEnum.Always
            '' Fill Values In ComboBox
            'csuser.ComboList = username
            '.Cols(COL_UserName).Style = csuser


            ' Table dt Contains following Columns
            ' StressID,PatientID,ExamId, VisitID , ClinicID

            For i = 0 To dt.Rows.Count - 1
                .Rows.Add()

                ' Fill the Retrived information to relative controls
                .SetData(i + 1, COL_StressID, dt.Rows(i)("StressID"))
                .SetData(i + 1, COL_PatientID, dt.Rows(i)("PatientID"))
                .SetData(i + 1, COL_ExamID, dt.Rows(i)("ExamID"))
                .SetData(i + 1, COL_VisitID, dt.Rows(i)("VisitID"))
                .SetData(i + 1, COL_ClinicID, dt.Rows(i)("ClinicID"))
                .SetData(i + 1, COL_DateofStudy, Format(dt.Rows(i)("DateofStudy"), "MM/dd/yyyy"))
                .SetData(i + 1, COL_TestType, dt.Rows(i)("TestType"))
                .SetData(i + 1, COL_Procedure, dt.Rows(i)("Procedures"))
                .SetData(i + 1, COL_Result, dt.Rows(i)("Result"))

                Dim arrUsers() As String = Split(dt.Rows(i)("UserName"), "|")
                Dim strUserName As System.Text.StringBuilder
                If arrUsers.Length > 0 Then
                    strUserName = New System.Text.StringBuilder
                    For icnt As Int32 = 0 To arrUsers.Length - 1
                        If icnt > 0 Then
                            strUserName.Append("|")
                        End If
                        strUserName.Append(arrUsers(icnt))
                    Next
                    Dim csuser As CellStyle '= .Styles.Add("UserName")
                    Try
                        If (.Styles.Contains("UserName")) Then
                            csuser = .Styles("UserName")
                        Else
                            csuser = .Styles.Add("UserName")

                        End If
                    Catch ex As Exception
                        csuser = .Styles.Add("UserName")

                    End Try
                    'Fill Value in combo box
                    csuser.ComboList = strUserName.ToString()
                    .SetCellStyle(i + 1, COL_UserName, csuser)
                    .SetData(i + 1, COL_UserName, arrUsers(0))
                End If
                .SetData(i + 1, COL_UserNameButton, "")
                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_UserNameButton, i + 1, COL_UserNameButton)
                rgDig.Style = cstyle1
                'Dim csUser As CellStyle = .Styles.Add("UserName" & i + 1)
                'If Not IsDBNull(dt.Rows(i)("UserName")) Then
                '    struser = dt.Rows(i)("UserName").ToString
                '    struser = struser.Replace(",", "|")
                'End If
                'csUser.ComboList = struser
                '.Cols(COL_UserName).Style = csUser

                '' .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription"))
                'Dim cR As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_UserName)
                'cR.Style = csUser

            Next
        End With
    End Sub

    Private Sub tsStressTest_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsStressTest.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveStressTest()
                Case "Close"
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Function SaveStressTest()
        Try
            Dim objstresstest As clsstresstest
            Dim Arrlist As New ArrayList
            Dim objEjectionFractionDBLayer As New ClsEjectionFractionDBLayer
            With C1StressList
                .Select(0, 0, False)
                For i As Int16 = 1 To .Rows.Count - 1
                    If IsNothing(.GetData(i, COL_StressID)) Then
                        If Not IsNothing(.GetData(i, COL_DateofStudy)) Then


                            objstresstest = New clsstresstest
                            objstresstest.ClinicID = 1
                            objstresstest.StressID = .GetData(i, COL_StressID)

                            objstresstest.PatientID = _PatientID
                            objstresstest.Examid = _ExamID
                            objstresstest.VisitID = _VisitID

                            .SetData(i, COL_PatientID, _PatientID)
                            .SetData(i, COL_ExamID, _ExamID)
                            .SetData(i, COL_VisitID, _VisitID)
                            .SetData(i, COL_ClinicID, 1)

                            objstresstest.DateofStudy = .GetData(i, COL_DateofStudy)
                            objstresstest.TestType = .GetData(i, COL_TestType)
                            objstresstest.procedure = .GetData(i, COL_Procedure)
                            objstresstest.Result = .GetData(i, COL_Result)
                            'objstresstest.UserName = .GetData(i, COL_UserName)
                            Dim rg As C1.Win.C1FlexGrid.CellRange = C1StressList.GetCellRange(i, COL_UserName, i, COL_UserName)
                            Dim UserStyle As CellStyle = rg.Style()

                            '.HistoryCategory = Trim(c1ProblemList.GetData(i, COL_PRESCRIPTION))
                            Dim struser As String = ""
                            If Not UserStyle Is Nothing Then
                                If UserStyle.ComboList <> "" Then
                                    struser = UserStyle.ComboList
                                    objstresstest.UserName = struser
                                End If
                            End If
                            Arrlist.Add(objstresstest)
                        End If
                    Else
                        objstresstest = New clsstresstest
                        objstresstest.ClinicID = 1
                        objstresstest.StressID = .GetData(i, COL_StressID)
                        objstresstest.PatientID = .GetData(i, COL_PatientID)
                        objstresstest.Examid = .GetData(i, COL_ExamID)
                        objstresstest.VisitID = .GetData(i, COL_VisitID)
                        objstresstest.DateofStudy = .GetData(i, COL_DateofStudy)
                        objstresstest.TestType = .GetData(i, COL_TestType)
                        objstresstest.procedure = .GetData(i, COL_Procedure)
                        objstresstest.Result = .GetData(i, COL_Result)
                        'objstresstest.UserName = .GetData(i, COL_UserName)
                        Dim rg As C1.Win.C1FlexGrid.CellRange = C1StressList.GetCellRange(i, COL_UserName, i, COL_UserName)
                        Dim UserStyle As CellStyle = rg.Style()

                        '.HistoryCategory = Trim(c1ProblemList.GetData(i, COL_PRESCRIPTION))
                        Dim struser As String = ""
                        If Not UserStyle Is Nothing Then
                            If UserStyle.ComboList <> "" Then
                                struser = UserStyle.ComboList
                                objstresstest.UserName = struser
                            End If
                        End If
                        Arrlist.Add(objstresstest)
                    End If
                Next
                objEjectionFractionDBLayer.SaveStressTest(Arrlist)
            End With
            objEjectionFractionDBLayer = Nothing
            Arrlist.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Return True

    End Function

    Private Sub C1StressList_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1StressList.CellButtonClick
        Try
            With C1StressList

                If (e.Col = COL_UserNameButton) Then

                    Dim ofrmUser As New frmUserList(True)
                    If Not IsNothing(.GetCellStyle(e.Row, COL_UserName)) Then
                        If Not IsNothing(.GetCellStyle(e.Row, COL_UserName).ComboList) Then
                            ofrmUser.Users = .GetCellStyle(e.Row, COL_UserName).ComboList
                        End If
                    End If

                    ofrmUser.WindowState = FormWindowState.Normal
                    ofrmUser.StartPosition = FormStartPosition.CenterParent
                    ofrmUser.ShowDialog(IIf(IsNothing(ofrmUser.Parent), Me, ofrmUser.Parent))

                    Dim strUserName As String = ofrmUser.Users
                    Dim csuser As CellStyle '= .Styles.Add("UserName")
                    Try
                        If (.Styles.Contains("UserName")) Then
                            csuser = .Styles("UserName")
                        Else
                            csuser = .Styles.Add("UserName")

                        End If
                    Catch ex As Exception
                        csuser = .Styles.Add("UserName")

                    End Try
                    'Fill Value in combo box
                    csuser.ComboList = strUserName.ToString()
                    .SetCellStyle(e.Row, COL_UserName, csuser)

                    If strUserName.Length > 0 Then
                        .SetData(e.Row, COL_UserName, strUserName.Split("|").GetValue(0))
                    End If
                    ofrmUser.Dispose()
                    ofrmUser = Nothing
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1StressList_ChangeEdit(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1StressList.ChangeEdit
        Dim _strSearchString As String = ""
        Try

            _strSearchString = C1StressList.Editor.Text

            If ogloGridListControl IsNot Nothing Then
                'ogloGridListControl.Search(_strSearchString, SearchColumn.Code); 
                'ogloGridListControl.InStringSearch(_strSearchString); 

                If C1StressList.Col = COL_Procedure Then
                    'ogloGridListControl.FillControl(_strSearchString); 

                    'Dim _cptCode As String = ""
                    'Dim _facilityCode As String = ""

                    'If C1StressList IsNot Nothing AndAlso C1StressList.Rows.Count > 0 Then
                    '    _cptCode = Convert.ToString(C1StressList.GetData(C1StressList.Row, COL_Procedure))
                    '    _facilityCode = Convert.ToString(C1StressList.GetData(C1StressList.Row, COL_Procedure))
                    '    ogloGridListControl.SelectedCPTCode = _cptCode
                    '    ogloGridListControl.SelectedFacilityCode = _facilityCode
                    'End If

                    ogloGridListControl.FillControl(_strSearchString)
                Else
                    ogloGridListControl.AdvanceSearch(_strSearchString)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1StressList_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1StressList.RowColChange
        If C1StressList.Col <> COL_Procedure Then
            CloseInternalControl()
        End If
    End Sub

    Private Sub C1StressList_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1StressList.SelChange

    End Sub

    Private Sub C1StressList_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1StressList.StartEdit
        Try
            If e.Col = COL_Procedure Then

                Dim str As String = C1StressList.Editor.Text
                OpenInternalControl(gloBilling.gloGridListControlType.CPT, "CPT", False, e.Row, e.Col, str)

            End If
        
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Function OpenInternalControl(ByVal ControlType As gloBilling.gloGridListControlType, ByVal ControlHeader As String, ByVal IsMultiSelect As Boolean, ByVal RowIndex As Integer, ByVal ColIndex As Integer, ByVal SearchText As String) As Boolean
        Dim _result As Boolean = False
        Try

            If Not ogloGridListControl Is Nothing Then
                CloseInternalControl()
            End If

            '            ogloGridListControl = New gloBilling.gloGridListControl


            ogloGridListControl = New gloBilling.gloGridListControl(ControlType, False, pnlInternalControl.Width, RowIndex, ColIndex)
            ogloGridListControl.DatabaseConnectionString = GetConnectionString()
            AddHandler ogloGridListControl.ItemSelected, AddressOf C1ControlItemSelected
            AddHandler ogloGridListControl.InternalGridKeyDown, AddressOf C1ControlInternalGridKeyDown
            'gloBilling.ogloGridListControl.ItemSelected += New gloBilling.gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected)
            'gloBilling.ogloGridListControl.InternalGridKeyDown += New gloBilling.gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown)

            ogloGridListControl.ClinicID = 1
            ogloGridListControl.ControlHeader = ControlHeader
            pnlInternalControl.Controls.Add(ogloGridListControl)
            ogloGridListControl.Dock = DockStyle.Fill

            'If SearchText <> "" Then
            '    ogloGridListControl.Search(SearchText, SearchColumn.Code)
            'End If
            ogloGridListControl.Show()

            Dim _x As Integer = C1StressList.Cols(ColIndex).Left
            Dim _y As Integer = C1StressList.Rows(RowIndex).Bottom
            Dim _width As Integer = pnlInternalControl.Width
            Dim _height As Integer = pnlInternalControl.Height
            Dim _parentleft As Integer = C1StressList.Left 'Me.Parent.Bounds.Left
            Dim _parentwidth As Integer = C1StressList.Width 'Me.Parent.Bounds.Width
            Dim _diffFactor As Integer = _parentwidth - _x

            If _diffFactor < _width Then
                _x = C1StressList.Bounds.Width + (_diffFactor) 'Me.Parent.Bounds.Width + (_diffFactor)
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            Else
                pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location)
            End If

            'pnlInternalControl.SetBounds(c1Transaction.Cols[ColIndex].Left, c1Transaction.Rows[RowIndex].Bottom, 0, 0, BoundsSpecified.Location);
            pnlInternalControl.Visible = True
            pnlInternalControl.BringToFront()
            ogloGridListControl.Focus()
            _result = True
        Catch ex As Exception

            _result = False
        Finally
            ' RePositionInternalControl()
        End Try
        Return _result
    End Function
    Private Sub C1ControlItemSelected(ByVal sender As Object, ByVal e As EventArgs)

        If Not IsNothing(ogloGridListControl) Then

            Dim strvalue As String = CType(ogloGridListControl.SelectedItems(0), gloGeneralItem.gloItem).Code
        
            C1StressList.SetData(C1StressList.RowSel, C1StressList.ColSel, strvalue)
        End If
        If Not IsNothing(ogloGridListControl) Then
            ogloGridListControl.Dispose()
            ogloGridListControl = Nothing
        End If
        pnlInternalControl.Visible = False
        pnlInternalControl.SendToBack()

    End Sub
    Private Sub C1ControlInternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Function CloseInternalControl() As Boolean
        Dim _result As Boolean = False
        Try
            'SLR: Changed on 2/4/2014
            For i As Integer = pnlInternalControl.Controls.Count - 1 To 0 Step -1
                pnlInternalControl.Controls.RemoveAt(i)
            Next
            If Not IsNothing(ogloGridListControl) Then
                Try
                    RemoveHandler ogloGridListControl.ItemSelected, AddressOf C1ControlItemSelected
                    RemoveHandler ogloGridListControl.InternalGridKeyDown, AddressOf C1ControlInternalGridKeyDown
                Catch ex As Exception

                End Try
              

                ogloGridListControl.Dispose()
                ogloGridListControl = Nothing
            End If
            pnlInternalControl.Visible = False
            pnlInternalControl.SendToBack()
            _result = True
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            _result = False
        Finally
        End Try
        Return _result
    End Function
End Class






