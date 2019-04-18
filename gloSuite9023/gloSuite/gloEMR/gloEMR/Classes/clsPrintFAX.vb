Imports System.IO
Imports System.Data.SqlClient
'Imports gloEMR.gloAuditTrail.gloAuditTrail
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Runtime.InteropServices

Public Class clsPrintFAX
    Implements IDisposable
  

    Public Shared IsBlackIceSettingsSet As Boolean = False
    Public IsCancle As Boolean = False
    Public InsertCoverPageFirstForFaxWithCoverPage As Boolean = 1

#Region "Private Variables"

    'File Name which user wants to Print or FAX
    Enum enmFAXType
        PatientExam
        PatientLetters
        PatientMessages
        PatientOrders
        FormGallery
        ReferralLetter
        Prescription
        PTProtocols
        PatientMaterials
        NurseNotes
    End Enum

    Dim objCon As New SqlConnection

    Dim _sFileName As String

    'FAX Paramters - When user wants to send the FAX
    Dim _nPatientID As Long            ' Patient ID - FAX of Which Patient
    Dim _sFAXTo As String               ' FAX To
    Dim _sFAXType As String             ' Type of FAX e.f. Patient Exam, Patient Letters, Orders etc.
    Dim _sFAXNo As String               ' FAX No
    Dim _sLoginUser As String           ' Login User -  i.e. who has send the FAX
    Dim _dtFAXDate As DateTime          ' FAX Created Date & Time
    Dim _sDefaultPrinter As String = String.Empty     ' System Default Printer
    Dim _sFAXPrinter As String         ' FAX Printer
    Dim _sErrorMessage As String            ' Error Message
    '  Dim objWord As clsWordDocument      ''slr not used
    'Dim oTempWordApp As Wd.Application  ''slr make it local
    Dim _sFAXTypeDetails As String = ""
    Dim _sPriviousUsedPrinter As String = ""
    'Dim oChange As Object = False   Slr not used


#End Region

#Region "Properties"
    'File Name which user wants to Print or FAX
    Public Property FileName() As String
        Get
            Return _sFileName
        End Get
        Set(ByVal Value As String)
            _sFileName = Value
        End Set
    End Property
    Public Property GetPrivioousUsedPrinter() As String
        Get
            Return _sPriviousUsedPrinter
        End Get
        Set(ByVal Value As String)
            _sPriviousUsedPrinter = Value
        End Set
    End Property

    'FAX Parameters
    Public Property PatientID() As Long
        Get
            Return _nPatientID
        End Get
        Set(ByVal Value As Long)
            _nPatientID = Value
        End Set
    End Property
    Public Property FAXTo() As String
        Get
            Return _sFAXTo
        End Get
        Set(ByVal Value As String)
            _sFAXTo = Value
        End Set
    End Property
    Public Property FAXType() As String
        Get
            Return _sFAXType
        End Get
        Set(ByVal Value As String)
            _sFAXType = Value
        End Set
    End Property
    Public Property FAXNo()
        Get
            Return _sFAXNo
        End Get
        Set(ByVal Value)
            _sFAXNo = Value
        End Set
    End Property
    Public Property LoginUser() As String
        Get
            Return _sLoginUser
        End Get
        Set(ByVal Value As String)
            _sLoginUser = Value
        End Set
    End Property
    Public Property FAXDate() As DateTime
        Get
            Return _dtFAXDate
        End Get
        Set(ByVal Value As DateTime)
            _dtFAXDate = Value
        End Set
    End Property

    Public ReadOnly Property DefaultPrinter() As String
        Get
            Return _sDefaultPrinter
        End Get
    End Property
    Public ReadOnly Property FAXPrinter() As String
        Get
            Return _sFAXPrinter
        End Get
    End Property

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _sErrorMessage
        End Get
    End Property



    'sarika internet fax
    Public Property FAXTypeDetails() As String
        Get
            Return _sFAXTypeDetails
        End Get
        Set(ByVal value As String)
            _sFAXTypeDetails = value
        End Set
    End Property

    'Public Property WdContainer() As AxDSOFramer.AxFramerControl
    '    Get
    '        Return _objWdContainer
    '    End Get
    '    Set(ByVal value As AxDSOFramer.AxFramerControl)
    '        _objWdContainer = value
    '    End Set
    'End Property

    'sarika internet fax

#End Region

#Region "Constructor"
    Public Sub New()
        'Retrieve the Default Printer
        If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
            Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
            _sDefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
            objPrintDocument.Dispose()
            objPrintDocument = Nothing
        End If
       
    End Sub
    Public Sub New(ByVal strFAXPrinter As String)
        _sFAXPrinter = strFAXPrinter
        'Retrieve the Default Printer
        If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
            Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
            _sDefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
            objPrintDocument.Dispose()
            objPrintDocument = Nothing
        End If

    End Sub

#End Region

#Region "Public Functions"
    'To Print Document on Default System Printer
    Public Function PrintDocument(ByVal strFileName As String) As Boolean
        Return Nothing
    End Function
    'to Print Document on Specific Printer
    Public Function PrintDocument(ByVal strFileName As String, ByVal sPrinterName As String) As Boolean
        Return Nothing
    End Function

    'To FAX Document without FAX Parameters - It will take the FAX Parameters of Private variables
    Public Function FAXDocument(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal objDoc As String, ByVal enmFAXDocumentType As enmFAXType) As Boolean
        Return FAXDocument(myLoadWord, objDoc, _nPatientID, _sFAXTo, _sFAXType, _sFAXNo, _sLoginUser, _dtFAXDate, enmFAXDocumentType)
    End Function

    ''Added for Fax Module changes.
    '' Modified Function at the time of GLO2011-0014841 - Fax Header Issue...
    Public Function FAXDocumentRevised(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal sDocFileName As String, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True) As Boolean
        '  Dim wdTempFax As Wd.Application
        ' Dim oTempWordApp As Wd.Application = objDoc.ActiveWindow.Application
        Dim smyfile As String = ""
        Dim oSetting As New gloSettings.GeneralSettings(GetConnectionString)
        oSetting.GetSetting("InsertCoverPageFirstForFaxWithCoverPage", InsertCoverPageFirstForFaxWithCoverPage)

        '' Create a copy of main doc
        'Dim _sFileName As Object = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
        'Try
        '    objDoc.SaveAs(_sFileName)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try
        Try
            ' wdTempFax = New Wd.Application
            '' When eFax is OFF
            For Each node As myTreeNode In gstrfaxCollection
                '' Create a document object of main doc
                Dim objMainDoc As Wd.Document   'SLR: new is not needed

                If gblnFAXCoverPage = True AndAlso (String.IsNullOrEmpty(node.FaxCoverPage) = False) AndAlso File.Exists(node.FaxCoverPage) Then
                    If InsertCoverPageFirstForFaxWithCoverPage = True Then
                        smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        FileCopy(node.FaxCoverPage, smyfile)

                        '   objMainDoc = wdTempFax.Documents.Open(smyfile)
                        objMainDoc = myLoadWord.LoadWordApplication(smyfile)
                        objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                        objMainDoc.Activate()
                        objMainDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)

                        ' Main Doc Insert 
                        If File.Exists(sDocFileName) Then
                            UpdateLogForFax("Insert the Main Page")
                            UpdateLog("Insert the Main  Page")
                            objMainDoc.ActiveWindow.SetFocus()
                            objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                            objMainDoc.Application.Selection.InsertFile(sDocFileName)
                            UpdateLog("Main doc Inserted")
                            UpdateLogForFax("Main doc Inserted")
                        End If
                    Else
                        smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        FileCopy(sDocFileName, smyfile)

                        objMainDoc = myLoadWord.LoadWordApplication(smyfile)
                        objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                        objMainDoc.Activate()

                        If gblnFAXCoverPage = True AndAlso File.Exists(node.FaxCoverPage) Then
                            UpdateLogForFax("Insert the Cover Page")
                            UpdateLog("Insert the Cover Page")
                            objMainDoc.ActiveWindow.SetFocus()
                            objMainDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                            objMainDoc.Application.Selection.InsertFile(node.FaxCoverPage)
                            objMainDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                            UpdateLog("Cover Page Inserted")
                            UpdateLogForFax("Cover Page Inserted")
                        End If
                    End If
                Else
                    ' objMainDoc = oTempWordApp.ActiveDocument
                    smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                    FileCopy(sDocFileName, smyfile)
                    '  wdTempFax = New Wd.Application
                    ' objMainDoc = wdTempFax.Documents.Open(smyfile)

                    objMainDoc = myLoadWord.LoadWordApplication(smyfile)
                    objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                    objMainDoc.Activate()
                    'objMainDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)

                    If (String.IsNullOrEmpty(node.FaxCoverPage) = False) Then
                        '' Fax Page Insert 
                        If gblnFAXCoverPage = True AndAlso File.Exists(node.FaxCoverPage) Then
                            UpdateLogForFax("Insert the Cover Page")
                            UpdateLog("Insert the Cover Page")
                            objMainDoc.ActiveWindow.SetFocus()
                            objMainDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                            objMainDoc.Application.Selection.InsertFile(node.FaxCoverPage)
                            objMainDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                            UpdateLog("Cover Page Inserted")
                            UpdateLogForFax("Cover Page Inserted")
                        End If
                    End If
                End If

                If (gblnInternetFax = False) Then
                    UpdateLog("IsFAXPrinterHasToSet: " & IsFAXPrinterHasToSet)
                    If IsFAXPrinterHasToSet Then
                        UpdateLog("Setting Printer")
                        'Set the FAX Printer as the default Printer
                        myLoadWord.FilePrinterSetup(_sFAXPrinter, 1)
                        UpdateLog("Printer set")
                    End If
                End If

                If gblnPageNo = True Then
                    UpdateLog("InsertNamePageNo start ")

                    If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                        InsertNamePageNo(objMainDoc, GetPatientDetails(nPatientID))
                    Else
                        InsertPageFooterWithoutMSWBuildingBlock(objMainDoc, GetPatientDetails(nPatientID))
                    End If

                    UpdateLog("InsertNamePageNo end")
                End If

                '' Cleanup code
                If enmFAXDocumentType <> enmFAXType.Prescription Then
                    UpdateLogForFax("Start Clean up")
                    UpdateLog("Start Clean up")
                    ''objMainDoc.ActiveWindow.SetFocus()
                    'objMainDoc = CleanupDoc(objMainDoc)
                    gloWord.LoadAndCloseWord.CleanupDoc(objMainDoc)
                    UpdateLog("Cleaning up Form Fields Done")
                    UpdateLogForFax("Cleaning up Form Fields Done")
                End If

                Try
                    If (gblnInternetFax = False) Then

                        Dim sFileNameTemp As String = myLoadWord.SaveCurrentWord(objMainDoc, gloSettings.FolderSettings.AppTempFolderPath)
                        'Try
                        '    objMainDoc.SaveAs(sFileNameTemp)
                        'Catch ex As Exception
                        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        'End Try

                        '' Generate the binary file & save to DB
                        FAXDocumentNormalRevised(myLoadWord, objMainDoc, nPatientID, node.FaxName, node.FaxTo, sLoginUser, dtFAXDate, sFAXTypeDetails, enmFAXDocumentType, IsCleanUpRequired, IsFAXPrinterHasToSet, IsDSODefaultPrinterHasToSet, node)
                        'While (wdTempFax.BackgroundPrintingStatus <> 0)
                        '    System.Windows.Forms.Application.DoEvents()
                        '    Threading.Thread.Sleep(100)
                        'End While
                        '' Dispose Temp Document object
                        'objMainDoc.Close(False)
                        'If Not IsNothing(objMainDoc) Then
                        '    System.Runtime.InteropServices.Marshal.ReleaseComObject(objMainDoc) '  'SLR: marshall free
                        'End If
                        'objMainDoc = Nothing
                        myLoadWord.CloseWordOnly(objMainDoc)

                        ''Delete
                        If smyfile <> "" Then
                            If File.Exists(smyfile) Then
                                File.Delete(smyfile)
                                smyfile = ""
                            End If
                        End If

                        ' delete the temp files
                        If File.Exists(sFileNameTemp) Then
                            File.Delete(sFileNameTemp)
                        End If
                    Else
                        Dim sFileNameTemp As String = ""
                        Try
                            sFileNameTemp = myLoadWord.SaveCurrentWord(objMainDoc, gloSettings.FolderSettings.AppTempFolderPath)
                            'Try
                            '    objMainDoc.SaveAs(sFileNameTemp)
                            'Catch ex As Exception
                            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            'End Try

                            '' Dispose Temp Document object
                            'objMainDoc.Close(False)
                            'If Not IsNothing(objMainDoc) Then
                            '    System.Runtime.InteropServices.Marshal.ReleaseComObject(objMainDoc) '  'SLR: marshall free
                            'End If
                            'objMainDoc = Nothing
                            myLoadWord.CloseWordOnly(objMainDoc)
                            'If Not wdTemp Is Nothing Then
                            '    wdTemp.Quit()
                            '    If Not IsNothing(wdTemp) Then
                            '        System.Runtime.InteropServices.Marshal.ReleaseComObject(wdTemp) '  'SLR: marshall free
                            '    End If
                            '    wdTemp = Nothing
                            'End If

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        End Try

                        '' Generate the binary file & save to DB
                        IFAXDocument(sFileNameTemp, nPatientID, node.FaxName, node.FaxTo, sLoginUser, dtFAXDate, sFAXTypeDetails, enmFAXDocumentType, IsCleanUpRequired, IsFAXPrinterHasToSet, IsDSODefaultPrinterHasToSet)

                        ''Delete
                        If smyfile <> "" Then
                            If File.Exists(smyfile) Then
                                File.Delete(smyfile)
                                smyfile = ""
                            End If
                        End If

                        ' delete the temp files
                        If File.Exists(sFileNameTemp) Then
                            File.Delete(sFileNameTemp)
                        End If
                    End If

                Catch ex As Exception
                    Return False
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try
            Next

            If (IsNothing(oSetting) = False) Then
                oSetting.Dispose() : oSetting = Nothing
            End If

            'If Not wdTempFax Is Nothing Then
            '    While (wdTempFax.BackgroundPrintingStatus <> 0)
            '        System.Windows.Forms.Application.DoEvents()
            '        Threading.Thread.Sleep(100)
            '    End While
            '    wdTempFax.Quit()
            '    ''Commeneted 0n 20141031 To Resolve issue:# Target of invocation
            '    If Not IsNothing(wdTempFax) Then
            '        System.Runtime.InteropServices.Marshal.ReleaseComObject(wdTempFax) '  'SLR: marshall free
            '    End If
            '    wdTempFax = Nothing
            'End If
        Catch ex As Exception
            Return False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return True
    End Function
    ''Added for Fax Module changes.

    Public Function FAXDocument(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal objDoc As String, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True) As Boolean
        Try
            ''Fax Module change
            FAXDocumentRevised(myLoadWord, objDoc, nPatientID, sFAXTo, sFAXNo, sLoginUser, dtFAXDate, sFAXTypeDetails, enmFAXDocumentType, IsCleanUpRequired, IsFAXPrinterHasToSet, IsDSODefaultPrinterHasToSet)
            Return Nothing
        Catch ex As ClsFaxException
            UpdateLogForFax(ex.ToString)
            ''For Fax Module change
            Return False
            ''For Fax Module change
            Throw ex
        Catch ex As Exception
            'UpdateLogForFax(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''For Fax Module change
            Return False
            ''For Fax Module change
            Throw New ClsFaxException(ex.ToString)
        End Try
    End Function

    '' For Fax Module Changes
    Public Function FAXDocumentNormalRevised(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByRef objDoc As Wd.Document, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True, Optional ByVal node As myTreeNode = Nothing) As Boolean
        'Clear the error message
        _sErrorMessage = ""

        'Check FAX Printer settings are set or not
        If isPrinterSettingsSet(True) = False Then
            FAXDocumentNormalRevised = Nothing
            Exit Function
        End If
        '--------

        Dim blnTIFFFileGenerated As Boolean = False     'Temporary variable to check the TIFF File has been generated or not
        Dim strTIFFFileName As String = ""              ' TIFF File Name

        'Create the Object of FAX Class
        Dim objFAX As New clsFAX
        If IsBlackIceSettingsSet = False Then
            Try
                'code commented by sarika 21st nov 07 
                MainMenu.SetFAXPrinterDefaultSettings1()
            Catch ex As Exception
                MessageBox.Show("Error while setting Printer Default settings. " & ex.ToString, "gloEMR Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Try
            strTIFFFileName = RetrieveFAXDocumentName()
            If MainMenu.SetFAXPrinterDocumentSettings1(strTIFFFileName) = False Then
                FAXDocumentNormalRevised = Nothing
                Exit Function
            End If

            UpdateLog("Starts Printing.....")
            '' Bug #16438: Fax >> Application is printing document instead of sending fax, if the eFax setting is off.
            '' following line added to resolve the above bug.
            objDoc.Application.WordBasic.FilePrintSetup(Printer:=_sFAXPrinter, DoNotSetAsSysDefault:=1)
            '' generate a TIFF 
            'objDoc.Application.Options.PrintBackground = True
            'objDoc.PrintOut(Background:=True)

            '' objDoc.PrintOut()
            ''To wait until the fax gets printed so that the process doesnt go further
            'Threading.Thread.Sleep(300)
            gloWord.LoadAndCloseWord.PrintWordDocument(objDoc, False, True)
            Dim strfname As String = ""
            strfname = strTIFFFileName
            'End Shweta 20100109
            objFAX.AddPendingFAX(nPatientID, node.FaxName, gstrFAXType, node.FaxTo, sLoginUser, strfname, dtFAXDate, CurrentSendingFAXPriority)
            strTIFFFileName = String.Empty
            'No error occurs while Printing means TIFF File has been successfully generated
            blnTIFFFileGenerated = True
            If (IsDSODefaultPrinterHasToSet = True) AndAlso (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                '' Bug #16438: Fax >> Application is printing document instead of sending fax, if the eFax setting is off.
                'oTempWordApp.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)
                objDoc.Application.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)
            End If

        Catch ex As Exception
            'Error occured. So TIFF File has not been generated
            blnTIFFFileGenerated = False
            'Assign the error message
            _sErrorMessage = ex.Message
        Finally
            'Check the TIFF FIle has been successfully generated
            'If TIFF File has not been generated then delete the DB entry
            If blnTIFFFileGenerated = False Then
                'Delete DB Entry
                If Trim(strTIFFFileName) <> "" Then
                    objFAX.DeletePendingNormalFAX(strTIFFFileName)
                End If
            Else
                'Add the Entry in Audit Trail
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "FAX has been send - FAX To=" & sFAXTo & ", FAX No=" & sFAXNo & ", FAX Type=" & gstrFAXType, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            If Not IsNothing(objFAX) Then
                objFAX.Dispose()
                objFAX = Nothing
            End If
            ' objDoc = Nothing
        End Try
        Return blnTIFFFileGenerated
    End Function
    '' For Fax Module Changes    
    ''parameter blnPrintCancel added to ckeck whether printing is cancled or not bugid 86984
    Public Function PrintDoc(ByRef oPrint As Wd.Document, Optional ByVal m_PatientId As Int64 = 0, Optional ByVal blnShowPrinterDialog As Boolean = True, Optional ByVal PageNo As Integer = 0, Optional ByRef blnPrintCancel As Boolean = False, Optional ByRef _PreviousUsedPrinter As String = "")
        Dim oTempDoc As Wd.Document = oPrint
        gloWord.LoadAndCloseWord.CleanupDoc(oTempDoc)
        If gblnPageNo = True Then
            '' GLO2011-0014004 : Footers are missing in the Notes that are submitted to the insurance carriers
            '' Function name chenged from GetPatientName to GetPatientDetails

            If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                InsertNamePageNo(oTempDoc, GetPatientDetails(m_PatientId))
            Else
                InsertPageFooterWithoutMSWBuildingBlock(oTempDoc, GetPatientDetails(m_PatientId))
            End If
        End If
        If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
            oTempDoc.Application.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)


            '' Commented at the time of GLO2011-0014841 - FAX Header Issue, To resolve Formated Text Printed 
            '' instead of Liquid Link
            'oTempDoc.PrintPreview()
            If (PageNo > 0) Then
                Dim what As Object = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage
                Dim which As Object = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst
                Dim count As Object = PageNo
                Dim missing As Object = System.Reflection.Missing.Value
                Try
                    oTempDoc.Application.Selection.[GoTo](what, which, count, missing)
                Catch ex As Exception
                    'UpdateLog("Unable to Goto a particular page before printing.  Exception: " + ex.ToString())
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Goto a particular page before printing.  Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try


            End If
        End If
        If gblnUseDefaultPrinter = False AndAlso blnShowPrinterDialog AndAlso gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) = False Then
            ''Referral letter print issue Resolved #Referral letter was printing only selected exam:Mayuri 20140825
            'oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            'If (oTempDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).Show() = -1) Then

            '    System.Threading.Thread.Sleep(1000) ''As it is not printing the 1st document.
            '    _sPriviousUsedPrinter = oTempDoc.Application.ActivePrinter.ToString()
            '    PrintDoc = DialogResult.OK
            'Else
            '    PrintDoc = DialogResult.Cancel
            '    IsCancle = True
            'End If
            'oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            Dim myPrinter As String = gloWord.LoadAndCloseWord.PrintWordDocument(oTempDoc, True, False, m_PatientId)
            If (IsNothing(myPrinter) = False) Then
                If (myPrinter <> "Copied") Then
                    _sPriviousUsedPrinter = myPrinter
                    _PreviousUsedPrinter = _sPriviousUsedPrinter
                End If
                PrintDoc = DialogResult.OK
            Else
                PrintDoc = DialogResult.Cancel
                IsCancle = True
                blnPrintCancel = True
            End If
        Else
            PrintDoc = DialogResult.OK
            If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                If (Trim(_PreviousUsedPrinter) <> "") Then
                    oTempDoc.Application.WordBasic.FilePrintSetup(Printer:=_PreviousUsedPrinter, DoNotSetAsSysDefault:=1)
                Else
                    oTempDoc.Application.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)
                End If
            End If
            'oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            'oTempDoc.Application.Options.PrintBackground = True
            'oTempDoc.PrintOut(Background:=True)
            'While oTempDoc.Application.BackgroundPrintingStatus <> 0
            '    System.Windows.Forms.Application.DoEvents()
            '    System.Threading.Thread.Sleep(100)
            'End While
            'oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll

            Dim myPrinter As String = gloWord.LoadAndCloseWord.PrintWordDocument(oTempDoc, False, False, m_PatientId)
            ''added condition to ckeck whether printing is cancled or not bugid 86984
            If (IsNothing(myPrinter)) Then
                gloAuditTrail.gloAuditTrail.ExceptionLog("Some exception occurred while printing. Printing will be aborted now.", True)
                PrintDoc = DialogResult.Cancel
                IsCancle = True
                blnPrintCancel = True
            End If
        End If


            'If Not IsNothing(oTempDoc) Then 'SLR: Quit/close and Marshall free oTempDoc
            '    'oTempDoc.Close()

            '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTempDoc)  ''slr free it

            'End If
            'oTempDoc = Nothing  'Change made to solve memory Leak and word crash issue

    End Function

    '' for Fax module change.
    Public Function SendDoc(ByRef oSend As Wd.Document, Optional ByVal m_PatientId As Int64 = 0) As String
        Dim oTempDoc As Wd.Document = oSend
        Dim strSendFilename As String = String.Empty
        Try
            strSendFilename = oTempDoc.FullName
            gloWord.LoadAndCloseWord.CleanupDoc(oTempDoc)


            If gblnPageNo = True Then
                If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                    InsertNamePageNo(oTempDoc, GetPatientDetails(m_PatientId))
                Else
                    InsertPageFooterWithoutMSWBuildingBlock(oTempDoc, GetPatientDetails(m_PatientId))
                End If
            End If

            oTempDoc.SaveAs(strSendFilename, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

            Return strSendFilename
        Catch ex As Exception
            strSendFilename = ""
            Return strSendFilename
        Finally
            'If Not IsNothing(oTempDoc) Then 'SLR: Quit/close and Marshall free oTempDoc
            '    ' oTempDoc.Close()
            '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTempDoc)

            'End If
            'oTempDoc = Nothing
        End Try
    End Function

    Public Function GetFaxServiceVendor() As String

        Dim sFaxVendorQuery As String
        Dim objResult As Object

        Dim oDb As New gloDatabaseLayer.DBLayer(GetConnectionString(gstrServicesServerName, gstrServicesDBName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR))

        Try
            
            oDb.Connect(False)

            sFaxVendorQuery = " SELECT Distinct     " &
                              "  (select sSettingsValue from GLSettings where sSettingsName='FaxAccountType' AND GLSettings.nReferenceId=DBSettings.nDBConnectionId) AS AccountType " &
                              "  FROM         DBSettings INNER JOIN " &
                              "  GLSettings ON DBSettings.nDBConnectionId = GLSettings.nReferenceId " &
                              "  WHERE     (LOWER(DBSettings.sServiceName) = 'sfax') and sDatabaseName ='" & gstrDatabaseName & "'"

            objResult = oDb.ExecuteScalar_Query(sFaxVendorQuery)

            If Not objResult Is Nothing AndAlso objResult <> "" Then
                If Convert.ToInt32(objResult) = 3 Then 'Vendor = UPDOX
                    Return "UPDOX"
                End If
            End If
            Return ""
        Catch ex As Exception
            Return ""
        Finally
            oDb.Disconnect()
            If Not oDb Is Nothing Then
                oDb.Dispose()
                oDb = Nothing
            End If
        End Try
    End Function
    Private Shared _strlocalprinter As String = String.Empty
    Private Shared Function SetlocalPrinter() As String
        For Each strprinter As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            If (strprinter.ToLower().Contains("redirect") = False) Then
                If (strprinter.ToLower().Contains("black") OrElse strprinter.ToLower().Contains("xps") OrElse strprinter.ToLower().Contains("wonder")) Then
                    _strlocalprinter = strprinter
                    Return _strlocalprinter
                    Exit For
                End If

            End If
        Next



    End Function
    Public Function EFAXDocumentRevised(ByVal objEFaxSettings As clsEFaxSettings) As String

        UpdateLogForFax("---------In EFAX Document method-----------")
        'Clear the error message
        _sErrorMessage = ""

        Dim objFAX As New clsFAX
        Dim nFaxID As Int64 = 0
        Dim FaxVendor As String = String.Empty

        FaxVendor = GetFaxServiceVendor()


        Dim oByte As Byte() = Nothing
        Dim strData As String = ""
        Dim sDocExt As String = ""
        Dim sPDFfileName As String = ""
        Dim myLoadWord As gloWord.LoadAndCloseWord = Nothing
        ''added against case CAS-21517-R5R4D4 multiple instance of words are getting created
        Dim oCurDoc As Wd.Document = Nothing
        Try
            ' Check if the Fax service is configured with Updox vendor then conver the document into the PDF format 
            'and save into the Peding Fax table
            If FaxVendor = "UPDOX" And objEFaxSettings.EFax_DocumentExtension = "docx" Then

                sPDFfileName = Path.GetDirectoryName(objEFaxSettings.EFax_Faxfilepath) & "\" & System.Guid.NewGuid().ToString & ".PDF"
                myLoadWord = New gloWord.LoadAndCloseWord()
                '  Dim oword As Wd.Application = New Wd.Application()
                'Dim oWordConverter As Wd.Application = CreateObject("Word.Application")
                If (_strlocalprinter = String.Empty) Then
                    SetlocalPrinter()
                End If
                ''added for case CAS-21517-R5R4D4 slowness while sending fax
                If (_strlocalprinter.Trim() <> String.Empty) Then
                    '  oword.WordBasic.FilePrintSetup(Printer:=_strlocalprinter, DoNotSetAsSysDefault:=1)
                    myLoadWord.FilePrinterSetup(_strlocalprinter, 1)
                End If

                oCurDoc = myLoadWord.LoadWordApplication(objEFaxSettings.EFax_Faxfilepath)
                oCurDoc.Activate()



                oCurDoc.SaveAs(sPDFfileName, Wd.WdSaveFormat.wdFormatPDF)
                myLoadWord.CloseWordOnly(oCurDoc)
                '  oCurDoc.Close()






                ' myLoadWord.CloseWordOnly(oCurDoc)

                oByte = File.ReadAllBytes(sPDFfileName)
                strData = Convert.ToBase64String(oByte)
                sDocExt = "PDF"


                If (File.Exists(sPDFfileName)) Then
                    File.Delete(sPDFfileName)
                End If



            Else
                Dim objword As New clsWordDocument
                oByte = objword.ConvertFiletoBinary(objEFaxSettings.EFax_Faxfilepath)
                strData = Convert.ToBase64String(oByte)
                objword = Nothing
                sDocExt = "docx"



            End If

            oByte = Nothing 'Change made to solve memory Leak and word crash issue
            'Add the FAX Details in the Database


            nFaxID = objFAX.AddPendingFAX1(objEFaxSettings.PatientID, objEFaxSettings.EFax_FaxRecipientName, gstrFAXType, objEFaxSettings.EFax_FaxRecipientNumber, objEFaxSettings.EFax_FromName, "", Now, strData, sDocExt, CurrentSendingFAXPriority)



        Catch ex As Exception
            Throw New ClsFaxException(ex.ToString)
        Finally
            If Not IsNothing(myLoadWord) Then
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If

            oCurDoc = Nothing

        End Try

        objFAX.Dispose()    'Change made to solve memory Leak and word crash issue
        objFAX = Nothing
        UpdateLogForFax("END objFAX.AddPendingFAX For Single Recipent")
        Return Nothing
    End Function
    Public Function IFAXDocument(ByVal sfilename As String, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True) As Boolean
        Try
            UpdateLogForFax("In IfaxDocument")

            Dim objEFaxSettings As New clsEFaxSettings

            objEFaxSettings.FaxID = 0
            objEFaxSettings.PatientID = nPatientID
            objEFaxSettings.EFax_UserID = gstrEFaxUserID
            objEFaxSettings.EFax_UserPassword = gstrEFaxUserPassword
            objEFaxSettings.EFax_Tiff_image_flag = "false"
            objEFaxSettings.EFax_Resolution = "high"
            objEFaxSettings.EFax_FaxRecipientName = sFAXTo
            objEFaxSettings.EFax_FaxRecipientNumber = sFAXNo
            objEFaxSettings.EFax_FromName = gstrLoginName
            objEFaxSettings.EFax_Faxfilepath = sfilename
            objEFaxSettings.EFax_FaxCoverpagefilepath = ""
            objEFaxSettings.EFax_FaxCoverPage = False
            objEFaxSettings.EFax_DocumentExtension = "docx"
            objEFaxSettings.EFax_DocumentEncodingType = "base64"
            objEFaxSettings.EFax_DocumentContentType = "application/msword"
            objEFaxSettings.EFax_BillingCode = ""

            EFAXDocumentRevised(objEFaxSettings) 'Call Changes at the time of Memory Leak and Word Crash Issue

            UpdateLogForFax("In IfaxDocument")
            objEFaxSettings.Dispose()  ''slr dispose it
            objEFaxSettings = Nothing   'Change made to solve memory Leak and word crash issue

        Catch ex As ClsFaxException
            Throw ex
        Catch ex As Exception
            Throw New ClsFaxException(ex.ToString)
        End Try
        Return Nothing
    End Function

#End Region

#Region "Private functions"
    '' GLO2011-0014004 : Footers are missing in the Notes that are submitted to the insurance carriers
    '' Function name chenged from GetPatientName to GetPatientDetails and DOB added with Patient name in column
    Private Shared iLast_PatientId As Int64 = 0
    Private Shared strLastGetPatientDetails As String = ""
    Public Function GetPatientDetails(ByVal m_PatientId As Int64) As String
        If (m_PatientId = iLast_PatientId) Then
            Return strLastGetPatientDetails
        Else
            iLast_PatientId = m_PatientId
            'Dim strLastGetPatientDetails As String = ""
            Dim oDB As New DataBaseLayer
            Try

                'Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ' , DOB : ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId

                Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ', DOB: ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId

                strLastGetPatientDetails = oDB.GetRecord_Query(strSQL)
                If IsNothing(strLastGetPatientDetails) Then
                    strLastGetPatientDetails = ""
                End If
            Catch ex As Exception
                strLastGetPatientDetails = ""
            Finally
                oDB.Dispose()   'Change made to solve memory Leak and word crash issue
                oDB = Nothing
            End Try
            Return strLastGetPatientDetails
        End If
    End Function
    Public Sub InsertNamePageNo(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        Try
            If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
            oCurDoc.Activate()

            Try
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter
            Catch ex As Exception
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageFooter
            End Try

            oCurDoc.Application.Selection.Select()
            If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                oCurDoc.Application.Selection.HeaderFooter.Range.Select()

            End If

            Dim strFolderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Document Building Blocks\1033"
            Dim strtxt As String = ""

            If Directory.Exists(strFolderPath & "\14") Then 'Office 2010
                strtxt = strFolderPath & "\14\Built-In Building Blocks.dotx"
            ElseIf Directory.Exists(strFolderPath & "\15") Then 'Office 2013
                strtxt = strFolderPath & "\15\Built-In Building Blocks.dotx"
            Else 'Office 2007
                strtxt = strFolderPath & "\Building Blocks.dotx"
            End If

            'Dim strtxt As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            'strtxt &= "\Microsoft\Document Building Blocks\1033\15\Built-In Building Blocks.dotx"
            ''strtxt &= "\Microsoft\Document Building Blocks\1033\Building Blocks.dotx"

            If File.Exists(strtxt) Then
                oCurDoc.AttachedTemplate = strtxt
                If strtxt.Contains("14") = False AndAlso strtxt.Contains("15") = False Then
                    oCurDoc.XMLSchemaReferences.AutomaticValidation = True
                    oCurDoc.XMLSchemaReferences.AllowSaveAsXMLWithoutValidation = False
                End If
            End If
            If File.Exists(strtxt) Then
                Dim attribute As System.IO.FileAttributes
                attribute = File.GetAttributes(strtxt)
                If attribute <> FileAttributes.ReadOnly Then
                    attribute = FileAttributes.ReadOnly
                    File.SetAttributes(strtxt, attribute)
                End If
            End If
            For Each objTemp As Wd.Template In oCurDoc.Application.Templates
                If objTemp.Name = "Building Blocks.dotx" Or objTemp.Name = "Built-In Building Blocks.dotx" Then
                    objTemp.BuildingBlockEntries.Item("Bold Numbers 3").Insert(Where:=oCurDoc.Application.Selection.HeaderFooter.Range, RichText:=True)
                End If
            Next
            If sName <> "" Then
                oCurDoc.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft
                oCurDoc.Application.Selection.HeaderFooter.Range.InsertBefore(sName & vbTab & vbTab)
                oCurDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                oCurDoc.Application.Selection.TypeBackspace()
            End If

        Catch ex As Exception

        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Sub

    Public Sub InsertPageFooterWithoutMSWBuildingBlock(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        Dim strTrimmedName As String = sName.Trim()
        Try
            For Each oSection As Wd.Section In oCurDoc.Sections

                If oSection.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                    oSection.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
                End If

                Try
                    oSection.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter
                Catch ex As Exception
                    oSection.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekCurrentPageFooter
                End Try

                oSection.Application.Selection.HeaderFooter.Range.Delete()
                oSection.Application.Selection.HeaderFooter.Range.Font.Name = "Arial"

                oSection.Application.Selection.HeaderFooter.Range.Font.Size = 8
                oSection.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft

                oSection.Application.Selection.Range.Text = String.Empty

                oSection.Application.Selection.TypeText("Page ")

                Dim CurrentPage = Wd.WdFieldType.wdFieldPage

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, CurrentPage)

                oSection.Application.ActiveWindow.Selection.TypeText(" of ")

                Dim TotalPages = Wd.WdFieldType.wdFieldNumPages

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, TotalPages)

                If Not String.IsNullOrEmpty(strTrimmedName) Then
                    oSection.Application.Selection.HeaderFooter.Range.InsertBefore(strTrimmedName & vbTab & vbTab)
                End If

            Next

        Catch ex As Exception

        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Sub

    '''' <summary>
    '''' To Clean up the Document for removing FormFields and Tags that does n't contain data
    '''' </summary>
    '''' <remarks></remarks>
    Public Sub CleanupDoc(ByRef oCUDoc As Wd.Document)

        'To replcae fields
        'With oCUDoc.Application
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

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCUDoc.Application, FindText:="|*|", ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=True, MatchWholeWord:=False)
        Catch ex2 As Exception

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
            ''oCUDoc.Application.Selection.Find.ClearFormatting()
            ''oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
            'Try


            '    With oCUDoc.Application.Selection.Find
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

            '    oCUDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
            'Catch ex As Exception
            Try

                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCUDoc.Application, FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
            Catch ex2 As Exception

            End Try
            'End Try
        Next
        'oCUDoc.Application.Selection.Find.ClearFormatting()
        'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
        col_Tags.Clear()
        col_Tags = Nothing

        'With oCUDoc.Application.Selection.Find
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

        'oCUDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'oCUDoc.Application.Selection.Find.ClearFormatting()
        'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With oCUDoc.Application.Selection.Find
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

        'oCUDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'oCUDoc.Application.Selection.Find.ClearFormatting()
        'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With oCUDoc.Application.Selection.Find
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

        'oCUDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'oCUDoc.Application.Selection.Find.ClearFormatting()
        'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
        'With oCUDoc.Application.Selection.Find
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
        'oCUDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        If gblnWordColorHighlight = True Then
            '' SUDHIR 20100105 '' TO REMOVE HIGHLIGHTED COLOR OF FORMFIELDS ''
            For Each aField As Wd.FormField In oCUDoc.FormFields
                Try
                    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
                Catch ex As Exception

                End Try

            Next
        End If
        'For Each cntCtrl As Wd.ContentControl In oCUDoc.ContentControls
        '    If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
        '        cntCtrl.Delete(False)
        '    End If
        'Next
        For iCtrl As Integer = oCUDoc.ContentControls.Count To 1 Step -1
            Try
                Dim cntCtrl As Wd.ContentControl = oCUDoc.ContentControls(iCtrl)

                If cntCtrl.Type = Wd.WdContentControlType.wdContentControlDropdownList Then
                    cntCtrl.Delete(False)
                End If
            Catch ex As Exception

            End Try
        Next

    End Sub

    Public Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        'Dim oRandom As New Random 'For Random no. generation

        strTIFFFileName = gnClientMachineID & "-" & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") 'Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond & oRandom.Next(1, 9).ToString()
        'oRandom = Nothing
        '_dtCurrentDateTime = Nothing    'Change made to solve memory Leak and word crash issue
        UpdateVoiceLog("In retrieve fax document name")
        Return strTIFFFileName
    End Function
    Public Function Get_FAXCoverPageData(ByVal PatientID As Long, ByVal strFields As String, ByVal nVisitID As Long, Optional ByVal nEXAMID As Long = 0, Optional ByVal nReferralID As Long = 0) As String
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim filecnt As Int16
        Dim strDataCol As String
        Dim objCmd As New SqlCommand
        '//      Dim objCon As New SqlConnection
        Dim objSQLDataReader As SqlDataReader = Nothing
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdata_FAX"
        objCmd.Parameters.Clear()
        'objCon.ConnectionString = GetConnectionString()
        'objCon.Open()

        strData = ""
        If strFields <> "" Then
            If InStr(strFields, "Narration") Or InStr(strFields, "FlowSheet") Or InStr(strFields, "imgSignature") Or InStr(strFields, "imgClinicLogo") Then
                If InStr(strFields, "SingleRow") Then
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                Else
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = strFields
                End If

                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nExamID", nEXAMID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nEXAMID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                sqlParam = objCmd.Parameters.AddWithValue("@nReferralId", nReferralID)
                sqlParam.Direction = ParameterDirection.Input

                objCmd.Connection = objCon

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            Dim strFileName As String
                            If objSQLDataReader.Item(1) = "2" Then
                                filecnt = filecnt + 1
                                If InStr(strFields, "Narration") Then
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.Txt"
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Flowsheet" & filecnt & ".Txt"
                                End If
                            Else
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "image.bmp"
                            End If
                            strData = strFileName
                            '''''
                            'Save contents in file
                            Dim mstream As ADODB.Stream
                            mstream = New ADODB.Stream
                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                            mstream.Open()
                            'Check if there are records for selected Node
                            mstream.Write(objSQLDataReader.Item(0))
                            If System.IO.File.Exists(strFileName) Then
                                System.IO.File.Delete(strFileName)
                            End If
                            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                            mstream.Close()

                            If Not IsNothing(mstream) Then
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(mstream)  ''slr free it
                            End If



                            mstream = Nothing   'Change made to solve memory Leak and word crash issue
                            If InStr(strFields, "SingleRow") Then
                                ' Dim oFile As System.IO.File
                                Dim oRead As System.IO.StreamReader
                                Dim LineIn As String
                                Dim strNewString As New ArrayList
                                oRead = File.OpenText(strFileName)

                                While oRead.Peek <> -1
                                    LineIn = oRead.ReadLine()
                                    strNewString.Add(LineIn)
                                End While
                                oRead.Close()
                                oRead.Dispose()  ''slr dispose it
                                oRead = Nothing 'Change made to solve memory Leak and word crash issue
                                Dim oWrite As System.IO.StreamWriter
                                oWrite = File.CreateText(strFileName)
                                oWrite.WriteLine(strNewString.Item(0))

                                If strNewString.Count > 1 Then
                                    Dim nLoop As Int16
                                    For nLoop = strNewString.Count - 1 To 0 Step -1
                                        If Trim(strNewString.Item(nLoop)) <> "" Then
                                            Exit For
                                        End If
                                    Next
                                    oWrite.WriteLine(strNewString.Item(nLoop))
                                End If
                                oWrite.Close()
                                oWrite.Dispose()  ''slr freeit 
                                oWrite = Nothing 'Change made to solve memory Leak and word crash issue
                                strNewString = Nothing 'Change made to solve memory Leak and word crash issue
                            End If
                            '''''
                            flagOthers = objSQLDataReader.Item(1)
                        End If
                    End While
                End If
                objSQLDataReader.Close()

                objSQLDataReader = Nothing  ''slr freeit 
            ElseIf strFields.StartsWith("FAX") Then
            ElseIf Left(strFields, 6) <> "Others" Then

                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 500)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = strFields

                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nExamID", nEXAMID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nEXAMID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                sqlParam = objCmd.Parameters.AddWithValue("@nReferralId", nReferralID)
                sqlParam.Direction = ParameterDirection.Input
                objCmd.Connection = objCon

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            If strData = Nothing Then
                                strData = objSQLDataReader.Item(0)
                                flagOthers = objSQLDataReader.Item(1)
                            Else
                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
                            End If
                        End If
                    End While
                End If
                objSQLDataReader.Close()
                objSQLDataReader = Nothing  ''slr free it
            Else
                flagOthers = 0
                Select Case strFields
                End Select
                If InStr(strFields, "Age") Then
                    Dim objCmd1 As New SqlCommand
                    Dim objSQLDataReader1 As SqlDataReader
                    Dim sqlParam1 As SqlParameter
                    objCmd1.CommandType = CommandType.StoredProcedure
                    objCmd1.CommandText = "gsp_GetDOB"
                    objCmd1.Parameters.Clear()
                    sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam1.Direction = ParameterDirection.Input

                    objCmd1.Connection = objCon

                    objSQLDataReader1 = objCmd1.ExecuteReader

                    If objSQLDataReader1.HasRows = True Then
                        objSQLDataReader1.Read()
                        If IsDBNull(objSQLDataReader1.Item(0)) = False Then
                            dtDOB = objSQLDataReader1.Item(0)
                        End If
                    End If
                    Dim nMonths As Int16
                    nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
                    strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months"

                    objSQLDataReader1.Close()
                    objSQLDataReader1 = Nothing  ''slr free it
                    'Change made to solve memory Leak and word crash issue
                    objCmd1.Parameters.Clear()
                    objCmd1.Dispose()
                    objCmd1 = Nothing
                    sqlParam1 = Nothing
                ElseIf InStr(strFields, "TodayDate") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "DOS") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "Time") Then
                    strData = Format(Now, "Medium Time")
                End If
            End If
        End If
        '''' For Vitals if Field is of BloodPressure (Sitting or Standing(MIN/MAX))
        '''' then take only Integer part of its Value 
        If InStr(strFields, "dBloodPressure") Then
            strData = Split(strData, ".")(0)
        End If
        '''''
        strDataCol = strData & "|" & flagOthers.ToString

        'Change made to solve memory Leak and word crash issue
        If Not objCmd Is Nothing Then

            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        'If Not IsNothing(objCon) Then
        '    If objCon.State = ConnectionState.Open Then
        '        objCon.Close()
        '    End If
        '    'Change made to solve memory Leak and word crash issue
        '    objCon.Dispose()
        '    objCon = Nothing
        'End If

        If Not sqlParam Is Nothing Then
            sqlParam = Nothing
        End If
        Return strDataCol
    End Function
    Public Sub OpenConnection()
        If Not IsNothing(objCon) Then
            objCon.ConnectionString = GetConnectionString()
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

        Else
            objCon = New SqlConnection()
            objCon.ConnectionString = GetConnectionString()
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If
        End If


    End Sub
    Public Sub CloseConnection()
        If Not IsNothing(objCon) Then
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            'Change made to solve memory Leak and word crash issue
            objCon.Dispose()
            objCon = Nothing
        End If
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                _sFileName = Nothing
                _sFAXTo = Nothing
                _sFAXType = Nothing
                _sFAXNo = Nothing
                _sLoginUser = Nothing
                _sDefaultPrinter = Nothing
                _sFAXPrinter = Nothing
                _sErrorMessage = Nothing
                _sFAXTypeDetails = Nothing
                _sPriviousUsedPrinter = Nothing
                CloseConnection()  ''slr make nothing 
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Namespace PrintAndFaxWord

    Public Delegate Sub FaxThisModule(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)

    Public Class ClsPrintOrFax
        <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
        Private Shared Function GetActiveWindow() As IntPtr
        End Function
        Private Shared DefaultPrintDocument As New System.Drawing.Printing.PrintDocument()

        Public Shared Sub PrintOrFaxWordDocument(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal sFileName As String, ByVal bIsPrintFlag As Boolean, ByVal m_patientID As Long, ByVal fFaxFunction As FaxThisModule, ByVal totalPages As Integer, Optional ByVal blnShowPrinterDialog As Boolean = True, Optional ByRef wdDoc As Wd.Document = Nothing, Optional ByVal PageNo As Integer = 0, Optional ByRef blnPrintCancel As Boolean = False, Optional ByRef _PreviousUsedPrinter As String = "", Optional ByVal UseDirectFaxName As Boolean = False, Optional iOwner As IWin32Window = Nothing)

            Dim oTempDoc As Wd.Document = Nothing
            Dim Opened As Boolean = False
            Dim sFileNameForPrintOrFax As String = ""
            Dim Miss As Object = System.Reflection.Missing.Value
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages

            If (UseDirectFaxName) Then
                sFileNameForPrintOrFax = sFileName
                If Not File.Exists(sFileNameForPrintOrFax) Then
                    MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else

                sFileNameForPrintOrFax = ExamNewDocumentName()

                Try
                    File.Copy(sFileName, sFileNameForPrintOrFax)
                Catch ex As Exception

                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Copy file before printing. Source path:= '" & sFileName & "' ; Destination Path :='" & sFileNameForPrintOrFax & "'" & " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try

                If Not File.Exists(sFileNameForPrintOrFax) Then
                    MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Try
                    If (totalPages = 0) Then
                        oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax)
                        oTempDoc.Application.Visible = False
                        totalPages = oTempDoc.ComputeStatistics(PageCountStat, Miss) ''added for bugid 96435
                        myLoadWord.CloseWordOnly(oTempDoc)
                    End If
                Catch ex As Exception

                End Try


            End If

            If bIsPrintFlag Then

                Dim strpatname As String = ""
                If gblnPageNo = True Then

                    strpatname = GetPatientDetails(m_patientID)

                End If
                Dim obj As New gloWord.clsPrintWordQueue
                obj.FilePath = sFileNameForPrintOrFax

                Try


                    Using oDialog As New gloPrintDialog.gloPrintDialog(True)
                        Dim strOldPrinterName As String = String.Empty
                        oDialog.ConnectionString = GetConnectionString()

                        oDialog.TopMost = True
                        oDialog.ShowPrinterProfileDialog = True
                        oDialog.ModuleName = "WordPrinting"

                        oDialog.RegistryModuleName = "WordPrinting"

                        If oDialog IsNot Nothing Then
                            'Dim printDocument1 As New System.Drawing.Printing.PrintDocument()
                            If (Not gloGlobal.gloTSPrint.isCopyPrint) Then


                                oDialog.PrinterSettings = DefaultPrintDocument.PrinterSettings
                                oDialog.AllowSomePages = True


                                oDialog.PrinterSettings.ToPage = totalPages

                                oDialog.PrinterSettings.FromPage = 1
                                oDialog.PrinterSettings.MaximumPage = totalPages

                                oDialog.PrinterSettings.MinimumPage = 1


                                Try
                                    strOldPrinterName = oDialog.PrinterSettings.PrinterName
                                Catch ex As Exception

                                End Try

                            End If
                            oDialog.bEnableLocalPrinter = gblnEnableLocalPrinter
                            If oDialog.ShowDialog(iOwner) = System.Windows.Forms.DialogResult.OK Then

                                If (oDialog.bUseDefaultPrinter = True) Then
                                    oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                    oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
                                End If
                                If gloGlobal.gloTSPrint.isCopyPrint AndAlso (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) = False) Then
                                    oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                    oDialog.CustomPrinterExtendedSettings.IsShowProgress = False
                                End If

                                Dim objogloPrintProgressController As New gloWord.frmgloPrintQueueController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings)
                                objogloPrintProgressController.gblnPageNo = gblnPageNo
                                objogloPrintProgressController.strpatname = strpatname
                                objogloPrintProgressController.oldPrinterName = strOldPrinterName
                                objogloPrintProgressController.lstgloTemplate.Add(obj)
                                objogloPrintProgressController.lnPatientId = m_patientID

                                If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                                    If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then



                                        objogloPrintProgressController.Show()
                                        objogloPrintProgressController.BringToFront()
                                    Else

                                        objogloPrintProgressController.Show()
                                        objogloPrintProgressController.BringToFront()
                                    End If
                                Else

                                    Dim myCtrl As Form = Nothing
                                    Try
                                        Dim handle As IntPtr = GetActiveWindow()
                                        myCtrl = Control.FromHandle(handle)
                                    Catch ex As Exception
                                        ex = Nothing
                                    End Try


                                    objogloPrintProgressController.TopMost = True
                                    objogloPrintProgressController.ShowInTaskbar = False

                                    If Not IsNothing(myCtrl) Then
                                        objogloPrintProgressController.ShowDialog(myCtrl)
                                    Else
                                        objogloPrintProgressController.ShowDialog()
                                    End If
                                    If objogloPrintProgressController IsNot Nothing Then
                                        objogloPrintProgressController.Dispose()
                                    End If
                                    objogloPrintProgressController = Nothing


                                End If
                                'if

                            End If
                        Else
                            Dim _ErrorMessage As String = "Error in Showing Print Dialog"

                            If _ErrorMessage.Trim() <> "" Then
                                Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                                _MessageString = ""
                            End If


                            MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        End If

                    End Using


                Catch ex As Exception


                    Dim _ErrorMessage As String = ex.ToString()

                    If _ErrorMessage.Trim() <> "" Then
                        Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                        _MessageString = ""
                    End If



                    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    ex = Nothing



                End Try


            Else
                Call fFaxFunction(myLoadWord, sFileNameForPrintOrFax)
            End If

        End Sub
        ''added for PrintAllOrFaxWordDocument function
        Private Shared oDialogAll As gloPrintDialog.gloPrintDialog = Nothing
        Private Shared oDialogAllPrinterName As String = String.Empty
        Private Shared strAllOldPrinterName As String = String.Empty
        Private Shared blnAllIsCancel As Boolean = False
        Public Shared WriteOnly Property SetAllFileDialog() As Boolean

            Set(value As Boolean)
                oDialogAll = Nothing
                oDialogAllPrinterName = String.Empty
                blnAllIsCancel = False
                If strAllOldPrinterName <> [String].Empty Then
                    gloGlobal.gloTSPrint.SetDefaultPrinterSettings(strAllOldPrinterName)
                    Application.DoEvents()

                End If
                strAllOldPrinterName = String.Empty
            End Set
        End Property
        ''added for bugid 104407 Null Exception After Click On Save&Cls Button
        Public Shared Sub PrintAllorFaxGalleryDocument(ByVal ArrLst As ArrayList, ByVal m_PatientID As Int64, ByVal m_VisitID As Int64, ByVal _PreviousUsedPrinter As String)
            '  blnPrintCancel = False
            Dim strFileName As String = String.Empty
            Dim objCriteria As DocCriteria
            Dim ObjWord As clsWordDocument
            If ArrLst.Count > 0 Then

                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim oTempDoc As Wd.Document = Nothing
                Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
                Dim Missing As Object = System.Reflection.Missing.Value

                Try
                    'tlsFormGallery.MyToolStrip.Items("PrintAll").Enabled = False
                    For i As Integer = 0 To ArrLst.Count - 1
                        ' ObjWord = New clsWordDocument
                        Dim lst As myList
                        lst = CType(ArrLst.Item(i), myList)

                        'txtTemplateName.Text = lst.Description
                        '  strFileName = ExamNewDocumentName '' Application.StartupPath & "\Temp\Temp9.doc"
                        strFileName = gloWord.LoadAndCloseWord.ConvertFileFromBinary(lst.TemplateResult, gloSettings.FolderSettings.AppTempFolderPath) 'ObjWord.GenerateFile(lst.TemplateResult, strFileName)
                        'ObjWord = Nothing
                        If strFileName <> "" Then
                            ObjWord = New clsWordDocument
                            objCriteria = New DocCriteria
                            objCriteria.DocCategory = enumDocCategory.Others  ''added for bugid 87030
                            objCriteria.PatientID = m_PatientID
                            objCriteria.VisitID = m_VisitID
                            objCriteria.PrimaryID = 0
                            ObjWord.DocumentCriteria = objCriteria

                            oTempDoc = myLoadWord.LoadWordApplication(strFileName)

                            ObjWord.CurDocument = oTempDoc
                            ObjWord.GetFormFieldData(enumDocType.None)
                            oTempDoc = ObjWord.CurDocument
                            objCriteria.Dispose()
                            objCriteria = Nothing

                            ObjWord = Nothing
                            Dim myWordFileName As String = myLoadWord.SaveCurrentWord(oTempDoc, gloSettings.FolderSettings.AppTempFolderPath)


                            PrintAndFaxWord.ClsPrintOrFax.PrintAllOrFaxWordDocument(myLoadWord, myWordFileName, True, m_PatientID, Nothing, 0, Not CType(i, Boolean), oTempDoc, blnPrintCancel:=False, _PreviousUsedPrinter:=_PreviousUsedPrinter, PrintDocno:=i)



                            myLoadWord.CloseWordOnly(oTempDoc)


                        End If

                        '  If (blnPrintCancel = True) Then
                        'Exit For
                        'End If
                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.PrintAll, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                Finally
                    ''added property for bugid 96982,96984
                    PrintAndFaxWord.ClsPrintOrFax.SetAllFileDialog = Nothing
                    '  tlsFormGallery.MyToolStrip.Items("PrintAll").Enabled = True
                End Try


                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.Print, "Patient Form Gallery Document Printed", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
            ArrLst.Clear()
        End Sub


        ''function added for bugid  96982,96984 for printing all formgallery documents
        Public Shared Sub PrintAllOrFaxWordDocument(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal sFileName As String, ByVal bIsPrintFlag As Boolean, ByVal m_patientID As Long, ByVal fFaxFunction As FaxThisModule, ByVal totalPages As Integer, Optional ByVal blnShowPrinterDialog As Boolean = True, Optional ByRef wdDoc As Wd.Document = Nothing, Optional ByVal PageNo As Integer = 0, Optional ByRef blnPrintCancel As Boolean = False, Optional ByRef _PreviousUsedPrinter As String = "", Optional ByVal UseDirectFaxName As Boolean = False, Optional iOwner As IWin32Window = Nothing, Optional ByVal PrintDocno As Integer = 0)
            If (blnAllIsCancel = False) Then
                Dim oTempDoc As Wd.Document = Nothing
                Dim Opened As Boolean = False
                Dim sFileNameForPrintOrFax As String = ""
                Dim Missing As Object = System.Reflection.Missing.Value
                Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages

                If (UseDirectFaxName) Then
                    sFileNameForPrintOrFax = sFileName
                    If Not File.Exists(sFileNameForPrintOrFax) Then
                        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                Else

                    sFileNameForPrintOrFax = ExamNewDocumentName()

                    Try
                        File.Copy(sFileName, sFileNameForPrintOrFax)
                    Catch ex As Exception

                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Copy file before printing. Source path:= '" & sFileName & "' ; Destination Path :='" & sFileNameForPrintOrFax & "'" & " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                    If Not File.Exists(sFileNameForPrintOrFax) Then
                        MessageBox.Show("Error while printing or faxing. Please try again.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    If (totalPages = 0) Then
                        Try
                            oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax)
                            oTempDoc.Application.Visible = False
                            totalPages = oTempDoc.ComputeStatistics(PageCountStat, Missing) ''added for bugid 96435
                            myLoadWord.CloseWordOnly(oTempDoc)
                        Catch ex As Exception

                        End Try

                    End If
                    End If

                    If bIsPrintFlag Then

                        Dim strpatname As String = ""
                        If gblnPageNo = True Then

                            strpatname = GetPatientDetails(m_patientID)

                        End If
                        Dim obj As New gloWord.clsPrintWordQueue
                        obj.FilePath = sFileNameForPrintOrFax

                        Try


                            ''  Using oDialog As New gloPrintDialog.gloPrintDialog(True)
                            If oDialogAll Is Nothing Then
                                oDialogAll = New gloPrintDialog.gloPrintDialog(True)
                            End If
                            ' Dim strOldPrinterName As String = String.Empty
                            oDialogAll.ConnectionString = GetConnectionString()

                            oDialogAll.TopMost = True
                            oDialogAll.ShowPrinterProfileDialog = True
                            oDialogAll.ModuleName = "WordPrinting"

                            oDialogAll.RegistryModuleName = "WordPrinting"

                            If oDialogAll IsNot Nothing Then
                                'Dim printDocument1 As New System.Drawing.Printing.PrintDocument()
                            If (Not gloGlobal.gloTSPrint.isCopyPrint) Then


                                oDialogAll.AllowSomePages = True
                                If (PrintDocno = 0) Then
                                    oDialogAll.PrinterSettings = DefaultPrintDocument.PrinterSettings
                                End If

                                oDialogAll.PrinterSettings.ToPage = totalPages

                                oDialogAll.PrinterSettings.FromPage = 1
                                oDialogAll.PrinterSettings.MaximumPage = totalPages

                                oDialogAll.PrinterSettings.MinimumPage = 1
                            End If
                            oDialogAll.bEnableLocalPrinter = gblnEnableLocalPrinter



                            If (PrintDocno = 0) Then
                                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then


                                    Try

                                        strAllOldPrinterName = oDialogAll.PrinterSettings.PrinterName
                                    Catch ex As Exception

                                    End Try
                                End If
                                If oDialogAll.ShowDialog(iOwner) = System.Windows.Forms.DialogResult.OK Then

                                    If (oDialogAll.bUseDefaultPrinter = True) Then
                                        oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                        oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = True
                                    End If
                                    If gloGlobal.gloTSPrint.isCopyPrint AndAlso (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) = False) Then
                                        oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                        oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = False
                                    End If
                                    If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                                        oDialogAllPrinterName = oDialogAll.PrinterSettings.PrinterName
                                        If oDialogAllPrinterName <> strAllOldPrinterName Then
                                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oDialogAllPrinterName)
                                            Application.DoEvents()
                                        End If
                                    End If
                                    Dim objogloPrintProgressController As New gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings)
                                    objogloPrintProgressController.gblnPageNo = gblnPageNo
                                    objogloPrintProgressController.strpatname = strpatname
                                    objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName
                                    objogloPrintProgressController.lstgloTemplate.Add(obj)
                                    objogloPrintProgressController.lnPatientId = m_patientID

                                    If oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                                        If oDialogAll.CustomPrinterExtendedSettings.IsShowProgress Then
                                            objogloPrintProgressController.Show()
                                        Else
                                            objogloPrintProgressController.Show()
                                        End If
                                    Else
                                        Dim myCtrl As Form = Nothing
                                        Try
                                            Dim handle As IntPtr = GetActiveWindow()
                                            myCtrl = Control.FromHandle(handle)
                                        Catch ex As Exception
                                            ex = Nothing
                                        End Try


                                        objogloPrintProgressController.TopMost = True
                                        objogloPrintProgressController.ShowInTaskbar = False

                                        If Not IsNothing(myCtrl) Then
                                            objogloPrintProgressController.ShowDialog(myCtrl)
                                        Else
                                            objogloPrintProgressController.ShowDialog()
                                        End If
                                        If objogloPrintProgressController IsNot Nothing Then
                                            objogloPrintProgressController.Dispose()
                                        End If
                                        objogloPrintProgressController = Nothing


                                    End If
                                    'if
                                Else
                                    blnAllIsCancel = True

                                End If

                            Else
                                If (oDialogAll.bUseDefaultPrinter = True) Then
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = True
                                End If
                                If gloGlobal.gloTSPrint.isCopyPrint AndAlso (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) = False) Then
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = False
                                End If
                                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                                    If (oDialogAllPrinterName <> String.Empty) Then
                                        oDialogAll.PrinterSettings.PrinterName = oDialogAllPrinterName
                                    End If
                                End If
                                Dim objogloPrintProgressController As New gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings)
                                objogloPrintProgressController.gblnPageNo = gblnPageNo
                                objogloPrintProgressController.strpatname = strpatname
                                objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName
                                objogloPrintProgressController.lstgloTemplate.Add(obj)
                                objogloPrintProgressController.lnPatientId = m_patientID

                                If oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                                    If oDialogAll.CustomPrinterExtendedSettings.IsShowProgress Then
                                        objogloPrintProgressController.Show()
                                    Else
                                        objogloPrintProgressController.Show()
                                    End If
                                Else
                                    Dim myCtrl As Form = Nothing
                                    Try
                                        Dim handle As IntPtr = GetActiveWindow()
                                        myCtrl = Control.FromHandle(handle)
                                    Catch ex As Exception
                                        ex = Nothing
                                    End Try


                                    objogloPrintProgressController.TopMost = True
                                    objogloPrintProgressController.ShowInTaskbar = False

                                    If Not IsNothing(myCtrl) Then
                                        objogloPrintProgressController.ShowDialog(myCtrl)
                                    Else
                                        objogloPrintProgressController.ShowDialog()
                                    End If
                                    If objogloPrintProgressController IsNot Nothing Then
                                        objogloPrintProgressController.Dispose()
                                    End If
                                    objogloPrintProgressController = Nothing


                                End If
                                'if
                            End If
                            '' End Using
                        Else ''oDialogAll IsNot Nothing 
                            Dim _ErrorMessage As String = "Error in Showing Print Dialog"

                            If _ErrorMessage.Trim() <> "" Then
                                Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                                _MessageString = ""
                            End If


                            MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        End If

                    Catch ex As Exception


                        Dim _ErrorMessage As String = ex.ToString()

                        If _ErrorMessage.Trim() <> "" Then
                            Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                            _MessageString = ""
                        End If



                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                        ex = Nothing



                    End Try


                    Else
                        Call fFaxFunction(myLoadWord, sFileNameForPrintOrFax)
                    End If
            End If

        End Sub

        Public Shared Function GetPatientDetails(ByVal m_PatientId As Int64) As String
            Dim strLastGetPatientDetails As String = ""
            Dim oDB As New DataBaseLayer
            Try
                'Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ' , DOB : ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId

                Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ', DOB: ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId

                strLastGetPatientDetails = oDB.GetRecord_Query(strSQL)
                If IsNothing(strLastGetPatientDetails) Then
                    strLastGetPatientDetails = ""
                End If
            Catch ex As Exception
                strLastGetPatientDetails = ""
            Finally
                oDB.Dispose()   'Change made to solve memory Leak and word crash issue
                oDB = Nothing
            End Try
            Return strLastGetPatientDetails

        End Function
    End Class
End Namespace

Namespace SendWord
    Module MdlSendWord
        Public Function SendWordDocument(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal sFileName As String, ByVal m_patientID As Long, Optional ByRef wdDoc As Wd.Document = Nothing) As String
            Dim oTempDoc As Wd.Document = Nothing
            Dim notOpened As Boolean = False
            If (IsNothing(wdDoc)) Then
                Dim sFileNameForPrintOrFax As String = ExamNewDocumentName()
                Try
                    File.Copy(sFileName, sFileNameForPrintOrFax)
                Catch ex As Exception
                    'UpdateLog("Unable to Copy file before sending outside. Source path:= '" & sFileName & "' ; Destination Path :='" & sFileNameForPrintOrFax & "'" & " Exception: " + ex.ToString())
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Copy file before sending outside. Source path:= '" & sFileName & "' ; Destination Path :='" & sFileNameForPrintOrFax & "'" & " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return ""
                End Try
                oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax)
            Else

                notOpened = True
                oTempDoc = wdDoc
            End If
            If (IsNothing(oTempDoc) = False) Then

                Try

                    'If bIsPrintFlag Then
                    Dim oSendDoc As New clsPrintFAX
                    Try
                        Return oSendDoc.SendDoc(oTempDoc, m_patientID)
                    Catch ex As Exception
                        'UpdateLog("Error preparing document to send outside in SendDoc call. Error:= " & ex.ToString())
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Error preparing document to send outside in SendDoc call. Error:= " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        Return ""
                    Finally
                        If Not IsNothing(oSendDoc) Then
                            oSendDoc.Dispose()
                            oSendDoc = Nothing
                        End If
                    End Try

                Catch
                    UpdateLog("Error Creating clsPrintFax Object.")
                    Return ""
                Finally
                    If Not IsNothing(oTempDoc) Then
                        If (notOpened = False) Then
                            myLoadWord.CloseWordOnly(oTempDoc)
                        End If
                    End If

                End Try
            End If
            Return ""
        End Function
    End Module
End Namespace
