Imports oWord = Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop.Word
Imports Microsoft.Office.Core
Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Management
Imports gloGlobal
Imports gloWord


Module mdlDSO
    Public gblTempApp As oWord.Application
    Public isHandlerRemoved As Boolean = True
    Private _gblWordApplication As oWord.Application

    ' Public gWordApp As oWord.Application
    'Private strtWordApp As oWord.Application
    'Public ReadOnly Property GetWordInstance() As oWord.Application
    '    Get
    '        Return strtWordApp
    '    End Get
    'End Property

    'Public ReadOnly Property GetWordInstance(ByVal blnFlag As Boolean, Optional ByVal blnWordExist As Boolean = True) As oWord.Application
    '    Get
    '        CheckWordInstance(blnFlag, blnWordExist)
    '        Return _gblWordApplication
    '    End Get
    'End Property


    Public ReadOnly Property ExamNewDocumentName() As String
        Get

            'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath

            ''If GetOSInfo() = False Then
            ''    _Path = gstrgloEMRStartupPath & "\Temp"
            ''Else
            '    ' gloRegistrySetting.IsServerOS = True
            ''    '' Create a Temp Folder of each user 
            ''    ' gstrgloTempFolder = CreateNewTempDirectory() & "\" '' "\Temp\" & Now.Year & Now.Month & Now.Date & Now.Hour & Now.Minute

            ''    _Path = gstrgloEMRStartupPath & gstrgloTempFolder
            ''End If


            'Dim _NewDocumentName As String = ""
            'Dim _Extension As String = ".docx"
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
            'While File.Exists(_Path & "\" & _NewDocumentName) = True
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
            'End While
            'Return _Path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff")
        End Get
    End Property

    Public Function GetUniqueKey() As String
        'Dim maxSize As Integer = 10
        'Dim minSize As Integer = 5
        'Dim chars As Char() = New Char(10) {}
        'Dim a As String
        'a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        'chars = a.ToCharArray()
        'Dim size As Integer = maxSize
        'Dim data As Byte() = New Byte(0) {}
        'Dim crypto As New Security.Cryptography.RNGCryptoServiceProvider()
        'crypto.GetNonZeroBytes(data)
        'size = maxSize
        'data = New Byte(size - 1) {}
        'crypto.GetNonZeroBytes(data)
        'Dim result As New System.Text.StringBuilder(size)
        'For Each b As Byte In data
        '    result.Append(chars(b Mod (chars.Length - 1)))
        'Next
        'Return result.ToString
        Return gloGlobal.clsFileExtensions.RNGCharacterMask(10)
    End Function

    Private Function GetProcessOwnerName(ByVal ProcessName As String) As String

        Try
            Dim ProcessOwner As String = ""
            Dim x As New ObjectQuery("Select * From Win32_Process where Name ='" & ProcessName & "'")
            Using mos As New ManagementObjectSearcher(x)
                For Each mo As ManagementObject In mos.[Get]()
                    Dim s As String() = New String(1) {}
                    mo.InvokeMethod("GetOwner", DirectCast(s, Object()))
                    ProcessOwner = s(0).ToString()
                    s = Nothing
                    Exit For
                Next
                Return ProcessOwner
            End Using
            x = Nothing
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Return Nothing
            '11-Nov-14 Aniket: Bug #75482: gloEMR > Showing exception while first time login to gloEMR application
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' To exit the Unwanted WinWord processes for optimization
    ''' </summary>
    ''' <remarks></remarks>
    <System.STAThread()> Private Sub KillOtherWord()

        'Dim lstProcesses As List(Of System.Diagnostics.Process) = New List(Of System.Diagnostics.Process)

        'Dim session As Integer = Process.GetCurrentProcess().SessionId

        'For Each word_process As Process In Process.GetProcessesByName("WINWORD")
        '    If (word_process.SessionId = session) Then
        '        lstProcesses.Add(word_process)
        '        Exit For
        '    End If
        'Next

        'Try
        '    ''Get the no of word instances processes running
        '    If lstProcesses.Count() > 0 Then
        '        'Kill all the Word processes that are not been used for gloEMR or External
        '        Dim exitTempApp As oWord.Application = Nothing

        '        Try
        '            exitTempApp = CType((GetObject(Nothing, "Word.Application")), oWord.Application)
        '        Catch ex As Exception

        '        End Try

        '        If Not (exitTempApp Is Nothing) Then
        '            Dim noActiveDocument As Boolean = IsNothing(exitTempApp.Documents)
        '            If (noActiveDocument = False) Then
        '                noActiveDocument = exitTempApp.Documents.Count = 0
        '            End If
        '            If ((exitTempApp.Visible = False) OrElse noActiveDocument) Then

        '                Try
        '                    Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
        '                    exitTempApp.Quit(SaveChanges:=mysaveoptions)
        '                Catch ex As Exception

        '                End Try
        '                If (noActiveDocument) Then
        '                   ' Application.DoEvents()
        '                    lstProcesses.Clear()
        '                    For Each word_process As Process In Process.GetProcessesByName("WINWORD")
        '                        If (word_process.SessionId = session) Then
        '                            lstProcesses.Add(word_process)
        '                            Exit For
        '                        End If
        '                    Next
        '                    For Each Proc As Process In lstProcesses 'SLR: This is called at initial location
        '                        Try

        '                            Proc.CloseMainWindow()

        '                        Catch ex As Exception

        '                        End Try
        '                        '     Application.DoEvents()
        '                        Try

        '                            Proc.Close()


        '                        Catch ex As Exception

        '                        End Try
        '                        '        Application.DoEvents()
        '                        Try

        '                            Proc.Kill()
        '                        Catch ex As Exception


        '                        End Try

        '                        Exit For
        '                    Next

        '                End If
        '            End If
        '        End If


        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at function KillOtherWord of mdlDSO: " + ex.ToString() + " " + ex.InnerException.ToString(), False)
        '    Exit Sub
        'Finally
        '    If lstProcesses IsNot Nothing Then
        '        lstProcesses.Clear()
        '        lstProcesses = Nothing
        '    End If
        'End Try
        gloWord.gloWord.KillOtherWord()
    End Sub


    ''' <summary>
    ''' To initialize the global gloEMR Word instance for Voice and Word Documents
    ''' </summary>
    ''' <remarks></remarks>
    <System.STAThread()> _
    Public Sub InitWord()
        ''Kill the Word instances that are not properly killed
        KillOtherWord()
        ''Initialse the gloEMR Word Instance
        If Not CheckWord() Then
            _gblWordApplication = New oWord.Application()
            _gblWordApplication.Visible = False
        End If
    End Sub


    Public Function CheckWord() As Boolean
        'Dim session As Integer = Process.GetCurrentProcess().SessionId

        'For Each word_process As Process In Process.GetProcessesByName("WINWORD")
        '    If (word_process.SessionId = session) Then
        '        Return True
        '    End If
        'Next
        'Return False
        Return gloWord.gloWord.CheckWord()
        'Dim wdPro As System.Diagnostics.Process()
        'wdPro = System.Diagnostics.Process.GetProcessesByName("WINWORD")
        'If wdPro.Length > 0 Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function

    Public ReadOnly Property NewDocumentName() As String
        Get

            'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath

            ''If GetOSInfo() = False Then
            ''    _Path = gstrgloEMRStartupPath & "\Temp"
            ''Else
            ''    ' gloRegistrySetting.IsServerOS = True
            ''    '' Create a Temp Folder of each user 
            ''    ' gstrgloTempFolder = CreateNewTempDirectory() & "\" '' "\Temp\" & Now.Year & Now.Month & Now.Date & Now.Hour & Now.Minute

            ''    _Path = gstrgloEMRStartupPath & gstrgloTempFolder
            ''End If


            'Dim _NewDocumentName As String = ""
            'Dim _Extension As String = ".doc"
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
            'While File.Exists(_Path & "\" & _NewDocumentName) = True
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
            'End While
            'Return _Path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".doc", "MMddyyyyHHmmssffff")

        End Get
    End Property

    '''' <summary>
    '''' Check for existance of gloEMR Word instance 
    '''' </summary>
    '''' <param name="blnFlag"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Sub CheckWordInstance(ByVal blnFlag As Boolean, Optional ByVal blnWordExist As Boolean = True)
    '    System.Windows.Forms.Application.DoEvents()
    '    Try
    '        Dim tempApp As oWord.Application
    '        ''Get the Word instance object into temporary object
    '        tempApp = CType((GetObject(Nothing, "Word.Application")), oWord.Application)
    '        System.Windows.Forms.Application.DoEvents()
    '        ''check whether the temp object is gloEMR word istance
    '        If tempApp.Equals(_gblWordApplication) Then
    '            'If tempApp.Equals(strtWordApp) Then
    '            ''If so check for Visiblity or temporary control Flag 
    '            If tempApp.Visible = False OrElse blnFlag = False Then
    '                Exit Sub
    '            Else
    '                If Not blnWordExist Then
    '                    gblTempApp = New oWord.Application
    '                    gblTempApp.Visible = False
    '                    Exit Sub
    '                End If
    '                ''gloEMR Word Instance is being used by External Applications
    '                '' Releasing the existing gloEMR Word Instance 
    '                Marshal.FinalReleaseComObject(_gblWordApplication)
    '                _gblWordApplication = Nothing
    '                'Marshal.FinalReleaseComObject(strtWordApp)
    '                'strtWordApp = Nothing
    '                ''Re-Initializing the gloEMR Word Instance
    '                InitWord()
    '                ''Return False flag to re-initialize the word Control
    '                Exit Sub
    '            End If
    '        Else
    '            Try
    '                ''Get the Word instance object into temporary object
    '                tempApp = CType(Marshal.GetActiveObject("Word.Application"), oWord.Application)
    '                ''check whether the temp object is gloEMR word istance
    '                If tempApp.Equals(_gblWordApplication) Then
    '                    'If tempApp.Equals(strtWordApp) Then
    '                    Exit Sub
    '                Else
    '                    ''If not so return flag for temporary control Flag 
    '                    Exit Sub
    '                End If

    '                ''If no Word instance running - Execption occurs
    '            Catch ex As Exception
    '                ''Re-Initializing the gloEMR Word Instance
    '                InitWord()
    '                Exit Sub
    '            End Try
    '        End If
    '        ''If no Word instance running - Execption occurs
    '    Catch ex As Exception
    '        ''Check for Exception type
    '        If ex.Message.Equals("Cannot create ActiveX component.") Then
    '            ''Re-Initializing the gloEMR Word Instance
    '            InitWord()
    '            Exit Sub
    '        End If
    '    End Try
    'End Sub

    ''' <summary>
    ''' Exit the gloEMR Word Instance 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExitWord()
        '' Kill the other Word instances that have not been closed properly
        KillOtherWord()
    End Sub

    Public Function GetActiveDocumentName() As String
        Dim oWordApp As Microsoft.Office.Interop.Word.Application = Nothing

        Try
            oWordApp = CType(Marshal.GetActiveObject("Word.Application"), oWord.Application) 'GetObject(, "Word.Application") 'SLR: Get object create objects when there is no object,
        Catch ex As Exception

        End Try
        If (IsNothing(oWordApp) = False) Then
            ' If (IsNothing(oWordApp.Documents) = False) Then
            '   If (oWordApp.Documents.Count > 0) Then
            Try
                If (IsNothing(oWordApp.ActiveDocument) = False) Then
                    Return oWordApp.ActiveDocument.FullName
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Return Nothing
            End Try

            'Else
            '   Return Nothing
            'End If
            'Else
            '   Return Nothing
            'End If
        Else
            Return Nothing
        End If


    End Function


End Module
''' <summary>
''' <para>IWord Interface required for call GetdataFromOtherForms but compile time we dont know from which form's GetdataFromOtherForms method is required
'''       so we typecast windows from to IWord and call GetdataFromOtherForms of form which is required </para>
'''<para >Note:refer example statement in clsWord.vb -> clswordDocument's GetCheifComplaints() or GetCustomdataFromOtherForms function
'''       check following function are implemented by any other Interface then use same function 
''' </para>
''' 
''' <example>
'''           CType(myCallingForm, IWord).GetCustomdataFromOtherForms(enumDocType.CheifComplaints)
''' test for tfs
''' </example>
''' ''' 
''' </summary>
''' <remarks> added by dipak :20090917 for implement comman liquid field functionality 
''' </remarks>
Interface IWord
    'use to call GetdataFromOtherForms of mycaller form
    Sub GetdataFromOtherForms(ByVal DocType As gloEMR.gloEMRWord.enumDocType)
    'Sub GetdataFromOtherForms(ByVal ListDocTypes As List(Of gloEMR.gloEMRWord.enumDocType))
    'use to call ShowMicrophon of mycaller form
    Sub ShowMicrophone()
    'use to call TurnOffMicrophone of mycaller form
    Sub TurnOffMicrophone()
End Interface
'End Namespace