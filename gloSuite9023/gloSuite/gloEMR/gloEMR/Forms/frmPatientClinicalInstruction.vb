Public Class frmPatientClinicalInstruction

    Private oListControl As gloListControl.gloListControl
    Dim _nPatientId As Int64
    Dim _nID As Int64 = 0
    Dim _dtDate As DateTime = Now
    Dim dtMasterData As New DataTable()

    Dim _IsFromView As Boolean = False
    'Public Declare Function SetCursorPos Lib "user32" (ByVal X As Integer, ByVal Y As Integer) As Integer

    Public Sub New(ByVal PatientId As Int64, ByVal dtDate As DateTime)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _nPatientId = PatientId
        _dtDate = dtDate
        _IsFromView = False
    End Sub
    Public Sub New(ByVal PatientId As Int64, ByVal nID As Int64, ByVal IsFromView As Boolean)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _nPatientId = PatientId
        _nID = nID
        _IsFromView = IsFromView
    End Sub




    Private Sub btnInstruction_Click(sender As System.Object, e As System.EventArgs) Handles btnInstruction.Click
        Try

            Me.Cursor = Cursors.WaitCursor
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch ex As Exception

                End Try

                oListControl.Dispose()
                oListControl = Nothing
            End If


            oListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.ClinicalInstruction, True, Me.Width)
            oListControl.ControlHeader = "Clinical Instruction"
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            If dtMasterData IsNot Nothing AndAlso dtMasterData.Rows.Count > 0 Then
                For i As Integer = 0 To dtMasterData.Rows.Count - 1
                    oListControl.SelectedItems.Add(Convert.ToInt64(dtMasterData.Rows(i)("Id")), dtMasterData.Rows(i)("Instruction").ToString(), dtMasterData.Rows(i)("Description").ToString())
                Next
            End If


            Me.Controls.Add(oListControl)
            oListControl.OpenControl()
            pnlMain.Visible = False
            If oListControl.IsDisposed = False Then
                oListControl.Dock = DockStyle.Fill
                oListControl.BringToFront()
            End If
            Me.Cursor = Cursors.[Default]
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub oListControl_ItemClosedClick(sender As Object, e As EventArgs)
        Try
            If oListControl IsNot Nothing Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListControl.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
                Try
                    RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_SelectedClick
                    RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
                Catch ex As Exception

                End Try

                oListControl.Dispose()
                oListControl = Nothing
            End If
            pnlMain.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub oListControl_SelectedClick(sender As Object, e As EventArgs)
        Try

            Dim dtAddData As New DataTable()
            Dim dcId As New DataColumn("ID")
            Dim dcInstruction As New DataColumn("Instruction")
            Dim dcDescription As New DataColumn("Description")
            dtAddData.Columns.Add(dcId)
            dtAddData.Columns.Add(dcInstruction)
            dtAddData.Columns.Add(dcDescription)


            If oListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListControl.SelectedItems.Count - 1

                    Dim key As Object = oListControl.SelectedItems(i).ID
                    If dtMasterData.Rows.Find(key) Is Nothing Then

                        Dim drTemp As DataRow = dtMasterData.NewRow()
                        drTemp("ID") = oListControl.SelectedItems(i).ID
                        drTemp("Instruction") = oListControl.SelectedItems(i).Code
                        drTemp("Description") = oListControl.SelectedItems(i).Description
                        dtMasterData.Rows.Add(drTemp)


                        Dim drNew As DataRow = dtAddData.NewRow()
                        drNew("ID") = oListControl.SelectedItems(i).ID
                        drNew("Instruction") = oListControl.SelectedItems(i).Code
                        drNew("Description") = oListControl.SelectedItems(i).Description
                        dtAddData.Rows.Add(drNew)



                    End If

                Next
            End If
            Dim k As Integer = 0
            If dtAddData IsNot Nothing AndAlso dtAddData.Rows.Count > 0 Then
                For k = 0 To dtAddData.Rows.Count - 1
                    If txtInstruction.Text.Trim() <> "" Then
                        txtInstruction.Text = txtInstruction.Text + "," + dtAddData.Rows(k)("Instruction").ToString()
                    Else
                        txtInstruction.Text = dtAddData.Rows(k)("Instruction").ToString()
                    End If
                    If txtInstructionDtl.Text.Trim() <> "" Then
                        txtInstructionDtl.Text = txtInstructionDtl.Text + Environment.NewLine + Environment.NewLine + dtAddData.Rows(k)("Description").ToString()
                    Else
                        txtInstructionDtl.Text = dtAddData.Rows(k)("Description").ToString()
                    End If
                Next

            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlMain.Visible = True
        End Try



    End Sub

    Private Sub frmPatientClinicalInstruction_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try

            Dim dcId As New DataColumn("ID")
            Dim dcInstruction As New DataColumn("Instruction")
            Dim dcDescription As New DataColumn("Description")
            dtMasterData.Columns.Add(dcId)
            dtMasterData.Columns.Add(dcInstruction)
            dtMasterData.Columns.Add(dcDescription)
            dtMasterData.PrimaryKey = {dcId}
            Dim dtData As DataTable = Nothing


            Using objPatientClinicalInstruction As New ClsPatientClinicalInstruction()
                'If _IsFromView = False Then
                '    ' dtData = objPatientClinicalInstruction.GetPatientClinicalInstruction(_nPatientId, 0, Date.Now, _IsFromView)
                'Else
                If _IsFromView = True Then ''commented above to resolve bugid 65893
                    dtData = objPatientClinicalInstruction.GetPatientClinicalInstruction(_nPatientId, _nID, Nothing, _IsFromView)
                End If
            End Using


            If (Not IsNothing(dtData)) Then
                If (dtData.Rows.Count > 0) Then
                    _nID = Convert.ToInt64(dtData.Rows(0)("nId").ToString())
                    txtInstruction.Text = dtData.Rows(0)("sInstruction").ToString()
                    txtInstructionDtl.Text = dtData.Rows(0)("sInstructionDtl").ToString()
                    mskDate.Text = Format(dtData.Rows(0)("dtDate"), "MM/dd/yyyy")
                Else
                    mskDate.Text = Format(Date.Now, "MM/dd/yyyy")
                End If
            Else
                mskDate.Text = Format(Date.Now, "MM/dd/yyyy")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ts_btnOk_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnOk.Click
        Try
            If (ValidateData()) Then
                SavePatientClinicalInstruction()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()
    End Sub
    Private Sub SavePatientClinicalInstruction()

        Try
            Dim _ReturnId As Int64

            Using objPatientClinicalInstruction As New ClsPatientClinicalInstruction()
                _ReturnId = objPatientClinicalInstruction.SavePatientClinicalInstruction(_nID, _nPatientId, mskDate.Text, txtInstruction.Text.Trim(), txtInstructionDtl.Text.Trim(), True)
            End Using

            If (_ReturnId = 99999) Then
                MessageBox.Show("Clinical Instruction is already present for this visit. Enter a different date to save the record.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateData() As Boolean
        Dim _IsValid As Boolean = True
        Try
            If (txtInstructionDtl.Text.Trim() = "") Then
                MessageBox.Show("Please enter instruction description. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                Return _IsValid
            ElseIf (mskDate.Text.Trim() = "/  /") Then
                MessageBox.Show("Please enter date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _IsValid = False
                Return _IsValid
            End If


            If mskDate.Text <> "" Then
                If mskDate.Text <> "  /  /" Then
                    If gloDateMaster.gloDate.IsValidDateV2(mskDate.Text) = False Then

                        MessageBox.Show("Please enter a valid date.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mskDate.Focus()
                        ValidateData = Nothing
                        Exit Function

                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return _IsValid
    End Function

   

    Private Sub frmPatientClinicalInstruction_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If oListControl IsNot Nothing Then
            oListControl.Dispose()
        End If
    End Sub
End Class