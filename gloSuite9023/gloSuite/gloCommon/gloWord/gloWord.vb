Imports Microsoft.Office
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
'Imports Microsoft.Office.Interop.Word
Imports Microsoft.VisualBasic
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data
Imports System.Text.RegularExpressions
'Imports System.Text.StringBuilder
Imports System.Text
Imports System
Imports System.Timers
Imports System.Security.Cryptography
Imports System.Management
Imports System.Threading
Imports System.Reflection
Imports System.Runtime.Remoting.Messaging
Imports System.Data.SqlClient
Imports gloGlobal
Imports gloPrintDialog






Public Class gloWord

    Public Shared CurrentDoc As Wd.Document
    Public Shared gblTempApp As Wd.Application
    Private Shared _gblWordApplication As Wd.Application

    Private _MessageBoxCaption As String = "gloPM"

    Private Shared isShowAgeInDays As Boolean = False
    Private Shared ageLimit As Int64 = 0
    Public Shared ActiveDocumentName As String
    Public Shared Clipboard_Backup As Dictionary(Of String, Object) = Nothing


    'Public Sub New(ByVal oCurDoc As Wd.Document)
    '    _oCurDoc = oCurDoc
    'End Sub

    Public Sub New()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    'Public Shared Sub GetFormFieldData(ByVal strDataCols As Array, ByVal aField As Wd.FormField)
    '    oCurDoc = CurrentDoc
    '    SetFieldResult(strDataCols, aField)
    'End Sub

    Public Shared Sub GetFormFieldData(ByVal strDataCols As Array, ByVal aField As Wd.FormField, ByVal dtFlowSheet As DataTable)
        Select Case Trim(strDataCols(0))
            Case Is <> ""
                Select Case Len(strDataCols(0))
                    Case Is <= 255
                        Select Case strDataCols(1)
                            Case "2"
                                ''Bug #68689: 00000700: Error when printing Superbills
                                'Check if bookmark present in template or not.
                                If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                    aField.Result = "  "
                                    CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                    CurrentDoc.Application.Selection.Collapse()
                                    CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                    If File.Exists(strDataCols(0)) Then
                                        CurrentDoc.Application.Selection.InsertFile(strDataCols(0))
                                    End If
                                    Exit Select
                                Else
                                    MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Case "3"
                                ''Bug #68689: 00000700: Error when printing Superbills
                                'Check if bookmark present in template or not.
                                If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                    aField.Result = "  "
                                    CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                    CurrentDoc.Application.Selection.Collapse()
                                    CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)

                                    If aField.StatusText.Contains("Patient_Cards.iCard") Then
                                        Dim strDatas() As String = strDataCols(0).Split("~")
                                        For Each Str As String In strDatas
                                            Dim strEx As String = InsertImageIntoSelectionField(CurrentDoc, Str, "", "GloWordGetFormFieldData1", False)
                                            If (String.IsNullOrEmpty(strEx) = False) Then
                                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strEx, gloAuditTrail.ActivityOutCome.Failure)
                                            End If
                                        Next
                                    Else
                                        If File.Exists(strDataCols(0)) Then
                                            Dim strEx As String = InsertImageIntoSelectionField(CurrentDoc, strDataCols(0), "", "GloWordGetFormFieldData1", False)
                                            If (String.IsNullOrEmpty(strEx) = False) Then
                                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strEx, gloAuditTrail.ActivityOutCome.Failure)
                                            End If

                                            'Try
                                            '    Dim oImage As Image = Image.FromFile(strDataCols(0))
                                            '    If (IsNothing(oImage) = False) Then


                                            '        Try
                                            '            Global.gloWord.gloWord.GetClipboardData()
                                            '            'Clipboard.Clear()
                                            '            Try
                                            '                Clipboard.SetImage(oImage)
                                            '            Catch ex As Exception
                                            '                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '                ex = Nothing
                                            '            End Try

                                            '        Catch ex As Exception
                                            '            ' MessageBox.Show("Unable to set image to Clipboard", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '            ex = Nothing
                                            '        End Try
                                            '        Try
                                            '            Try
                                            '                CurrentDoc.Application.Selection.Paste()
                                            '            Catch ex As Exception
                                            '                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '                ex = Nothing
                                            '            End Try

                                            '            ' Clipboard.Clear()
                                            '            Global.gloWord.gloWord.SetClipboardData()
                                            '        Catch ex As Exception
                                            '            ' MessageBox.Show("Unable to get image from Clipboard", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '            ex = Nothing
                                            '        End Try

                                            '        oImage.Dispose()
                                            '        oImage = Nothing
                                            '    End If

                                            'Catch ex As Exception
                                            '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '    ex = Nothing
                                            'End Try

                                            'CurrentDoc.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"
                                        End If
                                    End If
                                    Exit Select
                                Else
                                    MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Case "4"
                                    '' For Decimal Datatype
                                    aField.Result = Convert.ToDecimal(strDataCols(0))
                            Case "5"
                                    '' GetAge In String
                                    aField.Result = GetAge(CType(strDataCols(0), Date))
                            Case "6" ''For FlowSheet To Generate Data in Tabular Form
                                    ''Bug #68689: 00000700: Error when printing Superbills
                                    'Check if bookmark present in template or not.
                                    If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                        aField.Result = "  "
                                        CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                        CurrentDoc.Application.Selection.Collapse()
                                        CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                        InsertFlowSheetTable(dtFlowSheet, aField)
                                    Else
                                        MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                            Case Else
                                    aField.Result = strDataCols(0)
                        End Select
                    Case Is > 255
                        Select Case strDataCols(1)
                            Case "2"
                                ''Bug #68689: 00000700: Error when printing Superbills
                                'Check if bookmark present in template or not.
                                If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                    aField.Result = "  "
                                    CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                    CurrentDoc.Application.Selection.Collapse()
                                    CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                    If File.Exists(strDataCols(0)) Then
                                        CurrentDoc.Application.Selection.InsertFile(strDataCols(0))
                                    End If
                                    Exit Select
                                Else
                                    MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Case "3"
                                ''Bug #68689: 00000700: Error when printing Superbills
                                'Check if bookmark present in template or not.
                                If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                    aField.Result = "  "
                                    CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                    CurrentDoc.Application.Selection.Collapse()
                                    CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)

                                    If aField.StatusText.Contains("Patient_Cards.iCard") Then
                                        Dim strDatas() As String = strDataCols(0).Split("~")
                                        For Each Str As String In strDatas
                                            Dim strEx As String = InsertImageIntoSelectionField(CurrentDoc, Str, "", "GloWordGetFormFieldData2", False)
                                            If (String.IsNullOrEmpty(strEx) = False) Then
                                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strEx, gloAuditTrail.ActivityOutCome.Failure)
                                            End If
                                        Next
                                    Else

                                        If File.Exists(strDataCols(0)) Then
                                            Dim strEx As String = InsertImageIntoSelectionField(CurrentDoc, strDataCols(0), "", "GloWordGetFormFieldData2", False)
                                            If (String.IsNullOrEmpty(strEx) = False) Then
                                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strEx, gloAuditTrail.ActivityOutCome.Failure)
                                            End If
                                            'Try
                                            '    Dim oImage As Image = Image.FromFile(strDataCols(0))
                                            '    If (IsNothing(oImage) = False) Then


                                            '        Try
                                            '            Global.gloWord.gloWord.GetClipboardData()
                                            '            'Clipboard.Clear()
                                            '            Try
                                            '                Clipboard.SetImage(oImage)
                                            '            Catch ex As Exception
                                            '                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '                ex = Nothing
                                            '            End Try


                                            '        Catch ex As Exception
                                            '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '            ex = Nothing
                                            '        End Try
                                            '        Try

                                            '            Try
                                            '                CurrentDoc.Application.Selection.Paste()
                                            '            Catch ex As Exception
                                            '                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '                ex = Nothing
                                            '            End Try

                                            '            ' Clipboard.Clear()
                                            '            Global.gloWord.gloWord.SetClipboardData()
                                            '        Catch ex As Exception
                                            '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '            ex = Nothing
                                            '        End Try
                                            '        oImage.Dispose()
                                            '        oImage = Nothing
                                            '    End If

                                            'Catch ex As Exception
                                            '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                            '    ex = Nothing
                                            'End Try

                                            'CurrentDoc.Application.Selection.InlineShapes.AddPicture(FileName:=strDataCols(0), LinkToFile:=False, SaveWithDocument:=True).AlternativeText = "gloImageDataField"
                                        End If
                                    End If
                                    Exit Select
                                Else
                                    MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Case "4"
                                    '' For Decimal Datatype
                                    aField.Result = Convert.ToDecimal(strDataCols(0))
                            Case "5"
                                    '' GetAge In String
                                    aField.Result = GetAge(CType(strDataCols(0), Date))
                            Case "6" ''For FlowSheet To Generate Data in Tabular Form
                                    ''Bug #68689: 00000700: Error when printing Superbills
                                    'Check if bookmark present in template or not.
                                    If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                        aField.Result = "  "
                                        CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                        CurrentDoc.Application.Selection.Collapse()
                                        CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                        InsertFlowSheetTable(dtFlowSheet, aField)
                                    Else
                                        MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                            Case Else
                                    ''Bug #68689: 00000700: Error when printing Superbills
                                    'Check if bookmark present in template or not.
                                    If CurrentDoc.Bookmarks.Exists(aField.Name) Then
                                        aField.Result = "  "
                                        CurrentDoc.Application.Selection.GoTo(What:=Wd.WdGoToItem.wdGoToBookmark, Name:=aField.Name)
                                        CurrentDoc.Application.Selection.Collapse()
                                        CurrentDoc.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                        CurrentDoc.Application.Selection.TypeText(strDataCols(0))
                                    Else
                                        MessageBox.Show("An issue has been detected with the liquid link <" & aField.Result & ">." & Environment.NewLine & "Please remove and re-add the liquid link to resolve the problem.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If
                        End Select
                End Select
            Case Else
                'aField.Result = aField.HelpText
                aField.Result = "|" & Replace(aField.Result, "|", "") & "|"
        End Select
    End Sub
    Public Shared Function GetClipBoardDataWithRetry(ByVal MaximumTries As Integer, ByRef strException As String) As DataObject
        Do
            Try
                Return Clipboard.GetDataObject()
            Catch ex As Exception
                MaximumTries = MaximumTries - 1
                If (MaximumTries = 0) Then
                    strException = strException & ex.ToString() & vbCrLf
                    Return Nothing
                End If
            End Try

        Loop Until MaximumTries <= 0
        Return Nothing
    End Function

    Public Shared Function GetClipBoardWithRetry(ByVal MaximumTries As Integer, ByRef strException As String) As Boolean
        'Dim GotClip As Boolean = False
        Do
            Try
                GetClipboardData()
                'MaximumTries = 0
                'GotClip = True
                Return True
            Catch ex As Exception
                MaximumTries = MaximumTries - 1
                If (MaximumTries = 0) Then
                    strException = strException & ex.ToString() & vbCrLf
                    Return False
                End If
            End Try
        Loop Until MaximumTries <= 0
        Return False
    End Function
    Public Shared Function SetClipBoardImageWithRetry(ByVal oImage As Image, ByVal MaximumTries As Integer, ByRef strException As String) As Boolean

        Do
            Try


                Try

                    Clipboard.SetImage(oImage)

                Catch ex As Exception



                    If (ex.Message.Contains("Current thread must be set to single thread apartment (STA) mode before OLE calls can be made. Ensure that your Main function has STAThreadAttribute marked on")) Then
                        Dim t As Thread = New Thread(AddressOf SetClipImage) ''added for incident 00065920
                        t.SetApartmentState(ApartmentState.STA)
                        Try
                            t.Start(oImage)
                            t.Join()
                            t = Nothing
                        Catch ex2 As Exception
                            ex2 = Nothing
                        End Try

                    End If

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    ex = Nothing
                End Try
                Return True
            Catch ex As Exception
                MaximumTries = MaximumTries - 1
                System.Threading.Thread.Sleep(100)
                If (MaximumTries = 0) Then
                    strException = strException & ex.ToString() & vbCrLf
                    Return False
                End If

            End Try

        Loop Until MaximumTries <= 0
        Return False
    End Function
    Public Shared Sub SetClipImage(Optional ByVal oImage As Object = Nothing)
        Dim _img As Image = CType(oImage, Image)
        Clipboard.SetImage(_img)
    End Sub
    Public Shared Function InsertImageIntoSelectionField(ByRef oCurDoc As Wd.Document, ByVal strFileName As String, ByVal strCaption As String, Optional ByVal strComingFrom As String = "", Optional bShowMessage As Boolean = False) As String
        Dim strException As String = ""
        Try
            If (String.IsNullOrEmpty(strFileName) = False) Then
                Dim oImage As Image = Image.FromFile(strFileName)
                If (IsNothing(oImage) = False) Then
                    'Dim MaximumTries As Integer = 5
                    Dim GotClip As Boolean = GetClipBoardWithRetry(5, strException)
                    'MaximumTries = 5
                    Dim SetClip As Boolean = SetClipBoardImageWithRetry(oImage, 5, strException)
                    If (SetClip = False) Then
                        If (bShowMessage) Then
                            MessageBox.Show("Unable to set image to Clipboard " & strComingFrom, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    Else
                        Try
                            oCurDoc.ActiveWindow.Selection.Paste()
                        Catch ex As Exception
                            If (bShowMessage) Then
                                MessageBox.Show("While Pasting form field data unable to get image from Clipboard " & strComingFrom & " due to locked by " & GetOpenClipboardWindowText(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If

                            strException = strException & ex.ToString() & vbCrLf
                        End Try
                        If (GotClip) Then
                            Try
                                SetClipboardData()
                            Catch ex As Exception
                                If (bShowMessage) Then
                                    MessageBox.Show("While Revoking unable to get image from Clipboard " & strComingFrom & " due to locked by " & GetOpenClipboardWindowText(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                                strException = strException & ex.ToString() & vbCrLf

                            End Try
                        End If
                    End If
                    oImage.Dispose()
                    oImage = Nothing
                Else
                    strException = strException & "Image Not Found from" & strFileName & vbCrLf
                End If
            Else
                strException = strException & "File Not Found" & vbCrLf
            End If
        Catch ex As Exception
            If (bShowMessage) Then
                MessageBox.Show("While Inserting image to form field data unable to get image from Clipboard" & strComingFrom & " due to locked by " & GetOpenClipboardWindowText(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            strException = strException & ex.ToString() & vbCrLf
        End Try
        Return strException
    End Function

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

    ' ''' <summary>
    ' ''' To exit the Unwanted WinWord processes for optimization
    ' ''' </summary>
    ' ''' <remarks></remarks>
    '<System.STAThread()> Private Shared Sub KillOtherWord()
    '    'Try
    '    '    Dim p As System.Diagnostics.Process()
    '    '    ''Get the no of word instances processes running
    '    '    p = System.Diagnostics.Process.GetProcessesByName("WINWORD")
    '    '    If p.Length > 0 Then
    '    '        Dim cnt As Int16 = 0
    '    '        ''Kill all the Word processes that are not been used for gloEMR or External
    '    '        While cnt <= p.Length
    '    '            Dim exitTempApp As oWord.Application
    '    '            ''Get the Word instance object 
    '    '            exitTempApp = CType((GetObject(Nothing, "Word.Application")), oWord.Application)
    '    '            If (exitTempApp.Visible = False Or exitTempApp.Documents.Count = 0) Then
    '    '                If Not (exitTempApp Is Nothing) Then
    '    '                    ''Kill the word instance that was invisible and release te refernce
    '    '                    exitTempApp.Quit()
    '    '                    Marshal.FinalReleaseComObject(exitTempApp)
    '    '                End If
    '    '            End If
    '    '            cnt += 1
    '    '        End While
    '    '    End If
    '    'Catch ex As Exception
    '    '    Exit Sub
    '    'End Try

    '    Dim wdPro As System.Diagnostics.Process()

    '    wdPro = System.Diagnostics.Process.GetProcessesByName("WINWORD")
    '    Try

    '        ''Get the no of word instances processes running
    '        If wdPro.Length > 0 Then
    '            'Kill all the Word processes that are not been used for gloEMR or External
    '            Dim myCount As Integer = 0
    '            For Each tempProc As Process In wdPro
    '                myCount = myCount + 1
    '                Dim exitTempApp As Wd.Application = Nothing
    '                Try
    '                    exitTempApp = CType((GetObject(Nothing, "Word.Application")), Wd.Application)
    '                Catch ex As Exception
    '                    'gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
    '                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '                    ex = Nothing
    '                End Try
    '                If Not (exitTempApp Is Nothing) Then
    '                    If (exitTempApp.Visible = False Or exitTempApp.Documents.Count = 0) Then
    '                        Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
    '                        Dim wdAppCaption As String = exitTempApp.Caption
    '                        ''Kill the word instance that was invisible and release te refernce
    '                        exitTempApp.Quit(SaveChanges:=mysaveoptions)
    '                        Dim releaseComObject As Boolean = False
    '                        For Each Proc As Process In Process.GetProcessesByName("WINWORD")
    '                            If Not String.IsNullOrEmpty(Proc.MainWindowTitle) Then
    '                                If Proc.MainWindowTitle.Contains(wdAppCaption) Then
    '                                    Proc.CloseMainWindow()
    '                                    If (myCount = wdPro.Length) Then
    '                                        Marshal.FinalReleaseComObject(exitTempApp)
    '                                        releaseComObject = True
    '                                    Else
    '                                        Marshal.ReleaseComObject(exitTempApp)
    '                                        releaseComObject = True
    '                                    End If
    '                                    Exit For
    '                                End If
    '                            End If
    '                        Next
    '                        If (releaseComObject = False) Then
    '                            If (myCount = wdPro.Length) Then
    '                                Marshal.FinalReleaseComObject(exitTempApp)
    '                                releaseComObject = True
    '                            Else
    '                                Marshal.ReleaseComObject(exitTempApp)
    '                                releaseComObject = True
    '                            End If

    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '        Exit Sub
    '    End Try
    'End Sub

    ''' <summary>
    ''' To exit the Unwanted WinWord processes for optimization
    ''' </summary>
    ''' <remarks></remarks>
    <System.STAThread()> Public Shared Sub KillOtherWord()

        Dim lstProcesses As List(Of Integer) = New List(Of Integer)
        lstProcesses.Clear()
        ' Dim session As Integer = Process.GetCurrentProcess().SessionId

        Dim arrProcesses As Process() = Process.GetProcessesByName("WINWORD")
        If ((IsNothing(arrProcesses)) OrElse (arrProcesses.Length = 0)) Then
            Return
        End If
        'SLR: Changed to reverse loop due to https://social.msdn.microsoft.com/Forums/en-US/f7d1749c-0cc2-4821-953c-89d518d804d1/getting-pid-of-created-ms-word-instance?forum=vblanguage
        For i As Integer = arrProcesses.Length - 1 To 0 Step -1
            If (arrProcesses(i).SessionId = LoadAndCloseWord.CurrentSessionID) Then
                'Dim ParentProcessId As Integer = WordDialogBoxBackgroundCloser.FindParentProcess.GetParentIDUsingNTQuery(arrProcesses(i).Id)
                'Dim ParentProcess As Process = Nothing
                'Try
                '    ParentProcess = Process.GetProcessById(ParentProcessId)
                'Catch

                'End Try
                'If (IsNothing(ParentProcess) OrElse ParentProcess.ProcessName.ToLower() = WordDialogBoxBackgroundCloser.MyProcessName) Then
                '    lstProcesses.Add(arrProcesses(i).Id)
                '    Exit For
                'End If
                lstProcesses.Add(arrProcesses(i).Id)
                Exit For
            End If
        Next
        arrProcesses = Nothing
        Try
            ''Get the no of word instances processes running
            If lstProcesses.Count() > 0 Then
                'LoadAndCloseWord.KillRunningWinword(Nothing, lstProcesses, True)
                'SLR:  commented because it is killing word opened by gloPM from gloEMR and by gloEMR from gloPM.
                'Kill all the Word processes that are not been used for gloEMR or External
                Dim exitTempApp As Wd.Application = Nothing
                'SLR: Commented to go with AsyncOperation since some time this hangs.
                'Try
                '    exitTempApp = CType((GetObject(Nothing, "Word.Application")), Wd.Application)
                'Catch ex As Exception

                'End Try
                AsyncReturnValue = Nothing

                Try
                    Dim AsyncWordApplicaion As New gloAsyncWordOperation()

                    ' Create the delegate.
                    Dim caller As New AsyncWordApplicationMethodCaller(AddressOf AsyncWordApplicaion.GetWordApplicationObject)
                    Dim myCallBackmethod As New AsyncCallback(AddressOf CallWordApplicationBackMethod)
                    Dim threadId As Integer = 0
                    ' Initiate the asychronous call.
                    Dim result As IAsyncResult = caller.BeginInvoke(threadId, myCallBackmethod, Nothing)
                    ' Poll while simulating work.
                    Dim noOfTimes As Integer = 10
                    While (result.IsCompleted = False) AndAlso (noOfTimes > 0)
                        Thread.Sleep(1000)
                        noOfTimes -= 1
                    End While

                    If result.IsCompleted Then
                        exitTempApp = AsyncReturnValue
                    End If

                    myCallBackmethod = Nothing
                    caller = Nothing
                    AsyncWordApplicaion = Nothing
                Catch

                End Try


                If Not (exitTempApp Is Nothing) Then
                    Dim noActiveDocument As Boolean = IsNothing(exitTempApp.Documents)
                    If (noActiveDocument = False) Then
                        noActiveDocument = exitTempApp.Documents.Count = 0
                    End If
                    If ((exitTempApp.Visible = False) OrElse noActiveDocument) Then
                        Try
                            exitTempApp.Caption = "~gloStream~" & Guid.NewGuid.ToString()
                        Catch

                        End Try
                        Dim wdAppCaption As String = Nothing
                        Try
                            wdAppCaption = exitTempApp.Caption
                        Catch

                        End Try
                        Try
                            Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
                            exitTempApp.Quit(SaveChanges:=mysaveoptions)
                        Catch ex As Exception

                        End Try
                        If (noActiveDocument) Then
                            LoadAndCloseWord.KillRunningWinword(wdAppCaption, lstProcesses, True)
                        End If
                    End If
                End If


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at function KillOtherWord of gloWord: " + ex.ToString() + " " + ex.InnerException.ToString(), False)
            Exit Sub
        Finally
            lstProcesses.Clear()
            lstProcesses = Nothing
        End Try
    End Sub
    Private Shared AsyncReturnValue As Wd.Application = Nothing
    Private Shared Sub CallWordApplicationBackMethod(ar As IAsyncResult)
        ' Retrieve the delegate.
        Dim result As AsyncResult = DirectCast(ar, AsyncResult)
        Dim caller As AsyncWordApplicationMethodCaller = DirectCast(result.AsyncDelegate, AsyncWordApplicationMethodCaller)


        '' Retrieve the format string that was passed as state 
        '' information.
        'Dim formatString As String = DirectCast(ar.AsyncState, String)

        ' Define a variable to receive the value of the out parameter.
        ' If the parameter were ref rather than out then it would have to
        ' be a class-level field so it could also be passed to BeginInvoke.
        Dim threadId As Integer = 0

        ' Call EndInvoke to retrieve the results.
        AsyncReturnValue = caller.EndInvoke(threadId, ar)

    End Sub


    ''' <summary>
    ''' To initialize the global gloEMR Word instance for Voice and Word Documents
    ''' </summary>
    ''' <remarks></remarks>
    <System.STAThread()> _
    Public Shared Sub InitializeWord()
        ''Kill the Word instances that are not properly killed
        KillOtherWord()
        ''Initialse the gloEMR Word Instance
        If Not CheckWord() Then
            _gblWordApplication = New Wd.Application()
            _gblWordApplication.Visible = False
        End If
        ' LoadAndCloseWord.beforeProcesses = LoadAndCloseWord.GetWordEntries()
    End Sub


    Public Shared Function CheckWord() As Boolean
        ' Dim session As Integer = Process.GetCurrentProcess().SessionId

        Dim arrProcesses As Process() = Process.GetProcessesByName("WINWORD")
        If ((IsNothing(arrProcesses)) OrElse (arrProcesses.Length = 0)) Then
            Return False
        End If
        'SLR: Changed to reverse loop due to https://social.msdn.microsoft.com/Forums/en-US/f7d1749c-0cc2-4821-953c-89d518d804d1/getting-pid-of-created-ms-word-instance?forum=vblanguage
        For i As Integer = arrProcesses.Length - 1 To 0 Step -1
            If (arrProcesses(i).SessionId = LoadAndCloseWord.CurrentSessionID) Then
                arrProcesses = Nothing
                Return True
            End If
        Next
        arrProcesses = Nothing
        Return False
        'Dim wdPro As System.Diagnostics.Process()
        'wdPro = System.Diagnostics.Process.GetProcessesByName("WINWORD")
        'If wdPro.Length > 0 Then
        '    wdPro = Nothing
        '    Return True
        'Else
        '    wdPro = Nothing
        '    Return False
        'End If
    End Function

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
    Public Shared Sub ExitWord()
        '' Kill the other Word instances that have not been closed properly
        KillOtherWord()
    End Sub

    '''' <summary>
    '''' To Clean up the Document for removing FormFields and Tags that does n't contain data
    '''' </summary>
    '''' <remarks></remarks>
    Public Shared Sub CleanupDocument()
        'To replcae fields
        'With CurrentDoc.Application
        '    .Selection.Find.ClearFormatting()
        '    .Selection.Find.Replacement.ClearFormatting()
        '    Try
        '        With .Selection.Find
        '            .Text = "|*|"
        '            .Replacement.Text = ""
        '            .Forward = True
        '            .Wrap = Wd.WdFindWrap.wdFindContinue
        '            .Format = False
        '            .MatchCase = False
        '            .MatchWholeWord = False
        '            .MatchAllWordForms = False
        '            .MatchSoundsLike = False
        '            .MatchWildcards = True
        '        End With
        '        .Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        '    Catch ex As Exception
        Try

            FindAndReplace(MyApp:=CurrentDoc.Application, FindText:="|*|", ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=True, MatchWholeWord:=False)
        Catch ex2 As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex2 = Nothing
        End Try
        '    End Try


        'End With
        Dim col_Tags As New Collection
        col_Tags.Add("[]")
        col_Tags.Add("[HPI]")
        col_Tags.Add("[Xray]")
        col_Tags.Add("[MRI]")
        col_Tags.Add("[PLAN]")
        For i As Int16 = 1 To col_Tags.Count
            'CurrentDoc.Application.Selection.Find.ClearFormatting()
            'CurrentDoc.Application.Selection.Find.Replacement.ClearFormatting()
            'Try
            '    With CurrentDoc.Application.Selection.Find
            '        .Text = CStr(col_Tags(i)).Trim
            '        .Replacement.Text = " "
            '        .Forward = True
            '        .Wrap = Wd.WdFindWrap.wdFindContinue
            '        .Format = False
            '        .MatchCase = False
            '        .MatchWholeWord = False
            '        .MatchWildcards = False
            '        .MatchSoundsLike = False
            '        .MatchAllWordForms = False
            '    End With

            '    CurrentDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
            'Catch ex As Exception
            Try

                FindAndReplace(MyApp:=CurrentDoc.Application, FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
            Catch ex2 As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex2 = Nothing
            End Try
            'End Try


        Next

        'CurrentDoc.Application.Selection.Find.ClearFormatting()
        'CurrentDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With CurrentDoc.Application.Selection.Find
        '    .Text = "[HPI]"
        '    .Replacement.Text = " "
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        'End With

        'CurrentDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'CurrentDoc.Application.Selection.Find.ClearFormatting()
        'CurrentDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With CurrentDoc.Application.Selection.Find
        '    .Text = "[Xray]"
        '    .Replacement.Text = " "
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        'End With

        'CurrentDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'CurrentDoc.Application.Selection.Find.ClearFormatting()
        'CurrentDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With CurrentDoc.Application.Selection.Find
        '    .Text = "[MRI]"
        '    .Replacement.Text = " "
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        'End With

        'CurrentDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'CurrentDoc.Application.Selection.Find.ClearFormatting()
        'CurrentDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With CurrentDoc.Application.Selection.Find
        '    .Text = "[PLAN]"
        '    .Replacement.Text = " "
        '    .Forward = True
        '    .Wrap = Wd.WdFindWrap.wdFindContinue
        '    .Format = False
        '    .MatchCase = False
        '    .MatchWholeWord = False
        '    .MatchWildcards = False
        '    .MatchSoundsLike = False
        '    .MatchAllWordForms = False
        'End With
        'CurrentDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        '' If gblnWordColorHighlight = True Then
        CurrentDoc.Application.Selection.Select()
        CurrentDoc.Application.Selection.WholeStory()
        CurrentDoc.Application.Selection.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
        '' End If

        'For Each cntCtrl As Wd.ContentControl In CurrentDoc.ContentControls
        '    If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
        '        cntCtrl.Delete(False)
        '    End If
        'Next
        Dim cntCtrl As Wd.ContentControl

        For iCtrl As Integer = CurrentDoc.ContentControls.Count To 1 Step -1
            Try
                cntCtrl = CurrentDoc.ContentControls(iCtrl)

                If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    cntCtrl.Delete(False)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        Next
    End Sub
    Public Shared Sub FindAndReplace(ByVal MyApp As Microsoft.Office.Interop.Word.Application, ByVal FindText As String, ByVal ReplaceWith As String, Optional ByVal Forward As Boolean = False, Optional ByVal Wrap As Integer = 1, Optional ByVal Replace As Integer = 2, Optional ByVal MatchWildCards As Boolean = False, Optional ByVal MatchWholeWord As Boolean = False)
        '  Refer below link, a bug from Microsoft..
        '  http://support2.microsoft.com/default.aspx?scid=kb;en-us;313104
        '  http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_26924442.html

        Dim MatchCase As Object = True
        ' Dim MatchWholeWord As Object = True
        '   Dim MatchWildCards As Object = False
        Dim MatchSoundsLike As Object = False
        Dim nMatchAllWordForms As Object = False
        '   Dim Forward As Object = True
        Dim Format As Object = False
        Dim MatchKashilda As Object = False
        Dim MatchDiacritics As Object = False
        Dim MatchAlefHamza As Object = False
        Dim MatchControl As Object = False
        Dim [ReadOnly] As Object = False
        Dim Visible As Object = True
        '  Dim Replace As Object = 2
        ' Dim Wrap As Object = 1
        Dim Parameters As Object() = New Object() {FindText, MatchCase, MatchWholeWord, MatchWildCards, MatchSoundsLike, nMatchAllWordForms, _
         Forward, Wrap, Format, ReplaceWith, Replace, MatchKashilda, _
         MatchDiacritics, MatchAlefHamza, MatchControl}
        MyApp.Selection.Find.[GetType]().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, Nothing, MyApp.Selection.Find, Parameters)

    End Sub
    ''Added by Mayuri : 20151030-To convert File into PDF Format
    Public Shared Function ConvertFileToPDF(ByVal strFilepath As String, ByVal AppFolderPath As String) As String
        Dim strPdfFilepath As String = ""
        If strFilepath <> "" Then
            Dim ofile As FileInfo = New FileInfo(strFilepath)
            ' Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(AppFolderPath, ofile.Extension, "MM dd yyyy hh mm ss tt") ' LoadAndCloseWord.NewDocumentName(AppFolderPath, ofile.Extension)
            If Not IsNothing(ofile) Then
                ofile = Nothing
            End If
            Dim myLoadWord As LoadAndCloseWord = New LoadAndCloseWord()
            Dim objWdDoc As Microsoft.Office.Interop.Word.Document = myLoadWord.LoadWordApplication(strFilepath)
            Try
                If (IsNothing(objWdDoc) = False) Then
                    Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                    Try
                        thisAlertLevel = objWdDoc.Application.DisplayAlerts
                        objWdDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                    Catch ex As Exception

                    End Try

                    strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(AppFolderPath, ".pdf", "MMddyyyyHHmmssffff") 'LoadAndCloseWord.NewDocumentName(AppFolderPath, ".pdf")
                    Try
                        objWdDoc.SaveAs(strPdfFilepath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)
                    Catch ex2 As Exception

                    End Try
                    Try
                        objWdDoc.Application.DisplayAlerts = thisAlertLevel
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try


            myLoadWord.CloseWordApplication(objWdDoc)


        End If
        Return strPdfFilepath
    End Function
    ''' <summary>
    ''' To Go to the Begining of the Template
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub GoToBegin()
        CurrentDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
    End Sub

    ''' <summary>
    ''' To Go to the End of the Template
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub GoToEnd()
        CurrentDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
    End Sub

    ''' <summary>
    ''' To Generate Table in Word Control
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub InsertTable(ByVal oList As ArrayList, Optional ByVal InsertAmountDue As Boolean = False)
        Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
        Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
        Dim objHeadingStyle1 As Object = Wd.WdBuiltinStyle.wdStyleHeading1

        Dim objNormalStyle As Object = Wd.WdBuiltinStyle.wdStyleNormal
        Dim objCollapseEnd As Object = Wd.WdCollapseDirection.wdCollapseEnd
        Try
            If Not IsNothing(oList) Then
                If oList.Count > 0 Then
                    With CurrentDoc.Application.Selection
                        Dim cntcontrol As Wd.ContentControl = CurrentDoc.Application.Selection.Range.ParentContentControl
                        If Not IsNothing(cntcontrol) Then
                            CurrentDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        End If

                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = oList.Count

                        ''Add the rich text Content control
                        'CurrentDoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)

                        ''Title will be Field description
                        ' CurrentDoc.Application.Selection.ParentContentControl.Title = "FlowSheet Title"

                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        ' CurrentDoc.Application.Selection.ParentContentControl.Tag = "FlowSheet Tag"

                        CurrentDoc.Application.Selection.Select()
                        'CurrentDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdWord, Count:=1)
                        Dim wdRng As Wd.Range = CurrentDoc.Application.Selection.Range
                        'wdRng.Text = "FlowSheet Title" & vbNewLine

                        'wdRng.Style = objHeadingStyle1
                        'wdRng.Collapse(objCollapseEnd)
                        'wdRng.Style = objNormalStyle
                        ''wdRange.InsertParagraph()
                        'wdRng.Collapse(objCollapseEnd)
                        'wdRng.Style = objHeadingStyle1

                        'CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)

                        'Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendTable(tb1, oList, InsertAmountDue) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateTableStyle()
                            FormatTables(style, tb1)
                            style = Nothing
                        End If
                    End With
                    CurrentDoc.ActiveWindow.SetFocus()
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CurrentDoc.Application.Selection.InsertParagraph()
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Shared Function PopulateAndExtendTable(ByVal tb1 As Wd.Table, ByVal oList As ArrayList, ByVal InsertAmountDue As Boolean) As Boolean
        Try
            Dim objMissing As Object = System.Reflection.Missing.Value
            'Dim oControl As Microsoft.Office.Interop.Word.ContentControl
            Dim oGeneralNode As New gloGeneralNode.gloGeneralNode

            Dim nrRows As Integer = 1
            Dim nrCols As Integer = oList.Count

            ''Set Column Names
            If (tb1.Rows.Count >= 1) Then
                For i As Integer = 0 To oList.Count - 1
                    tb1.Cell(1, i + 1).Range.Text = CType(oList(i), gloGeneralNode.gloGeneralNode).Text
                Next
            End If


            Dim TableName() As String
            TableName = CType(oList(0), gloGeneralNode.gloGeneralNode).Code.Split(".")
            If TableName.Length > 0 Then
                tb1.ID = TableName(0)
            End If

            '''''Move Cursor to the Table 
            CurrentDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            ''''''Move Cursor down in the Table

            tb1.Rows.Add(objMissing)  '''' new Row

            '''' Move Cursor to Newly Added Row in table
            CurrentDoc.Application.Selection.Move(Wd.WdUnits.wdRow)

            For iCol As Integer = 0 To oList.Count - 1
                oGeneralNode = CType(oList(iCol), gloGeneralNode.gloGeneralNode)
                'CurrentDoc.Application.Selection.MoveRight()


                Dim oFormField As Wd.FormField
                If (tb1.Rows.Count >= 2) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                    oFormField = tb1.Cell(2, iCol + 1).Application.Selection.FormFields.Add(tb1.Cell(2, iCol + 1).Application.Selection.Range, Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormTextInput)
                    oFormField.Result = oGeneralNode.Text
                    oFormField.HelpText = oGeneralNode.Text
                    oFormField.StatusText = oGeneralNode.Code
                End If


                'CurrentDoc.Application.Selection.TypeText(" ")
                CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                '''' Add Catergory in New Row and category Column
                'tb1.Cell(2, iCol + 1).Range.Text = oGeneralNode.Text
                '''' Add Item for Selected category 
                If (tb1.Rows.Count >= 2) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                    tb1.Cell(2, iCol + 1).Application.Selection.Select()
                End If

            Next

            '' CODE TO INSERT AMOUNT DUE ''
            If InsertAmountDue Then

                tb1.Rows.Add(objMissing)  '''' new Row
                '''' Move Cursor to Newly Added Row in table
                CurrentDoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                If (tb1.Rows.Count >= 3) AndAlso (tb1.Columns.Count >= 1) Then
                    tb1.Cell(3, tb1.Columns.Count - 1).Range.Text = "Amount Due"

                    'tb1.Cell(3, tb1.Columns.Count).Application.Selection.Select()
                    CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCell, Count:=tb1.Columns.Count - 1)

                    Dim oFormField As Wd.FormField
                    oFormField = tb1.Cell(3, tb1.Columns.Count).Application.Selection.FormFields.Add(tb1.Cell(3, tb1.Columns.Count).Application.Selection.Range, Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormTextInput)
                    oFormField.Result = "AmountDue"
                    oFormField.HelpText = "AmountDue"
                    oFormField.StatusText = "PatientStatement.AmountDue"

                    'CurrentDoc.Application.Selection.TypeText(" ")
                    CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

                    '''' Add Catergory in New Row and category Column
                    'tb1.Cell(2, iCol + 1).Range.Text = oGeneralNode.Text
                    '''' Add Item for Selected category 
                    tb1.Cell(3, tb1.Columns.Count).Application.Selection.Select()
                End If

            End If
            ''    ''

            ''''Move Cursor down in the Table

            CurrentDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            CurrentDoc.Application.Selection.InsertParagraph()
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            Return True
        Catch ex As Exception
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If CurrentDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                CurrentDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function
    Private Shared Function CreateTableStyle() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
        Dim LineStyleDouble As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        Dim LineStyleNone As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = CurrentDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = TextureNone
        evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
        FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25
        FirstRow.Font.Size = 10
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        'stylTbl.RowStripe = 1
        Return styl
    End Function
    Private Shared Sub FormatTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)
        'For Each t1 As Wd.Table In oCurDoc.Tables
        Dim objtStyl As Object = CType(tstyle, Object)
        tb1.Range.Style = tstyle
        ''to make all columns AutoFit
        For i As Integer = 0 To tb1.Columns.Count - 1
            tb1.Columns(i + 1).AutoFit()
        Next
        'Next
    End Sub


    ''Sudhir 20090416 '' TO GENERATE FLOWSHEET TABLE IN WORD CONTROL AT FORMFIELD
    Private Shared Sub InsertFlowSheetTable(ByVal dtFlowSheet As DataTable, ByVal oField As Wd.FormField)
        Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
        Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
        Dim objHeadingStyle1 As Object = Wd.WdBuiltinStyle.wdStyleHeading1

        Dim objNormalStyle As Object = Wd.WdBuiltinStyle.wdStyleNormal
        Dim objCollapseEnd As Object = Wd.WdCollapseDirection.wdCollapseEnd
        Try
            If Not IsNothing(dtFlowSheet) Then
                If dtFlowSheet.Rows.Count > 0 Then
                    With CurrentDoc.Application.Selection
                        Dim cntcontrol As Wd.ContentControl = CurrentDoc.Application.Selection.Range.ParentContentControl
                        If Not IsNothing(cntcontrol) Then
                            CurrentDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        End If

                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = dtFlowSheet.Columns.Count

                        ''Add the rich text Content control
                        '_oCurDoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)

                        ''Title will be Field description
                        ' _oCurDoc.Application.Selection.ParentContentControl.Title = "FlowSheet Title"

                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        ' _oCurDoc.Application.Selection.ParentContentControl.Tag = "FlowSheet Tag"

                        CurrentDoc.Application.Selection.Select()
                        '_oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdWord, Count:=1)
                        Dim wdRng As Wd.Range = CurrentDoc.Application.Selection.Range
                        'wdRng.Text = "FlowSheet Title" & vbNewLine

                        'wdRng.Style = objHeadingStyle1
                        'wdRng.Collapse(objCollapseEnd)
                        'wdRng.Style = objNormalStyle
                        ''wdRange.InsertParagraph()
                        'wdRng.Collapse(objCollapseEnd)
                        'wdRng.Style = objHeadingStyle1

                        'CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        'Dim tb1 As Wd.Table = oField.Range.Tables.Add(oField.Range, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendFlowSheetTable(tb1, dtFlowSheet) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateFlowSheetTableStyle()
                            FormatFlowSheetTables(style, tb1)
                            style = Nothing
                        End If
                    End With
                    CurrentDoc.ActiveWindow.SetFocus()
                End If
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CurrentDoc.Application.Selection.InsertParagraph()
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Shared Function PopulateAndExtendFlowSheetTable(ByVal tb1 As Wd.Table, ByVal dtFlowSheet As DataTable) As Boolean
        Try
            Dim objMissing As Object = System.Reflection.Missing.Value
            ' Dim oControl As Microsoft.Office.Interop.Word.ContentControl

            Dim nrRows As Integer = 1
            Dim nrCols As Integer = dtFlowSheet.Columns.Count
            Dim minCol As Integer = Math.Min(dtFlowSheet.Columns.Count, tb1.Columns.Count)
            ''Set Column Names
            If (tb1.Rows.Count >= 1) Then


                For i As Integer = 0 To minCol - 1
                    tb1.Cell(1, i + 1).Range.Text = dtFlowSheet.Columns(i).Caption
                Next
            End If

            '''''Move Cursor to the Table 
            CurrentDoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            ''''''Move Cursor down in the Table
            Dim strCellText As String
            'Dim t_Control_Type As ControlType

            For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1
                tb1.Rows.Add(objMissing)  '''' new Row
                For iCol As Integer = 0 To minCol - 1
                    strCellText = dtFlowSheet.Rows(iRow)(iCol).ToString
                    If iCol = 0 Then
                        CurrentDoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                        CurrentDoc.Application.Selection.MoveRight()

                        '''' Add Catergory in New Row and category Column
                        If (tb1.Rows.Count >= (iRow + 2)) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then


                            tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                            '''' Add Item for Selected category 
                            ' Dim oNameField As Wd.FormField
                            tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()
                        End If


                    Else '''' If the category is already add then add Item in the category
                        CurrentDoc.Application.Selection.MoveRight()
                        If (tb1.Rows.Count >= (iRow + 2)) AndAlso (tb1.Columns.Count >= (iCol + 1)) Then
                            tb1.Cell(iRow + 2, iCol + 1).Range.Text = strCellText
                            tb1.Cell(iRow + 2, iCol + 1).Application.Selection.Select()
                        End If


                    End If
                Next
            Next

            ''''Move Cursor down in the Table

            CurrentDoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            '_oCurDoc.Application.Selection.InsertParagraph()
            CurrentDoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            Return True
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If CurrentDoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                CurrentDoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return False
        End Try
    End Function
    Private Shared Function CreateFlowSheetTableStyle() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
        Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
        Dim LineStyleDouble As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble
        Dim LineStyleNone As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleNone
        Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now.Ticks)
        Dim styl As Wd.Style = CurrentDoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = TextureNone
        evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle
        FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25
        FirstRow.Font.Size = 10
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        'stylTbl.RowStripe = 1
        Return styl
    End Function
    Private Shared Sub FormatFlowSheetTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)
        'For Each t1 As Wd.Table In oCurDoc.Tables
        Dim objtStyl As Object = CType(tstyle, Object)
        tb1.Range.Style = tstyle
        ''to make all columns AutoFit
        For i As Integer = 0 To tb1.Columns.Count - 1
            tb1.Columns(i + 1).AutoFit()
        Next
        'Next
    End Sub


    Private Shared Function GetAge(ByVal BirthDate As DateTime) As String
        Dim _BDate As DateTime = BirthDate
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 AndAlso BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = Now.Year Then
            months = Now.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + Now.Month
        End If
        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 AndAlso Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 AndAlso Now.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        'Return years & " years " & months & " months " & days & " days"
        'Following code to display age in Numeric and Text
        'Dim age As New AgeDetail
        'age.Age = years & " Years " & months & " Months " & days & " Days"
        '' Cases

        ''20081119   ''Following Code to Store ExactAge in String
        Dim _AgeStr As String = ""
        If isShowAgeInDays = True AndAlso ageLimit >= DateDiff(DateInterval.Day, CType(_BDate, Date), Date.Now.Date) Then
            If years = 0 Then
                If months = 0 Then
                    If days <= 1 Then
                        _AgeStr = days & " Day"
                    Else
                        _AgeStr = days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Month " & days & " Day"
                    Else
                        _AgeStr = months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Months " & days & " Day"
                    Else
                        _AgeStr = months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Year "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Month "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Months "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Years "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Months " & days & " Days"
                    End If
                End If
            End If
        Else 'ShowAgeInDay is False OR AgeLimit less than Settings.
            If years = 0 Then
                'Added by pravin on 11/25/2008
                '                If months = 0 And months = 1 Then
                If months = 1 Then
                    _AgeStr = months & " Month"
                ElseIf months > 1 Then
                    _AgeStr = months & " Months"
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    _AgeStr = years & " Year "
                ElseIf months = 1 Then
                    _AgeStr = years & " Year " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Year " & months & " Months "
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    _AgeStr = years & " Years "
                ElseIf months = 1 Then
                    _AgeStr = years & " Years " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Years " & months & " Months "
                End If
            End If
            'Added by pravin if age in days  11/25/2008
            If years = 0 AndAlso months = 0 Then
                If days <= 1 Then
                    _AgeStr = days & " Day"
                Else
                    _AgeStr = days & " Days"
                End If
            End If
        End If
        Return _AgeStr
    End Function


    Public Shared Function CheckWordForException() As Boolean
        'Dim ofrmTest As New Global.gloWord.frmDSOTest
        'Try
        '    If ofrmTest.ShowDialog(ofrmTest.Parent) = DialogResult.Yes Then
        '        Return True
        '    Else
        '        MessageBox.Show(ofrmTest.ErrorMessage, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Return False
        '    End If
        'Catch ex As Exception
        '    Return True
        'Finally
        '    ofrmTest.Dispose()
        '    ofrmTest = Nothing
        'End Try

        Return True

    End Function

    Public Shared Function CheckActiveWord(ByVal oCurDoc As Microsoft.Office.Interop.Word.Document) As Boolean
        Try
            If oCurDoc IsNot Nothing Then
                ' If oCurDoc.Windows IsNot Nothing Then
                '  If (oCurDoc.Windows.Count > 0) Then
                Try
                    If oCurDoc.ActiveWindow IsNot Nothing Then
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try
                'Else
                '    Return False
                ' End If
                'Else
                '    Return True
                '  End If
            Else
                Return True
            End If
        Catch
            Try
                Dim oWordApp As Microsoft.Office.Interop.Word.Application = Nothing
                oWordApp = CType(Marshal.GetActiveObject("Word.Application"), Microsoft.Office.Interop.Word.Application) 'GetObject(, "Word.Application") 'SLR: Get object create objects when there is no object,
                If (IsNothing(oWordApp) = False) Then
                    oWordApp.Activate()
                    oWordApp = Nothing
                    Return False
                Else
                    Return True
                End If
            Catch
                Return True
            End Try
        End Try
    End Function

    Public Shared Function GetActiveDocumentName() As String
        Dim oWordApp As Microsoft.Office.Interop.Word.Application = Nothing
        Try
            oWordApp = CType(Marshal.GetActiveObject("Word.Application"), Microsoft.Office.Interop.Word.Application) 'GetObject(, "Word.Application") 'SLR: Get object create objects when there is no object,
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        If (IsNothing(oWordApp) = False) Then
            'If (IsNothing(oWordApp.Documents) = False) Then
            'If (oWordApp.Documents.Count > 0) Then
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
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetOpenClipboardWindow() As IntPtr
    End Function



    Public Shared Function GetProcessLockingClipboard() As Process
        Dim processId As Integer
        GetWindowThreadProcessId(GetOpenClipboardWindow(), processId)

        Return Process.GetProcessById(processId)
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetWindowText(hwnd As IntPtr, text As StringBuilder, count As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetWindowTextLength(hwnd As IntPtr) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function CloseWindow(ByVal hWnd As IntPtr) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function


    Public Shared Function GetOpenClipboardWindowText() As String
        Dim hwnd = GetOpenClipboardWindow()
        If hwnd = IntPtr.Zero Then
            Return "Unknown"
        End If
        Try
            'Dim int32Handle = hwnd.ToInt32()
            Dim ilen As Integer = GetWindowTextLength(hwnd) + 1
            If (ilen > 1) Then
                Dim sb As New System.Text.StringBuilder(ilen + 1)
                GetWindowText(hwnd, sb, ilen)

                Return sb.ToString()
            Else
                Dim processId As Integer = 0
                Dim threadId As Integer = GetWindowThreadProcessId(hwnd, processId)
                Try
                    Dim proc As Process = Process.GetProcessById(processId)
                    If (IsNothing(proc) = False) Then
                        Return proc.MainWindowTitle & "(" & proc.ProcessName & ")"
                    End If

                Catch ex As Exception

                End Try

                For Each proc As Process In Process.GetProcesses()
                    Try
                        If proc.MainWindowHandle = hwnd OrElse proc.Id = processId Then
                            Return proc.MainWindowTitle & "(" & proc.ProcessName & ")"
                        End If
                    Catch ex As Exception

                    End Try

                Next

                Return "Unknown With Handle: " & hwnd.ToString()
            End If

        Catch ex As Exception
            Return "Exception Occured in Clipboard: " & ex.ToString()
        Finally
            Try
                CloseWindow(hwnd)
            Catch ex As Exception

            End Try
        End Try

    End Function

    Public Shared Sub GetClipboardData()

        If (Not IsNothing(Clipboard_Backup)) Then
            Try
                Clipboard_Backup.Clear()
                Clipboard_Backup = Nothing
            Catch ex As Exception
                'MessageBox.Show("Error in Clipboard Clearing operation " + ex.ToString())
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        End If
        Dim strException As String = ""
        Dim dataObject As DataObject = Nothing
        Try
            dataObject = GetClipBoardDataWithRetry(5, strException)
            If (IsNothing(dataObject) AndAlso (String.IsNullOrEmpty(strException) = False)) Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, strException, gloAuditTrail.ActivityOutCome.Failure)
            End If

        Catch ex As Exception
            '  MessageBox.Show("Error while getting data " + ex.ToString())
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        If IsNothing(dataObject) Then
            Exit Sub
        End If

        Try
            'Bug #92173: 00001055: Fax number Copy/Paste not working 
            'Added a Text format check to copy and paste simple text 
            Clipboard_Backup = New Dictionary(Of String, Object)()
            If dataObject.GetDataPresent(DataFormats.Rtf) Then
                'Clipboard_Backup = New Dictionary(Of String, Object)()
                Try
                    Clipboard_Backup.Add(DataFormats.Rtf, dataObject.GetData(DataFormats.Rtf))

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ' MessageBox.Show("Error while adding data " + ex.ToString())
                    ex = Nothing
                End Try
            ElseIf dataObject.GetDataPresent(DataFormats.Text) Then
                Try
                    Clipboard_Backup.Add(DataFormats.Text, dataObject.GetData(DataFormats.Text))

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ' MessageBox.Show("Error while adding data " + ex.ToString())
                    ex = Nothing
                End Try

            End If
        Catch ex2 As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex2.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex2 = Nothing
        End Try



        'Dim dict As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
        'For Each format As Object In dataObject.GetFormats()
        '    Try
        '        If dataObject.GetDataPresent(format) = True Then
        '            'If format = "FileName" Or format = "FileNameW" Or format = "FileNameW" Or format = "EnhancedMetafile" Or format = "RenPrivateSourceFolder" Or format = "RenPrivateMessages" Or format = "RenPrivateItem" Then
        '            If format = "EnhancedMetafile" Then
        '                '    ''TODO : Need to add Logic for EnhancedMetafile
        '            ElseIf format = "RenPrivateSourceFolder" Or format = "RenPrivateMessages" Or format = "RenPrivateItem" Then
        '                dict.Clear()
        '                dict = Nothing
        '                Exit For
        '            ElseIf format = "Rich Text Format" Then
        '                Try
        '                    dict.Add(format, dataObject.GetData(format))
        '                Catch ex As Exception

        '                End Try

        '            End If
        '        End If
        '    Catch ex As Exception
        '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    End Try
        'Next
        'Clipboard_Backup = dict
    End Sub

    Public Shared Sub SetClipboardData()
        If IsNothing(Clipboard_Backup) Then
            Exit Sub
        End If
        'MessageBox.Show("About to set Clipboard")
        ' Dim dict As Dictionary(Of String, Object) = Clipboard_Backup
        Dim dataObject = New DataObject()

        For Each kvp As Object In Clipboard_Backup
            Try
                dataObject.SetData(kvp.Key, kvp.Value)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                '  MessageBox.Show("Error while setting data " + ex.ToString())
                ex = Nothing
            End Try
        Next
        Try
            Clipboard.SetDataObject(dataObject, True, 3, 1000) 'Retry for 3 times with 1000 milliseconds
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show("Error while setting data object " + ex.ToString())
            ex = Nothing
        End Try
        dataObject = Nothing
    End Sub


End Class
Public Class gloAsyncWordOperation
    ' The method to be executed asynchronously.
    Public Function GetWordApplicationObject(ByRef threadId As Integer) As Wd.Application
        threadId = System.Threading.Thread.CurrentThread.ManagedThreadId
        Try

            Dim exitTempApp As Wd.Application

            exitTempApp = CType((GetObject(Nothing, "Word.Application")), Wd.Application)
            Return exitTempApp

        Catch ex As Exception
            Return Nothing

        End Try
        Return Nothing
    End Function

End Class
Public Delegate Function AsyncWordApplicationMethodCaller(ByRef threadId As Integer) As Wd.Application

Public Class LoadAndCloseWord
    '''<summary>
    ''' Class containing the IOleMessageFilter
    ''' thread error-handling functions.
    '''</summary>
    Friend Class gloMessageFilter
        Implements IOleMessageFilter

        Private iWaitTimeLimit As Integer = 10000 'Wait for 10 secs. Increase this incase you get busy calls often. in 8033 it was infinite.

        '''<summary>Start the filter.</summary>
        Public Shared Sub Register()
            Dim staThread As Boolean = False
            Try
                System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA)
                staThread = True
            Catch ex As Exception
                staThread = False
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            If (staThread) Then
                Try
                    Dim newFilter As IOleMessageFilter = New gloMessageFilter()
                    Dim oldFilter As IOleMessageFilter = Nothing
                    CoRegisterMessageFilter(newFilter, oldFilter)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If


        End Sub

        '''<summary>Done with the filter, close it.</summary>
        Public Shared Sub Revoke()
            Dim staThread As Boolean = False
            Try
                System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA)
                staThread = True
            Catch ex As Exception
                staThread = False
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            If (staThread) Then
                Try
                    Dim oldFilter As IOleMessageFilter = Nothing
                    CoRegisterMessageFilter(Nothing, oldFilter)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
            End If
        End Sub


        ' IOleMessageFilter functions.

        '''<summary>Handle incoming thread requests.</summary>
        Private Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As System.IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As System.IntPtr) As Integer Implements IOleMessageFilter.HandleInComingCall
            'Return the flag SERVERCALL_ISHANDLED.
            Return 0
        End Function

        '''<summary>Thread call was rejected, so try again.</summary>
        Private Function RetryRejectedCall(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer Implements IOleMessageFilter.RetryRejectedCall
            If dwRejectType = 2 Then
                ' flag = SERVERCALL_RETRYLATER.
                ' Retry the thread call immediately if return >=0 & 
                ' <100.
                If (iWaitTimeLimit > 0) Then
                    iWaitTimeLimit -= 1
                    Return 99
                End If

            End If
            ' Waited for mytimelimit; Too busy; cancel call.
            Return -1
        End Function

        Private Function MessagePending(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer Implements IOleMessageFilter.MessagePending
            'Return the flag PENDINGMSG_WAITDEFPROCESS.
            Return 2
        End Function

        '''<summary>Implement the IOleMessageFilter interface.</summary>
        <DllImport("Ole32.dll")> _
        Private Shared Function CoRegisterMessageFilter(ByVal newFilter As IOleMessageFilter, ByRef oldFilter As IOleMessageFilter) As Integer
        End Function
    End Class


    <ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Interface IOleMessageFilter
        <PreserveSig()> _
        Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As IntPtr) As Integer

        <PreserveSig()> _
        Function RetryRejectedCall(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer

        <PreserveSig()> _
        Function MessagePending(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer
    End Interface
    Private wdApplication As Wd.Application = Nothing
    Private Shared _isTSPrintDialogOpen As Boolean = False
    Private Shared _isTSPrinterSelectionOpen As Boolean = False
    Private Shared _popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing
    Private Shared _showPopup As Boolean = True

    Public Shared Sub KillRunningWinword(ByVal wdAppCaption As String, ByVal refProcesses As List(Of Integer), ByVal bIsError As Boolean)
        Application.DoEvents()

        If (IsNothing(refProcesses)) Then
            Return
        End If

        For Each Proc As Integer In refProcesses 'Process.GetProcessesByName("WINWORD")
            ' If (Proc.SessionId = LoadAndCloseWord.CurrentSessionID) Then

            Dim ProcExists As Process = Nothing

            Try
                ProcExists = Process.GetProcessById(Proc)
            Catch ex As ArgumentException
                'The process specified by the processId parameter is not running. The identifier might be expired.
            End Try


            If (Not IsNothing(ProcExists)) AndAlso ((String.IsNullOrEmpty(wdAppCaption)) OrElse ((Not String.IsNullOrEmpty(ProcExists.MainWindowTitle)) AndAlso ProcExists.MainWindowTitle.Contains(wdAppCaption))) Then

                Try
                    If bIsError Then

                        Try

                            ProcExists.CloseMainWindow()

                        Catch ex As Exception

                        End Try
                        Application.DoEvents()
                        Try

                            ProcExists.Close()


                        Catch ex As Exception

                        End Try
                        Application.DoEvents()
                        Try

                            ProcExists.Kill()
                        Catch ex As Exception


                        End Try
                        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Due to Error Forcefully Killing application Succeded "))
                    Else

                        Try
                            ProcExists.CloseMainWindow()

                            ProcExists.Close()

                            ProcExists.Kill()
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Due to application still opened, Closing application  : {0}  ", ex.ToString()))

                            ex = Nothing
                        End Try
                        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Due to application still in opened state, Closing and killing application Succeded "))
                        ' WordDialogBoxBackgroundCloser.CloseThisWindow(proc.Id)
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Killing application  : {0}  ", ex.ToString()))

                    ex = Nothing
                End Try

                Exit For
            End If
            ProcExists = Nothing
            ' End If
        Next


    End Sub
    Public Shared Function GetWordEntries() As List(Of Integer)
        Dim refProcesses As List(Of Integer) = New List(Of Integer)
        refProcesses.Clear()
        Dim arrProcess As Process() = Process.GetProcessesByName("WINWORD")
        If ((IsNothing(arrProcess)) OrElse (arrProcess.Length = 0)) Then
            Return refProcesses
        End If

        For i As Integer = arrProcess.Length - 1 To 0 Step -1
            If (arrProcess(i).SessionId = CurrentSessionID) Then
                refProcesses.Add(arrProcess(i).Id)
            End If
        Next
        arrProcess = Nothing
        Return refProcesses
    End Function
    Public Sub GetWordEntriesNotIn(ByRef refProcesses As List(Of Integer))
        lstProcesses.Clear()
        Dim arrProcess As Process() = Process.GetProcessesByName("WINWORD")
        If ((IsNothing(arrProcess)) OrElse (arrProcess.Length = 0)) Then
            Return
        End If

        For i As Integer = arrProcess.Length - 1 To 0 Step -1
            If (arrProcess(i).SessionId = CurrentSessionID) Then
                If (IsNothing(refProcesses) OrElse (Not refProcesses.Contains(arrProcess(i).Id))) Then
                    lstProcesses.Add(arrProcess(i).Id)
                End If
            End If
        Next
        arrProcess = Nothing

    End Sub
    Public Function CheckWordApplicationLocked() As Boolean

        Try
            If (IsNothing(wdApplication) = False) Then

                If (IsNothing(wdApplication.Documents) = False) Then
                    Return wdApplication.Documents.Count >= 1
                Else
                    Return False
                End If
            Else
                Return False
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            CheckWordApplicationLocked = False
        End Try

    End Function
    Private lstProcesses As List(Of Integer) = New List(Of Integer)

    Public Shared CurrentSessionID As Integer = Process.GetCurrentProcess().SessionId

    Public Function LoadWordApplication(ByVal strFileName As String, Optional ByVal bOpen As Boolean = True, Optional ByVal bVisible As Boolean = False) As Wd.Document

        LoadApplicationOnly(bVisible)
        If IsNothing(wdApplication) Then
            Return Nothing
        End If
        Dim wdDoc As Wd.Document = Nothing
        Try
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                thisAlertLevel = wdApplication.DisplayAlerts
                wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            Catch ex As Exception

            End Try
            If (IsNothing(strFileName) = False) Then
                If (bOpen) Then
                    wdDoc = wdApplication.Documents.Open(strFileName)
                    If (IsNothing(wdDoc) = True) AndAlso (wdApplication.Documents.Count >= 1) Then 'SLR: if ROT is locked by dso or external word
                        Try
                            wdApplication = Nothing
                            LoadApplicationOnly(bVisible)
                            wdDoc = wdApplication.Documents.Open(strFileName)
                        Catch ex2 As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex2 = Nothing
                            WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                        End Try

                    End If
                Else
                    wdDoc = wdApplication.Documents.Add(strFileName)
                    If (IsNothing(wdDoc) = True) AndAlso (wdApplication.Documents.Count >= 1) Then 'SLR: if ROT is locked by dso or external word
                        Try
                            wdApplication = Nothing
                            LoadApplicationOnly(bVisible)
                            wdDoc = wdApplication.Documents.Add(strFileName)
                        Catch ex2 As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex2 = Nothing
                            WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                        End Try

                    End If
                End If
            Else
                Dim objmissing As Object = System.Reflection.Missing.Value
                wdDoc = wdApplication.Documents.Add(objmissing, objmissing, objmissing)
                If (IsNothing(wdDoc) = True) AndAlso (wdApplication.Documents.Count >= 1) Then 'SLR: if ROT is locked by dso or external word
                    Try
                        wdApplication = Nothing
                        LoadApplicationOnly(bVisible)
                        wdDoc = wdApplication.Documents.Add(objmissing, objmissing, objmissing)
                    Catch ex2 As Exception
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        ex2 = Nothing
                        WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                    End Try

                End If

            End If
            If (IsNothing(wdApplication) = False) Then
                Try
                    wdApplication.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
        End Try
        'Try
        '    wdApplication.Caption = "~gloStream~" & Guid.NewGuid.ToString()
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '    ex = Nothing
        'End Try
        'Incident #80398: 00046635 Added code changes for Printing Finished Exam
        If (IsNothing(wdDoc) = False) Then
            Try
                wdDoc.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView
                'wdDoc.ActiveWindow.View.ReadingLayoutAllowEditing = True
                wdDoc.ActiveWindow.View.ReadingLayout = False
                wdDoc.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
            End Try
            Try
                wdDoc.ActiveWindow.SetFocus()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
            End Try

            Try
                If wdDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    wdDoc.Unprotect()
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
            End Try

            Return wdDoc
        End If


        Return Nothing
    End Function
    Public Sub LoadApplicationOnly(Optional ByVal bVisible As Boolean = False)
        Try
            If (IsNothing(wdApplication)) Then
                Dim beforeProcesses As List(Of Integer) = GetWordEntries()
                wdApplication = New Wd.Application()
                GetWordEntriesNotIn(beforeProcesses)
                'Dim afterProcesses As List(Of Integer) = GetWordEntries()
                'lstProcesses.Clear()
                'For Each Proc As Integer In afterProcesses
                '    If (Not beforeProcesses.Contains(Proc)) Then
                '        lstProcesses.Add(Proc)
                '    End If
                'Next
                'afterProcesses = Nothing
                beforeProcesses = Nothing

                Try
                    wdApplication.Caption = "~gloStream_" & Guid.NewGuid.ToString()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

        If (IsNothing(wdApplication) = False) Then
            Try
                If (bVisible) Then
                    wdApplication.Visible = bVisible
                Else
                    '  wdApplication.Visible = True

                    wdApplication.ScreenUpdating = True
                    ' wdApplication.Resize(620, 340)
                    wdApplication.Visible = bVisible
                    ' wdApplication.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                    ' wdApplication.ActiveWindow.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try


        End If


    End Sub
    Public Function FilePrinterSetup(ByVal sPrinter As String, ByVal iDoNotSetAsSysDefault As Integer) As Boolean
        If (IsNothing(wdApplication)) Then
            LoadApplicationOnly()
            'wdApplication = New Wd.Application()
            'Try
            '    wdApplication.Caption = "~gloStream_" & Guid.NewGuid.ToString()
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            'End Try
        End If
        If (IsNothing(wdApplication) = False) Then
            wdApplication.Visible = False
            wdApplication.WordBasic.FilePrintSetup(Printer:=sPrinter, DoNotSetAsSysDefault:=iDoNotSetAsSysDefault)
            Return True
        End If
        Return False
    End Function
    Public Shared Function FilePrinterSetupToApplication(ByRef doc As Wd.Document, ByVal sPrinter As String, ByVal iDoNotSetAsSysDefault As Integer) As Boolean
        If (IsNothing(doc) = False) Then
            doc.Application.WordBasic.FilePrintSetup(Printer:=sPrinter, DoNotSetAsSysDefault:=iDoNotSetAsSysDefault)
            Return True
        End If
        Return False
    End Function
    'Private createdNew As Boolean = False
    'Public Function SetWordApplication(ByRef thisWordApplication As Wd.Application) As Wd.Application
    '    ' CloseApplicationOnly()
    '    Dim oldApplicaiton As Wd.Application = wdApplication

    '    If (IsNothing(thisWordApplication)) Then
    '        thisWordApplication = New Wd.Application()
    '        Try
    '            thisWordApplication.Caption = "~gloStream_" & Guid.NewGuid.ToString()
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '            ex = Nothing
    '        End Try

    '        createdNew = True
    '    Else
    '        setLocally = False
    '    End If
    '    wdApplication = thisWordApplication
    '    Try
    '        If (IsNothing(wdApplication) = False) Then
    '            wdApplication.Visible = False

    '        End If

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    End Try


    '    Return oldApplicaiton
    'End Function
    'Public Function ResetWordApplication(ByRef thisWordApplication As Wd.Application) As Boolean
    '    ' CloseApplicationOnly()
    '    If (createdNew) Then
    '        CloseApplicationOnly()
    '        createdNew = False
    '    Else
    '        setLocally = True
    '    End If
    '    wdApplication = thisWordApplication
    '    Try
    '        If (IsNothing(wdApplication) = False) Then
    '            wdApplication.Visible = False
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        ex = Nothing
    '    End Try

    '    Return False
    'End Function
    '''' <summary>
    '''' To Clean up the Document for removing FormFields and Tags that does n't contan data
    '''' </summary>
    '''' <remarks></remarks>
    Public Shared Sub CleanupDoc(ByRef wdDoc As Wd.Document)
        Dim strtags As String()
        Dim strtagsCompare As String()
        If (IsNothing(wdDoc)) Then
            gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Entered in CleanupDoc With wdDoc Nothing"))
            Exit Sub
        End If
        Dim wdDocName As String = wdDoc.FullName
        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Stared CleanupDoc on : {0}  ", wdDocName))

        Dim iobjField As IEnumerator = wdDoc.FormFields.GetEnumerator()
        While iobjField.MoveNext
            Dim objField As Wd.FormField = iobjField.Current
            Try

                If (Not IsNothing(objField)) Then

                    If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                        Try


                            objField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        Try

                            Dim myHelptextString As String = "|" & objField.HelpText.ToString() & "|"
                            Dim myResultTextString As String = objField.Result
                            '' If objField.HelpText = objField.Result Then ''Old
                            If myHelptextString = myResultTextString Then '' New 
                                objField.Result = ""
                                objField.Delete()
                            Else

                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                    End If
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End While
        
        
        ''//To replace the special tags
        'Dim col_Tags As New Collection
        'col_Tags.Add("[]")
        'col_Tags.Add("[HPI]")
        'col_Tags.Add("[Xray]")
        'col_Tags.Add("[MRI]")
        'col_Tags.Add("[PLAN]")

        ReDim Preserve strtags(4)
        ''//To replace the special tags
        strtags(0) = "[]"
        strtags(1) = "[HPI]"
        strtags(2) = "[Xray]"
        strtags(3) = "[MRI]"
        strtags(4) = "[PLAN]"
        ReDim Preserve strtagsCompare(strtags.Length - 1)
        For LenIndex As Integer = 0 To strtags.Length - 1
            strtagsCompare(LenIndex) = strtags(LenIndex).ToUpper()
        Next
        ''added for incident CAS-07812-N2K3R9
        Dim dtcat As DataTable = FillTagsCategory()
        If (Not IsNothing(dtcat)) Then

            For Each dr As DataRow In dtcat.Rows
                Dim myString As String = dr(0).ToString()

                If (String.IsNullOrEmpty(myString) = False) Then
                    Dim myStringUp As String = myString.ToUpper()
                    If Array.IndexOf(strtagsCompare, myStringUp) = -1 Then

                        ''   String.Compare(    StringComparison.CurrentCultureIgnoreCase
                        ReDim Preserve strtags(strtags.Length)
                        ReDim Preserve strtagsCompare(strtagsCompare.Length)
                        strtags(strtags.Length - 1) = myString
                        strtagsCompare(strtagsCompare.Length - 1) = myStringUp
                    End If
                End If
            Next

        End If


        For i As Int16 = 0 To strtags.Length - 1

            'Try
            '     _oCurDoc.Application.Selection.Find.Execute(FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)

            ' Catch ex As Exception
            Try

                FindAndReplace(wdApp:=wdDoc.Application, FindText:=CStr(strtags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
            Catch ex2 As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex2 = Nothing
            End Try
            'End Try
        Next

        Array.Clear(strtags, 0, strtags.Length)
        Array.Clear(strtagsCompare, 0, strtagsCompare.Length)
        strtags = Nothing
        strtagsCompare = Nothing
        If Not IsNothing(dtcat) Then
            dtcat.Rows.Clear()
            dtcat.Dispose()
            dtcat = Nothing
        End If

    


        Dim iCtrl As IEnumerator = wdDoc.ContentControls.GetEnumerator()
        While iCtrl.MoveNext
            Try
                Dim cntCtrl As Wd.ContentControl = iCtrl.Current

                If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    cntCtrl.Delete(False)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
        End While


        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Completed CleanupDoc on : {0}  ", wdDocName))
    End Sub


    Public Shared Function FillTagsCategory() As System.Data.DataTable


        Dim connMain As New SqlConnection
        Dim oResultTable As DataTable
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter

        connMain.ConnectionString = gloPMGlobal.DatabaseConnectionString ''GetConnectionString()

        Dim strprocname As String = "gsp_getTagsCategory"
        cmd = New SqlCommand(strprocname, connMain)

        cmd.CommandType = CommandType.StoredProcedure
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        oResultTable = New DataTable

        Try

            connMain.Open()
            da.Fill(oResultTable)



            If Not IsNothing(oResultTable) Then
                Return oResultTable
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ex = Nothing
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If

            If (connMain.State = ConnectionState.Open) Then
                connMain.Close()
            End If
            If IsNothing(connMain) = False Then
                connMain.Dispose()
                connMain = Nothing
            End If

        End Try


    End Function



    ''MatchCase parameter default set to false so that no need to pass it from all the screens
    Public Shared Function FindAndReplace(ByVal wdApp As Microsoft.Office.Interop.Word.Application, ByVal FindText As String, ByVal ReplaceWith As String, Optional ByVal Forward As Boolean = False, Optional ByVal Wrap As Integer = 1, Optional ByVal Replace As Integer = 2, Optional ByVal MatchWildCards As Boolean = False, Optional ByVal MatchWholeWord As Boolean = False, Optional ByVal Format As Boolean = False, Optional ByVal MatchCase As Object = False) As Boolean
        '  Refer below link, a bug from Microsoft..
        '  http://support2.microsoft.com/default.aspx?scid=kb;en-us;313104
        '  http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_26924442.html

        'Dim MatchCase As Object = True
        ' Dim MatchWholeWord As Object = True
        '   Dim MatchWildCards As Object = False
        Dim MatchSoundsLike As Object = False
        Dim nMatchAllWordForms As Object = False
        '   Dim Forward As Object = True
        '    Dim Format As Object = False
        Dim MatchKashilda As Object = False
        Dim MatchDiacritics As Object = False
        Dim MatchAlefHamza As Object = False
        Dim MatchControl As Object = False
        Dim [ReadOnly] As Object = False
        Dim Visible As Object = True
        '  Dim Replace As Object = 2
        ' Dim Wrap As Object = 1
        Dim Parameters As Object() = New Object() {FindText, MatchCase, MatchWholeWord, MatchWildCards, MatchSoundsLike, nMatchAllWordForms, _
         Forward, Wrap, Format, ReplaceWith, Replace, MatchKashilda, _
         MatchDiacritics, MatchAlefHamza, MatchControl}
        Return wdApp.Selection.Find.[GetType]().InvokeMember("Execute", System.Reflection.BindingFlags.InvokeMethod, Nothing, wdApp.Selection.Find, Parameters)

    End Function
    Public Shared Function ChangeToEditView(ByRef wdDoc As Wd.Document, ByRef myType As Wd.WdViewType) As Boolean
        If (IsNothing(wdDoc)) Then
            Return False
        End If
        Dim myLayout As Boolean = False

        Try
            myLayout = wdDoc.ActiveWindow.View.ReadingLayout
        Catch ex As Exception

        End Try
        Try

            myType = wdDoc.ActiveWindow.ActivePane.View.Type
            wdDoc.ActiveWindow.View.ReadingLayout = False
            If myType <> Wd.WdViewType.wdPrintView Then
                wdDoc.ActiveWindow.View.WrapToWindow = True
                wdDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
        Catch ex As Exception

        End Try

        Return myLayout
    End Function
    Public Shared Sub RestoreFromEditView(ByRef wdDoc As Wd.Document, ByRef myType As Wd.WdViewType, ByVal myLayout As Boolean)
        If (IsNothing(wdDoc)) Then
            Exit Sub
        End If
        Try
            If (IsNothing(myType) = False) Then
                If myType <> Wd.WdViewType.wdPrintView Then
                    wdDoc.ActiveWindow.ActivePane.View.Type = myType
                End If
            End If


        Catch ex As Exception

        End Try
        Try
            wdDoc.ActiveWindow.View.ReadingLayout = myLayout
        Catch ex As Exception
        End Try
    End Sub
    Public Function CloseWordApplication(ByRef wdDoc As Wd.Document, Optional ByVal wSaveChanges As Wd.WdSaveOptions = Wd.WdSaveOptions.wdDoNotSaveChanges) As Boolean
        CloseWordOnly(wdDoc, wSaveChanges)
        CloseApplicationOnly(wSaveChanges)
        Return True
    End Function
    Public Function CloseApplicationOnly(Optional ByVal wSaveChanges As Wd.WdSaveOptions = Wd.WdSaveOptions.wdDoNotSaveChanges) As Boolean

        If (IsNothing(wdApplication) = False) Then
            Dim wdAppCaption As String = Nothing
            Try
                wdAppCaption = wdApplication.Caption
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Failed while getting Caption  : {0}  ", ex.ToString()))
                ex = Nothing
            End Try

            Dim bIsError As Boolean = False
            '    Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                '       thisAlertLevel = wdApplication.DisplayAlerts
                wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            Catch ex As Exception

            End Try

            gloAuditTrail.gloAuditTrail.PrintLog(String.Format("before quiting word application"))
            Try
                wdApplication.Quit(SaveChanges:=wSaveChanges)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Failed while quiting application  : {0}  ", ex.ToString()))

                ex = Nothing
                bIsError = True
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("after quiting word application"))
                WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Succeed Closing Word Dialogs for word application"))
            End Try

            'SLR: not needed since application is already quit
            'Try
            '    wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            'Catch ex As Exception

            'End Try

            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wdApplication)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Failed while releasing comobject  : {0}  ", ex.ToString()))

                ex = Nothing
                bIsError = True
            End Try
            gloAuditTrail.gloAuditTrail.PrintLog(String.Format("release comobject of word application"))

            wdApplication = Nothing

            'Try
            '    GC.Collect()
            '    GC.WaitForPendingFinalizers()
            '    GC.Collect()
            '    GC.WaitForPendingFinalizers()
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            '    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Failed while GC Collect  : {0}  ", ex.ToString()))

            '    ex = Nothing
            'End Try
            'gloAuditTrail.gloAuditTrail.PrintLog(String.Format("GC Collect of word application"))
            Try
                'If bIsError Then
                KillRunningWinword(wdAppCaption, lstProcesses, bIsError)
                'End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("release proc kill of word application : {0} ", ex.ToString))
                ex = Nothing
            End Try
            gloAuditTrail.gloAuditTrail.PrintLog(String.Format("completed proce kill of word application"))
            'For Each Proc As Process In lstProcesses
            '    If (beforeProcesses.Contains(Proc)) Then
            '        beforeProcesses.Remove(Proc)
            '    End If
            'Next
            lstProcesses.Clear()
            Return True
        End If

        Return False
    End Function

    Public Function CloseWordOnly(ByRef wdDoc As Wd.Document, Optional ByVal wSaveChanges As Wd.WdSaveOptions = Wd.WdSaveOptions.wdDoNotSaveChanges) As Boolean
        Dim wddocname As String = String.Empty
        If (IsNothing(wdDoc) = False) Then

            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

            Try
                wddocname = wdDoc.FullName
                If (IsNothing(wdApplication) = False) Then
                    Try
                        thisAlertLevel = wdApplication.DisplayAlerts
                        wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                    Catch ex As Exception

                    End Try
                End If
                wdDoc.Close(SaveChanges:=wSaveChanges)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Succeed Closing Word Normal for file after Printout : {0}  ", wddocname))
                WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Succeed Closing Word Dialogs for file after Printout : {0}  ", wddocname))
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Failed with Error Closing Word Normal for file after Printout : {0}  ", wddocname))
                ex = Nothing
            End Try
            If (IsNothing(wdApplication) = False) Then
                Try
                    wdApplication.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try

                'Try
                '    wdApplication.Caption = "~gloStream~" & Guid.NewGuid.ToString()
                'Catch ex As Exception
                '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                '    ex = Nothing
                'End Try
            End If
            Try
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Releasing with Marshel Com object for word file : {0}  ", wddocname))
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wdDoc)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Released with Marshel Com object for word file : {0}  ", wddocname))
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Crashed with errror on Releasing with Marshel Com object for word file : {0}  ", wddocname))
                ex = Nothing
            End Try


            wdDoc = Nothing
        End If


        Return True
    End Function
    
    Public Function HTMLToDocx(downloadUrl As String, outputName As String) As Wd.Document

        ' Code to save web content in doc format
        Dim doc As Wd.Document = Nothing
        Try
            ' Return if no download Url is entered

            If String.IsNullOrEmpty(downloadUrl) Then
                Return doc
            End If

            ' Download Url content using WebClient

            Dim request As New Net.WebClient()

            request.UseDefaultCredentials = True

            Dim fileContent As Byte() = request.DownloadData(downloadUrl)
            Dim contentType As String = request.ResponseHeaders("content-type")
            Dim htmlName As String = Path.GetDirectoryName(outputName) + Path.GetFileName(outputName) + ".htm"
            If File.Exists(htmlName) Then
                System.IO.File.Delete(htmlName)
            End If
            System.IO.File.WriteAllBytes(htmlName, fileContent)

            If File.Exists(outputName) Then
                System.IO.File.Delete(outputName)
            End If

            If (IsNothing(wdApplication)) Then
                LoadApplicationOnly()
            End If
            If (IsNothing(wdApplication) = False) Then

                Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

                Try
                    thisAlertLevel = wdApplication.DisplayAlerts
                    wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex As Exception

                End Try

                wdApplication.Visible = False

                Dim visible As Object = False

                ' Open html file and save it as docx file

                doc = wdApplication.Documents.Open(htmlName, Type.Missing, False, Type.Missing, Type.Missing, Type.Missing, _
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, visible, _
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing)

                doc.SaveAs(outputName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)

                If (IsNothing(wdApplication) = False) Then
                    Try
                        wdApplication.DisplayAlerts = thisAlertLevel
                    Catch ex As Exception

                    End Try

                End If
                Try
                    System.IO.File.Delete(htmlName)
                Catch ex As Exception

                End Try
            End If

        Catch
            Throw
        End Try
        Return doc
    End Function
    Public Function WebToDocx(gloWebBrowser As WebBrowser, outputName As String) As Wd.Document

        ' Code to save web content in doc format
        Dim doc As Wd.Document = Nothing
        Try

            Dim htmlName As String = Path.Combine(Path.GetDirectoryName(outputName), Path.GetFileName(outputName) + ".mht")
            If File.Exists(htmlName) Then
                System.IO.File.Delete(htmlName)
            End If

            gloGlobal.SaveWebPage.SaveWebPageAsMHT(gloWebBrowser, htmlName)

            If File.Exists(outputName) Then
                System.IO.File.Delete(outputName)
            End If

            If (IsNothing(wdApplication)) Then
                LoadApplicationOnly()
            End If
            If (IsNothing(wdApplication) = False) Then

                Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

                Try
                    thisAlertLevel = wdApplication.DisplayAlerts
                    wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex As Exception

                End Try

                wdApplication.Visible = False

                Dim visible As Object = False

                ' Open html file and save it as docx file
                Try
                    'doc = wdApplication.Documents.Open(htmlName, Type.Missing, False, Type.Missing, Type.Missing, Type.Missing, _
                    '                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, visible, _
                    '                Type.Missing, Type.Missing, Type.Missing, Type.Missing)
                    'doc.ActiveWindow.View.Type = Interop.Word.WdViewType.wdPrintView

                    doc = LoadWordApplication(htmlName, True, False)
                    If (IsNothing(doc) = False) Then
                        Try
                            doc.PageSetup.Orientation = Wd.WdOrientation.wdOrientPortrait
                        Catch ex2 As Exception

                        End Try

                        doc.SaveAs(outputName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault)
                    End If
                Catch ex As Exception

                End Try


                If (IsNothing(wdApplication) = False) Then
                    Try
                        wdApplication.DisplayAlerts = thisAlertLevel
                    Catch ex As Exception

                    End Try

                End If
                Try
                    System.IO.File.Delete(htmlName)
                Catch ex As Exception

                End Try
            End If

        Catch
            Throw
        End Try
        Return doc
    End Function
    Public Function SaveCurrentWord(ByRef thisDoc As Wd.Document, ByVal thisPath As String) As String
        Dim wddocname As String = String.Empty
        If (IsNothing(thisDoc) = False) Then
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                wddocname = thisDoc.FullName
                If (IsNothing(wdApplication) = False) Then
                    Try
                        thisAlertLevel = wdApplication.DisplayAlerts
                        wdApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                    Catch ex As Exception

                    End Try
                End If
                Try
                    If thisDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                        thisDoc.Unprotect()
                    End If
                Catch ex1 As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex1.ToString, gloAuditTrail.ActivityOutCome.Failure)

                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Exception while unprotecting : {0}  ", wddocname))
                End Try
                thisDoc.Save()
            Catch ex2 As Exception
                Try
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)

                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Exception while Saving : {0}  ", wddocname))
                    ClearWordGarbage(wdApplication)
                Catch ex3 As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex3.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Exception while Clearing Word Garbage : {0}  ", wddocname))
                End Try

                Try
                    wddocname = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
                    thisDoc.SaveAs(wddocname, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch ex4 As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex4.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Exception while Saveas Also : {0}  ", wddocname))
                    SaveWord(thisDoc, wdApplication)
                    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Completed SaveWord : {0}  ", wddocname))
                End Try

            End Try
            If (IsNothing(wdApplication) = False) Then
                Try
                    wdApplication.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try
            End If
        End If
        Return wddocname
    End Function
    Shared Function LockFields(ByRef currDoc As Wd.Document) As Integer
        ' Dim rngStory As Wd.Range
        ' Dim lngLink As Long

        'lngLink = currDoc.Sections(1).Headers(1).Range.StoryType
        Dim lockCount = 0
        For Each rngStory As Wd.Range In currDoc.StoryRanges
            'Iterate through all linked stories
            Do
                On Error Resume Next
                'Call actionable procedure
                FieldsLockAll(rngStory)
                lockCount = lockCount + 1
                Select Case rngStory.StoryType
                    Case 6, 7, 8, 9, 10, 11
                        If rngStory.ShapeRange.Count > 0 Then

                            For Each oShp As Wd.Shape In rngStory.ShapeRange
                                If oShp.TextFrame.HasText Then
                                    'Call actionable procedure
                                    FieldsLockAll(oShp.TextFrame.TextRange)
                                    lockCount = lockCount + 1
                                End If
                            Next
                        End If
                    Case Else
                        'Do Nothing
                End Select
                On Error GoTo 0
                'Get next linked story (if any)
                rngStory = rngStory.NextStoryRange
            Loop Until rngStory Is Nothing
        Next rngStory
        Return lockCount
    End Function

    Shared Sub FieldsLockAll(ByRef oTargetRng As Wd.Range)
        'Dim cnt As Integer
        'cnt = 1
        'With oTargetRng.Fields
        'While .Count >= cnt
        '  .Item(cnt).Delete
        'Wend
        ' End With
        'Dim oFld
        For Each oFld As Wd.Field In oTargetRng.Fields
            oFld.Locked = True
            'Select Case oFld.Type
            '    Case Wd.WdFieldType.wdFieldDate
            '        oFld.Unlink()
            '    Case Else
            '        'Do nothing
            'End Select
        Next oFld
lbl_Exit:
        Exit Sub

    End Sub

    '04-Jan-2016 Aniket: Warn the user if the full document is being replaced by tags
    Shared Function ValidateTagsSelectedRange(ByRef oCurDoc As Wd.Document) As Boolean 'so that entire object is not passed

        'Dim wdDocumentRng As Wd.Range = oCurDoc.Application.ActiveDocument.Range() ‘ Please look https://msdn.microsoft.com/en-us/library/2a9dt54a.aspx
        Dim wdSelectionRng As Wd.Range = oCurDoc.Application.Selection.Range()
        Dim blnResult As Boolean = False

        Try

            If (oCurDoc.Content.Start = wdSelectionRng.Start) AndAlso (oCurDoc.Content.End = wdSelectionRng.End) Then
                If (MsgBox("The whole document will be replaced by the selected template/tag. The current data present in the document will be lost. Do you want to continue?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, "gloEMR")) = MsgBoxResult.Yes Then
                    blnResult = True
                Else
                    blnResult = False
                End If
            Else
                blnResult = True
            End If

            Return blnResult

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return True
        End Try

    End Function
    ''added for incident CAS-00640-Q3T3V0 while creating new template on save click showing word saveas dialog
    Public Shared Function CreateAndLoadNewDocument(ByRef wDSO As AxDSOFramer.AxFramerControl, ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, Optional ByVal bToProtect As Boolean = False) As String
        Dim myRandomFile As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
        Try
            Dim myLoadWord As LoadAndCloseWord = New LoadAndCloseWord()

            Dim objWdDoc As Microsoft.Office.Interop.Word.Document = myLoadWord.LoadWordApplication(Nothing)

            objWdDoc.SaveAs(myRandomFile)
            myLoadWord.CloseWordApplication(objWdDoc)
            myLoadWord = Nothing
            objWdDoc = Nothing


            LoadAndCloseWord.OpenDSO(wDSO, myRandomFile, thisDOC, thisAPP, False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try


        Return myRandomFile
    End Function
    

    Shared Function OpenDSO(ByRef wDSO As AxDSOFramer.AxFramerControl, ByRef oDocument As Object, ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, Optional ByVal bToProtect As Boolean = False) As String
        If (IsNothing(oDocument) = False) Then


            Dim strError As String = String.Empty
            Try
                gloMessageFilter.Register()
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            Try

                wDSO.Open(oDocument)
                Try
                    wDSO.Activate()
                Catch ex1 As Exception

                End Try
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                RepairDocx(oDocument)
                Try

                    wDSO.Open(oDocument)
                    Try
                        wDSO.Activate()
                    Catch ex1 As Exception

                    End Try
                    strError = ""
                Catch ex2 As Exception
                    strError = strError & ex2.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try

            End Try
            Try
                thisDOC = wDSO.ActiveDocument
                Try

                Catch ex1 As Exception
                    thisDOC.Activate()
                End Try
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            If (IsNothing(thisDOC) = False) Then
                Try
                    thisAPP = thisDOC.Application
                    Try
                        thisAPP.Caption = "~gloStream~" & Guid.NewGuid.ToString()
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    strError = strError & ex.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                'Incident #80398: 00046635 Added code changes for Printing Finished Exam
                Try
                    If (IsNothing(thisDOC.ActiveWindow) = False) Then
                        Try
                            thisDOC.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView
                            'thisDOC.ActiveWindow.View.ReadingLayoutAllowEditing = True
                            thisDOC.ActiveWindow.View.ReadingLayout = False
                            thisDOC.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        thisDOC.ActiveWindow.SetFocus()
                        Try
                            If (IsNothing(thisDOC.ActiveWindow.View) = False) Then
                                thisDOC.ActiveWindow.View.WrapToWindow = True
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        Try
                            If (IsNothing(thisDOC.ActiveWindow.View) = False) Then
                                'Bug #86546: 00000949: Template is showing <FORM TEXT> instead of formfied values 'SLR: moved to here.
                                thisDOC.ActiveWindow.View.ShowFieldCodes = False
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                            ex = Nothing
                        End Try

                    End If


                Catch ex As Exception
                    ' strError = strError & ex.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                If (bToProtect) Then
                    Try
                        thisDOC.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                    Catch ex As Exception
                        'strError = strError & ex.ToString()
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                End If

            End If

            Try
                gloMessageFilter.Revoke()
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Return strError
        Else
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Attempt to Open with No Content From Open DSO call", gloAuditTrail.ActivityOutCome.Failure)
            Return "No Content"
        End If

    End Function
    ''Bug #79843: 00000873: Exams are printing or exporting to pdf as blank. Added new parameter to unprotect finished exam
    Shared Function SaveDSO(ByRef wDSO As AxDSOFramer.AxFramerControl, ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, Optional ByVal unProtect As Boolean = False) As String
        Dim strError As String = String.Empty
        Try
            gloMessageFilter.Register()
        Catch ex As Exception
            strError = strError & ex.ToString()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Try
            Try
                wDSO.Activate()
            Catch ex1 As Exception

            End Try
            thisDOC = wDSO.ActiveDocument
        Catch ex As Exception
            strError = strError & ex.ToString()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        If (IsNothing(thisDOC) = False) Then
            ''Bug #79843: 00000873: Exams are printing or exporting to pdf as blank
            Try
                If unProtect AndAlso thisDOC.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                    thisDOC.Unprotect()
                End If
            Catch ex As Exception
                'strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                thisAPP = thisDOC.Application
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

            Try
                Try
                    thisAlertLevel = thisAPP.DisplayAlerts
                    thisAPP.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex1 As Exception

                End Try
                wDSO.Save()
            Catch ex As Exception
                Try
                    thisDOC.SaveAs(thisDOC.FullName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    Try
                        wDSO.Save()
                    Catch ex2 As Exception
                        strError = strError & ex2.ToString()
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        Try
                            SaveWord(thisDOC, thisAPP)
                        Catch ex3 As Exception
                            strError = strError & ex3.ToString()
                            ex3 = Nothing
                        End Try
                        ex2 = Nothing
                    End Try

                Catch ex1 As Exception

                    Try
                        wDSO.Save()
                    Catch ex2 As Exception
                        strError = strError & ex2.ToString()
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex2.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        Try
                            SaveWord(thisDOC, thisAPP)
                        Catch ex3 As Exception
                            strError = strError & ex3.ToString()
                            ex3 = Nothing
                        End Try
                        ex2 = Nothing
                    End Try
                    ex1 = Nothing
                End Try
                ex = Nothing
            End Try
            Try
                thisAPP.DisplayAlerts = thisAlertLevel
            Catch ex As Exception

            End Try
        Else
            strError = strError & "Not able to get active document from Save DSO call"
        End If

        Try
            gloMessageFilter.Revoke()
        Catch ex As Exception
            strError = strError & ex.ToString()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Return strError
    End Function
    Shared Function CloseAndDisposeDSO(ByRef wDSO As AxDSOFramer.AxFramerControl, Optional ByVal bDispose As Boolean = True) As String
        If (IsNothing(wDSO) = False) Then
            Dim strError As String = String.Empty
            Try
                gloMessageFilter.Register()
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Dim thisDOC As Wd.Document = Nothing
            Try
                thisDOC = wDSO.ActiveDocument
            Catch ex As Exception
                strError = strError & ex.ToString()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            If (IsNothing(thisDOC) = False) Then
                Try
                    thisDOC.Activate()
                Catch ex As Exception
                    strError = strError & ex.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                Dim thisAPP As Wd.Application = Nothing
                Try
                    thisAPP = thisDOC.Application
                Catch ex As Exception
                    strError = strError & ex.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                If (IsNothing(thisAPP) = False) Then
                    Try
                        thisAlertLevel = thisAPP.DisplayAlerts
                        thisAPP.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                    Catch ex As Exception

                    End Try


                    Try
                        Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)

                        thisDOC.Close(SaveChanges:=mysaveoptions)
                    Catch ex As Exception

                    End Try
                    Try
                        thisAPP.DisplayAlerts = thisAlertLevel
                    Catch ex As Exception

                    End Try
                    Try

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(thisDOC)

                    Catch ex As Exception
                        strError = strError & ex.ToString()
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try
                    thisDOC = Nothing
                    'Try
                    '    System.Runtime.InteropServices.Marshal.ReleaseComObject(thisAPP)
                    'Catch ex As Exception
                    '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    '    strError = strError & ex.ToString()
                    '    ex = Nothing

                    'End Try
                    'thisAPP = Nothing
                End If

                Try
                    gloMessageFilter.Revoke()
                Catch ex As Exception
                    strError = strError & ex.ToString()
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If
            If (bDispose) Then
                Try
                    wDSO.Dispose()
                Catch ex As Exception

                End Try
                wDSO = Nothing

            End If
            Return strError
        Else
            Return "No DSO"
        End If
    End Function
    Shared Function SaveWordDocument(ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, ByVal thisPath As String) As String

        If (IsNothing(thisDOC) = False) Then
            Try
                If (IsNothing(thisAPP)) Then
                    thisAPP = thisDOC.Application
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Dim sFileName As String = thisDOC.FullName
            Try
                Try
                    thisAlertLevel = thisAPP.DisplayAlerts
                    thisAPP.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch

                End Try
                Try
                    If thisDOC.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                        thisDOC.Unprotect()
                    End If
                Catch

                End Try
                thisDOC.Save()
            Catch
                Try
                    ClearWordGarbage(thisAPP)
                Catch

                End Try

                Try
                    thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch
                    Try
                        ClearWordGarbage(thisAPP)
                    Catch

                    End Try

                    Try
                        sFileName = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
                        thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                    Catch ex As Exception
                        SaveWord(thisDOC, thisAPP)
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                End Try

            End Try
            Try
                thisAPP.DisplayAlerts = thisAlertLevel
            Catch

            End Try
            Return sFileName
        Else
            Return ""
        End If

    End Function

    Shared Function SaveNewWordDocument(ByRef thisDOC As Wd.Document, ByVal thisPath As String) As String

        If (IsNothing(thisDOC) = False) Then
            Dim thisApp As Wd.Application = thisDOC.Application
            Try
                If (IsNothing(thisApp)) Then
                    thisApp = thisDOC.Application
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Dim sFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
            Try
                Try
                    thisAlertLevel = thisApp.DisplayAlerts
                    thisApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch

                End Try
                Try
                    If thisDOC.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                        thisDOC.Unprotect()
                    End If
                Catch

                End Try
                thisDOC.SaveAs(sFileName)
            Catch
                Try
                    ClearWordGarbage(thisApp)
                Catch

                End Try

                Try
                    thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch
                    Try
                        ClearWordGarbage(thisApp)
                    Catch

                    End Try

                    Try
                        sFileName = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
                        thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                    Catch ex As Exception
                        SaveWord(thisDOC, thisApp)
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                End Try

            End Try
            Try
                thisApp.DisplayAlerts = thisAlertLevel
            Catch

            End Try
            Return sFileName
        Else
            Return ""
        End If

    End Function

    Public Shared Sub RefreshWord(ByRef wdDoc As Wd.Document, ByRef thisApplication As Wd.Application)


        Dim strBookMark As String = "SignatureImage" & System.Guid.NewGuid.ToString("N")

        Try


            With wdDoc.Bookmarks
                .Add(Range:=wdDoc.ActiveWindow.Selection.Range, Name:=strBookMark)
            End With

            wdDoc.Content.Select()
            wdDoc.Content.Copy()
            wdDoc.Content.PasteAndFormat(Interop.Word.WdRecoveryType.wdFormatOriginalFormatting)

            wdDoc.Bookmarks(strBookMark).Select()
            wdDoc.Bookmarks(strBookMark).Delete()

        Catch ex As Exception

        End Try


    End Sub
    Public Shared Function AssignPrinterBookMarks(ByRef wdDoc As Wd.Document, ByVal FromPage As Integer, ByVal ToPage As Integer) As String


        Dim strUniqueID As String = gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff")
        Dim strStartBookMark As String = "PrintStart" & strUniqueID
        Dim strEndBookMark As String = "PrintEnd" & strUniqueID
        Try
            Dim oMissing As Object = System.Reflection.Missing.Value
            Dim what As Object = Wd.WdGoToItem.wdGoToPage
            Dim whichFirst As Object = Wd.WdGoToDirection.wdGoToFirst
            Dim fromCount As Object = FromPage
            Try
                Dim startRange As Wd.Range = wdDoc.Selection.[GoTo](what, whichFirst, fromCount, oMissing)
                startRange.SetRange(startRange.Start, startRange.Start)
                wdDoc.Bookmarks.Add(Range:=startRange, Name:=strStartBookMark)

            Catch ex As Exception
                strUniqueID = ""
            End Try
            what = Wd.WdGoToItem.wdGoToPage
            Dim toCount As Object = ToPage
            Dim whichLast As Object = Wd.WdGoToDirection.wdGoToFirst
            Try
                Dim endRange As Wd.Range = wdDoc.Selection.[GoTo](what, whichLast, toCount, oMissing)
                If (endRange.End > 0) Then
                    endRange.SetRange(endRange.End - 1, endRange.End - 1)
                Else
                    endRange.SetRange(endRange.End, endRange.End)
                End If

                wdDoc.Bookmarks.Add(Range:=endRange, Name:=strEndBookMark)

            Catch ex As Exception
                strUniqueID = ""
            End Try

        Catch ex As Exception
            strUniqueID = ""
        End Try
        Return strUniqueID

    End Function
    Public Shared Function GetRevisedPageNumbers(ByRef wdDoc As Wd.Document, ByVal strUniqueID As String, ByRef fromPage As Integer, ByRef toPage As Integer) As Boolean
        Dim strStartBookMark As String = "PrintStart" & strUniqueID
        Dim strEndBookMark As String = "PrintEnd" & strUniqueID
        Dim retVal As Boolean = False
        Try

            If (wdDoc.Bookmarks.Exists(strStartBookMark)) Then
                wdDoc.Bookmarks(strStartBookMark).Select()
                Try
                    fromPage = wdDoc.Application.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                    retVal = True
                Catch ex As Exception
                    retVal = False
                End Try
            Else
                retVal = False
            End If
            If (wdDoc.Bookmarks.Exists(strEndBookMark)) Then
                wdDoc.Bookmarks(strEndBookMark).Select()
                Try
                    toPage = wdDoc.Application.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                    retVal = retVal And True
                Catch ex As Exception
                    retVal = False
                End Try
            Else
                retVal = False
            End If
            If (fromPage > 0) OrElse (toPage > 0) Then
                Dim what As Object = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage
                Dim which As Object = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst
                Dim count As Object = If(fromPage > 0, fromPage, toPage)
                Dim missing As Object = System.Reflection.Missing.Value
                Try
                    wdDoc.Application.Selection.[GoTo](what, which, count, missing)
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            retVal = False
        End Try

        Return retVal

    End Function

    Public Shared Function SaveWord(ByRef wdDoc As Wd.Document, ByRef thisApplication As Wd.Application) As Boolean
        'SLR: http://www.office-archive.com/17-word/60fc491b45b2bb25.htm

        If (IsNothing(wdDoc) = False) Then


            ' Dim thisApplication As Wd.Application = wdDoc.Application

            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                thisAlertLevel = thisApplication.DisplayAlerts
                thisApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            Catch ex As Exception

            End Try

            Try
                Dim thisPagination As Boolean = thisApplication.Options.Pagination
                Dim thisVisible As Boolean = thisApplication.Visible
                Dim thisScreenUpdating As Boolean = thisApplication.ScreenUpdating
                ' thisApplication.Resize(620, 340)
                thisApplication.Options.Pagination = False
                thisApplication.Visible = True
                'thisApplication.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                'thisApplication.ActiveWindow.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                Try
                    wdDoc.UndoClear()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                Try
                    wdDoc.Repaginate()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Try
                    If (wdDoc.CompatibilityMode <> 65535) Then 'wdcurrent: but unable to enumerate:
                        If (wdDoc.CompatibilityMode <> thisApplication.Version) Then
                            wdDoc.Convert()
                        End If
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
                Try
                    thisApplication.ScreenUpdating = True
                    thisApplication.ScreenRefresh()
                    thisApplication.ScreenUpdating = thisScreenUpdating
                    thisApplication.Options.Pagination = thisPagination
                    thisApplication.Options.SavePropertiesPrompt = False
                    thisApplication.Options.SaveNormalPrompt = False
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
                Try
                    wdDoc.Save()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                thisApplication.Visible = thisVisible

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                thisApplication.DisplayAlerts = thisAlertLevel
            Catch ex As Exception

            End Try


        End If


        Return False
    End Function
    Shared Function GetACopy(ByRef wDSO As AxDSOFramer.AxFramerControl, ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, ByVal thisPath As String, Optional ByVal unProtect As Boolean = False) As String
        Dim _SaveFlag As Boolean
        Try
            _SaveFlag = thisDOC.Saved
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Get Flag Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Dim strAnyError As String = SaveDSO(wDSO, thisDOC, thisAPP, unProtect)
        Dim sFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
        Try
            Dim thisDocFullname As String = thisDOC.FullName
            Try
                File.Copy(thisDocFullname, sFileName)
            Catch ex As Exception
                'SLR: Double save so that file lock is freed
                Try
                    thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch ex2 As Exception

                End Try
                Try
                    thisDOC.SaveAs(thisDocFullname, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch ex2 As Exception

                End Try

            End Try

            Try
                thisDOC.Saved = _SaveFlag
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Set Flag Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Copy file  Destination Path :='" & sFileName & "'" & " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
        Return sFileName
    End Function
    Shared Function GetACopy(ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, ByVal thisPath As String, Optional ByVal unProtect As Boolean = False) As String
        Dim _SaveFlag As Boolean
        Try
            _SaveFlag = thisDOC.Saved
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Get Flag Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Try
            thisDOC.Save()
        Catch ex As Exception
            SaveWord(thisDOC, thisAPP)
        End Try

        Dim sFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(thisPath, ".docx", "MMddyyyyHHmmssffff")
        Try
            Dim thisDocFullname As String = thisDOC.FullName
            Try
                File.Copy(thisDocFullname, sFileName)
            Catch ex As Exception
                'SLR: Double save so that file lock is freed
                Try
                    thisDOC.SaveAs(sFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch ex2 As Exception

                End Try
                Try
                    thisDOC.SaveAs(thisDocFullname, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                Catch ex2 As Exception

                End Try

            End Try

            Try
                thisDOC.Saved = _SaveFlag
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Set Flag Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, "Unable to Copy file  Destination Path :='" & sFileName & "'" & " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
        Return sFileName
    End Function

    Shared bIamNotInsideClear As Boolean = True
    Public Shared Sub ClearWordGarbage(Optional ByRef thisApplication As Wd.Application = Nothing)
        'SLR: http://www.office-archive.com/17-word/60fc491b45b2bb25.htm
        If (bIamNotInsideClear) Then

            If (IsNothing(thisApplication)) Then
                Try
                    thisApplication = CType(Marshal.GetActiveObject("Word.Application"), Wd.Application)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
            End If

            If (IsNothing(thisApplication) = False) Then
                Try
                    gloMessageFilter.Register()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                Try
                    thisAlertLevel = thisApplication.DisplayAlerts
                    thisApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex As Exception

                End Try
                Try
                    Dim thisPagination As Boolean = thisApplication.Options.Pagination
                    Dim thisVisible As Boolean = thisApplication.Visible
                    Dim thisScreenUpdating As Boolean = thisApplication.ScreenUpdating
                    ' thisApplication.Resize(620, 340)
                    thisApplication.Options.Pagination = False
                    thisApplication.ActiveDocument.UndoClear()
                    thisApplication.ActiveDocument.Repaginate()
                    thisApplication.Visible = True
                    thisApplication.ScreenUpdating = True
                    thisApplication.ScreenRefresh()
                    thisApplication.ScreenUpdating = thisScreenUpdating
                    thisApplication.Options.Pagination = thisPagination
                    thisApplication.Options.SavePropertiesPrompt = False
                    thisApplication.Options.SaveNormalPrompt = False
                    bIamNotInsideClear = False
                    thisApplication.ActiveDocument.Save()
                    thisApplication.Visible = thisVisible
                    bIamNotInsideClear = True

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                bIamNotInsideClear = True
                Try
                    thisApplication.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try

                Try
                    gloMessageFilter.Revoke()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End If

        End If
    End Sub
    Private Shared _blnchkValidOS As Boolean = False
    ''added for 8071 integration changes
    Public Shared Function PrintDocumentEMF(ByRef oDoc As Wd.Document, ByRef Background As Object, ByRef Append As Object, ByRef Range As Object, ByRef OutputFileName As Object, ByRef aFrom As Object, ByRef aTo As Object, ByRef Item As Object, _
ByRef Copies As Object, ByRef Pages As Object, ByRef PageType As Object, ByRef PrintToFile As Object, ByRef Collate As Object, ByRef ActivePrinterMacGX As Object, _
ByRef ManualDuplexPrint As Object, ByRef PrintZoomColumn As Object, ByRef PrintZoomRow As Object, ByRef PrintZoomPaperWidth As Object, ByRef PrintZoomPaperHeight As Object, Optional ByVal lnPatientId As Long = 0, Optional ByVal _PrinterSettings As System.Drawing.Printing.PrinterSettings = Nothing) As Integer

        Dim Copied As Boolean = False
        Dim RetVal As Integer = -1
        Dim SplitDocList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)()

        Dim lstToAddDocuments As New List(Of gloPrintDialog.gloPrintProgressController.DocumentInfo)


        Dim ps As System.Drawing.Printing.PrinterSettings = Nothing
        If (Not IsNothing(_PrinterSettings)) Then
            ps = _PrinterSettings
        Else
            ps = New System.Drawing.Printing.PrinterSettings()
        End If

        Dim oDialog As New gloPrintDialog.gloPrintDialog()

        Try

            oDialog.CustomPrinterExtendedSettings.IsShowProgress = False
            oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
            '' oDoc.Save()

            If (gloGlobal.gloTSPrint.TerminalServer() = "RDP") Then
                SplitDocList = getPDFForWord(oDoc)
            Else
                If (_blnchkValidOS = False) Then ''added to check if emf is valid or not for that OS
                    gloGlobal.gloTSPrint.CheckValidEMFOS()
                    _blnchkValidOS = True
                End If
                If (gloGlobal.gloTSPrint.validEMFOS) Then
                    SplitDocList = getEMFForWord(oDoc)
                Else
                    SplitDocList = getPDFForWord(oDoc)
                End If
            End If


            lstToAddDocuments.Clear()

            For intCount As Integer = 0 To SplitDocList.Count - 1
                Dim curDocInfo As New gloPrintDialog.gloPrintProgressController.DocumentInfo
                curDocInfo.PdfFileName = SplitDocList(intCount).PdfFileName
                curDocInfo.SrcFileName = SplitDocList(intCount).SrcFileName
                'curDocInfo.footerInfo = lstFooter
                lstToAddDocuments.Add(curDocInfo)
            Next

            ''printer settings set
            oDialog.PrinterSettings = ps
            ' '  oDialog.PrinterSettings.Duplex = Type.Missing
            oDialog.PrinterSettings.Copies = Copies
            oDialog.PrinterSettings.Collate = Collate
            '    oDialog.PrinterSettings.PrintToFile = False
            'return lstToAddDocuments;


            'Added for vista/XP os for local printing case

            oDialog.CustomPrinterExtendedSettings.CurrentPageSize = gloExtendedPrinterSettings.PageSize.ActualPageSize
            oDialog.CustomPrinterExtendedSettings.IsActualLandscape = True
            oDialog.CustomPrinterExtendedSettings.PrinterMarginsBottom = Nothing
            oDialog.CustomPrinterExtendedSettings.PrinterMarginsLeft = -10
            oDialog.CustomPrinterExtendedSettings.PrinterMarginsRight = Nothing
            oDialog.CustomPrinterExtendedSettings.PrinterMarginsTop = -10


            Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = New gloPrintDialog.gloPrintProgressController(lstToAddDocuments, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, False, Nothing, False, False, False, "pdf")
            ogloPrintProgressController.IamPrinting = False
            ogloPrintProgressController.ShowProgress(Nothing)
            While (ogloPrintProgressController.IamPrinting = False)
                System.Threading.Thread.Sleep(1)
            End While

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)

        Finally
            If IsNothing(SplitDocList) = False Then
                SplitDocList.Clear()
                SplitDocList = Nothing
            End If

            If IsNothing(ps) = False Then
                ps = Nothing
            End If

            If IsNothing(oDialog) = False Then
                oDialog.Dispose()
                oDialog = Nothing
            End If
        End Try

        Return RetVal

    End Function

    Public Shared Function PrintWordDocument(ByRef oDoc As Wd.Document, ByVal bPrintFromWord As Boolean, Optional ByVal fromFax As Boolean = False, Optional ByVal lnPatientId As Long = 0) As String
        Dim Copied As Boolean = False
        If fromFax = False Then
            If gloGlobal.gloTSPrint.isCopyPrint Then 'Check setting for TS Print
                _popUpDetails = Nothing
                _showPopup = bPrintFromWord
                Copied = CopyPrintDoc(oDoc, lnPatientId)
            End If
        End If

        If Copied = False Then
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                Try
                    gloMessageFilter.Register()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Try
                    thisAlertLevel = oDoc.Application.DisplayAlerts
                    oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex As Exception

                End Try
                ' oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                oDoc.Application.Options.PrintBackground = False

                Dim RetVal As Integer = -1
                WordDialogBoxBackgroundCloser.isPrintingOn = True
                If (bPrintFromWord) Then

                    ' oDoc.Application.Visible = True
                    oDoc.Application.ScreenUpdating = True

                    oDoc.Application.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                    oDoc.Application.ActiveWindow.WindowState = Interop.Word.WdWindowState.wdWindowStateMinimize
                    '  oDoc.Application.Resize(620, 340)
                    RetVal = oDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).Show()
                Else
                    Try
                        oDoc.PrintOut(Background:=False)
                    Catch ex As Exception
                        RetVal = 0
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                End If

                If (RetVal = -1) Then

                    While oDoc.Application.BackgroundPrintingStatus <> 0
                        System.Windows.Forms.Application.DoEvents()
                        System.Threading.Thread.Sleep(100)
                    End While
                    Return oDoc.Application.ActivePrinter.ToString()
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
                Return Nothing
            Finally
                WordDialogBoxBackgroundCloser.isPrintingOn = False
                Try
                    oDoc.Application.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try

                'oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                Try
                    gloMessageFilter.Revoke()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End Try
        Else
            Return "Copied"
        End If

    End Function
    ''return type change while intergating changes from 8071 
    Public Shared Function PrintDocument(ByRef oDoc As Wd.Document, ByRef Background As Object, ByRef Append As Object, ByRef Range As Object, ByRef OutputFileName As Object, ByRef aFrom As Object, ByRef aTo As Object, ByRef Item As Object, _
 ByRef Copies As Object, ByRef Pages As Object, ByRef PageType As Object, ByRef PrintToFile As Object, ByRef Collate As Object, ByRef ActivePrinterMacGX As Object, _
 ByRef ManualDuplexPrint As Object, ByRef PrintZoomColumn As Object, ByRef PrintZoomRow As Object, ByRef PrintZoomPaperWidth As Object, ByRef PrintZoomPaperHeight As Object, Optional ByVal lnPatientId As Long = 0, Optional ByVal popupDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing, Optional ByVal PrinterName As String = "") As Integer
        Dim Copied As Boolean = False
        Dim RetVal As Integer = -1
        If gloGlobal.gloTSPrint.isCopyPrint Then 'Check setting for TS Print
            _popUpDetails = popupDetails
            Copied = CopyPrintDoc(oDoc, lnPatientId)
        End If
        If Copied = False Then
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try
                Try
                    gloMessageFilter.Register()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                Try
                    thisAlertLevel = oDoc.Application.DisplayAlerts
                    oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex As Exception

                End Try
                ' oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone

                oDoc.Application.Options.PrintBackground = False
                Dim thisBackground As Object = False
                ''  Dim RetVal As Integer = -1
                WordDialogBoxBackgroundCloser.isPrintingOn = True
                Try
                    If (PrinterName <> "") Then ''added for incident 67769 default printer changing
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(PrinterName)
                        Application.DoEvents()
                    End If
                    Dim stractivedocprintername As String = oDoc.Application.ActivePrinter.ToString()
                    ''added for incident CAS-03968-F8W0H4
                    If (stractivedocprintername.Contains(PrinterName) = False) Then
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(PrinterName)
                        Application.DoEvents()
                    End If

                    oDoc.PrintOut(thisBackground, Append, Range, OutputFileName, aFrom, aTo, Item, Copies, Pages, PageType, PrintToFile, Collate, ActivePrinterMacGX, ManualDuplexPrint, PrintZoomColumn, PrintZoomRow, PrintZoomPaperWidth, PrintZoomPaperHeight)
                Catch ex As Exception
                    RetVal = 0
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try


                If (RetVal = -1) Then

                    While oDoc.Application.BackgroundPrintingStatus <> 0
                        System.Windows.Forms.Application.DoEvents()
                        System.Threading.Thread.Sleep(100)
                    End While


                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            Finally
                WordDialogBoxBackgroundCloser.isPrintingOn = False
                Try
                    oDoc.Application.DisplayAlerts = thisAlertLevel
                Catch ex As Exception

                End Try

                'oDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                Try
                    gloMessageFilter.Revoke()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

            End Try
        End If
        Return RetVal
    End Function

    Private Shared myCounter As Integer = 0
    Public Shared Function CopyPrintDoc(ByVal aDoc As Microsoft.Office.Interop.Word.Document, ByVal lnPatientId As Long, Optional ByVal bClearPopUp As Boolean = True) As Boolean
        Try
            If aDoc IsNot Nothing Then
                SetPatientDetails(lnPatientId)
                Dim SplitDocList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)()

                '' Set local printer to word document which will improve performance
                If gloGlobal.gloTSPrint._strlocalprinter = "" Then
                    gloGlobal.gloTSPrint.SetlocalPrinter()
                End If
                If gloGlobal.gloTSPrint._strlocalprinter <> "NoLocalPrinter" Then
                    Try
                        aDoc.Application.WordBasic.FilePrintSetup(Printer:=gloGlobal.gloTSPrint._strlocalprinter, DoNotSetAsSysDefault:=1)
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
                        ex = Nothing
                    End Try
                End If
                ''

                If gloGlobal.gloTSPrint.UseEMFForWord Then
                    SplitDocList = getEMFForWord(aDoc)
                End If
                If gloGlobal.gloTSPrint.UseEMFForWord = False Or SplitDocList Is Nothing Then
                    Dim PDFFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".pdf", "MMddyyyyHHmmssffff")
                    aDoc.SaveAs(PDFFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)
                    FileSystem.FileClose()

                    gloAuditTrail.gloAuditTrail.PrintLog(strException:="PDF file generated for Word Printing.", ShowMessageBox:=False)

                    'Split large file as per setting
                    If gloGlobal.gloTSPrint.NoOfPages > 0 Then
                        Dim footer As New gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo()
                        Dim footerList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueFooterInfo)()
                        footer.FromPage = 0
                        footer.ToPage = 0
                        footerList.Add(footer)

                        SplitDocList = gloPrintDialog.gloRecoverPDF.SplitPDFToMaxNoOfPages(PDFFileName, gloGlobal.gloTSPrint.NoOfPages, Nothing, footerList)
                    Else
                        Dim physicalDoc As New gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo()
                        physicalDoc.PdfFileName = PDFFileName
                        physicalDoc.SrcFileName = PDFFileName
                        physicalDoc.footerInfo = Nothing
                        SplitDocList.Add(physicalDoc)
                    End If
                End If




                ''Generate MetaData File
                'Dim PDFWithoutPath As String = PDFFileName.Substring(PDFFileName.LastIndexOf("\") + 1)
                ' Dim strMetaDataFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".xml", "MMddyyyyHHmmssffff")


                ''Copy Files to mapped virtual drive
                If gloGlobal.gloTSPrint.isMapped() Then
                    If Not gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) AndAlso (_popUpDetails Is Nothing) AndAlso (_showPopup = True) Then
                        If _isTSPrinterSelectionOpen Then
                            Return True
                        End If
                        _isTSPrinterSelectionOpen = True
                        Dim tsPrintDialog As New gloPrintDialog.frmTSPrintDialog()
                        tsPrintDialog.ShowDialog()
                        _isTSPrinterSelectionOpen = False
                        If tsPrintDialog.cancelPirnt = True Then
                            Return True
                        End If
                        _popUpDetails = New gloClinicalQueueGeneral.QueueDocumentDocumentDetails()
                        _popUpDetails.PrintFrom = tsPrintDialog.pageFrom
                        _popUpDetails.PrintTo = tsPrintDialog.pageTo
                        _popUpDetails.Printer = tsPrintDialog.currPrinterFile
                        _popUpDetails.Copies = tsPrintDialog.NoOfCopies
                        _popUpDetails.Landscape = tsPrintDialog.isLandscape
                        _popUpDetails.Duplex = tsPrintDialog.duplex
                        _popUpDetails.Size = tsPrintDialog.currSize
                        _popUpDetails.Tray = tsPrintDialog.currTray
                        _popUpDetails.isCollete = tsPrintDialog.isCollete
                    End If

                    Dim strMetaDataFilePath As String = ""
                    Dim MetaDataGenerated As Boolean
                    myCounter += 1
                    If myCounter = 1000 Then
                        myCounter = 0
                    End If
                    If Not gloGlobal.gloTSPrint.UseZippedMetadata Then
                        strMetaDataFilePath = gloGlobal.gloTSPrint.TempPath & "01" & Format(DateTime.Now, "_MMddyyyy_hhmmsstt") & myCounter.ToString("D3") & ".xml"
                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _popUpDetails, bUseFileZip:=False)
                    End If



                    If MetaDataGenerated Or gloGlobal.gloTSPrint.UseZippedMetadata Then

                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="MetaData file generated for Word Printing.", ShowMessageBox:=False)

                        'File.Copy(PDFFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                        Dim PDFWithoutPath As [String] = ""
                        Dim First As Boolean = True
                        For fileCntr As Integer = 0 To SplitDocList.Count - 1

                            PDFWithoutPath = SplitDocList(fileCntr).PdfFileName.Substring(SplitDocList(fileCntr).PdfFileName.LastIndexOf("\") + 1)
                            gloGlobal.gloTSPrint.CopyFileToNetworkShare(SplitDocList(fileCntr).PdfFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                            If (First) Then
                                If Not gloGlobal.gloTSPrint.UseZippedMetadata Then
                                    gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\") + 1))
                                Else
                                    strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath & "\\01" & Format(DateTime.Now, "_MMddyyyy_hhmmsstt") & myCounter.ToString("D3") & ".xmz"
                                    MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _popUpDetails, bUseFileZip:=True)
                                End If


                                First = False
                            End If
                        Next
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="PDF and MetaData files copied to virtual drive for Word Printing.", ShowMessageBox:=False)
                    Else

                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="Error in MetaData file generation for Word Printing.", ShowMessageBox:=False)
                        Return False
                    End If
                Else
                    If _isTSPrintDialogOpen = False Then
                        _isTSPrintDialogOpen = True
                        Dim s As DialogResult = MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If s = Windows.Forms.DialogResult.Yes Then
                            _isTSPrintDialogOpen = False
                            gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Using RDP printer", ShowMessageBox:=False)
                            Return False
                        Else
                            _isTSPrintDialogOpen = False
                            gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Document Not Printed", ShowMessageBox:=False)
                            Return True
                        End If
                    Else
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found messagebox already active. Document Not Printed", ShowMessageBox:=False)
                        Return True
                    End If


                End If

                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        Finally
            gloGlobal.gloTSPrint.SetTestPatient()
            If bClearPopUp Then
                _popUpDetails = Nothing
            End If
        End Try
    End Function

    Public Shared Function CopyPrintDocList(ByVal SplitDocList As List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo), ByVal lnPatientId As Long, popup As gloClinicalQueueGeneral.QueueDocumentDocumentDetails) As Boolean
        Try
            If SplitDocList.Count > 0 Then
                SetPatientDetails(lnPatientId)

                _popUpDetails = popup

                ''Copy Files to mapped virtual drive
                If gloGlobal.gloTSPrint.isMapped() Then
                    If Not gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) AndAlso (_popUpDetails Is Nothing) AndAlso (_showPopup = True) Then
                        If _isTSPrinterSelectionOpen Then
                            Return True
                        End If
                        _isTSPrinterSelectionOpen = True
                        Dim tsPrintDialog As New gloPrintDialog.frmTSPrintDialog()
                        tsPrintDialog.ShowDialog()
                        _isTSPrinterSelectionOpen = False
                        If tsPrintDialog.cancelPirnt = True Then
                            Return True
                        End If
                        _popUpDetails = New gloClinicalQueueGeneral.QueueDocumentDocumentDetails()
                        _popUpDetails.PrintFrom = tsPrintDialog.pageFrom
                        _popUpDetails.PrintTo = tsPrintDialog.pageTo
                        _popUpDetails.Printer = tsPrintDialog.currPrinterFile
                        _popUpDetails.Copies = tsPrintDialog.NoOfCopies
                        _popUpDetails.Landscape = tsPrintDialog.isLandscape
                        _popUpDetails.Duplex = tsPrintDialog.duplex
                        _popUpDetails.Size = tsPrintDialog.currSize
                        _popUpDetails.Tray = tsPrintDialog.currTray
                        _popUpDetails.isCollete = tsPrintDialog.isCollete
                    End If

                    Dim strMetaDataFilePath As String = ""
                    Dim MetaDataGenerated As Boolean
                    myCounter += 1
                    If myCounter = 1000 Then
                        myCounter = 0
                    End If
                    If Not gloGlobal.gloTSPrint.UseZippedMetadata Then
                        strMetaDataFilePath = gloGlobal.gloTSPrint.TempPath & "01" & Format(DateTime.Now, "_MMddyyyy_hhmmsstt") & myCounter.ToString("D3") & ".xml"
                        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _popUpDetails, bUseFileZip:=False)
                    End If



                    If MetaDataGenerated Or gloGlobal.gloTSPrint.UseZippedMetadata Then

                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="MetaData file generated for Word Printing.", ShowMessageBox:=False)

                        'File.Copy(PDFFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                        Dim PDFWithoutPath As [String] = ""
                        'Dim First As Boolean = True
                        For fileCntr As Integer = 0 To SplitDocList.Count - 1

                            PDFWithoutPath = SplitDocList(fileCntr).PdfFileName.Substring(SplitDocList(fileCntr).PdfFileName.LastIndexOf("\") + 1)
                            gloGlobal.gloTSPrint.CopyFileToNetworkShare(SplitDocList(fileCntr).PdfFileName, gloGlobal.gloTSPrint.AppFolderPath + "\" + PDFWithoutPath)
                            'If (First) Then
                            '    If Not gloGlobal.gloTSPrint.UseZippedMetadata Then
                            '        gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\") + 1))
                            '    Else
                            '        strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath & "\\01" & Format(DateTime.Now, "_MMddyyyy_hhmmsstt") & myCounter.ToString("D3") & ".xmz"
                            '        MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _popUpDetails, bUseFileZip:=True)
                            '    End If


                            '    First = False
                            'End If
                        Next
                        If Not gloGlobal.gloTSPrint.UseZippedMetadata Then
                            gloGlobal.gloTSPrint.CopyFileToNetworkShare(strMetaDataFilePath, gloGlobal.gloTSPrint.AppFolderPath + "\" + strMetaDataFilePath.Substring(strMetaDataFilePath.LastIndexOf("\") + 1))
                        Else
                            strMetaDataFilePath = gloGlobal.gloTSPrint.AppFolderPath & "\\01" & Format(DateTime.Now, "_MMddyyyy_hhmmsstt") & myCounter.ToString("D3") & ".xmz"
                            MetaDataGenerated = GenerateMetaDataFile(strMetaDataFilePath, SplitDocList, _popUpDetails, bUseFileZip:=True)
                        End If
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="PDF and MetaData files copied to virtual drive for Word Printing.", ShowMessageBox:=False)
                    Else

                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="Error in MetaData file generation for Word Printing.", ShowMessageBox:=False)
                        Return False
                    End If
                Else
                    If _isTSPrintDialogOpen = False Then
                        _isTSPrintDialogOpen = True
                        MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP.", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information)
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Document Not Printed", ShowMessageBox:=False)
                        'Dim s As DialogResult = MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP." + Environment.NewLine + Environment.NewLine + "Instead can RDP printer be used now?", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        'If s = Windows.Forms.DialogResult.Yes Then
                        '    _isTSPrintDialogOpen = False
                        '    gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Using RDP printer", ShowMessageBox:=False)
                        '    Return False
                        'Else
                        '    _isTSPrintDialogOpen = False
                        '    gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found. Document Not Printed", ShowMessageBox:=False)
                        '    Return True
                        'End If
                    Else
                        gloAuditTrail.gloAuditTrail.PrintLog(strException:="Mapped drive not found messagebox already active. Document Not Printed", ShowMessageBox:=False)
                        Return True
                    End If


                End If

                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        Finally
            gloGlobal.gloTSPrint.SetTestPatient()
            _popUpDetails = Nothing
        End Try
    End Function

    Public Shared Function showTSPrintPopup() As Boolean
        Try
            If _isTSPrinterSelectionOpen Then
                Return False
            End If
            _isTSPrinterSelectionOpen = True
            Dim tsPrintDialog As New gloPrintDialog.frmTSPrintDialog()
            tsPrintDialog.ShowDialog()
            _isTSPrinterSelectionOpen = False
            If tsPrintDialog.cancelPirnt = True Then
                Return False
            End If
            _popUpDetails = New gloClinicalQueueGeneral.QueueDocumentDocumentDetails()
            _popUpDetails.PrintFrom = tsPrintDialog.pageFrom
            _popUpDetails.PrintTo = tsPrintDialog.pageTo
            _popUpDetails.Printer = tsPrintDialog.currPrinterFile
            _popUpDetails.Copies = tsPrintDialog.NoOfCopies
            _popUpDetails.Landscape = tsPrintDialog.isLandscape
            _popUpDetails.Duplex = tsPrintDialog.duplex
            _popUpDetails.Size = tsPrintDialog.currSize
            _popUpDetails.Tray = tsPrintDialog.currTray
            _popUpDetails.isCollete = tsPrintDialog.isCollete
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        End Try
    End Function

    Public Shared Sub cleatTSPrintPopupDetails()
        Try
            _popUpDetails = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
        End Try
    End Sub

    Public Shared Sub SetPatientDetails(ByVal m_PatientId As Long)
        Dim connMain As New SqlConnection
        Dim dsMain As DataSet
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter

        connMain.ConnectionString = gloPMGlobal.DatabaseConnectionString ''GetConnectionString()

        Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'') as PName, dtDOB as DOB from Patient where nPatientID=" & m_PatientId
        cmd = New SqlCommand(strSQL, connMain)


        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dsMain = New DataSet

        Try

            connMain.Open()
            da.Fill(dsMain)
            connMain.Close()

            dsMain.Tables(0).TableName = "PatientInfo"
            If dsMain.Tables(0).Rows.Count > 0 Then
                gloGlobal.gloTSPrint.PatientName = dsMain.Tables(0).Rows(0).Item("PName")
                gloGlobal.gloTSPrint.PatientDOB = dsMain.Tables(0).Rows(0).Item("DOB")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If

            If IsNothing(connMain) = False Then
                connMain.Dispose()
                connMain = Nothing
            End If

        End Try
    End Sub

    Private Shared Function GenerateMetaDataFile(ByVal strFilePath As String, ByVal PhysicalFile As List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo), ByVal popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails, Optional ByVal bUseFileZip As Boolean = False) As Boolean
        Dim _QueueWriter As New gloClinicalQueueGeneral.gloQueueMetadatawriter()
        Dim QueueDoc As gloClinicalQueueGeneral.Queue = Nothing
        Try
            'Dim strFilePath As String = GenerateClinicalChartFileName(ds, 0, True)

            QueueDoc = _QueueWriter.GenerateWordMetaDataFile(gloGlobal.gloTSPrint.PatientName, gloGlobal.gloTSPrint.PatientDOB, gloGlobal.gloTSPrint.AddFooterInService, PhysicalFile, strFilePath.Substring(strFilePath.LastIndexOf("\") + 1), popUpDetails:=popUpDetails)
            Try
                gloQueueSchema.gloSerialization.SetClinicalDocument(strFilePath, QueueDoc, bUseFileZip)
                Return True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
                ex = Nothing
                Return False
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            Return False
        Finally
            If Not IsNothing(_QueueWriter) Then
                _QueueWriter.Dispose()
                _QueueWriter = Nothing
            End If
            If Not IsNothing(QueueDoc) Then
                QueueDoc = Nothing
            End If
        End Try

    End Function

    Public Shared Function getEMFForWord(aDoc As Interop.Word.Document) As List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)

        Try
            Dim SplitDocList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)()
            Dim NewFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
            Dim dicBytes As Dictionary(Of String, Byte()) = New Dictionary(Of String, Byte())()
            Dim iCount As Integer = 1
            Dim windows As Wd.Window = aDoc.ActiveWindow
            windows.Activate()
            '  For Each panes As Wd.Pane In windows.Panes
            For cntpanes As Integer = 1 To windows.Panes.Count
                Dim panes As Wd.Pane = windows.Panes(cntpanes)
                panes.Activate()
                For i As Integer = 1 To panes.Pages.Count
                    panes.Activate()
                    Dim ableToAccess As Boolean = False
                    Dim bytes As Byte() = Nothing
                    While Not ableToAccess
                        Try
                            bytes = panes.Pages(i).EnhMetaFileBits
                            ableToAccess = True
                        Catch generatedExceptionName As COMException
                            System.Threading.Thread.Sleep(1)
                        End Try
                    End While

                    dicBytes.Add(iCount.ToString(), bytes)

                    iCount += 1
                Next
            Next
            Dim ZipedFiles As List(Of String) = gloGlobal.gloTSPrint.ZipAllBytes(dicBytes, NewFileName, gloGlobal.gloTSPrint.NoOfPages)
            For i As Integer = 0 To ZipedFiles.Count - 1
                Dim physicalDoc As New gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo()
                physicalDoc.PdfFileName = ZipedFiles(i)
                physicalDoc.SrcFileName = ZipedFiles(i)
                physicalDoc.footerInfo = Nothing
                SplitDocList.Add(physicalDoc)
            Next


            Return SplitDocList
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function getTSPrintDialogDetails(ByRef result As Boolean) As gloClinicalQueueGeneral.QueueDocumentDocumentDetails
        Dim popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing
        result = True
        If gloGlobal.gloTSPrint.isCopyPrint AndAlso (Not gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False)) Then
            If gloGlobal.gloTSPrint.isMapped() Then
                If _isTSPrinterSelectionOpen Then
                    result = False
                    Return Nothing
                End If
                _isTSPrinterSelectionOpen = True
                Dim tsPrintDialog As New gloPrintDialog.frmTSPrintDialog()
                tsPrintDialog.ShowDialog()
                _isTSPrinterSelectionOpen = False
                If tsPrintDialog.cancelPirnt = True Then
                    result = False
                    Return Nothing
                End If
                popUpDetails = New gloClinicalQueueGeneral.QueueDocumentDocumentDetails()
                popUpDetails.PrintFrom = tsPrintDialog.pageFrom
                popUpDetails.PrintTo = tsPrintDialog.pageTo
                popUpDetails.Printer = tsPrintDialog.currPrinterFile
                popUpDetails.Copies = tsPrintDialog.NoOfCopies
                popUpDetails.Landscape = tsPrintDialog.isLandscape
                popUpDetails.Duplex = tsPrintDialog.duplex
                popUpDetails.Size = tsPrintDialog.currSize
                popUpDetails.Tray = tsPrintDialog.currTray
                popUpDetails.isCollete = tsPrintDialog.isCollete
            End If
        End If
        Return popUpDetails
    End Function

    Private Shared Function getPDFForWord(aDoc As Interop.Word.Document) As List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)

        Try
            Dim SplitDocList As New List(Of gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo)()
            Dim PDFFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".pdf", "MMddyyyyHHmmssffff")
            aDoc.SaveAs(PDFFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)
            Dim physicalDoc As New gloClinicalQueueGeneral.gloQueueMetadatawriter.QueueDocumentInfo()
            physicalDoc.PdfFileName = PDFFileName
            physicalDoc.SrcFileName = PDFFileName
            physicalDoc.footerInfo = Nothing
            SplitDocList.Add(physicalDoc)


            Return SplitDocList
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function ObjectCopy(ByVal obj As Object) As Object
        If IsNothing(obj) = False Then
            Dim bytesRead As Byte() = CType(obj, Byte())
            Return bytesRead.Clone()
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function SaveWordFiletoBinary(ByRef wDSO As AxDSOFramer.AxFramerControl, ByRef thisDOC As Wd.Document, ByRef thisAPP As Wd.Application, ByVal strPathForCopy As String, Optional ByVal bToProtect As Boolean = False, Optional ByVal bToClose As Boolean = False) As Byte()
        Dim alreadyClosed As Boolean = False
        Dim alreadySaved As Boolean = False
        Dim strFileName As String = Nothing
        If (IsNothing(thisDOC) = False) Then
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try

                Try
                    thisAlertLevel = thisAPP.DisplayAlerts
                    thisAPP.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex2 As Exception

                End Try
                SaveDSO(wDSO, thisDOC, thisAPP)
                If (File.Exists(thisDOC.FullName)) Then
                    strFileName = thisDOC.FullName
                Else
                    strFileName = NewDocumentName(strPathForCopy)
                    thisDOC.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    alreadySaved = True
                End If
                If (bToClose) Then
                    wDSO.Close()
                    alreadyClosed = True
                    Try
                        Return ConvertFiletoBinary(strFileName)
                    Catch ex3 As Exception
                        Throw ex3
                        Return Nothing
                    End Try
                Else
                    Return ConvertFiletoBinary(strFileName)
                End If

            Catch ex As Exception

                Try
                    Dim strFileName2 As String = NewDocumentName(strPathForCopy)

                    Try
                        FileSystem.FileCopy(strFileName, strFileName2)
                        Return ConvertFiletoBinary(strFileName2)
                    Catch ex2 As Exception
                        'If (alreadyClosed) Then
                        '    strFileName2 = strFileName
                        'Else
                        '    If (alreadySaved) Then
                        '        strFileName2 = strFileName
                        '    Else
                        '        strFileName2 = NewDocumentName(strPathForCopy)
                        '        thisDOC.SaveAs(strFileName2, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                        '    End If
                        'End If

                        Dim PageNo As Integer = 0
                        If (bToClose = False) Then
                            Try
                                PageNo = thisAPP.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                            Catch expage As Exception

                            End Try
                        End If

                        If (alreadyClosed = False) Then
                            wDSO.Close()
                            alreadyClosed = True
                        End If

                        Try
                            Return ConvertFiletoBinary(strFileName)
                        Catch ex3 As Exception
                            Throw ex3
                            Return Nothing
                        Finally
                            If (bToClose = False) Then
                                OpenDSO(wDSO, strFileName, thisDOC, thisAPP, bToProtect)
                                Try
                                    If (PageNo > 0) Then
                                        Dim what As Object = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage
                                        Dim which As Object = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst
                                        Dim count As Object = PageNo
                                        Dim missing As Object = System.Reflection.Missing.Value
                                        Try
                                            thisAPP.Selection.[GoTo](what, which, count, missing)
                                        Catch

                                        End Try

                                    End If
                                Catch expage As Exception

                                End Try
                                SaveDSO(wDSO, thisDOC, thisAPP)
                            End If

                        End Try
                    Finally
                        If (bToClose) Then
                            If (alreadyClosed = False) Then
                                wDSO.Close()
                                alreadyClosed = True
                            End If
                        End If
                    End Try
                Catch ex4 As Exception
                    Throw ex4
                    Return Nothing
                Finally
                    If (bToClose) Then
                        If (alreadyClosed = False) Then
                            wDSO.Close()
                            alreadyClosed = True
                        End If
                    End If
                End Try
            Finally
                If (bToClose) Then
                    If (alreadyClosed = False) Then
                        wDSO.Close()
                        alreadyClosed = True
                    End If

                    If (IsNothing(thisDOC) = False) Then
                        Try
                            Marshal.ReleaseComObject(thisDOC)
                        Catch ex As Exception


                        End Try
                        thisDOC = Nothing

                    End If
                End If
                Try
                    thisAPP.DisplayAlerts = thisAlertLevel
                Catch ex2 As Exception

                End Try
            End Try
        Else
            Return Nothing
        End If

    End Function
    Public Shared Function SaveWordFiletoBinary(ByRef thisDOC As Wd.Document, ByVal strPathForCopy As String) As Byte()
        Dim alreadyClosed As Boolean = False
        Dim alreadySaved As Boolean = False
        Dim strFileName As String = Nothing
        If (IsNothing(thisDOC) = False) Then
            Dim thisAPP As Wd.Application = thisDOC.Application
            Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Try

                Try
                    thisAlertLevel = thisAPP.DisplayAlerts
                    thisAPP.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                Catch ex2 As Exception

                End Try

                Try
                    thisDOC.Save()
                Catch ex2 As Exception
                    'strFileName = NewDocumentName(strPathForCopy)
                    'thisDOC.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    'alreadySaved = True
                    SaveWord(thisDOC, thisAPP)
                End Try


                If (File.Exists(thisDOC.FullName)) Then
                    strFileName = thisDOC.FullName
                Else
                    strFileName = NewDocumentName(strPathForCopy)
                    thisDOC.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    alreadySaved = True
                End If
                Return ConvertFiletoBinary(strFileName)


            Catch ex As Exception

                Try
                    Dim strFileName2 As String = NewDocumentName(strPathForCopy)

                    Try
                        FileSystem.FileCopy(strFileName, strFileName2)
                        Return ConvertFiletoBinary(strFileName2)
                    Catch ex2 As Exception
                        'If (alreadySaved) Then
                        '    strFileName2 = strFileName
                        'Else
                        '    strFileName2 = NewDocumentName(strPathForCopy)
                        '    thisDOC.SaveAs(strFileName2, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                        'End If
                        Dim PageNo As Integer = 0

                        Try
                            PageNo = thisAPP.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                        Catch expage As Exception

                        End Try


                        If (alreadyClosed = False) Then
                            Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)

                            thisDOC.Close(SaveChanges:=mysaveoptions)
                            alreadyClosed = True
                        End If

                        Try
                            Return ConvertFiletoBinary(strFileName)
                        Catch ex3 As Exception
                            Throw ex3
                            Return Nothing
                        Finally

                            thisDOC = thisAPP.Documents.Open(strFileName)
                            Try
                                If (PageNo > 0) Then
                                    Dim what As Object = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage
                                    Dim which As Object = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst
                                    Dim count As Object = PageNo
                                    Dim missing As Object = System.Reflection.Missing.Value
                                    Try
                                        thisAPP.Selection.[GoTo](what, which, count, missing)
                                    Catch

                                    End Try
                                End If
                            Catch expage As Exception

                            End Try
                            Try
                                thisDOC.Save()
                            Catch exsave As Exception

                            End Try



                        End Try
                    Finally
                    End Try
                Catch ex4 As Exception
                    Throw ex4
                    Return Nothing
                Finally
                End Try
            Finally
                Try
                    thisAPP.DisplayAlerts = thisAlertLevel
                Catch ex2 As Exception

                End Try
            End Try
        Else
            Return Nothing
        End If

    End Function
    Public Shared Function ConvertFiletoBinary(ByVal strFileName As String) As Byte()
        If File.Exists(strFileName) Then
            Try
                Return ConvertFiletoBinaryWithShare(strFileName, False)
            Catch ex As IOException

                Try
                    Return ConvertFiletoBinaryWithShare(strFileName, True)
                Catch ex2 As IOException
                    Throw ex2
                    Return Nothing
                Catch ex2 As Exception
                    Throw ex2
                    Return Nothing
                End Try

            Catch ex As Exception

                Try
                    Return ConvertFiletoBinaryWithShare(strFileName, True)
                Catch ex2 As IOException
                    Throw ex2
                    Return Nothing
                Catch ex2 As Exception
                    Throw ex2
                    Return Nothing
                End Try

            End Try
        Else
            Return Nothing
        End If

    End Function
    Private Shared Function ConvertFiletoBinaryWithShare(ByVal strFileName As String, Optional ByVal bToShare As Boolean = False) As Byte()

        Dim oFile As FileStream = Nothing
        Dim oReader As BinaryReader = Nothing
        Try

            Try
                If (bToShare) Then
                    oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Else
                    oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)
                End If
            Catch ex2 As IOException
                Throw ex2
                Return Nothing
            Catch ex2 As Exception
                Throw ex2
                Return Nothing
            End Try

            oReader = New BinaryReader(oFile)
            Dim bytesRead As Byte() = oReader.ReadBytes(oFile.Length)
            Return bytesRead

        Catch ex As IOException
            Throw ex
            Return Nothing
        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally

            If (IsNothing(oReader) = False) Then
                oReader.Close()
                oReader.Dispose()
                oReader = Nothing
            End If

            If (IsNothing(oFile) = False) Then
                oFile.Close()
                oFile.Dispose()
                oFile = Nothing
            End If

        End Try

    End Function
    Private Shared Function RNGCharacterMask(ByVal maxSize As Integer) As String
        Dim AsciiChars As Char() = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray()
        Dim data As Byte() = New Byte(0) {}
        Dim crypto As New RNGCryptoServiceProvider()
        crypto.GetNonZeroBytes(data) 'SLR: Initialize the key
        data = New Byte(maxSize - 1) {}
        crypto.GetNonZeroBytes(data) 'SLR: Generate the key
        Dim result As New StringBuilder(maxSize)
        For Each b As Byte In data
            result.Append(AsciiChars(b Mod (AsciiChars.Length - 1)))
        Next
        crypto.Dispose()
        crypto = Nothing
        data = Nothing
        Return result.ToString()
    End Function
    Public Shared ReadOnly Property NewDocumentName(_Path As String, Optional ByVal strExtension As String = ".docx") As String
        Get
            'Dim _NewDocumentName As String = System.DateTime.Now.ToString("MM dd yyyy hh mm ss tt") & " " & RNGCharacterMask(8)
            'Dim _dtCurrentDateTimeAndGUID As String = [String].Join("", _NewDocumentName.Split(Path.GetInvalidFileNameChars()))
            ''_dtCurrentDateTimeAndGUID = Regex.Replace(_dtCurrentDateTimeAndGUID, "(.*?\\)", "")
            ''_dtCurrentDateTimeAndGUID = Regex.Replace(_dtCurrentDateTimeAndGUID, "['()\n]", "")
            '_NewDocumentName = Path.Combine(_Path, _dtCurrentDateTimeAndGUID & strExtension)
            'Dim i As Integer = 0
            'While File.Exists(_NewDocumentName) And (i < Integer.MaxValue)
            '    i = i + 1
            '    _NewDocumentName = Path.Combine(_Path, _dtCurrentDateTimeAndGUID & "-" & i.ToString() & strExtension)
            'End While
            'Return _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(_Path, strExtension, "MMddyyyyHHmmssffff")
        End Get
    End Property
    Public Shared Function ConvertFileFromBinary(ByVal cntFromDB As Object, ByVal strPath As String, Optional ByVal convertToNewFormat As Boolean = True) As String

        If Not cntFromDB Is Nothing Then
            Dim content() As Byte = CType(cntFromDB, Byte())
            Dim contentLength As Integer = content.Length

            Dim header As String = ""
            Dim strFileName As String = NewDocumentName(strPath)
            Dim oldFileName As String = strFileName
            Dim tobeConverted As Boolean = False
            If (convertToNewFormat) Then
                If (contentLength > 5) Then
                    header = Conversion.Hex(content(0)) & Conversion.Hex(content(1)) & Conversion.Hex(content(2)) & Conversion.Hex(content(3)) & Conversion.Hex(content(4)) & Conversion.Hex(content(5))
                    If (header.ToLower() = "7b5c72746631") Then
                        oldFileName = NewDocumentName(strPath, ".rtf")
                        tobeConverted = True
                    Else
                        If (contentLength > 7) Then
                            header = header & Conversion.Hex(content(6)) & Conversion.Hex(content(7))
                            If (header.ToLower() = "d0cf11e0a1b11ae1") Then
                                oldFileName = NewDocumentName(strPath, ".doc")
                                tobeConverted = True
                            End If
                        End If
                    End If
                End If
            End If

            Dim oFile As New System.IO.FileStream(oldFileName, System.IO.FileMode.Create)
            If oFile Is Nothing Then
                Return ""
            End If
            oFile.Write(content, 0, contentLength)
            oFile.Flush()
            oFile.Close()
            If Not IsNothing(oFile) Then
                oFile.Dispose()
                oFile = Nothing
            End If
            If (convertToNewFormat AndAlso tobeConverted) Then
                ConvertToNewDocx(oldFileName, strFileName)
                Try
                    File.Delete(oldFileName)
                Catch ex As Exception

                End Try
            End If
            Return strFileName
        Else
            Return ""
        End If
    End Function
    Private Shared Function ConvertToNewDocx(ByVal strFileName As String, ByVal mysFileName As String) As String

        Dim mywordApplication As New Microsoft.Office.Interop.Word.Application()
        If mywordApplication IsNot Nothing Then
            Dim myoFileFormat As Object = CType(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, Object)
            Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
            Try
                Dim myDoc As Microsoft.Office.Interop.Word.Document = mywordApplication.Documents.Add(strFileName)
                If myDoc IsNot Nothing Then
                    Dim bClosed As Boolean = False
                    Dim bSaved As Boolean = False
                    Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Nothing
                    Try
                        thisAlertLevel = mywordApplication.DisplayAlerts
                        mywordApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                    Catch ex2 As Exception

                    End Try

                    Try
                        myDoc.SaveAs(mysFileName, myoFileFormat)
                        bSaved = True
                        Try
                            myDoc.Close(SaveChanges:=mysaveoptions)
                            bClosed = True
                        Catch ex As Exception

                        End Try

                        Try
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                        Catch ex As Exception

                        End Try
                    Catch ex As Exception
                        If (bSaved) Then
                            If (bClosed = False) Then
                                Try
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                                Catch ex2 As Exception

                                End Try
                            End If
                        Else

                            Try
                                myDoc.Close(mysaveoptions)
                                bClosed = True
                            Catch ex2 As Exception

                            End Try
                            Try
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                            Catch ex2 As Exception

                            End Try
                            File.Copy(strFileName, mysFileName)
                        End If

                    End Try
                    Try
                        mywordApplication.DisplayAlerts = thisAlertLevel
                    Catch ex2 As Exception

                    End Try
                Else
                    File.Copy(strFileName, mysFileName)
                End If
            Catch ex As Exception

            End Try
            mywordApplication.Application.Quit(SaveChanges:=mysaveoptions)
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mywordApplication)
            Catch ex As Exception

            End Try
        Else
            File.Copy(strFileName, mysFileName)
        End If
        Return mysFileName
    End Function
    Private Shared Function RepairDocx(ByVal strFileName As String) As String

        If (File.Exists(strFileName)) Then
            Dim DirectoryName As String = Path.GetDirectoryName(strFileName)
            Dim mysFileName As String = NewDocumentName(DirectoryName)
            Try
                File.Copy(strFileName, mysFileName)
            Catch ex As Exception
                mysFileName = strFileName
            End Try
            Dim mywordApplication As New Microsoft.Office.Interop.Word.Application()
            If mywordApplication IsNot Nothing Then
                Dim myoFileFormat As Object = CType(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, Object)
                Dim mysaveoptions As Object = CType(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Object)
                Dim myRepairOptions As Object = CType(True, Object)
                Try
                    Dim myDoc As Microsoft.Office.Interop.Word.Document = mywordApplication.Documents.OpenNoRepairDialog(FileName:=mysFileName, OpenAndRepair:=myRepairOptions)
                    If myDoc IsNot Nothing Then
                        Dim bClosed As Boolean = False
                        Dim bSaved As Boolean = False
                        Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Nothing
                        Try
                            thisAlertLevel = mywordApplication.DisplayAlerts
                            mywordApplication.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                        Catch ex2 As Exception

                        End Try

                        Try
                            myDoc.SaveAs(strFileName, myoFileFormat)
                            bSaved = True
                            Try
                                myDoc.Close(SaveChanges:=mysaveoptions)
                                bClosed = True
                            Catch ex As Exception

                            End Try

                            Try
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                            Catch ex As Exception

                            End Try
                        Catch ex As Exception
                            If (bSaved) Then
                                If (bClosed = False) Then
                                    Try
                                        System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                                    Catch ex2 As Exception

                                    End Try
                                End If
                            Else

                                Try
                                    myDoc.Close(mysaveoptions)
                                    bClosed = True
                                Catch ex2 As Exception

                                End Try
                                Try
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(myDoc)
                                Catch ex2 As Exception

                                End Try

                            End If

                        End Try
                        Try
                            mywordApplication.DisplayAlerts = thisAlertLevel
                        Catch ex2 As Exception

                        End Try

                    Else

                    End If
                Catch ex As Exception

                End Try
                mywordApplication.Application.Quit(SaveChanges:=mysaveoptions)
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(mywordApplication)
                Catch ex As Exception

                End Try

            End If

        End If
        Return strFileName
    End Function

End Class
Public Class frmDSOTest
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    ''''Private wdTest As New AxDSOFramer.AxFramerControl
    Public _ErrorMessage As String = ""

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(115, 0)
        Me.Name = ""
        Me.Opacity = 0
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = ""
        Me.ResumeLayout(False)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.Hide()
    End Sub

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
    End Property
    Private Function frmDSOTEst_SubLoad() As String
        Dim wdTest As New AxDSOFramer.AxFramerControl
        If (IsNothing(wdTest) = False) Then

            Me.Controls.Add(wdTest)
            Try
                wdTest.CreateNew("Word.Document")
                System.Threading.Thread.Sleep(500)
                'wdTest.Open("C:\Documents and Settings\Administrator\Desktop\New WinRAR archive.docx")

                Return ""
            Catch oEx As System.Reflection.TargetInvocationException
                Return "Please close the dialog box (if open) from other word application outside gloEMR"

            Catch oExCom As System.Runtime.InteropServices.COMException
                Return "Please close the dialog box (if open) from other word application outside gloEMR"

            Catch ex As Exception
                Return ex.ToString


            Finally
                Try
                    Me.Controls.Remove(wdTest)
                Catch ex As Exception

                End Try

                Try
                    wdTest.Close()
                Catch ex As Exception

                End Try
                Try
                    wdTest.Dispose()
                Catch ex As Exception

                End Try

                wdTest = Nothing
                'Me.Close()
            End Try
        Else
            Return "Unable to Create Word control"
            'Me.Close()
        End If
    End Function

    Private Sub frmDSOTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _ErrorMessage = frmDSOTEst_SubLoad()
        If (_ErrorMessage <> "") Then
            WordDialogBoxBackgroundCloser.CloseAnyWordDialogs()
            _ErrorMessage = frmDSOTEst_SubLoad()
        End If
        If (_ErrorMessage <> "") Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        End If
        Me.Close()

        'Dim wdTest As New AxDSOFramer.AxFramerControl
        'If (IsNothing(wdTest) = False) Then

        '    Me.Controls.Add(wdTest)
        '    Try
        '        wdTest.CreateNew("Word.Document")
        '        System.Threading.Thread.Sleep(500)
        '        'wdTest.Open("C:\Documents and Settings\Administrator\Desktop\New WinRAR archive.docx")

        '        Me.DialogResult = Windows.Forms.DialogResult.Yes
        '    Catch oEx As System.Reflection.TargetInvocationException
        '        _ErrorMessage = "Please close the dialog box (if open) from other word application outside gloEMR"
        '        Me.DialogResult = Windows.Forms.DialogResult.No
        '    Catch oExCom As System.Runtime.InteropServices.COMException
        '        _ErrorMessage = "Please close the dialog box (if open) from other word application outside gloEMR"
        '        Me.DialogResult = Windows.Forms.DialogResult.No
        '    Catch ex As Exception
        '        _ErrorMessage = ex.ToString
        '        Me.DialogResult = Windows.Forms.DialogResult.No

        '    Finally
        '        Try
        '            Me.Controls.Remove(wdTest)
        '        Catch ex As Exception

        '        End Try

        '        Try
        '            wdTest.Close()
        '        Catch ex As Exception

        '        End Try
        '        Try
        '            wdTest.Dispose()
        '        Catch ex As Exception

        '        End Try

        '        wdTest = Nothing
        '        Me.Close()
        '    End Try
        'Else
        '    Me.DialogResult = Windows.Forms.DialogResult.No
        '    _ErrorMessage = "Unable to Create Word control"
        '    Me.Close()
        'End If
        ''''End Try
        ''''Me.Close()
    End Sub
End Class

Public Class WordDialogBoxBackgroundCloser

    Private Shared tTimer As System.Timers.Timer
    Private Shared minExclusionStrLen As Integer = Integer.MaxValue
    Public Shared Property EnableDialogCloser As Boolean
        Get
            Return tTimer.Enabled
        End Get
        Set(value As Boolean)
            tTimer.Enabled = value
        End Set
    End Property

    Public Sub New(ByVal ExclusionStrings As ArrayList)
        MyCurrentProcessID = Process.GetCurrentProcess().Id
        'If Application.OpenForms.Count = 0 Then
        '    Throw New InvalidOperationException()
        'End If
        'Application.OpenForms(0).BeginInvoke(New Action(Function()
        '                                                    ' Enumerate windows to find dialogs
        '                                                    If cancelled = False Then
        '                                                        Dim callback As New EnumThreadWndProc(AddressOf checkWindow)
        '                                                        EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero)
        '                                                        GC.KeepAlive(callback)
        '                                                    End If
        '                                                End Function))

        strExclusionStrings = ExclusionStrings
        For Each myString As String In strExclusionStrings 'myExclusionStrings
            Dim thisLen As Integer = myString.Length()
            If (minExclusionStrLen > thisLen) Then
                minExclusionStrLen = thisLen
            End If
        Next

        tTimer = New System.Timers.Timer()
        tTimer.Interval = 2000 * 5
        AddHandler tTimer.Elapsed, New ElapsedEventHandler(AddressOf MyHandler)
        tTimer.Enabled = True
        tTimer.Start()
    End Sub
    Const MYDIALOGCLASS = "#32770"
    Const MYDIALOGWORD = "Microsoft Word"
    Const MYDIALOGSAVEAS = "Save As"
    Const MYPRINTDIALOG = "Print"
    Const MYPRINTSETUP = "Print Setup"
    Const MYPRINTCLASS = "bosa_sdm_msword"
    Const MYPRINTSUBCLASS = "bosa_sdm"
    Const MYNUICLASS = "NUIDialog"
    Const MYDRAGONDIALOG = "<???>"
    Const MYRDPIDLETIMER = "Idle timer expired"
    Const WM_CLOSE = &H10
    Const WM_SETREDRAW = &HB
    Const WM_LBUTTONDOWN = &H201
    Const WM_LBUTTONUP = &H202
    Const WM_KEYDOWN = &H100
    Const WM_KEYUP = &H101
    Const WM_CHAR = &H102
    Const VK_TAB = &H9
    Const VK_ENTER = &HD
    Const VK_UP = &H26
    Const VK_DOWN = &H28
    Const VK_RIGHT = &H27


    Private Sub MyHandler(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        If cancelled = False Then
            tTimer.Enabled = False
            tTimer.Stop()
            Dim hWin As IntPtr = Nothing
            Dim toLookForInactiveWindow As Boolean = True
            'Try
            '    hWin = GetChildWindowHandle(MYDIALOGCLASS, MYDRAGONDIALOG, False, True)
            '    If hWin <> IntPtr.Zero Then
            '        SetFocusToWordWindow(hWin, Nothing)

            '    End If
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            '    ex = Nothing
            'End Try
            Try
                hWin = GetChildWindowHandle(MYPRINTCLASS, MYPRINTDIALOG, True)
                If (hWin <> IntPtr.Zero) AndAlso (IsWindowVisible(hWin)) Then
                    bringWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYPRINTCLASS, MYPRINTSETUP, True)
                If hWin <> IntPtr.Zero Then
                    bringWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYDIALOGCLASS, MYDIALOGWORD)
                If hWin <> IntPtr.Zero Then
                    clickkWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYDIALOGCLASS, MYDIALOGSAVEAS)
                If hWin <> IntPtr.Zero Then
                    cancelWindow(hWin, Nothing)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYDIALOGCLASS, MYPRINTDIALOG)
                If (hWin <> IntPtr.Zero) AndAlso (IsWindowVisible(hWin)) Then
                    bringWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYDIALOGCLASS, MYPRINTSETUP)
                If hWin <> IntPtr.Zero Then
                    bringWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                hWin = GetChildWindowHandle(MYDIALOGCLASS, MYRDPIDLETIMER)
                If hWin <> IntPtr.Zero Then
                    clickOkWindow(hWin, True)
                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try

                hWin = GetChildWindowHandle(MYNUICLASS, MYDIALOGWORD)
                If hWin <> IntPtr.Zero Then
                    If (isPrintingOn) Then
                        closeWindowOrSaveWindow(hWin, Nothing)
                    Else
                        bringWindow(hWin, True)
                    End If

                    toLookForInactiveWindow = False
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                'If (toLookForInactiveWindow) Then
                If (GetSameWindowHandle(MYPRINTCLASS, False)) Then
                    toLookForInactiveWindow = False
                End If
                ' End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                '  If (toLookForInactiveWindow) Then
                If (GetSameWindowHandle(MYNUICLASS, False)) Then
                    toLookForInactiveWindow = False
                End If
                '   End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            Try
                If (toLookForInactiveWindow) Then
                    bringInactivateWindow()
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            If (IsNothing(tTimer) = False) Then
                tTimer.Enabled = True
                tTimer.Start()
            End If

        End If
    End Sub

    Public Sub Dispose()
        cancelled = True
        tTimer.Enabled = False
        tTimer.Stop()
        RemoveHandler tTimer.Elapsed, New Timers.ElapsedEventHandler(AddressOf MyHandler)
        tTimer.Dispose()
        tTimer = Nothing
    End Sub

    'Public Shared myExclusionStrings As [String]() _
    ' = {"There are too many spelling or grammatical errors".ToLower(), _
    '     "outside the printable area of the page".ToLower(), _
    '    "Word cannot establish a network connection with this document after the system resumed from suspend mode.  Save the document into a different file to keep any changes".ToLower(), _
    '    "caused a serious error the last time it was opened".ToLower()}

    Private Shared strExclusionStrings As ArrayList
    Private Shared logExclusionStrings As ArrayList = New ArrayList()
    Private Shared maxLimit As Integer = 128

    Public Shared Sub UpdateWordLog(ByVal strLogMessage As String)
        Dim myIndex As Integer = -1
        If (logExclusionStrings.Count > maxLimit) Then
            logExclusionStrings.Clear()
        End If
        For thisLog As Integer = 0 To logExclusionStrings.Count - 1
            If (logExclusionStrings(thisLog) = strLogMessage) Then
                Exit Sub
            Else
                If (logExclusionStrings(thisLog) > strLogMessage) Then
                    myIndex = thisLog
                    Exit For
                End If
            End If
        Next
        If (myIndex = -1) Then
            logExclusionStrings.Insert(logExclusionStrings.Count, strLogMessage)
        Else
            logExclusionStrings.Insert(myIndex, strLogMessage)
        End If
        Try
            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\WordMessages.log", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Function clickkWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean

        'Dim int32Parent = hWnd.ToInt32()
        Dim myParentLen As Integer = GetWindowTextLength(hWnd) + 1
        If (myParentLen > 1) Then


            Dim sbParent As New System.Text.StringBuilder(myParentLen + 1)
            GetWindowText(hWnd, sbParent, myParentLen)
            Dim parentTitle As String = sbParent.ToString()


            If parentTitle = MYDIALOGWORD Then

                Dim childHandles() As IntPtr = GetChildWindows(hWnd)
                Dim OkHandle As IntPtr = 0
                Dim YesHandle As IntPtr = 0
                Dim SaveHandle As IntPtr = 0
                Dim toCloseHandle As Boolean = False
                For Each hMessage As IntPtr In childHandles

                    'Dim int32Handle = hMessage.ToInt32()
                    Dim mylen As Integer = GetWindowTextLength(hMessage) + 1

                    If (mylen > 1) Then
                        Dim sbText As New System.Text.StringBuilder(mylen + 1)
                        GetWindowText(hMessage, sbText, mylen)
                        Dim thisTitle As String = sbText.ToString().ToLower().Replace("&", "")

                        If (thisTitle = "ok") Then
                            OkHandle = hMessage
                        Else
                            If (thisTitle = "yes") Then
                                YesHandle = hMessage
                            Else
                                If (thisTitle = "save") Then
                                    SaveHandle = hMessage
                                Else
                                    If (thisTitle.Length >= minExclusionStrLen) Then
                                        For Each myString As String In strExclusionStrings 'myExclusionStrings
                                            If thisTitle.IndexOf(myString) >= 0 Then

                                                toCloseHandle = True

                                            End If
                                        Next

                                    End If
                                    If (toCloseHandle = False) Then
                                        If Len(thisTitle) > 10 Then
                                            UpdateWordLog("From Microsoft Word Dialog: " & thisTitle)
                                            '   bringWindow(hWnd, Nothing)
                                        End If
                                    End If
                                End If

                            End If
                        End If
                    End If
                Next
                If (toCloseHandle) Then

                    Dim AppHandle As IntPtr = IntPtr.Zero

                    Dim iProcessID As Integer
                    GetWindowThreadProcessId(hWnd, iProcessID)
                    Dim pProcess As Process = Process.GetProcessById(iProcessID)
                    If (IsNothing(pProcess) = False) Then
                        AppHandle = pProcess.MainWindowHandle
                        pProcess = Nothing
                    End If



                    If (OkHandle <> IntPtr.Zero) Then
                        SendMessage(OkHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                        SendMessage(OkHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                    Else
                        If (YesHandle <> IntPtr.Zero) Then
                            SendMessage(YesHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                            SendMessage(YesHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                        Else
                            If (SaveHandle <> IntPtr.Zero) Then
                                SendMessage(SaveHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                                SendMessage(SaveHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                            Else
                                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                            End If

                        End If
                    End If
                    If (AppHandle <> IntPtr.Zero) Then
                        ShowWindow(AppHandle, ShowWindowCommands.Hide)
                    Else
                        Dim panotherProcess As Process = Process.GetProcessById(iProcessID)
                        If (IsNothing(panotherProcess) = False) Then
                            AppHandle = panotherProcess.MainWindowHandle
                            If (AppHandle <> IntPtr.Zero) Then
                                ShowWindow(AppHandle, ShowWindowCommands.Hide)
                            End If
                            panotherProcess = Nothing
                        End If
                    End If

                Else
                    bringWindow(hWnd, True)
                End If
            End If
        End If
        Return True
    End Function
    Private Shared Function clickOkWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean

        'Dim int32Parent = hWnd.ToInt32()
        Dim myParentLen As Integer = GetWindowTextLength(hWnd) + 1
        If (myParentLen > 1) Then


            Dim sbParent As New System.Text.StringBuilder(myParentLen + 1)
            GetWindowText(hWnd, sbParent, myParentLen)
            Dim parentTitle As String = sbParent.ToString()


            If parentTitle = MYRDPIDLETIMER Then

                Dim childHandles() As IntPtr = GetChildWindows(hWnd)
                Dim OkHandle As IntPtr = 0
                Dim toCloseHandle As Boolean = False
                For Each hMessage As IntPtr In childHandles

                    'Dim int32Handle = hMessage.ToInt32()
                    Dim mylen As Integer = GetWindowTextLength(hMessage) + 1

                    If (mylen > 1) Then
                        Dim sbText As New System.Text.StringBuilder(mylen + 1)
                        GetWindowText(hMessage, sbText, mylen)
                        Dim thisTitle As String = sbText.ToString().ToLower().Replace("&", "")

                        If (thisTitle = "ok") Then
                            OkHandle = hMessage
                        Else
                            If (thisTitle.Length >= minExclusionStrLen) Then
                                For Each myString As String In strExclusionStrings 'myExclusionStrings
                                    If thisTitle.IndexOf(myString) >= 0 Then

                                        toCloseHandle = True

                                    End If
                                Next

                            End If
                            If (toCloseHandle = False) Then
                                If Len(thisTitle) > 10 Then
                                    UpdateWordLog("From Rdp Idle Timer: " & thisTitle)
                                    '   bringWindow(hWnd, Nothing)
                                End If
                            End If
                        End If
                    End If
                Next
                If (toCloseHandle) Then

                    If (OkHandle <> IntPtr.Zero) Then
                        SendMessage(OkHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                        SendMessage(OkHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                    Else
                        SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                    End If

                Else
                    bringWindow(hWnd, True)
                End If
            End If
        End If
        Return True
    End Function

    Private Shared Function closeWindowOrSaveWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean


        Dim childHandles() As IntPtr = GetChildWindows(hWnd)
        Dim OkHandle As IntPtr = 0
        Dim YesHandle As IntPtr = 0
        Dim SaveHandle As IntPtr = 0
        Dim toCloseHandle As Boolean = False
        For Each hMessage As IntPtr In childHandles


            'Dim int32Handle = hMessage.ToInt32()
            Dim mylen As Integer = GetWindowTextLength(hMessage) + 1

            If (mylen > 1) Then
                Dim sbText As New System.Text.StringBuilder(mylen + 1)
                GetWindowText(hMessage, sbText, mylen)
                Dim thisTitle As String = sbText.ToString().ToLower().Replace("&", "")

                If (thisTitle = "ok") Then
                    OkHandle = hMessage
                Else
                    If (thisTitle = "yes") Then
                        YesHandle = hMessage
                    Else
                        If (thisTitle = "save") Then
                            SaveHandle = hMessage
                        Else
                            If (thisTitle.Length >= minExclusionStrLen) Then
                                For Each myString As String In strExclusionStrings 'myExclusionStrings
                                    If thisTitle.IndexOf(myString) >= 0 Then

                                        toCloseHandle = True

                                    End If
                                Next

                            End If
                            If (toCloseHandle = False) Then
                                If Len(thisTitle) > 10 Then
                                    UpdateWordLog("From NUI Dialog:" & thisTitle)
                                    '   bringWindow(hWnd, Nothing)
                                End If
                            End If
                        End If

                    End If
                End If
            End If
        Next
        If (OkHandle <> IntPtr.Zero) Then
            SendMessage(OkHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
            SendMessage(OkHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
        Else
            If (YesHandle <> IntPtr.Zero) Then
                SendMessage(YesHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                SendMessage(YesHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
            Else
                If (SaveHandle <> IntPtr.Zero) Then
                    SendMessage(SaveHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                    SendMessage(SaveHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                Else
                    bringWindow(hWnd, True)
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                    LoadAndCloseWord.ClearWordGarbage()
                End If
            End If
        End If

        Return True
    End Function
    Private Shared Function cancelWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean


        'Dim int32Parent = hWnd.ToInt32()
        Dim myParentLen As Integer = GetWindowTextLength(hWnd) + 1
        If (myParentLen > 1) Then


            Dim sbParent As New System.Text.StringBuilder(myParentLen + 1)
            GetWindowText(hWnd, sbParent, myParentLen)
            Dim parentTitle As String = sbParent.ToString()


            If parentTitle = MYDIALOGSAVEAS Then

                Dim childHandles() As IntPtr = GetChildWindows(hWnd)


                For Each hMessage As IntPtr In childHandles

                    'Dim int32Handle = hMessage.ToInt32()
                    Dim mylen As Integer = GetWindowTextLength(hMessage) + 1

                    If (mylen > 1) Then
                        Dim sbText As New System.Text.StringBuilder(mylen + 1)
                        GetWindowText(hMessage, sbText, mylen)
                        Dim thisTitle As String = sbText.ToString().ToLower().Replace("&", "")

                        If (thisTitle = "cancel") Then
                            Dim AppHandle As IntPtr = IntPtr.Zero

                            Dim iProcessID As Integer
                            GetWindowThreadProcessId(hWnd, iProcessID)
                            Dim pProcess As Process = Process.GetProcessById(iProcessID)
                            Dim toCancel As Boolean = False
                            If (IsNothing(pProcess) = False) Then
                                AppHandle = pProcess.MainWindowHandle
                                If (String.IsNullOrEmpty(pProcess.MainWindowTitle)) Then
                                    toCancel = pProcess.ProcessName.ToLower().Contains("winword")
                                Else
                                    If (pProcess.MainWindowTitle.Contains("~gloStream_")) Then
                                        toCancel = pProcess.ProcessName.ToLower().Contains("winword")
                                    End If
                                End If
                                '   pProcess = Nothing
                            Else
                                toCancel = True
                            End If
                            If (toCancel) Then
                                SendMessage(hMessage, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero)
                                SendMessage(hMessage, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero)
                                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                                LoadAndCloseWord.ClearWordGarbage()
                                If (AppHandle <> IntPtr.Zero) Then
                                    ShowWindow(AppHandle, ShowWindowCommands.Hide)
                                Else
                                    Dim panotherProcess As Process = Process.GetProcessById(iProcessID)
                                    If (IsNothing(panotherProcess) = False) Then
                                        AppHandle = panotherProcess.MainWindowHandle
                                        If (AppHandle <> IntPtr.Zero) Then
                                            ShowWindow(AppHandle, ShowWindowCommands.Hide)
                                        End If
                                        panotherProcess = Nothing
                                    End If
                                End If
                                pProcess = Nothing
                                Exit For
                            Else
                                If (IsNothing(pProcess) = False) Then
                                    If (String.IsNullOrEmpty(pProcess.MainWindowTitle)) Then
                                        UpdateWordLog("Save As Process: " & pProcess.ProcessName & " : " & "No title")
                                    Else
                                        UpdateWordLog("Save As Process: " & pProcess.ProcessName & " : " & pProcess.MainWindowTitle)
                                        If (pProcess.MainWindowTitle.Contains("~gloStream~")) Then
                                            If (pProcess.ProcessName.ToLower().Contains("winword")) Then
                                                bringWindow(hWnd, True)
                                                pProcess = Nothing
                                                Exit For
                                            End If
                                        End If
                                    End If
                                    pProcess = Nothing
                                End If

                                '  bringWindow(hWnd, Nothing)
                            End If

                        End If

                    End If
                Next

            End If
        End If
        Return True
    End Function
    Shared Function UpdateScreenOfControl(ByVal thisControl As Control, ByVal bResumeOrSuspend As Boolean) As Boolean
        Try
            'SLR: We can use LockWindowUpdate for single thread applicaiton
            SendMessage(thisControl.Handle, WM_SETREDRAW, bResumeOrSuspend, 0)
            If (bResumeOrSuspend) Then
                'InvalidateRect(thisControl.Handle, Nothing, True)
                'UpdateWindow(thisControl.Handle)
                'SLR: ideally both should be refreshing, But not refreshing, hence used, builtin refresh which is a costly call.
                thisControl.Refresh()
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
        End Try
    End Function
    'Private Function SetFocusToWordWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean
    '    Dim runningProcessDragon As Boolean = False
    '    Try
    '        Dim iProcessID As Integer
    '        GetWindowThreadProcessId(hWnd, iProcessID)
    '        Dim pProcess As Process = Process.GetProcessById(iProcessID)
    '        If (IsNothing(pProcess) = False) Then
    '            If (pProcess.ProcessName.ToLower().Contains("natspeak")) Then
    '                Dim int32Handle = hWnd.ToInt32()
    '                Dim mylen As Integer = GetWindowTextLength(int32Handle) + 1
    '                'If (mylen > 1) Then
    '                '    Dim sbText As New System.Text.StringBuilder(mylen)
    '                '    GetWindowText(int32Handle, sbText, mylen)
    '                '    ' UpdateWordLog(sbText.ToString() & " " & DateAndTime.Now.Ticks.ToString())
    '                'End If

    '                If (mylen >= 3) Then
    '                    runningProcessDragon = True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try


    '    If (runningProcessDragon) Then

    '        Dim thisApplication As Wd.Application = Nothing
    '        Try
    '            thisApplication = CType(Marshal.GetActiveObject("Word.Application"), Wd.Application)
    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '            ex = Nothing
    '        End Try


    '        If (IsNothing(thisApplication) = False) Then
    '            'Try
    '            '    thisApplication.Activate()
    '            'Catch ex As Exception

    '            'End Try
    '            'Try
    '            '    If (IsNothing(thisApplication.ActiveWindow) = False) Then
    '            '        thisApplication.ActiveWindow.Activate()
    '            '        Try
    '            '            thisApplication.ActiveWindow.SetFocus()

    '            '        Catch ex1 As Exception

    '            '        End Try
    '            '    End If


    '            'Catch ex As Exception

    '            'End Try
    '            Try

    '                If (IsNothing(thisApplication.ActiveDocument) = False) Then
    '                    Dim myDocument As Wd.Document = thisApplication.ActiveDocument
    '                    If (activateWindow(myDocument)) Then
    '                        Return True
    '                    End If
    '                End If


    '                Dim myDocuments As Wd.Documents = thisApplication.Documents
    '                If (IsNothing(myDocuments) = False) Then
    '                    For Each myDocument As Wd.Document In myDocuments
    '                        If (activateWindow(myDocument)) Then
    '                            Return True
    '                        End If
    '                    Next
    '                End If





    '            Catch ex As Exception
    '                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '                ex = Nothing
    '            End Try

    '        End If
    '    End If
    '    Return False

    'End Function
    Public Shared Function SetFocusToEditableWordWindow(ByVal myForm As Form, ByVal bChild As Boolean, ByRef activeDSO As AxDSOFramer.AxFramerControl) As Boolean

        Dim thisApplication As Wd.Application = Nothing
        Try
            thisApplication = CType(Marshal.GetActiveObject("Word.Application"), Wd.Application)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try


        If (IsNothing(thisApplication) = False) Then

            Try

                If (IsNothing(thisApplication.ActiveDocument) = False) Then
                    Dim myDocument As Wd.Document = thisApplication.ActiveDocument
                    If (activateWindow(myDocument, myForm, bChild, activeDSO)) Then
                        Return True
                    End If
                End If


                Dim myDocuments As Wd.Documents = thisApplication.Documents
                If (IsNothing(myDocuments) = False) Then
                    For Each myDocument As Wd.Document In myDocuments
                        If (activateWindow(myDocument, myForm, bChild, activeDSO)) Then
                            Return True
                        End If
                    Next
                End If





            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

        End If
        activeDSO = Nothing
        Return False

    End Function
    Private Shared Function activateWindow(ByRef wdDocument As Wd.Document, ByVal myOpenForm As Form, ByVal bChild As Boolean, ByRef activeDSO As AxDSOFramer.AxFramerControl) As Boolean
        Try
            If (wdDocument.ProtectionType = Interop.Word.WdProtectionType.wdNoProtection) Then
                wdDocument.Activate()
                If (IsNothing(wdDocument.ActiveWindow) = False) Then
                    wdDocument.ActiveWindow.SetFocus()
                    If (IsNothing(myOpenForm)) Then

                        For Each myForm As Form In Application.OpenForms
                            If (myForm.ContainsFocus) Then
                                Dim thisDSO As AxDSOFramer.AxFramerControl = getDSOFramerControl(wdDocument.FullName, myForm)
                                If (IsNothing(thisDSO) = False) Then
                                    If (IsNothing(myForm) = False) Then

                                        bringToTop(myForm, bChild)

                                    End If
                                    thisDSO.Activate()
                                    activeDSO = thisDSO
                                    Return True
                                End If

                            End If
                        Next
                        If (Application.OpenForms.Count) > 1 Then
                            Dim thisDSO As AxDSOFramer.AxFramerControl = getDSOFramerControl(wdDocument.FullName, Application.OpenForms(Application.OpenForms.Count - 1))
                            If (IsNothing(thisDSO) = False) Then
                                bringToTop(Application.OpenForms(Application.OpenForms.Count - 1), bChild)
                                thisDSO.Activate()
                                activeDSO = thisDSO
                                Return True
                            End If
                        End If
                    Else

                        Dim thisDSO As AxDSOFramer.AxFramerControl = getDSOFramerControl(wdDocument.FullName, myOpenForm)
                        If (IsNothing(thisDSO) = False) Then
                            If (IsNothing(myOpenForm) = False) Then

                                bringToTop(myOpenForm, bChild)

                            End If
                            thisDSO.Activate()
                            activeDSO = thisDSO
                            Return True
                        End If
                    End If

                    Return True
                End If
            End If
        Catch ex As Exception

        End Try

        Return False
    End Function
    Private Shared Function getDSOFramerControl(ByVal strDocumentName As String, ByVal thisControl As Control) As Control
        If (IsNothing(thisControl) = False) Then
            For Each myControl As Control In thisControl.Controls
                If TypeOf myControl Is AxDSOFramer.AxFramerControl Then
                    Dim thisDSO As AxDSOFramer.AxFramerControl = TryCast(myControl, AxDSOFramer.AxFramerControl)
                    If (IsNothing(thisDSO.ActiveDocument) = False) Then
                        Dim myString As String = thisDSO.ActiveDocument.FullName
                        If (myString = strDocumentName) Then
                            Return thisDSO
                        End If
                    End If

                End If
                Dim otherDSO As AxDSOFramer.AxFramerControl = getDSOFramerControl(strDocumentName, myControl)
                If (IsNothing(otherDSO) = False) Then
                    Return otherDSO
                End If
            Next
        End If

        Return Nothing
    End Function
    Private Shared maxHowManyTimes As Integer = 4
    Private Shared howManyTimesToBring As Integer = maxHowManyTimes
    Private Shared lastWind As IntPtr = IntPtr.Zero
    Private Shared lastTime As System.DateTime = System.DateTime.Now()
    Private Shared maxWaitTime As Long = 2000 * maxHowManyTimes * 30
    Private Shared Function bringWindow(ByVal hWnd As IntPtr, ByVal lp As Boolean) As Boolean
        If ((lastWind <> hWnd) OrElse (howManyTimesToBring > 0)) Then
            If (IsWindowTopMost(hWnd)) Then ' And (lastWind = hWnd) Then
                Return True
            Else
                If (lp) Then
                    ShowWindowAsync(hWnd, ShowWindowCommands.ShowDefault)
                End If

                ShowWindowAsync(hWnd, ShowWindowCommands.Show)
                SetForegroundWindow(hWnd)
                If (lp) Then
                    MakeTopMost(hWnd)
                    InvalidateRect(hWnd, IntPtr.Zero, True)
                    UpdateWindow(hWnd)
                End If


            End If

            If (lastWind <> hWnd) Then
                howManyTimesToBring = maxHowManyTimes
                lastWind = hWnd
            Else
                howManyTimesToBring = howManyTimesToBring - 1
            End If
            Return True
        Else

            Dim curSeconds As TimeSpan = System.DateTime.Now.Subtract(lastTime)
            If (curSeconds.Ticks > maxWaitTime) Then
                howManyTimesToBring = maxHowManyTimes
                lastTime = System.DateTime.Now()
            End If
        End If


        Return False
    End Function

    Public Shared Function bringToTop(ByVal myForm As Form, ByVal bChild As Boolean) As Boolean

        'If bChild Then
        '    bringToTop = True
        '    Exit Function
        'End If

        Dim hWnd As IntPtr = IntPtr.Zero
        If (bChild = False) Then
            If (IsNothing(myForm.ParentForm) = False) Then
                If (IsNothing(myForm.ParentForm.Handle) = False) Then
                    hWnd = myForm.ParentForm.Handle
                End If
            End If
            If (hWnd = IntPtr.Zero) Then
                If (IsNothing(myForm.Parent) = False) Then
                    If (IsNothing(myForm.Parent.Handle) = False) Then
                        hWnd = myForm.Parent.Handle
                    End If
                End If
            End If
        End If

        If (hWnd = IntPtr.Zero) Then
            If (IsNothing(myForm) = False) Then
                If (IsNothing(myForm.Handle) = False) Then
                    hWnd = myForm.Handle
                End If
            End If
        End If
        If (hWnd = IntPtr.Zero) Then
            Return False
        End If
        If (IsWindowTopMost(hWnd)) Then
            Return True
        Else
            If (bChild = False) Then
                ShowWindowAsync(hWnd, ShowWindowCommands.Show)
            End If

            SetForegroundWindow(hWnd)

        End If



        Return False
    End Function
    Private MaxDontLook As Integer = 4
    Private DontLooks As Integer = 0
    Private Function bringInactivateWindow() As Boolean
        If (isPrintingOn) Then
            Return False
        End If
        If (DontLooks < MaxDontLook) Then
            DontLooks = DontLooks + 1
            Return False
        Else
            DontLooks = 0
        End If
        For Each myForm As Form In Application.OpenForms
            If (myForm.ContainsFocus) Then
                '    UpdateWordLog(myForm.Name & " " & Now.Ticks.ToString() & " " & myForm.Name)
                Return True
            Else
                '  UpdateWordLog(Now.Ticks.ToString() & " No Focus " & myForm.Name)
            End If
        Next

        Dim allWindowHandles() As IntPtr = GetWindows()
        Dim toReturnTrue As Boolean = False
        For Each hWindowHandle As IntPtr In allWindowHandles
            '    'Dim iProcessID As Integer
            '    'GetWindowThreadProcessId(hMessage, iProcessID)
            '    'Dim pProcess As Process = Process.GetProcessById(iProcessID)

            '    'If (IsNothing(pProcess) = False) Then
            '    '    If (pProcess.MainWindowHandle <> IntPtr.Zero) Then
            '    '        If (pProcess.MainWindowTitle.Contains("Print")) Then
            '    '            Dim myInteger As Integer = 0
            '    '        End If

            '    '        Dim hParent = GetParent(pProcess.MainWindowHandle)
            '    '        If (hParent <> IntPtr.Zero) Then
            '    '            GetWindowThreadProcessId(hParent, iProcessID)
            '    '            pProcess = Process.GetProcessById(iProcessID)
            '    '            If (IsNothing(pProcess) = False) Then
            '    '                If (pProcess.MainWindowHandle <> IntPtr.Zero) Then
            '    '                    If (String.IsNullOrEmpty(pProcess.MainWindowTitle)) Then
            '    '                        If (pProcess.MainWindowTitle.Contains("~gloStream_")) Then
            '    '                            If (pProcess.ProcessName.ToLower().Contains("winword")) Then
            '    '                                If (SetFocusToWindow(hMessage, True, True)) Then
            '    '                                    Return True
            '    '                                End If
            '    '                            End If
            '    '                        End If
            '    '                    End If
            '    '                End If
            '    '            End If
            '    '        End If

            '    '    End If
            '    'End If
            If (SetFocusToWindow(hWindowHandle)) Then
                toReturnTrue = True
            End If
        Next
        If (toReturnTrue) Then
            Return True
        End If

        Dim hWnd As IntPtr = GetForegroundWindow()
        If (hWnd = IntPtr.Zero) Then
            hWnd = GetActiveWindow()
        End If
        If (hWnd <> IntPtr.Zero) Then
            If (SetFocusToWindow(hWnd)) Then
                Return True
            End If
        End If
        Return False

    End Function
    Public Shared Sub ForceWindowIntoForeground(ByVal hWnd As IntPtr)
        Dim currentThread As UInteger = GetCurrentThreadId()

        Dim activeWindow As IntPtr = GetForegroundWindow()
        Dim activeProcess As UInteger
        Dim activeThread As UInteger = GetWindowThreadProcessId(activeWindow, activeProcess)
        Dim windowProcess As UInteger
        Dim windowThread As UInteger = GetWindowThreadProcessId(hWnd, windowProcess)
        If currentThread <> activeThread Then
            AttachThreadInput(currentThread, activeThread, True)
        End If
        If windowThread <> currentThread Then
            AttachThreadInput(windowThread, currentThread, True)
        End If
        Dim oldTimeout As UInteger = 0, newTimeout As UInteger = 0
        SystemParametersInfo(SPI.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, oldTimeout, 0)
        SystemParametersInfo(SPI.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, newTimeout, 0)
        LockSetForegroundWindow(LSFW_UNLOCK)
        AllowSetForegroundWindow(ASFW_ANY)
        SetForegroundWindow(hWnd)
        ShowWindowAsync(hWnd, ShowWindowCommands.Show)
        SystemParametersInfo(SPI.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, oldTimeout, 0)
        If currentThread <> activeThread Then
            AttachThreadInput(currentThread, activeThread, False)
        End If
        If windowThread <> currentThread Then
            AttachThreadInput(windowThread, currentThread, False)
        End If
    End Sub
    '18-Aug-2015 Aniket: Resolving Bug #84080: EMR: Send Portal- Send portal window goes back as user unlock EMR screen
    Public Shared Function HideWindow(hHandle As System.IntPtr) As Boolean
        Return ShowWindow(hHandle, ShowWindowCommands.Hide)
    End Function

    Private Shared Function SetFocusToWindow(ByVal hMessage As IntPtr, Optional ByVal bLog As Boolean = True, Optional ByVal forceClose As Boolean = False) As Boolean

        If (hMessage <> IntPtr.Zero) Then
            Try
                Dim thisApiWindow As ApiWindow = New ApiWindow(hMessage)
                Try
                    If (thisApiWindow.ClassName.Contains(MYDIALOGCLASS) OrElse thisApiWindow.ClassName.Contains(MYPRINTSUBCLASS)) AndAlso (thisApiWindow.MainWindowTitle.Contains(MYPRINTDIALOG) OrElse thisApiWindow.MainWindowTitle.Contains(MYPRINTSETUP)) Then 'Or thisApiWindow.MainWindowTitle.Contains(MYPRINTDIALOG) Or thisApiWindow.MainWindowTitle.Contains(MYPRINTSETUP) Then
                        If (IsWindowVisible(hMessage)) Then
                            bringWindow(hMessage, True)
                            If (bLog) Then
                                UpdateWordLog("Bringing Active Window Title: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Window Class: " & thisApiWindow.ClassName)
                            End If
                            Return True
                        End If
                    End If
                    If (thisApiWindow.ClassName.Contains(MYDIALOGCLASS) OrElse thisApiWindow.ClassName.Contains(MYNUICLASS)) AndAlso thisApiWindow.MainWindowTitle.Contains(MYDIALOGWORD) Then
                        If (isPrintingOn OrElse forceClose) Then
                            closeWindowOrSaveWindow(hMessage, Nothing)
                        Else
                            bringWindow(hMessage, True)
                        End If
                        If (bLog) Then
                            UpdateWordLog("Closing & Saving Active Window Title: " & thisApiWindow.MainWindowTitle & vbTab & "Closing & Saving Active Window Class: " & thisApiWindow.ClassName)
                        End If
                        Return True
                    End If
                    If thisApiWindow.ClassName.Contains(MYDIALOGCLASS) AndAlso thisApiWindow.MainWindowTitle.Contains(MYDIALOGSAVEAS) Then
                        cancelWindow(hMessage, Nothing)
                        If (bLog) Then
                            UpdateWordLog("Cancelling Active Window Title: " & thisApiWindow.MainWindowTitle & vbTab & "Cancelling Active Window Class: " & thisApiWindow.ClassName)
                        End If
                        Return True
                    End If
                    If thisApiWindow.ClassName.Contains(MYDIALOGCLASS) AndAlso thisApiWindow.MainWindowTitle.Contains(MYRDPIDLETIMER) Then
                        clickOkWindow(hMessage, Nothing)
                        If (bLog) Then
                            UpdateWordLog("Okeying Active Window Title: " & thisApiWindow.MainWindowTitle & vbTab & "Okeying Active Window Class: " & thisApiWindow.ClassName)
                        End If
                        Return True
                    End If
                    If thisApiWindow.ClassName.Contains(MYDIALOGCLASS) Then
                        If (IsWindowVisible(hMessage)) Then
                            Dim iProcessID As Integer
                            GetWindowThreadProcessId(hMessage, iProcessID)
                            Dim pProcess As Process = Process.GetProcessById(iProcessID)
                            If (IsNothing(pProcess) = False) Then
                                Dim ProcessName As String = Nothing
                                If (IsNothing(pProcess.ProcessName) = False) Then
                                    ProcessName = pProcess.ProcessName.ToLower()
                                End If

                                If (IsNothing(ProcessName) = False) Then
                                    If (ProcessName.Contains("natspeak")) OrElse (ProcessName.Contains("taskmgr")) Then
                                        'Dragon natutrally speaking dialog or Task manager dialog: don't bring into front.
                                    Else
                                        If (ProcessName.Contains(MyProcessName)) Then
                                            bringWindow(hMessage, False)
                                            If (bLog) Then
                                                UpdateWordLog("Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                            End If
                                            pProcess = Nothing
                                            Return True
                                        Else
                                            If (ProcessName.Contains("winword")) Then
                                                bringWindow(hMessage, True)
                                                If (bLog) Then
                                                    UpdateWordLog("Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                                End If
                                                pProcess = Nothing
                                                Return True
                                            Else
                                                If (ProcessName.Contains("explore")) Then
                                                    If (thisApiWindow.MainWindowTitle.Contains("Patient Information for")) Then
                                                        If (bLog) Then
                                                            UpdateWordLog("Not Bringing Active Dialog Title : " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                                        End If
                                                        pProcess = Nothing
                                                        Return True
                                                    Else
                                                        If (gloGlobal.LoadFromAssembly.GetApplicationWindowState()) Then ''it will check if application is maximized 
                                                            bringWindow(hMessage, True)
                                                            If (bLog) Then
                                                                UpdateWordLog("Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                                            End If
                                                            pProcess = Nothing
                                                            Return True
                                                        Else
                                                            If (bLog) Then
                                                                UpdateWordLog("Not Bringing Active Dialog Title State Minimized: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                                            End If
                                                            pProcess = Nothing
                                                            Return True
                                                        End If
                                                    End If
                                                Else
                                                    ''added incident CAS-00281-F1Z6Z7 --screen goes blank
                                                    If (thisApiWindow.MainWindowTitle.Contains("Patient Information for")) Then
                                                        If (bLog) Then
                                                            UpdateWordLog("Else Part Not Bringing Active Dialog Title : " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                                        End If
                                                        pProcess = Nothing
                                                        Return True
                                                    End If

                                                    If (bLog) Then
                                                        UpdateWordLog("Not Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Not Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Not Bringing Active Process Name: " & pProcess.ProcessName)
                                                    End If
                                                    pProcess = Nothing
                                                    Return False

                                                End If

                                            End If

                                        End If

                                        'If (ProcessName.Contains(MyProcessName)) OrElse (ProcessName.Contains("explore")) OrElse (ProcessName.Contains("winword")) Then
                                        '    If (ProcessName.Contains("explore")) Then
                                        '        If (gloGlobal.LoadFromAssembly.GetApplicationWindowState()) Then ''it will check if application is maximized 
                                        '            bringWindow(hMessage, Not ProcessName.Contains(MyProcessName))
                                        '        End If
                                        '    Else
                                        '        bringWindow(hMessage, Not ProcessName.Contains(MyProcessName))
                                        '    End If
                                        '    If (bLog) Then
                                        '        UpdateWordLog("Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Bringing Active Process Name: " & pProcess.ProcessName)
                                        '    End If
                                        '    pProcess = Nothing
                                        '    Return True
                                        'Else
                                        '    If (bLog) Then
                                        '        UpdateWordLog("Not Bringing Active Dialog Title: " & thisApiWindow.MainWindowTitle & vbTab & "Not Bringing Active Dialog Class: " & thisApiWindow.ClassName & "Not Bringing Active Process Name: " & pProcess.ProcessName)
                                        '    End If
                                        '    pProcess = Nothing
                                        '    Return False
                                        'End If
                                    End If
                                End If

                            End If
                        End If
                    End If
                Catch ex As Exception

                End Try

            Catch ex2 As Exception

            End Try

        End If
        Return False
    End Function
    Private Shared natspeakWindowPointer As IntPtr = IntPtr.Zero
    Public Shared Function GetWindowContentByProcessName(ByVal processname As String) As String
        Dim retString As String = ""
        Try
            Dim allWindowHandles() As IntPtr = GetWindows()
            For Each hMessage As IntPtr In allWindowHandles
                If (hMessage <> IntPtr.Zero) Then
                    retString = GetWindowContentByHandle(hMessage, processname)
                    If retString <> "" Then
                        Exit For
                    End If
                End If
            Next
        Catch ex2 As Exception
            Return ""
        End Try
        Return retString
    End Function

    Public Shared Function GetWindowContentByHandle(ByVal hMessage As IntPtr, ByVal ProcessName As String) As String
        Dim retString As String = ""
        Try
            If (hMessage <> IntPtr.Zero) Then
                Dim thisApiWindow As ApiWindow = New ApiWindow(hMessage)
                Dim iProcessID As Integer
                GetWindowThreadProcessId(hMessage, iProcessID)
                Dim pProcess As Process = Process.GetProcessById(iProcessID)
                Dim _processname = pProcess.ProcessName.ToLower()
               If _processname.Contains(ProcessName) Then
                    If thisApiWindow.ClassName.Contains(MYDIALOGCLASS) And thisApiWindow.MainWindowTitle <> "Dragon Medical" Then
                        natspeakWindowPointer = hMessage
                        retString = thisApiWindow.MainWindowContent
                    End If
                End If
            End If
        Catch ex2 As Exception
            Return ""
        End Try
        Return retString
    End Function

    Public Shared Function GetNatSpeakWindowcontent() As String
        Try

            Dim strcontent As String = ""
            If natspeakWindowPointer = IntPtr.Zero Then
                strcontent = GetWindowContentByProcessName("natspeak")
            Else
                strcontent = GetWindowContentByHandle(natspeakWindowPointer, "natspeak")
            End If
            Return strcontent
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function CloseAnyWordDialogs() As Boolean


        Dim allWindowHandles() As IntPtr = GetWindows()
        Dim toReturnTrue As Boolean = False
        For Each hWindowHandle As IntPtr In allWindowHandles
            '    'Dim iProcessID As Integer
            '    'GetWindowThreadProcessId(hMessage, iProcessID)
            '    'Dim pProcess As Process = Process.GetProcessById(iProcessID)

            '    'If (IsNothing(pProcess) = False) Then
            '    '    If (pProcess.MainWindowHandle <> IntPtr.Zero) Then
            '    '        If (pProcess.MainWindowTitle.Contains("Print")) Then
            '    '            Dim myInteger As Integer = 0
            '    '        End If

            '    '        Dim hParent = GetParent(pProcess.MainWindowHandle)
            '    '        If (hParent <> IntPtr.Zero) Then
            '    '            GetWindowThreadProcessId(hParent, iProcessID)
            '    '            pProcess = Process.GetProcessById(iProcessID)
            '    '            If (IsNothing(pProcess) = False) Then
            '    '                If (pProcess.MainWindowHandle <> IntPtr.Zero) Then
            '    '                    If (String.IsNullOrEmpty(pProcess.MainWindowTitle)) Then
            '    '                        If (pProcess.MainWindowTitle.Contains("~gloStream_")) Then
            '    '                            If (pProcess.ProcessName.ToLower().Contains("winword")) Then
            '    '                                If (SetFocusToWindow(hMessage, True, True)) Then
            '    '                                    Return True
            '    '                                End If
            '    '                            End If
            '    '                        End If
            '    '                    End If
            '    '                End If
            '    '            End If
            '    '        End If

            '    '    End If
            '    'End If
            If (SetFocusToWindow(hWindowHandle, True, True)) Then
                toReturnTrue = True
            End If
        Next
        If (toReturnTrue) Then
            Return True
        End If

        Dim hWnd As IntPtr = GetForegroundWindow()
        If (hWnd = IntPtr.Zero) Then
            hWnd = GetActiveWindow()
        End If
        If (hWnd <> IntPtr.Zero) Then
            If (SetFocusToWindow(hWnd, True, True)) Then
                Return True
            End If
        End If
        Return False

    End Function
    Public Shared MyProcessName As String = Process.GetCurrentProcess().ProcessName.ToLower() ''slr
    '' added done for hiding dialog window when locking screen SLR
    Public Shared Function IsDialogWindowOpened() As IntPtr
        Dim allWindowHandles() As IntPtr = GetWindows()
        Dim toReturnTrue As Boolean = False
        For Each hWindowHandle As IntPtr In allWindowHandles

            If (hWindowHandle <> IntPtr.Zero) Then
                Try
                    Dim thisApiWindow As ApiWindow = New ApiWindow(hWindowHandle)
                    Try
                        If (thisApiWindow.ClassName.Contains(MYDIALOGCLASS)) Then
                            If (IsWindowVisible(hWindowHandle)) Then
                                Dim iProcessID As Integer
                                GetWindowThreadProcessId(hWindowHandle, iProcessID)
                                Dim pProcess As Process = Process.GetProcessById(iProcessID)
                                If (IsNothing(pProcess) = False) Then
                                    Dim ProcessName As String = Nothing
                                    If (IsNothing(pProcess.ProcessName) = False) Then
                                        ProcessName = pProcess.ProcessName.ToLower()
                                    End If
                                    If (IsNothing(ProcessName) = False) Then

                                        If (ProcessName.Contains(MyProcessName)) Then
                                            pProcess = Nothing
                                            Return hWindowHandle
                                        End If
                                    End If
                                End If
                                pProcess = Nothing
                            End If
                        End If

                    Catch ex As Exception

                    End Try

                Catch ex2 As Exception

                End Try

            End If
        Next
        Return IntPtr.Zero
    End Function

    Private Function GetActiveWordWindow() As IntPtr
        Dim wdAppCaption As String = "~gloStream_"
        ' Dim session As Integer = Process.GetCurrentProcess().SessionId
        Dim arrProcesses As Process() = Process.GetProcessesByName("WINWORD")
        If ((IsNothing(arrProcesses)) OrElse (arrProcesses.Length = 0)) Then
            Return IntPtr.Zero
        End If
        'SLR: Changed to reverse loop due to https://social.msdn.microsoft.com/Forums/en-US/f7d1749c-0cc2-4821-953c-89d518d804d1/getting-pid-of-created-ms-word-instance?forum=vblanguage
        For i As Integer = arrProcesses.Length - 1 To 0 Step -1
            Dim Proc As Process = arrProcesses(i)
            If (Proc.SessionId = LoadAndCloseWord.CurrentSessionID) Then
                If (Proc.MainWindowHandle <> IntPtr.Zero) Then
                    If Not String.IsNullOrEmpty(Proc.MainWindowTitle) Then
                        If Proc.MainWindowTitle.Contains(wdAppCaption) Then
                            arrProcesses = Nothing
                            Try
                                Return Proc.MainWindowHandle
                            Catch
                            Finally
                                Proc = Nothing
                            End Try
                        End If
                    End If
                End If
            End If
        Next
        arrProcesses = Nothing
        Return IntPtr.Zero
    End Function
    Private cancelled As Boolean = False



    ''' <summary>
    ''' Utility function using Windows API cals to loop though
    ''' child windows of Word and see if there are any that
    ''' match the passed in child class name. If there are
    ''' then we return the pointer to that window.
    ''' </summary>
    ''' <param name="winClass"></param>
    ''' <returns></returns>
    Private Function GetChildWindowHandle(ByVal winClass As String, ByVal winTitle As String, Optional ByVal toIgnoreClass As Boolean = False, Optional ByVal toIgnoreTitle As Boolean = False) As IntPtr
        Dim hWin As IntPtr = IntPtr.Zero
        hWin = FindWindow(winClass, winTitle)
        If (hWin = IntPtr.Zero) AndAlso (toIgnoreClass) Then

            hWin = FindWindow(Nothing, winTitle)

        End If
        If (hWin = IntPtr.Zero) AndAlso (toIgnoreTitle) Then

            hWin = FindWindow(winClass, Nothing)

        End If

        'While hWin <> IntPtr.Zero

        '    Dim int32Parent = hWin.ToInt32()
        '    Dim myParentLen As Integer = GetWindowTextLength(int32Parent) + 1
        '    If (myParentLen > 1) Then


        '        Dim sbParent As New System.Text.StringBuilder(myParentLen)
        '        GetWindowText(int32Parent, sbParent, myParentLen)
        '        Dim parentTitle As String = sbParent.ToString()


        '        If parentTitle = winTitle Then
        '            Exit While
        '        End If
        '    End If

        '    ' get next window
        '    hWin = FindWindowEx(IntPtr.Zero, hWin, winClass, [String].Empty)
        'End While
        Return hWin
    End Function
    Private Function GetSameWindowHandle(ByVal winClass As String, Optional ByVal toBringFront As Boolean = True) As Boolean
        Dim hWin As IntPtr = IntPtr.Zero
        hWin = FindWindowEx(IntPtr.Zero, hWin, winClass, Nothing)

        While hWin <> IntPtr.Zero

            'Dim int32Parent = hWin.ToInt32()
            Dim myParentLen As Integer = GetWindowTextLength(hWin) + 1
            If (myParentLen > 1) Then
                Dim sbParent As New System.Text.StringBuilder(myParentLen + 1)
                GetWindowText(hWin, sbParent, myParentLen)
                Dim parentTitle As String = sbParent.ToString()
                UpdateWordLog(winClass & " : " & parentTitle)
                If ((winClass = MYNUICLASS) AndAlso (parentTitle.Contains(MYDIALOGWORD))) Then
                    UpdateWordLog("Bringing to Front " & winClass & " : " & parentTitle)
                    bringWindow(hWin, True)
                    If (isPrintingOn) Then
                        UpdateWordLog("Clicking Window " & winClass & " : " & parentTitle)
                        clickkWindow(hWin, Nothing)
                        UpdateWordLog("Closing Window " & winClass & " : " & parentTitle)
                        SendMessage(hWin, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                    Else
                        UpdateWordLog("Printing is not on " & winClass & " : " & parentTitle)
                    End If

                    Return True
                End If
            End If
            If (toBringFront) Then
                bringWindow(hWin, True)
                Return True
            End If

            hWin = FindWindowEx(IntPtr.Zero, hWin, winClass, [String].Empty)
        End While
        Return False
    End Function

    Private Shared Function GetChildWindows(ByVal ParentHandle As IntPtr) As IntPtr()
        Dim ChildrenList As New List(Of IntPtr)
        Dim ListHandle As GCHandle = GCHandle.Alloc(ChildrenList)
        Try
            EnumChildWindows(ParentHandle, AddressOf EnumWindow, GCHandle.ToIntPtr(ListHandle))
        Finally
            If ListHandle.IsAllocated Then ListHandle.Free()
        End Try
        Return ChildrenList.ToArray
    End Function
    Private Shared Function GetWindows() As IntPtr()
        Dim ChildrenList As New List(Of IntPtr)
        Dim ListHandle As GCHandle = GCHandle.Alloc(ChildrenList)
        Try
            EnumWindows(AddressOf EnumWindow, GCHandle.ToIntPtr(ListHandle))
        Finally
            If ListHandle.IsAllocated Then ListHandle.Free()
        End Try
        Return ChildrenList.ToArray
    End Function

    Private Shared Function EnumWindow(ByVal Handle As IntPtr, ByVal Parameter As IntPtr) As Boolean
        Dim ChildrenList As List(Of IntPtr) = GCHandle.FromIntPtr(Parameter).Target
        If ChildrenList Is Nothing Then Throw New Exception("GCHandle Target could not be cast as List(Of IntPtr)")
        ChildrenList.Add(Handle)
        Return True
    End Function
    Public Shared Sub Close(ByVal thisForm As Form, Optional ByVal immediatelyCall As Boolean = False)
        If (immediatelyCall) Then
            SendMessage(thisForm.Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
        Else
            PostMessage(thisForm.Handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
        End If
    End Sub



    Private Sub SendEnter(ByVal Handle As IntPtr)
        PostMessage(Handle, WM_KEYDOWN, VK_ENTER, IntPtr.Zero)
        PostMessage(Handle, WM_CHAR, VK_ENTER, IntPtr.Zero)
        SendMessage(Handle, WM_CHAR, VK_ENTER, IntPtr.Zero)
        PostMessage(Handle, WM_KEYUP, VK_ENTER, IntPtr.Zero)
    End Sub

    Private Sub SendTab(ByVal Handle As IntPtr)
        PostMessage(Handle, WM_KEYDOWN, VK_TAB, IntPtr.Zero)
        PostMessage(Handle, WM_KEYUP, VK_TAB, IntPtr.Zero)
    End Sub
    Private Shared thisPrintingVariable As Boolean = False
    Public Shared Property isPrintingOn As Boolean
        Get
            Return thisPrintingVariable
        End Get
        Set(value As Boolean)
            thisPrintingVariable = value
            If (value) Then
                tTimer.Stop()
                tTimer.Interval = 2000
                tTimer.Start()
            Else
                tTimer.Stop()
                tTimer.Interval = 2000 * 5
                tTimer.Start()
            End If
        End Set
    End Property
    Public Shared Function CloseThisWindow(procID As Integer) As Boolean
        Dim hWnd As IntPtr = IntPtr.Zero
        Dim tid As Integer
        Dim pid As Integer
        ' Loop through all top-level windows
        Do

            ' get the window handle
            hWnd = FindWindowEx(IntPtr.Zero, hWnd, Nothing, Nothing)
            ' get the process id (and thread id)
            If (hWnd <> IntPtr.Zero) Then
                pid = 0
                tid = GetWindowThreadProcessId(hWnd, pid)

                If pid = procID Then

                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
                    Return True
                    Exit Do
                End If
            End If
        Loop While Not hWnd.Equals(IntPtr.Zero)
        Return False
    End Function
    ''' <summary>
    ''' A utility class to determine a process parent.
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ParentProcessUtilities
        ' These members must match PROCESS_BASIC_INFORMATION
        Friend Reserved1 As IntPtr
        Friend PebBaseAddress As IntPtr
        Friend Reserved2_0 As IntPtr
        Friend Reserved2_1 As IntPtr
        Friend UniqueProcessId As IntPtr
        Friend InheritedFromUniqueProcessId As IntPtr
    End Structure
    ''' <summary>
    ''' Gets the parent process of the current process.
    ''' </summary>
    ''' <returns>An instance of the Process class.</returns>
    Public Shared Function GetParentProcess() As Integer
        Return GetParentProcess(Process.GetCurrentProcess().Handle)
    End Function

    ''' <summary>
    ''' Gets the parent process of specified process.
    ''' </summary>
    ''' <param name="id">The process id.</param>
    ''' <returns>An instance of the Process class.</returns>
    Public Shared Function GetParentProcess(ByVal id As Integer) As Integer
        Dim CurrentProcess As Process = Process.GetProcessById(id)
        Return GetParentProcess(CurrentProcess.Handle)
    End Function
    Public Shared Function GetParentProcess(ByVal CurrentProcess As Process) As Integer
        Return GetParentProcess(CurrentProcess.Handle)
    End Function
    ''' <summary>
    ''' Gets the parent process of a specified process.
    ''' </summary>
    ''' <param name="handle">The process handle.</param>
    ''' <returns>An instance of the Process class or null if an error occurred.</returns>
    Public Shared Function GetParentProcess(ByVal handle As IntPtr) As Integer
        Dim pbi As New ParentProcessUtilities()
        Dim returnLength As Integer
        Dim status As Integer = NtQueryInformationProcess(handle, 0, pbi, Marshal.SizeOf(pbi), returnLength)
        If status <> 0 Then
            Return Nothing
        End If

        Try
            Return pbi.InheritedFromUniqueProcessId.ToInt32()
        Catch generatedExceptionName As ArgumentException
            ' not found
            Return Nothing
        End Try
    End Function

    Public Shared Function GetOwnerOfProcess(ByVal CurrentProcess As Process) As String
        Dim ph As IntPtr = IntPtr.Zero
        Dim wi As Security.Principal.WindowsIdentity = Nothing
        Try
            If (OpenProcessToken(CurrentProcess.Handle, TOKEN_QUERY, ph) <> 0) Then
                wi = New Security.Principal.WindowsIdentity(ph)
                Return wi.Name
            Else
                Return "Access Rights Error Code: " + Marshal.GetLastWin32Error()
            End If

        Catch Ex As Exception
            Return "No Rights Exception: " + Ex.ToString()
        Finally
            If ph <> IntPtr.Zero Then
                CloseHandle(ph)
            End If
            If (IsNothing(wi) = False) Then
                wi.Dispose()
                wi = Nothing
            End If
        End Try
    End Function
    ' P/Invoke declarations
    'Private Delegate Function EnumThreadWndProc(hWnd As IntPtr, lp As IntPtr) As Boolean
    '<System.Runtime.InteropServices.DllImport("user32.dll")> _
    'Private Shared Function EnumThreadWindows(tid As Integer, callback As EnumThreadWndProc, lp As IntPtr) As Boolean
    'End Function
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function EnumChildWindows(ByVal WindowHandle As IntPtr, ByVal Callback As EnumWindowProcess, ByVal lParam As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function EnumWindows(ByVal Callback As EnumWindowProcess, ByVal lParam As IntPtr) As Boolean
    End Function

    Private Delegate Function EnumWindowProcess(ByVal Handle As IntPtr, ByVal Parameter As IntPtr) As Boolean


    <System.Runtime.InteropServices.DllImport("kernel32.dll")> _
    Private Shared Function GetCurrentThreadId() As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetClassName(ByVal hWnd As IntPtr, ByVal buffer As StringBuilder, ByVal buflen As Integer) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal className As String, ByVal windowTitle As String) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal sb As StringBuilder) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function GetDlgItem(ByVal hDlg As IntPtr, ByVal nIDDlgItem As Integer) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetWindowText(ByVal hwnd As Integer, ByVal text As StringBuilder, ByVal count As Integer) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetWindowTextLength(ByVal hwnd As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function ShowWindowAsync(ByVal hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> ByVal nCmdShow As ShowWindowCommands) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As ShowWindowCommands) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowLong(hWnd As IntPtr, <MarshalAs(UnmanagedType.I4)> nIndex As WindowLongFlags) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function InvalidateRect(hWnd As IntPtr, rect As IntPtr, clear As Boolean) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function UpdateWindow(hWnd As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetActiveWindow() As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function IsWindowVisible(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("ntdll.dll")> _
    Private Shared Function NtQueryInformationProcess(processHandle As IntPtr, processInformationClass As Integer, ByRef processInformation As ParentProcessUtilities, processInformationLength As Integer, ByRef returnLength As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function AttachThreadInput(ByVal idAttach As System.UInt32, ByVal idAttachTo As System.UInt32, ByVal fAttach As Boolean) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32", CharSet:=CharSet.Auto)> _
    Public Shared Function SystemParametersInfo(ByVal intAction As Integer, ByVal intParam As Integer, ByVal strParam As String, ByVal intWinIniFlag As Integer) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function LockSetForegroundWindow(uLockCode As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function AllowSetForegroundWindow(ByVal dwProcessId As Integer) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function OpenProcessToken(ByVal ProcessHandle As IntPtr, ByVal DesiredAccess As Integer, ByRef TokenHandle As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)> _
    Private Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Private Shared ReadOnly TOKEN_QUERY As UInt32 = &H8
    Private Shared ReadOnly ASFW_ANY As Integer = -1
    Private Shared ReadOnly LSFW_LOCK As UInteger = 1

    Private Shared ReadOnly LSFW_UNLOCK As UInteger = 2

    Private Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)

    Const WS_EX_TOPMOST As UInt32 = &H8
    Const SWP_NOSIZE As UInt32 = &H1
    Const SWP_NOMOVE As UInt32 = &H2
    Const TOPMOST_FLAGS As UInt32 = SWP_NOMOVE Or SWP_NOSIZE

    Private Shared Sub MakeTopMost(hWnd As IntPtr)
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS)
    End Sub
    Private Shared Function IsWindowTopMost(hWnd As IntPtr) As Boolean
        Return (GetWindowLong(hWnd, WindowLongFlags.GWL_EXSTYLE) And WS_EX_TOPMOST) <> 0
    End Function
    'SLR: To optimize the call to find whether lastapplicationactive?
    Private Shared MyCurrentProcessID As Integer = 0
    Private Shared LastForegroundWindow As IntPtr = IntPtr.Zero
    Private Shared LastApllicationActive As Boolean = True
    Public Shared Function IsMyApplicationOnTop() As Boolean
        Dim hWnd As IntPtr = GetForegroundWindow()
        If (IsNothing(hWnd) = False) Then
            If (hWnd <> LastForegroundWindow) Then
                LastForegroundWindow = hWnd
                Dim processId As UInteger
                GetWindowThreadProcessId(hWnd, processId)
                LastApllicationActive = (Process.GetProcessById(processId).Id = MyCurrentProcessID)
            End If
            Return LastApllicationActive
        End If
        Return False 'SLR: No rights to get foreground window like lockscreen.
    End Function
    Public Shared Function GetMyActivatedHandle() As IntPtr
        Return GetForegroundWindow()
    End Function
    Public Enum WindowLongFlags As Integer
        GWL_EXSTYLE = -20
        GWLP_HINSTANCE = -6
        GWLP_HWNDPARENT = -8
        GWL_ID = -12
        GWL_STYLE = -16
        GWL_USERDATA = -21
        GWL_WNDPROC = -4
        DWLP_USER = &H8
        DWLP_MSGRESULT = &H0
        DWLP_DLGPROC = &H4
    End Enum
    Enum ShowWindowCommands As Integer
        ''' <summary>
        ''' Hides the window and activates another window.
        ''' </summary>
        Hide = 0
        ''' <summary>
        ''' Activates and displays a window. If the window is minimized or 
        ''' maximized, the system restores it to its original size and position.
        ''' An application should specify this flag when displaying the window 
        ''' for the first time.
        ''' </summary>
        Normal = 1
        ''' <summary>
        ''' Activates the window and displays it as a minimized window.
        ''' </summary>
        ShowMinimized = 2
        ''' <summary>
        ''' Maximizes the specified window.
        ''' </summary>
        Maximize = 3
        ' is this the right value?
        ''' <summary>
        ''' Activates the window and displays it as a maximized window.
        ''' </summary>       
        ShowMaximized = 3
        ''' <summary>
        ''' Displays a window in its most recent size and position. This value 
        ''' is similar to see cref="Win32.ShowWindowCommands.Normal", except 
        ''' the window is not actived.
        ''' </summary>
        ShowNoActivate = 4
        ''' <summary>
        ''' Activates the window and displays it in its current size and position. 
        ''' </summary>
        Show = 5
        ''' <summary>
        ''' Minimizes the specified window and activates the next top-level 
        ''' window in the Z order.
        ''' </summary>
        Minimize = 6
        ''' <summary>
        ''' Displays the window as a minimized window. This value is similar to
        ''' see cref="Win32.ShowWindowCommands.ShowMinimized", except the 
        ''' window is not activated.
        ''' </summary>
        ShowMinNoActive = 7
        ''' <summary>
        ''' Displays the window in its current size and position. This value is 
        ''' similar to see cref="Win32.ShowWindowCommands.Show", except the 
        ''' window is not activated.
        ''' </summary>
        ShowNA = 8
        ''' <summary>
        ''' Activates and displays the window. If the window is minimized or 
        ''' maximized, the system restores it to its original size and position. 
        ''' An application should specify this flag when restoring a minimized window.
        ''' </summary>
        Restore = 9
        ''' <summary>
        ''' Sets the show state based on the SW_* value specified in the 
        ''' STARTUPINFO structure passed to the CreateProcess function by the 
        ''' program that started the application.
        ''' </summary>
        ShowDefault = 10
        ''' <summary>
        '''  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
        ''' that owns the window is not responding. This flag should only be 
        ''' used when minimizing windows from a different thread.
        ''' </summary>
        ForceMinimize = 11
    End Enum
    Enum SPI
        SPI_GETBEEP = &H1
        SPI_SETBEEP = &H2
        SPI_GETMOUSE = &H3
        SPI_SETMOUSE = &H4
        SPI_GETBORDER = &H5
        SPI_SETBORDER = &H6
        SPI_GETKEYBOARDSPEED = &HA
        SPI_SETKEYBOARDSPEED = &HB
        SPI_LANGDRIVER = &HC
        SPI_ICONHORIZONTALSPACING = &HD
        SPI_GETSCREENSAVETIMEOUT = &HE
        SPI_SETSCREENSAVETIMEOUT = &HF
        SPI_GETSCREENSAVEACTIVE = &H10
        SPI_SETSCREENSAVEACTIVE = &H11
        SPI_GETGRIDGRANULARITY = &H12
        SPI_SETGRIDGRANULARITY = &H13
        SPI_SETDESKWALLPAPER = &H14
        SPI_SETDESKPATTERN = &H15
        SPI_GETKEYBOARDDELAY = &H16
        SPI_SETKEYBOARDDELAY = &H17
        SPI_ICONVERTICALSPACING = &H18
        SPI_GETICONTITLEWRAP = &H19
        SPI_SETICONTITLEWRAP = &H1A
        SPI_GETMENUDROPALIGNMENT = &H1B
        SPI_SETMENUDROPALIGNMENT = &H1C
        SPI_SETDOUBLECLKWIDTH = &H1D
        SPI_SETDOUBLECLKHEIGHT = &H1E
        SPI_GETICONTITLELOGFONT = &H1F
        SPI_SETDOUBLECLICKTIME = &H20
        SPI_SETMOUSEBUTTONSWAP = &H21
        SPI_SETICONTITLELOGFONT = &H22
        SPI_GETFASTTASKSWITCH = &H23
        SPI_SETFASTTASKSWITCH = &H24
        SPI_SETDRAGFULLWINDOWS = &H25
        SPI_GETDRAGFULLWINDOWS = &H26
        SPI_GETNONCLIENTMETRICS = &H29
        SPI_SETNONCLIENTMETRICS = &H2A
        SPI_GETMINIMIZEDMETRICS = &H2B
        SPI_SETMINIMIZEDMETRICS = &H2C
        SPI_GETICONMETRICS = &H2D
        SPI_SETICONMETRICS = &H2E
        SPI_SETWORKAREA = &H2F
        SPI_GETWORKAREA = &H30
        SPI_SETPENWINDOWS = &H31
        SPI_GETHIGHCONTRAST = &H42
        SPI_SETHIGHCONTRAST = &H43
        SPI_GETKEYBOARDPREF = &H44
        SPI_SETKEYBOARDPREF = &H45
        SPI_GETSCREENREADER = &H46
        SPI_SETSCREENREADER = &H47
        SPI_GETANIMATION = &H48
        SPI_SETANIMATION = &H49
        SPI_GETFONTSMOOTHING = &H4A
        SPI_SETFONTSMOOTHING = &H4B
        SPI_SETDRAGWIDTH = &H4C
        SPI_SETDRAGHEIGHT = &H4D
        SPI_SETHANDHELD = &H4E
        SPI_GETLOWPOWERTIMEOUT = &H4F
        SPI_GETPOWEROFFTIMEOUT = &H50
        SPI_SETLOWPOWERTIMEOUT = &H51
        SPI_SETPOWEROFFTIMEOUT = &H52
        SPI_GETLOWPOWERACTIVE = &H53
        SPI_GETPOWEROFFACTIVE = &H54
        SPI_SETLOWPOWERACTIVE = &H55
        SPI_SETPOWEROFFACTIVE = &H56
        SPI_SETICONS = &H58
        SPI_GETDEFAULTINPUTLANG = &H59
        SPI_SETDEFAULTINPUTLANG = &H5A
        SPI_SETLANGTOGGLE = &H5B
        SPI_GETWINDOWSEXTENSION = &H5C
        SPI_SETMOUSETRAILS = &H5D
        SPI_GETMOUSETRAILS = &H5E
        SPI_SCREENSAVERRUNNING = &H61
        SPI_GETFILTERKEYS = &H32
        SPI_SETFILTERKEYS = &H33
        SPI_GETTOGGLEKEYS = &H34
        SPI_SETTOGGLEKEYS = &H35
        SPI_GETMOUSEKEYS = &H36
        SPI_SETMOUSEKEYS = &H37
        SPI_GETSHOWSOUNDS = &H38
        SPI_SETSHOWSOUNDS = &H39
        SPI_GETSTICKYKEYS = &H3A
        SPI_SETSTICKYKEYS = &H3B
        SPI_GETACCESSTIMEOUT = &H3C
        SPI_SETACCESSTIMEOUT = &H3D
        SPI_GETSERIALKEYS = &H3E
        SPI_SETSERIALKEYS = &H3F
        SPI_GETSOUNDSENTRY = &H40
        SPI_SETSOUNDSENTRY = &H41
        SPI_GETSNAPTODEFBUTTON = &H5F
        SPI_SETSNAPTODEFBUTTON = &H60
        SPI_GETMOUSEHOVERWIDTH = &H62
        SPI_SETMOUSEHOVERWIDTH = &H63
        SPI_GETMOUSEHOVERHEIGHT = &H64
        SPI_SETMOUSEHOVERHEIGHT = &H65
        SPI_GETMOUSEHOVERTIME = &H66
        SPI_SETMOUSEHOVERTIME = &H67
        SPI_GETWHEELSCROLLLINES = &H68
        SPI_SETWHEELSCROLLLINES = &H69
        SPI_GETMENUSHOWDELAY = &H6A
        SPI_SETMENUSHOWDELAY = &H6B
        SPI_GETSHOWIMEUI = &H6E
        SPI_SETSHOWIMEUI = &H6F
        SPI_GETMOUSESPEED = &H70
        SPI_SETMOUSESPEED = &H71
        SPI_GETSCREENSAVERRUNNING = &H72
        SPI_GETDESKWALLPAPER = &H73
        SPI_GETACTIVEWINDOWTRACKING = &H1000
        SPI_SETACTIVEWINDOWTRACKING = &H1001
        SPI_GETMENUANIMATION = &H1002
        SPI_SETMENUANIMATION = &H1003
        SPI_GETCOMBOBOXANIMATION = &H1004
        SPI_SETCOMBOBOXANIMATION = &H1005
        SPI_GETLISTBOXSMOOTHSCROLLING = &H1006
        SPI_SETLISTBOXSMOOTHSCROLLING = &H1007
        SPI_GETGRADIENTCAPTIONS = &H1008
        SPI_SETGRADIENTCAPTIONS = &H1009
        SPI_GETKEYBOARDCUES = &H100A
        SPI_SETKEYBOARDCUES = &H100B
        SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES
        SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES
        SPI_GETACTIVEWNDTRKZORDER = &H100C
        SPI_SETACTIVEWNDTRKZORDER = &H100D
        SPI_GETHOTTRACKING = &H100E
        SPI_SETHOTTRACKING = &H100F
        SPI_GETMENUFADE = &H1012
        SPI_SETMENUFADE = &H1013
        SPI_GETSELECTIONFADE = &H1014
        SPI_SETSELECTIONFADE = &H1015
        SPI_GETTOOLTIPANIMATION = &H1016
        SPI_SETTOOLTIPANIMATION = &H1017
        SPI_GETTOOLTIPFADE = &H1018
        SPI_SETTOOLTIPFADE = &H1019
        SPI_GETCURSORSHADOW = &H101A
        SPI_SETCURSORSHADOW = &H101B
        SPI_GETMOUSESONAR = &H101C
        SPI_SETMOUSESONAR = &H101D
        SPI_GETMOUSECLICKLOCK = &H101E
        SPI_SETMOUSECLICKLOCK = &H101F
        SPI_GETMOUSEVANISH = &H1020
        SPI_SETMOUSEVANISH = &H1021
        SPI_GETFLATMENU = &H1022
        SPI_SETFLATMENU = &H1023
        SPI_GETDROPSHADOW = &H1024
        SPI_SETDROPSHADOW = &H1025
        SPI_GETBLOCKSENDINPUTRESETS = &H1026
        SPI_SETBLOCKSENDINPUTRESETS = &H1027
        SPI_GETUIEFFECTS = &H103E
        SPI_SETUIEFFECTS = &H103F
        SPI_GETFOREGROUNDLOCKTIMEOUT = &H2000
        SPI_SETFOREGROUNDLOCKTIMEOUT = &H2001
        SPI_GETACTIVEWNDTRKTIMEOUT = &H2002
        SPI_SETACTIVEWNDTRKTIMEOUT = &H2003
        SPI_GETFOREGROUNDFLASHCOUNT = &H2004
        SPI_SETFOREGROUNDFLASHCOUNT = &H2005
        SPI_GETCARETWIDTH = &H2006
        SPI_SETCARETWIDTH = &H2007
        SPI_GETMOUSECLICKLOCKTIME = &H2008
        SPI_SETMOUSECLICKLOCKTIME = &H2009
        SPI_GETFONTSMOOTHINGTYPE = &H200A
        SPI_SETFONTSMOOTHINGTYPE = &H200B
        SPI_GETFONTSMOOTHINGCONTRAST = &H200C
        SPI_SETFONTSMOOTHINGCONTRAST = &H200D
        SPI_GETFOCUSBORDERWIDTH = &H200E
        SPI_SETFOCUSBORDERWIDTH = &H200F
        SPI_GETFOCUSBORDERHEIGHT = &H2010
        SPI_SETFOCUSBORDERHEIGHT = &H2011
        SPI_GETFONTSMOOTHINGORIENTATION = &H2012
        SPI_SETFONTSMOOTHINGORIENTATION = &H2013
    End Enum
    Public Class ApiWindow
        Private m_Title As String = Nothing
        Private m_Class As String = Nothing
        Private m_hWnd As IntPtr = IntPtr.Zero
        Private m_Content As String = Nothing
        Private WM_SETTEXT As Integer = 12
        Private WM_GETTEXT As Integer = 13

        Public ReadOnly Property MainWindowTitle As String
            Get
                If (IsNothing(m_Title)) Then
                    If (m_hWnd <> IntPtr.Zero) Then

                        Try
                            'Dim int32Handle = m_hWnd.ToInt32()
                            Dim mylen As Integer = GetWindowTextLength(m_hWnd) + 1
                            Dim sbText As New System.Text.StringBuilder(mylen + 1)

                            If (mylen > 1) Then
                                GetWindowText(m_hWnd, sbText, mylen)
                                m_Title = sbText.ToString().Replace("&", "")
                            End If

                        Catch ex As Exception

                        End Try
                    End If
                End If
                Return m_Title
            End Get
        End Property
        Public Property MainWindowContent As String
            Get
                If (IsNothing(m_Content)) Then
                    If (m_hWnd <> IntPtr.Zero) Then
                        Try
                            Dim sbText As New System.Text.StringBuilder(500)
                            SendMessage(m_hWnd, WM_GETTEXT, sbText.Capacity, sbText)
                            m_Content = sbText.ToString().Replace("&", "")
                        Catch ex As Exception

                        End Try
                    End If
                End If
                Return m_Content
            End Get
            Set(value As String)

            End Set
        End Property
        Public ReadOnly Property ClassName As String
            Get
                If (m_hWnd <> IntPtr.Zero) Then

                    Try

                        Dim sbclassText As New System.Text.StringBuilder(257)
                        GetClassName(m_hWnd, sbclassText, 256)
                        m_Class = sbclassText.ToString()

                    Catch ex As Exception

                    End Try
                End If
                Return m_Class
            End Get
        End Property
        Public Sub New(hWnd As Integer)
            m_hWnd = hWnd
        End Sub


    End Class
    Public Class FindParentProcess

        '/ <summary>
        '/ A utility class to determine a process parent.
        '/ </summary>
        <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Private Structure PROCESS_BASIC_INFORMATION
            Public ExitStatus As IntPtr
            Public PebBaseAddress As IntPtr
            Public AffinityMask As IntPtr
            Public BasePriority As IntPtr
            Public UniqueProcessId As IntPtr
            Public InheritedFromUniqueProcessId As IntPtr

            Public ReadOnly Property Size() As IntPtr
                Get
                    Return 6 * IntPtr.Size
                End Get
            End Property
        End Structure
        <DllImport("ntdll.dll", SetLastError:=True)> _
        Private Shared Function NtQueryInformationProcess(processHandle As IntPtr, processInformationClass As Integer, ByRef processInformation As PROCESS_BASIC_INFORMATION, processInformationLength As UInteger, ByRef returnLength As Integer) As Integer
        End Function
        <DllImport("kernel32.dll")> _
        Private Shared Function OpenProcess(processAccess As ProcessAccessFlags, bInheritHandle As Boolean, processId As Integer) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Private Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll")> _
        Private Shared Function OpenProcess(processAccess As Integer, bInheritHandle As Boolean, processId As Integer) As IntPtr
        End Function
        <Flags()> _
        Private Enum ProcessAccessFlags As UInteger
            All = &H1F0FFF
            Terminate = &H1
            CreateThread = &H2
            VirtualMemoryOperation = &H8
            VirtualMemoryRead = &H10
            VirtualMemoryWrite = &H20
            DuplicateHandle = &H40
            CreateProcess = &H80
            SetQuota = &H100
            SetInformation = &H200
            QueryInformation = &H400
            QueryLimitedInformation = &H1000
            Synchronize = &H100000
        End Enum
        Private Enum PROCESSINFOCLASS As Integer
            ProcessBasicInformation = 0
            ' 0, q: PROCESS_BASIC_INFORMATION, PROCESS_EXTENDED_BASIC_INFORMATION
            ProcessQuotaLimits
            ' qs: QUOTA_LIMITS, QUOTA_LIMITS_EX
            ProcessIoCounters
            ' q: IO_COUNTERS
            ProcessVmCounters
            ' q: VM_COUNTERS, VM_COUNTERS_EX
            ProcessTimes
            ' q: KERNEL_USER_TIMES
            ProcessBasePriority
            ' s: KPRIORITY
            ProcessRaisePriority
            ' s: ULONG
            ProcessDebugPort
            ' q: HANDLE
            ProcessExceptionPort
            ' s: HANDLE
            ProcessAccessToken
            ' s: PROCESS_ACCESS_TOKEN
            ProcessLdtInformation
            ' 10
            ProcessLdtSize
            ProcessDefaultHardErrorMode
            ' qs: ULONG
            ProcessIoPortHandlers
            ' (kernel-mode only)
            ProcessPooledUsageAndLimits
            ' q: POOLED_USAGE_AND_LIMITS
            ProcessWorkingSetWatch
            ' q: PROCESS_WS_WATCH_INFORMATION[]; s: void
            ProcessUserModeIOPL
            ProcessEnableAlignmentFaultFixup
            ' s: BOOLEAN
            ProcessPriorityClass
            ' qs: PROCESS_PRIORITY_CLASS
            ProcessWx86Information
            ProcessHandleCount
            ' 20, q: ULONG, PROCESS_HANDLE_INFORMATION
            ProcessAffinityMask
            ' s: KAFFINITY
            ProcessPriorityBoost
            ' qs: ULONG
            ProcessDeviceMap
            ' qs: PROCESS_DEVICEMAP_INFORMATION, PROCESS_DEVICEMAP_INFORMATION_EX
            ProcessSessionInformation
            ' q: PROCESS_SESSION_INFORMATION
            ProcessForegroundInformation
            ' s: PROCESS_FOREGROUND_BACKGROUND
            ProcessWow64Information
            ' q: ULONG_PTR
            ProcessImageFileName
            ' q: UNICODE_STRING
            ProcessLUIDDeviceMapsEnabled
            ' q: ULONG
            ProcessBreakOnTermination
            ' qs: ULONG
            ProcessDebugObjectHandle
            ' 30, q: HANDLE
            ProcessDebugFlags
            ' qs: ULONG
            ProcessHandleTracing
            ' q: PROCESS_HANDLE_TRACING_QUERY; s: size 0 disables, otherwise enables
            ProcessIoPriority
            ' qs: ULONG
            ProcessExecuteFlags
            ' qs: ULONG
            ProcessResourceManagement
            ProcessCookie
            ' q: ULONG
            ProcessImageInformation
            ' q: SECTION_IMAGE_INFORMATION
            ProcessCycleTime
            ' q: PROCESS_CYCLE_TIME_INFORMATION
            ProcessPagePriority
            ' q: ULONG
            ProcessInstrumentationCallback
            ' 40
            ProcessThreadStackAllocation
            ' s: PROCESS_STACK_ALLOCATION_INFORMATION, PROCESS_STACK_ALLOCATION_INFORMATION_EX
            ProcessWorkingSetWatchEx
            ' q: PROCESS_WS_WATCH_INFORMATION_EX[]
            ProcessImageFileNameWin32
            ' q: UNICODE_STRING
            ProcessImageFileMapping
            ' q: HANDLE (input)
            ProcessAffinityUpdateMode
            ' qs: PROCESS_AFFINITY_UPDATE_MODE
            ProcessMemoryAllocationMode
            ' qs: PROCESS_MEMORY_ALLOCATION_MODE
            ProcessGroupInformation
            ' q: USHORT[]
            ProcessTokenVirtualizationEnabled
            ' s: ULONG
            ProcessConsoleHostProcess
            ' q: ULONG_PTR
            ProcessWindowInformation
            ' 50, q: PROCESS_WINDOW_INFORMATION
            ProcessHandleInformation
            ' q: PROCESS_HANDLE_SNAPSHOT_INFORMATION // since WIN8
            ProcessMitigationPolicy
            ' s: PROCESS_MITIGATION_POLICY_INFORMATION
            ProcessDynamicFunctionTableInformation
            ProcessHandleCheckingMode
            ProcessKeepAliveCount
            ' q: PROCESS_KEEPALIVE_COUNT_INFORMATION
            ProcessRevokeFileHandles
            ' s: PROCESS_REVOKE_FILE_HANDLES_INFORMATION
            MaxProcessInfoClass
        End Enum

        Public Shared Function GetParentIDUsingNTQuery(ByVal myId As Integer) As Integer
            Dim pbi As New PROCESS_BASIC_INFORMATION()

            'Get a handle to our own process
            Dim hProc As IntPtr = OpenProcess(ProcessAccessFlags.QueryInformation, False, myId)
            Dim queryStatus As Integer = 0
            Try
                Dim sizeInfoReturned As Integer
                queryStatus = NtQueryInformationProcess(hProc, PROCESSINFOCLASS.ProcessBasicInformation, pbi, pbi.Size, sizeInfoReturned)
            Finally
                If Not hProc.Equals(IntPtr.Zero) Then
                    'Close handle and free allocated memory
                    CloseHandle(hProc)
                    hProc = IntPtr.Zero
                End If
            End Try
            If (queryStatus = 0) Then
                Return pbi.InheritedFromUniqueProcessId.ToInt32()
            Else
                Return 0
            End If

        End Function
        Public Shared Function GetParentIDUsingWMI(ByVal myId As Integer) As Integer
            Dim query As String = String.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", myId)
            Try
                Using search As ManagementObjectSearcher = New ManagementObjectSearcher("root\\CIMV2", query)
                    Using results As ManagementObjectCollection.ManagementObjectEnumerator = search.Get().GetEnumerator()
                        results.MoveNext()
                        Dim queryObj As ManagementBaseObject = results.Current
                        Dim parentId As Integer = CType(queryObj("ParentProcessId"), System.UInt32)
                        Return parentId
                    End Using
                End Using
            Catch ex As Exception
                Return Nothing
            End Try
            query = Nothing
        End Function

    End Class


End Class
