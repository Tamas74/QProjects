Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class frmIntuitStaffMapping

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim objfrmAddStaffMapping As New frmAddStaffMapping
        objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
        objfrmAddStaffMapping.ShowDialog()
        objfrmAddStaffMapping.Dispose()
        objfrmAddStaffMapping = Nothing
        FillC1StaffMapping()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim oclsgloIntuit As clsgloIntuit
        Try
            If C1StaffMapping.RowSel >= 1 Then
                If MessageBox.Show("Are you sure you want to delete this Staff ID?", "gloEMR Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    oclsgloIntuit = New clsgloIntuit
                    Dim _CheckMessagecnt As Integer = 0
                    _CheckMessagecnt = (oclsgloIntuit.checkStaffIDAssociation(C1StaffMapping.GetData(C1StaffMapping.RowSel, 1)))
                    If _CheckMessagecnt > 0 Then
                        MessageBox.Show("Staff ID cannot be deleted as it is used further.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    oclsgloIntuit.DeleteStaffMapping(C1StaffMapping.GetData(C1StaffMapping.RowSel, 0))
                    FillC1StaffMapping()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
       
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub FillC1StaffMapping()
        Dim oclsgloIntuit As clsgloIntuit
        Dim dt As DataTable
        Try
            oclsgloIntuit = New clsgloIntuit
            dt = oclsgloIntuit.GetStaffMapping()
            If Not IsNothing(dt) Then
                C1StaffMapping.DataSource = dt.DefaultView
                If C1StaffMapping.Rows.Count > 1 Then
                    C1StaffMapping.Select(1, 1)
                End If
            End If
            SetGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
    End Sub


    Private Sub SetGridStyle()
        'Width
        C1StaffMapping.Cols(0).Width = 0
        C1StaffMapping.Cols(1).Width = C1StaffMapping.Width * 0.3
        C1StaffMapping.Cols(2).Width = C1StaffMapping.Width * 0.7
        'visible
        C1StaffMapping.Cols(1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1StaffMapping.Cols(0).Visible = False
        C1StaffMapping.Cols(1).Visible = True
        C1StaffMapping.Cols(2).Visible = True
        C1StaffMapping.AllowEditing = False

    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmIntuitStaffMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillC1StaffMapping()
    End Sub
    Private Sub btn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Modify.Click
        Dim oclsgloIntuit As clsgloIntuit = New clsgloIntuit
        Try
            If C1StaffMapping.RowSel >= 1 Then
                Dim objfrmAddStaffMapping As New frmAddStaffMapping
                objfrmAddStaffMapping.MappingID = C1StaffMapping.GetData(C1StaffMapping.RowSel, 0)
                objfrmAddStaffMapping.staffID = C1StaffMapping.GetData(C1StaffMapping.RowSel, 1)
                objfrmAddStaffMapping.Description = C1StaffMapping.GetData(C1StaffMapping.RowSel, 2)
                objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
                If oclsgloIntuit.IsPatientPortalEnabled() Then
                    If C1StaffMapping.GetData(C1StaffMapping.RowSel, 2).ToString() = CheckMappingType(C1StaffMapping.GetData(C1StaffMapping.RowSel, 0).ToString(), C1StaffMapping.GetData(C1StaffMapping.RowSel, 2).ToString()) Then
                        objfrmAddStaffMapping.btnSearchUser.Enabled = False
                        objfrmAddStaffMapping.btnClearTestName.Enabled = False
                        objfrmAddStaffMapping.cmb_To.Enabled = False
                    End If
                End If
                objfrmAddStaffMapping.ShowDialog()
                objfrmAddStaffMapping.Dispose()
                objfrmAddStaffMapping = Nothing
                FillC1StaffMapping()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
    End Sub

    Private Sub C1StaffMapping_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1StaffMapping.DoubleClick
        Dim oclsgloIntuit As clsgloIntuit = New clsgloIntuit
        Try
            If C1StaffMapping.RowSel >= 1 Then
                Dim objfrmAddStaffMapping As New frmAddStaffMapping
                objfrmAddStaffMapping.MappingID = C1StaffMapping.GetData(C1StaffMapping.RowSel, 0)
                objfrmAddStaffMapping.staffID = C1StaffMapping.GetData(C1StaffMapping.RowSel, 1)
                objfrmAddStaffMapping.Description = C1StaffMapping.GetData(C1StaffMapping.RowSel, 2)
                objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
                If oclsgloIntuit.IsPatientPortalEnabled() Then
                    If C1StaffMapping.GetData(C1StaffMapping.RowSel, 2).ToString() = CheckMappingType(C1StaffMapping.GetData(C1StaffMapping.RowSel, 0).ToString(), C1StaffMapping.GetData(C1StaffMapping.RowSel, 2).ToString()) Then
                        objfrmAddStaffMapping.btnSearchUser.Enabled = False
                        objfrmAddStaffMapping.btnClearTestName.Enabled = False
                        objfrmAddStaffMapping.cmb_To.Enabled = False
                    End If
                End If

                objfrmAddStaffMapping.ShowDialog()
                objfrmAddStaffMapping.Dispose()
                objfrmAddStaffMapping = Nothing
                FillC1StaffMapping()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
    End Sub

    Private Function CheckMappingType(sMappingID As String, sDescription As String) As String
        Dim oclsgloIntuit As clsgloIntuit
        Dim dt As DataTable
        Dim sResult As String = ""
        Try
            oclsgloIntuit = New clsgloIntuit
            dt = oclsgloIntuit.GetStaffMapping()

            Dim drSelect() As DataRow = dt.Select("MappingID=" + sMappingID)

            If Not IsNothing(drSelect) Then
                For Each dr As DataRow In drSelect
                    If dr("Description").ToString().Contains("Appointment") Then
                        sResult = dr("Description").ToString()
                    End If
                    If dr("Description").ToString().Contains("Refill Request") Then
                        sResult = dr("Description").ToString()
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
        End Try
        Return sResult
    End Function

    

End Class