
Imports System.IO
Imports System.Windows.Forms

Public Class mdlGeneral


    Public Shared eRxHubParticipantId As String = "" '\\"T00000000020315"
    Public Shared eRxHubPassword As String = "" '\\"FXTXGJVZ0W"
    Public Shared sAdvanceRxEnabled As String = ""
    Public Shared sAdvanceStagingServer As String = ""

    ''assign the sql server credentials so that the issettings func will not take the values from EMR registry
    Public Shared sServerName As String = ""
    Public Shared sDatabaseName As String = ""
    Public Shared sUserName As String = ""
    Public Shared sPassword As String = ""


    Public Const constEncryptDecryptKey As String = "12345678" '\\gloEMR5.0 key="12345678" '\\previous key ="20gloStreamInc08"

    'P => Production, T=> Test
    Public Shared sEARFileType As String = "T"

    'N => New Report, R => Retransmission
    Public Shared sEARTransmissionAction As String = "N"

    Public Shared sEARTransmissionFileType As String = "EAR"
    '  Public sEARFileType As String = ""
    'CCYYMMDD -- must be Sunday
    Public sEARStartDate As String = ""

    'CCYYMMDD -- must be Saturday
    Public sEAREndDate As String = ""
    Public nProviderID As Int64 = 0

    '(HardCoded)
    Public Shared sEARFilePath As String


    'same as SenderID
    'will bve stored and retrieved from the settings table
    'setting will be provided from gloemradmin
    'Public eRxSourceParticipantID As String = "T00000000020315"

    '(HardCoded)
    '  Public gstrDatabaseConnectionString As String ' = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=RxHub;Data Source=gloint"

    Public gstrMessageBoxCaption As String = "gloEAR"

    Public Shared Sub UpdateLog(ByVal strLogMessage As String)
        Dim objFile As StreamWriter

        Try
            'Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\DMSBulkInsert.log", True) 

            objFile = New System.IO.StreamWriter(Application.StartupPath & "\EARGeneration.log", True)


            objFile.WriteLine(((System.DateTime.Now.ToString() & ":") + System.DateTime.Now.Millisecond.ToString() & "     ") + strLogMessage)
            objFile.Close()

            objFile = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Public Shared Function GetPrefixTransactionID(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As Long
        Dim strID As String
        Dim dtDate As DateTime
        dtDate = dtFromDate
        strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtToDate.Date) & DateDiff(DateInterval.Second, dtToDate.Date, dtToDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), dtFromDate.Date)
        Return CLng(strID)
    End Function


    Public Function RetrieveFileName(ByVal dtExtractDate As DateTime, ByVal gstrEARDownloadDirectory As String) As String
        Dim _sFileName As String = ""
        'temprarily use current date and time
        dtExtractDate = DateTime.Now()

        Try
            _sFileName = "EAR_" & eRxHubParticipantId & "_" & dtExtractDate.ToString("yyyyMMdd") & "_" & dtExtractDate.ToString("hhmmss") & ".txt"
        Catch ex As Exception
            Throw ex
        End Try

        sEARFilePath = gstrEARDownloadDirectory & "\" & _sFileName
        Return gstrEARDownloadDirectory & "\" & _sFileName

    End Function

    Public Class Clsconnect
        Public Shared gServerName As String
        Public Shared gDataBase As String
        Public Shared gInterval As Integer
        Public Shared gstrUserId As String
        Public Shared gstrPassword As String
        'Public Shared constEncryptDecryptKey As String = "20gloStreamInc08"
        Public Shared sPath As String
        Public Shared sSelectedClient As String


        Public Shared Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String) As String
            Dim strConnectionString As String
            strConnectionString = "SERVER=" & gServerName & ";DATABASE=" & gDataBase & ";USER id=" & gstrUserId & ";Password=" & gstrPassword
            Return strConnectionString
        End Function
        Public Shared Function GetConnectionString() As String
            Return GetConnectionString(gServerName, gDataBase)
        End Function

    End Class

End Class
