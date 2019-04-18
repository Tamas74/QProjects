Public Class clsDataGrid
    Inherits DataGrid

    Private m_HtiMouseDown As HitTestInfo
    Private m_LastHTI As HitTestInfo
    'Private m_ControlCollenction As New ArrayList
    Private m_FullRowSelect As Boolean
    Sub New()
        MyBase.New()
        m_FullRowSelect = True
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        On Error Resume Next
        Me.ResetSelection()
        Dim ptPoint As Point = New Point(e.X, e.Y)
        MyBase.OnMouseDown(e)
        Dim htInfo As HitTestInfo = Me.HitTest(ptPoint)
        If htInfo.Type = HitTestType.Cell Then
            If m_FullRowSelect Then
                Me.UnSelect(m_HtiMouseDown.Row)
                Me.CurrentCell = New DataGridCell(htInfo.Row, htInfo.Column)
                Me.Select(htInfo.Row)
            End If
            m_HtiMouseDown = htInfo
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        On Error Resume Next
        Dim ptPoint As Point = New Point(e.X, e.Y)
        'MyBase.OnMouseUp(e)
        Dim htInfo As HitTestInfo = Me.HitTest(ptPoint)
        If htInfo.Type = HitTestType.Cell Then
            m_LastHTI = htInfo
            Me.CurrentCell = New DataGridCell(htInfo.Row, htInfo.Column)
            If m_FullRowSelect Then
                Me.UnSelect(m_HtiMouseDown.Row)
                Me.Select(htInfo.Row)
            End If
        End If
        MyBase.OnMouseUp(e)
    End Sub

    <System.ComponentModel.Browsable(False)> _
    Public Property FullRowSelect() As Boolean
        Get
            Return m_FullRowSelect
        End Get
        Set(ByVal Value As Boolean)
            m_FullRowSelect = Value
            If Value Then
                If Not (m_LastHTI Is Nothing) Then
                    Me.Select(m_LastHTI.Row)
                Else
                    Try
                        If Not (Me.DataSource Is Nothing) Then
                            Me.Select(0)
                        End If
                    Catch
                    End Try
                End If
            Else
                If Not (m_LastHTI Is Nothing) Then
                    Me.UnSelect(m_LastHTI.Row)
                Else
                    Try
                        Me.UnSelect(0)
                    Catch
                    End Try
                End If
            End If
        End Set
    End Property

    Public Sub ResetSelectedRows()
        Me.ResetSelection()
    End Sub
End Class
