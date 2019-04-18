
Imports System.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Diagnostics



Public Class Cls_TabIndexSettings

#Region " Class For Comparing two controls in the selected tab scheme. "

    Private Class TabSchemeComparer
        Implements IComparer
        Private comparisonScheme As TabScheme

#Region "IComparer Members"

        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim control1 As Control = TryCast(x, Control)
            Dim control2 As Control = TryCast(y, Control)

            If control1 Is Nothing OrElse control2 Is Nothing Then
                Debug.Assert(False, "Attempting to compare a non-control")
                Return 0
            End If

            If comparisonScheme = TabScheme.None Then
                Return 0
            End If

            If comparisonScheme = TabScheme.AcrossFirst Then
                ' The primary direction to sort is the y direction (using the Top property).
                ' If two controls have the same y coordination, then we sort them by their x's.
                If control1.Top < control2.Top Then
                    Return -1
                ElseIf control1.Top > control2.Top Then
                    Return 1
                Else
                    Return (control1.Left.CompareTo(control2.Left))
                End If
            Else
                ' comparisonScheme = TabScheme.DownFirst
                ' The primary direction to sort is the x direction (using the Left property).
                ' If two controls have the same x coordination, then we sort them by their y's.
                If control1.Left < control2.Left Then
                    Return -1
                ElseIf control1.Left > control2.Left Then
                    Return 1
                Else
                    Return (control1.Top.CompareTo(control2.Top))
                End If
            End If
        End Function

#End Region


        Public Sub New(scheme As TabScheme)
            comparisonScheme = scheme
        End Sub
    End Class

#End Region

#Region " Variable and Enum "

    Private container As Control
    Private schemeOverrides As Hashtable
    Private curTabIndex As Integer = 0

    Public Enum TabScheme
        None
        AcrossFirst
        DownFirst
    End Enum

#End Region

#Region " Public and Private Methods "

    Public Sub New(container As Control)
        Me.container = container
        Me.curTabIndex = 0
        Me.schemeOverrides = New Hashtable()
    End Sub

    Private Sub New(container As Control, curTabIndex As Integer, schemeOverrides As Hashtable)
        Me.container = container
        Me.curTabIndex = curTabIndex
        Me.schemeOverrides = schemeOverrides
    End Sub

    Public Sub SetSchemeForControl(c As Control, scheme As TabScheme)
        schemeOverrides(c) = scheme
    End Sub

    Public Function SetTabOrder(scheme As TabScheme) As Integer
        Try
            Dim controlArraySorted As New ArrayList()
            controlArraySorted.AddRange(container.Controls)
            controlArraySorted.Sort(New TabSchemeComparer(scheme))

            For Each c As Control In controlArraySorted
                Debug.WriteLine("Tab Index Settings:  Changing tab index for " & c.Name)


                c.TabIndex = System.Math.Max(System.Threading.Interlocked.Increment(curTabIndex), curTabIndex - 1)
                If c.Controls.Count > 0 Then
                    ' Control has children -- recurse.
                    Dim childScheme As TabScheme = scheme
                    curTabIndex = (New Cls_TabIndexSettings(c, curTabIndex, schemeOverrides)).SetTabOrder(childScheme)
                End If
            Next

            Return curTabIndex
        Catch e As Exception
            Debug.Assert(False, "Exception in SetTabOrder:  " & e.Message)
            Return 0
        End Try
    End Function

#End Region

End Class




