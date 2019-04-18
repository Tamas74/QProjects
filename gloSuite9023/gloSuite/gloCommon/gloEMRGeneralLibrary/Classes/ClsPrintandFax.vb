Imports gloEMRGeneralLibrary.gloGeneral
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
'Imports Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word


Namespace gloprintfax
    Public Class ClsPrintandFax
        Implements IPrintandFax
        Dim _sDefaultPrinter As String      ' System Default Printer
        Dim _sFileName As String
#Region "Public Properties"
        Public Property DefaultPrinter() As String Implements IPrintandFax.DefaultPrinter
            Get
                Return _sDefaultPrinter
            End Get
            Set(ByVal value As String)
                _sDefaultPrinter = value
            End Set
        End Property
        Public Property FileName() As String Implements IPrintandFax.FileName
            Get
                Return _sFileName
            End Get
            Set(ByVal value As String)
                _sFileName = value
            End Set
        End Property
#End Region

    End Class
    Interface IPrintandFax
        Property DefaultPrinter() As String
        Property FileName() As String
    End Interface
    Public Class FaxBusinessLayer
        Inherits ClsPrintandFax
        Implements IDisposable

        '  Private objword As Wd.Application
        Dim _sFaxPrinter As String  ' FAX Printer
        Private _PendingFax As PendingFax
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Enum enmDateCriteria
            Today
            Yesterday
            LastWeek
            LastMonth
            Customize
        End Enum

        Public Property PendingFaxObject() As PendingFax
            Get
                If Not IsNothing(_PendingFax) Then
                    _PendingFax = New PendingFax
                Else
                    Return _PendingFax
                End If
                Return _PendingFax
            End Get
            Set(ByVal value As PendingFax)
                _PendingFax = value
            End Set
        End Property
        Public Sub New()
            MyBase.new()
        End Sub
        Public Sub New(ByVal strFAXPrinter As String)
            MyBase.new()
            _sFaxPrinter = strFAXPrinter
            'Retrieve the Default Printer
            If Not gloGlobal.gloTSPrint.isCopyPrint Then
                Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
                DefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
                objPrintDocument.Dispose()
                objPrintDocument = Nothing
            End If
        End Sub
#Region "Private variables"
        Enum enmFAXType
            PatientExam
            PatientLetters
            PatientMessages
            PatientOrders
            FormGallery
            ReferralLetter
            Prescription
            PTProtocols
        End Enum
#End Region
#Region "Property Procedures"
        Public ReadOnly Property FAXPrinter() As String
            Get
                Return _sFaxPrinter
            End Get
        End Property
#End Region

#Region "Private Functions"



        'To FAX Document without FAX Parameters - It will take the FAX Parameters of Private variables
        'Public Function FAXDocument(ByVal objDoc As Word.Document, ByVal enmFAXDocumentType As enmFAXType) As Boolean
        '    Return FAXDocument(objDoc, enmFAXDocumentType)
        'End Function

        'This function is to be implemented in the shared class of Patient Exam
        'Private Sub ReplaceFieldsTemp()
        '    Dim objField As FormField
        '    For Each objField In objWord.ActiveDocument.FormFields
        '        If objField.Type = WdFieldType.wdFieldFormTextInput Then
        '            If objField.StatusText = objField.Result Then
        '                objField.Result = ""
        '            End If
        '        End If
        '    Next
        'End Sub
        'This function is to be implemented in the shared class of Patient Exam
        'Private Sub ReplaceNavigationIconsTemp()
        '    objWord.ActiveDocument.Application.Selection.Find.ClearFormatting()
        '    objWord.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        '    With objWord.ActiveDocument.Application.Selection.Find
        '        .Text = "[]"
        '        .Replacement.Text = " "
        '        .Forward = True
        '        .Wrap = Word.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = False
        '        .MatchWildcards = False
        '        .MatchSoundsLike = False
        '        .MatchAllWordForms = False
        '    End With
        '    objWord.ActiveDocument.Application.Selection.Find.Execute(Replace:=Word.WdReplace.wdReplaceAll)
        'End Sub
#End Region
#Region "Public Functions"
        Friend Function AddPendingFAX(ByVal objPendingFax As PendingFax, Optional ByVal CurrentFAXPriority As globalFax.enmFAXPriority = globalFax.enmFAXPriority.NormalPriority) As Boolean
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try
                _FaxDBLayer = New FaxDBLayer
                _FaxDBLayer.AddPendingFAX(objPendingFax)
            Catch ex As FaxException

                Dim objex As New FaxException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If

            End Try
            Return False
        End Function

        Public Function Fill_PendingFAXes(ByVal enmFAXDateCriteria As enmDateCriteria, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal nPatientID As Long = 0) As PendingFaxes
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try

                _FaxDBLayer = New FaxDBLayer
                Dim _PendingFaxes As PendingFaxes
                _PendingFaxes = _FaxDBLayer.Fill_PendingFAXes(enmFAXDateCriteria, dtFromDate, dtToDate, nPatientID)

                If Not IsNothing(_PendingFaxes) Then
                    Return _PendingFaxes
                Else
                    Return Nothing
                End If
            Catch ex As FaxException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function Fill_PendingFAXes(Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As PendingFaxes
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try
                _FaxDBLayer = New FaxDBLayer
                Dim _PendingFaxes As PendingFaxes
                _PendingFaxes = _FaxDBLayer.Fill_PendingFAXes(nPatientID, nMaxNoAttempts)

                If Not IsNothing(_PendingFaxes) Then
                    Return _PendingFaxes
                Else
                    Return Nothing
                End If
            Catch ex As FaxException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = ""
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function ReInitialisePendingFAX(ByVal nFAXID As Integer, Optional ByVal strFAXNo As String = "") As Boolean
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try

                _FaxDBLayer = New FaxDBLayer
                Return _FaxDBLayer.ReInitialisePendingFAX(nFAXID, strFAXNo)
            Catch ex As FaxException
                Return False
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Reinitializing Fax"
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function RetrieveFAXDetails(ByVal strFileName As String) As PendingFax
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try

                _FaxDBLayer = New FaxDBLayer
                Dim _PendingFax As PendingFax
                _PendingFax = _FaxDBLayer.RetrieveFAXDetails(strFileName)
                Return _PendingFax
            Catch ex As FaxException
                Return _PendingFax
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Retrieving Pending Fax"
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function GetPharmacyFAXNo(ByVal nPatientID As Long) As ContactInformation
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try
                _FaxDBLayer = New FaxDBLayer
                Dim _ContactInformation As ContactInformation
                _ContactInformation = _FaxDBLayer.GetPharmacyFAXNo(nPatientID)
                Return _ContactInformation
            Catch ex As FaxException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Retrieving Pending Fax"
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function RetrieveFAXAttemptsDetails(ByVal nFAXID As Integer) As PendingFaxDetails
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try
                _FaxDBLayer = New FaxDBLayer
                Return _FaxDBLayer.RetrieveFAXAttemptsDetails(nFAXID)
            Catch ex As FaxException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Retrieving Pending Fax Details"
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function
        Public Function GetContactFAXNo(ByVal nContactID As Long) As String
            Dim _FaxDBLayer As FaxDBLayer = Nothing
            Try
                _FaxDBLayer = New FaxDBLayer
                Return _FaxDBLayer.GetContactFAXNo(nContactID)
            Catch ex As FaxException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Retrieving Pending Fax Details"
                Throw objex
            Finally
                If (IsNothing(_FaxDBLayer) = False) Then
                    _FaxDBLayer.Dispose()
                    _FaxDBLayer = Nothing
                End If
            End Try
        End Function



        'Public Function FAXDocument(ByRef objDoc As Wd.Document, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True) As Boolean
        '    globalFax.UpdateLog("In FAX Document method")
        '    'Clear the error message
        '    '_sErrorMessage = ""

        '    Dim blnTIFFFileGenerated As Boolean = False     'Temporary variable to check the TIFF File has been generated or not
        '    Dim strTIFFFileName As String = ""              ' TIFF File Name

        '    'Create the Object of FAX Class
        '    globalFax.UpdateLog("Creating the object of clsFAX class")
        '    Dim objFAX As New FaxDBLayer
        '    Try
        '        globalFax.UpdateLog("Object created")
        '        'Retrieve the FAX Document Name - i.e. FAX Document Name will be the combination of System Date and Time
        '        strTIFFFileName = FaxSettings.RetrieveFAXDocumentName()
        '        globalFax.UpdateLog("TIFF File Name generated")

        '        'Set the FAX Printer Settings
        '        globalFax.UpdateLog("SET Printer settings")
        '        If FaxSettings.SetFAXPrinterDocumentSettings(strTIFFFileName) = False Then
        '            If (IsNothing(objFAX) = False) Then
        '                objFAX.Dispose()
        '                objFAX = Nothing
        '            End If


        '            Dim objex As New FaxException
        '            objex.ErrMessage = "Unable to set the FAX Printer Settings"
        '            Throw objex
        '            Return False
        '        End If
        '        globalFax.UpdateLog("Printer settings set")
        '        '  objword = objDoc.Application

        '        'If the FAX Document type is other than Prescription then replace the Empty Fields & Navigation Icons

        '        If enmFAXDocumentType <> enmFAXType.Prescription AndAlso IsCleanUpRequired Then
        '            globalFax.UpdateLog("Start Clean up")
        '            Dim objField As Wd.FormField
        '            For Each objField In objDoc.FormFields
        '                If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
        '                    If objField.StatusText = objField.Result Then
        '                        objField.Result = ""
        '                    End If
        '                End If
        '            Next
        '            'Try
        '            '    objDoc.Application.Selection.Find.Execute(FindText:="[]", ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)

        '            'Catch ex As Exception
        '            Try

        '                FindAndReplace(MyApp:=objDoc.Application, FindText:="[]", ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
        '            Catch ex2 As Exception

        '            End Try
        '            'End Try

        '            'If blnHPIEnabled Then
        '            '    CleanupHPI()
        '            'End If
        '            'UpdateLog("Clean Done")
        '        End If

        '        If globalFax.gblnFAXCoverPage = True AndAlso File.Exists(globalFax.gstrFaxCoverPage) Then
        '            globalFax.UpdateLog("Insert the Cover Page")
        '            'Insert the Cover Page
        '            objDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        '            objDoc.Application.Selection.InsertFile(globalFax.gstrFaxCoverPage)
        '            objDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
        '            globalFax.UpdateLog("Cover Page Inserted")

        '            'InsertCoverPage(objDoc, enmFAXDocumentType)
        '        End If


        '        If IsFAXPrinterHasToSet Then
        '            globalFax.UpdateLog("Setting Printer")
        '            'Set the FAX Printer as the default Printer
        '            objDoc.Application.WordBasic.FilePrintSetup(Printer:=_sFaxPrinter, DoNotSetAsSysDefault:=1)
        '            globalFax.UpdateLog("Printer set")
        '        End If


        '        'Add the FAX Details in the Database
        '        _PendingFax.FaxType = globalFax.gstrFAXType
        '        _PendingFax.FileName = strTIFFFileName

        '        objFAX.AddPendingFAX(_PendingFax, globalFax.CurrentSendingFAXPriority)

        '        'Print the DOC File i.e. to Generate the TIFF File from DOC File
        '        globalFax.UpdateLog("Starts Printing.....")
        '        objDoc.Application.Options.PrintBackground = False
        '        objDoc.PrintOut(Background:=False)

        '        ' objword.PrintOut()
        '        globalFax.UpdateLog("Printing Done")
        '        'No error occurs while Printing means TIFF File has been successfully generated
        '        blnTIFFFileGenerated = True
        '        If IsDSODefaultPrinterHasToSet = True Then
        '            objDoc.Application.WordBasic.FilePrintSetup(Printer:=DefaultPrinter, DoNotSetAsSysDefault:=1)
        '        End If
        '        ' objword = Nothing
        '    Catch ex As Exception
        '        'Error occured. So TIFF File has not been generated
        '        blnTIFFFileGenerated = False
        '        'Assign the error message
        '        Dim objex As New FaxException
        '        objex.ErrMessage = "Error Sending Fax"
        '        Throw objex
        '    Finally
        '        'Check the TIFF FIle has been successfully generated
        '        'If TIFF File has not been generated then delete the DB entry
        '        If blnTIFFFileGenerated = False Then
        '            'Delete DB Entry
        '            If Trim(strTIFFFileName) <> "" Then
        '                objFAX.DeletePendingFAX(strTIFFFileName)
        '            End If
        '        Else
        '            'Add the Entry in Audit Trail
        '            'Dim objAudit As New clsAudit

        '            'Function to be implemented 
        '            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "FAX has been send - FAX To=" & sFAXTo & ", FAX No=" & sFAXNo & ", FAX Type=" & gstrFAXType, sLoginUser, gstrClientMachineName, nPatientID)
        '            'Function to be implemented 

        '            'objAudit = Nothing
        '        End If
        '        If (IsNothing(objFAX) = False) Then
        '            objFAX.Dispose()
        '            objFAX = Nothing
        '        End If
        '        '  objDoc = Nothing
        '    End Try
        '    Return blnTIFFFileGenerated
        'End Function
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
#End Region
        

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
#Region "FaxSettings"
    Friend Class FaxSettings
        Implements IDisposable
        Private disposedValue As Boolean = False
#Region "Private functions"
        ' To detect redundant calls
        Friend Shared Function RetrieveFAXDocumentName() As String
            'Set FAX Settings
            Dim strTIFFFileName As String = ""
            ' Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            strTIFFFileName = globalFax.gnClientMachineID & "-" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") 'Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
            Return strTIFFFileName
        End Function
        Friend Shared Function SetFAXPrinterDefaultSettings() As Boolean
            Try
                'Set TIFF Printer settings by using EXE
                If File.Exists(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe") = True Then
                    'Enable the controlling of the output folder/file name via StartDoc Win32 API ---- -0-Disable --- 1-Enable
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " general.startdocfilename=1")
                    'Set FAX Printer Output Directory
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.folder=" & globalFax.gstrFAXOutputDirectory)

                    'Hide Progress Dialog Box
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " general.hidestsdialog=1")

                    'Check File exists or not
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.filexistact=2")

                    'POP Dialog before printing starts
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.popupdialog=2")

                    'Image File Format
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.fileformat=1")

                    'Image Color
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.imagecolor=0")

                    'Image Compression
                    Select Case globalFax.gstrFAXCompression
                        Case "CCITT G3"
                            Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.compression=1")
                        Case "CCITT G4"
                            Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.compression=2")
                        Case "Packbits"
                            Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.compression=3")
                    End Select


                    'Image Compression
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.pagetype=0")

                    'DPI Settings
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " printer.9.dmPrintQuality=DPI resolution:200x200")
                Else
                    Return False
                    Exit Function
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Friend Shared Function SetFAXPrinterDocumentSettings(ByVal strFAXDocumentName As String) As Boolean
            Try
                'Set TIFF Printer settings by using EXE
                If File.Exists(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe") = True Then
                    'Set FAX Printer Output File Name
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.basefilename=" & strFAXDocumentName)
                Else
                    Return False
                    Exit Function
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Friend Shared Function SetFAXPrinterSettings(ByVal strFAXDocumentName As String) As Boolean
            Try
                'Set TIFF Printer settings by using EXE
                If File.Exists(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe") = True Then
                    'Enable the controlling of the output folder/file name via StartDoc Win32 API ---- -0-Disable --- 1-Enable
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " general.startdocfilename=1")
                    'Set FAX Printer Output Directory
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.folder=" & globalFax.gstrFAXOutputDirectory)
                    'Set FAX Printer Output File Name
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.basefilename=" & strFAXDocumentName)

                    'Hide Progress Dialog Box
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " general.hidestsdialog=1")

                    'Check File exists or not
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.filexistact=2")

                    'POP Dialog before printing starts
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " save.popupdialog=2")

                    'Image File Format
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.fileformat=1")

                    'Image Color
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.imagecolor=3")

                    'Image Compression
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.compression=3")

                    'Image Compression
                    Shell(clsgeneral.StartUpPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & globalFax.gstrFAXPrinterName & " image.pagetype=0")

                Else
                    Return False
                    Exit Function
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region
        Public Sub New()
            MyBase.new()
        End Sub
#Region "IDisposable Implementation"
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub
#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
#End Region
    End Class
#End Region
    Friend Class FaxDBLayer
        Implements IDisposable
        Enum enmDateCriteria
            Today
            Yesterday
            LastWeek
            LastMonth
            Customize
        End Enum
        Enum enmFAXPriority
            NormalPriority
            SendImmediately
        End Enum
        Dim m_FAXPriority As enmFAXPriority
        Private disposedValue As Boolean = False        ' To detect redundant calls

#Region "   Public Properties"
#End Region
        Friend Function AddPendingFAX(ByVal objPendingFax As PendingFax, Optional ByVal CurrentFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority) As Boolean
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer

            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim objParameter As DBParameter
            objParameter = New DBParameter
            Try

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.PatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.FaxTo
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FAXTo"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.FaxType
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FAXType"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.FaxNo
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FAXNo"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.LoginUser
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@LoginUser"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.FileName
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FileName"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = objPendingFax.FaxDate
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@FAXDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                Select Case CurrentFAXPriority
                    Case enmFAXPriority.NormalPriority
                        objParameter.Value = 0
                    Case enmFAXPriority.SendImmediately
                        objParameter.Value = 1
                End Select
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Bit
                objParameter.Name = "@FAXPriority"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                _gloEMRDatabase.GetDataValue("gsp_InUpPendingFAX")

                Return True
            Catch ex As gloDBException
                Return False
            Catch ex As FaxException
                Dim objex As New FaxException
                objex.ErrMessage = "Error while fetching Visit date"
                Throw objex
                Return False
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Friend Function DeletePendingFAX(ByVal strFileName As String) As Boolean
            Dim _gloEMRDatabase As New gloEMRDatabase.DataBaseLayer
            '   Dim dttable As New DataTable
            Dim objParameter As DBParameter
            Try

                objParameter = New DBParameter
                If IsDBNull(strFileName) = False Then
                    objParameter.Value = strFileName
                Else
                    objParameter.Value = ""
                End If
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FileName"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)
                _gloEMRDatabase.Add("gsp_DeletePendingFAX")
                Return True
            Catch ex As gloDBException
                Return False
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Deleting Pending Fax"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
            Return True
        End Function
        Friend Function ReInitialisePendingFAX(ByVal nFAXID As Integer, Optional ByVal strFAXNo As String = "") As Boolean
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer
            '  Dim dttable As New DataTable
            Dim objParameter As DBParameter
            Try

                objParameter = New DBParameter
                objParameter.Value = nFAXID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@FAXID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = strFAXNo
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FAXNo"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                _gloEMRDatabase.Add("gsp_ReinitialisePendingFAX")
                Return True
            Catch ex As gloDBException
                Return False
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Reinitializing Pending Fax"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        'Function to be implemented in this class
        Friend Function Fill_PendingFAXes(ByVal enmFAXDateCriteria As enmDateCriteria, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal nPatientID As Long = 0) As PendingFaxes
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim objParameter As DBParameter
            Try
                Dim dtFAXFromDate As DateTime
                Dim dtFAXToDate As DateTime
                Select Case enmFAXDateCriteria
                    Case enmDateCriteria.Today
                        dtFAXFromDate = System.DateTime.Now.Date
                        dtFAXToDate = System.DateTime.Now.Date.AddDays(1)
                    Case enmDateCriteria.Yesterday
                        dtFAXFromDate = System.DateTime.Now.Date.AddDays(-1)
                        dtFAXToDate = System.DateTime.Now.Date
                    Case enmDateCriteria.LastWeek
                        dtFAXFromDate = System.DateTime.Now.Date.AddDays(-7)
                        dtFAXToDate = System.DateTime.Now.Date.AddDays(1)
                    Case enmDateCriteria.LastMonth
                        dtFAXFromDate = System.DateTime.Now.Date.AddMonths(-1)
                        dtFAXToDate = System.DateTime.Now.Date.AddDays(1)
                    Case enmDateCriteria.Customize
                        dtFAXFromDate = dtFromDate.Date
                        dtFAXToDate = dtToDate.AddDays(1)
                End Select



                objParameter = New DBParameter

                objParameter.Value = dtFromDate
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@FromDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = dtToDate
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@ToDate"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objParameter = New DBParameter
                objParameter.Value = nPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                Dim dtTable As DataTable = _gloEMRDatabase.GetDataTable("gsp_RetrievePendingFAXes")
                If (IsNothing(dtTable) = False) Then


                    If dtTable.Rows.Count > 0 Then
                        Dim _PendingFaxes As New PendingFaxes
                        Dim _PendingFax As PendingFax
                        Dim i As Int32
                        For i = 0 To dtTable.Rows.Count - 1
                            _PendingFax = New PendingFax
                            _PendingFax.PatientName = dtTable.Rows(0)("PatientName")
                            _PendingFax.FaxID = dtTable.Rows(0)("FAXID")
                            _PendingFax.PatientID = dtTable.Rows(0)("PatientID")
                            _PendingFax.FaxTo = dtTable.Rows(0)("FAXTo")
                            _PendingFax.FaxType = dtTable.Rows(0)("FAXType")
                            _PendingFax.FaxNo = dtTable.Rows(0)("FAXNo")
                            _PendingFax.LoginUser = dtTable.Rows(0)("LoginUser")
                            _PendingFax.FaxDate = dtTable.Rows(0)("FAXDate")
                            _PendingFax.FileName = dtTable.Rows(0)("FAXFileName")
                            _PendingFax.NoofAttempts = dtTable.Rows(0)("NoOfAttempts")
                            _PendingFax.CurrentStatus = dtTable.Rows(0)("CurrentStatus")
                            _PendingFaxes.Add(_PendingFax)
                        Next
                        dtTable.Dispose()
                        dtTable = Nothing
                        Return _PendingFaxes
                    Else
                        dtTable.Dispose()
                        dtTable = Nothing
                        Return Nothing
                    End If
                Else

                    Return Nothing
                End If
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = ""
                Throw objex
                Return Nothing
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try

        End Function
        Friend Function Fill_PendingFAXes(Optional ByVal nPatientID As Long = 0, Optional ByVal nMaxNoAttempts As Int16 = 0) As PendingFaxes
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer
            Try


                Dim objParameter As DBParameter
                objParameter = New DBParameter

                objParameter.Value = nPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)


                If nMaxNoAttempts > 0 Then
                    objParameter.Value = nMaxNoAttempts
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.DataType = SqlDbType.Int
                    objParameter.Name = "@MaxNoOfAttempts"
                    _gloEMRDatabase.DBParametersCol.Add(objParameter)
                End If

                Dim dtTable As DataTable = _gloEMRDatabase.GetDataTable("gsp_RetrieveAllPendingFAXes")
                If (IsNothing(dtTable) = False) Then


                    If dtTable.Rows.Count > 0 Then
                        Dim _PendingFaxes As New PendingFaxes
                        Dim _PendingFax As PendingFax
                        Dim i As Int32
                        For i = 0 To dtTable.Rows.Count - 1
                            _PendingFax = New PendingFax

                            _PendingFax.FaxID = dtTable.Rows(0)("FAXID")
                            _PendingFax.PatientID = dtTable.Rows(0)("PatientID")
                            _PendingFax.PatientName = dtTable.Rows(0)("PatientName")
                            _PendingFax.FaxTo = dtTable.Rows(0)("FAXTo")
                            _PendingFax.FaxType = dtTable.Rows(0)("FAXType")
                            _PendingFax.FaxNo = dtTable.Rows(0)("FAXNo")
                            _PendingFax.LoginUser = dtTable.Rows(0)("LoginUser")
                            _PendingFax.FaxDate = dtTable.Rows(0)("FAXDate")
                            _PendingFax.FileName = dtTable.Rows(0)("FAXFileName")
                            _PendingFax.NoofAttempts = dtTable.Rows(0)("NoOfAttempts")
                            _PendingFax.CurrentStatus = dtTable.Rows(0)("CurrentStatus")
                            _PendingFaxes.Add(_PendingFax)
                        Next
                        dtTable.Dispose()
                        dtTable = Nothing
                        Return _PendingFaxes
                    Else
                        dtTable.Dispose()
                        dtTable = Nothing
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = ""
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try

        End Function
        Friend Function GetPharmacyFAXNo(ByVal nPatientID As Long) As ContactInformation

            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim objmytable As DataTable
            Dim objParameter As DBParameter
            Try


                objParameter = New DBParameter

                objParameter.Value = nPatientID ''gloGeneral.globalPatient.gnPatientID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.DateTime
                objParameter.Name = "@PatientID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                objmytable = _gloEMRDatabase.GetDataTable("gsp_GetFaxNo")
                Dim _contactinformation As New ContactInformation
                If (IsNothing(objmytable) = False) Then
                    If objmytable.Rows.Count > 0 Then
                        _contactinformation.Address.Fax = objmytable.Rows(0)(0)
                        _contactinformation.Name = objmytable.Rows(0)(1)
                        _contactinformation.Address.Phone = objmytable.Rows(0)(2)
                        _contactinformation.Address.Street = objmytable.Rows(0)(3) & " " & objmytable.Rows(0)(4)
                        _contactinformation.Address.City = objmytable.Rows(0)(5)
                        _contactinformation.Address.State = objmytable.Rows(0)(6)
                        _contactinformation.Address.Zip = objmytable.Rows(0)(7)
                    End If
                    objmytable.Dispose()
                    objmytable = Nothing
                End If
                Return _contactinformation
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Fetching Pharmacy FaxNo"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Friend Function GetContactFAXNo(ByVal nContactID As Long) As String
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim strFAXNo As String
            Dim objParameter As DBParameter
            Try


                objParameter = New DBParameter

                objParameter.Value = nContactID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.BigInt
                objParameter.Name = "@nContactID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                strFAXNo = _gloEMRDatabase.GetDataValue("gsp_GetContactFaxNo")
                Return strFAXNo
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Fetching Contact FaxNo"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Friend Function RetrieveFAXDetails(ByVal strFileName As String) As PendingFax
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim dttable As DataTable = Nothing
            Dim objParameter As DBParameter
            Dim _PendingFax As New PendingFax
            Try

                objParameter = New DBParameter

                objParameter.Value = strFileName
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.VarChar
                objParameter.Name = "@FileName"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                dttable = _gloEMRDatabase.GetDataTable("gsp_RetrievePendingDetails")
                If (IsNothing(dttable) = False) Then


                    If dttable.Rows.Count > 0 Then

                        _PendingFax.FaxID = dttable.Rows(0)("FAXID")
                        _PendingFax.PatientID = dttable.Rows(0)("PatientID")
                        _PendingFax.PatientName = dttable.Rows(0)("PatientName")
                        _PendingFax.FaxTo = dttable.Rows(0)("FAXTo")
                        _PendingFax.FaxType = dttable.Rows(0)("FAXTo")
                        _PendingFax.FaxNo = dttable.Rows(0)("FAXNo")
                        _PendingFax.LoginUser = dttable.Rows(0)("LoginUser")
                        _PendingFax.FaxDate = dttable.Rows(0)("FAXDate")
                    End If
                    dttable.Dispose()
                    dttable = Nothing
                End If
                Return _PendingFax
            Catch ex As gloDBException
                Return _PendingFax
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Fetching Pending Faxes"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Friend Function RetrieveFAXAttemptsDetails(ByVal nFAXID As Integer) As PendingFaxDetails
            Dim _gloEMRDatabase As gloEMRDatabase.DataBaseLayer
            _gloEMRDatabase = New gloEMRDatabase.DataBaseLayer

            Dim dttable As DataTable
            Dim objParameter As DBParameter

            Try
                objParameter = New DBParameter
                objParameter.Value = nFAXID
                objParameter.Direction = ParameterDirection.Input
                objParameter.DataType = SqlDbType.Int
                objParameter.Name = "@FAXID"
                _gloEMRDatabase.DBParametersCol.Add(objParameter)

                dttable = _gloEMRDatabase.GetDataTable("gsp_FillFAXAttemptsDetails")
                If (IsNothing(dttable) = False) Then


                    If dttable.Rows.Count > 0 Then
                        Dim _PendingFaxdetails As New PendingFaxDetails
                        Dim _PendingFaxdetail As PendingFaxDetail
                        Dim i As Int32
                        For i = 0 To dttable.Rows.Count - 1
                            _PendingFaxdetail = New PendingFaxDetail
                            _PendingFaxdetail.FaxDate = dttable.Rows(0)("AttemptDate")
                            _PendingFaxdetail.FaxDTLID = dttable.Rows(0)("FAXDTLID")
                            _PendingFaxdetail.FaxResponse = dttable.Rows(0)("FAXResponse")
                            _PendingFaxdetails.Add(_PendingFaxdetail)
                        Next
                        dttable.Dispose()
                        dttable = Nothing
                        Return _PendingFaxdetails
                    Else
                        dttable.Dispose()
                        dttable = Nothing
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Catch ex As gloDBException
                Return Nothing
            Catch ex As Exception
                Dim objex As New FaxException
                objex.ErrMessage = "Error Fetching Pending Fax Details"
                Throw objex
            Finally
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End Try
        End Function
        Friend Sub New()
            MyBase.New()
        End Sub
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Public Class globalPrint
        Implements IDisposable
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub
#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
    Public Class globalFax
        Implements IDisposable
        Enum enmFAXPriority
            NormalPriority
            SendImmediately
        End Enum
        Public Enum enmFAXType
            PatientExam
            PatientLetters
            PatientMessages
            PatientOrders
            FormGallery
            ReferralLetter
            Prescription
            PTProtocols
            Medication
        End Enum
        Public Shared gstrFAXPrinterName As String
        Public Shared gnClientMachineID As String
        Public Shared gstrRxReportpath As String
        Public Shared gstrFaxCoverPage As String
        'FAX Settings
        Public Shared gstrFAXOutputDirectory As String
        Public Shared gnNoOfAttempts As Int16
        Public Shared gblnSameCoverPageForAllReferrals As Boolean
        Public Shared gblnFAXPrinterSettingsSet As Boolean = False
        Public Shared gstrFAXCompression As String = ""
        Public Shared gblnFAXCoverPage As Boolean
        Public Shared CurrentSendingFAXPriority As enmFAXPriority = enmFAXPriority.NormalPriority
        Public Shared gstrFAXContactPerson As String
        Public Shared gstrFAXContactPersonFAXNo As String
        Public Shared gstrFAXType As String = ""
        Public Shared eFAXType As enmFAXType
        Public Shared Sub UpdateLog(ByVal strLogMessage As String)
            Try

                Dim objFile As New System.IO.StreamWriter(clsgeneral.StartUpPath & "\gloEMRFAXLogTest.txt", True)
                objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
                objFile.Close()
                objFile.Dispose()
                objFile = Nothing
            Catch ex As Exception
            End Try
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
    Public Class PrintException
        Inherits ApplicationException
        Implements IDisposable
        Private _ErrMessage As String
        Private _ErrCode As String
        Private disposedValue As Boolean = False        ' To detect redundant calls

        Public Property ErrMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal Value As String)
                _ErrMessage = Value
            End Set
        End Property

        Public Property ErrCode() As String
            Get
                Return _ErrCode
            End Get
            Set(ByVal Value As String)
                _ErrCode = Value
            End Set
        End Property
        Public Sub New()
            MyBase.New()
        End Sub


        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Public Class FaxException
        Inherits ApplicationException
        Implements IDisposable
        Private _ErrMessage As String
        Private _ErrCode As String
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Public Property ErrMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal Value As String)
                _ErrMessage = Value
            End Set
        End Property
        Public Property ErrCode() As String
            Get
                Return _ErrCode
            End Get
            Set(ByVal Value As String)
                _ErrCode = Value
            End Set
        End Property
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

End Namespace
