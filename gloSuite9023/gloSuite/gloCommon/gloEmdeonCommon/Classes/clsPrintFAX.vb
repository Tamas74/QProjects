Imports System.IO
Imports System.Data.SqlClient
Imports Wd = Microsoft.Office.Interop.Word
Imports gloUserControlLibrary
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEmdeonCommon
Imports gloEmdeonCommon.gloEMRWord
Imports System.Windows.Forms
Imports gloEMRReports
Imports Microsoft.Win32
Imports gloSettings
Imports System.Management
Imports pdftron.PDF


Public Class clsPrintFAX

    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Public InsertCoverPageFirstForFaxWithCoverPage As Boolean = 1

    'sarika internet fax
    Public Enum SendMethod
        sPrint = 1
        sFax = 2
        sEFax = 3
    End Enum
    'sarika internet fax
    'sarika code optimization for speed 20080930
    Public Shared IsBlackIceSettingsSet As Boolean = False
    Shared AxBlackIceDEVMODE1 As AxBLACKICEDEVMODELib.AxBlackIceDEVMODE
    '------


#Region "Private Variables"
    'sarika internet fax


    ' Private _objWdContainer As AxDSOFramer.AxFramerControl

    'sarika internet fax

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

    Dim _sDefaultPrinter As String      ' System Default Printer
    Dim _sFAXPrinter As String          ' FAX Printer

    Dim _sErrorMessage As String            ' Error Message
    Dim objWord As clsWordDocument
    Dim oTempWordApp As Wd.Application

    'sarika internet fax
    Dim _sFAXTypeDetails As String = ""
    'sarika internet fax
    'variable added by dipak 20090825 for store value of previously used printer
    Dim _sPriviousUsedPrinter As String = ""
    'end add dipak

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

    Dim _blnFaxSentfromOrders As Boolean = False

    Public Property blnFaxSentfromOrders() As Boolean
        Get
            Return _blnFaxSentfromOrders
        End Get
        Set(ByVal value As Boolean)
            _blnFaxSentfromOrders = value
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
        If Not gloGlobal.gloTSPrint.isCopyPrint Then
            Dim objPrintDocument As New System.Drawing.Printing.PrintDocument
            _sDefaultPrinter = objPrintDocument.PrinterSettings.PrinterName
            objPrintDocument.Dispose()
            objPrintDocument = Nothing
        End If
        gstrMessageBoxCaption = GetMessageBoxCaption()
        getClientID()
    End Sub
    Public Sub New(ByVal strFAXPrinter As String)
        _sFAXPrinter = strFAXPrinter
        'Retrieve the Default Printer
        If Not gloGlobal.gloTSPrint.isCopyPrint Then
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




    'modified by dipak 20090821
    'modification in PrintDoc function of optional argument  "Optional ByVal blnShowPrinterDialog As Boolean = True" for solving the problem in bug  #1541  of multiple Notes are selected, it shows the Print
    'Dialog that many times.
    'modified by dipak 20090825 
    'PintDoc procedure made PrintDoc Function return Print DialogResult
    Public Function PrintDoc(ByVal oPrint As Wd.Document, Optional ByVal m_PatientId As Int64 = 0, Optional ByVal blnShowPrinterDialog As Boolean = True)
        Dim oTempDoc As Wd.Document
        oTempDoc = CleanupDoc(oPrint)
        If gblnPageNo = True Then
            oTempDoc = InsertNamePageNo(oTempDoc, GetPatientName(m_PatientId))
        End If

        oTempDoc.Application.ActivePrinter = _sDefaultPrinter
        oTempDoc.PrintPreview()

        If gblnIsDefaultPrinter = False And blnShowPrinterDialog Then
            'code modified by dipak 20090825 for track PintDialogboxResult =Ok/Cancel Click
            '-1 in if statement indiacate compair with value of ok click 
            If (oTempDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).Show() = -1) Then
                _sPriviousUsedPrinter = oTempDoc.Application.ActivePrinter.ToString()
                PrintDoc = DialogResult.OK
            Else
                PrintDoc = DialogResult.Cancel
            End If
            ' oTempDoc.Application.ActivePrinter = oTempDoc.Application.Dialogs(Microsoft.Office.Interop.Word.WdWordDialog.wdDialogFilePrint).

        Else
            'oTempDoc.Application.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)
            PrintDoc = DialogResult.OK
            If (_sPriviousUsedPrinter <> "") Then
                oTempDoc.Application.ActivePrinter = _sPriviousUsedPrinter
            Else
                oTempDoc.Application.ActivePrinter = _sDefaultPrinter
            End If

            'Added by Shweta to Print out of margin page 20090827
            oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
            oTempDoc.Application.Options.PrintBackground = False
            'oTempDoc.PrintFormsData = True '' COMMENT BY SUDHIR 20091023 '' 
            oTempDoc.Application.Options.PrintBackground = False
            oTempDoc.PrintOut(Background:=False)

            ' oTempDoc.PrintOut()
            oTempDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
            System.Threading.Thread.Sleep(1000)
            'end Shweta

        End If
        'end modified
        System.Threading.Thread.Sleep(1000)
    End Function

    Public Function SendDoc(ByVal oSend As Wd.Document, Optional ByVal m_PatientId As Int64 = 0) As String
        Dim oTempDoc As Wd.Document
        Dim strSendFilename As String = String.Empty
        Try
            strSendFilename = ExamNewDocumentName
            oTempDoc = CleanupDoc(oSend)

            If gblnPageNo = True Then
                oTempDoc = InsertNamePageNo(oTempDoc, GetPatientName(m_PatientId))
            End If

            oTempDoc.SaveAs(strSendFilename, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

        Catch ex As Exception
            strSendFilename = ""
            Return strSendFilename
        End Try
        Return strSendFilename
    End Function


    Public Sub GetFaxSettings(ByVal _PatientID As Int64, ByVal LoginName As String)
        'gnPatientID = _PatientID
        gstrLoginName = LoginName
        'Dim regKey As RegistryKey
        'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
        'If IsNothing(regKey.GetValue("FAXPrinterName")) = False Then
        '    gstrFAXPrinterName = regKey.GetValue("FAXPrinterName")
        'End If
        'If IsNothing(regKey.GetValue("FAXOutputDirectory")) = False Then
        '    gstrFAXOutputDirectory = regKey.GetValue("FAXOutputDirectory")
        'End If
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)


        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXPrinterName")) = False Then
            gstrFAXPrinterName = gloRegistrySetting.GetRegistryValue("FAXPrinterName")
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")) = False Then
            gstrFAXOutputDirectory = gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")
        End If
        gloRegistrySetting.CloseRegistryKey()

    End Sub
    Public Sub FaxLabOrder(ByVal OrderID As Long, ByVal arrTests As ArrayList, ByVal PatientID As Long)
        Try
            'Create report object
            ' Dim oLabs As Rpt_LabOrder = Nothing

            'Call create report function to prepare report for sending fax
            'oLabs = CreateReport(OrderID, arrTests, PatientID)

            Dim bnSqlAuthentication As Boolean = Not System.Convert.ToBoolean(appSettings("WindowAuthentication"))
            Dim gstrSQLServerName As String = System.Convert.ToString(appSettings("SQLServerName"))
            Dim gstrDatabaseName As String = System.Convert.ToString(appSettings("DatabaseName"))
            Dim gstrSQLLoginName As String = System.Convert.ToString(appSettings("SQLLoginName"))
            Dim gstrSQLPassword As String = System.Convert.ToString(appSettings("SQLPassword"))

            mdlFAX.gstrFAXContactPerson = ""
            mdlFAX.gstrFAXContactPersonFAXNo = ""
            mdlFAX.multipleRecipients = False
            ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
            ''Change gstrFAXContacts by gstrfaxCollection
            mdlFAX.gstrfaxCollection = Nothing


            'sarika internet fax
            ' code added to read if internet fax
            Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim _strSQL As String = String.Empty
            Dim _objIFax As New Object

            Try
                oDBLayer.Connect(False)
                _strSQL = "select sSettingsValue from Settings where sSettingsName = 'InternetFax'"
                _objIFax = oDBLayer.ExecuteScalar_Query(_strSQL)

                If Not IsNothing(_objIFax) AndAlso System.Convert.ToInt16(_objIFax) = 1 Then
                    gblnInternetFax = True
                Else
                    gblnInternetFax = False
                End If

                oDBLayer.Disconnect()

            Catch ex As Exception
                If Not IsNothing(oDBLayer) Then
                    oDBLayer.Dispose()
                End If
                _objIFax = Nothing
            End Try
            ' code added to read if internet fax is on

            If gblnInternetFax = False Then

                'sarika 12th oct 07
                'Check FAX Printer settings are set or not
                If isPrinterSettingsSet(True) = False Then
                    Exit Sub
                End If
                '--------

                Try
                    gloEmdeonCommon.MainMenu.SetFAXPrinterDefaultSettings1()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                    MessageBox.Show("Error in setting Default Printer settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'Retrieve the FAX Cover Page details
                'Find FAX Parameters
                'Get Pharmacy FAX No
                'Dim strFAXTo As String
                'Dim strFAXNo As String
                'Dim objmytable As mytable
                Dim objFAX As New clsFAX
                'objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

                'If Not IsNothing(objmytable) Then
                '    gstrFAXContactPersonFAXNo = objmytable.Description
                '    gstrFAXContactPerson = objmytable.Code
                'End If

                'If Trim(gstrFAXContactPerson) = "" Then
                '    gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                'End If

                ' If gblnFAXCoverPage Then
                ''mdlFAX.Owner = Me
                If mdlFAX.RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
                    Exit Sub
                Else
                    blnFaxSentfromOrders = True
                End If
                'Else
                'If Trim(gstrFAXContactPersonFAXNo) = "" Then
                '    gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                'End If
                'End If


                'code commented by sarika 13th nov 07 -- for 1 fax to multiple recipients

                'If Trim(gstrFAXContactPersonFAXNo) = "" Then
                '    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    'sarika 3rd oct 07
                '    'the fax is send even then the fax no. is not entered.
                '    Exit Function
                '    '----------------------------------
                'End If

                ''Retrieve FAX Document Name
                'Dim strFAXDocumentName As String
                'strFAXDocumentName = RetrieveFAXDocumentName()

                ''If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Function
                'If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

                'objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                'oRpt.PrintToPrinter(1, False, 0, 0)
                'objFAX = Nothing
                '--------
                


                'code added by sarika 13th nov 07 -- for 1 fax to multiple recipients
                If multipleRecipients = False Then


                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'sarika 3rd oct 07
                        'the fax is send even then the fax no. is not entered.
                        Exit Sub
                        '----------------------------------
                    End If

                    'Retrieve FAX Document Name
                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()
                    If gloEmdeonCommon.MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub
                    objFAX.AddPendingFAX(PatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    '    oLabs.PrintOptions.PrinterName = gstrFAXPrinterName
                    '    oLabs.PrintToPrinter(1, False, 0, 0)

                    Dim clsPrntRpt As gloSSRSApplication.clsPrintReport = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLLoginName, gstrSQLPassword)
                    clsPrntRpt.PrintReport("LabOrderReport_SSRS", "OrderID,PatientID", System.Convert.ToString(OrderID) + "," + System.Convert.ToString(PatientID), False, gstrFAXPrinterName)
                    If Not IsNothing(clsPrntRpt) Then
                        clsPrntRpt.Dispose()
                        clsPrntRpt = Nothing
                    End If

                Else
                    If Not IsNothing(gstrfaxCollection) Then
                        ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
                        ''Change gstrFAXContacts by gstrfaxCollection
                        If gstrfaxCollection.Count = 0 Then
                            MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            'sarika 3rd oct 07
                            'the fax is send even then the fax no. is not entered.
                            Exit Sub
                            '----------------------------------
                        End If

                        'Retrieve FAX Document Name
                        Dim strFAXDocumentName As String
                        strFAXDocumentName = RetrieveFAXDocumentName()

                        Dim strFAXDocumentName1 As String = ""
                        strFAXDocumentName1 = strFAXDocumentName

                        ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
                        ''Change gstrFAXContacts by gstrfaxCollection
                        For i As Integer = 0 To gstrfaxCollection.Count - 1
                            strFAXDocumentName = strFAXDocumentName1 & i.ToString
                            If gloEmdeonCommon.MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Sub

                            Dim mynode As myTreeNode

                            'mynode = New myTreeNode

                            ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
                            ''Change gstrFAXContacts by gstrfaxCollection
                            mynode = CType(gstrfaxCollection.Item(i + 1), myTreeNode)

                            objFAX.AddPendingFAX(PatientID, mynode.Text, "Labs", mynode.Tag, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                            '    oLabs.PrintOptions.PrinterName = gstrFAXPrinterName
                            '      oLabs.PrintToPrinter(1, False, 0, 0)

                            Dim clsPrntRpt As gloSSRSApplication.clsPrintReport = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLLoginName, gstrSQLPassword)
                            clsPrntRpt.PrintReport("LabOrderReport_SSRS", "OrderID,PatientID", System.Convert.ToString(OrderID) + "," + System.Convert.ToString(PatientID), False, gstrFAXPrinterName)
                            If Not IsNothing(clsPrntRpt) Then
                                clsPrntRpt.Dispose()
                                clsPrntRpt = Nothing
                            End If
                            mynode = Nothing
                        Next
                    End If


                End If
                objFAX = Nothing
                '------

            Else
                'sarika internet fax

                Dim strfilename As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf")

                If File.Exists(strfilename) Then
                    File.Delete(strfilename)
                End If

                'ExportLabOrderReport(OrderId, ArrTestName, strTempExamfilepath)
                Dim ocls As New gloSSRSApplication.clsSSRSRender(gstrSQLServerName, gstrDatabaseName, bnSqlAuthentication, gstrSQLLoginName, gstrSQLPassword)
                ocls.SSRSGeneratePDF("LabOrderReport_SSRS", "OrderID,PatientID", System.Convert.ToString(OrderID) + "," & System.Convert.ToString(PatientID), strfilename)
                If (IO.File.Exists(strfilename)) Then
                    Dim in_Doc1 As pdftron.PDF.PDFDoc = New pdftron.PDF.PDFDoc(strfilename)
                    in_Doc1.InitSecurityHandler()
                    in_Doc1.PageCreate()
                    in_Doc1.Save(strfilename, 0)
                    in_Doc1.Close()
                    in_Doc1.Dispose()
                    in_Doc1 = Nothing
                End If

                ocls = Nothing

                If mdlFAX.RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
                    Exit Sub
                Else
                    blnFaxSentfromOrders = True
                End If

                Dim objclsFaxReport As New clsPrintFaxReport

                objclsFaxReport.FaxReport(strfilename, PatientID)
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub



    Property ConnectionString()
        Get
            Return _GetConnectionString
        End Get
        Set(ByVal value)
            _GetConnectionString = value
        End Set
    End Property






#End Region





    Public Function FAXDocument(ByVal objDoc As Wd.Document, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True, Optional ByVal parentForm As Form = Nothing) As Boolean
        Try
            ''Fax Module change
            FAXDocumentRevised(objDoc, nPatientID, sFAXTo, sFAXNo, sLoginUser, dtFAXDate, sFAXTypeDetails, enmFAXDocumentType, IsCleanUpRequired, IsFAXPrinterHasToSet, IsDSODefaultPrinterHasToSet, parentForm)
            Return True
        Catch ex As ClsFaxException
            UpdateLogForFax(ex.ToString)
            ''For Fax Module change
            Return False
            ''For Fax Module change
            Throw ex
        Catch ex As Exception
            UpdateLogForFax(ex.ToString)
            ''For Fax Module change
            Return False
            ''For Fax Module change
            Throw New ClsFaxException(ex.ToString)
        End Try
    End Function


    Public Function FAXDocumentRevised(ByVal objDoc As Wd.Document, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True, Optional ByVal parentForm As Form = Nothing) As Boolean
        Dim wdTemp As Wd.Application
        oTempWordApp = objDoc.ActiveWindow.Application
        Dim smyfile As Object = ""
        Dim oSetting As New gloSettings.GeneralSettings(GetConnectionString)
        oSetting.GetSetting("InsertCoverPageFirstForFaxWithCoverPage", InsertCoverPageFirstForFaxWithCoverPage)

        '' Create a copy of main doc
        Dim _sFileName As Object = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
        _sFAXPrinter = gstrFAXPrinterName
        Try
            objDoc.SaveAs(_sFileName)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Try
            '' When eFax is OFF
            For Each node As myTreeNode In gstrfaxCollection
                '' Create a document object of main doc
                Dim objMainDoc As Wd.Document

                If gblnFAXCoverPage = True AndAlso File.Exists(node.FaxCoverPage) Then
                    If InsertCoverPageFirstForFaxWithCoverPage = True Then
                        smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        FileCopy(node.FaxCoverPage, smyfile)
                        wdTemp = New Wd.Application
                        objMainDoc = wdTemp.Documents.Open(smyfile)
                        objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                        objMainDoc.Activate()
                        objMainDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)

                        ' Main Doc Insert 
                        If File.Exists(_sFileName) Then
                            UpdateLogForFax("Insert the Main Page")
                            UpdateLog("Insert the Main  Page")
                            objMainDoc.ActiveWindow.SetFocus()
                            objMainDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                            objMainDoc.Application.Selection.InsertFile(_sFileName)
                            UpdateLog("Main doc Inserted")
                            UpdateLogForFax("Main doc Inserted")
                        End If
                    Else
                        smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        FileCopy(_sFileName, smyfile)
                        wdTemp = New Wd.Application
                        objMainDoc = wdTemp.Documents.Open(smyfile)
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
                Else
                    ' objMainDoc = oTempWordApp.ActiveDocument
                    smyfile = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                    FileCopy(_sFileName, smyfile)
                    wdTemp = New Wd.Application
                    objMainDoc = wdTemp.Documents.Open(smyfile)
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
                        wdTemp.WordBasic.FilePrintSetup(Printer:=_sFAXPrinter, DoNotSetAsSysDefault:=1)
                        UpdateLog("Printer set")
                    End If
                End If

                If gblnPageNo = True Then
                    UpdateLog("InsertNamePageNo start ")
                    objMainDoc = InsertNamePageNo(objMainDoc, GetPatientDetails(nPatientID))
                    UpdateLog("InsertNamePageNo end")
                End If

                '' Cleanup code
                If enmFAXDocumentType <> enmFAXType.Prescription Then
                    UpdateLogForFax("Start Clean up")
                    UpdateLog("Start Clean up")
                    ''objMainDoc.ActiveWindow.SetFocus()
                    objMainDoc = CleanupDoc(objMainDoc)
                    UpdateLog("Cleaning up Form Fields Done")
                    UpdateLogForFax("Cleaning up Form Fields Done")
                End If
                If (IsNothing(objMainDoc)) Then
                    objMainDoc = New Wd.Document()
                End If
                Try
                    If (gblnInternetFax = False) Then

                        Dim sFileNameTemp As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        Try
                            objMainDoc.SaveAs(sFileNameTemp)
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        End Try

                        '' Generate the binary file & save to DB
                        FAXDocumentNormalRevised(objMainDoc, nPatientID, node.FaxName, node.FaxTo, sLoginUser, dtFAXDate, sFAXTypeDetails, enmFAXDocumentType, IsCleanUpRequired, IsFAXPrinterHasToSet, IsDSODefaultPrinterHasToSet, node, parentForm)

                        '' Dispose Temp Document object
                        objMainDoc.Close(False)
                        objMainDoc = Nothing
                        If Not wdTemp Is Nothing Then
                            wdTemp.Quit()
                            wdTemp = Nothing
                        End If

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
                        Dim sFileNameTemp As String = mdlGeneral.ExamNewFaxFileName(gloSettings.FolderSettings.AppTempFolderPath, ".docx")
                        Try
                            objMainDoc.SaveAs(sFileNameTemp, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)

                            '' Dispose Temp Document object
                            objMainDoc.Close(False)
                            objMainDoc = Nothing
                            If Not wdTemp Is Nothing Then
                                wdTemp.Quit()
                                wdTemp = Nothing
                            End If

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
        Catch ex As Exception
            Return False
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Return True
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

            objEFaxSettings = Nothing   'Change made to solve memory Leak and word crash issue

        Catch ex As ClsFaxException
            Throw ex
        Catch ex As Exception
            Throw New ClsFaxException(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function GetFaxServiceVendor() As String

        Dim sFaxVendorQuery As String
        Dim objResult As Object

        Dim oDb As New gloDatabaseLayer.DBLayer(GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord))

        Try

            oDb.Connect(False)
            Dim gstrDatabaseName As String = System.Convert.ToString(appSettings("DatabaseName"))

            sFaxVendorQuery = " SELECT Distinct     " &
                              "  (select sSettingsValue from GLSettings where sSettingsName='FaxAccountType' AND GLSettings.nReferenceId=DBSettings.nDBConnectionId) AS AccountType " &
                              "  FROM         DBSettings INNER JOIN " &
                              "  GLSettings ON DBSettings.nDBConnectionId = GLSettings.nReferenceId " &
                              "  WHERE     (LOWER(DBSettings.sServiceName) = 'sfax') and sDatabaseName ='" & gstrDatabaseName & "'"

            objResult = oDb.ExecuteScalar_Query(sFaxVendorQuery)

            If Not objResult Is Nothing AndAlso objResult <> "" Then
                If System.Convert.ToInt32(objResult) = 3 Then 'Vendor = UPDOX
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


        Try
            ' Check if the Fax service is configured with Updox vendor then conver the document into the PDF format 
            'and save into the Peding Fax table
            If FaxVendor = "UPDOX" And objEFaxSettings.EFax_DocumentExtension = "docx" Then

                sPDFfileName = Path.GetDirectoryName(objEFaxSettings.EFax_Faxfilepath) & "\" & System.Guid.NewGuid().ToString & ".PDF"

                Dim oword As Wd.Application = New Wd.Application()
                Dim odoc As Wd.Document = oword.Documents.Open(objEFaxSettings.EFax_Faxfilepath)
                odoc.Activate()
                odoc.SaveAs(sPDFfileName, Wd.WdSaveFormat.wdFormatPDF)
                odoc.Close()
                oword = Nothing
                odoc = Nothing

                oByte = File.ReadAllBytes(sPDFfileName)
                strData = System.Convert.ToBase64String(oByte)
                sDocExt = "PDF"

                If (File.Exists(sPDFfileName)) Then
                    File.Delete(sPDFfileName)
                End If

            Else
                Dim objword As New clsWordDocument
                oByte = objword.ConvertFiletoBinary(objEFaxSettings.EFax_Faxfilepath)
                strData = System.Convert.ToBase64String(oByte)
                objword = Nothing
                sDocExt = "docx"
            End If

            oByte = Nothing 'Change made to solve memory Leak and word crash issue
            'Add the FAX Details in the Database

            nFaxID = objFAX.AddPendingFAX1(objEFaxSettings.PatientID, objEFaxSettings.EFax_FaxRecipientName, gstrFAXType, objEFaxSettings.EFax_FaxRecipientNumber, objEFaxSettings.EFax_FromName, "", Now, strData, sDocExt, CurrentSendingFAXPriority)

        Catch ex As Exception
            Throw New ClsFaxException(ex.ToString)
        End Try

        objFAX.Dispose()    'Change made to solve memory Leak and word crash issue
        objFAX = Nothing
        UpdateLogForFax("END objFAX.AddPendingFAX For Single Recipent")
        Return Nothing
    End Function

    ' For Fax Module Changes
    Public Function FAXDocumentNormalRevised(ByVal objDoc As Wd.Document, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As DateTime, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As enmFAXType, Optional ByVal IsCleanUpRequired As Boolean = True, Optional ByVal IsFAXPrinterHasToSet As Boolean = True, Optional ByVal IsDSODefaultPrinterHasToSet As Boolean = True, Optional ByVal node As myTreeNode = Nothing, Optional ByVal parentForm As Form = Nothing) As Boolean
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
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Initialize, " Initializing FAX Driver Start", gloAuditTrail.ActivityOutCome.Success)
                AxBlackIceDEVMODE1 = New AxBLACKICEDEVMODELib.AxBlackIceDEVMODE
                CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).BeginInit()
                parentForm.Controls("pnlWordTemplate").Controls.Add(AxBlackIceDEVMODE1)
                AxBlackIceDEVMODE1.Enabled = True
                AxBlackIceDEVMODE1.Visible = False
                AxBlackIceDEVMODE1.Location = New System.Drawing.Point(594, 24)
                AxBlackIceDEVMODE1.Name = "AxBlackIceDEVMODE1"
                AxBlackIceDEVMODE1.Size = New System.Drawing.Size(16, 16)
                AxBlackIceDEVMODE1.TabIndex = 10
                CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).EndInit()
                SetFAXPrinterDefaultSettings1()
            Catch ex As Exception
                MessageBox.Show("Error while setting Printer Default settings. " & ex.ToString, "gloEMR Fax", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        Try
            strTIFFFileName = RetrieveFAXDocumentName()
            If SetFAXPrinterDocumentSettings1(strTIFFFileName) = False Then
                FAXDocumentNormalRevised = Nothing
                Exit Function
            End If

            UpdateLog("Starts Printing.....")
            '' Bug #16438: Fax >> Application is printing document instead of sending fax, if the eFax setting is off.
            objDoc.Application.WordBasic.FilePrintSetup(Printer:=_sFAXPrinter, DoNotSetAsSysDefault:=1)
            '' generate a TIFF 
            objDoc.Application.Options.PrintBackground = False
            objDoc.PrintOut(Background:=False)

            '   objDoc.PrintOut()
            'To wait until the fax gets printed so that the process doesnt go further
            Threading.Thread.Sleep(300)
            Dim strfname As String = ""
            strfname = strTIFFFileName
            objFAX.AddPendingFAX(nPatientID, node.FaxName, gstrFAXType, node.FaxTo, sLoginUser, strfname, dtFAXDate, CurrentSendingFAXPriority)
            strTIFFFileName = String.Empty

            blnTIFFFileGenerated = True
            If IsDSODefaultPrinterHasToSet = True Then
                '' Bug #16438: Fax >> Application is printing document instead of sending fax, if the eFax setting is off.
                objDoc.Application.WordBasic.FilePrintSetup(Printer:=_sDefaultPrinter, DoNotSetAsSysDefault:=1)
            End If

        Catch ex As Exception
            'Error occured. So TIFF File has not been generated
            blnTIFFFileGenerated = False
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
            objDoc = Nothing
            If (Not IsNothing(AxBlackIceDEVMODE1)) Then
                parentForm.Controls("pnlWordTemplate").Controls.Remove(AxBlackIceDEVMODE1)
                AxBlackIceDEVMODE1.Dispose()
                AxBlackIceDEVMODE1 = Nothing
            End If

        End Try
        Return blnTIFFFileGenerated
    End Function

    Public Function GetOSName() As String
        Dim result As String = String.Empty
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem")
            For Each os As ManagementObject In searcher.Get()
                result = os("Caption").ToString()
                Exit For
            Next
            searcher.Dispose() : searcher = Nothing

        Catch ex As Exception
        End Try
        Return result
    End Function

    Public Function SetFAXPrinterDefaultSettings1() As Boolean

        Try

            If gstrFAXPrinterName = "" Then
                SetFAXPrinterDefaultSettings1 = Nothing
                Exit Function
            End If
            ''Added for check fax seetings for Black Ice issuie on windows8 as on 20121203 
            If gblnInternetFax = False Then

                Dim strOsName As String = GetOSName()
                If strOsName.Trim() <> String.Empty And strOsName.ToLower().Contains("windows 8") Then
                Else
                    pBlackIceDEVMODE = AxBlackIceDEVMODE1.LoadBlackIceDEVMODE(gstrFAXPrinterName)

                    If pBlackIceDEVMODE = 0 Then
                        MsgBox("Cannot open '" & gstrFAXPrinterName & "' Printer driver", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                        SetFAXPrinterDefaultSettings1 = Nothing
                        Exit Function
                    End If

                    ' Output directory
                    bsuccess = AxBlackIceDEVMODE1.SetOutputDirectory(gstrFAXOutputDirectory, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetOutputDirectory'")
                    End If

                    ' File format TIFF group 4

                    bsuccess = AxBlackIceDEVMODE1.SetFileFormat(7, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetFileFormat'")
                    End If


                    ' Orientation (Portrait/Landscape

                    bsuccess = AxBlackIceDEVMODE1.SetOrientation(1, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetOrientation'")
                    End If

                    'disable generation of group file
                    bsuccess = AxBlackIceDEVMODE1.DisableGroupFile(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableGroupFile'")
                    End If

                    ''if group file is generated disable deletion of group file
                    bsuccess = AxBlackIceDEVMODE1.DisableDeleteGroupFile(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableDeleteGroupFile'")
                    End If

                    'The document will not be forced to be printed always using the printer’s resolution, regardless to the DPI setting stored in the document 
                    bsuccess = AxBlackIceDEVMODE1.DisableForcePrinterDPI(pBlackIceDEVMODE)
                    If Not bsuccess Then
                        MsgBox("Error in calling Active X function: 'DisableForcePrinterDPI'")
                    End If

                    bsuccess = AxBlackIceDEVMODE1.DisableAdvancedPaperSize(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableAdvancedPaperSize'")
                    End If

                    'vertical and horizontal resolution values.
                    bsuccess = AxBlackIceDEVMODE1.SetXDPI(200, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetXDPI'")
                    End If
                    bsuccess = AxBlackIceDEVMODE1.SetYDPI(200, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetYDPI'")
                    End If

                    ' Color depth
                    bsuccess = AxBlackIceDEVMODE1.SetColorDepth(BITS_8, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        'MsgBox "Error in calling Active X function: 'SetColorDepth'"
                    End If

                    'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
                    bsuccess = AxBlackIceDEVMODE1.EnablePageNumbering(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'EnablePageNumbering'")
                    End If

                    '3 indicates Exact filename.
                    'i.e., we ourself generate the filename, we are not using any Blackice filename generation method .
                    bsuccess = AxBlackIceDEVMODE1.SetFileGenerationMethod(3, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetFileGenerationMethod'")
                    End If

                    bsuccess = AxBlackIceDEVMODE1.DisableFaxOutput(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'EnableFaxOutput'")
                    End If

                    'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
                    bsuccess = AxBlackIceDEVMODE1.DisableWriteText(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableWriteText'")
                    End If


                    'GammaLink compatible TIFF output requires this setting to be checked, because they can only send TIFF images with reverse bit order.
                    ' the driver will create a TIFF file that is compatible with the requirements listed in File Format for Internet Fax.
                    bsuccess = AxBlackIceDEVMODE1.DisableInternetTiffFormat(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableInternetTiffformat'")
                    End If

                    'if you are not capturing printer messages for end of printing, for example you can disable this in the printer options and it can get you a little more speed.
                    bsuccess = AxBlackIceDEVMODE1.DisableMessagingInterface(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'DisableMessagingInterface'")
                    End If

                    'The Photo Quality option enables or disables the dithering of the Black Ice driver.
                    bsuccess = AxBlackIceDEVMODE1.SetDithering(DITHER_SHARP, pBlackIceDEVMODE)

                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'SetDithering'")
                    End If

                    bsuccess = AxBlackIceDEVMODE1.EnableMultipageImage(pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error in calling Active X function: 'EnableMultipageImage'")
                    End If

                    'to save the settings applied to the black ice printer deriver
                    ' The Smooth, Sharp, and Stucki filters produce better quality output.
                    bsuccess = AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
                    If (bsuccess = False) Then
                        MsgBox("Error saving the devmode")
                        SetFAXPrinterDefaultSettings1 = Nothing
                        Exit Function
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Finally

        End Try
    End Function

    Public Shared Function SetFAXPrinterDocumentSettings1(ByVal strFAXDocumentName As String) As Boolean
        Try
            'If gstrFAXPrinterName = "" Then
            '    MessageBox.Show("Please select the Fax printer name", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            bsuccess = AxBlackIceDEVMODE1.SetImageFileName(strFAXDocumentName & ".tif", pBlackIceDEVMODE)

            If (bsuccess = False) Then
                MsgBox("Error in calling active X function: 'SetImageFileName'")
            End If

            'all the files will be appended in a single tiff file
            bsuccess = AxBlackIceDEVMODE1.EnableKeepExistingFiles(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling active X function: 'EnableKeepExistingFiles'")
            End If

            bsuccess = AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error saving the devmode")
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally


        End Try
    End Function

#Region "Private functions"

    Public Function GetPatientDetails(ByVal m_PatientId As Int64)
        Dim strName As String = ""

        Dim oDB As New DataBaseLayer
        Try

            Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ' , DOB : ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId
            strName = oDB.GetRecord_Query(strSQL)
            If Not IsNothing(strName) Then
                Return strName
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        Finally
            oDB.Dispose()   'Change made to solve memory Leak and word crash issue
            oDB = Nothing
        End Try
    End Function

    Private Function GetPatientName(ByVal m_PatientId As Int64)
        Dim strName As String = ""

        Dim oDB As New DataBaseLayer
        Try

            Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'') as Name from Patient where nPatientID=" & m_PatientId
            strName = oDB.GetRecord_Query(strSQL)
            If Not IsNothing(strName) Then
                Return strName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return ""
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Private Function InsertNamePageNo(ByVal oCurDoc As Wd.Document, ByVal sName As String) As Wd.Document
        If oCurDoc Is Nothing Then
            Return Nothing
            Exit Function
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


            Dim strtxt As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            strtxt &= "\Microsoft\Document Building Blocks\1033\Building Blocks.dotx"

            If File.Exists(strtxt) Then
                oCurDoc.AttachedTemplate = strtxt
                oCurDoc.XMLSchemaReferences.AutomaticValidation = True
                oCurDoc.XMLSchemaReferences.AllowSaveAsXMLWithoutValidation = False
            End If

            For Each objTemp As Wd.Template In oCurDoc.Application.Templates
                If objTemp.Name = "Building Blocks.dotx" Then
                    objTemp.BuildingBlockEntries.Item("Bold Numbers 3").Insert(Where:=oCurDoc.Application.Selection.HeaderFooter.Range, RichText:=True)
                End If
            Next
            If sName <> "" Then

                oCurDoc.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft
                oCurDoc.Application.Selection.HeaderFooter.Range.InsertBefore(sName & vbTab & vbTab)
                oCurDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                oCurDoc.Application.Selection.TypeBackspace()
            End If
            Return oCurDoc


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return oCurDoc
        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Function

    Private Function CleanupDocold(ByVal oCUDoc As Wd.Document) As Wd.Document
        Dim objField As Wd.FormField 'Form field Variable

        For Each objField In oCUDoc.FormFields
            objField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight
            If objField.Type = Wd.WdFieldType.wdFieldFormTextInput Then
                If objField.HelpText = objField.Result Then
                    objField.Result = ""
                End If
            End If
        Next

        ''//To replace the special tags
        Dim col_Tags As New Collection
        col_Tags.Add("[]")
        col_Tags.Add("[HPI]")
        col_Tags.Add("[Xray]")
        col_Tags.Add("[MRI]")
        col_Tags.Add("[PLAN]")

        For i As Int16 = 1 To col_Tags.Count
            ' oCUDoc.Application.Selection.Find.ClearFormatting()
            'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
            'Try
            '    oCUDoc.Application.Selection.Find.Execute(FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll)

            'Catch ex As Exception
            Try

                gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCUDoc.Application, FindText:=CStr(col_Tags(i)).Trim, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
            Catch ex2 As Exception

            End Try
            'End Try

            'With _oCurDoc.Application.Selection.Find
            '    .Text = CStr(col_Tags(i)).Trim
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
            '_oCurDoc.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        Next
        Return oCUDoc
    End Function

    '''' <summary>
    '''' To Clean up the Document for removing FormFields and Tags that does n't contain data
    '''' </summary>
    '''' <remarks></remarks>
    Public Function CleanupDoc(ByVal oCUDoc As Wd.Document) As Wd.Document

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
            'oCUDoc.Application.Selection.Find.ClearFormatting()
            'oCUDoc.Application.Selection.Find.Replacement.ClearFormatting()
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

        Return oCUDoc
    End Function

    Public Function getClientID()
        Dim objCmd As SqlCommand = Nothing
        Try
            'gnPrefixTransactionID = 0
            'Dim nLoginUsers As Byte
            Dim objCon As New SqlConnection
            objCmd = New SqlCommand
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure

            'Aniket Renamed gsp_CheckClientMachinePermission to sp_CheckClientMachinePermission as it is necessary for backward compatibility in multiple databases
            objCmd.CommandText = "sp_CheckClientMachinePermission"
            objCmd.Connection = objCon

            Dim objParaClientMachineName As New SqlParameter
            With objParaClientMachineName
                .ParameterName = "@MachineName"
                .Value = System.Windows.Forms.SystemInformation.ComputerName()
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaClientMachineName)
            objParaClientMachineName = Nothing

            ''Sandip Darade 20091113
            Dim objParaProductCode As New SqlParameter
            With objParaProductCode
                .ParameterName = "@sProductCode"
                .Value = "1"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProductCode)
            objParaProductCode = Nothing

            gnClientMachineID = 0
            objCon.Open()
            gnClientMachineID = objCmd.ExecuteScalar
            objCon.Close()
            If IsNothing(gnClientMachineID) Then
                gnClientMachineID = 0
            End If
            'objCmd.Dispose()
            'objCmd = Nothing
            objCon.Dispose()
            objCon = Nothing
            'If gnClientMachineID = 0 Then
            '    Return False
            'Else
            '    Return True
            'End If
        Catch ex As Exception
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        strTIFFFileName = gnClientMachineID & "-" & Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond

        UpdateVoiceLog("In retrieve fax document name")
        Return strTIFFFileName
    End Function

    Private Function SetFAXPrinterDocumentSettings(ByVal strFAXDocumentName As String) As Boolean
        Try
            'Set TIFF Printer settings by using EXE
            If File.Exists(Application.StartupPath & "\TIFF Printer\zvprtcfg_wIN32.exe") = True Then
                'Set FAX Printer Output File Name
                Shell(Application.StartupPath & "\TIFF Printer\zvprtcfg_wIN32.exe " & gstrFAXPrinterName & " save.basefilename=" & strFAXDocumentName)
            Else
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try
    End Function





    Public Function Get_FAXCoverPageData(ByVal PatientID As Long, ByVal strFields As String, ByVal nVisitID As Long, Optional ByVal nEXAMID As Long = 0, Optional ByVal nReferralID As Long = 0) As String
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim filecnt As Int16
        Dim strDataCol As String = ""
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing

        Dim objCmd1 As New SqlCommand
        Dim sqlParam1 As SqlParameter = Nothing

        Try

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetFieldsdata_FAX"
            objCmd.Parameters.Clear()
            'Con.Open()
            'Dim nCount As Int16
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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                Dim strFileName As String
                                If objSQLDataReader.Item(1) = "2" Then
                                    filecnt = filecnt + 1
                                    If InStr(strFields, "Narration") Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & "image.bmp"
                                End If
                                strData = strFileName
                                '''''
                                'Save contents in file
                                Dim mstream As ADODB.Stream
                                mstream = New ADODB.Stream
                                mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                                mstream.Open()
                                '       Dim sContents As String
                                'Check if there are records for selected Node
                                mstream.Write(objSQLDataReader.Item(0))
                                If System.IO.File.Exists(strFileName) Then
                                    System.IO.File.Delete(strFileName)
                                End If
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                                mstream.Close()
                                If InStr(strFields, "SingleRow") Then
                                    'Dim oFile As System.IO.File
                                    Dim oRead As System.IO.StreamReader
                                    Dim LineIn As String
                                    Dim strNewString As New ArrayList
                                    'Dim j As Integer
                                    oRead = File.OpenText(strFileName)

                                    While oRead.Peek <> -1
                                        LineIn = oRead.ReadLine()
                                        strNewString.Add(LineIn)
                                    End While
                                    oRead.Close()
                                    oRead.Dispose()
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
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                End If
                                '''''
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()
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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else

                                    'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

                Else
                    flagOthers = 0
                    Select Case strFields
                    End Select
                    If InStr(strFields, "Age") Then

                        Dim objSQLDataReader1 As SqlDataReader

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
                        strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                        '                        strData = "20"
                        objSQLDataReader1.Close()
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

        Catch ex As Exception
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If objCmd1 IsNot Nothing Then
                objCmd1.Parameters.Clear()
                objCmd1.Dispose()
                objCmd1 = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If


            If Not IsNothing(sqlParam1) Then
                sqlParam1 = Nothing
            End If
        End Try
        Return strDataCol
    End Function

    Public Sub OpenConnection()
        objCon.ConnectionString = GetConnectionString()
        objCon.Open()
    End Sub

    Public Sub CloseConnection()
        objCon.Close()
    End Sub

#End Region

    'Private Sub FAXDocumentRevised(ByVal objDoc As Microsoft.Office.Interop.Word.Document, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sLoginUser As String, ByVal dtFAXDate As Date, ByVal sFAXTypeDetails As String, ByVal enmFAXDocumentType As clsPrintFAX.enmFAXType, ByVal IsCleanUpRequired As Boolean, ByVal IsFAXPrinterHasToSet As Boolean, ByVal IsDSODefaultPrinterHasToSet As Boolean)
    '    Throw New NotImplementedException
    'End Sub

End Class

