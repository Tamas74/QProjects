Imports gloEMRGeneralLibrary
Public Class gloSearchC1FlexgridUserCtrl


    Public Event gloC1FlexgridUserCtrlMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event _FlexAfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexCellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event _FlexClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event _FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event _FlexMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event txtSearchFlexGridTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event btnCloseRefillClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event cntClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
    Private _RowIndex As Int32
    Private _RowsCount As Int32

    Public Property RowsCol() As Int32
        Get
            Return _Flex.Rows.Count
        End Get
        Set(ByVal value As Int32)
            _Flex.Rows.Count = value
        End Set
    End Property
    Public Property RowIndex() As Int32
        Get
            Return _RowIndex
        End Get
        Set(ByVal value As Int32)
            _RowIndex = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetGridStyle()
        setlblTxtStyles()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub setlblTxtStyles()
        With txtSearchFlexGrid
            .BorderStyle = Windows.Forms.BorderStyle.Fixed3D
            .ForeColor = Drawing.SystemColors.WindowText
            .Width = 200
            .Height = 21

        End With

        With lblSearchOn
            .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            .Height = 82
            .Width = 15
            '.BackColor = Color.FromArgb(31, 73, 125)
        End With
        'With lblColName
        '    .Font = New Font("Tahoma", 9, FontStyle.Bold)
        '    .Height = 82
        '    .Width = 15
        '    '.BackColor = Color.FromArgb(207, 227, 254)
        'End With
        With lblSearchString
            .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            .Height = 82
            .Width = 15
            '.BackColor = Color.FromArgb(31, 73, 125)
        End With

    End Sub

    Private Sub SetGridStyle()
        With _Flex
            .Redraw = False
            .Cols.Fixed = 0
            .Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.FromArgb(240, 247, 255)


            .Styles.Fixed.BackColor = Color.FromArgb(86, 126, 211)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

            .Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Alternate.Border.Color = Color.FromArgb(159, 181, 211)

            .Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
            .Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Normal.Border.Color = Color.FromArgb(159, 181, 211)

            .Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.Focus.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Focus.ForeColor = Color.Black
            .Redraw = True
        End With
    End Sub

    Protected Sub _Flex_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterEdit
        Try
            RaiseEvent _FlexAfterEdit(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _Flex_CellButtonClick(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellButtonClick
        Try
            RaiseEvent _FlexCellButtonClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _Flex_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Try
                '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                '        _Flex.ContextMenuStrip.Dispose()
                '        _Flex.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                _Flex.ContextMenuStrip = cntListmenuStrip
                'Dim trvNode As myTreeNode
                'trvNode = CType(trPrescriptionDetails.GetNodeAt(e.X, e.Y), myTreeNode)
                ' If IsNothing(trvNode) = False Then
                With _Flex
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    .Select(r, True)
                    'trPrescriptionDetails.SelectedNode = trvNode
                    If r > 0 Then
                        'Try
                        '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                        '        _Flex.ContextMenuStrip.Dispose()
                        '        _Flex.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        _Flex.ContextMenuStrip = cntListmenuStrip

                    Else
                        'Try
                        '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                        '        _Flex.ContextMenuStrip.Dispose()
                        '        _Flex.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        _Flex.ContextMenuStrip = Nothing
                    End If

                End With
            End If
            RaiseEvent _FlexMouseDown(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub _Flex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.Click
        Try
            RaiseEvent _FlexClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _Flex_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _Flex.DoubleClick
        Try
            RaiseEvent _FlexDoubleClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _Flex_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseUp
        Try
            RaiseEvent _FlexMouseUp(sender, e)
        Catch ex As Exception

        End Try

    End Sub


    Private Sub txtSearchFlexGrid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFlexGrid.TextChanged
        Try
            RaiseEvent txtSearchFlexGridTextChanged(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCloseRefill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseRefill.Click
        Try
            RaiseEvent btnCloseRefillClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub
    'C

    Private Sub cntListmenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cntListmenuStrip.ItemClicked
        RaiseEvent cntClick(sender, e)
    End Sub
  
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearchFlexGrid.ResetText()
        txtSearchFlexGrid.Focus()
    End Sub
End Class
