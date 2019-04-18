Public Class FrmReconsile_History
    Dim Col_Listrec As Integer = 0
    Dim Col_Rec As Integer = 1
    Dim Col_RecDate As Integer = 2
    Dim Col_Count As Integer = 3
    Dim dt As DataTable = Nothing
    Dim objclsrechist As New ClsReconsileHistory
    Dim _PatientID As Int64 = 0
    Dim _RecType As Integer = 0
    Public Property PatientID As Int64
        Get
            Return _PatientID
        End Get
        Set(value As Int64)
            _PatientID = value
        End Set
    End Property

    Public Property RecType As Integer
        Get
            Return _RecType
        End Get
        Set(value As Integer)
            _RecType = value
        End Set
    End Property
    Private Sub SetGridStyle()

        Try

            dt = objclsrechist.getPatientRecHistory(_PatientID, _RecType)
            If (IsNothing(dt)) Then
                Exit Sub
            End If
            c1RecHistory.DataSource = dt
            With c1RecHistory
                .AllowSorting = True


                .Redraw = False

                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width - 20


                c1RecHistory.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing


                .Cols.Count = Col_Count
                .Rows.Fixed = 1


                '  .Styles.ClearUnused()
                c1RecHistory.Width = _TotalWidth
                .Dock = DockStyle.Fill
                .AllowResizing = True

                .Cols(Col_Listrec).Width = _TotalWidth * 0.33
                .Cols(Col_Listrec).AllowEditing = False
                .Cols(Col_Listrec).Visible = True
                .Cols(Col_Listrec).Caption = "List Provided"
                .Cols(Col_Listrec).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_Rec).Width = _TotalWidth * 0.33
                .Cols(Col_Rec).AllowEditing = False
                .Cols(Col_Rec).Visible = True
                .Cols(Col_Rec).Caption = "Reconciliation Performed"
                .Cols(Col_Rec).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_RecDate).Width = _TotalWidth * 0.33
                .Cols(Col_RecDate).AllowEditing = False
                .Cols(Col_RecDate).Visible = True
                .Cols(Col_RecDate).Caption = "Reconciliation Date"
                .Cols(Col_RecDate).DataType = GetType(System.DateTime)
                .Cols(Col_RecDate).Format = "MM/dd/yyyy"
                .Cols(Col_RecDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter





                .Redraw = True


            End With
            c1RecHistory.ExtendLastCol = True

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub

    Private Sub FrmReconsile_History_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetGridStyle()
    End Sub

    Private Sub tlsbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
    End Sub
End Class