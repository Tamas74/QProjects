Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class frmIntuitLocationMapping

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim objfrmAddStaffMapping As New frmAddLocationMapping
        objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
        objfrmAddStaffMapping.ShowDialog()
        objfrmAddStaffMapping.Dispose()
        objfrmAddStaffMapping = Nothing
        FillLocationMapping()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim oclsgloIntuit As New clsgloIntuit
        Try
            If C1LocationMapping.RowSel >= 1 Then
                If MessageBox.Show("Are you sure you want to delete this Location?", "gloEMR Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    oclsgloIntuit = New clsgloIntuit
                    Dim sResult As String = oclsgloIntuit.IsLocation_ProviderInMessageMapping(C1LocationMapping.GetData(C1LocationMapping.RowSel, 1), "location")
                    If sResult <> "" Then
                        MessageBox.Show(sResult, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        oclsgloIntuit.DeleteLocationMapping(C1LocationMapping.GetData(C1LocationMapping.RowSel, 0))
                        FillLocationMapping()
                    End If
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
    Private Sub FillLocationMapping()
        Dim oclsgloIntuit As New clsgloIntuit
        Dim dt As DataTable
        Try
            oclsgloIntuit = New clsgloIntuit
            dt = oclsgloIntuit.GetLocationMapping
            If Not IsNothing(dt) Then
                C1LocationMapping.DataSource = dt.DefaultView
                If C1LocationMapping.Rows.Count > 1 Then
                    C1LocationMapping.Select(1, 1)
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
        C1LocationMapping.Cols(0).Width = 0
        C1LocationMapping.Cols(1).Width = 0
        C1LocationMapping.Cols(2).Width = C1LocationMapping.Width * 0.6
        C1LocationMapping.Cols(3).Width = C1LocationMapping.Width * 0.4
        'visible
        C1LocationMapping.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1LocationMapping.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1LocationMapping.Cols(0).Visible = False
        C1LocationMapping.Cols(1).Visible = False
        C1LocationMapping.Cols(2).Visible = True
        C1LocationMapping.Cols(3).Visible = True
        C1LocationMapping.AllowEditing = False

    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmIntuitProviderMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillLocationMapping()
    End Sub
    Private Sub btn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Modify.Click
        If C1LocationMapping.RowSel >= 1 Then
            Dim objfrmAddStaffMapping As New frmAddLocationMapping
            objfrmAddStaffMapping.MappingID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 0)
            objfrmAddStaffMapping.LocationID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 1)
            objfrmAddStaffMapping.IntuitLocationID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 3)
            objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
            objfrmAddStaffMapping.ShowDialog()
            objfrmAddStaffMapping.Dispose()
            objfrmAddStaffMapping = Nothing
            FillLocationMapping()
        End If
    End Sub

    Private Sub C1StaffMapping_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1LocationMapping.DoubleClick
        If C1LocationMapping.RowSel >= 1 Then
            Dim objfrmAddStaffMapping As New frmAddLocationMapping
            objfrmAddStaffMapping.MappingID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 0)
            objfrmAddStaffMapping.LocationID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 1)
            objfrmAddStaffMapping.IntuitLocationID = C1LocationMapping.GetData(C1LocationMapping.RowSel, 3)
            objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
            objfrmAddStaffMapping.ShowDialog()
            objfrmAddStaffMapping.Dispose()
            objfrmAddStaffMapping = Nothing
            FillLocationMapping()
        End If
    End Sub
End Class