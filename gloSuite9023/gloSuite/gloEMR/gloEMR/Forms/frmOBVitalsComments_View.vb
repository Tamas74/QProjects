Imports gloEMR.gloOBVitals
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmOBVitalComments_View
    Dim dv As DataView
    Private objclsOBVitalsComments As New clsPatientVitals
    Dim _PatientID As Long
    Private Const COL_ID = 0
    Private Const COL_CODE = 1
    Private Const COL_NAME = 2
    Private Const COL_OrderType = 3
    Private Const COL_StructuredLabResults = 4
    Private Const COL_OBTranofCare = 5
    Private COL_COUNT = 3
    Private ID As Long = 0
    Public Shared IsOpen As Boolean = False
    Dim _isCommentAdded As Boolean = False
    Dim oTests As LabActor.Tests = Nothing
    Private blnRefreshClicked As Boolean
    Private Shared frm As frmOBVitalComments_View
    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub
    Public Shared Function GetInstance(ByVal Patientid As Long) As frmOBVitalComments_View

        Try


            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmOBVitalComments_View" Then

                    If CType(f, frmOBVitalComments_View)._PatientID = Patientid Then
                        IsOpen = True
                        frm = f
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmOBVitalComments_View(Patientid)
            End If

        Finally

        End Try
        Return frm
    End Function
    Private Sub frmOBVitalComments_View_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gloC1FlexStyle.Style(c1OBVitals)
            '27-Oct-15 Aniket: Bug #90735: gloEMR: OB vital comment- Applicaton does not keep user defined size of grid
            SetGridStyle()
            Fill_List()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub SetGridStyle()
        With c1OBVitals
            .Styles.Normal.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Styles.Alternate.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .ExtendLastCol = True
        End With
    End Sub
    Private Sub Fill_List()
        Try
            With c1OBVitals
                c1OBVitals.Visible = False
                'c1OBVitals.Clear()
                c1OBVitals.DataSource = Nothing
                COL_COUNT = 1
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Clear(C1.Win.C1FlexGrid.ClearFlags.All)
                .SetData(0, 0, "Comments_ID")
                .SetData(0, 1, "Comments")
                Dim _Width As Single = (.Width - 20) / 2
                .Cols(0).Width = 0
                .Cols(1).Width = _Width * 1
            End With

            Dim oOBVitalsAckNotes As New ClsOBVitalsComment
            Try
                Dim dtNotes As DataTable = oOBVitalsAckNotes.Get_OBVitalComments(0)
                If Not dtNotes Is Nothing AndAlso dtNotes.Rows.Count > 0 Then
                    With c1OBVitals
                        For i As Int32 = 0 To dtNotes.Rows.Count - 1
                            If (txtListSearch.Text = "") Or (dtNotes.Rows(i)("Comments").ToString.ToUpper.Contains(txtListSearch.Text.ToUpper)) Then
                                .Rows.Add()
                                .SetData(.Rows.Count - 1, 0, dtNotes.Rows(i)("Comments_ID"))
                                .SetData(.Rows.Count - 1, 1, dtNotes.Rows(i)("Comments"))
                            End If


                        Next

                    End With
                End If

                If Not dtNotes Is Nothing Then
                    dtNotes.Dispose()
                    dtNotes = Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                oOBVitalsAckNotes.Dispose()
                oOBVitalsAckNotes = Nothing
            End Try

            If _isCommentAdded = True Then
                Dim _FindRow As Integer = c1OBVitals.FindRow(ID, 0, COL_ID, True, True, False)
                c1OBVitals.Select(_FindRow, 0, True)
            Else
                c1OBVitals.ColSel = 1
                c1OBVitals.TopRow = 0
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "View Patient OBVitals Opened", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, 0, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch ex As Exception
            Throw ex
        Finally
            _isCommentAdded = False
            c1OBVitals.Visible = True
        End Try
    End Sub
    Private Sub frmOBVitals_TestView_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim Criteria As String = ""
        Try
            With c1OBVitals
                If .Cols.Count = 2 Then
                    Dim _Width As Single = (.Width - 20) / 2
                    .Cols(0).Width = 0
                    .Cols(1).Width = _Width * 1

                ElseIf .Cols.Count = COL_COUNT Then
                    Dim _Width As Single = (.Width - 20) / 5
                    .Cols(COL_ID).Width = 0
                    .Cols(COL_NAME).Width = _Width * 3.5
                End If

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    Private Sub AddCategory()
        Try
            Dim oOBVital_Comments As New frmOBVital_Comments
            oOBVital_Comments.CommentType = "OBComments"
            oOBVital_Comments.ShowDialog(IIf(IsNothing(oOBVital_Comments.Parent), Me, oOBVital_Comments.Parent))
            ID = oOBVital_Comments.CurrentCommentsID
            oOBVital_Comments.Dispose()
            oOBVital_Comments = Nothing
            _isCommentAdded = True
            RefreshCategory()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Dim _ID As Long = 0
        Dim _Code As String = ""
        Dim _Name As String = ""
        Try
            If c1OBVitals.Rows.Count > 1 Then
                If c1OBVitals.Row > 0 Then
                    _ID = c1OBVitals.GetData(c1OBVitals.Row, COL_ID)
                    If c1OBVitals.Cols.Count = COL_COUNT Then
                        _Code = c1OBVitals.GetData(c1OBVitals.Row, COL_CODE) & ""
                        _Name = c1OBVitals.GetData(c1OBVitals.Row, COL_NAME) & ""
                    End If
                    If _ID > 0 Then
                        Dim oOBVital_CommentsMst As New frmOBVital_Comments
                        oOBVital_CommentsMst.CommentType = "OBComments"
                        oOBVital_CommentsMst._blnModify = True
                        oOBVital_CommentsMst._OBVitalCommentsID = c1OBVitals.GetData(c1OBVitals.Row, 0)
                        oOBVital_CommentsMst._Notes = c1OBVitals.GetData(c1OBVitals.Row, 1)
                        oOBVital_CommentsMst.ShowDialog(IIf(IsNothing(oOBVital_CommentsMst.Parent), Me, oOBVital_CommentsMst.Parent))
                        oOBVital_CommentsMst.Dispose()
                        oOBVital_CommentsMst = Nothing
                        RefreshCategory()
                        Dim _FindRow As Integer = c1OBVitals.FindRow(_ID, 0, COL_ID, True, True, False)
                        c1OBVitals.Select(_FindRow, 0, True)
                    End If
                Else
                    MessageBox.Show("Select the record to be modified", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("There are no comments to modify", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            Dim _ID As Long = 0
            If c1OBVitals.Rows.Count > 1 Then
                If c1OBVitals.Row > 0 Then
                    _ID = c1OBVitals.GetData(c1OBVitals.Row, COL_ID)
                    If _ID > 0 Then
                        If MessageBox.Show("Are you sure, you want to delete the record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                            Dim oOBVitalsComments As New ClsOBVitalsComment
                            oOBVitalsComments.Delete(_ID)
                            oOBVitalsComments.Dispose()
                            oOBVitalsComments = Nothing
                            RefreshCategory()
                            c1OBVitals.ColSel = 1
                            c1OBVitals.TopRow = 0
                        End If
                    Else
                        MessageBox.Show("There are no record to delete", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                    End If
                Else
                    MessageBox.Show("Select the record to be deleted", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                End If
            Else
                MessageBox.Show("There are no Comments to delete", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub RefreshCategory()
        Try
            txtListSearch.Focus()
            Fill_List()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub txtListSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1OBVitals.Rows.Count >= 0 Then
                    c1OBVitals.Select(0, 0)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.Validation, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "New"
                Call AddCategory()
            Case "Modify"
                Call UpdateCategory()

            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                blnRefreshClicked = True
                txtListSearch.Clear()
                Call RefreshCategory()
                blnRefreshClicked = False
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub txtListSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtListSearch.TextChanged
        Dim dvOBVitals As DataView = Nothing
        Dim C1OBVitals_DataTable As DataTable
        Dim oCol As DataColumn
        Try
            If txtListSearch.Text.Trim <> "" Then
                Fill_List()
                C1OBVitals_DataTable = New DataTable
                If c1OBVitals.Cols.Count > 0 Then
                    oCol = New DataColumn
                    For i As Integer = 0 To c1OBVitals.Cols.Count - 1
                        oCol.Caption = c1OBVitals.GetData(0, i)
                        oCol.ColumnName = c1OBVitals.GetData(0, i)
                        C1OBVitals_DataTable.Columns.Add(c1OBVitals.GetData(0, i))
                    Next

                End If
                Dim oRow As DataRow
                If c1OBVitals.Rows.Count > 1 Then

                    For iRow As Integer = 1 To c1OBVitals.Rows.Count - 1
                        oRow = C1OBVitals_DataTable.NewRow
                        For iCol As Integer = 0 To c1OBVitals.Cols.Count - 1
                            oRow(iCol) = c1OBVitals.GetData(iRow, iCol)
                        Next
                        C1OBVitals_DataTable.Rows.Add(oRow)
                    Next
                    dvOBVitals = C1OBVitals_DataTable.DefaultView
                End If

                If IsNothing(dvOBVitals) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                'c1OBVitals.Clear()
                c1OBVitals.DataSource = Nothing
                c1OBVitals.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
                c1OBVitals.DataSource = dvOBVitals

                Dim strOBVitals As String
                If Trim(txtListSearch.Text) <> "" Then
                    strOBVitals = Replace(txtListSearch.Text, "'", "''")
                    If (strOBVitals.StartsWith("*") = True) Then
                        strOBVitals = Replace(strOBVitals, "*", "") & ""
                        strOBVitals = "*" + strOBVitals
                    Else
                        strOBVitals = Replace(strOBVitals, "*", "") & ""
                    End If
                    strOBVitals = Replace(strOBVitals, "[", "") & ""
                    strOBVitals = mdlGeneral.ReplaceSpecialCharacters(strOBVitals)
                Else
                    strOBVitals = ""
                End If

                dvOBVitals.RowFilter = "[" & dvOBVitals.Table.Columns(1).ColumnName & "]" & " Like '%" & strOBVitals & "%'"

                If dvOBVitals.Count > 0 Then
                    c1OBVitals.RowSel = 1
                End If
            Else
                Fill_List()
            End If
            Dim _Width As Single = (c1OBVitals.Width - 20) / 2
            c1OBVitals.Cols(0).Width = 0
            c1OBVitals.Cols(1).Width = _Width * 1
            c1OBVitals.ColSel = 1
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.OBPlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Query, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1OBVitals_DoubleClick(sender As Object, e As System.EventArgs) Handles c1OBVitals.DoubleClick
        UpdateCategory()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        txtListSearch.ResetText()
        txtListSearch.Focus()
    End Sub
    Private Sub c1OBVitals_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1OBVitals.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class