Public Class CustomDataGrid
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        dgGrid.AllowSorting = True
        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Try

                    If (IsNothing(dgGrid) = False) Then
                        dgGrid.TableStyles.Clear()
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dgGrid)
                        dgGrid.Dispose()
                        dgGrid = Nothing
                    End If
                Catch ex As Exception

                End Try
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents dgGrid As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Public Event SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event OKClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event AddClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Dblclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event CellChanged(ByVal sender, ByVal e)
    Public Event ClickRow(ByVal sender, ByVal e)
    Public Event MouseUpClick(ByVal sender, ByVal e)
    Public Event TextKeyPress(ByVal sender, ByVal e)
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomDataGrid))
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.dgGrid = New System.Windows.Forms.DataGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtsearch = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(404, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 25)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(468, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 25)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Cancel"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(340, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(64, 25)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'dgGrid
        '
        Me.dgGrid.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgGrid.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgGrid.CaptionForeColor = System.Drawing.Color.White
        Me.dgGrid.CaptionVisible = False
        Me.dgGrid.DataMember = ""
        Me.dgGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgGrid.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgGrid.GridLineColor = System.Drawing.Color.Black
        Me.dgGrid.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgGrid.HeaderForeColor = System.Drawing.Color.White
        Me.dgGrid.Location = New System.Drawing.Point(0, 27)
        Me.dgGrid.Name = "dgGrid"
        Me.dgGrid.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgGrid.ReadOnly = True
        Me.dgGrid.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgGrid.Size = New System.Drawing.Size(534, 282)
        Me.dgGrid.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search By"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsearch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(76, 3)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(168, 22)
        Me.txtsearch.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 309)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(534, 27)
        Me.Panel1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtsearch)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(534, 27)
        Me.Panel2.TabIndex = 4
        '
        'CustomDataGrid
        '
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.dgGrid)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CustomDataGrid"
        Me.Size = New System.Drawing.Size(534, 336)
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Event GridKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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
        Grid
    End Enum
    Public Sub SetDatasource(ByVal dv As DataView)
        dgGrid.DataSource = dv
    End Sub
    Public Property SearchText() As String
        Get
            Return txtsearch.Text
        End Get
        Set(ByVal Value As String)
            txtsearch.Text = Value
        End Set
    End Property
    Public ReadOnly Property CurrentID() As Object
        Get
            Return dgGrid.Item(dgGrid.CurrentRowIndex, 0)
        End Get
    End Property
    Public ReadOnly Property GridWith() As Integer
        Get
            Return dgGrid.Width
        End Get
    End Property
    Public ReadOnly Property CurrentName() As Object
        Get
            Return dgGrid.Item(dgGrid.CurrentRowIndex, 1)
        End Get
    End Property
    Public ReadOnly Property GetIsSelected(ByVal i As Integer) As Boolean
        Get
            Return dgGrid.IsSelected(i)
        End Get
    End Property
    Public ReadOnly Property GetCurrentrowIndex() As Integer
        Get
            Return dgGrid.CurrentRowIndex
        End Get
    End Property
    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        RaiseEvent SearchChanged(sender, e)
    End Sub
    Public Sub SetTableStyleCol(ByVal ts As DataGridTableStyle)
        dgGrid.TableStyles.Clear()
        dgGrid.TableStyles.Add(ts)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent CloseClick(sender, e)
    End Sub

    Private Sub dgGrid_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgGrid.DoubleClick
        RaiseEvent Dblclick(sender, e)
    End Sub

    Private Sub dgGrid_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgGrid.CurrentCellChanged
        RaiseEvent CellChanged(sender, e)
    End Sub
    Public Property GetItem(ByVal i As Integer, ByVal j As Integer) As Object
        Get
            Return dgGrid.Item(i, j)
        End Get
        Set(ByVal Value As Object)
            dgGrid.Item(i, j) = Value
        End Set
    End Property
    Public Function GetVisiblerowcount() As Integer
        Return dgGrid.VisibleRowCount
    End Function
    Public Sub Selectsearch(ByVal enm As enmcontrol)
        Dim objSender As Object = Nothing
        Dim obje As EventArgs = Nothing
        Select Case enm
            Case enmcontrol.Add
                btnAdd_Click(objSender, obje)
            Case enmcontrol.Close
                btnClose_Click(objSender, obje)
            Case enmcontrol.Search
                txtsearch.Focus()
            Case enmcontrol.OK
                btnOK_Click(objSender, obje)
            Case enmcontrol.Grid
                dgGrid.Select()
        End Select
    End Sub
    Public WriteOnly Property SetVisible() As Boolean
        Set(ByVal Value As Boolean)
            btnAdd.Visible = Value
        End Set
    End Property

    Private Sub CustomDataGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        RaiseEvent ClickRow(sender, e)
    End Sub
    Private Sub dgGrid_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgGrid.MouseUp
        RaiseEvent MouseUpClick(sender, e)
    End Sub
    Public Sub GetSelect(ByVal i As Integer)
        dgGrid.Select(i)
    End Sub
    Public Sub UnselectGridRow(ByVal rowindex As Int32)
        dgGrid.UnSelect(rowindex)
    End Sub
    Private Sub dgGrid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgGrid.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            RaiseEvent GridKeyPress(sender, e)
        End If
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        RaiseEvent AddClick(sender, e)
    End Sub

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        RaiseEvent TextKeyPress(sender, e)
    End Sub
End Class
