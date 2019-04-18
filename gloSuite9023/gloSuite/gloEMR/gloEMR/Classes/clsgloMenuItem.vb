Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms


Public Class gloMenuItem

    Inherits MenuItem

    Enum enumEventType
        NewDocument = 0
        MergeWithDocument = 1
        MergeInExisting = 2
    End Enum

    Private _Tag As String
    Private _EventType As enumEventType
    Private _MenuItemDoucment As gloStream.gloDMS.Supporting.MenuItemDocument


    Public Property Tag() As String
        Get
            Return _Tag
        End Get
        Set(ByVal Value As String)
            _Tag = Value
        End Set
    End Property

    Public Property EventType() As enumEventType
        Get
            Return _EventType
        End Get
        Set(ByVal Value As enumEventType)
            _EventType = Value
        End Set
    End Property

    Public Property DocumentMenuItem() As gloStream.gloDMS.Supporting.MenuItemDocument
        Get
            Return _MenuItemDoucment
        End Get
        Set(ByVal Value As gloStream.gloDMS.Supporting.MenuItemDocument)
            _MenuItemDoucment = Value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        _MenuItemDoucment = New gloStream.gloDMS.Supporting.MenuItemDocument
        'Me.OwnerDraw = True
    End Sub
    Private disposedValue As Boolean = False        ' To detect redundant calls
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _MenuItemDoucment = Nothing
            End If
            MyBase.Dispose(False)
        End If

        'If Not Me Is Nothing Then

        'End If
        Me.disposedValue = True
    End Sub

End Class
