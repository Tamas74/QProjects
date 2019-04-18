Public Class gloC1FlexgridUserCtrl

    '.Styles.Alternate.BackColor = Color.LightBlue
    '.Styles.Alternate.ForeColor = Color.Black
    '.Styles.Highlight.BackColor = Color.CadetBlue
    '.Styles.Highlight.ForeColor = Color.Black

    Public Event gloC1FlexgridUserCtrlMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    'Public Event gloC1FlexgridMouseDown(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexAfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexCellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    Public Event _FlexMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event _FlexClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event _FlexDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event _FlexMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) ''added new event to fix bu 4626
    Public Event _FlexMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event OnAfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs)
    Public Event _FlexSelChange(ByVal sender As System.Object, ByVal e As EventArgs)
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
    Public ReadOnly Property SelectedRowIndex() As Int32
        Get
            Return _Flex.RowSel
        End Get
    End Property
    Private Sub SetGridStyle()
        With _Flex
            .Cols.Fixed = 0
            .Font = gloGlobal.clsgloFont.gFontVerdana_Regular 'New Font("Verdana", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.White

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
            '.Styles.Fixed.BackColor = Color.FromArgb(51, 125, 207)
            .Styles.Fixed.BackColor = Color.FromArgb(4, 96, 162)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFontVerdana_Bold 'New Font("Verdana", 9, FontStyle.Bold)

            '.Styles.Alternate.BackColor = Color.FromArgb(234, 242, 252) '' Color.LightBlue
            .Styles.Alternate.BackColor = Color.FromArgb(214, 235, 248) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.Black
            .Styles.Alternate.Border.Color = Drawing.SystemColors.Control

            .Styles.Normal.BackColor = Color.GhostWhite
            .Styles.Normal.ForeColor = Color.Black
            .Styles.Normal.Border.Color = Drawing.SystemColors.Control

            '.Styles.Highlight.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Highlight.BackColor = Color.FromArgb(255, 197, 108)
            .Styles.Highlight.ForeColor = Color.Black

            '.Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.ForeColor = Color.Black
            .BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
            Dim i As Int32
            For i = 0 To .Styles.Count - 1
                Dim objcell As C1.Win.C1FlexGrid.CellStyle
                objcell = _Flex.Styles.Item(i)
                objcell.Border.Color = Color.Black
            Next

        End With
    End Sub
    Protected Sub _Flex_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.AfterEdit
        RaiseEvent _FlexAfterEdit(sender, e)
    End Sub

    Private Sub _Flex_AfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles _Flex.AfterSort
        RaiseEvent OnAfterSort(sender, e)
    End Sub
    Protected Sub _Flex_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles _Flex.CellButtonClick
        RaiseEvent _FlexCellButtonClick(sender, e)
    End Sub


    Protected Sub _Flex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.Click
        RaiseEvent _FlexClick(sender, e)
    End Sub

    ''added new event to fix bu 4626
    Protected Sub _Flex_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Flex.DoubleClick
        RaiseEvent _FlexDoubleClick(sender, e)
    End Sub

 

    Protected Sub _Flex_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
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
                With _Flex
                    Dim r As Integer = _Flex.HitTest(e.X, e.Y).Row
                    .Select(r, True)
                    If r > 0 Then
                        'Try
                        '    If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                        '        _Flex.ContextMenuStrip.Dispose()
                        '        _Flex.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        _Flex.ContextMenuStrip = cntListmenuStrip
                        RowIndex = r
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
            Else
                RowIndex = _Flex.Row
            End If

            RaiseEvent _FlexMouseDown(sender, e)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub _Flex_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseUp
        RaiseEvent _FlexMouseUp(sender, e)
    End Sub
    Private Sub _Flex_SelChange(sender As System.Object, e As System.EventArgs) Handles _Flex.SelChange
        RaiseEvent _FlexSelChange(sender, e)
    End Sub
    Private Sub _Flex_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDoubleClick
        RaiseEvent _FlexMouseDoubleClick(sender, e)
    End Sub

    Public Sub New()
        MyBase.New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetGridStyle()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
