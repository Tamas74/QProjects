Module mdlLockScreen
    Public Structure LastInputInfo
        Public cbSize As Int32
        Public dwTime As Int32
    End Structure

    Public Declare Function GetLastInputInfo Lib "User32.dll" (ByRef lpLastInputInfo As LastInputInfo) As Long
    Public Declare Function timeGetTime Lib "winmm.dll" () As Long
    

End Module
