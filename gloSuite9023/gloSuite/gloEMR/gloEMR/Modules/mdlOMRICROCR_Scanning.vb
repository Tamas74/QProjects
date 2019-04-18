Module mdlScanning
    Private Scanning As Boolean = False
    Private StopScanning As Boolean = False
    Private ScanFolder As String = Application.StartupPath & "\Acquire Image"
    Private Declare Function GetTempFileName Lib "kernel32" Alias "GetTempFileNameA" (ByVal lpszPath As String, ByVal lpPrefixString As String, ByVal wUnique As Long, ByVal lpTempFileName As String) As Long
    Private Declare Function DeleteFile Lib "kernel32" Alias "DeleteFileA" (ByVal lpFileName As String) As Long
    Public gNodeImageFromFile As String = "Image From File"
    Public gNodeImageFromScanner As String = "Image From Scanner"
    Public gsProcessCCITTFDocument As String = ""
    Public gMultipleScanImagesFolder As String = "MultipleScanImages" ' to temporary scan multiple images in this folder
    Public gPDFFormatSampleFolder As String = "DMSSetting_PDFFormat"

    Public Function ImageFromFile(ByVal sImageFile As String) As String
        ImageFromFile = ""
        Dim _NewFilePath As String = Get_NewScanFileName(ScanFolder)
        If System.IO.File.Exists(sImageFile) = True Then
            System.IO.File.Copy(sImageFile, _NewFilePath)
            If System.IO.File.Exists(_NewFilePath) = True Then
                Return _NewFilePath
            End If
        End If
    End Function

    Public Function ImageFromFile_ForReadResult(ByVal sImageFile As String, ByVal sTempleteName As String) As String
        'ImageFromFile_ForReadResult = ""
        'Dim _NewFileFolder As String = gTempletePath & "\" & sTempleteName & " Scan"
        'Dim _NewFilePath As String = ""

        'Dim i As Long
        'For i = 1 To 32000 ' HardCoded 32000
        '    If System.IO.File.Exists(_NewFileFolder & "\" & sTempleteName & i & ".tif") = False Then
        '        _NewFilePath = _NewFileFolder & "\" & sTempleteName & i & ".tif"
        '        Exit For
        '    End If
        'Next

        'If System.IO.File.Exists(sImageFile) = True Then
        '    If System.IO.Directory.Exists(_NewFileFolder) = True Then
        '        System.IO.File.Copy(sImageFile, _NewFilePath)
        '        If System.IO.File.Exists(_NewFilePath) = True Then
        '            Return _NewFilePath
        '        End If
        '    End If
        'End If
        Return ""
    End Function

    Private Function NewInDir(ByVal Folder As String) As String
        'Process Image 3
        Dim ret As String
        ret = Space(260)
        Dim res As Long
        res = GetTempFileName(ScanFolder, "Img", 0, ret)
        If res = 0 Then Err.Raise(0, "", "The GetTempFileName function failed")
        res = DeleteFile(ret)
        If res = 0 Then Err.Raise(0, "", "The DeleteFile function failed")
        ret = RTrim(ret)
        ' Replace .tmp with .tif extension
        ret = Left(ret, Len(ret) - 4)
        ret = ret + "tif"
        NewInDir = ret
    End Function

    Private Function Get_NewScanFileName(ByVal sScanFolder As String) As String
        Get_NewScanFileName = ""
        Dim _FileName As String = Replace(Replace(Date.Now, "/", ""), ":", "")
        Dim _FileExtension As String = ".TIF"

        If System.IO.File.Exists(sScanFolder & "\" & _FileName & _FileExtension) = False Then
            Get_NewScanFileName = sScanFolder & "\" & _FileName & _FileExtension
        Else
            For i As Integer = 1 To 32000
                If System.IO.File.Exists(sScanFolder & "\" & _FileName & i & _FileExtension) = False Then
                    Get_NewScanFileName = sScanFolder & "\" & _FileName & i & _FileExtension
                    Exit For
                End If
            Next
        End If
    End Function

    Private Function Get_MultipleImageScanFolder() As String
        Get_MultipleImageScanFolder = ""
        Dim _MultipleImageBaseFolder As String = Application.StartupPath & "\" & gMultipleScanImagesFolder  ' \" '& Replace(Replace(Date.Today.Now, "/", ""), ":", "")
        Dim _MultipleImageCurrentFolder As String = _MultipleImageBaseFolder & "\" & Replace(Replace(Date.Now, "/", ""), ":", "")
        'Make Multiple Images Base Folder
        If System.IO.Directory.Exists(_MultipleImageBaseFolder) = False Then
            MkDir(_MultipleImageBaseFolder)
        End If
        'Make Current DateTime Folder in Base Folder
        If System.IO.Directory.Exists(_MultipleImageCurrentFolder) = False Then
            MkDir(_MultipleImageCurrentFolder)
        Else
            For i As Integer = 1 To 32000
                If System.IO.Directory.Exists(_MultipleImageCurrentFolder & i) = False Then
                    MkDir(_MultipleImageCurrentFolder)
                    Exit For
                End If
            Next
        End If
        Return _MultipleImageCurrentFolder
    End Function


End Module
