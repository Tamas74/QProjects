Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class CustomTask
    Inherits System.Windows.Forms.UserControl
    'Private col_select As Boolean = 0
    'Private col_nuserid As Integer = 1
    'Private col_sloginname As Integer = 2
    'Private col_column1 As Integer = 3
    'Private col_cloumn2 As Integer = 5
    Public IsICD9Checked As Boolean = False ''added to check Whether ICD9 or 10 is Checked
    Public colIndex As Integer
    Public _GridDatasource As DataView
    Public Event Gridkeypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    Enum enmcontrol
        'Search
        'OK
        'Close
        'Grid
        'Add
        Add
        Close
        Search
        OK
        C1Task


    End Enum
    '''''''''''code added by pradeep on 12/06/2010
    'Public Property setText() As String
    '    Get
    '        Return
    '    End Get
    '    Set(ByVal value As String)

    '    End Set
    'End Property
    Public WriteOnly Property SetDeSelectAllVisible() As Boolean
        Set(ByVal value As Boolean)
            tsbtn_DeSelectAll.Visible = value
        End Set
    End Property
    Public WriteOnly Property SetSelectAllVisible() As Boolean
        Set(ByVal value As Boolean)
            tsbtn_SelectAll.Visible = value
        End Set
    End Property
    Public ReadOnly Property IsCheckrbtnBeersList() As Boolean
        Get
            Return rbtnBeersList.Checked
        End Get
    End Property
    'Public ReadOnly Property IsChecked() As Boolean
    '    Get
    '        Return chkBeersList.CheckState
    '    End Get
    'End Property
    Public WriteOnly Property setPnlVisible() As Boolean
        Set(ByVal value As Boolean)
            PnlBeersList.Visible = value
        End Set
    End Property
    Public Property enablerbtnBeersList() As Boolean
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
            rbtnBeersList.Enabled = value
        End Set
    End Property
    Public Property enablerbtnAllDrugs() As Boolean
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
            rbtnAllDrugs.Enabled = value
        End Set
    End Property
    Public WriteOnly Property setPnlICDVisible() As Boolean  ''added for ICD10 implementation

        Set(ByVal value As Boolean)
            PnlICD.Visible = value
        End Set
    End Property
    Public Property rbICD10Transition() As Boolean
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
            If (value = True) Then
                rbICD10.Checked = True
            Else
                rbICD9.Checked = True
            End If
        End Set
    End Property

    Public Sub setPnlSize(ByVal value1 As Integer, ByVal value2 As Integer)
        Panel2.Height = value1
        Panel2.Width = value2
    End Sub
    ''' 'end ''''''''''''''''''''''''''''''''''''''''''''''''''

    Public Property SearchText() As String
        Get
            Return txtsearch.Text
        End Get
        Set(ByVal Value As String)
            txtsearch.Text = Value
        End Set
    End Property
    Private _SetSearchTextWidth As Int64
    Public WriteOnly Property SetSearchTextWidth() As Int64
        Set(ByVal _SetSearchTextWidth As Int64)
            txtsearch.Width = _SetSearchTextWidth
        End Set
    End Property


    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        RaiseEvent TextKeyPress(sender, e)
    End Sub

    Public Sub txtsearch_Textchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        _GridDatasource = C1Task.DataSource
        RaiseEvent SearchChanged(sender, e)
    End Sub

    Public Sub OKCustomTask(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub CloseCustomTask(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent CloseClick(sender, e)
    End Sub
    Public Property GridDatasource() As DataView
        Get
            Return _GridDatasource
        End Get
        Set(ByVal value As DataView)
            value = _GridDatasource
        End Set
    End Property
    Public Sub Selectsearch(ByVal enm As enmcontrol)
        Dim objSender As Object = Nothing
        Dim obje As System.EventArgs = Nothing
        Select Case enm
            Case enmcontrol.Add
                tsbtn_New_Click(objSender, obje)
            Case enmcontrol.Close
                tsbtn_Cancel_Click(objSender, obje)
            Case enmcontrol.Search
                txtsearch.Focus()
            Case enmcontrol.OK
                tsbtn_OK_Click(objSender, obje)
            Case enmcontrol.C1Task
                C1Task.Select()
        End Select
    End Sub

    Private Sub AddCustomTask()
        Dim sender As Object = Nothing
        Dim e As EventArgs = Nothing
        RaiseEvent AddClick(sender, e)

    End Sub

    Public WriteOnly Property SetVisible() As Boolean
        Set(ByVal value As Boolean)
            tsbtn_New.Visible = value
        End Set
    End Property
    Public Sub datasource(ByVal dv As DataView)

        C1Task.DataSource = dv

    End Sub

    Public ReadOnly Property CurrentID() As Object
        Get
            Return C1Task.Item(C1Task.Row, 0)
        End Get

    End Property

    Public ReadOnly Property Gridwidth() As Integer
        Get
            Return C1Task.Width
        End Get
    End Property
    Public ReadOnly Property GetIsSelected(ByVal i As Integer) As Boolean
        Get
            If C1Task.Row = i Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    '' Added By Ujwala Atre
    Public ReadOnly Property GetIsChecked(ByVal i As Integer, ByVal j As Integer) As Boolean
        Get
            If (C1Task.GetCellCheck(i, j) = CheckEnum.Checked) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Function setChecked() As Boolean
        Dim i As Integer
        For i = 1 To C1Task.Rows.Count
            C1Task.SetCellCheck(i, 0, CheckEnum.Checked)
        Next
        Return C1Task.Rows.Count > 0
    End Function

    Public Property C1Grid() As C1.Win.C1FlexGrid.C1FlexGrid
        Get
            Return C1Task
        End Get
        Set(ByVal value As C1.Win.C1FlexGrid.C1FlexGrid)
            value = C1Task
        End Set
    End Property

    Public ReadOnly Property GetTotalRows() As Integer

        Get
            Return C1Task.Rows.Count
        End Get

    End Property
    '' Added By Ujwala Atre

    Public Sub SetTableStyleCol(ByVal ts As clsDataGridTableStyle)

        C1Task.Styles.Clear()
        'gloC1FlexStyle.Style(C1Task)
        'flex.Styles.Add( ts)

    End Sub

    Public Property GetItem(ByVal i As Integer, ByVal j As Integer) As Object
        Get
            Return C1Task.Item(i, j)
        End Get
        Set(ByVal value As Object)
            C1Task.Item(i, j) = value
        End Set
    End Property
    Public ReadOnly Property GetCurrentrowIndex() As Integer

        Get
            Return C1Task.RowSel
        End Get

    End Property
    Public Function GetSelect(ByVal i As Integer) As Integer
        Return C1Task.Row
    End Function

    Public Sub GridDesign(ByVal Gridcontrol As C1.Win.C1FlexGrid.C1FlexGrid)

    End Sub

    Private Sub C1Task_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Task.Click
        gloC1FlexStyle.Style(C1Task)
        Dim i As Integer
        i = C1Task.Col

        Dim temp As Integer = 0
        temp = C1Task.HitTest.Row

        If temp > 0 Then
            If i > 0 Then
                If C1Task.GetData(0, i) <> "Select" Then
                    'Label1.Text = ""
                    'Label1.Text = "Search On " & CType((C1Task.GetData(0, i)), String)
                    colIndex = i
                    'ObjTasksDBLayer.DsDataview
                Else
                    Dim selected_Items = C1Task.GetData(0, i)
                    colIndex = i
                End If
            Else
                Exit Sub
            End If
        End If
        '    Label1.Text = lblColName.Text
    End Sub

    'Private Sub ts_LM_Orders_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_LM_Orders.ItemClicked
    '    Try
    '        Select Case e.ClickedItem.Tag
    '            Case "New"
    '                AddCustomTask()

    '            Case "OK"
    '                OKCustomTask()

    '            Case "Cancel"
    '                CloseCustomTask()


    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub tsbtn_New_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_New.Click
        Try
            AddCustomTask()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tsbtn_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_OK.Click
        Try
            OKCustomTask(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tsbtn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_Cancel.Click
        Try
            CloseCustomTask(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Task_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles C1Task.DragEnter

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        gloC1FlexStyle.Style(C1Task)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub CustomTask_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        gloC1FlexStyle.Style(C1Task)
       
    End Sub

    'Private Sub chkBeersList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If chkBeersList.CheckState = CheckState.Checked Then
    '        chkBeersList.Text = "All Drugs"
    '    Else
    '        chkBeersList.Text = "Beers List"
    '    End If
    '    RaiseEvent chkBeersListClick(sender, e)
    'End Sub

    Private Sub rbtnBeersList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnBeersList.CheckedChanged
        RaiseEvent rbtnBeersListClick(sender, e)
    End Sub

    Private Sub rbtnAllDrugs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAllDrugs.CheckedChanged
        RaiseEvent rbtnAllDrugsClick(sender, e)
    End Sub


    Private Sub C1Task_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Task.MouseDoubleClick
        RaiseEvent MouseDubClick(sender, e)
    End Sub
    Private Sub C1Task_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Task.MouseMove
        RaiseEvent MouseMoveClick(sender, e)
    End Sub

    Private Sub tsbtn_SelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtn_SelectAll.Click
        RaiseEvent SelectAllClick(sender, e)
    End Sub

    Private Sub tsbtn_DeSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtn_DeSelectAll.Click
        RaiseEvent DeSelectAllClick(sender, e)
    End Sub

    Private Sub C1Task_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Task.DoubleClick
        RaiseEvent GridDoubleClick(sender, e)
    End Sub

    Private Sub C1Task_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles C1Task.KeyPress
        RaiseEvent Gridkeypress(sender, e)
    End Sub

    Private Sub rbICD9_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbICD9.CheckedChanged
        If rbICD9.Checked = True Then
            IsICD9Checked = True
            RaiseEvent rbtnICD9Click(sender, e)
        End If
    End Sub

    Private Sub rbICD10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbICD10.CheckedChanged  ''added for ICD10 implementation
        If rbICD10.Checked = True Then
            IsICD9Checked = False
            RaiseEvent rbtnICD10Click(sender, e)
        End If
    End Sub
End Class
