Option Compare Binary
Option Explicit On 
Option Strict On

Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
'''   Erweitert den Dialog zur Druckvorschau um einige optische Effekte.
''' </summary>
Public Class ExtendedPrintPreviewDialog
    Inherits System.Windows.Forms.PrintPreviewDialog
    Dim WithEvents toolstripbtn As ToolStripButton
    Public WithEvents dg As DataGrid
    Public height As Double = 0
    Public Event ClickPrint(ByVal sender As Object, ByVal e As System.EventArgs, ByVal Dg As DataGrid, ByVal height As Double)
#Region " Vom Windows Form Designer generierter Code "

    ''' <summary>
    '''   Erstellt eine neue Instanz von <c>ExtendedPrintPreviewDialog</c>.
    ''' </summary>
    Public Sub New()
        MyBase.New()
        InitializeComponent()

        ' Einige Anpassungen vornehmen. Vor Allem der "Schliessen"-Button ist etwas mager
        ' ausgefallen, daher wird er im Systemstyle dargestellt und an den unteren
        ' Rand des Dialogs gerückt.
        With Me
            'If .Controls.Count > 0 Then

            '    Dim b As Button = DirectCast(.Controls(1).Controls(0), Button)
            '    b.Location = New Point(0, 0)
            '    b.FlatStyle = FlatStyle.System
            '    Me.MinimumSize = New Size(Me.MinimumSize.Width - b.Width, Me.MinimumSize.Height)
            '    Dim p As New Panel
            '    b.Size = New Size(80, 24)
            '    p.Size = b.Size
            '    p.Controls.Add(b)
            '    b.Anchor = AnchorStyles.None
            '    p.Height = 40
            '    p.Dock = DockStyle.Bottom
            '    .Controls.Add(p)
            '    With DirectCast(.Controls(1), ToolBar)
            '        .Buttons.RemoveAt(8)
            '        .Divider = False
            '    End With
            '    Dim frm As System.Windows.Forms.Form = DirectCast(Me, Form)
            '    frm.WindowState = FormWindowState.Maximized
            'End If
            Dim t As ToolStrip = DirectCast(.Controls(1), ToolStrip)
            t.Items(0).Visible = False
            toolstripbtn = New ToolStripButton
            toolstripbtn.DisplayStyle = ToolStripItemDisplayStyle.Text
            toolstripbtn.Text = "Print"

            t.Items.Add(toolstripbtn)
            Dim frm As System.Windows.Forms.Form = DirectCast(Me, Form)
            frm.WindowState = FormWindowState.Maximized
        End With
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ExtendedPrintPreviewDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(400, 300)
        Me.Name = "ExtendedPrintPreviewDialog"
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub toolstripbtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles toolstripbtn.Click
        RaiseEvent ClickPrint(sender, e, dg, height)
    End Sub
    Public WriteOnly Property SetDatagrid() As DataGrid
        Set(ByVal value As DataGrid)
            dg = CType(value, DataGrid)
        End Set
    End Property
    Public WriteOnly Property Setheight() As Double
        Set(ByVal value As Double)
            height = value
        End Set
    End Property

End Class