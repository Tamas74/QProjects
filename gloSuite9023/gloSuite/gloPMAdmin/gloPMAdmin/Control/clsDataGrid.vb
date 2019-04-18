Public Class clsDataGrid
    Inherits DataGrid

    Private m_HtiMouseDown As HitTestInfo
    Private m_LastHTI As HitTestInfo
    Private m_ControlCollenction As New ArrayList
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
            m_HtiMouseDown = htInfo
            If m_FullRowSelect Then
                Me.CurrentCell = New DataGridCell(htInfo.Row, htInfo.Column)
                Me.Select(htInfo.Row)
            End If

        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        On Error Resume Next
        Dim ptPoint As Point = New Point(e.X, e.Y)
        MyBase.OnMouseUp(e)
        Dim htInfo As HitTestInfo = Me.HitTest(ptPoint)
        If htInfo.Type = HitTestType.Cell Then
            m_LastHTI = htInfo
            Me.CurrentCell = New DataGridCell(htInfo.Row, htInfo.Column)
            If m_FullRowSelect Then
                Me.UnSelect(m_HtiMouseDown.Row)
                Me.Select(htInfo.Row)
            End If
        End If
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
                        Me.Select(0)
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

    Private Sub InitializeComponent()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'clsDataGrid
        '
        Me.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaptionForeColor = System.Drawing.Color.White
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.GridLineColor = System.Drawing.Color.Black
        Me.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.HeaderForeColor = System.Drawing.Color.White
        Me.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.ParentRowsForeColor = System.Drawing.Color.Black
        Me.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.SelectionForeColor = System.Drawing.Color.Black
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
