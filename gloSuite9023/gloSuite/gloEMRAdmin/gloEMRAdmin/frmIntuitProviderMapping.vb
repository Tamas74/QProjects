Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class frmIntuitProviderMapping

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim objfrmAddStaffMapping As New frmAddProviderMapping
        objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
        objfrmAddStaffMapping.ShowDialog()
        objfrmAddStaffMapping.Dispose()
        objfrmAddStaffMapping = Nothing
        FillProviderMapping()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim oclsgloIntuit As New clsgloIntuit
        Try
            If C1ProviderMapping.RowSel >= 1 Then
                If MessageBox.Show("Are you sure you want to delete this Provider?", "gloEMR Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    oclsgloIntuit = New clsgloIntuit

                    Dim sResult As String = oclsgloIntuit.IsLocation_ProviderInMessageMapping(C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 1), "provider")
                    If sResult <> "" Then
                        MessageBox.Show(sResult, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Else
                        oclsgloIntuit.DeleteProviderMapping(C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 0))
                        FillProviderMapping()
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
    Private Sub FillProviderMapping()
        Dim oclsgloIntuit As New clsgloIntuit
        Dim dt As DataTable
        Try
            oclsgloIntuit = New clsgloIntuit
            dt = oclsgloIntuit.GetProviderMapping
            If Not IsNothing(dt) Then
                C1ProviderMapping.DataSource = dt.DefaultView
                If C1ProviderMapping.Rows.Count > 1 Then
                    C1ProviderMapping.Select(1, 1)
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
        C1ProviderMapping.Cols(0).Width = 0
        C1ProviderMapping.Cols(1).Width = 0
        C1ProviderMapping.Cols(2).Width = C1ProviderMapping.Width * 0.6
        C1ProviderMapping.Cols(3).Width = C1ProviderMapping.Width * 0.4
        'visible
        C1ProviderMapping.Cols(2).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1ProviderMapping.Cols(3).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        C1ProviderMapping.Cols(0).Visible = False
        C1ProviderMapping.Cols(1).Visible = False
        C1ProviderMapping.Cols(2).Visible = True
        C1ProviderMapping.Cols(3).Visible = True
        C1ProviderMapping.AllowEditing = False

    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmIntuitProviderMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillProviderMapping()
    End Sub
    Private Sub btn_Modify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Modify.Click
        If C1ProviderMapping.RowSel >= 1 Then
            Dim objfrmAddStaffMapping As New frmAddProviderMapping
            objfrmAddStaffMapping.MappingID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 0)
            objfrmAddStaffMapping.ProviderID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 1)
            objfrmAddStaffMapping.IntuitProviderID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 3)
            objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
            objfrmAddStaffMapping.ShowDialog()
            objfrmAddStaffMapping.Dispose()
            objfrmAddStaffMapping = Nothing
            FillProviderMapping()
        End If
    End Sub

    Private Sub C1StaffMapping_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1ProviderMapping.DoubleClick
        If C1ProviderMapping.RowSel >= 1 Then
            Dim objfrmAddStaffMapping As New frmAddProviderMapping
            objfrmAddStaffMapping.MappingID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 0)
            objfrmAddStaffMapping.ProviderID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 1)
            objfrmAddStaffMapping.IntuitProviderID = C1ProviderMapping.GetData(C1ProviderMapping.RowSel, 3)
            objfrmAddStaffMapping.StartPosition = FormStartPosition.CenterScreen
            objfrmAddStaffMapping.ShowDialog()
            objfrmAddStaffMapping.Dispose()
            objfrmAddStaffMapping = Nothing
            FillProviderMapping()
        End If
    End Sub
End Class