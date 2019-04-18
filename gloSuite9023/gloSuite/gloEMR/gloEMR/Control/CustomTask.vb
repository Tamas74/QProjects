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
    Public colIndex As Integer
    Public Event AfterSelChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

    Public Property SearchText() As String
        Get
            Return txtsearch.Text
        End Get
        Set(ByVal Value As String)
            txtsearch.Text = Value
        End Set
    End Property

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        RaiseEvent TextKeyPress(sender, e)
    End Sub

    Public Sub txtsearch_Textchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        RaiseEvent SearchChanged(sender, e)
    End Sub

    Public Sub OKCustomTask()
        Dim sender As Object = Nothing
        Dim e As EventArgs = Nothing
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub CloseCustomTask()
        Dim sender As Object = Nothing
        Dim e As EventArgs = Nothing
        RaiseEvent CloseClick(sender, e)

    End Sub

    Public Sub Selectsearch(ByVal enm As enmcontrol)

        Select Case enm
            Case enmcontrol.Add
                Dim objSender As Object = Nothing
                Dim obje As System.EventArgs = Nothing
                tsbtn_New_Click(objSender, obje)
            Case enmcontrol.Close
                Dim objSender As Object = Nothing
                Dim obje As System.EventArgs = Nothing
                tsbtn_Cancel_Click(objSender, obje)
            Case enmcontrol.Search
                txtsearch.Focus()
            Case enmcontrol.OK
                Dim objSender As Object = Nothing
                Dim obje As System.EventArgs = Nothing
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
        'Resolved bug: 42404
        If C1Task.Cols.Count > 2 Then
            If C1Task.Cols(2).Caption = "sCategoryType" Then
                C1Task.Cols(2).Visible = False
            End If
        End If
      
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

    Private Sub C1Task_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1Task.AfterSelChange
        RaiseEvent AfterSelChanged(sender, e)

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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tsbtn_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_OK.Click
        Try
            OKCustomTask()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tsbtn_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_Cancel.Click
        Try
            CloseCustomTask()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtsearch.Text = String.Empty
    End Sub
End Class
