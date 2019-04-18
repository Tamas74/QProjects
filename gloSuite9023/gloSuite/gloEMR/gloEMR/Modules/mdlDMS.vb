Module mdlDMS
    Public sABBYYLogString As String = ""

    'DMS System Constant Variables - Vinayak
    'Public DMSRootPath As String = Application.StartupPath
    'Public DMSRootPath As String
    Public DMSRootFolder As String = "DMS System"
    Public DMSGeneralContainer As String = "Container"
    Public DMSRecycleBinContainer As String = "Recycle Bin"
    Public DMSScanContainer As String = "Scan"
    Public DMSNoteDocument As String = "Note.Doc"
    Public DMSTempExtractFolder As String = "Temp"

    Public DMSImportFile As String = ""


    Public DMSSendToNewFile As String = "Send to new file"
    Public DMSSendToMergeFile As String = "Merge in existing file"
    Public DMSSendToGeneralBin As String = "Send to General Bin"
    Public DMSSendToAnotherPatient As String = "Send to another patient"
    Public DMSSendToDelete As String = "Delete Page"
    Public DMSAddNotes As String = "Add Note"
    Public DMSDeleteNote As String = "Delete Note"
    Public DMSSelectAllPages As String = "Select All"
    Public DMSPrintFile As String = "Print"
    Public DMSFaxFile As String = "Fax"
    Public DMSPrintFileAll As String = "Print All"
    Public DMSFaxFileAll As String = "Fax All"
    Public DMSPrintFaxFile As String = "Print and Fax"

    Public Const DocumentTodays = "Today's"
    Public Const DocumentYesterDay = "Yesterday"
    Public Const DocumentLastWeek = "Last Week"
    Public Const DocumentLastMonth = "Last Month"
    Public Const DocumentAll = "All"
    Public Const DocumentCustomDate = "Custom Date"


    Enum DocumentCriteria
        TodaysDocument = 0
        YesterDaysDocument = 1
        LastWeeksDocument = 2
        LastMonthsDocument = 3
        AllDocument = 4
        CustomDateDocument = 5
    End Enum

    Enum UnCategory_Criteria
        NewPages = 1
        MergePages = 2
        DeletePages = 3
    End Enum
    Enum PrintFaxOption
        Print = 0
        Fax = 1
        PrintFax = 2
        PrintAll = 3
        FaxAll = 4
    End Enum

    Public Function DateAsString(ByVal dtDate As Date) As String
        Dim nDay As Integer = dtDate.Day
        Dim nMonth As Integer = dtDate.Month
        Dim nYear As Integer = dtDate.Year
        DateAsString = If(Val(nMonth) <= 9, "0" & Val(nMonth), nMonth) & If(Val(nDay) <= 9, "0" & Val(nDay), nDay) & nYear
    End Function

    Public Function WriteAbbyyLog(ByVal Log As String) As Boolean
        Dim _Result As Boolean = False
        Dim oLogFolder As String = Application.StartupPath & "\ABBYY LOG"
        If System.IO.Directory.Exists(oLogFolder) = False Then
            MkDir(oLogFolder)
        End If

        Dim oLogFile As String = ""
        oLogFile = oLogFolder & "\" & Replace(Replace(Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt"), "/", " "), ":", " ") & ".ini"

        While System.IO.File.Exists(oLogFile) = True
            oLogFile = oLogFolder & "\" & Replace(Replace(Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt"), "/", " "), ":", " ") & ".ini"
        End While

        Dim oFile As System.IO.StreamWriter = System.IO.File.CreateText(oLogFile)
        oFile.WriteLine(sABBYYLogString)
        oFile.Close()
        oFile.Dispose()
        oFile = Nothing
        Return _Result
    End Function
End Module
