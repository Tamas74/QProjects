'------------------------------
'
'
'
'------------------------------
Option Explicit On 

'Namespaces

'Needed to maintain Errorlog
Imports System.IO


Module mdlOMR
    '<OLD>
    'This Global variable store current Directory Path
    Public gblDirPath As String = Application.StartupPath & "\.."
    '''''''''This Global variable store Tif file Exist
    ''''''''Public gblTifPath As String = gblDirPath & "\Tif"
    'This Global variable store path for Essential resources
    Public gblEssentaialsPath As String = Trim(gblDirPath) & "\Essentials"

    '<all the following paths are in essential folder>
    'This Global variable store path for gblClassifiersPath
    Public gblClassifiersPath As String = Trim(gblEssentaialsPath) & "\Classifiers"

    '''''''''This Global variable store path for Template
    ''''''''Public gblTempPath As String = Trim(gblEssentaialsPath) & "\Templates"

    '''''''''This Global variable store path for gblTempDbPath
    ''''''''Public gblTempDbPath As String = Trim(gblEssentaialsPath) & "\TemplatesDB"
    '''''''''This Global variable store path for FDL file
    ''''''''Public gblFdlPath As String = Trim(gblEssentaialsPath) & "\FDLs"

    'This Global variable store Tif On which template constructed
    Public gblTempTifPath As String '= Trim(gblEssentaialsPath) & "\TemplatesTif"

    '</OLD>

    '<Setting>
    Private _RootPath As String = Application.StartupPath

    '<Delete this variable while assigning>
    ''Public gnClientMachineID As String = "1"

    Public Property RootPath() As String
        Get
            Return _RootPath
        End Get
        Set(ByVal Value As String)
            Value = _RootPath
        End Set
    End Property

#Region "Scanner functionality"
    'Private _PatientID As String = "1"
    Private _ScannedImagePath As String


    'Public Property PatientID() As String
    '    Get
    '        Return _PatientID
    '    End Get
    '    Set(ByVal Value As String)
    '        _PatientID = Value
    '    End Set
    'End Property

    Public Property ScannedImagePath() As String
        Get
            Return _ScannedImagePath
        End Get
        Set(ByVal Value As String)
            _ScannedImagePath = Value
        End Set
    End Property

#End Region

    'Region containing functionality related to Customize messaging

#Region "Customize messaging"

    



#End Region

    'Region contain entire functionality related to Error Management

    'When this file would be available at developer site then Developer can identify 
    'following terms regarding occurred error.
    '[1] Error Date
    '[2] Error Time
    '[3] Error Message
    '[4] Error NameSpace
    '[5] Error event
    '[6] Error Form/Class
    '[7] Error on line Number

#Region "Error Managemet"


    '*******************************************************************
    '***********[ Developer can change setting for "Error Log File" ]****
    '*******************************************************************

    'variables for property procedures
    Private _ErrLogFilePath As String = Application.StartupPath & "\..\"
    Private _DelimiterCharacter As Char = "~"
    Private _ErrorLogFileName As String = "ErrorLog.ini"

    'Property Procedures
    Private Property ErrLogFilePath() As String
        Get
            Return _ErrLogFilePath
        End Get
        Set(ByVal Value As String)
            _ErrLogFilePath = Value
        End Set
    End Property

    Private Property DelimiterCharacter() As Char
        Get
            Return _DelimiterCharacter
        End Get
        Set(ByVal Value As Char)
            _DelimiterCharacter = Value
        End Set
    End Property

    Private Property ErrorLogFileName() As String
        Get
            Return _ErrLogFilePath
        End Get
        Set(ByVal Value As String)
            _ErrLogFilePath = Value
        End Set
    End Property

    'Function Name = setErrorLog
    'Scope = Public

    'Functionality
    'This function will maintain log of each and every Error occured into log file.
    'This function will save Error detail like Error Date, Error Time, Error Message, '
    'Error NameSpace, Error event, Error Form/Class, Error on line Number.

    'Parameter
    '[1] Ex (Exception)= Exception (c)

    'Output = update Error log (Error Log File)
    Public Sub setErrorLog(ByVal Ex As Exception)
        Try
            Dim sFileName As String = _ErrLogFilePath & "\" & Trim(_ErrorLogFileName)

            If File.Exists(sFileName) = True Then
                Dim stmWrite As StreamWriter '= New StreamWriter(sFileName)
                'Dim filErrorLog As File
                stmWrite = File.AppendText(sFileName)
                stmWrite.WriteLine(getErrorString(Ex, _DelimiterCharacter))
                stmWrite.Close()
                stmWrite.Dispose()
            Else
                Dim stmAppendWrite As StreamWriter = File.CreateText(sFileName)
                stmAppendWrite.WriteLine("Error Date~Error Time~Error Message~Error NameSpace~Error Event~Error Form/Class~Error on Line Number")
                stmAppendWrite.WriteLine(getErrorString(Ex, _DelimiterCharacter))
                stmAppendWrite.Close()
                stmAppendWrite.Dispose()
            End If
        Catch exp As Exception
            'MessageBox.Show(exp.ToString, "Error Log", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    'Function Name = getErrorString
    'Scope = Private

    'Functionality
    'This Function will return Error String(Single Line)

    'Input 
    '[1] Ex (Exception)= Exception (c)
    '[2] Delimeter (Char)  = Delimeter Character (c)

    'Output = generate Error string with all error detail seprated by Delimiter (single line)
    Private Function getErrorString(ByVal ex As Exception, ByVal Delimiter As Char) As String
        Dim sErrorString As String
        Dim sTemp As String
        Dim arrTemp() As String

        'Error Date [1]
        sErrorString = Trim(Format(Now(), "dd-MMM-yyyy"))

        'Error Time [2]
        sErrorString = sErrorString & Trim(Delimiter) & Trim(Format(Now(), "HH:mm"))

        With ex
            'Error Message [3]
            sErrorString = sErrorString & Trim(Delimiter) & Trim(removeCharacters(.Message & "", Chr(13)))
            'NameSpace [4]
            sErrorString = sErrorString & Trim(Delimiter) & Trim(removeCharacters(.Source & "", Chr(13)))

            'Spliting string on " at "
            arrTemp = Split(" " & Trim(.StackTrace & ""), " at ")
            If UBound(arrTemp) >= 0 Then
                'Accessing Last Element of array
                sTemp = Trim(arrTemp(UBound(arrTemp)) & "")

                'Spliting string on " in "
                arrTemp = Split(" " & Trim(sTemp), " in ")
                If UBound(arrTemp) >= 0 Then
                    'Event [5]
                    sErrorString = sErrorString & Trim(Delimiter) & Trim(arrTemp(0) & "")

                    'Accessing Last Element of array
                    sTemp = Trim(arrTemp(1) & "")

                    'Spliting string on ":line"
                    arrTemp = Split(Trim(sTemp), ":line")

                    If UBound(arrTemp) >= 0 Then
                        'Form path(with Name) [6]
                        sErrorString = sErrorString & Trim(Delimiter) & Trim(arrTemp(0) & "")

                        'Line Number [7]
                        sErrorString = sErrorString & Trim(Delimiter) & Trim(arrTemp(1) & "")
                    End If
                End If
            End If

            getErrorString = ValidateErrorString(sErrorString, Delimiter, 6)
        End With

    End Function

    'Function Name =  ValidateErrorString
    'Scope = Private

    'Functionality
    'check validity of string.
    'If given number of "delimiter character" exists then string is valid
    'if "delimiter character" is less than expacted then this function will add "delimiter character" to string

    'Input
    '[1] ErrString (string) = Contain a error string (c)
    '[2] Delimeter (Char)  = Delimeter Character (c)
    '[3] MaxDelimer (integer) = Number of Delimiter charachters

    'Output = valid Error String
    Private Function ValidateErrorString(ByVal ErrString As String, ByVal Delimiter As Char, ByVal MaxDelimiter As Integer) As String
        Dim i As Integer
        Dim iDelimiterCount As Integer
        Dim iErrLength As Integer = Len(Trim(ErrString))
        For i = 1 To iErrLength
            If UCase(Mid(Trim(ErrString), i, 1)) = UCase(Trim(Delimiter)) Then
                iDelimiterCount = iDelimiterCount + 1
            End If
        Next

        If (MaxDelimiter > iDelimiterCount) Then
            ValidateErrorString = addDelimeterToString(ErrString, Delimiter, (MaxDelimiter - iDelimiterCount))
        Else
            ValidateErrorString = ErrString
        End If
    End Function

    'Function Name =  addDelimiterToString
    'Scope = Private

    'Functionality
    'Add given number of Delimiter to given Error string.

    'Input
    '[1] ErrString (string) = Error string
    '[2] Delimiter (character) = Delimiter character
    '[3] Number (integer) = number of Delimiter

    'Output = Error String
    Private Function addDelimeterToString(ByVal ErrString As String, ByVal Delimiter As Char, ByVal Number As Integer) As String
        Dim i As Integer
        For i = 1 To Number
            ErrString = ErrString & Trim(Delimiter)
        Next
        addDelimeterToString = ErrString
    End Function

    'Function Name =  removeCharacters
    'Scope = Private

    'Functionality = Remove given character from given string

    'Input
    '[1] sString (string) = Remove character from this string
    '[2] Character (char) = (This character will be remoed from the string] generally it will be Chr(13) means ENTER Key

    'Output = String without given character
    Private Function removeCharacters(ByVal sString As String, ByVal character As Char) As String
        Dim i As Integer
        Dim iStrLength As Integer = Len(Trim(sString))
        removeCharacters = ""
        For i = 1 To iStrLength
            If UCase(Mid(Trim(sString), i, 1)) <> UCase(Trim(character)) Then
                'chr(10) also send data to next line
                If UCase(Mid(Trim(sString), i, 1)) <> Chr(10) Then removeCharacters = removeCharacters & Mid(Trim(sString), i, 1)
            Else
                'MsgBox("chr(13)")
            End If
        Next
    End Function

#End Region

#Region "Nilesh"
    Public OMRRootPath As String = Application.StartupPath
    'Public gstrMessageBoxCaption As String = "gloEMR"
    'Public gstrDatabaseName As String = "gloEMR-DMS"
    'Public gstrSQLServerName As String = "SAKAR_SERVER"


    'Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String) As String
    '    Dim strConnectionString As String
    '    strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
    '    Return strConnectionString
    'End Function
    'Public Function GetConnectionString() As String
    '    Return GetConnectionString(gstrSQLServerName, gstrDatabaseName)
    'End Function

#End Region

End Module
