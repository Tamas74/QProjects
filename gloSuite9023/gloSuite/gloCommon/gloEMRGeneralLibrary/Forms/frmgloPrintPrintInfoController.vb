
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.Threading
Imports gloPrintDialog
Imports System.Data.SqlClient
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Runtime.InteropServices

''Namespace gloEMRGeneralLibrary

Partial Public Class frmgloPrintPrintInfoController
    Inherits Form

    Private _PrinterSetting As PrinterSettings = Nothing

    Private _ExtendedPrinterSettings As gloExtendedPrinterSettings = Nothing

    Public IsPrintingResumed As Boolean = False

    Public IsPrintingCanceled As Boolean = False

    Private IsPrintingCompletedWhilePressingPause As Boolean = True

    Public ChkRemiderForUnSchedle As Boolean = False

    Public Shared blnBackGroundPrint As Boolean = False

    Private PrintDocument As Integer = 0

    Public _bIsUnscheduledCare As Boolean = False

    Private clickTime As DateTime = DateTime.Now

    Private blnbtnPauseclicked As Boolean = False

    Private _isFormClose As Boolean = False

    '  Public oListTempleteIds As ArrayList = Nothing

    Public OldPrinterName As String = ""

    Private myLoadWord As gloWord.LoadAndCloseWord = Nothing

    Public _databaseConnectionString As String = ""

    Private _popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing

    Private myCaller As Control = Nothing
    Public WithEvents InfoButtonWebBrowser As System.Windows.Forms.WebBrowser
    Public Sub New(Optional ByVal myControl As Control = Nothing)
        'Shown += AddressOf frmgloPrintWordProgressController_Shown
        'FormClosed += AddressOf frmgloPrintWordProgressController_FormClosed
        myCaller = myControl
        InitializeComponent()
    End Sub

    Public Sub New(ByVal sPrinterSettings As PrinterSettings, ByVal sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional ByVal oSourceDocSelectedPages As ArrayList = Nothing, Optional ByVal myControl As Control = Nothing)
        myCaller = myControl
        gloPrintWordProgressControllerCall(sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages)
    End Sub

    Friend Sub gloPrintWordProgressControllerCall(ByVal sPrinterSettings As PrinterSettings, ByVal sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional ByVal oSourceDocSelectedPages As ArrayList = Nothing)
        Try
            InitializeComponent()
            If gloGlobal.gloTSPrint.isCopyPrint Then
                Me.Text = "Copying for Printing"
            Else
                Me.Text = "Printing"
            End If

            Me.BringToFront()
            _PrinterSetting = sPrinterSettings
            _ExtendedPrinterSettings = sExtendedPrinterSettings
            If gloGlobal.gloTSPrint.isCopyPrint AndAlso (Not gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False)) Then
                _ExtendedPrinterSettings.IsBackGroundPrint = True
                _ExtendedPrinterSettings.IsShowProgress = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Const CP_NOCLOSE_BUTTON As Integer = 512

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get

    End Property

    Private Sub frmgloPrintWordProgressController_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

    End Sub



    Private Delegate Sub del()

    Private methodToInvoke As EventHandler

    Public Sub PrintWithOrWithoutBackground()
        Try
            If _ExtendedPrinterSettings.IsShowProgress Then
                Dim retry As Integer = 20
                While Not IsPrintingCompletedWhilePressingPause
                    InvokeBackgroundUpdateControls()
                    Thread.Sleep(100)
                    If System.Math.Max(System.Threading.Interlocked.Decrement(retry), retry + 1) = 0 Then
                        Exit While
                    End If
                End While

                IsPrintingCompletedWhilePressingPause = False
            End If

            Dim res As Boolean = True
            _popUpDetails = gloWord.LoadAndCloseWord.getTSPrintDialogDetails(res)
            If Not res Then
                IsPrintingCompletedWhilePressingPause = True
                InvokeCompleteUpdateControls()
                Return
            End If

            If _ExtendedPrinterSettings.IsBackGroundPrint Then
                blnBackGroundPrint = True
                If (methodToInvoke IsNot Nothing) Then
                    methodToInvoke = Nothing
                End If

                methodToInvoke = New EventHandler(AddressOf Me.OnPrint)
                methodToInvoke.BeginInvoke(Me, Nothing, New AsyncCallback(AddressOf Me.OnPrintComplete), Nothing)
            Else
                Print()
                IsPrintingCompletedWhilePressingPause = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Public Delegate Sub UpdateBackgroundControlsDelegate()

    Public Sub InvokeBackgroundUpdateControls()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateBackgroundControlsDelegate(AddressOf UpdateBackgroundControls))
            Else
                UpdateBackgroundControls()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private TempPrintDocument As Integer = 0

    Private Sub UpdateBackgroundControls()
        Try
            TempPrintDocument = PrintDocument
            If (TempPrintDocument = 0) Then
                TempPrintDocument = 1
            End If

            lblPages.Text = "Printing " & TempPrintDocument.ToString() & " of " + pbDocument.Maximum.ToString()
        Catch
        End Try
    End Sub

    Private Sub OnPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Print()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Public Delegate Sub UpdateBeginControlsDelegate()

    Public Sub InvokeBeginUpdateControls()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateBeginControlsDelegate(AddressOf UpdateBeginControls))
            Else
                UpdateBeginControls()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub UpdateBeginControls()
        Try
            If gloGlobal.gloTSPrint.isCopyPrint Then
                lblPrinterNameValue.Text = "To Network Drive"
            Else
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            End If

            pbDocument.Minimum = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private _ntempTemplateID As Long = 0

    Private Sub Print()
        Dim fromDoc As Int64 = 0
        Try
            If myLoadWord Is Nothing Then
                myLoadWord = New gloWord.LoadAndCloseWord()
            End If

            For iCount As Integer = 0 To 0
                If fromDoc < PrintDocument Then
                    fromDoc += 1
                Else
                    If _isFormClose = True Then
                        Return
                    End If

                    '_ntempTemplateID = Convert.ToInt64(oListTempleteIds(iCount))
                    Try
                        PrintTemplate(myLoadWord)
                        PrintDocument += 1
                        fromDoc = PrintDocument
                        InvokeProgressUpdateControls()
                        If blnbtnPauseclicked Then
                            InvokeEnableDisablePauseButton()
                        End If

                        If IsPrintingResumed Then
                            Exit For
                        End If
                    Catch ex1 As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, False)
                    End Try

                    _ntempTemplateID = 0
                End If
            Next
        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
        Catch __unusedException2__ As Exception
        Finally
            If (blnBackGroundPrint = False) And _isFormClose = False Then
                _isFormClose = True
                Me.Close()
                Me.Dispose(True)
            End If
        End Try
    End Sub

    Public Function GetPatientTemplate(ByVal TransactionID As Int64) As DataTable
        'Dim oDB As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        'oDB.Connect(False)
        'Dim strSQL As String = ""
        'Dim dt As DataTable = Nothing
        'strSQL = " SELECT isnull(nPatientID,0) as nPatientID, isnull(nCategoryID,0) AS nCategoryID , ISNULL(sCategoryName,'') as sCategoryName , isnull(nTemplateID,0) AS nTemplateID , ISNULL(sTemplateName,'') as sTemplateName , nFromDate, nToDate, ISNULL(nProviderID,0) AS nProviderID, ISNULL(iTemplate,null) AS iTemplate, ISNULL(nCount,1) AS nCount, ISNULL(nClinicID,0) AS nClinicID " & " FROM PatientTemplates WITH (NOLOCK) " & " WHERE nTransactionID = " + TransactionID & " "
        'oDB.Retrive_Query(strSQL, dt)
        'oDB.Disconnect()
        'oDB.Dispose()
        'Return dt
        Return Nothing
    End Function

    Public Function ConvertBinaryToFile(ByVal StreamData As Object, ByVal FilePath As String) As Boolean
        'Dim _result As Boolean = False
        'Try
        '    If StreamData IsNot Nothing AndAlso StreamData <> DBNull.Value Then
        '        Dim byteRead As Byte() = CType(StreamData, Byte())
        '        Dim oFile As FileStream = New FileStream(FilePath, FileMode.Create)
        '        oFile.Write(byteRead, 0, byteRead.Length)
        '        oFile.Close()
        '        oFile.Dispose()
        '        oFile = Nothing
        '        _result = True
        '    End If
        'Catch
        'End Try

        Return Nothing
    End Function

    Private TemplateName As String = ""
    'Public Function ConvertData() As Microsoft.Office.Interop.Word.Document
    '    '  Me.Cursor = Cursors.WaitCursor
    '    Dim strPdfFilepath As String = ""
    '    Dim objpdf As pdftron.PDF.PDFDoc
    '    Dim wd As Microsoft.Office.Interop.Word.Document
    '    Dim wdcreated As Boolean = False
    '    Dim objWord As gloWord.LoadAndCloseWord = Nothing
    '    Dim Copied As Boolean = False
    '    Dim gblnUseDefaultPrinter As Boolean = False
    '    Dim frmobj As Object
    '    Dim outputFileName As String = String.Empty
    '    If gloGlobal.gloTSPrint.isCopyPrint Then

    '        objWord = New gloWord.LoadAndCloseWord()

    '        outputFileName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
    '        wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
    '        'Copied = gloWord.LoadAndCloseWord.CopyPrintDoc(wd, 0)
    '        'wdcreated = True
    '        'If (Copied = True) Then
    '        '    objWord.CloseWordApplication(wd)
    '        '    objWord = Nothing
    '        'End If
    '    End If

    '    'If Copied = False Then
    '    '    Try


    '    '        If (wdcreated = True) Then


    '    '            'Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")

    '    '            'strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")

    '    '            ' wd.SaveAs(strPdfFilepath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)
    '    '            ' objWord.CloseWordApplication(wd)
    '    '            'objWord = Nothing
    '    '        Else

    '    '            objWord = New gloWord.LoadAndCloseWord()

    '    '            Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")

    '    '            Try


    '    'wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
    '    '            Catch ex As Exception

    '    'End Try

    '    If wd Is Nothing Then

    '        wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
    '    End If
    '    If wd Is Nothing Then

    '        wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
    '    End If

    '    ' strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")

    '    ' wd.SaveAs(strPdfFilepath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, False, "", False)



    '    ' End If
    '    'Catch expp As Exception


    '    'Finally
    '    '    If Not IsNothing(objWord) Then
    '    '        objWord.CloseWordApplication(wd)
    '    '    End If
    '    objWord = Nothing
    '    '    '      If Me.InfoButtonWebBrowser.ReadyState = WebBrowserReadyState.Complete Then
    '    '    If gblnUseDefaultPrinter = True Then
    '    '        objpdf = New PDFDoc(strPdfFilepath)
    '    '        Print(objpdf)
    '    '    Else
    '    '        objpdf = New PDFDoc(strPdfFilepath)
    '    '        Print(objpdf)
    '    '    End If

    '    '    If Not IsNothing(objpdf) Then
    '    '        objpdf.Dispose()
    '    '        objpdf = Nothing
    '    '    End If

    '    'End Try



    '    'End If
    '    ' End If
    '    Return wd
    'End Function
    Public Sub PrintTemplate(ByRef myLoadWord As gloWord.LoadAndCloseWord)
        '  Dim dttemp As DataTable = Nothing
        Dim Visible As Object = False
        Dim Background As Object = False
        Dim Range As Object = Wd.WdPrintOutRange.wdPrintAllDocument
        Dim Copies As Object = If(gloGlobal.gloTSPrint.isCopyPrint, CShort(1), _PrinterSetting.Copies)
        Dim PageType As Object = Wd.WdPrintOutPages.wdPrintAllPages
        Dim PrintToFile As Object = False
        Dim Collate As Object = If(gloGlobal.gloTSPrint.isCopyPrint, True, _PrinterSetting.Collate)
        Dim ActivePrinterMacGX As Object = Type.Missing
        Dim ManualDuplexPrint As Object = False
        Dim PrintZoomColumn As Object = 1
        Dim PrintZoomRow As Object = 1
        Dim missing As Object = Type.Missing
        Try
            'dttemp = GetPatientTemplate(_TransactionID)
            'If dttemp IsNot Nothing AndAlso dttemp.Rows.Count > 0 Then
            '    TemplateName = dttemp.Rows(0)("sTemplateName").ToString()
            '    Dim strNewDocumentName As String = ""
            '    '  strNewDocumentName = gloOffice.Supporting.NewDocumentName()
            '    strNewDocumentName = "" '' type new document
            '    Dim objTemplateDocument As Object
            '    If Not String.IsNullOrEmpty(dttemp.Rows(0)("iTemplate").ToString()) Then
            '        objTemplateDocument = dttemp.Rows(0)("iTemplate")
            '        If TemplateName.Contains("PatientStatement") Then
            '            strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc")
            '        End If

            '        ConvertBinaryToFile(objTemplateDocument, strNewDocumentName)


            'If gloGlobal.gloTSPrint.isCopyPrint Then
            '    Dim objWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            '    Dim wd As Microsoft.Office.Interop.Word.Document
            '    Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
            '    wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
            '    Copied = gloWord.LoadAndCloseWord.CopyPrintDoc(wd, 0)
            '    objWord.CloseWordApplication(wd)
            '    objWord = Nothing
            'End If
            'If Copied = False Then
            '    If gblnUseDefaultPrinter Then
            '        InfoButtonWebBrowser.Print()
            '    Else
            '        InfoButtonWebBrowser.ShowPrintDialog()
            '    End If
            'End If

            ' Dim objWord = New gloWord.LoadAndCloseWord()

            Dim outputFileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
            '  Wd = objWord.WebToDocx(InfoButtonWebBrowser, outputFileName)


            'Dim oTemp As Wd.Document = myLoadWord.LoadWordApplication(strNewDocumentName)
            Dim oTemp As Wd.Document = myLoadWord.WebToDocx(InfoButtonWebBrowser, outputFileName)
            If (oTemp IsNot Nothing) Then
                If Not gloGlobal.gloTSPrint.isCopyPrint Then
                    If OldPrinterName <> _PrinterSetting.PrinterName Then
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName)
                        Application.DoEvents()
                    End If
                End If

                gloWord.LoadAndCloseWord.PrintDocument(oTemp, Background, missing, missing, missing, missing, missing, missing, Copies, missing, missing, PrintToFile, Collate, missing, ManualDuplexPrint, PrintZoomColumn, PrintZoomRow, missing, missing, popupDetails:=_popUpDetails)
            End If

            myLoadWord.CloseWordOnly(oTemp)
            If Not gloGlobal.gloTSPrint.isCopyPrint Then
                If OldPrinterName <> String.Empty Then
                    gloGlobal.gloTSPrint.SetDefaultPrinterSettings(OldPrinterName)
                    Application.DoEvents()
                End If
            End If
            'End If
            ' End If
        Catch ex As System.Runtime.InteropServices.COMException
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
        Catch __unusedException2__ As Exception
        Finally
            'If dttemp IsNot Nothing Then
            '    dttemp.Dispose()
            'End If

            'dttemp = Nothing
        End Try
    End Sub

    Public Delegate Sub EnableDisablePauseButtonDelegate()

    Public Sub InvokeEnableDisablePauseButton()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New EnableDisablePauseButtonDelegate(AddressOf EnableDisablePauseButton))
            Else
                EnableDisablePauseButton()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub EnableDisablePauseButton()
        Dim ts As TimeSpan = DateTime.Now.Subtract(clickTime)
        If ts.Seconds > 1 Then
            blnbtnPauseclicked = False
            btnPause.Enabled = True
        End If
    End Sub

    Public Function GetPatientDetails(ByVal m_PatientId As Int64) As Object
        Return Nothing
    End Function

    Public Sub InsertNamePageNo(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Return
        End If

        Try
            If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If

            oCurDoc.Activate()
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter
            oCurDoc.Application.Selection.[Select]()
            If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                oCurDoc.Application.Selection.HeaderFooter.Range.[Select]()
            End If

            Dim strFolderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Microsoft\Document Building Blocks\1033"
            Dim strtxt As String = ""
            If Directory.Exists(strFolderPath & "\14") Then
                strtxt = strFolderPath & "\14\Built-In Building Blocks.dotx"
            ElseIf Directory.Exists(strFolderPath & "\15") Then
                strtxt = strFolderPath & "\15\Built-In Building Blocks.dotx"
            Else
                strtxt = strFolderPath & "\Building Blocks.dotx"
            End If

            If File.Exists(strtxt) Then
                If strtxt.Contains("14") = False And strtxt.Contains("15") = False Then
                    oCurDoc.XMLSchemaReferences.AutomaticValidation = True
                    oCurDoc.XMLSchemaReferences.AllowSaveAsXMLWithoutValidation = False
                End If
            End If

            If File.Exists(strtxt) Then
                Dim attribute As System.IO.FileAttributes = Nothing
                attribute = File.GetAttributes(strtxt)
                If attribute <> FileAttributes.[ReadOnly] Then
                    attribute = FileAttributes.[ReadOnly]
                    File.SetAttributes(strtxt, attribute)
                End If
            End If

            For Each objTemp As Wd.Template In oCurDoc.Application.Templates
                If objTemp.Name = "Building Blocks.dotx" Or objTemp.Name = "Built-In Building Blocks.dotx" Then
                    objTemp.BuildingBlockEntries.Item("Bold Numbers 3").Insert(Where:=oCurDoc.Application.Selection.HeaderFooter.Range, RichText:=True)
                End If
            Next

            If Not String.IsNullOrEmpty(sName) Then
                oCurDoc.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft
                oCurDoc.Application.Selection.HeaderFooter.Range.InsertBefore(sName & Constants.vbTab + Constants.vbTab)
                oCurDoc.Application.Selection.EndKey(Wd.WdUnits.wdStory)
                oCurDoc.Application.Selection.TypeBackspace()
            End If
        Catch
        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Sub

    Public Sub InsertPageFooterWithoutMSWBuildingBlock(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Return
        End If

        Try
            For Each oSection As Wd.Section In oCurDoc.Sections
                If oSection.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                    oSection.Application.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
                End If

                oSection.Application.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter
                oSection.Application.Selection.HeaderFooter.Range.Delete()
                oSection.Application.Selection.HeaderFooter.Range.Font.Name = "Arial"
                oSection.Application.Selection.HeaderFooter.Range.Font.Size = 8
                oSection.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Wd.WdParagraphAlignment.wdAlignParagraphLeft
                oSection.Application.Selection.Range.Text = String.Empty
                oSection.Application.Selection.TypeText("Page ")
                Dim CurrentPage As Object = Wd.WdFieldType.wdFieldPage
                oSection.Application.ActiveWindow.Selection.TypeText(" of ")
                Dim TotalPages As Object = Wd.WdFieldType.wdFieldNumPages
                If Not String.IsNullOrEmpty(sName.Trim()) Then
                    oSection.Application.Selection.HeaderFooter.Range.InsertBefore(sName.Trim() + Constants.vbTab + Constants.vbTab)
                End If
            Next
        Catch
        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Sub

    Private Sub UpdateReminder(ByVal TaskID As Int64)
        'Dim objCon As SqlConnection = New SqlConnection()
        'Dim objCmd As SqlCommand = New SqlCommand()
        'Try
        '    objCon.Open()
        '    objCmd.CommandType = CommandType.Text
        '    objCmd.CommandText = "UPDATE RM_Reminder_MST SET bIsDismissed ='TRUE' WHERE nRefrenceType = 2 AND nReferenceID = " & TaskID & " "
        '    objCmd.Connection = objCon
        '    objCmd.ExecuteNonQuery()
        'Catch __unusedSqlException1__ As SqlException
        '    If objCon.State = ConnectionState.Open Then
        '        objCon.Close()
        '    End If

        'Catch __unusedException2__ As Exception
        '    If objCon.State = ConnectionState.Open Then
        '        objCon.Close()
        '    End If

        'Finally
        '    If (objCon IsNot Nothing) Then
        '        If objCon.State = ConnectionState.Open Then
        '            objCon.Close()
        '        End If

        '        objCon.Dispose()
        '        objCon = Nothing
        '    End If

        '    If objCmd IsNot Nothing Then
        '        objCmd.Parameters.Clear()
        '        objCmd.Dispose()
        '        objCmd = Nothing
        '    End If
        'End Try
    End Sub

    Private Sub Fill_TemplateGallery()
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
    End Sub

    Public Delegate Sub UpdateProgressControlsDelegate()

    Public Sub InvokeProgressUpdateControls()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateProgressControlsDelegate(AddressOf UpdateProgressControls))
            Else
                UpdateProgressControls()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub UpdateProgressControls()
        Try
            If (pbDocument.Value < pbDocument.Maximum) Then
                pbDocument.Value = PrintDocument
                pbDocument.Refresh()
                lblPages.Text = "Printing " & PrintDocument.ToString() & " of " + pbDocument.Maximum.ToString()
                If (PrintDocument = 3) Then
                    btnRestart.Enabled = True
                End If

                Application.DoEvents()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub OnPrintComplete(ByVal iar As IAsyncResult)
        Try
            IsPrintingCompletedWhilePressingPause = True
            Try
                If Not IsPrintingResumed Then
                    InvokeCompleteUpdateControls()
                End If
            Catch
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        Finally
            Dim ar As System.Runtime.Remoting.Messaging.AsyncResult = TryCast(iar, System.Runtime.Remoting.Messaging.AsyncResult)
            If ar IsNot Nothing Then
                Dim invokedMethod As EventHandler = TryCast(ar.AsyncDelegate, EventHandler)
                If invokedMethod IsNot Nothing Then
                    Try
                        invokedMethod.EndInvoke(iar)
                    Catch
                    End Try
                End If
            End If
        End Try
    End Sub

    Public Delegate Sub UpdateCompleteControlsDelegate()

    Public Sub InvokeCompleteUpdateControls()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New UpdateCompleteControlsDelegate(AddressOf UpdateCompleteControls))
            Else
                UpdateCompleteControls()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub UpdateCompleteControls()
        Try
            If (_isFormClose = False) Then
                _isFormClose = True
                Me.Close()
                Me.Dispose(True)
                btnRestart.Enabled = True
                If (btnPause.Visible = False) Then
                    IsPrintingResumed = True
                End If

                btnPause.Update()
                btnRestart.Update()
                btnPause.ResumeLayout()
                btnRestart.ResumeLayout()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnRestart_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub btnPause_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub



    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmgloPrintPrintInfoController))
        Me.btnRestart = New System.Windows.Forms.Button()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.lblPageNoOfDocument = New System.Windows.Forms.Label()
        Me.lblCopies = New System.Windows.Forms.Label()
        Me.lblPrinterNameValue = New System.Windows.Forms.Label()
        Me.lblPrinterName = New System.Windows.Forms.Label()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.lblPages = New System.Windows.Forms.Label()
        Me.pbDocument = New System.Windows.Forms.ProgressBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.panel1.SuspendLayout()
        Me.panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRestart
        '
        Me.btnRestart.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRestart.Enabled = False
        Me.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestart.Image = CType(resources.GetObject("btnRestart.Image"), System.Drawing.Image)
        Me.btnRestart.Location = New System.Drawing.Point(207, 23)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(31, 67)
        Me.btnRestart.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnRestart, "Restart")
        Me.btnRestart.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.lblPageNoOfDocument)
        Me.panel1.Controls.Add(Me.lblCopies)
        Me.panel1.Controls.Add(Me.lblPrinterNameValue)
        Me.panel1.Controls.Add(Me.lblPrinterName)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(1, 2)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(10)
        Me.panel1.Size = New System.Drawing.Size(339, 35)
        Me.panel1.TabIndex = 18
        '
        'lblPageNoOfDocument
        '
        Me.lblPageNoOfDocument.AutoSize = True
        Me.lblPageNoOfDocument.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblPageNoOfDocument.Location = New System.Drawing.Point(326, 10)
        Me.lblPageNoOfDocument.Name = "lblPageNoOfDocument"
        Me.lblPageNoOfDocument.Size = New System.Drawing.Size(0, 13)
        Me.lblPageNoOfDocument.TabIndex = 4
        '
        'lblCopies
        '
        Me.lblCopies.AutoEllipsis = True
        Me.lblCopies.AutoSize = True
        Me.lblCopies.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblCopies.Location = New System.Drawing.Point(326, 10)
        Me.lblCopies.Name = "lblCopies"
        Me.lblCopies.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCopies.Size = New System.Drawing.Size(3, 13)
        Me.lblCopies.TabIndex = 3
        '
        'lblPrinterNameValue
        '
        Me.lblPrinterNameValue.AutoEllipsis = True
        Me.lblPrinterNameValue.AutoSize = True
        Me.lblPrinterNameValue.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPrinterNameValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterNameValue.Location = New System.Drawing.Point(74, 10)
        Me.lblPrinterNameValue.Name = "lblPrinterNameValue"
        Me.lblPrinterNameValue.Size = New System.Drawing.Size(77, 13)
        Me.lblPrinterNameValue.TabIndex = 8
        Me.lblPrinterNameValue.Text = "printer name"
        '
        'lblPrinterName
        '
        Me.lblPrinterName.AutoSize = True
        Me.lblPrinterName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPrinterName.Location = New System.Drawing.Point(10, 10)
        Me.lblPrinterName.Name = "lblPrinterName"
        Me.lblPrinterName.Size = New System.Drawing.Size(64, 13)
        Me.lblPrinterName.TabIndex = 7
        Me.lblPrinterName.Text = "Printing To :"
        '
        'btnPause
        '
        Me.btnPause.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPause.Image = CType(resources.GetObject("btnPause.Image"), System.Drawing.Image)
        Me.btnPause.Location = New System.Drawing.Point(238, 23)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(31, 67)
        Me.btnPause.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnPause, "Pause")
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlay.Image = CType(resources.GetObject("btnPlay.Image"), System.Drawing.Image)
        Me.btnPlay.Location = New System.Drawing.Point(269, 23)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(31, 67)
        Me.btnPlay.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnPlay, "Play")
        Me.btnPlay.UseVisualStyleBackColor = True
        Me.btnPlay.Visible = False
        '
        'lblPages
        '
        Me.lblPages.AutoEllipsis = True
        Me.lblPages.AutoSize = True
        Me.lblPages.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPages.Location = New System.Drawing.Point(10, 10)
        Me.lblPages.Name = "lblPages"
        Me.lblPages.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblPages.Size = New System.Drawing.Size(78, 13)
        Me.lblPages.TabIndex = 2
        Me.lblPages.Text = "Please Wait... "
        '
        'pbDocument
        '
        Me.pbDocument.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbDocument.Location = New System.Drawing.Point(10, 90)
        Me.pbDocument.Name = "pbDocument"
        Me.pbDocument.Size = New System.Drawing.Size(321, 18)
        Me.pbDocument.TabIndex = 9
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(300, 23)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(31, 67)
        Me.btnCancel.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Close")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(340, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 115)
        Me.Label1.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Silver
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(340, 1)
        Me.Label5.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Silver
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 116)
        Me.Label2.TabIndex = 21
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Silver
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(341, 1)
        Me.Label3.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Silver
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(341, 1)
        Me.Label4.TabIndex = 23
        '
        'panel3
        '
        Me.panel3.Controls.Add(Me.btnRestart)
        Me.panel3.Controls.Add(Me.btnPause)
        Me.panel3.Controls.Add(Me.btnPlay)
        Me.panel3.Controls.Add(Me.btnCancel)
        Me.panel3.Controls.Add(Me.lblPages)
        Me.panel3.Controls.Add(Me.pbDocument)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel3.Location = New System.Drawing.Point(0, 0)
        Me.panel3.Name = "panel3"
        Me.panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.panel3.Size = New System.Drawing.Size(341, 118)
        Me.panel3.TabIndex = 19
        '
        'frmgloPrintPrintInfoController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 118)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.panel3)
        Me.Name = "frmgloPrintPrintInfoController"
        Me.Opacity = 0.0R
        Me.Text = "frmgloPrintPrintInfoController"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    '  Private components As System.ComponentModel.IContainer
    Private WithEvents btnRestart As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblPageNoOfDocument As System.Windows.Forms.Label
    Private WithEvents lblCopies As System.Windows.Forms.Label
    Private WithEvents lblPrinterNameValue As System.Windows.Forms.Label
    Private WithEvents lblPrinterName As System.Windows.Forms.Label
    Private WithEvents btnPause As System.Windows.Forms.Button
    Private WithEvents btnPlay As System.Windows.Forms.Button
    Private WithEvents lblPages As System.Windows.Forms.Label
    Private WithEvents pbDocument As System.Windows.Forms.ProgressBar
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents panel3 As System.Windows.Forms.Panel

    Private Sub frmgloPrintBatchPatientStatementController_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            myLoadWord = New gloWord.LoadAndCloseWord()
            ''   gloGlobal.LoadFromAssembly.PrintMenuEventclick(True, "frmgloPrintPrintInfoController")
            'If oListTempleteIds IsNot Nothing Then
            '    If oListTempleteIds.Count > 0 Then
            '        pbDocument.Maximum = oListTempleteIds.Count
            '    End If
            'End If

            If gloGlobal.gloTSPrint.isCopyPrint Then
                lblPrinterNameValue.Text = "To Network Drive"
            Else
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            End If

            If _ExtendedPrinterSettings.IsShowProgress Then
                PrintWithOrWithoutBackground()
            Else
                Hide()
                PrintWithOrWithoutBackground()
            End If

            If Me.Visible = True Then
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmgloPrintBatchPatientStatementController_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            methodToInvoke = Nothing
            ' gloGlobal.LoadFromAssembly.PrintMenuEventclick(False, "frmgloPrintPrintInfoController")
            If (myLoadWord IsNot Nothing) Then
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If

            blnBackGroundPrint = False
            'If oListTempleteIds IsNot Nothing Then
            '    oListTempleteIds.Clear()
            '    oListTempleteIds = Nothing
            'End If
        Catch
        Finally
            Try
                If Not gloGlobal.gloTSPrint.isCopyPrint Then
                    If OldPrinterName <> String.Empty Then
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(OldPrinterName)
                        Application.DoEvents()
                    End If
                End If
            Catch
            End Try
        End Try
    End Sub
End Class
''End Namespace

'