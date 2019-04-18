Public Class frmDASSettings

    Private Col_MapTo As Integer = 0
    Private Col_TestName As Integer = 1
    Private Col_ResultName As Integer = 2
    Private Col_LoincCode As Integer = 3

    Public SelectedESRCRP As String = ""
    Public TestName As String = ""

    Dim dsDAS As DataSet

    Private _TestID As Int64 = 0

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim dResult As DialogResult
        Me.BindingContext(dsDAS, "DASTest").EndCurrentEdit()
        Me.BindingContext(dsDAS, "DASTest.DASTestDASTestResult").EndCurrentEdit()
        If dsDAS.HasChanges() Then
            dResult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
        Else
            dsDAS.AcceptChanges()
            Me.Close()
        End If
        If dResult = Windows.Forms.DialogResult.Yes Then
            If SaveDASSettings() Then
                dsDAS.AcceptChanges()
                Me.Close()
            End If
        End If
        If dResult = Windows.Forms.DialogResult.No Then
            dsDAS.AcceptChanges()
            Me.Close()
        End If
       
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Rowno As Integer = 0
            If txtTestName.Text.Trim() = "" Then
                MessageBox.Show("Test name can not be blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal TestId As Int64)

        ' This call is required by the designer.
        InitializeComponent()
        _TestID = TestId
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub SetGridStyle()

        C1ResultDetails.Cols(0).Width = 0
        C1ResultDetails.Cols(1).Width = C1ResultDetails.Width * 0.7
        C1ResultDetails.Cols(2).Width = C1ResultDetails.Width * 0.3

        'visible
        C1ResultDetails.Cols(0).Visible = False
        C1ResultDetails.Cols(1).Visible = True
        C1ResultDetails.Cols(2).Visible = True

    End Sub

    Private Sub frmDASSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim dResult As DialogResult
            Me.BindingContext(dsDAS, "DASTest").EndCurrentEdit()
            Me.BindingContext(dsDAS, "DASTest.DASTestDASTestResult").EndCurrentEdit()
            If dsDAS.HasChanges() Then
                dResult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
            Else
            End If
            If dResult = Windows.Forms.DialogResult.Yes Then
                If SaveDASSettings() = False Then
                    e.Cancel = True
                End If
            End If
            If dResult = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmDASSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If _TestID = 0 Then

        'Else
        Try

            FillGrid(_TestID)

            If _TestID = 0 Then 'New Mode
                dsDAS.Tables("DASTest").Rows.Add()
                Me.BindingContext(dsDAS, "DASTest").Position = dsDAS.Tables("DASTest").Rows.Count - 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'End If
    End Sub
    Private Sub FillGrid(ByVal Id As Int64)
        Try
            Dim ocls As New clsDASSettings

            dsDAS = ocls.GetDASTestResult(_TestID)

            C1ResultDetails.SetDataBinding(dsDAS, "DASTest.DASTestDASTestResult")
           
            txtTestName.DataBindings.Add("Text", dsDAS, "DASTest.TestName")
            rbtnCRP.DataBindings.Add("Checked", dsDAS, "DASTest.IsCRP")
            rbtnESR.DataBindings.Add("Checked", dsDAS, "DASTest.IsESR")

            SetStyleGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub SetStyleGrid()

        Try

            C1ResultDetails.Cols(0).Visible = False
            C1ResultDetails.Cols(1).Visible = False
            C1ResultDetails.Cols(2).Visible = False
            C1ResultDetails.Cols(5).Visible = False

            C1ResultDetails.Cols(0).Width = 0
            C1ResultDetails.Cols(1).Width = 0
            C1ResultDetails.Cols(2).Width = 0
            C1ResultDetails.Cols(3).Width = C1ResultDetails.Width * 0.7
            C1ResultDetails.Cols(4).Width = C1ResultDetails.Width * 0.3
            C1ResultDetails.Cols(5).Width = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try

            If txtTestName.Text.Trim() = "" Then
                MessageBox.Show("Test name can not be blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If SaveDASSettings() Then
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function SaveDASSettings() As Boolean
        If txtTestName.Text.Trim() = "" Then
            MessageBox.Show("Test name can not be blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If C1ResultDetails.Rows.Count > 1 Then
            If ValidateDtResult() = False Then
                Return False
            End If
        End If

        Dim ocls As New clsDASSettings
        Try
            If Not IsNothing(dsDAS) Then
                txtTestName.Focus()
                Me.BindingContext(dsDAS, "DASTest").EndCurrentEdit()
                Me.BindingContext(dsDAS, "DASTest.DASTestDASTestResult").EndCurrentEdit()
            End If
            If Not dsDAS.GetChanges Is Nothing Then
                dsDAS = SetRowState(dsDAS.GetChanges)
                If ocls.SaveDataset(dsDAS, "DASTestResult", "InUpDel_DASSettings", "@DASTestResult") = 0 Then
                    Return False
                End If
                dsDAS.AcceptChanges()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        Finally
            ocls.Dispose()
            ocls = Nothing
        End Try
        Return True
    End Function

    Public Function ValidateDtResult() As Boolean
        Try
            Dim i As Integer
            C1ResultDetails.Select()
            For i = 1 To C1ResultDetails.Rows.Count - 2
                If Convert.ToString(C1ResultDetails.GetData(i, 3)).Trim() = "" Then
                    MessageBox.Show("Result name can not be blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Return False
    End Function

    Public Function SetRowState(ByVal dsMain As DataSet) As DataSet

        Try

            dsMain.EnforceConstraints = False
            For intTables As Integer = 0 To dsMain.Tables.Count - 1

                For intCount As Integer = 0 To dsMain.Tables(intTables).Rows.Count - 1
                    If dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Added" Then
                        dsMain.Tables(intTables).Rows(intCount)("RowState") = "Added"
                    ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Modified" Then
                        dsMain.Tables(intTables).Rows(intCount)("RowState") = "Modified"
                    ElseIf dsMain.Tables(intTables).Rows(intCount).RowState.ToString = "Deleted" Then
                        dsMain.Tables(intTables).Rows(intCount).RejectChanges()
                        dsMain.Tables(intTables).Rows(intCount)("RowState") = "Deleted"
                    End If
                Next

            Next

            Return dsMain
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function
    
    Private Sub btnSearchLabTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLabTest.Click
        Try
            Dim ofrm As New frmLabTests
            ofrm.StartPosition = FormStartPosition.CenterScreen
            If Not txtTestName.Text = "" Then
                ofrm.strSerchText = txtTestName.Text
            End If
            ofrm.ShowDialog()
            If Not ofrm.strTestName = "" Then
                txtTestName.Text = ofrm.strTestName
            End If
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClearTestName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTestName.Click
        txtTestName.Text = ""
    End Sub
End Class