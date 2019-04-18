Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Partial Public Class CheckedListBoxDroDown
    Inherits C1.Win.C1Input.DropDownForm
    Private WithEvents checkedListBox1 As System.Windows.Forms.CheckedListBox
    Private WithEvents Tooltip1 As New ToolTip
    Private _IsDisableControl As Boolean = False
    'Public Property IsDisableControl() As Boolean
    '    Get
    '        Return _IsDisableControl
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _IsDisableControl = value
    '    End Set
    'End Property

    Public Sub New(ByVal items() As String, ByVal isDisable As Boolean)
        InitializeComponent()
        If Not IsNothing(items) Then
            If items.Length > 0 Then
                For Each item As String In items
                    If Not IsNothing(item) Then
                        If Convert.ToString(item) <> "" Then
                            checkedListBox1.Items.Add(item)
                        End If

                    End If

                Next
            End If

        End If
        _IsDisableControl = isDisable
    End Sub

    Private Sub InitializeComponent()
        Me.checkedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'checkedListBox1
        '
        Me.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.checkedListBox1.CheckOnClick = True
        Me.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.checkedListBox1.FormattingEnabled = True
        Me.checkedListBox1.IntegralHeight = False
        Me.checkedListBox1.Location = New System.Drawing.Point(0, 0)
        Me.checkedListBox1.Name = "checkedListBox1"
        Me.checkedListBox1.Size = New System.Drawing.Size(296, 113)
        Me.checkedListBox1.TabIndex = 1
        '
        'CheckedListBoxDroDown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 113)
        Me.Controls.Add(Me.checkedListBox1)
        Me.MinimumSize = New System.Drawing.Size(115, 0)
        Me.Name = "CheckedListBoxDroDown"
        Me.Options = CType(((((C1.Win.C1Input.DropDownFormOptionsFlags.Focusable Or C1.Win.C1Input.DropDownFormOptionsFlags.FixedWidth) _
            Or C1.Win.C1Input.DropDownFormOptionsFlags.FixedHeight) _
            Or C1.Win.C1Input.DropDownFormOptionsFlags.AlwaysPostChanges) _
            Or C1.Win.C1Input.DropDownFormOptionsFlags.AutoResize), C1.Win.C1Input.DropDownFormOptionsFlags)
        Me.ResumeLayout(False)

    End Sub
   
    Private Sub CheckedListBoxDroDown_Open(sender As System.Object, e As System.EventArgs) Handles MyBase.Open
        Try

            If _IsDisableControl = True Then
                checkedListBox1.Enabled = False

            End If
            Dim s As String = OwnerControl.Text
            For i As Integer = 0 To checkedListBox1.Items.Count - 1
                If (s.Contains(checkedListBox1.Items(i).ToString())) Then
                    checkedListBox1.SetItemChecked(i, True)
                Else
                    checkedListBox1.SetItemChecked(i, False)
                End If

            Next
            checkedListBox1.SelectedIndex = -1

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckedListBoxDroDown_PostChanges(sender As Object, e As System.EventArgs) Handles Me.PostChanges
        Dim conc As String = ""
        For Each item As String In checkedListBox1.CheckedItems
            conc = conc & item & "|"

        Next
        If conc.Length > 0 Then
            conc = conc.Remove(conc.LastIndexOf("|"), 1)
            OwnerControl.Value = conc
        Else
            OwnerControl.Value = conc
        End If

    End Sub

   

    Private Sub CheckedListBoxDroDown_VisibleChanged(sender As Object, e As System.EventArgs) Handles Me.VisibleChanged

    End Sub

    Private Sub checkedListBox1_MouseHover(sender As System.Object, e As System.EventArgs) Handles checkedListBox1.MouseHover
        'Try


        'Dim pos As Point = checkedListBox1.PointToClient(MousePosition)

        'Dim index As Integer = checkedListBox1.IndexFromPoint(pos)

        'If index > -1 Then



        '    pos = Me.PointToClient(MousePosition)


        '        Tooltip1.Show(checkedListBox1.Items(index).ToString(), Me, pos, 5000)
        'End If

        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        'End Try
    End Sub

    Private Sub checkedListBox1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles checkedListBox1.MouseMove
      
        Try
            Dim topIndex As Integer = Me.checkedListBox1.TopIndex
            Dim itemHeight As Integer = Me.checkedListBox1.ItemHeight
            Dim index As Integer = (e.Y \ itemHeight) + topIndex
            If (index >= checkedListBox1.Items.Count) Then
                index = checkedListBox1.Items.Count - 1
            End If
            If (index >= 0) Then
                Dim itemText As String = Me.checkedListBox1.Items(index)
                If Me.Tooltip1.GetToolTip(Me.checkedListBox1) <> itemText Then
                    Me.Tooltip1.SetToolTip(Me.checkedListBox1, itemText)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        
    End Sub
End Class
