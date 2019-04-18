Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmLab_GroupMaster
    Public nEditID As Int64
    Public sEditName As String
    Public sEditCode As String
    Public blnIsModify As Boolean = False

    Private Const COL_SELECT = 0
    Private Const COL_ID = 1
    Private Const COL_CODE = 2
    Private Const COL_NAME = 3
    Private Const COL_COUNT = 4

    Private Sub frmLab_GroupMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1LabTest)
        Try
            C1LabTest.BeginInit()

            Fill_Tests()

            If blnIsModify = False Then
                'open in edit mode
                Dim oLabGroup As New gloEMRLabGroup
                Dim oGroup As LabActor.LabGroup

                oGroup = oLabGroup.GetGroup(nEditID)
                If Not oGroup Is Nothing Then
                    txtCode.Text = oGroup.LabGroupCode
                    txtGroup.Text = oGroup.LabGroupName
                    For i As Int16 = 0 To oGroup.Tests.Count - 1
                        For j As Int16 = 1 To C1LabTest.Rows.Count - 1
                            If C1LabTest.GetData(j, COL_ID) = oGroup.Tests.Item(i).TestID Then
                                C1LabTest.SetData(j, COL_SELECT, True)
                                Exit For
                            End If
                        Next
                    Next
                    oGroup.Dispose()
                    oGroup = Nothing
                End If
                oLabGroup.Dispose()
                oLabGroup = Nothing
                'oGroup = Nothing

                'open in add mode
            End If

            txtCode.Select()
            '  C1LabTest.ColSel = 1
            C1LabTest.EndInit()
        Catch ex As Exception
            C1LabTest.EndInit()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub SetGridStyle()
        With C1LabTest
            .Cols.Fixed = 0
            .Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.White

            '.Styles.Fixed.BackColor = Color.FromArgb(51, 125, 207)
            '.Styles.Fixed.ForeColor = Color.White
            '.Styles.Fixed.Font = New Font("Arial", 10, FontStyle.Bold)

            '.Styles.Alternate.BackColor = Color.FromArgb(234, 242, 252) '' Color.LightBlue
            '.Styles.Alternate.ForeColor = Color.Black
            '.Styles.Alternate.Border.Color = Drawing.SystemColors.Control

            '.Styles.Normal.BackColor = Color.GhostWhite
            '.Styles.Normal.ForeColor = Color.Black
            '.Styles.Normal.Border.Color = Drawing.SystemColors.Control

            '.Styles.Highlight.BackColor = Color.FromArgb(255, 224, 160)
            '.Styles.Highlight.ForeColor = Color.Black

            '.Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            '.Styles.Focus.ForeColor = Color.Black
            '.Rows(0).Height = 21
        End With
    End Sub

    Private Sub Fill_Tests()
        Try
            C1LabTest.BeginInit()

            With C1LabTest
                'C1LabTest.Clear()
                SetGridStyle()
                C1LabTest.DataSource = Nothing
                .Cols.Count = COL_COUNT
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Clear(C1.Win.C1FlexGrid.ClearFlags.All)

                .SetData(0, COL_SELECT, "Select")
                .SetData(0, COL_ID, "ID")
                .SetData(0, COL_CODE, "Code")
                .SetData(0, COL_NAME, "Name")

                Dim _Width As Single = (.Width - 20) / 5
                .Cols(COL_ID).Width = 0
                .Cols(COL_SELECT).Width = _Width * 0.6
                .Cols(COL_CODE).Width = _Width * 1.5
                .Cols(COL_NAME).Width = _Width * 2.9

                .Cols(COL_ID).DataType = GetType(Int64)
                .Cols(COL_SELECT).DataType = GetType(Boolean)
                .Cols(COL_CODE).DataType = GetType(String)
                .Cols(COL_NAME).DataType = GetType(String)
            End With

            Dim oTests As LabActor.Tests
            Dim oLabTest As New gloEMRLabTest
            oTests = oLabTest.GetTests(False)
            If Not oTests Is Nothing Then
                With C1LabTest
                    For i As Int16 = 0 To oTests.Count - 1
                        .Rows.Add()
                        .SetData(.Rows.Count - 1, COL_SELECT, CheckState.Unchecked)
                        .SetData(.Rows.Count - 1, COL_ID, oTests.Item(i).TestID)
                        .SetData(.Rows.Count - 1, COL_CODE, oTests.Item(i).Code)
                        .SetData(.Rows.Count - 1, COL_NAME, oTests.Item(i).Name)
                    Next
                End With
                oTests.Dispose()
                oTests = Nothing
            End If
            'oTests = Nothing
            oLabTest.Dispose()
            oLabTest = Nothing
            C1LabTest.EndInit()

        Catch ex As Exception
            C1LabTest.EndInit()
            Throw ex
        End Try

    End Sub

    Private Sub SaveGroupMaster()
        Dim oLabGroup As New gloEMRLabGroup
        Dim blnTestFlag As Boolean = False

        Try
            'Do the Result grid validations
            If txtCode.Text = "" And txtGroup.Text = "" Then
                MessageBox.Show("Please enter Group Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtCode.Text = "" Then
                MessageBox.Show("Please enter Group Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCode.Text = ""
                txtCode.Select()
                Exit Sub
            End If
            If txtGroup.Text = "" Then
                MessageBox.Show("Please enter Group Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtGroup.Text = ""
                txtGroup.Select()
                Exit Sub
            End If

            C1LabTest.BeginInit()
            For i As Int16 = 1 To C1LabTest.Rows.Count - 1
                If C1LabTest.GetData(i, COL_SELECT) = True Then
                    blnTestFlag = True
                    Exit For
                End If
            Next
            C1LabTest.EndInit()

            If blnTestFlag = False Then
                MessageBox.Show("Please select atleast one Test.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            If blnIsModify = True Then

                '' Validation for duplicate records for add

                If oLabGroup.IsCodeExists(txtCode.Text) = True And oLabGroup.IsExists(txtGroup.Text) = True Then
                    MessageBox.Show("Duplicate Group Code and Name.Please enter another Group Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtGroup.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                If oLabGroup.IsCodeExists(txtCode.Text) = True Then
                    MessageBox.Show("Duplicate Group Code.Please enter another Group Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtCode.Text = ""
                    txtCode.Select()
                    Exit Sub
                End If
                If oLabGroup.IsExists(txtGroup.Text) = True Then
                    MessageBox.Show("Duplicate Group Name.Please enter another Group Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtGroup.Text = ""
                    txtGroup.Select()
                    Exit Sub
                End If



                With oLabGroup.LabGroup
                    .LabGroupID = 0
                    .LabGroupCode = txtCode.Text
                    .LabGroupName = txtGroup.Text

                    With .Tests
                        Dim oTest As LabActor.Test
                        C1LabTest.BeginInit()
                        'Fetch all the rows from the Flex Grid
                        For i As Int16 = 1 To C1LabTest.Rows.Count - 1
                            oTest = New LabActor.Test
                            If C1LabTest.GetData(i, COL_SELECT) = True Then
                                oTest.TestID = C1LabTest.GetData(i, COL_ID)
                            End If
                            .Add(oTest)
                            oTest = Nothing
                        Next
                        C1LabTest.EndInit()
                    End With
                End With
                oLabGroup.Add()
            Else
                'validation for duplicate records for modify
                If UCase(txtCode.Text) <> UCase(sEditCode) And UCase(txtGroup.Text) <> UCase(sEditName) Then
                    If oLabGroup.IsCodeExists(txtCode.Text) = True And oLabGroup.IsExists(txtGroup.Text) = True Then
                        MessageBox.Show("Duplicate Group Code and Name.Please enter another Group Code and Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtGroup.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If
                If UCase(txtCode.Text) <> UCase(sEditCode) Then
                    If oLabGroup.IsCodeExists(txtCode.Text) = True Then
                        MessageBox.Show("Duplicate Group Code.Please enter another Group Code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtCode.Text = ""
                        txtCode.Select()
                        Exit Sub
                    End If
                End If

                If UCase(txtGroup.Text) <> UCase(sEditName) Then
                    If oLabGroup.IsExists(txtGroup.Text) = True Then
                        MessageBox.Show("Duplicate Group Name.Please enter another Group Name.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtGroup.Text = ""
                        txtGroup.Select()
                        Exit Sub
                    End If
                End If

                With oLabGroup.LabGroup
                    .LabGroupID = nEditID
                    .LabGroupCode = txtCode.Text
                    .LabGroupName = txtGroup.Text

                    With .Tests
                        Dim oTest As LabActor.Test
                        C1LabTest.BeginInit()
                        'Fetch all the rows from the Flex Grid
                        For i As Int16 = 1 To C1LabTest.Rows.Count - 1
                            oTest = New LabActor.Test
                            If C1LabTest.GetData(i, COL_SELECT) = True Then
                                oTest.TestID = C1LabTest.GetData(i, COL_ID)
                            End If
                            .Add(oTest)
                            oTest = Nothing
                        Next
                        C1LabTest.EndInit()

                    End With

                End With

                oLabGroup.Modify(nEditID)
            End If

            If blnIsModify = True Then
                txtCode.Text = ""
                txtGroup.Text = ""
                txtCode.Select()
                C1LabTest.BeginInit()
                For i As Int16 = 1 To C1LabTest.Rows.Count - 1
                    C1LabTest.SetData(i, COL_SELECT, False)
                Next
                C1LabTest.EndInit()
                ' Else
                ' Me.Close()
            End If
            C1LabTest.EndInit()
            Me.Close()
            'txtCode.Select()
        Catch ex As Exception
            C1LabTest.EndInit()
            If blnIsModify = False Then
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            End If
        Finally
            oLabGroup.Dispose()
            oLabGroup = Nothing
        End Try
    End Sub

  
   
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor

            Select Case Trim(C1LabTest.Cols(C1LabTest.ColSel).Caption)
                Case "Code"
                    Dim _introw As Integer = C1LabTest.FindRow(txtSearch.Text.Trim, 1, 2, False, False, False)
                    C1LabTest.Row = _introw
                    '  C1LabTest.Select(_introw, True)
                Case "Select"
                    Dim _introw As Integer = C1LabTest.FindRow(txtSearch.Text.Trim, 1, 2, False, False, False)
                    C1LabTest.Row = _introw
                    '  C1LabTest.Select(_introw, True)
                Case "Name"
                    Dim _introw As Integer = C1LabTest.FindRow(txtSearch.Text.Trim, 1, 3, False, False, False)
                    C1LabTest.Row = _introw
                    ' C1LabTest.Select(_introw, True)
            End Select
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1LabTest_AfterRowColChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1LabTest.AfterRowColChange
        Try
            If C1LabTest.Cols(C1LabTest.ColSel).Caption = "Code" And C1LabTest.Cols(C1LabTest.ColSel).Caption = "Select" Then
                lblSearch.Text = "Search on Code"
            ElseIf C1LabTest.Cols(C1LabTest.ColSel).Caption = "Name" Then
                lblSearch.Text = "Search on " & C1LabTest.Cols(C1LabTest.ColSel).Caption & ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_GroupMaster_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_GroupMaster.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveGroupMaster()

                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try

    End Sub

   
    Private Sub C1LabTest_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1LabTest.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class