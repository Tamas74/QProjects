Imports System.Reflection

Public Class MultiLineColumn

    Inherits DataGridTextBoxColumn

    Private mTxtAlign As HorizontalAlignment

    Private mDrawTxt As New StringFormat

    Private mbAdjustHeight As Boolean = True

    'Private m_intPreEditHeight As Integer = 0

    'Private m_rownum As Integer

    Dim WithEvents dg As DataGrid

    Private arHeights As ArrayList

    Private Sub GetHeightList()

        Dim mi As MethodInfo = dg.GetType().GetMethod("get_DataGridRows", BindingFlags.FlattenHierarchy Or BindingFlags.IgnoreCase Or BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Static)

        Dim dgra As Array = CType(mi.Invoke(Me.dg, Nothing), Array)

        arHeights = New ArrayList

        Dim dgRowHeight As Object

        For Each dgRowHeight In dgra

            If dgRowHeight.ToString().EndsWith("DataGridRelationshipRow") = True Then

                arHeights.Add(dgRowHeight)

            End If

        Next

    End Sub

    Public Sub New()

        mTxtAlign = HorizontalAlignment.Left

        mDrawTxt.Alignment = StringAlignment.Near

        Me.ReadOnly = True

    End Sub

    Protected Overloads Overrides Sub Edit(ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal bounds As System.Drawing.Rectangle, ByVal [readOnly] As Boolean, ByVal instantText As String, ByVal cellIsVisible As Boolean)
        MyBase.Edit(source, rowNum, bounds, [readOnly], instantText, cellIsVisible)

        Me.TextBox.TextAlign = mTxtAlign

        Me.TextBox.Multiline = mbAdjustHeight

        If rowNum = source.Count - 1 Then

            GetHeightList()

        End If

    End Sub

    Protected Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics, ByVal bounds As System.Drawing.Rectangle, ByVal source As System.Windows.Forms.CurrencyManager, ByVal rowNum As Integer, ByVal backBrush As System.Drawing.Brush, ByVal foreBrush As System.Drawing.Brush, ByVal alignToRight As Boolean)

        Static bPainted As Boolean = False

        If Not bPainted Then

            dg = Me.DataGridTableStyle.DataGrid

            GetHeightList()

        End If

        'clear the cell 

        g.FillRectangle(backBrush, bounds)

        'draw the value 

        Dim s As String = Me.GetColumnValueAtRow([source], rowNum).ToString()

        Dim r As New RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height)

        r.Inflate(0, -1)

        ' get the height column should be 

        Dim sDraw As SizeF = g.MeasureString(s, Me.TextBox.Font, Me.Width, mDrawTxt)

        Dim h As Integer = sDraw.Height + 15

        If mbAdjustHeight Then

            Try

                Dim pi As PropertyInfo = arHeights(rowNum).GetType().GetProperty("Height")

                ' get current height 

                Dim curHeight As Integer = pi.GetValue(arHeights(rowNum), Nothing)

                ' adjust height 

                If h > curHeight Then

                    pi.SetValue(arHeights(rowNum), h, Nothing)

                    Dim sz As Size = dg.Size

                    dg.Size = New Size(sz.Width - 1, sz.Height - 1)

                    dg.Size = sz

                End If

            Catch

                ' something wrong leave default height 

                GetHeightList()

            End Try

        End If

        g.DrawString(s, MyBase.TextBox.Font, foreBrush, r, mDrawTxt)

        bPainted = True

    End Sub

    Public Property DataAlignment() As HorizontalAlignment

        Get

            Return mTxtAlign

        End Get

        Set(ByVal Value As HorizontalAlignment)

            mTxtAlign = Value

            If mTxtAlign = HorizontalAlignment.Center Then

                mDrawTxt.Alignment = StringAlignment.Center

            ElseIf mTxtAlign = HorizontalAlignment.Right Then

                mDrawTxt.Alignment = StringAlignment.Far

            Else

                mDrawTxt.Alignment = StringAlignment.Near

            End If

        End Set

    End Property

    Public Property AutoAdjustHeight() As Boolean

        Get

            Return mbAdjustHeight

        End Get

        Set(ByVal Value As Boolean)

            mbAdjustHeight = Value

            Try

                dg.Invalidate()

            Catch

            End Try

        End Set

    End Property

End Class

