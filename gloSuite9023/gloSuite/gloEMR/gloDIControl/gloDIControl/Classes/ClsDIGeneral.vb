
Public Class ClsDIGeneral

    Private Shared _mAgeInDays As String = ""
    Private Shared mIsDefaultPrinterSet As Boolean = False
    Public Shared Property PatientAgeInDays() As String
        Get
            Return _mAgeInDays
        End Get
        Set(ByVal value As String)
            _mAgeInDays = value
        End Set
    End Property
    Public Shared Property IsDefaultPrinterSet() As Boolean
        Get
            Return mIsDefaultPrinterSet
        End Get
        Set(ByVal value As Boolean)
            mIsDefaultPrinterSet = value
        End Set
    End Property
End Class
