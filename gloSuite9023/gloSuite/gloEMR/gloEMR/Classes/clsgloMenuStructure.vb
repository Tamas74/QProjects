
Public Class gloToolBars
    Private _New As gloMenuToolBarDetail
    Private _Save As gloMenuToolBarDetail
    Private _SaveAndClose As gloMenuToolBarDetail
    Private _Finish As gloMenuToolBarDetail
    Private _Close As gloMenuToolBarDetail
    Private _Print As gloMenuToolBarDetail
    Private _Fax As gloMenuToolBarDetail
    Private _PreviousRecord As gloMenuToolBarDetail
    Private _HideRecord As gloMenuToolBarDetail

    Public ReadOnly Property NewItem() As gloMenuToolBarDetail
        Get
            _New.MenuName = "New"
            _New.MenuIdentifier = "New"
            Return _New
        End Get
    End Property


    Public ReadOnly Property Save() As gloMenuToolBarDetail
        Get
            _Save.MenuName = "Save"
            _Save.MenuIdentifier = "Save"
            Return _Save
        End Get
    End Property

    Public ReadOnly Property SaveAndClose() As gloMenuToolBarDetail
        Get
            _SaveAndClose.MenuName = "SaveAndClose"
            _SaveAndClose.MenuIdentifier = "SaveAndClose"
            Return _SaveAndClose
        End Get
    End Property

    Public ReadOnly Property Finish() As gloMenuToolBarDetail
        Get
            _Finish.MenuName = "Finish"
            _Finish.MenuIdentifier = "Finish"
            Return _Finish
        End Get
    End Property

    Public ReadOnly Property Close() As gloMenuToolBarDetail
        Get
            _Close.MenuName = "Close"
            _Close.MenuIdentifier = "Close"
            Return _Close
        End Get
    End Property

    Public ReadOnly Property Print() As gloMenuToolBarDetail
        Get
            _Print.MenuName = "Print"
            _Print.MenuIdentifier = "Print"
            Return _Print
        End Get
    End Property

    Public ReadOnly Property Fax() As gloMenuToolBarDetail
        Get
            _Fax.MenuName = "Fax"
            _Fax.MenuIdentifier = "Fax"
            Return _Fax
        End Get
    End Property

    Public ReadOnly Property PreviousRecord() As gloMenuToolBarDetail
        Get
            _PreviousRecord.MenuName = "Previous"
            _PreviousRecord.MenuIdentifier = "Previous"
            Return _PreviousRecord
        End Get
    End Property

    Public ReadOnly Property HideRecord() As gloMenuToolBarDetail
        Get
            _HideRecord.MenuName = "Hide"
            _HideRecord.MenuIdentifier = "Hide"
            Return _HideRecord
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        _New = New gloMenuToolBarDetail
        _Save = New gloMenuToolBarDetail
        _SaveAndClose = New gloMenuToolBarDetail
        _Finish = New gloMenuToolBarDetail
        _Close = New gloMenuToolBarDetail
        _Print = New gloMenuToolBarDetail
        _Fax = New gloMenuToolBarDetail
        _PreviousRecord = New gloMenuToolBarDetail
        _HideRecord = New gloMenuToolBarDetail
    End Sub

    Protected Overrides Sub Finalize()
        _New = Nothing
        _Save = Nothing
        _SaveAndClose = Nothing
        _Finish = Nothing
        _Close = Nothing
        _Print = Nothing
        _Fax = Nothing
        _PreviousRecord = Nothing
        _HideRecord = Nothing
        MyBase.Finalize()
    End Sub
End Class

Public Class gloMenuToolBarDetail

    Private _MenuName As String
    Private _MenuIdentifier As String

    Public Property MenuName() As String
        Get
            Return _MenuName
        End Get
        Set(ByVal value As String)
            _MenuName = value
        End Set
    End Property

    Public Property MenuIdentifier() As String
        Get
            Return _MenuIdentifier
        End Get
        Set(ByVal value As String)
            _MenuIdentifier = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class